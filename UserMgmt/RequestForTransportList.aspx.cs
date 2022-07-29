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
    public partial class RequestForTransportList : System.Web.UI.Page
    {
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
                        Session["usertype"] = user.party_type;
                        Session["party_code"] = user.party_code;
                        Session["rolename"] = user.role_name;
                        if (user.role_name == "Applicant" || user.role_name == "Bond Officer" || user.role_name.Trim()== "Deputy Commissioner")
                        {
                            ddPassFor.SelectedIndex = 2;
                            ddPassFor_SelectedIndexChanged(sender, null);
                            //if (user.role_name == "Bond Officer")
                            //{
                            //    Response.Redirect("RequestForTransportList");
                            //}
                            if (user.party_type == "Distillery Unit" || user.party_code=="ENA" )
                            {
                                ddPassFor.SelectedValue = "N";
                            }
                            if (user.role_name == "Sugar Mill")
                            {
                                ddPassFor.SelectedValue = "N";
                            }

                        }

                        else
                        {
                            Session["UserID"] = Session["UserID"];
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
                Response.Redirect("RequestForTransportPassForm");
            }
            else
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append("Select Transport Pass/Dispach For");
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
            Response.Redirect("PassDispatchList");

        }

        protected void btnIssueForPass_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("PassIssueList");
        }
        protected void btnRequestForPass_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RequestForTransportList");

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string RequestID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrequest_for_pass_id") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            string party_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpartycode") as Label).Text;
            Session["rtpparty_code"] = party_code;
            Session["rftptfinancial_year"] = financial_year;
            Session["RequestID"] = RequestID;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "1";
            Session["ptype"] = ddPassFor.SelectedValue;
            Response.Redirect("RequestForTransportPassForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string RequestID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrequest_for_pass_id") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            string party_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpartycode") as Label).Text;
            Session["rtpparty_code"] = party_code;
            Session["rftptfinancial_year"] = financial_year;
            Session["RequestID"] = RequestID;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "2";
            Session["ptype"] = ddPassFor.SelectedValue;
            Response.Redirect("RequestForTransportPassForm");
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {

            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string PassRequestID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrequest_for_pass_id") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            string party_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpartycode") as Label).Text;
            Session["rtpparty_code"] = party_code;
            Session["rftptfinancial_year"] = financial_year;
            Session["ptype"] = ddPassFor.SelectedValue;
            Session["PassRequestID"] = PassRequestID;
            Session["rtype"] = "0";
            Response.Redirect("PassDispatchForm");
        }
        protected void btnNOCView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string RequestID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrequest_for_pass_id") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            string party_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpartycode") as Label).Text;
            string recordstatuss = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrecordstatus") as Label).Text;
            Session["recordstatus"] = recordstatuss;
            Session["rtpparty_code"] = party_code;
            Session["rftptfinancial_year"] = financial_year;
            Session["RequestID"] = RequestID;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "1";
            Session["ptype"] = ddPassFor.SelectedValue;
            Response.Redirect("RequestForTransportPassForm");
        }

        protected void btnNOCEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string RequestID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrequest_for_pass_id") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            string party_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpartycode") as Label).Text;
            string recordstatuss= (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrecordstatus") as Label).Text;
            Session["recordstatus"] = recordstatuss;
            Session["rtpparty_code"] = party_code;
            Session["rftptfinancial_year"] = financial_year;
            Session["RequestID"] = RequestID;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "2";
            Session["ptype"] = ddPassFor.SelectedValue;
            Response.Redirect("RequestForTransportPassForm");
        }

        protected void btnNOCApply_Click(object sender, EventArgs e)
        {

            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string PassRequestID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrequest_for_pass_id") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            string party_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpartycode") as Label).Text;
            string recordstatuss = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrecordstatus") as Label).Text;
            Session["recordstatus"] = recordstatuss;
            Session["rtpparty_code"] = party_code;
            Session["rftptfinancial_year"] = financial_year;
            Session["ptype"] = ddPassFor.SelectedValue;
            Session["PassRequestID"] = PassRequestID;
            Session["rtype"] = "0";
            Response.Redirect("PassDispatchForm");
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
                    else if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "ENA Distillery Unit")
                    {


                        List<ReaquestForPass> requests = new List<ReaquestForPass>();
                        requests = BL_RequestForTransportPass.GetList(Session["UserID"].ToString());
                        var list = (from s in requests
                                    where s.party_code == Session["party_code"].ToString()
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
                                LinkButton btn2 = dr1.FindControl("btnEdit") as LinkButton;
                                btn2.Visible = false;
                                lbl.Text = "Expired";
                                lbl.ForeColor = System.Drawing.Color.Red;
                            }
                        }


                    }
                    else if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Sugar Mill")
                    {

                        btnAddRecord.Visible = false;
                        List<ReaquestForPass> requests = new List<ReaquestForPass>();
                        requests = BL_RequestForTransportPass.GetList(Session["UserID"].ToString());
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
                            DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                            Label lbl = (dr1.FindControl("lblstatus") as Label);
                            if (approved <= lifted)
                            {
                                LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                btn.Visible = false;
                            }
                            if (DateTime.Now.Date > dt)
                            {
                                LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                btn.Visible = false;
                                LinkButton btn2 = dr1.FindControl("btnEdit") as LinkButton;
                                btn2.Visible = false;
                                lbl.Text = "Expired";
                                lbl.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }

                }

                else
                {
                    grdNOCList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                    string passtype = "";
                    if (ddPassFor.SelectedValue=="N")
                    {
                        passtype = "DOM";
                        grdMolassesReleaseRequest.Visible = false;
                        grdNOCList.Visible = true;
                        if (grdNOCList.Rows.Count > 0)
                        {
                            grdNOCList.HeaderRow.Cells[0].Text = "Permit No";
                            grdNOCList.HeaderRow.Cells[5].Text = "Permit Qty";
                        }
                        if (Session["rolename"].ToString() == "Bond Officer")
                        {

                            List<ReaquestForPass> requests = new List<ReaquestForPass>();
                            requests = BL_RequestForTransportPass.GetpermitList(Session["UserID"].ToString(), passtype);
                            var list = (from s in requests
                                        where s.party_code == Session["party_code"].ToString() && s.record_status != "N" /*&& s.financial_year==user.financial_year*/
                                        orderby s.request_for_pass_id descending
                                        select s);
                            grdNOCList.DataSource = list.ToList();
                            grdNOCList.DataBind();
                            if (grdNOCList.Rows.Count > 0)
                            {
                                grdNOCList.HeaderRow.Cells[0].Text = "Permit No";
                                grdNOCList.HeaderRow.Cells[5].Text = "Permit Qty";
                            }
                            btnAddRecord.Visible = false;
                            foreach (GridViewRow dr1 in grdNOCList.Rows)
                            {
                                LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                btn.Visible = false;
                                LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                                btn1.Visible = false;
                            }


                        }
                        //if (Session["rolename"].ToString().Trim() == "Deputy Commissioner")
                        //{

                        //    List<ReaquestForPass> requests = new List<ReaquestForPass>();
                        //    requests = BL_RequestForTransportPass.GetpermitList(Session["UserID"].ToString(), passtype);
                        //    var list = (from s in requests
                        //                where s.record_status == "A" && s.approval_status.Trim() != "Approved"
                        //                orderby s.request_for_pass_id descending
                        //                select s);
                        //    grdNOCList.DataSource = list.ToList();
                        //    grdNOCList.DataBind();
                        //    if (grdNOCList.Rows.Count > 0)
                        //    {
                        //        grdNOCList.HeaderRow.Cells[0].Text = "Permit No";
                        //        grdNOCList.HeaderRow.Cells[5].Text = "Permit Qty";
                        //    }
                        //    btnAddRecord.Visible = false;
                        //    foreach (GridViewRow dr1 in grdNOCList.Rows)
                        //    {
                        //        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                        //        btn.Visible = false;
                        //        LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                        //        btn1.Visible = false;
                        //    }


                        //}
                        if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "ENA Distillery Unit")
                        {
                            List<ReaquestForPass> requests = new List<ReaquestForPass>();
                            requests = BL_RequestForTransportPass.GetpermitList(Session["UserID"].ToString(), passtype);
                            var list = (from s in requests
                                        where s.party_code == Session["party_code"].ToString() /*/* && s.financial_year == user.financial_year*/
                                        orderby s.request_for_pass_id descending
                                        select s);
                            grdNOCList.DataSource = list.ToList();
                            grdNOCList.DataBind();
                            if (grdNOCList.Rows.Count > 0)
                            {
                                grdNOCList.HeaderRow.Cells[0].Text = "Permit No";
                                grdNOCList.HeaderRow.Cells[5].Text = "Permit Qty";
                            }
                            foreach (GridViewRow dr1 in grdNOCList.Rows)
                            {
                                //LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                                //btn1.Visible = false;
                                double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                                double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text); 
                                   string  recordstatus = (dr1.FindControl("lblrecordstatus") as Label).Text;
                                Label lbl = (dr1.FindControl("lblstatus") as Label);
                                DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                                if (approved <= lifted)
                                {
                                    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                    btn.Visible = false;
                                }
                                LinkButton btn1 = dr1.FindControl("btnissue") as LinkButton;
                                btn1.Visible = false;
                               
                                    if (DateTime.Now.Date > dt  && recordstatus!="N")
                                    {
                                        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                        btn.Visible = false; 
                                         LinkButton btn2 = dr1.FindControl("btnEdit") as LinkButton;
                                        btn2.Visible = false;
                                    LinkButton btn3 = dr1.FindControl("LinkButton1") as LinkButton;
                                    btn3.Visible = false;
                                    lbl.Text = "Expired";
                                        lbl.ForeColor = System.Drawing.Color.Red;
                                    }
                                
                            }


                        }
                        if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Sugar Mill")
                        {
                            List<ReaquestForPass> requests = new List<ReaquestForPass>();
                            requests = BL_RequestForTransportPass.GetpermitList(Session["UserID"].ToString(), passtype);
                            var list = (from s in requests
                                        where s.party_code == Session["party_code"].ToString() /*/* && s.financial_year == user.financial_year*/
                                        orderby s.request_for_pass_id descending
                                        select s);
                            grdNOCList.DataSource = list.ToList();
                            grdNOCList.DataBind();
                            if (grdNOCList.Rows.Count > 0)
                            {
                                grdNOCList.HeaderRow.Cells[0].Text = "Permit No";
                                grdNOCList.HeaderRow.Cells[5].Text = "Permit Qty";
                            }
                            btnAddRecord.Visible = true;
                            foreach (GridViewRow dr1 in grdNOCList.Rows)
                            {
                                //LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                                //btn1.Visible = false;
                                double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                                double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                                if (approved <= lifted)
                                {
                                    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                    btn.Visible = false;
                                }
                                DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                                Label lbl = (dr1.FindControl("lblstatus") as Label);
                                string recordstatus = (dr1.FindControl("lblrecordstatus") as Label).Text;
                                if (DateTime.Now.Date > dt && recordstatus != "N")
                                {
                                    LinkButton btn2 = dr1.FindControl("btnEdit") as LinkButton;
                                    LinkButton btn3 = dr1.FindControl("LinkButton1") as LinkButton;
                                    btn3.Visible = false;
                                    btn2.Visible = false;
                                    lbl.Text = "Expired";
                                    lbl.ForeColor = System.Drawing.Color.Red;
                                }
                            }
                        }
                    }
                
                    else
                    {
                        passtype = "EXP";
                        grdMolassesReleaseRequest.Visible = false;
                        grdNOCList.Visible = true;
                        if (Session["rolename"].ToString() == "Bond Officer")
                        {

                            List<ReaquestForPass> requests = new List<ReaquestForPass>();
                            requests = BL_RequestForTransportPass.GetNOCList(Session["UserID"].ToString(), passtype);
                            var list = (from s in requests
                                        where s.party_code == Session["party_code"].ToString() && s.record_status != "N" /*/* && s.financial_year == user.financial_year*/
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
                        //if (Session["rolename"].ToString().Trim() == "Deputy Commissioner")
                        //{

                        //    List<ReaquestForPass> requests = new List<ReaquestForPass>();
                        //    requests = BL_RequestForTransportPass.GetNOCList(Session["UserID"].ToString(), passtype);
                        //    var list = (from s in requests
                        //                where s.record_status == "A" && s.approval_status.Trim() != "Approved"
                        //                orderby s.request_for_pass_id descending
                        //                select s);
                        //    grdNOCList.DataSource = list.ToList();
                        //    grdNOCList.DataBind();
                        //    btnAddRecord.Visible = false;
                        //    foreach (GridViewRow dr1 in grdNOCList.Rows)
                        //    {
                        //        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                        //        btn.Visible = false;
                        //        LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                        //        btn1.Visible = false;
                        //    }


                        //}
                        if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "ENA Distillery Unit")
                        {
                            List<ReaquestForPass> requests = new List<ReaquestForPass>();
                            requests = BL_RequestForTransportPass.GetNOCList(Session["UserID"].ToString(), passtype);
                            var list = (from s in requests
                                        where s.party_code == Session["party_code"].ToString() /*/* && s.financial_year == user.financial_year*/
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
                                if (approved <= lifted)
                                {
                                    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                    btn.Visible = false;
                                }
                                LinkButton btn1 = dr1.FindControl("btnissue") as LinkButton;
                                btn1.Visible = false;
                                string recordstatus = (dr1.FindControl("lblrecordstatus") as Label).Text;
                                if (DateTime.Now.Date > dt && recordstatus != "N")
                                {
                                    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                    LinkButton btn2 = dr1.FindControl("btnEdit") as LinkButton;
                                    LinkButton btn3 = dr1.FindControl("LinkButton1") as LinkButton;
                                    btn3.Visible = false;
                                    btn2.Visible = false;
                                    btn.Visible = false;
                                    lbl.Text = "Expired";
                                    lbl.ForeColor = System.Drawing.Color.Red;
                                }
                            }


                        }
                        if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Sugar Mill")
                        {
                            List<ReaquestForPass> requests = new List<ReaquestForPass>();
                            requests = BL_RequestForTransportPass.GetNOCList(Session["UserID"].ToString(), passtype);
                            var list = (from s in requests
                                        where s.party_code == Session["party_code"].ToString() /*/* && s.financial_year == user.financial_year*/
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
                                if (approved <= lifted)
                                {
                                    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                    btn.Visible = false;
                                }
                                DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                                Label lbl = (dr1.FindControl("lblstatus") as Label);
                                string recordstatus = (dr1.FindControl("lblrecordstatus") as Label).Text;
                                if (DateTime.Now.Date > dt && recordstatus != "N")
                                {
                                    LinkButton btn2 = dr1.FindControl("btnEdit") as LinkButton;
                                    btn2.Visible = false;
                                    LinkButton btn3 = dr1.FindControl("LinkButton1") as LinkButton;
                                    btn3.Visible = false;
                                    lbl.Text = "Expired";
                                    lbl.ForeColor = System.Drawing.Color.Red;
                                }
                            }
                        }
                    }
                }
                //    grdMolassesReleaseRequest.Visible = false;
                //    grdNOCList.Visible = true;
                //    if (Session["rolename"].ToString() == "Bond Officer")
                //    {

                //        List<ReaquestForPass> requests = new List<ReaquestForPass>();
                //        requests = BL_RequestForTransportPass.GetNOCList(Session["UserID"].ToString(), passtype);
                //        var list = (from s in requests
                //                    where s.party_code == Session["party_code"].ToString() && s.record_status != "N"
                //                    orderby s.request_for_pass_id descending
                //                    select s);
                //        grdNOCList.DataSource = list.ToList();
                //        grdNOCList.DataBind();
                //        btnAddRecord.Visible = false;
                //        foreach (GridViewRow dr1 in grdNOCList.Rows)
                //        {
                //            LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                //            btn.Visible = false;
                //            LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                //            btn1.Visible = false;
                //        }


                //    }
                //    if (Session["rolename"].ToString().Trim() == "Deputy Commissioner")
                //    {

                //        List<ReaquestForPass> requests = new List<ReaquestForPass>();
                //        requests = BL_RequestForTransportPass.GetNOCList(Session["UserID"].ToString(), passtype);
                //        var list = (from s in requests
                //                    where  s.record_status == "A" && s.approval_status.Trim() != "Approved"
                //                    orderby s.request_for_pass_id descending
                //                    select s);
                //        grdNOCList.DataSource = list.ToList();
                //        grdNOCList.DataBind();
                //        btnAddRecord.Visible = false;
                //        foreach (GridViewRow dr1 in grdNOCList.Rows)
                //        {
                //            LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                //            btn.Visible = false;
                //            LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                //            btn1.Visible = false;
                //        }


                //    }
                //    if (Session["rolename"].ToString() == "Applicant" &&  Session["usertype"].ToString() == "ENA Distillery Unit")
                //    {
                //        List<ReaquestForPass> requests = new List<ReaquestForPass>();
                //        requests = BL_RequestForTransportPass.GetNOCList(Session["UserID"].ToString(), passtype);
                //        var list = (from s in requests
                //                    where s.party_code == Session["party_code"].ToString()
                //                    orderby s.request_for_pass_id descending
                //                    select s);
                //        grdNOCList.DataSource = list.ToList();
                //        grdNOCList.DataBind();
                //        foreach (GridViewRow dr1 in grdNOCList.Rows)
                //        {
                //            //LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                //            //btn1.Visible = false;
                //            double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                //            double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                //            Label lbl = (dr1.FindControl("lblstatus") as Label);
                //            DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                //            if (approved <= lifted)
                //            {
                //                LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                //                btn.Visible = false;
                //            }
                //            if (DateTime.Now.Date > dt)
                //            {
                //                LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                //                btn.Visible = false;
                //                lbl.Text = "Expired";
                //                lbl.ForeColor = System.Drawing.Color.Red;
                //            }
                //        }


                //    }
                //    if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Sugar Mill")
                //    {
                //        List<ReaquestForPass> requests = new List<ReaquestForPass>();
                //        requests = BL_RequestForTransportPass.GetNOCList(Session["UserID"].ToString(), passtype);
                //        var list = (from s in requests
                //                    where s.party_code == Session["party_code"].ToString()
                //                    orderby s.request_for_pass_id descending
                //                    select s);
                //        grdNOCList.DataSource = list.ToList();
                //        grdNOCList.DataBind();
                //        btnAddRecord.Visible = true;
                //        foreach (GridViewRow dr1 in grdNOCList.Rows)
                //        {
                //            //LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                //            //btn1.Visible = false;
                //            double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                //            double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                //            if (approved <= lifted)
                //            {
                //                LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                //                btn.Visible = false;
                //            }
                //            DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                //            Label lbl = (dr1.FindControl("lblstatus") as Label);
                //            if (DateTime.Now.Date > dt)
                //            {
                //                lbl.Text = "Expired";
                //                lbl.ForeColor = System.Drawing.Color.Red;
                //            }
                //        }
                //    }
                //}
            }
        }

        protected void grdMolassesReleaseRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["UserID"] != null)
            {
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
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
                if (Session["rolename"].ToString() == "Bond Officer")
                {


                    List<ReaquestForPass> requests = new List<ReaquestForPass>();
                    requests = BL_RequestForTransportPass.GetList(Session["UserID"].ToString());
                    var list = (from s in requests
                                where s.toparty_code == Session["party_code"].ToString() && s.record_status != "N" /*/* && s.financial_year == user.financial_year*/
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
                if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Distillery Unit" || Session["usertype"].ToString() == "ENA Distillery Unit")
                {
                    List<ReaquestForPass> requests = new List<ReaquestForPass>();
                    requests = BL_RequestForTransportPass.GetList(Session["UserID"].ToString());
                    var list = (from s in requests
                                where s.party_code == Session["party_code"].ToString() /*/* && s.financial_year == user.financial_year*/
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
                    requests = BL_RequestForTransportPass.GetList(Session["UserID"].ToString());
                    var list = (from s in requests
                                where s.toparty_code == Session["party_code"].ToString() && (s.record_status != "N") /*/* && s.financial_year == user.financial_year*/
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
                        if (approved <= lifted)
                        {
                            LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                            btn.Visible = false;
                        }
                        if (DateTime.Now.Date > dt)
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

        protected void grdNOCList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["UserID"] != null)
            {
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                string passtype = "";
                if (ddPassFor.SelectedValue == "N")
                {
                    passtype = "DOM";
                }
                else
                {
                    passtype = "EXP";
                }
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

               
                if (ddPassFor.SelectedValue == "N")
                {
                    passtype = "DOM";

                    if (Session["rftpsearch"] != null && Session["rftptext"] != null)
                    {
                        ddsearch.SelectedValue = Session["rftpsearch"].ToString();
                        txtpage.Text = Session["rftptext"].ToString();
                        if (ddsearch.SelectedValue != "Select")
                        {

                            if (txtpage != null)
                            {
                                if (ddsearch.SelectedValue == "pass_valid_upto")
                                {
                                    if (txtpage.Text.ToString().Length == 10)
                                    {

                                        requests = BL_RequestForTransportPass.GetpermitList(Session["UserID"].ToString(), passtype);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                        txtpage.Focus();
                                        txtpage.Text = "";
                                        ddsearch.SelectedIndex = 0;

                                        requests = BL_RequestForTransportPass.GetpermitList(Session["UserID"].ToString(), passtype);
                                    }

                                }
                                else
                                {
                                    requests = BL_RequestForTransportPass.Searchpermit("", ddsearch.SelectedValue, txtpage.Text, passtype);
                                }
                            }
                        }
                    }
                    else
                    {
                        requests = BL_RequestForTransportPass.GetpermitList(Session["UserID"].ToString(), passtype);
                    }
                    
                    grdMolassesReleaseRequest.Visible = false;
                    grdNOCList.Visible = true;
                    if (grdNOCList.Rows.Count > 0)
                    {
                        grdNOCList.HeaderRow.Cells[0].Text = "Permit No";
                        grdNOCList.HeaderRow.Cells[5].Text = "Permit Qty";
                    }
                    if (Session["rolename"].ToString() == "Bond Officer")
                    {

                        //List<ReaquestForPass> requests = new List<ReaquestForPass>();
                        //requests = BL_RequestForTransportPass.GetpermitList(Session["UserID"].ToString(), passtype);
                        if (ddsearch.SelectedValue == "pass_valid_upto")
                        {
                            var list = (from s in requests
                                        where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.pass_valid_upto == txtpage.Text /*/* && s.financial_year == user.financial_year*/
                                        orderby s.request_for_pass_id descending
                                        select s);
                            grdNOCList.DataSource = list.ToList();
                            grdNOCList.DataBind();
                        }
                        else
                        {
                            var list = (from s in requests
                                        where s.party_code == Session["party_code"].ToString() && s.record_status != "N" /* /* && s.financial_year == user.financial_year*/
                                        orderby s.request_for_pass_id descending
                                        select s);
                            grdNOCList.DataSource = list.ToList();
                            grdNOCList.DataBind();
                        }

                        if (grdNOCList.Rows.Count > 0)
                        {
                            grdNOCList.HeaderRow.Cells[0].Text = "Permit No";
                            grdNOCList.HeaderRow.Cells[5].Text = "Permit Qty";
                        }
                        btnAddRecord.Visible = false;
                        foreach (GridViewRow dr1 in grdNOCList.Rows)
                        {
                            LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                            btn.Visible = false;
                            LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                            btn1.Visible = false;
                        }


                    }
                    //if (Session["rolename"].ToString().Trim() == "Deputy Commissioner")
                    //{
                    //    if (ddsearch.SelectedValue == "pass_valid_upto")
                    //    {
                    //        var list = (from s in requests
                    //                    where s.record_status == "A" && s.approval_status.Trim() != "Approved" && s.pass_valid_upto == txtpage.Text
                    //                    orderby s.request_for_pass_id descending
                    //                    select s);
                    //        grdNOCList.DataSource = list.ToList();
                    //        grdNOCList.DataBind();
                    //    }
                    //    else
                    //    {

                    //        var list = (from s in requests
                    //                    where s.record_status == "A" && s.approval_status.Trim() != "Approved"
                    //                    orderby s.request_for_pass_id descending
                    //                    select s);
                    //        grdNOCList.DataSource = list.ToList();
                    //        grdNOCList.DataBind();
                    //    }
                    //    if (grdNOCList.Rows.Count > 0)
                    //    {
                    //        grdNOCList.HeaderRow.Cells[0].Text = "Permit No";
                    //        grdNOCList.HeaderRow.Cells[5].Text = "Permit Qty";
                    //    }
                    //    btnAddRecord.Visible = false;
                    //    foreach (GridViewRow dr1 in grdNOCList.Rows)
                    //    {
                    //        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                    //        btn.Visible = false;
                    //        LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                    //        btn1.Visible = false;
                    //    }


                    //}
                    if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "ENA Distillery Unit")
                    {
                        if (ddsearch.SelectedValue == "pass_valid_upto")
                        {
                            var list = (from s in requests
                                        where s.party_code == Session["party_code"].ToString() && s.pass_valid_upto == txtpage.Text /* && s.financial_year == user.financial_year*/
                                        orderby s.request_for_pass_id descending
                                        select s);
                            grdNOCList.DataSource = list.ToList();
                            grdNOCList.DataBind();
                        }
                        else
                        {
                            var list = (from s in requests
                                        where s.party_code == Session["party_code"].ToString() /* && s.financial_year == user.financial_year*/
                                        orderby s.request_for_pass_id descending
                                        select s);
                            grdNOCList.DataSource = list.ToList();
                            grdNOCList.DataBind();
                        }
                        if (grdNOCList.Rows.Count > 0)
                        {
                            grdNOCList.HeaderRow.Cells[0].Text = "Permit No";
                            grdNOCList.HeaderRow.Cells[5].Text = "Permit Qty";
                        }
                        foreach (GridViewRow dr1 in grdNOCList.Rows)
                        {
                            //LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                            //btn1.Visible = false;
                            double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                            double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                            Label lbl = (dr1.FindControl("lblstatus") as Label);
                            DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                            if (approved <= lifted)
                            {
                                LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                btn.Visible = false;
                            }
                            LinkButton btn1 = dr1.FindControl("btnissue") as LinkButton;
                            btn1.Visible = false;
                            string recordstatus = (dr1.FindControl("lblrecordstatus") as Label).Text;
                            if (DateTime.Now.Date > dt && recordstatus != "N")
                            {
                                LinkButton btn2 = dr1.FindControl("btnEdit") as LinkButton;
                                btn2.Visible = false;
                                LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                LinkButton btn3 = dr1.FindControl("LinkButton1") as LinkButton;
                                btn3.Visible = false;
                                btn.Visible = false;
                                lbl.Text = "Expired";
                                lbl.ForeColor = System.Drawing.Color.Red;
                            }
                        }


                    }
                    if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Sugar Mill")
                    {
                        if (ddsearch.SelectedValue == "pass_valid_upto")
                        {
                            var list = (from s in requests
                                        where s.party_code == Session["party_code"].ToString() && s.pass_valid_upto == txtpage.Text /* && s.financial_year == user.financial_year*/
                                        orderby s.request_for_pass_id descending
                                        select s);
                            grdNOCList.DataSource = list.ToList();
                            grdNOCList.DataBind();
                        }
                        else
                        {
                            var list = (from s in requests
                                        where s.party_code == Session["party_code"].ToString() /* && s.financial_year == user.financial_year*/
                                        orderby s.request_for_pass_id descending
                                        select s);
                            grdNOCList.DataSource = list.ToList();
                            grdNOCList.DataBind();
                        }
                        if (grdNOCList.Rows.Count > 0)
                        {
                            grdNOCList.HeaderRow.Cells[0].Text = "Permit No";
                            grdNOCList.HeaderRow.Cells[5].Text = "Permit Qty";
                        }
                        btnAddRecord.Visible = true;
                        foreach (GridViewRow dr1 in grdNOCList.Rows)
                        {
                            //LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                            //btn1.Visible = false;
                            double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                            double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                            if (approved <= lifted)
                            {
                                LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                btn.Visible = false;
                            }
                            DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                            Label lbl = (dr1.FindControl("lblstatus") as Label);
                            string recordstatus = (dr1.FindControl("lblrecordstatus") as Label).Text;
                            if (DateTime.Now.Date > dt && recordstatus != "N")
                            {
                                LinkButton btn2 = dr1.FindControl("btnEdit") as LinkButton;
                                btn2.Visible = false;
                                LinkButton btn3 = dr1.FindControl("LinkButton1") as LinkButton;
                                btn3.Visible = false;
                                lbl.Text = "Expired";
                                lbl.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }

                else
                {
                    passtype = "EXP";
                    if (Session["rftpsearch"] != null && Session["rftptext"] != null)
                    {
                        ddsearch.SelectedValue = Session["rftpsearch"].ToString();
                        txtpage.Text = Session["rftptext"].ToString();
                        if (ddsearch.SelectedValue != "Select")
                        {

                            if (txtpage != null)
                            {
                                if (ddsearch.SelectedValue == "valied_date")
                                {
                                    if (txtpage.Text.ToString().Length == 10)
                                    {

                                        requests = BL_RequestForTransportPass.GetNOCList(Session["UserID"].ToString(), passtype);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                        txtpage.Focus();
                                        txtpage.Text = "";
                                        ddsearch.SelectedIndex = 0;

                                        requests = BL_RequestForTransportPass.GetNOCList(Session["UserID"].ToString(), passtype);
                                    }

                                }
                                else
                                {
                                    requests = BL_RequestForTransportPass.Searchena("", ddsearch.SelectedValue, txtpage.Text, passtype);
                                }
                            }
                        }
                    }
                    else
                    {
                        requests = BL_RequestForTransportPass.GetNOCList(Session["UserID"].ToString(), passtype);
                    }
                   
                    grdMolassesReleaseRequest.Visible = false;
                    grdNOCList.Visible = true;
                    if (Session["rolename"].ToString() == "Bond Officer")
                    {

                        //List<ReaquestForPass> requests = new List<ReaquestForPass>();
                        //requests = BL_RequestForTransportPass.GetNOCList(Session["UserID"].ToString(), passtype);
                        if (ddsearch.SelectedValue == "pass_valid_upto")
                        {
                            var list = (from s in requests
                                        where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.pass_valid_upto == txtpage.Text /* && s.financial_year == user.financial_year*/
                                        orderby s.request_for_pass_id descending
                                        select s);
                            grdNOCList.DataSource = list.ToList();
                            grdNOCList.DataBind();
                        }
                        else
                        {
                            var list = (from s in requests
                                        where s.party_code == Session["party_code"].ToString() && s.record_status != "N" /* && s.financial_year == user.financial_year*/
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
                            LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                            btn1.Visible = false;
                        }


                    }
                    //if (Session["rolename"].ToString().Trim() == "Deputy Commissioner")
                    //{
                    //    if (ddsearch.SelectedValue == "pass_valid_upto")
                    //    {
                    //        var list = (from s in requests
                    //                    where s.record_status == "A" && s.approval_status.Trim() != "Approved" && s.pass_valid_upto == txtpage.Text
                    //                    orderby s.request_for_pass_id descending
                    //                    select s);
                    //        grdNOCList.DataSource = list.ToList();
                    //        grdNOCList.DataBind();
                    //    }
                    //    else
                    //    {
                    //        var list = (from s in requests
                    //                    where s.record_status == "A" && s.approval_status.Trim() != "Approved"
                    //                    orderby s.request_for_pass_id descending
                    //                    select s);
                    //        grdNOCList.DataSource = list.ToList();
                    //        grdNOCList.DataBind();
                    //    }
                    //    btnAddRecord.Visible = false;
                    //    foreach (GridViewRow dr1 in grdNOCList.Rows)
                    //    {
                    //        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                    //        btn.Visible = false;
                    //        LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                    //        btn1.Visible = false;
                    //    }


                    //}
                    if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "ENA Distillery Unit")
                    {
                        if (ddsearch.SelectedValue == "pass_valid_upto")
                        {
                            var list = (from s in requests
                                        where s.party_code == Session["party_code"].ToString() && s.pass_valid_upto == txtpage.Text /* && s.financial_year == user.financial_year*/
                                        orderby s.request_for_pass_id descending
                                        select s);
                            grdNOCList.DataSource = list.ToList();
                            grdNOCList.DataBind();
                        }
                        else
                        {
                            var list = (from s in requests
                                        where s.party_code == Session["party_code"].ToString() /* && s.financial_year == user.financial_year*/
                                        orderby s.request_for_pass_id descending
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
                            DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                            if (approved <= lifted)
                            {
                                LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                btn.Visible = false;
                            }
                            LinkButton btn1 = dr1.FindControl("btnissue") as LinkButton;
                            btn1.Visible = false;
                            string recordstatus = (dr1.FindControl("lblrecordstatus") as Label).Text;
                            if (DateTime.Now.Date > dt && recordstatus != "N")
                            {
                                LinkButton btn2 = dr1.FindControl("btnEdit") as LinkButton;
                                btn2.Visible = false;
                                LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                btn.Visible = false;
                                LinkButton btn3 = dr1.FindControl("LinkButton1") as LinkButton;
                                btn3.Visible = false;
                                lbl.Text = "Expired";
                                lbl.ForeColor = System.Drawing.Color.Red;
                            }
                        }


                    }
                    if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Sugar Mill")
                    {
                        if (ddsearch.SelectedValue == "pass_valid_upto")
                        {
                            var list = (from s in requests
                                        where s.party_code == Session["party_code"].ToString() && s.pass_valid_upto == txtpage.Text /* && s.financial_year == user.financial_year*/
                                        orderby s.request_for_pass_id descending
                                        select s);
                            grdNOCList.DataSource = list.ToList();
                            grdNOCList.DataBind();
                        }
                        else
                        {
                            var list = (from s in requests
                                        where s.party_code == Session["party_code"].ToString() /* && s.financial_year == user.financial_year*/
                                        orderby s.request_for_pass_id descending
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
                            if (approved <= lifted)
                            {
                                LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                btn.Visible = false;
                            }
                            DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                            Label lbl = (dr1.FindControl("lblstatus") as Label);
                            string recordstatus = (dr1.FindControl("lblrecordstatus") as Label).Text;
                            if (DateTime.Now.Date > dt && recordstatus != "N")
                            {
                                LinkButton btn2 = dr1.FindControl("btnEdit") as LinkButton;
                                btn2.Visible = false;
                                LinkButton btn3 = dr1.FindControl("LinkButton1") as LinkButton;
                                btn3.Visible = false;
                                lbl.Text = "Expired";
                                lbl.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }
            }
        }
        protected void btnprint_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer; 
            string RequestID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrequest_for_pass_id") as Label).Text;
            string passtype = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpasstype") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            Session["rftptfinancial_year"] = financial_year;
            List<Reportmaster> reports = new List<Reportmaster>();
            reports = BL_WorkFlow.GetReports(Session["UserID"].ToString());
            if(passtype=="DOM")
            {
                Session["ReportId"] = "ena_form46_domestic";
            }
            else
            {
                Session["ReportId"] = "ena_form46";
            }
            
            Session["Pass_Type"] = passtype;
            Session["UserID"] = Session["UserID"].ToString();
            Session["Pass_ID"] = RequestID;
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('ReportPage' ,'_blank');", true);
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

            if (Session["rftpsearch"] != null && Session["rftptext"] != null)
            {
                ddsearch.SelectedValue = Session["rftpsearch"].ToString();
                txtpages.Text = Session["rftptext"].ToString();
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
        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdMolassesReleaseRequest.TopPagerRow;
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
            else if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "ENA Distillery Unit")
            {


                List<ReaquestForPass> requests = new List<ReaquestForPass>();
                requests = BL_RequestForTransportPass.GetList(Session["UserID"].ToString());
                var list = (from s in requests
                            where s.party_code == Session["party_code"].ToString() /* && s.financial_year == user.financial_year*/
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
                    LinkButton btn1 = dr1.FindControl("btnissue") as LinkButton;
                    btn1.Visible = false;
                    if (DateTime.Now.Date > dt)
                    {
                        lbl.Text = "Expired";
                        lbl.ForeColor = System.Drawing.Color.Red;
                    }
                }


            }
            else if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Sugar Mill")
            {

                btnAddRecord.Visible = false;
                List<ReaquestForPass> requests = new List<ReaquestForPass>();
                requests = BL_RequestForTransportPass.GetList(Session["UserID"].ToString());
                var list = (from s in requests
                            where s.toparty_code == Session["party_code"].ToString() && (s.record_status != "N") /* && s.financial_year == user.financial_year*/
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
                    DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                    Label lbl = (dr1.FindControl("lblstatus") as Label);
                    if (approved <= lifted)
                    {
                        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                        btn.Visible = false;
                    }
                    LinkButton btn2 = dr1.FindControl("btnissue") as LinkButton;
                    btn2.Visible = false;
                    if (DateTime.Now.Date > dt)
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
                grdNOCList.PageIndex = a - 1;
            }
            else
            {
                grdNOCList.PageIndex = a;
            }
            string userid = Session["UserID"].ToString();
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            grdNOCList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
            string passtype = "";
            if (ddPassFor.SelectedValue == "N")
            {
                passtype = "DOM";
                grdMolassesReleaseRequest.Visible = false;
                grdNOCList.Visible = true;
                if (grdNOCList.Rows.Count > 0)
                {
                    grdNOCList.HeaderRow.Cells[0].Text = "Permit No";
                    grdNOCList.HeaderRow.Cells[5].Text = "Permit Qty";
                }
                if (Session["rolename"].ToString() == "Bond Officer")
                {

                    List<ReaquestForPass> requests = new List<ReaquestForPass>();
                    requests = BL_RequestForTransportPass.GetpermitList(Session["UserID"].ToString(), passtype);
                    var list = (from s in requests
                                where s.party_code == Session["party_code"].ToString() && s.record_status != "N" /* && s.financial_year == user.financial_year*/
                                orderby s.request_for_pass_id descending
                                select s);
                    grdNOCList.DataSource = list.ToList();
                    grdNOCList.DataBind();
                    if (grdNOCList.Rows.Count > 0)
                    {
                        grdNOCList.HeaderRow.Cells[0].Text = "Permit No";
                        grdNOCList.HeaderRow.Cells[5].Text = "Permit Qty";
                    }
                    btnAddRecord.Visible = false;
                    foreach (GridViewRow dr1 in grdNOCList.Rows)
                    {
                        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                        btn.Visible = false;
                        LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                        btn1.Visible = false;
                    }


                }
                //if (Session["rolename"].ToString().Trim() == "Deputy Commissioner")
                //{

                //    List<ReaquestForPass> requests = new List<ReaquestForPass>();
                //    requests = BL_RequestForTransportPass.GetpermitList(Session["UserID"].ToString(), passtype);
                //    var list = (from s in requests
                //                where s.record_status == "A" && s.approval_status.Trim() != "Approved"
                //                orderby s.request_for_pass_id descending
                //                select s);
                //    grdNOCList.DataSource = list.ToList();
                //    grdNOCList.DataBind();
                //    if (grdNOCList.Rows.Count > 0)
                //    {
                //        grdNOCList.HeaderRow.Cells[0].Text = "Permit No";
                //        grdNOCList.HeaderRow.Cells[5].Text = "Permit Qty";
                //    }
                //    btnAddRecord.Visible = false;
                //    foreach (GridViewRow dr1 in grdNOCList.Rows)
                //    {
                //        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                //        btn.Visible = false;
                //        LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                //        btn1.Visible = false;
                //    }


                //}
                if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "ENA Distillery Unit")
                {
                    List<ReaquestForPass> requests = new List<ReaquestForPass>();
                    requests = BL_RequestForTransportPass.GetpermitList(Session["UserID"].ToString(), passtype);
                    var list = (from s in requests
                                where s.party_code == Session["party_code"].ToString() /* && s.financial_year == user.financial_year*/
                                orderby s.request_for_pass_id descending
                                select s);
                    grdNOCList.DataSource = list.ToList();
                    grdNOCList.DataBind();
                    if (grdNOCList.Rows.Count > 0)
                    {
                        grdNOCList.HeaderRow.Cells[0].Text = "Permit No";
                        grdNOCList.HeaderRow.Cells[5].Text = "Permit Qty";
                    }
                    foreach (GridViewRow dr1 in grdNOCList.Rows)
                    {
                        //LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                        //btn1.Visible = false;
                        double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                        double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                        Label lbl = (dr1.FindControl("lblstatus") as Label);
                        DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                        if (approved <= lifted)
                        {
                            LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                            btn.Visible = false;
                        }
                        LinkButton btn1 = dr1.FindControl("btnissue") as LinkButton;
                        btn1.Visible = false;
                        string recordstatus = (dr1.FindControl("lblrecordstatus") as Label).Text;
                        if (DateTime.Now.Date > dt && recordstatus != "N")
                        {
                            LinkButton btn2 = dr1.FindControl("btnEdit") as LinkButton;
                            btn2.Visible = false;
                            LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                            btn.Visible = false;
                            LinkButton btn3 = dr1.FindControl("LinkButton1") as LinkButton;
                            btn3.Visible = false;
                            lbl.Text = "Expired";
                            lbl.ForeColor = System.Drawing.Color.Red;
                        }
                    }


                }
                if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Sugar Mill")
                {
                    List<ReaquestForPass> requests = new List<ReaquestForPass>();
                    requests = BL_RequestForTransportPass.GetpermitList(Session["UserID"].ToString(), passtype);
                    var list = (from s in requests
                                where s.party_code == Session["party_code"].ToString() /* && s.financial_year == user.financial_year*/  
                                orderby s.request_for_pass_id descending
                                select s);
                    grdNOCList.DataSource = list.ToList();
                    grdNOCList.DataBind();
                    if (grdNOCList.Rows.Count > 0)
                    {
                        grdNOCList.HeaderRow.Cells[0].Text = "Permit No";
                        grdNOCList.HeaderRow.Cells[5].Text = "Permit Qty";
                    }
                    btnAddRecord.Visible = true;
                    foreach (GridViewRow dr1 in grdNOCList.Rows)
                    {
                        //LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                        //btn1.Visible = false;
                        double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                        double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                        if (approved <= lifted)
                        {
                            LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                            btn.Visible = false;
                        }
                        DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                        Label lbl = (dr1.FindControl("lblstatus") as Label);
                        string recordstatus = (dr1.FindControl("lblrecordstatus") as Label).Text;
                        if (DateTime.Now.Date > dt && recordstatus != "N")
                        {
                            LinkButton btn2 = dr1.FindControl("btnEdit") as LinkButton;
                            btn2.Visible = false;
                            LinkButton btn3 = dr1.FindControl("LinkButton1") as LinkButton;
                            btn3.Visible = false;
                            lbl.Text = "Expired";
                            lbl.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }

            else
            {
                passtype = "EXP";
                grdMolassesReleaseRequest.Visible = false;
                grdNOCList.Visible = true;
                if (Session["rolename"].ToString() == "Bond Officer")
                {

                    List<ReaquestForPass> requests = new List<ReaquestForPass>();
                    requests = BL_RequestForTransportPass.GetNOCList(Session["UserID"].ToString(), passtype);
                    var list = (from s in requests
                                where s.party_code == Session["party_code"].ToString() && s.record_status != "N" /* && s.financial_year == user.financial_year*/
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
                //if (Session["rolename"].ToString().Trim() == "Deputy Commissioner")
                //{

                //    List<ReaquestForPass> requests = new List<ReaquestForPass>();
                //    requests = BL_RequestForTransportPass.GetNOCList(Session["UserID"].ToString(), passtype);
                //    var list = (from s in requests
                //                where s.record_status == "A" && s.approval_status.Trim() != "Approved"
                //                orderby s.request_for_pass_id descending
                //                select s);
                //    grdNOCList.DataSource = list.ToList();
                //    grdNOCList.DataBind();
                //    btnAddRecord.Visible = false;
                //    foreach (GridViewRow dr1 in grdNOCList.Rows)
                //    {
                //        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                //        btn.Visible = false;
                //        LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                //        btn1.Visible = false;
                //    }


                //}
                if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "ENA Distillery Unit")
                {
                    List<ReaquestForPass> requests = new List<ReaquestForPass>();
                    requests = BL_RequestForTransportPass.GetNOCList(Session["UserID"].ToString(), passtype);
                    var list = (from s in requests
                                where s.party_code == Session["party_code"].ToString() /* && s.financial_year == user.financial_year*/
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
                        if (approved <= lifted)
                        {
                            LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                            btn.Visible = false;
                        }
                        LinkButton btn1 = dr1.FindControl("btnissue") as LinkButton;
                        btn1.Visible = false;
                        string recordstatus = (dr1.FindControl("lblrecordstatus") as Label).Text;
                        if (DateTime.Now.Date > dt && recordstatus != "N")
                        {
                            LinkButton btn2 = dr1.FindControl("btnEdit") as LinkButton;
                            btn2.Visible = false;
                            LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                            btn.Visible = false;
                            LinkButton btn3 = dr1.FindControl("LinkButton1") as LinkButton;
                            btn3.Visible = false;
                            lbl.Text = "Expired";
                            lbl.ForeColor = System.Drawing.Color.Red;
                        }
                    }


                }
                if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Sugar Mill")
                {
                    List<ReaquestForPass> requests = new List<ReaquestForPass>();
                    requests = BL_RequestForTransportPass.GetNOCList(Session["UserID"].ToString(), passtype);
                    var list = (from s in requests
                                where s.party_code == Session["party_code"].ToString() /* && s.financial_year == user.financial_year*/
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
                        if (approved <= lifted)
                        {
                            LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                            btn.Visible = false;
                        }
                        LinkButton btn1 = dr1.FindControl("btnissue") as LinkButton;
                        btn1.Visible = false;
                        DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                        Label lbl = (dr1.FindControl("lblstatus") as Label);
                        string recordstatus = (dr1.FindControl("lblrecordstatus") as Label).Text;
                        if (DateTime.Now.Date > dt && recordstatus != "N")
                        {
                            LinkButton btn2 = dr1.FindControl("btnEdit") as LinkButton;
                            btn2.Visible = false;
                            LinkButton btn3 = dr1.FindControl("LinkButton1") as LinkButton;
                            btn3.Visible = false;
                            lbl.Text = "Expired";
                            lbl.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }


        }
        protected void btnsearch_Click1(object sender, EventArgs e)
        {
            GridViewRow row = grdNOCList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {
                Session["rftpsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["rftptext"] = txtpage.Text;
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        grdNOCList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                        string passtype = "";
                        if (ddPassFor.SelectedValue == "N")
                        {
                            passtype = "DOM";
                            if (ddsearch.SelectedValue == "pass_valid_upto")
                            {
                                if (txtpage.Text.ToString().Length == 10)
                                {
                                    requests = BL_RequestForTransportPass.GetpermitList(Session["UserID"].ToString(), passtype);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                    txtpage.Focus();
                                    txtpage.Text = "";
                                    ddsearch.SelectedIndex = 0;
                                    requests = BL_RequestForTransportPass.GetpermitList(Session["UserID"].ToString(), passtype);
                                }
                            }
                            else
                            {
                                requests = BL_RequestForTransportPass.Searchpermit("", ddsearch.SelectedValue, txtpage.Text,passtype);
                            }
                            grdMolassesReleaseRequest.Visible = false;
                            grdNOCList.Visible = true;
                            if (grdNOCList.Rows.Count > 0)
                            {
                                grdNOCList.HeaderRow.Cells[0].Text = "Permit No";
                                grdNOCList.HeaderRow.Cells[5].Text = "Permit Qty";
                            }
                            if (Session["rolename"].ToString() == "Bond Officer")
                            {

                                //List<ReaquestForPass> requests = new List<ReaquestForPass>();
                                //requests = BL_RequestForTransportPass.GetpermitList(Session["UserID"].ToString(), passtype);
                                if (ddsearch.SelectedValue == "pass_valid_upto")
                                {
                                    var list = (from s in requests
                                                where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.pass_valid_upto==txtpage.Text /* && s.financial_year == user.financial_year*/
                                                orderby s.request_for_pass_id descending
                                                select s);
                                    grdNOCList.DataSource = list.ToList();
                                    grdNOCList.DataBind();
                                }
                                else
                                {
                                    var list = (from s in requests
                                                where s.party_code == Session["party_code"].ToString() && s.record_status != "N" /* && s.financial_year == user.financial_year*/
                                                orderby s.request_for_pass_id descending
                                                select s);
                                    grdNOCList.DataSource = list.ToList();
                                    grdNOCList.DataBind();
                                }
                                   
                                if (grdNOCList.Rows.Count > 0)
                                {
                                    grdNOCList.HeaderRow.Cells[0].Text = "Permit No";
                                    grdNOCList.HeaderRow.Cells[5].Text = "Permit Qty";
                                }
                                btnAddRecord.Visible = false;
                                foreach (GridViewRow dr1 in grdNOCList.Rows)
                                {
                                    LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                    btn.Visible = false;
                                    LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                                    btn1.Visible = false;
                                }


                            }
                            //if (Session["rolename"].ToString().Trim() == "Deputy Commissioner")
                            //{
                            //    if (ddsearch.SelectedValue == "pass_valid_upto")
                            //    {
                            //        var list = (from s in requests
                            //                    where s.record_status == "A" && s.approval_status.Trim() != "Approved" && s.pass_valid_upto == txtpage.Text
                            //                    orderby s.request_for_pass_id descending
                            //                    select s);
                            //        grdNOCList.DataSource = list.ToList();
                            //        grdNOCList.DataBind();
                            //    }
                            //    else
                            //    {

                            //        var list = (from s in requests
                            //                    where s.record_status == "A" && s.approval_status.Trim() != "Approved"
                            //                    orderby s.request_for_pass_id descending
                            //                    select s);
                            //        grdNOCList.DataSource = list.ToList();
                            //        grdNOCList.DataBind();
                            //    }
                            //    if (grdNOCList.Rows.Count > 0)
                            //    {
                            //        grdNOCList.HeaderRow.Cells[0].Text = "Permit No";
                            //        grdNOCList.HeaderRow.Cells[5].Text = "Permit Qty";
                            //    }
                            //    btnAddRecord.Visible = false;
                            //    foreach (GridViewRow dr1 in grdNOCList.Rows)
                            //    {
                            //        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                            //        btn.Visible = false;
                            //        LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                            //        btn1.Visible = false;
                            //    }


                            //}
                            if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "ENA Distillery Unit")
                            {
                                if (ddsearch.SelectedValue == "pass_valid_upto")
                                {
                                    var list = (from s in requests
                                                where s.party_code == Session["party_code"].ToString() && s.pass_valid_upto == txtpage.Text /* && s.financial_year == user.financial_year*/
                                                orderby s.request_for_pass_id descending
                                                select s);
                                    grdNOCList.DataSource = list.ToList();
                                    grdNOCList.DataBind();
                                }
                                else
                                {
                                    var list = (from s in requests
                                                where s.party_code == Session["party_code"].ToString() /* && s.financial_year == user.financial_year*/
                                                orderby s.request_for_pass_id descending
                                                select s);
                                    grdNOCList.DataSource = list.ToList();
                                    grdNOCList.DataBind();
                                }
                                if (grdNOCList.Rows.Count > 0)
                                {
                                    grdNOCList.HeaderRow.Cells[0].Text = "Permit No";
                                    grdNOCList.HeaderRow.Cells[5].Text = "Permit Qty";
                                }
                                foreach (GridViewRow dr1 in grdNOCList.Rows)
                                {
                                    //LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                                    //btn1.Visible = false;
                                    double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                                    double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                                    Label lbl = (dr1.FindControl("lblstatus") as Label);
                                    DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                                    if (approved <= lifted)
                                    {
                                        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                        btn.Visible = false;
                                    }
                                    LinkButton btn1 = dr1.FindControl("btnissue") as LinkButton;
                                    btn1.Visible = false;
                                    string recordstatus = (dr1.FindControl("lblrecordstatus") as Label).Text;
                                    if (DateTime.Now.Date > dt && recordstatus != "N")
                                    {
                                        LinkButton btn2 = dr1.FindControl("btnEdit") as LinkButton;
                                        btn2.Visible = false;
                                        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                        btn.Visible = false;
                                        lbl.Text = "Expired";
                                        LinkButton btn3 = dr1.FindControl("LinkButton1") as LinkButton;
                                        btn3.Visible = false;
                                        lbl.ForeColor = System.Drawing.Color.Red;
                                    }
                                }


                            }
                            if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Sugar Mill")
                            {
                                if (ddsearch.SelectedValue == "pass_valid_upto")
                                {
                                    var list = (from s in requests
                                                where s.party_code == Session["party_code"].ToString() && s.pass_valid_upto == txtpage.Text /* && s.financial_year == user.financial_year*/
                                                orderby s.request_for_pass_id descending
                                                select s);
                                    grdNOCList.DataSource = list.ToList();
                                    grdNOCList.DataBind();
                                }
                                else
                                {
                                    var list = (from s in requests
                                                where s.party_code == Session["party_code"].ToString() /* && s.financial_year == user.financial_year*/
                                                orderby s.request_for_pass_id descending
                                                select s);
                                    grdNOCList.DataSource = list.ToList();
                                    grdNOCList.DataBind();
                                }
                                if (grdNOCList.Rows.Count > 0)
                                {
                                    grdNOCList.HeaderRow.Cells[0].Text = "Permit No";
                                    grdNOCList.HeaderRow.Cells[5].Text = "Permit Qty";
                                }
                                btnAddRecord.Visible = true;
                                foreach (GridViewRow dr1 in grdNOCList.Rows)
                                {
                                    //LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                                    //btn1.Visible = false;
                                    double approved = Convert.ToDouble((dr1.FindControl("PassApprovedQty") as Label).Text);
                                    double lifted = Convert.ToDouble((dr1.FindControl("PassLiftedQty") as Label).Text);
                                    if (approved <= lifted)
                                    {
                                        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                        btn.Visible = false;
                                    }
                                    LinkButton btn1 = dr1.FindControl("btnissue") as LinkButton;
                                    btn1.Visible = false;
                                    DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                                    Label lbl = (dr1.FindControl("lblstatus") as Label);
                                    string recordstatus = (dr1.FindControl("lblrecordstatus") as Label).Text;
                                    if (DateTime.Now.Date > dt && recordstatus != "N")
                                    {
                                        LinkButton btn2 = dr1.FindControl("btnEdit") as LinkButton;
                                        btn2.Visible = false;
                                        lbl.Text = "Expired";
                                        LinkButton btn3 = dr1.FindControl("LinkButton1") as LinkButton;
                                        btn3.Visible = false;
                                        lbl.ForeColor = System.Drawing.Color.Red;
                                    }
                                }
                            }
                        }

                        else
                        {
                            passtype = "EXP";
                            if (ddsearch.SelectedValue == "pass_valid_upto")
                            {
                                if (txtpage.Text.ToString().Length == 10)
                                {
                                    requests = BL_RequestForTransportPass.GetNOCList(Session["UserID"].ToString(), passtype);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                    txtpage.Focus();
                                    txtpage.Text = "";
                                    ddsearch.SelectedIndex = 0;
                                    requests = BL_RequestForTransportPass.GetNOCList(Session["UserID"].ToString(), passtype);
                                }
                            }
                            else
                            {
                                requests = BL_RequestForTransportPass.Searchena("", ddsearch.SelectedValue, txtpage.Text,passtype);
                            }
                            grdMolassesReleaseRequest.Visible = false;
                            grdNOCList.Visible = true;
                            if (Session["rolename"].ToString() == "Bond Officer")
                            {

                                //List<ReaquestForPass> requests = new List<ReaquestForPass>();
                                //requests = BL_RequestForTransportPass.GetNOCList(Session["UserID"].ToString(), passtype);
                                if (ddsearch.SelectedValue == "pass_valid_upto")
                                {
                                    var list = (from s in requests
                                                where s.party_code == Session["party_code"].ToString() && s.record_status != "N" && s.pass_valid_upto==txtpage.Text /* && s.financial_year == user.financial_year*/
                                                orderby s.request_for_pass_id descending
                                                select s);
                                    grdNOCList.DataSource = list.ToList();
                                    grdNOCList.DataBind();
                                }
                                else
                                {
                                    var list = (from s in requests
                                                where s.party_code == Session["party_code"].ToString() && s.record_status != "N" /* && s.financial_year == user.financial_year*/
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
                                    LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                                    btn1.Visible = false;
                                }


                            }
                            //if (Session["rolename"].ToString().Trim() == "Deputy Commissioner")
                            //{
                            //    if (ddsearch.SelectedValue == "pass_valid_upto")
                            //    {
                            //        var list = (from s in requests
                            //                    where s.record_status == "A" && s.approval_status.Trim() != "Approved" && s.pass_valid_upto == txtpage.Text
                            //                    orderby s.request_for_pass_id descending
                            //                    select s);
                            //        grdNOCList.DataSource = list.ToList();
                            //        grdNOCList.DataBind();
                            //    }
                            //    else
                            //    {
                            //        var list = (from s in requests
                            //                    where s.record_status == "A" && s.approval_status.Trim() != "Approved"
                            //                    orderby s.request_for_pass_id descending
                            //                    select s);
                            //        grdNOCList.DataSource = list.ToList();
                            //        grdNOCList.DataBind();
                            //    }
                            //    btnAddRecord.Visible = false;
                            //    foreach (GridViewRow dr1 in grdNOCList.Rows)
                            //    {
                            //        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                            //        btn.Visible = false;
                            //        LinkButton btn1 = dr1.FindControl("btnEdit") as LinkButton;
                            //        btn1.Visible = false;
                            //    }


                            //}
                            if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "ENA Distillery Unit")
                            {
                                if (ddsearch.SelectedValue == "pass_valid_upto")
                                {
                                    var list = (from s in requests
                                                where s.party_code == Session["party_code"].ToString() && s.pass_valid_upto == txtpage.Text /* && s.financial_year == user.financial_year*/
                                                orderby s.request_for_pass_id descending
                                                select s);
                                    grdNOCList.DataSource = list.ToList();
                                    grdNOCList.DataBind();
                                }
                                else
                                {
                                    var list = (from s in requests
                                                where s.party_code == Session["party_code"].ToString() /* && s.financial_year == user.financial_year*/
                                                orderby s.request_for_pass_id descending
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
                                    DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                                    if (approved <= lifted)
                                    {
                                        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                        btn.Visible = false;
                                    }
                                    LinkButton btn1 = dr1.FindControl("btnissue") as LinkButton;
                                    btn1.Visible = false;
                                    string recordstatus = (dr1.FindControl("lblrecordstatus") as Label).Text;
                                    if (DateTime.Now.Date > dt && recordstatus != "N")
                                    {
                                        LinkButton btn2 = dr1.FindControl("btnEdit") as LinkButton;
                                        btn2.Visible = false;
                                        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                        btn.Visible = false;
                                        LinkButton btn3 = dr1.FindControl("LinkButton1") as LinkButton;
                                        btn3.Visible = false;
                                        lbl.Text = "Expired";
                                        lbl.ForeColor = System.Drawing.Color.Red;
                                    }
                                }


                            }
                            if (Session["rolename"].ToString() == "Applicant" && Session["usertype"].ToString() == "Sugar Mill")
                            {
                                if (ddsearch.SelectedValue == "pass_valid_upto")
                                {
                                    var list = (from s in requests
                                                where s.party_code == Session["party_code"].ToString() && s.pass_valid_upto == txtpage.Text /* && s.financial_year == user.financial_year*/
                                                orderby s.request_for_pass_id descending
                                                select s);
                                    grdNOCList.DataSource = list.ToList();
                                    grdNOCList.DataBind();
                                }
                                else
                                {
                                    var list = (from s in requests
                                                where s.party_code == Session["party_code"].ToString() /* && s.financial_year == user.financial_year*/
                                                orderby s.request_for_pass_id descending
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
                                    if (approved <= lifted)
                                    {
                                        LinkButton btn = dr1.FindControl("btnApply") as LinkButton;
                                        btn.Visible = false;
                                    }
                                    LinkButton btn1 = dr1.FindControl("btnissue") as LinkButton;
                                    btn1.Visible = false;
                                    DateTime dt = Convert.ToDateTime((dr1.FindControl("lblValidUpto") as Label).Text);
                                    Label lbl = (dr1.FindControl("lblstatus") as Label);
                                    string recordstatus = (dr1.FindControl("lblrecordstatus") as Label).Text;
                                    if (DateTime.Now.Date > dt && recordstatus != "N")
                                    {
                                        LinkButton btn2 = dr1.FindControl("btnEdit") as LinkButton;
                                        btn2.Visible = false;
                                        lbl.Text = "Expired";
                                        LinkButton btn3 = dr1.FindControl("LinkButton1") as LinkButton;
                                        btn3.Visible = false;
                                        lbl.ForeColor = System.Drawing.Color.Red;
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
            Session["rftpsearch"] = null;
            Session["rftptext"] = null;
            ddPassFor_SelectedIndexChanged(sender, e);
        }
    }
}