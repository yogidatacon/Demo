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
    public partial class RaidTeamList : System.Web.UI.Page
    {
        List<cm_seiz_RaidTeam> raids = new List<cm_seiz_RaidTeam>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Session["UserID"] = Session["UserID"];
                string seizureNo = Session["seizureNo"].ToString();
                string raidby = "";
                if (Session["RaidBy"].ToString().Substring(0, 1).ToUpper() == "E")
                    raidby = "E";
                else
                    raidby = "P";
                raids = BL_cm_seiz_RaidTeam.GetDeails(seizureNo);
                var ad = (from s in raids
                          where s.raidby == raidby
                          select s);
                grdRaidTeamView.DataSource = ad.ToList();
                grdRaidTeamView.DataBind();
                for (int i = 0; i < grdRaidTeamView.Rows.Count; i++)
                {
                    string user1 = (grdRaidTeamView.Rows[i].FindControl("lbluserid") as Label).Text;
                    if (Session["UserID"].ToString() != user1)
                        (grdRaidTeamView.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
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
            Session["seizureno"] = Session["seizureno"];
            Session["rtype"] = 0;
            Response.Redirect("RaidTeamForm");
        }
        protected void btnBasicInformation_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("BasicIformationForm");
        }

        protected void btnEAS_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ExcisableArticlesSeizedList");
        }

        protected void btnOtherExisable_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VehicleList");

        }
        protected void btnRaidTeam_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RaidTeamList");

        }
        protected void btnAccusedDetails_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("AccusedDetailsList");
        }

        protected void btnRaidTeamll_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RaidTeamList");
        }
        protected void btnWitness_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("WitnessList");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string adid = (gvr.Cells[gvr.Cells.Count - 1].FindControl("TableId") as Label).Text;
            Session["TableId"] = adid;
            Session["seizureno"] = Session["seizureno"];
            Session["rtype"] = 1;
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RaidTeamForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string adid = (gvr.Cells[gvr.Cells.Count - 1].FindControl("TableId") as Label).Text;
            Session["TableId"] = adid;
            Session["seizureno"] = Session["seizureno"];
            Session["rtype"] = 2;
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RaidTeamForm");           
        }

        protected void grdRaidTeamView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdRaidTeamView.PageIndex = e.NewPageIndex;
            raids = new List<cm_seiz_RaidTeam>();

            string seizureNo = Session["seizureNo"].ToString();
            raids = BL_cm_seiz_RaidTeam.GetDeails(seizureNo);
            var ad = (from s in raids
                      where s.seizureno == Session["seizureno"].ToString()
                      select s);
            grdRaidTeamView.DataSource = ad.ToList();
            grdRaidTeamView.DataBind();
        }
    }
}