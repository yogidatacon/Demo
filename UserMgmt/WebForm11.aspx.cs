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
    public partial class WebForm11 : System.Web.UI.Page
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
                    Release_Request rr = new Release_Request();
                    string val;
                    rr.record_status = "A";
                    rr.release_request_id = Session["request_id"].ToString();
                    rr.approval_level = Session["level"].ToString();
                    rr.approval_status = Session["approval_status"].ToString();
                    rr.remarks = Session["remarks"].ToString();
                    rr.user_id = Session["UserID"].ToString();
                    rr.valid_date = Session["valid_date"].ToString();
                    rr.rr_balance_qty =Convert.ToDouble( Session["balance_qty"].ToString());
                    Response.Redirect("ReleaseRequestAppliedList");
                    val = BL_Release_Request.Approve(rr);
                    if (val == "0")
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("ReleaseRequestAppliedList");
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
                    Response.Redirect("ReleaseRequestAppliedList");
                }
            }

        }
    }
}