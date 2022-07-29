using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class SCMDailyMolassesProduction : System.Web.UI.Page
    {
        DataTable vats = new DataTable();
        List<DailyMolassesProduction_e> production1 = new List<DailyMolassesProduction_e>();
        DailyMolassesProduction_e product = new DailyMolassesProduction_e();
        List<Party_Master> party = new List<Party_Master>();
        List<DalyMolasses_e> parties = new List<DalyMolasses_e>();
        DataTable dt = new DataTable();
        UserDetails user = new UserDetails();
        int Doc_id ;
      //  string document = "";
      //  string discription = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];

                    string userid = Session["UserID"].ToString();
                    user = BL_UserDetails.GetUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        Session["financial_year"] = user.financial_year;
                        party = new List<Party_Master>();
                        party = BL_Party_Master.GetList();
                        CalendarExtender.EndDate = DateTime.Now;
                        //if (ViewState["vats"] == null)
                        //{

                        //    vats.Columns.Add("dailymolassesproduction_id");
                        //    vats.Columns.Add("vat_type_code");
                        //    vats.Columns.Add("vat_type_name");
                        //    vats.Columns.Add("vat_code");
                        //    vats.Columns.Add("vat_name");
                        //    vats.Columns.Add("storage_content");
                        //    vats.Columns.Add("record_status");
                        //    vats.Columns.Add("Total_Capacity");
                        //    vats.Columns.Add("txtTodaysProd");
                        //    vats.Columns.Add("uom_code");
                        //    vats.Columns.Add("uom_name");
                        //    ViewState["vats"] = vats;
                        //}
                        if (Session["UserID"].ToString() == "Admin")
                        {
                            var partynames = (from s in party
                                              where s.party_code != "ALL"
                                              select new { party_name = s.party_name, party_code = s.party_code }).Distinct();
                            ddlpartyname.DataSource = partynames;
                            ddlpartyname.DataTextField = "party_name";
                            ddlpartyname.DataValueField = "party_code";
                            ddlpartyname.DataBind();
                            ddlpartyname.Items.Insert(0, "Select");

                            grdDailyMolassesProduction.Enabled = true;
                            btnSaveasDraft.Visible = true;
                            btnSubmit.Visible = true;
                            btnCancel.Visible = true;
                            btnApprove.Visible = false;
                            btnReject.Visible = false;
                            approverremarks.Visible = false;
                        }
                        else if (user.role_name == "Bond Officer")
                        {

                            btnSaveasDraft.Visible = false;
                            btnSubmit.Visible = false;
                            btnCancel.Visible = false;
                            btnApprove.Visible = true;
                            btnReject.Visible = true;
                            var partynames = (from s in party
                                              where s.party_code == user.party_code
                                              select new { party_name = s.party_name, party_code = s.party_code }).Distinct();
                            ddlpartyname.DataSource = partynames;
                            ddlpartyname.DataTextField = "party_name";
                            ddlpartyname.DataValueField = "party_code";
                            ddlpartyname.DataBind();
                            ddlpartyname.Items.Insert(0, "Select");
                            ddlpartyname.SelectedValue = user.party_code;
                        }
                        else
                        {
                            var partynames = (from s in party
                                              where s.party_code == user.party_code
                                              select new { party_name = s.party_name, party_code = s.party_code }).Distinct();
                            ddlpartyname.DataSource = partynames;
                            ddlpartyname.DataTextField = "party_name";
                            ddlpartyname.DataValueField = "party_code";
                            ddlpartyname.DataBind();
                            ddlpartyname.Items.Insert(0, "Select");
                            ddlpartyname.SelectedValue = user.party_code;
                            grdDailyMolassesProduction.Enabled = true;
                            ddlpartyname.Enabled = false;
                            btnSaveasDraft.Visible = true;
                            btnSubmit.Visible = true;
                            btnCancel.Visible = true;
                            btnApprove.Visible = false;
                            btnReject.Visible = false;
                            approverremarks.Visible = false;
                        }
                        if (ViewState["Records"] == null)
                        {
                            dt.Columns.Add("Status");
                            dt.Columns.Add("Doc_Name");
                            dt.Columns.Add("Description");
                            dt.Columns.Add("Doc_Path");
                            dt.Columns.Add("Doc_id");
                            ViewState["Records"] = dt;
                        }
                        if (Session["rtype"].ToString() != "0")
                        {
                            txtdob1.Value = Session["date"].ToString();
                            CalendarExtender.SelectedDate = Convert.ToDateTime(txtdob1.Value);
                            GetVATS();



                        }
                        else
                        {
                            GetVATS();
                        }
                    }
                    else
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append("Database Server Not Connecting");
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    }
                }
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/LoginPage");
                }
            }
        }
        protected void txtDate(object sender, EventArgs e)
        {
            //if (IsPostBack)
            //{
            //    string userid = Session["UserID"].ToString();
            //    user = BL_UserDetails.GetUser(Session["UserID"].ToString());
            //    production1 = new List<DailyMolassesProduction_e>();
            //    if (userid != "Admin")
            //    {
            //        production1 = BL_DailyMolassesProduction.GetProduction(user.party_code);
            //        ddlpartyname.SelectedValue = user.party_code;
            //        ddlpartyname.Attributes.Add("disabled", "disabled");
            //        grdDailyMolassesProduction.DataSource = production1;
            //        grdDailyMolassesProduction.DataBind();
            //    }
            //    else
            //    {
            //        production1 = BL_DailyMolassesProduction.GetProduction(ddlpartyname.SelectedValue);
            //        grdDailyMolassesProduction.DataSource = production1;
            //        grdDailyMolassesProduction.DataBind();
            //    }
            //}
        }
        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DailyMolassesProductionList");
        }
        protected void txtTodaysProduction(object sender, EventArgs e)
        {
            //TextBox textBox = sender as TextBox;
            //GridViewRow gvr = (GridViewRow)textBox.NamingContainer;
            //string capacity = (gvr.Cells[5].FindControl("lbltotalcapacity") as Label).Text;
            //string TodaysProd = (gvr.Cells[6].FindControl("txtTodaysProd") as TextBox).Text;
            //if (Convert.ToDecimal(capacity) < Convert.ToDecimal(TodaysProd))
            //{
            //    (gvr.Cells[6].FindControl("txtTodaysProd") as TextBox).Text = "0";
            //    string script = "alert(\"Production cannot be greater than VAT storage capacity !!!!!\");";
            //    ScriptManager.RegisterStartupScript(this, GetType(),
            //                          "ServerControlScript", script, true);
            //}
        }

        protected void btnVATtansfers_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATTransferList");
        }
        protected void UploadFile(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (idupDocument.HasFile)
                {

                    dummytable.Visible = false;
                    string fileName = System.IO.Path.GetFileName(idupDocument.PostedFile.FileName);
                    FileInfo fi = new FileInfo(fileName);
                    string[] filetype = fileName.Replace("'", "").Split('.');
                    string m = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-sss");
                    idupDocument.PostedFile.SaveAs(Server.MapPath("~/Eascm_Docs/") + fi.Name.Replace(fi.Extension, "").Replace(",", "_").Replace(" ", "_") + "_" + m + fi.Extension);
                    string path = Server.MapPath("~/Eascm_Docs/") + fi.Name.Replace(fi.Extension, "") + "_" + m + fi.Extension;
                    dt = (DataTable)ViewState["Records"];
                    dt.Rows.Add("",fileName, txtDiscription.Text, path, Doc_id);
                    grdAdd.DataSource = dt;
                    grdAdd.DataBind();
                    for (int i2 = 0; i2 < dt.Rows.Count; i2++)
                    {
                        if ((grdAdd.Rows[i2].FindControl("lblStatus") as Label).Text == "1")
                        {
                            if (Session["rtype"].ToString() != "1")
                                (grdAdd.Rows[i2].FindControl("ImageButton1") as ImageButton).Visible = true;
                            else
                                (grdAdd.Rows[i2].FindControl("ImageButton1") as ImageButton).Visible = false;
                        }
                    }
                    Doc_id++;
                    txtDiscription.Text = "";
                    Session["document"] = idupDocument.ToString();
                    Session["discription"] = txtDiscription.Text;

                }
            }

        }
        [WebMethod]
        public static string chkDuplicateDates(Object scpdate)
        {
            int value = 0;
           
            value = BL_User_Mgnt.GetExistsData("dailymolassesproduction", "entrydate", scpdate.ToString());
            
            return value.ToString();
        }

        protected void DeleteFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            File.Delete(filePath);
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void DownloadFile(object sender, ImageClickEventArgs e)
        {
            if (IsPostBack)
            {
                string filePath = (sender as ImageButton).CommandArgument;
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(filePath));
                Response.WriteFile(Server.MapPath("~/Eascm_Docs/" + System.IO.Path.GetFileName(filePath)));
                Response.End();
            }
        }

        protected void btnRemove_Click(object sender, ImageClickEventArgs e)
        {
            if (IsPostBack)
            {
                ImageButton lb = (ImageButton)sender;
                GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
                string filePath = (sender as ImageButton).CommandArgument;
                //File.Delete(Server.MapPath("~/Eascm_Docs/" + System.IO.Path.GetFileName(filePath)));
                //FileInfo fInfoEvent;
                //fInfoEvent = new FileInfo(System.IO.Path.GetFileName(filePath));
                //fInfoEvent.Delete();
                string a = Session["rtype"].ToString(); 
                string v = Path.GetFileName(filePath).ToString();
                if (Session["rtype"].ToString() != "0")
                {
                    bool value = BL_User_Mgnt.DeletefileEascm("eascm_docs", Session["dailymolassesproduction_id"].ToString(), v, Session["Party_code"].ToString());
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
                for (int i2 = 0; i2 < dt1.Rows.Count; i2++)
                {
                    if ((grdAdd.Rows[i2].FindControl("lblStatus") as Label).Text == "1")
                    {
                        if(Session["rtype"].ToString() != "1")
                         (grdAdd.Rows[i2].FindControl("ImageButton1") as ImageButton).Visible = true;
                        else
                         (grdAdd.Rows[i2].FindControl("ImageButton1") as ImageButton).Visible = false;
                    }
                }
                if (dt1.Rows.Count < 1)
                    dummytable.Visible = true;
            }
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DailyMolassesProductionList");
        }

        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                btnSaveasDraft.Enabled = false;
                if (grdDailyMolassesProduction.Rows.Count > 0)
                {
                    string user_id = Session["UserID"].ToString();

                    production1 = new List<DailyMolassesProduction_e>();
                    for (int i = 0; i < grdDailyMolassesProduction.Rows.Count; i++)
                    {
                        DailyMolassesProduction_e prod1 = new DailyMolassesProduction_e();
                        GridViewRow row = grdDailyMolassesProduction.Rows[i];
                        prod1.dailymolassesproduction_id = (row.Cells[8].FindControl("lbldailymolassesproduction_id") as Label).Text;
                        if ((row.Cells[6].FindControl("txtTodaysProd") as TextBox).Text != "")
                            prod1.dailyproduction = Convert.ToDouble((row.Cells[6].FindControl("txtTodaysProd") as TextBox).Text);
                        else
                            prod1.dailyproduction = 0;
                        prod1.brix = (row.Cells[7].FindControl("txtTodaysBrix") as TextBox).Text;
                        prod1.vat_code = (row.Cells[0].FindControl("lblvatcode") as Label).Text;
                        prod1.vat_name = (row.Cells[1].FindControl("lblvatname") as Label).Text;
                        prod1.storage_content = (row.Cells[2].FindControl("lblstorage_content") as Label).Text;
                        prod1.uom_code = (row.Cells[3].FindControl("lbluom_code") as Label).Text;
                        prod1.vat_totalcapacity = Convert.ToDouble((row.Cells[5].FindControl("txttotalcapacity") as TextBox).Text);
                        prod1.creation_date = txtdob1.Value;
                        prod1.party_code = ddlpartyname.SelectedValue;
                        prod1.record_status = "N";
                       prod1.financial_year= Session["financial_year"].ToString();
                        prod1.user_id = Session["UserID"].ToString();
                        prod1.remarks = txtRemarks1.Text;
                        //else
                        // prod1.record_status = "N";
                        if (i == 0)
                        {
                            prod1.docs = new List<EASCM_DOCS>();
                            for (int j = 0; j < grdAdd.Rows.Count; j++)
                            {
                                EASCM_DOCS doc = new EASCM_DOCS();
                                doc.doc_name = (grdAdd.Rows[j].FindControl("lblFileName") as Label).Text;
                                doc.doc_path = (grdAdd.Rows[j].FindControl("lblFilePath") as Label).Text;
                                doc.description = (grdAdd.Rows[j].FindControl("lblDiscriptione") as Label).Text;
                                prod1.docs.Add(doc);
                                // prod1.remarks = txtRemarks1.Text;
                            }
                        }

                        production1.Add(prod1);

                    }
                    //  user = BL_UserDetails.GetUser(Session["UserID"].ToString());
                    string val;
                    if (Session["rtype"].ToString() == "0")
                        val = BL_DailyMolassesProduction.InsertDailyProduction(production1);
                    else
                    {

                        val = BL_DailyMolassesProduction.updateMolassesaction(production1);
                    }
                    if (val == "0")
                    {

                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("DailyMolassesProductionList");
                    }
                    else
                    {
                        string message = "Server Error";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    }
                }
                else
                {
                    btnSaveasDraft.Enabled = true;
                    string message = "No Vat Details.heck Vat Details";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
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
                if (grdDailyMolassesProduction.Rows.Count > 0)
                {

                    string user_id = Session["UserID"].ToString();

                    production1 = new List<DailyMolassesProduction_e>();
                    for (int i = 0; i < grdDailyMolassesProduction.Rows.Count; i++)
                    {
                        DailyMolassesProduction_e prod1 = new DailyMolassesProduction_e();
                        GridViewRow row = grdDailyMolassesProduction.Rows[i];
                        prod1.dailymolassesproduction_id = (row.Cells[8].FindControl("lbldailymolassesproduction_id") as Label).Text;
                        if ((row.Cells[6].FindControl("txtTodaysProd") as TextBox).Text != "")
                            prod1.dailyproduction = Convert.ToDouble((row.Cells[6].FindControl("txtTodaysProd") as TextBox).Text);
                        else
                            prod1.dailyproduction = 0;
                        prod1.brix = (row.Cells[7].FindControl("txtTodaysBrix") as TextBox).Text;
                        prod1.vat_code = (row.Cells[0].FindControl("lblvatcode") as Label).Text;
                        prod1.vat_name = (row.Cells[1].FindControl("lblvatname") as Label).Text;
                        prod1.storage_content = (row.Cells[2].FindControl("lblstorage_content") as Label).Text;
                        prod1.uom_code = (row.Cells[3].FindControl("lbluom_code") as Label).Text;
                        prod1.vat_totalcapacity = Convert.ToDouble((row.Cells[5].FindControl("txttotalcapacity") as TextBox).Text);
                        prod1.creation_date = txtdob1.Value;
                        prod1.party_code = ddlpartyname.SelectedValue;
                        prod1.record_status = "Y";
                        prod1.financial_year = Session["financial_year"].ToString();
                        prod1.user_id = Session["UserID"].ToString();
                        prod1.remarks = txtRemarks1.Text;
                        //else
                        // prod1.record_status = "N";
                        if (i == 0)
                        {
                            prod1.docs = new List<EASCM_DOCS>();
                            for (int j = 0; j < grdAdd.Rows.Count; j++)
                            {
                                EASCM_DOCS doc = new EASCM_DOCS();
                                doc.doc_name = (grdAdd.Rows[j].FindControl("lblFileName") as Label).Text;
                                doc.doc_path = (grdAdd.Rows[j].FindControl("lblFilePath") as Label).Text;
                                doc.description = (grdAdd.Rows[j].FindControl("lblDiscriptione") as Label).Text;
                                prod1.docs.Add(doc);
                                // prod1.remarks = txtRemarks1.Text;
                            }
                        }

                        production1.Add(prod1);

                    }
                    // user = BL_UserDetails.GetUser(Session["UserID"].ToString());
                    string val;
                    if (Session["rtype"].ToString() == "0")
                        val = BL_DailyMolassesProduction.InsertDailyProduction(production1);
                    else
                    {

                        val = BL_DailyMolassesProduction.updateMolassesaction(production1);
                    }
                    if (val == "0")
                    {

                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("DailyMolassesProductionList");
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
                        string message = "Server Error";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    }

                }
            }
            else
            {
                string message = "No Vat Details.Check Vat Details";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append(message);
                sb.Append("')};");
                sb.Append("</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string party_code = Session["Party_code"].ToString();
                string molassesproduction_id = (grdDailyMolassesProduction.Rows[0].Cells[8].FindControl("lbldailymolassesproduction_id") as Label).Text;
                string edate = Session["date"].ToString();

                string remarks = txtapproverremarks.Text;
                string recordstatus = "A";
                string val;
                production1 = new List<DailyMolassesProduction_e>();
                for (int i = 0; i < grdDailyMolassesProduction.Rows.Count; i++)
                {
                    DailyMolassesProduction_e prod1 = new DailyMolassesProduction_e();
                    GridViewRow row = grdDailyMolassesProduction.Rows[i];
                    prod1.dailymolassesproduction_id = (row.Cells[8].FindControl("lbldailymolassesproduction_id") as Label).Text;
                    prod1.dailyproduction = Convert.ToDouble((row.Cells[6].FindControl("txtTodaysProd") as TextBox).Text);
                    prod1.brix = (row.Cells[7].FindControl("txtTodaysBrix") as TextBox).Text;
                    prod1.vat_code = (row.Cells[0].FindControl("lblvatcode") as Label).Text;
                    prod1.vat_name = (row.Cells[1].FindControl("lblvatname") as Label).Text;
                    prod1.storage_content = (row.Cells[2].FindControl("lblstorage_content") as Label).Text;
                    prod1.uom_code = (row.Cells[3].FindControl("lbluom_code") as Label).Text;
                    prod1.vat_totalcapacity = Convert.ToDouble((row.Cells[5].FindControl("txttotalcapacity") as TextBox).Text);
                    prod1.creation_date = txtDATE.Text;
                    prod1.financial_year = Session["financial_year"].ToString();
                    prod1.party_code = ddlpartyname.SelectedValue;
                    prod1.record_status = "Y";
                    production1.Add(prod1);
                }
                val = BL_DailyMolassesProduction.Approve(party_code, edate, remarks, recordstatus, molassesproduction_id, Session["UserID"].ToString(), production1);

                if (val == "0")
                {

                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("DailyMolassesProductionList");
                }
                else
                {
                    string message = "Server Error";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
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
                string party_code = Session["Party_code"].ToString();
                string edate = Session["date"].ToString();
                string molassesproduction_id = (grdDailyMolassesProduction.Rows[0].Cells[8].FindControl("lbldailymolassesproduction_id") as Label).Text;
                string remarks = txtapproverremarks.Text;
                string recordstatus = "R";
                string val;
                production1 = new List<DailyMolassesProduction_e>();
                for (int i = 0; i < grdDailyMolassesProduction.Rows.Count; i++)
                {
                    DailyMolassesProduction_e prod1 = new DailyMolassesProduction_e();
                    GridViewRow row = grdDailyMolassesProduction.Rows[i];
                    prod1.dailymolassesproduction_id = (row.Cells[8].FindControl("lbldailymolassesproduction_id") as Label).Text;
                    prod1.dailyproduction = Convert.ToDouble((row.Cells[6].FindControl("txtTodaysProd") as TextBox).Text);
                    prod1.brix = (row.Cells[7].FindControl("txtTodaysBrix") as TextBox).Text;
                    prod1.vat_code = (row.Cells[0].FindControl("lblvatcode") as Label).Text;
                    prod1.vat_name = (row.Cells[1].FindControl("lblvatname") as Label).Text;
                    prod1.storage_content = (row.Cells[2].FindControl("lblstorage_content") as Label).Text;
                    prod1.uom_code = (row.Cells[3].FindControl("lbluom_code") as Label).Text;
                    prod1.financial_year = Session["financial_year"].ToString();
                    prod1.vat_totalcapacity = Convert.ToDouble((row.Cells[5].FindControl("txttotalcapacity") as TextBox).Text);
                    prod1.creation_date = txtDATE.Text;
                    prod1.party_code = ddlpartyname.SelectedValue;
                    prod1.record_status = "R";
                    production1.Add(prod1);
                }
                val = BL_DailyMolassesProduction.Approve(party_code, edate, remarks, recordstatus, molassesproduction_id, Session["UserID"].ToString(),production1);
                if (val == "0")
                {

                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("DailyMolassesProductionList");
                }
                else
                {
                    string message = "Server Error";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                }
            }
        }

        protected void grdDailyMolassesProduction_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

       

        protected void GetVATS()
        {

           
            if (txtdob1.Value!="" )
            {
                //  user = BL_UserDetails.GetUser(Session["UserID"].ToString());
               
                production1 = BL_DailyMolassesProduction.GetMolassesActionList(Session["Party_code"].ToString(), Session["date"].ToString());
                var pp = (from s in production1
                                  where s.record_status=="N" || s.record_status == "R" orderby s.vat_code
                          select s);
                if (production1.Count > 0)
                {
                    for (int i1 = 0; i1 < production1[0].docs.Count; i1++)
                    {
                        if (i1 == 0)
                            dummytable.Visible = false;
                        dt = (DataTable)ViewState["Records"];
                        dt.Rows.Add("1", production1[0].docs[i1].doc_name, production1[0].docs[i1].description, production1[0].docs[i1].doc_path, production1[0].docs[i1].id);
                        grdAdd.DataSource = dt;
                        grdAdd.DataBind();

                        Doc_id++;
                    }
                }
                if (pp.ToList().Count > 0)
                {
                    for (int i2 = 0; i2 < dt.Rows.Count; i2++)
                    {
                        if ((grdAdd.Rows[i2].FindControl("lblStatus") as Label).Text == "1")
                        {
                            //(grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = false;
                            if ((Session["rtype"].ToString() != "1"))
                                (grdAdd.Rows[i2].FindControl("ImageButton1") as ImageButton).Visible = true;
                            else
                                (grdAdd.Rows[i2].FindControl("ImageButton1") as ImageButton).Visible = false;
                        }
                    }
                }
                string v = Session["Party_code"].ToString();
                ddlpartyname.SelectedValue = Session["Party_code"].ToString();
                txtDATE.Text = Session["date"].ToString();
                txtdob1.Value = Session["date"].ToString();
             //   txtdob.Value = Session["date"].ToString();
                //    GetVATS();
                for (int i2 = 0; i2 < production1.Count; i2++)
                {
                    if (txtRemarks1.Text == "")
                        txtRemarks1.Text = production1[i2].remarks;
                }
                //  production1 = new List<DailyMolassesProduction_e>();
                ddlpartyname.SelectedValue = Session["Party_code"].ToString();
                ddlpartyname.Attributes.Add("disabled", "disabled");
                Session["dailymolassesproduction_id"] = production1[0].dailymolassesproduction_id;
                List<All_Approvals> approvals = new List<All_Approvals>();
                approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), production1[0].dailymolassesproduction_id.ToString(), "DMP");
                //  grdApprovalDetails.DataSource = approvals;
                            var list4 = (from s in approvals
                                         where s.financial_year == Session["DMfinancial_year"].ToString()
                                         select s);
                grdApprovalDetails.DataSource = list4.ToList();
                approverremarks.Visible = false;
                grdApprovalDetails.DataBind();

                if (Session["rtype"].ToString() == "1")
                {
                   // grdAdd.Columns[grdAdd.Columns.Count - 1].Visible = false;
                    grdDailyMolassesProduction.Enabled = false;
                    Image1.Visible = false;
                    docs.Visible = false;
                    txtRemarks1.ReadOnly = true;
                    txtDATE.ReadOnly = true;

                    btnSaveasDraft.Visible = false;
                    btnCancel.Visible = false;
                    btnSubmit.Visible = false;
                    foreach (GridViewRow g1 in grdDailyMolassesProduction.Rows)
                    {
                        TextBox txt = g1.FindControl("txtTodaysProd") as TextBox;
                        TextBox txt1 = g1.FindControl("txtTodaysBrix") as TextBox;
                        vattotal.Value = txt.Text;
                        if (txt.Text != "0")
                        {
                            txt.ReadOnly = true;
                            txt1.ReadOnly = true;
                        }
                    }
                    if (production1[0].record_status == "A")
                    {

                        txtapproverremarks.ReadOnly = true;
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                       // grdAdd.Columns[grdAdd.Columns.Count - 1].Visible = false;
                        Image1.Visible = false;
                    }
                    for (int i3 = 0; i3 < grdAdd.Rows.Count; i3++)
                    {
                        if ((grdAdd.Rows[i3].FindControl("lblStatus") as Label).Text == "1")
                        {
                            if ((Session["rtype"].ToString() == "1"))
                                (grdAdd.Rows[i3].FindControl("ImageButton2") as ImageButton).Visible = true;
                            else
                                (grdAdd.Rows[i3].FindControl("ImageButton2") as ImageButton).Visible = false;
                            (grdAdd.Rows[i3].FindControl("ImageButton1") as ImageButton).Visible = false;
                        }
                    }
                    docs.Visible = false;
                    //List<All_Approvals> approvals = new List<All_Approvals>();
                    //approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), production1[0].dailymolassesproduction_id.ToString(), "DMP");
                    grdApprovalDetails.DataSource = list4.ToList();
                    approverremarks.Visible = false;
                    grdApprovalDetails.DataBind();
                    if (user.role_name != "Bond Officer")
                    {
                        btnSaveasDraft.Visible = false;
                        btnSubmit.Visible = false;
                        btnCancel.Visible = false;
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        //grdAdd.Columns[grdAdd.Columns.Count - 1].Visible = false;
                        Image1.Visible = false;
                    }
                    if (user.role_name == "Bond Officer" && production1[0].record_status == "Y")
                    {
                        approverremarks.Visible = true;
                    }
                    if (user.role_name == "Bond Officer" && production1[0].record_status == "R")
                    {
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        approverremarks.Visible = false;
                    }

                }
                int i = 0;
                if (pp.ToList().Count >0 )
                {
                    List<DailyMolassesProduction_e> production2 = new List<DailyMolassesProduction_e>();
                    production2 = BL_DailyMolassesProduction.GetProduction(ddlpartyname.SelectedValue, Session["financial_year"].ToString());
                    grdDailyMolassesProduction.DataSource = production2;
                    grdDailyMolassesProduction.DataBind();
                 

                    foreach (GridViewRow g1 in grdDailyMolassesProduction.Rows)
                    {
                        Label lbl = (g1.FindControl("lblvatcode") as Label);
                       
                            if (pp.ToList()[i].vat_code == lbl.Text)
                            {
                                TextBox txt = g1.FindControl("txtTodaysProd") as TextBox;
                                TextBox txt1 = g1.FindControl("txtTodaysBrix") as TextBox;
                                txt.Text = pp.ToList()[i].dailyproduction.ToString();
                                txt1.Text = pp.ToList()[i].brix.ToString();
                            vattotal.Value = txt.Text;
                                i++;
                                if (i == pp.ToList().Count)
                                {
                                    break;
                                }
                            }
                       

                    }
                }
                else
                {
                    grdDailyMolassesProduction.DataSource =production1;
                    grdDailyMolassesProduction.DataBind();

                }
            }
            else
            {
                List<DailyMolassesProduction_e> production2 = new List<DailyMolassesProduction_e>();
                production2 = BL_DailyMolassesProduction.GetProduction(ddlpartyname.SelectedValue, Session["financial_year"].ToString());
                grdDailyMolassesProduction.DataSource = production2;
                grdDailyMolassesProduction.DataBind();
            }



        }

        protected void txtDATE_TextChanged(object sender, EventArgs e)
        {
            GetVATS();
        }
    }
}