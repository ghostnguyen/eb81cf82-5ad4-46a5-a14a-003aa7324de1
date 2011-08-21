﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAMS.Models;

namespace OAMS.Controllers
{
    public class QuoteDetailFeeController : BaseController<QuoteDetailFeeRepository>
    {
        [HttpGet]
        public JsonResult Get(int quoteDetailID)
        {
            return Json(Repo.Get(quoteDetailID).Select(r => new
            {
                r.ID,
                r.QuoteDetailID,
                r.MediaRate,
                r.ProductionFee,
                r.LightFee,
                r.VAT,
                r.Discount,
                r.Other,
                r.Note
            }), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(List<QuoteDetailFee> l)
        {
            Repo.Save(l);
            return Json(Repo.Get(l.First().QuoteDetailID.Value).Select(r => new
            {
                r.ID,
                r.QuoteDetailID,
                r.MediaRate,
                r.ProductionFee,
                r.LightFee,
                r.VAT,
                r.Discount,
                r.Other,
                r.Note
            }));
        }
    }
}
