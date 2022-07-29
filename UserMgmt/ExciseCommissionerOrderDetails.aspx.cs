using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class ExciseCommissionerOrderDetails : System.Web.UI.Page
    {
        List<cm_seiz_Excisecom> _Apparatus = new List<cm_seiz_Excisecom>();
        List<cm_seiz_ExcisableArticlesSeized> articals = new List<cm_seiz_ExcisableArticlesSeized>();
        List<cm_seiz_vehicledetails> vehicals = new List<cm_seiz_vehicledetails>();
        List<cm_seiz_Apparatus> Apparatus = new List<cm_seiz_Apparatus>();
        List<cm_seiz_Property> property = new List<cm_seiz_Property>();
        List<cm_seiz_Money> money = new List<cm_seiz_Money>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                    
                string seizureNo = Session["seizureNo"]?.ToString()?? string.Empty;
                //Get FirNo
                cm_seiz_FIR firDetails = BL_CM_Common.GetPRFIRNo(seizureNo+"&"+Session["RaidBy"].ToString());
                if (firDetails.prfirno != null)
                {
                    txtPRFIRNo.Text = firDetails.prfirno.Trim();
                    txtfirdate.Text = firDetails.prfirdate.Trim();
                    string datelock = BL_CM_Common.GetDate("exciseautomation.seizure_dmconfiscation where seizureNo='" + seizureNo + "'", "dmorderdate");
                    CalendarExtender1.StartDate = Convert.ToDateTime(datelock.Trim());
                    CalendarExtender2.StartDate = Convert.ToDateTime(datelock.Trim());
                    CalendarExtender3.StartDate = Convert.ToDateTime(datelock.Trim());
                }
                articals = BL_cm_seiz_ExcisableArticlesSeized.GetList(Session["seizureNo"].ToString() + "&" + Session["RaidBy"]);
                grdExcisableArticle.DataSource = articals.ToArray();
                grdExcisableArticle.DataBind();

                vehicals = new List<cm_seiz_vehicledetails>();
                vehicals = BL_cm_seiz_vehicledetails.GetList(seizureNo + "&" + Session["RaidBy"]);
                GridView1.DataSource = vehicals.ToArray();
                GridView1.DataBind();
                Apparatus = BL_cm_seiz_Apparatus.GetList(Session["seizureNo"].ToString() + "&" + Session["RaidBy"]);
                grdApparatusView.DataSource = Apparatus.ToArray();
                grdApparatusView.DataBind();
                property = new List<cm_seiz_Property>();
                property = BL_Property.GetList(Session["seizureNo"].ToString() + "&" + Session["RaidBy"]);
                grdPropertyView.DataSource = property.ToList();
                grdPropertyView.DataBind();
                money = new List<cm_seiz_Money>();
                money = BL_Money.GetList(Session["seizureNo"].ToString() + "&" + Session["RaidBy"]);
                grdMoneyListView.DataSource = money.ToList();
                grdMoneyListView.DataBind();
                grdExcisableArticle.Attributes.Add("disabled", "disabled");
                GridView1.Attributes.Add("disabled", "disabled");
                grdApparatusView.Attributes.Add("disabled", "disabled");
                grdPropertyView.Attributes.Add("disabled", "disabled");
                grdMoneyListView.Attributes.Add("disabled", "disabled");

                if (Session["rtype"].ToString() != "0")
                {
                    string tableId = Session["tableId"].ToString();

                    cm_seiz_Excisecom obj = new cm_seiz_Excisecom();
                    obj = BL_cm_seiz_Excisecom.GetDetailsByID(tableId);

                    //cm_obj.prfirno = ddlPRFIRNo.SelectedValue;
                    txtLetterNo.Text = obj.appl_letterno;
                    txtDate.Text= obj.appl_letterdate  ;
                    txtOrderNo.Text=obj.ecorderno;
                    txtOrderDate.Text= obj.ecorderdate;
                    txtDMRemarks.Text= obj.ecremarks;
                    txtConfiscationCaseNo.Text= obj.confiscation_caseno;
                    txtNameofCommissioner.Text= obj.magistratename;
                    txtTotalReceivedAmount.Text=(obj.amountreceived.ToString());
                    txthigherdate.Text = obj.highauthority_date;
                    txtHigherAuthorityName.Text = obj.highauthority_name;
                    txtRemarks.Text = obj.highauthority_remarks;
                    txtConfiscationCaseNo.Text= obj.confiscation_caseno;
                    CalendarExtender1.StartDate = Convert.ToDateTime(obj.appl_letterdate);
                    CalendarExtender2.StartDate = Convert.ToDateTime(obj.ecorderdate);
                    CalendarExtender3.StartDate = Convert.ToDateTime(obj.highauthority_date);
                    if ((Session["rtype"].ToString() == "1"))
                    {
                        btnCancel.Visible = false;
                        btnSaveasDraft.Visible = false;
                        btnSubmit.Visible = false;
                        Image1.Visible = false;
                        Image3.Visible = false;
                        Image4.Visible = false;
                       // ddlPRFIRNo.Attributes.Add("disabled", "disabled");
                        txtLetterNo.Attributes.Add("disabled", "disabled");
                        txtDate.Attributes.Add("disabled", "disabled");
                        txtOrderNo.Attributes.Add("disabled", "disabled");
                        txtOrderDate.Attributes.Add("disabled", "disabled");
                        txtDMRemarks.Attributes.Add("disabled", "disabled");
                        txtConfiscationCaseNo.Attributes.Add("disabled", "disabled");
                        txtNameofCommissioner.Attributes.Add("disabled", "disabled");
                        txtTotalReceivedAmount.Attributes.Add("disabled", "disabled");
                        txthigherdate.Attributes.Add("disabled", "disabled");
                        txtHigherAuthorityName.Attributes.Add("disabled", "disabled");
                        txtRemarks.Attributes.Add("disabled", "disabled");
                        txtConfiscationCaseNo.Attributes.Add("disabled", "disabled");
                    }
                }
            }
        }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {

            Response.Redirect("ExciseCommissionerOrderDetailsList");
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //cm_seiz_Excisecom cm_obj = new cm_seiz_Excisecom();
            ////cm_obj.bail_type_master_code = txtcode.Text;
            ////cm_obj.bail_type_master_name = txtName.Text;
            //cm_obj.user_id = Session["UserID"].ToString();
            //cm_obj.lastmodified_date = DateTime.Now;
            //cm_obj.user_id = Session["UserID"].ToString();
            ////cm_obj.record_status = true;
            //cm_obj.record_deleted = false;
            //if (BL_cm_seiz_Excisecom.InsertSeiz_Excisecom(cm_obj))
            //{
            //    string message = "Record is Updated.";
            //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //    sb.Append("<script type = 'text/javascript'>");
            //    sb.Append("window.onload=function(){");
            //    sb.Append("alert('");
            //    sb.Append(message);
            //    sb.Append("')};");
            //    sb.Append("</script>");
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
            //    Session["UserID"] = Session["UserID"].ToString();
            //    Response.Redirect("~/BailMasterList");
            //}

            if (IsPostBack)
            {
                cm_seiz_Excisecom cm_obj = new cm_seiz_Excisecom();
                cm_obj.seizureno = Convert.ToInt32(Session["seizureNo"].ToString());
                // cm_obj.prfirno = ddlPRFIRNo.SelectedValue;
                cm_obj.appl_letterno = txtLetterNo.Text;
                cm_obj.appl_letterdate = txtDate.Text;
                cm_obj.ecorderno = txtOrderNo.Text;
                cm_obj.ecorderdate = txtOrderDate.Text;
                cm_obj.ecremarks = txtDMRemarks.Text;
                cm_obj.confiscation_caseno = txtConfiscationCaseNo.Text;
                cm_obj.magistratename = txtNameofCommissioner.Text;
                cm_obj.amountreceived = txtTotalReceivedAmount.Text;
                cm_obj.highauthority_date = txthigherdate.Text;
                cm_obj.highauthority_name = txtHigherAuthorityName.Text;
                cm_obj.highauthority_remarks = txtRemarks.Text;
                cm_obj.finalseizureno = Session["seizureNo"].ToString();
                cm_obj.confiscation_caseno = txtConfiscationCaseNo.Text;
                cm_obj.raidby = Session["RaidBy"].ToString().Substring(0, 1).ToUpper();
               
                //cm_obj.auction_orderby = txd.Text;
                //cm_obj.destruction_orderby = txtOwnerPresentAddress.Text;

                cm_obj.user_id = Session["UserID"].ToString();
                cm_obj.lastmodified_date = DateTime.Now.ToShortDateString();
                cm_obj.creation_date = DateTime.Now.ToShortDateString();
                cm_obj.record_status = "Y";
                cm_obj.record_deleted = false;
                DateTime dt2 = DateTime.ParseExact(cm_obj.ecorderdate, "dd-MM-yyyy", null);
                DateTime dt1 = DateTime.ParseExact(cm_obj.highauthority_date, "dd-MM-yyyy", null);
                DateTime dt3 = DateTime.ParseExact(cm_obj.appl_letterdate, "dd-MM-yyyy", null);
               
                int cmp = dt2.CompareTo(dt1);
                int cmp1 = dt3.CompareTo(dt1);
                int cmp2 = dt3.CompareTo(dt2);

                if (cmp > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the appeal Date should be greater than or equal to the order Date.\');", true);
                    return;
                    // date1 is greater means date1 is comes after date2
                }
                if (cmp1 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the appeal Date should be greater than or equal to the Letter Date.\');", true);
                    return;
                    // date1 is greater means date1 is comes after date2
                }
                if (cmp2 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the order Date should be greater than or equal to the Letter Date.\');", true);
                    return;
                    // date1 is greater means date1 is comes after date2
                }
                else
                {
                    btnSubmit.Enabled = false;
                    bool val;
                    //foreach (GridViewRow dr in grdExcisableArticle.Rows)
                    //{
                    //    cm_seiz_ExcisableArticlesSeized articals = new cm_seiz_ExcisableArticlesSeized();
                    //    articals.seizureno = cm_obj.seizureno;
                    //    articals.seizure_excisable_articles_id = Convert.ToInt32((dr.FindControl("lblArticleId") as Label).Text);

                    //    articals.date_of_destruction = (dr.FindControl("AvalueDateFixedForDestruction") as TextBox).Text;
                    //    articals.actioncompleted = (dr.FindControl("ddlAction") as DropDownList).SelectedItem.ToString();
                    //    cm_obj.articals.Add(articals);

                    //}
                    //cm_obj.vehicals = new List<cm_seiz_vehicledetails>();
                    //foreach (GridViewRow dr in GridView1.Rows)
                    //{
                    //    cm_seiz_vehicledetails vehicals = new cm_seiz_vehicledetails();
                    //    vehicals.seizureno = cm_obj.seizureno;
                    //    vehicals.seizure_vehicledetails_id = Convert.ToInt32((dr.FindControl("lblVehicleId") as Label).Text);
                    //    vehicals.actioncompleted = (dr.FindControl("ddlAction") as DropDownList).SelectedItem.ToString();
                    //    vehicals.auction_or_releasedate = (dr.FindControl("auction_or_releasedate") as TextBox).Text;
                    //    vehicals.auctionreleaseamount = (dr.FindControl("txtAmountRecieved") as TextBox).Text;
                    //    vehicals.infavourof = (dr.FindControl("txtInFavourOf") as TextBox).Text;

                    //    vehicals.challan_date = (dr.FindControl("ChallanDate") as TextBox).Text;
                    //    vehicals.challan_no = (dr.FindControl("txtDepositedVideChallanNo") as TextBox).Text;
                    //    cm_obj.vehicals.Add(vehicals);

                    //}
                    string tempTableId = Session["tableId"]?.ToString() ?? string.Empty;
                    if (Session["rtype"].ToString() == "0" && string.IsNullOrEmpty(tempTableId))
                        val = BL_cm_seiz_Excisecom.InsertSeiz_Excisecom(cm_obj);
                    else
                    {
                        cm_obj.seizure_excisecom_id = Convert.ToInt32(Session["tableId"].ToString());
                        val = BL_cm_seiz_Excisecom.Update(cm_obj);
                    }
                    if (val == true)
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("~/ExciseCommissionerOrderDetailsList");
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
        }

        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                cm_seiz_Excisecom cm_obj = new cm_seiz_Excisecom();
                cm_obj.seizureno = Convert.ToInt32(Session["seizureNo"].ToString());               
                
               // cm_obj.prfirno = ddlPRFIRNo.SelectedValue;
                cm_obj.appl_letterno = txtLetterNo.Text;
                cm_obj.appl_letterdate = txtDate.Text;
                cm_obj.ecorderno = txtOrderNo.Text;
                cm_obj.ecorderdate = txtOrderDate.Text;
                cm_obj.ecremarks = txtDMRemarks.Text;
                cm_obj.confiscation_caseno = txtConfiscationCaseNo.Text;
                cm_obj.magistratename = txtNameofCommissioner.Text;
                cm_obj.amountreceived = txtTotalReceivedAmount.Text;
                cm_obj.highauthority_date = txthigherdate.Text;
                cm_obj.highauthority_name = txtHigherAuthorityName.Text;
                cm_obj.highauthority_remarks = txtRemarks.Text;
                cm_obj.finalseizureno = Session["seizureNo"].ToString();                
                cm_obj.confiscation_caseno = txtConfiscationCaseNo.Text;


                //cm_obj.auction_orderby = txd.Text;
                //cm_obj.destruction_orderby = txtOwnerPresentAddress.Text;

                cm_obj.user_id = Session["UserID"].ToString();
                cm_obj.lastmodified_date = DateTime.Now.ToShortDateString();
                cm_obj.creation_date = DateTime.Now.ToShortDateString();
                cm_obj.record_status = "N";
                cm_obj.record_deleted = false;
                cm_obj.raidby = Session["RaidBy"].ToString().Substring(0, 1).ToUpper();
                DateTime dt2 = DateTime.ParseExact(cm_obj.ecorderdate, "dd-MM-yyyy", null);
                DateTime dt1 = DateTime.ParseExact(cm_obj.highauthority_date, "dd-MM-yyyy", null);
                DateTime dt3 = DateTime.ParseExact(cm_obj.appl_letterdate, "dd-MM-yyyy", null);

                int cmp = dt2.CompareTo(dt1);
                int cmp1 = dt3.CompareTo(dt1);
                int cmp2 = dt3.CompareTo(dt2);

                if (cmp > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the appeal Date should be greater than or equal to the order Date.\');", true);
                    return;
                    // date1 is greater means date1 is comes after date2
                }
                if (cmp1 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the appeal Date should be greater than or equal to the Letter Date.\');", true);
                    return;
                    // date1 is greater means date1 is comes after date2
                }
                if (cmp2 > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please ensure that the order Date should be greater than or equal to the Letter Date.\');", true);
                    return;
                    // date1 is greater means date1 is comes after date2
                }
                else
                {
                    btnSaveasDraft.Enabled = false;
                    bool val;
                    //cm_obj.articals = new List<cm_seiz_ExcisableArticlesSeized>();
                    //foreach (GridViewRow dr in grdExcisableArticle.Rows)
                    //{
                    //    cm_seiz_ExcisableArticlesSeized articals = new cm_seiz_ExcisableArticlesSeized();
                    //    articals.seizureno = cm_obj.seizureno;
                    //    articals.seizure_excisable_articles_id = Convert.ToInt32((dr.FindControl("lblArticleId") as Label).Text);

                    //    articals.date_of_destruction = (dr.FindControl("AvalueDateFixedForDestruction") as TextBox).Text;
                    //    articals.actioncompleted = (dr.FindControl("ddlAction") as DropDownList).SelectedItem.ToString();
                    //    cm_obj.articals.Add(articals);

                    //}
                    //cm_obj.vehicals = new List<cm_seiz_vehicledetails>();
                    //foreach (GridViewRow dr in GridView1.Rows)
                    //{
                    //    cm_seiz_vehicledetails vehicals = new cm_seiz_vehicledetails();
                    //    vehicals.seizureno = cm_obj.seizureno;
                    //    vehicals.seizure_vehicledetails_id = Convert.ToInt32((dr.FindControl("lblVehicleId") as Label).Text);
                    //    vehicals.actioncompleted = (dr.FindControl("ddlAction") as DropDownList).SelectedItem.ToString();
                    //    vehicals.auction_or_releasedate = (dr.FindControl("auction_or_releasedate") as TextBox).Text;
                    //    vehicals.auctionreleaseamount = (dr.FindControl("txtAmountRecieved") as TextBox).Text;
                    //    vehicals.infavourof = (dr.FindControl("txtInFavourOf") as TextBox).Text;

                    //    vehicals.challan_date = (dr.FindControl("ChallanDate") as TextBox).Text;
                    //    vehicals.challan_no = (dr.FindControl("txtDepositedVideChallanNo") as TextBox).Text;
                    //    cm_obj.vehicals.Add(vehicals);

                    //}
                    string tempTableId = Session["tableId"]?.ToString() ?? string.Empty;
                    if (Session["rtype"].ToString() == "0" && string.IsNullOrEmpty(tempTableId))
                        val = BL_cm_seiz_Excisecom.InsertSeiz_Excisecom(cm_obj);
                    else
                    {
                        cm_obj.seizure_excisecom_id = Convert.ToInt32(Session["tableId"].ToString());
                        val = BL_cm_seiz_Excisecom.Update(cm_obj);
                    }
                    if (val == true)
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("~/ExciseCommissionerOrderDetailsList");
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
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/ExciseCommissionerOrderDetailsList");
        }

        protected void btnSeizure_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            List<Reportmaster> reports = new List<Reportmaster>();
            reports = BL_WorkFlow.GetReports(Session["UserID"].ToString());
            Session["ReportId"] = "CM_SEIZURE_DETAILS";
            Session["SeizureNo"] = Session["seizureNo"].ToString();
            Session["Raidby"] = Session["RaidBy"];
            Session["UserID"] = Session["UserID"].ToString();
            Session["seizureno"] = Session["seizureNo"].ToString();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('ReportPage' ,'_blank');", true);

        }

        protected void btnFIR_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            List<Reportmaster> reports = new List<Reportmaster>();
            reports = BL_WorkFlow.GetReports(Session["UserID"].ToString());
            Session["ReportId"] = "CM_SEIZURE_DETAILS";
            Session["SeizureNo"] = Session["seizureNo"].ToString();
            Session["Raidby"] = Session["RaidBy"];
            Session["UserID"] = Session["UserID"].ToString();
            Session["seizureno"] = Session["seizureNo"].ToString();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('ReportPage' ,'_blank');", true);

        }

        protected void btnChargeSheet_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            List<Reportmaster> reports = new List<Reportmaster>();
            reports = BL_WorkFlow.GetReports(Session["UserID"].ToString());
            Session["ReportId"] = "CM_SEIZURE_DETAILS";
            Session["SeizureNo"] = Session["seizureNo"].ToString();
            Session["Raidby"] = Session["RaidBy"];
            Session["UserID"] = Session["UserID"].ToString();
            Session["seizureno"] = Session["seizureNo"].ToString();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('ReportPage' ,'_blank');", true);

        }

        protected void btnBail_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            List<Reportmaster> reports = new List<Reportmaster>();
            reports = BL_WorkFlow.GetReports(Session["UserID"].ToString());
            Session["ReportId"] = "CM_SEIZURE_DETAILS";
            Session["SeizureNo"] = Session["seizureNo"].ToString();
            Session["Raidby"] = Session["RaidBy"];
            Session["UserID"] = Session["UserID"].ToString();
            Session["seizureno"] = Session["seizureNo"].ToString();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('ReportPage' ,'_blank');", true);

        }
        protected void grdExcisableArticle_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string action = (e.Row.FindControl("lblAction") as Label).Text;
                DropDownList ddlAction = (e.Row.FindControl("ddlAction") as DropDownList);
                ddlAction.Items.Insert(0, new ListItem("Select"));
                ddlAction.Items.Insert(1, new ListItem("Destruction"));
                ddlAction.Items.Insert(2, new ListItem("Acquittal"));

                if (!string.IsNullOrEmpty(action) && action != "Pending")
                    ddlAction.Items.FindByValue(action).Selected = true;
                string destruction = (e.Row.FindControl("lbldate_of_destruction") as Label).Text.Replace("01/01/1990 00:00:00", "");
                TextBox tdestruction = (e.Row.FindControl("AvalueDateFixedForDestruction") as TextBox);
                if (!string.IsNullOrEmpty(destruction))
                {
                    destruction = DateTime.Parse(destruction).ToString("yyyy-MM-dd");
                    tdestruction.Text = destruction.ToString();
                }
              
                    e.Row.Enabled = false;
                    //ddlAction.Attributes.Add("Disable", "Disable");
                    //tdestruction.Attributes.Add("Disable", "Disable");
                
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string action = (e.Row.FindControl("lblAction") as Label).Text;
                DropDownList ddlAction = (e.Row.FindControl("ddlAction") as DropDownList);
                ddlAction.Items.Insert(0, new ListItem("Select"));
                ddlAction.Items.Insert(1, new ListItem("Auction"));
                ddlAction.Items.Insert(2, new ListItem("Release"));
                if (!string.IsNullOrEmpty(action) && action != "Pending")
                    ddlAction.Items.FindByValue(action).Selected = true;
                string destruction = (e.Row.FindControl("lblauction_or_releasedate") as Label).Text.Replace("01/01/1990 00:00:00", ""); ;
                TextBox tdestruction = (e.Row.FindControl("auction_or_releasedate") as TextBox);
                if (!string.IsNullOrEmpty(destruction))
                {
                    destruction = DateTime.Parse(destruction).ToString("yyyy-MM-dd");
                    tdestruction.Text = destruction.ToString();
                }
                //chalana date
                string ChallanDate = (e.Row.FindControl("lblChallanDate") as Label).Text.Replace("01/01/1990 00:00:00", ""); ;
                TextBox tChallanDate = (e.Row.FindControl("ChallanDate") as TextBox);
                if (!string.IsNullOrEmpty(ChallanDate))
                {
                    ChallanDate = DateTime.Parse(ChallanDate).ToString("yyyy-MM-dd");
                    tChallanDate.Text = ChallanDate.ToString();
                }
               
                    e.Row.Enabled = false;
                    //ddlAction.Attributes.Add("Disable", "Disable");
                    //tdestruction.Attributes.Add("Disable", "Disable");
               
            }
        }



        protected void grdApparatusView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string action = (e.Row.FindControl("lblAction") as Label).Text;
                DropDownList ddlAction = (e.Row.FindControl("ddlAction") as DropDownList);
                ddlAction.Items.Insert(0, new ListItem("Select"));
                ddlAction.Items.Insert(1, new ListItem("Destruction"));
                ddlAction.Items.Insert(2, new ListItem("Acquittal"));

                if (!string.IsNullOrEmpty(action) && action != "Pending")
                    ddlAction.Items.FindByValue(action).Selected = true;
                string destruction = (e.Row.FindControl("lbldate_of_destruction") as Label).Text.Replace("01/01/1990 00:00:00", "");
                TextBox tdestruction = (e.Row.FindControl("AvalueDateFixedForDestruction") as TextBox);
                if (!string.IsNullOrEmpty(destruction))
                {
                    destruction = DateTime.Parse(destruction).ToString("yyyy-MM-dd");
                    tdestruction.Text = destruction.ToString();
                }
                
                    e.Row.Enabled = false;
                    //ddlAction.Attributes.Add("Disable", "Disable");
                    //tdestruction.Attributes.Add("Disable", "Disable");
                
            }
        }

        protected void grdPropertyView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string action = (e.Row.FindControl("lblAction") as Label).Text;
                DropDownList ddlAction = (e.Row.FindControl("ddlAction") as DropDownList);
                ddlAction.Items.Insert(0, new ListItem("Select"));
                ddlAction.Items.Insert(1, new ListItem("Destruction"));
                ddlAction.Items.Insert(2, new ListItem("Acquittal"));

                if (!string.IsNullOrEmpty(action) && action != "Pending")
                    ddlAction.Items.FindByValue(action).Selected = true;
                string destruction = (e.Row.FindControl("lbldate_of_destruction") as Label).Text.Replace("01/01/1990 00:00:00", "");
                TextBox tdestruction = (e.Row.FindControl("AvalueDateFixedForDestruction") as TextBox);
                if (!string.IsNullOrEmpty(destruction))
                {
                    destruction = DateTime.Parse(destruction).ToString("yyyy-MM-dd");
                    tdestruction.Text = destruction.ToString();
                }
               
                    e.Row.Enabled = false;
                    //ddlAction.Attributes.Add("Disable", "Disable");
                    //tdestruction.Attributes.Add("Disable", "Disable");
               
            }
        }

        protected void grdMoneyListView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string action = (e.Row.FindControl("lblAction") as Label).Text;
                DropDownList ddlAction = (e.Row.FindControl("ddlAction") as DropDownList);
                ddlAction.Items.Insert(0, new ListItem("Select"));
                ddlAction.Items.Insert(1, new ListItem("Destruction"));
                ddlAction.Items.Insert(2, new ListItem("Acquittal"));

                if (!string.IsNullOrEmpty(action) && action != "Pending")
                    ddlAction.Items.FindByValue(action).Selected = true;
                string destruction = (e.Row.FindControl("lbldate_of_destruction") as Label).Text.Replace("01/01/1990 00:00:00", "");
                TextBox tdestruction = (e.Row.FindControl("AvalueDateFixedForDestruction") as TextBox);
                if (!string.IsNullOrEmpty(destruction))
                {
                    destruction = DateTime.Parse(destruction).ToString("yyyy-MM-dd");
                    tdestruction.Text = destruction.ToString();
                }
                
                    e.Row.Enabled = false;
                    //ddlAction.Attributes.Add("Disable", "Disable");
                    //tdestruction.Attributes.Add("Disable", "Disable");
               
            }
        }
    }
}