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
            return View(repo.GetAll());
        }

        public ActionResult Create(int? contractID)
        {
            Site e = repo.InitWithDefaultValue();
            return View(e);
        }

        [HttpPost]
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

        public ActionResult Edit(int id)
        {
            Site e = repo.Get(id);
            e.NewGeoFullName = e.GeoFullName;
            return View(e);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, IEnumerable<HttpPostedFileBase> files, List<int> DeletePhotoList, string[] noteList, List<SDP> siteDetailFiles, List<int> DeleteSiteDetailPhotoList, List<MoveSP> movedL)
        {
            repo.Update(id, UpdateModel, files, DeletePhotoList, noteList, siteDetailFiles, DeleteSiteDetailPhotoList, movedL);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            repo.Delete(id);
            return RedirectToAction("Index");
        }

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
