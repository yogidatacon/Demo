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
    public partial class RequestForTransportPassForm : System.Web.UI.Page
    {
        UserDetails user = new UserDetails();
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
                        //List<Release_Request> rr = new List<Release_Request>();
                        //rr = BL_Release_Request.GetRRList();
                        CalendarExtender.StartDate = DateTime.Now;
                        CalendarExtender1.EndDate = DateTime.Now;
                        passtype.Value = Session["ptype"].ToString();
                        rolename.Value = user.role_name;
                        Session["role"] = user.role_name;
                        Session["rtparty_code"] = user.party_code;
                        Session["financial_year"] = user.financial_year;
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
                                                where s.from_party == user.party_code && s.record_status == "I" && Convert.ToDateTime(s.valid_date) > DateTime.Now.Date
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
                                                    where s.party_code == user.party_code && s.record_status == "I" && s.rr_lifted_qty < s.rr_quantity && Convert.ToDateTime(s.valid_date) >= DateTime.Now.Date
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
                                                    where s.party_code == user.party_code && s.record_status == "I"
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
                                                    where s.from_party == user.party_code && s.record_status == "I" && Convert.ToDateTime(s.valid_date) >= DateTime.Now.Date
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
                                                    where s.from_party == user.party_code && s.record_status == "I"
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
                            string nocpasstype;
                            if (Session["ptype"].ToString()=="E")
                            {
                                nocpasstype = "EXP";
                            }
                            else
                            {
                                Response.Redirect("WebForm2.aspx");
                                nocpasstype = "DOM";
                            }
                            List<NOC_Details> noc = new List<NOC_Details>();
                            noc =BL_RequestForTransportPass.GetNOCList1(nocpasstype);
                            if (noc.Count > 0)
                            {
                                if (user.role_name == "Bond Officer")
                                {
                                    var list = (from s in noc
                                                where s.party_code == user.party_code && s.record_status == "I"
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
                                if (user.role_name.Trim() == "Deputy Commissioner")
                                {
                                    var list = (from s in noc
                                                where s.record_status == "I"
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
                                                    where s.party_code == user.party_code && s.record_status == "I" && s.noc_lifted_qty < s.noc_total_qty && Convert.ToDateTime(s.valid_upto) >= DateTime.Now.Date
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
                                                    where s.party_code == user.party_code && s.record_status == "I"
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
                        btnReferBack.Visible = false;
                        btnissue.Visible = false;
                        approverid.Visible = false;
                        approverremarks.Visible = false;
                        if (Session["rtype"].ToString() != "0")
                        {
                            ReaquestForPass pass1 = new ReaquestForPass();
                            pass1 = BL_RequestForTransportPass.GetDetails(Session["RequestID"].ToString(), Session["ptype"].ToString(), Session["rftptfinancial_year"].ToString());

                            ddlReleaseRequestNo.SelectedValue = pass1.rrnoc_request_id;
                            txtdob1.Value = pass1.pass_valid_upto;
                            ddlReleaseRequestNo_SelectedIndexChanged(sender, e);
                            // txtReleaseRequestDate.Text = pass1.rr_date;
                            txtpass.Value = pass1.request_for_pass_date;
                            txtTemperature.Text = pass1.temperature.ToString();
                            txtStrength.Text = pass1.strength.ToString();
                            txtIndication.Text = pass1.indication.ToString();
                            txtRouteDetails.Text = pass1.route_details;
                           
                            txtTankerNo.Text = pass1.vehicle_no;
                            txtDigitalLockNo.Text = pass1.digital_lock_no;
                            txtPassRequestedQty.Text = pass1.req_qty.ToString();
                            txtPassRequestedQty_TextChanged(sender, null);
                            txtPassApprovedQty.Text = pass1.approved_qty.ToString();
                            txtAllotmentQty.Text = pass1.alloted_qty.ToString();
                            txtApprovedQTY.Text = pass1.approvedqty.ToString();
                            //    txtBalance.Text= pass1.blance_qty.ToString();
                            balanceqty.Value = pass1.blance_qty.ToString();
                            Session["User"] = pass1.user_id;
                            //  ca.StartDate = Convert.ToDateTime(pass1.valied_date);
                            // pass.Visible = true;
                            if (Session["ptype"].ToString() == "N" || Session["ptype"].ToString() == "E")
                            {
                                ddlDepot.SelectedValue = pass1.noc_depotdetail_id;
                                ddlDepot_SelectedIndexChanged(sender, null);
                            }
                            txtPermitNo.Text = pass1.permitno;
                            //     txtSupplierAddress.Text = pass1.name + "&" + pass1.address;
                            txtSupplierAddress.ReadOnly = true;
                            // txtMaterialType.Text = pass1.product_name;

                            if (user.role_name == "Applicant" && (pass1.record_status == "A" || pass1.record_status == "I"))
                            {
                                txtPassApprovedQty.Visible = true;
                                pass.Visible = true;

                            }
                            List<All_Approvals> approvals = new List<All_Approvals>();
                            approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), Session["RequestID"].ToString(), "RTP");
                            var list4 = (from s in approvals
                                         where s.financial_year == Session["rftptfinancial_year"].ToString()
                                         select s);
                            grdApprovalDetails.DataSource = list4.ToList();
                            grdApprovalDetails.DataBind();
                            if (Session["rtype"].ToString() == "1")
                            {
                                ddlReleaseRequestNo.Enabled = false;
                                txtPermitNo.ReadOnly = true;
                                txtPassRequestedQty.ReadOnly = true;
                                btnReject.Visible = false;
                                btnApprove.Visible = false;
                                btnCancel.Visible = false;
                                btnSaveasDraft.Visible = false;
                                btnSubmit.Visible = false;
                                txtRouteDetails.ReadOnly = true;
                                txtTemperature.ReadOnly = true;
                                txtStrength.ReadOnly = true;
                                txtIndication.ReadOnly = true;
                                txtTankerNo.ReadOnly = true;
                                txtDigitalLockNo.ReadOnly = true;
                                btnissue.Visible = false;
                                ddlDepot.Enabled = false;
                                Image1.Visible = false;
                                Image2.Visible = false;
                                txtPassApprovedQty.ReadOnly = true;
                                if (user.role_name == "Bond Officer" && pass1.record_status == "A")
                                {
                                    txtPassApprovedQty.ReadOnly = true;
                                    txtPassApprovedQty.Visible = true;
                                    pass.Visible = true;
                                    approverid.Visible = true;
                                    approverremarks.Visible = false;
                                }
                                if (user.role_name == "Bond Officer" && pass1.record_status == "A" /*pass1.approval_status == "Approved"*/)
                                {
                                    txtPassApprovedQty.ReadOnly = true;
                                    txtPassApprovedQty.Visible = true;
                                    btnCancel.Visible = true;
                                    pass.Visible = true;
                                    approverremarks.Visible = true;
                                    btnApprove.Visible = false;
                                    btnReferBack.Visible = false;
                                    approverid.Visible = true;
                                    btnissue.Visible = true;
                                }
                                if (user.role_name == "Bond Officer" && pass1.record_status == "Y" || pass1.record_status == "B")
                                {
                                    txtPassApprovedQty.ReadOnly = false;
                                    approverremarks.Visible = true;
                                    txtPassApprovedQty.Visible = true;
                                    btnApprove.Visible = true;
                                    btnReject.Visible = true;
                                    pass.Visible = true;
                                }
                                if (pass1.record_status == "A" && user.role_name.Trim() == "Bond Officer")
                                {
                                    btnReferBack.Visible =false;
                                    btnApprove.Visible = false;
                                    btnissue.Visible = true;
                                    //btnReject.Visible = true;
                                    btnCancel.Visible = true;
                                    approverremarks.Visible = true;
                                    approverid.Visible = true;
                                    txtPassApprovedQty.Visible = true;
                                    txtPassApprovedQty.ReadOnly = true;
                                    pass.Visible = true;
                                }
                                if (pass1.record_status == "I")
                                {
                                    approverid.Visible = true;

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
            Response.Redirect("RequestForTransportList");
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

        protected void ddlReleaseRequestNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["ptype"].ToString() == "M")
            {
                txtPassRequestedQty.Text = "";
                txtApprovedQTY.Text = "";
                txtAllotmentQty.Text = "";
                txtBalance.Text = "";
                List<Release_Request> rr = new List<Release_Request>();
                rr = BL_Release_Request.GetRRList();
                var list = from s in rr
                           where s.record_status == "I" && s.release_request_id == ddlReleaseRequestNo.SelectedValue
                           select s;
                if (list.ToList().Count > 0)
                {
                    txtAllotmentQty.Text = list.ToList()[0].allocation_qty.ToString();
                    // txtReleaseRequestDate.Text = list.ToList()[0].rr_date;
                    txtdob1.Value = list.ToList()[0].valid_date;

                    DateTime dt = Convert.ToDateTime(list.ToList()[0].valid_date);
                    //txtIssuedqty.Text = requested;
                    if (DateTime.Now.Date > dt)
                    {
                        txtMaterialType.Text = list.ToList()[0].product_name;
                        txtApprovedQTY.Text = list.ToList()[0].rr_approved_qty.ToString();
                        //txtSupplierAddress.Text = list.ToList()[0].molasses_supplier +"&"+list.ToList()[0].m;
                        //txtcutomername.ReadOnly = true;
                        string requested = BL_RequestForTransportPass.GetBalance(ddlReleaseRequestNo.SelectedValue, "RR");
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
                        //  txtcutomername.ReadOnly = true;
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
                        //txtcutomername.Text = list.ToList()[0].molasses_supplier;
                        //txtcutomername.ReadOnly = true;
                        string requested = BL_RequestForTransportPass.GetBalance(ddlReleaseRequestNo.SelectedValue, "RR");
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
                if (Session["rtype"].ToString() == "0")
                {
                    Session["rftptfinancial_year"] = Session["financial_year"].ToString();
                }
                NOC_Details noc1 = new NOC_Details();
                noc1 = BL_NOC_Details.GetDetails(ddlReleaseRequestNo.SelectedValue.ToString(), Session["rftptfinancial_year"].ToString(), Session["rtparty_code"].ToString());
                if (txtdob1.Value != "")
                {
                    DateTime dt = Convert.ToDateTime(txtdob1.Value);
                    if (DateTime.Now.Date > dt)
                    {
                        txtSupplierAddress.Text = noc1.Cust_name + "  " + "&" + "  " + noc1.cust_address;
                        txtSupplierAddress.ReadOnly = true;
                        txtMaterialType.Text = noc1.product_name;
                        ddlDepot.DataSource = noc1.depot;
                        ddlDepot.DataTextField = "Depot_name";
                        ddlDepot.DataValueField = "depot_id";
                        ddlDepot.DataBind();
                        ddlDepot.Items.Insert(0, "Select");
                        if (Session["recordstatus"].ToString() != "N")
                        {
                            btnSaveasDraft.Visible = false;
                            btnSubmit.Visible = false;
                            btnCancel.Visible = false;
                        }
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
                        txtSupplierAddress.Text = noc1.Cust_name + "  "+ "&" + "  " + noc1.cust_address;
                        txtSupplierAddress.ReadOnly = true;
                        txtMaterialType.Text = noc1.product_name;
                        ddlDepot.DataSource = noc1.depot;
                        ddlDepot.DataTextField = "Depot_name";
                        ddlDepot.DataValueField = "depot_id";
                        ddlDepot.DataBind();
                        ddlDepot.Items.Insert(0, "Select");
                    }
                }
                else
                {
                    txtSupplierAddress.Text = noc1.Cust_name + "  " + "&" + "  " + noc1.cust_address;
                    txtSupplierAddress.ReadOnly = true;
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
            if (IsPostBack)
            {
                if (txtPassRequestedQty.Text != "")
                {
                    ReaquestForPass pass = new ReaquestForPass();
                    pass.rrnoc_request_id = ddlReleaseRequestNo.SelectedValue;
                    pass.record_status = "N";
                    pass.pass_type = Session["ptype"].ToString();
                    pass.party_code = party_code.Value;
                    pass.user_id = Session["UserID"].ToString();
                    pass.req_qty = Convert.ToDouble(txtPassRequestedQty.Text);
                    pass.route_details = txtRouteDetails.Text;
                    pass.request_for_pass_date = txtpass.Value;
                    pass.permitno = txtPermitNo.Text;
                    pass.vehicle_no = txtTankerNo.Text;
                    pass.temperature = Convert.ToDouble(txtTemperature.Text);
                    pass.strength = Convert.ToDouble(txtStrength.Text);
                    pass.indication = Convert.ToDouble(txtIndication.Text);
                    pass.pass_valid_upto = txtdob1.Value;
                    pass.approval_status = "Draft";
                    pass.financial_year = Session["financial_year"].ToString();
                    pass.digital_lock_no = txtDigitalLockNo.Text;
                    if (Session["ptype"].ToString() == "N" || Session["ptype"].ToString() == "E")
                        pass.noc_depotdetail_id = ddlDepot.SelectedValue;
                    string val = "";
                    if (Session["rtype"].ToString() == "0")
                        val = BL_RequestForTransportPass.Insert(pass);
                    else
                    {
                        pass.request_for_pass_id = Session["RequestID"].ToString();
                        val = BL_RequestForTransportPass.Update(pass);
                    }
                    if (val == "0")
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("RequestForTransportList");
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
                else
                {

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append("pleease check Pass Requested Qty");
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                }
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                btnSubmit.Enabled = false;
                ReaquestForPass pass = new ReaquestForPass();
                pass.rrnoc_request_id = ddlReleaseRequestNo.SelectedValue;
                pass.record_status = "Y";
                pass.party_code = party_code.Value;
                pass.pass_type = Session["ptype"].ToString();
                pass.permitno = txtPermitNo.Text;
                pass.user_id = Session["UserID"].ToString();
                pass.req_qty = Convert.ToDouble(txtPassRequestedQty.Text);
                pass.route_details = txtRouteDetails.Text;
                pass.request_for_pass_date = txtpass.Value;
                pass.vehicle_no = txtTankerNo.Text;
                pass.financial_year = Session["financial_year"].ToString();
                pass.temperature = Convert.ToDouble(txtTemperature.Text);
                pass.strength = Convert.ToDouble(txtStrength.Text);
                pass.indication = Convert.ToDouble(txtIndication.Text);
                pass.pass_valid_upto = txtdob1.Value;
                pass.approval_status = "Pending";
                pass.digital_lock_no = txtDigitalLockNo.Text;
                if (Session["ptype"].ToString() == "N" || Session["ptype"].ToString() == "E")
                    pass.noc_depotdetail_id = ddlDepot.SelectedValue;
                string val = "";
                if (Session["rtype"].ToString() == "0")
                    val = BL_RequestForTransportPass.Insert(pass);
                else
                {
                    pass.request_for_pass_id = Session["RequestID"].ToString();
                    val = BL_RequestForTransportPass.Update(pass);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("RequestForTransportList");
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RequestForTransportList.aspx");
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ReaquestForPass pass = new ReaquestForPass();
                pass.rrnoc_request_id = ddlReleaseRequestNo.SelectedValue;
             
                pass.user_id = Session["UserID"].ToString();
                pass.approved_qty = Convert.ToDouble(txtPassApprovedQty.Text);
                pass.remarks = txtApproverremarks.Value;
                pass.financial_year = Session["rftptfinancial_year"].ToString();
                pass.party_code = Session["rtpparty_code"].ToString();
                if (rolename.Value == "Bond Officer")
                {
                    pass.approval_status = "Recommended by " + rolename.Value;
                    pass.record_status = "A";
                }
                else
                {
                    pass.approval_status = "Approved";
                    pass.record_status = "A";
                }
                string val = "";
                pass.request_for_pass_id = Session["RequestID"].ToString();
                val = BL_RequestForTransportPass.Approve(pass);



                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("RequestForTransportList");


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
                pass.approval_status = "Rejected by " + rolename.Value;
                pass.remarks = txtApproverremarks.Value;
                pass.financial_year = Session["rftptfinancial_year"].ToString();
                pass.party_code = Session["rtpparty_code"].ToString();
                pass.approved_qty = 0;
                string val = "";
                pass.request_for_pass_id = Session["RequestID"].ToString();
                val = BL_RequestForTransportPass.Approve(pass);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("RequestForTransportList");
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
                noc1 = BL_NOC_Details.GetDetails(ddlReleaseRequestNo.SelectedValue.ToString(), Session["rftptfinancial_year"].ToString(), Session["rtparty_code"].ToString());
                var list = from s in noc1.depot
                           where s.Depot_id == ddlDepot.SelectedValue
                           select s;
                if (list.ToList().Count > 0)
                {
                    if (Session["role"].ToString()=="Applicant")
                    {
                        user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                        txtNameAddress.Text = user.user_name + "  " + "&" + "  " + user.user_address;
                    }
                    else
                    {
                        user = BL_UserDetails.CheckUser(Session["User"].ToString());
                        txtNameAddress.Text = user.user_name + "  " + "&" + "  " + user.user_address;
                    }
                   
                    txtAllotmentQty.Text = list.ToList()[0].qty.ToString();
                    // txtReleaseRequestDate.Text = noc1.nocdate;
                    //  txtdob1.Value = noc1.valid_upto;
                    txtPermitNo.Text = noc1.tenderno;
                    txtSupplierAddress.Text = noc1.Cust_name + "  " + "&" + "  " + noc1.cust_address;
                    txtdistrict.Text = noc1.district;
                    txtstate.Text = noc1.state;
                    //txtMaterialType.Text =noc1.noc_for;

                    txtApprovedQTY.Text = list.ToList()[0].qty.ToString();
                    string requested = BL_RequestForTransportPass.GetBalance(ddlReleaseRequestNo.SelectedValue, Session["ptype"].ToString());
                    double value = Convert.ToDouble(txtAllotmentQty.Text) - Convert.ToDouble(requested);
                    var result = value.ToString("F2");
                    if (txtPassRequestedQty.Text == "")
                    {
                        balanceqty.Value = result;
                        txtBalance.Text = result;
                    }
                    else
                    {
                        balanceqty.Value = Convert.ToDouble(Convert.ToDouble(result)).ToString(); /* + Convert.ToDouble(txtPassRequestedQty.Text)).ToString();*/
                        txtBalance.Text = Convert.ToDouble(Convert.ToDouble(result)).ToString();  /*+ Convert.ToDouble(txtPassRequestedQty.Text)).ToString();*/
                    }

                }
            }
        }

        protected void btnReferBack_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ReaquestForPass pass = new ReaquestForPass();
                pass.rrnoc_request_id = ddlReleaseRequestNo.SelectedValue;
                pass.record_status = "B";
                pass.user_id = Session["UserID"].ToString();
                pass.approved_qty = Convert.ToDouble(txtPassApprovedQty.Text);
                pass.remarks = txtApproverremarks.Value;
                pass.financial_year = Session["rftptfinancial_year"].ToString();
                pass.party_code = Session["rtpparty_code"].ToString();
                pass.approval_status = "Refer Back  to Bond Officer";
                string val = "";
                pass.request_for_pass_id = Session["RequestID"].ToString();
                val = BL_RequestForTransportPass.Approve(pass);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("RequestForTransportList");
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

        protected void btnissue_Click(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                ReaquestForPass pass = new ReaquestForPass();
                pass.rrnoc_request_id = ddlReleaseRequestNo.SelectedValue;
                pass.record_status = "I";
                pass.user_id = Session["UserID"].ToString();
                pass.approved_qty = Convert.ToDouble(txtPassApprovedQty.Text);
                pass.approval_status = "Issued by " + rolename.Value;
                pass.financial_year = Session["rftptfinancial_year"].ToString();
                pass.party_code = Session["rtpparty_code"].ToString();
                pass.remarks = txtApproverremarks.Value;
                string val = "";
                pass.request_for_pass_id = Session["RequestID"].ToString();
                val = BL_RequestForTransportPass.Approve(pass);

                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("RequestForTransportList");
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

        protected void txtStrength_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtStrength.Text != "")
                {
                    if (txtPassRequestedQty.Text != "")
                    {
                        txtQtylpl.Text = (Convert.ToDouble(txtPassRequestedQty.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                    }

                }

            }
        }

        protected void txtPassRequestedQty_TextChanged(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (txtStrength.Text != "")
                {
                    if (txtPassRequestedQty.Text != "")
                    {
                        txtQtylpl.Text = (Convert.ToDouble(txtPassRequestedQty.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                    }
                }
            }
            if (IsPostBack)
            {
                if (txtStrength.Text != "")
                {
                    if (txtPassRequestedQty.Text != "")
                    {
                        txtQtylpl.Text = (Convert.ToDouble(txtPassRequestedQty.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Strength !!!!!\');", true);
                    txtStrength.Focus();
                }
            }
        }
    }
}