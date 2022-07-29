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
    public partial class OffencesCommittedList : System.Web.UI.Page
    {
        List<cm_seiz_OffencesCommitted> offns = new List<cm_seiz_OffencesCommitted>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Session["seizureno"] = Session["seizureno"];
                string raidby = "";
                if (Session["RaidBy"].ToString().Substring(0, 1).ToUpper() == "E")
                    raidby = "E";
                else
                    raidby = "P";
                offns = BL_cm_seiz_OffencesCommitted.GetList(Session["seizureno"].ToString());
                var ad = (from s in offns
                          where s.raidby ==raidby && s.user_id==Session["UserID"].ToString()
                          select s);
               grdOffencesCommittedView.DataSource = ad.ToList();
               grdOffencesCommittedView.DataBind();
                for (int i = 0; i < grdOffencesCommittedView.Rows.Count; i++)
                {
                    string user1 = (grdOffencesCommittedView.Rows[i].FindControl("lbluserid") as Label).Text;
                    if (Session["UserID"].ToString() != user1)
                        (grdOffencesCommittedView.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
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
            Session["rtype1"] = 0;
            Session["seizureno"] = Session["seizureno"];
            Response.Redirect("OffencesCommittedForm");
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

        protected void btnAccusedDetail_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("AccusedDetailsList");
        }
        protected void btnOffencesCommitted_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("OffencesCommittedList");

        }
        protected void btnCaseHistory_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("CaseHistoryList");

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string adid = (gvr.Cells[gvr.Cells.Count - 1].FindControl("TableId") as Label).Text;
            Session["TableId"] = adid;
            Session["UserID"] = Session["UserID"];
            Session["seizureno"] = Session["seizureno"];
            Session["rtype"] = 1;
            Session["rtype1"] = 1;
            Response.Redirect("OffencesCommittedForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string adid = (gvr.Cells[gvr.Cells.Count - 1].FindControl("TableId") as Label).Text;
            Session["TableId"] = adid;
            Session["UserID"] = Session["UserID"];
            Session["seizureno"] = Session["seizureno"];
            Session["rtype"] = 2;
            Session["rtype1"] = 2;
            Response.Redirect("OffencesCommittedForm");
        }

        protected void grdOffencesCommittedView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdOffencesCommittedView.PageIndex = e.NewPageIndex;
            offns = new List<cm_seiz_OffencesCommitted>();

            string seizureno = Session["seizureno"].ToString();
            offns = BL_cm_seiz_OffencesCommitted.GetList(seizureno);
            var ad = (from s in offns
                      where s.seizureno == Session["seizureno"].ToString()
                      select s);
            grdOffencesCommittedView.DataSource = ad.ToList();
            grdOffencesCommittedView.DataBind();
        }
    }
}