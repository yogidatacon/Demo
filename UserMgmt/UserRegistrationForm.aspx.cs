using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;
using AjaxControlToolkit;
using Newtonsoft.Json.Linq;
using System.Collections;

using System.Web.Hosting;

namespace UserMgmt
{
    public partial class UserRegistrationForm : System.Web.UI.Page
    {
        NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");

        List<Org_Master> org_name = new List<Org_Master>();
        List<Party_Master> party = new List<Party_Master>();
        List<RoleLevel> rolelevel = new List<RoleLevel>();
        List<AccessType> accesstype = new List<AccessType>();
        List<RoleMaster> roles = new List<RoleMaster>();
        List<State> statelist = new List<State>();
        List<Division> divisions = new List<Division>();
        List<District> Districts = new List<District>();
        List<UserDetails> users = new List<UserDetails>();
        List<Department> Department = new List<Department>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // string v = DateTime.Now.ToString("dd-MM-yyyy_hh_mm_ss");
                string strPreviousPage = "";
                if (Request.UrlReferrer != null)
                {

                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
               
                string userid = Session["UserID"].ToString();
              //  string rtype = Session["rtype"].ToString();
                GetDropDownValues(Session["UserID"].ToString());
               
                users = new List<UserDetails>();
                users = BL_UserDetails.GetUserList(userid);
                if(Session["rtype"].ToString()!="0")
                {
                    //txtpassword.Text = "12345678";
                    //txtrepassword.Text= "12345678";
                    //txtpassword.Attributes.Add("readonly", "readonly");
                    //txtrepassword.Attributes.Add("readonly", "readonly");
                     txtuserid.ReadOnly = true;
                    string id = Session["id"].ToString();
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.GetUserDetails(id);
                    txtuserName.Text = user.user_name;
                    //txtDATE_OF_BIRTH.Text = user.user_dob.ToString().Replace("/","-").Substring(0,10);
                    txtuserid.Text = user.user_id;
                    txtpassword.Text ="0";
                    txtrepassword.Text = "..........";
                    pass.Visible = false;
                    repass.Visible = false;
                    address.Text = user.user_address;
                    txtmobile.Text = user.mobile;
                    txtemail.Text = user.email_id;
                    ddOrgnames.SelectedValue = user.org_id.ToString();
                    ddAccessType.SelectedValue = user.access_type_code.ToString();
                    ddRoleLevel.SelectedValue = user.role_level_code.ToString();
                    ddRole.SelectedValue = user.role_name_code.ToString();
                    ddStates.SelectedValue = user.state_code;
                    ddStates_SelectedIndexChanged(sender, e);
                    ddDivisions.SelectedValue = user.division_code;
                    ddDivisions_SelectedIndexChanged(sender, e);
                    ddDistricts.SelectedValue = user.district_code;
                    ddlDesignation.SelectedValue = user.designation_code;
                    ddlDepartment.SelectedValue=user.department_name;
                    btnDownloadMf1Attachment.Text= user.photoname;
                    ddlparty.SelectedValue = user.party_code; 
                    //txtdob.Value = user.user_dob;

                   // btnDownloadMf1Attachment.Visible = true;
                    if (Session["rtype"].ToString()=="1")
                    {
                        //btnDownload.Visible =true;
                        btnUpload.Visible = false;
                        //txtDATE_OF_BIRTH.Enabled = false;
                        txtemail.Enabled = false;
                        ddlDepartment.Enabled = false;
                        txtuserid.Enabled = false;
                        txtuserName.Enabled = false;
                        address.Enabled = false;
                        txtmobile.Enabled = false;
                      //  comments.Enabled = false;
                        ddAccessType.Enabled = false;
                        ddDistricts.Enabled = false;
                        ddDivisions.Enabled = false;
                        ddOrgnames.Enabled = false;
                        ddRole.Enabled = false;
                        ddRoleLevel.Enabled = false;
                        ddStates.Enabled = false;
                        ddlparty.Enabled = false;
                        //  txtUploadHiden.Visible = false;
                        btnUpload.Visible = false;
                      //  txtUploadHiden.Visible = false;
                     //   UploadPhoto.Visible = false;
                        //txtDATE_OF_BIRTH.BackColor = System.Drawing.Color.LightGray;
                        txtuserid.BackColor = System.Drawing.Color.LightGray;
                      //  txtpassword.BackColor = System.Drawing.Color.LightGray;
                        txtrepassword.BackColor = System.Drawing.Color.LightGray;
                        txtemail.BackColor = System.Drawing.Color.LightGray;
                        txtuserName.BackColor = System.Drawing.Color.LightGray;
                        address.BackColor = System.Drawing.Color.LightGray;
                        txtmobile.BackColor = System.Drawing.Color.LightGray;
                      //  comments.BackColor = System.Drawing.Color.LightGray;
                        ddAccessType.BackColor = System.Drawing.Color.LightGray;
                        ddDistricts.BackColor = System.Drawing.Color.LightGray;
                        ddDivisions.BackColor = System.Drawing.Color.LightGray;
                        ddOrgnames.BackColor = System.Drawing.Color.LightGray;
                        ddRole.BackColor = System.Drawing.Color.LightGray;
                        ddRoleLevel.BackColor = System.Drawing.Color.LightGray;
                        ddStates.BackColor = System.Drawing.Color.LightGray;
                        ddlDepartment.BackColor = System.Drawing.Color.LightGray;
                        ddlparty.BackColor = System.Drawing.Color.LightGray;
                        txtpassword.Text = "..........";
                        txtrepassword.Text = "..........";
                    }
                    else
                    {
                      btnDownload.Visible = true;
                    }
                   
                }
                else
                {
                  btnDownload.Visible = false;
                }
                }
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/LoginPage");
                }
            }
            }
        public string encryption(String password)
        {

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            //encrypt the given password string into Encrypted data  
            encrypt = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder encryptdata = new StringBuilder();
            //Create a new string by using the encrypted data  
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }

        private void GetDropDownValues(string userid)
        {

            List<Org_Master> org_master = new List<Org_Master>();
            org_master = BL_org_Master.GetListValues(Session["UserID"].ToString());
            var org_master1 = from s in org_master
                              where s.status == "Active"
                              select s;
            ddOrgnames.DataSource = org_master1.ToList();
            ddOrgnames.DataTextField = "org_name";
            ddOrgnames.DataValueField = "org_id";
            ddOrgnames.DataBind();
            ddOrgnames.Items.Insert(0, "Select");
            party = new List<Party_Master>();
            party = BL_Party_Master.GetList();
            ddlparty.DataSource = party;
            ddlparty.DataTextField = "party_name";
            ddlparty.DataValueField = "party_code";
            ddlparty.DataBind();
            ddlparty.Items.Insert(0, "Select");
           
            rolelevel = new List<RoleLevel>();
            rolelevel = BL_User_Mgnt.GetRoleLevels(userid);
            ddRoleLevel.DataSource = rolelevel;
            ddRoleLevel.DataTextField = "levelname";
            ddRoleLevel.DataValueField = "role_level_code";
            ddRoleLevel.DataBind();
            ddRoleLevel.Items.Insert(0, "Select");
            accesstype = new List<AccessType>();
            accesstype = BL_User_Mgnt.GetAccessTypeList(userid);
            ddAccessType.DataSource = accesstype;
            ddAccessType.DataTextField = "access_type_name";
            ddAccessType.DataValueField = "access_type_code";
            ddAccessType.DataBind();
            ddAccessType.Items.Insert(0, "Select");
            roles = new List<RoleMaster>();
            roles = BL_User_Mgnt.GetRoleMasterList(userid);
            ddRole.DataSource = roles;
            ddRole.DataTextField = "rolename";
            ddRole.DataValueField = "rolecode";
            ddRole.DataBind();
            ddRole.Items.Insert(0, "Select");
            ///////////////////////////////////////////////////
            statelist = new List<State>();
            statelist = BL_User_Mgnt.GetListValues(userid);
            ddStates.DataSource = statelist;
            ddStates.DataTextField = "State_Name";
            ddStates.DataValueField = "State_Code";
            ddStates.DataBind();
            ddStates.Items.Insert(0, "Select");
            Department = new List<Department>();
            Department = BL_User_Mgnt.GetDeptList(userid);
            ddlDepartment.DataSource = Department;
            ddlDepartment.DataTextField = "Dept_Name";
            ddlDepartment.DataValueField = "Dept_Code";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, "Select");
           List<Employee_Details> emp = new List<Employee_Details>();
            emp = BL_Employee_Details.GetList();
            ddlEmp_code.DataSource = emp;
            ddlEmp_code.DataTextField = "emp_code";
            ddlEmp_code.DataValueField ="employee_master_id";
            ddlEmp_code.DataBind();
            ddlEmp_code.Items.Insert(0, "Select");
            List<Designation_Details> des = new List<Designation_Details>();
            des = BL_Designation_Details.GetDList();
            ddlDesignation.DataSource = des;
            ddlDesignation.DataTextField = "designation_name";
            ddlDesignation.DataValueField = "designation_code";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, "Select");

        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
          
        }
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
          
            //DateTime dob = Convert.ToDateTime(txtDate.Text.ToString());
            //int dobDate = Convert.ToInt32(dob.ToString("yyyy"));
            //var curDate = DateTime.Now.ToString("dd/MM/yyyy");

            //var curYr = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
            //int yrDif = curYr - dobDate;
            //int age = yrDif;
            //if (yrDif < 21)
            //{
            //    txtDate.Text = "";
            //    string message = "Age should be greater than 21 years";
            //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //    sb.Append("<script type = 'text/javascript'>");
            //    sb.Append("window.onload=function(){");
            //    sb.Append("alert('");
            //    sb.Append(message);
            //    sb.Append("')};");
            //    sb.Append("</script>");
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
            //    txtDate.Focus();
                
            //}
            //else
            //{
               
            //}
        }
        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            //if (txtDate.Text != "")
            //{
            //    DateTime dob = Convert.ToDateTime(txtDate.Text.ToString());
            //    int dobDate =Convert.ToInt32( dob.ToString("yyyy"));
            //    var curDate =DateTime.Now.ToString("dd/MM/yyyy");

            //    var curYr = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
            //    int yrDif = curYr - dobDate;
            //    int age = yrDif;
            //    if (yrDif < 21)
            //    {
            //       txtDate.Text= "";
            //        string message = "Age should be greater than 21 years";
            //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //        sb.Append("<script type = 'text/javascript'>");
            //        sb.Append("window.onload=function(){");
            //        sb.Append("alert('");
            //        sb.Append(message);
            //        sb.Append("')};");
            //        sb.Append("</script>");
            //        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
            //        txtDate.Focus();
            //    }
            //    else
            //    {

            //      //  txtage.Value = age.ToString();
            //    }
            //}
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                btnSave.Enabled = false;
               // CalendarExtender.EndDate = DateTime.Now;
                if (Session["rtype"].ToString() == "0")
                {
                  
                    string password = encryption(txtpass.Value);
                    UserDetails user = new UserDetails();
                    user.user_name = txtuserName.Text;
                    user.user_id = txtuserid.Text;
                  //  txtDATE_OF_BIRTH.Text = txtdob.Value;
                  //  user.user_dob = txtDATE_OF_BIRTH.Text.Replace("/", "-");
                    int currentyear = DateTime.Now.Year;
                 //   int byear =Convert.ToInt32( txtDATE_OF_BIRTH.Text.Replace("/", "-").Substring(6,4));
                  //  int age1 = currentyear - byear;
                  //  user.user_age = age1.ToString() ;
                    user.user_password = password;
                    user.user_address = address.Text;
                    user.mobile = txtmobile.Text;
                    user.email_id = txtemail.Text;
                    user.org_id = Convert.ToInt32(ddOrgnames.SelectedValue.ToString());
                    user.role_level_code = Convert.ToInt32(ddRoleLevel.SelectedValue.ToString());
                    user.role_name_code = Convert.ToInt32(ddRole.SelectedValue.ToString());
                    user.access_type_code = Convert.ToInt32(ddAccessType.SelectedValue.ToString());
                    user.state_code = ddStates.SelectedValue.ToString();
                    user.division_code = ddDivisions.SelectedValue.ToString();
                    user.district_code = ddDistricts.SelectedValue.ToString();
                    user.department_name = ddlDepartment.SelectedValue.ToString();
                    user.designation_code = ddlDesignation.SelectedValue;
                    user.party_code = ddlparty.SelectedValue.ToString();
                    if (ddlEmp_code.SelectedValue == "Select")
                        user.emp_id = "0";
                    else
                        user.emp_id = ddlEmp_code.SelectedValue;
                    user.photoname =Upload_Photo();
                    if (BL_UserDetails.InsertUser(user))
                    {
                        string message = "Successfully Submitted.";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                        Session["UserID"] = Session["UserID"].ToString();
                        Response.Redirect("UserRegistrationList");
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        string message = "Server Side Error.";
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
                    UserDetails user = new UserDetails();
                    user.user_name = txtuserName.Text;
                    user.user_id = txtuserid.Text;
                  //  txtDATE_OF_BIRTH.Text = txtdob.Value;
                  //  user.user_dob = txtDATE_OF_BIRTH.Text.Replace("/", "-");
                  //  int currentyear = DateTime.Now.Year;
                  //  int byear = Convert.ToInt32(txtDATE_OF_BIRTH.Text.Replace("/", "-").Substring(6, 4));
                  //  int age1 = currentyear - byear;
                  //  user.user_age = age1.ToString();
                    user.user_password = txtpassword.Text;
                    user.user_address = address.Text;
                    user.mobile = txtmobile.Text;
                    user.email_id = txtemail.Text;
                    user.org_id = Convert.ToInt32(ddOrgnames.SelectedValue.ToString());
                    //   user.role_level_code = Convert.ToInt32(ddRoleLevel.SelectedValue.ToString());
                    user.role_name_code = Convert.ToInt32(ddRole.SelectedValue.ToString());
                    user.access_type_code = Convert.ToInt32(ddAccessType.SelectedValue.ToString());
                    user.state_code = ddStates.SelectedValue.ToString();
                    user.division_code = ddDivisions.SelectedValue.ToString();
                    user.district_code = ddDistricts.SelectedValue.ToString();
                    user.role_level_code =Convert.ToInt32( ddRoleLevel.SelectedValue.ToString());
                    user.department_name = ddlDepartment.SelectedValue.ToString();
                    user.designation_code = ddlDesignation.SelectedValue;
                    user.party_code = ddlparty.SelectedValue.ToString();
                    if (ddlEmp_code.SelectedValue == "Select")
                        user.emp_id = "0";
                    else
                        user.emp_id = ddlEmp_code.SelectedValue;
                    if (Upload_Photo() != "")
                        user.photoname = Upload_Photo();
                    else
                        user.photoname = btnDownloadMf1Attachment.Text;
                    user.id = Convert.ToInt32(Session["id"].ToString());
                    if (BL_UserDetails.UpdateUser(user))
                    {
                        string message = "Successfully Updated.";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                        Session["UserID"] = Session["UserID"].ToString();
                        Response.Redirect("UserRegistrationList");
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        string message = "Server Side Error.";
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
                 
                    idproofimage.PostedFile.SaveAs(Server.MapPath("~/photos/") + txtuserid.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh_mm_ss")  + fi.Extension);
                    file = "~/photos/" + txtuserid.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh_mm_ss") + fi.Extension;
                }
               
            }
            return file;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("UserRegistrationList");
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("UserRegistrationList");
        }
        protected void btnDownloadMf1Attachment_Click(object sender, EventArgs e)
        {
            //string reportName = SessionObject.Get<string>("userphoto");
            //if (File.Exists(Server.MapPath("~/userphoto") + "\\" + reportName))
            //{
            //    Response.ContentType = "application/msword";
            //    Response.AddHeader("content-disposition", "attachment;filename=\"" + reportName + "\"");
            //    Response.WriteFile(Server.MapPath("~/userphoto/Attachments") + "\\" + reportName);
            //    Response.End();
            //}
        }
       
        protected void btnDownloadmf1_Click(object sender, EventArgs e)
        {
            string reportName = btnDownloadMf1Attachment.Text;

            string filePath = reportName;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
        }

        //[System.Web.Services.WebMethod]
        //public static string GetDownloadAttachment(Upload_File file)
        //{
        //    SessionObject.Set<string>("userphoto", file.File);
        //    return file.File;
        //}
       

        //protected void txtuserid_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsPostBack)
        //    {
        //        int value = BL_User_Mgnt.GetExistsData("user_registration", "User_id", txtuserid.Text);
        //        if (value > 0)
        //        {
        //            string message = "UserName is Already Exists.";
        //            System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //            sb.Append("<script type = 'text/javascript'>");
        //            sb.Append("window.onload=function(){");
        //            sb.Append("alert('");
        //            sb.Append(message);
        //            sb.Append("')};");
        //            sb.Append("</script>");
        //            ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
        //            txtuserid.Text = "";
        //            txtuserid.Focus();
        //        }
        //    }
        //}

        protected void txtrepassword_TextChanged(object sender, EventArgs e)
        {
           
        }
        [WebMethod]
        public static string chkDuplicateUserIDData(Object User_id)
        {
            // IEnumerable myList = email_id as IEnumerable;
            int value = BL_User_Mgnt.GetExistsData("user_registration", "User_id", User_id.ToString());


            return value.ToString();
        }

        [WebMethod]
        public static string chkDuplicateEmailData(Object email_id)
        {
           // IEnumerable myList = email_id as IEnumerable;
            int value = BL_User_Mgnt.GetExistsData("user_registration", "email_id", email_id.ToString());
           

            return value.ToString();
        }

        protected void ddStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            divisions = new List<Division>();
            divisions = BL_User_Mgnt.GetDivisions(ddStates.SelectedValue);
            ddDivisions.DataSource = divisions;
            ddDivisions.DataTextField = "Division_Name";
            ddDivisions.DataValueField = "Division_Code";
            ddDivisions.DataBind();
            ddDivisions.Items.Insert(0, "Select");

           
        }

        protected void ddDivisions_SelectedIndexChanged(object sender, EventArgs e)
        {
            Districts = new List<District>();
            Districts = BL_User_Mgnt.GetDistricts(ddDivisions.SelectedValue);
            var org_master1 = from s in Districts
                              where s.division_Code ==ddDivisions.SelectedValue
                              select s;
            ddDistricts.DataSource = org_master1.ToList();
            ddDistricts.DataTextField = "District_Name";
            ddDistricts.DataValueField = "District_Code";
            ddDistricts.DataBind();
            ddDistricts.Items.Insert(0, "Select");
        }
        protected void UserRegistration_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("UserRegistrationList");
        }
        protected void Employee_Details_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("EmployeeList");
        }

        protected void Designation_1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("DepartmentMasterList.aspx");
        }

        protected void Designation_2_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("DesignationList");
        }

        protected void ddlEmp_code_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                if(ddlEmp_code.SelectedValue!="Select")
                {
                    Employee_Details emp = new Employee_Details();
                    emp = BL_Employee_Details.GetDetails(ddlEmp_code.SelectedValue);
                    txtuserName.Text = emp.emp_name;
                    txtmobile.Text = emp.mobile;
                    txtemail.Text = emp.email_id;
                    address.Text = emp.present_address;
                    ddStates.SelectedValue = emp.state_code;
                   // emp.district_code = "01";
                    ddStates_SelectedIndexChanged(sender, null);
                    ddDivisions.SelectedValue = emp.division_code;
                    ddDivisions_SelectedIndexChanged(sender, null);
                    ddDistricts.SelectedValue = emp.district_code;
                    ddlDepartment.SelectedValue = emp.department_code;
                    txtuserName.ReadOnly = true;
                    txtmobile.ReadOnly = true;
                    txtemail.ReadOnly = true;
                    address.ReadOnly = true;
                    ddStates.Enabled = false;
                    ddDivisions.Enabled = false;
                    ddDistricts.Enabled = false;
                    ddlDepartment.Enabled = false;
                }
                
            }
        }
    }
}