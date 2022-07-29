using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class OffenceTypeMasterForm : System.Web.UI.Page
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
                    string CasteId = Session["OffenceTypeId"].ToString();
                    txtid.Value = CasteId;
                    List<cm_offence> offence = new List<cm_offence>();
                    offence = BL_cm_offence.GetList();
                    var list = from s in offence
                               where s.offence_type_code == Session["OffenceTypeCode"].ToString()
                               select s;
                    if (list.ToList().Count > 0)
                    {
                        txtCode.Attributes.Add("disabled", "disabled");
                        txtName.Attributes.Add("disabled", "disabled");
                    }
                    if (rtype == "1")
                    {
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        txtName.Text = Session["OffenceTypeName"].ToString();
                        txtCode.Text = Session["OffenceTypeCode"].ToString();
                        txtid.Value = Session["OffenceTypeId"].ToString();
                        txtCode.ReadOnly = true;
                        txtName.ReadOnly = true;

                    }
                    if (rtype == "2")
                    {
                        txtName.Text = Session["OffenceTypeName"].ToString();
                        txtCode.Text = Session["OffenceTypeCode"].ToString();
                        txtid.Value = Session["OffenceTypeId"].ToString();
                    }
                }
                else
                {
                    //int n = Convert.ToInt32(BL_org_Master.GetMaxID("bail_type_master"));
                    //txtid.Value = (n + 1).ToString();
                    Session["OffenceTypeId"] = txtid.Value;
                    btnSave.Text = "Submit";
                    btnCancel.Text = "Cancel";
                }
            }
        }
        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            //Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/OffenceTypeMasterList");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            cm_offence_type cm_obj = new cm_offence_type();
            if (string.IsNullOrEmpty(txtCode.Text) || string.IsNullOrWhiteSpace(txtCode.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Offence Type Code\');", true);
                return;
            }
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Offence Type Name\');", true);
                return;
            }
            cm_obj.offence_code = txtCode.Text;
            cm_obj.offence_name = txtName.Text;
            cm_obj.user_id = Session["UserID"].ToString();
           cm_obj.lastmodified_date = DateTime.Now.ToShortDateString();
            cm_obj.record_status = "Y";
            cm_obj.record_deleted = false;
            
            if (Session["rtype"].ToString() != "0")
            {
                cm_obj.offence_master_id= Convert.ToInt32(Session["OffenceTypeId"].ToString());
                if (BL_cm_offence_type.UpdateOffenceType(cm_obj))
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
                    Response.Redirect("~/OffenceTypeMasterList");
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

                if (BL_cm_offence_type.InsertOffenceType(cm_obj))
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
                    Response.Redirect("~/OffenceTypeMasterList");
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
            Response.Redirect("~/OffenceTypeMasterList");
        }

        protected void txtCode_TextChanged(object sender, EventArgs e)
        {
            if (txtCode.Text != "")
            {
                int value = BL_User_Mgnt.GetExistsData("offence_type_master", "offence_type_code", txtCode.Text);
                if (value > 0)
                {
                    string message = "Offence Type Code  is Already Exists.";
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