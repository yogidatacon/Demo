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
    public partial class RMR_GrainBase : System.Web.UI.Page
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
                CalendarExtender2.EndDate = DateTime.Now;
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                   
                    UserDetails user = new UserDetails();
                    List<RawMaterial> raw = new List<RawMaterial>();
                    raw = BL_RawMaterial.GetRawMaterialList("");
                    ddtypeofmeterial.DataSource = raw;
                    ddtypeofmeterial.DataTextField = "rawmaterial_name";
                    ddtypeofmeterial.DataValueField = "product_type_code";
                    ddtypeofmeterial.DataBind();
                    ddtypeofmeterial.Items.Insert(0, "Select");
                    List<UOM_Master> uom = new List<UOM_Master>();
                    uom =BL_UOM.GetList("");
                    ddlUOM.DataSource = uom;
                    ddlUOM.DataTextField = "uom_name";
                    ddlUOM.DataValueField = "uom_code";
                    ddlUOM.DataBind();
                    ddlUOM.Items.Insert(0, "Select");
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    party_code.Value = user.party_code;
                    Session["district_code"] = user.district_code;
                    txtDATE.ReadOnly = true;
                    txtRecieptDate.ReadOnly = true;
                    // rtype.Value =Session["rtype"];
                    if (Session["UserID"].ToString() == "Admin")
                    {
                        btnSaveAs.Visible = true;
                        btnSubmit.Visible = true;
                        btnCancel.Visible = true;
                        btnApprove.Visible = true;
                        btnReject.Visible = true;
                        btnSaveAs.Visible = true;
                        btnSubmit.Visible = true;
                        btnCancel.Visible = true;
                    }
                    else if (user.role_name == "Bond Officer")
                    {
                        btnSaveAs.Visible = false;
                        btnSubmit.Visible = false;
                        btnCancel.Visible = false;
                        btnApprove.Visible = true;
                        btnReject.Visible = true;
                    }
                    else
                    {
                        btnSaveAs.Visible = true;
                        btnSubmit.Visible = true;
                        btnCancel.Visible = true;
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        approvalremarks.Visible = false;
                        txtApproverremarks.Visible = false;
                    }
                    hdtotal.Value = Session["rtype"].ToString();
                    ddlSupplierType.SelectedIndex = 1;
                    if (ddlSupplierType.SelectedValue == "Sugar Mill")
                    {
                        List<Party_Master> party = new List<Party_Master>();
                        party = BL_Party_Master.GetList();
                        var list = from s in party
                                   where s.party_type_code == "SGR"
                                   select s;
                        ddlparty.DataSource = list.ToList();
                        ddlparty.DataTextField = "party_name";
                        ddlparty.DataValueField = "party_code";
                        ddlparty.DataBind();
                        ddlparty.Items.Insert(0, "Select");
                        ddp.Visible = true;
                        txtp.Visible = false;
                        ddp.Visible = true;
                        ddlparty.Visible = true;
                    }
                    if (Session["rtype"].ToString() != "0")
                    {

                        RawMaterialReceipt rawmaterials = new RawMaterialReceipt();
                        rawmaterials = BL_RawMaterialReceipt.GetGainList(Convert.ToInt32(Session["receipt_id"].ToString()), Session["rgbfinancial_year"].ToString());
                        txtDATE.Text = rawmaterials.rmr_entrydate;
                       // txtGrossWeight.Text = rawmaterials.grossweight.ToString();
                       // txtTankerWeight.Text = rawmaterials.tankerweight.ToString();
                        txtPassNo.Text = rawmaterials.rmrpassno;
                        party_code.Value = rawmaterials.party_code;
                       // txtUnitName.Text = rawmaterials.party_name;
                        txtdob.Value = rawmaterials.rmr_entrydate;
                        CalendarExtender.SelectedDate = Convert.ToDateTime(rawmaterials.rmr_entrydate);
                        ddlSupplierType.SelectedValue = rawmaterials.suppliertype;
                        if (ddlSupplierType.SelectedValue == "Sugar Mill")
                        {
                            ddlparty.SelectedValue = rawmaterials.supplier;
                            ddlparty.Visible = true;
                            ddp.Visible = true;
                            txtp.Visible = false;
                        }
                        else
                        {
                            txtfromparty.Text=rawmaterials.suppliername;
                            txtp.Visible = true;
                            txtfromparty.Visible = true;
                            ddp.Visible = false;
                            ddlSupplierType.SelectedValue = rawmaterials.suppliertype;
                        }

                        // rmr.rawmaterial_receipt_id = Session["receipt_id"].ToString();

                        ddlUOM.SelectedValue = rawmaterials.uom;
                        ddtypeofmeterial.SelectedValue = rawmaterials.rawmaterial;
                        reciept.Value= rawmaterials.passissuedate.Substring(0, 10).Replace("/", "-");
                        CalendarExtender2.SelectedDate = Convert.ToDateTime(rawmaterials.passissuedate);
                        txtOtyDispatch.Text = rawmaterials.passqty.ToString();
                        Session["total"] = rawmaterials.passqty;
                       // hdtotal.Value= rawmaterials.passqty.ToString();
                        txtRecieptDate.Text = rawmaterials.passissuedate.Substring(0, 10).Replace("/", "-");
                        if (rawmaterials.rmr_entrydate != null)
                            CalendarExtender.SelectedDate = Convert.ToDateTime(rawmaterials.rmr_entrydate);
               
                        txtVehicleNo.Text = rawmaterials.vehicleno;
                        //  txtSupplierWeight.Text= rawmaterials.passqty.ToString();
                      //  txtNetWeight.Text = (Convert.ToDouble(txtGrossWeight.Text) - Convert.ToDouble(txtTankerWeight.Text)).ToString();
                      //  txtTransitWastage.Text = (Convert.ToDouble(txtOtyDispatch.Text) - Convert.ToDouble(txtNetWeight.Text)).ToString();
                        txtRemarks.Text = rawmaterials.remarks;
                       // NetWeight.Value = rawmaterials.netweight.ToString();
                        rawmaterials.rawmaterial_receipt_id = Session["receipt_id"].ToString();
                        txtRemarks.Text = rawmaterials.remarks;
                        List<RawMaterialReceipt> vats = new List<RawMaterialReceipt>();
                        vats = BL_RawMaterialReceipt.GetGrainvatsList(user.party_code, Session["receipt_id"].ToString().ToString(), rawmaterials.rawmaterial, Session["rgbfinancial_year"].ToString());
                        if (Session["UserID"].ToString() == "Admin")
                        {
                            grdRawMaterial.DataSource = vats;
                            grdRawMaterial.DataBind();
                        }
                        else
                        {

                            var list = from s in vats
                                       where s.rawmaterial_receipt_id == Session["receipt_id"].ToString()
                                       select s;

                            grdRawMaterial.DataSource = list.ToList();
                            grdRawMaterial.DataBind();

                            foreach (GridViewRow dr1 in grdRawMaterial.Rows)
                            {
                                TextBox txt = dr1.FindControl("txtQuantity") as TextBox;
                                if (txtdob.Value == "")
                                {
                                    txt.Text = "0";
                                }
                            }
                        }
                        int receipt_id = Convert.ToInt32(Session["receipt_id"].ToString());
                        List<All_Approvals> approvals = new List<All_Approvals>();
                        approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), rawmaterials.rawmaterial_receipt_id.ToString(), "RMR");
                        grdApprovalDetails.DataSource = approvals;
                        grdApprovalDetails.DataBind();
                        approvalremarks.Visible = false;
                        if (approvals.Count <= 0)
                        {
                            approv.Visible = false;
                            approvalremarks.Visible = false;
                        }
                        if (Session["rtype"].ToString() == "1")
                        {
                            txtDATE.ReadOnly = true;
                           // txtGrossWeight.Enabled = false;
                            //   txtSupplierWeight.Enabled = false;
                          //  txtNetWeight.Enabled = false;
                           // txtNetWeight.Enabled = false;
                            txtPassNo.Enabled = false;
                            //  txtTankerWeight.Enabled = false;
                            //  txtTransitWastage.Enabled = false;
                            ddlparty.Enabled = false;
                            ddtypeofmeterial.Enabled = false;
                            ddlSupplierType.Enabled = false;
                            ddlUOM.Enabled = false;
                            txtRemarks.Enabled = false;
                            txtOtyDispatch.Enabled = false;
                            txtfromparty.Enabled = false;
                            txtVehicleNo.Enabled = false;
                          //  txtApproverremarks.Enabled = false;
                            txtRemarks.Enabled = false;
                            btnCancel.Visible = false;
                            btnSaveAs.Visible = false;
                            btnSubmit.Visible = false;
                            grdRawMaterial.Enabled = false;
                            btnApprove.Visible = false;
                            btnReject.Visible = false;
                            Image1.Visible = false;
                            txtDATE.Enabled = false;
                            if (user.role_name == "Bond Officer" && rawmaterials.record_status == "Y")
                            {
                                grdRawMaterial.Enabled = false;
                                btnReject.Visible = true;
                                btnApprove.Visible = true;
                                approvalremarks.Visible = true;
                            }

                        }
                        else
                        {
                            foreach (GridViewRow g1 in grdRawMaterial.Rows)
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
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                rawmaterial = new List<RawMaterialReceipt>();
                string val;
                for (int j = 0; j < grdRawMaterial.Rows.Count; j++)
                {
                    RawMaterialReceipt rmr = new RawMaterialReceipt();
                    if (j == 0)
                    {
                        rmr.record_status = "A";
                        rmr.rawmaterial_receipt_id = Session["receipt_id"].ToString();
                        rmr.remarks = txtApproverremarks.Text;
                    }
                    GridViewRow row = grdRawMaterial.Rows[j];
                    if ((row.Cells[1].FindControl("txtQuantity") as TextBox).Text.Trim() != "0" || (row.Cells[1].FindControl("txtQuantity") as TextBox).Text.Trim() != "")
                    {
                        rmr.rmstorageid = (row.Cells[1].FindControl("lblstorageid") as Label).Text;
                        rmr.vat_code = (row.Cells[1].FindControl("lblVatCode") as Label).Text;
                        rmr.storedqty = Convert.ToDouble((row.Cells[1].FindControl("txtQuantity") as TextBox).Text);
                        rmr.opening_dips = Convert.ToDouble((row.Cells[1].FindControl("txtdips") as TextBox).Text);
                    }
                    rmr.party_code = party_code.Value;
                    rmr.user_id = Session["UserID"].ToString();
                    if (txtOtyDispatch.Text != "")
                        rmr.passqty = Convert.ToDouble(txtOtyDispatch.Text);
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
                for (int j = 0; j < grdRawMaterial.Rows.Count; j++)
                {

                    RawMaterialReceipt rmr = new RawMaterialReceipt();
                    if (j == 0)
                    {
                        rmr.record_status = "R";

                        rmr.rawmaterial_receipt_id = Session["receipt_id"].ToString();
                        rmr.remarks = txtApproverremarks.Text;
                        rmr.user_id = Session["UserID"].ToString();
                    }

                    GridViewRow row = grdRawMaterial.Rows[j];
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
                if (grdRawMaterial.Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Vats not available \');", true);
                        ddtypeofmeterial.ClearSelection();
                    }
                   else if (Convert.ToDouble(txtOtyDispatch.Text) != Convert.ToDouble(Session["total"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Receipt Quantity and Total Quantity must be equal \');", true);
                    }
                else if(ddlparty.SelectedValue =="Select" && txtfromparty.Text =="")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter unit or select unit name \');", true);
                }
                    else
                    {
                         rawmaterial = new List<RawMaterialReceipt>();
                  
                    for (int j = 0; j < grdRawMaterial.Rows.Count; j++)
                        {
                        RawMaterialReceipt rmr = new RawMaterialReceipt();
                        if (j == 0)
                            {
                                rmr.grossweight = Convert.ToDouble(txtOtyDispatch.Text);
                                // rmr.tankerweight = Convert.ToDouble(txtTankerWeight.Text);
                                //  rmr.transitweight = Convert.ToDouble(txtTransitWastage.Text);
                                //    rmr.supplierweight = Convert.ToDouble(txtSupplierWeight.Text);
                                rmr.netweight = Convert.ToDouble(txtOtyDispatch.Text);
                                rmr.rmr_entrydate = txtdob.Value;
                                rmr.passno = txtPassNo.Text;
                                rmr.passqty = Convert.ToDouble(txtOtyDispatch.Text);
                                rmr.remarks = txtRemarks.Text;
                                rmr.rawmaterial = ddtypeofmeterial.SelectedValue;
                                rmr.party_code = party_code.Value;
                                rmr.vehicleno = txtVehicleNo.Text;
                                rmr.passissuedate = reciept.Value;
                                rmr.user_id = Session["UserID"].ToString();
                                rmr.record_status = "Y";
                                rmr.uom = ddlUOM.SelectedValue;
                                if (ddlSupplierType.SelectedValue == "Sugar Mill")
                                {
                                    rmr.supplier = ddlparty.SelectedValue;
                                    rmr.suppliertype = ddlSupplierType.SelectedItem.ToString();
                                   // rmr.suppliername = ddlparty.SelectedItem.ToString();
                                }
                                else
                                {
                                
                                   rmr.suppliername = txtfromparty.Text;
                                    rmr.suppliertype = ddlSupplierType.SelectedItem.ToString();
                                }

                                // rmr.rawmaterial_receipt_id = Session["receipt_id"].ToString();
                            }

                            GridViewRow row = grdRawMaterial.Rows[j];
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
                if (grdRawMaterial.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Vats not available \');", true);
                    ddtypeofmeterial.ClearSelection();
                }
                else if (Convert.ToDouble(txtOtyDispatch.Text) != Convert.ToDouble(Session["total"].ToString()))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Receipt Quantity and Total Quantity must be equal \');", true);
                }
                else if (ddlparty.SelectedValue == "Select" && txtfromparty.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter unit or select unit name \');", true);
                }
                else 
                {

                    rawmaterial = new List<RawMaterialReceipt>();
                 
                    for (int j = 0; j < grdRawMaterial.Rows.Count; j++)
                    {
                        RawMaterialReceipt rmr = new RawMaterialReceipt();
                        if (j == 0)
                        {
                            rmr.grossweight = Convert.ToDouble(txtOtyDispatch.Text);
                            // rmr.tankerweight = Convert.ToDouble(txtTankerWeight.Text);
                            //  rmr.transitweight = Convert.ToDouble(txtTransitWastage.Text);
                            //    rmr.supplierweight = Convert.ToDouble(txtSupplierWeight.Text);
                            rmr.netweight = Convert.ToDouble(txtOtyDispatch.Text);
                            rmr.rmr_entrydate = txtdob.Value;
                            rmr.passno = txtPassNo.Text;
                            rmr.passqty = Convert.ToDouble(txtOtyDispatch.Text);
                            rmr.remarks = txtRemarks.Text;
                            rmr.rawmaterial = ddtypeofmeterial.SelectedValue;
                            rmr.party_code = party_code.Value;
                            rmr.vehicleno = txtVehicleNo.Text;
                            rmr.passissuedate = reciept.Value;
                            rmr.user_id = Session["UserID"].ToString();
                            rmr.record_status = "N";
                            rmr.uom = ddlUOM.SelectedValue;
                            if (ddlSupplierType.SelectedValue == "Sugar Mill")
                            {
                                rmr.supplier = ddlparty.SelectedValue;
                                rmr.suppliertype = ddlSupplierType.SelectedItem.ToString();
                               // rmr.suppliername = ddlparty.SelectedItem.ToString();
                            }
                            else
                            {
                              
                                rmr.suppliername = txtfromparty.Text;
                                rmr.suppliertype = ddlSupplierType.SelectedItem.ToString();
                            }

                            // rmr.rawmaterial_receipt_id = Session["receipt_id"].ToString();
                        }
                        //rmr.store = new List<RMR_Store>();
                        //RMR_Store rmr1 = new RMR_Store();
                        GridViewRow row = grdRawMaterial.Rows[j];
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

        protected void ddlSupplierType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (ddlSupplierType.SelectedValue == "Sugar Mill")
                {
                    List<Party_Master> party = new List<Party_Master>();
                    party = BL_Party_Master.GetList();
                    var list = from s in party
                               where s.party_type_code =="SGR" 
                               select s;
                    ddlparty.DataSource = list.ToList();
                    ddlparty.DataTextField = "party_name";
                    ddlparty.DataValueField = "party_code";
                    ddlparty.DataBind();
                    ddlparty.Items.Insert(0, "Select");
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

        protected void ddtypeofmeterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
              
                if (ddtypeofmeterial.SelectedValue != "Select" && ddtypeofmeterial.SelectedValue != "")
                {
                    if (Session["rtype"].ToString() == "0")
                    {
                        List<RawMaterialReceipt> vats = new List<RawMaterialReceipt>();
                        vats = BL_RawMaterialReceipt.GetvatsList1(party_code.Value, "0", ddtypeofmeterial.SelectedValue);
                        var list = from s in vats
                                   where s.product_type_code == ddtypeofmeterial.SelectedValue
                                   select s;
                        grdRawMaterial.DataSource = vats;
                        grdRawMaterial.DataBind();
                    }
                    else
                    {
                        List<RawMaterialReceipt> vats = new List<RawMaterialReceipt>();
                        vats = BL_RawMaterialReceipt.GetGrainvatsList(party_code.Value, Session["receipt_id"].ToString(), ddtypeofmeterial.SelectedValue, Session["rgbfinancial_year"].ToString());
                        var list = from s in vats
                                   where s.product_type_code == ddtypeofmeterial.SelectedValue
                                   select s;
                        grdRawMaterial.DataSource = vats;
                        grdRawMaterial.DataBind();
                    }
                }
            }
        }

        private void GrandTotal()
        {
           double GTotal = 0f;
            for (int i = 0; i < grdRawMaterial.Rows.Count; i++)
            {
                String total = (grdRawMaterial.Rows[i].FindControl("txtQuantity") as TextBox).Text;
                GTotal += Convert.ToSingle(total);
            }
            Session["total"] = GTotal.ToString();
        }

        protected void ddtypeofmeterial_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (IsPostBack)
            { 
           
            if (ddtypeofmeterial.SelectedValue != "Select" && ddtypeofmeterial.SelectedValue != "")
            {
                if (Session["rtype"].ToString() == "0")
                {
                    List<RawMaterialReceipt> vats = new List<RawMaterialReceipt>();
                    vats = BL_RawMaterialReceipt.GetvatsList1(party_code.Value, "0", ddtypeofmeterial.SelectedValue);
                    var list = from s in vats
                               where s.product_type_code == ddtypeofmeterial.SelectedValue
                               select s;
                    grdRawMaterial.DataSource = vats;
                    grdRawMaterial.DataBind();
                }
                else
                {
                    List<RawMaterialReceipt> vats = new List<RawMaterialReceipt>();
                    vats = BL_RawMaterialReceipt.GetGrainvatsList(party_code.Value, Session["receipt_id"].ToString(), ddtypeofmeterial.SelectedValue, Session["rgbfinancial_year"].ToString());
                    var list = from s in vats
                               where s.product_type_code == ddtypeofmeterial.SelectedValue
                               select s;
                    grdRawMaterial.DataSource = vats;
                    grdRawMaterial.DataBind();
                }
            }
            }
        }
    }
}