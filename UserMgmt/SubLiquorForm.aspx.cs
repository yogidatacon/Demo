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
    public partial class SubLiquorForm : System.Web.UI.Page
    {
        private readonly IMasterDataService masterDataService = new MasterDataProvider();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadLiquorTypes();
                if (Request.QueryString.AllKeys.Contains("QMode"))
                {
                    var key = Convert.ToInt32(Request.QueryString.Get("QId"));
                    LoadDetails(key);
                    ddlLiquorNames.Enabled = false;
                }
            }
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/SubLiquorList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var subLiquor = new SubLiquor()
            {
                type_of_liquor_id = Convert.ToInt32(ddlLiquorNames.SelectedValue),
                type_of_liquor_name = ddlLiquorNames.SelectedItem.Text,
                liquor_sub_name = txtSubLiquorName.Text,
            };
            if (Request.QueryString.AllKeys.Contains("QMode"))
            {
                subLiquor.liquor_sub_type_id = Convert.ToInt32(Request.QueryString.Get("QId"));
            }
            try
            {
                var isThanaSaved = Request.QueryString.AllKeys.Contains("QMode") ? masterDataService.UpdateSubLiquor(subLiquor) : masterDataService.SaveSubLiquor(subLiquor);
                if (!isThanaSaved.Item1)
                {
                    var alert = ($@"
                                    <script type='text/javascript'>
                                        window.onload=function()
                                        {{
                                             alert('{ isThanaSaved.Item2}');
                                        }};
                                    </script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", alert);
                    return;
                }
                Session["UserID"] = Session["UserID"].ToString();
                Response.Redirect("~/SubLiquorList.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/SubLiquorList.aspx");
        }

        #region Private Methods
        private void LoadLiquorTypes()
        {
            var liquorList = masterDataService.TypeOfLiquorList(Convert.ToString(Session["UserID"]));
            ddlLiquorNames.DataSource = liquorList;
            //ddlLiquorNames.Items.Insert(0, new ListItem(string.Empty, string.Empty));
            //ddlLiquorNames.SelectedIndex = 0;
            ddlLiquorNames.DataBind();
        }

        private void LoadDetails(int key)
        {
            SubLiquor subLiquor = masterDataService.LoadSubLiquorDetails(key);
            if (subLiquor == null)
            {
                throw new ArgumentNullException();
            }
            ddlLiquorNames.SelectedValue = Convert.ToString(subLiquor.type_of_liquor_id);
            txtSubLiquorName.Text = subLiquor.liquor_sub_name;
        }
        #endregion
    }
}