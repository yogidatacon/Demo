using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
   public class BL_SystemSetting
    {
        public static List<SystemSetting> GetList(string userid)
        {
            return DL_SystemSetting.GetList(userid);
        }
        public static bool InsertSys(SystemSetting product)
        {
            return DL_SystemSetting.InsertSys(product);
        }
        public static bool UpdateSys(SystemSetting product)
        {
            return DL_SystemSetting.UpdateSys(product);
        }
        public static List<SystemSetting> Searchsys(string tablename, string column, string value)
        {
            return DL_SystemSetting.Searchsys(tablename, column, value);
        }
    }
}
