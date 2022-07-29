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
  public  class DL_UserDetails
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static List<UserDetails> GetUserList(string userid)
        {
            List<UserDetails> users = new List<UserDetails>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
               
                try
                {
                    cn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,c.role_name,b.org_name,d.district_name from exciseautomation.user_registration a left join exciseautomation.org_master b on a.org_id=b.org_id left join exciseautomation.role_master c on a.role_name_code = c.role_name_code inner join exciseautomation.District_master d on a.district_code=d.district_code order by b.org_name,c.role_name,a.user_registration_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                       
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            users = new List<UserDetails>();
                            while (dr.Read())
                            {
                                UserDetails user = new UserDetails();
                                user.org_name = dr["org_name"].ToString().Trim();
                                user.user_name = dr["user_name"].ToString().Trim();
                                user.user_id = dr["user_id"].ToString().Trim();
                                user.mobile = dr["mobile"].ToString().Trim();
                                user.email_id = dr["email_id"].ToString().Trim();
                                user.role_name = dr["role_name"].ToString().Trim();
                                user.role_name_code =Convert.ToInt32( dr["role_name_code"].ToString());
                                user.id =Convert.ToInt32( dr["user_registration_id"].ToString());
                                user.district_code = dr["district_code"].ToString();
                                user.district_name = dr["district_name"].ToString();

                                users.Add(user);
                            }
                        }
                        _log.Info("Get Users List Success");
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Get Users List Fail :"+ex.Message);
                }
            }
            return users;
        }
        public static List<UserDetails> SearchUserDetails(string tablename, string column, string value)
        {
            List<UserDetails> users = new List<UserDetails>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
             
                try
                {
                    cn.Open();
                    NpgsqlCommand cmd;
                    if (column=="moble")
                    {
                        int a = Convert.ToInt32(value);
                     cmd = new NpgsqlCommand("select a.*,c.role_name,b.org_name,d.district_name from exciseautomation.user_registration a left join exciseautomation.org_master b on a.org_id=b.org_id left join exciseautomation.role_master c on a.role_name_code = c.role_name_code left join exciseautomation.District_master d on a.district_code=d.district_code where " + column + " Ilike '%" + a + "%'   order by b.org_name,c.role_name,a.user_registration_id", cn);

                    }
                   else if (column != "role_name"&& column != "moble")
                    {
                        
                        cmd = new NpgsqlCommand("select a.*,c.role_name,b.org_name,d.district_name from exciseautomation.user_registration a left join exciseautomation.org_master b on a.org_id=b.org_id left join exciseautomation.role_master c on a.role_name_code = c.role_name_code left join exciseautomation.District_master d on a.district_code=d.district_code where a."+column+" Ilike '%" + value + "%'   order by b.org_name,c.role_name,a.user_registration_id", cn);

                    }
                    else
                    {
                         cmd = new NpgsqlCommand("select a.*,c.role_name,b.org_name,d.district_name from exciseautomation.user_registration a left join exciseautomation.org_master b on a.org_id=b.org_id left join exciseautomation.role_master c on a.role_name_code = c.role_name_code left join exciseautomation.District_master d on a.district_code=d.district_code where " + column + " Ilike '%" + value + "%'   order by b.org_name,c.role_name,a.user_registration_id", cn);
                    }

                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                        {
                            users = new List<UserDetails>();
                            while (dr.Read())
                            {
                                UserDetails user = new UserDetails();
                                user.org_name = dr["org_name"].ToString().Trim();
                                user.user_name = dr["user_name"].ToString().Trim();
                                user.user_id = dr["user_id"].ToString().Trim();
                                user.mobile = dr["mobile"].ToString().Trim();
                                user.email_id = dr["email_id"].ToString().Trim();
                                user.role_name = dr["role_name"].ToString().Trim();
                                user.role_name_code = Convert.ToInt32(dr["role_name_code"].ToString());
                                user.id = Convert.ToInt32(dr["user_registration_id"].ToString());
                                user.district_code = dr["district_code"].ToString();
                                user.district_name = dr["district_name"].ToString();

                                users.Add(user);
                            }
                        }
                        _log.Info("Get Users List Success");
                    
                }
                catch (Exception ex)
                {
                    _log.Info("Get Users List Fail :" + ex.Message);
                }
            }
            return users;
        }
        public static List<UserDetails> GetUserPermissins(string userid)
        {
            List<UserDetails> users = new List<UserDetails>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
              
                try
                {
                    cn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.approver_level as party_level ,b.party_name,c.party_type_name,c.party_type_code,d.role_name,b.party_captive_unit_name,b.party_captive,e.financial_year,g.module_code,f.submodule_code,f.tab_name_id,f.add_role_permission,f.add_role_permission,f.edit_role_permission,f.delete_role_permission,f.review_role_permission,f.approve_role_permission,b.enddate from exciseautomation.user_registration a inner join exciseautomation.party_master b on a.party_code = b.party_code inner join exciseautomation.party_type_master c on b.party_type_code = c.party_type_code inner join exciseautomation.role_master d on a.role_name_code = d.role_name_code left join exciseautomation.user_permission f on a.user_registration_id = f.user_registration_id left join exciseautomation.module_master g on g.mns_no = f.mns_no left join exciseautomation.party_financial_yr e on e.party_type_code = c.party_type_code where a.user_id='" + userid + "'", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            users = new List<UserDetails>();
                            while (dr.Read())
                            {
                                UserDetails user = new UserDetails();
                                user.id = Convert.ToInt32(dr["user_registration_id"].ToString());
                                user.user_id = dr["user_id"].ToString();
                                user.user_dob = dr["user_dob"].ToString();
                                user.user_password = dr["user_password"].ToString();
                                user.access_type_code = Convert.ToInt32(dr["access_type_code"].ToString());
                                if (dr["role_level_code"].ToString() != "")
                                    user.role_level_code = Convert.ToInt32(dr["role_level_code"].ToString());
                                user.org_id = Convert.ToInt32(dr["org_id"].ToString());
                                user.role_name_code = Convert.ToInt32(dr["role_name_code"].ToString());
                                user.user_address = dr["user_address"].ToString();
                                user.mobile = dr["mobile"].ToString();
                                user.email_id = dr["email_id"].ToString();
                                user.state_code = dr["State_code"].ToString();
                                user.division_code = dr["Division_code"].ToString();
                                user.district_code = dr["District_Code"].ToString();
                                user.photoname = dr["photoname"].ToString();
                                //user.comments = dr["Comments"].ToString();
                                user.user_name = dr["user_name"].ToString();
                                user.mobile = dr["mobile"].ToString();
                                user.email_id = dr["email_id"].ToString();
                                user.department_name = dr["department_code"].ToString();
                                user.party_code = dr["Party_code"].ToString();
                                user.party_name = dr["Party_name"].ToString();
                                user.party_type = dr["Party_type_name"].ToString();
                                user.party_type_code = dr["Party_type_code"].ToString();
                                user.role_name = dr["role_name"].ToString();
                                user.party_captive_unit_name = dr["party_captive_unit_name"].ToString();
                                user.financial_year = dr["financial_year"].ToString();
                                user.iscaptive = dr["party_captive"].ToString();
                                user.module_code = dr["module_code"].ToString();
                                user.submodule_code = dr["submodule_code"].ToString(); 
                                user.tab_name_id = dr["tab_name_id"].ToString();
                                if (dr["enddate"].ToString() != "")
                                 user.Financial_year_enddate = dr["enddate"].ToString();
                                //  user.tab_name = dr["tab_name"].ToString();
                                user.add = dr["add_role_permission"].ToString();
                                user.edit = dr["edit_role_permission"].ToString();
                                user.delete = dr["delete_role_permission"].ToString();
                                user.review = dr["review_role_permission"].ToString();
                                user.party_approvel_level = dr["party_level"].ToString();
                                user.approve = dr["approve_role_permission"].ToString();
                                users.Add(user);
                            }
                            _log.Info("Get User Success :" + userid);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Get User Fail :" + userid+"_" + ex.Message);
                }
            }
            return users;
        }

        public static UserDetails CheckUser(string userid)
        {
            UserDetails user = new UserDetails();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
               
                try
                {
                    cn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.view_checkuser where user_id='" + userid + "'", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            user = new UserDetails();
                            while (dr.Read())
                            {
                                user.id = Convert.ToInt32(dr["user_registration_id"].ToString());
                                user.user_id = dr["user_id"].ToString();
                                user.user_dob = dr["user_dob"].ToString();
                                user.user_password = dr["user_password"].ToString();
                                user.access_type_code = Convert.ToInt32(dr["access_type_code"].ToString());
                                if (dr["role_level_code"].ToString() != "")
                                    user.role_level_code = Convert.ToInt32(dr["role_level_code"].ToString());
                                user.org_id = Convert.ToInt32(dr["org_id"].ToString());
                                user.role_name_code = Convert.ToInt32(dr["role_name_code"].ToString());
                                user.user_address = dr["user_address"].ToString();
                                user.mobile = dr["mobile"].ToString();
                                user.email_id = dr["email_id"].ToString();
                                user.state_code = dr["State_code"].ToString();
                                user.division_code = dr["Division_code"].ToString();
                                user.district_code = dr["District_Code"].ToString();
                                user.photoname = dr["photoname"].ToString();
                                //user.comments = dr["Comments"].ToString();
                                user.user_name = dr["user_name"].ToString();
                                user.mobile = dr["mobile"].ToString();
                                user.email_id = dr["email_id"].ToString();
                                user.department_name = dr["department_code"].ToString();
                                user.party_code = dr["Party_code"].ToString();
                                user.party_name = dr["Party_name"].ToString();
                                user.party_type = dr["Party_type_name"].ToString();
                                user.party_type_code = dr["party_type_code"].ToString();
                                user.role_name = dr["role_name"].ToString();
                                user.party_captive_unit_name = dr["party_captive_unit_name"].ToString();
                                user.financial_year = dr["financial_year"].ToString();
                                user.iscaptive = dr["party_captive"].ToString();
                                user.digi_id = dr["dongle_id"].ToString();
                                user.digi_password = dr["dongle_password"].ToString();
                                //user.module_code = dr["module_code"].ToString();
                                //user.submodule_code = dr["submodule_code"].ToString();
                                //user.tab_name_id = dr["tab_name_id"].ToString();
                                //user.tab_name = dr["tab_name"].ToString();
                                //user.add = dr["add_role_permission"].ToString();
                                //user.edit = dr["edit_role_permission"].ToString();
                                //user.delete = dr["delete_role_permission"].ToString();
                                //user.review = dr["review_role_permission"].ToString();
                                //user.approve = dr["approve_role_permission"].ToString();
                            }
                            _log.Info("Get User Success :" + user.id + "-" + user.user_name);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Get User Fail :" + "-" + ex.Message);
                }
            }
            return user;
        }

        public static List<UserDetails> AllUserList(string userid)
        {
            List<UserDetails> users = new List<UserDetails>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {

                try
                {
                    cn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.enddate from exciseautomation.view_checkuser a inner join exciseautomation.party_master b on a.party_code=b.party_code   ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;

                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            users = new List<UserDetails>();
                            while (dr.Read())
                            {
                                UserDetails user = new UserDetails();
                                user.id = Convert.ToInt32(dr["user_registration_id"].ToString());
                                user.user_id = dr["user_id"].ToString();
                                user.user_dob = dr["user_dob"].ToString();
                                user.user_password = dr["user_password"].ToString();
                                user.access_type_code = Convert.ToInt32(dr["access_type_code"].ToString());
                                if (dr["role_level_code"].ToString() != "")
                                    user.role_level_code = Convert.ToInt32(dr["role_level_code"].ToString());
                                user.org_id = Convert.ToInt32(dr["org_id"].ToString());
                                user.role_name_code = Convert.ToInt32(dr["role_name_code"].ToString());
                                user.user_address = dr["user_address"].ToString();
                                user.mobile = dr["mobile"].ToString();
                                user.email_id = dr["email_id"].ToString();
                                user.state_code = dr["State_code"].ToString();
                                user.division_code = dr["Division_code"].ToString();
                                user.district_code = dr["District_Code"].ToString();
                                user.photoname = dr["photoname"].ToString();
                                //user.comments = dr["Comments"].ToString();
                                user.user_name = dr["user_name"].ToString();
                                user.mobile = dr["mobile"].ToString();
                                user.email_id = dr["email_id"].ToString();
                                user.department_name = dr["department_code"].ToString();
                                user.party_code = dr["Party_code"].ToString();
                                user.party_name = dr["Party_name"].ToString();
                                user.party_type = dr["Party_type_name"].ToString();
                                user.party_type_code = dr["party_type_code"].ToString();
                                user.role_name = dr["role_name"].ToString();
                                user.party_captive_unit_name = dr["party_captive_unit_name"].ToString();
                                user.financial_year = dr["financial_year"].ToString();
                                if (dr["enddate"].ToString() != "")
                                    user.Financial_year_enddate = dr["enddate"].ToString();
                                user.iscaptive = dr["party_captive"].ToString();
                                user.digi_id = dr["dongle_id"].ToString();
                                user.digi_password = dr["dongle_password"].ToString();

                                users.Add(user);
                            }
                        }
                        _log.Info("Get Users List Success");
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Get Users List Fail :" + ex.Message);
                }
            }
            return users;
        }
     

        public static List<UserDetails> GetUsers(string userid)
        {
            List<UserDetails> users = new List<UserDetails>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
             
                try
                {
                    cn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select  distinct user_name,user_id,Role_name_code,org_id,user_registration_id from exciseautomation.user_registration order by user_registration_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;

                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            users = new List<UserDetails>();
                            while (dr.Read())
                            {
                                UserDetails user = new UserDetails();
                                user.user_name = dr["user_name"].ToString()+"/"+ dr["user_id"].ToString();
                                user.user_id =dr["user_id"].ToString();
                                user.role_name_code =Convert.ToInt32( dr["Role_name_code"].ToString());
                                user.org_id = Convert.ToInt32(dr["org_id"].ToString());
                                user.id = Convert.ToInt32(dr["user_registration_id"].ToString());
                                users.Add(user);
                            }
                            _log.Info("Get Single User Details Success");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Get Single User Details Fail :"+ex.Message);
                }
            }
            return users;
        }

        public static List<UserDetails> SearchUserpermission(string tablename, string column, string value)
        {
            List<UserDetails> users = new List<UserDetails>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
               
                try
                {
                    cn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select   user_name,user_id,Role_name_code,org_id,user_registration_id from exciseautomation.user_registration where " + column + " Ilike '%" + value + "%' order by user_registration_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;

                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            users = new List<UserDetails>();
                            while (dr.Read())
                            {
                                UserDetails user = new UserDetails();
                                user.user_name = dr["user_name"].ToString() + "/" + dr["user_id"].ToString();
                                user.user_id = dr["user_id"].ToString();
                                user.role_name_code = Convert.ToInt32(dr["Role_name_code"].ToString());
                                user.org_id = Convert.ToInt32(dr["org_id"].ToString());
                                user.id = Convert.ToInt32(dr["user_registration_id"].ToString());
                                users.Add(user);
                            }
                            _log.Info("Get Single User Details Success");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Get Single User Details Fail :" + ex.Message);
                }
            }
            return users;
        }
        public static bool InsertUser(UserDetails user)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    
                    StringBuilder str = new StringBuilder();
                    NpgsqlCommand cmd1 = new NpgsqlCommand("select case when max(user_registration_id) is null then 0 else max(user_registration_id) end as user_registration_id from exciseautomation.user_registration", cn);
                    int user_registrationid = Convert.ToInt32(cmd1.ExecuteScalar().ToString()) + 1;
                    //  
                    //  "'" + user_registrationid + "',user_registration_id,
                    str.Append("INSERT INTO exciseautomation.user_registration(");
                    str.Append("user_registration_id,access_type_code, user_address, district_code, division_code");
                    str.Append(", email_id, mobile,org_id, photoname, user_password,");
                    str.Append(" role_name_code, state_code, user_id, user_name, record_status,role_level_code,department_code,party_code,employee_master_id,designation_code");
                    str.Append(")Values(");
                    str.Append("'" + user_registrationid + "','" + user.access_type_code + "','" + user.user_address + "','" + user.district_code + "','" + user.division_code + "',");
                    str.Append("'" + user.email_id + "','" + user.mobile + "','" + user.org_id + "','" + user.photoname + "','" + user.user_password + "',");
                    str.Append("'" + user.role_name_code + "','" + user.state_code + "','" + user.user_id + "','" + user.user_name + "','Active','"+user.role_level_code+"','"+user.department_name+"','"+user.party_code+"','"+user.emp_id+"','"+user.designation_code+"')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    cmd = new NpgsqlCommand("select user_registration_id from exciseautomation.user_registration where user_id='" + user.user_id+"'", cn);
                    string userid = cmd.ExecuteScalar().ToString();
                    cmd = new NpgsqlCommand("select case when max(user_permission_id) is null then 0 else max(user_permission_id) end as user_permission_id from exciseautomation.user_permission", cn);
                    int userepermissionID = Convert.ToInt32(cmd.ExecuteScalar().ToString()) + 1;
                    cmd = new NpgsqlCommand("Select * from exciseautomation.role_permission where role_name_code='"+user.role_name_code+"'", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dr.Close();
                    foreach (DataRow row in dt.Rows)
                    {
                        string org_id = row["org_id"].ToString();
                        string module_code = row["mns_no"].ToString();
                        string submodule_code = row["submodule_code"].ToString();
                        string tab_name_id = row["tab_name_id"].ToString();
                        string approve_role_permission = row["approve_role_permission"].ToString();
                        string edit_role_permission = row["edit_role_permission"].ToString();
                        string delete_role_permission = row["delete_role_permission"].ToString();
                        string add_role_permission = row["add_role_permission"].ToString();
                        string review_role_permission = row["review_role_permission"].ToString();
                        cmd = new NpgsqlCommand("INSERT INTO exciseautomation.user_permission(user_permission_id, user_registration_id, approve_role_permission,  edit_role_permission, delete_role_permission, add_role_permission, org_id, review_role_permission, role_name_code, status, submodule_code, tab_name_id, mns_no, user_id)VALUES('"+ userepermissionID + "','"+userid+"','"+approve_role_permission+"','"+ edit_role_permission+"', '"+delete_role_permission+"', '"+add_role_permission+"','"+ org_id+"', '"+review_role_permission+"', '"+user.role_name_code+"', 'Active', '"+submodule_code+"', '"+tab_name_id+"','"+ module_code+"', '"+user.user_id+"')",cn);
                        cmd.ExecuteNonQuery();
                        userepermissionID++;
                        //cmd = new NpgsqlCommand("INSERT INTO exciseautomation.role_permission(role_permission_id,approve_role_permission,  edit_role_permission, delete_role_permission, add_role_permission, org_id, review_role_permission, role_name_code, status, submodule_code, tab_name_id, mns_no, user_id)VALUES('" + rolepermissionID + "','" + approve_role_permission + "','" + edit_role_permission + "', '" + delete_role_permission + "', '" + add_role_permission + "','" + org_id + "', '" + review_role_permission + "', '" + user.role_name_code + "', 'Active', '" + submodule_code + "', '" + tab_name_id + "','" + module_code + "', '" + user.user_id + "')", cn);
                        //cmd.ExecuteNonQuery();
                    }
                    trn.Commit();
                    cn.Close();
                        value = true;
                        _log.Info("Insert User Success :" + user.id + "-" + user.user_name);
                   
                }
                catch (Exception ex)
                {
                    _log.Info("Insert User Fail :" + user.id + "-" + user.user_name+"-"+ex.Message);
                    value = false;
                    trn.Rollback();
                }
            }
            return value;
        }
        public static bool UpdateUser(UserDetails user)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
              
                try
                {
                    cn.Open();
                    StringBuilder str = new StringBuilder();
                    str.Append("update exciseautomation.user_registration set ");
                    str.Append("access_type_code='" + user.access_type_code + "', user_address='" + user.user_address + "',  district_code='" + user.district_code + "', division_code='" + user.division_code + "'");
                    str.Append(", email_id='" + user.email_id + "', mobile='" + user.mobile + "',org_id='" + user.org_id + "', photoname='" + user.photoname + "', ");
                    str.Append(" role_name_code='" + user.role_name_code + "',role_level_code='"+user.role_level_code+"', state_code='" + user.state_code + "', user_name='" + user.user_name + "',department_code='"+user.department_name+"',party_code='"+user.party_code+"',user_id='"+user.user_id+ "',employee_master_id='"+user.emp_id+ "',designation_code='"+user.designation_code+"' where user_registration_id='" + user.id + "'");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                        _log.Info("Update User Success :" + user.id + "-" + user.user_name);
                    }
                    else
                    {
                        value = false;
                        _log.Info("Update User Fail :" + user.id + "-" + user.user_name);
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Update User Fail :" + user.id + "-" + user.user_name + "-" + ex.Message);
                    value = false;
                }
            }
            return value;
        }
        //public static UserDetails GetUser(string userid)
        //{
        //    UserDetails user = new UserDetails();
        //    using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
        //    {

        //        try
        //        {
        //            cn.Open();
        //            using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,c.party_type_name,e.role_name,b.financial_year from exciseautomation.user_registration a inner join exciseautomation.party_master b  on a.party_code = b.party_code inner join exciseautomation.party_type_master c on c.party_type_code = b.party_type_code inner join exciseautomation.role_master e on a.role_name_code=e.role_name_code   where a.user_id='" + userid+ "'", cn))
        //            {
        //                cmd.CommandType = System.Data.CommandType.Text;
        //                NpgsqlDataReader dr = cmd.ExecuteReader();
        //                if (dr.HasRows)
        //                {
        //                     user = new UserDetails();
        //                    while (dr.Read())
        //                    {
        //                        user.id=Convert.ToInt32( dr["user_registration_id"].ToString());
        //                        user.user_id= dr["user_id"].ToString();
        //                        user.user_dob= dr["user_dob"].ToString();
        //                        user.user_password= dr["user_password"].ToString();
        //                        user.access_type_code = Convert.ToInt32(dr["access_type_code"].ToString());
        //                        if(dr["role_level_code"].ToString()!="")
        //                       user.role_level_code = Convert.ToInt32(dr["role_level_code"].ToString());
        //                        user.org_id =Convert.ToInt32( dr["org_id"].ToString());
        //                        user.role_name_code = Convert.ToInt32(dr["role_name_code"].ToString());
        //                        user.user_address = dr["user_address"].ToString();
        //                        user.mobile = dr["mobile"].ToString();
        //                        user.email_id = dr["email_id"].ToString();
        //                        user.state_code = dr["State_code"].ToString();
        //                        user.division_code = dr["Division_code"].ToString();
        //                        user.financial_year= dr["financial_year"].ToString();
        //                        user.district_code = dr["District_Code"].ToString();
        //                        user.photoname = dr["photoname"].ToString();
        //                        //user.comments = dr["Comments"].ToString();
        //                        user.user_name = dr["user_name"].ToString();
        //                        user.mobile = dr["mobile"].ToString();
        //                        user.email_id = dr["email_id"].ToString();
        //                        user.department_name = dr["department_code"].ToString();
        //                        user.party_code = dr["Party_code"].ToString();
        //                        user.party_type = dr["Party_type_name"].ToString();
        //                        user.role_name = dr["role_name"].ToString();
        //                    }
        //                    _log.Info("Get User Success :" + user.id + "-" + user.user_name);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            _log.Info("Get User Fail :" + user.id + "-" + user.user_name + "-" + ex.Message);
        //            user.record_status = ex.Message;
        //        }
        //    }
        //    return user;
        //}


        public static UserDetails GetUser(string userid)
        {
            UserDetails user = new UserDetails();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {

                try
                {
                    cn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,c.party_type_name,e.role_name,b.financial_year,d1.designation_name,d1.designation_code,e1.emp_name,e1.doj,e1.start_date,e1.end_date,e1.emer_contact,e1.bloodgroup,e1.prangpf_no from exciseautomation.user_registration a inner join exciseautomation.party_master b  on a.party_code = b.party_code inner join exciseautomation.party_type_master c on c.party_type_code = b.party_type_code inner join exciseautomation.role_master e on a.role_name_code=e.role_name_code  left join  exciseautomation.employee_master e1 on a.user_id=e1.user_id  left join  exciseautomation.designation_master d1 on e1.designation_code=d1.designation_code where a.user_id='" + userid + "'", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            user = new UserDetails();
                            while (dr.Read())
                            {
                                user.id = Convert.ToInt32(dr["user_registration_id"].ToString());
                                user.user_id = dr["user_id"].ToString();
                                user.user_name= dr["user_name"].ToString();
                                user.user_dob = dr["user_dob"].ToString();
                                user.user_password = dr["user_password"].ToString();
                                user.access_type_code = Convert.ToInt32(dr["access_type_code"].ToString());
                                if (dr["role_level_code"].ToString() != "")
                                    user.role_level_code = Convert.ToInt32(dr["role_level_code"].ToString());
                                user.org_id = Convert.ToInt32(dr["org_id"].ToString());
                                user.role_name_code = Convert.ToInt32(dr["role_name_code"].ToString());
                                user.user_address = dr["user_address"].ToString();
                                user.mobile = dr["mobile"].ToString();
                                user.email_id = dr["email_id"].ToString();
                                user.state_code = dr["State_code"].ToString();
                                user.division_code = dr["Division_code"].ToString();
                                user.financial_year = dr["financial_year"].ToString();
                                user.district_code = dr["District_Code"].ToString();
                                user.photoname = dr["photoname"].ToString();
                                user.blood_group = dr["bloodgroup"].ToString();
                                //user.comments = dr["Comments"].ToString();
                                user.user_name = dr["emp_name"].ToString();
                                user.mobile = dr["mobile"].ToString();
                                user.email_id = dr["email_id"].ToString();
                                user.department_name = dr["department_code"].ToString();
                                user.party_code = dr["Party_code"].ToString();
                                user.party_type = dr["Party_type_name"].ToString();
                                user.role_name = dr["role_name"].ToString();
                                user.designation_code = dr["designation_code"].ToString();
                                user.designation_name = dr["designation_name"].ToString();
                                user.emergency_contact = dr["emer_contact"].ToString();
                                user.pran_no = dr["prangpf_no"].ToString();
                                if (dr["doj"].ToString() != "" || dr["doj"].ToString() != null)
                                    user.date_of_joining = dr["doj"].ToString().Substring(0, 10).Replace("/", "-");
                                if (dr["doj"].ToString() != "" || dr["doj"].ToString() != null)
                                    user.date_of_joining = dr["doj"].ToString().Substring(0, 10).Replace("/", "-");
                                if (dr["start_date"].ToString() != "" || dr["start_date"].ToString() != null)
                                    user.date_of_posting = dr["start_date"].ToString().Substring(0, 10).Replace("/", "-");
                                if (dr["end_date"].ToString() != "" || dr["end_date"].ToString() != null)
                                    user.date_of_retairment = dr["end_date"].ToString().Substring(0, 10).Replace("/", "-");
                            }
                            _log.Info("Get User Success :" + user.id + "-" + user.user_name);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Get User Fail :" + user.id + "-" + user.user_name + "-" + ex.Message);
                    user.record_status = ex.Message;
                }
            }
            return user;
        }
        public static UserDetails GetUserDetails(string userid)
        {
            UserDetails user = new UserDetails();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
               
                try
                {
                    cn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.user_registration where user_registration_id='" + userid + "'", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;

                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            user = new UserDetails();
                            while (dr.Read())
                            {
                                user.id = Convert.ToInt32(dr["user_registration_id"].ToString());
                                user.user_id = dr["user_id"].ToString();
                                user.user_dob = dr["user_dob"].ToString();
                                user.user_password = dr["user_password"].ToString();
                                user.access_type_code = Convert.ToInt32(dr["access_type_code"].ToString());
                                if (dr["role_level_code"].ToString() != "")
                                    user.role_level_code = Convert.ToInt32(dr["role_level_code"].ToString());
                                user.org_id = Convert.ToInt32(dr["org_id"].ToString());
                                user.role_name_code = Convert.ToInt32(dr["role_name_code"].ToString());
                                user.user_address = dr["user_address"].ToString();
                                user.mobile = dr["mobile"].ToString();
                                user.email_id = dr["email_id"].ToString();
                                user.state_code = dr["State_code"].ToString();
                                user.division_code = dr["Division_code"].ToString();
                                user.district_code = dr["District_Code"].ToString();
                                user.photoname = dr["photoname"].ToString();
                                user.designation_code = dr["designation_code"].ToString();
                                //user.comments = dr["Comments"].ToString();
                                user.user_name = dr["user_name"].ToString();
                                user.mobile = dr["mobile"].ToString();
                                user.email_id = dr["email_id"].ToString();
                                user.department_name = dr["department_code"].ToString();
                                user.party_code = dr["Party_code"].ToString();
                            }
                            _log.Info("Get User Success :" + user.id + "-" + user.user_name);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Get User Fail :" + user.id + "-" + user.user_name + "-" + ex.Message);
                }
            }
            return user;
        }




        public static UserDetails GetProfileUser(string userid,string mobile)
        {
            UserDetails user = new UserDetails();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {

                try
                {
                    cn.Open();
                    NpgsqlCommand cmd;

                    cmd = new NpgsqlCommand("select count(employee_master_id) from exciseautomation.employee_master where user_id='" + userid+"'", cn);
                    int re =Convert.ToInt32(cmd.ExecuteScalar());
                    if (re >0)
                    {
                        using (NpgsqlCommand cmd2 = new NpgsqlCommand("select a.*,c.party_type_name,e.role_name,b.financial_year,d1.designation_name,d1.designation_code,e1.emp_name,e1.doj,e1.start_date,e1.end_date,e1.emer_contact,e1.bloodgroup,e1.prangpf_no from exciseautomation.user_registration a inner join exciseautomation.party_master b  on a.party_code = b.party_code inner join exciseautomation.party_type_master c on c.party_type_code = b.party_type_code inner join exciseautomation.role_master e on a.role_name_code=e.role_name_code  left join  exciseautomation.employee_master e1 on a.user_id=e1.user_id  left join  exciseautomation.designation_master d1 on e1.designation_code=d1.designation_code where a.user_id='" + userid + "'", cn))
                        {
                            cmd2.CommandType = System.Data.CommandType.Text;
                            NpgsqlDataReader dr = cmd2.ExecuteReader();
                            if (dr.HasRows)
                            {
                                user = new UserDetails();
                                while (dr.Read())
                                {
                                    user.id = Convert.ToInt32(dr["user_registration_id"].ToString());
                                    user.user_id = dr["user_id"].ToString();
                                    user.user_name = dr["user_name"].ToString();
                                    user.user_dob = dr["user_dob"].ToString();
                                    user.user_password = dr["user_password"].ToString();
                                    user.access_type_code = Convert.ToInt32(dr["access_type_code"].ToString());
                                    if (dr["role_level_code"].ToString() != "")
                                        user.role_level_code = Convert.ToInt32(dr["role_level_code"].ToString());
                                    user.org_id = Convert.ToInt32(dr["org_id"].ToString());
                                    user.role_name_code = Convert.ToInt32(dr["role_name_code"].ToString());
                                    user.user_address = dr["user_address"].ToString();
                                    user.mobile = dr["mobile"].ToString();
                                    user.email_id = dr["email_id"].ToString();
                                    user.state_code = dr["State_code"].ToString();
                                    user.division_code = dr["Division_code"].ToString();
                                    user.financial_year = dr["financial_year"].ToString();
                                    user.district_code = dr["District_Code"].ToString();
                                    user.photoname = dr["photoname"].ToString();
                                    user.blood_group = dr["bloodgroup"].ToString();
                                    //user.comments = dr["Comments"].ToString();
                                    if(dr["emp_name"].ToString()!="" && dr["emp_name"].ToString()!=null)
                                    {
                                        user.user_name = dr["emp_name"].ToString();
                                    }
                                    else
                                    {
                                        user.user_name = dr["user_name"].ToString();
                                    }
                                    user.mobile = dr["mobile"].ToString();
                                    user.email_id = dr["email_id"].ToString();
                                    user.department_name = dr["department_code"].ToString();
                                    user.party_code = dr["Party_code"].ToString();
                                    user.party_type = dr["Party_type_name"].ToString();
                                    user.role_name = dr["role_name"].ToString();
                                    user.designation_code = dr["designation_code"].ToString();
                                    user.designation_name = dr["designation_name"].ToString();
                                    user.emergency_contact = dr["emer_contact"].ToString();
                                    user.pran_no = dr["prangpf_no"].ToString();
                                    if (dr["doj"].ToString() != "" || dr["doj"].ToString() != null)
                                        user.date_of_joining = dr["doj"].ToString().Substring(0, 10).Replace("/", "-");
                                    if (dr["doj"].ToString() != "" || dr["doj"].ToString() != null)
                                        user.date_of_joining = dr["doj"].ToString().Substring(0, 10).Replace("/", "-");
                                    if (dr["start_date"].ToString() != "" || dr["start_date"].ToString() != null)
                                        user.date_of_posting = dr["start_date"].ToString().Substring(0, 10).Replace("/", "-");
                                    if (dr["end_date"].ToString() != "" || dr["end_date"].ToString() != null)
                                        user.date_of_retairment = dr["end_date"].ToString().Substring(0, 10).Replace("/", "-");
                                }
                                _log.Info("Get User Success :" + user.id + "-" + user.user_name);
                            }
                        }

                    }
                    else
                    {
                        using (NpgsqlCommand cmd3 = new NpgsqlCommand("select a.*,c.party_type_name,e.role_name,b.financial_year,d1.designation_name,d1.designation_code,e1.emp_name,e1.doj,e1.start_date,e1.end_date,e1.emer_contact,e1.bloodgroup,e1.prangpf_no from exciseautomation.user_registration a inner join exciseautomation.party_master b  on a.party_code = b.party_code inner join exciseautomation.party_type_master c on c.party_type_code = b.party_type_code inner join exciseautomation.role_master e on a.role_name_code=e.role_name_code  left join  exciseautomation.employee_master e1 on a.user_id=e1.user_id  left join  exciseautomation.designation_master d1 on e1.designation_code=d1.designation_code where e1.mobile='" + mobile + "'", cn))
                        {
                            cmd3.CommandType = System.Data.CommandType.Text;
                            NpgsqlDataReader dr = cmd3.ExecuteReader();
                            if (dr.HasRows)
                            {
                                user = new UserDetails();
                                while (dr.Read())
                                {
                                    user.id = Convert.ToInt32(dr["user_registration_id"].ToString());
                                    user.user_id = dr["user_id"].ToString();
                                    user.user_name = dr["user_name"].ToString();
                                    user.user_dob = dr["user_dob"].ToString();
                                    user.user_password = dr["user_password"].ToString();
                                    user.access_type_code = Convert.ToInt32(dr["access_type_code"].ToString());
                                    if (dr["role_level_code"].ToString() != "")
                                        user.role_level_code = Convert.ToInt32(dr["role_level_code"].ToString());
                                    user.org_id = Convert.ToInt32(dr["org_id"].ToString());
                                    user.role_name_code = Convert.ToInt32(dr["role_name_code"].ToString());
                                    user.user_address = dr["user_address"].ToString();
                                    user.mobile = dr["mobile"].ToString();
                                    user.email_id = dr["email_id"].ToString();
                                    user.state_code = dr["State_code"].ToString();
                                    user.division_code = dr["Division_code"].ToString();
                                    user.financial_year = dr["financial_year"].ToString();
                                    user.district_code = dr["District_Code"].ToString();
                                    user.photoname = dr["photoname"].ToString();
                                    user.blood_group = dr["bloodgroup"].ToString();
                                    //user.comments = dr["Comments"].ToString();
                                    if (dr["emp_name"].ToString() != "" && dr["emp_name"].ToString() != null)
                                    {
                                        user.user_name = dr["emp_name"].ToString();
                                    }
                                    else
                                    {
                                        user.user_name = dr["user_name"].ToString();
                                    }
                                    user.mobile = dr["mobile"].ToString();
                                    user.email_id = dr["email_id"].ToString();
                                    user.department_name = dr["department_code"].ToString();
                                    user.party_code = dr["Party_code"].ToString();
                                    user.party_type = dr["Party_type_name"].ToString();
                                    user.role_name = dr["role_name"].ToString();
                                    user.designation_code = dr["designation_code"].ToString();
                                    user.designation_name = dr["designation_name"].ToString();
                                    user.emergency_contact = dr["emer_contact"].ToString();
                                    user.pran_no = dr["prangpf_no"].ToString();
                                    if (dr["doj"].ToString() != "" || dr["doj"].ToString() != null)
                                        user.date_of_joining = dr["doj"].ToString().Substring(0, 10).Replace("/", "-");
                                    if (dr["doj"].ToString() != "" || dr["doj"].ToString() != null)
                                        user.date_of_joining = dr["doj"].ToString().Substring(0, 10).Replace("/", "-");
                                    if (dr["start_date"].ToString() != "" || dr["start_date"].ToString() != null)
                                        user.date_of_posting = dr["start_date"].ToString().Substring(0, 10).Replace("/", "-");
                                    if (dr["end_date"].ToString() != "" || dr["end_date"].ToString() != null)
                                        user.date_of_retairment = dr["end_date"].ToString().Substring(0, 10).Replace("/", "-");
                                }
                                _log.Info("Get User Success :" + user.id + "-" + user.user_name);
                            }
                        }

                    }

                   
                }
                catch (Exception ex)
                {
                    _log.Info("Get User Fail :" + user.id + "-" + user.user_name + "-" + ex.Message);
                    user.record_status = ex.Message;
                }
            }
            return user;
        }





    }
}
