using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public class SiteDetailPhotoRepository : BaseRepository
    {
        public void EditNote(int id, string note)
        {
            var v = DB.SiteDetailPhotoes.SingleOrDefault(r => r.ID == id);
            if (v != null)
            {
                v.Note = note;
                DB.SaveChanges();
            }
        }
    }
}