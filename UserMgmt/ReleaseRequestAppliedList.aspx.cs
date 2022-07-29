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
    public partial class IssuedReleaseRequestLetterList : System.Web.UI.Page
    {
        // UserDetails user = new UserDetails();
        List<Release_Request> rr = new List<Release_Request>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        Session["rolename"] = user.role_name;
                        Session["distcode"] = user.district_code;
                        Session["usertype"] = user.party_type;
                        Session["party_code"] = user.party_code;
                        grdReleaseRequest.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                        if (user.party_type == "M & tP")
                        {
                            //molasses.Visible = false;
                           // ENA.Visible = true;
                            MTB.Visible = false;
                            ETB.Visible = true;
                        }
                        else
                        {
                           // molasses.Visible = true;
                            //ENA.Visible = false;
                            MTB.Visible = true;
                            ETB.Visible = false;

                        }
                        List<Release_Request> rr = new List<Release_Request>();
                        rr = BL_Release_Request.GetRRList();
                        if (user.role_name == "Applicant" && user.party_type == "Distillery Unit")
                        {
                            var list = from s in rr
                                       where s.party_code == user.party_code && s.financial_year==user.financial_year
                                       orderby Convert.ToDateTime(s.rr_date) descending
                                       select s;

                            grdReleaseRequest.DataSource = list.ToList();
                            grdReleaseRequest.DataBind();
                            foreach (GridViewRow dr1 in grdReleaseRequest.Rows)
                            {
                                LinkButton btn = dr1.FindControl("Issued") as LinkButton;
                                btn.Visible = false;
                            }
                        }
                        else if (user.user_id == "Admin")
                        {
                            var list = from s in rr
                                       
                                       orderby Convert.ToDateTime(s.rr_date) descending
                                       select s;

                            grdReleaseRequest.DataSource = list.ToList();
                            grdReleaseRequest.DataBind();
                            foreach (GridViewRow dr in grdReleaseRequest.Rows)
                            {

                          string status = (dr.FindControl("lblStatus") as Label).Text;
                                // adate = DateTime.Now.AddDays(-1);
                                if (status== "Issued" || status == "Approved")
                                {
                                    (dr.FindControl("btnEdit") as LinkButton).Visible = true;
                                }
                            }
                        }
                     
                           else if (user.role_name.Trim() == "Assistant Commissioner" && user.party_type == "All")
                        {

                            ReleaseRequestMolasses.Visible = false;
                            var list1 = from s in rr
                                        where s.suplier_district==user.district_code && s.record_status != "N" && s.record_status != "R"
                                        orderby Convert.ToDateTime(s.rr_date) descending
                                        select s;

                            grdReleaseRequest.DataSource = list1.ToList();
                            grdReleaseRequest.DataBind();
                            foreach (GridViewRow dr1 in grdReleaseRequest.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = false;
                            }


                        }
                    }
                }
                if (strPreviousPage == "" || System.Web.HttpContext.Current.Session["UserID"] == null)
                {
                    Response.Redirect("~/LoginPage");
                }
            }
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

        protected void btnView_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            LinkButton btn = (LinkButton)sender;//lblparty_code
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            int rowindex = gvr.RowIndex;
            string request_id = (grdReleaseRequest.Rows[rowindex].FindControl("lblRRNo") as Label).Text;
            string party_code = (grdReleaseRequest.Rows[rowindex].FindControl("lblparty_code") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancial_year") as Label).Text;
            Session["rrfinancial_year"] = financial_year;
            Session["UserID"] = Session["UserID"].ToString();
            Session["request_id"] = request_id;
            Session["party_code"] = party_code;
            Session["rtype"] = 1;
            Response.Redirect("ReleaseRequestForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            LinkButton btn = (LinkButton)sender;//lblparty_code
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            int rowindex = gvr.RowIndex;
            string request_id = (grdReleaseRequest.Rows[rowindex].FindControl("lblRRNo") as Label).Text;
            string party_code = (grdReleaseRequest.Rows[rowindex].FindControl("lblparty_code") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancial_year") as Label).Text;
            Session["rrfinancial_year"] = financial_year;
            Session["UserID"] = Session["UserID"].ToString();
            Session["request_id"] = request_id;
            Session["party_code"] = party_code;
            Session["rtype"] = 2;
            Response.Redirect("ReleaseRequestForm");
        }

        protected void Issued_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Session["UserID"] = Session["UserID"];
                //user = new UserDetails();
                //user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                LinkButton btn = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)btn.NamingContainer;
                int rowindex = gvr.RowIndex;
                string request_id = (grdReleaseRequest.Rows[rowindex].FindControl("lblRRNo") as Label).Text;
                string financialyear = (grdReleaseRequest.Rows[rowindex].FindControl("lblfinancial_year") as Label).Text;
                Session["UserID"] = Session["UserID"].ToString();
                Session["request_id"] = request_id;
                Session["rtype"] = 2;
                Release_Request rr = new Release_Request();
                rr.release_request_id = request_id;
                rr.approval_status = "Issued";
                rr.record_status = "I";
                rr.approval_status = "Issued by " + Session["rolename"];
                rr.user_id = Session["UserID"].ToString();
                rr.financial_year = financialyear;
                rr.party_code= (grdReleaseRequest.Rows[rowindex].FindControl("lblparty_code") as Label).Text;
                string val = "";
                val = BL_Release_Request.Approve(rr);
                if (val == "0")
                {
                    List<Release_Request> rr1 = new List<Release_Request>();
                    rr1 = BL_Release_Request.GetRRList();
                    var list1 = from s in rr1
                                where s.district_code == Session["distcode"].ToString()
                                select s;
                    grdReleaseRequest.DataSource = list1.ToList();
                    grdReleaseRequest.DataBind();
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("ReleaseRequestAppliedList");
                }
                else
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(val);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                
                Session["ReportId"] ="ReleaseRequest";
                Session["UserID"] = Session["UserID"].ToString();
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('ReportPage' ,'_blank');", true);
            }
        }

        protected void PassRequest_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RequestForPassList");
        }
        protected void btnprint_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string rrno = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblRRNo") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancial_year") as Label).Text;
            Session["rrfinancial_year"] = financial_year;
            List<Reportmaster> reports = new List<Reportmaster>();
            reports = BL_WorkFlow.GetReports(Session["UserID"].ToString());
            Session["ReportId"] = "RR";
            Session["UserID"] = Session["UserID"].ToString();
            Session["RR_no"] = rrno;
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('ReportPage' ,'_blank');", true);
        }

        protected void grdReleaseRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if(System.Web.HttpContext.Current.Session["UserID"] != null)
            {
                if (e.NewPageIndex < 0)
                {
                    grdReleaseRequest.PageIndex = 0;
                }
                else
                {
                    grdReleaseRequest.PageIndex = e.NewPageIndex;
                }
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                GridViewRow row = grdReleaseRequest.TopPagerRow;
                TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");

                if (Session["rlapsearch"] != null && Session["rlapstext"] != null)
                {
                    ddsearch.SelectedValue = Session["rlapsearch"].ToString();
                    txtpage.Text = Session["rlaptext"].ToString();
                    if (ddsearch.SelectedValue != "Select")
                    {

                        if (txtpage != null)
                        {
                            if (ddsearch.SelectedValue == "rr_date")
                            {
                                if (txtpage.Text.ToString().Length == 10)
                                {


                                    rr = BL_Release_Request.GetRRList();
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                    txtpage.Focus();
                                    txtpage.Text = "";
                                    ddsearch.SelectedIndex = 0;

                                    rr = BL_Release_Request.GetRRList();
                                }
                            }
                            else
                            {
                                rr = new List<Release_Request>();
                                rr = BL_Release_Request.Search1("", ddsearch.SelectedValue, txtpage.Text);
                            }

                        }
                    }
                }
                else
                {
                  
                    rr = BL_Release_Request.GetRRList();
                }
                if (user.role_name == "Applicant" && user.party_type == "Distillery Unit")
                {
                    if (ddsearch.SelectedValue == "rr_date")
                    {
                        var list = from s in rr
                                   where s.party_code == user.party_code && s.rr_date == txtpage.Text
                                   orderby Convert.ToDateTime(s.rr_date) descending
                                   select s;

                        grdReleaseRequest.DataSource = list.ToList();
                        grdReleaseRequest.DataBind();
                        foreach (GridViewRow dr1 in grdReleaseRequest.Rows)
                        {
                            LinkButton btn = dr1.FindControl("Issued") as LinkButton;
                            btn.Visible = false;
                        }
                    }
                    else
                    {
                        var list = from s in rr
                                   where s.party_code == user.party_code
                                   orderby Convert.ToDateTime(s.rr_date) descending
                                   select s;

                        grdReleaseRequest.DataSource = list.ToList();
                        grdReleaseRequest.DataBind();
                    }


                }
                else if (user.user_id == "Admin")
                {
                    if (ddsearch.SelectedValue == "rr_date")
                    {
                        var list = from s in rr
                                   where s.rr_date == txtpage.Text
                                   orderby Convert.ToDateTime(s.rr_date) descending
                                   select s;

                        grdReleaseRequest.DataSource = list.ToList();
                        grdReleaseRequest.DataBind();
                    }
                    else
                    {
                        var list = from s in rr

                                   orderby Convert.ToDateTime(s.rr_date) descending
                                   select s;

                        grdReleaseRequest.DataSource = list.ToList();
                        grdReleaseRequest.DataBind();
                    }
                    foreach (GridViewRow dr in grdReleaseRequest.Rows)
                    {

                        string status = (dr.FindControl("lblStatus") as Label).Text;
                        // adate = DateTime.Now.AddDays(-1);
                        if (status == "Issued" || status == "Approved")
                        {
                            (dr.FindControl("btnEdit") as LinkButton).Visible = true;
                        }
                    }
                }
                else if (user.role_name.Trim() == "Assistant Commissioner" && user.party_type == "All")
                {

                    ReleaseRequestMolasses.Visible = false;
                    if (ddsearch.SelectedValue == "rr_date")
                    {
                        var list = from s in rr
                                   where s.suplier_district == user.district_code && s.record_status != "N" && s.record_status != "R" && s.rr_date == txtpage.Text
                                   orderby Convert.ToDateTime(s.rr_date) descending
                                   select s;

                        grdReleaseRequest.DataSource = list.ToList();
                        grdReleaseRequest.DataBind();
                    }
                    else
                    {
                        var list1 = from s in rr
                                    where s.suplier_district == user.district_code && s.record_status != "N" && s.record_status != "R"
                                    orderby Convert.ToDateTime(s.rr_date) descending
                                    select s;

                        grdReleaseRequest.DataSource = list1.ToList();
                        grdReleaseRequest.DataBind();
                    }
                    foreach (GridViewRow dr1 in grdReleaseRequest.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                        btn.Visible = false;
                    }


                }
            }
        }
        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdReleaseRequest.TopPagerRow;
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
                grdReleaseRequest.PageIndex = a - 1;
            }
            else
            {
                grdReleaseRequest.PageIndex = a;
            }


            string userid = Session["UserID"].ToString();
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
             rr = BL_Release_Request.GetRRList();
            if (user.role_name == "Applicant" && user.party_type == "Distillery Unit")
            {
                var list = from s in rr
                           where s.party_code == user.party_code
                           orderby Convert.ToDateTime(s.rr_date) descending
                           select s;

                grdReleaseRequest.DataSource = list.ToList();
                grdReleaseRequest.DataBind();
                foreach (GridViewRow dr1 in grdReleaseRequest.Rows)
                {
                    LinkButton btn = dr1.FindControl("Issued") as LinkButton;
                    btn.Visible = false;
                }

            }
            if (user.role_name.Trim() == "Assistant Commissioner" && user.party_type == "All")
            {

                ReleaseRequestMolasses.Visible = false;
                var list1 = from s in rr
                            where s.suplier_district == user.district_code && s.record_status != "N" && s.record_status != "R"
                            orderby Convert.ToDateTime(s.rr_date) descending
                            select s;

                grdReleaseRequest.DataSource = list1.ToList();
                grdReleaseRequest.DataBind();
                foreach (GridViewRow dr1 in grdReleaseRequest.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    btn.Visible = false;
                }


            }



        }

        protected void grdReleaseRequest_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdReleaseRequest.TopPagerRow;
            if (grdReleaseRequest.PageCount != 0)
            {
                grdReleaseRequest.TopPagerRow.Visible = true;
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

            if (Session["rlapsearch"] != null && Session["rlaptext"] != null)
            {
                ddsearch.SelectedValue = Session["rlapsearch"].ToString();
                txtpages.Text = Session["rlaptext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdReleaseRequest.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdReleaseRequest.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdReleaseRequest.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdReleaseRequest.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdReleaseRequest.PageIndex == 0)
            {
                ((ImageButton)grdReleaseRequest.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdReleaseRequest.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdReleaseRequest.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdReleaseRequest.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdReleaseRequest.PageIndex + 1 == grdReleaseRequest.PageCount)
            {
                ((ImageButton)grdReleaseRequest.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdReleaseRequest.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdReleaseRequest.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdReleaseRequest.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }

        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GridViewRow row = grdReleaseRequest.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {
                Session["rlapsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["rlaptext"] = txtpage.Text;
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        if (ddsearch.SelectedValue == "rr_date")
                        {
                            if (txtpage.Text.ToString().Length == 10)
                            {


                                rr = BL_Release_Request.GetRRList();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                txtpage.Focus();
                                txtpage.Text = "";
                                ddsearch.SelectedIndex = 0;

                                rr = BL_Release_Request.GetRRList();
                            }
                        }
                        else
                        {
                            rr = BL_Release_Request.Search1("", ddsearch.SelectedValue, txtpage.Text);
                        }
                        if (user.role_name == "Applicant" && user.party_type == "Distillery Unit")
                        {
                            if (ddsearch.SelectedValue == "rr_date")
                            {
                                var list = from s in rr
                                           where s.party_code == user.party_code && s.rr_date==txtpage.Text
                                           orderby Convert.ToDateTime(s.rr_date) descending
                                           select s;

                                grdReleaseRequest.DataSource = list.ToList();
                                grdReleaseRequest.DataBind();
                            }
                            else
                            {
                                var list = from s in rr
                                           where s.party_code == user.party_code
                                           orderby Convert.ToDateTime(s.rr_date) descending
                                           select s;

                                grdReleaseRequest.DataSource = list.ToList();
                                grdReleaseRequest.DataBind();
                            }
                            foreach (GridViewRow dr1 in grdReleaseRequest.Rows)
                            {
                                LinkButton btn = dr1.FindControl("Issued") as LinkButton;
                                btn.Visible = false;
                            }

                        }
                        else if (user.user_id == "Admin")
                        {
                            if (ddsearch.SelectedValue == "rr_date")
                            {
                                var list = from s in rr
                                           where s.rr_date == txtpage.Text
                                           orderby Convert.ToDateTime(s.rr_date) descending
                                           select s;

                                grdReleaseRequest.DataSource = list.ToList();
                                grdReleaseRequest.DataBind();
                            }
                            else
                            {
                                var list = from s in rr

                                           orderby Convert.ToDateTime(s.rr_date) descending
                                           select s;

                                grdReleaseRequest.DataSource = list.ToList();
                                grdReleaseRequest.DataBind();
                            }
                            foreach (GridViewRow dr in grdReleaseRequest.Rows)
                            {

                                string status = (dr.FindControl("lblStatus") as Label).Text;
                                // adate = DateTime.Now.AddDays(-1);
                                if (status == "Issued" || status == "Approved")
                                {
                                    (dr.FindControl("btnEdit") as LinkButton).Visible = true;
                                }
                            }
                        }
                      else  if (user.role_name.Trim() == "Assistant Commissioner" && user.party_type == "All")
                        {

                            ReleaseRequestMolasses.Visible = false;
                            if (ddsearch.SelectedValue == "rr_date")
                            {
                                var list = from s in rr
                                           where s.suplier_district == user.district_code && s.record_status != "N" && s.record_status != "R" && s.rr_date == txtpage.Text
                                           orderby Convert.ToDateTime(s.rr_date) descending
                                           select s;

                                grdReleaseRequest.DataSource = list.ToList();
                                grdReleaseRequest.DataBind();
                            }
                            else
                            {
                                var list1 = from s in rr
                                            where s.suplier_district == user.district_code && s.record_status != "N" && s.record_status != "R"
                                            orderby Convert.ToDateTime(s.rr_date) descending
                                            select s;

                                grdReleaseRequest.DataSource = list1.ToList();
                                grdReleaseRequest.DataBind();
                            }
                            foreach (GridViewRow dr1 in grdReleaseRequest.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                                btn.Visible = false;
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
            Session["rlapsearch"] = null;
            Session["rlapstext"] = null;
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            rr = BL_Release_Request.GetRRList();
            if (user.role_name == "Applicant" && user.party_type == "Distillery Unit")
            {
                var list = from s in rr
                           where s.party_code == user.party_code
                           orderby Convert.ToDateTime(s.rr_date) descending
                           select s;

                grdReleaseRequest.DataSource = list.ToList();
                grdReleaseRequest.DataBind();
                foreach (GridViewRow dr1 in grdReleaseRequest.Rows)
                {
                    LinkButton btn = dr1.FindControl("Issued") as LinkButton;
                    btn.Visible = false;
                }
            }
            else if (user.user_id == "Admin")
            {
                
                    var list = from s in rr

                               orderby Convert.ToDateTime(s.rr_date) descending
                               select s;

                    grdReleaseRequest.DataSource = list.ToList();
                    grdReleaseRequest.DataBind();
                foreach (GridViewRow dr in grdReleaseRequest.Rows)
                {

                    string status = (dr.FindControl("lblStatus") as Label).Text;
                    // adate = DateTime.Now.AddDays(-1);
                    if (status == "Issued" || status == "Approved")
                    {
                        (dr.FindControl("btnEdit") as LinkButton).Visible = true;
                    }
                }
            }
           else if (user.role_name.Trim() == "Assistant Commissioner" && user.party_type == "All")
            {

                ReleaseRequestMolasses.Visible = false;
                var list1 = from s in rr
                            where s.suplier_district == user.district_code && s.record_status != "N" && s.record_status != "R"
                            orderby Convert.ToDateTime(s.rr_date) descending
                            select s;

                grdReleaseRequest.DataSource = list1.ToList();
                grdReleaseRequest.DataBind();
                foreach (GridViewRow dr1 in grdReleaseRequest.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    btn.Visible = false;
                }


            }

        }
    }
}