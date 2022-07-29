using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class ChargesheetForm : System.Web.UI.Page
    {
        List<cm_seiz_ChargeSheet> _witness = new List<cm_seiz_ChargeSheet>();
        static List<cm_seiz_AccusedDetails> ads = new List<cm_seiz_AccusedDetails>();

        DataTable dt = new DataTable();        
        int Doc_id = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                List<cm_designation> _designation = new List<cm_designation>();
                _designation = BL_cm_designation.GetList();
                //ddlModeDisposal.DataSource = _designation;
                //ddlModeDisposal.DataTextField = "designation_name";
                //ddlModeDisposal.DataValueField = "designation_code";
                //ddlModeDisposal.DataBind();
                //ddlModeDisposal.Items.Insert(0, "Select");

                if (ViewState["Records"] == null)
                {
                    dt.Columns.Add("Doc_Name");
                    dt.Columns.Add("Discription");
                    dt.Columns.Add("Doc_Path");
                    dt.Columns.Add("Doc_id");

                    ViewState["Records"] = dt;
                }
                //List<cm_disposal_of_property> disposal_of_property = new List<cm_disposal_of_property>();
                //disposal_of_property = BL_cm_disposal_of_property.GetList();
                //ddlModeDisposal.DataSource = disposal_of_property;
                //ddlModeDisposal.DataTextField = "disposal_of_property_code";
                //ddlModeDisposal.DataValueField = "disposal_of_property_code";
                //ddlModeDisposal.DataBind();
                //ddlModeDisposal.Items.Insert(0, "Select");
                Session["rtype1"] = Session["rtype"];
                string seizureNo = Session["seizureNo"].ToString();
                //Get FirNo
                cm_seiz_FIR firDetails = BL_CM_Common.GetPRFIRNo(seizureNo+"&"+Session["raidby"].ToString());
                if (firDetails.prfirno != null)
                { 
                    txtPRFIRNo.Text = firDetails.prfirno.Trim(); 
                    txtfirdate.Text= firDetails.prfirdate.Trim();
                    CalendarExtender1.StartDate =Convert.ToDateTime(firDetails.prfirdate.Trim()) ;
                }

               
                //ads = BL_cm_seiz_AccusedDetails.GetDetails(seizureNo);

                //var ad = (from s in ads
                //          where s.seizureno == Convert.ToInt32(seizureNo) 
                //          select s);
                //grdAccusedDetailsListView.DataSource = ad.ToList();
                //grdAccusedDetailsListView.DataBind();

                if (Session["rtype"].ToString() != "0")
                {
                    string tableId = Session["tableId"].ToString();

                    cm_seiz_ChargeSheet obj = new cm_seiz_ChargeSheet();
                    obj = BL_cm_seiz_ChargeSheet.GetDetailsById(tableId);
                    if(obj.chargesheet_date!=null && obj.chargesheet_date!="")
                    CalendarExtender1.SelectedDate = Convert.ToDateTime(obj.chargesheet_date);
                    else
                        CalendarExtender1.SelectedDate = Convert.ToDateTime(firDetails.prfirdate.Trim());
                    txtMemoofEvidence.Text = obj.evidenceproof;
                    txtSeizedPropertyKept.Text = obj.placeof_seizedpropertykept;
                    txtCourtorderDate.Text = obj.producedatcourt_date;
                    txtDateProductionSeizerProperty.Text= obj.chargesheet_date; 
                    ddlModeDisposal.SelectedValue = obj.disposalmode_code.ToString();
                    txtRemarks.Text = obj.chargesheet_remarks;
                    ddvehicle.SelectedValue = obj.vehicle_verification;
                    ddvehiclefsl.SelectedValue = obj.vehicle_fsl;
                    ddliquore.SelectedValue = obj.liquor_test;
                    ddliquorefsl.SelectedValue = obj.liquor_fsl;
                    Doc_id = 0;
                    for (int i = 0; i < obj.docs.Count; i++)
                    {
                        if (i == 0)
                            dummytable.Visible = false;
                        dt = (DataTable)ViewState["Records"];
                        dt.Rows.Add(obj.docs[i].doc_name, obj.docs[i].description, obj.docs[i].doc_path, obj.docs[i].seizure_docs_id);
                        grdAdd.DataSource = dt;
                        grdAdd.DataBind();
                        Doc_id++;

                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (obj.record_status == "Y")
                        {
                            (grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = true;
                            (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = true;
                            //btnSubmit.Visible = false;
                            //idupDocument.Enabled = false;
                            //txtDiscription.Enabled = false;
                            //btnUpload.Enabled = false;
                        }
                        if ((Session["rtype"].ToString() == "1"))
                        {
                            (grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = true;
                            (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                        }
                    }

                    if ((Session["rtype"].ToString() == "1"))
                    {
                        btnCancel.Visible = false;
                        btnSaveasDraft.Visible = false;
                        btnSubmit.Visible = false;
                        btnSubmit.Visible = false;
                        dummytable.Visible = false;
                      //  txtCourtorderDate.Enabled = false;
                        idupDocument.Enabled = false;
                        txtDiscription.Enabled = false;
                        txtCourtorderDate.Attributes.Add("disabled", "disabled");
                        Image2.Visible = false;
                        Image3.Visible = false;
                        btnUpload.Enabled = false;
                        txtDateProductionSeizerProperty.Attributes.Add("disabled", "disabled");
                        txtMemoofEvidence.Attributes.Add("disabled", "disabled");
                        txtSeizedPropertyKept.Attributes.Add("disabled", "disabled");
                      //  txtCourtorderDate.Enabled = false;
                        ddlModeDisposal.Attributes.Add("disabled", "disabled");
                        txtRemarks.Attributes.Add("disabled", "disabled");
                      //  grdAccusedDetailsListView.Columns[0].Visible = false;
                        docs.Visible = false;
                    }
                }
            }
        }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChargesheetList");
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                btnSubmit.Enabled = false;
                cm_seiz_ChargeSheet cm_obj = new cm_seiz_ChargeSheet();
                cm_obj.seizureno = Convert.ToInt32(Session["seizureNo"].ToString());
                cm_obj.finalseizureno = Session["seizureNo"].ToString();
                cm_obj.evidenceproof = txtMemoofEvidence.Text;
                cm_obj.placeof_seizedpropertykept = txtSeizedPropertyKept.Text;// txtDateProductionSeizerProperty.Text;
                cm_obj.chargesheet_date = txtDateProductionSeizerProperty.Text;
                cm_obj.producedatcourt_date = txtCourtorderDate.Text;
                cm_obj.raidby = Session["RaidBy"].ToString().Substring(0, 1).ToUpper();
                //cm_obj.producedatcourt_date = txtCourtorderDate.Text;
                cm_obj.disposalmode_code = Convert.ToInt32(ddlModeDisposal.SelectedValue.ToString());
                cm_obj.chargesheet_remarks = txtRemarks.Text;
                cm_obj.user_id = Session["UserID"].ToString();
                cm_obj.vehicle_verification = ddvehicle.SelectedValue;
                if (cm_obj.vehicle_verification == "Yes")
                    cm_obj.vehicle_fsl = ddvehiclefsl.SelectedValue;
                cm_obj.liquor_test = ddliquore.SelectedValue;
                if (cm_obj.liquor_test == "Yes")
                    cm_obj.liquor_fsl = ddliquorefsl.SelectedValue;
                cm_obj.record_status = "Y";
                cm_obj.record_deleted = "N";
                string strHostName = System.Net.Dns.GetHostName();
                string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                cm_obj.ipaddress = clientIPAddress.ToString();
                int i = 0;
                cm_obj.docs = new List<Seizure_Docs>();
                dt = ViewState["Records"] as DataTable;
                foreach (DataRow dr in dt.Rows)
                {
                    Seizure_Docs doc = new Seizure_Docs();
                    doc.seizureno = cm_obj.seizureno.ToString();
                    doc.doc_name = dr["Doc_Name"].ToString();
                    doc.doc_path = (grdAdd.Rows[i].FindControl("lblFilePath") as Label).Text;
                    doc.description = (grdAdd.Rows[i].FindControl("lblDiscriptione") as Label).Text;
                    cm_obj.docs.Add(doc);
                    i++;
                }
                DateTime dt3 = Convert.ToDateTime("01/01/0001 00:00:00");
                int cmp = 0;
                if (cm_obj.producedatcourt_date != "")
                    dt3 = DateTime.ParseExact(cm_obj.producedatcourt_date, "dd-MM-yyyy", null);
                DateTime dt4 = DateTime.ParseExact(txtfirdate.Text, "dd-MM-yyyy", null);
                DateTime dt2 = DateTime.ParseExact(cm_obj.chargesheet_date, "dd-MM-yyyy", null);
                if (cm_obj.producedatcourt_date != "")
                    cmp = dt4.CompareTo(dt3);
                int cmp1 = dt4.CompareTo(dt2);
                if (cmp > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the  Date is greater than or equal to the FIR Date.\');", true);
                    return;

                    // date1 is greater means date1 is comes after date2
                }
                if (cmp1 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the  Date is greater than or equal to the FIR Date.\');", true);
                    return;

                    // date1 is greater means date1 is comes after date2
                }
                //Saving Accused Details
                //cm_obj.accuseDetailsList = new List<cm_seiz_AccusedDetails>();
                //for (int ad = 0; ad < grdAccusedDetailsListView.Rows.Count; ad++)
                //{
                //    bool value = (grdAccusedDetailsListView.Rows[ad].FindControl("chSelect") as CheckBox).Checked;

                //    if (value)
                //    {
                //        cm_seiz_AccusedDetails accDetails = new cm_seiz_AccusedDetails();

                //        accDetails.seizure_accused_details_id = Convert.ToInt32((grdAccusedDetailsListView.Rows[ad].FindControl("lblAccusedID") as Label).Text);
                //        accDetails.chargesheet_status = "Y";
                //        cm_obj.accuseDetailsList.Add(accDetails);
                //    }
                //}
                //if (cm_obj.accuseDetailsList.Count == 0)
                //{
                //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //    sb.Append("<script type = 'text/javascript'>");
                //    sb.Append("window.onload=function(){");
                //    sb.Append("alert('");
                //    sb.Append("Select at least one Accused details");
                //    sb.Append("')};");
                //    sb.Append("</script>");
                //    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                //    return;
                //}
                else
                {
                    bool val;
                    string tempTableId = Session["tableId"]?.ToString() ?? string.Empty;
                    if (Session["rtype"].ToString() == "0" && string.IsNullOrEmpty(tempTableId))
                        val = BL_cm_seiz_ChargeSheet.InsertSeiz_ChargeSheet(cm_obj);
                    else
                    {
                        cm_obj.seizure_chargesheet_id = Convert.ToInt32(Session["tableId"].ToString());
                        val = BL_cm_seiz_ChargeSheet.Update(cm_obj);
                    }
                    if (val == true)
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("ChargesheetList");
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
                btnSaveasDraft.Enabled = false;
                cm_seiz_ChargeSheet cm_obj = new cm_seiz_ChargeSheet();
                cm_obj.seizureno = Convert.ToInt32(Session["seizureNo"].ToString());
                cm_obj.finalseizureno = Session["seizureNo"].ToString();
                cm_obj.evidenceproof = txtMemoofEvidence.Text;
                cm_obj.placeof_seizedpropertykept = txtSeizedPropertyKept.Text;// txtDateProductionSeizerProperty.Text;
                cm_obj.chargesheet_date = txtDateProductionSeizerProperty.Text;
                cm_obj.producedatcourt_date = txtCourtorderDate.Text;
                //cm_obj.producedatcourt_date = txtCourtorderDate.Text;
                cm_obj.disposalmode_code = Convert.ToInt32(ddlModeDisposal.SelectedValue.ToString());
                cm_obj.chargesheet_remarks = txtRemarks.Text;
                cm_obj.vehicle_verification = ddvehicle.SelectedValue;
                if (cm_obj.vehicle_verification == "Yes")
                    cm_obj.vehicle_fsl = ddvehiclefsl.SelectedValue;
                cm_obj.liquor_test = ddliquore.SelectedValue;
                if (cm_obj.liquor_test == "Yes")
                    cm_obj.liquor_fsl = ddliquorefsl.SelectedValue;
                cm_obj.user_id = Session["UserID"].ToString();
                cm_obj.record_status = "N";
                cm_obj.record_deleted = "N";
                cm_obj.raidby = Session["RaidBy"].ToString().Substring(0, 1).ToUpper();
                string strHostName = System.Net.Dns.GetHostName();
                string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                cm_obj.ipaddress = clientIPAddress.ToString();
                int i = 0;
                cm_obj.docs = new List<Seizure_Docs>();
                dt = ViewState["Records"] as DataTable;
                foreach (DataRow dr in dt.Rows)
                {
                    Seizure_Docs doc = new Seizure_Docs();
                    doc.seizureno = cm_obj.seizureno.ToString();
                    doc.doc_name = dr["Doc_Name"].ToString();
                    doc.doc_path = (grdAdd.Rows[i].FindControl("lblFilePath") as Label).Text;
                    doc.description = (grdAdd.Rows[i].FindControl("lblDiscriptione") as Label).Text;
                    cm_obj.docs.Add(doc);
                    i++;
                }
                DateTime dt3=Convert.ToDateTime("01/01/0001 00:00:00");
                int cmp = 0;
                if (cm_obj.producedatcourt_date!="")
                dt3 = DateTime.ParseExact(cm_obj.producedatcourt_date, "dd-MM-yyyy", null);
                DateTime dt4 = DateTime.ParseExact(txtfirdate.Text, "dd-MM-yyyy", null);
                DateTime dt2 = DateTime.ParseExact(cm_obj.chargesheet_date, "dd-MM-yyyy", null);
                if (cm_obj.producedatcourt_date != "")
                     cmp = dt4.CompareTo(dt3);
                int cmp1 = dt4.CompareTo(dt2);
                if (cmp > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the  Date is greater than or equal to the FIR Date.\');", true);
                    return;

                    // date1 is greater means date1 is comes after date2
                }
               if (cmp1 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the  Date is greater than or equal to the FIR Date.\');", true);
                    return;

                    // date1 is greater means date1 is comes after date2
                }
                else
                {
                    //Saving Accused Details
                    //cm_obj.accuseDetailsList = new List<cm_seiz_AccusedDetails>();
                    //for (int ad = 0; ad < grdAccusedDetailsListView.Rows.Count; ad++)
                    //{
                    //    bool value = (grdAccusedDetailsListView.Rows[ad].FindControl("chSelect") as CheckBox).Checked;


                    //        cm_seiz_AccusedDetails accDetails = new cm_seiz_AccusedDetails();

                    //        accDetails.seizure_accused_details_id = Convert.ToInt32((grdAccusedDetailsListView.Rows[ad].FindControl("lblAccusedID") as Label).Text);
                    //    if (value)

                    //        accDetails.chargesheet_status = "Y";
                    //    else
                    //        accDetails.chargesheet_status = "N";
                    //    cm_obj.accuseDetailsList.Add(accDetails);

                    //}
                    //if (cm_obj.accuseDetailsList.Count == 0)
                    //{
                    //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    //    sb.Append("<script type = 'text/javascript'>");
                    //    sb.Append("window.onload=function(){");
                    //    sb.Append("alert('");
                    //    sb.Append("Select at least one Accused details");
                    //    sb.Append("')};");
                    //    sb.Append("</script>");
                    //    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    //    return;
                    //}

                    bool val;
                    string tempTableId = Session["tableId"]?.ToString() ?? string.Empty;
                    if (Session["rtype"].ToString() == "0" && string.IsNullOrEmpty(tempTableId))
                        val = BL_cm_seiz_ChargeSheet.InsertSeiz_ChargeSheet(cm_obj);
                    else
                    {
                        cm_obj.seizure_chargesheet_id = Convert.ToInt32(Session["tableId"].ToString());
                        val = BL_cm_seiz_ChargeSheet.Update(cm_obj);
                    }
                    if (val == true)
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("ChargesheetList");
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
            Response.Redirect("~/ChargesheetList");
        }
        protected void btnChargesheetFiling_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ChargesheetList");
        }
        protected void btnADDAccusedDetails_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ADDAccusedDetailsList");
        }
        protected void UploadFile(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                //if (idupDocument.HasFile)
                //{
                dummytable.Visible = false;
                string fileName = Path.GetFileName(idupDocument.PostedFile.FileName);
                string[] filetype = fileName.Split('.');
                string m = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-sss");
                idupDocument.PostedFile.SaveAs(Server.MapPath("~/CM_Docs/") + filetype[0] + "_" + m + "." + filetype[1]);
                string path = Server.MapPath("~/CM_Docs/") + filetype[0] + "_" + m + "." + filetype[1];
                dt = (DataTable)ViewState["Records"];
                dt.Rows.Add(fileName, txtDiscription.Text, path, Doc_id);
                grdAdd.DataSource = dt;
                grdAdd.DataBind();
                Doc_id++;
                txtDiscription.Text = "";
                // }
            }
        }
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ImageButton lb = (ImageButton)sender;
                GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
                string filePath = (sender as ImageButton).CommandArgument;
                string a = Session["rtype1"].ToString();
                string v = Path.GetFileName(filePath).ToString();
                if (Session["rtype1"].ToString() != "0")
                {
                    bool value = BL_User_Mgnt.Deletefile("seizure_docs", Session["seizureNo"].ToString(), v);
                    if (value)
                    {
                        File.Delete(Server.MapPath("~/CM_Docs/" + Path.GetFileName(filePath)));
                        FileInfo fInfoEvent;
                        fInfoEvent = new FileInfo(Path.GetFileName(filePath));
                        fInfoEvent.Delete();
                    }
                }
                else
                {
                    File.Delete(Server.MapPath("~/CM_Docs/" + Path.GetFileName(filePath)));
                    FileInfo fInfoEvent;
                    fInfoEvent = new FileInfo(Path.GetFileName(filePath));
                    fInfoEvent.Delete();

                }
                DataTable dt2 = (DataTable)ViewState["Records"];
                ViewState["CurrentTable"] = dt2;
                int rowID = gvRow.RowIndex;
                DataTable dt1 = ViewState["Records"] as DataTable;
                dt1.Rows[rowID].Delete();
                ViewState["dt"] = dt1;
                grdAdd.DataSource = dt1;
                grdAdd.DataBind();
                if (dt1.Rows.Count < 1)
                    dummytable.Visible = true;
            }
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string filePath = (sender as ImageButton).CommandArgument;
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                Response.WriteFile(Server.MapPath("~/CM_Docs/" + Path.GetFileName(filePath)));
                Response.End();
            }
        }
        protected void DeleteFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            File.Delete(filePath);
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        protected void btnSeizure_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            List<Reportmaster> reports = new List<Reportmaster>();
            reports = BL_WorkFlow.GetReports(Session["UserID"].ToString());
            Session["ReportId"] = "CM_SEIZURE_DETAILS";
            Session["SeizureNo"]= Session["seizureNo"].ToString();
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
          //  Session["SeizureNo"] = Session["seizureNo"].ToString();
            Session["Raidby"] = Session["RaidBy"];
            Session["UserID"] = Session["UserID"].ToString();
            Session["seizureno"] = Session["seizureNo"].ToString();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('ReportPage' ,'_blank');", true);

        }
    }
}