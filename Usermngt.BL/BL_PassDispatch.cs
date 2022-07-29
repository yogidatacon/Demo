using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
   public class BL_PassDispatch
    {
        public static string Insert(Pass_Details pass)
        {
            return DL_PassDispatch.Insert(pass);
        }

        public static string Update(Pass_Details pass)
        {
            return DL_PassDispatch.Update(pass);
        }

        public static List<Pass_Details> GetPassList()
        {
            return DL_PassDispatch.GetPassList();
        }
        public static List<Pass_Details> Search(string tablename, string column, string value)
        {
            return DL_PassDispatch.Search(tablename, column, value);
        }
        public static Pass_Details GetDetails(string pass_id, string pass_type,string financial_year)
        {
            return DL_PassDispatch.GetDetails(pass_id,financial_year);
        }
        public static string Issue(Pass_Details pass)
        {
            return DL_PassDispatch.Issue(pass);
        }

        public static string GetAvailableQTY(string vat,string financial_year)
        {
            return DL_PassDispatch.GetAvailableQTY(vat,financial_year);
        }

        public static Pass_Details GetPassDetails(string passno)
        {
            return DL_PassDispatch.GetPassDetails(passno);
        }
    }
}
