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
using System.Web.Script.Serialization;
using System.Linq.Expressions;

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

        //public ActionResult _130()
        //{
        //    return View();
        //}

        public ActionResult _130(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return View();
            }
            //query=[{%22Name%22:%22Geo1%22,%22Values%22:[%22HCMC%22]},{%22Name%22:%22Type%22,%22Values%22:[%22WMB%22]}]
            //query=[{%22Name%22:%22Type%22,%22Values%22:[%22WMB%22]}]
            //query=[{%22Name%22:%22Geo1%22,%22Values%22:[%22HCMC%22]}]
            //query=[{%22Name%22:%22Geo1%22,%22Values%22:[%22HCMC%22],%22IsShow%22:%22false%22},{%22Name%22:%22Geo2%22},{%22Name%22:%22Type%22,%22IsCount%22:%22true%22}]

            OAMSEntities db = new OAMSEntities();

            List<Rpt130> paramsL = new List<Rpt130>();

            JavaScriptSerializer ser = new JavaScriptSerializer();
            paramsL = ser.Deserialize<List<Rpt130>>(query);

            foreach (var item in paramsL.Where(r => (r.Values == null || r.Values.Count == 0) && r.IsCount).ToList())
            {
                switch (item.Name)
                {
                    case "Type":
                        item.Values = db.CodeMasters.Where(r => r.Type == CodeMasterType.Type).OrderBy(r => r.Order).Select(r => r.Code).ToList();
                        item.Values.Add("");
                        break;
                    case "Format":
                        var format = PropertyName.For<CodeMasterType>(r => r.Format);
                        item.Values = db.CodeMasters.Where(r => r.Type == format).OrderBy(r => r.Order).Select(r => r.Code).ToList();
                        item.Values.Add("");
                        break;
                    default:
                        break;
                }
            }

            foreach (var item in paramsL.Where(r => r.Values != null && r.Values.Count > 0 && r.IsCount).ToList())
            {
                var np = item.Values.Select(r => new Rpt130()
                {
                    Name = item.Name,
                    Values = new List<string>() { r },
                    IsCount = item.IsCount,
                    IsShow = item.IsShow
                })
                .ToList();

                paramsL.InsertRange(paramsL.IndexOf(item), np);
                paramsL.Remove(item);
            }

            for (int i = 0; i < paramsL.Count; i++)
            {
                var item = paramsL[i];
                item.Order = i;
                item.PName = "P_" + i.ToString();

                item.IsShow = item.IsShow ?? true;
            }



            ViewBag.ParamsL = paramsL;



            var r1 = db.SiteDetailMores.Select(r => r);
            //List<SiteDetailMore> r1 = db.SiteDetailMores.ToList();

            var whereL = paramsL.Where(r => r.Values != null && r.Values.Count > 0 && !r.IsCount);

            foreach (var item in whereL)
            {

                //Use local variable, why? see the link
                //http://stackoverflow.com/questions/6096692/filter-iqueryable-in-a-loop-with-multiple-where-statements
                var values = item.Values;

                switch (item.Name)
                {
                    case "Geo1":
                        r1 = r1.Where(r => r.SiteDetail.Site.Geo1 != null && values.Contains(r.SiteDetail.Site.Geo1.Name));
                        break;
                    case "Geo2":
                        r1 = r1.Where(r => r.SiteDetail.Site.Geo2 != null && values.Contains(r.SiteDetail.Site.Geo2.Name));
                        break;
                    case "Type":
                        r1 = r1.Where(r => values.Contains(r.SiteDetail.Type));
                        break;
                    case "Format":
                        r1 = r1.Where(r => values.Contains(r.SiteDetail.Format));
                        break;
                    case "Contractor":
                        r1 = r1.Where(r => r.SiteDetail.Site.Contractor != null && values.Contains(r.SiteDetail.Site.Contractor.Name));
                        break;
                    case "Category1":
                        r1 = r1.Where(r => r.Product != null && r.Product.Category1 != null && values.Contains(r.Product.Category1.Name));
                        break;
                    case "Category2":
                        r1 = r1.Where(r => r.Product != null && r.Product.Category2 != null && values.Contains(r.Product.Category2.Name));
                        break;
                    case "Client":
                        r1 = r1.Where(r => r.Product != null && r.Product.Client != null && values.Contains(r.Product.Client.Name));
                        break;
                    case "Product":
                        r1 = r1.Where(r => r.Product != null && values.Contains(r.Product.Name));
                        break;
                }
            }

            var r2 = r1.ToList().Select(r => new
            {
                Geo1 = r.SiteDetail.Site.Geo1 != null ? r.SiteDetail.Site.Geo1.Name : "",
                Geo2 = r.SiteDetail.Site.Geo2 != null ? r.SiteDetail.Site.Geo2.Name : "",
                Type = r.SiteDetail.Type ?? "",
                Format = r.SiteDetail.Format ?? "",
                Contractor = r.SiteDetail.Site.Contractor != null ? r.SiteDetail.Site.Contractor.Name : "",
                Product = r.Product != null ? r.Product.Name : "",
                Client = (r.Product != null && r.Product.Client != null) ? r.Product.Client.Name : "",
                Category1 = (r.Product != null && r.Product.Category1 != null) ? r.Product.Category1.Name : "",
                Category2 = (r.Product != null && r.Product.Category2 != null) ? r.Product.Category2.Name : "",
            }).ToList();

            List<ExpandoObject> l = new List<ExpandoObject>();

            var selectL = paramsL.Where(r => r.IsShow.HasValue && r.IsShow.Value);

            foreach (var item in r2)
            {
                ExpandoObject row = new ExpandoObject();
                var IDicRow = (IDictionary<String, Object>)row;

                foreach (var s in selectL.OrderBy(r => r.Order))
                {
                    switch (s.Name)
                    {
                        case "Geo1":
                            IDicRow.Add(s.PName, item.Geo1);
                            break;
                        case "Geo2":
                            IDicRow.Add(s.PName, item.Geo2);
                            break;
                        case "Type":
                            IDicRow.Add(s.PName, item.Type);
                            break;
                        case "Format":
                            IDicRow.Add(s.PName, item.Format);
                            break;
                        case "Contractor":
                            IDicRow.Add(s.PName, item.Contractor);
                            break;
                        case "Category1":
                            IDicRow.Add(s.PName, item.Category1);
                            break;
                        case "Category2":
                            IDicRow.Add(s.PName, item.Category2);
                            break;
                        case "Client":
                            IDicRow.Add(s.PName, item.Client);
                            break;
                        case "Product":
                            IDicRow.Add(s.PName, item.Product);
                            break;
                    }
                }
                l.Add(row);
            }

            var l1 = l.GroupBy(r => r, new Rpt130Comparer(paramsL));

            var countParams = paramsL.Where(r => r.IsCount);

            foreach (var item in l1)
            {
                var dic = (IDictionary<string, object>)item.Key;
                foreach (var param in countParams)
                {
                    dic[param.PName] = item.Where(r => ((r as IDictionary<string, object>)[param.PName]).ToString() == param.Values.FirstOrDefault()).Count();
                }

                dic["TotalCount"] = item.Count();
            }

            var result = l1.Select(r => r.Key).ToList();

            return View(result);
        }

        public class Rpt130Comparer : IEqualityComparer<ExpandoObject>
        {
            private List<Rpt130> l;
            public Rpt130Comparer(List<Rpt130> list)
            {
                l = list;
            }
            public bool Equals(ExpandoObject x, ExpandoObject y)
            {
                var a = (IDictionary<string, object>)x;
                var b = (IDictionary<string, object>)y;

                return l.Where(r => r.IsShow.HasValue && r.IsShow.Value && !r.IsCount).FirstOrDefault(r => (string)a[r.PName] != (string)b[r.PName]) == null;
            }

            public int GetHashCode(ExpandoObject obj)
            {
                return 0;
            }
        }

        public ActionResult _140(int contractID, DateTime? dFrom, DateTime? dTo)
        {
            var contract = ContractRepository.I.Get(contractID);
            if (contract != null)
            {
                ViewBag.ClientName = contract.ClientName;
                ViewBag.From = dFrom.ToShortDateString();
                ViewBag.To = dTo.ToShortDateString();
                Rpt140Comparer comparer = new Rpt140Comparer();
                var l = contract.ContractDetails.Select(r => new
                {
                    ContractDetail = r,
                    ContractDetailTimelines = r.ContractDetailTimelines
                                                .Where(r1 => dFrom <= r1.FromDate && r1.ToDate <= dTo)
                                                .OrderBy(r1 => r1.Order)
                                                .ToList(),

                }).ToList()
                .GroupBy(r => r.ContractDetailTimelines, comparer)
                .Select(r => new Rpt140
                {
                    ContractDetailTimelines = r.Key,
                    List = r.Select(r1 => new Rpt140.Row
                    {
                        SiteID = r1.ContractDetail.Site.ID,
                        ContractDetailID = r1.ContractDetail.ID,
                        AddressLine1 = r1.ContractDetail.Site.AddressLine1,
                        AddressLine2 = r1.ContractDetail.Site.AddressLine2,
                        Location = r1.ContractDetail.Site.Code,
                        List = r.Key.Select(r2 => r1.ContractDetail.GetByOrder(r2.Order.Value) == null ? false : r1.ContractDetail.GetByOrder(r2.Order.Value).HasValidPhoto
                        ).ToList()
                    }).ToList(),
                }).ToList()
                ;

                return View(l);
            }

            return View();
        }

        public class Rpt140Comparer : IEqualityComparer<List<ContractDetailTimeline>>
        {
            bool IEqualityComparer<List<ContractDetailTimeline>>.Equals(List<ContractDetailTimeline> x, List<ContractDetailTimeline> y)
            {
                bool isEqual = false;

                x = x ?? new List<ContractDetailTimeline>();
                y = y ?? new List<ContractDetailTimeline>();

                if (x.Count != y.Count)
                {
                    isEqual = false;
                }
                else
                {
                    isEqual = true;
                    x = x.OrderBy(r => r.Order).ToList();
                    y = y.OrderBy(r => r.Order).ToList();

                    for (int i = 0; i < x.Count; i++)
                    {
                        if (x[i].Order != y[i].Order
                            || x[i].FromDate != y[i].FromDate
                            || x[i].ToDate != y[i].ToDate)
                        {
                            isEqual = false;
                            break;
                        }
                    }
                }

                return isEqual;
            }

            int IEqualityComparer<List<ContractDetailTimeline>>.GetHashCode(List<ContractDetailTimeline> obj)
            {
                return 0;
            }
        }

        public ActionResult _150()
        {
            return View();
        }

        [HttpPost]
        public ActionResult _150(Rpt150 rpt)
        {
            OAMSEntities db = new OAMSEntities();

            if (rpt.userL != null)
            {
                var v1 = db.Sites.Where(r => true
                    && (r.CreatedDate != null && rpt.userL.Contains(r.CreatedBy) && rpt.from <= r.CreatedDate && r.CreatedDate <= rpt.to)
                    )
                    .GroupBy(r => new { r.CreatedBy, r.Geo1.Name })
                    .Select(r => new Rpt150.Row1
                    {
                        Name = r.Key.CreatedBy,
                        Geo1 = r.Key.Name,
                        CreateCount = r.Count()
                    }
                    )
                    .ToList()
                    ;

                var v2 = db.SitePhotoes.Where(r => true
                    && (r.TakenDate != null && rpt.userL.Contains(r.CreatedBy) && rpt.from <= r.TakenDate && r.TakenDate <= rpt.to)
                    )
                    .GroupBy(r => new { r.CreatedBy, r.Site.Geo1.Name })
                    .Select(r => new Rpt150.Row2
                    {
                        Name = r.Key.CreatedBy,
                        Geo1 = r.Key.Name,
                        SiteCount = r.Select(r1 => r1.SiteID).Distinct().Count(),
                        SitePhotoCount = r.Count()
                    }
                    )
                    .ToList()
                    ;

                var v3 = db.SiteDetailPhotoes.Where(r => true
                    && (r.TakenDate != null && rpt.userL.Contains(r.CreatedBy) && rpt.from <= r.TakenDate && r.TakenDate <= rpt.to)
                    )
                    .GroupBy(r => new { r.CreatedBy, r.SiteDetail.Site.Geo1.Name })
                    .Select(r => new Rpt150.Row3
                    {
                        Name = r.Key.CreatedBy,
                        Geo1 = r.Key.Name,
                        SiteDetailCount = r.Select(r1 => r1.SiteDetailID).Distinct().Count(),
                        SiteDetailPhotoCount = r.Count()
                    }
                    )
                    .ToList()
                    ;

                var geo1List = v1.Select(r => r.Geo1).Union(v2.Select(r => r.Geo1)).Union(v3.Select(r => r.Geo1)).Distinct().ToList();

                //Rpt150 rpt = new Rpt150();
                rpt.L1 = v1;
                rpt.L2 = v2;
                rpt.L3 = v3;
                rpt.Geo1L = geo1List;
            }

            return View(rpt);
        }

        public ActionResult _151(string user, DateTime? from, DateTime? to, string geo1)
        {
            OAMSEntities db = new OAMSEntities();

            List<Site> l = new List<Site>();
            if (!string.IsNullOrEmpty(user))
            {
                l = db.Sites.Where(r => true
                    && (r.CreatedDate != null && user == r.CreatedBy && from <= r.CreatedDate && r.CreatedDate <= to)
                    && (r.Geo1 != null && r.Geo1.Name == geo1)
                    )
                    .ToList()
                    ;
            }

            return View(l);
        }

        public ActionResult _152(string user, DateTime? from, DateTime? to, string geo1)
        {
            OAMSEntities db = new OAMSEntities();

            List<Site> l = new List<Site>();
            if (!string.IsNullOrEmpty(user))
            {
                l = db.SitePhotoes.Where(r => true
                    && (r.TakenDate != null && user == r.CreatedBy && from <= r.TakenDate && r.TakenDate <= to)
                    && (r.Site.Geo1 != null && r.Site.Geo1.Name == geo1)
                    )
                    .Select(r => r.Site)
                    .Distinct()
                    .ToList()
                    ;
            }

            return View(l);
        }

        public ActionResult _153(string user, DateTime? from, DateTime? to, string geo1)
        {
            OAMSEntities db = new OAMSEntities();

            List<Site> l = new List<Site>();
            if (!string.IsNullOrEmpty(user))
            {
                l = db.SiteDetailPhotoes.Where(r => true
                    && (r.TakenDate != null && user == r.CreatedBy && from <= r.TakenDate && r.TakenDate <= to)
                    && (r.SiteDetail.Site.Geo1 != null && r.SiteDetail.Site.Geo1.Name == geo1)
                    )
                    .Select(r => r.SiteDetail.Site)
                    .Distinct()
                    .ToList()
                    ;
            }

            return View(l);
        }

    }
}

