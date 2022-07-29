using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class BailsubForm : System.Web.UI.Page
    {
        List<cm_seiz_ChargeSheet> _witness = new List<cm_seiz_ChargeSheet>();
        DataTable dt = new DataTable();
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                List<cm_court> _court = new List<cm_court>();
                _court = BL_cm_court.GetList();
                ddlNameoftheCourt.DataSource = _court;
                ddlNameoftheCourt.DataTextField = "court_master_name";
                ddlNameoftheCourt.DataValueField = "court_master_code";
                ddlNameoftheCourt.DataBind();
                ddlNameoftheCourt.Items.Insert(0, "Select");

                List<cm_bail_type> _bailType = new List<cm_bail_type>();
                _bailType = BL_cm_bail_type.GetList();
                ddlBailType.DataSource = _bailType;
                ddlBailType.DataTextField = "bail_type_master_name";
                ddlBailType.DataValueField = "bail_type_master_code";
                ddlBailType.DataBind();
                ddlBailType.Items.Insert(0, "Select");
                string raidby = "";
                if (Session["RaidBy"].ToString().Substring(0, 1).ToUpper() == "E")
                    raidby = "E";
                else
                    raidby = "P";
                string strRtype = Session["rtype"]?.ToString() ?? string.Empty;
               
                string seizureNo = Session["seizureNo"]?.ToString() ?? string.Empty;

                List<cm_seiz_AccusedDetails> accusedDetails = new List<cm_seiz_AccusedDetails>();
                accusedDetails = BL_cm_seiz_AccusedDetails.GetDetails(seizureNo+"&"+raidby);

                List<cm_seiz_Bail> acc = new List<cm_seiz_Bail>();
                acc = BL_cm_seiz_Bail.GetList(seizureNo);
               
                var accc = accusedDetails.Where(f1 => acc.All(f2 => f2.seizure_accused_details_id != f1.seizure_accused_details_id.ToString()));
              
            

                //Get FirNo
                cm_seiz_FIR firDetails = BL_CM_Common.GetPRFIRNo(seizureNo+"&"+Session["RaidBy"].ToString());
               
                if (firDetails.prfirno != null)
                {
                    txtPRFIRNo.Text = firDetails.prfirno.Trim();
                    txtfirdate.Text = firDetails.prfirdate.Trim();
                    string datelock = BL_CM_Common.GetDate("exciseautomation.seizure_chargesheet where seizureNo='"+ seizureNo+"'", " chargesheet_date");
                    if (datelock == "0")
                    {
                        CalendarExtender1.StartDate = Convert.ToDateTime(txtfirdate.Text.Trim());
                        CalendarExtender2.StartDate = Convert.ToDateTime(txtfirdate.Text.Trim());
                    }
                    else
                    {
                        CalendarExtender1.StartDate = Convert.ToDateTime(datelock.Trim());
                        CalendarExtender2.StartDate = Convert.ToDateTime(datelock.Trim());
                    }
                }

                if (Session["rtype"].ToString() != "0")
                {
                    //string seizureNo = Session["seizureNo"].ToString();
                    string tableId = Session["tableId"].ToString();
                  
                    cm_seiz_Bail obj = new cm_seiz_Bail();
                    obj = BL_cm_seiz_Bail.GetDetailsById(tableId);
                   // var ac = accusedDetails.Where(f1 => acc.All(f2 => f2.seizure_accused_details_id != f1.seizure_accused_details_id.ToString() && f2.seizure_bail_id.ToString()!=tableId));
                    ddlAccusedName.DataSource = accusedDetails.ToList();
                    ddlAccusedName.DataTextField = "accusedname";
                    ddlAccusedName.DataValueField = "seizure_accused_details_id";
                    ddlAccusedName.DataBind();
                    ddlAccusedName.Items.Insert(0, "Select");
                    ddlAccusedName.Enabled = false;
                    //CalendarExtender1.StartDate = DateTime.Now;
                    //CalendarExtender2.StartDate = DateTime.Now;
                    txtBailNo.Text = obj.bailno;
                    txtBailRequestDate.Text = obj.bailrequestdate;
                    txtBailGrantedDate.Text = obj.bailgranteddate;
                    txtBailer.Text = obj.bailer;
                    txtReasonForGrant.Text = obj.bailreason;
                    ddlNameoftheCourt.SelectedValue = obj.court_master_code?.ToString()?? string.Empty;
                    ddlAccusedName.SelectedValue = obj.seizure_accused_details_id?.ToString() ?? string.Empty;
                    ddlBailType.SelectedValue = obj.bail_type_master_code?.ToString() ?? string.Empty;
                    CalendarExtender1.SelectedDate = Convert.ToDateTime(obj.bailrequestdate);
                    CalendarExtender2.SelectedDate = Convert.ToDateTime(obj.bailgranteddate);
                    if (obj.bailgranted == "Y")
                    {
                        rdYes.Checked = true;
                    }
                    else
                        rdNo.Checked = true;

                    if ((Session["rtype"].ToString() == "1"))
                    {
                        ddlAccusedName.DataSource = accusedDetails.ToList();
                        ddlAccusedName.DataTextField = "accusedname";
                        ddlAccusedName.DataValueField = "seizure_accused_details_id";
                        ddlAccusedName.DataBind();
                        ddlAccusedName.Items.Insert(0, "Select");
                        //CalendarExtender1.StartDate = DateTime.Now;
                        //CalendarExtender2.StartDate = DateTime.Now;
                        btnCancel.Visible = false;
                        btnSaveasDraft.Visible = false;
                        btnSubmit.Visible = false;
                        rdYes.Enabled = false;
                        rdNo.Enabled= false;
                        txtBailNo.Attributes.Add("disabled", "disabled");
                        txtBailRequestDate.Attributes.Add("disabled", "disabled");
                        txtBailGrantedDate.Attributes.Add("disabled", "disabled");
                        txtBailer.Attributes.Add("disabled", "disabled");
                        txtReasonForGrant.Attributes.Add("disabled", "disabled");
                        ddlNameoftheCourt.Attributes.Add("disabled", "disabled");
                        ddlAccusedName.Enabled = false;
                        ddlBailType.Attributes.Add("disabled", "disabled");
                        Image1.Visible = false;
                        Image2.Visible = false;
                    }
                }
                else
                {
                    ddlAccusedName.DataSource = accc.ToList();
                    ddlAccusedName.DataTextField = "accusedname";
                    ddlAccusedName.DataValueField = "seizure_accused_details_id";
                    ddlAccusedName.DataBind();
                    ddlAccusedName.Items.Insert(0, "Select");
                    //CalendarExtender1.StartDate = DateTime.Now;
                    //CalendarExtender2.StartDate = DateTime.Now;
                }
            }
        }
    

        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Response.Redirect("BailList");
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
               
                cm_seiz_Bail cm_obj = new cm_seiz_Bail();
                cm_obj.seizureno = Convert.ToInt32(Session["seizureNo"].ToString());
                cm_obj.bailno = txtBailNo.Text;
                cm_obj.bailrequestdate = txtBailRequestDate.Text;
                cm_obj.bailgranteddate = txtBailGrantedDate.Text;
                DateTime dt2 = DateTime.ParseExact(cm_obj.bailrequestdate, "dd-MM-yyyy", null);
                DateTime dt1 = DateTime.ParseExact(cm_obj.bailgranteddate, "dd-MM-yyyy", null);
                DateTime dt3 = DateTime.ParseExact(txtfirdate.Text, "dd-MM-yyyy", null);
                int cmp = dt2.CompareTo(dt1);
                int cmp1 = dt3.CompareTo(dt2);

                if (cmp > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the bail granted Date is greater than or eaqual to the bail request Date.\');", true);
                    return;
                    // date1 is greater means date1 is comes after date2
                }
                if (cmp1 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the bail request Date is greater than or eaqual to the FIR Date.\');", true);
                    return;
                    // date1 is greater means date1 is comes after date2
                }
                //else if (cmp == 0)
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the bail granted Date is greater than to the bail request Date.\');", true);
                //    // date1 is greater means date1 is comes after date2
                //}
                else
                {
                    btnSubmit.Enabled = false;
                    cm_obj.bailer = txtBailer.Text;
                    cm_obj.bailreason = txtReasonForGrant.Text;
                    cm_obj.raidby = Session["RaidBy"].ToString().Substring(0, 1).ToUpper();
                    cm_obj.court_master_code = (ddlNameoftheCourt.SelectedValue.ToString());
                    cm_obj.seizure_accused_details_id = ddlAccusedName.SelectedValue.ToString();
                    cm_obj.bail_type_master_code = ddlBailType.SelectedValue;
                    string strHostName = System.Net.Dns.GetHostName();
                    string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                    cm_obj.ipaddress = clientIPAddress.ToString();
                    if (rdYes.Checked)
                        cm_obj.bailgranted = "Y";
                    else
                        cm_obj.bailgranted = "N";

                    cm_obj.user_id = Session["UserID"].ToString();
                    cm_obj.record_status = "Y";
                    cm_obj.record_deleted = "N";

                    bool val;
                    string tempTableId = Session["tableId"]?.ToString() ?? string.Empty;
                    if (Session["rtype"].ToString() == "0" && string.IsNullOrEmpty(tempTableId))
                        val = BL_cm_seiz_Bail.InsertSeiz_Bail(cm_obj);
                    else
                    {
                        cm_obj.seizure_bail_id = Convert.ToInt32(Session["tableId"].ToString());
                        val = BL_cm_seiz_Bail.Update(cm_obj);
                    }
                    if (val == true)
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("BailList");
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
               
                cm_seiz_Bail cm_obj = new cm_seiz_Bail();
                cm_obj.bailrequestdate = txtBailRequestDate.Text;
                cm_obj.bailgranteddate = txtBailGrantedDate.Text;

                DateTime dt2 = DateTime.ParseExact(cm_obj.bailrequestdate, "dd-MM-yyyy", null);
                DateTime dt1 = DateTime.ParseExact(cm_obj.bailgranteddate, "dd-MM-yyyy", null);
                DateTime dt3 = DateTime.ParseExact(txtfirdate.Text, "dd-MM-yyyy", null);
                int cmp = dt2.CompareTo(dt1);
                int cmp1 = dt3.CompareTo(dt2);
              

                if (cmp>0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the bail granted Date is greater than or eaqual to the bail request Date.\');", true);
                    return;
                    // date1 is greater means date1 is comes after date2
                }
                if (cmp1 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the bail request Date is greater than or eaqual to the FIR Date.\');", true);
                    return;
                    // date1 is greater means date1 is comes after date2
                }
                else
                {
                    btnSaveasDraft.Enabled = false;
                    cm_obj.seizureno = Convert.ToInt32(Session["seizureNo"].ToString());
                    cm_obj.bailno = txtBailNo.Text;
                    cm_obj.bailrequestdate = txtBailRequestDate.Text;
                    cm_obj.bailgranteddate = txtBailGrantedDate.Text;

                    cm_obj.bailer = txtBailer.Text;
                    cm_obj.bailreason = txtReasonForGrant.Text;

                    cm_obj.court_master_code = (ddlNameoftheCourt.SelectedValue.ToString());
                    cm_obj.seizure_accused_details_id = ddlAccusedName.SelectedValue.ToString();
                    cm_obj.bail_type_master_code = ddlBailType.SelectedValue;
                    string strHostName = System.Net.Dns.GetHostName();
                    string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                    cm_obj.ipaddress = clientIPAddress.ToString();
                    if (rdYes.Checked)
                        cm_obj.bailgranted = "Y";
                    else
                        cm_obj.bailgranted = "N";

                    cm_obj.user_id = Session["UserID"].ToString();
                    cm_obj.record_status = "N";
                    cm_obj.record_deleted = "N";
                    cm_obj.raidby = Session["RaidBy"].ToString().Substring(0, 1).ToUpper();

                    bool val;
                    string tempTableId = Session["tableId"]?.ToString() ?? string.Empty;
                    if (Session["rtype"].ToString() == "0" && string.IsNullOrEmpty(tempTableId))
                        val = BL_cm_seiz_Bail.InsertSeiz_Bail(cm_obj);
                    else
                    {
                        cm_obj.seizure_bail_id = Convert.ToInt32(Session["tableId"].ToString());
                        val = BL_cm_seiz_Bail.Update(cm_obj);
                    }
                    if (val == true)
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("BailList");
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
            Response.Redirect("~/BailList");
        }
        protected void btnAccusedBailHistory_Click(object sender, EventArgs e)
        {

            Response.Redirect("BailSubList.aspx");
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