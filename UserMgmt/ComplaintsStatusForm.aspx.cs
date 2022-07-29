using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class ComplaintsStatusForm : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        
        protected void Page_Load(object sender, EventArgs e)
        {
           
       
            if (!IsPostBack)
            {
                if (ViewState["Records"] == null)
                {
                    dt.Columns.Add("Status");
                    dt.Columns.Add("Doc_Name");
                    dt.Columns.Add("Discription");
                    dt.Columns.Add("Doc_Path");
                    dt.Columns.Add("Doc_id");
                    ViewState["Records"] = dt;
                }
                if (Session["rtype"] != null)
                {
                    if (Session["rtype"].ToString() != "0")
                    {
                        Complaint cmp = new Complaint();
                        cmp = BL_Complaint.GetDetails(Convert.ToInt32(Session["Complaint_ID"].ToString()));
                    txtComplaintno.Text = cmp.complaint_no.ToString();
                        ddlcomplaintype.SelectedValue = cmp.complaint_status;
                        if ((Session["rtype"].ToString() == "1"))
                        {

                            txtComplaintno.ReadOnly = true;
                            ddlcomplaintype.Enabled = false;
                            btnCancel.Visible = false;
                            btnSaveasDraft.Visible = false;
                            btnSubmit.Visible = false;
                        }

                        else
                        {
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append("<script type = 'text/javascript'>");
                            sb.Append("window.onload=function(){");
                            sb.Append("alert('");
                            sb.Append("Database Server Not Connecting");
                            sb.Append("')};");
                            sb.Append("</script>");
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                        }
                    }

                    else
                    {
                        Response.Redirect("~/User_Mgmt");
                    }
                }

            }

        }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ComplaintList.aspx");

        }



        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Complaint cmp = new Complaint();
                cmp.complaint_no= Convert.ToInt32(txtComplaintno.Text);
                cmp.complaint_status = ddlcomplaintype.SelectedValue;
                cmp.record_status = "N";
                string val;
                //if (Session["rtype"].ToString() == "0")
                val = BL_Complaint.StatusUpdate(cmp);
                //else
                //{
                //    cmp.complaint_id = Convert.ToInt32(Session["Complaint_ID"].ToString());
                //    val = BL_Complaint.Update(cmp);
                //}
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("https://prohibitionbihar.in/#");
                }
                else
                {

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(val);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                }

            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Complaint cmp = new Complaint();
                cmp.complaint_no = Convert.ToInt32(txtComplaintno.Text);
                cmp.complaint_status = ddlcomplaintype.SelectedValue;
                cmp.record_status = "N";
                string val;
                //if (Session["rtype"].ToString() == "0")
                val = BL_Complaint.StatusUpdate(cmp);
                //else
                //{
                //    cmp.complaint_id = Convert.ToInt32(Session["Complaint_ID"].ToString());
                //    val = BL_Complaint.Update(cmp);
                //}
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("https://prohibitionbihar.in/#");
                }
                else
                {

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(val);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("https://prohibitionbihar.in/#");
        }


        [WebMethod]
        public static string chkDuplicateState(Object statename)
        {
            // IEnumerable myList = email_id as IEnumerable;
            int s = Convert.ToInt32(statename);
            int value = BL_Complaint.GetExistsData("complaint", "complaint_no", s);
            return value.ToString();
        }

        protected void txtcomplaint_no(object sender, EventArgs e)
        {
            if(txtComplaintno.Text!="")
            {
                int value = BL_Complaint.GetExistsData("complaint", "complaint_no", Convert.ToInt32(txtComplaintno.Text));
                if(value<=0)
                {
                    
                         ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Complaint number is not exists !!!!!\');", true);
                    txtComplaintno.Focus();
                    txtComplaintno.Text = "";
                }
            }
           
        
        }

        protected void btncomplaint_no(object sender, EventArgs e)
        {
            if (txtComplaintno.Text != "")
            {
                int value = BL_Complaint.GetExistsData("complaint", "complaint_no", Convert.ToInt32(txtComplaintno.Text));
                if (value <= 0)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Complaint number is not exists !!!!!\');", true);
                    txtComplaintno.Focus();
                    txtComplaintno.Text = "";
                }
                else
                {
                    List<Complaint> allots = new List<Complaint>();
                    allots = BL_Complaint.Getlist();
                    var list = (from s in allots
                                where s.complaint_no==Convert.ToInt32(txtComplaintno.Text)
                                select s);
                    grdAdd.DataSource = list.ToList();
                    grdAdd.DataBind();
                   if(grdAdd.Rows.Count>0)
                    CMPS.Visible = true;
                }
            }


        }


    }
}