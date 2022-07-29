using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class CustomerMasterList : System.Web.UI.Page
    {
        List<CustomerDetails> customers = new List<CustomerDetails>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                    Session["UserID"] = Session["UserID"];
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    Session["party"] = user.party_code;
                    if ((user.role_name != "All" || user.party_type != "ALL") && user.role_name == "Applicant")
                    {
                        grdCustomerMaster.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                        //List<CustomerDetails> customers = new List<CustomerDetails>();
                        customers = BL_User_Mgnt.GetCustomers(user.party_code);
                        var list2 = from s in customers
                                    where s.party_code == user.party_code
                                    select s;
                        grdCustomerMaster.DataSource = list2.ToList();
                        grdCustomerMaster.DataBind();
                    }
                    else
                    {
                        Response.Redirect("~/User_Mgmt");
                    }
                }
                else
                {
                    Response.Redirect("~/User_Mgmt");
                }
            }
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = 0;
            Response.Redirect("CustomerMasterForm");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            int rowindex = gvr.RowIndex;
            string Customerid = (grdCustomerMaster.Rows[rowindex].FindControl("lblCustomerid") as Label).Text;
            Session["UserID"] = Session["UserID"].ToString();
            Session["Customerid"] = Customerid;
            Session["rtype"] = 1;
            Response.Redirect("CustomerMasterForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            int rowindex = gvr.RowIndex;
            string Customerid = (grdCustomerMaster.Rows[rowindex].FindControl("lblCustomerid") as Label).Text;
            Session["UserID"] = Session["UserID"].ToString();
            Session["Customerid"] = Customerid;
            Session["rtype"] = 2;
            Response.Redirect("CustomerMasterForm");
        }

        protected void grdCustomerMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0)
            {
                grdCustomerMaster.PageIndex = 0;
            }
            else
            {
                grdCustomerMaster.PageIndex = e.NewPageIndex;
            }
            GridViewRow row = grdCustomerMaster.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            List<CustomerDetails> customers = new List<CustomerDetails>();
            string userid = Session["UserID"].ToString();
            Session["UserID"] = userid;
            if (Session["cmdsearch"] != null && Session["cmdtext"] != null)
            {
                ddsearch.SelectedValue = Session["cmdsearch"].ToString();
                txtpage.Text = Session["cmdtext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                       
                        customers = BL_User_Mgnt.Search("", ddsearch.SelectedValue, txtpage.Text);
                        var list2 = from s in customers
                                    where s.party_code == user.party_code
                                    select s;
                        grdCustomerMaster.DataSource = list2.ToList();
                        grdCustomerMaster.DataBind();
                    }
                }
            }
            else
            {
                customers = BL_User_Mgnt.GetCustomers(user.party_code);
                var list2 = from s in customers
                            where s.party_code == user.party_code
                            select s;
                grdCustomerMaster.DataSource = list2.ToList();
                grdCustomerMaster.DataBind();
            }
        }
        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdCustomerMaster.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            if (Convert.ToInt32(txtpage.Text) <= 0)
            {
                a = 0;
            }
            else
            {
                a = Convert.ToInt32(txtpage.Text);
            }
            if (a != 0)
            {
                grdCustomerMaster.PageIndex = a - 1;
            }
            else
            {
                grdCustomerMaster.PageIndex = a;
            }


            string userid = Session["UserID"].ToString();
            List<CustomerDetails> customers = new List<CustomerDetails>();
            customers = BL_User_Mgnt.GetCustomers(Session["party"].ToString());
            var list2 = from s in customers
                        where s.party_code == Session["party"].ToString()
                        select s;
            grdCustomerMaster.DataSource = list2.ToList();
            grdCustomerMaster.DataBind();


        }

        protected void grdCustomerMaster_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdCustomerMaster.TopPagerRow;
            if (grdCustomerMaster.PageCount != 0)
            {
                grdCustomerMaster.TopPagerRow.Visible = true;
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

            if (Session["cmdsearch"] != null && Session["cmdtext"] != null)
            {
                ddsearch.SelectedValue = Session["cmdsearch"].ToString();
                txtpages.Text = Session["cmdtext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdCustomerMaster.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdCustomerMaster.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdCustomerMaster.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdCustomerMaster.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdCustomerMaster.PageIndex == 0)
            {
                ((ImageButton)grdCustomerMaster.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdCustomerMaster.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdCustomerMaster.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdCustomerMaster.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdCustomerMaster.PageIndex + 1 == grdCustomerMaster.PageCount)
            {
                ((ImageButton)grdCustomerMaster.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdCustomerMaster.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdCustomerMaster.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdCustomerMaster.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }


        protected void btnsearch_Click(object sender, EventArgs e)
        {


            GridViewRow row = grdCustomerMaster.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["cmdsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["cmdtext"] = txtpage.Text;
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        List<CustomerDetails> customers = new List<CustomerDetails>();
                        customers = BL_User_Mgnt.Search("",ddsearch.SelectedValue,txtpage.Text);
                        var list2 = from s in customers
                                    where s.party_code == user.party_code
                                    select s;
                        grdCustomerMaster.DataSource = list2.ToList();
                        grdCustomerMaster.DataBind();
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Select Field Name\');", true);
                ddsearch.Focus();
            }



            }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["cmdsearch"] = null;
            Session["cmdtext"] = null;
            customers = BL_User_Mgnt.GetCustomers(Session["party"].ToString());
            var list2 = from s in customers
                        where s.party_code == Session["party"].ToString()
                        select s;
            grdCustomerMaster.DataSource = list2.ToList();
            grdCustomerMaster.DataBind();
        }
    }
}