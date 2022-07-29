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
    public partial class DistillationSetupList : System.Web.UI.Page
    {
        List<Distillation> Distillation = new List<Distillation>();
        public  UserDetails user = new UserDetails();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    grdDistillationSetup.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];

                    user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        Session["rolename"] = user.role_name;
                        Session["partycode"] = user.party_code;
                        Session["UserID"] = Session["UserID"];
                        Distillation = new List<Distillation>();
                        Distillation = BL_Distillation.GetList();
                        //grdDistillationSetup.DataSource = Distillation;
                        //grdDistillationSetup.DataBind();


                        if (Session["rolename"].ToString() == "Bond Officer")
                        {
                            var list = from s in Distillation
                                       where s.party_code == Session["partycode"].ToString() && s.record_status != "N" && s.financial_year==user.financial_year
                                       select s;
                            grdDistillationSetup.DataSource = list.ToList();
                            grdDistillationSetup.DataBind();
                            foreach (GridViewRow dr1 in grdDistillationSetup.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = false;
                            }
                            btnAddRecord.Visible = false;
                        }

                        else if (Session["UserID"].ToString() == "Admin")
                        {
                            var list = from s in Distillation
                                       select s;
                            grdDistillationSetup.DataSource = list.ToList();
                            grdDistillationSetup.DataBind();
                            foreach (GridViewRow dr1 in grdDistillationSetup.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = true;
                            }
                        }

                        else
                        {
                            var list = from s in Distillation
                                       where s.party_code == Session["partycode"].ToString() && s.financial_year == user.financial_year
                                       select s;
                            grdDistillationSetup.DataSource = list.ToList();
                            grdDistillationSetup.DataBind();
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
        protected void lnkRawMaterialWastage_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawmaterialWastageList.aspx");
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
            Session["rtype"] = "0";
            Response.Redirect("DistillationSetupForm");
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
            string DistillationId = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDistillationID") as Label).Text;
            string rawmaterialId = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrawmaterialfermenterid") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["Difinancial_year"] = financial_year;
            Session["UserID"] = Session["UserID"];
            Session["DistillationId"] = DistillationId;
            Session["RawmaterialId"] = rawmaterialId;
            Session["rtype"] = "2";
            Response.Redirect("DistillationSetupForm.aspx");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer; 
            string DistillationId = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDistillationID") as Label).Text;
            string rawmaterialId = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrawmaterialfermenterid") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["Difinancial_year"] = financial_year;
            Session["UserID"] = Session["UserID"];
            Session["DistillationId"] = DistillationId;
            Session["RawmaterialId"] = rawmaterialId;
            Session["rtype"] = "1";
            Response.Redirect("DistillationSetupForm.aspx");
        }

       
        protected void grdDistillationSetup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.NewPageIndex == -1)
                {
                    grdDistillationSetup.PageIndex = 0;
                }
                else
                {
                    grdDistillationSetup.PageIndex = e.NewPageIndex;
                }
                GridViewRow row = grdDistillationSetup.TopPagerRow;
                TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
                if (Session["dissearch"] != null && Session["distext"] != null)
                {
                    ddsearch.SelectedValue = Session["dissearch"].ToString();
                    txtpage.Text = Session["distext"].ToString();
                    if (ddsearch.SelectedValue != "Select")
                    {

                        if (txtpage != null)
                        {
                            if (ddsearch.SelectedValue == "setup_date" || ddsearch.SelectedValue == "distillation_date")
                            {
                                if (txtpage.Text.ToString().Length == 10)
                                {

                                    Distillation = new List<Distillation>();
                                    Distillation = BL_Distillation.Search("", ddsearch.SelectedValue, Convert.ToDateTime(txtpage.Text).ToString("yyyy/MM/dd").Replace("/", "-"));

                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                    txtpage.Focus();
                                    txtpage.Text = "";
                                    Distillation = new List<Distillation>();
                                    Distillation = BL_Distillation.GetList();
                                }
                            }
                            else
                            {
                                Distillation = new List<Distillation>();
                                Distillation = BL_Distillation.Search("", ddsearch.SelectedValue, txtpage.Text);
                            }
                        }
                    }
                }
                else
                {
                    Distillation = new List<Distillation>();
                    Distillation = BL_Distillation.GetList();
                }
                user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                if (Session["rolename"].ToString() == "Bond Officer")
                {
                    var list = from s in Distillation
                               where s.party_code == Session["partycode"].ToString() && s.record_status != "N" && s.financial_year == user.financial_year
                               select s;
                    grdDistillationSetup.DataSource = list.ToList();
                    grdDistillationSetup.DataBind();
                    foreach (GridViewRow dr1 in grdDistillationSetup.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                    btnAddRecord.Visible = false;

                }
                else if (Session["UserID"].ToString() == "Admin")
                {
                    var list = from s in Distillation
                               select s;
                    grdDistillationSetup.DataSource = list.ToList();
                    grdDistillationSetup.DataBind();
                    foreach (GridViewRow dr1 in grdDistillationSetup.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = true;
                    }
                }
                else
                {
                    var list = from s in Distillation
                               where s.party_code == Session["partycode"].ToString() && s.financial_year == user.financial_year
                               select s;
                    grdDistillationSetup.DataSource = list.ToList();
                    grdDistillationSetup.DataBind();
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
            GridViewRow row = grdDistillationSetup.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                Distillation = new List<Distillation>();
                Distillation = BL_Distillation.GetList();
                //user = new UserDetails();
                //user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                if (Session["rolename"].ToString() == "Bond Officer")
                {
                    var list = from s in Distillation
                               where s.party_code == Session["partycode"].ToString() && s.record_status != "N"
                               select s;
                    grdDistillationSetup.DataSource = list.ToList();
                    grdDistillationSetup.DataBind();
                    foreach (GridViewRow dr1 in grdDistillationSetup.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                    btnAddRecord.Visible = false;

                }
                else if (Session["UserID"].ToString() == "Admin")
                {
                    var list = from s in Distillation
                               select s;
                    grdDistillationSetup.DataSource = list.ToList();
                    grdDistillationSetup.DataBind();
                    foreach (GridViewRow dr1 in grdDistillationSetup.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = true;
                    }
                }
                else
                {
                    var list = from s in Distillation
                               where s.party_code == Session["partycode"].ToString()
                               select s;
                    grdDistillationSetup.DataSource = list.ToList();
                    grdDistillationSetup.DataBind();
                }



            }
            return Distillation.ToString();
        }



        protected void btnsearch_Click(object sender, EventArgs e)
        {


            GridViewRow row = grdDistillationSetup.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["dissearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["distext"] = txtpage.Text;
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        if (ddsearch.SelectedValue == "setup_date" || ddsearch.SelectedValue == "distillation_date")
                        {
                            if (txtpage.Text.ToString().Length == 10)
                            {

                                //Distillation = new List<Distillation>();
                                //DateTime date = DateTime.Parse(txtpage.Text);
                                //string day = date.Day.ToString();
                                //string month = date.ToString("MM");
                                //string year = date.Year.ToString();
                                //txtpage.Text = year + "-" + month + "-" + day;
                                //string a = Convert.ToDateTime(txtpage.Text).ToString("yyyy/MM/dd").Replace("/","-");
                                //Distillation= BL_Distillation.Search("", ddsearch.SelectedValue,txtpage.Text );
                                Distillation = new List<Distillation>();
                                Distillation = BL_Distillation.GetList();

                                if (Session["rolename"].ToString() == "Bond Officer")
                                {

                                    if (ddsearch.SelectedValue == "setup_date")
                                    {
                                        var list = from s in Distillation
                                                   where s.party_code == Session["partycode"].ToString() && s.record_status != "N" && s.setup_date == txtpage.Text && s.financial_year == user.financial_year
                                                   select s;
                                        grdDistillationSetup.DataSource = list.ToList();
                                        grdDistillationSetup.DataBind();
                                    }
                                    else if(ddsearch.SelectedValue == "distillation_date")
                                        {
                                        var list1 = from s in Distillation
                                                   where s.party_code == Session["partycode"].ToString() && s.record_status != "N"&& s.distillation_date==txtpage.Text && s.financial_year == user.financial_year
                                                    select s;
                                        grdDistillationSetup.DataSource = list1.ToList();
                                        grdDistillationSetup.DataBind();

                                    }
                                    

                                    foreach (GridViewRow dr1 in grdDistillationSetup.Rows)
                                    {
                                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                        btn.Visible = false;
                                    }
                                    btnAddRecord.Visible = false;

                                }
                                else if (Session["UserID"].ToString() == "Admin")
                                {
                                    if (ddsearch.SelectedValue == "setup_date")
                                    {
                                        var list = from s in Distillation
                                                   where  s.setup_date == txtpage.Text
                                                   select s;
                                        grdDistillationSetup.DataSource = list.ToList();
                                        grdDistillationSetup.DataBind();
                                    }
                                    else if (ddsearch.SelectedValue == "distillation_date")
                                    {
                                        var list1 = from s in Distillation
                                                    where  s.distillation_date == txtpage.Text
                                                    select s;
                                        grdDistillationSetup.DataSource = list1.ToList();
                                        grdDistillationSetup.DataBind();

                                    }
                                    foreach (GridViewRow dr1 in grdDistillationSetup.Rows)
                                    {
                                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                        btn.Visible = true;
                                    }
                                }
                                else
                                {
                                    if (ddsearch.SelectedValue == "setup_date")
                                    {
                                        var list = from s in Distillation
                                                   where s.party_code == Session["partycode"].ToString()  && s.setup_date == txtpage.Text && s.financial_year == user.financial_year
                                                   select s;
                                        grdDistillationSetup.DataSource = list.ToList();
                                        grdDistillationSetup.DataBind();
                                    }
                                    else if (ddsearch.SelectedValue == "distillation_date")
                                    {
                                        var list1 = from s in Distillation
                                                    where s.party_code == Session["partycode"].ToString() && s.distillation_date == txtpage.Text && s.financial_year == user.financial_year
                                                    select s;
                                        grdDistillationSetup.DataSource = list1.ToList();
                                        grdDistillationSetup.DataBind();

                                    }
                                }


                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                txtpage.Focus();
                                txtpage.Text = "";
                                Distillation = new List<Distillation>();
                                Distillation = BL_Distillation.GetList();
                            }
                        }
                        else
                        {
                            Distillation = new List<Distillation>();
                            Distillation = BL_Distillation.Search("", ddsearch.SelectedValue, txtpage.Text);
                        

                        if (Session["rolename"].ToString() == "Bond Officer")
                        {
                            var list = from s in Distillation
                                       where s.party_code == Session["partycode"].ToString() && s.record_status != "N" && s.financial_year == user.financial_year
                                       select s;
                            grdDistillationSetup.DataSource = list.ToList();
                            grdDistillationSetup.DataBind();
                            foreach (GridViewRow dr1 in grdDistillationSetup.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = false;
                            }
                            btnAddRecord.Visible = false;

                        }
                        else if (Session["UserID"].ToString() == "Admin")
                        {
                            var list = from s in Distillation
                                       select s;
                            grdDistillationSetup.DataSource = list.ToList();
                            grdDistillationSetup.DataBind();
                            foreach (GridViewRow dr1 in grdDistillationSetup.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = true;
                            }
                        }
                        else
                        {
                            var list = from s in Distillation
                                       where s.party_code == Session["partycode"].ToString() && s.financial_year == user.financial_year
                                       select s;
                            grdDistillationSetup.DataSource = list.ToList();
                            grdDistillationSetup.DataBind();
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
            GridViewRow row = grdDistillationSetup.TopPagerRow;
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
                grdDistillationSetup.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdDistillationSetup.PageIndex = a - 1;
            }
            Distillation = new List<Distillation>();
            Distillation = BL_Distillation.GetList();

            if (Session["rolename"].ToString() == "Bond Officer")
            {
                var list = from s in Distillation
                           where s.party_code == Session["partycode"].ToString() && s.record_status != "N"
                           select s;
                grdDistillationSetup.DataSource = list.ToList();
                grdDistillationSetup.DataBind();
                foreach (GridViewRow dr1 in grdDistillationSetup.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    btn.Visible = false;
                }
                btnAddRecord.Visible = false;

            }
            else if (Session["UserID"].ToString() == "Admin")
            {
                var list = from s in Distillation
                           select s;
                grdDistillationSetup.DataSource = list.ToList();
                grdDistillationSetup.DataBind();
                foreach (GridViewRow dr1 in grdDistillationSetup.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    btn.Visible = true;
                }
            }
            else
            {
                var list = from s in Distillation
                           where s.party_code == Session["partycode"].ToString()
                           select s;
                grdDistillationSetup.DataSource = list.ToList();
                grdDistillationSetup.DataBind();
            }



        }

        protected void grdDistillationSetup_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdDistillationSetup.TopPagerRow;
            if (grdDistillationSetup.PageCount != 0)
            {
                grdDistillationSetup.TopPagerRow.Visible = true;
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
            if (Session["dissearch"] != null && Session["distext"] != null)
            {
                ddsearch.SelectedValue = Session["dissearch"].ToString();
                txtpages.Text = Session["distext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdDistillationSetup.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdDistillationSetup.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdDistillationSetup.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdDistillationSetup.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdDistillationSetup.PageIndex == 0)
            {
                ((ImageButton)grdDistillationSetup.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdDistillationSetup.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdDistillationSetup.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdDistillationSetup.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdDistillationSetup.PageIndex + 1 == grdDistillationSetup.PageCount)
            {
                ((ImageButton)grdDistillationSetup.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdDistillationSetup.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdDistillationSetup.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdDistillationSetup.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["dissearch"] = null;
            Session["distext"] = null;
            Distillation = new List<Distillation>();
            Distillation = BL_Distillation.GetList();

            if (Session["rolename"].ToString() == "Bond Officer")
            {
                var list = from s in Distillation
                           where s.party_code == Session["partycode"].ToString() && s.record_status != "N"
                           select s;
                grdDistillationSetup.DataSource = list.ToList();
                grdDistillationSetup.DataBind();
                foreach (GridViewRow dr1 in grdDistillationSetup.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    btn.Visible = false;
                }
                btnAddRecord.Visible = false;

            }
            else if (Session["UserID"].ToString() == "Admin")
            {
                var list = from s in Distillation
                           select s;
                grdDistillationSetup.DataSource = list.ToList();
                grdDistillationSetup.DataBind();
                foreach (GridViewRow dr1 in grdDistillationSetup.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    btn.Visible = true;
                }
            }
            else
            {
                var list = from s in Distillation
                           where s.party_code == Session["partycode"].ToString()
                           select s;
                grdDistillationSetup.DataSource = list.ToList();
                grdDistillationSetup.DataBind();
            }


        }
    }
}