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
    public partial class SCMSugarCanePurchaseRegList : System.Web.UI.Page
    {
        List<SugarCanePurchase> scp = new List<SugarCanePurchase>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                    grdSugarCanePurchaseList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                    Session["UserID"] = Session["UserID"];
                    scp = new List<SugarCanePurchase>();
                    scp = BL_SugarCanePurchase.GetList();
                    Session["party_type"] = "SGR";
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        Session["party_code"] = user.party_code;
                        if (user == null)
                            Response.Redirect("~/LoginPage");
                        if (user.role_name == "Bond Officer")
                        {
                            var list = from s in scp
                                       where s.party_code == user.party_code && s.record_status != "N" && s.financialyear==user.financial_year
                                       orderby Convert.ToDateTime(s.entrydate) descending
                                       select s;
                            grdSugarCanePurchaseList.DataSource = list.ToList();
                            grdSugarCanePurchaseList.DataBind();
                            foreach (GridViewRow dr1 in grdSugarCanePurchaseList.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = false;
                            }
                            AddRecords.Visible = false;

                        }
                        else if (Session["UserID"].ToString() == "Admin")
                        {
                            var list = (from s in scp
                                        orderby Convert.ToDateTime(s.entrydate) descending
                                        select new { party_name = s.party_name, party_code = s.party_code, entrydate = s.entrydate, total_purchase = s.total_purchase, total_canecrushed = s.total_canecrushed, record_status = s.record_status, sugarcanepurchase_id = s.sugarcanepurchase_id }).Distinct();
                            grdSugarCanePurchaseList.DataSource = list.ToList();
                            grdSugarCanePurchaseList.DataBind();
                            foreach (GridViewRow dr1 in grdSugarCanePurchaseList.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = true;
                            }
                        }

                        else
                        {
                            var list = from s in scp
                                       where s.party_code == user.party_code && s.financialyear == user.financial_year
                                       orderby Convert.ToDateTime(s.entrydate) descending
                                       select s;
                            grdSugarCanePurchaseList.DataSource = list.ToList();
                            grdSugarCanePurchaseList.DataBind();
                            //foreach (GridViewRow dr1 in grdSugarCanePurchaseList.Rows)
                            //{
                            //    Label status = dr1.FindControl("lblstatus") as Label;
                            //    if (status.Text == "Draft")
                            //    {
                            //        AddRecords.Visible = false;
                            //    }
                            //}
                        }
                        //List<OpeningBalance> openingbalance = new List<OpeningBalance>();
                        //openingbalance = BL_OpeningBalance.GetOpeningBalanceList(Session["party_code"].ToString(), "");
                        //var partynames = from s in openingbalance
                        //                 where s.party_code == user.party_code && s.financial_year == user.financial_year
                        //                 select s;
                        //if (partynames.ToList()[0].record_status != "A")
                        //{
                        //    //lnkRMR.Visible = false;
                        //    //lnkRawMaterialToFermenter.Visible = false;
                        //    //lnkFermentertoReceiver.Visible = false;
                        //    //lnkReceivertoStorage.Visible = false;
                        //    //lnkFromStoragetoDispatch.Visible = false;
                        //    //lnkDailyDispatchClosure.Visible = false;
                        //    //btnVATtansfers.Visible = false;
                        //    //lnkRawMaterialWastage.Visible = false;
                        //    //LinkButton1.Visible = false;
                        //    //btnIssue.Visible = false;
                        //    //btnConsumption.Visible = false;
                        //    //drawid.Visible = false;
                        //    //SGR.Visible = false;
                        //    Session["UserID"] = Session["UserID"];
                        //    Response.Redirect("OpeningBalanceList");
                        //}
                    }
                  
                }

                if (strPreviousPage == "" || System.Web.HttpContext.Current.Session["UserID"] == null)
                {
                    Response.Redirect("~/LoginPage");
                }
            }
        }

        protected void AddRecords_Click(object sender, EventArgs e)
        {

            string party = "";
            foreach (GridViewRow dr1 in grdSugarCanePurchaseList.Rows)
            {
                Label party1 = dr1.FindControl("lblparty_code") as Label;
                party = party1.Text;
            }
            scp = BL_SugarCanePurchase.GetList();
            var list = from s in scp
                       where s.party_code == party && s.record_status == "N"
                       select s;


            if (list.ToList().Count == 0)
            {
                Session["UserID"] = Session["UserID"];
                Session["rtype"] = "0";
                Response.Redirect("SCMSugarCanePurchaseRegForm");
            }
            else
            {
                string message = "Please Submit Draft record of Previous Date then only it will allow new entry";
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
        protected void btnVATtansfers_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATTransferList");
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

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string scpcode = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblformrg4_id") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            string party_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblparty_code") as Label).Text;
            Session["scpparty_code"] = party_code;
            Session["scpfinancial_year"] = financial_year;

            Session["UserID"] = Session["UserID"];
            Session["scp_id"] = scpcode;
            Session["rtype"] = "2";
            Response.Redirect("SCMSugarCanePurchaseRegForm");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string scpcode = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblformrg4_id") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            string party_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblparty_code") as Label).Text;
            Session["scpparty_code"] = party_code;
            Session["scpfinancial_year"] = financial_year;
            Session["UserID"] = Session["UserID"];
            Session["scp_id"] = scpcode;
            Session["rtype"] = "1";
            Response.Redirect("SCMSugarCanePurchaseRegForm");
        }

        protected void grdSugarCanePurchaseList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (IsPostBack)
            {
                if (System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    if (e.NewPageIndex == -1)
                    {
                        grdSugarCanePurchaseList.PageIndex = 0;
                    }
                    else
                    {
                        grdSugarCanePurchaseList.PageIndex = e.NewPageIndex;
                    }
                       

                        UserDetails user = new UserDetails();
                        user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                        if (user != null)
                        {
                        GridViewRow row = grdSugarCanePurchaseList.TopPagerRow;
                        TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                        DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
                        if (Session["scpsearch"] != null && Session["scptext"] != null)
                        {
                            ddsearch.SelectedValue = Session["scpsearch"].ToString();
                            txtpage.Text = Session["scptext"].ToString();
                            if (ddsearch.SelectedValue != "Select")
                            {

                                if (txtpage != null)
                                {
                                    if (ddsearch.SelectedValue == "entrydate")
                                    {
                                        if (txtpage.Text.ToString().Length == 10)
                                        {
                                            //rawmaterial = BL_RawMaterialReceipt.Search("molassesissueregister", ddsearch.SelectedValue, Convert.ToDateTime(txtpage.Text).ToString("yyyy/MM/dd").Replace("/" ,"-"));
                                            scp = new List<SugarCanePurchase>();
                                            scp = BL_SugarCanePurchase.GetList();
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                            txtpage.Focus();
                                            txtpage.Text = "";
                                            ddsearch.SelectedIndex = 0;
                                            scp = new List<SugarCanePurchase>();
                                            scp = BL_SugarCanePurchase.GetList();
                                        }

                                    }
                                    else
                                    {
                                        scp = BL_SugarCanePurchase.Search("sugarcanepurchase", ddsearch.SelectedValue, txtpage.Text);
                                    }

                                    if (user.role_name == "Bond Officer")
                                    {
                                        if (ddsearch.SelectedValue == "entrydate")
                                        {
                                            var list = from s in scp
                                                       where s.party_code == user.party_code && s.record_status != "N" && s.entrydate==txtpage.Text && s.financialyear == user.financial_year
                                                       orderby Convert.ToDateTime(s.entrydate) descending
                                                       select s;
                                            grdSugarCanePurchaseList.DataSource = list.ToList();
                                            grdSugarCanePurchaseList.DataBind();
                                        }
                                        else
                                        {
                                            var list = from s in scp
                                                       where s.party_code == user.party_code && s.record_status != "N" && s.financialyear == user.financial_year
                                                       orderby Convert.ToDateTime(s.entrydate) descending
                                                       select s;
                                            grdSugarCanePurchaseList.DataSource = list.ToList();
                                            grdSugarCanePurchaseList.DataBind();
                                        }
                                            
                                        foreach (GridViewRow dr1 in grdSugarCanePurchaseList.Rows)
                                        {

                                            LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                            btn.Visible = false;
                                        }
                                        AddRecords.Visible = false;

                                    }
                                    else if (Session["UserID"].ToString() == "Admin")
                                    {
                                        if (ddsearch.SelectedValue == "rmr_entrydate")
                                        {
                                            
                                            var list = (from s in scp
                                                        where s.entrydate == txtpage.Text
                                                        orderby Convert.ToDateTime(s.entrydate) descending
                                                        select new { party_name = s.party_name, party_code = s.party_code, entrydate = s.entrydate, total_purchase = s.total_purchase, total_canecrushed = s.total_canecrushed, record_status = s.record_status, sugarcanepurchase_id = s.sugarcanepurchase_id }).Distinct();
                                            grdSugarCanePurchaseList.DataSource = list.ToList();
                                            grdSugarCanePurchaseList.DataBind();
                                            
                                        }
                                        else
                                        {
                                            var list = (from s in scp
                                                        orderby Convert.ToDateTime(s.entrydate) descending
                                                        select new { party_name = s.party_name, party_code = s.party_code, entrydate = s.entrydate, total_purchase = s.total_purchase, total_canecrushed = s.total_canecrushed, record_status = s.record_status, sugarcanepurchase_id = s.sugarcanepurchase_id }).Distinct();
                                            grdSugarCanePurchaseList.DataSource = list.ToList();
                                            grdSugarCanePurchaseList.DataBind();
                                        }
                                        foreach (GridViewRow dr1 in grdSugarCanePurchaseList.Rows)
                                        {
                                            LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                            btn.Visible = true;
                                        }
                                    }
                                    else
                                    {
                                        if (ddsearch.SelectedValue == "entrydate")
                                        {
                                            var list = from s in scp
                                                       where s.party_code == user.party_code  && s.entrydate == txtpage.Text && s.financialyear == user.financial_year
                                                       orderby Convert.ToDateTime(s.entrydate) descending
                                                       select s;
                                            grdSugarCanePurchaseList.DataSource = list.ToList();
                                            grdSugarCanePurchaseList.DataBind();
                                        }
                                        else
                                        {
                                            var list = from s in scp
                                                       where s.party_code == user.party_code && s.financialyear == user.financial_year
                                                       orderby Convert.ToDateTime(s.entrydate) descending
                                                       select s;
                                            grdSugarCanePurchaseList.DataSource = list.ToList();
                                            grdSugarCanePurchaseList.DataBind();
                                        }

                                    }
                                }
                            }
                        }
                        else
                        {
                            scp = new List<SugarCanePurchase>();
                            scp = BL_SugarCanePurchase.GetList();

                            if (user.role_name == "Bond Officer")
                            {
                                var list = from s in scp
                                           where s.party_code == user.party_code && s.record_status != "N" && s.financialyear == user.financial_year
                                           orderby Convert.ToDateTime(s.entrydate) descending
                                           select s;
                                grdSugarCanePurchaseList.DataSource = list.ToList();
                                grdSugarCanePurchaseList.DataBind();
                                foreach (GridViewRow dr1 in grdSugarCanePurchaseList.Rows)
                                {

                                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                    btn.Visible = false;
                                }
                                AddRecords.Visible = false;

                            }
                            else if (Session["UserID"].ToString() == "Admin")
                            {
                                var list = (from s in scp
                                            orderby Convert.ToDateTime(s.entrydate) descending
                                            select new { party_name = s.party_name, party_code = s.party_code, entrydate = s.entrydate, total_purchase = s.total_purchase, total_canecrushed = s.total_canecrushed, record_status = s.record_status, sugarcanepurchase_id = s.sugarcanepurchase_id }).Distinct();
                                grdSugarCanePurchaseList.DataSource = list.ToList();
                                grdSugarCanePurchaseList.DataBind();
                                foreach (GridViewRow dr1 in grdSugarCanePurchaseList.Rows)
                                {
                                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                    btn.Visible = true;
                                }
                            }
                            else
                            {
                                var list = from s in scp
                                           where s.party_code == user.party_code && s.financialyear == user.financial_year
                                           orderby Convert.ToDateTime(s.entrydate) descending
                                           select s;
                                grdSugarCanePurchaseList.DataSource = list.ToList();
                                grdSugarCanePurchaseList.DataBind();
                                //foreach (GridViewRow dr1 in grdSugarCanePurchaseList.Rows)
                                //{
                                //    Label status = dr1.FindControl("lblstatus") as Label;
                                //    if (status.Text == "Draft")
                                //    {
                                //        AddRecords.Visible = false;
                                //    }
                                //}
                            }
                        }
                        }

                    }
                }
            
        }

        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdSugarCanePurchaseList.TopPagerRow;
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
                grdSugarCanePurchaseList.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdSugarCanePurchaseList.PageIndex = a - 1;
            }

            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            scp = new List<SugarCanePurchase>();
            scp = BL_SugarCanePurchase.GetList();
            string userid = Session["UserID"].ToString();

            if (user.role_name == "Bond Officer")
            {
                var list = from s in scp
                           where s.party_code == user.party_code && s.record_status != "N" && s.financialyear == user.financial_year
                           orderby Convert.ToDateTime(s.entrydate) descending
                           select s;
                grdSugarCanePurchaseList.DataSource = list.ToList();
                grdSugarCanePurchaseList.DataBind();
                foreach (GridViewRow dr1 in grdSugarCanePurchaseList.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    btn.Visible = false;
                }
                AddRecords.Visible = false;

            }
            else if (Session["UserID"].ToString() == "Admin")
            {
                var list = (from s in scp
                            orderby Convert.ToDateTime(s.entrydate) descending
                            select new { party_name = s.party_name, party_code = s.party_code, entrydate = s.entrydate, total_purchase = s.total_purchase, total_canecrushed = s.total_canecrushed, record_status = s.record_status, sugarcanepurchase_id = s.sugarcanepurchase_id }).Distinct();
                grdSugarCanePurchaseList.DataSource = list.ToList();
                grdSugarCanePurchaseList.DataBind();
                foreach (GridViewRow dr1 in grdSugarCanePurchaseList.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    btn.Visible = true;
                }
            }
            else
            {
                var list = from s in scp
                           where s.party_code == user.party_code && s.financialyear == user.financial_year
                           orderby Convert.ToDateTime(s.entrydate) descending
                           select s;
                grdSugarCanePurchaseList.DataSource = list.ToList();
                grdSugarCanePurchaseList.DataBind();
            }


        }

        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdSugarCanePurchaseList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                scp = new List<SugarCanePurchase>();
                scp = BL_SugarCanePurchase.GetList();
                string userid = Session["UserID"].ToString();

                if (user.role_name == "Bond Officer")
                {
                    var list = from s in scp
                               where s.party_code == user.party_code && s.record_status != "N" && s.financialyear == user.financial_year
                               orderby Convert.ToDateTime(s.entrydate) descending
                               select s;
                    grdSugarCanePurchaseList.DataSource = list.ToList();
                    grdSugarCanePurchaseList.DataBind();
                    foreach (GridViewRow dr1 in grdSugarCanePurchaseList.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                    AddRecords.Visible = false;

                }
                else if (Session["UserID"].ToString() == "Admin")
                {
                    var list = (from s in scp
                                orderby Convert.ToDateTime(s.entrydate) descending
                                select new { party_name = s.party_name, party_code = s.party_code, entrydate = s.entrydate, total_purchase = s.total_purchase, total_canecrushed = s.total_canecrushed, record_status = s.record_status, sugarcanepurchase_id = s.sugarcanepurchase_id }).Distinct();
                    grdSugarCanePurchaseList.DataSource = list.ToList();
                    grdSugarCanePurchaseList.DataBind();
                    foreach (GridViewRow dr1 in grdSugarCanePurchaseList.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = true;
                    }
                }
                else
                {
                    var list = from s in scp
                               where s.party_code == user.party_code && s.financialyear == user.financial_year
                               orderby Convert.ToDateTime(s.entrydate) descending
                               select s;
                    grdSugarCanePurchaseList.DataSource = list.ToList();
                    grdSugarCanePurchaseList.DataBind();
                }

            }
            return scp.ToString();
        }



        protected void btnsearch_Click(object sender, EventArgs e)
        {


            GridViewRow row = grdSugarCanePurchaseList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["scpsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["scptext"] = txtpage.Text;
                    if (ddsearch.SelectedValue == "entrydate")
                    {
                        if (txtpage.Text.ToString().Length == 10)
                        {
                            //rawmaterial = BL_RawMaterialReceipt.Search("molassesissueregister", ddsearch.SelectedValue, Convert.ToDateTime(txtpage.Text).ToString("yyyy/MM/dd").Replace("/" ,"-"));
                            scp = new List<SugarCanePurchase>();
                            scp = BL_SugarCanePurchase.GetList();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                            txtpage.Focus();
                            txtpage.Text = "";
                            ddsearch.SelectedIndex = 0;
                            scp = new List<SugarCanePurchase>();
                            scp = BL_SugarCanePurchase.GetList();
                        }

                    }
                    else
                    {
                        scp = BL_SugarCanePurchase.Search("sugarcanepurchase", ddsearch.SelectedValue, txtpage.Text);
                    }

                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        if (user.role_name == "Bond Officer")
                        {
                            if (ddsearch.SelectedValue == "entrydate")
                            {
                                var list = from s in scp
                                           where s.party_code == user.party_code && s.record_status != "N" && s.entrydate == txtpage.Text && s.financialyear == user.financial_year
                                           orderby Convert.ToDateTime(s.entrydate) descending
                                           select s;
                                grdSugarCanePurchaseList.DataSource = list.ToList();
                                grdSugarCanePurchaseList.DataBind();
                            }
                            else
                            {
                                var list = from s in scp
                                           where s.party_code == user.party_code && s.record_status != "N" && s.financialyear == user.financial_year
                                           orderby Convert.ToDateTime(s.entrydate) descending
                                           select s;
                                grdSugarCanePurchaseList.DataSource = list.ToList();
                                grdSugarCanePurchaseList.DataBind();
                            }

                            foreach (GridViewRow dr1 in grdSugarCanePurchaseList.Rows)
                            {

                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = false;
                            }
                            AddRecords.Visible = false;

                        }
                        else if (Session["UserID"].ToString() == "Admin")
                        {
                            if (ddsearch.SelectedValue == "rmr_entrydate")
                            {

                                var list = (from s in scp
                                            where s.entrydate == txtpage.Text
                                            orderby Convert.ToDateTime(s.entrydate) descending
                                            select new { party_name = s.party_name, party_code = s.party_code, entrydate = s.entrydate, total_purchase = s.total_purchase, total_canecrushed = s.total_canecrushed, record_status = s.record_status, sugarcanepurchase_id = s.sugarcanepurchase_id }).Distinct();
                                grdSugarCanePurchaseList.DataSource = list.ToList();
                                grdSugarCanePurchaseList.DataBind();

                            }
                            else
                            {
                                var list = (from s in scp
                                            orderby Convert.ToDateTime(s.entrydate) descending
                                            select new { party_name = s.party_name, party_code = s.party_code, entrydate = s.entrydate, total_purchase = s.total_purchase, total_canecrushed = s.total_canecrushed, record_status = s.record_status, sugarcanepurchase_id = s.sugarcanepurchase_id }).Distinct();
                                grdSugarCanePurchaseList.DataSource = list.ToList();
                                grdSugarCanePurchaseList.DataBind();
                            }
                            foreach (GridViewRow dr1 in grdSugarCanePurchaseList.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = true;
                            }
                        }
                        else
                        {
                            if (ddsearch.SelectedValue == "entrydate")
                            {
                                var list = from s in scp
                                           where s.party_code == user.party_code && s.entrydate == txtpage.Text && s.financialyear == user.financial_year
                                           orderby Convert.ToDateTime(s.entrydate) descending
                                           select s;
                                grdSugarCanePurchaseList.DataSource = list.ToList();
                                grdSugarCanePurchaseList.DataBind();
                            }
                            else
                            {
                                var list = from s in scp
                                           where s.party_code == user.party_code && s.financialyear == user.financial_year
                                           orderby Convert.ToDateTime(s.entrydate) descending
                                           select s;
                                grdSugarCanePurchaseList.DataSource = list.ToList();
                                grdSugarCanePurchaseList.DataBind();
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



        protected void grdSugarCanePurchaseList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdSugarCanePurchaseList.TopPagerRow;
            if (grdSugarCanePurchaseList.Rows.Count > 0)
            {
                grdSugarCanePurchaseList.TopPagerRow.Visible = true;
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
            if (Session["scpsearch"] != null && Session["scptext"] != null)
            {
                ddsearch.SelectedValue = Session["scpsearch"].ToString();
                txtpages.Text = Session["scptext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdSugarCanePurchaseList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdSugarCanePurchaseList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdSugarCanePurchaseList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdSugarCanePurchaseList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdSugarCanePurchaseList.PageIndex == 0)
            {
                ((ImageButton)grdSugarCanePurchaseList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdSugarCanePurchaseList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdSugarCanePurchaseList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdSugarCanePurchaseList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdSugarCanePurchaseList.PageIndex + 1 == grdSugarCanePurchaseList.PageCount)
            {
                ((ImageButton)grdSugarCanePurchaseList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdSugarCanePurchaseList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdSugarCanePurchaseList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdSugarCanePurchaseList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["scpsearch"] = null;
            Session["scptext"] = null;
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            scp = new List<SugarCanePurchase>();
            scp = BL_SugarCanePurchase.GetList();
            string userid = Session["UserID"].ToString();

            if (user.role_name == "Bond Officer")
            {
                var list = from s in scp
                           where s.party_code == user.party_code && s.record_status != "N" && s.financialyear == user.financial_year
                           orderby Convert.ToDateTime(s.entrydate) descending
                           select s;
                grdSugarCanePurchaseList.DataSource = list.ToList();
                grdSugarCanePurchaseList.DataBind();
                foreach (GridViewRow dr1 in grdSugarCanePurchaseList.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    btn.Visible = false;
                }
                AddRecords.Visible = false;

            }
            else if (Session["UserID"].ToString() == "Admin")
            {
                var list = (from s in scp
                            orderby Convert.ToDateTime(s.entrydate) descending
                            select new { party_name = s.party_name, party_code = s.party_code, entrydate = s.entrydate, total_purchase = s.total_purchase, total_canecrushed = s.total_canecrushed, record_status = s.record_status, sugarcanepurchase_id = s.sugarcanepurchase_id }).Distinct();
                grdSugarCanePurchaseList.DataSource = list.ToList();
                grdSugarCanePurchaseList.DataBind();
                foreach (GridViewRow dr1 in grdSugarCanePurchaseList.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    btn.Visible = true;
                }
            }
            else
            {
                var list = from s in scp
                           where s.party_code == user.party_code && s.financialyear == user.financial_year
                           orderby Convert.ToDateTime(s.entrydate) descending
                           select s;
                grdSugarCanePurchaseList.DataSource = list.ToList();
                grdSugarCanePurchaseList.DataBind();
            }

        }
    }
}