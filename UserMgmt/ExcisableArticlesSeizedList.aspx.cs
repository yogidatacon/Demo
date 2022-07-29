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
    public partial class ExcisableArticlesSeizedList : System.Web.UI.Page
    {
        List<cm_seiz_ExcisableArticlesSeized> obj = new List<cm_seiz_ExcisableArticlesSeized>();
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
                string seizureNo = Session["seizureNo"].ToString()+"&"+Session["RaidBy"];
                obj = BL_cm_seiz_ExcisableArticlesSeized.GetList(seizureNo);
                grdExcisableArticlesView.DataSource = obj.ToArray();
                grdExcisableArticlesView.DataBind();
                for (int i = 0; i < grdExcisableArticlesView.Rows.Count; i++)
                {
                    string user = (grdExcisableArticlesView.Rows[i].FindControl("lbluserid") as Label).Text;
                    if (Session["UserID"].ToString() != user)
                        (grdExcisableArticlesView.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                }
                Session["sType"] = Session["sType"];
                if (Session["sType"].ToString() == "1")
                    btnAddRecord.Visible = false;

            }
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Session["tableId"] = string.Empty;
            Session["rtype"] = 0;           
            Response.Redirect("ExcisableArticlesSeizedForm");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            string seizureId = (gvr.Cells[gvr.Cells.Count - 1].FindControl("seizureno") as Label).Text;
            string tableId = (gvr.Cells[gvr.Cells.Count - 1].FindControl("TableId") as Label).Text;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "1";
            Session["seizureno"] = seizureId;
            Session["tableId"] = tableId;
            Response.Redirect("ExcisableArticlesSeizedForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string seizureId = (gvr.Cells[gvr.Cells.Count - 1].FindControl("seizureno") as Label).Text;
            string tableId = (gvr.Cells[gvr.Cells.Count - 1].FindControl("TableId") as Label).Text;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "2";
            Session["seizureno"] = seizureId;
            Session["tableId"] = tableId;
            Response.Redirect("ExcisableArticlesSeizedForm");
        }

        protected void grdExcisableArticlesView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdExcisableArticlesView.PageIndex = e.NewPageIndex;
            obj = new List<cm_seiz_ExcisableArticlesSeized>();

            string seizureNo = Session["seizureNo"].ToString() + "&" + Session["RaidBy"];
            obj = BL_cm_seiz_ExcisableArticlesSeized.GetList(seizureNo);
            grdExcisableArticlesView.DataSource = obj.ToArray();
            grdExcisableArticlesView.DataBind();
        }
    }
}