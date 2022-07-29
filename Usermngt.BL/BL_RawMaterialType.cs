using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
 public   class BL_RawMaterialType
    {
        public static List<RawMaterialType> GetRawMaterialList(string userid)
        {
            return DL_RawMaterialType.GetRawMaterial(userid);
        }
        public static List<RawMaterialType> SearchRawMaterialType(string tablename, string column, string value)
        {
            return DL_RawMaterialType.SearchRawMaterialType(tablename, column, value);
        }
        public static bool InsertRawMaterial(RawMaterialType rawmaterial)
        {
            return DL_RawMaterialType.InsertRawMaterial(rawmaterial);
        }

        public static bool UpdateRawMaterial(RawMaterialType rawmaterial)
        {
            return DL_RawMaterialType.UpdateRawMaterial(rawmaterial);
        }
    }
}
