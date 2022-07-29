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
    public partial class ThanaMasterForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string userid = Session["UserID"].ToString();
            Session["UserID"] = userid;


          
            if (!IsPostBack)
            {



                List<State> statelist = new List<State>();
                statelist = BL_User_Mgnt.GetListValues(userid);
                DDState.DataSource = statelist;
                DDState.DataTextField = "state_name";
                DDState.DataValueField = "state_code";
                DDState.DataBind();
                DDState.Items.Insert(0, "Select");

               


                string rtype = Session["rtype"].ToString();
                Session["rtype"] = rtype;

                if (rtype != "0")
                {


                    if (rtype == "1")
                    {
                        txtcode.Text = Session["thana_code"].ToString();
                        txtName.Text = Session["thana_name"].ToString();
                       
                        DDState.SelectedValue = Session["state_code"].ToString();
                        DDState_SelectedIndexChanged(sender, null);
                       
                        DDDivision.SelectedValue = Session["division_code"].ToString();
                        DDDivision_SelectedIndexChanged(sender, null);
                        ddDistrict.SelectedValue = Session["district_code"].ToString();
                        ddDistrict_SelectedIndexChanged(sender, null);
                        txtcode.Enabled = false;
                        txtName.Enabled = false;
                        ddDistrict.Enabled = false;
                        DDDivision.Enabled = false;
                        DDState.Enabled = false;
                        btnSave.Visible = false;
                        btnCancel.Visible = false;

                    }
                    else
                    {
                        txtcode.Text = Session["thana_code"].ToString();
                        txtName.Text = Session["thana_name"].ToString();
                        DDState.SelectedValue = Session["state_code"].ToString();
                        DDState_SelectedIndexChanged(sender, null);

                        DDDivision.SelectedValue = Session["division_code"].ToString();
                        DDDivision_SelectedIndexChanged(sender, null);
                        ddDistrict.SelectedValue = Session["district_code"].ToString();
                        ddDistrict_SelectedIndexChanged(sender, null);

                        txtcode.Enabled = false;
                        txtName.Enabled = true;
                        ddDistrict.Enabled = true;
                        DDDivision.Enabled = true;
                        DDState.Enabled = true;
                        btnSave.Visible = true;
                        btnCancel.Visible = true;
                    }
                }
            }

        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            //Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ThanaMasterList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string rtype = Session["rtype"].ToString();
            string user_id = Session["UserID"].ToString();
            if (rtype == "0")
            {
                ThanaMaster thana = new ThanaMaster();
                thana.thana_code = txtcode.Text;
                string s1 = txtName.Text;
                s1 = Regex.Replace(s1, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                thana.thana_name = s1;
                thana.district_code = ddDistrict.SelectedValue;
                thana.division_code = DDDivision.SelectedValue;
                thana.state_code = DDState.SelectedValue;
                thana.user_id = user_id;
                List<ThanaMaster> thanalist = new List<ThanaMaster>();
                thanalist = BL_Thana.GetThanaList(Session["UserID"].ToString());
                var org_master1 = from s in thanalist
                                  where s.division_code == DDDivision.SelectedValue && s.district_code == ddDistrict.SelectedValue && s.thana_name == s1.ToString()
                                  select s;
                if (org_master1.ToList().Count == 0)
                {
                    // dt.Rows.Add(txtcode.Text, txtName.Text, District.SelectedItem.ToString(), DDDivision.SelectedItem.ToString(), DDState.SelectedItem.ToString());
                    if (BL_Thana.InsertThana(thana))
                    {
                        string message = "Record is  Sucessfuly Submitted.";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());


                        Session["UserID"] = user_id;
                        Response.Redirect("ThanaMasterList.aspx");
                    }
                    else
                    {
                        string message = "Server Error";
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
                else
                {
                    string message = "Thana Name Already exists in this District";
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
            else
            {
                ThanaMaster thana = new ThanaMaster();
                thana.thana_code = txtcode.Text;
                string s1 = txtName.Text;
                s1 = Regex.Replace(s1, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                thana.thana_name = s1;
                thana.district_code = ddDistrict.SelectedValue;
                thana.division_code = DDDivision.SelectedValue;
                thana.state_code = DDState.SelectedValue;
                thana.user_id = user_id;
                thana.thana_master_id = txtid.Value;
                if (BL_Thana.UpdateThana(thana))
                {
                    string message = "Successfully Updated";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    Session["UserID"] = Session["UserID"].ToString();

                    Response.Redirect("ThanaMasterList.aspx");
                }
                else
                {
                    string message = "Server Side Error";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    return;
                }


            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ThanaMasterList.aspx");
        }
        protected void StateMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/StateList");
        }

        protected void DivisionMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/DivisionList");
        }

        protected void DistrictMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/Districtlist");

        }

        protected void RoleLevelMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/RoleLevelList");
        }

        protected void AccessTypeMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/AccessTypeList");
        }

        protected void RoleMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/RoleMasterList1");
        }
        protected void ThanaMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ThanaMasterList");
        }

        protected void ddDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (ddDistrict.SelectedValue != "Select")
                {
                    int n = BL_Thana.GetMax();
                    txtcode.Text = DDDivision.SelectedValue + ddDistrict.SelectedValue + string.Format("{0:000000}", n);
                txtcode.ReadOnly = true;
                }
           
        }

        protected void DDDivision_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (DDDivision.SelectedValue != "Select")
            {
                List<District> district = new List<District>();
                district = BL_User_Mgnt.GetDistricts("");
                var list = (from s in district
                            where s.division_Code == DDDivision.SelectedValue
                            
                            select s);
                ddDistrict.DataSource = list.ToList();
                ddDistrict.DataTextField = "district_name";
                ddDistrict.DataValueField = "district_code";
                ddDistrict.DataBind();
                ddDistrict.Items.Insert(0, "Select");
            }
        }

        protected void DDState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDState.SelectedValue != "Select")
            {
                List<Division> division = new List<Division>();
                division = BL_User_Mgnt.GetDivisions("");
                var list = (from s in division
                            where s.state_Code == DDState.SelectedValue
                            select s);
                DDDivision.DataSource = list.ToList();
                DDDivision.DataTextField = "division_name";
                DDDivision.DataValueField = "division_code";
                DDDivision.DataBind();
                DDDivision.Items.Insert(0, "Select");
            }
        }
    }
}