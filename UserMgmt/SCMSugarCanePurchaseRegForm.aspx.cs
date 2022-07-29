using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;
using AjaxControlToolkit;
using System.Web.Services;

namespace UserMgmt
{
   
    public partial class SCMSugarCanePurchaseRegForm : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        List<Party_Master> partymasters = new List<Party_Master>();
        static  SugarCanePurchase scp = new SugarCanePurchase();
        static string entrydate;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strPreviousPage = "";
                txtDate.ReadOnly = true;
                CalendarExtender.EndDate = DateTime.Now;
               
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    CalendarExtender.EndDate = DateTime.Now;
                    Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
                    Session["UserID"] = Session["UserID"];
                    partymasters = new List<Party_Master>();
                    partymasters = BL_Party_Master.GetList();
                    var list = from s in partymasters
                               where s.party_code != "ALL"
                               select s;
                    ddlPartyName.DataSource = list.ToList();
                    ddlPartyName.DataTextField = "party_name";
                    ddlPartyName.DataValueField = "party_code";
                    ddlPartyName.DataBind();
                    ddlPartyName.Items.Insert(0, "Select");
                    rtype.Value = Session["rtype"].ToString();
                    if (ViewState["Records"] == null)
                    {
                        dt.Columns.Add("Doc_Name");
                        dt.Columns.Add("Discription");
                        dt.Columns.Add("Doc_Path");
                        dt.Columns.Add("Doc_id");

                        ViewState["Records"] = dt;
                    }
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    Session["financial_year"] = user.financial_year;
                    if (Session["UserID"].ToString() == "Admin")
                    {

                        btnSaveasDraft.Visible = true;
                        btnSubmit.Visible = true;
                        btnCancel.Visible = true;
                        //btnApprove.Visible = true;
                        //btnReject.Visible = true;
                    }
                    else if (user.role_name == "Bond Officer")
                    {

                        btnSaveasDraft.Visible = false;
                        btnSubmit.Visible = false;
                        btnCancel.Visible = false;
                        btnApprove.Visible = true;
                        btnReject.Visible = true;
                        ddlPartyName.SelectedValue = user.party_code;
                        ddlPartyName.Enabled = false;
                        string value = BL_SugarCanePurchase.GetPedning(user.party_code, Session["financial_year"].ToString());
                        txtPending.Text = value;
                        txtpending1.Value = value;
                        txtPending.ReadOnly = true;
                    }
                    else
                    {
                        ddlPartyName.SelectedValue = user.party_code;
                        btnSaveasDraft.Visible = true;
                        btnSubmit.Visible = true;
                        btnCancel.Visible = true;
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        ddlPartyName.Enabled = false;
                        string value = BL_SugarCanePurchase.GetPedning(user.party_code, Session["financial_year"].ToString());
                        txtPending.Text = value;
                        txtpending1.Value = value;
                        txtPending.ReadOnly = true;
                        approverremaks.Visible = false;

                    }
                    if (Session["rtype"].ToString() != "0")
                    {
                        scp = new SugarCanePurchase();
                        scp = BL_SugarCanePurchase.GetDetails(Convert.ToInt32(Session["scp_id"].ToString()), Session["scpfinancial_year"].ToString());
                        ddlPartyName.SelectedValue = scp.party_code;
                        //  ddlFinancialYear.SelectedValue = scp.financialyear;
                        txtDate.Text = scp.entrydate;
                        CalendarExtender.StartDate = Convert.ToDateTime(txtDate.Text);
                        txtdob.Value = scp.entrydate;
                        txtTotal.Value = scp.total_purchase.ToString();
                        textcanecrushed.Text = scp.total_canecrushed.ToString();
                        textFOE.Text = scp.ownestate_purchase.ToString();
                        textpfg.Text = scp.factorygate_purchase.ToString();
                        textpfs.Text = scp.outstation_purchase.ToString();
                        txtRemarks1.Text = scp.remarks;
                        textTotal.Text = scp.total_purchase.ToString();
                        entrydate = scp.entrydate.Substring(0, 10);
                        if (textcanecrushed.Text == "0")
                            textcanecrushed.Text = "";
                        //   txtPending.Text =(Convert.ToDouble(txtPending.Text) - scp.total_purchase).ToString();
                        txtpending1.Value = (Convert.ToDouble(txtPending.Text) - scp.total_purchase).ToString();
                        if (scp.record_status == "N"|| scp.record_status == "R")
                        {
                            canecrushed.Value = textcanecrushed.Text;
                        }
                    
                        for (int i = 0; i < scp.docs.Count; i++)
                        {
                            if (i == 0)
                                dummytable.Visible = false;
                            dt = (DataTable)ViewState["Records"];
                            dt.Rows.Add(scp.docs[i].doc_name, scp.docs[i].description, scp.docs[i].doc_path, scp.docs[i].id);
                            grdAdd.DataSource = dt;
                            grdAdd.DataBind();
                        }
                        List<All_Approvals> approvals = new List<All_Approvals>();
                        approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), scp.sugarcanepurchase_id.ToString(), "SCP");
                        var list4 = (from s in approvals
                                     where s.financial_year == Session["scpfinancial_year"].ToString()
                                     select s);
                        grdApprovalDetails.DataSource = list4.ToList();
                        grdApprovalDetails.DataBind();
                        if (scp.record_status == "Y" || user.role_name == "Bond Officer")
                        {
                            foreach (GridViewRow dr1 in grdAdd.Rows)
                            {
                                ImageButton btn = dr1.FindControl("ImageButton1") as ImageButton;
                                btn.Visible = false;
                            }

                            Image1.Visible = false;
                            CalendarExtender.Enabled = false;
                        }
                        if ((Session["rtype"].ToString() == "1"))
                        {
                            if (scp.record_status == "A" || scp.record_status == "Y" || user.role_name == "Bond Officer")
                            {
                                txtApproverremarks.ReadOnly = true;
                                btnApprove.Visible = false;
                                btnReject.Visible = false;
                                //  grdAdd.Columns[grdAdd.Columns.Count - 1].Visible = false;
                                foreach (GridViewRow dr1 in grdAdd.Rows)
                                {
                                    ImageButton btn = dr1.FindControl("ImageButton1") as ImageButton;
                                    btn.Visible = false;
                                }
                                Image1.Visible = false; 
                                txtRemarks1.ReadOnly = true;

                            }
                            txtPending.ReadOnly = true;
                          //  txtDate.ReadOnly = true;
                            ddlPartyName.Enabled = false;
                            ddlPartyName.Enabled = false;
                            textcanecrushed.ReadOnly = true;
                            textFOE.ReadOnly = true;
                            textpfg.ReadOnly = true;
                            textpfs.ReadOnly = true;
                            docs.Visible = false;
                            txtRemarks1.ReadOnly = true;
                            approverremaks.Visible = false;
                            //   txtApproverremarks.Attributes.Add("disabled", "disabled");
                            if (user.role_name != "Bond Officer")
                            {
                                btnSaveasDraft.Visible = false;
                                btnSubmit.Visible = false;
                                btnCancel.Visible = false;

                                foreach (GridViewRow dr1 in grdAdd.Rows)
                                {
                                    ImageButton btn = dr1.FindControl("ImageButton1") as ImageButton;
                                    btn.Visible = false;
                                }
                                Image1.Visible = false; CalendarExtender.Enabled = false;
                            }
                            if (user.role_name == "Bond Officer" && scp.record_status == "Y")
                            {
                                approverremaks.Visible = true;
                                btnApprove.Visible = true;
                                btnReject.Visible = true;
                                txtApproverremarks.ReadOnly = false;
                            }
                        }
                    }
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                }
                if (strPreviousPage == "" || System.Web.HttpContext.Current.Session["UserID"] == null)
                {
                    Response.Redirect("~/LoginPage");
                }
                
            }
        }
     
        int Doc_id = 1;
        protected void UploadFile(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (idupDocument.HasFile)
                {

                    dummytable.Visible = false;
                    string fileName = Path.GetFileName(idupDocument.PostedFile.FileName);
                    FileInfo fi = new FileInfo(fileName);
                    string[] filetype = fileName.Replace("'", "").Split('.');
                    string m = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-sss");
                    idupDocument.PostedFile.SaveAs(Server.MapPath("~/Eascm_Docs/") + fi.Name.Replace(fi.Extension, "").Replace(",", "_").Replace(" ", "_") + "_" + m + fi.Extension);
                    string path = Server.MapPath("~/Eascm_Docs/") + fi.Name.Replace(fi.Extension, "") + "_" + m + fi.Extension;
                    dt = (DataTable)ViewState["Records"];
                    dt.Rows.Add(fileName, txtDiscription.Text, path, Doc_id);
                    grdAdd.DataSource = dt;
                    grdAdd.DataBind();
                    Doc_id++;
                    txtDiscription.Text = "";
                   

                }
            }
           
        }
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ImageButton lb = (ImageButton)sender;
                GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
                string filePath = (sender as ImageButton).CommandArgument;
                //File.Delete(Server.MapPath("~/Eascm_Docs/" + Path.GetFileName(filePath)));
                //FileInfo fInfoEvent;
                //fInfoEvent = new FileInfo(Path.GetFileName(filePath));
                //fInfoEvent.Delete();
                string a = Session["rtype"].ToString();
                string v = Path.GetFileName(filePath).ToString();
                if (Session["rtype"].ToString() != "0")
                {
                    bool value = BL_User_Mgnt.DeletefileEascm("eascm_docs", Session["scp_id"].ToString(), v, Session["scpparty_code"].ToString());
                    if (value)
                    {
                        File.Delete(Server.MapPath("~/Eascm_Docs/" + Path.GetFileName(filePath)));
                        FileInfo fInfoEvent;
                        fInfoEvent = new FileInfo(Path.GetFileName(filePath));
                        fInfoEvent.Delete();
                    }
                }
                else
                {
                    File.Delete(Server.MapPath("~/Eascm_Docs/" + Path.GetFileName(filePath)));
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
        protected void btnVATtansfers_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATTransferList");
        }
        protected void DownloadFile(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string filePath = (sender as ImageButton).CommandArgument;
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                Response.WriteFile(Server.MapPath("~/Eascm_Docs/" + Path.GetFileName(filePath)));
                Response.End();
            }
        }
        protected void DeleteFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            File.Delete(filePath);
            Response.Redirect(Request.Url.AbsoluteUri);
        }
       
      

        protected void btnShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("SCMSugarCanePurchaseRegList");
        }
        protected void btnRG4_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("SCMSugarCanePurchaseRegList");
        }

        protected void btnDMP_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DailyMolassesProductionList");
        }

        protected void btnMIR_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("MolassesIssueRegisterList");
        }

        protected void btnOpeningBalance_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("OpeningBalanceList");
        }

      

        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                btnSaveasDraft.Enabled = false;
                if (textcanecrushed.Text == "")
                    textcanecrushed.Text = "0";
                if (txtTotal.Value == "")
                    txtTotal.Value = "0";
                if (Convert.ToDouble(txtTotal.Value) > 0 || Convert.ToDouble(textcanecrushed.Text)>0)
                {
                    SugarCanePurchase scp = new SugarCanePurchase();
                    scp.party_code = ddlPartyName.SelectedValue;
                    scp.financialyear = Session["financial_year"].ToString();
                    scp.entrydate = Convert.ToDateTime(txtdob.Value).ToString("dd-MM-yyyy");
                    if (textpfg.Text == "")
                        textpfg.Text = "0";
                    scp.factorygate_purchase = Convert.ToDouble(textpfg.Text);
                    if (textpfs.Text == "")
                        textpfs.Text = "0";
                        scp.outstation_purchase = Convert.ToDouble(textpfs.Text);
                    if (textFOE.Text == "")
                        textFOE.Text = "0";
                        scp.ownestate_purchase = Convert.ToDouble(textFOE.Text);
                    scp.total_purchase = Convert.ToDouble(txtTotal.Value);
                    if(textcanecrushed.Text!="")
                    scp.total_canecrushed = Convert.ToDouble(textcanecrushed.Text);
                    scp.record_type = Convert.ToInt32(Session["rtype"].ToString());
                    scp.remarks = txtRemarks1.Text;
                    scp.record_status = "N";
                    scp.user_id = Session["UserId"].ToString();
                    int i = 0;
                    scp.docs = new List<EASCM_DOCS>();
                    dt = ViewState["Records"] as DataTable;
                    foreach (DataRow dr in dt.Rows)
                    {
                        EASCM_DOCS doc = new EASCM_DOCS();
                        doc.doc_name = dr["Doc_Name"].ToString();
                        doc.doc_path = (grdAdd.Rows[i].FindControl("lblFilePath") as Label).Text;
                        doc.description = (grdAdd.Rows[i].FindControl("lblDiscriptione") as Label).Text;
                        scp.docs.Add(doc);
                        i++;
                    }


                    string val;
                    if (Session["rtype"].ToString() == "0")
                        val = BL_SugarCanePurchase.Insert(scp);
                    else
                    {
                        scp.sugarcanepurchase_id = Convert.ToInt32(Session["scp_id"].ToString());
                        val = BL_SugarCanePurchase.Update(scp);
                    }
                    if (val == "0")
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("SCMSugarCanePurchaseRegList");
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
                else
               
                {

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append("Please Check the Values");
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                }

            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                btnSubmit.Enabled = false;
                if (textcanecrushed.Text == "")
                    textcanecrushed.Text = "0";
                if (txtTotal.Value == "")
                    txtTotal.Value = "0";
                if (Convert.ToDouble(txtTotal.Value) > 0 || Convert.ToDouble(textcanecrushed.Text) > 0)
                {
                    SugarCanePurchase scp = new SugarCanePurchase();
                    scp.party_code = ddlPartyName.SelectedValue;
                    scp.financialyear = Session["financial_year"].ToString();
                    scp.entrydate = Convert.ToDateTime(txtdob.Value).ToString("dd-MM-yyyy");
                    if (textpfg.Text == "")
                        textpfg.Text = "0";
                    scp.factorygate_purchase = Convert.ToDouble(textpfg.Text);
                    if (textpfs.Text == "")
                        textpfs.Text = "0";
                    scp.outstation_purchase = Convert.ToDouble(textpfs.Text);
                    if (textFOE.Text == "")
                        textFOE.Text = "0";
                    scp.ownestate_purchase = Convert.ToDouble(textFOE.Text);
                    scp.total_purchase = Convert.ToDouble(txtTotal.Value);
                    if (textcanecrushed.Text != "")
                        scp.total_canecrushed = Convert.ToDouble(textcanecrushed.Text);
                    scp.record_type = Convert.ToInt32(Session["rtype"].ToString());
                    scp.remarks = txtRemarks1.Text;
                    scp.record_status = "Y";
                    scp.user_id = Session["UserId"].ToString();
                    int i = 0;
                    scp.docs = new List<EASCM_DOCS>();
                    dt = ViewState["Records"] as DataTable;
                    foreach (DataRow dr in dt.Rows)
                    {
                        EASCM_DOCS doc = new EASCM_DOCS();
                        doc.doc_name = dr["Doc_Name"].ToString();
                        doc.doc_path = (grdAdd.Rows[i].FindControl("lblFilePath") as Label).Text;
                        doc.description = (grdAdd.Rows[i].FindControl("lblDiscriptione") as Label).Text;
                        scp.docs.Add(doc);
                        i++;
                    }


                    string val;
                    if (Session["rtype"].ToString() == "0")
                        val = BL_SugarCanePurchase.Insert(scp);
                    else
                    {
                        scp.sugarcanepurchase_id = Convert.ToInt32(Session["scp_id"].ToString());
                        val = BL_SugarCanePurchase.Update(scp);
                    }
                    if (val == "0")
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("SCMSugarCanePurchaseRegList");
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
                else

                {

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append("Please Check the Values");
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                }

            }
        }
        [WebMethod]
        public static string chkDuplicateDates(Object scpdate)
        {
            int value = 0;
            if (scpdate.ToString().Length >10)
            {

                if (entrydate != scpdate.ToString().Substring(0, 10))
                    value = BL_User_Mgnt.GetExistsData("sugarcanepurchase", "entrydate", scpdate.ToString());
            }
            return value.ToString();
        }
        
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("SCMSugarCanePurchaseRegList");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (idupDocument.HasFile)
                {

                    dummytable.Visible = false;
                    string fileName = Path.GetFileName(idupDocument.PostedFile.FileName);
                    string[] filetype = fileName.Split('.');
                    string m = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-sss");
                    idupDocument.PostedFile.SaveAs(Server.MapPath("~/Eascm_Docs/") + filetype[0] + "_" + m + "." + filetype[1]);
                    string path = Server.MapPath("~/Eascm_Docs/") + filetype[0] + "_" + m + "." + filetype[1];
                    dt = (DataTable)ViewState["Records"];
                    dt.Rows.Add(fileName, txtDiscription.Text, path, Doc_id);
                    grdAdd.DataSource = dt;
                    grdAdd.DataBind();
                    Doc_id++;
                    txtDiscription.Text = "";

                }
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                SugarCanePurchase scp = new SugarCanePurchase();
                scp.party_code = ddlPartyName.SelectedValue;
                scp.record_status = "A";
                string val;
                scp.sugarcanepurchase_id = Convert.ToInt32(Session["scp_id"].ToString());
                scp.financialyear = Session["financial_year"].ToString();
                scp.remarks = txtApproverremarks.Text;
                scp.user_id = Session["UserID"].ToString();
                scp.party_code = Session["scpparty_code"].ToString();
                val = BL_SugarCanePurchase.Approve(scp);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("SCMSugarCanePurchaseRegList");
                }
                else
                {

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

        protected void btnReject_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                SugarCanePurchase scp = new SugarCanePurchase();
                scp.party_code = ddlPartyName.SelectedValue;
                scp.record_status = "R";
                string val;
                scp.sugarcanepurchase_id = Convert.ToInt32(Session["scp_id"].ToString());
                scp.remarks = txtApproverremarks.Text;
                scp.financialyear = Session["financial_year"].ToString();
                scp.party_code = Session["scpparty_code"].ToString();
                scp.user_id = Session["UserID"].ToString();
                val = BL_SugarCanePurchase.Approve(scp);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("SCMSugarCanePurchaseRegList");
                }
                else
                {

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
}