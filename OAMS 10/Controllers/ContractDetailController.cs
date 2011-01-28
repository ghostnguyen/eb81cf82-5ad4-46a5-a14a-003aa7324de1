using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAMS.Models;

namespace OAMS.Controllers
{
    [HandleError]
    [CustomAuthorize]
    public class ContractDetailController : Controller
    {
        ContractDetailRepository repo = new ContractDetailRepository();

        public ActionResult Edit(int id)
        {
            ContractDetail e = repo.Get(id);
            //e.NewCategoryFullName = e.CategoryFullName;
            return View(e);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            repo.Update(id, UpdateModel);
            var v = repo.Get(id);
            //UpdateModel(v);
            //repo.Save();
            return RedirectToAction("Edit", "Contract", new { id = v.ContractID });
        }

        public ActionResult Delete(int id)
        {
            //var v = repo.Get(id);

            //UpdateModel(v);

            int contractID = repo.Delete(id).Value;

            return RedirectToAction("Edit", "Contract", new { id = contractID });
        }

        
        //public ActionResult Edit(int id)
        //{
        //    return View(repo.Get(id));
        //}

        //[HttpPost]
        //public PartialViewResult Edit(int id, FormCollection collection)
        //{
        //    var v = repo.Get(id);

        //    //UpdateModel(v);

        //    //repo.Save();

        //    //return PartialView("Edit", "Contract", new { id = v.ContractID });
        //    return PartialView(v);
        //}
    }
}
