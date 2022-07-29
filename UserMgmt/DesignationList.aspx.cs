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
    public partial class DesignationList : System.Web.UI.Page
    {
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
                Session["UserID"] = userid;
                List<Designation_Details> des = new List<Designation_Details>();
                des = BL_Designation_Details.GetDList();
                grddesignationlist.DataSource = des;
                grddesignationlist.DataBind();

            }
        }

        protected void btnAddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Session["rtype"] = 0;
            Response.Redirect("DesignationForm");
        }

        protected void grddesignationlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string div_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDesignationCode") as Label).Text;
            string div_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDesignationName") as Label).Text;
            string dtype = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDesignationTypecode") as Label).Text;
            Session["UserID"] = Session["UserID"].ToString();
            Session["rType"] = 2;
            Session["DCode"] = div_id;
            Session["DName"] = div_name;
            Session["Dtype"] = dtype;
            Response.Redirect("DesignationForm");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string div_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDesignationCode") as Label).Text;
            string div_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDesignationName") as Label).Text;
            string dtype = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDesignationTypecode") as Label).Text;
            Session["UserID"] = Session["UserID"].ToString();
            Session["rType"] = 1;
            Session["DCode"] = div_id;
            Session["DName"] = div_name;
            Session["Dtype"] = dtype;
            Response.Redirect("DesignationForm");
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
            Response.Redirect("DesignationTypeList");
        }

        protected void Designation_2_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("DesignationList");
        }
    }
}