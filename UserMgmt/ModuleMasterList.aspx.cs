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
    public partial class ModuleMasterList : System.Web.UI.Page
    {
        List<Module_Master> modules = new List<Module_Master>();
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
                grdmodulemasterList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                Session["UserID"] = Session["UserID"];
                modules = new List<Module_Master>();
                modules = BL_Module_Master.GetList();
                grdmodulemasterList.DataSource = modules;
                grdmodulemasterList.DataBind();
            }
        }

        protected void AddRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Session["rtype"] = "0";
            Response.Redirect("ModuleMasterForm");
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

        protected void grdmodulemasterList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0)
            {
                grdmodulemasterList.PageIndex = 0;
            }
            else
            {
                grdmodulemasterList.PageIndex = e.NewPageIndex;
            }

            GridViewRow row = grdmodulemasterList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["mosearch"] != null && Session["motext"] != null)
            {
                ddsearch.SelectedValue = Session["mosearch"].ToString();
                txtpage.Text = Session["motext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        string qery = txtSearch.Text.ToString();
                        qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


                        string userid = Session["UserID"].ToString();
                        modules = new List<Module_Master>();
                        modules = BL_Module_Master.SearchModule("module_master", ddsearch.SelectedValue, txtpage.Text);
                        grdmodulemasterList.DataSource = modules;
                        grdmodulemasterList.DataBind();
                    }
                }
            }
            else
            {


               
                modules = new List<Module_Master>();
                modules = BL_Module_Master.GetList();
                grdmodulemasterList.DataSource = modules;
                grdmodulemasterList.DataBind();
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string org_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblorg_id") as Label).Text;
            string module_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblModuleCode") as Label).Text;
            string module_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblmodulename") as Label).Text;
            string mns = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblmns") as Label).Text;
            Session["org_id"] = org_id;
            Session["module_code"] = module_code;
            Session["mns_no"] = mns;
            Session["module_name"] = module_name;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "1";//ModuleMasterForm
            Response.Redirect("ModuleMasterForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string org_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblorg_id") as Label).Text;
            string module_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblModuleCode") as Label).Text;
            string module_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblmodulename") as Label).Text;
            string mns = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblmns") as Label).Text;
            Session["org_id"] = org_id;
            Session["module_code"] = module_code;
            Session["mns_no"] = mns;
            Session["module_name"] = module_name;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "2";//ModuleMasterForm
            Response.Redirect("ModuleMasterForm");
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtSearch.Text == "")
                {
                    string userid = Session["UserID"].ToString();
                    modules = new List<Module_Master>();
                    modules = BL_Module_Master.GetList();
                    grdmodulemasterList.DataSource = modules;
                    grdmodulemasterList.DataBind();
                    // (grdAccessTypeList.DataSource as DataTable).DefaultView.RowFilter = string.Format("access_type_name LIKE '{1}%' or access_type_desc LIKE '{2}%'  ", txtSearch.Text, txtSearch.Text);
                }
            }
        }
        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdmodulemasterList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                string userid = Session["UserID"].ToString();
                modules = new List<Module_Master>();
                modules = BL_Module_Master.GetList();
                grdmodulemasterList.DataSource = modules;
                grdmodulemasterList.DataBind();
            }
            return modules.ToString();
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GridViewRow row = grdmodulemasterList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["mosearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["motext"] = txtpage.Text;
                    string qery = txtSearch.Text.ToString();
                qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());

                modules = new List<Module_Master>();
                modules = BL_Module_Master.SearchModule("module_master",ddsearch.SelectedValue,txtpage.Text);
                grdmodulemasterList.DataSource = modules;
                grdmodulemasterList.DataBind();

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
            GridViewRow row = grdmodulemasterList.TopPagerRow;
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
                grdmodulemasterList.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdmodulemasterList.PageIndex = a - 1;
            }
            

            string userid = Session["UserID"].ToString();
            Session["UserID"] = Session["UserID"];
            modules = new List<Module_Master>();
            modules = BL_Module_Master.GetList();
            grdmodulemasterList.DataSource = modules;
            grdmodulemasterList.DataBind();


        }

        protected void grdmodulemasterList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdmodulemasterList.TopPagerRow;
            if (grdmodulemasterList.Rows.Count > 0)
            {
                grdmodulemasterList.TopPagerRow.Visible = true;
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
            if (Session["mosearch"] != null && Session["motext"] != null)
            {
                ddsearch.SelectedValue = Session["mosearch"].ToString();
                txtpages.Text = Session["motext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdmodulemasterList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdmodulemasterList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdmodulemasterList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdmodulemasterList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdmodulemasterList.PageIndex == 0)
            {
                ((ImageButton)grdmodulemasterList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdmodulemasterList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdmodulemasterList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdmodulemasterList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdmodulemasterList.PageIndex + 1 == grdmodulemasterList.PageCount)
            {
                ((ImageButton)grdmodulemasterList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdmodulemasterList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdmodulemasterList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdmodulemasterList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["mosearch"] = null;
            Session["motext"] = null;
            modules = new List<Module_Master>();
            modules = BL_Module_Master.GetList();
            grdmodulemasterList.DataSource = modules;
            grdmodulemasterList.DataBind();
        }
    }
}