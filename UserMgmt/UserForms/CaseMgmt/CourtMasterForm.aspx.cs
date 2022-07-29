using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class CourtMasterForm : System.Web.UI.Page
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
                    string CasteId = Session["CourtId"].ToString();
                    txtid.Value = CasteId;
                    int n = Convert.ToInt32(BL_org_Master.GetMaxID("Court_master&" + Session["CourtCode"].ToString()));
                    if (n > 0)
                    {
                        txtCode.Attributes.Add("disabled", "disabled");
                        txtName.Attributes.Add("disabled", "disabled");
                    }
                    if (rtype == "1")
                    {
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        txtName.Text = Session["CourtName"].ToString();
                        txtCode.Text = Session["CourtCode"].ToString();
                        txtid.Value = Session["CourtId"].ToString();
                        txtCode.ReadOnly = true;
                        txtName.ReadOnly = true;
                       
                    }
                    if (rtype == "2")
                    {
                        txtName.Text = Session["CourtName"].ToString();
                       txtCode.Text = Session["CourtCode"].ToString();
                        txtid.Value = Session["CourtId"].ToString();

                    }

                }
                else
                {
                    //int n = Convert.ToInt32(BL_org_Master.GetMaxID("bail_type_master"));
                  
                    Session["CourtId"] = txtid.Value;
                    btnSave.Text = "Submit";
                    btnCancel.Text = "Cancel";
                }
            }
        }
        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("CourtMasterList");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            cm_court cm_obj = new cm_court();
            if (string.IsNullOrEmpty(txtCode.Text) || string.IsNullOrWhiteSpace(txtCode.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter  Court Section  Code\');", true);
                return;
            }
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Court Section Name\');", true);
                return;
            }
            cm_obj.court_master_code = txtCode.Text;
            cm_obj.court_master_name = txtName.Text;  
            cm_obj.user_id = Session["UserID"].ToString();
            cm_obj.lastmodified_date = DateTime.Now.ToShortDateString();
            cm_obj.record_status = "Y";
            cm_obj.record_deleted = false;

            if (Session["rtype"].ToString() != "0")
            {
                cm_obj.court_master_id = Convert.ToInt32(Session["CourtId"].ToString());
                if (BL_cm_court.UpdateCourt(cm_obj))
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
                    Response.Redirect("CourtMasterList");
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

                if (BL_cm_court.InsertCourt(cm_obj))
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
                    Response.Redirect("CourtMasterList");
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
            Response.Redirect("CourtMasterList");
        }

        protected void txtCode_TextChanged(object sender, EventArgs e)
        {
            if (txtCode.Text != "")
            {
                int value = BL_User_Mgnt.GetExistsData("court_master", "court_master_code", txtCode.Text);
                if (value > 0)
                {
                    string message = "Court Master Code  is Already Exists.";
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