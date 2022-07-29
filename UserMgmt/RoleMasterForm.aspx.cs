using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;
using Newtonsoft.Json;
using System.Web.Services;
using System.Text.RegularExpressions;

namespace UserMgmt
{
    public partial class RoleMasterForm : System.Web.UI.Page
    {
        List<Org_Master> org_name = new List<Org_Master>();
        List<RoleLevel> rolelevel = new List<RoleLevel>();
        List<AccessType> accesstype = new List<AccessType>();
        List<RoleMaster> rolemaster = new List<RoleMaster>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
                string rtype = Session["rtype"].ToString();
                Session["rtype"] = rtype;
               
                GetDropDownValues(Session["UserID"].ToString());
                if (rtype != "0")
                {
                    List<UserDetails> users = new List<UserDetails>();
                    users = BL_UserDetails.GetUserList("");
                    string v = Session["role_id"].ToString();
                    string dc = Session["rolename"].ToString().Trim();
                    var list = from s in users
                               where s.role_name==Session["rolename"].ToString().Trim()
                               select s;
                    if (list.ToList().Count > 0)
                    {
                        txtRoleName.Attributes.Add("disabled", "disabled");
                        txtRoleName.Attributes.Add("disabled", "disabled");
                    }
                    txtRoleName.Text = Session["rolename"].ToString();
                    if (Session["strenth"].ToString() == "0")
                        txtSanctionStrength.Text = "";
                    else
                        txtSanctionStrength.Text = Session["strenth"].ToString();
                    ddAccessType.SelectedValue= Session["role_access_type_code"].ToString(); 
                    ddNextrole.SelectedValue= Session["nexrole_code"].ToString();
                    ddorgnnames.SelectedValue= Session["org_id"].ToString(); ;
                    ddRolelevels.SelectedValue= Session["role_level_code"].ToString();
                    Session["role_id"] = Session["role_id"].ToString();
                    lblid.Value = Session["role_id"].ToString();
                    if (rtype == "1" || list.ToList().Count > 0)
                    {
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        txtRoleName.Enabled = false;
                        txtSanctionStrength.Enabled = false;
                        ddAccessType.Enabled = false;
                        ddNextrole.Enabled = false;
                        ddorgnnames.Enabled = false;
                        ddRolelevels.Enabled = false;
                        txtRoleName.BackColor = System.Drawing.Color.LightGray;
                        txtSanctionStrength.BackColor = System.Drawing.Color.LightGray;
                        ddAccessType.BackColor = System.Drawing.Color.LightGray;
                        ddNextrole.BackColor = System.Drawing.Color.LightGray;
                        ddorgnnames.BackColor = System.Drawing.Color.LightGray;
                        ddRolelevels.BackColor = System.Drawing.Color.LightGray;

                    }
                   else if (rtype == "2")
                    {
                        btnSave.Text = "Save";
                        btnCancel.Text = "Cancel";
                    }
                }
                else
                {
                    //int n = Convert.ToInt32(BL_org_Master.GetMaxID("role_master"));
                    //lblid.Value = (n + 1).ToString();
                    // Session["role_id"] = (n + 1).ToString();
                    btnSave.Text = "Submit";
                    btnCancel.Text = "Cancel";
                }
            }
        }

        private void GetDropDownValues(string  userid)
        {

            List<Org_Master> org_master = new List<Org_Master>();
            org_master = BL_org_Master.GetListValues(Session["UserID"].ToString());
            var org_master1 = from s in org_master
                              where s.status == "Active"
                              select s;
            ddorgnnames.DataSource = org_master1.ToList();
            ddorgnnames.DataTextField = "org_name";
            ddorgnnames.DataValueField = "org_id";
            ddorgnnames.DataBind();
            ddorgnnames.Items.Insert(0, "Select");
            rolelevel = new List<RoleLevel>();
            rolelevel = BL_User_Mgnt.GetRoleLevels(userid);
            ddRolelevels.DataSource = rolelevel;
            ddRolelevels.DataTextField = "levelname";
            ddRolelevels.DataValueField = "role_level_code";
            ddRolelevels.DataBind();
            ddRolelevels.Items.Insert(0, "Select");
            accesstype = new List<AccessType>();
            accesstype = BL_User_Mgnt.GetAccessTypeList(userid);
            ddAccessType.DataSource = accesstype;
            ddAccessType.DataTextField = "access_type_name";
            ddAccessType.DataValueField = "access_type_code";
            ddAccessType.DataBind();
            ddAccessType.Items.Insert(0, "Select");
            rolemaster = new List<RoleMaster>();
            rolemaster = BL_User_Mgnt.GetRoleMasterList(userid);
            ddNextrole.DataSource = rolemaster;
            ddNextrole.DataTextField = "rolename";
            ddNextrole.DataValueField = "rolecode";
            ddNextrole.DataBind();
            ddNextrole.Items.Insert(0, "Select");
            ddNextrole.Items.Insert(1, "NA");
        }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/RoleMasterList1");
        }
        protected void StateMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/StateList");
        }
        protected void DivisionMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/DivisionList");
        }
        protected void DistrictMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/Districtlist");

        }
        protected void RoleLevelMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/RoleLevelList");
        }
        protected void AccessTypeMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/AccessTypeList");
        }
        protected void RoleMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/RoleMasterList1");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                btnSave.Enabled = false;
                RoleMaster rolemaster = new RoleMaster();
                rolemaster.rolecode = lblid.Value;
                string s = txtRoleName.Text;
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                rolemaster.rolename =s;
                rolemaster.rolelevel = ddRolelevels.SelectedValue;
                rolemaster.accestype = ddAccessType.SelectedValue;
                rolemaster.nextrole = ddNextrole.SelectedItem.ToString();
                rolemaster.nextroleCode = ddNextrole.SelectedValue.ToString();
                rolemaster.organization = ddorgnnames.SelectedValue;
                if (txtSanctionStrength.Text == "")
                    txtSanctionStrength.Text = "0";
                rolemaster.sanction_strength = txtSanctionStrength.Text;
                rolemaster.user_id = Session["UserID"].ToString();
               
                if (Session["rtype"].ToString() != "0")
                {
                    rolemaster.id = Convert.ToInt32(lblid.Value);
                    if (BL_User_Mgnt.UpdateRoleMaster(rolemaster))
                    {
                        string message = "Record is Successfully Updated.";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                        Session["UserID"] = Session["UserID"].ToString();
                        Response.Redirect("~/RoleMasterList1");
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
                    if (BL_User_Mgnt.InsertRoleMaster(rolemaster))
                    {
                        string message = "Record is Successfully Submited.";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                        Session["UserID"] = Session["UserID"].ToString();
                        Response.Redirect("~/RoleMasterList1");
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
        protected void ThanaMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ThanaMasterList");
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/RoleMasterList1");
        }
        static string role="";
        [WebMethod]
        public static string chkDuplicateRoleName(Object rolename)
        {
            int value = 0;
            if (role != rolename.ToString())
            {
                string s = rolename.ToString();
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                value = BL_User_Mgnt.GetExistsData("role_master", "role_name", s);
            }
            return value.ToString();
        }
        protected void txtLevelName_TextChanged(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static string SaveRolePermission(string roleobj)
        {
            JsonSerializerSettings serializationSettings = new JsonSerializerSettings();
            serializationSettings.NullValueHandling = NullValueHandling.Ignore;
            var rolePerssion_ViewModelObj = JsonConvert.DeserializeObject<RolePerssion_ViewModel>(roleobj, serializationSettings);
            string result = "success";
            return result;
        }
    }
}