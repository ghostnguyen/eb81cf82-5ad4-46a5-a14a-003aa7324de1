using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public class QuoteDetailFeeRepository : BaseRepository
    {
        public IQueryable<QuoteDetailFee> Get(int quoteDetailID)
        {
            return DB.QuoteDetailFees.Where(r => r.QuoteDetailID == quoteDetailID);
        }

        public void Save(List<QuoteDetailFee> l)
        {
            if (l.GroupBy(r => r.QuoteDetailID).Count() != 1)
            {
                throw new Exception("List<QuoteDetailFee> Error");
            }

            int? quoteDetailID = l.First().QuoteDetailID;

            var oldIDL = l.Where(r => r.ID > 0).Select(r => r.ID);
            var deleteL = DB.QuoteDetailFees.Where(r => r.QuoteDetailID == quoteDetailID && !oldIDL.Contains(r.ID));
            foreach (var item in deleteL)
            {
                DB.DeleteObject(item);
            }

            Save();
            var newL = l.Where(r => r.ID == 0);

            foreach (var item in newL)
            {
                DB.QuoteDetailFees.AddObject(item);
            }
            Save();

        }
    }
}