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
    public partial class DocumentFormatForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string strPreviousPage = "";
                Session["UserID"] = Session["UserID"];
                if (Request.UrlReferrer != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];

                    List<Party_Master> partymasters = new List<Party_Master>();
                    partymasters = BL_Party_Master.GetList();
                    ddlPartyName.DataSource = partymasters;
                    ddlPartyName.DataTextField = "Party_name";
                    ddlPartyName.DataValueField = "Party_code";
                    ddlPartyName.DataBind();
                    ddlPartyName.Items.Insert(0, "Select");
                    if(Session["rtype"].ToString()=="1")
                    {
                        ddlPartyName.SelectedValue = Session["party_code"].ToString();
                        List<DocumentFormats> doc = new List<DocumentFormats>();
                        doc = BL_DispatchType.GetDocReportList();
                        ddlPartyName.Enabled = false;
                        var product = (from s in doc
                                       where s.party_code ==ddlPartyName.SelectedValue
                                       select s);
                        txtNoc.Text = product.ToList()[0].noc;
                        txtPass.Text = product.ToList()[0].pass;
                      txtReleaseRequest.Text = product.ToList()[0].release_request;
                        txtMolassesAllotment.Text = product.ToList()[0].molasses_allotment;
                        txtpermit.Text = product.ToList()[0].permit;
                    }
                }
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/LoginPage");
                }
            }
          
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DocumentFormatList");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                DocumentFormats doc = new DocumentFormats();
                doc.party_code = ddlPartyName.SelectedValue;
                doc.noc = txtNoc.Text.ToUpper();
                doc.release_request = txtReleaseRequest.Text.ToUpper();
                doc.pass = txtPass.Text.ToUpper();
                doc.molasses_allotment = txtMolassesAllotment.Text.ToUpper();
                doc.permit = txtpermit.Text.ToUpper();
                string val = BL_DispatchType.InsertDoc(doc);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("DocumentFormatList");
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DocumentFormatList");
        }

        protected void ddReportName_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}