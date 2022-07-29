using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class OrgFroms : System.Web.UI.Page
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
                string rtype = Session["rtype"].ToString();
                Session["rtype"] = rtype;
             
                if (rtype != "0")
                {
                    List<Module_Master> module = new List<Module_Master>();
                    module = BL_Module_Master.GetList();
                    var list = from s in module
                               where s.org_id == Session["org_id"].ToString()
                               select s;
                   
                    string org_id = Session["org_id"].ToString();
                    Session["org_id"] = org_id;
                    Org_Master org = new Org_Master();
                    org = BL_org_Master.GetOrg_Details(org_id);
                    txtOrgname.Text = org.org_name;
                    txtDescr.Value = org.org_desc;
                    txtorgtyp.Text = org.org_type;
                    txtaddress.Value = org.org_address;
                    textPan.Text = org.pan;
                    txtEmail.Text = org.email_id;
                    txtContactNumber.Text = org.cont_number;
                   // txtstartDate.Value = org.start_date.ToShortDateString();
                   textTan.Text = org.tan;
                    txtGST.Text = org.gst;
                    textTin.Text = org.tin;
                  //  txtenddate.Value = org.end_date.ToShortDateString();
                    orgid.Value= org_id;
                  
                    if (rtype == "1" || list.ToList().Count > 0)
                    {
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        txtOrgname.ReadOnly = true;
                        txtDescr.Attributes.Add("disabled", "disabled");
                        txtorgtyp.Enabled = false;
                        txtaddress.Attributes.Add("disabled", "disabled");
                        textPan.ReadOnly = true;
                        txtContactNumber.ReadOnly = true;
                        textTan.ReadOnly = true;
                        textTin.ReadOnly = true;
                        txtEmail.ReadOnly = true;
                        txtGST.ReadOnly = true;
                        // End_date.Attributes.Add("disabled", "disabled");

                    }
                    if (rtype == "2")
                    {
                        btnSave.Text = "Save";
                        btnCancel.Text = "Cancel";
                    }
                
                }
                else
                {
                    //int n = Convert.ToInt32(BL_org_Master.GetMaxID("org_master"));
                    //orgid.Value =(n+1).ToString() ;
                    Session["org_id"] = orgid.Value;
                    btnSave.Text = "Submit";
                    btnCancel.Text = "Cancel";
                }
            }
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

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            string rtype = Session["rtype"].ToString();
            Session["rtype"] = rtype;
            Session["org_id"] = orgid.Value;
            Response.Redirect("OrgList");
        }
        [WebMethod]
        public static string chkDuplicateEmailData(Object email_id)
        {
            // IEnumerable myList = email_id as IEnumerable;
            int value = BL_User_Mgnt.GetExistsData("org_master", "email_id", email_id.ToString());
            return value.ToString();
        }
        [WebMethod]
        public static string chkDuplicaOrgname(Object orgname)
        {
            // IEnumerable myList = email_id as IEnumerable;
            int value = BL_User_Mgnt.GetExistsData("org_master", "org_name", orgname.ToString());
            return value.ToString();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            string rtype = Session["rtype"].ToString();
            string org_id = Session["org_id"].ToString();
            Org_Master org = new Org_Master();
            org.org_name = txtOrgname.Text;
            org.org_type = txtorgtyp.Text;
            org.org_address = txtaddress.Value;
            org.org_desc = txtDescr.Value;
            org.pan = textPan.Text;
            org.tan = textTan.Text;
            org.tin = textTin.Text;
            org.email_id = txtEmail.Text;
            org.cont_number = txtContactNumber.Text;
            org.gst = txtGST.Text;
            //org.start_date = Convert.ToDateTime(txtstartDate.Value);
            //org.end_date = Convert.ToDateTime(txtenddate.Value);
            // org.org_id = org_id;
            if (rtype != "0")
            {
                org.org_id = orgid.Value;
                if (BL_org_Master.UpdateOrg_Details(org))
                {
                    string message = "Record is Updated.";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    Session["rtype"] = rtype;
                    Session["org_id"] = Session["org_id"].ToString();
                    Response.Redirect("OrgList");
                }
                else
                {
                    btnSave.Enabled = true;
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
                org.org_id =orgid.Value;
               // org.org_code = "ORG"+String.Format("{0:0000}",Convert.ToInt32(orgid.Value));
                if (BL_org_Master.InsertOrg_Details(org))
                {
                    string message = "Record is Succesfully Submited.";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());

                    Session["rtype"] = rtype;
                    Session["org_id"] = Session["org_id"].ToString();
                    Response.Redirect("OrgList");
                }
                else
                {
                    btnSave.Enabled = true;
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
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("OrgList");
        }
    }
}