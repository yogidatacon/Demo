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
    public partial class DistilleryForm : System.Web.UI.Page
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
                    txtDistilleryId.Enabled = false;
                }
            }
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/DistilleryList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var distillery = new Distillery()
            {
                distillery_code = Convert.ToInt32(txtDistilleryId.Text.ToString()),
                distillery_name = txtDistilleryName.Text,
            };
            if (Request.QueryString.AllKeys.Contains("QMode"))
            {
                distillery.distillery_code = Convert.ToInt32(Request.QueryString.Get("QId"));
            }
            try
            {
                var isDistillerySaved = Request.QueryString.AllKeys.Contains("QMode") ? masterDataService.UpdateDistillery(distillery) : masterDataService.SaveDistillery(distillery);
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
                Response.Redirect("~/DistilleryList.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/DistilleryList.aspx");
        }

        #region Private Methods 
        private void LoadDetails(int key)
        {
            Distillery distillery = masterDataService.LoadDistilleryDetails(key);
            if (distillery == null)
            {
                throw new ArgumentNullException();
            }
            txtDistilleryId.Text = distillery.distillery_code.ToString();
            txtDistilleryName.Text = distillery.distillery_name;
        }
        #endregion
    }
}