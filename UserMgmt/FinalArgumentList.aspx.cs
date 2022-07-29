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
    public partial class FinalArgumentList : System.Web.UI.Page
    {
        List<cm_seiz_trial> obj = new List<cm_seiz_trial>();
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
                string trialstage_code = "8";//Final Argument
                obj = BL_cm_seiz_trial.GetList(seizureNo, trialstage_code);
                var ad = (from s in obj
                          where s.raidby == raidby
                          select s);
                grdFinalArgumentList.DataSource = ad.ToArray();
                grdFinalArgumentList.DataBind();
                for (int i = 0; i < grdFinalArgumentList.Rows.Count; i++)
                {
                    string user1 = (grdFinalArgumentList.Rows[i].FindControl("lbluserid") as Label).Text;
                    if (Session["UserID"].ToString() != user1)
                    {
                        (grdFinalArgumentList.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                        btnAddRecord.Visible = false;
                    }
                }
            }
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Session["tableId"] = string.Empty;
            Session["rtype"] = 0;
            Response.Redirect("FinalArgumentForm");
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
            Response.Redirect("FinalArgumentForm");
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
            Response.Redirect("FinalArgumentForm");
        }

        protected void grdFinalArgumentList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdFinalArgumentList.PageIndex = e.NewPageIndex;
            obj = new List<cm_seiz_trial>();

            Session["UserID"] = Session["UserID"];
            string seizureNo = Session["seizureNo"]?.ToString() ?? string.Empty;
            string trialstage_code = "8";//Final Argument
            obj = BL_cm_seiz_trial.GetList(seizureNo, trialstage_code);
            grdFinalArgumentList.DataSource = obj.ToArray();
            grdFinalArgumentList.DataBind();
        }
    }
}