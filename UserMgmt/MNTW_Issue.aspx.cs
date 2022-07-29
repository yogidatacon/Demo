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
    public partial class MNTW_Issue : System.Web.UI.Page
    {

        DataTable dt = new DataTable();
        static int Doc_id = 1;

        List<Product_Master> indents = new List<Product_Master>();
        List<WorkFlow> workflow = new List<WorkFlow>();
        List<VAT_Master> vats = new List<VAT_Master>();
        List<Party_Master> partymasters = new List<Party_Master>();
        Molasses_Allocation ic = new Molasses_Allocation();
        UserDetails user = new UserDetails();
        string record_status = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                ViewState["Records"] = null;
                CalendarExtender.EndDate = DateTime.Now;
                string userid = Session["UserID"].ToString();
                UserDetails user = new UserDetails();
                user = BL_UserDetails.GetUser(userid);
                Session["financial_year"] = user.financial_year;
                if (ViewState["Records"] == null)
                {

                    dt.Columns.Add("storage_vat_code");
                    dt.Columns.Add("storage_vat");

                    dt.Columns.Add("issue_vat_code");
                    dt.Columns.Add("issue_vat");


                    dt.Columns.Add("issued_qty_bl");
                    dt.Columns.Add("issued_qty_lpl");
                    dt.Columns.Add("strength");


                    ViewState["Records"] = dt;
                }

                if (Session["UserID"].ToString() == "Admin")
                {
                    btnSave.Visible = false;
                    btnSumbit.Visible = false;
                    btnCancel.Visible = false;

                }

                else if (user.role_name == "Bond Officer")
                {
                    btnApprove.Visible = true;
                    btnReject.Visible = true;
                    txtBORemarks.Visible = true;
                    btnSave.Visible = false;
                    btnSumbit.Visible = false;
                    btnCancel.Visible = false;
                    lblApproverRemarks.Visible = true;

                    txtApplicationRequestNo.ReadOnly = true;
                    txtIssueDate.ReadOnly = true;



                    ddlProduct.Attributes.Add("disabled", "disabled");
                    ddlStorageVAT.Attributes.Add("disabled", "disabled");
                     

                    txtAvailableQtyBL.ReadOnly = true;
                    txtAvailableQtyLPL.ReadOnly = true;
                    txtIssueQtyBL.ReadOnly = true;
                    txtIssueQtyLPL.ReadOnly = true;
                    txtStrength.ReadOnly = true;
                    btnAdd.Visible = false;
                    btnCancel.Visible = false;
                    btnSave.Visible = false;
                    btnSumbit.Visible = false;
                    btnAdd.Visible = false;
                    dummytable.Visible = false;

                }

                indents = new List<Product_Master>();
                indents = BL_ProductMaster.GetProductMasterList("");


                var enav = (from s in indents
                            where s.product_type_code == "8"
                            select s);

                ddlProduct.DataSource = enav.ToList();
                ddlProduct.DataTextField = "product_name";
                ddlProduct.DataValueField = "product_code";
                ddlProduct.DataBind();
                ddlProduct.Items.Insert(0, "Select");

                vats = new List<VAT_Master>();
                vats = BL_VATMaster.GetvatmasterList(user.party_code);

                var ind = (from s in vats
                           where s.vat_type_code == "MST"
                           select s);

                ddlStorageVAT.DataSource = ind.ToList();
                ddlStorageVAT.DataTextField = "vat_name";
                ddlStorageVAT.DataValueField = "vat_code";
                ddlStorageVAT.DataBind();
                ddlStorageVAT.Items.Insert(0, "Select");

                //ddlIssuedVat

                 

                if (Session["rtype"].ToString() != "0")
                {

                    List<All_Approvals> approvals = new List<All_Approvals>();
                    approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), Session["mirid"].ToString(), "MTI");
                    if (approvals.Count > 0)
                    {
                        var list4 = (from s in approvals
                                     where s.financial_year == Session["istwfinancial_year"].ToString()
                                     select s);
                        grdApprovalDetails.DataSource = list4.ToList();

                        grdApprovalDetails.DataBind();
                        approverid.Visible = true;
                    }
                }

                if (Session["rtype"].ToString() == "1")
                {
                    string id = Session["mirid"].ToString();
                    ic = BL_Molasses_Allocation.GetMNTIssueRegDetails(Session["mirid"].ToString(), Session["istwfinancial_year"].ToString());
                    txtApplicationRequestNo.Text = ic.application_requestno.ToString();
                    txtIssueDate.Text = ic.issue_date.ToString().Substring(0, 10);
                    Image1.Visible = false;
                    ddlProduct.SelectedValue = ic.product_code;
                    txtRemarks.Text = ic.remarks.ToString();

                    txtRemarks.ReadOnly = true;
                    txtApplicationRequestNo.ReadOnly = true;
                    txtIssueDate.ReadOnly = true;
                    lblApproverRemarks.Visible = false;
                    if (user.role_name == "Bond Officer")
                    {

                        btnAdd.Visible = false;
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        txtBORemarks.Visible = false;
                    }


                    ddlProduct.Attributes.Add("disabled", "disabled");
                    ddlStorageVAT.Attributes.Add("disabled", "disabled");
                     

                    txtAvailableQtyBL.ReadOnly = true;
                    txtAvailableQtyLPL.ReadOnly = true;
                    txtIssueQtyBL.ReadOnly = true;
                    txtIssueQtyLPL.ReadOnly = true;
                    txtStrength.ReadOnly = true;
                    btnAdd.Visible = false;
                    btnCancel.Visible = false;
                    btnSave.Visible = false;
                    btnSumbit.Visible = false;
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                    txtBORemarks.Visible = false;

                    dummytable.Visible = false;

                    using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
                    {
                        cn.Open();

                        using (NpgsqlCommand cmd1 = new NpgsqlCommand("select iri.issue_register_id, iri.storage_vat as storage_vat_code,iri.issue_qty as issued_qty_bl,"
                            + " cast(iri.issue_qty_lpl as text) as issued_qty_lpl, iri.strength,vm2.vat_name as storage_vat"
                            + " from exciseautomation.issue_register_item iri "
                            + " inner join exciseautomation.vat_master vm2 on vm2.vat_code =iri.storage_vat"
                            + " where iri.issue_register_id='" + id + "' and iri.financial_year='"+user.financial_year+"' order by iri.issue_register_id", cn))
                        {
                            cmd1.CommandType = System.Data.CommandType.Text;
                            //cmd.Parameters.AddWithValue("@UserID", userid);
                            NpgsqlDataReader dr2 = cmd1.ExecuteReader();

                            dt.Clear();
                            dt.Load(dr2);

                            grdAdd.DataSource = dt;
                            grdAdd.DataBind();

                        }



                    }

                }
                if (Session["rtype"].ToString() == "2")
                {
                    string id = Session["mirid"].ToString();
                    ic = BL_Molasses_Allocation.GetMNTIssueRegDetails(Session["mirid"].ToString(), Session["istwfinancial_year"].ToString());
                    txtApplicationRequestNo.Text = ic.application_requestno.ToString();
                    txtIssueDate.Text = ic.issue_date.ToString().Substring(0, 10);
                    Image1.Visible = false;
                    ddlProduct.SelectedValue = ic.product_code;
                    txtRemarks.Text = ic.remarks.ToString();
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
                        lblApproverRemarks.Visible = false;

                    }

                    dummytable.Visible = false;

                    using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
                    {
                        cn.Open();

                        using (NpgsqlCommand cmd1 = new NpgsqlCommand("select iri.issue_register_id, iri.storage_vat as storage_vat_code,iri.issue_qty as issued_qty_bl,"
                            + " cast(iri.issue_qty_lpl as text) as issued_qty_lpl, iri.strength,vm2.vat_name as storage_vat"
                            + " from exciseautomation.issue_register_item iri "
                            + " inner join exciseautomation.vat_master vm2 on vm2.vat_code =iri.storage_vat"
                            + " where iri.issue_register_id='" + id + "' and iri.financial_year='" + user.financial_year + "' order by iri.issue_register_id", cn))
                        {
                            cmd1.CommandType = System.Data.CommandType.Text;
                            //cmd.Parameters.AddWithValue("@UserID", userid);
                            NpgsqlDataReader dr2 = cmd1.ExecuteReader();

                            //dt.Clear();
                            dt.Load(dr2);

                            grdAdd.DataSource = dt;
                            grdAdd.DataBind();

                        }



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

        protected void ddlStorageVAT_SelectedIndexChanged(object sender, EventArgs e)
        {
            VAT_Master vat = new VAT_Master();
            vat = BL_VATMaster.GetVATDetails(ddlStorageVAT.SelectedValue.ToString());
            txtAvailableQtyBL.Text = vat.vat_availablecapacity.ToString();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {


                dummytable.Visible = false;
                 
                string storage_vat = ddlStorageVAT.SelectedItem.ToString();
                string issued_qty_bl = txtIssueQtyBL.Text;
                string issued_qty_lpl = Request.Form[txtIssueQtyLPL.UniqueID];
                string strength = txtStrength.Text;
                string storage_vat_code = ddlStorageVAT.SelectedValue.ToString();
                 
                int consumption_register_id = Doc_id;
                bool ifExist = false;
                dt = (DataTable)ViewState["Records"];

                foreach (DataRow row in dt.Rows)
                {
                    if (row["storage_vat_code"].ToString() == ddlStorageVAT.SelectedValue.ToString())

                        ifExist = true;
                    break;

                }

                if (!ifExist)
                {
                    dt.Rows.Add(storage_vat_code, storage_vat,"","",issued_qty_bl, issued_qty_lpl,
                        strength);
                }

                else
                {
                    // this.lbgvck.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Entry for same Storage Vat and Issue Vat Already exist !!!!!\');", true);
                }



                grdAdd.DataSource = dt;
                grdAdd.DataBind();

                txtIssueDate.Text = Request.Form[txtIssueDate.UniqueID];
                Doc_id++;
                txtAvailableQtyBL.Text = "";

                ddlStorageVAT.ClearSelection();
                
                txtAvailableQtyLPL.Text = "";
                txtIssueQtyBL.Text = "";
                txtIssueQtyLPL.Text = "";
                txtStrength.Text = "";


            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                user = BL_UserDetails.GetUser(Session["UserID"].ToString());
                Molasses_Allocation allotment = new Molasses_Allocation();
                allotment.application_requestno = txtApplicationRequestNo.Text;
                allotment.financial_year = Session["financial_year"].ToString();
                allotment.product_code = ddlProduct.SelectedValue;

                allotment.consumption_date = Request.Form[txtIssueDate.UniqueID];


                // allotment.prov_indent_qty = Convert.ToDouble(txtProvisionalIndent.Text);

                allotment.record_status = "Y";

                allotment.user_id = Session["UserID"].ToString();
                allotment.remarks = txtRemarks.Text;
                allotment.party_code = user.party_code.ToString();
                allotment.docs = new List<EASCM_DOCS>();
                dt = ViewState["Records"] as DataTable;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    EASCM_DOCS doc = new EASCM_DOCS();
                    doc.issue_vat = dr["issue_vat_code"].ToString();
                    doc.storage_vat = dr["storage_vat_code"].ToString();
                    doc.issue_qty = dr["issued_qty_bl"].ToString();
                    doc.consumption_qty = dr["issued_qty_lpl"].ToString();
                    doc.strength = dr["strength"].ToString();

                    allotment.docs.Add(doc);
                    i++;
                }
                string val;

                if (Session["rtype"].ToString() == "0")
                {

                    val = BL_Molasses_Allocation.Insert_MTP_Issue(allotment);
                }
                if (Session["rtype"].ToString() == "2")
                {
                    allotment.issue_no = Convert.ToInt32(Session["mirid"].ToString());

                    val = BL_Molasses_Allocation.UpdateMNTIssueReg(allotment);
                }
                Session["UserID"] = Session["UserID"];
                ViewState.Clear();
                Response.Redirect("MNTW_IssueList.aspx");

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("MNTW_IssueList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                user = BL_UserDetails.GetUser(Session["UserID"].ToString());
                Molasses_Allocation allotment = new Molasses_Allocation();
                allotment.application_requestno = txtApplicationRequestNo.Text;
                allotment.financial_year = Session["financial_year"].ToString();
                allotment.product_code = ddlProduct.SelectedValue;

                allotment.consumption_date = txtIssueDate.Text;


                // allotment.prov_indent_qty = Convert.ToDouble(txtProvisionalIndent.Text);

                allotment.record_status = "N";

                allotment.user_id = Session["UserID"].ToString();
                allotment.remarks = txtRemarks.Text;
                allotment.party_code = user.party_code.ToString();
                allotment.docs = new List<EASCM_DOCS>();
                dt = ViewState["Records"] as DataTable;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    EASCM_DOCS doc = new EASCM_DOCS();
                    doc.issue_vat = dr["issue_vat_code"].ToString();
                    doc.storage_vat = dr["storage_vat_code"].ToString();
                    doc.issue_qty = dr["issued_qty_bl"].ToString();
                    doc.consumption_qty = dr["issued_qty_lpl"].ToString();
                    doc.strength = dr["strength"].ToString();

                    allotment.docs.Add(doc);
                    i++;
                }
                string val;


                if (Session["rtype"].ToString() == "0")
                {

                    val = BL_Molasses_Allocation.Insert_MTP_Issue(allotment);
                }
                if (Session["rtype"].ToString() == "2")
                {
                    allotment.issue_no = Convert.ToInt32(Session["mirid"].ToString());

                    val = BL_Molasses_Allocation.UpdateMNTIssueReg(allotment);
                }

                //val = BL_Molasses_Allocation.Insert_MTP_Issue(allotment);
                Session["UserID"] = Session["UserID"];
                ViewState.Clear();
                Response.Redirect("MNTW_IssueList.aspx");

            }
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("MNTW_IssueList.aspx");
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Molasses_Allocation allotment = new Molasses_Allocation();
                allotment.application_requestno = txtApplicationRequestNo.Text;

                allotment.product_code = ddlProduct.SelectedValue;
                allotment.financial_year = Session["financial_year"].ToString();
                allotment.consumption_date = txtIssueDate.Text;
                allotment.issue_no = Convert.ToInt32(Session["mirid"].ToString());

                // allotment.prov_indent_qty = Convert.ToDouble(txtProvisionalIndent.Text);

                allotment.record_status = "A";

                allotment.user_id = Session["UserID"].ToString();
                allotment.remarks = txtBORemarks.Text;
                allotment.docs = new List<EASCM_DOCS>();
                dt = ViewState["Records"] as DataTable;
                int i = 0;
                string storagevat_code = "";
                string issue_qt = "";
                foreach (DataRow dr in dt.Rows)
                {
                    EASCM_DOCS doc = new EASCM_DOCS();
                    doc.issue_vat = dr["issue_vat_code"].ToString();
                    doc.storage_vat = dr["storage_vat_code"].ToString();
                    doc.issue_qty = dr["issued_qty_bl"].ToString();
                    doc.consumption_qty = dr["issued_qty_lpl"].ToString();
                    doc.strength = dr["strength"].ToString();

                    allotment.docs.Add(doc);

                    storagevat_code = dr["storage_vat_code"].ToString();
                    issue_qt = dr["issued_qty_bl"].ToString();
                    i++;
                }
                string val;


                //val = BL_Molasses_Allocation.Approve_MTNIssue(allotment);
                //Session["UserID"] = Session["UserID"];
                //Response.Redirect("MNT_IssueList.aspx");

                VAT_Master vat = new VAT_Master();
                vat = BL_VATMaster.GetVATDetails(storagevat_code.ToString());
                string vat_capacity = vat.vat_availablecapacity.ToString();

                if (Convert.ToDecimal(vat_capacity) >= Convert.ToDecimal(issue_qt))
                {

                    val = BL_Molasses_Allocation.Approve_MTNIssue(allotment);
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("MNTW_IssueList.aspx");
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
                allotment.financial_year = Session["financial_year"].ToString();
                allotment.product_code = ddlProduct.SelectedValue;

                allotment.consumption_date = txtIssueDate.Text;
                allotment.issue_no = Convert.ToInt32(Session["mirid"].ToString());

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
                    doc.storage_vat = dr["storage_vat_code"].ToString();
                    doc.issue_qty = dr["issued_qty_bl"].ToString();

                    doc.strength = dr["strength"].ToString();

                    allotment.docs.Add(doc);
                    i++;
                }
                string val;


                val = BL_Molasses_Allocation.Approve_MTNIssue(allotment);
                Session["UserID"] = Session["UserID"];
                Response.Redirect("MNTW_IssueList.aspx");

            }
        }
    }
}