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
    public partial class RawMaterialtoFermenterForm : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        static Form82 fermenter = new Form82();
        static string entrydate;
        static string _party_code;
        double molassestotal = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //molassestotal.Visible = false;
            //if (IsPostBack)
            //{
            //    txtDATE.Text = txtdob1.Value;
            //}

            if (!IsPostBack)
            {
                CalendarExtender.EndDate = DateTime.Now;

                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {

                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];

                    if (ViewState["Records"] == null)
                    {
                        dt.Columns.Add("vat_code");
                        dt.Columns.Add("StorageVat");
                        dt.Columns.Add("Product");
                        dt.Columns.Add("Molasses");
                        dt.Columns.Add("Mahua");
                        dt.Columns.Add("Gur");
                        dt.Columns.Add("Spent Wash");
                        dt.Columns.Add("Active Wash");
                        dt.Columns.Add("Water");
                        dt.Columns.Add("Other materials");
                        dt.Columns.Add("No of vats");
                        dt.Columns.Add("Doc_id");
                        ViewState["Records"] = dt;
                    }
                    Session["UserID"] = Session["UserID"];
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        Session["party_code"] = user.party_code;
                        Session["rolename"] = user.role_name;
                        Session["financial_year"] = user.financial_year;
                        party_code.Value = user.party_code.ToString();
                        _party_code = party_code.Value;
                        List<VAT_Master> vats = new List<VAT_Master>();
                        vats = BL_VATMaster.GetvatmasterList(party_code.Value);
                        var list = from s in vats
                                   where s.party_code == user.party_code && s.vat_type_code == "FER" || s.vat_type_code == "fer"
                                   select s;
                        ddlFermenter.DataSource = list.ToList();
                        ddlFermenter.DataTextField = "vat_name";
                        ddlFermenter.DataValueField = "vat_code";
                        ddlFermenter.DataBind();
                        ddlFermenter.Items.Insert(0, "Select");
                        var list1 = from s in vats
                                    where s.party_code == user.party_code && s.vat_type_code == "MST" || s.vat_type_code == "Mst"
                                    select s;
                        ddlMolassesStorageVAT.DataSource = list1.ToList();
                        ddlMolassesStorageVAT.DataTextField = "vat_name";
                        ddlMolassesStorageVAT.DataValueField = "vat_code";
                        ddlMolassesStorageVAT.DataBind();
                        ddlMolassesStorageVAT.Items.Insert(0, "Select");
                        //List<RawMaterialType> rawmaterial = new List<RawMaterialType>();
                        //rawmaterial = BL_RawMaterialType.GetRawMaterialList(user.user_id);
                        //var list9 = from s in rawmaterial
                        //            select s;
                        //ddlRawmaterialType.DataSource = list9.ToList();
                        //ddlRawmaterialType.DataTextField = "rawmaterial_type_name";
                        //ddlRawmaterialType.DataValueField = "rawmaterial_type_code";
                        //ddlRawmaterialType.DataBind();
                        //ddlRawmaterialType.Items.Insert(0, "Select");
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        approverid.Visible = false;
                        approverremaks.Visible = false;
                        //molassestotal.Visible = true;
                      lblstar.Text = "*";
                       lblstar.ForeColor=System.Drawing.Color.Red;
                        lblRawMaterialUsed.InnerText =  "Raw Material Used";
                        if (Session["UserID"].ToString() == "Admin")
                        {
                            btnSaveAsDraft.Visible = true;
                            btnSubmit.Visible = true;
                            btnCancel.Visible = true;
                            btnApprove.Visible = false;
                            btnReject.Visible = false;
                            approverid.Visible = false;
                            approverremaks.Visible = false;
                        }
                        else if (user.role_name == "Bond Officer")
                        {
                            btnSaveAsDraft.Visible = false;
                            btnSubmit.Visible = false;
                            btnCancel.Visible = false;
                            btnApprove.Visible = false;
                            btnReject.Visible = false;
                            approverremaks.Visible = false;
                            approverid.Visible = false;
                            txtApproverremarks.Visible = false;
                            // ddlPartyName.SelectedValue = user.party_code;
                            //ddlPartyName.Attributes.Add("Disabled", "Disabled");
                        }
                        else
                        {
                            //ddlPartyName.SelectedValue = user.party_code;
                            btnSaveAsDraft.Visible = true;
                            btnSubmit.Visible = true;
                            btnCancel.Visible = true;
                            btnApprove.Visible = false;
                            btnReject.Visible = false;
                            //   ddlPartyName.Attributes.Add("Disabled", "Disabled");
                            //if (scp.record_status == "R" || scp.record_status == "A")
                            //{
                            //    approverremaks.Visible = true;
                            //    txtApproverremarks.Attributes.Add("disabled", "disabled");
                            //}
                            //else
                            //{
                            approverremaks.Visible = false;
                            //    txtApproverremarks.Attributes.Add("disabled", "disabled");
                            //}
                        }

                        if (Session["rtype"].ToString() != "0")
                        {
                            approverid.Visible = true;
                            fermenter = new Form82();
                            fermenter = BL_Form82.GetDetails(Convert.ToInt32(Session["FermenterId"].ToString()), Session["rffinancial_year"].ToString());
                            txtDATE.Text = fermenter.setup_date;
                            txtdob1.Value = fermenter.setup_date;
                            CalendarExtender.SelectedDate = Convert.ToDateTime(fermenter.setup_date);
                            CalendarExtender.EndDate = DateTime.Now;
                            txtSetuptime.Value = fermenter.setup_time;
                            ddlFermenter.SelectedValue = fermenter.tofermentervat;
                            txtVatNumber.Text = fermenter.no_of_vat_cask;
                            txtBLofWashSetup.Value = fermenter.total_bl_washsetup.ToString();
                            txtSpentWash.Value = fermenter.sg_spentwash.ToString();
                            txtSGVatorCask.Value = fermenter.sg_of_wash.ToString();
                            molassestotal = fermenter.total_qty_transferred;
                            //    lbltotal.Visible = true;
                            //    molassestotal.Visible = true;
                            for (int i = 0; i < fermenter.fermSetup.Count; i++)
                            {
                                if (i == 0)
                                    dummyDatatable.Visible = false;
                                dt = (DataTable)ViewState["Records"];
                                dt.Rows.Add(fermenter.fermSetup[i].vat_code, fermenter.fermSetup[i].vat_name,fermenter.fermSetup[i].rawmaterial,fermenter.fermSetup[i].molasses, fermenter.fermSetup[i].mahua, fermenter.fermSetup[i].gur, fermenter.fermSetup[i].spentwash, fermenter.fermSetup[i].activewash, fermenter.fermSetup[i].water, fermenter.fermSetup[i].other_material, fermenter.fermSetup[i].no_of_each_vat, fermenter.fermSetup[i].fermenter_setup_id);
                                gridToStore.DataSource = dt;
                                gridToStore.DataBind();
                            }
                            List<All_Approvals> approvals = new List<All_Approvals>();
                            approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), fermenter.rawmaterial_fermenter_id.ToString(), "F82");
                            var list4 = (from s in approvals
                                         where s.financial_year == user.financial_year
                                         select s);
                            grdApprovalDetails.DataSource = list4.ToList();
                            grdApprovalDetails.DataBind();
                            if (fermenter.record_status == "Y" || user.role_name == "Bond Officer")
                            {
                                foreach (GridViewRow dr1 in gridToStore.Rows)
                                {
                                    ImageButton btn = dr1.FindControl("ImageButton1") as ImageButton;
                                    btn.Visible = false;
                                }
                                Image1.Visible = false;
                                approverremaks.Visible = false;
                                approverid.Visible = false;
                                txtApproverremarks.Visible = false;
                            }
                            if ((Session["rtype"].ToString() == "1"))
                            {
                                if (fermenter.record_status == "A")
                                {
                                    txtApproverremarks.Attributes.Add("disabled", "disabled");
                                    btnApprove.Visible = false;
                                    btnReject.Visible = false;
                                    gridToStore.Columns[gridToStore.Columns.Count - 1].Visible = false;
                                    Image1.Visible = false;

                                }
                                txtDATE.Attributes.Add("disabled", "disabled");
                                ddlFermenter.Attributes.Add("disabled", "disabled");
                                //txtRemarks1.Attributes.Add("disabled", "disabled");
                                ddlMolassesStorageVAT.Enabled = false;
                                //ddlRawmaterialType.Enabled = false;
                                txtSetuptime.Attributes.Add("disabled", "disabled");
                                txtVatNumber.Enabled = false;
                                txtMolassesused.Attributes.Add("disabled", "disabled");
                                txtGur.Attributes.Add("disabled", "disabled");
                                txtSpentWash.Attributes.Add("disabled", "disabled");
                                txtActiveWash.Attributes.Add("disabled", "disabled");
                                txtWater.Attributes.Add("disabled", "disabled");
                                txtOtherMaterials.Attributes.Add("disabled", "disabled");
                                txtBLofWashSetup.Attributes.Add("disabled", "disabled");
                                txtSpentWash1.Attributes.Add("disabled", "disabled");
                                txtSGVatorCask.Attributes.Add("disabled", "disabled");
                                txtMahua.Attributes.Add("disabled", "disabled");
                                txtVatNumber.Attributes.Add("disabled", "disabled");
                                approverremaks.Visible = false;
                                btnSaveAsDraft.Visible = false;
                                btnSubmit.Visible = false;
                                btnCancel.Visible = false;
                                //lbltotal.Enabled = false;
                                btnAdd.Visible = false;
                                //   txtApproverremarks.Attributes.Add("disabled", "disabled");
                                if (user.role_name == "Bond Officer")
                                {
                                    btnSaveAsDraft.Visible = false;
                                    btnSubmit.Visible = false;
                                    btnCancel.Visible = false;
                                    btnApprove.Visible = false;
                                    btnReject.Visible = false;
                                    approverid.Visible = false;
                                    txtApproverremarks.Visible = false;
                                    approverremaks.Visible = false;

                                    //txtApproverremarks.Attributes.Add("disabled", "disabled");

                                    gridToStore.Columns[gridToStore.Columns.Count - 1].Visible = false;
                                    Image1.Visible = false;
                                }
                                gridToStore.Columns[gridToStore.Columns.Count - 1].Visible = false;
                                Image1.Visible = false;
                                if (user.role_name == "Bond Officer" && fermenter.record_status == "Y")
                                {
                                    approverremaks.Visible = false;
                                    approverid.Visible = false;
                                    txtApproverremarks.Visible = false;
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
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("RawMaterialtoFermenterList");
        }
        protected void lnkDailyDispatchClosure_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("DailyDispatchClosureList");
        }
        protected void lnkRawMaterialToFermenter_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("RawMaterialtoFermenterList");
        }
        protected void lnkFermentertoReceiver_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("FermentertoReceiverList");
        }
        protected void lnkReceivertoStorage_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ReceivertoStorageList");
        }
        protected void lnkRawMaterialWastage_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawmaterialWastageList.aspx");
        }
        protected void lnkFromStoragetoDispatch_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("FromStoragetoDispatchList");
        }
        protected void lnkRMR_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialReceiptList");
        }
        protected void btnVATtansfers_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATTransferList");
        }



        protected void ddlMolassesStorageVAT_Click(object sender, EventArgs e)
        {

            if (ddlMolassesStorageVAT.SelectedValue != null)
            {
                string storage = ddlMolassesStorageVAT.SelectedValue;
                string fermenter = ddlFermenter.SelectedValue;
                string vatcode1 = ddlMolassesStorageVAT.SelectedValue;
                string party_code1 = party_code.Value;
                Form82 vats = new Form82();
                vats = BL_Form82.Getvatavl1(vatcode1, Session["financial_year"].ToString());
                txtAvailableStock.Value = Convert.ToString(vats.vat_availablecapacity);
              ProductType va1 = new ProductType();
                va1 = BL_Form82.Getproduct(vatcode1, party_code1);
                txtRawMaterial.Value = va1.product_type_name;
                lblRawMaterialUsed.InnerText= va1.product_type_name+" " +"Used";
                  
                //int value = 0;
                //    if (entrydate != storage)
                //{ 
                //        value = BL_Form82.GetDup( storage,fermenter);
                //    if(value >=1)
                //    {
                //        string script = "alert(\"StorageVat Already exist !!!!!\");";
                //        ScriptManager.RegisterStartupScript(this, GetType(),
                //                              "ServerControlScript", script, true);
                //        ddlFermenter.ClearSelection();
                //        ddlMolassesStorageVAT.Focus();
                //    }

            }
        }
        protected void txtMolassesused_TextChanged(object sender, EventArgs e)
        {
            if (ddlMolassesStorageVAT.SelectedValue != "Select")
            {
                if (txtMolassesused.Text != "")
                {
                    if (Convert.ToDouble(txtMolassesused.Text) <= Convert.ToDouble(txtAvailableStock.Value))
                    {

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('"+txtRawMaterial.Value+" cannot be greater than Available Stock  \');", true);
                        txtMolassesused.Focus();
                        txtMolassesused.Text = "";
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Select  StorageVAT  \');", true);
                ddlMolassesStorageVAT.Focus();
                txtMolassesused.Text = "";
            }


        }

        int Doc_id = 1;
        protected void Add(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (ddlMolassesStorageVAT.SelectedItem != null)
                {
                    // get value and text which item you have selected
                    string codeitem = ddlMolassesStorageVAT.SelectedValue;
                    string storagevat = ddlMolassesStorageVAT.SelectedItem.Text.ToString();
                    string RawMaterialType =txtRawMaterial.Value;
                    //add the selected item to gridview
                    DataTable dtgridview = ViewState["Records"] as DataTable;
                    DataRow dr2 = dtgridview.NewRow();
                    bool ifExist = false;
                    foreach (DataRow dr in dtgridview.Rows)
                    {
                        if (dr["StorageVat"].ToString() == ddlMolassesStorageVAT.SelectedItem.ToString())
                        {
                            ifExist = true;
                            break;
                        }
                    }
                    if (!ifExist)
                    {
                        dummyDatatable.Visible = false;
                        string m = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-sss");
                        dt = (DataTable)ViewState["Records"];
                        dt.Rows.Add(ddlMolassesStorageVAT.SelectedValue, storagevat,  RawMaterialType, txtMolassesused.Text, txtMahua.Value, txtGur.Value, txtSpentWash1.Value, txtActiveWash.Value, txtWater.Value, txtOtherMaterials.Value, txtVatNumber.Text, Doc_id);
                        gridToStore.DataSource = dt;
                        gridToStore.DataBind();
                        Doc_id++;
                        ddlMolassesStorageVAT.ClearSelection();
                       txtRawMaterial.Value = "";
                        txtAvailableStock.Value = "";
                        gridToStore.Visible = true;
                        txtMolassesused.Text = "";
                        txtActiveWash.Value = "";
                        txtMahua.Value = "";
                        txtGur.Value = "";
                        txtSpentWash1.Value = "";
                        txtWater.Value = "";
                        txtOtherMaterials.Value = "";

                        for (int i = 0; i < gridToStore.Rows.Count; i++)
                        {
                            GridViewRow row1 = gridToStore.Rows[i];
                            string Qty1 = (row1.Cells[1].FindControl("lblMolassesUsed") as Label).Text;
                            molassestotal += Convert.ToDouble(Qty1);
                            Session["inbl"] = (gridToStore.FooterRow.Cells[i].FindControl("lblTotal") as Label).Text = molassestotal.ToString();
                            string a = Session["inbl"].ToString();
                        }
                    }
                    else
                    {
                        // this.lbgvck.Visible = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('StorageVat Already exist !!!!!\');", true);
                    }

                }
            }
        }


        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (gridToStore.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Add StorageVAT ');", true);
                    ddlMolassesStorageVAT.ClearSelection();
                    ddlMolassesStorageVAT.Focus();

                }
                else
                {
                    Form82 fermenter = new Form82();
                    if (txtDATE.Text == "" || txtDATE.Text != "")
                    {
                        txtDATE.Text = txtdob1.Value;
                    }
                    fermenter.setup_date = Convert.ToDateTime(txtDATE.Text).ToString("dd-MM-yyyy");
                    fermenter.setup_time = txtSetuptime.Value;
                    fermenter.party_code = party_code.Value;
                    fermenter.no_of_vat_cask = txtVatNumber.Text;
                    fermenter.total_qty_transferred = Convert.ToDouble(Session["inbl"].ToString());
                    fermenter.tofermentervat = ddlFermenter.SelectedValue;
                    fermenter.total_bl_washsetup = Convert.ToDouble(txtBLofWashSetup.Value);
                    fermenter.sg_spentwash = Convert.ToDouble(txtSpentWash.Value);
                    fermenter.sg_of_wash = Convert.ToDouble(txtSGVatorCask.Value);
                    fermenter.user_id = Session["UserId"].ToString();
                    fermenter.financial_year = Session["financial_year"].ToString();
                    fermenter.setup_complete = "N";
                    fermenter.record_status = "N";
                    int i = 0;
                    fermenter.fermSetup = new List<FermenterSetUp>();
                    dt = ViewState["Records"] as DataTable;
                    foreach (DataRow dr in dt.Rows)
                    {
                        FermenterSetUp setup = new FermenterSetUp();
                        setup.fermenter_setup_id = Convert.ToInt32((gridToStore.Rows[i].FindControl("lblDoc") as Label).Text);
                        setup.fromstoragevat = (gridToStore.Rows[i].FindControl("lblVatcode") as Label).Text;
                        setup.rawmaterial = (gridToStore.Rows[i].FindControl("lblProduct") as Label).Text;
                        string b = (gridToStore.Rows[i].FindControl("lblMolassesUsed") as Label).Text;
                        if (b != "")
                        {
                            setup.molasses = Convert.ToDouble((gridToStore.Rows[i].FindControl("lblMolassesUsed") as Label).Text);
                        }

                        string a = (gridToStore.Rows[i].FindControl("lblMahua") as Label).Text;
                        if (a != "")
                        {
                            setup.mahua = Convert.ToDouble((gridToStore.Rows[i].FindControl("lblMahua") as Label).Text);
                        }
                        //else
                        //{
                        //    setup.mahua = null;
                        //}

                        string c = (gridToStore.Rows[i].FindControl("lblGur") as Label).Text;
                        if (c != "")
                        {
                            setup.gur = Convert.ToDouble((gridToStore.Rows[i].FindControl("lblGur") as Label).Text);
                        }


                        string d = (gridToStore.Rows[i].FindControl("lblSpentWash") as Label).Text;
                        if (d != "")
                        {
                            setup.spentwash = Convert.ToDouble((gridToStore.Rows[i].FindControl("lblSpentWash") as Label).Text);
                        }


                        string f = (gridToStore.Rows[i].FindControl("lblActiveWash") as Label).Text;
                        if (f != "")
                        {
                            setup.activewash = Convert.ToDouble((gridToStore.Rows[i].FindControl("lblActiveWash") as Label).Text);
                        }

                        string g = (gridToStore.Rows[i].FindControl("lblWater") as Label).Text;
                        if (g != "")
                        {
                            setup.water = Convert.ToDouble((gridToStore.Rows[i].FindControl("lblWater") as Label).Text);
                        }

                        setup.other_material = (gridToStore.Rows[i].FindControl("lblOthermaterials") as Label).Text;
                        setup.no_of_each_vat = (gridToStore.Rows[i].FindControl("lblvat") as Label).Text;
                        fermenter.fermSetup.Add(setup);
                        i++;
                    }


                    string val;
                    if (Session["rtype"].ToString() == "0")
                        val = BL_Form82.Insert(fermenter);
                    else
                    {
                        fermenter.rawmaterial_fermenter_id = Convert.ToInt32(Session["FermenterId"].ToString());
                        val = BL_Form82.Update(fermenter);
                    }
                    if (val == "0")
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("RawMaterialtoFermenterList");
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


        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Form82 ferm = new Form82();

                ferm.record_status = "A";
                string val;
                ferm.rawmaterial_fermenter_id = Convert.ToInt32(Session["FermenterId"].ToString());
                ferm.remarks = txtApproverremarks.Value;
                ferm.user_id = Session["UserID"].ToString();
                val = BL_Form82.Approve(ferm);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("RawMaterialtoFermenterList");
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
                Form82 ferm = new Form82();

                ferm.record_status = "R";
                string val;
                ferm.rawmaterial_fermenter_id = Convert.ToInt32(Session["FermenterId"].ToString());
                ferm.remarks = txtApproverremarks.Value;
                ferm.user_id = Session["UserID"].ToString();
                val = BL_Form82.Approve(ferm);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("RawMaterialtoFermenterList");
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
                if (gridToStore.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Add Molasses StorageVAT ');", true);
                    ddlMolassesStorageVAT.ClearSelection();
                    ddlMolassesStorageVAT.Focus();

                }
                else
                {
                    Form82 fermenter = new Form82();
                    if (txtDATE.Text == "" || txtDATE.Text != "")
                    {
                        txtDATE.Text = txtdob1.Value;
                    }
                    fermenter.setup_date = Convert.ToDateTime(txtDATE.Text).ToString("dd-MM-yyyy");
                    fermenter.setup_time = txtSetuptime.Value;
                    fermenter.party_code = party_code.Value;
                    fermenter.no_of_vat_cask = txtVatNumber.Text;
                    fermenter.total_qty_transferred = Convert.ToDouble(Session["inbl"].ToString());
                    fermenter.tofermentervat = ddlFermenter.SelectedValue;
                    fermenter.total_bl_washsetup = Convert.ToDouble(txtBLofWashSetup.Value);
                    fermenter.sg_spentwash = Convert.ToDouble(txtSpentWash.Value);
                    fermenter.sg_of_wash = Convert.ToDouble(txtSGVatorCask.Value);
                    fermenter.user_id = Session["UserId"].ToString();
                    fermenter.financial_year = Session["financial_year"].ToString();
                    // fermenter.total_qty_transferred = Convert.ToDouble(txtAvailableStock.Value);
                    fermenter.setup_complete = "N";
                    fermenter.fermSetup = new List<FermenterSetUp>();
                    fermenter.user_id = Session["UserId"].ToString();
                    for (int i = 0; i < gridToStore.Rows.Count; i++)
                    {
                        FermenterSetUp setup = new FermenterSetUp();
                        setup.fermenter_setup_id = Convert.ToInt32((gridToStore.Rows[i].FindControl("lblDoc") as Label).Text);
                        setup.fromstoragevat = (gridToStore.Rows[i].FindControl("lblVatcode") as Label).Text;
                        setup.rawmaterial = (gridToStore.Rows[i].FindControl("lblProduct") as Label).Text;
                        string b = (gridToStore.Rows[i].FindControl("lblMolassesUsed") as Label).Text;
                        if (b != "")
                        {
                            setup.molasses = Convert.ToDouble((gridToStore.Rows[i].FindControl("lblMolassesUsed") as Label).Text);
                        }
                        else
                        {

                        }
                        string a = (gridToStore.Rows[i].FindControl("lblMahua") as Label).Text;
                        if (a != "")
                        {
                            setup.mahua = Convert.ToDouble((gridToStore.Rows[i].FindControl("lblMahua") as Label).Text);
                        }

                        string c = (gridToStore.Rows[i].FindControl("lblGur") as Label).Text;
                        if (c != "")
                        {
                            setup.gur = Convert.ToDouble((gridToStore.Rows[i].FindControl("lblGur") as Label).Text);
                        }


                        string d = (gridToStore.Rows[i].FindControl("lblSpentWash") as Label).Text;
                        if (d != "")
                        {
                            setup.spentwash = Convert.ToDouble((gridToStore.Rows[i].FindControl("lblSpentWash") as Label).Text);
                        }


                        string f = (gridToStore.Rows[i].FindControl("lblActiveWash") as Label).Text;
                        if (f != "")
                        {
                            setup.activewash = Convert.ToDouble((gridToStore.Rows[i].FindControl("lblActiveWash") as Label).Text);
                        }

                        string g = (gridToStore.Rows[i].FindControl("lblWater") as Label).Text;
                        if (g != "")
                        {
                            setup.water = Convert.ToDouble((gridToStore.Rows[i].FindControl("lblWater") as Label).Text);
                        }

                        setup.other_material = (gridToStore.Rows[i].FindControl("lblOthermaterials") as Label).Text;
                        setup.no_of_each_vat = (gridToStore.Rows[i].FindControl("lblvat") as Label).Text;
                        fermenter.fermSetup.Add(setup);

                    }
                    fermenter.record_status = "Y";
                    string val;

                    if (Session["rtype"].ToString() == "0")
                        val = BL_Form82.Insert(fermenter);
                    else
                    {
                        fermenter.rawmaterial_fermenter_id = Convert.ToInt32(Session["FermenterId"].ToString());
                        val = BL_Form82.Update(fermenter);
                    }
                    if (val == "0")
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("RawMaterialtoFermenterList");
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

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                double total = 0;
                ImageButton lb = (ImageButton)sender;
                GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
                //string filePath = (sender as ImageButton).CommandArgument;
                //File.Delete(Server.MapPath("~/Eascm_Docs/" + Path.GetFileName(filePath)));
                //FileInfo fInfoEvent;
                //fInfoEvent = new FileInfo(Path.GetFileName(filePath));
                //fInfoEvent.Delete();
                DataTable dt2 = (DataTable)ViewState["Records"];
                ViewState["CurrentTable"] = dt2;
                int rowID = gvRow.RowIndex;
                DataTable dt1 = ViewState["Records"] as DataTable;
                dt1.Rows[rowID].Delete();
                ViewState["dt"] = dt1;
                gridToStore.DataSource = dt1;
                gridToStore.DataBind();
                if (dt1.Rows.Count < 1)
                    dummyDatatable.Visible = true;

                //for (int i = 0; i < gridToStore.Rows.Count; i++)
                //{
                //    GridViewRow row1 = gridToStore.Rows[i];
                //    string Qty1 = (row1.Cells[1].FindControl("lblMolassesUsed") as Label).Text;
                //    total += Convert.ToDouble(Qty1);
                //    lbltotal.Text = total.ToString();
                //    molassestotal.Visible = true;
                //}
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (IsPostBack)
            {
                ImageButton lb = (ImageButton)sender;
                GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
                //string filePath = (sender as ImageButton).CommandArgument;
                //File.Delete(Server.MapPath("~/Eascm_Docs/" + Path.GetFileName(filePath)));
                //FileInfo fInfoEvent;
                //fInfoEvent = new FileInfo(Path.GetFileName(filePath));
                //fInfoEvent.Delete();
                DataTable dt2 = (DataTable)ViewState["Records"];
                ViewState["CurrentTable"] = dt2;
                int rowID = gvRow.RowIndex;
                DataTable dt1 = ViewState["Records"] as DataTable;
                dt1.Rows[rowID].Delete();
                ViewState["dt"] = dt1;
                gridToStore.DataSource = dt1;
                gridToStore.DataBind();
                if (dt1.Rows.Count < 1)
                    dummyDatatable.Visible = true;
                for (int i = 0; i < gridToStore.Rows.Count; i++)
                {
                    GridViewRow row1 = gridToStore.Rows[i];
                    string Qty1 = (row1.Cells[1].FindControl("lblMolassesUsed") as Label).Text;
                    molassestotal += Convert.ToDouble(Qty1);
                    Session["inbl"] = (gridToStore.FooterRow.Cells[i].FindControl("lblTotal") as Label).Text = molassestotal.ToString();
                    string a = Session["inbl"].ToString();
                }
            }
        }

        public static string chkDuplicateDates(Object scpdate)
        {
            int value = 0;
            if (scpdate.ToString().Length > 1)
            {
                if (entrydate != scpdate.ToString())
                    value = BL_User_Mgnt.GetExistsData("fermenter_setup", "fromstoragevat", scpdate.ToString());
            }
            return value.ToString();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialtoFermenterList");
        }

        double total = 0;
        protected void gridToStore_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    string a = (e.Row.FindControl("lblMolassesUsed") as Label).Text;
            //    if (a == "" || a == null)
            //    {
            //        (e.Row.FindControl("lblMolassesUsed") as Label).Text = "0";
            //    }
            //    else
            //    {
            //        total += Convert.ToDouble((e.Row.FindControl("lblMolassesUsed") as Label).Text);
            //    }
            //}
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                double total1 = total;
                Session["inbl"] = (e.Row.FindControl("lblTotal") as Label).Text = molassestotal.ToString();
                string a = Session["inbl"].ToString();
            }

        }

        protected void lnkOpeningBalance_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("OpeningBalanceList");

        }

        protected void gridToStore_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (IsPostBack)
            {
                gridToStore.PageIndex = e.NewPageIndex;

                for (int i = 0; i < gridToStore.Rows.Count; i++)
                {
                    GridViewRow row1 = gridToStore.Rows[i];
                    string Qty1 = (row1.Cells[1].FindControl("lblMolassesUsed") as Label).Text;
                    molassestotal += Convert.ToDouble(Qty1);
                    (gridToStore.FooterRow.Cells[i].FindControl("lblTotal") as Label).Text = molassestotal.ToString();
                }
            }
        }

        protected void ddlFermenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (ddlFermenter.SelectedItem.ToString() != "Select")
                {
                    txtDATE.Text = txtdob1.Value;
                    int value = BL_Form82.GetExistsData("rawmaterial_fermenter", txtDATE.Text, ddlFermenter.SelectedValue);
                    if (value > 0)
                    {
                        string message = "This fermenter is already setup for the selected date";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                        ddlFermenter.ClearSelection();
                        ddlFermenter.Focus();
                    }
                }
            }
        }
    }

}