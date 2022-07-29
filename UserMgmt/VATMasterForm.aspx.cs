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
    public partial class VATMasterForm : System.Web.UI.Page
    {
        List<VatType> vattypes = new List<VatType>();
        static string vatname1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null)
                {

                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                    GetDropDownValues();
                string userid = Session["UserID"].ToString();
                Session["UserID"] = Session["UserID"];
                vattypes = new List<VatType>();
                vattypes = BL_VatType.GetListValue(userid);
                ddlVatType.DataSource = vattypes;
                ddlVatType.DataTextField = "vat_type_name";
                ddlVatType.DataValueField= "vat_type_code";
                ddlVatType.DataBind();
                ddlVatType.Items.Insert(0, "Select");
                if(Session["rtype"].ToString()=="0")
                {
                    //int n =Convert.ToInt32( BL_org_Master.GetMaxID("vat_master"));
                    //vatcode.Value =(n+1).ToString();
                }
                else
                {
                   
                  
                   if(Session["rtype"].ToString() == "1")
                    {
                        ddlContent.Attributes.Add("disabled", "disabled");
                        ddlPartyType.Attributes.Add("disabled", "disabled");
                        ddlPartyType.Attributes.Add("disabled", "disabled");
                        ddlParty.Attributes.Add("disabled", "disabled");

                        ddlProductType.Attributes.Add("disabled", "disabled");
                        ddlProductType.Attributes.Add("disabled", "disabled");
                        ddlUOM.Attributes.Add("disabled", "disabled");
                        ddlVatType.Attributes.Add("disabled", "disabled");
                        txtDepth.Attributes.Add("disabled", "disabled");
                        txtVatCapacity.Attributes.Add("disabled", "disabled");
                        txtVatName.Attributes.Add("disabled", "disabled");
                        btnCancel.Visible = false;
                        btnSave.Visible = false;
                    }
                    vatcode.Value = Session["Vat_code"].ToString();
                    VAT_Master vat = new VAT_Master();
                    vat = BL_VATMaster.GetVATDetails(vatcode.Value);
                    
                    txtVatCapacity.Text =vat.vat_totalcapacity.ToString();
                    txtDepth.Text = vat.vat_depth.ToString();
                    ddlContent.SelectedValue = vat.content;
                    ddlPartyType.SelectedValue = vat.party_type_code;
                    ddlPartyType_SelectedIndexChanged(sender, e);
                    ddlParty.SelectedValue = vat.party_code;
                    vatname1= vat.vat_name;
                    ddlProductType.SelectedValue = vat.product_type_code;
                    ddlProductType_SelectedIndexChanged(sender, e);
                    ddlUOM.SelectedValue = vat.uom_code;
                    ddlVatType.SelectedValue = vat.vat_type_code;
                    ddlVatType_SelectedIndexChanged(sender, e);
                    txtVatName.Text = vat.vat_name;
                }
                }
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/LoginPage");
                }
            }
              
        }

        private void GetDropDownValues()
        {
            List< Party_Type_Master> partytypes = new List<Party_Type_Master>();
            partytypes = BL_Party_Type_Master.GetList();
            ddlPartyType.DataSource = partytypes;
            ddlPartyType.DataTextField = "Party_Type_Name";
            ddlPartyType.DataValueField = "Party_Type_Code";
            ddlPartyType.DataBind();
            ddlPartyType.Items.Insert(0, "Select");
            List<UOM_Master> uom = new List<UOM_Master>();
            uom = BL_UOM.GetList(Session["UserID"].ToString());
            ddlUOM.DataSource = uom;
            ddlUOM.DataTextField = "uom_name";
            ddlUOM.DataValueField = "uom_code";
            ddlUOM.DataBind();
            ddlUOM.Items.Insert(0, "Select");
            List<ProductType>  product = new List<ProductType>();
            product = BL_ProductType.GetProductList(Session["UserID"].ToString());
            ddlProductType.DataSource = product;
            ddlProductType.DataTextField = "Product_type_name";
            ddlProductType.DataValueField = "Product_type_code";
            ddlProductType.DataBind();
            ddlProductType.Items.Insert(0, "Select");

        }

        protected void ddlVatType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtVatName.Text = "";
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATMasterList");
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
        protected void partyfinancialyears_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PartyFinancialYearList");
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                VAT_Master vat = new VAT_Master();
                vat.vat_code = vatcode.Value.ToUpper();
                vat.vat_name = txtVatName.Text;
                vat.party_code = ddlParty.SelectedValue;
                vat.party_type_code = ddlPartyType.SelectedValue;
                vat.product_type_code = ddlProductType.SelectedValue;
                vat.org_id = 1;
               
                vat.uom_code = ddlUOM.SelectedValue;
                vat.content = ddlContent.SelectedValue;
                vat.vat_totalcapacity =Convert.ToDouble( txtVatCapacity.Text);
                vat.vat_availablecapacity = Convert.ToDouble(txtVatCapacity.Text);
                vat.vat_depth = Convert.ToDouble(txtDepth.Text);
                vat.user_id = Session["UserID"].ToString();
                vat.vat_type_code = ddlVatType.SelectedValue.ToUpper();
                string val;
                if (Session["Rtype"].ToString() == "0")
                    val = BL_VATMaster.Insert(vat);
                else
                    val = BL_VATMaster.Update(vat);
                if(val=="0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("VATMasterList");

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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATMasterList");
        }
        //[WebMethod]
        //public static string chkDuplicateVATName(Object vatname)
        //{
        //    int value = BL_User_Mgnt.GetExistsData("VAT_Master", "VAT_Name", vatname.ToString());
        //    return value.ToString();
        //}
        protected void ddlPartyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Party_Master> party = new List<Party_Master>();
            party = BL_Party_Master.GetList();
            var partynames = from s in party
                         where s.party_type_code == ddlPartyType.SelectedValue
                         select s;
           
            ddlParty.DataSource = partynames;
            ddlParty.DataTextField = "Party_Name";
            ddlParty.DataValueField = "Party_Code";
            ddlParty.DataBind();
            ddlParty.Items.Insert(0, "Select");
            txtVatName.Text = "";
            
        }
        [WebMethod]
        public static string chkDuplicateVATName(Object vatname)
        {
            int value = 0;
            string[] vat = vatname.ToString().Split('_');
            if (vatname1 !=vat[0])
            {
                value = BL_User_Mgnt.GetExistsData("VAT_Master", "VAT_Name", vatname.ToString());
            }
            return value.ToString();
        }
        protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Product_Master> content = new List<Product_Master>();
            content = BL_ProductMaster.GetProductMasterList(Session["UserID"].ToString());
            var contentnames = from s in content
                               where s.product_type_code == ddlProductType.SelectedValue
                               select s;
            ddlContent.DataSource = contentnames;
            ddlContent.DataTextField = "Product_Name";
            ddlContent.DataValueField = "Product_Code";
            ddlContent.DataBind();
            ddlContent.Items.Insert(0, "Select");
            txtVatName.Text = "";
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