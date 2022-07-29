using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
   public class BL_ProductType
    {
        public static List<ProductType> GetProductList(string userid)
        {
            return DL_ProductType.GetProductType(userid);
        }
        public static bool InsertProductType(ProductType product)
        {
            return DL_ProductType.InsertProduct(product);
        }
        public static bool InsertDept(Department product)
        {
            return DL_ProductType.InsertDept(product);
        }
        public static List<ProductType> SearchProductType(string tablename, string column, string value)
        {
            return DL_ProductType.SearchProductType(tablename, column, value);
        }
        public static bool UpdateProduct(ProductType product)
        {
            return DL_ProductType.UpdateProduct(product);
        }

        public static bool UpdateDept(Department product)
        {
            return DL_ProductType.UpdateDept(product);
        }
        }
}
