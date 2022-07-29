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
    public partial class SizeForm : System.Web.UI.Page
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
            Response.Redirect("~/SizeList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var size = new SizeMaster()
            {
                size_master_name = txtSizeName.Text
            };
            if (Request.QueryString.AllKeys.Contains("QMode"))
            {
                size.size_master_id = Convert.ToInt32(Request.QueryString.Get("QId"));
            }
            try
            {
                var isSizeSavedResponse = Request.QueryString.AllKeys.Contains("QMode") ? masterDataService.UpdateSize(size) : masterDataService.SaveSize(size);
                if (!isSizeSavedResponse.Item1)
                {
                    var alert = ($@"
                                    <script type='text/javascript'>
                                        window.onload=function()
                                        {{
                                             alert('{ isSizeSavedResponse.Item2}');
                                        }};
                                    </script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", alert);
                    return;
                }
                Session["UserID"] = Session["UserID"].ToString();
                Response.Redirect("~/SizeList.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/SizeList.aspx");
        }

        #region Private Methods 
        private void LoadDetails(int key)
        {
            SizeMaster size = masterDataService.LoadSizeDetails(key);
            if (size == null)
            {
                throw new ArgumentNullException();
            }
            txtSizeName.Text = size.size_master_name;
        }
        #endregion
    }
}