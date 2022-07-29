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
    public partial class orgFinancialyrlist : System.Web.UI.Page
    {
        List<Org_Finacial_yr> Org_Finacial = new List<Org_Finacial_yr>();
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
                OrgFinancialList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                string userid = Session["UserID"].ToString();
                Org_Finacial = new List<Org_Finacial_yr>();
                Org_Finacial = BL_org_Master.GetFinacListValues(userid);
                OrgFinancialList.DataSource = Org_Finacial;
                OrgFinancialList.DataBind();
            }
        }

        protected void OrganisationDetails_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("OrgList");
        }

        protected void OrganisationFinancialYear_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("orgFinancialyrlist");
        }
        protected void Module_Master1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ModuleMasterList");
        }

        protected void Submodule_master1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("SubModuleList");
        }

        protected void tab_master1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("TabNameList");
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string org_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblorg_id") as Label).Text;
            string OrgName = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblOrgName") as Label).Text;
            string lblFinancialYear = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblFinancialYear") as Label).Text;
            Session["UserID"] = Session["UserID"].ToString();
            Session["org_id"] = org_id;
            Session["rType"] = 1;
            Session["OrgName"] = OrgName;
            Session["FinancialYear"] = lblFinancialYear;
            Response.Redirect("orgFinancialyrform");
        }

        protected void btnInactive_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Session["rType"] =2;
        }

        protected void OrgFinancialList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0)
            {
                OrgFinancialList.PageIndex = 0;
            }
            else
            {
                OrgFinancialList.PageIndex = e.NewPageIndex;
            }

            GridViewRow row = OrgFinancialList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["ofsearch"] != null && Session["oftext"] != null)
            {
                ddsearch.SelectedValue = Session["ofsearch"].ToString();
                txtpage.Text = Session["oftext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        string qery = txtSearch.Text.ToString();
                        qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


                        string userid = Session["UserID"].ToString();
                        Org_Finacial = new List<Org_Finacial_yr>();
                        Org_Finacial = BL_org_Master.SearchFinacialYear("org_financial_yr", ddsearch.SelectedValue, txtpage.Text);
                        OrgFinancialList.DataSource = Org_Finacial;
                        OrgFinancialList.DataBind();
                    }
                }
            }
            else
            {


                string userid = Session["UserID"].ToString();
                Org_Finacial = new List<Org_Finacial_yr>();
                Org_Finacial = BL_org_Master.GetFinacListValues(userid);
                OrgFinancialList.DataSource = Org_Finacial;
                OrgFinancialList.DataBind();
            }
        }

        protected void AddRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Session["rType"] = 0;
            Response.Redirect("orgFinancialyrform");
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtSearch.Text == "")
                {
                    string userid = Session["UserID"].ToString();
                    Org_Finacial = new List<Org_Finacial_yr>();
                    Org_Finacial = BL_org_Master.GetFinacListValues(userid);
                    OrgFinancialList.DataSource = Org_Finacial;
                    OrgFinancialList.DataBind();
                    // (grdAccessTypeList.DataSource as DataTable).DefaultView.RowFilter = string.Format("access_type_name LIKE '{1}%' or access_type_desc LIKE '{2}%'  ", txtSearch.Text, txtSearch.Text);
                }
            }

        }

        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = OrgFinancialList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                string userid = Session["UserID"].ToString();
                Org_Finacial = new List<Org_Finacial_yr>();
                Org_Finacial = BL_org_Master.GetFinacListValues(userid);
                OrgFinancialList.DataSource = Org_Finacial;
                OrgFinancialList.DataBind();
            }
            return OrgFinancialList.ToString();
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GridViewRow row = OrgFinancialList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["ofsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["oftext"] = txtpage.Text;
                    string qery = txtSearch.Text.ToString();
                qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());

                string userid = Session["UserID"].ToString();
                Org_Finacial = new List<Org_Finacial_yr>();
                Org_Finacial = BL_org_Master.SearchFinacialYear("org_financial_yr", ddsearch.SelectedValue,txtpage.Text);
                OrgFinancialList.DataSource = Org_Finacial;
                OrgFinancialList.DataBind();

            }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Select Filed Name\');", true);
                ddsearch.Focus();
            }


        }



        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = OrgFinancialList.TopPagerRow;
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
                OrgFinancialList.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                OrgFinancialList.PageIndex = a - 1;
            }
            

            
            string userid = Session["UserID"].ToString();
            Org_Finacial = new List<Org_Finacial_yr>();
            Org_Finacial = BL_org_Master.GetFinacListValues(userid);
            OrgFinancialList.DataSource = Org_Finacial;
            OrgFinancialList.DataBind();


        }

        protected void OrgFinancialList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = OrgFinancialList.TopPagerRow;
            if (OrgFinancialList.Rows.Count > 0)
            {
                OrgFinancialList.TopPagerRow.Visible = true;
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
            if (Session["ofsearch"] != null && Session["oftext"] != null)
            {
                ddsearch.SelectedValue = Session["ofsearch"].ToString();
                txtpages.Text = Session["oftext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = OrgFinancialList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = OrgFinancialList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < OrgFinancialList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == OrgFinancialList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (OrgFinancialList.PageIndex == 0)
            {
                ((ImageButton)OrgFinancialList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)OrgFinancialList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)OrgFinancialList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)OrgFinancialList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (OrgFinancialList.PageIndex + 1 == OrgFinancialList.PageCount)
            {
                ((ImageButton)OrgFinancialList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)OrgFinancialList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)OrgFinancialList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)OrgFinancialList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["ofsearch"] = null;
            Session["oftext"] = null;
            string userid = Session["UserID"].ToString();
            Org_Finacial = new List<Org_Finacial_yr>();
            Org_Finacial = BL_org_Master.GetFinacListValues(userid);
            OrgFinancialList.DataSource = Org_Finacial;
            OrgFinancialList.DataBind();


        }
    }
}