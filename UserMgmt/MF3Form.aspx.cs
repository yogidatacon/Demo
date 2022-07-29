using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class MF3Form : System.Web.UI.Page
    {
        DataTable storagedt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    txtRow6.Attributes.Add("maxlength", txtRow6.MaxLength.ToString());
                    txtrow7.Attributes.Add("maxlength", txtrow7.MaxLength.ToString());


                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];

                    if (ViewState["StorageRecords"] == null)
                    {
                        storagedt.Columns.Add("vat_code");
                        storagedt.Columns.Add("vat_name");
                        storagedt.Columns.Add("bal_capacity");
                        storagedt.Columns.Add("molasses_prod_tank_storage_id");
                        ViewState["StorageRecords"] = storagedt;
                    }
                    Session["UserID"] = Session["UserID"];
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        List<VAT_Master> vatmasters = new List<VAT_Master>();
                        vatmasters = BL_VATMaster.GetvatmasterList(user.party_code);
                        txtFinancialYear.Text = user.financial_year;
                        // ddStorageYear.Items.Insert(0, user.financial_year);
                        // ddStorageYear.Enabled = false;
                        txtToalCapacity.Text = "0";
                        partycode.Value = user.party_code;
                        ddlStorageTankname.DataSource = vatmasters.ToList();
                        ddlStorageTankname.DataTextField = "vat_name";
                        ddlStorageTankname.DataValueField = "vat_code";
                        ddlStorageTankname.DataBind();
                        storagedeleted = "";
                        ddlStorageTankname.Items.Insert(0, "Select");
                        List<Product_Master> products = new List<Product_Master>();
                        products = BL_ProductMaster.GetProductMasterList("");
                        var product = (from s in products
                                       where s.product_type_code == "1" || s.product_type_code == "1"
                                       select s);
                        ddlMaterial.DataSource = product.ToList();
                        ddlMaterial.DataTextField = "product_name";
                        ddlMaterial.DataValueField = "product_code";
                        ddlMaterial.DataBind();
                        ddlMaterial.Items.Insert(0, "Select");
                        if (Session["rtype"].ToString() != "0")
                        {
                            MF3_Details mf3 = new MF3_Details();
                            mf3 = BL_MF3_Details.GetDetails(Session["mpid"].ToString(), Session["MF3financial_year"].ToString());
                            txtFinancialYear.Text = mf3.financial_year;
                            CalendarExtender1.SelectedDate = Convert.ToDateTime(mf3.crushing_closedate);
                            txtDate.Text = mf3.crushing_closedate;
                            txtTotalCrushingqty.Text = mf3.cane_crushed_total.ToString();
                            txtTotalSugarqty.Text = mf3.sugar_produced_total.ToString();
                            txtproducedqty.Text = mf3.molasses_produced_total.ToString();
                            txtTotalLifted.Text = mf3.qty_lifted_total.ToString();
                            ddlMaterial.SelectedValue = mf3.product_code;
                            //  txtToalCapacity.Text = mf3.bal_avail_qty_total.ToString();
                            txtRow6.Text = mf3.wagon_load;
                            txtrow7.Text = mf3.preventive_arrangement;
                            grdstorage.DataSource = null;
                            double total = 0;
                            for (int i = 0; i < mf3.storage.Count; i++)
                            {
                                dummyStoragetable.Visible = false;
                                storagedt = (DataTable)ViewState["StorageRecords"];
                                storagedt.Rows.Add(mf3.storage[i].vat_code, mf3.storage[i].vat_name, mf3.storage[i].bal_capacity, mf3.storage[i].molasses_prod_tank_storage_id);
                                grdstorage.DataSource = storagedt;
                                grdstorage.DataBind();
                                total = total + Convert.ToDouble(mf3.storage[i].bal_capacity);
                            }
                            txtToalCapacity.Text = total.ToString();
                            if (Session["rtype"].ToString() == "1" || mf3.record_status != "N")
                            {
                                txtFinancialYear.ReadOnly = true;
                                txtDate.ReadOnly = true;
                                txtTotalCrushingqty.ReadOnly = true;
                                txtTotalSugarqty.ReadOnly = true;
                                txtproducedqty.ReadOnly = true;
                                txtTotalLifted.ReadOnly = true;
                                txtRow6.ReadOnly = true;
                                txtrow7.ReadOnly = true;
                                Image1.Visible = false;
                                btnAdd.Visible = false;
                                btnCancel.Visible = false;
                                btnSaveasDraft.Visible = false;
                                btnSubmit.Visible = false;
                                ddlStorageTankname.Enabled = false;
                                txtStorageQTY.ReadOnly = true;
                                grdstorage.Columns[grdstorage.Columns.Count - 1].Visible = false;
                                ddlMaterial.Enabled = false;
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
        public static string CheckDuplicates(Object value)
        {
            string val = "";
            val = BL_MF3_Details.GetDupValues(value.ToString());
            return val;
        }
        protected void MF2_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("MolassesProvisionalProductionList");
        }
        protected void MF3_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("MF3List");
        }
        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("MF3List");
        }
     
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
               
                storagedt = (DataTable)ViewState["StorageRecords"];
                DataRow[] rowsFiltered = storagedt.Select("vat_code = '" + ddlStorageTankname.SelectedValue + "' "); //1 result
                if (rowsFiltered.Length > 0)
                {
                    ddlStorageTankname.SelectedValue = "Select";
                    txtStorageQTY.Text = "";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append("Values are Already Exists");
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());

                }
                else
                {
               
                    double total2 =Convert.ToDouble(txtproducedqty.Text)- Convert.ToDouble(txtTotalLifted.Text);
                    if (txtToalCapacity.Text == "")
                    {
                        total1.Value = txtStorageQTY.Text;
                        total1.Value = (Convert.ToDouble(txtToalCapacity.Text) + Convert.ToDouble(txtStorageQTY.Text)).ToString();
                    }
                    else
                        total1.Value = (Convert.ToDouble(txtToalCapacity.Text) + Convert.ToDouble(txtStorageQTY.Text)).ToString();
                    if (Convert.ToDouble(total1.Value) <= total2)
                    {
                        dummyStoragetable.Visible = false;
                        storagedt.Rows.Add(ddlStorageTankname.SelectedValue, ddlStorageTankname.SelectedItem.ToString(), txtStorageQTY.Text, "0");
                        grdstorage.DataSource = storagedt;
                        grdstorage.DataBind();
                        if (txtToalCapacity.Text == "")
                            txtToalCapacity.Text = "0";
                        double total = Convert.ToDouble(txtToalCapacity.Text) + Convert.ToDouble(txtStorageQTY.Text);
                        txtToalCapacity.Text = total.ToString();
                        storagetotal.Value = total.ToString();
                        ddlStorageTankname.SelectedValue = "Select";

                        txtStorageQTY.Text = "";
                    }
                    else
                    {
                        txtStorageQTY.Text = "";
                        txtStorageQTY.Focus();
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append("Not  (sl:3)-(sl:4) More than of Available QTY");
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    }
                }
            }
        }
        static string storagedeleted = "";
        protected void lnkRemove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ImageButton lb = (ImageButton)sender;
                GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
                DataTable dt2 = (DataTable)ViewState["StorageRecords"];
                ViewState["CurrentTable"] = dt2;
                int rowID = gvRow.RowIndex;
                if (dt2.Rows[rowID]["molasses_prod_tank_storage_id"].ToString() != "0")
                {
                    if (storagedeleted != "")
                        storagedeleted = storagedeleted + "_" + dt2.Rows[rowID]["molasses_prod_tank_storage_id"].ToString();
                    else
                        storagedeleted = dt2.Rows[rowID]["molasses_prod_tank_storage_id"].ToString();
                }
                double total = Convert.ToDouble(txtToalCapacity.Text) - Convert.ToDouble(dt2.Rows[rowID]["bal_capacity"].ToString());
                txtToalCapacity.Text = total.ToString();
                storagetotal.Value = total.ToString();
                DataTable dt1 = ViewState["StorageRecords"] as DataTable;
                dt1.Rows[rowID].Delete();
                ViewState["dt"] = dt1;
                grdstorage.DataSource = dt1;
                grdstorage.DataBind();
                if (dt1.Rows.Count < 1)
                {
                    dummyStoragetable.Visible = true;
                    grdstorage.Visible = false;
                }

            }
        }

        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                btnSaveasDraft.Enabled = false;
                MF3_Details mf3 = new MF3_Details();
                mf3.financial_year = txtFinancialYear.Text;
                mf3.crushing_closedate = txtDate.Text;
                mf3.cane_crushed_total = Convert.ToDouble(txtTotalCrushingqty.Text);
                mf3.sugar_produced_total= Convert.ToDouble(txtTotalSugarqty.Text);
                mf3.molasses_produced_total= Convert.ToDouble(txtproducedqty.Text);
                mf3.qty_lifted_total= Convert.ToDouble(txtTotalLifted.Text);
                mf3.wagon_load = txtRow6.Text;
                mf3.preventive_arrangement = txtrow7.Text;
                mf3.storage = new List<Molasses_Storage_Details_MF2>();
                mf3.product_code = ddlMaterial.SelectedValue;
                for (int i = 0; i < grdstorage.Rows.Count; i++)
                {
                    Molasses_Storage_Details_MF2 storage = new Molasses_Storage_Details_MF2();
                    GridViewRow row = grdstorage.Rows[i];
                    storage.vat_code = (row.Cells[1].FindControl("lblvatcode") as Label).Text;
                    storage.bal_capacity = (row.Cells[1].FindControl("lblQTY") as TextBox).Text;
                    storage.molasses_prod_tank_storage_id = (row.Cells[1].FindControl("lblid") as Label).Text;
                    storage.deleted_ids = storagedeleted;
                    mf3.storage.Add(storage);
                }
               
                mf3.record_status = "N";
                mf3.user_id = Session["UserID"].ToString();
                mf3.party_code = partycode.Value;
             //   mf3.product_code = ddlMaterial.SelectedValue;
                string val = "";
                if (Session["rtype"].ToString() == "0")
                {
                    val = BL_MF3_Details.Insert(mf3);
                }
                else
                {
                    mf3.molasses_actual_prod_id = Session["mpid"].ToString();
                    val = BL_MF3_Details.Update(mf3);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("MF3List");
                }
                else
                {
                    btnSaveasDraft.Enabled = true;
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
                btnSubmit.Enabled = false;
                MF3_Details mf3 = new MF3_Details();
                mf3.financial_year = txtFinancialYear.Text;
                mf3.crushing_closedate = txtDate.Text;
                mf3.cane_crushed_total = Convert.ToDouble(txtTotalCrushingqty.Text);
                mf3.sugar_produced_total = Convert.ToDouble(txtTotalSugarqty.Text);
                mf3.molasses_produced_total = Convert.ToDouble(txtproducedqty.Text);
                mf3.qty_lifted_total = Convert.ToDouble(txtTotalLifted.Text);
                mf3.wagon_load = txtRow6.Text;
                mf3.preventive_arrangement = txtrow7.Text;
                mf3.storage = new List<Molasses_Storage_Details_MF2>();
                mf3.product_code = ddlMaterial.SelectedValue;
                for (int i = 0; i < grdstorage.Rows.Count; i++)
                {
                    Molasses_Storage_Details_MF2 storage = new Molasses_Storage_Details_MF2();
                    GridViewRow row = grdstorage.Rows[i];
                    storage.vat_code = (row.Cells[1].FindControl("lblvatcode") as Label).Text;
                    storage.bal_capacity = (row.Cells[1].FindControl("lblQTY") as TextBox).Text;
                    storage.molasses_prod_tank_storage_id = (row.Cells[1].FindControl("lblid") as Label).Text;
                    storage.deleted_ids = storagedeleted;
                    mf3.storage.Add(storage);
                }

                mf3.record_status = "Y";
                mf3.user_id = Session["UserID"].ToString();
                mf3.party_code = partycode.Value;
                //   mf3.product_code = ddlMaterial.SelectedValue;
                string val = "";
                if (Session["rtype"].ToString() == "0")
                {
                    val = BL_MF3_Details.Insert(mf3);
                }
                else
                {
                    mf3.molasses_actual_prod_id = Session["mpid"].ToString();
                    val = BL_MF3_Details.Update(mf3);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("MF3List");
                }
                else
                {
                    btnSubmit.Enabled = true;
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
            Response.Redirect("MF3List");
        }

       
    }
}