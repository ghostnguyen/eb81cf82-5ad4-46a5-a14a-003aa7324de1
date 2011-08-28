using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public class QuoteDetailRepository : BaseRepository
    {
        public QuoteDetail Get(int id)
        {
            return DB.QuoteDetails.Single(r => r.ID == id);
        }

        public IQueryable<QuoteDetail> GetAll()
        {
            return DB.QuoteDetails;
        }

        public int? Delete(int id)
        {
            QuoteDetail e = Get(id);

            int? quoteID = e.QuoteID;

          

            DB.DeleteObject(e);
            Save();

            return quoteID;
        }
        
       

        public void Update(int ID, Action<QuoteDetail> updateMethod)
        {
            QuoteDetail e = Get(ID);
            updateMethod(e);
            Save();
        }



        
    }
}