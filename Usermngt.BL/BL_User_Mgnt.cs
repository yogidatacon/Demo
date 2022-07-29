using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_User_Mgnt
    {
        public static List<State> GetListValues(string userid)
        {
            return DL_User_Mngnt.GetStateList(userid);
        }
        public static string InsertState(State state)
        {
            return DL_User_Mngnt.InserState(state);
        }
        public static List<District> GetAllDistrictsList()
        {
            return DL_User_Mngnt.GetAllDistricts();
        }
        public static List<cm_court> GetDistrictsCourtList(string distid)
        {
            return DL_User_Mngnt.GetDistrictCourtList(distid);
        }
        public static cm_court GetDMCONDetails(int dmcase_registration_id)
        {
            return DL_cm_court.GetDMCONDetails(dmcase_registration_id);
        }
        public static List<cm_court> GetAllDistrictsListPR(string distid)
        {
            return DL_User_Mngnt.GetAllDistrictsPR(distid);
        }
        public static List<State> SearchState(string tablename, string column, string value)
        {
            return DL_User_Mngnt.SearchState(tablename, column, value);
        }
        public static bool InsertDivision(Division division)
        {
            return DL_User_Mngnt.InserDivision(division);
        }
        public static List<Division> SearchDivision(string tablename, string column, string value)
        {
            return DL_User_Mngnt.SearchDivision(tablename, column, value);
        }
        public static List<RoleMaster> GetRoleMasterList(string userid)
        {
            return DL_User_Mngnt.GetRoleMasterList(userid);
        }


        public static List<AccessType> GetAccessTypeList(string userid)
        {
            return DL_User_Mngnt.GetAccessTypeList(userid);
        }
        public static List<AccessType> SearchExistsData(string tablename, string column, string value)
        {
            return DL_User_Mngnt.SearchExistsData(tablename, column, value);
        }
        public static List<District> SearchDistrictData(string tablename, string column, string value)
        {
            return DL_User_Mngnt.SearchDistrictData(tablename, column, value);
        }
        public static List<RoleLevel> GetRoleLevels(string userid)
        {
            return DL_User_Mngnt.GetRoleLevels(userid);
        }

        public static List<CustomerDetails> GetCustomers(string party_code)
        {
            return DL_User_Mngnt.GetCustomers(party_code);
        }
        public static List<CustomerDetails> Search(string tablename, string column, string value)
        {
            return DL_User_Mngnt.Search(tablename, column, value);
        }
        public static List<District> GetDistricts(string userid)
        {
            return DL_User_Mngnt.GetDistrictList(userid);
        }
        public static List<Division> GetDivisions(string userid)
        {
            return DL_User_Mngnt.GetDivisionList(userid);
        }

        public static int GetExistsData(string tablename,string column,string value)
        {
            return DL_User_Mngnt.GetExistsData(tablename, column, value);
        }

        public static CustomerDetails GetCustomerDetails(string cutid)
        {
            return DL_User_Mngnt.GetCustomerDetails(cutid);
        }

        public static bool UpdatetDivision(Division division)
        {
            return DL_User_Mngnt.UpdateDivision(division);
        }
        public static bool UpdateRoleMaster(RoleMaster rolemaster)
        {
            return DL_User_Mngnt.UpdateRoleMaster(rolemaster);
        }
        public static bool InsertDistrict(District district)
        {
            return DL_User_Mngnt.InserDistrict(district);
        }

        public static string InsertCust(CustomerDetails cust)
        {
            return DL_User_Mngnt.InsertCust(cust);
        }

        public static bool InsertRoleMaster(RoleMaster rolemaster)
        {
            return DL_User_Mngnt.InsertRoleMaster(rolemaster);
        }
        public static List<RoleMaster> SearchRoleMaster(string tablename, string column, string value)
        {
            return DL_User_Mngnt.SearchRoleMaster(tablename, column, value);
        }
        public static string UpdatetDistrict(District district)
        {
            return DL_User_Mngnt.UpdateDistrict(district);
        }

        public static bool InsertRoleLevel(RoleLevel rolelevel)
        {
            return DL_User_Mngnt.InserRoleLevel(rolelevel);
        }

        public static bool UpdateRoleLevel(RoleLevel rolelevel)
        {
            return DL_User_Mngnt.UpdateRoleLevel(rolelevel);
        }

        public static bool InsertAccessType(AccessType accesstype)
        {
            return DL_User_Mngnt.InserAccessType(accesstype);
        }

        public static bool UpdateAccessType(AccessType accesstype)
        {
            return DL_User_Mngnt.UpdateAccessType(accesstype);
        }

        public static List<Department> GetDeptList(string userid)
        {
            return DL_User_Mngnt.GetDeptList(userid);
        }
        public static List<RoleLevel> SearchroleLevel(string tablename, string column, string value)
        {
            return DL_User_Mngnt.SearchroleLevel(tablename, column, value);
        }
        public static List<Thana_Details> GetThanaList(string user_id)
        {
            return DL_User_Mngnt.GetThanaList(user_id);
        }

        public static bool Deletefile(string v1, string v2, string v3)
        {
            return DL_User_Mngnt.Deletefile(v1,v2,v3);
        }

        public static bool DeletefileEascm(string v1, string v2, string v3, string v4)
        {
            return DL_User_Mngnt.DeletefileEascm(v1, v2, v3,v4);
        }
    }
}
