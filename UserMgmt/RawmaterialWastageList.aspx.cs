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
    public partial class RawmaterialWastageList : System.Web.UI.Page
    {
        List<RawmaterialWastage> Fermenter = new List<RawmaterialWastage>();
        public static UserDetails user = new UserDetails();
        static string _party_code;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    grdFermenterSetup.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];

                    Session["UserID"] = Session["UserID"];
                    Fermenter = new List<RawmaterialWastage>();
                    Fermenter = BL_RawMaterialWastage.GetDispatch();

                    user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    party_code.Value = user.party_code.ToString();
                    _party_code = party_code.Value;
                    if (user != null)
                    {
                        Session["party_code"] = user.party_code;
                        Session["rolename"] = user.role_name;
                        if (user.role_name == "Bond Officer")
                        {
                            var list = from s in Fermenter
                                       where s.party_code == user.party_code && s.record_status != "N" && s.financial_year==user.financial_year
                                       select s;
                            grdFermenterSetup.DataSource = list.ToList();
                            grdFermenterSetup.DataBind();
                            foreach (GridViewRow dr1 in grdFermenterSetup.Rows)
                            {
                                Label lbl = dr1.FindControl("lblstatus") as Label;
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = false;
                                //lbl.Visible = false;
                            }
                            btnAddRecord.Visible = false;

                        }
                        else if (Session["UserID"].ToString() == "Admin")
                        {
                            var list = from s in Fermenter
                                       select s;
                            grdFermenterSetup.DataSource = list.ToList();
                            grdFermenterSetup.DataBind();
                            foreach (GridViewRow dr1 in grdFermenterSetup.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = true;
                            }
                            btnAddRecord.Visible = false;
                        }
                        else
                        {
                            var list = from s in Fermenter
                                       where s.party_code == user.party_code && s.financial_year == user.financial_year
                                       select s;
                            grdFermenterSetup.DataSource = list.ToList();
                            grdFermenterSetup.DataBind();
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

        protected void lnkRMR_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialReceiptList");
        }
        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = 0;
            Response.Redirect("RawmaterialWastageForm.aspx");
        }

        protected void btnVATtansfers_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATTransferList");
        }
        protected void btnDistillation_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DistillationSetupList.aspx");

        }

        protected void btnfermenter_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialtoFermenterList");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string FermenterId = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["rwfinancial_year"] = financial_year;
            Session["UserID"] = Session["UserID"];
            Session["FermenterId"] = FermenterId;
            Session["rtype"] = "2";
            Response.Redirect("RawmaterialWastageForm.aspx");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string FermenterId = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["rwfinancial_year"] = financial_year;
            Session["UserID"] = Session["UserID"];
            Session["FermenterId"] = FermenterId;
            Session["rtype"] = "1";
            Response.Redirect("RawmaterialWastageForm.aspx");
        }

        protected void grdFermenterSetup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.NewPageIndex < 0)
                {
                    grdFermenterSetup.PageIndex = 0;
                }
                else
                {
                    grdFermenterSetup.PageIndex = e.NewPageIndex;
                }
                //user = new UserDetails();
                //   user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                GridViewRow row = grdFermenterSetup.TopPagerRow;
                TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
                if (Session["rwasearch"] != null && Session["rwatext"] != null)
                {
                    ddsearch.SelectedValue = Session["rwasearch"].ToString();
                    txtpage.Text = Session["rwatext"].ToString();
                    if (ddsearch.SelectedValue != "Select")
                    {

                        if (txtpage != null)
                        {
                            Fermenter = new List<RawmaterialWastage>();
                            Fermenter = BL_RawMaterialWastage.Search("", ddsearch.SelectedValue, txtpage.Text);
                        }
                    }
                }
                else
                {
                    Fermenter = new List<RawmaterialWastage>();
                    Fermenter = BL_RawMaterialWastage.GetDispatch();
                }
                    if (Session["rolename"].ToString() == "Bond Officer")
                {
                    var list = from s in Fermenter
                               where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.financial_year == user.financial_year
                               select s;
                    grdFermenterSetup.DataSource = list.ToList();
                    grdFermenterSetup.DataBind();
                    foreach (GridViewRow dr1 in grdFermenterSetup.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                    btnAddRecord.Visible = false;

                }
                else if (Session["UserID"].ToString() == "Admin")
                {
                    var list = from s in Fermenter
                               select s;
                    grdFermenterSetup.DataSource = list.ToList();
                    grdFermenterSetup.DataBind();
                    foreach (GridViewRow dr1 in grdFermenterSetup.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = true;
                    }
                }
                else
                {
                    var list = from s in Fermenter
                               where s.party_code == Session["party_code"].ToString() && s.financial_year == user.financial_year
                               select s;
                    grdFermenterSetup.DataSource = list.ToList();
                    grdFermenterSetup.DataBind();
                }





            }
        }

        protected void lnkOpeningBalance_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("OpeningBalanceList");

        }

        protected void lnkRawMaterialWastage_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawmaterialWastageList.aspx"); 
        }
        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdFermenterSetup.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                Fermenter = new List<RawmaterialWastage>();
                Fermenter = BL_RawMaterialWastage.GetDispatch();

                if (user.role_name == "Bond Officer")
                {
                    var list = from s in Fermenter
                               where s.party_code == user.party_code && s.record_status != "N"
                               select s;
                    grdFermenterSetup.DataSource = list.ToList();
                    grdFermenterSetup.DataBind();
                    foreach (GridViewRow dr1 in grdFermenterSetup.Rows)
                    {
                        Label lbl = dr1.FindControl("lblstatus") as Label;
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                        //lbl.Visible = false;
                    }
                    btnAddRecord.Visible = false;

                }
                else if (Session["UserID"].ToString() == "Admin")
                {
                    var list = from s in Fermenter
                               select s;
                    grdFermenterSetup.DataSource = list.ToList();
                    grdFermenterSetup.DataBind();
                    foreach (GridViewRow dr1 in grdFermenterSetup.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = true;
                    }
                }
                else
                {
                    var list = from s in Fermenter
                               where s.party_code == user.party_code
                               select s;
                    grdFermenterSetup.DataSource = list.ToList();
                    grdFermenterSetup.DataBind();
                }

            }
            return Fermenter.ToString();
        }



        protected void btnsearch_Click(object sender, EventArgs e)
        {


            GridViewRow row = grdFermenterSetup.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["rwasearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["rwatext"] = txtpage.Text;
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    { 
                        Fermenter = new List<RawmaterialWastage>();
                        Fermenter = BL_RawMaterialWastage.Search("", ddsearch.SelectedValue, txtpage.Text);

                        if (user.role_name == "Bond Officer")
                        {
                            var list = from s in Fermenter
                                       where s.party_code == user.party_code && s.record_status != "N" && s.financial_year == user.financial_year
                                       select s;
                            grdFermenterSetup.DataSource = list.ToList();
                            grdFermenterSetup.DataBind();
                            foreach (GridViewRow dr1 in grdFermenterSetup.Rows)
                            {
                                Label lbl = dr1.FindControl("lblstatus") as Label;
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = false;
                                //lbl.Visible = false;
                            }
                            btnAddRecord.Visible = false;

                        }
                        else if (Session["UserID"].ToString() == "Admin")
                        {
                            var list = from s in Fermenter
                                       select s;
                            grdFermenterSetup.DataSource = list.ToList();
                            grdFermenterSetup.DataBind();
                            foreach (GridViewRow dr1 in grdFermenterSetup.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = true;
                            }
                        }
                        else
                        {
                            var list = from s in Fermenter
                                       where s.party_code == user.party_code && s.financial_year == user.financial_year
                                       select s;
                            grdFermenterSetup.DataSource = list.ToList();
                            grdFermenterSetup.DataBind();
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
            GridViewRow row = grdFermenterSetup.TopPagerRow;
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
                grdFermenterSetup.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdFermenterSetup.PageIndex = a - 1;
            }

            
            Session["UserID"] = Session["UserID"];
            Fermenter = new List<RawmaterialWastage>();
            Fermenter = BL_RawMaterialWastage.GetDispatch();

            user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            party_code.Value = user.party_code.ToString();
            _party_code = party_code.Value;
            if (user != null)
            {
                Session["party_code"] = user.party_code;
                Session["rolename"] = user.role_name;
                if (user.role_name == "Bond Officer")
                {
                    var list = from s in Fermenter
                               where s.party_code == user.party_code && s.record_status != "N" && s.financial_year == user.financial_year
                               select s;
                    grdFermenterSetup.DataSource = list.ToList();
                    grdFermenterSetup.DataBind();
                    foreach (GridViewRow dr1 in grdFermenterSetup.Rows)
                    {
                        Label lbl = dr1.FindControl("lblstatus") as Label;
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                        //lbl.Visible = false;
                    }
                    btnAddRecord.Visible = false;

                }
                else if (Session["UserID"].ToString() == "Admin")
                {
                    var list = from s in Fermenter
                               select s;
                    grdFermenterSetup.DataSource = list.ToList();
                    grdFermenterSetup.DataBind();
                    foreach (GridViewRow dr1 in grdFermenterSetup.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = true;
                    }
                }
                else
                {
                    var list = from s in Fermenter
                               where s.party_code == user.party_code && s.financial_year == user.financial_year
                               select s;
                    grdFermenterSetup.DataSource = list.ToList();
                    grdFermenterSetup.DataBind();
                }
            }


        }

        protected void grdFermenterSetup_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdFermenterSetup.TopPagerRow;
            if (grdFermenterSetup.Rows.Count > 0)
            {
                grdFermenterSetup.TopPagerRow.Visible = true;
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
            if (Session["rwasearch"] != null && Session["rwatext"] != null)
            {
                ddsearch.SelectedValue = Session["rwasearch"].ToString();
                txtpages.Text = Session["rwatext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdFermenterSetup.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdFermenterSetup.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdFermenterSetup.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdFermenterSetup.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdFermenterSetup.PageIndex == 0)
            {
                ((ImageButton)grdFermenterSetup.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdFermenterSetup.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdFermenterSetup.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdFermenterSetup.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdFermenterSetup.PageIndex + 1 == grdFermenterSetup.PageCount)
            {
                ((ImageButton)grdFermenterSetup.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdFermenterSetup.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdFermenterSetup.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdFermenterSetup.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["rwasearch"] = null;
            Session["rwatext"] = null;
            user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            Fermenter = new List<RawmaterialWastage>();
            Fermenter = BL_RawMaterialWastage.GetDispatch();
            party_code.Value = user.party_code.ToString();
            _party_code = party_code.Value;
            if (user != null)
            {
                Session["party_code"] = user.party_code;
                Session["rolename"] = user.role_name;
                if (user.role_name == "Bond Officer")
                {
                    var list = from s in Fermenter
                               where s.party_code == user.party_code && s.record_status != "N" && s.financial_year == user.financial_year
                               select s;
                    grdFermenterSetup.DataSource = list.ToList();
                    grdFermenterSetup.DataBind();
                    foreach (GridViewRow dr1 in grdFermenterSetup.Rows)
                    {
                        Label lbl = dr1.FindControl("lblstatus") as Label;
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                        //lbl.Visible = false;
                    }
                    btnAddRecord.Visible = false;

                }
                else if (Session["UserID"].ToString() == "Admin")
                {
                    var list = from s in Fermenter
                               select s;
                    grdFermenterSetup.DataSource = list.ToList();
                    grdFermenterSetup.DataBind();
                    foreach (GridViewRow dr1 in grdFermenterSetup.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = true;
                    }
                }
                else
                {
                    var list = from s in Fermenter
                               where s.party_code == user.party_code && s.financial_year == user.financial_year
                               select s;
                    grdFermenterSetup.DataSource = list.ToList();
                    grdFermenterSetup.DataBind();
                }
            }

        }
    }
}