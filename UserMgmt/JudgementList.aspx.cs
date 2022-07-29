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
    public partial class JudgementList : System.Web.UI.Page
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
                string trialstage_code = "9";//Judgement
                obj = BL_cm_seiz_trial.GetList(seizureNo, trialstage_code);
                var ad = (from s in obj
                          where s.raidby == raidby
                          select s);
                grdJudgementList.DataSource = ad.ToArray();
                grdJudgementList.DataBind();
                for (int i = 0; i < grdJudgementList.Rows.Count; i++)
                {
                    string user1 = (grdJudgementList.Rows[i].FindControl("lbluserid") as Label).Text;
                    if (Session["UserID"].ToString() != user1)
                        (grdJudgementList.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                }
                if (grdJudgementList.Rows.Count > 0)
                    btnAddRecord.Visible = false;
            }
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Session["tableId"] = string.Empty;
            Session["rtype"] = 0;
            Response.Redirect("JudgementForm");
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
            Response.Redirect("JudgementForm");
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
            Response.Redirect("JudgementForm");
        }

        protected void grdJudgementList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdJudgementList.PageIndex = e.NewPageIndex;
            obj = new List<cm_seiz_trial>();

            Session["UserID"] = Session["UserID"];
            string seizureNo = Session["seizureNo"]?.ToString() ?? string.Empty;
            string trialstage_code = "9";//Judgement
            obj = BL_cm_seiz_trial.GetList(seizureNo, trialstage_code);
            grdJudgementList.DataSource = obj.ToArray();
            grdJudgementList.DataBind();
        }
    }
}