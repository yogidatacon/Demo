using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class ApparatusList : System.Web.UI.Page
    {
        List<cm_seiz_Apparatus> obj = new List<cm_seiz_Apparatus>();
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
                obj = BL_cm_seiz_Apparatus.GetList(seizureNo);
                grdApparatusView.DataSource = obj.ToArray();
                grdApparatusView.DataBind();
                for (int i = 0; i < grdApparatusView.Rows.Count; i++)
                {
                    string user = (grdApparatusView.Rows[i].FindControl("lbluserid") as Label).Text;
                    if (Session["UserID"].ToString() != user)
                        (grdApparatusView.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                }
                Session["sType"] = Session["sType"];
                if (Session["sType"].ToString() == "1")
                    btnAddRecord.Visible = false;
            }
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
            Response.Redirect("ApparatusForm");
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
            Response.Redirect("ApparatusForm");
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = 0;
            Session["tableId"] = string.Empty;
            Response.Redirect("ApparatusForm");
        }

        protected void grdApparatusView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdApparatusView.PageIndex = e.NewPageIndex;
            obj = new List<cm_seiz_Apparatus>();

            string seizureNo = Session["seizureNo"].ToString();
            obj = BL_cm_seiz_Apparatus.GetList(seizureNo);
            grdApparatusView.DataSource = obj.ToArray();
            grdApparatusView.DataBind();
        }
    }
}