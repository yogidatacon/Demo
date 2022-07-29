using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class DailyDispatchClosureForm : System.Web.UI.Page
    {
        List<DailyDispatchClosure> dispatch = new List<DailyDispatchClosure>();

        DailyDispatchClosure dispatchs = new DailyDispatchClosure();
        List<StorageToDispatch> Sto = new List<StorageToDispatch>();
        UserDetails user = new UserDetails();
        static string _party_code;

        double total = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDateofRemoval.ReadOnly = true;
                CalendarExtender.EndDate = DateTime.Now;
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
               
                Session["UserID"] = Session["UserID"];

                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                VAT_Master vats = new VAT_Master();
                string userid = Session["UserID"].ToString();
                    if(user != null)
                    {
                        Session["Rolename"] = user.role_name;
                        Session["Partycode"] = user.party_code;
                        Session["financial_year"] = user.financial_year;
                    Sto = BL_StorageToDispatch.GetList();
                party_code.Value = Session["Partycode"].ToString();
               // _party_code = party_code.Value;

                //vat = new List<VAT_Master>();
                //vat = BL_DailyDispatchClosure.GetVatName(userid);
                //ddDispatchVAT.DataSource = vat;
                //ddDispatchVAT.DataValueField = "vat_code";
                //ddDispatchVAT.DataTextField = "vat_name";
                //ddDispatchVAT.DataBind();
                //ddDispatchVAT.Items.Insert(0, "select");
                btnApprove.Visible = false;
                btnReject.Visible = false;
                approverid.Visible = true;
                approverremarks.Visible = false;
                vats.vat_code = ddDispatchVAT.SelectedValue;
                if (Session["UserID"].ToString() == "Admin")
                {
                    btnSaveasDraft.Visible = true;
                    btnSubmit.Visible = true;
                    btnCancel.Visible = true;
                    btnApprove.Visible = true;
                    btnReject.Visible = true;
                }
                else if (Session["Rolename"].ToString() == "Bond Officer")
                {
                    btnSaveasDraft.Visible = false;
                    btnSubmit.Visible = false;
                    btnCancel.Visible = false;
                    btnApprove.Visible = true;
                    btnReject.Visible = true;
                }
                else
                {
                    btnSaveasDraft.Visible = true;
                    btnSubmit.Visible = true;
                    btnCancel.Visible = true;
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                }

                if (Session["rtype"].ToString() != "0")
                {
                    dispatchs = new DailyDispatchClosure(); 
                    dispatchs = BL_DailyDispatchClosure.GetDetails(party_code.Value, Convert.ToInt32(Session["dailydispatchclosure_id"].ToString()), Session["Dfinancial_year"].ToString());
                    txtDateofRemoval.Text = dispatchs.closure_date;
                            txtdob.Value = dispatchs.closure_date;
                            CalendarExtender.SelectedDate = Convert.ToDateTime(dispatchs.closure_date);
                            if(dispatchs.record_status == "N" || dispatchs.record_status == "R")
                            {
                                Sto = BL_StorageToDispatch.GetDEN(party_code.Value, dispatchs.closure_date);

                                var list1 = from s in Sto
                                            select s;
                                ddDispatchVAT.DataSource = list1.ToList();
                                ddDispatchVAT.DataTextField = "dispatchvat";
                                ddDispatchVAT.DataValueField = "to_dispatchvat";
                                ddDispatchVAT.DataBind();
                                ddDispatchVAT.SelectedValue = dispatchs.from_dispatchvat;
                                
                            }
                                
                            if (dispatchs.record_status == "Y" || dispatchs.record_status == "A" )
                            {
                                Sto = BL_StorageToDispatch.GetsubmitedVat(party_code.Value, dispatchs.closure_date);
                                var list3 = from s in Sto
                                            select s;
                                ddDispatchVAT.DataSource = list3.ToList();
                                ddDispatchVAT.DataTextField = "dispatchvat";
                                ddDispatchVAT.DataValueField = "to_dispatchvat";
                                ddDispatchVAT.DataBind();
                                ddDispatchVAT.SelectedValue = dispatchs.from_dispatchvat;
                                List<StorageToDispatch> St1 = new List<StorageToDispatch>();
                                St1 = BL_StorageToDispatch.GetDENList(party_code.Value, dispatchs.closure_date, dispatchs.from_dispatchvat);
                                var list5 = from s in St1
                                            where s.record_status == "D"
                                            select s;
                                grdDispatchvat.DataSource = list5.ToList();
                                grdDispatchvat.DataBind();
                            }
                            else { 

                            List<StorageToDispatch> Sto1 = new List<StorageToDispatch>();
                            Sto1 = BL_StorageToDispatch.GetDENList(party_code.Value, dispatchs.closure_date, ddDispatchVAT.SelectedValue);
                            var list2 = from s in Sto1
                                        where s.record_status == "I"
                                        select s;
                            grdDispatchvat.DataSource = list2.ToList();
                            grdDispatchvat.DataBind();
                            }



                            // ddDispatchVAT.SelectedValue = dispatchs.from_dispatchvat;
                            txtDispatchQty.Text = dispatchs.dispatchqty.ToString();
                    txtDipsinWetInches.Text = dispatchs.dips.ToString();
                    txtTemperature.Text = dispatchs.temperature.ToString();
                    txtIndication.Text = dispatchs.indication.ToString();
                    txtStrength.Text = dispatchs.strength.ToString();
                    txtDecreasbyReduction.Text = dispatchs.dec_reduction.ToString();
                            DBRED.Value = dispatchs.dec_reduction.ToString();
                            txtDecreasbyBlending.Text = dispatchs.dec_blending.ToString();
                            DBB.Value = dispatchs.dec_blending.ToString();
                            txtDecreasRacking.Text = dispatchs.dec_racking.ToString();
                            DBR.Value = dispatchs.dec_racking.ToString();
                            txtDecreasbyWastage.Text = dispatchs.dec_wastage.ToString();
                            DBW.Value = dispatchs.dec_wastage.ToString();
                            txtRemarks.Text = dispatchs.remarks;
                    txtIncreaseBLInOperation.Text = dispatchs.txtIncreaseBLInOperation.ToString();
                            IBO.Value = dispatchs.txtIncreaseBLInOperation.ToString();
                            txtIncreaseBLByGroging.Text = dispatchs.IncreaseBLByGroging.ToString();
                            IBG.Value = dispatchs.IncreaseBLByGroging.ToString();
                            txtBalanceBLQty.Text = dispatchs.bl_balanceqty.ToString();
                    txtBalanceLPQty.Text = dispatchs.lp_balanceqty.ToString();
                    ReceiverToStoragrTransfer aval = new ReceiverToStoragrTransfer();
                    aval = BL_ReceiverToStoragrTransfer.GetVatData(dispatchs.from_dispatchvat, party_code.Value);
                    txtAvailableQtyBL.Text = aval.vat_availablecapacity.ToString();
                    //txtAvailableQtyBL.Text = avalableqty.ToString();
                   txtAvailableQtyLPL.Text = (Convert.ToDouble(txtAvailableQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                    //txtBalanceBLQty.Text = balaqty.ToString();
                    //txtBalanceLPQty.Text = balanceLp.ToString();
                    List<All_Approvals> approvals = new List<All_Approvals>();
                    approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), dispatchs.dailydispatchclosure_id.ToString(), "DDC");
                            //  grdApprovalDetails.DataSource = approvals;
                            var list4 = (from s in approvals
                                        where s.financial_year == Session["Dfinancial_year"].ToString()
                                         select s);
                            grdApprovalDetails.DataSource = list4.ToList();
                            grdApprovalDetails.DataBind();
                    if (Session["rtype"].ToString() == "1")
                    {
                        txtDateofRemoval.Attributes.Add("disabled", "disabled");
                        ddDispatchVAT.Enabled = false;
                        txtDispatchQty.Enabled = false;
                        txtDipsinWetInches.Enabled = false;
                        txtTemperature.Enabled = false;
                        txtIndication.Enabled = false;
                        txtStrength.Enabled = false;
                        txtDecreasbyReduction.Enabled = false;
                        txtDecreasbyBlending.Enabled = false;
                        txtDecreasRacking.Enabled = false;
                        txtDecreasbyWastage.Enabled = false;
                        txtRemarks.Attributes.Add("disabled", "disabled");
                                txtIncreaseBLByGroging.Enabled = false;
                                txtIncreaseBLInOperation.Enabled = false;
                                btnSaveasDraft.Visible = false;
                        btnSubmit.Visible = false;
                        btnCancel.Visible = false;
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        if (Session["Rolename"].ToString() == "Bond Officer" && dispatchs.record_status == "Y")
                        {
                            btnSaveasDraft.Visible = false;
                            btnSubmit.Visible = false;
                            btnCancel.Visible = false;
                            btnApprove.Visible = true;
                            btnReject.Visible = true;
                            approverid.Visible = true;
                            approverremarks.Visible = true;
                            txtApproverremarks.Visible = true;
                        }
                        if (dispatchs.record_status == "A" || dispatchs.record_status == "R")
                        {
                            approverid.Visible = true;
                            approverremarks.Visible = false;

                            btnSaveasDraft.Visible = false;
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
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/LoginPage");
                }
            }
        }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DailyDispatchClosureList");
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
        protected void btnVATtansfers_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATTransferList");
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
        protected void txtDateofRemoval_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if(txtdob.Value !="")
                {
                    txtDateofRemoval.Text= txtdob.Value;
                    string date = txtdob.Value;
                    Sto = BL_StorageToDispatch.GetDEN(party_code.Value, date);

                    var list1 = from s in Sto

                                select s;
                    ddDispatchVAT.DataSource = list1.ToList();
                    ddDispatchVAT.DataTextField = "dispatchvat";
                    ddDispatchVAT.DataValueField = "to_dispatchvat";
                    ddDispatchVAT.DataBind();
                    ddDispatchVAT.Items.Insert(0, "Select");
                    //  grd();
                    //List<StorageToDispatch> Sto1 = new List<StorageToDispatch>();
                    //Sto1 = BL_StorageToDispatch.GetDENList1(party_code.Value, date);
                    //var list2 = from s in Sto1
                    //            where s.record_status == "I"
                    //            select s;
                    //grdDispatchvat.DataSource = list2.ToList();
                    //grdDispatchvat.DataBind();

                    //StorageToDispatch Sto2 = new StorageToDispatch();
                    //Sto2 = BL_StorageToDispatch.GetDispatchqty(party_code.Value, date);
                    //ddDispatchVAT.SelectedValue = Sto2.to_dispatchvat;
                    //txtDispatchQty.Text = Sto2.dispatchqty.ToString();
                    //txtBalanceBLQty.Text = Sto2.dispatchqty.ToString();
                    //ddDispatchVAT.Enabled = false;

                    //string vatcode = ddDispatchVAT.SelectedValue;
                    //ReceiverToStoragrTransfer aval = new ReceiverToStoragrTransfer();
                    //aval = BL_ReceiverToStoragrTransfer.GetVatData(vatcode, party_code.Value);
                    //txtAvailableQtyBL.Text = aval.vat_availablecapacity.ToString();
                }
            }
           
        }

        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string userid = Session["UserID"].ToString();
                DailyDispatchClosure dispatchs = new DailyDispatchClosure();
                if (txtDateofRemoval.Text == "" || txtDateofRemoval.Text != "")
                {
                    txtDateofRemoval.Text = txtdob.Value;
                }
                dispatchs.party_code = party_code.Value;
                dispatchs.closure_date = Convert.ToDateTime(txtDateofRemoval.Text).ToString("dd-MM-yyyy");
                dispatchs.from_dispatchvat = ddDispatchVAT.SelectedValue;
                dispatchs.dispatchqty = Convert.ToDouble(txtDispatchQty.Text);
                dispatchs.dips = Convert.ToDouble(txtDipsinWetInches.Text);
                dispatchs.temperature = Convert.ToDouble(txtTemperature.Text);
                dispatchs.indication = Convert.ToDouble(txtIndication.Text);
                dispatchs.strength = Convert.ToDouble(txtStrength.Text);
               
                if (txtDecreasbyReduction.Text != "")
                {
                    dispatchs.dec_reduction = Convert.ToDouble(txtDecreasbyReduction.Text);
                }
                if (txtDecreasbyBlending.Text != "")
                {
                    dispatchs.dec_blending = Convert.ToDouble(txtDecreasbyBlending.Text);
                }
                if (txtDecreasRacking.Text != "")
                {
                    dispatchs.dec_racking = Convert.ToDouble(txtDecreasRacking.Text);
                }
                if (txtDecreasbyWastage.Text != "")
                {
                    dispatchs.dec_wastage = Convert.ToDouble(txtDecreasbyWastage.Text);
                }
                if (txtIncreaseBLByGroging.Text != "")
                    dispatchs.IncreaseBLByGroging = Convert.ToDouble(txtIncreaseBLByGroging.Text);
                if (txtIncreaseBLInOperation.Text != "")
                    dispatchs.txtIncreaseBLInOperation = Convert.ToDouble(txtIncreaseBLInOperation.Text);
                dispatchs.bl_balanceqty= Convert.ToDouble(txtBalanceBLQty.Text);
                dispatchs.lp_balanceqty= Convert.ToDouble(txtBalanceLPQty.Text);
                dispatchs.remarks = txtRemarks.Text;
               dispatchs.financial_year= Session["financial_year"].ToString();
                dispatchs.user_id = userid;
                dispatchs.record_status = "N";


                string val;

                if (Session["rtype"].ToString() == "0")
                    val = BL_DailyDispatchClosure.InsertDispatchClosure(dispatchs).ToString();
                else
                {
                    dispatchs.dailydispatchclosure_id = Convert.ToInt32(Session["dailydispatchclosure_id"].ToString());
                    val = BL_DailyDispatchClosure.UpdateDispatchClosure(dispatchs).ToString();
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("DailyDispatchClosureList");
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string userid = Session["UserID"].ToString();

                DailyDispatchClosure dispatchs = new DailyDispatchClosure();
                if (txtDateofRemoval.Text == "" || txtDateofRemoval.Text != "")
                {
                    txtDateofRemoval.Text = txtdob.Value;
                }
                dispatchs.closure_date = Convert.ToDateTime(txtDateofRemoval.Text).ToString("dd-MM-yyyy");
                dispatchs.from_dispatchvat = ddDispatchVAT.SelectedValue;
                dispatchs.party_code = party_code.Value;
                dispatchs.dispatchqty = Convert.ToDouble(txtDispatchQty.Text);
                dispatchs.dips = Convert.ToDouble(txtDipsinWetInches.Text);
                dispatchs.temperature = Convert.ToDouble(txtTemperature.Text);
                dispatchs.indication = Convert.ToDouble(txtIndication.Text);
                dispatchs.strength = Convert.ToDouble(txtStrength.Text);
                if (txtDecreasbyReduction.Text != "")
                {
                    dispatchs.dec_reduction = Convert.ToDouble(txtDecreasbyReduction.Text);
                }
                if (txtDecreasbyBlending.Text != "")
                {
                    dispatchs.dec_blending = Convert.ToDouble(txtDecreasbyBlending.Text);
                }
                if (txtDecreasRacking.Text != "")
                {
                    dispatchs.dec_racking = Convert.ToDouble(txtDecreasRacking.Text);
                }
                if (txtDecreasbyWastage.Text != "")
                {
                    dispatchs.dec_wastage = Convert.ToDouble(txtDecreasbyWastage.Text);
                }
                if (txtIncreaseBLByGroging.Text != "")
                    dispatchs.IncreaseBLByGroging = Convert.ToDouble(txtIncreaseBLByGroging.Text);
                else
                    dispatchs.IncreaseBLByGroging = 0;
                if (txtIncreaseBLInOperation.Text != "")
                    dispatchs.txtIncreaseBLInOperation = Convert.ToDouble(txtIncreaseBLInOperation.Text);
                else
                    dispatchs.txtIncreaseBLInOperation =0;
                dispatchs.remarks = txtRemarks.Text;
                dispatchs.bl_balanceqty = Convert.ToDouble(txtBalanceBLQty.Text);
                dispatchs.lp_balanceqty = Convert.ToDouble(txtBalanceLPQty.Text);
                dispatchs.user_id = userid;
                dispatchs.financial_year = Session["financial_year"].ToString();
                dispatchs.record_status = "Y";
                string val;
                if (Session["rtype"].ToString() == "0")
                    val = BL_DailyDispatchClosure.InsertDispatchClosure(dispatchs).ToString();
                else
                {
                    dispatchs.dailydispatchclosure_id = Convert.ToInt32(Session["dailydispatchclosure_id"].ToString());
                    val = BL_DailyDispatchClosure.UpdateDispatchClosure(dispatchs).ToString();
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("DailyDispatchClosureList");
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
                DailyDispatchClosure dispatchs = new DailyDispatchClosure();

                dispatchs.record_status = "R";
                string val;
                dispatchs.dailydispatchclosure_id = Convert.ToInt32(Session["dailydispatchclosure_id"].ToString());
                dispatchs.remarks = txtApproverremarks.Value;
                dispatchs.user_id = Session["UserID"].ToString();
                dispatchs.financial_year = Session["financial_year"].ToString();
                dispatchs.closure_date = Convert.ToDateTime(txtDateofRemoval.Text).ToString("dd-MM-yyyy");
                dispatchs.from_dispatchvat = ddDispatchVAT.SelectedValue;
                val = BL_DailyDispatchClosure.Approve(dispatchs);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("DailyDispatchClosureList");
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


        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                DailyDispatchClosure dispatchs = new DailyDispatchClosure();
                dispatchs.record_status = "A";
                string val;
                dispatchs.dailydispatchclosure_id = Convert.ToInt32(Session["dailydispatchclosure_id"].ToString());
                dispatchs.remarks = txtApproverremarks.Value;
                dispatchs.user_id = Session["UserID"].ToString();
                dispatchs.party_code = party_code.Value;
                dispatchs.closure_date = Convert.ToDateTime(txtDateofRemoval.Text).ToString("dd-MM-yyyy");
                dispatchs.from_dispatchvat = ddDispatchVAT.SelectedValue;
                dispatchs.dispatchqty = Convert.ToDouble(txtDispatchQty.Text);
                dispatchs.dips = Convert.ToDouble(txtDipsinWetInches.Text);
                dispatchs.temperature = Convert.ToDouble(txtTemperature.Text);
                dispatchs.financial_year = Session["financial_year"].ToString();
                dispatchs.indication = Convert.ToDouble(txtIndication.Text);
                dispatchs.strength = Convert.ToDouble(txtStrength.Text);

                if (txtDecreasbyReduction.Text != "")
                {
                    dispatchs.dec_reduction = Convert.ToDouble(txtDecreasbyReduction.Text);
                }
                if (txtDecreasbyBlending.Text != "")
                {
                    dispatchs.dec_blending = Convert.ToDouble(txtDecreasbyBlending.Text);
                }
                if (txtDecreasRacking.Text != "")
                {
                    dispatchs.dec_racking = Convert.ToDouble(txtDecreasRacking.Text);
                }
                if (txtDecreasbyWastage.Text != "")
                {
                    dispatchs.dec_wastage = Convert.ToDouble(txtDecreasbyWastage.Text);
                }
                if (txtIncreaseBLByGroging.Text != "")
                    dispatchs.IncreaseBLByGroging = Convert.ToDouble(txtIncreaseBLByGroging.Text);
                if (txtIncreaseBLInOperation.Text != "")
                    dispatchs.txtIncreaseBLInOperation = Convert.ToDouble(txtIncreaseBLInOperation.Text);
                dispatchs.bl_balanceqty = Convert.ToDouble(txtBalanceBLQty.Text);
                dispatchs.lp_balanceqty = Convert.ToDouble(txtBalanceLPQty.Text);
                val = BL_DailyDispatchClosure.Approve(dispatchs);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("DailyDispatchClosureList");
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DailyDispatchClosureList");
        }

        protected void ddDispatchVAT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if(txtDateofRemoval.Text=="")
                {
                    txtDateofRemoval.Text = txtdob.Value;
                }
                string date = Convert.ToDateTime(txtDateofRemoval.Text).ToString("dd-MM-yyyy");
                string vatcode = ddDispatchVAT.SelectedValue;
                string code = party_code.Value;
                    List<StorageToDispatch> S = new List<StorageToDispatch>();
                    S = BL_StorageToDispatch.GetDENList(party_code.Value, date, vatcode);
                    var list2 = from s in S
                                where s.record_status == "I"
                                select s;
                
                    grdDispatchvat.DataSource = list2.ToList();
                    grdDispatchvat.DataBind();
                    int count = grdDispatchvat.Rows.Count;
                    grdDispatchvat.Visible = true;
                //List<DailyDispatchClosure> DES = new List<DailyDispatchClosure>();
                //DES = BL_DailyDispatchClosure.GetDispatch();
                //Vat.DataSource = DES;
                //Vat.DataBind();

                StorageToDispatch Sto2 = new StorageToDispatch();
                Sto2 = BL_StorageToDispatch.GetDispatchqty(party_code.Value, date,vatcode);
                ddDispatchVAT.SelectedValue = Sto2.to_dispatchvat;
                txtDispatchQty.Text = Sto2.dispatchqty.ToString();
                txtBalanceBLQty.Text = Sto2.dispatchqty.ToString();
               
                ReceiverToStoragrTransfer aval = new ReceiverToStoragrTransfer();
                aval = BL_ReceiverToStoragrTransfer.GetVatData(vatcode, party_code.Value);
                txtAvailableQtyBL.Text = aval.vat_availablecapacity.ToString();
                //txtBalanceBLQty.Text =( Convert.ToDouble(txtAvailableQtyBL.Text) - Convert.ToDouble(txtDispatchQty.Text)).ToString();
                txtDecreasRacking.Text = "";
                txtDecreasbyReduction.Text = "";
                txtDecreasbyWastage.Text = "";
                txtDecreasbyBlending.Text = "";
                //txtBalanceLPQty.Text = "";
             
                if (txtStrength.Text != "")
                {
                    txtAvailableQtyLPL.Text = (Convert.ToDouble(txtAvailableQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();

                    txtBalanceLPQty.Text = (Convert.ToDouble(txtBalanceBLQty.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                }
               // grd();
            }
        }
        protected void grd()
        {
            string date = Convert.ToDateTime(txtDateofRemoval.Text).ToString("dd-MM-yyyy");
            string vatcode = ddDispatchVAT.SelectedValue;
            string code = party_code.Value;
            grdDispatchvat.DataSource = null;
            grdDispatchvat.DataBind();
            List<StorageToDispatch> S = new List<StorageToDispatch>();
            S = BL_StorageToDispatch.GetDENList(party_code.Value, date,vatcode);
            var list2 = from s in S
                        where s.record_status == "I"
                        select s;
            grdDispatchvat.DataSource = S;
            grdDispatchvat.DataBind();
         
        }
        protected void txtStrength_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtStrength.Text != "" && txtAvailableQtyBL.Text!="")
                {
                    txtAvailableQtyLPL.Text = (Convert.ToDouble(txtAvailableQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();

                   txtBalanceLPQty.Text = (Convert.ToDouble(txtBalanceBLQty.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                }
            }
        }

        protected void txtDecreasbyReduction_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtStrength.Text != "")
                {
                   
                    if (txtDispatchQty.Text != "")
                    {
                        //if (txtDecreasbyReduction.Text == "")
                        //    txtDecreasbyReduction.Text ="0";
                        //if (txtDecreasbyBlending.Text == "")
                        //    txtDecreasbyBlending.Text = "0";
                        //if (txtDecreasRacking.Text == "")
                        //    txtDecreasRacking.Text = "0";
                        //if (txtDecreasbyWastage.Text == "")
                        //    txtDecreasbyWastage.Text = "0";
                        if (txtDecreasbyReduction.Text != "")
                        {
                            DBRED.Value = txtDecreasbyReduction.Text;
                            total = Convert.ToDouble(txtBalanceBLQty.Text) - (Convert.ToDouble(txtDecreasbyReduction.Text)/*+ Convert.ToDouble(txtDecreasbyBlending.Text)+ Convert.ToDouble(txtDecreasRacking.Text)+ Convert.ToDouble(txtDecreasbyWastage.Text)*/);
                            txtBalanceBLQty.Text = total.ToString();
                            txtBalanceLPQty.Text = (Convert.ToDouble(txtBalanceBLQty.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                        else
                        {
                            if (DBRED.Value == "")
                                DBRED.Value = "0";
                          total = Convert.ToDouble(txtBalanceBLQty.Text);
                          total += Convert.ToDouble(DBRED.Value);
                          txtBalanceBLQty.Text = total.ToString();
                          txtBalanceLPQty.Text = (Convert.ToDouble(txtBalanceBLQty.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                    }
                    //else
                    //{
                    //    total = Convert.ToDouble(txtBalanceBLQty.Text);
                    //    total -= Convert.ToDouble(txtDecreasbyReduction.Text);
                    //    txtBalanceBLQty.Text = total.ToString();
                    //    txtBalanceLPQty.Text = (Convert.ToDouble(txtBalanceBLQty.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                    //}
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Strength\');", true);
                    txtStrength.Focus();
                    txtDecreasbyReduction.Text = "";
                }
            }
        }

        protected void txtDecreasbyBlending_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtStrength.Text != "")
                {
                   
                    if (txtDispatchQty.Text != "")
                    {
                        //if (txtDecreasbyReduction.Text == "")
                        //    txtDecreasbyReduction.Text = "0";
                        //if (txtDecreasbyBlending.Text == "")
                        //    txtDecreasbyBlending.Text = "0";
                        //if (txtDecreasRacking.Text == "")
                        //    txtDecreasRacking.Text = "0";
                        //if (txtDecreasbyWastage.Text == "")
                        //    txtDecreasbyWastage.Text = "0";
                        if (txtDecreasbyBlending.Text != "")
                        {
                            DBB.Value = txtDecreasbyBlending.Text;
                            total = Convert.ToDouble(txtBalanceBLQty.Text) - (/*Convert.ToDouble(txtDecreasbyReduction.Text) +*/ Convert.ToDouble(txtDecreasbyBlending.Text) /*+ Convert.ToDouble(txtDecreasRacking.Text) + Convert.ToDouble(txtDecreasbyWastage.Text)*/);
                        txtBalanceBLQty.Text = total.ToString();
                        txtBalanceLPQty.Text = (Convert.ToDouble(txtBalanceBLQty.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                        else
                        {
                            if (DBB.Value == "")
                             DBB.Value = "0";
                            total = Convert.ToDouble(txtBalanceBLQty.Text);
                            total += Convert.ToDouble(DBB.Value);
                            txtBalanceBLQty.Text = total.ToString();
                            txtBalanceLPQty.Text = (Convert.ToDouble(txtBalanceBLQty.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                    }
                    //else
                    //{
                    //    total = Convert.ToDouble(txtBalanceBLQty.Text);
                    //    total -= Convert.ToDouble(txtDecreasbyBlending.Text);
                    //    txtBalanceBLQty.Text = total.ToString();
                    //    txtBalanceLPQty.Text = (Convert.ToDouble(txtBalanceBLQty.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                    //}
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Strength\');", true);
                    txtStrength.Focus();
                    txtDecreasbyBlending.Text = "";
                }
            }
        }

        protected void txtDecreasRacking_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtStrength.Text != "")
            {
                   
                if (txtDispatchQty.Text != "")
                {
                        //if (txtDecreasbyReduction.Text == "")
                        //    txtDecreasbyReduction.Text = "0";
                        //if (txtDecreasbyBlending.Text == "")
                        //    txtDecreasbyBlending.Text = "0";
                        //if (txtDecreasRacking.Text == "")
                        //    txtDecreasRacking.Text = "0";
                        //if (txtDecreasbyWastage.Text == "")
                        //    txtDecreasbyWastage.Text = "0";
                        if (txtDecreasRacking.Text != "")
                        {
                            DBR.Value = txtDecreasRacking.Text;
                            total = Convert.ToDouble(txtBalanceBLQty.Text) - (/*Convert.ToDouble(txtDecreasbyReduction.Text) + Convert.ToDouble(txtDecreasbyBlending.Text) +*/ Convert.ToDouble(txtDecreasRacking.Text)/* + Convert.ToDouble(txtDecreasbyWastage.Text)*/);
                            txtBalanceBLQty.Text = total.ToString();
                            txtBalanceLPQty.Text = (Convert.ToDouble(txtBalanceBLQty.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                        else
                        {
                            if (DBR.Value == "")
                                DBR.Value = "0";
                            total = Convert.ToDouble(txtBalanceBLQty.Text);
                            total += Convert.ToDouble(DBR.Value);
                            txtBalanceBLQty.Text = total.ToString();
                            txtBalanceLPQty.Text = (Convert.ToDouble(txtBalanceBLQty.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                    }
                    //else
                    //{
                    //    total = Convert.ToDouble(txtBalanceBLQty.Text);
                    //    total -= Convert.ToDouble(txtDecreasRacking.Text);
                    //    txtBalanceBLQty.Text = total.ToString();
                    //    txtBalanceLPQty.Text = (Convert.ToDouble(txtBalanceBLQty.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                    //}
                }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Strength\');", true);
                txtStrength.Focus();
                    txtDecreasRacking.Text = "";
            }
        }
    }

        protected void txtDecreasbyWastage_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtStrength.Text != "")
                {
                  
                    if (txtDispatchQty.Text != "")
                    {
                        //if (txtDecreasbyReduction.Text == "")
                        //    txtDecreasbyReduction.Text = "0";
                        //if (txtDecreasbyBlending.Text == "")
                        //    txtDecreasbyBlending.Text = "0";
                        //if (txtDecreasRacking.Text == "")
                        //    txtDecreasRacking.Text = "0";
                        if (txtDecreasbyWastage.Text != "")
                        {
                            DBW.Value = txtDecreasbyWastage.Text;
                            //txtDecreasbyWastage.Text = "0";
                            total = Convert.ToDouble(txtBalanceBLQty.Text) - (/*Convert.ToDouble(txtDecreasbyReduction.Text) + Convert.ToDouble(txtDecreasbyBlending.Text) + Convert.ToDouble(txtDecreasRacking.Text) +*/ Convert.ToDouble(txtDecreasbyWastage.Text));
                            txtBalanceBLQty.Text = total.ToString();
                            txtBalanceLPQty.Text = (Convert.ToDouble(txtBalanceBLQty.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                        else
                        {
                            if (DBW.Value == "")
                                DBW.Value = "0";
                            total = Convert.ToDouble(txtBalanceBLQty.Text);
                            total += Convert.ToDouble(DBW.Value);
                            txtBalanceBLQty.Text = total.ToString();
                            txtBalanceLPQty.Text = (Convert.ToDouble(txtBalanceBLQty.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                    }
                    //else
                    //{
                    //    total = Convert.ToDouble(txtBalanceBLQty.Text);
                    //    total -= Convert.ToDouble(txtDecreasbyWastage.Text);
                    //    txtBalanceBLQty.Text = total.ToString();
                    //    txtBalanceLPQty.Text = (Convert.ToDouble(txtBalanceBLQty.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                    //}
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Strength\');", true);
                    txtStrength.Focus();
                    txtDecreasbyWastage.Text = "";
                }
            }
        }

        protected void lnkOpeningBalance_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("OpeningBalanceList");

        }

        protected void txtIncreaseBLInOperation_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtStrength.Text != "")
                {
                   
                    if (txtDispatchQty.Text != "")
                    {
                        if (txtIncreaseBLInOperation.Text != "")
                        {
                            IBO.Value = txtIncreaseBLInOperation.Text;
                            //    txtIncreaseBLInOperation.Text = "0";
                            //if (txtIncreaseBLByGroging.Text == "")
                            //    txtIncreaseBLByGroging.Text = "0";
                            total = Convert.ToDouble(txtBalanceBLQty.Text) + (Convert.ToDouble(txtIncreaseBLInOperation.Text)/*+ Convert.ToDouble(txtIncreaseBLByGroging.Text)*/);
                            txtBalanceBLQty.Text = total.ToString();
                            txtBalanceLPQty.Text = (Convert.ToDouble(txtBalanceBLQty.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                        else
                        {
                            if (IBO.Value == "")
                                IBO.Value = "0";
                            total = Convert.ToDouble(txtBalanceBLQty.Text);
                            total -= Convert.ToDouble(IBO.Value);
                            txtBalanceBLQty.Text = total.ToString();
                            txtBalanceLPQty.Text = (Convert.ToDouble(txtBalanceBLQty.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                    }
                    //else
                    //{
                    //    total = Convert.ToDouble(txtBalanceBLQty.Text);
                    //    total -= Convert.ToDouble(txtIncreaseBLByGroging.Text);
                    //    txtBalanceBLQty.Text = total.ToString();
                    //    txtBalanceLPQty.Text = (Convert.ToDouble(txtBalanceBLQty.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                    //}
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Strength\');", true);
                    txtStrength.Focus();
                    txtIncreaseBLInOperation.Text = "";
                }
            }
        }

        protected void txtIncreaseBLByGroging_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtStrength.Text != "")
                {
                  
                    if (txtDispatchQty.Text != "")
                    {
                        //if (txtIncreaseBLInOperation.Text == "")
                        //    txtIncreaseBLInOperation.Text = "0";
                        if (txtIncreaseBLByGroging.Text != "")
                        {
                            IBG.Value = txtIncreaseBLByGroging.Text;
                            //txtIncreaseBLByGroging.Text = "0";
                            total = Convert.ToDouble(txtBalanceBLQty.Text) + (/*Convert.ToDouble(txtIncreaseBLInOperation.Text) +*/ Convert.ToDouble(txtIncreaseBLByGroging.Text));
                        txtBalanceBLQty.Text = total.ToString();
                        txtBalanceLPQty.Text = (Convert.ToDouble(txtBalanceBLQty.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                        else
                        {
                            if (IBG.Value == "")
                                IBG.Value = "0";
                            total = Convert.ToDouble(txtBalanceBLQty.Text);
                            total -= Convert.ToDouble(IBG.Value);
                            txtBalanceBLQty.Text = total.ToString();
                            txtBalanceLPQty.Text = (Convert.ToDouble(txtBalanceBLQty.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                    }
                    //else
                    //{
                    //    total = Convert.ToDouble(txtBalanceBLQty.Text);
                    //    total -= Convert.ToDouble(txtIncreaseBLByGroging.Text);
                    //    txtBalanceBLQty.Text = total.ToString();
                    //    txtBalanceLPQty.Text = (Convert.ToDouble(txtBalanceBLQty.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                    //}
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Strength\');", true);
                    txtStrength.Focus();
                    txtIncreaseBLByGroging.Text = "";
                }
            }
        }

        protected void grdDispatchvat_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (IsPostBack)
            {
                grdDispatchvat.PageIndex = e.NewPageIndex;
                List<StorageToDispatch> Sto1 = new List<StorageToDispatch>();
                Sto1 = BL_StorageToDispatch.GetDENList(Session["Partycode"].ToString(),txtdob.Value,ddDispatchVAT.SelectedValue);
                var list2 = from s in Sto1
                            select s;
                grdDispatchvat.DataSource = list2.ToList();
                grdDispatchvat.DataBind();

            }
            }

        protected void txtDispatchQty_TextChanged(object sender, EventArgs e)
        {
            grd();
        }
    }
}