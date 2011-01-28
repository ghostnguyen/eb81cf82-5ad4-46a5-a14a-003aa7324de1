using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAMS.Models;
using Google.GData.Photos;

namespace OAMS.Controllers
{
    [CustomAuthorize]
    public class SiteController : AsyncController
    {
        //
        // GET: /Site/

        SiteRepository repo = new SiteRepository();

        public ActionResult Index()
        {
            repo.FixDB();

            return View(repo.GetAll());
        }

        //
        // GET: /Site/Details/5

        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //
        // GET: /Site/Create

        public ActionResult Create(int? contractID)
        {
            Site e = repo.InitWithDefaultValue();
            return View(e);
        }

        //
        // POST: /Site/Create



        [HttpPost]
        //public ActionResult Create(IEnumerable<HttpPostedFileBase> files)
        public ActionResult Create(int? contractID, IEnumerable<HttpPostedFileBase> files, string[] noteList, bool? IsFirstSave)
        {
            var site = repo.Add(UpdateModel, files, noteList);

            if (IsFirstSave.HasValue && IsFirstSave.Value)
            {
                return RedirectToAction("Edit", new { id = site.ID });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Site/Edit/5

        public ActionResult Edit(int id)
        {
            Site e = repo.Get(id);
            e.NewGeoFullName = e.GeoFullName;
            //e.NewCategoryFullName = e.CategoryFullName;
            return View(e);
        }

        //
        // POST: /Site/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, IEnumerable<HttpPostedFileBase> files, List<int> DeletePhotoList, string[] noteList)
        {
            // TODO: Add update logic here
            repo.Update(id, UpdateModel, files, DeletePhotoList, noteList);

            //return View(e);
            return RedirectToAction("Index");
        }

        //public ActionResult EditCompleted(string[] headlines)
        //public ActionResult EditCompleted()
        //{
        //    return RedirectToAction("Index");
        //    //return View("News", new ViewStringModel
        //    //{
        //    //    NewsHeadlines = headlines
        //    //});
        //}

        //
        // GET: /Site/Delete/5

        public ActionResult Delete(int id)
        {
            //repo.DeletePhoto(id);
            repo.Delete(id);
            return RedirectToAction("Index");

            //return View();
        }

        //
        // POST: /Site/Delete/5

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    //repo.DeletePhoto(id);
        //    repo.Delete(id);
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public JsonResult JsonList()
        {
            OAMSEntities db = new OAMSEntities();
            var result = db.Sites.Where(r => r.Lng > 0 && r.Lat > 0)
                .Select(r => new { r.Lat, r.Lng, r.Code, Note = r.Code })
                .ToList();

            return Json(result);
        }



    }
}
