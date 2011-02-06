using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAMS.Models;

namespace OAMS.Controllers
{
    [CustomAuthorize]
    public class ContractorController : BaseController<ContractorRepository>
    {
        public ActionResult Index()
        {
            var v = Repo.GetAll();
            return View(v);
        }

        //
        // GET: /Contractor/Details/5

        public ActionResult Details(int id)
        {
            return View(Repo.Get(id));
        }

        //
        // GET: /Contractor/Create

        public ActionResult Create()
        {
            return View(new Contractor());
        }

        //
        // POST: /Contractor/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            // TODO: Add insert logic here
            var v = new Contractor();
            UpdateModel(v);
            Repo.Add(v);
            return RedirectToAction("Index");
        }

        //
        // GET: /Contractor/Edit/5

        public ActionResult Edit(int id)
        {
            return View(Repo.Get(id));
        }

        //
        // POST: /Contractor/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            // TODO: Add update logic here
            var v = Repo.Get(id);
            UpdateModel(v);
            Repo.Update(v);
            return RedirectToAction("Index");
        }

        //
        // GET: /Contractor/Delete/5

        public ActionResult Delete(int id)
        {
            var v = Repo.Get(id);
            Repo.Delete(v);
            return RedirectToAction("Index");
        }

        //
        // POST: /Contractor/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            // TODO: Add delete logic here

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Replace(int id, int replaceID)
        {
            bool r = Repo.Replace(id, replaceID);
            string str = "";
            if (r)
                str = "Replace Done.";
            else
                str = "Replace Fail.";
            return Json(str);
        }
    }
}
