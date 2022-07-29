using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
   public class BL_RawMaterial
    {
        public static List<RawMaterial> GetRawMaterialList(string userid)
        {
            return DL_RawMaterial.GetRawMaterial(userid);
        }

        public static List<RawMaterial> GetRawMaterial(string userid)
        {
            return DL_RawMaterial.GetRawMateriallist(userid);
        }

        public static bool InsertRawMaterial(RawMaterial rawmaterial)
        {
            return DL_RawMaterial.InsertRawMaterial(rawmaterial);
        }
        public static List<RawMaterial> SearchRawMaterial(string tablename, string column, string value)
        {
            return DL_RawMaterial.SearchRawMaterial(tablename, column, value);
        }
        public static bool UpdateRawMaterial(RawMaterial rawmaterial)
        {
            return DL_RawMaterial.UpdateRawMaterial(rawmaterial);
        }
    }
}
