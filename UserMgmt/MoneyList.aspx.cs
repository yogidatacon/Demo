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
    public partial class MoneyList : System.Web.UI.Page
    {
        List<cm_seiz_Money> money = new List<cm_seiz_Money>();
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
            money = new List<cm_seiz_Money>();
            money = BL_Money.GetList(seizureNo);
            //UserDetails user = new UserDetails();
            //user = BL_UserDetails.CheckUser(Session["UserID"].ToString());

            //if (Session["UserID"].ToString() == "Admin")
            ////{
            //    var list = from s in money
            //               select s;
                grdMoneyListView.DataSource = money.ToList();
                grdMoneyListView.DataBind();
            for (int i = 0; i < grdMoneyListView.Rows.Count; i++)
            {
                string user1 = (grdMoneyListView.Rows[i].FindControl("lbluserid") as Label).Text;
                if (Session["UserID"].ToString() != user1)
                    (grdMoneyListView.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
            }
            //foreach (GridViewRow dr1 in grdMoneyListView.Rows)
            //{
            //    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
            //    btn.Visible = true;
            //}
            //}
            //else
            //{
            //    var list = from s in money
            //               where s.user_id == user.user_id
            //               select s;
            //    grdMoneyListView.DataSource = list.ToList();
            //    grdMoneyListView.DataBind();
            //    Session["sType"] = Session["sType"];
            if (Session["sType"].ToString() == "1")
                    btnAddRecord.Visible = false;
            

        }
        protected void AddRecord_Click(object sender, EventArgs e)
        {

            Session["UserID"] = Session["UserID"].ToString();
            Session["tableId"] = string.Empty;
            Session["rtype"] = "0";
            Response.Redirect("MoneyForm");
        }
        protected void btnBasicInformation_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("BasicIformationForm");

        }

        protected void btnEAS_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ExcisableArticlesSeizedList");
        }

        protected void btnOtherExisable_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("VehicleList");

        }
        protected void btnRaidTeam_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("RaidTeamList");

        }
        protected void btnAccusedDetails_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("AccusedDetailsList");
        }

        protected void btnVehicle_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("VehicleList");
        }
        protected void btnApparatus_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ApparatusList");

        }
        protected void btnProperty_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PropertyList");

        }
        protected void btnMoney_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("MoneyList");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string MoneyId = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblseizuremoneydetailsid") as Label).Text;
            string tableId = (gvr.Cells[gvr.Cells.Count - 1].FindControl("TableId") as Label).Text;
            Session["UserID"] = Session["UserID"];
            Session["MoneyId"] = MoneyId;
            Session["rtype"] = "2";
            Session["tableId"] = tableId;
            Response.Redirect("MoneyForm");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string MoneyId = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblseizuremoneydetailsid") as Label).Text;
            string tableId = (gvr.Cells[gvr.Cells.Count - 1].FindControl("TableId") as Label).Text;
            Session["UserID"] = Session["UserID"];
            Session["MoneyId"] = MoneyId;
            Session["rtype"] = "1";
            Session["tableId"] = tableId;
            Response.Redirect("MoneyForm");
        }

        protected void grdMoneyListView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if(IsPostBack)
            {
               grdMoneyListView.PageIndex = e.NewPageIndex;
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());

                if (Session["UserID"].ToString() == "Admin")
                {
                    var list = from s in money
                               select s;
                    grdMoneyListView.DataSource = list.ToList();
                    grdMoneyListView.DataBind();
                    foreach (GridViewRow dr1 in grdMoneyListView.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = true;
                    }
                }
                else
                {
                    var list = from s in money
                               where s.user_id == user.user_id
                               select s;
                    grdMoneyListView.DataSource = list.ToList();
                    grdMoneyListView.DataBind();
                }
            }

        }
    }
}