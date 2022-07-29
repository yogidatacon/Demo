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
    public partial class TraiChargeSheetFrame : System.Web.UI.Page
    {
        List<cm_seiz_trial> obj = new List<cm_seiz_trial>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string seizureNo = Session["seizureNo"]?.ToString() ?? string.Empty;
                //Get FirNo
                cm_seiz_FIR firDetails = BL_CM_Common.GetPRFIRNo(seizureNo + "&" + Session["raidby"].ToString());
                if (firDetails.prfirno != null)
                {
                    txtPRFIRNo.Text = firDetails.prfirno.Trim();
                    txtfirdate.Text = firDetails.prfirdate.Trim();
                    string datelock = BL_CM_Common.GetDate("exciseautomation.seizure_trial where seizureNo='" + seizureNo + "' and trialstage_code='3'", "currentstagedate");
                    CalendarExtender1.StartDate = Convert.ToDateTime(datelock.Trim());
                    CalendarExtender2.StartDate = Convert.ToDateTime(datelock.Trim());
                }
                //CalendarExtender1.StartDate = DateTime.Now;
                //CalendarExtender2.StartDate = DateTime.Now;

                if (Session["rtype"].ToString() != "0")
                {
                    string tableId = Session["tableId"].ToString();
                    cm_seiz_trial obj = new cm_seiz_trial();
                    obj = BL_cm_seiz_trial.GetDetailsById(tableId);
                    txtChargesheetundersection.Text = obj.chargedundersection;
                    txtFramingChargedDate.Text = obj.currentstagedate;
                    txtNexthearingDate.Text = obj.nexthearingdate;
                    CalendarExtender1.SelectedDate = Convert.ToDateTime(obj.currentstagedate);
                    CalendarExtender2.SelectedDate = Convert.ToDateTime(obj.nexthearingdate);
                    if ((Session["rtype"].ToString() == "1"))
                    {
                        btnCancel.Visible = false;
                        btnSaveasDraft.Visible = false;
                        btnSubmit.Visible = false;
                        txtChargesheetundersection.Attributes.Add("disabled", "disabled");
                        txtFramingChargedDate.Attributes.Add("disabled", "disabled");
                        txtNexthearingDate.Attributes.Add("disabled", "disabled");
                        //ddlPRFIRNo.Attributes.Add("disabled", "disabled");
                        Image1.Visible = false;
                        Image2.Visible = false;
                    }
                }
            }
        }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/TrailChargesheetList");
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                cm_seiz_trial cm_obj = new cm_seiz_trial();
                cm_obj.seizureno = Convert.ToInt32(Session["seizureNo"].ToString());
                cm_obj.trialstage_code = 4;
                cm_obj.currentstage = 4;
                cm_obj.currentstagedate = txtFramingChargedDate.Text;
                cm_obj.chargedundersection = txtChargesheetundersection.Text;
                cm_obj.nexthearingdate = txtNexthearingDate.Text;
                cm_obj.finalseizureno = (Session["seizureNo"].ToString());
                cm_obj.lastmodified_date = DateTime.Now.ToShortDateString();
                cm_obj.creation_date = DateTime.Now.ToShortDateString();
                cm_obj.user_id = Session["UserID"].ToString();
                cm_obj.record_status = "Y";
                cm_obj.record_deleted = "N";
                DateTime dt2 = DateTime.ParseExact(cm_obj.currentstagedate, "dd-MM-yyyy", null);
                DateTime dt1 = DateTime.ParseExact(cm_obj.nexthearingdate, "dd-MM-yyyy", null);
                DateTime dt3 = DateTime.ParseExact(txtfirdate.Text, "dd-MM-yyyy", null);
                cm_obj.raidby = Session["RaidBy"].ToString().Substring(0, 1).ToUpper();
               
                int cmp = dt2.CompareTo(dt1);
                int cmp1 = dt3.CompareTo(dt2);
                if (cmp > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the Next hearing Date should be  greater than  to the Framing Charged Date.\');", true);
                    return;
                    // date1 is greater means date1 is comes after date2
                }
                if (cmp1 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the Framing Charged Date should be  greater than  to the FIR Date.\');", true);
                    return;
                    // date1 is greater means date1 is comes after date2
                }
                //else if (cmp == 0)
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the Next hearing Date should be  greater than  to the Framing Charged Date.\');", true);
                //    // date1 is greater means date1 is comes after date2
                //}
                else
                {
                    btnSubmit.Enabled = false;
                    bool val;
                    string tempTableId = Session["tableId"]?.ToString() ?? string.Empty;
                    if (Session["rtype"].ToString() == "0" && string.IsNullOrEmpty(tempTableId))
                        val = BL_cm_seiz_trial.InsertSeiz_Trial(cm_obj, cm_obj.trialstage_code.ToString());
                    else
                    {
                        cm_obj.seizure_trial_id = Convert.ToInt32(Session["tableId"].ToString());
                        val = BL_cm_seiz_trial.Update(cm_obj);
                    }
                    if (val == true)
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("~/TrailChargesheetList");
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
                cm_seiz_trial cm_obj = new cm_seiz_trial();
                cm_obj.currentstagedate = txtFramingChargedDate.Text;
                cm_obj.chargedundersection = txtChargesheetundersection.Text;
                cm_obj.nexthearingdate = txtNexthearingDate.Text;
                DateTime dt2 = DateTime.ParseExact(cm_obj.currentstagedate, "dd-MM-yyyy", null);
                DateTime dt1 = DateTime.ParseExact(cm_obj.nexthearingdate, "dd-MM-yyyy", null);
                DateTime dt3 = DateTime.ParseExact(txtfirdate.Text, "dd-MM-yyyy", null);
                cm_obj.raidby = Session["RaidBy"].ToString().Substring(0, 1).ToUpper();
                int cmp = dt2.CompareTo(dt1);
                int cmp1 = dt3.CompareTo(dt2);
                if (cmp > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the Next hearing Date should be  greater than  to the Framing Charged Date.\');", true);
                    return;
                    // date1 is greater means date1 is comes after date2
                }
                if (cmp1 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the Framing Charged Date should be  greater than  to the FIR Date.\');", true);
                    return;
                    // date1 is greater means date1 is comes after date2
                }
                //else if (cmp== 0)
                // {
                //     ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the Next hearing Date should be  greater than  to the Framing Charged Date.\');", true);
                //     // date1 is greater means date1 is comes after date2
                // }
                else
                {
                    btnSaveasDraft.Enabled = false;
                    cm_obj.seizureno = Convert.ToInt32(Session["seizureNo"].ToString());
                    cm_obj.trialstage_code = 4;
                    cm_obj.currentstage = 4;
                    cm_obj.currentstagedate = txtFramingChargedDate.Text;
                    cm_obj.chargedundersection = txtChargesheetundersection.Text;
                    cm_obj.nexthearingdate = txtNexthearingDate.Text;
                    cm_obj.finalseizureno = (Session["seizureNo"].ToString());
                    cm_obj.lastmodified_date = DateTime.Now.ToShortDateString();
                    cm_obj.creation_date = DateTime.Now.ToShortDateString();
                    cm_obj.user_id = Session["UserID"].ToString();
                    cm_obj.record_status = "N";
                    cm_obj.record_deleted = "N";
                    bool val;
                    string tempTableId = Session["tableId"]?.ToString() ?? string.Empty;
                    if (Session["rtype"].ToString() == "0" && string.IsNullOrEmpty(tempTableId))
                        val = BL_cm_seiz_trial.InsertSeiz_Trial(cm_obj, cm_obj.trialstage_code.ToString());
                    else
                    {
                        cm_obj.seizure_trial_id = Convert.ToInt32(Session["tableId"].ToString());
                        val = BL_cm_seiz_trial.Update(cm_obj);
                    }
                    if (val == true)
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("~/TrailChargesheetList");
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
            Response.Redirect("~/TrailChargesheetList");
        }

        protected void btnSeizure_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            List<Reportmaster> reports = new List<Reportmaster>();
            reports = BL_WorkFlow.GetReports(Session["UserID"].ToString());
            Session["ReportId"] = "CM_SEIZURE_DETAILS";
            Session["SeizureNo"] = Session["seizureNo"].ToString();
            Session["Raidby"] = Session["RaidBy"];
            Session["UserID"] = Session["UserID"].ToString();
            Session["seizureno"] = Session["seizureNo"].ToString();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('ReportPage' ,'_blank');", true);

        }

        protected void btnFIR_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            List<Reportmaster> reports = new List<Reportmaster>();
            reports = BL_WorkFlow.GetReports(Session["UserID"].ToString());
            Session["ReportId"] = "CM_SEIZURE_DETAILS";
            Session["SeizureNo"] = Session["seizureNo"].ToString();
            Session["Raidby"] = Session["RaidBy"];
            Session["UserID"] = Session["UserID"].ToString();
            Session["seizureno"] = Session["seizureNo"].ToString();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('ReportPage' ,'_blank');", true);

        }

        protected void btnChargeSheet_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            List<Reportmaster> reports = new List<Reportmaster>();
            reports = BL_WorkFlow.GetReports(Session["UserID"].ToString());
            Session["ReportId"] = "CM_SEIZURE_DETAILS";
            Session["SeizureNo"] = Session["seizureNo"].ToString();
            Session["Raidby"] = Session["RaidBy"];
            Session["UserID"] = Session["UserID"].ToString();
            Session["seizureno"] = Session["seizureNo"].ToString();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('ReportPage' ,'_blank');", true);

        }

        protected void btnBail_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            List<Reportmaster> reports = new List<Reportmaster>();
            reports = BL_WorkFlow.GetReports(Session["UserID"].ToString());
            Session["ReportId"] = "CM_SEIZURE_DETAILS";
            Session["SeizureNo"] = Session["seizureNo"].ToString();
            Session["Raidby"] = Session["RaidBy"];
            Session["UserID"] = Session["UserID"].ToString();
            Session["seizureno"] = Session["seizureNo"].ToString();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('ReportPage' ,'_blank');", true);

        }
    }
}