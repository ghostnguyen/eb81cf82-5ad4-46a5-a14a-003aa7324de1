using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public class ProductRepository : BaseRepository<ProductRepository>
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
            Product e = Get(id);
            Product replaceClient = Get(replaceID);

            if (e == null || replaceClient == null)
            {
                return false;
            }
            else
            {
                var sL = DB.ContractDetails.Where(r => r.ProductID == replaceID);
                IQueryable<SiteDetailMore> cL = DB.SiteDetailMores.Where(r => r.ProductID == replaceID);
                IQueryable<SiteMonitoring> caL = DB.SiteMonitorings.Where(r => r.ProductID == replaceID);

                foreach (var item in sL)
                {
                    item.ProductID = id;
                }
                foreach (var item in cL)
                {
                    item.ProductID = id;
                }
                foreach (var item in caL)
                {
                    item.ProductID = id;
                }

                Save();

                DB.Products.DeleteObject(replaceClient);
                Save();

                return true;
            }
        }
    }
}