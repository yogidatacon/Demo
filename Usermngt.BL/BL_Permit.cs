using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
  public  class BL_Permit
    {

        public static UserDetails CheckUser(string userid)
        {
            return DL_Permit.CheckUser(userid);
        }
        public static Permit Getvalue(string id, string financial_year)
        {
            return DL_Permit.Getvalue(id,financial_year);
        }

        public static List<UserDetails> Check()
        {
            return DL_Permit.Check();
        }
        public static string Approve(Permit record)
        {
            return DL_Permit.Approve(record);
        }

        public static Permit GetDetails(int permit_id, string financial_year)
        {
            return DL_Permit.GetDetails(permit_id,financial_year);
        }
        public static Permit Gettransfordetails(int permit_id ,string financial_year, string party)
        {
            return DL_Permit.Gettransfordetails(permit_id,financial_year,party);
        }
        public static string Update(Permit from)
        {
            return DL_Permit.Update(from);
        }
        public static string GetPartyMax(string party_code,string district,string financial_year)
        {
            return DL_Permit.GetPartyMax(party_code,district, financial_year);
        }
        public static List<Permit> GetList()
        {
            return DL_Permit.GetList();
        }
        public static List<Permit> Gettrasforpass()
        {
            return DL_Permit.Gettrasforpass();
        }


        public static string Insert(Permit from)
        {
            return DL_Permit.Insert(from);
        }

       
    }
}
