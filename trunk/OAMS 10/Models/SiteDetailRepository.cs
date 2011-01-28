using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public class SiteDetailRepository : BaseRepository
    {
        public IQueryable<SiteDetail> GetAll()
        {
            return DB.SiteDetails;
        }

        //public void UpdateCategory(SiteDetail e)
        //{
        //    CategoryRepository catRepository = new CategoryRepository();
        //    catRepository.Set3LevelByFullname(e.NewCategoryFullName, e.UpdateCategory);
        //}



        public SiteDetail Add(int siteID)
        {
            var siteRepo = new SiteRepository() { DB = DB };
            var site = siteRepo.Get(siteID);

            var siteDetail = new SiteDetail();
            site.SiteDetails.Add(siteDetail);

            Save();

            return siteDetail;
        }

        public SiteDetail Get(int id)
        {
            var v = DB.SiteDetails.SingleOrDefault(r => r.ID == id);
            return v;
        }

        public SiteDetail Update(SiteDetail e)
        {
            DB.SiteDetails.Attach(e);
            //UpdateCategory(e);
            DB.ObjectStateManager.ChangeObjectState(e, System.Data.EntityState.Modified);
            Save();

            return e;
        }

        public void Delete(int id)
        {
            var v = Get(id);
            DB.DeleteObject(v);
            Save();
        }
    }
}