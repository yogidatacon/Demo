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
    public class DL_cm_seiz_CaseHistory
    {
        public static List<cm_seiz_CaseHistory> GetList(string seizureNo)
        {
            List<cm_seiz_CaseHistory> lstObj = new List<cm_seiz_CaseHistory>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    //using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT ad.*,atm.apparatus_type FROM exciseautomation.seizure_accusedcasehistory ad  INNER JOIN apparatus_type_master atm ON atm.apparatus_type_code=ad.seizure_accused_details_id where seizureno= " + seizureNo, cn))
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.seizure_accusedcasehistory  where seizureno= " + seizureNo, cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_seiz_CaseHistory>();
                            while (dr.Read())
                            {
                                cm_seiz_CaseHistory record = new cm_seiz_CaseHistory();
                                record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                                record.seizure_accusedcasehistory_id = Convert.ToInt32(dr["seizure_accusedcasehistory_id"].ToString());
                                record.seizure_accused_details_id = dr["seizure_accused_details_id"].ToString();
                                record.case_id = dr["case_id"].ToString();
                                record.case_details = dr["case_details"].ToString();
                                record.idno = dr["idno"].ToString();
                                record.record_status = dr["record_status"].ToString();                                
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }

        public static string Update_CaseHistory(cm_seiz_CaseHistory cm_obj)
        {
            string value = "0";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                    cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure_accusedcasehistory SET  case_id = '" + cm_obj.case_id + "', case_details = '" + cm_obj.case_details + "', idno = '" + cm_obj.idno + "', idproof_code = '" + cm_obj.idproof_code + "', seizure_accused_details_id = '" + cm_obj.seizure_accused_details_id + "', lastmodified_date = '" + DateTime.Now.ToShortDateString() + "', user_id = '" + cm_obj.user_id + "', record_status = '" + cm_obj.record_status + "' WHERE seizure_accusedcasehistory_id ='" + cm_obj.seizure_accusedcasehistory_id + "'", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = "0";
                    }
                    else
                    {
                        value = "1";
                    }
                }
                catch (Exception ex)
                {
                    value = "1";
                    throw (ex);
                }
            }
            return value;
        }

        public static string InsertSeiz_CaseHistory(cm_seiz_CaseHistory obj)
        {
            string value = string.Empty;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    try
                    {

                        string tableName = "exciseautomation.seizure_accusedcasehistory";
                        int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.seizure_accusedcasehistory", "seizure_accusedcasehistory_id").ToString()) + 1;
                        string columnNames = "seizure_accusedcasehistory_id, seizure_accused_details_id, seizureno, idproof_code, idno, case_id,case_details,lastmodified_date, user_id, creation_date, record_status, record_deleted,raidby";
                        string input = max + "','" + obj.seizure_accused_details_id + "','" + obj.seizureno + "','" + obj.idproof_code + "','" + obj.idno + "','" + obj.case_id + "','" + obj.case_details +  "','" + DateTime.Now.ToShortDateString() + "','" + obj.user_id + "','" + DateTime.Now.ToShortDateString() + "','" + obj.record_status + "','" + obj.record_deleted+"','"+obj.raidby;

                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            value = "0";
                        }
                        else
                            value = "1";

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        value = "1";
                    }
                }
            }
            return value;
        }

        public static cm_seiz_CaseHistory GetDetails(string tableId)
        {
            cm_seiz_CaseHistory record = new cm_seiz_CaseHistory();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.seizure_accusedcasehistory where seizure_accusedcasehistory_id=" + tableId, cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            record = new cm_seiz_CaseHistory();
                            while (dr.Read())
                            {
                                record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                                record.seizure_accusedcasehistory_id = Convert.ToInt32(dr["seizure_accusedcasehistory_id"].ToString());
                                record.seizure_accused_details_id = dr["seizure_accused_details_id"].ToString();
                                record.idproof_code = dr["idproof_code"].ToString();
                                record.case_id = dr["case_id"].ToString();
                                record.idno= dr["idno"].ToString();
                                record.case_details = dr["case_details"].ToString();
                                record.ipaddress = dr["ipaddress"].ToString();
                                record.record_status = dr["record_status"].ToString();
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return record;
        }
    }
}
