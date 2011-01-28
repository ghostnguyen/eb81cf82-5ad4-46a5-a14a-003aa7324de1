using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAMS.Models;
using Google.GData.Photos;

namespace OAMS.Controllers
{
    [CustomAuthorize]
    public class SiteMonitoringController : AsyncController
    {
        //
        // GET: /Site/

        SiteMonitoringRepository repo = new SiteMonitoringRepository();



        //
        // GET: /Site/Details/5

        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //
        // GET: /Site/Create

        public ActionResult Create(int? contractDetailID)
        {
            SiteMonitoring e = repo.InitWithDefaultValue(contractDetailID);
            return View(e);
        }

        //
        // POST: /Site/Create



        [HttpPost]
        public ActionResult Create(int? contractDetailID, IEnumerable<HttpPostedFileBase> files, string[] noteList)
        {
            repo.Create(UpdateModel, files, contractDetailID, noteList);

            ContractDetailRepository contractDetailRepository = new ContractDetailRepository();

            ContractDetail e = contractDetailRepository.Get(contractDetailID.Value);

            return RedirectToAction("Edit", "Contract", new { id = e.ContractID });
        }

        //
        // GET: /Site/Edit/5

        public ActionResult Edit(int id)
        {
            SiteMonitoring e = repo.Get(id);
            ContractDetailRepository cdrepo = new ContractDetailRepository();
            ContractDetail cd = cdrepo.Get(e.ContractDetailID.ToInt());
            e.Site = cd.Site;
            return View(e);
        }

        //
        // POST: /Site/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, IEnumerable<HttpPostedFileBase> files, List<int> DeletePhotoList, SiteMonitoring siteMonitoring, IEnumerable<HttpPostedFileBase> filesOfFixed, string[] noteList, string[] noteOfFixedList)
        {
            SiteMonitoring e = repo.Update(id, UpdateModel, files, DeletePhotoList, filesOfFixed, noteList, noteOfFixedList);
            return RedirectToAction("Edit", "Contract", new { id = e.ContractDetail.ContractID });
        }

        public ActionResult EditLast(int contractDetailID)
        {
            return RedirectToAction("Edit", new { id = repo.GetLastID(contractDetailID) });
        }


        public ActionResult View(int id)
        {
            return View(repo.Get(id));
        }

        //
        // POST: /Site/Delete/5

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    //repo.DeletePhoto(id);
        //    repo.Delete(id);
        //    return RedirectToAction("Index");
        //}





    }
}
