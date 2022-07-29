using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class SCMDailyMolassesProductionList : System.Web.UI.Page
    {
        List<DalyMolasses_e> production = new List<DalyMolasses_e>();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    grdSCMDailyMolassesView.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.GetUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        production = new List<DalyMolasses_e>();
                        production = BL_DailyMolassesProduction.GetList(Session["UserID"].ToString());
                        if (Session["UserID"].ToString() == "Admin")
                        {
                            production = new List<DalyMolasses_e>();
                            production = BL_DailyMolassesProduction.GetList(Session["UserID"].ToString());
                            grdSCMDailyMolassesView.DataSource = production;
                            grdSCMDailyMolassesView.DataBind();
                        }
                        else if (user.role_name == "Bond Officer")
                        {
                            var list = from s in production
                                       where s.party_code == user.party_code && s.record_status != "N" && s.financial_year==user.financial_year
                                       orderby Convert.ToDateTime(s.entrydate) descending
                                       select s;
                            grdSCMDailyMolassesView.DataSource = list.ToList();
                            grdSCMDailyMolassesView.DataBind();
                            foreach (GridViewRow dr1 in grdSCMDailyMolassesView.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = false;
                            }
                            btnAddRecord.Visible = false;
                        }
                        else if (user.role_name == "Applicant")
                        {
                            var list = from s in production
                                       where s.party_code == user.party_code && s.financial_year == user.financial_year
                                       orderby Convert.ToDateTime(s.entrydate) descending
                                       select s;
                            grdSCMDailyMolassesView.DataSource = list.ToList();
                            grdSCMDailyMolassesView.DataBind();
                            if (user.role_name == "Bond Officer")
                            {
                                btnAddRecord.Visible = false;
                            }

                        }
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

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = 0;
            Response.Redirect("DailyMolassesProduction");
        }

        protected void btnVATtansfers_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATTransferList");
        }



        protected void btnView_Click(object sender, EventArgs e)
        {
            //  string action_type = "view";
            // Session["action_type"] = action_type;
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string lblPartyCode = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblPartyCode") as Label).Text;
            string creation_date = (gvr.Cells[1].FindControl("lblEntryDate") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["DMfinancial_year"] = financial_year;
            Session["party_code"] = lblPartyCode;
            Session["date"] = creation_date;
            Session["rtype"] = 1;
            Response.Redirect("DailyMolassesProduction");
        }

        protected void btnEdite_Click(object sender, EventArgs e)
        {
            //   string action_type = "Edit";
            //Session["action_type"] = action_type;
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string lblPartyCode = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblPartyCode") as Label).Text;
            string creation_date = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblEntryDate") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["DMfinancial_year"] = financial_year;
            Session["party_code"] = lblPartyCode;
            Session["date"] = creation_date;
            Session["rtype"] = 2;
            Response.Redirect("DailyMolassesProduction");
        }

        protected void btnRG4_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("SCMSugarCanePurchaseRegList");
        }

        protected void btnDMP_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DailyMolassesProductionList");
        }

        protected void btnMIR_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("MolassesIssueRegisterList");
        }

        protected void btnOpeningBalance_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("OpeningBalanceList");
        }

        protected void grdSCMDailyMolassesView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (IsPostBack)
            {
                if (System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    if (e.NewPageIndex == -1)
                    {
                        grdSCMDailyMolassesView.PageIndex = 0;
                    }

                    else {
                        grdSCMDailyMolassesView.PageIndex = e.NewPageIndex;
                    }
                    GridViewRow row = grdSCMDailyMolassesView.TopPagerRow;
                    TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                    DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
                    if (Session["dmosearch"] != null && Session["dmotext"] != null)
                    {
                        ddsearch.SelectedValue = Session["dmosearch"].ToString();
                        txtpage.Text = Session["dmotext"].ToString();
                        if (ddsearch.SelectedValue != "Select")
                        {

                            if (txtpage != null)
                            {
                                if (ddsearch.SelectedValue == "entrydate")
                                {
                                    if (txtpage.Text.ToString().Length == 10)
                                    {
                                        //rawmaterial = BL_RawMaterialReceipt.Search("molassesissueregister", ddsearch.SelectedValue, Convert.ToDateTime(txtpage.Text).ToString("yyyy/MM/dd").Replace("/" ,"-"));
                                        production = new List<DalyMolasses_e>();
                                        production = BL_DailyMolassesProduction.GetList(Session["UserID"].ToString());
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                        txtpage.Focus();
                                        txtpage.Text = "";
                                        ddsearch.SelectedIndex = 0;
                                        production = new List<DalyMolasses_e>();
                                        production = BL_DailyMolassesProduction.GetList(Session["UserID"].ToString());
                                    }

                                }
                                else
                                {
                                    production = new List<DalyMolasses_e>();
                                    production = BL_DailyMolassesProduction.Search("dailymolassesproduction", ddsearch.SelectedValue, txtpage.Text);
                                }
                            }
                        }
                    }
                    else
                    {
                        production = new List<DalyMolasses_e>();
                        production = BL_DailyMolassesProduction.GetList(Session["UserID"].ToString());
                    }



                    //  grdSCMDailyMolassesView.DataSource = production;
                    //  grdSCMDailyMolassesView.DataBind();
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.GetUser(Session["UserID"].ToString());
                    if (user != null)
                    {

                        if (Session["UserID"].ToString() == "Admin")
                        {

                            
                            if (ddsearch.SelectedValue == "entrydate")
                            {
                                var list = from s in production
                                           where s.entrydate == txtpage.Text
                                           orderby Convert.ToDateTime(s.entrydate) descending
                                           select s;
                                grdSCMDailyMolassesView.DataSource = list.ToList();
                                grdSCMDailyMolassesView.DataBind();
                            }
                            else
                            {
                                var list = from s in production

                                           orderby Convert.ToDateTime(s.entrydate) descending
                                           select s;
                                grdSCMDailyMolassesView.DataSource = list.ToList();
                                grdSCMDailyMolassesView.DataBind();
                            }
                        }
                        else if (user.role_name == "Bond Officer")
                        {
                            if (ddsearch.SelectedValue == "entrydate")
                            {
                                var list = from s in production
                                           where s.party_code == user.party_code && s.record_status != "N" && s.entrydate == txtpage.Text && s.financial_year == user.financial_year
                                           orderby Convert.ToDateTime(s.entrydate) descending
                                           select s;
                                grdSCMDailyMolassesView.DataSource = list.ToList();
                                grdSCMDailyMolassesView.DataBind();
                            }
                            else
                            {
                                var list = from s in production
                                           where s.party_code == user.party_code && s.record_status != "N" && s.financial_year == user.financial_year
                                           orderby Convert.ToDateTime(s.entrydate) descending
                                           select s;
                                grdSCMDailyMolassesView.DataSource = list.ToList();
                                grdSCMDailyMolassesView.DataBind();
                            }
                            foreach (GridViewRow dr1 in grdSCMDailyMolassesView.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = false;
                            }
                            btnAddRecord.Visible = false;
                        }
                        else if (user.role_name == "Applicant")
                        {
                            if (ddsearch.SelectedValue == "entrydate")
                            {
                                var list = from s in production
                                           where s.party_code == user.party_code  && s.entrydate == txtpage.Text && s.financial_year == user.financial_year
                                           orderby Convert.ToDateTime(s.entrydate) descending
                                           select s;
                                grdSCMDailyMolassesView.DataSource = list.ToList();
                                grdSCMDailyMolassesView.DataBind();
                            }
                            else
                            { 
                                var list = from s in production
                                           where s.party_code == user.party_code && s.financial_year == user.financial_year
                                           orderby Convert.ToDateTime(s.entrydate) descending
                                           select s;
                            grdSCMDailyMolassesView.DataSource = list.ToList();
                            grdSCMDailyMolassesView.DataBind();
                        }
                            if (user.role_name == "Bond Officer")
                            {
                                btnAddRecord.Visible = false;
                            }

                        }
                    }
                }
            }
        }

        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdSCMDailyMolassesView.TopPagerRow;
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
                grdSCMDailyMolassesView.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdSCMDailyMolassesView.PageIndex = a - 1;
            }

            

            string userid = Session["UserID"].ToString();
            UserDetails user = new UserDetails();
            user = BL_UserDetails.GetUser(Session["UserID"].ToString());
            if (user != null)
            {
                production = new List<DalyMolasses_e>();
                production = BL_DailyMolassesProduction.GetList(Session["UserID"].ToString());
                if (Session["UserID"].ToString() == "Admin")
                {
                    production = new List<DalyMolasses_e>();
                    production = BL_DailyMolassesProduction.GetList(Session["UserID"].ToString());
                    grdSCMDailyMolassesView.DataSource = production;
                    grdSCMDailyMolassesView.DataBind();
                }
                else if (user.role_name == "Bond Officer")
                {
                    var list = from s in production
                               where s.party_code == user.party_code && s.record_status != "N" && s.financial_year == user.financial_year
                               orderby Convert.ToDateTime(s.entrydate) descending
                               select s;
                    grdSCMDailyMolassesView.DataSource = list.ToList();
                    grdSCMDailyMolassesView.DataBind();
                    foreach (GridViewRow dr1 in grdSCMDailyMolassesView.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                    btnAddRecord.Visible = false;
                }
                else if (user.role_name == "Applicant")
                {
                    var list = from s in production
                               where s.party_code == user.party_code && s.financial_year == user.financial_year
                               orderby Convert.ToDateTime(s.entrydate) descending
                               select s;
                    grdSCMDailyMolassesView.DataSource = list.ToList();
                    grdSCMDailyMolassesView.DataBind();
                    if (user.role_name == "Bond Officer")
                    {
                        btnAddRecord.Visible = false;
                    }

                }
            }


        }
        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdSCMDailyMolassesView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                production = new List<DalyMolasses_e>();
                production = BL_DailyMolassesProduction.GetList(Session["UserID"].ToString());
                if (Session["UserID"].ToString() == "Admin")
                {
                    production = new List<DalyMolasses_e>();
                    production = BL_DailyMolassesProduction.GetList(Session["UserID"].ToString());
                    grdSCMDailyMolassesView.DataSource = production;
                    grdSCMDailyMolassesView.DataBind();
                }
                else if (user.role_name == "Bond Officer")
                {
                    var list = from s in production
                               where s.party_code == user.party_code && s.record_status != "N" && s.financial_year == user.financial_year
                               orderby Convert.ToDateTime(s.entrydate) descending
                               select s;
                    grdSCMDailyMolassesView.DataSource = list.ToList();
                    grdSCMDailyMolassesView.DataBind();
                    foreach (GridViewRow dr1 in grdSCMDailyMolassesView.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                    btnAddRecord.Visible = false;
                }
                else if (user.role_name == "Applicant")
                {
                    var list = from s in production
                               where s.party_code == user.party_code && s.financial_year == user.financial_year
                               orderby Convert.ToDateTime(s.entrydate) descending
                               select s;
                    grdSCMDailyMolassesView.DataSource = list.ToList();
                    grdSCMDailyMolassesView.DataBind();
                    if (user.role_name == "Bond Officer")
                    {
                        btnAddRecord.Visible = false;
                    }

                }
            }
            return production.ToString();
        }



        protected void btnsearch_Click(object sender, EventArgs e)
        {


            GridViewRow row = grdSCMDailyMolassesView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["dmosearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["dmotext"] = txtpage.Text;
                    if (ddsearch.SelectedValue == "entrydate")
                    {
                        if (txtpage.Text.ToString().Length == 10)
                        {
                            //rawmaterial = BL_RawMaterialReceipt.Search("molassesissueregister", ddsearch.SelectedValue, Convert.ToDateTime(txtpage.Text).ToString("yyyy/MM/dd").Replace("/" ,"-"));
                            production = new List<DalyMolasses_e>();
                            production = BL_DailyMolassesProduction.GetList(Session["UserID"].ToString());
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                            txtpage.Focus();
                            txtpage.Text = "";
                            ddsearch.SelectedIndex = 0;
                            production = new List<DalyMolasses_e>();
                            production = BL_DailyMolassesProduction.GetList(Session["UserID"].ToString());
                        }

                    }
                    else
                    {

                        production = BL_DailyMolassesProduction.Search("dailymolassesproduction", ddsearch.SelectedValue, txtpage.Text);
                    }

                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        if (Session["UserID"].ToString() == "Admin")
                        {


                            if (ddsearch.SelectedValue == "entrydate")
                            {
                                var list = from s in production
                                           where s.entrydate == txtpage.Text
                                           orderby Convert.ToDateTime(s.entrydate) descending
                                           select s;
                                grdSCMDailyMolassesView.DataSource = list.ToList();
                                grdSCMDailyMolassesView.DataBind();
                            }
                            else
                            {
                                var list = from s in production

                                           orderby Convert.ToDateTime(s.entrydate) descending
                                           select s;
                                grdSCMDailyMolassesView.DataSource = list.ToList();
                                grdSCMDailyMolassesView.DataBind();
                            }
                        }
                        else if (user.role_name == "Bond Officer")
                        {
                            if (ddsearch.SelectedValue == "entrydate")
                            {
                                var list = from s in production
                                           where s.party_code == user.party_code && s.record_status != "N" && s.entrydate == txtpage.Text && s.financial_year == user.financial_year
                                           orderby Convert.ToDateTime(s.entrydate) descending
                                           select s;
                                grdSCMDailyMolassesView.DataSource = list.ToList();
                                grdSCMDailyMolassesView.DataBind();
                            }
                            else
                            {
                                var list = from s in production
                                           where s.party_code == user.party_code && s.record_status != "N" && s.financial_year == user.financial_year
                                           orderby Convert.ToDateTime(s.entrydate) descending
                                           select s;
                                grdSCMDailyMolassesView.DataSource = list.ToList();
                                grdSCMDailyMolassesView.DataBind();
                            }
                            foreach (GridViewRow dr1 in grdSCMDailyMolassesView.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = false;
                            }
                            btnAddRecord.Visible = false;
                        }
                        else if (user.role_name == "Applicant")
                        {
                            if (ddsearch.SelectedValue == "entrydate")
                            {
                                var list = from s in production
                                           where s.party_code == user.party_code && s.entrydate == txtpage.Text && s.financial_year == user.financial_year
                                           orderby Convert.ToDateTime(s.entrydate) descending
                                           select s;
                                grdSCMDailyMolassesView.DataSource = list.ToList();
                                grdSCMDailyMolassesView.DataBind();
                            }
                            else
                            {
                                var list = from s in production
                                           where s.party_code == user.party_code && s.financial_year == user.financial_year
                                           orderby Convert.ToDateTime(s.entrydate) descending
                                           select s;
                                grdSCMDailyMolassesView.DataSource = list.ToList();
                                grdSCMDailyMolassesView.DataBind();
                            }
                            if (user.role_name == "Bond Officer")
                            {
                                btnAddRecord.Visible = false;
                            }

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


        protected void grdSCMDailyMolassesView_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdSCMDailyMolassesView.TopPagerRow;
            if (grdSCMDailyMolassesView.Rows.Count > 0)
            {
                grdSCMDailyMolassesView.TopPagerRow.Visible = true;
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
            if (Session["dmosearch"] != null && Session["dmotext"] != null)
            {
                ddsearch.SelectedValue = Session["dmosearch"].ToString();
                txtpages.Text = Session["dmotext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdSCMDailyMolassesView.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdSCMDailyMolassesView.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdSCMDailyMolassesView.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdSCMDailyMolassesView.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdSCMDailyMolassesView.PageIndex == 0)
            {
                ((ImageButton)grdSCMDailyMolassesView.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdSCMDailyMolassesView.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdSCMDailyMolassesView.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdSCMDailyMolassesView.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdSCMDailyMolassesView.PageIndex + 1 == grdSCMDailyMolassesView.PageCount)
            {
                ((ImageButton)grdSCMDailyMolassesView.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdSCMDailyMolassesView.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdSCMDailyMolassesView.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdSCMDailyMolassesView.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["dmosearch"] = null;
            Session["dmotext"] = null;
            UserDetails user = new UserDetails();
            user = BL_UserDetails.GetUser(Session["UserID"].ToString());
            if (user != null)
            {
                production = new List<DalyMolasses_e>();
                production = BL_DailyMolassesProduction.GetList(Session["UserID"].ToString());
                if (Session["UserID"].ToString() == "Admin")
                {
                    production = new List<DalyMolasses_e>();
                    production = BL_DailyMolassesProduction.GetList(Session["UserID"].ToString());
                    grdSCMDailyMolassesView.DataSource = production;
                    grdSCMDailyMolassesView.DataBind();
                }
                else if (user.role_name == "Bond Officer")
                {
                    var list = from s in production
                               where s.party_code == user.party_code && s.record_status != "N" && s.financial_year == user.financial_year
                               orderby Convert.ToDateTime(s.entrydate) descending
                               select s;
                    grdSCMDailyMolassesView.DataSource = list.ToList();
                    grdSCMDailyMolassesView.DataBind();
                    foreach (GridViewRow dr1 in grdSCMDailyMolassesView.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                    btnAddRecord.Visible = false;
                }
                else if (user.role_name == "Applicant")
                {
                    var list = from s in production
                               where s.party_code == user.party_code && s.financial_year == user.financial_year
                               orderby Convert.ToDateTime(s.entrydate) descending
                               select s;
                    grdSCMDailyMolassesView.DataSource = list.ToList();
                    grdSCMDailyMolassesView.DataBind();
                    if (user.role_name == "Bond Officer")
                    {
                        btnAddRecord.Visible = false;
                    }

                }
            }

            }
        }
}