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


        public int? RoadType2 { get; set; }
        public List<int> InstallationPosition1MarkList { get; set; }
        public int? InstallationPosition2 { get; set; }
        public string ViewingDistance { get; set; }

        public string ViewingSpeed { get; set; }

        public bool IsWithinCircle { get; set; }
        public double Distance { get; set; }
        public double? Lat { get; set; }
        public double? Long { get; set; }
        //public DateTime? From { get; set; }
        //public DateTime? To { get; set; }

        public List<int?> ContractorList { get; set; }
        public List<int?> ClientList { get; set; }
        public List<int?> ProductIDList { get; set; }
        public List<Guid?> CatList { get; set; }
        public int? ScoreFrom { get; set; }
        public int? ScoreTo { get; set; }

        //public int? OutdateInDaysFrom { get; set; }
        //public int? OutdateInDaysTo { get; set; }

        //public bool NoPhoto { get; set; }
        public DateTime? NoPhotoFrom { get; set; }
        public DateTime? NoPhotoTo { get; set; }

        //public DateTime? LastPhotoFrom { get; set; }
        //public DateTime? LastPhotoTo { get; set; }
        public DateTime? HasPhotoFrom { get; set; }
        public DateTime? HasPhotoTo { get; set; }

        public FindSite()
        {
            ContractorList = new List<int?>();
            ProductIDList = new List<int?>();
            InstallationPosition1MarkList = new List<int>();
            CatList = new List<Guid?>();
            ClientList = new List<int?>();
            Geo2List = new List<string>();
        }
    }
}



