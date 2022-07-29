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
   
    public partial class RaidTeamForm : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        List<cm_seiz_RaidTeam> raids = new List<cm_seiz_RaidTeam>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (ViewState["Records"] == null)
                {
                    dt.Columns.Add("Status");
                    dt.Columns.Add("Doc_Name");
                    dt.Columns.Add("Discription");
                    dt.Columns.Add("Doc_Path");
                    dt.Columns.Add("Doc_id");

                    ViewState["Records"] = dt;
                }
                Session["rtype1"] = Session["rtype"];
                //List<Designation_Details> Designationtype = new List<Designation_Details>();
                //Designationtype = BL_Designation_Details.GetDtypeList();
                List<Designation_Details> Designation = new List<Designation_Details>();
                Designation = BL_Designation_Details.GetDList();
                List<Designation_Details> Designation1 = new List<Designation_Details>();
                Designation1 = BL_Designation_Details.GetDListdesignationtype();
             
                var desi1 = (from sa in Designation1
                             select new 
                             {
                                 designation_type_code = sa.designation_type_code,
                                 designation_type = sa.designation_type
                                 //Qty = names.Field<int>("Qty")

                             }).ToList();
                ddlRaidTeamLeadBy.DataSource = desi1;
                ddlRaidTeamLeadBy.DataTextField = "designation_type";
                ddlRaidTeamLeadBy.DataValueField = "designation_type_code";
                ddlRaidTeamLeadBy.DataBind();
                ddlRaidTeamLeadBy.Items.Insert(0, "Select");
                if (Session["RaidBy"].ToString() == "E" || Session["RaidBy"].ToString() == "Excise")
                {
                    var desi = (from s in Designation
                                where s.designation_type_code == "REX"
                                select s);
                    ddlDesignation.DataSource = desi.ToList();
                    ddlDesignation.DataTextField = "designation_name";
                    ddlDesignation.DataValueField = "designation_code";
                    ddlDesignation.DataBind();
                    ddlDesignation.Items.Insert(0, "Select");
                }
                else
                {
                    var desi = (from s in Designation
                                where s.designation_type_code == "RPO"
                                select s);
                    ddlDesignation.DataSource = desi.ToList();
                    ddlDesignation.DataTextField = "designation_name";
                    ddlDesignation.DataValueField = "designation_code";
                    ddlDesignation.DataBind();
                    ddlDesignation.Items.Insert(0, "Select");
                }

                if (Session["rtype"].ToString()!="0")
                {
                    string seizureNo = Session["seizureNo"].ToString();
                 
                    raids = BL_cm_seiz_RaidTeam.GetDeails(seizureNo);
                    var val = Session["TableId"].ToString();
                    var raid = (from s in raids
                              where s.seizure_raiddetails_id == Session["TableId"].ToString()
                              select s);
                    txtOfficerName.Text = raid.ToList()[0].officername;
                    ddlDesignation.SelectedValue = raid.ToList()[0].designation_code;
                    ddlRaidTeamLeadBy.SelectedValue = raid.ToList()[0].raidteamcode;
                   
                    txtMobile.Text = raid.ToList()[0].mobileno;
                    if (txtMobile.Text == "0")
                        txtMobile.Text = "";
                    grdAdd.DataSource = null;
                    // txtOfficerName.Text = raid.ToList()[0].officername;
                    Doc_id = 0;
                    try
                    {
                        for (int i = 0; i < raid.ToList()[0].docs.Count; i++)
                        {
                            if (i == 0)
                                dummytable.Visible = false;
                            dt = (DataTable)ViewState["Records"];
                            dt.Rows.Add("1", raid.ToList()[0].docs[i].doc_name, raid.ToList()[0].docs[i].description, raid.ToList()[0].docs[i].doc_path, raid.ToList()[0].docs[i].seizure_docs_id);
                            grdAdd.DataSource = dt;
                            grdAdd.DataBind();
                            Doc_id++;

                        }
                    }
                    
                    catch (Exception exe)
                    {

                        
                    }

                    for (int i = 0; i < raid.ToList()[0].docs.Count; i++)
                    {
                        if (raid.ToList()[0].record_status == "Y")
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
                    //if (Session["rtype"].ToString() == "1" || raids.ToList()[0].record_status == "Y")
                    if (Session["rtype"].ToString() == "1")
                    {
                        txtOfficerName.ReadOnly = true;
                        ddlDesignation.Enabled = false;
                        ddlRaidTeamLeadBy.Enabled = false;                       
                        btnSaveasDraft.Visible = false;
                        btnCancel.Visible = false;
                        idupDocument.Enabled = false;
                        txtDiscription.Enabled = false;
                        btnUpload.Enabled = false;
                        docs.Visible = false;
                        txtMobile.Enabled = false;
                        //Designation1 = BL_Designation_Details.GetDListdesignationtype();
                        //ddlRaidTeamLeadBy.DataSource = Designation1;
                        //ddlRaidTeamLeadBy.DataTextField = "designation_type";
                        //ddlRaidTeamLeadBy.DataValueField = "designation_type_code";
                        //ddlRaidTeamLeadBy.DataBind();
                        //ddlRaidTeamLeadBy.Items.Insert(0, ddlRaidTeamLeadBy.SelectedValue);

                        //ddlDesignation.DataSource = Designation;
                        //ddlDesignation.DataTextField = "designation_name";
                        //ddlDesignation.DataValueField = "designation_code";
                        //ddlDesignation.DataBind();
                        //ddlDesignation.Items.Insert(0, ddlDesignation.SelectedValue);
                    }
                }
            }
        }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {

            Response.Redirect("RaidTeamList");
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

        protected void btnRaidTeamll_Click(object sender, EventArgs e)
        {
            Response.Redirect("RaidTeamList");
         
        }
        protected void btnWitness_Click(object sender, EventArgs e)
        {
            Response.Redirect("WitnessList");
        

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
                    dt.Rows.Add("", fileName, txtDiscription.Text, path, Doc_id);
                    grdAdd.DataSource = dt;
                    grdAdd.DataBind();

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    (grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = true;
                    (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = true;
                        
               }
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
                cm_seiz_RaidTeam raid = new cm_seiz_RaidTeam();
                raid.officername = txtOfficerName.Text;
                raid.designation_code = ddlDesignation.SelectedValue;
                raid.raidteamlead = ddlRaidTeamLeadBy.SelectedValue;
                raid.seizureno = Session["seizureno"].ToString();
                raid.user_id = Session["UserID"].ToString();
                raid.raidby = Session["RaidBy"].ToString().Substring(0, 1);
                string strHostName = System.Net.Dns.GetHostName();
                string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                raid.ipaddress = clientIPAddress.ToString();
                raid.mobileno = txtMobile.Text;
                if (raid.mobileno == "")
                    raid.mobileno = "0";
                dt = ViewState["Records"] as DataTable;
                int i = 0;
                raid.docs = new List<Seizure_Docs>();
                foreach (DataRow dr in dt.Rows)
                {
                    Seizure_Docs doc = new Seizure_Docs();
                    doc.doc_name = dr["Doc_Name"].ToString();
                    doc.doc_path = (grdAdd.Rows[i].FindControl("lblFilePath") as Label).Text;
                    doc.description = (grdAdd.Rows[i].FindControl("lblDiscriptione") as Label).Text;
                    doc.seizureno = Session["seizureno"].ToString();

                    raid.docs.Add(doc);
                    i++;
                }
                string val;

                string tempTableId = Session["tableId"]?.ToString() ?? string.Empty;
                if (Session["rtype"].ToString() == "0" && string.IsNullOrEmpty(tempTableId))
                    val = BL_cm_seiz_RaidTeam.Insert(raid);
                else
                {
                    raid.seizure_raiddetails_id = Session["tableId"].ToString();
                    val = BL_cm_seiz_RaidTeam.Update(raid);
                }
                if (val == "0")
                {
                    Response.Redirect("RaidTeamList");
                }
                else
                {
                    btnSaveasDraft.Enabled = true;
                    string message = val;
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("RaidTeamList");
        }

        protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Designation_Details> Designation = new List<Designation_Details>();
            Designation = BL_Designation_Details.GetDList();
            var ad = (from s in Designation
                      where s.designation_type_code ==ddlDesignation.SelectedValue
                      select s);
            ddlDesignation.DataSource = ad.ToList();
            ddlDesignation.DataTextField = "designation_name";
            ddlDesignation.DataValueField = "designation_code";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, "Select");
        }
    }
}