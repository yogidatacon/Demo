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
    public partial class ThanaList : System.Web.UI.Page
    {
        private readonly IMasterDataService masterDataService = new MasterDataProvider();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.Keys.Count > default(int))
                {
                    string userid = Session["UserID"].ToString();
                    BindThanaList(userid);
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
            Response.Redirect("ThanaForm.aspx");
        }

        #region Grid Operations
        protected void GridViews_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                string thana_id = e.CommandArgument.ToString();
                Response.Redirect($"/ThanaForm.aspx?QMode=Edit&QId={thana_id}");
            }
        }

        protected void grdThanaList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdThanaList.PageIndex = e.NewPageIndex;
            string userid = Convert.ToString(Session["UserID"]);
            BindThanaList(userid);
        }
        #endregion

        #region Private Methods
        private void BindThanaList(string userid)
        {
            var thanas = masterDataService.ThanaList(userid);
            grdThanaList.DataSource = thanas;
            grdThanaList.DataBind();
        }
        #endregion
    }
}