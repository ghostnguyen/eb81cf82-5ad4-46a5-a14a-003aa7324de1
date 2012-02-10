using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public class GeoRepository : BaseRepository<GeoRepository>
    {
        public Geo GetByFullname(string fullname)
        {
            return (from e in DB.Geos
                    where e.FullName.ToLower() == fullname.Trim().ToLower()
                    select e).SingleOrDefault();
        }

        public Geo Get(Guid? ID = null)
        {
            Geo e = DB.Geos.Where(r => r.ID == ID).SingleOrDefault();

            return e;
        }

        public string GetName(Guid? ID = null)
        {
            Geo e = DB.Geos.Where(r => r.ID == ID).SingleOrDefault();
            return e == null ? "" : e.Name;
        }

        public IQueryable<Geo> GetByParentID(Guid? parentID = null)
        {
            return parentID.HasValue ?
                DB.Geos.Where(r => r.ParentID == parentID)
                : DB.Geos.Where(r => r.ParentID == null);
        }

        public Geo Add(Action<Geo> updateMethod)
        {
            Geo e = new Geo();

            e.ID = Guid.NewGuid();
            updateMethod(e);

            e.Name = e.Name.Trim();

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
            v.Name = v.Name.Trim();

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