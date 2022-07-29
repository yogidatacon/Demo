using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Usermngt.Entities;
using Npgsql;
using System.Data;

namespace Usermngt.DAL
{
    public class DL_cm_seiz_AppealDetails
    {
        public static List<cm_seiz_AppealDetails> GetList(string seizureNo)
        {
            List<cm_seiz_AppealDetails> lstObj = new List<cm_seiz_AppealDetails>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT ad.*,cm.court_master_code,cm.court_master_name,asm.accusedstatus_code,asm.accusedstatus_name FROM exciseautomation.seizure_appealdetails ad INNER JOIN exciseautomation.accusedstatus_master asm ON asm.accusedstatus_code=ad.accusedstatus_code INNER JOIN exciseautomation.court_master cm ON cm.court_master_code=ad.court_master_code where seizureNo= " + seizureNo, cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_seiz_AppealDetails>();
                            while (dr.Read())
                            {
                                cm_seiz_AppealDetails record = new cm_seiz_AppealDetails();
                                record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                                record.seizure_appealdetails_id = Convert.ToInt32(dr["seizure_appealdetails_id"].ToString());
                                record.court_master_code = dr["court_master_code"].ToString();
                                record.court_master_code = dr["court_master_name"].ToString();
                                record.seizure_accused_details_id = dr["seizure_accused_details_id"].ToString();
                                record.accusedstatus_code = dr["accusedstatus_code"].ToString();
                                record.accusedstatus_name = dr["accusedstatus_name"].ToString();
                                record.appealno = dr["appealno"].ToString();
                                record.appealdate = Convert.ToDateTime(dr["appealdate"]).ToString("dd-MM-yyyy");
                                record.appealby = dr["appealby"].ToString();
                                record.appealresult = dr["appealresult"].ToString();
                                record.resultdate = Convert.ToDateTime(dr["resultdate"]).ToString("dd-MM-yyyy");
                                record.finalseizureno = dr["finalseizureno"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.raidby = dr["raidby"].ToString();
                                record.user_id = dr["user_id"].ToString();
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

        public static cm_seiz_AppealDetails GetDetails(string tableId)
        {
            cm_seiz_AppealDetails record = new cm_seiz_AppealDetails();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT ad.*,cm.court_master_code,cm.court_master_name,asm.accusedstatus_code,asm.accusedstatus_name FROM exciseautomation.seizure_appealdetails ad INNER JOIN exciseautomation.accusedstatus_master asm ON asm.accusedstatus_code=ad.accusedstatus_code INNER JOIN  exciseautomation.court_master cm ON cm.court_master_code=ad.court_master_code where seizure_appealdetails_id=" + tableId, cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            record = new cm_seiz_AppealDetails();
                            while (dr.Read())
                            {
                                record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());                             
                                record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                                record.seizure_appealdetails_id = Convert.ToInt32(dr["seizure_appealdetails_id"].ToString());
                                record.court_master_code = dr["court_master_code"].ToString();
                                //record.court_master_name = dr["court_master_name"].ToString();
                                record.seizure_accused_details_id = dr["seizure_accused_details_id"].ToString();
                                record.accusedstatus_code = dr["accusedstatus_code"].ToString();
                                record.accusedstatus_name = dr["accusedstatus_name"].ToString();
                                record.appealno = dr["appealno"].ToString();
                                record.appealdate = Convert.ToDateTime(dr["appealdate"]).ToString("dd-MM-yyyy");
                                record.appealby = dr["appealby"].ToString();
                                record.appealresult = dr["appealresult"].ToString();
                                record.resultdate = Convert.ToDateTime(dr["resultdate"]).ToString("dd-MM-yyyy");
                                record.finalseizureno = dr["finalseizureno"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.raidby = dr["raidby"].ToString();
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

        public static bool InsertSeiz_Appeal(cm_seiz_AppealDetails obj)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    try
                    {

                        string tableName = "exciseautomation.seizure_appealdetails";
                        int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.seizure_appealdetails", "seizure_appealdetails_id").ToString()) + 1;
                        string columnNames = "seizure_appealdetails_id, seizureno, court_master_code, seizure_accused_details_id, accusedstatus_code, appealno, appealdate, appealby, appealresult, resultdate, finalseizureno, lastmodified_date, user_id, creation_date, record_status, record_deleted,raidby";
                        string input = "";
                        input = max + "','" + obj.seizureno + "','" + obj.court_master_code + "','" + obj.seizure_accused_details_id + "','" + obj.accusedstatus_code + "','" + obj.appealno + "','" + obj.appealdate + "','" + obj.appealby + "','" + obj.appealresult + "','" + obj.resultdate + "','" + obj.finalseizureno + "','" + DateTime.Now.ToShortDateString() + "','" + obj.user_id + "','" + DateTime.Now.ToShortDateString() + "','" + obj.record_status + "','" + obj.record_deleted+"','"+obj.raidby;

                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            value = true;
                            //Update Stage Code in Seizure table
                            DL_Common.UpdateStageCode(obj.seizureno, DL_Common.stage_code_Appeal);
                        }
                        else
                            value = false;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        value = false;
                    }
                }
            }
            return value;
        }

        public static bool Update_Appeal(cm_seiz_AppealDetails cm_obj)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                    cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure_appealdetails SET  court_master_code = '" + cm_obj.court_master_code + "', seizure_accused_details_id = '" + cm_obj.seizure_accused_details_id + "', accusedstatus_code = '" + cm_obj.accusedstatus_code + "', appealno = '" + cm_obj.appealno + "', appealdate = '" + cm_obj.appealdate + "', appealby = '" + cm_obj.appealby + "', appealresult = '" + cm_obj.appealresult + "', resultdate = '" + cm_obj.resultdate + "',  lastmodified_date = '" + DateTime.Now.ToShortDateString() + "', user_id = '" + cm_obj.user_id + "', record_status = '" + cm_obj.record_status + "' WHERE seizure_appealdetails_id ='" + cm_obj.seizure_appealdetails_id + "'", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                    }
                    else
                    {
                        value = false;
                    }
                }
                catch (Exception ex)
                {
                    value = false;
                    throw (ex);
                }
            }
            return value;
        }
      
    }
}
