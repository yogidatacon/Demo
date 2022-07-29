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
    public partial class PropertyTypeMasterForm : System.Web.UI.Page
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
                    string CasteId = Session["PropertyTypeId"].ToString();
                    txtid.Value = CasteId;
                    txtPropertyTypeCode.Attributes.Add("disabled", "disabled");
                    int n = Convert.ToInt32(BL_org_Master.GetMaxID("Property&" + Session["PropertyTypeCode"].ToString()));
                    if(n>0)
                    {
                       // txtPropertyTypeCode.Attributes.Add("disabled", "disabled");
                        txtPropertyType.Attributes.Add("disabled", "disabled");
                        btnCancel.Visible = false;
                        btnSave.Visible = false;
                    }
                    if (rtype == "1")
                    {
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                       txtPropertyType.Text = Session["PropertyTypeName"].ToString();
                       txtPropertyTypeCode.Text = Session["PropertyTypeCode"].ToString();
                        txtid.Value = Session["PropertyTypeId"].ToString();
                        txtPropertyType.ReadOnly = true;
                        txtPropertyTypeCode.ReadOnly = true;

                    }
                    if (rtype == "2")
                    {
                        txtPropertyType.Text = Session["PropertyTypeName"].ToString();
                        txtPropertyTypeCode.Text = Session["PropertyTypeCode"].ToString();
                        txtid.Value = Session["PropertyTypeId"].ToString();
                    }
                }
                else
                {
                    txtPropertyTypeCode.Attributes.Add("disabled", "disabled");
                    int n = Convert.ToInt32(BL_org_Master.GetMaxID("article_category_master"));
                    if (n < 10)
                        txtid.Value = "P0" + (n + 1).ToString();
                    else
                        txtPropertyTypeCode.Text = "P" + (n + 1).ToString();
                    Session["PropertyTypeId"] = txtid.Value;
                    btnSave.Text = "Submit";
                    btnCancel.Text = "Cancel";
                }
            }
        }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {

            Response.Redirect("PropertyTypeMasterList");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            cm_property_type cm_obj = new cm_property_type();
          
            int n = Convert.ToInt32(BL_org_Master.GetMaxID("property_type_master&"+txtPropertyType.Text));
            if (n!=0  && n != Convert.ToInt32(txtPropertyTypeCode.Text.Substring(1, 2)))
            {
                string message = "Property Name is Already Exists";
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


                cm_obj.product_type_code = txtPropertyTypeCode.Text;
                cm_obj.product_type_name = txtPropertyType.Text;
                cm_obj.user_id = Session["UserID"].ToString();
                cm_obj.lastmodified_date = DateTime.Now.ToShortDateString();
                cm_obj.record_status = "Y";
                cm_obj.record_deleted = false;
                //if (BL_cm_property_type.InsertPropertyType(cm_obj))
                //{
                //    string message = "Record is Updated.";
                //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //    sb.Append("<script type = 'text/javascript'>");
                //    sb.Append("window.onload=function(){");
                //    sb.Append("alert('");
                //    sb.Append(message);
                //    sb.Append("')};");
                //    sb.Append("</script>");
                //    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                //    Session["UserID"] = Session["UserID"].ToString();
                //    Response.Redirect("~/CourtMasterList");
                //}

                if (Session["rtype"].ToString() != "0")
                {
                    cm_obj.product_type_master_id = Convert.ToInt32(Session["PropertyTypeId"].ToString());
                    if (BL_cm_property_type.Updatepropertytype(cm_obj))
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
                        Response.Redirect("PropertyTypeMasterList");
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

                    if (BL_cm_property_type.InsertPropertyType(cm_obj))
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
                        Response.Redirect("PropertyTypeMasterList");
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
            Response.Redirect("PropertyTypeMasterList");
        }

        protected void txtPropertyTypeCode_TextChanged(object sender, EventArgs e)
        {
            if (txtPropertyTypeCode.Text != "")
            {
                int value = BL_User_Mgnt.GetExistsData("property_type_master", "property_type_code", txtPropertyTypeCode.Text);
                if (value > 0)
                {
                    string message = "Property Type Code  is Already Exists.";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    txtPropertyTypeCode.Text = "";
                    txtPropertyTypeCode.Focus();
                }
            }
        }
    }
}