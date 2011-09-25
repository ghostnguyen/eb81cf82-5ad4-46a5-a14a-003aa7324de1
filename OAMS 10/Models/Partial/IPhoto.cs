using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public interface IPhoto
    {
        int ID { get; set; }
        string Url { get; set; }
        string AtomUrl { get; set; }
        DateTime? TakenDate { get; set; }
        string Note { get; set; }
        double? Lng { get; set; }
        double? Lat { get; set; }
    }
}