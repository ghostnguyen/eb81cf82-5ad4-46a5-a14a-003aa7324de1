using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAMS.Models;

namespace OAMS.Controllers
{
    [CustomAuthorize]
    public class ClientController : Controller
    {
        //
        // GET: /Client/

        ClientRepository repo = new ClientRepository();

        
        public ActionResult Index()
        {
            var v = repo.GetAll();
            return View(v);
        }

        //
        // GET: /Client/Details/5

        public ActionResult Details(int id)
        {
            return View(repo.Get(id));
        }

        //
        // GET: /Client/Create
        public ActionResult Create()
        {
            return View(new Client());
        }

        //
        // POST: /Client/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            // TODO: Add insert logic here
            var v = new Client();
            UpdateModel(v);
            repo.Add(v);
            return RedirectToAction("Index");
        }

        //
        // GET: /Client/Edit/5

        public ActionResult Edit(int id)
        {
            return View(repo.Get(id));
        }

        //
        // POST: /Client/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            // TODO: Add update logic here
            var v = repo.Get(id);
            UpdateModel(v);
            repo.Update(v);
            return RedirectToAction("Index");
        }

        //
        // GET: /Client/Delete/5

        public ActionResult Delete(int id)
        {
            var v = repo.Get(id);
            if (v.Products.Count == 0)
            {
                repo.Delete(v);
            }
            return RedirectToAction("Index");
        }

        //
        // POST: /Client/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            // TODO: Add delete logic here

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Replace(int id, int replaceID)
        {
            bool r = repo.Replace(id, replaceID);
            string str = "";
            if (r)
                str = "Replace Done.";
            else
                str = "Replace Fail.";
            return Json(str);
        }
    }
}
