using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_Financial_Years
    {
        public static List<Financial_Years> GetFinacListValues()
        {
            return DL_Financial_Years.GetFinacListValues();
        }
        public static List<Financial_Years> Search(string tablename, string column, string value)
        {
            return DL_Financial_Years.SearchPartyType(tablename, column, value);
        }

        public static string Insert(Financial_Years fin)
        {
            return DL_Financial_Years.Insert(fin);
        }

        public static string Update(Financial_Years fin)
        {
            return DL_Financial_Years.Update(fin);
        }
      
        public static Financial_Years GetDetails(string fin_id)
        {
            return DL_Financial_Years.GetDetails(fin_id);
        }
    }
}
