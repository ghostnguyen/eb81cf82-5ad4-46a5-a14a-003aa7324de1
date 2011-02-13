using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAMS.Models;
using System.ComponentModel.DataAnnotations;

namespace OAMS.Models
{
    [MetadataType(typeof(QuoteNotaion))]
    public partial class Quote
    {
        public List<QuoteDetail> Details
        {
            get
            {
                return Details.ToList();
            }
        }

        public string ContractorName
        {
            get
            {
                return Contractor != null ? Contractor.Name : "";
            }
        }

        public string ClientName
        {
            get
            {
                return Client != null ? Client.Name : "";
            }
        }
    }
    public class QuoteNotaion
    {
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? SignedDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? ExpiredDate { get; set; }
    }
}