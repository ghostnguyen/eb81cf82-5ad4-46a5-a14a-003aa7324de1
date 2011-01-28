using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public class BaseRepository 
    {
        private OAMSEntities _db = new OAMSEntities();
        public OAMSEntities DB
        {
            get { return _db; }
            set { _db = value; }
        }

        public int Save()
        {
            return DB.SaveChanges();
        }
    }
}