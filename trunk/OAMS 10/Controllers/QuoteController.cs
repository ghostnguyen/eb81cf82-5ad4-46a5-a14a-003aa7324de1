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
    public class QuoteController : BaseController<QuoteRepository>
    {
        QuoteRepository repo = new QuoteRepository();

        public ActionResult Index()
        {
            var v = repo.GetAll();
            return View(v);
        }


        public ActionResult Details(int id)
        {
            return View(repo.Get(id));
        }


        public ActionResult Create()
        {
            Quote e = new Quote();
            return View(e);
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            // TODO: Add insert logic here

            var v = new Quote();

            UpdateModel(v);

            repo.Add(v);

            repo.Save();

            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {
            return View(repo.Get(id));
        }


        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var v = repo.Get(id);

            UpdateModel(v);

            repo.Save();

            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var v = repo.Get(id);
            repo.Delete(v);
            repo.Save();
            return View();
        }


        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult AddSite(int quoteID, int siteDetailID)
        {
            repo.AddSite(quoteID, siteDetailID);
            return Json(0);
        }

        //public ActionResult ViewReport(int id, DateTime? dFrom, DateTime? dTo)
        //{
        //    return View(repo.Report(id, dFrom, dTo));
        //}

        //public ActionResult ViewReportDetail(int id, DateTime? dFrom, DateTime? dTo)
        //{
        //    //return View(repo.ReportDetails(id, dFrom, dTo).Take(3).ToList());
        //    return View(repo.ReportDetails(id, dFrom, dTo));
        //}

        //[HttpPost]
        //public JsonResult OverwriteTimelineForDetail(int id)
        //{
        //    repo.OverwriteTimelineForDetail(id);
        //    return Json(0);
        //}
    }
}
