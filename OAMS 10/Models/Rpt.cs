using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public class Rpt
    {

    }

    public class Rpt101
    {
        public string Geo1FullName { get; set; }
        public IQueryable<SiteDetail> sdList { get; set; }
    }

    public class Rpt102
    {
        public string Cat1FullName { get; set; }
        public List<SiteDetailMore> List { get; set; }
        public bool HideCat2 { get; set; }
    }
}