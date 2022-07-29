using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class PropertyForm : System.Web.UI.Page
    {
        static string _party_code ;
       // List<Property> Property = new List<Property>();
     static cm_seiz_Property property = new cm_seiz_Property();
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {
                

                string strPreviousPage = "";
                if (Request.UrlReferrer != null)
                {

                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                }
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/LoginPage");
                }
             
                Session["UserID"] = Session["UserID"];
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());

                //party_code.Value= user.party_code.ToString();
                // _party_code = party_code.Value;
                
                List<cm_property_type> property1 = new List<cm_property_type>();
                property1= BL_cm_property_type.GetList();

                var list = from s in property1
                         
                           select s;
               ddlPropertyType.DataSource = list.ToList();
                ddlPropertyType.DataTextField = "product_type_name";
               ddlPropertyType.DataValueField = "product_type_code";
               ddlPropertyType.DataBind();
               ddlPropertyType.Items.Insert(0, "Select");
                List<ThanaMaster> Thana = new List<ThanaMaster>();
                Thana = BL_Property.GetThana();
                var list1 = from s in Thana
                        
                            select s;
               ddlThana.DataSource = list1.ToList();
                ddlThana.DataTextField = "thana_name";
                ddlThana.DataValueField = "thana_code";
                ddlThana.DataBind();
                ddlThana.Items.Insert(0, "Select");
            
                if (Session["UserID"].ToString() == "Admin")
                {
                  
                    btnSaveasDraft.Visible = true;
                    btnCancel.Visible = true;
               
                }
             
                else
                {
                    btnSaveasDraft.Visible = true;
                    btnCancel.Visible = true;
                }

                if (Session["rtype"].ToString() != "0")
                {
                    int _propertyId = Convert.ToInt32(Session["PropertyId"].ToString());
                    string b = Session["UserID"].ToString();
                    string seizureNo = Session["seizureNo"].ToString();
                    //string tableId = Session["tableId"].ToString();
                    property = new cm_seiz_Property();

                    // Property = new List<Property>();
                    property = BL_Property.GetDetails(b, Convert.ToInt32(_propertyId));
                    ddlPropertyType.SelectedValue = property.property_type_code.ToString();
                    txtPropertyAddress.Text = property.propertyaddress;
                    txtLocation.Text = property.propertylocation;
                    txtLandmark.Text = property.propertylandmark;
                    txtCircle.Text = property.propertycriclename;
                    txtKhasraNo.Text = property.propertykhasrano;
                    txtKhataNo.Text = property.propertykhatano;
                    txtMobileNo.Text = property.contactno;
                    txtMouzaName.Text = property.propertymauzaname;
                    txtOwnerName.Text = property.ownername;
                   //ddlThana.SelectedValue = property.propertythanano;
                    txtThana.Text = property.propertythanano;
                    txtThana.Text = property.propertythanano;
                    txtOwnerPermanentAddress.Text = property.permanentaddress;
                    txtOwnerPresentAddress.Text = property.presentaddress;

                    if ((Session["rtype"].ToString() == "1"))
                    {
                        chk.Visible = false;
                        btnCancel.Visible = false;
                        btnSaveasDraft.Visible = false;
                        ddlPropertyType.Enabled = false;
                        txtOwnerPresentAddress.ReadOnly = true;
                        txtOwnerPermanentAddress.ReadOnly = true;
                        txtMobileNo.ReadOnly = true;
                        txtLandmark.ReadOnly = true;
                        txtKhataNo.ReadOnly = true;
                        txtKhasraNo.ReadOnly = true;
                        txtCircle.ReadOnly = true;
                        txtMouzaName.ReadOnly = true;
                        txtOwnerName.ReadOnly = true;
                        // ddlThana.Enabled = false;
                        txtThana.ReadOnly = true;
                        txtPropertyAddress.ReadOnly = true;
                        txtLocation.ReadOnly = true;
                        //idupDocument.Enabled = false;
                        //txtDiscription.Enabled = false;
                        //btnUpload.Enabled = false;

                    }
                }
            }

        }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PropertyList");
        }
        protected void btnBasicInformation_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("BasicIformationForm");

        }

        protected void btnEAS_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ExcisableArticlesSeizedList");
        }

        protected void btnOtherExisable_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("VehicleList");

        }
        protected void btnRaidTeam_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("RaidTeamList");

        }
        protected void btnAccusedDetails_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("AccusedDetailsList");
        }

        protected void btnVehicle_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("VehicleList");
        }
        protected void btnApparatus_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ApparatusList");

        }
        protected void btnProperty_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PropertyList");

        }
        protected void btnMoney_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("MoneyList");
        }



        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                btnSaveasDraft.Enabled = false;
                cm_seiz_Property property = new cm_seiz_Property();
                property.seizureno = Convert.ToInt32(Session["seizureNo"].ToString());
                property.property_type_code = ddlPropertyType.SelectedValue.ToString();
                property.seizureno = Convert.ToInt32(Session["seizureNo"].ToString());
                property.propertyaddress = txtPropertyAddress.Text;
                property.propertylocation = txtLocation.Text;
                property.propertylandmark = txtLandmark.Text;
                property.propertycriclename = txtCircle.Text;
                property.propertymauzaname = txtMouzaName.Text;
                property.propertykhatano = txtKhataNo.Text;
                property.propertykhasrano = txtKhasraNo.Text;
                // property.propertythanano = ddlThana.SelectedValue;
                property.propertythanano = txtThana.Text;
                property.user_id= Session["UserID"].ToString();
                property.ownername = txtOwnerName.Text;
                property.contactno = txtMobileNo.Text;
                property.presentaddress = txtOwnerPresentAddress.Text;
                property.permanentaddress = txtOwnerPermanentAddress.Text;
                property.record_status ="N";
                property.raidby = Session["RaidBy"].ToString().Substring(0, 1);
                string strHostName = System.Net.Dns.GetHostName();
                string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                property.ipaddress = clientIPAddress.ToString();
                string val;
                string tempTableId = Session["tableId"]?.ToString() ?? string.Empty;
                if (Session["rtype"].ToString() == "0" && string.IsNullOrEmpty(tempTableId))
                    val = BL_Property.Insert(property);
                else
                {
                    property.seizure_propertydetails_id = Convert.ToInt32(base.Session["PropertyId"].ToString());
                    val = BL_Property.Update(property);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("PropertyList");
                }
                else
                {
                    btnSaveasDraft.Enabled = true;
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(val);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                }


            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PropertyList");
        }
    }
}