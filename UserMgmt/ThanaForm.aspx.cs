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
    public partial class ThanaForm : System.Web.UI.Page
    {
        private readonly IMasterDataService masterDataService = new MasterDataProvider();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadThana();
                if (Request.QueryString.AllKeys.Contains("QMode"))
                {
                    var key = Convert.ToInt32(Request.QueryString.Get("QId"));
                    LoadDetails(key);
                    ddlDistrictNames.Enabled = false;
                }
            }
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/ThanaList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var thana = new Thana()
            {
                district_code = ddlDistrictNames.SelectedValue,
                thana_name = txtThanaName.Text,
                created_by = Convert.ToString(Session["UserID"])
            };
            if (Request.QueryString.AllKeys.Contains("QMode"))
            {
                thana.thana_master_id = Convert.ToInt32(Request.QueryString.Get("QId"));
            }
            try
            {
                var isThanaSaved = Request.QueryString.AllKeys.Contains("QMode") ? masterDataService.UpdateThana(thana) : masterDataService.SaveThana(thana);
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
                Response.Redirect("~/ThanaList.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/ThanaList.aspx");
        }

        #region Private Methods
        private void LoadThana()
        {
            var districtList = masterDataService.DistrictList();
            ddlDistrictNames.DataSource = districtList;
            ddlDistrictNames.Items.Insert(0, new ListItem(string.Empty, string.Empty));
            ddlDistrictNames.SelectedIndex = 0;
            ddlDistrictNames.DataBind();
        }

        private void LoadDetails(int key)
        {
            Thana thana = masterDataService.LoadThanaDetails(key);
            if (thana == null)
            {
                throw new ArgumentNullException();
            }
            ddlDistrictNames.SelectedValue = thana.district_code;
            txtThanaName.Text = thana.thana_name;
        }
        #endregion
    }
}