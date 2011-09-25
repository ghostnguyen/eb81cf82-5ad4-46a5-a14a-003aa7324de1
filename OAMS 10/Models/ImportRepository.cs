using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public class ImportRepository : BaseRepository<ImportRepository>
    {
        public void Import()
        {
            //SiteRepository siteRepo = new SiteRepository();
            //var data = DB.Temps.ToList();
            //foreach (var item in data)
            //{
            //    Site e = siteRepo.InitWithDefaultValue();

            //    e.Code = item.Site;
            //    e.AddressLine1 = item.Address;
            //    e.Lng = item.Lng;
            //    e.Lat = item.Lat;
            //    e.Geo1ID = new Guid("3FEF9863-0408-49D7-A6F6-70998711C493");
            //    e.Geo2ID = new Guid("617323E3-7DEB-4953-8890-F0C559E700EC");

            //    siteRepo.UpdateFrontBackLit(e);
            //    DB.Sites.AddObject(e);

            //    Save();

            //    var siteDetail = new SiteDetail();
            //    siteDetail.Type = "BSH";
            //    siteDetail.Name = item.Bus + " - " + item.Face;

            //    e.SiteDetails.Add(siteDetail);

            //    Save();
            //}
        }
    }
}