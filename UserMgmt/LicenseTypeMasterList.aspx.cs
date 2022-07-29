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
    public partial class LicenseTypeMasterList : System.Web.UI.Page
    {
        List<LicenseType> list = new List<LicenseType>();
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
                grdLicenseList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                list = new List<LicenseType>();
                list = BL_LicenseType.GetList(Session["UserID"].ToString());
                grdLicenseList.DataSource = list;
                grdLicenseList.DataBind();
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
            Response.Redirect("LicenseTypeMasterForm");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string uom_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblCode") as Label).Text;
            string uom_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblName") as Label).Text;
            Session["license_code"] = uom_code;
            Session["license_name"] = uom_name;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "1";
            Response.Redirect("LicenseTypeMasterForm");
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string uom_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblCode") as Label).Text;
            string uom_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblName") as Label).Text;
            Session["license_code"] = uom_code;
            Session["license_name"] = uom_name;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "2";
            Response.Redirect("LicenseTypeMasterForm");
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
                grdLicenseList.PageIndex = 0;
            }
            else
            {
                grdLicenseList.PageIndex = e.NewPageIndex;
            }

            GridViewRow row = grdLicenseList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["lmsearch"] != null && Session["lmtext"] != null)
            {
                ddsearch.SelectedValue = Session["lmsearch"].ToString();
                txtpage.Text = Session["lmtext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        string qery = txtSearch.Text.ToString();
                        qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


                        list = new List<LicenseType>();
                        list = BL_LicenseType.SearchLicense("lic_type_Master", ddsearch.SelectedValue, txtpage.Text);
                        grdLicenseList.DataSource = list;
                        grdLicenseList.DataBind();
                    }
                }
            }
            else
            {




                list = new List<LicenseType>();
                list = BL_LicenseType.GetList(Session["UserID"].ToString());
                grdLicenseList.DataSource = list;
                grdLicenseList.DataBind();
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
                   list = new List<LicenseType>();
                   list = BL_LicenseType.GetList(Session["UserID"].ToString());
                    grdLicenseList.DataSource = list;
                    grdLicenseList.DataBind();

                }
            }

        }

        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdLicenseList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                string userid = Session["UserID"].ToString();
                list = new List<LicenseType>();
                list = BL_LicenseType.GetList(Session["UserID"].ToString());
                grdLicenseList.DataSource = list;
                grdLicenseList.DataBind();

            }
            return list.ToString();
        }


        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GridViewRow row = grdLicenseList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["lmsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["lmtext"] = txtpage.Text;

                    string qery = txtSearch.Text.ToString();
                qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());

                list = new List<LicenseType>();
                list = BL_LicenseType.SearchLicense("lic_type_Master",ddsearch.SelectedValue,txtpage.Text);
                grdLicenseList.DataSource =list;
                grdLicenseList.DataBind();
            }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Select Filed Name\');", true);
                ddsearch.Focus();
            }
        }

        protected void grdLicenseList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdLicenseList.TopPagerRow;
            if (grdLicenseList.Rows.Count > 0)
            {
                grdLicenseList.TopPagerRow.Visible = true;
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
            if (Session["lmsearch"] != null && Session["lmtext"] != null)
            {
                ddsearch.SelectedValue = Session["lmsearch"].ToString();
                txtpages.Text = Session["lmtext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdLicenseList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdLicenseList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdLicenseList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdLicenseList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdLicenseList.PageIndex == 0)
            {
                ((ImageButton)grdLicenseList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdLicenseList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdLicenseList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdLicenseList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdLicenseList.PageIndex + 1 == grdLicenseList.PageCount)
            {
                ((ImageButton)grdLicenseList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdLicenseList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdLicenseList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdLicenseList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }



        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdLicenseList.TopPagerRow;
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
                grdLicenseList.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdLicenseList.PageIndex = a - 1;
            }
            list = new List<LicenseType>();
            list = BL_LicenseType.GetList(Session["UserID"].ToString());
            grdLicenseList.DataSource = list;
            grdLicenseList.DataBind();
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["lmsearch"] = null;
            Session["lmtext"] = null;
            list = new List<LicenseType>();
            list = BL_LicenseType.GetList(Session["UserID"].ToString());
            grdLicenseList.DataSource = list;
            grdLicenseList.DataBind();
        }
    }
}