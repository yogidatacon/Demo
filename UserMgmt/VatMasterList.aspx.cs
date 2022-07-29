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
    public partial class VatMasterList : System.Web.UI.Page
    {
        List<VAT_Master> vatmasters = new List<VAT_Master>();
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
                grdvatmasterList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                string userid = Session["UserID"].ToString();
                Session["UserID"] = Session["UserID"];
                vatmasters = new List<VAT_Master>();
                vatmasters = BL_VATMaster.GetvatmasterList(userid);
                grdvatmasterList.DataSource = vatmasters;
                grdvatmasterList.DataBind();
            }
        }
        protected void DispatchTypeMaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DispatchTypeMasterList.aspx");
        }
        protected void grdvatmasterList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex == -1)
            {
                grdvatmasterList.PageIndex = 0;
            }
            else
            {
                grdvatmasterList.PageIndex = e.NewPageIndex;
            }
            GridViewRow row = grdvatmasterList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["vsearch"] != null && Session["vtext"] != null)
            {
                ddsearch.SelectedValue = Session["vsearch"].ToString();
                txtpage.Text = Session["vtext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        string qery = txtSearch.Text.ToString();
                        qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());

                        vatmasters = new List<VAT_Master>();
                        vatmasters = BL_VATMaster.SearchVATMaster("vat_master", ddsearch.SelectedValue, txtpage.Text);
                        grdvatmasterList.DataSource = vatmasters;
                        grdvatmasterList.DataBind();
                    }
                }
            }
            else
            {


                vatmasters = new List<VAT_Master>();
                vatmasters = BL_VATMaster.GetvatmasterList(Session["UserID"].ToString());
                grdvatmasterList.DataSource = vatmasters;
                grdvatmasterList.DataBind();
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "1";
            Response.Redirect("VATMasterForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string vat_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblvat_code") as Label).Text;
            Session["vat_code"] = vat_code;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "2";
            Response.Redirect("VATMasterForm");
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "0";
            Response.Redirect("VATMasterForm");
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

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtSearch.Text == "")
                {

                    string userid = Session["UserID"].ToString();
                    Session["UserID"] = Session["UserID"];
                    vatmasters = new List<VAT_Master>();
                    vatmasters = BL_VATMaster.GetvatmasterList(userid);
                    grdvatmasterList.DataSource = vatmasters;
                    grdvatmasterList.DataBind();

                }
            }

        }
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdvatmasterList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                string userid = Session["UserID"].ToString();
                vatmasters = new List<VAT_Master>();
                vatmasters = BL_VATMaster.GetvatmasterList(userid);
                grdvatmasterList.DataSource = vatmasters;
                grdvatmasterList.DataBind();
            }
            return vatmasters.ToString();
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {

            GridViewRow row = grdvatmasterList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["vsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["vtext"] = txtpage.Text;
                    string qery = txtSearch.Text.ToString();
                qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());

                string userid = Session["UserID"].ToString();
                Session["UserID"] = Session["UserID"];
                vatmasters = new List<VAT_Master>();
                vatmasters = BL_VATMaster.SearchVATMaster("vat_master", ddsearch.SelectedValue,txtpage.Text);
                grdvatmasterList.DataSource = vatmasters;
                grdvatmasterList.DataBind();
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
            GridViewRow row = grdvatmasterList.TopPagerRow;
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
                grdvatmasterList.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdvatmasterList.PageIndex = a - 1;
            }
            

            string userid = Session["UserID"].ToString();
            Session["UserID"] = Session["UserID"];
            vatmasters = new List<VAT_Master>();
            vatmasters = BL_VATMaster.GetvatmasterList(userid);
            grdvatmasterList.DataSource = vatmasters;
            grdvatmasterList.DataBind();


        }

        protected void grdvatmasterList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdvatmasterList.TopPagerRow;
            if (grdvatmasterList.Rows.Count > 0)
            {
                grdvatmasterList.TopPagerRow.Visible = true;
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
            if (Session["vsearch"] != null && Session["vtext"] != null)
            {
                ddsearch.SelectedValue = Session["vsearch"].ToString();
                txtpages.Text = Session["vtext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdvatmasterList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdvatmasterList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdvatmasterList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdvatmasterList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdvatmasterList.PageIndex == 0)
            {
                ((ImageButton)grdvatmasterList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdvatmasterList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdvatmasterList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdvatmasterList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdvatmasterList.PageIndex + 1 == grdvatmasterList.PageCount)
            {
                ((ImageButton)grdvatmasterList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdvatmasterList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdvatmasterList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdvatmasterList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["vsearch"] = null;
            Session["vtext"] = null;
            string userid = Session["UserID"].ToString();
            Session["UserID"] = Session["UserID"];
            vatmasters = new List<VAT_Master>();
            vatmasters = BL_VATMaster.GetvatmasterList(userid);
            grdvatmasterList.DataSource = vatmasters;
            grdvatmasterList.DataBind();

        }
    }
}