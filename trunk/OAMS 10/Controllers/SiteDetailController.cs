using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAMS.Models;

namespace OAMS.Controllers
{
    [CustomAuthorize]
    public class SiteDetailController : BaseController<SiteDetailRepository, SiteDetailController>
    {
        public PartialViewResult Add(int siteID)
        {
            var r = Repo.Add(siteID);
            return PartialView("View", r);
        }

        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            var r = Repo.Get(id);
            //r.NewCategoryFullName = r.CategoryFullName;
            return PartialView("Edit", r);
        }

        //[HttpPost]
        //public PartialViewResult Edit(SiteDetail r)
        //{
        //    if (ModelState.IsValid)
        //    {                
        //        r = Repo.Update(r);
        //        return PartialView("View", r);
        //    }
        //    return PartialView("Edit", r);
        //}

        [HttpPost]
        public PartialViewResult Edit(SiteDetail e)
        {
            var id = e.ID;
            if (ModelState.IsValid)
            {
                var r = Repo.Update(id, UpdateModel);
                return PartialView("View", r);
            }
            return PartialView("Edit", Repo.Get(id));
        }

        public PartialViewResult View(int id)
        {
            var r = Repo.Get(id);

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
