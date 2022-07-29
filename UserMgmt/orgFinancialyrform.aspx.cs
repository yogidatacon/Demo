using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class orgFinancialyrform : System.Web.UI.Page
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
                string user_id = "admin";
                if (rtype != "0")
                {
                    string org_id = Session["org_id"].ToString();
                    string OrgName = Session["OrgName"].ToString();
                    orgid.Value = org_id;
                    ddOrgnames.Items.Insert(0, new ListItem(OrgName, ""));
                    btnSave.Visible = false;
                    btnCancel.Visible = false;
                    ddOrgnames.Enabled = false;
                   // ddOrgnames.Visible = false;
                    txtOrgname.Visible = false;
                    txtOrgname.Text = OrgName;
                   // txtOrgname.BackColor= System.Drawing.Color.DarkGray;
                    ddOrgnames.BackColor = System.Drawing.Color.LightGray;
                    txtyear.Value= Session["FinancialYear"].ToString(); 
                    txtyear.Attributes.Add("disabled", "disabled");
                   // txtOrgname.Enabled = false;
                }
                else
                {
                    txtOrgname.Visible = false;
                    List<Org_Finacial_yr> orgnames = new List<Org_Finacial_yr>();
                    orgnames = BL_org_Master.GetOrgnames(user_id);
                    txtyear.Value = GetCurrentFinancialYear();
                    for (int n = 0; n < orgnames.Count; n++)
                    {
                        ddOrgnames.Items.Insert((n + 1), new ListItem(orgnames[n].org_name, orgnames[n].org_id));
                    }
                    btnSave.Text = "Submit";
                    btnCancel.Text = "Cancel";
                }
            }
        }
        public static string GetCurrentFinancialYear()
        {
            int CurrentYear = DateTime.Today.Year;
            int PreviousYear = DateTime.Today.Year - 1;
            int NextYear = DateTime.Today.Year + 1;
            string PreYear = PreviousYear.ToString();
            string NexYear = NextYear.ToString();
            string CurYear = CurrentYear.ToString();
            string FinYear = null;

            if (DateTime.Today.Month > 3)
                FinYear = CurYear + "-" + NexYear;
            else
                FinYear = PreYear + "-" + CurYear;
            return FinYear.Trim();
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
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("orgFinancialyrlist");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<Org_Finacial_yr> Org_Finacial = new List<Org_Finacial_yr>();
            Org_Finacial = BL_org_Master.GetFinacListValues("");
            var financial_year = from s in Org_Finacial
                                 where s.status == "Active"
                                 select s;
            if (txtyear.Value==financial_year.ToList()[0].financial_year)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Only one record per year');", true);
            }
            else
            {

           
            btnSave.Enabled = false;
            string rtype = Session["rtype"].ToString();
           // string org_id = Session["org_id"].ToString();
            Org_Finacial_yr org = new Org_Finacial_yr();
            org.org_name = ddOrgnames.SelectedItem.ToString();
            org.org_id = ddOrgnames.SelectedValue.ToString();
            org.financial_year = txtyear.Value;
            if (rtype != "0")
            {
                if (BL_org_Master.UpdateOrgFinance_Details(org))
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
                    Response.Redirect("orgFinancialyrlist");
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
                if (BL_org_Master.InsertOrgFinance_Details(org))
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
                   // Session["org_id"] = Session["org_id"].ToString();
                    Response.Redirect("orgFinancialyrlist");
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
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("orgFinancialyrlist");
        }
    }
}