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
    public partial class ProductMasterForm : System.Web.UI.Page
    {
        public static string productname1;
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
                List<ProductType> Productlist = new List<ProductType>();
                Productlist = BL_ProductType.GetProductList(userid);
                DDProduct.DataSource = Productlist;
                DDProduct.DataTextField = "product_type_name";
                DDProduct.DataValueField= "product_type_code";
                DDProduct.DataBind();
                DDProduct.Items.Insert(0, "Select");
                string rtype = Session["rtype"].ToString();
                Session["rtype"] = rtype;
                if (rtype != "0")
                {
                    List<VAT_Master> party = new List<VAT_Master>();
                    party = BL_VATMaster.GetvatmasterList("");
                    var list = from s1 in party
                               where s1.content == Session["product_code"].ToString()
                               select s1;
                    if (rtype == "1" || list.ToList().Count>0)
                    {
                        txtcode.Text = Session["product_code"].ToString();
                        txtName.Text = Session["product_name"].ToString();
                        DDProduct.SelectedValue = Session["product_type_code"].ToString();
                        txtcode.ReadOnly = true;
                        DDProduct.Enabled = false;
                        txtName.ReadOnly = true;
                        btnCancel.Visible = false;
                        btnSave.Visible = false;
                    }
                    else if (rtype == "2")
                    {
                        txtcode.Text = Session["product_code"].ToString();
                        txtName.Text = Session["product_name"].ToString();
                        DDProduct.SelectedValue = Session["product_type_code"].ToString();
                        productname1 = Session["product_name"].ToString();
                        txtcode.ReadOnly = true;
                        txtName.Enabled = true;
                        btnCancel.Visible = true;
                        btnSave.Visible = true;
                    }
                }
            }
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ProductMasterList.aspx");
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
                Product_Master product = new Product_Master();
                string s = txtName.Text.ToString();
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                product.product_code = txtcode.Text.ToUpper();
                product.product_name =s;
                product.user_id = user_id;
                product.product_type_code = DDProduct.SelectedValue;
                if (BL_ProductMaster.InsertProductMaster(product))
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
                    Response.Redirect("ProductMasterList.aspx");
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
                Product_Master product = new Product_Master();
                product.product_code = txtcode.Text;
                string s = txtName.Text.ToString();
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                product.product_name =s;
                product.user_id = user_id;
                product.product_type_code = DDProduct.SelectedValue;
                if (BL_ProductMaster.UpdateProductMaster(product))
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
                    Response.Redirect("ProductMasterList.aspx");
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
        public static string chkDuplicateProductName(Object productname)
        {
            int value = 0;
            if (productname1 != productname.ToString())
            {
                string s = productname.ToString();
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                value = BL_User_Mgnt.GetExistsData("product_master", "product_name", s);
            }

            return value.ToString();
        }
        [WebMethod]
        public static string chkDuplicateProductCode(Object productcode)
        {
            int value = 0;

            value = BL_User_Mgnt.GetExistsData("product_master", "product_code", productcode.ToString().ToUpper());

            return value.ToString();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ProductMasterList.aspx");
           
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
        protected void vattypemaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VatTypeMasterList");
        }
        protected void partyfinancialyears_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PartyFinancialYearList");
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