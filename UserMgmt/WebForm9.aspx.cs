using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace WebApplication1
{
    public partial class WebForm9 : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            //string x = Request.QueryString["x"].ToString();

            //Response.Write(x);
            if (ViewState["Records"] == null)
            {
                dt.Columns.Add("Status");
                dt.Columns.Add("Doc_Name");
                dt.Columns.Add("Discription");
                dt.Columns.Add("Doc_Path");
                dt.Columns.Add("Doc_id");
                ViewState["Records"] = dt;
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
                    Molasses_Allocation allotment = new Molasses_Allocation();
                    allotment.molasses_allotment_request_id = Session["AllotmentNo"].ToString();
                    allotment.qty_allotted_till_date = Convert.ToDouble(Session["allotmentqty"].ToString());
                    // allotment.qty_allotted_till_date=Convert.ToDouble(appqty.Value);
                    allotment.remarks = Session["remarks"].ToString();
                    allotment.product_name = Session["product_name"].ToString();
                    allotment.record_status = "Y";
                    allotment.user_id = Session["UserID"].ToString();
                    int i = 0;
                    Session["UserID"] = Session["UserID"].ToString();
                    ViewState["Records"] = (DataTable)Session["griddata"];
                    dt = ViewState["Records"] as DataTable;
                    allotment.docs = new List<EASCM_DOCS>();
                    foreach (DataRow dr in dt.Rows)
                    {

                        if (dr["Status"].ToString() == "")
                        {
                            EASCM_DOCS doc = new EASCM_DOCS();
                            doc.doc_name = dr["Doc_Name"].ToString();
                            doc.doc_path = dr["Doc_path"].ToString();
                            doc.description = dr["Discription"].ToString();
                            // approversummary = approversummary + "{!}" + doc.doc_name + "{!}" + doc.doc_path + "{!}" + doc.description;
                            allotment.docs.Add(doc);
                        }
                        i++;
                    }
                    string val;

                    val = BL_Molasses_Allocation.Approve(allotment);

                    if (val == "0")
                    {
                        Session["UserID"] = Session["UserID"].ToString();
                        Response.Redirect("Allocation_P");
                    }
                    else
                    {
                        string message = "Server Error";
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

                else if (a == "failed")
                {
                    //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Digital Signature Not Matching');", true);
                    ClientScript.RegisterStartupScript(GetType(), "someKey", "alert('oops');", true);
                    Session["UserID"] = Session["UserID"].ToString();
                    Response.Redirect("Allocation_P");
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string script = string.Format("sessionStorage.userId= '{0}';", "com");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "key", script, true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('http://www.google.com','_newtab');", true);
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('HtmlPage1.html' ,'');", true);

            
        }
    }
}