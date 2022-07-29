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
    public partial class UserPermissionsList : System.Web.UI.Page
    {
        List<UserDetails> users = new List<UserDetails>();
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
                grdRolePermissionList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());

                string userid = Session["UserID"].ToString();
                users = new List<UserDetails>();
                users = BL_UserDetails.GetUsers(userid);
                grdRolePermissionList.DataSource = users;
                grdRolePermissionList.DataBind();
            }
        }

        protected void RolePermission_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("RolePermissionList");
        }

        protected void UserPermission_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("UserPermissionsList");
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Session["rtype"] = "0";
            Response.Redirect("UserPermissionsForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lbluser_id") as Label).Text;
            string rolename_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrolename_code") as Label).Text;
            string org_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblorg_id") as Label).Text;
            string registrationid = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblregistrationid") as Label).Text;
            Session["User"] = id;
            Session["UserID"] = Session["UserID"].ToString();
            Session["org_id"] = org_id;
            Session["role_name_code"] = rolename_code;
            Session["registration_id"] = registrationid;
            Session["rtype"] = "2";
            Response.Redirect("UserPermissionsForm");
        }

        protected void grdRolePermissionList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0)
            {
                grdRolePermissionList.PageIndex = 0;
            }
            else
            {
                grdRolePermissionList.PageIndex = e.NewPageIndex;
            }

            GridViewRow row = grdRolePermissionList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["upsearch"] != null && Session["uptext"] != null)
            {
                ddsearch.SelectedValue = Session["upsearch"].ToString();
                txtpage.Text = Session["uptext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        string qery = txtSearch.Text.ToString();
                        qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


                        users = new List<UserDetails>();
                        users = BL_UserDetails.SearchUserpermission("user_registration", ddsearch.SelectedValue, txtpage.Text);
                        grdRolePermissionList.DataSource = users;
                        grdRolePermissionList.DataBind();
                    }
                }
            }
            else
            {


                users = new List<UserDetails>();
                users = BL_UserDetails.GetUsers(Session["UserID"].ToString());
                grdRolePermissionList.DataSource = users;
                grdRolePermissionList.DataBind();
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtSearch.Text == "")
                {
                    string userid = Session["UserID"].ToString();
                    users = new List<UserDetails>();
                    users = BL_UserDetails.GetUsers(userid);
                    grdRolePermissionList.DataSource = users;
                    grdRolePermissionList.DataBind();
                    // (grdAccessTypeList.DataSource as DataTable).DefaultView.RowFilter = string.Format("access_type_name LIKE '{1}%' or access_type_desc LIKE '{2}%'  ", txtSearch.Text, txtSearch.Text);
                }
            }

        }
        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdRolePermissionList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                string userid = Session["UserID"].ToString();
                users = new List<UserDetails>();
                users = BL_UserDetails.GetUsers(userid);
                grdRolePermissionList.DataSource = users;
                grdRolePermissionList.DataBind();
            }
            return users.ToString();
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GridViewRow row = grdRolePermissionList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["upsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["uptext"] = txtpage.Text;
                    string qery = txtSearch.Text.ToString();
                qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                string userid = Session["UserID"].ToString();
                users = new List<UserDetails>();
                users = BL_UserDetails.SearchUserpermission("user_registration",ddsearch.SelectedValue,txtpage.Text);
                grdRolePermissionList.DataSource = users;
                grdRolePermissionList.DataBind();

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
            GridViewRow row = grdRolePermissionList.TopPagerRow;
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
                grdRolePermissionList.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdRolePermissionList.PageIndex = a - 1;
            }
            

            string userid = Session["UserID"].ToString();
            users = new List<UserDetails>();
            users = BL_UserDetails.GetUsers(userid);
            grdRolePermissionList.DataSource = users;
            grdRolePermissionList.DataBind();


        }

        protected void grdRolePermissionList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdRolePermissionList.TopPagerRow;
            if (grdRolePermissionList.Rows.Count > 0)
            {
                grdRolePermissionList.TopPagerRow.Visible = true;
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
            if (Session["upsearch"] != null && Session["uptext"] != null)
            {
                ddsearch.SelectedValue = Session["upsearch"].ToString();
                txtpages.Text = Session["uptext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdRolePermissionList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdRolePermissionList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdRolePermissionList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdRolePermissionList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdRolePermissionList.PageIndex == 0)
            {
                ((ImageButton)grdRolePermissionList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdRolePermissionList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdRolePermissionList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdRolePermissionList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdRolePermissionList.PageIndex + 1 == grdRolePermissionList.PageCount)
            {
                ((ImageButton)grdRolePermissionList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdRolePermissionList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdRolePermissionList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdRolePermissionList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["upsearch"] = null;
            Session["uptext"] = null;
            string userid = Session["UserID"].ToString();
            users = new List<UserDetails>();
            users = BL_UserDetails.GetUsers(userid);
            grdRolePermissionList.DataSource = users;
            grdRolePermissionList.DataBind();
        }
    }
}