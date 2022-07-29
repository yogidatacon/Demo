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
    public partial class VATTransfersFroms : System.Web.UI.Page
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
                txtAvailableQtyLPL.ReadOnly = true;
                // txtTransferQTY.Attributes.Add("disabled", "disabled");
                txtTransferQTYLPL.ReadOnly = true;
                string user_id = Session["UserID"].ToString();
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(user_id);
                if (user_id == "Admin")
                {
                        // Response.Redirect("~/LoginPage"); 
                        vatmasters = new List<VAT_Master>();
                        vatmasters = BL_VATMaster.GetvatmasterList(user.party_code);
                        var Vat_types = (from s in vatmasters
                                        
                                         select new { Vat_type = s.vat_type_name, Vat_type_code = s.vat_type_code }).Distinct();
                        ddVatType.DataSource = Vat_types.ToList();
                        ddVatType.DataTextField = "Vat_type";
                        ddVatType.DataValueField = "Vat_type_code";
                        ddVatType.DataBind();
                        ddVatType.Items.Insert(0, "Select");
                        btnReject.Visible = false;
                        btnApprove.Visible = false;
                    }
                else if(user.role_name=="Bond Officer")
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
                if(Session["rtype"].ToString()!="0")
                {
                    List<VATTransfers_> vattransfer = new List<VATTransfers_>();
                    vattransfer = BL_VATTransfers_.GetList(user.party_code,"");
                    var vattrans= from s1 in vattransfer
                                     where s1.vat_transfer_id ==Convert.ToInt32( Session["transferid"].ToString())
                    select s1;
                    string v = vattrans.ToList()[0].vat_type_code;
                    ddVatType.SelectedValue = vattrans.ToList()[0].vat_type_code;
                    txtStrength.Text = vattrans.ToList()[0].strength.ToString();
                    FromVATName.Value = vattrans.ToList()[0].from_vat;
                    ddlFromVATName.SelectedValue = vattrans.ToList()[0].from_vat;
                    ddlToVATName.SelectedValue = vattrans.ToList()[0].to_vat;
                    ToVATName.Value = vattrans.ToList()[0].to_vat; 
                    txtDipsinWetInches.Text = vattrans.ToList()[0].dips.ToString();
                    txtIndication.Text = vattrans.ToList()[0].indication.ToString();
                  
                    txtTemperature.Text = vattrans.ToList()[0].temperature.ToString();
                    txtTransferQTY.Text = vattrans.ToList()[0].transferqty.ToString();
                    List<All_Approvals> approvals = new List<All_Approvals>();
                    approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), vattrans.ToList()[0].vat_transfer_id.ToString(), "VTV");
                    grdApprovalDetails.DataSource = approvals;
                    grdApprovalDetails.DataBind();
                    if (approvals.Count <= 0)
                        txtApproverremarks.ReadOnly = true;
                    if (Session["rtype"].ToString()=="1")
                    {
                        ddVatType.Enabled = false;
                        ddlFromVATName.Enabled = false;
                        ddlToVATName.Enabled = false;
                        txtDipsinWetInches.ReadOnly = true;
                        txtIndication.ReadOnly = true;
                        txtStrength.ReadOnly = true;
                        txtTemperature.ReadOnly = true;
                        txtTransferQTY.ReadOnly = true;
                        btnCancel.Visible = false;
                        btnSave1.Visible = false;
                        btnSaveasDraft.Visible = false;
                       
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
            if (vatcode!= null)
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
            
                VATTransfers_ vat = new VATTransfers_();
                vat.vat_type_code = ddVatType.SelectedValue;
                vat.dips = Convert.ToDouble(txtDipsinWetInches.Text);
                vat.indication = Convert.ToDouble(txtIndication.Text);
                vat.temperature = Convert.ToDouble(txtTemperature.Text);
                vat.strength = Convert.ToDouble(txtStrength.Text);
                vat.from_vat = FromVATName.Value;
            vat.to_vat = ToVATName.Value;
                vat.user_id = Session["UserID"].ToString();
                vat.transferqty = Convert.ToDouble(txtTransferQTY.Text);
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATTransferList");
        }

        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            VATTransfers_ vat = new VATTransfers_();
            vat.vat_type_code = ddVatType.SelectedValue;
            vat.dips = Convert.ToDouble(txtDipsinWetInches.Text);
            vat.indication = Convert.ToDouble(txtIndication.Text);
            vat.temperature = Convert.ToDouble(txtTemperature.Text);
            vat.strength = Convert.ToDouble(txtStrength.Text);
            vat.from_vat = FromVATName.Value;
            vat.to_vat = ToVATName.Value;
            vat.user_id = Session["UserID"].ToString();
            vat.transferqty = Convert.ToDouble(txtTransferQTY.Text);
            vat.record_status = "N";
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

        protected void btnReject_Click(object sender, EventArgs e)
        {
            VATTransfers_ vat = new VATTransfers_();
            string remarks = txtApproverremarks.Text;
            string transferid = Session["transferid"].ToString();
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

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            VATTransfers_ vat = new VATTransfers_();
            string remarks = txtApproverremarks.Text;
            string transferid = Session["transferid"].ToString();
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
        protected void btnOpeningBalance_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("OpeningBalanceList");
        }
    }
}