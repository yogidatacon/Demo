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
    public partial class SubModuleMasterForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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
                List<Org_Master>  org_master = new List<Org_Master>();
                org_master = BL_org_Master.GetListValues(Session["UserID"].ToString());
                var org_master1 = from s in org_master
                                  where s.status == "Active"
                                  select s;
                ddOrgNmae.DataSource = org_master1.ToList();
                ddOrgNmae.DataTextField = "org_name";
                ddOrgNmae.DataValueField = "org_id";
                ddOrgNmae.DataBind();
                ddOrgNmae.Items.Insert(0, "Select");
                
                if (Session["rtype"].ToString() != "0")
                {
                    
                   
                    ddOrgNmae.SelectedValue= Session["org_name"].ToString();
                    ddOrgNmae_SelectedIndexChanged(sender, e);
                    ddModuleNme.SelectedValue = Session["Module_name"].ToString();
                    txtSubModuleCode.Text = Session["submodule_code"].ToString();
                    txtSubModuleName.Text = Session["submodule_name"].ToString();
                    sub = Session["submodule_name"].ToString();
                    List<Tab_Master> tabs = new List<Tab_Master>();
                    tabs = BL_Tab_Master.GetList();
                    var list = from s in tabs
                               where s.submodule_code == Session["submodule_code"].ToString()
                               select s;
                    if (Session["rtype"].ToString() == "1" || list.ToList().Count>0)
                    {
                        txtSubModuleCode.ReadOnly = true;
                        txtSubModuleName.ReadOnly = true;
                        ddModuleNme.Enabled = false;
                        ddOrgNmae.Enabled = false;
                        btnCancel.Visible = false;
                        btnSave.Visible = false;
                    }
                    else
                    {
                        txtSubModuleCode.ReadOnly = true;
                    }
                }
            }
        }
        protected void ShowRecords_Click(object sender, EventArgs e)
        {

            Response.Redirect("SubModuleList.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SubModuleList.aspx");
        }
        protected void OrganisationDetails_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("OrgList");
        }

        protected void OrganisationFinancialYear_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("orgFinancialyrlist");
        }
        protected void Module_Master1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ModuleMasterList");
        }

        protected void Submodule_master1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("SubModuleList");
        }

        protected void tab_master1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("TabNameList");
        }
        static string sub;
        [WebMethod]
        public static string chkDuplicateSubmodulename(Object submodulename)
        {
            int value = 0;
            if (sub != submodulename.ToString())
            {
                string s = submodulename.ToString();
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                value = BL_User_Mgnt.GetExistsData("Submodule_master", "Submodule_name", submodulename.ToString());
            }
            return value.ToString();
        }
        [WebMethod]
        public static string chkDuplicateSubmodulecode(Object submodulecode)
        {
           
            int value = BL_User_Mgnt.GetExistsData("Submodule_master", "Submodule_code", submodulecode.ToString().ToUpper());
            return value.ToString();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                SubModule_Master submodule = new SubModule_Master();
                submodule.org_id = ddOrgNmae.SelectedValue;
                submodule.mns_no = ddModuleNme.SelectedValue;
                submodule.submodule_code = txtSubModuleCode.Text.ToUpper();
                submodule.submodule_name = txtSubModuleName.Text;
                string val;
                if (Session["rtype"].ToString() == "0")
                    val = BL_SubModule_Master.Insert(submodule);
                else
                    val = BL_SubModule_Master.Update(submodule);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"].ToString();
                    Response.Redirect("SubModuleList");
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

        protected void ddOrgNmae_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Module_Master> modules = new List<Module_Master>();
            modules = BL_Module_Master.GetList();
            var modules1 = from s in modules
                           where s.org_id ==ddOrgNmae.SelectedValue
                           select s;
            ddModuleNme.DataSource = modules1.ToList();
            ddModuleNme.DataTextField = "module_name";
            ddModuleNme.DataValueField = "mns_no";
            ddModuleNme.DataBind();
            ddModuleNme.Items.Insert(0, "Select");
        }
    }
}