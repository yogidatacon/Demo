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
    public partial class ReceiverTransferForm : System.Web.UI.Page
    {
        static string _party_code;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CalendarExtender.EndDate = DateTime.Now;
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
                        Session["financial_year"] = user.financial_year;
                    List<VAT_Master> vats = new List<VAT_Master>();
                vats = BL_VATMaster.GetvatmasterList(user.party_code);

                var list1 = from s in vats
                            where s.party_code == user.party_code && s.vat_type_code == "Sto" || s.vat_type_code == "STO"
                            select s;
                ddlToStoragevat.DataSource = list1.ToList();
                ddlToStoragevat.DataTextField = "vat_name";
              ddlToStoragevat.DataValueField = "vat_code";
                ddlToStoragevat.DataBind();
                ddlToStoragevat.Items.Insert(0, "Select");

                var list2 = from s in vats
                            where s.party_code == user.party_code && s.vat_type_code == "DEN"
                            select s;
                ddTransferToDispatchVat.DataSource = list2.ToList();
                ddTransferToDispatchVat.DataTextField = "vat_name";
               ddTransferToDispatchVat.DataValueField = "vat_code";
                ddTransferToDispatchVat.DataBind();
               ddTransferToDispatchVat.Items.Insert(0, "Select");

                party_code.Value = user.party_code.ToString();
                _party_code = party_code.Value;
                btnApprove.Visible = false;
                btnReject.Visible = false;
                approverid.Visible = false;
                approverremarks.Visible = false;
                if (Session["rtype"].ToString() != "0")
                {
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                   ReceiverToStoragrTransfer frm84 = new ReceiverToStoragrTransfer();
                    string frm84id = Session["ReceivertoStorage"].ToString();
                    //  string from_receivervat = Session["from_receivervat"].ToString();
                    string Party_Code = Session["Party_Code"].ToString();
                    frm84 = BL_ReceiverToStoragrTransfer.GetDetails(Party_Code, frm84id, Session["rtfinancial_year"].ToString());
                   txtTransferDate.Text = frm84.transfer_date;
                        CalendarExtender.SelectedDate = Convert.ToDateTime(frm84.transfer_date);
                    txttrdate.Value= frm84.transfer_date;
                    txtTransferTime.Value = frm84.transfer_time;
                    rtype.Value = Session["rtype"].ToString();
                    txtDipInWetCms.Text = frm84.dips.ToString();
                    txtTemprature.Text = frm84.temperature.ToString();
                    txtIndication.Text = frm84.indication.ToString();
                    txtStrength.Text = frm84.strength.ToString();
                    txtIncreaseBLLitresInOperation.Text = frm84.inc_operation.ToString();
                    txtIncreaseBLLitresByGroging.Text = frm84.inc_groging.ToString();
                    txtDecreasByBlending.Text = frm84.dec_blending.ToString();
                    txtDecreasByRacking.Text = frm84.dec_racking.ToString();
                    txtDecreasByReduction.Text = frm84.dec_reduction.ToString();
                    txtDecreasByWastageStroage.Text = frm84.dec_wastage.ToString();
                    ddlToStoragevat.SelectedValue = frm84.fromstoragevat;
                    ddTransferToDispatchVat.SelectedValue = frm84.todispatchvat; 
                    ReceiverToStoragrTransfer aval = new ReceiverToStoragrTransfer();
                    aval = BL_ReceiverToStoragrTransfer.GetVatData(frm84.fromstoragevat, Party_Code);
                    txtAvailableQtyBL.Text = aval.vat_availablecapacity.ToString();
                    txtRemarks.Text = frm84.remarks;
                    txtTransferQtyBL.Text = frm84.total_bl_transfer.ToString();
                    txtTransferQtyLPL.Text = frm84.total_lp_transfer.ToString();

                    List<All_Approvals> approvals = new List<All_Approvals>();
                    approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), frm84.receiver_storage_transfer_id.ToString(), "RTST");
                            var list4 = (from s in approvals
                                         where s.financial_year == Session["rtfinancial_year"].ToString()
                                         select s);
                            grdApprovalDetails.DataSource = list4.ToList();
                            grdApprovalDetails.DataBind();
                    //if (frm84.record_status == "Y" || user.role_name == "Bond Officer")
                    //{
                    //    foreach (GridViewRow dr1 in grdReceiverVat.Rows)
                    //    {
                    //        ImageButton btn = dr1.FindControl("ImageButton1") as ImageButton;
                    //        btn.Visible = false;
                    //    }
                    //    Image1.Visible = false;
                    //}

                    // txtRemarks.Text = frm84.remarks;
                    if (Session["rtype"].ToString() == "1")
                    {
                        txtTransferDate.Attributes.Add("Disabled", "Disabled");
                        txtDipInWetCms.Attributes.Add("Disabled", "Disabled");
                        txtTemprature.Attributes.Add("Disabled", "Disabled");
                        txtIndication.Attributes.Add("Disabled", "Disabled");
                        txtStrength.Attributes.Add("Disabled", "Disabled");

                        ddlToStoragevat.Attributes.Add("Disabled", "Disabled");
                        txtIncreaseBLLitresInOperation.Attributes.Add("Disabled", "Disabled");
                        txtIncreaseBLLitresByGroging.Attributes.Add("Disabled", "Disabled");
                        txtDecreasByBlending.Attributes.Add("Disabled", "Disabled");
                        txtDecreasByRacking.Attributes.Add("Disabled", "Disabled");
                        txtDecreasByReduction.Attributes.Add("Disabled", "Disabled");
                        txtDecreasByWastageStroage.Attributes.Add("Disabled", "Disabled");
                        txtTransferTime.Attributes.Add("Disabled", "Disabled");
                        txtTransferQtyBL.ReadOnly = true;
                        ddTransferToDispatchVat.Enabled = false;
                        btnSaveasDraft.Visible = false;
                        btnSubmit.Visible = false;
                        btnCancel.Visible = false;
                        approverremarks.Visible = false;
                        txtApproverremarks.Visible = false;
                          txtRemarks.Attributes.Add("Disabled", "Disabled");
                   
                    if (user.role_name == "Bond Officer" && frm84.record_status == "Y")
                    {
                        btnApprove.Visible = true;
                        btnReject.Visible = true;
                        approverid.Visible = true;
                        approverremarks.Visible = true;
                            txtApproverremarks.Visible = true;
                    }
                    if (frm84.record_status == "A" || frm84.record_status == "R")
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
            Response.Redirect("ReceiverTransferList");
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

        protected void lnkFromStoragetoDispatch_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("FromStoragetoDispatchList");
        }
        protected void lnkRawMaterialWastage_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawmaterialWastageList.aspx");
        }

        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReceiverTransferList");
          
        }
        protected void lnkRMR_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialReceiptList");
        }
        protected void btnReceipts_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReceivertoStorageList");
          
        }
        protected void btnVATtansfers_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATTransferList");
        }

        protected void ddlToStoragevat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if(ddlToStoragevat.SelectedValue !="Select")
                {
                    string vatcode = ddlToStoragevat.SelectedValue;
                    string code = party_code.Value;
                    ReceiverToStoragrTransfer aval = new ReceiverToStoragrTransfer();
                    aval = BL_ReceiverToStoragrTransfer.GetVatData(vatcode, code);
                    txtAvailableQtyBL.Text = aval.vat_availablecapacity.ToString();
                }
               
            }
        }

        protected void txtTransferQty_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if(txtTransferQtyBL.Text !="")
                {
                    if(Convert.ToDouble(txtTransferQtyBL.Text) <= Convert.ToDouble(txtAvailableQtyBL.Text))
                    {
                        if (txtStrength.Text != "")
                        {
                            txtTransferQtyLPL.Text = (Convert.ToDouble(txtTransferQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                        }
                        else
                        {
                           
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Strength \');", true);
                            txtStrength.Focus();
                        }
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Transfer Qty can be less then or equal to Available Qty \');", true);
                        txtTransferQtyBL.Focus();
                        txtTransferQtyBL.Text = "";
                    }
                }

               
            }
        }
        protected void txtStrength_TextChanged(object sender, EventArgs e)
        {
            if (txtTransferQtyBL.Text != "")
            {
                txtTransferQtyLPL.Text = (Convert.ToDouble(txtTransferQtyBL.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
            }
        }



        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                double total = 0;
                double total1 = 0;
                ReceiverToStoragrTransfer form84 = new ReceiverToStoragrTransfer();
                if(txtTransferDate.Text=="" || txtTransferDate.Text !="")
                {
                    txtTransferDate.Text=txttrdate.Value;
                }
                form84.transfer_date = txtTransferDate.Text.Substring(0, 10);
                form84.transfer_time = txtTransferTime.Value;
                form84.party_code = party_code.Value;
                form84.fromstoragevat = ddlToStoragevat.SelectedValue;
           total=  Convert.ToDouble(txtTransferQtyBL.Text);
                total1= Convert.ToDouble(txtTransferQtyLPL.Text);
                if (txtIncreaseBLLitresByGroging.Text != "")
                {
                    form84.inc_groging = Convert.ToDouble(txtIncreaseBLLitresByGroging.Text);
                  //  total += Convert.ToDouble(txtIncreaseBLLitresByGroging.Text);
                  //  total1 = (Convert.ToDouble(total) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
                }
                if (txtIncreaseBLLitresInOperation.Text != "")
                {
                    form84.inc_operation = Convert.ToDouble(txtIncreaseBLLitresInOperation.Text);
                   // total += Convert.ToDouble(txtIncreaseBLLitresInOperation.Text);
                  //  total1 = (Convert.ToDouble(total) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
                }
                form84.indication = Convert.ToDouble(txtIndication.Text);
                form84.dips = Convert.ToDouble(txtDipInWetCms.Text);
                form84.temperature = Convert.ToDouble(txtTemprature.Text);
                form84.strength = Convert.ToDouble(txtStrength.Text);

                if (txtDecreasByBlending.Text != "")
                {
                    form84.dec_blending = Convert.ToDouble(txtDecreasByBlending.Text);
                   // total -= Convert.ToDouble(txtDecreasByBlending.Text);
                   // total1 = (Convert.ToDouble(total) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
                }
                if (txtDecreasByRacking.Text != "")
                {
                    form84.dec_racking = Convert.ToDouble(txtDecreasByRacking.Text);
                    //total -= Convert.ToDouble(txtDecreasByRacking.Text);
                  //  total1 = (Convert.ToDouble(total) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
                }
                if (txtDecreasByReduction.Text != "")
                {
                    form84.dec_reduction = Convert.ToDouble(txtDecreasByReduction.Text);
                  //  total -= Convert.ToDouble(txtDecreasByReduction.Text);
                   // total1 = (Convert.ToDouble(total) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
                }
                if (txtDecreasByWastageStroage.Text != "")
                {
                    form84.dec_wastage = Convert.ToDouble(txtDecreasByWastageStroage.Text);
                  //  total -= Convert.ToDouble(txtDecreasByWastageStroage.Text);
                 //   total1 = (Convert.ToDouble(total) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
                }

                //total1 += (Convert.ToDouble(total) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
                if (total < 0)
                {
                    form84.total_bl_transfer = 0;
                    form84.total_lp_transfer = 0;
                }
                else
                {
                    form84.total_bl_transfer = total;
                    form84.total_lp_transfer = total1;
                }
              //  form84.total_bl_transfer = Convert.ToDouble(txtTransferQtyBL.Text);
              //  form84.total_lp_transfer = Convert.ToDouble(txtTransferQtyLPL.Text);
                form84.todispatchvat = ddTransferToDispatchVat.SelectedValue;
                form84.remarks = txtRemarks.Text;
                form84.user_id = Session["UserID"].ToString();
                form84.financial_year= Session["financial_year"].ToString();
                form84.record_status = "N";
                string val;
                string a = Session["rtype"].ToString();
                if (Session["rtype"].ToString() == "0")
                {
                    val = BL_ReceiverToStoragrTransfer.Insert(form84);
                }
                   
                else
                {
                    form84.receiver_storage_transfer_id = Convert.ToInt32(Session["ReceivertoStorage"].ToString());
                    val = BL_ReceiverToStoragrTransfer.Update(form84);
                }
                   
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("ReceiverTransferList");
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

        protected void btnSubmit_Click1(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                double total = 0;
                double total1 = 0;
                ReceiverToStoragrTransfer form84 = new ReceiverToStoragrTransfer();
                if (txtTransferDate.Text == "" || txtTransferDate.Text != "")
                {
                    txtTransferDate.Text = txttrdate.Value;
                }
                form84.transfer_date = txtTransferDate.Text.Substring(0, 10);
                form84.transfer_time = txtTransferTime.Value;
                form84.party_code = party_code.Value;
                form84.fromstoragevat = ddlToStoragevat.SelectedValue;
                total = Convert.ToDouble(txtTransferQtyBL.Text);
                total1 = Convert.ToDouble(txtTransferQtyLPL.Text);
                if (txtIncreaseBLLitresByGroging.Text != "")
                {
                    form84.inc_groging = Convert.ToDouble(txtIncreaseBLLitresByGroging.Text);
                  //  total += Convert.ToDouble(txtIncreaseBLLitresByGroging.Text);
                  //  total1 = (Convert.ToDouble(total) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
                }
                if (txtIncreaseBLLitresInOperation.Text != "")
                {
                    form84.inc_operation = Convert.ToDouble(txtIncreaseBLLitresInOperation.Text);
                  //  total += Convert.ToDouble(txtIncreaseBLLitresInOperation.Text);
                  //  total1 = (Convert.ToDouble(total) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
                }
                form84.indication = Convert.ToDouble(txtIndication.Text);
                form84.dips = Convert.ToDouble(txtDipInWetCms.Text);
                form84.temperature = Convert.ToDouble(txtTemprature.Text);
                form84.strength = Convert.ToDouble(txtStrength.Text);

                if (txtDecreasByBlending.Text != "")
                {
                    form84.dec_blending = Convert.ToDouble(txtDecreasByBlending.Text);
                    //total -= Convert.ToDouble(txtDecreasByBlending.Text);
                   // total1 = (Convert.ToDouble(total) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
                }
                if (txtDecreasByRacking.Text != "")
                {
                    form84.dec_racking = Convert.ToDouble(txtDecreasByRacking.Text);
                   // total -= Convert.ToDouble(txtDecreasByRacking.Text);
                   // total1 = (Convert.ToDouble(total) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
                }
                if (txtDecreasByReduction.Text != "")
                {
                    form84.dec_reduction = Convert.ToDouble(txtDecreasByReduction.Text);
                   // total -= Convert.ToDouble(txtDecreasByReduction.Text);
                    //total1 = (Convert.ToDouble(total) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
                }
                if (txtDecreasByWastageStroage.Text != "")
                {
                    form84.dec_wastage = Convert.ToDouble(txtDecreasByWastageStroage.Text);
                    //total -= Convert.ToDouble(txtDecreasByWastageStroage.Text);
                    //total1 = (Convert.ToDouble(total) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
                }

               // total1 += (Convert.ToDouble(total) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
                if (total < 0)
                {
                    form84.total_bl_transfer = 0;
                    form84.total_lp_transfer = 0;
                }
                else
                {
                    form84.total_bl_transfer = total;
                    form84.total_lp_transfer = total1;
                }

                form84.todispatchvat = ddTransferToDispatchVat.SelectedValue;
            
                form84.remarks = txtRemarks.Text;
                form84.user_id = Session["UserID"].ToString();
                form84.financial_year = Session["financial_year"].ToString();
                form84.record_status = "Y";
                string val;
                if (Session["rtype"].ToString() == "0")
                    val = BL_ReceiverToStoragrTransfer.Insert(form84);
                else
                    form84.receiver_storage_transfer_id= Convert.ToInt32(Session["ReceivertoStorage"].ToString());
                val = BL_ReceiverToStoragrTransfer.Update(form84);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("ReceiverTransferList");
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
            Response.Redirect("ReceiverTransferList");
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                ReceiverToStoragrTransfer ferm = new ReceiverToStoragrTransfer();

                ferm.record_status = "A";
                string val;
                ferm.receiver_storage_transfer_id = Convert.ToInt32(Session["ReceivertoStorage"].ToString());
                ferm.remarks = txtApproverremarks.Value;
                ferm.user_id = Session["UserID"].ToString();
                ferm.party_code = party_code.Value;
                ferm.fromstoragevat = ddlToStoragevat.SelectedValue;
                ferm.total_bl_transfer =Convert.ToDouble(txtTransferQtyBL.Text);
                ferm.total_lp_transfer = Convert.ToDouble(txtTransferQtyLPL.Text);
                ferm.todispatchvat = ddTransferToDispatchVat.SelectedValue;
                ferm.financial_year = Session["financial_year"].ToString();
                val = BL_ReceiverToStoragrTransfer.Approve(ferm);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("ReceiverTransferList");
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
                ReceiverToStoragrTransfer ferm = new ReceiverToStoragrTransfer();
                ferm.record_status = "R";
                string val;
                ferm.receiver_storage_transfer_id = Convert.ToInt32(Session["ReceivertoStorage"].ToString());
                ferm.remarks = txtApproverremarks.Value;
                ferm.user_id = Session["UserID"].ToString();
                ferm.party_code = party_code.Value;
                ferm.total_bl_transfer = Convert.ToDouble(txtTransferQtyBL.Text);
                ferm.total_lp_transfer = Convert.ToDouble(txtTransferQtyLPL.Text);
                ferm.financial_year = Session["financial_year"].ToString();
                val = BL_ReceiverToStoragrTransfer.Approve(ferm);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("ReceiverTransferList");
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
    }
}