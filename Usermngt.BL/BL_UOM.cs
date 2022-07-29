using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public  class BL_UOM
    {
        public static List<UOM_Master> GetList(string userid)
        {
            return DL_UOM.GetList(userid);
        }
        public static string Insert(UOM_Master uom)
        {
            return DL_UOM.Insert(uom);
        }
        public static List<UOM_Master> SearchUOM(string tablename, string column, string value)
        {
            return DL_UOM.SearchUOM(tablename, column, value);
        }
        public static string Update(UOM_Master uom)
        {
            return DL_UOM.Update(uom);
        }
    }
}
