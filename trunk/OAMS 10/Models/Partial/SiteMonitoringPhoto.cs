using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAMS.Models;
using System.ComponentModel.DataAnnotations;

namespace OAMS.Models
{

    public partial class SiteMonitoringPhoto
    {
        public bool IsValidTakenDate
        {
            get
            {
                bool result = false;

                ContractDetailTimeline timeline = SiteMonitoring.ContractDetail.ContractDetailTimelines.Where(r => r.Order == SiteMonitoring.Order).FirstOrDefault();
                if ((IsReview.HasValue && IsReview.Value)
                    || timeline == null)
                {
                    result = true;
                }
                else
                {
                    result = TakenDate.HasValue
                        && timeline.Contains(TakenDate);
                }

                return result;
            }
        }
    }
}