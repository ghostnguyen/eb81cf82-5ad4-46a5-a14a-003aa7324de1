using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAMS.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OAMS.Models
{


    public class QuoteDetailNotaion
    {

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C}")]
        public decimal? Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? ProductionPrice { get; set; }
    }


    [MetadataType(typeof(QuoteDetailNotaion))]
    public partial class QuoteDetail
    {
        public string CurrentProductName
        {
            get
            {
                return Product != null ? Product.Name : "";
            }
        }
    }
}