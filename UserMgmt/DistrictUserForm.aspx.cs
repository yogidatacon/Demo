using System;
using System.Linq;
using System.Web.UI.WebControls;
using Usermngt.BL.MasterData;
using Usermngt.BL.Service;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class DistrictUserForm : System.Web.UI.Page
    {

        private readonly IMasterDataService masterDataService = new MasterDataProvider();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDistrict();
                LoadDepartment();
                if (Request.QueryString.AllKeys.Contains("QMode"))
                {
                    var key = Convert.ToInt32(Request.QueryString.Get("QId"));
                    LoadDetails(key);
                    txtUserName.Enabled = false;
                    ddlDepartmentName.Enabled = false;
                    ddlDistrictNames.Enabled = false;
                    //if (Request.QueryString.Get("QMode") == "View")
                    //{
                    //    ddlDepartmentName.Enabled = false;
                    //    ddlDistrictNames.Enabled = false;
                    //    txtUserName.Enabled = false;
                    //    txtPassword.Enabled = false;
                    //    txtFullName.Enabled = false;
                    //    txtEmailId1.Disabled = true;
                    //    txtEmailId2.Disabled = true;
                    //    txtEmailId3.Disabled = true;
                    //    txtMobileNo1.Disabled = true;
                    //    txtMobileNo2.Disabled = true;
                    //    txtMobileNo3.Disabled = true;
                    //}
                }
            }
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/DistrictUserList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var ditrictUser = new DistrictUser()
            {
                department_code = ddlDepartmentName.SelectedValue,
                district_name = ddlDistrictNames.SelectedItem.Text,
                district_code = ddlDistrictNames.SelectedValue,
                user_id = txtUserName.Text,
                user_password = txtPassword.Text,
                full_name = txtFullName.Text,
                email_id = txtEmailId1.Value,
                email_id2 = txtEmailId2.Value,
                email_id3 = txtEmailId3.Value,
                mobile_no = Convert.ToInt64(txtMobileNo1.Value),
                mobile_no2 = Convert.ToInt64(txtMobileNo2.Value),
                mobile_no3 = Convert.ToInt64(txtMobileNo3.Value),
                created_by = Convert.ToString(Session["UserID"])
            };
            if (Request.QueryString.AllKeys.Contains("QMode"))
            {
                ditrictUser.district_login_id = Convert.ToInt32(Request.QueryString.Get("QId"));
            }
            try
            {
                var isDistrictUserSaved = Request.QueryString.AllKeys.Contains("QMode") ? masterDataService.UpdateDistrictUser(ditrictUser) : masterDataService.SaveDistrictUser(ditrictUser);
                var alert = ($@"<script type = 'text/javascript'>
                                window.onload=function()
                                {{
                                     alert('{ isDistrictUserSaved.Item2}');
                                }};
                                </script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", alert);
                if (!isDistrictUserSaved.Item1) return;
                Session["UserID"] = Session["UserID"].ToString();
                Response.Redirect("~/DistrictUserList.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/DistrictUserList.aspx");
        }


        #region Private Methods
        private void LoadDistrict()
        {
            var districtList = masterDataService.DistrictList();
            ddlDistrictNames.DataSource = districtList;
            ddlDistrictNames.Items.Insert(0, new ListItem(string.Empty, string.Empty));
            ddlDistrictNames.SelectedIndex = 0;
            ddlDistrictNames.DataBind();
        }

        private void LoadDepartment()
        {
            var deprtmentList = masterDataService.DepartmentList();
            ddlDepartmentName.DataSource = deprtmentList;
            ddlDepartmentName.Items.Insert(0, new ListItem(string.Empty, string.Empty));
            ddlDepartmentName.SelectedIndex = 0;
            ddlDepartmentName.DataBind();
        }

        private void LoadDetails(int key)
        {
            DistrictUser districtUser = masterDataService.LoadDistrictUserDetails(key);

            if (districtUser == null)
            {
                throw new ArgumentNullException();
            }
            ddlDepartmentName.SelectedValue = districtUser.department_code;
           
            //Bhavin
            //ddlDistrictNames.SelectedValue = districtUser.district_code;
            ddlDistrictNames.SelectedItem.Text = districtUser.district_name;
            //End

            txtUserName.Text = districtUser.user_id;
            txtPassword.Text = districtUser.user_password;
            txtFullName.Text = districtUser.full_name;
            txtEmailId1.Value = districtUser.email_id;
            txtEmailId2.Value = districtUser.email_id2;
            txtEmailId3.Value = districtUser.email_id3;
            txtMobileNo1.Value = Convert.ToString(districtUser.mobile_no);
            txtMobileNo2.Value = Convert.ToString(districtUser.mobile_no2);
            txtMobileNo3.Value = Convert.ToString(districtUser.mobile_no3);

        }
        #endregion
    }
}