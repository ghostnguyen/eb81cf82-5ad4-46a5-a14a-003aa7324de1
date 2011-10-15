using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.GData.Photos;
using Google.GData.Client;
using System.IO;
using System.Web.Mvc.Async;
using System.Net;

using System.Drawing;
using System.Drawing.Imaging;
using ExifLibrary;

namespace OAMS.Models
{
    public class PicasaRepository : BaseRepository<PicasaRepository>
    {
        private PicasaService service1;
        private int totalPhotos = 0;

        public PicasaService PicasaService
        {
            get
            {
                if (service1 == null)
                {
                    service1 = new PicasaService("OAMS");
                    service1.setUserCredentials(AppSetting.GoogleUsername, AppSetting.GooglePassword);
                }

                return service1;
            }
        }

        public List<PicasaEntry> UploadPhoto2(IEnumerable<HttpPostedFileBase> files, string[] noteList = null)
        {
            if (files == null
                || files.Count() == 0
                || files.Where(r => r != null).Count() == 0)
            {
                return null;
            }

            List<PicasaEntry> l = new List<PicasaEntry>();

            Update_AlbumAtomUrl();

            Uri postUri = new Uri(AppSetting.AlbumAtomUrl.Replace("entry", "feed").ToHttpsUri());

            for (int i = 0; i < files.Count(); i++)
            {
                var item = files.ElementAt(i);

                if (item != null)
                {
                    //DateTime? takenDate = GetMetadata_TakenDate(item);

                    float? lng = null;
                    float? lat = null;
                    GetMetadata_GPS(item, out lng, out lat);

                    MemoryStream mStream = new MemoryStream();

                    item.InputStream.Position = 0;
                    item.InputStream.CopyTo(mStream);
                    mStream.Position = 0;

                    PicasaEntry entry = new PhotoEntry();
                    entry.MediaSource = new Google.GData.Client.MediaFileSource(mStream, Path.GetFileName(item.FileName), "image/jpeg");
                    if (noteList != null)
                    {
                        entry.Title = new AtomTextConstruct(AtomTextConstructElementType.Title, noteList[i]);
                        entry.Summary = new AtomTextConstruct(AtomTextConstructElementType.Summary, noteList[i]);
                    }

                    PicasaEntry createdEntry = PicasaService.Insert(postUri, entry);

                    l.Add(createdEntry);
                }
            }

            return l;
        }


        public static DateTime? GetMetadata_TakenDate(HttpPostedFileBase item)
        {
            DateTime? takenDate = null;
            item.InputStream.Position = 0;
            Image img = Image.FromStream(item.InputStream);
            //http://msdn.microsoft.com/en-us/library/system.drawing.imaging.propertyitem.id.aspx
            PropertyItem prop = img.PropertyItems.Where(r => r.Id == 306).FirstOrDefault();
            if (prop != null)
            {
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                takenDate = DateTime.Parse(encoding.GetString(prop.Value).Trim('\0').Replace(4, '/').Replace(7, '/'));
            }
            return takenDate;
        }

        private static void GetMetadata_GPS(HttpPostedFileBase item, out float? lng, out float? lat)
        {
            MemoryStream mStream = new MemoryStream();

            item.InputStream.Position = 0;
            item.InputStream.CopyTo(mStream);
            mStream.Position = 0;

            var data = ExifFile.Read(mStream);

            if (data.Properties.ContainsKey(ExifTag.GPSLongitude))
            {
                GPSLatitudeLongitude gps_lng = data.Properties[ExifTag.GPSLongitude] as GPSLatitudeLongitude;
                lng = gps_lng.ToFloat();
            }
            else lng = null;

            if (data.Properties.ContainsKey(ExifTag.GPSLatitude))
            {
                GPSLatitudeLongitude gps_lat = data.Properties[ExifTag.GPSLatitude] as GPSLatitudeLongitude;
                lat = gps_lat.ToFloat();
            }
            else lat = null;
        }


