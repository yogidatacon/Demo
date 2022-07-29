using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
   public class BL_Thana
    {

        public static List<ThanaMaster> GetThanaList(string userid)
        {
            return DL_Thana.GetThana(userid);
        }
        public static bool InsertThana(ThanaMaster thana)
        {
            return DL_Thana.Insetthana(thana);
        }
        public static List<ThanaMaster> GetThana1(string district)
        {
            return DL_Thana.GetThana1(district);
        }
        public static bool UpdateThana(ThanaMaster thana)
        {
            return DL_Thana.UpdateThana(thana);
        }
        public static List<ThanaMaster> SearchThana(string tablename, string column, string value)
        {
            return DL_Thana.SearchThana(tablename, column, value);
        }
        public static int GetMax()
        {
            return DL_Thana.GetMax();
        }
    }
}
