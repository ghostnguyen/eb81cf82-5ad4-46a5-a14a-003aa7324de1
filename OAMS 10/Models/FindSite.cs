using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public class FindSite
    {
        public int CampaignID { get; set; }

        public string Geo1FullName { get; set; }
        public List<string> Geo2List { get; set; }

        public List<string> StyleList { get; set; }
        public string Format { get; set; }

        public string RoadType1 { get; set; }
        public string RoadType2 { get; set; }
        public string InstallationPosition1 { get; set; }
        public string InstallationPosition2 { get; set; }
        public string ViewingDistance { get; set; }
       
        public string ViewingSpeed { get; set; }
        public string Height { get; set; }
        public string DirectionalTrafficPublicTransport { get; set; }
        public string ShopSignsBillboards { get; set; }
        public string FlagsTemporaryBannersPromotionalItems { get; set; }
        public string CompetitiveProductSigns { get; set; }

        public bool IsWithinCircle { get; set; }
        public double Distance { get; set; }
        public double? Lat { get; set; }
        public double? Long { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public List<int> ContractorList { get; set; }
        //public List<string> ClientList { get; set; }
        public List<int> ClientList { get; set; }
        public List<string> ProductList { get; set; }
        public List<string> CatList { get; set; }
        public string ScoreFrom { get; set; }
        public string ScoreTo { get; set; }
    }
}


