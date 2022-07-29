using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL.Service;
using Usermngt.BL.MasterData;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class ParameterForm : System.Web.UI.Page
    {
        private readonly IMasterDataService masterDataService = new MasterDataProvider();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.AllKeys.Contains("QMode"))
                {
                    var key = Convert.ToInt32(Request.QueryString.Get("QId"));
                    LoadDetails(key); 
                }
            }
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/ParameterList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var parameter = new ParameterMaster()
            {
                parameter_master_name = txtParameterName.Text,
                created_by = Convert.ToString(Session["UserID"])
            };
            if (Request.QueryString.AllKeys.Contains("QMode"))
            {
                parameter.parameter_master_id = Convert.ToInt32(Request.QueryString.Get("QId"));
            }
            try
            {
                var isDistillerySaved = Request.QueryString.AllKeys.Contains("QMode") ? masterDataService.UpdateParameter(parameter) : masterDataService.SaveParameter(parameter);
                if (!isDistillerySaved.Item1)
                {
                    var alert = ($@"
                                    <script type='text/javascript'>
                                        window.onload=function()
                                        {{
                                             alert('{ isDistillerySaved.Item2}');
                                        }};
                                    </script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", alert);
                    return;
                }
                Session["UserID"] = Session["UserID"].ToString();
                Response.Redirect("~/ParameterList.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/ParameterList.aspx");
        }

        #region Private Methods 
        private void LoadDetails(int key)
        {
            ParameterMaster param = masterDataService.LoadParameterDetails(key);
            if (param == null)
            {
                throw new ArgumentNullException();
            }
            txtParameterName.Text = param.parameter_master_name;
        }
        #endregion
    }
}