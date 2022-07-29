using System;
using System.Collections.Generic;
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
    public partial class RoleLevelMasterForm : System.Web.UI.Page
    {
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
              
                if (rtype != "0")
                {
                    string level_id = Session["level_id"].ToString();
                    Session["level_id"] = level_id;
                    txtid.Value = level_id;
                    List<RoleMaster> rolemaster = new List<RoleMaster>();
                    rolemaster = BL_User_Mgnt.GetRoleMasterList("");
                    var list = from s in rolemaster
                               where s.rolelevel == Session["level_id"].ToString()
                               select s;
                    if (list.ToList().Count > 0)
                    {
                        txtLevelName.Attributes.Add("disabled", "disabled");
                        txtDescription.Attributes.Add("disabled", "disabled");
                        btnCancel.Visible = false;
                        btnSave.Visible = false;
                    }
                    if (rtype == "1")
                    {
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        txtLevelName.Text = Session["Level_Name"].ToString();
                        txtDescription.Text = Session["Description"].ToString();
                        txtid.Value = Session["Level_Id"].ToString();
                        txtDescription.Enabled = false;
                        txtLevelName.Enabled = false;
                        txtLevelName.BackColor = System.Drawing.Color.LightGray;
                        txtDescription.BackColor = System.Drawing.Color.LightGray;
                    }
                    if (rtype == "2")
                    {
                        txtLevelName.Text = Session["Level_Name"].ToString();
                        txtDescription.Text = Session["Description"].ToString();
                        txtid.Value = Session["Level_Id"].ToString();

                    }

                }
                else
                {
                    //int n = Convert.ToInt32(BL_org_Master.GetMaxID("role_level_master"));
                   // txtid.Value = (n + 1).ToString();
                    Session["Role_Level_ID"] = txtid.Value;
                    btnSave.Text = "Submit";
                    btnCancel.Text = "Cancel";
                }
            }
        }
        static string rolelevel="";
        [WebMethod]
        public static string chkDuplicateRoleLevelName(Object rolelevelname)
        {
            int value = 0;
            if (rolelevel != rolelevelname.ToString())
            {
                string s = rolelevelname.ToString();
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                value = BL_User_Mgnt.GetExistsData("Role_level_master", "role_level_name", s);
            }
            return value.ToString();
        }
        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/RoleLevelList");
        }

        protected void StateMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StateList");
        }

        protected void DistrictMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Districtlist");
        }

        protected void DivisionMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DivisionList");
        }

        protected void RoleLevelMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/RoleLevelList");
        }

        protected void AccessTypeMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AccessTypeList");
        }

        protected void RoleMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/RoleMasterList1");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            RoleLevel rolelevel = new RoleLevel();
            string s = txtLevelName.Text;
            s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
            rolelevel.levelname = s;
            rolelevel.role_level_code = txtid.Value;
            rolelevel.description = txtDescription.Text;
            rolelevel.user_id = Session["UserID"].ToString();
            if (Session["rtype"].ToString()!="0")
            {
                if (BL_User_Mgnt.UpdateRoleLevel(rolelevel))
                {
                    //string message = "Record is Updated.";
                    //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    //sb.Append("<script type = 'text/javascript'>");
                    //sb.Append("window.onload=function(){");
                    //sb.Append("alert('");
                    //sb.Append(message);
                    //sb.Append("')};");
                    //sb.Append("</script>");
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    Session["UserID"] = Session["UserID"].ToString();
                    Response.Redirect("~/RoleLevelList");
                }
                else
                {
                    btnSave.Enabled = true;
                }
            }
            else
            {
                if(BL_User_Mgnt.InsertRoleLevel(rolelevel))
                {
                    //string message = "Record is Updated.";
                    //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    //sb.Append("<script type = 'text/javascript'>");
                    //sb.Append("window.onload=function(){");
                    //sb.Append("alert('");
                    //sb.Append(message);
                    //sb.Append("')};");
                    //sb.Append("</script>");
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    Session["UserID"] = Session["UserID"].ToString();
                    Response.Redirect("~/RoleLevelList");
                }
                else
                {
                    btnSave.Enabled = true;
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/RoleLevelList");
        }
        protected void ThanaMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ThanaMasterList");
        }
        protected void txtLevelName_TextChanged(object sender, EventArgs e)
        {
            int value = BL_User_Mgnt.GetExistsData("role_level_master", "role_level_name", txtLevelName.Text);
            if (value > 0)
            {
                string message = "Role Level Name  is Already Exists.";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append(message);
                sb.Append("')};");
                sb.Append("</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                txtLevelName.Text = "";
                txtLevelName.Focus();
            }
        }
    }
}