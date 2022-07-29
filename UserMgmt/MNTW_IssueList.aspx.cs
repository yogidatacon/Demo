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
    public partial class MNTW_IssueList : System.Web.UI.Page
    {
        List<Molasses_Allocation> ic = new List<Molasses_Allocation>();
        string user_role = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Session["userid"] = Session["UserID"].ToString();
                //Session["userid"] = "mtp_who";


                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                }
                //if (strPreviousPage == "")
                //{
                //    Response.Redirect("~/LoginPage");
                //}
                string userid = Session["UserID"].ToString();
                UserDetails user = new UserDetails();
                user = BL_UserDetails.GetUser(userid);
                user_role = user.role_name.ToString();
                Session["Username"] = user.user_name.ToString();
                if (user.role_name == "Bond Officer" || user.role_name == "Admin")
                {
                    btnAddRecord.Visible = false;
                }

                else
                {
                    btnAddRecord.Visible = true;
                }

                ic = new List<Molasses_Allocation>();
                ic = BL_Molasses_Allocation.GetMNTIssueList();

                //grdConsumptionList.DataSource = ic.ToList();
                //grdConsumptionList.DataBind();
                //dummytable.Visible = false;

                if (user.role_name == "Bond Officer")
                {
                    var list = from s in ic
                               where s.party_code == user.party_code && s.record_status != "N" && s.financial_year == user.financial_year
                               // orderby s.rmr_entrydate descending
                               select s;
                    grdConsumptionList.DataSource = list.ToList();
                    grdConsumptionList.DataBind();


                }

                else if (user.role_name == "Applicant")
                {
                    var list = from s in ic
                               where s.party_code == user.party_code && s.financial_year == user.financial_year
                               // orderby s.rmr_entrydate descending
                               select s;
                    grdConsumptionList.DataSource = list.ToList();
                    grdConsumptionList.DataBind();


                }
            }
        }

        protected void btnIssue_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("MNT_IssueList.aspx");
        }

        protected void btnConsumption_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("MNT_ConsumptionList.aspx");
        }

        protected void lnkRMR_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("RawMaterialReceiptList.aspx");
        }

        protected void lnkOB_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("OpeningBalanceList.aspx");
        }

        protected void btnAddRecord_Click(object sender, EventArgs e)
        {
            Session["rtype"] = 0;
            Session["UserID"] = Session["UserID"];
            Response.Redirect("MNTW_Issue.aspx");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string mirid = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblconsumption_no") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["istwfinancial_year"] = financial_year;
            Session["UserID"] = Session["UserID"].ToString();
            Session["mirid"] = mirid;
            Session["rtype"] = 1;
            Response.Redirect("MNTW_Issue.aspx");
        }


        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string mirid = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblconsumption_no") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["istwfinancial_year"] = financial_year;
            Session["UserID"] = Session["UserID"].ToString();
            Session["mirid"] = mirid;
            Session["rtype"] = 2;
            Response.Redirect("MNTW_Issue.aspx");
        }

        protected void grdConsumptionList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblstatus = (Label)e.Row.Cells[4].FindControl("lblstatus");


                if (lblstatus.Text == "Draft" && user_role == "Applicant")
                {
                    LinkButton btnEdit = (LinkButton)e.Row.Cells[5].FindControl("btnEdit");
                    btnEdit.Visible = true;
                }

                if (lblstatus.Text == "Pending" && user_role == "Applicant")
                {
                    LinkButton btnEdit = (LinkButton)e.Row.Cells[5].FindControl("btnEdit");
                    btnEdit.Visible = false;
                }
                if (lblstatus.Text == "Pending" && user_role == "Bond Officer")
                {
                    LinkButton btnEdit = (LinkButton)e.Row.Cells[5].FindControl("btnEdit");
                    btnEdit.Visible = true;
                }

                if (lblstatus.Text == "Approved" && user_role == "Applicant")
                {
                    LinkButton btnEdit = (LinkButton)e.Row.Cells[5].FindControl("btnEdit");
                    btnEdit.Visible = false;
                }

                if (lblstatus.Text == "Approved" && user_role == "Bond Officer")
                {
                    LinkButton btnEdit = (LinkButton)e.Row.Cells[5].FindControl("btnEdit");
                    btnEdit.Visible = false;
                }

                if (lblstatus.Text == "Rejected" && user_role == "Applicant")
                {
                    LinkButton btnEdit = (LinkButton)e.Row.Cells[5].FindControl("btnEdit");
                    btnEdit.Visible = false;
                }
                if (lblstatus.Text == "Rejected" && user_role == "Bond Officer")
                {
                    LinkButton btnEdit = (LinkButton)e.Row.Cells[5].FindControl("btnEdit");
                    btnEdit.Visible = false;
                }
            }
        }

        protected void grdConsumptionList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (IsPostBack)
            {
                Session["userid"] = Session["UserID"].ToString();
                //Session["userid"] = "mtp1_bo";
                grdConsumptionList.PageIndex = e.NewPageIndex;
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                }
                //if (strPreviousPage == "")
                //{
                //    Response.Redirect("~/LoginPage");
                //}
                string userid = Session["UserID"].ToString();
                UserDetails user = new UserDetails();
                user = BL_UserDetails.GetUser(userid);
                user_role = user.role_name.ToString();
                Session["Username"] = user.user_name.ToString();
                if (user.role_name == "Bond Officer" || user.role_name == "Admin")
                {
                    btnAddRecord.Visible = false;
                }

                else
                {
                    btnAddRecord.Visible = true;
                }

                ic = new List<Molasses_Allocation>();
                ic = BL_Molasses_Allocation.GetMNTIssueList();

                //grdConsumptionList.DataSource = ic.ToList();
                //grdConsumptionList.DataBind();
                //dummytable.Visible = false;

                if (user.role_name == "Bond Officer")
                {
                    var list = from s in ic
                               where s.party_code == user.party_code && s.record_status != "N"
                               // orderby s.rmr_entrydate descending
                               select s;
                    grdConsumptionList.DataSource = list.ToList();
                    grdConsumptionList.DataBind();


                }

                else if (user.role_name == "Applicant")
                {

                    grdConsumptionList.DataSource = ic.ToList();
                    grdConsumptionList.DataBind();


                }
            }
        }
    }
}