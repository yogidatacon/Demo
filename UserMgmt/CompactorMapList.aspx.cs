using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL.MasterData;
using Usermngt.BL.Service;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class CompactorMapList : System.Web.UI.Page
    {

        private readonly IMasterDataService masterDataService = new MasterDataProvider();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Keys.Count > default(int))
            {
                string userid = Session["UserID"].ToString();
                BindCompactorList(userid);
            }
            else
            {
                Response.Redirect("~/LoginPage");
            }
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Convert.ToString(Session["UserID"]);
            Response.Redirect("CompactorMapForm.aspx");
        }

        protected void GridViews_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit_Record")
            {
                string compactor_id = e.CommandArgument.ToString();
                //Bhavin
                //Response.Redirect($"/ThanaForm.aspx?QMode=Edit&QId={thana_id}");
                Response.Redirect($"~/CompactorMapForm.aspx?QMode=Edit&QId={compactor_id}");
                //End
            }
            if (e.CommandName == "Delete_Record")
            {
                string compactor_id = e.CommandArgument.ToString();
                bool result = masterDataService.deleteCompactor(compactor_id);
                if (result == false)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Record Can not be Deleted. Because it is used in other table. ')</script>");
                }
                string userid = Session["UserID"].ToString();
                BindCompactorList(userid);                
            }
        }


        protected void grdCompactorList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCompactorList.PageIndex = e.NewPageIndex;
            string userid = Convert.ToString(Session["UserID"]);
            BindCompactorList(userid);
        }

        private void BindCompactorList(string userid)
        {

            var compactor = masterDataService.CompactorList(userid);
            grdCompactorList.DataSource = compactor;
            grdCompactorList.DataBind();

        }
    }
}