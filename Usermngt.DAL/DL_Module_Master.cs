

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
  public  class DL_Module_Master
    {
      static  NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static string Insert(Module_Master module)
        {
           
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                       int max =Convert.ToInt32( DL_Org_List.GetMaxID("Module_master").ToString())+1;
                        StringBuilder str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.module_master(mns_no,module_code, module_name,  org_id, lastmodified_date,status,  user_id)");
                        str.Append("VALUES('" + max+ "','" + module.module_code + "','" + module.module_name + "','" + module.org_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','Created','" + module.user_id + "')");
                  
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        VAL = "0";
                    }
                    else
                        VAL = "1";
                    cn.Close();
                    _log.Info("Module Insertion Sucess:" + module.module_code + '-' + module.module_name);

                }
                catch (Exception ex1)
                {
                    _log.Info("Module Insertion Fail:" + module.module_code + '-' + module.module_name+"-"+ ex1.Message);

                    VAL = ex1.Message;
                }

            }
            return VAL;
        }
        public static string Update(Module_Master module)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("Update exciseautomation.module_master set  module_name='" + module.module_name + "', org_id='" + module.org_id + "',user_id='" + module.user_id + "', lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "',status='Updated' where module_code='" + module.module_code + "' ");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        VAL = "0";
                    }
                    else
                        VAL = "1";
                    cn.Close();
                    _log.Info("Module Update Success:" + module.module_code + '-' + module.module_name);
                }
                catch (Exception ex1)
                {
                    _log.Info("Module Update Fail :" + module.module_code + '-' + module.module_name+"-"+ex1.Message);
                    VAL = ex1.Message;
                }

            }
            return VAL;
        }
        public static List<Module_Master> GetList()
        {

            List<Module_Master> modules = new List<Module_Master>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.org_name from exciseautomation.Module_master a inner join exciseautomation.org_master b on a.org_id=b.org_id  order by a.org_id,a.mns_no", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            modules = new List<Module_Master>();
                            while (dr.Read())
                            {
                                Module_Master record = new Module_Master();
                                record.module_name = dr["module_name"].ToString();
                                record.module_code = dr["module_code"].ToString();
                                record.org_id = dr["org_id"].ToString();
                                record.org_name = dr["org_name"].ToString();
                                record.mns_no = dr["mns_no"].ToString();
                                modules.Add(record);
                            }
                        }
                    }
                    _log.Info("Module Get List Success");
                    cn.Close();
                }
                catch (Exception ex)
                {
                    _log.Info("Module Get List Fail :"+ex.Message);
                }

            }
            return modules;
        }
        public static List<Module_Master> SearchModule(string tablename, string column, string value)
        {
            List<Module_Master> modules = new List<Module_Master>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.org_name from exciseautomation.Module_master a inner join exciseautomation.org_master b on a.org_id=b.org_id where a." + column + " Ilike '%" + value + "%'  order by a.org_id,a.module_name", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            modules = new List<Module_Master>();
                            while (dr.Read())
                            {
                                Module_Master record = new Module_Master();
                                record.module_name = dr["module_name"].ToString();
                                record.module_code = dr["module_code"].ToString();
                                record.org_id = dr["org_id"].ToString();
                                record.org_name = dr["org_name"].ToString();
                                record.mns_no = dr["mns_no"].ToString();
                                modules.Add(record);
                            }
                        }
                    }
                    _log.Info("Module Get List Success");
                    cn.Close();
                }
                catch (Exception ex)
                {
                    _log.Info("Module Get List Fail :" + ex.Message);
                }

            }
            return modules;
        }
        }
}
