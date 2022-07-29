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
    #region DL_cm_seiz_ChargeSheet
    public class DL_cm_seiz_ChargeSheet
    {
    
        public static bool InsertSeiz_ChargeSheet(cm_seiz_ChargeSheet obj)
        {
            bool value = false;
            StringBuilder str = new StringBuilder();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    NpgsqlTransaction trn;
                    trn = cn.BeginTransaction();
                    try
                    {
                        string InsertQuery = "";
                        string tableName = "exciseautomation.seizure_chargesheet";
                        int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.seizure_chargesheet", "seizure_chargesheet_id").ToString()) + 1;
                        obj.finalseizureno = DL_Common.GetMaxID("exciseautomation.seizure where seizureno='" + obj.seizureno + "'", "finalseizureno").ToString();
                        if (obj.disposalmode_code != 4)
                        {
                            string columnNames = "seizure_chargesheet_id, seizureno, disposalmode_code, evidenceproof, placeof_seizedpropertykept,   chargesheet_remarks, finalseizureno, ipaddress, lastmodified_date, user_id, creation_date, record_status, record_deleted,raidby,chargesheet_date,vehicle_verification ,vehicle_fsl ,liquor_test ,liquor_fsl";
                            string input = max + "','" + obj.seizureno + "','" + obj.disposalmode_code + "','" + obj.evidenceproof + "','" + obj.placeof_seizedpropertykept + "','" + obj.chargesheet_remarks + "','" + obj.finalseizureno + "','" + obj.ipaddress + "','" + DateTime.Now.ToShortDateString() + "','" + obj.user_id + "','" + DateTime.Now.ToShortDateString() + "','" + obj.record_status + "','" + obj.record_deleted + "','" + obj.raidby + "','" + obj.chargesheet_date+"','"+obj.vehicle_verification+"','"+obj.vehicle_fsl+"','"+obj.liquor_test+"','"+obj.liquor_fsl;

                            InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";
                        }
                        else
                        {
                            string columnNames = "seizure_chargesheet_id, seizureno, disposalmode_code, evidenceproof, placeof_seizedpropertykept, producedatcourt_date,  chargesheet_remarks, finalseizureno, ipaddress, lastmodified_date, user_id, creation_date, record_status, record_deleted,raidby,chargesheet_date,vehicle_verification ,vehicle_fsl ,liquor_test ,liquor_fsl";
                            string input = max + "','" + obj.seizureno + "','" + obj.disposalmode_code + "','" + obj.evidenceproof + "','" + obj.placeof_seizedpropertykept + "','" + obj.producedatcourt_date + "','" + obj.chargesheet_remarks + "','" + obj.finalseizureno + "','" + obj.ipaddress + "','" + DateTime.Now.ToShortDateString() + "','" + obj.user_id + "','" + DateTime.Now.ToShortDateString() + "','" + obj.record_status + "','" + obj.record_deleted + "','" + obj.raidby + "','" + obj.chargesheet_date + "','" + obj.vehicle_verification + "','" + obj.vehicle_fsl + "','" + obj.liquor_test + "','" + obj.liquor_fsl;

                            InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";
                        }
                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            for (int i = 0; i < obj.docs.Count; i++)
                            {
                                n = 0;
                                str = new StringBuilder();
                                str.Append("INSERT INTO exciseautomation.seizure_docs(seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,raidby)");
                                str.Append("Values('" + obj.seizureno + "','" + max + "','" + obj.docs[i].doc_name + "', '" + obj.docs[i].description + "','" + obj.docs[i].doc_path + "','CHS','" + obj.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+obj.raidby+"')");
                                NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                                n = cmd3.ExecuteNonQuery();
                            }
                            value = true;
                            if (obj.user_id.Contains("thana_"))
                            {
                                cmd = new NpgsqlCommand("update exciseautomation.complaint_data set seizure_chargesheet_id='" + max + "' where seizureno='" + obj.seizureno + "' and prfirno='" + obj.prfirno + "'", cn);
                                cmd.ExecuteNonQuery();
                            }
                            //Update Stage Code in Seizure table
                            DL_Common.UpdateStageCode(obj.seizureno, DL_Common.stage_code_Chargesheet);

                            //Save Accused Details
                            //for (int i = 0; i < obj.accuseDetailsList.Count; i++)
                            //{
                                
                            //        cmd = new NpgsqlCommand();
                            //        if (obj.accuseDetailsList[i].chargesheet_status == "Y")
                            //            cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure_accuseddetails	SET  chargesheet_status='" + obj.accuseDetailsList[i].chargesheet_status + "', seizure_chargesheet_id='" + obj.seizure_chargesheet_id + "' WHERE seizure_accused_details_id ='" + obj.accuseDetailsList[i].seizure_accused_details_id + "'", cn);
                            //        else
                            //            cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure_accuseddetails	SET  chargesheet_status=NULL, seizure_chargesheet_id=NULL WHERE seizure_accused_details_id ='" + obj.accuseDetailsList[i].seizure_accused_details_id + "'", cn);
                            //        cmd.ExecuteNonQuery();
                                
                            //}

                            trn.Commit();
                            //_log.Info("Sugarcanepurchase Insertion Success:" + scp.sugarcanepurchase_id + '-' + scp.party_code);
                            cn.Close();
                        }
                        else
                        {
                            trn.Rollback();
                            value = false;
                        }
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
        public static bool Update(cm_seiz_ChargeSheet cm_obj)
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
                    StringBuilder str = new StringBuilder();
                    //producedatcourt_time='" + cm_obj.producedatcourt_time + "', --removed
                    if(cm_obj.disposalmode_code==4)
                        cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure_chargesheet set disposalmode_code='" + cm_obj.disposalmode_code + "', evidenceproof='" + cm_obj.evidenceproof + "', placeof_seizedpropertykept='" + cm_obj.placeof_seizedpropertykept + "', producedatcourt_date='" + cm_obj.producedatcourt_date + "',chargesheet_date='" + cm_obj.chargesheet_date + "',  chargesheet_remarks='" + cm_obj.chargesheet_remarks + "', finalseizureno='" + cm_obj.finalseizureno + "', ipaddress='" + cm_obj.ipaddress + "', lastmodified_date='" + DateTime.Now.ToShortDateString() + "', user_id='" + cm_obj.user_id + "', record_status='" + cm_obj.record_status + "', record_deleted='" + cm_obj.record_deleted + "',vehicle_verification='"+cm_obj.vehicle_verification+"' ,vehicle_fsl='"+cm_obj.vehicle_fsl+"' ,liquor_test='"+cm_obj.liquor_test+"' ,liquor_fsl='"+cm_obj.liquor_fsl+"' WHERE seizure_chargesheet_id ='" + cm_obj.seizure_chargesheet_id + "'", cn);
                    else

                    cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure_chargesheet set disposalmode_code='" + cm_obj.disposalmode_code + "', evidenceproof='" + cm_obj.evidenceproof + "', placeof_seizedpropertykept='" + cm_obj.placeof_seizedpropertykept + "', chargesheet_date='"+cm_obj.chargesheet_date+"',  chargesheet_remarks='" + cm_obj.chargesheet_remarks + "', finalseizureno='" + cm_obj.finalseizureno + "', ipaddress='" + cm_obj.ipaddress + "', lastmodified_date='" + DateTime.Now.ToShortDateString() + "', user_id='" + cm_obj.user_id + "', record_status='" + cm_obj.record_status + "', record_deleted='" + cm_obj.record_deleted + "',vehicle_verification='" + cm_obj.vehicle_verification + "' ,vehicle_fsl='" + cm_obj.vehicle_fsl + "' ,liquor_test='" + cm_obj.liquor_test + "' ,liquor_fsl='" + cm_obj.liquor_fsl + "' WHERE seizure_chargesheet_id ='" + cm_obj.seizure_chargesheet_id + "'", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {

                        //for (int i = 0; i < cm_obj.accuseDetailsList.Count; i++)
                        //{
                        //    cmd = new NpgsqlCommand();
                        //    if (cm_obj.accuseDetailsList[i].chargesheet_status == "Y")
                        //        cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure_accuseddetails	SET  chargesheet_status='" + cm_obj.accuseDetailsList[i].chargesheet_status + "', seizure_chargesheet_id='" + cm_obj.seizure_chargesheet_id + "' WHERE seizure_accused_details_id ='" + cm_obj.accuseDetailsList[i].seizure_accused_details_id + "'", cn);
                        //    else
                        //        cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure_accuseddetails	SET  chargesheet_status=NULL, seizure_chargesheet_id=NULL WHERE seizure_accused_details_id ='" + cm_obj.accuseDetailsList[i].seizure_accused_details_id + "'", cn);
                        //    cmd.ExecuteNonQuery();
                        //}
                        value = true;

                    }
                    else
                    {
                        value = false;
                    }
                    cmd = new NpgsqlCommand("Delete from exciseautomation.seizure_docs where doc_type_code='CHS' and doc_id='" + cm_obj.seizure_chargesheet_id + "'", cn);
                    cmd.ExecuteNonQuery();
                    for (int i = 0; i < cm_obj.docs.Count; i++)
                    {
                        str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.seizure_docs(seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,raidby)");
                        str.Append("Values('" + cm_obj.seizureno + "','" + cm_obj.seizure_chargesheet_id + "','" + cm_obj.docs[i].doc_name + "', '" + cm_obj.docs[i].description + "','" + cm_obj.docs[i].doc_path + "','CHS','" + cm_obj.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+cm_obj.raidby+"')");
                        NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                        n = cmd3.ExecuteNonQuery();
                    }
                  
                    if (n == 1)
                    {
                        value = true;                        
                        trn.Commit();
                    }
                    else
                    {
                        value = false;
                        trn.Rollback();
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
        public static cm_seiz_ChargeSheet GetDetailsById(string tableId)
        {
            cm_seiz_ChargeSheet result = new cm_seiz_ChargeSheet();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT cs.*,prfirno  FROM exciseautomation.seizure_chargesheet cs INNER JOIN exciseautomation.seizure_fir F ON F.seizureno = cs.seizureno Where seizure_chargesheet_id= " + tableId + " order by seizure_chargesheet_id", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        result.seizure_chargesheet_id = Convert.ToInt32(dr["seizure_chargesheet_id"].ToString());
                        result.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                        result.disposalmode_code = Convert.ToInt32(dr["disposalmode_code"].ToString());
                        result.evidenceproof = dr["evidenceproof"].ToString();
                        result.placeof_seizedpropertykept = dr["placeof_seizedpropertykept"].ToString();
                        if (dr["producedatcourt_date"].ToString() != "" && dr["producedatcourt_date"].ToString() != null)
                            result.producedatcourt_date = Convert.ToDateTime(dr["producedatcourt_date"]).ToString("dd-MM-yyyy");
                        result.producedatcourt_time = dr["producedatcourt_time"].ToString();
                        result.chargesheet_remarks = dr["chargesheet_remarks"].ToString();
                        result.finalseizureno = dr["finalseizureno"].ToString();
                        result.ipaddress = dr["ipaddress"].ToString();
                        result.lastmodified_date = dr["lastmodified_date"].ToString();
                        result.user_id = dr["user_id"].ToString();
                        result.creation_date = dr["creation_date"].ToString();
                        result.record_status = dr["record_status"].ToString();
                        result.record_deleted = dr["record_deleted"].ToString();
                        result.prfirno = dr["prfirno"].ToString();
                        result.raidby = dr["raidby"].ToString();
                        result.chargesheet_date = dr["chargesheet_date"].ToString();
                        result.vehicle_verification = dr["vehicle_verification"].ToString();
                        result.vehicle_fsl = dr["vehicle_fsl"].ToString();
                        result.liquor_test = dr["liquor_test"].ToString();
                        result.liquor_fsl = dr["liquor_fsl"].ToString();
                        result.docs = new List<Seizure_Docs>();
                        try
                        {
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.Seizure_Docs where doc_id='" + result.seizure_chargesheet_id + "' and doc_type_code='CHS' and raidby='"+result.raidby+"' order by seizure_docs_id", cn))
                            {
                                cmd1.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr2 = cmd1.ExecuteReader();

                                if (dr2.HasRows)
                                {
                                    while (dr2.Read())
                                    {
                                        Seizure_Docs doc = new Seizure_Docs();
                                        doc.seizure_docs_id = dr2["seizure_docs_id"].ToString();
                                        doc.doc_id = dr2["doc_id"].ToString();
                                        doc.doc_name = dr2["doc_Name"].ToString();
                                        doc.description = dr2["doc_desc"].ToString();
                                        doc.doc_path = dr2["doc_path"].ToString();
                                        result.docs.Add(doc);
                                    }
                                }
                                dr2.Close();
                            }
                            //lstObj.Add(record);
                        }
                        catch (Exception ex)
                        {
                            //throw;
                        }
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
        public static List<cm_seiz_ChargeSheet> GetList(string adid)
        {
            List<cm_seiz_ChargeSheet> ads = new List<cm_seiz_ChargeSheet>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT cs.*,prfirno,prfirdate,c.disposalmode_name  FROM exciseautomation.seizure_chargesheet cs INNER JOIN exciseautomation.seizure_fir F ON F.seizureno = cs.seizureno  inner join exciseautomation.disposalmode_master c on cs.disposalmode_code=c.disposalmode_code Where cs.seizureno= " + adid + " order by seizure_chargesheet_id", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        cm_seiz_ChargeSheet ad = new cm_seiz_ChargeSheet();
                        ad.seizure_chargesheet_id = Convert.ToInt32(dr["seizure_chargesheet_id"].ToString());
                        ad.finalseizureno = (dr["finalseizureno"].ToString());
                        ad.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                        ad.disposalmode_code = Convert.ToInt32(dr["disposalmode_code"].ToString());
                        ad.disposalmode_name = dr["disposalmode_name"].ToString();
                        ad.evidenceproof = dr["evidenceproof"].ToString();
                        ad.placeof_seizedpropertykept = dr["placeof_seizedpropertykept"].ToString();
                        if(dr["producedatcourt_date"].ToString()!="" && dr["producedatcourt_date"].ToString() != null)
                        ad.producedatcourt_date = Convert.ToDateTime(dr["producedatcourt_date"]).ToString("dd-MM-yyyy");
                        ad.producedatcourt_time = dr["producedatcourt_time"].ToString();
                        ad.chargesheet_remarks = dr["chargesheet_remarks"].ToString();
                        ad.finalseizureno = dr["finalseizureno"].ToString();
                        ad.ipaddress = dr["ipaddress"].ToString();
                        ad.lastmodified_date = dr["lastmodified_date"].ToString();
                        ad.user_id = dr["user_id"].ToString();
                        ad.creation_date = dr["creation_date"].ToString();
                        ad.record_status = dr["record_status"].ToString();
                        ad.record_deleted = dr["record_deleted"].ToString();
                        ad.prfirno = dr["prfirno"].ToString();
                        ad.prfirdate = Convert.ToDateTime(dr["prfirdate"]).ToString("dd-MM-yyyy");
                        ad.raidby = dr["raidby"].ToString();
                        ad.vehicle_verification = dr["vehicle_verification"].ToString();
                        ad.vehicle_fsl = dr["vehicle_fsl"].ToString();
                        ad.liquor_test = dr["liquor_test"].ToString();
                        ad.liquor_fsl = dr["liquor_fsl"].ToString();
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
    #endregion DL_cm_seiz_ChargeSheet
}
