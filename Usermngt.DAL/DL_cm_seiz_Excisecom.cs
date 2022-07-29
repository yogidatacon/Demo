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
    public class DL_cm_seiz_Excisecom
    {
        public static List<cm_seiz_Excisecom> GetList(string seizureNo)
        {
            List<cm_seiz_Excisecom> lstObj = new List<cm_seiz_Excisecom>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT raidby,seizure_excisecom_id, seizureno, prfirno, appl_letterno, appl_letterdate, ecorderno, ecorderdate, ecremarks, confiscation_caseno, magistratename, amountreceived, highauthority_date, highauthority_name, highauthority_remarks, finalseizureno, lastmodified_date, user_id, creation_date, record_status, record_deleted, appealed_to_ha FROM exciseautomation.seizure_excisecom where seizureNo= " + seizureNo, cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_seiz_Excisecom>();
                            while (dr.Read())
                            {
                                cm_seiz_Excisecom record = new cm_seiz_Excisecom();
                                record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                                record.seizure_excisecom_id = Convert.ToInt32(dr["seizure_excisecom_id"].ToString());
                                record.prfirno = dr["prfirno"].ToString();
                                record.appl_letterno = dr["appl_letterno"].ToString();
                                record.appl_letterdate = Convert.ToDateTime(dr["appl_letterdate"]).ToString("dd-MM-yyyy");
                                record.ecorderno = dr["ecorderno"].ToString();
                                record.ecorderdate = Convert.ToDateTime(dr["ecorderdate"]).ToString("dd-MM-yyyy");
                                record.ecremarks = dr["ecremarks"].ToString();

                                record.confiscation_caseno = dr["confiscation_caseno"].ToString();
                                record.magistratename = dr["magistratename"].ToString();
                                record.amountreceived = dr["amountreceived"].ToString();
                                record.highauthority_date = Convert.ToDateTime(dr["highauthority_date"]).ToString("dd-MM-yyyy");
                                record.highauthority_name = dr["highauthority_name"].ToString();
                                record.highauthority_remarks = dr["highauthority_remarks"].ToString();

                                record.finalseizureno = dr["finalseizureno"].ToString();
                                record.lastmodified_date = dr["lastmodified_date"].ToString();
                                record.user_id = dr["user_id"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.appealed_to_ha = dr["appealed_to_ha"].ToString();
                                record.raidby = dr["raidby"].ToString();
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

        public static cm_seiz_Excisecom GetDetails(string tableId)
        {
            cm_seiz_Excisecom record = new cm_seiz_Excisecom();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT seizure_excisecom_id, seizureno, prfirno, appl_letterno, appl_letterdate, ecorderno, ecorderdate, ecremarks, confiscation_caseno, magistratename, amountreceived, highauthority_date, highauthority_name, highauthority_remarks, finalseizureno, lastmodified_date, user_id, creation_date, record_status, record_deleted, appealed_to_ha FROM exciseautomation.seizure_excisecom where seizure_excisecom_id=" + tableId, cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            record = new cm_seiz_Excisecom();
                            while (dr.Read())
                            {
                                record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                                record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                                record.seizure_excisecom_id = Convert.ToInt32(dr["seizure_excisecom_id"].ToString());
                                record.prfirno = dr["prfirno"].ToString();
                                record.appl_letterno = dr["appl_letterno"].ToString();
                                record.appl_letterdate = Convert.ToDateTime(dr["appl_letterdate"]).ToString("dd-MM-yyyy");
                                record.ecorderno = dr["ecorderno"].ToString();
                                record.ecorderdate = Convert.ToDateTime(dr["ecorderdate"]).ToString("dd-MM-yyyy");
                                record.ecremarks = dr["ecremarks"].ToString();

                                record.confiscation_caseno = dr["confiscation_caseno"].ToString();
                                record.magistratename = dr["magistratename"].ToString();
                                record.amountreceived = dr["amountreceived"].ToString();
                                record.highauthority_date = Convert.ToDateTime(dr["highauthority_date"]).ToString("dd-MM-yyyy");
                                record.highauthority_name = dr["highauthority_name"].ToString();
                                record.highauthority_remarks = dr["highauthority_remarks"].ToString();

                                record.finalseizureno = dr["finalseizureno"].ToString();
                                record.lastmodified_date = dr["lastmodified_date"].ToString();
                                record.user_id = dr["user_id"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.appealed_to_ha = dr["appealed_to_ha"].ToString();
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

        public static bool InsertSeiz_Excisecom(cm_seiz_Excisecom obj)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    try
                    {

                        string tableName = "exciseautomation.seizure_excisecom";
                        int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.seizure_excisecom", "seizure_excisecom_id").ToString()) + 1;
                        string columnNames = "seizure_excisecom_id, seizureno, prfirno, appl_letterno, appl_letterdate, ecorderno, ecorderdate, ecremarks, confiscation_caseno, magistratename, amountreceived, highauthority_date, highauthority_name, highauthority_remarks, finalseizureno, lastmodified_date, user_id, creation_date, record_status, record_deleted, appealed_to_ha,raidby ";
                        string input = "";
                        input = max + "','" + obj.seizureno + "','" + obj.prfirno + "','" + obj.appl_letterno + "','" + obj.appl_letterdate + "','" + obj.ecorderno + "','" + obj.ecorderdate + "','" + obj.ecremarks + "','" + obj.confiscation_caseno + "','" + obj.magistratename + "','" + obj.amountreceived + "','" + obj.highauthority_date + "','" + obj.highauthority_name + "','" + obj.highauthority_remarks + "','" + obj.finalseizureno + "','" + obj.lastmodified_date + "','" + obj.user_id + "','" + obj.creation_date + "','" + obj.record_status + "','" + obj.record_deleted + "','" + obj.appealed_to_ha+"','"+obj.raidby ; 

                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {

                            //for (int i = 0; i < obj.articals.Count; i++)
                            //{
                            //    cmd = new NpgsqlCommand("update exciseautomation.seizure_excisable_articles set date_of_destruction='" + obj.articals[i].date_of_destruction + "',actioncompleted='" + obj.articals[i].actioncompleted + "' where seizure_excisable_articles_id ='" + obj.articals[i].seizure_excisable_articles_id + "' and seizureno='" + obj.seizureno + "'", cn);
                            //    cmd.ExecuteNonQuery();
                            //}
                            //for (int i = 0; i < obj.vehicals.Count; i++)
                            //{
                            //    cmd = new NpgsqlCommand("update exciseautomation.seizure_vehicledetails set auction_or_releasedate='" + obj.vehicals[i].auction_or_releasedate + "',actioncompleted='" + obj.vehicals[i].actioncompleted + "',challan_no='" + obj.vehicals[i].challan_no + "',challan_date='" + obj.vehicals[i].challan_date + "',auctionreleaseamount='" + obj.vehicals[i].auctionreleaseamount + "',infavourof='" + obj.vehicals[i].infavourof + "' where seizure_vehicledetails_id ='" + obj.vehicals[i].seizure_vehicledetails_id + "' and seizureno='" + obj.seizureno + "'", cn);
                            //    cmd.ExecuteNonQuery();
                            //}
                            //Update Stage Code in Seizure table
                            DL_Common.UpdateStageCode(obj.seizureno, DL_Common.stage_code_ExciseCommissionerOrderDetails);
                            value = true;
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

        public static bool Update_Excisecom(cm_seiz_Excisecom cm_obj)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                    cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure_excisecom SET  appl_letterno = '" + cm_obj.appl_letterno + "', appl_letterdate = '" + cm_obj.appl_letterdate + "', ecorderno = '" + cm_obj.ecorderno + "', ecorderdate = '" + cm_obj.ecorderdate + "', ecremarks = '" + cm_obj.ecremarks + "', confiscation_caseno = '" + cm_obj.confiscation_caseno + "', magistratename = '" + cm_obj.magistratename + "', amountreceived = '" + cm_obj.amountreceived + "', highauthority_date = '" + cm_obj.highauthority_date + "', highauthority_name = '" + cm_obj.highauthority_name + "', highauthority_remarks = '" + cm_obj.highauthority_remarks + "',  lastmodified_date = '" + DateTime.Now.ToShortDateString() + "', user_id = '" + cm_obj.user_id + "', record_status = '" + cm_obj.record_status + "',raidby='"+cm_obj.raidby+"' WHERE seizure_excisecom_id ='" + cm_obj.seizure_excisecom_id + "'", cn);                    

                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        //for (int i = 0; i < cm_obj.articals.Count; i++)
                        //{
                        //    cmd = new NpgsqlCommand("update exciseautomation.seizure_excisable_articles set date_of_destruction='" + cm_obj.articals[i].date_of_destruction + "',actioncompleted='" + cm_obj.articals[i].actioncompleted + "' where seizure_excisable_articles_id ='" + cm_obj.articals[i].seizure_excisable_articles_id + "' and seizureno='" + cm_obj.seizureno + "'", cn);
                        //    cmd.ExecuteNonQuery();
                        //}
                        //for (int i = 0; i < cm_obj.vehicals.Count; i++)
                        //{
                        //    cmd = new NpgsqlCommand("update exciseautomation.seizure_vehicledetails set auction_or_releasedate='" + cm_obj.vehicals[i].auction_or_releasedate + "',actioncompleted='" + cm_obj.vehicals[i].actioncompleted + "',challan_no='" + cm_obj.vehicals[i].challan_no + "',challan_date='" + cm_obj.vehicals[i].challan_date + "',auctionreleaseamount='" + cm_obj.vehicals[i].auctionreleaseamount + "',infavourof='" + cm_obj.vehicals[i].infavourof + "' where seizure_vehicledetails_id ='" + cm_obj.vehicals[i].seizure_vehicledetails_id + "' and seizureno='" + cm_obj.seizureno + "'", cn);
                        //    cmd.ExecuteNonQuery();
                        //}
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
