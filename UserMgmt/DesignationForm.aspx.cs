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
    public partial class DesignationForm : System.Web.UI.Page
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
                string userid = Session["UserID"].ToString();
                Session["UserID"] = userid;
                List<Designation_Details> des = new List<Designation_Details>();
                des = BL_Designation_Details.GetDtypeList();
                ddlDesignationtype.DataSource = des;
                ddlDesignationtype.DataTextField = "designation_type";
                ddlDesignationtype.DataValueField = "designation_type_code";
                ddlDesignationtype.DataBind();
                ddlDesignationtype.Items.Insert(0, "Select");
                if(Session["rtype"].ToString()!="0")
                {
                    txtDesignationCode.ReadOnly = true;
                    txtDesignationCode.Text = Session["DCode"].ToString();
                    txtDesignationName.Text = Session["DName"].ToString();
                    ddlDesignationtype.SelectedValue = Session["DType"].ToString();
                    if (Session["rtype"].ToString()=="1")
                    {
                        ddlDesignationtype.Enabled = false;
                        txtDesignationName.ReadOnly = true;
                        btnCancel.Visible = false;
                        btnSave.Visible = false;
                    }
                }

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                btnSave.Enabled = false;
                Designation_Details des = new Designation_Details();
                des.designation_type_code = ddlDesignationtype.SelectedValue;
                des.designation_name = txtDesignationName.Text;
                des.designation_code = txtDesignationCode.Text;
                des.user_id = Session["UserID"].ToString();
                string val = "";
               
                    val = BL_Designation_Details.InsertD(des);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("DesignationList");
                }
                else
                {
                    btnSave.Enabled = true;
                    string message = "Server Error";
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
            Response.Redirect("DesignationList");
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("DesignationList");
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
            Response.Redirect("DesignationTypeList");
        }

        protected void Designation_2_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("DesignationList");
        }
    }
}