using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class workflowForm : System.Web.UI.Page
    {
        List<WorkFlow> workflow = new List<WorkFlow>();
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
                dummytable.Visible = true;
                List<WorkFlow> SubModules = new List<WorkFlow>();
                SubModules = BL_WorkFlow.GetSubModules();
                ddlsubmodules.DataSource = SubModules;
                ddlsubmodules.DataTextField = "SubModule_name";
                ddlsubmodules.DataValueField = "Submodule_code";
                ddlsubmodules.DataBind();
                ddlsubmodules.Items.Insert(0, "Select");
                
                //List<WorkFlow> district = new List<WorkFlow>();
                //district = BL_WorkFlow.GetDistricts();
                //ddlDistrict.DataSource = district;
                //ddlDistrict.DataTextField = "district";
                //ddlDistrict.DataValueField = "district_Code";
                //ddlDistrict.DataBind();
                //ddlDistrict.Items.Insert(0, "Select");
                
                if (ViewState["Records"] == null)
                {
                    dt.Columns.Add("SubModule_Name");
                    dt.Columns.Add("SubModule_Code");
                    dt.Columns.Add("Tab_Name");
                    dt.Columns.Add("Tab_id");
                    dt.Columns.Add("Role_Name");
                    dt.Columns.Add("Role_Name_code");
                    dt.Columns.Add("District_Name");
                    dt.Columns.Add("District_Code");
                    dt.Columns.Add("User_Name");
                    dt.Columns.Add("User_registration_id");
                    dt.Columns.Add("Approver_Level");
                    dt.Columns.Add("Approver_Level1");
                    dt.Columns.Add("Action");
                    ViewState["Records"] = dt;
                }
                if (Session["rtype"].ToString() == "0")
                {
                    int n = BL_RolePermisions.GetMaxID("workflow");
                    txtid.Value = (n + 1).ToString();
                }
                else
                {
                    if (Session["rtype"].ToString() == "1")
                    {

                        ddlDistrict.Enabled = false;
                        ddlRoleName.Enabled = false;
                        ddlsubmodules.Enabled = false;
                        ddlTabnames.Enabled = false;
                        ddlUsers.Enabled = false;
                        btnAdd.Visible = false;
                        btnCancel.Visible = false;
                        btnSave.Visible = false;
                        ddlApproverlevel.Enabled = false;
                        grdAdd.Columns[grdAdd.Columns.Count - 1].Visible = false;
                    }

                    txtid.Value = Session["id"].ToString();
                   
                    
                    workflow = new List<WorkFlow>();
                    
                    workflow = BL_WorkFlow.Getworkflow(Convert.ToInt32(txtid.Value));
                    ddlsubmodules.SelectedValue = workflow[0].submodule_code;
                    ddlsubmodules_SelectedIndexChanged(sender, e);
                    ddlTabnames.SelectedValue = workflow[0].tab_id;
                   
                    ddlTabnames_SelectedIndexChanged(sender, e);
                    for (int i = 0; i < workflow.Count; i++)
                    {
                        dummytable.Visible = false;
                        dt = (DataTable)ViewState["Records"];
                        
                        ddlRoleName.SelectedValue = workflow[i].role_name_code;
                        ddlRoleName_SelectedIndexChanged(sender, e);
                        ddlDistrict.SelectedValue = workflow[0].district_code;
                        ddlDistrict_SelectedIndexChanged(sender, null);
                        ddlUsers.SelectedValue = workflow[i].user_registration_id;
                        dt.Rows.Add(ddlsubmodules.SelectedItem, ddlsubmodules.SelectedValue, ddlTabnames.SelectedItem, ddlTabnames.SelectedValue, ddlRoleName.SelectedItem.ToString(), ddlRoleName.SelectedValue, ddlDistrict.SelectedItem.ToString(), ddlDistrict.SelectedValue, ddlUsers.SelectedItem, ddlUsers.SelectedValue, workflow[i].approver_level);
                        grdAdd.DataSource = dt;
                        grdAdd.DataBind();
                    }
                    ddlUsers.SelectedIndex =0;
                    ddlRoleName.SelectedIndex =0;
                    ddlApproverlevel.SelectedIndex = 0;
                }
            }
        }
       
        protected void WorkFlowDetails_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("WorkFlowLIst");
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("WorkFlowLIst");
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
                        List<WorkFlow> workflows = new List<WorkFlow>();
                        int n =Convert.ToInt32( txtid.Value);
                        foreach (GridViewRow row in grdAdd.Rows)
                        {
                            WorkFlow workflow = new WorkFlow();
                          
                            workflow.submodule_code = (row.Cells[0].FindControl("lblSubModuleCode") as Label).Text.ToString();
                            workflow.tab_id= (row.Cells[0].FindControl("lbltab_id") as Label).Text.ToString();
                            workflow.role_name_code = (row.Cells[0].FindControl("lblRole_name_code") as Label).Text.ToString();
                            workflow.district_code = (row.Cells[0].FindControl("lblDistrict_Code") as Label).Text.ToString();
                            workflow.user_registration_id = (row.Cells[0].FindControl("lblUser_Registration_id") as Label).Text.ToString();
                            workflow.approver_level= (row.Cells[0].FindControl("lblApproverLevel1") as Label).Text.ToString();
                            workflow.id = n;
                            workflow.org_id = 1;
                            workflow.user_id = userid;
                            workflows.Add(workflow);
                            n++;
                          
                          
                        }
                        string VAL = BL_WorkFlow.InsertWorkFlow(workflows);
                        if (VAL!="1")
                        {
                            btnSave.Enabled = true;
                            string message1 = "Record is already exist in Database";
                            System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
                            sb1.Append("<script type = 'text/javascript'>");
                            sb1.Append("window.onload=function(){");
                            sb1.Append("alert('");
                            sb1.Append(message1);
                            sb1.Append("')};");
                            sb1.Append("</script>");
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb1.ToString());
                            Response.Redirect("WorkFlowLIst.aspx");
                            return;
                        }
                        if (VAL == "1")
                        {
                            btnSave.Enabled = true;
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
                            Response.Redirect("WorkFlowLIst.aspx");
                        }
                       
                    }
                    catch (Exception ex2)
                    {
                        btnSave.Enabled = true;
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
                    List<WorkFlow> workflows = new List<WorkFlow>();
                    int n = Convert.ToInt32(txtid.Value);
                    foreach (GridViewRow row in grdAdd.Rows)
                    {
                        WorkFlow workflow = new WorkFlow();
                        workflow.submodule_code = (row.Cells[0].FindControl("lblSubModuleCode") as Label).Text.ToString();
                        workflow.tab_id = (row.Cells[0].FindControl("lbltab_id") as Label).Text.ToString();
                        workflow.role_name_code = (row.Cells[0].FindControl("lblRole_name_code") as Label).Text.ToString();
                        workflow.district_code = (row.Cells[0].FindControl("lblDistrict_Code") as Label).Text.ToString();
                        workflow.user_registration_id = (row.Cells[0].FindControl("lblUser_Registration_id") as Label).Text.ToString();
                        workflow.approver_level = (row.Cells[0].FindControl("lblApproverLevel1") as Label).Text.ToString();
                        workflow.id = n;
                       
                        workflow.org_id = 1;
                        workflow.user_id = userid;
                        workflows.Add(workflow);
                        n++;
                    }
                    string VAL = BL_WorkFlow.UpdateWorkFlow(workflows);
                    Session["UserID"] = Session["UserID"].ToString();
                    Response.Redirect("WorkFlowLIst");
                }
                
            }
            else
            {
                btnSave.Enabled = true;
                string message = "Please Select the Details";
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("WorkFlowLIst");
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            ImageButton lb = (ImageButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            DataTable dt2 = (DataTable)ViewState["Records"];
            ViewState["CurrentTable"] = dt2;
            int rowID = gvRow.RowIndex;
            DataTable dt1 = ViewState["Records"] as DataTable;
            dt1.Rows[rowID].Delete();
            ViewState["dt"] = dt1;
            grdAdd.DataSource = dt1;
            grdAdd.DataBind();
            if (dt1.Rows.Count == 0)
            {
                dummytable.Visible = true;
            }
               
            
            
        }
        DataTable dt=new DataTable();
        string district, rolename, user, submodule, tab;

        protected void ddlTabnames_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlsubmodules.SelectedIndex != 0)
                {
                    List<WorkFlow> rolenames = new List<WorkFlow>();
                    rolenames = BL_WorkFlow.GetRoleNames();
                    ddlRoleName.DataSource = rolenames;
                    ddlRoleName.DataTextField = "role_name";
                    ddlRoleName.DataValueField = "role_name_code";
                    ddlRoleName.DataBind();
                    ddlRoleName.Items.Insert(0, "Select");
                }
            }
            catch
            {

            }
           
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlRoleName.SelectedValue != "Select")
                {
                    List<UserDetails> usernames = new List<UserDetails>();
                    usernames = BL_UserDetails.GetUserList("");
                    var users = from s in usernames
                                where s.role_name_code == Convert.ToInt32(ddlRoleName.SelectedValue) && (s.district_code == ddlDistrict.SelectedValue || s.district_code == "AL")
                                select s;
                    ddlUsers.DataSource = users;
                    ddlUsers.DataTextField = "User_name";
                    ddlUsers.DataValueField = "id";
                    ddlUsers.DataBind();
                    ddlUsers.Items.Insert(0, "Select");
                }
                else
                {
                    ddlRoleName.Focus();
                    string message = "Please Select the Role Name";
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
            catch
            {

            }
        }

        protected void grdAdd_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;
            dummytable.Visible = false;
            dt = (DataTable)ViewState["Records"];
            int n = 0;
            int row =dt.Rows.Count+1;
            workflow = new List<WorkFlow>();
            workflow = BL_WorkFlow.Checkworkflow(ddlsubmodules.SelectedValue.ToString(),ddlTabnames.SelectedValue,ddlRoleName.SelectedValue,ddlDistrict.SelectedValue,ddlUsers.SelectedValue);
            if (workflow.Count==0 || ddlDistrict.SelectedValue=="AL")
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["SubModule_name"].ToString() != ddlsubmodules.SelectedItem.ToString() || dr["Tab_name"].ToString() != ddlTabnames.SelectedItem.ToString())
                    {
                        n = 2;
                        string message = "SubModule name,Tab name or District Name Not matched with the grid data...!!! ";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                        break;
                    }
                    if (dr["SubModule_name"].ToString() == ddlsubmodules.SelectedItem.ToString() && dr["Tab_name"].ToString() == ddlTabnames.SelectedItem.ToString() && dr["role_name"].ToString() == ddlRoleName.SelectedItem.ToString())
                    {
                        n = 1;
                        string message = "Record Already Added to grid...!!!";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                        break;
                    }
                    if (dr["approver_level"].ToString() == ddlApproverlevel.SelectedItem.ToString())
                    {
                        n = 1;
                        string message = "Approver Level Already Added to grid...!!!";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                        break;
                    }
                }
                if (n == 0)
                {
                    dt.Rows.Add(ddlsubmodules.SelectedItem, ddlsubmodules.SelectedValue, ddlTabnames.SelectedItem, ddlTabnames.SelectedValue, ddlRoleName.SelectedItem.ToString(), ddlRoleName.SelectedValue, ddlDistrict.SelectedItem.ToString(), ddlDistrict.SelectedValue, ddlUsers.SelectedItem, ddlUsers.SelectedValue, ddlApproverlevel.SelectedItem, ddlApproverlevel.SelectedItem);
                    grdAdd.DataSource = dt;
                    grdAdd.DataBind();
                    if (row == 1)
                    {
                        tab = ddlTabnames.Text;
                        submodule = ddlsubmodules.Text;
                        rolename = ddlRoleName.Text;
                        user = ddlUsers.Text;
                        district = ddlDistrict.Text;
                    }
                    ddlsubmodules.SelectedIndex = 0;
                    ddlTabnames.SelectedIndex = 0;
                    ddlRoleName.SelectedIndex = 0;
                    ddlDistrict.SelectedIndex = 0;
                    ddlUsers.SelectedIndex = 0;

                }
            }
            else
            {
                if (Session["rtype"].ToString() == "0")
                {
                    string message = "SubModule name,Tab name , District Name ,role_name matched with the existing data...!!! ";
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
                    dt.Rows.Add(ddlsubmodules.SelectedItem, ddlsubmodules.SelectedValue, ddlTabnames.SelectedItem, ddlTabnames.SelectedValue, ddlRoleName.SelectedItem.ToString(), ddlRoleName.SelectedValue, ddlDistrict.SelectedItem.ToString(), ddlDistrict.SelectedValue, ddlUsers.SelectedItem, ddlUsers.SelectedValue, ddlApproverlevel.SelectedItem, ddlApproverlevel.SelectedItem);
                    grdAdd.DataSource = dt;
                    grdAdd.DataBind();
                    if (row == 1)
                    {
                        tab = ddlTabnames.Text;
                        submodule = ddlsubmodules.Text;
                        rolename = ddlRoleName.Text;
                        user = ddlUsers.Text;
                        district = ddlDistrict.Text;
                    }
                    ddlsubmodules.SelectedIndex = 0;
                    ddlTabnames.SelectedIndex = 0;
                    ddlRoleName.SelectedIndex = 0;
                    ddlDistrict.SelectedIndex = 0;
                    ddlUsers.SelectedIndex = 0;
                }
            }
            btnAdd.Enabled = true;
        }

        protected void ddlsubmodules_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlsubmodules.SelectedIndex != 0)
                {
                    List<WorkFlow> tabnames = new List<WorkFlow>();
                    tabnames = BL_WorkFlow.GetTabNames(ddlsubmodules.SelectedValue);
                    ddlTabnames.DataSource = tabnames;
                    ddlTabnames.DataTextField = "tab_name";
                    ddlTabnames.DataValueField = "tab_id";
                    ddlTabnames.DataBind();
                    ddlTabnames.Items.Insert(0, "Select");
                }
            }
            catch
            {

            }
           
        }
       // int n = 1;
        protected void ddlRoleName_SelectedIndexChanged(object sender, EventArgs e)
        {
           try
            {
                if (ddlRoleName.SelectedIndex != 0)
                {
                    List<UserDetails> usernames = new List<UserDetails>();
                    usernames = BL_UserDetails.GetUserList("");
                    var users = (from s in usernames
                                 where s.role_name_code == Convert.ToInt32(ddlRoleName.SelectedValue)
                                 select new { District_name= s.district_name, District_code= s.district_code }).Distinct();

                    ddlDistrict.DataSource = users.Distinct();
                    ddlDistrict.DataTextField = "District_name";
                    ddlDistrict.DataValueField = "District_code";
                    ddlDistrict.DataBind();
                    ddlDistrict.Items.Insert(0, "Select");
                }
            }
            catch
            {

            }
        }
        
    }
}