using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class LicenseApplicationForm : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        List<WorkFlow> workflow = new List<WorkFlow>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CalendarExtender.EndDate = DateTime.Now;
                CalendarExtender4.EndDate= DateTime.Now;
                CalendarExtender5.StartDate= DateTime.Now;
                //approverremarks.Visible = false;
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

                    Session["UserID"] = Session["UserID"];
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        Session["party_code"] = user.party_code;
                        Session["rolename"] = user.role_name;
                        Session["year"] = user.financial_year;
                        List<LicenseType> tab = new List<LicenseType>();
                        tab = BL_LicenseType.GetList("");
                        var list1 = from s in tab
                                    select s;
                        ddlLicense.DataSource = list1.ToList();
                        ddlLicense.DataTextField = "lic_type_name";
                        ddlLicense.DataValueField = "lic_type_code";
                        ddlLicense.DataBind();
                        ddlLicense.Items.Insert(0, "Select");
                        List<LicenseFee> tab1 = new List<LicenseFee>();
                        tab1 = BL_LicenseFee.GetList("");
                        var list2 = from s in tab1
                                    select s;
                        ddlfee.DataSource = list2.ToList();
                        ddlfee.DataTextField = "lic_fee_code";
                        ddlfee.DataValueField = "lic_fee_code";
                        ddlfee.DataBind();
                        ddlfee.Items.Insert(0, "Select");
                        List<State> statelist = new List<State>();
                        statelist = BL_User_Mgnt.GetListValues("");
                        var list3 = from s in statelist
                                    where s.state_Code=="BH"
                                    select s;
                        ddStates.DataSource = list3.ToList();
                        ddStates.DataTextField = "State_Name";
                        ddStates.DataValueField = "State_Code";
                        ddStates.DataBind();
                        ddStates.Items.Insert(0, "Select");
                        List<cm_idproof> statelist1 = new List<cm_idproof>();
                        statelist1 = BL_cm_idproof.GetList();
                        ddlidproof.DataSource = statelist1;
                        ddlidproof.DataTextField = "idproof_name";
                        ddlidproof.DataValueField = "idproof_code";
                        ddlidproof.DataBind();
                        ddlidproof.Items.Insert(0, "Select");
                        approverremarks.Visible = false;
                        btnApprove.Visible = false;
                        btnhodapprover.Visible = false;
                        btnReject.Visible = false;
                        btnDownload.Visible = false;
                        btnDownload1.Visible = false;
                        btnReferBack.Visible = false;
                        btnIssue.Visible = false;
                        Image2.Visible = false;
                        Image3.Visible = false;
                        workflow = new List<WorkFlow>();
                        workflow = BL_WorkFlow.ApprovelLevels("LIC", "196");
                        var aplevel = (from s in workflow
                                       where s.user_registration_id == user.id.ToString()
                                       select s);
                        if (aplevel.ToList().Count > 0)
                        {
                            currentlevel.Value = aplevel.ToList()[0].approver_level;
                        }
                        // dummytable.Visible = true;
                        if (Session["rolename"].ToString().Trim() == "Deputy Commissioner" || Session["rolename"].ToString().Trim() == "Commissioner" || currentlevel.Value == "3" || currentlevel.Value == "2")
                        {
                            start.Visible = true;
                            CalendarExtender4.Enabled =true;
                            CalendarExtender5.Enabled = true;
                            Image2.Visible =true;
                            Image3.Visible = true;
                        }
                        else
                        {
                            start.Visible = false;
                            CalendarExtender4.Enabled = false;
                            CalendarExtender5.Enabled = false;
                            
                        }
                        
                        if (Session["rtype"].ToString() != "0")
                        {
                            btnApprove.Visible = false;
                            btnhodapprover.Visible = false;
                            btnReject.Visible = false;
                            btnReferBack.Visible = false;
                            btnIssue.Visible = false;
                            approverid.Visible = false;
                            approverremarks.Visible = false;
                            LicenseApplication frm = new LicenseApplication();
                            //    string frm84id = Session["ReceivertoStorageId"].ToString();
                            //    //  string from_receivervat = Session["from_receivervat"].ToString();
                            //    string Party_Code = Session["Party_Code"].ToString();
                            frm = BL_LicenseApplication.GetDetails(Convert.ToInt32(Session["licenseId"].ToString()), Session["Lfinancial_year"].ToString());
                            //    CalendarExtender.SelectedDate = Convert.ToDateTime(frm84.receipt_date);
                            txttrdate.Value = frm.dob;
                            txtAddress.Text = frm.address;
                            txtApplicantname.Text = frm.applicant_name;
                            txtApplicationDate.Text = frm.dob;
                            txtemail.Text = frm.email;
                            txtRemarks.Text = frm.remarks;
                            txtGST.Text = frm.gst;
                            txtMobile.Text = frm.mobile.ToString();
                            txtPan.Text = frm.pan;
                            txtunitname.Text = frm.father_unit_name;
                            btnDownloadMf1Attachment.Text = frm.photo_image;
                            Hdphoto.Value= frm.photo_image;
                            txtphotoname.Text = frm.photoname;
                            btnDownloadMf1Attachment1.Text = frm.idproof_image;
                            Hdidproof.Value= frm.idproof_image;
                            txtPin.Text = frm.pin.ToString();
                            txtTan.Text = frm.tan;
                            txtTin.Text = frm.tin;
                            txttaluk.Text = frm.taluk_town;
                            Hdrenewal.Value = frm.renewed;
                            ddlfee.SelectedValue = frm.lic_fee_code;
                            ddlfee_SelectedIndexChanged(sender, e);
                            ddlLicense.SelectedValue = frm.lic_type_code;
                            txtdate.Value = frm.start_date;
                            txtReceiptDate.Text= frm.start_date;
                            txtdor.Value = frm.end_date;
                            txtDateofRemoval.Text= frm.end_date;
                           
                            //  ddlLicense_SelectedIndexChanged(sender,e);
                            //for (int j = 0; j < frm.applied.Count-1; j++)
                            //{
                            //    List<LicenseSubType> tab5 = new List<LicenseSubType>();
                            //tab5 = BL_LicenseSubType.GetList("");
                            //    var list5 = from s in tab5
                            //                where s.lic_type_code == ddlLicense.SelectedValue
                            //                select s;
                            //    Chsub.DataSource = list5.ToList();
                            //    Chsub.DataTextField = "lic_subtype_name";
                            //    Chsub.DataValueField = "lic_subtype_code";
                            //    //    Chsub.SelectedValue = frm.applied[j].lic_subtype_code;
                            //    //    Chsub.DataBind();
                            //    Chsub.SelectedValue = frm.applied[j].lic_subtype_code;

                            //   Chsub.SelectedIndex =j;
                            //        break;


                            //}
                            ddStates.SelectedValue = frm.state_code;
                            ddStates_SelectedIndexChanged(sender, e);
                            ddDivisions.SelectedValue = frm.division_code;
                            ddDivisions_SelectedIndexChanged(sender, e);
                            ddDistricts.SelectedValue = frm.district_code;
                            ddDistricts_SelectedIndexChanged(sender, e);
                            ddlthana.SelectedValue = frm.thana_code;
                            ddlidproof.SelectedValue = frm.idproof_code;
                            dummytable.Visible = false;
                            if(frm.start_date!="" && frm.end_date !="")
                            {
                                txtReceiptDate.Text = frm.start_date;
                                txtDateofRemoval.Text = frm.end_date;
                                txtdate.Value = frm.start_date;
                                txtdor.Value = frm.end_date;
                            }
                         
                            // EnaTable.Visible = false;
                            //dt = (DataTable)ViewState["Records"];
                            //dt.Rows.Add(frm.photoname, frm.idproof_code, frm.idproof_name, frm.idproof_image, frm.lic_application_id);
                            //grdAdd.DataSource = dt;
                            //grdAdd.DataBind();

                            //Doc_id++;
                            if (frm.doc != null)
                            {
                                for (int i = 0; i < frm.doc.Count; i++)
                                {
                                    if (i == 0)
                                        dummytable.Visible = false;
                                    dt = (DataTable)ViewState["Records"];
                                    dt.Rows.Add(frm.doc[i].doc_name, frm.doc[i].description, frm.doc[i].doc_path, frm.doc[i].id);
                                    grdAdd.DataSource = dt;
                                    grdAdd.DataBind();
                                }
                            }
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                              if(frm.record_status!="N")
                                {
                                    (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                                    (grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = true;
                                }
                              else
                                {
                                    (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible =true;
                                    (grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = true;
                                }
                                    
                              
                            }
                            // idproofimage1.PostedFile.FileName.Insert(0, frm.idproof_image);
                            List<aplliedfor> frm1 = new List<aplliedfor>();
                            frm1 = BL_LicenseApplication.getdetail(Convert.ToInt32(Session["licenseId"].ToString()));
                            var list5 = from s in frm1
                                           select s;
                            Chsub.DataSource = list5.ToList();
                            Chsub.DataTextField = "lic_subtype_name";
                            Chsub.DataValueField = "lic_subtype_code";
                            Chsub.DataBind();
                            foreach (ListItem li in Chsub.Items)
                            {
                                li.Selected = true;
                            }
                            //for (int i = 0; i < frm.applied.Count; i++)
                            //{
                            //    Chsub.SelectedValue = frm.applied[i].lic_subtype_code;
                            //}
                            if ( Session["rolename"].ToString().Trim()== "Deputy Commissioner" || Session["rolename"].ToString().Trim() == "Commissioner" || currentlevel.Value == "3" || currentlevel.Value == "2")
                            {
                                start.Visible = true;
                               
                            }
                           else
                            {
                                if (frm.start_date != "" && frm.end_date != "")
                                {
                                    txtReceiptDate.Text = frm.start_date;
                                    txtDateofRemoval.Text = frm.end_date;
                                    txtdate.Value = frm.start_date;
                                    txtdor.Value = frm.end_date;
                                }
                                else
                                {
                                    start.Visible = false;
                                }
                                    
                            }
                            //FileIDproof.PostedFile.FileName= frm.idproof_image;
                            //    if (frm84.record_status == "Y" || frm84.record_status == "A")
                            //    {
                            //    }
                            //    else
                            //    {
                            //    }
                            //    //List<FReceiverInput> form84 = new List<FReceiverInput>();
                            //    //form84 = BL_ReceiverToStorage_84.GetReceiverVAt(frm84.to_storagevat, frm84.production_date, party_code.Value);
                            //    List<All_Approvals> approvals = new List<All_Approvals>();
                            //    approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), frm84.receiver_storage_receipt_id.ToString(), "RTS");
                            //    //if (frm84.record_status == "Y" || user.role_name == "Bond Officer")
                            //    //{
                            //    //    foreach (GridViewRow dr1 in grdReceiverVat.Rows)
                            //    //    {
                            //    //        ImageButton btn = dr1.FindControl("ImageButton1") as ImageButton;
                            //    //        btn.Visible = false;
                            //    //    }
                            //    //    Image1.Visible = false;
                            //    //}
                            //    txtRemarks.Text = frm84.remarks;
                            List<All_Approvals> approvals = new List<All_Approvals>();
                            approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), frm.lic_application_id.ToString(), "LIC");
                            var list4 = (from s in approvals
                                         where s.financial_year == Session["Lfinancial_year"].ToString()
                                         select s);
                            grdApprovalDetails.DataSource = list4.ToList();
                            grdApprovalDetails.DataBind();
                            if (Session["rtype"].ToString() == "3" || Hdrenewal.Value=="Y")
                            {
                                if (frm.record_status != "I" && frm.record_status != "E" && frm.record_status != "B")
                                {
                                    txtReceiptDate.Text = "";
                                    txtDateofRemoval.Text = "";
                                    txtdate.Value = "";
                                    txtdor.Value = "";
                                }
                                Chsub.Enabled = false;
                                ddlfee.Enabled = false;
                                ddlLicense.Enabled = false;
                                ddlthana.Enabled = false;
                                ddStates.Enabled = false;
                                ddDistricts.Enabled = false;
                                txtGST.ReadOnly = true;
                                txtPin.ReadOnly = true;
                                txtrank.ReadOnly = true;
                                txtTan.ReadOnly = true;
                                txtTin.ReadOnly = true;
                                txtPan.ReadOnly = true;
                                txtApplicantname.ReadOnly = true;
                                txtApplicationDate.ReadOnly = true;
                                txtapplicationno.ReadOnly = true;
                                txtAddress.ReadOnly = true;
                                ddDivisions.Enabled = false;
                                txttaluk.ReadOnly = true;
                                txtunitname.ReadOnly = true;
                               if( Hdphoto.Value !="" && Hdidproof.Value!="")
                                {
                                    btnDownload.Visible = true;
                                    btnDownload1.Visible = true;
                                }

                                txtRemarks.ReadOnly = false;
                                Image1.Visible = false;
                            }
                                if (Session["rtype"].ToString() == "2")
                            {
                                if (frm.record_status == "B" && Session["rolename"].ToString().Trim() == "Deputy Commissioner" || currentlevel.Value == "2")
                                {
                                    txtAadhaar.ReadOnly = true;
                                    txtAddress.ReadOnly = true;
                                    txtApplicantname.ReadOnly = true;
                                    txtApplicationDate.ReadOnly = true;
                                    txtapplicationno.ReadOnly = true;
                                    CalendarExtender.Enabled = false;
                                    CalendarExtender1.Enabled = false;
                                    txtunitname.ReadOnly = true;
                                    txtAddress.ReadOnly = true;
                                    idproofimage.Visible = false;
                                    idproofimage1.Visible = false;
                                    ddlidproof.Enabled = false;
                                    FileIDproof.Enabled = false;
                                    FilePhoto.Enabled = false;
                                    btnDownload.Visible = true;
                                    btnDownload1.Visible = true;
                                    txtphotoname.ReadOnly = true;
                                    txtMobile.ReadOnly = true;
                                    Chsub.Enabled = false;
                                    ddlfee.Enabled = false;
                                    ddlLicense.Enabled = false;
                                    ddlthana.Enabled = false;
                                    ddStates.Enabled = false;
                                    ddDistricts.Enabled = false;
                                    btnUpload.Visible = false;
                                    btnUpload1.Visible = false;
                                    docs.Visible = false;
                                    ddDivisions.Enabled = false;
                                    txttaluk.ReadOnly = true;
                                    Button1.Visible = false;
                                    txtGST.ReadOnly = true;
                                    txtPin.ReadOnly = true;
                                    txtrank.ReadOnly = true;
                                    txtTan.ReadOnly = true;
                                    txtTin.ReadOnly = true;
                                    txtPan.ReadOnly = true;
                                    btnUpload.Visible = false;
                                    // txtphotoname.ReadOnly = true;
                                    idproofimage.Enabled = false;
                                    idproofimage1.Enabled = false;
                                    txtMobile.ReadOnly = true;
                                    txtemail.ReadOnly = true;
                                   // btnApprove.Visible = false;
                                //    btnhodapprover.Visible = false;
                                 //   btnReject.Visible = false;
                                  //  approverid.Visible = false;
                                 //   approverremarks.Visible = false;
                                    txtRemarks.ReadOnly=true;
                                    btnSaveasDraft.Visible = false;
                                    btnSubmit.Visible = false;
                                    btnCancel.Visible = false;
                                    Image1.Visible = false;
                                    btnReferBack.Visible = false;
                                    btnIssue.Visible = false;
                                    //Image2.Visible = false;
                                    //grdAdd.Enabled = false;
                                    //grdAdd.Columns[grdAdd.Columns.Count - 1].Visible = false;
                                    Image1.Visible = false;

                                    approverid.Visible = true;
                                    approverid.Visible = true;
                                    approverremarks.Visible = true;
                                    txtApproverremarks.Visible = true;
                                    btnApprove.Visible = false;
                                    btnhodapprover.Visible = true;
                                    btnReferBack.Visible = false;
                                    btnReject.Visible = true;
                                    btnSaveasDraft.Visible = false;
                                }
                            }
                                if (Session["rtype"].ToString() == "1")
                            {
                                txtAadhaar.ReadOnly = true;
                                txtAddress.ReadOnly = true;
                                txtApplicantname.ReadOnly = true;
                                txtApplicationDate.ReadOnly = true;
                                txtapplicationno.ReadOnly = true;
                                CalendarExtender.Enabled = false;
                                CalendarExtender1.Enabled = false;
                                txtunitname.ReadOnly = true;
                                txtAddress.ReadOnly = true;
                                idproofimage.Visible = false;
                                idproofimage1.Visible = false;
                                ddlidproof.Enabled=false;
                                FileIDproof.Enabled = false;
                                FilePhoto.Enabled = false;
                                btnDownload.Visible =true;
                                btnDownload1.Visible = true;
                                txtphotoname.ReadOnly = true;
                                txtMobile.ReadOnly = true;
                                Chsub.Enabled = false;
                                ddlfee.Enabled = false;
                                ddlLicense.Enabled = false;
                                ddlthana.Enabled = false;
                                ddStates.Enabled = false;
                                ddDistricts.Enabled = false;
                                btnUpload.Visible = false;
                                btnUpload1.Visible = false;
                                docs.Visible = false;
                                ddDivisions.Enabled = false;
                                txttaluk.ReadOnly = true;
                                Button1.Visible = false;
                                txtGST.ReadOnly = true;
                                txtPin.ReadOnly = true;
                                txtrank.ReadOnly = true;
                                txtTan.ReadOnly = true;
                                txtTin.ReadOnly = true;
                                txtPan.ReadOnly = true;
                                btnUpload.Visible = false;
                               // txtphotoname.ReadOnly = true;
                                idproofimage.Enabled = false;
                                idproofimage1.Enabled = false;
                                txtMobile.ReadOnly = true;
                                txtemail.ReadOnly = true;
                                btnApprove.Visible = false;
                                btnhodapprover.Visible = false;
                                btnReject.Visible = false;
                                approverid.Visible = false;
                                approverremarks.Visible = false;
                                txtRemarks.ReadOnly=true;
                                btnSaveasDraft.Visible = false;
                                btnSubmit.Visible = false;
                                btnCancel.Visible = false;
                                Image1.Visible = false;
                                btnReferBack.Visible = false;
                                btnIssue.Visible = false;
                                //Image2.Visible = false;
                                //grdAdd.Enabled = false;
                              //  grdAdd.Columns[grdAdd.Columns.Count - 1].Visible = false;
                                Image1.Visible = false;
                                if (Session["rolename"].ToString().Trim()== "Deputy Commissioner" || Session["rolename"].ToString().Trim() == "Commissioner" || currentlevel.Value == "3" || currentlevel.Value == "2")
                                {
                                    start.Visible = true;
                                    if (frm.record_status == "D"|| frm.record_status == "C"|| frm.record_status == "I" || frm.record_status == "B" || frm.record_status == "R")
                                    {
                                        txtReceiptDate.Text = frm.start_date;
                                        txtDateofRemoval.Text = frm.end_date;
                                        txtdate.Value= frm.start_date;
                                        txtdor.Value = frm.end_date;
                                    }
                                       
                                }
                                if (Session["rolename"].ToString().Trim() == "Deputy Commissioner" || currentlevel.Value == "2")
                                {
                                    start.Visible =true;
                                    //CalendarExtender4.Enabled =true;
                                    //CalendarExtender5.Enabled =true;
                                }
                                else
                                {
                                    start.Visible = false;
                                    CalendarExtender4.Enabled = false;
                                    CalendarExtender5.Enabled = false;
                                }
                                if (user.role_name.Trim() == "Assistant Commissioner" && frm.record_status == "Y" || currentlevel.Value == "1")
                                {
                                    btnSaveasDraft.Visible = false;
                                    btnSubmit.Visible = false;
                                    btnCancel.Visible = false;
                                    btnApprove.Visible = true;
                                    btnhodapprover.Visible = false;
                                    docs.Visible = true;
                                    Button1.Visible = true;
                                    grdAdd.Columns[grdAdd.Columns.Count - 1].Visible = true;
                                   // grdAdd.Columns[grdAdd.Columns.Count - 2].Visible =true;
                                    if ( frm.doc.Count ==0)
                                    {
                                        dummytable.Visible =true;
                                    }
                                    btnReject.Visible = true;
                                    approverid.Visible = true;
                                    approverremarks.Visible = true;
                                    txtApproverremarks.Visible = true;
                                }
                                if (frm.record_status == "A" && Session["rolename"].ToString().Trim() == "Deputy Commissioner" || currentlevel.Value == "2")
                                {
                                    approverid.Visible = true;
                                    approverid.Visible = true;
                                    approverremarks.Visible = true;
                                    txtApproverremarks.Visible = true;
                                    btnApprove.Visible =false;
                                    btnhodapprover.Visible =true;
                                    grdAdd.Visible = true;
                                    btnReferBack.Visible =false;
                                    btnReject.Visible = true;
                                    btnSaveasDraft.Visible = false;
                                }
                                if(frm.record_status == "D" && Session["rolename"].ToString().Trim() == "Commissioner" || currentlevel.Value == "3")
                                {
                                    approverid.Visible = true;
                                    approverid.Visible = true;
                                    approverremarks.Visible = true;
                                    txtApproverremarks.Visible = true;
                                    btnApprove.Visible = false;
                                    btnhodapprover.Visible = true;
                                    btnReferBack.Visible = true;
                                }
                                if (frm.record_status == "C" && currentlevel.Value == "2")
                                {
                                    approverid.Visible = true;
                                    approverid.Visible = true;
                                    approverremarks.Visible = true;
                                    txtApproverremarks.Visible = true;
                                    btnApprove.Visible = false;
                                    btnhodapprover.Visible = false;
                                    btnReferBack.Visible =false;
                                    btnIssue.Visible = true;
                                    btnCancel.Visible = true;

                                }
                             
                                if (frm.record_status != "N" && frm.record_status != "Y")
                                {
                                    approverid.Visible = true;
                                }
                                if (frm.record_status == "R")
                                {
                                    approverid.Visible =true;
                                    approverremarks.Visible = false;
                                    txtApproverremarks.Visible =false;
                                    txtApproverremarks.Attributes.Add("disabled", "disabled");
                                    btnApprove.Visible = false;
                                    btnhodapprover.Visible = false;
                                    btnReferBack.Visible = false;
                                    btnIssue.Visible = false;
                                    btnCancel.Visible = false;
                                    btnReject.Visible = false;
                                    docs.Visible = false;

                                }
                            }
                            else
                            {
                                if (Session["rtype"].ToString() == "3")
                                {
                                    if (Hdphoto.Value != "" && Hdidproof.Value != "")
                                    {
                                        btnDownload.Visible = true;
                                        btnDownload1.Visible = true;
                                    }
                                }
                                else
                                {
                                    btnDownload.Visible = false;
                                    btnDownload1.Visible = false;
                                }
                                   
                            }

                        }
                      
                    
                    }

                    else
                    {
                        btnDownload.Visible = false;
                        btnDownload1.Visible = false;
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
        [WebMethod]
        //public static string GetValuesofVAT(Object vatcode)
        //{

        //    string value = BL_ReceiverToStorage_84.GetExistsData(vatcode.ToString(),_party_code);
        //    return value;
        //}
        protected void ddStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Division> divisions = new List<Division>();
            divisions = BL_User_Mgnt.GetDivisions(ddStates.SelectedValue);
            ddDivisions.DataSource = divisions;
            ddDivisions.DataTextField = "Division_Name";
            ddDivisions.DataValueField = "Division_Code";
            ddDivisions.DataBind();
            ddDivisions.Items.Insert(0, "Select");
            List<District> Districts = new List<District>();
            Districts = BL_LicenseApplication.GetDistrictList(ddStates.SelectedValue);
            var org_master1 = from s in Districts
                              where s.state_Code == ddStates.SelectedValue
                              select s;
            ddDistricts.DataSource = org_master1.ToList();
            ddDistricts.DataTextField = "District_Name";
            ddDistricts.DataValueField = "District_Code";
            ddDistricts.DataBind();
            ddDistricts.Items.Insert(0, "Select");
        }

        protected void ddDivisions_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<District> Districts = new List<District>();
            Districts = BL_User_Mgnt.GetDistricts(ddDivisions.SelectedValue);
            var org_master1 = from s in Districts
                              where s.division_Code == ddDivisions.SelectedValue
                              select s;
            ddDistricts.DataSource = org_master1.ToList();
            ddDistricts.DataTextField = "District_Name";
            ddDistricts.DataValueField = "District_Code";
            ddDistricts.DataBind();
            ddDistricts.Items.Insert(0, "Select");
        }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("LicenseApplicationList.aspx");
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


        protected void lnkRMR_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialReceiptList");
        }
        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReceiverTransferList");

        }

        protected void btnReceipts_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReceivertoStorageList");

        }
        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (Hdphoto.Value == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Attach Photo file');", true);
                    FilePhoto.Focus();
                }
              else  if (Hdidproof.Value == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Attach Idproof file');", true);
                    FileIDproof.Focus();
                }
               else if(Chsub.SelectedValue=="")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Select License Type');", true);
                    Chsub.Focus();
                }
                else
                {
                    LicenseApplication lic = new LicenseApplication();
                    lic.lic_fee_code = ddlfee.SelectedValue;
                    lic.lic_type_code = ddlLicense.SelectedValue;
                    lic.applicant_name = txtApplicantname.Text;
                    lic.father_unit_name = txtunitname.Text;
                    lic.dob = txttrdate.Value;
                    lic.party_code = Session["party_code"].ToString();
                    lic.district_code = ddDistricts.SelectedValue;
                    lic.division_code = ddDivisions.SelectedValue;
                    lic.lic_subtype_code = Chsub.SelectedValue;
                    lic.mobile = Convert.ToDouble(txtMobile.Text);
                    lic.state_code = ddStates.SelectedValue;
                    // lic.thana_code = ddlthana.SelectedValue;
                    lic.taluk_town = txttaluk.Text;
                    if (txtAadhaar.Text == "")
                    {
                        lic.aadhaar = 0;
                    }
                    else
                    {
                        lic.aadhaar = Convert.ToInt32(txtAadhaar.Text);
                    }

                    lic.address = txtAddress.Text;
                    lic.advt_ref = txtapplicationno.Text;
                    lic.gst = txtGST.Text;
                    //lic.pin =Convert.ToInt32(txtPin.Text);
                    lic.tan = txtTan.Text;
                    lic.pan = txtPan.Text;
                    lic.tin = txtTin.Text;
                    lic.email = txtemail.Text;
                    //dt = ViewState["Records"] as DataTable;
                    //int j = 0;
                    //foreach (DataRow dr in dt.Rows)
                    //{

                    //     lic.photoname = dr["photoname"].ToString();
                    //    lic.idproof_code = dr["idproof_code"].ToString();
                    //    lic.idproof_image = dr["idproof_image"].ToString();
                    //        // approversummary = approversummary + "{!}" + doc.doc_name + "{!}" + doc.doc_path + "{!}" + doc.description;
                    //    j++;
                    //}
                    lic.idproof_code = ddlidproof.SelectedValue;
                    lic.idproof_image = Upload_Photo1();
                    lic.photoname = txtphotoname.Text;
                    lic.photo_image = Upload_Photo();
                    lic.remarks = txtRemarks.Text;
                    lic.user_id = Session["UserID"].ToString();
                  lic.financial_year=  Session["year"].ToString();
                    lic.doc = new List<EASCM_DOCS>();
                    dt = ViewState["Records"] as DataTable;
                    int j = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        EASCM_DOCS doc = new EASCM_DOCS();
                        doc.doc_name = dr["Doc_Name"].ToString();
                        doc.doc_path = (grdAdd.Rows[j].FindControl("lblFilePath") as Label).Text;
                        doc.description = (grdAdd.Rows[j].FindControl("lblDiscriptione") as Label).Text;
                        lic.doc.Add(doc);
                        j++;
                    }
                    lic.lic_status = "C";
                    lic.applied = new List<aplliedfor>();
                    aplliedfor step = new aplliedfor();
                    for (int i = 0; i < Chsub.Items.Count; i++)
                    {
                        if (Chsub.Items[i].Selected)
                        {
                            step.lic_subtype_code = Chsub.SelectedValue;

                        }

                        lic.applied.Add(step);
                    }

                    lic.record_status = "N";
                    string val;
                    string a = Session["rtype"].ToString();
                    if (Session["rtype"].ToString() == "0"|| Session["rtype"].ToString() == "3")
                    {
                        if(Session["rtype"].ToString() == "3")
                        {
                            lic.lic_application_id = Convert.ToInt32(Session["licenseId"].ToString());
                            lic.renewed = "Y";
                        }
                        else
                        {
                            lic.renewed = "N";
                        }
                        val = BL_LicenseApplication.Insert(lic);
                    }

                    else
                    {
                        lic.lic_application_id = Convert.ToInt32(Session["licenseId"].ToString());
                        val = BL_LicenseApplication.Update(lic);
                    }

                    if (val == "0")
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("LicenseApplicationList.aspx");
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

        protected void btnSubmit_Click1(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if(Hdphoto.Value=="")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Attach Photo file');", true);
                    FilePhoto.Focus();
                }
             else if(Hdidproof.Value=="")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Attach Idproof file');", true);
                    FileIDproof.Focus();
                }
               else if (Chsub.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Select License Type');", true);
                    Chsub.Focus();
                }
                else
                {
                LicenseApplication lic = new LicenseApplication();
                lic.lic_fee_code = ddlfee.SelectedValue;
                lic.lic_type_code = ddlLicense.SelectedValue;
                lic.party_code = Session["party_code"].ToString();
                lic.applicant_name = txtApplicantname.Text;
                lic.father_unit_name = txtunitname.Text;
                lic.dob = txttrdate.Value;
                lic.district_code = ddDistricts.SelectedValue;
                lic.division_code = ddDivisions.SelectedValue;
                lic.lic_subtype_code = Chsub.SelectedValue;
                lic.mobile = Convert.ToDouble(txtMobile.Text);
                lic.state_code = ddStates.SelectedValue;
               // lic.thana_code = ddlthana.SelectedValue;
                lic.taluk_town = txttaluk.Text;
                if (txtAadhaar.Text == "")
                {
                    lic.aadhaar = 0;
                }
                else
                {
                    lic.aadhaar = Convert.ToInt32(txtAadhaar.Text);
                }
                lic.advt_ref = txtapplicationno.Text;
                lic.gst = txtGST.Text;
                if(txtPin.Text!="")
                {
                    lic.pin = Convert.ToInt32(txtPin.Text);
                }
                
                lic.tan = txtTan.Text;
                lic.pan = txtPan.Text;
                lic.tin = txtTin.Text;
                lic.email = txtemail.Text;
                lic.address = txtAddress.Text;
                dt = ViewState["Records"] as DataTable;
                lic.idproof_code = ddlidproof.SelectedValue;
                lic.idproof_image = Upload_Photo1();
                lic.photoname = txtphotoname.Text;
                lic.photo_image = Upload_Photo();
                lic.remarks = txtRemarks.Text;
                lic.user_id = Session["UserID"].ToString();
                    lic.financial_year = Session["year"].ToString();
                    lic.doc = new List<EASCM_DOCS>();
                dt = ViewState["Records"] as DataTable;
                int j = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    EASCM_DOCS doc = new EASCM_DOCS();
                    doc.doc_name = dr["Doc_Name"].ToString();
                    doc.doc_path = (grdAdd.Rows[j].FindControl("lblFilePath") as Label).Text;
                    doc.description = (grdAdd.Rows[j].FindControl("lblDiscriptione") as Label).Text;
                    lic.doc.Add(doc);
                    j++;
                }
                lic.lic_status = "S";
                lic.applied = new List<aplliedfor>();
                aplliedfor step = new aplliedfor();
                for (int i = 0; i < Chsub.Items.Count; i++)
                {
                    if (Chsub.Items[i].Selected)
                    {
                        step.lic_subtype_code = Chsub.SelectedValue;

                    }

                    lic.applied.Add(step);
                }
                lic.record_status = "Y";
                string val;
                string a = Session["rtype"].ToString();
                    if (Session["rtype"].ToString() == "0" || Session["rtype"].ToString() == "3")
                    {
                        if (Session["rtype"].ToString() == "3")
                        {
                            lic.lic_application_id = Convert.ToInt32(Session["licenseId"].ToString());
                            lic.renewed = "Y";
                        }
                        else
                        {
                            lic.renewed = "N";
                        }
                        val = BL_LicenseApplication.Insert(lic);
                }

                else
                {
                    lic.lic_application_id = Convert.ToInt32(Session["licenseId"].ToString());
                    val = BL_LicenseApplication.Update(lic);
                }

                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("LicenseApplicationList.aspx");
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

        protected void btnVATtansfers_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATTransferList");
        }

        protected void lnkOpeningBalance_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("OpeningBalanceList");

        }

        protected void ddlthana_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddDistricts_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ThanaMaster> Districts = new List<ThanaMaster>();
            Districts = BL_Thana.GetThana1(ddDistricts.SelectedValue);
            var org_master1 = from s in Districts
                              where s.district_code == ddDistricts.SelectedValue
                              select s;
            ddlthana.DataSource = org_master1.ToList();
            ddlthana.DataTextField = "thana_name";
            ddlthana.DataValueField = "thana_code";
            ddlthana.DataBind();
            ddlthana.Items.Insert(0, "Select");
        }

        protected void ddStates_SelectedIndexChanged1(object sender, EventArgs e)
        {
            List<Division> divisions = new List<Division>();
            divisions = BL_User_Mgnt.GetDivisions(ddStates.SelectedValue);
            ddDivisions.DataSource = divisions;
            ddDivisions.DataTextField = "Division_Name";
            ddDivisions.DataValueField = "Division_Code";
            ddDivisions.DataBind();
            ddDivisions.Items.Insert(0, "Select");
            //List<District> Districts = new List<District>();
            //Districts = BL_LicenseApplication.GetDistrictList(ddStates.SelectedValue);
            //var org_master1 = from s in Districts
            //                  where s.state_Code == ddStates.SelectedValue
            //                  select s;
            //ddDistricts.DataSource = org_master1.ToList();
            //ddDistricts.DataTextField = "District_Name";
            //ddDistricts.DataValueField = "District_Code";
            //ddDistricts.DataBind();
            //ddDistricts.Items.Insert(0, "Select");
        }

        protected void ddlLicense_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["rtype"].ToString() == "0")
            {
                int value = 0;
                value = BL_LicenseApplication.GetExistsData(Session["year"].ToString(), ddlLicense.SelectedValue, Session["party_code"].ToString());
                if (value == 1)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append("  License can be applied for this type only once per Year ");
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    ddlLicense.ClearSelection();

                }
                else
                {
                    List<LicenseSubType> tab = new List<LicenseSubType>();
                    tab = BL_LicenseSubType.GetList("");
                    var list1 = from s in tab
                                where s.lic_type_code == ddlLicense.SelectedValue
                                select s;
                    Chsub.DataSource = list1.ToList();
                    Chsub.DataTextField = "lic_subtype_name";
                    Chsub.DataValueField = "lic_subtype_code";
                    Chsub.DataBind();
                }
            }
            else
            {
                List<LicenseSubType> tab = new List<LicenseSubType>();
                tab = BL_LicenseSubType.GetList("");
                var list1 = from s in tab
                            where s.lic_type_code == ddlLicense.SelectedValue
                            select s;
                Chsub.DataSource = list1.ToList();
                Chsub.DataTextField = "lic_subtype_name";
                Chsub.DataValueField = "lic_subtype_code";
                Chsub.DataBind();
            }
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                LicenseApplication ferm = new LicenseApplication();
                if (Session["rolename"].ToString().Trim() == "Deputy Commissioner" || currentlevel.Value == "2") 
                {
                    ferm.record_status = "D";
                    ferm.lic_status = "D";
                }
                else if(Session["rolename"].ToString().Trim() == "Commissioner" || currentlevel.Value == "3")
                {
                    ferm.record_status = "C";
                    ferm.lic_status = "C";
                }
                else
                {
                    ferm.record_status = "A";
                    ferm.doc = new List<EASCM_DOCS>();
                    dt = ViewState["Records"] as DataTable;
                    int j = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        EASCM_DOCS doc = new EASCM_DOCS();
                        doc.doc_name = dr["Doc_Name"].ToString();
                        doc.doc_path = (grdAdd.Rows[j].FindControl("lblFilePath") as Label).Text;
                        doc.description = (grdAdd.Rows[j].FindControl("lblDiscriptione") as Label).Text;
                        ferm.doc.Add(doc);
                        j++;
                    }

                }
                ferm.financial_year = Session["Lfinancial_year"].ToString();
                ferm.party_code = Session["Lparty_code"].ToString();
                string val;
                ferm.lic_application_id = Convert.ToInt32(Session["licenseId"].ToString());
                ferm.start_date = txtdate.Value;
                ferm.end_date = txtdor.Value;
                ferm.remarks = txtApproverremarks.Value;
                ferm.user_id = Session["rolename"].ToString();
                ferm.user = Session["UserID"].ToString();
                val = BL_LicenseApplication.Approve(ferm);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("LicenseApplicationList.aspx");
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
                LicenseApplication ferm = new LicenseApplication();
                ferm.record_status = "R";
                string val;
                ferm.lic_application_id = Convert.ToInt32(Session["licenseId"].ToString());
                ferm.remarks = txtApproverremarks.Value;
                ferm.user_id = Session["rolename"].ToString();
                ferm.user = Session["UserID"].ToString();
              ferm.financial_year = Session["Lfinancial_year"].ToString();
                ferm.party_code = Session["Lparty_code"].ToString();
                ferm.doc = new List<EASCM_DOCS>();
                dt = ViewState["Records"] as DataTable;
                int j = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    EASCM_DOCS doc = new EASCM_DOCS();
                    doc.doc_name = dr["Doc_Name"].ToString();
                    doc.doc_path = (grdAdd.Rows[j].FindControl("lblFilePath") as Label).Text;
                    doc.description = (grdAdd.Rows[j].FindControl("lblDiscriptione") as Label).Text;
                    ferm.doc.Add(doc);
                    j++;
                }
                val = BL_LicenseApplication.Approve(ferm);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("LicenseApplicationList.aspx");
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

        private string Upload_Photo()
        {
            StringBuilder sMessage = new StringBuilder();
            string file = "";
            string path = "";
            if (FilePhoto.HasFile)
            {
                if (FilePhoto.PostedFile.FileName.Length > 0)
                {
                    string fileName = Path.GetFileName(FilePhoto.PostedFile.FileName);
                    FileInfo fi = new FileInfo(fileName);
                    string[] filetype = fileName.Replace("'", "").Split('.');

                    //FilePhoto.PostedFile.SaveAs(Server.MapPath("~/photos/") + txtApplicantname.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh_mm_ss") + fi.Extension);
                    //file = "~/photos/" + txtApplicantname.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh_mm_ss") + fi.Extension;
                    string m = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-sss");
                    FilePhoto.PostedFile.SaveAs(Server.MapPath("~/photos/") + fi.Name.Replace(fi.Extension, "").Replace(",", "_").Replace(" ", "_") + "_" + m + fi.Extension);
                    path = Server.MapPath("~/photos/") + fi.Name.Replace(fi.Extension, "") + "_" + m + fi.Extension;
                }

            }
            else
            {
                btnDownloadMf1Attachment.Text = Hdphoto.Value;
                string reportName = btnDownloadMf1Attachment.Text;
                string fileName = reportName;
                FileInfo fi = new FileInfo(fileName);
                string[] filetype = fileName.Replace("'", "").Split('.');
             
                string m = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-sss");
               FilePhoto.PostedFile.SaveAs(Server.MapPath("~/photos/") + fi.Name.Replace(fi.Extension, "").Replace(",", "_").Replace(" ", "_") + "_" + m + fi.Extension);
                 path = Server.MapPath("~/photos/") + fi.Name.Replace(fi.Extension, "") + "_" + m + fi.Extension;
                path= Hdphoto.Value;
                FilePhoto.PostedFile.SaveAs(Server.MapPath("~/photos/") + txtApplicantname.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh_mm_ss") + fi.Extension);
                file = "~/photos/" + txtApplicantname.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh_mm_ss") + fi.Extension;
            }

            return path;
        }
        protected void btnDownloadmf1_Click(object sender, EventArgs e)
        {
            string reportName = btnDownloadMf1Attachment.Text;
            string filePath = reportName;
            //Response.ContentType = ContentType;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            //Response.WriteFile(filePath);
            //Response.End();
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath.Replace(",", "_").Replace(" ", "_")));
            if (File.Exists(Server.MapPath("~/photos/" + Path.GetFileName(filePath.Replace(",", "_").Replace(" ", "_")))))
                Response.WriteFile(Server.MapPath("~/photos/" + Path.GetFileName(filePath.Replace(",", "_").Replace(" ", "_"))));
            else
                Response.WriteFile(Server.MapPath("~/photos/" + Path.GetFileName(filePath)));
            Response.End();
        }


        private string Upload_Photo1()
        {
            StringBuilder sMessage = new StringBuilder();
            string file = "";
            string path = "";
            if (FileIDproof.HasFile)
            {
                if (FileIDproof.PostedFile.FileName.Length > 0)
                {
                    string fileName = Path.GetFileName(FileIDproof.PostedFile.FileName);
                    FileInfo fi = new FileInfo(fileName);
                    string[] filetype = fileName.Replace("'", "").Split('.');

                    //FileIDproof.PostedFile.SaveAs(Server.MapPath("~/photos/") + txtApplicantname.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh_mm_ss") + fi.Extension);
                    //file = "~/photos/" + txtApplicantname.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh_mm_ss") + fi.Extension;
                    string m = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-sss");
                    FileIDproof.PostedFile.SaveAs(Server.MapPath("~/photos/") + fi.Name.Replace(fi.Extension, "").Replace(",", "_").Replace(" ", "_") + "_" + m + fi.Extension);
                    path = Server.MapPath("~/photos/") + fi.Name.Replace(fi.Extension, "") + "_" + m + fi.Extension;
                }

            }
            else
            {
                btnDownloadMf1Attachment1.Text = Hdidproof.Value;
                string reportName = btnDownloadMf1Attachment1.Text;
                string fileName = reportName;
                FileInfo fi = new FileInfo(fileName);
                string[] filetype = fileName.Replace("'", "").Split('.');
                string m = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-sss");
                FileIDproof.PostedFile.SaveAs(Server.MapPath("~/photos/") + fi.Name.Replace(fi.Extension, "").Replace(",", "_").Replace(" ", "_") + "_" + fi.Extension);
                path = Server.MapPath("~/photos/") + fi.Name.Replace(fi.Extension, "") + "_" + m + fi.Extension;
                path = Hdidproof.Value;
                //FileIDproof.PostedFile.SaveAs(Server.MapPath("~/photos/") + txtApplicantname.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh_mm_ss") + fi.Extension);
                //file = "~/photos/" + txtApplicantname.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy_hh_mm_ss") + fi.Extension;
            }
            return path;
        }
        protected void btnDownloadmf2_Click(object sender, EventArgs e)
        {
            string reportName = btnDownloadMf1Attachment1.Text;

            string filePath = reportName;
            //Response.ContentType = ContentType;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            //Response.WriteFile(filePath);

            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath.Replace(",", "_").Replace(" ", "_")));
            if (File.Exists(Server.MapPath("~/photos/" + Path.GetFileName(filePath.Replace(",", "_").Replace(" ", "_")))))
                Response.WriteFile(Server.MapPath("~/photos/" + Path.GetFileName(filePath.Replace(",", "_").Replace(" ", "_"))));
            else
                Response.WriteFile(Server.MapPath("~/photos/" + Path.GetFileName(filePath)));
            Response.End();
        }


        //int Doc_id = 1;
        //protected void UploadFile(object sender, EventArgs e)
        //{
        //    if (IsPostBack)
        //    {
        //        if (idupDocument.HasFile)
        //        {

        //            dummytable.Visible = false;
        //            //EnaTable.Visible = false
        //            string fileName = Path.GetFileName(idupDocument.PostedFile.FileName);
        //            string fileName1 = Path.GetFileName(idupDocument1.PostedFile.FileName);
        //            //fileName.Replace)
        //            //  string[] filetype = fileName.Replace("'","").Split('.');
        //            FileInfo fi = new FileInfo(fileName);
        //            FileInfo fi2 = new FileInfo(fileName1);
        //            string[] filetype = fileName.Replace("'", "").Split('.');
        //            string[] filetype1 = fileName1.Replace("'", "").Split('.');
        //            string m = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-sss");
        //            idupDocument.PostedFile.SaveAs(Server.MapPath("~/photos/") + fi.Name.Replace(fi.Extension, "").Replace(",", "_").Replace(" ", "_") + "_" + m + fi.Extension);
        //            string path = Server.MapPath("~/photos/") + fi.Name.Replace(fi.Extension, "") + "_" + m + fi.Extension;
        //            idupDocument1.PostedFile.SaveAs(Server.MapPath("~/photos/") + fi2.Name.Replace(fi2.Extension, "").Replace(",", "_").Replace(" ", "_") + "_" + m + fi2.Extension);
        //            string path1 = Server.MapPath("~/photos/") + fi2.Name.Replace(fi2.Extension, "") + "_" + m + fi2.Extension;
        //            dt = (DataTable)ViewState["Records"];
        //            dt.Rows.Add( path,ddlidproof.SelectedValue,ddlidproof.SelectedItem.ToString(), path1, Doc_id);
        //            grdAdd.DataSource = dt;
        //            grdAdd.DataBind();
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                //if ((grdAdd.Rows[i].FindControl("lblStatus") as Label).Text == "1")
        //                //{
        //                //    //(grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = false;
        //                //    (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
        //                //}
        //            }
        //            Doc_id++;
        //            ddlidproof.ClearSelection();


        //        }
        //    }

        //}
        //protected void btnRemove_Click(object sender, EventArgs e)
        //{
        //    if (IsPostBack)
        //    {
        //        ImageButton lb = (ImageButton)sender;
        //        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        //        string filePath = (sender as ImageButton).CommandArgument;
        //        File.Delete(Server.MapPath("~/photos/" + Path.GetFileName(filePath)));
        //        FileInfo fInfoEvent;
        //        fInfoEvent = new FileInfo(Path.GetFileName(filePath));
        //        fInfoEvent.Delete();
        //        DataTable dt2 = (DataTable)ViewState["Records"];
        //        ViewState["CurrentTable"] = dt2;
        //        int rowID = gvRow.RowIndex;
        //        DataTable dt1 = ViewState["Records"] as DataTable;
        //        dt1.Rows[rowID].Delete();
        //        ViewState["dt"] = dt1;
        //        grdAdd.DataSource = dt1;
        //        grdAdd.DataBind();
        //        for (int i = 0; i < dt1.Rows.Count; i++)
        //        {
        //            //if ((grdAdd.Rows[i].FindControl("lblStatus") as Label).Text == "1")
        //            //{
        //                //  (grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = false;
        //                //  (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
        //            //}
        //        }
        //        if (dt1.Rows.Count < 1)
        //            dummytable.Visible = true;
        //        //EnaTable.Visible = false;
        //    }
        //}
        //protected void DownloadFile(object sender, EventArgs e)
        //{
        //    if (IsPostBack)
        //    {
        //        string filePath = (sender as ImageButton).CommandArgument;
        //        Response.ContentType = ContentType;
        //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath.Replace(",", "_").Replace(" ", "_")));
        //        if (File.Exists(Server.MapPath("~/photos/" + Path.GetFileName(filePath.Replace(",", "_").Replace(" ", "_")))))
        //            Response.WriteFile(Server.MapPath("~/photos/" + Path.GetFileName(filePath.Replace(",", "_").Replace(" ", "_"))));
        //        else
        //            Response.WriteFile(Server.MapPath("~/photos/" + Path.GetFileName(filePath)));
        //        Response.End();
        //    }
        //}

        int Doc_id = 1;
        
        protected void UploadFile(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (Hdidproof.Value == "")
                {
                    Hdidproof.Value = Path.GetFileName(FileIDproof.PostedFile.FileName);
                    btnDownloadMf1Attachment1.Text = Path.GetFileName(FileIDproof.PostedFile.FileName);
                }
                if (Hdphoto.Value == "")
                {
                    Hdphoto.Value = Path.GetFileName(FilePhoto.PostedFile.FileName);
                    btnDownloadMf1Attachment.Text = Path.GetFileName(FilePhoto.PostedFile.FileName);
                }

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
                    bool value = BL_User_Mgnt.DeletefileEascm("eascm_docs", Session["licenseId"].ToString(), v, Session["Lparty_code"].ToString());
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
        protected void DownloadFile1(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string filePath = (sender as ImageButton).CommandArgument;
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath.Replace(",", "_").Replace(" ", "_")));
                if (File.Exists(Server.MapPath("~/photos/" + Path.GetFileName(filePath.Replace(",", "_").Replace(" ", "_")))))
                    Response.WriteFile(Server.MapPath("~/photos/" + Path.GetFileName(filePath.Replace(",", "_").Replace(" ", "_"))));
                else
                    Response.WriteFile(Server.MapPath("~/photos/" + Path.GetFileName(filePath)));
                Response.End();
            }
        }
        protected void btnReferBack_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                LicenseApplication ferm = new LicenseApplication();

                ferm.record_status = "B";
                string val;
                ferm.lic_application_id = Convert.ToInt32(Session["licenseId"].ToString());
                ferm.start_date = txtdate.Value;
                ferm.end_date = txtdor.Value;
                ferm.remarks = txtApproverremarks.Value;
                ferm.user_id = Session["rolename"].ToString();
                ferm.user = Session["UserID"].ToString();
                ferm.financial_year = Session["Lfinancial_year"].ToString();
                ferm.party_code = Session["Lparty_code"].ToString();
                val = BL_LicenseApplication.Approve(ferm);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("LicenseApplicationList.aspx");
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

        protected void btnIssue_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                LicenseApplication ferm = new LicenseApplication();
            
                    ferm.record_status = "I";
                string val;
                ferm.lic_application_id = Convert.ToInt32(Session["licenseId"].ToString());
                ferm.start_date = txtdate.Value;
                ferm.end_date = txtdor.Value;
                ferm.remarks = txtApproverremarks.Value;
                ferm.user_id = Session["rolename"].ToString();
                ferm.user = Session["UserID"].ToString();
        ferm.financial_year = Session["Lfinancial_year"].ToString();
                ferm.party_code = Session["Lparty_code"].ToString();
                val = BL_LicenseApplication.Approve(ferm);
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("LicenseApplicationList.aspx");
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

        protected void ddlfee_SelectedIndexChanged(object sender, EventArgs e)
        {
            LicenseFee vats = new LicenseFee();
            vats = BL_LicenseApplication.GetAval(ddlfee.SelectedValue);
            if (Session["rtype"].ToString() == "3"|| Hdrenewal.Value=="Y")
            {
                txtfeeamount.Text = Convert.ToString(vats.lic_renewal_fee);
            }
            else
            {
                txtfeeamount.Text = Convert.ToString(vats.lic_fee_amt);
            }
                
        }

        protected void FilePhoto_DataBinding(object sender, EventArgs e)
        {
            btnDownloadMf1Attachment.Text = Hdidproof.Value;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("LicenseApplicationList.aspx");
        }

        protected void txtGST_TextChanged(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                if (Hdidproof.Value=="")
                {
                    Hdidproof.Value = Path.GetFileName(FileIDproof.PostedFile.FileName);
                    btnDownloadMf1Attachment1.Text = Path.GetFileName(FileIDproof.PostedFile.FileName);
                }
                if (Hdphoto.Value == "")
                {
                    Hdphoto.Value = Path.GetFileName(FilePhoto.PostedFile.FileName);
                    btnDownloadMf1Attachment.Text = Path.GetFileName(FilePhoto.PostedFile.FileName);
                }
            }

        }
       // [WebMethod]
        //public static string chkDuplicateUOMCode(Object code)
        //{
        //    Hdphoto.Value = Path.GetFileName(FilePhoto.PostedFile.FileName);
        //}
        //[WebMethod]
        //public static string chkDuplicateUOMCode(Object code)
        //{
        //    Hdidproof.Value = Path.GetFileName(FileIDproof.PostedFile.FileName);
        //}
    }
}