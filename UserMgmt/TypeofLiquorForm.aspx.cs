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
    public partial class TypeofLiquorForm : System.Web.UI.Page
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
            Response.Redirect("~/TypeofLiquorList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var liquor = new TypeOfLiquor()
            {
                type_of_liquor_name = txtLiquorName.Text,
            };
            if (Request.QueryString.AllKeys.Contains("QMode"))
            {
                liquor.type_of_liquor_id = Convert.ToInt32(Request.QueryString.Get("QId"));
            }
            try
            {
                var isDistillerySaved = Request.QueryString.AllKeys.Contains("QMode") ? masterDataService.UpdateTypeOfLiquor(liquor) : masterDataService.SaveTypeOfLiquor(liquor);
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
                Response.Redirect("~/TypeofLiquorList.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/TypeofLiquorList.aspx");
        }

        #region Private Methods 
        private void LoadDetails(int key)
        {
            TypeOfLiquor liquors = masterDataService.LoadTypeOfLiquorDetails(key);
            if (liquors == null)
            {
                throw new ArgumentNullException();
            }
            txtLiquorName.Text = liquors.type_of_liquor_name;
        }
        #endregion
    }
}