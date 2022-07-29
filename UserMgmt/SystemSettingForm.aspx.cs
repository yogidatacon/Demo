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
    public partial class SystemSettingForm : System.Web.UI.Page
    {
        public static string producttypename1;
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
                Session["UserID"] = Session["UserID"];
                string rtype = Session["rtype"].ToString();
                Session["rtype"] = rtype;
                if (rtype != "0")
                {
                    List<SystemSetting> party = new List<SystemSetting>();
                    party = BL_SystemSetting.GetList("");
                    var list = from s1 in party
                               where s1.parameter_name == Session["name"].ToString()
                    select s1;
                    if (rtype == "1" )
                    {
                        txtcode.Text = Session["value"].ToString() ;
                        txtname1.Text = Session["name"].ToString();
                        txtparameter.Text= Session["string"].ToString();
                        txtcode.Enabled = false;
                        txtname1.Enabled = false;
                        btnCancel.Visible = false;
                        btnSave.Visible = false;
                    }
                    else if (rtype == "2")
                    {
                        txtcode.Text = Session["value"].ToString();
                        txtname1.Text = Session["name"].ToString();
                        txtparameter.Text = Session["string"].ToString();
                        //txtcode.Enabled = false;
                        //txtname1.Enabled = true;
                        btnCancel.Visible = true;
                        btnSave.Visible = true;
                    }
                }
            }
        }
        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("SystemSettingList.aspx");
        }
        protected void DispatchTypeMaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DispatchTypeMasterList.aspx");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            string rtype = Session["rtype"].ToString();
            string user_id = Session["UserID"].ToString();
            if (rtype == "0")
            {
                if(txtcode.Text=="" && txtparameter.Text=="")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Enter Either Parameter String or Parameter Value \');", true);
                    txtcode.Focus();
                }
                else
                {
                SystemSetting product = new SystemSetting();
                string s = txtname1.Text.ToString();
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                product.parameter_name = txtname1.Text.ToString();
                product.parameter_value_num = Convert.ToInt32(txtcode.Text);
                product.parameter_value_str = txtparameter.Text;
                product.user_id = user_id;
                if (BL_SystemSetting.InsertSys(product))
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("SystemSettingList.aspx");
                }
                else
                {
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
            else
            {
                if (txtcode.Text == "" && txtparameter.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Enter Either Parameter String or Parameter Value \');", true);
                    txtcode.Focus();
                }
                else
                {
                    SystemSetting product = new SystemSetting();
                    string s = txtname1.Text;
                    s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                    product.parameter_name = txtname1.Text.ToString();
                    product.parameter_value_num = Convert.ToInt32(txtcode.Text);
                    product.parameter_value_str = txtparameter.Text;
                    product.user_id = user_id;
                    product.system_setting_id = Convert.ToInt32(Session["id"].ToString());
                    if (BL_SystemSetting.UpdateSys(product))
                    {
                        Session["UserID"] = Session["UserID"].ToString();
                        Response.Redirect("SystemSettingList.aspx");
                    }
                    else
                    {
                        string message = "Server Side Error";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                        return;
                    }
                }

            }
        }
        [WebMethod]
        public static string chkDuplicateProductTypeName(Object producttypename)
        {
            int value = 0;
            if (producttypename1 != producttypename.ToString())
            {
                string s = producttypename.ToString();
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                value = BL_User_Mgnt.GetExistsData("product_type_master", "product_type_name", s);
            }

            return value.ToString();
        }
        [WebMethod]
        public static string chkDuplicateProductTypeCode(Object producttypecode)
        {
            int value = 0;

            value = BL_User_Mgnt.GetExistsData("product_type_master", "product_type_code", producttypecode.ToString().ToUpper());

            return value.ToString();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("SystemSettingList.aspx");
        }

        protected void partymaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("PartyMasterList");
        }
        protected void partyfinancialyears_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PartyFinancialYearList");
        }

        protected void productmaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ProductMasterList");
        }

        protected void vatmaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATMasterList");

        }

        protected void partytypemaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("PartyTypeMasterList");
        }
        protected void vattypemaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VatTypeMasterList");
        }
        protected void producttypemaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ProductTypeList");
        }
        protected void uommaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("UOMMasterList");
        }
        protected void RawMaterialTypeMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialTypeMasterList");
        }

        protected void RawMaterial_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialList");
        }

        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                txtparameter.Text = "";
            }
        }

        protected void txtparameter_TextChanged(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                txtcode.Text = "";
            }
        }
    }
}