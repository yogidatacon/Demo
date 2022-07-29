using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class HelpDeskForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count == 0)
            {
                Response.Redirect("~/LoginPage");
            }
            else
            {
                string userid = Session["UserID"].ToString();
                // string userid = "Admin";
                Session["UserID"] = userid;
               // labUsername.Text = Session["Username"].ToString();
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(userid);
                //txtContactNumber.Text = user.mobile;
               // txtEmail.Text = user.email_id;
                if (Session["rtype"].ToString() != "0")
                {
                    int id = Convert.ToInt32(Session["ticketid"].ToString());
                    Helpdesk help = new Helpdesk();
                    help = BL_HelpDesk.GetDetails(id);
                   


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

    }
}

