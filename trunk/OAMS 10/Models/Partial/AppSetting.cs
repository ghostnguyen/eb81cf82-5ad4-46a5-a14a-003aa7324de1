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
        public static string DefaultGeo1Name { get; set; }

        public static string FindMapCenterLat { get; set; }
        public static string FindMapCenterLng { get; set; }

        public static string MapBoundSWLat { get; set; }
        public static string MapBoundSWLng { get; set; }
        public static string MapBoundNELat { get; set; }
        public static string MapBoundNELng { get; set; }

        static public string GoogleUsername { get; set; }
        static public string GooglePassword { get; set; }
        static public string AmbientClientUrl { get; set; }

        static public string AlbumAtomUrl { get; set; }


        /// <summary>
        /// Valid range in meter
        /// </summary>
        static public int ValidRange { get; set; }
    }
}