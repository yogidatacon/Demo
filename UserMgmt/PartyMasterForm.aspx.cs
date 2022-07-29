using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class PartyMasterForm : System.Web.UI.Page
    {
      public static Party_Master party = new Party_Master();
        List<Party_Type_Master> partytypes = new List<Party_Type_Master>();
        List<District> districts = new List<District>();
        public static string rtype;
        public static string org_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string strPreviousPage = "";
                isgrain.Visible = false;
                if (Request.UrlReferrer != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                }
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/LoginPage");
                }
                GetDropDownListValues();
                rtype = Session["rtype"].ToString();
                captive.Visible = false;
                
                if (Session["rtype"].ToString()!="0")
                {
                    List<Org_Master> org_master = new List<Org_Master>();
                    org_master = BL_org_Master.GetListValues("");
                  org_id = org_master[0].org_id;

                    List<VAT_Master> party1 = new List<VAT_Master>();
                    party1 = BL_VATMaster.GetvatmasterList("");
                    var list = from s1 in party1
                               where s1.party_code == Session["Party_Code"].ToString()
                               select s1;
                    party = new Party_Master();
                    party = BL_Party_Master.GetPartyDetails(Session["Party_Code"].ToString());
                    ddlPartyType.SelectedValue = party.party_type_code;
                    ddlPartyType_SelectedIndexChanged(sender, e);
                    txtpartyname.Text = party.party_name;
                    txtPartyCode.Text = party.party_code;
                    txtLicenseNo.Text = party.party_license_no;
                    txtPartyAddress.Text = party.party_address;
                    txtMobile.Text = party.mobile_no;
                    ddlactive.SelectedValue = party.party_active;
                    ddlDistrict.SelectedValue = party.district_code;
                    ddlCaptive.SelectedValue = party.party_captive;
                    txtFinanceyear.Text = party.financialyear;
                    txtGST.Text = party.gst;
                    txtEmail.Text = party.email_id;
                    textPan.Text = party.pan;
                    textTan.Text = party.tan;
                    textTin.Text = party.tin;
                    if (party.isgrainbased=="true"|| party.isgrainbased == "True")
                    CheckBox1.Checked =true;
                    
                    if (ddlCaptive.SelectedValue == "True")
                    {
                        ddlCaptive_SelectedIndexChanged(sender, e);
                        ddlCaptiveunitname.SelectedValue = party.party_captive_unit_name;
                        if (party.party_captive_unit_name != "")
                        {
                            captive.Visible = true;
                        }
                    }
                   
                    if (Session["rtype"].ToString() == "1" || list.ToList().Count>0)
                    {
                        ddlPartyType.Enabled = false;
                        txtLicenseNo.ReadOnly = true;
                        txtMobile.ReadOnly = true;
                        txtPartyAddress.ReadOnly = true;
                        txtPartyCode.ReadOnly = true;
                        txtpartyname.ReadOnly = true;
                        //   ddlactive.Attributes.Add("Disabled", "Disabled");
                        ddlCaptive.Enabled = false;
                        ddlCaptiveunitname.Enabled = false;
                        ddlDistrict.Enabled = false;
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                      
                    }
                    else
                    {
                        txtPartyCode.ReadOnly = true;
                    }
                }
                else
                {
                    //partymasters = new List<Party_Master>();
                    //partymasters = BL_Party_Master.GetList();
                    //var partynames = from s in partymasters
                    //                 where s.party_code != "ALL"
                    //                 select s;
                }
            }

        }

        private void GetDropDownListValues()
        {
            partytypes = new List<Party_Type_Master>();
            partytypes = BL_Party_Type_Master.GetList();
            var partynames = from s in partytypes
                             where s.party_type_code != "ALL" && s.party_type_code != "All"
                             select s;
            ddlPartyType.DataSource = partynames.ToList();
            ddlPartyType.DataTextField = "Party_Type_Name";
            ddlPartyType.DataValueField = "Party_Type_Code";
            ddlPartyType.DataBind();
            ddlPartyType.Items.Insert(0, "Select");
            districts = new List<District>();
            districts = BL_User_Mgnt.GetDistricts("");
            ddlDistrict.DataSource = districts;
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_Code";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, "Select");

           
        }

        protected void partymaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("PartyMasterList");
        }
        protected void productmaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ProductMasterList");
        }

        protected void vatmaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATMasterList");
        }
      
        protected void partytypemaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("PartyTypeMasterList");
        }
        protected void uommaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("UOMMasterList");
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("PartyMasterList");
        }
        protected void DispatchTypeMaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DispatchTypeMasterList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(IsPostBack)
            {

                party = new Party_Master();
                string s = txtpartyname.Text.ToString();
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                party.party_name =s;
                party.party_code = txtPartyCode.Text.ToUpper();
                party.party_type_code = ddlPartyType.SelectedValue;
                party.party_license_no = txtLicenseNo.Text;
                party.party_address = txtPartyAddress.Text;
                party.mobile_no = txtMobile.Text;
                party.district_code = ddlDistrict.SelectedValue;
                party.gst = txtGST.Text;
                party.tan = textTan.Text;
                party.tin = textTin.Text;
                party.pan = textPan.Text;
                party.org_id =Convert.ToInt32(org_id);
                party.email_id = txtEmail.Text;
                if (CheckBox1.Checked == true)
                    party.isgrainbased = "true";
                else
                    party.isgrainbased = "false";
                party.party_captive = ddlCaptive.SelectedValue;
                party.financialyear = txtFinanceyear.Text;
                party.party_active = ddlactive.SelectedValue;
                party.user_id = Session["UserID"].ToString();
                if (ddlCaptiveunitname.SelectedValue == "Select")
                    party.party_captive_unit_name = "";
                else
                    party.party_captive_unit_name = ddlCaptiveunitname.SelectedValue;
                string val;
                if (Session["rtype"].ToString()=="0")
                 val = BL_Party_Master.Insert(party);
                else
                    val = BL_Party_Master.Update(party);
                if (val=="0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("PartyMasterList");
                }
                else
                {
                    string message = val;
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                }
            }
        }
        [WebMethod]
        public static string chkDuplicateEmailData(Object email_id)
        {
            // IEnumerable myList = email_id as IEnumerable;
            int value = BL_User_Mgnt.GetExistsData("party_master", "email_id", email_id.ToString());
            return value.ToString();
        }
        [WebMethod]
        public static string GetFinancialYear(Object partytype)
        {
            string value="";
            List<Financial_Years> financial_years = new List<Financial_Years>();
            financial_years = BL_Financial_Years.GetFinacListValues();
            var year = (from s in financial_years
                        where s.party_type_code == partytype.ToString() && s.status == "Y"
                        select s);
            if(year.ToList().Count>0)
                value = year.ToList()[0].financial_year.ToString();
            return value.ToString();
        }
        [WebMethod]
        public static string chkDuplicatePartyCode(Object partycode)
        {
            int value = BL_User_Mgnt.GetExistsData("party_master", "Party_code", partycode.ToString().ToUpper());
            return value.ToString();
        }
        [WebMethod]
        public static string chkDuplicateLicenseNo(Object licenseno)
        {
            int value = BL_User_Mgnt.GetExistsData("party_master", "party_license_no", licenseno.ToString());
            return value.ToString();
        }
        [WebMethod]
        public static string GetCaptivenamesList()
        {
            string value ="";
            List<Party_Master> partymasters = new List<Party_Master>();
            partymasters = BL_Party_Master.GetList();
            for(int i=0;i<partymasters.Count;i++)
            {
                if(i==0)
                {
                    value = partymasters[i].party_name;
                }
                else
                    value = value+"_"+ partymasters[i].party_name;

            }
            return value.ToString();
        }
        [WebMethod]
        public static string chkDuplicatePartyName(Object partyname)
        {
            int value = 0;
            if (party.party_name != partyname.ToString())
            {
                string s = partyname.ToString();
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                value = BL_User_Mgnt.GetExistsData("party_master", "Party_name", s);
            }
            
            return value.ToString();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("PartyMasterList");
        }
        protected void partyfinancialyears_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PartyFinancialYearList");
        }
        protected void producttypemaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ProductTypeList");
        }
        protected void vattypemaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VatTypeMasterList");
        }
        protected void RawMaterialTypeMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialTypeMasterList");
        }

        protected void RawMaterial_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialList");
        }
        List<Party_Master> partymasters = new List<Party_Master>();
        protected void ddlPartyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                if (ddlPartyType.SelectedValue == "002"|| ddlPartyType.SelectedValue == "d01" || ddlPartyType.SelectedValue == "DIS")
                {
                    partymasters = new List<Party_Master>();
                    partymasters = BL_Party_Master.GetList();
                    var list = (from s in partymasters
                               where s.party_type_code != ddlPartyType.SelectedValue && s.party_active == "Active" && s.party_code!="All"
                               select s);
                    ddlCaptiveunitname.DataSource = list.ToList();
                    ddlCaptiveunitname.DataTextField = "Party_name";
                    ddlCaptiveunitname.DataValueField = "party_code";
                    ddlCaptiveunitname.DataBind();
                    ddlCaptiveunitname.Items.Insert(0, "Select");
                    ddlCaptive.Visible = true;
                    ddlCaptive.Enabled = true;
                //captive.Visible = true;
              
                
                    ddlCaptiveunitname.Visible = true;
                    ddlCaptive.SelectedValue = "Select";
                    ddlCaptive_SelectedIndexChanged(sender, e);
                    ddlCaptive.Attributes.Add("Enable", "Enable");
                    ddlCaptive.BackColor = System.Drawing.Color.LightYellow;
                List<Financial_Years> financial_years = new List<Financial_Years>();
                financial_years = BL_Financial_Years.GetFinacListValues();
                List<Org_Finacial_yr> Org_Finacial = new List<Org_Finacial_yr>();
                Org_Finacial = BL_org_Master.GetFinacListValues("");
                var year = from s in Org_Finacial
                                     where s.status == "Active"
                                     select s;
                //var year = (from s in financial_years
                //            where s.party_type_code ==ddlPartyType.SelectedValue && s.status == "Y"
                //            select s);
                if(year.ToList().Count>0)
                txtFinanceyear.Text = year.ToList()[0].financial_year.ToString();
            }
                else
                {
                if (ddlPartyType.SelectedValue == "ENA")
                {
                    isgrain.Visible = true;
                    CheckBox1.Visible = true;
                }
                else
                {
                    isgrain.Visible = false;
                    CheckBox1.Visible = false;
                }
                ddlCaptive.SelectedValue = "False";
                    ddlCaptive.Enabled = false;
              
                ddlCaptive.BackColor =System.Drawing.Color.Gray;
                    captive.Visible = false;
                    ddlCaptiveunitname.Visible = false;
                List<Financial_Years> financial_years = new List<Financial_Years>();
                financial_years = BL_Financial_Years.GetFinacListValues();
                var year = (from s in financial_years
                            where s.party_type_code == ddlPartyType.SelectedValue && s.status == "Y"
                            select s);
                if (year.ToList().Count > 0)
                    txtFinanceyear.Text = year.ToList()[0].financial_year.ToString();

            }
           
        }

        protected void ddlCaptive_SelectedIndexChanged(object sender, EventArgs e)
        {
           
                if(ddlCaptive.SelectedValue=="True")
                {
                if (ddlPartyType.SelectedValue == "002" || ddlPartyType.SelectedValue == "d01" || ddlPartyType.SelectedValue == "DIS")
                {
                    captive.Visible = true;
                    ddlCaptiveunitname.Visible = true;
                    isgrain.Visible = true;
                }
                else
                {

                    string message = "Cative Unit is not Applicable for Sugar Mills!";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    ddlCaptive.SelectedValue = "False";
                }
              

                }
               else if (ddlCaptive.SelectedValue == "Select")
                {
                    ddlCaptive.Enabled = true;
                   // ddlCaptiveunitname.Visible = true;
                }
                else
                    captive.Visible = false;
            }
       
    }
}