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
  public  class DL_SystemSetting
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static List<SystemSetting> GetList(string userid)
        {
            List<SystemSetting> products = new List<SystemSetting>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.system_setting  ", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        products = new List<SystemSetting>();
                        while (dr.Read())
                        {
                            SystemSetting product = new SystemSetting();
                            product.system_setting_id = Convert.ToInt32( dr["system_setting_id"].ToString());
                            product.parameter_name = dr["parameter_name"].ToString();
                            product.parameter_value_num= Convert.ToInt32( dr["parameter_value_num"].ToString());
                            product.parameter_value_str = dr["parameter_value_str"].ToString();
                            product.user_id = userid;
                            products.Add(product);

                        }
                    }
                    _log.Info("Get Product Type Master List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Product Type Master List Success :" + ex.Message);
                }
            }
            return products;
        }
        public static List<SystemSetting> Searchsys(string tablename, string column, string value)
        {
            List<SystemSetting> products = new List<SystemSetting>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.system_setting where " + column + " Ilike '%" + value + "%' order by system_setting_id  ", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        products = new List<SystemSetting>();
                        while (dr.Read())
                        {
                          SystemSetting product = new SystemSetting();
                            product.system_setting_id = Convert.ToInt32(dr["system_setting_id"].ToString());
                            product.parameter_name = dr["parameter_name"].ToString();
                            product.parameter_value_num = Convert.ToInt32(dr["parameter_value_num"].ToString());
                            product.parameter_value_str = dr["parameter_value_str"].ToString();
                            product.user_id = dr["user_id"].ToString(); ;
                            // product.user_id = userid;
                            products.Add(product);

                        }
                    }
                    _log.Info("Get Product Type Master List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Product Type Master List Success :" + ex.Message);
                }
            }
            return products;

        }
       

        public static bool InsertSys(SystemSetting product)
        {
            bool value = false;

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.system_setting(parameter_name, parameter_value_num, parameter_value_str, creation_date, user_id) VALUES('"+product.parameter_name+"', '"+product.parameter_value_num+"', '"+product.parameter_value_str+"','"+DateTime.Now.ToShortDateString()+"', '"+product.user_id+"') ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                        _log.Info("Insert Department Master Success:" + product.parameter_name + "-" + product.parameter_value_num);
                    }
                    else
                    {
                        value = false;
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Insert Department Success:" + product.parameter_name + "-" + product.parameter_value_num + "-" + ex.Message);
                    value = false;
                }
                return value;

            }
        }


      

        public static bool UpdateSys(SystemSetting product)
        {
            bool value = false;

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.system_setting SET parameter_name ='"+product.parameter_name+"', parameter_value_num ='"+product.parameter_value_num+"', parameter_value_str ='"+product.parameter_value_str+"', lastmodified_date ='"+DateTime.Now.ToShortDateString()+"' WHERE system_setting_id ='"+product.system_setting_id+"'", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                        _log.Info("Update Department Master Success:" + product.parameter_name + "-" + product.parameter_value_num);
                    }
                    else
                    {
                        value = false;
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Insert Department Master Success:" + product.parameter_name + "-" + product.parameter_value_num + "-" + ex.Message);
                    value = false;
                }
                return value;

            }
        }


        public static List<SystemSetting> serversetting(string userid)
        {
            List<SystemSetting> products = new List<SystemSetting>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.config_master  ", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        products = new List<SystemSetting>();
                        while (dr.Read())
                        {
                            SystemSetting product = new SystemSetting();
                            product.system_setting_id = Convert.ToInt32(dr["system_setting_id"].ToString());
                            product.parameter_name = dr["parameter_name"].ToString();
                            product.parameter_value_num = Convert.ToInt32(dr["parameter_value_num"].ToString());
                            product.parameter_value_str = dr["parameter_value_str"].ToString();
                            product.user_id = userid;
                            products.Add(product);

                        }
                    }
                    _log.Info("Get Product Type Master List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Product Type Master List Success :" + ex.Message);
                }
            }
            return products;
        }

    }
}
