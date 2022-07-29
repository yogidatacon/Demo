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
    public partial class DistilleryList : System.Web.UI.Page
    {
        private readonly IMasterDataService masterDataService = new MasterDataProvider();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.Keys.Count > default(int))
                {
                    string userid = Session["UserID"].ToString();
                    BindDistilleryList(userid);
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
            Response.Redirect("DistilleryForm.aspx");
        }

        #region Grid Operations
        protected void GridViews_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit_Record")
            {
                string distillery_id = e.CommandArgument.ToString();
                //Bhavin
                //Response.Redirect($"/DistilleryForm.aspx?QMode=Edit&QId={distillery_id}");
                Response.Redirect($"~/DistilleryForm.aspx?QMode=Edit&QId={distillery_id}");
                //End
            }
        }

        protected void grdDistilleryList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDistilleryList.PageIndex = e.NewPageIndex;
            string userid = Convert.ToString(Session["UserID"]);
            BindDistilleryList(userid);
        }
        #endregion

        #region Private Methods
        private void BindDistilleryList(string userid)
        {
            var thanas = masterDataService.DistilleryList(userid);
            grdDistilleryList.DataSource = thanas;
            grdDistilleryList.DataBind();
        }
        #endregion
    }
}