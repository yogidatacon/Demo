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
    public partial class UserReports : System.Web.UI.Page
    {
       static List<Reportmaster> reports = new List<Reportmaster>();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null)
                {

                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
               
                string userid = Session["UserID"].ToString();
                Session["UserID"]= userid;
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                reports = new List<Reportmaster>();
                reports = BL_WorkFlow.GetReports(userid);
                    Session["district_code"] = user.district_code;
                    if (user.user_id == "Admin"||user.role_name.Trim()== "Deputy Commissioner" || user.role_name.Trim()== "Assistant Commissioner")
                    {
                        var list = (from s in reports
                                    
                                    select s);
                        vat.Visible = false;
                        sale.Visible = false;
                        ddlReportName.DataSource = list.ToList();
                    }
                    else
                    {
                        var list = (from s in reports
                                    where s.partytype == user.party_type
                                    select s);
                        vat.Visible = false;
                        sale.Visible = false;
                        ddlReportName.DataSource = list.ToList();
                    }

                ddlReportName.DataTextField = "reportname";
                ddlReportName.DataValueField = "id";
                ddlReportName.DataBind();
                ddlReportName.Items.Insert(0, "Select");
                    List<Org_Finacial_yr> Org_Finacial = new List<Org_Finacial_yr>();
                    Org_Finacial =BL_org_Master.GetFinacListValues("");
                    ddlfinancialyear.DataSource = Org_Finacial;
                    ddlfinancialyear.DataTextField = "financial_year";
                    ddlfinancialyear.DataValueField = "financial_year";
                    ddlfinancialyear.DataBind();
                    ddlfinancialyear.Items.Insert(0, "Select");  
                    List<Party_Master> party = new List<Party_Master>();
                party = BL_Party_Master.GetList();
                ddlparty.DataSource = party;
                ddlparty.DataTextField = "party_name";
                ddlparty.DataValueField = "party_code";
                ddlparty.DataBind();
                ddlparty.Items.Insert(0, "Select");
                if (user.party_type != "All" || user.party_type != "ALL")
                {
                    ddlparty.SelectedValue = user.party_code;
                    if (user.user_id != "Admin")
                    {
                        ddlparty.Enabled = false;
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
            if (ddlReportName.SelectedValue == "27")
            {
                List<VAT_Master> vatmasters = new List<VAT_Master>();
                vatmasters = BL_VATMaster.GetvatmasterList(ddlparty.SelectedValue);
                var list = (from s in vatmasters
                            where s.party_code == ddlparty.SelectedValue && s.vat_type_code == "MST"
                            select s);
                ddVats.DataSource = list.ToList();
                ddVats.DataTextField = "Vat_name";
                ddVats.DataValueField = "vat_code";
                ddVats.DataBind();
                ddVats.Items.Insert(0, "Select");
                vat.Visible = true;
                sale.Visible = false;
            }
            else if (ddlReportName.SelectedValue == "71"|| ddlReportName.SelectedValue == "72"|| ddlReportName.SelectedValue == "73"|| ddlReportName.SelectedValue == "74")
            {
                financialyear.Visible = false;
                //todate1.Visible = false;
                //fromdate.Visible = false;
                vat.Visible = false;
                sale.Visible = false;
            }
            else if (ddlReportName.SelectedValue == "34")
            {
                List<VAT_Master> vatmasters = new List<VAT_Master>();
                vatmasters = BL_VATMaster.GetGrainvatmasterList(ddlparty.SelectedValue);
                var list = (from s in vatmasters
                            where s.party_code == ddlparty.SelectedValue && s.vat_type_code == "MST"
                            select s);
                ddVats.DataSource = list.ToList();
                ddVats.DataTextField = "Vat_name";
                ddVats.DataValueField = "vat_code";
                ddVats.DataBind();
                ddVats.Items.Insert(0, "Select");
                vat.Visible = true;
                sale.Visible = false;
            }
            else if (ddlReportName.SelectedValue == "26")
            {
                List<VAT_Master> vatmasters = new List<VAT_Master>();
                vatmasters = BL_VATMaster.GetvatmasterList(ddlparty.SelectedValue);
                var list = (from s in vatmasters
                            where s.party_code == ddlparty.SelectedValue && s.vat_type_code == "MST"
                            select s);
                ddVats.DataSource = list.ToList();
                ddVats.DataTextField = "Vat_name";
                ddVats.DataValueField = "vat_code";
                ddVats.DataBind();
                ddVats.Items.Insert(0, "Select");
                vat.Visible = true;
                sale.Visible = false;
            }
            else if (ddlReportName.SelectedValue == "21" || ddlReportName.SelectedValue == "40")
            {
                List<VAT_Master> vatmasters = new List<VAT_Master>();
                vatmasters = BL_VATMaster.GetvatmasterList(ddlparty.SelectedValue);
                var list = (from s in vatmasters
                            where s.party_code == ddlparty.SelectedValue && s.vat_type_code == "FER"
                            select s);
                ddVats.DataSource = list.ToList();
                ddVats.DataTextField = "Vat_name";
                ddVats.DataValueField = "vat_code";
                ddVats.DataBind();
                ddVats.Items.Insert(0, "Select");
                vat.Visible = true;
                sale.Visible = false;
            }

            else if (ddlReportName.SelectedValue == "23" || ddlReportName.SelectedValue == "42")
            {
                todate1.Visible = true;
                fromdate.Visible = true;
                vat.Visible = true;
                sale.Visible = false;
                List<VAT_Master> vatmasters = new List<VAT_Master>();
                vatmasters = BL_VATMaster.GetvatmasterList(ddlparty.SelectedValue);
                var list = (from s in vatmasters
                            where s.party_code == ddlparty.SelectedValue && s.vat_type_code == "STO"
                            select s);
                ddVats.DataSource = list.ToList();
                ddVats.DataTextField = "Vat_name";
                ddVats.DataValueField = "vat_code";
                ddVats.DataBind();
                ddVats.Items.Insert(0, "Select");
            }
            else if (ddlReportName.SelectedValue == "24" || ddlReportName.SelectedValue == "43")
            {
                todate1.Visible =true;
                fromdate.Visible = true;
                vat.Visible = true;
                List<VAT_Master> vatmasters = new List<VAT_Master>();
                vatmasters = BL_VATMaster.GetvatmasterList(ddlparty.SelectedValue);
                var list = (from s in vatmasters
                            where s.party_code == ddlparty.SelectedValue && s.vat_type_code == "DEN"
                            select s);
                ddVats.DataSource = list.ToList();
                ddVats.DataTextField = "Vat_name";
                ddVats.DataValueField = "vat_code";
                ddVats.DataBind();
                ddVats.Items.Insert(0, "Select");
                vat.Visible = true;
                sale.Visible = false;
            }

            else if ( ddlReportName.SelectedValue == "61")
            {
                todate1.Visible = true;
                fromdate.Visible = true;
                vat.Visible = true;
                List<VAT_Master> vatmasters = new List<VAT_Master>();
                vatmasters = BL_VATMaster.GetvatmasterList(ddlparty.SelectedValue);
                var list = (from s in vatmasters
                            where s.party_code == ddlparty.SelectedValue && s.vat_type_code == "DEN"
                            select s);
                ddVats.DataSource = list.ToList();
                ddVats.DataTextField = "Vat_name";
                ddVats.DataValueField = "vat_code";
                ddVats.DataBind();
                ddVats.Items.Insert(0, "Select");
                vat.Visible = true;
                sale.Visible =true;
            }
            else if (ddlReportName.SelectedValue == "28")
            {
                List<VAT_Master> vatmasters = new List<VAT_Master>();
                vatmasters = BL_VATMaster.GetvatmasterList(ddlparty.SelectedValue);
                var list = (from s in vatmasters
                            where s.party_code == ddlparty.SelectedValue && (s.vat_type_code == "STO")
                            select s);
                ddVats.DataSource = list.ToList();
                ddVats.DataTextField = "Vat_name";
                ddVats.DataValueField = "vat_code";
                ddVats.DataBind();
                ddVats.Items.Insert(0, "Select");
                vat.Visible = true;
                sale.Visible = false;
            }
            //else if (ddlReportName.SelectedValue == "41")
            //{
            //    List<VAT_Master> vatmasters = new List<VAT_Master>();
            //    vatmasters = BL_VATMaster.GetvatmasterList(ddlparty.SelectedValue);
            //    var list = (from s in vatmasters
            //                where s.party_code == ddlparty.SelectedValue && (s.vat_type_code == "REC")
            //                select s);
            //    ddVats.DataSource = list.ToList();
            //    ddVats.DataTextField = "Vat_name";
            //    ddVats.DataValueField = "vat_code";
            //    ddVats.DataBind();
            //    ddVats.Items.Insert(0, "Select");
            //    vat.Visible = true;
            //    sale.Visible = false;
            //}
            else if ( ddlReportName.SelectedValue == "46" )
            {
                List<VAT_Master> vatmasters = new List<VAT_Master>();
                vatmasters = BL_VATMaster.GetvatmasterList(ddlparty.SelectedValue);
                var list = (from s in vatmasters
                            where s.party_code == ddlparty.SelectedValue && (s.vat_type_code == "DEN" /*&& s.content == "10"*/)
                            select s);
                ddVats.DataSource = list.ToList();
                ddVats.DataTextField = "Vat_name";
                ddVats.DataValueField = "vat_code";
                ddVats.DataBind();
                ddVats.Items.Insert(0, "Select");
                vat.Visible = true;
                sale.Visible = false;
            }
            else if ( ddlReportName.SelectedValue == "51")
            {
                List<VAT_Master> vatmasters = new List<VAT_Master>();
                vatmasters = BL_VATMaster.GetvatmasterList(ddlparty.SelectedValue);
                var list = (from s in vatmasters
                            where s.party_code == ddlparty.SelectedValue && (s.vat_type_code == "STO" /*&& s.content == "12"*/)
                            select s);
                ddVats.DataSource = list.ToList();
                ddVats.DataTextField = "Vat_name";
                ddVats.DataValueField = "vat_code";
                ddVats.DataBind();
                ddVats.Items.Insert(0, "Select");
                vat.Visible = true;
                sale.Visible = false;
            }
            else if (ddlReportName.SelectedValue == "47")
            {
                List<VAT_Master> vatmasters = new List<VAT_Master>();
                vatmasters = BL_VATMaster.GetvatmasterList(ddlparty.SelectedValue);
                var list = (from s in vatmasters
                            where s.party_code == ddlparty.SelectedValue && (s.vat_type_code == "MST" && s.content == "9")
                            select s);
                ddVats.DataSource = list.ToList();
                ddVats.DataTextField = "Vat_name";
                ddVats.DataValueField = "vat_code";
                ddVats.DataBind();
                ddVats.Items.Insert(0, "Select");
                vat.Visible = true;
                sale.Visible = false;
            }
            else if (ddlReportName.SelectedValue == "48")
            {
                List<VAT_Master> vatmasters = new List<VAT_Master>();
                vatmasters = BL_VATMaster.GetvatmasterList(ddlparty.SelectedValue);
                var list = (from s in vatmasters
                            where s.party_code == ddlparty.SelectedValue && (s.vat_type_code == "DEN" && s.content == "11")
                            select s);
                ddVats.DataSource = list.ToList();
                ddVats.DataTextField = "Vat_name";
                ddVats.DataValueField = "vat_code";
                ddVats.DataBind();
                ddVats.Items.Insert(0, "Select");
                vat.Visible = true;
                sale.Visible = false;
            }
            else if (ddlReportName.SelectedValue =="44")
            {
                List<VAT_Master> vatmasters = new List<VAT_Master>();
                vatmasters = BL_VATMaster.GetvatmasterList(ddlparty.SelectedValue);
                var list = (from s in vatmasters
                            where s.party_code == ddlparty.SelectedValue && (s.vat_type_code == "STO" && s.content == "7")
                            select s);
                ddVats.DataSource = list.ToList();
                ddVats.DataTextField = "Vat_name";
                ddVats.DataValueField = "vat_code";
                ddVats.DataBind();
                ddVats.Items.Insert(0, "Select");

                vat.Visible = true;
                sale.Visible = false;
            }
            else if (ddlReportName.SelectedValue =="45")
            {
                List<VAT_Master> vatmasters = new List<VAT_Master>();
                vatmasters = BL_VATMaster.GetvatmasterList(ddlparty.SelectedValue);
                var list = (from s in vatmasters
                            where s.party_code == ddlparty.SelectedValue && (s.vat_type_code == "STO" && s.content=="8")
                            select s);
                ddVats.DataSource = list.ToList();
                ddVats.DataTextField = "Vat_name";
                ddVats.DataValueField = "vat_code";
                ddVats.DataBind();
                ddVats.Items.Insert(0, "Select");
                vat.Visible = true;
                sale.Visible = false;
            }
            else if (ddlReportName.SelectedValue == "29")
            {
                List<VAT_Master> vatmasters = new List<VAT_Master>();
                vatmasters = BL_VATMaster.GetvatmasterList(ddlparty.SelectedValue);
                var list = (from s in vatmasters
                            where s.party_code == ddlparty.SelectedValue && (s.vat_type_code == "DEN")
                            select s);
                ddVats.DataSource = list.ToList();
                ddVats.DataTextField = "Vat_name";
                ddVats.DataValueField = "vat_code";
                ddVats.DataBind();
                ddVats.Items.Insert(0, "Select");
                vat.Visible = true;
                sale.Visible = false;
            }
            else if (ddlReportName.SelectedValue == "57")
            {
                List<VAT_Master> vatmasters = new List<VAT_Master>();
                vatmasters = BL_VATMaster.GetvatmasterList(ddlparty.SelectedValue);
                var list = (from s in vatmasters
                            where s.party_code == ddlparty.SelectedValue && (s.vat_type_code == "ISS")
                            select s);
                ddVats.DataSource = list.ToList();
                ddVats.DataTextField = "Vat_name";
                ddVats.DataValueField = "vat_code";
                ddVats.DataBind();
                ddVats.Items.Insert(0, "Select");
                vat.Visible = true;
                sale.Visible = false;
            }
            else if ( ddlReportName.SelectedValue == "60")
            {
                List<VAT_Master> vatmasters = new List<VAT_Master>();
                vatmasters = BL_VATMaster.GetvatmasterList(ddlparty.SelectedValue);
                var list = (from s in vatmasters
                            where s.party_code == ddlparty.SelectedValue && (s.vat_type_code == "ISS")
                            select s);
                ddVats.DataSource = list.ToList();
                ddVats.DataTextField = "Vat_name";
                ddVats.DataValueField = "vat_code";
                ddVats.DataBind();
                ddVats.Items.Insert(0, "Select");
                vat.Visible = true;
                sale.Visible = false;
                fromdate.Visible = false;
                todate1.Visible = false;
            }
            else if (ddlReportName.SelectedValue == "59" ||ddlReportName.SelectedValue == "68"|| ddlReportName.SelectedValue == "71")
            {
                List<VAT_Master> vatmasters = new List<VAT_Master>();
                vatmasters = BL_VATMaster.GetvatmasterList(ddlparty.SelectedValue);
                var list = (from s in vatmasters
                            where s.party_code == ddlparty.SelectedValue && (s.vat_type_code == "MST")
                            select s);
                ddVats.DataSource = list.ToList();
                ddVats.DataTextField = "Vat_name";
                ddVats.DataValueField = "vat_code";
                ddVats.DataBind();
                ddVats.Items.Insert(0, "Select");
                vat.Visible = true;
                sale.Visible = false;
                fromdate.Visible = false;
                todate1.Visible = false;
            }
            else if (ddlReportName.SelectedValue == "38"||ddlReportName.SelectedValue == "61"|| ddlReportName.SelectedValue == "49"|| ddlReportName.SelectedValue == "50")
            {
                sale.Visible = true;
                vat.Visible = false;
            }
            else
            {
                sale.Visible = false;
                vat.Visible = false;
            }
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
                reports = BL_WorkFlow.GetReports(Session["UserID"].ToString());
                var contentnames = from s in reports
                                   where s.id== Convert.ToInt32( ddlReportName.SelectedValue)
                                   select new { reports = s.reportfilename };
                // string val = Convert.ToDateTime(txtfrom.Value).ToString("yyyy-MM-dd");
                Session["ReportId"] = contentnames.ToList()[0].reports.ToString();
                Session["UserID"] = Session["UserID"].ToString();
                if (/*ddlReportName.SelectedValue != "23" && ddlReportName.SelectedValue != "24" &&*/ /*ddlReportName.SelectedValue != "43" && ddlReportName.SelectedValue != "42" &&*/ ddlReportName.SelectedValue != "59" && ddlReportName.SelectedValue != "60" && ddlReportName.SelectedValue != "68" /*&& ddlReportName.SelectedValue != "71" && ddlReportName.SelectedValue != "72" && ddlReportName.SelectedValue != "73" && ddlReportName.SelectedValue != "74"*/)
                {
                    Session["FromDate"] = Convert.ToDateTime(txtfrom.Value).ToString("yyyy-MM-dd");
                    Session["ToDate"] = Convert.ToDateTime(txtto.Value).ToString("yyyy-MM-dd");
                }
                Session["Reportfinancialyear"] = ddlfinancialyear.SelectedValue;
                Session["Party_code"] = ddlparty.SelectedValue;
                if (ddlReportName.SelectedValue == "34" || ddlReportName.SelectedValue == "21" || ddlReportName.SelectedValue == "40" /*|| ddlReportName.SelectedValue == "41" */|| ddlReportName.SelectedValue == "43" || ddlReportName.SelectedValue == "59" || ddlReportName.SelectedValue == "68"|| ddlReportName.SelectedValue == "71" || ddlReportName.SelectedValue == "60"|| ddlReportName.SelectedValue == "42" ||  ddlReportName.SelectedValue == "23"|| ddlReportName.SelectedValue == "26" || ddlReportName.SelectedValue == "24" || ddlReportName.SelectedValue == "27" || ddlReportName.SelectedValue == "28" || ddlReportName.SelectedValue == "44" || ddlReportName.SelectedValue == "29" || ddlReportName.SelectedValue == "45" || ddlReportName.SelectedValue == "46" || ddlReportName.SelectedValue == "47"|| ddlReportName.SelectedValue == "48" || ddlReportName.SelectedValue == "51"|| ddlReportName.SelectedValue == "57" || ddlReportName.SelectedValue == "61")
                    Session["Vat_code"] = ddVats.SelectedValue;
                if (ddlReportName.SelectedValue == "38"|| ddlReportName.SelectedValue == "49"|| ddlReportName.SelectedValue == "50" || ddlReportName.SelectedValue == "61")
                    Session["Pass_Type"] = ddlSaleType.SelectedValue;
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('ReportPage' ,'_blank');", true);
            }
        }

        protected void ddlparty_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void ddlfinancialyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Party_Master partym = new Party_Master();
            partym = BL_Party_Master.GetPartyDetails(ddlparty.SelectedValue);
            if (ddlparty.SelectedValue != "ALL")
            {
                string year1 = ddlfinancialyear.SelectedValue.ToString().Substring(5);
                string year = ddlfinancialyear.SelectedValue.ToString().Substring(0, 4);
                enddate.Value = partym.enddate.Substring(0, 6) + "" + year1;
                startdate.Value = partym.startdate.Substring(0, 6) + "" + year;
                CalendarExtender.StartDate = Convert.ToDateTime(startdate.Value);
                CalendarExtender.EndDate = Convert.ToDateTime(enddate.Value);
            }
            //CalendarExtender1.StartDate = Convert.ToDateTime(startdate.Value);
            //CalendarExtender1.EndDate = Convert.ToDateTime(enddate.Value);
          


        }
    }
}