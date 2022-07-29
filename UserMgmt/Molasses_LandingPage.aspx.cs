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
    public partial class Molasses_LandingPage1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
            UserDetails user = new UserDetails();
            user = BL_UserDetails.GetUserDetails(Session["UserID"].ToString());
            if(user.party_type== "Distillery")
            {
                Session["UserID"] = Session["UserID"];

                Response.Redirect("MF1_List");

            }
            if(user.party_type == "Sugar Mill")
            {
                Session["UserID"] = Session["UserID"];
                Response.Redirect("MF2_List");
            }
           
        }

        protected void btnMF1_Click(object sender, EventArgs e)
        {

        }

        protected void btnAllocation_Click(object sender, EventArgs e)
        {

        }
       
        protected void btnMolassesAllocation_Click(object sender, EventArgs e)
        {

        }

        protected void btnMF2_Click(object sender, EventArgs e)
        {

        }

        protected void btnMF3_Click(object sender, EventArgs e)
        {

        }
    }
}