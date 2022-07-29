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
    public class DL_cm_seiz_OffencesCommitted
    {
        public static string Insert(cm_seiz_OffencesCommitted off)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    //  ad.accusedstatus_code ="1";
                    NpgsqlCommand cmd1 = new NpgsqlCommand("select case when max(seizure_accused_offences_id) is null then 0 else max(seizure_accused_offences_id) end as seizure_accused_offences_id from exciseautomation.seizure_accusedoffences", cn);
                    int maxid = Convert.ToInt32(cmd1.ExecuteScalar())+1;
                    StringBuilder str = new StringBuilder();
                    if (!string.IsNullOrEmpty(off.offence_code))
                    {
                        str.Append("INSERT INTO exciseautomation.seizure_accusedoffences(seizure_accused_offences_id, seizure_accused_details_id, seizureno, offence_code,offence_section_code, offence_details,other_offence_details,   user_id, creation_date, record_status,raidby,ipaddress)Values(");
                        str.Append("'" + maxid + "','" + off.seizure_accused_details_id + "','" + off.seizureno + "','" + off.offence_code + "','" + off.offence_section_code + "','" + off.offence_details + "','"+off.other_offences+"','" + off.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + off.record_status + "','"+off.raidby+"','"+off.ipaddress+"')");
                    }
                    //else
                    //{
                    //    str.Append("INSERT INTO exciseautomation.seizure_accusedoffences(seizure_accused_offences_id, seizure_accused_details_id, seizureno,  offencename,   user_id, creation_date, record_status)Values(");
                    //    str.Append("'" + maxid + "','" + off.seizure_accused_details_id + "','" + off.seizureno + "','" +  off.offencename + "','" + off.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + off.record_status + "')");
                    //}
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    //cmd = new NpgsqlCommand("Delete from exciseautomation.seizure_docs where doc_type_code='RDT' and doc_id='" + ad.seizure_raiddetails_id + "'", cn);
                    //cmd.ExecuteNonQuery();
                    for (int i = 0; i < off.docs.Count; i++)
                    {
                        str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.seizure_docs(raidby,seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date)");
                        str.Append("Values('"+off.raidby+"','" + off.docs[i].seizureno + "','" + maxid + "','" + off.docs[i].doc_name + "', '" + off.docs[i].description + "','" + off.docs[i].doc_path + "','OFF','" + off.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "')");
                        NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                        cmd3.ExecuteNonQuery();
                    }
                    cmd.ExecuteNonQuery();
                    VAL = "0";
                    trn.Commit();
                    cn.Close();


                }
                catch (Exception ex1)
                {
                    //  _log.Info("NOC Insertion Fail:" + noc.noc_id + '-' + noc.party_code + " - " + ex1.Message);
                    VAL = ex1.Message;
                    trn.Rollback();
                }

            }
            return VAL;
        }

        public static List<cm_seiz_OffencesCommitted> GetoffenceList(string v)
        {
            List<cm_seiz_OffencesCommitted> ads = new List<cm_seiz_OffencesCommitted>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {

                    NpgsqlCommand cmd = new NpgsqlCommand(" select distinct offence_code,offence_section_code  from exciseautomation.seizure_accusedoffences  order by offence_code", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        cm_seiz_OffencesCommitted ad = new cm_seiz_OffencesCommitted();
                       // ad.seizure_accused_details_id = dr["seizure_accused_details_id"].ToString();
                      //  ad.accusedname = dr["accusedname"].ToString();
                        ad.offence_code = dr["offence_code"].ToString();
                       // ad.offence_name = dr["offence_name"].ToString();
                        ad.offence_section_code = dr["offence_section_code"].ToString();
                      //  ad.offence_details = dr["offence_details"].ToString();
                       // ad.other_offences = dr["other_offence_details"].ToString();
                       // ad.seizure_accused_offences_id = dr["seizure_accused_offences_id"].ToString();
                       // ad.record_status = dr["record_status"].ToString();
                       // ad.seizureno = dr["seizureno"].ToString();
                       // ad.docs = new List<Seizure_Docs>();
                        
                    }
                    trn.Commit();
                    cn.Close();


                }
                catch (Exception ex1)
                {
                    //  _log.Info("NOC Insertion Fail:" + noc.noc_id + '-' + noc.party_code + " - " + ex1.Message);
                    // VAL = ex1.Message;
                    trn.Rollback();
                }

            }
            return ads;
        }

        public static List<cm_seiz_OffencesCommitted> GetDetails(string seizureno)
        {
            List<cm_seiz_OffencesCommitted> ads = new List<cm_seiz_OffencesCommitted>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {

                    NpgsqlCommand cmd = new NpgsqlCommand(" select a.*,b.accusedname,d.offence_name from exciseautomation.seizure_accusedoffences a inner join exciseautomation.seizure_accuseddetails b on a.seizure_accused_details_id = b.seizure_accused_details_id and a.seizureno=b.seizureno and a.raidby=b.raidby left join exciseautomation.offence_master d on d.offence_code = a.offence_code  where a.seizureno='" + seizureno+"' order by a.seizure_accused_offences_id", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        cm_seiz_OffencesCommitted ad = new cm_seiz_OffencesCommitted();
                        ad.seizure_accused_details_id = dr["seizure_accused_details_id"].ToString();
                        ad.accusedname = dr["accusedname"].ToString();
                        ad.offence_code = dr["offence_code"].ToString();
                        ad.offence_name = dr["offence_name"].ToString();
                        ad.offence_section_code = dr["offence_section_code"].ToString();
                        ad.offence_details = dr["offence_details"].ToString();
                        ad.other_offences = dr["other_offence_details"].ToString();
                        ad.seizure_accused_offences_id = dr["seizure_accused_offences_id"].ToString();
                        ad.record_status = dr["record_status"].ToString();
                        ad.seizureno = dr["seizureno"].ToString();
                        ad.raidby = dr["raidby"].ToString();
                        ad.docs = new List<Seizure_Docs>();
                        ad.user_id = dr["user_id"].ToString();
                        using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.Seizure_Docs where doc_id='" + ad.seizure_accused_offences_id + "' and doc_type_code='OFF' and raidby='"+ad.raidby+"' order by seizure_docs_id", cn))
                        {
                            cmd1.CommandType = System.Data.CommandType.Text;
                            //cmd.Parameters.AddWithValue("@UserID", userid);
                            NpgsqlDataReader dr2 = cmd1.ExecuteReader();

                            if (dr2.HasRows)
                            {

                                while (dr2.Read())
                                {
                                    Seizure_Docs doc = new Seizure_Docs();
                                    doc.seizure_docs_id =dr2["seizure_docs_id"].ToString();
                                    doc.doc_id = dr2["doc_id"].ToString();
                                    doc.doc_name = dr2["doc_Name"].ToString();
                                    doc.description = dr2["doc_desc"].ToString();
                                    doc.doc_path = dr2["doc_path"].ToString();
                                    ad.docs.Add(doc);
                                }

                            }
                            dr2.Close();
                        }
                        
                        ads.Add(ad);
                    }
                    trn.Commit();
                    cn.Close();


                }
                catch (Exception ex1)
                {
                    //  _log.Info("NOC Insertion Fail:" + noc.noc_id + '-' + noc.party_code + " - " + ex1.Message);
                    // VAL = ex1.Message;
                    trn.Rollback();
                }

            }
            return ads;
        }

        public static string Update(cm_seiz_OffencesCommitted ad)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    //  ad.accusedstatus_code = "1";

                    StringBuilder str = new StringBuilder();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    if(ad.offence_code != string.Empty)
                    cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure_accusedoffences  SET  seizure_accused_details_id ='" + ad.seizure_accused_details_id + "', offence_code ='" + ad.offence_code + "', offence_section_code ='" + ad.offence_section_code + "',offence_details='"+ad.offence_details+"',other_offence_details='"+ad.other_offences+"', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "', user_id ='" + ad.user_id + "', record_deleted ='" + ad.record_deleted + "',raidby='"+ad.raidby+"',ipaddress='"+ad.ipaddress+"' WHERE seizure_accused_offences_id =" + ad.seizure_accused_offences_id + "", cn);
                    //else
                    //    cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure_accusedoffences  SET  seizure_accused_details_id ='" + ad.seizure_accused_details_id + "', offence_type_code =null, offencename ='" + ad.offencename + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "', user_id ='" + ad.user_id + "', record_deleted ='" + ad.record_deleted + "' WHERE seizure_accused_offences_id =" + ad.seizure_accused_offences_id + "", cn);
                    int n = cmd.ExecuteNonQuery();

                    cmd = new NpgsqlCommand("Delete from exciseautomation.seizure_docs where doc_type_code='OFF' and doc_id='" + ad.seizure_accused_offences_id + "' and raidby='" + ad.raidby + "'", cn);
                    cmd.ExecuteNonQuery();
                    for (int i = 0; i < ad.docs.Count; i++)
                    {
                        str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.seizure_docs(seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,raidby)");
                        str.Append("Values('" + ad.docs[i].seizureno + "','" + ad.seizure_accused_offences_id + "','" + ad.docs[i].doc_name + "', '" + ad.docs[i].description + "','" + ad.docs[i].doc_path + "','OFF','" + ad.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+ad.raidby+"')");
                        NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                        cmd3.ExecuteNonQuery();
                    }
                    if (n == 1)
                    {
                        VAL = "0";
                        trn.Commit();
                        cn.Close();
                    }
                    else
                    {
                        VAL = "1";
                        trn.Rollback();
                    }

                }
                catch (Exception ex1)
                {
                    //  _log.Info("NOC Insertion Fail:" + noc.noc_id + '-' + noc.party_code + " - " + ex1.Message);
                    VAL = ex1.Message;
                    trn.Rollback();
                }

            }
            return VAL;
        }
    }
}
