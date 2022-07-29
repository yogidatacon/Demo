using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class ComplaintForm : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        List<District> Districts = new List<District>();
        List<ThanaMaster> Thana = new List<ThanaMaster>();

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                if (ViewState["Records"] == null)
                {
                    dt.Columns.Add("Status");
                    dt.Columns.Add("Doc_Name");
                    dt.Columns.Add("Discription");
                    dt.Columns.Add("Doc_Path");
                    dt.Columns.Add("Doc_id");
                    ViewState["Records"] = dt;
                }
                List<State> statelist = new List<State>();
                statelist = BL_User_Mgnt.GetListValues(Session["UserID"].ToString());
                ddlState.DataSource = statelist;
                ddlState.DataTextField = "State_Name";
                ddlState.DataValueField = "State_Code";
                ddlState.DataBind();
                ddlState.Items.Insert(0, "Select");
                if (Session["rtype"] != null)
                {
                    if (Session["rtype"].ToString() != "0")
                    {
                        Complaint cmp = new Complaint();
                        cmp = BL_Complaint.GetDetails(Convert.ToInt32(Session["Complaint_ID"].ToString()));
                        txtcomplainantname.Text = cmp.complainant_name;
                        txtmobile.Text = cmp.mobile_no.ToString();
                        txtemail.Text = cmp.email_id;
                        ddlcomplaintype.SelectedValue = cmp.complaint_type;
                        txtAddress.Text = cmp.address;
                        txtcomplaintdetails.Text = cmp.complaint_details;
                        txtthana.Text = cmp.thana;
                        txtdistrict.Text = cmp.district;
                        ddlState.SelectedValue = cmp.state;
                        ddlState_SelectedIndexChanged(sender, e);
                        ddlDistrict.SelectedValue = cmp.district;
                        ddlDistrict_SelectedIndexChanged(sender, e);
                        ddlThana.SelectedValue = cmp.thana;
                        txtvillage.Text = cmp.village;
                        txtlandmark.Text = cmp.landmark;

                        Doc_id = 0;
                        for (int i = 0; i < cmp.docs.Count; i++)
                        {
                            if (i == 0)
                                dummytable.Visible = false;
                            dt = (DataTable)ViewState["Records"];
                            dt.Rows.Add("1", cmp.docs[i].doc_name, cmp.docs[i].description, cmp.docs[i].doc_path, cmp.docs[i].id);
                            grdAdd.DataSource = dt;
                            grdAdd.DataBind();

                            Doc_id++;

                        }
                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    if ((grdAdd.Rows[i].FindControl("lblStatus") as Label).Text == "1")
                        //    {
                        //        //(grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = false;
                        //        (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                        //    }
                        //}

                        if ((Session["rtype"].ToString() == "1"))
                        {
                            foreach (GridViewRow dr1 in grdAdd.Rows)
                            {
                                ImageButton btn = dr1.FindControl("ImageButton1") as ImageButton;
                                btn.Visible = false;
                            }

                            txtcomplainantname.ReadOnly = true;
                            txtmobile.ReadOnly = true;
                            txtemail.ReadOnly = true;
                            ddlcomplaintype.Enabled = false;
                            txtAddress.ReadOnly = true;
                            txtlandmark.ReadOnly = true;
                            txtvillage.ReadOnly = true;
                            ddlThana.Enabled = false;
                            ddlDistrict.Enabled = false;
                            ddlState.Enabled = false;
                            txtcomplaintdetails.ReadOnly = true;
                            btnCancel.Visible = false;
                            btnSaveasDraft.Visible = false;
                            btnSubmit.Visible = false;
                            docs.Visible = false;

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

                    else
                    {
                        Response.Redirect("~/User_Mgmt");
                    }
                }

            }

        }

        static int Doc_id = 1;
        protected void UploadFile(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
                    dt.Rows.Add("", fileName, txtDiscription.Text, path, Doc_id);
                    grdAdd.DataSource = dt;
                    grdAdd.DataBind();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //if ((grdAdd.Rows[i].FindControl("lblStatus") as Label).Text == "1")
                        // {
                        //(grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = false;
                        //    (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                        //}
                    }
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
                // string a = Session["rtype"].ToString();
                string v = Path.GetFileName(filePath).ToString();
                if (Session["rtype"] != null && Session["rtype"].ToString() != "0")
                {
                    bool value = BL_User_Mgnt.DeletefileEascm("eascm_docs", Session["Allotment_ID"].ToString(), v, Session["aparty_code"].ToString());
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
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    if ((grdAdd.Rows[i].FindControl("lblStatus") as Label).Text == "1")
                    {
                        //  (grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = false;
                        (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                    }
                }
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
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ComplaintList.aspx");

        }



        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Complaint cmp = new Complaint();
                cmp.complainant_name = txtcomplainantname.Text;
                cmp.complaint_type = ddlcomplaintype.SelectedValue;
                cmp.mobile_no = Convert.ToInt32(txtmobile.Text);
                cmp.email_id = txtemail.Text;
                cmp.complaint_details = txtcomplaintdetails.Text;
                cmp.address = txtAddress.Text;
                cmp.state = ddlState.SelectedValue;
                cmp.district = ddlDistrict.SelectedValue;
                cmp.thana = ddlThana.SelectedValue;
                cmp.village = txtvillage.Text;
                cmp.landmark = txtlandmark.Text;
                cmp.otp = "0";
                cmp.record_status = "N";

                cmp.docs = new List<EASCM_DOCS>();
                dt = ViewState["Records"] as DataTable;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    EASCM_DOCS doc = new EASCM_DOCS();
                    doc.doc_name = dr["Doc_Name"].ToString();
                    doc.doc_path = dr["Doc_path"].ToString();
                    doc.description = dr["Discription"].ToString();
                    cmp.docs.Add(doc);
                    i++;
                }
                string val;
                if (Session["rtype"].ToString() == "0")
                    val = BL_Complaint.Insert(cmp);
                else
                {
                    cmp.complaint_id = Convert.ToInt32(Session["Complaint_ID"].ToString());
                    val = BL_Complaint.Update(cmp);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("ComplaintList.aspx");
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
                Complaint cmp = new Complaint();
                cmp.complainant_name = txtcomplainantname.Text;
                cmp.complaint_type = ddlcomplaintype.SelectedValue;
                cmp.mobile_no = Convert.ToInt32(txtmobile.Text);
                cmp.email_id = txtemail.Text;
                cmp.complaint_details = txtcomplaintdetails.Text;
                cmp.address = txtAddress.Text;
                cmp.state = ddlState.SelectedValue;
                cmp.district = ddlDistrict.SelectedValue;
                cmp.thana = ddlThana.SelectedValue;
                cmp.village = txtvillage.Text;
                cmp.landmark = txtlandmark.Text;
                cmp.otp = "0";
                cmp.record_status = "N";
                cmp.docs = new List<EASCM_DOCS>();
                dt = ViewState["Records"] as DataTable;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    EASCM_DOCS doc = new EASCM_DOCS();
                    doc.doc_name = dr["Doc_Name"].ToString();
                    doc.doc_path = (grdAdd.Rows[i].FindControl("lblFilePath") as Label).Text;
                    doc.description = (grdAdd.Rows[i].FindControl("lblDiscriptione") as Label).Text;
                    cmp.docs.Add(doc);
                    i++;
                }
                string val;

                if (Session["rtype"].ToString() == "0")
                    val = BL_Complaint.Insert(cmp);
                else
                {
                    cmp.complaint_id = Convert.ToInt32(Session["Complaint_ID"].ToString());
                    val = BL_Complaint.Update(cmp);
                }
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("ComplaintList.aspx");
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
            Response.Redirect("ComplaintList.aspx");
        }




        protected string Generate_otp()
        {
            char[] charArr = "0123456789".ToCharArray();
            string strrandom = string.Empty;
            Random objran = new Random();
            for (int i = 0; i < 4; i++)
            {
                //It will not allow Repetation of Characters
                int pos = objran.Next(1, charArr.Length);
                if (!strrandom.Contains(charArr.GetValue(pos).ToString())) strrandom += charArr.GetValue(pos);
                else i--;
            }
            return strrandom;
        }
        // End OTP Generation function
        // Start Send OTP code on button click
        protected void btnSendOTP_Click(object sender, EventArgs e)
        {
            string otp = Generate_otp();
            string mobileNo = txtmobile.Text.Trim();
            string SMSContents = "", smsResult = "";
            SMSContents = otp + " is your One-Time Password, valid for 10 minutes only, Please do not share your OTP with anyone.";
            smsResult = SendSMS(mobileNo, SMSContents);
            txtmobile.Focus();
            //lblMsg.Text = " OTP is sent to your registered mobile no.";
            //lblMsg.CssClass = "green";
            mobileNo = string.Empty;
            txtmobile.Focus();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "countdown()", true);
        }

        public static string SendSMS(string MblNo, string Msg)
        {
            string MainUrl = "smtpout.asia.secureserver.net"; //Here need to give SMS API URL
            string UserName = "controlroom@prohibitionbihar.in"; //Here need to give username
            string Password = "IEMS@123"; //Here need to give Password
            string SenderId = "controlroom@prohibitionbihar.in";
            string strMobileno = MblNo;
            string URL = "";
            URL = MainUrl + "username=" + UserName + "&msg_token=" + Password + "&sender_id=" + SenderId + "&message=" + HttpUtility.UrlEncode(Msg).Trim() + "&mobile=" + strMobileno.Trim() + "";
            string strResponce = GetResponse(URL);
            string msg = "";
            if (strResponce.Equals("Fail"))
            {
                msg = "Fail";
            }
            else
            {
                msg = strResponce;
            }
            return msg;



            //val = BL_HelpDesk.Insert(Helpdesk);
            //MailMessage msg = new MailMessage();
            //msg.From = new MailAddress("controlroom@prohibitionbihar.in");
            //msg.To.Add("charan.ccsd@gmail.com");
            //msg.Subject = "Issues";
            ////if (idupDocument.HasFile)
            ////{
            ////    string FileName = Path.GetFileName(idupDocument.PostedFile.FileName);
            ////    msg.Attachments.Add(new Attachment(idupDocument.PostedFile.InputStream, FileName));
            ////}
            //msg.Body = "Resolve Time";
            //SmtpClient smt = new SmtpClient();
            //smt.Host = "smtpout.asia.secureserver.net";
            //// smt.Host = "smtp.gmail.com";
            //System.Net.NetworkCredential ntwd = new NetworkCredential();
            //ntwd.UserName = "controlroom@prohibitionbihar.in"; //Your Email ID  
            //ntwd.Password = "IEMS@123"; // Your Password  
            //smt.UseDefaultCredentials = true;
            //smt.Credentials = ntwd;
            //smt.Port = 587;
            //smt.EnableSsl = true;
            //try
            //{
            //    smt.Send(msg);
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Email sent.');", true);
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Email not sent.');", true);
            //}
            ////ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "window.close()", true);
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopup", "window.close();window.opener.location.href=window.opener.location.href;", true);
        }

        public static string GetResponse(string smsURL)
        {
            try
            {
                WebClient objWebClient = new WebClient();
                System.IO.StreamReader reader = new System.IO.StreamReader(objWebClient.OpenRead(smsURL));
                string ResultHTML = reader.ReadToEnd();
                return ResultHTML;
            }
            catch (Exception)
            {
                return "Fail";
            }
        }
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlState.SelectedValue == "BH")
            {
                Districts = new List<District>();
                Districts = BL_User_Mgnt.GetDistricts("");
                var org_master1 = from s in Districts
                                  where s.state_Code == ddlState.SelectedValue
                                  select s;
                ddlDistrict.DataSource = org_master1.ToList();
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Code";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, "Select");
                txtdistrict.Visible = false;
                ddlDistrict.Visible = true;
                txtthana.Visible = false;
                ddlDistrict.Visible = true;
                ddlThana.Visible = true;
            }
            else
            {
                txtthana.Visible = true;
                txtdistrict.Visible = true;
                ddlDistrict.Visible = false;
                ddlThana.Visible = false;
            }
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            Thana = new List<ThanaMaster>();
            Thana = BL_Thana.GetThanaList(ddlDistrict.SelectedValue);
            var org_master1 = from s in Thana
                              where s.district_code == ddlDistrict.SelectedValue
                              select s;
            ddlThana.DataSource = org_master1.ToList();
            ddlThana.DataTextField = "thana_name";
            ddlThana.DataValueField = "thana_code";
            ddlThana.DataBind();
            ddlThana.Items.Insert(0, "Select");
        }

    }
}