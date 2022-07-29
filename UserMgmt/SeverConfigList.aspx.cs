using System;
using System.Collections.Generic;
using Usermngt.BL;
using Usermngt.Entities;
using System.Web.UI.WebControls;

namespace UserMgmt
{
    public partial class SeverConfigList : System.Web.UI.Page
    {
        List<Server_Configs> server = new List<Server_Configs>();
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
                string userid = Session["UserID"].ToString();
                server = new List<Server_Configs>();
                server = BL_SeverConfig.GetServerList(userid);
                ServerConfigurationList.DataSource = server;
                ServerConfigurationList.DataBind();
            }
        }

        protected void AddRecords_Click(object sender, EventArgs e)
        {
            Session["rType"] = 0;
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ServerConfigs.aspx");
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string server_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblCode") as Label).Text;
            string server_user = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblName") as Label).Text;
            string server_domain = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDomain") as Label).Text;
            string server_url = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblURL") as Label).Text;
            string server_password = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblPassword") as Label).Text;



            Session["UserID"] = Session["UserID"].ToString();
            Session["server_code"] = server_code;
            Session["server_user"] = server_user;
            Session["server_domain"] = server_domain;
            Session["server_url"] = server_url;
             Session["server_password"] = server_password;

            Session["rType"] = 1;
            Response.Redirect("ServerConfigs.aspx");
        }

        //protected void btnEdite_Click(object sender, EventArgs e)
        //{
        //    LinkButton btn = (LinkButton)sender;
        //    GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        //    string server_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblCode") as Label).Text;
        //    string server_user = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblName") as Label).Text;
        //    string server_domain = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDomain") as Label).Text;
        //    string server_url = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblURL") as Label).Text;
        //    string server_password = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblPassword") as Label).Text;



        //    Session["UserID"] = Session["UserID"].ToString();
        //    Session["server_code"] = server_code;
        //    Session["server_user"] = server_user;
        //    Session["server_domain"] = server_domain;
        //    Session["server_url"] = server_url;
        //    Session["server_password"] = server_password;

        //    Session["rType"] = 2;
        //    Response.Redirect("ServerConfigs.aspx");
        //}

       
    }


}