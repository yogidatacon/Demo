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
    public partial class UOMMasterList : System.Web.UI.Page
    {
        List<UOM_Master> uomlist = new List<UOM_Master>();
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
                grdProductList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                uomlist = new List<UOM_Master>();
                uomlist = BL_UOM.GetList(Session["UserID"].ToString());
                grdProductList.DataSource = uomlist;
                grdProductList.DataBind();
            }

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
        protected void uommaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("UOMMasterList");
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "0";
            Response.Redirect("UOMMasterForm");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string uom_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblCode") as Label).Text;
            string uom_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblName") as Label).Text;
            Session["uom_code"] = uom_code;
            Session["uom_name"] = uom_name;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "1";
            Response.Redirect("UOMMasterForm");
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string uom_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblCode") as Label).Text;
            string uom_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblName") as Label).Text;
            Session["uom_code"] = uom_code;
            Session["uom_name"] = uom_name;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "2";
            Response.Redirect("UOMMasterForm");
        }
        protected void vattypemaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VatTypeMasterList");
        }
        protected void grdProductList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0)
            {
                grdProductList.PageIndex = 0;
            }
            else
            {
                grdProductList.PageIndex = e.NewPageIndex;
            }

            GridViewRow row = grdProductList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["uomsearch"] != null && Session["uomtext"] != null)
            {
                ddsearch.SelectedValue = Session["uomsearch"].ToString();
                txtpage.Text = Session["uomtext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        string qery = txtSearch.Text.ToString();
                        qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());

                        uomlist = new List<UOM_Master>();
                        uomlist = BL_UOM.SearchUOM("uom_Master", ddsearch.SelectedValue, txtpage.Text);
                        grdProductList.DataSource = uomlist;
                        grdProductList.DataBind();
                    }
                }
            }
            else
            {




               
                uomlist = new List<UOM_Master>();
                uomlist = BL_UOM.GetList(Session["UserID"].ToString());
                grdProductList.DataSource = uomlist;
                grdProductList.DataBind();
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
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    uomlist = new List<UOM_Master>();
                    uomlist = BL_UOM.GetList(Session["UserID"].ToString());
                    grdProductList.DataSource = uomlist;
                    grdProductList.DataBind();

                }
            }

        }
        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdProductList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                string userid = Session["UserID"].ToString();
                uomlist = new List<UOM_Master>();
                uomlist = BL_UOM.GetList(Session["UserID"].ToString());
                grdProductList.DataSource = uomlist;
                grdProductList.DataBind();

            }
            return uomlist.ToString();
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {


            GridViewRow row = grdProductList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["uomsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["uomtext"] = txtpage.Text;
                    string qery = txtSearch.Text.ToString();
                qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());

                uomlist = new List<UOM_Master>();
                uomlist = BL_UOM.SearchUOM("uom_Master", ddsearch.SelectedValue,txtpage.Text);
                grdProductList.DataSource = uomlist;
                grdProductList.DataBind();
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
            GridViewRow row = grdProductList.TopPagerRow;
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
                grdProductList.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdProductList.PageIndex = a - 1;
            }
            

            uomlist = new List<UOM_Master>();
            uomlist = BL_UOM.GetList(Session["UserID"].ToString());
            grdProductList.DataSource = uomlist;
            grdProductList.DataBind();


        }

        protected void grdProductList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdProductList.TopPagerRow;
            if (grdProductList.Rows.Count > 0)
            {
                grdProductList.TopPagerRow.Visible = true;
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
            if (Session["uomsearch"] != null && Session["uomtext"] != null)
            {
                ddsearch.SelectedValue = Session["uomsearch"].ToString();
                txtpages.Text = Session["uomtext"].ToString();
            }


                //if (lblPages != null)
                //{
                lblPages.Text = grdProductList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdProductList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdProductList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdProductList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdProductList.PageIndex == 0)
            {
                ((ImageButton)grdProductList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdProductList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdProductList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdProductList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdProductList.PageIndex + 1 == grdProductList.PageCount)
            {
                ((ImageButton)grdProductList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdProductList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdProductList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdProductList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["uomsearch"] = null;
            Session["uomtext"] = null;
            uomlist = new List<UOM_Master>();
            uomlist = BL_UOM.GetList(Session["UserID"].ToString());
            grdProductList.DataSource = uomlist;
            grdProductList.DataBind();

        }
    }
}