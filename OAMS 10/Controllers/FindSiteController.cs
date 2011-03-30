﻿using System;
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

        [HttpPost]
        public JsonResult FindJson(FindSite e)
        {
            var siteDetailRepo = new SiteDetailRepository();

            //List<SiteDetail> l = siteDetailRepo.DB.SiteDetails.Include("Site").Include("SiteDetailMore").ToList()

            List<SiteDetail> l = siteDetailRepo.DB.SiteDetails
                .Where(r =>
                    e.StyleList.Contains(r.Type)
                    && (string.IsNullOrEmpty(e.Format) || r.Format == e.Format)
                    && (!e.ViewingDistance.HasValue || r.Site.ViewingDistance == e.ViewingDistance)
                    && (!e.InstallationPosition2.HasValue || r.Site.InstallationPosition2 == e.InstallationPosition2) // Angle to Road
                    && (!e.RoadType2.HasValue || r.Site.RoadType2 == e.RoadType2) //Traffic
                    && (string.IsNullOrEmpty(e.Geo1FullName) || (r.Site.Geo1 != null && r.Site.Geo1.FullName == e.Geo1FullName))
                    ).ToList()
                .Where(r =>
                    (e.ContractorList == null || e.ContractorList.Contains(r.Site.ContractorID))
                    && (e.ClientList == null || e.ClientList.Intersect(r.SiteDetailMores.Select(r1 => r1.Product == null ? 0 : r1.Product.ClientID.ToInt())).Count() > 0)
                    && (e.ProductIDList == null || e.ProductIDList.Contains(r.ProductID.HasValue ? r.ProductID.Value : 0))
                    && (e.InstallationPosition1MarkList == null || e.InstallationPosition1MarkList.Contains(r.Site.InstallationPosition1.HasValue ? r.Site.InstallationPosition1.Value : 0))
                    && (e.CatList == null
                        || (r.Product != null
                            && (e.CatList.Contains(r.Product.CategoryID1.ToString()) || e.CatList.Contains(r.Product.CategoryID2.ToString()) || e.CatList.Contains(r.Product.CategoryID3.ToString()))
                            )
                        )
                    && (r.Site.Score.ToInt() >= e.ScoreFrom.ToInt() && r.Site.Score.ToInt() <= e.ScoreTo.ToInt())
                    && ((string.IsNullOrEmpty(e.Geo1FullName) && e.Geo2List == null)
                        || (e.Geo2List != null && (e.Geo2List.FirstOrDefault() == null || (r.Site.Geo2 != null && e.Geo2List.Contains(r.Site.Geo2.FullName)))))
                    && (!e.IsWithinCircle || Helper.DistanceBetweenPoints(r.Site.Lat, r.Site.Lng, e.Lat, e.Long) <= e.Distance)
                    ).ToList();

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
                Type = string.IsNullOrEmpty(r.Type) ? "" : codeMasterRepo.GetNote(cmt.Type, r.Type),
                CodeType = r.Type,
                r.Site.GeoFullName,
                Address = r.Site.AddressLine1 + " " + r.Site.AddressLine2,
                Orientation = r.Width >= r.Height ? "Horizontal" : "Vertical",
                Size = string.Format("{0}m x {1}m", r.Height.ToString(), r.Width.ToString()),
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

        public ActionResult Find4Quote(int campaignID = 0)
        {
            FindSite e = new FindSite();
            e.From = DateTime.Now.Date;
            e.CampaignID = campaignID;
            return View(e);
        }

        [HttpPost]
        public JsonResult FindJson4Contract(FindSite e, int contractID)
        {
            var siteDetailRepo = new SiteDetailRepository();

            var l = siteDetailRepo.DB.SiteDetails.Include("Site").ToList()
                .Where(r =>
                    e.StyleList.Contains(r.Type)
                && (e.ContractorList == null || e.ContractorList.Contains(r.Site.ContractorID.ToInt()))
                && (e.ClientList == null || e.ClientList.Intersect(r.SiteDetailMores.Select(r1 => r1.Product == null ? 0 : r1.Product.ClientID.ToInt())).Count() > 0)
                && (e.ProductIDList == null || e.ProductIDList.Contains(r.ProductID.HasValue ? r.ProductID.Value : 0))
                && (e.CatList == null
                    || (r.Product != null
                        && (e.CatList.Contains(r.Product.CategoryID1.ToString()) || e.CatList.Contains(r.Product.CategoryID2.ToString()) || e.CatList.Contains(r.Product.CategoryID3.ToString()))
                        )
                    )
                && (string.IsNullOrEmpty(e.Format) || r.Format == e.Format)
                && (!e.RoadType2.HasValue || r.Site.RoadType2 == e.RoadType2) //Traffic
                && (!e.ViewingDistance.HasValue || r.Site.ViewingDistance == e.ViewingDistance)
                && (!e.InstallationPosition2.HasValue || r.Site.InstallationPosition2 == e.InstallationPosition2) // Angle to Road
                && (string.IsNullOrEmpty(e.ViewingSpeed) || r.Site.ViewingSpeed == e.ViewingSpeed.ToInt())
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
                r.SiteID,
                r.Name,
                r.Site.Lat,
                r.Site.Lng,
                AddressLine1 = r.Site.AddressLine1 ?? "",
                AddressLine2 = r.Site.AddressLine2 ?? "",
                Code = r.Site.Code ?? "",
                r.Format,
                Type = string.IsNullOrEmpty(r.Type) ? "" : codeMasterRepo.GetNote(cmt.Type, r.Type),
                CodeType = r.Type,
                r.Site.GeoFullName,
                Address = r.Site.AddressLine1 + " " + r.Site.AddressLine2,
                Orientation = r.Width >= r.Height ? "Horizontal" : "Vertical",
                Size = string.Format("{0}m x {1}m", r.Height.ToString(), r.Width.ToString()),
                Lighting = r.Site.FrontlitNumerOfLamps > 0 ? "Fontlit" : "Backlit",
                Contractor = r.Site.Contractor != null ? r.Site.Contractor.Name : "",
                CurrentProduct = r.Product == null ? "" : (r.Product.Name ?? ""),
                CurrentClient = r.Product == null ? "" : (r.Product.Client == null ? "" : (r.Product.Client.Name ?? "")),
                r.Site.Score,
                AlbumID = string.IsNullOrEmpty(r.Site.AlbumUrl) ? "" : r.Site.AlbumUrl.Split('/')[9].Split('?')[0],
                AuthID = string.IsNullOrEmpty(r.Site.AlbumUrl) ? "" : r.Site.AlbumUrl.Split('?')[1].Split('=')[1],
                Added = r.Site.ContractDetails.Where(r1 => r1.ContractID == contractID && r1.SiteDetailName == r.Name).Count() > 0 ? true : false,

                //Add = false,    
            }));
        }

        [HttpPost]
        public JsonResult FindJson4Quote(FindSite e, int quoteID)
        {
            var siteDetailRepo = new SiteDetailRepository();

            var l = siteDetailRepo.DB.SiteDetails.Include("Site").ToList()
                .Where(r =>
                    e.StyleList.Contains(r.Type)
                && (e.ContractorList == null || e.ContractorList.Contains(r.Site.ContractorID.ToInt()))
                && (e.ClientList == null || e.ClientList.Intersect(r.SiteDetailMores.Select(r1 => r1.Product == null ? 0 : r1.Product.ClientID.ToInt())).Count() > 0)
                && (e.ProductIDList == null || e.ProductIDList.Contains(r.ProductID.HasValue ? r.ProductID.Value : 0))
                && (e.CatList == null
                    || (r.Product != null
                        && (e.CatList.Contains(r.Product.CategoryID1.ToString()) || e.CatList.Contains(r.Product.CategoryID2.ToString()) || e.CatList.Contains(r.Product.CategoryID3.ToString()))
                        )
                    )
                && (string.IsNullOrEmpty(e.Format) || r.Format == e.Format)
                
                && (!e.RoadType2.HasValue || r.Site.RoadType2 == e.RoadType2) //Traffic
                && (!e.ViewingDistance.HasValue || r.Site.ViewingDistance == e.ViewingDistance)
                && (!e.InstallationPosition2.HasValue || r.Site.InstallationPosition2 == e.InstallationPosition2) // Angle to Road
                && (string.IsNullOrEmpty(e.ViewingSpeed) || r.Site.ViewingSpeed == e.ViewingSpeed.ToInt())
                
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
                r.SiteID,
                r.Name,
                r.Site.Lat,
                r.Site.Lng,
                AddressLine1 = r.Site.AddressLine1 ?? "",
                AddressLine2 = r.Site.AddressLine2 ?? "",
                Code = r.Site.Code ?? "",
                r.Format,
                Type = string.IsNullOrEmpty(r.Type) ? "" : codeMasterRepo.GetNote(cmt.Type, r.Type),
                CodeType = r.Type,
                r.Site.GeoFullName,
                Address = r.Site.AddressLine1 + " " + r.Site.AddressLine2,
                Orientation = r.Width >= r.Height ? "Horizontal" : "Vertical",
                Size = string.Format("{0}m x {1}m", r.Height.ToString(), r.Width.ToString()),
                Lighting = r.Site.FrontlitNumerOfLamps > 0 ? "Fontlit" : "Backlit",
                Contractor = r.Site.Contractor != null ? r.Site.Contractor.Name : "",
                CurrentProduct = r.Product == null ? "" : (r.Product.Name ?? ""),
                CurrentClient = r.Product == null ? "" : (r.Product.Client == null ? "" : (r.Product.Client.Name ?? "")),
                r.Site.Score,
                AlbumID = string.IsNullOrEmpty(r.Site.AlbumUrl) ? "" : r.Site.AlbumUrl.Split('/')[9].Split('?')[0],
                AuthID = string.IsNullOrEmpty(r.Site.AlbumUrl) ? "" : r.Site.AlbumUrl.Split('?')[1].Split('=')[1],
                Added = r.Site.QuoteDetails.Where(r1 => r1.QuoteID == quoteID && r1.SiteDetailName == r.Name).Count() > 0 ? true : false,
            }));
        }
    }
}


