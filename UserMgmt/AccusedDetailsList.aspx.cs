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
    public partial class AccusedDetailsList : System.Web.UI.Page
    {
        List<cm_seiz_AccusedDetails> ads = new List<cm_seiz_AccusedDetails>();
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
                ads = BL_cm_seiz_AccusedDetails.GetDetails(Session["seizureno"].ToString()+"&"+raidby);
                string seizureno = Session["seizureno"].ToString();
                var ad = (from s in ads
                          where s.raidby==raidby
                          select s);
                grdAccusedDetailsView.DataSource = ad.ToList();
                grdAccusedDetailsView.DataBind();
                for (int i = 0; i < grdAccusedDetailsView.Rows.Count; i++)
                {
                    string user1 = (grdAccusedDetailsView.Rows[i].FindControl("lbluserid") as Label).Text;
                  //  bool visble = (grdAccusedDetailsView.Rows[i].FindControl("lbluserid") as LinkButton);
                    if (Session["UserID"].ToString() != user1 )
                        (grdAccusedDetailsView.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
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
            Session["rtype1"] = 0;
            Response.Redirect("AccusedDetailsForm");
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
            Response.Redirect("AccusedDetailsForm");
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
            Response.Redirect("AccusedDetailsForm");
        }

        protected void grdAccusedDetailsView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAccusedDetailsView.PageIndex = e.NewPageIndex;
            ads = new List<cm_seiz_AccusedDetails>();

            string seizureno = Session["seizureno"].ToString();
            ads = BL_cm_seiz_AccusedDetails.GetDetails("");
            var ad = (from s in ads
                      where s.seizureno == Convert.ToInt32(seizureno)
                      select s);
            grdAccusedDetailsView.DataSource = ad.ToList();
            grdAccusedDetailsView.DataBind();
        }
    }
}