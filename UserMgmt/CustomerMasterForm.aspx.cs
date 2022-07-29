using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class CustomerMasterForm : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                    Session["UserID"] = Session["UserID"];
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    party_code.Value = user.party_code;
                    if (user.party_type != "All" || user.party_type != "ALL")
                    {

                        List<State> states = new List<State>();
                        states = BL_User_Mgnt.GetListValues(user.user_id);
                        ddlState.DataSource = states;
                        ddlState.DataTextField = "State_name";
                        ddlState.DataValueField = "State_code";
                        ddlState.DataBind();
                        ddlState.Items.Insert(0, "Select");
                        if (Session["rtype"].ToString() != "0")
                        {
                            CustomerDetails cust = new CustomerDetails();
                            cust = BL_User_Mgnt.GetCustomerDetails(Session["Customerid"].ToString());
                            txtCustomerAddress.Text = cust.cust_address;
                            txtMobileNumber.Text = cust.cust_mobile;
                            txtCustomerName.Text = cust.cust_name;
                            txtEmailID.Text = cust.cust_email;
                            party_code.Value = cust.party_code;
                            ddlState.SelectedValue = cust.state_code;
                            ddlState_SelectedIndexChanged(sender, null);
                            txtdistrict.Text = cust.district_name;
                            ddlDistrict_SelectedIndexChanged(sender, null);
                            txtThana.Text = cust.thana_name;
                            user.party_code = cust.party_code;
                            txtPINCode.Text = cust.pincode;
                            if(Session["rtype"].ToString() == "1")
                            {
                                txtCustomerAddress.ReadOnly = true;
                                txtMobileNumber.ReadOnly = true;
                                txtCustomerName.ReadOnly = true;
                                txtEmailID.ReadOnly = true;
                                ddlState.Enabled = false;
                                txtThana.ReadOnly = true;
                               // ddlState_SelectedIndexChanged(sender, null);
                                txtdistrict.ReadOnly =true;
                              //  ddlDistrict_SelectedIndexChanged(sender, null);
                                user.party_code = cust.party_code;
                                txtPINCode.ReadOnly = true;
                                btnCancel.Visible = false;
                                btnSubmit.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("~/User_Mgmt");
                    }
                }
                else
                {
                    Response.Redirect("~/User_Mgmt");
                }
            }
        }
        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("CustomerMasterList");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                CustomerDetails cust = new CustomerDetails();
                string AD = txtCustomerAddress.Text;
                AD = Regex.Replace(AD, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
               
                cust.cust_address =AD;
                cust.cust_mobile = txtMobileNumber.Text;
                string CM = txtCustomerName.Text;
                CM = Regex.Replace(CM, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                cust.cust_name = CM;
                cust.cust_email = txtEmailID.Text;
                string DIST = txtdistrict.Text;
                DIST = Regex.Replace(DIST, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                cust.district_code = DIST;
                string THANA = txtThana.Text;
                THANA = Regex.Replace(THANA, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                cust.thana_code = THANA;
                cust.state_code = ddlState.SelectedValue;
                cust.party_code = party_code.Value;
                cust.pincode = txtPINCode.Text;
                cust.user_id = Session["UserID"].ToString();
                if (Session["rtype"].ToString() != "0")
                    cust.customer_id = Session["Customerid"].ToString();
                string val = BL_User_Mgnt.InsertCust(cust);
                Session["UserID"] = Session["UserID"];
                Response.Redirect("CustomerMasterList");

            }
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            //List<District> districts = new List<District>();
            //districts = BL_User_Mgnt.GetDistricts(Session["UserID"].ToString());
            //var list = from s in districts
            //           where s.state_Code ==ddlState.SelectedValue
            //           select s;
            //ddlDistrict.DataSource = list;
            //ddlDistrict.DataTextField = "District_name";
            //ddlDistrict.DataValueField = "District_code";
            //ddlDistrict.DataBind();
            //ddlDistrict.Items.Insert(0, "Select");
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            //List<Thana_Details> thana = new List<Thana_Details>();
            //thana = BL_User_Mgnt.GetThanaList(Session["UserID"].ToString());
            //var list = from s in thana
            //           where s.state_code == ddlState.SelectedValue && s.district_code==ddlDistrict.SelectedValue
            //           select s;
            //ddlThana.DataSource = list;
            //ddlThana.DataTextField = "thana_name";
            //ddlThana.DataValueField = "thana_code";
            //ddlThana.DataBind();
            //ddlThana.Items.Insert(0, "Select");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("CustomerMasterList");
        }
    }
}