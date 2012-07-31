using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAMS.Models;

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
            if (id > 0)
            {
                SiteDetailPhotoRepository rep = new SiteDetailPhotoRepository();
                var v = rep.DB.SiteDetailPhotoes.SingleOrDefault(r => r.ID == id);
                if (v != null)
                {
                    ViewBag.Url = v.Url;
                }
            }
            return View();
        }
    }
}
