using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.Entities;

namespace Usermngt.DAL
{
  public  class DL_Tab_Master
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static string Insert(Tab_Master tab)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    StringBuilder str = new StringBuilder();

                    str.Append("INSERT INTO exciseautomation.tabname_master(tab_name, tab_desc, submodule_code, mns_no, org_id,  user_id, lastmodified_date,status)");
                    str.Append("VALUES('" + tab.tab_name + "','" + tab.tab_desc + "','" + tab.submodule_code + "','" + tab.mns_no + "','" + tab.org_id + "','" + tab.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','Created')");

                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        VAL = "0";
                        _log.Info("Insert TabNames Success :" + tab.tab_name_id + "-" + tab.tab_name);
                    }
                    else
                    {
                        VAL = "1";
                        _log.Info("Insert Tabnames Fail :" + tab.tab_name_id + "-" + tab.tab_name);
                    }
                    cn.Close();

                }
                catch (Exception ex1)
                {
                    _log.Info("Insert Tabnames Fail :" + tab.tab_name_id + "-" + tab.tab_name+"-"+ex1.Message);
                    VAL = ex1.Message;
                }

            }
            return VAL;
        }
        public static string Update(Tab_Master tab)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("Update exciseautomation.tabname_master set tab_name='"+tab.tab_name+ "',tab_desc='"+tab.tab_desc+"', submodule_code='" + tab.submodule_code + "', org_id='" + tab.org_id + "',user_id='" + tab.user_id + "', lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "',mns_no='" + tab.mns_no + "',status='Updated' where tab_name_id='" + tab.tab_name_id + "' ");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        VAL = "0";
                        _log.Info("Update TabNames Success :" + tab.tab_name_id + "-" + tab.tab_name);
                    }
                    else
                    {
                        VAL = "1";
                        _log.Info("Update Tabnames Fail :" + tab.tab_name_id + "-" + tab.tab_name);
                    }
                    cn.Close();

                }
                catch (Exception ex1)
                {
                    _log.Info("Update Tabnames Fail :" + tab.tab_name_id + "-" + tab.tab_name + "-" + ex1.Message);
                    VAL = ex1.Message;
                }

            }
            return VAL;
        }
        public static List<Tab_Master> GetList()
        {

            List<Tab_Master> tabnames = new List<Tab_Master>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.org_name,c.Module_name,d.submodule_name from exciseautomation.tabname_master a inner join exciseautomation.org_master b on a.org_id=b.org_id inner join exciseautomation.module_master c on a.mns_no=c.mns_no  inner join exciseautomation.SubModule_master d on a.submodule_code=d.submodule_code  order by a.org_id,a.mns_no,a.submodule_code,a.tab_name_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            tabnames = new List<Tab_Master>();
                            while (dr.Read())
                            {
                                Tab_Master record = new Tab_Master();
                                record.submodule_name = dr["submodule_name"].ToString();
                                record.submodule_code = dr["Submodule_code"].ToString();
                                record.org_id = dr["org_id"].ToString();
                                record.org_name = dr["org_name"].ToString();
                                record.module_name = dr["Module_name"].ToString();
                                record.mns_no = dr["mns_no"].ToString();
                                record.tab_name = dr["tab_name"].ToString();
                                record.tab_name_id =Convert.ToInt32( dr["tab_name_id"].ToString());
                                record.tab_desc= dr["tab_desc"].ToString();
                                tabnames.Add(record);
                            }
                        }
                        _log.Info("Get TabNames List Success");
                    }
                    cn.Close();
                }
                catch (Exception ex)
                {
                    _log.Info("Get TabNames List Success :"+ex.Message);
                }

            }
            return tabnames;
        }

        public static List<Tab_Master> SearchTab(string tablename, string column, string value)
        {

            List<Tab_Master> tabnames = new List<Tab_Master>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.org_name,c.Module_name,d.submodule_name from exciseautomation.tabname_master a inner join exciseautomation.org_master b on a.org_id=b.org_id inner join exciseautomation.module_master c on a.mns_no=c.mns_no  inner join exciseautomation.SubModule_master d on a.submodule_code=d.submodule_code where " + column + " Ilike '%" + value + "%'  order by a.org_id,a.mns_no,a.submodule_code,a.tab_name_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            tabnames = new List<Tab_Master>();
                            while (dr.Read())
                            {
                                Tab_Master record = new Tab_Master();
                                record.submodule_name = dr["submodule_name"].ToString();
                                record.submodule_code = dr["Submodule_code"].ToString();
                                record.org_id = dr["org_id"].ToString();
                                record.org_name = dr["org_name"].ToString();
                                record.module_name = dr["Module_name"].ToString();
                                record.mns_no = dr["mns_no"].ToString();
                                record.tab_name = dr["tab_name"].ToString();
                                record.tab_name_id = Convert.ToInt32(dr["tab_name_id"].ToString());
                                record.tab_desc = dr["tab_desc"].ToString();
                                tabnames.Add(record);
                            }
                        }
                        _log.Info("Get TabNames List Success");
                    }
                    cn.Close();
                }
                catch (Exception ex)
                {
                    _log.Info("Get TabNames List Success :" + ex.Message);
                }

            }
            return tabnames;
        }
        }
}
