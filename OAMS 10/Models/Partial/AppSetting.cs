using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAMS.Models;
using System.ComponentModel.DataAnnotations;

namespace OAMS.Models
{
    partial class AppSetting
    {
        public static Guid DefaultGeoID { get; set; }
    }
}