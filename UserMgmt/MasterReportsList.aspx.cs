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
    public partial class MasterReportsList : System.Web.UI.Page
    {
        List<Reportmaster> reportmaster = new List<Reportmaster>();
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
                grdReportList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                Session["UserID"] = Session["UserID"];
                reportmaster = new List<Reportmaster>();
                reportmaster = BL_WorkFlow.GetReportsList();
                grdReportList.DataSource = reportmaster;
                grdReportList.DataBind();
            }
        }
        protected void ReportList_Click(object sender, EventArgs e)
        {

        }
        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "0";
            Response.Redirect("MasterReports");
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "1";
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text;
            string reportname = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblReport_Name") as Label).Text;
            string module = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblmns_no") as Label).Text;
            string stastus = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblstatus") as Label).Text;
            string reportfilename = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblreportfilename") as Label).Text;
            string partytype= (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpartytypecode") as Label).Text;
            Session["module_name"] = module; 
             Session["reportname"] = reportname;
            Session["reportfilename"] = reportfilename;
            Session["partytype"] = partytype;
            Session["id"] = id;
            Session["status"] = stastus;
            Response.Redirect("MasterReports");
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "1";
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text;
            string reportname = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblReport_Name") as Label).Text;
            string module = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblmns_no") as Label).Text;
            string stastus = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblstatus") as Label).Text;
            string reportfilename = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblreportfilename") as Label).Text;
            string partytype = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpartytypecode") as Label).Text;
            Session["module_name"] = module;
            Session["reportname"] = reportname;
            Session["reportfilename"] = reportfilename;
            Session["partytype"] = partytype;
            Session["id"] = id;
            Session["status"] = stastus;
            Response.Redirect("MasterReports");
        }
        protected void grdReportList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (IsPostBack)
            {
                    if (e.NewPageIndex < 0)
                    {
                        grdReportList.PageIndex = 0;
                    }
                    else
                    {
                        grdReportList.PageIndex = e.NewPageIndex;
                    }
                    //user = new UserDetails();
                    //   user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    GridViewRow row = grdReportList.TopPagerRow;
                    TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                    DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
                    if (Session["rptsearch"] != null && Session["rpttext"] != null)
                    {
                        ddsearch.SelectedValue = Session["rptsearch"].ToString();
                        txtpage.Text = Session["rpttext"].ToString();
                        if (ddsearch.SelectedValue != "Select")
                        {
                            if (txtpage != null)
                            {
                            reportmaster = new List<Reportmaster>();
                            reportmaster = BL_WorkFlow.Search("", ddsearch.SelectedValue, txtpage.Text);
                            grdReportList.DataSource = reportmaster;
                            grdReportList.DataBind();
                        }
                        }
                    }
                    else
                    {
                    reportmaster = new List<Reportmaster>();
                reportmaster = BL_WorkFlow.GetReportsList();
                grdReportList.DataSource = reportmaster;
                grdReportList.DataBind();
                    }
                }
        }
        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdReportList.TopPagerRow;
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
                grdReportList.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdReportList.PageIndex = a - 1;
            }
            


            reportmaster = new List<Reportmaster>();
            reportmaster = BL_WorkFlow.GetReportsList();
            grdReportList.DataSource = reportmaster;
            grdReportList.DataBind();
        }

        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdReportList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                Session["UserID"] = Session["UserID"];
                reportmaster = new List<Reportmaster>();
                reportmaster = BL_WorkFlow.GetReportsList();
                grdReportList.DataSource = reportmaster;
                grdReportList.DataBind();
            }
            return reportmaster.ToString();
        }



        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GridViewRow row = grdReportList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["rptsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["rpttext"] = txtpage.Text;
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        Session["UserID"] = Session["UserID"];
                        reportmaster = new List<Reportmaster>();
                        reportmaster = BL_WorkFlow.Search("",ddsearch.SelectedValue,txtpage.Text);
                        grdReportList.DataSource = reportmaster;
                        grdReportList.DataBind();
                    }
                }


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Select Field Name\');", true);
                ddsearch.Focus();
            }



        }


        protected void grdReportList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdReportList.TopPagerRow;
            grdReportList.TopPagerRow.Visible = true;
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
            if (Session["rptsearch"] != null && Session["rpttext"] != null)
            {
                ddsearch.SelectedValue = Session["rptsearch"].ToString();
                txtpages.Text = Session["rpttext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdReportList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdReportList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdReportList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdReportList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdReportList.PageIndex == 0)
            {
                ((ImageButton)grdReportList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdReportList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdReportList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdReportList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdReportList.PageIndex + 1 == grdReportList.PageCount)
            {
                ((ImageButton)grdReportList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdReportList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdReportList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdReportList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["rptsearch"] = null;
            Session["rpttext"] = null;
            Session["UserID"] = Session["UserID"];
            reportmaster = new List<Reportmaster>();
            reportmaster = BL_WorkFlow.GetReportsList();
            grdReportList.DataSource = reportmaster;
            grdReportList.DataBind();
        }
    }
}