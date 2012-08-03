using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAMS.Models;
using System.Drawing.Imaging;
using System.Net;
using System.IO;
using System.Drawing;

namespace OAMS.Controllers
{
    [HandleError]
    [CustomAuthorize]
    public class HomeController : BaseController<HomeRepository, HomeController>
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to Outdoor Advertising Managerment System.";

            return View();
        }

        public ActionResult About()
        {
            //ImportRepository r = new ImportRepository();
            //r.Import();
            return View();
        }

        public class Item
        {
            public string Name { get; set; }
            public bool Count { get; set; }
            public List<object> Values { get; set; }
            public bool Visible { get; set; }
        }

        public class Row
        {
            public string Client { get; set; }
            public string Type { get; set; }
        }

        public ActionResult About2()
        {
            OAMSEntities db = new OAMSEntities();

            var v = db.Sites.Where(r => r.Score < 50).ToList();
            v.ForEach(r => r.UpdateScore());

            db.SaveChanges();

            return View();
        }

        public ViewResult Chart(string id)
        {
            ViewBag.Id = id;
            return View();
        }

        public ViewResult CreativeTool(int? id)
        {
            ViewBag.ID = id.HasValue ? id.ToString() : "";
            return View();
        }

        public ActionResult SiteDetailPhoto(int? id)
        {
            ImageResult imageResult = null;
            if (id > 0)
            {
                SiteDetailPhotoRepository rep = new SiteDetailPhotoRepository();
                var v = rep.DB.SiteDetailPhotoes.SingleOrDefault(r => r.ID == id);
                if (v != null)
                {
                    
                    ImageFormat format;
                    Image image;
                    using (var client = new WebClient())
                    {
                        try
                        {
                            var data = client.DownloadData(v.Url);
                            var contentType = client.ResponseHeaders["Content-Type"];
                            format = mimeTypes[contentType];
                            var ms = new MemoryStream();
                            ms.Write(data, 0, data.Length);
                            image = System.Drawing.Image.FromStream(ms);

                            imageResult = new ImageResult { Image = image, ImageFormat = format };
                        }
                        catch (WebException exception)
                        {
                            //return new HttpResult("Image error", HttpStatusCode.NotFound);
                        }
                    }
                }
            }
           
            return imageResult;
        }

        public class ImageResult : ActionResult
        {
            public ImageResult() { }
            public Image Image { get; set; }
            public ImageFormat ImageFormat { get; set; }
            public override void ExecuteResult(ControllerContext context)
            {
                // verify properties 
                if (Image == null)
                {
                    throw new ArgumentNullException("Image");
                }
                if (ImageFormat == null)
                {
                    throw new ArgumentNullException("ImageFormat");
                }
                // output 
                context.HttpContext.Response.Clear();
                if (ImageFormat.Equals(ImageFormat.Bmp)) context.HttpContext.Response.ContentType = "image/bmp";
                if (ImageFormat.Equals(ImageFormat.Gif)) context.HttpContext.Response.ContentType = "image/gif";
                if (ImageFormat.Equals(ImageFormat.Icon)) context.HttpContext.Response.ContentType = "image/vnd.microsoft.icon";
                if (ImageFormat.Equals(ImageFormat.Jpeg)) context.HttpContext.Response.ContentType = "image/jpeg";
                if (ImageFormat.Equals(ImageFormat.Png)) context.HttpContext.Response.ContentType = "image/png";
                if (ImageFormat.Equals(ImageFormat.Tiff)) context.HttpContext.Response.ContentType = "image/tiff";
                if (ImageFormat.Equals(ImageFormat.Wmf)) context.HttpContext.Response.ContentType = "image/wmf";
                Image.Save(context.HttpContext.Response.OutputStream, ImageFormat);
            }
        }

        private Dictionary<string, ImageFormat> mimeTypes = new Dictionary<string, ImageFormat>{
                                                          {"image/gif", ImageFormat.Gif},
                                                          {"image/jpeg", ImageFormat.Jpeg},
                                                          {"image/png", ImageFormat.Png},
                                                      };
    }
}
