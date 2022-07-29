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
    public partial class TabNameMasterForm : System.Web.UI.Page
    {
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
                List<Org_Master> org_master = new List<Org_Master>();
                org_master = BL_org_Master.GetListValues(Session["UserID"].ToString());
                var org_master1 = from s in org_master
                                  where s.status == "Active"
                                  select s;
                ddOrgNmae.DataSource = org_master1.ToList();
                ddOrgNmae.DataTextField = "org_name";
                ddOrgNmae.DataValueField = "org_id";
                ddOrgNmae.DataBind();
                ddOrgNmae.Items.Insert(0, "Select");
                List<Module_Master> modules = new List<Module_Master>();
                modules = BL_Module_Master.GetList();
                ddModuleNme.DataSource = modules;
                ddModuleNme.DataTextField = "module_name";
                ddModuleNme.DataValueField = "mns_no";
                ddModuleNme.DataBind();
                ddModuleNme.Items.Insert(0, "Select");
                if (Session["rtype"].ToString() != "0")
                {
                    tab_name_id.Value = Session["tab_name_id"].ToString();
                    ddOrgNmae.Items.FindByText(Session["org_name"].ToString()).Selected = true;
                    ddModuleNme.Items.FindByText(Session["Module_name"].ToString()).Selected = true;
                    ddModuleNme_SelectedIndexChanged(sender, e);
                    string s = Session["submodule_name"].ToString();
                    ddSubModulName.Items.FindByText(Session["submodule_name"].ToString()).Selected = true;
                    txtTabName.Text = Session["tab_name"].ToString();
                    tab= Session["tab_name"].ToString(); ;
                    txtDescription.Text = Session["tab_desc"].ToString();
                    List<WorkFlow> tabs1 = new List<WorkFlow>();
                    tabs1 = BL_WorkFlow.Getworkflowlist("");
                    var list = from s1 in tabs1
                               where s1.tab_id == Session["tab_name_id"].ToString()
                               select s1;
                    if (Session["rtype"].ToString() == "1" || list.ToList().Count>0)
                    {
                        txtTabName.ReadOnly = true;
                        txtDescription.ReadOnly = true;
                        ddOrgNmae.Enabled = false;
                        ddModuleNme.Enabled = false;
                        ddSubModulName.Enabled = false;
                        btnCancel.Visible = false;
                        btnSave.Visible = false;
                    }
                 
                    
                }
            }

        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("TabNameList");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("TabNameList");
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
        static string tab;
        [WebMethod]
        public static string chkDuplicateTabname(Object tabname)
        {
            int value = 0;
            if (tab != tabname.ToString())
            {
                string s = tabname.ToString();
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                value = BL_User_Mgnt.GetExistsData("tabname_master", "tab_name", tabname.ToString());
            }
            return value.ToString();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Tab_Master tab = new Tab_Master();
                tab.org_id = ddOrgNmae.SelectedValue;
                tab.mns_no = ddModuleNme.SelectedValue;
                tab.submodule_code = ddSubModulName.SelectedValue;
                string s = txtTabName.Text.ToString();
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                tab.tab_name =s ;
                tab.tab_desc = txtDescription.Text;
              
                string val;
                if (Session["rtype"].ToString() == "0")
                    val = BL_Tab_Master.Insert(tab);
                else
                {
                    tab.tab_name_id = Convert.ToInt32(tab_name_id.Value);
                    val = BL_Tab_Master.Update(tab);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"].ToString();
                    Response.Redirect("TabNameList");
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

        protected void ddModuleNme_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                List<SubModule_Master> submodules = new List<SubModule_Master>();
                submodules = BL_SubModule_Master.GetList();
                var sub = from s in submodules
                          where s.mns_no == ddModuleNme.SelectedValue
                                 select s;
                ddSubModulName.DataSource = sub;
                ddSubModulName.DataTextField = "submodule_name";
                ddSubModulName.DataValueField = "submodule_code";
                ddSubModulName.DataBind();
                ddSubModulName.Items.Insert(0, "Select");
           
        }

        protected void ddOrgNmae_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Module_Master> modules = new List<Module_Master>();
            modules = BL_Module_Master.GetList();
            var modules1 = from s in modules
                           where s.org_id == ddOrgNmae.SelectedValue
                           select s;
            ddModuleNme.DataSource = modules1.ToList();
            ddModuleNme.DataTextField = "module_name";
            ddModuleNme.DataValueField = "mns_no";
            ddModuleNme.DataBind();
            ddModuleNme.Items.Insert(0, "Select");
        }
    }
}