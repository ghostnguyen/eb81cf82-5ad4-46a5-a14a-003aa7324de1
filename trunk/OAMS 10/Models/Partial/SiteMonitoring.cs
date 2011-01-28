using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAMS.Models;
using System.ComponentModel.DataAnnotations;

namespace OAMS.Models
{

    public partial class SiteMonitoring
    {
        public Site Site;
        public string CurrentProductName
        {
            get
            {
                return Product != null ? Product.Name : "";
            }
        }

        public bool HasInvalidPhoto
        {
            get
            {
                return SiteMonitoringPhotoes.Where(r => !r.IsValidTakenDate).Count() > 0;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class,
                AllowMultiple = false,
                Inherited = true)]
    public sealed class ValidateSiteMonitoringOrderAttribute
        : ValidationAttribute
    {
        private const string _defaultErrorMessage =
            "Order conflicts with photos.";

        public ValidateSiteMonitoringOrderAttribute()
            : base(_defaultErrorMessage)
        {
        }

        public override bool IsValid(object value)
        {
            bool isValid = false;
            var e = value as SiteMonitoring;

            if (e != null)
            {
                var repo = new SiteMonitoringRepository();
                isValid = repo.ValidateOrder(e.ID, e.Order);
            }

            return isValid;
        }
    }

}