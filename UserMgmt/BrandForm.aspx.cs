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
    public partial class BrandForm : System.Web.UI.Page
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
            Response.Redirect("~/BrandList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var brand = new BrandMaster()
            {
                brand_master_name = txtBrandName.Text
            };
            if (Request.QueryString.AllKeys.Contains("QMode"))
            {
                brand.brand_master_id = Convert.ToInt32(Request.QueryString.Get("QId"));
            }
            try
            {
                var isBrandSaveResponse = Request.QueryString.AllKeys.Contains("QMode") ? masterDataService.UpdateBrand(brand) : masterDataService.SaveBrand(brand);
                if (!isBrandSaveResponse.Item1)
                {
                    var alert = ($@"
                                    <script type='text/javascript'>
                                        window.onload=function()
                                        {{
                                             alert('{ isBrandSaveResponse.Item2}');
                                        }};
                                    </script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", alert);
                    return;
                }
                Session["UserID"] = Session["UserID"].ToString();
                Response.Redirect("~/BrandList.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/BrandList.aspx");
        }

        #region Private Methods 
        private void LoadDetails(int key)
        {
            BrandMaster brand = masterDataService.LoadBrandDetails(key);
            if (brand == null)
            {
                throw new ArgumentNullException();
            }
            txtBrandName.Text = brand.brand_master_name;
        }
        #endregion
    }
}