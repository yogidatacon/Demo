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
    public partial class SEC_Registration : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        int Doc_id = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                List<District> districts = new List<District>();

                districts = BL_User_Mgnt.GetAllDistrictsList();
                var list = from s in districts
                           select s;
                ddlDistrict.DataSource = list.ToList();
                ddlDistrict.DataTextField = "district_name";
                ddlDistrict.DataValueField = "district_code";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, "Select");
                ddlDistrict.SelectedValue = Session["district_code"].ToString();
                ddlDistrict.Enabled = false;
                ddlDistrict_SelectedIndexChanged(sender, null);
                ddlCaseType.SelectedIndex = 0;
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
                if (ViewState["Records1"] == null)
                {
                    dt1.Columns.Add("Status");
                    dt1.Columns.Add("Doc_Name");
                    dt1.Columns.Add("Doc_Type");
                    dt1.Columns.Add("Discription");
                    dt1.Columns.Add("Doc_Path");
                    dt1.Columns.Add("Doc_id");
                    dt1.Columns.Add("User_id");
                    ViewState["Records1"] = dt1;
                }
                if (ViewState["Records2"] == null)
                {
                    dt2.Columns.Add("Status");
                    dt2.Columns.Add("Doc_Name");
                    dt2.Columns.Add("Doc_Type");
                    dt2.Columns.Add("Discription");
                    dt2.Columns.Add("Doc_Path");
                    dt2.Columns.Add("Doc_id");
                    dt2.Columns.Add("User_id");
                    ViewState["Records2"] = dt2;
                }
                if (ViewState["Records3"] == null)
                {
                    dt3.Columns.Add("Status");
                    dt3.Columns.Add("Doc_Name");
                    dt3.Columns.Add("Doc_Type");
                    dt3.Columns.Add("Discription");
                    dt3.Columns.Add("Doc_Path");
                    dt3.Columns.Add("Doc_id");
                    dt3.Columns.Add("User_id");
                    ViewState["Records3"] = dt3;
                }
                if (ViewState["Records4"] == null)
                {
                    dt4.Columns.Add("Status");
                    dt4.Columns.Add("Doc_Name");
                    dt4.Columns.Add("Doc_Type");
                    dt4.Columns.Add("Discription");
                    dt4.Columns.Add("Doc_Path");
                    dt4.Columns.Add("Doc_id");
                    dt4.Columns.Add("User_id");
                    ViewState["Records4"] = dt4;
                }
                Session["rtype1"] = Session["rtype"];
                if (Session["rtype"].ToString() != "0")
                {
                    cm_court obj = new cm_court();
                    obj = BL_cm_court.GetSECDetails(Session["seccase_registration_id"].ToString());
                    ddlDistrict.SelectedValue = obj.district_code;
                    ddlDistrict_SelectedIndexChanged(sender, null);
                    ddlThana.SelectedValue = obj.thana_code;
                    CalendarExtender1.StartDate =Convert.ToDateTime( obj.confiscationorderdate);
                    CalendarExtender2.StartDate = Convert.ToDateTime(obj.confiscationorderdate);
                    ddlPrFirNo.SelectedValue = obj.seizure_fir_no.ToString();
                   

                    ddlCaseType.SelectedValue = obj.case_type;
                    txtAppealCaseno.Text = obj.appealno;
                    txtAppealCaseRegisterDate.Text = obj.appealdate;
                    txtAppellantName.Text = obj.appellant_name.Trim();
                    txtAppellantContactNo.Text = obj.appellant_contact.Trim();
                    txtConfiscationOrderDate.Text = obj.confiscationorderdate;
                    txtConfiscationOrderNo.Text = obj.confiscationorderno;
                    txtDateofHearing.Text = obj.case_hearingdate;
                    // txt.Text = obj.case_hearingdate;
                  //  txtRremarks.Text = obj.remarks;
                    txtRemarks.Text = obj.remarks;
                    rdProperty.Checked = false;
                    rdVehicle.Checked = false;
                    rdbExcise.Checked = false;
                    rdbPolice.Checked = false;
                    if (obj.confiscation_code == "PR")
                        rdProperty.Checked = true;
                    if (obj.confiscation_code == "VH")
                        rdVehicle.Checked = true;
                    if (obj.raidby == "E")
                        rdbExcise.Checked = true;
                    if (obj.raidby == "P")
                        rdbPolice.Checked = true;
                    ddlThana_SelectedIndexChanged(sender, null);
                    Doc_id = 0;
                    if (obj.docs1.Count == 0)
                    {
                        DataTable d1t = new DataTable();
                        grdAdd1.DataSource =d1t;
                        grdAdd1.DataBind();
                    }
                    if (obj.docs2.Count == 0)
                    {
                        DataTable d1t = new DataTable();
                        grdAdd2.DataSource = d1t;
                        grdAdd2.DataBind();
                    }
                    if (obj.docs3.Count == 0)
                    {
                        DataTable d1t = new DataTable();
                        grdAdd3.DataSource = d1t;
                        grdAdd3.DataBind();
                    }
                    if (obj.docs4.Count == 0)
                    {
                        DataTable d1t = new DataTable();
                        grdAdd4.DataSource = d1t;
                        grdAdd4.DataBind();
                    }
                    Doc_id = 0;
                    for (int i = 0; i < obj.docs1.Count; i++)
                    {
                        //seizureNo = Session["seizureNo"].ToString();
                        if (i == 0)
                            dummytable.Visible = false;
                        dt1 = (DataTable)ViewState["Records1"];
                        dt1.Rows.Add("", obj.docs1[i].doc_name, obj.docs1[i].document_type, obj.docs1[i].description, obj.docs1[i].doc_path, obj.docs1[i].seizure_docs_id);
                        grdAdd1.DataSource = dt1;
                        grdAdd1.DataBind();
                        Doc_id++;
                    }
                    Doc_id = 0;
                    for (int i = 0; i < obj.docs2.Count; i++)
                    {
                        //seizureNo = Session["seizureNo"].ToString();
                        if (i == 0)
                            dummytable.Visible = false;
                        dt2 = (DataTable)ViewState["Records2"];
                        dt2.Rows.Add("", obj.docs2[i].doc_name, obj.docs2[i].document_type, obj.docs2[i].description, obj.docs2[i].doc_path, obj.docs2[i].seizure_docs_id);
                        grdAdd2.DataSource = dt2;
                        grdAdd2.DataBind();
                        Doc_id++;
                    }
                    Doc_id = 0;
                    for (int i = 0; i < obj.docs3.Count; i++)
                    {
                        //seizureNo = Session["seizureNo"].ToString();
                        if (i == 0)
                            dummytable.Visible = false;
                        dt3 = (DataTable)ViewState["Records3"];
                        dt3.Rows.Add("", obj.docs3[i].doc_name, obj.docs3[i].document_type, obj.docs3[i].description, obj.docs3[i].doc_path, obj.docs3[i].seizure_docs_id);
                        grdAdd3.DataSource = dt3;
                        grdAdd3.DataBind();
                        Doc_id++;
                    }
                    Doc_id = 0;
                    for (int i = 0; i < obj.docs4.Count; i++)
                    {
                        //seizureNo = Session["seizureNo"].ToString();
                        if (i == 0)
                            dummytable.Visible = false;
                        dt4 = (DataTable)ViewState["Records4"];
                        dt4.Rows.Add("", obj.docs4[i].doc_name, obj.docs4[i].document_type, obj.docs4[i].description, obj.docs4[i].doc_path, obj.docs4[i].seizure_docs_id);
                        grdAdd4.DataSource = dt4;
                        grdAdd4.DataBind();
                        Doc_id++;
                    }
                    Doc_id = 0;
                    for (int i = 0; i < obj.docs5.Count; i++)
                    {
                        //seizureNo = Session["seizureNo"].ToString();
                        if (i == 0)
                            dummytable.Visible = false;
                        dt = (DataTable)ViewState["Records"];
                        dt.Rows.Add("", obj.docs5[i].doc_name, obj.docs5[i].document_type, obj.docs5[i].description, obj.docs5[i].doc_path, obj.docs5[i].seizure_docs_id);
                        grdAdd.DataSource = dt;
                        grdAdd.DataBind();
                        Doc_id++;
                    }
                    ddlDistrict.Attributes.Add("Disabled", "Disabled");
                    ddlThana.Attributes.Add("Disabled", "Disabled");
                    ddlPrFirNo.Attributes.Add("Disabled", "Disabled");
                    
                    //   Image3.Visible = false;
                    //docs.Visible = false;
                    rdbExcise.Enabled = false;
                    rdbPolice.Enabled = false;
                    rdProperty.Enabled = false;
                    rdVehicle.Enabled = false;

                    if (Session["rtype"].ToString() == "1")
                    {
                        //for (int i = 0; i < obj.docs1.Count; i++)
                        //{
                        //    if ((Session["rtype"].ToString() == "1"))
                        //    {
                        //        (grdAdd1.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = true;
                        //        (grdAdd1.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                        //    }
                        //}
                        for (int i = 0; i < obj.docs5.Count; i++)
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
                        txtAppealCaseno.Enabled = false;
                        txtAppealCaseRegisterDate.Attributes.Add("disabled", "disabled");
                        txtAppellantContactNo.Enabled = false;
                        txtAppellantName.Enabled = false;
                        txtDateofHearing.Attributes.Add("disabled", "disabled");
                        txtRemarks.Attributes.Add("Disabled", "Disabled");
                        ddlCaseType.Attributes.Add("disabled", "disabled");
                        Image1.Visible = false;
                        Image2.Visible = false;
                        Image3.Visible = false;
                        //if (obj.case_action == "Next Hearing")
                        //    txtDateofHearing.Attributes.Add("disabled", "disabled");
                        //if (obj.case_action == "Case Dispose")
                        //{
                        txtConfiscationOrderNo.Enabled = false;
                            txtConfiscationOrderDate.Attributes.Add("disabled", "disabled");
                       // }
                        docs.Visible = false;

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
                    dt.Rows.Add("", fileName, ddlDocumentType.SelectedItem.ToString(), txtDiscription.Text, path, Doc_id);
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
                    //txtRaidLocation.Text = "AAJAM NAGAR SHO";


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
                string a = Session["rtype1"].ToString();
                string v = Path.GetFileName(filePath).ToString();
                if (Session["rtype1"].ToString() != "0")
                {
                    bool value = BL_User_Mgnt.Deletefile("seizure_docs", Session["seccase_registration_id"].ToString(), v);
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
            //txtRaidLocation.Text = "AAJAM NAGAR SHO";
        }

        protected void rdInformationFromControl_OncheckedChange(object sender, EventArgs e)
        {
           // txtRaidLocation.Text = "AAJAM NAGAR SHO";
        }



        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                cm_court cm_obj = new cm_court();
                cm_obj.district_code = ddlDistrict.SelectedValue.ToString();
                cm_obj.seizure_fir_no = Convert.ToInt32(ddlPrFirNo.SelectedValue.ToString());
                cm_obj.thana_code = ddlThana.SelectedValue;//.Text;
                cm_obj.case_type =ddlCaseType.SelectedValue;
                cm_obj.appealno = txtAppealCaseno.Text;
                cm_obj.appealdate = txtAppealCaseRegisterDate.Text;
                cm_obj.appellant_name = txtAppellantName.Text;
                cm_obj.appellant_contact = txtAppellantContactNo.Text;
                cm_obj.user_id = Session["UserID"].ToString();
                cm_obj.case_hearingdate = txtDateofHearing.Text;
                
                cm_obj.lastmodified_date = DateTime.Now.ToShortDateString();
                cm_obj.record_status = "N";
             ////   cm_obj.case_action = dd.SelectedItem.ToString();
             //   if (cm_obj.case_action == "Next Hearing")
             //       cm_obj.next_hearingdate = txtDateofHearing.Text;
               
                    cm_obj.confiscationorderno = txtConfiscationOrderNo.Text.Trim();
                    cm_obj.confiscationorderdate = txtConfiscationOrderDate.Text.Trim();
               
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
                    doc.seizureno = cm_obj.seizureno.ToString();
                    doc.doc_name = dr["Doc_Name"].ToString();
                    doc.doc_path = (grdAdd.Rows[i].FindControl("lblFilePath") as Label).Text;
                    doc.description = (grdAdd.Rows[i].FindControl("lblDiscriptione") as Label).Text;
                    doc.document_type = (grdAdd.Rows[i].FindControl("lblDocumentType") as Label).Text;
                    cm_obj.docs.Add(doc);
                    i++;
                }
                DateTime dt1 = DateTime.ParseExact(cm_obj.appealdate, "dd-MM-yyyy", null);
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
                    bool val;
                    //if (Session["rtype"].ToString() == "0")
                    //    val = BL_cm_court.InsertDMEntry(cm_obj);
                    //else
                    //{
                    cm_obj.seccase_registration_id = Convert.ToInt32(Session["seccase_registration_id"].ToString());
                    val = BL_cm_court.UpdateSECEntry(cm_obj);
                    //   }
                    if (val == true)
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("SEC_Registration_List");
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
          
            if (IsPostBack)
            {
                cm_court cm_obj = new cm_court();
                cm_obj.district_code = ddlDistrict.SelectedValue.ToString();
                cm_obj.seizure_fir_no = Convert.ToInt32(ddlPrFirNo.SelectedValue.ToString());
                cm_obj.thana_code = ddlThana.SelectedValue;//.Text;
                cm_obj.case_type = ddlCaseType.SelectedValue;
                cm_obj.appealno = txtAppealCaseno.Text;
                cm_obj.appealdate = txtAppealCaseRegisterDate.Text;
                cm_obj.appellant_name = txtAppellantName.Text;
                cm_obj.appellant_contact = txtAppellantContactNo.Text;
                // cm_obj.court_master_code = NameofCourt.Value;
                cm_obj.case_hearingdate = txtDateofHearing.Text;
                cm_obj.user_id = Session["UserID"].ToString();
                cm_obj.lastmodified_date = DateTime.Now.ToShortDateString();
                cm_obj.record_status = "Y";
                              ////   cm_obj.case_action = dd.SelectedItem.ToString();
                //   if (cm_obj.case_action == "Next Hearing")
              //  cm_obj.hearing_status = dd.Text;

                cm_obj.confiscationorderno = txtConfiscationOrderNo.Text.Trim();
                cm_obj.confiscationorderdate = txtConfiscationOrderDate.Text.Trim();

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
                    doc.seizureno = cm_obj.seizureno.ToString();
                    doc.doc_name = dr["Doc_Name"].ToString();
                    doc.doc_path = (grdAdd.Rows[i].FindControl("lblFilePath") as Label).Text;
                    doc.description = (grdAdd.Rows[i].FindControl("lblDiscriptione") as Label).Text;
                    doc.document_type = (grdAdd.Rows[i].FindControl("lblDocumentType") as Label).Text;
                    cm_obj.docs.Add(doc);
                    i++;
                }
                DateTime dt1 = DateTime.ParseExact(cm_obj.appealdate, "dd-MM-yyyy", null);
                DateTime dt3 = DateTime.ParseExact(cm_obj.case_hearingdate, "dd-MM-yyyy", null);
                int cmp1 = dt1.CompareTo(dt3);
                if (cmp1 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the Date of Hearing should be greater than or equal to the Appeal Date.\');", true);
                    return;
                    // date1 is greater means date1 is comes after date2
                }
                else
                {
                    btnSubmit.Enabled = false;
                    bool val;
                    //if (Session["rtype"].ToString() == "0")
                    //    val = BL_cm_court.InsertDMEntry(cm_obj);
                    //else
                    //{
                    cm_obj.hearing_status = "Y";
                    cm_obj.seccase_registration_id = Convert.ToInt32(Session["seccase_registration_id"].ToString());
                    val = BL_cm_court.UpdateSECEntry(cm_obj);
                    //   }
                    if (val == true)
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("SEC_Registration_List");
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
            Response.Redirect("SEC_Registration_List");
        }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Response.Redirect("SEC_Registration_List");
        }

        protected void ddlPrFirNo_SelectedIndexChanged(object sender, EventArgs e)
        {

          


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
                             where l.thana_code == ddlThana.SelectedValue && l.raidby == raidby
                             select l;
                List<cm_court> _seiz = new List<cm_court>();
                _seiz = BL_cm_seiz_Dmconfiscation.GetDMCaseList();
                var ad = (from s in _seiz
                          where s.district_code == Session["district_code"].ToString()
                          select s);
                var accc = prlist.Where(f1 => _seiz.All(f2 => f2.seizure_fir_no != f1.seizureno));
                if (Session["rtype"].ToString() == "0")
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
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
           
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

        protected void ddlActionPorposed_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}