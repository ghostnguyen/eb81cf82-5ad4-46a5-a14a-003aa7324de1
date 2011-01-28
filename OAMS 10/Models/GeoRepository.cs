using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public class GeoRepository : BaseRepository
    {
        //private OAMSEntities DB = new OAMSEntities();

        // Query Methods 
        //public IQueryable<Geo> GetAll() { return DB.Geos; }
        //public IQueryable<Geo> GetAll(int level) { return DB.Geos.Where(r => r.Level == level); }

        //public Geo Get(string name, int level, Guid? parentID)
        //{
        //    return DB.Geos.Where(r => r.Level == level
        //        && r.Name.Trim().ToLower() == name.Trim().ToLower()
        //        && object.Equals(r.ParentID, parentID)
        //        ).SingleOrDefault();
        //}

        public static string HCMC_Name
        {
            get
            {
                OAMS.Models.GeoRepository geoRepository = new OAMS.Models.GeoRepository();
                var hcmc = geoRepository.Get(OAMSSetting.HCMC_ID);
                string name = hcmc == null ? "" : hcmc.Name;

                return name;
            }
        }

        public Geo GetByFullname(string fullname)
        {
            return (from e in DB.Geos
                    where e.FullName.ToLower() == fullname.Trim().ToLower()
                    select e).SingleOrDefault();
        }

        //public IQueryable<Geo> Get(List<Guid> IDList, int level)
        //{
        //    return DB.Geos.Where(r => IDList.Contains(r.ID) && r.Level == level);
        //}

        //public Geo Get(Guid ID, int level)
        //{
        //    Geo e = DB.Geos.Where(r => r.ID == ID && r.Level == level).SingleOrDefault();

        //    if (e == null)
        //        throw new Exception("Không tìm thấy đơn vị hành chính.");

        //    return e;
        //}

        public Geo Get(Guid? ID = null)
        {
            Geo e = DB.Geos.Where(r => r.ID == ID).SingleOrDefault();

            return e;
        }

        public IQueryable<Geo> GetByParentID(Guid? parentID = null)
        {
            return parentID.HasValue ?
                DB.Geos.Where(r => r.ParentID == parentID)
                : DB.Geos.Where(r => r.ParentID == null);
        }

        // Add/Delete 
        //public Geo Add(string name, int level, Guid? parentID)
        //{
        //    Geo geo = new Geo();
        //    geo.Name = name.Trim();
        //    geo.Level = level;
        //    geo.ParentID = parentID;

        //    DB.Geos.AddObject(geo);

        //    return geo;
        //}

        public Geo Add(Action<Geo> updateMethod)
        {
            Geo e = new Geo();

            e.ID = Guid.NewGuid();
            updateMethod(e);

            DB.Geos.AddObject(e);

            e.Level = e.Parent == null ? 1 : e.Parent.Level + 1;
            
            UpdateFullnameRecursive(e);

            Save();

            return e;
        }

        public Geo Update(Guid ID, Action<Geo> updateMethod)
        {
            var v = Get(ID);

            updateMethod(v);

            UpdateFullnameRecursive(v);

            Save();

            return v;
        }


        public void UpdateFullnameRecursive(Geo e)
        {
            SetFullname(e);
            e.FullNameNoDiacritics = e.FullName.RemoveDiacritics();

            foreach (var item in e.Children)
            {
                UpdateFullnameRecursive(item);
            }
        }

        public void Delete(Geo e)
        {
            DB.Geos.DeleteObject(e);
        }

        //public void Add(string geo1Name, string geo2Name, string geo3Name)
        //{
        //    //Geo1
        //    if (string.IsNullOrEmpty(geo1Name)
        //        || string.IsNullOrEmpty(geo1Name.Trim())) return;

        //    Geo geo1 = Get(geo1Name, 1, null);

        //    if (geo1 == null)
        //        geo1 = Add(geo1Name, 1, null);

        //    //Geo2
        //    if (string.IsNullOrEmpty(geo2Name)
        //        || string.IsNullOrEmpty(geo2Name.Trim())) return;

        //    Geo geo2 = Get(geo2Name, 2, geo1ID);
        //    Guid? geo2ID = geo2 != null ? geo2.ID : Insert(geo2Name, 2, geo1ID);

        //    //Geo3
        //    if (string.IsNullOrEmpty(geo3Name)
        //        || string.IsNullOrEmpty(geo3Name.Trim())) return;

        //    Geo geo3 = Get(geo3Name, 3, geo2ID);
        //    Guid? geo3ID = geo3 != null ? geo3.ID : Insert(geo3Name, 3, geo2ID);
        //}

        // Persistence 
      

        public static void SetFullname(Geo e)
        {
            if (e.Level == 1)
            {
                e.FullName = e.Name;
            }

            if (e.Level == 2)
            {
                e.FullName = e.Name + ", " + e.Parent.Name;
            }

            if (e.Level == 3)
            {
                e.FullName = e.Name + ", " + e.Parent.Name + ", " + e.Parent.Parent.Name;
            }
        }

        public string GetFullname(Geo geo1, Geo geo2, Geo geo3)
        {
            return geo3 != null ? geo3.FullName : geo2 != null ? geo2.FullName : geo1 != null ? geo1.FullName : "";
        }

        //public static string GetFullAddress(string address, string fullGeo)
        //{
        //    return (address + ", " + fullGeo).Trim(',', ' ');
        //}

        public void Set3LevelByFullname(string fullname, Func<Guid?, Guid?, Guid?, int> setGeoFunc)
        {

            if (string.IsNullOrEmpty(fullname)
                       || string.IsNullOrEmpty(fullname.Trim()))
            {
                //throw new Exception("Nhập đơn vị hành chính.");
            }
            else
            {
                Geo g = GetByFullname(fullname);
                if (g == null)
                {
                    throw new Exception("Nhập sai đơn vị hành chính.");
                }
                else
                {
                    Guid? geo1ID = null;
                    Guid? geo2ID = null;
                    Guid? geo3ID = null;

                    if (g.Level == 1)
                    {
                        geo1ID = g.ID;
                    }

                    if (g.Level == 2)
                    {
                        geo2ID = g.ID;
                        geo1ID = g.Parent.ID;
                    }

                    if (g.Level == 3)
                    {
                        geo3ID = g.ID;
                        geo2ID = g.Parent.ID;
                        geo1ID = g.Parent.Parent.ID;
                    }

                    setGeoFunc(geo1ID, geo2ID, geo3ID);
                }
            }
        }
    }
}