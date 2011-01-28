using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAMS.Models;

namespace OAMS.Controllers
{
    [CustomAuthorize]
    public class FindSiteController : Controller
    {
        //
        // GET: /FindSite/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Find(int campaignID = 0)
        {
            FindSite e = new FindSite();
            e.From = DateTime.Now.Date;
            e.CampaignID = campaignID;
            return View(e);
        }


        //[HttpPost]
        //public ActionResult Find(FindSite e)
        //{
        //    e.Results = SiteRepository.Repo.GetAll().ToList();

        //    return View(e);
        //}

        [HttpPost]
        public JsonResult FindJson(FindSite e)
        {
            //SiteRepository siteRepo = new SiteRepository();

            //List<Site> l = siteRepo.GetAll().ToList()
            //    .Where(r =>
            //        e.StyleList.Contains(r.Type)
            //    && (e.ContractorList == null || e.ContractorList.Contains(r.ContractorID.ToInt()))
            //            && (e.ClientList == null || e.ClientList.Contains(r.CurrentClientID.ToInt()))
            //    //&& (e.ClientList == null || e.ClientList.Join(r.SiteDetails.Select(r1 => r1.ClientID), r1 => r1, r1 => r1.ToInt(), (r1, r2) => r1).Count() > 0)
            //    && (e.CatList == null || e.CatList.Contains(r.CategoryID1.ToString()) || e.CatList.Contains(r.CategoryID2.ToString()) || e.CatList.Contains(r.CategoryID3.ToString()))
            //    && (string.IsNullOrEmpty(e.Format) || r.Format == e.Format)
            //    && (string.IsNullOrEmpty(e.RoadType1) || r.RoadType1 == e.RoadType1.ToInt())
            //    && (string.IsNullOrEmpty(e.RoadType2) || r.RoadType2 == e.RoadType2.ToInt())
            //    && (string.IsNullOrEmpty(e.InstallationPosition1) || r.InstallationPosition1 == e.InstallationPosition1.ToInt())
            //    && (string.IsNullOrEmpty(e.InstallationPosition2) || r.InstallationPosition2 == e.InstallationPosition2.ToInt())
            //    && (string.IsNullOrEmpty(e.ViewingDistance) || r.ViewingDistance == e.ViewingDistance.ToInt())
            //    && (string.IsNullOrEmpty(e.ViewingSpeed) || r.ViewingSpeed == e.ViewingSpeed.ToInt())
            //    && (string.IsNullOrEmpty(e.Height) || r.Height == e.Height.ToInt())
            //    && (string.IsNullOrEmpty(e.DirectionalTrafficPublicTransport) || r.DirectionalTrafficPublicTransport == e.DirectionalTrafficPublicTransport.ToInt())
            //    && (string.IsNullOrEmpty(e.ShopSignsBillboards) || r.ShopSignsBillboards == e.ShopSignsBillboards.ToInt())
            //    && (string.IsNullOrEmpty(e.FlagsTemporaryBannersPromotionalItems) || r.FlagsTemporaryBannersPromotionalItems == e.FlagsTemporaryBannersPromotionalItems.ToInt())
            //    && (string.IsNullOrEmpty(e.CompetitiveProductSigns) || r.CompetitiveProductSigns == e.CompetitiveProductSigns.ToInt())
            //    && (e.ProductList == null || e.ProductList.Match(r.CurrentProduct))
            //    && (string.IsNullOrEmpty(e.Geo1FullName) || (r.Geo1 != null && r.Geo1.FullName == e.Geo1FullName))
            //    && ((string.IsNullOrEmpty(e.Geo1FullName) && e.Geo2List == null)
            //        || (e.Geo2List != null && (e.Geo2List.FirstOrDefault() == null || (r.Geo2 != null && e.Geo2List.Contains(r.Geo2.FullName)))))

            //        ).ToList()
            //    .Where(r => !e.IsWithinCircle || Helper.DistanceBetweenPoints(r.Lat, r.Lng, e.Lat, e.Long) <= e.Distance)
            //    .ToList();

            var siteDetailRepo = new SiteDetailRepository();


            List<SiteDetail> l = siteDetailRepo.DB.SiteDetails.Include("Site").ToList()
                .Where(r =>
                    e.StyleList.Contains(r.Site.Type)
                && (e.ContractorList == null || e.ContractorList.Contains(r.Site.ContractorID.ToInt()))
                && (e.ClientList == null || e.ClientList.Contains(r.Product == null ? 0 : r.Product.ClientID.ToInt()))
                        //&& (e.CatList == null || e.CatList.Contains(r.CategoryID1.ToString()) || e.CatList.Contains(r.CategoryID2.ToString()) || e.CatList.Contains(r.CategoryID3.ToString()))
                && (e.CatList == null
                    || (r.Product != null
                        && (e.CatList.Contains(r.Product.CategoryID1.ToString()) || e.CatList.Contains(r.Product.CategoryID2.ToString()) || e.CatList.Contains(r.Product.CategoryID3.ToString()))
                        )
                    )
                && (string.IsNullOrEmpty(e.Format) || r.Format == e.Format)
                && (string.IsNullOrEmpty(e.RoadType1) || r.Site.RoadType1 == e.RoadType1.ToInt())
                && (string.IsNullOrEmpty(e.RoadType2) || r.Site.RoadType2 == e.RoadType2.ToInt())
                && (string.IsNullOrEmpty(e.InstallationPosition1) || r.Site.InstallationPosition1 == e.InstallationPosition1.ToInt())
                && (string.IsNullOrEmpty(e.InstallationPosition2) || r.Site.InstallationPosition2 == e.InstallationPosition2.ToInt())
                && (string.IsNullOrEmpty(e.ViewingDistance) || r.Site.ViewingDistance == e.ViewingDistance.ToInt())
                && (string.IsNullOrEmpty(e.ViewingSpeed) || r.Site.ViewingSpeed == e.ViewingSpeed.ToInt())
                && (string.IsNullOrEmpty(e.Height) || r.Site.Height == e.Height.ToInt())
                && (string.IsNullOrEmpty(e.DirectionalTrafficPublicTransport) || r.Site.DirectionalTrafficPublicTransport == e.DirectionalTrafficPublicTransport.ToInt())
                && (string.IsNullOrEmpty(e.ShopSignsBillboards) || r.Site.ShopSignsBillboards == e.ShopSignsBillboards.ToInt())
                && (string.IsNullOrEmpty(e.FlagsTemporaryBannersPromotionalItems) || r.Site.FlagsTemporaryBannersPromotionalItems == e.FlagsTemporaryBannersPromotionalItems.ToInt())
                && (string.IsNullOrEmpty(e.CompetitiveProductSigns) || r.Site.CompetitiveProductSigns == e.CompetitiveProductSigns.ToInt())
                        //&& (e.ProductList == null || e.ProductList.Match(r.Product))
                && (e.ProductList == null || e.ProductList.Match(r.Product == null ? "" : r.Product.Name))
                && (r.Site.Score.ToInt() >= e.ScoreFrom.ToInt() && r.Site.Score.ToInt() <= e.ScoreTo.ToInt())
                && (string.IsNullOrEmpty(e.Geo1FullName) || (r.Site.Geo1 != null && r.Site.Geo1.FullName == e.Geo1FullName))
                && ((string.IsNullOrEmpty(e.Geo1FullName) && e.Geo2List == null)
                    || (e.Geo2List != null && (e.Geo2List.FirstOrDefault() == null || (r.Site.Geo2 != null && e.Geo2List.Contains(r.Site.Geo2.FullName)))))

                    ).ToList()
                .Where(r => !e.IsWithinCircle || Helper.DistanceBetweenPoints(r.Site.Lat, r.Site.Lng, e.Lat, e.Long) <= e.Distance)
                .ToList();

            CodeMasterType cmt = new CodeMasterType();

            CodeMasterRepository codeMasterRepo = new CodeMasterRepository();

            return Json(l.Distinct().Select(r => new
            {
                r.Site.ID,
                r.Site.Lat,
                r.Site.Lng,
                AddressLine1 = r.Site.AddressLine1 ?? "",
                AddressLine2 = r.Site.AddressLine2 ?? "",

                Code = r.Site.Code ?? "",
                r.Format,
                Type = string.IsNullOrEmpty(r.Site.Type) ? "" : codeMasterRepo.GetNote(cmt.Type, r.Site.Type),
                CodeType = r.Site.Type,
                r.Site.GeoFullName,
                Address = r.Site.AddressLine1 + " " + r.Site.AddressLine2,
                Orientation = r.Site.Width >= r.Site.Height ? "Horizontal" : "Vertical",
                Size = string.Format("{0}m x {1}m", r.Site.Height.ToString(), r.Site.Width.ToString()),
                Lighting = r.Site.FrontlitNumerOfLamps > 0 ? "Fontlit" : "Backlit",
                Contractor = r.Site.Contractor != null ? r.Site.Contractor.Name : "",
                CurrentProduct = r.Product == null ? "" : (r.Product.Name ?? ""),
                CurrentClient = r.Product == null ? "" : (r.Product.Client == null ? "" : (r.Product.Client.Name ?? "")),
                r.Site.Score,
                Rating = r.Site.Score.ToRating(),
                AlbumID = string.IsNullOrEmpty(r.Site.AlbumUrl) ? "" : r.Site.AlbumUrl.Split('/')[9].Split('?')[0],
                AuthID = string.IsNullOrEmpty(r.Site.AlbumUrl) ? "" : r.Site.AlbumUrl.Split('?')[1].Split('=')[1],
                CategoryLevel1 = r.Product == null ? "" : (r.Product.Category1 != null ? r.Product.Category1.Name : ""),
                CategoryLevel2 = r.Product == null ? "" : (r.Product.Category2 != null ? r.Product.Category2.Name : ""),
                Geo2 = r.Site.Geo2 != null ? r.Site.Geo2.Name : "",
                Geo3 = r.Site.Geo3 != null ? r.Site.Geo3.Name : ""
            }));
        }

