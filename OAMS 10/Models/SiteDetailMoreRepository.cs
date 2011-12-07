using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Objects;

namespace OAMS.Models
{
    public class SiteDetailMoreRepository : BaseRepository<SiteDetailMoreRepository>
    {
        //public IQueryable<SiteDetailMore> GetAll()
        //{
        //    return DB.ContractorContactDetails;
        //}

        public SiteDetailMore Add(int siteDetailID)
        {
            //var siteDetailRepo = new SiteDetailRepository() { DB = DB };
            //var siteDetail = siteDetailRepo.Get(siteDetailID);

            var siteDetailMore = new SiteDetailMore() { SiteDetailID = siteDetailID };
            //contractorContact.ContractorContactDetails.Add(contractorContactDetail);
            DB.SiteDetailMores.AddObject(siteDetailMore);
            Save();
            return siteDetailMore;
        }

        public SiteDetailMore Get(int id)
        {
            return DB.SiteDetailMores.SingleOrDefault(r => r.ID == id);
        }

        //public SiteDetailMore Update(SiteDetailMore e)
        //{
        //    DB.SiteDetailMores.Attach(e);
        //    DB.ObjectStateManager.ChangeObjectState(e, System.Data.EntityState.Modified);
        //    Save();
        //    return e;
        //}

        public SiteDetailMore Update(int id,Action<SiteDetailMore> updateF)
        {
            var r = Get(id);
            updateF(r);
            Save();
            return r;
        }

        public void Delete(int id)
        {
            var v = Get(id);
            DB.DeleteObject(v);
            Save();
        }
    }
}
