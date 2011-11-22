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
                //.Select(r =>
                //{
                //    dynamic a = new ExpandoObject();
                //    a.ID = r.ID;
                //    a.Codea = r.Code;
                //    a.GeoFullName = r.GeoFullName;
                //    a.AddressLine1 = r.AddressLine1;
                //    a.AddressLine2 = r.AddressLine2;
                //    a.Type = string.Join(",", r.Type.ToArray());
                //    a.Format = string.Join(",", r.Format.ToArray());
                //    a.Product = string.Join(",", r.Product.ToArray());
                //    a.Client = string.Join(",", r.Client.ToArray());
                //    a.Score = r.Score;
                //    a.Count = r.Count;
                //    a.LastUpdatedBy = r.LastUpdatedBy;
                //    a.LastUpdatedDate = r.LastUpdatedDate;
                //    a.CreatedBy = r.CreatedBy;
                //    a.CreatedDate = r.CreatedDate;
                //    return (ExpandoObject)a;
                //})
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


            return View(v);
            //return View(repo.GetAll());
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
    }
}
