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
    public partial class ChangeFinancialYear : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];

                    Session["UserID"] = Session["UserID"];
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    Session["party_type"] = user.party_type;
                    Session["party_code"] = user.party_code;
                    Session["Rolename"] = user.role_name;
                    if (user != null)
                    {
                        Session["financial_year"] = user.financial_year;
                       // List<UserDetails> party= new List<UserDetails>();
                       //party =BL_UserDetails.AllUserList("");
                       // var list1 = from s in party
                       //             where s.role_name != "Bond Officer" && (s.party_type_code == "MTP" || s.party_type_code == "MTR" || s.party_type_code == "MTW" || s.party_type_code == "SGR" || s.party_type_code == "DIS" || s.party_type_code == "ENA")
                       //             select s;
                       // ddlpartyname.DataSource = list1.ToList();
                       // ddlpartyname.DataTextField = "user_name";
                       // ddlpartyname.DataValueField = "user_id";
                       // ddlpartyname.DataBind();
                       // ddlpartyname.Items.Insert(0, "Select");
                        List<Org_Finacial_yr> Org_Finacial = new List<Org_Finacial_yr>();
                        Org_Finacial = BL_org_Master.GetFinacListValues("");
                        var year = from s in Org_Finacial
                                   where s.status == "Active"
                                   select s;
                        txtcurrentfinancial.Text = year.ToList()[0].financial_year;
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        txtApproverremarks.Visible = false;
                        approverremaks.Visible = false;
                        //  txtcurrentfinancial.Text = party[0].financialyear;
                        if (Session["rtype"].ToString() != "0")
                        {
                            List<UserDetails> party = new List<UserDetails>();
                            party = BL_UserDetails.AllUserList("");
                            var list1 = from s in party
                                        where s.role_name != "Bond Officer" && s.party_code == Session["Party_Code1"].ToString()
                                        select s;
                            ddlpartyname.DataSource = list1.ToList();
                            ddlpartyname.DataTextField = "user_name";
                            ddlpartyname.DataValueField = "user_id";
                            ddlpartyname.DataBind();
                            Party_Master partym = new Party_Master();
                            partym = BL_Party_Master.GetPartyDetails(Session["Party_Code1"].ToString());
                            List<All_Approvals> approvals = new List<All_Approvals>();
                            approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(),list1.ToList()[0].id.ToString(), "CHY");
                            var list = (from s in approvals
                                        select s);
                            grdApprovalDetails.DataSource = list.ToList();
                            grdApprovalDetails.DataBind();
                            id.Value = list1.ToList()[0].id.ToString();
                            string year1 = txtcurrentfinancial.Text.ToString().Substring(5);
                            string year2 = txtcurrentfinancial.Text.ToString().Substring(0,4);
                            enddate.Value = partym.enddate.Substring(0, 6) + "" +year1 ;
                             startdate.Value = partym.startdate.Substring(0, 6) + "" + year2;
                            if (Session["rtype"].ToString() == "1")
                            {
                                ddlpartyname.Enabled = false;
                                txtcurrentfinancial.ReadOnly = true;
                                btnCancel.Visible = false;
                                btnSaveasDraft.Visible = false;
                                btnApprove.Visible = false;
                                btnReject.Visible = false;
                                if (Session["Rolename"].ToString() == "Bond Officer")
                                {
                                    btnSaveasDraft.Visible = false;
                                    if (partym.record_status == "Y")
                                    {
                                        approverremaks.Visible = true;
                                        txtApproverremarks.Visible = true;
                                        btnApprove.Visible = true;
                                        btnReject.Visible = false;
                                        btnCancel.Visible =true;
                                    }

                                }
                                if (user.role_name.ToString().Trim() == "Deputy Commissioner")
                                {
                                    btnSaveasDraft.Visible = false;
                                    if (partym.record_status == "A")
                                    {
                                        approverremaks.Visible = true;
                                        txtApproverremarks.Visible = true;
                                        btnApprove.Visible = true;
                                        btnReject.Visible = false;
                                        btnCancel.Visible = true;
                                    }
                                }
                                if (user.role_name.ToString().Trim() == "Commissioner")
                                {
                                    btnSaveasDraft.Visible = false;
                                    if (partym.record_status == "D")
                                    {
                                        approverremaks.Visible = true;
                                        txtApproverremarks.Visible = true;
                                        btnApprove.Visible = true;
                                        btnReject.Visible = false;
                                        btnCancel.Visible = true;
                                    }
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

        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                UserDetails user = new UserDetails();
                user.party_code = ddlpartyname.SelectedValue;
                user.financial_year = txtcurrentfinancial.Text;
                string val;
                DataTable table = new DataTable();
                table.Columns.Add("ID", typeof(int));
                table.Columns.Add("Table", typeof(string));
                table.Columns.Add("Name", typeof(string));
                table.Columns.Add("User", typeof(string));
                table.Columns.Add("Date", typeof(DateTime));
                //Party_Master party = new Party_Master();
                //party =BL_Party_Master.Checkparty(ddlpartyname.SelectedValue);
                UserDetails user1 = new UserDetails();
                user1 = BL_UserDetails.CheckUser(ddlpartyname.SelectedValue);
                // Here we add five DataRows.
                if (user1.party_type == "Distillery Unit" || user1.party_type == "ENA Distillery Unit" ||user1.party_type_code=="DIS" || user.party_type_code == "ENA")
                {
                    //table.Rows.Add(1, "rawmaterial_receipt", "rawmaterial_receipt", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(2, "rmreceipt_storage", "rmreceipt_storage", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(3, "rawmaterial_fermenter", "rawmaterial_fermenter", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(4, "fermenter_setup", "fermenter_setup", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(5, "distillation", "distillation", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(6, "distillation_tostore", "distillation_tostore", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(7, "fermenter_receiver", "fermenter_receiver", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(8, "fermenter_receiver_input", "fermenter_receiver_input", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(9, "fermenter_receiver_output", "fermenter_receiver_output", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(10, "receiver_storage_receipt", "receiver_storage_receipt", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(11, "receiver_storage_receiptvat", "receiver_storage_receiptvat", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(12, "receiver_storage_transfer", "receiver_storage_transfer", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(13, "storage_dispatch", "storage_dispatch", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(14, "dailydispatchclosure", "dailydispatchclosure", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(15, "vat_transfer", "vat_transfer", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(16, "rawmaterial_wastage", "rawmaterial_wastage", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(17, "molasses_indent","molasses_indent", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(18, "molasses_prov_prod", "molasses_prov_prod", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(19, "molasses_allotment_request", "molasses_allotment_request", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(20, "noc", "noc", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(21, "noc_depotdetail", "noc_depotdetail", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(20, "release_request", "release_request", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(23, "request_for_pass", "request_for_pass", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(24, "pass", "pass", Session["UserID"].ToString(), DateTime.Now);
                    table.Rows.Add(21, "openingbalance", "openingbalance", Session["UserID"].ToString(), DateTime.Now); 
                    table.Rows.Add(22, "vat_master", "vat_master", Session["UserID"].ToString(), DateTime.Now); 
                         //table.Rows.Add(23, "eascm_docs", "eascm_docs", Session["UserID"].ToString(), DateTime.Now);
                }
                if (user1.party_type == "Sugar Mill" || user1.party_type_code == "SGR")
                {
                    //table.Rows.Add(1, "sugarcanepurchase", "sugarcanepurchase", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(2, "dailymolassesproduction", "dailymolassesproduction", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(3, "molassesissueregister", "molassesissueregister", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(4, "vat_transfer", "vat_transfer", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(5, "molasses_indent","molasses_indent", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(6, "molasses_prov_prod", "molasses_prov_prod", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(7, "molasses_allotment_request", "molasses_allotment_request", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(8, "noc", "noc", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(9, "noc_depotdetail", "noc_depotdetail", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(8, "release_request", "release_request", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(11, "pass", "pass", Session["UserID"].ToString(), DateTime.Now);
                    table.Rows.Add(9, "openingbalance", "openingbalance", Session["UserID"].ToString(), DateTime.Now);
                    table.Rows.Add(10, "vat_master", "vat_master", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(11, "eascm_docs", "eascm_docs", Session["UserID"].ToString(), DateTime.Now);
                }

                if ( user1.party_type_code == "MTP" || user1.party_type_code == "MTR" || user1.party_type_code == "MTW")
                {
                   
                    //table.Rows.Add(1, "issue_register", "issue_register", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(2, "issue_register_item", "issue_register_item", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(3, "consumption_register", "consumption_register", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(4, "consumption_register_item", "consumption_register_item", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(5, "permit", "permit", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(6, "lic_application", "lic_application", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(7, "molasses_allotment_request", "molasses_allotment_request", Session["UserID"].ToString(), DateTime.Now);
                    table.Rows.Add(8, "openingbalance", "openingbalance", Session["UserID"].ToString(), DateTime.Now);
                    table.Rows.Add(9, "vat_master", "vat_master", Session["UserID"].ToString(), DateTime.Now);
                    //table.Rows.Add(10, "eascm_docs", "eascm_docs", Session["UserID"].ToString(), DateTime.Now);
                }

                //for (int i=0;i<=table.Rows.Count;i++)
                //{

                //}
                int i = 1;
                DataTable table1 = new DataTable();
                table1.Columns.Add("ID", typeof(int));
                table1.Columns.Add("Table", typeof(string));
                table1.Columns.Add("Status", typeof(string));
                table1.Columns.Add("User", typeof(string));
                foreach (DataRow dr in table.Rows)
                {
                    string name = dr["Table"].ToString();
                    string party_type = user1.party_type;
                    string party_code = user1.party_code;
                    string userid = ddlpartyname.SelectedValue;
                    string financialendate = enddate.Value;
                    string startdates = startdate.Value;
                    //foreach (var item in .ItemArray)
                    //{
                    //    console.Write("Value:" + item);
                    //}
                    //Label1.InnerText =name+ "......Processing...";
                    //done.InnerText = name + "......Processing...";
                 
                    val = BL_Party_Master.changefinancialyear(name,party_code,userid ,financialendate,startdates);
                    //done.InnerText = val;
                    table1.Rows.Add(i, name,val, Session["UserID"].ToString());
                    i++;
                    if(val.Substring(0,1)=="1")
                    {
                        break;
                    }
                    //grdApprovalDetails.DataSource = table1;
                    //grdApprovalDetails.DataBind();

                }
                Response.Redirect("ChangeFinancialYearList.aspx");
                //if (val == "0")
                //{
                //    Session["UserID"] = Session["UserID"];
                //    Response.Redirect("ReceiverTransferList");
                //}
                //else
                //{
                //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //    sb.Append("<script type = 'text/javascript'>");
                //    sb.Append("window.onload=function(){");
                //    sb.Append("alert('");
                //    sb.Append(val);
                //    sb.Append("')};");
                //    sb.Append("</script>");
                //    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                //}
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ChangeFinancialYearList.aspx");
          
        }



        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

                Party_Master party = new Party_Master();

                party.user_id = Session["UserID"].ToString();
                party.id = id.Value;
                party.remarks = txtApproverremarks.InnerText;
                party.party_code = Session["Party_Code1"].ToString();
                party.rolename = Session["Rolename"].ToString().Trim();
                if (Session["Rolename"].ToString().Trim() == "Bond Officer")
                {
                    party.record_status = "A";
                }
                if (Session["Rolename"].ToString().Trim() == "Deputy Commissioner")
                {
                    party.record_status = "D";
                }
                if (Session["Rolename"].ToString().Trim() == "Commissioner")
                {
                    party.record_status = "C";
                }
                

                    string val;

                    val = BL_Party_Master.Approve(party);

                    if (val == "0")
                    {
                      
                            Session["UserID"] = Session["UserID"];
                            Response.Redirect("ChangeFinancialYearList.aspx");
                      
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
                if (IsPostBack)
                {
               Party_Master party = new Party_Master();
                    party.user_id = Session["UserID"].ToString();
                    party.id = id.Value;
                    party.remarks = txtApproverremarks.InnerText;
                    party.party_code = Session["Party_Code1"].ToString();
                    party.record_status = "R";
                    party.rolename = Session["Rolename"].ToString().Trim();
                    string val;
                    val = BL_Party_Master.Approve(party);
                    if (val == "0")
                    {
                            Session["UserID"] = Session["UserID"];
                            Response.Redirect("ChangeFinancialYearList.aspx");
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
        }


        //private void approval()
        //{
        //        UserDetails user = new UserDetails();
        //        user.party_code = ddlpartyname.SelectedValue;
        //        user.financial_year = txtcurrentfinancial.Text;
        //        string val;
        //        DataTable table = new DataTable();
        //        table.Columns.Add("ID", typeof(int));
        //        table.Columns.Add("Table", typeof(string));
        //        table.Columns.Add("Name", typeof(string));
        //        table.Columns.Add("User", typeof(string));
        //        table.Columns.Add("Date", typeof(DateTime));
        //        //Party_Master party = new Party_Master();
        //        //party =BL_Party_Master.Checkparty(ddlpartyname.SelectedValue);
        //        UserDetails user1 = new UserDetails();
        //        user1 = BL_UserDetails.CheckUser(ddlpartyname.SelectedValue);
        //        // Here we add five DataRows.
        //        if (user1.party_type == "Distillery Unit" || user1.party_type == "ENA Distillery Unit" || user1.party_type_code == "DIS" || user.party_type_code == "ENA")
        //        {
        //            table.Rows.Add(1, "rawmaterial_receipt", "rawmaterial_receipt", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(2, "rmreceipt_storage", "rmreceipt_storage", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(3, "rawmaterial_fermenter", "rawmaterial_fermenter", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(4, "fermenter_setup", "fermenter_setup", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(5, "distillation", "distillation", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(6, "distillation_tostore", "distillation_tostore", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(7, "fermenter_receiver", "fermenter_receiver", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(8, "fermenter_receiver_input", "fermenter_receiver_input", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(9, "fermenter_receiver_output", "fermenter_receiver_output", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(10, "receiver_storage_receipt", "receiver_storage_receipt", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(11, "receiver_storage_receiptvat", "receiver_storage_receiptvat", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(12, "receiver_storage_transfer", "receiver_storage_transfer", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(13, "storage_dispatch", "storage_dispatch", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(14, "dailydispatchclosure", "dailydispatchclosure", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(15, "vat_transfer", "vat_transfer", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(16, "rawmaterial_wastage", "rawmaterial_wastage", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(17, "molasses_indent", "molasses_indent", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(18, "molasses_prov_prod", "molasses_prov_prod", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(19, "molasses_allotment_request", "molasses_allotment_request", Session["UserID"].ToString(), DateTime.Now);
        //            //table.Rows.Add(20, "noc", "noc", Session["UserID"].ToString(), DateTime.Now);
        //            //table.Rows.Add(21, "noc_depotdetail", "noc_depotdetail", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(20, "release_request", "release_request", Session["UserID"].ToString(), DateTime.Now);
        //            //table.Rows.Add(23, "request_for_pass", "request_for_pass", Session["UserID"].ToString(), DateTime.Now);
        //            //table.Rows.Add(24, "pass", "pass", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(21, "openingbalance", "openingbalance", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(22, "vat_master", "vat_master", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(23, "eascm_docs", "eascm_docs", Session["UserID"].ToString(), DateTime.Now);
        //        }
        //        if (user1.party_type == "Sugar Mill" || user1.party_type_code == "SGR")
        //        {
        //            table.Rows.Add(1, "sugarcanepurchase", "sugarcanepurchase", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(2, "dailymolassesproduction", "dailymolassesproduction", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(3, "molassesissueregister", "molassesissueregister", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(4, "vat_transfer", "vat_transfer", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(5, "molasses_indent", "molasses_indent", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(6, "molasses_prov_prod", "molasses_prov_prod", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(7, "molasses_allotment_request", "molasses_allotment_request", Session["UserID"].ToString(), DateTime.Now);
        //            //table.Rows.Add(8, "noc", "noc", Session["UserID"].ToString(), DateTime.Now);
        //            //table.Rows.Add(9, "noc_depotdetail", "noc_depotdetail", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(8, "release_request", "release_request", Session["UserID"].ToString(), DateTime.Now);
        //            //table.Rows.Add(11, "pass", "pass", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(9, "openingbalance", "openingbalance", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(10, "vat_master", "vat_master", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(11, "eascm_docs", "eascm_docs", Session["UserID"].ToString(), DateTime.Now);
        //        }

        //        if (user1.party_type_code == "MTP" || user1.party_type_code == "MTR" || user1.party_type_code == "MTW")
        //        {

        //            table.Rows.Add(1, "issue_register", "issue_register", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(2, "issue_register_item", "issue_register_item", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(3, "consumption_register", "consumption_register", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(4, "consumption_register_item", "consumption_register_item", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(5, "permit", "permit", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(6, "lic_application", "lic_application", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(7, "molasses_allotment_request", "molasses_allotment_request", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(8, "openingbalance", "openingbalance", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(9, "vat_master", "vat_master", Session["UserID"].ToString(), DateTime.Now);
        //            table.Rows.Add(10, "eascm_docs", "eascm_docs", Session["UserID"].ToString(), DateTime.Now);
        //        }

        //        //for (int i=0;i<=table.Rows.Count;i++)
        //        //{

        //        //}
        //        int i = 1;
        //        DataTable table1 = new DataTable();
        //        table1.Columns.Add("ID", typeof(int));
        //        table1.Columns.Add("Table", typeof(string));
        //        table1.Columns.Add("Status", typeof(string));
        //        table1.Columns.Add("User", typeof(string));
        //        foreach (DataRow dr in table.Rows)
        //        {
        //            string name = dr["Table"].ToString();
        //            string party_type = user1.party_type;
        //            string party_code = user1.party_code;
        //            string userid = ddlpartyname.SelectedValue;
        //            string financialendate = enddate.Value;
        //            //foreach (var item in .ItemArray)
        //            //{
        //            //    console.Write("Value:" + item);
        //            //}
        //            //Label1.InnerText =name+ "......Processing...";
        //            //done.InnerText = name + "......Processing...";

        //            val = BL_Party_Master.changefinancialyear(name, party_code, userid, financialendate);
        //            //done.InnerText = val;
        //            table1.Rows.Add(i, name, val, Session["UserID"].ToString());
        //            i++;
        //            if (val.Substring(0, 1) == "1")
        //            {
        //                break;
        //            }
        //            //grdApprovalDetails.DataSource = table1;
        //            //grdApprovalDetails.DataBind();

        //        }
        //        //if (val == "0")
        //        //{
        //        //    Session["UserID"] = Session["UserID"];
        //        //    Response.Redirect("ReceiverTransferList");
        //        //}
        //        //else
        //        //{
        //        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //        //    sb.Append("<script type = 'text/javascript'>");
        //        //    sb.Append("window.onload=function(){");
        //        //    sb.Append("alert('");
        //        //    sb.Append(val);
        //        //    sb.Append("')};");
        //        //    sb.Append("</script>");
        //        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
        //        //}
            
        //}

    }
}