using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAMS.Models;
using System.Web.Mvc;

namespace OAMS.Controllers
{
    [CustomAuthorize]
    public class ContractDetailTimelineController : BaseController<ContractDetailTimelineRepository>
    {
        public PartialViewResult Add(int contractDetailID, int timeCount, string timeUnit)
        {
            ContractDetailTimeline r = Repo.Add(contractDetailID, timeCount, timeUnit);
            return PartialView("View", r);
        }

        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            ContractDetailTimeline r = Repo.Get(id);
            return PartialView("Edit", r);
        }

        [HttpPost]
        public PartialViewResult Edit(ContractDetailTimeline r)
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
            var r = Repo.Get(id);

            return PartialView("View", r);
        }

        [HttpPost]
        public PartialViewResult DeleteTheLastTimeline(int contractDetailID)
        {
            var r = Repo.DeleteTheLastTimeline(contractDetailID);

            //return new JsonResult { Data = new { Err = "as", Html = PartialView("ContractTimelineView", r) } };

            return PartialView("~/Views/ContractDetail/ManageContractDetailTimeline.ascx", r);
        }
    }
}