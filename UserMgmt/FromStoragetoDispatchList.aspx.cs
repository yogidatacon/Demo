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
    public partial class FromStoragetoDispatchList : System.Web.UI.Page
    {
        List<StorageToDispatch> form84 = new List<StorageToDispatch>();
        public static UserDetails user = new UserDetails();
        static string _party_code;
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
                grdStoragetoDispatch.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                Session["UserID"] = Session["UserID"];
                user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                party_code.Value = user.party_code.ToString();
                Session["party_code"] = user.party_code;
                Session["rolename"] = user.role_name;
                _party_code = party_code.Value;
                form84 = new List<StorageToDispatch>();
                form84 = BL_StorageToDispatch.GetList();
                //grdReceivertoStorage.DataSource = form84;
                //grdReceivertoStorage.DataBind();
                if (user.role_name == "Bond Officer")
                {
                    var list = from s in form84
                               where s.party_code == user.party_code && s.record_status != "N" && s.financial_year==user.financial_year
                               select s;
                    grdStoragetoDispatch.DataSource = list.ToList();
                    grdStoragetoDispatch.DataBind();
                    foreach (GridViewRow dr1 in grdStoragetoDispatch.Rows)
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
                    grdStoragetoDispatch.DataSource = list.ToList();
                    grdStoragetoDispatch.DataBind();
                    foreach (GridViewRow dr1 in grdStoragetoDispatch.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = true;
                    }
                }


                else
                {
                    var list = from s in form84
                               where s.party_code == party_code.Value && s.financial_year == user.financial_year
                               select s;
                    grdStoragetoDispatch.DataSource = list.ToList();
                    grdStoragetoDispatch.DataBind();
                }
            }
        }
        protected void lnkDailyDispatchClosure_Click(object sender, EventArgs e)
        {

            Response.Redirect("DailyDispatchClosureList.aspx");
        }
        protected void lnkRawMaterialToFermenter_Click(object sender, EventArgs e)
        {

            Response.Redirect("RawMaterialtoFermenterList.aspx");
        }

        protected void lnkFermentertoReceiver_Click(object sender, EventArgs e)
        {

            Response.Redirect("FermentertoReceiverList.aspx");
        }

        protected void lnkReceivertoStorage_Click(object sender, EventArgs e)
        {

            Response.Redirect("ReceivertoStorageList.aspx");
        }

        protected void lnkRawMaterialWastage_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawmaterialWastageList.aspx");
        }

        protected void lnkFromStoragetoDispatch_Click(object sender, EventArgs e)
        {

            Response.Redirect("FromStoragetoDispatchList.aspx");
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
            Response.Redirect("FromStoragetoDispatchForm.aspx");
        }

        protected void btnVATtansfers_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATTransferList");
        }








        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string form84id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblform84Did") as Label).Text;
            // string from_receivervat = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfrom_receivervat") as Label).Text;
            string PartyCode = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpartycode") as Label).Text;
            // Session["from_receivervat"] = from_receivervat;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["Sfinancial_year"] = financial_year;
            Session["StorageId"] = form84id;
            Session["Party_Code"] = PartyCode;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "2";
            Response.Redirect("FromStoragetoDispatchForm");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string form84id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblform84Did") as Label).Text;
            // string from_receivervat = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfrom_receivervat") as Label).Text;
            string PartyCode = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpartycode") as Label).Text;
            //  Session["from_receivervat"] = from_receivervat;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["Sfinancial_year"] = financial_year;
            Session["StorageId"] = form84id;
            Session["Party_Code"] = PartyCode;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "1";
            Response.Redirect("FromStoragetoDispatchForm");
        }




        protected void grdStoragetoDispatch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.NewPageIndex == -1)
                {
                    grdStoragetoDispatch.PageIndex = 0;
                }
                else
                {
                    grdStoragetoDispatch.PageIndex = e.NewPageIndex;
                }
                GridViewRow row = grdStoragetoDispatch.TopPagerRow;
                TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
                if (Session["stdsearch"] != null && Session["stdtext"] != null)
                {
                    ddsearch.SelectedValue = Session["stdsearch"].ToString();
                    txtpage.Text = Session["stdtext"].ToString();
                    if (ddsearch.SelectedValue != "Select")
                    {

                        if (txtpage != null)
                        {
                            if (ddsearch.SelectedValue == "receipt_date")
                            {
                                if (txtpage.Text.ToString().Length == 10)
                                {

                                    form84 = new List<StorageToDispatch>();
                                    form84 = BL_StorageToDispatch.GetList();
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                    txtpage.Focus();
                                    txtpage.Text = "";
                                    ddsearch.SelectedIndex = 0;
                                    form84 = new List<StorageToDispatch>();
                                    form84 = BL_StorageToDispatch.GetList();

                                }
                            }
                            else
                            {

                                form84 = new List<StorageToDispatch>();
                                form84 = BL_StorageToDispatch.Search("", ddsearch.SelectedValue, txtpage.Text);
                            }

                        }
                    }
                }
                else
                {

                    // user = new UserDetails();
                    //user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    form84 = BL_StorageToDispatch.GetList();
                }
                if (user.role_name == "Bond Officer")
                {
                    if (ddsearch.SelectedValue == "receipt_date")
                    {
                        var list = from s in form84
                                   where s.party_code == user.party_code && s.record_status != "N" && s.receipt_date == txtpage.Text && s.financial_year == user.financial_year
                                   select s;
                        grdStoragetoDispatch.DataSource = list.ToList();
                        grdStoragetoDispatch.DataBind();
                    }
                    else
                    {
                        var list = from s in form84
                                   where s.party_code == user.party_code && s.record_status != "N" && s.financial_year == user.financial_year
                                   select s;
                        grdStoragetoDispatch.DataSource = list.ToList();
                        grdStoragetoDispatch.DataBind();
                    }

                    foreach (GridViewRow dr1 in grdStoragetoDispatch.Rows)
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
                        grdStoragetoDispatch.DataSource = list.ToList();
                        grdStoragetoDispatch.DataBind();
                    }
                    else
                    {
                        var list = from s in form84
                                   select s;
                        grdStoragetoDispatch.DataSource = list.ToList();
                        grdStoragetoDispatch.DataBind();
                    }
                    foreach (GridViewRow dr1 in grdStoragetoDispatch.Rows)
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
                                   where s.party_code == party_code.Value && s.receipt_date == txtpage.Text && s.financial_year == user.financial_year
                                   select s;
                        grdStoragetoDispatch.DataSource = list.ToList();
                        grdStoragetoDispatch.DataBind();
                    }
                    else
                    {

                        var list = from s in form84
                                   where s.party_code == party_code.Value && s.financial_year == user.financial_year
                                   select s;
                        grdStoragetoDispatch.DataSource = list.ToList();
                        grdStoragetoDispatch.DataBind();
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
            GridViewRow row = grdStoragetoDispatch.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                form84 = new List<StorageToDispatch>();
                form84 = BL_StorageToDispatch.GetList();
                //grdReceivertoStorage.DataSource = form84;
                //grdReceivertoStorage.DataBind();
                if (user.role_name == "Bond Officer")
                {
                    var list = from s in form84
                               where s.party_code == user.party_code && s.record_status != "N" && s.financial_year == user.financial_year
                               select s;
                    grdStoragetoDispatch.DataSource = list.ToList();
                    grdStoragetoDispatch.DataBind();
                    foreach (GridViewRow dr1 in grdStoragetoDispatch.Rows)
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
                    grdStoragetoDispatch.DataSource = list.ToList();
                    grdStoragetoDispatch.DataBind();
                    foreach (GridViewRow dr1 in grdStoragetoDispatch.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = true;
                    }
                }


                else
                {
                    var list = from s in form84
                               where s.party_code == party_code.Value && s.financial_year == user.financial_year
                               select s;
                    grdStoragetoDispatch.DataSource = list.ToList();
                    grdStoragetoDispatch.DataBind();
                }
            }
            return form84.ToString();
        }



        protected void btnsearch_Click(object sender, EventArgs e)
        {


            GridViewRow row = grdStoragetoDispatch.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["stdsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["stdtext"] = txtpage.Text;
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        if (ddsearch.SelectedValue == "receipt_date")
                        {
                            if (txtpage.Text.ToString().Length == 10)
                            {

                                form84 = new List<StorageToDispatch>();
                                form84 = BL_StorageToDispatch.GetList();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                txtpage.Focus();
                                txtpage.Text = "";
                                ddsearch.SelectedIndex = 0;
                                form84 = new List<StorageToDispatch>();
                                form84 = BL_StorageToDispatch.GetList();

                            }
                        }
                        else
                        {
                            form84 = new List<StorageToDispatch>();
                            form84 = BL_StorageToDispatch.Search("", ddsearch.SelectedValue, txtpage.Text);
                        }
                        //grdReceivertoStorage.DataSource = form84;
                        //grdReceivertoStorage.DataBind();
                        if (user.role_name == "Bond Officer")
                        {
                            if (ddsearch.SelectedValue == "receipt_date")
                            {
                                var list = from s in form84
                                           where s.party_code == user.party_code && s.record_status != "N" && s.receipt_date==txtpage.Text && s.financial_year == user.financial_year
                                           select s;
                                grdStoragetoDispatch.DataSource = list.ToList();
                                grdStoragetoDispatch.DataBind();
                            }
                            else
                            {
                                var list = from s in form84
                                           where s.party_code == user.party_code && s.record_status != "N" && s.financial_year == user.financial_year
                                           select s;
                                grdStoragetoDispatch.DataSource = list.ToList();
                                grdStoragetoDispatch.DataBind();
                            }
                               
                            foreach (GridViewRow dr1 in grdStoragetoDispatch.Rows)
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
                                    grdStoragetoDispatch.DataSource = list.ToList();
                                    grdStoragetoDispatch.DataBind();
                                }
                                else
                                { 
                                    var list = from s in form84
                                               select s;
                                grdStoragetoDispatch.DataSource = list.ToList();
                                grdStoragetoDispatch.DataBind();
                            }
                            foreach (GridViewRow dr1 in grdStoragetoDispatch.Rows)
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
                                           where s.party_code == party_code.Value && s.receipt_date == txtpage.Text && s.financial_year == user.financial_year
                                           select s;
                                grdStoragetoDispatch.DataSource = list.ToList();
                                grdStoragetoDispatch.DataBind();
                            }
                            else
                            {

                                var list = from s in form84
                                           where s.party_code == party_code.Value && s.financial_year == user.financial_year
                                           select s;
                                grdStoragetoDispatch.DataSource = list.ToList();
                                grdStoragetoDispatch.DataBind();
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
            GridViewRow row = grdStoragetoDispatch.TopPagerRow;
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
                grdStoragetoDispatch.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdStoragetoDispatch.PageIndex = a - 1;
            }

            

            Session["UserID"] = Session["UserID"];
            user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            party_code.Value = user.party_code.ToString();
            Session["party_code"] = user.party_code;
            Session["rolename"] = user.role_name;
            _party_code = party_code.Value;
            form84 = new List<StorageToDispatch>();
            form84 = BL_StorageToDispatch.GetList();
            //grdReceivertoStorage.DataSource = form84;
            //grdReceivertoStorage.DataBind();
            if (user.role_name == "Bond Officer")
            {
                var list = from s in form84
                           where s.party_code == user.party_code && s.record_status != "N" && s.financial_year == user.financial_year
                           select s;
                grdStoragetoDispatch.DataSource = list.ToList();
                grdStoragetoDispatch.DataBind();
                foreach (GridViewRow dr1 in grdStoragetoDispatch.Rows)
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
                grdStoragetoDispatch.DataSource = list.ToList();
                grdStoragetoDispatch.DataBind();
                foreach (GridViewRow dr1 in grdStoragetoDispatch.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    btn.Visible = true;
                }
            }


            else
            {
                var list = from s in form84
                           where s.party_code == party_code.Value && s.financial_year == user.financial_year
                           select s;
                grdStoragetoDispatch.DataSource = list.ToList();
                grdStoragetoDispatch.DataBind();
            }


        }

        protected void grdStoragetoDispatch_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdStoragetoDispatch.TopPagerRow;

            if (grdStoragetoDispatch.Rows.Count > 0)
            {
                grdStoragetoDispatch.TopPagerRow.Visible = true;
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
            if (Session["stdsearch"] != null && Session["stdtext"] != null)
            {
                ddsearch.SelectedValue = Session["stdsearch"].ToString();
                txtpages.Text = Session["stdtext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdStoragetoDispatch.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdStoragetoDispatch.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdStoragetoDispatch.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdStoragetoDispatch.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdStoragetoDispatch.PageIndex == 0)
            {
                ((ImageButton)grdStoragetoDispatch.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdStoragetoDispatch.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdStoragetoDispatch.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdStoragetoDispatch.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdStoragetoDispatch.PageIndex + 1 == grdStoragetoDispatch.PageCount)
            {
                ((ImageButton)grdStoragetoDispatch.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdStoragetoDispatch.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdStoragetoDispatch.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdStoragetoDispatch.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["stdsearch"] = null;
            Session["stdtext"] = null;
            user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            party_code.Value = user.party_code.ToString();
          
            form84 = new List<StorageToDispatch>();
            form84 = BL_StorageToDispatch.GetList();
            //grdReceivertoStorage.DataSource = form84;
            //grdReceivertoStorage.DataBind();
            if (user.role_name == "Bond Officer")
            {
                var list = from s in form84
                           where s.party_code == user.party_code && s.record_status != "N" && s.financial_year == user.financial_year
                           select s;
                grdStoragetoDispatch.DataSource = list.ToList();
                grdStoragetoDispatch.DataBind();
                foreach (GridViewRow dr1 in grdStoragetoDispatch.Rows)
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
                grdStoragetoDispatch.DataSource = list.ToList();
                grdStoragetoDispatch.DataBind();
                foreach (GridViewRow dr1 in grdStoragetoDispatch.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    btn.Visible = true;
                }
            }


            else
            {
                var list = from s in form84
                           where s.party_code == party_code.Value && s.financial_year == user.financial_year
                           select s;
                grdStoragetoDispatch.DataSource = list.ToList();
                grdStoragetoDispatch.DataBind();
            }

        }
    }
}