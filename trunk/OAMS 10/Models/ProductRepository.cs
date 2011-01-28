using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public class ProductRepository : BaseRepository
    {
        public IQueryable<Product> GetAll()
        {
            return DB.Products;
        }

        public Product Get(int ID)
        {
            return DB.Products.Where(r => r.ID == ID).SingleOrDefault();
        }

        public Product Add(Product e)
        {
            UpdateCategory(e);
            DB.Products.AddObject(e);
            Save();
            return e;
        }

        public Product Update(Product e)
        {
            DB.Products.Attach(e);
            UpdateCategory(e);
            DB.ObjectStateManager.ChangeObjectState(e, System.Data.EntityState.Modified);
            Save();

            return e;
        }

        public void UpdateCategory(Product e)
        {
            CategoryRepository catRepository = new CategoryRepository();
            catRepository.Set3LevelByFullname(e.NewCategoryFullName, e.UpdateCategory);
        }

        public void Delete(Product e)
        {
            DB.Products.DeleteObject(e);
            Save();
        }

        public bool Replace(int id, int replaceID)
        {
            //Product e = Get(id);
            //Product replaceClient = Get(replaceID);

            //if (e == null || replaceClient == null)
            //{
            //    return false;
            //}
            //else
            //{
            //    var sL = DB.SiteDetails.Where(r => r.ClientID == replaceID);
            //    IQueryable<Contract> cL = DB.Contracts.Where(r => r.ClientID == replaceID);
            //    IQueryable<Campaign> caL = DB.Campaigns.Where(r => r.ClientID == replaceID);
            //    foreach (var item in sL)
            //    {
            //        item.ClientID = id;
            //    }
            //    foreach (var item in cL)
            //    {
            //        item.ClientID = id;
            //    }
            //    foreach (var item in caL)
            //    {
            //        item.ClientID = id;
            //    }

            //    Save();
            //    DB.Products.DeleteObject(replaceClient);
            //    Save();

            //    return true;
            //}
            return false;
        }
    }
}