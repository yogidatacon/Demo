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
  public  class DL_LicenseSubType
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static List<LicenseSubType> GetList(string userid)
        {
            List<LicenseSubType> ms = new List<LicenseSubType>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT a.*,b.lic_type_name FROM exciseautomation.lic_subtype_master a  inner join exciseautomation.lic_type_master b on a.lic_type_code=b.lic_type_code order by a.lic_subtype_code", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        ms = new List<LicenseSubType>();
                        while (dr.Read())
                        {
                            LicenseSubType om = new LicenseSubType();
                            om.lic_subtype_code= dr["lic_subtype_code"].ToString();
                            om.lic_subtype_name= dr["lic_subtype_name"].ToString();
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

        public static List<LicenseSubType> SearchLicense(string tablename, string column, string value)
        {
            List<LicenseSubType> ms = new List<LicenseSubType>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT a.*,b.lic_type_code,b.lic_type_name FROM exciseautomation.lic_subtype_master a inner join exciseautomation.lic_type_master b on a.lic_type_code=b.lic_type_code  where " + column + " Ilike '%" + value + "%' order by lic_subtype_name ", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        ms = new List<LicenseSubType>();
                        while (dr.Read())
                        {
                            LicenseSubType om = new LicenseSubType();
                            om.lic_subtype_code = dr["lic_subtype_code"].ToString();
                            om.lic_subtype_name = dr["lic_subtype_name"].ToString();
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
        public static string Insert(LicenseSubType om)
        {
            string value;

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.lic_subtype_master (lic_subtype_code, lic_subtype_name,lic_type_code,  user_id, creation_date) VALUES('" + om.lic_subtype_code + "', '" + om.lic_subtype_name + "', '" + om.lic_type_code + "', '" + om.user_id + "', '" + DateTime.Now.ToShortDateString() + "')", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = "0";
                        _log.Info("Insert license_type Success :" + om.lic_subtype_code + "-" + om.lic_subtype_name);
                    }
                    else
                    {
                        value = "1";
                        _log.Info("Insert license_type Fail :" + om.lic_subtype_code + "-" + om.lic_subtype_name);
                    }
                    cn.Close();

                }
                catch (Exception ex1)
                {
                    _log.Info("Insert license_type Fail :" + om.lic_subtype_code + "-" + om.lic_subtype_name + "-" + ex1.Message);
                    value = ex1.Message;
                }
                return value;

            }
        }

        public static string Update(LicenseSubType om)
        {
            string value;

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.lic_subtype_master SET  lic_subtype_name = '" + om.lic_subtype_name + "',lic_type_code = '" + om.lic_type_code + "' WHERE   lic_subtype_code = '" + om.lic_subtype_code + "' ", cn);
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
