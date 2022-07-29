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
    public partial class RawMaterialTypeMaster : System.Web.UI.Page
    {
        RawMaterialType rawmaterial = new RawMaterialType();
        public static string meterialtype;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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
                if (Session["rtype"].ToString() != "0")
            {
              

                if (Session["rtype"].ToString() == "1")
                {
                    txtRawMaterialTypecode.Text = Session["rawmaterial_type_code"].ToString();
                    txtRawname.Text = Session["rawmaterial_type_name"].ToString();
                    txtRawMaterialTypecode.Enabled = false;
                        meterialtype = txtRawname.Text;
                    txtRawname.Enabled = false;
                    btnCancel.Visible = false;
                    btnSave.Visible = false;
                }
                else
                {
                    txtRawMaterialTypecode.Text = Session["rawmaterial_type_code"].ToString();
                    txtRawname.Text = Session["rawmaterial_type_name"].ToString();
                        meterialtype = txtRawname.Text;
                        txtRawMaterialTypecode.Enabled = false;
                }
            }
            }
        }
        protected void DispatchTypeMaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DispatchTypeMasterList.aspx");
        }
        protected void partyfinancialyears_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PartyFinancialYearList");
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialTypeMasterList");
        }
        [WebMethod]
        public static string CheckDuplicatesCode(Object code)
        {
            int value = BL_User_Mgnt.GetExistsData("rawmaterial_type_master", "rawmaterial_type_code", code.ToString().ToUpper());
            return value.ToString();
        }
        [WebMethod]
        public static string CheckDuplicatesName(Object name)
        {
            int value = 0;
            if (meterialtype != name.ToString())
            {
                string s = name.ToString();
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                value = BL_User_Mgnt.GetExistsData("rawmaterial_type_master", "rawmaterial_type_name", s);
            }

            return value.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string rtype = Session["rtype"].ToString();
            string user_id = Session["UserID"].ToString();
            if (rtype == "0")
            {
                RawMaterialType rawmaterial = new RawMaterialType();
                string s = txtRawname.Text.ToString();
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                rawmaterial.rawmaterial_type_code = txtRawMaterialTypecode.Text.ToUpper();
                rawmaterial.rawmaterial_type_name = s;
                rawmaterial.user_id = user_id;
                if (BL_RawMaterialType.InsertRawMaterial(rawmaterial))
                {

                    string message = "Record is  Sucessfuly Submitted.";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    Session["UserID"] = user_id;
                    Response.Redirect("RawMaterialTypeMasterList");
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
            else
            {
                RawMaterialType rawmaterial = new RawMaterialType();
                string s = txtRawname.Text.ToString();
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                rawmaterial.rawmaterial_type_code = txtRawMaterialTypecode.Text.ToUpper();
                rawmaterial.rawmaterial_type_name = s;
                rawmaterial.user_id = user_id;
                
                if (BL_RawMaterialType.UpdateRawMaterial(rawmaterial))
                {
                    string message = "Successfully Updated";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    Session["UserID"] = Session["UserID"].ToString();

                    Response.Redirect("RawMaterialTypeMasterList");
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialTypeMasterList");
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
        protected void partymaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("PartyMasterList");
        }
        protected void vattypemaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VatTypeMasterList");
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
    }
}