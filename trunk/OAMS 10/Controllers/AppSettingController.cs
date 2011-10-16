using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAMS.Models;

namespace OAMS.Controllers
{
    [CustomAuthorize]
    public class AppSettingController : BaseController<AppSettingRepository>
    {
        //
        // GET: /AppSetting/

        public ActionResult Index()
        {
            return View(Repo.GetAll());
        }

        public ActionResult UpdateDefaultGeoID(Guid id)
        {
            Repo.InsertOrUpdate(PropertyName.For(() => AppSetting.DefaultGeoID), id.ToString());

            return RedirectToAction("Index", "Home");
        }

        public ActionResult UpdateFindMapCenter(string lat, string lng)
        {
            if (!String.IsNullOrEmpty(lat))
            {
                Repo.InsertOrUpdate(PropertyName.For(() => AppSetting.FindMapCenterLat), lat);
            }

            if (!String.IsNullOrEmpty(lng))
            {
                Repo.InsertOrUpdate(PropertyName.For(() => AppSetting.FindMapCenterLng), lng);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult UpdateMapBound(string swlat, string swlng, string nelat, string nelng)
        {
            if (!String.IsNullOrEmpty(swlat))
            {
                Repo.InsertOrUpdate(PropertyName.For(() => AppSetting.MapBoundSWLat), swlat);
            }

            if (!String.IsNullOrEmpty(swlng))
            {
                Repo.InsertOrUpdate(PropertyName.For(() => AppSetting.MapBoundSWLng), swlng);
            }

            if (!String.IsNullOrEmpty(nelat))
            {
                Repo.InsertOrUpdate(PropertyName.For(() => AppSetting.MapBoundNELat), nelat);
            }

            if (!String.IsNullOrEmpty(nelng))
            {
                Repo.InsertOrUpdate(PropertyName.For(() => AppSetting.MapBoundNELng), nelng);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult UpdatePicasaAccount(string username, string password)
        {
            if (!String.IsNullOrEmpty(username))
            {
                Repo.InsertOrUpdate(PropertyName.For(() => AppSetting.GoogleUsername), username);
            }

            if (!String.IsNullOrEmpty(password))
            {
                Repo.InsertOrUpdate(PropertyName.For(() => AppSetting.GooglePassword), password);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Update(string key, string value)
        {
            if (!string.IsNullOrEmpty(key))
            {
                Repo.InsertOrUpdate(key, value);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteEmptyAlbum()
        {
            PicasaRepository.I.DeleteEmptyAlbum();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Deploy()
        {
            System.Diagnostics.EventLog.CreateEventSource("OAMS", "OAMS123");

            System.Diagnostics.EventLog.WriteEntry("OAMS", "deploy", System.Diagnostics.EventLogEntryType.Information);

            return RedirectToAction("About", "Home");
        }


    }
}
