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
    public partial class DistillationSetupForm : System.Web.UI.Page
    {
      string _party_code;
        DataTable dt = new DataTable();
       static Distillation Distillation = new Distillation();
        string date = "";
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if(!IsPostBack)
            {
                txtStartDat.ReadOnly = true;
                txtEndDat.ReadOnly = true;
                txtDateofDistillation.ReadOnly = true;
                CalendarExtender2.EndDate= DateTime.Now;
                
                //if(date!="")
                //{
                    CalendarExtender4.EndDate = DateTime.Now;
                //}
              
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {

                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
             
                if (ViewState["Records"] == null)
                {
                    dt.Columns.Add("vat_code");
                    dt.Columns.Add("Receiver VAT");
                    dt.Columns.Add("Bulk Liters");
                    dt.Columns.Add("LP Liters");
                    dt.Columns.Add("Doc_id");
                    ViewState["Records"] = dt;
                }

                Session["UserID"] = Session["UserID"];
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if(user != null)
                    {
                        Session["Rolename"] = user.role_name;
                     
                        Session["party_code"] = user.party_code;
                        Session["financial_year"] = user.financial_year;
                        party_code.Value = user.party_code.ToString();
                        //   _party_code = user.party_code.ToString();
                        List<VAT_Master> vats = new List<VAT_Master>();
                vats = BL_VATMaster.GetvatmasterList(Session["party_code"].ToString());
                var list1 = from s in vats
                           where s.party_code == Session["party_code"].ToString() && s.vat_type_code == "rec" || s.vat_type_code == "REC"
                            select s;
                ddlReceiverVAT.DataSource = list1.ToList();
                ddlReceiverVAT.DataTextField = "vat_name";
                ddlReceiverVAT.DataValueField = "vat_code";
                ddlReceiverVAT.DataBind();
                ddlReceiverVAT.Items.Insert(0, "Select");

                List<Form82> f82 = new List<Form82>();
            f82 =BL_Form82.Getdistinctdate(party_code.Value);
            var list = from s in f82
                     
                       select s;
            ddlDateofSetup.DataSource = list.ToList();
           ddlDateofSetup.DataTextField = "setup_date";
         //  ddlDateofSetup.DataValueField = "rawmaterial_fermenter_id";
            ddlDateofSetup.DataBind();
           ddlDateofSetup.Items.Insert(0, "Select");
            //party_code.Value = 
          

                btnApprove.Visible = false;
                btnReject.Visible = false;
                approverremaks.Visible = false;
                approverid.Visible = false;
                if (Session["UserID"].ToString() == "Admin")
                {

                    btnSaveAsDraft.Visible = true;
                    btnSubmit.Visible = true;
                    btnCancel.Visible = true;
                    btnApprove.Visible = true;
                    btnReject.Visible = true;
                }
                else if (Session["Rolename"].ToString()== "Bond Officer")
                {
                    
                    btnSaveAsDraft.Visible = false;
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
                    btnSaveAsDraft.Visible = true;
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


                if (Session["rtype"].ToString() != "0")
                {
                    if(txtgpd.Value !="")
                    {
                        CalendarExtender3.StartDate = Convert.ToDateTime(txtStartDat.Text);
                            
                    }
                  
                        approverid.Visible = true;
                    Distillation = new Distillation();
                    int RFID = Convert.ToInt32( Session["RawmaterialId"].ToString());
                    Distillation =BL_Distillation.GetDetails(Convert.ToInt32(Session["DistillationId"].ToString()), Session["Difinancial_year"].ToString());
                            Session["Rawfid"] = Distillation.rawmaterial_fermenter_id;
                    if (Distillation.record_status == "Y" || Distillation.record_status == "A" )
                    {
                        f82 = BL_Form82.GetList();
                        var list3 = from s in f82
                                   where s.party_code == Session["party_code"].ToString() && s.record_status == "Y" && s.rawmaterial_fermenter_id==RFID
                                   select s;
                        ddlDateofSetup.DataSource = list3.ToList();
                        ddlDateofSetup.DataTextField = "setup_date";
                        // ddlDateofSetup.DataValueField = "rawmaterial_fermenter_id";
                        ddlDateofSetup.DataBind();

                       
                    }
                    else
                    {
                               
                               
                                ddlDateofSetup.SelectedValue = Distillation.setup_date;
                    }
                  
                   
                   txtdob1.Value= Distillation.distillation_date;
                    ddlFermenter.SelectedValue = Distillation.tofermentervat;
                    List<Form82> f8 = new List<Form82>();
                    f8 = BL_Form82.GetList();
                    var list2 = from s in f8
                                where s.tofermentervat == Distillation.tofermentervat && s.setup_date == Distillation.setup_date
                                select s;
                    ddlFermenter.DataSource = list2.ToList();
                    ddlFermenter.DataTextField = "vat_name";
                    ddlFermenter.DataValueField = "tofermentervat";
                    ddlFermenter.DataBind();
                    ddlFermenter.SelectedValue = Distillation.vat_name.ToString();
                    txtTotalBL.Value = Distillation.total_bl_washsetup.ToString();
                    txtMolassesUsed.Value = Distillation.total_qty_transferred.ToString();
                    txtwashcask.Value = Distillation.sg_spentwash.ToString();
                    txtDateofDistillation.Text = Distillation.distillation_date;
                        CalendarExtender4.SelectedDate = Convert.ToDateTime(Distillation.distillation_date);
                            if(ddlDateofSetup.SelectedValue !="")
                            {
                                date = Convert.ToDateTime(ddlDateofSetup.SelectedItem.ToString()).ToString("dd-MM-yyyy");
                                CalendarExtender4.StartDate = Convert.ToDateTime(ddlDateofSetup.SelectedItem.ToString());
                            }
                     
                       
                        txtFinalSpecificGravity.Value = Distillation.final_sg.ToString();
                    txtDegreeofAttenuation.Value = Distillation.degree_of_attenuation.ToString();
                    txtVatorCasks.Value = Distillation.no_of_vat_cask;
                    txtStartDat.Text = Distillation.start_date;
                        CalendarExtender2.SelectedDate= Convert.ToDateTime(Distillation.start_date);
                        CalendarExtender2.StartDate= Convert.ToDateTime(Distillation.distillation_date);
                        txtgpd.Value= Distillation.start_date;
                    txtStartTim.Value = Distillation.start_time;
                    txtEndDat.Text = Distillation.end_date;
                        CalendarExtender3.SelectedDate = Convert.ToDateTime(Distillation.end_date);
                        CalendarExtender3.StartDate= Convert.ToDateTime(Distillation.start_date);
                        txtEndTim.Value = Distillation.end_time;
                    txtgpd1.Value= Distillation.end_date;
                    txtBulkLitres1.Value = Distillation.bl_to_still.ToString();
                    txtToWhicStill.Value = Distillation.to_which_still.ToString();
                    txtTotalBLRemoved.Value = Distillation.total_bl_removed_from_distillation.ToString();
                    txtTotalLPLitresRemoved.Value = Distillation.total_lp_removed_from_distillation.ToString();
                            Session["recordstatus"] = Distillation.record_status;
                //  if (txtgpd.Value != "")
                //{
                //    CalendarExtender3.StartDate = Convert.ToDateTime(txtgpd.Value);
                //}
                //else
                //{
                //    CalendarExtender3.StartDate = DateTime.Now;
                //}

                //if (txtgpd1.Value != "")
                //{
                //    CalendarExtender2.EndDate = Convert.ToDateTime(txtgpd1.Value);
                //}
                //else
                //{
                //    CalendarExtender2.EndDate = DateTime.Now;
                //}
                    if (Distillation.distillation_complete == "Y")
                    {
                        RadioButton1.Checked = true;
                    }
                    else
                    {
                        RadioButton2.Checked = true;
                    }


                    txtBulk.Value = Distillation.bl_redistillation.ToString();
                    txtVessel.Value = Distillation.from_vessel;
                    txtStillRemoved.Value = Distillation.to_which_still_removed;
                    txtBulkLitresp.Value = Distillation.bl_produced.ToString();
                    txtLPLitresp.Value = Distillation.lp_produced.ToString();
                    txtBulkOfMaterials.Value = Distillation.bl_per_material.ToString();
                    txtLPspritinwash.Value = Distillation.lp_per_material.ToString();
                    txtDeg10Attenuation.Value = Distillation.degree_per100wash.ToString();
                    txtSpiritPages.Value = Distillation.spirit_charge_register.ToString();
                    txtRemarks.Value = Distillation.remarks;
                
                    for (int i = 0; i < Distillation.DStore.Count; i++)
                    {
                        if (i == 0)
                           dummytable.Visible = false;
                        dt = (DataTable)ViewState["Records"];
                        dt.Rows.Add(Distillation.DStore[i].vat_code,Distillation.DStore[i].vat_name, Distillation.DStore[i].bl_store, Distillation.DStore[i].lp_store);
                       grdDistillation.DataSource = dt;
                        grdDistillation.DataBind();
                    }
                    List<All_Approvals> approvals = new List<All_Approvals>();
                    approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), Distillation.distillation_id.ToString(), "F82D");
                   
                    if (Distillation.record_status == "Y" || Session["Rolename"].ToString()== "Bond Officer")
                    {
                        foreach (GridViewRow dr1 in grdDistillation.Rows)
                        {
                            ImageButton btn = dr1.FindControl("ImageButton1") as ImageButton;
                            btn.Visible = false;
                        }
                       // Image1.Visible = false;
                    }
                            if (Session["UserID"].ToString() == "Admin")
                            {
                                ddlDateofSetup.Attributes.Add("disabled", "disabled");
                                ddlFermenter.Attributes.Add("disabled", "disabled");
                                txtRemarks.Attributes.Add("disabled", "disabled");
                                ddlReceiverVAT.Enabled = false;
                                txtDateofDistillation.Attributes.Add("disabled", "disabled");
                                txtFinalSpecificGravity.Attributes.Add("disabled", "disabled");
                                txtDegreeofAttenuation.Attributes.Add("disabled", "disabled");
                                txtVatorCasks.Attributes.Add("disabled", "disabled");
                                txtStartTim.Attributes.Add("disabled", "disabled");
                                txtStartDat.Attributes.Add("disabled", "disabled");
                                txtEndDat.Attributes.Add("disabled", "disabled");
                                txtEndTim.Attributes.Add("disabled", "disabled");
                                //txtBulkLitres1.Attributes.Add("disabled", "disabled");
                                //txtTotalBLRemoved.Attributes.Add("disabled", "disabled");
                                //txtTotalLPLitresRemoved.Attributes.Add("disabled", "disabled");
                                txtStillRemoved.Attributes.Add("disabled", "disabled");

                                txtBulkLitresp.Attributes.Add("disabled", "disabled");
                                txtLPLitresp.Attributes.Add("disabled", "disabled");
                                txtBulkOfMaterials.Attributes.Add("disabled", "disabled");
                                txtLPspritinwash.Attributes.Add("disabled", "disabled");
                                //txtToWhicStill.Attributes.Add("disabled", "disabled");
                                txtBulkLiters.Attributes.Add("disabled", "disabled");
                                txtLPLiters.Attributes.Add("disabled", "disabled");
                                txtBulk.Attributes.Add("disabled", "disabled");
                                txtVessel.Attributes.Add("disabled", "disabled");
                                txtDeg10Attenuation.Attributes.Add("disabled", "disabled");
                                txtSpiritPages.Attributes.Add("disabled", "disabled");
                                grdDistillation.Columns[grdDistillation.Columns.Count - 1].Visible = false;
                                Image2.Visible = false;
                                RadioButton1.Enabled = false;
                                RadioButton2.Enabled = false;
                                approverremaks.Visible = false;
                                btnAdd.Visible = false;
                                btnSaveAsDraft.Visible = false;
                                btnupdate.Visible = true;
                                btnSubmit.Visible = false;
                                btnCancel.Visible = true;
                                btnApprove.Visible = false;
                                btnReject.Visible = false;
                                var list4 = (from s in approvals
                                             where s.user_id == "Admin"
                                             select s);
                                grdApprovalDetails.DataSource = list4.ToList();
                                grdApprovalDetails.DataBind();
                            }
                            else
                            {
                                var list4 = (from s in approvals
                                             where s.financial_year== Session["Difinancial_year"].ToString()
                                select s);
                                grdApprovalDetails.DataSource = list4.ToList();
                               // grdApprovalDetails.DataSource = approvals;
                                grdApprovalDetails.DataBind();
                            }
                            if ((Session["rtype"].ToString() == "1"))
                    {
                        if (Distillation.record_status == "A" || Distillation.record_status=="R")
                        {
                            txtApproverremarks.Attributes.Add("disabled", "disabled");
                            btnApprove.Visible = false;
                            btnReject.Visible = false;
                            grdDistillation.Columns[grdDistillation.Columns.Count - 1].Visible = false;
                         // Image1.Visible = false;

                        }
                       ddlDateofSetup.Attributes.Add("disabled", "disabled");
                        ddlFermenter.Attributes.Add("disabled", "disabled");
                        txtRemarks.Attributes.Add("disabled", "disabled");
                        ddlReceiverVAT.Enabled = false;
                        txtDateofDistillation.Attributes.Add("disabled", "disabled");
                        txtFinalSpecificGravity.Attributes.Add("disabled", "disabled");
                        txtDegreeofAttenuation.Attributes.Add("disabled", "disabled");
                       txtVatorCasks.Attributes.Add("disabled", "disabled");
                       txtStartTim.Attributes.Add("disabled", "disabled");
                        txtStartDat.Attributes.Add("disabled", "disabled");
                        txtEndDat.Attributes.Add("disabled", "disabled");
                        txtEndTim.Attributes.Add("disabled", "disabled");
                        txtBulkLitres1.Attributes.Add("disabled", "disabled");
                       txtTotalBLRemoved.Attributes.Add("disabled", "disabled");
                        txtTotalLPLitresRemoved.Attributes.Add("disabled", "disabled");
                        txtStillRemoved.Attributes.Add("disabled", "disabled");

                        txtBulkLitresp.Attributes.Add("disabled", "disabled");
                        txtLPLitresp.Attributes.Add("disabled", "disabled");
                        txtBulkOfMaterials.Attributes.Add("disabled", "disabled");
                        txtLPspritinwash.Attributes.Add("disabled", "disabled");
                        txtToWhicStill.Attributes.Add("disabled", "disabled");
                        txtBulkLiters.Attributes.Add("disabled", "disabled");
                        txtLPLiters.Attributes.Add("disabled", "disabled");
                        txtBulk.Attributes.Add("disabled", "disabled");
                        txtVessel.Attributes.Add("disabled", "disabled");
                        txtDeg10Attenuation.Attributes.Add("disabled", "disabled");
                        txtSpiritPages.Attributes.Add("disabled", "disabled");
                        RadioButton1.Enabled = false;
                        RadioButton2.Enabled = false;
                        approverremaks.Visible = false;
                        btnAdd.Visible = false;
                        btnSaveAsDraft.Visible = false;
                        btnSubmit.Visible = false;
                        btnCancel.Visible = false;
                        //   txtApproverremarks.Attributes.Add("disabled", "disabled");
                        if (Session["Rolename"].ToString() == "Bond Officer")
                        {
                            btnSaveAsDraft.Visible = false;
                            btnSubmit.Visible = false;
                            btnCancel.Visible = false;
                            btnfermenter.Visible = false;
                            if (Distillation.record_status == "Y")
                            {
                                approverremaks.Visible = true;

                                //txtApproverremarks.Attributes.Add("disabled", "disabled");
                            }
                            else
                            {
                                approverremaks.Visible = false;
                            }
                         
                        }

                        grdDistillation.Columns[grdDistillation.Columns.Count - 1].Visible = false;
                       // Image1.Visible = false;
                        if (Session["Rolename"].ToString() == "Bond Officer" && Distillation.record_status == "Y")
                        {
                            approverremaks.Visible = true;
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
        protected void lnkRawMaterialWastage_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawmaterialWastageList.aspx");
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

        protected void lnkRMR_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialReceiptList");
         }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = 0;
            Response.Redirect("DistillationSetupList.aspx");
        }

        protected void btnVATtansfers_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATTransferList");
        }

        protected void btnDistillation_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DistillationSetupList.aspx");

        }

        protected void btnfermenter_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialtoFermenterList");
           

        }
        protected void ddlDateofSetup_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (IsPostBack)
            {
                if(ddlDateofSetup.SelectedValue !="")
                {
                 
                    UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
         //   Session["party_code"] = user.party_code;
             date =Convert.ToDateTime( ddlDateofSetup.SelectedValue).ToString("dd-MM-yyyy");
                    ddsetup.Value = date;
                   CalendarExtender4.StartDate = Convert.ToDateTime(ddsetup.Value);
                    CalendarExtender4.EndDate = DateTime.Now;
                    List<Form82> f82 = new List<Form82>();
                    f82 = BL_Form82.GetList();
            var list1 = from s in f82
                        where s.party_code == Session["party_code"].ToString() && s.setup_date==date && s.setup_complete =="N" 
                        select s;
            ddlFermenter.DataSource = list1.ToList();
            ddlFermenter.DataTextField = "vat_name";
            ddlFermenter.DataValueField = "tofermentervat";
                ddlFermenter.DataBind();
            ddlFermenter.Items.Insert(0, "Select");
                    txtDateofDistillation.Text = "";
                  
                }
            }
        }

        protected void ddlFermenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if(ddlFermenter.SelectedValue!=null)
                {

                
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
               // Session["party_code"] = user.party_code;
                string date = Convert.ToDateTime(ddlDateofSetup.SelectedItem.ToString()).ToString("dd-MM-yyyy");
                    string code = ddlFermenter.SelectedValue;
                Form82 f82 = new Form82(); 
                f82 = BL_Form82.Getfermentervat(date,code);
                txtMolassesUsed.Value = f82.total_qty_transferred.ToString();
                txtTotalBL.Value = f82.total_bl_washsetup.ToString();
                txtwashcask.Value = f82.sg_of_wash.ToString();
                }
            }
        }

        int Doc_id = 1;
        protected void Add(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                if (ddlReceiverVAT.SelectedItem !=null)
                {
                    // get value and text which item you have selected
                    string vat_code = ddlReceiverVAT.SelectedValue;
                    string recivervat = ddlReceiverVAT.SelectedItem.ToString();
                    //add the selected item to gridview
                    DataTable dtgridview = ViewState["Records"] as DataTable;
                    DataRow dr2 = dtgridview.NewRow();
                    bool ifExist = false;
                    foreach (DataRow dr in dtgridview.Rows)
                    {
                        if (dr["Receiver VAT"].ToString() == ddlReceiverVAT.SelectedItem.ToString())
                        {
                            ifExist = true;
                            break;
                        }
                    }
                    if (!ifExist)
                    {
                      
                        dummytable.Visible = false;
                        string m = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-sss");
                        dt = (DataTable)ViewState["Records"];
                        string vat_code1 = ddlReceiverVAT.SelectedValue;
                        string recivervat1 = ddlReceiverVAT.SelectedItem.ToString();
                        dt.Rows.Add(vat_code1, recivervat1, txtBulkLiters.Text, txtLPLiters.Value, Doc_id);
                        grdDistillation.DataSource = dt;
                        grdDistillation.DataBind();
                        Doc_id++;
                        grdDistillation.Visible = true;
                        ddlReceiverVAT.ClearSelection();
                        txtBulkLiters.Text = "";
                        txtLPLiters.Value = "";
                        //for (int i = 0; i <grdDistillation.Rows.Count; i++)
                        //{
                        //    GridViewRow row1 = grdDistillation.Rows[i];
                        //    string Qty1 = (row1.Cells[1].FindControl("lblMolassesUsed") as Label).Text;
                        //    total += Convert.ToDouble(Qty1);
                        //    lbltotal.Text = total.ToString();
                        //    molassestotal.Visible = true;
                        //}
                        //txtAvailableStock.Value = "";
                    }
                    else
                    {
                        // this.lbgvck.Visible = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('ReceiverVat Already exist !!!!!\');", true);
                        ddlReceiverVAT.ClearSelection();
                        txtBulkLiters.Text = "";
                        txtLPLiters.Value = "";

                    }

                }


            //    if (IsPostBack)
            //{
            //    double total = 0;
            //    dummytable.Visible = false;
            //    string m = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-sss");
            //    dt = (DataTable)ViewState["Records"];
            //    string vat_code = ddlReceiverVAT.SelectedValue;
            //    string recivervat = ddlReceiverVAT.SelectedItem.ToString();
            //    dt.Rows.Add(vat_code,recivervat, txtBulkLiters.Value,txtLPLiters.Value, Doc_id);
            //    grdDistillation.DataSource = dt;
            //    grdDistillation.DataBind();
            //    Doc_id++;
            //    grdDistillation.Visible = true;
            //   ddlReceiverVAT.ClearSelection();
            //    txtBulkLiters.Value = "";
            //  txtLPLiters.Value = "";
            //    //for (int i = 0; i <grdDistillation.Rows.Count; i++)
            //    //{
            //    //    GridViewRow row1 = grdDistillation.Rows[i];
            //    //    string Qty1 = (row1.Cells[1].FindControl("lblMolassesUsed") as Label).Text;
            //    //    total += Convert.ToDouble(Qty1);
            //    //    lbltotal.Text = total.ToString();
            //    //    molassestotal.Visible = true;
            //    //}
            //    //txtAvailableStock.Value = "";
            }
        }


        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Distillation distillation = new Distillation();
                if (txtDateofDistillation.Text == "" || txtDateofDistillation.Text != "")
                {
                    txtDateofDistillation.Text = txtdob1.Value;
                }
                if (txtStartDat.Text == "" || txtStartDat.Text != "")
                {
                    txtStartDat.Text = txtgpd.Value;
                }
                if (txtEndDat.Text == "" || txtEndDat.Text != "")
                {
                    txtEndDat.Text = txtgpd1.Value;
                }
                //if(txtStartTim.Value=="")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Enter StartTime');", true);
                //}
                distillation.final_sg = Convert.ToDouble(txtFinalSpecificGravity.Value);
                distillation.distillation_date= Convert.ToDateTime(txtDateofDistillation.Text).ToString("dd-MM-yyyy");
                distillation.degree_of_attenuation= Convert.ToDouble(txtDegreeofAttenuation.Value);
                distillation.no_of_vat_cask = txtVatorCasks.Value;
                distillation.start_date = txtStartDat.Text;
                distillation.vat_code = ddlFermenter.SelectedValue;
               // distillation.rawmaterial_fermenter_id=Convert.ToInt32( ddlDateofSetup.SelectedValue);
                distillation.setup_date = ddlDateofSetup.SelectedItem.ToString();
                distillation.start_time = txtStartTim.Value;
                distillation.party_code = party_code.Value;
                distillation.end_date = txtEndDat.Text;
                distillation.end_time = txtEndTim.Value;
                distillation.bl_to_still = Convert.ToDouble(txtBulkLitres1.Value);
                distillation.to_which_still = txtToWhicStill.Value;
                distillation.total_bl_removed_from_distillation = Convert.ToDouble(txtTotalBLRemoved.Value);
                distillation.total_lp_removed_from_distillation = Convert.ToDouble(txtTotalLPLitresRemoved.Value);
                if(RadioButton1.Checked)
                {
                    distillation.distillation_complete = "Y";
                 }
                else
                {
                    distillation.distillation_complete = "N";
                }
                distillation.bl_redistillation = Convert.ToDouble(txtBulk.Value);
                distillation.from_vessel = txtVessel.Value;
                distillation.to_which_still_removed = txtStillRemoved.Value;
                distillation.bl_produced = Convert.ToDouble(txtBulkLitresp.Value);
                distillation.lp_produced = Convert.ToDouble(txtLPLitresp.Value);
                distillation.bl_per_material = Convert.ToDouble(txtBulkOfMaterials.Value);
                distillation.lp_per_material = Convert.ToDouble(txtLPspritinwash.Value);
                distillation.degree_per100wash = Convert.ToDouble(txtDeg10Attenuation.Value);
                distillation.spirit_charge_register = txtSpiritPages.Value;
                distillation.financial_year = Session["financial_year"].ToString();
                distillation.remarks = txtRemarks.Value;
                
                    distillation.record_status = "N";
              
                distillation.user_id = Session["UserID"].ToString();
             
                int i = 0;
               distillation.DStore = new List<DistillationToStore>();
                dt = ViewState["Records"] as DataTable;
                foreach (DataRow dr in dt.Rows)
                {
                    DistillationToStore setup = new DistillationToStore();
                    //setup.distillation_tostore_id= Convert.ToInt32((grdDistillation.Rows[i].FindControl("lblDistillationID") as Label).Text);
                   setup.vat_code= (grdDistillation.Rows[i].FindControl("lblvatcode") as Label).Text;
                    setup.bl_store= Convert.ToDouble((grdDistillation.Rows[i].FindControl("lblBulkLiters") as Label).Text);
                    setup.lp_store= Convert.ToDouble((grdDistillation.Rows[i].FindControl("lblLPLiters") as Label).Text);
                    distillation.DStore.Add(setup);
                    i++;
                }
                string val;
                if (Session["rtype"].ToString() == "0")
                    val = BL_Distillation.Insert(distillation);
                else
                {
                   distillation.distillation_id = Convert.ToInt32(Session["DistillationId"].ToString());
                   // distillation.rawmaterial_fermenter_id= Convert.ToInt32(Session["RawmaterialId"].ToString());
                    val = BL_Distillation.Update(distillation);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("DistillationSetupList.aspx");
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
                if(grdDistillation.Rows.Count==0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Add Spirit produced to store details');", true);
                    ddlReceiverVAT.ClearSelection();
                    ddlReceiverVAT.Focus();
                    txtBulkLiters.Text = "";
                    txtLPLiters.Value = "";
                }
                else
                { 
                Distillation distillation = new Distillation();
                  if(RadioButton1.Checked==false && RadioButton2.Checked==false)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Select distillation complete for the setup date yes or no ');", true);
                        RadioButton1.Focus();
                    }
                  else
                    { 
                
                    txtDateofDistillation.Text = txtdob1.Value;
                    txtStartDat.Text = txtgpd.Value;
                    txtEndDat.Text = txtgpd1.Value;
                distillation.final_sg = Convert.ToDouble(txtFinalSpecificGravity.Value);
                distillation.distillation_date = Convert.ToDateTime(txtDateofDistillation.Text).ToString("dd-MM-yyyy");
                distillation.degree_of_attenuation = Convert.ToDouble(txtDegreeofAttenuation.Value);
                distillation.no_of_vat_cask = txtVatorCasks.Value;
                distillation.start_date = txtStartDat.Text;
                distillation.vat_code = ddlFermenter.SelectedValue;
               // distillation.rawmaterial_fermenter_id = Convert.ToInt32(ddlDateofSetup.SelectedValue);
                distillation.setup_date = ddlDateofSetup.SelectedItem.ToString();
                distillation.start_time = txtStartTim.Value;
                distillation.end_date = txtEndDat.Text;
                distillation.party_code = party_code.Value;
                distillation.end_time = txtEndTim.Value;
                distillation.bl_to_still = Convert.ToDouble(txtBulkLitres1.Value);
                distillation.to_which_still = txtToWhicStill.Value;
                distillation.total_bl_removed_from_distillation = Convert.ToDouble(txtTotalBLRemoved.Value);
                distillation.total_lp_removed_from_distillation = Convert.ToDouble(txtTotalLPLitresRemoved.Value);
                if (RadioButton1.Checked)
                {
                    distillation.distillation_complete = "Y";
                }
                else
                {
                    distillation.distillation_complete = "N";
                }
                distillation.bl_redistillation = Convert.ToDouble(txtBulk.Value);
                distillation.from_vessel = txtVessel.Value;
                distillation.to_which_still_removed = txtStillRemoved.Value;
                distillation.bl_produced = Convert.ToDouble(txtBulkLitresp.Value);
                distillation.lp_produced = Convert.ToDouble(txtLPLitresp.Value);
                distillation.bl_per_material = Convert.ToDouble(txtBulkOfMaterials.Value);
                distillation.lp_per_material = Convert.ToDouble(txtLPspritinwash.Value);
                distillation.degree_per100wash = Convert.ToDouble(txtDeg10Attenuation.Value);
                distillation.spirit_charge_register = txtSpiritPages.Value;
                distillation.remarks = txtRemarks.Value;
                 distillation.financial_year = Session["financial_year"].ToString();
                        //distillation.record_status = "N";
                        distillation.DStore = new List<DistillationToStore>();
              
                distillation.user_id = Session["UserID"].ToString();
                for (int i = 0; i < grdDistillation.Rows.Count; i++)
                {
                    DistillationToStore setup = new DistillationToStore();
                    //setup.distillation_tostore_id= Convert.ToInt32((grdDistillation.Rows[i].FindControl("lblDistillationID") as Label).Text);
                    setup.vat_code = (grdDistillation.Rows[i].FindControl("lblvatcode") as Label).Text;
                    setup.bl_store = Convert.ToDouble((grdDistillation.Rows[i].FindControl("lblBulkLiters") as Label).Text);
                    setup.lp_store = Convert.ToDouble((grdDistillation.Rows[i].FindControl("lblLPLiters") as Label).Text);
                    distillation.DStore.Add(setup);

                }
               
                            distillation.record_status = "Y";
                      

                        string val;

                if (Session["rtype"].ToString() == "0")
                    val = BL_Distillation.Insert(distillation);
                else
                {
                    distillation.distillation_id = Convert.ToInt32(Session["DistillationId"].ToString());
               //     distillation.rawmaterial_fermenter_id = Convert.ToInt32(Session["RawmaterialId"].ToString());
                    val = BL_Distillation.Update(distillation);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("DistillationSetupList");
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
                grdDistillation.DataSource = dt1;
                grdDistillation.DataBind();
                if (dt1.Rows.Count < 1)
                    dummytable.Visible = true;
            }
        }

        //public static string chkDuplicateDates(Object scpdate)
        //{
        //    int value = 0;
        //    if (scpdate.ToString().Length > 1)
        //    {
        //        if (entrydate != scpdate.ToString())
        //            value = BL_User_Mgnt.GetExistsData("fermenter_setup", "fromstoragevat", scpdate.ToString());
        //    }
        //    return value.ToString();
        //}



        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Distillation ferm = new Distillation();

                ferm.record_status = "A";
                string val;
                ferm.distillation_id = Convert.ToInt32(Session["DistillationId"].ToString());
                ferm.rawmaterial_fermenter_id= Convert.ToInt32(Session["Rawfid"].ToString());
                ferm.remarks = txtApproverremarks.Value;
                ferm.party_code = party_code.Value;
                ferm.financial_year = Session["financial_year"].ToString();
                ferm.tofermentervat = ddlFermenter.SelectedValue;
                ferm.user_id = Session["UserID"].ToString();
                ferm.DStore = new List<DistillationToStore>();

                ferm.user_id = Session["UserID"].ToString();
                for (int i = 0; i < grdDistillation.Rows.Count; i++)
                {
                    DistillationToStore setup = new DistillationToStore();
                    //setup.distillation_tostore_id= Convert.ToInt32((grdDistillation.Rows[i].FindControl("lblDistillationID") as Label).Text);
                    setup.vat_code = (grdDistillation.Rows[i].FindControl("lblvatcode") as Label).Text;
                    setup.bl_store = Convert.ToDouble((grdDistillation.Rows[i].FindControl("lblBulkLiters") as Label).Text);
                    setup.lp_store = Convert.ToDouble((grdDistillation.Rows[i].FindControl("lblLPLiters") as Label).Text);
                    ferm.DStore.Add(setup);

                }
                val = BL_Distillation.Approve(ferm);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("DistillationSetupList.aspx");
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
               Distillation ferm = new Distillation();

                ferm.record_status = "R";
                string val;
                ferm.distillation_id= Convert.ToInt32(Session["DistillationId"].ToString());
                ferm.remarks = txtApproverremarks.Value;
                ferm.financial_year = Session["financial_year"].ToString();
                ferm.party_code = party_code.Value;
                ferm.user_id = Session["UserID"].ToString();
                val = BL_Distillation.Approve(ferm);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("DistillationSetupList.aspx");
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
              Session["UserID"] = Session["UserID"];
            Response.Redirect("DistillationSetupList");
        }

        //protected void txtEndDat_TextChanged(object sender, EventArgs e)
        //{
        //    if(IsPostBack)
        //    { 
        //    if(txtStartDat.Text !="" && txtEndDat.Text !="")
        //    {
        //        var stdate = Convert.ToDateTime(txtStartDat.Text);
        //        var enddate = Convert.ToDateTime(txtEndDat.Text);

        //        if( enddate >= stdate)
        //        {

        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('EndDate used can be less then or equal to StartDate \');", true);
        //            txtEndDat.Text = "";
        //            txtEndDat.Focus();
        //        }

        //    }
        //}
        //}

        protected void lnkOpeningBalance_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("OpeningBalanceList");

        }

        protected void txtStartDat_TextChanged(object sender, EventArgs e)
        {
            if(IsPostBack)
            {

            
           
          if (txtgpd.Value != "")
            {
                CalendarExtender3.StartDate = Convert.ToDateTime(txtgpd.Value);
            }
            else
            {
                CalendarExtender3.StartDate = Convert.ToDateTime(txtStartDat.Text);
            }
            }
        }

        protected void txtEndDat_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

                if (txtgpd1.Value != "")
                {
                    CalendarExtender2.EndDate = Convert.ToDateTime(txtgpd1.Value);
                }
                else
                {
                    CalendarExtender2.EndDate = Convert.ToDateTime(txtEndDat.Text);
                }
            }
        }

        protected void txtDateofDistillation_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtdob1.Value != "")
                {
                    CalendarExtender2.StartDate = Convert.ToDateTime(txtdob1.Value);
                }
                else
                {
                    CalendarExtender2.StartDate = Convert.ToDateTime(txtDateofDistillation.Text);
                }
            }
        }

        protected void ddlReceiverVAT_SelectedIndexChanged(object sender, EventArgs e)
        { if(IsPostBack)
            {
                Form82 vats = new Form82();
                vats = BL_Form82.Getvatavl(ddlReceiverVAT.SelectedValue, party_code.Value);
                txtAvailableQty.Value = Convert.ToString(vats.vat_availablecapacity);
            }
          
        }

        protected void txtBulkLiters_TextChanged(object sender, EventArgs e)
        {
            if (txtBulkLiters.Text != "")
            {
                if (Convert.ToDouble(txtBulkLiters.Text) <= Convert.ToDouble(txtAvailableQty.Value))
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('BulkLiters used can be less then or equal to Available Stock  \');", true);
                    txtBulkLiters.Focus();
                    txtBulkLiters.Text = "";
                }
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Distillation distillation = new Distillation();
             
                if (txtStartDat.Text == "" || txtStartDat.Text != "")
                {
                    txtStartDat.Text = txtgpd.Value;
                }
                if (txtEndDat.Text == "" || txtEndDat.Text != "")
                {
                    txtEndDat.Text = txtgpd1.Value;
                }
              
                if (RadioButton1.Checked)
                {
                    distillation.distillation_complete = "Y";
                }
                else
                {
                    distillation.distillation_complete = "N";
                }
                distillation.start_date = txtStartDat.Text;
                distillation.end_date = txtEndDat.Text;
                distillation.bl_to_still = Convert.ToDouble(txtBulkLitres1.Value);
                distillation.to_which_still = txtToWhicStill.Value;
                distillation.total_bl_removed_from_distillation = Convert.ToDouble(txtTotalBLRemoved.Value);
                distillation.total_lp_removed_from_distillation = Convert.ToDouble(txtTotalLPLitresRemoved.Value);
             distillation.financial_year = Session["Difinancial_year"].ToString();
                distillation.user_id = Session["UserID"].ToString();

               
                string val;
               
                    distillation.distillation_id = Convert.ToInt32(Session["DistillationId"].ToString());
                    // distillation.rawmaterial_fermenter_id= Convert.ToInt32(Session["RawmaterialId"].ToString());
                    val = BL_Distillation.AdminUpdate(distillation);
                
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("DistillationSetupList.aspx");
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

        //protected void ddlDateofSetup_TextChanged(object sender, EventArgs e)
        //{
        //   if( ddsetup.Value !="")
        //    {
        //        CalendarExtender1.StartDate = Convert.ToDateTime(ddsetup.Value);
        //    }

        //}
    }
}