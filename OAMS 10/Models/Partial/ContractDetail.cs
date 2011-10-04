using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAMS.Models;
using System.ComponentModel.DataAnnotations;

namespace OAMS.Models
{

    public partial class ContractDetail
    {
        public string CurrentProductName
        {
            get
            {
                return Product != null ? Product.Name : "";
            }
        }

        public SiteMonitoring GetByOrder(int order)
        {
            return this.SiteMonitorings.Where(r => r.Order == order).FirstOrDefault();
        }
    }
}