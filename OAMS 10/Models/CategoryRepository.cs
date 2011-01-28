using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public class CategoryRepository : BaseRepository
    {
        //private OAMSEntities DB = new OAMSEntities();

        public Category GetByFullname(string fullname)
        {
            return (from e in DB.Categories
                    where e.FullName.ToLower() == fullname.Trim().ToLower()
                    select e).SingleOrDefault();
        }

        public Category Get(Guid? ID = null)
        {
            Category e = DB.Categories.Where(r => r.ID == ID).SingleOrDefault();

            return e;
        }

        public IQueryable<Category> GetByParentID(Guid? parentID = null)
        {
            return parentID.HasValue ?
                DB.Categories.Where(r => r.ParentID == parentID)
                : DB.Categories.Where(r => r.ParentID == null);
        }

        // Add/Delete 
        //public Category Add(string name, int level, Guid? parentID)
        //{
        //    Category Category = new Category();
        //    Category.Name = name.Trim();
        //    Category.Level = level;
        //    Category.ParentID = parentID;

        //    DB.Categories.AddObject(Category);

        //    return Category;
        //}

        public Category Add(Category e)
        {
            e.ID = Guid.NewGuid();
            DB.Categories.AddObject(e);
            e.Level = e.Parent == null ? 1 : e.Parent.Level + 1;
            //SetFullname(e);
            //e.FullNameNoDiacritics = e.FullName.RemoveDiacritics();
            UpdateFullname(e);
            
            Save();
            return e;
        }

        public void UpdateFullname(Category e)
        {
            SetFullname(e);
            e.FullNameNoDiacritics = e.FullName.RemoveDiacritics();
        }

        public void Delete(Category e)
        {
            DB.Categories.DeleteObject(e);
        }

        //public void Add(string Category1Name, string Category2Name, string Category3Name)
        //{
        //    //Category1
        //    if (string.IsNullOrEmpty(Category1Name)
        //        || string.IsNullOrEmpty(Category1Name.Trim())) return;

        //    Category Category1 = Get(Category1Name, 1, null);

        //    if (Category1 == null)
        //        Category1 = Add(Category1Name, 1, null);

        //    //Category2
        //    if (string.IsNullOrEmpty(Category2Name)
        //        || string.IsNullOrEmpty(Category2Name.Trim())) return;

        //    Category Category2 = Get(Category2Name, 2, Category1ID);
        //    Guid? Category2ID = Category2 != null ? Category2.ID : Insert(Category2Name, 2, Category1ID);

        //    //Category3
        //    if (string.IsNullOrEmpty(Category3Name)
        //        || string.IsNullOrEmpty(Category3Name.Trim())) return;

        //    Category Category3 = Get(Category3Name, 3, Category2ID);
        //    Guid? Category3ID = Category3 != null ? Category3.ID : Insert(Category3Name, 3, Category2ID);
        //}

        // Persistence 
       

        public static void SetFullname(Category e)
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

        public string GetFullname(Category Category1, Category Category2, Category Category3)
        {
            return Category3 != null ? Category3.FullName : Category2 != null ? Category2.FullName : Category1 != null ? Category1.FullName : "";
        }

        //public static string GetFullAddress(string address, string fullCategory)
        //{
        //    return (address + ", " + fullCategory).Trim(',', ' ');
        //}

        public void Set3LevelByFullname(string fullname, Func<Guid?, Guid?, Guid?, int> setCategoryFunc)
        {

            if (string.IsNullOrEmpty(fullname)
                       || string.IsNullOrEmpty(fullname.Trim()))
            {
                setCategoryFunc(null, null, null);
            }
            else
            {
                Category g = GetByFullname(fullname);
                if (g == null)
                {
                    throw new Exception("Nhập sai");
                }
                else
                {
                    Guid? Category1ID = null;
                    Guid? Category2ID = null;
                    Guid? Category3ID = null;

                    if (g.Level == 1)
                    {
                        Category1ID = g.ID;
                    }

                    if (g.Level == 2)
                    {
                        Category2ID = g.ID;
                        Category1ID = g.Parent.ID;
                    }

                    if (g.Level == 3)
                    {
                        Category3ID = g.ID;
                        Category2ID = g.Parent.ID;
                        Category1ID = g.Parent.Parent.ID;
                    }

                    setCategoryFunc(Category1ID, Category2ID, Category3ID);
                }
            }
        }
    }
}