using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public class BaseRepository<T> where T : new()
    {
        private OAMSEntities _db = new OAMSEntities();
        public OAMSEntities DB
        {
            get { return _db; }
            set { _db = value; }
        }

        static public T I = new T();


        public int Save()
        {
            return DB.SaveChanges();
        }
    }
}