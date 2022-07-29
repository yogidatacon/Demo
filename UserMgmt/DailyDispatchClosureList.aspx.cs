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
    public partial class DailyDispatchClosureList : System.Web.UI.Page
    {
        public UserDetails user = new UserDetails();
        List<DailyDispatchClosure> DES = new List<DailyDispatchClosure>();
        string _party_code;
        protected void Page_Load(object sender, EventArgs e)
        {
            string userid = Session["UserID"].ToString();
            if (!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                    grdDailyDispatchClosure.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                    Session["UserID"] = Session["UserID"];
                    user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    party_code.Value = user.party_code.ToString();
                    Session["Rolename"] = user.role_name;
                    Session["Partycode"] = user.party_code;
                    // _party_code = party_code.Value;
                    DES = new List<DailyDispatchClosure>();
                    DES = BL_DailyDispatchClosure.GetDispatch();
                    if (Session["Rolename"].ToString() == "Bond Officer")
                    {
                        var list = from s in DES
                                   where s.party_code == Session["Partycode"].ToString() && s.record_status != "N" && s.financial_year==user.financial_year
                                   select s;
                        grdDailyDispatchClosure.DataSource = list.ToList();
                        grdDailyDispatchClosure.DataBind();
                        foreach (GridViewRow dr1 in grdDailyDispatchClosure.Rows)
                        {
                            LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                            btn.Visible = false;
                        }
                        btnAddRecord.Visible = false;
                    }

                    else
                    {
                        var list = from s in DES
                                   where s.party_code == Session["Partycode"].ToString() && s.financial_year == user.financial_year
                                   select s;
                        grdDailyDispatchClosure.DataSource = list.ToList();
                        grdDailyDispatchClosure.DataBind();
                    }



                    //dispatch = new List<DailyDispatchClosure>();
                    //dispatch = BL_DailyDispatchClosure.GetDispatch(user.party_code);
                    //grdDailyDispatchClosure.DataSource = dispatch;
                    //grdDailyDispatchClosure.DataBind();
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
        protected void lnkRawMaterialWastage_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawmaterialWastageList.aspx");
        }

        protected void lnkFromStoragetoDispatch_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("FromStoragetoDispatchList");
        }


        protected void lnkRMR_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialReceiptList");
        }
        protected void btnVATtansfers_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATTransferList");
        }


        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = 0;
            Response.Redirect("DailyDispatchClosureForm");
        }


        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string Id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDDLid") as Label).Text;
            string PartyCode = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpartycode") as Label).Text; 
           string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["Dfinancial_year"] = financial_year;
            Session["UserID"] = Session["UserID"];
            int a = Convert.ToInt32(Id);
            Session["dailydispatchclosure_id"] = Id;
            Session["party_code"] = PartyCode;
            Session["rtype"] = "2";
            Response.Redirect("DailyDispatchClosureForm");

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string Id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDDLid") as Label).Text;
            string PartyCode = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpartycode") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["Dfinancial_year"] = financial_year;
            Session["UserID"] = Session["UserID"];
            int a = Convert.ToInt32(Id);
            Session["dailydispatchclosure_id"] = Id;
            Session["party_code"] = PartyCode;
            Session["rtype"] = "1";
            Response.Redirect("DailyDispatchClosureForm");
        }

        protected void grdDailyDispatchClosure_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.NewPageIndex == -1)
                {
                    grdDailyDispatchClosure.PageIndex = 0;
                }
                else
                {
                    grdDailyDispatchClosure.PageIndex = e.NewPageIndex;
                }
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                GridViewRow row = grdDailyDispatchClosure.TopPagerRow;
                TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
                if (Session["didsearch"] != null && Session["didtext"] != null)
                {
                    ddsearch.SelectedValue = Session["didsearch"].ToString();
                    txtpage.Text = Session["didtext"].ToString();
                    if (ddsearch.SelectedValue != "Select")
                    {

                        if (txtpage != null)
                        {
                            if (ddsearch.SelectedValue == "closure_date")
                            {
                                if (txtpage.Text.ToString().Length == 10)
                                {

                                    DES = new List<DailyDispatchClosure>();
                                    DES = BL_DailyDispatchClosure.GetDispatch();
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                    txtpage.Focus();
                                    txtpage.Text = "";
                                    ddsearch.SelectedIndex = 0;
                                    DES = new List<DailyDispatchClosure>();
                                    DES = BL_DailyDispatchClosure.GetDispatch();
                                }
                            }
                            else
                            {
                                DES = new List<DailyDispatchClosure>();
                                DES = BL_DailyDispatchClosure.Search("", ddsearch.SelectedValue, txtpage.Text);
                            }

                        }
                    }
                }
                else
                {

                    //user = new UserDetails();
                    //  user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    DES = new List<DailyDispatchClosure>();
                    DES = BL_DailyDispatchClosure.GetDispatch();
                }
                if (Session["Rolename"].ToString() == "Bond Officer")
                {
                    if (ddsearch.SelectedValue == "closure_date")
                    {
                        var list = from s in DES
                                   where s.party_code == party_code.Value && s.record_status != "N" && s.closure_date == txtpage.Text && s.financial_year == user.financial_year
                                   select s;
                        grdDailyDispatchClosure.DataSource = list.ToList();
                        grdDailyDispatchClosure.DataBind();
                    }
                    else
                    {
                        var list = from s in DES
                                   where s.party_code == party_code.Value && s.record_status != "N" && s.financial_year == user.financial_year
                                   select s;
                        grdDailyDispatchClosure.DataSource = list.ToList();
                        grdDailyDispatchClosure.DataBind();
                    }

                    foreach (GridViewRow dr1 in grdDailyDispatchClosure.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                    btnAddRecord.Visible = false;

                }
                else if (Session["UserID"].ToString() == "Admin")
                {
                    if (ddsearch.SelectedValue == "closure_date")
                    {
                        var list = from s in DES
                                   where s.closure_date == txtpage.Text
                                   select s;
                        grdDailyDispatchClosure.DataSource = list.ToList();
                        grdDailyDispatchClosure.DataBind();
                    }
                    else
                    {
                        var list = from s in DES
                                   select s;
                        grdDailyDispatchClosure.DataSource = list.ToList();
                        grdDailyDispatchClosure.DataBind();
                    }
                    foreach (GridViewRow dr1 in grdDailyDispatchClosure.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = true;
                    }
                }

                else
                {
                    if (ddsearch.SelectedValue == "closure_date")
                    {
                        var list = from s in DES
                                   where s.party_code == party_code.Value && s.closure_date == txtpage.Text && s.financial_year == user.financial_year
                                   select s;
                        grdDailyDispatchClosure.DataSource = list.ToList();
                        grdDailyDispatchClosure.DataBind();
                    }
                    else
                    {
                        var list = from s in DES
                                   where s.party_code == party_code.Value && s.financial_year == user.financial_year
                                   select s;
                        grdDailyDispatchClosure.DataSource = list.ToList();
                        grdDailyDispatchClosure.DataBind();
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
            GridViewRow row = grdDailyDispatchClosure.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                DES = new List<DailyDispatchClosure>();
                DES = BL_DailyDispatchClosure.GetDispatch();
                if (Session["Rolename"].ToString() == "Bond Officer")
                {
                    var list = from s in DES
                               where s.party_code == party_code.Value && s.record_status != "N" && s.financial_year == user.financial_year
                               select s;
                    grdDailyDispatchClosure.DataSource = list.ToList();
                    grdDailyDispatchClosure.DataBind();
                    foreach (GridViewRow dr1 in grdDailyDispatchClosure.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                    btnAddRecord.Visible = false;

                }
                else if (Session["UserID"].ToString() == "Admin")
                {
                    var list = from s in DES
                               select s;
                    grdDailyDispatchClosure.DataSource = list.ToList();
                    grdDailyDispatchClosure.DataBind();
                    foreach (GridViewRow dr1 in grdDailyDispatchClosure.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = true;
                    }
                }

                else
                {
                    var list = from s in DES
                               where s.party_code == party_code.Value && s.financial_year == user.financial_year
                               select s;
                    grdDailyDispatchClosure.DataSource = list.ToList();
                    grdDailyDispatchClosure.DataBind();
                }
            }
            return DES.ToString();
        }



        protected void btnsearch_Click(object sender, EventArgs e)
        {


            GridViewRow row = grdDailyDispatchClosure.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["didsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["didtext"] = txtpage.Text;
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        if (ddsearch.SelectedValue == "closure_date")
                        {
                            if (txtpage.Text.ToString().Length == 10)
                            {

                                DES = new List<DailyDispatchClosure>();
                                DES = BL_DailyDispatchClosure.GetDispatch();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                txtpage.Focus();
                                txtpage.Text = "";
                                ddsearch.SelectedIndex = 0;
                                DES = new List<DailyDispatchClosure>();
                                DES = BL_DailyDispatchClosure.GetDispatch();
                            }
                        }
                        else
                        {
                            DES = new List<DailyDispatchClosure>();
                            DES = BL_DailyDispatchClosure.Search("", ddsearch.SelectedValue, txtpage.Text);
                        }
                        if (Session["Rolename"].ToString() == "Bond Officer")
                        {
                            if (ddsearch.SelectedValue == "closure_date")
                            {
                                var list = from s in DES
                                           where s.party_code == party_code.Value && s.record_status != "N" && s.closure_date==txtpage.Text && s.financial_year == user.financial_year
                                           select s;
                                grdDailyDispatchClosure.DataSource = list.ToList();
                                grdDailyDispatchClosure.DataBind();
                            }
                            else
                            {
                                var list = from s in DES
                                           where s.party_code == party_code.Value && s.record_status != "N" && s.financial_year == user.financial_year
                                           select s;
                                grdDailyDispatchClosure.DataSource = list.ToList();
                                grdDailyDispatchClosure.DataBind();
                            }
                                
                            foreach (GridViewRow dr1 in grdDailyDispatchClosure.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = false;
                            }
                            btnAddRecord.Visible = false;

                        }
                        else if (Session["UserID"].ToString() == "Admin")
                        {
                            if (ddsearch.SelectedValue == "closure_date")
                            {
                                var list = from s in DES
                                           where  s.closure_date == txtpage.Text
                                           select s;
                                grdDailyDispatchClosure.DataSource = list.ToList();
                                grdDailyDispatchClosure.DataBind();
                            }
                            else
                            {
                                var list = from s in DES
                                           select s;
                                grdDailyDispatchClosure.DataSource = list.ToList();
                                grdDailyDispatchClosure.DataBind();
                            }
                            foreach (GridViewRow dr1 in grdDailyDispatchClosure.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = true;
                            }
                        }

                        else
                        {
                            if (ddsearch.SelectedValue == "closure_date")
                            {
                                var list = from s in DES
                                           where s.party_code == party_code.Value && s.closure_date == txtpage.Text && s.financial_year == user.financial_year
                                           select s;
                                grdDailyDispatchClosure.DataSource = list.ToList();
                                grdDailyDispatchClosure.DataBind();
                            }
                            else
                            {
                                var list = from s in DES
                                           where s.party_code == party_code.Value && s.financial_year == user.financial_year
                                           select s;
                                grdDailyDispatchClosure.DataSource = list.ToList();
                                grdDailyDispatchClosure.DataBind();
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
            GridViewRow row = grdDailyDispatchClosure.TopPagerRow;
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
                grdDailyDispatchClosure.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdDailyDispatchClosure.PageIndex = a - 1;
            }

            

            Session["UserID"] = Session["UserID"];
            user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            party_code.Value = user.party_code.ToString();
            Session["Rolename"] = user.role_name;
            Session["Partycode"] = user.party_code;
            // _party_code = party_code.Value;
            DES = new List<DailyDispatchClosure>();
            DES = BL_DailyDispatchClosure.GetDispatch();
            if (Session["Rolename"].ToString() == "Bond Officer")
            {
                var list = from s in DES 
                           where s.party_code == Session["Partycode"].ToString() && s.record_status != "N" && s.financial_year == user.financial_year
                           select s;
                grdDailyDispatchClosure.DataSource = list.ToList();
                grdDailyDispatchClosure.DataBind();
                foreach (GridViewRow dr1 in grdDailyDispatchClosure.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    btn.Visible = false;
                }
                btnAddRecord.Visible = false;
            }

            else
            {
                var list = from s in DES
                           where s.party_code == Session["Partycode"].ToString() && s.financial_year == user.financial_year
                           select s;
                grdDailyDispatchClosure.DataSource = list.ToList();
                grdDailyDispatchClosure.DataBind();
            }



            //dispatch = new List<DailyDispatchClosure>();
            //dispatch = BL_DailyDispatchClosure.GetDispatch(user.party_code);
            //grdDailyDispatchClosure.DataSource = dispatch;
            //grdDailyDispatchClosure.DataBind();
        


    }

    protected void grdDailyDispatchClosure_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdDailyDispatchClosure.TopPagerRow;

            if (grdDailyDispatchClosure.Rows.Count > 0)
            {
                grdDailyDispatchClosure.TopPagerRow.Visible = true;
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
            if (Session["didsearch"] != null && Session["didtext"] != null)
            {
                ddsearch.SelectedValue = Session["didsearch"].ToString();
                txtpages.Text = Session["didtext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdDailyDispatchClosure.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdDailyDispatchClosure.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdDailyDispatchClosure.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdDailyDispatchClosure.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdDailyDispatchClosure.PageIndex == 0)
            {
                ((ImageButton)grdDailyDispatchClosure.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdDailyDispatchClosure.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdDailyDispatchClosure.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdDailyDispatchClosure.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdDailyDispatchClosure.PageIndex + 1 == grdDailyDispatchClosure.PageCount)
            {
                ((ImageButton)grdDailyDispatchClosure.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdDailyDispatchClosure.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdDailyDispatchClosure.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdDailyDispatchClosure.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["didsearch"] = null;
            Session["didtext"] = null;
            Session["UserID"] = Session["UserID"];
            user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            party_code.Value = user.party_code.ToString();
            Session["Rolename"] = user.role_name;
            Session["Partycode"] = user.party_code;
            // _party_code = party_code.Value;
            DES = new List<DailyDispatchClosure>();
            DES = BL_DailyDispatchClosure.GetDispatch();
            if (Session["Rolename"].ToString() == "Bond Officer")
            {
                var list = from s in DES
                           where s.party_code == Session["Partycode"].ToString() && s.record_status != "N" && s.financial_year == user.financial_year
                           select s;
                grdDailyDispatchClosure.DataSource = list.ToList();
                grdDailyDispatchClosure.DataBind();
                foreach (GridViewRow dr1 in grdDailyDispatchClosure.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    btn.Visible = false;
                }
                btnAddRecord.Visible = false;
            }

            else
            {
                var list = from s in DES
                           where s.party_code == Session["Partycode"].ToString() && s.financial_year == user.financial_year
                           select s;
                grdDailyDispatchClosure.DataSource = list.ToList();
                grdDailyDispatchClosure.DataBind();
            }

        }
    }
}