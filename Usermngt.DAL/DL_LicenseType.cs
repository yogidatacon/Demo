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
  public  class DL_LicenseType
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static List<LicenseType> GetList(string userid)
        {
            List<LicenseType> ms = new List<LicenseType>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.lic_type_master  order by lic_type_code ", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        ms = new List<LicenseType>();
                        while (dr.Read())
                        {
                            LicenseType om = new LicenseType();
                            om.lic_type_code = dr["lic_type_code"].ToString();
                            om.lic_type_name = dr["lic_type_name"].ToString();
                            om.user_id = userid;
                            ms.Add(om);

                        }
                    }
                    _log.Info("Get License_Type List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get License_Type List Fail");
                }
            }
            return ms;
        }

        public static List<LicenseType> SearchLicense(string tablename, string column, string value)
        {
            List<LicenseType> ms = new List<LicenseType>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.lic_type_master where " + column + " Ilike '%" + value + "%' order by lic_type_name ", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        ms = new List<LicenseType>();
                        while (dr.Read())
                        {
                            LicenseType om = new LicenseType();
                            om.lic_type_code = dr["lic_type_code"].ToString();
                            om.lic_type_name = dr["lic_type_name"].ToString();
                            om.user_id = dr["user_id"].ToString();
                            ms.Add(om);
                        }
                    }
                    _log.Info("Get lic_type List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get lic_type List Fail");
                }
            }
            return ms;
        }
        public static string Insert(LicenseType om)
        {
            string value;

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.lic_type_master (lic_type_code, lic_type_name,  user_id, creation_date) VALUES('" + om.lic_type_code + "', '" + om.lic_type_name + "',  '" + om.user_id + "', '" + DateTime.Now.ToShortDateString() + "')", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = "0";
                        _log.Info("Insert license_type Success :" + om.lic_type_code + "-" + om.lic_type_name);
                    }
                    else
                    {
                        value = "1";
                        _log.Info("Insert license_type Fail :" + om.lic_type_code + "-" + om.lic_type_name);
                    }
                    cn.Close();

                }
                catch (Exception ex1)
                {
                    _log.Info("Insert license_type Fail :" + om.lic_type_code + "-" + om.lic_type_name + "-" + ex1.Message);
                    value = ex1.Message;
                }
                return value;

            }
        }

        public static string Update(LicenseType om)
        {
            string value;

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.lic_type_master SET  lic_type_name = '" + om.lic_type_name + "' WHERE   lic_type_code = '" + om.lic_type_code + "' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = "0";
                        _log.Info("Update License Type Success :" + om.lic_type_code + "-" + om.lic_type_name);
                    }
                    else
                    {
                        value = "1";
                        _log.Info("Update License Type Fail :" + om.lic_type_code + "-" + om.lic_type_name);
                    }
                    cn.Close();
                }
                catch (Exception ex1)
                {
                    _log.Info("Update License Type Fail :" + om.lic_type_code + "-" + om.lic_type_name + "-" + ex1.Message);
                    value = ex1.Message;
                }
                return value;

            }
        }
    }
}
