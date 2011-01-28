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
    public class ContractController : BaseController<ContractRepository>
    {
        //
        // GET: /Contractor/

        ContractRepository repo = new ContractRepository();

        public ActionResult Index()
        {
            var v = repo.GetAll();
            return View(v);
        }

        //
        // GET: /Contractor/Details/5

        public ActionResult Details(int id)
        {
            return View(repo.Get(id));
        }

        //
        // GET: /Contractor/Create

        public ActionResult Create()
        {
            Contract e = new Contract();
            //e.EffectiveDate = DateTime.Now;
            return View(e);
        }

        //
        // POST: /Contractor/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            // TODO: Add insert logic here

            var v = new Contract();

            UpdateModel(v);

            repo.Add(v);

            repo.Save();

            return RedirectToAction("Index");
        }

        //
        // GET: /Contractor/Edit/5

        public ActionResult Edit(int id)
        {
            return View(repo.Get(id));
        }

        //
        // POST: /Contractor/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var v = repo.Get(id);

            UpdateModel(v);

            repo.Save();

            return RedirectToAction("Index");
        }

        //
        // GET: /Contractor/Delete/5

        public ActionResult Delete(int id)
        {
            var v = repo.Get(id);
            repo.Delete(v);
            repo.Save();
            return View();
        }

        //
        // POST: /Contractor/Delete/5

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
        public JsonResult AddSite(int contractID, int siteDetailID)
        {
            repo.AddSite(contractID, siteDetailID);
            return Json(0);
        }

        public ActionResult ViewReport(int id, DateTime? dFrom, DateTime? dTo)
        {
            return View(repo.Report(id, dFrom, dTo));
        }

        public ActionResult ViewReportDetail(int id, DateTime? dFrom, DateTime? dTo)
        {
            //return View(repo.ReportDetails(id, dFrom, dTo).Take(3).ToList());
            return View(repo.ReportDetails(id, dFrom, dTo));
        }

        [HttpPost]
        public JsonResult OverwriteTimelineForDetail(int id)
        {
            repo.OverwriteTimelineForDetail(id);
            return Json(0);
        }
    }
}
