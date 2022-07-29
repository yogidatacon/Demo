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
    public partial class DigitalSignatureList : System.Web.UI.Page
    {
        List<DigitalSignature> DigitalSignature = new List<DigitalSignature>();
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
                Session["UserID"] = Session["UserID"];

                DigitalSignature = BL_DigitalSignature.GetList();
                grdDigitalSignature.DataSource = DigitalSignature.ToList();
                grdDigitalSignature.DataBind();
            }
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["rtype"] = 0;
            Response.Redirect("DigitalSignatureForm");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string ID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblID") as Label).Text;
            Session["UserId"] = Session["UserID"].ToString();
            Session["rtype"] = "1";
            Session["Digi_id"] = ID;
            Response.Redirect("DigitalSignatureForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string ID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblID") as Label).Text;
            Session["UserId"] = Session["UserID"].ToString();
            Session["rtype"] = "2";
            Session["Digi_id"] = ID;
            Response.Redirect("DigitalSignatureForm");
        }
       

        protected void grdDigitalSignature_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           grdDigitalSignature.PageIndex = e.NewPageIndex;
            DigitalSignature = BL_DigitalSignature.GetList();
            grdDigitalSignature.DataSource = DigitalSignature.ToList();
            grdDigitalSignature.DataBind();
        }
    }
}