using System;
using System.Web.UI.WebControls;
using Usermngt.BL.MasterData;
using Usermngt.BL.Service;

namespace UserMgmt
{
    public partial class DistrictUserList : System.Web.UI.Page
    {
        private readonly IMasterDataService masterDataService = new MasterDataProvider();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.Keys.Count > default(int))
                {
                    string userid = Session["UserID"].ToString();
                    BindDistrictUserGrid(userid);
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
            Session["rType"] = 0;
            Response.Redirect("DistrictUserForm.aspx");
        }

        #region Grid Operations
        protected void GridViews_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string district_user_id = e.CommandArgument.ToString();

            if (e.CommandName == "Edit")
            {
                Response.Redirect($"~/DistrictUserForm.aspx?QMode=Edit&QId={district_user_id}");
            }
            if (e.CommandName == "Edit")
            {
                Response.Redirect($"~/DistrictUserForm.aspx?QMode=View&QId={district_user_id}");
            }
        }

        protected void grdDistrictUserList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDistrictUserList.PageIndex = e.NewPageIndex;
            string userid = Convert.ToString(Session["UserID"]);
            BindDistrictUserGrid(userid);
        }
        #endregion

        #region Private Methods
        private void BindDistrictUserGrid(string userid)
        {
            var districtUsers = masterDataService.DistrictUsers(userid);
            grdDistrictUserList.DataSource = districtUsers;
            grdDistrictUserList.DataBind();
        }
        #endregion
    }
}