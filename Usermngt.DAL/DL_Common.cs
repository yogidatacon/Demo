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
    public class DL_Common
    {
        internal static object GetMaxID(string tableName, string columnName)
        {
            string value = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {                    
                    NpgsqlCommand cmd = new NpgsqlCommand("Select Max(" + columnName + ") orgid from " + tableName + "", cn);
                    value = cmd.ExecuteScalar().ToString();
                    if (value == "")
                    {
                        value = "0";
                    }
                    cn.Close();
                    //_log.Info("Get MaxID Success :" + tableName);
                }
                catch (Exception ex1)
                {
                    //_log.Info("Get MaxID Success :" + tableName);
                    value = "";
                }

            }
            return value;
        }

        public static string GetDate(string tablename, string columnName)
        {
            string value = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("Select " + columnName + " orgid from " + tablename + "", cn);
                    value = cmd.ExecuteScalar().ToString();
                    if (value == "")
                    {
                        value = "0";
                    }
                    cn.Close();
                    //_log.Info("Get MaxID Success :" + tableName);
                }
                catch (Exception ex1)
                {
                    //_log.Info("Get MaxID Success :" + tableName);
                    value = "";
                }

            }
            return value;
        }

     

        public static UserDetails CheckUser(string userid)
        {
            UserDetails user = new UserDetails();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select deptM.department_name, a.*, dm.district_name,divm.division_name,b.party_name,c.party_type_name,d.role_name,b.party_captive_unit_name,b.party_captive,e.financial_year from exciseautomation.user_registration a inner join exciseautomation.party_master b on a.party_code = b.party_code inner join exciseautomation.party_type_master c on b.party_type_code = c.party_type_code inner join exciseautomation.role_master d on a.role_name_code = d.role_name_code left join exciseautomation.party_financial_yr e on e.party_type_code = c.party_type_code left join exciseautomation.district_master dm on a.district_code = dm.district_code left join exciseautomation.division_master divm on a.division_code = divm.division_code left join exciseautomation.department_master deptM on deptM.department_code =a.department_code  where a.user_id='" + userid + "'", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            user = new UserDetails();
                            while (dr.Read())
                            {
                                user.id = Convert.ToInt32(dr["user_registration_id"].ToString());
                                user.user_id = dr["user_id"].ToString();
                                user.user_dob = dr["user_dob"].ToString();
                                user.user_password = dr["user_password"].ToString();
                                user.access_type_code = Convert.ToInt32(dr["access_type_code"].ToString());
                                if (dr["role_level_code"].ToString() != "")
                                    user.role_level_code = Convert.ToInt32(dr["role_level_code"].ToString());
                                user.org_id = Convert.ToInt32(dr["org_id"].ToString());
                                user.role_name_code = Convert.ToInt32(dr["role_name_code"].ToString());
                                user.user_address = dr["user_address"].ToString();
                                user.mobile = dr["mobile"].ToString();
                                user.email_id = dr["email_id"].ToString();
                                user.state_code = dr["State_code"].ToString();
                                user.division_code = dr["Division_code"].ToString();
                                user.district_code = dr["District_Code"].ToString();
                                user.division_name = dr["division_name"].ToString();
                                user.district_name = dr["district_name"].ToString();
                                user.photoname = dr["photoname"].ToString();
                                //user.comments = dr["Comments"].ToString();
                                user.user_name = dr["user_name"].ToString();
                                user.mobile = dr["mobile"].ToString();
                                user.email_id = dr["email_id"].ToString();
                                user.department_name = dr["department_name"].ToString();
                                user.party_code = dr["Party_code"].ToString();
                                user.party_name = dr["Party_name"].ToString();
                                user.party_type = dr["Party_type_name"].ToString();
                                user.role_name = dr["role_name"].ToString();
                                user.party_captive_unit_name = dr["party_captive_unit_name"].ToString();
                                user.financial_year = dr["financial_year"].ToString();
                                user.iscaptive = dr["party_captive"].ToString();
                               
                                //user.module_code = dr["module_code"].ToString();
                                //user.submodule_code = dr["submodule_code"].ToString();
                                //user.tab_name_id = dr["tab_name_id"].ToString();
                                //user.tab_name = dr["tab_name"].ToString();
                                //user.add = dr["add_role_permission"].ToString();
                                //user.edit = dr["edit_role_permission"].ToString();
                                //user.delete = dr["delete_role_permission"].ToString();
                                //user.review = dr["review_role_permission"].ToString();
                                //user.approve = dr["approve_role_permission"].ToString();
                            }
                           // _log.Info("Get User Success :" + user.id + "-" + user.user_name);
                        }
                        dr.Close();
                    }
                }
                catch (Exception ex)
                {
                    //_log.Info("Get User Fail :" + user.id + "-" + user.user_name + "-" + ex.Message);
                }
            }
            return user;
        }

        internal static object UpdateStageCode(int seizureNo, int stagecode)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                    cn.Open();
                try
                {

                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure set seizure_stage_code=" + stagecode + " where seizureno=" + seizureNo + " and  (seizure_stage_code < " + stagecode + " or seizure_stage_code is null)", cn);
                    
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
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

        internal static object UpdateSeizure(int seizureNo, int stagecode, string accusedId, string type)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                    cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand();                    
                    if (stagecode == DL_Common.stage_code_JudgementList)
                    {
                        cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure_accuseddetails set Judgement_status='" + type + "' where seizureno=" + seizureNo + " and seizure_accused_details_id = "+ accusedId, cn);
                    }
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
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

        public static cm_seiz_FIR GetPRFIRNo(string seizureNo)
        {
            string[] dept = seizureNo.Split('&');
            seizureNo = dept[0];
            string d = "";
            if (dept[1] == "Excise" || dept[1] == "E")
                d = "E";
            else
                d = "P";
            cm_seiz_FIR firDetails = new cm_seiz_FIR();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT seizure_fir_id, seizureno, designation_code, raidby, prfirno, prfirdate, manualprfirno, manualbookdate, raidorderby, complaintno, complaintdate, infotocourtdate, finalseizureno, ipaddress, lastmodified_date, user_id, creation_date, record_status, record_deleted FROM exciseautomation.seizure_fir where seizureno ='" + seizureNo + "' and raidby='"+d+"'", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            firDetails = new cm_seiz_FIR();
                            while (dr.Read())
                            {
                                firDetails.prfirno = dr["prfirno"].ToString();
                                firDetails.prfirdate = dr["prfirdate"].ToString().Substring(0, 10).Replace("/", "-");
                            }                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    //_log.Info("Get User Fail :" + user.id + "-" + user.user_name + "-" + ex.Message);
                }
            }
            return firDetails;
        }

        #region Constant 
        public const  int stage_code_FIR = 13;
        public const int stage_code_Chargesheet = 14;
        public const int stage_code_Bail = 15;
        public const int stage_code_Cognizance = 16;
        public const int stage_code_Appearance = 17;
        public const int stage_code_PolicePaperSupply = 18;
        public const int stage_code_FramingCharged = 19;
        public const int stage_code_ProsecutionEvidence = 20;
        public const int stage_code_AccusedStatement = 21;
        public const int stage_code_DefenceStatement = 22;
        public const int stage_code_FinalArgumentList = 23;
        public const int stage_code_JudgementList = 24;
        public const int stage_code_TrailCaseHistoryList = 25;
        public const int stage_code_DMOrderDetails = 26;
        public const int stage_code_ExciseCommissionerOrderDetails = 27;
        public const int stage_code_Appeal = 28;

        #endregion
    }
}
