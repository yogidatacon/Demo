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
    public partial class ParameterList : System.Web.UI.Page
    {
        private readonly IMasterDataService masterDataService = new MasterDataProvider();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.Keys.Count > default(int))
                {
                    string userid = Session["UserID"].ToString();
                    BindParameterList(userid);
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
            Response.Redirect("ParameterForm.aspx");
        }

        #region Grid Operations
        protected void GridViews_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                string parameter_master_id = e.CommandArgument.ToString();
                //Bhavin
                //Response.Redirect($"/ParameterForm.aspx?QMode=Edit&QId={parameter_master_id}");
                Response.Redirect($"~/ParameterForm.aspx?QMode=Edit&QId={parameter_master_id}");
                //End
            }
            //Bhavin
            if (e.CommandName == "Delete_Record")
            {
                 string parameter_master_id = e.CommandArgument.ToString();
                bool result = masterDataService.deleteParameter(parameter_master_id);

                //Bhavin
                if (result == false)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Record Can not be Deleted. Because it is used in other table. ')</script>");

                }
                //End
                string userid = Session["UserID"].ToString();
                BindParameterList(userid);
            }
            //End
        }

        protected void grdParameterList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdParameterList.PageIndex = e.NewPageIndex;
            string userid = Convert.ToString(Session["UserID"]);
            BindParameterList(userid);
        }
        #endregion

        #region Private Methods
        private void BindParameterList(string userid)
        {
            var liquorTypes = masterDataService.ParameterList(userid);
            grdParameterList.DataSource = liquorTypes;
            grdParameterList.DataBind();
        }
        #endregion
    }
}