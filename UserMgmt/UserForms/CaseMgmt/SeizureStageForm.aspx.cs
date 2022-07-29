using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class SeizureStageForm : System.Web.UI.Page
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
                    string CasteId = Session["SeizureStageId"].ToString();
                    txtid.Value = CasteId;
                    //List<cm_caste> caste = new List<cm_caste>();
                    //caste = BL_cm_caste.GetList();
                    //var list = from s in caste
                    //           where s.religion_code == Session["ReligionCode"].ToString()
                    //           select s;
                    //if (list.ToList().Count > 0)
                    //{
                    //    txtCode.Attributes.Add("disabled", "disabled");
                    //    txtName.Attributes.Add("disabled", "disabled");
                    //}
                    if (rtype == "1")
                    {
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        txtName.Text = Session["SeizureStageName"].ToString();
                        txtCode.Text = Session["SeizureStageCode"].ToString();
                        txtid.Value = Session["SeizureStageId"].ToString();
                        txtCode.ReadOnly = true;
                        txtName.ReadOnly = true;

                    }
                    if (rtype == "2")
                    {
                        txtName.Text = Session["SeizureStageName"].ToString();
                        txtCode.Text = Session["SeizureStageCode"].ToString();
                        txtid.Value = Session["SeizureStageId"].ToString();
                    }
                }
                else
                {
                    //int n = Convert.ToInt32(BL_org_Master.GetMaxID("bail_type_master"));
                    //txtid.Value = (n + 1).ToString();
                    Session["ReligionId"] = txtid.Value;
                    btnSave.Text = "Submit";
                    btnCancel.Text = "Cancel";
                }
            }
        }
        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("SeizurStageList");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            cm_seizure_stage cm_obj = new cm_seizure_stage();
            if (string.IsNullOrEmpty(txtCode.Text) || string.IsNullOrWhiteSpace(txtCode.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Seizure Stage Code\');", true);
                return;
            }
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Seizure Stage Name\');", true);
                return;
            }
            cm_obj.seizure_stage_code = txtCode.Text;
            cm_obj.seizure_stage_name = txtName.Text;
            cm_obj.seizure_stage_sequence = 1;//txtName.Value;
            cm_obj.user_id = Session["UserID"].ToString();
           cm_obj.lastmodified_date = DateTime.Now.ToShortDateString();
            cm_obj.record_status = "Y";
            cm_obj.record_deleted = false;
            //if (BL_cm_seizure_stage.InsertSeizureStage(cm_obj))
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
            //    Response.Redirect("~/SeizureStageList");
            //}


            if (Session["rtype"].ToString() != "0")
            {
                cm_obj.seizure_stage_id = Convert.ToInt32(Session["SeizureStageId"].ToString());
                if (BL_cm_seizure_stage.UpdateSeizurestage(cm_obj))
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
                    Response.Redirect("SeizurStageList");
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
                if (BL_cm_seizure_stage.InsertSeizureStage(cm_obj))
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
                    Response.Redirect("SeizurStageList");
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("SeizurStageList");
        }

        protected void txtCode_TextChanged(object sender, EventArgs e)
        {
            if (txtCode.Text != "")
            {
                int value = BL_User_Mgnt.GetExistsData("seizure_stage", "seizure_stage_code", txtCode.Text);
                if (value > 0)
                {
                    string message = "Seizure Stage Code  is Already Exists.";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    txtCode.Text = "";
                    txtCode.Focus();
                }
            }
        }

      
    }
}