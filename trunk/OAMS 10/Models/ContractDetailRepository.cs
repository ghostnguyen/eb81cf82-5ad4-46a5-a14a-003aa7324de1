using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public class ContractDetailRepository : BaseRepository
    {
        public ContractDetail Get(int id)
        {
            return DB.ContractDetails.Single(r => r.ID == id);
        }

        public IQueryable<ContractDetail> GetAll()
        {
            return DB.ContractDetails;
        }

        public int? Delete(int id)
        {
            ContractDetail e = Get(id);

            int? contractID = e.ContractID;

            var l = e.SiteMonitorings.ToList();
            foreach (var item in l)
            {
                DB.DeleteObject(item);
            }

            var l1 = e.ContractDetailTimelines.ToList();
            foreach (var item in l1)
            {
                DB.DeleteObject(item);
            }

            DB.DeleteObject(e);
            Save();

            return contractID;
        }

        public void CopyTimeline(int id)
        {
            var r = Get(id);
            if (r.ContractDetailTimelines.Count == 0)
            {
                foreach (var item in r.Contract.ContractTimelines)
                {
                    var newItem = new ContractDetailTimeline();
                    newItem.ContractDetailID = id;
                    newItem.Order = item.Order;
                    newItem.FromDate = item.FromDate;
                    newItem.ToDate = item.ToDate;

                    DB.ContractDetailTimelines.AddObject(newItem);
                }

                DB.SaveChanges();
            }
        }

        public void Update(int ID, Action<ContractDetail> updateMethod)
        {
            ContractDetail e = Get(ID);
            updateMethod(e);
            //UpdateCategory(e);
            Save();
        }



        //public void UpdateCategory(ContractDetail e)
        //{
        //    CategoryRepository catRepository = new CategoryRepository();
        //    catRepository.Set3LevelByFullname(e.NewCategoryFullName, e.UpdateCategory);
        //}
    }
}