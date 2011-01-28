using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAMS.Models;

namespace OAMS.Controllers
{
    [CustomAuthorize]
    public class CampaignController : Controller
    {
        CampaignRepository repo = new CampaignRepository();

        public ActionResult Index()
        {
            return View(repo.GetAll());
        }

        public ActionResult Create()
        {
            return View(new Campaign());
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var v = new Campaign();

            UpdateModel(v);

            repo.Add(v);

            repo.Save();

            return RedirectToAction("Edit");
        }

        public ActionResult Edit(int id)
        {
            return View(repo.Get(id));
        }

        //
        // POST: /Contractor/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var v = repo.Get(id);

            UpdateModel(v);

            repo.Save();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult AddSiteDetail(int campaignID, int contractDetailID)
        {
            CampaignDetailRepository cdr = new CampaignDetailRepository();
            CampaignDetail e = cdr.Create(campaignID, contractDetailID);
            if (e != null)
            {
                cdr.Save();

                return Json(e.ID);
            }
            else { return Json(0); }
        }
    }
}
