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
    public partial class OrgList : System.Web.UI.Page
    {
        List<Org_Master> org_master = new List<Org_Master>();
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
                if (Session.Count == 0)
                {
                    Response.Redirect("~/LoginPage");
                }
                else
                {
                    string userid = Session["UserID"].ToString();
                    org_master = new List<Org_Master>();
                    org_master = BL_org_Master.GetListValues(userid);
                    OrgListView.DataSource = org_master;
                    OrgListView.DataBind();
                }
                OrgListView.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
            }
        }

        

       

       

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string org_id = (gvr.Cells[gvr.Cells.Count-1].FindControl("lblorg_id") as Label).Text;
            Session["UserID"] = Session["UserID"].ToString();
            Session["org_id"] = org_id;
            Session["rType"] = 1;
            Response.Redirect("OrgFroms");
        }

        protected void grdMF2List_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0)
            {
                OrgListView.PageIndex = 0;
            }
            else
            {
                OrgListView.PageIndex = e.NewPageIndex;
            }

            GridViewRow row = OrgListView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["osearch"] != null && Session["otext"] != null)
            {
                ddsearch.SelectedValue = Session["osearch"].ToString();
                txtpage.Text = Session["otext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        string qery = txtSearch.Text.ToString();
                        qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


                        string userid = Session["UserID"].ToString();
                        org_master = new List<Org_Master>();
                        org_master = BL_org_Master.SearchOrg("org_master", ddsearch.SelectedValue, txtpage.Text);
                        OrgListView.DataSource = org_master;
                        OrgListView.DataBind();
                    }
                }
            }
            else
            {


                string userid = Session["UserID"].ToString();
                org_master = new List<Org_Master>();
                org_master = BL_org_Master.GetListValues(userid);
                OrgListView.DataSource = org_master;
                OrgListView.DataBind();
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string org_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblorg_id") as Label).Text;
            Session["UserID"] = Session["UserID"].ToString();
            Session["org_id"] = org_id;
            Session["rType"] = 2;
            Response.Redirect("OrgFroms");
        }

        protected void AddRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Session["rType"] =0;
            Response.Redirect("OrgFroms");
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

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtSearch.Text == "")
                {
                    string userid = Session["UserID"].ToString();
                    Session["UserID"] = userid;
                     org_master = new List<Org_Master>();
                    org_master = BL_org_Master.GetListValues(userid);
                    OrgListView.DataSource = org_master;
                    OrgListView.DataBind();
                    // (grdAccessTypeList.DataSource as DataTable).DefaultView.RowFilter = string.Format("access_type_name LIKE '{1}%' or access_type_desc LIKE '{2}%'  ", txtSearch.Text, txtSearch.Text);
                }
            }

        }


        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = OrgListView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                string userid = Session["UserID"].ToString();
                org_master = new List<Org_Master>();
                org_master = BL_org_Master.GetListValues(userid);
                OrgListView.DataSource = org_master;
                OrgListView.DataBind();
            }
            return OrgListView.ToString();
        }


        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GridViewRow row = OrgListView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["osearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["otext"] = txtpage.Text;
                    string qery = txtSearch.Text.ToString();
                qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
              
                string userid = Session["UserID"].ToString();
                org_master = new List<Org_Master>();
                org_master = BL_org_Master.SearchOrg("org_master",ddsearch.SelectedValue,txtpage.Text);
                OrgListView.DataSource = org_master;
                OrgListView.DataBind();

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
            GridViewRow row = OrgListView.TopPagerRow;
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
                OrgListView.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                OrgListView.PageIndex = a - 1;
            }
            

            string userid = Session["UserID"].ToString();
            org_master = new List<Org_Master>();
            org_master = BL_org_Master.GetListValues(userid);
            OrgListView.DataSource = org_master;
            OrgListView.DataBind();


        }

        protected void OrgListView_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = OrgListView.TopPagerRow;
            if (OrgListView.Rows.Count > 0)
            {
                OrgListView.TopPagerRow.Visible = true;
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
            if (Session["osearch"] != null && Session["otext"] != null)
            {
                ddsearch.SelectedValue = Session["osearch"].ToString();
                txtpages.Text = Session["otext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = OrgListView.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = OrgListView.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < OrgListView.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == OrgListView.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (OrgListView.PageIndex == 0)
            {
                ((ImageButton)OrgListView.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)OrgListView.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)OrgListView.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)OrgListView.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (OrgListView.PageIndex + 1 == OrgListView.PageCount)
            {
                ((ImageButton)OrgListView.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)OrgListView.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)OrgListView.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)OrgListView.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["osearch"] = null;
            Session["otext"] = null;
            string userid = Session["UserID"].ToString();
            org_master = new List<Org_Master>();
            org_master = BL_org_Master.GetListValues(userid);
            OrgListView.DataSource = org_master;
            OrgListView.DataBind();
        }
    }
}