using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
   public class BL_LicenseType
    {

        public static List<LicenseType> GetList(string userid)
        {
            return DL_LicenseType.GetList(userid);
        }

        public static List<LicenseType> SearchLicense(string tablename, string column, string value)
        {
            return DL_LicenseType.SearchLicense(tablename, column, value);
        }
        public static string Insert(LicenseType om)
        {
            return DL_LicenseType.Insert(om);
        }

        public static string Update(LicenseType om)
        {
            return DL_LicenseType.Update(om);
        }

        }
}
