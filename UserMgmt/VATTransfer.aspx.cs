using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class VATTransfer : System.Web.UI.Page
    {
        public static List<VAT_Master> vatmasters = new List<VAT_Master>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
               
                txtAvailableQtyBL.ReadOnly = true;
                txtAvailableQtyLPL1.ReadOnly = true;
                // txtTransferQTY.Attributes.Add("disabled", "disabled");
                txtTransferQTYLPL.ReadOnly = true;
                string user_id = Session["UserID"].ToString();
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(user_id);
                    Session["party_code"] = user.party_code;
                    Session["financial_year"] = user.financial_year;
                    if (user.party_type == "Distillery Unit" || user.party_type == "ENA Distillery Unit")
                    {
                        sgr.Visible = false;
                        AvalBL.Text = "Available Qty (BL)";
                        TBL.Text = "Transfer Qty (BL)";
                    }
                    else
                    {
                        dst.Visible = false;
                        TLP.Visible = false;
                        ALP.Visible = false;
                        TBL.Text = "Transfer Qty (Quintals)";
                        AvalBL.Text = "Available Qty (Quintals)";
                    }
                        
                    if (user_id == "Admin")
                {
                        Response.Redirect("~/LoginPage");
                        //vatmasters = new List<VAT_Master>();
                        //vatmasters = BL_VATMaster.GetvatmasterList(Session["party_code"] );
                        //var Vat_types = (from s in vatmasters

                        //                 select new { Vat_type = s.vat_type_name, Vat_type_code = s.vat_type_code }).Distinct();
                        //ddVatType.DataSource = Vat_types.ToList();
                        //ddVatType.DataTextField = "Vat_type";
                        //ddVatType.DataValueField = "Vat_type_code";
                        //ddVatType.DataBind();
                        //ddVatType.Items.Insert(0, "Select");
                        //btnReject.Visible = false;
                        //btnApprove.Visible = false;
                    }
                    else if (user.role_name == "Bond Officer")
                {
                    vatmasters = new List<VAT_Master>();
                    vatmasters = BL_VATMaster.GetvatmasterList(user.party_code);
                    var Vat_types = (from s in vatmasters
                                     where s.party_code == user.party_code
                                     select new { Vat_type = s.vat_type_name, Vat_type_code = s.vat_type_code }).Distinct();
                    ddVatType.DataSource = Vat_types.ToList();
                    ddVatType.DataTextField = "Vat_type";
                    ddVatType.DataValueField = "Vat_type_code";
                    ddVatType.DataBind();
                    ddVatType.Items.Insert(0, "Select");
                    btnSaveasDraft.Visible = false;
                    btnSave1.Visible = false;
                    btnCancel.Visible = false;

                }
                else
                {
                    vatmasters = new List<VAT_Master>();
                    vatmasters = BL_VATMaster.GetvatmasterList(user.party_code);
                    var Vat_types = (from s in vatmasters
                                     where s.party_code == user.party_code
                                     select new { Vat_type = s.vat_type_name, Vat_type_code = s.vat_type_code }).Distinct();
                    ddVatType.DataSource = Vat_types.ToList();
                    ddVatType.DataTextField = "Vat_type";
                    ddVatType.DataValueField = "Vat_type_code";
                    ddVatType.DataBind();
                    ddVatType.Items.Insert(0, "Select");
                    btnReject.Visible = false;
                    btnApprove.Visible = false;

                    approverremaks.Visible = false;
                }
                if (Session["rtype"].ToString() != "0")
                {
                    List<VATTransfers_> vattransfer = new List<VATTransfers_>();
                    vattransfer = BL_VATTransfers_.GetList(user.party_code,"");
                    var vattrans = from s1 in vattransfer
                                   where s1.vat_transfer_id == Convert.ToInt32(Session["transferid"].ToString()) && s1.financial_year == Session["VTfinancial_year"].ToString()
                                   select s1;
                    string v = vattrans.ToList()[0].vat_type_code;
                    ddVatType.SelectedValue = vattrans.ToList()[0].vat_type_code;
                        txtTransferDate.Text = vattrans.ToList()[0].transfered_date;
                        txttrdate.Value = vattrans.ToList()[0].transfered_date;
                        ddVatType_SelectedIndexChanged(sender, null);
                    txtStrength.Text = vattrans.ToList()[0].strength.ToString();
                    //FromVATName1.Value = vattrans.ToList()[0].from_vat;
                    ddlFromVATName.SelectedValue = vattrans.ToList()[0].from_vat;
                    ddlFromVATName_SelectedIndexChanged(sender, null);

                    ddlToVATName.SelectedValue = vattrans.ToList()[0].to_vat;
                  
                  //  ToVATName1.Value = vattrans.ToList()[0].to_vat;
                    txtDipsinWetInches.Text = vattrans.ToList()[0].dips.ToString();
                    txtIndication.Text = vattrans.ToList()[0].indication.ToString();
                        txtRemarks.Text = vattrans.ToList()[0].remarks;
                        txtTemperature.Text = vattrans.ToList()[0].temperature.ToString();
                    txtTransferQTY.Text = vattrans.ToList()[0].transferqty.ToString();
                        txtTransferQTYLPL.Text = vattrans.ToList()[0].lp_transferqty.ToString();
                        txtTransferQTY_TextChanged(sender, null);
                    List<All_Approvals> approvals = new List<All_Approvals>();
                    approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), vattrans.ToList()[0].vat_transfer_id.ToString(), "VTV");
                        var list4 = (from s in approvals
                                   where s.financial_year == Session["VTfinancial_year"].ToString()
                                     select s);
                        grdApprovalDetails.DataSource = list4.ToList();
                        grdApprovalDetails.DataBind();
                    if (approvals.Count <= 0)
                        txtApproverremarks.ReadOnly = false;
                    if (Session["rtype"].ToString() == "1")
                    {
                        ddVatType.Enabled = false;
                        ddlFromVATName.Enabled = false;
                        ddlToVATName.Enabled = false;
                        txtDipsinWetInches.ReadOnly = true;
                        txtIndication.ReadOnly = true;
                        txtStrength.ReadOnly = true;
                        txtTemperature.ReadOnly = true;
                        txtTransferQTY.ReadOnly = true;
                            txtRemarks.ReadOnly = true;
                        btnCancel.Visible = false;
                        btnSave1.Visible = false;
                        btnSaveasDraft.Visible = false;
                            Image1.Visible = false;

                    }
                        if (user.role_name == "Bond Officer")
                        {
                            approverremaks.Visible = true;
                            txtApproverremarks.ReadOnly = false;
                        }
                       if( vattrans.ToList()[0].record_status == "A")
                        {
                            approverremaks.Visible = false;
                            txtApproverremarks.ReadOnly = false;
                            btnReject.Visible = false;
                            btnApprove.Visible = false;
                            btnCancel.Visible = false;

                        }
                    }
                }
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/LoginPage");
                }
            }
        }
        //[System.Web.Services.WebMethod]
        //public static string GetTransforForm(string indent_id)
        //{
        //    VATTransfers_ vat = new VATTransfers_();
        //  //  MolassesIndent oMI = GetMolasses(Convert.ToInt32(slno));
        //   // return Newtonsoft.Json.JsonConvert.SerializeObject(oMI);
        //}
        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATTransferList");
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
        protected void btnRG4_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("SCMSugarCanePurchaseRegList");
        }
        protected void lnkRawMaterialWastage_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawmaterialWastageList.aspx");
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
        protected void btnVATtansfers_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATTransferList");
        }
        [WebMethod]

        public static VAT_Master[] LoadVatNames(Object vattype)
        {

            IEnumerable<VAT_Master> projectslist = (from proj in vatmasters where proj.vat_type_code == vattype.ToString() select proj).AsEnumerable().Select(projt => new VAT_Master() { vat_name = projt.vat_name, vat_code = projt.vat_code });

            // JavaScriptSerializer ser = new JavaScriptSerializer();
            return projectslist.ToArray();
        }
        [WebMethod]
        public static string GetAvailableQTY(Object vatcode)
        {

            string val1 = "";
            if (vatcode != null)
            {
                if (vatcode.ToString() != "Select")
                {
                    var val = (from proj in vatmasters where proj.vat_code == vatcode.ToString() select new { avaialble = proj.vat_availablecapacity });
                    val1 = val.ToList()[0].avaialble.ToString();
                }
            }
            // string val1 = val.ToList()[0].ToString();
            return val1.ToString();
        }
        protected void btnSave1_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                VATTransfers_ vat = new VATTransfers_();
                vat.vat_type_code = ddVatType.SelectedValue;
                vat.transfered_date = txttrdate.Value;
                vat.party_code = Session["party_code"].ToString();
                vat.dips = Convert.ToDouble(txtDipsinWetInches.Text);
                vat.indication = Convert.ToDouble(txtIndication.Text);
                vat.temperature = Convert.ToDouble(txtTemperature.Text);
                vat.strength = Convert.ToDouble(txtStrength.Text);
                vat.from_vat = ddlFromVATName.SelectedValue;
                vat.to_vat = ddlToVATName.SelectedValue;
                vat.remarks = txtRemarks.Text;
                vat.user_id = Session["UserID"].ToString();
                vat.transferqty = Convert.ToDouble(txtTransferQTY.Text);
                vat.lp_transferqty = Convert.ToDouble(txtTransferQTYLPL.Text);
              vat.financial_year=  Session["financial_year"].ToString();
                vat.record_status = "Y";
                string val;
                if (Session["rtype"].ToString() == "0")
                    val = BL_VATTransfers_.Insert(vat);
                else
                {
                    vat.vat_transfer_id = Convert.ToInt32(Session["transferid"].ToString());

                    val = BL_VATTransfers_.Update(vat);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("VATTransferList");
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
            Response.Redirect("VATTransferList");
        }

        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                VATTransfers_ vat = new VATTransfers_();
                vat.vat_type_code = ddVatType.SelectedValue;
                vat.transfered_date = txttrdate.Value;
                vat.party_code = Session["party_code"].ToString();
                vat.dips = Convert.ToDouble(txtDipsinWetInches.Text);
                vat.indication = Convert.ToDouble(txtIndication.Text);
                vat.temperature = Convert.ToDouble(txtTemperature.Text);
                vat.strength = Convert.ToDouble(txtStrength.Text);
                vat.from_vat =ddlFromVATName.SelectedValue;
                vat.to_vat = ddlToVATName.SelectedValue;
                vat.remarks = txtRemarks.Text;
                vat.user_id = Session["UserID"].ToString();
                vat.transferqty = Convert.ToDouble(txtTransferQTY.Text);
                vat.lp_transferqty = Convert.ToDouble(txtTransferQTYLPL.Text);
                vat.record_status = "N";
                vat.financial_year = Session["financial_year"].ToString();
                string val;
                if (Session["rtype"].ToString() == "0")
                    val = BL_VATTransfers_.Insert(vat);
                else
                {
                    vat.vat_transfer_id = Convert.ToInt32(Session["transferid"].ToString());

                    val = BL_VATTransfers_.Update(vat);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("VATTransferList");
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
                VATTransfers_ vat = new VATTransfers_();
                vat.vat_type_code = ddVatType.SelectedValue;
                vat.transfered_date = txttrdate.Value;
                vat.dips = Convert.ToDouble(txtDipsinWetInches.Text);
                vat.indication = Convert.ToDouble(txtIndication.Text);
                vat.temperature = Convert.ToDouble(txtTemperature.Text);
                vat.strength = Convert.ToDouble(txtStrength.Text);
                vat.from_vat = ddlFromVATName.SelectedValue;
                vat.to_vat = ddlToVATName.SelectedValue;
                vat.financial_year = Session["financial_year"].ToString();
                vat.partyname = Session["party_code"].ToString();
                vat.user_id = Session["UserID"].ToString();
                vat.transferqty = Convert.ToDouble(txtTransferQTY.Text);
                vat.lp_transferqty = Convert.ToDouble(txtTransferQTYLPL.Text);
                vat.remarks = txtApproverremarks.Text;
                vat.vat_transfer_id = Convert.ToInt32(Session["transferid"].ToString());
                vat.record_status = "R";
                string val;
                vat.user_id = Session["UserID"].ToString();
                val = BL_VATTransfers_.Approve(vat);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("VATTransferList");
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
                VATTransfers_ vat = new VATTransfers_();
                vat.vat_type_code = ddVatType.SelectedValue;
                vat.transfered_date = txttrdate.Value;
                vat.dips = Convert.ToDouble(txtDipsinWetInches.Text);
                vat.indication = Convert.ToDouble(txtIndication.Text);
                vat.temperature = Convert.ToDouble(txtTemperature.Text);
                vat.strength = Convert.ToDouble(txtStrength.Text);
                vat.from_vat = ddlFromVATName.SelectedValue;
                vat.to_vat = ddlToVATName.SelectedValue;
                vat.partyname = Session["party_code"].ToString();
                vat.financial_year = Session["financial_year"].ToString();
                vat.user_id = Session["UserID"].ToString();
                vat.transferqty = Convert.ToDouble(txtTransferQTY.Text);
                vat.lp_transferqty = Convert.ToDouble(txtTransferQTYLPL.Text);
                vat. remarks = txtApproverremarks.Text;
          vat.vat_transfer_id =Convert.ToInt32( Session["transferid"].ToString());
                vat.record_status = "A";
                string val;
                string user_id = Session["UserID"].ToString();
                val = BL_VATTransfers_.Approve(vat);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("VATTransferList");
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
        protected void btnOpeningBalance_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("OpeningBalanceList");
        }
    
        protected void ddVatType_SelectedIndexChanged(object sender, EventArgs e)
        {
          
                var fromvats = from proj in vatmasters
                               where proj.vat_type_code == ddVatType.SelectedValue
                               select proj;
                ddlFromVATName.DataSource = fromvats.ToList();
                ddlFromVATName.DataTextField = "Vat_name";
                ddlFromVATName.DataValueField = "Vat_code";
                ddlFromVATName.DataBind();
                ddlFromVATName.Items.Insert(0, "Select");
            //    //double strenth = Convert.ToDouble(txtStrength.Text);
            //    //double lpl = (fromvats.ToList()[0].vat_availablecapacity * (1 + strenth / 100));
            //    //txtAvailableQtyBL.Text = fromvats.ToList()[0].vat_availablecapacity.ToString();
            //    //txtAvailableQtyLPL1.Text = lpl.ToString();
                
            //}
            //else
            //{
            //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //    sb.Append("<script type = 'text/javascript'>");
            //    sb.Append("window.onload=function(){");
            //    sb.Append("alert('");
            //    sb.Append("Please Enter Strenth");
            //    sb.Append("')};");
            //    sb.Append("</script>");
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
            //}

        }

        protected void ddlFromVATName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtStrength.Text != "")

            {
                List<VAT_Master> vatmasters1 = new List<VAT_Master>();
                vatmasters1 = BL_VATMaster.GetvatmasterList(Session["party_code"].ToString() );
                var fromvats = from proj in vatmasters1
                               where proj.vat_code == ddlFromVATName.SelectedValue
                               select proj;
                var tovats = from proj1 in vatmasters1
                               where proj1.vat_type_code == ddVatType.SelectedValue && proj1.vat_code!=ddlFromVATName.SelectedValue
                             select proj1;
                ddlToVATName.DataSource = tovats.ToList();
                ddlToVATName.DataTextField = "Vat_name";
                ddlToVATName.DataValueField = "Vat_code";
                ddlToVATName.DataBind();
                ddlToVATName.Items.Insert(0, "Select");
                double strenth = Convert.ToDouble(txtStrength.Text);
                if(fromvats.ToList().Count!=0)
                {
                    double lpl = (fromvats.ToList()[0].vat_availablecapacity * (1 + strenth / 100));
                    txtAvailableQtyBL.Text = fromvats.ToList()[0].vat_availablecapacity.ToString();
                    txtAvailableQtyLPL1.Text = lpl.ToString();

                }


            }
            else
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append("alert('");
                sb.Append("Please Enter a Strength Value");
                sb.Append("')};");
                sb.Append("</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
            }
        }

        protected void txtAvailableQtyBL_TextChanged(object sender, EventArgs e)
        {
           
        }

        protected void txtTransferQTY_TextChanged(object sender, EventArgs e)
        {
            if (txtTransferQTY.Text != "")
            {

                double strenth = Convert.ToDouble(txtStrength.Text);
                if (Convert.ToDouble(txtTransferQTY.Text) <= Convert.ToDouble(txtAvailableQtyBL.Text))
                {
                    double lpl = (Convert.ToDouble(txtTransferQTY.Text) * (1 + strenth / 100));
                    //txtAvailableQtyBL.Text = fromvats.ToList()[0].vat_availablecapacity.ToString();
                    txtTransferQTYLPL.Text = lpl.ToString();
                }
                else
                {
                    if (Session["rtype"].ToString() != "1")
                    {
                        txtTransferQTY.Text = "";
                        txtTransferQTY.Focus();
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append("ToVat Qty not more than that of FromVat Available Qty");
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    }
                }
            }
        }
    }
}