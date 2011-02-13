﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAMS.Models;
using System.ComponentModel.DataAnnotations;

namespace OAMS.Models
{

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