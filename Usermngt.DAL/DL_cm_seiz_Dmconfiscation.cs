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
    public class DL_cm_seiz_Dmconfiscation
    {
        public static List<cm_seiz_Dmconfiscation> GetList(string seizureNo)
        {
            List<cm_seiz_Dmconfiscation> lstObj = new List<cm_seiz_Dmconfiscation>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select raidby,seizure_dmconfiscation_id, seizureno, prfirno, appl_letterno, appl_letterdate, dmorderno, dmorderdate, dmremarks, confiscation_caseno, magistratename, amountreceived, highauthority_date, highauthority_name, highauthority_remarks, finalseizureno, lastmodified_date, user_id, creation_date, record_status, record_deleted, appealed_to_ha from exciseautomation.seizure_dmconfiscation where seizureNo= " + seizureNo, cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_seiz_Dmconfiscation>();
                            while (dr.Read())
                            {
                                cm_seiz_Dmconfiscation record = new cm_seiz_Dmconfiscation();
                                record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                                record.seizure_dmconfiscation_id = Convert.ToInt32(dr["seizure_dmconfiscation_id"].ToString());
                                record.prfirno = dr["prfirno"].ToString();
                                record.appl_letterno = dr["appl_letterno"].ToString();
                                if(dr["appl_letterdate"].ToString()!="")
                                record.appl_letterdate = Convert.ToDateTime(dr["appl_letterdate"]).ToString("dd-MM-yyyy");
                                record.dmorderno = dr["dmorderno"].ToString();
                                if(dr["dmorderdate"].ToString()!="")
                                record.dmorderdate = Convert.ToDateTime(dr["dmorderdate"]).ToString("dd-MM-yyyy");
                                record.dmremarks = dr["dmremarks"].ToString();

                                record.confiscation_caseno = dr["confiscation_caseno"].ToString();
                                record.magistratename = dr["magistratename"].ToString();
                                record.amountreceived = Convert.ToInt32(dr["amountreceived"].ToString());
                                if(dr["highauthority_date"].ToString()!="")
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

        public static List<cm_court> GetSECCaseList()
        {
            List<cm_court> lstObj = new List<cm_court>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT distinct a1.*,b1.seizure_fir_no,b1.district_code,b1.raidby,d.district_name,b1.confiscation_code,e.prfirno,b.court_master_name,b1.caseno as caseno1  FROM exciseautomation.seccase_registration a1 inner join exciseautomation.excase_registration e1 on a1.excase_registration_id =e1.excase_registration_id inner join exciseautomation.dmcase_registration b1 on e1.dmcase_registration_id = b1.dmcase_registration_id inner join exciseautomation.court_master b on b.court_master_code = b1.court_master_code inner join exciseautomation.district_master d on d.district_code = b1.district_code inner join exciseautomation.seizure_fir e on e.seizureno = b1.seizure_fir_no and e.raidby = b1.raidby order by a1.excase_registration_id desc", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_court>();
                            while (dr.Read())
                            {
                                cm_court record = new cm_court();
                                record.seccase_registration_id = Convert.ToInt32(dr["seccase_registration_id"].ToString());
                                record.seizure_fir_no = Convert.ToInt32(dr["seizure_fir_no"].ToString());
                                record.prfirno = dr["prfirno"].ToString();
                                record.district_code = dr["district_code"].ToString();
                                record.district_name = dr["district_name"].ToString();
                                record.caseno = dr["caseno1"].ToString();
                                record.case_action = dr["case_action"].ToString();
                                record.raidby = dr["raidby"].ToString();
                                record.confiscation_code = dr["confiscation_code"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.hearing_status = dr["hearing_status"].ToString();
                                if (record.hearing_status == "")
                                    record.hearing_status = "N";
                                if (dr["casedate"].ToString() != "")
                                    record.case_registerdate = dr["casedate"].ToString().Substring(0, 10).Replace("/", "-");
                                if (record.record_status == "Y" && record.hearing_status == "")
                                    record.hearing_status = "N";
                                record.court_master_name = dr["court_master_name"].ToString();
                                if (dr["date_of_hearing"].ToString() != "")
                                    record.case_hearingdate = dr["date_of_hearing"].ToString().Substring(0, 10).Replace("/", "-");
                                if (dr["next_hearing_date"].ToString() != "")
                                    record.next_hearingdate = dr["next_hearing_date"].ToString().Substring(0, 10).Replace("/", "-");
                                else
                                    record.next_hearingdate = record.case_hearingdate;
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


        //GetEXCaseList

        public static List<cm_court> GetEXCaseList()
        {
            List<cm_court> lstObj = new List<cm_court>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT distinct a1.*,b1.seizure_fir_no,b1.district_code,b1.raidby,d.district_name,b1.confiscation_code,e.prfirno,b.court_master_name FROM exciseautomation.excase_registration a1 inner join exciseautomation.dmcase_registration b1 on a1.dmcase_registration_id = b1.dmcase_registration_id inner join exciseautomation.court_master b on b.court_master_code = b1.court_master_code inner join exciseautomation.district_master d on d.district_code = b1.district_code inner join exciseautomation.seizure_fir e on e.seizureno = b1.seizure_fir_no and e.raidby = b1.raidby order by a1.excase_registration_id desc", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_court>();
                            while (dr.Read())
                            {
                                cm_court record = new cm_court();
                                record.excase_registration_id = Convert.ToInt32(dr["excase_registration_id"].ToString());
                                record.seizure_fir_no = Convert.ToInt32(dr["seizure_fir_no"].ToString());
                                record.prfirno = dr["prfirno"].ToString();
                                record.district_code = dr["district_code"].ToString();
                                record.district_name = dr["district_name"].ToString();
                                record.caseno = dr["caseno"].ToString();
                                record.case_action = dr["case_action"].ToString();
                                record.raidby = dr["raidby"].ToString();
                                record.confiscation_code = dr["confiscation_code"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.hearing_status = dr["hearing_status"].ToString();
                                if (record.hearing_status == "")
                                    record.hearing_status = "N";
                                if (dr["casedate"].ToString() != "")
                                    record.case_registerdate = dr["casedate"].ToString().Substring(0, 10).Replace("/", "-");
                                if (record.record_status == "Y" && record.hearing_status == "")
                                    record.hearing_status = "N";
                                record.court_master_name = dr["court_master_name"].ToString();
                                if(dr["date_of_hearing"].ToString()!="")
                                record.case_hearingdate = dr["date_of_hearing"].ToString().Substring(0, 10).Replace("/", "-");
                                if (dr["next_hearing_date"].ToString() != "")
                                    record.next_hearingdate = dr["next_hearing_date"].ToString().Substring(0, 10).Replace("/", "-");
                                else
                                    record.next_hearingdate = record.case_hearingdate;
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
        public static List<cm_court> GetDMCaseList()
        {
            List<cm_court> lstObj = new List<cm_court>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select distinct  dm.*,b.court_master_name,d.district_name,e.prfirno,t.thana_name from exciseautomation.dmcase_registration dm  inner join exciseautomation.court_master b on b.court_master_code = dm.court_master_code inner join exciseautomation.district_master d on d.district_code = dm.district_code inner join exciseautomation.seizure_fir e on e.seizureno=dm.seizure_fir_no and e.raidby=dm.raidby inner join exciseautomation.thana_master t on dm.thana_code=t.thana_code order by dm.dmcase_registration_id desc", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_court>();
                            while (dr.Read())
                            {
                                cm_court record = new cm_court();
                                record.dmcase_registration_id = Convert.ToInt32(dr["dmcase_registration_id"].ToString());
                                record.seizure_fir_no = Convert.ToInt32(dr["seizure_fir_no"].ToString());
                                record.prfirno = dr["prfirno"].ToString();
                                record.district_code = dr["district_code"].ToString();
                                record.district_name = dr["district_name"].ToString();
                                record.caseno = dr["caseno"].ToString();
                                record.raidby = dr["raidby"].ToString();
                                record.case_action = dr["case_action"].ToString();
                                record.confiscation_code = dr["confiscation_code"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.hearing_status = dr["hearing_status"].ToString();
                                if(dr["casedate"].ToString()!="")
                                record.case_registerdate = dr["casedate"].ToString().Substring(0,10).Replace("/","-");
                                if (record.hearing_status == "")
                                    record.hearing_status = "N";
                                record.court_master_name = dr["court_master_name"].ToString();
                                record.court_master_code = dr["court_master_code"].ToString();
                                if(dr["date_of_hearing"].ToString()!="")
                                record.case_hearingdate = dr["date_of_hearing"].ToString().Substring(0, 10).Replace("/", "-");
                                if (dr["next_hearing_date"].ToString() != "")
                                    record.next_hearingdate = dr["next_hearing_date"].ToString().Substring(0, 10).Replace("/", "-");
                                else
                                    record.next_hearingdate = record.case_hearingdate;
                                record.thana_name = dr["thana_name"].ToString();
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

        

        public static cm_seiz_Dmconfiscation GetDetails(string tableId)
        {
            cm_seiz_Dmconfiscation record = new cm_seiz_Dmconfiscation();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.seizure_dmconfiscation where seizure_dmconfiscation_id=" + tableId, cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            record = new cm_seiz_Dmconfiscation();
                            while (dr.Read())
                            {
                                record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                                record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                                record.seizure_dmconfiscation_id = Convert.ToInt32(dr["seizure_dmconfiscation_id"].ToString());
                                record.prfirno = dr["prfirno"].ToString();
                                record.appl_letterno = dr["appl_letterno"].ToString();
                                record.appl_letterdate = Convert.ToDateTime(dr["appl_letterdate"]).ToString("dd-MM-yyyy");
                                record.dmorderno = dr["dmorderno"].ToString();
                                record.dmorderdate = Convert.ToDateTime(dr["dmorderdate"]).ToString("dd-MM-yyyy");
                                record.dmremarks = dr["dmremarks"].ToString();

                                record.confiscation_caseno = dr["confiscation_caseno"].ToString();
                                record.magistratename = dr["magistratename"].ToString();
                                record.amountreceived = Convert.ToInt32(dr["amountreceived"].ToString());
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
                    Console.Write(ex);
                    //value = false;
                }
            }
            return record;
        }

        public static bool InsertSeiz_Appeal(cm_seiz_Dmconfiscation obj)
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

                        string tableName = "exciseautomation.seizure_dmconfiscation";
                        int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.seizure_dmconfiscation", "seizure_dmconfiscation_id").ToString()) + 1;
                        string columnNames = "seizure_dmconfiscation_id, seizureno, prfirno, appl_letterno, appl_letterdate, dmorderno, dmorderdate, dmremarks,confiscation_caseno, magistratename, amountreceived, highauthority_date, highauthority_name, highauthority_remarks, finalseizureno, lastmodified_date, user_id, creation_date, record_status, record_deleted, appealed_to_ha,raidby";
                        string input = "";
                        input = max + "','" + obj.seizureno + "','" + obj.prfirno + "','" + obj.appl_letterno + "','" + obj.appl_letterdate + "','" + obj.dmorderno + "','" + obj.dmorderdate + "','" + obj.dmremarks + "','" + obj.confiscation_caseno + "','" + obj.magistratename + "','" + obj.amountreceived + "','" + obj.highauthority_date + "','" + obj.highauthority_name + "','" + obj.highauthority_remarks + "','" + obj.finalseizureno + "','" + obj.lastmodified_date + "','" + obj.user_id + "','" + obj.creation_date + "','" + obj.record_status + "','" + obj.record_deleted + "','" + obj.appealed_to_ha+"','"+obj.raidby ;
                        //input = max + "','" + obj.seizureno + "','" + obj.apparatus_type_code + "','" + obj.manufacturer_code + "','" + obj.apparatus_name + "','" + obj.makemodel + "','" + obj.ownername + "','" + obj.presentaddress + "','" + obj.permanentaddress + "','" + obj.contactno + "','" + DateTime.Now.ToShortDateString() + "','" + obj.user_id + "','" + DateTime.Now.ToShortDateString() + "','" + obj.record_status + "','" + obj.record_deleted;

                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {

                            for (int i = 0; i < obj.articals.Count; i++)
                            {
                                if (obj.articals[i].date_of_destruction == "")
                                {
                                    cmd = new NpgsqlCommand("update exciseautomation.seizure_excisable_articles set actioncompleted='" + obj.articals[i].actioncompleted + "' where seizure_excisable_articles_id ='" + obj.articals[i].seizure_excisable_articles_id + "' and seizureno='" + obj.seizureno + "'", cn);
                                    cmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    cmd = new NpgsqlCommand("update exciseautomation.seizure_excisable_articles set date_of_destruction='" + obj.articals[i].date_of_destruction + "',actioncompleted='" + obj.articals[i].actioncompleted + "' where seizure_excisable_articles_id ='" + obj.articals[i].seizure_excisable_articles_id + "' and seizureno='" + obj.seizureno + "'", cn);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            for (int i = 0; i < obj.vehicals.Count; i++)
                            {
                                if (obj.vehicals[i].auctionreleaseamount == "")
                                    obj.vehicals[i].auctionreleaseamount = "0";
                                if (obj.vehicals[i].auction_or_releasedate == "" && obj.vehicals[i].challan_date == "")
                                {
                                    cmd = new NpgsqlCommand("update exciseautomation.seizure_vehicledetails set actioncompleted='" + obj.vehicals[i].actioncompleted + "',challan_no='" + obj.vehicals[i].challan_no + "',auctionreleaseamount='" + obj.vehicals[i].auctionreleaseamount + "',infavourof='" + obj.vehicals[i].infavourof + "' where seizure_vehicledetails_id =" + obj.vehicals[i].seizure_vehicledetails_id + " and seizureno='" + obj.seizureno + "'", cn);
                                    cmd.ExecuteNonQuery();
                                }
                                else if (obj.vehicals[i].auction_or_releasedate == "" && obj.vehicals[i].challan_date != "")
                                {
                                    cmd = new NpgsqlCommand("update exciseautomation.seizure_vehicledetails set actioncompleted='" + obj.vehicals[i].actioncompleted + "',challan_no='" + obj.vehicals[i].challan_no + "',challan_date='" + obj.vehicals[i].challan_date + "',auctionreleaseamount='" + obj.vehicals[i].auctionreleaseamount + "',infavourof='" + obj.vehicals[i].infavourof + "' where seizure_vehicledetails_id ='" + obj.vehicals[i].seizure_vehicledetails_id + "' and seizureno='" + obj.seizureno + "'", cn);
                                    cmd.ExecuteNonQuery();
                                }
                                else if (obj.vehicals[i].challan_date == "" && obj.vehicals[i].auction_or_releasedate != "")
                                {
                                    cmd = new NpgsqlCommand("update exciseautomation.seizure_vehicledetails set auction_or_releasedate='" + obj.vehicals[i].auction_or_releasedate + "',actioncompleted='" + obj.vehicals[i].actioncompleted + "',challan_no='" + obj.vehicals[i].challan_no + "',auctionreleaseamount='" + obj.vehicals[i].auctionreleaseamount + "',infavourof='" + obj.vehicals[i].infavourof + "' where seizure_vehicledetails_id ='" + obj.vehicals[i].seizure_vehicledetails_id + "' and seizureno='" + obj.seizureno + "'", cn);
                                    cmd.ExecuteNonQuery();
                                }

                                else
                                {
                                    cmd = new NpgsqlCommand("update exciseautomation.seizure_vehicledetails set auction_or_releasedate='" + obj.vehicals[i].auction_or_releasedate + "',actioncompleted='" + obj.vehicals[i].actioncompleted + "',challan_no='" + obj.vehicals[i].challan_no + "',challan_date='" + obj.vehicals[i].challan_date + "',auctionreleaseamount='" + obj.vehicals[i].auctionreleaseamount + "',infavourof='" + obj.vehicals[i].infavourof + "' where seizure_vehicledetails_id ='" + obj.vehicals[i].seizure_vehicledetails_id + "' and seizureno='" + obj.seizureno + "'", cn);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            for (int i = 0; i < obj.Apparatus.Count; i++)
                            {
                                if (obj.Apparatus[i].date_of_destruction == "")
                                {
                                    cmd = new NpgsqlCommand("update exciseautomation.seizure_apparatusdetails set actioncompleted='" + obj.Apparatus[i].actioncompleted + "' where seizure_apparatusdetails_id ='" + obj.Apparatus[i].seizure_apparatusdetails_id + "' and seizureno='" + obj.seizureno + "'", cn);
                                    cmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    cmd = new NpgsqlCommand("update exciseautomation.seizure_apparatusdetails set date_of_destruction='" + obj.articals[i].date_of_destruction + "',actioncompleted='" + obj.Apparatus[i].actioncompleted + "' where seizure_apparatusdetails_id ='" + obj.Apparatus[i].seizure_apparatusdetails_id + "' and seizureno='" + obj.seizureno + "'", cn);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            for (int i = 0; i < obj.Property.Count; i++)
                            {
                                if (obj.Property[i].date_of_destruction == "")
                                {
                                    cmd = new NpgsqlCommand("update exciseautomation.seizure_propertydetails set actioncompleted='" + obj.Property[i].actioncompleted + "' where seizure_propertydetails_id ='" + obj.Property[i].seizure_propertydetails_id + "' and seizureno='" + obj.seizureno + "'", cn);
                                    cmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    cmd = new NpgsqlCommand("update exciseautomation.seizure_propertydetails set date_of_destruction='" + obj.Property[i].date_of_destruction + "',actioncompleted='" + obj.Property[i].actioncompleted + "' where seizure_propertydetails_id ='" + obj.Property[i].seizure_propertydetails_id + "' and seizureno='" + obj.seizureno + "'", cn);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            for (int i = 0; i < obj.Money.Count; i++)
                            {
                                if (obj.Money[i].date_of_destruction == "")
                                {
                                    cmd = new NpgsqlCommand("update exciseautomation.seizure_moneydetails set actioncompleted='" + obj.Money[i].actioncompleted + "' where seizure_moneydetails_id ='" + obj.Money[i].seizure_moneydetails_id + "' and seizureno='" + obj.seizureno + "'", cn);
                                    cmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    cmd = new NpgsqlCommand("update exciseautomation.seizure_moneydetails set date_of_destruction='" + obj.Money[i].date_of_destruction + "',actioncompleted='" + obj.Money[i].actioncompleted + "' where seizure_moneydetails_id ='" + obj.Money[i].seizure_moneydetails_id + "' and seizureno='" + obj.seizureno + "'", cn);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            DL_Common.UpdateStageCode(obj.seizureno, DL_Common.stage_code_DMOrderDetails);
                            trn.Commit();
                            cn.Close();
                            value = true;
                        }
                        else
                        {
                            trn.Rollback();
                            cn.Close();
                            value = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        trn.Commit();
                        cn.Close();
                        Console.Write(ex);
                        value = false;
                    }
                }
            }
            return value;
        }

        public static bool Update_Appeal(cm_seiz_Dmconfiscation cm_obj)
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
                    cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure_dmconfiscation SET  appl_letterno = '" + cm_obj.appl_letterno + "', appl_letterdate = '" + cm_obj.appl_letterdate + "', dmorderno = '" + cm_obj.dmorderno + "', dmorderdate = '" + cm_obj.dmorderdate + "', dmremarks = '" + cm_obj.dmremarks + "', confiscation_caseno = '" + cm_obj.confiscation_caseno + "', magistratename = '" + cm_obj.magistratename + "', amountreceived = '" + cm_obj.amountreceived + "', highauthority_date = '" + cm_obj.highauthority_date + "', highauthority_name = '" + cm_obj.highauthority_name + "', highauthority_remarks = '" + cm_obj.highauthority_remarks + "',  lastmodified_date = '" + DateTime.Now.ToShortDateString() + "', user_id = '" + cm_obj.user_id + "', record_status = '" + cm_obj.record_status + "' WHERE seizure_dmconfiscation_id ='" + cm_obj.seizure_dmconfiscation_id + "'", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        for (int i = 0; i < cm_obj.articals.Count; i++)
                        {
                            if (cm_obj.articals[i].date_of_destruction == "")
                            {
                                cmd = new NpgsqlCommand("update exciseautomation.seizure_excisable_articles set actioncompleted='" + cm_obj.articals[i].actioncompleted + "' where seizure_excisable_articles_id ='" + cm_obj.articals[i].seizure_excisable_articles_id + "' and seizureno='" + cm_obj.seizureno + "'", cn);
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                cmd = new NpgsqlCommand("update exciseautomation.seizure_excisable_articles set date_of_destruction='" + cm_obj.articals[i].date_of_destruction + "',actioncompleted='" + cm_obj.articals[i].actioncompleted + "' where seizure_excisable_articles_id ='" + cm_obj.articals[i].seizure_excisable_articles_id + "' and seizureno='" + cm_obj.seizureno + "'", cn);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        for (int i = 0; i < cm_obj.vehicals.Count; i++)
                        {
                            if (cm_obj.vehicals[i].auctionreleaseamount == "")
                                cm_obj.vehicals[i].auctionreleaseamount = "0";
                            if (cm_obj.vehicals[i].auction_or_releasedate == "" && cm_obj.vehicals[i].challan_date == "")
                            {
                                cmd = new NpgsqlCommand("update exciseautomation.seizure_vehicledetails set actioncompleted='" + cm_obj.vehicals[i].actioncompleted + "',challan_no='" + cm_obj.vehicals[i].challan_no + "',auctionreleaseamount='" + cm_obj.vehicals[i].auctionreleaseamount + "',infavourof='" + cm_obj.vehicals[i].infavourof + "' where seizure_vehicledetails_id =" + cm_obj.vehicals[i].seizure_vehicledetails_id + " and seizureno='" + cm_obj.seizureno + "'", cn);
                                cmd.ExecuteNonQuery();
                            }
                            else if (cm_obj.vehicals[i].auction_or_releasedate == "" && cm_obj.vehicals[i].challan_date != "")
                            {
                                cmd = new NpgsqlCommand("update exciseautomation.seizure_vehicledetails set actioncompleted='" + cm_obj.vehicals[i].actioncompleted + "',challan_no='" + cm_obj.vehicals[i].challan_no + "',challan_date='" + cm_obj.vehicals[i].challan_date + "',auctionreleaseamount='" + cm_obj.vehicals[i].auctionreleaseamount + "',infavourof='" + cm_obj.vehicals[i].infavourof + "' where seizure_vehicledetails_id ='" + cm_obj.vehicals[i].seizure_vehicledetails_id + "' and seizureno='" + cm_obj.seizureno + "'", cn);
                                cmd.ExecuteNonQuery();
                            }
                            else if (cm_obj.vehicals[i].challan_date == "" && cm_obj.vehicals[i].auction_or_releasedate != "")
                            {
                                cmd = new NpgsqlCommand("update exciseautomation.seizure_vehicledetails set auction_or_releasedate='" + cm_obj.vehicals[i].auction_or_releasedate + "',actioncompleted='" + cm_obj.vehicals[i].actioncompleted + "',challan_no='" + cm_obj.vehicals[i].challan_no + "',auctionreleaseamount='" + cm_obj.vehicals[i].auctionreleaseamount + "',infavourof='" + cm_obj.vehicals[i].infavourof + "' where seizure_vehicledetails_id ='" + cm_obj.vehicals[i].seizure_vehicledetails_id + "' and seizureno='" + cm_obj.seizureno + "'", cn);
                                cmd.ExecuteNonQuery();
                            }

                            else
                            {
                                cmd = new NpgsqlCommand("update exciseautomation.seizure_vehicledetails set auction_or_releasedate='" + cm_obj.vehicals[i].auction_or_releasedate + "',actioncompleted='" + cm_obj.vehicals[i].actioncompleted + "',challan_no='" + cm_obj.vehicals[i].challan_no + "',challan_date='" + cm_obj.vehicals[i].challan_date + "',auctionreleaseamount='" + cm_obj.vehicals[i].auctionreleaseamount + "',infavourof='" + cm_obj.vehicals[i].infavourof + "' where seizure_vehicledetails_id ='" + cm_obj.vehicals[i].seizure_vehicledetails_id + "' and seizureno='" + cm_obj.seizureno + "'", cn);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        for (int i = 0; i < cm_obj.Apparatus.Count; i++)
                        {
                            if (cm_obj.Apparatus[i].date_of_destruction == "")
                            {
                                cmd = new NpgsqlCommand("update exciseautomation.seizure_apparatusdetails set actioncompleted='" + cm_obj.Apparatus[i].actioncompleted + "' where seizure_apparatusdetails_id ='" + cm_obj.Apparatus[i].seizure_apparatusdetails_id + "' and seizureno='" + cm_obj.seizureno + "'", cn);
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                cmd = new NpgsqlCommand("update exciseautomation.seizure_apparatusdetails set date_of_destruction='" + cm_obj.articals[i].date_of_destruction + "',actioncompleted='" + cm_obj.Apparatus[i].actioncompleted + "' where seizure_apparatusdetails_id ='" + cm_obj.Apparatus[i].seizure_apparatusdetails_id + "' and seizureno='" + cm_obj.seizureno + "'", cn);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        for (int i = 0; i < cm_obj.Property.Count; i++)
                        {
                            if (cm_obj.Property[i].date_of_destruction == "")
                            {
                                cmd = new NpgsqlCommand("update exciseautomation.seizure_propertydetails set actioncompleted='" + cm_obj.Property[i].actioncompleted + "' where seizure_propertydetails_id ='" + cm_obj.Property[i].seizure_propertydetails_id + "' and seizureno='" + cm_obj.seizureno + "'", cn);
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                cmd = new NpgsqlCommand("update exciseautomation.seizure_propertydetails set date_of_destruction='" + cm_obj.Property[i].date_of_destruction + "',actioncompleted='" + cm_obj.Property[i].actioncompleted + "' where seizure_propertydetails_id ='" + cm_obj.Property[i].seizure_propertydetails_id + "' and seizureno='" + cm_obj.seizureno + "'", cn);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        for (int i = 0; i < cm_obj.Money.Count; i++)
                        {
                            if (cm_obj.Money[i].date_of_destruction == "")
                            {
                                cmd = new NpgsqlCommand("update exciseautomation.seizure_moneydetails set actioncompleted='" + cm_obj.Money[i].actioncompleted + "' where seizure_moneydetails_id ='" + cm_obj.Money[i].seizure_moneydetails_id + "' and seizureno='" + cm_obj.seizureno + "'", cn);
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                cmd = new NpgsqlCommand("update exciseautomation.seizure_moneydetails set date_of_destruction='" + cm_obj.Money[i].date_of_destruction + "',actioncompleted='" + cm_obj.Money[i].actioncompleted + "' where seizure_moneydetails_id ='" + cm_obj.Money[i].seizure_moneydetails_id + "' and seizureno='" + cm_obj.seizureno + "'", cn);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        trn.Commit();
                        cn.Close();
                        value = true;
                    }
                    else
                    {
                        value = false;
                        trn.Rollback();
                        cn.Close();
                    }
                }
                catch (Exception ex)
                {
                    trn.Rollback();
                    cn.Close();
                    value = false;
                    throw (ex);
                }
            }
            return value;
        }
    }
}
