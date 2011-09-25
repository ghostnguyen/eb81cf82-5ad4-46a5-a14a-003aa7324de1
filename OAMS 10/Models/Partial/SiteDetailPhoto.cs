using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAMS.Models;
using System.ComponentModel.DataAnnotations;

namespace OAMS.Models
{
    public partial class SiteDetailPhoto : IPhoto
    {
        public bool IsValidGPS
        {
            get
            {
                bool result = false;

                var v = SiteDetail.Site;

                if (!v.Lat.HasValue || !v.Lng.HasValue
                    || !Lat.HasValue || !Lng.HasValue)
                {                    
                    result = true;
                }
                else
                {
                    result = Helper.DistanceBetweenPoints(v.Lat, v.Lng, Lat, Lng) * 1000 <= AppSetting.ValidRange;
                }

                return result;
            }
        }
    }
}