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
    public partial class PartyFinancialYearList : System.Web.UI.Page
    {
        List<Financial_Years> financial_years = new List<Financial_Years>();
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
                partyFinancialList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                string userid = Session["UserID"].ToString();
                financial_years = new List<Financial_Years>();
                financial_years = BL_Financial_Years.GetFinacListValues();
                partyFinancialList.DataSource = financial_years;
                partyFinancialList.DataBind();
            }
        }

        protected void partyFinancialList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex == -1)
            {
                partyFinancialList.PageIndex = 0;
            }
            else
            {
                partyFinancialList.PageIndex = e.NewPageIndex;
            }
            GridViewRow row = partyFinancialList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["pfsearch"] != null && Session["pftext"] != null)
            {
                ddsearch.SelectedValue = Session["pfsearch"].ToString();
                txtpage.Text = Session["pftext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {

                        financial_years = new List<Financial_Years>();
                        financial_years = BL_Financial_Years.Search("party_type_master", ddsearch.SelectedValue, txtpage.Text);
                        partyFinancialList.DataSource = financial_years;
                        partyFinancialList.DataBind();
                    }
                }
            }
            else
            {




                financial_years = new List<Financial_Years>();
                financial_years = BL_Financial_Years.GetFinacListValues();
                partyFinancialList.DataSource = financial_years;
                partyFinancialList.DataBind();

            }
                
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string fin_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text;
            Session["UserID"] = Session["UserID"].ToString();
            Session["fin_id"] = fin_id;
            Session["rtype"] = 1;
            Response.Redirect("PartyFinancialYear");
        }

        protected void financiallist_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PartyFinancialYearList");
        }
        protected void partyfinancialyears_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PartyFinancialYearList");
        }

        protected void AddRecords_Click(object sender, EventArgs e)
        {
            Session["rtype"] =0;
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PartyFinancialYear");
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
        protected void uommaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("UOMMasterList");
        }

        protected void DispatchTypeMaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DispatchTypeMasterList.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string fin_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text;
            Session["UserID"] = Session["UserID"].ToString();
            Session["fin_id"] = fin_id;
            Session["rtype"] = 2;
            Response.Redirect("PartyFinancialYear");
        }
        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = partyFinancialList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                string userid = Session["UserID"].ToString();
                financial_years = new List<Financial_Years>();
                financial_years = BL_Financial_Years.GetFinacListValues();
                partyFinancialList.DataSource = financial_years;
                partyFinancialList.DataBind();
            }
            return financial_years.ToString();
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {


            GridViewRow row = partyFinancialList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["pfsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["pftext"] = txtpage.Text;

                    financial_years = new List<Financial_Years>();
                    financial_years = BL_Financial_Years.Search("party_type_master", ddsearch.SelectedValue, txtpage.Text);
                    partyFinancialList.DataSource = financial_years;
                    partyFinancialList.DataBind();
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
            GridViewRow row = partyFinancialList.TopPagerRow;
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
                partyFinancialList.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                partyFinancialList.PageIndex = a - 1;
            }
            

            string userid = Session["UserID"].ToString();
            financial_years = new List<Financial_Years>();
            financial_years = BL_Financial_Years.GetFinacListValues();
            partyFinancialList.DataSource = financial_years;
            partyFinancialList.DataBind();


        }

        protected void partyFinancialList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = partyFinancialList.TopPagerRow;
            if (partyFinancialList.Rows.Count > 0)
            {
                partyFinancialList.TopPagerRow.Visible = true;
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
            if (Session["pfsearch"] != null && Session["pftext"] != null)
            {
                ddsearch.SelectedValue = Session["pfsearch"].ToString();
                txtpages.Text = Session["pftext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = partyFinancialList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = partyFinancialList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < partyFinancialList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == partyFinancialList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (partyFinancialList.PageIndex == 0)
            {
                ((ImageButton)partyFinancialList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)partyFinancialList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)partyFinancialList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)partyFinancialList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (partyFinancialList.PageIndex + 1 == partyFinancialList.PageCount)
            {
                ((ImageButton)partyFinancialList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)partyFinancialList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)partyFinancialList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)partyFinancialList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["pfsearch"] = null;
            Session["pftext"] = null;
            string userid = Session["UserID"].ToString();
            financial_years = new List<Financial_Years>();
            financial_years = BL_Financial_Years.GetFinacListValues();
            partyFinancialList.DataSource = financial_years;
            partyFinancialList.DataBind();
        }
    }
}