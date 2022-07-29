using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    public partial class RawMaterialForm : System.Web.UI.Page
    {
        RawMaterial rawmaterial = new RawMaterial();
        List<RawMaterialType> rawmaterialtype = new List<RawMaterialType>();
        static public string meterialname;
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
                GetDropDownListValue();
                if (Session["rtype"].ToString() != "0")
                {
                    //rawmaterial = new RawMaterial();
                    //rawmaterial.rawmaterial_code = txtRawMaterialCod.Text;
                    //rawmaterial.rawmaterial_name = txtRawMaterialNam.Text;
                   
                    rawmaterial.rawmaterial_type_code = ddRawMaterialType.SelectedValue;
                    rawmaterial.uom_code = ddUOM.SelectedValue;

                    if (Session["rtype"].ToString() == "1")
                    {
                        ddRawMaterialType.SelectedValue = Session["rawmaterial_type_code"].ToString();
                        txtRawMaterialCod.Text = Session["rawmaterial_code"].ToString();
                        txtRawMaterialNam.Text = Session["rawmaterial_name"].ToString();
                        meterialname = txtRawMaterialNam.Text;
                        ddUOM.SelectedValue = Session["uom_code"].ToString();
                        DDProduct.SelectedValue = Session["product_type_code"].ToString();
                        txtRawMaterialCod.Enabled = false;
                        txtRawMaterialNam.Enabled = false;
                        DDProduct.Enabled = false;
                        ddRawMaterialType.Enabled = false;
                        ddUOM.Enabled = false;
                        btnCancel.Visible = false;
                        btnSave.Visible = false;

                    }
                    else
                    {
                        ddRawMaterialType.SelectedValue = Session["rawmaterial_type_code"].ToString();
                        txtRawMaterialCod.Text = Session["rawmaterial_code"].ToString();
                        txtRawMaterialNam.Text = Session["rawmaterial_name"].ToString();
                        DDProduct.SelectedValue = Session["product_type_code"].ToString();
                        ddUOM.SelectedValue = Session["uom_code"].ToString();

                        txtRawMaterialCod.Enabled=false;
                    }



                }
            }
        }


        private void GetDropDownListValue()
        {
            string userid = Session["UserID"].ToString();
            rawmaterialtype = new List<RawMaterialType>();
            rawmaterialtype = BL_RawMaterialType.GetRawMaterialList(userid);
            ddRawMaterialType.DataSource = rawmaterialtype;
            ddRawMaterialType.DataTextField = "rawmaterial_type_name";
            ddRawMaterialType.DataValueField = "rawmaterial_type_code";
            ddRawMaterialType.DataBind();
            ddRawMaterialType.Items.Insert(0, "Select");
            List<ProductType> Productlist = new List<ProductType>();
            Productlist = BL_ProductType.GetProductList(userid);
            DDProduct.DataSource = Productlist;
            DDProduct.DataTextField = "product_type_name";
            DDProduct.DataValueField = "product_type_code";
            DDProduct.DataBind();
            DDProduct.Items.Insert(0, "Select");
            NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString());
            cn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.uom_master", cn);
            
            ddUOM.DataSource = cmd.ExecuteReader();
            ddUOM.DataTextField = "uom_name";
            ddUOM.DataValueField = "uom_code";
            ddUOM.DataBind();
            ddUOM.Items.Insert(0, "Select");



        }
        protected void partyfinancialyears_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PartyFinancialYearList");
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialList.aspx");
        }
        [WebMethod]
        public static string CheckDuplicatesCode(Object code)
        {
            int value = BL_User_Mgnt.GetExistsData("rawmaterial_master", "rawmaterial_code", code.ToString().ToUpper());
            return value.ToString();
        }
        [WebMethod]
        public static string CheckDuplicatesName(Object name)
        {
            int value = 0;
            if (meterialname != name.ToString())
            {
                string s = name.ToString();
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                value = BL_User_Mgnt.GetExistsData("rawmaterial_master", "rawmaterial_name", s);
            }

            return value.ToString();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string rtype = Session["rtype"].ToString();
            string user_id = Session["UserID"].ToString();
            if (rtype == "0")
            {
                RawMaterial rawmaterial = new RawMaterial();
                rawmaterial.rawmaterial_code = txtRawMaterialCod.Text.ToUpper();
                string s = txtRawMaterialNam.Text.ToString();
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                rawmaterial.rawmaterial_name =s;
                rawmaterial.rawmaterial_type_code = ddRawMaterialType.SelectedValue;
                rawmaterial.uom_code = ddUOM.SelectedValue; 
                rawmaterial.user_id = user_id;
                rawmaterial.product_type_code = DDProduct.SelectedValue;
                if (BL_RawMaterial.InsertRawMaterial(rawmaterial))
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
                    Response.Redirect("RawMaterialList.aspx");
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
                RawMaterial rawmaterial = new RawMaterial();
                rawmaterial.rawmaterial_code = txtRawMaterialCod.Text.ToUpper();
                string s = txtRawMaterialNam.Text.ToString();
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                rawmaterial.rawmaterial_name = s;
                rawmaterial.rawmaterial_type_code = ddRawMaterialType.SelectedValue;
                rawmaterial.uom_code = ddUOM.SelectedValue;
                rawmaterial.user_id = user_id;
                rawmaterial.product_type_code = DDProduct.SelectedValue;
                if (BL_RawMaterial.UpdateRawMaterial(rawmaterial))
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
                    Response.Redirect("RawMaterialList.aspx");
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
            Response.Redirect("RawMaterialList.aspx");
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
        protected void DispatchTypeMaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DispatchTypeMasterList.aspx");
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