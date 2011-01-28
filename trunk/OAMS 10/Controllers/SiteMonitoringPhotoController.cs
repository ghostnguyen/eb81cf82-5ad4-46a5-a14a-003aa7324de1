using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAMS.Models;

namespace OAMS.Controllers
{
    [CustomAuthorize]
    public class SiteMonitoringPhotoController : BaseController<SiteMonitoringPhotoRepository>
    {
        //
        // GET: /SitePhoto/

        [HttpPost]
        public JsonResult EditNote(int id, string note)
        {
            Repo.EditNote(id, note);
            return new JsonResult();
        }

    }
}
