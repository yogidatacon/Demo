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
    public class DL_cm_seiz_FIR
    {
        public static bool InsertSeiz_FIR(cm_seiz_FIR obj)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    NpgsqlTransaction trn;
                    trn = cn.BeginTransaction();
                    try
                    {

                        string tableName = "exciseautomation.seizure_fir";
                        int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.seizure_fir", "seizure_fir_id").ToString()) + 1;
                        obj.finalseizureno =DL_Common.GetMaxID("exciseautomation.seizure where seizureno='"+obj.seizureno+"'", "finalseizureno").ToString();
                        string columnNames = "seizure_fir_id, seizureno, designation_code, raidby, prfirno, prfirdate, manualprfirno, manualbookdate,raidorderby, complaintno, complaintdate, infotocourtdate,  ipaddress, lastmodified_date, user_id, creation_date, record_status, record_deleted,finalseizureno";
                        string input = max + "','" + obj.seizureno + "','" + obj.designation_code + "','" + obj.raidby + "','" + obj.prfirno + "','" + obj.prfirdate + "','" + obj.manualprfirno + "','" + obj.manualbookdate + "','" + obj.raidorderby + "','" + obj.complaintno + "','" + obj.complaintdate + "','" + obj.infotocourtdate + "','" + obj.ipaddress + "','" + DateTime.Now.ToShortDateString() + "','" + obj.user_id + "','" + DateTime.Now.ToShortDateString() + "','" + obj.record_status + "','" + obj.record_deleted+"','"+obj.finalseizureno;

                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            value = true;
                            if (obj.user_id.Contains("thana_"))
                            {
                                cmd = new NpgsqlCommand("update exciseautomation.complaint_data set prfirno='" + max + "' where seizureno='" + obj.seizureno + "'  ", cn);
                                cmd.ExecuteNonQuery();
                            }
                            if (obj.record_status == "Y")
                            {

                                cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure_accuseddetails	SET  fir_status='Y', seizure_fir_id='" + obj.seizure_fir_id + "' WHERE seizureno ='" + obj.seizureno + "' and raidby='" + obj.raidby + "'", cn);
                                cmd.ExecuteNonQuery();
                            
                            }
                            DL_Common.UpdateStageCode(obj.seizureno, DL_Common.stage_code_FIR);
                            trn.Commit();
                            cn.Close();
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
        public static bool Update(cm_seiz_FIR cm_obj)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                    cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure_fir	SET   designation_code= '" + cm_obj.designation_code + "', raidby = '" + cm_obj.raidby + "', prfirno = '" + cm_obj.prfirno + "', prfirdate = '" + cm_obj.prfirdate + "', manualprfirno = '" + cm_obj.manualprfirno + "', manualbookdate = '" + cm_obj.manualbookdate + "', raidorderby = '" + cm_obj.raidorderby + "', complaintno = '" + cm_obj.complaintno + "', complaintdate = '" + cm_obj.complaintdate + "', infotocourtdate = '" + cm_obj.infotocourtdate + "', ipaddress = '" + cm_obj.ipaddress + "',  lastmodified_date = '" + DateTime.Now.ToShortDateString() + "', user_id = '" + cm_obj.user_id + "', record_status = '" + cm_obj.record_status + "' WHERE seizure_fir_id ='" + cm_obj.seizure_fir_id + "'", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        
                            if (cm_obj.record_status == "Y")
                            {

                                cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure_accuseddetails	SET  fir_status='Y', seizure_fir_id='" + cm_obj.seizure_fir_id + "' WHERE seizureno ='" + cm_obj.seizureno + "' and raidby='" + cm_obj.raidby + "'", cn);
                                cmd.ExecuteNonQuery();

                            }
                        
                        trn.Commit();
                        cn.Close();
                        value = true;

                      // DL_Common.UpdateStageCode(cm_obj.seizureno, DL_Common.stage_code_FIR);
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
        public static cm_seiz_FIR GetDetailsById(string tableId)
        {
            cm_seiz_FIR result = new cm_seiz_FIR();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,d.Designation_name from  exciseautomation.seizure_fir a inner join exciseautomation.designation_master d on d.designation_code = a.designation_code Where seizure_fir_id= " + tableId + " order by seizure_fir_id", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        
                        result.seizure_fir_id = Convert.ToInt32(dr["seizure_fir_id"].ToString());
                        result.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                        result.designation_code = dr["designation_code"].ToString();
                        result.raidby = dr["raidby"].ToString();
                        result.prfirno = dr["prfirno"].ToString();
                        result.prfirdate = Convert.ToDateTime(dr["prfirdate"]).ToString("dd-MM-yyyy");
                        result.manualprfirno = dr["manualprfirno"].ToString();
                        result.manualbookdate = Convert.ToDateTime(dr["manualbookdate"]).ToString("dd-MM-yyyy");
                        result.raidorderby = dr["raidorderby"].ToString();
                        result.complaintno = dr["complaintno"].ToString();
                        result.complaintdate = Convert.ToDateTime(dr["complaintdate"]).ToString("dd-MM-yyyy");
                        result.infotocourtdate = Convert.ToDateTime(dr["infotocourtdate"]).ToString("dd-MM-yyyy");
                        result.finalseizureno = dr["finalseizureno"].ToString();
                        result.ipaddress = dr["ipaddress"].ToString();
                        result.lastmodified_date = dr["lastmodified_date"].ToString();
                        result.user_id = dr["user_id"].ToString();
                        result.creation_date = dr["creation_date"].ToString();
                        result.record_status = dr["record_status"].ToString();
                        result.record_deleted = dr["record_deleted"].ToString(); 
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
        public static List<cm_seiz_FIR> GetList(string adid)
        {
            List<cm_seiz_FIR> ads = new List<cm_seiz_FIR>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,d.Designation_name,S.* from  exciseautomation.seizure_fir a inner join exciseautomation.designation_master d on d.designation_code = a.designation_code inner join exciseautomation.seizure S on S.seizureno = a.seizureno and s.raidby=a.raidby  Where S.seizureno= " + adid + " order by seizure_fir_id", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        cm_seiz_FIR ad = new cm_seiz_FIR();
                        ad.seizure_fir_id = Convert.ToInt32(dr["seizure_fir_id"].ToString());
                        ad.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                        ad.finalseizureno =dr["finalseizureno"].ToString();
                        ad.designation_code = dr["designation_code"].ToString();
                        ad.raidby = dr["raidby"].ToString();
                        ad.prfirno = dr["prfirno"].ToString();                        
                        ad.manualprfirno = dr["manualprfirno"].ToString();
                        //ad.manualbookdate = dr["manualbookdate"].ToString();
                        //ad.prfirdate = dr["prfirdate"].ToString();
                        //ad.complaintdate = dr["complaintdate"].ToString();
                        //ad.infotocourtdate = dr["infotocourtdate"].ToString();
                        ad.infotocourtdate = Convert.ToDateTime(dr["infotocourtdate"]).ToString("dd-MM-yyyy");
                        ad.complaintdate = Convert.ToDateTime(dr["complaintdate"]).ToString("dd-MM-yyyy");
                        ad.manualbookdate = Convert.ToDateTime(dr["manualbookdate"]).ToString("dd-MM-yyyy"); //dr["manualbookdate"].ToString();
                        ad.prfirdate = Convert.ToDateTime(dr["prfirdate"]).ToString("dd-MM-yyyy"); 
                        ad.raidorderby = dr["raidorderby"].ToString();
                        ad.complaintno = dr["complaintno"].ToString();
                        ad.finalseizureno = dr["finalseizureno"].ToString();
                        ad.ipaddress = dr["ipaddress"].ToString();
                        ad.lastmodified_date = dr["lastmodified_date"].ToString();
                        ad.user_id = dr["user_id"].ToString();
                        ad.creation_date = dr["creation_date"].ToString();
                        ad.record_status = dr["record_status"].ToString();
                        ad.record_deleted = dr["record_deleted"].ToString();
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
