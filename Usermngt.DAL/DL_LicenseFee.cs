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
  public  class DL_LicenseFee
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static List<LicenseFee> GetList(string userid)
        {
            List<LicenseFee> ms = new List<LicenseFee>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.lic_subtype_name,c.lic_type_name from exciseautomation.lic_fee_master a inner join exciseautomation.lic_subtype_master b on a.lic_subtype_code=b.lic_subtype_code inner join exciseautomation.lic_type_master c on a.lic_type_code=c.lic_type_code order by lic_fee_code ", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        ms = new List<LicenseFee>();
                        while (dr.Read())
                        {
                            LicenseFee om = new LicenseFee();
                            om.lic_subtype_code = dr["lic_subtype_code"].ToString();
                            om.lic_fee_code = dr["lic_fee_code"].ToString();
                            om.lic_type_code = dr["lic_type_code"].ToString();
                            om.lic_type_name = dr["lic_type_name"].ToString();
                            om.lic_subtype_name = dr["lic_subtype_name"].ToString();
                            om.lic_fee_master_id= Convert.ToInt32( dr["lic_fee_master_id"].ToString());
                            om.lic_proc_fee = Convert.ToDouble(dr["lic_proc_fee"].ToString());
                            om.lic_regn_amt = Convert.ToDouble(dr["lic_regn_amt"].ToString());
                            om.lic_security_amt = Convert.ToDouble(dr["lic_security_amt"].ToString());
                            om.lic_adv_fee = Convert.ToDouble(dr["lic_adv_fee"].ToString());
                            om.lic_fee_amt = Convert.ToDouble(dr["lic_fee_amt"].ToString());
                            om.lic_proc_fee = Convert.ToDouble(dr["lic_proc_fee"].ToString());
                            if (dr["lic_renewal_fee"].ToString() != "" && dr["lic_renewal_fee"].ToString() != null)
                                om.lic_renewal_fee = Convert.ToDouble(dr["lic_renewal_fee"].ToString());
                            else
                                om.lic_renewal_fee = 0;
                            om.user_id =dr["user_id"].ToString();
                            om.user_id = userid;
                            ms.Add(om);
                        }
                    }
                    _log.Info("Get License Fee List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get License Fee List Fail");
                }
            }
            return ms;
        }

        public static List<LicenseFee> SearchLicense(string tablename, string column, string value)
        {
            List<LicenseFee> ms = new List<LicenseFee>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT a.*,b.lic_subtype_name,c.lic_type_name FROM exciseautomation.lic_fee_master a inner join exciseautomation.lic_subtype_master b on a.lic_subtype_code=b.lic_subtype_code inner join exciseautomation.lic_type_master c on a.lic_type_code=c.lic_type_code  where " + column + " Ilike '%" + value + "%' order by lic_fee_code ", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        ms = new List<LicenseFee>();
                        while (dr.Read())
                        {
                            LicenseFee om = new LicenseFee();
                            om.lic_subtype_code = dr["lic_subtype_code"].ToString();
                            om.lic_fee_code = dr["lic_fee_code"].ToString();
                            om.lic_type_code = dr["lic_type_code"].ToString();
                            om.lic_type_name = dr["lic_type_name"].ToString();
                            om.lic_subtype_name = dr["lic_subtype_name"].ToString();
                            om.lic_fee_master_id = Convert.ToInt32(dr["lic_fee_master_id"].ToString());
                            om.lic_proc_fee = Convert.ToDouble(dr["lic_proc_fee"].ToString());
                            om.lic_regn_amt = Convert.ToDouble(dr["lic_regn_amt"].ToString());
                            om.lic_security_amt = Convert.ToDouble(dr["lic_security_amt"].ToString());
                            om.lic_adv_fee = Convert.ToDouble(dr["lic_adv_fee"].ToString());
                            om.lic_fee_amt = Convert.ToDouble(dr["lic_fee_amt"].ToString());
                            om.lic_proc_fee = Convert.ToDouble(dr["lic_proc_fee"].ToString());
                            if (dr["lic_renewal_fee"].ToString() != "" && dr["lic_renewal_fee"].ToString() != null)
                                om.lic_renewal_fee = Convert.ToDouble(dr["lic_renewal_fee"].ToString());
                            else
                                om.lic_renewal_fee = 0;
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
        public static string Insert(LicenseFee om)
        {
            string value;

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.lic_fee_master( lic_fee_code, lic_type_code, lic_subtype_code, lic_fee_amt, lic_regn_amt, lic_security_amt, lic_adv_fee, lic_proc_fee, creation_date, lastmodified_date, user_id, record_status,lic_renewal_fee)VALUES('" + om.lic_fee_code+"', '"+om.lic_type_code+"', '"+om.lic_subtype_code+"', '"+om.lic_fee_amt+"', '"+om.lic_regn_amt+"', '"+om.lic_security_amt+"', '"+om.lic_adv_fee+"', '"+om.lic_proc_fee+"', '"+DateTime.Now.ToShortDateString()+ "','" + DateTime.Now.ToShortDateString() + "','" + om.user_id+"','True','"+om.lic_renewal_fee+"')", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = "0";
                       // _log.Info("Insert license_type Success :" + om.lic_fee_code + "-" + om.lic_type_code);
                    }
                    else
                    {
                        value = "1";
                        _log.Info("Insert license_type Fail :" + om.lic_fee_code + "-" + om.lic_type_code);
                    }
                    cn.Close();

                }
                catch (Exception ex1)
                {
                    _log.Info("Insert license_type Fail :" + om.lic_fee_code + "-" + om.lic_type_code + "-" + ex1.Message);
                    value = ex1.Message;
                }
                return value;

            }
        }

        public static string Update(LicenseFee om)
        {
            string value;

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.lic_fee_master SET   lic_type_code ='"+om.lic_type_code+"', lic_subtype_code ='"+om.lic_subtype_code+"', lic_fee_amt ='"+om.lic_fee_amt+"', lic_regn_amt ='"+om.lic_regn_amt+"', lic_security_amt ='"+om.lic_security_amt+"', lic_adv_fee ='"+om.lic_adv_fee+"', lic_proc_fee ='"+om.lic_proc_fee+"', lastmodified_date ='"+DateTime.Now.ToShortDateString()+ "',lic_renewal_fee='"+om.lic_renewal_fee+"' WHERE lic_fee_code ='" + om.lic_fee_code + "' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = "0";
                        _log.Info("Update License Type Success :" + om.lic_fee_code + "-" + om.lic_type_code);
                    }
                    else
                    {
                        value = "1";
                        _log.Info("Update License Type Fail :" + om.lic_fee_code + "-" + om.lic_type_code);
                    }
                    cn.Close();
                }
                catch (Exception ex1)
                {
                    _log.Info("Update License Type Fail :" + om.lic_fee_code + "-" + om.lic_type_code + "-" + ex1.Message);
                    value = ex1.Message;
                }
                return value;

            }
        }

        public static LicenseFee GetDetails( int master_id)
        {

            LicenseFee om = new LicenseFee();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.lic_fee_master WHERE lic_fee_master_id='"+master_id+"'", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();

                    foreach (DataRow dr in dt.Rows)
                    {
                        om.lic_subtype_code = dr["lic_subtype_code"].ToString();
                        om.lic_fee_code = dr["lic_fee_code"].ToString();
                        om.lic_type_code = dr["lic_type_code"].ToString();
                        om.lic_fee_master_id = Convert.ToInt32(dr["lic_fee_master_id"].ToString());
                        om.lic_proc_fee = Convert.ToDouble(dr["lic_proc_fee"].ToString());
                        om.lic_regn_amt = Convert.ToDouble(dr["lic_regn_amt"].ToString());
                        om.lic_security_amt = Convert.ToDouble(dr["lic_security_amt"].ToString());
                        om.lic_adv_fee = Convert.ToDouble(dr["lic_adv_fee"].ToString());
                        om.lic_fee_amt = Convert.ToDouble(dr["lic_fee_amt"].ToString()); 
                        om.lic_proc_fee = Convert.ToDouble(dr["lic_proc_fee"].ToString());
                        if (dr["lic_renewal_fee"].ToString() != "" && dr["lic_renewal_fee"].ToString() != null)
                            om.lic_renewal_fee = Convert.ToDouble(dr["lic_renewal_fee"].ToString());
                        else
                            om.lic_renewal_fee = 0;
                        om.user_id = dr["user_id"].ToString();

                    }


                }

                catch (Exception ex)
                {
                    throw (ex);
                }
                return om;
            }
        }
    }
}
