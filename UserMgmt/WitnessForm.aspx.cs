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
    public partial class WitnessForm : System.Web.UI.Page
    {
        List<cm_seiz_Witness> _witness = new List<cm_seiz_Witness>();
        DataTable dt = new DataTable();
        static int Doc_id = 1;
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
                    dt.Columns.Add("Status");
                    ViewState["Records"] = dt;
                }
                List<cm_designation> _designation = new List<cm_designation>();
                _designation = BL_cm_designation.GetList();
                ddlDesignation.DataSource = _designation;
                ddlDesignation.DataTextField = "designation_name";
                ddlDesignation.DataValueField = "designation_code";
                ddlDesignation.DataBind();
                ddlDesignation.Items.Insert(0, "Select");
            

                List<cm_gender> _gender = new List<cm_gender>();
                _gender = BL_cm_gender.GetList();
                ddlGender.DataSource = _gender;
                ddlGender.DataTextField = "gender_name";
                ddlGender.DataValueField = "gender_code";
                ddlGender.DataBind();
                ddlGender.Items.Insert(0, "Select");

                //List<cm_designation> _designation = new List<cm_designation>();
                //_designation = BL_cm_designation.GetList();
                //ddlDesignation.DataSource = _designation;
                //ddlDesignation.DataTextField = "designation_name";
                //ddlDesignation.DataValueField = "designation_code";
                //ddlDesignation.DataBind();
                //ddlDesignation.Items.Insert(0, "Select");
                Session["rtype1"] = Session["rtype"];
                if (Session["rtype"].ToString() != "0")
                {
                    string seizureNo = Session["seizureNo"].ToString();
                    string tableId = Session["tableId"].ToString();
                    searchid.Visible = false;
                    cm_seiz_Witness obj = new cm_seiz_Witness();
                    obj = BL_cm_seiz_Witness.GetDetails(tableId);

                    txtName.Text = obj.witnessname;
                    //txtDesignation.Text = obj.designation_code;
                    if (obj.witnesstype == "D")
                        rdDepartmentOfficer.Checked = true;
                    if (obj.witnesstype == "I")
                        rdIndependentPerson.Checked = true;
                    ddlDesignation.SelectedValue = obj.designation_code;
                    txtAge.Text = obj.witness_age;
                    ddlGender.SelectedValue = obj.gender_code;
                    txtFatherSpouseName.Text = obj.relativename;
                    txtMobileNo.Text = obj.mobile;
                    txtLandlineNo.Text = obj.landline;
                  
                    txtEmailId.Text = obj.witness_emailid;
                    txtPermanentAddress.Text = obj.permanentaddress;
                    txtPresentAddress.Text = obj.presentaddress;
                    if (obj.witnesstype == "D" || obj.witnesstype == "Department Officer")
                        rdDepartmentOfficer.Checked = true;
                    if (obj.witnesstype == "I" || obj.witnesstype == "Independent Person")
                        rdIndependentPerson.Checked = true;
                    if (obj.gender_code == "Select")
                        obj.gender_code = "0";
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
                            btnSubmit.Visible = false;
                            idupDocument.Enabled = false;
                            txtDiscription.Enabled= false;
                            btnUpload.Enabled = false;
                        }
                        if ((Session["rtype"].ToString() == "1"))
                        {
                            (grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = true;
                            (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                        }
                    }
                    for (int i = 0; i < obj.docs.Count; i++)
                    {
                        if (obj.record_status == "Y")
                        {
                            (grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = true;
                            (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = true;
                            ////btnSubmit.Visible = false;
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
                        idupDocument.Enabled = false;
                        txtDiscription.Enabled = false;
                        btnUpload.Enabled = false;
                        docs.Visible = false;
                        btnSubmit.Visible = false;
                        dummytable.Visible = false;
                        txtName.Attributes.Add("disabled", "disabled");
                        //txtDesignation.Attributes.Add("disabled", "disabled");
                        ddlDesignation.Attributes.Add("disabled", "disabled");
                        txtAge.Attributes.Add("disabled", "disabled");
                        ddlGender.Attributes.Add("disabled", "disabled");
                        txtFatherSpouseName.Attributes.Add("disabled", "disabled");
                        txtMobileNo.Attributes.Add("disabled", "disabled");
                        txtLandlineNo.Attributes.Add("disabled", "disabled");
                        txtEmailId.Attributes.Add("disabled", "disabled");
                        txtPermanentAddress.Attributes.Add("disabled", "disabled");
                        txtPresentAddress.Attributes.Add("disabled", "disabled");
                        rdDepartmentOfficer.Enabled = false;
                        rdIndependentPerson.Enabled = false;
                        chk.Enabled = false;
                        btnSubmit.Enabled = false; 
                    }
                }
            }
        }

        protected void rdIndependentPerson_OncheckedChange(object sender, EventArgs e)
        {
           if(rdIndependentPerson.Checked)
            desg.Visible = false;
           else if (rdDepartmentOfficer.Checked)
                desg.Visible = true;

        }
        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("WitnessList");
        }

        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                btnSaveasDraft.Enabled = false;
                cm_seiz_Witness cm_obj = new cm_seiz_Witness();
                cm_obj.seizureno = Session["seizureNo"].ToString();
                cm_obj.witnessname = txtName.Text;
                cm_obj.designation_code = ddlDesignation.SelectedValue;
                cm_obj.witness_age = txtAge.Text;
                cm_obj.gender_code = ddlGender.SelectedValue;
                cm_obj.relativename = txtFatherSpouseName.Text;
                cm_obj.mobile = txtMobileNo.Text;
                cm_obj.landline = txtLandlineNo.Text.Trim();
                //if (cm_obj.landline == "")
                //    cm_obj.landline = null;
                if (cm_obj.gender_code == "Select")
                    cm_obj.gender_code = "0";
                cm_obj.witness_emailid = txtEmailId.Text;
                cm_obj.district_code = Session["district_code"].ToString();
                cm_obj.permanentaddress = txtPermanentAddress.Text;
                cm_obj.presentaddress = txtPresentAddress.Text;
                if (rdDepartmentOfficer.Checked)
                    cm_obj.witnesstype = "D";
                else if(rdIndependentPerson.Checked)
                        cm_obj.witnesstype = "I";
                cm_obj.user_id = Session["UserID"].ToString();
                cm_obj.lastmodified_date = DateTime.Now.ToShortDateString();
                cm_obj.record_status = "N";
                cm_obj.raidby = Session["RaidBy"].ToString().Substring(0, 1).ToUpper();
                string strHostName = System.Net.Dns.GetHostName();
                string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                cm_obj.ipaddress = clientIPAddress.ToString();
                cm_obj.docs = new List<Seizure_Docs>();
                dt = ViewState["Records"] as DataTable;
                int i = 0;
               
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Seizure_Docs doc = new Seizure_Docs();
                        doc.doc_name = dr["Doc_Name"].ToString();
                        doc.doc_path = (grdAdd.Rows[i].FindControl("lblFilePath") as Label).Text;
                        doc.description = (grdAdd.Rows[i].FindControl("lblDiscriptione") as Label).Text;
                        doc.seizureno = Session["seizureno"].ToString();
                        cm_obj.docs.Add(doc);
                        i++;
                    }
                }
              
                string val;
                string tempTableId = Session["tableId"]?.ToString() ?? string.Empty;
                if (Session["rtype1"].ToString() == "0" && string.IsNullOrEmpty(tempTableId))
                    val = BL_cm_seiz_Witness.InsertWitness(cm_obj);
                else
                {
                    cm_obj.seizure_witnessdetails_id = (Session["tableId"].ToString());
                    val = BL_cm_seiz_Witness.Update(cm_obj);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("~/WitnessList");
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/WitnessList");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                }
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/LoginPage");
                }
                Session["UserID"] = Session["UserID"];

                string witnessname = txtWitnessName.Text.Trim();
                string fatherName = txtFSName.Text.Trim();
                string mobile = txtMNo.Text.Trim();
                if (witnessname.Trim() != "" || fatherName.Trim() != "" || mobile.Trim() != "")
                {
                    _witness = BL_cm_seiz_Witness.WitnessSearch(witnessname, fatherName, mobile);
                    grdWitnessView.DataSource = _witness.ToList();
                    grdWitnessView.DataBind();
                }
                else
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append("Enter Atleast one Value");
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                }

            }
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                btnSubmit.Enabled = false;
                cm_seiz_Witness cm_obj = new cm_seiz_Witness();
                cm_obj.seizureno = Session["seizureNo"].ToString();
                cm_obj.witnessname = txtName.Text;
                // cm_obj.designation_code = txtDesignation.Text;
                cm_obj.designation_code = ddlDesignation.SelectedValue;
                 cm_obj.witness_age = txtAge.Text;
                cm_obj.gender_code = ddlGender.SelectedValue;
                cm_obj.relativename = txtFatherSpouseName.Text;
                cm_obj.mobile = txtMobileNo.Text;
                cm_obj.landline = txtLandlineNo.Text;
                if (cm_obj.landline == "")
                    cm_obj.landline = null;
                cm_obj.witness_emailid = txtEmailId.Text;
                cm_obj.permanentaddress = txtPermanentAddress.Text;
                cm_obj.presentaddress = txtPresentAddress.Text;
                cm_obj.raidby = Session["RaidBy"].ToString().Substring(0, 1).ToUpper();
                string strHostName = System.Net.Dns.GetHostName();
                string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                cm_obj.ipaddress = clientIPAddress.ToString();
                if (rdDepartmentOfficer.Checked)
                    cm_obj.witnesstype = "D";
                else if (rdIndependentPerson.Checked)
                    cm_obj.witnesstype = "I";
                cm_obj.user_id = Session["UserID"].ToString();
                cm_obj.lastmodified_date = DateTime.Now.ToShortDateString();
                cm_obj.record_status = "Y";
                cm_obj.district_code = Session["district_code"].ToString();
                cm_obj.docs = new List<Seizure_Docs>();
                dt = ViewState["Records"] as DataTable;
                int i = 0;
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Seizure_Docs doc = new Seizure_Docs();
                        doc.doc_name = dr["Doc_Name"].ToString();
                        doc.doc_path = (grdAdd.Rows[i].FindControl("lblFilePath") as Label).Text;
                        doc.description = (grdAdd.Rows[i].FindControl("lblDiscriptione") as Label).Text;
                        doc.seizureno = Session["seizureno"].ToString();
                        cm_obj.docs.Add(doc);
                        i++;
                    }
                }
                string val;
                string tempTableId = Session["tableId"]?.ToString() ?? string.Empty;
                if (Session["rtype1"].ToString() == "0" && string.IsNullOrEmpty(tempTableId))
                    val = BL_cm_seiz_Witness.InsertWitness(cm_obj);
                else
                {
                    cm_obj.seizure_witnessdetails_id = (Session["tableId"].ToString());
                    val = BL_cm_seiz_Witness.Update(cm_obj);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("~/SeizureList");
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

        protected void chselect_CheckedChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

                foreach (GridViewRow row in grdWitnessView.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[row.Cells.Count - 1].FindControl("chselect") as CheckBox);
                        if (chkRow.Checked)
                        {
                           string witnesstype = (row.Cells[0].FindControl("lblWitnessType") as Label).Text;
                            if (witnesstype == "D")
                                rdDepartmentOfficer.Checked = true;
                            else if (witnesstype == "I")
                                rdIndependentPerson.Checked = true;
                            else
                            {
                                rdIndependentPerson.Checked = false;
                                rdDepartmentOfficer.Checked = false;
                            }
                            txtName.Text= (row.Cells[1].FindControl("lblWitnessName") as Label).Text;
                            string s = (row.Cells[1].FindControl("lblDesignation") as Label).Text;
                            if ((row.Cells[1].FindControl("lbldesignation_code") as Label).Text!="")
                            ddlDesignation.SelectedValue = (row.Cells[1].FindControl("lbldesignation_code") as Label).Text;
                            txtAge.Text = (row.Cells[1].FindControl("lblAge") as Label).Text;
                            ddlGender.SelectedValue= (row.Cells[1].FindControl("lblGender") as Label).Text;
                            txtFatherSpouseName.Text = (row.Cells[1].FindControl("lblFatherSpouseName") as Label).Text;
                            txtLandlineNo.Text= (row.Cells[1].FindControl("lbllandline") as Label).Text;
                            txtMobileNo.Text= (row.Cells[1].FindControl("lblmobileno") as Label).Text;
                            txtPermanentAddress.Text= (row.Cells[1].FindControl("lblpermanentaddress") as Label).Text;
                            txtPresentAddress.Text= (row.Cells[1].FindControl("lblpresentaddress") as Label).Text;
                            txtEmailId.Text = (row.Cells[1].FindControl("lblwitness_emailid") as Label).Text;
                            string wtype = (row.Cells[1].FindControl("lblWitnessType") as Label).Text;
                            if (wtype == "D" || wtype== "Department Officer")
                                rdDepartmentOfficer.Checked = true;
                            if (wtype == "I" || wtype == "Independent Person")
                                rdIndependentPerson.Checked = true;
                            break;
                        }
                        else
                        {
                            txtName.Text ="";
                            ddlDesignation.SelectedValue = "Select";
                            txtAge.Text ="";
                            ddlGender.SelectedValue = "Select";
                            txtFatherSpouseName.Text = "";
                            txtLandlineNo.Text = "";
                            txtMobileNo.Text = "";
                            txtPermanentAddress.Text = "";
                            txtPresentAddress.Text = "";
                            txtEmailId.Text ="";
                            rdDepartmentOfficer.Checked = false;
                            rdIndependentPerson.Checked = false;
                        }
                    }
                }

            }
        }

        protected void grdWitnessView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdWitnessView.PageIndex = e.NewPageIndex;
            string witnessname = txtWitnessName.Text.Trim();
            string fatherName = txtFSName.Text.Trim();
            string mobile = txtMNo.Text.Trim();
            _witness = BL_cm_seiz_Witness.WitnessSearch(witnessname, fatherName, mobile);
            grdWitnessView.DataSource = _witness.ToList();
            grdWitnessView.DataBind();
        }
    }
}