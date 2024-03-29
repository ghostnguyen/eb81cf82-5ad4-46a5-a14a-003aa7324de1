﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAMS.Models;

namespace OAMS.Controllers
{
    [CustomAuthorize]
    public class SiteDetailMoreController : BaseController<SiteDetailMoreRepository, SiteDetailMoreController>
    {
        public PartialViewResult Add(int siteDetailID)
        {
            var r = Repo.Add(siteDetailID);
            return PartialView("View", r);
        }

        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            SiteDetailMore r = Repo.Get(id);
            return PartialView("Edit", r);
        }

        [HttpPost]
        public PartialViewResult Edit(SiteDetailMore r)
        {
            var id = r.ID;
            if (ModelState.IsValid)
            {
                r = Repo.Update(id, UpdateModel);
                return PartialView("View", r);
            }
            return PartialView("Edit", r);
        }


        public PartialViewResult View2(int id)
        {
            SiteDetailMore r = Repo.Get(id);

            return PartialView("View", r);
        }

        [HttpPost]
        public PartialViewResult Delete(int id)
        {
            Repo.Delete(id);
            return null;
        }
    }
}
