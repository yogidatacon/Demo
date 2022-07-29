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
    public class DL_cm_seiz_RaidTeam
    {
        public static string Insert(cm_seiz_RaidTeam raid)
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
                    NpgsqlCommand cmd1 = new NpgsqlCommand("select case when max(seizure_raiddetails_id) is null then 0 else max(seizure_raiddetails_id) end as seizure_raiddetails_id from exciseautomation.seizure_raiddetails", cn);
                    int maxid = Convert.ToInt32(cmd1.ExecuteScalar())+1;
                    StringBuilder str = new StringBuilder();
                    str.Append("INSERT INTO exciseautomation.seizure_raiddetails(seizure_raiddetails_id,seizureno,officername,designation_code,raidteamlead,user_id,creation_date,record_status,raidby,contact_no,ipaddress)Values(");
                    str.Append("'"+maxid+"','"+raid.seizureno+"','"+raid.officername.Trim()+"','"+raid.designation_code+"','"+raid.raidteamlead+"','"+raid.user_id+"','"+DateTime.Now.ToString("dd-MM-yyyy")+"','N','"+raid.raidby+"','"+raid.mobileno+"','"+raid.ipaddress+"')");
    
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    cmd.ExecuteNonQuery();
                    for (int i = 0; i < raid.docs.Count; i++)
                    {
                        str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.seizure_docs(raidby,seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date)");
                        str.Append("Values('"+raid.raidby+"','"+raid.docs[i].seizureno+"','" + maxid + "','" + raid.docs[i].doc_name + "', '" + raid.docs[i].description + "','" + raid.docs[i].doc_path + "','RDT','" + raid.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "')");
                        NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                        cmd3.ExecuteNonQuery();
                    }
                   
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


        public static List<cm_seiz_RaidTeam> GetDetails(string seizureNo)
        {
            List<cm_seiz_RaidTeam> ads = new List<cm_seiz_RaidTeam>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(" select  trim(a.raidteamlead) as raidteamlead,(a.*),b.Designation_name,designation_type_name, c.designation_type_code from exciseautomation.seizure_raiddetails a left join exciseautomation.designation_master b on b.designation_code = a.designation_code left join exciseautomation.designation_type_master c on b.designation_type_code=c.designation_type_code  where seizureNo=" + seizureNo + " order by a.seizure_raiddetails_id", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows) //form click
                    {
                        cm_seiz_RaidTeam ad = new cm_seiz_RaidTeam();
                        ad.officername = dr["officername"].ToString();
                        ad.designation_code = dr["Designation_code"].ToString();
                        ad.designation_name = dr["Designation_name"].ToString();
                        ad.raidteamlead = dr["designation_type_name"].ToString();
                        ad.raidteamcode = dr["designation_type_code"].ToString();
                        ad.seizure_raiddetails_id = dr["seizure_raiddetails_id"].ToString();
                        ad.record_status = dr["record_status"].ToString();
                        ad.seizureno = dr["seizureno"].ToString();
                        ad.raidby = dr["raidby"].ToString();
                        ad.mobileno = dr["contact_no"].ToString();
                        ad.user_id = dr["user_id"].ToString();
                        ad.docs = new List<Seizure_Docs>();
                        using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.Seizure_Docs where seizureno='" + ad.seizureno + "' and doc_type_code='RDT' and raidby='"+ad.raidby+"' and doc_id='"+ad.seizure_raiddetails_id+"' order by seizure_docs_id", cn))
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
        public static string Update(cm_seiz_RaidTeam ad)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("update exciseautomation.seizure_raiddetails set    raidteam_type='" + ad.raidteam_type+"', officername='"+ad.officername+"', designation_code='"+ad.designation_code+"', raidteamlead='"+ad.raidteamlead+"',  user_id='"+ad.user_id+ "',lastmodified_date ='" + DateTime.Now.ToString("dd-MM-yyyy")+"', record_status='"+ad.record_status+"',raidby='"+ad.raidby+ "',contact_no='"+ad.mobileno+"',ipaddress='"+ad.ipaddress+"' where seizure_raiddetails_id='" + ad.seizure_raiddetails_id+"'");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    cmd.ExecuteNonQuery();
                    cmd = new NpgsqlCommand("Delete from exciseautomation.seizure_docs where doc_type_code='RDT' and doc_id='" + ad.seizure_raiddetails_id+ "' and raidby='" + ad.raidby + "'", cn);
                    cmd.ExecuteNonQuery();
                    for (int i = 0; i < ad.docs.Count; i++)
                    {
                        str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.seizure_docs(seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,raidby)");
                        str.Append("Values('" + ad.docs[i].seizureno + "','" + ad.seizure_raiddetails_id + "','" + ad.docs[i].doc_name + "', '" + ad.docs[i].description + "','" + ad.docs[i].doc_path + "','RDT','" + ad.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+ad.raidby+"')");
                        NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                        cmd3.ExecuteNonQuery();
                    }
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
    }
}
