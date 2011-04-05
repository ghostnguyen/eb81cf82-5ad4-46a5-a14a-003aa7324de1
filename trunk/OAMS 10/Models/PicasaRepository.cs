﻿using System;
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

namespace OAMS.Models
{
    public class PicasaRepository : BaseRepository
    {
        public PicasaService InitPicasaService()
        {
            PicasaService service = new PicasaService("OAMS");
            service.setUserCredentials(AppSetting.GoogleUsername, AppSetting.GooglePassword);

            service.AsyncOperationCompleted += new AsyncOperationCompletedEventHandler(service_AsyncOperationCompleted);
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

            Uri postUri = new Uri(e.AlbumUrl.Replace("entry", "feed"));

            for (int i = 0; i < files.Count(); i++)
            {
                var item = files.ElementAt(i);

                if (item != null)
                {
                    DateTime? takenDate = GetMetadata_TakenDate(item);

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
                        photo.Note = noteList[i];
                        e.SitePhotoes.Add(photo);
                    }
                }
            }
        }

        private static DateTime? GetMetadata_TakenDate(HttpPostedFileBase item)
        {
            DateTime? takenDate = null;
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


        public string CreateAlbum(string name, bool isBackup = false)
        {
            PicasaService service = InitPicasaService();

            AlbumEntry newEntry = new AlbumEntry();

            newEntry.Title.Text = name + (isBackup ? "B" : "");
            newEntry.Summary.Text = newEntry.Title.Text;

            Uri feedUri = new Uri(PicasaQuery.CreatePicasaUri(AppSetting.GoogleUsername));

            PicasaEntry createdEntry = (PicasaEntry)service.Insert(feedUri, newEntry);

            //5507469898148065681
            return createdEntry.EditUri.Content;
            //return createdEntry.Id.AbsoluteUri;
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
                b = br.ReadBytes(500000);
                br.Close();
            }
            myResp.Close();

            MemoryStream mem = new MemoryStream(b);

            UploadPhotoToBackupAlbum(item.Site, mem);

            a.Delete();
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
                b = br.ReadBytes(500000);
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
                b = br.ReadBytes(500000);
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
                            photo.Note = note;
                            siteDetail.SiteDetailPhotoes.Add(photo);
                        }
                    }
                }
            }
        }
    }
}
