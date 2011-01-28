using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public class Rpt01_Model_SiteMonitoring
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string ClientName { get; set; }
        public List<Rpt01_Model_SiteMonitoringDetail> Details { get; set; }
        public int TotalWorking { get; set; }
        public int TotalIssues { get; set; }
        public int TotalClean { get; set; }
        public int TotalCreativeGoodCondition { get; set; }
        public int TotalExternnalInterference { get; set; }
        public int TotalVandalism { get; set; }
        public int TotalRequiredFollowUp { get; set; }
        public int TotalNowFixed { get; set; }
    }

    public class Rpt01_Model_SiteMonitoringDetail
    {
        public int ID { get; set; }
        public string SiteCode { get; set; }
        public string Supplier { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string Size { get; set; }
        public string Client { get; set; }
        public string Product { get; set; }
        public string SiteType { get; set; }
        public string SiteFormat { get; set; }
        public string Inspection { get; set; }
        public int PhotoCount { get; set; }
        public string BackFront { get; set; }
        public bool? Working { get; set; }

        public int? NumOfBulbs { get; set; }
        public int? BulbsWorking { get; set; }
        public string Issue { get; set; }
        public bool? Clean { get; set; }
        public bool? CreativeGoodConditon { get; set; }


        public bool? ExternalInterference { get; set; }
        public bool? Vandalism { get; set; }
        public string FollowUp { get; set; }
        public bool? RequiredFollowUp { get; set; }
        public bool? NowFixed { get; set; }
        
        public string Action { get; set; }
        public DateTime? LastestPhotoDate { get; set; }
        public DateTime? DateOfProblem { get; set; }
        public DateTime? DateFixed { get; set; }
        
        
        public string Comments { get; set; }
    }
}