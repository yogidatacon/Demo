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
    public partial class WebForm12 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(Request.QueryString["x"]) || string.IsNullOrWhiteSpace(Request.QueryString["y"]))
            {
                // not there!
            }

            else
            {
                string a = Request.QueryString["x"].ToString();
                string b = Session["AllotmentNo"].ToString();
                if (a == "ok")
                {
                    Pass_Details pass = new Pass_Details();
                    pass.carrier = Session["carrier"].ToString();
                    pass.vehicle_no = Session["vehicle"].ToString();
                    pass.vehicle_type = Session["vehicle_type"].ToString();
                    pass.driver = Session["driver"].ToString();
                    pass.challan_no = Session["challan_no"].ToString();
                    pass.digital_lock_no = Session["digital_lock_no"].ToString();
                    pass.dispatch_date = Session["dispatch_date"].ToString();
                    pass.dispatch_time = Session["dispatch_time"].ToString();
                    pass.dispatch_duration = Session["dispatch_duration"].ToString();
                    pass.route_details = Session["route_details"].ToString();
                    pass.pass_id = Session["pass_id"].ToString();
                    pass.request_for_pass_id = Session["request_for_pass_id"].ToString();
                    pass.party_code = Session["party_code"].ToString();
                    pass.to_dispatch_vat = Session["to_dispatch_vat"].ToString();
                    pass.available_qty = Session["available_qty"].ToString();
                    pass.record_status = "I";
                    pass.pass_type = Session["ptype"].ToString();
                    pass.rrnoc_record_request_id = Session["rrnoc_record_request_id"].ToString();
                    pass.noc_depotdetail_id = Session["noc_depotdetail_id"].ToString();
                    pass.dispatch_qty = Convert.ToDouble(Session["dispatch_qty"].ToString());
                    pass.approver_remarks = Session["approver_remarks"].ToString();
                    pass.user_id = Session["UserID"].ToString();
                    string val = BL_Pass_Details.Issue(pass);
                    if (val == "0")
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("PassList");
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
                else if (a == "failed")
                {
                    //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Digital Signature Not Matching');", true);
                    ClientScript.RegisterStartupScript(GetType(), "someKey", "alert('oops');", true);
                    Session["UserID"] = Session["UserID"].ToString();
                    Response.Redirect("PassList");
                }
            }

        }
    }
}