using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class ReleaseRequestForm : System.Web.UI.Page
    {
      // static UserDetails user = new UserDetails();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    if (ViewState["Records"] == null)
                    {
                        dt.Columns.Add("Doc_Name");
                        dt.Columns.Add("Discription");
                        dt.Columns.Add("Doc_Path");
                        dt.Columns.Add("Doc_id");
                        ViewState["Records"] = dt;
                    }
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        Session["level"] = user.role_level_code;
                        Session["rolename"] = user.role_name;
                        if (user.party_type == "M & tP")
                        {
                            lblDepot.Text = "ENA/Spirit Final Allotment No";
                            molasses.Visible = false;
                            ENA.Visible = true;
                            MTB.Visible = false;
                            ETB.Visible = true;
                        }
                        else
                        {
                            lblDepot.Text = "Molasses Final Allotment No";
                            molasses.Visible = true;
                            ENA.Visible = false;
                            MTB.Visible = true;
                            ETB.Visible = false;
                        }
                            valieddate12.Visible = false;
                        txtValiedDate.ReadOnly = true;
                        if (Session["rtype"].ToString() == "0")
                        {
                            approverremaks.Visible = false;
                            string allotno = Session["AllotmentNo"].ToString();
                            string product_code = Session["product_code"].ToString();
                            productcode.Value = product_code;
                            btnApprove.Visible = false;
                            btnReject.Visible = false; 
                            List<Release_Request> rr = new List<Release_Request>();
                            rr = BL_Release_Request.GetList();
                            txtReleaseRequestNo.Text = BL_Release_Request.GetMax(user.party_code);
                            //   txtReleaseRequestNo.Text = BL_Release_Request.GetRRMax(user.party_code);
                            partycode.Value = user.party_code;
                            var list = from s in rr
                                       where s.party_code == user.party_code && s.rr_allotmentno == allotno && s.product_code == product_code
                                       select s;
                            txtReleaseRequestDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                            txtFiscalYear.Text = user.financial_year;
                            txtMolassesFinalAllotmentNo.Text = list.ToList()[0].final_allotment_no;
                            txtAllotmentValidUpto.Text = list.ToList()[0].valid_date;
                            txtUnitName.Text = list.ToList()[0].party_name;
                            txtMaterialType.Text = list.ToList()[0].product_name;
                            txtAllotmentQty.Text = list.ToList()[0].allocation_qty.ToString();
                            txtApprovedQTY.Text = list.ToList()[0].rr_approved_qty.ToString();
                            fromparty.Value = list.ToList()[0].from_party;
                            txtRemarks.Text = list.ToList()[0].remarks;

                            if (list.ToList()[0].rr_balance_qty.ToString() == "0")
                                txtBalanceQTY.Text = (list.ToList()[0].allocation_qty- list.ToList()[0].rr_quantity).ToString(); /*list.ToList()[0].allocation_qty.ToString();*/
                            else
                                txtBalanceQTY.Text = list.ToList()[0].rr_balance_qty.ToString();
                            txtReleaseRequestQTY.Text = list.ToList()[0].rr_quantity.ToString();
                            List<Release_Request> rr1 = new List<Release_Request>();
                            rr1 = BL_Release_Request.GetRRList();
                            var list1 = from s in rr1
                                        where s.rr_allotmentno == list.ToList()[0].rr_allotmentno && s.party_code == user.party_code
                                        select s;

                            grdReleaseRequest.DataSource = list1.ToList();
                            grdReleaseRequest.DataBind();
                          
                        }
                        else
                        {
                            approverremaks.Visible = false;
                            // approver.Visible = false;
                            btnApprove.Visible = false;
                            btnReject.Visible = false;
                            partycode.Value = user.party_code;
                            string rrno = Session["request_id"].ToString() + "_" + Session["party_code"];

                            Release_Request rr1 = new Release_Request();
                            rr1 = BL_Release_Request.GetDetails(rrno, Session["rrfinancial_year"].ToString());
                            rrid.Value = rr1.release_request_id;
                            txtReleaseRequestNo.Text = rr1.rr_reqno;
                            //    RRRequestid.Value = rr1.rr_reqno;


                            txtReleaseRequestDate.Text = rr1.rr_date;
                            txtFiscalYear.Text = rr1.financial_year;
                            txtMolassesFinalAllotmentNo.Text = rr1.final_allotment_no;
                            txtAllotmentValidUpto.Text = rr1.allotment_date;
                            txtUnitName.Text = rr1.party_name;
                            txtMaterialType.Text = rr1.product_name;
                            txtAllotmentQty.Text = rr1.allocation_qty.ToString();
                            txtApprovedQTY.Text = rr1.rr_approved_qty.ToString();
                            fromparty.Value = rr1.from_party;
                            txtBalanceQTY.Text = rr1.rr_balance_qty.ToString();
                            txtReleaseRequestQTY.Text = rr1.rrqty.ToString();
                            txtNewRequestedQty.Text = rr1.rr_quantity.ToString();
                            hidrqty.Value= rr1.rr_quantity.ToString();
                            productcode.Value = rr1.product_code;
                            txtRemarks.Text = rr1.remarks;
                            txtValiedDate.Text = rr1.valid_date;
                            txtvalieddate1.Value = rr1.valid_date;
                           
                            List<Release_Request> rr2 = new List<Release_Request>();
                            rr2 = BL_Release_Request.GetRRList();
                            var list1 = from s in rr2
                                        where s.rr_allotmentno == rr1.rr_allotmentno && s.party_code == rr1.party_code
                                        select s;

                            grdReleaseRequest.DataSource = list1.ToList();
                            grdReleaseRequest.DataBind();
                            if (rr1.doc != null)
                            {
                                for (int i = 0; i < rr1.doc.Count; i++)
                                {
                                    if (i == 0)
                                        dummytable.Visible = false;
                                    dt = (DataTable)ViewState["Records"];
                                    dt.Rows.Add(rr1.doc[i].doc_name, rr1.doc[i].description, rr1.doc[i].doc_path, rr1.doc[i].id);
                                    grdAdd.DataSource = dt;
                                    grdAdd.DataBind();
                                }
                            }
                            List<All_Approvals> approvals = new List<All_Approvals>();
                            approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), rr1.release_request_id.ToString(), "RRL");
                            if (Session["UserID"].ToString() == "Admin")
                            {
                                if (rr1.valid_date != null)
                                    CalendarExtender1.SelectedDate = Convert.ToDateTime(rr1.valid_date);
                                CalendarExtender1.StartDate = DateTime.Now;
                                btnCancel.Visible = true;
                                btnSaveasDraft.Visible = false;
                                btnSubmit.Visible = false;
                                docs.Visible = false;
                                txtReleaseRequestNo.ReadOnly = true;
                                txtReleaseRequestDate.ReadOnly = true;
                                txtFiscalYear.ReadOnly = true;
                                txtMolassesFinalAllotmentNo.ReadOnly = true;
                                txtAllotmentValidUpto.ReadOnly = true;
                                txtUnitName.ReadOnly = true;
                                txtMaterialType.ReadOnly = true;
                                txtAllotmentQty.ReadOnly = true;
                                txtApprovedQTY.ReadOnly = true;
                                txtBalanceQTY.ReadOnly = true;
                                txtReleaseRequestQTY.ReadOnly = true;
                                valieddate12.Visible = true;
                                docs.Visible = false;
                                txtNewRequestedQty.ReadOnly = false;
                                txtRemarks.ReadOnly = true;
                                btnupdate.Visible = true;
                                foreach (GridViewRow dr1 in grdAdd.Rows)
                                {
                                    ImageButton btn = dr1.FindControl("ImageButton1") as ImageButton;
                                    btn.Visible = false;
                                }
                                var list4 = (from s in approvals
                                             where s.user_id == "Admin"
                                             select s);
                                grdApprovalDetails.DataSource = list4.ToList();
                                approver.Visible = true;
                                grdApprovalDetails.DataBind();
                            }
                            else
                            {
                                if (rr1.valid_date != null)
                                    CalendarExtender1.SelectedDate = Convert.ToDateTime(rr1.valid_date);
                                CalendarExtender1.StartDate = DateTime.Now;
                                CalendarExtender1.EndDate = Convert.ToDateTime(rr1.allotment_date);
                                var list4 = (from s in approvals
                                             where s.financial_year == Session["rrfinancial_year"].ToString()
                                             select s);
                                grdApprovalDetails.DataSource = list4.ToList();
                                grdApprovalDetails.DataBind();
                            }
                            
                            if (Session["rtype"].ToString() == "1")
                            {
                                txtReleaseRequestNo.ReadOnly = true;
                                txtReleaseRequestDate.ReadOnly = true;
                                txtFiscalYear.ReadOnly = true;
                                txtMolassesFinalAllotmentNo.ReadOnly = true;
                                txtAllotmentValidUpto.ReadOnly = true;
                                txtUnitName.ReadOnly = true;
                                txtMaterialType.ReadOnly = true;
                                txtAllotmentQty.ReadOnly = true;
                                txtApprovedQTY.ReadOnly = true;
                                txtBalanceQTY.ReadOnly = true;
                                txtReleaseRequestQTY.ReadOnly = true;
                                docs.Visible = false;
                                txtNewRequestedQty.ReadOnly = true;
                                txtRemarks.ReadOnly = true;
                                foreach (GridViewRow dr1 in grdAdd.Rows)
                                {
                                    ImageButton btn = dr1.FindControl("ImageButton1") as ImageButton;
                                    btn.Visible = false;
                                }
                                btnCancel.Visible = false;
                                btnSaveasDraft.Visible = false;
                                btnSubmit.Visible = false;

                                if (approvals.Count <= 0)
                                {
                                    approver.Visible = false;
                                    approverremaks.Visible = false;
                                }
                                if (user.role_name.Trim() == "Assistant Commissioner" && rr1.record_status != "N" && rr1.record_status != "R")
                                {
                                    approverremaks.Visible = true;
                                    approver.Visible = true;
                                    btnApprove.Visible = true;
                                    btnReject.Visible = true;
                                    valieddate12.Visible = true;
                                    Image2.Visible = true;

                                }
                                if (rr1.record_status == "A" || rr1.record_status == "I")
                                {
                                    approverremaks.Visible = false;
                                    // approver.Visible = false;
                                    btnApprove.Visible = false;
                                    btnReject.Visible = false;

                                    valieddate12.Visible = true;
                                    Image2.Visible = false;
                                }
                                if (user.role_name.Trim() == "Assistant Commissioner")
                                {
                                    btnIssuedReleaseRequestLetter.Visible = true;
                                    btnReleaseRequestMolasses.Visible = false;
                                    txtNewRequestedQty.ReadOnly = true;
                                    Response.Cookies["btnIssuedReleaseRequestLetter-cssClass"].Value = "active";
                                }

                            }

                        }
                    }
                }
                if (strPreviousPage == "" || System.Web.HttpContext.Current.Session["UserID"] == null)
                {
                    Response.Redirect("~/LoginPage");
                }
            }
        }
        int Doc_id = 1;
        DataTable dt = new DataTable();
        protected void UploadFile(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (idupDocument.HasFile)
                {

                    dummytable.Visible = false;
                    string fileName = Path.GetFileName(idupDocument.PostedFile.FileName);
                    FileInfo fi = new FileInfo(fileName);
                    string[] filetype = fileName.Replace("'", "").Split('.');
                    string m = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-sss");
                    idupDocument.PostedFile.SaveAs(Server.MapPath("~/Eascm_Docs/") + fi.Name.Replace(fi.Extension, "").Replace(",", "_").Replace(" ", "_") + "_" + m + fi.Extension);
                    string path = Server.MapPath("~/Eascm_Docs/") + fi.Name.Replace(fi.Extension, "") + "_" + m + fi.Extension;
                    dt = (DataTable)ViewState["Records"];
                    dt.Rows.Add(fileName, txtDiscription.Text, path, Doc_id);
                    grdAdd.DataSource = dt;
                    grdAdd.DataBind();
                    Doc_id++;
                    txtDiscription.Text = "";


                }
            }

        }
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ImageButton lb = (ImageButton)sender;
                GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
                string filePath = (sender as ImageButton).CommandArgument;
                //File.Delete(Server.MapPath("~/Eascm_Docs/" + Path.GetFileName(filePath)));
                //FileInfo fInfoEvent;
                //fInfoEvent = new FileInfo(Path.GetFileName(filePath));
                //fInfoEvent.Delete();
                string a = Session["rtype"].ToString();
                string v = Path.GetFileName(filePath).ToString();
                if (Session["rtype"].ToString() != "0")
                {
                    bool value = BL_User_Mgnt.DeletefileEascm("eascm_docs", Session["request_id"].ToString(), v, partycode.Value);
                    if (value)
                    {
                        File.Delete(Server.MapPath("~/Eascm_Docs/" + Path.GetFileName(filePath)));
                        FileInfo fInfoEvent;
                        fInfoEvent = new FileInfo(Path.GetFileName(filePath));
                        fInfoEvent.Delete();
                    }
                }
                else
                {
                    File.Delete(Server.MapPath("~/Eascm_Docs/" + Path.GetFileName(filePath)));
                    FileInfo fInfoEvent;
                    fInfoEvent = new FileInfo(Path.GetFileName(filePath));
                    fInfoEvent.Delete();

                }
                DataTable dt2 = (DataTable)ViewState["Records"];
                ViewState["CurrentTable"] = dt2;
                int rowID = gvRow.RowIndex;
                DataTable dt1 = ViewState["Records"] as DataTable;
                dt1.Rows[rowID].Delete();
                ViewState["dt"] = dt1;
                grdAdd.DataSource = dt1;
                grdAdd.DataBind();
                if (dt1.Rows.Count < 1)
                    dummytable.Visible = true;
            }
        }
        protected void DownloadFile(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string filePath = (sender as ImageButton).CommandArgument;
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath.Replace(",", "_").Replace(" ", "_")));
                if (File.Exists(Server.MapPath("~/Eascm_Docs/" + Path.GetFileName(filePath.Replace(",", "_").Replace(" ", "_")))))
                    Response.WriteFile(Server.MapPath("~/Eascm_Docs/" + Path.GetFileName(filePath.Replace(",", "_").Replace(" ", "_"))));
                else
                    Response.WriteFile(Server.MapPath("~/Eascm_Docs/" + Path.GetFileName(filePath)));
                Response.End();
            }
        }
        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            if (Session["rtype"].ToString() == "0")
            {
                Session["UserID"] = Session["UserID"];
                Response.Redirect("ReleaseRequestList");
            }
            else
            {
                Session["UserID"] = Session["UserID"];
                Response.Redirect("ReleaseRequestAppliedList");
            }
        }
        protected void btnReleaseRequestMolasses_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ReleaseRequestList");
        }

        protected void btnIssuedReleaseRequestLetter_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ReleaseRequestAppliedList");
        }

        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                btnSaveasDraft.Enabled = false;
                Release_Request rr = new Release_Request();
              
                rr.rr_date= txtReleaseRequestDate.Text;
                rr.financial_year= txtFiscalYear.Text;
                rr.rr_allotmentno=txtMolassesFinalAllotmentNo.Text;
                rr.valid_date=  txtAllotmentValidUpto.Text;
                rr.party_code =partycode.Value;
                
                rr.product_code= productcode.Value;
               
                rr.allocation_qty = Convert.ToDouble(txtAllotmentQty.Text);// = list.ToList()[0].allocation_qty.ToString();
                rr.rr_approved_qty = Convert.ToDouble(txtApprovedQTY.Text);//= list.ToList()[0].rr_approved_qty.ToString();
                rr.rr_balance_qty = Convert.ToDouble(txtBalanceQTY.Text);
                rr.rr_quantity = Convert.ToDouble(txtNewRequestedQty.Text);
                rr.rr_reqno = txtReleaseRequestNo.Text;
                rr.rr_date = txtReleaseRequestDate.Text;
                rr.party_code = partycode.Value;
                rr.from_party = fromparty.Value;
                rr.record_status = "N";
                rr.remarks = txtRemarks.Text;
                rr.user_id = Session["UserID"].ToString();
                rr.doc = new List<EASCM_DOCS>();
                dt = ViewState["Records"] as DataTable;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    EASCM_DOCS doc = new EASCM_DOCS();
                    doc.doc_name = dr["Doc_Name"].ToString();
                    doc.doc_path = (grdAdd.Rows[i].FindControl("lblFilePath") as Label).Text;
                    doc.description = (grdAdd.Rows[i].FindControl("lblDiscriptione") as Label).Text;
                    rr.doc.Add(doc);
                    i++;
                }
                string val = "";
                if (Session["rtype"].ToString()== "0")
                    val = BL_Release_Request.Insert(rr);
                else
                {
                    rr.release_request_id = Session["request_id"].ToString();
                    val = BL_Release_Request.Update(rr);
                }
                if (val == "0")
                {
                    if (Session["rtype"].ToString() == "0")
                        Response.Redirect("ReleaseRequestList");
                    else
                        Response.Redirect("ReleaseRequestAppliedList");
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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Release_Request rr = new Release_Request();
                rr.rr_date = txtReleaseRequestDate.Text;
                rr.financial_year = txtFiscalYear.Text;
                rr.rr_allotmentno = txtMolassesFinalAllotmentNo.Text;
                rr.valid_date = txtAllotmentValidUpto.Text;
                rr.party_code = partycode.Value;
                rr.product_code = productcode.Value;
                rr.allocation_qty = Convert.ToDouble(txtAllotmentQty.Text);// = list.ToList()[0].allocation_qty.ToString();
                rr.rr_approved_qty = Convert.ToDouble(txtApprovedQTY.Text);//= list.ToList()[0].rr_approved_qty.ToString();
                rr.rr_balance_qty = Convert.ToDouble(txtBalanceQTY.Text);
                rr.rr_quantity = Convert.ToDouble(txtNewRequestedQty.Text);
                rr.rr_reqno = txtReleaseRequestNo.Text;
                rr.rr_date = txtReleaseRequestDate.Text;
                rr.remarks = txtRemarks.Text;
                rr.from_party = fromparty.Value;
                rr.record_status = "Y";
                rr.user_id = Session["UserID"].ToString();
                rr.doc = new List<EASCM_DOCS>();
                dt = ViewState["Records"] as DataTable;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    EASCM_DOCS doc = new EASCM_DOCS();
                    doc.doc_name = dr["Doc_Name"].ToString();
                    doc.doc_path = (grdAdd.Rows[i].FindControl("lblFilePath") as Label).Text;
                    doc.description = (grdAdd.Rows[i].FindControl("lblDiscriptione") as Label).Text;
                    rr.doc.Add(doc);
                    i++;
                }
                string val = "";
                if (Session["rtype"].ToString() == "0")
                    val = BL_Release_Request.Insert(rr);
                else
                {
                    rr.release_request_id = Session["request_id"].ToString();
                    val = BL_Release_Request.Update(rr);
                }
                if (val == "0")
                {
                    
                    Session["UserID"] = Session["UserID"];
                    if (Session["rtype"].ToString() == "0")
                        Response.Redirect("ReleaseRequestList");
                    else
                        Response.Redirect("ReleaseRequestAppliedList");
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
            if (Session["rtype"].ToString() == "0")
                Response.Redirect("ReleaseRequestList");
            else
                Response.Redirect("ReleaseRequestAppliedList");
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Release_Request rr = new Release_Request();
                string val;
                rr.record_status = "A";
                rr.release_request_id = Session["request_id"].ToString();
                rr.approval_level =Session["level"].ToString();
                rr.approval_status = "Approved by "+Session["rolename"];
                rr.remarks = txtApproverremarks.Value;
                rr.financial_year = txtFiscalYear.Text;
                rr.user_id = Session["UserID"].ToString();
                rr.rr_approved_qty = Convert.ToDouble(txtNewRequestedQty.Text);
                rr.rr_balance_qty= Convert.ToDouble(txtBalanceQTY.Text);
                rr.valid_date = txtvalieddate1.Value;
                rr.rr_balance_qty = rr.rr_balance_qty - rr.rr_approved_qty;
                int value = BL_Molasses_Allocation.GetDigitalsignature(Session["UserID"].ToString());
                if (value == 1)
                {
                    Session["balance_qty"] = rr.rr_balance_qty;
                    Session["valid_date"] = rr.valid_date;
                    Session["remarks"] = rr.remarks;
                    Session["approval_status"] = rr.approval_status;
                    Response.Redirect("HtmlPage3.html");
                }
                else
                { 
                    val = BL_Release_Request.Approve(rr);
                    if (val == "0")
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("ReleaseRequestAppliedList");
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

        protected void btnReject_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Release_Request rr = new Release_Request();
                string val;
                rr.record_status = "R";
                rr.release_request_id = Session["request_id"].ToString();
                rr.approval_level =Session["level"].ToString();
                rr.financial_year = txtFiscalYear.Text;
                rr.approval_status = "Rejected by " + Session["rolename"];
                rr.remarks = txtApproverremarks.Value;
                rr.user_id = Session["UserID"].ToString();
                rr.rr_approved_qty = Convert.ToDouble(txtNewRequestedQty.Text);
                rr.rr_balance_qty = Convert.ToDouble(txtBalanceQTY.Text);
                rr.rr_balance_qty = rr.rr_balance_qty; //- rr.rr_approved_qty;
                val = BL_Release_Request.Approve(rr);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("ReleaseRequestAppliedList");
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

        protected void PassRequest_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RequestForPassList");
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Release_Request rr = new Release_Request();
                string val;
                if(hidrqty.Value != txtNewRequestedQty.Text)
                {
                    rr.record_status = "New Request Qty Updated By Admin";
                }
                else if(txtvalieddate1.Value !=txtValiedDate.Text)
                {
                    rr.record_status = "Valid Date Upto Updated By Admin(From " + txtValiedDate.Text + " To " + txtvalieddate1.Value + ")";
                }
                else
                {
                    rr.record_status = " Updated By Admin";
                }
                rr.financial_year = Session["rrfinancial_year"].ToString();
                rr.release_request_id = Session["request_id"].ToString();
                rr.user_id = Session["UserID"].ToString();
                rr.rr_approved_qty = Convert.ToDouble(txtNewRequestedQty.Text);
                rr.valid_date = txtvalieddate1.Value;
                    val = BL_Release_Request.Adminupdate(rr);
                    if (val == "0")
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("ReleaseRequestAppliedList");
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