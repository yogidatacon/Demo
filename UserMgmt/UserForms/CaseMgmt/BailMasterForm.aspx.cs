using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class BailMasterForm : System.Web.UI.Page
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
                    string bail_id = Session["BailId"].ToString();
                    txtid.Value = bail_id;
                    int n = Convert.ToInt32(BL_org_Master.GetMaxID("seizure_bail&" + Session["BailCode"].ToString()));

                    if (n > 0)
                    {
                        txtcode.Attributes.Add("disabled", "disabled");
                        txtName.Attributes.Add("disabled", "disabled");
                    }
                    if (rtype == "1")
                    {
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        txtName.Text = Session["BailName"].ToString();
                       txtcode.Text = Session["BailCode"].ToString();
                        txtid.Value = Session["BailId"].ToString();
                        txtcode.ReadOnly = true;
                        txtName.ReadOnly = true;
                    }
                    if (rtype == "2")
                    {
                        txtName.Text = Session["BailName"].ToString();
                        txtcode.Text = Session["BailCode"].ToString();
                        txtid.Value = Session["BailId"].ToString();

                    }

                }
                else
                {
                    //int n = Convert.ToInt32(BL_org_Master.GetMaxID("bail_type_master"));
                    int n = Convert.ToInt32(BL_org_Master.GetMaxID("bail_type_master"));
                    txtcode.Text = "B" + string.Format("{0:00}", (n + 1));
                    Session["BailId"] = txtid.Value;
                    btnSave.Text = "Submit";
                    btnCancel.Text = "Cancel";
                }
            }
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/BailMasterList");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            cm_bail_type cm_obj = new cm_bail_type();
            //if (string.IsNullOrEmpty(txtcode.Text) || string.IsNullOrWhiteSpace(txtcode.Text))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter  Bail Section  Code\');", true);
            //    return;
            //}
            //if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Bail Section Name\');", true);
            //    return;
            //}
            cm_obj.bail_type_master_code = txtcode.Text;
            cm_obj.bail_type_master_name = txtName.Text;
            cm_obj.user_id = Session["UserID"].ToString();
           cm_obj.lastmodified_date = DateTime.Now.ToShortDateString();
            cm_obj.user_id = Session["UserID"].ToString();
            cm_obj.record_status = "Y";
            cm_obj.record_deleted = false;
            if (Session["rtype"].ToString() != "0")
            {
                cm_obj.bail_type_master_id = Convert.ToInt32(Session["BailId"].ToString());
                if (BL_cm_bail_type.UpdateBail(cm_obj))
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
                    Response.Redirect("~/BailMasterList");
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
                
                if (BL_cm_bail_type.InsertBailType(cm_obj))
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
                    Response.Redirect("~/BailMasterList");
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
            Response.Redirect("~/BailMasterList");
        }

        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            if (txtcode.Text != "")
            {
                int value = BL_User_Mgnt.GetExistsData("bail_type_master", "bail_type_master_code", txtcode.Text);
                if (value > 0)
                {
                    string message = "Bail Code  is Already Exists.";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    txtcode.Text = "";
                    txtcode.Focus();
                }
            }
        }
    }
}