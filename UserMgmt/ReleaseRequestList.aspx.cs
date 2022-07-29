using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class ReleaseRequestList : System.Web.UI.Page
    {
        List<Release_Request> rr = new List<Release_Request>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    Session["party"] = user.party_code;
                    if (user != null)
                    {
                        rr = new List<Release_Request>();
                        rr = BL_Release_Request.GetList();
                        if(user.party_type== "M & tP")
                        {
                            molasses.Visible = false;
                            ENA.Visible = true;
                            MTB.Visible = false;
                            ETB.Visible = true;
                        }
                        else
                        {
                            molasses.Visible =true;
                            ENA.Visible =false;
                            MTB.Visible =true;
                            ETB.Visible =false;

                        }
                        grdMolassesReleaseRequest.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                        if (user.role_name.Trim() == "Assistant Commissioner")
                        {
                            //var list = from s in rr
                            //           where s.district_code == user.district_code
                            //           select s;
                            //grdMolassesReleaseRequest.DataSource = list.ToList();
                            //grdMolassesReleaseRequest.DataBind();
                            //grdMolassesReleaseRequest.Columns[grdMolassesReleaseRequest.Columns.Count - 1].Visible = false;
                            Session["UserID"] = Session["UserID"];
                            ReleaseRequestMolasses.Visible = false;
                            Response.Redirect("ReleaseRequestAppliedList");


                        }
                        else if(user.user_id == "Admin")
                        {
                            var list = from s in rr
                                       orderby Convert.ToDateTime(s.rr_date) descending
                                       select s;

                            grdMolassesReleaseRequest.DataSource = list.ToList();
                            grdMolassesReleaseRequest.DataBind();

                            foreach (GridViewRow dr in grdMolassesReleaseRequest.Rows)
                            {

                                DateTime adate = Convert.ToDateTime((dr.FindControl("lblValidUpto") as Label).Text);
                                // adate = DateTime.Now.AddDays(-1);
                                LinkButton btn = dr.FindControl("btnNewRequest") as LinkButton;
                          
                                if (DateTime.Now.Date > adate)
                                {
                                    btn.Text = "Request is Expired";
                                 
                                    btn.BackColor = System.Drawing.Color.Red;
                                    btn.Enabled = false;
                                   
                                }
                                if ((dr.FindControl("lblBalanceQTY") as Label).Text != "")
                                {
                                    if (Convert.ToDouble((dr.FindControl("lblBalanceQTY") as Label).Text) == 0)
                                    {
                                        btn.Text = "Request is Expired";

                                        btn.BackColor = System.Drawing.Color.Red;
                                        btn.Enabled = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            var list = from s in rr
                                       where s.party_code == user.party_code && s.financial_year==user.financial_year
                                       orderby Convert.ToDateTime(s.rr_date) descending
                                       select s;

                            grdMolassesReleaseRequest.DataSource = list.ToList();
                            grdMolassesReleaseRequest.DataBind();

                            foreach (GridViewRow dr in grdMolassesReleaseRequest.Rows)
                            {

                                DateTime adate = Convert.ToDateTime((dr.FindControl("lblValidUpto") as Label).Text);
                                // adate = DateTime.Now.AddDays(-1);
                                LinkButton btn = dr.FindControl("btnNewRequest") as LinkButton;
                                if (DateTime.Now.Date > adate)
                                {
                                    btn.Text = "Request is Expired";
                                    btn.BackColor = System.Drawing.Color.Red;
                                    btn.Enabled = false;

                                }
                                //string a= (dr.FindControl("lblBalanceQTY") as Label).Text;
                                if ((dr.FindControl("lblBalanceQTY") as Label).Text != "")
                                {
                                    if (Convert.ToDouble((dr.FindControl("lblBalanceQTY") as Label).Text) == 0)
                                    {
                                        btn.Text = "Request is Expired";

                                        btn.BackColor = System.Drawing.Color.Red;
                                        btn.Enabled = false;
                                    }
                                }
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
                if (strPreviousPage == "" || System.Web.HttpContext.Current.Session["UserID"] == null)
                {
                    Response.Redirect("~/LoginPage");
                }
            }
        }
        protected void AddRecord_Click(object sender, EventArgs e)
        {
           
        }
        protected void btnReleaseRequestMolasses_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ReleaseRequestList");
        }
        protected void btnIssuedReleaseRequestLetter_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ReleaseRequestAppliedList");
        }
        protected void btnNewRequest_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            int rowindex = gvr.RowIndex;
            string AllotmentNo = (grdMolassesReleaseRequest.Rows[rowindex].FindControl("lblAllotmentNo") as Label).Text;
            string Product = (grdMolassesReleaseRequest.Rows[rowindex].FindControl("lblproduct_code") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancial_year") as Label).Text;
            Session["rrfinancial_year"] = financial_year;
            Session["UserID"] = Session["UserID"].ToString();
            Session["AllotmentNo"] = AllotmentNo;
            Session["product_code"] = Product;
            Session["rtype"] = 0;
            Response.Redirect("ReleaseRequestForm");
        }

        protected void PassRequest_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RequestForPassList");
        }

        protected void grdMolassesReleaseRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["UserID"] != null)
            {
                if (e.NewPageIndex < 0)
                {
                    grdMolassesReleaseRequest.PageIndex = 0;
                }
                else
                {
                    grdMolassesReleaseRequest.PageIndex = e.NewPageIndex;
                }
               
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                if (user != null)
                {
                    GridViewRow row = grdMolassesReleaseRequest.TopPagerRow;
                    TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                    DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");

                    if (Session["rlessearch"] != null && Session["rlestext"] != null)
                    {
                        ddsearch.SelectedValue = Session["rlessearch"].ToString();
                        txtpage.Text = Session["rlestext"].ToString();
                        if (ddsearch.SelectedValue != "Select")
                        {

                            if (txtpage != null)
                            {
                                if (ddsearch.SelectedValue == "req_allotmentdate")
                                {
                                    if (txtpage.Text.ToString().Length == 10)
                                    {
                                        rr = BL_Release_Request.GetList();
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                        txtpage.Focus();
                                        txtpage.Text = "";
                                        ddsearch.SelectedIndex = 0;
                                        rr = BL_Release_Request.GetList();
                                    }
                                }
                                else if (ddsearch.SelectedValue == "rr_balance_qty")
                                {
                                    if (txtpage.Text.ToString() != " ")
                                    {
                                        rr = BL_Release_Request.GetList();
                                    }
                                    else
                                    {
                                        txtpage.Focus();
                                        txtpage.Text = "";
                                        ddsearch.SelectedIndex = 0;
                                    }
                                }
                                else
                                {
                                    rr = new List<Release_Request>();
                                    rr = BL_Release_Request.Search("", ddsearch.SelectedValue, txtpage.Text);
                                }

                            }
                        }
                    }
                    else
                    { 
                        rr = new List<Release_Request>();
                    rr = BL_Release_Request.GetList();
                }

                    if (user.role_name == "Assistant Commissioner")
                    {
                        if (ddsearch.SelectedValue == "valid_date")
                        {
                            var list = from s in rr
                                       where s.district_code == user.district_code && s.valid_date == txtpage.Text
                                       select s;

                            grdMolassesReleaseRequest.DataSource = list.ToList();
                            grdMolassesReleaseRequest.DataBind();
                        }
                        else if (ddsearch.SelectedValue == "rr_balance_qty")
                        {
                            if (txtpage.Text != "")
                            {
                                var list = from s in rr
                                           where s.party_code == user.party_code && s.rr_balance_qty == Convert.ToDouble(txtpage.Text)
                                           orderby Convert.ToDateTime(s.rr_date) descending
                                           select s;

                                grdMolassesReleaseRequest.DataSource = list.ToList();
                                grdMolassesReleaseRequest.DataBind();
                            }

                        }

                        else
                        {
                            var list = from s in rr
                                       where s.district_code == user.district_code
                                       select s;

                            grdMolassesReleaseRequest.DataSource = list.ToList();
                            grdMolassesReleaseRequest.DataBind();
                        }
                        
                        grdMolassesReleaseRequest.Columns[grdMolassesReleaseRequest.Columns.Count - 1].Visible = false;

                    }
                    else if (user.user_id == "Admin")
                    {
                        if (ddsearch.SelectedValue == "valid_date")
                        {
                            var list = from s in rr
                                       where  s.valid_date == txtpage.Text
                                       orderby Convert.ToDateTime(s.rr_date) descending
                                       select s;

                            grdMolassesReleaseRequest.DataSource = list.ToList();
                            grdMolassesReleaseRequest.DataBind();
                        }
                        else if (ddsearch.SelectedValue == "rr_balance_qty")
                        {
                            if (txtpage.Text != "")
                            {
                                var list = from s in rr
                                           where s.rr_balance_qty == Convert.ToDouble(txtpage.Text)
                                           orderby Convert.ToDateTime(s.rr_date) descending
                                           select s;

                                grdMolassesReleaseRequest.DataSource = list.ToList();
                                grdMolassesReleaseRequest.DataBind();
                            }

                        }
                        else
                        {
                            var list = from s in rr
                                       
                                       orderby Convert.ToDateTime(s.rr_date) descending
                                       select s;

                            grdMolassesReleaseRequest.DataSource = list.ToList();
                            grdMolassesReleaseRequest.DataBind();
                        }

                        foreach (GridViewRow dr in grdMolassesReleaseRequest.Rows)
                        {

                            DateTime adate = Convert.ToDateTime((dr.FindControl("lblValidUpto") as Label).Text);
                            // adate = DateTime.Now.AddDays(-1);
                            LinkButton btn = dr.FindControl("btnNewRequest") as LinkButton;
                            if (DateTime.Now.Date > adate)
                            {
                                btn.Text = "Request is Expired";
                               
                                btn.BackColor = System.Drawing.Color.Red;
                                btn.Enabled = false;
                              
                            }
                            if ((dr.FindControl("lblBalanceQTY") as Label).Text != "")
                            {
                                if (Convert.ToDouble((dr.FindControl("lblBalanceQTY") as Label).Text) == 0)
                                {
                                    btn.Text = "Request is Expired";

                                    btn.BackColor = System.Drawing.Color.Red;
                                    btn.Enabled = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (ddsearch.SelectedValue == "valid_date")
                        {
                            var list = from s in rr
                                       where s.party_code == user.party_code && s.valid_date == txtpage.Text
                                       orderby Convert.ToDateTime(s.rr_date) descending
                                       select s;

                            grdMolassesReleaseRequest.DataSource = list.ToList();
                            grdMolassesReleaseRequest.DataBind();
                        }
                        else if (ddsearch.SelectedValue == "rr_balance_qty")
                        {
                            if (txtpage.Text != "")
                            {
                                var list = from s in rr
                                           where s.party_code == user.party_code && s.rr_balance_qty == Convert.ToDouble(txtpage.Text)
                                           orderby Convert.ToDateTime(s.rr_date) descending
                                           select s;

                                grdMolassesReleaseRequest.DataSource = list.ToList();
                                grdMolassesReleaseRequest.DataBind();
                            }

                        }
                        else
                        {
                            var list = from s in rr
                                       where s.party_code == user.party_code
                                       orderby Convert.ToDateTime(s.rr_date) descending
                                       select s;

                            grdMolassesReleaseRequest.DataSource = list.ToList();
                            grdMolassesReleaseRequest.DataBind();
                        }
                        foreach (GridViewRow dr in grdMolassesReleaseRequest.Rows)
                        {

                            DateTime adate = Convert.ToDateTime((dr.FindControl("lblValidUpto") as Label).Text);
                            LinkButton btn = dr.FindControl("btnNewRequest") as LinkButton;
                            if (adate <= DateTime.Now)
                            {
                                btn.Text = "Request is Expired";
                                btn.BackColor = System.Drawing.Color.Red;
                                btn.Enabled = false;
                              
                            }
                            if ((dr.FindControl("lblBalanceQTY") as Label).Text != "")
                            {
                                if (Convert.ToDouble((dr.FindControl("lblBalanceQTY") as Label).Text) == 0)
                                {
                                    btn.Text = "Request is Expired";

                                    btn.BackColor = System.Drawing.Color.Red;
                                    btn.Enabled = false;
                                }
                            }
                        }
                    }
                }
            }
        }
        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdMolassesReleaseRequest.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            if (Convert.ToInt32(txtpage.Text) <=0)
            {
                a = 0;
            }
            else
            {
                a = Convert.ToInt32(txtpage.Text);
            }
            if (a != 0)
            {
                grdMolassesReleaseRequest.PageIndex = a - 1;
            }
            else
            {
                grdMolassesReleaseRequest.PageIndex = a;
            }
            
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            rr = new List<Release_Request>();
            rr = BL_Release_Request.GetList();
            if (user.party_type == "M & tP")
            {
                molasses.Visible = false;
                ENA.Visible = true;
                MTB.Visible = false;
                ETB.Visible = true;
            }
            else
            {
                molasses.Visible = true;
                ENA.Visible = false;
                MTB.Visible = true;
                ETB.Visible = false;

            }
            string userid = Session["UserID"].ToString();
            if (user.role_name.Trim() == "Assistant Commissioner")
            {
                //var list = from s in rr
                //           where s.district_code == user.district_code
                //           select s;
                //grdMolassesReleaseRequest.DataSource = list.ToList();
                //grdMolassesReleaseRequest.DataBind();
                //grdMolassesReleaseRequest.Columns[grdMolassesReleaseRequest.Columns.Count - 1].Visible = false;
                Session["UserID"] = Session["UserID"];
                ReleaseRequestMolasses.Visible = false;
                Response.Redirect("ReleaseRequestAppliedList");


            }
            else
            {
                var list = from s in rr
                           where s.party_code == user.party_code
                           orderby Convert.ToDateTime(s.rr_date) descending
                           select s;

                grdMolassesReleaseRequest.DataSource = list.ToList();
                grdMolassesReleaseRequest.DataBind();

                foreach (GridViewRow dr in grdMolassesReleaseRequest.Rows)
                {

                    DateTime adate = Convert.ToDateTime((dr.FindControl("lblValidUpto") as Label).Text);
                    // adate = DateTime.Now.AddDays(-1);
                    LinkButton btn = dr.FindControl("btnNewRequest") as LinkButton;
                    if (DateTime.Now.Date > adate)
                    {
                        btn.Text = "Request is Expired";
                        btn.BackColor = System.Drawing.Color.Red;
                        btn.Enabled = false;
                    }
                    if ((dr.FindControl("lblBalanceQTY") as Label).Text != "")
                    {
                        if (Convert.ToDouble((dr.FindControl("lblBalanceQTY") as Label).Text) == 0)
                        {
                            btn.Text = "Request is Expired";

                            btn.BackColor = System.Drawing.Color.Red;
                            btn.Enabled = false;
                        }
                    }
                }

            }


        }

        protected void grdMolassesReleaseRequest_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdMolassesReleaseRequest.TopPagerRow;
            if (grdMolassesReleaseRequest.PageCount != 0)
            {
                grdMolassesReleaseRequest.TopPagerRow.Visible = true;
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

            if (Session["rlessearch"] != null && Session["rlestext"] != null)
            {
                ddsearch.SelectedValue = Session["rlessearch"].ToString();
                txtpages.Text = Session["rlestext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdMolassesReleaseRequest.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdMolassesReleaseRequest.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdMolassesReleaseRequest.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdMolassesReleaseRequest.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdMolassesReleaseRequest.PageIndex == 0)
            {
                ((ImageButton)grdMolassesReleaseRequest.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdMolassesReleaseRequest.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdMolassesReleaseRequest.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdMolassesReleaseRequest.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdMolassesReleaseRequest.PageIndex + 1 == grdMolassesReleaseRequest.PageCount)
            {
                ((ImageButton)grdMolassesReleaseRequest.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdMolassesReleaseRequest.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdMolassesReleaseRequest.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdMolassesReleaseRequest.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GridViewRow row = grdMolassesReleaseRequest.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {
                Session["rlessearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["rlestext"] = txtpage.Text;
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        if (ddsearch.SelectedValue == "valid_date")
                        {
                            if (txtpage.Text.ToString().Length == 10)
                            {


                                rr = BL_Release_Request.GetList();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                txtpage.Focus();
                                txtpage.Text = "";
                                ddsearch.SelectedIndex = 0;

                                rr = BL_Release_Request.GetList();
                            }
                        }
                        else if(ddsearch.SelectedValue == "rr_balance_qty")
                        {
                            if (txtpage.Text.ToString() != " ")
                            {
                                rr = BL_Release_Request.GetList();
                            }
                            else
                            {
                                txtpage.Focus();
                                txtpage.Text = "";
                                ddsearch.SelectedIndex = 0;
                            }
                            }
                        else
                        {
                            rr = new List<Release_Request>();
                            rr = BL_Release_Request.Search("", ddsearch.SelectedValue, txtpage.Text);
                        }
                        if (user.role_name.Trim() == "Assistant Commissioner")
                        {
                            Session["UserID"] = Session["UserID"];
                            ReleaseRequestMolasses.Visible = false;
                            Response.Redirect("ReleaseRequestAppliedList");
                        }

                        else if (user.user_id == "Admin")
                        {
                            if (ddsearch.SelectedValue == "valid_date")
                            {
                                var list = from s in rr
                                           where s.valid_date == txtpage.Text
                                           orderby Convert.ToDateTime(s.rr_date) descending
                                           select s;

                                grdMolassesReleaseRequest.DataSource = list.ToList();
                                grdMolassesReleaseRequest.DataBind();
                            }
                            else if (ddsearch.SelectedValue == "rr_balance_qty")
                            {
                                if (txtpage.Text != "")
                                {
                                    var list = from s in rr
                                               where s.rr_balance_qty == Convert.ToDouble(txtpage.Text)
                                               orderby Convert.ToDateTime(s.rr_date) descending
                                               select s;

                                    grdMolassesReleaseRequest.DataSource = list.ToList();
                                    grdMolassesReleaseRequest.DataBind();
                                }

                            }
                            else
                            {
                                var list = from s in rr
                                        
                                           orderby Convert.ToDateTime(s.rr_date) descending
                                           select s;

                                grdMolassesReleaseRequest.DataSource = list.ToList();
                                grdMolassesReleaseRequest.DataBind();
                            }

                            foreach (GridViewRow dr in grdMolassesReleaseRequest.Rows)
                            {

                                DateTime adate = Convert.ToDateTime((dr.FindControl("lblValidUpto") as Label).Text);
                                // adate = DateTime.Now.AddDays(-1);
                                LinkButton btn = dr.FindControl("btnNewRequest") as LinkButton;
                                if (DateTime.Now.Date > adate)
                                {
                                    btn.Text = "Request is Expired";
                                 
                                    btn.BackColor = System.Drawing.Color.Red;
                                    btn.Enabled = false;
                                   
                                }
                                if ((dr.FindControl("lblBalanceQTY") as Label).Text != "")
                                {
                                    if (Convert.ToDouble((dr.FindControl("lblBalanceQTY") as Label).Text) == 0)
                                    {
                                        btn.Text = "Request is Expired";

                                        btn.BackColor = System.Drawing.Color.Red;
                                        btn.Enabled = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (ddsearch.SelectedValue == "valid_date")
                            {
                                var list = from s in rr
                                           where s.party_code == user.party_code && s.valid_date == txtpage.Text
                                           orderby Convert.ToDateTime(s.rr_date) descending
                                           select s;

                                grdMolassesReleaseRequest.DataSource = list.ToList();
                                grdMolassesReleaseRequest.DataBind();
                            }
                            else if (ddsearch.SelectedValue == "rr_balance_qty")
                            {
                                if(txtpage.Text!="")
                                {
                                     var list = from s in rr
                                           where s.party_code == user.party_code && s.rr_balance_qty == Convert.ToDouble(txtpage.Text)
                                           orderby Convert.ToDateTime(s.rr_date) descending
                                           select s;

                                grdMolassesReleaseRequest.DataSource = list.ToList();
                                grdMolassesReleaseRequest.DataBind();
                                }
                               
                            }
                            else
                            {
                                var list = from s in rr
                                           where s.party_code == user.party_code
                                           orderby Convert.ToDateTime(s.rr_date) descending
                                           select s;

                                grdMolassesReleaseRequest.DataSource = list.ToList();
                                grdMolassesReleaseRequest.DataBind();
                            }
                               

                            foreach (GridViewRow dr in grdMolassesReleaseRequest.Rows)
                            {

                                DateTime adate = Convert.ToDateTime((dr.FindControl("lblValidUpto") as Label).Text);
                                // adate = DateTime.Now.AddDays(-1);
                                LinkButton btn = dr.FindControl("btnNewRequest") as LinkButton;
                                if (DateTime.Now.Date > adate)
                                {
                                    btn.Text = "Request is Expired";
                                    btn.BackColor = System.Drawing.Color.Red;
                                    btn.Enabled = false;
                                   
                                }
                                if ((dr.FindControl("lblBalanceQTY") as Label).Text != "")
                                {
                                    if (Convert.ToDouble((dr.FindControl("lblBalanceQTY") as Label).Text) == 0)
                                    {
                                        btn.Text = "Request is Expired";

                                        btn.BackColor = System.Drawing.Color.Red;
                                        btn.Enabled = false;
                                    }
                                }
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

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["rlessearch"] = null;
            Session["rlestext"] = null;
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            Session["party"] = user.party_code;
            if (user != null)
            {
                rr = new List<Release_Request>();
                rr = BL_Release_Request.GetList();
                if (user.party_type == "M & tP")
                {
                    molasses.Visible = false;
                    ENA.Visible = true;
                    MTB.Visible = false;
                    ETB.Visible = true;
                }
                else
                {
                    molasses.Visible = true;
                    ENA.Visible = false;
                    MTB.Visible = true;
                    ETB.Visible = false;

                }
                grdMolassesReleaseRequest.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                if (user.role_name.Trim() == "Assistant Commissioner")
                {
                    
                    Session["UserID"] = Session["UserID"];
                    ReleaseRequestMolasses.Visible = false;
                    Response.Redirect("ReleaseRequestAppliedList");
                }
                else if (user.user_id == "Admin")
                {
                    var list = from s in rr
                               orderby Convert.ToDateTime(s.rr_date) descending
                               select s;

                    grdMolassesReleaseRequest.DataSource = list.ToList();
                    grdMolassesReleaseRequest.DataBind();

                    foreach (GridViewRow dr in grdMolassesReleaseRequest.Rows)
                    {

                        DateTime adate = Convert.ToDateTime((dr.FindControl("lblValidUpto") as Label).Text);
                        // adate = DateTime.Now.AddDays(-1);
                        LinkButton btn = dr.FindControl("btnNewRequest") as LinkButton;
                        if (DateTime.Now.Date > adate)
                        {
                            btn.Text = "Request is Expired";
                            btn.BackColor = System.Drawing.Color.Red;
                            btn.Enabled = false;
                         
                        }
                        if ((dr.FindControl("lblBalanceQTY") as Label).Text != "")
                        {
                            if (Convert.ToDouble((dr.FindControl("lblBalanceQTY") as Label).Text) == 0)
                            {
                                btn.Text = "Request is Expired";

                                btn.BackColor = System.Drawing.Color.Red;
                                btn.Enabled = false;
                            }
                        }
                    }
                }

                else
                {
                    var list = from s in rr
                               where s.party_code == user.party_code
                               orderby Convert.ToDateTime(s.rr_date) descending
                               select s;

                    grdMolassesReleaseRequest.DataSource = list.ToList();
                    grdMolassesReleaseRequest.DataBind();

                    foreach (GridViewRow dr in grdMolassesReleaseRequest.Rows)
                    {

                        DateTime adate = Convert.ToDateTime((dr.FindControl("lblValidUpto") as Label).Text);
                        // adate = DateTime.Now.AddDays(-1);
                        LinkButton btn = dr.FindControl("btnNewRequest") as LinkButton;
                        if (DateTime.Now.Date > adate)
                        {
                            btn.Text = "Request is Expired";
                            btn.BackColor = System.Drawing.Color.Red;
                            btn.Enabled = false;
                           
                        }
                        if ((dr.FindControl("lblBalanceQTY") as Label).Text != "")
                        {
                            if (Convert.ToDouble((dr.FindControl("lblBalanceQTY") as Label).Text) == 0)
                            {
                                btn.Text = "Request is Expired";

                                btn.BackColor = System.Drawing.Color.Red;
                                btn.Enabled = false;
                            }
                        }
                    }

                }
            }
        }
    }
}