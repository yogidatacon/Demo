using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Usermngt.Entities;

namespace Usermngt.DAL
{
    public class DL_WorkFlow
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static List<WorkFlow> GetSubModules()
        {
            List<WorkFlow> submodules = new List<WorkFlow>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select distinct Submodule_name,Submodule_Code from exciseautomation.submodule_master order by Submodule_name", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                WorkFlow submodule = new WorkFlow();
                                submodule.submodule_name = dr["Submodule_name"].ToString();
                                submodule.submodule_code = dr["Submodule_Code"].ToString();
                                submodules.Add(submodule);
                            }
                            _log.Info("Get SubModule In WorkFlow Success");
                        }
                    }
                    cn.Close();
                }
                catch (Exception ex)
                {
                    _log.Info("Get SubModule In WorkFlow Fail :"+ex.Message);
                }
                return submodules;
            }
        }
        public static List<WorkFlow> GetTabNames(string role_name_code)
        {
            List<WorkFlow> tabnames = new List<WorkFlow>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select distinct tab_name_id,tab_name from exciseautomation.tabname_master where submodule_code='"+role_name_code+"' order by tab_name", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                WorkFlow tabname = new WorkFlow();
                                tabname.tab_id = dr["tab_name_id"].ToString();
                                tabname.tab_name = dr["tab_name"].ToString();
                                tabnames.Add(tabname);
                            }
                            _log.Info("Get TabName In WorkFlow Success");
                        }
                    }
                    cn.Close();
                }
                catch (Exception ex)
                {
                    _log.Info("Get TabName In WorkFlow Fail :" + ex.Message);
                }
                return tabnames;
            }
        }
        public static List<WorkFlow> GetRoleNames()
        {
            List<WorkFlow> rolenames = new List<WorkFlow>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select distinct role_name_code,role_name from exciseautomation.role_master order by role_name", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                WorkFlow rolename = new WorkFlow();
                                rolename.role_name_code = dr["role_name_code"].ToString();
                                rolename.role_name = dr["role_name"].ToString();
                                rolenames.Add(rolename);
                            }
                            _log.Info("Get RoleNames In WorkFlow Success");
                        }
                    }
                    cn.Close();
                }
                catch (Exception ex)
                {
                    _log.Info("Get RoleNames In WorkFlow Fail :" + ex.Message);
                }
                return rolenames;
            }
        }
        public static List<WorkFlow> GetDistricts()
        {
            List<WorkFlow> districts = new List<WorkFlow>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select distinct District_name,District_Code from exciseautomation.district_master order by District_name", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                WorkFlow dist = new WorkFlow();
                                dist.district_code = dr["District_Code"].ToString();
                                dist.district = dr["District_name"].ToString();
                                districts.Add(dist);
                            }
                            _log.Info("Get District In WorkFlow Success");
                        }
                    }
                    cn.Close();
                }
                catch (Exception ex)
                {
                    _log.Info("Get District In WorkFlow Fail :" + ex.Message);
                }
                return districts;
            }
        }
        public static List<WorkFlow> GetUserNames(int role_name_code)
        {
            List<WorkFlow> users = new List<WorkFlow>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select distinct user_name,user_registration_id from exciseautomation.user_registration where role_name_code='"+role_name_code+"' order by user_name", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                WorkFlow user = new WorkFlow();
                                user.username = dr["user_name"].ToString();
                                user.user_registration_id = dr["user_registration_id"].ToString();
                                users.Add(user);
                            }
                            _log.Info("Get UserNames In WorkFlow Success");
                        }
                    }
                    cn.Close();
                }
                catch (Exception ex)
                {
                    _log.Info("Get UserNames In WorkFlow Fail :" + ex.Message);
                }
                return users;
            }
        }
        public static List<WorkFlow> Getworkflowlist(string username)
        {
            List<WorkFlow> workflowlist = new List<WorkFlow>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select distinct a.work_flow_id,a.district_code,a.submodule_code,a.tab_name_id,a.txn,d.district_name,b.tab_name,c.submodule_name,e.role_name,f.user_name from exciseautomation.workflow a inner join  exciseautomation.tabname_master b on a.tab_name_id = b.tab_name_id and a.record_deleted=false inner join exciseautomation.submodule_master c on a.submodule_code = c.submodule_code  inner join exciseautomation.district_master d on a.district_code = d.district_code inner join exciseautomation.role_master e on a.role_name_code=e.role_name_code inner join exciseautomation.user_registration f on a.user_registration_id=f.user_registration_id order by a.work_flow_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                WorkFlow flow = new WorkFlow();
                               
                                flow.submodule_name = dr["submodule_name"].ToString();
                                flow.tab_name = dr["tab_name"].ToString();
                                flow.district = dr["district_name"].ToString();
                                flow.district_code= dr["district_code"].ToString();
                                flow.submodule_code = dr["submodule_code"].ToString();
                                flow.tab_id = dr["tab_name_id"].ToString();
                                 flow.user_id = dr["User_name"].ToString();
                                flow.id =Convert.ToInt32( dr["txn"].ToString());
                                flow.role_name = dr["role_name"].ToString();
                                workflowlist.Add(flow);
                            }
                            _log.Info("Get WorkFlow List Success");
                        }
                    }
                    cn.Close();
                }
                catch (Exception ex)
                {
                    _log.Info("Get WorkFlow List Fail :" + ex.Message);
                }
                return workflowlist;
            }
        }
        public static List<WorkFlow> SearchWorkFlow(string tablename, string column, string value)
        {
            List<WorkFlow> workflowlist = new List<WorkFlow>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select distinct a.work_flow_id,a.district_code,a.submodule_code,a.tab_name_id,a.txn,d.district_name,b.tab_name,c.submodule_name,e.role_name,f.user_name from exciseautomation.workflow a inner join  exciseautomation.tabname_master b on a.tab_name_id = b.tab_name_id and a.record_deleted=false inner join exciseautomation.submodule_master c on a.submodule_code = c.submodule_code  inner join exciseautomation.district_master d on a.district_code = d.district_code inner join exciseautomation.role_master e on a.role_name_code=e.role_name_code inner join exciseautomation.user_registration f on a.user_registration_id=f.user_registration_id where " + column + " Ilike '%" + value + "%' order by a.work_flow_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                WorkFlow flow = new WorkFlow();

                                flow.submodule_name = dr["submodule_name"].ToString();
                                flow.tab_name = dr["tab_name"].ToString();
                                flow.district = dr["district_name"].ToString();
                                flow.district_code = dr["district_code"].ToString();
                                flow.submodule_code = dr["submodule_code"].ToString();
                                flow.tab_id = dr["tab_name_id"].ToString();
                                flow.user_id = dr["User_name"].ToString();
                                flow.id = Convert.ToInt32(dr["txn"].ToString());
                                flow.role_name = dr["role_name"].ToString();
                                workflowlist.Add(flow);
                            }
                            _log.Info("Get WorkFlow List Success");
                        }
                    }
                    cn.Close();
                }
                catch (Exception ex)
                {
                    _log.Info("Get WorkFlow List Fail :" + ex.Message);
                }
                return workflowlist;
            }
        }

        public static List<WorkFlow> Getworkflow(int txn)
        {
            List<WorkFlow> workflowlist = new List<WorkFlow>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("(select a.*,d.district_name,b.tab_name,c.submodule_name,f.user_name from exciseautomation.workflow a inner join  exciseautomation.tabname_master b on a.tab_name_id = b.tab_name_id and a.txn='" + txn + "' and a.record_deleted=false inner join exciseautomation.submodule_master c on a.submodule_code = c.submodule_code  inner join exciseautomation.district_master d on a.district_code = d.district_code inner join  exciseautomation.user_registration f on a.user_registration_id=f.user_registration_id)    order by work_flow_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                WorkFlow flow = new WorkFlow();
                                flow.id = Convert.ToInt32(dr["work_flow_id"].ToString());
                                flow.submodule_name = dr["submodule_name"].ToString();
                                flow.tab_name = dr["tab_name"].ToString();
                                flow.district_code = dr["district_code"].ToString();
                                flow.approver_level = dr["approver_level"].ToString();
                                flow.role_name_code = dr["role_name_code"].ToString();
                                flow.user_id = dr["User_id"].ToString();
                                flow.submodule_code = dr["submodule_code"].ToString();
                                flow.tab_id = dr["tab_name_id"].ToString();
                                flow.user_registration_id = dr["user_registration_id"].ToString();
                                flow.username = dr["User_name"].ToString();
                                flow.district = dr["District_name"].ToString();
                                workflowlist.Add(flow);
                            }
                            _log.Info("Get WorkFlow  Success");
                        }
                    }
                    cn.Close();
                }
                catch (Exception ex)
                {
                    _log.Info("Get WorkFlow  Fail :" + ex.Message);
                }
                return workflowlist;
            }
        }
        public static List<WorkFlow> Checkworkflow(string submodule_code, string tab_id, string role_name_code, string district_code,string userid)
        {
            List<WorkFlow> workflowlist = new List<WorkFlow>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.workflow where submodule_code='"+submodule_code+"' and tab_name_id='"+tab_id+"' and role_name_code='"+role_name_code+"' and  user_registration_id='" +userid+"' ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                WorkFlow flow = new WorkFlow();
                                flow.id = Convert.ToInt32(dr["work_flow_id"].ToString());
                                flow.district_code = dr["district_code"].ToString();
                                flow.approver_level = dr["approver_level"].ToString();
                                flow.role_name_code = dr["role_name_code"].ToString();
                                flow.user_id = dr["User_id"].ToString();
                                flow.submodule_code = dr["submodule_code"].ToString();
                                flow.tab_id = dr["tab_name_id"].ToString();
                                flow.user_registration_id = dr["user_registration_id"].ToString();
                                workflowlist.Add(flow);
                            }
                            _log.Info("Check WorkFlow List Success");
                        }
                    }
                    cn.Close();
                }
                catch (Exception ex)
                {
                    _log.Info("Check WorkFlow List Fail :" + ex.Message);
                }
                return workflowlist;
            }
        }
        public static List<WorkFlow> ApprovelLevels(string submodule_code, string tab_id)
        {
            List<WorkFlow> workflowlist = new List<WorkFlow>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select distinct a.user_registration_id,a.district_code,a.approver_level,a.role_name_code,a.submodule_code,a.tab_name_id,b.role_name from exciseautomation.workflow a inner join  exciseautomation.role_master b on a.role_name_code=b.role_name_code  where submodule_code='" + submodule_code + "' and tab_name_id='" + tab_id + "' and a.record_deleted=false", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                WorkFlow flow = new WorkFlow();
                              //  flow.id = Convert.ToInt32(dr["work_flow_id"].ToString());
                                flow.district_code = dr["district_code"].ToString();
                                flow.approver_level = dr["approver_level"].ToString();
                                flow.role_name_code = dr["role_name_code"].ToString();
                               // flow.user_id = dr["User_id"].ToString();
                                flow.submodule_code = dr["submodule_code"].ToString();
                                flow.tab_id = dr["tab_name_id"].ToString();
                                flow.role_name = dr["role_name"].ToString();
                                flow.user_registration_id = dr["user_registration_id"].ToString();
                                workflowlist.Add(flow);
                            }
                            _log.Info("Check WorkFlow List Success");
                        }
                    }
                    cn.Close();
                }
                catch (Exception ex)
                {
                    _log.Info("Check WorkFlow List Fail :" + ex.Message);
                }
                return workflowlist;
            }
        }
        public static string InsertWorkFlow(List<WorkFlow> workflow)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
               
                try
                {
                    int max = (DL_RolePermisions.GetMaxID("workflow"))+1;
                    workflow[0].id = max;
                    for (int i = 0; i < workflow.Count; i++)
                    {

                        StringBuilder str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.workflow(work_flow_id, district_code,approver_level,txn , org_id, role_name_code, status, submodule_code, tab_name_id,user_registration_id , lastmodified_date,  creation_date, user_id)Values(");
                        str.Append("'" + max + "','" + workflow[i].district_code + "','" + workflow[i].approver_level + "','" + workflow[0].id + "','" + workflow[i].org_id + "','" + workflow[i].role_name_code + "','Active','" + workflow[i].submodule_code + "','" + workflow[i].tab_id + "','" + workflow[i].user_registration_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + workflow[i].user_id + "')");
                        NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                        int n = cmd.ExecuteNonQuery();
                        max++;
                        VAL = "1";
                        _log.Info("Insert WorkFlow Success :"+workflow[i].id+"-"+workflow[i].role_name);
                    }
                    cn.Close();
                    return VAL;
                }
                catch (Exception ex)
                {
                    _log.Info("Insert WorkFlow Success :" +ex.Message);
                    return VAL=ex.Message;
                }
               
            }
        }
        public static string UpdateWorkFlow(List<WorkFlow> workflow)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();

                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("update exciseautomation.workflow set record_deleted='true' where txn='" + workflow[0].id + "'", cn);
                    cmd.ExecuteNonQuery();
                    cmd = new NpgsqlCommand("select max(work_flow_id) from exciseautomation.workflow", cn);
                    int id =Convert.ToInt32( cmd.ExecuteScalar())+1;
                    for (int i = 0; i < workflow.Count; i++)
                    {
                        StringBuilder str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.workflow(work_flow_id, district_code,approver_level,txn , org_id, role_name_code, status, submodule_code, tab_name_id,user_registration_id , lastmodified_date,  creation_date, user_id)Values(");
                        str.Append("'" +(id)+ "','" + workflow[i].district_code + "','" + workflow[i].approver_level + "','" + workflow[0].id + "','" + workflow[i].org_id + "','" + workflow[i].role_name_code + "','Active','" + workflow[i].submodule_code + "','" + workflow[i].tab_id + "','" + workflow[i].user_registration_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + workflow[i].user_id + "')");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        int n = cmd.ExecuteNonQuery();
                        id++;
                        VAL = "1";
                        _log.Info("Update WorkFlow Success :" + workflow[i].id + "-" + workflow[i].role_name);
                    }
                    cn.Close();
                    return VAL;
                }
                catch (Exception ex)
                {
                    _log.Info("Update WorkFlow Success :" + ex.Message);
                    return VAL = ex.Message;
                }
            }
        }
        public static string InsertReport(Reportmaster report)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("Select Count(1) from exciseautomation.submodule_master where mns_no='" + report.mns_no + "' and submodule_name='Reports' and org_id='" + report.org_id + "'", cn);
                    VAL = cmd.ExecuteScalar().ToString();
                    StringBuilder str = new StringBuilder();
                    int n;
                    //if (VAL == "" || VAL == "0")
                    //{

                    //    str = new StringBuilder();
                    //    str.Append("INSERT INTO exciseautomation.submodule_master( submodule_code, submodule_name, submodule_desc, mns_no, org_id, status, user_id, lastmodified_date)Values(");
                    //    str.Append("'RPT-" + report.mns_no + "','Reports','Reports','" + report.mns_no + "','" + report.org_id + "','" + report.reportstatus + "','" + report.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "')");
                    //    cmd = new NpgsqlCommand(str.ToString(), cn);
                    //    n = cmd.ExecuteNonQuery();
                    //    _log.Info("Insert Report Success :" + report.mns_no + "-" + report.reportname);
                    //}
                    //cmd = new NpgsqlCommand("Select Count(1) from exciseautomation.tabname_master where submodule_code='+ RPT-" + report.mns_no + "' and  mns_no='" + report.mns_no + "' and tab_name='" + report.reportname + "' and org_id='" + report.org_id + "'", cn);
                    //VAL = cmd.ExecuteScalar().ToString();
                    //if (VAL == "" || VAL == "0")
                    //{
                    //    str = new StringBuilder();
                    //    str.Append("INSERT INTO exciseautomation.tabname_master( tab_name, tab_desc, submodule_code, mns_no, org_id, status, user_id, lastmodified_date)Values(");
                    //    str.Append("'" + report.reportname + "','" + report.reportname + "','RPT-" + report.mns_no + "','" + report.mns_no + "','" + report.org_id + "','" + report.reportstatus + "','" + report.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "')");
                    //    cmd = new NpgsqlCommand(str.ToString(), cn);
                    //    n = cmd.ExecuteNonQuery();
                    //    _log.Info("Insert Report Success :" + report.mns_no + "-" + report.reportname);
                    //}
                    
                    cmd = new NpgsqlCommand("Select Count(1) from exciseautomation.report_master where  report_name='" + report.reportname + "' ", cn);
                    VAL = cmd.ExecuteScalar().ToString();
                    int rmax = (DL_RolePermisions.GetMaxID("Report_master")) + 1;
                    if (VAL == "" || VAL == "0")
                    {
                        if (report.reportstatus == "Active")
                            report.reportstatus = "True";
                        else
                            report.reportstatus = "False";
                        str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.report_master(report_id, org_id, mns_no, role_name_code, report_name, lastmodified_date, createdby_id, creation_date, user_id, report_status,  report_path,raw_report_name,party_type_code)Values(");
                        str.Append("'" + rmax + "','" + report.org_id + "','" + report.mns_no + "','" + report.role_name_code + "','" + report.reportname + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + report.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + report.user_id + "','" + report.reportstatus + "','" + report.report_path + "','"+report.reportfilename+"','"+report.partytype+"')");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        n = cmd.ExecuteNonQuery();
                        VAL = "1";
                        _log.Info("Insert Report Success :" + report.mns_no + "-" + report.reportname);
                    }
                    else
                    {
                        VAL = "2";
                        _log.Info("Insert Report Fail :" + report.mns_no + "-" + report.reportname);
                    }

                    cn.Close();
                    return VAL;
                }
                catch (Exception ex)
                {
                    _log.Info("Insert Report Fail :" + report.mns_no + "-" + report.reportname);
                    return VAL = ex.Message;
                }

            }
        }

        public static string UpdateReport(Reportmaster report)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("Select Count(1) from exciseautomation.submodule_master where mns_no='" + report.mns_no + "' and submodule_name='Reports' and org_id='" + report.org_id + "'", cn);
                    VAL = cmd.ExecuteScalar().ToString();
                    StringBuilder str = new StringBuilder();
                    int n;
                    //if (VAL == "" || VAL == "0")
                    //{

                    //    str = new StringBuilder();
                    //    str.Append("INSERT INTO exciseautomation.submodule_master( submodule_code, submodule_name, submodule_desc, mns_no, org_id, status, user_id, lastmodified_date)Values(");
                    //    str.Append("'RPT-" + report.mns_no + "','Reports','Reports','" + report.mns_no + "','" + report.org_id + "','" + report.reportstatus + "','" + report.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "')");
                    //    cmd = new NpgsqlCommand(str.ToString(), cn);
                    //    n = cmd.ExecuteNonQuery();
                    //    _log.Info("Insert Report Success :" + report.mns_no + "-" + report.reportname);
                    //}
                    //cmd = new NpgsqlCommand("Select Count(1) from exciseautomation.tabname_master where submodule_code='+ RPT-" + report.mns_no + "' and  mns_no='" + report.mns_no + "' and tab_name='" + report.reportname + "' and org_id='" + report.org_id + "'", cn);
                    //VAL = cmd.ExecuteScalar().ToString();
                    //if (VAL == "" || VAL == "0")
                    //{
                    //    str = new StringBuilder();
                    //    str.Append("INSERT INTO exciseautomation.tabname_master( tab_name, tab_desc, submodule_code, mns_no, org_id, status, user_id, lastmodified_date)Values(");
                    //    str.Append("'" + report.reportname + "','" + report.reportname + "','RPT-" + report.mns_no + "','" + report.mns_no + "','" + report.org_id + "','" + report.reportstatus + "','" + report.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "')");
                    //    cmd = new NpgsqlCommand(str.ToString(), cn);
                    //    n = cmd.ExecuteNonQuery();
                    //    _log.Info("Insert Report Success :" + report.mns_no + "-" + report.reportname);
                    //}

                    cmd = new NpgsqlCommand("Select Count(1) from exciseautomation.report_master where  report_name='" + report.reportname + "' ", cn);
                    VAL = cmd.ExecuteScalar().ToString();
                    int rmax = (DL_RolePermisions.GetMaxID("Report_master")) + 1;
                    if (VAL == "1" || VAL == "0")
                    {
                        if (report.reportstatus == "Active")
                            report.reportstatus = "True";
                        else
                            report.reportstatus = "False";
                        str = new StringBuilder();
                        //str.Append("INSERT INTO exciseautomation.report_master(report_id, org_id, mns_no, role_name_code, report_name, lastmodified_date, createdby_id, creation_date, user_id, report_status,  report_path,raw_report_name,party_type_code)Values(");
                        //str.Append("'" + rmax + "','" + report.org_id + "','" + report.mns_no + "','" + report.role_name_code + "','" + report.reportname + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + report.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + report.user_id + "','" + report.reportstatus + "','" + report.report_path + "','" + report.reportfilename + "','" + report.partytype + "')");
                        cmd = new NpgsqlCommand("UPDATE exciseautomation.report_master SET  org_id ='"+report.org_id+"', mns_no ='"+report.mns_no+"', role_name_code ='"+report.role_name_code+"', report_name ='"+report.reportname+"', lastmodified_date ='"+DateTime.Now.ToShortDateString()+"', user_id ='"+report.user_id+"', report_status ='"+report.reportstatus+"', report_path ='"+report.report_path+"', raw_report_name ='"+report.reportfilename+"', party_type_code ='"+report.partytype+"' WHERE report_id ='"+report.id+"' ", cn);
                        n = cmd.ExecuteNonQuery();
                        VAL = "1";
                        _log.Info("Insert Report Success :" + report.mns_no + "-" + report.reportname);
                    }
                    else
                    {
                        VAL = "2";
                        _log.Info("Insert Report Fail :" + report.mns_no + "-" + report.reportname);
                    }

                    cn.Close();
                    return VAL;
                }
                catch (Exception ex)
                {
                    _log.Info("Insert Report Fail :" + report.mns_no + "-" + report.reportname);
                    return VAL = ex.Message;
                }

            }
        }
        public static List<Reportmaster> GetReportlist()
        {
            List<Reportmaster> reportlist = new List<Reportmaster>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.Module_name,c.party_type_name from exciseautomation.report_master a inner join exciseautomation.module_master b on a.mns_no=b.mns_no and a.record_deleted=false left join exciseautomation.party_type_master c on c.party_type_code=a.party_type_code", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            reportlist = new List<Reportmaster>();

                            while (dr.Read())
                            {
                                Reportmaster report = new Reportmaster();
                                report.id =Convert.ToInt32( dr["Report_id"].ToString());
                                report.org_id = Convert.ToInt32(dr["Org_Id"].ToString());
                                report.mns_no = Convert.ToInt32(dr["mns_no"].ToString());
                                report.role_name_code = Convert.ToInt32(dr["role_name_code"].ToString());
                                 report.reportname = dr["report_name"].ToString();
                                report.report_path = dr["Report_path"].ToString();
                                report.module_name = dr["Module_name"].ToString();
                                report.partytype = dr["party_type_name"].ToString();
                                report.partytypecode = dr["party_type_code"].ToString();
                                report.reportfilename = dr["raw_report_name"].ToString();
                                if (dr["Report_status"].ToString()=="True")
                                report.reportstatus ="Active";
                                else
                                    report.reportstatus = "InActive";
                                reportlist.Add(report);
                            }
                            _log.Info("Get Report List Success" );
                        }
                    }
                    cn.Close();
                }
                catch (Exception ex)
                {
                    _log.Info("Get Report List Fail :"+ex.Message);
                }
                return reportlist;
            }
        }
        public static List<Reportmaster> Search(string tablename, string column, string value)
        {
            List<Reportmaster> mir = new List<Reportmaster>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.Module_name,c.party_type_name from exciseautomation.report_master a inner join exciseautomation.module_master b on a.mns_no=b.mns_no and a.record_deleted=false left join exciseautomation.party_type_master c on c.party_type_code=a.party_type_code where  " + column + " Ilike '%" + value + "%'  order by   report_name desc", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            mir = new List<Reportmaster>();
                            while (dr.Read())
                            {
                                Reportmaster report = new Reportmaster();
                                report.id = Convert.ToInt32(dr["Report_id"].ToString());
                                report.org_id = Convert.ToInt32(dr["Org_Id"].ToString());
                                report.mns_no = Convert.ToInt32(dr["mns_no"].ToString());
                                report.role_name_code = Convert.ToInt32(dr["role_name_code"].ToString());
                                report.reportname = dr["report_name"].ToString();
                                report.report_path = dr["Report_path"].ToString();
                                report.module_name = dr["Module_name"].ToString();
                                report.partytype = dr["party_type_name"].ToString();
                                report.reportfilename = dr["raw_report_name"].ToString();
                                if (dr["Report_status"].ToString() == "True")
                                    report.reportstatus = "Active";
                                else
                                    report.reportstatus = "InActive";
                                mir.Add(report);


                            }
                        }
                    }
                    cn.Close();

                }
                catch (Exception ex)
                {
                    throw (ex);
                }

            }
            return mir;
        }

        public static List<Reportmaster> GetReports(string username)
        {
            List<Reportmaster> reportlist = new List<Reportmaster>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    // username = "Siva123";
                    if (username == "Admin"|| username == "ALL")
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("select distinct a.report_id,a.report_name,a.raw_report_name,c.party_type_name from exciseautomation.report_master a left join exciseautomation.party_type_master c on c.party_type_code=a.party_type_code ", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            //cmd.Parameters.AddWithValue("@UserID", userid);
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                reportlist = new List<Reportmaster>();

                                while (dr.Read())
                                {
                                    Reportmaster report = new Reportmaster();
                                    report.id = Convert.ToInt32(dr["Report_id"].ToString());
                                    report.reportname = dr["report_name"].ToString();
                                    report.reportfilename = dr["raw_report_name"].ToString();
                                    report.partytype = dr["party_type_name"].ToString();
                                    reportlist.Add(report);
                                }
                                _log.Info("Get Report  Success");
                            }
                        }
                        cn.Close();
                    }
                    else
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("select distinct a.report_id,a.report_name,a.raw_report_name,c.party_type_name from exciseautomation.report_master a left join exciseautomation.party_type_master c on c.party_type_code=a.party_type_code   ", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            //cmd.Parameters.AddWithValue("@UserID", userid);
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                reportlist = new List<Reportmaster>();

                                while (dr.Read())
                                {
                                    Reportmaster report = new Reportmaster();
                                    report.id = Convert.ToInt32(dr["Report_id"].ToString());
                                    report.reportname = dr["report_name"].ToString();
                                    report.reportfilename = dr["raw_report_name"].ToString();
                                    report.partytype = dr["party_type_name"].ToString();
                                    reportlist.Add(report);
                                }
                                _log.Info("Get Report  Success");
                            }
                        }
                        cn.Close();
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Get Report  Fail :"+ex.Message);
                }
                return reportlist;
            }
        }
    }
}
