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
    public partial class AccessTypeFrom : System.Web.UI.Page
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
                    string level_id = Session["AccessTypeCode"].ToString();
                    Session["AccessTypeCode"] = level_id;
                    txtid.Value = level_id;
                    List<RoleMaster> rolemaster = new List<RoleMaster>();
                    rolemaster = BL_User_Mgnt.GetRoleMasterList("");
                    var list = from s in rolemaster
                               where s.accestype == Session["AccessTypeCode"].ToString()
                               select s;
                   
                   if (rtype == "1"|| list.ToList().Count > 0)
                    {
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        txtAccessTypeName.Text = Session["AccessTypeName"].ToString();
                        txtDescription.Text = Session["Description"].ToString();
                        txtid.Value = Session["AccessTypeCode"].ToString();
                        txtAccessTypeCode.Text = Session["AccessTypeCode"].ToString();
                        txtDescription.Enabled = false;
                        txtAccessTypeName.Enabled = false;
                        txtAccessTypeName.BackColor = System.Drawing.Color.LightGray;
                        txtDescription.BackColor = System.Drawing.Color.LightGray;
                    }
                    if (rtype == "2")
                    {
                        txtAccessTypeName.Text = Session["AccessTypeName"].ToString();
                        txtDescription.Text = Session["Description"].ToString();
                        txtid.Value = Session["AccessTypeCode"].ToString();
                   txtAccessTypeCode.Text = Session["AccessTypeCode"].ToString();

                    }

                }
                else
                {
                    int n = Convert.ToInt32(BL_org_Master.GetMaxID("access_type_master"));
                    txtid.Value = (n + 1).ToString();
                    Session["AccessTypeCode"] = txtid.Value;
                    btnSave.Text = "Submit";
                    btnCancel.Text = "Cancel";
                }
            }
        }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AccessTypeList");
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
        protected void ThanaMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ThanaMasterList");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            AccessType accesstype = new AccessType();
            string s = txtAccessTypeName.Text.ToString();
            s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
            accesstype.access_type_name = s;
            accesstype.access_type_code = txtid.Value.ToUpper();
            accesstype.access_type_code = txtAccessTypeCode.Text.ToUpper();
            accesstype.access_type_desc = txtDescription.Text;
            accesstype.user_id = Session["UserID"].ToString();
            if (Session["rtype"].ToString() != "0")
            {
                if (BL_User_Mgnt.UpdateAccessType(accesstype))
                {
                    string message = "Record is Updated.";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    Session["UserID"] = Session["UserID"].ToString();
                    Response.Redirect("~/AccessTypeList");
                }
                else
                {

                }
            }
            else
            {
                if (BL_User_Mgnt.InsertAccessType(accesstype))
                {
                    string message = "Record is Saved.";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    Session["UserID"] = Session["UserID"].ToString();
                    Response.Redirect("~/AccessTypeList");
                }
                else
                {

                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/AccessTypeList");
        }
        static string accesstype="";
        [WebMethod]
        public static string chkDuplicateAccessTypeName(Object accesstypename)
        {
            int value = 0;
            if (accesstype != accesstypename.ToString())
            {
                string s = accesstypename.ToString();
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                value = BL_User_Mgnt.GetExistsData("access_type_master", "access_type_name", s);
            }
            return value.ToString();
        }

        static string access = "";
        [WebMethod]
        public static string chkDuplicateAccessTypecode(Object accesstypecode)
        {
            int value = 0;
            if (access != accesstypecode.ToString())
            {
                string s = accesstypecode.ToString();
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                value = BL_User_Mgnt.GetExistsData("access_type_master", "access_type_code", s);
            }
            return value.ToString();
        }
    }
}