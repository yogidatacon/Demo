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
    public partial class CaseHistoryList : System.Web.UI.Page
    {
        List<cm_seiz_CaseHistory> obj = new List<cm_seiz_CaseHistory>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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
                Session["UserID"] = Session["UserID"];

                string seizureNo = Session["seizureNo"].ToString();
                obj = BL_cm_seiz_CaseHistory.GetList(seizureNo);
                grdCaseHistoryView.DataSource = obj.ToArray();
                grdCaseHistoryView.DataBind();
                Session["sType"] = Session["sType"];
                if (Session["sType"].ToString() == "1")
                    btnAddRecord.Visible = false;
            }

        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            // Session["seizureno"] = "1";
            Session["UserID"] = Session["UserID"].ToString();
            Session["tableId"] = string.Empty;
            Session["rtype"] = 0;
            Response.Redirect("CaseHistoryForm.aspx");
        }
       
        protected void btnCaseHistory_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("CaseHistoryList.aspx");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            string ID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("seizureno") as Label).Text;
            string tableId = (gvr.Cells[gvr.Cells.Count - 1].FindControl("TableId") as Label).Text;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "1";
            Session["seizureno"] = ID;
            Session["tableId"] = tableId;
            Response.Redirect("CaseHistoryForm.aspx");

            //LinkButton btn = (LinkButton)sender;
            //GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            //string seizure_accusedcasehistory_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text;
            //string case_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblCaseID") as Label).Text;
            //string seizure_accused_details_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblseizure_accused_details_id") as Label).Text;
            //string ipaddress = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblipaddress") as Label).Text;
            //string case_details = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblCaseDetails") as Label).Text;
            //string record_status = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrecord_status") as Label).Text;
            //Session["seizure_accusedcasehistory_id"] = seizure_accusedcasehistory_id;
            //Session["seizure_accused_details_id"] = seizure_accused_details_id;
            //Session["case_id"] = case_id;
            //Session["ipaddress"] = ipaddress;
            //Session["case_details"] = case_details;
            //Session["record_status"] = record_status;
            //Session["seizureno"] = "1";
            //Session["rtype"] = 1;
            //Response.Redirect("CaseHistoryForm.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string ID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("seizureno") as Label).Text;
            string tableId = (gvr.Cells[gvr.Cells.Count - 1].FindControl("TableId") as Label).Text;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "2";
            Session["seizureno"] = ID;
            Session["tableId"] = tableId;
            Response.Redirect("CaseHistoryForm.aspx");

            //LinkButton btn = (LinkButton)sender;
            //GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            //string seizure_accusedcasehistory_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text;
            //string case_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblCaseID") as Label).Text;
            //string seizure_accused_details_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblseizure_accused_details_id") as Label).Text;
            //string ipaddress = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblipaddress") as Label).Text;
            //string case_details = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblCaseDetails") as Label).Text;
            //string record_status = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrecord_status") as Label).Text;
            //Session["seizure_accusedcasehistory_id"] = seizure_accusedcasehistory_id;
            //Session["seizure_accused_details_id"] = seizure_accused_details_id;
            //Session["case_id"] = case_id;
            //Session["ipaddress"] = ipaddress;
            //Session["case_details"] = case_details;
            //Session["record_status"] = record_status;
            //Session["seizureno"] = "1";
            //Session["rtype"] = 2;
            //Response.Redirect("CaseHistoryForm.aspx");
        }

        protected void grdCaseHistoryView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCaseHistoryView.PageIndex = e.NewPageIndex;
            obj = new List<cm_seiz_CaseHistory>();

            string seizureNo = Session["seizureNo"].ToString();
            obj = BL_cm_seiz_CaseHistory.GetList(seizureNo);
            grdCaseHistoryView.DataSource = obj.ToArray();
            grdCaseHistoryView.DataBind();
        }
    }
}