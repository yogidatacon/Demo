using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class DesignationMasterForm : System.Web.UI.Page
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
                List<cm_designation_type> Designationtype = new List<cm_designation_type>();
                Designationtype = BL_cm_designation_type.GetList();
                var list = from s in Designationtype
                           select s;
              ddlDesignationType.DataSource = list.ToList();
                ddlDesignationType.DataTextField = "designation_type_name";
                ddlDesignationType.DataValueField = "designation_type_code";
                ddlDesignationType.DataBind();
                ddlDesignationType.Items.Insert(0, "Select");

                if (rtype != "0")
                {
                    string CasteId = Session["DesignationId"].ToString();
                    txtid.Value = CasteId;
                    int n = Convert.ToInt32(BL_org_Master.GetMaxID("designation&"+ Session["DesignationCode"].ToString()));
                    if (n > 0)
                    {
                        txtCode.Attributes.Add("disabled", "disabled");
                        txtName.Attributes.Add("disabled", "disabled");
                        ddlDesignationType.Attributes.Add("disabled", "disabled");
                        btnCancel.Visible = false;
                        btnSave.Visible = false;
                    }
                    if (rtype == "1")
                    {
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        txtName.Text = Session["DesignationName"].ToString();
                        txtCode.Text = Session["DesignationCode"].ToString();
                        txtid.Value = Session["DesignationId"].ToString();
                        ddlDesignationType.SelectedValue = Session["Designation"].ToString();

                        txtCode.ReadOnly = true;
                        txtName.ReadOnly = true;
                        ddlDesignationType.Attributes.Add("disabled", "disabled");
                    }
                    if (rtype == "2")
                    {
                        txtName.Text = Session["DesignationName"].ToString();
                        txtCode.Text = Session["DesignationCode"].ToString();
                        txtid.Value = Session["DesignationId"].ToString();
                        ddlDesignationType.SelectedValue = Session["Designation"].ToString();

                    }
                }
                else
                {
                    int n = Convert.ToInt32(BL_org_Master.GetMaxID("designation_master"));
                    txtCode.Text ="D"+string.Format("{0:00}", (n + 1));
                    Session["DesignationId"] = txtid.Value;
                    btnSave.Text = "Submit";
                    btnCancel.Text = "Cancel";
                }
            }
        }
        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/DesignationMasterList");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            cm_designation cm_obj = new cm_designation();
          
            cm_obj.designation_code = txtCode.Text;            
            cm_obj.designation_name = txtName.Text;
            cm_obj.designation_type_code = ddlDesignationType.SelectedValue;
            cm_obj.user_id = Session["UserID"].ToString();
           cm_obj.lastmodified_date = DateTime.Now.ToShortDateString();
            cm_obj.record_status = "Y";
            cm_obj.record_deleted = false;
            List<cm_designation> _designation = new List<cm_designation>();
            _designation = BL_cm_designation.GetList();
            var list = from s in _designation
                       where s.designation_name == txtName.Text.Trim() && s.designation_type_code==ddlDesignationType.SelectedValue
                       select s;

            if (list.ToList().Count > 0)
            {
                if ((list.ToList()[0].designation_code != txtCode.Text))
                {
                    string message = "Designation Name Already Exists!";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                }
                else
                {
                    if (Session["rtype"].ToString() != "0")
                    {
                        cm_obj.designation_master_id = Convert.ToInt32(Session["DesignationId"].ToString());
                        if (BL_cm_designation.UpdateDesignation(cm_obj))
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
                            Response.Redirect("~/DesignationMasterList");
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
            else
            {
                if (Session["rtype"].ToString() != "0")
                {
                    cm_obj.designation_master_id = Convert.ToInt32(Session["DesignationId"].ToString());
                    if (BL_cm_designation.UpdateDesignation(cm_obj))
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
                        Response.Redirect("~/DesignationMasterList");
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
                    if (BL_cm_designation.InsertDesignation(cm_obj))
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
                        Response.Redirect("~/DesignationMasterList");
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/DesignationMasterList");
        }

        protected void txtCode_TextChanged(object sender, EventArgs e)
        {
            if (txtCode.Text != "")
            {
                int value = BL_User_Mgnt.GetExistsData("designation_master", "designation_code", txtCode.Text);
                if (value > 0)
                {
                    string message = "Designation Code  is Already Exists.";
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