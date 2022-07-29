using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class MNT_Consumption : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
       int Doc_id = 1;
        string available_qty = "0";

        List<Product_Master> indents = new List<Product_Master>();
        List<WorkFlow> workflow = new List<WorkFlow>();
        List<VAT_Master> vats = new List<VAT_Master>();
        // UserDetails user = new UserDetails();
        List<Party_Master> partymasters = new List<Party_Master>();
        Molasses_Allocation ic = new Molasses_Allocation();
        UserDetails user = new UserDetails();
        string record_status = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //string userid = Session["UserID"].ToString();
            if (!IsPostBack)
            {
                //CalendarExtender.StartDate = DateTime.Now;
                CalendarExtender.EndDate = DateTime.Now;
                string userid = Session["UserID"].ToString();
                UserDetails user = new UserDetails();
                user = BL_UserDetails.GetUser(userid);
                Session["financial_year"] = user.financial_year;

                if (ViewState["Records"] == null)
                {
                    dt.Columns.Add("issue_vat_code");
                    dt.Columns.Add("issue_vat");
                    dt.Columns.Add("medicine_name");
                    dt.Columns.Add("batch_no");
                    dt.Columns.Add("consumption_qty");
                    dt.Columns.Add("strength");
                    dt.Columns.Add("consumption_register_id");
                    dt.Columns.Add("medicine_qty");
                    dt.Columns.Add("consumption_qtyLPL");
                  
                    ViewState["Records"] = dt;
                }

                if (Session["UserID"].ToString() == "Admin")
                {
                    btnSave.Visible = true;
                    btnSubmit.Visible = true;
                    btnCancel.Visible = true;

                }

                else if (user.role_name == "Bond Officer")
                {
                    

                    btnApprove.Visible = true;
                    btnReject.Visible = true;
                    btnSave.Visible = false;
                    btnSubmit.Visible = false;
                    lblApproverRemarks.Visible = true;

                    txtRemarks.ReadOnly = true;
                    txtApplicationRequestNo.ReadOnly = true;
                    txtIssueDate.ReadOnly = true;
                    txtMedicineQuantity.ReadOnly = true;


                    ddlProduct.Attributes.Add("disabled", "disabled");
                    ddlVAT1.Attributes.Add("disabled", "disabled");

                    txtQtyBL.ReadOnly = true;
                    txtQtyLPL.ReadOnly = true;
                    txtApplicationRequestNo.ReadOnly = true;
                    txtAvailableQty.ReadOnly = true;
                    txtBatchNo.ReadOnly = true;
                    txtNameofMedicine.ReadOnly = true;
                    txtStrength.ReadOnly = true;
                    btnAdd.Visible = false;

                   
                        
                        btnAdd.Visible = false;
                    

                }



                indents = new List<Product_Master>();
                indents = BL_ProductMaster.GetProductMasterList("");



                var enav = (from s in indents
                            where s.product_type_code == "7"
                            select s);

                ddlProduct.DataSource = enav.ToList();
                ddlProduct.DataTextField = "product_name";
                ddlProduct.DataValueField = "product_code";
                ddlProduct.DataBind();
                ddlProduct.Items.Insert(0, "Select");

                vats = new List<VAT_Master>();
                vats = BL_VATMaster.GetvatmasterList(user.party_code);

                var ind = (from s in vats
                           where s.vat_type_code == "ISS"
                           select s);

                ddlVAT1.DataSource = ind.ToList();
                ddlVAT1.DataTextField = "vat_name";
                ddlVAT1.DataValueField = "vat_code";
                ddlVAT1.DataBind();
                ddlVAT1.Items.Insert(0, "Select");

                if (Session["rtype"].ToString() != "0")
                {
                    List<All_Approvals> approvals = new List<All_Approvals>();
                    approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), Session["mirid"].ToString(), "MTC");
                    if (approvals.Count > 0)
                    {
                        var list4 = (from s in approvals
                                     where s.financial_year == Session["CONfinancial_year"].ToString()
                                     select s);
                        grdApprovalDetails.DataSource = list4.ToList();
                        grdApprovalDetails.DataBind();
                        approverid.Visible = true;
                    }
                }
               
                    if (Session["rtype"].ToString() == "1")
                {
                    lblApproverRemarks.Visible = false;
                    string id = Session["mirid"].ToString();
                    ic = BL_Molasses_Allocation.GetMNTConsRegDetails(Session["mirid"].ToString(), Session["CONfinancial_year"].ToString());
                    txtApplicationRequestNo.Text = ic.application_requestno.ToString();
                    txtIssueDate.Text = ic.consumption_date.ToString().Substring(0, 10);
                    Image1.Visible = false;
                    ddlProduct.SelectedValue = ic.product_code;
                    txtRemarks.Text = ic.remarks.ToString();

                    txtTotalConsumptionv.Value = ic.qty_allotted_till_date.ToString();
                    txtWaste.Text = ic.prov_indent_qty.ToString();
                    txtTotalConsumptionL.Value = ic.reqd_qty.ToString();

                    txtWaste.ReadOnly = true;
                    txtTotalConsumptionv.Disabled = true;
                    txtTotalConsumptionL.Disabled = true;
                    txtMedicineQuantity.ReadOnly = true;

                    txtRemarks.ReadOnly = true;
                    txtApplicationRequestNo.ReadOnly = true;
                    txtIssueDate.ReadOnly = true;
                    if (user.role_name == "Bond Officer")
                    {
                        txtBORemarks.Visible = false;
                        btnAdd.Visible = false;
                    }


                    ddlProduct.Attributes.Add("disabled", "disabled");
                    ddlVAT1.Attributes.Add("disabled", "disabled");

                    txtQtyBL.ReadOnly = true;
                    txtQtyLPL.ReadOnly = true;
                    txtApplicationRequestNo.ReadOnly = true;
                    txtAvailableQty.ReadOnly = true;
                    txtBatchNo.ReadOnly = true;
                    txtNameofMedicine.ReadOnly = true;
                    txtStrength.ReadOnly = true;
                    btnAdd.Visible = false;
                    btnCancel.Visible = false;
                    btnSave.Visible = false;
                    btnSubmit.Visible = false;
                    btnApprove.Visible = false;
                    btnReject.Visible = false;


                       
                    
                    dummytable.Visible = false;

                    using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
                    {
                        cn.Open();

                        using (NpgsqlCommand cmd1 = new NpgsqlCommand("select distinct (consumption_register_id),medicine_qty, issue_vat as issue_vat_code,vm.vat_name as issue_vat, medicine_name, batch_no,consumption_bl_qty as consumption_qty,cast(consumption_lp_qty as text) as consumption_qtyLPL,"
                            + " strength FROM exciseautomation.consumption_register_item cri"
                            + " inner join exciseautomation.vat_master vm on vm.vat_code = cri.issue_vat where consumption_register_id='" + id + "' and cri.financial_year='" + user.financial_year + "' order by consumption_register_id", cn))
                        {
                            cmd1.CommandType = System.Data.CommandType.Text;
                            //cmd.Parameters.AddWithValue("@UserID", userid);
                            NpgsqlDataReader dr2 = cmd1.ExecuteReader();

                            dt.Load(dr2);

                            grdAdd.DataSource = dt;
                            grdAdd.DataBind();
                          //  ViewState["Records"] = dt;

                        }



                    }
                    foreach (GridViewRow dr1 in grdAdd.Rows)
                    {
                        ImageButton btn = dr1.FindControl("ImageButton1") as ImageButton;
                        btn.Visible = false;
                    }
                    grdAdd.Columns[grdAdd.Columns.Count - 1].Visible = false;

                }

                if (Session["rtype"].ToString() == "2")
                {
                    string id = Session["mirid"].ToString();
                    ic = BL_Molasses_Allocation.GetMNTConsRegDetails(Session["mirid"].ToString(), Session["CONfinancial_year"].ToString());
                    txtApplicationRequestNo.Text = ic.application_requestno.ToString();
                    txtIssueDate.Text = ic.consumption_date.ToString().Substring(0, 10);
                    
                    ddlProduct.SelectedValue = ic.product_code;
                    txtRemarks.Text = ic.remarks.ToString();
                    Image1.Visible = false;

                    txtWaste.ReadOnly =false;
                    txtTotalConsumptionv.Disabled = true;

                    txtTotalConsumptionv.Value = ic.qty_allotted_till_date.ToString();
                    txtWaste.Text = ic.prov_indent_qty.ToString();
                    txtTotalConsumptionL.Value = ic.reqd_qty.ToString();

                    txtAvailableQty.ReadOnly = true;
                    if (user.role_name == "Bond Officer")
                    {
                        btnAdd.Visible = false;
                        btnCancel.Visible = false;
                        btnSave.Visible = false;
                        btnSubmit.Visible = false;
                        btnApprove.Visible = true;
                        btnReject.Visible = true;
                        txtMedicineQuantity.ReadOnly = true;
                        txtAvailableQty.ReadOnly = false;
                        btnAdd.Visible = false;
                        txtTotalConsumptionL.Disabled = true;
                        Image1.Visible = false; 
                    }

                    if (user.role_name == "Applicant")
                    {
                        btnAdd.Visible = true;
                        btnCancel.Visible = true;
                        btnSave.Visible = true;
                        btnSubmit.Visible = true;
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        lblApproverRemarks.Visible = false;
                        txtAvailableQty.ReadOnly = false;
                        
                    }

                    if (user.role_name == "Bond Officer")
                    {
                        txtRemarks.Visible = true;
                        txtRemarks.ReadOnly = true;
                        txtBORemarks.Visible = true;
                        btnAdd.Visible = false;
                    }

                    if (user.role_name == "Applicant")
                    {
                        txtRemarks.Visible = true;
                        txtBORemarks.Visible = false;

                    }
                   
                    dummytable.Visible = false;

                    using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
                    {
                        cn.Open();

                        using (NpgsqlCommand cmd1 = new NpgsqlCommand("select distinct (consumption_register_id),medicine_qty, issue_vat as issue_vat_code,vm.vat_name as issue_vat, medicine_name, batch_no,consumption_bl_qty as consumption_qty,cast(consumption_lp_qty as text) as consumption_qtyLPL,"
                            + " strength FROM exciseautomation.consumption_register_item cri"
                            + " inner join exciseautomation.vat_master vm on vm.vat_code = cri.issue_vat where consumption_register_id='" + id + "' and cri.financial_year='"+user.financial_year+"' order by consumption_register_id", cn))
                        {
                            cmd1.CommandType = System.Data.CommandType.Text;
                            //cmd.Parameters.AddWithValue("@UserID", userid);
                            NpgsqlDataReader dr2 = cmd1.ExecuteReader();

                            dt.Load(dr2);

                            grdAdd.DataSource = dt;
                            grdAdd.DataBind();
                          //  ViewState["Records"] = dt;

                        }



                    }
                    if (ic.record_status == "Y" || user.role_name == "Bond Officer")
                    {
                        foreach (GridViewRow dr1 in grdAdd.Rows)
                        {
                            ImageButton btn = dr1.FindControl("ImageButton1") as ImageButton;
                            btn.Visible = false;
                        }
                        grdAdd.Columns[grdAdd.Columns.Count - 1].Visible = false;
                    }

                }

                if (user.role_name != "Bond Officer")
                {
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                    txtBORemarks.Visible = false;
                }

            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                user = BL_UserDetails.GetUser(Session["UserID"].ToString());
                Molasses_Allocation allotment = new Molasses_Allocation();
                allotment.application_requestno = txtApplicationRequestNo.Text;

                allotment.product_code = ddlProduct.SelectedValue;

                allotment.consumption_date = Request.Form[txtIssueDate.UniqueID];
                

                // allotment.prov_indent_qty = Convert.ToDouble(txtProvisionalIndent.Text);

                allotment.record_status = "Y";

                allotment.user_id = Session["UserID"].ToString();
                allotment.remarks = txtRemarks.Text;
                allotment.party_code = user.party_code.ToString();

                allotment.qty_allotted_till_date = Convert.ToDouble(txtTotalConsumptionv.Value);
                allotment.prov_indent_qty= Convert.ToDouble(txtWaste.Text);
                allotment.reqd_qty= Convert.ToDouble(txtTotalConsumptionL.Value);
                allotment.financial_year = Session["financial_year"].ToString();

                allotment.docs = new List<EASCM_DOCS>();
                dt = ViewState["Records"] as DataTable;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    EASCM_DOCS doc = new EASCM_DOCS();
                    doc.issue_vat = dr["issue_vat_code"].ToString();
                    doc.medicine_name = (grdAdd.Rows[i].FindControl("lblmedicine_name") as Label).Text;
                    doc.batch_no = (grdAdd.Rows[i].FindControl("lblbatch_no") as Label).Text;
                    doc.consumption_qty = dr["consumption_qty"].ToString();
                    doc.strength = dr["strength"].ToString();
                    doc.issue_qty= dr["medicine_qty"].ToString();
                    doc.storage_vat = dr["consumption_qtyLPL"].ToString();
                    allotment.docs.Add(doc);
                    i++;
                }
                string val;
                if (Session["rtype"].ToString() == "0")
                {
                    val = BL_Molasses_Allocation.Insert_MTP_Con(allotment);
                }
                if (Session["rtype"].ToString() == "2")
                {
                    allotment.consumption_no = Convert.ToInt32(Session["mirid"].ToString());
                    allotment.issue_no = Convert.ToInt32(Session["mirid"].ToString());
                    val = BL_Molasses_Allocation.UpdateMNTConsumReg(allotment);
                }

                
                Session["UserID"] = Session["UserID"];
                Response.Redirect("MNT_ConsumptionList.aspx");

            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {


                dummytable.Visible = false;
                string issue_vat = ddlVAT1.SelectedItem.ToString();
                string medicine_name = txtNameofMedicine.Text;
                string batch_no = txtBatchNo.Text;
                string consumption_qty = txtQtyBL.Text;
                string strength = txtStrength.Text;
                string medicine_qty = txtMedicineQuantity.Text;
                string consumption_qtyLPL = Request.Form[txtQtyLPL.UniqueID];

                int consumption_register_id = Doc_id;
                string issue_vat_code = ddlVAT1.SelectedValue.ToString();
                dt = (DataTable)ViewState["Records"];
                dt.Rows.Add(issue_vat_code,issue_vat, medicine_name, batch_no, consumption_qty, strength, consumption_register_id, medicine_qty,consumption_qtyLPL);
                grdAdd.DataSource = dt;
                grdAdd.DataBind();
                txtIssueDate.Text = Request.Form[txtIssueDate.UniqueID];
                decimal sum = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sum += decimal.Parse(dt.Rows[i]["consumption_qty"].ToString());
                }
               
                txtTotalConsumptionv.Value = sum.ToString();

                decimal sum_lpl = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sum_lpl += Decimal.Parse(dt.Rows[i]["consumption_qtyLPL"].ToString());
                }
               
                txtTotalConsumptionL.Value = sum_lpl.ToString();

                Doc_id++;
                

                ddlVAT1.Enabled=false;
                txtQtyBL.Text = "";
                txtStrength.Text = "";
                txtQtyLPL.Text = "";
                txtNameofMedicine.Text = "";
                txtBatchNo.Text = "";
                txtMedicineQuantity.Text = "";



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
                int value = dt2.Rows.Count;
                int rowID = gvRow.RowIndex;
                DataTable dt1 = ViewState["Records"] as DataTable;
                dt1.Rows[rowID].Delete();
                dt1.AcceptChanges();
                ViewState["dt"] = dt1;
                if (value == 1 )
                {
                    dt1.Clear();
                }
                grdAdd.DataSource = dt1;
                grdAdd.DataBind();
                if (dt1.Rows.Count < 1)
                    dummytable.Visible = true;
                txtIssueDate.Text = Request.Form[txtIssueDate.UniqueID];
                decimal sum = 0;
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    sum += decimal.Parse(dt1.Rows[i]["consumption_qty"].ToString());
                }
                txtTotalConsumptionv.Value = sum.ToString();

                decimal sum_lpl = 0;
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    sum_lpl += Decimal.Parse(dt1.Rows[i]["consumption_qtyLPL"].ToString());
                }
                txtTotalConsumptionL.Value = sum_lpl.ToString();
                ddlVAT1.Enabled = true;
                txtQtyBL.Text = "";
                txtStrength.Text = "";
                txtQtyLPL.Text = "";
                txtNameofMedicine.Text = "";
                txtBatchNo.Text = "";
                txtMedicineQuantity.Text = "";
            }
        }

        protected void ddlVAT_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void ddlVAT_SelectedIndexChanged1(object sender, EventArgs e)
        {
            
        }

        protected void ddlVAT1_SelectedIndexChanged(object sender, EventArgs e)
        {
            VAT_Master vat = new VAT_Master();
            vat = BL_VATMaster.GetVATDetails(ddlVAT1.SelectedValue.ToString());
            txtAvailableQty.Text = vat.vat_availablecapacity.ToString();
            available_qty= vat.vat_availablecapacity.ToString();
        }

        protected void ddlVAT1_SelectedIndexChanged1(object sender, EventArgs e)
        {
            VAT_Master vat = new VAT_Master();
            vat = BL_VATMaster.GetVATDetails(ddlVAT1.SelectedValue.ToString());
            txtAvailableQty.Text = vat.vat_availablecapacity.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                user = BL_UserDetails.GetUser(Session["UserID"].ToString());
                Molasses_Allocation allotment = new Molasses_Allocation();
                allotment.application_requestno = txtApplicationRequestNo.Text;

                allotment.product_code = ddlProduct.SelectedValue;

                allotment.consumption_date = txtIssueDate.Text;
                //allotment.consumption_no = Convert.ToInt32(Session["mirid"].ToString());

                // allotment.prov_indent_qty = Convert.ToDouble(txtProvisionalIndent.Text);

                allotment.record_status = "N";
                allotment.party_code = user.party_code.ToString();
                allotment.user_id = Session["UserID"].ToString();
                allotment.remarks = txtRemarks.Text;
                allotment.qty_allotted_till_date = Convert.ToDouble(txtTotalConsumptionv.Value);
                allotment.prov_indent_qty = Convert.ToDouble(txtWaste.Text);
                allotment.reqd_qty = Convert.ToDouble(txtTotalConsumptionL.Value);
                allotment.financial_year = Session["financial_year"].ToString();
                allotment.docs = new List<EASCM_DOCS>();
                dt = ViewState["Records"] as DataTable;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    EASCM_DOCS doc = new EASCM_DOCS();
                    doc.issue_vat = dr["issue_vat_code"].ToString();
                    doc.medicine_name = (grdAdd.Rows[i].FindControl("lblmedicine_name") as Label).Text;
                    doc.batch_no = (grdAdd.Rows[i].FindControl("lblbatch_no") as Label).Text;
                    doc.consumption_qty = dr["consumption_qty"].ToString();
                    doc.strength = dr["strength"].ToString();
                    doc.issue_qty = dr["medicine_qty"].ToString();
                    doc.storage_vat= dr["consumption_qtyLPL"].ToString();
                    allotment.docs.Add(doc);
                    i++;
                }
                string val;
                if (Session["rtype"].ToString() == "0")
                {
                    val = BL_Molasses_Allocation.Insert_MTP_Con(allotment);
                }
                if (Session["rtype"].ToString() == "2")
                {
                    allotment.issue_no = Convert.ToInt32(Session["mirid"].ToString());
                    val = BL_Molasses_Allocation.UpdateMNTConsumReg(allotment);
                }


                Session["UserID"] = Session["UserID"];
                Response.Redirect("MNT_ConsumptionList.aspx");
                

            }
            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("MNT_ConsumptionList.aspx");
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("MNT_ConsumptionList.aspx");
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

                Molasses_Allocation allotment = new Molasses_Allocation();
                allotment.application_requestno = txtApplicationRequestNo.Text;

                allotment.product_code = ddlProduct.SelectedValue;

                allotment.consumption_date = txtIssueDate.Text;
                allotment.consumption_no = Convert.ToInt32(Session["mirid"].ToString());

                // allotment.prov_indent_qty = Convert.ToDouble(txtProvisionalIndent.Text);
                allotment.financial_year = Session["financial_year"].ToString();
                allotment.record_status = "A";

                allotment.user_id = Session["UserID"].ToString();
                allotment.remarks = txtBORemarks.Text;
                allotment.reqd_qty = Convert.ToDouble(txtWaste.Text.ToString());
                allotment.docs = new List<EASCM_DOCS>();
                dt = ViewState["Records"] as DataTable;
                int i = 0;
                string storagevat_code = "";
                string issue_qt = "";
                foreach (DataRow dr in dt.Rows)
                {
                    EASCM_DOCS doc = new EASCM_DOCS();
                    doc.issue_vat = dr["issue_vat_code"].ToString();

                    doc.medicine_name = (grdAdd.Rows[i].FindControl("lblmedicine_name") as Label).Text;
                    doc.batch_no = (grdAdd.Rows[i].FindControl("lblbatch_no") as Label).Text;
                    doc.consumption_qty = dr["consumption_qty"].ToString();
                    doc.strength = dr["strength"].ToString();
                    storagevat_code = dr["issue_vat_code"].ToString();
                    issue_qt = dr["consumption_qty"].ToString();
                    allotment.docs.Add(doc);

                    i++;
                    ddlVAT1.SelectedValue = dr["issue_vat_code"].ToString();
                }
                string val;


                //val = BL_Molasses_Allocation.Approve_MTNConsumption(allotment);
                //Session["UserID"] = Session["UserID"];
                //Response.Redirect("MNT_ConsumptionList.aspx");

                
                VAT_Master vat = new VAT_Master();
                vat = BL_VATMaster.GetVATDetails(ddlVAT1.SelectedValue.ToString());
                //txtAvailableQty.Text = vat.vat_availablecapacity.ToString();
                string vat_capacity = vat.vat_availablecapacity.ToString();

                if (Convert.ToDecimal(vat_capacity) >= Convert.ToDecimal(issue_qt))
                {
                    val = BL_Molasses_Allocation.Approve_MTNConsumption(allotment);
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("MNT_ConsumptionList.aspx");
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Cannot Approve as Available Quantity is less than Issuing Quantity !!!!!\');", true);
                }
            }

        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Molasses_Allocation allotment = new Molasses_Allocation();
                allotment.application_requestno = txtApplicationRequestNo.Text;

                allotment.product_code = ddlProduct.SelectedValue;
                allotment.financial_year = Session["financial_year"].ToString();
                allotment.consumption_date = txtIssueDate.Text;
                allotment.consumption_no = Convert.ToInt32(Session["mirid"].ToString());

                // allotment.prov_indent_qty = Convert.ToDouble(txtProvisionalIndent.Text);

                allotment.record_status = "R";

                allotment.user_id = Session["UserID"].ToString();
                allotment.remarks = txtBORemarks.Text;
                allotment.docs = new List<EASCM_DOCS>();
                dt = ViewState["Records"] as DataTable;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    EASCM_DOCS doc = new EASCM_DOCS();
                    doc.issue_vat = dr["issue_vat_code"].ToString();
                    doc.medicine_name = (grdAdd.Rows[i].FindControl("lblmedicine_name") as Label).Text;
                    doc.batch_no = (grdAdd.Rows[i].FindControl("lblbatch_no") as Label).Text;
                    doc.consumption_qty = dr["consumption_qty"].ToString();
                    doc.strength = dr["strength"].ToString();

                    allotment.docs.Add(doc);
                    i++;
                }
                string val;


                val = BL_Molasses_Allocation.Approve_MTNConsumption(allotment);
                Session["UserID"] = Session["UserID"];
                Response.Redirect("MNT_ConsumptionList.aspx");

            }
        }

        
    }
}
