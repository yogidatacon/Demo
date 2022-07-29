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
    public partial class FromStoragetoDispatchForm : System.Web.UI.Page
    {
        double bl_oty = 0;
        double total = 0;
        static string _party_code;
        List<ReceiverToStoragrTransfer> form83 = new List<ReceiverToStoragrTransfer>();
        StorageToDispatch frm84D = new StorageToDispatch();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CalendarExtender1.EndDate = DateTime.Now;
                //approverremarks.Visible = false;
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
              
                Session["UserID"] = Session["UserID"];
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        Session["party_code"] = user.party_code;
                        Session["rolename"] = user.role_name;
                        Session["financial_year"] = user.financial_year;
                        List<ReceiverToStoragrTransfer> form83 = new List<ReceiverToStoragrTransfer>();
                form83 = BL_ReceiverToStoragrTransfer.GetTransferdate(user.party_code);
                        var list1 = from s in form83
                                    where s.financial_year == user.financial_year
                            select s;
                ddlTransferDate.DataSource = list1.ToList();
                ddlTransferDate.DataTextField = "transfer_date";
                ddlTransferDate.DataValueField = "receiver_storage_transfer_id";
                ddlTransferDate.DataBind();
                ddlTransferDate.Items.Insert(0, "Select");
                party_code.Value = user.party_code.ToString();
                _party_code = party_code.Value;
                btnApprove.Visible = false;
                btnReject.Visible = false;
                approverid.Visible = true;
                approverremarks.Visible = false;
                if (Session["rtype"].ToString() != "0")
                {
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                   StorageToDispatch frm84D = new StorageToDispatch();
                    string frm84Did = Session["StorageId"].ToString();
                    //  string from_receivervat = Session["from_receivervat"].ToString();
                    string Party_Code = Session["Party_Code"].ToString();
                    frm84D =BL_StorageToDispatch.GetDetails(party_code.Value, frm84Did, Session["Sfinancial_year"].ToString());
                    txtReceiptDate.Text = frm84D.receipt_date;
                        CalendarExtender1.SelectedDate = Convert.ToDateTime(frm84D.receipt_date);
                    txtdate.Value= frm84D.receipt_date;
                    txtReceiptTime.Value = frm84D.receipt_hour;
                    // rtype.Value = Session["rtype"].ToString();
                //    ddlTransferDate.SelectedValue = frm84D.receiver_storage_transfer_id.ToString();
                   if( frm84D.record_status == "A")
                    {
                        form83 = BL_ReceiverToStoragrTransfer.GetSubmiteddate(party_code.Value, frm84D.receiver_storage_transfer_id);
                        var list3 = from s in form83
                                    where s.financial_year == user.financial_year
                                    select s;
                        ddlTransferDate.DataSource = list3.ToList();
                        ddlTransferDate.DataTextField = "transfer_date";
                        ddlTransferDate.DataValueField = "receiver_storage_transfer_id";
                        ddlTransferDate.DataBind();
                    }
                   else
                    {
                        ddlTransferDate.SelectedValue = frm84D.receiver_storage_transfer_id.ToString();
                    }
                 
                    form83 = BL_ReceiverToStoragrTransfer.GetDENvat(frm84D.receiver_storage_transfer_id);
                    var list2 = from s in form83
                                where s.financial_year == user.financial_year
                                select s;
                    ddDispatchVAT.DataSource = list2.ToList();
                    ddDispatchVAT.DataTextField = "dispatchvat";
                    ddDispatchVAT.DataValueField = "todispatchvat";
                    ddDispatchVAT.DataBind();
                    txtDipInWetCms.Text = frm84D.dips.ToString();
                    txtTemprature.Text = frm84D.temperature.ToString();
                    txtIndication.Text = frm84D.indication.ToString();
                    txtStrength.Text = frm84D.strength.ToString();
                    txtIncreaseBLLitresInOperation.Text = frm84D.inc_operation.ToString();
                            INO.Value = frm84D.inc_operation.ToString();
                            txtIncreaseBLLitresByGroging.Text = frm84D.inc_groging.ToString();
                            IBG.Value= frm84D.inc_groging.ToString();
                            txtDecreaseByBlending.Text = frm84D.dec_blending.ToString();
                            DBB.Value= frm84D.dec_blending.ToString();
                            txtDecreaseByRacking.Text = frm84D.dec_racking.ToString();
                            DBR.Value = frm84D.dec_racking.ToString();
                            txtDecreasByReduction.Text = frm84D.dec_reduction.ToString();
                            DBRED.Value = frm84D.dec_reduction.ToString();
                            txtDecreaseByWastageStroage.Text = frm84D.dec_wastage.ToString();
                            DBWS.Value = frm84D.dec_wastage.ToString();
                            txtDenaturedQtyBL.Text = frm84D.denatured_qty.ToString();
                    txtDenaturingAgentName.Text = frm84D.denaturing_agent;
                    txtBalanceQtyBL.Text = frm84D.bl_balanceqty.ToString();
                    total= frm84D.bl_balanceqty;
                            if(txtDenaturedQtyBL.Text !="")
                            {
                                //double a = frm84D.total_bl_receipt - frm84D.denatured_qty;
                                //txtReceiptQtyBL.Text =a.ToString();
                                //txtReceiptQtyLPL.Text = (Convert.ToDouble(a) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString(); 
                                txtReceiptQtyBL.Text = frm84D.total_bl_receipt.ToString();
                                txtReceiptQtyLPL.Text = frm84D.total_lp_receipt.ToString();

                            }
                            else
                            {
                                txtReceiptQtyBL.Text = frm84D.total_bl_receipt.ToString();
                                txtReceiptQtyLPL.Text = frm84D.total_lp_receipt.ToString();
                            }
                  txtBalanceQtyLP.Text = frm84D.lp_balanceqty.ToString();
                    ddDispatchVAT.SelectedValue = frm84D.to_dispatchvat;
                    if(frm84D.hasqtyincreased =="Y")
                    {
                        chIncrease.Checked = true;
                    }
                    //form83 = BL_ReceiverToStorage_84.GetStoragevat(frm84.fermenter_receiver_output_id);
                    //var list = from s in form83
                    //           select s;
                    //ddlToStoragevat.DataSource = list.ToList();
                    //ddlToStoragevat.DataTextField = "vat_name";
                    //ddlToStoragevat.DataValueField = "to_storagevat";
                    //ddlToStoragevat.DataBind();
                    List<All_Approvals> approvals = new List<All_Approvals>();
                    approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), frm84D.storage_dispatch_id.ToString(), "STD");
                            var list4 = (from s in approvals
                                         where s.financial_year == Session["Sfinancial_year"].ToString()
                                         select s);
                            grdApprovalDetails.DataSource = list4.ToList();
                            grdApprovalDetails.DataBind();
                    //if (frm84D.record_status == "Y" || user.role_name == "Bond Officer")
                    //{
                    //    foreach (GridViewRow dr1 in grdReceiverVat.Rows)
                    //    {
                    //        ImageButton btn = dr1.FindControl("ImageButton1") as ImageButton;
                    //        btn.Visible = false;
                    //    }
                    //    Image1.Visible = false;
                    //}

                    txtRemarks.Text = frm84D.remarks;
                    if (Session["rtype"].ToString() == "1")
                    {
                        txtReceiptDate.Attributes.Add("Disabled", "Disabled");
                        txtDipInWetCms.Attributes.Add("Disabled", "Disabled");
                        txtTemprature.Attributes.Add("Disabled", "Disabled");
                        txtIndication.Attributes.Add("Disabled", "Disabled");
                        txtStrength.Attributes.Add("Disabled", "Disabled");

                        ddlTransferDate.Attributes.Add("Disabled", "Disabled");
                        txtIncreaseBLLitresInOperation.Attributes.Add("Disabled", "Disabled");
                        txtIncreaseBLLitresByGroging.Attributes.Add("Disabled", "Disabled");
                        txtDecreaseByBlending.Attributes.Add("Disabled", "Disabled");
                        txtDecreaseByRacking.Attributes.Add("Disabled", "Disabled");
                        txtDecreasByReduction.Attributes.Add("Disabled", "Disabled");
                        txtDecreaseByWastageStroage.Attributes.Add("Disabled", "Disabled");
                        txtDenaturingAgentName.ReadOnly = true;
                        txtDenaturedQtyBL.ReadOnly = true;
                        chIncrease.Enabled = false;
                        txtRemarks.ReadOnly = true;
                        txtReceiptTime.Attributes.Add("Disabled", "Disabled");
                        ddDispatchVAT.Attributes.Add("Disabled", "Disabled");
                        btnSaveasDraft.Visible = false;
                        btnSubmit.Visible = false;
                        btnCancel.Visible = false;
                        txtApproverremarks.Visible = false;
                        approverremarks.Visible = false;

                        if (user.role_name == "Bond Officer" && frm84D.record_status == "Y")
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
                        if (frm84D.record_status == "A" || frm84D.record_status == "R")
                        {
                            approverid.Visible = true;
                            approverremarks.Visible = false;
                            btnApprove.Visible = false;
                            btnReject.Visible = false;
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
            Response.Redirect("FromStoragetoDispatchList.aspx");
        }

        protected void lnkDailyDispatchClosure_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DailyDispatchClosureList.aspx");
        }
        protected void lnkRawMaterialToFermenter_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialtoFermenterList.aspx");
        }

        protected void lnkFermentertoReceiver_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("FermentertoReceiverList.aspx");
        }

        protected void lnkReceivertoStorage_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ReceivertoStorageList.aspx");
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
        protected void btnVATtansfers_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATTransferList");
        }

        protected void ddlTransferDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                int transferid = Convert.ToInt32(ddlTransferDate.SelectedValue);
                if(ddlTransferDate.SelectedValue != null)
                {
                    List<ReceiverToStoragrTransfer> form83 = new List<ReceiverToStoragrTransfer>();
                    form83 = BL_ReceiverToStoragrTransfer.GetDENvat(transferid);
                    var list1 = from s in form83

                                select s;
                    ddDispatchVAT.DataSource = list1.ToList();
                    ddDispatchVAT.DataTextField = "dispatchvat";
                    ddDispatchVAT.DataValueField = "todispatchvat";
                    ddDispatchVAT.DataBind();
                    ddDispatchVAT.Items.Insert(0, "Select");
                    txtReceiptQtyBL.Text = "";
                    txtReceiptQtyLPL.Text = "";
                    txtDenaturedQtyBL.Text = "";
                    txtIncreaseBLLitresByGroging.Text = "";
                    txtIncreaseBLLitresInOperation.Text = "";
                    txtDecreasByReduction.Text = "";
                    txtDecreaseByBlending.Text = "";
                    txtDecreaseByRacking.Text = "";
                    txtDecreaseByWastageStroage.Text = "";
                    txtBalanceQtyBL.Text = "";
                    txtBalanceQtyLP.Text = "";
                    chIncrease.Checked = false;
                }
            }
        }

        protected void ddDispatchVAT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                int transferid = Convert.ToInt32(ddlTransferDate.SelectedValue);
                if (ddDispatchVAT.SelectedValue != "")
                {
                    string vatcode = ddDispatchVAT.SelectedValue;
                    string code = party_code.Value;
                    ReceiverToStoragrTransfer aval = new ReceiverToStoragrTransfer();
                    aval = BL_ReceiverToStoragrTransfer.GetBLLPquty(vatcode, code, transferid);
                    txtReceiptQtyBL.Text = aval.total_bl_transfer.ToString();
                    txtBalanceQtyBL.Text = aval.total_bl_transfer.ToString();
                    txtReceiptQtyLPL.Text = aval.total_lp_transfer.ToString();
                }
            }
        }
       
        protected void txtIncreaseBLLitresInOperation_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
               
                //if (ddDispatchVAT.SelectedValue != "")
                //{
                    if (txtStrength.Text != "")
                    {
                        if (txtBalanceQtyBL.Text == "0" || txtBalanceQtyBL.Text == "")
                        {
                            total = Convert.ToDouble(txtReceiptQtyBL.Text) + Convert.ToDouble(txtIncreaseBLLitresInOperation.Text);
                            txtBalanceQtyBL.Text = total.ToString();
                            txtBalanceQtyLP.Text = (Convert.ToDouble(txtBalanceQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                        else
                        {if (txtIncreaseBLLitresInOperation.Text != "")
                        {
                            INO.Value = txtIncreaseBLLitresInOperation.Text;
                            total = Convert.ToDouble(txtBalanceQtyBL.Text);
                            total += Convert.ToDouble(txtIncreaseBLLitresInOperation.Text);
                            txtBalanceQtyBL.Text = total.ToString();
                            txtBalanceQtyLP.Text = (Convert.ToDouble(txtBalanceQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                        else
                        {
                            if (INO.Value == "")
                                INO.Value = "0";
                            total = Convert.ToDouble(txtBalanceQtyBL.Text);
                            total -= Convert.ToDouble(INO.Value);
                            txtBalanceQtyBL.Text = total.ToString();
                            txtBalanceQtyLP.Text = (Convert.ToDouble(txtBalanceQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                        }
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Strength\');", true);
                        txtStrength.Focus();
                        txtIncreaseBLLitresInOperation.Text = "";
                    }
               // }
            }
        }

        protected void txtIncreaseBLLitresByGroging_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
              
                //if (ddDispatchVAT.SelectedValue != "")
                //{
                    if (txtStrength.Text != "")
                    {
                        if (txtBalanceQtyBL.Text == "0" || txtBalanceQtyBL.Text == "")
                        {
                            total = Convert.ToDouble(txtReceiptQtyBL.Text) + Convert.ToDouble(txtIncreaseBLLitresByGroging.Text);
                            txtBalanceQtyBL.Text = total.ToString();
                            txtBalanceQtyLP.Text = (Convert.ToDouble(txtBalanceQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                        else
                        {
                        if (txtIncreaseBLLitresByGroging.Text != "")
                        {
                            IBG.Value = txtIncreaseBLLitresByGroging.Text;
                            total = Convert.ToDouble(txtBalanceQtyBL.Text);
                            total += Convert.ToDouble(txtIncreaseBLLitresByGroging.Text);
                            txtBalanceQtyBL.Text = total.ToString();
                            txtBalanceQtyLP.Text = (Convert.ToDouble(txtBalanceQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                        else
                        {
                            if (IBG.Value == "")
                                IBG.Value = "0";
                           total = Convert.ToDouble(txtBalanceQtyBL.Text);
                            total -= Convert.ToDouble(IBG.Value);
                            txtBalanceQtyBL.Text = total.ToString();
                            txtBalanceQtyLP.Text = (Convert.ToDouble(txtBalanceQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Strength\');", true);
                        txtStrength.Focus();
                        txtIncreaseBLLitresByGroging.Text = "";
                    }
               // }
            }
        }
        
        protected void chIncrease_CheckedChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
               
                if (ddDispatchVAT.SelectedValue != "")
                {
                   if (txtDenaturedQtyBL.Text != "")
                  {
                        //if (txtDenaturedQtyBL.Text == "")
                        //txtDenaturedQtyBL.Text = "0";
                        if(chIncrease.Checked)
                        {
                        total = Convert.ToDouble(txtBalanceQtyBL.Text) + Convert.ToDouble(txtDenaturedQtyBL.Text);
                            txtBalanceQtyBL.Text = total.ToString();
                            if (txtStrength.Text != "")
                            {
                                double strength = Convert.ToDouble(txtStrength.Text);
                                if (strength > 0)
                                {
                                    txtBalanceQtyLP.Text = (Convert.ToDouble(txtBalanceQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Strength\');", true);
                                txtStrength.Focus();
                                chIncrease.Checked = false;
                            }

                        }
                        else
                        {
                            total = Convert.ToDouble(txtBalanceQtyBL.Text);
                            total -= Convert.ToDouble(txtDenaturedQtyBL.Text);
                            txtBalanceQtyBL.Text = total.ToString();
                            if (txtStrength.Text != "")
                            {
                                double strength = Convert.ToDouble(txtStrength.Text);
                                if (strength > 0)
                                {
                                    txtBalanceQtyLP.Text = (Convert.ToDouble(txtBalanceQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Strength\');", true);
                                txtStrength.Focus();
                                chIncrease.Checked = false;
                            }
                        }

                    }
                }
            }
        }

        protected void txtStrength_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
               
                if (ddDispatchVAT.SelectedValue != "Select")
                {
                    if (chIncrease.Checked)
                    {
                        if (txtDenaturedQtyBL.Text != "")
                        {
                            if (txtStrength.Text != "")
                            {

                                total = Convert.ToDouble(txtReceiptQtyBL.Text) + Convert.ToDouble(txtDenaturedQtyBL.Text);
                                txtBalanceQtyBL.Text = total.ToString();
                                txtBalanceQtyLP.Text = (Convert.ToDouble(txtBalanceQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Strength\');", true);
                                txtStrength.Focus();
                               
                            }
                        }
                    }
                    else
                    {
                        total = Convert.ToDouble(txtReceiptQtyBL.Text);
                         txtBalanceQtyBL.Text = total.ToString();
                        txtBalanceQtyLP.Text = (Convert.ToDouble(txtBalanceQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                    }
                }
            }
        }

        protected void txtDecreasByReduction_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                
                //if (ddDispatchVAT.SelectedValue != "")
                //{
                    if (txtStrength.Text != "")
                    {
                        if (txtBalanceQtyBL.Text == "0" || txtBalanceQtyBL.Text == "")
                        {
                            total = Convert.ToDouble(txtReceiptQtyBL.Text) - Convert.ToDouble(txtDecreasByReduction.Text);
                            txtBalanceQtyBL.Text = total.ToString();
                            txtBalanceQtyLP.Text = (Convert.ToDouble(txtBalanceQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                        else
                        {
                        if (txtDecreasByReduction.Text != "")
                        {
                            DBRED.Value = txtDecreasByReduction.Text;
                            total = Convert.ToDouble(txtBalanceQtyBL.Text);
                            total -= Convert.ToDouble(txtDecreasByReduction.Text);
                            txtBalanceQtyBL.Text = total.ToString();
                            txtBalanceQtyLP.Text = (Convert.ToDouble(txtBalanceQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                        else
                        {
                            if (DBRED.Value == "")
                                DBRED.Value = "0";
                               total = Convert.ToDouble(txtBalanceQtyBL.Text);
                            total += Convert.ToDouble(DBRED.Value);
                            txtBalanceQtyBL.Text = total.ToString();
                            txtBalanceQtyLP.Text = (Convert.ToDouble(txtBalanceQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Strength\');", true);
                        txtStrength.Focus();
                    txtDecreasByReduction.Text = "";
                    }
               // }

            }
        }

        protected void txtDecreaseByBlending_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
               
                //if (ddDispatchVAT.SelectedValue != "")
                //{
                    if (txtStrength.Text != "")
                    {
                        if (txtBalanceQtyBL.Text == "0" || txtBalanceQtyBL.Text == "")
                        {
                            total = Convert.ToDouble(txtReceiptQtyBL.Text) - Convert.ToDouble(txtDecreaseByBlending.Text);
                            txtBalanceQtyBL.Text = total.ToString();
                            txtBalanceQtyLP.Text = (Convert.ToDouble(txtBalanceQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                        else
                        {
                        if (txtDecreaseByBlending.Text != "")
                        {
                            DBB.Value = txtDecreaseByBlending.Text;
                            total = Convert.ToDouble(txtBalanceQtyBL.Text);
                            total -= Convert.ToDouble(txtDecreaseByBlending.Text);
                            txtBalanceQtyBL.Text = total.ToString();
                            txtBalanceQtyLP.Text = (Convert.ToDouble(txtBalanceQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                        else
                        {
                            if (DBB.Value == "")
                                DBB.Value = "0";
                           total = Convert.ToDouble(txtBalanceQtyBL.Text);
                            total += Convert.ToDouble(DBB.Value);
                            txtBalanceQtyBL.Text = total.ToString();
                            txtBalanceQtyLP.Text = (Convert.ToDouble(txtBalanceQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                        }
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Strength\');", true);
                        txtStrength.Focus();
                    txtDecreaseByBlending.Text = "";
                    }
              //  }
            }
        }

        protected void txtDecreaseByRacking_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
               
                //if (ddDispatchVAT.SelectedValue !="")
                //{
                    if (txtStrength.Text != "")
                    {
                        if (txtBalanceQtyBL.Text == "0" || txtBalanceQtyBL.Text == "")
                        {
                            total = Convert.ToDouble(txtReceiptQtyBL.Text) - Convert.ToDouble(txtDecreaseByRacking.Text);
                            txtBalanceQtyBL.Text = total.ToString();
                            txtBalanceQtyLP.Text = (Convert.ToDouble(txtBalanceQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                        else
                        {
                        if (txtDecreaseByRacking.Text != "")
                        {
                            DBR.Value = txtDecreaseByRacking.Text;
                            total = Convert.ToDouble(txtBalanceQtyBL.Text);
                            total -= Convert.ToDouble(txtDecreaseByRacking.Text);
                            txtBalanceQtyBL.Text = total.ToString();
                            txtBalanceQtyLP.Text = (Convert.ToDouble(txtBalanceQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                        else
                        {
                            if (DBR.Value == "")
                                DBR.Value = "0";
                            total = Convert.ToDouble(txtBalanceQtyBL.Text);
                            total += Convert.ToDouble(DBR.Value);
                            txtBalanceQtyBL.Text = total.ToString();
                            txtBalanceQtyLP.Text = (Convert.ToDouble(txtBalanceQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Strength\');", true);
                        txtStrength.Focus();
                    txtDecreaseByRacking.Text = "";
                    }
               // }
            }
        }

        protected void txtDecreaseByWastageStroage_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
               
                //if (ddDispatchVAT.SelectedValue != "")
                //{ 
                if (txtStrength.Text !="")
                {
                    if (txtBalanceQtyBL.Text == "0" || txtBalanceQtyBL.Text == "")
                    {
                        total = Convert.ToDouble(txtReceiptQtyBL.Text) - Convert.ToDouble(txtDecreaseByWastageStroage.Text);
                        txtBalanceQtyBL.Text = total.ToString();
                        txtBalanceQtyLP.Text = (Convert.ToDouble(txtBalanceQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                    }
                    else
                    {
                        if (txtDecreaseByWastageStroage.Text != "")
                        {
                            DBWS.Value = txtDecreaseByWastageStroage.Text;
                            total = Convert.ToDouble(txtBalanceQtyBL.Text);
                            total -= Convert.ToDouble(txtDecreaseByWastageStroage.Text);
                            txtBalanceQtyBL.Text = total.ToString();
                            txtBalanceQtyLP.Text = (Convert.ToDouble(txtBalanceQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                        else
                        {
                            if (DBWS.Value == "")
                                DBWS.Value = "0";
                           total = Convert.ToDouble(txtBalanceQtyBL.Text);
                            total += Convert.ToDouble(DBWS.Value);
                            txtBalanceQtyBL.Text = total.ToString();
                            txtBalanceQtyLP.Text = (Convert.ToDouble(txtBalanceQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                    }
                }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Strength\');", true);
                        txtStrength.Focus();
                    txtDecreaseByWastageStroage.Text = "";
                    }
                //}

            }
        }

        protected void txtBalanceQtyBL_DataBinding(object sender, EventArgs e)
        {
            if (txtStrength.Text != "")
            {
                txtBalanceQtyLP.Text = (Convert.ToDouble(txtBalanceQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
            }
        }
        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                StorageToDispatch frm84 = new StorageToDispatch();
                if (txtReceiptDate.Text == "" || txtReceiptDate.Text != "")
                {
                    txtReceiptDate.Text = txtdate.Value;
                }
                frm84.receipt_date=txtReceiptDate.Text.Substring(0, 10);
                frm84.transfer_date = ddlTransferDate.SelectedItem.ToString();
                frm84.receipt_hour = txtReceiptTime.Value;
                frm84.party_code = party_code.Value;
                frm84.to_dispatchvat= ddDispatchVAT.SelectedValue;

                if (txtIncreaseBLLitresByGroging.Text != "")
                {
                    frm84.inc_groging = Convert.ToDouble(txtIncreaseBLLitresByGroging.Text);
                }
                if (txtIncreaseBLLitresInOperation.Text != "")
                {
                    frm84.inc_operation = Convert.ToDouble(txtIncreaseBLLitresInOperation.Text);
                }
                frm84.indication = Convert.ToDouble(txtIndication.Text);
                frm84.dips = Convert.ToDouble(txtDipInWetCms.Text);
                frm84.temperature = Convert.ToDouble(txtTemprature.Text);
                frm84.strength = Convert.ToDouble(txtStrength.Text);

                if (txtDecreaseByBlending.Text != "")
                {
                    frm84.dec_blending = Convert.ToDouble(txtDecreaseByBlending.Text);
                }
                if (txtDecreaseByRacking.Text != "")
                {
                    frm84.dec_racking = Convert.ToDouble(txtDecreaseByRacking.Text);
                }
                if (txtDecreasByReduction.Text != "")
                {
                    frm84.dec_reduction = Convert.ToDouble(txtDecreasByReduction.Text);
                }
                if (txtDecreaseByWastageStroage.Text != "")
                {
                    frm84.dec_wastage = Convert.ToDouble(txtDecreaseByWastageStroage.Text);
                }
              
                if(txtBalanceQtyBL.Text !="")
                {
                   
                    frm84.bl_balanceqty= Convert.ToDouble(txtBalanceQtyBL.Text);
                    bl_oty= Convert.ToDouble(txtBalanceQtyBL.Text);
                }
                if(txtBalanceQtyLP.Text !="")
                {
                    
                    frm84.lp_balanceqty= Convert.ToDouble(txtBalanceQtyLP.Text);

                }
                if(chIncrease.Checked)
                {
                    frm84.hasqtyincreased = "Y";
                }
                else
                {
                    frm84.hasqtyincreased = "N";
                }
                frm84.denaturing_agent = txtDenaturingAgentName.Text;
                frm84.receiver_storage_transfer_id = Convert.ToInt32( ddlTransferDate.SelectedValue);
                //if (chIncrease.Checked)
                //{
                   if (txtDenaturedQtyBL.Text != "")
                   {
                     frm84.denatured_qty = Convert.ToDouble(txtDenaturedQtyBL.Text);
                     frm84.total_bl_receipt = Convert.ToDouble(txtReceiptQtyBL.Text);
                     frm84.total_lp_receipt = (Convert.ToDouble(frm84.total_bl_receipt) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
                  }
                //}
                else
                {
                    frm84.total_bl_receipt = Convert.ToDouble(txtReceiptQtyBL.Text);
                    frm84.total_lp_receipt = Convert.ToDouble(txtReceiptQtyLPL.Text);
               }
              
                frm84.remarks = txtRemarks.Text;
               frm84.financial_year= Session["financial_year"].ToString();
                frm84.user_id = Session["UserID"].ToString();
                frm84.record_status = "N";
                string val;
                //string a = Session["rtype"].ToString();
                if (Session["rtype"].ToString() == "0")
                {
                    val = BL_StorageToDispatch.Insert(frm84);
                }

                else
                {
                    frm84.storage_dispatch_id = Convert.ToInt32(Session["StorageId"].ToString());
                    val = BL_StorageToDispatch.Update(frm84);
                }

                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("FromStoragetoDispatchList");
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
                StorageToDispatch frm84 = new StorageToDispatch();
                if(txtReceiptDate.Text == "" || txtReceiptDate.Text !="")
                {
                  txtReceiptDate.Text = txtdate.Value;
                }
                frm84.receipt_date = txtReceiptDate.Text.Substring(0, 10);
                frm84.transfer_date = ddlTransferDate.SelectedItem.ToString();
                frm84.receipt_hour = txtReceiptTime.Value;
                frm84.party_code = party_code.Value;
                frm84.to_dispatchvat = ddDispatchVAT.SelectedValue;

                if (txtIncreaseBLLitresByGroging.Text != "")
                {
                    frm84.inc_groging = Convert.ToDouble(txtIncreaseBLLitresByGroging.Text);
                }
                if (txtIncreaseBLLitresInOperation.Text != "")
                {
                    frm84.inc_operation = Convert.ToDouble(txtIncreaseBLLitresInOperation.Text);
                }
                frm84.indication = Convert.ToDouble(txtIndication.Text);
                frm84.dips = Convert.ToDouble(txtDipInWetCms.Text);
                frm84.temperature = Convert.ToDouble(txtTemprature.Text);
                frm84.strength = Convert.ToDouble(txtStrength.Text);

                if (txtDecreaseByBlending.Text != "")
                {
                    frm84.dec_blending = Convert.ToDouble(txtDecreaseByBlending.Text);
                }
                if (txtDecreaseByRacking.Text != "")
                {
                    frm84.dec_racking = Convert.ToDouble(txtDecreaseByRacking.Text);
                }
                if (txtDecreasByReduction.Text != "")
                {
                    frm84.dec_reduction = Convert.ToDouble(txtDecreasByReduction.Text);
                }
                if (txtDecreaseByWastageStroage.Text != "")
                {
                    frm84.dec_wastage = Convert.ToDouble(txtDecreaseByWastageStroage.Text);
                }
                if (txtDenaturedQtyBL.Text != "")
                {
                    frm84.denatured_qty = Convert.ToDouble(txtDenaturedQtyBL.Text);
                }
                if (txtBalanceQtyBL.Text != "")
                {
                    frm84.bl_balanceqty = Convert.ToDouble(txtBalanceQtyBL.Text);
                }
                if (txtBalanceQtyLP.Text != "")
                {
                    frm84.lp_balanceqty = Convert.ToDouble(txtBalanceQtyLP.Text);
                }
                if (chIncrease.Checked)
                {
                    frm84.hasqtyincreased = "Y";
                }
                else
                {
                    frm84.hasqtyincreased = "N";
                }
                frm84.denaturing_agent = txtDenaturingAgentName.Text;
                frm84.receiver_storage_transfer_id = Convert.ToInt32(ddlTransferDate.SelectedValue);
               /// if (chIncrease.Checked)
                //{
                    if (txtDenaturedQtyBL.Text != "")
                    {
                        frm84.denatured_qty = Convert.ToDouble(txtDenaturedQtyBL.Text);
                       frm84.total_bl_receipt = Convert.ToDouble(txtReceiptQtyBL.Text);
                        frm84.total_lp_receipt = (Convert.ToDouble(frm84.total_bl_receipt) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
                   // }
                }
               else
                {
                    frm84.total_bl_receipt = Convert.ToDouble(txtReceiptQtyBL.Text);
                    frm84.total_lp_receipt = Convert.ToDouble(txtReceiptQtyLPL.Text);
                }
                frm84.remarks = txtRemarks.Text;
                frm84.user_id = Session["UserID"].ToString();
                frm84.financial_year = Session["financial_year"].ToString();
                frm84.record_status = "Y";
                string val;
                //string a = Session["rtype"].ToString();
                if (Session["rtype"].ToString() == "0")
                {
                    val = BL_StorageToDispatch.Insert(frm84);
                }

                else
                {
                    frm84.storage_dispatch_id = Convert.ToInt32(Session["StorageId"].ToString());
                    val = BL_StorageToDispatch.Update(frm84);
                }

                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("FromStoragetoDispatchList");
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
            Response.Redirect("FromStoragetoDispatchList");
        }
        protected void lnkRawMaterialWastage_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawmaterialWastageList.aspx");
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                StorageToDispatch frm84 = new StorageToDispatch();

                frm84.record_status = "A";
                string val;
                frm84.storage_dispatch_id = Convert.ToInt32(Session["StorageId"].ToString());
                frm84.remarks = txtApproverremarks.Value;
                frm84.user_id = Session["UserID"].ToString();
                frm84.receipt_date = txtReceiptDate.Text.Substring(0, 10);
                frm84.transfer_date = ddlTransferDate.SelectedItem.ToString();
                frm84.receipt_hour = txtReceiptTime.Value;
                frm84.party_code = party_code.Value;
                frm84.to_dispatchvat = ddDispatchVAT.SelectedValue;

                if (txtIncreaseBLLitresByGroging.Text != "")
                {
                    frm84.inc_groging = Convert.ToDouble(txtIncreaseBLLitresByGroging.Text);
                }
                if (txtIncreaseBLLitresInOperation.Text != "")
                {
                    frm84.inc_operation = Convert.ToDouble(txtIncreaseBLLitresInOperation.Text);
                }

                if (txtDecreaseByBlending.Text != "")
                {
                    frm84.dec_blending = Convert.ToDouble(txtDecreaseByBlending.Text);
                }
                if (txtDecreaseByRacking.Text != "")
                {
                    frm84.dec_racking = Convert.ToDouble(txtDecreaseByRacking.Text);
                }
                if (txtDecreasByReduction.Text != "")
                {
                    frm84.dec_reduction = Convert.ToDouble(txtDecreasByReduction.Text);
                }
                if (txtDecreaseByWastageStroage.Text != "")
                {
                    frm84.dec_wastage = Convert.ToDouble(txtDecreaseByWastageStroage.Text);
                }
                if (txtDenaturedQtyBL.Text != "")
                {
                    frm84.denatured_qty = Convert.ToDouble(txtDenaturedQtyBL.Text);
                }
                if (txtBalanceQtyBL.Text != "")
                {
                    frm84.bl_balanceqty = Convert.ToDouble(txtBalanceQtyBL.Text);
                }
                if (txtBalanceQtyLP.Text != "")
                {
                    frm84.lp_balanceqty = Convert.ToDouble(txtBalanceQtyLP.Text);
                }
                if (chIncrease.Checked)
                {
                    frm84.hasqtyincreased = "Y";
                }
                else
                {
                    frm84.hasqtyincreased = "N";
                }
                frm84.financial_year = Session["financial_year"].ToString();
                frm84.denaturing_agent = txtDenaturingAgentName.Text;
                frm84.receiver_storage_transfer_id = Convert.ToInt32(ddlTransferDate.SelectedValue);
                if (txtDenaturedQtyBL.Text != "")
                {
                    frm84.denatured_qty = Convert.ToDouble(txtDenaturedQtyBL.Text);
                    frm84.total_bl_receipt = Convert.ToDouble(txtReceiptQtyBL.Text);
                    frm84.total_lp_receipt = (Convert.ToDouble(frm84.total_bl_receipt) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
                   
                }
                else
                {
                    frm84.total_bl_receipt = Convert.ToDouble(txtReceiptQtyBL.Text);
                    frm84.total_lp_receipt = Convert.ToDouble(txtReceiptQtyLPL.Text);
                }
                val = BL_StorageToDispatch.Approve(frm84);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("FromStoragetoDispatchList");
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
                StorageToDispatch ferm = new StorageToDispatch();
                ferm.record_status = "R";
                string val;
                ferm.storage_dispatch_id = Convert.ToInt32(Session["StorageId"].ToString());
                ferm.remarks = txtApproverremarks.Value;
                ferm.user_id = Session["UserID"].ToString();
                ferm.party_code = party_code.Value;
                ferm.financial_year = Session["financial_year"].ToString();
                ferm.receiver_storage_transfer_id = Convert.ToInt32(ddlTransferDate.SelectedValue);
                val = BL_StorageToDispatch.Approve(ferm);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("FromStoragetoDispatchList");
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

        protected void lnkOpeningBalance_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("OpeningBalanceList");

        }

        protected void txtDenaturedQtyBL_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

                if (chIncrease.Checked)
                {
                    total = Convert.ToDouble(txtBalanceQtyBL.Text) + Convert.ToDouble(txtDenaturedQtyBL.Text);
                    txtBalanceQtyBL.Text = total.ToString();
                    if (txtStrength.Text != "")
                    {
                        int strength = Convert.ToInt32(txtStrength.Text);
                        if (strength > 0)
                        {
                            txtBalanceQtyLP.Text = (Convert.ToDouble(txtBalanceQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                    }
                }
            }
        }
    }
}