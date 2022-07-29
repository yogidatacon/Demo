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
    public partial class FIRList : System.Web.UI.Page
    {
        List<cm_seiz_FIR> obj = new List<cm_seiz_FIR>();
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
               
                Session["UserID"] = Session["UserID"];
                string raidby = "";
                if (Session["RaidBy"].ToString().Substring(0, 1).ToUpper() == "E")
                    raidby = "E";
                else
                    raidby = "P";
                string seizureNo = Session["seizureNo"]?.ToString() ?? string.Empty;
                obj = BL_cm_seiz_FIR.GetList(seizureNo);
                var fir = (from s in obj
                          where s.raidby == raidby
                          select s);
                grdFIRListView.DataSource = fir.ToArray();
                grdFIRListView.DataBind();
                for (int i = 0; i < grdFIRListView.Rows.Count; i++)
                {
                    string user1 = (grdFIRListView.Rows[i].FindControl("lbluserid") as Label).Text;
                    if (Session["UserID"].ToString() != user1)
                        (grdFIRListView.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                }
                if (grdFIRListView.Rows.Count > 0)
                    btnAddRecord.Visible = false;
            }
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Session["tableId"] = string.Empty;
            Session["rtype"] = 0;
            Response.Redirect("FIRForm");
        }

        protected void btnFIRRegistration_Click(object sender, EventArgs e)
        {
            Response.Redirect("FIRList");

        }
        protected void btnADDAccusedDetails_Click(object sender, EventArgs e)
        {
            Response.Redirect("ADDAccusedDetailsList");
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            //string ID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("seizureno") as Label).Text;
            string tableId = (gvr.Cells[gvr.Cells.Count - 1].FindControl("TableId") as Label).Text;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "1";
            //Session["seizureno"] = ID;
            Session["tableId"] = tableId;
            Response.Redirect("FIRForm");
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            //string ID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("seizureno") as Label).Text;
            string tableId = (gvr.Cells[gvr.Cells.Count - 1].FindControl("TableId") as Label).Text;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "2";
            //Session["seizureno"] = ID;
            Session["tableId"] = tableId;
            Response.Redirect("FIRForm");
        }

        protected void grdFIRListView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdFIRListView.PageIndex = e.NewPageIndex;
            obj = new List<cm_seiz_FIR>();
            string seizureNo = Session["seizureNo"].ToString();
            obj = BL_cm_seiz_FIR.GetList(seizureNo);
            grdFIRListView.DataSource = obj.ToArray();
            grdFIRListView.DataBind();
        }
    }
}