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
    public partial class TypeofLiquorList : System.Web.UI.Page
    {
        private readonly IMasterDataService masterDataService = new MasterDataProvider();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.Keys.Count > default(int))
                {
                    string userid = Session["UserID"].ToString();
                    BindLiquorTypeList(userid);
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
            Response.Redirect("TypeofLiquorForm.aspx");
        }

        #region Grid Operations
        protected void GridViews_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit_Record")
            {
                string liquorTypeId = e.CommandArgument.ToString();
                //Bhavin
                //Response.Redirect($"/TypeofLiquorForm.aspx?QMode=Edit&QId={liquorTypeId}");
                Response.Redirect($"~/TypeofLiquorForm.aspx?QMode=Edit&QId={liquorTypeId}");
                //End
            }
            if (e.CommandName == "Delete_Record")
            {
                string liquorTypeId = e.CommandArgument.ToString();
                bool result = masterDataService.deleteLiqType(liquorTypeId);
                //Bhavin
                if (result == false)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Record Can not be Deleted. Because it is used in other table. ')</script>");

                }
                //End
                string userid = Session["UserID"].ToString();
                BindLiquorTypeList(userid);
            }
        }

        protected void grdLiquourList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdLiquourList.PageIndex = e.NewPageIndex;
            string userid = Convert.ToString(Session["UserID"]);
            BindLiquorTypeList(userid);
        }
        #endregion

        #region Private Methods
        private void BindLiquorTypeList(string userid)
        {
            var liquorTypes = masterDataService.TypeOfLiquorList(userid);
            grdLiquourList.DataSource = liquorTypes;
            grdLiquourList.DataBind();
        }
        #endregion
    }
}