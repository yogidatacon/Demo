using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;
using System.Data;
using System.IO;

namespace UserMgmt
{
    public partial class AccusedStatementForm : System.Web.UI.Page
    {
        #region Variables
        List<cm_seiz_trial> obj = new List<cm_seiz_trial>();
        DataTable dt = new DataTable();
        int Doc_id = 1;
        #endregion Variables
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ViewState["Records"] == null)
                {
                    dt.Columns.Add("Doc_Name");
                    dt.Columns.Add("Discription");
                    dt.Columns.Add("Doc_Path");
                    dt.Columns.Add("Doc_id");

                    ViewState["Records"] = dt;
                }
                Session["rtype1"] = Session["rtype"];

                string seizureNo = Session["seizureNo"]?.ToString() ?? string.Empty;
                //Get FirNo
                cm_seiz_FIR firDetails = BL_CM_Common.GetPRFIRNo(seizureNo+"&"+Session["RaidBy"].ToString());
                if (firDetails.prfirno != null)
                {
                    txtPRFIRNo.Text = firDetails.prfirno.Trim();
                    txtfirdate.Text = firDetails.prfirdate.Trim();
                    string datelock = BL_CM_Common.GetDate("exciseautomation.seizure_trial where seizureNo='" + seizureNo + "' and trialstage_code='5'", "currentstagedate");
                    CalendarExtender1.StartDate = Convert.ToDateTime(datelock.Trim());
                    CalendarExtender2.StartDate = Convert.ToDateTime(datelock.Trim());
                }
                if (Session["rtype"].ToString() != "0")
                {
                    
                    string tableId = Session["tableId"].ToString();

                    cm_seiz_trial obj = new cm_seiz_trial();
                    obj = BL_cm_seiz_trial.GetDetailsById(tableId);

                    txtAccusedStatementDate.Text = obj.currentstagedate;
                    txtNexthearingDate.Text = obj.nexthearingdate;
                    CalendarExtender1.SelectedDate = Convert.ToDateTime(obj.currentstagedate);
                    CalendarExtender2.SelectedDate = Convert.ToDateTime(obj.nexthearingdate);
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
                            //(grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = false;
                            (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                        }
                    }

                    if ((Session["rtype"].ToString() == "1"))
                    {
                        btnCancel.Visible = false;
                        btnSaveasDraft.Visible = false;
                        btnSubmit.Visible = false;
                        txtAccusedStatementDate.Attributes.Add("disabled", "disabled");
                        txtNexthearingDate.Attributes.Add("disabled", "disabled");
                        txtPRFIRNo.Attributes.Add("disabled", "disabled");
                        docs.Visible = false;
                        Image1.Visible = false;
                        Image2.Visible = false;

                    }
                }
            }
        }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {

            Response.Redirect("AccusedStatementList");
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                btnSubmit.Enabled = false;
                cm_seiz_trial cm_obj = new cm_seiz_trial();
                cm_obj.seizureno = Convert.ToInt32(Session["seizureNo"].ToString());
                cm_obj.trialstage_code = 6;
                cm_obj.currentstage = 6;
                cm_obj.currentstagedate = txtAccusedStatementDate.Text;
                cm_obj.nexthearingdate = txtNexthearingDate.Text;
                cm_obj.finalseizureno = (Session["seizureNo"].ToString());
                cm_obj.lastmodified_date = DateTime.Now.ToShortDateString();
                cm_obj.creation_date = DateTime.Now.ToShortDateString();
                cm_obj.user_id = Session["UserID"].ToString();
                cm_obj.record_status = "Y";
                cm_obj.record_deleted = "N";
                cm_obj.raidby = Session["RaidBy"].ToString().Substring(0, 1);
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
                DateTime dt2 = DateTime.ParseExact(cm_obj.currentstagedate, "dd-MM-yyyy", null);
                DateTime dt1 = DateTime.ParseExact(cm_obj.nexthearingdate, "dd-MM-yyyy", null);

                int cmp = dt2.CompareTo(dt1);

                if (cmp > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the Next hearing Date should be  greater than or equal to the Accused Statement Date.\');", true);
                    return;
                    // date1 is greater means date1 is comes after date2
                }
                else
                {
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
                        Response.Redirect("AccusedStatementList");
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
                cm_seiz_trial cm_obj = new cm_seiz_trial();
                cm_obj.currentstagedate = txtAccusedStatementDate.Text;
                cm_obj.nexthearingdate = txtNexthearingDate.Text;
                DateTime dt2 = DateTime.ParseExact(cm_obj.currentstagedate, "dd-MM-yyyy", null);
                DateTime dt1 = DateTime.ParseExact(cm_obj.nexthearingdate, "dd-MM-yyyy", null);

                int cmp = dt2.CompareTo(dt1);

                if (cmp > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the Next hearing Date should be  greater than or equal to the Accused Statement Date.\');", true);
                    return;
                    // date1 is greater means date1 is comes after date2
                }
                else
                {
                    cm_obj.seizureno = Convert.ToInt32(Session["seizureNo"].ToString());
                    cm_obj.trialstage_code = 6;
                    cm_obj.currentstage = 6;
                    cm_obj.currentstagedate = txtAccusedStatementDate.Text;
                    cm_obj.nexthearingdate = txtNexthearingDate.Text;
                    cm_obj.finalseizureno = (Session["seizureNo"].ToString());
                    cm_obj.lastmodified_date = DateTime.Now.ToShortDateString();
                    cm_obj.creation_date = DateTime.Now.ToShortDateString();
                    cm_obj.user_id = Session["UserID"].ToString();
                    cm_obj.record_status = "N";
                    cm_obj.record_deleted = "N";
                    cm_obj.raidby = Session["RaidBy"].ToString().Substring(0, 1);
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
                        Response.Redirect("AccusedStatementList");
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
            Response.Redirect("~/AccusedStatementList");
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

        #region FileLoad
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

        #endregion FielLoad     
    }
}