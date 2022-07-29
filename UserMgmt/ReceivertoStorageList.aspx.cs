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
    public partial class ReceivertoStorageList : System.Web.UI.Page
    {
        List<ReceiverToStorage_84> form84 = new List<ReceiverToStorage_84>();
        public static UserDetails user = new UserDetails();
        static string _party_code;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    grdReceivertoStorage.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];

                    Session["UserID"] = Session["UserID"];
                    user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    party_code.Value = user.party_code.ToString();
                    // _party_code = party_code.Value;
                    if (user != null)
                    {
                        Session["party_code"] = user.party_code;
                        Session["rolename"] = user.role_name;
                        form84 = new List<ReceiverToStorage_84>();
                        form84 = BL_ReceiverToStorage_84.GetList();
                        //grdReceivertoStorage.DataSource = form84;
                        //grdReceivertoStorage.DataBind();
                        if (user.role_name == "Bond Officer")
                        {
                            var list = from s in form84
                                       where s.party_code == user.party_code && s.record_status != "N"&& s.financial_year==user.financial_year
                                       select s;
                            grdReceivertoStorage.DataSource = list.ToList();
                            grdReceivertoStorage.DataBind();
                            foreach (GridViewRow dr1 in grdReceivertoStorage.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = false;
                            }
                            btnAddRecord.Visible = false;

                        }

                        else if (Session["UserID"].ToString() == "Admin")
                        {
                            var list = from s in form84
                                       select s;
                            grdReceivertoStorage.DataSource = list.ToList();
                            grdReceivertoStorage.DataBind();
                            foreach (GridViewRow dr1 in grdReceivertoStorage.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = true;
                            }
                        }

                        else
                        {
                            var list = from s in form84
                                       where s.party_code == user.party_code && s.financial_year == user.financial_year
                                       select s;
                            grdReceivertoStorage.DataSource = list.ToList();
                            grdReceivertoStorage.DataBind();
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
        protected void lnkRawMaterialWastage_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawmaterialWastageList.aspx");
        }
        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ReceiverTransferList");

        }

        protected void btnReceipts_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ReceivertoStorageList");

        }

        protected void lnkRMR_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialReceiptList");
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = 0;
            Response.Redirect("ReceivertoStorageForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string form84id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblform84id") as Label).Text;
            // string from_receivervat = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfrom_receivervat") as Label).Text;
            string PartyCode = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpartycode") as Label).Text;
            // Session["from_receivervat"] = from_receivervat;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["rstfinancial_year"] = financial_year;
            Session["ReceivertoStorageId"] = form84id;
            Session["Party_Code"] = PartyCode;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "2";
            Response.Redirect("ReceivertoStorageForm");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string form84id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblform84id") as Label).Text;
            // string from_receivervat = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfrom_receivervat") as Label).Text;
            string PartyCode = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpartycode") as Label).Text;
            //  Session["from_receivervat"] = from_receivervat;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["rstfinancial_year"] = financial_year;
            Session["ReceivertoStorageId"] = form84id;
            Session["Party_Code"] = PartyCode;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "1";
            Response.Redirect("ReceivertoStorageForm");
        }
        protected void btnVATtansfers_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATTransferList");
        }

        protected void grdReceivertoStorage_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.NewPageIndex == -1)
                {
                    grdReceivertoStorage.PageIndex = 0;
                }
                else
                {
                    grdReceivertoStorage.PageIndex = e.NewPageIndex;
                }
                GridViewRow row = grdReceivertoStorage.TopPagerRow;
                TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
                if (Session["rsrsearch"] != null && Session["rsrtext"] != null)
                {
                    ddsearch.SelectedValue = Session["rsrsearch"].ToString();
                    txtpage.Text = Session["rsrtext"].ToString();
                    if (ddsearch.SelectedValue != "Select")
                    {

                        if (txtpage != null)
                        {
                            if (ddsearch.SelectedValue == "receipt_date")
                            {
                                if (txtpage.Text.ToString().Length == 10)
                                {

                                    form84 = new List<ReceiverToStorage_84>();
                                    form84 = BL_ReceiverToStorage_84.GetList();
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                    txtpage.Focus();
                                    txtpage.Text = "";
                                    ddsearch.SelectedIndex = 0;
                                    form84 = new List<ReceiverToStorage_84>();
                                    form84 = BL_ReceiverToStorage_84.GetList();

                                }
                            }
                            else
                            {
                                form84 = new List<ReceiverToStorage_84>();
                                form84 = BL_ReceiverToStorage_84.Search("", ddsearch.SelectedValue, txtpage.Text);
                            }

                        }
                    }
                }
                else
                {
                    //user = new UserDetails();
                    //user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    form84 = BL_ReceiverToStorage_84.GetList();
                }
                if (Session["rolename"].ToString() == "Bond Officer")
                {
                    if (ddsearch.SelectedValue == "receipt_date")
                    {
                        var list = from s in form84
                                   where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.receipt_date == txtpage.Text && s.financial_year == user.financial_year
                                   select s;
                        grdReceivertoStorage.DataSource = list.ToList();
                        grdReceivertoStorage.DataBind();

                    }
                    else
                    {
                        var list = from s in form84
                                   where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.financial_year == user.financial_year
                                   select s;
                        grdReceivertoStorage.DataSource = list.ToList();
                        grdReceivertoStorage.DataBind();
                    }

                    foreach (GridViewRow dr1 in grdReceivertoStorage.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                    btnAddRecord.Visible = false;

                }
                else if (Session["UserID"].ToString() == "Admin")
                {

                    if (ddsearch.SelectedValue == "receipt_date")
                    {
                        var list = from s in form84
                                   where s.receipt_date == txtpage.Text
                                   select s;
                        grdReceivertoStorage.DataSource = list.ToList();
                        grdReceivertoStorage.DataBind();

                    }
                    else
                    {
                        var list = from s in form84
                                   select s;
                        grdReceivertoStorage.DataSource = list.ToList();
                        grdReceivertoStorage.DataBind();
                    }
                    foreach (GridViewRow dr1 in grdReceivertoStorage.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = true;
                    }
                }
                else
                {
                    if (ddsearch.SelectedValue == "receipt_date")
                    {
                        var list = from s in form84
                                   where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.receipt_date == txtpage.Text && s.financial_year == user.financial_year
                                   select s;
                        grdReceivertoStorage.DataSource = list.ToList();
                        grdReceivertoStorage.DataBind();

                    }
                    else
                    {
                        var list = from s in form84
                                   where s.party_code == Session["party_code"].ToString() && s.financial_year == user.financial_year
                                   select s;
                        grdReceivertoStorage.DataSource = list.ToList();
                        grdReceivertoStorage.DataBind();
                    }
                }
            }
        }

        protected void lnkOpeningBalance_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("OpeningBalanceList");

        }
        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdReceivertoStorage.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                form84 = new List<ReceiverToStorage_84>();
                form84 = BL_ReceiverToStorage_84.GetList();
                //grdReceivertoStorage.DataSource = form84;
                //grdReceivertoStorage.DataBind();
                if (user.role_name == "Bond Officer")
                {
                    var list = from s in form84
                               where s.party_code == user.party_code && s.record_status != "N"
                               select s;
                    grdReceivertoStorage.DataSource = list.ToList();
                    grdReceivertoStorage.DataBind();
                    foreach (GridViewRow dr1 in grdReceivertoStorage.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                    btnAddRecord.Visible = false;

                }

                else if (Session["UserID"].ToString() == "Admin")
                {
                    var list = from s in form84
                               select s;
                    grdReceivertoStorage.DataSource = list.ToList();
                    grdReceivertoStorage.DataBind();
                    foreach (GridViewRow dr1 in grdReceivertoStorage.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = true;
                    }
                }

                else
                {
                    var list = from s in form84
                               where s.party_code == user.party_code
                               select s;
                    grdReceivertoStorage.DataSource = list.ToList();
                    grdReceivertoStorage.DataBind();
                }
            }
            return form84.ToString();
        }



        protected void btnsearch_Click(object sender, EventArgs e)
        {


            GridViewRow row = grdReceivertoStorage.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["rsrsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["rsrtext"] = txtpage.Text;
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        if (ddsearch.SelectedValue == "receipt_date")
                        {
                            if (txtpage.Text.ToString().Length == 10)
                            {

                                form84 = new List<ReceiverToStorage_84>();
                                form84 = BL_ReceiverToStorage_84.GetList();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                txtpage.Focus();
                                txtpage.Text = "";
                                ddsearch.SelectedIndex = 0;
                                form84 = new List<ReceiverToStorage_84>();
                                form84 = BL_ReceiverToStorage_84.GetList();

                            }
                        }
                        else
                        {
                            form84 = new List<ReceiverToStorage_84>();
                            form84 = BL_ReceiverToStorage_84.Search("", ddsearch.SelectedValue, txtpage.Text);
                        }

                        if (Session["rolename"].ToString() == "Bond Officer")
                        {
                            if (ddsearch.SelectedValue == "receipt_date")
                            {
                                var list = from s in form84
                                           where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.receipt_date==txtpage.Text && s.financial_year == user.financial_year
                                           select s;
                                grdReceivertoStorage.DataSource = list.ToList();
                                grdReceivertoStorage.DataBind();

                            }
                            else
                            {
                                var list = from s in form84
                                           where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.financial_year == user.financial_year
                                           select s;
                                grdReceivertoStorage.DataSource = list.ToList();
                                grdReceivertoStorage.DataBind();
                            }

                            foreach (GridViewRow dr1 in grdReceivertoStorage.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = false;
                            }
                            btnAddRecord.Visible = false;

                        }
                        else if (Session["UserID"].ToString() == "Admin")
                        {

                            if (ddsearch.SelectedValue == "receipt_date")
                            {
                                var list = from s in form84
                                           where  s.receipt_date == txtpage.Text
                                           select s;
                                grdReceivertoStorage.DataSource = list.ToList();
                                grdReceivertoStorage.DataBind();

                            }
                            else
                            {
                                var list = from s in form84
                                           select s;
                                grdReceivertoStorage.DataSource = list.ToList();
                                grdReceivertoStorage.DataBind();
                            }
                            foreach (GridViewRow dr1 in grdReceivertoStorage.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = true;
                            }
                        }
                        else
                        {
                            if (ddsearch.SelectedValue == "receipt_date")
                            {
                                var list = from s in form84
                                           where s.party_code == Session["party_code"].ToString()  && s.receipt_date == txtpage.Text && s.financial_year == user.financial_year
                                           select s;
                                grdReceivertoStorage.DataSource = list.ToList();
                                grdReceivertoStorage.DataBind();

                            }
                            else
                            {
                                var list = from s in form84
                                           where s.party_code == Session["party_code"].ToString() && s.financial_year == user.financial_year
                                           select s;
                                grdReceivertoStorage.DataSource = list.ToList();
                                grdReceivertoStorage.DataBind();
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





        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdReceivertoStorage.TopPagerRow;
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
                grdReceivertoStorage.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdReceivertoStorage.PageIndex = a - 1;
            }

            

            Session["UserID"] = Session["UserID"];
            user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            party_code.Value = user.party_code.ToString();
            // _party_code = party_code.Value;
            if (user != null)
            {
                Session["party_code"] = user.party_code;
                Session["rolename"] = user.role_name;
                form84 = new List<ReceiverToStorage_84>();
                form84 = BL_ReceiverToStorage_84.GetList();
                //grdReceivertoStorage.DataSource = form84;
                //grdReceivertoStorage.DataBind();
                if (user.role_name == "Bond Officer")
                {
                    var list = from s in form84
                               where s.party_code == user.party_code && s.record_status != "N"
                               select s;
                    grdReceivertoStorage.DataSource = list.ToList();
                    grdReceivertoStorage.DataBind();
                    foreach (GridViewRow dr1 in grdReceivertoStorage.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                    btnAddRecord.Visible = false;

                }

                else if (Session["UserID"].ToString() == "Admin")
                {
                    var list = from s in form84
                               select s;
                    grdReceivertoStorage.DataSource = list.ToList();
                    grdReceivertoStorage.DataBind();
                    foreach (GridViewRow dr1 in grdReceivertoStorage.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = true;
                    }
                }

                else
                {
                    var list = from s in form84
                               where s.party_code == user.party_code
                               select s;
                    grdReceivertoStorage.DataSource = list.ToList();
                    grdReceivertoStorage.DataBind();
                }
            }



        }

        protected void grdReceivertoStorage_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdReceivertoStorage.TopPagerRow;

            if (grdReceivertoStorage.Rows.Count > 0)
            {
                grdReceivertoStorage.TopPagerRow.Visible = true;
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
            if (Session["rsrsearch"] != null && Session["rsrtext"] != null)
            {
                ddsearch.SelectedValue = Session["rsrsearch"].ToString();
                txtpages.Text = Session["rsrtext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdReceivertoStorage.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdReceivertoStorage.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdReceivertoStorage.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdReceivertoStorage.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdReceivertoStorage.PageIndex == 0)
            {
                ((ImageButton)grdReceivertoStorage.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdReceivertoStorage.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdReceivertoStorage.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdReceivertoStorage.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdReceivertoStorage.PageIndex + 1 == grdReceivertoStorage.PageCount)
            {
                ((ImageButton)grdReceivertoStorage.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdReceivertoStorage.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdReceivertoStorage.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdReceivertoStorage.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["rsrsearch"] = null;
            Session["rsrtext"] = null;
            Session["UserID"] = Session["UserID"];
            user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            party_code.Value = user.party_code.ToString();
            // _party_code = party_code.Value;
            if (user != null)
            {
                Session["party_code"] = user.party_code;
                Session["rolename"] = user.role_name;
                form84 = new List<ReceiverToStorage_84>();
                form84 = BL_ReceiverToStorage_84.GetList();
                //grdReceivertoStorage.DataSource = form84;
                //grdReceivertoStorage.DataBind();
                if (user.role_name == "Bond Officer")
                {
                    var list = from s in form84
                               where s.party_code == user.party_code && s.record_status != "N"
                               select s;
                    grdReceivertoStorage.DataSource = list.ToList();
                    grdReceivertoStorage.DataBind();
                    foreach (GridViewRow dr1 in grdReceivertoStorage.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                    btnAddRecord.Visible = false;

                }

                else if (Session["UserID"].ToString() == "Admin")
                {
                    var list = from s in form84
                               select s;
                    grdReceivertoStorage.DataSource = list.ToList();
                    grdReceivertoStorage.DataBind();
                    foreach (GridViewRow dr1 in grdReceivertoStorage.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = true;
                    }
                }

                else
                {
                    var list = from s in form84
                               where s.party_code == user.party_code
                               select s;
                    grdReceivertoStorage.DataSource = list.ToList();
                    grdReceivertoStorage.DataBind();
                }
            }

        }
    }
}