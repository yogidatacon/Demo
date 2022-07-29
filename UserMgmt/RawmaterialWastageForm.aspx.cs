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
    public partial class RawmaterialWastageForm : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        static RawmaterialWastage fermenter = new RawmaterialWastage();
        static string entrydate;
        static string _party_code;
        double molassestotal = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
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
                                   where s.party_code == user.party_code && (s.vat_type_code=="MST" || s.vat_type_code == "STO" || s.vat_type_code == "DEN")
                                   select s;
                        ddlFermenter.DataSource = list.ToList();
                        ddlFermenter.DataTextField = "vat_name";
                        ddlFermenter.DataValueField = "vat_code";
                        ddlFermenter.DataBind();
                        ddlFermenter.Items.Insert(0, "Select");
                      

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

                        if (Session["UserID"].ToString() == "Admin")
                        {
                            btnSaveAsDraft.Visible = false;
                            btnSubmit.Visible = false;
                            btnupdate.Visible = true;
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
                            fermenter = new RawmaterialWastage();
                            fermenter =BL_RawMaterialWastage.GetDetails(Session["party_code"].ToString(),Convert.ToInt32(Session["FermenterId"].ToString()), Session["rwfinancial_year"].ToString());
                            txtDATE.Text = fermenter.rmw_entrydate;
                            txtdob1.Value = fermenter.rmw_entrydate;
                            CalendarExtender.SelectedDate = Convert.ToDateTime(fermenter.rmw_entrydate);
                            CalendarExtender.EndDate = DateTime.Now;
                           
                            ddlFermenter.SelectedValue = fermenter.vat_code;
                            string sub = ddlFermenter.SelectedValue.Substring(3, 3);
                            if (sub == "MST")
                            {
                                txtTransitWastage.ReadOnly = false;
                                txtDecreaseInWastage.ReadOnly = true;
                                txtIncreaseInOperation.ReadOnly = true;
                                txtStorageWastage.ReadOnly = false;
                                txtHandlingWastage.ReadOnly = false;
                            }
                            else
                            {
                                txtTransitWastage.ReadOnly = true;
                                txtDecreaseInWastage.ReadOnly = false;
                                txtIncreaseInOperation.ReadOnly = false;
                                txtStorageWastage.ReadOnly = true;
                                txtHandlingWastage.ReadOnly = true;
                            }

                            txtTransitWastage.Text = fermenter.transit_wastage.ToString();
                            txtStorageWastage.Text = fermenter.storage_wastage.ToString();
                            txtHandlingWastage.Text = fermenter.handling_wastage.ToString();
                            txtIncreaseInOperation.Text = fermenter.inc_operation.ToString();
                            txtDecreaseInWastage.Text = fermenter.dec_wastage.ToString();
                            txtRemarks1.Value = fermenter.remarks;
                            //    lbltotal.Visible = true;
                            //    molassestotal.Visible = true;

                                List<All_Approvals> approvals = new List<All_Approvals>();
                            approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), fermenter.rawmaterial_wastage_id.ToString(), "RMW");
                            if (Session["UserID"].ToString() == "Admin")
                            {
                                txtDATE.Attributes.Add("disabled", "disabled");
                                ddlFermenter.Attributes.Add("disabled", "disabled");
                                txtRemarks1.Attributes.Add("disabled", "disabled");
                                txtTransitWastage.ReadOnly = false;
                                txtStorageWastage.ReadOnly = false;
                                txtHandlingWastage.ReadOnly = false;
                                var list4 = (from s in approvals
                                             where s.user_id == "Admin"
                                             select s);
                                grdApprovalDetails.DataSource = list4.ToList();
                                grdApprovalDetails.DataBind();
                            }
                            else
                            {
                                var list4 = (from s in approvals
                                             where s.financial_year == Session["rwfinancial_year"].ToString()
                                             select s);
                                grdApprovalDetails.DataSource = list4.ToList();
                                grdApprovalDetails.DataBind();
                            }
                               
                            if (fermenter.record_status == "Y" || user.role_name == "Bond Officer")
                            {
                               
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

                                    Image1.Visible = false;

                                }
                                txtDATE.Attributes.Add("disabled", "disabled");
                                ddlFermenter.Attributes.Add("disabled", "disabled");
                                txtRemarks1.Attributes.Add("disabled", "disabled");
                                txtTransitWastage.ReadOnly = true;
                                txtDecreaseInWastage.ReadOnly = true;
                                txtIncreaseInOperation.ReadOnly = true;
                                txtStorageWastage.ReadOnly = true;
                                txtHandlingWastage.ReadOnly = true;
                                //ddlRawmaterialType.Enabled = false;
                                approverremaks.Visible = false;
                                btnSaveAsDraft.Visible = false;
                                btnSubmit.Visible = false;
                                btnCancel.Visible = false;
                                //lbltotal.Enabled = false;

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


                                    Image1.Visible = false;
                                }
                            
                                Image1.Visible = false;
                                if (user.role_name == "Bond Officer" && fermenter.record_status == "Y")
                                {
                                    approverremaks.Visible =true;
                                    approverid.Visible = false;
                                    btnApprove.Visible =true;
                                    btnReject.Visible =true;
                                    txtApproverremarks.Visible =true;
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
        protected void lnkRawMaterialWastage_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawmaterialWastageList.aspx");
        }
        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("RawmaterialWastageList.aspx");
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



      
      
        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

            RawmaterialWastage ferm = new RawmaterialWastage();
                if (txtDATE.Text == "" || txtDATE.Text != "")
                {
                    txtDATE.Text = txtdob1.Value;
                }
                ferm.rmw_entrydate = Convert.ToDateTime(txtDATE.Text).ToString("dd-MM-yyyy");
               
                ferm.party_code = party_code.Value;
                ferm.vat_code = ddlFermenter.SelectedValue;
                ferm.user_id = Session["UserId"].ToString();
                if(txtTransitWastage.Text!="")
                {
                    ferm.transit_wastage = Convert.ToDouble(txtTransitWastage.Text);
                }
                if(txtStorageWastage.Text !="")
                {
                    ferm.storage_wastage = Convert.ToDouble(txtStorageWastage.Text);
                }
                if (txtHandlingWastage.Text != "")
                {
                    ferm.handling_wastage = Convert.ToDouble(txtHandlingWastage.Text);
                }
               if(txtIncreaseInOperation.Text !="")
                {
                    ferm.inc_operation = Convert.ToDouble(txtIncreaseInOperation.Text);
                }
               if(txtDecreaseInWastage.Text !="")
                {
                    ferm.dec_wastage = Convert.ToDouble(txtDecreaseInWastage.Text);
                }
              
               
                ferm.remarks = txtRemarks1.InnerText;
                ferm.record_status = "N";
                ferm.financial_year= Session["financial_year"].ToString();
                    //else
                    //{
                    //    setup.mahua = null;
                    //}


                string val;
                if (Session["rtype"].ToString() == "0")
                    val = BL_RawMaterialWastage.Insert(ferm);
                else
                {
                    ferm.rawmaterial_wastage_id = Convert.ToInt32(Session["FermenterId"].ToString());
                    val = BL_RawMaterialWastage.Update(ferm);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("RawmaterialWastageList.aspx");
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
               RawmaterialWastage ferm = new RawmaterialWastage();

                ferm.record_status = "A";
                string val;
                ferm.rawmaterial_wastage_id = Convert.ToInt32(Session["FermenterId"].ToString());
                ferm.remarks = txtApproverremarks.Value;
                if (txtTransitWastage.Text != "")
                {
                    ferm.transit_wastage = Convert.ToDouble(txtTransitWastage.Text);
                }
                if (txtStorageWastage.Text != "")
                {
                    ferm.storage_wastage = Convert.ToDouble(txtStorageWastage.Text);
                }
                if (txtHandlingWastage.Text != "")
                {
                    ferm.handling_wastage = Convert.ToDouble(txtHandlingWastage.Text);
                }
                if (txtIncreaseInOperation.Text != "")
                {
                    ferm.inc_operation = Convert.ToDouble(txtIncreaseInOperation.Text);
                }
                if (txtDecreaseInWastage.Text != "")
                {
                    ferm.dec_wastage = Convert.ToDouble(txtDecreaseInWastage.Text);
                }
                ferm.financial_year = Session["financial_year"].ToString();
                ferm.user_id = Session["UserID"].ToString();
                val = BL_RawMaterialWastage.Approve(ferm);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("RawmaterialWastageList.aspx");
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
                RawmaterialWastage ferm = new RawmaterialWastage();

                ferm.record_status = "R";
                string val;
                ferm.rawmaterial_wastage_id = Convert.ToInt32(Session["FermenterId"].ToString());
                ferm.remarks = txtApproverremarks.Value;
                ferm.user_id = Session["UserID"].ToString();
                ferm.financial_year = Session["financial_year"].ToString();
                val = BL_RawMaterialWastage.Approve(ferm);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("RawmaterialWastageList.aspx");
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
                RawmaterialWastage ferm = new RawmaterialWastage();
                if (txtDATE.Text == "" || txtDATE.Text != "")
                {
                    txtDATE.Text = txtdob1.Value;
                }
                ferm.rmw_entrydate = Convert.ToDateTime(txtDATE.Text).ToString("dd-MM-yyyy");

                ferm.party_code = party_code.Value;

               
                ferm.vat_code = ddlFermenter.SelectedValue;
                ferm.user_id = Session["UserId"].ToString();

                if (txtTransitWastage.Text != "")
                {
                    ferm.transit_wastage = Convert.ToDouble(txtTransitWastage.Text);
                }
                if (txtStorageWastage.Text != "")
                {
                    ferm.storage_wastage = Convert.ToDouble(txtStorageWastage.Text);
                }
                if (txtHandlingWastage.Text != "")
                {
                    ferm.handling_wastage = Convert.ToDouble(txtHandlingWastage.Text);
                }
                if (txtIncreaseInOperation.Text != "")
                {
                    ferm.inc_operation = Convert.ToDouble(txtIncreaseInOperation.Text);
                }
                if (txtDecreaseInWastage.Text != "")
                {
                    ferm.dec_wastage = Convert.ToDouble(txtDecreaseInWastage.Text);
                }
                ferm.remarks = txtRemarks1.InnerText;
                ferm.record_status = "Y";
                ferm.financial_year = Session["financial_year"].ToString();
                //else
                //{
                //    setup.mahua = null;
                //}


                string val;
                if (Session["rtype"].ToString() == "0")
                    val = BL_RawMaterialWastage.Insert(ferm);
                else
                {
                    ferm.rawmaterial_wastage_id = Convert.ToInt32(Session["FermenterId"].ToString());
                    val = BL_RawMaterialWastage.Update(ferm);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("RawmaterialWastageList.aspx");
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

        protected void ddlFermenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (ddlFermenter.SelectedItem.ToString() != "Select")
                {
                    string sub = ddlFermenter.SelectedValue.Substring(3, 3);
                    if (sub=="MST")
                    {
                        txtTransitWastage.ReadOnly = false;
                        txtDecreaseInWastage.ReadOnly = true;
                        txtIncreaseInOperation.ReadOnly = true;
                        txtStorageWastage.ReadOnly = false;
                        txtHandlingWastage.ReadOnly = false;
                    }
                    else
                    {
                        txtTransitWastage.ReadOnly = true;
                        txtDecreaseInWastage.ReadOnly =false;
                        txtIncreaseInOperation.ReadOnly = false;
                        txtStorageWastage.ReadOnly = true;
                        txtHandlingWastage.ReadOnly = true;
                    }

                    if (txtDATE.Text != "" || txtdob1.Value != "")
                    {


                        txtDATE.Text = txtdob1.Value;
                        DateTime dt = DateTime.ParseExact(txtDATE.Text, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        string month = dt.Month.ToString();
                        string year = dt.Year.ToString();
                        int value = BL_RawMaterialWastage.GetExistsData(month, ddlFermenter.SelectedValue,year);
                        if (value > 0)
                        {
                            string message = " Wastage & adjustment for this vat can only done once in a month";
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
                    else
                    {
                        string message = "Please select date";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                        ddlFermenter.ClearSelection();
                        txtDATE.Focus();
                    }
                }
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                RawmaterialWastage ferm = new RawmaterialWastage();

                ferm.party_code = party_code.Value;
             
                ferm.vat_code = ddlFermenter.SelectedValue;
                ferm.user_id = Session["UserId"].ToString();

                if (txtTransitWastage.Text != "")
                {
                    ferm.transit_wastage = Convert.ToDouble(txtTransitWastage.Text);
                }
                if (txtStorageWastage.Text != "")
                {
                    ferm.storage_wastage = Convert.ToDouble(txtStorageWastage.Text);
                }
                if (txtHandlingWastage.Text != "")
                {
                    ferm.handling_wastage = Convert.ToDouble(txtHandlingWastage.Text);
                }
                if (txtIncreaseInOperation.Text != "")
                {
                    ferm.inc_operation = Convert.ToDouble(txtIncreaseInOperation.Text);
                }
                if (txtDecreaseInWastage.Text != "")
                {
                    ferm.dec_wastage = Convert.ToDouble(txtDecreaseInWastage.Text);
                }
                string val;
                    ferm.rawmaterial_wastage_id = Convert.ToInt32(Session["FermenterId"].ToString());
                    val = BL_RawMaterialWastage.AdminUpdate(ferm);
               
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("RawmaterialWastageList.aspx");
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