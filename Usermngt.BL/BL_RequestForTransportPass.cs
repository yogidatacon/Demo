using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
  public  class BL_RequestForTransportPass
    {
        public static string Insert(ReaquestForPass pass)
        {
            return DL_RequestForTransportPass.Insert(pass);
        }
        public static string Update(ReaquestForPass pass)
        {
            return DL_RequestForTransportPass.Update(pass);
        }
        public static List<NOC_Details> GetNOCList1(string pass_type)
        {
            return DL_RequestForTransportPass.GetNOCList1(pass_type);
        }
        public static List<ReaquestForPass> GetList(string user_id)
        {
            return DL_RequestForTransportPass.GetList(user_id);
        }

        public static ReaquestForPass GetRequest(string requestid, string pass_type, string party_code, string party_type,string financial_year)
        {
            return DL_RequestForTransportPass.GetRequest(requestid, pass_type, party_code, party_type,financial_year);
        }

        public static ReaquestForPass GetDetails(string requestid, string pass_type,string financial_year)
        {
            return DL_RequestForTransportPass.GetDetails(requestid, pass_type,financial_year);
        }

        public static string Approve(ReaquestForPass pass)
        {
            return DL_RequestForTransportPass.Approve(pass);
        }

        public static string GetBalance(string id, string pass_type)
        {
            return DL_RequestForTransportPass.GetBalance(id, pass_type);
        }

        public static List<ReaquestForPass> GetNOCList(string user_id, string party_type)
        {
            return DL_RequestForTransportPass.GetNOCList(user_id,party_type);
        }
        public static List<ReaquestForPass> Searchena(string tablename, string column, string value, string passtype)
        {
            return DL_RequestForTransportPass.Searchena(tablename, column, value, passtype);
        }

        public static List<ReaquestForPass> GetpermitList(string user_id, string party_type)
        {
            return DL_RequestForTransportPass.GetpermitList(user_id, party_type);
        }
        public static List<ReaquestForPass> Searchpermit(string tablename, string column, string value, string passtype)
        {

            return DL_RequestForTransportPass.Searchpermit(tablename, column, value, passtype);
        }
    }
    }
