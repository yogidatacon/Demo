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
    public partial class BasicIformationForm : System.Web.UI.Page
    {

        DataTable dt = new DataTable();
        int Doc_id = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string a = Session["rtype"].ToString();

                
                Image1.Visible = true;
                CalendarExtender.EndDate = DateTime.Now;
                rdInformationFromControl.Checked = true;
                lblname.Text = "Control Room Complaint No";
                txtDATE.Focus();
                List<Thana_Details> thana = new List<Thana_Details>();
                thana = BL_User_Mgnt.GetThanaList(string.Empty);
                var ad = (from s in thana
                          where s.district_code == Session["district_code"].ToString()
                          select s);
                ddlThana.DataSource = ad.ToArray();
                ddlThana.DataTextField = "thana_name";
                ddlThana.DataValueField = "thana_code";
                ddlThana.DataBind();
                ddlThana.Items.Insert(0, "Select");

                if (ViewState["Records"] == null)
                {
                    dt.Columns.Add("Doc_Name");
                    dt.Columns.Add("Discription");
                    dt.Columns.Add("Doc_Path");
                    dt.Columns.Add("Doc_id");

                    ViewState["Records"] = dt;
                }
                if (Session["rtype"].ToString()=="3")
                {
                    txtRaidLocation.Text = Session["raidlocation"].ToString();
                    rdInformationFromControl.Checked = true;
                    txtName.Text= Session["compid"].ToString();
                    txtName.Enabled = false;
                    txtRaidLocation.Enabled = false;
                    rdInformationFromControl.Enabled = false;
                    rdInformationFromOther.Enabled = false;
                    rdPersonally.Enabled = false;
                    string th = Session["thana_mst_code"].ToString();
                    ddlThana.SelectedValue = th;
                    ddlThana.Enabled = false;
                    Session["rtype"] = "0";
                }
               if (Session["rtype"].ToString() != "0" || Session["seizureNo"].ToString()!="")
                {
                    string seizureNo = Session["seizureNo"].ToString();

                    cm_seiz_BasicIformation obj = new cm_seiz_BasicIformation();
                    obj = BL_cm_seiz_BasicIformation.ViewDetails(seizureNo + "&" + Session["RaidBy"]+"&"+ Session["UserID"]);
                    Image1.Visible = true;
                    txtDATE.Text =Convert.ToDateTime(obj.raid_date).ToString("dd-MM-yyyy");
                    txtdob.Value= Convert.ToDateTime(obj.raid_date).ToString("dd-MM-yyyy");
                    CalendarExtender.SelectedDate = Convert.ToDateTime(obj.raid_date);
                  
                    raidtime.Value = obj.raid_time;
                    txtRaidLocation.Text = obj.raid_location?.ToString()?? string.Empty;
                    if (obj.thana_code != null)
                    {
                        ddlThana.SelectedValue = obj.thana_code.ToString();
                    }// obj.thana_code?.ToString()?? string.Empty;
                    
                    if (obj.recoverytype == "PR")
                    {
                        rdPersonally.Checked = true;
                        rdInformationFromControl.Checked = false;
                        rdInformationFromOther.Checked = false;
                        rdPersonally_OncheckedChange(sender, null);
                    }
                    else if (obj.recoverytype == "IC")
                    {
                        rdInformationFromOther.Checked = false;
                        rdPersonally.Checked = false;
                        rdInformationFromControl.Checked = true;
                        if (Session["UserID"].ToString().Contains("thana_"))
                        {
                            txtRaidLocation.Enabled = false;
                            ddlThana.Enabled = false;
                            txtName.Enabled = false;
                        }
                        rdInformationFromControl_OncheckedChange(sender, null);
                        //rdPersonally.Checked = false;
                        //rdInformationFromControl.Checked = true;
                        //rdInformationFromOther.Checked = false;
                        //txtRaidLocation.Enabled = false;
                        //ddlThana.Enabled = false;
                        //txtName.Enabled = false;
                    }
                    else if(obj.recoverytype == "IS")
                    {
                        rdPersonally.Checked = false;
                        rdInformationFromControl.Checked = false;
                        rdInformationFromOther.Checked = true;
                        rdInformationFromOther_OncheckedChange(sender, null);
                    }

                    txtName.Text = obj.recoveryname;
                    txtManualBookSeizure.Text = obj.manualseizureno?.ToString() ?? string.Empty;
                    txtLatitude.Text = obj.latitude?.ToString() ?? string.Empty;
                    txtLongitude.Text = obj.longitude;
                    txtRemarks.Text = obj.remarks;

                    Doc_id = 0;
                    for (int i = 0; i < obj.docs.Count; i++)
                    {
                       //seizureNo = Session["seizureNo"].ToString();
                        if (i == 0)
                            dummytable.Visible = false;
                        dt = (DataTable)ViewState["Records"];
                        dt.Rows.Add(obj.docs[i].doc_name, obj.docs[i].description, obj.docs[i].doc_path, obj.docs[i].seizure_docs_id);
                        grdAdd.DataSource = dt;
                        grdAdd.DataBind();
                        Doc_id++;

                    }
                    // for (int i = 0; i < dt.Rows.Count; i++)
                    for (int i = 0; i < obj.docs.Count; i++)
                    {
                        if (obj.record_status == "Y")
                        {
                            (grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = true;
                            (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = true;
                            ////btnSubmit.Visible = false;
                            //idupDocument.Enabled = false;
                            //txtDiscription.Enabled = false;
                            //btnUpload.Enabled = false;
                        }
                        if ((Session["rtype"].ToString() == "1"))
                        {
                            (grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = true;
                            (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                        }
                        }

                    if ((Session["rtype"].ToString() == "1") || obj.record_status=="Y")
                    {
                        btnCancel.Visible = false;
                        btnSaveasDraft.Visible = false;
                        idupDocument.Enabled = false;
                        txtDiscription.Enabled = false;
                        btnUpload.Enabled = false;
                        Image1.Visible = false;
                        docs.Visible = false;
                        //btnSubmit.Visible = false;
                        txtDATE.Attributes.Add("disabled", "disabled");
                        raidtime.Attributes.Add("disabled", "disabled");
                        txtRaidLocation.Attributes.Add("disabled", "disabled");
                        ddlThana.Attributes.Add("disabled", "disabled");
                        txtName.Attributes.Add("disabled", "disabled");
                        txtManualBookSeizure.Attributes.Add("disabled", "disabled");
                        txtLatitude.Attributes.Add("disabled", "disabled");
                        txtLongitude.Attributes.Add("disabled", "disabled");
                        txtRemarks.Attributes.Add("disabled", "disabled");
                        if (grdAdd.Rows.Count == 0)
                        {
                            grdAdd.Visible = false;
                        }
                        dummytable.Visible = false;
                        rdPersonally.Enabled = false;
                        rdInformationFromControl.Enabled = false;
                        rdInformationFromOther.Enabled = false;
                    }
                }
            }
        }

        protected void rdPersonally_OncheckedChange(object sender, EventArgs e)
        {

           
                if (rdPersonally.Checked)
                {
                    rdInformationFromControl.Checked = false;
                    rdInformationFromOther.Checked = false;
                    rdPersonally.Checked = true;
                    lblname.Text = "Name";
                    txtDATE.Text = txtdob.Value;
                }
            
        }

        protected void rdInformationFromControl_OncheckedChange(object sender, EventArgs e)
        {
           
                if (rdInformationFromControl.Checked)
                {
                    rdInformationFromControl.Checked = true;
                    lblname.Text = "Control Room Complaint No";
                   txtDATE.Text = txtdob.Value;
                }
           
        }

        protected void rdInformationFromOther_OncheckedChange(object sender, EventArgs e)
        {
           
                if (rdInformationFromOther.Checked)
                {
                    rdInformationFromControl.Checked = false;
                    rdPersonally.Checked = false;
                    lblname.Text = "Source Name";
                    txtDATE.Text = txtdob.Value;
                }
           
        }
        
        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                btnSaveasDraft.Enabled = false;
                cm_seiz_BasicIformation cm_obj = new cm_seiz_BasicIformation();
                if (!string.IsNullOrEmpty(txtDATE.Text))
                {
                    cm_obj.raid_date = txtDATE.Text.ToString();
                }
                else
                {
                    cm_obj.raid_date = DateTime.Now.ToShortDateString(); //txtdob.Value;  // Convert.ToDateTime(txtdob);
                }
               
                cm_obj.raid_time =raidtime.Value.ToString();
                if (cm_obj.raid_time == "")
                    cm_obj.raid_time = null;
                cm_obj.raid_location = txtRaidLocation.Text;
                cm_obj.raidby = Session["RaidBy"].ToString().Substring(0, 1);
                //cm_obj.thana_code = "1";// ddlThana.SelectedValue;
                cm_obj.thana_code =  ddlThana.SelectedValue;
                cm_obj.district_code = Session["district_code"].ToString();
                cm_obj.division_code = Session["division_code"].ToString();
                string strHostName = System.Net.Dns.GetHostName();
                string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                cm_obj.ipaddress = clientIPAddress.ToString();
                if (rdPersonally.Checked)
                {
                    cm_obj.recoverytype = "PR";
                }
                else if(rdInformationFromControl.Checked)
                {
                    cm_obj.recoverytype = "IC";
                }
                else if (rdInformationFromOther.Checked)
                {
                    cm_obj.recoverytype = "IS";
                }
               
                cm_obj.recoveryname = txtName.Text;
                cm_obj.manualseizureno = txtManualBookSeizure.Text;
                cm_obj.latitude = txtLatitude.Text;
                cm_obj.longitude = txtLongitude.Text;
                cm_obj.remarks = txtRemarks.Text;
                cm_obj.user_id = Session["UserID"].ToString();
                cm_obj.lastmodified_date = DateTime.Now;
                cm_obj.record_status = "N";
                cm_obj.record_deleted = false;

                int i = 0;
                cm_obj.docs = new List<Seizure_Docs>();
                dt = ViewState["Records"] as DataTable;
                foreach (DataRow dr in dt.Rows)
                {
                    Seizure_Docs doc = new Seizure_Docs();
                    doc.seizureno = cm_obj.seizureno.ToString();
                    doc.doc_name = dr["Doc_Name"].ToString();
                    doc.doc_path = (grdAdd.Rows[i].FindControl("lblFilePath") as Label).Text;
                    doc.description = (grdAdd.Rows[i].FindControl("lblDiscriptione") as Label).Text;
                    cm_obj.docs.Add(doc);
                    i++;
                }
                String a = Session["rtype1"].ToString();
                bool val;
                if (Session["rtype1"].ToString() == "0" && string.IsNullOrEmpty(Session["seizureNo"].ToString()))
                    val = BL_cm_seiz_BasicIformation.InsertBasicIformation(cm_obj);
                else
                {
                    if (! string.IsNullOrEmpty(Session["seizureNo"].ToString()))
                        cm_obj.seizureno =Convert.ToInt32(Session["seizureNo"].ToString());

                    val = BL_cm_seiz_BasicIformation.Update(cm_obj);
                }
                if (val == true)
                {
                    //Session["UserID"] = Session["UserID"];
                    Response.Redirect("SeizureList");
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
                
                cm_seiz_BasicIformation cm_obj = new cm_seiz_BasicIformation();
                if (!string.IsNullOrEmpty(txtDATE.Text))
                {
                    cm_obj.raid_date = txtdob.ToString();
                }
                else
                {
                    cm_obj.raid_date = DateTime.Now.ToShortDateString();// Convert.ToDateTime(txtdob);
                }
                cm_obj.raid_time = raidtime.Value.ToString();
                cm_obj.raid_location = txtRaidLocation.Text;
                cm_obj.thana_code =ddlThana.SelectedValue;
                if (rdPersonally.Checked)
                {
                    cm_obj.recoverytype = "PR";
                }
                else if (rdInformationFromControl.Checked)
                {
                    cm_obj.recoverytype = "IC";
                }
                else if (rdInformationFromOther.Checked)
                {
                    cm_obj.recoverytype = "IS";
                }

                cm_obj.recoveryname = txtName.Text;
                cm_obj.manualseizureno = txtManualBookSeizure.Text;
                cm_obj.latitude = txtLatitude.Text;
                cm_obj.longitude = txtLongitude.Text;
                cm_obj.remarks = txtRemarks.Text;
                cm_obj.user_id = Session["UserID"].ToString();
                cm_obj.lastmodified_date = DateTime.Now;
                cm_obj.record_status = "Y";
                cm_obj.record_deleted = false;
                if (cm_obj.raid_time == "")
                    cm_obj.raid_time = null;
                cm_obj.raidby = Session["RaidBy"].ToString().Substring(0, 1);
                string strHostName = System.Net.Dns.GetHostName();
                string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                cm_obj.ipaddress = clientIPAddress.ToString();
                bool val;
                if (Session["rtype1"].ToString() == "0")
                    val = BL_cm_seiz_BasicIformation.InsertBasicIformation(cm_obj);
                else
                {
                    val = BL_cm_seiz_BasicIformation.Update(cm_obj);
                }
                if (val == true)
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("SeizureList");
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
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/SeizureList");
            //Session["topdiv"] = "1";
        }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Session["SFIR"] = "";
            Session["topdiv"] = "1";
            Response.Redirect("SeizureList");
           
        }

        protected void UploadFile(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

                if (IsPostBack)
                {
                    //if (idupDocument.HasFile)
                    //{
                    dummytable.Visible = false;
                    string fileName = Path.GetFileName(idupDocument.PostedFile.FileName);
                    string[] filetype = fileName.Split('.');
                    string m = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-sss");
                    idupDocument.PostedFile.SaveAs(Server.MapPath("~/CM_Docs/") + filetype[0] + "_" + m + "." + filetype[1]);
                    string path = Server.MapPath("~/CM_Docs/") + filetype[0] + "_" + m + "." + filetype[1];
                    dt = (DataTable)ViewState["Records"];
                    dt.Rows.Add(fileName, txtDiscription.Text, path, Doc_id);
                    grdAdd.DataSource = dt;
                    grdAdd.DataBind();
                    Doc_id++;
                    txtDiscription.Text = "";
                    // }
                }
                //    //if (idupDocument.HasFile)
                //    //{
                //    dummytable.Visible = false;
                //    string fileName = Path.GetFileName(idupDocument.PostedFile.FileName);
                //    string[] filetype = fileName.Split('.');
                //    string m = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-sss");
                //    idupDocument.PostedFile.SaveAs(Server.MapPath("~/CM_Docs/") + filetype[0] + "_" + m + "." + filetype[1]);
                //    string path = Server.MapPath("~/CM_Docs/") + filetype[0] + "_" + m + "." + filetype[1];
                //    dt = (DataTable)ViewState["Records"];
                //    dt.Rows.Add(fileName, txtDiscription.Text, path, Doc_id);
                //    grdAdd.DataSource = dt;
                //    grdAdd.DataBind();
                //    Doc_id++;
                //    txtDiscription.Text = "";
                //    // }
                //}
            }
        }
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ImageButton lb = (ImageButton)sender;
                GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
                string filePath = (sender as ImageButton).CommandArgument;
                string a = Session["rtype1"].ToString();
             string v =  Path.GetFileName(filePath).ToString();
                if (Session["rtype1"].ToString() != "0")
                {
                    bool value = BL_User_Mgnt.Deletefile("seizure_docs", Session["seizureNo"].ToString(),v);
                    if (value)
                    {
                        File.Delete(Server.MapPath("~/CM_Docs/" + Path.GetFileName(filePath)));
                        FileInfo fInfoEvent;
                        fInfoEvent = new FileInfo(Path.GetFileName(filePath));
                        fInfoEvent.Delete();
                    }
                }
                else
                {
                    File.Delete(Server.MapPath("~/CM_Docs/" + Path.GetFileName(filePath)));
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
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                Response.WriteFile(Server.MapPath("~/CM_Docs/" + Path.GetFileName(filePath)));
                Response.End();
            }
        }
        protected void DeleteFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            File.Delete(filePath);
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
}