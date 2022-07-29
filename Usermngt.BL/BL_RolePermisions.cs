using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_RolePermisions
    {
        public static List<RolePermissions> GetRolePermission(int id)
        {
            return DL_RolePermisions.GetRolepermission(id);
        }
        public static List<RolePermissions> GetRolePermissionList()
        {
            return DL_RolePermisions.GetRolepermissionList();
        }
        public static List<RolePermissions> SearchRolePermissions(string tablename, string column, string value)
        {
            return DL_RolePermisions.SearchRolePermissions(tablename, column, value);
        }
        public static int GetMaxID(string tablename)
        {
            return DL_RolePermisions.GetMaxID(tablename);
        }

        public static bool InsertRolePermissions(string[] values,string role_name_code,string orgid,string rolepermissionID,string user)
        {
            return DL_RolePermisions.InsertRolePermissions(values, role_name_code, orgid, rolepermissionID,user);
        }

        public static List<RolePerssion_ViewModel> GetRoleModelList()
        {
            return DL_RolePermisions.GetRoleModelList();
        }

        public static bool UpdateRolePermissions(string[] values, string role_name_code, string orgid, string rolepermissionID, string user)
        {
            return DL_RolePermisions.ClearRolePermissions(values, role_name_code, orgid, rolepermissionID, user);
        }

        public static List<RolePermissions> GetUserPermission(int registrationid, string role_name_code)
        {
            return DL_RolePermisions.GetUserpermission(registrationid, role_name_code);
        }

        public static bool InsertUserPermissions(string[] values, string role_name_code, string orgId, int userpermissionid,string registration_id, string user_id)
        {
            return DL_RolePermisions.InsertUserPermissions(values, role_name_code, orgId, userpermissionid, registration_id.ToString(), user_id);
        }

        public static bool UpdateUserPermissions(string[] values, string role_name_code, string orgId,string userpermissionid, string user_id)
        {
            return DL_RolePermisions.ClearUserPermissions(values, role_name_code, orgId, userpermissionid, user_id);
        }

        public static bool UpdatePermissions(int id,string tablename)
        {
            return DL_RolePermisions.ClearOldPermissions(id, tablename);
        }
    }
}
