using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.Entities;
using Usermngt.BL;
using Npgsql;
using System.Configuration;
using System.Data;
using System.Web.Services;

namespace UserMgmt
{
    public partial class OpeningBalanceForm : System.Web.UI.Page
    {
        DataTable vats = new DataTable();
        DataTable vats1 = new DataTable();
        List<OpeningBalance> openingbalance = new List<OpeningBalance>();
        List<Party_Master> party = new List<Party_Master>();
        OpeningBalance opening = new OpeningBalance();
        // static UserDetails user = new UserDetails();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                }
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/LoginPage");
                }
                grdOpeningBalanceView.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                string userid = Session["UserID"].ToString();
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(userid);
                Session["financial_year"] =user.financial_year;
                if (user != null)
                {
                    if (userid == "Admin")
                    {
                        Session["Party_code"] = Session["party_code1"].ToString();
                        Session["financial_year"] = Session["financial_year"].ToString();
                    }
                    else
                    {
                        Session["Party_code"] = user.party_code;
                    }
                       
                    if (user.party_type == "M & tP"|| user.party_code == "MTR" || user.party_code == "MTW")
                    {
                        MTP.Visible = true;
                        SCM.Visible = false;
                        if (user.party_code == "MTR" || user.party_code == "MTW")
                        {
                            btnConsumption.Visible = false;
                        }
                    }
                    else
                    {
                        MTP.Visible = false;
                        SCM.Visible = true;
                    }
                    if (ViewState["vats"] == null)
                    {
                        vats.Columns.Add("storage_content");
                        vats.Columns.Add("openingbalance_id");
                        vats.Columns.Add("vat_type_code");
                        vats.Columns.Add("vat_type_name");
                        vats.Columns.Add("vat_code");
                        vats.Columns.Add("vat_name");
                        vats.Columns.Add("record_status");
                        vats.Columns.Add("Total_Capacity");
                        vats.Columns.Add("openingbalancevalue");
                        vats.Columns.Add("uom_code");
                        vats.Columns.Add("uom_name");
                        ViewState["vats"] = vats;
                    }
                    if (user.party_type == "Distillery Unit" || user.party_type == "ENA Distillery Unit")
                        sgr.Visible = false;
                    else
                        dst.Visible = false;
                    if (user.role_name == "Bond Officer")
                    {
                        btnApprove.Visible = true;
                        btnReject.Visible = true;
                        btnCancel.Visible = false;
                        btnSubmit.Visible = false;
                        btnSaveasDraft.Visible = false;
                    }
                    else if (userid == "Admin")
                    {
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        btnCancel.Visible = true;
                        btnSubmit.Visible = true;
                        btnSaveasDraft.Visible = true;
                        approverremarks.Visible = false;
                        sgr.Visible = false;
                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        btnCancel.Visible = true;
                        btnSubmit.Visible = true;
                        btnSaveasDraft.Visible = true;
                        approverremarks.Visible = false;
                    }
                    if (Session["rtype"].ToString() != "0")
                    {

                        openingbalance = new List<OpeningBalance>();
                        openingbalance = BL_OpeningBalance.GetOpeningBalanceList(Session["Party_code"].ToString(), "");
                        var partyname22 = from s in openingbalance
                                        where s.financial_year == Session["ofinancial_year"].ToString()
                                        select s;
                        //if (Session["rtype"].ToString() == "1")
                        //{
                        int value = BL_OpeningBalance.GetExistsData("openingbalance", "party_code", user.party_code);
                        if (value == 1)
                        {
                            var partyname = from s in openingbalance
                                            where s.financial_year == Session["ofinancial_year"].ToString()
                                            select s;
                            for (int i = 0; i < partyname.ToList().Count; i++)
                            {
                                vats = (DataTable)ViewState["vats"];
                                //string d = grdOpeningBalanceView.Rows[i].Cells[0].Text;
                                //vats.Rows.Add(openingbalance[i].storage_content, openingbalance[i].openingbalance_id, openingbalance[i].vat_type_code, openingbalance[i].vat_type_name, openingbalance[i].vat_code, openingbalance[i].vat_name, openingbalance[i].record_status, openingbalance[i].Total_Capacity, openingbalance[i].openingbalancevalue, openingbalance[i].uom_code, openingbalance[i].uom_name);
                                //grdOpeningBalanceView.DataSource = vats;

                                vats.Rows.Add(partyname.ToList()[i].storage_content, partyname.ToList()[i].openingbalance_id, partyname.ToList()[i].vat_type_code, partyname.ToList()[i].vat_type_name, partyname.ToList()[i].vat_code, partyname.ToList()[i].vat_name, partyname.ToList()[i].record_status, partyname.ToList()[i].Total_Capacity, partyname.ToList()[i].openingbalancevalue, partyname.ToList()[i].uom_code, partyname.ToList()[i].uom_name);
                                grdOpeningBalanceView.DataSource = vats;
                                grdOpeningBalanceView.DataBind();


                            }
                            if (user.role_name == "Applicant")
                            {
                                int n1 = 0;
                                foreach (GridViewRow g1 in grdOpeningBalanceView.Rows)
                                {
                                    TextBox txt = g1.FindControl("txtOpeningBalance") as TextBox;

                                    //if ((Convert.ToDouble(txt.Text) >= 0 && (openingbalance[n1].record_status != "N" && openingbalance[n1].record_status != "R" && openingbalance[n1].record_status.Trim() != "")) || Session["rtype"].ToString() == "1")  && partyname.ToList()[n1].record_status != " " 
                                    if ((Convert.ToDouble(txt.Text) >= 0 && (partyname.ToList()[n1].record_status != "N" && partyname.ToList()[n1].record_status != "R" && partyname.ToList()[n1].record_status.Trim() != "")) || Session["rtype"].ToString() == "1")
                                    {
                                        txt.ReadOnly = true;
                                        approverremarks.Visible = false;
                                        List<All_Approvals> approvals = new List<All_Approvals>();
                                        approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), partyname.ToList()[n1].openingbalance_id.ToString(), "OBF");
                                        if (approvals.Count > 0)
                                        {
                                            grdApprovalDetails.DataSource = approvals;
                                            grdApprovalDetails.DataBind();
                                            approverid.Visible = true;
                                        }
                                        txtRemarks1.Value = partyname.ToList()[n1].remarks;
                                        txtRemarks1.Attributes.Add("Disabled", "Disabled");


                                    }
                                    else
                                    {
                                        if (partyname.ToList()[n1].record_status == "N" || partyname.ToList()[n1].record_status == "R")
                                        {
                                            btnCancel.Visible = true;
                                            btnSaveasDraft.Visible = true;
                                            btnSubmit.Visible = true;
                                        }
                                        txtRemarks1.Value = partyname.ToList()[n1].remarks;
                                    }
                                    n1++;
                                }
                            }
                            if (partyname22.ToList()[0].record_status == "A" || partyname22.ToList()[0].record_status == "Y")
                            {
                                btnCancel.Visible = false;
                                btnSaveasDraft.Visible = false;
                                btnSubmit.Visible = false;
                                approverid.Visible = true;
                            }
                        }
                        else
                        {
                            //var partyname = from s in openingbalance
                            //                select s;
                            for (int i = 0; i < openingbalance.Count; i++)
                            {
                                vats = (DataTable)ViewState["vats"];
                                //string d = grdOpeningBalanceView.Rows[i].Cells[0].Text;
                                vats.Rows.Add(openingbalance[i].storage_content, openingbalance[i].openingbalance_id, openingbalance[i].vat_type_code, openingbalance[i].vat_type_name, openingbalance[i].vat_code, openingbalance[i].vat_name, openingbalance[i].record_status, openingbalance[i].Total_Capacity, openingbalance[i].openingbalancevalue, openingbalance[i].uom_code, openingbalance[i].uom_name);
                                grdOpeningBalanceView.DataSource = vats;
                                grdOpeningBalanceView.DataBind();
                            }
                            if (user.role_name == "Applicant")
                            {
                                int n1 = 0;
                                foreach (GridViewRow g1 in grdOpeningBalanceView.Rows)
                                {
                                    TextBox txt = g1.FindControl("txtOpeningBalance") as TextBox;

                                    if ((Convert.ToDouble(txt.Text) >= 0 && (openingbalance[n1].record_status != "N" && openingbalance[n1].record_status != "R" && openingbalance[n1].record_status.Trim() != "")) || Session["rtype"].ToString() == "1")  
                                   // if ((Convert.ToDouble(txt.Text) >= 0 && (partyname.ToList()[n1].record_status != "N" && partyname.ToList()[n1].record_status != "R" && partyname.ToList()[n1].record_status.Trim() != "")) || Session["rtype"].ToString() == "1")
                                    {
                                        txt.ReadOnly = true;
                                        approverremarks.Visible = false;
                                        List<All_Approvals> approvals = new List<All_Approvals>();
                                        approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), openingbalance[n1].openingbalance_id.ToString(), "OBF");
                                        if (approvals.Count > 0)
                                        {
                                            grdApprovalDetails.DataSource = approvals;
                                            grdApprovalDetails.DataBind();
                                            approverid.Visible = true;
                                        }
                                        txtRemarks1.Value = openingbalance[n1].remarks;
                                        txtRemarks1.Attributes.Add("Disabled", "Disabled");


                                    }
                                    else
                                    {
                                        if (openingbalance[n1].record_status == "N" && openingbalance[n1].record_status == "R")
                                        {
                                            btnCancel.Visible = true;
                                            btnSaveasDraft.Visible = true;
                                            btnSubmit.Visible = true;
                                        }
                                        txtRemarks1.Value = openingbalance[n1].remarks;
                                    }
                                    n1++;
                                }
                            }
                            if (openingbalance[0].record_status == "A" || openingbalance[0].record_status == "Y")
                            {
                                btnCancel.Visible = false;
                                btnSaveasDraft.Visible = false;
                                btnSubmit.Visible = false;
                                approverid.Visible = true;
                            }
                        }
                       

                        if (userid == "Admin")
                        {
                            for (int i = 0; i < openingbalance.Count; i++)
                            {
                                vats = (DataTable)ViewState["vats"];
                                //string d = grdOpeningBalanceView.Rows[i].Cells[0].Text;
                                vats.Rows.Add(openingbalance[i].storage_content, openingbalance[i].openingbalance_id, openingbalance[i].vat_type_code, openingbalance[i].vat_type_name, openingbalance[i].vat_code, openingbalance[i].vat_name, openingbalance[i].record_status, openingbalance[i].Total_Capacity, openingbalance[i].openingbalancevalue, openingbalance[i].uom_code, openingbalance[i].uom_name);
                                grdOpeningBalanceView.DataSource = vats;
                                grdOpeningBalanceView.DataBind();

                            }
                            int n1 = 0;
                            foreach (GridViewRow g1 in grdOpeningBalanceView.Rows)
                            {
                                TextBox txt = g1.FindControl("txtOpeningBalance") as TextBox;

                                if ((Convert.ToDouble(txt.Text) >= 0 && (openingbalance[n1].record_status != "N" && openingbalance[n1].record_status != "R" && openingbalance[n1].record_status.Trim() != "")) || Session["rtype"].ToString() == "1")
                                {
                                    txt.ReadOnly = true;
                                    approverremarks.Visible = false;
                                    List<All_Approvals> approvals = new List<All_Approvals>();
                                    approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), openingbalance[n1].openingbalance_id.ToString(), "OBF");
                                    if (approvals.Count > 0)
                                    {
                                        var list4 = (from s in approvals
                                                   //  where s.financial_year == user.financial_year
                                                     select s);
                                        grdApprovalDetails.DataSource = list4.ToList();
                                        grdApprovalDetails.DataBind();
                                        approverid.Visible = true;
                                    }
                                    txtRemarks1.Value = openingbalance[n1].remarks;
                                    txtRemarks1.Attributes.Add("Disabled", "Disabled");


                                }
                                else
                                {
                                    btnCancel.Visible = true;
                                    btnSaveasDraft.Visible = true;
                                    btnSubmit.Visible = true;
                                    txtRemarks1.Value = openingbalance[n1].remarks;
                                }
                                n1++;
                            }
                        }
                        else if (user.role_name == "Bond Officer")
                        {
                            var partynames = from s in openingbalance
                                             where s.party_code == user.party_code && s.record_status != "N" && s.financial_year == Session["ofinancial_year"].ToString()
                                             select s;
                            grdOpeningBalanceView.DataSource = partynames.ToList();
                            grdOpeningBalanceView.DataBind();
                            int n1 = 0;
                            txtRemarks1.Attributes.Add("Disabled", "Disabled");
                            btnCancel.Visible = false;
                            btnSubmit.Visible = false;
                            btnSaveasDraft.Visible = false;
                            approverremarks.Visible = true;
                            approverid.Visible = true;
                            txtRemarks1.Attributes.Add("Disabled", "Disabled");

                            foreach (GridViewRow g1 in grdOpeningBalanceView.Rows)
                            {
                                Label txt1 = g1.FindControl("lblstatus") as Label;
                                TextBox txt = g1.FindControl("txtOpeningBalance") as TextBox;
                                if (n1 == 0)
                                {
                                    List<All_Approvals> approvals = new List<All_Approvals>();
                                    approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), partynames.ToList()[n1].openingbalance_id.ToString(), "OBF");
                                    if (approvals.Count > 0)
                                    {
                                        var list4 = (from s in approvals
                                                   //  where s.financial_year == user.financial_year
                                                     select s);
                                        grdApprovalDetails.DataSource = list4.ToList();

                                        grdApprovalDetails.DataBind();
                                        approverid.Visible = true;
                                    }
                                    txtRemarks1.Value = partynames.ToList()[0].remarks;

                                }
                                //if(txt1.Text!="A")
                                //{
                                //    btnApprove.Visible = true;
                                //    btnReject.Visible = true;
                                //    approverremarks.Visible = true;
                                //}
                                txt.ReadOnly = true;
                                if (txt1.Text == "Approved" || txt1.Text == "Rejected")
                                {
                                    btnApprove.Visible = false;
                                    btnReject.Visible = false;
                                    approverremarks.Visible = false;
                                }

                                n1++;
                            }
                            //if (openingbalance[0].record_status != "Y" )
                            //{
                            //   // txtapproverremarks.Attributes.Add("Disabled", "Disabled");
                            //    btnApprove.Visible = false;
                            //    btnReject.Visible = false;
                            //}
                        }
                        //if(user.role_name == "Bond Officer" && openingbalance[0].record_status=="Y")
                        // {
                        //     approverremarks.Visible = true;
                        //     approverid.Visible = true;
                        //     grdOpeningBalanceView.Attributes.Add("disabled", "disabled");
                        // }

                        // if (Session["rtype"].ToString() == "1" || openingbalance[0].record_status == "Y")
                        // {
                        //     txtRemarks1.Attributes.Add("Disabled", "Disabled");
                        //     btnCancel.Visible = false;
                        //     btnSubmit.Visible = false;
                        //     btnSaveasDraft.Visible = false;


                        // }


                        }
                        else
                        {
                            openingbalance = new List<OpeningBalance>();
                            openingbalance = BL_OpeningBalance.GetOpeningBalanceList("", "");

                            if (openingbalance.Count <= 0)
                            {
                                btnCancel.Visible = false;
                                btnSubmit.Visible = false;
                                btnSaveasDraft.Visible = false;
                                txtRemarks1.Visible = false;
                                approverid.Visible = false;
                                remark.Visible = false;
                            }
                            else
                            {
                                if (userid == "Admin")
                                {
                                    var partynames = from s in openingbalance
                                                     where s.party_code == openingbalance[0].party_code
                                                     select s;
                                    for (int i = 0; i < partynames.ToList().Count; i++)
                                    {
                                        vats = (DataTable)ViewState["vats"];
                                        // string d = grdOpeningBalanceView.Rows[i].Cells[0].Text;
                                        vats.Rows.Add(partynames.ToList()[i].storage_content, partynames.ToList()[i].openingbalance_id, partynames.ToList()[i].vat_type_code, partynames.ToList()[i].vat_type_name, partynames.ToList()[i].vat_code, partynames.ToList()[i].vat_name, partynames.ToList()[i].record_status, partynames.ToList()[i].openingbalancevalue, partynames.ToList()[i].uom_code, partynames.ToList()[i].uom_name);
                                        grdOpeningBalanceView.DataSource = vats;
                                        grdOpeningBalanceView.DataBind();

                                    }
                                    btnApprove.Visible = false;
                                    btnReject.Visible = false;
                                    int n = 0;
                                    if (openingbalance[0].record_status == "Y")
                                    {

                                        foreach (GridViewRow g1 in grdOpeningBalanceView.Rows)
                                        {
                                            TextBox txt = g1.FindControl("txtOpeningBalance") as TextBox;
                                            LinkButton btn = g1.FindControl("btnEdit") as LinkButton;
                                            if (openingbalance[n].record_status == "Y")
                                            {
                                                txt.ReadOnly = true;
                                                txtRemarks1.Attributes.Add("Disabled", "Disabled");
                                            }

                                            n++;
                                        }
                                    }
                                }
                                else if (user.role_name.Contains("Bond"))
                                {
                                    var partynames = from s in openingbalance
                                                     where s.party_code == user.party_code && s.record_status != "N"
                                                     select s;
                                    txtRemarks1.Attributes.Add("Disabled", "Disabled");
                                    for (int i = 0; i < partynames.ToList().Count; i++)
                                    {
                                        vats = (DataTable)ViewState["vats"];
                                        // string d = grdOpeningBalanceView.Rows[i].Cells[0].Text;
                                        vats.Rows.Add(partynames.ToList()[i].storage_content, partynames.ToList()[i].openingbalance_id, partynames.ToList()[i].vat_type_code, partynames.ToList()[i].vat_type_name, partynames.ToList()[i].vat_code, partynames.ToList()[i].vat_name, partynames.ToList()[i].record_status, partynames.ToList()[i].openingbalancevalue, partynames.ToList()[i].uom_code, partynames.ToList()[i].uom_name);
                                        grdOpeningBalanceView.DataSource = vats;
                                        grdOpeningBalanceView.DataBind();

                                    }
                                    grdOpeningBalanceView.Enabled = false;
                                }
                                else
                                {
                                    if (openingbalance[0].record_status == "A" || openingbalance[0].record_status == "R")
                                    {
                                        approverremarks.Visible = true;
                                    }

                                    var partynames = from s in openingbalance
                                                     where s.party_code == user.party_code
                                                     select s;
                                    for (int i = 0; i < partynames.ToList().Count; i++)
                                    {
                                        vats = (DataTable)ViewState["vats"];
                                        // string d = grdOpeningBalanceView.Rows[i].Cells[0].Text;
                                        vats.Rows.Add(partynames.ToList()[i].storage_content, partynames.ToList()[i].openingbalance_id, partynames.ToList()[i].vat_type_code, partynames.ToList()[i].vat_type_name, partynames.ToList()[i].vat_code, partynames.ToList()[i].vat_name, partynames.ToList()[i].record_status, partynames.ToList()[i].openingbalancevalue, partynames.ToList()[i].uom_code, partynames.ToList()[i].uom_name);
                                        grdOpeningBalanceView.DataSource = vats;
                                        grdOpeningBalanceView.DataBind();

                                    }
                                    int n = 0;
                                    foreach (GridViewRow g1 in grdOpeningBalanceView.Rows)
                                    {
                                        TextBox txt = g1.FindControl("txtOpeningBalance") as TextBox;
                                        if (partynames.ToList()[n].record_status == "Y")
                                            txt.ReadOnly = true;
                                        else
                                        {
                                            btnCancel.Visible = true;
                                            btnSaveasDraft.Visible = true;
                                            btnSubmit.Visible = true;
                                        }
                                        n++;
                                    }

                                }
                            }
                        }

                    //openingbalance = BL_OpeningBalance.GetOpeningBalanceList(Session["party_code"].ToString(), "");
                    //var partynames1 = from s in openingbalance
                    //                 where s.party_code == user.party_code && s.financial_year == user.financial_year
                    //                 select s;
                    //if (partynames1.ToList()[0].record_status != "A")
                    //{
                    //    lnkRMR.Visible = false;
                    //    lnkRawMaterialToFermenter.Visible = false;
                    //    lnkFermentertoReceiver.Visible = false;
                    //    lnkReceivertoStorage.Visible = false;
                    //    lnkFromStoragetoDispatch.Visible = false;
                    //    lnkDailyDispatchClosure.Visible = false;
                    //    btnVATtansfers.Visible = false;
                    //    lnkRawMaterialWastage.Visible = false;
                    //    LinkButton1.Visible = false;
                    //    btnIssue.Visible = false;
                    //    btnConsumption.Visible = false;
                    //    btnRG4.Visible = false;
                    //    btnDMP.Visible = false;
                    //    btnMIR.Visible = false;
                    //    LinkButton2.Visible = false;
                    //}
                }
                }
            
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //openingbalance = new List<OpeningBalance>();
            //openingbalance = BL_OpeningBalance.GetOpeningBalanceList(ddpartyName.SelectedValue.ToString());
            //grdOpeningBalanceView.DataSource = openingbalance;
            //grdOpeningBalanceView.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                btnSubmit.Enabled = false;
                // string userid = Session["UserID"].ToString();
                openingbalance = new List<OpeningBalance>();
                vats = (DataTable)ViewState["vats"];
                foreach (DataRow g1 in vats.Rows)
                {
                    OpeningBalance opbalance = new OpeningBalance();
                    opbalance.storage_content = g1["storage_content"].ToString();
                    opbalance.vat_type_code = g1["vat_type_code"].ToString();
                    opbalance.vat_code = g1["vat_code"].ToString();
                    opbalance.uom_code = g1["uom_code"].ToString();
                    opbalance.vat_type_name = g1["vat_type_name"].ToString();
                    opbalance.vat_name = g1["vat_name"].ToString();
                    if (g1["openingbalancevalue"].ToString() != "")
                        opbalance.openingbalancevalue = Convert.ToDouble(g1["openingbalancevalue"].ToString());
                    else
                        opbalance.openingbalancevalue = 0;
                    opbalance.uom_name = g1["uom_name"].ToString();
                    opbalance.user_id = Session["UserID"].ToString();
                    opbalance.openingbalanceyear = Session["financial_year"].ToString();
                    opbalance.financial_year = Session["financial_year"].ToString();
                    opbalance.remarks = txtRemarks1.Value;
                    opbalance.record_status = "Y";
                    opbalance.openingbalance_id = g1["openingbalance_id"].ToString();
                    opbalance.party_code = Session["party_code"].ToString();
                    openingbalance.Add(opbalance);

                }
                string val;

                val = BL_OpeningBalance.InsertOpeningbalance(openingbalance);

                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("OpeningBalanceList");
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
            Response.Redirect("OpeningBalanceList");
        }
        protected void lnkRawMaterialWastage_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawmaterialWastageList.aspx");
        }
        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("OpeningBalanceList");
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
        protected void btnIssue_Click(object sender, EventArgs e)
        {
            if (Session["Party_code"].ToString() == "MTR" || Session["Party_code"].ToString() == "MTW")
            {
                Response.Redirect("MNTW_IssueList.aspx");
            }
            else
            {
                Response.Redirect("MNT_IssueList.aspx");
            }
        }

        protected void btnConsumption_Click(object sender, EventArgs e)
        {
            Response.Redirect("MNT_ConsumptionList.aspx");
        }
        protected void btnOpeningBalance_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("OpeningBalanceList");
        }


        protected void grdOpeningBalanceView_DataBinding(object sender, EventArgs e)
        {


        }

        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                btnSaveasDraft.Enabled = false;
                // string userid = Session["UserID"].ToString();
                openingbalance = new List<OpeningBalance>();
                vats = (DataTable)ViewState["vats"];
                foreach (DataRow g1 in vats.Rows)
                {
                    OpeningBalance opbalance = new OpeningBalance();
                    opbalance.storage_content = g1["storage_content"].ToString();
                    opbalance.vat_type_code = g1["vat_type_code"].ToString();
                    opbalance.vat_code = g1["vat_code"].ToString();
                    opbalance.uom_code = g1["uom_code"].ToString();
                    opbalance.vat_type_name = g1["vat_type_name"].ToString();
                    opbalance.vat_name = g1["vat_name"].ToString();
                    if (g1["openingbalancevalue"].ToString() != "")
                        opbalance.openingbalancevalue = Convert.ToDouble(g1["openingbalancevalue"].ToString());
                    else
                        opbalance.openingbalancevalue = 0;
                    opbalance.uom_name = g1["uom_name"].ToString();
                    opbalance.user_id = Session["UserID"].ToString();
                    opbalance.openingbalanceyear = Session["financial_year"].ToString();
                    opbalance.financial_year = Session["financial_year"].ToString();
                    opbalance.remarks = txtRemarks1.Value;
                    opbalance.record_status = "N";
                    opbalance.openingbalance_id = g1["openingbalance_id"].ToString();
                    opbalance.party_code = Session["party_code"].ToString();
                    openingbalance.Add(opbalance);

                }
                string val;
                string h = Session["rtype"].ToString();

                val = BL_OpeningBalance.InsertOpeningbalance(openingbalance);

                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("OpeningBalanceList");
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

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            // string userid = Session["UserID"].ToString();
            openingbalance = new List<OpeningBalance>();
            string party_code = Session["party_code"].ToString();
            string val;
            openingbalance = new List<OpeningBalance>();
            vats = (DataTable)ViewState["vats"];
            foreach (DataRow g1 in vats.Rows)
            {
                OpeningBalance opbalance = new OpeningBalance();
                opbalance.storage_content = g1["storage_content"].ToString();
                opbalance.vat_type_code = g1["vat_type_code"].ToString();
                opbalance.vat_code = g1["vat_code"].ToString();
                opbalance.uom_code = g1["uom_code"].ToString();
                opbalance.vat_type_name = g1["vat_type_name"].ToString();
                opbalance.vat_name = g1["vat_name"].ToString();
                if (g1["openingbalancevalue"].ToString() != "")
                    opbalance.openingbalancevalue = Convert.ToDouble(g1["openingbalancevalue"].ToString());
                else
                    opbalance.openingbalancevalue = 0;
                opbalance.uom_name = g1["uom_name"].ToString();
                opbalance.user_id = Session["UserID"].ToString();
                opbalance.remarks = txtapproverremarks.Value;
                if (g1["record_status"].ToString() != "A")
                    opbalance.record_status = "A";
                opbalance.openingbalanceyear = Session["financial_year"].ToString();
                opbalance.financial_year = Session["financial_year"].ToString();
                opbalance.openingbalance_id = g1["openingbalance_id"].ToString();
                opbalance.party_code = Session["party_code"].ToString();
                openingbalance.Add(opbalance);

            }
            val = BL_OpeningBalance.Approve(openingbalance);
            if (val == "0")
            {
                Session["UserID"] = Session["UserID"];
                Response.Redirect("OpeningBalanceList");
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
            //string userid = Session["UserID"].ToString();
            openingbalance = new List<OpeningBalance>();
            string party_code = Session["party_code"].ToString();
            string val;
            openingbalance = new List<OpeningBalance>();
            openingbalance = new List<OpeningBalance>();
            vats = (DataTable)ViewState["vats"];
            foreach (DataRow g1 in vats.Rows)
            {
                OpeningBalance opbalance = new OpeningBalance();
                opbalance.storage_content = g1["storage_content"].ToString();
                opbalance.vat_type_code = g1["vat_type_code"].ToString();
                opbalance.vat_code = g1["vat_code"].ToString();
                opbalance.uom_code = g1["uom_code"].ToString();
                opbalance.vat_type_name = g1["vat_type_name"].ToString();
                opbalance.vat_name = g1["vat_name"].ToString();
                if (g1["openingbalancevalue"].ToString() != "")
                    opbalance.openingbalancevalue = Convert.ToDouble(g1["openingbalancevalue"].ToString());
                else
                    opbalance.openingbalancevalue = 0;
                opbalance.uom_name = g1["uom_name"].ToString();
                opbalance.user_id = Session["UserID"].ToString();
                opbalance.remarks = txtapproverremarks.Value;
                if (g1["record_status"].ToString() != "A")
                    opbalance.record_status = "R";

                opbalance.openingbalance_id = g1["openingbalance_id"].ToString();
                opbalance.party_code = Session["party_code"].ToString();
                opbalance.financial_year = Session["financial_year"].ToString();
                openingbalance.Add(opbalance);

            }
            val = BL_OpeningBalance.Approve(openingbalance);
            if (val == "0")
            {
                Session["UserID"] = Session["UserID"];
                Response.Redirect("OpeningBalanceList");
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

        protected void grdOpeningBalanceView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            if (e.NewPageIndex == -1)
            {
                grdOpeningBalanceView.PageIndex = 0;
            }

            else
            {
                grdOpeningBalanceView.PageIndex = e.NewPageIndex;
            }
            UserDetails user = new UserDetails();
            GridViewRow row = grdOpeningBalanceView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["obfsearch"] != null && Session["obftext"] != null)
            {
                ddsearch.SelectedValue = Session["obfsearch"].ToString();
                txtpage.Text = Session["obftext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        openingbalance = new List<OpeningBalance>();
                        openingbalance = BL_OpeningBalance.Search("", ddsearch.SelectedValue, txtpage.Text, Session["Party_code"].ToString(), "");
                        var partynames = from s in openingbalance
                                         where s.party_code == openingbalance[0].party_code && s.financial_year == Session["ofinancial_year"].ToString()
                                         select s;
                        for (int i = 0; i < partynames.ToList().Count; i++)
                        {
                            vats = (DataTable)ViewState["vats"];
                            // string d = grdOpeningBalanceView.Rows[i].Cells[0].Text;
                           // vats.Rows.Add(partynames.ToList()[i].storage_content, partynames.ToList()[i].openingbalance_id, partynames.ToList()[i].vat_type_code, partynames.ToList()[i].vat_type_name, partynames.ToList()[i].vat_code, partynames.ToList()[i].vat_name, partynames.ToList()[i].record_status, partynames.ToList()[i].openingbalancevalue, partynames.ToList()[i].uom_code, partynames.ToList()[i].uom_name);
                            grdOpeningBalanceView.DataSource = openingbalance;
                            grdOpeningBalanceView.DataBind();

                            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                            int n1 = 0;
                            foreach (GridViewRow g1 in grdOpeningBalanceView.Rows)
                            {
                                TextBox txt = g1.FindControl("txtOpeningBalance") as TextBox;
                                Label status = g1.FindControl("lblstatus") as Label;
                                if (status.Text == "Approved" || Session["rtype"].ToString() == "1" || user.role_name != "Applicant")
                                    txt.ReadOnly = true;
                                else
                                {
                                    if (Session["rtype"].ToString() != "1")
                                    {
                                        btnCancel.Visible = true;
                                        btnSaveasDraft.Visible = true;
                                        btnSubmit.Visible = true;
                                    }
                                }
                                n1++;
                            }

                        }
                    }
                    }
                }
                else
                {


                    vats = (DataTable)ViewState["vats"];
                    grdOpeningBalanceView.DataSource = vats;
                    grdOpeningBalanceView.DataBind();


                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    int n1 = 0;
                    foreach (GridViewRow g1 in grdOpeningBalanceView.Rows)
                    {
                        TextBox txt = g1.FindControl("txtOpeningBalance") as TextBox;
                        Label status = g1.FindControl("lblstatus") as Label;
                        if (status.Text == "Approved" || Session["rtype"].ToString() == "1" || user.role_name != "Applicant")
                            txt.ReadOnly = true;
                        else
                        {
                            if (Session["rtype"].ToString() != "1")
                            {
                                btnCancel.Visible = true;
                                btnSaveasDraft.Visible = true;
                                btnSubmit.Visible = true;
                            }
                        }
                        n1++;
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
        protected void btnVATtansfers_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATTransferList");
        }

        protected void lnkRMR_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialReceiptList");
        }

        protected void txtOpeningBalance_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                TextBox lb = (TextBox)sender;
                GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
                int i = gvRow.RowIndex;
                int page = (((grdOpeningBalanceView.PageIndex) * Convert.ToInt32(Session["pagesize"].ToString())) + i);
                DataTable dt2 = (DataTable)ViewState["vats"];

                string value = (grdOpeningBalanceView.Rows[i].Cells[6].FindControl("txtOpeningBalance") as TextBox).Text;
                string total = (grdOpeningBalanceView.Rows[i].Cells[5].FindControl("txttotalcapacity") as TextBox).Text;
                if (value == "")
                    value = "0";
                if (total == "")
                    total = "0";
                if (Convert.ToDouble(total) >= Convert.ToDouble(value))
                {
                    dt2.Rows[page]["openingbalancevalue"] = value;
                    ViewState["vats"] = dt2;
                    vats = (DataTable)ViewState["vats"];
                    grdOpeningBalanceView.DataSource = dt2;
                    grdOpeningBalanceView.DataBind();
                    int n1 = 0;
                    foreach (GridViewRow g1 in grdOpeningBalanceView.Rows)
                    {
                        TextBox txt = g1.FindControl("txtOpeningBalance") as TextBox;
                        Label ststus = g1.FindControl("lblstatus") as Label;
                        if (txt.Text != "")
                        {
                            if ((Convert.ToDouble(txt.Text) > 0 && (ststus.Text != "N" && ststus.Text != "R" && ststus.Text.Trim() != "")) || Session["rtype"].ToString() == "1")
                            {
                                txt.ReadOnly = false;
                            }
                        }
                        n1++;
                    }
                }
                else
                {
                    (grdOpeningBalanceView.Rows[i].Cells[6].FindControl("txtOpeningBalance") as TextBox).Text = "";
                    (grdOpeningBalanceView.Rows[i].Cells[6].FindControl("txtOpeningBalance") as TextBox).Focus();
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append("Opening cannot be greater than VAT storage capacity");
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                }
            }
        }

        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdOpeningBalanceView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                vats = (DataTable)ViewState["vats"];
                grdOpeningBalanceView.DataSource = vats;
                grdOpeningBalanceView.DataBind();
               
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                int n1 = 0;
                foreach (GridViewRow g1 in grdOpeningBalanceView.Rows)
                {
                    TextBox txt = g1.FindControl("txtOpeningBalance") as TextBox;
                    Label status = g1.FindControl("lblstatus") as Label;
                    if (status.Text == "Approved" || Session["rtype"].ToString() == "1" || user.role_name != "Applicant")
                        txt.ReadOnly = true;
                    else
                    {
                        if (Session["rtype"].ToString() != "1")
                        {
                            btnCancel.Visible = true;
                            btnSaveasDraft.Visible = true;
                            btnSubmit.Visible = true;
                        }
                    }
                    n1++;
                }


            }
            return vats.ToString();
        }



        protected void btnsearch_Click(object sender, EventArgs e)
        {


            GridViewRow row = grdOpeningBalanceView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["obfsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["obftext"] = txtpage.Text;
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        openingbalance = new List<OpeningBalance>();
                        openingbalance = BL_OpeningBalance.Search("",ddsearch.SelectedValue,txtpage.Text.Trim(),Session["Party_code"].ToString(), "");
                        for (int i = 0; i < openingbalance.Count; i++)
                        {
                            vats1 = (DataTable)ViewState["vats"];
                            //string d = grdOpeningBalanceView.Rows[i].Cells[0].Text;
                         //   vats1.Rows.Add(openingbalance[i].storage_content, openingbalance[i].openingbalance_id, openingbalance[i].vat_type_code, openingbalance[i].vat_type_name, openingbalance[i].vat_code, openingbalance[i].vat_name, openingbalance[i].record_status, openingbalance[i].Total_Capacity, openingbalance[i].openingbalancevalue, openingbalance[i].uom_code, openingbalance[i].uom_name);
                            grdOpeningBalanceView.DataSource =openingbalance;
                            grdOpeningBalanceView.DataBind();

                        }
                        //var partynames = from s in openingbalance
                        //                 where s.party_code == openingbalance[0].party_code
                        //                 select s;
                        //    for (int i = 0; i < partynames.ToList().Count; i++)
                        //    {
                        //        vats = (DataTable)ViewState["vats"];
                        //        // string d = grdOpeningBalanceView.Rows[i].Cells[0].Text;
                        //        vats.Rows.Add(partynames.ToList()[i].storage_content, partynames.ToList()[i].openingbalance_id, partynames.ToList()[i].vat_type_code, partynames.ToList()[i].vat_type_name, partynames.ToList()[i].vat_code, partynames.ToList()[i].vat_name, partynames.ToList()[i].record_status, partynames.ToList()[i].openingbalancevalue, partynames.ToList()[i].uom_code, partynames.ToList()[i].uom_name);
                        //        grdOpeningBalanceView.DataSource = vats;
                        //        grdOpeningBalanceView.DataBind();

                        user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                        int n1 = 0;
                        foreach (GridViewRow g1 in grdOpeningBalanceView.Rows)
                        {
                            TextBox txt = g1.FindControl("txtOpeningBalance") as TextBox;
                            Label status = g1.FindControl("lblstatus") as Label;
                            if (status.Text == "Approved" || Session["rtype"].ToString() == "1" || user.role_name != "Applicant")
                                txt.ReadOnly = true;
                            else
                            {
                                if (Session["rtype"].ToString() != "1")
                                {
                                    btnCancel.Visible = true;
                                    btnSaveasDraft.Visible = true;
                                    btnSubmit.Visible = true;
                                }
                            }
                            n1++;
                        }

                    }
                }
                    
                       
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Select Field Name\');", true);
                ddsearch.Focus();
            }



        }

        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdOpeningBalanceView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            if (Convert.ToInt32(txtpage.Text) < 1)
            {
                a = 0;
            }
            else
            {
                a = Convert.ToInt32(txtpage.Text);
            }

            if (Convert.ToInt32(txtpage.Text) <= 0)
            {
                grdOpeningBalanceView.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdOpeningBalanceView.PageIndex = a - 1;
            }


            vats = (DataTable)ViewState["vats"];
            grdOpeningBalanceView.DataSource = vats;
            grdOpeningBalanceView.DataBind();
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            int n1 = 0;
            foreach (GridViewRow g1 in grdOpeningBalanceView.Rows)
            {
                TextBox txt = g1.FindControl("txtOpeningBalance") as TextBox;
                Label status = g1.FindControl("lblstatus") as Label;
                if (status.Text == "Approved" || Session["rtype"].ToString() == "1" || user.role_name != "Applicant")
                    txt.ReadOnly = true;
                else
                {
                    if (Session["rtype"].ToString() != "1")
                    {
                        btnCancel.Visible = true;
                        btnSaveasDraft.Visible = true;
                        btnSubmit.Visible = true;
                    }
                }
                n1++;
            }







        }

        protected void grdOpeningBalanceView_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdOpeningBalanceView.TopPagerRow;
            grdOpeningBalanceView.TopPagerRow.Visible = true;
            if (row == null)
            {
                return;
            }

            DropDownList DDLPage = (DropDownList)row.Cells[0].FindControl("DDLPage");
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            Label lblPages = (Label)row.Cells[0].FindControl("lblPages");
            Label lblCurrent = (Label)row.Cells[0].FindControl("lblCurrent");
            TextBox txtpages = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["obfsearch"] != null && Session["obftext"] != null)
            {
                ddsearch.SelectedValue = Session["obfsearch"].ToString();
                txtpages.Text = Session["obftext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdOpeningBalanceView.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdOpeningBalanceView.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdOpeningBalanceView.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdOpeningBalanceView.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdOpeningBalanceView.PageIndex == 0)
            {
                ((ImageButton)grdOpeningBalanceView.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdOpeningBalanceView.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdOpeningBalanceView.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdOpeningBalanceView.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdOpeningBalanceView.PageIndex + 1 == grdOpeningBalanceView.PageCount)
            {
                ((ImageButton)grdOpeningBalanceView.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdOpeningBalanceView.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdOpeningBalanceView.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdOpeningBalanceView.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["obfsearch"] = null;
            Session["obftext"] = null;
            vats = (DataTable)ViewState["vats"];
            grdOpeningBalanceView.DataSource = vats;
            grdOpeningBalanceView.DataBind();
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            int n1 = 0;
            foreach (GridViewRow g1 in grdOpeningBalanceView.Rows)
            {
                TextBox txt = g1.FindControl("txtOpeningBalance") as TextBox;
                Label status = g1.FindControl("lblstatus") as Label;
                if (status.Text == "Approved" || Session["rtype"].ToString() == "1" || user.role_name != "Applicant")
                    txt.ReadOnly = true;
                else
                {
                    if (Session["rtype"].ToString() != "1")
                    {
                        btnCancel.Visible = true;
                        btnSaveasDraft.Visible = true;
                        btnSubmit.Visible = true;
                    }
                }
                n1++;
            }

        }
    }
}