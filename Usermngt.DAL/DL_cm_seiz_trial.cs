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
    public class DL_cm_seiz_trial
    {
        public static bool InsertSeiz_trial(cm_seiz_trial obj, string trialstage_code)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    NpgsqlTransaction trn;
                    StringBuilder str = new StringBuilder();
                    trn = cn.BeginTransaction();
                    try
                    {
                        string tableName = "exciseautomation.seizure_trial";
                        int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.seizure_trial", "seizure_trial_id").ToString()) + 1;
                        string columnNames="", input="";

                        switch (trialstage_code)
                        {
                            case "1":
                            case "2":
                            case "3":
                            
                                columnNames = "seizure_trial_id, seizureno, trialstage_code, currentstagedate, chargedundersection, nexthearingdate, finalseizureno,currentstage, lastmodified_date, user_id, creation_date, record_status, record_deleted,raidby";
                                input = max + "','" + obj.seizureno + "','" + obj.trialstage_code + "','" + obj.currentstagedate + "','" + obj.chargedundersection + "','" + obj.nexthearingdate + "','" + obj.finalseizureno + "','" + obj.currentstage + "','" + obj.lastmodified_date + "','" + obj.user_id + "','" + obj.creation_date + "','" + obj.record_status + "','" + obj.record_deleted+"','"+obj.raidby;
                                break;

                            case "4":
                                columnNames = "seizure_trial_id, seizureno, trialstage_code, currentstagedate, chargedundersection, nexthearingdate, finalseizureno,currentstage, lastmodified_date, user_id, creation_date, record_status, record_deleted,raidby";
                                input = max + "','" + obj.seizureno + "','" + obj.trialstage_code + "','" + obj.currentstagedate + "','" + obj.chargedundersection + "','" + obj.nexthearingdate + "','" + obj.finalseizureno + "','" + obj.currentstage + "','" + obj.lastmodified_date + "','" + obj.user_id + "','" + obj.creation_date + "','" + obj.record_status + "','" + obj.record_deleted+"','"+obj.raidby;
                                break;

                                //todo: Save Documents
                            case "5":
                            case "6":
                            case "7":
                            case "8":
                                columnNames = "seizure_trial_id, seizureno, trialstage_code, currentstagedate, chargedundersection, nexthearingdate, finalseizureno,currentstage, remarks, lastmodified_date, user_id, creation_date, record_status, record_deleted,raidby";
                                input = max + "','" + obj.seizureno + "','" + obj.trialstage_code + "','" + obj.currentstagedate + "','" + obj.chargedundersection + "','" + obj.nexthearingdate + "','" + obj.finalseizureno + "','" + obj.currentstage + "','" + obj.remarks + "','" + obj.lastmodified_date + "','" + obj.user_id + "','" + obj.creation_date + "','" + obj.record_status + "','" + obj.record_deleted+"','"+obj.raidby;
                                break;
                           
                            
                            case "9":
                                columnNames = "seizure_trial_id, seizureno, trialstage_code, currentstagedate, chargedundersection,  finalseizureno,currentstage, punishment, fine, remarks, lastmodified_date, user_id, creation_date, record_status, record_deleted,raidby";
                                input = max + "','" + obj.seizureno + "','" + obj.trialstage_code + "','" + obj.currentstagedate + "','" + obj.chargedundersection + "','" +  obj.finalseizureno + "','" + obj.currentstage + "','" + obj.punishment + "','" + obj.fine + "','" + obj.remarks + "','" + obj.lastmodified_date + "','" + obj.user_id + "','" + obj.creation_date + "','" + obj.record_status + "','" + obj.record_deleted+"','"+obj.raidby;
                                break;
                            default:
                                break;
                        }

                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();

                        if (n == 1)
                        {
                            if (obj.trialstage_code == 5 || obj.trialstage_code == 6 || obj.trialstage_code == 7 ||  obj.trialstage_code == 9)
                            {
                                for (int i = 0; i < obj.docs.Count; i++)
                                {
                                    n = 0;
                                    str = new StringBuilder();
                                    str.Append("INSERT INTO exciseautomation.seizure_docs(seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,raidby)");
                                    str.Append("Values('" + obj.seizureno + "','" + max + "','" + obj.docs[i].doc_name + "', '" + obj.docs[i].description + "','" + obj.docs[i].doc_path + "','STD','" + obj.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+obj.raidby+"')");
                                    NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                                    n = cmd3.ExecuteNonQuery();
                                }
                            }
                            trn.Commit();
                            value = true;

                            //Update Stage Code in Seizure table
                            //string stage = trialstage_code;
                            int stage_value = 0;
                            switch (trialstage_code)
                            {
                                case "1":
                                    stage_value = DL_Common.stage_code_Cognizance;
                                    break;
                                case "2":
                                    stage_value = DL_Common.stage_code_Appearance;
                                    break;
                                case "3":
                                    stage_value = DL_Common.stage_code_PolicePaperSupply;
                                    break;
                                case "4":
                                    stage_value = DL_Common.stage_code_FramingCharged;
                                    break;
                                case "5":
                                    stage_value = DL_Common.stage_code_ProsecutionEvidence;
                                    break;
                                case "6":
                                    stage_value = DL_Common.stage_code_AccusedStatement;
                                    break;
                                case "7":
                                    stage_value = DL_Common.stage_code_DefenceStatement;
                                    break;
                                case "8":
                                    stage_value = DL_Common.stage_code_FinalArgumentList;
                                    break;
                                case "9":
                                    stage_value = DL_Common.stage_code_JudgementList;
                                    break;
                            }
                            DL_Common.UpdateStageCode(obj.seizureno, stage_value);

                            if (trialstage_code=="9")
                            {
                                DL_Common.UpdateSeizure(obj.seizureno, stage_value, obj.accusedId, obj.judgementType);
                            }
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
        public static bool Update(cm_seiz_trial cm_obj)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                    cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                StringBuilder str = new StringBuilder();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    string query = "";
                    if(cm_obj.trialstage_code==1 || cm_obj.trialstage_code == 2 || cm_obj.trialstage_code == 3 || cm_obj.trialstage_code == 4)
                    query = "UPDATE exciseautomation.seizure_trial	SET   currentstagedate= '" + cm_obj.currentstagedate + "', chargedundersection= '" + cm_obj.chargedundersection + "', nexthearingdate= '" + cm_obj.nexthearingdate + "', lastmodified_date = '" + DateTime.Now.ToShortDateString() + "', user_id = '" + cm_obj.user_id + "', record_status = '" + cm_obj.record_status + "' WHERE seizure_trial_id ='" + cm_obj.seizure_trial_id + "'";


                    else if (cm_obj.trialstage_code == 5 || cm_obj.trialstage_code == 6 || cm_obj.trialstage_code == 7 || cm_obj.trialstage_code == 8)
                        query = "UPDATE exciseautomation.seizure_trial	SET remarks= '" + cm_obj.remarks + "',  currentstagedate= '" + cm_obj.currentstagedate + "', chargedundersection= '" + cm_obj.chargedundersection + "', nexthearingdate= '" + cm_obj.nexthearingdate + "', lastmodified_date = '" + DateTime.Now.ToShortDateString() + "', user_id = '" + cm_obj.user_id + "', record_status = '" + cm_obj.record_status + "' WHERE seizure_trial_id ='" + cm_obj.seizure_trial_id + "'";

                    else if (cm_obj.trialstage_code == 8 || cm_obj.trialstage_code == 9)
                        query = "UPDATE exciseautomation.seizure_trial	SET   currentstagedate= '" + cm_obj.currentstagedate + "', chargedundersection= '" + cm_obj.chargedundersection + "', punishment= '" + cm_obj.punishment + "', fine= '" + cm_obj.fine + "', remarks= '" + cm_obj.remarks + "', lastmodified_date = '" + DateTime.Now.ToShortDateString() + "', user_id = '" + cm_obj.user_id + "', record_status = '" + cm_obj.record_status + "' WHERE seizure_trial_id ='" + cm_obj.seizure_trial_id + "'";


                    cmd = new NpgsqlCommand(query, cn);
                    int n = cmd.ExecuteNonQuery();

                    if (cm_obj.trialstage_code == 5 || cm_obj.trialstage_code == 6 || cm_obj.trialstage_code == 7 ||  cm_obj.trialstage_code == 9)
                    {
                        cmd = new NpgsqlCommand("Delete from exciseautomation.seizure_docs where doc_type_code='STD' and doc_id='" + cm_obj.seizure_trial_id + "' and raidby='"+cm_obj.raidby+"'" , cn);
                        cmd.ExecuteNonQuery();
                        for (int i = 0; i < cm_obj.docs.Count; i++)
                        {
                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.seizure_docs(seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,raidby)");
                            str.Append("Values('" + cm_obj.seizureno + "','" + cm_obj.seizure_trial_id + "','" + cm_obj.docs[i].doc_name + "', '" + cm_obj.docs[i].description + "','" + cm_obj.docs[i].doc_path + "','STD','" + cm_obj.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+cm_obj.raidby+"')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd3.ExecuteNonQuery();
                        }
                    }
                    if (n == 1)
                    {
                        value = true;
                        if (cm_obj.trialstage_code == 9)
                        {
                            DL_Common.UpdateSeizure(cm_obj.seizureno, DL_Common.stage_code_JudgementList, cm_obj.accusedId, cm_obj.judgementType);
                        }
                    }
                    else
                    {
                        value = false;
                    }
                    trn.Commit();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    value = false;
                    trn.Rollback();
                    throw (ex);
                }
            }
            return value;
        }
        public static cm_seiz_trial GetDetailsById(string tableId)
        {
            cm_seiz_trial result = new cm_seiz_trial();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {                    
                    //NpgsqlCommand cmd = new NpgsqlCommand("SELECT seizure_trial_id, seizureno, trialstage_code, currentstagedate, chargedundersection, witnessdetails, accusedstatement, nexthearingdate, remarks, finalseizureno, lastmodified_date, user_id, creation_date, record_status, record_deleted, currentstage, columnconviction, fine, punishment FROM exciseautomation.seizure_trial Where seizure_trial_id= " + tableId + " order by seizure_trial_id", cn);
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT ad.Judgement_status,seizure_accused_details_id,accusedname, seizure_trial_id, st.seizureno, trialstage_code, currentstagedate,chargedundersection, witnessdetails,accusedstatement, nexthearingdate, st.remarks, finalseizureno, st.lastmodified_date, st.user_id, st.creation_date, st.record_status,st.record_deleted, currentstage, columnconviction, fine, punishment FROM exciseautomation.seizure_trial st INNER JOIN exciseautomation.seizure_accuseddetails ad ON ad.seizureno = st.seizureno Where seizure_trial_id= " + tableId + " order by seizure_trial_id", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        result.seizure_trial_id = Convert.ToInt32(dr["seizure_trial_id"].ToString());
                        result.seizureno = Convert.ToInt32(dr["seizureno"].ToString());                        
                        result.trialstage_code = Convert.ToInt32(dr["trialstage_code"].ToString());
                        if(dr["currentstagedate"].ToString()!="")
                        result.currentstagedate = dr["currentstagedate"].ToString().Substring(0, 10).Replace("/", "-");
                        result.chargedundersection = dr["chargedundersection"].ToString();
                        result.witnessdetails = dr["witnessdetails"].ToString();
                        result.accusedstatement = dr["accusedstatement"].ToString();
                        if (result.trialstage_code != 9) //Judgement nexthearingdate not available
                        {
                            if (dr["nexthearingdate"].ToString() != "")
                                result.nexthearingdate = dr["nexthearingdate"].ToString().Substring(0, 10).Replace("/", "-");
                        }
                        result.remarks = dr["remarks"].ToString();
                        result.finalseizureno = dr["finalseizureno"].ToString();
                        result.currentstage = Convert.ToInt32(dr["currentstage"].ToString());
                        result.columnconviction = dr["columnconviction"].ToString();
                        result.fine = dr["fine"].ToString();
                        result.punishment = dr["punishment"].ToString();
                        result.finalseizureno = dr["finalseizureno"].ToString();
                        result.lastmodified_date = dr["lastmodified_date"].ToString();
                        result.user_id = dr["user_id"].ToString();
                        result.creation_date = dr["creation_date"].ToString();
                        result.record_status = dr["record_status"].ToString();
                        result.record_deleted = dr["record_deleted"].ToString();
                        result.judgementType= dr["Judgement_status"].ToString();
                        result.accusedId = dr["seizure_accused_details_id"].ToString();
                        result.docs = new List<Seizure_Docs>();

                        try
                        {
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.Seizure_Docs where doc_id='" + result.seizure_trial_id + "' and doc_type_code='STD' order by seizure_docs_id", cn))
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
        public static List<cm_seiz_trial> GetList(string seizureNo, string trialstage_code)
        {
            List<cm_seiz_trial> ads = new List<cm_seiz_trial>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT raidby,seizure_trial_id, seizureno, trialstage_code, currentstagedate, chargedundersection, witnessdetails, accusedstatement, nexthearingdate, remarks, finalseizureno, lastmodified_date, user_id, creation_date, record_status, record_deleted, currentstage, columnconviction, fine, punishment FROM exciseautomation.seizure_trial Where seizureno= " + seizureNo + " and trialstage_code = "+trialstage_code+"  order by seizure_trial_id", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        cm_seiz_trial ad = new cm_seiz_trial();
                        ad.seizure_trial_id = Convert.ToInt32(dr["seizure_trial_id"].ToString());
                        ad.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                        ad.trialstage_code = Convert.ToInt32(dr["trialstage_code"].ToString());
                        if(dr["currentstagedate"].ToString()!="")
                        ad.currentstagedate =dr["currentstagedate"].ToString().Substring(0, 10).Replace("/", "-"); ;
                        ad.chargedundersection = dr["chargedundersection"].ToString();
                        ad.witnessdetails = dr["witnessdetails"].ToString();
                        ad.accusedstatement = dr["accusedstatement"].ToString();
                        if(dr["nexthearingdate"].ToString() != "")
                        ad.nexthearingdate =dr["nexthearingdate"].ToString().Substring(0,10).Replace("/","-");

                        ad.remarks = dr["remarks"].ToString();
                        ad.finalseizureno = dr["finalseizureno"].ToString();
                        ad.currentstage = Convert.ToInt32(dr["currentstage"].ToString());
                        ad.columnconviction = dr["columnconviction"].ToString();
                        ad.fine = dr["fine"].ToString();
                        ad.punishment = dr["punishment"].ToString();
                        ad.finalseizureno = dr["finalseizureno"].ToString();                        
                        ad.lastmodified_date = dr["lastmodified_date"].ToString();
                        ad.user_id = dr["user_id"].ToString();
                        ad.creation_date = dr["creation_date"].ToString();
                        ad.record_status = dr["record_status"].ToString();
                        ad.record_deleted = dr["record_deleted"].ToString();
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
