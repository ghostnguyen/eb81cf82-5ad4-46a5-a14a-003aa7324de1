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
    public class QuoteDetailController : BaseController<QuoteDetailRepository>
    {
        public ActionResult Edit(int id)
        {
            QuoteDetail e = Repo.Get(id);
            
            return View(e);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            Repo.Update(id, UpdateModel);
            var v = Repo.Get(id);
            //UpdateModel(v);
            //repo.Save();
            return RedirectToAction("Edit", "Quote", new { id = v.QuoteID });
        }

        public ActionResult Delete(int id)
        {
            //var v = repo.Get(id);

            //UpdateModel(v);

            int quoteID = Repo.Delete(id).Value;

            return RedirectToAction("Edit", "Quote", new { id = quoteID });
        }
    }
}
