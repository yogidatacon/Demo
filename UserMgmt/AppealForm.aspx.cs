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
    public partial class AppealForm : System.Web.UI.Page
    {
        List<cm_seiz_AppealDetails> _Apparatus = new List<cm_seiz_AppealDetails>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["UserID"] = Session["UserID"];
                List<cm_court> _court = new List<cm_court>();
                _court = BL_cm_court.GetList();
                ddlNameofCourt.DataSource = _court;
                ddlNameofCourt.DataTextField = "court_master_name";
                ddlNameofCourt.DataValueField = "court_master_code";
                ddlNameofCourt.DataBind();
                ddlNameofCourt.Items.Insert(0, "Select");
                //CalendarExtender1.StartDate = DateTime.Now;
                //CalendarExtender2.StartDate = DateTime.Now;
                string seizureNo = Session["seizureNo"]?.ToString()?? string.Empty;
                string raidby = "";
                if (Session["RaidBy"].ToString().Substring(0, 1).ToUpper() == "E")
                    raidby = "E";
                else
                    raidby = "P";
                cm_seiz_FIR firDetails = BL_CM_Common.GetPRFIRNo(seizureNo+"&"+Session["RaidBy"].ToString());
                if (firDetails.prfirno != null)
                {
                    txtPRFIRNo.Text = firDetails.prfirno.Trim();
                    string datelock = BL_CM_Common.GetDate("exciseautomation.seizure_excisecom where seizureNo='" + seizureNo + "'", "ecorderdate");
                    CalendarExtender1.StartDate = Convert.ToDateTime(datelock.Trim());
                    CalendarExtender2.StartDate = Convert.ToDateTime(datelock.Trim());
                }

                List<cm_seiz_AccusedDetails> ads = new List<cm_seiz_AccusedDetails>();
                ads = BL_cm_seiz_AccusedDetails.GetDetails(seizureNo + "&" + Session["RaidBy"].ToString());
                string seizureno = Session["seizureno"].ToString();
                var ad = (from s in ads
                          
                          select s);
                ddlAccusedName.DataSource = ad.ToList();
                ddlAccusedName.DataTextField = "accusedname";
                ddlAccusedName.DataValueField = "seizure_accused_details_id";
                ddlAccusedName.DataBind();
                ddlAccusedName.Items.Insert(0, "Select");

                List<cm_seiz_Accused_Status> accused_status = new List<cm_seiz_Accused_Status>();
                accused_status = BL_cm_seiz_Accused_Status.GetList();
                ddlAccusedStatus.DataSource = accused_status;
                ddlAccusedStatus.DataTextField = "accusedstatus_name";
                ddlAccusedStatus.DataValueField = "accusedstatus_code";
                ddlAccusedStatus.DataBind();
                ddlAccusedStatus.Items.Insert(0, "Select");

                if (Session["rtype"].ToString() != "0")
                {
                    string tableId = Session["tableId"].ToString();

                    cm_seiz_AppealDetails obj = new cm_seiz_AppealDetails();
                    obj = BL_cm_seiz_AppealDetails.GetDetailsByID(tableId);

                    ddlNameofCourt.SelectedValue = obj.court_master_code.ToString();
                    ddlAccusedName.SelectedValue = obj.seizure_accused_details_id.ToString();
                    ddlAccusedStatus.SelectedValue= obj.accusedstatus_code.Trim();                    
                    txtAppealNo.Text = obj.appealno;
                    txtDate.Text = obj.appealdate.ToString();
                    txtAppealBy.Text = obj.appealby.ToString();
                    txtResult.Text = obj.appealresult;
                    txtResultDate.Text = obj.resultdate;
                    CalendarExtender1.StartDate = Convert.ToDateTime(obj.appealdate);
                    CalendarExtender2.StartDate = Convert.ToDateTime(obj.resultdate);
                    if ((Session["rtype"].ToString() == "1"))
                    {
                        btnCancel.Visible = false;
                        btnSaveasDraft.Visible = false;
                        btnSubmit.Visible = false;
                        ddlNameofCourt.Attributes.Add("disabled", "disabled");
                        ddlAccusedName.Attributes.Add("disabled", "disabled");
                        ddlAccusedStatus.Attributes.Add("disabled", "disabled");
                        txtAppealNo.Attributes.Add("disabled", "disabled");
                        txtDate.Attributes.Add("disabled", "disabled");
                        txtAppealBy.Attributes.Add("disabled", "disabled");
                        txtResult.Attributes.Add("disabled", "disabled");
                        txtResultDate.Attributes.Add("disabled", "disabled");
                    }
                }
            }
        }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("AppealList");
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
               
                cm_seiz_AppealDetails cm_obj = new cm_seiz_AppealDetails();
                cm_obj.seizureno = Convert.ToInt32(Session["seizureNo"].ToString());
                cm_obj.finalseizureno = Session["seizureNo"].ToString();

                cm_obj.court_master_code = ddlNameofCourt.SelectedValue;
                cm_obj.seizure_accused_details_id = ddlAccusedName.SelectedValue;
                cm_obj.accusedstatus_code = ddlAccusedStatus.SelectedValue;
                cm_obj.appealno = txtAppealNo.Text;
                cm_obj.appealdate = txtDate.Text;
                cm_obj.appealby = txtAppealBy.Text;
                cm_obj.appealresult = txtResult.Text;
                cm_obj.resultdate = txtResultDate.Text;
                cm_obj.user_id = Session["UserID"].ToString();
                cm_obj.lastmodified_date = DateTime.Now;
                cm_obj.record_status = "Y";
                cm_obj.record_deleted = false;
                cm_obj.raidby = Session["RaidBy"].ToString().Substring(0, 1);
                DateTime dt2 = DateTime.ParseExact(cm_obj.appealdate, "dd-MM-yyyy", null);
                DateTime dt1 = DateTime.ParseExact(cm_obj.resultdate, "dd-MM-yyyy", null);

                int cmp = dt2.CompareTo(dt1);

                if (cmp > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the result Date is greater than to the appeal Date.\');", true);
                    // date1 is greater means date1 is comes after date2
                }
                else
                {
                    btnSubmit.Enabled = false;
                    bool val;
                    string tempTableId = Session["tableId"]?.ToString() ?? string.Empty;
                    if (Session["rtype"].ToString() == "0" && string.IsNullOrEmpty(tempTableId))
                        val = BL_cm_seiz_AppealDetails.InsertAppeal(cm_obj);
                    else
                    {
                        cm_obj.seizure_appealdetails_id = Convert.ToInt32(Session["tableId"].ToString());
                        val = BL_cm_seiz_AppealDetails.Update(cm_obj);
                    }
                    if (val == true)
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("AppealList");
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(val);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    }
                }
            }
        }

        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
               
                cm_seiz_AppealDetails cm_obj = new cm_seiz_AppealDetails();
                cm_obj.seizureno = Convert.ToInt32(Session["seizureNo"].ToString());
                cm_obj.finalseizureno = Session["seizureNo"].ToString();
            
                cm_obj.court_master_code = ddlNameofCourt.SelectedValue;
                cm_obj.seizure_accused_details_id = ddlAccusedName.SelectedValue; 
                cm_obj.accusedstatus_code = ddlAccusedStatus.SelectedValue;
                cm_obj.appealno = txtAppealNo.Text;
                cm_obj.appealdate = txtDate.Text;
                cm_obj.appealby = txtAppealBy.Text;
                cm_obj.appealresult = txtResult.Text;
                cm_obj.resultdate = txtResultDate.Text;
                cm_obj.user_id = Session["UserID"].ToString();
                cm_obj.lastmodified_date = DateTime.Now;
                cm_obj.record_status = "N";
                cm_obj.record_deleted = false;
                DateTime dt2 = DateTime.ParseExact(cm_obj.appealdate, "dd-MM-yyyy", null);
                DateTime dt1 = DateTime.ParseExact(cm_obj.resultdate, "dd-MM-yyyy", null);
                cm_obj.raidby = Session["RaidBy"].ToString().Substring(0, 1);
                int cmp = dt2.CompareTo(dt1);

                if (cmp >0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the result Date is greater than to the appeal Date.\');", true);
                    // date1 is greater means date1 is comes after date2
                }
                else
                {
                    btnSaveasDraft.Enabled = false;
                    bool val;
                    string tempTableId = Session["tableId"]?.ToString() ?? string.Empty;
                    if (Session["rtype"].ToString() == "0" && string.IsNullOrEmpty(tempTableId))
                        val = BL_cm_seiz_AppealDetails.InsertAppeal(cm_obj);
                    else
                    {
                        cm_obj.seizure_appealdetails_id = Convert.ToInt32(Session["tableId"].ToString());
                        val = BL_cm_seiz_AppealDetails.Update(cm_obj);
                    }
                    if (val == true)
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("AppealList");
                    }
                    else
                    {
                        btnSaveasDraft.Enabled = true;
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(val);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    }
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/AppealList");
        }
    }
}