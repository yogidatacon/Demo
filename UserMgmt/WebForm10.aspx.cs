using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class WebForm10 : System.Web.UI.Page
    {
        DataTable depot = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewState["DepotRecords"] == null)
            {
                depot.Columns.Add("Depot_Name");
                depot.Columns.Add("QTY");
                depot.Columns.Add("AppQTY");
                depot.Columns.Add("Depot_id");
                depot.Columns.Add("id");
                ViewState["DepotRecords"] = depot;
            }
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

                    NOC_Details noc = new NOC_Details();
                    noc.noc_total_qty = 0;
                    string val = "";
                    int j = 0;
                    ViewState["DepotRecords"] = (DataTable)Session["depot"];
                    depot = ViewState["DepotRecords"] as DataTable;
                    noc.depot = new List<NOC_Depot>();
                    foreach (DataRow dr in depot.Rows)
                    {
                        NOC_Depot dep = new NOC_Depot();
                        dep.Depot_name = dr["Depot_Name"].ToString();
                        dep.qty = Convert.ToDouble(dr["QTY"].ToString());
                        dep.Depot_id = dr["Depot_id"].ToString();
                        noc.noc_total_qty = noc.noc_total_qty + Convert.ToDouble(dep.qty);
                        noc.depot.Add(dep);
                        j++;
                    }
                    noc.noc_id = Session["ID"].ToString();
                    noc.approverlevel  =Convert.ToInt32( Session["approverlevel"].ToString());
                    noc.approver_remarks = Session["approver_remarks"].ToString();
                    noc.noc_status  = Session["noc_status"].ToString();
                    noc.record_status  = Session["record_status"].ToString();
                    noc.valid_upto =   Session["valid_upto"].ToString();
                    val = BL_NOC_Details.Approve(noc);
                    if (val == "0")
                    {
                        Response.Redirect("NOC_P");
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
                    Response.Redirect("NOC_P");
                }
            }
             
        }
    }
}