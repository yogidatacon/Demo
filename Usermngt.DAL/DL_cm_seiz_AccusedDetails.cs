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
    public class DL_cm_seiz_AccusedDetails
    {
        public static string Insert(cm_seiz_AccusedDetails ad)
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
                    
                    NpgsqlCommand cmd1 = new NpgsqlCommand("select case when max(seizure_accused_details_id) is null then 0 else max(seizure_accused_details_id) end as seizure_accused_details_id from exciseautomation.seizure_accuseddetails", cn);
                    int maxid = Convert.ToInt32(cmd1.ExecuteScalar())+1 ;
                    StringBuilder str = new StringBuilder();
                    if (ad.accused_age == "")
                        ad.accused_age ="0";
                    if (ad.offence_code == "Select")
                        ad.offence_code = "0";
                    if (ad.mobileno == "")
                        ad.mobileno = "0";
                    str.Append("INSERT INTO exciseautomation.seizure_accuseddetails(seizure_accused_details_id, seizureno, gender_code, religion_code,category_code, caste_code, idproof_code, accusedstatus_code, accusedname, relativename, accused_age,offence_code,mobileno,");
                    str.Append("identificaton_mark, occupation, presentaddress, permanentaddress, breathtest, breathtest_result, bloodtest, bloodtest_result, idno, user_id, creation_date, record_status,raidby,ipaddress,SDR_CAF,alternate_contact,state_code,district_code,thana_code,district_name,thana_name)Values(");
                    str.Append("'"+maxid+"','"+ad.seizureno+"','"+ad.gender_code+"','"+ad.religion_code+"','"+ad.category_code+"','"+ad.caste_code+"','"+ad.idproof_code+"','"+ad.accusedstatus_code+"','"+ad.accusedname+"','"+ad.relativename+"','"+ad.accused_age+"','"+ad.offence_code+"','"+ad.mobileno+"',");
                    str.Append("'" + ad.identificaton_mark + "','" + ad.occupation + "','" + ad.presentaddress + "','" + ad.permanentaddress + "','" + ad.breathtest + "','" + ad.breathtest_result + "','" + ad.bloodtest + "','" + ad.bloodtest_result + "','" + ad.idno + "','" + ad.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + ad.record_status + "','"+ad.raidby+"','"+ad.ipaddress+"','"+ad.SDR_CAF+"','"+ad.mobileno1+"','"+ad.state_code+"','"+ad.district_code+"','"+ad.thana_code+"','"+ad.district_code1+"','"+ad.thana_code1+"')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
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

        public static DataTable GetSearchDetailsID(string id)
        {
            DataTable dt1 = new DataTable();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.seizure_accuseddetails where seizure_accused_details_id=" + id + " ", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        cm_seiz_AccusedDetails ad = new cm_seiz_AccusedDetails();
                       // ad.seizure_accused_details_id = Convert.ToInt32(dr["seizure_accused_details_id"].ToString());
                        ad.accusedname = dr["accusedname"].ToString();
                        ad.idno = dr["idno"].ToString();
                        ad.mobileno = dr["mobileno"]?.ToString() ?? string.Empty;
                        ad.relativename = dr["relativename"].ToString();
                        string strcon = "b.accusedname='"+ad.accusedname+"'";
                        int n1 =0;
                        if(ad.idno.Trim()!="")
                        {
                            strcon = strcon + " and b.idno='" + ad.idno.Trim() + "'";
                            n1++;
                        }
                        if (ad.mobileno.Trim() != "" && ad.mobileno.Trim() != "0")
                        {
                            strcon = strcon + " and b.mobileno='" + ad.mobileno.Trim() + "'";
                            n1++;
                        }
                        if (ad.relativename.Trim() != "" )
                        {
                            strcon = strcon + " and b.relativename='" + ad.relativename.Trim() + "'";
                            n1++;
                        }
                        if(n1>0)
                        {
                            StringBuilder str = new StringBuilder();
                            str.Append("select a.seizureno,d.district_name,c.prfirno,c.prfirdate,f.thana_code,f.thana_name,h.offence_section_name  from exciseautomation.seizure a ");
                            str.Append("inner join exciseautomation.seizure_accuseddetails b on a.seizureno =b.seizureno and a.raidby =b.raidby ");
                            str.Append("left join exciseautomation.seizure_fir c on a.seizureno =c.seizureno and a.raidby =c.raidby ");
                            str.Append("inner join exciseautomation.district_master d on a.district_code =d.district_code ");
                            str.Append("inner join exciseautomation.seizure_basicinfo e on a.seizureno =e.seizureno and a.raidby =e.raidby ");
                            str.Append("left join exciseautomation.seizure_accusedoffences g on a.seizureno =g.seizureno and a.raidby =g.raidby ");
                            str.Append("left join exciseautomation.offence_section_master h on g.offence_section_code =h.offence_section_code ");
                            str.Append("inner join exciseautomation.thana_master f on a.district_code =f.district_code and e.thana_code =f.thana_code WHERE b.seizure_accused_details_id<>" + id + " and " + strcon);
                            NpgsqlCommand cmd1 = new NpgsqlCommand(str.ToString(), cn);
                            NpgsqlDataReader dr12 = cmd1.ExecuteReader();
                            dt1 = new DataTable();
                            dt1.Load(dr12);
                            dr1.Close();
                        }
                        
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
            return dt1;
        }

        public static List<cm_seiz_AccusedDetails> GetSearchDetails(string qery)
        {
            List<cm_seiz_AccusedDetails> ads = new List<cm_seiz_AccusedDetails>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(" select a.*,g.idproof_name,f.accusedstatus_name,d.gender_name,b.religion_name from exciseautomation.seizure_accuseddetails a inner join exciseautomation.religion_master b on a.religion_code = b.religion_code inner join exciseautomation.gender_master d on d.gender_code = a.gender_code  inner join exciseautomation.accusedstatus_master f on a.accusedstatus_code = f.accusedstatus_code inner join exciseautomation.idproof_master g on g.idproof_code = a.idproof_code where "+qery+" order by seizure_accused_details_id", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        cm_seiz_AccusedDetails ad = new cm_seiz_AccusedDetails();
                        ad.seizure_accused_details_id = Convert.ToInt32(dr["seizure_accused_details_id"].ToString());
                        ad.accusedname = dr["accusedname"].ToString();
                        ad.offence_code = dr["offence_code"].ToString();
                        ad.accusedstatus_code = dr["accusedstatus_code"].ToString();
                        ad.accused_age = dr["accused_age"].ToString();
                        ad.gender_code = dr["gender_code"].ToString();
                        ad.relativename = dr["relativename"].ToString();
                        ad.religion_code = dr["religion_code"].ToString();
                        ad.caste_code = dr["caste_code"].ToString();
                        ad.idproof_code = dr["idproof_code"].ToString();
                        ad.gender_name = dr["gender_name"].ToString();
                        ad.category_code = dr["category_code"].ToString();
                        ad.religion_name = dr["religion_name"].ToString();                        
                        ad.idproof_name = dr["idproof_name"].ToString();
                        ad.idno = dr["idno"].ToString();
                        ad.mobileno1 = dr["alternate_contact"].ToString();
                        ad.identificaton_mark = dr["identificaton_mark"].ToString();
                        ad.occupation = dr["occupation"].ToString();
                        ad.mobileno = dr["mobileno"]?.ToString() ?? string.Empty;
                        ad.permanentaddress = dr["permanentaddress"].ToString();
                        ad.presentaddress = dr["presentaddress"].ToString();
                        ad.breathtest = dr["breathtest"].ToString();
                        ad.breathtest_result = dr["breathtest_result"].ToString();
                        ad.bloodtest = dr["bloodtest"].ToString();
                        ad.bloodtest_result = dr["bloodtest_result"].ToString();
                        ad.record_status = dr["record_status"].ToString();
                        ad.seizureno = Convert.ToInt32(dr["seizureno"]);
                        ad.SDR_CAF = dr["SDR_CAF"].ToString();
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

        public static List<cm_seiz_AccusedDetails> GetDetails(string adid)
        {
            List<cm_seiz_AccusedDetails> ads = new List<cm_seiz_AccusedDetails>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    string query = "";
                    if (string.IsNullOrEmpty(adid))

                        query = " select a.*,g.idproof_name,f.accusedstatus_name,caste_code,d.gender_name,b.religion_name from exciseautomation.seizure_accuseddetails a left join exciseautomation.religion_master b on a.religion_code = b.religion_code left join exciseautomation.gender_master d on d.gender_code = a.gender_code  inner join exciseautomation.accusedstatus_master f on a.accusedstatus_code = f.accusedstatus_code left join exciseautomation.idproof_master g on g.idproof_code = a.idproof_code order by seizure_accused_details_id";

                    else
                    {
                        string[] dept = adid.Split('&');
                        adid = dept[0];
                        string d = "";
                        if (dept[1] == "Excise" || dept[1] == "E")
                            d = "E";
                        else
                            d = "P";
                        query = " select a.*,g.idproof_name,f.accusedstatus_name,caste_code,d.gender_name,b.religion_name from exciseautomation.seizure_accuseddetails a left join exciseautomation.religion_master b on a.religion_code = b.religion_code left join exciseautomation.gender_master d on d.gender_code = a.gender_code  inner join exciseautomation.accusedstatus_master f on a.accusedstatus_code = f.accusedstatus_code left join exciseautomation.idproof_master g on g.idproof_code = a.idproof_code WHERE a.seizureno=" + adid + " and a.raidby='"+d+"' order by seizure_accused_details_id";
                    }
                    NpgsqlCommand cmd = new NpgsqlCommand(query, cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        cm_seiz_AccusedDetails ad = new cm_seiz_AccusedDetails();
                        ad.seizure_accused_details_id =Convert.ToInt32( dr["seizure_accused_details_id"].ToString());
                        ad.accusedname = dr["accusedname"].ToString();
                        ad.offence_code = dr["offence_code"].ToString();
                        ad.accusedstatus_code = dr["accusedstatus_code"].ToString();
                        ad.accusedstatus_name = dr["accusedstatus_name"].ToString();
                        ad.accused_age = dr["accused_age"].ToString();
                        ad.gender_code = dr["gender_code"].ToString();
                        ad.relativename = dr["relativename"].ToString();
                        ad.religion_code = dr["religion_code"].ToString();
                        ad.caste_code = dr["caste_code"].ToString();
                        ad.category_code = dr["category_code"].ToString();
                        ad.caste_details = dr["caste_details"].ToString();
                        ad.idproof_code = dr["idproof_code"].ToString();
                        ad.gender_name = dr["gender_name"].ToString();
                        ad.mobileno = dr["mobileno"].ToString();
                        ad.religion_name = dr["religion_name"].ToString();
                        ad.idproof_name = dr["idproof_name"].ToString();
                        ad.idno = dr["idno"].ToString();
                        ad.mobileno1 = dr["alternate_contact"].ToString();
                        ad.identificaton_mark = dr["identificaton_mark"].ToString();
                        ad.occupation = dr["occupation"].ToString();
                        ad.state_code = dr["state_code"].ToString();
                        ad.district_code = dr["district_code"].ToString();
                        ad.thana_code = dr["thana_code"].ToString();
                        ad.district_code1 = dr["district_name"].ToString();
                        ad.thana_code1 = dr["thana_name"].ToString();
                        // ad.mobileno = dr["mobileno"].ToString();
                        ad.permanentaddress = dr["permanentaddress"].ToString();
                        ad.presentaddress = dr["presentaddress"].ToString();
                        ad.breathtest = dr["breathtest"].ToString();
                        ad.breathtest_result = dr["breathtest_result"].ToString();
                        ad.bloodtest = dr["bloodtest"].ToString();
                        ad.bloodtest_result = dr["bloodtest_result"].ToString();
                        ad.record_status = dr["record_status"].ToString();
                        ad.seizureno = Convert.ToInt32(dr["seizureno"]);
                        ad.user_id = dr["user_id"].ToString();
                        ad.raidby = dr["raidby"].ToString();
                        ad.SDR_CAF = dr["SDR_CAF"].ToString();
                        ad.fir_status = dr["fir_status"].ToString();
                        ad.bail_status = dr["bail_status"].ToString();
                        //===================================
                        if (dr["fir_status"].ToString()!="")
                        ad.fir_status ="Y";
                        else
                            ad.fir_status = "N";
                       
                        string firid = dr["seizure_fir_id"]?.ToString()?? string.Empty;
                        if (firid != string.Empty)
                            ad.seizure_fir_id = Convert.ToInt32(dr["seizure_fir_id"]);
                        else
                            ad.seizure_fir_id = 0;
                        //====================================
                        if (dr["chargesheet_status"].ToString() != "")
                            ad.chargesheet_status = "Y";
                        else
                            ad.chargesheet_status = "N";
                       
                        string seizure_chargesheet_id = dr["seizure_chargesheet_id"]?.ToString() ?? string.Empty;
                        if (seizure_chargesheet_id != string.Empty)
                            ad.seizure_chargesheet_id = Convert.ToInt32(dr["seizure_chargesheet_id"]);
                        else
                            ad.seizure_chargesheet_id = 0;
                        //=====================================
                        //====================================
                        if (dr["bail_status"].ToString() != "")
                            ad.bail_status = "Y";
                        else
                            ad.bail_status = "N";

                       
                        //=====================================
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

        public static string Update(cm_seiz_AccusedDetails ad)
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
                    str.Append("Update exciseautomation.seizure_accuseddetails set category_code='" + ad.category_code+ "',caste_details='" + ad.caste_details+"', gender_code='" + ad.gender_code + "', religion_code='" + ad.religion_code + "', caste_code='" + ad.caste_code + "', idproof_code='" + ad.idproof_code + "', accusedstatus_code='" + ad.accusedstatus_code + "', accusedname='"+ad.accusedname+ "', relativename='" + ad.relativename + "', accused_age='" + ad.accused_age + "',");
                    str.Append("identificaton_mark='" + ad.identificaton_mark + "', occupation='" + ad.occupation + "', presentaddress='" + ad.presentaddress + "', permanentaddress='" + ad.permanentaddress + "', breathtest='" + ad.breathtest + "', breathtest_result='" + ad.breathtest_result + "', bloodtest='" + ad.bloodtest + "', bloodtest_result='" + ad.bloodtest_result + "', idno='" + ad.idno + "', user_id='" + ad.user_id + "',  record_status='"+ad.record_status+ "',lastmodified_date='"+DateTime.Now.ToString("dd-MM-yyyy")+ "',offence_code='"+ad.offence_code+"',mobileno='"+ad.mobileno+"',raidby='"+ad.raidby+"',ipaddress='"+ad.ipaddress+ "',SDR_CAF='" + ad.SDR_CAF+ "',alternate_contact='"+ad.mobileno1+"',state_code='"+ad.state_code+ "',district_code='"+ad.district_code+"',thana_code='"+ad.thana_code+ "',district_name='" + ad.district_code1 + "',thana_name='" + ad.thana_code1 + "' where seizure_accused_details_id='" + ad.seizure_accused_details_id+"'");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
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
    }
}
