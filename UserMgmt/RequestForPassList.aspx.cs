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
    public partial class RequestForPassList : System.Web.UI.Page
    {
        // public static  UserDetails user = new UserDetails();
        List<ReaquestForPass> requests = new List<ReaquestForPass>();
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
                        grdMolassesReleaseRequest.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                      grdNOCList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                        Session["usertype"] = user.party_type;
                        Session["party_code"] = user.party_code;
                        Session["rolename"] = user.role_name;
                        if (user.role_name == "Applicant" || user.role_name == "Bond Officer"|| Session["UserID"].ToString() == "Admin")
                        {
                            ddPassFor.SelectedIndex = 1;
                            ddPassFor_SelectedIndexChanged(sender, null);
                            if (user.role_name == "Bond Officer")
                            {
                                Response.Redirect("PassList");
                            }
                            if (user.party_type == "Distillery Unit")
                            {
                                ddPassFor.SelectedValue = "M";
                            }
                            if (user.role_name == "Sugar Mill")
                            {
                                ddPassFor.SelectedValue = "N";
                            }

                        }

                        else
                        {
                            Response.Redirect("~/User_Mgmt");
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

            if (ddPassFor.SelectedValue != "Select")
            {
                Session["rtype"] = 0;
                Session["ptype"] = ddPassFor.SelectedValue;
                Session["UserID"] = Session["UserID"];
                Response.Redirect("RequestForPassForm");
            }
            else
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append("Select Pass/Dispach For");
                sb.Append("')};");
                sb.Append("</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
            }
            //if (ddPassFor.SelectedValue == "M" && user.party_type== "Sugar Mill")
            //{
            //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //    sb.Append("<script type = 'text/javascript'>");
            //    sb.Append("window.onload=function(){");
            //    sb.Append("alert('");
            //    sb.Append("Not Allowed for Sugar Mill");
            //    sb.Append("')};");
            //    sb.Append("</script>");
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
            //}
        }
        protected void btnApplyForPass_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("PassList");

        }

        protected void btnIssueForPass_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("PassIssueList");
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
            string RequestID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrequest_for_pass_id") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            string party_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpartycode") as Label).Text;
            Session["rpparty_code"] = party_code;
            Session["rfptfinancial_year"] = financial_year;
            Session["Pfinancial_year"] = financial_year;
            Session["RequestID"] = RequestID;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "1";
            Session["ptype"] = ddPassFor.SelectedValue;
            Response.Redirect("RequestForPassForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string RequestID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrequest_for_pass_id") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            string party_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpartycode") as Label).Text;
            Session["rpparty_code"] = party_code;
            Session["rfptfinancial_year"] = financial_year;
            Session["Pfinancial_year"] = financial_year;
            Session["RequestID"] = RequestID;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "2";
            Session["ptype"] =ddPassFor.SelectedValue;
            Response.Redirect("RequestForPassForm");
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
           
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string PassRequestID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrequest_for_pass_id") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            string party_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpartycode") as Label).Text;
            Session["rpparty_code"] = party_code;
            Session["rfptfinancial_year"] = financial_year;
            Session["Pfinancial_year"] = financial_year;
            Session["ptype"] = ddPassFor.SelectedValue;
            Session["PassRequestID"] = PassRequestID;
            Session["rtype"] = "0";
            Response.Redirect("PassForm");
        }
        protected void btnNOCView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string RequestID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrequest_for_pass_id") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            string party_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpartycode") as Label).Text;
            Session["rpparty_code"] = party_code;
            Session["rfptfinancial_year"] = financial_year;
            Session["Pfinancial_year"] = financial_year;
            Session["RequestID"] = RequestID;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "1";
            Session["ptype"] = ddPassFor.SelectedValue;
            Response.Redirect("RequestForPassForm");
        }

        protected void btnNOCEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string RequestID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrequest_for_pass_id") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            string party_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpartycode") as Label).Text;
            Session["rpparty_code"] = party_code;
            Session["rfptfinancial_year"] = financial_year;
            Session["Pfinancial_year"] = financial_year;
            Session["RequestID"] = RequestID;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "2";
            Session["ptype"] = ddPassFor.SelectedValue;
            Response.Redirect("RequestForPassForm");
        }

        protected void btnNOCApply_Click(object sender, EventArgs e)
        {

            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string PassRequestID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrequest_for_pass_id") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            string party_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpartycode") as Label).Text;
            Session["rpparty_code"] = party_code;
            Session["rfptfinancial_year"] = financial_year;
            Session["Pfinancial_year"] = financial_year;
            Session["ptype"] = ddPassFor.SelectedValue;
            Session["PassRequestID"] = PassRequestID;
            Session["rtype"] = "0";
            Response.Redirect("PassForm");
        }


        protected void ddPassFor_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (System.Web.HttpContext.Current.Session["UserID"] != null)
            {
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                if (ddPassFor.SelectedValue == "M")
                {
                    grdNOCList.Visible = false;
                    grdMolassesReleaseRequest.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                    grdMolassesReleaseRequest.Visible = true;
                    if (Session["rolename"].ToString() == "Bond Officer")
                    {


                        //List<ReaquestForPass> requests = new List<ReaquestForPass>();
                        //requests = BL_ReaquestForPass.GetList(user.user_id);
                        //var list = (from s in requests
                        //            where s.toparty_code == user.party_code && s.record_status != "N"
                        //            select s);
                        //grdMolassesReleaseRequest.DataSource = list.ToList();
                        //grdMolassesReleaseRequest.DataBind();
                        //btnAddRecord.Visible = false;
                        //foreach (GridViewRow dr1 in grdMolassesReleaseRequest.Rows)
                        //{
                        //    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                        //    btn.Visible = false;
                        //}


                    }
                    else if (Session["UserID"].ToString() == "Admin")
                    {
                        List<ReaquestForPass> requests = new List<ReaquestForPass>();
                        requests = BL_ReaquestForPass.GetList(Session["UserID"].ToString());
                        var list = (from s in requests
                                    orderby s.request_for_pass_id descending
                                    select s);
                        grdMolassesReleaseRequest.DataSource = list.ToList();
                        grdMolassesReleaseRequest.DataBind();
                        foreach (GridViewRow dr1 in grdMolassesReleaseRequest.Rows)
                        {
                            // LinkButton btn = dr1.FindControl("btnApply") as LinkButton;

                            LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                            btn.Visible = false;
                            DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                            Label lbl = (dr1.FindControl("lblstatus") as Label);
                            if (DateTime.Now.Date > dt)
                            {
                                lbl.Text = "Expired";
                                lbl.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                    else if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Distillery Unit")
                    {


                        List<ReaquestForPass> requests = new List<ReaquestForPass>();
                        requests = BL_ReaquestForPass.GetList(Session["UserID"].ToString());
                        var list = (from s in requests
                                    where s.party_code == Session["party_code"].ToString() && s.financial_year == user.financial_year
                                    orderby s.request_for_pass_id descending
                                    select s);
                        grdMolassesReleaseRequest.DataSource = list.ToList();
                        grdMolassesReleaseRequest.DataBind();
                        foreach (GridViewRow dr1 in grdMolassesReleaseRequest.Rows)
                        {
                            // LinkButton btn = dr1.FindControl("btnApply") as LinkButton;

                            LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                            btn.Visible = false;
                            if((dr1.FindControl("lblValidUpto") as Label).Text!="")
                            { 
                            DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                            Label lbl = (dr1.FindControl("lblstatus") as Label);
                            if (DateTime.Now.Date > dt)
                            {
                                lbl.Text = "Expired";
                                lbl.ForeColor = System.Drawing.Color.Red;
                            }
                            }
                        }


                    }
                    else if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Sugar Mill")
                    {

                        btnAddRecord.Visible = false;
                        List<ReaquestForPass> requests = new List<ReaquestForPass>();
                        requests = BL_ReaquestForPass.GetList(Session["UserID"].ToString());
                        var list = (from s in requests
                                    where s.toparty_code == Session["party_code"].ToString() && (s.record_status != "N") && s.financial_year == user.financial_year
                                    orderby s.request_for_pass_id descending
                                    select s);
                        grdMolassesReleaseRequest.DataSource = list.ToList();
                        grdMolassesReleaseRequest.DataBind();
                        foreach (GridViewRow dr1 in grdMolassesReleaseRequest.Rows)
                        {

                            LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                            btn1.Visible = false;
                            double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                            double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                            if ((dr1.FindControl("lblValidUpto") as Label).Text != "")
                            {
                                DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                            Label lbl = (dr1.FindControl("lblstatus") as Label);
                            //if (approved <= lifted)
                            //{
                            //    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                            //    btn.Visible = false;
                            //}
                            //if ((dr1.FindControl("lblValidUpto") as Label).Text != "")
                            //{
                                if (DateTime.Now.Date > dt || approved <= lifted)
                                {
                                    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                    btn.Visible = false;

                                    lbl.Text = "Expired";
                                    lbl.ForeColor = System.Drawing.Color.Red;
                                }
                            }
                        }
                    }

                }

                else
                {
                    grdNOCList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                    grdMolassesReleaseRequest.Visible = false;
                    grdNOCList.Visible = true;
                    if (Session["rolename"].ToString() == "Bond Officer")
                    {

                        List<ReaquestForPass> requests = new List<ReaquestForPass>();
                        requests = BL_ReaquestForPass.GetNOCList(Session["UserID"].ToString());
                        var list = (from s in requests
                                    where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.financial_year == user.financial_year
                                    orderby s.request_for_pass_id descending
                                    select s);
                        grdNOCList.DataSource = list.ToList();
                        grdNOCList.DataBind();
                        btnAddRecord.Visible = false;
                        foreach (GridViewRow dr1 in grdNOCList.Rows)
                        {
                            LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                            btn.Visible = false;
                            LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                            btn1.Visible = false;
                        }


                    }
                     if (Session["UserID"].ToString() == "Admin")
                    {
                        List<ReaquestForPass> requests = new List<ReaquestForPass>();
                        requests = BL_ReaquestForPass.GetNOCList(Session["UserID"].ToString());
                        var list = (from s in requests
                                    orderby s.request_for_pass_id descending
                                    select s);
                        grdNOCList.DataSource = list.ToList();
                        grdNOCList.DataBind();
                        foreach (GridViewRow dr1 in grdNOCList.Rows)
                        {

                            //LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                            //btn1.Visible = false;
                            double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                            double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                            Label lbl = (dr1.FindControl("lblstatus") as Label);
                            DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                            //if (approved <= lifted)
                            //{
                            //    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                            //    btn.Visible = false;
                            //}
                            if (DateTime.Now.Date > dt || approved <= lifted)
                            {
                                LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                btn.Visible = false;

                                lbl.Text = "Expired";
                                lbl.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                        if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Distillery Unit")
                    {


                        List<ReaquestForPass> requests = new List<ReaquestForPass>();
                        requests = BL_ReaquestForPass.GetNOCList(Session["UserID"].ToString());
                        var list = (from s in requests
                                    where s.party_code == Session["party_code"].ToString() && s.financial_year == user.financial_year
                                    orderby s.request_for_pass_id descending
                                    select s);
                        grdNOCList.DataSource = list.ToList();
                        grdNOCList.DataBind();
                        foreach (GridViewRow dr1 in grdNOCList.Rows)
                        {

                            //LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                            //btn1.Visible = false;
                            double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                            double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                            Label lbl = (dr1.FindControl("lblstatus") as Label);
                            if ((dr1.FindControl("lblValidUpto") as Label).Text != "")
                            {
                                DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                                //if (approved <= lifted)
                                //{
                                //    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                //    btn.Visible = false;
                                //}
                                if (DateTime.Now.Date > dt || approved <= lifted)
                                {
                                    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                    btn.Visible = false;

                                    lbl.Text = "Expired";
                                    lbl.ForeColor = System.Drawing.Color.Red;
                                }
                            }
                        }


                    }
                    if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Sugar Mill")
                    {
                        List<ReaquestForPass> requests = new List<ReaquestForPass>();
                        requests = BL_ReaquestForPass.GetNOCList(Session["UserID"].ToString());
                        var list = (from s in requests
                                    where s.party_code == Session["party_code"].ToString() && s.financial_year == user.financial_year
                                    orderby s.request_for_pass_id descending
                                    select s);
                        grdNOCList.DataSource = list.ToList();
                        grdNOCList.DataBind();
                        btnAddRecord.Visible = true;
                        foreach (GridViewRow dr1 in grdNOCList.Rows)
                        {

                            //LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                            //btn1.Visible = false;
                            double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                            double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                            //if (approved <= lifted)
                            //{
                            //    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                            //    btn.Visible = false;
                            //}
                            if ((dr1.FindControl("lblValidUpto") as Label).Text != "")
                            {
                                DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                                Label lbl = (dr1.FindControl("lblstatus") as Label);
                                if (DateTime.Now.Date > dt || approved <= lifted)
                                {
                                    lbl.Text = "Expired";
                                    lbl.ForeColor = System.Drawing.Color.Red;
                                    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                    btn.Visible = false;
                                }
                            }
                        }
                    }
                }
            }
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
                grdNOCList.Visible = false;
                grdMolassesReleaseRequest.Visible = true;
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                GridViewRow row = grdMolassesReleaseRequest.TopPagerRow;
                TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");

                if (Session["rrrsearch"] != null && Session["rrrtext"] != null)
                {
                    ddsearch.SelectedValue = Session["rrrsearch"].ToString();
                    txtpage.Text = Session["rrrtext"].ToString();
                    if (ddsearch.SelectedValue != "Select")
                    {

                        if (txtpage != null)
                        {
                            if (ddsearch.SelectedValue == "valid_upto")
                            {
                                if (txtpage.Text.ToString().Length == 10)
                                {

                                    requests = BL_ReaquestForPass.GetList(Session["UserID"].ToString());
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                    txtpage.Focus();
                                    txtpage.Text = "";
                                    ddsearch.SelectedIndex = 0;

                                    requests = BL_ReaquestForPass.GetList(Session["UserID"].ToString());
                                }

                            }
                            else
                            {
                                requests = BL_ReaquestForPass.Search("", ddsearch.SelectedValue, txtpage.Text);
                            }
                        }
                    }
                }


                else
                {
                    requests = BL_ReaquestForPass.GetList(Session["UserID"].ToString());
                }


                if (Session["rolename"].ToString() == "Bond Officer")
                {

                    if (ddsearch.SelectedValue == "valied_date")
                    {
                        var list = (from s in requests
                                    where s.party_code == Session["party_code"].ToString() && s.valied_date == txtpage.Text && s.financial_year == user.financial_year
                                    orderby s.request_for_pass_id descending

                                    select s);
                        grdMolassesReleaseRequest.DataSource = list.ToList();
                        grdMolassesReleaseRequest.DataBind();
                    }
                    else
                    {

                        var list = (from s in requests
                                    where s.toparty_code == Session["party_code"].ToString() && s.record_status != "N" && s.financial_year == user.financial_year
                                    orderby s.request_for_pass_id descending
                                    select s);
                        grdMolassesReleaseRequest.DataSource = list.ToList();
                        grdMolassesReleaseRequest.DataBind();
                    }
                    btnAddRecord.Visible = false;
                    foreach (GridViewRow dr1 in grdMolassesReleaseRequest.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                        btn.Visible = false;

                    }


                }
                if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Distillery Unit")
                {
                    if (ddsearch.SelectedValue == "valied_date")
                    {
                        var list = (from s in requests
                                    where s.party_code == Session["party_code"].ToString() && s.valied_date == txtpage.Text && s.financial_year == user.financial_year
                                    orderby s.request_for_pass_id descending
                                    orderby s.issue_nocno ascending
                                    select s);
                        grdMolassesReleaseRequest.DataSource = list.ToList();
                        grdMolassesReleaseRequest.DataBind();
                    }
                    else
                    {

                        var list = (from s in requests
                                    where s.party_code == Session["party_code"].ToString() && s.financial_year == user.financial_year
                                    orderby s.request_for_pass_id descending
                                    select s);
                        grdMolassesReleaseRequest.DataSource = list.ToList();
                        grdMolassesReleaseRequest.DataBind();
                    }
                    foreach (GridViewRow dr1 in grdMolassesReleaseRequest.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                        btn.Visible = false;
                        if ((dr1.FindControl("lblValidUpto") as Label).Text != "")
                        {
                            if ((dr1.FindControl("lblValidUpto") as Label).Text != "")
                            {
                                DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                                Label lbl = (dr1.FindControl("lblstatus") as Label);
                                if (DateTime.Now.Date > dt)
                                {
                                    lbl.Text = "Expired";
                                    lbl.ForeColor = System.Drawing.Color.Red;
                                }
                            }
                        }
                    }


                }
                if (Session["UserID"].ToString() == "Admin")
                {
                    btnAddRecord.Visible = false;

                    if (ddsearch.SelectedValue == "valied_date")
                    {
                        var list = (from s in requests
                                    where  s.valied_date == txtpage.Text 
                                    orderby s.request_for_pass_id descending
                                    orderby s.issue_nocno ascending
                                    select s);
                        grdMolassesReleaseRequest.DataSource = list.ToList();
                        grdMolassesReleaseRequest.DataBind();
                    }
                    else
                    {
                        var list = (from s in requests
                                    orderby s.request_for_pass_id descending
                                    select s);
                        grdMolassesReleaseRequest.DataSource = list.ToList();
                        grdMolassesReleaseRequest.DataBind();
                    }
                    foreach (GridViewRow dr1 in grdMolassesReleaseRequest.Rows)
                    {

                        LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                        btn1.Visible = false;
                        double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                        double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                        Label lbl = (dr1.FindControl("lblstatus") as Label);
                        if ((dr1.FindControl("lblValidUpto") as Label).Text != "")
                        {
                            DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                            //if (approved <= lifted)
                            //{
                            //    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                            //    btn.Visible = false;
                            //}
                            if (DateTime.Now.Date > dt || approved <= lifted)
                            {
                                LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                btn.Visible = false;

                                lbl.Text = "Expired";
                                lbl.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }

                }

                if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Sugar Mill")
                {


                    btnAddRecord.Visible = false;

                    if (ddsearch.SelectedValue == "valied_date")
                    {
                        var list = (from s in requests
                                    where s.party_code == Session["party_code"].ToString() && s.valied_date == txtpage.Text && s.financial_year == user.financial_year
                                    orderby s.request_for_pass_id descending
                                    orderby s.issue_nocno ascending
                                    select s);
                        grdMolassesReleaseRequest.DataSource = list.ToList();
                        grdMolassesReleaseRequest.DataBind();
                    }
                    else
                    {
                        var list = (from s in requests
                                    where s.toparty_code == Session["party_code"].ToString() && (s.record_status != "N") && s.financial_year == user.financial_year
                                    orderby s.request_for_pass_id descending
                                    select s);
                        grdMolassesReleaseRequest.DataSource = list.ToList();
                        grdMolassesReleaseRequest.DataBind();
                    }
                    foreach (GridViewRow dr1 in grdMolassesReleaseRequest.Rows)
                    {

                        LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                        btn1.Visible = false;
                        double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                        double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                        Label lbl = (dr1.FindControl("lblstatus") as Label);
                        if ((dr1.FindControl("lblValidUpto") as Label).Text != "")
                        {
                            DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                            //if (approved <= lifted)
                            //{
                            //    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                            //    btn.Visible = false;
                            //}
                            if (DateTime.Now.Date > dt || approved <= lifted)
                            {
                                LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                btn.Visible = false;

                                lbl.Text = "Expired";
                                lbl.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }

                }

            }
        }

        protected void grdNOCList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["UserID"] != null)
            {
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                grdMolassesReleaseRequest.Visible = false;
                grdNOCList.Visible = true;
                if (e.NewPageIndex < 0)
                {
                    grdNOCList.PageIndex = 0;
                }
                else
                {
                    grdNOCList.PageIndex = e.NewPageIndex;
                }

                GridViewRow row = grdNOCList.TopPagerRow;
                TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");

                if (Session["nocrsearch"] != null && Session["nocrtext"] != null)
                {
                    ddsearch.SelectedValue = Session["nocrsearch"].ToString();
                    txtpage.Text = Session["nocrtext"].ToString();
                    if (ddsearch.SelectedValue != "Select")
                    {

                        if (txtpage != null)
                        {
                            if (ddsearch.SelectedValue == "valied_date")
                            {
                                if (txtpage.Text.ToString().Length == 10)
                                {

                                    requests = BL_ReaquestForPass.GetNOCList(Session["UserID"].ToString());
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                    txtpage.Focus();
                                    txtpage.Text = "";
                                    ddsearch.SelectedIndex = 0;
                               
                                    requests = BL_ReaquestForPass.GetNOCList(Session["UserID"].ToString());
                                }

                            }
                            else
                            {
                                requests = BL_ReaquestForPass.Search1("", ddsearch.SelectedValue, txtpage.Text);
                            }
                        }
                    }
                }


                else
                {
                    requests = BL_ReaquestForPass.GetNOCList(Session["UserID"].ToString());
                }
                if (Session["rolename"].ToString() == "Bond Officer")
                {
                    if (ddsearch.SelectedValue == "valied_date")
                    {
                        var list = (from s in requests
                                    where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.valied_date == txtpage.Text && s.financial_year == user.financial_year
                                    orderby s.request_for_pass_id descending
                                    select s);
                        grdNOCList.DataSource = list.ToList();
                        grdNOCList.DataBind();
                    }
                    else
                    {
                        var list = (from s in requests
                                    where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.financial_year == user.financial_year
                                    orderby s.request_for_pass_id descending
                                    select s);
                        grdNOCList.DataSource = list.ToList();
                        grdNOCList.DataBind();

                    }

                    btnAddRecord.Visible = false;
                    foreach (GridViewRow dr1 in grdNOCList.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                        btn.Visible = false;
                    }


                }
                if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Distillery Unit")
                {
                    if (ddsearch.SelectedValue == "valied_date")
                    {
                        var list = (from s in requests
                                    where s.party_code == Session["party_code"].ToString() && s.valied_date == txtpage.Text && s.financial_year == user.financial_year
                                    orderby s.request_for_pass_id descending
                                    orderby s.issue_nocno ascending
                                    select s);
                        grdNOCList.DataSource = list.ToList();
                        grdNOCList.DataBind();
                    }
                    else
                    {
                        var list = (from s in requests
                                    where s.party_code == Session["party_code"].ToString() && s.financial_year == user.financial_year
                                    orderby s.request_for_pass_id descending
                                    orderby s.issue_nocno ascending
                                    select s);
                        grdNOCList.DataSource = list.ToList();
                        grdNOCList.DataBind();
                    }
                    foreach (GridViewRow dr1 in grdNOCList.Rows)
                    {

                        //LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                        //btn1.Visible = false;
                        double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                        double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                        Label lbl = (dr1.FindControl("lblstatus") as Label);
                        if ((dr1.FindControl("lblValidUpto") as Label).Text != "")
                        {
                            DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                            //if (approved <= lifted)
                            //{
                            //    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                            //    btn.Visible = false;
                            //}
                            if (DateTime.Now.Date > dt || approved <= lifted)
                            {
                                LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                btn.Visible = false;

                                lbl.Text = "Expired";
                                lbl.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }


                }
                if (Session["UserID"].ToString() == "Admin")
                {
                    if (ddsearch.SelectedValue == "valied_date")
                    {
                        var list = (from s in requests
                                    where s.valied_date == txtpage.Text
                                    orderby s.request_for_pass_id descending
                                    orderby s.issue_nocno ascending
                                    select s);
                        grdNOCList.DataSource = list.ToList();
                        grdNOCList.DataBind();
                    }
                    else
                    {
                        var list = (from s in requests
                                  
                                    orderby s.request_for_pass_id descending
                                    orderby s.issue_nocno ascending
                                    select s);
                        grdNOCList.DataSource = list.ToList();
                        grdNOCList.DataBind();
                    }
                    btnAddRecord.Visible = true;
                    foreach (GridViewRow dr1 in grdNOCList.Rows)
                    {

                        //LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                        //btn1.Visible = false;
                        double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                        double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                        //if (approved <= lifted)
                        //{
                        //    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                        //    btn.Visible = false;
                        //}
                        DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                        Label lbl = (dr1.FindControl("lblstatus") as Label);
                        if (DateTime.Now.Date > dt || approved <= lifted)
                        {
                            lbl.Text = "Expired";
                            lbl.ForeColor = System.Drawing.Color.Red;
                            LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                            btn.Visible = false;
                        }
                    }
                }
                if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Sugar Mill")
                {
                    if (ddsearch.SelectedValue == "valied_date")
                    {
                        var list = (from s in requests
                                    where s.party_code == Session["party_code"].ToString() && s.valied_date == txtpage.Text && s.financial_year == user.financial_year
                                    orderby s.request_for_pass_id descending
                                    orderby s.issue_nocno ascending
                                    select s);
                        grdNOCList.DataSource = list.ToList();
                        grdNOCList.DataBind();
                    }
                    else
                    {
                        var list = (from s in requests
                                    where s.party_code == Session["party_code"].ToString() && s.financial_year == user.financial_year
                                    orderby s.request_for_pass_id descending
                                    orderby s.issue_nocno ascending
                                    select s);
                        grdNOCList.DataSource = list.ToList();
                        grdNOCList.DataBind();
                    }
                    btnAddRecord.Visible = true;
                    foreach (GridViewRow dr1 in grdNOCList.Rows)
                    {

                        //LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                        //btn1.Visible = false;
                        double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                        double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                        //if (approved <= lifted)
                        //{
                        //    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                        //    btn.Visible = false;
                        //}
                        if ((dr1.FindControl("lblValidUpto") as Label).Text != "")
                        {
                            DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                            Label lbl = (dr1.FindControl("lblstatus") as Label);
                            if (DateTime.Now.Date > dt || approved <= lifted)
                            {
                                lbl.Text = "Expired";
                                lbl.ForeColor = System.Drawing.Color.Red;
                                LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                btn.Visible = false;
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
            TextBox txtpage1 = (TextBox)row.Cells[0].FindControl("txtpage");
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Convert.ToInt32(txtpage1.Text) <= 0)
            {
                a = 0;
            }
            else
            {
                a = Convert.ToInt32(txtpage1.Text);
            }
            if (a != 0)
            {
                grdMolassesReleaseRequest.PageIndex = a - 1;
            }
            else
            {
                grdMolassesReleaseRequest.PageIndex = a;
            }



            string userid = Session["UserID"].ToString();
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            grdNOCList.Visible = false;
            grdMolassesReleaseRequest.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
            grdMolassesReleaseRequest.Visible = true;
            if (Session["rrrsearch"] != null && Session["rrrtext"] != null)
            {
                ddsearch.SelectedValue = Session["rrrsearch"].ToString();
                txtpage.Text = Session["rrrtext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        if (ddsearch.SelectedValue == "valid_upto")
                        {
                            if (txtpage.Text.ToString().Length == 10)
                            {

                                requests = BL_ReaquestForPass.GetList(Session["UserID"].ToString());
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                txtpage.Focus();
                                txtpage.Text = "";
                                ddsearch.SelectedIndex = 0;

                                requests = BL_ReaquestForPass.GetList(Session["UserID"].ToString());
                            }

                        }
                        else
                        {
                            requests = BL_ReaquestForPass.Search("", ddsearch.SelectedValue, txtpage.Text);
                        }
                    }
                }
            }


            else
            {
                requests = BL_ReaquestForPass.GetList(Session["UserID"].ToString());
            }
            if (Session["rolename"].ToString() == "Bond Officer")
            {

                if (ddsearch.SelectedValue == "valied_date")
                {
                    var list = (from s in requests
                                where s.party_code == Session["party_code"].ToString() && s.valied_date == txtpage.Text && s.financial_year == user.financial_year
                                orderby s.request_for_pass_id descending

                                select s);
                    grdMolassesReleaseRequest.DataSource = list.ToList();
                    grdMolassesReleaseRequest.DataBind();
                }
                else
                {

                    var list = (from s in requests
                                where s.toparty_code == Session["party_code"].ToString() && s.record_status != "N" && s.financial_year == user.financial_year
                                orderby s.request_for_pass_id descending
                                select s);
                    grdMolassesReleaseRequest.DataSource = list.ToList();
                    grdMolassesReleaseRequest.DataBind();
                }
                btnAddRecord.Visible = false;
                foreach (GridViewRow dr1 in grdMolassesReleaseRequest.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                    btn.Visible = false;

                }


            }
            if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Distillery Unit")
            {
                if (ddsearch.SelectedValue == "valied_date")
                {
                    var list = (from s in requests
                                where s.party_code == Session["party_code"].ToString() && s.valied_date == txtpage.Text && s.financial_year == user.financial_year
                                orderby s.request_for_pass_id descending
                                orderby s.issue_nocno ascending
                                select s);
                    grdMolassesReleaseRequest.DataSource = list.ToList();
                    grdMolassesReleaseRequest.DataBind();
                }
                else
                {

                    var list = (from s in requests
                                where s.party_code == Session["party_code"].ToString() && s.financial_year == user.financial_year
                                orderby s.request_for_pass_id descending
                                select s);
                    grdMolassesReleaseRequest.DataSource = list.ToList();
                    grdMolassesReleaseRequest.DataBind();
                }
                foreach (GridViewRow dr1 in grdMolassesReleaseRequest.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                    btn.Visible = false;
                    DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                    Label lbl = (dr1.FindControl("lblstatus") as Label);
                    if (DateTime.Now.Date > dt)
                    {
                        lbl.Text = "Expired";
                        lbl.ForeColor = System.Drawing.Color.Red;
                    }
                }


            }
            if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Sugar Mill")
            {


                btnAddRecord.Visible = false;

                if (ddsearch.SelectedValue == "valied_date")
                {
                    var list = (from s in requests
                                where s.party_code == Session["party_code"].ToString() && s.valied_date == txtpage.Text && s.financial_year == user.financial_year
                                orderby s.request_for_pass_id descending
                                orderby s.issue_nocno ascending
                                select s);
                    grdMolassesReleaseRequest.DataSource = list.ToList();
                    grdMolassesReleaseRequest.DataBind();
                }
                else
                {
                    var list = (from s in requests
                                where s.toparty_code == Session["party_code"].ToString() && (s.record_status != "N") && s.financial_year == user.financial_year
                                orderby s.request_for_pass_id descending
                                select s);
                    grdMolassesReleaseRequest.DataSource = list.ToList();
                    grdMolassesReleaseRequest.DataBind();
                }
                foreach (GridViewRow dr1 in grdMolassesReleaseRequest.Rows)
                {

                    LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                    btn1.Visible = false;
                    double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                    double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                    Label lbl = (dr1.FindControl("lblstatus") as Label);
                    DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                    //if (approved <= lifted)
                    //{
                    //    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                    //    btn.Visible = false;
                    //}
                    if (DateTime.Now.Date > dt || approved <= lifted)
                    {
                        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                        btn.Visible = false;

                        lbl.Text = "Expired";
                        lbl.ForeColor = System.Drawing.Color.Red;
                    }
                }

            }



        }



        protected void txtpage_TextChanged1(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdNOCList.TopPagerRow;
            TextBox txtpage1 = (TextBox)row.Cells[0].FindControl("txtpage");
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Convert.ToInt32(txtpage1.Text) <= 0)
            {
                a = 0;
            }
            else
            {
                a = Convert.ToInt32(txtpage1.Text);
            }
            if (a != 0)
            {
                grdNOCList.PageIndex = a - 1;
            }
            else
            {
                grdNOCList.PageIndex = a;
            }


            string userid = Session["UserID"].ToString();
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            grdMolassesReleaseRequest.Visible = false;
            grdNOCList.Visible = true;
          

            if (Session["nocrsearch"] != null && Session["nocrtext"] != null)
            {
                ddsearch.SelectedValue = Session["nocrsearch"].ToString();
                txtpage.Text = Session["nocrtext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        if (ddsearch.SelectedValue == "valied_date")
                        {
                            if (txtpage.Text.ToString().Length == 10)
                            {

                                requests = BL_ReaquestForPass.GetNOCList(Session["UserID"].ToString());
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                txtpage.Focus();
                                txtpage.Text = "";
                                ddsearch.SelectedIndex = 0;

                                requests = BL_ReaquestForPass.GetNOCList(Session["UserID"].ToString());
                            }

                        }
                        else
                        {
                            requests = BL_ReaquestForPass.Search1("", ddsearch.SelectedValue, txtpage.Text);
                        }
                    }
                }
            }


            else
            {
                requests = BL_ReaquestForPass.GetNOCList(Session["UserID"].ToString());
            }
            if (Session["rolename"].ToString() == "Bond Officer")
            {
                if (ddsearch.SelectedValue == "valied_date")
                {
                    var list = (from s in requests
                                where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.valied_date == txtpage.Text && s.financial_year == user.financial_year
                                orderby s.request_for_pass_id descending
                                select s);
                    grdNOCList.DataSource = list.ToList();
                    grdNOCList.DataBind();
                }
                else
                {
                    var list = (from s in requests
                                where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.financial_year == user.financial_year
                                orderby s.request_for_pass_id descending
                                select s);
                    grdNOCList.DataSource = list.ToList();
                    grdNOCList.DataBind();

                }

                btnAddRecord.Visible = false;
                foreach (GridViewRow dr1 in grdNOCList.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                    btn.Visible = false;
                }


            }
            if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Distillery Unit")
            {
                if (ddsearch.SelectedValue == "valied_date")
                {
                    var list = (from s in requests
                                where s.party_code == Session["party_code"].ToString() && s.valied_date == txtpage.Text && s.financial_year == user.financial_year
                                orderby s.request_for_pass_id descending
                                orderby s.issue_nocno ascending
                                select s);
                    grdNOCList.DataSource = list.ToList();
                    grdNOCList.DataBind();
                }
                else
                {
                    var list = (from s in requests
                                where s.party_code == Session["party_code"].ToString() && s.financial_year == user.financial_year
                                orderby s.request_for_pass_id descending
                                orderby s.issue_nocno ascending
                                select s);
                    grdNOCList.DataSource = list.ToList();
                    grdNOCList.DataBind();
                }
                foreach (GridViewRow dr1 in grdNOCList.Rows)
                {

                    //LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                    //btn1.Visible = false;
                    double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                    double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                    Label lbl = (dr1.FindControl("lblstatus") as Label);
                    if ((dr1.FindControl("lblValidUpto") as Label).Text != "")
                    {
                        DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                        //if (approved <= lifted)
                        //{
                        //    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                        //    btn.Visible = false;
                        //}
                        if (DateTime.Now.Date > dt || approved <= lifted)
                        {
                            LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                            btn.Visible = false;

                            lbl.Text = "Expired";
                            lbl.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }


            }
            if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Sugar Mill")
            {
                if (ddsearch.SelectedValue == "valied_date")
                {
                    var list = (from s in requests
                                where s.party_code == Session["party_code"].ToString() && s.valied_date == txtpage.Text && s.financial_year == user.financial_year
                                orderby s.request_for_pass_id descending
                                orderby s.issue_nocno ascending
                                select s);
                    grdNOCList.DataSource = list.ToList();
                    grdNOCList.DataBind();
                }
                else
                {
                    var list = (from s in requests
                                where s.party_code == Session["party_code"].ToString() && s.financial_year == user.financial_year
                                orderby s.request_for_pass_id descending
                                orderby s.issue_nocno ascending
                                select s);
                    grdNOCList.DataSource = list.ToList();
                    grdNOCList.DataBind();
                }
                btnAddRecord.Visible = true;
                foreach (GridViewRow dr1 in grdNOCList.Rows)
                {

                    //LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                    //btn1.Visible = false;
                    double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                    double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                    //if (approved <= lifted)
                    //{
                    //    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                    //    btn.Visible = false;
                    //}
                    if ((dr1.FindControl("lblValidUpto") as Label).Text != "")
                    {
                        DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                        Label lbl = (dr1.FindControl("lblstatus") as Label);
                        if (DateTime.Now.Date > dt || approved <= lifted)
                        {
                            lbl.Text = "Expired";
                            lbl.ForeColor = System.Drawing.Color.Red;
                            LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                            btn.Visible = false;
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

            if (Session["rrrsearch"] != null && Session["rrrtext"] != null)
            {
                ddsearch.SelectedValue = Session["rrrsearch"].ToString();
                txtpages.Text = Session["rrrtext"].ToString();
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

        protected void grdNOCList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdNOCList.TopPagerRow;
            if (grdNOCList.PageCount != 0)
            {
                grdNOCList.TopPagerRow.Visible = true;
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

            if (Session["nocrsearch"] != null && Session["nocrtext"] != null)
            {
                ddsearch.SelectedValue = Session["nocrsearch"].ToString();
                txtpages.Text = Session["nocrtext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdNOCList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdNOCList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdNOCList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdNOCList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdNOCList.PageIndex == 0)
            {
                ((ImageButton)grdNOCList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdNOCList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdNOCList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdNOCList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdNOCList.PageIndex + 1 == grdNOCList.PageCount)
            {
                ((ImageButton)grdNOCList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdNOCList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdNOCList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdNOCList.TopPagerRow.FindControl("btnNext")).Visible = false;

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
                Session["rrrsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["rrrtext"] = txtpage.Text;
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        if (ddsearch.SelectedValue == "valied_date")
                        {
                            if (txtpage.Text.ToString().Length == 10)
                            {
                                requests = BL_ReaquestForPass.GetList(Session["UserID"].ToString());
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                txtpage.Focus();
                                txtpage.Text = "";
                                ddsearch.SelectedIndex = 0;
                             
                                requests = BL_ReaquestForPass.GetList(Session["UserID"].ToString());
                            }
                        }
                        else
                        {

                            requests = BL_ReaquestForPass.Search("", ddsearch.SelectedValue, txtpage.Text);
                        }
                        if (Session["rolename"].ToString() == "Bond Officer")
                        {

                            if (ddsearch.SelectedValue == "valied_date")
                            {
                                var list = (from s in requests
                                            where s.party_code == Session["party_code"].ToString() && s.valied_date == txtpage.Text 
                                            orderby s.request_for_pass_id descending
                                          
                                            select s);
                                grdMolassesReleaseRequest.DataSource = list.ToList();
                                grdMolassesReleaseRequest.DataBind();
                            }
                            else
                            {

                                var list = (from s in requests
                                            where s.toparty_code == Session["party_code"].ToString() && s.record_status != "N" 
                                            orderby s.request_for_pass_id descending
                                            select s);
                                grdMolassesReleaseRequest.DataSource = list.ToList();
                                grdMolassesReleaseRequest.DataBind();
                            }
                            btnAddRecord.Visible = false;
                            foreach (GridViewRow dr1 in grdMolassesReleaseRequest.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                btn.Visible = false;

                            }


                        }
                        if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Distillery Unit")
                        {
                            if (ddsearch.SelectedValue == "valied_date")
                            {
                                var list = (from s in requests
                                            where s.party_code == Session["party_code"].ToString() && s.valied_date == txtpage.Text 
                                            orderby s.request_for_pass_id descending
                                            orderby s.issue_nocno ascending
                                            select s);
                                grdMolassesReleaseRequest.DataSource = list.ToList();
                                grdMolassesReleaseRequest.DataBind();
                            }
                            else
                            {

                                var list = (from s in requests
                                            where s.party_code == Session["party_code"].ToString() 
                                            orderby s.request_for_pass_id descending
                                            select s);
                                grdMolassesReleaseRequest.DataSource = list.ToList();
                                grdMolassesReleaseRequest.DataBind();
                            }
                            foreach (GridViewRow dr1 in grdMolassesReleaseRequest.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                btn.Visible = false;
                                DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                                Label lbl = (dr1.FindControl("lblstatus") as Label);
                                if (DateTime.Now.Date > dt)
                                {
                                    lbl.Text = "Expired";
                                    lbl.ForeColor = System.Drawing.Color.Red;
                                }
                            }


                        }
                        if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Sugar Mill")
                        {


                            btnAddRecord.Visible = false;

                            if (ddsearch.SelectedValue == "valied_date")
                            {
                                var list = (from s in requests
                                            where s.toparty_code == Session["party_code"].ToString() && s.valied_date == txtpage.Text 
                                            orderby s.request_for_pass_id descending
                                            orderby s.issue_nocno ascending
                                            select s);
                                grdMolassesReleaseRequest.DataSource = list.ToList();
                                grdMolassesReleaseRequest.DataBind();
                            }
                            else
                            {
                                var list = (from s in requests
                                            where s.toparty_code == Session["party_code"].ToString() && (s.record_status != "N") 
                                            orderby s.request_for_pass_id descending
                                            select s);
                                grdMolassesReleaseRequest.DataSource = list.ToList();
                                grdMolassesReleaseRequest.DataBind();
                            }
                            foreach (GridViewRow dr1 in grdMolassesReleaseRequest.Rows)
                            {

                                LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                                btn1.Visible = false;
                                double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                                double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                                Label lbl = (dr1.FindControl("lblstatus") as Label);
                                DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                                //if (approved <= lifted)
                                //{
                                //    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                //    btn.Visible = false;
                                //}
                                if (DateTime.Now.Date > dt || approved <= lifted)
                                {
                                    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                    btn.Visible = false;

                                    lbl.Text = "Expired";
                                    lbl.ForeColor = System.Drawing.Color.Red;
                                }
                            }

                        }
                        if (Session["UserID"].ToString() == "Admin")
                        {

                            btnAddRecord.Visible = false;

                            if (ddsearch.SelectedValue == "valied_date")
                            {
                                var list = (from s in requests
                                            where  s.valied_date == txtpage.Text
                                            orderby s.request_for_pass_id descending
                                            orderby s.issue_nocno ascending
                                            select s);
                                grdMolassesReleaseRequest.DataSource = list.ToList();
                                grdMolassesReleaseRequest.DataBind();
                            }
                            else
                            {
                                var list = (from s in requests
                                            orderby s.request_for_pass_id descending
                                            select s);
                                grdMolassesReleaseRequest.DataSource = list.ToList();
                                grdMolassesReleaseRequest.DataBind();
                            }
                            foreach (GridViewRow dr1 in grdMolassesReleaseRequest.Rows)
                            {

                                LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                                btn1.Visible = false;
                                double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                                double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                                Label lbl = (dr1.FindControl("lblstatus") as Label);
                                DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                                //if (approved <= lifted)
                                //{
                                //    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                //    btn.Visible = false;
                                //}
                                if (DateTime.Now.Date > dt || approved <= lifted)
                                {
                                    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                    btn.Visible = false;

                                    lbl.Text = "Expired";
                                    lbl.ForeColor = System.Drawing.Color.Red;
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
        protected void btnsearch_Click1(object sender, EventArgs e)
        {
            GridViewRow row = grdNOCList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {
                Session["nocrsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["nocrtext"] = txtpage.Text;
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        if (ddsearch.SelectedValue == "valied_date")
                        {
                            if (txtpage.Text.ToString().Length == 10)
                            {
                                requests = BL_ReaquestForPass.GetNOCList(Session["UserID"].ToString());
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                txtpage.Focus();
                                txtpage.Text = "";
                                ddsearch.SelectedIndex = 0;
                                requests = BL_ReaquestForPass.GetNOCList(Session["UserID"].ToString());
                            }
                        }
                        else
                        {
                            requests = BL_ReaquestForPass.Search1("", ddsearch.SelectedValue, txtpage.Text);
                        }
                        if (Session["rolename"].ToString() == "Bond Officer")
                        {
                            if (ddsearch.SelectedValue == "valied_date")
                            {
                                var list = (from s in requests
                                            where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.valied_date==txtpage.Text
                                            orderby s.request_for_pass_id descending
                                            select s);
                                grdNOCList.DataSource = list.ToList();
                                grdNOCList.DataBind();
                            }
                            else
                            {
                                var list = (from s in requests
                                            where s.party_code == Session["party_code"].ToString() && s.record_status != "N"
                                            orderby s.request_for_pass_id descending
                                            select s);
                                grdNOCList.DataSource = list.ToList();
                                grdNOCList.DataBind();

                            }
                                
                            btnAddRecord.Visible = false;
                            foreach (GridViewRow dr1 in grdNOCList.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                btn.Visible = false;
                            }


                        }
                        if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Distillery Unit")
                        {
                            if (ddsearch.SelectedValue == "valied_date")
                            {
                                var list = (from s in requests
                                            where s.party_code == Session["party_code"].ToString() && s.valied_date == txtpage.Text
                                            orderby s.request_for_pass_id descending
                                            orderby s.issue_nocno ascending
                                            select s);
                                grdNOCList.DataSource = list.ToList();
                                grdNOCList.DataBind();
                            }
                            else
                            {
                                var list = (from s in requests
                                            where s.party_code == Session["party_code"].ToString()
                                            orderby s.request_for_pass_id descending
                                            orderby s.issue_nocno ascending
                                            select s);
                                grdNOCList.DataSource = list.ToList();
                                grdNOCList.DataBind();
                            }
                            foreach (GridViewRow dr1 in grdNOCList.Rows)
                            {

                                //LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                                //btn1.Visible = false;
                                double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                                double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                                Label lbl = (dr1.FindControl("lblstatus") as Label);
                                if ((dr1.FindControl("lblValidUpto") as Label).Text != "")
                                {
                                    DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                                    //if (approved <= lifted)
                                    //{
                                    //    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                    //    btn.Visible = false;
                                    //}
                                    if (DateTime.Now.Date > dt || approved <= lifted)
                                    {
                                        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                        btn.Visible = false;

                                        lbl.Text = "Expired";
                                        lbl.ForeColor = System.Drawing.Color.Red;
                                    }
                                }
                            }


                        }
                        if (Session["UserID"].ToString() == "Admin")
                        {
                            if (ddsearch.SelectedValue == "valied_date")
                            {
                                var list = (from s in requests
                                            where  s.valied_date == txtpage.Text
                                            orderby s.request_for_pass_id descending
                                            orderby s.issue_nocno ascending
                                            select s);
                                grdNOCList.DataSource = list.ToList();
                                grdNOCList.DataBind();
                            }
                            else
                            {
                                var list = (from s in requests
                                            orderby s.request_for_pass_id descending
                                            orderby s.issue_nocno ascending
                                            select s);
                                grdNOCList.DataSource = list.ToList();
                                grdNOCList.DataBind();
                            }
                            btnAddRecord.Visible = true;
                            foreach (GridViewRow dr1 in grdNOCList.Rows)
                            {

                                //LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                                //btn1.Visible = false;
                                double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                                double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                                //if (approved <= lifted)
                                //{
                                //    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                //    btn.Visible = false;
                                //}
                                DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                                Label lbl = (dr1.FindControl("lblstatus") as Label);
                                if (DateTime.Now.Date > dt || approved <= lifted)
                                {
                                    lbl.Text = "Expired";
                                    lbl.ForeColor = System.Drawing.Color.Red;
                                    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                    btn.Visible = false;
                                }
                            }
                        }
                            if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Sugar Mill")
                        {
                            if (ddsearch.SelectedValue == "valied_date")
                            {
                                var list = (from s in requests
                                            where s.party_code == Session["party_code"].ToString() && s.valied_date == txtpage.Text
                                            orderby s.request_for_pass_id descending
                                            orderby s.issue_nocno ascending
                                            select s);
                                grdNOCList.DataSource = list.ToList();
                                grdNOCList.DataBind();
                            }
                            else
                            {
                                var list = (from s in requests
                                            where s.party_code == Session["party_code"].ToString()
                                            orderby s.request_for_pass_id descending
                                            orderby s.issue_nocno ascending
                                            select s);
                                grdNOCList.DataSource = list.ToList();
                                grdNOCList.DataBind();
                            }
                            btnAddRecord.Visible = true;
                            foreach (GridViewRow dr1 in grdNOCList.Rows)
                            {

                                //LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                                //btn1.Visible = false;
                                double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                                double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                                //if (approved <= lifted)
                                //{
                                //    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                //    btn.Visible = false;
                                //}
                                if ((dr1.FindControl("lblValidUpto") as Label).Text != "")
                                {
                                    DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                                    Label lbl = (dr1.FindControl("lblstatus") as Label);
                                    if (DateTime.Now.Date > dt || approved <= lifted)
                                    {
                                        lbl.Text = "Expired";
                                        lbl.ForeColor = System.Drawing.Color.Red;
                                        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                        btn.Visible = false;
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
            Session["rrrsearch"] = null;
            Session["rrrtext"] = null;
            if (Session["rolename"].ToString() == "Bond Officer")
            {


                List<ReaquestForPass> requests = new List<ReaquestForPass>();
                requests = BL_ReaquestForPass.GetList(Session["UserID"].ToString());
                var list = (from s in requests
                            where s.toparty_code == Session["party_code"].ToString() && s.record_status != "N"
                            orderby s.request_for_pass_id descending
                            select s);
                grdMolassesReleaseRequest.DataSource = list.ToList();
                grdMolassesReleaseRequest.DataBind();
                btnAddRecord.Visible = false;
                foreach (GridViewRow dr1 in grdMolassesReleaseRequest.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                    btn.Visible = false;

                }


            }
            if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Distillery Unit")
            {


                List<ReaquestForPass> requests = new List<ReaquestForPass>();
                requests = BL_ReaquestForPass.GetList(Session["UserID"].ToString());
                var list = (from s in requests
                            where s.party_code == Session["party_code"].ToString()
                            orderby s.request_for_pass_id descending
                            select s);
                grdMolassesReleaseRequest.DataSource = list.ToList();
                grdMolassesReleaseRequest.DataBind();
                foreach (GridViewRow dr1 in grdMolassesReleaseRequest.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                    btn.Visible = false;
                    DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                    Label lbl = (dr1.FindControl("lblstatus") as Label);
                    if (DateTime.Now.Date > dt)
                    {
                        lbl.Text = "Expired";
                        lbl.ForeColor = System.Drawing.Color.Red;
                    }
                }


            }
            if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Sugar Mill")
            {


                btnAddRecord.Visible = false;
                List<ReaquestForPass> requests = new List<ReaquestForPass>();
                requests = BL_ReaquestForPass.GetList(Session["UserID"].ToString());
                var list = (from s in requests
                            where s.toparty_code == Session["party_code"].ToString() && (s.record_status != "N")
                            orderby s.request_for_pass_id descending
                            select s);
                grdMolassesReleaseRequest.DataSource = list.ToList();
                grdMolassesReleaseRequest.DataBind();
                foreach (GridViewRow dr1 in grdMolassesReleaseRequest.Rows)
                {

                    LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                    btn1.Visible = false;
                    double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                    double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                    Label lbl = (dr1.FindControl("lblstatus") as Label);
                    DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                    //if (approved <= lifted)
                    //{
                    //    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                    //    btn.Visible = false;
                    //}
                    if (DateTime.Now.Date > dt || approved <= lifted)
                    {
                        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                        btn.Visible = false;

                        lbl.Text = "Expired";
                        lbl.ForeColor = System.Drawing.Color.Red;
                    }
                }

            }
        }

        protected void LinkButton5_Click1(object sender, EventArgs e)
        {
            Session["nocrsearch"] = null;
            Session["nocrtext"] = null;
            if (Session["rolename"].ToString() == "Bond Officer")
            {

                List<ReaquestForPass> requests = new List<ReaquestForPass>();
                requests = BL_ReaquestForPass.GetNOCList(Session["UserID"].ToString());
                var list = (from s in requests
                            where s.party_code == Session["party_code"].ToString() && s.record_status != "N"
                            orderby s.request_for_pass_id descending
                            select s);
                grdNOCList.DataSource = list.ToList();
                grdNOCList.DataBind();
                btnAddRecord.Visible = false;
                foreach (GridViewRow dr1 in grdNOCList.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                    btn.Visible = false;
                }


            }
            if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Distillery Unit")
            {


                List<ReaquestForPass> requests = new List<ReaquestForPass>();
                requests = BL_ReaquestForPass.GetNOCList(Session["UserID"].ToString());
                var list = (from s in requests
                            where s.party_code == Session["party_code"].ToString()
                            orderby s.request_for_pass_id descending
                            orderby s.issue_nocno ascending
                            select s);
                grdNOCList.DataSource = list.ToList();
                grdNOCList.DataBind();
                foreach (GridViewRow dr1 in grdNOCList.Rows)
                {

                    //LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                    //btn1.Visible = false;
                    double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                    double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                    Label lbl = (dr1.FindControl("lblstatus") as Label);
                    if ((dr1.FindControl("lblValidUpto") as Label).Text != "")
                    {
                        DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                        //if (approved <= lifted)
                        //{
                        //    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                        //    btn.Visible = false;
                        //}
                        if (DateTime.Now.Date > dt || approved <= lifted)
                        {
                            LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                            btn.Visible = false;

                            lbl.Text = "Expired";
                            lbl.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }


            }
            if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Sugar Mill")
            {
                List<ReaquestForPass> requests = new List<ReaquestForPass>();
                requests = BL_ReaquestForPass.GetNOCList(Session["UserID"].ToString());
                var list = (from s in requests
                            where s.party_code == Session["party_code"].ToString()
                            orderby s.request_for_pass_id descending
                            orderby s.issue_nocno ascending
                            select s);
                grdNOCList.DataSource = list.ToList();
                grdNOCList.DataBind();
                btnAddRecord.Visible = true;
                foreach (GridViewRow dr1 in grdNOCList.Rows)
                {

                    //LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                    //btn1.Visible = false;
                    double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                    double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                    //if (approved <= lifted)
                    //{
                    //    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                    //    btn.Visible = false;
                    //}
                    if ((dr1.FindControl("lblValidUpto") as Label).Text != "")
                    {
                        DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                        Label lbl = (dr1.FindControl("lblstatus") as Label);
                        if (DateTime.Now.Date > dt || approved <= lifted)
                        {
                            lbl.Text = "Expired";
                            lbl.ForeColor = System.Drawing.Color.Red;
                            LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                            btn.Visible = false;
                        }
                    }
                }
            }
        }
    }
}