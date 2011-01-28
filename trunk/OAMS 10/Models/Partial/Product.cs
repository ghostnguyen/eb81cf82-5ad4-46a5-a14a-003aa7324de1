using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAMS.Models;
using System.ComponentModel.DataAnnotations;

namespace OAMS.Models
{

    public partial class Product
    {
        public string CategoryFullName
        {
            get
            {
                CategoryRepository catRepository = new CategoryRepository();
                return catRepository.GetFullname(Category1, Category2, Category3);
            }
        }

        public string NewCategoryFullName
        {
            get;
            set;
        }

        public string NewClientName
        {
            get
            {
                return Client != null ? Client.Name : "";
            }
        }

        public int UpdateCategory(Guid? catID1, Guid? catID2, Guid? catID3)
        {
            CategoryID1 = catID1;
            CategoryID2 = catID2;
            CategoryID3 = catID3;

            return 0;
        }
    }
}