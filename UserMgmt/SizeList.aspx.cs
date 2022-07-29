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
    public partial class SizeList : System.Web.UI.Page
    {
        private readonly IMasterDataService masterDataService = new MasterDataProvider();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.Keys.Count > default(int))
                {
                    string userid = Session["UserID"].ToString();
                    BindSizeList(userid);
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
            Response.Redirect("SizeForm.aspx");
        }

        #region Grid Operations
        protected void GridViews_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit_Record")
            {
                string sizemasteId = e.CommandArgument.ToString();
                //Bhavin
                //Response.Redirect($"/SizeForm.aspx?QMode=Edit&QId={sizemasteId}");
                Response.Redirect($"~/SizeForm.aspx?QMode=Edit&QId={sizemasteId}");
                //End
            }
            if (e.CommandName == "Delete_Record")
            {
                string sizemasteId = e.CommandArgument.ToString();
                bool result = masterDataService.deleteSize(sizemasteId);
                //Bhavin
                if (result == false)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Record Can not be Deleted. Because it is used in other table. ')</script>");

                }
                //End
                string userid = Session["UserID"].ToString();
                BindSizeList(userid);
            }
        }

        protected void grdSizeList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSizeList.PageIndex = e.NewPageIndex;
            string userid = Convert.ToString(Session["UserID"]);
            BindSizeList(userid);
        }
        #endregion

        #region Private Methods
        private void BindSizeList(string userid)
        {
            var sizes = masterDataService.SizeMasterList(userid);
            grdSizeList.DataSource = sizes;
            grdSizeList.DataBind();
        }
        #endregion
    }
}