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
    public partial class PassList : System.Web.UI.Page
    {
        List<Pass_Details> pass = new List<Pass_Details>();
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
                        Session["party_code"] = user.party_code;
                        Session["rolename"] = user.role_name;
                        Session["usertype"] = user.party_type;
                        ddPassFor.SelectedValue = "RR";
                        ddPassFor_SelectedIndexChanged(sender, null);
                        //else
                        //{
                        //    Response.Redirect("~/User_Mgmt");
                        //}
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


        protected void AddRecord_Click(object sender, EventArgs e)
        {
            //if (ddPassFor.Text != "Select")
            //{
            //    if (ddPassFor.SelectedItem.ToString() == "NOC")
            //    {
            //        ddPassFor.Text = "NOC";
            //        Response.Redirect("NOCPass.aspx");
            //    }
            //    if (ddPassFor.SelectedItem.ToString() == "Molasses")
            //    {
            //        ddPassFor.Text = "Molasses";
            //        Response.Redirect("PassForm.aspx");
            //    }
            //}
            //else
            //{
            //    string message = "Select Pass For.";
            //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //    sb.Append("<script type = 'text/javascript'>");
            //    sb.Append("window.onload=function(){");
            //    sb.Append("alert('");
            //    sb.Append(message);
            //    sb.Append("')};");
            //    sb.Append("</script>");
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
            //}
        }


        protected void btnApplyForPass_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = 0;
            Response.Redirect("PassList.aspx");

        }
        protected void btnRequestForPass_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RequestForPassList");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string passtype = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpass_type") as Label).Text;
            string requestid= (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrrnoc_record_request_id") as Label).Text;
            string PassNo = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblPassNo") as Label).Text;
            string issnued = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrrnoc_record_request_id1") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["Pfinancial_year"] = financial_year;
            Session["ptype"] = passtype;
            Session["PassRequestID"] = requestid;
            Session["issued"] = issnued;
            Session["pass_id"] = PassNo;
            Session["rtype"] = "1";
            Response.Redirect("PassForm");
           
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string passtype = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpass_type") as Label).Text;
            string requestid = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrrnoc_record_request_id") as Label).Text;
            string PassNo = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblPassNo") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["Pfinancial_year"] = financial_year;
            Session["ptype"] = passtype;
            Session["PassRequestID"] = requestid;
            Session["pass_id"] = PassNo;
            Session["rtype"] = "2";
            Response.Redirect("PassForm");
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string passtype = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpass_type") as Label).Text;
            string requestid = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrrnoc_record_request_id") as Label).Text;
            string PassNo = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblPassNo") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["Pfinancial_year"] = financial_year;
            Session["ptype"] = passtype;
            Session["PassRequestID"] = requestid;
            Session["pass_id"] = PassNo;
            Session["rtype"] = "3";
            Response.Redirect("PassForm");
        }
      
        protected void grdPassApplicationList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPassApplicationList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
            if (e.NewPageIndex < 0)
            {
                grdPassApplicationList.PageIndex = 0;
            }
            else
            {
                grdPassApplicationList.PageIndex = e.NewPageIndex;
            }
            if (System.Web.HttpContext.Current.Session["UserID"] != null && ddPassFor.SelectedValue != "Select")
            {
                GridViewRow row = grdPassApplicationList.TopPagerRow;
                TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                if (Session["passsearch"] != null && Session["passtext"] != null)
                {
                    ddsearch.SelectedValue = Session["passsearch"].ToString();
                    txtpage.Text = Session["passtext"].ToString();
                    if (ddsearch.SelectedValue != "Select")
                    {

                        if (txtpage != null)
                        {
                            if (ddsearch.SelectedValue == "dispatch_date")
                            {
                                if (txtpage.Text.ToString().Length == 10)
                                {

                                    pass = BL_Pass_Details.GetPassList();
                                }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                txtpage.Focus();
                                txtpage.Text = "";
                                ddsearch.SelectedIndex = 0;
                                pass = BL_Pass_Details.GetPassList();
                            }

                        }
                        else
                        {
                            pass = BL_Pass_Details.Search(ddPassFor.SelectedValue, ddsearch.SelectedValue, txtpage.Text);
                        }
                    }
                    }
                        }
                   
                
                else
                {
                pass = BL_Pass_Details.GetPassList();
                }

                grdPassApplicationList.DataSource = null;
                grdPassApplicationList.DataBind();
                if (Session["rolename"].ToString() == "Bond Officer")
                    if (Session["rolename"].ToString() == "Bond Officer")
                    {
                        btnRequestForPass.Visible = false;
                    }

                if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Sugar Mill")
                {
                    if (ddsearch.SelectedValue == "dispatch_date")
                    {
                        var list = (from s in pass
                                    where s.from_party == Session["party_code"].ToString() && s.pass_type == ddPassFor.SelectedValue && s.dispatch_date == txtpage.Text && s.financial_year == user.financial_year
                                    orderby s.pass_reqno descending
                                    select s);
                        grdPassApplicationList.DataSource = list.ToList();
                        grdPassApplicationList.DataBind();
                    }
                    else
                    {
                        var list = (from s in pass
                                    where s.from_party == Session["party_code"].ToString() && s.pass_type == ddPassFor.SelectedValue && s.financial_year == user.financial_year
                                    orderby s.pass_reqno descending
                                    select s);
                        grdPassApplicationList.DataSource = list.ToList();
                        grdPassApplicationList.DataBind();
                    }


                    foreach (GridViewRow dr1 in grdPassApplicationList.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                        btn.Visible = false;
                    }
                }
                else
                    if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Distillery Unit")
                {
                    if (ddsearch.SelectedValue == "dispatch_date")
                    {
                        var list = (from s in pass
                                    where s.party_code == Session["party_code"].ToString() && s.pass_type == ddPassFor.SelectedValue && s.dispatch_date == txtpage.Text && s.financial_year == user.financial_year
                                    orderby s.pass_reqno descending
                                    select s);
                        grdPassApplicationList.DataSource = list.ToList();
                        grdPassApplicationList.DataBind();
                    }
                    else
                    {
                        var list = (from s in pass
                                    where s.party_code == Session["party_code"].ToString() && s.pass_type == ddPassFor.SelectedValue && s.financial_year == user.financial_year
                                    orderby s.pass_reqno descending
                                    select s);
                        grdPassApplicationList.DataSource = list.ToList();
                        grdPassApplicationList.DataBind();
                    }

                    foreach (GridViewRow dr1 in grdPassApplicationList.Rows)
                    {
                        Label lbl = dr1.FindControl("lblpass_type") as Label;
                        if (lbl.Text == "RR")
                        {
                            LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                            btn1.Visible = false;
                        }
                        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                        btn.Visible = false;
                    }
                }
                else if (Session["rolename"].ToString() == "Bond Officer" && Session["usertype"].ToString() == "Distillery Unit")
                {
                    if (ddsearch.SelectedValue == "dispatch_date")
                    {
                        var list = (from s in pass
                                    where s.party_code == Session["party_code"].ToString() && (s.record_status == "D" || s.record_status == "Y" || s.record_status == "I" || s.record_status == "R") && s.pass_type == ddPassFor.SelectedValue && s.dispatch_date == txtpage.Text && s.financial_year == user.financial_year
                                    orderby s.pass_reqno descending
                                    select s);
                        grdPassApplicationList.DataSource = list.ToList();
                        grdPassApplicationList.DataBind();
                    }
                    else
                    {
                        var list = (from s in pass
                                    where s.party_code == Session["party_code"].ToString() && (s.record_status == "D" || s.record_status == "Y" || s.record_status == "I" || s.record_status == "R") && s.pass_type == ddPassFor.SelectedValue && s.financial_year == user.financial_year
                                    orderby s.pass_reqno descending
                                    select s);
                        grdPassApplicationList.DataSource = list.ToList();
                        grdPassApplicationList.DataBind();
                    }
                }
                else if (Session["rolename"].ToString() == "Bond Officer" && Session["usertype"].ToString() == "Sugar Mill")
                {
                    if (ddsearch.SelectedValue == "dispatch_date")
                    {
                        var list = (from s in pass
                                    where s.from_party == Session["party_code"].ToString() && (s.record_status == "Y" || s.record_status == "I" || s.record_status == "R") && s.pass_type == ddPassFor.SelectedValue && s.dispatch_date == txtpage.Text && s.financial_year == user.financial_year
                                    orderby s.pass_reqno descending
                                    select s);
                        grdPassApplicationList.DataSource = list.ToList();
                        grdPassApplicationList.DataBind();
                    }
                    else
                    {
                        var list = (from s in pass
                                    where s.from_party == Session["party_code"].ToString() && (s.record_status == "Y" || s.record_status == "I" || s.record_status == "R") && s.pass_type == ddPassFor.SelectedValue && s.financial_year == user.financial_year
                                    orderby s.pass_reqno descending
                                    select s);
                        grdPassApplicationList.DataSource = list.ToList();
                        grdPassApplicationList.DataBind();
                    }
                }
                else if (Session["UserID"].ToString() == "Admin")
                {
                    if (ddsearch.SelectedValue == "dispatch_date")
                    {
                        var list = (from s in pass
                                    where s.pass_type == ddPassFor.SelectedValue && s.dispatch_date == txtpage.Text 
                                    orderby s.pass_reqno descending
                                    select s);
                        grdPassApplicationList.DataSource = list.ToList();
                        grdPassApplicationList.DataBind();
                    }
                    else
                    {
                        var list = (from s in pass
                                    where  s.pass_type == ddPassFor.SelectedValue 
                                    orderby s.pass_reqno descending
                                    select s);
                        grdPassApplicationList.DataSource = list.ToList();
                        grdPassApplicationList.DataBind();
                    }

                    foreach (GridViewRow dr1 in grdPassApplicationList.Rows)
                    {
                        Label lbl = dr1.FindControl("lblpass_type") as Label;
                        if (lbl.Text == "RR")
                        {
                            LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                            btn1.Visible = false;
                        }
                        string status = (dr1.FindControl("lblStatus") as Label).Text;

                        if (status == "Issued" || status == "Approved")
                        {
                            (dr1.FindControl("btnEdit") as LinkButton).Visible = true;
                        }
                        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                        btn.Visible = false;
                    }

                }



            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
           
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string PassNo = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblPassNo") as Label).Text;
            string pass_type = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpass_type") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["Pfinancial_year"] = financial_year;
            List<Reportmaster> reports = new List<Reportmaster>();
            reports = BL_WorkFlow.GetReports(Session["UserID"].ToString());
            Session["ReportId"] = "view_pass";
            Session["UserID"] = Session["UserID"].ToString();
            Session["Pass_No"] = PassNo;
            Session["Pass_Type"] = pass_type;
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('ReportPage' ,'_blank');", true);
        }

        protected void ddPassFor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["UserID"] != null && ddPassFor.SelectedValue != "Select")
            {
                List<Pass_Details> pass = new List<Pass_Details>();
                pass = BL_Pass_Details.GetPassList();
                grdPassApplicationList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());

                if (Session["rolename"].ToString() == "Bond Officer")
                    {
                        btnRequestForPass.Visible = false;
                    }

                    if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Sugar Mill")
                    {

                        var list = (from s in pass
                                    where s.from_party == Session["party_code"].ToString() && s.pass_type == ddPassFor.SelectedValue && s.financial_year == user.financial_year
                                    orderby s.pass_reqno descending
                                    select s);
                        grdPassApplicationList.DataSource = list.ToList();
                        grdPassApplicationList.DataBind();
                        foreach (GridViewRow dr1 in grdPassApplicationList.Rows)
                        {
                            LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                            btn.Visible = false;
                        }
                    }
                else if (Session["UserID"].ToString() == "Admin")
                {
                    var list = (from s in pass
                                where s.pass_type == ddPassFor.SelectedValue
                                orderby s.pass_reqno descending
                                select s);
                    grdPassApplicationList.DataSource = list.ToList();
                    grdPassApplicationList.DataBind();

                    foreach (GridViewRow dr1 in grdPassApplicationList.Rows)
                    {
                        Label lbl = dr1.FindControl("lblpass_type") as Label;
                        if (lbl.Text == "RR")
                        {
                            LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                            btn1.Visible = false;
                        }
                        string status = (dr1.FindControl("lblStatus") as Label).Text;

                        if (status == "Issued" || status == "Approved")
                        {
                            (dr1.FindControl("btnEdit") as LinkButton).Visible = true;
                        }
                        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                        btn.Visible = false;
                    }

                }

                else
                        if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Distillery Unit")
                {
                    var list = (from s in pass
                                where s.party_code == Session["party_code"].ToString() && s.pass_type == ddPassFor.SelectedValue && s.financial_year == user.financial_year
                                orderby s.pass_reqno descending
                                select s);
                    grdPassApplicationList.DataSource = list.ToList();
                    grdPassApplicationList.DataBind();

                    foreach (GridViewRow dr1 in grdPassApplicationList.Rows)
                    {
                        Label lbl = dr1.FindControl("lblpass_type") as Label;
                        if (lbl.Text == "RR")
                        {
                            LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                            btn1.Visible = false;
                        }
                        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                        btn.Visible = false;
                    }
                }
                else if (Session["rolename"].ToString() == "Bond Officer" && Session["usertype"].ToString() == "Distillery Unit")
                {
                    var list = (from s in pass
                                where s.party_code == Session["party_code"].ToString() && (s.record_status == "D" || s.record_status == "Y" || s.record_status == "I" || s.record_status == "R") && s.pass_type == ddPassFor.SelectedValue && s.financial_year == user.financial_year
                                orderby s.pass_reqno descending
                                select s);
                    grdPassApplicationList.DataSource = list.ToList();
                    grdPassApplicationList.DataBind();
                }
                else if (Session["rolename"].ToString() == "Bond Officer" && Session["usertype"].ToString() == "Sugar Mill")
                {
                    var list = (from s in pass
                                where s.from_party == Session["party_code"].ToString() && (s.record_status == "Y" || s.record_status == "I" || s.record_status == "R") && s.pass_type == ddPassFor.SelectedValue && s.financial_year == user.financial_year
                                orderby s.pass_reqno descending
                                select s);
                    grdPassApplicationList.DataSource = list.ToList();
                    grdPassApplicationList.DataBind();
                }
               
            }
        }
        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdPassApplicationList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            if (Convert.ToInt32(txtpage.Text) <= 0)
            {
                a = 0;
            }
            else
            {
                a = Convert.ToInt32(txtpage.Text);
            }
            if (a != 0)
            {
                grdPassApplicationList.PageIndex = a - 1;

            }
            else
            {
                grdPassApplicationList.PageIndex = a;
            }

            string userid = Session["UserID"].ToString();
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            List<Pass_Details> pass = new List<Pass_Details>();
            pass = BL_Pass_Details.GetPassList();
            grdPassApplicationList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());


            if (Session["rolename"].ToString() == "Bond Officer")
            {
                btnRequestForPass.Visible = false;
            }

            if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Sugar Mill")
            {

                var list = (from s in pass
                            where s.from_party == Session["party_code"].ToString() && s.pass_type == ddPassFor.SelectedValue && s.financial_year == user.financial_year
                            orderby s.pass_reqno descending
                            select s);
                grdPassApplicationList.DataSource = list.ToList();
                grdPassApplicationList.DataBind();
                foreach (GridViewRow dr1 in grdPassApplicationList.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                    btn.Visible = false;
                }
            }
            else
                if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Distillery Unit")
            {
                var list = (from s in pass
                            where s.party_code == Session["party_code"].ToString() && s.pass_type == ddPassFor.SelectedValue && s.financial_year == user.financial_year
                            orderby s.pass_reqno descending
                            select s);
                grdPassApplicationList.DataSource = list.ToList();
                grdPassApplicationList.DataBind();

                foreach (GridViewRow dr1 in grdPassApplicationList.Rows)
                {
                    Label lbl = dr1.FindControl("lblpass_type") as Label;
                    if (lbl.Text == "RR")
                    {
                        LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                        btn1.Visible = false;
                    }
                    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                    btn.Visible = false;
                }
            }
            else if (Session["rolename"].ToString() == "Bond Officer" && Session["usertype"].ToString() == "Distillery Unit")
            {
                var list = (from s in pass
                            where s.party_code == Session["party_code"].ToString() && (s.record_status == "D" || s.record_status == "Y" || s.record_status == "I" || s.record_status == "R") && s.pass_type == ddPassFor.SelectedValue && s.financial_year == user.financial_year
                            orderby s.pass_reqno descending
                            select s);
                grdPassApplicationList.DataSource = list.ToList();
                grdPassApplicationList.DataBind();
            }
            else if (Session["rolename"].ToString() == "Bond Officer" && Session["usertype"].ToString() == "Sugar Mill")
            {
                var list = (from s in pass
                            where s.from_party == Session["party_code"].ToString() && (s.record_status == "Y" || s.record_status == "I" || s.record_status == "R") && s.pass_type == ddPassFor.SelectedValue && s.financial_year == user.financial_year
                            orderby s.pass_reqno descending
                            select s);
                grdPassApplicationList.DataSource = list.ToList();
                grdPassApplicationList.DataBind();
            }


        }

        protected void grdPassApplicationList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdPassApplicationList.TopPagerRow;
            if (grdPassApplicationList.PageCount != 0)
            {
                grdPassApplicationList.TopPagerRow.Visible = true;
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

            if (Session["passsearch"] != null && Session["passtext"] != null)
            {
                ddsearch.SelectedValue = Session["passsearch"].ToString();
                txtpages.Text = Session["passtext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdPassApplicationList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdPassApplicationList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdPassApplicationList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdPassApplicationList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdPassApplicationList.PageIndex == 0)
            {
                ((ImageButton)grdPassApplicationList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdPassApplicationList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdPassApplicationList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdPassApplicationList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdPassApplicationList.PageIndex + 1 == grdPassApplicationList.PageCount)
            {
                ((ImageButton)grdPassApplicationList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdPassApplicationList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdPassApplicationList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdPassApplicationList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }

        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GridViewRow row = grdPassApplicationList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {
                Session["passsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["passtext"] = txtpage.Text;
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        if (ddsearch.SelectedValue == "dispatch_date")
                        {
                            if (txtpage.Text.ToString().Length == 10)
                            {

                                pass = BL_Pass_Details.GetPassList();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                txtpage.Focus();
                                txtpage.Text = "";
                                ddsearch.SelectedIndex = 0;
                                pass = BL_Pass_Details.GetPassList();
                            }
                        }
                        else
                        {

                            pass = BL_Pass_Details.Search(ddPassFor.SelectedValue, ddsearch.SelectedValue, txtpage.Text);
                        }
                        if (Session["rolename"].ToString() == "Bond Officer")
                        {
                            btnRequestForPass.Visible = false;
                        }

                        if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Sugar Mill")
                        {
                            if (ddsearch.SelectedValue == "dispatch_date")
                            {
                                var list = (from s in pass
                                            where s.from_party == Session["party_code"].ToString() && s.pass_type == ddPassFor.SelectedValue && s.dispatch_date == txtpage.Text&& s.financial_year == user.financial_year
                                            orderby s.pass_reqno descending
                                            select s);
                                grdPassApplicationList.DataSource = list.ToList();
                                grdPassApplicationList.DataBind();
                            }
                            else
                            {
                                var list = (from s in pass
                                            where s.from_party == Session["party_code"].ToString() && s.pass_type == ddPassFor.SelectedValue&& s.financial_year == user.financial_year
                                            orderby s.pass_reqno descending
                                            select s);
                                grdPassApplicationList.DataSource = list.ToList();
                                grdPassApplicationList.DataBind();
                            }

                               
                            foreach (GridViewRow dr1 in grdPassApplicationList.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                btn.Visible = false;
                            }
                        }
                        else
                            if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Distillery Unit")
                        {
                            if (ddsearch.SelectedValue == "dispatch_date")
                            {
                                var list = (from s in pass
                                            where s.party_code == Session["party_code"].ToString() && s.pass_type == ddPassFor.SelectedValue && s.dispatch_date == txtpage.Text && s.financial_year == user.financial_year
                                            orderby s.pass_reqno descending
                                            select s);
                                grdPassApplicationList.DataSource = list.ToList();
                                grdPassApplicationList.DataBind();
                            }
                            else
                            {
                                var list = (from s in pass
                                            where s.party_code == Session["party_code"].ToString() && s.pass_type == ddPassFor.SelectedValue && s.financial_year == user.financial_year
                                            orderby s.pass_reqno descending
                                            select s);
                                grdPassApplicationList.DataSource = list.ToList();
                                grdPassApplicationList.DataBind();
                            }

                            foreach (GridViewRow dr1 in grdPassApplicationList.Rows)
                            {
                                Label lbl = dr1.FindControl("lblpass_type") as Label;
                                if (lbl.Text == "RR")
                                {
                                    LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                                    btn1.Visible = false;
                                }
                                LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                btn.Visible = false;
                            }
                        }
                        else if (Session["rolename"].ToString() == "Bond Officer" && Session["usertype"].ToString() == "Distillery Unit")
                        {
                            if (ddsearch.SelectedValue == "dispatch_date")
                            {
                                var list = (from s in pass
                                            where s.party_code == Session["party_code"].ToString() && (s.record_status == "D" || s.record_status == "Y" || s.record_status == "I" || s.record_status == "R") && s.pass_type == ddPassFor.SelectedValue && s.dispatch_date == txtpage.Text && s.financial_year == user.financial_year
                                            orderby s.pass_reqno descending
                                            select s);
                                grdPassApplicationList.DataSource = list.ToList();
                                grdPassApplicationList.DataBind();
                            }
                            else
                            {
                                var list = (from s in pass
                                            where s.party_code == Session["party_code"].ToString() && (s.record_status == "D" || s.record_status == "Y" || s.record_status == "I" || s.record_status == "R") && s.pass_type == ddPassFor.SelectedValue && s.financial_year == user.financial_year
                                            orderby s.pass_reqno descending
                                            select s);
                                grdPassApplicationList.DataSource = list.ToList();
                                grdPassApplicationList.DataBind();
                            }
                        }
                        else if (Session["rolename"].ToString() == "Bond Officer" && Session["usertype"].ToString() == "Sugar Mill")
                        {
                            if (ddsearch.SelectedValue == "dispatch_date")
                            {
                                var list = (from s in pass
                                            where s.from_party == Session["party_code"].ToString() && (s.record_status == "Y" || s.record_status == "I" || s.record_status == "R") && s.pass_type == ddPassFor.SelectedValue && s.dispatch_date == txtpage.Text && s.financial_year == user.financial_year
                                            orderby s.pass_reqno descending
                                            select s);
                                grdPassApplicationList.DataSource = list.ToList();
                                grdPassApplicationList.DataBind();
                            }
                            else
                            {
                                var list = (from s in pass
                                            where s.from_party == Session["party_code"].ToString() && (s.record_status == "Y" || s.record_status == "I" || s.record_status == "R") && s.pass_type == ddPassFor.SelectedValue && s.financial_year == user.financial_year
                                            orderby s.pass_reqno descending
                                            select s);
                                grdPassApplicationList.DataSource = list.ToList();
                                grdPassApplicationList.DataBind();
                            }
                        }
                        else if (Session["UserID"].ToString() == "Admin")
                        {
                            if (ddsearch.SelectedValue == "dispatch_date")
                            {
                                var list = (from s in pass
                                            where s.pass_type == ddPassFor.SelectedValue && s.dispatch_date == txtpage.Text
                                            orderby s.pass_reqno descending
                                            select s);
                                grdPassApplicationList.DataSource = list.ToList();
                                grdPassApplicationList.DataBind();
                            }
                            else
                            {
                                var list = (from s in pass
                                            where s.pass_type == ddPassFor.SelectedValue
                                            orderby s.pass_reqno descending
                                            select s);
                                grdPassApplicationList.DataSource = list.ToList();
                                grdPassApplicationList.DataBind();
                            }

                            foreach (GridViewRow dr1 in grdPassApplicationList.Rows)
                            {
                                Label lbl = dr1.FindControl("lblpass_type") as Label;
                                if (lbl.Text == "RR")
                                {
                                    LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                                    btn1.Visible = false;
                                }
                                string status = (dr1.FindControl("lblStatus") as Label).Text;

                                if (status == "Issued" || status == "Approved")
                                {
                                    (dr1.FindControl("btnEdit") as LinkButton).Visible = true;
                                }
                                LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
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
            Session["passsearch"] = null;
            Session["passtext"] = null;
            pass = BL_Pass_Details.GetPassList();
            grdPassApplicationList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());

            if (Session["rolename"].ToString() == "Bond Officer")
            {
                btnRequestForPass.Visible = false;
            }

            if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Sugar Mill")
            {

                var list = (from s in pass
                            where s.from_party == Session["party_code"].ToString() && s.pass_type == ddPassFor.SelectedValue && s.financial_year == user.financial_year
                            orderby s.pass_reqno descending
                            select s);
                grdPassApplicationList.DataSource = list.ToList();
                grdPassApplicationList.DataBind();
                foreach (GridViewRow dr1 in grdPassApplicationList.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                    btn.Visible = false;
                }
            }
            else
                if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Distillery Unit")
            {
                var list = (from s in pass
                            where s.party_code == Session["party_code"].ToString() && s.pass_type == ddPassFor.SelectedValue && s.financial_year == user.financial_year
                            orderby s.pass_reqno descending
                            select s);
                grdPassApplicationList.DataSource = list.ToList();
                grdPassApplicationList.DataBind();

                foreach (GridViewRow dr1 in grdPassApplicationList.Rows)
                {
                    Label lbl = dr1.FindControl("lblpass_type") as Label;
                    if (lbl.Text == "RR")
                    {
                        LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                        btn1.Visible = false;
                    }
                    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                    btn.Visible = false;
                }
            }
            else if (Session["rolename"].ToString() == "Bond Officer" && Session["usertype"].ToString() == "Distillery Unit")
            {
                var list = (from s in pass
                            where s.party_code == Session["party_code"].ToString() && (s.record_status == "D" || s.record_status == "Y" || s.record_status == "I" || s.record_status == "R") && s.pass_type == ddPassFor.SelectedValue && s.financial_year == user.financial_year
                            orderby s.pass_reqno descending
                            select s);
                grdPassApplicationList.DataSource = list.ToList();
                grdPassApplicationList.DataBind();
            }
            else if (Session["rolename"].ToString() == "Bond Officer" && Session["usertype"].ToString() == "Sugar Mill")
            {
                var list = (from s in pass
                            where s.from_party == Session["party_code"].ToString() && (s.record_status == "Y" || s.record_status == "I" || s.record_status == "R") && s.pass_type == ddPassFor.SelectedValue && s.financial_year == user.financial_year
                            orderby s.pass_reqno descending
                            select s);
                grdPassApplicationList.DataSource = list.ToList();
                grdPassApplicationList.DataBind();
            }
            else if (Session["UserID"].ToString() == "Admin")
            {
               
                    var list = (from s in pass
                                where s.pass_type == ddPassFor.SelectedValue
                                orderby s.pass_reqno descending
                                select s);
                    grdPassApplicationList.DataSource = list.ToList();
                    grdPassApplicationList.DataBind();
                

                foreach (GridViewRow dr1 in grdPassApplicationList.Rows)
                {
                    Label lbl = dr1.FindControl("lblpass_type") as Label;
                    if (lbl.Text == "RR")
                    {
                        LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                        btn1.Visible = false;
                    }
                    string status = (dr1.FindControl("lblStatus") as Label).Text;

                    if (status == "Issued" || status == "Approved")
                    {
                        (dr1.FindControl("btnEdit") as LinkButton).Visible = true;
                    }
                    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                    btn.Visible = false;
                }

            }
        }
    }
}