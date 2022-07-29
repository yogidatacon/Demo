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
    public partial class FermentertoReceiverForm : System.Web.UI.Page
    {
        List<FermentertoReceiverForm_83> form83 = new List<FermentertoReceiverForm_83>();
        static string _party_code;
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        string outbl;
        string outLp;
        string inbl;
        string inlp;
        double inputbl = 0;
        double inputlp = 0;
        double outputbl = 0;
        double outputlp = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                CalendarExtender.EndDate = DateTime.Now;
               
                CalendarExtender2.EndDate = DateTime.Now;


                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
               

                if (ViewState["Records"] == null)
                {
                    dt.Columns.Add("FermenterCode");
                    dt.Columns.Add("vatcode");
                    dt.Columns.Add("Dips");
                    dt.Columns.Add("Temperature");
                    dt.Columns.Add("Indication");
                    dt.Columns.Add("Strength");
                    dt.Columns.Add("Receiver");
                    dt.Columns.Add("Bulk Liters");
                    dt.Columns.Add("LP Liters");
                    dt.Columns.Add("Doc_id");
                    ViewState["Records"] = dt;
                }

                if (ViewState["Store"] == null)
                {
                    dt2.Columns.Add("vat_code");
                    dt2.Columns.Add("StorageVat");
                    dt2.Columns.Add("Date");
                    dt2.Columns.Add("Hours");
                    dt2.Columns.Add("BulkLiter");
                    dt2.Columns.Add("LPLiter");
                    dt2.Columns.Add("Docid");
                    ViewState["Store"] = dt2;
                }


                Session["UserID"] = Session["UserID"];
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                Session["party_code"] = user.party_code;
                    Session["rolename"] = user.role_name;
                    Session["financial_year"] = user.financial_year;
                   
                    List<VAT_Master> vats = new List<VAT_Master>();
                vats = BL_VATMaster.GetvatmasterList(Session["party_code"].ToString());
                  
                    party_code.Value = user.party_code.ToString();
                _party_code = party_code.Value;
                    var list1 = from s in vats
                               where s.party_code == Session["party_code"].ToString() &&  s.vat_type_code == "STO" 
                                select s;
                ddlStorageVat.DataSource = list1.ToList();
                ddlStorageVat.DataTextField = "vat_name";
                ddlStorageVat.DataValueField = "vat_code";
                ddlStorageVat.DataBind();
                ddlStorageVat.Items.Insert(0, "Select");
                    party.Value = party_code.Value;
                List<Distillation> dist = new List<Distillation>();
                dist = BL_Distillation.Getdate(party_code.Value);
                var list2 = from s in dist
                           
                            select s;
               ddlDistillationDate.DataSource = list2.ToList();
                ddlDistillationDate.DataTextField = "distillation_date";
                //ddlDistillationDate.DataValueField = "distillation_id";
                ddlDistillationDate.DataBind();
                ddlDistillationDate.Items.Insert(0, "Select");
                btnApprove.Visible = false;
                btnReject.Visible = false;
                approverremarks.Visible = false;
                approverid.Visible = false;
                if (Session["UserID"].ToString() == "Admin")
                {

                    btnSaveasDraft.Visible = true;
                    btnSubmit.Visible = true;
                    btnCancel.Visible = true;
                    btnApprove.Visible = true;
                    btnReject.Visible = true;
                }
                else if (Session["rolename"].ToString()== "Bond Officer")
                {

                   btnSaveasDraft.Visible = false;
                    btnSubmit.Visible = false;
                    btnCancel.Visible = false;
                    btnApprove.Visible = true;
                    btnReject.Visible = true;
                    //ddlPartyName.SelectedValue = user.party_code;
                    //ddlPartyName.Attributes.Add("Disabled", "Disabled");
                }
                else
                {
                    //ddlPartyName.SelectedValue = user.party_code;
                    btnSaveasDraft.Visible = true;
                    btnSubmit.Visible = true;
                    btnCancel.Visible = true;
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                    // ddlPartyName.Attributes.Add("Disabled", "Disabled");
                    //if (.record_status == "R" || scp.record_status == "A")
                    //{
                    //    approverremaks.Visible = true;
                    //    txtApproverremarks.Attributes.Add("disabled", "disabled");
                    //}
                    //else
                    //{
                    //    approverremaks.Visible = false;
                    //    txtApproverremarks.Attributes.Add("disabled", "disabled");
                    //}
                }
                if (Session["rtype"].ToString()!="0")
                {
                    approverremarks.Visible = false;
                    approverid.Visible = true;
                    FermentertoReceiverForm_83 frm83 = new FermentertoReceiverForm_83();
                int frm83id = Convert.ToInt32( Session["fermenterreceiverid"].ToString());
                 //  string ferementer = Session["from_fermentervat"].ToString();
                  string Party_Code = Session["Party_Code"].ToString();
                   frm83 = BL_FermentertoReceiverForm_83.GetDetails(frm83id, party_code.Value, Session["Ffinancial_year"].ToString());
                    txtDATE.Text = frm83.gauged_date.ToString();
                    txtgpd.Value = frm83.gauged_date;
                        CalendarExtender.SelectedDate = Convert.ToDateTime(frm83.gauged_date);
                        party.Value = frm83.party_code;

                    if( frm83.record_status == "A")
                    {
                        List<Distillation> dist1 = new List<Distillation>();
                        dist1 = BL_Distillation.GetSubmiteddate(frm83id);
                        var list6 = from s in dist1
                                    where s.financial_year==user.financial_year
                                    select s;
                        ddlDistillationDate.DataSource = list6.ToList();
                        ddlDistillationDate.DataTextField = "distillation_date";
                       // ddlDistillationDate.DataValueField = "distillation_id";
                        ddlDistillationDate.DataBind();
                            List<Distillation> dist2 = new List<Distillation>();
                            dist2 = BL_Distillation.SubmitedSetupGetList(Session["party_code"].ToString());
                            var list5 = from s in dist2
                                        where /*s.party_code == Session["party_code"].ToString() &&*/ s.rawmaterial_fermenter_id == Convert.ToInt32( frm83.distillation_id)
                                        select s;
                            ddlSetupDate.DataSource = list5.ToList();
                            ddlSetupDate.DataTextField = "setup_date";
                            ddlSetupDate.DataValueField = "rawmaterial_fermenter_id";
                            ddlSetupDate.DataBind();
                        }
                      else
                    {
                        ddlDistillationDate.SelectedValue = frm83.distillation_date;
                            List<Distillation> dist2 = new List<Distillation>();
                            dist2 = BL_Distillation.SetupGetList(Session["party_code"].ToString());
                            var list5 = from s in dist2
                                        where /*s.party_code == Session["party_code"].ToString() &&*/ s.rawmaterial_fermenter_id == Convert.ToInt32(frm83.distillation_id)
                                        select   s;
                            ddlSetupDate.DataSource = list5.ToList();
                            ddlSetupDate.DataTextField = "setup_date";
                            ddlSetupDate.DataValueField = "rawmaterial_fermenter_id";
                            ddlSetupDate.DataBind();
                        }

                       // ddlDistillationDate_SelectedIndexChanged( sender,  e);



                        List<Distillation> vat = new List<Distillation>();
                    string date = frm83.distillation_id;
                    vat = BL_Distillation.GetVat(frm83.distillation_date,Convert.ToInt32(frm83.distillation_id));
                    var list = from s in vat
                               where s.party_code == Session["party_code"].ToString()
                               select s;
                    ddlFermenter.DataSource = list.ToList();
                    ddlFermenter.DataTextField = "vat_name";
                    ddlFermenter.DataValueField = "vat_code";
                    ddlFermenter.DataBind();
                    ddlFermenter.Items.Insert(0, "Select");

                    List<DistillationToStore> storage = new List<DistillationToStore>();

                    //storage = BL_Distillation.GetToStoreList(frm83.distillation_date,ddlFermenter.SelectedValue,user.party_code,ddlSetupDate.SelectedItem.ToString());
                    //var list3 = from s in storage
                    //            where s.party_code == Session["party_code"].ToString()
                    //            select s;
                    //ddlReceiver.DataSource = list3.ToList();
                    //ddlReceiver.DataTextField = "vat_name";
                    //ddlReceiver.DataValueField = "vat_code";
                    //ddlReceiver.DataBind();
                    //ddlReceiver.Items.Insert(0, "Select");

                    txtToWhichStill.Text = frm83.to_which_still;
                   if(frm83.removal_date=="" || frm83.removal_date ==null)
                    {
                        txtDate3.Text = "";
                    }
                   else
                    {
                        txtDate3.Text = frm83.removal_date.ToString();
                            CalendarExtender2.SelectedDate = Convert.ToDateTime(frm83.removal_date);
                        txtdor1.Value = frm83.removal_date;
                    }
                  
                    txtRBulkLiters.Text = frm83.redistillation_bl_qty.ToString();
                    txtRLpLiters.Text = frm83.redistillation_lp_qty.ToString();
                    txtRemarks1.Value = frm83.remarks.ToString();
                    for (int i = 0; i < frm83.ReceiverInput.Count; i++)
                    {
                        if (i == 0)
                            dummy.Visible = false;
                        dt = (DataTable)ViewState["Records"];
                        dt.Rows.Add(frm83.ReceiverInput[i].fermentervat, frm83.ReceiverInput[i].receivervat, frm83.ReceiverInput[i].dips, frm83.ReceiverInput[i].temperature, frm83.ReceiverInput[i].indication, frm83.ReceiverInput[i].strength, frm83.ReceiverInput[i].vat_name, frm83.ReceiverInput[i].bl_received, frm83.ReceiverInput[i].lp_received, frm83.ReceiverInput[i].fermenter_receiver_input_id);
                       grdToReceiver.DataSource = dt;
                        grdToReceiver.DataBind();
                        txtStrength.Text = frm83.ReceiverInput[i].strength.ToString();
                     }

                    for (int j = 0; j < frm83.ReceiverOutput.Count; j++)
                    {
                        if (j == 0)
                            dummyDatatable.Visible = false;
                        dt2 = (DataTable)ViewState["Store"];
                        dt2.Rows.Add(frm83.ReceiverOutput[j].to_storagevat, frm83.ReceiverOutput[j].vat_name, frm83.ReceiverOutput[j].removal_date, frm83.ReceiverOutput[j].removal_hour, frm83.ReceiverOutput[j].bl_tostorage, frm83.ReceiverOutput[j].lp_tostorage, frm83.ReceiverOutput[j].fermenter_receiver_output_id);
                        gridToStore.DataSource = dt2;
                        gridToStore.DataBind();
                      
                    }
                    outputbl = frm83.total_output_bl_qty;
                    outputlp = frm83.total_output_lp_qty;
                    inputbl = frm83.total_input_bl_qty;
                    inputlp = frm83.total_input_lp_qty;
                        if(gridToStore.Rows.Count !=0)

                        {
                            Session["outbl"] = (gridToStore.FooterRow.FindControl("lblTotal") as Label).Text = outputbl.ToString();
                            Session["outLp"] = (gridToStore.FooterRow.FindControl("lblLPTotal") as Label).Text = outputlp.ToString();
                        }

                        //   (grdToReceiver.FooterRow.FindControl("lblTotal") as Label).Text = inputbl.ToString();
                        if (grdToReceiver.Rows.Count != 0)
                        {
                            Session["inbl"] = (grdToReceiver.FooterRow.FindControl("lblTotal") as Label).Text = inputbl.ToString();
                            Session["inlp"] = (grdToReceiver.FooterRow.FindControl("lblLPTotal") as Label).Text = inputlp.ToString();
                        }
                    List<All_Approvals> approvals = new List<All_Approvals>();
                    approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), frm83.fermenter_receiver_id.ToString(), "FTR");
                        var list4 = (from s in approvals
                                     where s.financial_year == Session["Ffinancial_year"].ToString()
                                     select s);
                        grdApprovalDetails.DataSource = list4.ToList();
                        approverremarks.Visible = false;
                    grdApprovalDetails.DataBind();
                    if (frm83.record_status == "Y" || Session["rolename"].ToString() == "Bond Officer")
                    {
                        foreach (GridViewRow dr1 in grdToReceiver.Rows)
                        {
                            ImageButton btn = dr1.FindControl("ImageButton1") as ImageButton;
                            btn.Visible = false;
                        }
                         Image1.Visible = false;

                        foreach (GridViewRow dr1 in gridToStore.Rows)
                        {
                            ImageButton btn = dr1.FindControl("ImageButton3") as ImageButton;
                            btn.Visible = false;
                        }
                        Image1.Visible = false;
                    }
                    if ((Session["rtype"].ToString() == "1"))
                    {

                        if (frm83.record_status == "A")
                        {
                           txtapproverremarks.Attributes.Add("disabled", "disabled");
                            btnApprove.Visible = false;
                            btnReject.Visible = false;
                           gridToStore.Columns[gridToStore.Columns.Count - 1].Visible = false;
                            grdToReceiver.Columns[grdToReceiver.Columns.Count - 1].Visible = false;
                            Image1.Visible = false;
                        }
                        btnCancel.Visible = false;
                        btnSaveasDraft.Visible = false;
                        btnSubmit.Visible = false;
                        txtDATE.Attributes.Add("disabled", "disabled");
                        txtDate3.Attributes.Add("disabled", "disabled");
                        txtDateofRemoval.Attributes.Add("disabled", "disabled");
                        ddlDistillationDate.Attributes.Add("disabled", "disabled");
                        txtHoursofRemoval.Attributes.Add("disabled", "disabled");
                        txtRBulkLiters.Attributes.Add("disabled", "disabled");
                        txtDipsinWetInches.Attributes.Add("disabled", "disabled");
                        txtTemperature.Attributes.Add("disabled", "disabled");
                        txtIndication.Attributes.Add("disabled", "disabled");
                        txtStrength.Attributes.Add("disabled", "disabled");
                        ddlFermenter.Attributes.Add("disabled", "disabled");
                        ddlReceiver.Attributes.Add("disabled", "disabled");
                        txtBulkLiters.Attributes.Add("disabled", "disabled");
                        BulkLiters.Attributes.Add("disabled", "disabled");
                            ddlSetupDate.Attributes.Add("disabled", "disabled");
                            txtToWhichStill.Attributes.Add("disabled", "disabled");
                        txtRemarks1.Attributes.Add("disabled", "disabled");
                        ddlStorageVat.Attributes.Add("disabled", "disabled");
                        btnAdd.Visible = false;
                        btnadd1.Visible = false;
                        Image1.Visible = false;
                        Image2.Visible = false;
                        gridToStore.Enabled = false;
                        grdToReceiver.Enabled = false;
                        approverremarks.Visible = false;

                        if (Session["rolename"].ToString() == "Bond Officer")
                        {
                            btnSaveasDraft.Visible = false;
                            btnSubmit.Visible = false;
                            btnCancel.Visible = false;

                            if (frm83.record_status == "Y")
                            {
                                approverremarks.Visible = true;

                                //txtApproverremarks.Attributes.Add("disabled", "disabled");
                            }
                            else
                            {
                                approverremarks.Visible = false;
                            }
                            gridToStore.Columns[gridToStore.Columns.Count - 1].Visible = false;
                            Image1.Visible = false;
                        }
                        gridToStore.Columns[gridToStore.Columns.Count - 1].Visible = false;
                        Image1.Visible = false;
                      grdToReceiver.Columns[grdToReceiver.Columns.Count - 1].Visible = false;
                        Image1.Visible = false;
                        if (Session["rolename"].ToString() == "Bond Officer" && frm83.record_status == "Y")
                        {
                            approverremarks.Visible = true;
                        }
                        if (Session["rolename"].ToString() == "Bond Officer" && frm83.record_status == "Y")
                        {
                            approverremarks.Visible = true;
                            txtapproverremarks.Visible = true;
                            btnApprove.Visible = true;
                            btnReject.Visible = true;
                        }
                        if (frm83.record_status == "A" || frm83.record_status == "R")
                        {
                            btnSaveasDraft.Visible = false;
                            btnApprove.Visible = false;
                            btnReject.Visible = false;
                        }

                    }
                }
                }
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/LoginPage");
                }
            }
        }
        protected void ddlDistillationDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                if(ddlDistillationDate.SelectedValue !="Select")
                {
                   string a= txtgpd.Value;
               
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                List<Distillation> vat = new List<Distillation>();
                    vat = BL_Distillation.SetupGetList(Session["party_code"].ToString());
                    var list1 = from s in vat
                                where /*s.party_code == Session["party_code"].ToString() &&*/ s.distillation_date == ddlDistillationDate.SelectedValue 
                                select s;
                    ddlSetupDate.DataSource = list1.ToList();
                    ddlSetupDate.DataTextField = "setup_date";
                    ddlSetupDate.DataValueField = "rawmaterial_fermenter_id";
                    ddlSetupDate.DataBind();
                string date = ddlDistillationDate.SelectedValue;
                vat = BL_Distillation.GetVat(date,Convert.ToInt32(ddlSetupDate.SelectedValue));
                var list = from s in vat
                           where s.party_code == Session["party_code"].ToString()
                           select s;
                ddlFermenter.DataSource = list.ToList();
                ddlFermenter.DataTextField = "vat_name";
                ddlFermenter.DataValueField = "vat_code";
                ddlFermenter.DataBind();
                ddlFermenter.Items.Insert(0, "Select");
                   // CalendarExtender.SelectedDate = Convert.ToDateTime(txtgpd.Value);
              
                   


                }
            }
        }

        protected void ddlFermenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string vatcode = ddlFermenter.SelectedValue;
                string code = Session["party_code"].ToString();
                List<DistillationToStore> storage = new List<DistillationToStore>();
                string date = ddlDistillationDate.SelectedValue;
                storage = BL_Distillation.GetToStoreList(date,vatcode, Session["party_code"].ToString(),ddlSetupDate.SelectedItem.ToString());
                var list3 = from s in storage
                            where s.party_code == party_code.Value
                            select s;
                ddlReceiver.DataSource = list3.ToList();
                ddlReceiver.DataTextField = "vat_name";
                ddlReceiver.DataValueField = "vat_code";
                ddlReceiver.DataBind();
                ddlReceiver.Items.Insert(0, "Select");
                Distillation aval = new Distillation();
                aval = BL_Distillation.Getvatavailableqty(vatcode, code);
                txtAvailableQtyBL.Text = aval.vat_availablecapacity.ToString();

                Form82 f82 = new Form82();
                f82 = BL_Form82.GetSetupdate(party_code.Value, ddlDistillationDate.SelectedValue,vatcode);
                CalendarExtender1.StartDate = Convert.ToDateTime(f82.setup_date);
                CalendarExtender1.EndDate = DateTime.Now;
                CalendarExtender2.EndDate = DateTime.Now;
            }
        }


        protected void ddlReceiver_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if(txtStrength.Text != "")
                { 
                string vatcode = ddlReceiver.SelectedValue;
                string date = ddlDistillationDate.SelectedItem.ToString();
                    int id = Convert.ToInt32(ddlSetupDate.SelectedValue);
                    FermentertoReceiverForm_83 receiver1 = new FermentertoReceiverForm_83();
                    receiver1 = BL_FermentertoReceiverForm_83.GetVatAval(vatcode, party.Value, date,id);
                    DistillationToStore receiver = new DistillationToStore();
                receiver = BL_Distillation.Getreciverbl(vatcode,date,id);
                    if (Session["rtype"].ToString() != "2")
                    {
                        if (receiver1.total_output_bl_qty != 0 /* && receiver.bl_store < receiver1.total_output_bl_qty*/)
                        {
                            txtBulkLiters.Text = (receiver.bl_store - receiver1.total_output_bl_qty).ToString();
                        }
                        else
                        {
                            txtBulkLiters.Text = receiver.bl_store.ToString();
                        }
                    }
                    else
                    {
                        txtBulkLiters.Text = receiver.bl_store.ToString();
                    }

                //double bulk = Convert.ToDouble(txtBulkLiters.Text);
                //double lp = Convert.ToDouble(txtStrength.Text);
                txtLPLiters.Text = (Convert.ToDouble(txtBulkLiters.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Strength !!!!!\');", true);
                    txtStrength.Focus();
                    ddlReceiver.ClearSelection();
                }
            }
       }
        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("FermentertoReceiverList");
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

        protected void lnkRawMaterialWastage_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawmaterialWastageList.aspx");
        }
        protected void lnkRMR_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialReceiptList");
        }

        int Doc_id = 1;
        protected void Add(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

                if (ddlReceiver.SelectedValue != "")
                {
                    // get value and text which item you have selected
                    string vat_code = ddlReceiver.SelectedValue;
                    string recivervat = ddlReceiver.SelectedItem.ToString();
                    //add the selected item to gridview
                    DataTable dtgridview = ViewState["Records"] as DataTable;
                    DataRow dr2 = dtgridview.NewRow();
                    bool ifExist = false;
                    foreach (DataRow dr in dtgridview.Rows)
                    {
                        if (dr["Receiver"].ToString() == ddlReceiver.SelectedItem.ToString())
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

                        string fermentervat = ddlFermenter.SelectedValue;
                        dt.Rows.Add(fermentervat, vat_code, txtDipsinWetInches.Text, txtTemperature.Text, txtIndication.Text, txtStrength.Text, recivervat, txtBulkLiters.Text, txtLPLiters.Text, Doc_id);
                        grdToReceiver.DataSource = dt;
                        grdToReceiver.DataBind();
                        Doc_id++;
                        grdToReceiver.Visible = true;
                        ddlReceiver.ClearSelection();
                        txtBulkLiters.Text = "";
                        txtLPLiters.Text = "";
                        txtDipsinWetInches.Text = "";
                        txtTemperature.Text = "";
                        txtIndication.Text = "";
                    }
                    else
                    {
                        // this.lbgvck.Visible = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('ReceiverVat Already exist !!!!!\');", true);
                    }

                }
                for (int i = 0; i < grdToReceiver.Rows.Count; i++)
                {
                    GridViewRow row1 = grdToReceiver.Rows[i];
                    string Qty1 = (row1.Cells[1].FindControl("lblBulkLiters") as Label).Text;
                    string Qty2 = (row1.Cells[1].FindControl("LPLiters") as Label).Text;
                    inputbl += Convert.ToDouble(Qty1);
                    inputlp += Convert.ToDouble(Qty2);
                    Session["inbl"]  = (grdToReceiver.FooterRow.Cells[i].FindControl("lblTotal") as Label).Text = inputbl.ToString();
                    Session["inlp"] = (grdToReceiver.FooterRow.Cells[i].FindControl("lblLPTotal") as Label).Text = inputlp.ToString();
                    
                }

            }
        }

        double total = 0;
        double lptotal = 0;
        protected void grdToReceiver_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    total += Convert.ToDouble((e.Row.FindControl("lblBulkLiters") as Label).Text);
            //    lptotal += Convert.ToDouble((e.Row.FindControl("LPLiters") as Label).Text);
            //}
            //if (Session["rtype"].ToString() != "0")
            //{
            //    if (e.Row.RowType == DataControlRowType.Footer)
            //    {
            //        Session["inbl"] = (e.Row.FindControl("lblTotal") as Label).Text = inputbl.ToString();
            //        Session["inlp"] = (e.Row.FindControl("lblLPTotal") as Label).Text = inputlp.ToString();
            //    }   }

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
                grdToReceiver.DataSource = dt1;
                grdToReceiver.DataBind();
                if (dt1.Rows.Count < 1)
                    dummy.Visible = true;
                for (int i = 0; i < grdToReceiver.Rows.Count; i++)
                {
                    GridViewRow row1 = grdToReceiver.Rows[i];
                    string Qty1 = (row1.Cells[1].FindControl("lblBulkLiters") as Label).Text;
                    string Qty2 = (row1.Cells[1].FindControl("LPLiters") as Label).Text;
                    inputbl += Convert.ToDouble(Qty1);
                    inputlp += Convert.ToDouble(Qty2);
                    Session["inbl"] = (grdToReceiver.FooterRow.Cells[i].FindControl("lblTotal") as Label).Text = inputbl.ToString();
                    Session["inlp"] = (grdToReceiver.FooterRow.Cells[i].FindControl("lblLPTotal") as Label).Text = inputlp.ToString();

                }
            }
        }

        protected void BulkLiters_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if(BulkLiters.Text != "")
                {
                if (grdToReceiver.Rows.Count > 1)
                {
                    if (Convert.ToDouble(Session["inbl"].ToString()) != Convert.ToDouble(BulkLiters.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('To Store bulk liters value must equal to receiver bulk liter value  \');", true);
                        BulkLiters.Text = "";
                        BulkLiters.Focus();

                    }
                    else
                    {
                        if (txtStrength.Text != "")
                        {
                            LPLiters.Text = (Convert.ToDouble(BulkLiters.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Strength \');", true);
                            txtStrength.Focus();
                            txtStrength.Text = "";
                            BulkLiters.Text = "";
                        }
                    }
                }
                else
                {
                    //if (Convert.ToDouble(BulkLiters.Text) <= Convert.ToDouble(Session["inbl"].ToString()))
                    //{
                        if (txtStrength.Text != "")
                        {
                            LPLiters.Text = (Convert.ToDouble(BulkLiters.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();

                        }


                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter Strength \');", true);
                            txtStrength.Focus();
                            txtStrength.Text = "";
                            BulkLiters.Text = "";
                        }
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('To Store bulk liters value less than equal to receiver bulk liter value  \');", true);
                    //    BulkLiters.Text = "";
                    //    BulkLiters.Focus();
                    //}
                }
                }
                else
                {
                    BulkLiters.Text = "";
                    BulkLiters.Focus();
                }
            }
        }
        protected void Add1(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if(txtDateofRemoval.Text=="")
                {
                    txtDateofRemoval.Text = txtdor.Value;
                }
               
                if (ddlStorageVat.SelectedItem != null)
                {
                    // get value and text which item you have selected
                    string vat_code = ddlStorageVat.SelectedValue;
                    string recivervat = ddlStorageVat.SelectedItem.ToString();
                    //add the selected item to gridview
                    DataTable dtgridview = ViewState["Store"] as DataTable;
                    DataRow dr2 = dtgridview.NewRow();
                    bool ifExist = false;
                    foreach (DataRow dr in dtgridview.Rows)
                    {
                        if (dr["StorageVat"].ToString() == ddlStorageVat.SelectedItem.ToString())
                        {
                            ifExist = true;
                            break;
                        }
                    }
                    if (!ifExist)
                    {
                        dummyDatatable.Visible = false;
                        string m = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-sss");
                        dt2 = (DataTable)ViewState["Store"];
                        if(txtDateofRemoval.Text=="")
                        {
                            txtDateofRemoval.Text = txtdor.Value;
                        }

                        dt2.Rows.Add(vat_code, recivervat, txtDateofRemoval.Text, txtHoursofRemoval.Value, BulkLiters.Text, LPLiters.Text, Doc_id);
                        gridToStore.DataSource = dt2;
                        gridToStore.DataBind();
                        Doc_id++;
                        gridToStore.Visible = true;
                        ddlStorageVat.ClearSelection();
                        BulkLiters.Text = "";
                        txtHoursofRemoval.Value = "";
                        LPLiters.Text = "";
                        txtDateofRemoval.Text = "";
                        txtHoursofRemoval.Value = "";
                    }
                    else
                    {
                        // this.lbgvck.Visible = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('StorageVat Already exist !!!!!\');", true);
                    }

                }
                for (int i = 0; i < gridToStore.Rows.Count; i++)
                {
                    GridViewRow row1 = gridToStore.Rows[i];
                    string Qty1 = (row1.Cells[1].FindControl("lblBulkLiters") as Label).Text;
                    string Qty2 = (row1.Cells[1].FindControl("lblLPLiters") as Label).Text;
                    outputbl += Convert.ToDouble(Qty1);
                    outputlp += Convert.ToDouble(Qty2);
                    Session["outbl"] = (gridToStore.FooterRow.Cells[i].FindControl("lblTotal") as Label).Text = outputbl.ToString();
                    Session["outLp"] = (gridToStore.FooterRow.Cells[i].FindControl("lblLPTotal") as Label).Text = outputlp.ToString();
                  
                }

            }
        }

        double total1 = 0;
        double lptotal1 = 0;
        protected void gridToStore_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    total1 += Convert.ToDouble((e.Row.FindControl("lblBulkLiters") as Label).Text);
            //    lptotal1 += Convert.ToDouble((e.Row.FindControl("lblLPLiters") as Label).Text);
            //}
            //if (Session["rtype"].ToString() != "0")
            //{
            //    if (e.Row.RowType == DataControlRowType.Footer)
            //    {

            //        Session["outbl"] = (e.Row.FindControl("lblTotal") as Label).Text = outputbl.ToString();
            //        Session["outLp"] = (e.Row.FindControl("lblLPTotal") as Label).Text = outputlp.ToString();
            //    }
            //}
               
        }
        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            if (IsPostBack)
            {
                ImageButton lb = (ImageButton)sender;
                GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
                DataTable dt3 = (DataTable)ViewState["Store"];
                ViewState["CurrentTable"] = dt3;
                int rowID = gvRow.RowIndex;
                DataTable dt2 = ViewState["Store"] as DataTable;
                dt2.Rows[rowID].Delete();
                ViewState["dt2"] = dt2;
                gridToStore.DataSource = dt2;
                gridToStore.DataBind();
                if (dt2.Rows.Count < 1)
                    dummyDatatable.Visible = true;

                for (int i = 0; i < gridToStore.Rows.Count; i++)
                {
                    GridViewRow row1 = gridToStore.Rows[i];
                    string Qty1 = (row1.Cells[1].FindControl("lblBulkLiters") as Label).Text;
                    string Qty2 = (row1.Cells[1].FindControl("lblLPLiters") as Label).Text;
                    outputbl += Convert.ToDouble(Qty1);
                    outputlp += Convert.ToDouble(Qty2);
                    Session["outbl"] = (gridToStore.FooterRow.Cells[i].FindControl("lblTotal") as Label).Text = outputbl.ToString();
                    Session["outLp"] = (gridToStore.FooterRow.Cells[i].FindControl("lblLPTotal") as Label).Text = outputlp.ToString();
                    //string a = Session["inbl"].ToString();
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                if(grdToReceiver.Rows.Count==0 || gridToStore.Rows.Count==0)
                {
                    if(grdToReceiver.Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert(' Add ReceiverVat');", true);
                        ddlReceiver.ClearSelection();
                        ddlReceiver.Focus();
                    }

                    if (gridToStore.Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert(' Add StorageVat');", true);
                     ddlStorageVat.ClearSelection();
                        ddlStorageVat.Focus();
                        BulkLiters.Text = "";
                        txtHoursofRemoval.Value = "";
                        LPLiters.Text = "";
                        txtDateofRemoval.Text = "";
                      
                    }
                }
                else
                { 
                FermentertoReceiverForm_83 from83 = new FermentertoReceiverForm_83();
                    if (txtDATE.Text == "" || txtDATE.Text != "")
                    {
                        txtDATE.Text = txtgpd.Value;
                    }

                    if (txtDateofRemoval.Text == "" || txtDateofRemoval.Text != "")
                    {
                        txtDateofRemoval.Text = txtdor.Value;
                    }

                    if (txtDate3.Text == "" || txtDate3.Text != "")
                    {
                        txtDate3.Text = txtdor1.Value;
                    }
                    from83.gauged_date = txtDATE.Text;
                from83.distillation_date = ddlDistillationDate.SelectedItem.ToString();
                    //setup id is stored in  attribute1
                    from83.distillation_id = ddlSetupDate.SelectedValue;
                from83.party_code = party_code.Value;
                from83.to_which_still = txtToWhichStill.Text;
                if(txtRBulkLiters.Text=="")
                {
                    from83.redistillation_bl_qty = 0;
                }
                else
                {
                    from83.redistillation_bl_qty = Convert.ToDouble(txtRBulkLiters.Text);
                }
               if(txtRLpLiters.Text=="")
                {
                    from83.redistillation_lp_qty = 0;
                }
                else
                {
                    from83.redistillation_lp_qty = Convert.ToDouble(txtRLpLiters.Text);
                }
             if(txtDate3.Text=="")
                {
                    from83.removal_date = null;
                }
             else
                {
                    from83.removal_date = txtDate3.Text;
                }
                
                from83.remarks = txtRemarks1.Value;
               from83.record_status = "Y";
                from83.user_id = Session["UserID"].ToString();
                    from83.financial_year= Session["financial_year"].ToString();
                from83.ReceiverInput = new List<FReceiverInput>();
               
                for (int i = 0; i < grdToReceiver.Rows.Count; i++)
                {
                    FReceiverInput setup = new FReceiverInput();
                    setup.fermentervat = (grdToReceiver.Rows[i].FindControl("lblFermenterCode") as Label).Text;
                    setup.receivervat = (grdToReceiver.Rows[i].FindControl("lblVatCoder") as Label).Text;
                    setup.dips = Convert.ToDouble((grdToReceiver.Rows[i].FindControl("lblDipsinWetCM") as Label).Text);
                    setup.temperature = Convert.ToDouble((grdToReceiver.Rows[i].FindControl("lblTemperature") as Label).Text);
                    setup.indication = Convert.ToDouble((grdToReceiver.Rows[i].FindControl("lblIndication") as Label).Text);
                    setup.strength = Convert.ToDouble((grdToReceiver.Rows[i].FindControl("lblStrength") as Label).Text);
                    setup.bl_received = Convert.ToDouble((grdToReceiver.Rows[i].FindControl("lblBulkLiters") as Label).Text);
                    setup.lp_received = Convert.ToDouble((grdToReceiver.Rows[i].FindControl("LPLiters") as Label).Text);
                    from83.ReceiverInput.Add(setup);
                }
               from83.fermentervat = (grdToReceiver.Rows[0].FindControl("lblFermenterCode") as Label).Text;
                from83.ReceiverOutput = new List<FReceiverOuput>();
                for (int j = 0; j < gridToStore.Rows.Count; j++)
                {
                    FReceiverOuput output = new FReceiverOuput();
                    //setup.distillation_tostore_id= Convert.ToInt32((grdDistillation.Rows[i].FindControl("lblDistillationID") as Label).Text);
                    output.to_storagevat = (gridToStore.Rows[j].FindControl("lblVatCode") as Label).Text;
                    output.removal_date = (gridToStore.Rows[j].FindControl("lblDateofRemoval") as Label).Text;
                    output.removal_hour = (gridToStore.Rows[j].FindControl("lblHoursofRemoval") as Label).Text;
                    output.bl_tostorage = Convert.ToDouble((gridToStore.Rows[j].FindControl("lblBulkLiters") as Label).Text);
                    output.lp_tostorage = Convert.ToDouble((gridToStore.Rows[j].FindControl("lblLPLiters") as Label).Text);
                    from83.ReceiverOutput.Add(output);

                }
                from83.total_input_bl_qty = Convert.ToDouble(Session["inbl"].ToString());
                from83.total_input_lp_qty = Convert.ToDouble(Session["inlp"].ToString());
                from83.total_output_bl_qty = Convert.ToDouble(Session["outbl"].ToString());
                from83.total_output_lp_qty = Convert.ToDouble(Session["outLp"].ToString());
                string val;
                if (Session["rtype"].ToString() == "0")
                {
                    val = BL_FermentertoReceiverForm_83.Insert(from83);
               }
                else
                {
                   from83.fermenter_receiver_id = Convert.ToInt32(Session["fermenterreceiverid"].ToString());
                    val = BL_FermentertoReceiverForm_83.Update(from83);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("FermentertoReceiverList");
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
        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                 FermentertoReceiverForm_83 from83 = new FermentertoReceiverForm_83();

                if (txtDATE.Text == "" || txtDATE.Text != "")
                {
                    txtDATE.Text = txtgpd.Value;
                }

                if (txtDateofRemoval.Text == "" || txtDateofRemoval.Text !="")
                {
                    txtDateofRemoval.Text = txtdor.Value;
                }

                if (txtDate3.Text == "" || txtDate3.Text !="")
                {
                    txtDate3.Text = txtdor1.Value;
                }

                from83.gauged_date = txtDATE.Text;
                from83.distillation_date = ddlDistillationDate.SelectedItem.ToString();
                //setup id in attribute1
                from83.distillation_id = ddlSetupDate.SelectedValue;
                from83.to_which_still = txtToWhichStill.Text;
                if (txtRBulkLiters.Text == "")
                {
                    from83.redistillation_bl_qty = 0;
                }
                else
                {
                    from83.redistillation_bl_qty = Convert.ToDouble(txtRBulkLiters.Text);
                }
                if (txtRLpLiters.Text == "")
                {
                    from83.redistillation_lp_qty = 0;
                }
                else
                {
                    from83.redistillation_lp_qty = Convert.ToDouble(txtRLpLiters.Text);
                }
                if (txtDate3.Text == "")
                {
                    from83.removal_date =null;
                }
                else
                {
                    from83.removal_date = txtDate3.Text;
                }
                from83.remarks = txtRemarks1.Value;
                from83.party_code = party_code.Value;
                from83.record_status = "N";
                from83.user_id = Session["UserID"].ToString();
                from83.financial_year = Session["financial_year"].ToString();
                int i = 0;
                from83.ReceiverInput = new List<FReceiverInput >();
                dt = ViewState["Records"] as DataTable;
                foreach (DataRow dr in dt.Rows)
                {
                    FReceiverInput setup = new FReceiverInput();
                    //setup.distillation_tostore_id= Convert.ToInt32((grdDistillation.Rows[i].FindControl("lblDipsinWetCM") as Label).Text);
                    setup.fermentervat = (grdToReceiver.Rows[i].FindControl("lblFermenterCode") as Label).Text;
                    setup.receivervat = (grdToReceiver.Rows[i].FindControl("lblVatCoder") as Label).Text;
                   setup.dips= Convert.ToDouble((grdToReceiver.Rows[i].FindControl("lblDipsinWetCM") as Label).Text);
                   setup.temperature= Convert.ToDouble((grdToReceiver.Rows[i].FindControl("lblTemperature") as Label).Text);
                    setup.indication= Convert.ToDouble((grdToReceiver.Rows[i].FindControl("lblIndication") as Label).Text);
                    setup.strength= Convert.ToDouble((grdToReceiver.Rows[i].FindControl("lblStrength") as Label).Text);
                    setup.bl_received = Convert.ToDouble((grdToReceiver.Rows[i].FindControl("lblBulkLiters") as Label).Text);
                    setup.lp_received = Convert.ToDouble((grdToReceiver.Rows[i].FindControl("LPLiters") as Label).Text);
                    from83.ReceiverInput.Add(setup);
                    i++;

                }
                int j = 0;
                from83.ReceiverOutput = new List<FReceiverOuput>();
                dt2 = ViewState["Store"] as DataTable;
                foreach (DataRow dr1 in dt2.Rows)
                {
                    FReceiverOuput output = new FReceiverOuput();
                    output.to_storagevat = (gridToStore.Rows[j].FindControl("lblVatCode") as Label).Text;
                    output.removal_date = (gridToStore.Rows[j].FindControl("lblDateofRemoval") as Label).Text;
                    output.removal_hour = (gridToStore.Rows[j].FindControl("lblHoursofRemoval") as Label).Text;
                    output.bl_tostorage = Convert.ToDouble((gridToStore.Rows[j].FindControl("lblBulkLiters") as Label).Text);
                    output.lp_tostorage = Convert.ToDouble((gridToStore.Rows[j].FindControl("lblLPLiters") as Label).Text);
                    from83.ReceiverOutput.Add(output);
                        j++;
                    }
                if (grdToReceiver.Rows.Count != 0)
                {

                    from83.total_input_bl_qty = Convert.ToDouble(Session["inbl"].ToString());
                    from83.total_input_lp_qty = Convert.ToDouble(Session["inlp"].ToString());
                    from83.fermentervat = (grdToReceiver.Rows[0].FindControl("lblFermenterCode") as Label).Text;
                }
                if(gridToStore.Rows.Count != 0)
                { 
                from83.total_output_bl_qty = Convert.ToDouble(Session["outbl"].ToString());
                from83.total_output_lp_qty = Convert.ToDouble(Session["outLp"].ToString());
                }
               

                string val;

                if (Session["rtype"].ToString() == "0")
                {
                    val = BL_FermentertoReceiverForm_83.Insert(from83);
                }
                else
                {
                    from83.fermenter_receiver_id = Convert.ToInt32(Session["fermenterreceiverid"].ToString());
                    val = BL_FermentertoReceiverForm_83.Update(from83);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("FermentertoReceiverList");
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

    
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Session["UserID"] = Session["UserID"];
                Response.Redirect("FermentertoReceiverList");
            }
        }
        [WebMethod]
        //public static string GetValuesofVAT(Object vatcode)
        //{
        //    //string value = BL_FermentertoReceiverForm_83.GetExistsData(vatcode.ToString(), _party_code);
        //    //return value;
        //}

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                FermentertoReceiverForm_83 from83 = new FermentertoReceiverForm_83();
                from83.fermenter_receiver_id = Convert.ToInt32(Session["fermenterreceiverid"].ToString());
                from83.user_id = Session["UserID"].ToString();
                from83.distillation_date = ddlDistillationDate.SelectedValue;
                from83.distillation_id = ddlSetupDate.SelectedValue;
                from83.remarks = txtapproverremarks.Value;
                from83.record_status = "A";
                from83.financial_year = Session["financial_year"].ToString();
                from83.party_code = party.Value;
                int i = 0;
                from83.ReceiverInput = new List<FReceiverInput>();
                dt = ViewState["Records"] as DataTable;
                foreach (DataRow dr in dt.Rows)
                {
                    FReceiverInput setup = new FReceiverInput();
                    //setup.distillation_tostore_id= Convert.ToInt32((grdDistillation.Rows[i].FindControl("lblDipsinWetCM") as Label).Text);
                    setup.fermentervat = (grdToReceiver.Rows[i].FindControl("lblFermenterCode") as Label).Text;
                    setup.receivervat = (grdToReceiver.Rows[i].FindControl("lblVatCoder") as Label).Text;
                    setup.dips = Convert.ToDouble((grdToReceiver.Rows[i].FindControl("lblDipsinWetCM") as Label).Text);
                    setup.temperature = Convert.ToDouble((grdToReceiver.Rows[i].FindControl("lblTemperature") as Label).Text);
                    setup.indication = Convert.ToDouble((grdToReceiver.Rows[i].FindControl("lblIndication") as Label).Text);
                    setup.strength = Convert.ToDouble((grdToReceiver.Rows[i].FindControl("lblStrength") as Label).Text);
                    setup.bl_received = Convert.ToDouble((grdToReceiver.Rows[i].FindControl("lblBulkLiters") as Label).Text);
                    setup.lp_received = Convert.ToDouble((grdToReceiver.Rows[i].FindControl("LPLiters") as Label).Text);
                    from83.ReceiverInput.Add(setup);
                    i++;

                }
                int j = 0;
                from83.ReceiverOutput = new List<FReceiverOuput>();
                dt2 = ViewState["Store"] as DataTable;
                foreach (DataRow dr1 in dt2.Rows)
                {
                    FReceiverOuput output = new FReceiverOuput();
                    output.to_storagevat = (gridToStore.Rows[j].FindControl("lblVatCode") as Label).Text;
                    output.removal_date = (gridToStore.Rows[j].FindControl("lblDateofRemoval") as Label).Text;
                    output.removal_hour = (gridToStore.Rows[j].FindControl("lblHoursofRemoval") as Label).Text;
                    output.bl_tostorage = Convert.ToDouble((gridToStore.Rows[j].FindControl("lblBulkLiters") as Label).Text);
                    output.lp_tostorage = Convert.ToDouble((gridToStore.Rows[j].FindControl("lblLPLiters") as Label).Text);
                    from83.ReceiverOutput.Add(output);
                    j++;
                }
                string val;
                val = BL_FermentertoReceiverForm_83.Approve(from83);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("FermentertoReceiverList");
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
                FermentertoReceiverForm_83 from83 = new FermentertoReceiverForm_83();
                from83.fermenter_receiver_id = Convert.ToInt32(Session["fermenterreceiverid"].ToString());
                from83.user_id = Session["UserID"].ToString();
                from83.financial_year = Session["financial_year"].ToString();
                from83.distillation_date = ddlDistillationDate.SelectedValue;
                from83.remarks = txtapproverremarks.Value;
                from83.party_code = party_code.Value;
                from83.record_status = "R";
                int i = 0;
                from83.ReceiverInput = new List<FReceiverInput>();
                dt = ViewState["Records"] as DataTable;
                foreach (DataRow dr in dt.Rows)
                {
                    FReceiverInput setup = new FReceiverInput();
                    //setup.distillation_tostore_id= Convert.ToInt32((grdDistillation.Rows[i].FindControl("lblDipsinWetCM") as Label).Text);
                    setup.fermentervat = (grdToReceiver.Rows[i].FindControl("lblFermenterCode") as Label).Text;
                    setup.receivervat = (grdToReceiver.Rows[i].FindControl("lblVatCoder") as Label).Text;
                    setup.dips = Convert.ToDouble((grdToReceiver.Rows[i].FindControl("lblDipsinWetCM") as Label).Text);
                    setup.temperature = Convert.ToDouble((grdToReceiver.Rows[i].FindControl("lblTemperature") as Label).Text);
                    setup.indication = Convert.ToDouble((grdToReceiver.Rows[i].FindControl("lblIndication") as Label).Text);
                    setup.strength = Convert.ToDouble((grdToReceiver.Rows[i].FindControl("lblStrength") as Label).Text);
                    setup.bl_received = Convert.ToDouble((grdToReceiver.Rows[i].FindControl("lblBulkLiters") as Label).Text);
                    setup.lp_received = Convert.ToDouble((grdToReceiver.Rows[i].FindControl("LPLiters") as Label).Text);
                    from83.ReceiverInput.Add(setup);
                    i++;

                }
                int j = 0;
                from83.ReceiverOutput = new List<FReceiverOuput>();
                dt2 = ViewState["Store"] as DataTable;
                foreach (DataRow dr1 in dt2.Rows)
                {
                    FReceiverOuput output = new FReceiverOuput();
                    output.to_storagevat = (gridToStore.Rows[j].FindControl("lblVatCode") as Label).Text;
                    output.removal_date = (gridToStore.Rows[j].FindControl("lblDateofRemoval") as Label).Text;
                    output.removal_hour = (gridToStore.Rows[j].FindControl("lblHoursofRemoval") as Label).Text;
                    output.bl_tostorage = Convert.ToDouble((gridToStore.Rows[j].FindControl("lblBulkLiters") as Label).Text);
                    output.lp_tostorage = Convert.ToDouble((gridToStore.Rows[j].FindControl("lblLPLiters") as Label).Text);
                    from83.ReceiverOutput.Add(output);
                    j++;
                }
                string val;
                val = BL_FermentertoReceiverForm_83.Approve(from83);
                if (val == "0")
                {

                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("FermentertoReceiverList");
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

       

        protected void btnVATtansfers_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATTransferList");
        }

        protected void txtRBulkLiters_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtStrength.Text !=null)
            {
                txtRLpLiters.Text = (Convert.ToDouble(txtRBulkLiters.Text) * (1 + (Convert.ToDouble(txtStrength.Text) / 100))).ToString();
                }

                else
                {
                    string message = "Enter Strength";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    txtStrength.Focus();
                    txtRBulkLiters.Text = "";
                }
            }
        }

        protected void lnkOpeningBalance_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("OpeningBalanceList");

        }

        protected void ddlStorageVat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Form82 vats = new Form82();
                vats = BL_Form82.Getvatavl(ddlStorageVat.SelectedValue, party_code.Value);
                txtAvailableQty.Value = Convert.ToString(vats.vat_availablecapacity);
            }
        }

        protected void ddlSetupDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (ddlSetupDate.SelectedValue != "Select")
                {
                    string a = txtgpd.Value;

                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    List<Distillation> vat = new List<Distillation>();
                    string date = ddlDistillationDate.SelectedValue;
                    vat = BL_Distillation.GetVat(date,Convert.ToInt32( ddlSetupDate.SelectedValue));
                    var list = from s in vat
                               where s.party_code == Session["party_code"].ToString()
                               select s;
                    ddlFermenter.DataSource = list.ToList();
                    ddlFermenter.DataTextField = "vat_name";
                    ddlFermenter.DataValueField = "vat_code";
                    ddlFermenter.DataBind();
                    ddlFermenter.Items.Insert(0, "Select");
                    // CalendarExtender.SelectedDate = Convert.ToDateTime(txtgpd.Value);
                }
            }
        }
    }
}