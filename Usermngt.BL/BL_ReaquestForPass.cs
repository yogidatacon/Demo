using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_ReaquestForPass
    {
        public static string Insert(ReaquestForPass pass)
        {
            return DL_ReaquestForPass.Insert(pass);
        }
        public static string Update(ReaquestForPass pass)
        {
            return DL_ReaquestForPass.Update(pass);
        }

        public static List<ReaquestForPass> GetList(string user_id)
        {
            return DL_ReaquestForPass.GetList(user_id);
        }
        public static List<ReaquestForPass> Search(string tablename, string column, string value)
        {
            return DL_ReaquestForPass.Search(tablename, column, value);
        }
        public static ReaquestForPass GetRequest(string requestid, string pass_type,string party_code,string party_type,string financial_year)
        {
            return DL_ReaquestForPass.GetRequest(requestid, pass_type,party_code, party_type,financial_year);
        }

        public static ReaquestForPass GetDetails(string requestid,string pass_type,string party_code,string financial_year)
        {
            return DL_ReaquestForPass.GetDetails(requestid, pass_type, party_code,financial_year);
        }

        public static string Approve(ReaquestForPass pass)
        {
            return DL_ReaquestForPass.Approve(pass);
        }

        public static string GetBalance(string id, string pass_type,string financial_year)
        {
            return DL_ReaquestForPass.GetBalance(id, pass_type,financial_year);
        }

        public static List<ReaquestForPass> GetNOCList(string user_id)
        {
            return DL_ReaquestForPass.GetNOCList(user_id);
        }
        public static List<ReaquestForPass> Search1(string tablename, string column, string value)
        {
            return DL_ReaquestForPass.Search1(tablename, column, value);
        }
        }
}
