using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAMS.Models;

namespace OAMS.Controllers
{
    [CustomAuthorize]
    public class CategoryController : Controller
    {
        CategoryRepository repo = new CategoryRepository();

        public ActionResult Index(Guid? parentID = null)
        {
            var v = repo.GetByParentID(parentID).ToList();
            return View(v);
        }

        public ActionResult IndexParent(Guid? parentID = null)
        {
            var v = repo.Get(parentID);
            if (v == null || v.Parent == null)
            {
                return RedirectToAction("Index");
                //return View("Index", repo.GetByParentID());
            }
            else
            {
                return RedirectToAction("Index", new { parentID = v.ParentID });
                //return View("Index", v.Parent.Children);
            }



            //var v = repo.GetByParentID(parentID).ToList();
        }

        //
        // GET: /Category/Create
        public ActionResult Create(Guid? parentID)
        {
            Category e = new Category();
            e.ParentID = parentID;

            return View(e);
        }

        //
        // POST: /Category/Create

        [HttpPost]
        public ActionResult Create(Guid? parentID, FormCollection collection)
        {
            // TODO: Add insert logic here
            Category e = new Category();
            UpdateModel(e);
            repo.Add(e);
            //repo.Save();
            return RedirectToAction("Index", new { parentID = e.ParentID });
        }

        //
        // GET: /Category/Edit/5
        public ActionResult Edit(Guid ID)
        {
            var v = repo.Get(ID);
            return View(v);
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            var v = repo.Get(id);

            UpdateModel(v);

            repo.UpdateFullname(v);

            repo.Save();

            // TODO: Add update logic here

            return RedirectToAction("Index", new { parentID = v.ParentID });
        }

        //
        // GET: /Category/Delete/5

        public ActionResult Delete(Guid id)
        {
            Category e = repo.Get(id);

            Guid? parentID = e.ParentID;
            
            repo.Delete(e);

            repo.Save();

            
            return RedirectToAction("Index", new { parentID = parentID });
        }

        //
        // POST: /Category/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            return RedirectToAction("Index");
        }
    }
}
