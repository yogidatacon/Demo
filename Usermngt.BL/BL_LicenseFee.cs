using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
   public class BL_LicenseFee
    {
        public static List<LicenseFee> GetList(string userid)
        {
            return DL_LicenseFee.GetList(userid);
        }

        public static List<LicenseFee> SearchLicense(string tablename, string column, string value)
        {
            return DL_LicenseFee.SearchLicense(tablename, column, value);
        }

        public static string Insert(LicenseFee om)
        {
            return DL_LicenseFee.Insert(om);
        }
        public static string Update(LicenseFee om)
        {
            return DL_LicenseFee.Update(om);
        }
        public static LicenseFee GetDetails(int master_id)
        {
            return DL_LicenseFee.GetDetails(master_id);
        }
        }
}
