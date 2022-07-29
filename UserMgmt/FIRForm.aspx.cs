using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class FIRForm : System.Web.UI.Page
    {
        List<cm_seiz_FIR> _witness = new List<cm_seiz_FIR>();
        static List<cm_seiz_AccusedDetails> ads = new List<cm_seiz_AccusedDetails>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<cm_designation> _designation = new List<cm_designation>();
                _designation = BL_cm_designation.GetList();
                ddlDesignation.DataSource = _designation;
                ddlDesignation.DataTextField = "designation_name";
                ddlDesignation.DataValueField = "designation_code";
                ddlDesignation.DataBind();
                ddlDesignation.Items.Insert(0, "Select");
                string raidby = "";
                if (Session["RaidBy"].ToString().Substring(0, 1).ToUpper() == "E")
                    raidby = "E";
                else
                    raidby = "P";
                string strRtype = Session["rtype"]?.ToString() ?? string.Empty;
                string seizureNo = Session["seizureNo"].ToString();
                ads = BL_cm_seiz_AccusedDetails.GetDetails(seizureNo+"&"+raidby);
               
                var ad = (from s in ads
                          where s.seizureno == Convert.ToInt32(seizureNo)
                          select s);
                grdAccusedDetailsListView.DataSource = ad.ToList();
                grdAccusedDetailsListView.DataBind();

                //CalendarExtender.StartDate = DateTime.Now;
                //CalendarExtender1.StartDate = DateTime.Now;
                //CalendarExtender2.StartDate = DateTime.Now;
                //CalendarExtender3.StartDate = DateTime.Now;
                //CalendarExtender4.StartDate = DateTime.Now;
                //txtFIRDate.Attributes.Add("disabled", "disabled");             
                //txtManualBookDate.Attributes.Add("disabled", "disabled");
                //txtComplaintDate.Attributes.Add("disabled", "disabled");
                //txtcourt.Attributes.Add("disabled", "disabled");
             

                if (strRtype != "0")
                   // if (Session["rtype"].ToString() != "0")
                    {
                    
                    string tableId = Session["tableId"].ToString();

                    cm_seiz_FIR obj = new cm_seiz_FIR();
                    obj = BL_cm_seiz_FIR.GetDetailsById(tableId);
                    CalendarExtender.SelectedDate = Convert.ToDateTime(obj.prfirdate);
                    CalendarExtender1.SelectedDate = Convert.ToDateTime(obj.manualbookdate);
                    CalendarExtender2.SelectedDate = Convert.ToDateTime(obj.complaintdate);
                    CalendarExtender3.SelectedDate = Convert.ToDateTime(obj.infotocourtdate);
                   
                    //txtRaidBy.Text = obj.raidby;
                    txtRaidOrderBy.Text = obj.raidorderby;
                    ddlDesignation.SelectedValue = obj.designation_code;
                    txtPRFIRNo.Text = obj.prfirno;
                    txtFIRDate.Text = obj.prfirdate;//obj.prfirdate;
                    txtdob.Value= obj.prfirdate.ToString();
                    txtManualBookPRFIR.Text = obj.manualprfirno;
                    txtManualBookDate.Text = obj.manualbookdate;//obj.manualbookdate;
                    txtdor.Value= obj.manualbookdate;
                    txtOfficialComplaintNo.Text = obj.complaintno;
                    txtComplaintDate.Text = obj.complaintdate; //obj.complaintdate;
                    txtgpd.Value = obj.complaintdate;
                    txtcourt.Text = obj.infotocourtdate;
                    txtinfo.Value= obj.infotocourtdate;
                  
                    if ((Session["rtype"].ToString() == "1"))
                    {
                        btnCancel.Visible = false;
                        btnSaveasDraft.Visible = false;
                        btnSubmit.Visible = false;
                        Image1.Visible = false;
                        Image2.Visible = false;
                        Image3.Visible = false;
                        Image4.Visible = false;
                        //idupDocument.Enabled = false;
                        //txtDiscription.Enabled = false;
                        //btnUpload.Enabled = false;
                        //txtRaidBy.Attributes.Add("disabled", "disabled");
                        txtRaidOrderBy.Attributes.Add("disabled", "disabled");
                        ddlDesignation.Attributes.Add("disabled", "disabled");
                        txtPRFIRNo.Attributes.Add("disabled", "disabled");
                        txtFIRDate.Attributes.Add("disabled", "disabled");
                        txtManualBookPRFIR.Attributes.Add("disabled", "disabled");
                        txtManualBookDate.Attributes.Add("disabled", "disabled");
                        txtOfficialComplaintNo.Attributes.Add("disabled", "disabled");
                        txtComplaintDate.Attributes.Add("disabled", "disabled");
                        txtcourt.Attributes.Add("disabled", "disabled");
                        grdAccusedDetailsListView.Attributes.Add("disabled", "disabled");
                       // grdAccusedDetailsListView.Columns[0].Visible = false;
                    }
                }
            }
        }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("FIRList");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                cm_seiz_FIR cm_obj = new cm_seiz_FIR();
                cm_obj.seizureno = Convert.ToInt32(Session["seizureNo"].ToString());
                cm_obj.raidby = Session["RaidBy"].ToString().Substring(0, 1).ToUpper(); //txtRaidBy.Text;
                cm_obj.raidorderby = txtRaidOrderBy.Text;
                cm_obj.designation_code = ddlDesignation.SelectedValue;
                cm_obj.prfirno = txtPRFIRNo.Text;
                cm_obj.prfirdate = txtFIRDate.Text;
                cm_obj.manualprfirno = txtManualBookPRFIR.Text;
                cm_obj.manualbookdate = txtManualBookDate.Text;//txtdor.ToString();
                cm_obj.complaintdate = txtComplaintDate.Text;
                cm_obj.prfirdate = txtFIRDate.Text;
                cm_obj.manualbookdate = txtManualBookDate.Text;
                cm_obj.infotocourtdate = txtcourt.Text;
                DateTime dt2 = DateTime.ParseExact(cm_obj.prfirdate, "dd-MM-yyyy", null);
                DateTime dt1 = DateTime.ParseExact(cm_obj.manualbookdate, "dd-MM-yyyy", null);
                DateTime dt3 = DateTime.ParseExact(cm_obj.complaintdate, "dd-MM-yyyy", null);
                DateTime dt4 = DateTime.ParseExact(cm_obj.infotocourtdate, "dd-MM-yyyy", null);

                int cmp = dt3.CompareTo(dt2);
                int cmp1 = dt3.CompareTo(dt1);
                int cmp2 = dt3.CompareTo(dt4);

                int cmp22 = dt2.CompareTo(dt4);

                if (cmp > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the FIR Date is greater than or equal to the Complaint Date.\');", true);
                    return;

                    // date1 is greater means date1 is comes after date2
                }
                if (cmp1 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the ManualBook Date is greater than or equal to the Complaint Date.\');", true);
                    return;
                    // date1 is greater means date1 is comes after date2
                }
                if (cmp2 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the Information set to Court  Date is greater than or equal to the Complaint Date.\');", true);
                    return;
                    // date1 is greater means date1 is comes after date2
                }
                if (cmp22 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the Information set to Court  Date is greater than or equal to the FIR Date.\');", true);
                    return;
                    // date1 is greater means date1 is comes after date2
                }
                else
                {
                    btnSubmit.Enabled = false;
                    cm_obj.infotocourtdate = txtcourt.Text;
                    cm_obj.user_id = Session["UserID"].ToString();
                    cm_obj.record_status = "Y";
                    cm_obj.record_deleted = "N";
                    cm_obj.finalseizureno = Session["seizureNo"].ToString();
                    string strHostName = System.Net.Dns.GetHostName();
                    string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                    cm_obj.ipaddress = clientIPAddress.ToString();
                    cm_obj.accuseDetailsList = new List<cm_seiz_AccusedDetails>();
                   
                    for (int i = 0; i < grdAccusedDetailsListView.Rows.Count; i++)
                    {
                        //bool value = (grdAccusedDetailsListView.Rows[i].FindControl("chSelect") as CheckBox).Checked;


                        cm_seiz_AccusedDetails accDetails = new cm_seiz_AccusedDetails();

                        accDetails.seizure_accused_details_id = Convert.ToInt32((grdAccusedDetailsListView.Rows[i].FindControl("lblAccusedID") as Label).Text);
                        //if (value)
                        //    accDetails.fir_status = "Y";
                        //else
                        //    accDetails.fir_status = "N";
                        cm_obj.accuseDetailsList.Add(accDetails);

                    }
                    bool val;
                    string tempTableId = Session["tableId"]?.ToString() ?? string.Empty;
                    if (Session["rtype"].ToString() == "0" && string.IsNullOrEmpty(tempTableId))
                        val = BL_cm_seiz_FIR.InsertSeiz_FIR(cm_obj);
                    else
                    {
                        cm_obj.seizure_fir_id = Convert.ToInt32(Session["tableId"].ToString());
                        val = BL_cm_seiz_FIR.Update(cm_obj);
                    }
                    if (val == true)
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("FIRList");
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
                cm_seiz_FIR cm_obj = new cm_seiz_FIR();
                cm_obj.complaintdate = txtComplaintDate.Text;
                cm_obj.prfirdate = txtFIRDate.Text;
                cm_obj.manualbookdate = txtManualBookDate.Text;
                cm_obj.infotocourtdate = txtcourt.Text;
                DateTime dt2 = DateTime.ParseExact(cm_obj.prfirdate, "dd-MM-yyyy", null);
                DateTime dt1 = DateTime.ParseExact(cm_obj.manualbookdate, "dd-MM-yyyy", null);
                DateTime dt3 = DateTime.ParseExact(cm_obj.complaintdate, "dd-MM-yyyy", null);
                DateTime dt4 = DateTime.ParseExact(cm_obj.infotocourtdate, "dd-MM-yyyy", null);

                int cmp = dt3.CompareTo(dt2);
                int cmp1 = dt3.CompareTo(dt1);
                int cmp2 = dt3.CompareTo(dt4);
                int cmp22 = dt2.CompareTo(dt4);

                if (cmp > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the FIR Date is greater than or equal to the Complaint Date.\');", true);
                    return;

                    // date1 is greater means date1 is comes after date2
                }
                if (cmp1 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the ManualBook Date is greater than or equal to the Complaint Date.\');", true);
                    return;
                    // date1 is greater means date1 is comes after date2
                }
                if (cmp2 > 0 )
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the Information set to Court  Date is greater than or equal to the Complaint Date.\');", true);
                    return;
                    // date1 is greater means date1 is comes after date2
                }
                if (cmp22 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the Information set to Court  Date is greater than or equal to the FIR Date.\');", true);
                    return;
                    // date1 is greater means date1 is comes after date2
                }
                else
                {
                    btnSaveasDraft.Enabled = false;
                    cm_obj.seizureno = Convert.ToInt32(Session["seizureNo"].ToString());
                    cm_obj.raidby = Session["RaidBy"].ToString().Substring(0,1).ToUpper();// txtRaidBy.Text;
                    cm_obj.raidorderby = txtRaidOrderBy.Text;
                    cm_obj.designation_code = ddlDesignation.SelectedValue;
                    cm_obj.prfirno = txtPRFIRNo.Text;
                    cm_obj.prfirdate = txtFIRDate.Text;
                    //  cm_obj.prfirdate = txtdob.Value;
                    cm_obj.manualprfirno = txtManualBookPRFIR.Text;
                    cm_obj.manualbookdate = txtManualBookDate.Text;
                    cm_obj.complaintno = txtOfficialComplaintNo.Text;
                    cm_obj.complaintdate = txtComplaintDate.Text;
                    cm_obj.infotocourtdate = txtcourt.Text;
                    string strHostName = System.Net.Dns.GetHostName();
                    string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                    cm_obj.ipaddress = clientIPAddress.ToString();
                    //cm_obj.seizureno = Convert.ToInt32(Session["seizureNo"].ToString());
                    //cm_obj.raidby = Session["RaidBy"].ToString(); //txtRaidBy.Text;
                    //cm_obj.raidorderby = txtRaidOrderBy.Text;
                    //cm_obj.designation_code = ddlDesignation.SelectedValue;
                    //cm_obj.prfirno = txtPRFIRNo.Text;
                    //cm_obj.prfirdate = txtFIRDate.Text;
                    //cm_obj.manualprfirno = txtManualBookPRFIR.Text;
                    //cm_obj.manualbookdate = txtdor.Value;
                    //cm_obj.complaintno = txtOfficialComplaintNo.Text;
                    //cm_obj.complaintdate = txtComplaintDate.Text;
                    //cm_obj.infotocourtdate = txtcourt.Text;
                    cm_obj.user_id = Session["UserID"].ToString();
                    cm_obj.record_status = "N";
                    cm_obj.record_deleted = "N";
                    cm_obj.finalseizureno = Session["seizureNo"].ToString();
                    //DateTime prfirdate = Convert.ToDateTime(cm_obj.prfirdate);
                    //DateTime complaintdate = Convert.ToDateTime(cm_obj.complaintdate);

                    //else if (cmp < 0)
                    //{
                    //    // date2 is greater means date1 is comes after date1
                    //}
                    //else
                    //{
                    //    // date1 is same as date2
                    //}

                    //if (prfirdate >= complaintdate)
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the FIR Date is greater than or equal to the Complaint Date.\');", true);
                    //    txtFIRDate.Focus();
                    //    txtFIRDate.Text = "";
                    //}


                    //Saving Accused Details
                    cm_obj.accuseDetailsList = new List<cm_seiz_AccusedDetails>();
                    for (int i = 0; i < grdAccusedDetailsListView.Rows.Count; i++)
                    {
                        //bool value = (grdAccusedDetailsListView.Rows[i].FindControl("chSelect") as CheckBox).Checked;


                        cm_seiz_AccusedDetails accDetails = new cm_seiz_AccusedDetails();

                        accDetails.seizure_accused_details_id = Convert.ToInt32((grdAccusedDetailsListView.Rows[i].FindControl("lblAccusedID") as Label).Text);
                        //if (value)
                        //    accDetails.fir_status = "Y";
                        //else
                        //    accDetails.fir_status = "N";
                        cm_obj.accuseDetailsList.Add(accDetails);

                    }
                    
                    bool val;
                    string tempTableId = Session["tableId"]?.ToString() ?? string.Empty;
                    if (Session["rtype"].ToString() == "0" && string.IsNullOrEmpty(tempTableId))
                        val = BL_cm_seiz_FIR.InsertSeiz_FIR(cm_obj);
                    else
                    {
                        cm_obj.seizure_fir_id = Convert.ToInt32(Session["tableId"].ToString());
                        val = BL_cm_seiz_FIR.Update(cm_obj);
                    }
                    if (val == true)
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("FIRList");
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
            Response.Redirect("~/FIRList");
        }

        protected void btnFIRRegistration_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("FIRList");
           
        }
        protected void btnADDAccusedDetails_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ADDAccusedDetailsList");
          

        }
        protected void LinkButton1_Click(object sender, EventArgs e)
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