using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAMS.Models;

namespace OAMS.Controllers
{
    [CustomAuthorize]
    public class ContractTimelineController : BaseController<ContractTimelineRepository>
    {
        
        public PartialViewResult Add(int contractID, int timeCount, string timeUnit)
        {
            ContractTimeline r = Repo.Add(contractID, timeCount, timeUnit);
            return PartialView("View", r);
        }

        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            ContractTimeline r = Repo.Get(id);
            return PartialView("Edit", r);
        }

        [HttpPost]
        public PartialViewResult Edit(ContractTimeline r)
        {
            if (ModelState.IsValid)
            {
                r = Repo.Update(r);
                return PartialView("View", r);
            }
            return PartialView("Edit", r);
        }

        
        public PartialViewResult View(int id)
        {
            ContractTimeline r = Repo.Get(id);

            return PartialView("View", r);
        }

        [HttpPost]
        public PartialViewResult DeleteTheLastTimeline(int contractID)
        {
            var r = Repo.DeleteTheLastTimeline(contractID);

            //return new JsonResult { Data = new { Err = "as", Html = PartialView("ContractTimelineView", r) } };

            return PartialView("~/Views/Contract/ManageContractTimeline.ascx", r);
        }

    }
}
