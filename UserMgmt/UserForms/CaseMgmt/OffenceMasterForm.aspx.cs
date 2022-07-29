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
    public partial class OffenceMasterForm : System.Web.UI.Page
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
                //List<cm_offence_type> offence = new List<cm_offence_type>();
                //offence = BL_cm_offence_type.GetList();
                //var list = from s in offence
                //           select s;
                //ddlOffenceType.DataSource = list.ToList();
                //ddlOffenceType.DataTextField = "offence_name";
                //ddlOffenceType.DataValueField = "offence_code";
                //ddlOffenceType.DataBind();
                //ddlOffenceType.Items.Insert(0, "Select");


                if (rtype != "0")
                {
                    string CasteId = Session["OffenceId"].ToString();
                    txtid.Value = CasteId;
                    txtName.Text = Session["OffenceName"].ToString();
                    List<cm_seiz_OffencesCommitted> offns = new List<cm_seiz_OffencesCommitted>();
                    offns = BL_cm_seiz_OffencesCommitted.GetoffenceList("");
                    var ad = (from s in offns
                              where s.offence_code == Session["OffenceCode"].ToString()
                              select s);
                   
                    if (ad.ToList().Count > 0)
                    {
                        txtcode.ReadOnly = true;
                        txtName.ReadOnly = true;
                    }
                   if (rtype == "1")
                    {
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        txtName.Text = Session["OffenceName"].ToString();
                        txtcode.Text = Session["OffenceCode"].ToString();
                     //   ddlOffenceType.SelectedValue = Session["OffenceTypeCode"].ToString();
                        txtid.Value = Session["OffenceId"].ToString();
                        txtcode.ReadOnly = true;
                        txtName.ReadOnly = true;
                      //  ddlOffenceType.Enabled = false;

                    }
                    if (rtype == "2")
                    {
                      //  ddlOffenceType.SelectedValue = Session["OffenceTypeCode"].ToString();
                        txtName.Text = Session["OffenceName"].ToString();
                        txtcode.Text = Session["OffenceCode"].ToString();
                        txtid.Value = Session["OffenceId"].ToString();
                        txtcode.ReadOnly = true;
                    }
                }
                else
                {
                    int n = Convert.ToInt32(BL_org_Master.GetMaxID("offence_master"));
                    txtcode.Text = "OF" + string.Format("{0:00}",n + 1);
                    Session["OffenceId"] = txtid.Value;
                    txtcode.ReadOnly = true;
                    btnSave.Text = "Submit";
                    btnCancel.Text = "Cancel";
                }
            }
        }
        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/OffenceMasterList");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            cm_offence cm_obj = new cm_offence();
           
            cm_obj.offence_code = txtcode.Text;
            cm_obj.offence_name = txtName.Text;
           // cm_obj.offence_code = ddlOffenceType.SelectedValue;
            cm_obj.user_id = Session["UserID"].ToString();
           cm_obj.lastmodified_date = DateTime.Now.ToShortDateString();
            cm_obj.record_status = "Y";
            cm_obj.record_deleted = false;
            List<cm_offence> _offence = new List<cm_offence>();
            _offence = BL_cm_offence.GetList();
            var ad = (from s in _offence
                      where s.offence_name == cm_obj.offence_name
                      select s);
            if (ad.ToList().Count <= 0 || (ad.ToList()[0].offence_code==txtcode.Text && Convert.ToInt32(Session["OffenceId"].ToString())>0))
            {
                //if (BL_cm_offence.InsertOffencey(cm_obj))
                //{
                //    string message = "Record is Updated.";
                //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //    sb.Append("<script type = 'text/javascript'>");
                //    sb.Append("window.onload=function(){");
                //    sb.Append("alert('");
                //    sb.Append(message);
                //    sb.Append("')};");
                //    sb.Append("</script>");
                //    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                //    Session["UserID"] = Session["UserID"].ToString();
                //    Response.Redirect("~/OffenceMasterList");
                //}

                if (Session["rtype"].ToString() != "0")
                {
                    cm_obj.offence_master_id = Convert.ToInt32(Session["OffenceId"].ToString());
                    if (BL_cm_offence.Updateoffence(cm_obj))
                    {
                        string message = "Record is Successfully Updated.";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                        Session["UserID"] = Session["UserID"].ToString();
                        Response.Redirect("~/OffenceMasterList");
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        string message = "Server Side Error.";
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
                    if (BL_cm_offence.InsertOffencey(cm_obj))
                    {
                        string message = "Record is Successfully Submited.";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                        Session["UserID"] = Session["UserID"].ToString();
                        Response.Redirect("~/OffenceMasterList");
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        string message = "Server Side Error.";
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
            else
            {
                string message = "offcence name is already exists!";
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/OffenceMasterList");
        }

        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            if (txtcode.Text != "")
            {
                int value = BL_User_Mgnt.GetExistsData("offence_master", "offence_code", txtcode.Text);
                if (value > 0)
                {
                    string message = "Offence Code  is Already Exists.";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    txtcode.Text = "";
                    txtcode.Focus();
                }
            }
        }
    }
}