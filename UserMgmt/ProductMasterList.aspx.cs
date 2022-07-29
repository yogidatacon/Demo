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
    public partial class ProductMasterList : System.Web.UI.Page
    {
        List<Product_Master> product = new List<Product_Master>();

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
                grdProductMaster.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                string userid = Session["UserID"].ToString();
                Session["UserID"] = Session["UserID"];
                product = new List<Product_Master>();
                product = BL_ProductMaster.GetProductMasterList(userid);
                grdProductMaster.DataSource = product;
                grdProductMaster.DataBind();
                 
            }
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "0";
            Response.Redirect("ProductMasterForm.aspx");
        }


        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string product_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblCode") as Label).Text;
            string product_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblName") as Label).Text;
            string product_type_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblProduct") as Label).Text;
            string product_type_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblProductCode") as Label).Text;
            Session["UserID"] = Session["UserID"].ToString();
            Session["product_code"] = product_code;
            Session["product_name"] = product_name;
            Session["product_type_code"] = product_type_code;
            Session["product_type_name"] = product_type_name;
            Session["rType"] = 1;
            Response.Redirect("ProductMasterForm.aspx");
        }
        protected void btnEdite_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string product_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblCode") as Label).Text;
            string product_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblName") as Label).Text;
            string product_type_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblProduct") as Label).Text;
            string product_type_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblProductCode") as Label).Text;
            Session["UserID"] = Session["UserID"].ToString();
            Session["product_code"] = product_code;
            Session["product_name"] = product_name;
            Session["product_type_code"] = product_type_code;
            Session["product_type_name"] = product_type_name;
            Session["rType"] = 2;
            Response.Redirect("ProductMasterForm.aspx");
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
        protected void grdProductMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0)
            {
                grdProductMaster.PageIndex = 0;
            }
            else
            {
                grdProductMaster.PageIndex = e.NewPageIndex;
            }

            GridViewRow row = grdProductMaster.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["prosearch"] != null && Session["protext"] != null)
            {
                ddsearch.SelectedValue = Session["prosearch"].ToString();
                txtpage.Text = Session["protext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        string qery = txtSearch.Text.ToString();
                        qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());

                        product = new List<Product_Master>();
                        product = BL_ProductMaster.SearchProduct("product_master", ddsearch.SelectedValue, txtpage.Text);
                        grdProductMaster.DataSource = product;
                        grdProductMaster.DataBind();
                    }
                }
            }
            else
            {





                product = new List<Product_Master>();
                product = BL_ProductMaster.GetProductMasterList(Session["UserID"].ToString());
                grdProductMaster.DataSource = product;
                grdProductMaster.DataBind();
            }
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

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtSearch.Text == "")
                {
                    string userid = Session["UserID"].ToString();
                    Session["UserID"] = Session["UserID"];
                    product = new List<Product_Master>();
                    product = BL_ProductMaster.GetProductMasterList(userid);
                    grdProductMaster.DataSource = product;
                    grdProductMaster.DataBind();

                }
            }

        }
        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdProductMaster.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                string userid = Session["UserID"].ToString();
                product = new List<Product_Master>();
                product = BL_ProductMaster.GetProductMasterList(userid);
                grdProductMaster.DataSource = product;
                grdProductMaster.DataBind();
            }
            return product.ToString();
        }


        protected void btnsearch_Click(object sender, EventArgs e)
        {


            GridViewRow row = grdProductMaster.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["prosearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["protext"] = txtpage.Text;
                    string qery = txtSearch.Text.ToString();
                qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());

                string userid = Session["UserID"].ToString();
                Session["UserID"] = Session["UserID"];
                product = new List<Product_Master>();
                product = BL_ProductMaster.SearchProduct("product_master", ddsearch.SelectedValue,txtpage.Text);
                grdProductMaster.DataSource = product;
                grdProductMaster.DataBind();

            }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Select Field Name\');", true);
                ddsearch.Focus();
            }


        }

        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdProductMaster.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            if (Convert.ToInt32(txtpage.Text) < 1)
            {
                a = 0;
            }
            else
            {
                a = Convert.ToInt32(txtpage.Text);
            }
            if (Convert.ToInt32(txtpage.Text) <= 0)
            {
                grdProductMaster.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdProductMaster.PageIndex = a - 1;
            }
            

            string userid = Session["UserID"].ToString();
            Session["UserID"] = Session["UserID"];
            product = new List<Product_Master>();
            product = BL_ProductMaster.GetProductMasterList(userid);
            grdProductMaster.DataSource = product;
            grdProductMaster.DataBind();


        }

        protected void grdProductMaster_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdProductMaster.TopPagerRow;
            if (grdProductMaster.Rows.Count > 0)
            {
                grdProductMaster.TopPagerRow.Visible = true;
            }
            if (row == null)
            {
                return;
            }

            DropDownList DDLPage = (DropDownList)row.Cells[0].FindControl("DDLPage");
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            Label lblPages = (Label)row.Cells[0].FindControl("lblPages");
            Label lblCurrent = (Label)row.Cells[0].FindControl("lblCurrent");
            TextBox txtpages = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["prosearch"] != null && Session["protext"] != null)
            {
                ddsearch.SelectedValue = Session["prosearch"].ToString();
                txtpages.Text = Session["protext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdProductMaster.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdProductMaster.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdProductMaster.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdProductMaster.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdProductMaster.PageIndex == 0)
            {
                ((ImageButton)grdProductMaster.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdProductMaster.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdProductMaster.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdProductMaster.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdProductMaster.PageIndex + 1 == grdProductMaster.PageCount)
            {
                ((ImageButton)grdProductMaster.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdProductMaster.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdProductMaster.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdProductMaster.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["prosearch"] = null;
            Session["protext"] = null;
            string userid = Session["UserID"].ToString();
            Session["UserID"] = Session["UserID"];
            product = new List<Product_Master>();
            product = BL_ProductMaster.GetProductMasterList(userid);
            grdProductMaster.DataSource = product;
            grdProductMaster.DataBind();
        }
    }
}