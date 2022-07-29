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
    public partial class LicenseFeeMaster : System.Web.UI.Page
    {
        LicenseFee license = new LicenseFee();
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
                List<LicenseType> tab = new List<LicenseType>();
                tab = BL_LicenseType.GetList("");
                var list1 = from s in tab

                            select s;
                ddlLicense.DataSource = list1.ToList();
                ddlLicense.DataTextField = "lic_type_name";
                ddlLicense.DataValueField = "lic_type_code";
                ddlLicense.DataBind();
                ddlLicense.Items.Insert(0, "Select");
                List<LicenseSubType> tabs = new List<LicenseSubType>();
                tabs = BL_LicenseSubType.GetList("");
                var list = from s in tabs
                         
                           select s;
                ddlsubtype.DataSource = list.ToList();
                ddlsubtype.DataTextField = "lic_subtype_name";
                ddlsubtype.DataValueField = "lic_subtype_code";
                ddlsubtype.DataBind();
                ddlsubtype.Items.Insert(0, "Select");
                // rtype = Session["rtype"].ToString();
                Session["UserID"] = Session["UserID"];
                if (Session["rtype"].ToString() != "0")
                {
                    license = new LicenseFee();
                    license = BL_LicenseFee.GetDetails(Convert.ToInt32(Session["licenseId"]));
                    txtcode.Text = license.lic_fee_code;
                    ddlLicense.SelectedValue = license.lic_type_code;
                    ddlsubtype.SelectedValue = license.lic_subtype_code;
                    txtFee.Text = license.lic_fee_amt.ToString();
                    txtregn.Text = license.lic_regn_amt.ToString();
                    txtsecurity.Text = license.lic_security_amt.ToString();
                    txtadvance.Text = license.lic_adv_fee.ToString();
                    txtProc.Text = license.lic_proc_fee.ToString();
                    txtcode.Enabled = false;
                    //List<LicenseSubType> tabs = new List<LicenseSubType>();
                    //tabs = BL_LicenseSubType.GetList("");
                    //var list = from s in tabs
                    //           where s.lic_type_code == Session["license_code"].ToString()
                    //           select s;
                    //List<VAT_Master> party = new List<VAT_Master>();
                    //party = BL_VATMaster.GetvatmasterList("");
                    //var list1 = from s1 in party
                    //            where s1.uom_code == Session["license_code"].ToString()
                    //            select s1;
                    if (Session["rtype"].ToString() == "1")
                    {

                        txtadvance.ReadOnly = true;
                        txtFee.ReadOnly = true;
                        txtProc.ReadOnly = true;
                        txtregn.ReadOnly = true;
                        txtsecurity.ReadOnly = true;
                        ddlLicense.Enabled = false;
                        ddlsubtype.Enabled = false;
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
                LicenseFee om = new LicenseFee();
                om.lic_fee_code = txtcode.Text.ToUpper();
                om.lic_type_code = ddlLicense.SelectedValue;
                om.lic_subtype_code = ddlsubtype.SelectedValue;
                om.lic_fee_amt = Convert.ToDouble(txtFee.Text);
                om.lic_regn_amt= Convert.ToDouble(txtregn.Text);
                om.lic_security_amt = Convert.ToDouble(txtsecurity.Text);
                om.lic_adv_fee = Convert.ToDouble(txtadvance.Text);
                om.lic_proc_fee= Convert.ToDouble(txtProc.Text);
                om.lic_renewal_fee = Convert.ToDouble(txtrenewal.Text);
                om.user_id = Session["UserID"].ToString();
                string val;
                if (Session["rtype"].ToString() == "0")
                { 
                    val = BL_LicenseFee.Insert(om);
                }
                else
                { 
                    om.lic_fee_master_id = Convert.ToInt32(Session["licenseId"].ToString());
                    val = BL_LicenseFee.Update(om);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("LicenseFeeList.aspx");
                }
                else
                {
                    string message = val;
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

        protected void licensesub_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("LicenseSubTypeList.aspx");
        }

        protected void licensefee_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("LicenseFeeList.aspx");
        }

        protected void license_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("LicenseTypeMasterList.aspx");

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("LicenseFeeList");
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
        protected void DispatchTypeMaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DispatchTypeMasterList.aspx");
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

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("LicenseFeeList");
        }
        [WebMethod]
        public static string chkDuplicateUOMCode(Object code)
        {
            int value = BL_User_Mgnt.GetExistsData("lic_fee_master", "lic_fee_code", code.ToString().ToUpper());
            return value.ToString();
        }
        [WebMethod]
        //public static string chkDuplicateUOMName(Object name)
        //{
        //    int value = 0;
        //    if (uom1 != name.ToString())
        //        value = BL_User_Mgnt.GetExistsData("lic_subtype_Master", "lic_subtype_name", name.ToString());

        //    return value.ToString();
        //}
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