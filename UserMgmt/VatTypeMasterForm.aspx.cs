using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class VatTypeMasterForm : System.Web.UI.Page
    {
        public static string vattypename1;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                
                string rtype = Session["rtype"].ToString();
                Session["rtype"] = rtype;
                if (Session["rtype"].ToString() != "0")
                {
                    List<VAT_Master> tabs = new List<VAT_Master>();
                    tabs = BL_VATMaster.GetvatmasterList("");
                    var list = from s in tabs
                               where s.vat_type_code == Session["vat_type_code"].ToString()
                               select s;
                    if (rtype == "1" || list.ToList().Count > 0)
                    {
                        txtcode.Text = Session["vat_type_code"].ToString();
                        txtName.Text = Session["vat_type_name"].ToString();
                        txtcode.ReadOnly = true;
                        txtName.ReadOnly = true;
                        btnCancel.Visible = false;
                        btnSave.Visible = false;
                    }
                    else if (rtype == "2")
                    {
                        txtcode.Text = Session["vat_type_code"].ToString();
                        txtName.Text = Session["vat_type_name"].ToString();
                        vattypename1 = Session["vat_type_name"].ToString();
                        txtcode.ReadOnly = true;
                        txtName.Enabled = true;
                        btnCancel.Visible = true;
                        btnSave.Visible = true;
                    }
                }
                }
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/LoginPage");
                }
            }
        }
        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VatTypeMasterList");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string rtype = Session["rtype"].ToString();
            string user_id = Session["UserID"].ToString();
            if (rtype == "0")
            {
                VatType vat = new VatType();
                vat.vat_type_code = txtcode.Text.ToUpper();
                vat.vat_type_name = txtName.Text;
                vat.user_id = user_id;
                if (BL_VatType.InsertVat(vat))
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
                    Response.Redirect("VatTypeMasterList");
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
                VatType vat = new VatType();
                vat.vat_type_code = txtcode.Text.ToUpper(); ;
                vat.vat_type_name = txtName.Text;
                vat.user_id = user_id;
                vat.id = txtid.Value;
                if (BL_VatType.UpdateVat(vat))
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
                    Response.Redirect("VatTypeMasterList");
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
        [WebMethod]
        public static string chkDuplicateVATTypeName(Object vattypename)
        {
            int value = 0;
            if (vattypename1 != vattypename.ToString())
                value = BL_User_Mgnt.GetExistsData("VAT_Type_Master", "VAT_Type_Name", vattypename.ToString());
            return value.ToString();
        }
        [WebMethod]
        public static string chkDuplicateVATTypeCode(Object vattypecode)
        {
            int value = 0;
            value = BL_User_Mgnt.GetExistsData("VAT_Type_Master", "VAT_Type_Code", vattypecode.ToString());
            return value.ToString();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VatTypeMasterList");
        }
        protected void DispatchTypeMaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DispatchTypeMasterList.aspx");
        }
        protected void partymaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("PartyMasterList");
        }

        protected void productmaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ProductMasterList");
        }
        protected void partyfinancialyears_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PartyFinancialYearList");
        }

        protected void vatmaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATMasterList");

        }
        protected void uommaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("UOMMasterList");
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
        protected void vattypemaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VatTypeMasterList");
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
    }
}