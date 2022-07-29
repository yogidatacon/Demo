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
    public partial class WebForm1 : System.Web.UI.Page
    {
        List<Permit> permit = new List<Permit>();
        List<VAT_Master> vat = new List<VAT_Master>();
        RawMaterialReceipt rawmaterials = new RawMaterialReceipt();
        double total = 0;
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ViewState["Records"] == null)
                {
                    dt.Columns.Add("product_code");
                    dt.Columns.Add("product_name");
                    dt.Columns.Add("strength");
                    dt.Columns.Add("req_qty");
                    dt.Columns.Add("uom_code");
                    dt.Columns.Add("uom_name");
                    dt.Columns.Add("Doc_id");
                    ViewState["Records"] = dt;
                }
                CalendarExtender.EndDate = DateTime.Now;
                CalendarExtender3.EndDate = DateTime.Now;
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    Session["party"] = user.party_code;
                    List<LicenseApplication> raw = new List<LicenseApplication>();
                    raw = BL_LicenseApplication.Getlicense();
                    ddlLicnse.DataSource = raw;
                    ddlLicnse.DataTextField = "applicant_name";
                    ddlLicnse.DataValueField = "lic_application_id";
                    ddlLicnse.DataBind();
                    ddlLicnse.Items.Insert(0, "Select");
                    List<Molasses_Allocation> uo = new List<Molasses_Allocation>();
                    uo = BL_Molasses_Allocation.GetMTPList();
                    var list1 = from s in uo
                                where s.approverlevel == 2 && s.record_status == "I"
                                select s;
                    ddlallotmentno.DataSource = list1.ToList();
                    ddlallotmentno.DataTextField = "final_allotmentno";
                    ddlallotmentno.DataValueField = "molasses_allotment_request_id";
                    ddlallotmentno.DataBind();
                    ddlallotmentno.Items.Insert(0, "Select");

                    Session["district_code"] = user.district_code;
                    txtDATE.ReadOnly = true;
                    txtreceiptdate.ReadOnly = true;
                    // rtype.Value =Session["rtype"];
                    if (Session["UserID"].ToString() == "Admin")
                    {
                        btnSaveasDraft.Visible = true;
                        btnSubmit.Visible = true;
                        btnCancel.Visible = true;
                        btnApprove.Visible = true;
                        btnReject.Visible = true;

                        btnSubmit.Visible = true;
                        btnCancel.Visible = true;
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

                    List<UserDetails> party = new List<UserDetails>();
                    party = BL_Permit.Check();
                    var list = from s in party
                               where s.party_type == "ENA Distillery Unit" && s.role_name == "Applicant"
                               select s;
                    ddlpurchasedparty.DataSource = list.ToList();
                    ddlpurchasedparty.DataTextField = "user_name";
                    ddlpurchasedparty.DataValueField = "user_id";
                    ddlpurchasedparty.DataBind();
                    ddlpurchasedparty.Items.Insert(0, "Select");
                    ddlpurchasedparty.Visible = true;

                    List<Product_Master> product = new List<Product_Master>();
                    product = BL_ProductMaster.GetProductMasterList("");
                    ddlproduct.DataSource = product;
                    ddlproduct.DataTextField = "product_name";
                    ddlproduct.DataValueField = "product_code";
                    ddlproduct.DataBind();
                    ddlproduct.Items.Insert(0, "Select");
                    List<UOM_Master> uom = new List<UOM_Master>();
                    uom = BL_UOM.GetList("");
                    ddluom.DataSource = uom;
                    ddluom.DataTextField = "uom_name";
                    ddluom.DataValueField = "uom_code";
                    ddluom.DataBind();
                    ddluom.Items.Insert(0, "Select");
                    //DataTable dt = new DataTable();
                    //DataRow dr = dt.NewRow();

                    //dt.Rows.Add(dr);

                    //grdrawmaterial.DataSource = dt;

                    //grdrawmaterial.DataBind();
                    //grdrawmaterial.Visible = true;
                    if (Session["rtype"].ToString() != "0")
                    {

                        Permit per = new Permit();
                        per = BL_Permit.GetDetails(Convert.ToInt32(Session["permit_id"].ToString()), Session["Perfinancial_year"].ToString());
                        txtpermitno.Text = per.permit_no.ToString();
                        ddlLicnse.SelectedValue = per.lic_application_id.ToString();
                        ddlLicnse_SelectedIndexChanged(sender, e);
                        txtDATE.Text = per.permit_date;
                        txtgpd.Value = per.permit_date;
                        ddlpermitType.SelectedValue = per.permit_type.ToString();
                        ddlpurchasedparty.SelectedValue = per.purchase_from_party;
                        ddlpurchasedparty_SelectedIndexChanged(sender, e);
                        txtdistrict.Text = per.purchase_district;
                        txtagentname.Text = per.agent_name;
                        txtdutyamount.Text = per.duty_amt.ToString();
                        txtdutyrate.Text = per.duty_rate.ToString();
                        txtroutechart.Text = per.route_chart;
                       txtpass.Text = per.permit_validity.ToString();
                        txtchallanno.Text = per.challan_no.ToString();
                        txtreceiptdate.Text = per.challan_date;
                        receipt.Value = per.challan_date;
                        txtpermitqty.Text = per.permit_qty.ToString();
                        txtRemarks1.InnerText = per.remarks;
                        ddlallotmentno.SelectedValue = per.molasses_allotament_request_id.ToString();
                        ddlallotmentno_SelectedIndexChanged(sender, e);
                        for (int i = 0; i < per.permit_item.Count; i++)
                        {
                            if (i == 0)
                                dummy.Visible = false;
                            dt = (DataTable)ViewState["Records"];
                            dt.Rows.Add(per.permit_item[i].product_code, per.permit_item[i].product_name, per.permit_item[i].strength, per.permit_item[i].req_qty, per.permit_item[i].uom_code, per.permit_item[i].uom_name, per.permit_item[i].permit_item_id);
                            grdpermit.DataSource = dt;
                            grdpermit.DataBind();

                        }
                        Image1.Visible = false;
                        Image2.Visible = false;
                        grdpermit.Enabled = false;
                        grdpermit.Columns[grdpermit.Columns.Count - 1].Visible = false;
                        Image1.Visible = false;

                        //List<All_Approvals> approvals = new List<All_Approvals>();
                        //approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), rawmaterials.rawmaterial_receipt_id.ToString(), "PER");
                        //grdApprovalDetails.DataSource = approvals;
                        //grdApprovalDetails.DataBind();
                        //approverremarks.Visible = false;
                        //if (approvals.Count <= 0)
                        //{
                        //    approverid.Visible = false;
                        //    approverremarks.Visible = false;
                        //}
                        List<All_Approvals> approvals = new List<All_Approvals>();
                        approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), per.permit_id.ToString(), "PER");
                        grdApprovalDetails.DataSource = approvals;
                        grdApprovalDetails.DataBind();
                        if (Session["rtype"].ToString() == "1")
                        {
                            txtDATE.ReadOnly = true;
                            txtpermitno.ReadOnly = true;
                            ddlpermitType.Enabled = false;
                            ddlLicnse.Enabled = false;
                            ddlpurchasedparty.Enabled = false;
                            ddlproduct.Enabled = false;
                            txtagentname.ReadOnly = true;
                            txtroutechart.ReadOnly = true;
                            txtstrength.ReadOnly = true;
                            txtdistrict.ReadOnly = true;
                            txtdutyamount.ReadOnly = true;
                            txtdutyrate.ReadOnly = true;
                            txtpass.ReadOnly = true;

                            txtpermitno.ReadOnly = true;
                            txtpermitqty.ReadOnly = true;
                            permititme.Visible = false;
                            txtRemarks1.Attributes.Add("disabled", "disabled");
                            btnCancel.Visible = false;
                            btnSaveasDraft.Visible = false;
                            txtchallanno.ReadOnly = true;
                            btnSubmit.Visible = false;
                           // grdrawmaterial.Enabled = false;
                            btnApprove.Visible = false;
                            btnReject.Visible = false;
                            Image1.Visible = false;
                            txtDATE.ReadOnly = true;
                            if (user.role_name == "Bond Officer" && per.record_status == "Y")
                            {

                              //  grdrawmaterial.Enabled = false;
                                btnReject.Visible = true;
                                btnApprove.Visible = true;
                                txtapproverremarks.Visible = true;
                            }
                            if (per.record_status == "A")
                            {
                                approverid.Visible = true;
                                btnReject.Visible = false;
                                btnApprove.Visible = false;
                                approverremarks.Visible = false;
                                txtapproverremarks.Visible = false;
                            }
                        }
                        else
                        {
                            //foreach (GridViewRow g1 in grdrawmaterial.Rows)
                            //{
                            //    TextBox txt = g1.FindControl("txtQuantity") as TextBox;
                            //    TextBox txt1 = g1.FindControl("txtdips") as TextBox;
                            //    if ((rawmaterials.record_status == "Y" || rawmaterials.record_status == "A") && Session["UserID"].ToString() != "Admin")
                            //    {
                            //        txt.Enabled = false;
                            //        txt1.Enabled = false;
                            //    }
                            //}
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
            Response.Redirect("PermitList.aspx");
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
            Response.Redirect("PermitList.aspx");
        }

        protected void ddlRawMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        protected void ddlSupplierType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {


            }
        }


        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Permit from = new Permit();
                from.permit_id = Convert.ToInt32(Session["permit_id"].ToString());
                from.user_id = Session["UserID"].ToString();
                from.remarks = txtapproverremarks.Value;
                from.molasses_allotament_request_id = Convert.ToInt32(ddlallotmentno.SelectedValue);
                from.record_status = "A";
                from.party_code = party_code.Value;
                string val;
                val = BL_Permit.Approve(from);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("PermitList.aspx");
                }
                else
                {
                    string message = "Server Error";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
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
                Permit from = new Permit();
                from.permit_id = Convert.ToInt32(Session["permit_id"].ToString());
                from.user_id = Session["UserID"].ToString();
                from.molasses_allotament_request_id = Convert.ToInt32(ddlallotmentno.SelectedValue);
                from.remarks = txtapproverremarks.Value;
                from.record_status = "R";
                from.party_code = party_code.Value;
                string val;
                val = BL_Permit.Approve(from);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("PermitList.aspx");
                }
                else
                {
                    string message = "Server Error";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
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
                if (grdpermit.Rows.Count == 0 || grdpermit.Rows.Count == 0)
                {
                    if (grdpermit.Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert(' Add ReceiverVat');", true);
                        ddlproduct.ClearSelection();
                        ddlproduct.Focus();
                    }


                }
                else
                {
                    Permit from = new Permit();
                    if (txtDATE.Text == "" || txtDATE.Text != "")
                    {
                        txtDATE.Text = txtgpd.Value;
                    }

                    from.permit_date = txtDATE.Text;
                    from.permit_no = txtpermitno.Text;
                    from.purchase_district = txtdistrict.Text;
                    from.party_code = Session["party"].ToString();
                    from.permit_validity =txtpass.Text;
                    from.permit_type = ddlpermitType.SelectedValue;
                    from.purchase_from_party = ddlpurchasedparty.SelectedValue;
                    from.lic_application_id = Convert.ToInt32(ddlLicnse.SelectedValue);
                    from.agent_name = txtagentname.Text;
                    from.challan_date = receipt.Value;
                    from.challan_no = txtchallanno.Text;
                    from.duty_amt = Convert.ToDouble(txtdutyamount.Text);
                    from.molasses_allotament_request_id = Convert.ToInt32(ddlallotmentno.SelectedValue);
                    from.duty_rate = Convert.ToDouble(txtdutyrate.Text);
                    from.route_chart = txtroutechart.Text;
                    from.remarks = txtRemarks1.Value;
                    from.permit_qty = Convert.ToDouble(txtpermitqty.Text);
                    from.record_status = "Y";
                    from.user_id = Session["UserID"].ToString();
                    from.permit_item = new List<Permit_item>();

                    for (int i = 0; i < grdpermit.Rows.Count; i++)
                    {
                        Permit_item setup = new Permit_item();
                        setup.product_code = Convert.ToInt32((grdpermit.Rows[i].FindControl("lblproduct") as Label).Text);
                        setup.uom_code = (grdpermit.Rows[i].FindControl("lblUOM") as Label).Text;
                        setup.req_qty = Convert.ToDouble((grdpermit.Rows[i].FindControl("lblQuantity") as Label).Text);
                        setup.strength = Convert.ToDouble((grdpermit.Rows[i].FindControl("lblStrength") as Label).Text);

                        from.permit_item.Add(setup);
                    }

                    string val;
                    if (Session["rtype"].ToString() == "0")
                    {
                        val = BL_Permit.Insert(from);
                    }
                    else
                    {
                        from.permit_id = Convert.ToInt32(Session["permit_id"].ToString());
                        val = BL_Permit.Update(from);
                    }
                    if (val == "0")
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("PermitList.aspx");
                    }

                    else
                    {
                        string message = val;
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
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
                if (grdpermit.Rows.Count == 0 || grdpermit.Rows.Count == 0)
                {
                    if (grdpermit.Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert(' Add ReceiverVat');", true);
                        ddlproduct.ClearSelection();
                        ddlproduct.Focus();
                    }


                }
                else
                {
                    Permit from = new Permit();
                    if (txtDATE.Text == "" || txtDATE.Text != "")
                    {
                        txtDATE.Text = txtgpd.Value;
                    }

                    from.permit_date = txtDATE.Text;
                    from.permit_no = txtpermitno.Text;
                    from.purchase_district = txtdistrict.Text;
                    from.permit_validity = txtpass.Text;
                    from.permit_type = ddlpermitType.SelectedValue;
                    from.party_code = Session["party"].ToString();
                    from.purchase_from_party = ddlpurchasedparty.SelectedValue;
                    from.lic_application_id = Convert.ToInt32(ddlLicnse.SelectedValue);
                    from.agent_name = txtagentname.Text;
                    from.molasses_allotament_request_id = Convert.ToInt32(ddlallotmentno.SelectedValue);
                    from.challan_date = receipt.Value;
                    from.challan_no = txtchallanno.Text;
                    from.duty_amt = Convert.ToDouble(txtdutyamount.Text);
                    from.duty_rate = Convert.ToDouble(txtdutyrate.Text);
                    from.route_chart = txtroutechart.Text;
                    from.remarks = txtRemarks1.Value;
                    from.permit_qty = Convert.ToDouble(txtpermitqty.Text);
                    from.record_status = "N";
                    from.user_id = Session["UserID"].ToString();
                    from.permit_item = new List<Permit_item>();

                    for (int i = 0; i < grdpermit.Rows.Count; i++)
                    {
                        Permit_item setup = new Permit_item();
                        setup.product_code = Convert.ToInt32((grdpermit.Rows[i].FindControl("lblproduct") as Label).Text);
                        setup.uom_code = (grdpermit.Rows[i].FindControl("lblUOM") as Label).Text;
                        setup.req_qty = Convert.ToDouble((grdpermit.Rows[i].FindControl("lblQuantity") as Label).Text);
                        setup.strength = Convert.ToDouble((grdpermit.Rows[i].FindControl("lblStrength") as Label).Text);

                        from.permit_item.Add(setup);
                    }

                    string val;
                    if (Session["rtype"].ToString() == "0")
                    {
                        val = BL_Permit.Insert(from);
                    }
                    else
                    {
                        from.permit_id = Convert.ToInt32(Session["permit_id"].ToString());
                        val = BL_Permit.Update(from);
                    }
                    if (val == "0")
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("PermitList.aspx");
                    }

                    else
                    {
                        string message = val;
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    }
                }

            }
        }
        //private void GrandTotal()
        //{
        //    double GTotal = 0f;
        //    for (int i = 0; i < grdrawmaterial.Rows.Count; i++)
        //    {
        //        String total = (grdrawmaterial.Rows[i].FindControl("txtQuantity") as TextBox).Text;
        //        GTotal += Convert.ToDouble(total);
        //    }
        //    Session["total"] = GTotal.ToString();
        //}

        protected void grdrawmaterial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList uomlist = (e.Row.FindControl("ddluom") as DropDownList);
                DropDownList productlist = (e.Row.FindControl("ddlproduct") as DropDownList);
                List<UOM_Master> uom = new List<UOM_Master>();
                uom = BL_UOM.GetList("");
                uomlist.DataSource = uom;
                uomlist.DataTextField = "uom_name";
                uomlist.DataValueField = "uom_code";
                uomlist.DataBind();
                uomlist.Items.Insert(0, "Select");

                List<Product_Master> product = new List<Product_Master>();
                product = BL_ProductMaster.GetProductMasterList("");
                productlist.DataSource = product;
                productlist.DataTextField = "product_name";
                productlist.DataValueField = "product_code";
                productlist.DataBind();
                productlist.Items.Insert(0, "Select");

            }
        }

        int Doc_id = 1;
        protected void Add(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

                if (ddlproduct.SelectedValue != "")
                {
                    // get value and text which item you have selected
                    string vat_code = ddlproduct.SelectedValue;
                    string recivervat = ddlproduct.SelectedItem.ToString();
                    //add the selected item to gridview
                    DataTable dtgridview = ViewState["Records"] as DataTable;
                    DataRow dr2 = dtgridview.NewRow();
                    bool ifExist = false;
                    foreach (DataRow dr in dtgridview.Rows)
                    {
                        if (dr["Receiver"].ToString() == ddlproduct.SelectedItem.ToString())
                        {
                            ifExist = true;
                            break;
                        }
                    }
                    if (!ifExist)
                    {
                        dummy.Visible = false;
                        string m = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-sss");
                        dt = (DataTable)ViewState["Records"];

                        string product_name = ddlproduct.SelectedItem.ToString();
                        dt.Rows.Add(ddlproduct.SelectedValue, product_name, txtstrength.Text, txtquantity.Text, ddluom.SelectedValue, ddluom.SelectedItem.ToString(), Doc_id);
                        grdpermit.DataSource = dt;
                        grdpermit.DataBind();
                        Doc_id++;
                        grdpermit.Visible = true;
                        ddluom.ClearSelection();
                        ddlproduct.ClearSelection();
                        txtquantity.Text = "";
                        txtstrength.Text = "";

                    }
                    else
                    {
                        // this.lbgvck.Visible = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('ReceiverVat Already exist !!!!!\');", true);
                    }

                }
                //for (int i = 0; i < grdToReceiver.Rows.Count; i++)
                //{
                //    GridViewRow row1 = grdToReceiver.Rows[i];
                //    string Qty1 = (row1.Cells[1].FindControl("lblBulkLiters") as Label).Text;
                //    string Qty2 = (row1.Cells[1].FindControl("LPLiters") as Label).Text;
                //    inputbl += Convert.ToDouble(Qty1);
                //    inputlp += Convert.ToDouble(Qty2);
                //    Session["inbl"]  = (grdToReceiver.FooterRow.Cells[i].FindControl("lblTotal") as Label).Text = inputbl.ToString();
                //    Session["inlp"] = (grdToReceiver.FooterRow.Cells[i].FindControl("lblLPTotal") as Label).Text = inputlp.ToString();

                //}

            }
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {

            if (IsPostBack)
            {
                ImageButton lb = (ImageButton)sender;
                GridViewRow gvRow = (GridViewRow)lb.NamingContainer;

                DataTable dt2 = (DataTable)ViewState["Records"];
                ViewState["CurrentTable"] = dt2;
                int rowID = gvRow.RowIndex;
                DataTable dt1 = ViewState["Records"] as DataTable;
                dt1.Rows[rowID].Delete();
                ViewState["dt"] = dt1;
                grdpermit.DataSource = dt1;
                grdpermit.DataBind();
                if (dt1.Rows.Count < 1)
                    dummy.Visible = true;
                //for (int i = 0; i < grdToReceiver.Rows.Count; i++)
                //{
                //    GridViewRow row1 = grdToReceiver.Rows[i];
                //    string Qty1 = (row1.Cells[1].FindControl("lblBulkLiters") as Label).Text;
                //    string Qty2 = (row1.Cells[1].FindControl("LPLiters") as Label).Text;
                //    inputbl += Convert.ToDouble(Qty1);
                //    inputlp += Convert.ToDouble(Qty2);
                //    Session["inbl"] = (grdToReceiver.FooterRow.Cells[i].FindControl("lblTotal") as Label).Text = inputbl.ToString();
                //    Session["inlp"] = (grdToReceiver.FooterRow.Cells[i].FindControl("lblLPTotal") as Label).Text = inputlp.ToString();

                //}
            }
        }

        protected void ddlLicnse_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (IsPostBack)
            //{
            List<LicenseApplication> raw = new List<LicenseApplication>();
            raw = BL_LicenseApplication.Getlic(Convert.ToInt32(ddlLicnse.SelectedValue));
            txtlicense.Text = raw[0].lic_type_name;
            txtFeeAmount.Text = raw[0].lic_fee_amt.ToString();
            txtstartdate.Text = raw[0].start_date;
            txtenddate.Text = raw[0].end_date;
            //}
        }

        protected void ddlpurchasedparty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                UserDetails user = new UserDetails();
                user = BL_Permit.CheckUser(ddlpurchasedparty.SelectedValue);
                txtdistrict.Text = user.district_name;
            }

        }

        protected void ddlallotmentno_SelectedIndexChanged(object sender, EventArgs e)
        {
            Molasses_Allocation aval = new Molasses_Allocation();
            if (Session["rtype"].ToString() == "0")
            {
                Session["Perfinancial_year"] = Session["financial_year"].ToString();
            }
            aval = BL_Molasses_Allocation.GetDetails(ddlallotmentno.SelectedValue, Session["Perfinancial_year"].ToString());
            Permit per = new Permit();
            per = BL_Permit.Getvalue(ddlallotmentno.SelectedValue, Session["Perfinancial_year"].ToString());
            double c = aval.approved_qty - per.permit_qty;
            txtaaqty.Text = c.ToString();
        }

        protected void txtpermitqty_TextChanged(object sender, EventArgs e)
        {
            if (txtpermitqty.Text != "")
            {
                if (Convert.ToDouble(txtpermitqty.Text) <= Convert.ToDouble(txtaaqty.Text))
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Permit Qty cannot be greater than Available Qty  \');", true);
                    //string message = val;
                    //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    //sb.Append("<script type = 'text/javascript'>");
                    //sb.Append("window.onload=function(){");
                    //sb.Append("alert('");
                    //sb.Append(message);
                    //sb.Append("')};");
                    //sb.Append("</script>");
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    txtpermitqty.Focus();
                    txtpermitqty.Text = "";
                }
            }
        }
    }
}