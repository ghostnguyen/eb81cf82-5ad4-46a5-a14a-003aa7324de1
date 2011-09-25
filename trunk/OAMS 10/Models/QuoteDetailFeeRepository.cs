using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public class QuoteDetailFeeRepository : BaseRepository<QuoteDetailFeeRepository>
    {
        public IQueryable<QuoteDetailFee> Get(int quoteDetailID)
        {
            return DB.QuoteDetailFees.Where(r => r.QuoteDetailID == quoteDetailID);
        }

        public void Save(List<QuoteDetailFee> l, List<int?> deleteIDList, List<int?> updateIDList)
        {
            if (l.GroupBy(r => r.QuoteDetailID).Count() != 1)
            {
                throw new Exception("List<QuoteDetailFee> Error");
            }

            if (deleteIDList != null)
            {
                var deleteL = DB.QuoteDetailFees.Where(r => deleteIDList.Contains(r.ID));
                foreach (var item in deleteL)
                {
                    DB.DeleteObject(item);
                }
                Save();
            }

            var newL = l.Where(r => r.ID == 0);
            foreach (var item in newL)
            {
                DB.QuoteDetailFees.AddObject(item);
            }
            Save();

            if (updateIDList != null)
            {
                var updateL = l.Where(r => updateIDList.Contains(r.ID));
                foreach (var item in updateL)
                {
                    DB.QuoteDetailFees.Attach(item);
                    //DB.DetectChanges();
                    DB.ObjectStateManager.ChangeObjectState(item, System.Data.EntityState.Modified);
                    Save();
                }
            }
        }
    }
}