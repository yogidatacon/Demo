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
    public partial class LabTechMaster : System.Web.UI.MasterPage
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
                lblUser.Text = Session["Username"].ToString();
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(userid);
                if (userid != "Admin")
                {

                    usermgmtMenu.Visible = false;
                    digiLockerMenu.Visible = false;
                    reportconfig_menu.Visible = false;
                    scm_menu.Visible = false;
                    if (user.party_type == "Sugar Mill")
                    {
                        scm_menu.Visible = true;
                        A1Sugar.Visible = true;
                        A1Distilleries.Visible = false;
                        A3.Visible = false;
                    }
                    if (user.party_type == "Distillery")
                    {
                        scm_menu.Visible = true;
                        A1Distilleries.Visible = true;
                        A1Sugar.Visible = false;
                        A3.Visible = false;
                    }
                    if (user.role_name == "Bond Officer" && user.party_type == "Sugar Mill")
                    {
                        scm_menu.Visible = true;
                        scm_menu.Visible = true;
                        A1Sugar.Visible = true;
                        A1Distilleries.Visible = false;
                        A3.Visible = false;
                    }
                    if (user.role_name == "Bond Officer" && user.party_type == "Distillery")
                    {
                        scm_menu.Visible = true;
                        A1Distilleries.Visible = true;
                        A1Sugar.Visible = false;
                        A3.Visible = false;
                    }
                }
            }
        }



        protected void UserManagementMasters_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("StateList");
        }

        protected void Organisation_Click(object sender, EventArgs e)
        {
            //Organisation.BackColor = System.Drawing.Color.Yellow;
            //Session["UserID"] = Session["UserID"];
            //Response.Redirect("OrgList");
        }
        protected void LogOut()
        {

        }

        protected void RolePermission_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RolePermissionList");
            //  Response.Redirect("WebForm1.aspx");
        }

        protected void UserRegistration_Click(object sender, EventArgs e)
        {
            // li_UserRegistration.Attributes["class"] = "Active";
            Session["UserID"] = Session["UserID"];
            Response.Redirect("UserRegistrationList");
        }

        protected void WorkFlow_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("WorkFlowLIst");
        }

        protected void UserReport_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("UserReports");
        }


        protected void btnMasterReports_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("MasterReportsList");
        }

        protected void DigiLocker_Click(object sender, EventArgs e)
        {

        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(-30); //Delete the cookie
            Session.Abandon();
            Response.Redirect("~/LoginPage.aspx");
        }
    }
}