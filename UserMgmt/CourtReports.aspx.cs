using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class CourtReports : System.Web.UI.Page
    {
        static List<Reportmaster> reports = new List<Reportmaster>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null)
                {

                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];

                    string userid = Session["UserID"].ToString();
                    Session["UserID"] = userid;
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    reports = new List<Reportmaster>();
                    reports = BL_WorkFlow.GetReports(user.party_code);
                    Session["party"] = user.party_code;
                    List<District> districts = new List<District>();
                    districts = BL_User_Mgnt.GetAllDistrictsList();
                    var list3 = from s in districts
                                select s;
                    ddldistrict.DataSource = list3.ToList();
                    ddldistrict.DataTextField = "district_name";
                    ddldistrict.DataValueField = "district_code";
                    ddldistrict.DataBind();
                    ddldistrict.Items.Insert(0, "Select");

                    List<cm_court> cm_court = new List<cm_court>();
                    cm_court = BL_cm_court.GetList();
                    var list4 = from s in cm_court
                                select s;
                    ddlCourt.DataSource = list4.ToList();
                    ddlCourt.DataTextField = "court_master_name";
                    ddlCourt.DataValueField = "court_master_code";
                    ddlCourt.DataBind();
                    ddlCourt.Items.Insert(0, "Select");

                    Session["rolename"] = user.role_name;
                    Session["role_name_code"] = user.role_name_code;
                    Session["role_level_code"] = user.role_level_code;
                    if (user.user_id == "Admin" || user.role_name.Trim() == "Commissioner" || user.role_name.Trim() == "Deputy Commissioner" || user.role_name.Trim() == "Assistant Commissioner")
                    {
                        var list = (from s in reports
                                    where s.partytype == "Excise" && (s.id == 85 || s.id == 86 /*|| s.id == 87 || s.id == 88*/)
                                    select s);

                        DEPT.Visible = true;
                        ddlReportName.DataSource = list.ToList();
                    }
                    //else if (user.role_name_code == 42)
                    //{
                    //    var list = (from s in reports
                    //                where s.partytype == "Excise" && (s.id != 85 && s.id != 86 && s.id != 87 && s.id != 88)
                    //                select s);

                    //    DEPT.Visible = true;
                    //    ddlReportName.DataSource = list.ToList();
                    //    Session["district_code"] = user.district_code;
                    //    ddldistrict.SelectedValue = Session["district_code"].ToString();
                    //    ddldistrict.Enabled = false;
                    //    //   ddldistrict_SelectedIndexChanged(sender, null);
                    //    if (userid.Contains("excise_"))
                    //        Session["RaidBy"] = "E";
                    //    else
                    //        Session["RaidBy"] = "P";
                    //    ddlDEPTType.SelectedValue = Session["RaidBy"].ToString();
                    //    ddlDEPTType.Enabled = false;
                    //}
                    else
                    {
                        var list = (from s in reports
                                    where s.partytype == "Excise" && (s.id == 85 || s.id == 86 /*|| s.id == 87 || s.id == 88*/)
                                    select s);

                        DEPT.Visible = true;
                        ddlReportName.DataSource = list.ToList();
                        Session["district_code"] = user.district_code;
                        ddldistrict.SelectedValue = Session["district_code"].ToString();
                        ddldistrict.Enabled = false;
                        //   ddldistrict_SelectedIndexChanged(sender, null);
                        if (userid.Contains("excise_"))
                            Session["RaidBy"] = "E";
                        else
                            Session["RaidBy"] = "P";
                        ddlDEPTType.SelectedValue = Session["RaidBy"].ToString();
                        ddlDEPTType.Enabled = false;
                    }
                    Session["party_type_name"] = "Excise";
                    ddlReportName.DataTextField = "reportname";
                    ddlReportName.DataValueField = "id";
                    ddlReportName.DataBind();
                    ddlReportName.Items.Insert(0, "Select");


                    //List<Org_Finacial_yr> Org_Finacial = new List<Org_Finacial_yr>();
                    //Org_Finacial = BL_org_Master.GetFinacListValues("");
                    //ddlfinancialyear.DataSource = Org_Finacial;
                    //ddlfinancialyear.DataTextField = "financial_year";
                    //ddlfinancialyear.DataValueField = "financial_year";
                    //ddlfinancialyear.DataBind();
                    //ddlfinancialyear.Items.Insert(0, "Select");
                    //List<Party_Master> party = new List<Party_Master>();
                    //party = BL_Party_Master.GetList();
                    //ddlparty.DataSource = party;
                    //ddlparty.DataTextField = "party_name";
                    //ddlparty.DataValueField = "party_code";
                    //ddlparty.DataBind();
                    //ddlparty.Items.Insert(0, "Select");
                    if (user.party_type != "All" || user.party_type != "ALL")
                    {
                        //  ddlparty.SelectedValue = user.party_code;
                        if (user.user_id != "Admin")
                        {
                            // ddlparty.Enabled = false;
                            ddlparty_SelectedIndexChanged(sender, null);
                        }


                    }
                }
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/LoginPage");
                }

            }
        }
        protected void ddlReportName_SelectedIndexChanged(object sender, EventArgs e)
        {
            todate1.Visible = true;
            fromdate.Visible = true;
            DEPT.Visible = true;
            DST.Visible = true;
            Court.Visible = false;
            if (ddlReportName.SelectedValue == "75" || ddlReportName.SelectedValue.Trim() == "77" || ddlReportName.SelectedValue.Trim() == "79" || ddlReportName.SelectedValue == "81")
            {
                todate1.Visible = false;
                fromdate.Visible = false;
                //DEPT.Visible = false;
                DST.Visible = false;

                //List<VAT_Master> vatmasters = new List<VAT_Master>();
                //vatmasters = BL_VATMaster.GetvatmasterList(ddlparty.SelectedValue);
                //var list = (from s in vatmasters
                //            where s.party_code == ddlparty.SelectedValue && s.vat_type_code == "MST"
                //            select s);
                //ddVats.DataSource = list.ToList();
                //ddVats.DataTextField = "Vat_name";
                //ddVats.DataValueField = "vat_code";
                //ddVats.DataBind();
                //ddVats.Items.Insert(0, "Select");
                //vat.Visible = true;
                //DEPT.Visible = false;
            }

            if (ddlReportName.SelectedValue == "76" || ddlReportName.SelectedValue == "78" || ddlReportName.SelectedValue == "80" || ddlReportName.SelectedValue == "50")
            {
                // todate1.Visible = false;
                // fromdate.Visible = false;
                //DEPT.Visible = false;
                DST.Visible = false;


            }
            if (ddlReportName.SelectedValue == "83" || ddlReportName.SelectedValue == "84" || ddlReportName.SelectedValue == "85" || ddlReportName.SelectedValue == "86")
            {
                DEPT.Visible = false;
                Court.Visible = false;
            }

            if (ddlReportName.SelectedValue == "84" || ddlReportName.SelectedValue == "86")
            {
                DEPT.Visible = false;
                Court.Visible = true;
            }
            if (ddlReportName.SelectedValue == "87" || ddlReportName.SelectedValue == "88")
            {
                DEPT.Visible = false;
                Court.Visible = false;
                DST.Visible = false;
            }
            //else
            //{
            //    DEPT.Visible = false;

            //}
        }
        protected void ddlFormat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void UserReports_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ReportPage");

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("SCMDashBoard.aspx");
        }
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            if (ddlReportName.SelectedValue != "Select")
            {
                reports = new List<Reportmaster>();
                reports = BL_WorkFlow.GetReports(Session["party"].ToString());

                var contentnames = from s in reports
                                   where s.id == Convert.ToInt32(ddlReportName.SelectedValue)
                                   select new { reports = s.reportfilename };
                // string val = Convert.ToDateTime(txtfrom.Value).ToString("yyyy-MM-dd");
                Session["ReportId"] = contentnames.ToList()[0].reports.ToString();
                Session["UserID"] = Session["UserID"].ToString();
                Session["district_code"] = ddldistrict.SelectedValue;
                Session["RaidBy"] = ddlDEPTType.SelectedValue;
                Session["court_code"] = ddlCourt.SelectedValue;
                if (ddlReportName.SelectedValue.Trim() != "77" && ddlReportName.SelectedValue.Trim() != "79" && ddlReportName.SelectedValue != "75" && ddlReportName.SelectedValue != "60" && ddlReportName.SelectedValue != "68" && ddlReportName.SelectedValue != "71" && ddlReportName.SelectedValue != "81")
                {
                    Session["FromDate"] = Convert.ToDateTime(txtfrom.Value).ToString("yyyy-MM-dd");
                    Session["ToDate"] = Convert.ToDateTime(txtto.Value).ToString("yyyy-MM-dd");
                }
                //Session["Reportfinancialyear"] = ddlfinancialyear.SelectedValue;
                //Session["Party_code"] = ddlparty.SelectedValue;
                if (ddlReportName.SelectedValue == "34" || ddlReportName.SelectedValue == "21" || ddlReportName.SelectedValue == "40" || ddlReportName.SelectedValue == "41" || ddlReportName.SelectedValue == "43" || ddlReportName.SelectedValue == "59" || ddlReportName.SelectedValue == "68" || ddlReportName.SelectedValue == "71" || ddlReportName.SelectedValue == "60" || ddlReportName.SelectedValue == "42" || ddlReportName.SelectedValue == "23" || ddlReportName.SelectedValue == "26" || ddlReportName.SelectedValue == "24" || ddlReportName.SelectedValue == "27" || ddlReportName.SelectedValue == "28" || ddlReportName.SelectedValue == "44" || ddlReportName.SelectedValue == "29" || ddlReportName.SelectedValue == "45" || ddlReportName.SelectedValue == "46" || ddlReportName.SelectedValue == "47" || ddlReportName.SelectedValue == "48" || ddlReportName.SelectedValue == "51" || ddlReportName.SelectedValue == "57" || ddlReportName.SelectedValue == "61")
                    //  Session["Vat_code"] = ddVats.SelectedValue;
                    if (ddlReportName.SelectedValue == "38" || ddlReportName.SelectedValue == "49" || ddlReportName.SelectedValue == "50" || ddlReportName.SelectedValue == "61")
                        Session["Pass_Type"] = ddlDEPTType.SelectedValue;
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('ReportPage' ,'_blank');", true);
            }
        }

        protected void ddlparty_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Thana_Details> thana = new List<Thana_Details>();
            thana = BL_User_Mgnt.GetThanaList(string.Empty);
            //if (Session["rolename"].ToString() == "Commissioner")
            //{
            //    var ad = (from s in thana
            //              where s.district_code == ddldistrict.SelectedValue
            //              select s);
            //    ddlThana.DataSource = ad.ToArray();
            //    ddlThana.DataTextField = "thana_name";
            //    ddlThana.DataValueField = "thana_code";
            //    ddlThana.DataBind();
            //    ddlThana.Items.Insert(0, "Select");
            //}
            //else
            //{
            //    var ad = (from s in thana
            //              where s.district_code == Session["district_code"].ToString()
            //              select s);
            //    ddlThana.DataSource = ad.ToArray();
            //    ddlThana.DataTextField = "thana_name";
            //    ddlThana.DataValueField = "thana_code";
            //    ddlThana.DataBind();
            //    //ddlThana.Items.Insert(0, "Select");
            //    ddlThana.Enabled = false;
            //}
        }

        //protected void ddlfinancialyear_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Party_Master partym = new Party_Master();
        //    partym = BL_Party_Master.GetPartyDetails(ddlparty.SelectedValue);
        //    string year1 = ddlfinancialyear.SelectedValue.ToString().Substring(5);
        //    string year = ddlfinancialyear.SelectedValue.ToString().Substring(0, 4);
        //    enddate.Value = partym.enddate.Substring(0, 6) + "" + year1;
        //    startdate.Value = partym.startdate.Substring(0, 6) + "" + year;
        //    CalendarExtender.StartDate = Convert.ToDateTime(startdate.Value);
        //    CalendarExtender.EndDate = Convert.ToDateTime(enddate.Value);
        //    CalendarExtender1.StartDate = Convert.ToDateTime(startdate.Value);
        //    CalendarExtender1.EndDate = Convert.ToDateTime(enddate.Value);



        //}
    }
}