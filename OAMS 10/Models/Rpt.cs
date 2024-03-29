﻿using System;
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
        public List<Rpt101Row> List { get; set; }
    }

    public class Rpt101Row
    {
        public string Geo2 { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }
    }

    public class Rpt102
    {
        public string Cat1FullName { get; set; }
        public string Geo1FullName { get; set; }
        public List<Rpt102Row> List { get; set; }
        public bool HideCat2 { get; set; }
    }

    public class Rpt102Row
    {
        public string Type { get; set; }
        public string Cat1 { get; set; }
        public string Cat2 { get; set; }
        public int Count { get; set; }

    }

    public class Rpt103
    {
        public string Geo1FullName { get; set; }
        public List<Rpt103Row> List { get; set; }
        public bool HideProduct { get; set; }
    }

    public class Rpt103Row
    {
        public string Client { get; set; }
        public string Product { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }
    }

    public class Rpt104
    {
        public string Geo1FullName { get; set; }
        public List<Rpt104Row> List { get; set; }
        public bool HideProduct { get; set; }
    }

    public class Rpt104Row
    {
        public string Contractor { get; set; }
        public string Product { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }
    }

    public class Rpt105
    {
        public string Geo1FullName { get; set; }
        public string Type { get; set; }
        public int LessThan { get; set; }
        public List<Row> List { get; set; }

        public class Row
        {
            public string Geo2 { get; set; }
            public int Count { get; set; }
        }
    }

    public class Rpt106
    {
        public string Geo1FullName { get; set; }
        public List<Row> List { get; set; }

        public class Row
        {
            public string Type { get; set; }
            public int Count { get; set; }
        }
    }

    public class Rpt107
    {
        public string Geo1FullName { get; set; }
        public string Cat1FullName { get; set; }
        public int LessThan { get; set; }
        public List<Row> List { get; set; }

        public class Row
        {
            public string Geo2 { get; set; }
            public int Count { get; set; }
        }
    }

    public class Rpt108
    {
        public string Geo1FullName { get; set; }
        public string Cat1FullName { get; set; }
        public List<Row> List { get; set; }

        public class Row
        {
            public string Type { get; set; }
            public int Count { get; set; }
        }
    }

    public class Rpt109
    {
        public string Geo1FullName { get; set; }
        public string Cat1FullName { get; set; }
        public List<Row> List { get; set; }

        public class Row
        {
            public string Client { get; set; }
            public int Count { get; set; }
        }
    }

    public class Rpt110
    {
        public string Geo1FullName { get; set; }
        public string Cat1FullName { get; set; }
        public List<Row> List { get; set; }

        public class Row
        {
            public string Product { get; set; }
            public int Count { get; set; }
        }
    }

    public class Rpt111
    {
        public string Geo1FullName { get; set; }
        public string Cat1FullName { get; set; }
        public string Client { get; set; }
        public List<Row> List { get; set; }

        public class Row
        {
            public string Geo2 { get; set; }
            public int Count { get; set; }
        }
    }

    public class Rpt112
    {
        public string Geo1FullName { get; set; }
        public string Cat1FullName { get; set; }
        public string Client { get; set; }
        public List<Row> List { get; set; }

        public class Row
        {
            public string Product { get; set; }
            public int Count { get; set; }
        }
    }

    public class Rpt120
    {
        public string Geo1FullName { get; set; }
        public string Cat1FullName { get; set; }
        public string Client { get; set; }
        public string Type { get; set; }
        public int LessThan { get; set; }

        public string GroupBy { get; set; }
        public List<Row> List { get; set; }

        public class Row
        {
            public string Note { get; set; }
            public int Count { get; set; }
        }
    }

    public class Rpt130
    {
        public string Name { get; set; }
        public List<string> Values { get; set; }
        public bool IsCount { get; set; }
        public bool? IsShow { get; set; }
        public int Order { get; set; }
        public string PName { get; set; }

        public int? LessThan { get; set; }
    }

    public class Rpt140
    {
        public List<ContractDetailTimeline> ContractDetailTimelines { get; set; }
        public List<Row> List { get; set; }

        public class Row
        {
            public int SiteID { get; set; }
            public int ContractDetailID { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string Location { get; set; }
            public List<bool> List { get; set; }
        }
    }

    public class Rpt150
    {
        public DateTime? from { get; set; }
        public DateTime? to { get; set; }
        public List<string> userL { get; set; }

        public List<Row1> L1 { get; set; }
        public List<Row2> L2 { get; set; }
        public List<Row3> L3 { get; set; }
        public List<string> Geo1L { get; set; }


        public class Row1
        {
            public string Name { get; set; }
            public string Geo1 { get; set; }
            public int CreateCount { get; set; }
        }

        public class Row2
        {
            public string Name { get; set; }
            public string Geo1 { get; set; }
            public int SiteCount { get; set; }
            public int SitePhotoCount { get; set; }
        }

        public class Row3
        {
            public string Name { get; set; }
            public string Geo1 { get; set; }
            public int SiteDetailCount { get; set; }
            public int SiteDetailPhotoCount { get; set; }
        }
    }
}
