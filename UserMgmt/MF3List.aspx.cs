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
    public partial class MF3List : System.Web.UI.Page
    {
        List<MF3_Details> mf = new List<MF3_Details>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];

                    Session["UserID"] = Session["UserID"];
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        grdMolassesProductionActualList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                        List<MF3_Details> mf3 = new List<MF3_Details>();
                        mf3 = BL_MF3_Details.GetList();
                        var list = from s in mf3
                                   where s.party_code == user.party_code && s.financial_year==user.financial_year
                                   select s;
                        grdMolassesProductionActualList.DataSource = list.ToList();
                        grdMolassesProductionActualList.DataBind();
                    }
                    else
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append("Database Server Not Connecting");
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    }

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
            Session["rtype"] = 0;
            Response.Redirect("MF3Form");
        }

        protected void MF2_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("MolassesProvisionalProductionList");
        }

        protected void MF3_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("MF3List");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string mpid = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblFiancialYear") as Label).Text;
            Session["MF3financial_year"] = financial_year;
            Session["UserID"] = Session["UserID"].ToString();
            Session["mpid"] = mpid;
            Session["rtype"] = 2;
            Response.Redirect("MF3Form");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string mpid = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblFiancialYear") as Label).Text;
            Session["MF3financial_year"] = financial_year;
            Session["UserID"] = Session["UserID"].ToString();
            Session["mpid"] = mpid;
            Session["rtype"] = 1;
            Response.Redirect("MF3Form");
        }

        protected void grdMolassesProductionActualList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0)
            {
                grdMolassesProductionActualList.PageIndex = 0;
            }
            else
            {
                grdMolassesProductionActualList.PageIndex = e.NewPageIndex;
            }
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            GridViewRow row = grdMolassesProductionActualList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");

            if (Session["mf3search"] != null && Session["mf3text"] != null)
            {
                ddsearch.SelectedValue = Session["mf3search"].ToString();
                txtpage.Text = Session["mf3text"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        if (ddsearch.SelectedValue == "crushing_closedate")
                        {
                            if (txtpage.Text.ToString().Length == 10)
                            {

                                mf = BL_MF3_Details.GetList();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                txtpage.Focus();
                                txtpage.Text = "";
                                ddsearch.SelectedIndex = 0;

                                mf = BL_MF3_Details.GetList();
                            }

                        }
                        else
                        {
                            mf = BL_MF3_Details.Search("", ddsearch.SelectedValue, txtpage.Text);
                        }
                    }
                }
            }


            else
            {
                mf = BL_MF3_Details.GetList();
            }



            if (ddsearch.SelectedValue == "crushing_closedate")
            {
                var list = from s in mf
                           where s.party_code == user.party_code && s.crushing_closedate == txtpage.Text && s.financial_year == user.financial_year
                           select s;
                grdMolassesProductionActualList.DataSource = list.ToList();
                grdMolassesProductionActualList.DataBind();
            }
            else
            {
                var list = from s in mf
                           where s.party_code == user.party_code && s.financial_year == user.financial_year
                           select s; 
                grdMolassesProductionActualList.DataSource = list.ToList();
                grdMolassesProductionActualList.DataBind();
            }
        }
        protected void btnprint_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string MF3_No = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblFiancialYear") as Label).Text;
            Session["MF3financial_year"] = financial_year;
            Session["ReportId"] = "MF3";
            Session["UserID"] = Session["UserID"].ToString();
            Session["MF3_No"] = MF3_No;
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('ReportPage' ,'_blank');", true);
        }
        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdMolassesProductionActualList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            if (Convert.ToInt32(txtpage.Text) < 1)
            {
                a = 0;
            }
            else
            {
                a = Convert.ToInt32(txtpage.Text);
            }
            if (a != 0)
            {
                grdMolassesProductionActualList.PageIndex = a - 1;
            }
            else
            {
                grdMolassesProductionActualList.PageIndex = a;
            }
          

            string userid = Session["UserID"].ToString();
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            List<MF3_Details> mf3 = new List<MF3_Details>();
            mf3 = BL_MF3_Details.GetList();
            grdMolassesProductionActualList.DataSource = mf3;
            grdMolassesProductionActualList.DataBind();


        }

        protected void grdMolassesProductionActualList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdMolassesProductionActualList.TopPagerRow;
            if (grdMolassesProductionActualList.PageCount != 0)
            {
                grdMolassesProductionActualList.TopPagerRow.Visible = true;
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

            if (Session["mf3search"] != null && Session["mf3text"] != null)
            {
                ddsearch.SelectedValue = Session["mf3search"].ToString();
                txtpages.Text = Session["mf3text"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdMolassesProductionActualList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdMolassesProductionActualList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdMolassesProductionActualList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdMolassesProductionActualList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdMolassesProductionActualList.PageIndex == 0)
            {
                ((ImageButton)grdMolassesProductionActualList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdMolassesProductionActualList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdMolassesProductionActualList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdMolassesProductionActualList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdMolassesProductionActualList.PageIndex + 1 == grdMolassesProductionActualList.PageCount)
            {
                ((ImageButton)grdMolassesProductionActualList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdMolassesProductionActualList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdMolassesProductionActualList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdMolassesProductionActualList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }

        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GridViewRow row = grdMolassesProductionActualList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {
                Session["mf3search"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["mf3text"] = txtpage.Text;
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        if (ddsearch.SelectedValue == "crushing_closedate")
                        {
                            if (txtpage.Text.ToString().Length == 10)
                            {

                                mf = BL_MF3_Details.GetList();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                txtpage.Focus();
                                txtpage.Text = "";
                                ddsearch.SelectedIndex = 0;
                                mf = BL_MF3_Details.GetList();
                            }
                        }
                        else
                        {

                            mf = BL_MF3_Details.Search("", ddsearch.SelectedValue, txtpage.Text);
                        }

                        if (ddsearch.SelectedValue == "crushing_closedate")
                        {
                            var list = from s in mf
                                       where s.party_code == user.party_code && s.crushing_closedate == txtpage.Text && s.financial_year == user.financial_year
                                       select s;
                            grdMolassesProductionActualList.DataSource = list.ToList();
                            grdMolassesProductionActualList.DataBind();
                        }
                        else
                        {
                            var list = from s in mf
                                       where s.party_code == user.party_code && s.financial_year == user.financial_year
                                       select s;
                            grdMolassesProductionActualList.DataSource = list.ToList();
                            grdMolassesProductionActualList.DataBind();
                        }
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
            Session["mf3search"] = null;
            Session["mf3text"] = null;
            string userid = Session["UserID"].ToString();
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            List<MF3_Details> mf3 = new List<MF3_Details>();
            mf3 = BL_MF3_Details.GetList();
            grdMolassesProductionActualList.DataSource = mf3;
            grdMolassesProductionActualList.DataBind();
        }
    }
}