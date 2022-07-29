using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;
using System.IO;
using Npgsql;
using System.Configuration;

namespace UserMgmt
{
    public partial class DMEE: System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        int Doc_id = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                List<District> districts = new List<District>();
                if (ViewState["Records"] == null)
                {
                    dt.Columns.Add("Status");
                    dt.Columns.Add("Doc_Name");
                    dt.Columns.Add("Doc_Type");
                    dt.Columns.Add("Discription");
                    dt.Columns.Add("Doc_Path");
                    dt.Columns.Add("Doc_id");
                    dt.Columns.Add("User_id");
                    ViewState["Records"] = dt;
                }
                districts = BL_User_Mgnt.GetAllDistrictsList();
                var list = from s in districts
                           select s;
                ddlDistrict.DataSource = list.ToList();
                ddlDistrict.DataTextField = "district_name";
                ddlDistrict.DataValueField = "district_code";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, "Select");
                CalendarExtender3.EndDate =DateTime.Today;
                CalendarExtender4.StartDate = DateTime.Today;
                CalendarExtender1.StartDate = DateTime.Today;
                ddlDistrict.SelectedValue =Session["district_code"].ToString();
                ddlDistrict_SelectedIndexChanged(sender, null);
                Session["rtype1"] = Session["rtype"];
                if (Session["rtype"].ToString() != "0")
                {
                    cm_court obj = new cm_court();
                    obj = BL_cm_court.GetDMDetails(Session["dmcase_registration_id"].ToString());
                    ddlThana.SelectedValue = obj.thana_code;
                    if (obj.confiscation_code == "PR")
                        rdProperty.Checked = true;
                    if (obj.confiscation_code == "VH")
                        rdVehicle.Checked = true;
                    if (obj.raidby == "E")
                        rdbExcise.Checked = true;
                    if (obj.raidby == "P")
                        rdbPolice.Checked = true;
                    ddlThana_SelectedIndexChanged(sender, null);
                    ddlPrFirNo.SelectedValue = obj.seizure_fir_no.ToString();
                    txtProposedLetterNo.Text = obj.proposed_letterno;
                    txtProposedLetterDate.Text = obj.proposed_letterdate;
                  //  if (obj.case_type == "New")
                        ddlCaseType.SelectedValue = obj.case_type;
                   // if (obj.case_type == "Old")
                    //    ddlCaseType.SelectedIndex = 2;
                    txtCaseNo.Text = obj.caseno;
                    txtCaseRegisterDate.Text = obj.case_registerdate;
                    ddlNameofCourt.SelectedValue = obj.court_master_code;
                    txtDateofHearing.Text = obj.case_hearingdate;
                    txtRemarks.Text = obj.remarks;
                    Doc_id = 0;
                    for (int i = 0; i < obj.docs1.Count; i++)
                    {
                        //seizureNo = Session["seizureNo"].ToString();
                        if (i == 0)
                            dummytable.Visible = false;
                        dt = (DataTable)ViewState["Records"];
                        dt.Rows.Add("",obj.docs1[i].doc_name,obj.docs1[i].document_type, obj.docs1[i].description, obj.docs1[i].doc_path, obj.docs1[i].seizure_docs_id);
                        grdAdd.DataSource = dt;
                        grdAdd.DataBind();
                        Doc_id++;

                    }
                    if ( Session["rtype"].ToString() == "1")
                    {

                        ddlPrFirNo.Enabled = false; ;
                        ddlThana.Enabled = false;
                        txtProposedLetterNo.Attributes.Add("Disabled", "Disabled");
                        txtCaseNo.Attributes.Add("Disabled", "Disabled");
                        txtRemarks.Attributes.Add("Disabled", "Disabled");
                        txtProposedLetterDate.Attributes.Add("Disabled", "Disabled");
                        txtDateofHearing.Attributes.Add("Disabled", "Disabled");
                        txtCaseRegisterDate.Attributes.Add("Disabled", "Disabled");
                        ddlCaseType.Attributes.Add("Disabled", "Disabled");
                        ddlNameofCourt.Attributes.Add("Disabled", "Disabled");
                        for (int i = 0; i < obj.docs1.Count; i++)
                        {
                            if ((Session["rtype"].ToString() == "1"))
                            {
                                (grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = true;
                                (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                            }
                        }
                        btnCancel.Visible = false;
                        btnSaveasDraft.Visible = false;
                        btnSubmit.Visible = false;
                        Image1.Visible = false;
                        Image2.Visible = false;
                        Image3.Visible = false;
                        docs.Visible = false;
                        rdbExcise.Enabled = false;
                        rdbPolice.Enabled = false;
                        rdProperty.Enabled = false;
                        rdVehicle.Enabled = false;

                    }
                }
                
            }
        }

        protected void UploadFile(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (idupDocument.HasFile)
                {

                    dummytable.Visible = false;
                    //EnaTable.Visible = false;
                    string fileName = Path.GetFileName(idupDocument.PostedFile.FileName);
                    //fileName.Replace)
                    //  string[] filetype = fileName.Replace("'","").Split('.');
                    FileInfo fi = new FileInfo(fileName);
                    string[] filetype = fileName.Replace("'", "").Split('.');
                    string m = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-sss");
                    idupDocument.PostedFile.SaveAs(Server.MapPath("~/CM_Docs/") + fi.Name.Replace(fi.Extension, "").Replace(",", "_").Replace(" ", "_") + "_" + m + fi.Extension);
                    string path = Server.MapPath("~/CM_Docs/") + fi.Name.Replace(fi.Extension, "") + "_" + m + fi.Extension;
                    dt = (DataTable)ViewState["Records"];
                    dt.Rows.Add("", fileName,ddlDocumentType.SelectedItem.ToString(), txtDiscription.Text, path, Doc_id);
                    grdAdd.DataSource = dt;
                    grdAdd.DataBind();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if ((grdAdd.Rows[i].FindControl("lblStatus") as Label).Text == "1")
                        {
                            //(grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = false;
                            (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                        }
                    }
                    Doc_id++;
                    txtDiscription.Text = "";
                    ddlDocumentType.ClearSelection();
                   


                }
            }

        }
        protected void ddlThana_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdbExcise.Checked == true || rdbPolice.Checked == true)
            {
                string raidby = "";
                if (rdbExcise.Checked)
                    raidby = "E";
                if (rdbPolice.Checked)
                    raidby = "P";
                List<cm_court> prnoo = new List<cm_court>();
                prnoo = BL_User_Mgnt.GetAllDistrictsListPR(ddlDistrict.SelectedValue.ToString());
                var prlist = from l in prnoo
                             where l.thana_code == ddlThana.SelectedValue && l.raidby== raidby
                             select l;
                List<cm_court> _seiz = new List<cm_court>();
                _seiz = BL_cm_seiz_Dmconfiscation.GetDMCaseList();
                var ad = (from s in _seiz
                          where s.district_code == Session["district_code"].ToString()
                          select s);
                var accc = prlist.Where(f1 => _seiz.All(f2 => f2.seizure_fir_no != f1.seizureno));
                if(Session["rtype"].ToString()=="0")
                    ddlPrFirNo.DataSource = accc.ToList();
                else
                ddlPrFirNo.DataSource = prlist.ToList();
                ddlPrFirNo.DataTextField = "prfirno";
                ddlPrFirNo.DataValueField = "seizureno";
                ddlPrFirNo.DataBind();
                ddlPrFirNo.Items.Insert(0, "Select");
            }
            else
            {
                ddlThana.SelectedIndex = 0;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append("Select Seizure By");
                sb.Append("')};");
                sb.Append("</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
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
                    bool value = BL_User_Mgnt.Deletefile("seizure_docs", Session["dmcase_registration_id"].ToString(), v);
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
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    if ((grdAdd.Rows[i].FindControl("lblStatus") as Label).Text == "1")
                    {
                        //  (grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = false;
                        //  (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                    }
                }
                if (dt1.Rows.Count < 1)
                    dummytable.Visible = true;
                //EnaTable.Visible = false;
            }
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string filePath = (sender as ImageButton).CommandArgument;
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath.Replace(",", "_").Replace(" ", "_")));
                if (File.Exists(Server.MapPath("~/CM_Docs/" + Path.GetFileName(filePath.Replace(",", "_").Replace(" ", "_")))))
                    Response.WriteFile(Server.MapPath("~/CM_Docs/" + Path.GetFileName(filePath.Replace(",", "_").Replace(" ", "_"))));
                else
                    Response.WriteFile(Server.MapPath("~/CM_Docs/" + Path.GetFileName(filePath)));
                Response.End();
            }
        }

        protected void rdPersonally_OncheckedChange(object sender, EventArgs e)
        {
          
        }

        protected void rdInformationFromControl_OncheckedChange(object sender, EventArgs e)
        {
            
        }

        
        
        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (IsPostBack)
                {

                    cm_court cm_obj = new cm_court();
                    cm_obj.district_code = ddlDistrict.SelectedValue.ToString();
                    cm_obj.seizure_fir_no = Convert.ToInt32(ddlPrFirNo.SelectedValue.ToString());
                    cm_obj.thana_code = ddlThana.SelectedValue;//.Text;
                    cm_obj.proposed_letterno = txtProposedLetterNo.Text;
                    cm_obj.proposed_letterdate = txtProposedLetterDate.Text;// DateTime.Now.ToShortDateString();
                    cm_obj.case_type = ddlCaseType.SelectedItem.ToString();
                    cm_obj.caseno = txtCaseNo.Text;
                    cm_obj.case_registerdate = txtCaseRegisterDate.Text;
                    cm_obj.court_master_code = ddlNameofCourt.SelectedValue.ToString();
                    cm_obj.case_hearingdate = txtDateofHearing.Text;
                    cm_obj.user_id = Session["UserID"].ToString();
                    cm_obj.lastmodified_date = DateTime.Now.ToShortDateString();
                    cm_obj.record_status = "N";
                    cm_obj.hearing_status = "N";
                    cm_obj.record_deleted = false;
                    if (rdVehicle.Checked)
                        cm_obj.confiscation_code = "VH";
                    if (rdProperty.Checked)
                        cm_obj.confiscation_code = "PR";
                    if (rdbExcise.Checked)
                        cm_obj.raidby = "E";
                    if (rdbPolice.Checked)
                        cm_obj.raidby = "P";
                    cm_obj.remarks = txtRemarks.Text;
                    int i = 0;
                    cm_obj.docs = new List<Seizure_Docs>();
                    dt = ViewState["Records"] as DataTable;
                    foreach (DataRow dr in dt.Rows)
                    {
                        Seizure_Docs doc = new Seizure_Docs();
                        doc.seizureno = cm_obj.seizure_fir_no.ToString();
                        doc.doc_name = dr["Doc_Name"].ToString();
                        doc.doc_path = (grdAdd.Rows[i].FindControl("lblFilePath") as Label).Text;
                        doc.document_type= (grdAdd.Rows[i].FindControl("lblDocumentType") as Label).Text;
                        doc.description = (grdAdd.Rows[i].FindControl("lblDiscriptione") as Label).Text;
                        cm_obj.docs.Add(doc);
                        i++;
                    }
                    DateTime dt1 = DateTime.ParseExact(cm_obj.case_registerdate, "dd-MM-yyyy", null);
                    DateTime dt3 = DateTime.ParseExact(cm_obj.case_hearingdate, "dd-MM-yyyy", null);
                    int cmp1 = dt1.CompareTo(dt3);
                    if (cmp1 > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure Date of Hearing should be greater than or equal to the Case Registration Date.\');", true);
                        return;
                        // date1 is greater means date1 is comes after date2
                    }
                    else
                    {
                        btnSaveasDraft.Enabled = false;
                        bool val;
                        if (Session["rtype1"].ToString() == "0")
                            val = BL_cm_court.InsertDMEntry(cm_obj);
                        else
                        {
                            cm_obj.dmcase_registration_id = Convert.ToInt32(Session["dmcase_registration_id"].ToString());
                            val = BL_cm_court.UpdateDMEntry(cm_obj);
                        }
                        if (val == true)
                        {
                            Session["UserID"] = Session["UserID"];
                            Response.Redirect("DME_List");
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
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                
                cm_court cm_obj = new cm_court();
                cm_obj.district_code = ddlDistrict.SelectedValue.ToString();
                cm_obj.seizure_fir_no = Convert.ToInt32(ddlPrFirNo.SelectedValue.ToString());
                cm_obj.thana_code = ddlThana.SelectedValue;//.Text;
                cm_obj.proposed_letterno = txtProposedLetterNo.Text;
                cm_obj.proposed_letterdate = txtProposedLetterDate.Text;// DateTime.Now.ToShortDateString();
                cm_obj.case_type = ddlCaseType.SelectedItem.ToString();
                cm_obj.caseno = txtCaseNo.Text;
                cm_obj.case_registerdate = txtCaseRegisterDate.Text;
                cm_obj.court_master_code = ddlNameofCourt.SelectedValue.ToString();
                cm_obj.case_hearingdate = txtDateofHearing.Text;
                cm_obj.user_id = Session["UserID"].ToString();
                cm_obj.lastmodified_date = DateTime.Now.ToShortDateString();
                cm_obj.record_status = "Y";
                cm_obj.hearing_status = "Y";
                cm_obj.record_deleted = false;
                cm_obj.case_action = "";
                if (rdVehicle.Checked)
                    cm_obj.confiscation_code = "VH";
                if (rdProperty.Checked)
                    cm_obj.confiscation_code = "PR";
                if (rdbExcise.Checked)
                    cm_obj.raidby = "E";
                if (rdbPolice.Checked)
                    cm_obj.raidby = "P";
                cm_obj.remarks = txtRemarks.Text;
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
                    doc.document_type = (grdAdd.Rows[i].FindControl("lblDocumentType") as Label).Text;

                    cm_obj.docs.Add(doc);
                    i++;
                }
                DateTime dt1 = DateTime.ParseExact(cm_obj.case_registerdate, "dd-MM-yyyy", null);
                DateTime dt3 = DateTime.ParseExact(cm_obj.case_hearingdate, "dd-MM-yyyy", null);
                int cmp1 = dt1.CompareTo(dt3);
                if (cmp1 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the appeal Date should be greater than or equal to the order Date.\');", true);
                    return;
                    // date1 is greater means date1 is comes after date2
                }
                else
                {
                    btnSubmit.Enabled = false;
                    bool val;
                    if (Session["rtype1"].ToString() == "0")
                        val = BL_cm_court.InsertDMEntry(cm_obj);
                    else
                    {
                        cm_obj.dmcase_registration_id = Convert.ToInt32(Session["dmcase_registration_id"].ToString());
                        val = BL_cm_court.UpdateDMEntry(cm_obj);
                    }
                    if (val == true)
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("DME_List");
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DME_List");
        }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DME_List");
        }

        protected void ddlPrFirNo_SelectedIndexChanged(object sender, EventArgs e)
        {
             
            
          
            
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
           
           // ddlNameofCourt.Enabled = true;
          
            List<cm_court> dc = new List<cm_court>();
            dc = BL_User_Mgnt.GetDistrictsCourtList(ddlDistrict.SelectedValue.ToString());
            var dclist = from li in dc
                         select li;
            ddlNameofCourt.DataSource = dclist.ToList();
            ddlNameofCourt.DataTextField = "court_master_name";
            ddlNameofCourt.DataValueField = "court_master_code";
            ddlNameofCourt.DataBind();
            ddlNameofCourt.Items.Insert(0, "Select");
            List<Thana_Details> thana = new List<Thana_Details>();
            thana = BL_User_Mgnt.GetThanaList(string.Empty);
            var ad = (from s in thana
                      where s.district_code == Session["district_code"].ToString()
                      select s);
            ddlThana.DataSource = ad.ToArray();
            ddlThana.DataTextField = "thana_name";
            ddlThana.DataValueField = "thana_code";
            ddlThana.DataBind();
            ddlThana.Items.Insert(0, "Select");
        }

        
    }
}