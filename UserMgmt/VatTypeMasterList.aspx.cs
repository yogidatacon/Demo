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
    public partial class VatTypeMasterList : System.Web.UI.Page
    {
        List<VatType> vattypelists = new List<VatType>();
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
                grdVatMasterView.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                string userid = Session["UserID"].ToString();
                //Session["UserID"] = userid;
                vattypelists = new List<VatType>();
                vattypelists = BL_VatType.GetListValue(userid);
                grdVatMasterView.DataSource = vattypelists;
                grdVatMasterView.DataBind();
            }
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rType"] = 0;
            Response.Redirect("VatTypeMasterForm");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string vat_type_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblCode") as Label).Text;
            string vat_type_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblName") as Label).Text;

            Session["UserID"] = Session["UserID"].ToString();
            Session["vat_type_code"] = vat_type_code;
            Session["vat_type_name"] = vat_type_name;


            Session["rType"] = 1;
            Response.Redirect("VatTypeMasterForm");
        }

        protected void btnEdite_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string vat_type_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblCode") as Label).Text;
            string vat_type_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblName") as Label).Text;

            Session["UserID"] = Session["UserID"].ToString();
            Session["vat_type_code"] = vat_type_code;
            Session["vat_type_name"] = vat_type_name;


            Session["rType"] = 2;
            Response.Redirect("VatTypeMasterForm");
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
        protected void partytypemaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("PartyTypeMasterList");
        }
        protected void DispatchTypeMaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DispatchTypeMasterList.aspx");
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
        protected void partyfinancialyears_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PartyFinancialYearList");
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtSearch.Text == "")
                {

                    string userid = Session["UserID"].ToString();
                    //Session["UserID"] = userid;
                    
                    vattypelists = new List<VatType>();
                    vattypelists = BL_VatType.GetListValue(userid);
                    grdVatMasterView.DataSource = vattypelists;
                    grdVatMasterView.DataBind();

                }
            }

        }
        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdVatMasterView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                string userid = Session["UserID"].ToString();
                vattypelists = new List<VatType>();
                vattypelists = BL_VatType.GetListValue(userid);
                grdVatMasterView.DataSource = vattypelists;
                grdVatMasterView.DataBind();

            }
            return vattypelists.ToString();
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {

            GridViewRow row = grdVatMasterView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["vtsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["vttext"] = txtpage.Text;
                    string qery = txtSearch.Text.ToString();
                qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());

                vattypelists = new List<VatType>();
                vattypelists = BL_VatType.SearchVatType("vat_type_master",ddsearch.SelectedValue,txtpage.Text);
                grdVatMasterView.DataSource = vattypelists;
                grdVatMasterView.DataBind();
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
            GridViewRow row = grdVatMasterView.TopPagerRow;
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
                grdVatMasterView.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdVatMasterView.PageIndex = a - 1;
            }
            

            string userid = Session["UserID"].ToString();
            
            //Session["UserID"] = userid;
            vattypelists = new List<VatType>();
            vattypelists = BL_VatType.GetListValue(userid);
            grdVatMasterView.DataSource = vattypelists;
            grdVatMasterView.DataBind();


        }

        protected void grdVatMasterView_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdVatMasterView.TopPagerRow;
            if (grdVatMasterView.Rows.Count > 0)
            {
                grdVatMasterView.TopPagerRow.Visible = true;
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
            if (Session["vtsearch"] != null && Session["vttext"] != null)
            {
                ddsearch.SelectedValue = Session["vtsearch"].ToString();
                txtpages.Text = Session["vttext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdVatMasterView.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdVatMasterView.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdVatMasterView.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdVatMasterView.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdVatMasterView.PageIndex == 0)
            {
                ((ImageButton)grdVatMasterView.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdVatMasterView.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdVatMasterView.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdVatMasterView.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdVatMasterView.PageIndex + 1 == grdVatMasterView.PageCount)
            {
                ((ImageButton)grdVatMasterView.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdVatMasterView.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdVatMasterView.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdVatMasterView.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void grdVatMasterView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            string userid = Session["UserID"].ToString();
            if (e.NewPageIndex == -1)
            {
                grdVatMasterView.PageIndex = 0;
            }
            else
            {
                grdVatMasterView.PageIndex = e.NewPageIndex;
            }
            GridViewRow row = grdVatMasterView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["vtsearch"] != null && Session["vttext"] != null)
            {
                ddsearch.SelectedValue = Session["vtsearch"].ToString();
                txtpage.Text = Session["vttext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        string qery = txtSearch.Text.ToString();
                        qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());

                        vattypelists = new List<VatType>();
                        vattypelists = BL_VatType.SearchVatType("vat_type_master", ddsearch.SelectedValue, txtpage.Text);
                        grdVatMasterView.DataSource = vattypelists;
                        grdVatMasterView.DataBind();
                    }
                }
            }
            else
            {
                vattypelists = new List<VatType>();
                vattypelists = BL_VatType.GetListValue(userid);
                grdVatMasterView.DataSource = vattypelists;
                grdVatMasterView.DataBind();
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["vtsearch"] = null;
            Session["vttext"] = null;
            string userid = Session["UserID"].ToString();

            //Session["UserID"] = userid;
            vattypelists = new List<VatType>();
            vattypelists = BL_VatType.GetListValue(userid);
            grdVatMasterView.DataSource = vattypelists;
            grdVatMasterView.DataBind();
        }
    }
}