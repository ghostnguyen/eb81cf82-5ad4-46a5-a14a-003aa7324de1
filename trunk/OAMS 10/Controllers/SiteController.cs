using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAMS.Models;
using Google.GData.Photos;
using System.Dynamic;

namespace OAMS.Controllers
{
    [CustomAuthorize]
    public class SiteController : BaseController<SiteRepository, SiteController>
    {
        //
        // GET: /Site/

        SiteRepository repo = new SiteRepository();

        public ActionResult Index()
        {
            OAMSEntities db = new OAMSEntities();
            //var v = db.Sites
            //    .Include("SiteDetails")
            //    .ToList();            

            var viewFee = QuoteDetailController.IsAuthorize(r => r.ViewFee());

            var v = db.Sites.Select(r => new
            {
                r.ID,
                r.Code,
                GeoFullName = r.Geo3 != null ? r.Geo3.FullName : r.Geo2 != null ? r.Geo2.FullName : r.Geo1 != null ? r.Geo1.FullName : "",
                r.AddressLine1,
                r.AddressLine2,
                Type = r.SiteDetails.Select(r1 => r1.Type).Distinct(),
                Format = r.SiteDetails.Select(r1 => r1.Format).Distinct(),
                Product = r.SiteDetails.SelectMany(r1 => r1.SiteDetailMores).Select(r1 => r1.Product).Select(r1 => r1.Name),
                Client = r.SiteDetails.SelectMany(r1 => r1.SiteDetailMores).Select(r1 => r1.Product).Select(r1 => r1.Client).Select(r1 => r1.Name),
                r.Score,
                Count = r.SiteDetails.SelectMany(r1 => r1.SiteDetailPhotoes).Count(),
                r.LastUpdatedBy,
                r.LastUpdatedDate,
                r.CreatedBy,
                r.CreatedDate,
                QuoteDetailFees = r.QuoteDetails.SelectMany(r1 => r1.QuoteDetailFees).Where(r1 => r1.Months == 12).Select(r1 => r1.MediaRate),
            }).ToList()
            .Select(r => new
            {
                r.ID,
                r.Code,
                r.GeoFullName,
                r.AddressLine1,
                r.AddressLine2,
                Type = string.Join(",", r.Type.ToArray()),
                Format = string.Join(",", r.Format.ToArray()),
                Product = string.Join(",", r.Product.ToArray()),
                Client = string.Join(",", r.Client.ToArray()),
                r.Score,
                r.Count,
                r.LastUpdatedBy,
                r.LastUpdatedDate,
                r.CreatedBy,
                r.CreatedDate,
                Rate = viewFee ? string.Join(" | ", r.QuoteDetailFees.Where(r1 => r1.HasValue).Select(r1 => r1.Value.ToString("C"))) : ""
            }.ToExpando())
            .ToList();
            ;
           
            //return View("Index" + AppSetting.Realm, v);
            return View("Index", v);
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

        public ActionResult MovePhoto(int from, int to)
        {
            repo.MovePhoto(from, to);
            return View("Create");
        }

        public ActionResult UpdateTakenDatePhoto()
        {
            repo.UpdateTakenDatePhoto();
            return View("Create");
        }

        public ActionResult ShowMap(int id)
        {
            var v = Repo.Get(id);
            return View(v);
        }
    }
}
