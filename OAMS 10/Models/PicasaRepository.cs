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
        private PicasaService service;
        private int totalPhotos = 0;
        public PicasaService InitPicasaService()
        {
            if (service == null)
            {
                service = new PicasaService("OAMS");
                service.setUserCredentials(AppSetting.GoogleUsername, AppSetting.GooglePassword);

                service.AsyncOperationCompleted += new AsyncOperationCompletedEventHandler(service_AsyncOperationCompleted);
            }

            return service;
        }

        void service_AsyncOperationCompleted(object sender, AsyncOperationCompletedEventArgs e)
        {
            dynamic r = e.UserState;

            int a = r.SiteID;
            AsyncManager am = r.AM;
            am.OutstandingOperations.Decrement();
        }

        public void UploadPhotoToBackupAlbum(Site e, Stream stream)
        {

            PicasaService service = InitPicasaService();

            if (string.IsNullOrEmpty(e.BackupAlbumUrl))
            {
                e.BackupAlbumUrl = CreateAlbum(e.ID.ToString(), true); ;
            }

            Uri postUri = new Uri(e.BackupAlbumUrl.Replace("entry", "feed"));

            stream.Position = 0;

            PicasaEntry entry = new PhotoEntry();
            entry.MediaSource = new Google.GData.Client.MediaFileSource(stream, "backup", "image/jpeg");

            PicasaEntry createdEntry = service.Insert(postUri, entry);
        }

        public void UploadPhoto(Site e, IEnumerable<HttpPostedFileBase> files, string[] noteList)
        {
            if (files == null
                || files.Count() == 0
                || files.Where(r => r != null).Count() == 0)
            {
                return;
            }

            PicasaService service = InitPicasaService();

            if (string.IsNullOrEmpty(e.AlbumUrl))
            {
                e.AlbumUrl = CreateAlbum(e.ID.ToString());
            }

            Uri postUri = new Uri(e.AlbumUrl.Replace("entry", "feed").ToHttpsUri());

            for (int i = 0; i < files.Count(); i++)
            {
                var item = files.ElementAt(i);

                if (item != null)
                {
                    DateTime? takenDate = GetMetadata_TakenDate(item);

                    float? lng = null;
                    float? lat = null;
                    GetMetadata_GPS(item, out lng, out lat);

                    MemoryStream mStream = new MemoryStream();

                    item.InputStream.Position = 0;
                    item.InputStream.CopyTo(mStream);
                    mStream.Position = 0;

                    PicasaEntry entry = new PhotoEntry();
                    entry.MediaSource = new Google.GData.Client.MediaFileSource(mStream, Path.GetFileName(item.FileName), "image/jpeg");
                    entry.Title = new AtomTextConstruct(AtomTextConstructElementType.Title, noteList[i]);
                    entry.Summary = new AtomTextConstruct(AtomTextConstructElementType.Summary, noteList[i]);

                    PicasaEntry createdEntry = service.Insert(postUri, entry);

                    if (createdEntry != null)
                    {
                        SitePhoto photo = new SitePhoto();

                        photo.Url = createdEntry.Media.Content.Url;
                        photo.AtomUrl = createdEntry.EditUri.Content;
                        photo.TakenDate = takenDate;
                        photo.Lng = lng;
                        photo.Lat = lat;
                        photo.Note = noteList[i];
                        e.SitePhotoes.Add(photo);
                    }
                }
            }
        }

        private static DateTime? GetMetadata_TakenDate(HttpPostedFileBase item)
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
            PicasaService service = InitPicasaService();

            AlbumEntry newEntry = new AlbumEntry();

            newEntry.Title.Text = name + (isBackup ? "B" : "");
            newEntry.Summary.Text = newEntry.Title.Text;

            Uri feedUri = new Uri(PicasaQuery.CreatePicasaUri(AppSetting.GoogleUsername).ToHttpsUri());

            PicasaEntry createdEntry = (PicasaEntry)service.Insert(feedUri, newEntry);

            //5507469898148065681
            return createdEntry.EditUri.Content;
        }

        public void DeletePhoto(SitePhoto item)
        {
            PicasaService service = InitPicasaService();
            PicasaEntry a = (PicasaEntry)service.Get(item.AtomUrl);

            byte[] b;
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(item.Url);
            WebResponse myResp = myReq.GetResponse();
            Stream stream = myResp.GetResponseStream();

            using (BinaryReader br = new BinaryReader(stream))
            {
                b = br.ReadBytes(50000000);
                br.Close();
            }
            myResp.Close();

            MemoryStream mem = new MemoryStream(b);

            UploadPhotoToBackupAlbum(item.Site, mem);

            a.Delete();
        }

        public PicasaEntry MovingPhoto(string photoUrl, string photoAtomUrl, string albumUrl, string note)
        {
            PicasaService service = InitPicasaService();

            byte[] b;
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(photoUrl.ToHttpsUri());
            WebResponse myResp = myReq.GetResponse();
            Stream stream = myResp.GetResponseStream();

            using (BinaryReader br = new BinaryReader(stream))
            {
                b = br.ReadBytes(50000000);
                br.Close();
            }
            myResp.Close();

            MemoryStream mem = new MemoryStream(b);

            var entry = UploadPhoto(albumUrl, mem, note);

            PicasaEntry a = (PicasaEntry)service.Get(photoAtomUrl.ToHttpsUri());
            a.Delete();

            return entry;
        }

        public PicasaEntry UploadPhoto(string albumUrl, Stream stream, string note)
        {
            PicasaEntry createdEntry = null;

            if (!string.IsNullOrEmpty(albumUrl))
            {
                PicasaService service = InitPicasaService();
                SiteDetailRepository siteDetailRepository = new SiteDetailRepository();
                siteDetailRepository.DB = DB;

                Uri postUri = new Uri(albumUrl.Replace("entry", "feed"));

                stream.Position = 0;

                PicasaEntry entry = new PhotoEntry();
                entry.MediaSource = new Google.GData.Client.MediaFileSource(stream, "move", "image/jpeg");
                entry.Title = new AtomTextConstruct(AtomTextConstructElementType.Title, note);
                entry.Summary = new AtomTextConstruct(AtomTextConstructElementType.Summary, note);

                createdEntry = service.Insert(postUri, entry);
            }
            return createdEntry;
        }


        public void UploadPhoto(SiteMonitoring e, IEnumerable<HttpPostedFileBase> files, string[] noteList, bool isCheckDate = true, bool? IsReview = null)
        {
            if (files == null
                || files.Count() == 0
                || files.Where(r => r != null).Count() == 0)
            {
                return;
            }

            PicasaService service = InitPicasaService();

            if (string.IsNullOrEmpty(e.AlbumUrl))
            {
                e.AlbumUrl = CreateAlbum("M" + e.ID.ToString());
            }

            Uri postUri = new Uri(e.AlbumUrl.Replace("entry", "feed"));

            for (int i = 0; i < files.Count(); i++)
            {
                var item = files.ElementAt(i);
                if (item != null)
                {
                    DateTime? takenDate = GetMetadata_TakenDate(item);

                    float? lng = null;
                    float? lat = null;
                    GetMetadata_GPS(item, out lng, out lat);

                    ContractDetailTimeline timeline = e.ContractDetail.ContractDetailTimelines.Where(r => r.Order == e.Order).FirstOrDefault();
                    if (
                        !isCheckDate ||
                        (timeline != null
                        && takenDate.HasValue
                        && timeline.Contains(takenDate))
                        )
                    {
                        MemoryStream mStream = new MemoryStream();

                        item.InputStream.Position = 0;
                        item.InputStream.CopyTo(mStream);
                        mStream.Position = 0;

                        //PicasaEntry entry = (PicasaEntry)service.Insert(postUri, mStream, "image/jpeg", "");
                        //PicasaEntry entry = (PicasaEntry)service.Insert(postUri, item.InputStream, "image/jpeg", "");
                        //photoUriList.Add(entry.Media.Content.Url);


                        PicasaEntry entry = new PhotoEntry();
                        entry.MediaSource = new Google.GData.Client.MediaFileSource(mStream, Path.GetFileName(item.FileName), "image/jpeg");
                        entry.Title = new AtomTextConstruct(AtomTextConstructElementType.Title, noteList[i]);
                        entry.Summary = new AtomTextConstruct(AtomTextConstructElementType.Summary, noteList[i]);

                        //service.InsertAsync(postUri, entry, new { SiteID = e.ID, AM = asyncManager });
                        PicasaEntry createdEntry = service.Insert(postUri, entry);

                        if (createdEntry != null)
                        {
                            SiteMonitoringPhoto photo = new SiteMonitoringPhoto();

                            photo.Url = createdEntry.Media.Content.Url;
                            photo.AtomUrl = createdEntry.EditUri.Content;
                            photo.TakenDate = takenDate;
                            photo.Lng = lng;
                            photo.Lat = lat;
                            photo.Note = noteList[i];
                            photo.IsReview = IsReview;

                            e.SiteMonitoringPhotoes.Add(photo);
                        }
                    }
                }
            }
        }

        public void DeletePhoto(SiteMonitoringPhoto item)
        {
            PicasaService service = InitPicasaService();
            PicasaEntry a = (PicasaEntry)service.Get(item.AtomUrl);

            byte[] b;
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(item.Url);
            WebResponse myResp = myReq.GetResponse();
            Stream stream = myResp.GetResponseStream();

            using (BinaryReader br = new BinaryReader(stream))
            {
                b = br.ReadBytes(50000000);
                br.Close();
            }
            myResp.Close();

            MemoryStream mem = new MemoryStream(b);

            UploadPhotoToBackupAlbum(item.SiteMonitoring, mem);

            a.Delete();
        }

        public void DeletePhoto(SiteDetailPhoto item)
        {
            PicasaService service = InitPicasaService();
            PicasaEntry a = (PicasaEntry)service.Get(item.AtomUrl);

            byte[] b;
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(item.Url);
            WebResponse myResp = myReq.GetResponse();
            Stream stream = myResp.GetResponseStream();

            using (BinaryReader br = new BinaryReader(stream))
            {
                b = br.ReadBytes(50000000);
                br.Close();
            }
            myResp.Close();

            MemoryStream mem = new MemoryStream(b);

            UploadPhotoToBackupAlbum(item.SiteDetail, mem);

            a.Delete();
        }

        public void UploadPhotoToBackupAlbum(SiteMonitoring e, Stream stream)
        {
            PicasaService service = InitPicasaService();

            if (string.IsNullOrEmpty(e.BackupAlbumUrl))
            {
                e.BackupAlbumUrl = CreateAlbum("M" + e.ID.ToString(), true);
            }

            Uri postUri = new Uri(e.BackupAlbumUrl.Replace("entry", "feed"));

            stream.Position = 0;

            PicasaEntry entry = new PhotoEntry();
            entry.MediaSource = new Google.GData.Client.MediaFileSource(stream, "backup", "image/jpeg");

            PicasaEntry createdEntry = service.Insert(postUri, entry);
        }

        public void UploadPhotoToBackupAlbum(SiteDetail e, Stream stream)
        {
            PicasaService service = InitPicasaService();

            if (string.IsNullOrEmpty(e.BackupAlbumUrl))
            {
                e.BackupAlbumUrl = CreateAlbum("SD_" + e.ID.ToString(), true);
            }

            Uri postUri = new Uri(e.BackupAlbumUrl.Replace("entry", "feed"));

            stream.Position = 0;

            PicasaEntry entry = new PhotoEntry();
            entry.MediaSource = new Google.GData.Client.MediaFileSource(stream, "backup", "image/jpeg");

            PicasaEntry createdEntry = service.Insert(postUri, entry);
        }

        public void UploadPhoto(List<SDP> siteDetailFiles)
        {
            if (siteDetailFiles == null)
            {
                return;
            }

            var v = siteDetailFiles.Where(r => r.File != null).ToList().GroupBy(r => r.SiteDetailID);

            if (v.Count() == 0)
            {
                return;
            }

            PicasaService service = InitPicasaService();
            SiteDetailRepository siteDetailRepository = new SiteDetailRepository();
            siteDetailRepository.DB = DB;

            foreach (var item in v)
            {
                if (item.Count() == 0) continue;

                var siteDetail = siteDetailRepository.Get(item.Key);
                if (string.IsNullOrEmpty(siteDetail.AlbumUrl))
                {
                    siteDetail.AlbumUrl = CreateAlbum("SD_" + siteDetail.ID.ToString());
                }

                Uri postUri = new Uri(siteDetail.AlbumUrl.Replace("entry", "feed"));

                for (int i = 0; i < item.Count(); i++)
                {
                    var file = item.ElementAt(i).File;
                    var note = item.ElementAt(i).Note;
                    if (file != null)
                    {
                        DateTime? takenDate = GetMetadata_TakenDate(file);

                        float? lng = null;
                        float? lat = null;
                        GetMetadata_GPS(file, out lng, out lat);

                        MemoryStream mStream = new MemoryStream();

                        file.InputStream.Position = 0;
                        file.InputStream.CopyTo(mStream);
                        mStream.Position = 0;

                        PicasaEntry entry = new PhotoEntry();
                        entry.MediaSource = new Google.GData.Client.MediaFileSource(mStream, Path.GetFileName(file.FileName), "image/jpeg");
                        entry.Title = new AtomTextConstruct(AtomTextConstructElementType.Title, note);
                        entry.Summary = new AtomTextConstruct(AtomTextConstructElementType.Summary, note);

                        PicasaEntry createdEntry = service.Insert(postUri, entry);

                        if (createdEntry != null)
                        {
                            var photo = new SiteDetailPhoto();

                            photo.Url = createdEntry.Media.Content.Url;
                            photo.AtomUrl = createdEntry.EditUri.Content;
                            photo.TakenDate = takenDate;
                            photo.Lng = lng;
                            photo.Lat = lat;
                            photo.Note = note;
                            siteDetail.SiteDetailPhotoes.Add(photo);
                        }
                    }
                }
            }
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
                    var service = InitPicasaService();

                    AlbumQuery query = new AlbumQuery();

                    query.Uri = new Uri(AppSetting.AlbumAtomUrl);

                    var picasaFeed = service.Query(query);

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
            Update_AlbumAtomUrl();

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
            PicasaService service = InitPicasaService();
            var atom = service.Get(photoAtomUrl.ToHttpsUri());

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
    }
}
