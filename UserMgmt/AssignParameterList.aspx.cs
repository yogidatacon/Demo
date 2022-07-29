using System;
using System.Web.UI.WebControls;
using Usermngt.BL.MasterData;
using Usermngt.BL.Service;

namespace UserMgmt
{
    public partial class AssignParameterList : System.Web.UI.Page
    {
        private readonly IMasterDataService masterDataService = new MasterDataProvider();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.Keys.Count > default(int))
                {
                    string userid = Session["UserID"].ToString();
                    BindAssignParameterList(userid);
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
            Response.Redirect("AssignParameterForm.aspx");
        }

        #region Grid Operations
        protected void GridViews_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                var inputParams = e.CommandArgument.ToString().Split(',');
                var AssignedParameterId = inputParams.Length > 0 ? inputParams[0] : default(string);
                var LiquorTypeId = inputParams.Length > 1 ? inputParams[1] : default(string);
                var SubLiquorTypeId = inputParams.Length > 2 ? inputParams[2] : default(string);
                //Bhavin
                //Response.Redirect($"/AssignParameterForm.aspx?QMode=View&QId={AssignedParameterId}&QliquorId={LiquorTypeId}&QsubliquorId={SubLiquorTypeId}");
                Response.Redirect($"~/AssignParameterForm.aspx?QMode=View&QId={AssignedParameterId}&QliquorId={LiquorTypeId}&QsubliquorId={SubLiquorTypeId}");
                //End
            }
        }

        protected void grdAssignedParameterList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAssignedParameterList.PageIndex = e.NewPageIndex;
            string userid = Convert.ToString(Session["UserID"]);
            BindAssignParameterList(userid);
        }
        #endregion

        #region Private Methods
        private void BindAssignParameterList(string userid)
        {
            var assignedParameters = masterDataService.ListAssignedParameters(userid);
            grdAssignedParameterList.DataSource = assignedParameters;
            grdAssignedParameterList.DataBind();
        }
        #endregion
    }
}