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
    }
}
