using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public class SitePhotoRepository : BaseRepository
    {
        public void EditNote(int id, string note)
        {
            var v = DB.SitePhotoes.SingleOrDefault(r => r.ID == id);
            if (v != null)
            {
                v.Note = note;
                DB.SaveChanges();
            }
        }
    }
}