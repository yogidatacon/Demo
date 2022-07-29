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
    public partial class Daily_Dairy_Entry_Raid : System.Web.UI.Page
    {
        daily_diary_raid dd = new daily_diary_raid();
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        double total = 0;
        int Doc_id = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                a.Visible = false;
                a1.Visible = false;
                v.Visible = false;
                v1.Visible = false;
                ddlUOM.Visible = true;
                utype.Visible = false;
                rtype.Visible = false;
                ddVehicletype.Visible = false;
                n.Visible = false;
                txtvolumesno.Visible = false;
                DivDetails.Visible = false;
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

                if (ViewState["Records2"] == null)
                {
                    dt2.Columns.Add("gender_code");
                    dt2.Columns.Add("gender_Name");
                    dt2.Columns.Add("arresting");
                    dt2.Columns.Add("Doc_id");
                    dt2.Columns.Add("User_id");
                    ViewState["Records2"] = dt2;
                }
                if (ViewState["Records1"] == null)
                {
                    dt1.Columns.Add("type_of_recovery_name");
                    dt1.Columns.Add("type_of_recovery");
                    dt1.Columns.Add("recovery_description");
                    dt1.Columns.Add("recovery_particulars_id");
                    dt1.Columns.Add("uom_code");
                    dt1.Columns.Add("uom_name");
                    dt1.Columns.Add("recovery_qty");
                    dt1.Columns.Add("daily_dairy_raid_id");
                    dt1.Columns.Add("daily_dairy_recovery_id");
                    dt1.Columns.Add("User_id");
                    ViewState["Records1"] = dt1;
                }
                List<UOM_Master> UOM_MasterDetails = new List<UOM_Master>();
                UOM_MasterDetails = BL_UOM.GetList(string.Empty);
                ddlUOM.DataSource = UOM_MasterDetails;
                ddlUOM.DataTextField = "uom_name";
                ddlUOM.DataValueField = "uom_code";
                ddlUOM.DataBind();
                ddlUOM.Items.Insert(0, "Select");
                List<cm_gender> _gender = new List<cm_gender>();
                _gender = BL_cm_gender.GetList();
                ddlGender.DataSource = _gender;
                ddlGender.DataTextField = "gender_name";
                ddlGender.DataValueField = "gender_code";
                ddlGender.DataBind();
                ddlGender.Items.Insert(0, "Select");

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
                string c=Session["rtype"].ToString();
                Session["rtype1"] = Session["rtype"];
                string b = Session["rtype1"].ToString();
                if (Session["rtype"].ToString() == "0" && Session["rtype1"].ToString() == "0")
                {
                    Session["DDUserID"] = Session["UserID"].ToString();
                }
               if (Session["DDUserID"]==null)
                {
                    Session["DDUserID"] = Session["UserID"].ToString();
                }
                string f = Session["DDUserID"].ToString();
                UserDetails user1 = new UserDetails();
                user1 = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                UserDetails user = new UserDetails();
                user = BL_UserDetails.GetProfileUser(Session["DDUserID"].ToString(), user1.mobile);
                if (user != null)
                {
                    txtnameofofficer.Text = user.user_name;
                    ddlDesignation.SelectedValue = user.designation_code;
                    ddlDistrict.SelectedValue = user.district_code;
                    txtMobileNo.Text = user.mobile;
                }
               
                if (Session["rtype"].ToString() != "0" && Session["rtype1"].ToString()!="0")
                {
                    dd = new daily_diary_raid();
                    dd = BL_cm_seiz_Daily_Dairy_Raid.GetDetails(Session["daily_dairy_raid_id"].ToString());

                    txtraidlocation.Text = dd.place_of_raid;
                    txtraiddate.Text = dd.raid_entry_date;
                    txtteamleader.Text = dd.raid_team_leader;
                    ddlRecovery.SelectedValue = dd.raid_recovery;
                  //  ddlGender.SelectedValue = dd.gender;
                    txtdistance.Text = dd.distance_of_travelled;
                    if (dd.recovery.Count > 0)
                    {
                        //ddlExcisablegood.SelectedValue = dd.recovery[0].recovery_type;
                        //ddlExcisablegood_SelectedIndexChanged(sender, null);
                        //if (dd.recovery[0].recovery_particulars_id != "")
                        //    ddVehicletype.SelectedValue = dd.recovery[0].recovery_particulars_id;
                        //if (dd.recovery[0].uom_code != "")
                        //    ddlUOM.SelectedValue = dd.recovery[0].uom_code;
                        //txtvolumesno.Text = dd.recovery[0].recovery_qty;
                        Doc_id = 0;
                        for (int i = 0; i < dd.recovery.Count; i++)
                        {
                            if (i == 0)
                                Div1.Visible = false;
                            dt1 = (DataTable)ViewState["Records1"];
                            // dt1.Rows.Add("", dd.docs[i].doc_name, dd.docs[i].description, dd.docs[i].doc_path, dd.docs[i].seizure_docs_id);
                            dt1.Rows.Add(dd.recovery[i].recovery_description, dd.recovery[i].recovery_type, dd.recovery[i].recovery_particulars_name, dd.recovery[i].recovery_particulars_id, dd.recovery[i].uom_code, dd.recovery[i].uom_name, dd.recovery[i].recovery_qty, dd.daily_dairy_raid_id, dd.recovery[i].daily_dairy_recovery_id, dd.user_id);
                            grdrecovery.DataSource = dt1;
                            grdrecovery.DataBind();
                            Doc_id++;
                        }


                    }
                    txtnoarresting.Text = dd.no_of_arrested;
                    txtnoarresting_TextChanged(sender, e);
                    txtAbsconding.Text = dd.no_of_absconding;
                    txtnoofcaseinstituted.Text = dd.no_of_case_instituted;
                    txtotherrecovery.Text = dd.other_recovery;
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
                   
                        Doc_id = 0;
                        for (int i = 0; i < dd.genderlist.Count; i++)
                        {
                            //seizureNo = Session["seizureNo"].ToString();
                            if (i == 0)
                                ArrestingDiv.Visible = false;
                            dt2 = (DataTable)ViewState["Records2"];
                            dt2.Rows.Add(dd.genderlist[i].gender_code, dd.genderlist[i].gender_name, dd.genderlist[i].arresting, dd.genderlist[i].daily_dairy_raid_id, dd.genderlist[i].user_id);
                            grdArresting.DataSource = dt2;
                            grdArresting.DataBind();
                            Doc_id++;

                        }
                    
                }
                if (Session["rtype"].ToString() == "1" && Session["rtype1"].ToString() == "1")
                {
                    txtraidlocation.Enabled = false;
                    txtraiddate.Attributes.Add("Disabled", "Disabled");
                    txtnoarresting.Enabled = false;
                    txtnoofcaseinstituted.Enabled = false;
                    txtotherrecovery.Enabled = false;
                    txtteamleader.Enabled = false;
                    ddlExcisablegood.Enabled = false;
                    gender.Visible = false;
                    Arrst.Visible = false;
                    btnAdd.Visible = false;
                    docs.Visible = false;
                    btnGAdd.Visible = false;
                    txtvolumesno.Enabled = false;
                    txtAbsconding.Enabled = false;
                    ddlRecovery.Enabled = false;
                    ddlUOM.Enabled = false;
                    ddVehicletype.Enabled = false;
                    Image1.Visible = false;
                    txtdistance.Enabled = false;
                    btnCancel.Visible = false;
                    btnSaveasDraft.Visible = false;
                    btnSubmit.Visible = false;
                    for (int i = 0; i < dd.docs.Count; i++)
                    {
                        if (dd.docs[i].doc_name != "" && dd.docs[i].doc_name != null)
                        {
                            if ((Session["rtype"].ToString() == "1" && Session["rtype1"].ToString() == "1"))
                            {
                                (grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = true;
                                (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                            }
                        }
                    }
                    grdrecovery.Columns[grdrecovery.Columns.Count - 1].Visible = false;
                    grdArresting.Columns[grdArresting.Columns.Count - 1].Visible = false;
                    ddlExcisablegood.Visible = false;
                    rec.Visible = false;

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
                    bool value = BL_User_Mgnt.Deletefile("seizure_docs", Session["daily_dairy_raid_id"].ToString(), v);
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
                {
                    dummytable.Visible = true;
                    grdAdd.Visible = false;
                }
                //EnaTable.Visible = false;
            }
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string filePath = (sender as ImageButton).CommandArgument;
                
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename="+ filePath);
                //if (hdocsfrom.Value!=null && hdocsfrom.Value !="")
                //{
                    Response.WriteFile(Server.MapPath("~/CM_Docs/" + filePath));
                //}
                //else
                //{
                //    Response.WriteFile(Server.MapPath("~/MobileImages/" + filePath));
                //}
                Response.End();

            }
        }

        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                dd = new daily_diary_raid();
                dd.raid_entry_date = txtraiddate.Text;
                dd.place_of_raid = txtraidlocation.Text;
                dd.distance_of_travelled = txtdistance.Text;
                dd.raid_team_leader = txtteamleader.Text;
                dd.raid_recovery = ddlRecovery.SelectedValue;
                dd.no_of_absconding = txtAbsconding.Text;
                dd.no_of_arrested = txtnoarresting.Text;
                dd.gender = ddlGender.SelectedValue;
                dd.no_of_case_instituted = txtnoofcaseinstituted.Text;
                dd.other_recovery = txtotherrecovery.Text;
                dd.user_id = Session["UserID"].ToString();
                dd.record_status = "N";
                dd.recovery = new List<daily_dairy_recovery>();
                dt1 = ViewState["Records1"] as DataTable;
                int i = 0;
                if (dd.raid_recovery == "Yes")
                {
                    foreach (GridViewRow dr in grdrecovery.Rows)
                    {
                        daily_dairy_recovery recovery = new daily_dairy_recovery();
                        recovery.recovery_type = (grdrecovery.Rows[i].FindControl("lbltype_of_recovery") as Label).Text;
                        recovery.recovery_description = (grdrecovery.Rows[i].FindControl("lblrecovery_description") as Label).Text.Replace("-", "");
                        recovery.recovery_particulars_id = (grdrecovery.Rows[i].FindControl("lblrecovery_particulars_id") as Label).Text.Replace("-", "");
                        recovery.uom_code = (grdrecovery.Rows[i].FindControl("lbluom_code") as Label).Text.Replace("-", "");
                        //  recovery.uom_code = (grdAdd.Rows[i].FindControl("lbluom_code") as Label).Text;
                        recovery.recovery_qty = (grdrecovery.Rows[i].FindControl("lblrecovery_qty") as Label).Text;
                        recovery.daily_dairy_raid_id = (grdrecovery.Rows[i].FindControl("lbldaily_dairy_recovery_id") as Label).Text;
                        dd.recovery.Add(recovery);
                        i++;
                    }
                }
                dd.docs = new List<Seizure_Docs>();
                i = 0;
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

                dd.genderlist = new List<seizure_gender>();
                i = 0;
                dt2= ViewState["Records2"] as DataTable;
                foreach (DataRow dr in dt2.Rows)
                {
                    seizure_gender doc = new seizure_gender();
                    doc.gender_code= (grdArresting.Rows[i].FindControl("lblGendercode") as Label).Text;
                    doc.gender_name= (grdArresting.Rows[i].FindControl("lblGender") as Label).Text;
                    doc.arresting = Convert.ToInt32( (grdArresting.Rows[i].FindControl("lblArresting") as Label).Text);
                    doc.daily_dairy_raid_id = (grdArresting.Rows[i].FindControl("lblGenderid") as Label).Text;
                    doc.user_id = Session["UserID"].ToString();
                    dd.genderlist.Add(doc);
                    i++;
                }
                if (Session["rtype1"].ToString() == "0")
                {
                    bool val = BL_cm_seiz_Daily_Dairy_Raid.Insert(dd);
                    if(val)
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("Daily_Dairy_Entry_Raid_List.aspx");
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
                        Response.Redirect("Daily_Dairy_Entry_Raid_List.aspx");
                    }
                    
                }
                else
                {
                    dd.daily_dairy_raid_id = Session["daily_dairy_raid_id"].ToString();
                    bool val = BL_cm_seiz_Daily_Dairy_Raid.Update(dd);
                    if (val)
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("Daily_Dairy_Entry_Raid_List.aspx");
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
                        Response.Redirect("Daily_Dairy_Entry_Raid_List.aspx");
                    }
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("Daily_Dairy_Entry_Raid_List.aspx");
        }
     

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            if (IsPostBack)
            {
                ImageButton lb = (ImageButton)sender;
                GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
                DataTable dt3 = (DataTable)ViewState["Records1"];
                ViewState["CurrentTable"] = dt3;
                int rowID = gvRow.RowIndex;
                DataTable dt11 = ViewState["Records1"] as DataTable;
                dt11.Rows[rowID].Delete();
                ViewState["dt"] = dt11;
                grdrecovery.DataSource = dt11;
                grdrecovery.DataBind();
                for (int i = 0; i < grdrecovery.Rows.Count; i++)
                {
                    if ((grdrecovery.Rows[i].FindControl("lbldaily_dairy_recovery_id") as Label).Text != "0")
                    {
                        //  (grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = false;
                        //  (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                    }
                }
                if (grdrecovery.Rows.Count < 1)
                {
                    Div1.Visible = true;
                    grdrecovery.Visible = false;
                }
                //EnaTable.Visible = false;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (ddlExcisablegood.SelectedItem.ToString()!="Select")
                {

                    Div1.Visible = false;
                    //EnaTable.Visible = false;
                    
                    dt1 = (DataTable)ViewState["Records1"];
                    if(ddlExcisablegood.SelectedItem.ToString()=="D")
                    dt1.Rows.Add(ddlExcisablegood.SelectedItem.ToString(),ddlExcisablegood.SelectedValue, "",  "", "", "", txtvolumesno.Text,"0","0","");
                   else if (ddlExcisablegood.SelectedItem.ToString() == "V")
                        dt1.Rows.Add(ddlExcisablegood.SelectedItem.ToString(), ddlExcisablegood.SelectedValue, ddVehicletype.SelectedItem.ToString().Replace("Select", "-"), ddVehicletype.SelectedValue.Replace("Select", ""), "",  "", txtvolumesno.Text, "0", "0", "");
                    else
                        dt1.Rows.Add(ddlExcisablegood.SelectedItem.ToString(), ddlExcisablegood.SelectedValue, ddVehicletype.SelectedItem.ToString().Replace("Select", ""), ddVehicletype.SelectedValue.Replace("Select", ""), ddlUOM.SelectedValue.Replace("Select", ""), ddlUOM.SelectedItem.ToString().Replace("Select", ""), txtvolumesno.Text, "0", "0", "");
                    grdrecovery.DataSource = dt1;
                    grdrecovery.DataBind();
                    //for (int i = 0; i < grdrecovery.Rows.Count; i++)
                    //{
                    //    if ((grdrecovery.Rows[i].FindControl("lbldaily_dairy_recovery_id") as Label).Text!= "0")
                    //    {
                    //        (grdrecovery.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                    //    }
                    //}
                    Doc_id++;
                    if (grdrecovery.Rows.Count>0)
                    {
                       
                        grdrecovery.Visible = true;
                    }
                    ddlExcisablegood.SelectedIndex =-1;
                    ddVehicletype.SelectedIndex = -1;
                    ddlUOM.SelectedIndex = -1;
                    txtvolumesno.Text = "";
                }
            }

        }

        protected void ddlExcisablegood_SelectedIndexChanged(object sender, EventArgs e)
        {
           
                if(ddlExcisablegood.SelectedValue=="E")
                {
                    ddVehicletype.DataSource = null;
                    List<cm_article_category> _article_name = new List<cm_article_category>();
                    _article_name = BL_cm_article_category.GetList();
                    ddVehicletype.DataSource = _article_name;
                    ddVehicletype.DataTextField = "article_category_name";
                    ddVehicletype.DataValueField = "article_category_code";
                    ddVehicletype.DataBind();
                    ddVehicletype.Items.Insert(0, "Select");
                    ddVehicletype.Visible = true;
                    a.Visible = true;
                    a1.Visible = true;
                    v.Visible = false;
                    v1.Visible = false;
                    n.Visible = false;
                    ddlUOM.Visible = true;
                    txtvolumesno.Visible = true;
                    rtype.Visible = true;
                    u.Visible = true;
                    utype.Visible = true;
                }
                if (ddlExcisablegood.SelectedValue == "V")
                {
                    ddVehicletype.DataSource = null;
                    
                    List<cm_Vehicle_type> vehicleTypeDetails = new List<cm_Vehicle_type>();
                    vehicleTypeDetails = BL_cm_Vehicle_type.GetList();
                    ddVehicletype.DataSource = vehicleTypeDetails;
                    ddVehicletype.DataTextField = "vehicle_type";
                    ddVehicletype.DataValueField = "vehicle_type_code";
                    ddVehicletype.DataBind();
                    ddVehicletype.Items.Insert(0, "Select");
                    ddlUOM.Visible = false;
                    a.Visible = false;
                    a1.Visible = false;
                    v.Visible = true;
                    v1.Visible = true;
                    n.Visible = false;
                    txtvolumesno.Visible = true;
                    ddVehicletype.Visible = true;
                    u.Visible = false;
                    rtype.Visible = true;
                    utype.Visible = false;

                }
                if (ddlExcisablegood.SelectedValue == "D")
                {
                    ddVehicletype.Visible = false;
                    ddlUOM.Visible = false;
                    a.Visible = false;
                    a1.Visible = false;
                    v.Visible = false;
                    v1.Visible = false;
                    n.Visible = true;
                    txtvolumesno.Visible = true;
                    utype.Visible = false;
                    rtype.Visible = false;

                }
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                dd = new daily_diary_raid();
                dd.raid_entry_date = txtraiddate.Text;
                dd.place_of_raid = txtraidlocation.Text;
                dd.distance_of_travelled = txtdistance.Text;
                dd.raid_team_leader = txtteamleader.Text;
                dd.raid_recovery = ddlRecovery.SelectedValue;
                dd.no_of_absconding = txtAbsconding.Text;
                dd.no_of_arrested = txtnoarresting.Text;
                dd.gender = ddlGender.SelectedValue;
                dd.no_of_case_instituted = txtnoofcaseinstituted.Text;
                dd.other_recovery = txtotherrecovery.Text;
                dd.user_id = Session["UserID"].ToString();
                dd.record_status = "Y";
                dd.recovery = new List<daily_dairy_recovery>();
                dt1 = ViewState["Records1"] as DataTable;
                int i = 0;
                if (dd.raid_recovery == "Yes")
                {
                    foreach (DataRow dr in dt1.Rows)
                    {
                        daily_dairy_recovery recovery = new daily_dairy_recovery();
                        recovery.recovery_type = (grdrecovery.Rows[i].FindControl("lbltype_of_recovery") as Label).Text;
                        recovery.recovery_description = (grdrecovery.Rows[i].FindControl("lblrecovery_description") as Label).Text.Replace("-", "");
                        recovery.recovery_particulars_id = (grdrecovery.Rows[i].FindControl("lblrecovery_particulars_id") as Label).Text.Replace("-", "");
                        recovery.uom_code = (grdrecovery.Rows[i].FindControl("lbluom_code") as Label).Text.Replace("-", "");
                        //  recovery.uom_code = (grdAdd.Rows[i].FindControl("lbluom_code") as Label).Text;
                        recovery.recovery_qty = (grdrecovery.Rows[i].FindControl("lblrecovery_qty") as Label).Text;
                        recovery.daily_dairy_raid_id = (grdrecovery.Rows[i].FindControl("lbldaily_dairy_recovery_id") as Label).Text;
                        dd.recovery.Add(recovery);
                        i++;
                    }
                }
                dd.docs = new List<Seizure_Docs>();
                i = 0;
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
                dd.genderlist = new List<seizure_gender>();
                i = 0;
                dt2 = ViewState["Records2"] as DataTable;
                foreach (DataRow dr in dt2.Rows)
                {
                    seizure_gender doc = new seizure_gender();
                    doc.gender_code = (grdArresting.Rows[i].FindControl("lblGendercode") as Label).Text;
                    doc.gender_name = (grdArresting.Rows[i].FindControl("lblGender") as Label).Text;
                    doc.arresting = Convert.ToInt32((grdArresting.Rows[i].FindControl("lblArresting") as Label).Text);
                    doc.daily_dairy_raid_id = (grdArresting.Rows[i].FindControl("lblGenderid") as Label).Text;
                    doc.user_id = Session["UserID"].ToString();
                    dd.genderlist.Add(doc);
                    i++;
                }
                if (Session["rtype1"].ToString() == "0")
                {
                    bool val = BL_cm_seiz_Daily_Dairy_Raid.Insert(dd);
                    if (val)
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("Daily_Dairy_Entry_Raid_List.aspx");
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
                        Response.Redirect("Daily_Dairy_Entry_Raid_List.aspx");
                    }

                }
                else
                {
                    dd.daily_dairy_raid_id = Session["daily_dairy_raid_id"].ToString();
                    bool val = BL_cm_seiz_Daily_Dairy_Raid.Update(dd);
                    if (val)
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("Daily_Dairy_Entry_Raid_List.aspx");
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
                        Response.Redirect("Daily_Dairy_Entry_Raid_List.aspx");
                    }
                }
            }
        }

        protected void ShowRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("Daily_Dairy_Entry_Raid_List.aspx");
        }
        protected void txtnoarresting_TextChanged(object sender, EventArgs e)
        {
            if(txtnoarresting.Text!="")
            {
                if(Convert.ToDouble(txtnoarresting.Text) >0)
                {
                    DivDetails.Visible = true;
                    if(grdArresting.Rows.Count>0)
                    {
                        ArrestingDiv.Visible = false;

                    }
                    else
                    {
                        ArrestingDiv.Visible =true;
                    }
                }
                else
                {
                    DivDetails.Visible = false;
                }
            }
        }


        protected void btnAddgender_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                for (int i = 0; i < grdArresting.Rows.Count; i++)
                {
                    GridViewRow row1 = grdArresting.Rows[i];
                    string Qty1 = (row1.Cells[1].FindControl("lblArresting") as Label).Text;
                    total += Convert.ToDouble(Qty1);
                }

                txttotalarrest.Value =( total + Convert.ToDouble(txtArresting.Text)).ToString();
                 ArrestingDiv.Visible = false;
                //EnaTable.Visible = false;
                if (Convert.ToDouble(txttotalarrest.Value) <= Convert.ToDouble(txtnoarresting.Text))
                 {
                    dt2 = (DataTable)ViewState["Records2"];
                    dt2.Rows.Add(ddlGender.SelectedValue, ddlGender.SelectedItem.ToString(), txtArresting.Text, Doc_id, Session["UserID"].ToString());
                    grdArresting.DataSource = dt2;
                    grdArresting.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert(' Arresting must be less than or equal to No of Arresting ');", true);
                    ddlGender.ClearSelection();
                    ddlGender.Focus();
                    txtArresting.Text = "";
                    if (grdArresting.Rows.Count == 0)
                    {
                        ArrestingDiv.Visible = true;
                    }
                }
                //for (int i = 0; i < grdrecovery.Rows.Count; i++)
                //{
                //    if ((grdrecovery.Rows[i].FindControl("lbldaily_dairy_recovery_id") as Label).Text!= "0")
                //    {
                //        (grdrecovery.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                //    }
                //}
                
                    Doc_id++;
                    if (grdArresting.Rows.Count > 0)
                    {

                    grdArresting.Visible = true;
                    }
                    ddlGender.SelectedIndex = -1;
                    txtArresting.Text = "";
                
            }

        }

        protected void btnDelete_Click1(object sender, ImageClickEventArgs e)
        {
            if (IsPostBack)
            {
                ImageButton lb = (ImageButton)sender;
                GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
                DataTable dt4 = (DataTable)ViewState["Records2"];
                ViewState["CurrentTable"] = dt4;
                int rowID = gvRow.RowIndex;
                DataTable dt12 = ViewState["Records2"] as DataTable;
                dt12.Rows[rowID].Delete();
                ViewState["dt"] = dt12;
               grdArresting.DataSource = dt12;
                grdArresting.DataBind();
                for (int i = 0; i < grdArresting.Rows.Count; i++)
                {
                    //if ((grdArresting.Rows[i].FindControl("lbldaily_dairy_recovery_id") as Label).Text != "0")
                    //{
                    //    //  (grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = false;
                    //    //  (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                    //}
                }
                if (grdArresting.Rows.Count < 1)
                {
                    ArrestingDiv.Visible = true;
                    grdArresting.Visible = false;
                }
                //EnaTable.Visible = false;
            }
        }
       

      
    }
}