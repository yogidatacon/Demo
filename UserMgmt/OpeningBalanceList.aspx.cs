using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class OpeningBalanceList : System.Web.UI.Page
    {
        List<OpeningBalance> openingbalance = new List<OpeningBalance>();
        List<Party_Master> party = new List<Party_Master>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                }
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/LoginPage");
                }
                //grdopeningbalanceList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                string userid = Session["UserID"].ToString();
                UserDetails user = new UserDetails();
                user = BL_UserDetails.GetUser(userid);
                if (user != null)
                {
                    Session["rolename"] = user.role_name;
                    Session["party_code"] = user.party_code;
                    if (user.party_type == "M & tP"|| user.party_code == "MTR" || user.party_code == "MTW")
                    {
                        MTP.Visible = true;
                        SCM.Visible = false;
                        if (user.party_code == "MTR" || user.party_code == "MTW")
                        {
                            btnConsumption.Visible = false;
                        }
                    }
                    else
                    {
                        MTP.Visible = false;
                        SCM.Visible = true;
                    }
                    if (user.party_type == "Distillery Unit" || user.party_type == "ENA Distillery Unit")
                        sgr.Visible = false;
                    else
                        dst.Visible = false;
                    openingbalance = new List<OpeningBalance>();
                    openingbalance = BL_OpeningBalance.GetOpeningBalanceList(Session["party_code"].ToString(), "");
                    if (userid == "Admin")
                    {
                        SCM.Visible = true;
                        if (Session["party_type"].ToString() == "SGR")
                        {
                            dst.Visible = false;
                            sgr.Visible = true;
                            string a = "Sugar Mill";
                            openingbalance = BL_OpeningBalance.GetOpeningBalanceList(user.party_code, a);
                           
                        }
                        else
                        {
                            string a = "Distillery Unit";
                            openingbalance = BL_OpeningBalance.GetOpeningBalanceList(user.party_code, a);
                            dst.Visible = true;
                            sgr.Visible = false;
                        }
                        var partynames = (from s in openingbalance
                                          select new { party_code = s.party_code, party_name = s.party_name, record_status = s.record_status, financial_year = s.financial_year }).Distinct();
                        grdopeningbalanceList.DataSource = partynames.ToList();
                        grdopeningbalanceList.DataBind();
                      
                    }
                    else if (user.role_name == "Bond Officer")
                    {
                        btnAddRecord.Visible = false;
                        var partynames = (from s in openingbalance
                                          where s.party_code == user.party_code && s.record_status != "N" && s.financial_year == user.financial_year
                                          select new { party_code = s.party_code, party_name = s.party_name, record_status = s.record_status, financial_year = s.financial_year }).Distinct();
                        grdopeningbalanceList.DataSource = partynames.ToList();
                        grdopeningbalanceList.DataBind();
                        foreach (GridViewRow dr1 in grdopeningbalanceList.Rows)
                        {
                            LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                            btn.Visible = false;
                        }
                        btnAddRecord.Visible = false;
                    }
                    else
                    {
                       int value =BL_OpeningBalance.GetExistsData("openingbalance", "party_code", user.party_code);
                        if(value ==1)
                        {
                            var partynames = (from s in openingbalance
                                              where s.party_code == user.party_code && s.financial_year == user.financial_year
                                              orderby s.vat_code ascending
                                              select new { party_code = s.party_code, party_name = s.party_name, record_status = s.record_status, financial_year = s.financial_year }).Distinct();

                            grdopeningbalanceList.DataSource = partynames.ToList();
                            grdopeningbalanceList.DataBind();
                            int n = 0;

                            if (partynames.ToList().Count > 0)
                            {
                                btnAddRecord.Visible = false;
                                //foreach (GridViewRow dr1 in grdopeningbalanceList.Rows)
                                //{

                                //    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                //    btn.Visible = false;
                                //}
                            }
                        }
                        else
                        {
                            var partynames = (from s in openingbalance
                                              where s.party_code == user.party_code 
                                              orderby s.vat_code ascending
                                              select new { party_code = s.party_code, party_name = s.party_name, record_status = s.record_status,financial_year=s.financial_year }).Distinct();

                            grdopeningbalanceList.DataSource = partynames.ToList();
                            grdopeningbalanceList.DataBind();
                            int n = 0;

                            if (partynames.ToList().Count > 0)
                            {
                                btnAddRecord.Visible = false;
                                //foreach (GridViewRow dr1 in grdopeningbalanceList.Rows)
                                //{

                                //    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                //    btn.Visible = false;
                                //}
                            }
                        }
                     
                    }

                  
                    //openingbalance = BL_OpeningBalance.GetOpeningBalanceList(Session["party_code"].ToString(), "");
                    //var partynames1 = from s in openingbalance
                    //                 where s.party_code == user.party_code && s.financial_year == user.financial_year
                    //                 select s;
                    //if (partynames1.ToList()[0].record_status != "A")
                    //{
                    //    lnkRMR.Visible = false;
                    //    lnkRawMaterialToFermenter.Visible = false;
                    //    lnkFermentertoReceiver.Visible = false;
                    //    lnkReceivertoStorage.Visible = false;
                    //    lnkFromStoragetoDispatch.Visible = false;
                    //    lnkDailyDispatchClosure.Visible = false;
                    //    btnVATtansfers.Visible = false;
                    //    lnkRawMaterialWastage.Visible = false;
                    //    LinkButton1.Visible = false;
                    //    btnIssue.Visible = false;
                    //    btnConsumption.Visible = false;
                    //    btnRG4.Visible = false;
                    //    btnDMP.Visible = false;
                    //    btnMIR.Visible = false;
                    //    LinkButton2.Visible = false;
                    //}
                }
                }
            
        }
        
        protected void lnkDailyDispatchClosure_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DailyDispatchClosureList");
        }
        protected void lnkRawMaterialToFermenter_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialtoFermenterList");
        }

        protected void lnkFermentertoReceiver_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("FermentertoReceiverList");
        }
        protected void lnkRawMaterialWastage_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawmaterialWastageList.aspx");
        }
        protected void btnIssue_Click(object sender, EventArgs e)
        {
            if (Session["party_code"].ToString() == "MTR" || Session["party_code"].ToString() == "MTW")
            {
                Response.Redirect("MNTW_IssueList.aspx");
            }
            else
            {
                Response.Redirect("MNT_IssueList.aspx");
            }

        }

        protected void btnConsumption_Click(object sender, EventArgs e)
        {
            Response.Redirect("MNT_ConsumptionList.aspx");
        }



        protected void lnkOB_Click(object sender, EventArgs e)
        {
            Response.Redirect("OpeningBalance.aspx");
        }
        protected void lnkReceivertoStorage_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ReceivertoStorageList");
        }

        protected void lnkFromStoragetoDispatch_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("FromStoragetoDispatchList");
        }
        protected void btnVATtansfers_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATTransferList");
        }

        protected void lnkRMR_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialReceiptList");
        }
        protected void btnOpeningBalance_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("OpeningBalanceList");
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string party_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblPartyCode") as Label).Text;
             string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["party_code"] = party_name;
            Session["party_code1"] = party_name;
            Session["ofinancial_year"] = financial_year;
            //   Session["creation_date"] = creation_date;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] =1;
            Response.Redirect("OpeningBalanceForm");
        }
        protected void btnEdite_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string party_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblPartyCode") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            //  string creation_date = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDate") as Label).Text;
            Session["party_code"] = party_name;
            Session["party_code1"] = party_name;
            Session["ofinancial_year"] = financial_year;
            // Session["creation_date"] = creation_date;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = 2;
            Response.Redirect("OpeningBalanceForm"); ;
        }
        protected void AddRecords_Click(object sender, EventArgs e)
        {
            Session["rtype"] = 0;
            Session["UserID"] = Session["UserID"];
            Response.Redirect("OpeningBalanceForm");
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
        

        protected void grdopeningbalanceList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (!IsPostBack)
            {
                if (System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    if (e.NewPageIndex < 0)
                    {
                        grdopeningbalanceList.PageIndex = 0;
                    }
                    else
                    {

                        grdopeningbalanceList.PageIndex = e.NewPageIndex;
                    }
                    // UserDetails user = new UserDetails();
                    // user = BL_UserDetails.GetUser(Session["UserID"].ToString());
                    string userid = Session["UserID"].ToString();
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.GetUser(userid);
                    openingbalance = new List<OpeningBalance>();
                    openingbalance = BL_OpeningBalance.GetOpeningBalanceList(Session["party_code"].ToString(), "");
                    if (Session["UserID"].ToString() == "Admin")
                    {
                        var partynames = (from s in openingbalance
                                          select new { party_code = s.party_code, party_name = s.party_name, record_status = s.record_status }).Distinct();
                        grdopeningbalanceList.DataSource = partynames.ToList();
                        grdopeningbalanceList.DataBind();
                        sgr.Visible = false;
                    }
                    else if (Session["rolename"].ToString() == "Bond Officer")
                    {
                        btnAddRecord.Visible = false;
                        var partynames = (from s in openingbalance
                                          where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.financial_year==user.financial_year
                                          select new { party_code = s.party_code, party_name = s.party_name, record_status = s.record_status }).Distinct();
                        grdopeningbalanceList.DataSource = partynames.ToList();
                        grdopeningbalanceList.DataBind();
                        foreach (GridViewRow dr1 in grdopeningbalanceList.Rows)
                        {
                            LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                            btn.Visible = false;
                        }
                        btnAddRecord.Visible = false;
                    }
                    else
                    {
                        var partynames = (from s in openingbalance
                                          where s.party_code == Session["party_code"].ToString() && s.financial_year==user.financial_year
                                          orderby s.vat_code ascending
                                          select new { party_code = s.party_code, party_name = s.party_name, record_status = s.record_status }).Distinct();
                        grdopeningbalanceList.DataSource = partynames.ToList();
                        grdopeningbalanceList.DataBind();
                        int n = 0;

                        if (partynames.ToList().Count > 0)
                        {
                            btnAddRecord.Visible = false;
                            //foreach (GridViewRow dr1 in grdopeningbalanceList.Rows)
                            //{

                            //    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                            //    btn.Visible = false;
                            //}
                        }
                    }
                }
            }
        }

        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdopeningbalanceList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            if (Convert.ToInt32(txtpage.Text) <= 0)
            {
                a = 0;
            }
            else
            {
                a = Convert.ToInt32(txtpage.Text);
            }
            if (a != 0)
            {
                grdopeningbalanceList.PageIndex = a - 1;
            }
            else
            {
                grdopeningbalanceList.PageIndex = a;
            }
          

            var partynames = (from s in openingbalance
                              select new { party_code = s.party_code, party_name = s.party_name, record_status = s.record_status }).Distinct();
            grdopeningbalanceList.DataSource = partynames.ToList();
            grdopeningbalanceList.DataBind();
            sgr.Visible = false;


        }

        protected void grdopeningbalanceList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdopeningbalanceList.TopPagerRow;
            //grdopeningbalanceList.TopPagerRow.Visible = true;
            if (row == null)
            {
                return;
            }

            DropDownList DDLPage = (DropDownList)row.Cells[0].FindControl("DDLPage");
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            Label lblPages = (Label)row.Cells[0].FindControl("lblPages");
            Label lblCurrent = (Label)row.Cells[0].FindControl("lblCurrent");

            //if (lblPages != null)
            //{
            lblPages.Text = grdopeningbalanceList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdopeningbalanceList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdopeningbalanceList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdopeningbalanceList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdopeningbalanceList.PageIndex == 0)
            {
                ((ImageButton)grdopeningbalanceList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdopeningbalanceList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdopeningbalanceList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdopeningbalanceList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdopeningbalanceList.PageIndex + 1 == grdopeningbalanceList.PageCount)
            {
                ((ImageButton)grdopeningbalanceList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdopeningbalanceList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdopeningbalanceList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdopeningbalanceList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }
    }
}