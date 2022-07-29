using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.Entities;

namespace Usermngt.DAL
{
    public class DL_RolePermisions
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static List<RolePermissions> GetRolepermission(int role_name_code)
        {
            List<RolePermissions> rolePermissions = new List<RolePermissions>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.role_permission where role_name_code='" + role_name_code + "'", cn);

                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        rolePermissions = new List<RolePermissions>();
                        while (dr.Read())
                        {
                            RolePermissions permission = new RolePermissions();
                            permission.id = Convert.ToInt32(dr["role_permission_id"].ToString());
                            permission.role_name_code = Convert.ToInt32(dr["role_name_code"].ToString());
                            permission.org_id = Convert.ToInt32(dr["org_id"].ToString());
                            permission.submodule_name = dr["submodule_code"].ToString();
                            permission.tab_name = dr["tab_name_id"].ToString();
                            permission.addpermition = dr["add_role_permission"].ToString();
                            permission.approvepermition = dr["approve_role_permission"].ToString();
                            permission.editpermition = dr["edit_role_permission"].ToString();
                            permission.reviewpermition = dr["review_role_permission"].ToString();
                            permission.deletepermition = dr["delete_role_permission"].ToString();
                            //   permission.list_url = dr["list_url"].ToString();
                            permission.mns_no = dr["mns_no"].ToString();
                            rolePermissions.Add(permission);
                        }
                    }
                    _log.Info("Get RolePermission Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get RolePermission Fail :"+ex.Message);
                }
            }
            return rolePermissions;
        }
        public static List<RolePermissions> GetUserpermission(int userregistrationid,string role_name_code)
        {
            List<RolePermissions> rolePermissions = new List<RolePermissions>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select * from  exciseautomation.user_permission  where user_registration_id='" + userregistrationid + "'  ", cn);

                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        rolePermissions = new List<RolePermissions>();
                        while (dr.Read())
                        {
                            RolePermissions permission = new RolePermissions();
                            permission.userid = dr["user_id"].ToString();
                            permission.user_registration_id = dr["user_registration_id"].ToString();
                            permission.role_name_code = Convert.ToInt32(dr["role_name_code"].ToString());
                            permission.org_id = Convert.ToInt32(dr["org_id"].ToString());
                            permission.submodule_name = dr["submodule_code"].ToString();
                            permission.tab_name = dr["tab_name_id"].ToString();
                            permission.mns_no = dr["mns_no"].ToString();
                            
                                permission.id = Convert.ToInt32(dr["user_permission_id"].ToString());
                                permission.addpermition = dr["add_role_permission"].ToString();
                                permission.approvepermition = dr["approve_role_permission"].ToString();
                                permission.editpermition = dr["edit_role_permission"].ToString();
                                permission.reviewpermition = dr["review_role_permission"].ToString();
                                permission.deletepermition = dr["delete_role_permission"].ToString();
                               
                            
                            //else
                            //{
                                
                            //    permission.addpermition = dr["add_role_permission"].ToString();
                            //    permission.approvepermition = dr["approve_role_permission"].ToString();
                            //    permission.editpermition = dr["edit_role_permission"].ToString();
                            //    permission.reviewpermition = dr["review_role_permission"].ToString();
                            //    permission.deletepermition = dr["delete_role_permission"].ToString();
                               
                              
                               
                            //}
                            rolePermissions.Add(permission);
                        }
                    }
                    _log.Info("Get User Permission  Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get User Permission Fail :"+ex.Message);
                }
            }
            return rolePermissions;
        }
        public static List<RolePermissions> GetRolepermissionList()
        {
            List<RolePermissions> rolePermissions = new List<RolePermissions>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select distinct b.Role_name,b.Role_name_code from exciseautomation.role_permission a right join exciseautomation.role_Master b on a.role_name_code=b.role_name_code order by b.role_name", cn);

                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        rolePermissions = new List<RolePermissions>();
                        while (dr.Read())
                        {
                            try
                            {
                                RolePermissions role = new RolePermissions();
                                role.role_name = dr["Role_name"].ToString();
                                role.role_name_code = Convert.ToInt32(dr["Role_name_code"].ToString());
                                // role.id = Convert.ToInt32(dr["role_permission_id"].ToString());
                                rolePermissions.Add(role);
                            }
                            catch
                            {

                            }
                        }
                    }
                    _log.Info("Get RolePermissions List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get RolePermissions List Fail :"+ex.Message);
                }
            }
            return rolePermissions;
        }

        public static List<RolePermissions> SearchRolePermissions(string tablename, string column, string value)
        {
            List<RolePermissions> rolePermissions = new List<RolePermissions>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select distinct b.Role_name,b.Role_name_code from exciseautomation.role_permission a right join exciseautomation.role_Master b on a.role_name_code=b.role_name_code  where " + column + " Ilike '%" + value + "%' order by b.role_name", cn);

                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        rolePermissions = new List<RolePermissions>();
                        while (dr.Read())
                        {
                            try
                            {
                                RolePermissions role = new RolePermissions();
                                role.role_name = dr["Role_name"].ToString();
                                role.role_name_code = Convert.ToInt32(dr["Role_name_code"].ToString());
                                // role.id = Convert.ToInt32(dr["role_permission_id"].ToString());
                                rolePermissions.Add(role);
                            }
                            catch
                            {

                            }
                        }
                    }
                    _log.Info("Get RolePermissions List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get RolePermissions List Fail :" + ex.Message);
                }
            }
            return rolePermissions;
        }

        public static List<RolePerssion_ViewModel> GetRoleModelList()
        {
            List<RolePerssion_ViewModel> RoleModelList = new List<RolePerssion_ViewModel>();
            try
            {
                using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
                {
                    cn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.Module_master ", cn))
                    {
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        DataTable tmodules = new DataTable();
                        tmodules.Load(dr);
                        dr.Close();
                        RoleModelList = new List<RolePerssion_ViewModel>();
                        foreach (DataRow row in tmodules.Rows)
                        {
                            RolePerssion_ViewModel role = new RolePerssion_ViewModel();
                            role.mns_no = Convert.ToInt32(row["mns_no"].ToString());
                            role.module_code = row["Module_code"].ToString();
                            role.module_name = row["module_name"].ToString();
                            role.submodulenames = new List<SubModule_Master>();
                            _log.Info("Get Module_master In ViewModel Success ");
                            using (NpgsqlConnection cn1 = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
                            {
                                cn1.Open();
                                using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.submodule_master where mns_no='" + role.mns_no + "' ", cn1))
                                {
                                    NpgsqlDataReader dr1 = cmd1.ExecuteReader();
                                    DataTable tsubmodules = new DataTable();
                                    tsubmodules.Load(dr1);
                                    dr1.Close();
                                    foreach (DataRow row1 in tsubmodules.Rows)
                                    {
                                        SubModule_Master sm = new SubModule_Master();
                                        sm.submodule_id = Convert.ToInt32(row1["submodule_id"].ToString());
                                        sm.submodule_code = row1["submodule_code"].ToString();
                                        sm.submodule_name = row1["submodule_name"].ToString();
                                        sm.tabnames = new List<Tab_Master>();
                                        _log.Info("Get SubModule_master In ViewModel Success ");
                                        using (NpgsqlConnection cn2 = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
                                        {
                                            cn2.Open();
                                            using (NpgsqlCommand cmd2 = new NpgsqlCommand("select * from exciseautomation.tabname_master where submodule_code='" + sm.submodule_code + "'  ", cn2))
                                            {
                                                NpgsqlDataReader dr3 = cmd2.ExecuteReader();
                                                DataTable ttabname = new DataTable();
                                                ttabname.Load(dr3);
                                                dr3.Close();
                                                foreach (DataRow row2 in ttabname.Rows)
                                                {
                                                    Tab_Master tbs = new Tab_Master();
                                                    tbs.tab_name_id = Convert.ToInt32(row2["tab_name_id"].ToString());
                                                    tbs.tab_name = row2["tab_name"].ToString();
                                                    sm.tabnames.Add(tbs);
                                                    _log.Info("Get TabName Master In ViewModel Success ");
                                                }
                                            }
                                        }
                                        role.submodulenames.Add(sm);
                                    }
                                }
                            }
                            RoleModelList.Add(role);
                        }
                    }
                }
                _log.Info("Get ViewModel Success ");
            }
            catch (Exception ex)
            {
                _log.Info("Get ViewModel Fail : "+ex.Message);
            }
            return RoleModelList;
        }
        public static int GetMaxID(string tablename)
        {
            int n = 0;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    string id = tablename;
                    if (tablename == "workflow")
                    {
                        id = "work_flow";
                    }
                    if (tablename == "Report_master")
                    {
                        id = "Report";
                    }
                   
                    NpgsqlCommand cmd = new NpgsqlCommand("select max(" + id + "_id) from exciseautomation." + tablename + "", cn);
                    string val = cmd.ExecuteScalar().ToString();
                    if (val == "" || val == "0")
                        n = 0;
                    else
                        n = Convert.ToInt32(val);
                    _log.Info("Get GetMaxID Success : " + tablename);
                }
                catch (Exception ex)
                {
                    _log.Info("Get GetMaxID Fail : " + tablename+"-"+ex.Message);
                }
            }
            return n;
        }
        public static bool InsertRolePermissions(string[] values, string role_name_code, string orgid, string rolepermissionID, string user)
        {
            bool value = false;
            string type = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    rolepermissionID =( GetMaxID("role_permission") + 1).ToString();
                    NpgsqlCommand cmd = new NpgsqlCommand("Select COUNT(1) from exciseautomation.role_permission where   tab_name_id='" + values[2] + "' and submodule_code='" + values[1] + "' and mns_no='" + values[0] + "' and role_name_code='"+role_name_code+"'", cn);
                    string VAL = cmd.ExecuteScalar().ToString();
                    int n = 0;
                    if (VAL != "0")
                    {
                        _log.Info("Update role_permission No Records : " + values);
                        n = 1;
                    }
                    if (n > 0)
                    {
                        if (values[values.Length - 1].ToString() == "Add")
                        {
                            type = "add_role_permission";
                        }
                        if (values[values.Length - 1].ToString() == "Edit")
                        {
                            type = "edit_role_permission";
                        }
                        if (values[values.Length - 1].ToString() == "Delete")
                        {
                            type = "delete_role_permission";
                        }
                        if (values[values.Length - 1].ToString() == "Review")
                        {
                            type = "review_role_permission";
                        }
                        if (values[values.Length - 1].ToString() == "Approve")
                        {
                            type = "approve_role_permission";
                        }
                        StringBuilder str = new StringBuilder();
                        str.Append("Update exciseautomation.role_permission set ");
                        str.Append("" + type + "='Y',");
                        str.Append(" user_id='" + user + "', lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "' where  tab_name_id='" + values[2] + "' and submodule_code='" + values[1] + "' and mns_no='" + values[0] + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        n = cmd.ExecuteNonQuery();
                        str = new StringBuilder();
                        str.Append("Update exciseautomation.user_permission set ");
                        str.Append("" + type + "='Y',");
                        str.Append("lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "' where  tab_name_id='" + values[2] + "' and submodule_code='" + values[1] + "' and mns_no='" + values[0] + "' and role_name_code='" + role_name_code + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        n = cmd.ExecuteNonQuery();

                        if (n > 0)
                        {
                            value = true;
                            _log.Info("Update role_permission Success : " + rolepermissionID+"-"+type);
                        }

                        else
                            value = false;
                    }
                    else
                    {
                        cmd = new NpgsqlCommand("select max(role_permission_id) from exciseautomation.role_permission", cn);
                        string val = cmd.ExecuteScalar().ToString();
                        if (val == "" || val == "0")
                            rolepermissionID = "1";
                        else
                            rolepermissionID = (Convert.ToInt32(val) + 1).ToString();
                       
                        if (values[values.Length - 1].ToString() == "Add")
                        {

                            type = "add_role_permission";
                            StringBuilder str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.role_permission(");
                            str.Append("role_permission_id, " + type + ", org_id,");
                            str.Append("role_name_code, status, submodule_code, tab_name_id, mns_no, user_id, lastmodified_date,edit_role_permission,review_role_permission,approve_role_permission,delete_role_permission)Values(");
                            str.Append("'" + rolepermissionID + "','Y','" + orgid + "',");
                            str.Append("'" + role_name_code + "','Active','" + values[1] + "','" + values[2] + "',");
                            str.Append("'" + values[0] + "','" + user + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','N','N','N','N')");
                            cmd = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd.ExecuteNonQuery();
                            cmd = new NpgsqlCommand("select distinct user_registration_id from exciseautomation.user_registration where role_name_code='" + role_name_code + "'", cn);
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            DataTable dt = new DataTable ();
                            dt.Load(dr);
                            dr.Close();
                            foreach(DataRow row in dt.Rows)
                            {
                                string registration_id = row["user_registration_id"].ToString();
                                InsertUserPermissions(values, role_name_code, orgid, 0, registration_id, user);
                            }
                            
                            if (n == 1)
                            {
                                value = true;
                                _log.Info("Insert role_permission Add Success : " + rolepermissionID + "-" + type);
                            }
                            else
                            {
                                value = false;
                                _log.Info("Insert role_permission Add  Fail : " + rolepermissionID + "-" + type);
                            }
                           
                        }
                        if (values[values.Length - 1].ToString() == "Edit")
                        {
                            type = "edit_role_permission";
                            StringBuilder str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.role_permission(");
                            str.Append("role_permission_id, " + type + ", org_id,");
                            str.Append("role_name_code, status, submodule_code, tab_name_id, mns_no, user_id, lastmodified_date,add_role_permission,review_role_permission,approve_role_permission,delete_role_permission)Values(");
                            str.Append("'" + rolepermissionID + "','Y','" + orgid + "',");
                            str.Append("'" + role_name_code + "','Active','" + values[1] + "','" + values[2] + "',");
                            str.Append("'" + values[0] + "','" + user + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','N','N','N','N')");
                            cmd = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd.ExecuteNonQuery();
                            cmd = new NpgsqlCommand("select distinct user_registration_id from exciseautomation.user_permission where role_name_code='" + role_name_code + "'", cn);
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            DataTable dt = new DataTable();
                            dt.Load(dr);
                            dr.Close();
                            foreach (DataRow row in dt.Rows)
                            {
                                string registration_id = row["user_registration_id"].ToString();
                                InsertUserPermissions(values, role_name_code, orgid, 0, registration_id, user);
                            }
                            if (n == 1)
                            {
                                value = true;
                                _log.Info("Insert role_permission Edit  Success : " + rolepermissionID + "-" + type);
                            }
                            else
                            {
                                value = false;
                                _log.Info("Insert role_permission Edit Fail : " + rolepermissionID + "-" + type);
                            }
                        }
                        if (values[values.Length - 1].ToString() == "Delete")
                        {

                            type = "delete_role_permission";
                            StringBuilder str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.role_permission(");
                            str.Append("role_permission_id, " + type + ", org_id,");
                            str.Append("role_name_code, status, submodule_code, tab_name_id, mns_no, user_id, lastmodified_date,add_role_permission,edit_role_permission,approve_role_permission,review_role_permission)Values(");
                            str.Append("'" + rolepermissionID + "','Y','" + orgid + "',");
                            str.Append("'" + role_name_code + "','Active','" + values[1] + "','" + values[2] + "',");
                            str.Append("'" + values[0] + "','" + user + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','N','N','N','N')");
                            cmd = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd.ExecuteNonQuery();
                            cmd = new NpgsqlCommand("select distinct user_registration_id from exciseautomation.user_permission where role_name_code='" + role_name_code + "'", cn);
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            DataTable dt = new DataTable();
                            dt.Load(dr);
                            dr.Close();
                            foreach (DataRow row in dt.Rows)
                            {
                                string registration_id = row["user_registration_id"].ToString();
                                InsertUserPermissions(values, role_name_code, orgid, 0, registration_id, user);
                            }
                            if (n == 1)
                            {
                                value = true;
                                _log.Info("Insert role_permission Delete Success : " + rolepermissionID + "-" + type);
                            }
                            else
                            {
                                value = false;
                                _log.Info("Insert role_permission Delete Fail : " + rolepermissionID + "-" + type);
                            }
                        }
                        if (values[values.Length - 1].ToString() == "Review")
                        {
                            type = "review_role_permission";
                            StringBuilder str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.role_permission(");
                            str.Append("role_permission_id, " + type + ", org_id,");
                            str.Append("role_name_code, status, submodule_code, tab_name_id, mns_no, user_id, lastmodified_date,add_role_permission,edit_role_permission,approve_role_permission,delete_role_permission)Values(");
                            str.Append("'" + rolepermissionID + "','Y','" + orgid + "',");
                            str.Append("'" + role_name_code + "','Active','" + values[1] + "','" + values[2] + "',");
                            str.Append("'" + values[0] + "','" + user + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','N','N','N','N')");
                            cmd = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd.ExecuteNonQuery();
                            cmd = new NpgsqlCommand("select distinct user_registration_id from exciseautomation.user_permission where role_name_code='" + role_name_code + "'", cn);
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            DataTable dt = new DataTable();
                            dt.Load(dr);
                            dr.Close();
                            foreach (DataRow row in dt.Rows)
                            {
                                string registration_id = row["user_registration_id"].ToString();
                                InsertUserPermissions(values, role_name_code, orgid, 0, registration_id, user);
                            }
                            if (n == 1)
                            {
                                value = true;
                                _log.Info("Insert role_permission Review Success : " + rolepermissionID + "-" + type);
                            }
                            else
                            {
                                value = false;
                                _log.Info("Insert role_permission Review Fail : " + rolepermissionID + "-" + type);
                            }
                        }
                        if (values[values.Length - 1].ToString() == "Approve")
                        {
                            type = "approve_role_permission";
                            StringBuilder str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.role_permission(");
                            str.Append("role_permission_id, " + type + ", org_id,");
                            str.Append("role_name_code, status, submodule_code, tab_name_id, mns_no, user_id, lastmodified_date,add_role_permission,edit_role_permission,review_role_permission,delete_role_permission)Values(");
                            str.Append("'" + rolepermissionID + "','Y','" + orgid + "',");
                            str.Append("'" + role_name_code + "','Active','" + values[1] + "','" + values[2] + "',");
                            str.Append("'" + values[0] + "','" + user + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','N','N','N','N')");
                            cmd = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd.ExecuteNonQuery();
                            cmd = new NpgsqlCommand("select distinct user_registration_id from exciseautomation.user_permission where role_name_code='" + role_name_code + "'", cn);
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            DataTable dt = new DataTable();
                            dt.Load(dr);
                            dr.Close();
                            foreach (DataRow row in dt.Rows)
                            {
                                string registration_id = row["user_registration_id"].ToString();
                                InsertUserPermissions(values, role_name_code, orgid, 0, registration_id, user);
                            }
                            if (n == 1)
                            {
                                value = true;
                                _log.Info("Insert role_permission Approve Success : " + rolepermissionID + "-" + type);
                            }
                            else
                            {
                                value = false;
                                _log.Info("Insert role_permission Approve Fail : " + rolepermissionID + "-" + type);
                            }
                        }


                    }
                }
                catch (Exception ex)
                {
                    
                        _log.Info("Insert role_permission  Fail : " + rolepermissionID + "-" + type+"-"+ex.Message);
                  
                }
            }
            return value;
        }
        public static bool InsertUserPermissions(string[] values, string role_name_code, string orgid, int userpermissionid, string registration_id, string user_id)
        {
            bool value = false;
            string type = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    //userpermissionid = GetMaxID("user_permission")+1;
                    NpgsqlCommand cmd = new NpgsqlCommand("Select COUNT(1) from exciseautomation.user_permission where   tab_name_id='" + values[2] + "' and submodule_code='" + values[1] + "' and mns_no='" + values[0] + "' and  user_registration_id='" + registration_id + "'", cn);
                    string VAL = cmd.ExecuteScalar().ToString();
                    int n = 0;
                    if (VAL != "0")
                    {
                        n = 1;
                    }
                    if (n > 0)
                    {

                        if (values[values.Length - 1].ToString() == "Add")
                        {
                            type = "add_role_permission";
                        }
                        if (values[values.Length - 1].ToString() == "Edit")
                        {
                            type = "edit_role_permission";
                        }
                        if (values[values.Length - 1].ToString() == "Delete")
                        {
                            type = "delete_role_permission";
                        }
                        if (values[values.Length - 1].ToString() == "Review")
                        {
                            type = "review_role_permission";
                        }
                        if (values[values.Length - 1].ToString() == "Approve")
                        {
                            type = "approve_role_permission";
                        }
                        StringBuilder str = new StringBuilder();
                        str.Append("Update exciseautomation.user_permission set ");
                        str.Append("" + type + "='Y',");
                        str.Append("lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "' where  tab_name_id='" + values[2] + "' and submodule_code='" + values[1] + "' and mns_no='" + values[0] + "' and  user_registration_id='" + registration_id + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            value = true;
                            _log.Info("Update User_permission  Success : " + userpermissionid + "-" + type);
                        }
                        else
                        {
                            value = false;
                            _log.Info("Update role_permission  Fail : " + userpermissionid + "-" + type);
                        }
                    }
                    else
                    {
                        cmd = new NpgsqlCommand("select max(user_permission_id) from exciseautomation.user_permission", cn);
                        string val = cmd.ExecuteScalar().ToString();
                        if (val == "" || val == "0")
                            userpermissionid = 1;
                        else
                            userpermissionid = Convert.ToInt32(val) + 1;
                        if (values[values.Length - 1].ToString() == "Add")
                        {

                            type = "add_role_permission";
                            StringBuilder str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.user_permission(");
                            str.Append(" user_permission_id," + type + ", org_id,");
                            str.Append("role_name_code, status, submodule_code, tab_name_id, mns_no,user_registration_id, user_id, lastmodified_date,edit_role_permission,review_role_permission,approve_role_permission,delete_role_permission)Values(");
                            str.Append("" + userpermissionid + ",'Y','" + orgid + "',");
                            str.Append("'" + role_name_code + "','Active','" + values[1] + "','" + values[2] + "',");
                            str.Append("'" + values[0] + "','" + registration_id + "','" + user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','N','N','N','N')");
                            cmd = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd.ExecuteNonQuery();
                            if (n == 1)
                            {
                                value = true;
                                _log.Info("Insert User_permission Add Success : " + userpermissionid + "-" + type);
                            }
                            else
                            {
                                value = false;
                                _log.Info("Insert role_permission Add Fail : " + userpermissionid + "-" + type);
                            }
                        }
                        if (values[values.Length - 1].ToString() == "Edit")
                        {
                            type = "edit_role_permission";
                            StringBuilder str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.user_permission(");
                            str.Append(" user_permission_id," + type + ", org_id,");
                            str.Append("role_name_code, status, submodule_code, tab_name_id, mns_no,user_registration_id, user_id, lastmodified_date,add_role_permission,review_role_permission,approve_role_permission,delete_role_permission)Values(");
                            str.Append("" + userpermissionid + ",'Y','" + orgid + "',");
                            str.Append("'" + role_name_code + "','Active','" + values[1] + "','" + values[2] + "',");
                            str.Append("'" + values[0] + "','" + registration_id + "','" + user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','N','N','N','N')");
                            cmd = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd.ExecuteNonQuery();
                            if (n == 1)
                            {
                                value = true;
                                _log.Info("Insert User_permission Edit Success : " + userpermissionid + "-" + type);
                            }
                            else
                            {
                                value = false;
                                _log.Info("Insert role_permission Edit Fail : " + userpermissionid + "-" + type);
                            }
                        }
                        if (values[values.Length - 1].ToString() == "Delete")
                        {

                            type = "delete_role_permission";
                            StringBuilder str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.user_permission(");
                            str.Append("user_permission_id," + type + ", org_id,");
                            str.Append("role_name_code, status, submodule_code, tab_name_id, mns_no, user_registration_id,user_id, lastmodified_date,add_role_permission,edit_role_permission,approve_role_permission,review_role_permission)Values(");
                            str.Append("" + userpermissionid + ",'Y','" + orgid + "',");
                            str.Append("'" + role_name_code + "','Active','" + values[1] + "','" + values[2] + "',");
                            str.Append("'" + values[0] + "','" + registration_id + "','" + user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','N','N','N','N')");
                            cmd = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd.ExecuteNonQuery();
                            if (n == 1)
                            {
                                value = true;
                                _log.Info("Insert User_permission Delete Success : " + userpermissionid + "-" + type);
                            }
                            else
                            {
                                value = false;
                                _log.Info("Insert role_permission Delete Fail : " + userpermissionid + "-" + type);
                            }
                        }
                        if (values[values.Length - 1].ToString() == "Review")
                        {
                            type = "review_role_permission";
                            StringBuilder str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.user_permission(");
                            str.Append("user_permission_id," + type + ", org_id,");
                            str.Append("role_name_code, status, submodule_code, tab_name_id, mns_no, user_registration_id,user_id, lastmodified_date,add_role_permission,edit_role_permission,approve_role_permission,delete_role_permission)Values(");
                            str.Append("" + userpermissionid + ",'Y','" + orgid + "',");
                            str.Append("'" + role_name_code + "','Active','" + values[1] + "','" + values[2] + "',");
                            str.Append("'" + values[0] + "','" + registration_id + "','" + user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','N','N','N','N')");
                            cmd = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd.ExecuteNonQuery();
                            if (n == 1)
                            {
                                value = true;
                                _log.Info("Insert User_permission Review Success : " + userpermissionid + "-" + type);
                            }
                            else
                            {
                                value = false;
                                _log.Info("Insert role_permission Review Fail : " + userpermissionid + "-" + type);
                            }
                        }
                        if (values[values.Length - 1].ToString() == "Approve")
                        {
                            type = "approve_role_permission";
                            StringBuilder str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.user_permission(");
                            str.Append("user_permission_id," + type + ", org_id,");
                            str.Append("role_name_code, status, submodule_code, tab_name_id, mns_no,user_registration_id, user_id, lastmodified_date,add_role_permission,edit_role_permission,review_role_permission,delete_role_permission)Values(");
                            str.Append("" + userpermissionid + ",'Y','" + orgid + "',");
                            str.Append("'" + role_name_code + "','Active','" + values[1] + "','" + values[2] + "',");
                            str.Append("'" + values[0] + "','" + registration_id + "','" + user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','N','N','N','N')");
                            cmd = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd.ExecuteNonQuery();
                            if (n == 1)
                            {
                                value = true;
                                _log.Info("Insert User_permission Approve Success : " + userpermissionid + "-" + type);
                            }
                            else
                            {
                                value = false;
                                _log.Info("Insert role_permission Approve Fail : " + userpermissionid + "-" + type);
                            }
                        }


                    }
                }
                catch (Exception ex)
                {
                   
                        _log.Info("Insert User_permission  Success : " + userpermissionid + "-" + type+"-"+ex.Message);
                   
                }
            }
            return value;
        }

        public static bool ClearRolePermissions(string[] values, string role_name_code, string orgid, string rolepermissionID, string user)
        {
            bool value = false;
            string type = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("Select COUNT(1) from exciseautomation.role_permission where   tab_name_id='" + values[3] + "' and submodule_code='" + values[2] + "' and mns_no='" + values[1] + "'", cn);
                    string VAL = cmd.ExecuteScalar().ToString();
                    int n = 0;
                    if (VAL != "" || VAL != "0")
                    {
                        StringBuilder str = new StringBuilder();
                        str.Append("Update exciseautomation.role_permission set add_role_permission='N',edit_role_permission='N',review_role_permission='N',approve_role_permission='N',delete_role_permission='N', ");

                        str.Append("lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "' where  tab_name_id='" + values[3] + "' and submodule_code='" + values[2] + "' and mns_no='" + values[1] + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        n = cmd.ExecuteNonQuery();

                        if (n == 1)
                        {
                            value = true;
                            _log.Info("Clear User_permission  Success : " +values);
                        }
                        else
                        {
                            value = false;
                            _log.Info("Clear role_permission  Fail : " + values);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Clear role_permission  Fail : " + ex.Message);
                    value = false;
                }
            }
            return value;
        }
        public static bool ClearOldPermissions(int ID, string table)
        {
            bool value = false;
            string type = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    if (table == "role_permission")
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand("delete from exciseautomation.role_permission where   role_name_code=" + ID + "", cn);
                        cmd.ExecuteNonQuery();
                        cmd = new NpgsqlCommand("delete from exciseautomation.user_permission where   role_name_code=" + ID + "", cn);
                        cmd.ExecuteNonQuery();
                        //int n = 0;
                        //if (VAL != "")
                        //{
                        //    StringBuilder str = new StringBuilder();
                        //    str.Append("Update exciseautomation.role_permission set add_role_permission='N',edit_role_permission='N',review_role_permission='N',approve_role_permission='N',delete_role_permission='N', ");

                        //    str.Append("lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "' where  role_permission_id=" + ID + "");
                        //    cmd = new NpgsqlCommand(str.ToString(), cn);
                        //    n = cmd.ExecuteNonQuery();

                        //    if (n == 1)
                        //    {
                        //        _log.Info("Clear OldPermissions of Role_Permissions Sucess : " +ID);
                        //        value = true;
                        //    }
                        //    else
                        //    {
                        //        _log.Info("Clear OldPermissions of Role_Permissions Fail : " + ID);
                        //        value = false;
                        //    }
                        //}
                    }
                    else
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand("delete from exciseautomation.user_permission where   user_registration_id=" + ID + "", cn);
                        cmd.ExecuteNonQuery();
                        //int n = 0;
                        //if (VAL != "")
                        //{
                        //    StringBuilder str = new StringBuilder();
                        //    str.Append("delete exciseautomation.user_permission set add_role_permission='N',edit_role_permission='N',review_role_permission='N',approve_role_permission='N',delete_role_permission='N', ");

                        //    str.Append("lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "' where  user_permission_id=" + ID + "");
                        //    cmd = new NpgsqlCommand(str.ToString(), cn);
                        //    n = cmd.ExecuteNonQuery();
                        //    if (n == 1)
                        //    {
                        //        _log.Info("Clear OldPermissions of Role_Permissions Sucess : " + ID);
                        //        value = true;
                        //    }
                        //    else
                        //    {
                        //        _log.Info("Clear OldPermissions of Role_Permissions Fail : " + ID);
                        //        value = false;
                        //    }
                        //}
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Clear OldPermissions of Role_Permissions Fail : " + ex.Message);
                    value = false;
                }
            }
            return value;
        }
        public static bool ClearUserPermissions(string[] values, string role_name_code, string orgid, string userregistrationid, string user_id)
        {
            bool value = false;
            string type = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("Select COUNT(1) from exciseautomation.user_permission where   tab_name_id='" + values[3] + "' and submodule_code='" + values[2] + "' and mns_no='" + values[1] + "' and user_registration_id='" + userregistrationid + "'", cn);
                    string VAL = cmd.ExecuteScalar().ToString();
                    int n = 0;
                    if (VAL != "" && VAL != "0")
                    {
                        StringBuilder str = new StringBuilder();
                        str.Append("Update exciseautomation.user_permission set add_role_permission='N',edit_role_permission='N',review_role_permission='N',approve_role_permission='N',delete_role_permission='N', ");

                        str.Append("lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "' where  user_registration_id='" + userregistrationid + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        n = cmd.ExecuteNonQuery();

                        if (n == 1)
                        {
                            _log.Info("Clear OldPermissions of User_Permissions Sucess : " + values);
                            value = true;
                        }
                        else
                        {
                            _log.Info("Clear OldPermissions of User_Permissions Fail : " + values);
                            value = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                        _log.Info("Clear OldPermissions of User_Permissions Fail : " + ex.Message);
                    
                    value = false;
                }
            }
            return value;
        }
    }
}
