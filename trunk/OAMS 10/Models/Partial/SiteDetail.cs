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
        public string ToStringClient { get { return string.Join(",", SiteDetailMores.Select(r1 => r1.Product == null ? "" : (r1.Product.Client == null ? "" : (r1.Product.Client.Name ?? "")))); } }
        public string ToStringProduct { get { return string.Join(",", SiteDetailMores.Select(r1 => r1.Product == null ? "" : (r1.Product.Name ?? ""))); } }
        public string ToStringCategoryLevel1 { get { return string.Join(",", SiteDetailMores.Select(r => r.Product == null ? "" : (r.Product.Category1 != null ? r.Product.Category1.Name : ""))); } }
        public string ToStringCategoryLevel2 { get { return string.Join(",", SiteDetailMores.Select(r => r.Product == null ? "" : (r.Product.Category2 != null ? r.Product.Category2.Name : ""))); } }


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