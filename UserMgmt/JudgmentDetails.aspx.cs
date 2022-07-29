using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserMgmt
{
    public partial class JudgmentDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {

            Response.Redirect("JudgementForm");
        }

        protected void btnTrailList_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrailList");


        }

        protected void btnTrailCaseHistory_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrailCaseHistoryList");
        }
        protected void btnTrial_Click(object sender, EventArgs e)
        {
            Response.Redirect("CognizanceList");
        }
        protected void btnProsecutionEvidence_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProsecutionEvidenceList");

        }

        protected void btnAccusedStatement_Click(object sender, EventArgs e)
        {
            Response.Redirect("AccusedStatementList");

        }
        protected void btnDefenceStatement_Click(object sender, EventArgs e)
        {
            Response.Redirect("DefenceStatementList");
        }
        protected void btnFinalArgument_Click(object sender, EventArgs e)
        {
            Response.Redirect("FinalArgumentList");

        }

        protected void btnJudgement_Click(object sender, EventArgs e)
        {
            Response.Redirect("JudgementList");

        }

        protected void btnJudD_Click(object sender, EventArgs e)
        {
            Response.Redirect("JudgmentDetails");

        }
    }
}