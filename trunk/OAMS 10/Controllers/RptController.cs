using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAMS.Models;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Reflection;
using Microsoft.CSharp.RuntimeBinder;

namespace OAMS.Controllers
{
    public class RptController : Controller
    {
        public ActionResult _101()
        {
            return View();
        }

        [HttpPost]
        public ActionResult _101(Rpt101 e)
        {

            OAMSEntities db = new OAMSEntities();

            e.sdList = db.Sites.Where(r => true
                && (r.Geo1 != null && r.Geo1.FullName == e.Geo1FullName)
                ).SelectMany(r => r.SiteDetails);

            return View(e);
        }

        public ActionResult _102(Rpt102 e)
        {
            if (e == null) e = new Rpt102();
            OAMSEntities db = new OAMSEntities();

            e.List = db.SiteDetailMores
                .Where(r =>
                    r.Product != null
                    && r.Product.Category1 != null
                    &&
                    (string.IsNullOrEmpty(e.Cat1FullName)
                        || r.Product.Category1.FullName == e.Cat1FullName))
                .OrderBy(r => r.Product.Category1.Name).ToList();

            return View(e);
        }


    }
}