        public string CreateAlbum(string name, bool isBackup = false)
        {
            AlbumEntry newEntry = new AlbumEntry();

            newEntry.Title.Text = name + (isBackup ? "B" : "");
            newEntry.Summary.Text = newEntry.Title.Text;

            Uri feedUri = new Uri(PicasaQuery.CreatePicasaUri(AppSetting.GoogleUsername).ToHttpsUri());

            PicasaEntry createdEntry = (PicasaEntry)PicasaService.Insert(feedUri, newEntry);

            //5507469898148065681
            return createdEntry.EditUri.Content;
        }

        public void CreateGenericAlbum()
        {
            AppSettingRepository appBLL = new AppSettingRepository();
            string url = CreateAlbum(DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss"));
            appBLL.InsertOrUpdate("AlbumAtomUrl", url);
        }

        public void Update_AlbumAtomUrl()
        {
            if (string.IsNullOrEmpty(AppSetting.AlbumAtomUrl))
            {
                CreateGenericAlbum();
            }
            else
            {
                if (totalPhotos == 0 || totalPhotos >= 990)
                {
                    AlbumQuery query = new AlbumQuery();

                    query.Uri = new Uri(AppSetting.AlbumAtomUrl);

                    var picasaFeed = PicasaService.Query(query);

                    if (picasaFeed != null)
                    {
                        totalPhotos = (new AlbumAccessor((PicasaEntry)picasaFeed.Entries[0])).NumPhotos.ToInt();
                    }
                }

                if (totalPhotos >= 990)
                {
                    CreateGenericAlbum();
                    totalPhotos = 0;
                }

            }
        }

        public PicasaEntry MovePhoto2GenericAlbum(IPhoto p)
        {
            string albumid = AppSetting.AlbumAtomUrl.Split('/')[9].Split('?')[0];

            string note = "";
            if (p is SitePhoto)
            {
                var sp = p as SitePhoto;
                note = string.Format("SitePhoto_{0}_Site_{1}", sp.ID.ToString(), sp.SiteID.ToString());
            }

            var entry = MovingPhoto1(p.Url, p.AtomUrl, albumid, note);

            if (entry != null)
            {
                if (p.Url != entry.Media.Content.Url)
                    p.Url = entry.Media.Content.Url;

                if (p.AtomUrl != entry.EditUri.Content)
                {
                    p.AtomUrl = entry.EditUri.Content;
                }

                totalPhotos++;
            }

            return null;
        }

        public PicasaEntry MovingPhoto1(string photoUrl, string photoAtomUrl, string albumid, string note)
        {
            Update_AlbumAtomUrl();

            var atom = PicasaService.Get(photoAtomUrl.ToHttpsUri());

            PicasaEntry a = (PicasaEntry)atom;

            string old = a.GetPhotoExtensionValue("albumid");
            if (old != albumid)
            {
                a.Title = new AtomTextConstruct(AtomTextConstructElementType.Title, note);
                a.Summary = new AtomTextConstruct(AtomTextConstructElementType.Summary, note);

                a.SetPhotoExtensionValue("albumid", albumid);
                a.Update();
            }

            return a;
        }

        public void UpdateTitle(string photoAtomUrl, string title)
        {
            var atom = PicasaService.Get(photoAtomUrl.ToHttpsUri());
            PicasaEntry a = (PicasaEntry)atom;

            a.Title = new AtomTextConstruct(AtomTextConstructElementType.Title, title);
            a.Summary = new AtomTextConstruct(AtomTextConstructElementType.Summary, title);

            a.Update();
        }

        public void DeletePhoto(string photoAtomUrl)
        {
            var atom = PicasaService.Get(photoAtomUrl.ToHttpsUri());
            PicasaEntry a = (PicasaEntry)atom;

            a.Title = new AtomTextConstruct(AtomTextConstructElementType.Title, "X_" + a.Title.Text);
            a.Summary = new AtomTextConstruct(AtomTextConstructElementType.Summary, "X_" + a.Summary.Text);

            a.Update();
        }
    }
}
