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
    public partial class TabNameList : System.Web.UI.Page
    {
        List<Tab_Master>  tabnames = new List<Tab_Master>();
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
                grdTabnameList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                tabnames = new List<Tab_Master>();
                tabnames = BL_Tab_Master.GetList();
                grdTabnameList.DataSource = tabnames;
                grdTabnameList.DataBind();

            }
        }
        protected void AddRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Session["rtype"] = "0";
            Response.Redirect("TabNameMasterForm");
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
            string org_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblorg_name") as Label).Text;
            string Module_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblModule_Name") as Label).Text;
            string submodule_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblsubmodule_name") as Label).Text;
            string tab_name_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lbltab_name_id") as Label).Text;
            string tab_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lbltab_name") as Label).Text;
            string tab_desc = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lbltab_desc") as Label).Text;
            Session["org_name"] = org_name;
            Session["Module_name"] = Module_name;
            Session["submodule_name"] = submodule_name;
            Session["tab_name_id"] = tab_name_id;
            Session["tab_name"] = tab_name;
            Session["tab_desc"] = tab_desc;
            Session["UserID"] = Session["UserID"].ToString();
            Session["rtype"] = "1";
            Response.Redirect("TabNameMasterForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string org_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblorg_name") as Label).Text;
            string Module_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblModule_Name") as Label).Text;
            string submodule_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblsubmodule_name") as Label).Text;
            string tab_name_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lbltab_name_id") as Label).Text;
            string tab_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lbltab_name") as Label).Text;
            string tab_desc = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lbltab_desc") as Label).Text;
            Session["org_name"] = org_name;
            Session["Module_name"] = Module_name;
            Session["submodule_name"] = submodule_name;
            Session["tab_name_id"] = tab_name_id;
            Session["tab_name"] = tab_name;
            Session["tab_desc"] = tab_desc;
            Session["UserID"] = Session["UserID"].ToString();
            Session["rtype"] = "2";
            Response.Redirect("TabNameMasterForm");
        }

        protected void grdTabnameList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0)
            {
                grdTabnameList.PageIndex = 0;
            }
            else
            {
                grdTabnameList.PageIndex = e.NewPageIndex;
            }

            GridViewRow row = grdTabnameList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["tasearch"] != null && Session["tatext"] != null)
            {
                ddsearch.SelectedValue = Session["tasearch"].ToString();
                txtpage.Text = Session["tatext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        string qery = txtSearch.Text.ToString();
                        qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


                        string userid = Session["UserID"].ToString();
                        tabnames = new List<Tab_Master>();
                        tabnames = BL_Tab_Master.SearchTab("tabname_master", ddsearch.SelectedValue, txtpage.Text);
                        grdTabnameList.DataSource = tabnames;
                        grdTabnameList.DataBind();
                    }
                }
            }
            else
            {

               
                tabnames = new List<Tab_Master>();
                tabnames = BL_Tab_Master.GetList();
                grdTabnameList.DataSource = tabnames;
                grdTabnameList.DataBind();
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtSearch.Text == "")
                {
                    string userid = Session["UserID"].ToString();
                    tabnames = new List<Tab_Master>();
                    tabnames = BL_Tab_Master.GetList();
                    grdTabnameList.DataSource = tabnames;
                    grdTabnameList.DataBind();
                    // (grdAccessTypeList.DataSource as DataTable).DefaultView.RowFilter = string.Format("access_type_name LIKE '{1}%' or access_type_desc LIKE '{2}%'  ", txtSearch.Text, txtSearch.Text);
                }
            }
        }
        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdTabnameList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                string userid = Session["UserID"].ToString();
                tabnames = new List<Tab_Master>();
                tabnames = BL_Tab_Master.GetList();
                grdTabnameList.DataSource = tabnames;
                grdTabnameList.DataBind();
            }
            return tabnames.ToString();
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GridViewRow row = grdTabnameList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["tasearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["tatext"] = txtpage.Text;
                    string qery = txtSearch.Text.ToString();
                qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());

                tabnames = new List<Tab_Master>();
                tabnames = BL_Tab_Master.SearchTab("tabname_master", ddsearch.SelectedValue,txtpage.Text);
                grdTabnameList.DataSource = tabnames;
                grdTabnameList.DataBind();

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
            GridViewRow row = grdTabnameList.TopPagerRow;
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
                grdTabnameList.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdTabnameList.PageIndex = a - 1;
            }
            

            string userid = Session["UserID"].ToString();
            tabnames = new List<Tab_Master>();
            tabnames = BL_Tab_Master.GetList();
            grdTabnameList.DataSource = tabnames;
            grdTabnameList.DataBind();


        }

        protected void grdTabnameList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdTabnameList.TopPagerRow;
            if (grdTabnameList.Rows.Count > 0)
            {
                grdTabnameList.TopPagerRow.Visible = true;
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
            if (Session["tasearch"] != null && Session["tatext"] != null)
            {
                ddsearch.SelectedValue = Session["tasearch"].ToString();
                txtpages.Text = Session["tatext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdTabnameList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdTabnameList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdTabnameList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdTabnameList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdTabnameList.PageIndex == 0)
            {
                ((ImageButton)grdTabnameList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdTabnameList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdTabnameList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdTabnameList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdTabnameList.PageIndex + 1 == grdTabnameList.PageCount)
            {
                ((ImageButton)grdTabnameList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdTabnameList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdTabnameList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdTabnameList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["tasearch"] = null;
            Session["tatext"] = null;
            string userid = Session["UserID"].ToString();
            tabnames = new List<Tab_Master>();
            tabnames = BL_Tab_Master.GetList();
            grdTabnameList.DataSource = tabnames;
            grdTabnameList.DataBind();
        }
    }
}