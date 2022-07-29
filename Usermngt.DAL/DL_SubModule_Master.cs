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
   public class DL_SubModule_Master
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static string Insert(SubModule_Master submodule)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    StringBuilder str = new StringBuilder();

                    str.Append("INSERT INTO exciseautomation.submodule_master(submodule_code, submodule_name,mns_no,  org_id, lastmodified_date,status,  user_id)");
                    str.Append("VALUES('" + submodule.submodule_code + "','" + submodule.submodule_name + "','" + submodule.mns_no + "','" + submodule.org_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','Created','" + submodule.user_id + "')");

                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        VAL = "0";
                        _log.Info("Insert SubModule Success :" + submodule.submodule_code + "-" + submodule.submodule_name);
                    }
                    else
                    {
                        VAL = "1";
                        _log.Info("Insert SubModule Fail :" + submodule.submodule_code + "-" + submodule.submodule_name);
                    }
                    cn.Close();

                }
                catch (Exception ex1)
                {
                    _log.Info("Insert SubModule Fail :" + submodule.submodule_code + "-" + submodule.submodule_name+"-"+ex1.Message);
                    VAL = ex1.Message;
                }

            }
            return VAL;
        }
        public static string Update(SubModule_Master submodule)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("Update exciseautomation.submodule_master set  submodule_name='" + submodule.submodule_name + "', org_id='" + submodule.org_id + "',user_id='" + submodule.user_id + "', lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "',mns_no='"+ submodule.mns_no+ "',status='Updated' where submodule_code='" + submodule.submodule_code + "' ");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        VAL = "0";
                        _log.Info("Update SubModule Success :" + submodule.submodule_code + "-" + submodule.submodule_name);
                    }
                    else
                    {
                        VAL = "1";
                        _log.Info("Update SubModule Fail :" + submodule.submodule_code + "-" + submodule.submodule_name);
                    }
                    cn.Close();

                }
                catch (Exception ex1)
                {
                    _log.Info("Update SubModule Fail :" + submodule.submodule_code + "-" + submodule.submodule_name + "-" + ex1.Message);
                    VAL = ex1.Message;
                }

            }
            return VAL;
        }
        public static List<SubModule_Master> GetList()
        {

            List<SubModule_Master> submodules = new List<SubModule_Master>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.org_name,c.Module_name from exciseautomation.SubModule_master a inner join exciseautomation.org_master b on a.org_id=b.org_id inner join exciseautomation.module_master c on a.mns_no=c.mns_no  order by a.org_id,a.mns_no,a.submodule_code", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            submodules = new List<SubModule_Master>();
                            while (dr.Read())
                            {
                                SubModule_Master record = new SubModule_Master();
                                record.submodule_name = dr["Submodule_name"].ToString();
                                record.submodule_code = dr["Submodule_code"].ToString();
                                record.org_id = dr["org_id"].ToString();
                                record.org_name = dr["org_name"].ToString();
                                record.module_name = dr["Module_name"].ToString();
                                record.mns_no = dr["mns_no"].ToString();
                                submodules.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get SubModule Sucess");
                }
                catch (Exception ex)
                {
                    _log.Info("Get SubModule ListFail :" + ex.Message);
                }

            }
            return submodules;
        }
        public static List<SubModule_Master> SearchSubModule(string tablename, string column, string value)
        {
            List<SubModule_Master> submodules = new List<SubModule_Master>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.org_name,c.Module_name from exciseautomation.SubModule_master a inner join exciseautomation.org_master b on a.org_id=b.org_id inner join exciseautomation.module_master c on a.mns_no=c.mns_no where " + column + " Ilike '%" + value + "%'   order by a.org_id,a.mns_no,a.submodule_name", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            submodules = new List<SubModule_Master>();
                            while (dr.Read())
                            {
                                SubModule_Master record = new SubModule_Master();
                                record.submodule_name = dr["Submodule_name"].ToString();
                                record.submodule_code = dr["Submodule_code"].ToString();
                                record.org_id = dr["org_id"].ToString();
                                record.org_name = dr["org_name"].ToString();
                                record.module_name = dr["Module_name"].ToString();
                                record.mns_no = dr["mns_no"].ToString();
                                submodules.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get SubModule Sucess");
                }
                catch (Exception ex)
                {
                    _log.Info("Get SubModule ListFail :" + ex.Message);
                }

            }
            return submodules;
        }
        }
}
