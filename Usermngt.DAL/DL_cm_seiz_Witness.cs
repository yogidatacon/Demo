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
    public class DL_cm_seiz_Witness
    {
        public static List<cm_seiz_Witness> GetList(string seizureNo)
        {
            List<cm_seiz_Witness> ads = new List<cm_seiz_Witness>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    //seizure_witnessdetails_id, seizureno, gender_code, witnesstype, designation_code, witnessname, relativename, witness_age, presentaddress, permanentaddress, mobile, landline, ipaddress, witness_emailid, lastmodified_date, user_id, creation_date, record_status, record_deleted
                   // NpgsqlCommand cmd = new NpgsqlCommand(" select a.*,b.Designation_name from exciseautomation.seizure_witnessdetails a inner join exciseautomation.designation_master b on b.designation_code = a.designation_code order by a.seizure_raiddetails_id", cn);
                    NpgsqlCommand cmd = new NpgsqlCommand("select w.*, g.gender_name,d.designation_name from exciseautomation.seizure_witnessdetails w   left join exciseautomation.gender_master g on g.gender_code = w.gender_code left join exciseautomation.designation_master d on w.designation_code=d.designation_code where seizureNo=" + seizureNo+ " order by seizure_witnessdetails_id desc  ", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        cm_seiz_Witness ad = new cm_seiz_Witness();
                        ad.seizure_witnessdetails_id = dr["seizure_witnessdetails_id"].ToString();
                        ad.seizureno = dr["seizureno"].ToString();
                        ad.gender_code = dr["gender_name"].ToString();
                        ad.witnesstype = dr["witnesstype"].ToString();
                        //ad.designation_code = dr["designation_code"].ToString();
                        ad.designation_code = dr["designation_name"].ToString();
                        if (dr["witnesstype"].ToString() == "I")
                            ad.witnesstype = "IndependentPerson";
                                else if (dr["witnesstype"].ToString() == "D")
                            ad.witnesstype = "DepartmentOfficer";
                                else
                            ad.witnesstype = dr["witnesstype"].ToString();

                        ad.witnessname = dr["witnessname"].ToString();
                        ad.relativename = dr["relativename"].ToString();
                        ad.witness_age = dr["witness_age"].ToString();
                        ad.presentaddress = dr["presentaddress"].ToString();
                        ad.permanentaddress = dr["permanentaddress"].ToString();
                        ad.raidby = dr["raidby"].ToString();
                        ad.permanentaddress = dr["permanentaddress"].ToString();
                        ad.mobile = dr["mobile"].ToString();
                        ad.landline = dr["landline"].ToString();
                        ad.witness_emailid = dr["witness_emailid"].ToString();
                        ad.record_status = dr["record_status"].ToString();
                        ad.user_id = dr["user_id"].ToString();
                        ad.docs = new List<Seizure_Docs>();
                        using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.Seizure_Docs where seizureno='" + ad.seizureno + "' and doc_type_code='WDT' and raidby='"+ad.raidby+"' order by seizureno", cn))
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

        public static List<cm_seiz_Witness> WitnessSearch(string witnessname, string fatherName, string mobile)
        {
            List<cm_seiz_Witness> ads = new List<cm_seiz_Witness>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    string query = "";
                    if (witnessname!="" && fatherName==""&& mobile=="")
                     query = "select w.*,d.Designation_name,g.gender_name from exciseautomation.seizure_witnessdetails w  left join exciseautomation.designation_master d on d.designation_code = w.designation_code   left join exciseautomation.gender_master g on g.gender_code = w.gender_code  where witnessname ilike  '%" + witnessname + "%'  order by seizure_witnessdetails_id";
                    if (witnessname != "" && fatherName != "" && mobile == "")
                        query = "select w.*,d.Designation_name,g.gender_name from exciseautomation.seizure_witnessdetails w  left join exciseautomation.designation_master d on d.designation_code = w.designation_code   left join exciseautomation.gender_master g on g.gender_code = w.gender_code  where witnessname ilike  '%" + witnessname + "%' and  relativename ilike '%" + fatherName + "%'  order by seizure_witnessdetails_id";
                    if (witnessname != "" && fatherName != "" && mobile != "")
                        query = " select w.*,d.Designation_name,g.gender_name from exciseautomation.seizure_witnessdetails w  left join exciseautomation.designation_master d on d.designation_code = w.designation_code   left join exciseautomation.gender_master g on g.gender_code = w.gender_code  where witnessname ilike  '%" + witnessname + "%' and relativename ilike '%" + fatherName + "%'  and mobile ilike '%" + mobile + "%' order by seizure_witnessdetails_id";
                    if (witnessname == "" && fatherName != "" && mobile == "")
                        query = "select w.*,d.Designation_name,g.gender_name from exciseautomation.seizure_witnessdetails w  left join exciseautomation.designation_master d on d.designation_code = w.designation_code   left join exciseautomation.gender_master g on g.gender_code = w.gender_code  where relativename ilike  '%" + fatherName + "%'  order by seizure_witnessdetails_id";
                    if (witnessname != "" && fatherName != "" && mobile == "")
                        query = "select w.*,d.Designation_name,g.gender_name from exciseautomation.seizure_witnessdetails w  left join exciseautomation.designation_master d on d.designation_code = w.designation_code   left join exciseautomation.gender_master g on g.gender_code = w.gender_code  where witnessname ilike  '%" + witnessname + "%' and  relativename ilike '%" + fatherName + "%'  order by seizure_witnessdetails_id";
                    if (witnessname == "" && fatherName == "" && mobile != "")
                        query = "select w.*,d.Designation_name,g.gender_name from exciseautomation.seizure_witnessdetails w  left join exciseautomation.designation_master d on d.designation_code = w.designation_code   left join exciseautomation.gender_master g on g.gender_code = w.gender_code  where mobile ilike '%" + mobile + "%'  order by seizure_witnessdetails_id";
                    if (witnessname == "" && fatherName != "" && mobile != "")
                        query = "select w.*,d.Designation_name,g.gender_name from exciseautomation.seizure_witnessdetails w  left join exciseautomation.designation_master d on d.designation_code = w.designation_code   left join exciseautomation.gender_master g on g.gender_code = w.gender_code  where mobile ilike '%" + mobile + "%' and relativename ilike  '%" + fatherName + "%'  order by seizure_witnessdetails_id";
                    if (witnessname != "" && fatherName == "" && mobile != "")
                        query = "select w.*,d.Designation_name,g.gender_name from exciseautomation.seizure_witnessdetails w  left join exciseautomation.designation_master d on d.designation_code = w.designation_code   left join exciseautomation.gender_master g on g.gender_code = w.gender_code  where mobile ilike '%" + mobile + "%' and witnessname ilike  '%" + witnessname + "%'  order by seizure_witnessdetails_id";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        cm_seiz_Witness ad = new cm_seiz_Witness();
                        ad.seizure_witnessdetails_id = (dr["seizure_witnessdetails_id"].ToString());
                        ad.seizureno = dr["seizureno"].ToString();
                        ad.designation_name = dr["Designation_name"].ToString();
                        ad.designation_code = dr["Designation_code"].ToString();
                        ad.gender_code = dr["gender_code"].ToString();

                        ad.witnesstype = dr["witnesstype"].ToString();
                        

                        ad.witnessname = dr["witnessname"].ToString();
                        ad.relativename = dr["relativename"].ToString();
                        ad.witness_age = dr["witness_age"].ToString();
                        ad.presentaddress = dr["presentaddress"].ToString();
                        ad.permanentaddress = dr["permanentaddress"].ToString();

                        ad.permanentaddress = dr["permanentaddress"].ToString();
                        ad.mobile = dr["mobile"].ToString();
                        ad.landline = dr["landline"].ToString();
                        ad.witness_emailid = dr["witness_emailid"].ToString();
                        ad.record_status = dr["record_status"].ToString();
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

        public static cm_seiz_Witness GetDetails(string tableId)
        {
            cm_seiz_Witness record = new cm_seiz_Witness();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(" select w.*, g.gender_name from exciseautomation.seizure_witnessdetails w    left join exciseautomation.gender_master g on g.gender_code = w.gender_code where seizure_witnessdetails_id=" + tableId, cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr1 = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr1);
                        dr1.Close();
                        //if (dr.HasRows)
                        //{
                            record = new cm_seiz_Witness();
                            foreach (DataRow dr in dt.Rows)
                            {
                                record.seizureno = dr["seizureno"].ToString();
                                record.gender_code = dr["gender_code"].ToString();
                               
                                record.witnesstype = dr["witnesstype"].ToString();
                               
                                  
                               
                                record.designation_code = dr["Designation_code"].ToString();
                                // record.gender_code = dr["gender_name"].ToString();

                                record.witnessname = dr["witnessname"].ToString();
                                record.relativename = dr["relativename"].ToString();
                                record.witness_age = dr["witness_age"].ToString();
                                record.presentaddress = dr["presentaddress"].ToString();
                                record.permanentaddress = dr["permanentaddress"].ToString();

                                record.permanentaddress = dr["permanentaddress"].ToString();
                                record.mobile = dr["mobile"].ToString();
                                record.landline = dr["landline"].ToString();
                                record.witness_emailid = dr["witness_emailid"].ToString();
                                record.record_status = dr["record_status"].ToString();
                            record.seizure_witnessdetails_id= dr["seizure_witnessdetails_id"].ToString(); 
                            record.docs = new List<Seizure_Docs>();
                            record.raidby = dr["raidby"].ToString();  

                                try
                                {
                                //doc_id = '" + record.seizure_witnessdetails_id+ "'
                                    using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.Seizure_Docs where seizureno ='" + record.seizureno + "' and doc_id='"+record.seizure_witnessdetails_id+"' and doc_type_code='WDT' and raidby='"+record.raidby+"' order by seizureno", cn))
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
                                                record.docs.Add(doc);
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

                            //while (dr.Read())
                            //{
                            //    record.seizureno = dr["seizureno"].ToString();
                            //    record.gender_code = dr["gender_code"].ToString();
                            //    if (dr["witnesstype"].ToString() == "I")
                            //        record.witnesstype = "IndependentPerson";
                            //    else if (dr["witnesstype"].ToString() == "D")
                            //        record.witnesstype = "DepartmentOfficer";
                            //    else
                            //        record.witnesstype = dr["witnesstype"].ToString();
                            //    record.designation_code = dr["Designation_code"].ToString();
                            //   // record.gender_code = dr["gender_name"].ToString();

                            //    record.witnessname = dr["witnessname"].ToString();
                            //    record.relativename = dr["relativename"].ToString();
                            //    record.witness_age = dr["witness_age"].ToString();
                            //    record.presentaddress = dr["presentaddress"].ToString();
                            //    record.permanentaddress = dr["permanentaddress"].ToString();

                            //    record.permanentaddress = dr["permanentaddress"].ToString();
                            //    record.mobile = dr["mobile"].ToString();
                            //    record.landline = dr["landline"].ToString();
                            //    record.witness_emailid = dr["witness_emailid"].ToString();
                            //    record.record_status = dr["record_status"].ToString();
                               

                               
                            //}
                        //}
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

        public static string Insert(cm_seiz_Witness witness)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {                
                    int maxid = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.seizure_witnessdetails", "seizure_witnessdetails_id").ToString()) + 1;
                    StringBuilder str = new StringBuilder();
                    str.Append("INSERT INTO exciseautomation.seizure_witnessdetails(seizure_witnessdetails_id, seizureno, gender_code, designation_code, witnessname, witnesstype, relativename, witness_age, presentaddress, permanentaddress, mobile, landline, witness_emailid, lastmodified_date, user_id, creation_date, record_status,raidby,ipaddress)Values(");
                    str.Append("'" + maxid + "','" + witness.seizureno + "','" + witness.gender_code + "','" + witness.designation_code + "','" + witness.witnessname + "','" + witness.witnesstype + "','" + witness.relativename + "','" + witness.witness_age + "','" + witness.presentaddress + "','" + witness.permanentaddress + "','" + witness.mobile + "','" + witness.landline + "','" + witness.witness_emailid+ "','" + DateTime.Now.ToShortDateString() + "','" + witness.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "', '"+ witness.record_status + "','"+witness.raidby+"','"+witness.ipaddress+"')");

                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    cmd.ExecuteNonQuery();
                    for (int i = 0; i < witness.docs.Count; i++)
                    {
                        str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.seizure_docs(seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,raidby)");
                        str.Append("Values('" + witness.docs[i].seizureno + "','" + maxid + "','" + witness.docs[i].doc_name + "', '" + witness.docs[i].description + "','" + witness.docs[i].doc_path + "','WDT','" + witness.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+witness.raidby+"')");
                        NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                        cmd3.ExecuteNonQuery();
                    }
                    if (witness.record_status == "Y")
                    {
                        //if (witness.record_status == "Y")
                        //{
                        //    NpgsqlCommand cmd4 = new NpgsqlCommand("UPDATE exciseautomation.seizure_witnessdetails SET record_status='Y' WHERE seizureno='" + witness.seizureno + "' AND raidby='" + witness.raidby + "'", cn);
                        //    cmd4.ExecuteNonQuery();
                        //}
                        string raidyear = DL_Common.GetMaxID("exciseautomation.seizure_basicinfo  where district_code='" + witness.district_code + "' and raidby='" + witness.raidby + "' and seizureno=" + witness.seizureno + "", "raid_date").ToString();
                        //  raidyear = "01-01-2022";
                        DateTime dt2 = DateTime.ParseExact(raidyear.Substring(0, 10).Replace("/", "-"), "dd-MM-yyyy", null);
                        string y = "";
                        if (dt2.Month < 4)
                        {
                            y = dt2.Year.ToString();
                        }
                        else
                        {
                            y = (dt2.Year + 1).ToString();
                        }
                        raidyear =y;
                        NpgsqlCommand cmd11 = new NpgsqlCommand("SELECT case when MAX(a.finalseizureno) is null then '0' else MAX(a.finalseizureno) end FROM exciseautomation.seizure a inner join exciseautomation.seizure_basicinfo b on a.seizureno = b.seizureno WHERE a.district_code='" + witness.district_code + "' and a.raidby='" + witness.raidby + "'  and  b.raid_date between '31-03-"+ (Convert.ToInt32(raidyear)-1)+ "' and '31-03-" + raidyear + "'", cn);
                        int finalseizureno = Convert.ToInt32(cmd11.ExecuteScalar()) + 1;
                        str = new StringBuilder();
                        str.Append("update exciseautomation.seizure SET  finalseizureno ='" + finalseizureno + "', lastmodified_date ='" + witness.lastmodified_date + "', user_id ='" + witness.user_id + "', record_status ='" + witness.record_status + "' where seizureno=" + witness.seizureno + " and raidby='" + witness.raidby + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();

                        str = new StringBuilder();
                        str.Append("update exciseautomation.seizure_basicinfo SET  lastmodified_date ='" + witness.lastmodified_date + "', user_id ='" + witness.user_id + "', record_status ='" + witness.record_status + "' where seizureno=" + witness.seizureno + " and raidby='" + witness.raidby + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();

                        str = new StringBuilder();
                        str.Append("update exciseautomation.seizure_excisable_articles SET  lastmodified_date ='" + witness.lastmodified_date + "', user_id ='" + witness.user_id + "', record_status ='" + witness.record_status + "' where seizureno=" + witness.seizureno + " and raidby='" + witness.raidby + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();

                        str = new StringBuilder();
                        str.Append("update exciseautomation.seizure_vehicledetails SET  lastmodified_date ='" + witness.lastmodified_date + "', user_id ='" + witness.user_id + "', record_status ='" + witness.record_status + "' where seizureno=" + witness.seizureno + " and raidby='" + witness.raidby + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();

                        str = new StringBuilder();
                        str.Append("update exciseautomation.seizure_apparatusdetails SET  lastmodified_date ='" + witness.lastmodified_date + "', user_id ='" + witness.user_id + "', record_status ='" + witness.record_status + "' where seizureno=" + witness.seizureno + " and raidby='" + witness.raidby + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();

                        str = new StringBuilder();
                        str.Append("update exciseautomation.seizure_propertydetails SET  lastmodified_date ='" + witness.lastmodified_date + "', user_id ='" + witness.user_id + "', record_status ='" + witness.record_status + "' where seizureno=" + witness.seizureno + " and raidby='" + witness.raidby + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();

                        str = new StringBuilder();
                        str.Append("update exciseautomation.seizure_moneydetails SET  lastmodified_date ='" + witness.lastmodified_date + "', user_id ='" + witness.user_id + "', record_status ='" + witness.record_status + "' where seizureno=" + witness.seizureno + " and raidby='" + witness.raidby + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();

                        str = new StringBuilder();
                        str.Append("update exciseautomation.seizure_accuseddetails SET  lastmodified_date ='" + witness.lastmodified_date + "', user_id ='" + witness.user_id + "', record_status ='" + witness.record_status + "' where seizureno=" + witness.seizureno + " and raidby='" + witness.raidby + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();

                        str = new StringBuilder();
                        str.Append("update exciseautomation.seizure_accusedoffences SET  lastmodified_date ='" + witness.lastmodified_date + "', user_id ='" + witness.user_id + "', record_status ='" + witness.record_status + "' where seizureno=" + witness.seizureno + " and raidby='" + witness.raidby + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();

                        str = new StringBuilder();
                        str.Append("update exciseautomation.seizure_accusedcasehistory SET  lastmodified_date ='" + witness.lastmodified_date + "', user_id ='" + witness.user_id + "', record_status ='" + witness.record_status + "' where seizureno=" + witness.seizureno + " and raidby='" + witness.raidby + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();

                        str = new StringBuilder();
                        str.Append("update exciseautomation.seizure_raiddetails SET  lastmodified_date ='" + witness.lastmodified_date + "', user_id ='" + witness.user_id + "', record_status ='" + witness.record_status + "' where seizureno=" + witness.seizureno + " and raidby='" + witness.raidby + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();

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

        public static string Update(cm_seiz_Witness witness)
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
                    //str.Append("update exciseautomation.seizure_witnessdetails set  raidteam_type='" + ad.raidteam_type + "', officername='" + ad.officername + "', designation_code='" + ad.designation_code + "', raidteamlead='" + ad.raidteamlead + "',  user_id='" + ad.user_id + "',lastmodified_date ='" + DateTime.Now.ToString("dd-MM-yyyy") + "', record_status='" + ad.record_status + "' where seizure_raiddetails_id='" + ad.seizure_raiddetails_id + "'");
                    str.Append("update exciseautomation.seizure_witnessdetails SET gender_code ='"+witness.gender_code+ "', witnesstype ='" + witness.witnesstype+ "', designation_code ='" + witness.designation_code+ "', witnessname ='" + witness.witnessname+ "', relativename ='" + witness.relativename+ "', witness_age ='" + witness.witness_age+ "', presentaddress ='" + witness.presentaddress+ "', permanentaddress ='" + witness.permanentaddress+ "', mobile ='" + witness.mobile + "', landline ='" + witness.landline + "',  witness_emailid ='" + witness.witness_emailid + "', lastmodified_date ='" + witness.lastmodified_date + "', user_id ='" + witness.user_id+ "', record_status ='" + witness.record_status+ "',raidby='"+witness.raidby+"',ipaddress='"+witness.ipaddress+"' where seizure_witnessdetails_id='" + witness.seizure_witnessdetails_id + "'");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    cmd.ExecuteNonQuery();
                    cmd = new NpgsqlCommand("Delete from exciseautomation.seizure_docs where doc_type_code='WDT' and doc_id='" + witness.seizure_witnessdetails_id + "' and raidby='" + witness.raidby + "'", cn);
                    cmd.ExecuteNonQuery();
                    for (int i = 0; i < witness.docs.Count; i++)
                    {
                        str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.seizure_docs(seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,raidby)");
                        str.Append("Values('" + witness.docs[i].seizureno + "','" + witness.seizure_witnessdetails_id + "','" + witness.docs[i].doc_name + "', '" + witness.docs[i].description + "','" + witness.docs[i].doc_path + "','WDT','" + witness.user_id + "','" + DateTime.Now.ToShortDateString() + "','"+witness.raidby+"')");
                        NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                        cmd3.ExecuteNonQuery();
                    }
                    if (witness.record_status == "Y")
                    {
                        //if (witness.record_status == "Y")
                        //{
                        //    NpgsqlCommand cmd4 = new NpgsqlCommand("UPDATE exciseautomation.seizure_witnessdetails SET record_status='Y' WHERE seizureno='"+witness.seizureno+"' AND raidby='"+witness.raidby+"'", cn);
                        //    cmd4.ExecuteNonQuery();
                        //}
                        string raidyear = DL_Common.GetMaxID("exciseautomation.seizure_basicinfo  where district_code='" + witness.district_code + "' and raidby='" + witness.raidby + "' and seizureno=" + witness.seizureno + "", "raid_date").ToString();
                      //  raidyear = "01-01-2022";
                        DateTime dt2 = DateTime.ParseExact(raidyear.Substring(0,10).Replace("/","-"), "dd-MM-yyyy", null);
                        string y = "";
                        if(dt2.Month<4)
                        {
                            y = dt2.Year.ToString();
                        }
                        else
                        {
                            y = (dt2.Year+1).ToString();
                        }
                        raidyear = y;
                        NpgsqlCommand cmd11 = new NpgsqlCommand("SELECT case when MAX(a.finalseizureno) is null then '0' else MAX(a.finalseizureno) end FROM exciseautomation.seizure a inner join exciseautomation.seizure_basicinfo b on a.seizureno = b.seizureno WHERE a.district_code='" + witness.district_code + "' and a.raidby='" + witness.raidby + "'  and  b.raid_date between '31-03-" + (Convert.ToInt32(raidyear) - 1) + "' and '31-03-" + raidyear + "'", cn);
                        int finalseizureno = Convert.ToInt32(cmd11.ExecuteScalar()) + 1;
                        str = new StringBuilder();
                        str.Append("update exciseautomation.seizure SET  finalseizureno ='" +finalseizureno + "', lastmodified_date ='" + witness.lastmodified_date + "', user_id ='" + witness.user_id + "', record_status ='" + witness.record_status + "' where seizureno=" + witness.seizureno + " and raidby='" + witness.raidby + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();

                        str = new StringBuilder();
                        str.Append("update exciseautomation.seizure_basicinfo SET  lastmodified_date ='" + witness.lastmodified_date + "', user_id ='" + witness.user_id + "', record_status ='" + witness.record_status + "' where seizureno=" + witness.seizureno + " and raidby='" + witness.raidby + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();

                        str = new StringBuilder();
                        str.Append("update exciseautomation.seizure_excisable_articles SET  lastmodified_date ='" + witness.lastmodified_date + "', user_id ='" + witness.user_id + "', record_status ='" + witness.record_status + "' where seizureno=" + witness.seizureno + " and raidby='" + witness.raidby + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();

                        str = new StringBuilder();
                        str.Append("update exciseautomation.seizure_vehicledetails SET  lastmodified_date ='" + witness.lastmodified_date + "', user_id ='" + witness.user_id + "', record_status ='" + witness.record_status + "' where seizureno=" + witness.seizureno + " and raidby='" + witness.raidby + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();

                        str = new StringBuilder();
                        str.Append("update exciseautomation.seizure_apparatusdetails SET  lastmodified_date ='" + witness.lastmodified_date + "', user_id ='" + witness.user_id + "', record_status ='" + witness.record_status + "' where seizureno=" + witness.seizureno + " and raidby='" + witness.raidby + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();

                        str = new StringBuilder();
                        str.Append("update exciseautomation.seizure_propertydetails SET  lastmodified_date ='" + witness.lastmodified_date + "', user_id ='" + witness.user_id + "', record_status ='" + witness.record_status + "' where seizureno=" + witness.seizureno + " and raidby='" + witness.raidby + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();

                        str = new StringBuilder();
                        str.Append("update exciseautomation.seizure_moneydetails SET  lastmodified_date ='" + witness.lastmodified_date + "', user_id ='" + witness.user_id + "', record_status ='" + witness.record_status + "' where seizureno=" + witness.seizureno + " and raidby='" + witness.raidby + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();

                        str = new StringBuilder();
                        str.Append("update exciseautomation.seizure_accuseddetails SET  lastmodified_date ='" + witness.lastmodified_date + "', user_id ='" + witness.user_id + "', record_status ='" + witness.record_status + "' where seizureno=" + witness.seizureno + " and raidby='" + witness.raidby + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();

                        str = new StringBuilder();
                        str.Append("update exciseautomation.seizure_accusedoffences SET  lastmodified_date ='" + witness.lastmodified_date + "', user_id ='" + witness.user_id + "', record_status ='" + witness.record_status + "' where seizureno=" + witness.seizureno + " and raidby='" + witness.raidby + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();

                        str = new StringBuilder();
                        str.Append("update exciseautomation.seizure_accusedcasehistory SET  lastmodified_date ='" + witness.lastmodified_date + "', user_id ='" + witness.user_id + "', record_status ='" + witness.record_status + "' where seizureno=" + witness.seizureno + " and raidby='" + witness.raidby + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();

                        str = new StringBuilder();
                        str.Append("update exciseautomation.seizure_raiddetails SET  lastmodified_date ='" + witness.lastmodified_date + "', user_id ='" + witness.user_id + "', record_status ='" + witness.record_status + "' where seizureno=" + witness.seizureno + " and raidby='" + witness.raidby + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();
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
