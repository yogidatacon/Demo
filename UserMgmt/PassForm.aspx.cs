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
    public partial class PassForm : System.Web.UI.Page
    {
       static ReaquestForPass pass = new ReaquestForPass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    userparty.Value = user.party_code;
                    Session["financial_year"] = user.financial_year;
                    //    CalendarExtender3.StartDate = DateTime.Now;
                    //ddlDispatchType.Items.Insert(0, "Select");
                    //ddlDispatchType.Items.Insert(1, "Export");
                    //ddlDispatchType.Items.Insert(2, "PipeLine");
                    //ddlDispatchType.Items.Insert(2, "Within State");
                    //  List<UOM_Master> uomlist = new List<UOM_Master>();
                    // uomlist = BL_UOM.GetList(Session["UserID"].ToString());
                    //ddlUOM.DataSource = uomlist;
                    //  ddlUOM.DataTextField = "uom_name";
                    //  ddlUOM.DataValueField = "uom_code";
                    // ddlUOM.DataBind();
                    // ddlUOM.Items.Insert(0, "Select");
                    if (user.financial_year != "")
                    {
                        int year = Convert.ToInt32(user.financial_year.Substring(0, 4));
                        string year1 = user.financial_year;
                        string year2 = (year - 1).ToString() + "-" + (year).ToString();
                        string year3 = (year - 2).ToString() + "-" + (year - 1).ToString();
                        ddlYearofProduction.Items.Insert(0, "Select");
                        ddlYearofProduction.Items.Insert(1, year1);
                        ddlYearofProduction.Items.Insert(2, year2);
                        ddlYearofProduction.Items.Insert(3, year3);
                    }
                    passtype.Value = Session["ptype"].ToString();
                    btnApprove.Visible = false;
                    approverremaks.Visible = false;
                    btnReject.Visible = false;
                    approver.Visible = false;
                    List<DispatchType> dis = new List<DispatchType>();
                    dis = BL_DispatchType.GetList();
                    ddlDispatchType.DataSource = dis;
                    ddlDispatchType.DataTextField = "dispatch_type_name";
                    ddlDispatchType.DataValueField = "dispatch_type_id";
                    ddlDispatchType.DataBind();
                    ddlDispatchType.Items.Insert(0, "Select");
                    if (Session["ptype"].ToString() == "M" || Session["ptype"].ToString() == "RR")
                    {
                        Depot.Visible = false;
                        nocrequest.Visible = false;
                        issuedNOC.Visible = false;
                        Molasses.Visible = true;
                        if (user.party_type == "Distillery Unit")
                        {
                            customer.Visible = false;
                            caddress.Visible = false;
                            Supplieraddress.Visible = true;
                            Suppliername.Visible = true;
                        }
                        else
                        {
                            customer.Visible = true;
                            caddress.Visible = true;
                            Supplieraddress.Visible = false;
                            Suppliername.Visible = false;
                        }
                        ApprovalB.Visible =true;
                        txtDateOfDispatch.ReadOnly = true;
                        //    CalendarExtender3.StartDate = DateTime.Now;
                        ReaquestForPass rrpass = new ReaquestForPass();
                        rrpass = BL_ReaquestForPass.GetRequest(Session["PassRequestID"].ToString(), Session["ptype"].ToString(), user.party_code, user.party_type, Session["Pfinancial_year"].ToString());
                        txtReleaseRequestNo.Text = rrpass.rr_noc_issuedno;
                        rr_noc_request_id.Value = rrpass.rrnoc_request_id;
                        txtPassRequestNo.Text = rrpass.request_for_pass_id;
                        txtPassRequiredFor.Text = rrpass.product_name;
                        txtValidUpto.Text = rrpass.valied_date;
                        CalendarExtender3.EndDate = Convert.ToDateTime(rrpass.valied_date);
                        txtAllotmentrequestno.Text = rrpass.rr_allotmentno;
                        txtSupplierName.Text = rrpass.name;
                        txtSupplierAddress.Text = rrpass.address;
                        txtReleaseRequestQuantity.Text = rrpass.req_qty.ToString();
                        txtLiftedQuantity.Text = rrpass.lifted_qty.ToString();
                        txtBalanceQuantity.Text = rrpass.blance_qty.ToString();
                        //txtQtyDispatch.Text = rrpass.req_qty.ToString();
                        party_code.Value = rrpass.toparty_code;
                        product_code.Value = rrpass.product_code;
                        var list1 = from s in rrpass.vats
                                    where s.content == rrpass.product_code
                                    select s;
                        ddlDispatchVAT.DataSource = list1.ToList();
                        ddlDispatchVAT.DataTextField = "vat_name";
                        ddlDispatchVAT.DataValueField = "vat_code";
                        ddlDispatchVAT.DataBind();
                        ddlDispatchVAT.Items.Insert(0, "Select");
                        if (Session["rtype"].ToString() != "0")
                        {
                            Pass_Details pas = new Pass_Details();
                            pas = BL_Pass_Details.GetDetails(Session["pass_id"].ToString(), Session["ptype"].ToString(), Session["Pfinancial_year"].ToString());
                            ddlDispatchType.SelectedValue = pas.dispatch_type_id;
                            ddlDispatchVAT.SelectedValue = pas.to_dispatch_vat;
                            ddlDispatchVAT_SelectedIndexChanged(sender, null);
                            status.Value = pas.record_status;
                            txtpassissuedno.Text = pas.pass_issuedno;
                            if (txtpassissuedno.Text == "")
                                issued.Visible = false;
                            balance.Value = rrpass.blance_qty.ToString();// + pas.dispatch_qty).ToString();
                            txtQtyDispatch.Text = pas.dispatch_qty.ToString();
                            txtTaxInvoiceNo1.Text = pas.taxinvoice;
                            txtBRIX.Text = pas.brix;
                            txtSugarContent.Text = pas.sugar_content;
                            txtRemarks1.Text = pas.remarks;
                            ddlYearofProduction.SelectedValue = pas.prev_prod_year;
                            txtNameOfCarrier.Text = pas.carrier;
                            txtVehicleNo.Text = pas.vehicle_no;
                            txtVehicleType.Text = pas.vehicle_type;
                            txtDriverName.Text = pas.driver;
                            txtTransportChallanBiltyNo.Text = pas.challan_no;
                            txtDigitalLockNo.Text = pas.digital_lock_no;
                            txtDateOfDispatch.Text = pas.dispatch_date;
                            dd1.Value = pas.dispatch_date;
                            txtTimeOfDispatch.Value = pas.dispatch_time;
                            txtDurationOfDispatch.Text = pas.dispatch_duration;
                            txtRouteDetails.Text = pas.route_details;
                        
                            if (ddlDispatchType.SelectedItem.ToString() == "Pipeline" || ddlDispatchType.SelectedItem.ToString() == "PipeLine")
                            {
                                    pipeline.Visible = false;
                                
                            }

                            List<All_Approvals> approvals = new List<All_Approvals>();
                            approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), pas.pass_id.ToString(), "Pass");
                            if (Session["UserID"].ToString() == "Admin")
                            {
                                CalendarExtender3.Enabled = true;
                                btnCancel.Visible = true;
                                btnSaveasDraft.Visible = false;
                                btnSubmit.Visible = false;
                                ddlDispatchVAT.Enabled = false;
                                ddlDispatchType.Enabled = false;
                                //  ddlUOM.Enabled = false;
                                txtQtyDispatch.ReadOnly = true;
                                txtTaxInvoiceNo1.ReadOnly = false;
                                txtBRIX.ReadOnly = true;
                                txtSugarContent.ReadOnly = true;
                                txtRemarks1.ReadOnly = true;
                                ddlYearofProduction.Enabled = false;
                                txtNameOfCarrier.ReadOnly = false;
                                txtVehicleNo.ReadOnly = false;
                                txtVehicleType.ReadOnly = false;
                                txtDriverName.ReadOnly = false;
                                txtTransportChallanBiltyNo.ReadOnly = false;
                                txtDigitalLockNo.ReadOnly = false;
                                txtDateOfDispatch.ReadOnly = false;
                                txtDigitalLockNo.ReadOnly = false;
                                txtDateOfDispatch.ReadOnly = false;
                                txtRouteDetails.ReadOnly = false;
                                btnupdate.Visible = true;
                                //if (status.Value == "I")
                                //{
                                txtDurationOfDispatch.ReadOnly =false;

                                Image1.Visible = true;
                                txtDateOfDispatch.ReadOnly=true;
                                approver.Visible = true;
                                ApprovalB.Visible = true;
                                var list4 = (from s in approvals
                                             where s.user_id == "Admin"
                                             select s);
                                grdApprovalDetails.DataSource = list4.ToList();
                                grdApprovalDetails.DataBind();
                            }
                            else
                            {
                                var list4 = (from s in approvals
                                             where s.financial_year == Session["Pfinancial_year"].ToString()
                                             select s);
                                grdApprovalDetails.DataSource = list4.ToList();
                                grdApprovalDetails.DataBind();
                            }
                              
                            if (approvals.Count > 0 || status.Value == "I" || status.Value == "P" || status.Value == "M" || status.Value == "D")
                            {
                                if (Session["UserID"].ToString() != "Admin")
                                {
                                    approver.Visible = true;
                                    ApprovalB.Visible = true;
                                    btnSaveasDraft.Visible = false;
                                    txtNameOfCarrier.ReadOnly = true;
                                    txtVehicleNo.ReadOnly = true;
                                    txtVehicleType.ReadOnly = true;
                                    txtDriverName.ReadOnly = true;
                                    txtTransportChallanBiltyNo.ReadOnly = true;
                                    txtDigitalLockNo.ReadOnly = true;
                                    txtDateOfDispatch.ReadOnly = true;
                                    ddlDispatchVAT.Enabled = false;
                                    txtRouteDetails.ReadOnly = true;
                                    //if (status.Value == "I")
                                    //{
                                    txtTimeOfDispatch.Attributes.Add("Disabled", "Disabled");
                                    txtDurationOfDispatch.ReadOnly = true;

                                    Image1.Visible = false;
                                    txtDateOfDispatch.Enabled = false;
                                }
                                // }
                            }
                            if (Session["rtype"].ToString() == "1" || Session["rtype"].ToString() == "3")
                            {
                                btnCancel.Visible = false;
                                btnSaveasDraft.Visible = false;
                                btnSubmit.Visible = false;
                                ddlDispatchVAT.Enabled = false;
                                ddlDispatchType.Enabled = false;
                                //  ddlUOM.Enabled = false;
                                txtQtyDispatch.ReadOnly = true;
                                txtTaxInvoiceNo1.ReadOnly = true;
                                txtBRIX.ReadOnly = true;
                                txtSugarContent.ReadOnly = true;
                                txtRemarks1.ReadOnly = true;
                                ddlYearofProduction.Enabled = false;
                                txtNameOfCarrier.ReadOnly = true;
                                txtVehicleNo.ReadOnly = true;
                                txtVehicleType.ReadOnly = true;
                                txtDriverName.ReadOnly = true;
                                txtTransportChallanBiltyNo.ReadOnly = true;
                                txtDigitalLockNo.ReadOnly = true;
                                txtDateOfDispatch.ReadOnly = true;
                                txtTimeOfDispatch.Attributes.Add("Disabled", "Disabled");
                                txtDurationOfDispatch.ReadOnly = true;
                                txtRouteDetails.ReadOnly = true;
                                Image1.Visible = false;
                                txtDateOfDispatch.Enabled = false;
                                ddlDispatchVAT.Enabled = false;
                                if (user.role_name == "Bond Officer" && pas.record_status == "Y" && Session["rtype"].ToString() == "3")
                                {
                                    ApprovalB.Visible = true;
                                    btnSaveasDraft.Visible = false;
                                    btnApprove.Visible = true;
                                    btnCancel.Visible = true;
                                    btnReject.Visible = true;
                                    // approver.Visible = true;
                                    approverremaks.Visible = true;
                                }
                                else if (user.role_name == "Bond Officer")
                                {
                                    if (status.Value == "I" || status.Value == "P" || status.Value == "M" || status.Value == "D")
                                    {
                                        ApprovalB.Visible = true;
                                        //  approverremaks.Visible = true;
                                    }
                                    btnSaveasDraft.Visible = false;
                                    txtNameOfCarrier.ReadOnly = true;
                                    txtVehicleNo.ReadOnly = true;
                                    txtVehicleType.ReadOnly = true;
                                    txtDriverName.ReadOnly = true;
                                    txtTransportChallanBiltyNo.ReadOnly = true;
                                    txtDigitalLockNo.ReadOnly = true;
                                    txtDateOfDispatch.ReadOnly = true;
                                    txtTimeOfDispatch.Attributes.Add("Disabled", "Disabled");
                                    txtDurationOfDispatch.ReadOnly = true;
                                    txtRouteDetails.ReadOnly = true;
                                    Image1.Visible = false;
                                    txtDateOfDispatch.Enabled = false;
                                    ddlDispatchVAT.Enabled = false;
                                }
                                if ( status.Value == "I" || status.Value == "P" || status.Value == "M" || status.Value == "D")
                                {
                                    // ApprovalB.Visible = true;
                                    // approverremaks.Visible = true;
                                    approver.Visible = true;
                                }
                            }
                        }
                    }
                    if (Session["ptype"].ToString() == "N" || Session["ptype"].ToString() == "NOC")
                    {
                        ReleaseRequest.Visible = false;
                        AllNo.Visible = false;
                        Supplieraddress.Visible = false;
                        Suppliername.Visible = false;
                        Depot.Visible = true;
                        Molasses.Visible = false;
                        Molasses1.Visible = false;
                        Dispatchvat.Visible = true;

                        var list = from s in dis
                                   where s.dispatch_type_name != "Pipeline"
                                   select s;
                        ddlDispatchType.DataSource = list.ToList();
                        ddlDispatchType.DataTextField = "dispatch_type_name";
                        ddlDispatchType.DataValueField = "dispatch_type_id";
                        ddlDispatchType.DataBind();
                        ddlDispatchType.Items.Insert(0, "Select");

                        ApprovalB.Visible = true;
                        if(Session["rtype"].ToString()=="0")
                        {
                            Session["Pfinancial_year"]=Session["financial_year"].ToString();
                        }
                        pass = new ReaquestForPass();
                        pass = BL_ReaquestForPass.GetRequest(Session["PassRequestID"].ToString(), Session["ptype"].ToString(), user.party_code, user.party_type, Session["Pfinancial_year"].ToString());

                        rr_noc_request_id.Value = pass.rrnoc_request_id;
                        txtReleaseRequestNo.Text = pass.rr_noc_id;
                        txtPassRequestNo.Text = pass.request_for_pass_id;
                        txtPassRequiredFor.Text = pass.product_name;
                        txtValidUpto.Text = pass.valied_date;
                        txtAllotmentrequestno.Text = pass.issue_nocno;
                        txtSupplierName.Text = pass.name;
                        depotid.Value = pass.noc_depotdetail_id;
                        txtSupplierAddress.Text = pass.address;
                        CalendarExtender3.EndDate = Convert.ToDateTime(pass.valied_date);
                        txtReleaseRequestQuantity.Text = pass.req_qty.ToString();
                        txtLiftedQuantity.Text = pass.lifted_qty.ToString();
                        txtBalanceQuantity.Text = pass.blance_qty.ToString();
                        //  txtQtyDispatch.Text = pass.req_qty.ToString();
                        party_code.Value = pass.toparty_code;
                        product_code.Value = pass.product_code;
                        txtDepot.Text = pass.depot;
                        var list1 = from s in pass.vats
                                    where s.content == pass.product_code
                                    select s;
                        ddlDispatchVAT.DataSource = list1.ToList();
                        ddlDispatchVAT.DataTextField = "vat_name";
                        ddlDispatchVAT.DataValueField = "vat_code";
                        ddlDispatchVAT.DataBind();
                        ddlDispatchVAT.Items.Insert(0, "Select");

                        if (Session["rtype"].ToString() != "0" || Session["rtype"].ToString() == "3")
                        {
                            Pass_Details pas = new Pass_Details();

                            pas = BL_Pass_Details.GetDetails(Session["pass_id"].ToString(), Session["ptype"].ToString(), Session["Pfinancial_year"].ToString());
                            ddlDispatchType.SelectedValue = pas.dispatch_type_id;
                            ddlDispatchVAT.SelectedValue = pas.to_dispatch_vat;
                            ddlDispatchVAT_SelectedIndexChanged(sender, null);
                            txtQtyDispatch.Text = pas.dispatch_qty.ToString();
                            txtTaxInvoiceNo1.Text = pas.taxinvoice;
                            txtBRIX.Text = pas.brix;
                            txtpassissuedno.Text = pas.pass_issuedno;
                            if (txtpassissuedno.Text == "")
                                issued.Visible = false;
                            txtSugarContent.Text = pas.sugar_content;
                            txtRemarks1.Text = pas.remarks;
                            txtNameOfCarrier.Text = pas.carrier;
                            txtVehicleNo.Text = pas.vehicle_no;
                            txtVehicleType.Text = pas.vehicle_type;
                            txtDriverName.Text = pas.driver;
                            balance.Value = pass.blance_qty.ToString();
                            txtTransportChallanBiltyNo.Text = pas.challan_no;
                            txtDigitalLockNo.Text = pas.digital_lock_no;
                            txtDateOfDispatch.Text = pas.dispatch_date;
                            dd1.Value= pas.dispatch_date;
                            txtTimeOfDispatch.Value = pas.dispatch_time;
                            txtDurationOfDispatch.Text = pas.dispatch_duration;
                            txtRouteDetails.Text = pas.route_details;
                            if (ddlDispatchType.SelectedItem.ToString() == "PipeLine" || ddlDispatchType.SelectedItem.ToString() == "Pipeline")
                            {
                                pipeline.Visible =false;
                            }
                            List<All_Approvals> approvals = new List<All_Approvals>();
                            approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), pas.pass_id.ToString(), "Pass");
                            if (Session["UserID"].ToString() == "Admin")
                            {
                                CalendarExtender3.Enabled = true;
                                btnCancel.Visible = true;
                                btnSaveasDraft.Visible = false;
                                btnSubmit.Visible = false;
                                ddlDispatchVAT.Enabled = true;
                                ddlDispatchType.Enabled = false;
                                approver.Visible = true;
                                ApprovalB.Visible = true;
                                btnSaveasDraft.Visible = false;
                                txtNameOfCarrier.ReadOnly = false;
                                txtVehicleNo.ReadOnly = false;
                                txtVehicleType.ReadOnly = false;
                                txtDriverName.ReadOnly = false;
                                txtTransportChallanBiltyNo.ReadOnly = false;
                                txtDigitalLockNo.ReadOnly = false;
                                txtDateOfDispatch.ReadOnly = false;
                                txtDigitalLockNo.ReadOnly = false;
                                txtDateOfDispatch.ReadOnly = false;
                                txtDurationOfDispatch.ReadOnly = false;
                                txtRouteDetails.ReadOnly = false;
                                Image1.Visible = true;
                                txtQtyDispatch.ReadOnly = true;
                                txtTaxInvoiceNo1.ReadOnly = false;
                                txtBRIX.ReadOnly = true;
                                txtSugarContent.ReadOnly = true;
                                txtRemarks1.ReadOnly = true;
                                ddlYearofProduction.Enabled = false;

                               
                                btnupdate.Visible = true;
                                var list4 = (from s in approvals
                                             where s.user_id == "Admin"
                                             select s);
                                grdApprovalDetails.DataSource = list4.ToList();
                                grdApprovalDetails.DataBind();

                            }
                            else
                            {
                                var list4 = (from s in approvals
                                             where s.financial_year == user.financial_year
                                             select s);
                                grdApprovalDetails.DataSource = list4.ToList();
                                grdApprovalDetails.DataBind();
                            }
                              
                          
                            if (approvals.Count > 0 || status.Value == "I" || status.Value == "P" || status.Value == "M" || status.Value == "D")
                            {
                                if (Session["UserID"].ToString() != "Admin")
                                {
                                    //approver.Visible = true;
                                    //ApprovalB.Visible = true;
                                    //btnSaveasDraft.Visible = false;
                                    //txtNameOfCarrier.ReadOnly = true;
                                    //txtVehicleNo.ReadOnly = true;
                                    //txtVehicleType.ReadOnly = true;
                                    //txtDriverName.ReadOnly = true;
                                    //txtTransportChallanBiltyNo.ReadOnly = true;
                                    //txtDigitalLockNo.ReadOnly = true;
                                    //txtDateOfDispatch.ReadOnly = true;
                                    //txtTimeOfDispatch.Attributes.Add("Disabled", "Disabled");
                                    //txtDurationOfDispatch.ReadOnly = true;
                                    //txtRouteDetails.ReadOnly = true;
                                    //Image1.Visible = false;
                                    //txtDateOfDispatch.Enabled = false;
                                    //ddlDispatchVAT.Enabled = false;
                                }
                            }
                            //if (user.role_name == "Bond Officer" && pas.record_status == "Y" && Session["rtype"].ToString() == "3")
                            //{
                            //    ApprovalB.Visible = true;
                            //    btnSaveasDraft.Visible = false;
                            //    btnApprove.Visible = true;
                            //}
                            //else if (user.role_name == "Bond Officer")
                            //{
                            //    ApprovalB.Visible = true;
                            //    btnSaveasDraft.Visible = false;
                            //}

                            if (Session["rtype"].ToString() == "1" || Session["rtype"].ToString() == "3")
                            {
                                btnCancel.Visible = false;
                                btnSaveasDraft.Visible = false;
                                btnSubmit.Visible = false;
                                ddlYearofProduction.SelectedValue = pas.prev_prod_year;
                                ddlDispatchVAT.SelectedValue = pas.to_dispatch_vat;
                                ddlDispatchVAT_SelectedIndexChanged(sender, null);
                                ddlDispatchType.Enabled = false;
                                // ddlUOM.Enabled = false;
                                txtQtyDispatch.ReadOnly = true;
                                txtTaxInvoiceNo1.ReadOnly = true;
                                txtBRIX.ReadOnly = true;
                                txtSugarContent.ReadOnly = true;
                                txtRemarks1.ReadOnly = true;
                                ddlYearofProduction.Enabled = false;
                                ddlDispatchVAT.Enabled = false;
                                btnSaveasDraft.Visible = false;

                              
                                ApprovalB.Visible = true;
                                if (user.role_name == "Bond Officer" && pas.record_status == "Y" && Session["rtype"].ToString() == "3")
                                {
                                    ApprovalB.Visible = true;
                                    btnSaveasDraft.Visible = false;
                                    btnApprove.Visible = true;
                                    btnCancel.Visible = true;
                                    btnReject.Visible = true;
                                    approverremaks.Visible = true;
                                }

                                else if (user.role_name == "Bond Officer" || status.Value == "I" || status.Value == "P" || status.Value == "M" || status.Value == "D")
                                {
                                     if (status.Value == "I" || status.Value == "P" || status.Value == "M" || status.Value == "D")
                                        {
                                            ApprovalB.Visible = true;
                                            //  approverremaks.Visible = true;
                                        }
                                       
                                    btnSaveasDraft.Visible = false;
                                    txtNameOfCarrier.ReadOnly = true;
                                    txtVehicleNo.ReadOnly = true;
                                    txtVehicleType.ReadOnly = true;
                                    txtDriverName.ReadOnly = true;
                                    txtTransportChallanBiltyNo.ReadOnly = true;
                                    txtDigitalLockNo.ReadOnly = true;
                                    txtDateOfDispatch.ReadOnly = true;
                                    txtTimeOfDispatch.Attributes.Add("Disabled", "Disabled");
                                    txtDurationOfDispatch.ReadOnly = true;
                                    txtRouteDetails.ReadOnly = true;
                                    Image1.Visible = false;
                                    txtDateOfDispatch.Enabled = false;
                                    ddlDispatchVAT.Enabled = false;
                                }
                                else
                                {
                                    txtNameOfCarrier.ReadOnly = true;
                                    txtVehicleNo.ReadOnly = true;
                                    txtVehicleType.ReadOnly = true;
                                    txtDriverName.ReadOnly = true;
                                    txtTransportChallanBiltyNo.ReadOnly = true;
                                    txtDigitalLockNo.ReadOnly = true;
                                    txtDateOfDispatch.ReadOnly = true;
                                    txtTimeOfDispatch.Attributes.Add("Disabled", "Disabled");
                                    txtDurationOfDispatch.ReadOnly = true;
                                    txtRouteDetails.ReadOnly = true;
                                    // Image1.Visible = false;
                                    txtDateOfDispatch.Enabled = false;
                                    ddlDispatchVAT.Enabled = false;
                                }
                                if (pas.record_status == "I"  || status.Value == "P" || status.Value == "M" || status.Value == "D")
                                {
                                    // ApprovalB.Visible = true;
                                    //   approverremaks.Visible = true;
                                    approver.Visible = true;
                                }
                            }
                        }
                    }

                }
            }
        }
         


        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("PassList");
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

        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                btnSaveasDraft.Enabled = false;
                Pass_Details pass = new Pass_Details();
                pass.pass_for =product_code.Value;
               // pass.rrnoc_record_request_id = Session["PassRequestID"].ToString();
               
                if(Session["ptype"].ToString()=="M"|| Session["ptype"].ToString() == "RR")
                {
                    pass.party_code = party_code.Value;
                    pass.supplier_unit = userparty.Value;
                    pass.customer_id ="";
                    pass.noc_depotdetail_id ="";
                    pass.to_dispatch_vat ="";
                    pass.product_code = product_code.Value;

                }
                else
                {
                    pass.party_code = userparty.Value;
                    pass.customer_id = party_code.Value;
                    pass.supplier_unit = userparty.Value;
                    pass.noc_depotdetail_id =depotid.Value;
                   
                }
                pass.dispatch_type_id = ddlDispatchType.SelectedValue;
                pass.to_dispatch_vat = ddlDispatchVAT.SelectedValue;
                pass.dispatch_qty = Convert.ToDouble(txtQtyDispatch.Text);
                //if (ddlUOM.SelectedValue == "Select")
                //    ddlUOM.SelectedValue = uom.Value;
                //pass.uom_code = ddlUOM.SelectedValue;
                pass.prev_prod_year = ddlYearofProduction.SelectedItem.ToString();
                pass.brix = txtBRIX.Text;
                pass.sugar_content = txtSugarContent.Text;
                pass.taxinvoice = txtTaxInvoiceNo1.Text;
                pass.remarks = txtRemarks1.Text;
                pass.user_id = Session["UserID"].ToString();
                pass.record_status = "N";

                pass.carrier = txtNameOfCarrier.Text;
                pass.vehicle_no = txtVehicleNo.Text;
                pass.vehicle_type = txtVehicleType.Text;
                pass.driver = txtDriverName.Text;
                pass.challan_no = txtTransportChallanBiltyNo.Text;
                pass.digital_lock_no = txtDigitalLockNo.Text;
                pass.dispatch_date = dd1.Value;
                pass.dispatch_time = txtTimeOfDispatch.Value;
                pass.dispatch_duration = txtDurationOfDispatch.Text;
                pass.route_details = txtRouteDetails.Text;

                pass.allotment_validupto = txtValidUpto.Text;
                pass.pass_type = Session["ptype"].ToString();
                pass.request_for_pass_id = txtPassRequestNo.Text;
                pass.rrnoc_record_request_id = rr_noc_request_id.Value;
              pass.financial_year=  Session["financial_year"].ToString();

                pass.lifted_qty = (Convert.ToDouble(txtLiftedQuantity.Text) + Convert.ToDouble(txtQtyDispatch.Text)).ToString();
                //pass.lifted_qty =(Convert.ToDouble(balance.Value) - Convert.ToDouble(txtQtyDispatch.Text)).ToString();


                string val;
                if(Session["rtype"].ToString()=="0")
                {
                    val = BL_Pass_Details.Insert(pass);
                    
                }
                else
                {
                    pass.pass_id = Session["pass_id"].ToString();
                    val = BL_Pass_Details.Update(pass);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("PassList");


                }
                else
                {
                    btnSaveasDraft.Enabled = true;
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                btnSubmit.Enabled = false;
                Pass_Details pass = new Pass_Details();
                pass.pass_for = product_code.Value;
              //  pass.rrnoc_record_request_id = Session["PassRequestID"].ToString();
             //   pass.party_code = userparty.Value;
                if (Session["ptype"].ToString() == "M" || Session["ptype"].ToString() == "RR")
                {
                    pass.party_code = party_code.Value; ;
                    pass.supplier_unit = userparty.Value;
                    pass.customer_id = "";
                    pass.noc_depotdetail_id = "";
                    pass.to_dispatch_vat = "";
                    pass.product_code = product_code.Value;

                }
                else
                {
                    pass.party_code = userparty.Value;
                    pass.customer_id = party_code.Value;
                    pass.supplier_unit = userparty.Value;
                    pass.noc_depotdetail_id = depotid.Value;

                }
                pass.dispatch_type_id = ddlDispatchType.SelectedValue;
                pass.to_dispatch_vat = ddlDispatchVAT.SelectedValue;
                pass.dispatch_qty = Convert.ToDouble(txtQtyDispatch.Text);
                //if (ddlUOM.SelectedValue == "Select")
                //    ddlUOM.SelectedValue = uom.Value;
                //pass.uom_code = ddlUOM.SelectedValue;
                pass.prev_prod_year = ddlYearofProduction.SelectedItem.ToString();
                pass.brix = txtBRIX.Text;
                pass.sugar_content = txtSugarContent.Text;
                pass.taxinvoice = txtTaxInvoiceNo1.Text;
                pass.remarks = txtRemarks1.Text;
                pass.user_id = Session["UserID"].ToString();
                pass.record_status = "Y";
                pass.carrier = txtNameOfCarrier.Text;
                pass.vehicle_no = txtVehicleNo.Text;
                pass.vehicle_type = txtVehicleType.Text;
                pass.driver = txtDriverName.Text;
                pass.challan_no = txtTransportChallanBiltyNo.Text;
                pass.digital_lock_no = txtDigitalLockNo.Text;
                pass.dispatch_date = dd1.Value;
                pass.dispatch_time = txtTimeOfDispatch.Value;
                pass.dispatch_duration = txtDurationOfDispatch.Text;
                pass.route_details = txtRouteDetails.Text;

                pass.allotment_validupto = txtValidUpto.Text;
                pass.pass_type = Session["ptype"].ToString();
                pass.request_for_pass_id = Session["PassRequestID"].ToString();
                pass.rrnoc_record_request_id = rr_noc_request_id.Value;
                pass.lifted_qty = (Convert.ToDouble(txtLiftedQuantity.Text) + Convert.ToDouble(txtQtyDispatch.Text)).ToString();
                pass.financial_year = Session["financial_year"].ToString();
                string val;
                if (Session["rtype"].ToString() == "0")
                {
                    val = BL_Pass_Details.Insert(pass);

                }
                else
                {
                    pass.pass_id = Session["pass_id"].ToString();
                    val = BL_Pass_Details.Update(pass);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("PassList");
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

        protected void ddlDispatchVAT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["rtype"].ToString() == "0")
            {
                Session["Pfinancial_year"] = Session["financial_year"].ToString();
            }
            string val = BL_Pass_Details.GetAvailableQTY(ddlDispatchVAT.SelectedValue, Session["Pfinancial_year"].ToString());
            string[] va = val.Split('_');
            if(va.Length==2)
                txtreservedqty.Text = va[1];
          if(va[0].ToString()=="")
            {
                double available = 0;
                txtAvailableQty.Text = available.ToString();
            }
          else
            {
              
                double available = Convert.ToDouble(va[0]) - Convert.ToDouble(txtreservedqty.Text);
                txtAvailableQty.Text = available.ToString();
            }
           
            //double avialable = Convert.ToDouble(txtAvailableQty.Text);
            //double disqty = Convert.ToDouble(txtQtyDispatch.Text);
            //if(avialable<disqty)
            //{
            //    ddlDispatchVAT.SelectedIndex=0;
            //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //    sb.Append("<script type = 'text/javascript'>");
            //    sb.Append("window.onload=function(){");
            //    sb.Append("alert('");
            //    sb.Append("Dispatch qty not morethan of Available QTY");
            //    sb.Append("')};");
            //    sb.Append("</script>");
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
            //}
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                List<Pass_Details> pass1 = new List<Pass_Details>();
                btnApprove.Enabled = false;
                Pass_Details pass = new Pass_Details();
                pass.carrier = txtNameOfCarrier.Text;
                pass.vehicle_no = txtVehicleNo.Text;
                pass.vehicle_type = txtVehicleType.Text;
                pass.driver = txtDriverName.Text;
                pass.challan_no = txtTransportChallanBiltyNo.Text;
                pass.digital_lock_no = txtDigitalLockNo.Text;
                pass.dispatch_date =dd1.Value;
                pass.dispatch_time = txtTimeOfDispatch.Value;
                pass.dispatch_duration = txtDurationOfDispatch.Text;
                pass.route_details = txtRouteDetails.Text;
                pass.pass_id = Session["pass_id"].ToString();
                pass.request_for_pass_id = txtPassRequestNo.Text;
                pass.party_code = userparty.Value;
                pass.to_dispatch_vat =ddlDispatchVAT.SelectedValue;
                pass.available_qty =(Convert.ToDouble(txtAvailableQty.Text)-Convert.ToDouble(txtQtyDispatch.Text)).ToString();
                pass.record_status = "I";
                pass.pass_type = Session["ptype"].ToString();
                pass.rrnoc_record_request_id = rr_noc_request_id.Value;
                pass.noc_depotdetail_id = depotid.Value;
                pass.dispatch_qty =Convert.ToDouble( txtQtyDispatch.Text);
                pass.approver_remarks = txtApproverremarks.Value;
                pass.user_id = Session["UserID"].ToString();
                pass.financial_year = Session["financial_year"].ToString();
                if (ddlDispatchType.SelectedValue == "3")
                    pass.route_details = "Pipeline";
             pass1.Add(pass);
                int value = BL_Molasses_Allocation.GetDigitalsignature(Session["UserID"].ToString());
                if (value == 1)
                {
                    Session["carrier"] = pass.carrier;
                    Session["vehicle"] = pass.vehicle_no;
                    Session["vehicle_type"] = pass.vehicle_type;
                    Session["driver"] = pass.driver;
                    Session["digital_lock_no"] = pass.digital_lock_no;
                    Session["dispatch_date"] = pass.dispatch_date;
                    Session["dispatch_time"] = pass.dispatch_time;
                    Session["dispatch_duration"] = pass.dispatch_duration;
                    Session["route_details"] = pass.route_details;
                    Session["request_for_pass_id"] = pass.request_for_pass_id;
                    Session["party_code"] = pass.party_code;
                    Session["to_dispatch_vat"] = pass.to_dispatch_vat;
                    Session["available_qty"] = pass.available_qty;
                    Session["rrnoc_record_request_id"] = pass.rrnoc_record_request_id;
                    Session["noc_depotdetail_id"] = pass.noc_depotdetail_id;
                    Session["dispatch_qty"] = pass.dispatch_qty;
                    Session["approver_remarks"] = pass.approver_remarks;
                    Session["challan_no"] = pass.challan_no;
                    Session["pass"] = pass1;
                    Response.Redirect("HtmlPage4.html");
                }
                else
                {
                string val = BL_Pass_Details.Issue(pass);

                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("PassList");
                }
                else
                {
                    btnApprove.Enabled = true;
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
            Response.Redirect("PassList");
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Pass_Details pass = new Pass_Details();
                pass.pass_id = Session["pass_id"].ToString();
                pass.record_status = "R";
                pass.user_id = Session["UserID"].ToString();
                pass.approver_remarks = txtApproverremarks.Value;
                pass.dispatch_qty = Convert.ToDouble(txtQtyDispatch.Text);
                pass.request_for_pass_id = txtPassRequestNo.Text;
                pass.party_code = userparty.Value;
                pass.financial_year = Session["financial_year"].ToString();
                string val = "";
             //   pass.request_for_pass_id = Session["RequestID"].ToString();
                val = BL_Pass_Details.Issue(pass);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("PassList");
                }
                else
                {
                    btnApprove.Enabled = true;
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

        protected void ddlDispatchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDispatchType.SelectedItem.ToString() == "Pipeline" || ddlDispatchType.SelectedItem.ToString() == "PipeLine")
            {

                pipeline.Visible = false;
            }
            else
            {
                pipeline.Visible = true;
            }

        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                List<Pass_Details> pass1 = new List<Pass_Details>();
                btnApprove.Enabled = false;
                Pass_Details pass = new Pass_Details();
                pass.carrier = txtNameOfCarrier.Text;
                pass.vehicle_no = txtVehicleNo.Text;
                pass.taxinvoice = txtTaxInvoiceNo1.Text;
                pass.vehicle_type = txtVehicleType.Text;
                pass.driver = txtDriverName.Text;
                pass.challan_no = txtTransportChallanBiltyNo.Text;
                pass.digital_lock_no = txtDigitalLockNo.Text;
                pass.dispatch_date = dd1.Value;
                pass.dispatch_time = txtTimeOfDispatch.Value;
                pass.dispatch_duration = txtDurationOfDispatch.Text;
                pass.route_details = txtRouteDetails.Text;
                pass.pass_id = Session["pass_id"].ToString();
                pass.request_for_pass_id = txtPassRequestNo.Text;
                pass.party_code = userparty.Value;
                pass.to_dispatch_vat = ddlDispatchVAT.SelectedValue;
                pass.available_qty = (Convert.ToDouble(txtAvailableQty.Text) - Convert.ToDouble(txtQtyDispatch.Text)).ToString();
                pass.record_status = "I";
                pass.pass_type = Session["ptype"].ToString();
                pass.rrnoc_record_request_id = rr_noc_request_id.Value;
                pass.noc_depotdetail_id = depotid.Value;
                pass.dispatch_qty = Convert.ToDouble(txtQtyDispatch.Text);
                pass.approver_remarks = txtApproverremarks.Value;
                pass.financial_year = Session["Pfinancial_year"].ToString();
                pass.user_id = Session["UserID"].ToString();
                if (ddlDispatchType.SelectedValue == "3")
                    pass.route_details = "Pipeline";
                pass1.Add(pass);
             
                    string val = BL_Pass_Details.Adminupdate(pass);

                    if (val == "0")
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("PassList");
                    }
                    else
                    {
                        btnApprove.Enabled = true;
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
}