using AjaxControlToolkit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class UserPermissions : System.Web.UI.Page
    {
        List<Org_Master> org_name = new List<Org_Master>();
        List<RoleMaster> roles = new List<RoleMaster>();
        List<RolePermissions> rolepermisions = new List<RolePermissions>();
        static List<RolePermissions> rolepermision = new List<RolePermissions>();
        List<RolePerssion_ViewModel> rolePerssion_ViewModel = new List<RolePerssion_ViewModel>();
        string userid;
        static string user_id;
        static int userpermissionid;
        static  int registrationid;
        static string role_name_code;
        protected void Page_Load(object sender, EventArgs e)
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
            user_id = Session["UserID"].ToString();
            Session["UserID"] = user_id;
            org_name = new List<Org_Master>();
            org_name = BL_org_Master.GetListValues(userid);
            ddorgnnames.DataSource = org_name;
            ddorgnnames.DataTextField = "org_name";
            ddorgnnames.DataValueField = "org_id";
            org_id.Value = org_name[0].org_id;
            ddorgnnames.DataBind();
            ddorgnnames.Enabled = false;
            ddorgnnames.BackColor = System.Drawing.Color.LightGray;
            roles = new List<RoleMaster>();
            roles = BL_User_Mgnt.GetRoleMasterList(userid);
            ddRolename.DataSource = roles;
            ddRolename.DataTextField = "rolename";
            ddRolename.DataValueField = "rolecode";
            ddRolename.DataBind();
            ddRolename.Items.Insert(0, "Select");
            CreateModelview();
            CreateMenus();
            int n = BL_RolePermisions.GetMaxID("user_permission");
            txtid.Value = (n + 1).ToString();
            userpermissionid =Convert.ToInt32( txtid.Value);
            if (Session["rtype"].ToString() == "0")
            {
               
            }
            else
            {

                userid = Session["User"].ToString();
                string val = Session["Role_name_code"].ToString();
              //  txtid.Value = userid.ToString();
              if (Session["Role_name_code"].ToString()!="Select" || Session["Role_name_code"].ToString() != "")
                ddRolename.SelectedValue= Session["Role_name_code"].ToString();
                ddRolename.Enabled = false;
                role_name_code= Session["Role_name_code"].ToString();
                ddorgnnames.SelectedValue =Session["org_id"].ToString();
                ddorgnnames.Enabled = false;
                registrationid = Convert.ToInt32(Session["registration_id"].ToString());
                txtuserid.Text = userid;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "BindUserPermissionData", "BindUserPermissionData()", true);
            }

        }
        [System.Web.Services.WebMethod]
        public static string GetUserPermissionData(string user_id)
        {
            rolepermision = new List<RolePermissions>();
        
            rolepermision = BL_RolePermisions.GetUserPermission(registrationid, role_name_code);

            return Newtonsoft.Json.JsonConvert.SerializeObject(rolepermision);
        }
        DataTable dt = new DataTable();
        private void CreateMenus()
        {
            #region Create Accordion with properties and css class

            Accordion ParentAccordion = new Accordion();
            ParentAccordion.ID = "MyParentAccordion";
            ParentAccordion.SelectedIndex = -1;//No default selection
            ParentAccordion.RequireOpenedPane = false;//no open pane
            ParentAccordion.HeaderCssClass = "ParentAccordionHeader";//Header class
            ParentAccordion.HeaderSelectedCssClass = "ParentAccordionHeaderSelected";//Selected herder class
            ParentAccordion.ContentCssClass = "ParentAccordionContent";//Content class

            #endregion



            #region Get the Distinct Branches

            var res = dt.AsEnumerable()
                        .GroupBy(r => r.Field<String>("Module_name")).Select(g => new
                        {

                            Module_name = g.First().Field<String>("Module_name"),
                            Module_id = g.First().Field<String>("mns_no")
                        });
            #endregion

            Label lbParentTitle; Label lbParentContent; AccordionPane ParentPane;
            AccordionPane ChildPane; Accordion ChildAccordion;
            int i = 0;

            foreach (var r in rolePerssion_ViewModel)
            {
                #region Create the Parent Pane with Data

                ParentPane = new AccordionPane();
                lbParentTitle = new Label();
                lbParentContent = new Label();
                ParentPane.ID = "Pane_" + i.ToString();
                lbParentTitle.Text = r.module_name;
                //  ParentPane.HeaderContainer.Controls.Add(lbParentTitle);
                StringBuilder str0 = new StringBuilder();
                str0.Append("<div runat=\"server\" id=\"Moduldiv_" + r.mns_no + "\" name=\"Moduldiv_" + r.mns_no + "\" class=\"class_Sub" + r.mns_no.ToString() + "\"style=\"left : 10px;\">");
                str0.Append("<label for=\"x\" style=\"width: 350px;\">");
                str0.Append("<input runat=\"server\" type =\"checkbox\" id=\"chk_" + r.mns_no.ToString() + "\" name=\"chk_" + r.mns_no.ToString() + "\" onclick=\"ModuleCheckAll('class_Sub" + r.mns_no.ToString() + "','chk_" + r.mns_no.ToString() + "')\"/>");
                str0.Append("&nbsp;&nbsp;<span>" + r.module_name + "</span></label>");
                ParentPane.HeaderContainer.Controls.Add(new LiteralControl(str0.ToString()));

                #endregion

                #region Create the Child Accordion

                ChildAccordion = new Accordion();
                ChildAccordion.ID = r.module_code + "_" + i.ToString();
                ChildAccordion.SelectedIndex = -1;//No default selection
                ChildAccordion.RequireOpenedPane = false;//no default open pane
                ChildAccordion.HeaderCssClass = "ChildAccordionHeader";//Header class
                ChildAccordion.HeaderSelectedCssClass = "ChildAccordionHeaderSelected";//Selected herder class
                ChildAccordion.ContentCssClass = "ChildAccordionContent";//Content class
                #endregion



                foreach (var submodule in r.submodulenames)
                {
                    ChildPane = new AccordionPane();
                    //lbChildTitle = new Label();
                    //lbChildContent = new Label();
                    ChildPane.ID = "subModule_" + r.mns_no + "_" + submodule.submodule_code.ToString();// submodule.submodule_id.ToString();
                    //lbChildTitle.Text = submodule.submodule_name;
                    int j = 0;
                    // ChildPane.HeaderContainer.Controls.Add(new LiteralControl(lbChildTitle));
                    StringBuilder str1 = new StringBuilder();
                    str1.Append("<div runat=\"server\" id=\"subdiv_" + submodule.submodule_id.ToString() + "\" name=\"subdiv_" + submodule.submodule_id.ToString() + "\" class=\"class_Sub" + r.mns_no.ToString() + "_" + submodule.submodule_code.ToString() + " class_Sub" + r.mns_no.ToString() + "\"style=\"left : 10px;\">");
                    str1.Append("<label for=\"x\" style=\"width: 350px;\">");
                    str1.Append("<input runat=\"server\" type =\"checkbox\" id=\"chk_" + ChildPane.ID + "\" name=\"chk_" + ChildPane.ID + "\" onclick=\"SubModuleCheckAll('class_Sub" + r.mns_no.ToString() + "_" + submodule.submodule_code.ToString() + "' ,'class_Sub" + r.mns_no.ToString() + "','chk_" + ChildPane.ID + "')\"/>");
                    str1.Append("&nbsp;&nbsp;<span>" + submodule.submodule_name + "</span></label>");
                    ChildPane.HeaderContainer.Controls.Add(new LiteralControl(str1.ToString()));
                    while (submodule.tabnames.Count > j)
                    {

                        StringBuilder str = new StringBuilder();
                        str.Append("<div runat=\"server\" id=\"divTab_" + submodule.tabnames[j].tab_name_id + "\" name=\"divTab_" + submodule.tabnames[j].tab_name_id + "\" class=\"class_Sub" + r.mns_no.ToString() + "_" + submodule.submodule_code.ToString() + " class_Sub" + r.mns_no.ToString() + "\"style=\"left = 10px;\">");
                        str.Append("<label for=\"x\" style=\"width: 350px;\">");
                        str.Append("<input runat=\"server\" type=\"checkbox\" id=\"tab_" + r.mns_no + "_" + submodule.submodule_code.ToString() + "_" + submodule.tabnames[j].tab_name_id + "\" name=\"tab_" + r.mns_no + "_" + submodule.submodule_code.ToString() + "_" + submodule.tabnames[j].tab_name_id + "\"  onclick=\"CheckBoxCheckAll('divTab_" + submodule.tabnames[j].tab_name_id + "','tab_" + r.mns_no + "_" + submodule.submodule_code.ToString() + "_" + submodule.tabnames[j].tab_name_id + "')\"/>");
                        str.Append("&nbsp;&nbsp;<span>" + submodule.tabnames[j].tab_name + "</span></label>");
                        str.Append(" <label for=\"y\">");
                        str.Append("<input runat=\"server\" type=\"checkbox\" id=\"" + r.mns_no + "_" + submodule.submodule_code.ToString() + "_" + submodule.tabnames[j].tab_name_id + "_Add\" name=\"" + r.mns_no + "_" + submodule.submodule_code.ToString() + "_" + submodule.tabnames[j].tab_name_id + "_Add\"/>");
                        str.Append("<span>Add</span></label>");
                        str.Append(" <label for=\"z\">");
                        str.Append("<input runat=\"server\" type=\"checkbox\" id=\"" + r.mns_no + "_" + submodule.submodule_code.ToString() + "_" + submodule.tabnames[j].tab_name_id + "_Edit\" name=\"" + r.mns_no + "_" + submodule.submodule_code.ToString() + "_" + submodule.tabnames[j].tab_name_id + "_Edit\"/>");
                        str.Append("<span>Edit</span></label>");
                        str.Append(" <label for=\"n\">");
                        str.Append("<input runat=\"server\" type=\"checkbox\" id=\"" + r.mns_no + "_" + submodule.submodule_code.ToString() + "_" + submodule.tabnames[j].tab_name_id + "_Delete\" name=\"" + r.mns_no + "_" + submodule.submodule_code.ToString() + "_" + submodule.tabnames[j].tab_name_id + "_Delete\"/>");
                        str.Append("<span>Delete</span></label>");
                        str.Append(" <label for=\"w\">");
                        str.Append("<input runat=\"server\" type=\"checkbox\" id=\"" + r.mns_no + "_" + submodule.submodule_code.ToString() + "_" + submodule.tabnames[j].tab_name_id + "_Review\" name=\"" + r.mns_no + "_" + submodule.submodule_code.ToString() + "_" + submodule.tabnames[j].tab_name_id + "_Review\" />");
                        str.Append("<span>Review</span></label>");
                        str.Append(" <label for=\"v\">");
                        str.Append("<input runat=\"server\" type=\"checkbox\" id=\"" + r.mns_no + "_" + submodule.submodule_code.ToString() + "_" + submodule.tabnames[j].tab_name_id + "_Approve\" name=\"" + r.mns_no + "_" + submodule.submodule_code.ToString() + "_" + submodule.tabnames[j].tab_name_id + "_Approve\"/>");
                        str.Append("<span>Approve</span></label></div><br/>");



                        //  ChildPane.ContentContainer.Controls.Add(lbl);
                        ChildPane.ContentContainer.Controls.Add(new LiteralControl(str.ToString()));

                        j++;
                    }
                    if (submodule.tabnames.Count > 0)
                    {
                        ChildAccordion.Panes.Add(ChildPane);
                        ChildPane.HeaderContainer.Controls.Add(new LiteralControl("</div>"));
                    }

                   
                }
                ParentPane.HeaderContainer.Controls.Add(new LiteralControl("</div>"));


                #region  Add Child Accordion to the Parent Pane and Parent Pane to the Parent Accordion

                ParentPane.ContentContainer.Controls.Add(ChildAccordion);
                ParentAccordion.Panes.Add(ParentPane);

                #endregion

                i++;
            }

            //  #region Add the Parent Accordion to the panel of page

            this.MyContent.Controls.Add(ParentAccordion);

        }
        //  #endregion
        private void CreateModelview()
        {
            rolePerssion_ViewModel = new List<RolePerssion_ViewModel>();
            rolePerssion_ViewModel = BL_RolePermisions.GetRoleModelList();
        }
        [WebMethod]
        private static PropertyInfo[] GetProperties(object obj)
        {
            return obj.GetType().GetProperties();
        }

        [WebMethod]
        public static string SaveUserPermission(Object roleobj, string rolecode, string orgId)
        {
            IEnumerable myList = roleobj as IEnumerable;

           
                BL_RolePermisions.UpdatePermissions(Convert.ToInt32(registrationid), "user_permission");
          //  }
            if (myList != null)
            {
                foreach (object element in myList)
                {
                  
                    if (element != null)
                    {
                        string[] values = element.ToString().Split('_');
                        //if (element.ToString().StartsWith("tab_"))
                        //{
                        //    values = element.ToString().Split('_');
                        //    BL_RolePermisions.UpdateUserPermissions(values, rolecode.ToString(), orgId, registrationid.ToString(), user_id);
                        //}
                        if (values[values.Length - 1].ToString() == "Add")
                        {
                            BL_RolePermisions.InsertUserPermissions(values, rolecode.ToString(), orgId, userpermissionid, registrationid.ToString(), user_id);
                        }
                        if (values[values.Length - 1].ToString() == "Edit")
                        {
                            BL_RolePermisions.InsertUserPermissions(values, rolecode.ToString(), orgId, userpermissionid, registrationid.ToString(), user_id);
                        }
                        if (values[values.Length - 1].ToString() == "Delete")
                        {
                            BL_RolePermisions.InsertUserPermissions(values, rolecode.ToString(), orgId, userpermissionid, registrationid.ToString(), user_id);
                        }
                        if (values[values.Length - 1].ToString() == "Review")
                        {
                            BL_RolePermisions.InsertUserPermissions(values, rolecode.ToString(), orgId, userpermissionid, registrationid.ToString(), user_id);
                        }
                        if (values[values.Length - 1].ToString() == "Approve")
                        {
                            BL_RolePermisions.InsertUserPermissions(values, rolecode.ToString(), orgId, userpermissionid, registrationid.ToString(), user_id);
                        }
                    }
                }
            }
            return "SuccessFully Submitted";
        }
        string  ConvertObjectToString(Object obj)
        {
            return obj?.ToString() ?? string.Empty;
        }
        protected void RolePermission_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("RolePermissionList");
        }
        protected void UserPermission_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("UserPermissionsList");
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("UserPermissionsList");
        }
        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Session["rtype"] = "0";
            Response.Redirect("UserPermissionsList");
        }
    }

}