using System;
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

        public ActionResult _120()
        {
            return View();
        }

        [HttpPost]
        public ActionResult _120(Rpt120 e)
        {
            bool usingSiteDetailMore = false;

            OAMSEntities db = new OAMSEntities();

            IQueryable<SiteDetailMore> l = db.SiteDetailMores;

            if (!string.IsNullOrEmpty(e.Geo1FullName))
            {
                l = l.Where(r => r.SiteDetail.Site.Geo1 != null && r.SiteDetail.Site.Geo1.FullName == e.Geo1FullName);
            }

            if (!string.IsNullOrEmpty(e.Cat1FullName))
            {
                l = l.Where(r => r.Product != null && r.Product.Category1 != null && r.Product.Category1.FullName == e.Cat1FullName);
                usingSiteDetailMore = true;
            }

            if (!string.IsNullOrEmpty(e.Client))
            {
                l = l.Where(r => r.Product != null && r.Product.Client != null && r.Product.Client.Name == e.Client);
                usingSiteDetailMore = true;
            }

            if (!string.IsNullOrEmpty(e.Type))
            {
                l = l.Where(r => r.SiteDetail.Type == e.Type);
            }

            IQueryable<IGrouping<string, SiteDetail>> q1 = null;
            IQueryable<IGrouping<string, SiteDetailMore>> q2 = null;

            switch (e.GroupBy)
            {
                case "Type":

                    if (usingSiteDetailMore)
                    {
                        q2 = l.GroupBy(r => r.SiteDetail.Type);
                    }
                    else
                    {
                        q1 = l.Select(r => r.SiteDetail).GroupBy(r => r.Type);
                    }

                    break;
                case "Geo2":

                    if (usingSiteDetailMore)
                    {
                        q2 = l.GroupBy(r => r.SiteDetail.Site.Geo2.Name);
                    }
                    else
                    {
                        q1 = l.Select(r => r.SiteDetail).GroupBy(r => r.Site.Geo2.Name);
                    }
                    break;
                case "Product":
                    q2 = l.GroupBy(r => r.Product.Name);

                    break;
                case "Client":
                    q2 = l.GroupBy(r => r.Product.Client.Name);
                    break;

                default:
                    break;
            }

            if (usingSiteDetailMore)
            {
                e.List = q2.Select(r => new Rpt120.Row
                {
                    Note = r.Key,
                    Count = r.Count(),
                }).ToList();
            }
            else
            {
                e.List = q1.Select(r => new Rpt120.Row
                {
                    Note = r.Key,
                    Count = r.Count(),
                }).ToList();
            }

            if (e.LessThan > 0)
            {
                Func<Rpt120.Row, bool> f = r => r.Count < e.LessThan;

                var nr = new Rpt120.Row() { Note = "Other" };
                nr.Count = e.List.Where(f).Sum(r => r.Count);

                e.List.RemoveAll(f.ToPredicate());
                e.List.Add(nr);
            }

            return View(e);
        }

        public ActionResult _130()
        {
            return View();
        }

        [HttpPost]
        public ActionResult _130(string query)
        {
            List<Rpt130> paramsL = new List<Rpt130>();

            OAMSEntities db = new OAMSEntities();

            var whereL = paramsL.Where(r => r.Values != null || r.Values.Count > 0);

            var r1 = db.SiteDetailMores.AsQueryable();
            foreach (var item in whereL)
            {
                //Geo 1, Geo 2, type, format, contractor, category 1, category 2, client, product, 
                switch (item.Name)
                {
                    case "Geo1":
                        r1 = r1.Where(r => r.SiteDetail.Site.Geo1 != null && item.Values.Contains(r.SiteDetail.Site.Geo1.Name));
                        break;
                    case "Geo2":
                        r1 = r1.Where(r => r.SiteDetail.Site.Geo2 != null && item.Values.Contains(r.SiteDetail.Site.Geo2.Name));
                        break;
                    case "Type":
                        r1 = r1.Where(r => item.Values.Contains(r.SiteDetail.Type));
                        break;
                    case "Format":
                        r1 = r1.Where(r => item.Values.Contains(r.SiteDetail.Format));
                        break;
                    case "Contractor":
                        r1 = r1.Where(r => r.SiteDetail.Site.Contractor != null && item.Values.Contains(r.SiteDetail.Site.Contractor.Name));
                        break;
                    case "Category1":
                        r1 = r1.Where(r => r.Product != null && r.Product.Category1 != null && item.Values.Contains(r.Product.Category1.Name));
                        break;
                    case "Category2":
                        r1 = r1.Where(r => r.Product != null && r.Product.Category2 != null && item.Values.Contains(r.Product.Category2.Name));
                        break;
                    case "Client":
                        r1 = r1.Where(r => r.Product != null && r.Product.Client != null && item.Values.Contains(r.Product.Client.Name));
                        break;
                    case "Product":
                        r1 = r1.Where(r => r.Product != null && item.Values.Contains(r.Product.Name));
                        break;
                }
            }

            var r2 = r1.ToList();
            var selectL = paramsL.Where(r => r.IsShow);
            foreach (var item in selectL)
            {
                
            }

            return null;
        }
    }
}

