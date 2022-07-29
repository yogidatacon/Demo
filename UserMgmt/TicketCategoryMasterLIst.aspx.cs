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
    public partial class TicketCategoryMasterLIst : System.Web.UI.Page
    {
        List<Ticketcategory> ticket = new List<Ticketcategory>();
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
                grdTicketcategoryMasterList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                string userid = Session["UserID"].ToString();
                ticket = new List<Ticketcategory>();
                ticket = BL_Ticketcategory.GetList();
                grdTicketcategoryMasterList.DataSource = ticket;
                grdTicketcategoryMasterList.DataBind();
            }
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserId"] = Session["UserID"].ToString();
            Session["rtype"] = "0";
            Response.Redirect("TicketcategoryMaster.aspx");
        }


        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblCode") as Label).Text;
            string Name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblName") as Label).Text;
            string id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblId") as Label).Text;
            Session["UserId"] = Session["UserID"].ToString();
            Session["rtype"] = "1";
            Session["ID"] = id;
            Session["Name"] = Name;
            Session["Code"] = code;
            Response.Redirect("TicketcategoryMaster.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblCode") as Label).Text;
            string Name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblName") as Label).Text;
            string id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblId") as Label).Text;
            Session["UserId"] = Session["UserID"].ToString();
            Session["rtype"] = "2";
            Session["ID"] = id;
            Session["Name"] = Name;
            Session["Code"] = code;
            Response.Redirect("TicketcategoryMaster.aspx");
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {

            //if (txtSearch.Text == "")
            //{
            //    accesstypelist = new List<AccessType>();
            //    accesstypelist = BL_User_Mgnt.GetAccessTypeList(Session["UserID"].ToString());
            //    grdAccessTypeList.DataSource = accesstypelist;
            //    grdAccessTypeList.DataBind();
            //    // (grdAccessTypeList.DataSource as DataTable).DefaultView.RowFilter = string.Format("access_type_name LIKE '{1}%' or access_type_desc LIKE '{2}%'  ", txtSearch.Text, txtSearch.Text);
            //}


        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            //if (txtSearch.Text != "")
            //{

            //    string qery = txtSearch.Text.ToString();
            //    qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


            //    accesstypelist = new List<AccessType>();
            //    accesstypelist = BL_User_Mgnt.SearchExistsData("access_type_master", "access_type_name", qery);
            //    grdAccessTypeList.DataSource = accesstypelist;
            //    grdAccessTypeList.DataBind();
            //}


        }

        protected void Priority_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PriorityList.aspx");
        }

        protected void Helpdesk_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("HelpDeskList.aspx");
        }

        protected void Ticket_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("TicketStatusLIst.aspx");
        }

        protected void grdTicketcategoryMasterList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdTicketcategoryMasterList.PageIndex = e.NewPageIndex;
            ticket = new List<Ticketcategory>();
            ticket = BL_Ticketcategory.GetList();
            grdTicketcategoryMasterList.DataSource = ticket;
            grdTicketcategoryMasterList.DataBind();
        }

        protected void Ticketcategory_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("TicketcategoryList.aspx");
        }

        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdTicketcategoryMasterList.TopPagerRow;
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
                grdTicketcategoryMasterList.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdTicketcategoryMasterList.PageIndex = a - 1;
            }

            

            string userid = Session["UserID"].ToString();
            ticket = new List<Ticketcategory>();
            ticket = BL_Ticketcategory.GetList();
            grdTicketcategoryMasterList.DataSource = ticket;
            grdTicketcategoryMasterList.DataBind();


        }

        protected void grdTicketcategoryMasterList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdTicketcategoryMasterList.TopPagerRow;
            if (grdTicketcategoryMasterList.Rows.Count > 0)
            {
                grdTicketcategoryMasterList.TopPagerRow.Visible = true;
            }
            if (row == null)
            {
                return;
            }

            DropDownList DDLPage = (DropDownList)row.Cells[0].FindControl("DDLPage");
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            Label lblPages = (Label)row.Cells[0].FindControl("lblPages");
            Label lblCurrent = (Label)row.Cells[0].FindControl("lblCurrent");

            //if (lblPages != null)
            //{
            lblPages.Text = grdTicketcategoryMasterList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdTicketcategoryMasterList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdTicketcategoryMasterList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdTicketcategoryMasterList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdTicketcategoryMasterList.PageIndex == 0)
            {
                ((ImageButton)grdTicketcategoryMasterList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdTicketcategoryMasterList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdTicketcategoryMasterList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdTicketcategoryMasterList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdTicketcategoryMasterList.PageIndex + 1 == grdTicketcategoryMasterList.PageCount)
            {
                ((ImageButton)grdTicketcategoryMasterList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdTicketcategoryMasterList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdTicketcategoryMasterList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdTicketcategoryMasterList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }
    }
}