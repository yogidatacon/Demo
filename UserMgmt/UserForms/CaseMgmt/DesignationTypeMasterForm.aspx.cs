using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class DesignationTypeMasterForm : System.Web.UI.Page
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
                    string CasteId = Session["DesignationId"].ToString();
                    txtid.Value = CasteId;
                    int n = Convert.ToInt32(BL_org_Master.GetMaxID("designation_type&" + Session["DesignationCode"].ToString()));
                    //List<RoleMaster> rolemaster = new List<RoleMaster>();
                    //rolemaster = BL_User_Mgnt.GetRoleMasterList("");
                    //var list = from s in rolemaster
                    //           where s.accestype == Session["AccessTypeCode"].ToString()
                    //           select s;
                    if (n > 0)
                    {
                        txtCode.Attributes.Add("disabled", "disabled");
                        txtName.Attributes.Add("disabled", "disabled");
                    }
                    if (rtype == "1")
                    {
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        txtName.Text = Session["DesignationName"].ToString();
                        txtCode.Text = Session["DesignationCode"].ToString();
                       
                        txtid.Value = Session["DesignationId"].ToString();
                        txtCode.ReadOnly = true;
                        txtName.ReadOnly = true;
                     
                    }
                    if (rtype == "2")
                    {
                        txtName.Text = Session["DesignationName"].ToString();
                        txtCode.Text = Session["DesignationCode"].ToString();
                        txtid.Value = Session["DesignationId"].ToString();
                        

                    }
                }
                else
                {
                    //int n = Convert.ToInt32(BL_org_Master.GetMaxID("bail_type_master"));
                    //txtid.Value = (n + 1).ToString();
                    Session["DesignationId"] = txtid.Value;
                    btnSave.Text = "Submit";
                    btnCancel.Text = "Cancel";
                }
            }
        }
        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("DesignationTypeMasterList");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            cm_designation_type cm_obj = new cm_designation_type();
            if (string.IsNullOrEmpty(txtCode.Text) || string.IsNullOrWhiteSpace(txtCode.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter  Designation Type  Code\');", true);
                return;
            }
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Designation Type Name\');", true);
                return;
            }
            cm_obj.designation_type_code = txtCode.Text;
            cm_obj.designation_type_name = txtName.Text;
            cm_obj.user_id = Session["UserID"].ToString();
           cm_obj.lastmodified_date = DateTime.Now.ToShortDateString();
            cm_obj.record_status = "Y";
            cm_obj.record_deleted = false;
          

            if (Session["rtype"].ToString() != "0")
            {
                cm_obj.designation_type_master_id= Convert.ToInt32(Session["DesignationId"].ToString());
                if (BL_cm_designation_type.UpdateDesignation(cm_obj))
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
                    Response.Redirect("~/DesignationTypeMasterList");
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

                if (BL_cm_designation_type.InsertDesignationType(cm_obj))
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
                    Response.Redirect("~/DesignationTypeMasterList");
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/DesignationTypeMasterList");
        }

        protected void txtCode_TextChanged(object sender, EventArgs e)
        {
            if (txtCode.Text != "")
            {
                int value = BL_User_Mgnt.GetExistsData("designation_type_master", "designation_type_code", txtCode.Text);
                if (value > 0)
                {
                    string message = "Designation Type Code  is Already Exists.";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    txtCode.Text = "";
                    txtCode.Focus();
                }
            }
        }
    }
}