using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class ReceivertoStorageForm : System.Web.UI.Page
    {
        static string _party_code;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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
                    if(user != null)
                    {
                        Session["party_code"] = user.party_code;
                        Session["rolename"] = user.role_name;
                        Session["financial_year"] = user.financial_year;
                        List<FReceiverOuput> form83 = new List<FReceiverOuput>();
                form83 = BL_ReceiverToStorage_84.GetListreceiverop(user.party_code);
              var list1 = from s in form83
                          select s;
                ddlProductionDate.DataSource = list1.ToList();
                ddlProductionDate.DataTextField = "removal_date";
               //ddlProductionDate.DataValueField = "fermenter_receiver_id";
               ddlProductionDate.DataBind();
               ddlProductionDate.Items.Insert(0, "Select");
                party_code.Value = user.party_code.ToString();
                _party_code = party_code.Value;
                btnApprove.Visible = false;
                btnReject.Visible = false;
                approverid.Visible =true;
                approverremarks.Visible = false;
                        receivervalue.Visible = false;
                        grdvalue.Visible = false;
                if (Session["rtype"].ToString() != "0")
                {
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                    ReceiverToStorage_84 frm84 = new ReceiverToStorage_84();
                    string frm84id = Session["ReceivertoStorageId"].ToString();
                    //  string from_receivervat = Session["from_receivervat"].ToString();
                    string Party_Code = Session["Party_Code"].ToString();
                    frm84 = BL_ReceiverToStorage_84.GetDetails(Party_Code, frm84id, Session["rstfinancial_year"].ToString());
                    txtReceiptDate.Text = frm84.receipt_date;
                        CalendarExtender.SelectedDate = Convert.ToDateTime(frm84.receipt_date);
                    txttrdate.Value= frm84.receipt_date;
                    txtReceiptTime.Value = frm84.receipt_time;
                    rtype.Value = Session["rtype"].ToString();


                    if (frm84.record_status == "Y" || frm84.record_status == "A")
                    {
                        form83 = BL_ReceiverToStorage_84.Getsubmiteddate(party_code.Value);
                        var list3 = from s in form83
                                    select s;
                        ddlProductionDate.DataSource = list3.ToList();
                        ddlProductionDate.DataTextField = "removal_date";
                        //ddlProductionDate.DataValueField = "fermenter_receiver_id";
                        ddlProductionDate.DataBind();
                         ddlProductionDate.SelectedValue = frm84.production_date;
                                form83 = BL_ReceiverToStorage_84.GetStoragevat(party_code.Value, frm84.production_date,user.financial_year);
                                var list = from s in form83
                                           where s.moved_to_nextstage == "Y"
                                           select s;
                                ddlToStoragevat.DataSource = list.ToList();
                                ddlToStoragevat.DataTextField = "vat_name";
                                ddlToStoragevat.DataValueField = "to_storagevat";
                                ddlToStoragevat.DataBind();
                            }
                    else
                    {
                        ddlProductionDate.SelectedValue = frm84.production_date;
                                form83 = BL_ReceiverToStorage_84.GetStoragevat(party_code.Value, frm84.production_date,user.financial_year);
                                var list = from s in form83
                                           where s.moved_to_nextstage == "N"
                                           select s;
                                ddlToStoragevat.DataSource = list.ToList();
                                ddlToStoragevat.DataTextField = "vat_name";
                                ddlToStoragevat.DataValueField = "to_storagevat";
                                ddlToStoragevat.DataBind();
                     }

                  //  ddlProductionDate.SelectedValue = frm84.production_date;
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
                   
                      ddlToStoragevat.SelectedValue = frm84.to_storagevat;
                            ddlToStoragevat_SelectedIndexChanged(sender, e);
                    //List<FReceiverInput> form84 = new List<FReceiverInput>();
                    // form84 = BL_ReceiverToStorage_84.GetReceiverVAt(frm84.to_storagevat,frm84.production_date, party_code.Value);
                    //   grdReceiverVat.DataSource = form84;
                    //   grdReceiverVat.DataBind();
                    //dummytable.Visible = false;

                            List<All_Approvals> approvals = new List<All_Approvals>();
                    approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), frm84.receiver_storage_receipt_id.ToString(), "RTS");
                            var list4 = (from s in approvals
                                         where s.financial_year == Session["rstfinancial_year"].ToString()
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

                    txtRemarks.Text = frm84.remarks;
                    if (Session["rtype"].ToString() == "1")
                    {
                       txtReceiptDate.Attributes.Add("Disabled", "Disabled");
                        txtDipInWetCms.ReadOnly = true;
                                txtTemprature.ReadOnly = true;
                                txtIndication.ReadOnly = true;
                                txtStrength.ReadOnly = true;
                                ddlToStoragevat.Attributes.Add("Disabled", "Disabled");
                        txtIncreaseBLLitresInOperation.ReadOnly = true;
                                txtIncreaseBLLitresByGroging.ReadOnly = true;
                                txtDecreasByBlending.ReadOnly = true;
                        txtDecreasByRacking.ReadOnly = true;
                                txtDecreasByReduction.ReadOnly = true;
                                txtDecreasByWastageStroage.ReadOnly = true;
                                txtReceiptTime.Attributes.Add("Disabled", "Disabled");
                        ddlProductionDate.Attributes.Add("Disabled", "Disabled");
                        txtRemarks.Attributes.Add("Disabled", "Disabled");
                        btnSaveasDraft.Visible = false;
                        btnSubmit.Visible = false;
                        btnCancel.Visible = false;
                        txtApproverremarks.Visible = false;
                        approverremarks.Visible = false;
                                receivervalue.Visible = false;
                                grdvalue.Visible = false;

                                if (user.role_name == "Bond Officer" && frm84.record_status == "Y")
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
                    if (frm84.record_status == "A" || frm84.record_status == "R")
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
        [WebMethod]
        //public static string GetValuesofVAT(Object vatcode)
        //{
             
        //    string value = BL_ReceiverToStorage_84.GetExistsData(vatcode.ToString(),_party_code);
        //    return value;
        //}
        protected void ddlProductionDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (ddlProductionDate.SelectedValue != null)
                {
                    string date =Convert.ToDateTime( ddlProductionDate.SelectedValue).ToString("dd-MM-yyyy");
                    List<FReceiverOuput> form83 = new List<FReceiverOuput>();
                    form83 = BL_ReceiverToStorage_84.GetStoragevat(party_code.Value, date, Session["financial_year"].ToString());
                    var list = from s in form83
                               where s.moved_to_nextstage=="N"
                               select s;
                    ddlToStoragevat.DataSource = list.ToList();
                    ddlToStoragevat.DataTextField = "vat_name";
                    ddlToStoragevat.DataValueField = "to_storagevat";
                    ddlToStoragevat.DataBind();
                    ddlToStoragevat.Items.Insert(0, "Select");

                }
            }
        }

        protected void ddlToStoragevat_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(IsPostBack)
            //{

               string date =Convert.ToDateTime( ddlProductionDate.SelectedValue).ToString("dd-MM-yyyy");
               string vat_code = ddlToStoragevat.SelectedValue;
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            if (ddlToStoragevat.SelectedValue != "Select")
                {
                FReceiverOuput vat = new FReceiverOuput();
                vat = BL_ReceiverToStorage_84.Gettotal(vat_code,  party_code.Value,date,user.financial_year);
                txtreceived.Text = vat.bl_tostorage.ToString();
                Session["lp"] = vat.lp_tostorage;
                 List <FReceiverOuput> form83 = new List<FReceiverOuput>();
                    form83 = BL_ReceiverToStorage_84.GetStoragevat(party_code.Value, ddlProductionDate.SelectedItem.ToString(),user.financial_year);
                    var list = from s in form83
                               
                               select s;
           
                    if (list.ToList().Count > 1)
                { 
                    List<FReceiverInput> form8 = new List<FReceiverInput>();
                List<FReceiverInput> form82 = new List<FReceiverInput>();
                        form82 = BL_ReceiverToStorage_84.GetReceiverVAt1(vat_code, date, party_code.Value,user.financial_year);
                    List<FReceiverInput> distinct = new List<FReceiverInput>();
                    distinct = BL_ReceiverToStorage_84.GetdistinctVAt(vat_code, date, party_code.Value,user.financial_year);
                    for (int i=0;i< distinct.Count;i++)
                    {
                        List<FReceiverInput> form = new List<FReceiverInput>();
                        form = BL_ReceiverToStorage_84.GetReceiver(distinct[i].fermenter_receiver_id,user.financial_year);
                        if(form.Count > 1)
                        {
                            form8.AddRange(form);
                        }
                        else
                        {
                            List<FReceiverInput> sto = new List<FReceiverInput>();
                            sto = BL_ReceiverToStorage_84.GetSTOVAt(distinct[i].fermenter_receiver_id,vat_code,user.financial_year);
                            form8.AddRange(sto);
                        }
                    }
                    var list2 = from s in form8
                                where  s.financial_year == user.financial_year
                               select s;
                    grdReceiverVat.DataSource = list2.ToList();
                        grdReceiverVat.DataBind();
                        dummytable.Visible = false;
                    }
                    else
                    {
                        dummytable.Visible = false;
                        List<FReceiverInput> form82 = new List<FReceiverInput>();
                        form82 = BL_ReceiverToStorage_84.GetReceiverVAt(vat_code, date, party_code.Value,user.financial_year);
                    var list2 = from s in form82
                                where s.financial_year == user.financial_year
                                select s;
                    grdReceiverVat.DataSource = list2.ToList();
                    grdReceiverVat.DataBind();
                        dummytable.Visible = false;
                    }
                }

           // }

        }
        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ReceivertoStorageList");
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


        protected void lnkRMR_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialReceiptList");
        }
        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReceiverTransferList");

        }

        protected void btnReceipts_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReceivertoStorageList");

        }
        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                //btnSaveasDraft.Visible = false;
                //btnSubmit.Visible = false;
                //btnCancel.Visible = false;

                double total = 0;
                double total1 = 0;
                ReceiverToStorage_84 form84 = new ReceiverToStorage_84();
                if (txtReceiptDate.Text == "" || txtReceiptDate.Text !="")
                {
                    txtReceiptDate.Text = txttrdate.Value;
                }

                form84.receipt_date = txtReceiptDate.Text;
                form84.receipt_time = txtReceiptTime.Value;
                form84.party_code = party_code.Value;
                form84.to_storagevat = ddlToStoragevat.SelectedValue;
                form84.production_date = ddlProductionDate.SelectedValue;
              //  form84.fermenter_receiver_output_id = Convert.ToInt32(ddlProductionDate.SelectedValue);
                
             

                if (txtIncreaseBLLitresByGroging.Text !="")
                {
                    form84.inc_groging = Convert.ToDouble(txtIncreaseBLLitresByGroging.Text);
                   // total += Convert.ToDouble(txtIncreaseBLLitresByGroging.Text);
                  // total1 = (Convert.ToDouble(total) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
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
                   // total -= Convert.ToDouble(txtDecreasByRacking.Text);
                   // total1 = (Convert.ToDouble(total) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
                }
                if (txtDecreasByReduction.Text != "")
                {
                    form84.dec_reduction = Convert.ToDouble(txtDecreasByReduction.Text);
                   // total -= Convert.ToDouble(txtDecreasByReduction.Text);
                  // total1 = (Convert.ToDouble(total) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
                }
                if (txtDecreasByWastageStroage.Text != "")
                {
                    form84.dec_wastage = Convert.ToDouble(txtDecreasByWastageStroage.Text);
                  //  total -= Convert.ToDouble(txtDecreasByWastageStroage.Text);
                  // total1 = (Convert.ToDouble(total) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
                }
               // total1 += (Convert.ToDouble(total) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
              
               
                form84.remarks = txtRemarks.Text;
               form84.user_id = Session["UserID"].ToString();
                form84.financial_year = Session["financial_year"].ToString();
                form84.ReceiverReceiptVat = new List<ReceiptReceiverVat>();

              

                for (int i = 0; i < grdReceiverVat.Rows.Count; i++)
                {
                    GridViewRow row1 = grdReceiverVat.Rows[i];
                    string Qty1 = (row1.Cells[1].FindControl("lblBulkLiters") as Label).Text;
                    string Qty2 = (row1.Cells[1].FindControl("lblLPLiters") as Label).Text;
                    total += Convert.ToDouble(Qty1);
                    total1 += Convert.ToDouble(Qty2);
                }
                List<FReceiverOuput> form83 = new List<FReceiverOuput>();
                form83 = BL_ReceiverToStorage_84.GetStoragevat(party_code.Value, ddlProductionDate.SelectedItem.ToString(), form84.financial_year);
                var list = from s in form83
                           select s;
                if (list.ToList().Count > 1)
                {
                   FReceiverOuput vat = new FReceiverOuput();
                   vat = BL_ReceiverToStorage_84.Gettotal(ddlToStoragevat.SelectedValue, Session["party_code"].ToString(), ddlProductionDate.SelectedItem.ToString(), form84.financial_year);

                    form84.total_bl_receipt =vat.bl_tostorage;
                    form84.total_lp_receipt = vat.lp_tostorage;
                    List<FReceiverInput> form8 = new List<FReceiverInput>();
                    form8 = BL_ReceiverToStorage_84.GetReceiverVAtvalue(ddlToStoragevat.SelectedValue, Session["party_code"].ToString(), ddlProductionDate.SelectedItem.ToString(),form84.financial_year);
                    grdvalue.DataSource = form8;
                    grdvalue.DataBind();
                    for (int j = 0; j < grdvalue.Rows.Count; j++)
                    {
                        ReceiptReceiverVat setup = new ReceiptReceiverVat();
                        setup.from_receivervat = (grdvalue.Rows[j].FindControl("lblvatcode") as Label).Text;
                        setup.bl_receipt = Convert.ToDouble((grdvalue.Rows[j].FindControl("lblBulkLiters") as Label).Text);
                        setup.lp_receipt = Convert.ToDouble((grdvalue.Rows[j].FindControl("lblLPLiters") as Label).Text);
                        form84.ReceiverReceiptVat.Add(setup);
                    }
                    receivervalue.Visible = false;
                }
                else
                {
                    for (int j = 0; j < grdReceiverVat.Rows.Count; j++)
                    {
                        ReceiptReceiverVat setup = new ReceiptReceiverVat();

                        setup.from_receivervat = (grdReceiverVat.Rows[j].FindControl("lblvatcode") as Label).Text;
                        string a = (grdReceiverVat.Rows[j].FindControl("lblremovalhour") as Label).Text; 
                        if ( total == Convert.ToDouble(txtreceived.Text)  )
                        {
                            setup.bl_receipt = Convert.ToDouble((grdReceiverVat.Rows[j].FindControl("lblBulkLiters") as Label).Text);
                            setup.lp_receipt = Convert.ToDouble((grdReceiverVat.Rows[j].FindControl("lblLPLiters") as Label).Text);
                            form84.total_bl_receipt = total;
                            form84.total_lp_receipt = total1;
                        
                        }
                        else
                        {
                            ReceiverToStorage_84 qty = new ReceiverToStorage_84();
                            qty = BL_ReceiverToStorage_84.GetVatAval(setup.from_receivervat, party_code.Value, ddlProductionDate.SelectedValue, a, form84.financial_year);
                            setup.bl_receipt = qty.total_bl_receipt;
                            setup.lp_receipt = qty.total_lp_receipt;
                            form84.total_bl_receipt += setup.bl_receipt;
                            form84.total_lp_receipt += setup.lp_receipt;
                        }

                        form84.ReceiverReceiptVat.Add(setup);
                    }
                  
                }
                form84.record_status = "N";
                string val;
                if (Session["rtype"].ToString() =="0")
                {
                    val = BL_ReceiverToStorage_84.Insert(form84);
                }
                else
                {
                    form84.receiver_storage_receipt_id = Convert.ToInt32(Session["ReceivertoStorageId"].ToString());
                    val = BL_ReceiverToStorage_84.Update(form84);
                }
                   
                if(val=="0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("ReceivertoStorageList");
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
                btnSubmit.Enabled = false;
                if(grdReceiverVat.Rows.Count==0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert(' Add ReceiverVat');", true);
                    ddlToStoragevat.ClearSelection();
                    ddlToStoragevat.Focus();
                }
                else {
                double total = 0;
                double total1 = 0;
                ReceiverToStorage_84 form84 = new ReceiverToStorage_84();
                    if (txtReceiptDate.Text == "" || txtReceiptDate.Text != "")
                    {
                        txtReceiptDate.Text = txttrdate.Value;
                    }


                    form84.receipt_date = txtReceiptDate.Text.Substring(0, 10);
                form84.receipt_time = txtReceiptTime.Value;
                form84.party_code = party_code.Value;
                form84.to_storagevat = ddlToStoragevat.SelectedValue;
                form84.production_date = ddlProductionDate.SelectedItem.ToString();
                //form84.fermenter_receiver_output_id = Convert.ToInt32(ddlProductionDate.SelectedValue);
                //int i = 0;
                //total = Convert.ToDouble((grdReceiverVat.Rows[i].FindControl("lblBulkLiters") as Label).Text);
                //total1 = Convert.ToDouble((grdReceiverVat.Rows[i].FindControl("lblLPLiters") as Label).Text);
                //form84.receivervat = (grdReceiverVat.Rows[i].FindControl("lblvatcode") as Label).Text;
                if (txtIncreaseBLLitresByGroging.Text != "")
                {
                    form84.inc_groging = Convert.ToDouble(txtIncreaseBLLitresByGroging.Text);
                   // total += Convert.ToDouble(txtIncreaseBLLitresByGroging.Text);
                     // total1 = (Convert.ToDouble(total) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
                }
                if (txtIncreaseBLLitresInOperation.Text != "")
                {
                    form84.inc_operation = Convert.ToDouble(txtIncreaseBLLitresInOperation.Text);
                   // total += Convert.ToDouble(txtIncreaseBLLitresInOperation.Text);
                     // total1 = (Convert.ToDouble(total) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
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
                   // total -= Convert.ToDouble(txtDecreasByRacking.Text);
                     //total1 = (Convert.ToDouble(total) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
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
                    // total1 = (Convert.ToDouble(total) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
                }
               // total1 += (Convert.ToDouble(total) * (1 + (Convert.ToDouble(txtStrength.Text) / 100)));
                form84.remarks = txtRemarks.Text;
                form84.user_id = Session["UserID"].ToString();
                    form84.financial_year = Session["financial_year"].ToString();
                    form84.ReceiverReceiptVat = new List<ReceiptReceiverVat>();
                   

                    for (int i = 0; i < grdReceiverVat.Rows.Count; i++)
                    {
                        GridViewRow row1 = grdReceiverVat.Rows[i];
                        string Qty1 = (row1.Cells[1].FindControl("lblBulkLiters") as Label).Text;
                        string Qty2 = (row1.Cells[1].FindControl("lblLPLiters") as Label).Text;
                        total += Convert.ToDouble(Qty1);
                        total1 += Convert.ToDouble(Qty2);
                    }
                    List<FReceiverOuput> form83 = new List<FReceiverOuput>();
                    form83 = BL_ReceiverToStorage_84.GetStoragevat(party_code.Value, ddlProductionDate.SelectedItem.ToString(), form84.financial_year);
                    var list = from s in form83
                               select s;
                    if (list.ToList().Count > 1)
                    {
                         FReceiverOuput vat = new FReceiverOuput();
                         vat = BL_ReceiverToStorage_84.Gettotal(ddlToStoragevat.SelectedValue, Session["party_code"].ToString(), ddlProductionDate.SelectedItem.ToString(), form84.financial_year);
                        form84.total_bl_receipt = vat.bl_tostorage;
                        form84.total_lp_receipt = vat.lp_tostorage;
                        List<FReceiverInput> form8 = new List<FReceiverInput>();
                        form8 = BL_ReceiverToStorage_84.GetReceiverVAtvalue(ddlToStoragevat.SelectedValue, Session["party_code"].ToString(), ddlProductionDate.SelectedItem.ToString(), form84.financial_year);
                        grdvalue.DataSource = form8;
                        grdvalue.DataBind();
                        for (int j = 0; j < grdvalue.Rows.Count; j++)
                        {
                            ReceiptReceiverVat setup = new ReceiptReceiverVat();
                            setup.from_receivervat = (grdvalue.Rows[j].FindControl("lblvatcode") as Label).Text;
                            setup.bl_receipt = Convert.ToDouble((grdvalue.Rows[j].FindControl("lblBulkLiters") as Label).Text);
                            setup.lp_receipt = Convert.ToDouble((grdvalue.Rows[j].FindControl("lblLPLiters") as Label).Text);
                            form84.ReceiverReceiptVat.Add(setup);
                        }
                        receivervalue.Visible = false;
                    }
                    else
                    {
                        for (int j = 0; j < grdReceiverVat.Rows.Count; j++)
                        {
                            ReceiptReceiverVat setup = new ReceiptReceiverVat();
                            setup.from_receivervat = (grdReceiverVat.Rows[j].FindControl("lblvatcode") as Label).Text;
                            string a = (grdReceiverVat.Rows[j].FindControl("lblremovalhour") as Label).Text;
                            if (total == Convert.ToDouble(txtreceived.Text))
                            {
                                setup.bl_receipt = Convert.ToDouble((grdReceiverVat.Rows[j].FindControl("lblBulkLiters") as Label).Text);
                                setup.lp_receipt = Convert.ToDouble((grdReceiverVat.Rows[j].FindControl("lblLPLiters") as Label).Text);
                                form84.total_bl_receipt = total;
                                form84.total_lp_receipt = total1;
                              
                            }
                            else
                            {
                                ReceiverToStorage_84 qty = new ReceiverToStorage_84();
                                qty = BL_ReceiverToStorage_84.GetVatAval(setup.from_receivervat, party_code.Value, ddlProductionDate.SelectedValue, a, form84.financial_year);
                                setup.bl_receipt = qty.total_bl_receipt;
                                setup.lp_receipt = qty.total_lp_receipt;
                                form84.total_bl_receipt += setup.bl_receipt;
                                form84.total_lp_receipt += setup.lp_receipt;
                            }
                            form84.ReceiverReceiptVat.Add(setup);
                        }
                       
                       
                    }
                    //if (total < 0)
                    //{
                    //    form84.total_bl_receipt = 0;
                    //    form84.total_lp_receipt = 0;
                    //}
                    //else
                    //{
                    //    FReceiverOuput vat = new FReceiverOuput();
                    //    vat= BL_ReceiverToStorage_84.Gettotal(ddlToStoragevat.SelectedValue, Session["UserID"].ToString(),ddlProductionDate.SelectedItem.ToString());
                    //    form84.total_bl_receipt = vat.bl_tostorage;
                    //    form84.total_lp_receipt = vat.lp_tostorage;
                    //}

                    form84.record_status = "Y";
                string val;
                if (Session["rtype"].ToString() == "0")
                    val = BL_ReceiverToStorage_84.Insert(form84);
                else
                    form84.receiver_storage_receipt_id = Convert.ToInt32(Session["ReceivertoStorageId"].ToString());
                val = BL_ReceiverToStorage_84.Update(form84);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("ReceivertoStorageList");
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
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ReceivertoStorageList");
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                btnApprove.Enabled = false;
                ReceiverToStorage_84 ferm = new ReceiverToStorage_84();
                double total = 0;
                double total1 = 0;
                ferm.record_status = "A";
                string val;
                ferm.receiver_storage_receipt_id = Convert.ToInt32(Session["ReceivertoStorageId"].ToString());
                ferm.remarks = txtApproverremarks.Value;
                ferm.user_id = Session["UserID"].ToString();
                ferm.to_storagevat = ddlToStoragevat.SelectedValue;
                ferm.production_date = ddlProductionDate.SelectedItem.ToString();
                ferm.party_code = party_code.Value;
                ferm.financial_year = Session["financial_year"].ToString();
                if (txtIncreaseBLLitresByGroging.Text != "")
                {
                    ferm.inc_groging = Convert.ToDouble(txtIncreaseBLLitresByGroging.Text);
                }
                if (txtIncreaseBLLitresInOperation.Text != "")
                {
                    ferm.inc_operation = Convert.ToDouble(txtIncreaseBLLitresInOperation.Text);
                }
                ferm.indication = Convert.ToDouble(txtIndication.Text);
                ferm.dips = Convert.ToDouble(txtDipInWetCms.Text);
                ferm.temperature = Convert.ToDouble(txtTemprature.Text);
                ferm.strength = Convert.ToDouble(txtStrength.Text);
                if (txtDecreasByBlending.Text != "")
                {
                    ferm.dec_blending = Convert.ToDouble(txtDecreasByBlending.Text);
                }
                if (txtDecreasByRacking.Text != "")
                {
                    ferm.dec_racking = Convert.ToDouble(txtDecreasByRacking.Text);
                }
                if (txtDecreasByReduction.Text != "")
                {
                    ferm.dec_reduction = Convert.ToDouble(txtDecreasByReduction.Text);
                }
                if (txtDecreasByWastageStroage.Text != "")
                {
                    ferm.dec_wastage = Convert.ToDouble(txtDecreasByWastageStroage.Text);
                }
                ferm.user_id = Session["UserID"].ToString();
                ferm.ReceiverReceiptVat = new List<ReceiptReceiverVat>();


                for (int i = 0; i < grdReceiverVat.Rows.Count; i++)
                {
                    GridViewRow row1 = grdReceiverVat.Rows[i];
                    string Qty1 = (row1.Cells[1].FindControl("lblBulkLiters") as Label).Text;
                    string Qty2 = (row1.Cells[1].FindControl("lblLPLiters") as Label).Text;
                    total += Convert.ToDouble(Qty1);
                    total1 += Convert.ToDouble(Qty2);
                }
                List<FReceiverOuput> form83 = new List<FReceiverOuput>();
                form83 = BL_ReceiverToStorage_84.GetStoragevat(party_code.Value, ddlProductionDate.SelectedItem.ToString(), ferm.financial_year);
                var list = from s in form83
                           select s;
                if (list.ToList().Count > 1)
                {
                    FReceiverOuput vat = new FReceiverOuput();
                    vat = BL_ReceiverToStorage_84.Gettotal(ddlToStoragevat.SelectedValue, Session["party_code"].ToString(), ddlProductionDate.SelectedItem.ToString(), ferm.financial_year);
                    ferm.total_bl_receipt = vat.bl_tostorage;
                    ferm.total_lp_receipt = vat.lp_tostorage;
                    List<FReceiverInput> form8 = new List<FReceiverInput>();
                    form8 = BL_ReceiverToStorage_84.GetReceiverVAtvalue(ddlToStoragevat.SelectedValue, Session["party_code"].ToString(), ddlProductionDate.SelectedItem.ToString(), ferm.financial_year);
                    grdvalue.DataSource = form8;
                    grdvalue.DataBind();
                    for (int j = 0; j < grdvalue.Rows.Count; j++)
                    {
                        ReceiptReceiverVat setup = new ReceiptReceiverVat();
                        setup.from_receivervat = (grdvalue.Rows[j].FindControl("lblvatcode") as Label).Text;
                        setup.bl_receipt = Convert.ToDouble((grdvalue.Rows[j].FindControl("lblBulkLiters") as Label).Text);
                        setup.lp_receipt = Convert.ToDouble((grdvalue.Rows[j].FindControl("lblLPLiters") as Label).Text);
                        ferm.ReceiverReceiptVat.Add(setup);
                    }
                    receivervalue.Visible = false;
                }
                else
                {
                    for (int j = 0; j < grdReceiverVat.Rows.Count; j++)
                    {
                        ReceiptReceiverVat setup = new ReceiptReceiverVat();
                        setup.from_receivervat = (grdReceiverVat.Rows[j].FindControl("lblvatcode") as Label).Text;
                        string a = (grdReceiverVat.Rows[j].FindControl("lblremovalhour") as Label).Text;
                        if (total == Convert.ToDouble(txtreceived.Text))
                        {
                            setup.bl_receipt = Convert.ToDouble((grdReceiverVat.Rows[j].FindControl("lblBulkLiters") as Label).Text);
                            setup.lp_receipt = Convert.ToDouble((grdReceiverVat.Rows[j].FindControl("lblLPLiters") as Label).Text);
                            ferm.total_bl_receipt = total;
                            ferm.total_lp_receipt = total1;

                        }
                        else
                        {
                            ReceiverToStorage_84 qty = new ReceiverToStorage_84();
                            qty = BL_ReceiverToStorage_84.GetVatAval(setup.from_receivervat, party_code.Value, ddlProductionDate.SelectedValue, a, ferm.financial_year);
                            setup.bl_receipt = qty.total_bl_receipt;
                            setup.lp_receipt = qty.total_lp_receipt;
                            ferm.total_bl_receipt += setup.bl_receipt;
                            ferm.total_lp_receipt += setup.lp_receipt;
                        }
                        ferm.ReceiverReceiptVat.Add(setup);
                    }
                   

                }
                val = BL_ReceiverToStorage_84.Approve(ferm);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("ReceivertoStorageList");
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
                btnReject.Enabled = false;
                ReceiverToStorage_84 ferm = new ReceiverToStorage_84();
                ferm.record_status = "R";
                string val;
                ferm.receiver_storage_receipt_id = Convert.ToInt32(Session["ReceivertoStorageId"].ToString());
                ferm.remarks = txtApproverremarks.Value;
                ferm.financial_year = Session["financial_year"].ToString();
                ferm.user_id = Session["UserID"].ToString();
                ferm.to_storagevat = ddlToStoragevat.SelectedValue;
                ferm.party_code = party_code.Value;

                ferm.production_date = ddlProductionDate.SelectedItem.ToString();
                val = BL_ReceiverToStorage_84.Approve(ferm);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("ReceivertoStorageList");
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

        protected void btnVATtansfers_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATTransferList");
        }
        protected void lnkRawMaterialWastage_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawmaterialWastageList.aspx");
        }

        protected void lnkOpeningBalance_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("OpeningBalanceList");

        }
    }
}