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
    public class DL_VatType
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static List<VatType> GetVatTypeList(string userid)
        {
            List<VatType> Vats = new List<VatType>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.vat_type_master order by vat_type_code,vat_type_name", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            Vats = new List<VatType>();
                            while (dr.Read())
                            {
                                VatType vat = new VatType();
                                vat.vat_type_code = dr["vat_type_code"].ToString();
                                vat.vat_type_name = dr["vat_type_name"].ToString();
                                // vat.org_id = dr["org_id"].ToString();
                                //  vat.vat_active= dr["vat_active"].ToString();
                                vat.user_id = dr["user_id"].ToString();
                                //  vat.status = dr["status"].ToString();
                                Vats.Add(vat);
                            }
                            _log.Info("Get VAT Type Master List Success");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Get VAT Type Master List Fail :"+ex.Message);
                }
            }
            return Vats;


        }
        public static List<VatType> SearchVatType(string tablename, string column, string value)
        {
            List<VatType> Vats = new List<VatType>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.vat_type_master where " + column + " Ilike '%" + value + "%' order by vat_type_name", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            Vats = new List<VatType>();
                            while (dr.Read())
                            {
                                VatType vat = new VatType();
                                vat.vat_type_code = dr["vat_type_code"].ToString();
                                vat.vat_type_name = dr["vat_type_name"].ToString();
                                // vat.org_id = dr["org_id"].ToString();
                                //  vat.vat_active= dr["vat_active"].ToString();
                                vat.user_id = dr["user_id"].ToString();
                                //  vat.status = dr["status"].ToString();
                                Vats.Add(vat);
                            }
                            _log.Info("Get VAT Type Master List Success");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Get VAT Type Master List Fail :" + ex.Message);
                }
            }
            return Vats;

        }

        public static bool InserVatType(VatType vat)
        {
            bool value = false;

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.vat_type_master(vat_type_code, vat_type_name, org_id, vat_active, record_status, creation_date, lastmodified_date, user_id)VALUES('" + vat.vat_type_code.ToUpper() + "', '" + vat.vat_type_name + "',1, 'true', 'true', ' " + DateTime.Now.ToShortDateString() + "', '" + DateTime.Now.ToShortDateString() + "', '" + vat.user_id + "') ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                        _log.Info("Insert VAT Type Master Success :" + vat.vat_type_code.ToUpper()+"-"+vat.vat_type_name);
                    }
                    else
                    {
                        value = false;
                        _log.Info("Insert VAT Type Master Success :" + vat.vat_type_code.ToUpper() + "-" + vat.vat_type_name);
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Insert VAT Type Master Success :" + vat.vat_type_code.ToUpper() + "-" + vat.vat_type_name+"-"+ex.Message);
                    value = false;
                }
                return value;
            }

        }


        public static bool UpdateVat(VatType vat)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.vat_type_master SET  vat_type_name ='" + vat.vat_type_name + "' where vat_type_code='" + vat.vat_type_code + "' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                        _log.Info("Update VAT Type Master Success :" + vat.vat_type_code + "-" + vat.vat_type_name);
                    }
                    else
                    {
                        value = false;
                        _log.Info("Update VAT Type Master Success :" + vat.vat_type_code + "-" + vat.vat_type_name);
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Update VAT Type Master Success :" + vat.vat_type_code + "-" + vat.vat_type_name + "-" + ex.Message);
                    value = false;
                }
                return value;



            }
        }

    }
}
