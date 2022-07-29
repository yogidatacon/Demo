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
    public partial class BrandList : System.Web.UI.Page
    {
        private readonly IMasterDataService masterDataService = new MasterDataProvider();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.Keys.Count > default(int))
                {
                    string userid = Session["UserID"].ToString();
                    BindBrands(userid);
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
            Response.Redirect("BrandForm.aspx");
        }

        #region Grid Operations
        protected void GridViews_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit_Record")
            {
                string brand_id = e.CommandArgument.ToString();
                //Bhavin
                //Response.Redirect($"/BrandForm.aspx?QMode=Edit&QId={brand_id}");
                Response.Redirect($"~/BrandForm.aspx?QMode=Edit&QId={brand_id}");
                //End
            }
            if (e.CommandName == "Delete_Record")
            {
                string brand_id = e.CommandArgument.ToString();
                bool result = masterDataService.deleteBrand(brand_id);
                //Bhavin
                if (result == false)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Record Can not be Deleted. Because it is used in other table. ')</script>");

                }
                //End
                string userid = Session["UserID"].ToString();
                BindBrands(userid);
            }
        }

        protected void grdBrandList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdBrandList.PageIndex = e.NewPageIndex;
            string userid = Convert.ToString(Session["UserID"]);
            BindBrands(userid);
        }
        #endregion

        #region Private Methods
        private void BindBrands(string userid)
        {
            var brands = masterDataService.BrandMasterList(userid);
            grdBrandList.DataSource = brands;
            grdBrandList.DataBind();
        }
        #endregion
    }
}