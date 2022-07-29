using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities.CaseMgmt;

namespace UserMgmt
{
    public partial class FIRFiledComplaints : System.Web.UI.Page
    {
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
                Session["seizureNo"] = "";
                string userid = Session["UserID"].ToString();
                List<Call_Complaints> calllist = new List<Call_Complaints>();
                calllist = BL_cm_seiz_BasicIformation.GetComplaintList(userid.Trim());
                var ad = (from s in calllist
                          where s.seizureno != "" && s.prfirno != ""
                          select s);
                grdComplintList.DataSource = ad.ToArray(); ;
                grdComplintList.DataBind();
            }
        }
        protected void newComplaints_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("SHO_DashBoard");
        }

        protected void SezierAdded_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("SeizureAddesComplaints");
        }

        protected void FIRfiled_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("FIRFiledComplaints");
        }
    }
}