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
    public class DL_cm_seiz_Bail
    {
        public static bool InsertSeiz_Bail(cm_seiz_Bail obj)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    try
                    {

                        string tableName = "exciseautomation.seizure_bail";
                        int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.seizure_bail", "seizure_bail_id").ToString()) + 1;
                        obj.finalseizureno = DL_Common.GetMaxID("exciseautomation.seizure where seizureno='" + obj.seizureno + "'", "finalseizureno").ToString();
                        string columnNames = "seizure_bail_id, seizureno, court_master_code, seizure_accused_details_id, bail_type_master_code, bailgranted,bailno, bailrequestdate, bailgranteddate, bailreason, bailer, ipaddress, lastmodified_date, user_id, creation_date,record_status, record_deleted,raidby";
                        string input = max + "','" + obj.seizureno + "','" + obj.court_master_code + "','" + obj.seizure_accused_details_id + "','" + obj.bail_type_master_code + "','" + obj.bailgranted + "','" + obj.bailno + "','" + obj.bailrequestdate + "','" + obj.bailgranteddate + "','" + obj.bailreason + "','" + obj.bailer +  "','" + obj.ipaddress + "','" + DateTime.Now.ToShortDateString() + "','" + obj.user_id + "','" + DateTime.Now.ToShortDateString() + "','" + obj.record_status + "','" + obj.record_deleted+"','"+obj.raidby;

                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            if (obj.record_status == "Y")
                            {
                                cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure_accuseddetails SET  bail_status = '" + obj.bailgranted + "' WHERE seizure_accused_details_id = '" + obj.seizure_accused_details_id + "'", cn);
                                n = cmd.ExecuteNonQuery();
                            }
                            DL_Common.UpdateStageCode(obj.seizureno, DL_Common.stage_code_Bail);
                            value = true;
                            
                            //Update Stage Code in Seizure table
                           
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
        public static bool Update(cm_seiz_Bail cm_obj)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                    cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure_bail SET  court_master_code = '" + cm_obj.court_master_code + "', seizure_accused_details_id = '" + cm_obj.seizure_accused_details_id + "', bail_type_master_code = '" + cm_obj.bail_type_master_code + "', bailgranted = '" + cm_obj.bailgranted + "', bailno = '" + cm_obj.bailno + "', bailrequestdate = '" + cm_obj.bailrequestdate + "', bailgranteddate = '" + cm_obj.bailgranteddate + "', bailreason = '" + cm_obj.bailreason + "', bailer = '" + cm_obj.bailer + "', ipaddress = '" + cm_obj.ipaddress + "', lastmodified_date = '" + DateTime.Now.ToShortDateString() + "', user_id = '" + cm_obj.user_id + "', record_status = '" + cm_obj.record_status + "', record_deleted = '" + cm_obj.record_deleted + "' WHERE seizure_bail_id ='" + cm_obj.seizure_bail_id + "'", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        if (cm_obj.record_status == "Y")
                        {
                            cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure_accuseddetails SET  bail_status = '"+ cm_obj.bailgranted+"' WHERE seizure_accused_details_id = '" + cm_obj.seizure_accused_details_id + "'", cn);
                            n = cmd.ExecuteNonQuery();
                        }
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
        public static cm_seiz_Bail GetDetailsById(string tableId)
        {
            cm_seiz_Bail result = new cm_seiz_Bail();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT b.*,prfirno, prfirdate, ad.accusedname ,court_master_name,bm.bail_type_master_name FROM exciseautomation.seizure_bail b INNER JOIN exciseautomation.seizure_fir F ON F.seizureno = b.seizureno INNER JOIN exciseautomation.seizure_accuseddetails ad ON ad.seizure_accused_details_id = b.seizure_accused_details_id INNER JOIN exciseautomation.court_master cm ON cm.court_master_code = b.court_master_code INNER JOIN exciseautomation.bail_type_master bm ON bm.bail_type_master_code = b.bail_type_master_code  Where seizure_bail_id= " + tableId + " order by seizure_bail_id", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {

                        result.seizure_bail_id = Convert.ToInt32(dr["seizure_bail_id"].ToString());
                        result.finalseizureno = dr["seizureno"].ToString();
                        result.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                        result.court_master_code = dr["court_master_code"].ToString();
                        result.seizure_accused_details_id = dr["seizure_accused_details_id"].ToString();
                        result.bail_type_master_code = dr["bail_type_master_code"].ToString();
                        result.bailgranted = dr["bailgranted"].ToString();
                        result.bailno = dr["bailno"].ToString();
                        result.bailrequestdate = Convert.ToDateTime(dr["bailrequestdate"]).ToString("dd-MM-yyyy");
                        result.bailgranteddate = Convert.ToDateTime(dr["bailgranteddate"]).ToString("dd-MM-yyyy");
                        result.bailreason = dr["bailreason"].ToString();
                        result.bailer = dr["bailer"].ToString();
                        result.ipaddress = dr["ipaddress"].ToString();
                        result.lastmodified_date = dr["lastmodified_date"].ToString();
                        result.user_id = dr["user_id"].ToString();
                        result.creation_date = dr["creation_date"].ToString();
                        result.record_status = dr["record_status"].ToString();
                        result.record_deleted = dr["record_deleted"].ToString();
                        result.prfirno = dr["prfirno"].ToString();
                        result.accusedname = dr["accusedname"].ToString();
                        result.prfirdate = dr["prfirdate"].ToString();
                        result.court_master_name = dr["court_master_name"].ToString();
                        result.bail_type_master_name = dr["bail_type_master_name"].ToString();
                    }
                    trn.Commit();
                    cn.Close();
                }
                catch (Exception ex1)
                {
                    //  _log.Info("NOC Insertion Fail:" + noc.noc_id + '-' + noc.party_code + " - " + ex1.Message);
                    //VAL = ex1.Message;
                    trn.Rollback();
                }
            }
            return result;
        }
        public static List<cm_seiz_Bail> GetList(string adid)
        {
            List<cm_seiz_Bail> ads = new List<cm_seiz_Bail>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT b.*,prfirno, prfirdate, F.finalseizureno, ad.accusedname ,court_master_name,bm.bail_type_master_name FROM exciseautomation.seizure_bail b INNER JOIN exciseautomation.seizure_fir F ON F.seizureno = b.seizureno INNER JOIN exciseautomation.seizure_accuseddetails ad ON ad.seizureno  = b.seizureno AND ad.seizure_accused_details_id=b.seizure_accused_details_id INNER JOIN exciseautomation.court_master cm ON cm.court_master_code = b.court_master_code INNER JOIN exciseautomation.bail_type_master bm ON bm.bail_type_master_code = b.bail_type_master_code Where b.seizureno= " + adid + " order by seizure_bail_id", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        cm_seiz_Bail ad = new cm_seiz_Bail();
                        ad.seizure_bail_id = Convert.ToInt32(dr["seizure_bail_id"].ToString());
                        ad.finalseizureno = dr["finalseizureno"].ToString();
                        ad.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                        ad.court_master_code = dr["court_master_code"].ToString();
                        ad.seizure_accused_details_id = dr["seizure_accused_details_id"].ToString();
                        ad.bail_type_master_code = dr["bail_type_master_code"].ToString();
                        ad.bailgranted = dr["bailgranted"].ToString();
                        ad.bailno = dr["bailno"].ToString();
                        ad.bailrequestdate = Convert.ToDateTime(dr["bailrequestdate"]).ToString("dd-MM-yyyy");
                        ad.bailgranteddate = Convert.ToDateTime(dr["bailgranteddate"]).ToString("dd-MM-yyyy");
                        ad.bailreason = dr["bailreason"].ToString();
                        ad.bailer = dr["bailer"].ToString();
                        ad.ipaddress = dr["ipaddress"].ToString();
                        ad.lastmodified_date = dr["lastmodified_date"].ToString();
                        ad.user_id = dr["user_id"].ToString();
                        ad.creation_date = dr["creation_date"].ToString();
                        ad.record_status = dr["record_status"].ToString();
                        ad.record_deleted = dr["record_deleted"].ToString();
                        ad.prfirno = dr["prfirno"].ToString();
                        ad.prfirdate = Convert.ToDateTime(dr["prfirdate"]).ToString("dd-MM-yyyy");
                        ad.accusedname = dr["accusedname"].ToString();
                        ad.court_master_name = dr["court_master_name"].ToString();
                        ad.bail_type_master_name = dr["bail_type_master_name"].ToString();
                        ad.raidby = dr["raidby"].ToString();
                        ads.Add(ad);
                    }
                    trn.Commit();
                    cn.Close();
                }
                catch (Exception ex1)
                {
                    //  _log.Info("NOC Insertion Fail:" + noc.noc_id + '-' + noc.party_code + " - " + ex1.Message);
                    //VAL = ex1.Message;
                    trn.Rollback();
                }
            }
            return ads;
        }
    }
}
