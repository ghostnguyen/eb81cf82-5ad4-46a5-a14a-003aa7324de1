using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAMS.Models;

namespace OAMS.Controllers
{
    [CustomAuthorize]
    public class ClientContactDetailController : BaseController<ClientContactDetailRepository>
    {
        public PartialViewResult Add(int clientContactID)
        {
            var r = Repo.Add(clientContactID);
            return PartialView("View", r);
        }

        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            ClientContactDetail r = Repo.Get(id);
            return PartialView("Edit", r);
        }
        
        [HttpPost]
        public PartialViewResult Edit(ClientContactDetail r)
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
            ClientContactDetail r = Repo.Get(id);

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
