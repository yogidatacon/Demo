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
    public partial class RequestForPassForm : System.Web.UI.Page
    {
        //UserDetails user = new UserDetails();
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
                    if (user!= null)
                    {
                        //List<Release_Request> rr = new List<Release_Request>();
                        //rr = BL_Release_Request.GetRRList();
                        passtype.Value = Session["ptype"].ToString();
                        Session["financial_year"] = user.financial_year;
                        Session["rparty_code"] = user.party_code;
                        if (Session["rtype"].ToString() == "0")
                        {
                            Session["rfptfinancial_year"] = Session["financial_year"].ToString();
                        }
                        if (Session["ptype"].ToString() == "M")
                        {
                            List<Release_Request> rr = new List<Release_Request>();
                            rr = BL_Release_Request.GetRRList();
                            if (rr.Count > 0)
                            {
                                depot.Visible = false;
                                nocl.Visible = false;
                                rrl.Visible = true;
                                if (user.role_name == "Bond Officer")
                                {
                                    var list = (from s in rr
                                                where s.from_party == user.party_code && s.record_status == "I" && Convert.ToDateTime(s.valid_date) > DateTime.Now.Date && s.financial_year == Session["rfptfinancial_year"].ToString()
                                                select s);
                                    ddlReleaseRequestNo.DataSource = list;
                                    ddlReleaseRequestNo.DataTextField = "rr_issueno";
                                    ddlReleaseRequestNo.DataValueField = "release_request_id";
                                    ddlReleaseRequestNo.DataBind();
                                    party_code.Value = list.ToList()[0].party_code;
                                    ddlReleaseRequestNo.Items.Insert(0, "Select");
                                }
                                if (user.role_name == "Applicant" && user.party_type == "Distillery Unit")
                                {
                                    if (Session["rtype"].ToString() == "0")
                                    {
                                        var list = (from s in rr
                                                    where s.party_code == user.party_code && s.record_status == "I" && s.rr_lifted_qty < s.rr_quantity &&  Convert.ToDateTime(s.valid_date)>=DateTime.Now.Date && s.financial_year == Session["rfptfinancial_year"].ToString()
                                                    select s);
                                        if (list.ToList().Count > 0)
                                        {
                                            ddlReleaseRequestNo.DataSource = list;
                                            ddlReleaseRequestNo.DataTextField = "rr_issueno";
                                            ddlReleaseRequestNo.DataValueField = "release_request_id";
                                            ddlReleaseRequestNo.DataBind();
                                            party_code.Value = list.ToList()[0].party_code;
                                            ddlReleaseRequestNo.Items.Insert(0, "Select");
                                        }
                                    }
                                    else
                                    {
                                        var list = (from s in rr
                                                    where s.party_code == user.party_code && s.record_status == "I" && s.financial_year == Session["rfptfinancial_year"].ToString()
                                                    select s);
                                        if (list.ToList().Count > 0)
                                        {
                                            ddlReleaseRequestNo.DataSource = list;
                                            ddlReleaseRequestNo.DataTextField = "rr_issueno";
                                            ddlReleaseRequestNo.DataValueField = "release_request_id";
                                            ddlReleaseRequestNo.DataBind();
                                            party_code.Value = list.ToList()[0].party_code;
                                            ddlReleaseRequestNo.Items.Insert(0, "Select");
                                        }
                                    }


                                }
                                if (user.role_name == "Applicant" && user.party_type == "Sugar Mill")
                                {
                                    if (Session["rtype"].ToString() == "0")
                                    {
                                        var list = (from s in rr
                                                    where s.from_party == user.party_code && s.record_status == "I" && Convert.ToDateTime(s.valid_date) >= DateTime.Now.Date && s.financial_year == Session["rfptfinancial_year"].ToString()
                                                    select s);
                                        if (list.ToList().Count > 0)
                                        {
                                            ddlReleaseRequestNo.DataSource = list;
                                            ddlReleaseRequestNo.DataTextField = "rr_issueno";
                                            ddlReleaseRequestNo.DataValueField = "release_request_id";
                                            ddlReleaseRequestNo.DataBind();
                                            party_code.Value = list.ToList()[0].party_code;
                                            ddlReleaseRequestNo.Items.Insert(0, "Select");
                                        }
                                    }
                                    else
                                    {
                                        var list = (from s in rr
                                                    where s.from_party == user.party_code && s.record_status == "I" && s.financial_year == Session["rfptfinancial_year"].ToString()
                                                    select s);
                                        if (list.ToList().Count > 0)
                                        {
                                            ddlReleaseRequestNo.DataSource = list;
                                            ddlReleaseRequestNo.DataTextField = "rr_issueno";
                                            ddlReleaseRequestNo.DataValueField = "release_request_id";
                                            ddlReleaseRequestNo.DataBind();
                                            party_code.Value = list.ToList()[0].party_code;
                                            ddlReleaseRequestNo.Items.Insert(0, "Select");
                                        }
                                    }


                                }
                            }
                        }
                        else
                        {
                            rrl.Visible = false;
                            depot.Visible = true;
                            nocl.Visible = true;
                            List<NOC_Details> noc = new List<NOC_Details>();
                            noc = BL_NOC_Details.GetNOCList();
                            if (noc.Count > 0)
                            {
                                if (user.role_name == "Bond Officer")
                                {
                                    var list = (from s in noc
                                                where s.party_code == user.party_code && s.record_status == "I" && s.financial_year == Session["rfptfinancial_year"].ToString()
                                                select s);
                                    if (list.ToList().Count > 0)
                                    {
                                        ddlReleaseRequestNo.DataSource = list.ToList();
                                        ddlReleaseRequestNo.DataTextField = "issue_nocno";
                                        ddlReleaseRequestNo.DataValueField = "noc_id";
                                        ddlReleaseRequestNo.DataBind();
                                        party_code.Value = list.ToList()[0].party_code;
                                        ddlReleaseRequestNo.Items.Insert(0, "Select");
                                    }
                                }
                                if (user.role_name == "Applicant")
                                {
                                    if (Session["rtype"].ToString() == "0")
                                    {
                                        var list = (from s in noc
                                                    where s.party_code == user.party_code && s.record_status == "I" && s.noc_lifted_qty < s.noc_total_qty && Convert.ToDateTime(s.valid_upto) >= DateTime.Now.Date && s.financial_year == Session["rfptfinancial_year"].ToString()
                                                    orderby s.issue_nocno ascending
                                                    select s);
                                        party_code.Value = user.party_code;
                                        if (list.ToList().Count > 0)
                                        {
                                            ddlReleaseRequestNo.DataSource = list.ToList();
                                            ddlReleaseRequestNo.DataTextField = "issue_nocno";
                                            ddlReleaseRequestNo.DataValueField = "noc_id";
                                            ddlReleaseRequestNo.DataBind();
                                            ddlReleaseRequestNo.Items.Insert(0, "Select");
                                        }
                                    }
                                    else
                                    {
                                        var list = (from s in noc
                                                    where s.party_code == user.party_code && s.record_status == "I" && s.financial_year == Session["rfptfinancial_year"].ToString()
                                                    orderby s.issue_nocno ascending
                                                    select s);
                                        party_code.Value = user.party_code;
                                        if (list.ToList().Count > 0)
                                        {
                                            ddlReleaseRequestNo.DataSource = list.ToList();
                                            ddlReleaseRequestNo.DataTextField = "issue_nocno";
                                            ddlReleaseRequestNo.DataValueField = "noc_id";
                                            ddlReleaseRequestNo.DataBind();

                                            ddlReleaseRequestNo.Items.Insert(0, "Select");
                                        }
                                    }
                                }


                            }


                        }
                        txtPassApprovedQty.Visible = false;
                        pass.Visible = false;
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        if (Session["rtype"].ToString() != "0")
                        {
                            ReaquestForPass pass1 = new ReaquestForPass();
                            pass1 = BL_ReaquestForPass.GetDetails(Session["RequestID"].ToString(), Session["ptype"].ToString(), party_code.Value, Session["rfptfinancial_year"].ToString());

                            ddlReleaseRequestNo.SelectedValue = pass1.rrnoc_request_id;
                            ddlReleaseRequestNo_SelectedIndexChanged(sender, e);
                            txtReleaseRequestDate.Text = pass1.rr_date;
                            txtRRValidUpto.Text = pass1.valied_date;
                            txtPassRequestedQty.Text = pass1.req_qty.ToString();
                            txtPassApprovedQty.Text = pass1.approved_qty.ToString();
                            txtAllotmentQty.Text = pass1.alloted_qty.ToString();
                            txtApprovedQTY.Text = pass1.approvedqty.ToString();
                            //    txtBalance.Text= pass1.blance_qty.ToString();
                            balanceqty.Value = pass1.blance_qty.ToString();
                            //  ca.StartDate = Convert.ToDateTime(pass1.valied_date);
                            // pass.Visible = true;
                            if (Session["ptype"].ToString() == "N")
                            {
                                ddlDepot.SelectedValue = pass1.noc_depotdetail_id;
                                ddlDepot_SelectedIndexChanged(sender, null);
                            }

                            txtcutomername.Text = pass1.name;
                            txtcutomername.ReadOnly = true;
                            // txtMaterialType.Text = pass1.product_name;

                            if (user.role_name == "Applicant" && (pass1.record_status == "A" || pass1.record_status == "I"))
                            {
                                txtPassApprovedQty.Visible = true;
                                pass.Visible = true;

                            }

                            if (Session["rtype"].ToString() == "1")
                            {
                                ddlReleaseRequestNo.Enabled = false;

                                txtPassRequestedQty.ReadOnly = true;
                                btnReject.Visible = false;
                                btnApprove.Visible = false;
                                btnCancel.Visible = false;
                                btnSaveasDraft.Visible = false;
                                btnSubmit.Visible = false;
                                btnApprove.Visible = false;
                                btnReject.Visible = false;
                                ddlDepot.Enabled = false;
                                txtPassApprovedQty.ReadOnly = true;
                                if (user.role_name == "Bond Officer" && pass1.record_status == "A")
                                {
                                    txtPassApprovedQty.ReadOnly = true;
                                    txtPassApprovedQty.Visible = true;
                                    pass.Visible = true;

                                }
                                if (user.role_name == "Bond Officer" && pass1.record_status == "Y")
                                {
                                    txtPassApprovedQty.ReadOnly = false;
                                    txtPassApprovedQty.Visible = true;
                                    btnApprove.Visible = true;
                                    btnReject.Visible = true;
                                    pass.Visible = true;
                                }
                                if (pass1.record_status == "A")
                                {
                                    txtPassApprovedQty.Visible = true;
                                    txtPassApprovedQty.ReadOnly = true;
                                    pass.Visible = true;
                                }
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

        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RequestForPassList");
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

        protected void ddlReleaseRequestNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["ptype"].ToString() == "M")
            {
                txtPassRequestedQty.Text = "";
                txtApprovedQTY.Text = "";
                txtAllotmentQty.Text = "";
                txtBalance.Text = "";
                if (Session["rtype"].ToString() == "0")
                {
                    Session["rfptfinancial_year"] = Session["financial_year"].ToString();
                }
                List<Release_Request> rr = new List<Release_Request>();
                rr = BL_Release_Request.GetRRList();
                var list = from s in rr
                           where s.record_status == "I" && s.release_request_id == ddlReleaseRequestNo.SelectedValue && s.financial_year == Session["rfptfinancial_year"].ToString()
                           select s;
                if (list.ToList().Count > 0)
                {
                    txtAllotmentQty.Text = list.ToList()[0].allocation_qty.ToString();
                    txtReleaseRequestDate.Text = list.ToList()[0].rr_date;
                    txtRRValidUpto.Text = list.ToList()[0].valid_date;
                   
                    DateTime dt = Convert.ToDateTime(list.ToList()[0].valid_date);
                    //txtIssuedqty.Text = requested;
                    if (DateTime.Now.Date > dt)
                    {
                        txtMaterialType.Text = list.ToList()[0].product_name;
                        txtApprovedQTY.Text = list.ToList()[0].rr_approved_qty.ToString();
                        txtcutomername.Text = list.ToList()[0].molasses_supplier;
                        txtcutomername.ReadOnly = true;
                        string requested = BL_ReaquestForPass.GetBalance(ddlReleaseRequestNo.SelectedValue, "RR", Session["rfptfinancial_year"].ToString());
                        double value = Convert.ToDouble(txtApprovedQTY.Text) - Convert.ToDouble(requested);
                        var result = value.ToString("F2");
                        if (txtPassRequestedQty.Text == "")
                        {
                            balanceqty.Value = result;
                            txtBalance.Text = result;
                        }
                        else
                        {
                            balanceqty.Value = Convert.ToDouble(Convert.ToDouble(result) + Convert.ToDouble(txtPassRequestedQty.Text)).ToString();
                            txtBalance.Text = Convert.ToDouble(Convert.ToDouble(result) + Convert.ToDouble(txtPassRequestedQty.Text)).ToString();
                        }
                        txtcutomername.ReadOnly = true;
                        btnSaveasDraft.Visible = false;
                        btnSubmit.Visible = false;
                        btnCancel.Visible = false;
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append("Valid Date is Expired");
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    }
                    else
                    {
                        txtMaterialType.Text = list.ToList()[0].product_name;
                        txtApprovedQTY.Text = list.ToList()[0].rr_approved_qty.ToString();
                        txtcutomername.Text = list.ToList()[0].molasses_supplier;
                        txtcutomername.ReadOnly = true;
                        string requested = BL_ReaquestForPass.GetBalance(ddlReleaseRequestNo.SelectedValue, "RR", Session["rfptfinancial_year"].ToString());
                        double value = Convert.ToDouble(txtApprovedQTY.Text) - Convert.ToDouble(requested);
                        var result = value.ToString("F2");
                        if (txtPassRequestedQty.Text == "")
                        {
                            balanceqty.Value = result;
                            txtBalance.Text = result;
                        }
                        else
                        {
                            balanceqty.Value = Convert.ToDouble(Convert.ToDouble(result) + Convert.ToDouble(txtPassRequestedQty.Text)).ToString();
                            txtBalance.Text = Convert.ToDouble(Convert.ToDouble(result) + Convert.ToDouble(txtPassRequestedQty.Text)).ToString();
                        }
                    }
                }
            }
            else
            {
                txtPassRequestedQty.Text = "";
                txtApprovedQTY.Text = "";
                txtAllotmentQty.Text = "";
                txtBalance.Text = "";
              //  ddlDepot.SelectedValue = "Select";
              if(Session["rtype"].ToString()=="0")
                {
                    Session["rfptfinancial_year"] =Session["financial_year"].ToString();
                }
                NOC_Details noc1 = new NOC_Details();
                noc1 = BL_NOC_Details.GetDetails(ddlReleaseRequestNo.SelectedValue.ToString(), Session["rfptfinancial_year"].ToString(), Session["rparty_code"].ToString());
                DateTime dt =Convert.ToDateTime( noc1.valid_upto);
                if (DateTime.Now.Date > dt)
                {
                    txtcutomername.Text = noc1.Cust_name;
                    txtcutomername.ReadOnly = true;
                    txtMaterialType.Text = noc1.product_name;
                    ddlDepot.DataSource = noc1.depot;
                    ddlDepot.DataTextField = "Depot_name";
                    ddlDepot.DataValueField = "depot_id";
                    ddlDepot.DataBind();
                    ddlDepot.Items.Insert(0, "Select");
                    btnSaveasDraft.Visible = false;
                    btnSubmit.Visible = false;
                    btnCancel.Visible = false;
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append("Valid Date is Expired");
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                }
                else
                {
                    txtcutomername.Text = noc1.Cust_name;
                    txtcutomername.ReadOnly = true;
                    txtMaterialType.Text = noc1.product_name;
                    ddlDepot.DataSource = noc1.depot;
                    ddlDepot.DataTextField = "Depot_name";
                    ddlDepot.DataValueField = "depot_id";
                    ddlDepot.DataBind();
                    ddlDepot.Items.Insert(0, "Select");
                }
                
            }

          
        }

        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                if (txtPassRequestedQty.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Pass Requested Qty');", true);
                    txtPassRequestedQty.Focus();
                    txtPassRequestedQty.Text = "";
                }
                else if(Convert.ToDouble(txtPassRequestedQty.Text) <1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Requested Qty Zero or Less Than 1 is Not Allowed');", true);
                    txtPassRequestedQty.Focus();
                    txtPassRequestedQty.Text = "";
                }
                else
                {
                    ReaquestForPass pass = new ReaquestForPass();
                    pass.rrnoc_request_id = ddlReleaseRequestNo.SelectedValue;
                    pass.record_status = "N";
                    pass.pass_type = Session["ptype"].ToString();
                    pass.party_code = party_code.Value;
                    pass.user_id = Session["UserID"].ToString();
                    pass.req_qty = Convert.ToDouble(txtPassRequestedQty.Text);
                    pass.financial_year = Session["financial_year"].ToString();
                    if (Session["ptype"].ToString() == "N")
                        pass.noc_depotdetail_id = ddlDepot.SelectedValue;
                    string val = "";
                    if (Session["rtype"].ToString() == "0")
                        val = BL_ReaquestForPass.Insert(pass);
                    else
                    {
                        pass.request_for_pass_id = Session["RequestID"].ToString();
                        val = BL_ReaquestForPass.Update(pass);
                    }
                    if (val == "0")
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("RequestForPassList");


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
                //else
                //{

                //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //    sb.Append("<script type = 'text/javascript'>");
                //    sb.Append("window.onload=function(){");
                //    sb.Append("alert('");
                //    sb.Append("pleease check Pass Requested Qty");
                //    sb.Append("')};");
                //    sb.Append("</script>");
                //    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                //}
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtPassRequestedQty.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Pass Requested Qty');", true);
                    txtPassRequestedQty.Focus();
                    txtPassRequestedQty.Text = "";
                }
                else if (Convert.ToDouble(txtPassRequestedQty.Text) < 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Requested Qty Zero or Less Than 1 is Not Allowed');", true);
                    txtPassRequestedQty.Focus();
                    txtPassRequestedQty.Text = "";
                }
                else
                {
                    btnSubmit.Enabled = false;
                    ReaquestForPass pass = new ReaquestForPass();
                    pass.rrnoc_request_id = ddlReleaseRequestNo.SelectedValue;
                    pass.record_status = "Y";
                    pass.party_code = party_code.Value;
                    pass.pass_type = Session["ptype"].ToString();
                    pass.financial_year = Session["financial_year"].ToString();
                    pass.user_id = Session["UserID"].ToString();
                    pass.req_qty = Convert.ToDouble(txtPassRequestedQty.Text);
                    if (Session["ptype"].ToString() == "N")
                        pass.noc_depotdetail_id = ddlDepot.SelectedValue;
                    string val = "";
                    if (Session["rtype"].ToString() == "0")
                        val = BL_ReaquestForPass.Insert(pass);
                    else
                    {
                        pass.request_for_pass_id = Session["RequestID"].ToString();
                        val = BL_ReaquestForPass.Update(pass);
                    }
                    if (val == "0")
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("RequestForPassList");


                    }
                    else
                    {
                        btnSubmit.Enabled = true;
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
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RequestForPassList");
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ReaquestForPass pass = new ReaquestForPass();
                pass.rrnoc_request_id = ddlReleaseRequestNo.SelectedValue;
                pass.record_status = "A";
                pass.user_id = Session["UserID"].ToString();
                pass.financial_year = Session["financial_year"].ToString();
                pass.approved_qty = Convert.ToDouble(txtPassApprovedQty.Text);
                pass.party_code = Session["rpparty_code"].ToString();
                string val = "";
                pass.request_for_pass_id = Session["RequestID"].ToString();
                val = BL_ReaquestForPass.Approve(pass);
               
                  
                   
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("RequestForPassList");


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

        protected void btnReject_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ReaquestForPass pass = new ReaquestForPass();
                pass.rrnoc_request_id = ddlReleaseRequestNo.SelectedValue;
                pass.record_status = "R";
                pass.user_id = Session["UserID"].ToString();
                pass.financial_year = Session["financial_year"].ToString();
                pass.approved_qty =0;
                pass.party_code = Session["rpparty_code"].ToString();
                string val = "";
                pass.request_for_pass_id = Session["RequestID"].ToString();
                val = BL_ReaquestForPass.Approve(pass);



                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("RequestForPassList");


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

        protected void ddlDepot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDepot.SelectedValue != "Select")
            {
               // txtPassRequestedQty.Text = "";
                NOC_Details noc1 = new NOC_Details();
                noc1 = BL_NOC_Details.GetDetails(ddlReleaseRequestNo.SelectedValue.ToString(), Session["rfptfinancial_year"].ToString(), Session["rparty_code"].ToString());
                var list = from s in noc1.depot
                           where s.Depot_id == ddlDepot.SelectedValue
                           select s;
                if (list.ToList().Count > 0)
                {
                    txtAllotmentQty.Text = list.ToList()[0].qty.ToString();
                    txtReleaseRequestDate.Text = noc1.nocdate;
                    txtRRValidUpto.Text = noc1.valid_upto;
                    //txtMaterialType.Text =noc1.noc_for;

                    txtApprovedQTY.Text = list.ToList()[0].qty.ToString();
                    string requested = BL_ReaquestForPass.GetBalance(ddlReleaseRequestNo.SelectedValue, "NOC", Session["rfptfinancial_year"].ToString());
                    double value = Convert.ToDouble(txtAllotmentQty.Text) - Convert.ToDouble(requested);
                    var result =value.ToString("F2");
                    if (txtPassRequestedQty.Text == "")
                    {
                        balanceqty.Value = result;
                        txtBalance.Text = result;
                    }
                    else
                    {
                        balanceqty.Value =Convert.ToDouble(Convert.ToDouble(result) + Convert.ToDouble(txtPassRequestedQty.Text)).ToString();
                        txtBalance.Text = Convert.ToDouble(Convert.ToDouble(result) + Convert.ToDouble(txtPassRequestedQty.Text)).ToString();
                    }
                  
                }
            }
        }
    }
}