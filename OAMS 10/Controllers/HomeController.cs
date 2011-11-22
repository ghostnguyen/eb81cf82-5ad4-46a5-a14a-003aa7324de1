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
            List<Item> itemL = new List<Item>() {
            new Item(){Name = "Client",Values = new List<object>(){"Unilever"}},
            //new Item(){Name = "Type"},
            new Item(){Name = "Type",Values= new List<object>(){"BBR"},Count=true},
            new Item(){Name = "Total"},
            };

            OAMSEntities db = new OAMSEntities();


            var v = db.SiteDetailMores.Select(r => new Row()
            {
                Client = r.Product.Client.Name,
                Type = r.SiteDetail.Type,
            });

            foreach (var item in itemL.Where(r => r.Values != null && r.Count == false))
            {
                //v = v.Where("@0.Contains(Client)", item.Values);
            }


            //db.SiteDetailMores.Where



            //foreach (var item in itemL.Where(r => r.Values != null))
            //{

            //}

            //foreach (var item in itemL.Where(r => r.Values == null))
            //{

            //}


            return View();
        }

        public ViewResult Chart(string id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}
