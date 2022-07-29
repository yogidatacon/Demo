using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class MasterReports : System.Web.UI.Page
    {
        List<RolePerssion_ViewModel> modules = new List<RolePerssion_ViewModel>();
       Reportmaster reportmaster = new Reportmaster();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                
                Session["UserID"] = Session["UserID"].ToString();
                  
                modules = new List<RolePerssion_ViewModel>();
                modules = BL_RolePermisions.GetRoleModelList();
                ddlModuleName.DataSource = modules;
                ddlModuleName.DataTextField = "Module_Name";
                ddlModuleName.DataValueField = "mns_no";
                ddlModuleName.DataBind();
                ddlModuleName.Items.Insert(0, "--Select--");
                if (Session["rtype"].ToString()== "0")
                {
                        List<Party_Type_Master> partytypes = new List<Party_Type_Master>();
                        partytypes = BL_Party_Type_Master.GetList();

                        ddpartytype.DataSource = partytypes;
                        ddpartytype.DataTextField = "Party_Type_Name";
                        ddpartytype.DataValueField = "Party_Type_Code";
                        ddpartytype.DataBind();
                        ddpartytype.Items.Insert(0, "Select");
                        int n = BL_RolePermisions.GetMaxID("Report_master");
                    txtid.Value = (n + 1).ToString();
                }
                else
                {
                    txtid.Value =Session["id"].ToString();
                    txtReportName.Text = Session["Reportname"].ToString();
                   // if (Session["status"].ToString()=="True")
                    ddlactive.SelectedValue = Session["status"].ToString();
                        List<Party_Type_Master> partytypes1 = new List<Party_Type_Master>();
                        partytypes1 = BL_Party_Type_Master.GetList();
                        ddpartytype.DataSource = partytypes1;
                        ddpartytype.DataTextField = "Party_Type_Name";
                        ddpartytype.DataValueField = "Party_Type_Code";
                        ddpartytype.DataBind();
                        ddpartytype.Items.Insert(0, "Select");
                        ddpartytype.SelectedValue= Session["partytype"].ToString();
                    //else
                    //    ddlactive.SelectedValue = "InActive";
                    ddlModuleName.SelectedValue = Session["module_name"].ToString();
                    txtreportfilename.Text = Session["reportfilename"].ToString();
                    //btnsave.Visible = false;
                    //tbnCancel.Visible = false;
                }
                }
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/LoginPage");
                }
            }
        }
      
        protected void btnsave_Click(object sender, EventArgs e)
        {
           
            reportmaster = new Reportmaster();
            reportmaster.id = Convert.ToInt32(txtid.Value);
            reportmaster.org_id = 1;
            reportmaster.role_name_code = 1;
            reportmaster.user_id = Session["UserID"].ToString();
            reportmaster.reportname = txtReportName.Text;
            reportmaster.reportfilename = txtreportfilename.Text;
            reportmaster.mns_no =Convert.ToInt32( ddlModuleName.SelectedValue);
            reportmaster.reportstatus = ddlactive.SelectedValue.ToString();
            reportmaster.partytype = ddpartytype.SelectedValue;
            //   reportmaster.report_path = "~/Report Project1/Report Project1/" + txtreportfilename.Text+".rdl";
            //if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(reportmaster.report_path)))
            //{
            //    string message1 = "Report Name Not Exists in Server...!!!";
            //    System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
            //    sb1.Append("<script type = 'text/javascript'>");
            //    sb1.Append("window.onload=function(){");
            //    sb1.Append("alert('");
            //    sb1.Append(message1);
            //    sb1.Append("')};");
            //    sb1.Append("</script>");
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb1.ToString());
            //    return;
            //}
            //else
            //{
            string val;
            if (Session["rtype"].ToString() == "0")
            {
                val = BL_WorkFlow.InsertReport(reportmaster);
            }
               
            else
            {
                reportmaster.id = Convert.ToInt32(Session["id"].ToString());
                val = BL_WorkFlow.UpdateReport(reportmaster);
            }
            if (val == "1")
                {
                    Session["UserID"] = Session["UserID"].ToString();
                    Response.Redirect("MasterReportsList");
                }
                else
                {
                    if (val == "2")
                    {
                        string message1 = "Report Name is Already Exists in this Module...!!!";
                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
                        sb1.Append("<script type = 'text/javascript'>");
                        sb1.Append("window.onload=function(){");
                        sb1.Append("alert('");
                        sb1.Append(message1);
                        sb1.Append("')};");
                        sb1.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb1.ToString());
                       // Response.Redirect("WorkFlowLIst.aspx");
                    }
                    else
                    {
                        string message1 = val;
                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
                        sb1.Append("<script type = 'text/javascript'>");
                        sb1.Append("window.onload=function(){");
                        sb1.Append("alert('");
                        sb1.Append(message1);
                        sb1.Append("')};");
                        sb1.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb1.ToString());
                       // Response.Redirect("WorkFlowLIst.aspx");
                    }
               // }
            }
        }

        protected void UserReport_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("MasterReportsList");
        }

       

        protected void tbnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("MasterReportsList");
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("MasterReportsList");
        }
    }
}