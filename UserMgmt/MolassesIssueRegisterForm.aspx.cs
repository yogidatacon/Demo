using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class MolassesIssueRegisterForm : System.Web.UI.Page
    {
         List<VAT_Master> vatmasters = new List<VAT_Master>();
         List<Molasses_Issue_Register> mir = new List<Molasses_Issue_Register>();
         List<Molasses_Issue_Register> mir2 = new List<Molasses_Issue_Register>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strPreviousPage = "";
                CalendarExtender.EndDate = DateTime.Now;
                txtDipsinCM.Text = "0";
                txtissuedqut.Text = "0";
                txtDate1.ReadOnly = true;
                if (Request.UrlReferrer != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                }
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/LoginPage");
                }

                string userid = Session["UserID"].ToString();
                Session["UserID"] = Session["UserID"];
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                if (user != null)
                {
                    //List<Party_Master> partymasters = new List<Party_Master>();
                    //partymasters = BL_Party_Master.GetList();
                    //var list = from s in partymasters
                    //           where s.party_code != "ALL" && (s.party_type_code=="DIS"|| s.party_type_code=="d01")
                    //           select s;
                    //ddlPartyName.DataSource = list.ToList();
                    //ddlPartyName.DataTextField = "party_name";
                    //ddlPartyName.DataValueField = "party_code";
                    //ddlPartyName.DataBind();
                    //ddlPartyName.Items.Insert(0, "Select");
                    Session["financial_year"] = user.financial_year;
                    vatmasters = new List<VAT_Master>();
                    vatmasters = BL_VATMaster.GetvatmasterList("Admin");
                    if (userid != "Admin")
                    {
                        var vats = from s in vatmasters
                                   where s.party_code == user.party_code
                                   select s;

                        ddlVatName.DataSource = vats.ToList();
                        partycode.Value = user.party_code;
                    }
                    else
                    {
                        ddlVatName.DataSource = vatmasters;
                    }
                    ddlVatName.DataTextField = "vat_name";
                    ddlVatName.DataValueField = "vat_code";
                    ddlVatName.DataBind();
                    ddlVatName.Items.Insert(0, "Select");
                    ddlVatName.Enabled = false;
                    //List<UOM_Master> uomlist = new List<UOM_Master>();
                    //uomlist = BL_UOM.GetList(Session["UserID"].ToString());
                    //ddlUOM.DataSource = uomlist;
                    //ddlUOM.Attributes.Add("Disabled", "Disabled");
                    //ddlUOM.DataTextField = "uom_name";
                    //ddlUOM.DataValueField = "uom_code";
                    //ddlUOM.DataBind();
                    //ddlUOM.Items.Insert(0, "Select");
                    List<Pass_Details> pass = new List<Pass_Details>();
                    pass = BL_Pass_Details.GetPassList();

                    //var list1 = (from s in pass
                    //            where s.record_status == "I"

                    //            select s);
                    //ddpassno.DataSource = list1.ToList();
                    //ddpassno.DataTextField ="pass_reqno";
                    //ddpassno.DataValueField = "pass_id";
                    //ddpassno.DataBind();
                    //ddpassno.Items.Insert(0, "Select");
                    if (Session["UserID"].ToString() == "Admin")
                    {

                        btnSaveasDraft.Visible = true;
                        btnSubmit.Visible = true;
                        btnCancel.Visible = true;
                        btnApprove.Visible = true;
                        btnReject.Visible = true;
                    }
                    else if (user.role_name == "Bond Officer")
                    {

                        btnSaveasDraft.Visible = false;
                        btnSubmit.Visible = false;
                        btnCancel.Visible = false;
                        btnApprove.Visible = true;
                        btnReject.Visible = true;

                        var list = (from s in pass
                                    where s.from_party == user.party_code && s.record_status == "I"

                                    select s);
                        ddpassno.DataSource = list.ToList();
                        ddpassno.DataTextField = "pass_issuedno";
                        ddpassno.DataValueField = "pass_id";
                        ddpassno.DataBind();
                        ddpassno.Items.Insert(0, "Select");
                        // ddlPartyName.SelectedValue = user.party_code;
                        //ddlPartyName.Attributes.Add("disabled", "disabled");
                    }
                    else
                    {
                        var list = (from s in pass
                                    where s.from_party == user.party_code && s.record_status == "I"

                                    select s);
                        ddpassno.DataSource = list.ToList();
                        ddpassno.DataTextField = "pass_issuedno";
                        ddpassno.DataValueField = "pass_id";
                        ddpassno.DataBind();
                        ddpassno.Items.Insert(0, "Select");
                        btnSaveasDraft.Visible = true;
                        btnSubmit.Visible = true;
                        btnCancel.Visible = true;
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        // ddlPartyName.Attributes.Add("Disabled", "Disabled");
                        approver.Visible = false;
                        approverremaks.Visible = false;
                    }
                    if (Session["rtype"].ToString() != "0")
                    {
                        Molasses_Issue_Register mir1 = new Molasses_Issue_Register();
                        mir1 = BL_Molasses_Issue_Register.GetDetails(Session["mirid"].ToString(), Session["MIRfinancial_year"].ToString());
                        txtDate1.Text = mir1.mir_entrydate.ToString();
                        if (mir1.record_status == "A")
                        {
                            var list = (from s in pass
                                        where s.from_party == user.party_code && (s.record_status == "I" || s.record_status == "M")

                                        select s);
                            ddpassno.DataSource = list.ToList();
                            ddpassno.DataTextField = "pass_issuedno";
                            ddpassno.DataValueField = "pass_id";
                            ddpassno.DataBind();
                            ddpassno.Items.Insert(0, "Select");
                        }
                        else
                        {
                            var list = (from s in pass
                                        where s.from_party == user.party_code && (s.record_status == "I" || s.record_status == "P" )

                                        select s);
                            ddpassno.DataSource = list.ToList();
                            ddpassno.DataTextField = "pass_issuedno";
                            ddpassno.DataValueField = "pass_id";
                            ddpassno.DataBind();
                            ddpassno.Items.Insert(0, "Select");
                        }
                        // ddpassno.Items.Insert(1, mir1.passno);
                        partycode.Value = mir1.party_code;
                        topartycode.Value = mir1.to_party_code;
                        vatcode.Value = mir1.vat_code;
                        txttoparty.Text = mir1.party_name;
                        //  ddlVatName.SelectedValue = mir1.vat_code;
                        //ddlUOM.SelectedValue = mir1.uom;
                        ddpassno.SelectedValue = mir1.passno;
                        ddpassno_SelectedIndexChanged(sender, null);
                        CalendarExtender.SelectedDate = Convert.ToDateTime(mir1.mir_entrydate);
                        txtdob.Value = mir1.mir_entrydate;
                        passno.Value = mir1.passno;
                        passdate.Value = mir1.passdate.Substring(0, 10).Replace("/", "-");
                        issuetype.Value = mir1.issuetype;
                     //   digilock.Value = mir1.digilockno;
                        //issuedqut.Value = mir1.issuedqty.ToString();
                        txtPassDate.Text = mir1.passdate.Substring(0, 10).Replace("/", "-");
                        //  txtissuetype.Text = mir1.dispatch_type_name;
                     //   txtDLN.Text = mir1.digilockno;
                        // txtissuedqut.Text = mir1.issuedqty.ToString();
                        txtvalue.Text = mir1.valuers.ToString();
                        txtBasic.Text = mir1.basicrs.ToString();
                        txtSPL.Text = mir1.splrs.ToString();
                        txtwaste.Text = mir1.destroyedqty.ToString();
                        txtRemarks.Text = mir1.remarks;
                        txtDipsinCM.Text = mir1.closing_dips.ToString();
                        List<All_Approvals> approvals = new List<All_Approvals>();
                        approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), mir1.molassesissueregister_id.ToString(), "MIR");
                        var list4 = (from s in approvals
                                     where s.financial_year == Session["MIRfinancial_year"].ToString()
                                     select s);
                        grdApprovalDetails.DataSource = list4.ToList();

                        grdApprovalDetails.DataBind();
                        if (approvals.Count <= 0)
                        {
                            approver.Visible = false;
                            approverremaks.Visible = false;
                        }
                        else
                        {
                            approver.Visible = true;
                        }
                        txtissuedqut.ReadOnly = true;
                        ddpassno.Enabled = false;
                        if (Session["rtype"].ToString() == "1" || user.role_name == "Bond Officer")
                        {
                            txtDate1.ReadOnly = true;
                            txttoparty.ReadOnly = false;
                            ddlVatName.Enabled = false;
                            txttoparty.ReadOnly = true;
                            //ddlUOM.Attributes.Add("Disabled", "Disabled");

                            txtPassDate.ReadOnly = true;
                            txtissuetype.ReadOnly = true;
                            txtDLN.ReadOnly = true;
                            txtissuedqut.ReadOnly = true;
                            txtvalue.ReadOnly = true;
                            txtBasic.ReadOnly = true;
                            txtSPL.ReadOnly = true;
                            txtwaste.ReadOnly = true;
                            txtRemarks.ReadOnly = true;
                            Image1.Visible = false;
                            txtDipsinCM.ReadOnly = true;
                            if (user.role_name == "Bond Officer" && mir1.record_status == "Y")
                            {
                                btnApprove.Visible = true;
                                btnReject.Visible = true;
                                approver.Visible = true;
                                approverremaks.Visible = true;
                            }
                            else
                            {
                                btnSaveasDraft.Visible = false;
                                btnSubmit.Visible = false;
                                btnCancel.Visible = false;
                                btnApprove.Visible = false;
                                btnReject.Visible = false;
                                approverremaks.Visible = false;
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
        }
       
       // [WebMethod]

        //public static VAT_Master[] LoadVatNames(Object partycode)
        //{
        //    IEnumerable<VAT_Master> projectslist=null;
        //    if (partycode != null)
        //    {
        //        if (partycode.ToString() != "Select" || partycode.ToString() != "")
        //        {
        //            projectslist = (from proj in vatmasters where proj.party_code == partycode.ToString() select proj).AsEnumerable().Select(projt => new VAT_Master() { vat_name = projt.vat_name, vat_code = projt.vat_code });
        //        }
        //    }
        //    return projectslist.ToArray();
          
        //}
      
        [WebMethod]
        protected void btnVATtansfers_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATTransferList");
        }
        public static string GetOpeningBalance(Object partycode)
        {
            string[] values = partycode.ToString().Split('_');
            List<Molasses_Issue_Register> mir = new List<Molasses_Issue_Register>();
            mir = BL_Molasses_Issue_Register.GetOpeningBalanceList();
            string val1 = "";
            if (partycode != null)
            {
                if (values[0] !="null" && values[0] != "Select" && values[1] !="null" && values[1] != "Select")
                {
                    var val = (from proj in mir where proj.party_code == values[0].ToString() && proj.vat_code == values[1].ToString() select new { openingbalance = proj.openingbalance, production = proj.production, issuedqty = proj.issuedqty, remqty = proj.rem_pass_qty, closing_dips = proj.closing_dips, uom = proj.uom });
                    if(val.ToList().Count>0)
                    val1 = val.ToList()[0].openingbalance.ToString() + "_" + val.ToList()[0].production.ToString() + "_" + val.ToList()[0].issuedqty.ToString() + "_" + val.ToList()[0].closing_dips.ToString() + "_" + val.ToList()[0].uom.ToString();
                    
                }
               
            }
            // string val1 = val.ToList()[0].ToString();
            return val1.ToString();
            

        }
        //GetPassDetails
        [WebMethod]
         
    //public static string GetPassDetails(Object passno)
    //    {
          
    //        string val1 = "";
    //        if (passno!= null && passno.ToString() != "Select" && passno.ToString() != "")
    //        {
    //            Pass_Details val= BL_Pass_Details.GetPassDetails(passno.ToString());
              
    //            val1 = val.dispatch_date.ToString() + "_" + val.dispatch_type_name.ToString() + "_" + val.digital_lock_no.ToString() + "_" + val.rem_pass_qty.ToString()+ "_"+val.dispatch_qty+"_" + val.dispatch_type_id.ToString() + "_" + val.customer_id.ToString()+"_"+ val.cutomer_name.ToString();
    //        }
    //        return val1.ToString();

    //    }
        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("MolassesIssueRegisterList");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("MolassesIssueRegisterList");
        }

        protected void lnkbtnAddSCM_Click(object sender, EventArgs e)
        {

        }
        protected void btnRG4_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("SCMSugarCanePurchaseRegList");
        }

        protected void btnDMP_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DailyMolassesProductionList");
        }

        protected void btnMIR_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("MolassesIssueRegisterList");
        }

        protected void btnOpeningBalance_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("OpeningBalanceList");
        }
        [System.Web.Services.WebMethod]
        public static string CheckDuplicates(Object values)
        {
            string val;
            val = "";// BL_Molasses_Issue_Register.GetExistingDetails(values.ToString());
           
                return val.ToString();
        }
        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                btnSaveasDraft.Enabled = false;
                Molasses_Issue_Register mir1 = new Molasses_Issue_Register();
                mir1.mir_entrydate = txtdob.Value;
                mir1.party_code = partycode.Value;
                mir1.vat_code = ddlVatName.SelectedValue;
                mir1.openingbalance = Convert.ToDouble(txtOpeningBalance.Text);
                mir1.to_party_code = topartycode.Value;
                mir1.passno = ddpassno.SelectedValue;
                mir1.passdate = txtPassDate.Text;
                mir1.issuetype = issuetype.Value;
                mir1.digilockno = digilock.Value;
                mir1.issuedqty = Convert.ToDouble(txtissuedqut.Text);
                if (txtvalue.Text != "")
                    mir1.valuers = Convert.ToDouble(txtvalue.Text);
                if (txtBasic.Text != "")
                    mir1.basicrs = Convert.ToDouble(txtBasic.Text);
                if(txtSPL.Text!="")
                mir1.splrs = Convert.ToDouble(txtSPL.Text);
                if (txtwaste.Text != "")
                    mir1.destroyedqty = Convert.ToDouble(txtwaste.Text);
                mir1.remarks = txtRemarks.Text;
                mir1.record_status = "N";
                if (txtDipsinCM.Text != "")
                    mir1.closing_dips = Convert.ToDouble(txtDipsinCM.Text);
                mir1.uom = uom.Value;
                mir1.financial_year = Session["financial_year"].ToString();
                mir1.user_id = Session["UserID"].ToString();
                string val;
                if (Session["rtype"].ToString() == "0")
                {
                    val = BL_Molasses_Issue_Register.Insert(mir1);
                }
                   
                else
                {
                    mir1.molassesissueregister_id = Convert.ToInt32(Session["mirid"]);
                    val = BL_Molasses_Issue_Register.Update(mir1);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("MolassesIssueRegisterList");
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                btnSubmit.Enabled = false;
                Molasses_Issue_Register mir1 = new Molasses_Issue_Register();
                mir1.mir_entrydate = txtdob.Value;
                mir1.party_code = partycode.Value;

                mir1.vat_code = ddlVatName.SelectedValue;
                mir1.openingbalance = Convert.ToDouble(txtOpeningBalance.Text);
                mir1.to_party_code = topartycode.Value;
                mir1.passno = ddpassno.SelectedValue;
                mir1.passdate = txtPassDate.Text;
                mir1.issuetype = issuetype.Value;
                mir1.digilockno = digilock.Value;
                mir1.issuedqty = Convert.ToDouble(txtissuedqut.Text);
                if (txtvalue.Text != "")
                    mir1.valuers = Convert.ToDouble(txtvalue.Text);
                if (txtBasic.Text != "")
                    mir1.basicrs = Convert.ToDouble(txtBasic.Text);
                if (txtSPL.Text != "")
                    mir1.splrs = Convert.ToDouble(txtSPL.Text);
                if (txtwaste.Text != "")
                    mir1.destroyedqty = Convert.ToDouble(txtwaste.Text);
                mir1.remarks = txtRemarks.Text;
                mir1.record_status = "";
                if (txtDipsinCM.Text != "")
                    mir1.closing_dips = Convert.ToDouble(txtDipsinCM.Text);
                mir1.uom = uom.Value;
                mir1.record_status = "Y";
                mir1.financial_year = Session["financial_year"].ToString();
                mir1.user_id = Session["UserID"].ToString();
                string val;
                if (Session["rtype"].ToString() == "0")
                { 
                    val = BL_Molasses_Issue_Register.Insert(mir1);
                }
                else
                { 
                    mir1.molassesissueregister_id = Convert.ToInt32(Session["mirid"]);
                }
                val = BL_Molasses_Issue_Register.Update(mir1);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("MolassesIssueRegisterList");
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
      

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string val;
               // string passno1 =passno.Value;
              //  double remqty1 = Convert.ToDouble(remqty.Value) - Convert.ToDouble(issuedqut.Value);
                Molasses_Issue_Register mir1 = new Molasses_Issue_Register();
                mir1.mir_entrydate = txtDate1.Text;
                mir1.party_code = partycode.Value;
                mir1.vat_code = ddlVatName.SelectedValue;
                mir1.openingbalance = Convert.ToDouble(txtOpeningBalance.Text);
                mir1.to_party_code = topartycode.Value;
                mir1.passno = passno.Value;
                mir1.passdate = passdate.Value;
                mir1.issuetype = issuetype.Value;
                mir1.digilockno = digilock.Value;
                mir1.issuedqty = Convert.ToDouble(txtissuedqut.Text);
                //mir1.valuers = Convert.ToDouble(txtvalue.Text);
                //mir1.basicrs = Convert.ToDouble(txtBasic.Text);
                //mir1.splrs = Convert.ToDouble(txtSPL.Text);
                //mir1.destroyedqty = Convert.ToDouble(txtwaste.Text);
                mir1.rem_pass_qty= Convert.ToDouble(remqty.Value) - Convert.ToDouble(txtissuedqut.Text);
                mir1.vehicle_no = vehicleno.Value;
                mir1.financial_year = Session["financial_year"].ToString();
                mir1.remarks =txtApproverremarks.Value;
                mir1.record_status = "A";
              //  mir1.closing_dips = Convert.ToDouble(txtDipsinCM.Text);
                mir1.uom = uom.Value;
                mir1.user_id = Session["UserID"].ToString();
                //   mir1.rem_pass_qty = Convert.ToDouble(remqty.Value) + mir1.issuedqty;
                if (mir1.issuetype == "3")
                    mir1.grossweight = mir1.issuedqty;
                mir1.molassesissueregister_id = Convert.ToInt32(Session["mirid"]);
                val = BL_Molasses_Issue_Register.Approve(mir1);
                if (val == "0")
                {

                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("MolassesIssueRegisterList");
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
                Molasses_Issue_Register mir1 = new Molasses_Issue_Register();
                mir1.mir_entrydate = txtDate1.Text;
                mir1.party_code = partycode.Value;
                mir1.vat_code = ddlVatName.SelectedValue;
                mir1.openingbalance = Convert.ToDouble(txtOpeningBalance.Text);
                mir1.financial_year = Session["financial_year"].ToString();
                mir1.closing_dips = Convert.ToDouble(txtDipsinCM.Text);
                mir1.uom = uom.Value;
                mir1.to_party_code = topartycode.Value;
                mir1.passno = passno.Value;
                mir1.passdate = passdate.Value;
                mir1.issuetype = issuetype.Value;
                mir1.digilockno = digilock.Value;
                mir1.issuedqty = Convert.ToDouble(txtissuedqut.Text);
                mir1.rem_pass_qty = Convert.ToDouble(remqty.Value) - Convert.ToDouble(txtissuedqut.Text);
                mir1.remarks = txtApproverremarks.Value;
                mir1.record_status = "R";
                mir1.user_id = Session["UserID"].ToString();
                mir1.molassesissueregister_id = Convert.ToInt32(Session["mirid"]);
                string val;
                val = BL_Molasses_Issue_Register.Approve(mir1);
                if (val == "0")
                {

                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("MolassesIssueRegisterList");
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

        protected void btnCancel_Click1(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("MolassesIssueRegisterList");
        }

        protected void ddpassno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddpassno.SelectedValue != "Select")
            {
                if (Session["rtype"].ToString() == "0")
                {
                    Session["MIRfinancial_year"] = Session["financial_year"].ToString();
                }
                Pass_Details val12 = BL_Pass_Details.GetPassDetails(ddpassno.SelectedValue, Session["MIRfinancial_year"].ToString());
                txtPassDate.Text = val12.dispatch_date.ToString();
                txtissuetype.Text = val12.dispatch_type_name;
                ddlVatName.SelectedValue = val12.to_dispatch_vat.ToString();
                vatcode.Value = val12.to_dispatch_vat.ToString();
                txtDLN.Text = val12.digital_lock_no.ToString();
                digilock.Value = val12.digital_lock_no.ToString();
                //  txdi
                txtissuedqut.Text = val12.dispatch_qty.ToString();
                remqty.Value = val12.rem_pass_qty.ToString();
                issuetype.Value = val12.dispatch_type_id.ToString();
                txttoparty.Text = val12.cutomer_name;
                topartycode.Value = val12.customer_id;
                List<Molasses_Issue_Register> mir = new List<Molasses_Issue_Register>();
                mir = BL_Molasses_Issue_Register.GetOpeningBalanceListMIR(ddlVatName.SelectedValue, Session["MIRfinancial_year"].ToString());
                txtOpeningBalance.Text = mir[0].openingbalance.ToString();
               uom.Value= mir[0].uom;
                txtOpeningBalance.Text = mir[0].openingbalance.ToString();
                txtTIQ.Text = mir[0].issuedqty.ToString();
                txtTPQ.Text = mir[0].production.ToString();
                txtAvailable.Text = (Convert.ToDouble( mir[0].openingbalance) + (Convert.ToDouble(mir[0].production) - Convert.ToDouble(mir[0].issuedqty))).ToString();
                txtClosingDips.Text = mir[0].closing_dips.ToString();
            }
            if(ddpassno.SelectedValue == "Select")
            {
                txtPassDate.Text ="";
                ddlVatName.SelectedValue ="Select";
                txtDLN.Text ="";
                txtissuedqut.Text ="";
                remqty.Value = "";
                txtissuetype.Text ="";
                txttoparty.Text ="";
                issuetype.Value = "";
                topartycode.Value ="";
            }

          //  val1 =  + "_" +  + "_" +  + "_" +  + "_" + + "_" +  + "_" + val.customer_id.ToString() + "_" + val.cutomer_name.ToString();
        }
    }
}