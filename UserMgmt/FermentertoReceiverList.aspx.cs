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
    public partial class FermentertoReceiverList : System.Web.UI.Page
    {

        List<FermentertoReceiverForm_83> form83 = new List<FermentertoReceiverForm_83>();
        public UserDetails user = new UserDetails();
        string _party_code;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    grdFermentertoReceiver.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];

                    Session["UserID"] = Session["UserID"];
                    user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    party_code.Value = user.party_code.ToString();
                    _party_code = party_code.Value;
                    if (user != null)
                    {
                        Session["party_code"] = user.party_code;
                        Session["rolename"] = user.role_name;
                        form83 = new List<FermentertoReceiverForm_83>();
                        form83 = BL_FermentertoReceiverForm_83.GetList();
                        //grdFermentertoReceiver.DataSource = form83;
                        //grdFermentertoReceiver.DataBind();


                        if (Session["rolename"].ToString() == "Bond Officer")
                        {
                            var list = from s in form83
                                       where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.financial_year==user.financial_year
                                       select s;
                            grdFermentertoReceiver.DataSource = list.ToList();
                            grdFermentertoReceiver.DataBind();
                            foreach (GridViewRow dr1 in grdFermentertoReceiver.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = false;
                            }
                            btnAddRecord.Visible = false;

                        }
                        else if (Session["UserID"].ToString() == "Admin")
                        {
                            var list = from s in form83
                                       select s;
                            grdFermentertoReceiver.DataSource = list.ToList();
                            grdFermentertoReceiver.DataBind();
                            foreach (GridViewRow dr1 in grdFermentertoReceiver.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = true;
                            }
                        }

                        else
                        {
                            var list = from s in form83
                                       where s.party_code == Session["party_code"].ToString() && s.financial_year == user.financial_year
                                       select s;
                            grdFermentertoReceiver.DataSource = list.ToList();
                            grdFermentertoReceiver.DataBind();
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


        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = 0;
            Response.Redirect("FermentertoReceiverForm");
        }

        protected void grdFermentertoReceiver_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.NewPageIndex == -1)
                {
                    grdFermentertoReceiver.PageIndex = 0;
                }
                else
                {
                    grdFermentertoReceiver.PageIndex = e.NewPageIndex;
                }
                user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                GridViewRow row = grdFermentertoReceiver.TopPagerRow;
                TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
                if (Session["ftrsearch"] != null && Session["ftrtext"] != null)
                {
                    ddsearch.SelectedValue = Session["ftrsearch"].ToString();
                    txtpage.Text = Session["ftrtext"].ToString();
                    if (ddsearch.SelectedValue != "Select")
                    {

                        if (txtpage != null)
                        {
                            if (ddsearch.SelectedValue == "gauged_date" || ddsearch.SelectedValue == "distillation_date")
                            {
                                if (txtpage.Text.ToString().Length == 10)
                                {
                                    form83 = new List<FermentertoReceiverForm_83>();
                                    form83 = BL_FermentertoReceiverForm_83.GetList();
                                }
                            }
                            else
                            {
                                form83 = new List<FermentertoReceiverForm_83>();
                                form83 = BL_FermentertoReceiverForm_83.Search("", ddsearch.SelectedValue, txtpage.Text);
                            }

                        }
                    }
                }
                else
                {
                  
                    form83 = BL_FermentertoReceiverForm_83.GetList();
                }
                if (Session["rolename"].ToString() == "Bond Officer")
                {
                    if (ddsearch.SelectedValue == "gauged_date")
                    {
                        var list = from s in form83
                                   where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.gauged_date == txtpage.Text && s.financial_year == user.financial_year
                                   select s;
                        grdFermentertoReceiver.DataSource = list.ToList();
                        grdFermentertoReceiver.DataBind();
                    }
                    else if (ddsearch.SelectedValue == "distillation_date")
                    {
                        var list = from s in form83
                                   where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.distillation_date == txtpage.Text && s.financial_year == user.financial_year
                                   select s;
                        grdFermentertoReceiver.DataSource = list.ToList();
                        grdFermentertoReceiver.DataBind();
                    }
                    else
                    {
                        var list = from s in form83
                                   where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.financial_year == user.financial_year
                                   select s;
                        grdFermentertoReceiver.DataSource = list.ToList();
                        grdFermentertoReceiver.DataBind();
                    }


                    foreach (GridViewRow dr1 in grdFermentertoReceiver.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                    btnAddRecord.Visible = false;

                }
                else if (Session["UserID"].ToString() == "Admin")
                {
                    if (ddsearch.SelectedValue == "gauged_date")
                    {
                        var list = from s in form83
                                   where s.gauged_date == txtpage.Text
                                   select s;
                        grdFermentertoReceiver.DataSource = list.ToList();
                        grdFermentertoReceiver.DataBind();
                    }
                    else if (ddsearch.SelectedValue == "distillation_date")
                    {
                        var list = from s in form83
                                   where s.distillation_date == txtpage.Text
                                   select s;
                        grdFermentertoReceiver.DataSource = list.ToList();
                        grdFermentertoReceiver.DataBind();
                    }
                    else
                    {
                        var list = from s in form83
                                   select s;
                        grdFermentertoReceiver.DataSource = list.ToList();
                        grdFermentertoReceiver.DataBind();
                    }
                    foreach (GridViewRow dr1 in grdFermentertoReceiver.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = true;
                    }
                }

                else
                {
                    if (ddsearch.SelectedValue == "gauged_date")
                    {
                        var list = from s in form83
                                   where s.party_code == Session["party_code"].ToString() && s.gauged_date == txtpage.Text && s.financial_year == user.financial_year
                                   select s;
                        grdFermentertoReceiver.DataSource = list.ToList();
                        grdFermentertoReceiver.DataBind();
                    }
                    else if (ddsearch.SelectedValue == "distillation_date")
                    {
                        var list = from s in form83
                                   where s.party_code == Session["party_code"].ToString() && s.distillation_date == txtpage.Text && s.financial_year == user.financial_year
                                   select s;
                        grdFermentertoReceiver.DataSource = list.ToList();
                        grdFermentertoReceiver.DataBind();
                    }
                    else
                    {
                        var list = from s in form83
                                   where s.party_code == Session["party_code"].ToString() && s.financial_year == user.financial_year
                                   select s;
                        grdFermentertoReceiver.DataSource = list.ToList();
                        grdFermentertoReceiver.DataBind();
                    }
                }

            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfermenterreceiverid") as Label).Text;
            string PartyCode = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblPartyCode") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["Ffinancial_year"] =financial_year;
            //  Session["Did"] = Did;
            Session["fermenterreceiverid"] = id;
            Session["Party_Code"] = PartyCode;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "1";
            Response.Redirect("FermentertoReceiverForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfermenterreceiverid") as Label).Text;
            ///  string Did = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lbldistillationid") as Label).Text;
            string PartyCode = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblPartyCode") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["Ffinancial_year"] = financial_year;
            // Session["Did"] = Did;
            Session["fermenterreceiverid"] = id;
            Session["Party_Code"] = PartyCode;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "2";
            Response.Redirect("FermentertoReceiverForm");
        }

        protected void btnVATtansfers_Click(object sender, EventArgs e)
        {

            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATTransferList");

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
            GridViewRow row = grdFermentertoReceiver.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                form83 = new List<FermentertoReceiverForm_83>();
                form83 = BL_FermentertoReceiverForm_83.GetList();
                //grdFermentertoReceiver.DataSource = form83;
                //grdFermentertoReceiver.DataBind();


                if (Session["rolename"].ToString() == "Bond Officer")
                {
                    var list = from s in form83
                               where s.party_code == Session["party_code"].ToString() && s.record_status != "N"
                               select s;
                    grdFermentertoReceiver.DataSource = list.ToList();
                    grdFermentertoReceiver.DataBind();
                    foreach (GridViewRow dr1 in grdFermentertoReceiver.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                    btnAddRecord.Visible = false;

                }
                else if (Session["UserID"].ToString() == "Admin")
                {
                    var list = from s in form83
                               select s;
                    grdFermentertoReceiver.DataSource = list.ToList();
                    grdFermentertoReceiver.DataBind();
                    foreach (GridViewRow dr1 in grdFermentertoReceiver.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = true;
                    }
                }

                else
                {
                    var list = from s in form83
                               where s.party_code == Session["party_code"].ToString()
                               select s;
                    grdFermentertoReceiver.DataSource = list.ToList();
                    grdFermentertoReceiver.DataBind();
                }

            }
            return form83.ToString();
        }



        protected void btnsearch_Click(object sender, EventArgs e)
        {


            GridViewRow row = grdFermentertoReceiver.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["ftrsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["ftrtext"] = txtpage.Text;
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        if (ddsearch.SelectedValue == "gauged_date" || ddsearch.SelectedValue == "distillation_date")
                        {
                            if (txtpage.Text.ToString().Length == 10)
                            {
                                form83 = new List<FermentertoReceiverForm_83>();
                                form83 = BL_FermentertoReceiverForm_83.GetList();
                            }
                        }
                        else
                        {
                            form83 = new List<FermentertoReceiverForm_83>();
                            form83 = BL_FermentertoReceiverForm_83.Search("", ddsearch.SelectedValue, txtpage.Text);
                        }
                              

                        if (Session["rolename"].ToString() == "Bond Officer")
                        {
                            if (ddsearch.SelectedValue == "gauged_date" )
                                {
                                var list = from s in form83
                                           where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.gauged_date==txtpage.Text && s.financial_year == user.financial_year
                                           select s;
                                grdFermentertoReceiver.DataSource = list.ToList();
                                grdFermentertoReceiver.DataBind();
                            }
                            else if(ddsearch.SelectedValue == "distillation_date")
                            {
                                var list = from s in form83
                                           where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.distillation_date==txtpage.Text && s.financial_year == user.financial_year
                                           select s;
                                grdFermentertoReceiver.DataSource = list.ToList();
                                grdFermentertoReceiver.DataBind();
                            }
                            else
                            {
                                var list = from s in form83
                                           where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.financial_year == user.financial_year
                                           select s;
                                grdFermentertoReceiver.DataSource = list.ToList();
                                grdFermentertoReceiver.DataBind();
                            }


                            foreach (GridViewRow dr1 in grdFermentertoReceiver.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = false;
                            }
                            btnAddRecord.Visible = false;

                        }
                        else if (Session["UserID"].ToString() == "Admin")
                        {
                            if (ddsearch.SelectedValue == "gauged_date")
                            {
                                var list = from s in form83
                                           where  s.gauged_date == txtpage.Text
                                           select s;
                                grdFermentertoReceiver.DataSource = list.ToList();
                                grdFermentertoReceiver.DataBind();
                            }
                            else if (ddsearch.SelectedValue == "distillation_date")
                            {
                                var list = from s in form83
                                           where  s.distillation_date == txtpage.Text
                                           select s;
                                grdFermentertoReceiver.DataSource = list.ToList();
                                grdFermentertoReceiver.DataBind();
                            }
                            else
                            {
                                var list = from s in form83
                                           select s;
                                grdFermentertoReceiver.DataSource = list.ToList();
                                grdFermentertoReceiver.DataBind();
                            }
                            foreach (GridViewRow dr1 in grdFermentertoReceiver.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = true;
                            }
                        }

                        else
                        {
                            if (ddsearch.SelectedValue == "gauged_date")
                            {
                                var list = from s in form83
                                           where s.party_code == Session["party_code"].ToString() && s.gauged_date == txtpage.Text && s.financial_year == user.financial_year
                                           select s;
                                grdFermentertoReceiver.DataSource = list.ToList();
                                grdFermentertoReceiver.DataBind();
                            }
                            else if (ddsearch.SelectedValue == "distillation_date")
                            {
                                var list = from s in form83
                                           where s.party_code == Session["party_code"].ToString()  && s.distillation_date == txtpage.Text && s.financial_year == user.financial_year
                                           select s;
                                grdFermentertoReceiver.DataSource = list.ToList();
                                grdFermentertoReceiver.DataBind();
                            }
                            else
                            {
                                var list = from s in form83
                                           where s.party_code == Session["party_code"].ToString() && s.financial_year == user.financial_year
                                           select s;
                                grdFermentertoReceiver.DataSource = list.ToList();
                                grdFermentertoReceiver.DataBind();
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
            GridViewRow row = grdFermentertoReceiver.TopPagerRow;
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
                grdFermentertoReceiver.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdFermentertoReceiver.PageIndex = a - 1;
            }
            

            Session["UserID"] = Session["UserID"];
            user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            party_code.Value = user.party_code.ToString();
            _party_code = party_code.Value;
            if (user != null)
            {
                Session["party_code"] = user.party_code;
                Session["rolename"] = user.role_name;
                form83 = new List<FermentertoReceiverForm_83>();
                form83 = BL_FermentertoReceiverForm_83.GetList();
                //grdFermentertoReceiver.DataSource = form83;
                //grdFermentertoReceiver.DataBind();


                if (Session["rolename"].ToString() == "Bond Officer")
                {
                    var list = from s in form83
                               where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.financial_year == user.financial_year
                               select s;
                    grdFermentertoReceiver.DataSource = list.ToList();
                    grdFermentertoReceiver.DataBind();
                    foreach (GridViewRow dr1 in grdFermentertoReceiver.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                    btnAddRecord.Visible = false;

                }
                else if (Session["UserID"].ToString() == "Admin")
                {
                    var list = from s in form83
                               select s;
                    grdFermentertoReceiver.DataSource = list.ToList();
                    grdFermentertoReceiver.DataBind();
                    foreach (GridViewRow dr1 in grdFermentertoReceiver.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = true;
                    }
                }

                else
                {
                    var list = from s in form83
                               where s.party_code == Session["party_code"].ToString() && s.financial_year == user.financial_year
                               select s;
                    grdFermentertoReceiver.DataSource = list.ToList();
                    grdFermentertoReceiver.DataBind();
                }
            }


        }

        protected void grdFermentertoReceiver_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdFermentertoReceiver.TopPagerRow;

            if (grdFermentertoReceiver.Rows.Count > 0)
            {
                grdFermentertoReceiver.TopPagerRow.Visible = true;
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
            if (Session["ftrsearch"] != null && Session["ftrtext"] != null)
            {
                ddsearch.SelectedValue = Session["ftrsearch"].ToString();
                txtpages.Text = Session["ftrtext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdFermentertoReceiver.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdFermentertoReceiver.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdFermentertoReceiver.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdFermentertoReceiver.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdFermentertoReceiver.PageIndex == 0)
            {
                ((ImageButton)grdFermentertoReceiver.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdFermentertoReceiver.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdFermentertoReceiver.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdFermentertoReceiver.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdFermentertoReceiver.PageIndex + 1 == grdFermentertoReceiver.PageCount)
            {
                ((ImageButton)grdFermentertoReceiver.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdFermentertoReceiver.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdFermentertoReceiver.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdFermentertoReceiver.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["ftrsearch"] = null;
            Session["ftrtext"] = null;
            user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            party_code.Value = user.party_code.ToString();
            _party_code = party_code.Value;
            if (user != null)
            {
                Session["party_code"] = user.party_code;
                Session["rolename"] = user.role_name;
                form83 = new List<FermentertoReceiverForm_83>();
                form83 = BL_FermentertoReceiverForm_83.GetList();
                //grdFermentertoReceiver.DataSource = form83;
                //grdFermentertoReceiver.DataBind();


                if (Session["rolename"].ToString() == "Bond Officer")
                {
                    var list = from s in form83
                               where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.financial_year == user.financial_year
                               select s;
                    grdFermentertoReceiver.DataSource = list.ToList();
                    grdFermentertoReceiver.DataBind();
                    foreach (GridViewRow dr1 in grdFermentertoReceiver.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                    btnAddRecord.Visible = false;

                }
                else if (Session["UserID"].ToString() == "Admin")
                {
                    var list = from s in form83
                               select s;
                    grdFermentertoReceiver.DataSource = list.ToList();
                    grdFermentertoReceiver.DataBind();
                    foreach (GridViewRow dr1 in grdFermentertoReceiver.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = true;
                    }
                }

                else
                {
                    var list = from s in form83
                               where s.party_code == Session["party_code"].ToString() && s.financial_year == user.financial_year
                               select s; 
                    grdFermentertoReceiver.DataSource = list.ToList();
                    grdFermentertoReceiver.DataBind();
                }

            }    }
        }
}