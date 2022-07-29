using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_NOC_Details
    {
        public static string GetMax()
        {
            return DL_NOC_Details.GetMax();
        }

        public static string Insert(NOC_Details noc)
        {
            return DL_NOC_Details.Insert(noc);
        }

        public static string Update(NOC_Details noc)
        {
            return DL_NOC_Details.Update(noc);
        }

        public static List<NOC_Details> GetNOCList()
        {
            return DL_NOC_Details.GetNOCList();
        }
        public static List<NOC> GetNOCList1()
        {
            return DL_NOC_Details.GetNOCList1();
        }
        public static List<NOC> Search(string tablename, string column, string value)
        {
            return DL_NOC_Details.Search(tablename, column, value);
        }
        public static NOC_Details GetDetails(string noc_id,string financial_year,string party_code)
        {
            return DL_NOC_Details.GetDetails(noc_id,financial_year,party_code);
        }

        public static string Approve(NOC_Details noc)
        {
            return DL_NOC_Details.Approve(noc);
        }
        public static string AdminUpdate(NOC_Details noc)
        {
            return DL_NOC_Details.AdminUpdate(noc);
        }

        public static string GetPartyMax(string party_code,string financial_year)
        {
            return DL_NOC_Details.GetPartyMax(party_code,financial_year);
        }
    }
}
