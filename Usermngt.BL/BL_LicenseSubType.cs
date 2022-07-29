using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
   public class BL_LicenseSubType
    {
        public static List<LicenseSubType> GetList(string userid)
        {
            return DL_LicenseSubType.GetList(userid);
        }

        public static List<LicenseSubType> SearchLicense(string tablename, string column, string value)
        {
            return DL_LicenseSubType.SearchLicense(tablename, column, value);
        }

        public static string Insert(LicenseSubType om)
        {
            return DL_LicenseSubType.Insert(om);
        }
        public static string Update(LicenseSubType om)
        {
            return DL_LicenseSubType.Update(om);
        }
        }
    }
