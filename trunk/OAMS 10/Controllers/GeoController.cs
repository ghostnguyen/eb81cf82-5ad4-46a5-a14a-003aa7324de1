using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAMS.Models;

namespace OAMS.Controllers
{
    [CustomAuthorize]
    public class GeoController : BaseController<GeoRepository>
    {
        public ActionResult Index(Guid? parentID = null)
        {
            var v = Repo.GetByParentID(parentID).ToList();
            return View(v);
        }

        public ActionResult IndexParent(Guid? parentID = null)
        {
            var v = Repo.Get(parentID);
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
        // GET: /Geo/Create
        public ActionResult Create(Guid? parentID)
        {
            Geo e = new Geo();
            e.ParentID = parentID;

            return View(e);
        }

        //
        // POST: /Geo/Create

        [HttpPost]
        public ActionResult Create(Guid? parentID, string name)
        {
            var e = Repo.Add(UpdateModel);

            return RedirectToAction("Index", new { parentID = e.ParentID });
        }

        //
        // GET: /Geo/Edit/5
        [HttpGet]
        public ActionResult Edit(Guid ID)
        {
            var v = Repo.Get(ID);
            return View(v);
        }

        //
        // POST: /Geo/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid id, string name)
        {
            var v = Repo.Update(id, UpdateModel);

            return RedirectToAction("Index", new { parentID = v.ParentID });
        }

        //
        // GET: /Geo/Delete/5

        public ActionResult Delete(Guid id)
        {
            Geo e = Repo.Get(id);

            Guid? parentID = e.ParentID;

            Repo.Delete(e);

            Repo.Save();


            return RedirectToAction("Index", new { parentID = parentID });
        }

        //
        // POST: /Geo/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            return RedirectToAction("Index");
        }
    }
}
