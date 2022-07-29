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
    public partial class MolassesProvisionalProductionForm1 : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        DataTable distdt = new DataTable();
        DataTable otherdt = new DataTable();
        DataTable storagedt = new DataTable();
        List<VAT_Master> vatmasters = new List<VAT_Master>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    distdeleted = "";
                    otherdeleted = "";
                    storagedeleted = "";
                    //CalendarExtender.EndDate = DateTime.Now;
                    txtLoadingofMolasses.Attributes.Add("maxlength", txtLoadingofMolasses.MaxLength.ToString());

                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];

                    // txtStorageQuantity.ReadOnly = true;
                    txtStorageTotal.ReadOnly = true;
                    txtOtherTotal.ReadOnly = true;
                    txtDistillery.ReadOnly = true;
                    Total7a.Value = "0";
                    Total7b.Value = "0";
                    txtOtherTotal.Text = "0";
                    txtStorageTotal.Text = "0";
                    txtDistillery.Text = "0";

                    //if (strPreviousPage == "")
                    //{
                    //    Response.Redirect("~/LoginPage");
                    //}
                    if (ViewState["Records"] == null)
                    {
                        dt.Columns.Add("vat_code");
                        dt.Columns.Add("vat_name");
                        dt.Columns.Add("vat_totalcapacity");
                        ViewState["Records"] = dt;
                    }
                    if (ViewState["DistRecords"] == null)
                    {
                        distdt.Columns.Add("party_code");
                        distdt.Columns.Add("party_name");
                        distdt.Columns.Add("delivered_year");
                        distdt.Columns.Add("delivered_qty");
                        distdt.Columns.Add("molasses_deliverydetail_id");
                        ViewState["DistRecords"] = distdt;
                    }
                    if (ViewState["OtherRecords"] == null)
                    {
                        otherdt.Columns.Add("other_party1");
                        otherdt.Columns.Add("other_party");
                        otherdt.Columns.Add("delivered_year");
                        otherdt.Columns.Add("delivered_qty");
                        otherdt.Columns.Add("molasses_other_deliverydetail_id");
                        ViewState["OtherRecords"] = otherdt;
                    }
                    if (ViewState["StorageRecords"] == null)
                    {
                        storagedt.Columns.Add("vat_code");
                        storagedt.Columns.Add("vat_name");
                        storagedt.Columns.Add("financial_year");
                        storagedt.Columns.Add("bal_capacity");
                        storagedt.Columns.Add("molasses_prod_tank_storage_id");
                        ViewState["StorageRecords"] = storagedt;
                    }
                    Session["UserID"] = Session["UserID"];
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        txtFinancialYear.Text = user.financial_year;
                        //captiveunit.Value = user.party_captive_unit_name;
                        // txtUnitName.Text = user.party_captive_unit_name;
                        partycode.Value = user.party_code;
                        int year = Convert.ToInt32(user.financial_year.Substring(0, 4));
                        string year3 = (year - 1).ToString() + "-" + year;
                        string year2 = (year - 2).ToString() + "-" + (year - 1).ToString();
                        string year1 = (year - 3).ToString() + "-" + (year - 2).ToString();
                        Year1.Text = year1;
                        Year2.Text = year2;
                        Year3.Text = year3;
                        lbl8Year1.Text = year1;
                        lbl8Year2.Text = year2;
                        lbl8Year3.Text = year3;
                        ddStorageYear.Items.Insert(0, "Select");
                        ddStorageYear.Items.Insert(1, year1);
                        ddStorageYear.Items.Insert(2, year2);
                        ddStorageYear.Items.Insert(3, year3);
                        OtherYear.Items.Insert(0, "Select");
                        OtherYear.Items.Insert(1, year1);
                        OtherYear.Items.Insert(2, year2);
                        OtherYear.Items.Insert(3, year3);
                        ddYear.Items.Insert(0, "Select");
                        ddYear.Items.Insert(1, year1);
                        ddYear.Items.Insert(2, year2);
                        ddYear.Items.Insert(3, year3);
                        txtDate1.Text = DateTime.Now.ToString("dd-MM-yyyy");
                        txtType.Text = "Provisional";
                        List<Party_Master> partymasters = new List<Party_Master>();
                        partymasters = BL_Party_Master.GetList();
                        var list1 = (from s in partymasters
                                     where (s.party_type_code == "DIS" || s.party_type_code == "d01") && s.party_active == "Active" && s.party_code != "All" && s.party_code != "ALL"
                                     select s);
                        partymasters = BL_Party_Master.GetList();



                        ddDistilleryName.Items.Insert(0, "Select");
                        ddDistilleryName.Items.Insert(0, "Select");
                        ddDistilleryName.DataSource = list1.ToList();
                        ddDistilleryName.DataTextField = "Party_name";
                        ddDistilleryName.DataValueField = "party_code";
                        ddDistilleryName.DataBind();
                        ddDistilleryName.Items.Insert(0, "Select");

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
                        vatmasters = new List<VAT_Master>();
                        vatmasters = BL_VATMaster.GetvatmasterList(user.party_code);
                      //  GridView1.DataSource = null;
                        if (Session["rtype"].ToString() == "0" )
                        {
                            for (int i = 0; i < vatmasters.Count; i++)
                            {

                                dt = (DataTable)ViewState["Records"];
                                dt.Rows.Add(vatmasters[i].vat_code, vatmasters[i].vat_name, vatmasters[i].vat_totalcapacity);
                                GridView1.DataSource = dt;
                                GridView1.DataBind();
                            }
                        }
                        ddlTankName.DataSource = vatmasters.ToList();
                        ddlTankName.DataTextField = "vat_name";
                        ddlTankName.DataValueField = "vat_code";
                        ddlTankName.DataBind();
                        ddlTankName.Items.Insert(0, "Select");
                        if (Session["rtype"].ToString() != "0")
                        {
                            Molasses_Production_MF2 mf2 = new Molasses_Production_MF2();
                            mf2 = BL_Molasses_Production_MF2.GetDetails(Session["mpid"].ToString(), Session["MPPfinancial_year"].ToString());
                            ddlCaptive.Enabled = false;
                            //  ddUnitName.Enabled = true;
                            txtFinancialYear.Text = mf2.financial_year;
                            txtDate1.Text = mf2.entry_date;
                            ddlCaptive.SelectedValue = mf2.iscaptive;
                            //  ddUnitName.SelectedValue = mf2.to_party_code;
                            CalendarExtender.SelectedDate = Convert.ToDateTime(mf2.cane_crushing_date);
                            txtDATE.Text = mf2.cane_crushing_date;
                            txtdob.Value = mf2.cane_crushing_date;
                            ddlMaterial.SelectedValue = mf2.product_code;
                            txtmolassesproduction.Text = mf2.molasses_plan_next_season.ToString();
                            txtsugarproduction.Text = mf2.sugar_plan_next_season.ToString();
                            txtQtyMproduceddaily.Text = mf2.molasses_prod_daily.ToString();
                            txtQtySproduceddaily.Text = mf2.sugar_prod_daily.ToString();
                            txtproductionstoredin.Text = mf2.new_prod_storage.ToString();
                            txtLoadingofMolasses.Text = mf2.wagon_loading;
                            txtYear21.Text = mf2.actualprod_prevyr1.ToString();
                            txtYear22.Text = mf2.actualprod_prevyr2.ToString();
                            txtYear23.Text = mf2.actualprod_prevyr3.ToString();
                            txtQtyYear1.Text = mf2.molasses_avail_pyr1.ToString();
                            txtQtyYear2.Text = mf2.molasses_avail_pyr2.ToString();
                            txtQtyYear3.Text = mf2.molasses_avail_pyr3.ToString();
                            txt14.Text = mf2.name_address_applicant;
                            txtQtyYear1.ReadOnly = true;
                            txtQtyYear2.ReadOnly = true;
                            txtQtyYear3.ReadOnly = true;
                            txt10.Text = mf2.why_stock_not_cleared;
                            txt11.Text = mf2.newcrop_storage;
                            txt12.Text = mf2.newcrop_storage_difficulty;
                            txt13.Text = mf2.cleaned_storage;
                            txt14b.Text = mf2.name_address_manager;
                            txt14a.Text = mf2.name_address_occupier;
                            txt15.Text = mf2.mechanical_pump;
                            txtDistillery.Text = mf2.total_molasses_delivered.ToString();
                            Total7a.Value = mf2.total_molasses_delivered.ToString();
                            txtOtherTotal.Text = mf2.other_person_total.ToString();
                            Total7b.Value = mf2.other_person_total.ToString(); ;
                            txtStorageTotal.Text = mf2.total_avail_stock_stored.ToString();
                            storagetotal.Value = mf2.total_avail_stock_stored.ToString();
                            storagetotalcapacity.Value = mf2.total_storage_capacity.ToString();
                            // mf2.vatmaster = vatmasters;
                            for (int i = 0; i < mf2.vatmaster.Count; i++)
                            {

                                dt = (DataTable)ViewState["Records"];
                                dt.Rows.Add(mf2.vatmaster[i].vat_code, mf2.vatmaster[i].vat_name, mf2.vatmaster[i].vat_totalcapacity);
                                GridView1.DataSource = dt;
                                GridView1.DataBind();
                                txtYear21.ReadOnly = true;
                                txtYear22.ReadOnly = true;
                                txtYear23.ReadOnly = true;
                            }
                            for (int i = 0; i < mf2.delevery.Count; i++)
                            {
                                dummygrdDistlleries.Visible = false;
                                distdt = (DataTable)ViewState["DistRecords"];
                                distdt.Rows.Add(mf2.delevery[i].party_code, mf2.delevery[i].party_name, mf2.delevery[i].delivered_year, mf2.delevery[i].delivered_qty, mf2.delevery[i].molasses_deliverydetail_id);
                                grdDistlleries.DataSource = distdt;
                                grdDistlleries.DataBind();
                                txtYear21.ReadOnly = true;
                                txtYear22.ReadOnly = true;
                                txtYear23.ReadOnly = true;

                            }
                            for (int i = 0; i < mf2.other.Count; i++)
                            {
                                dummyOthertable.Visible = false;
                                otherdt = (DataTable)ViewState["OtherRecords"];
                                otherdt.Rows.Add(mf2.other[i].other_party, mf2.other[i].other_party, mf2.other[i].delivered_year, mf2.other[i].delivered_qty, mf2.other[i].molasses_other_deliverydetail_id);
                                grdOther.DataSource = otherdt;
                                grdOther.DataBind();

                            }
                            for (int i = 0; i < mf2.storage.Count; i++)
                            {
                                dummyStoragetable.Visible = false;
                                storagedt = (DataTable)ViewState["StorageRecords"];
                                storagedt.Rows.Add(mf2.storage[i].vat_code, mf2.storage[i].vat_name, mf2.storage[i].financial_year, mf2.storage[i].bal_capacity, mf2.storage[i].molasses_prod_tank_storage_id);
                                grdStorage.DataSource = storagedt;
                                grdStorage.DataBind();

                            }
                            if (mf2.record_status != "N" || Session["rtype"].ToString() == "1")
                            {
                                // Image1.Visible = false;
                                txtFinancialYear.ReadOnly = true;
                                txtDate1.ReadOnly = true;
                                ddlCaptive.Enabled = false;
                                //  ddUnitName.Enabled = true;

                                ddlMaterial.Enabled = false;
                                txtmolassesproduction.ReadOnly = true;
                                txtsugarproduction.ReadOnly = true;
                                txtQtyMproduceddaily.ReadOnly = true;
                                txtQtySproduceddaily.ReadOnly = true;
                                txtproductionstoredin.ReadOnly = true;
                                txtLoadingofMolasses.ReadOnly = true;
                                txtYear21.ReadOnly = true;
                                txtYear22.ReadOnly = true;
                                txtYear23.ReadOnly = true;
                                txtQtyYear1.ReadOnly = true;
                                txtQtyYear2.ReadOnly = true;
                                txtQtyYear3.ReadOnly = true;
                                txt10.ReadOnly = true;
                                txt11.ReadOnly = true;
                                txt12.ReadOnly = true;
                                txt13.ReadOnly = true;
                                txt14.ReadOnly = true;
                                txt14b.ReadOnly = true;
                                txt14a.ReadOnly = true;
                                txt15.ReadOnly = true;
                                btnCancel.Visible = false;
                                btnSaveasDraft.Visible = false;
                                btnStorageAdd.Visible = false;
                                btnSubmit.Visible = false;
                                btnOther.Visible = false;
                                btnStorageAdd.Visible = false;
                                grdDistlleries.Columns[grdDistlleries.Columns.Count - 1].Visible = false;
                                grdOther.Columns[grdOther.Columns.Count - 1].Visible = false;
                                grdStorage.Columns[grdStorage.Columns.Count - 1].Visible = false;
                                btnDistAdd.Visible = false;
                                ddDistilleryName.Enabled = false;
                                ddYear.Enabled = false;
                                txtQuantity.ReadOnly = true;
                                txtDistilleryName.Enabled = false;
                                OtherYear.Enabled = false;
                                txtQuantity2.ReadOnly = true;
                                txtYear21.ReadOnly = true;
                                txtYear22.ReadOnly = true;
                                txtYear22.ReadOnly = true;
                                ddlTankName.Enabled = false;
                                ddStorageYear.Enabled = false;
                                txtStorageQuantity.ReadOnly = true;
                                txtQtyYear1.ReadOnly = true;
                                txtQtyYear2.ReadOnly = true;
                                txtQtyYear3.ReadOnly = true;
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
                else
                {
                    Response.Redirect("~/LoginPage");
                }
            }
        }
        [WebMethod]
        public static string CheckDuplicates(Object value)
        {
            string val = "";
            val = BL_Molasses_Production_MF2.GetValues(value.ToString());
            return val;
        }
        protected void AddDist(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
               
                distdt = (DataTable)ViewState["DistRecords"];
                // DataTable Id1 = new DataTable();
                DataRow[] rowsFiltered = distdt.Select("party_code = '" + ddDistilleryName.SelectedValue + "' and delivered_year = '" + ddYear.SelectedItem.ToString() + "'"); //1 result
           
                if (rowsFiltered.Length > 0)
                {
                    ddDistilleryName.SelectedValue = "Select";
                    ddYear.SelectedValue = "Select";
                    txtQuantity.Text = "";
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
                    if (lbl8Year1.Text == ddYear.SelectedValue)
                    {
                        double balance = Convert.ToDouble(txtQtyYear1.Text) - Convert.ToDouble(txtQuantity.Text);
                        
                        if (balance < 0)
                        {
                           
                            txtQuantity.Text = "";
                            txtQuantity.Focus();
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append("<script type = 'text/javascript'>");
                            sb.Append("window.onload=function(){");
                            sb.Append("alert('");
                            sb.Append("QTY not Morethan of " + Year1.Text + " Production qty");
                            sb.Append("')};");
                            sb.Append("</script>");
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());

                        }
                        else
                        {
                            txtQtyYear1.Text = balance.ToString();
                            txtYear21.ReadOnly = true;
                            dummygrdDistlleries.Visible = false;
                            distdt.Rows.Add(ddDistilleryName.SelectedValue, ddDistilleryName.SelectedItem.ToString(), ddYear.SelectedItem, txtQuantity.Text, "0");
                            grdDistlleries.DataSource = distdt;
                            grdDistlleries.DataBind();
                            if (txtDistillery.Text == "")
                                txtDistillery.Text = "0";
                            double total = Convert.ToDouble(txtDistillery.Text) + Convert.ToDouble(txtQuantity.Text);
                            txtDistillery.Text = total.ToString();
                            txtQtyYear1.ReadOnly = true;
                            txtQtyYear2.ReadOnly = true;
                            txtQtyYear3.ReadOnly = true;
                            if (grdDistlleries.Rows.Count > 0)
                                grdDistlleries.Visible = true;
                            ddDistilleryName.SelectedValue = "Select";
                            ddYear.SelectedValue = "Select";
                            txtQuantity.Text = "";
                        }
                    }
                  else  if (lbl8Year2.Text == ddYear.SelectedValue)
                    {
                        double balance = Convert.ToDouble(txtQtyYear2.Text) - Convert.ToDouble(txtQuantity.Text);
                      
                        if (balance < 0)
                        {
                            txtQuantity.Text = "";
                            txtQuantity.Focus();
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append("<script type = 'text/javascript'>");
                            sb.Append("window.onload=function(){");
                            sb.Append("alert('");
                            sb.Append("QTY not Morethan of " + Year2.Text + " Production qty");
                            sb.Append("')};");
                            sb.Append("</script>");
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());

                        }
                        else
                        {
                            txtQtyYear2.Text = balance.ToString();
                            txtYear22.ReadOnly = true;
                            dummygrdDistlleries.Visible = false;
                            distdt.Rows.Add(ddDistilleryName.SelectedValue, ddDistilleryName.SelectedItem.ToString(), ddYear.SelectedItem, txtQuantity.Text, "0");
                            grdDistlleries.DataSource = distdt;
                            grdDistlleries.DataBind();
                            if (txtDistillery.Text == "")
                                txtDistillery.Text = "0";
                            double total = Convert.ToDouble(txtDistillery.Text) + Convert.ToDouble(txtQuantity.Text);
                            txtDistillery.Text = total.ToString();
                            txtQtyYear1.ReadOnly = true;
                            txtQtyYear2.ReadOnly = true;
                            txtQtyYear3.ReadOnly = true;
                            if (grdDistlleries.Rows.Count > 0)
                                grdDistlleries.Visible = true;
                            ddDistilleryName.SelectedValue = "Select";
                            ddYear.SelectedValue = "Select";
                            txtQuantity.Text = "";
                        }
                        //  txtQtyYear2.ReadOnly = true;
                    }
                  else  if (lbl8Year3.Text == ddYear.SelectedValue)
                    {
                        double balance = Convert.ToDouble(txtQtyYear3.Text) - Convert.ToDouble(txtQuantity.Text);
                    
                        if (balance < 0)
                        {
                           // txtQtyYear3.Text = balance.ToString();
                            txtQuantity.Text = "";
                            txtQuantity.Focus();
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append("<script type = 'text/javascript'>");
                            sb.Append("window.onload=function(){");
                            sb.Append("alert('");
                            sb.Append("QTY not Morethan of " + Year3.Text + " Production qty");
                            sb.Append("')};");
                            sb.Append("</script>");
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());

                        }
                        else
                        {
                            txtQtyYear3.Text = balance.ToString();
                            dummygrdDistlleries.Visible = false;
                            txtYear23.ReadOnly = true;
                            distdt.Rows.Add(ddDistilleryName.SelectedValue, ddDistilleryName.SelectedItem.ToString(), ddYear.SelectedItem, txtQuantity.Text, "0");
                            grdDistlleries.DataSource = distdt;
                            grdDistlleries.DataBind();
                            if (txtDistillery.Text == "")
                                txtDistillery.Text = "0";
                            double total = Convert.ToDouble(txtDistillery.Text) + Convert.ToDouble(txtQuantity.Text);
                            txtDistillery.Text = total.ToString();
                            txtQtyYear1.ReadOnly = true;
                            txtQtyYear2.ReadOnly = true;
                            txtQtyYear3.ReadOnly = true;
                           // grdDistlleries.Visible = true;
                            ddDistilleryName.SelectedValue = "Select";
                            ddYear.SelectedValue = "Select";
                            txtQuantity.Text = "";
                            if (grdDistlleries.Rows.Count > 0)
                                grdDistlleries.Visible = true;
                        }
                        // txtQtyYear3.ReadOnly = true;
                    }
                    
                }
            }

        }
        protected void AddOther(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
               
                otherdt = (DataTable)ViewState["OtherRecords"];
                DataRow[] rowsFiltered = otherdt.Select("other_party = '" + txtDistilleryName.Text + "' and delivered_year = '" + OtherYear.SelectedItem.ToString() + "'"); //1 result
                
               if (rowsFiltered.Length > 0)
                {
                    txtDistilleryName.Text = "";
                    OtherYear.SelectedValue = "Select";
                    txtQuantity2.Text = "";
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
                    if (lbl8Year1.Text == OtherYear.SelectedValue)
                    {
                        double balance = Convert.ToDouble(txtQtyYear1.Text) - Convert.ToDouble(txtQuantity2.Text);
                      
                        if (balance< 0)
                        {
                            txtQuantity2.Text = "";
                            txtQuantity2.Focus();
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append("<script type = 'text/javascript'>");
                            sb.Append("window.onload=function(){");
                            sb.Append("alert('");
                            sb.Append("QTY not Morethan of " + Year1.Text + " Production qty");
                            sb.Append("')};");
                            sb.Append("</script>");
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());

                        }
                        else
                        {
                            txtQtyYear1.Text = balance.ToString();
                            txtYear21.ReadOnly = true;
                            dummyOthertable.Visible = false;
                            otherdt.Rows.Add(txtDistilleryName.Text, txtDistilleryName.Text, OtherYear.SelectedItem, txtQuantity2.Text, "0");
                            grdOther.DataSource = otherdt;
                            grdOther.DataBind();
                            if (txtOtherTotal.Text == "")
                                txtOtherTotal.Text = "0";
                            double total = Convert.ToDouble(txtOtherTotal.Text) + Convert.ToDouble(txtQuantity2.Text);
                            txtOtherTotal.Text = total.ToString();
                            Total7a.Value = Total7a.ToString();
                            txtQtyYear1.ReadOnly = true;
                            txtQtyYear2.ReadOnly = true;
                            txtQtyYear3.ReadOnly = true;
                            if (grdOther.Rows.Count > 0)
                                grdOther.Visible = true;
                            //if (lbl8Year1.Text == OtherYear.SelectedValue)
                            //{
                            //    double balance = Convert.ToDouble(txtQtyYear1.Text) - Convert.ToDouble(txtQuantity2.Text);
                            //    txtQtyYear1.Text = balance.ToString();
                            //}
                            //if (lbl8Year2.Text == OtherYear.SelectedValue)
                            //{
                            //    double balance = Convert.ToDouble(txtQtyYear2.Text) - Convert.ToDouble(txtQuantity2.Text);
                            //    txtQtyYear2.Text = balance.ToString();
                            //}
                            //if (lbl8Year3.Text == OtherYear.SelectedValue)
                            //{
                            //    double balance = Convert.ToDouble(txtQtyYear3.Text) - Convert.ToDouble(txtQuantity2.Text);
                            //    txtQtyYear3.Text = balance.ToString();
                            //}
                            txtDistilleryName.Text = "";
                            OtherYear.SelectedValue = "Select";
                            txtQuantity2.Text = "";
                        }
                    }
                  else  if (lbl8Year2.Text == OtherYear.SelectedValue)
                    {
                        double balance = Convert.ToDouble(txtQtyYear2.Text) - Convert.ToDouble(txtQuantity2.Text);
                      
                        if (balance< 0)
                        {
                            // ddDistilleryName.SelectedValue = "Select";
                            //   ddYear.SelectedValue = "Select";
                            txtQuantity2.Text = "";
                            txtQuantity2.Focus();
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append("<script type = 'text/javascript'>");
                            sb.Append("window.onload=function(){");
                            sb.Append("alert('");
                            sb.Append("QTY not Morethan of " + Year2.Text + " Production qty");
                            sb.Append("')};");
                            sb.Append("</script>");
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());

                        }
                        else
                        {
                            txtQtyYear2.Text = balance.ToString();
                            txtYear22.ReadOnly = true;
                            dummyOthertable.Visible = false;
                            otherdt.Rows.Add(txtDistilleryName.Text, txtDistilleryName.Text, OtherYear.SelectedItem, txtQuantity2.Text, "0");
                            grdOther.DataSource = otherdt;
                            grdOther.DataBind();
                            if (txtOtherTotal.Text == "")
                                txtOtherTotal.Text = "0";
                            double total = Convert.ToDouble(txtOtherTotal.Text) + Convert.ToDouble(txtQuantity2.Text);
                            txtOtherTotal.Text = total.ToString();
                            Total7a.Value = Total7a.ToString();
                            txtQtyYear1.ReadOnly = true;
                            txtQtyYear2.ReadOnly = true;
                            txtQtyYear3.ReadOnly = true;
                            if (grdOther.Rows.Count > 0)
                                grdOther.Visible = true;
                            //if (lbl8Year1.Text == OtherYear.SelectedValue)
                            //{
                            //    double balance = Convert.ToDouble(txtQtyYear1.Text) - Convert.ToDouble(txtQuantity2.Text);
                            //    txtQtyYear1.Text = balance.ToString();
                            //}
                            //if (lbl8Year2.Text == OtherYear.SelectedValue)
                            //{
                            //    double balance = Convert.ToDouble(txtQtyYear2.Text) - Convert.ToDouble(txtQuantity2.Text);
                            //    txtQtyYear2.Text = balance.ToString();
                            //}
                            //if (lbl8Year3.Text == OtherYear.SelectedValue)
                            //{
                            //    double balance = Convert.ToDouble(txtQtyYear3.Text) - Convert.ToDouble(txtQuantity2.Text);
                            //    txtQtyYear3.Text = balance.ToString();
                            //}
                            txtDistilleryName.Text = "";
                            OtherYear.SelectedValue = "Select";
                            txtQuantity2.Text = "";
                        }
                        //  txtQtyYear2.ReadOnly = true;
                    }
                   else if (lbl8Year3.Text == OtherYear.SelectedValue)
                    {
                        double balance = Convert.ToDouble(txtQtyYear3.Text) - Convert.ToDouble(txtQuantity2.Text);
                      
                        if (balance < 0)
                        {
                            txtQuantity2.Text = "";
                            txtQuantity2.Focus();
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append("<script type = 'text/javascript'>");
                            sb.Append("window.onload=function(){");
                            sb.Append("alert('");
                            sb.Append("QTY not Morethan of " + Year3.Text + " Production qty");
                            sb.Append("')};");
                            sb.Append("</script>");
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());

                        }
                        else
                        {
                            txtQtyYear3.Text = balance.ToString(); 
                            dummyOthertable.Visible = false;
                            txtYear23.ReadOnly = true;
                            otherdt.Rows.Add(txtDistilleryName.Text, txtDistilleryName.Text, OtherYear.SelectedItem, txtQuantity2.Text, "0");
                            grdOther.DataSource = otherdt;
                            grdOther.DataBind();
                            if (txtOtherTotal.Text == "")
                                txtOtherTotal.Text = "0";
                            double total = Convert.ToDouble(txtOtherTotal.Text) + Convert.ToDouble(txtQuantity2.Text);
                            txtOtherTotal.Text = total.ToString();
                            Total7a.Value = total.ToString();
                            txtQtyYear1.ReadOnly = true;
                            txtQtyYear2.ReadOnly = true;
                            txtQtyYear3.ReadOnly = true;
                            if (grdOther.Rows.Count > 0)
                                grdOther.Visible = true;
                            //if (lbl8Year1.Text == OtherYear.SelectedValue)
                            //{
                            //    double balance = Convert.ToDouble(txtQtyYear1.Text) - Convert.ToDouble(txtQuantity2.Text);
                            //    txtQtyYear1.Text = balance.ToString();
                            //}
                            //if (lbl8Year2.Text == OtherYear.SelectedValue)
                            //{
                            //    double balance = Convert.ToDouble(txtQtyYear2.Text) - Convert.ToDouble(txtQuantity2.Text);
                            //    txtQtyYear2.Text = balance.ToString();
                            //}
                            //if (lbl8Year3.Text == OtherYear.SelectedValue)
                            //{
                            //    double balance = Convert.ToDouble(txtQtyYear3.Text) - Convert.ToDouble(txtQuantity2.Text);
                            //    txtQtyYear3.Text = balance.ToString();
                            //}
                            txtDistilleryName.Text = "";
                            OtherYear.SelectedValue = "Select";
                            txtQuantity2.Text = "";
                        }
                        // txtQtyYear3.ReadOnly = true;
                    }
                   
                }
            }

        }
        
        protected void AddStorage(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
               
                storagedt = (DataTable)ViewState["StorageRecords"];
                DataRow[] rowsFiltered = storagedt.Select("vat_code = '" + ddlTankName.SelectedValue + "' and financial_year = '" + ddStorageYear.SelectedItem.ToString() + "'"); //1 result
               
               if (rowsFiltered.Length >0)
                {
                    ddlTankName.SelectedValue = "Select";
                    ddStorageYear.SelectedValue = "Select";
                    txtStorageQuantity.Text = "";
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
                    
                        if (lbl8Year1.Text == ddStorageYear.SelectedValue)
                        {
                            double balance = Convert.ToDouble(txtQtyYear1.Text) - Convert.ToDouble(txtStorageQuantity.Text);

                        if (balance < 0 || Convert.ToDouble(txtStorageQuantity.Text) == 0)
                        {

                            txtStorageQuantity.Text = "";
                            txtStorageQuantity.Focus();
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append("<script type = 'text/javascript'>");
                            sb.Append("window.onload=function(){");
                            sb.Append("alert('");
                            sb.Append("QTY not Morethan of " + Year1.Text + " Production qty");
                            sb.Append("')};");
                            sb.Append("</script>");
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());

                        }
                        else
                        {
                            txtQtyYear1.Text = balance.ToString();
                            txtYear21.ReadOnly = true;
                            dummyStoragetable.Visible = false;
                            storagedt.Rows.Add(ddlTankName.SelectedValue, ddlTankName.SelectedItem.ToString(), ddStorageYear.SelectedItem, txtStorageQuantity.Text, "0");
                            grdStorage.DataSource = storagedt;
                            grdStorage.DataBind();
                            if (txtStorageTotal.Text == "")
                                txtStorageTotal.Text = "0";
                            double total = Convert.ToDouble(txtStorageTotal.Text) + Convert.ToDouble(txtStorageQuantity.Text);
                            txtStorageTotal.Text = total.ToString();
                            storagetotal.Value = total.ToString();
                            ddlTankName.SelectedValue = "Select";
                            ddStorageYear.SelectedValue = "Select";
                            txtStorageQuantity.Text = "";
                            if (grdStorage.Rows.Count > 0)
                                grdStorage.Visible = true;
                            ddlTankName.SelectedValue = "Select";
                            ddStorageYear.SelectedValue = "Select";
                            txtStorageQuantity.Text = "";
                        }
                        }
                        else if (lbl8Year2.Text == ddStorageYear.SelectedValue)
                        {
                        double balance = Convert.ToDouble(txtQtyYear2.Text) - Convert.ToDouble(txtStorageQuantity.Text);

                        if (balance < 0 || Convert.ToDouble(txtStorageQuantity.Text) == 0)
                        {
                            txtStorageQuantity.Text = "";
                            txtStorageQuantity.Focus();
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append("<script type = 'text/javascript'>");
                                sb.Append("window.onload=function(){");
                                sb.Append("alert('");
                                sb.Append("QTY not Morethan of " + Year2.Text + " Production qty");
                                sb.Append("')};");
                                sb.Append("</script>");
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());

                            }
                            else
                            {
                                txtQtyYear2.Text = balance.ToString();
                                txtYear22.ReadOnly = true;
                            dummyStoragetable.Visible = false;
                            storagedt.Rows.Add(ddlTankName.SelectedValue, ddlTankName.SelectedItem.ToString(), ddStorageYear.SelectedItem, txtStorageQuantity.Text, "0");
                            grdStorage.DataSource = storagedt;
                            grdStorage.DataBind();
                            if (txtStorageTotal.Text == "")
                                txtStorageTotal.Text = "0";
                            double total = Convert.ToDouble(txtStorageTotal.Text) + Convert.ToDouble(txtStorageQuantity.Text);
                            txtStorageTotal.Text = total.ToString();
                            storagetotal.Value = total.ToString();
                            ddlTankName.SelectedValue = "Select";
                            ddStorageYear.SelectedValue = "Select";
                            txtStorageQuantity.Text = "";
                            if (grdStorage.Rows.Count > 0)
                                grdStorage.Visible = true;
                            ddlTankName.SelectedValue = "Select";
                            ddStorageYear.SelectedValue = "Select";
                            txtStorageQuantity.Text = "";
                        }
                            //  txtQtyYear2.ReadOnly = true;
                        }
                        else if (lbl8Year3.Text == ddStorageYear.SelectedValue)
                        {
                            double balance = Convert.ToDouble(txtQtyYear3.Text) - Convert.ToDouble(txtStorageQuantity.Text);

                        if (balance < 0 || Convert.ToDouble(txtStorageQuantity.Text) == 0)
                        {
                            // txtQtyYear3.Text = balance.ToString();
                            txtStorageQuantity.Text = "";
                            txtStorageQuantity.Focus();
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append("<script type = 'text/javascript'>");
                                sb.Append("window.onload=function(){");
                                sb.Append("alert('");
                                sb.Append("QTY not Morethan of " + Year3.Text + " Production qty");
                                sb.Append("')};");
                                sb.Append("</script>");
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());

                            }
                            else
                            {
                                txtQtyYear3.Text = balance.ToString();
                            dummyStoragetable.Visible = false;
                            storagedt.Rows.Add(ddlTankName.SelectedValue, ddlTankName.SelectedItem.ToString(), ddStorageYear.SelectedItem, txtStorageQuantity.Text, "0");
                            grdStorage.DataSource = storagedt;
                            grdStorage.DataBind();
                            if (txtStorageTotal.Text == "")
                                txtStorageTotal.Text = "0";
                            double total = Convert.ToDouble(txtStorageTotal.Text) + Convert.ToDouble(txtStorageQuantity.Text);
                            txtStorageTotal.Text = total.ToString();
                            storagetotal.Value = total.ToString();
                            ddlTankName.SelectedValue = "Select";
                            ddStorageYear.SelectedValue = "Select";
                            txtStorageQuantity.Text = "";
                            if (grdStorage.Rows.Count > 0)
                                grdStorage.Visible = true;
                            ddlTankName.SelectedValue = "Select";
                            ddStorageYear.SelectedValue = "Select";
                            txtStorageQuantity.Text = "";
                        }
                            // txtQtyYear3.ReadOnly = true;
                        }

                  
                    //if (Convert.ToDouble(StorageQuantity.Value) <= 0)
                    //{
                    //    ddlTankName.SelectedValue = "Select";
                    //    ddStorageYear.SelectedValue = "Select";
                    //    txtStorageQuantity.Text = "";
                    //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    //    sb.Append("<script type = 'text/javascript'>");
                    //    sb.Append("window.onload=function(){");
                    //    sb.Append("alert('");
                    //    sb.Append("Lessthan zero/zero QTY Not Allowed ");
                    //    sb.Append("')};");
                    //    sb.Append("</script>");
                    //    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    //}
                    //else
                    //{
                    //    dummyStoragetable.Visible = false;
                    //    storagedt.Rows.Add(ddlTankName.SelectedValue, ddlTankName.SelectedItem.ToString(), ddStorageYear.SelectedItem, StorageQuantity.Value, "0");
                    //    grdStorage.DataSource = storagedt;
                    //    grdStorage.DataBind();
                    //    if (txtStorageTotal.Text == "")
                    //        txtStorageTotal.Text = "0";
                    //    double total = Convert.ToDouble(txtStorageTotal.Text) + Convert.ToDouble(StorageQuantity.Value);
                    //    txtStorageTotal.Text = total.ToString();
                    //    storagetotal.Value = total.ToString();
                    //    ddlTankName.SelectedValue = "Select";
                    //    ddStorageYear.SelectedValue = "Select";
                    //    txtStorageQuantity.Text = "";
                    //    if (grdStorage.Rows.Count > 0)
                    //        grdStorage.Visible = true;
                    //}
                }
            }

        }
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                LinkButton lb = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
                DataTable dt2 = (DataTable)ViewState["Records"];
                ViewState["CurrentTable"] = dt2;
                int rowID = gvRow.RowIndex;
                DataTable dt1 = ViewState["Records"] as DataTable;
                dt1.Rows[rowID].Delete();
                ViewState["dt"] = dt1;
                GridView1.DataSource = dt1;
                GridView1.DataBind();
                //if (dt1.Rows.Count < 1)
                //    dummytable.Visible = true;
            }
        }
        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("MolassesProvisionalProductionList");
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


        protected void btnDistRemove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ImageButton lb = (ImageButton)sender;
                GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
                DataTable dt2 = (DataTable)ViewState["DistRecords"];
                ViewState["CurrentTable"] = dt2;
                int rowID = gvRow.RowIndex;
                if (dt2.Rows[rowID]["molasses_deliverydetail_id"].ToString() != "0")
                {
                    if (distdeleted != "")
                        distdeleted = distdeleted + "_" + dt2.Rows[rowID]["molasses_deliverydetail_id"].ToString();
                    else
                        distdeleted = dt2.Rows[rowID]["molasses_deliverydetail_id"].ToString();
                }
                string year = dt2.Rows[rowID]["delivered_year"].ToString();
                if (lbl8Year1.Text == year)
                {
                    double balance = Convert.ToDouble(txtQtyYear1.Text) + Convert.ToDouble(dt2.Rows[rowID]["delivered_qty"].ToString());
                    if (balance > Convert.ToDouble(txtQtyYear1.Text))
                    {
                        txtQtyYear1.Text = txtYear21.Text;
                    }
                    else
                    {
                        txtQtyYear1.Text = balance.ToString();
                    }
                }
                if (lbl8Year2.Text == year)
                {
                    double balance = Convert.ToDouble(txtQtyYear2.Text) + Convert.ToDouble(dt2.Rows[rowID]["delivered_qty"].ToString());
                    if (balance > Convert.ToDouble(txtQtyYear2.Text))
                    {
                        txtQtyYear2.Text = txtYear22.Text;
                    }
                    else
                    {
                        txtQtyYear2.Text = balance.ToString();
                    }
                }
                if (lbl8Year3.Text == year)
                {
                    double balance = Convert.ToDouble(txtQtyYear3.Text) + Convert.ToDouble(dt2.Rows[rowID]["delivered_qty"].ToString());
                    if (balance > Convert.ToDouble(txtQtyYear3.Text))
                    {
                        txtQtyYear3.Text = txtYear23.Text;
                    }
                    else
                    {
                        txtQtyYear3.Text = balance.ToString();
                    }
                }
                double total = Convert.ToDouble(txtDistillery.Text) - Convert.ToDouble(dt2.Rows[rowID]["delivered_qty"].ToString());
                
                if (total < 0)
                {
                   
                    total = 0;
                    txtDistillery.Text = total.ToString();
                }
                else
                {
                    txtDistillery.Text = total.ToString();
                }
                Total7a.Value = total.ToString();
                DataTable dt1 = ViewState["DistRecords"] as DataTable;
                dt1.Rows[rowID].Delete();
                ViewState["dt"] = dt1;
                grdDistlleries.DataSource = dt1;
                
                grdDistlleries.DataBind();
                if (dt1.Rows.Count <1)
                {
                    dummygrdDistlleries.Visible = true;
                    grdDistlleries.Visible = false;
                }
            }
        }

        protected void btnOtherRemove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ImageButton lb = (ImageButton)sender;
                GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
                DataTable dt2 = (DataTable)ViewState["OtherRecords"];
                ViewState["CurrentTable"] = dt2;
                int rowID = gvRow.RowIndex;
                if (dt2.Rows[rowID]["molasses_other_deliverydetail_id"].ToString() != "0")
                {
                    if (otherdeleted != "")
                        otherdeleted = otherdeleted + "_" + dt2.Rows[rowID]["molasses_other_deliverydetail_id"].ToString();
                    else
                        otherdeleted = dt2.Rows[rowID]["molasses_other_deliverydetail_id"].ToString();
                }
                DataTable dt1 = ViewState["OtherRecords"] as DataTable;
                string year = dt2.Rows[rowID]["delivered_year"].ToString();
                if (lbl8Year1.Text == year)
                {
                    double balance = Convert.ToDouble(txtQtyYear1.Text) +Convert.ToDouble(dt2.Rows[rowID]["delivered_qty"].ToString());
                    if (balance > Convert.ToDouble(txtQtyYear1.Text))
                    {
                        txtQtyYear1.Text = txtYear23.Text;
                    }
                    else
                    {
                        txtQtyYear1.Text = balance.ToString();
                    }
                }
                if (lbl8Year2.Text == year)
                {
                    double balance = Convert.ToDouble(txtQtyYear2.Text) + Convert.ToDouble(dt2.Rows[rowID]["delivered_qty"].ToString());
                    if (balance > Convert.ToDouble(txtQtyYear2.Text))
                    {
                        txtQtyYear2.Text = txtYear22.Text;
                    }
                    else
                    {
                        txtQtyYear2.Text = balance.ToString();
                    }
                }
                if (lbl8Year3.Text == year)
                {
                    double balance = Convert.ToDouble(txtQtyYear3.Text) + Convert.ToDouble(dt2.Rows[rowID]["delivered_qty"].ToString());
                    if (balance > Convert.ToDouble(txtQtyYear3.Text))
                    {
                        txtQtyYear3.Text = txtYear23.Text;
                    }
                    else
                    {
                        txtQtyYear3.Text = balance.ToString();
                    }
                }
                double total = Convert.ToDouble(txtOtherTotal.Text) - Convert.ToDouble(dt2.Rows[rowID]["delivered_qty"].ToString());
              
                if(total<0)
                {
                    total = 0;
                }
                txtOtherTotal.Text = total.ToString();
                Total7b.Value = total.ToString();
                dt1.Rows[rowID].Delete();
                ViewState["dt"] = dt1;
                grdOther.DataSource = dt1;
                grdOther.DataBind();

                if (dt1.Rows.Count <1)
                {
                    dummyOthertable.Visible = true;
                    grdOther.Visible = false;
                }
            }
        }

        protected void btnStorageRemove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ImageButton lb = (ImageButton)sender;
                GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
                DataTable dt2 = (DataTable)ViewState["StorageRecords"];
                ViewState["CurrentTable"] = dt2;
                int rowID = gvRow.RowIndex;
                if (dt2.Rows[rowID]["molasses_prod_tank_storage_id"].ToString()!="0")
                {
                    if(storagedeleted!="")
                    storagedeleted = storagedeleted + "_"+dt2.Rows[rowID]["molasses_prod_tank_storage_id"].ToString();
                    else
                        storagedeleted =dt2.Rows[rowID]["molasses_prod_tank_storage_id"].ToString();
                }
                double total = Convert.ToDouble(txtStorageTotal.Text) - Convert.ToDouble(dt2.Rows[rowID]["bal_capacity"].ToString());
                if(total<0)
                {
                    total = 0;
                }
                string year = dt2.Rows[rowID]["financial_year"].ToString();
                if (lbl8Year1.Text == year)
                {
                    double balance = Convert.ToDouble(txtQtyYear1.Text) + Convert.ToDouble(dt2.Rows[rowID]["bal_capacity"].ToString());
                    if (balance < Convert.ToDouble(txtQtyYear1.Text))
                    {
                        txtQtyYear1.Text = txtYear21.Text;
                    }
                    else
                    {
                        txtQtyYear1.Text = balance.ToString();
                    }
                }
                if (lbl8Year2.Text == year)
                {
                    double balance = Convert.ToDouble(txtQtyYear2.Text) + Convert.ToDouble(dt2.Rows[rowID]["bal_capacity"].ToString());
                    if (balance < Convert.ToDouble(txtQtyYear2.Text))
                    {
                        txtQtyYear2.Text = txtYear22.Text;
                    }
                    else
                    {
                        txtQtyYear2.Text = balance.ToString();
                    }
                }
                if (lbl8Year3.Text == year)
                {
                    double balance = Convert.ToDouble(txtQtyYear3.Text) + Convert.ToDouble(dt2.Rows[rowID]["bal_capacity"].ToString());
                    if (balance < Convert.ToDouble(txtQtyYear3.Text))
                    {
                        txtQtyYear3.Text = txtYear23.Text;
                    }
                    else
                    {
                        txtQtyYear3.Text = balance.ToString();
                    }
                }
               // double total = Convert.ToDouble(txtDistillery.Text) - Convert.ToDouble(dt2.Rows[rowID]["delivered_qty"].ToString());

                if (total < 0)
                {

                    total = 0;
                    txtStorageTotal.Text = total.ToString();
                }
                else
                {
                    txtStorageTotal.Text = total.ToString();
                }
               // txtStorageTotal.Text = total.ToString();
                storagetotal.Value = total.ToString();
                DataTable dt1 = ViewState["StorageRecords"] as DataTable;
                dt1.Rows[rowID].Delete();
                ViewState["dt"] = dt1;
                grdStorage.DataSource = dt1;
                grdStorage.DataBind();
                if (dt1.Rows.Count <1)
                {
                    dummyStoragetable.Visible = true;
                    grdStorage.Visible = false;
                }
               
            }
        }
        public static string distdeleted = "";
        public static string otherdeleted = "";
        public static string storagedeleted = "";
        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                btnSaveasDraft.Enabled = false;
                Molasses_Production_MF2 mf2 = new Molasses_Production_MF2();
                mf2.financial_year = txtFinancialYear.Text;
                mf2.entry_date = txtDate1.Text;
                mf2.iscaptive = ddlCaptive.SelectedValue;
              //  mf2.to_party_code = captiveunit.Value;
                mf2.cane_crushing_date = txtdob.Value;
              
                mf2.molasses_plan_next_season = Convert.ToDouble(txtmolassesproduction.Text);
                mf2.sugar_plan_next_season = Convert.ToDouble(txtsugarproduction.Text);
               
                mf2.molasses_prod_daily= Convert.ToDouble(txtQtyMproduceddaily.Text);
                mf2.sugar_prod_daily = Convert.ToDouble(txtQtySproduceddaily.Text);
                mf2.new_prod_storage = Convert.ToDouble(txtproductionstoredin.Text);
                 mf2.wagon_loading = txtLoadingofMolasses.Text;
                mf2.actualprod_prevyr1 = Convert.ToDouble(txtYear21.Text);
                mf2.actualprod_prevyr2 = Convert.ToDouble(txtYear22.Text);
                mf2.actualprod_prevyr3 = Convert.ToDouble(txtYear23.Text);
                mf2.molasses_avail_pyr1 = Convert.ToDouble(txtQtyYear1.Text);
                mf2.molasses_avail_pyr2 = Convert.ToDouble(txtQtyYear2.Text);
                mf2.molasses_avail_pyr3 =  Convert.ToDouble(txtQtyYear3.Text);
                mf2.why_stock_not_cleared = txt10.Text;
                mf2.newcrop_storage = txt11.Text;
                mf2.newcrop_storage_difficulty = txt12.Text;
                mf2.cleaned_storage = txt13.Text;
                mf2.name_address_manager = txt14b.Text;
                mf2.name_address_occupier = txt14a.Text;
                mf2.mechanical_pump = txt15.Text;
                mf2.total_molasses_delivered =Convert.ToDouble( txtDistillery.Text);
                mf2.other_person_total= Convert.ToDouble(txtOtherTotal.Text);
                mf2.total_avail_stock_stored= Convert.ToDouble(txtStorageTotal.Text);
                mf2.record_status = "N";
                mf2.product_code = ddlMaterial.SelectedValue;
                mf2.name_address_applicant = txt14.Text;
                mf2.delevery = new List<Molasses_Delivery_Details_MF2>();
                if(storagetotalcapacity.Value!="")
                mf2.total_storage_capacity=Convert.ToDouble(storagetotalcapacity.Value);
                for (int i=0;i<grdDistlleries.Rows.Count;i++)
                {
                    Molasses_Delivery_Details_MF2 delevry = new Molasses_Delivery_Details_MF2();
                    GridViewRow row = grdDistlleries.Rows[i];
                    delevry.party_code=  (row.Cells[1].FindControl("lblParty_code") as Label).Text;
                    delevry.delivered_year = (row.Cells[1].FindControl("lblYear") as Label).Text;
                    delevry.delivered_qty = (row.Cells[1].FindControl("lblQty") as Label).Text;
                    delevry.molasses_deliverydetail_id = (row.Cells[1].FindControl("lblid") as Label).Text;
                    delevry.deleted_ids = distdeleted;
                    mf2.delevery.Add(delevry);
                }
                mf2.other = new List<Molasses_Other_Delevery_MF2>();
                for (int i = 0; i < grdOther.Rows.Count; i++)
                {
                    Molasses_Other_Delevery_MF2 other = new Molasses_Other_Delevery_MF2();
                    GridViewRow row = grdOther.Rows[i];
                    other.other_party = (row.Cells[1].FindControl("lblDistilleryname") as Label).Text;
                    other.delivered_year = (row.Cells[1].FindControl("lblYear") as Label).Text;
                    other.delivered_qty = (row.Cells[1].FindControl("lblQty") as Label).Text;
                    other.molasses_other_deliverydetail_id = (row.Cells[1].FindControl("lblid") as Label).Text;
                    other.deleted_ids = otherdeleted;
                    mf2.other.Add(other);
                }
                mf2.storage = new List<Molasses_Storage_Details_MF2>();
                for (int i = 0; i < grdStorage.Rows.Count; i++)
                {
                    Molasses_Storage_Details_MF2 storage = new Molasses_Storage_Details_MF2();
                    GridViewRow row = grdStorage.Rows[i];
                    storage.vat_code = (row.Cells[1].FindControl("lblvat_code") as Label).Text;
                    storage.financial_year = (row.Cells[1].FindControl("lblYear") as Label).Text;
                    storage.bal_capacity = (row.Cells[1].FindControl("lblQTY") as Label).Text;
                    storage.molasses_prod_tank_storage_id = (row.Cells[1].FindControl("lblid") as Label).Text;
                    storage.deleted_ids = storagedeleted;
                    mf2.storage.Add(storage);
                }
                mf2.vatmaster = new List<VAT_Master>();
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    VAT_Master storage = new VAT_Master();
                    GridViewRow row = GridView1.Rows[i];
                    storage.vat_code = (row.Cells[1].FindControl("lblvat_code") as Label).Text;
                    storage.vat_totalcapacity =Convert.ToDouble( (row.Cells[1].FindControl("txtQuantity") as TextBox).Text);
                    mf2.vatmaster.Add(storage);
                }
             //   mf2.vatmaster = vatmasters;
                mf2.record_status = "N";
                mf2.userid = Session["UserID"].ToString();
                mf2.party_code = partycode.Value;
                mf2.product_code = ddlMaterial.SelectedValue;
                string val = "";
                if(Session["rtype"].ToString()=="0")
                {
                    val = BL_Molasses_Production_MF2.Insert(mf2);
                }
                else
                {
                    mf2.molasses_prov_prod_id = Session["mpid"].ToString();
                    val = BL_Molasses_Production_MF2.Update(mf2);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("MolassesProvisionalProductionList");
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
                btnSaveasDraft.Enabled = true;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                btnSubmit.Enabled = false;
                Molasses_Production_MF2 mf2 = new Molasses_Production_MF2();
                mf2.financial_year = txtFinancialYear.Text;
                mf2.entry_date = txtDate1.Text;
                mf2.iscaptive = ddlCaptive.SelectedValue;
             //   mf2.to_party_code = ddUnitName.SelectedValue;
                mf2.cane_crushing_date =txtdob.Value;

                mf2.molasses_plan_next_season = Convert.ToDouble(txtmolassesproduction.Text);
                mf2.sugar_plan_next_season = Convert.ToDouble(txtsugarproduction.Text);

                mf2.molasses_prod_daily = Convert.ToDouble(txtQtyMproduceddaily.Text);
                mf2.sugar_prod_daily = Convert.ToDouble(txtQtySproduceddaily.Text);
                mf2.new_prod_storage = Convert.ToDouble(txtproductionstoredin.Text);
                mf2.wagon_loading = txtLoadingofMolasses.Text;
                mf2.actualprod_prevyr1 = Convert.ToDouble(txtYear21.Text);
                mf2.actualprod_prevyr2 = Convert.ToDouble(txtYear22.Text);
                mf2.actualprod_prevyr3 = Convert.ToDouble(txtYear23.Text);
                mf2.molasses_avail_pyr1 = Convert.ToDouble(txtQtyYear1.Text);
                mf2.molasses_avail_pyr2 = Convert.ToDouble(txtQtyYear2.Text);
                mf2.molasses_avail_pyr3 = Convert.ToDouble(txtQtyYear3.Text);
                mf2.why_stock_not_cleared = txt10.Text;
                mf2.newcrop_storage = txt11.Text;
                mf2.newcrop_storage_difficulty = txt12.Text;
                mf2.cleaned_storage = txt13.Text;
                mf2.name_address_manager = txt14b.Text;
                mf2.name_address_occupier = txt14a.Text;
                mf2.mechanical_pump = txt15.Text;
                mf2.record_status = "Y";
                mf2.name_address_applicant = txt14.Text;
             //   mf2.product_code = ddlMaterial.SelectedValue;
                mf2.userid = Session["UserID"].ToString();
                mf2.total_molasses_delivered = Convert.ToDouble(txtDistillery.Text);
                mf2.other_person_total = Convert.ToDouble(txtOtherTotal.Text);
                mf2.total_avail_stock_stored = Convert.ToDouble(txtStorageTotal.Text);
                mf2.delevery = new List<Molasses_Delivery_Details_MF2>();
                if (storagetotalcapacity.Value != "")
                    mf2.total_storage_capacity = Convert.ToDouble(storagetotalcapacity.Value);
                for (int i = 0; i < grdDistlleries.Rows.Count; i++)
                {
                    Molasses_Delivery_Details_MF2 delevry = new Molasses_Delivery_Details_MF2();
                    GridViewRow row = grdDistlleries.Rows[i];
                    delevry.party_code = (row.Cells[1].FindControl("lblParty_code") as Label).Text;
                    delevry.delivered_year = (row.Cells[1].FindControl("lblYear") as Label).Text;
                    delevry.delivered_qty = (row.Cells[1].FindControl("lblQty") as Label).Text;
                    delevry.molasses_deliverydetail_id = (row.Cells[1].FindControl("lblid") as Label).Text;
                    delevry.deleted_ids = distdeleted;
                    mf2.delevery.Add(delevry);
                }
                mf2.other = new List<Molasses_Other_Delevery_MF2>();
                for (int i = 0; i < grdOther.Rows.Count; i++)
                {
                    Molasses_Other_Delevery_MF2 other = new Molasses_Other_Delevery_MF2();
                    GridViewRow row = grdOther.Rows[i];
                    other.other_party = (row.Cells[1].FindControl("lblDistilleryname") as Label).Text;
                    other.delivered_year = (row.Cells[1].FindControl("lblYear") as Label).Text;
                    other.delivered_qty = (row.Cells[1].FindControl("lblQty") as Label).Text;
                    other.molasses_other_deliverydetail_id = (row.Cells[1].FindControl("lblid") as Label).Text;
                    other.deleted_ids = otherdeleted;
                    mf2.other.Add(other);
                }
                mf2.storage = new List<Molasses_Storage_Details_MF2>();
                for (int i = 0; i < grdStorage.Rows.Count; i++)
                {
                    Molasses_Storage_Details_MF2 storage = new Molasses_Storage_Details_MF2();
                    GridViewRow row = grdStorage.Rows[i];
                    storage.vat_code = (row.Cells[1].FindControl("lblvat_code") as Label).Text;
                    storage.financial_year = (row.Cells[1].FindControl("lblYear") as Label).Text;
                    storage.bal_capacity = (row.Cells[1].FindControl("lblQTY") as Label).Text;
                    storage.molasses_prod_tank_storage_id = (row.Cells[1].FindControl("lblid") as Label).Text;
                    storage.deleted_ids = storagedeleted;
                    mf2.storage.Add(storage);
                }
                mf2.vatmaster = new List<VAT_Master>();
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    VAT_Master storage = new VAT_Master();
                    GridViewRow row = GridView1.Rows[i];
                    storage.vat_code = (row.Cells[1].FindControl("lblvat_code") as Label).Text;
                    storage.vat_totalcapacity = Convert.ToDouble((row.Cells[1].FindControl("txtQuantity") as TextBox).Text);
                    mf2.vatmaster.Add(storage);
                }
                mf2.record_status = "Y";
                mf2.userid = Session["UserID"].ToString();
                mf2.party_code = partycode.Value;
                mf2.product_code = ddlMaterial.SelectedValue;
                string val = "";
                if (Session["rtype"].ToString() == "0")
                {
                    val = BL_Molasses_Production_MF2.Insert(mf2);
                }
                else
                {
                    mf2.molasses_prov_prod_id = Session["mpid"].ToString();
                    val = BL_Molasses_Production_MF2.Update(mf2);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("MolassesProvisionalProductionList");
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
                btnSubmit.Enabled = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("MolassesProvisionalProductionList");
        }
    }
}