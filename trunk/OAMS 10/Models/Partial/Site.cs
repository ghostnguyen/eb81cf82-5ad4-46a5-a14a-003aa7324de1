using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAMS.Models;
using System.ComponentModel.DataAnnotations;

namespace OAMS.Models
{

    public partial class Site
    {
        public string GeoFullName
        {
            get
            {
                GeoRepository geoRepository = new GeoRepository();
                return geoRepository.GetFullname(Geo1, Geo2, Geo3);
            }
        }



        public string NewGeoFullName
        {
            get;
            set;
        }

        

        public string NewCategoryFullName
        {
            get;
            set;
        }

        public string ContractorName
        {
            get
            {
                return Contractor != null ? Contractor.Name : "";
            }
        }



        public int UpdateGeo(Guid? geoID1, Guid? geoID2, Guid? geoID3)
        {
            Geo1ID = geoID1;
            Geo2ID = geoID2;
            Geo3ID = geoID3;

            return 0;
        }


    }
}