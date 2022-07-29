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
    public partial class HelpDeskList : System.Web.UI.Page
    {
        List<Helpdesk> helpDesk = new List<Helpdesk>();
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
                grdHelpdeskList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                Session["UserID"] = Session["UserID"];
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                string userid = Session["UserID"].ToString();
                Session["Role_name_code"] = user.role_name_code;
                helpDesk = new List<Helpdesk>();
                helpDesk =BL_HelpDesk.GetList(userid);
                //grdFermentertoReceiver.DataSource = form83;
                //grdFermentertoReceiver.DataBind();
                if (user.user_id == "Admin")
                {
                    var list = from s in helpDesk
                               where s.record_status == "P" || s.record_status == "R" || s.record_status=="C"|| s.record_status == "O" || s.record_status == "N"
                               select s;
                   grdHelpdeskList.DataSource = list.ToList();
                    grdHelpdeskList.DataBind();
                    //foreach (GridViewRow dr1 in grdFermentertoReceiver.Rows)
                    //{
                    //    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    //    btn.Visible = false;
                    //}
                    //btnAddRecord.Visible = false;

                }
                else if (user.role_name == "Developer")
                {
                    var list = from s in helpDesk
                               where s.developer == user.user_name && s.record_status == "O" || s.record_status=="R" || s.record_status == "T" || s.record_status == "N"
                               select s;
                   grdHelpdeskList.DataSource = list.ToList();
                   grdHelpdeskList.DataBind();
                    //foreach (GridViewRow dr1 in grdHelpdeskList.Rows)
                    //{
                    //    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    //    btn.Visible = true;
                    //}
                }

                else if (user.role_name == "Test Engineer")
                {
                    var list = from s in helpDesk
                               where s.tester== user.user_name && s.record_status=="C" || s.record_status == "R"|| s.record_status == "O"
                               select s;
                    grdHelpdeskList.DataSource = list.ToList();
                    grdHelpdeskList.DataBind();
                }

                else if (user.user_id != "Admin" && user.role_name != "Developer" && user.role_name != "Test Engineer")
                {
                    var list = from s in helpDesk
                               where s.user_id == user.user_id 
                               select s;
                    grdHelpdeskList.DataSource = list.ToList();
                    grdHelpdeskList.DataBind();
                    foreach (GridViewRow dr1 in grdHelpdeskList.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                }
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string ticketid = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblticketid") as Label).Text;
            Session["rtype"] = "1";
            Session["UserId"] = Session["UserID"].ToString();
            Session["ticketid"] = ticketid;
            Response.Redirect("HelpDeskForm1.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string ticketid = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblticketid") as Label).Text;
            Session["UserId"] = Session["UserID"].ToString();
            Session["rtype"] = "2";
            Session["ticketid"] = ticketid;
            Response.Redirect("HelpDeskForm1.aspx");
        }

        protected void grdHelpdeskList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if(IsPostBack)
            {
                grdHelpdeskList.PageIndex = e.NewPageIndex;
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                string userid = Session["UserID"].ToString();
                helpDesk = new List<Helpdesk>();
                helpDesk = BL_HelpDesk.GetList(userid);
                //grdFermentertoReceiver.DataSource = form83;
                //grdFermentertoReceiver.DataBind();
                if (user.user_id == "Admin")
                {
                    var list = from s in helpDesk
                               where s.record_status == "P" || s.record_status == "R" || s.record_status == "C" || s.record_status == "O"
                               select s;
                    grdHelpdeskList.DataSource = list.ToList();
                    grdHelpdeskList.DataBind();
                    //foreach (GridViewRow dr1 in grdFermentertoReceiver.Rows)
                    //{
                    //    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    //    btn.Visible = false;
                    //}
                    //btnAddRecord.Visible = false;

                }
                else if (user.role_name == "Developer")
                {
                    var list = from s in helpDesk
                               where s.developer_code == user.id && s.record_status == "O" || s.record_status == "R" || s.record_status == "T" || s.record_status == "N"
                               select s;
                    grdHelpdeskList.DataSource = list.ToList();
                    grdHelpdeskList.DataBind();
                    //foreach (GridViewRow dr1 in grdHelpdeskList.Rows)
                    //{
                    //    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    //    btn.Visible = true;
                    //}
                }

                else if (user.role_name == "Test Engineer")
                {
                    var list = from s in helpDesk
                               where s.user_id == user.user_id && s.record_status == "C" || s.record_status == "R"
                               select s;
                    grdHelpdeskList.DataSource = list.ToList();
                    grdHelpdeskList.DataBind();
                }
                else if (user.user_id != "Admin" && user.role_name != "Developer" && user.role_name != "Test Engineer")
                {
                    var list = from s in helpDesk
                               where s.user_id == user.user_id
                               select s;
                    grdHelpdeskList.DataSource = list.ToList();
                    grdHelpdeskList.DataBind();
                    foreach (GridViewRow dr1 in grdHelpdeskList.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                }

            }
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

        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdHelpdeskList.TopPagerRow;
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
                grdHelpdeskList.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdHelpdeskList.PageIndex = a - 1;
            }
            

            string userid = Session["UserID"].ToString();
            var list = from s in helpDesk
                       where s.record_status == "P" || s.record_status == "R" || s.record_status == "C" || s.record_status == "O" || s.record_status == "N"
                       select s;
            grdHelpdeskList.DataSource = list.ToList();
            grdHelpdeskList.DataBind();


        }

        protected void grdHelpdeskList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdHelpdeskList.TopPagerRow;
            if (grdHelpdeskList.Rows.Count > 0)
            {
                grdHelpdeskList.TopPagerRow.Visible = true;
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
            lblPages.Text = grdHelpdeskList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdHelpdeskList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdHelpdeskList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdHelpdeskList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdHelpdeskList.PageIndex == 0)
            {
                ((ImageButton)grdHelpdeskList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdHelpdeskList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdHelpdeskList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdHelpdeskList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdHelpdeskList.PageIndex + 1 == grdHelpdeskList.PageCount)
            {
                ((ImageButton)grdHelpdeskList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdHelpdeskList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdHelpdeskList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdHelpdeskList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }
    }
}