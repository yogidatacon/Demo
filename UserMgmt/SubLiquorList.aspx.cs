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
    public partial class SubLiquorList : System.Web.UI.Page
    {
        private readonly IMasterDataService masterDataService = new MasterDataProvider();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.Keys.Count > default(int))
                {
                    string userid = Session["UserID"].ToString();
                    BindSubLiquorList(userid);
                }
                else
                {
                    Response.Redirect("~/LoginPage");
                }
            }
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Convert.ToString(Session["UserID"]);
            Response.Redirect("SubLiquorForm.aspx");
        }

        #region Grid Operations
        protected void GridViews_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit_Record")
            {
                string id = e.CommandArgument.ToString();
                //Bhavin
                //Response.Redirect($"/SubLiquorForm.aspx?QMode=Edit&QId={id}");
                Response.Redirect($"~/SubLiquorForm.aspx?QMode=Edit&QId={id}");
                //End
            }
            if (e.CommandName == "Delete_Record")
            {
                string id = e.CommandArgument.ToString();
                bool result = masterDataService.DeleteSubType(id);
                //Bhavin
                if (result == false)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Record Can not be Deleted. Because it is used in other table. ')</script>");

                }
                //End
                string userid = Session["UserID"].ToString();
                BindSubLiquorList(userid);
            }
        }

        protected void grdSubLiquorList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSubLiquorList.PageIndex = e.NewPageIndex;
            string userid = Convert.ToString(Session["UserID"]);
            BindSubLiquorList(userid);
        }
        #endregion

        #region Private Methods
        private void BindSubLiquorList(string userid)
        {
            var subLiquors = masterDataService.SubLiquorList(userid);
            grdSubLiquorList.DataSource = subLiquors;
            grdSubLiquorList.DataBind();
        }
        #endregion
    }
}