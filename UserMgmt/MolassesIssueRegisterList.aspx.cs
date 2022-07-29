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
    public partial class MolassesIssueRegisterList : System.Web.UI.Page
    {
        List<Molasses_Issue_Register> mir = new List<Molasses_Issue_Register>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
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
                grdMolassesIssueView.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                Session["UserID"] = Session["UserID"];
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                if (user != null)
                {
                    Session["rolename"] = user.role_name;
                    Session["party_code"] = user.party_code;
                    mir = new List<Molasses_Issue_Register>();
                    string userid = Session["UserID"].ToString();
                    mir = BL_Molasses_Issue_Register.GetList();
                    if (userid == "Admin")
                    {
                        grdMolassesIssueView.DataSource = mir;
                        grdMolassesIssueView.DataBind();
                    }
                    else if (Session["rolename"].ToString()== "Bond Officer")
                    {
                        var list = from s in mir
                                   where s.party_code ==Session["party_code"].ToString() && s.record_status != "N" && s.financial_year==user.financial_year
                                   orderby Convert.ToDateTime( s.mir_entrydate) descending
                                   select s;
                        grdMolassesIssueView.DataSource = list.ToList();
                        grdMolassesIssueView.DataBind();
                        foreach (GridViewRow dr1 in grdMolassesIssueView.Rows)
                        {
                            LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                            btn.Visible = false;
                        }
                        btnAddRecord.Visible = false;
                    }
                    else
                    {
                        var list = from s in mir
                                   where s.party_code == Session["party_code"].ToString() && s.financial_year == user.financial_year
                                   orderby Convert.ToDateTime(s.mir_entrydate) descending
                                   select s;
                        grdMolassesIssueView.DataSource = list.ToList();
                        grdMolassesIssueView.DataBind();


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
        }
        protected void btnVATtansfers_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATTransferList");
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["rtype"] = 0;
            Session["UserID"] = Session["UserID"];
            Response.Redirect("MolassesIssueRegisterForm");
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

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string mirid = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblmirid") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["MIRfinancial_year"] = financial_year;
            Session["UserID"] = Session["UserID"].ToString();
            Session["mirid"] = mirid;
            Session["rtype"] = 1;
            Response.Redirect("MolassesIssueRegisterForm");
        }

      
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string mirid = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblmirid") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["MIRfinancial_year"] = financial_year;
            Session["UserID"] = Session["UserID"].ToString();
            Session["mirid"] = mirid;
            Session["rtype"] = 2;
            Response.Redirect("MolassesIssueRegisterForm");
        }
       
        protected void grdMolassesIssueView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["UserID"] != null)
            {
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                if (e.NewPageIndex == -1)
                {
                    grdMolassesIssueView.PageIndex = 0;
                }

                else
                {


                    grdMolassesIssueView.PageIndex = e.NewPageIndex;
                }
                GridViewRow row = grdMolassesIssueView.TopPagerRow;
                TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
                if (Session["mirsearch"] != null && Session["mirtext"] != null)
                {
                    ddsearch.SelectedValue = Session["mirsearch"].ToString();
                    txtpage.Text = Session["mirtext"].ToString();
                    if (ddsearch.SelectedValue != "Select")
                    {

                        if (txtpage != null)
                        {
                            if (ddsearch.SelectedValue == "mir_entrydate")
                            {
                                if (txtpage.Text.ToString().Length == 10)
                                {
                                    //rawmaterial = BL_RawMaterialReceipt.Search("molassesissueregister", ddsearch.SelectedValue, Convert.ToDateTime(txtpage.Text).ToString("yyyy/MM/dd").Replace("/" ,"-"));
                                    mir = new List<Molasses_Issue_Register>();

                                    mir = BL_Molasses_Issue_Register.GetList();
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                    txtpage.Focus();
                                    txtpage.Text = "";
                                    ddsearch.SelectedIndex = 0;
                                    mir = new List<Molasses_Issue_Register>();

                                    mir = BL_Molasses_Issue_Register.GetList();
                                }

                            }
                            else
                            {
                                mir = new List<Molasses_Issue_Register>();
                                mir = BL_Molasses_Issue_Register.Search("molassesissueregister", ddsearch.SelectedValue, txtpage.Text);
                            }
                        }
                    }
                }
                else
                {


                    mir = new List<Molasses_Issue_Register>();

                    mir = BL_Molasses_Issue_Register.GetList();
                }
                // user = BL_UserDetails.GetUser(Session["UserID"].ToString());
                if (Session["UserID"].ToString() == "Admin")
                {
                    if (ddsearch.SelectedValue == "mir_entrydate")
                    {
                        var list = from s in mir
                                   where  s.mir_entrydate == txtpage.Text
                                   orderby Convert.ToDateTime(s.mir_entrydate) descending
                                   select s;
                        grdMolassesIssueView.DataSource = list.ToList();
                        grdMolassesIssueView.DataBind();
                    }
                    else
                    {
                        grdMolassesIssueView.DataSource = mir;
                        grdMolassesIssueView.DataBind();
                    }
                }
                else if (Session["rolename"].ToString()=="Bond Officer")
                {
                    if (ddsearch.SelectedValue == "mir_entrydate")
                    {
                        var list = from s in mir
                                   where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.mir_entrydate==txtpage.Text && s.financial_year == user.financial_year
                                   orderby Convert.ToDateTime(s.mir_entrydate) descending
                                   select s;
                        grdMolassesIssueView.DataSource = list.ToList();
                        grdMolassesIssueView.DataBind();
                    }
                    else
                    {
                        var list = from s in mir
                                   where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.financial_year == user.financial_year
                                   orderby Convert.ToDateTime(s.mir_entrydate) descending
                                   select s;
                        grdMolassesIssueView.DataSource = list.ToList();
                        grdMolassesIssueView.DataBind();
                    }


                    foreach (GridViewRow dr1 in grdMolassesIssueView.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                    btnAddRecord.Visible = false;
                }
                else
                {
                    if (ddsearch.SelectedValue == "mir_entrydate")
                    {
                        var list = from s in mir
                                   where s.mir_entrydate == txtpage.Text && s.financial_year == user.financial_year
                                   orderby Convert.ToDateTime(s.mir_entrydate) descending
                                   select s;
                        grdMolassesIssueView.DataSource = list.ToList();
                        grdMolassesIssueView.DataBind();
                    }
                    else
                    {
                        var list = from s in mir
                                   where s.party_code == Session["party_code"].ToString() && s.financial_year == user.financial_year
                                   orderby Convert.ToDateTime(s.mir_entrydate) descending
                                   select s;
                        grdMolassesIssueView.DataSource = list.ToList();
                        grdMolassesIssueView.DataBind();
                    }
                }
            }
        }
        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdMolassesIssueView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                mir = new List<Molasses_Issue_Register>();
                mir = BL_Molasses_Issue_Register.GetList();
                if (Session["UserID"].ToString() == "Admin")
                {
                    grdMolassesIssueView.DataSource = mir;
                    grdMolassesIssueView.DataBind();
                }
                else if (Session["rolename"].ToString() == "Bond Officer")
                {
                    var list = from s in mir
                               where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.financial_year == user.financial_year
                               orderby Convert.ToDateTime(s.mir_entrydate) descending
                               select s;
                    grdMolassesIssueView.DataSource = list.ToList();
                    grdMolassesIssueView.DataBind();
                    foreach (GridViewRow dr1 in grdMolassesIssueView.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                    btnAddRecord.Visible = false;
                }
                else
                {
                    var list = from s in mir
                               where s.party_code == Session["party_code"].ToString() && s.financial_year == user.financial_year
                               orderby Convert.ToDateTime(s.mir_entrydate) descending
                               select s;
                    grdMolassesIssueView.DataSource = list.ToList();
                    grdMolassesIssueView.DataBind();
                }
            }
            return mir.ToString();
        }



        protected void btnsearch_Click(object sender, EventArgs e)
        {


            GridViewRow row = grdMolassesIssueView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["mirsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["mirtext"] = txtpage.Text;
                    if (ddsearch.SelectedValue == "mir_entrydate")
                    {
                        if (txtpage.Text.ToString().Length == 10)
                        {
                            //rawmaterial = BL_RawMaterialReceipt.Search("molassesissueregister", ddsearch.SelectedValue, Convert.ToDateTime(txtpage.Text).ToString("yyyy/MM/dd").Replace("/" ,"-"));
                            mir = new List<Molasses_Issue_Register>();

                            mir = BL_Molasses_Issue_Register.GetList();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                            txtpage.Focus();
                            txtpage.Text = "";
                            ddsearch.SelectedIndex = 0;
                            mir = new List<Molasses_Issue_Register>();

                            mir = BL_Molasses_Issue_Register.GetList();
                        }

                    }
                    else
                    {

                        mir = BL_Molasses_Issue_Register.Search("molassesissueregister", ddsearch.SelectedValue, txtpage.Text);
                    }

                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        if (Session["UserID"].ToString() == "Admin")
                        {
                            if (ddsearch.SelectedValue == "mir_entrydate")
                            {
                                var list = from s in mir
                                           where s.mir_entrydate == txtpage.Text 
                                           orderby Convert.ToDateTime(s.mir_entrydate) descending
                                           select s;
                                grdMolassesIssueView.DataSource = list.ToList();
                                grdMolassesIssueView.DataBind();
                            }
                            else
                            {
                                grdMolassesIssueView.DataSource = mir;
                                grdMolassesIssueView.DataBind();
                            }
                        }
                        else if (Session["rolename"].ToString() == "Bond Officer")
                        {
                            if (ddsearch.SelectedValue == "mir_entrydate")
                            {
                                var list = from s in mir
                                           where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.mir_entrydate == txtpage.Text && s.financial_year == user.financial_year
                                           orderby Convert.ToDateTime(s.mir_entrydate) descending
                                           select s;
                                grdMolassesIssueView.DataSource = list.ToList();
                                grdMolassesIssueView.DataBind();
                            }
                            else
                            {
                                var list = from s in mir
                                           where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.financial_year == user.financial_year
                                           orderby Convert.ToDateTime(s.mir_entrydate) descending
                                           select s;
                                grdMolassesIssueView.DataSource = list.ToList();
                                grdMolassesIssueView.DataBind();
                            }


                            foreach (GridViewRow dr1 in grdMolassesIssueView.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = false;
                            }
                            btnAddRecord.Visible = false;
                        }
                        else
                        {
                            if (ddsearch.SelectedValue == "mir_entrydate")
                            {
                                var list = from s in mir
                                           where s.mir_entrydate == txtpage.Text && s.financial_year == user.financial_year
                                           orderby Convert.ToDateTime(s.mir_entrydate) descending
                                           select s;
                                grdMolassesIssueView.DataSource = list.ToList();
                                grdMolassesIssueView.DataBind();
                            }
                            else
                            {
                                var list = from s in mir
                                           where s.party_code == Session["party_code"].ToString() && s.financial_year == user.financial_year
                                           orderby Convert.ToDateTime(s.mir_entrydate) descending
                                           select s;
                                grdMolassesIssueView.DataSource = list.ToList();
                                grdMolassesIssueView.DataBind();
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
            GridViewRow row = grdMolassesIssueView.TopPagerRow;
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
                grdMolassesIssueView.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdMolassesIssueView.PageIndex = a - 1;
            }
            
            
            string userid = Session["UserID"].ToString();
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            if (user != null)
            {
                Session["rolename"] = user.role_name;
                Session["party_code"] = user.party_code;
                mir = new List<Molasses_Issue_Register>();
                
                mir = BL_Molasses_Issue_Register.GetList();
                if (userid == "Admin")
                {
                    grdMolassesIssueView.DataSource = mir;
                    grdMolassesIssueView.DataBind();
                }
                else if (Session["rolename"].ToString() == "Bond Officer")
                {
                    var list = from s in mir
                               where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.financial_year == user.financial_year
                               orderby Convert.ToDateTime(s.mir_entrydate) descending
                               select s;
                    grdMolassesIssueView.DataSource = list.ToList();
                    grdMolassesIssueView.DataBind();
                    foreach (GridViewRow dr1 in grdMolassesIssueView.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                    btnAddRecord.Visible = false;
                }
                else
                {
                    var list = from s in mir
                               where s.party_code == Session["party_code"].ToString() && s.financial_year == user.financial_year
                               orderby Convert.ToDateTime(s.mir_entrydate) descending
                               select s;
                    grdMolassesIssueView.DataSource = list.ToList();
                    grdMolassesIssueView.DataBind();


                }
            }


        }

        protected void grdMolassesIssueView_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdMolassesIssueView.TopPagerRow;
            if (grdMolassesIssueView.Rows.Count > 0)
            {
                grdMolassesIssueView.TopPagerRow.Visible = true;
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
            if (Session["mirsearch"] != null && Session["mirtext"] != null)
            {
                ddsearch.SelectedValue = Session["mirsearch"].ToString();
                txtpages.Text = Session["mirtext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdMolassesIssueView.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdMolassesIssueView.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdMolassesIssueView.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdMolassesIssueView.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdMolassesIssueView.PageIndex == 0)
            {
                ((ImageButton)grdMolassesIssueView.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdMolassesIssueView.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdMolassesIssueView.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdMolassesIssueView.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdMolassesIssueView.PageIndex + 1 == grdMolassesIssueView.PageCount)
            {
                ((ImageButton)grdMolassesIssueView.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdMolassesIssueView.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdMolassesIssueView.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdMolassesIssueView.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["mirsearch"] = null;
            Session["mirtext"] = null;
            string userid = Session["UserID"].ToString();
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            if (user != null)
            {
                Session["rolename"] = user.role_name;
                Session["party_code"] = user.party_code;
                mir = new List<Molasses_Issue_Register>();

                mir = BL_Molasses_Issue_Register.GetList();
                if (userid == "Admin")
                {
                    grdMolassesIssueView.DataSource = mir;
                    grdMolassesIssueView.DataBind();
                }
                else if (Session["rolename"].ToString() == "Bond Officer")
                {
                    var list = from s in mir
                               where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.financial_year == user.financial_year
                               orderby Convert.ToDateTime(s.mir_entrydate) descending
                               select s;
                    grdMolassesIssueView.DataSource = list.ToList();
                    grdMolassesIssueView.DataBind();
                    foreach (GridViewRow dr1 in grdMolassesIssueView.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }
                    btnAddRecord.Visible = false;
                }
                else
                {
                    var list = from s in mir
                               where s.party_code == Session["party_code"].ToString() && s.financial_year == user.financial_year
                               orderby Convert.ToDateTime(s.mir_entrydate) descending
                               select s;
                    grdMolassesIssueView.DataSource = list.ToList();
                    grdMolassesIssueView.DataBind();


                }
            }
        }
    }
}