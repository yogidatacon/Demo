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
    public partial class DivisionForm : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        static string divis="";
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
                        dt.Columns.Add("DivisionName");
                        dt.Columns.Add("Division_Code");
                        dt.Columns.Add("State");
                        dt.Columns.Add("State_Code");
                        ViewState["Records"] = dt;
                    }
                }
                else
                {
                    List<District> dist = new List<District>();
                    dist = BL_User_Mgnt.GetDistricts("");
                    var list = from s in dist
                               where s.division_Code == Session["div_Code"].ToString()
                               select s;
                    if (list.ToList().Count > 0)
                    {
                        txtdivisionName.Attributes.Add("Disabled", "Disabled");
                        ddStatenames.Attributes.Add("Disabled", "Disabled");
                    }
                    txtDivisionCode.Text = Session["div_Code"].ToString();
                    txtid.Value = Session["div_id"].ToString();
                    txtdivisionName.Text= Session["div_name"].ToString();
                    lblStatecode.Value= Session["state_code"].ToString();
                    List<State> statelist = new List<State>();
                    statelist = BL_User_Mgnt.GetListValues(userid);
                    btnAdd.Visible = false;
                    grdAdd.Visible = false;
                    for (int n = 0; n < statelist.Count; n++)
                    {
                        ddStatenames.Items.Insert((n + 1), new ListItem(statelist[n].state_name, statelist[n].state_Code));
                    }
                    txtDivisionCode.Attributes.Add("disabled", "disabled");
                    ddStatenames.SelectedValue= lblStatecode.Value;
                    if (rtype == "1")
                    {
                        txtdivisionName.Attributes.Add("disabled", "disabled");
                        btnCancel.Visible = false;
                        btnSave.Visible = false;
                    }

                }
                
            }

        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("DivisionList");
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;
            dt = (DataTable)ViewState["Records"];
            int n = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if ((dr["DivisionName"].ToString() == txtdivisionName.Text && dr["State"].ToString() ==ddStatenames.SelectedItem.ToString()))
                {
                    n = 1;
                }
                if ((dr["Division_Code"].ToString() == txtDivisionCode.Text && dr["State"].ToString() == ddStatenames.SelectedItem.ToString()))
                {
                    n=2;
                }

            }
            if (n == 0)
            {
                string s = txtdivisionName.Text;
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                dt.Rows.Add(s, txtDivisionCode.Text.ToUpper(), ddStatenames.SelectedItem.ToString(),ddStatenames.SelectedValue.ToString());
                grdAdd.DataSource = dt;
                grdAdd.DataBind();
                txtDivisionCode.Text = "";
                txtdivisionName.Text = "";
                //ddStatenames.SelectedValue ="";
                ddStatenames.SelectedIndex = -1;
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

        protected void grdAdd_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
        }
        protected void ThanaMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ThanaMasterList");
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/DivisionList");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (grdAdd.Rows.Count > 0)
            {
                btnSave.Enabled = false;
                string userid = Session["UserID"].ToString();
                string rtype = Session["rtype"].ToString();
                if (rtype == "0")
                {
                    try
                    {
                        foreach (GridViewRow row in grdAdd.Rows)
                        {
                            Division division = new Division();
                            division.state_Code = (row.Cells[0].FindControl("lblStateCode") as Label).Text.ToString();
                            string s = (row.Cells[0].FindControl("lblDivisionName") as Label).Text.ToString();
                            s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                            division.Division_name = s;
                            division.Division_Code = (row.Cells[0].FindControl("lblDivisionCode") as Label).Text.ToString().ToUpper();
                            division.user_id = Session["UserID"].ToString();
                            BL_User_Mgnt.InsertDivision(division);
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
                        Response.Redirect("DivisionList");
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
                    Division division = new Division();
                    division.state_Code = ddStatenames.SelectedValue;
                    string s = txtdivisionName.Text;
                    s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                    division.Division_name = s;
                    division.Division_Code = txtDivisionCode.Text.ToUpper();
                    division.user_id = Session["UserID"].ToString();
                    division.id = txtid.Value;
                    if (BL_User_Mgnt.UpdatetDivision(division))
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

                        Response.Redirect("DivisionList");
                    }
                    else
                    {
                        btnSave.Enabled = true;
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
            {
                btnSave.Enabled = true;
                string message = "Please Enter the Details";
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
        [WebMethod]
        public static string chkDuplicateDivisionName(Object divisionname)
        {
            int value =0;

            if (divis != divisionname.ToString())
            {
                string s = divisionname.ToString();
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                value = BL_User_Mgnt.GetExistsData("Division_Master", "Division_Name", s);
            }
            return value.ToString();
        }
        [WebMethod]
        public static string chkDuplicateDivisionCode(Object divisioncode)
        {
          
            int value = BL_User_Mgnt.GetExistsData("Division_Master", "Division_Code", divisioncode.ToString().ToUpper());
            return value.ToString();
        }
        protected void txtdivisionName_TextChanged(object sender, EventArgs e)
        {
            //int value = BL_User_Mgnt.GetExistsData("Division_Master", "Division_Name", txtdivisionName.Text);
            //if (value > 0)
            //{
            //    string message = "Division Name  is Already Exists.";
            //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //    sb.Append("<script type = 'text/javascript'>");
            //    sb.Append("window.onload=function(){");
            //    sb.Append("alert('");
            //    sb.Append(message);
            //    sb.Append("')};");
            //    sb.Append("</script>");
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
            //    txtdivisionName.Text = "";
            //    txtdivisionName.Focus();
            //}
        }

        protected void txtDivisionCode_TextChanged(object sender, EventArgs e)
        {
            //int value = BL_User_Mgnt.GetExistsData("Division_Master", "Division_Code", txtDivisionCode.Text);
            //if (value > 0)
            //{
            //    string message = "Division Code  is Already Exists.";
            //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //    sb.Append("<script type = 'text/javascript'>");
            //    sb.Append("window.onload=function(){");
            //    sb.Append("alert('");
            //    sb.Append(message);
            //    sb.Append("')};");
            //    sb.Append("</script>");
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
            //    txtDivisionCode.Text = "";
            //    txtDivisionCode.Focus();
            //}

        }
    }
}