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
    public partial class VehicleMasterForm : System.Web.UI.Page
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
                    string CasteId = Session["VehicleTypeId"].ToString();
                    txtid.Value = CasteId;
                    string v = Session["VehicleTypeCode"].ToString();
                    int n = Convert.ToInt32(BL_org_Master.GetMaxID("vehicle_type&"+v));
                    if (n > 0)
                    {
                        txtVehicleTypeCode.Attributes.Add("disabled", "disabled");
                        txtVehicleType.Attributes.Add("disabled", "disabled");
                        btnCancel.Visible = false;
                        btnSave.Visible = false;
                    }
                    if (rtype == "1")
                    {
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                       txtVehicleType.Text = Session["VehicleTypeName"].ToString();
                        txtVehicleTypeCode.Text = Session["VehicleTypeCode"].ToString();
                        txtid.Value = Session["VehicleTypeId"].ToString();
                        txtVehicleType.ReadOnly = true;
                        txtVehicleTypeCode.ReadOnly = true;

                    }
                    if (rtype == "2")
                    {
                        txtVehicleType.Text = Session["VehicleTypeName"].ToString();
                        txtVehicleTypeCode.Text = Session["VehicleTypeCode"].ToString();
                        txtid.Value = Session["VehicleTypeId"].ToString();
                    }
                }
                else
                {
                    int n = Convert.ToInt32(BL_org_Master.GetMaxID("vehicle_type_master"));
                    txtVehicleTypeCode.Text ="V"+string.Format("{0:00}", (n + 1));
                    Session["VehicleTypeId"] = txtid.Value;
                    btnSave.Text = "Submit";
                    btnCancel.Text = "Cancel";
                }
            }
        }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {

            Response.Redirect("VehicleMasterList");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            cm_Vehicle_type cm_obj = new cm_Vehicle_type();
            if (string.IsNullOrEmpty(txtVehicleTypeCode.Text) || string.IsNullOrWhiteSpace(txtVehicleTypeCode.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Seizure Stage Code\');", true);
                return;
            }
            if (string.IsNullOrEmpty(txtVehicleType.Text) || string.IsNullOrWhiteSpace(txtVehicleType.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Seizure Stage Name\');", true);
                return;
            }
            cm_obj.vehicle_type_code = txtVehicleTypeCode.Text;
            cm_obj.vehicle_type = txtVehicleType.Text;
            cm_obj.user_id = Session["UserID"].ToString();
           cm_obj.lastmodified_date = DateTime.Now.ToShortDateString();
            cm_obj.record_status = "Y";
            cm_obj.record_deleted = false;
            if (Session["rtype"].ToString() != "0")
            {
                cm_obj.vehicle_type_id = Convert.ToInt32(Session["VehicleTypeId"].ToString());
                if (BL_cm_Vehicle_type.UpdateVehicletype(cm_obj))
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
                    Response.Redirect("~/VehicleMasterList");
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

                if (BL_cm_Vehicle_type.InsertVehicleType(cm_obj))
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
                    Response.Redirect("~/VehicleMasterList");
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
            Response.Redirect("~/VehicleMasterList");
        }

        protected void txtVehicleTypeCode_TextChanged(object sender, EventArgs e)
        {
            if (txtVehicleTypeCode.Text != "")
            {
                int value = BL_User_Mgnt.GetExistsData("vehicle_type_master", "vehicle_type_code", txtVehicleTypeCode.Text);
                if (value > 0)
                {
                    string message = "Vehicle Type Code  is Already Exists.";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    txtVehicleTypeCode.Text = "";
                    txtVehicleTypeCode.Focus();
                }
            }
        }
    }
}