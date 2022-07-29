using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;
using Usermngt.Entities.CaseMgmt;

namespace UserMgmt
{
    public partial class SHO_DashBoard : System.Web.UI.Page
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
                calllist = BL_cm_seiz_BasicIformation.GetComplaintList(userid);
                var ad = (from s in calllist
                          where s.seizureno == "" && s.prfirno == ""
                          select s);
                grdComplintList.DataSource = ad.ToArray();
                grdComplintList.DataBind();
            }
        }

        protected void newComplaints_Click(object sender, EventArgs e)
        {
            Response.Redirect("SHO_DashBoard");
        }

        protected void SezierAdded_Click(object sender, EventArgs e)
        {
            Response.Redirect("SeizureAddesComplaints");
        }

        protected void FIRfiled_Click(object sender, EventArgs e)
        {
            Response.Redirect("FIRFiledComplaints");
        }

        protected void grdComplintList_DataBound(object sender, EventArgs e)
        {
            
        }

        protected void grdComplintList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = grdComplintList.SelectedRow.RowIndex;
            string compid = (grdComplintList.Rows[index].FindControl("lblcomid") as Label).Text;
            string raidlocation = (grdComplintList.Rows[index].FindControl("lblnearby") as Label).Text;
            string thana_mst_code = (grdComplintList.Rows[index].FindControl("lblthanacode") as Label).Text;
            Session["compid"] = compid;
            Session["raidlocation"] = raidlocation;
            Session["thana_mst_code"] = thana_mst_code;
            Session["UserID"] = Session["UserID"];
            Session["seizureNo"] = "";
            Session["rtype"] = "3";
            //string s = Session["division_code"].ToString();
            Response.Redirect("BasicIformationForm");

        }

        protected void grdComplintList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdComplintList, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";
            }
        }
    }
}