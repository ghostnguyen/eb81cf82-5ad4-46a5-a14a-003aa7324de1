﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAMS.Models;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Reflection;
using Microsoft.CSharp.RuntimeBinder;

namespace OAMS.Controllers
{
    [CustomAuthorize]
    public class RptController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult _101()
        {
            return View();
        }

        [HttpPost]
        public ActionResult _101(Rpt101 e)
        {
            OAMSEntities db = new OAMSEntities();

            e.List = db.SiteDetails.Where(r => true
                && (r.Site.Geo1 != null && r.Site.Geo1.FullName == e.Geo1FullName)
                ).GroupBy(r => new { Geo2 = r.Site.Geo2.Name, r.Type })
                .Select(r => new Rpt101Row
                {
                    Geo2 = r.Key.Geo2,
                    Type = r.Key.Type,
                    Count = r.Count()
                }).ToList();

            return View(e);
        }

        public ActionResult _102(Rpt102 e)
        {
            if (e == null) e = new Rpt102();
            OAMSEntities db = new OAMSEntities();

            var v = db.SiteDetailMores
                .Where(r =>
                    r.Product != null
                    && r.Product.Category1 != null
                    && !string.IsNullOrEmpty(r.SiteDetail.Type)
                    && (string.IsNullOrEmpty(e.Geo1FullName) || (r.SiteDetail.Site.Geo1 != null && r.SiteDetail.Site.Geo1.FullName == e.Geo1FullName))
                    && (string.IsNullOrEmpty(e.Cat1FullName)
                        || r.Product.Category1.FullName == e.Cat1FullName))
            .GroupBy(r => new { r.SiteDetail.Type, Cat1 = r.Product.Category1.Name, Cat2 = r.Product.Category2.Name })
            .Select(r => new Rpt102Row
                            {
                                Type = r.Key.Type,
                                Cat1 = r.Key.Cat1,
                                Cat2 = r.Key.Cat2,
                                Count = r.Count()
                            }).ToList();

            e.List = v;

            return View(e);
        }

        public ActionResult _103(Rpt103 e)
        {
            if (e == null) e = new Rpt103();

            OAMSEntities db = new OAMSEntities();

            e.List = db.SiteDetailMores.Where(r => true
                && (string.IsNullOrEmpty(e.Geo1FullName) || (r.SiteDetail.Site.Geo1 != null && r.SiteDetail.Site.Geo1.FullName == e.Geo1FullName))
                && !string.IsNullOrEmpty(r.SiteDetail.Type)
                ).GroupBy(r => new
                {
                    Client = r.Product.Client.Name,
                    Product = r.Product.Name,
                    Type = r.SiteDetail.Type,
                })
                .Select(r => new Rpt103Row
                {
                    Type = r.Key.Type,
                    Client = r.Key.Client,
                    Product = r.Key.Product,
                    Count = r.Count(),
                }).ToList();

            return View(e);
        }

        public ActionResult _104(Rpt104 e)
        {
            if (e == null) e = new Rpt104();

            OAMSEntities db = new OAMSEntities();

            e.List = db.SiteDetailMores.Where(r => true
                && (string.IsNullOrEmpty(e.Geo1FullName) || (r.SiteDetail.Site.Geo1 != null && r.SiteDetail.Site.Geo1.FullName == e.Geo1FullName))
                && !string.IsNullOrEmpty(r.SiteDetail.Type)
                ).GroupBy(r => new
                {
                    Contractor = r.SiteDetail.Site.Contractor.Name,
                    Product = r.Product.Name,
                    Type = r.SiteDetail.Type,
                })
                .Select(r => new Rpt104Row
                {
                    Type = r.Key.Type,
                    Contractor = r.Key.Contractor,
                    Product = r.Key.Product,
                    Count = r.Count(),
                }).ToList();

            return View(e);
        }

        public ActionResult _105()
        {
            return View();
        }

        [HttpPost]
        public ActionResult _105(Rpt105 e)
        {
            OAMSEntities db = new OAMSEntities();

            e.List = db.SiteDetails.Where(r => true
                && (r.Site.Geo1 != null && r.Site.Geo1.FullName == e.Geo1FullName)
                && (r.Type == e.Type)
                ).GroupBy(r => r.Site.Geo2.Name)
                .Select(r => new Rpt105.Row
                {
                    Geo2 = r.Key,
                    Count = r.Count(),
                }).ToList();

            if (e.LessThan > 0)
            {
                Func<Rpt105.Row, bool> f = r => r.Count < e.LessThan;

                Rpt105.Row nr = new Rpt105.Row() { Geo2 = "Other" };
                nr.Count = e.List.Where(f).Sum(r => r.Count);

                e.List.RemoveAll(f.ToPredicate());
                e.List.Add(nr);
            }

            return View(e);
        }
    }
}
