using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
   public class BL_org_Master
    {
        public static List<Org_Master> GetListValues(string userid)
        {
            return DL_Org_List.GetListValues(userid);
        }

        public static Org_Master GetOrg_Details(string org_id)
        {
            return DL_Org_List.GetOrg_Details(org_id);
        }
        public static bool UpdateOrg_Details(Org_Master org)
        {
            return DL_Org_List.UpdateOrg_Details(org);
        }
        public static List<Org_Master> SearchOrg(string tablename, string column, string value)
        {
            return DL_Org_List.SearchOrg(tablename, column, value);
        }
        public static List<Org_Finacial_yr> SearchFinacialYear(string tablename, string column, string value)
        {
            return DL_Org_List.SearchFinacialYear(tablename, column, value);
        }
        public static List<Org_Finacial_yr> GetFinacListValues(string userid)
        {
            return DL_Org_List.GetFinacListValues(userid);
        }
        public static List<Org_Finacial_yr> GetOrgnames(string userid)
        {
            return DL_Org_List.GetOrgnames(userid);
        }

        public static bool InsertOrg_Details(Org_Master org)
        {
            return DL_Org_List.InsertOrg_Details(org);
        }

        public static string GetMaxID(string tableName)
        {
            return DL_Org_List.GetMaxID(tableName);
        }

        public static bool UpdateOrgFinance_Details(Org_Finacial_yr org1)
        {
            return DL_Org_List.UpdateOrgFinance(org1);
        }

        public static bool InsertOrgFinance_Details(Org_Finacial_yr org1)
        {
            return DL_Org_List.InsertOrgFinance(org1);
        }

       

       
    }
}
