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
    public partial class MolassesProvisionalProductionList : System.Web.UI.Page
    {
        List<Molasses_Production_MF2> mf = new List<Molasses_Production_MF2>();

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
                        grdMolassesProvisionalProduction.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                       
                        mf = BL_Molasses_Production_MF2.GetList();
                        var list = from s in mf
                                   where s.party_code == user.party_code && s.financial_year==user.financial_year
                                   select s;
                        grdMolassesProvisionalProduction.DataSource = list.ToList();
                        grdMolassesProvisionalProduction.DataBind();
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
            Response.Redirect("MolassesProvisionalProductionForm");
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
            Session["MPPfinancial_year"] = financial_year;
            Session["UserID"] = Session["UserID"].ToString();
            Session["mpid"] = mpid;
            Session["rtype"] = 2;
            Response.Redirect("MolassesProvisionalProductionForm");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string mpid = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblFiancialYear") as Label).Text;
            Session["MPPfinancial_year"] = financial_year;
            Session["UserID"] = Session["UserID"].ToString();
            Session["mpid"] = mpid;
            Session["rtype"] = 1;
            Response.Redirect("MolassesProvisionalProductionForm");
        }

        protected void grdMolassesProvisionalProduction_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            if (e.NewPageIndex < 0)
            {
                grdMolassesProvisionalProduction.PageIndex = 0;
            }
            else
            {
                grdMolassesProvisionalProduction.PageIndex = e.NewPageIndex;
            }
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            GridViewRow row = grdMolassesProvisionalProduction.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");

            if (Session["mf2search"] != null && Session["mf2text"] != null)
            {
                ddsearch.SelectedValue = Session["mf2search"].ToString();
                txtpage.Text = Session["mf2text"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        if (ddsearch.SelectedValue == "entry_date")
                        {
                            if (txtpage.Text.ToString().Length == 10)
                            {

                                mf = BL_Molasses_Production_MF2.GetList();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                txtpage.Focus();
                                txtpage.Text = "";
                                ddsearch.SelectedIndex = 0;

                                mf = BL_Molasses_Production_MF2.GetList();
                            }

                        }
                        else
                        {
                            mf = BL_Molasses_Production_MF2.Search("", ddsearch.SelectedValue, txtpage.Text);
                        }
                    }
                }
            }


            else
            {
                mf = BL_Molasses_Production_MF2.GetList();
            }
        
          
            if (ddsearch.SelectedValue == "entry_date")
            {
                var list = from s in mf
                           where s.party_code == user.party_code && s.entry_date == txtpage.Text && s.financial_year == user.financial_year
                           select s;
                grdMolassesProvisionalProduction.DataSource = list.ToList();
                grdMolassesProvisionalProduction.DataBind();
            }
            else
            {
                var list = from s in mf
                           where s.party_code == user.party_code && s.financial_year == user.financial_year
                           select s;
                grdMolassesProvisionalProduction.DataSource = list.ToList();
                grdMolassesProvisionalProduction.DataBind();
            }
        }

        protected void btnprint_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string MF2_No = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblFiancialYear") as Label).Text;
            Session["MPPfinancial_year"] = financial_year;
            Session["ReportId"] = "MF2";
            Session["UserID"] = Session["UserID"].ToString();
            Session["MF2_No"] = MF2_No;
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('ReportPage' ,'_blank');", true);
        }
        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdMolassesProvisionalProduction.TopPagerRow;
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
                grdMolassesProvisionalProduction.PageIndex = a - 1;
            }
            else
            {
                grdMolassesProvisionalProduction.PageIndex = a;
            }
           

            string userid = Session["UserID"].ToString();
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            List<Molasses_Production_MF2> mf2 = new List<Molasses_Production_MF2>();
            mf2 = BL_Molasses_Production_MF2.GetList();
            var list = from s in mf2
                       where s.party_code == user.party_code
                       select s;
            grdMolassesProvisionalProduction.DataSource = list.ToList();
            grdMolassesProvisionalProduction.DataBind();


        }

        protected void grdMolassesProvisionalProduction_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdMolassesProvisionalProduction.TopPagerRow;
            if (grdMolassesProvisionalProduction.PageCount != 0)
            {
                grdMolassesProvisionalProduction.TopPagerRow.Visible = true;
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

            if (Session["mf2search"] != null && Session["mf2text"] != null)
            {
                ddsearch.SelectedValue = Session["nocrsearch"].ToString();
                txtpages.Text = Session["mf2text"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdMolassesProvisionalProduction.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdMolassesProvisionalProduction.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdMolassesProvisionalProduction.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdMolassesProvisionalProduction.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdMolassesProvisionalProduction.PageIndex == 0)
            {
                ((ImageButton)grdMolassesProvisionalProduction.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdMolassesProvisionalProduction.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdMolassesProvisionalProduction.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdMolassesProvisionalProduction.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdMolassesProvisionalProduction.PageIndex + 1 == grdMolassesProvisionalProduction.PageCount)
            {
                ((ImageButton)grdMolassesProvisionalProduction.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdMolassesProvisionalProduction.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdMolassesProvisionalProduction.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdMolassesProvisionalProduction.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }

        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GridViewRow row = grdMolassesProvisionalProduction.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {
                Session["mf2search"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["mf2text"] = txtpage.Text;
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        if (ddsearch.SelectedValue == "entry_date")
                        {
                            if (txtpage.Text.ToString().Length == 10)
                            {

                                mf = BL_Molasses_Production_MF2.GetList();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                txtpage.Focus();
                                txtpage.Text = "";
                                ddsearch.SelectedIndex = 0;
                                mf = BL_Molasses_Production_MF2.GetList();
                            }
                        }
                        else
                        {

                            mf = BL_Molasses_Production_MF2.Search("", ddsearch.SelectedValue, txtpage.Text);
                        }

                        if (ddsearch.SelectedValue == "entry_date")
                        {
                            var list = from s in mf
                                       where s.party_code == user.party_code && s.entry_date == txtpage.Text && s.financial_year == user.financial_year
                                       select s;
                            grdMolassesProvisionalProduction.DataSource = list.ToList();
                            grdMolassesProvisionalProduction.DataBind();
                        }
                        else
                        {
                            var list = from s in mf
                                       where s.party_code == user.party_code && s.financial_year == user.financial_year
                                       select s;
                            grdMolassesProvisionalProduction.DataSource = list.ToList();
                            grdMolassesProvisionalProduction.DataBind();
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
            Session["mf2search"] = null;
            Session["mf2text"] = null;
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            List<Molasses_Production_MF2> mf2 = new List<Molasses_Production_MF2>();
            mf2 = BL_Molasses_Production_MF2.GetList();
            var list = from s in mf2
                       where s.party_code == user.party_code
                       select s;
            grdMolassesProvisionalProduction.DataSource = list.ToList();
            grdMolassesProvisionalProduction.DataBind();
        }
    }
}