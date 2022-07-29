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
    public partial class PropertyList : System.Web.UI.Page
    {
        List<cm_seiz_Property> property = new List<cm_seiz_Property>();
        protected void Page_Load(object sender, EventArgs e)
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
            property = new List<cm_seiz_Property>();
            //property = BL_Property.GetList(Session["UserID"].ToString());
            property = BL_Property.GetList(seizureNo);
            //UserDetails user = new UserDetails();
            //user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            grdPropertyView.DataSource = property.ToList();
            grdPropertyView.DataBind();
            for (int i = 0; i < grdPropertyView.Rows.Count; i++)
            {
                string user1 = (grdPropertyView.Rows[i].FindControl("lbluserid") as Label).Text;
                if (Session["UserID"].ToString() != user1)
                    (grdPropertyView.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
            }
            Session["sType"] = Session["sType"];
                if (Session["sType"].ToString() == "1")
                    btnAddRecord.Visible = false;
            //}

        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Session["tableId"] = string.Empty;
            Session["rtype"] = 0;
            Response.Redirect("PropertyForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string FermenterId = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblseizurepropertydetailsid") as Label).Text;
            string tableId = (gvr.Cells[gvr.Cells.Count - 1].FindControl("TableId") as Label).Text;
            Session["UserID"] = Session["UserID"];
            Session["PropertyId"] = FermenterId;
            Session["rtype"] = "2";
            Session["tableId"] = tableId;
            Response.Redirect("PropertyForm");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string FermenterId = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblseizurepropertydetailsid") as Label).Text;
            string tableId = (gvr.Cells[gvr.Cells.Count - 1].FindControl("TableId") as Label).Text;
            Session["UserID"] = Session["UserID"];
            Session["PropertyId"] = FermenterId;
            Session["rtype"] = "1";
            Session["tableId"] = tableId;
            Response.Redirect("PropertyForm");
        }

        protected void grdPropertyView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if(IsPostBack)
            {
               grdPropertyView.PageIndex = e.NewPageIndex;
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());

                if (Session["UserID"].ToString() == "Admin")
                {
                    var list = from s in property
                               select s;
                    grdPropertyView.DataSource = list.ToList();
                    grdPropertyView.DataBind();
                    foreach (GridViewRow dr1 in grdPropertyView.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = true;
                    }
                }
                else
                {
                    var list = from s in property
                               where s.user_id == user.user_id
                               select s;
                    grdPropertyView.DataSource = list.ToList();
                    grdPropertyView.DataBind();
                }
            }
        }
    }
}