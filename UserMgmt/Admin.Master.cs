using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
       {
            if (!IsPostBack)
            {
                if (System.Web.HttpContext.Current.Session["UserID"] == null)
                {
                    Response.Redirect("~/LoginPage");
                }
                else
                {
                    //string userid = Session["UserID"].ToString();
                    //Session["UserID"] = userid;

                    lblUser.Text = Session["Username"].ToString();
                    string userid = Session["UserID"].ToString();
                    Session["UserID"] = userid;
                    List<SystemSetting> reports = new List<SystemSetting>();
                    reports = BL_SystemSetting.GetList(Session["Username"].ToString());
                    var contentnames = from s in reports
                                       where s.parameter_name == "MaxListSize" || s.parameter_name == "Gridview"
                                       select new { reports = s.parameter_value_num };
                    // string val = Convert.ToDateTime(txtfrom.Value).ToString("yyyy-MM-dd");
                    Session["pagesize"] = contentnames.ToList()[0].reports.ToString();

                    //Session["pagesize"] = 20;
                    List<UserDetails> user = new List<UserDetails>();
                    user = BL_UserDetails.GetUserPermissins(Session["UserID"].ToString());
                    if (userid.Contains("police_") || userid.Contains("Police"))
                        Session["RaidBy"] = "P";
                    else
                        Session["RaidBy"] = "E";
                    Session["devision_code"] = user[0].division_code;
                    Session["district_code"] = user[0].district_code;
                    //UserDetails user = new UserDetails();
                    //user = BL_UserDetails.CheckUser(userid); 
                    int k = 0;
                    Session["role_name_code"] = user[k].role_name_code;
                    if (Session["UserID"].ToString().Contains("excise_") || Session["UserID"].ToString().Contains("police_") || Session["UserID"].ToString() == "admin" || Session["role_name_code"].ToString() == "42" || Session["role_name_code"].ToString() == "10")
                    {
                        CM.Visible = true;
                        //Session["rtype"] = 0;
                    }
                    if (Session["UserID"].ToString() != "Admin")
                    {
                        HDM.Visible = false;
                        for (int i = 0; i < user.Count; i++)
                        {


                            if (user[i].module_code == "REP")
                            {
                                Control module_code = this.Page.Master.FindControl(user[i].module_code.ToString());
                                //  HtmlGenericControl li = (HtmlGenericControl)FindControl();
                                module_code.Visible = true;
                            }
                            else
                            {
                                if (user[i].role_name_code != 42 && user[i].role_name_code != 10)
                                {
                                    Control module_code = this.Page.Master.FindControl(user[i].module_code.ToString());
                                    //  HtmlGenericControl li = (HtmlGenericControl)FindControl();
                                    if(module_code!=null)
                                    module_code.Visible = true;
                                }
                            }

                            if (user[i].role_name_code != 42 && user[i].role_name_code != 10)
                            {

                                //  HtmlGenericControl li = (HtmlGenericControl)FindControl();
                                if (user[i].submodule_code == "MA" && (user[i].party_type == "ENA Distillery Unit" || user[i].party_type == "Distillery Unit" || user[i].party_type == "All"))
                                {
                                    Control submodule_code = this.Page.Master.FindControl(user[i].submodule_code.ToString() + "DD");
                                    if (submodule_code != null)
                                     submodule_code.Visible = true;
                                }
                                else if (user[i].submodule_code == "MA" && user[i].party_type == "Sugar Mill")
                                {
                                    Control submodule_code = this.Page.Master.FindControl(user[i].submodule_code.ToString() + "SM");
                                    if (submodule_code != null)
                                        submodule_code.Visible = true;
                                }

                                else
                                {
                                    try
                                    {
                                        Control submodule_code = this.Page.Master.FindControl(user[i].submodule_code.ToString());
                                        if (submodule_code != null)
                                           submodule_code.Visible = true;
                                    }
                                    catch
                                    {

                                    }
                                }


                            }
                        }
                        if (user.Count > 0)
                        {

                            if ((user[0].party_type == "Distillery Unit" || user[0].party_type_code == "DIS") || (user[0].party_type.Trim() == "ENA Distillery Unit" || user[0].party_type_code == "ENA"))
                            {
                                SM.Visible = false;
                                Session["partycode"] = user[0].party_code;
                                int value = BL_RawMaterialReceipt.GetGrainData(Session["partycode"].ToString());
                                if (value == 1)
                                {
                                    ENP.Visible = true;
                                    PAS.Visible = false;
                                    MADD.Visible = false;
                                    MASM.Visible = false;
                                    RR.Visible = false;
                                    CRH.Visible = false;
                                    DD.Visible = false;
                                    CMP.Visible = false;
                                    // App.Visible = true;
                                    //MNTP.Visible = true;
                                }
                            }
                            if (user[0].party_type == "Sugar Mill" || user[0].party_type_code == "SGR")
                            {
                                DIS.Visible = false;
                                DD.Visible = false;
                                CMP.Visible = false;
                            }
                            //if (user[0].party_type == "All")
                            //{
                            //    MTP.Visible = true;
                            //}
                            if (user[0].party_type.Trim() == "M & tP" || user[0].party_type_code == "MTP" || user[0].party_type_code == "MTR" || user[0].party_type_code == "MTW")
                            {
                                EIA.Visible = true;
                                MTP.Visible = true;
                                LIC.Visible = true;
                                PER.Visible = true;
                                ENP.Visible = false;
                                PAS.Visible = false;
                                MADD.Visible = false;
                                MASM.Visible = false;
                                RR.Visible = false;
                                DD.Visible = false;
                                //  App.Visible = false;
                                DIS.Visible = false;
                                SM.Visible = false;
                                NOC.Visible = false;
                                NCD.Visible = false;
                                CMP.Visible = false;
                                CRH.Visible = false;
                            }

                            //UM.Visible = false;
                            //masterReports.Visible = false;
                            //digiLockerMenu.Visible = false;
                            //REP1.Visible = true;
                            //SCM.Visible = false;
                            //EA.Visible = false;
                            // REP1.Visible = true;
                            //if (user.party_type == "Sugar Mill")
                            //{
                            //    SCM.Visible = true;
                            //    A1Sugar.Visible = true;
                            //    A1Distilleries.Visible = false;
                            //    A3.Visible = false;
                            //    EA.Visible = true;
                            //    MASM.Visible = true;
                            //    REP1.Visible = true;
                            //    MADD.Visible = false;
                            //    RR.Visible = false;
                            //}
                            //if (user.party_type == "Distillery Unit")
                            //{
                            //    SCM.Visible = true;
                            //    A1Distilleries.Visible = true;
                            //    A1Sugar.Visible = false;
                            //    A3.Visible = false;
                            //    EA.Visible = true;
                            //    MASM.Visible = false;
                            //    MADD.Visible = true;
                            //    RR.Visible = true;
                            //    REP1.Visible = true;
                            //}
                            //if (user.party_type == "ALL" || user.party_type == "All")
                            //{
                            //    //scm_menu.Visible = true;
                            //    // A1Distilleries.Visible = true;
                            //    //  A1Sugar.Visible = false;
                            //    //A3.Visible = false;
                            //    REP1.Visible = true;
                            //    EA.Visible = true;
                            //    MASM.Visible = false;
                            //    if (user.role_name == "Assistant Commissioner")
                            //        RR.Visible = true;

                            //    if (user.role_name == "Commissioner")
                            //    {

                            //        SCM.Visible = true;
                            //        CMS.Visible = true;
                            //    }
                            //    //if (user.role_name == "Assistant Commissioner")
                            //    //    CM.Visible = true;
                            //}
                            //if (user.role_name == "Bond Officer" && user.party_type == "Sugar Mill")
                            //{
                            //    SCM.Visible = true;
                            //   // scm_menu.Visible = true;
                            //    A1Sugar.Visible = true;
                            //    A1Distilleries.Visible = false;
                            //    A3.Visible = false;
                            //    EA.Visible = true;
                            //    CM.Visible = false;
                            //    MASM.Visible = false;
                            //    MADD.Visible = false;
                            //    REP1.Visible = true;
                            //}
                            //if (user.role_name == "Bond Officer" && user.party_type == "Distillery Unit")
                            //{
                            //    SCM.Visible = true;
                            //    A1Distilleries.Visible = true;
                            //    A1Sugar.Visible = false;
                            //    A3.Visible = false;
                            //    EA.Visible = true;
                            //    CM.Visible = false;
                            //    MASM.Visible = false;
                            //    MADD.Visible = false;
                            //    REP1.Visible = true;
                            //}
                            if (Session["UserID"].ToString().Contains("excise_") || Session["role_name_code"].ToString() == "10" || Session["UserID"].ToString().Contains("sec_") || Session["UserID"].ToString() == "dmuser" || Session["UserID"].ToString().Contains("police_") || user[0].role_name == "Commissioner" || Session["UserID"].ToString().Contains("thana_") || userid.Contains("dm_") || userid.Contains("adm_") || userid.Contains("sdm_") || userid.Contains("dclr_") || userid.Contains("adj_") || userid.Contains("asc_") || userid.Contains("bor_") || userid.Contains("hc_") || userid.Contains("jjb_") || userid.Contains("spc_") || userid.Contains("sp_") || userid.Contains("casereg_") || userid == "com_court_reg")
                            {
                                CM.Visible = true;
                                CRH.Visible = false;
                                DD.Visible =true;
                                CMP.Visible = false;

                                if (user[0].role_name != "Commissioner")
                                {
                                    DLM.Visible = false;
                                    SCM.Visible = false;
                                    EA.Visible = false;
                                    dashboard.Visible = false;
                                    navtoggle.Visible = false;
                                   

                                }

                                string seizureNo = Session["seizureNo"]?.ToString() ?? string.Empty;
                                cm_seiz_BasicIformation obj = new cm_seiz_BasicIformation();
                                obj = BL_cm_seiz_BasicIformation.ViewDetails(seizureNo + "&" + Session["RaidBy"]+"&"+ Session["UserID"]);

                                cm_seizure getSeizureDetails = new cm_seizure();
                                getSeizureDetails = BL_cm_seiz_BasicIformation.GetSeizureDetails(seizureNo + "&" + Session["RaidBy"]);
                                if (userid.Contains("thana_"))
                                {
                                    sho.Visible = true;
                                    //  Response.Redirect("SHO_DashBoard.aspx");
                                }
                                else
                                {

                                    DMEntry_key.Visible = false;
                                    DMES_key.Visible = false;
                                    DMH_List_key.Visible = false;
                                    DMEE_key1.Visible = false;
                                    sho.Visible = false;
                                    SEC_C.Visible = false;
                                    SEC_E.Visible = false;
                                    if (userid.Contains("casereg_"))
                                    {
                                        CM.Visible = false;
                                        CRH.Visible = true;
                                        DMEntry_key.Visible = true;
                                        DD.Visible = false;
                                        //DMER.Visible = true;
                                        cms.Visible = false;
                                    }
                                    if (userid.Contains("dm_") || userid.Contains("adm_") || userid.Contains("sdm_") || userid.Contains("dclr_") || userid.Contains("adj_") || userid.Contains("asc_") || userid.Contains("bor_") || userid.Contains("hc_") || userid.Contains("jjb_") || userid.Contains("spc_") || userid.Contains("sp_"))
                                    {
                                        CM.Visible = false;
                                        CRH.Visible = true;
                                        DMES_key.Visible = true;
                                        DD.Visible = false;
                                        //DMER.Visible = true;
                                        cms.Visible = false;
                                    }
                                    if (userid == "com_court_reg")
                                    {
                                        CM.Visible = false;
                                        CRH.Visible = true;
                                        DMH_List_key.Visible = true;
                                        DD.Visible = false;
                                        //DMER.Visible = true;
                                        cms.Visible = false;
                                    }
                                    if (userid == "com")
                                    {
                                        DMEE_key1.Visible = true;
                                        CRH.Visible = true;
                                        a29.Visible = true;
                                        DMER.Visible =true;
                                        CMP.Visible = true;
                                    }
                                    if (userid == "sec_court_reg")
                                    {
                                        CM.Visible = false;
                                        CRH.Visible = true;
                                        SEC_E.Visible = true;
                                        DD.Visible = false;
                                        //DMER.Visible = true;
                                        cms.Visible = false;
                                    }
                                    if (userid == "sec_court")
                                    {
                                        CM.Visible = false;
                                        CRH.Visible = true;
                                        SEC_C.Visible = true;
                                        DD.Visible = false;
                                        //DMER.Visible = true;
                                        cms.Visible = false;
                                    }


                                    A6.Visible = false;
                                    A7.Visible = false;
                                    A13.Visible = false;
                                    A14.Visible = false;
                                    A15.Visible = false;
                                    A16.Visible = false;
                                    A18.Visible = false;
                                    A19.Visible = false;
                                  //  A20.Visible = false;
                                    A22.Visible = false;
                                    A23.Visible = false;
                                    A8.Visible = false;
                                    A9.Visible = false;
                                    A10.Visible = false;
                                    A11.Visible = false;
                                    A1.Visible = false;
                                    A12.Visible = false;
                                    A17.Visible = false;
                                    A21.Visible = false;
                                    A24.Visible = false;
                                    A25.Visible = false;
                                    A26.Visible = false;
                                    A27.Visible = false;
                                  //  A28.Visible = false;
                                    A36.Visible = false;
                                    A37.Visible = false;
                                    A38.Visible = false;
                                    //DMEntry_key.Visible = false;
                                    //DMES_key.Visible = false;
                                    //DMH_List_key.Visible = false;
                                    //DMEE_key.Visible = false;

                                    if (obj.record_status == "Y" || obj.record_status == "N")
                                    {
                                        A6.Visible = true;
                                        A7.Visible = true;
                                        A13.Visible = true;
                                        A14.Visible = true;
                                        A15.Visible = true;
                                        A16.Visible = true;
                                        A18.Visible = true;
                                        A19.Visible = true;
                                       // A20.Visible = true;
                                        A22.Visible = true;
                                        A23.Visible = true;
                                    }

                                    if (obj.record_status == "Y")
                                    {
                                        A8.Visible = true;
                                        A9.Visible = true;
                                        //A10.Visible = true;
                                        //A11.Visible = true;
                                        //A1.Visible = true;
                                        //A12.Visible = true;
                                        //A17.Visible = true;
                                        //A21.Visible = false;
                                        //A24.Visible = true;
                                        //A25.Visible = true;
                                        //A26.Visible = true;
                                        //A27.Visible = true;
                                        //A28.Visible = true;
                                        //A36.Visible = true;
                                        //A37.Visible = true;
                                        //A38.Visible = true;
                                    }
                                    // A8.Visible = false;
                                    A9.Visible = false;
                                    A10.Visible = false;
                                    A11.Visible = false;
                                    A1.Visible = false;
                                    A12.Visible = false;
                                    A17.Visible = false;
                                    A21.Visible = false;
                                    A24.Visible = false;
                                    A25.Visible = false;
                                    A26.Visible = false;
                                    A27.Visible = false;
                                   // A28.Visible = false;
                                    A36.Visible = false;
                                    A37.Visible = false;
                                    A38.Visible = false;

                                    //if (getSeizureDetails.seizure_stage_code == 13)
                                    //{
                                    //    A8.Visible = true;
                                    //}
                                    if (getSeizureDetails.seizure_stage_code == 13)
                                    {
                                        A8.Visible = true;
                                        A9.Visible = true;
                                    }
                                    if (getSeizureDetails.seizure_stage_code == 14)
                                    {
                                        A8.Visible = true;
                                        A9.Visible = true;
                                        A10.Visible = true;
                                    }
                                    if (getSeizureDetails.seizure_stage_code == 15)
                                    {
                                        A8.Visible = true;
                                        A9.Visible = true;
                                        A10.Visible = true;
                                        A11.Visible = true;
                                        //A28.Visible = true;//TrailCaseHistoryList
                                    }
                                    if (getSeizureDetails.seizure_stage_code == 16)
                                    {
                                        A8.Visible = true;
                                        A9.Visible = true;
                                        A10.Visible = true;
                                        A11.Visible = true;
                                        A1.Visible = true;
                                     //   A28.Visible = true;//TrailCaseHistoryList
                                    }
                                    if (getSeizureDetails.seizure_stage_code == 17)
                                    {
                                        A8.Visible = true;
                                        A9.Visible = true;
                                        A10.Visible = true;
                                        A11.Visible = true;
                                        A1.Visible = true;
                                        A12.Visible = true;
                                       // A28.Visible = true;//TrailCaseHistoryList
                                    }
                                    if (getSeizureDetails.seizure_stage_code == 18)
                                    {
                                        A8.Visible = true;
                                        A9.Visible = true;
                                        A10.Visible = true;
                                        A11.Visible = true;
                                        A1.Visible = true;
                                        A12.Visible = true;
                                        A17.Visible = true;
                                      //  A28.Visible = true;//TrailCaseHistoryList
                                    }
                                    if (getSeizureDetails.seizure_stage_code == 19)
                                    {
                                        A8.Visible = true;
                                        A9.Visible = true;
                                        A10.Visible = true;
                                        A11.Visible = true;
                                        A1.Visible = true;
                                        A12.Visible = true;
                                        A17.Visible = true;
                                        A21.Visible = true;
                                       // A28.Visible = true;//TrailCaseHistoryList
                                    }
                                    if (getSeizureDetails.seizure_stage_code == 20)
                                    {
                                        A8.Visible = true;
                                        A9.Visible = true;
                                        A10.Visible = true;
                                        A11.Visible = true;
                                        A1.Visible = true;
                                        A12.Visible = true;
                                        A17.Visible = true;
                                        A21.Visible = true;
                                        A24.Visible = true;
                                      //  A28.Visible = true;//TrailCaseHistoryList
                                    }
                                    if (getSeizureDetails.seizure_stage_code == 21)
                                    {
                                        A8.Visible = true;
                                        A9.Visible = true;
                                        A10.Visible = true;
                                        A11.Visible = true;
                                        A1.Visible = true;
                                        A12.Visible = true;
                                        A17.Visible = true;
                                        A21.Visible = true;
                                        A24.Visible = true;
                                        A25.Visible = true;
                                       // A28.Visible = true;//TrailCaseHistoryList
                                    }
                                    if (getSeizureDetails.seizure_stage_code == 22)
                                    {
                                        A8.Visible = true;
                                        A9.Visible = true;
                                        A10.Visible = true;
                                        A11.Visible = true;
                                        A1.Visible = true;
                                        A12.Visible = true;
                                        A17.Visible = true;
                                        A21.Visible = true;
                                        A24.Visible = true;
                                        A25.Visible = true;
                                        A26.Visible = true;
                                      //  A28.Visible = true;//TrailCaseHistoryList
                                    }
                                    if (getSeizureDetails.seizure_stage_code == 23)
                                    {
                                        A8.Visible = true;
                                        A9.Visible = true;
                                        A10.Visible = true;
                                        A11.Visible = true;
                                        A1.Visible = true;
                                        A12.Visible = true;
                                        A17.Visible = true;
                                        A21.Visible = true;
                                        A24.Visible = true;
                                        A25.Visible = true;
                                        A26.Visible = true;
                                        A27.Visible = true;
                                       // A28.Visible = true;//TrailCaseHistoryList
                                    }
                                    if (getSeizureDetails.seizure_stage_code == 24)
                                    {
                                        //    A8.Visible = true;
                                        A9.Visible = true;
                                        //    A10.Visible = true;
                                        //    A11.Visible = true;
                                        //    A1.Visible = true;
                                        //    A12.Visible = true;
                                        //    A17.Visible = true;
                                        //    A21.Visible = true;
                                        //    A24.Visible = true;
                                        //    A25.Visible = true;
                                        //    A26.Visible = true;
                                        //    A27.Visible = true;
                                        //    A28.Visible = true;
                                    }
                                    if (getSeizureDetails.seizure_stage_code == 24 || getSeizureDetails.seizure_stage_code == 25)
                                    {
                                        A8.Visible = true;
                                        A9.Visible = true;
                                        A10.Visible = true;
                                        A11.Visible = true;
                                        A1.Visible = true;
                                        A12.Visible = true;
                                        A17.Visible = true;
                                        A21.Visible = true;
                                        A24.Visible = true;
                                        A25.Visible = true;
                                        A26.Visible = true;
                                        A27.Visible = true;
                                        //A28.Visible = true;
                                        A36.Visible = true;
                                       // A28.Visible = true;//TrailCaseHistoryList
                                    }
                                    if (getSeizureDetails.seizure_stage_code == 26)
                                    {
                                        A8.Visible = true;
                                        A9.Visible = true;
                                        A10.Visible = true;
                                        A11.Visible = true;
                                        A1.Visible = true;
                                        A12.Visible = true;
                                        A17.Visible = true;
                                        A21.Visible = true;
                                        A24.Visible = true;
                                        A25.Visible = true;
                                        A26.Visible = true;
                                        A27.Visible = true;
                                        //A28.Visible = true;
                                        A36.Visible = true;
                                        A37.Visible = true;
                                       // A28.Visible = true;//TrailCaseHistoryList
                                    }
                                    if (getSeizureDetails.seizure_stage_code == 27 || getSeizureDetails.seizure_stage_code == 28)
                                    {
                                        A8.Visible = true;
                                        A9.Visible = true;
                                        A10.Visible = true;
                                        A11.Visible = true;
                                        A1.Visible = true;
                                        A12.Visible = true;
                                        A17.Visible = true;
                                        A21.Visible = true;
                                        A24.Visible = true;
                                        A25.Visible = true;
                                        A26.Visible = true;
                                        A27.Visible = true;
                                        //A28.Visible = true;
                                        A36.Visible = true;
                                        A37.Visible = true;
                                        A38.Visible = true;
                                     //   A28.Visible = true;//TrailCaseHistoryList
                                                           //DMEntry_key.Visible = true;
                                                           //DMES_key.Visible = true;
                                                           //DMH_List_key.Visible = true;
                                                           //DMEE_key.Visible = true;
                                    }
                                }
                            }

                        }
                        else if (Session["UserID"].ToString() == "dmuser")
                        {
                            CM.Visible = true;

                            DLM.Visible = false;
                            SCM.Visible = false;
                            EA.Visible = false;

                        }
                    }

                    else
                    {

                        //A1Distilleries.Visible = false;
                        //A1Sugar.Visible = false;
                        //EA.Visible = false;
                        string s = Session["UserID"].ToString();
                        UM.Visible = true;
                        DLM.Visible = true;
                        REP.Visible = true;
                        AT.Visible = true;
                        SCM1.Visible = true;
                        DIS.Visible = true;
                        DIG.Visible = true;
                        SM.Visible = true;
                        EA.Visible = true;
                        CHE.Visible = true;
                        NOC.Visible = true;
                        MADD.Visible = true;
                        PAS.Visible = true;
                        RR.Visible = true;
                        //   A3.Visible = true;
                        SCM.Visible = true;
                        REP.Visible = true;
                        //  CL.Visible = true;
                        MR.Visible = true;
                        CRH.Visible = false;
                        //  CM.Visible = true;
                        // Fee.Visible = true;
                        //LI.Visible = true;
                        //  A4.Visible = true;
                        A5.Visible = true;
                        // App.Visible = true;
                        D1.Visible = true;
                        A2.Visible = true;
                        ST.Visible = true;
                        CHF.Visible = true;

                    }

                    if (Session["UserID"].ToString() != "Admin"&&user[0].role_name_code!=8 && user[0].role_name_code != 60 && user[0].role_name_code != 7 && user[0].role_name_code !=9 && user[0].role_name_code !=10 && user[0].role_name_code != 42 && user[0].role_level_code != 11 && user[0].role_level_code != 12 && user[0].role_level_code != 13)
                    {
                        if (user[0].Financial_year_enddate != "")
                        {
                            if (DateTime.Now.Date > Convert.ToDateTime(user[0].Financial_year_enddate))
                            {
                                UM.Visible = false;
                                SCM.Visible = false;
                                REP.Visible = false;
                                EA.Visible = false;
                                CHY.Visible = true;
                                CFY.Visible = true;
                                CRH.Visible = false;
                                DD.Visible = false;
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('please change financial year');", true); 
                            }
                            else if(user[0].party_approvel_level !="C")
                            {
                                
                                 UM.Visible = false;
                                SCM.Visible = false;
                                REP.Visible = false;
                                EA.Visible = false;
                                CHY.Visible = true;
                                CFY.Visible = true;
                                CRH.Visible = false;
                                DD.Visible = false;
                                //if (user[0].party_approvel_level == "Y")
                                //{
                                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('please take approval from Bond Officer');", true);
                                //}
                                //if (user[0].party_approvel_level == "A")
                                //{
                                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('please take approval from Deputy Commissioner');", true);
                                //}
                                //if (user[0].party_approvel_level == "D")
                                //{
                                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('please take approval from Commissioner');", true);
                                //}
                            }
                            else
                            {
                                DD.Visible = false;
                                CHY.Visible = false;
                                CRH.Visible = false;
                            }
                        }
                    }
                }

                    }
        }

        

        protected void UserManagementMasters_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("StateList");
        }

        protected void Organisation_Click(object sender, EventArgs e)
        {
            //Organisation.BackColor = System.Drawing.Color.Yellow;
            //Session["UserID"] = Session["UserID"];
            //Response.Redirect("OrgList");
        }
        protected void GetCMS()
        {
           
        }

        protected void RolePermission_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RolePermissionList");
            //  Response.Redirect("WebForm1.aspx");
        }

        protected void UserRegistration_Click(object sender, EventArgs e)
        {
            // li_UserRegistration.Attributes["class"] = "Active";
            Session["UserID"] = Session["UserID"];
            Response.Redirect("UserRegistrationList");
        }

        protected void WorkFlow_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("WorkFlowLIst");
        }

        protected void UserReport_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("UserReports");
        }
        protected void btnMasterReports_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("MasterReportsList");
        }
        protected void DigiLocker_Click(object sender, EventArgs e)
        {

        }
        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            if (Request.Cookies["UserID"] != null)
            {
                Response.Cookies["UserID"].Value = string.Empty;
                Response.Cookies["UserID"].Expires = DateTime.Now.AddMonths(-20);
            }
            Response.Redirect("~/LoginPage");
        }

        protected void CMS_Click(object sender, EventArgs e)
        {
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            if (Session["UserID"].ToString().Contains("excise_") || user.role_name == "Commissioner" || Session["UserID"].ToString() == "admin"|| Session["UserID"].ToString()=="dmuser")
            {
                List<Server_Configs> server = new List<Server_Configs>();
                server = BL_SeverConfig.GetServerList("");
                if(server[0].server_domain.Trim()== "WIN-AGCBLK3TA6N")
                {
                    Response.Redirect("http://192.168.3.26:8888/CM_IEMS/dashboard.aspx?username=" + Session["UserID"].ToString());
                }
                else
                {
                    Response.Redirect("https://prohibitionbihar.in/procasemg/dashboard.aspx?username=" + Session["UserID"].ToString());
                }
                //  Response.Redirect("http://117.254.110.7:8888/CM_IEMS/dashboard.aspx?username=" + Session["UserID"].ToString());
              
            }
            else if (Session["UserID"].ToString().Contains("police_"))
            {
                List<Server_Configs> server = new List<Server_Configs>();
                server = BL_SeverConfig.GetServerList("");
                if (server[0].server_domain.Trim() == "WIN-AGCBLK3TA6N")
                {
                    Response.Redirect("http://192.168.3.26:8888/CM_IEMS/dashboard.aspx?username=" + Session["UserID"].ToString());
                }
                else
                {
                    Response.Redirect("https://prohibitionbihar.in/policecase/dashboard?username=" + Session["UserID"].ToString());
                }
                // Response.Redirect("http://117.254.110.7:8888/CM_IEMS/dashboard?username=" + Session["UserID"].ToString());
               // Response.Redirect("https://prohibitionbihar.in/policecase/dashboard?username=" + Session["UserID"].ToString());
            }
            if(Session["UserID"].ToString() == "dmuser")
            {
                Response.Redirect("http://192.168.3.26:8888/CM_IEMS/dashboard.aspx?username=" + Session["UserID"].ToString());
            }
        }

        protected void CL_Click(object sender, EventArgs e)
        {

        }

        protected void CL_Click1(object sender, EventArgs e)
        {

        }

        protected void lnkDashBoard_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("SCMDashBoard");
        }
    }
}