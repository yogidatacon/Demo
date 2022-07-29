using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class DistrictForm : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        static string district="";
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
                string userid = Session["UserID"].ToString();
                string rtype = Session["rtype"].ToString();
                Session["UserID"] = userid;
                //this.txtDistrictName. += new EventHandler(txtDistrictName_ServerChange);
                //this.txtDistrictCode.ServerChange += new EventHandler(txtDistrictCode_ServerChange);
                if (rtype == "0")
                {
                    List<State> statelist = new List<State>();
                    statelist = BL_User_Mgnt.GetListValues(userid);
                    for (int n = 0; n < statelist.Count; n++)
                    {
                        ddStatenames.Items.Insert((n + 1), new ListItem(statelist[n].state_name, statelist[n].state_Code));
                    }
                    
                    if (ViewState["Records"] == null)
                    {
                        dt.Columns.Add("DistrictName");
                        dt.Columns.Add("District_Code");
                        dt.Columns.Add("Division_Name");
                        dt.Columns.Add("Division_Code");
                        dt.Columns.Add("State_name");
                        dt.Columns.Add("State_Code");
                        dt.Columns.Add("tab_district_id");
                        dt.Columns.Add("id");
                        ViewState["Records"] = dt;
                    }
                }
                else
                {
                    List<UserDetails> users = new List<UserDetails>();
                    users = BL_UserDetails.GetUserList("");
                    var list = from s in users
                               where s.district_code == Session["District_Code"].ToString()
                               select s;
                    if (list.ToList().Count > 0)
                    {
                        txtDistrictCode.Attributes.Add("disabled", "disabled");
                        ddDivisions.Attributes.Add("disabled", "disabled");
                        ddStatenames.Attributes.Add("disabled", "disabled");
                        txtDistrictName.Attributes.Add("disabled", "disabled");
                        txtlabid.ReadOnly = true;
                    }
                        txtDistrictCode.Text = Session["District_Code"].ToString();
                    txtid.Value = Session["div_id"].ToString();
                    txtDistrictName.Text = Session["District_Name"].ToString();
                    lblStatecode.Value = Session["state_code"].ToString();
                    lblDivisionCode.Value = Session["div_code"].ToString();
                    if(Session["lab_id"].ToString()!="")
                    {
                        txtlabid.Text = Session["lab_id"].ToString();
                    }
                    else
                    {
                        txtlabid.Text = "";
                    }
                 
                    List<State> statelist = new List<State>();
                    statelist = BL_User_Mgnt.GetListValues(userid);
                    btnAdd.Visible = false;
                    grdAdd.Visible = false;
                    for (int n = 0; n < statelist.Count; n++)
                    {
                        ddStatenames.Items.Insert((n + 1), new ListItem(statelist[n].state_name, statelist[n].state_Code));
                    }
                    ddStatenames.SelectedValue = lblStatecode.Value;
                    List<Division> divisions = new List<Division>();
                    divisions = BL_User_Mgnt.GetDivisions(userid);
                    ddDivisions.Items.Clear();
                    ddDivisions.Items.Insert(0, new ListItem("Select", ""));
                    for (int n = 0; n < divisions.Count; n++)
                    {
                        ddDivisions.Items.Insert((n + 1), new ListItem(divisions[n].Division_name, divisions[n].Division_Code));
                    }
                    ddDivisions.SelectedValue = lblDivisionCode.Value;
                    if (rtype == "1")
                    {
                        txtDistrictCode.Attributes.Add("disabled", "disabled");
                        txtDistrictName.Attributes.Add("disabled", "disabled");
                        ddStatenames.Enabled = false;
                        ddDivisions.Enabled = false;
                        btnCancel.Visible = false;
                        btnSave.Visible = false;
                    }
                    else
                    {
                        txtDistrictCode.Attributes.Add("disabled", "disabled");
                    }

                }

            }
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("Districtlist");
        }
        protected void ThanaMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ThanaMasterList");
        }
        protected void StateMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("~/StateList");
        }

        protected void DivisionMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("~/DivisionList");
        }

        protected void DistrictMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("~/Districtlist");

        }

        protected void RoleLevelMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("~/RoleLevelList");
        }

        protected void AccessTypeMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("~/AccessTypeList");
        }

        protected void RoleMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("~/RoleMasterList1");
        }

        protected void ddDivisions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddStatenames_SelectedIndexChanged(object sender, EventArgs e)
        {
            string userid = Session["UserID"].ToString();
            List<Division> divisions = new List<Division>();
            divisions = BL_User_Mgnt.GetDivisions(userid);
            ddDivisions.Items.Clear();
            ddDivisions.Items.Insert(0, new ListItem("Select",""));
            for (int n = 0; n < divisions.Count; n++)
            {
                ddDivisions.Items.Insert((n + 1), new ListItem(divisions[n].Division_name, divisions[n].Division_Code));
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (grdAdd.Rows.Count > 0)
            {
                string userid = Session["UserID"].ToString();
                string rtype = Session["rtype"].ToString();
                DataTable dt1 = ViewState["Records"] as DataTable;
                btnSave.Enabled = false;


                if (rtype == "0")
                {
                    try
                    {
                        foreach (GridViewRow row in grdAdd.Rows)
                        {
                            District district = new District();
                            district.state_Code = (row.Cells[0].FindControl("lblStateCode") as Label).Text.ToString();
                            string s = (row.Cells[0].FindControl("lblDistrictName") as Label).Text.ToString();
                            s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                            district.district_Name = s;
                            district.district_Code = (row.Cells[0].FindControl("lblDistricCode") as Label).Text.ToString();
                            district.division_Code = (row.Cells[0].FindControl("lblDivisionCode") as Label).Text.ToString();
                            district.tab_district_id= (row.Cells[0].FindControl("lbllabid") as Label).Text.ToString();
                            district.user_id = Session["UserID"].ToString();
                            BL_User_Mgnt.InsertDistrict(district);
                        }
                        string message = "Saved Successfully ";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                        Session["UserID"] = Session["UserID"].ToString();
                        Response.Redirect("DistrictList");
                    }
                    catch (Exception ex2)
                    {
                        string message = ex2.Message;
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
                    District district = new District();
                    district.state_Code = ddStatenames.SelectedValue;
                    district.division_Code = ddDivisions.SelectedValue;
                    district.district_Code = txtDistrictCode.Text;
                    string s = txtDistrictName.Text;
                    s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                    district.district_Name =s;
                    district.tab_district_id = txtlabid.Text;
                    district.user_id = Session["UserID"].ToString();
                    district.id = txtid.Value;
                    string val = BL_User_Mgnt.UpdatetDistrict(district);
                    if (val=="1")
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

                        Response.Redirect("DistrictList");
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        if (val.Contains("duplicate"))
                        {
                            string message = "District Name/District Code are Already Exist...!!!";
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
            }
            else
            {
                District district = new District();
                district.state_Code = ddStatenames.SelectedValue;
                district.division_Code = ddDivisions.SelectedValue;
                district.district_Code = txtDistrictCode.Text;
                district.tab_district_id = txtlabid.Text;
                string s = txtDistrictName.Text;
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                district.district_Name = s;
                district.user_id = Session["UserID"].ToString();
                district.id = txtid.Value;
                string val=  BL_User_Mgnt.UpdatetDistrict(district);
                Session["UserID"] = Session["UserID"].ToString();
                Response.Redirect("DistrictList");
            }

        }
    

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("Districtlist");
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            DataTable dt2 = (DataTable)ViewState["Records"];
            ViewState["CurrentTable"] = dt2;
            int rowID = gvRow.RowIndex;
            DataTable dt1 = ViewState["Records"] as DataTable;
            dt1.Rows[rowID].Delete();
            ViewState["dt"] = dt1;
            grdAdd.DataSource = dt1;
            grdAdd.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;
            dt = (DataTable)ViewState["Records"];
            int n = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if ((dr["DistrictName"].ToString() == txtDistrictName.Text && (dr["Division_Name"].ToString() == ddDivisions.SelectedItem.ToString()) && (dr["State_name"].ToString() == ddStatenames.SelectedItem.ToString())))
                {
                    n = 1;
                }
                if ((dr["District_Code"].ToString() == txtDistrictCode.Text && (dr["Division_Name"].ToString() == ddDivisions.SelectedItem.ToString()) && (dr["State_name"].ToString() == ddStatenames.SelectedItem.ToString()) ))
                {
                    n = 2;
                }

            }
            if (n == 0)
            {
                if (txtDistrictCode.Text.Trim() == "" || txtDistrictName.Text.Trim() == "")
                {
                    string message = "Blank values are not .";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                }
                else
                {

                    string s = txtDistrictName.Text;
                    s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                    dt.Rows.Add(s, txtDistrictCode.Text.ToUpper(), ddDivisions.SelectedItem.ToString(), ddDivisions.SelectedValue.ToString(), ddStatenames.SelectedItem.ToString(), ddStatenames.SelectedValue.ToString(),txtlabid.Text);
                    grdAdd.DataSource = dt;
                    grdAdd.DataBind();
                    txtDistrictCode.Text = "";
                    txtDistrictName.Text = "";
                    //ddStatenames.SelectedValue ="";
                    ddStatenames.SelectedIndex = -1;
                    ddDivisions.SelectedIndex = -1;
                }
            }
            else
            {
                if (n == 1)
                {
                    string message = "Division name is Already Exists.";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                }
                else
                {
                    string message = "Division Code is Already Exists.";
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
            btnAdd.Enabled = true;
        }

        protected void txtDistrictName_ServerChange(object sender, EventArgs e)
        {
            int value = BL_User_Mgnt.GetExistsData("District_Master", "District_Name",txtDistrictName.Text);
            if(value>0)
            {
                string message = "DistrictName is Already Exists.";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append(message);
                sb.Append("')};");
                sb.Append("</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                txtDistrictName.Text = "";
                txtDistrictName.Focus();
            }
        }

        protected void txtDistrictCode_ServerChange(object sender, EventArgs e)
        {
            int value = BL_User_Mgnt.GetExistsData("District_Master", "District_Code", txtDistrictName.Text);
            if (value > 0)
            {
                string message = "District Code is Already Exists.";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append(message);
                sb.Append("')};");
                sb.Append("</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                txtDistrictName.Text = "";
                txtDistrictName.Focus();
            }
        }

        [WebMethod]
        public static string chkDuplicateDistrictName(Object districtname)
        {
            int value = 0;
            if (district != districtname.ToString())
            {
                string s = districtname.ToString();
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                value = BL_User_Mgnt.GetExistsData("District_Master", "District_Name",s);
            }
            return value.ToString();
        }
        [WebMethod]
        public static string chkDuplicateDistrictCode(Object districtcode)
        {
            int value = BL_User_Mgnt.GetExistsData("District_Master", "District_Code", districtcode.ToString().ToUpper());
            return value.ToString();
        }
    }
}