        public ActionResult Find4Contract(int campaignID = 0)
        {
            FindSite e = new FindSite();
            e.From = DateTime.Now.Date;
            e.CampaignID = campaignID;
            return View(e);
        }

        [HttpPost]
        public JsonResult FindJson4Contract(FindSite e, int contractID)
        {
            //SiteRepository siteRepo = new SiteRepository();

            //List<Site> l = siteRepo.GetAll().ToList()
            //    .Where(r =>
            //        e.StyleList.Contains(r.Type)
            //    && (e.ContractorList == null || e.ContractorList.Contains(r.ContractorID.ToInt()))
            //    && (e.ClientList == null || e.ClientList.Contains(r.CurrentClientID.ToInt()))
            //    && (string.IsNullOrEmpty(e.Format) || r.Format == e.Format)
            //    && (string.IsNullOrEmpty(e.RoadType1) || r.RoadType1 == e.RoadType1.ToInt())
            //    && (string.IsNullOrEmpty(e.RoadType2) || r.RoadType2 == e.RoadType2.ToInt())
            //    && (string.IsNullOrEmpty(e.InstallationPosition1) || r.InstallationPosition1 == e.InstallationPosition1.ToInt())
            //    && (string.IsNullOrEmpty(e.InstallationPosition2) || r.InstallationPosition2 == e.InstallationPosition2.ToInt())
            //    && (string.IsNullOrEmpty(e.ViewingDistance) || r.ViewingDistance == e.ViewingDistance.ToInt())
            //    && (string.IsNullOrEmpty(e.ViewingSpeed) || r.ViewingSpeed == e.ViewingSpeed.ToInt())
            //    && (string.IsNullOrEmpty(e.Height) || r.Height == e.Height.ToInt())
            //    && (string.IsNullOrEmpty(e.DirectionalTrafficPublicTransport) || r.DirectionalTrafficPublicTransport == e.DirectionalTrafficPublicTransport.ToInt())
            //    && (string.IsNullOrEmpty(e.ShopSignsBillboards) || r.ShopSignsBillboards == e.ShopSignsBillboards.ToInt())
            //    && (string.IsNullOrEmpty(e.FlagsTemporaryBannersPromotionalItems) || r.FlagsTemporaryBannersPromotionalItems == e.FlagsTemporaryBannersPromotionalItems.ToInt())
            //    && (string.IsNullOrEmpty(e.CompetitiveProductSigns) || r.CompetitiveProductSigns == e.CompetitiveProductSigns.ToInt())
            //    && (string.IsNullOrEmpty(e.Geo1FullName) || (r.Geo1 != null && r.Geo1.FullName == e.Geo1FullName))
            //    && ((string.IsNullOrEmpty(e.Geo1FullName) && e.Geo2List == null)
            //        || (e.Geo2List != null && (e.Geo2List.FirstOrDefault() == null || (r.Geo2 != null && e.Geo2List.Contains(r.Geo2.FullName)))))

            //        ).ToList()
            //    .Where(r => !e.IsWithinCircle || Helper.DistanceBetweenPoints(r.Lat, r.Lng, e.Lat, e.Long) <= e.Distance)
            //    .ToList();

            var siteDetailRepo = new SiteDetailRepository();

            var l = siteDetailRepo.DB.SiteDetails.Include("Site").ToList()
                .Where(r =>
                    e.StyleList.Contains(r.Site.Type)
                && (e.ContractorList == null || e.ContractorList.Contains(r.Site.ContractorID.ToInt()))
                //&& (e.ClientList == null || e.ClientList.Contains(r.ClientID.ToInt()))
                && (e.ClientList == null || e.ClientList.Contains(r.Product == null ? 0 : r.Product.ClientID.ToInt()))

                //&& (e.CatList == null || e.CatList.Contains(r.CategoryID1.ToString()) || e.CatList.Contains(r.CategoryID2.ToString()) || e.CatList.Contains(r.CategoryID3.ToString()))
                && (e.CatList == null
                    || (r.Product != null
                        && (e.CatList.Contains(r.Product.CategoryID1.ToString()) || e.CatList.Contains(r.Product.CategoryID2.ToString()) || e.CatList.Contains(r.Product.CategoryID3.ToString()))
                        )
                    )
                && (string.IsNullOrEmpty(e.Format) || r.Format == e.Format)
                && (string.IsNullOrEmpty(e.RoadType1) || r.Site.RoadType1 == e.RoadType1.ToInt())
                && (string.IsNullOrEmpty(e.RoadType2) || r.Site.RoadType2 == e.RoadType2.ToInt())
                && (string.IsNullOrEmpty(e.InstallationPosition1) || r.Site.InstallationPosition1 == e.InstallationPosition1.ToInt())
                && (string.IsNullOrEmpty(e.InstallationPosition2) || r.Site.InstallationPosition2 == e.InstallationPosition2.ToInt())
                && (string.IsNullOrEmpty(e.ViewingDistance) || r.Site.ViewingDistance == e.ViewingDistance.ToInt())
                && (string.IsNullOrEmpty(e.ViewingSpeed) || r.Site.ViewingSpeed == e.ViewingSpeed.ToInt())
                && (string.IsNullOrEmpty(e.Height) || r.Site.Height == e.Height.ToInt())
                && (string.IsNullOrEmpty(e.DirectionalTrafficPublicTransport) || r.Site.DirectionalTrafficPublicTransport == e.DirectionalTrafficPublicTransport.ToInt())
                && (string.IsNullOrEmpty(e.ShopSignsBillboards) || r.Site.ShopSignsBillboards == e.ShopSignsBillboards.ToInt())
                && (string.IsNullOrEmpty(e.FlagsTemporaryBannersPromotionalItems) || r.Site.FlagsTemporaryBannersPromotionalItems == e.FlagsTemporaryBannersPromotionalItems.ToInt())
                && (string.IsNullOrEmpty(e.CompetitiveProductSigns) || r.Site.CompetitiveProductSigns == e.CompetitiveProductSigns.ToInt())
                //&& (e.ProductList == null || e.ProductList.Match(r.Product))
                && (e.ProductList == null || e.ProductList.Match(r.Product == null ? "" : r.Product.Name))
                && (string.IsNullOrEmpty(e.Geo1FullName) || (r.Site.Geo1 != null && r.Site.Geo1.FullName == e.Geo1FullName))
                && ((string.IsNullOrEmpty(e.Geo1FullName) && e.Geo2List == null)
                    || (e.Geo2List != null && (e.Geo2List.FirstOrDefault() == null || (r.Site.Geo2 != null && e.Geo2List.Contains(r.Site.Geo2.FullName)))))

                    ).ToList()
                .Where(r => !e.IsWithinCircle || Helper.DistanceBetweenPoints(r.Site.Lat, r.Site.Lng, e.Lat, e.Long) <= e.Distance)
                .ToList();

            CodeMasterType cmt = new CodeMasterType();

            CodeMasterRepository codeMasterRepo = new CodeMasterRepository();

            return Json(l.Select(r => new
            {
                r.ID,
                r.Site.Lat,
                r.Site.Lng,
                AddressLine1 = r.Site.AddressLine1 ?? "",
                AddressLine2 = r.Site.AddressLine2 ?? "",

                Code = r.Site.Code ?? "",
                r.Format,
                Type = string.IsNullOrEmpty(r.Site.Type) ? "" : codeMasterRepo.GetNote(cmt.Type, r.Site.Type),
                CodeType = r.Site.Type,
                r.Site.GeoFullName,
                Address = r.Site.AddressLine1 + " " + r.Site.AddressLine2,
                Orientation = r.Site.Width >= r.Site.Height ? "Horizontal" : "Vertical",
                Size = string.Format("{0}m x {1}m", r.Site.Height.ToString(), r.Site.Width.ToString()),
                Lighting = r.Site.FrontlitNumerOfLamps > 0 ? "Fontlit" : "Backlit",
                Contractor = r.Site.Contractor != null ? r.Site.Contractor.Name : "",
                //CurrentProduct = r.Product ?? "",
                //CurrentClient = r.CurrentClientName ?? "",
                CurrentProduct = r.Product == null ? "" : (r.Product.Name ?? ""),
                CurrentClient = r.Product == null ? "" : (r.Product.Client == null ? "" : (r.Product.Client.Name ?? "")),
                r.Site.Score,
                AlbumID = string.IsNullOrEmpty(r.Site.AlbumUrl) ? "" : r.Site.AlbumUrl.Split('/')[9].Split('?')[0],
                AuthID = string.IsNullOrEmpty(r.Site.AlbumUrl) ? "" : r.Site.AlbumUrl.Split('?')[1].Split('=')[1],
                //Added = r.Site.ContractDetails.Where(r1 => r1.ContractID == contractID).Count() > 0 ? true : false
                Add = false
            }));
        }
    }
}


