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
    public partial class Dairy_Daily_Otherthan_Raid : System.Web.UI.Page
    {
        daily_diary_entry_otherthan_raid dd = new daily_diary_entry_otherthan_raid();
        DataTable dt = new DataTable();
        int Doc_id = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CalendarExtender1.EndDate = DateTime.Now;
                if (ViewState["Records"] == null)
                {
                    dt.Columns.Add("Status");
                    dt.Columns.Add("Doc_Name");
                    dt.Columns.Add("Discription");
                    dt.Columns.Add("Doc_Path");
                    dt.Columns.Add("Doc_id");
                    dt.Columns.Add("User_id");
                    ViewState["Records"] = dt;
                }
                List<District> districts = new List<District>();
                List<Designation_Details> designation = new List<Designation_Details>();
                districts = BL_User_Mgnt.GetAllDistrictsList();
                var list = from s in districts
                           select s;
                ddlDistrict.DataSource = list.ToList();
                ddlDistrict.DataTextField = "district_name";
                ddlDistrict.DataValueField = "district_code";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, "Select");
                designation = BL_Designation_Details.GetDList();
                ddlDesignation.DataSource = designation.ToList();
                ddlDesignation.DataTextField = "designation_name";
                ddlDesignation.DataValueField = "designation_code";
                ddlDesignation.DataBind();
                ddlDistrict.Items.Add(new ListItem("All", "AL"));
                ddlDesignation.Items.Insert(0, "Select");
                List<UOM_Master> UOM_MasterDetails = new List<UOM_Master>();
                UOM_MasterDetails = BL_UOM.GetList(string.Empty);
                ddlUOM.DataSource = UOM_MasterDetails;
                ddlUOM.DataTextField = "uom_name";
                ddlUOM.DataValueField = "uom_code";
                ddlUOM.DataBind();
                ddlUOM.Items.Insert(0, "Select");
                liqdiv.Visible = false;
                Session["rtype1"] = Session["rtype"];
                if (Session["rtype"].ToString() == "0" && Session["rtype1"].ToString()=="0")
                {
                    Session["DDOUserID"] = Session["UserID"].ToString();
                }
                if (Session["DDOUserID"] == null)
                {
                    Session["DDOUserID"] = Session["UserID"].ToString();
                }
                string f = Session["DDOUserID"].ToString();
                UserDetails user1 = new UserDetails();
                user1 = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                UserDetails user = new UserDetails();
                user = BL_UserDetails.GetProfileUser(Session["DDOUserID"].ToString(), user1.mobile);
                if (user != null)
                {
                    txtnameofofficer.Text = user.user_name;
                    ddlDesignation.SelectedValue = user.designation_code;
                    ddlDistrict.SelectedValue = user.district_code;
                    txtMobileNo.Text = user.mobile;
                }
              
                if (Session["rtype"].ToString() != "0")
                {
                    dd = new daily_diary_entry_otherthan_raid();
                    dd = BL_cm_seiz_Daily_Dairy_otherthan_Raid.GetDetails(Session["daily_dairy_otherthan_raid_id"].ToString());
                    txtraiddate.Text = dd.raid_entry_date;
                    ddlIntelligence.SelectedValue = dd.intelligence_gathering;
                    ddPetrolling.SelectedValue = dd.petrolling;
                    ddlVehiclecheck.SelectedValue = dd.vehicle_check;
                    ddlliquordestruction.SelectedValue = dd.liquor_destruction;
                    ddlliquordestruction_SelectedIndexChanged(sender, e);
                    ddlwitnessappearance.SelectedValue = dd.witness_appearance_in_court;
                    ddOthers.SelectedValue = dd.others;
                    ddlUOM.SelectedValue = dd.uom_code;
                    txtQuantity.Text = dd.quantity.ToString();
                    txtMeeting.Text = dd.meeting;
                    txtrecovery.Text = dd.raid_recovery;
                    Doc_id = 0;
                    for (int i = 0; i < dd.docs.Count; i++)
                    {
                        //seizureNo = Session["seizureNo"].ToString();
                        if (i == 0)
                            dummytable.Visible = false;
                        dt = (DataTable)ViewState["Records"];
                        if (dd.docs[i].doc_name != "" && dd.docs[i].doc_name != null)
                        {
                            dt.Rows.Add("", dd.docs[i].doc_name, dd.docs[i].description, dd.docs[i].doc_path, dd.docs[i].seizure_docs_id);
                            grdAdd.DataSource = dt;
                            grdAdd.DataBind();
                        }
                        Doc_id++;
                        if (dd.docs[i].docs_from != "" && dd.docs[i].docs_from != null)
                            hdocsfrom.Value = dd.docs[i].docs_from;
                        else
                            hdocsfrom.Value = "";
                    }
                }
                if (Session["rtype"].ToString() == "1")
                {
                    txtraiddate.Attributes.Add("Disabled", "Disabled");
                    Image1.Visible = false;
                    ddlIntelligence.Enabled = false;
                    ddPetrolling.Enabled = false;
                    ddlVehiclecheck.Enabled = false;
                    ddlliquordestruction.Enabled = false;
                    ddlwitnessappearance.Enabled = false;
                    ddOthers.Enabled = false;
                    ddlUOM.Enabled = false;
                    txtMeeting.ReadOnly = true;
                    txtQuantity.ReadOnly = true;
                    txtrecovery.Enabled = false;
                    docs.Visible = false;
                    for (int i = 0; i < dd.docs.Count; i++)
                    {
                        if (dd.docs[i].doc_name != "" && dd.docs[i].doc_name != null)
                        {
                            if ((Session["rtype"].ToString() == "1"))
                            {
                                (grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = true;
                                (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                            }
                        }
                    }
                    btnCancel.Visible = false;
                    btnSaveasDraft.Visible = false;
                    btnSubmit.Visible = false;

                }
            }
        }
        protected void DDER_Click(object sender, EventArgs e)
        {
            Response.Redirect("Daily_Dairy_Entry_Raid_List.aspx");
        }

        protected void DDEOR_Click(object sender, EventArgs e)
        {
            Response.Redirect("Daily_Dairy_Otherthan_Raid_List.aspx");
        }

        protected void EVT_Click(object sender, EventArgs e)
        {
            Response.Redirect("Event_List.aspx");
        }
        protected void UploadFile(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (idupDocument.HasFile)
                {

                    dummytable.Visible = false;
                    //EnaTable.Visible = false;
                    string fileName = Path.GetFileName(idupDocument.PostedFile.FileName);
                    //fileName.Replace)
                    //  string[] filetype = fileName.Replace("'","").Split('.');
                    FileInfo fi = new FileInfo(fileName);
                    string[] filetype = fileName.Replace("'", "").Split('.');
                    string m = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-sss");
                    idupDocument.PostedFile.SaveAs(Server.MapPath("~/CM_Docs/") + fi.Name.Replace(fi.Extension, "").Replace(",", "_").Replace(" ", "_") + "_" + m + fi.Extension);
                    string path = Server.MapPath("~/CM_Docs/") + fi.Name.Replace(fi.Extension, "") + "_" + m + fi.Extension;
                    dt = (DataTable)ViewState["Records"];
                    dt.Rows.Add("", fileName, txtDiscription.Text, path, Doc_id);
                    grdAdd.DataSource = dt;
                    grdAdd.DataBind();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if ((grdAdd.Rows[i].FindControl("lblStatus") as Label).Text == "1")
                        {
                            //(grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = false;
                            (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                        }
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
                string a = Session["rtype1"].ToString();
                string v = Path.GetFileName(filePath).ToString();
                if (Session["rtype1"].ToString() != "0")
                {
                    bool value = BL_User_Mgnt.Deletefile("seizure_docs", Session["daily_dairy_otherthan_raid_id"].ToString(), v);
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
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    if ((grdAdd.Rows[i].FindControl("lblStatus") as Label).Text == "1")
                    {
                        //  (grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = false;
                        //  (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                    }
                }
                if (dt1.Rows.Count < 1)
                    dummytable.Visible = true;
                //EnaTable.Visible = false;
            }
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                //string filePath = (sender as ImageButton).CommandArgument;
                //Response.ContentType = ContentType;
                //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath.Replace(",", "_").Replace(" ", "_")));
                //if (File.Exists(Server.MapPath("~/CM_Docs/" + Path.GetFileName(filePath.Replace(",", "_").Replace(" ", "_")))))
                //    Response.WriteFile(Server.MapPath("~/CM_Docs/" + Path.GetFileName(filePath.Replace(",", "_").Replace(" ", "_"))));
                //else
                //    Response.WriteFile(Server.MapPath("~/CM_Docs/" + Path.GetFileName(filePath)));
               // Response.End();
                string filePath = (sender as ImageButton).CommandArgument;
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filePath);
               // if (hdocsfrom.Value != null && hdocsfrom.Value != "")
                    Response.WriteFile(Server.MapPath("~/CM_Docs/" + filePath));
                //else
                //    Response.WriteFile(Server.MapPath("~/MobileImages/" + filePath));
                Response.End();
               
            }
        }

        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                dd = new daily_diary_entry_otherthan_raid();
                dd.raid_entry_date = txtraiddate.Text;
                dd.intelligence_gathering = ddlIntelligence.SelectedValue;
                dd.petrolling = ddPetrolling.SelectedValue;
                dd.vehicle_check = ddlVehiclecheck.SelectedValue;
                dd.liquor_destruction = ddlliquordestruction.SelectedValue;
                if (ddlliquordestruction.SelectedValue == "Yes")
                {
                    dd.uom_code = ddlUOM.SelectedValue;
                    dd.quantity = Convert.ToDouble(txtQuantity.Text);
                }
                dd.witness_appearance_in_court = ddlwitnessappearance.SelectedValue;
                dd.others = ddOthers.SelectedValue;
                dd.raid_recovery = txtrecovery.Text;
                dd.user_id = Session["UserID"].ToString();
                dd.record_status = "N";
                dd.meeting = txtMeeting.Text;
                dd.docs = new List<Seizure_Docs>();
                int i = 0;
                dt = ViewState["Records"] as DataTable;
                foreach (DataRow dr in dt.Rows)
                {
                    Seizure_Docs doc = new Seizure_Docs();

                    doc.doc_name = dr["Doc_Name"].ToString();
                    doc.doc_path = (grdAdd.Rows[i].FindControl("lblFilePath") as Label).Text;
                    doc.description = (grdAdd.Rows[i].FindControl("lblDiscriptione") as Label).Text;

                    dd.docs.Add(doc);
                    i++;
                }
                if (Session["rtype1"].ToString() == "0")
                {
                    bool val = BL_cm_seiz_Daily_Dairy_otherthan_Raid.Insert(dd);
                    if (val)
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("Daily_Dairy_Otherthan_Raid_List.aspx");
                    }
                    else
                    {
                        string message = "Record is Not Saved.";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                        Session["UserID"] = Session["UserID"].ToString();
                        Response.Redirect("Daily_Dairy_Otherthan_Raid_List.aspx");
                    }

                }
                else
                {
                    dd.daily_dairy_otherthan_raid_id = Session["daily_dairy_otherthan_raid_id"].ToString();
                    bool val = BL_cm_seiz_Daily_Dairy_otherthan_Raid.Update(dd);
                    if (val)
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("Daily_Dairy_Otherthan_Raid_List.aspx");
                    }
                    else
                    {
                        string message = "Record is Not Updated.";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                        Session["UserID"] = Session["UserID"].ToString();
                        Response.Redirect("Daily_Dairy_Otherthan_Raid_List.aspx");
                    }
                }
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                dd = new daily_diary_entry_otherthan_raid();
                dd.raid_entry_date = txtraiddate.Text;
                dd.intelligence_gathering = ddlIntelligence.SelectedValue;
                dd.petrolling = ddPetrolling.SelectedValue;
                dd.vehicle_check = ddlVehiclecheck.SelectedValue;
                dd.liquor_destruction = ddlliquordestruction.SelectedValue;
                if(ddlliquordestruction.SelectedValue=="Yes")
                {
                    dd.uom_code = ddlUOM.SelectedValue;
                    dd.quantity = Convert.ToDouble( txtQuantity.Text);
                }
                dd.witness_appearance_in_court = ddlwitnessappearance.SelectedValue;
                dd.others = ddOthers.SelectedValue;
                dd.raid_recovery = txtrecovery.Text;
                dd.user_id = Session["UserID"].ToString();
                dd.record_status = "Y";
                dd.meeting = txtMeeting.Text;
                dd.docs = new List<Seizure_Docs>();
                int i = 0;
                dt = ViewState["Records"] as DataTable;
                foreach (DataRow dr in dt.Rows)
                {
                    Seizure_Docs doc = new Seizure_Docs();

                    doc.doc_name = dr["Doc_Name"].ToString();
                    doc.doc_path = (grdAdd.Rows[i].FindControl("lblFilePath") as Label).Text;
                    doc.description = (grdAdd.Rows[i].FindControl("lblDiscriptione") as Label).Text;

                    dd.docs.Add(doc);
                    i++;
                }
                if (Session["rtype1"].ToString() == "0")
                {
                    bool val = BL_cm_seiz_Daily_Dairy_otherthan_Raid.Insert(dd);
                    if (val)
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("Daily_Dairy_Otherthan_Raid_List.aspx");
                    }
                    else
                    {
                        string message = "Record is Not Saved.";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                        Session["UserID"] = Session["UserID"].ToString();
                        Response.Redirect("Daily_Dairy_Otherthan_Raid_List.aspx");
                    }

                }
                else
                {
                    dd.daily_dairy_otherthan_raid_id = Session["daily_dairy_otherthan_raid_id"].ToString();
                    bool val = BL_cm_seiz_Daily_Dairy_otherthan_Raid.Update(dd);
                    if (val)
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("Daily_Dairy_Otherthan_Raid_List.aspx");
                    }
                    else
                    {
                        string message = "Record is Not Updated.";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                        Session["UserID"] = Session["UserID"].ToString();
                        Response.Redirect("Daily_Dairy_Otherthan_Raid_List.aspx");
                    }
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("Daily_Dairy_Otherthan_Raid_List.aspx");
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("Daily_Dairy_Otherthan_Raid_List.aspx");
        }

        protected void ddlliquordestruction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlliquordestruction.SelectedValue=="Yes")
            {
                liqdiv.Visible = true;
            }
            else
            {
                liqdiv.Visible = false;
            }


        }



        }
}