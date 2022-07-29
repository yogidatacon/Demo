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
    public partial class OtherExcisableArticlesForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<cm_Vehicle_type> vehicleTypeDetails = new List<cm_Vehicle_type>();
                vehicleTypeDetails = BL_cm_Vehicle_type.GetList();

                ddlVehicleType.DataSource = vehicleTypeDetails;
                ddlVehicleType.DataTextField = "vehicle_type";
                ddlVehicleType.DataValueField = "vehicle_type_code";
                ddlVehicleType.DataBind();
                ddlVehicleType.Items.Insert(0, "Select");
                //drpVtype.DataSource = vehicleTypeDetails;
                //drpVtype.DataTextField = "vehicle_type";
                //drpVtype.DataValueField = "vehicle_type_code";
                //drpVtype.DataBind();
                //drpVtype.Items.Insert(0, "Select");
                serchid.Visible = true;
                if (Session["rtype"].ToString() != "0")
                {
                    string seizureNo = Session["seizureNo"].ToString();
                    string tableId = Session["tableId"].ToString();
                    serchid.Visible = false;
                    cm_seiz_vehicledetails obj = new cm_seiz_vehicledetails();
                    obj = BL_cm_seiz_vehicledetails.GetDetails(tableId);

                    txtVehicleName.Text = obj.vehiclename;
                    txtManufacturer.Text = obj.manufacturer_code.ToString();
                    ddlVehicleType.SelectedValue = obj.vehicle_type_code.ToString();
                    txtMakeModel.Text = obj.makemodel;
                    txtVehicleNo.Text = obj.vehicle_number;
                    txtChassisNo.Text = obj.chasisno;
                    txtRCNo.Text = obj.registrationno;
                    txtOwnerName.Text = obj.ownername.ToString();
                    txtMobileNo.Text = obj.contactno.ToString();
                    txtOwnerPermanentAddress.Text = obj.permanentaddress;
                    txtOwnerPresentAddress.Text = obj.presentaddress;
                    txtengno.Text = obj.engineno;
                    txtGPSCompany.Text = obj.gpscompany;
                    txtIMEI.Text = obj.imeino;
                    txtSIM.Text = obj.simno;
                    txtremarks.Text = obj.remarks;
                    if(obj.SDR_CAF!="")
                    ddlSDR_CAF.SelectedValue = obj.SDR_CAF;
                    if ((Session["rtype"].ToString() == "1"))
                    {
                        btnCancel.Visible = false;
                        btnSaveasDraft.Visible = false;
                        //btnSubmit.Visible = false;
                        txtVehicleName.Attributes.Add("disabled", "disabled");
                        txtManufacturer.Attributes.Add("disabled", "disabled");
                        ddlVehicleType.Attributes.Add("disabled", "disabled");
                        txtMakeModel.Attributes.Add("disabled", "disabled");
                        txtVehicleNo.Attributes.Add("disabled", "disabled");
                        txtChassisNo.Attributes.Add("disabled", "disabled");
                        txtRCNo.Attributes.Add("disabled", "disabled");
                        txtOwnerName.Attributes.Add("disabled", "disabled");
                        txtMobileNo.Attributes.Add("disabled", "disabled");
                        txtOwnerPermanentAddress.Attributes.Add("disabled", "disabled");
                        txtOwnerPresentAddress.Attributes.Add("disabled", "disabled");
                        chk.Attributes.Add("disabled", "disabled");
                        txtSIM.Attributes.Add("disabled", "disabled");
                        txtGPSCompany.Attributes.Add("disabled", "disabled");
                        txtIMEI.Attributes.Add("disabled", "disabled");
                        txtengno.Attributes.Add("disabled", "disabled");
                        txtremarks.Attributes.Add("disabled", "disabled");
                        chk.Enabled = false;
                        ddlSDR_CAF.Enabled = false;
                    }
                }
            }
        }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Response.Redirect("VehicleList");
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/VehicleList");
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                cm_seiz_vehicledetails cm_obj = new cm_seiz_vehicledetails();
                cm_obj.vehiclename = txtVehicleName.Text;
                cm_obj.vehicle_type_code = ddlVehicleType.SelectedValue;
                cm_obj.manufacturer_code = txtManufacturer.Text;
                cm_obj.makemodel = txtMakeModel.Text;
                cm_obj.vehicle_number = txtVehicleNo.Text;
                cm_obj.chasisno = txtChassisNo.Text;
                cm_obj.registrationno = txtRCNo.Text;
                cm_obj.ownername = txtOwnerName.Text;
                cm_obj.contactno = txtMobileNo.Text;
                cm_obj.permanentaddress = txtOwnerPermanentAddress.Text;
                cm_obj.presentaddress = txtOwnerPresentAddress.Text;
                cm_obj.raidby = Session["RaidBy"].ToString().Substring(0, 1);
                cm_obj.user_id = Session["UserID"].ToString();
                cm_obj.lastmodified_date = DateTime.Now;
                cm_obj.user_id = Session["UserID"].ToString();
                cm_obj.record_status = "Y";
                cm_obj.record_deleted = false;
                cm_obj.remarks = txtremarks.Text;
                cm_obj.engineno = txtengno.Text;
                cm_obj.gpscompany = txtGPSCompany.Text;
                cm_obj.imeino = txtIMEI.Text;
                cm_obj.simno = txtSIM.Text;
                cm_obj.SDR_CAF = ddlSDR_CAF.SelectedValue;
                if (cm_obj.SDR_CAF == "Select")
                    cm_obj.SDR_CAF = "No";
                string strHostName = System.Net.Dns.GetHostName();
                string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                cm_obj.ipaddress = clientIPAddress.ToString();
                bool val;
                if (Session["rtype"].ToString() == "0")
                    val = BL_cm_seiz_vehicledetails.Insertvehicledetails(cm_obj);
                else
                {
                    val = BL_cm_seiz_vehicledetails.Update(cm_obj);
                }
                if (val == true)
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("VehicleList");
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

        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                btnSaveasDraft.Enabled = false;
                cm_seiz_vehicledetails cm_obj = new cm_seiz_vehicledetails();
                cm_obj.seizureno = Convert.ToInt32(Session["seizureNo"].ToString());
                cm_obj.vehiclename = txtVehicleName.Text;                
                cm_obj.vehicle_type_code = ddlVehicleType.SelectedValue;
                cm_obj.manufacturer_code =txtManufacturer.Text;
                cm_obj.makemodel = txtMakeModel.Text;
                cm_obj.vehicle_number = txtVehicleNo.Text;
                cm_obj.chasisno = txtChassisNo.Text;
                cm_obj.registrationno = txtRCNo.Text;
                cm_obj.ownername = txtOwnerName.Text;
                cm_obj.contactno = txtMobileNo.Text;
                cm_obj.permanentaddress = txtOwnerPermanentAddress.Text;
                cm_obj.presentaddress = txtOwnerPresentAddress.Text;
                cm_obj.raidby = Session["RaidBy"].ToString().Substring(0, 1);
                cm_obj.user_id = Session["UserID"].ToString();
                cm_obj.lastmodified_date = DateTime.Now;
                cm_obj.record_status = "N";
                cm_obj.record_deleted = false;
                cm_obj.remarks = txtremarks.Text;
                cm_obj.engineno = txtengno.Text;
                cm_obj.gpscompany = txtGPSCompany.Text;
                cm_obj.imeino = txtIMEI.Text;
                cm_obj.simno = txtSIM.Text;
                if (ddlSDR_CAF.SelectedValue == "Select")
                    cm_obj.SDR_CAF = "No";
                else
                    cm_obj.SDR_CAF = ddlSDR_CAF.SelectedValue;
                string strHostName = System.Net.Dns.GetHostName();
                string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                cm_obj.ipaddress = clientIPAddress.ToString();
                bool val;
                string tempTableId = Session["tableId"]?.ToString() ?? string.Empty;
                if (Session["rtype"].ToString() == "0" && string.IsNullOrEmpty(tempTableId))
                    val = BL_cm_seiz_vehicledetails.Insertvehicledetails(cm_obj);
                else
                {
                    cm_obj.seizure_vehicledetails_id =Convert.ToInt32( Session["tableId"].ToString());
                    val = BL_cm_seiz_vehicledetails.Update(cm_obj);
                }
                if (val == true)
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("VehicleList");
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
        List<cm_seiz_vehicledetails> _vehicledetails = new List<cm_seiz_vehicledetails>();
        protected void btnVehicleSearch_Click(object sender, EventArgs e)
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

                string vno = txtVNo.Text;
                // string vType = drpVtype.SelectedValue;
                if (vno.Trim() != "")
                {
                    _vehicledetails = BL_cm_seiz_vehicledetails.VehicleSearch(vno, "");
                    grdExcisableArticlesView.DataSource = _vehicledetails.ToList();
                    grdExcisableArticlesView.DataBind();
                }
                else
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append("Enter Vehicle Number");
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

                foreach (GridViewRow row in grdExcisableArticlesView.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chselect") as CheckBox);
                        if (chkRow.Checked)
                        {
                           
                            ddlVehicleType.SelectedValue = (row.Cells[0].FindControl("lblVehicleTypeCode") as Label).Text;
                            txtVehicleName.Text = (row.Cells[1].FindControl("lblVehicleName") as Label).Text;
                            txtVehicleNo.Text = (row.Cells[2].FindControl("lblVehicleNo") as Label).Text;
                            txtMakeModel.Text = (row.Cells[3].FindControl("lblModel") as Label).Text;
                            txtManufacturer.Text = (row.Cells[4].FindControl("lblManufacturer") as Label).Text;
                            txtChassisNo.Text = (row.Cells[5].FindControl("lblChassisNo") as Label).Text;
                            txtRCNo.Text = (row.Cells[6].FindControl("lblRegistrationNumber") as Label).Text;
                            txtMobileNo.Text = (row.Cells[8].FindControl("lblMobileNo") as Label).Text;
                            txtOwnerName.Text = (row.Cells[6].FindControl("lblOwnerName") as Label).Text;
                            txtOwnerPermanentAddress.Text = (row.Cells[7].FindControl("lblOwnerPermanentAddress") as Label).Text;
                           
                            break;
                        }
                        else
                        {
                            ddlVehicleType.SelectedValue = "Select";
                            txtVehicleName.Text ="";
                            txtVehicleNo.Text ="";
                            txtMakeModel.Text = "";
                            txtManufacturer.Text ="";
                            txtChassisNo.Text = "";
                            txtRCNo.Text ="";
                            txtMobileNo.Text ="";
                            txtOwnerName.Text = "";
                            txtOwnerPermanentAddress.Text ="";
                        }
                    }
                }

            }
        }

        protected void grdExcisableArticlesView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdExcisableArticlesView.PageIndex = e.NewPageIndex;
            string vno = txtVNo.Text;
            // string vType = drpVtype.SelectedValue;
            _vehicledetails = BL_cm_seiz_vehicledetails.VehicleSearch(vno, "");
            grdExcisableArticlesView.DataSource = _vehicledetails.ToList();
            grdExcisableArticlesView.DataBind();
        }
    }
}