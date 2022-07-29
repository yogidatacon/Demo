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
    public partial class ModuleMasterForm : System.Web.UI.Page
    {
        List<Org_Master> org_master = new List<Org_Master>();
        public static string existingmodulename;
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
                org_master = new List<Org_Master>();
                org_master = BL_org_Master.GetListValues(Session["UserID"].ToString());
                var org_master1 = from s in org_master
                                   where s.status =="Active"
                                   select s;
                ddOrgNmae.DataSource = org_master1.ToList();
                ddOrgNmae.DataTextField = "org_name";
                ddOrgNmae.DataValueField = "org_id";
                ddOrgNmae.DataBind();
                ddOrgNmae.Items.Insert(0, "Select");
               
                if (Session["rtype"].ToString() != "0")
                {
                    ddOrgNmae.SelectedValue = Session["org_id"].ToString();
                    txtModuleCode.Text = Session["module_code"].ToString();
                    txtModuleName.Text = Session["module_name"].ToString();
                    existingmodulename = Session["module_name"].ToString();
                    List<SubModule_Master> smodule = new List<SubModule_Master>();
                    smodule = BL_SubModule_Master.GetList();
                    var list = from s in smodule
                               where s.mns_no == Session["mns_no"].ToString()
                               select s;
                    // moduleid.Value = Session["msn_no"].ToString();
                    if (Session["rtype"].ToString() == "1" ||list.ToList().Count>0)
                    {
                        txtModuleCode.ReadOnly = true;
                        txtModuleName.ReadOnly = true;
                        ddOrgNmae.Enabled= false;
                        btnCancel.Visible = false;
                        btnSave.Visible = false;
                    }
                    else
                    {
                        txtModuleCode.ReadOnly = true;
                    }
                }

            }
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ModuleMasterList");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ModuleMasterList");
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
        [WebMethod]
        public static string chkDuplicatemodulecode(Object modulecode)
        {
            int value = BL_User_Mgnt.GetExistsData("module_master", "module_code", modulecode.ToString().ToUpper());
            return value.ToString();
        }
        [WebMethod]
        public static string chkDuplicatemodulename(Object modulename)
        {
            int value = 0;
            if (existingmodulename != modulename.ToString())
            {
                string s = modulename.ToString();
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                value = BL_User_Mgnt.GetExistsData("module_master", "module_name", modulename.ToString());
            }
            return value.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                Module_Master module = new Module_Master();
                module.org_id = ddOrgNmae.SelectedValue;
                module.module_code = txtModuleCode.Text.ToUpper();
                string s = txtModuleName.Text;
                s = Regex.Replace(s, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                module.module_name = s;
                string n = BL_org_Master.GetMaxID("Module_master").ToString();
                moduleid.Value = (n + 1).ToString();
                module.mns_no = moduleid.Value;
                string val;
                if (Session["rtype"].ToString() == "0")
                    val = BL_Module_Master.Insert(module);
                else
                    val = BL_Module_Master.Update(module);
                if(val=="0")
                {
                    Session["UserID"] = Session["UserID"].ToString();
                    Response.Redirect("ModuleMasterList");
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
    }
}