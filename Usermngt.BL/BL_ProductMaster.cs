using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
  public  class BL_ProductMaster
    {
        public static List<Product_Master> GetProductMasterList(string userid)
        {
            return DL_ProductMaster.GetProductMaster(userid);
        }
        public static List<Product_Master> GetProductMasterpartyList(string party_code)
        {
            return DL_ProductMaster.GetProductMasterList(party_code);
        }
        public static List<Product_Master> SearchProduct(string tablename, string column, string value)
        {
            return DL_ProductMaster.SearchProduct(tablename, column, value);
        }
        public static bool InsertProductMaster(Product_Master product)
        {
            return DL_ProductMaster.InsertProductMaster(product);
        }
        public static bool UpdateProductMaster(Product_Master product)
        {
            return DL_ProductMaster.UpdateProductMaster(product);
        }
    }
}
