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
    public partial class SubModuleList : System.Web.UI.Page
    {
        List<SubModule_Master> submodules = new List<SubModule_Master>();
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

                grdsubmoduleList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                submodules = new List<SubModule_Master>();
                submodules = BL_SubModule_Master.GetList();
                grdsubmoduleList.DataSource = submodules;
                grdsubmoduleList.DataBind();
            }
        }
        protected void AddRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Session["rtype"] = "0";
            Response.Redirect("SubModuleMasterForm");
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
            string org_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblOrg_id") as Label).Text;
            string Module_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblmns_no") as Label).Text;
            string submodule_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblsubmodulename") as Label).Text;
            string submodule_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblsubModuleCode") as Label).Text;
            Session["org_name"] = org_name;
            Session["Module_name"] = Module_name;
            Session["submodule_name"] = submodule_name;
            Session["submodule_code"] = submodule_code;
            Session["UserID"] = Session["UserID"].ToString();
            Session["rtype"] = "1";
            Response.Redirect("SubModuleMasterForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string org_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblOrg_id") as Label).Text;
            string Module_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblmns_no") as Label).Text;
            string submodule_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblsubmodulename") as Label).Text;
            string submodule_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblsubModuleCode") as Label).Text;
            Session["org_name"] = org_name;
            Session["Module_name"] = Module_name;
            Session["submodule_name"] = submodule_name;
            Session["submodule_code"] = submodule_code;
            Session["UserID"] = Session["UserID"].ToString();
            Session["rtype"] = "2";
            Response.Redirect("SubModuleMasterForm");
        }

        protected void grdsubmoduleList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0)
            {
                grdsubmoduleList.PageIndex = 0;
            }
            else
            {
                grdsubmoduleList.PageIndex = e.NewPageIndex;
            }

            GridViewRow row = grdsubmoduleList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["susearch"] != null && Session["sutext"] != null)
            {
                ddsearch.SelectedValue = Session["susearch"].ToString();
                txtpage.Text = Session["sutext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        string qery = txtSearch.Text.ToString();
                        qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


                        string userid = Session["UserID"].ToString();
                        submodules = new List<SubModule_Master>();
                        submodules = BL_SubModule_Master.SearchSubModule("submodule_master", ddsearch.SelectedValue, txtpage.Text);
                        grdsubmoduleList.DataSource = submodules;
                        grdsubmoduleList.DataBind();
                    }
                }
            }
            else
            {


                submodules = new List<SubModule_Master>();
                submodules = BL_SubModule_Master.GetList();
                grdsubmoduleList.DataSource = submodules;
                grdsubmoduleList.DataBind();
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtSearch.Text == "")
                {
                    string userid = Session["UserID"].ToString();
                    submodules = new List<SubModule_Master>();
                    submodules = BL_SubModule_Master.GetList();
                    grdsubmoduleList.DataSource = submodules;
                    grdsubmoduleList.DataBind();
                    // (grdAccessTypeList.DataSource as DataTable).DefaultView.RowFilter = string.Format("access_type_name LIKE '{1}%' or access_type_desc LIKE '{2}%'  ", txtSearch.Text, txtSearch.Text);
                }
            }
        }
        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdsubmoduleList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                string userid = Session["UserID"].ToString();
                submodules = new List<SubModule_Master>();
                submodules = BL_SubModule_Master.GetList();
                grdsubmoduleList.DataSource = submodules;
                grdsubmoduleList.DataBind();
            }
            return submodules.ToString();
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GridViewRow row = grdsubmoduleList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["susearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["sutext"] = txtpage.Text;
                    string qery = txtSearch.Text.ToString();
                qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());

                submodules = new List<SubModule_Master>();
                submodules = BL_SubModule_Master.SearchSubModule("submodule_master", ddsearch.SelectedValue,txtpage.Text);
                grdsubmoduleList.DataSource = submodules;
                grdsubmoduleList.DataBind();

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
            GridViewRow row = grdsubmoduleList.TopPagerRow;
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
                grdsubmoduleList.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdsubmoduleList.PageIndex = a - 1;
            }
            

            string userid = Session["UserID"].ToString();
            submodules = new List<SubModule_Master>();
            submodules = BL_SubModule_Master.GetList();
            grdsubmoduleList.DataSource = submodules;
            grdsubmoduleList.DataBind();


        }

        protected void grdsubmoduleList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdsubmoduleList.TopPagerRow;
            if (grdsubmoduleList.Rows.Count > 0)
            {
                grdsubmoduleList.TopPagerRow.Visible = true;
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
            if (Session["susearch"] != null && Session["sutext"] != null)
            {
                ddsearch.SelectedValue = Session["susearch"].ToString();
                txtpages.Text = Session["sutext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdsubmoduleList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdsubmoduleList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdsubmoduleList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdsubmoduleList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdsubmoduleList.PageIndex == 0)
            {
                ((ImageButton)grdsubmoduleList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdsubmoduleList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdsubmoduleList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdsubmoduleList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdsubmoduleList.PageIndex + 1 == grdsubmoduleList.PageCount)
            {
                ((ImageButton)grdsubmoduleList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdsubmoduleList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdsubmoduleList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdsubmoduleList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["susearch"] = null;
            Session["sutext"] = null;
            string userid = Session["UserID"].ToString();
            submodules = new List<SubModule_Master>();
            submodules = BL_SubModule_Master.GetList();
            grdsubmoduleList.DataSource = submodules;
            grdsubmoduleList.DataBind();
        }
    }
}