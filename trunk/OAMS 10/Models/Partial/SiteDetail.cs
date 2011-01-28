using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAMS.Models;
using System.ComponentModel.DataAnnotations;

namespace OAMS.Models
{

    public partial class SiteDetail : IEquatable<SiteDetail>
    {
        //public string CategoryFullName
        //{
        //    get
        //    {
        //        CategoryRepository catRepository = new CategoryRepository();
        //        return catRepository.GetFullname(Category1, Category2, Category3);
        //    }
        //}

        //public string NewCategoryFullName
        //{
        //    get;
        //    set;
        //}

        public string CurrentProductName
        {
            get
            {
                return Product != null ? Product.Name : "";
            }
        }



        //public int UpdateCategory(Guid? catID1, Guid? catID2, Guid? catID3)
        //{
        //    CategoryID1 = catID1;
        //    CategoryID2 = catID2;
        //    CategoryID3 = catID3;

        //    return 0;
        //}

        public bool Equals(SiteDetail other)
        {

            //Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal.
            return ID.Equals(other.ID);
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public override int GetHashCode()
        {
            //Get hash code for the Code field.
            int hashProductCode = ID.GetHashCode();

            //Calculate the hash code for the product.
            return hashProductCode;
        }
    }


}