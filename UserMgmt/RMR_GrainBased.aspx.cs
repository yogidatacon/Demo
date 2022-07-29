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
    public partial class RMR_GrainBased : System.Web.UI.Page
    {
        List<RawMaterialReceipt> rawmaterial = new List<RawMaterialReceipt>();
        List<VAT_Master> vat = new List<VAT_Master>();
        RawMaterialReceipt rawmaterials = new RawMaterialReceipt();
        double total = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CalendarExtender.EndDate = DateTime.Now;
                CalendarExtender3.EndDate = DateTime.Now;
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];

                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    party_code.Value = user.party_code;
                    Session["financial_year"] = user.financial_year;
                    List<RawMaterial> raw = new List<RawMaterial>();
                    raw = BL_RawMaterial.GetRawMaterial(user.party_code);
                    ddlRawMaterial.DataSource = raw;
                    ddlRawMaterial.DataTextField = "rawmaterial_name";
                    ddlRawMaterial.DataValueField = "product_type_code";
                    ddlRawMaterial.DataBind();
                    ddlRawMaterial.Items.Insert(0, "Select");
                    List<UOM_Master> uom = new List<UOM_Master>();
                    uom = BL_UOM.GetList("");
                    ddlUom.DataSource = uom;
                    ddlUom.DataTextField = "uom_name";
                    ddlUom.DataValueField = "uom_code";
                    ddlUom.DataBind();
                    ddlUom.Items.Insert(0, "Select");
                 
                    Session["district_code"] = user.district_code;
                    Session["party_type"] = user.party_type;
                    if (user.party_type== "Distillery Unit")
                    {
                        ddlSupplierType.SelectedIndex = 2;
                        ddlSupplierType.SelectedValue = "Others";
                        txtp.Visible = true;
                        ddp.Visible = false;
                        ddlSupplierType.Enabled = false;
                        ddlSupplierName.Visible = false;
                        txtunit.Visible = true;
                        ena.Visible =false;
                        dis.Visible = true;
                    }
                    else
                    {
                        ena.Visible = true;
                        dis.Visible = false;
                        ddlSupplierType.SelectedIndex = 1;
                        if (ddlSupplierType.SelectedValue == "Sugar Mill")
                        {
                            List<Party_Master> party = new List<Party_Master>();
                            party = BL_Party_Master.GetList();
                            var list = from s in party
                                       where s.party_type_code == "SGR"
                                       select s;
                            ddlSupplierName.DataSource = list.ToList();
                            ddlSupplierName.DataTextField = "party_name";
                            ddlSupplierName.DataValueField = "party_code";
                            ddlSupplierName.DataBind();
                            ddlSupplierName.Items.Insert(0, "Select");
                            ddp.Visible = true;
                            txtp.Visible = false;
                            ddp.Visible = true;
                            ddlSupplierName.Visible = true;
                        }
                    }
                    txtDATE.ReadOnly = true;
                   txtreceiptdate.ReadOnly = true;
                    // rtype.Value =Session["rtype"];
                    if (Session["UserID"].ToString() == "Admin")
                    {
                        btnSaveasDraft.Visible =false;
                        btnSubmit.Visible =false;
                        btnCancel.Visible = true;
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        btnupdate.Visible = true;
                        txtRemarks1.Attributes.Add("disabled", "disabled");
                    }
                    else if (user.role_name == "Bond Officer")
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
                        approverremarks.Visible = false;
                       txtapproverremarks.Visible = false;
                    }
                   // hdtotal.Value = Session["rtype"].ToString();
                  
                    if (Session["rtype"].ToString() != "0")
                    {

                        RawMaterialReceipt rawmaterials = new RawMaterialReceipt();
                        rawmaterials = BL_RawMaterialReceipt.GetGainList(Convert.ToInt32(Session["receipt_id"].ToString()), Session["rgbfinancial_year"].ToString());
                        txtDATE.Text = rawmaterials.rmr_entrydate;
                        // txtGrossWeight.Text = rawmaterials.grossweight.ToString();
                        // txtTankerWeight.Text = rawmaterials.tankerweight.ToString();
                        txtReceiptNo.Text = rawmaterials.rmrpassno;
                        party_code.Value = rawmaterials.party_code;
                        // txtUnitName.Text = rawmaterials.party_name;
                       txtgpd.Value = rawmaterials.rmr_entrydate;
                        CalendarExtender.SelectedDate = Convert.ToDateTime(rawmaterials.rmr_entrydate);
                        ddlSupplierType.SelectedValue = rawmaterials.suppliertype;
                        if (ddlSupplierType.SelectedValue == "Sugar Mill")
                        {
                           ddlSupplierName.SelectedValue = rawmaterials.supplier;
                            ddlSupplierName.Visible = true;
                            ddp.Visible = true;
                            txtp.Visible = false;
                        }
                        else
                        {
                           txtunit.Text = rawmaterials.suppliername;
                            txtp.Visible = true;
                            txtunit.Visible = true;
                            ddp.Visible = false;
                            ddlSupplierType.SelectedValue = rawmaterials.suppliertype;
                        }

                       

                       ddlUom.SelectedValue = rawmaterials.uom;
                        ddlRawMaterial.SelectedValue = rawmaterials.rawmaterial;
                        receipt.Value = rawmaterials.passissuedate.Substring(0, 10).Replace("/", "-");
                        CalendarExtender3.SelectedDate = Convert.ToDateTime(rawmaterials.passissuedate);
                       txtreceiptqty.Text = rawmaterials.passqty.ToString();
                        Session["total"] = rawmaterials.passqty;
                        // hdtotal.Value= rawmaterials.passqty.ToString();
                        txtreceiptdate.Text = rawmaterials.passissuedate.Substring(0, 10).Replace("/", "-");
                        if (rawmaterials.rmr_entrydate != null)
                            CalendarExtender.SelectedDate = Convert.ToDateTime(rawmaterials.rmr_entrydate);

                        txtVehicleNo.Text = rawmaterials.vehicleno;
                        
                        rawmaterials.rawmaterial_receipt_id = Session["receipt_id"].ToString();
                        txtRemarks1.Value = rawmaterials.remarks;
                        List<RawMaterialReceipt> vats = new List<RawMaterialReceipt>();
                        vats = BL_RawMaterialReceipt.GetGrainvatsList(user.party_code, Session["receipt_id"].ToString().ToString(), rawmaterials.rawmaterial, Session["rgbfinancial_year"].ToString());
                        List<All_Approvals> approvals = new List<All_Approvals>();
                        approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), rawmaterials.rawmaterial_receipt_id.ToString(), "RMR");
                      
                        if (Session["UserID"].ToString() == "Admin")
                        {
                            grdrawmaterial.DataSource = vats;
                            grdrawmaterial.DataBind();
                            var list4 = (from s in approvals
                                         where s.user_id == "Admin"
                                         select s);
                            grdApprovalDetails.DataSource = list4.ToList();
                            grdApprovalDetails.DataBind();
                        }
                        else
                        {

                            var list = from s in vats
                                       where s.rawmaterial_receipt_id == Session["receipt_id"].ToString()
                                       select s;

                           grdrawmaterial.DataSource = list.ToList();
                            grdrawmaterial.DataBind();

                            foreach (GridViewRow dr1 in grdrawmaterial.Rows)
                            {
                                TextBox txt = dr1.FindControl("txtQuantity") as TextBox;
                                if (txtgpd.Value == "")
                                {
                                    txt.Text = "0";
                                }
                            }
                        
                            var list4 = (from s in approvals
                                         where s.financial_year == Session["rgbfinancial_year"].ToString()
                                         select s);
                            grdApprovalDetails.DataSource = list4.ToList();
                         
                            grdApprovalDetails.DataBind();
                        }
                        int receipt_id = Convert.ToInt32(Session["receipt_id"].ToString());
                        if (Session["party_type"].ToString() == "ENA Distillery Unit")
                        {
                            approverid.Visible = false;
                            approverremarks.Visible = false;
                            grdApprovalDetails.Visible = false;
                        }
                        else
                        {
                       
                        approverremarks.Visible = false;
                        if (approvals.Count <= 0)
                        {
                            approverid.Visible = false;
                            approverremarks.Visible = false;
                        }
                        }
                        if (Session["rtype"].ToString() == "1")
                        {
                            txtDATE.ReadOnly = true;
                            // txtGrossWeight.Enabled = false;
                            //   txtSupplierWeight.Enabled = false;
                            //  txtNetWeight.Enabled = false;
                            // txtNetWeight.Enabled = false;
                           txtReceiptNo.Enabled = false;
                            //  txtTankerWeight.Enabled = false;
                            //  txtTransitWastage.Enabled = false;
                           
                            ddlSupplierName.Enabled = false;
                            ddlRawMaterial.Enabled = false;
                            ddlSupplierType.Enabled = false;
                           ddlUom.Enabled = false;
                           
                            txtreceiptqty.Enabled = false;
                           txtunit.Enabled = false;
                            txtVehicleNo.Enabled = false;
                            //  txtApproverremarks.Enabled = false;
                            txtRemarks1.Attributes.Add("disabled", "disabled");
                            btnCancel.Visible = false;
                            btnSaveasDraft.Visible = false;
                            btnSubmit.Visible = false;
                           grdrawmaterial.Enabled = false;
                            btnApprove.Visible = false;
                            btnupdate.Visible =false;
                            btnReject.Visible = false;
                            Image1.Visible = false;
                            txtDATE.ReadOnly =true;
                            if (user.role_name == "Bond Officer" && rawmaterials.record_status == "Y")
                            {
                               
                               grdrawmaterial.Enabled = false;
                                btnReject.Visible = true;
                                btnApprove.Visible = true;
                               txtapproverremarks.Visible = true;
                            }

                        }
                        else
                        {
                            foreach (GridViewRow g1 in grdrawmaterial.Rows)
                            {
                                TextBox txt = g1.FindControl("txtQuantity") as TextBox;
                                TextBox txt1 = g1.FindControl("txtdips") as TextBox;
                                if ((rawmaterials.record_status == "Y" || rawmaterials.record_status == "A") && Session["UserID"].ToString() != "Admin")
                                {
                                    txt.Enabled = false;
                                    txt1.Enabled = false;
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
        protected void lnkShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RMR_GrainBasedList");
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
        protected void btnVATtansfers_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATTransferList");
        }
        protected void lnkRMR_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialReceiptList");
        }
        protected void btnOpeningBalance_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("OpeningBalanceList");
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RMR_GrainBasedList");
        }

        protected void ddlRawMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
            if (ddlRawMaterial.SelectedValue != "Select" && ddlRawMaterial.SelectedValue != "")
            {
                if (Session["rtype"].ToString() == "0")
                {
                    List<RawMaterialReceipt> vats = new List<RawMaterialReceipt>();
                    vats = BL_RawMaterialReceipt.GetvatsList1(party_code.Value, "0", ddlRawMaterial.SelectedValue);
                    var list = from s in vats
                               where s.product_type_code == ddlRawMaterial.SelectedValue
                               select s;
                    grdrawmaterial.DataSource = vats;
                    grdrawmaterial.DataBind();
                }
                else
                {
                    List<RawMaterialReceipt> vats = new List<RawMaterialReceipt>();
                    vats = BL_RawMaterialReceipt.GetGrainvatsList(party_code.Value, Session["receipt_id"].ToString(), ddlRawMaterial.SelectedValue, Session["rgbfinancial_year"].ToString());
                    var list = from s in vats
                               where s.product_type_code == ddlRawMaterial.SelectedValue
                               select s;
                    grdrawmaterial.DataSource = vats;
                    grdrawmaterial.DataBind();
                }
            }
            }
        }

        protected void ddlSupplierType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (ddlSupplierType.SelectedValue == "Sugar Mill")
                {
                    List<Party_Master> party = new List<Party_Master>();
                    party = BL_Party_Master.GetList();
                    var list = from s in party
                               where s.party_type_code == "SGR"
                               select s;
                    ddlSupplierName.DataSource = list.ToList();
                    ddlSupplierName.DataTextField = "party_name";
                    ddlSupplierName.DataValueField = "party_code";
                    ddlSupplierName.DataBind();
                    ddlSupplierName.Items.Insert(0, "Select");
                    ddp.Visible = true;
                    txtp.Visible = false;
                }
                else
                {
                    ddp.Visible = false;
                    txtp.Visible = true;
                }


            }
        }


        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                rawmaterial = new List<RawMaterialReceipt>();
                string val;
                for (int j = 0; j < grdrawmaterial.Rows.Count; j++)
                {
                    RawMaterialReceipt rmr = new RawMaterialReceipt();
                    if (j == 0)
                    {
                        rmr.record_status = "A";
                        rmr.rawmaterial_receipt_id = Session["receipt_id"].ToString();
                        rmr.remarks = txtapproverremarks.Value;
                        rmr.financial_year = Session["financial_year"].ToString();
                    }
                    GridViewRow row = grdrawmaterial.Rows[j];
                    if ((row.Cells[1].FindControl("txtQuantity") as TextBox).Text.Trim() != "0" || (row.Cells[1].FindControl("txtQuantity") as TextBox).Text.Trim() != "")
                    {
                        rmr.rmstorageid = (row.Cells[1].FindControl("lblstorageid") as Label).Text;
                        rmr.vat_code = (row.Cells[1].FindControl("lblVatCode") as Label).Text;
                        rmr.storedqty = Convert.ToDouble((row.Cells[1].FindControl("txtQuantity") as TextBox).Text);
                        rmr.opening_dips = Convert.ToDouble((row.Cells[1].FindControl("txtdips") as TextBox).Text);
                    }
                    rmr.party_code = party_code.Value;
                    rmr.user_id = Session["UserID"].ToString();
                    if (txtreceiptqty.Text != "")
                        rmr.passqty = Convert.ToDouble(txtreceiptqty.Text);
                    rawmaterial.Add(rmr);

                }
                val = BL_RawMaterialReceipt.Approve(rawmaterial);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("RMR_GrainBasedList");
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
                rawmaterial = new List<RawMaterialReceipt>();
                string val;
                for (int j = 0; j < grdrawmaterial.Rows.Count; j++)
                {

                    RawMaterialReceipt rmr = new RawMaterialReceipt();
                    if (j == 0)
                    {
                        rmr.record_status = "R";

                        rmr.rawmaterial_receipt_id = Session["receipt_id"].ToString();
                        rmr.remarks = txtapproverremarks.Value;
                        rmr.user_id = Session["UserID"].ToString();
                        rmr.financial_year = Session["financial_year"].ToString();
                    }

                    GridViewRow row = grdrawmaterial.Rows[j];
                    if ((row.Cells[1].FindControl("txtQuantity") as TextBox).Text.Trim() != "0" || (row.Cells[1].FindControl("txtQuantity") as TextBox).Text.Trim() != "")
                    {
                        rmr.rmstorageid = (row.Cells[1].FindControl("lblstorageid") as Label).Text;
                        rmr.vat_code = (row.Cells[1].FindControl("lblVatCode") as Label).Text;
                        rmr.storedqty = Convert.ToDouble((row.Cells[1].FindControl("txtQuantity") as TextBox).Text);
                        rmr.opening_dips = Convert.ToDouble((row.Cells[1].FindControl("txtdips") as TextBox).Text);
                    }
                    rawmaterial.Add(rmr);

                }
                val = BL_RawMaterialReceipt.Approve(rawmaterial);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("RMR_GrainBasedList");
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
                GrandTotal();
                //  total = Convert.ToDouble((grdRawMaterial.FooterRow.FindControl("lblTotal") as Label).Text);
                if (grdrawmaterial.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Vats not available \');", true);
                    ddlRawMaterial.ClearSelection();
                }
                else if (Convert.ToDouble(txtreceiptqty.Text) != Convert.ToDouble(Session["total"].ToString()))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Receipt Quantity and Total Quantity must be equal \');", true);
                }
                else if (ddlSupplierName.SelectedValue == "Select" && txtunit.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter unit or select unit name \');", true);
                }
                else
                {
                    rawmaterial = new List<RawMaterialReceipt>();

                    for (int j = 0; j < grdrawmaterial.Rows.Count; j++)
                    {
                        RawMaterialReceipt rmr = new RawMaterialReceipt();
                        if (j == 0)
                        {
                            rmr.grossweight = Convert.ToDouble(txtreceiptqty.Text);
                            // rmr.tankerweight = Convert.ToDouble(txtTankerWeight.Text);
                            //  rmr.transitweight = Convert.ToDouble(txtTransitWastage.Text);
                            //    rmr.supplierweight = Convert.ToDouble(txtSupplierWeight.Text);
                            rmr.netweight = Convert.ToDouble(txtreceiptqty.Text);
                            rmr.rmr_entrydate = txtgpd.Value;
                            rmr.passno = txtReceiptNo.Text;
                            rmr.passqty = Convert.ToDouble(txtreceiptqty.Text);
                            rmr.remarks = txtRemarks1.Value;
                            rmr.rawmaterial = ddlRawMaterial.SelectedValue;
                            rmr.party_code = party_code.Value;
                            rmr.vehicleno = txtVehicleNo.Text;
                            rmr.financial_year = Session["financial_year"].ToString();
                            rmr.passissuedate = receipt.Value;
                            rmr.user_id = Session["UserID"].ToString();
                            if(Session["party_type"].ToString()== "ENA Distillery Unit")
                            {
                                rmr.record_status = "A";
                            }
                            else
                            {
                                rmr.record_status = "Y";
                            }
                            
                            rmr.uom = ddlUom.SelectedValue;
                            if (ddlSupplierType.SelectedValue == "Sugar Mill")
                            {
                                rmr.supplier = ddlSupplierName.SelectedValue;
                                rmr.suppliertype = ddlSupplierType.SelectedItem.ToString();
                                // rmr.suppliername = ddlparty.SelectedItem.ToString();
                            }
                            else
                            {

                                rmr.suppliername = txtunit.Text;
                                rmr.suppliertype = ddlSupplierType.SelectedItem.ToString();
                            }

                            // rmr.rawmaterial_receipt_id = Session["receipt_id"].ToString();
                        }

                        GridViewRow row = grdrawmaterial.Rows[j];
                        if ((row.Cells[1].FindControl("txtQuantity") as TextBox).Text.Trim() != "0" || (row.Cells[1].FindControl("txtQuantity") as TextBox).Text.Trim() != "")
                        {
                            rmr.rmstorageid = (row.Cells[1].FindControl("lblstorageid") as Label).Text;
                            rmr.vat_code = (row.Cells[1].FindControl("lblVatCode") as Label).Text;
                            rmr.storedqty = Convert.ToDouble((row.Cells[1].FindControl("txtQuantity") as TextBox).Text);
                            rmr.opening_dips = Convert.ToDouble((row.Cells[1].FindControl("txtdips") as TextBox).Text);
                            rmr.user_id = Session["UserID"].ToString();

                        }
                        if (Session["rtype"].ToString() != "0")
                        {
                            rmr.rawmaterial_receipt_id = Session["receipt_id"].ToString();
                        }

                        rawmaterial.Add(rmr);
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter a quantity greater than 0..  \');", true);
                        //}

                    }
                    string val;
                    if (Session["rtype"].ToString() == "0")
                        val = BL_RawMaterialReceipt.InsertRawmaterialGrain(rawmaterial).ToString();
                    else
                    {

                        val = BL_RawMaterialReceipt.UpdateGrain(rawmaterial).ToString();
                    }
                    if (val == "0")
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("RMR_GrainBasedList");
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
        protected void btnSaveAs_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                GrandTotal();
                //Session["total"] = (grdRawMaterial.FooterRow.Cells[i].FindControl("lblTotal") as Label).Text;
                //total = Convert.ToDouble((grdRawMaterial.FooterRow.FindControl("lblTotal") as Label).Text);
                if (grdrawmaterial.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Vats not available \');", true);
                    ddlRawMaterial.ClearSelection();
                }
                else if (Convert.ToDouble(txtreceiptqty.Text) != Convert.ToDouble(Session["total"].ToString()))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Receipt Quantity and Total Quantity must be equal \');", true);
                }
                else if (ddlSupplierName.SelectedValue == "Select" && txtunit.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter unit or select unit name \');", true);
                }
                else
                {

                    rawmaterial = new List<RawMaterialReceipt>();

                    for (int j = 0; j < grdrawmaterial.Rows.Count; j++)
                    {
                        RawMaterialReceipt rmr = new RawMaterialReceipt();
                        if (j == 0)
                        {
                            rmr.grossweight = Convert.ToDouble(txtreceiptqty.Text);
                            // rmr.tankerweight = Convert.ToDouble(txtTankerWeight.Text);
                            //  rmr.transitweight = Convert.ToDouble(txtTransitWastage.Text);
                            //    rmr.supplierweight = Convert.ToDouble(txtSupplierWeight.Text);
                            rmr.netweight = Convert.ToDouble(txtreceiptqty.Text);
                            rmr.rmr_entrydate = txtgpd.Value;
                            rmr.passno = txtReceiptNo.Text;
                            rmr.passqty = Convert.ToDouble(txtreceiptqty.Text);
                            rmr.remarks = txtRemarks1.Value;
                            rmr.rawmaterial = ddlRawMaterial.SelectedValue;
                            rmr.party_code = party_code.Value;
                            rmr.vehicleno = txtVehicleNo.Text;
                            rmr.passissuedate = receipt.Value;
                            rmr.financial_year = Session["financial_year"].ToString();
                            rmr.user_id = Session["UserID"].ToString();
                            rmr.record_status = "N";
                            rmr.uom = ddlUom.SelectedValue;
                            if (ddlSupplierType.SelectedValue == "Sugar Mill")
                            {
                                rmr.supplier = ddlSupplierName.SelectedValue;
                                rmr.suppliertype = ddlSupplierType.SelectedItem.ToString();
                                // rmr.suppliername = ddlparty.SelectedItem.ToString();
                            }
                            else
                            {

                                rmr.suppliername = txtunit.Text;
                                rmr.suppliertype = ddlSupplierType.SelectedItem.ToString();
                            }

                            // rmr.rawmaterial_receipt_id = Session["receipt_id"].ToString();
                        }
                        //rmr.store = new List<RMR_Store>();
                        //RMR_Store rmr1 = new RMR_Store();
                        GridViewRow row = grdrawmaterial.Rows[j];
                        if ((row.Cells[1].FindControl("txtQuantity") as TextBox).Text.Trim() != "0" || (row.Cells[1].FindControl("txtQuantity") as TextBox).Text.Trim() != "")
                        {

                            rmr.rmstorageid = (row.Cells[1].FindControl("lblstorageid") as Label).Text;
                            rmr.vat_code = (row.Cells[1].FindControl("lblVatCode") as Label).Text;
                            rmr.storedqty = Convert.ToDouble((row.Cells[1].FindControl("txtQuantity") as TextBox).Text);
                            rmr.opening_dips = Convert.ToDouble((row.Cells[1].FindControl("txtdips") as TextBox).Text);
                            rmr.user_id = Session["UserID"].ToString();

                        }
                        if (Session["rtype"].ToString() != "0")
                        {
                            rmr.rawmaterial_receipt_id = Session["receipt_id"].ToString();
                        }
                        rawmaterial.Add(rmr);

                    }
                    string val;
                    if (Session["rtype"].ToString() == "0")
                        val = BL_RawMaterialReceipt.InsertRawmaterialGrain(rawmaterial).ToString();
                    else
                    {
                        //    rmr.rawmaterial_receipt_id =Session["receipt_id"].ToString();
                        val = BL_RawMaterialReceipt.UpdateGrain(rawmaterial).ToString();
                    }
                    if (val == "0")
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("RMR_GrainBasedList");
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
        private void GrandTotal()
        {
            double GTotal = 0f;
            for (int i = 0; i < grdrawmaterial.Rows.Count; i++)
            {
                String total = (grdrawmaterial.Rows[i].FindControl("txtQuantity") as TextBox).Text;
                GTotal += Convert.ToDouble(total);
            }
            Session["total"] = GTotal.ToString();
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                GrandTotal();
                //  total = Convert.ToDouble((grdRawMaterial.FooterRow.FindControl("lblTotal") as Label).Text);
                if (grdrawmaterial.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Vats not available \');", true);
                    ddlRawMaterial.ClearSelection();
                }
                else if (Convert.ToDouble(txtreceiptqty.Text) != Convert.ToDouble(Session["total"].ToString()))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Receipt Quantity and Total Quantity must be equal \');", true);
                }
                else if (ddlSupplierName.SelectedValue == "Select" && txtunit.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter unit or select unit name \');", true);
                }
                else
                {
                    rawmaterial = new List<RawMaterialReceipt>();

                    for (int j = 0; j < grdrawmaterial.Rows.Count; j++)
                    {
                        RawMaterialReceipt rmr = new RawMaterialReceipt();
                        if (j == 0)
                        {
                            rmr.grossweight = Convert.ToDouble(txtreceiptqty.Text);
                            // rmr.tankerweight = Convert.ToDouble(txtTankerWeight.Text);
                            //  rmr.transitweight = Convert.ToDouble(txtTransitWastage.Text);
                            //    rmr.supplierweight = Convert.ToDouble(txtSupplierWeight.Text);
                            rmr.netweight = Convert.ToDouble(txtreceiptqty.Text);
                            rmr.rmr_entrydate = txtgpd.Value;
                            rmr.passno = txtReceiptNo.Text;
                            rmr.passqty = Convert.ToDouble(txtreceiptqty.Text);
                            rmr.remarks = txtRemarks1.Value;
                            rmr.financial_year = Session["rgbfinancial_year"].ToString();
                            rmr.rawmaterial = ddlRawMaterial.SelectedValue;
                            rmr.party_code = party_code.Value;
                            rmr.vehicleno = txtVehicleNo.Text;
                            rmr.passissuedate = receipt.Value;
                            rmr.user_id = Session["UserID"].ToString();
                            if (Session["party_type"].ToString() == "ENA Distillery Unit")
                            {
                                rmr.record_status = "A";
                            }
                            else
                            {
                                rmr.record_status = "Y";
                            }

                            rmr.uom = ddlUom.SelectedValue;
                            if (ddlSupplierType.SelectedValue == "Sugar Mill")
                            {
                                rmr.supplier = ddlSupplierName.SelectedValue;
                                rmr.suppliertype = ddlSupplierType.SelectedItem.ToString();
                                // rmr.suppliername = ddlparty.SelectedItem.ToString();
                            }
                            else
                            {
                                rmr.suppliername = txtunit.Text;
                                rmr.suppliertype = ddlSupplierType.SelectedItem.ToString();
                            }
                            // rmr.rawmaterial_receipt_id = Session["receipt_id"].ToString();
                        }

                        GridViewRow row = grdrawmaterial.Rows[j];
                        if ((row.Cells[1].FindControl("txtQuantity") as TextBox).Text.Trim() != "0" || (row.Cells[1].FindControl("txtQuantity") as TextBox).Text.Trim() != "")
                        {
                            rmr.rmstorageid = (row.Cells[1].FindControl("lblstorageid") as Label).Text;
                            rmr.vat_code = (row.Cells[1].FindControl("lblVatCode") as Label).Text;
                            rmr.storedqty = Convert.ToDouble((row.Cells[1].FindControl("txtQuantity") as TextBox).Text);
                            rmr.opening_dips = Convert.ToDouble((row.Cells[1].FindControl("txtdips") as TextBox).Text);
                            rmr.user_id = Session["UserID"].ToString();

                        }
                        if (Session["rtype"].ToString() != "0")
                        {
                            rmr.rawmaterial_receipt_id = Session["receipt_id"].ToString();
                        }

                        rawmaterial.Add(rmr);
                    }
                    string val;
                   
                        val = BL_RawMaterialReceipt.AdminUpdateGrain(rawmaterial).ToString();
                    if (val == "0")
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("RMR_GrainBasedList");
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
    }
}