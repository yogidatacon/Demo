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
    public partial class OffencesCommittedForm : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
               // PEAct.Visible = false;
                OACT.Visible = false;
                List<cm_offence_type> offence_type = new List<cm_offence_type>();
                offence_type = BL_cm_offence_type.GetList();
                ddlOffence.DataSource = offence_type;
                ddlOffence.DataTextField = "Offence_name";
                ddlOffence.DataValueField = "offence_code";
                ddlOffence.DataBind();
                ddlOffence.Items.Insert(0, "Select");
                List<cm_offence_sections> offence_sections = new List<cm_offence_sections>();
                offence_sections = BL_cm_offence_type.GetSectionList();
                ddloffenceSection.DataSource = offence_sections;
                ddloffenceSection.DataTextField = "Offence_section_name";
                ddloffenceSection.DataValueField = "offence_section_code";
                ddloffenceSection.DataBind();
                ddloffenceSection.Items.Insert(0, "Select");
                OACT.Visible = true;
                if (ViewState["Records"] == null)
                {
                    
                    dt.Columns.Add("Doc_Name");
                    dt.Columns.Add("Discription");
                    dt.Columns.Add("Doc_Path");
                    dt.Columns.Add("Doc_id");
                    dt.Columns.Add("Status");
                    ViewState["Records"] = dt;
                }
                Session["rtype1"] = Session["rtype"];
                List<cm_seiz_AccusedDetails> ads = new List<cm_seiz_AccusedDetails>();
               
                string seizureno = Session["seizureno"].ToString();
                ads = BL_cm_seiz_AccusedDetails.GetDetails(seizureno+"&" + Session["RaidBy"].ToString());
                ddlAccusedName.DataSource = ads.ToList();
                ddlAccusedName.DataTextField = "accusedname";
                ddlAccusedName.DataValueField = "seizure_accused_details_id";
                ddlAccusedName.DataBind();
                ddlAccusedName.Items.Insert(0, "Select");
                if(Session["rtype"].ToString()!="0")
                {
                    List<cm_seiz_OffencesCommitted> offns = new List<cm_seiz_OffencesCommitted>();
                    offns = BL_cm_seiz_OffencesCommitted.GetList(seizureno);
                    Divdummy11.Visible = false;
                    string tableId = Session["TableId"].ToString();
                    var off = (from s in offns
                              where s.seizureno == seizureno && s.seizure_accused_offences_id == tableId
                               select s);
                    ddlAccusedName.SelectedValue = off.ToList()[0].seizure_accused_details_id;
                    ddlAccusedName_SelectedIndexChanged(sender, null);
                    ddlOffence.SelectedValue = off.ToList()[0].offence_code;
                    ddloffenceSection.SelectedValue = off.ToList()[0].offence_section_code;
                    txtoffencedetails.Text= off.ToList()[0].offence_details;
                    if (off.ToList()[0].other_offences != "")
                        CheckBox1.Checked = true;
                    //ddlOffenceunderPEAct.SelectedValue = off.ToList()[0].offence_type_code;
                    txtOtherApplicablesection.Text = off.ToList()[0].other_offences;
                    if (txtOtherApplicablesection.Text != string.Empty)
                    {
                        //rdPEAct.Checked = false;
                        //rdOtherOffences.Checked = true;
                        //PEAct.Visible = false;
                        
                        OACT.Visible = true;
                    }
                    else
                    {
                        //rdPEAct.Checked = true;
                        //rdOtherOffences.Checked = false;
                        //PEAct.Visible = true;
                        OACT.Visible = false;
                    }

                    Doc_id = 0;
                    for (int i = 0; i < off.ToList()[0].docs.Count; i++)
                    {
                        if (i == 0)
                            dummytable.Visible = false;
                        dt = (DataTable)ViewState["Records"];
                        dt.Rows.Add("1", off.ToList()[0].docs[i].doc_name, off.ToList()[0].docs[i].description, off.ToList()[0].docs[i].doc_path, off.ToList()[0].docs[i].seizure_docs_id);
                        grdAdd.DataSource = dt;
                        grdAdd.DataBind();
                        Doc_id++;

                    }
                    for (int i = 0; i <dt.Rows.Count; i++)
                    {
                        if (off.ToList()[0].record_status=="Y")
                        {
                            //(grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = false;
                           (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                            idupDocument.Enabled = false;
                            txtDiscription.Enabled = false;
                            btnUpload.Enabled = false;
                            
                        }
                    }
                }
                if (Session["rtype"].ToString() == "1" )
                {
                    btnCancel.Visible = false;
                    btnSaveasDraft.Visible = false;
                    idupDocument.Enabled = false;
                    txtDiscription.Enabled = false;
                    btnUpload.Enabled = false;
                    txtDiscription.Attributes.Add("disabled", "disabled");
                    txtoffencedetails.Attributes.Add("disabled", "disabled");
                    ddlAccusedName.Attributes.Add("disabled", "disabled");
                    ddlOffence.Attributes.Add("disabled", "disabled");
                    ddloffenceSection.Attributes.Add("disabled", "disabled");
                    //  ddlOffenceunderPEAct.Attributes.Add("disabled", "disabled");
                    txtOtherApplicablesection.Attributes.Add("disabled", "disabled");
                  //  rdPEAct.Enabled= false;
                  //  rdOtherOffences.Enabled = false;
                    idupDocument.Enabled = false;
                    txtDiscription.Enabled = false;
                    btnUpload.Enabled = false;
                    if (!CheckBox1.Checked)
                        CheckBox1.Visible = false;
                    else
                    CheckBox1.Visible = false;
                    docs.Visible = false;
                }
            }
        }


        

        protected void ShowRecord_Click(object sender, EventArgs e)
        {

            Response.Redirect("OffencesCommittedList");
        }
        protected void btnBasicInformation_Click(object sender, EventArgs e)
        {
            Response.Redirect("BasicIformationForm");

        }

        protected void btnEAS_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExcisableArticlesSeizedList");
        }

        protected void btnOtherExisable_Click(object sender, EventArgs e)
        {
            Response.Redirect("VehicleList");

        }
        protected void btnRaidTeam_Click(object sender, EventArgs e)
        {
            Response.Redirect("RaidTeamList");

        }
        protected void btnAccusedDetails_Click(object sender, EventArgs e)
        {
            Response.Redirect("AccusedDetailsList");
        }

        protected void btnAccusedDetail_Click(object sender, EventArgs e)
        {
            Response.Redirect("AccusedDetailsList");
        }
        protected void btnOffencesCommitted_Click(object sender, EventArgs e)
        {
            Response.Redirect("OffencesCommittedList");

        }
        protected void btnCaseHistory_Click(object sender, EventArgs e)
        {
            Response.Redirect("CaseHistoryList");

        }
        static int Doc_id = 1;
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

                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                  

                    //        (grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = false;
                    //        (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                    //btnSubmit.Visible = false;
                    

                    //}
                    Doc_id++;
                    txtDiscription.Text = "";


                //}
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
                //for (int i = 0; i < dt1.Rows.Count; i++)
                //{
                //    if ((grdAdd.Rows[i].FindControl("lblStatus") as Label).Text == "1")
                //    {
                //        (grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = false;
                //        (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                //    }
                //}
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
        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                btnSaveasDraft.Enabled = false;
                cm_seiz_OffencesCommitted offence = new cm_seiz_OffencesCommitted();
                offence.seizure_accused_offences_id= Session["tableId"]?.ToString() ?? string.Empty;
                offence.seizure_accused_details_id = ddlAccusedName.SelectedValue;                
                offence.seizureno = Session["seizureno"].ToString();
                offence.other_offences = txtOtherApplicablesection.Text;
                offence.offence_details = txtoffencedetails.Text;
                offence.offence_code = ddlOffence.SelectedValue;
                offence.offence_section_code = ddloffenceSection.SelectedValue;
                offence.record_status = "N";
                offence.user_id = Session["UserID"].ToString();
                offence.raidby = Session["RaidBy"].ToString().Substring(0, 1);
                string strHostName = System.Net.Dns.GetHostName();
                string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                offence.ipaddress = clientIPAddress.ToString();
                offence.docs = new List<Seizure_Docs>();
                dt = ViewState["Records"] as DataTable;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    Seizure_Docs doc = new Seizure_Docs();
                    doc.doc_name = dr["Doc_Name"].ToString();
                    doc.doc_path = (grdAdd.Rows[i].FindControl("lblFilePath") as Label).Text;
                    doc.description = (grdAdd.Rows[i].FindControl("lblDiscriptione") as Label).Text;
                    doc.seizureno = Session["seizureno"].ToString();
                    offence.docs.Add(doc);
                    i++;
                }
                string val;
                string tempTableId = Session["tableId"]?.ToString() ?? string.Empty;
                if (Session["rtype1"].ToString() == "0" && string.IsNullOrEmpty(tempTableId))
                    val = BL_cm_seiz_OffencesCommitted.Insert(offence);
                else
                {
                    offence.seizure_accused_offences_id = tempTableId;
                    val = BL_cm_seiz_OffencesCommitted.Update(offence);
                }
                if (val == "0")
                {
                    Response.Redirect("OffencesCommittedList");
                }
                else
                {
                    btnSaveasDraft.Enabled = true;
                    string message = val;
                    ValidateScript(message);
                }

            }
        }

        private void ValidateScript(string message)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("OffencesCommittedList");
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(CheckBox1.Checked)
            {
               OACT.Visible = true;
            }
            else
                OACT.Visible = false;
        }

        protected void ddlAccusedName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAccusedName.SelectedValue != "Select")
            {
                DataTable dt = new DataTable();
                dt = BL_cm_seiz_AccusedDetails.GetSearchDetailsID(ddlAccusedName.SelectedValue);
                grdCaseHistory.DataSource = dt;
                grdCaseHistory.DataBind();
                Divdummy11.Visible = false;
            }
        }
    }
}