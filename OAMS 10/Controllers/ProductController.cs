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
    public class ProductController : BaseController<ProductRepository>
    {
        //
        // GET: /Product/

        public ActionResult Index()
        {
            return View(Repo.GetAll());
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create(Product v)
        {
            try
            {
                Repo.Add(v);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Product/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var r = Repo.Get(id);
            r.NewCategoryFullName = r.CategoryFullName;

            return View(r);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(Product r)
        {
            try
            {
                r = Repo.Update(r);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(int id)
        {
            var v = Repo.Get(id);
            //if (v.Products.Count == 0)
            //{
            //    Repo.Delete(v);
            //}
            Repo.Delete(v);
            return RedirectToAction("Index");
        }

        //
        // POST: /Product/Delete/5

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
        public JsonResult Replace(int id, int replaceID)
        {
            string str = "";

            if (id == replaceID)
            {
                str = "The same items.";
            }
            else
            {
                bool r = Repo.Replace(id, replaceID);
                if (r)
                    str = "Replace Done.";
                else
                    str = "Replace Fail.";
            }
            return Json(str);
        }
    }
}
