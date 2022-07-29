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
    public partial class UserRegistrationList : System.Web.UI.Page
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
                grdUserList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                string userid = Session["UserID"].ToString();
                users = new List<UserDetails>();
                users = BL_UserDetails.GetUserList(userid);
                grdUserList.DataSource = users;
                grdUserList.DataBind();
            }
        }

        

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Session["rtype"] = "0";
            Response.Redirect("UserRegistrationForm");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text;
            Session["UserID"] = Session["UserID"].ToString();
            Session["id"] =id;
            Session["rtype"] = "1";
            Response.Redirect("UserRegistrationForm");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text;
            Session["ID"] =id;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "2";
            Response.Redirect("UserRegistrationForm");
        }

        protected void grdUserList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0)
            {
                grdUserList.PageIndex = 0;
            }
            else
            {
                grdUserList.PageIndex = e.NewPageIndex;
            }

            GridViewRow row = grdUserList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["usearch"] != null && Session["utext"] != null)
            {
                ddsearch.SelectedValue = Session["usearch"].ToString();
                txtpage.Text = Session["utext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        string qery = txtSearch.Text.ToString();
                        qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


                        users = new List<UserDetails>();
                        users = BL_UserDetails.SearchUserDetails("user_registration", ddsearch.SelectedValue, txtpage.Text);

                        grdUserList.DataSource = users;
                        grdUserList.DataBind();
                    }
                }
            }
            else
            {



                users = new List<UserDetails>();
                users = BL_UserDetails.GetUserList(Session["UserID"].ToString());
                grdUserList.DataSource = users;
                grdUserList.DataBind();
            }
        }
        protected void UserRegistration_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("UserRegistrationList");
        }
        protected void Employee_Details_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("EmployeeList");
        }

        protected void Designation_1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("DepartmentMasterList.aspx");
        }

        protected void Designation_2_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("DesignationList");
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtSearch.Text == "")
                {
                    string userid = Session["UserID"].ToString();
                    users = new List<UserDetails>();
                    users = BL_UserDetails.GetUserList(userid);
                    grdUserList.DataSource = users;
                    grdUserList.DataBind();
                    // (grdAccessTypeList.DataSource as DataTable).DefaultView.RowFilter = string.Format("access_type_name LIKE '{1}%' or access_type_desc LIKE '{2}%'  ", txtSearch.Text, txtSearch.Text);
                }
            }

        }
        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdUserList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                string userid = Session["UserID"].ToString();
                users = new List<UserDetails>();
                users = BL_UserDetails.GetUserList(userid);
                grdUserList.DataSource = users;
                grdUserList.DataBind();
            }
            return users.ToString();
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GridViewRow row = grdUserList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["usearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["utext"] = txtpage.Text;

                    string qery = txtSearch.Text.ToString();
                qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                string userid = Session["UserID"].ToString();
                users = new List<UserDetails>();

                users = BL_UserDetails.SearchUserDetails("user_registration", ddsearch.SelectedValue,txtpage.Text);
             
                grdUserList.DataSource = users;
                grdUserList.DataBind();

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
            GridViewRow row = grdUserList.TopPagerRow;
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
                grdUserList.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdUserList.PageIndex = a - 1;
            }

            

            string userid = Session["UserID"].ToString();
            users = new List<UserDetails>();
            users = BL_UserDetails.GetUserList(userid);
            grdUserList.DataSource = users;
            grdUserList.DataBind();


        }

        protected void grdUserList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdUserList.TopPagerRow;
            if (grdUserList.Rows.Count > 0)
            {
                grdUserList.TopPagerRow.Visible = true;
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
            if (Session["usearch"] != null && Session["utext"] != null)
            {
                ddsearch.SelectedValue = Session["usearch"].ToString();
                txtpages.Text = Session["utext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdUserList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdUserList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdUserList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdUserList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdUserList.PageIndex == 0)
            {
                ((ImageButton)grdUserList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdUserList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdUserList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdUserList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdUserList.PageIndex + 1 == grdUserList.PageCount)
            {
                ((ImageButton)grdUserList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdUserList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdUserList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdUserList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["usearch"] = null;
            Session["utext"] = null;
            string userid = Session["UserID"].ToString();
            users = new List<UserDetails>();
            users = BL_UserDetails.GetUserList(userid);
            grdUserList.DataSource = users;
            grdUserList.DataBind();
        }
    }
}