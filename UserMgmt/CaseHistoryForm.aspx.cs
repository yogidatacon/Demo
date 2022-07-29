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
    public partial class CaseHistoryForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<cm_idproof> _idproof = new List<cm_idproof>();
                _idproof = BL_cm_idproof.GetList();
                ddlAccusedIdProof.DataSource = _idproof;
                ddlAccusedIdProof.DataTextField = "idproof_name";
                ddlAccusedIdProof.DataValueField = "idproof_code";
                ddlAccusedIdProof.DataBind();
                ddlAccusedIdProof.Items.Insert(0, "Select");

                string seizureNo = Session["seizureNo"]?.ToString() ?? string.Empty;

                List<cm_seiz_AccusedDetails> accusedDetails = new List<cm_seiz_AccusedDetails>();
                accusedDetails = BL_cm_seiz_AccusedDetails.GetDetails(seizureNo);
                ddlAccusedName.DataSource = accusedDetails;
                ddlAccusedName.DataTextField = "accusedname";
                ddlAccusedName.DataValueField = "seizure_accused_details_id";
                ddlAccusedName.DataBind();
                ddlAccusedName.Items.Insert(0, "Select");

                if (Session["rtype"].ToString() != "0")
                {
                    
                    string id= Session["tableId"].ToString();

                    cm_seiz_CaseHistory obj = new cm_seiz_CaseHistory();
                    obj = BL_cm_seiz_CaseHistory.GetDetails(id);

                    //Todo Fetch from Table
                    //string id = Session["seizure_accusedcasehistory_id"].ToString();
                    ddlAccusedName.SelectedValue = obj.seizure_accused_details_id.ToString();  //"1";
                    ddlAccusedIdProof.SelectedValue = obj.idproof_code.ToString(); //"1";
                    txtIDNo.Text = obj.ipaddress.ToString();
                    txtCaseID.Text = obj.case_id;
                    txtCaseDetails.Text = obj.case_details;
                    txtIDNo.Text = obj.idno;


                    if (Session["rtype"].ToString() == "1" )
                    {
                        ddlAccusedIdProof.Enabled = false;
                        ddlAccusedName.Enabled = false;
                        txtCaseDetails.ReadOnly = true;
                        txtCaseID.ReadOnly = true;
                        txtIDNo.ReadOnly = true;
                        btnCancel.Visible = false;
                        btnSaveasDraft.Visible = false;
                    }
                }
            }

        }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {
          //  Session["UserID"] = Session["UserID"].ToString();

            Response.Redirect("CaseHistoryList.aspx");
        }

        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                cm_seiz_CaseHistory cm_obj = new cm_seiz_CaseHistory();

                if (ddlAccusedName.SelectedValue != "Select")
                    cm_obj.seizure_accused_details_id = ddlAccusedName.SelectedValue.ToString();
                //cm_obj.seizure_accusedcasehistory_id = Convert.ToInt32(ddlAccusedIdProof.SelectedValue);
                cm_obj.idproof_code = (ddlAccusedIdProof.SelectedValue);
                cm_obj.idno = txtIDNo.Text;
                cm_obj.case_id = txtCaseID.Text;
                cm_obj.case_details = txtCaseDetails.Text;
                //cm_obj.user_id = Session["UserID"].ToString();
                cm_obj.seizureno = Convert.ToInt32(Session["seizureno"].ToString());
                cm_obj.user_id = Session["UserID"].ToString();
                cm_obj.lastmodified_date = DateTime.Now.ToShortDateString();
                cm_obj.record_status = "N";
                cm_obj.record_deleted = false;
                cm_obj.raidby = Session["RaidBy"].ToString().Substring(0, 1);
                string value;
                string tempTableId = Session["tableId"]?.ToString() ?? string.Empty;
                if (Session["rtype"].ToString() == "0" && string.IsNullOrEmpty(tempTableId))
                    value = BL_cm_seiz_CaseHistory.Insert(cm_obj);
                else
                {
                    //cm_obj.seizure_accusedcasehistory_id = Session["tableId"].ToString();
                    cm_obj.seizure_accusedcasehistory_id=Convert.ToInt32(tempTableId);
                    value = BL_cm_seiz_CaseHistory.Update(cm_obj);
                }
                if (value == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Session["seizureno"] = Session["seizureNo"].ToString();
                    Response.Redirect("CaseHistoryList.aspx");
                }
                else
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(value);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CaseHistoryList.aspx");
        }
    }
}