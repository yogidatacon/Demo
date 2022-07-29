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
    public partial class ApparatusForm : System.Web.UI.Page
    {
        List<cm_seiz_Apparatus> _Apparatus = new List<cm_seiz_Apparatus>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                List<apparatus_type_master> apparatus_type = new List<apparatus_type_master>();
                apparatus_type = BL_cm_seiz_Apparatus.GetApparatusTypeMasterList(string.Empty);
                Session["UserID"] = Session["UserID"];
                ddlApparatusType.DataSource = apparatus_type;
                ddlApparatusType.DataTextField = "apparatus_type";
                ddlApparatusType.DataValueField = "Apparatus_type_code";
                ddlApparatusType.DataBind();
                ddlApparatusType.Items.Insert(0, "Select");

                if (Session["rtype"].ToString() != "0")
                {
                    string seizureNo = Session["seizureNo"].ToString();
                    string tableId = Session["tableId"].ToString();
                    serchid.Visible = false;
                    cm_seiz_Apparatus obj = new cm_seiz_Apparatus();
                    obj = BL_cm_seiz_Apparatus.GetDetails(tableId);

                    txtApparatusName.Text = obj.apparatus_name;
                    txtManufacturer.Text = obj.manufacturer_code.ToString();
                    ddlApparatusType.SelectedValue = obj.apparatus_type_code.ToString();
                    txtMakeModel.Text= obj.makemodel;
                    txtOwnerName.Text = obj.ownername?.ToString() ?? string.Empty;
                    txtMobileNo.Text = obj.contactno?.ToString() ?? string.Empty; 
                    txtOwnerPermanentAddress.Text = obj.permanentaddress;
                    txtOwnerPresentAddress.Text = obj.presentaddress;
                    txtIMEI.Text = obj.imeino;

                    if ((Session["rtype"].ToString() == "1"))
                    {
                        btnCancel.Visible = false;
                        btnSaveasDraft.Visible = false;
                        //btnSubmit.Visible = false;
                        txtApparatusName.Attributes.Add("disabled", "disabled");
                        txtManufacturer.Attributes.Add("disabled", "disabled");
                        ddlApparatusType.Attributes.Add("disabled", "disabled");
                        txtMakeModel.Attributes.Add("disabled", "disabled");
                        txtOwnerName.Attributes.Add("disabled", "disabled");
                        txtMobileNo.Attributes.Add("disabled", "disabled");
                        txtOwnerPermanentAddress.Attributes.Add("disabled", "disabled");
                        txtOwnerPresentAddress.Attributes.Add("disabled", "disabled");
                        txtIMEI.Attributes.Add("disabled", "disabled");
                        chk.Enabled = false;
                    }
                }
            }
        }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ApparatusList");
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                btnSaveasDraft.Enabled = false;
                cm_seiz_Apparatus cm_obj = new cm_seiz_Apparatus();
                cm_obj.seizureno = Convert.ToInt32(Session["seizureNo"].ToString());
                cm_obj.apparatus_name = txtApparatusName.Text;
                cm_obj.manufacturer_code = txtManufacturer.Text;
                cm_obj.apparatus_type_code = ddlApparatusType.SelectedValue;
                cm_obj.makemodel = txtMakeModel.Text;
                cm_obj.ownername = txtOwnerName.Text;
                cm_obj.contactno = txtMobileNo.Text;
                cm_obj.imeino = txtIMEI.Text;
                cm_obj.permanentaddress = txtOwnerPermanentAddress.Text;
                cm_obj.presentaddress = txtOwnerPresentAddress.Text;
                cm_obj.user_id = Session["UserID"].ToString();
                cm_obj.lastmodified_date = DateTime.Now;
                cm_obj.record_status = "N";
                cm_obj.record_deleted = false;
                cm_obj.raidby = Session["RaidBy"].ToString().Substring(0, 1);
                string strHostName = System.Net.Dns.GetHostName();
                string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                cm_obj.ipaddress = clientIPAddress.ToString();
                bool val;
                string tempTableId = Session["tableId"]?.ToString() ?? string.Empty;
                if (Session["rtype"].ToString() == "0" && string.IsNullOrEmpty(tempTableId))
                    val = BL_cm_seiz_Apparatus.InsertApparatus(cm_obj);
                else
                {
                    cm_obj.seizure_apparatusdetails_id =Convert.ToInt32(Session["tableId"].ToString());
                    val = BL_cm_seiz_Apparatus.Update(cm_obj);
                }
                if (val == true)
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("ApparatusList");
                }
                else
                {
                    btnSaveasDraft.Enabled = true;
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
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/ApparatusList");
        }
        protected void btnBasicInformation_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("BasicIformationForm");

        }

        protected void btnEAS_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ExcisableArticlesSeizedList");
        }

        protected void btnOtherExisable_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VehicleList");

        }
        protected void btnRaidTeam_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RaidTeamList");

        }
        protected void btnAccusedDetails_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("AccusedDetailsList");
        }

        protected void btnVehicle_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VehicleList");
        }
        protected void btnApparatus_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ApparatusList");

        }
        protected void btnProperty_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("PropertyList");

        }
        protected void btnMoney_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("MoneyList");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
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

                string name = txtAName.Text;
                if (name.Trim() != "")
                {
                    _Apparatus = BL_cm_seiz_Apparatus.ApparatusSearch(name);
                    grdApparatusView.DataSource = _Apparatus.ToList();
                    grdApparatusView.DataBind();
                }
                else
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append("Enter Apparatus Name");
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                }
            }
        }

        protected void chselect_CheckedChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

                foreach (GridViewRow row in grdApparatusView.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chselect") as CheckBox);
                        if (chkRow.Checked)
                        {

                            ddlApparatusType.SelectedValue = (row.Cells[0].FindControl("lblApparatusTypeCode") as Label).Text;
                            txtApparatusName.Text = (row.Cells[2].FindControl("lblApparatusName") as Label).Text;
                            break;
                        }
                        else
                        {
                            ddlApparatusType.SelectedValue = "Select";
                            txtAName.Text = "";
                        }
                    }
                }

            }
        }

        protected void grdApparatusView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdApparatusView.PageIndex = e.NewPageIndex;
            string name = txtAName.Text;
            _Apparatus = BL_cm_seiz_Apparatus.ApparatusSearch(name);
            grdApparatusView.DataSource = _Apparatus.ToList();
            grdApparatusView.DataBind();
        }
    }
}