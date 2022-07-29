using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class RawmaterialReceiptForm : System.Web.UI.Page
    {
        List<RawMaterialReceipt> rawmaterial = new List<RawMaterialReceipt>();
        List<VAT_Master> vat = new List<VAT_Master>();
        RawMaterialReceipt rawmaterials = new RawMaterialReceipt();
      
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];

                    // txtOtyDispatch.Text = "100";
                    //   string userid = Session["UserID"].ToString();
                    UserDetails user = new UserDetails();
                    txtDATE.ReadOnly = true;
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    party_code.Value = party_code.Value;
                    Session["party_code"] = user.party_type_code;
                    Session["financial_year"] = user.financial_year;
                    if (user.party_type == "M & tP"|| user.party_code == "MTP" || user.party_code == "MTR" || user.party_code == "MTW")
                    {
                        MTP.Visible = true;
                        SCM.Visible = false;
                        if(user.party_code == "MTR" || user.party_code == "MTW")
                        {
                            btnConsumption.Visible = false;
                        }
                    }
                    else
                    {
                        MTP.Visible = false;
                        SCM.Visible = true;
                    }
                    // rtype.Value =Session["rtype"];
                    if (Session["UserID"].ToString() == "Admin")
                    {
                        btnSaveAs.Visible = false;
                        btnSubmit.Visible = false;
                        btnCancel.Visible = true;
                        btnApprove.Visible =false;
                        btnReject.Visible = false;
                        txtRemarks.ReadOnly = true;
                        btnupdate.Visible = true;


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
                    }

                    if (Session["rtype"].ToString() != "0")
                    {

                        RawMaterialReceipt rawmaterials = new RawMaterialReceipt();
                        rawmaterials = BL_RawMaterialReceipt.GetList(Convert.ToInt32(Session["receipt_id"].ToString()),Session["rmrfinancial_year"].ToString());
                        txtDATE.Text = rawmaterials.rmr_entrydate;
                        txtGrossWeight.Text = rawmaterials.grossweight.ToString();
                        txtTankerWeight.Text = rawmaterials.tankerweight.ToString();
                        txtPassNo.Text = rawmaterials.passno;
                        party_code.Value = rawmaterials.party_code;
                        txtUnitName.Text = rawmaterials.party_name;
                        txtdob.Value = rawmaterials.rmr_entrydate;
                  
                        txtOtyDispatch.Text = rawmaterials.passqty.ToString();
                        txtDateOfIssue.Text = rawmaterials.passissuedate.Substring(0, 10).Replace("/", "-");
                        CalendarExtender.StartDate = Convert.ToDateTime( rawmaterials.passissuedate);
                        if (rawmaterials.rmr_entrydate!=null)
                        CalendarExtender.SelectedDate = Convert.ToDateTime(rawmaterials.rmr_entrydate);
                        txtVehicleNo.Text = rawmaterials.vehicleno;
                        //  txtSupplierWeight.Text= rawmaterials.passqty.ToString();
                        txtNetWeight.Text = (Convert.ToDouble(txtGrossWeight.Text) - Convert.ToDouble(txtTankerWeight.Text)).ToString();
                        txtTransitWastage.Text = (Convert.ToDouble(txtOtyDispatch.Text) - Convert.ToDouble(txtNetWeight.Text)).ToString();
                        txtRemarks.Text = rawmaterials.remarks;
                        NetWeight.Value = rawmaterials.netweight.ToString();
                        rawmaterials.rawmaterial_receipt_id = Session["receipt_id"].ToString();
                        txtRemarks.Text = rawmaterials.remarks;
                        List<RawMaterialReceipt> vats = new List<RawMaterialReceipt>();
                        vats = BL_RawMaterialReceipt.GetvatsList(user.party_code, Session["receipt_id"].ToString().ToString(), Session["rmrfinancial_year"].ToString());
                        List<All_Approvals> approvals = new List<All_Approvals>();
                        approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), rawmaterials.rawmaterial_receipt_id.ToString(), "RMR");
                        if (Session["UserID"].ToString() == "Admin")
                        {
                            grdRawMaterial.DataSource = vats;
                            grdRawMaterial.DataBind();

                          
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
                           
                                grdRawMaterial.DataSource = list.ToList();
                                grdRawMaterial.DataBind();
                            
                            foreach(GridViewRow dr1 in grdRawMaterial.Rows)
                            {
                                TextBox txt = dr1.FindControl("txtQuantity") as TextBox;
                                if(txtdob.Value=="")
                                {
                                    txt.Text = "0";
                                }
                            }

                            var list4 = (from s in approvals
                                         where s.financial_year == Session["rmrfinancial_year"].ToString()
                                         select s);
                            grdApprovalDetails.DataSource = list4.ToList();
                            grdApprovalDetails.DataBind();
                        }
                        int receipt_id = Convert.ToInt32(Session["receipt_id"].ToString());
                    
                        approvalremarks.Visible = false;
                        if (approvals.Count <= 0)
                        {
                            approv.Visible = false;
                            approvalremarks.Visible = false;
                        }
                        if (Session["rtype"].ToString() == "1")
                        {
                            txtDATE.ReadOnly = true;
                            txtGrossWeight.Enabled = false;
                            //   txtSupplierWeight.Enabled = false;
                            txtNetWeight.Enabled = false;
                            txtNetWeight.Enabled = false;
                            txtPassNo.Enabled = false;
                            txtTankerWeight.Enabled = false;
                            txtTransitWastage.Enabled = false;
                            txtRemarks.Enabled = false;
                            btnupdate.Visible = false;
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
                    //else
                    //{
                    //    // vat = new List<VAT_Master>();
                    //    // vat = BL_RawMaterialReceipt.GetVatName(user.party_code);
                    //    //// grdRawMaterial.DataSource=
                    //    List<RawMaterialReceipt> vats = new List<RawMaterialReceipt>();
                    //    vats = BL_RawMaterialReceipt.GetvatsList(user.party_code.ToString());
                    //    grdRawMaterial.DataSource = vats;
                    //    grdRawMaterial.DataBind();
                    //}
                }
                if (strPreviousPage == "" || System.Web.HttpContext.Current.Session["UserID"] == null)
                {
                    Response.Redirect("~/LoginPage");
                }
            }
        }
        protected void btnIssue_Click(object sender, EventArgs e)
        {
            if(Session["party_code"].ToString()=="MTR" || Session["party_code"].ToString() == "MTW")
            {
                Response.Redirect("MNTW_IssueList.aspx");
            }
            else
            {
                Response.Redirect("MNT_IssueList.aspx");
            }
            
           
          
        }

        protected void btnConsumption_Click(object sender, EventArgs e)
        {
            Response.Redirect("MNT_ConsumptionList.aspx");
        }

        protected void lnkRawMaterialWastage_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawmaterialWastageList.aspx");
        }

        protected void lnkOB_Click(object sender, EventArgs e)
        {
            Response.Redirect("OpeningBalance.aspx");
        }
        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialReceiptList");
        }

        protected void btnSaveAs_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
             //   string userid = Session["UserID"].ToString();
                 rawmaterial = new List<RawMaterialReceipt>();
               
                for (int j = 0; j <grdRawMaterial.Rows.Count; j++)
                {
                    RawMaterialReceipt rmr = new RawMaterialReceipt();
                    if (j == 0)
                    {
                        rmr.grossweight = Convert.ToDouble(txtGrossWeight.Text);
                        rmr.tankerweight = Convert.ToDouble(txtTankerWeight.Text);
                        rmr.transitweight = (Convert.ToDouble(txtOtyDispatch.Text) - Convert.ToDouble(NetWeight.Value));
                        //    rmr.supplierweight = Convert.ToDouble(txtSupplierWeight.Text);
                        rmr.netweight = Convert.ToDouble(NetWeight.Value);
                        rmr.rmr_entrydate = txtdob.Value;
                        rmr.remarks = txtRemarks.Text;
                        rmr.financial_year = Session["financial_year"].ToString();
                        rmr.user_id = Session["UserID"].ToString();
                        rmr.record_status = "N";
                        rmr.rawmaterial_receipt_id = Session["receipt_id"].ToString();
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
                    rawmaterial.Add(rmr);

                }
                    string val;
                if (rawmaterial[0].rmstorageid == "")
                    val = BL_RawMaterialReceipt.InsertRawmaterial(rawmaterial).ToString();
                else
                {
                    
                    val = BL_RawMaterialReceipt.Update(rawmaterial).ToString();
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("RawMaterialReceiptList");
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
                rawmaterial = new List<RawMaterialReceipt>();

                for (int j = 0; j < grdRawMaterial.Rows.Count; j++)
                {
                    RawMaterialReceipt rmr = new RawMaterialReceipt();
                    if (j == 0)
                    {
                        rmr.grossweight = Convert.ToDouble(txtGrossWeight.Text);
                        rmr.tankerweight = Convert.ToDouble(txtTankerWeight.Text);
                        rmr.transitweight = (Convert.ToDouble(txtOtyDispatch.Text) - Convert.ToDouble(NetWeight.Value));
                        //  rmr.supplierweight = Convert.ToDouble(txtSupplierWeight.Text);
                        rmr.netweight = Convert.ToDouble(NetWeight.Value);
                        rmr.rmr_entrydate =txtdob.Value;
                        rmr.remarks = txtRemarks.Text;
                        rmr.user_id = Session["UserID"].ToString();
                        rmr.financial_year = Session["financial_year"].ToString();
                        rmr.record_status = "Y";
                        rmr.rawmaterial_receipt_id = Session["receipt_id"].ToString();
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
                    rawmaterial.Add(rmr);

                }
                string val;
                if (Session["rtype"].ToString() == "0")
                    val = BL_RawMaterialReceipt.InsertRawmaterial(rawmaterial).ToString();
                else
                {
                   
                    val = BL_RawMaterialReceipt.Update(rawmaterial).ToString();
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("RawMaterialReceiptList");
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
                        rmr.financial_year = Session["financial_year"].ToString();
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
                    Response.Redirect("RawMaterialReceiptList");
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
                rawmaterial = new List<RawMaterialReceipt>();
                string val;
                for (int j = 0; j < grdRawMaterial.Rows.Count; j++)
                {
                    RawMaterialReceipt rmr = new RawMaterialReceipt();
                    if (j == 0)
                    {
                        rmr.record_status = "A";
                        rmr.rawmaterial_receipt_id = Session["receipt_id"].ToString();
                        rmr.remarks =txtApproverremarks.Text;
                        rmr.financial_year = Session["financial_year"].ToString();
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
                    if(txtOtyDispatch.Text!="")
                    rmr.passqty =Convert.ToDouble( txtOtyDispatch.Text);
                    rawmaterial.Add(rmr);

                }
                val = BL_RawMaterialReceipt.Approve(rawmaterial);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("RawMaterialReceiptList");
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
            Response.Redirect("RawMaterialReceiptList");
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                rawmaterial = new List<RawMaterialReceipt>();

                for (int j = 0; j < grdRawMaterial.Rows.Count; j++)
                {
                    RawMaterialReceipt rmr = new RawMaterialReceipt();
                    if (j == 0)
                    {
                        rmr.grossweight = Convert.ToDouble(txtGrossWeight.Text);
                        rmr.tankerweight = Convert.ToDouble(txtTankerWeight.Text);
                        rmr.transitweight = (Convert.ToDouble(txtOtyDispatch.Text) - Convert.ToDouble(NetWeight.Value));
                        //  rmr.supplierweight = Convert.ToDouble(txtSupplierWeight.Text);
                        rmr.netweight = Convert.ToDouble(NetWeight.Value);
                        rmr.rmr_entrydate = txtdob.Value;
                        rmr.remarks = txtRemarks.Text;
                        rmr.user_id = Session["UserID"].ToString();
                        rmr.financial_year = Session["rmrfinancial_year"].ToString();
                        rmr.rawmaterial_receipt_id = Session["receipt_id"].ToString();
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
                    rawmaterial.Add(rmr);

                }
                string val;
              

                    val = BL_RawMaterialReceipt.Adminupdate(rawmaterial).ToString();
              
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("RawMaterialReceiptList");
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