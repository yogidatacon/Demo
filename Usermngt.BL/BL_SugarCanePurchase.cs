using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_SugarCanePurchase
    {
        public static string Insert(SugarCanePurchase scp)
        {
            return DL_SugarCanePurchase.Insert(scp);

        }
        public static string Update(SugarCanePurchase scp)
        {
            return DL_SugarCanePurchase.Update(scp);
        }

        public static List<SugarCanePurchase> GetList()
        {
            return DL_SugarCanePurchase.GetList();
        }
        public static List<SugarCanePurchase> Search(string tablename, string column, string value)
        {
            return DL_SugarCanePurchase.Search(tablename, column, value);
        }
        public static SugarCanePurchase GetDetails(int scbid,string financial_year)
        {
            return DL_SugarCanePurchase.GetDetails(scbid,financial_year);
        }

        public static string Approve(SugarCanePurchase scp)
        {
            return DL_SugarCanePurchase.Approve(scp);
        }

        public static string GetPedning(string party_code, string financial_year)
        {
            return DL_SugarCanePurchase.GetPedning(party_code,financial_year);
        }
    }
}
