using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class DocumentFormatList : System.Web.UI.Page
    {
        List<DocumentFormats> doc = new List<DocumentFormats>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strPreviousPage = "";
                Session["UserID"] = Session["UserID"];
                if (Request.UrlReferrer != null)
                {
                    grdDocumentFormatList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                    List<DocumentFormats> doc = new List<DocumentFormats>();
                    doc = BL_DispatchType.GetDocReportList();
                    grdDocumentFormatList.DataSource = doc;
                    grdDocumentFormatList.DataBind();
                    
                }
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/LoginPage");
                }
            }
        }

        protected void AddRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"]= "0";
            Response.Redirect("DocumentFormatForm");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string party_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblPartycode") as Label).Text;
            Session["party_code"] = party_code;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = 1;
            Response.Redirect("DocumentFormatForm");
        }

        protected void grdDocumentFormatList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
          
            if (e.NewPageIndex < 0)
            {
                grdDocumentFormatList.PageIndex = 0;
            }
            else
            {
                grdDocumentFormatList.PageIndex = e.NewPageIndex;
            }
            //user = new UserDetails();
            //   user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            GridViewRow row = grdDocumentFormatList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["doftsearch"] != null && Session["dofttext"] != null)
            {
                ddsearch.SelectedValue = Session["doftsearch"].ToString();
                txtpage.Text = Session["dofttext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        Session["UserID"] = Session["UserID"];
                        doc = BL_DispatchType.Search("", ddsearch.SelectedValue, txtpage.Text);
                        grdDocumentFormatList.DataSource = doc;
                        grdDocumentFormatList.DataBind();
                    }
                }
            }
            else
            {




                List<DocumentFormats> doc = new List<DocumentFormats>();
                doc = BL_DispatchType.GetDocReportList();
                grdDocumentFormatList.DataSource = doc;
                grdDocumentFormatList.DataBind();
            }
        }

        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdDocumentFormatList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            if (Convert.ToInt32(txtpage.Text) < 1)
            {
                a = 0;
            }
            else
            {
                a = Convert.ToInt32(txtpage.Text);
            }
            if (Convert.ToInt32(txtpage.Text) <= 0)
            {
                grdDocumentFormatList.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdDocumentFormatList.PageIndex = a - 1;
            }
            

            List<DocumentFormats> doc = new List<DocumentFormats>();
            doc = BL_DispatchType.GetDocReportList();
            grdDocumentFormatList.DataSource = doc;
            grdDocumentFormatList.DataBind();
        }



        protected void grdDocumentFormatList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdDocumentFormatList.TopPagerRow;
            if (grdDocumentFormatList.Rows.Count > 0)
            {
                grdDocumentFormatList.TopPagerRow.Visible = true;
            }
            if (row == null)
            {
                return;
            }

            DropDownList DDLPage = (DropDownList)row.Cells[0].FindControl("DDLPage");
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            Label lblPages = (Label)row.Cells[0].FindControl("lblPages");
            Label lblCurrent = (Label)row.Cells[0].FindControl("lblCurrent");
            TextBox txtpages = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["doftsearch"] != null && Session["dofttext"] != null)
            {
                ddsearch.SelectedValue = Session["doftsearch"].ToString();
                txtpages.Text = Session["dofttext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdDocumentFormatList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdDocumentFormatList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdDocumentFormatList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdDocumentFormatList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdDocumentFormatList.PageIndex == 0)
            {
                ((ImageButton)grdDocumentFormatList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdDocumentFormatList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdDocumentFormatList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdDocumentFormatList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdDocumentFormatList.PageIndex + 1 == grdDocumentFormatList.PageCount)
            {
                ((ImageButton)grdDocumentFormatList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdDocumentFormatList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdDocumentFormatList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdDocumentFormatList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }
        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdDocumentFormatList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                Session["UserID"] = Session["UserID"];
                
                doc = BL_DispatchType.GetDocReportList();
                grdDocumentFormatList.DataSource = doc;
                grdDocumentFormatList.DataBind();
            }
            return doc.ToString();
        }



        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GridViewRow row = grdDocumentFormatList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["doftsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["dofttext"] = txtpage.Text;
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        Session["UserID"] = Session["UserID"];
                        doc = BL_DispatchType.Search("",ddsearch.SelectedValue,txtpage.Text);
                        grdDocumentFormatList.DataSource = doc;
                        grdDocumentFormatList.DataBind();
                    }
                }


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Select Field Name\');", true);
                ddsearch.Focus();
            }



        }


        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["doftsearch"] = null;
            Session["dofttext"] = null;
            Session["UserID"] = Session["UserID"];
            doc = BL_DispatchType.GetDocReportList();
            grdDocumentFormatList.DataSource = doc;
            grdDocumentFormatList.DataBind();
        }
    }
}