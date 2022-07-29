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

namespace UserMgmt
{
    public partial class User_Profile : System.Web.UI.Page
    {
        UserDetails user = new UserDetails();
        DataTable dt = new DataTable();
        static int Doc_id = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                List<District> districts = new List<District>();
                List<Designation_Details> designation = new List<Designation_Details>();
                districts = BL_User_Mgnt.GetAllDistrictsList();
                var list = from s in districts
                           select s;
                ddlDistrict.DataSource = list.ToList();
                ddlDistrict.DataTextField = "district_name";
                ddlDistrict.DataValueField = "district_code";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, "Select");
                designation = BL_Designation_Details.GetDList();
                ddlDesignation.DataSource = designation.ToList();
                ddlDesignation.DataTextField = "designation_name";
                ddlDesignation.DataValueField = "designation_code";
                ddlDesignation.DataBind();
                ddlDistrict.Items.Add(new ListItem("All", "AL"));
                ddlDesignation.Items.Insert(0, "Select");
                UserDetails user1 = new UserDetails();
                user1 = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                user = BL_UserDetails.GetProfileUser(Session["UserID"].ToString(),user1.mobile);
                if(user!=null)
                {
                    Session["User_id"] = user.id;
                    txtnameofofficer.Text = user.user_name;
                    ddlDesignation.SelectedValue = user.designation_code;
                  
                    btnDownloadMf1Attachment.Text = user.photoname;
                    ddlDistrict.SelectedValue = user.district_code;
                    txtMobileNo.Text = user.mobile;
                    if(user.user_dob.Length>9)
                    txtDOB.Text = user.user_dob.Substring(0,10).Replace("/","-");
                    txtdateofjoining.Text = user.date_of_joining;
                    txtdateofpresent.Text = user.date_of_posting;
                    txtretairement.Text = user.date_of_retairment;
                    txtPranNo.Text = user.pran_no;
                    txtemail.Text = user.email_id;
                    txtbloodgroup.Text = user.blood_group;
                    txtContactNo.Text = user.emergency_contact;
                }
            }
        }

        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                user = new UserDetails();
                user.user_name = txtnameofofficer.Text;
                user.designation_code = ddlDesignation.SelectedValue;
                user.district_code = ddlDistrict.SelectedValue;
                user.mobile = txtMobileNo.Text;
                user.user_dob = txtDOB.Text;
                user.date_of_joining = txtdateofjoining.Text;
                user.date_of_posting = txtdateofpresent.Text;
                user.date_of_retairment = txtretairement.Text;
                user.blood_group = txtbloodgroup.Text;
                user.pran_no = txtPranNo.Text;
                user.emergency_contact = txtContactNo.Text;
                user.user_id = Session["UserID"].ToString();
                user.id =Convert.ToInt32( Session["User_id"].ToString());
                user.email_id = txtemail.Text;
                if (Upload_Photo() != "")
                    user.photoname = Upload_Photo();
                else
                    user.photoname = btnDownloadMf1Attachment.Text;
                user.blood_group = txtbloodgroup.Text;
                bool val = BL_cm_seiz_Daily_Dairy_Raid.UserUpdate(user);
                if(val)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append("Updated Sucessfully");
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    Response.Redirect("User_Profile.aspx");
                }
                else
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append("Not Updated");
                    sb.Append("')};");                                                                        
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                }
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string reportName = btnDownloadMf1Attachment.Text;

            string filePath = reportName;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
        }
        protected void btnDownloadmf1_Click(object sender, EventArgs e)
        {
            string reportName = btnDownloadMf1Attachment.Text;

            string filePath = reportName;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
        }
        private string Upload_Photo()
        {
            StringBuilder sMessage = new StringBuilder();
            string file = "";
            if (idproofimage.HasFile)
            {
                if (idproofimage.PostedFile.FileName.Length > 0)
                {
                    string fileName = Path.GetFileName(idproofimage.PostedFile.FileName);
                    FileInfo fi = new FileInfo(fileName);
                    string[] filetype = fileName.Replace("'", "").Split('.');
                    idproofimage.PostedFile.SaveAs(Server.MapPath("~/photos/") + Session["UserID"].ToString() + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh_mm_ss") + fi.Extension);
                    file = "~/photos/" + Session["UserID"].ToString() + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh_mm_ss") + fi.Extension;
                }

            }
            return file;
        }
    }
}