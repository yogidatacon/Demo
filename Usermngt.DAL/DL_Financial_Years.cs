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
    public class DL_Financial_Years
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static List<Financial_Years> GetFinacListValues()
        {
            List<Financial_Years> FinacList = new List<Financial_Years>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.party_type_name,b.* from exciseautomation.party_type_Master a inner join exciseautomation.party_financial_yr b on a.party_type_code=b.party_type_code order by b.party_financial_yr_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            FinacList = new List<Financial_Years>();
                            while (dr.Read())
                            {
                                Financial_Years record = new Financial_Years();
                                record.party_type_name = dr["party_type_name"].ToString();
                                record.party_type_code = dr["party_type_code"].ToString();
                                record.start_date = dr["startdate"].ToString();
                                record.end_date = dr["enddate"].ToString();
                                record.status = dr["record_active"].ToString();
                                record.financial_year = dr["financial_year"].ToString();
                                record.id = Convert.ToInt32(dr["party_financial_yr_id"].ToString());
                                FinacList.Add(record);
                            }
                        }
                    }

                    cn.Close();
                    _log.Info("GetFinacListValues Success");
                }
                catch (Exception ex)
                {
                    _log.Info("GetFinacListValues Success :" + ex.Message);
                }

            }
            return FinacList;
        }
        public static List<Financial_Years> SearchPartyType(string tablename, string column, string value)
        {
            List<Financial_Years> FinacList = new List<Financial_Years>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.party_type_name,b.* from exciseautomation.party_type_Master a inner join exciseautomation.party_financial_yr b on a.party_type_code=b.party_type_code where " + column + " Ilike '%" + value + "%'    order by b.party_financial_yr_id ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            FinacList = new List<Financial_Years>();
                            while (dr.Read())
                            {
                                Financial_Years record = new Financial_Years();
                                record.party_type_name = dr["party_type_name"].ToString();
                                record.party_type_code = dr["party_type_code"].ToString();
                                record.start_date = dr["startdate"].ToString();
                                record.end_date = dr["enddate"].ToString();
                                record.status = dr["record_active"].ToString();
                                record.financial_year = dr["financial_year"].ToString();
                                record.id = Convert.ToInt32(dr["party_financial_yr_id"].ToString());
                                FinacList.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return FinacList;
        }



        public static Financial_Years GetDetails(string fin_id)
        {
            Financial_Years record = new Financial_Years();
            //List<Financial_Years> FinacList = new List<Financial_Years>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.party_type_name,b.* from exciseautomation.party_type_Master a inner join exciseautomation.party_financial_yr b on a.party_type_code=b.party_type_code where b.party_financial_yr_id='"+fin_id+"'", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                           // FinacList = new List<Financial_Years>();
                            while (dr.Read())
                            {
                               
                                record.party_type_name = dr["party_type_name"].ToString();
                                record.party_type_code = dr["party_type_code"].ToString();
                                record.start_date = dr["startdate"].ToString();
                                record.end_date = dr["enddate"].ToString();
                                record.status = dr["record_active"].ToString();
                                record.financial_year = dr["financial_year"].ToString();
                                record.id = Convert.ToInt32(dr["party_financial_yr_id"].ToString());
                               // FinacList.Add(record);
                            }
                        }
                    }

                    cn.Close();
                    _log.Info("GetFinacListValues Success");
                }
                catch (Exception ex)
                {
                    _log.Info("GetFinacListValues Success :" + ex.Message);
                }

            }
            return record;
        }

        public static string Update(Financial_Years fin)
        {
            string val = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    NpgsqlCommand cmd = new NpgsqlCommand("update exciseautomation.party_financial_yr set party_type_code, financial_year, startdate, enddate, lastmodified_date, record_active) VALUES('" + fin.party_type_code + "','" + fin.financial_year + "','" + fin.start_date + "','" + fin.end_date + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + fin.status + "')", cn);
                    cmd.ExecuteNonQuery();
                    val = "0";

                    cn.Close();
                    _log.Info("Insert FinacListValues Success");
                }
                catch (Exception ex)
                {
                    val = ex.Message;
                    _log.Info("Insert FinacListValues Success :" + ex.Message);
                }

            }
            return val;
        }

        public static string Insert(Financial_Years fin)
        {
            string val = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select count(1) as count from exciseautomation.party_financial_yr where party_type_code='" + fin.party_type_code + "' and financial_year ='" + fin.financial_year + "'", cn);
                    string val1 = cmd.ExecuteScalar().ToString();
                    if (val1 != "" && val1 != "0")
                    {
                        cmd = new NpgsqlCommand("update exciseautomation.party_financial_yr set record_active='N' where party_type_code='" + fin.party_type_code + "' ", cn);
                        cmd.ExecuteNonQuery();
                        cmd = new NpgsqlCommand("INSERT INTO exciseautomation.party_financial_yr(party_type_code, financial_year, startdate, enddate, lastmodified_date, record_active) VALUES('" + fin.party_type_code + "','" + fin.financial_year + "','" + fin.start_date + "','" + fin.end_date + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','Y')", cn);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd = new NpgsqlCommand("INSERT INTO exciseautomation.party_financial_yr(party_type_code, financial_year, startdate, enddate, lastmodified_date, record_active) VALUES('" + fin.party_type_code + "','" + fin.financial_year + "','" + fin.start_date + "','" + fin.end_date + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','Y')", cn);
                        cmd.ExecuteNonQuery();
                    }
                    val = "0";
                    cn.Close();
                    _log.Info("Insert FinacListValues Success");
                }
                catch (Exception ex)
                {
                    val = ex.Message;
                    _log.Info("Insert FinacListValues Success :" + ex.Message);
                }

            }
            return val;
        }
    }
}
