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
    public partial class NOCApplicationForm : System.Web.UI.Page
    {

        DataTable dt = new DataTable();
        DataTable depot = new DataTable();
        List<WorkFlow> workflow = new List<WorkFlow>();
        // static int currentlevel=0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    txtNOCDate.Text = DateTime.Now.ToShortDateString();
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        Session["rolename"] = user.role_name;
                        Session["partycode"] = user.party_code;
                        List<Product_Master> product = new List<Product_Master>();
                        product = BL_ProductMaster.GetProductMasterList(user.user_id);
                        int value = BL_RawMaterialReceipt.GetGrainData(Session["partycode"].ToString());
                        if (value == 1)
                        {
                            tender.Text = "Tender Number/Permit Number/PO No";
                            pono.Text = "Export Order Number";
                            lblDepot.Text = "Product Name";
                            if (gridDepotData.Rows.Count > 0)
                            {
                                gridDepotData.HeaderRow.Cells[0].Text = "Product Name";
                            }

                        }
                        else
                        {
                            tender.Text = "Tender Number";
                            pono.Text = "PO Number";
                            lblDepot.Text = "Depot Name";
                            if (gridDepotData.Rows.Count > 0)
                            {
                                gridDepotData.HeaderRow.Cells[0].Text = "Depot Name";
                            }
                        }
                        conformation.Value = user.user_id;
                        fisicalyear.Value = user.financial_year;
                        partycode.Value = user.party_code;
                        rolename.Value = user.role_name;
                        approverremaks.Visible = false;
                        btnReject.Visible = false;
                        btnApprove.Visible = false;
                        btnReferBack.Visible = false;
                        valieddate12.Visible = false;
                        Image2.Visible = false;
                        txtValiedDate.ReadOnly = true;
                        // CalendarExtender1.SelectedDate = DateTime.Now;
                        CalendarExtender1.StartDate = DateTime.Now;
                       // CalendarExtender2.StartDate = DateTime.Now;
                        if (ViewState["DepotRecords"] == null)
                        {
                            depot.Columns.Add("Depot_Name");
                            depot.Columns.Add("QTY");
                            depot.Columns.Add("AppQTY");
                            depot.Columns.Add("Depot_id");
                            depot.Columns.Add("id");
                            ViewState["DepotRecords"] = depot;
                        }
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
                        if (user.role_name == "Applicant" || user.party_type == "ALL" || user.party_type == "All")
                        {
                            List<CustomerDetails> customers = new List<CustomerDetails>();
                            customers = BL_User_Mgnt.GetCustomers(user.party_code);
                            noc_id.Value = BL_NOC_Details.GetMax();
                            txtNOCNumber.Text = BL_NOC_Details.GetPartyMax(user.party_code, fisicalyear.Value);
                            if (user.party_type == "Distillery Unit" || user.party_type.Trim() == "ENA Distillery Unit")
                            {
                                int value1 = BL_RawMaterialReceipt.GetGrainData(Session["partycode"].ToString());
                                if (value1 == 1)
                                {
                                    var list1 = from s in product
                                                where s.product_name != "Grain" && s.product_type_code == "6"
                                                select s;
                                    ddlNOCFor.DataSource = list1.ToList();
                                    ddlNOCFor.DataTextField = "Product_name";
                                    ddlNOCFor.DataValueField = "Product_code";
                                    ddlNOCFor.DataBind();
                                    ddlNOCFor.Items.Insert(0, "Select");
                                    //  
                                    var list2 = from s in customers
                                                where s.party_code == user.party_code
                                                select s;
                                    ddlCustomerName.DataSource = list2.ToList();
                                    ddlCustomerName.DataTextField = "cust_name";
                                    ddlCustomerName.DataValueField = "customer_id";
                                    ddlCustomerName.DataBind();
                                    ddlCustomerName.Items.Insert(0, "Select");
                                    dummyDepotDatatable.Visible = false;
                                    EnaTable.Visible = true;

                                }
                                else
                                {
                                    var list1 = from s in product
                                                where  s.product_type_code == "2" ||s.product_code == "3"
                                                select s;
                                    ddlNOCFor.DataSource = list1.ToList();
                                    ddlNOCFor.DataTextField = "Product_name";
                                    ddlNOCFor.DataValueField = "Product_code";
                                    ddlNOCFor.DataBind();
                                    ddlNOCFor.Items.Insert(0, "Select");
                                    //  
                                    var list2 = from s in customers
                                                where s.party_code == user.party_code
                                                select s;
                                    ddlCustomerName.DataSource = list2.ToList();
                                    ddlCustomerName.DataTextField = "cust_name";
                                    ddlCustomerName.DataValueField = "customer_id";
                                    ddlCustomerName.DataBind();
                                    ddlCustomerName.Items.Insert(0, "Select");
                                    dummyDepotDatatable.Visible = true;
                                    EnaTable.Visible = false;
                                }
                            }
                            else if (user.party_type == "Sugar Mill")
                            {
                                var list1 = from s in product
                                            where s.product_type_name == "Molasses"
                                            select s;
                                ddlNOCFor.DataSource = list1.ToList();
                                ddlNOCFor.DataTextField = "Product_name";
                                ddlNOCFor.DataValueField = "Product_code";
                                ddlNOCFor.DataBind();
                                ddlNOCFor.Items.Insert(0, "Select");
                                var list2 = from s in customers
                                            where s.party_code == user.party_code
                                            select s;
                                ddlCustomerName.DataSource = list2.ToList();
                                ddlCustomerName.DataTextField = "cust_name";
                                ddlCustomerName.DataValueField = "customer_id";
                                ddlCustomerName.DataBind();
                                ddlCustomerName.Items.Insert(0, "Select");
                                dummyDepotDatatable.Visible = true;
                                EnaTable.Visible = false;
                            }
                            else
                            {
                                ddlNOCFor.DataSource = product.ToList();
                                ddlNOCFor.DataTextField = "Product_name";
                                ddlNOCFor.DataValueField = "Product_code";
                                ddlNOCFor.DataBind();
                                ddlNOCFor.Items.Insert(0, "Select");
                                customers = BL_User_Mgnt.GetCustomers(user.party_code);
                                ddlCustomerName.DataSource = customers;
                                ddlCustomerName.DataTextField = "cust_name";
                                ddlCustomerName.DataValueField = "customer_id";
                                ddlCustomerName.DataBind();
                                ddlCustomerName.Items.Insert(0, "Select");
                                dummyDepotDatatable.Visible = true;
                                EnaTable.Visible = false;

                            }
                            gridDepotData.Columns[2].Visible = false;

                            if (Session["rtype"].ToString() != "0")
                            {
                                NOC_Details noc = new NOC_Details();
                                string vv = Session["NOC_ID"].ToString();
                                noc = BL_NOC_Details.GetDetails(Session["NOC_ID"].ToString(), Session["nocfinancial_year"].ToString(), Session["nocparty_code"].ToString());
                                ddlNOCFor.SelectedValue = noc.noc_for;
                                fisicalyear.Value = noc.financial_year;
                                if (noc.valid_upto != null)
                                    CalendarExtender1.SelectedDate = Convert.ToDateTime(noc.valid_upto);
                                txtNOCDate.Text = noc.nocdate;
                                noc_id.Value = noc.noc_id;
                                txtNOCNumber.Text = noc.req_nocno;
                                partycode.Value = noc.party_code;
                                ddlCustomerName.SelectedValue = noc.customer_id;
                                ddlCustomerName_SelectedIndexChanged(sender, null);
                                ddlNumberTypes.SelectedValue = noc.number_type;
                                ddlNumberTypes_SelectedIndexChanged(sender, e);
                                if (noc.number_issuedate != "" && noc.number_issuedate != null)
                                {
                                    txtissuedate.Text = noc.number_issuedate;
                                }
                                txtPONumber.Text = noc.pono;
                                txtTenderNumber.Text = noc.tenderno;
                                txtRemarks.Text = noc.remarks;
                                Session["recordstatus"] = noc.record_status;
                                txtValiedDate.Text = noc.valid_upto;
                                txtvalieddate1.Value = noc.valid_upto;
                                //if (noc.valid_upto != "")
                                //    CalendarExtender1.SelectedDate =Convert.ToDateTime(noc.valid_upto);
                                btnReferBack.Visible = false;
                                if (user.role_name == "Applicant" && noc.record_status != "I")
                                    gridDepotData.Columns[2].Visible = false;
                                else
                                     if (user.role_name.Trim() == "Deputy Commissioner" || user.role_name.Trim() == "Commissioner" || noc.record_status == "I" || (user.role_name.Trim() == "Assistant Commissioner" && noc.record_status == "B"))
                                    gridDepotData.Columns[2].Visible = true;
                                if (noc.depot != null)
                                {
                                    for (int i = 0; i < noc.depot.Count; i++)
                                    {
                                        if (i == 0)
                                            dummyDepotDatatable.Visible = false;
                                        EnaTable.Visible = false;
                                        depot = (DataTable)ViewState["DepotRecords"];
                                        if (noc.depot[i].qty <= 0)
                                        {
                                            depot.Rows.Add(noc.depot[i].Depot_name, noc.depot[i].reqqty, noc.depot[i].reqqty, noc.depot[i].Depot_id, "0");
                                        }
                                        else
                                        {
                                            depot.Rows.Add(noc.depot[i].Depot_name, noc.depot[i].reqqty, noc.depot[i].qty, noc.depot[i].Depot_id, "0");
                                        }

                                        gridDepotData.DataSource = depot;
                                        gridDepotData.DataBind();

                                    }
                                    int value2 = BL_RawMaterialReceipt.GetGrainData(Session["partycode"].ToString());
                                    if (value2 == 1)
                                    {
                                        lblDepot.Text = "Product Name";
                                        if (gridDepotData.Rows.Count > 0)
                                        {
                                            gridDepotData.HeaderRow.Cells[0].Text = "Product Name";
                                        }

                                    }
                                    else
                                    {
                                        lblDepot.Text = "Depot Name";
                                        if (gridDepotData.Rows.Count > 0)
                                        {
                                            gridDepotData.HeaderRow.Cells[0].Text = "Depot Name";
                                        }
                                    }
                                }
                                for (int i = 0; i < noc.docs.Count; i++)
                                {
                                    if (i == 0)
                                        dummytable.Visible = false;
                                    EnaTable.Visible = false;
                                    dt = (DataTable)ViewState["Records"];
                                    dt.Rows.Add("1", noc.docs[i].doc_name, noc.docs[i].description, noc.docs[i].doc_path, noc.docs[i].id, noc.docs[i].user_id);
                                    grdAdd.DataSource = dt;
                                    grdAdd.DataBind();

                                    Doc_id++;

                                }
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    string u = (grdAdd.Rows[i].FindControl("lbluser_id") as Label).Text;
                                    if ((grdAdd.Rows[i].FindControl("lblStatus") as Label).Text == "1" && user.user_id != (grdAdd.Rows[i].FindControl("lbluser_id") as Label).Text && (noc.record_status == "Y" || noc.record_status == "I"))
                                    {
                                        //(grdAdd.Rows[i].FindControl("ImageButton2") as ImageButton).Visible = false;
                                        (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                                    }
                                    else
                                    {
                                        (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = true;
                                    }
                                }

                                if (Session["UserID"].ToString() == "Admin")
                                {
                                    ddlNOCFor.Enabled = false;
                                    txtNOCDate.ReadOnly = true;

                                    // txtNOCNumber.Text = noc.noc_id;
                                    ddlCustomerName.Enabled = false;
                                    txtPONumber.ReadOnly = true;
                                    txtTenderNumber.ReadOnly = true;
                                    txtRemarks.ReadOnly = true;
                                    //foreach (GridViewRow dr1 in grdAdd.Rows)
                                    //{
                                    //    ImageButton btn = dr1.FindControl("ImageButton1") as ImageButton;
                                    //    btn.Visible = false;
                                    //}
                                    gridDepotData.Columns[gridDepotData.Columns.Count - 1].Visible = false;
                                    foreach (GridViewRow dr1 in gridDepotData.Rows)
                                    {

                                        TextBox btn = dr1.FindControl("txtQTY") as TextBox;
                                        btn.ReadOnly = true;
                                    }
                                    foreach (GridViewRow dr1 in grdAdd.Rows)
                                    {
                                        ImageButton btn = dr1.FindControl("ImageButton1") as ImageButton;
                                        btn.Visible = false;
                                    }
                                    depotdiv.Visible = false;
                                    docs.Visible = false;
                                    btnCancel.Visible = true;
                                    btnupdate.Visible = true;
                                    Image2.Visible = true;
                                    btnSaveasDraft.Visible = false;
                                    btnSubmit.Visible = false;
                                    List<All_Approvals> approvals = new List<All_Approvals>();
                                    approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), Session["NOC_ID"].ToString(), "NOC");
                                    var list4 = (from s in approvals
                                                 where s.user_id == "Admin"
                                                 select s);
                                    grdApprovalDetails.DataSource = list4.ToList();
                                    approv.Visible = true;
                                    grdApprovalDetails.DataBind();
                                    btnReject.Visible = false;
                                    approverremaks.Visible = false;
                                    btnApprove.Visible = false;
                                    valieddate12.Visible = true;
                                }
                                if ((Session["rtype"].ToString() == "1"))
                                {
                                    workflow = new List<WorkFlow>();
                                    workflow = BL_WorkFlow.ApprovelLevels("NOC", "113");
                                    var aplevel = (from s in workflow
                                                   where s.user_registration_id == user.id.ToString()
                                                   select s);
                                    currentlevel.Value = "0";

                                    if (aplevel.ToList().Count > 0 || user.role_name == "Applicant")
                                    {
                                        if (user.role_name != "Applicant")
                                            currentlevel.Value = aplevel.ToList()[0].approver_level;

                                        ddlNOCFor.Enabled = false;
                                        txtNOCDate.ReadOnly = true;

                                        // txtNOCNumber.Text = noc.noc_id;
                                        ddlCustomerName.Enabled = false;
                                        ddlNumberTypes.Enabled = false;
                                        txtissuedate.ReadOnly = true;
                                        Image4.Visible = false;
                                        txtPONumber.ReadOnly = true;
                                        txtTenderNumber.ReadOnly = true;
                                        txtRemarks.ReadOnly = true;
                                        //foreach (GridViewRow dr1 in grdAdd.Rows)
                                        //{
                                        //    ImageButton btn = dr1.FindControl("ImageButton1") as ImageButton;
                                        //    btn.Visible = false;
                                        //}
                                        gridDepotData.Columns[gridDepotData.Columns.Count - 1].Visible = false;
                                        foreach (GridViewRow dr1 in gridDepotData.Rows)
                                        {

                                            TextBox btn = dr1.FindControl("txtQTY") as TextBox;
                                            btn.ReadOnly = true;
                                        }
                                        foreach (GridViewRow dr1 in grdAdd.Rows)
                                        {
                                            ImageButton btn = dr1.FindControl("ImageButton1") as ImageButton;
                                            btn.Visible = false;
                                        }
                                        depotdiv.Visible = false;
                                        docs.Visible = false;
                                        btnCancel.Visible = false;
                                        btnSaveasDraft.Visible = false;
                                        btnSubmit.Visible = false;
                                        List<All_Approvals> approvals = new List<All_Approvals>();
                                        approvals = BL_All_Approvals.GetApprovals(Session["UserID"].ToString(), Session["NOC_ID"].ToString(), "NOC");
                                        var list4 = (from s in approvals
                                                     where s.financial_year == Session["nocfinancial_year"].ToString() && s.party_code==noc.party_code
                                                     select s);
                                        grdApprovalDetails.DataSource = list4.ToList();
                                        approv.Visible = false;
                                        grdApprovalDetails.DataBind();
                                        btnReject.Visible = false;
                                        approverremaks.Visible = false;
                                        btnApprove.Visible = false;
                                        valieddate12.Visible = false;
                                        if (Convert.ToInt32(currentlevel.Value) == 2 && noc.approverlevel == 1 && noc.record_status != "R")
                                        {
                                            valieddate12.Visible = true;
                                            Image2.Visible = true;
                                            //  gridDepotData.Columns[2].Visible = true;
                                            foreach (GridViewRow dr1 in gridDepotData.Rows)
                                            {

                                                TextBox btn = dr1.FindControl("txtQTY") as TextBox;
                                                btn.ReadOnly = false;
                                            }
                                        }
                                        else if ((Convert.ToInt32(currentlevel.Value) != 2 && noc.approverlevel == 0) || (Convert.ToInt32(currentlevel.Value) != 3 && noc.approverlevel == 0))
                                        {

                                            valieddate12.Visible = false;
                                        }
                                        else
                                        {

                                            txtValiedDate.ReadOnly = true;
                                            Image2.Visible = false;
                                            txtValiedDate.Enabled = false;
                                            valieddate12.Visible = true;
                                        }
                                        if (approvals.Count > 0)
                                        {
                                            approv.Visible = true;
                                            // valieddate12.Visible = true;
                                        }


                                        if ((user.party_type == "All" || user.party_type == "ALL"))
                                        {


                                            //  int workal = Convert.ToInt32(aplevel.ToList()[0].approver_level);
                                            // currentlevel = workal.ToString();
                                            if (noc.approverlevel == (Convert.ToInt32(currentlevel.Value) - 1) && noc.record_status != "N" && noc.record_status != "R")
                                            {
                                                approverremaks.Visible = true;
                                                btnReject.Visible = true;
                                                btnApprove.Visible = true;
                                                docs.Visible = true;

                                            }
                                            if (Convert.ToInt32(currentlevel.Value) > 1 && noc.approverlevel > 0)
                                            {
                                                if (noc.record_status != "R" && Convert.ToInt32(currentlevel.Value) != noc.approverlevel)
                                                {
                                                    if (noc.record_status != "A" && noc.record_status != "I")
                                                        btnReferBack.Visible = true;
                                                    if (Convert.ToInt32(currentlevel.Value) == 3)
                                                        btnReject.Visible = false;
                                                    if (Convert.ToInt32(currentlevel.Value) == 3 && noc.record_status == "B")
                                                        btnReferBack.Visible = false;
                                                }
                                            }

                                        }


                                    }
                                    else
                                    {

                                        Response.Redirect("~/SCMDashBoard");

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

        int depot_id = 1;
        protected void DepotAdd(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                dummyDepotDatatable.Visible = false;
                EnaTable.Visible = false;
                depot = (DataTable)ViewState["DepotRecords"];
                depot.Rows.Add(txtDepotName.Text, txtQuantity.Text, txtQuantity.Text, "0", depot_id);
                gridDepotData.DataSource = depot;
                gridDepotData.DataBind();

                depot_id++;
                int value = BL_RawMaterialReceipt.GetGrainData(Session["partycode"].ToString());
                if (value == 1)
                {
                    lblDepot.Text = "Product Name";
                    if (gridDepotData.Rows.Count > 0)
                    {
                        gridDepotData.HeaderRow.Cells[0].Text = "Product Name";
                    }

                }
                else
                {
                    lblDepot.Text = "Depot Name";
                    if (gridDepotData.Rows.Count > 0)
                    {
                        gridDepotData.HeaderRow.Cells[0].Text = "Depot Name";
                    }
                }
                txtDepotName.Text = "";
                txtQuantity.Text = "";
            }

        }
        static string deleteddepots = "";
        protected void DepotRemove_Click(object sender, EventArgs e)
        {

            ImageButton lb = (ImageButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            DataTable dt2 = (DataTable)ViewState["DepotRecords"];
            ViewState["CurrentTable"] = dt2;
            int rowID = gvRow.RowIndex;
            if (deleteddepots != "")
                deleteddepots = deleteddepots + "_" + dt2.Rows[rowID]["Depot_id"].ToString();
            else
                deleteddepots = dt2.Rows[rowID]["Depot_id"].ToString();
            DataTable dt1 = ViewState["DepotRecords"] as DataTable;
            dt1.Rows[rowID].Delete();
            ViewState["DepotRecords"] = dt1;
            gridDepotData.DataSource = dt1;
            gridDepotData.DataBind();
            depot_id--;
            if (dt1.Rows.Count < 1)
            {
                int value2 = BL_RawMaterialReceipt.GetGrainData(Session["partycode"].ToString());
                if (value2 == 1)
                {
                    dummyDepotDatatable.Visible = false;
                    EnaTable.Visible = true;
                }
                else
                {
                    dummyDepotDatatable.Visible = true;
                    EnaTable.Visible = false;
                }
            }
        }
        int Doc_id = 1;
        protected void UploadFile(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (idupDocument.HasFile)
                {

                    dummytable.Visible = false;
                    EnaTable.Visible = false;
                    string fileName = Path.GetFileName(idupDocument.PostedFile.FileName);
                    //fileName.Replace)
                    //  string[] filetype = fileName.Replace("'","").Split('.');
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
                //File.Delete(Server.MapPath("~/Eascm_Docs/" + Path.GetFileName(filePath)));
                //FileInfo fInfoEvent;
                //fInfoEvent = new FileInfo(Path.GetFileName(filePath));
                //fInfoEvent.Delete();
                string a = Session["rtype"].ToString();
                string v = Path.GetFileName(filePath).ToString();
                if (Session["rtype"].ToString() != "0")
                {
                    bool value = BL_User_Mgnt.DeletefileEascm("eascm_docs", Session["NOC_ID"].ToString(), v, Session["nocparty_code"].ToString());
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
                        //  (grdAdd.Rows[i].FindControl("ImageButton1") as ImageButton).Visible = false;
                    }
                }
                if (dt1.Rows.Count < 1)
                    dummytable.Visible = true;
                EnaTable.Visible = false;
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
            if (Session["rolename"].ToString() == "Applicant" || Session["UserID"].ToString() == "Admin")
            {
                Session["UserID"] = Session["UserID"];
                Response.Redirect("NOCApplicationList");
            }
            else
            {
                if (Session["formid"].ToString() == "P")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("NOC_P");
                }
                if (Session["formid"].ToString() == "A")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("NOC_A");
                }
                if (Session["formid"].ToString() == "B")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("NOC_B");
                }
                if (Session["formid"].ToString() == "I")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("NOC_I");
                }
            }
        }
        protected void btnNOCApplication_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("NOCApplicationList");

        }

        protected void btnNOCIssue_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("NOCIssueList");
        }

        protected void ddlCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlCustomerName.SelectedValue != "Select")
            {
                CustomerDetails cust = new CustomerDetails();
                cust = BL_User_Mgnt.GetCustomerDetails(ddlCustomerName.SelectedValue);
                txtCustomerAddress.Text = cust.cust_address;
                txtMobileNumber.Text = cust.cust_mobile;
                txtEmailID.Text = cust.cust_email;
                txtDistrict.Text = cust.district_name;
                txtThana.Text = cust.thana_name;
                txtState.Text = cust.state_name;
                txtPINCode.Text = cust.pincode;
            }

        }

        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (gridDepotData.Rows.Count > 0 && grdAdd.Rows.Count > 0)
                {
                    NOC_Details noc = new NOC_Details();
                    noc.noc_for = ddlNOCFor.SelectedValue;
                    noc.noc_id = noc_id.Value;
                    noc.req_nocno = txtNOCNumber.Text;
                    noc.nocdate = txtNOCDate.Text;
                    noc.tenderno = txtTenderNumber.Text;
                    noc.pono = txtPONumber.Text;
                    noc.remarks = txtRemarks.Text;
                    noc.number_issuedate = issuedate.Value;
                    noc.number_issuedate = txtissuedate.Text;
                    noc.number_type = ddlNumberTypes.SelectedValue;
                    noc.depot = new List<NOC_Depot>();
                    noc.record_status = "N";
                    noc.user_id = Session["UserID"].ToString();
                    noc.party_code = partycode.Value;
                    noc.financial_year = fisicalyear.Value;
                    noc.customer_id = ddlCustomerName.SelectedValue;
                    noc.noc_total_qty = 0;
                    depot = ViewState["DepotRecords"] as DataTable;
                    int j = 0;
                    foreach (DataRow dr in depot.Rows)
                    {
                        NOC_Depot dep = new NOC_Depot();
                        dep.Depot_name = dr["Depot_Name"].ToString();
                        dep.qty = Convert.ToDouble(dr["qty"].ToString());
                        dep.reqqty = Convert.ToDouble(dr["qty"].ToString());
                        dep.Depot_id = dr["Depot_id"].ToString();
                        noc.noc_total_qty = noc.noc_total_qty + Convert.ToDouble(dep.qty);
                        noc.depot.Add(dep);
                        j++;
                    }
                    noc.depot[0].deleted_id = deleteddepots;
                    noc.docs = new List<EASCM_DOCS>();
                    dt = ViewState["Records"] as DataTable;
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        EASCM_DOCS doc = new EASCM_DOCS();
                        doc.doc_name = dr["Doc_Name"].ToString();
                        doc.doc_path = (grdAdd.Rows[i].FindControl("lblFilePath") as Label).Text;
                        doc.description = (grdAdd.Rows[i].FindControl("lblDiscriptione") as Label).Text;
                        noc.docs.Add(doc);
                        i++;
                    }
                    string val = "";
                    noc.financial_year = fisicalyear.Value;
                    if (Session["rtype"].ToString() == "0")
                    {
                        val = BL_NOC_Details.Insert(noc);
                    }
                    else
                    {
                        noc.noc_id = Session["Noc_id"].ToString();
                        val = BL_NOC_Details.Update(noc);
                    }
                    if (val == "0")
                    {

                        Response.Redirect("NOCApplicationList");

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
                else
                {
                    if (gridDepotData.Rows.Count == 0)
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append("Please click on ADD button to save the depot details");
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    }
                    if (grdAdd.Rows.Count == 0)
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append("Please attach documents");
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                btnSubmit.Enabled = false;
                if (gridDepotData.Rows.Count > 0 && grdAdd.Rows.Count > 0)
                {
                    NOC_Details noc = new NOC_Details();
                    noc.noc_for = ddlNOCFor.SelectedValue;
                    noc.noc_id = noc_id.Value;
                    noc.req_nocno = txtNOCNumber.Text;
                    noc.nocdate = txtNOCDate.Text;
                    noc.tenderno = txtTenderNumber.Text;
                    noc.pono = txtPONumber.Text;
                    noc.remarks = txtRemarks.Text;
                    noc.number_issuedate = issuedate.Value;
                    noc.number_issuedate = txtissuedate.Text;
                    noc.number_type = ddlNumberTypes.SelectedValue;
                    noc.depot = new List<NOC_Depot>();
                    noc.record_status = "Y";
                    noc.user_id = Session["UserID"].ToString();
                    noc.party_code = partycode.Value;
                    noc.customer_id = ddlCustomerName.SelectedValue;
                    noc.noc_total_qty = 0;
                    depot = ViewState["DepotRecords"] as DataTable;
                    int j = 0;
                    foreach (DataRow dr in depot.Rows)
                    {
                        NOC_Depot dep = new NOC_Depot();
                        dep.Depot_name = dr["Depot_Name"].ToString();
                        dep.qty = Convert.ToDouble(dr["qty"].ToString());
                        dep.reqqty = Convert.ToDouble(dr["qty"].ToString());
                        dep.Depot_id = dr["Depot_id"].ToString();
                        noc.noc_total_qty = noc.noc_total_qty + Convert.ToDouble(dep.qty);
                        noc.depot.Add(dep);
                        j++;
                    }
                    noc.depot[0].deleted_id = deleteddepots;
                    noc.docs = new List<EASCM_DOCS>();
                    dt = ViewState["Records"] as DataTable;
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        EASCM_DOCS doc = new EASCM_DOCS();
                        doc.doc_name = dr["Doc_Name"].ToString();
                        doc.doc_path = (grdAdd.Rows[i].FindControl("lblFilePath") as Label).Text;
                        doc.description = (grdAdd.Rows[i].FindControl("lblDiscriptione") as Label).Text;
                        noc.docs.Add(doc);
                        i++;
                    }
                    string val = "";
                    noc.financial_year = fisicalyear.Value;
                    if (Session["rtype"].ToString() == "0")
                    {
                        val = BL_NOC_Details.Insert(noc);
                    }
                    else
                    {

                        val = BL_NOC_Details.Update(noc);
                    }
                    if (val == "0")
                    {

                        Response.Redirect("NOCApplicationList");

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
                else
                {
                    if (gridDepotData.Rows.Count == 0)
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append("Please click on ADD button to save the depot details");
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    }
                    if (grdAdd.Rows.Count == 0)
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append("Please attach documents");
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    }
                }

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("NOCApplicationList");
        }
        // static string recordstatus = "";
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string val = "";
                NOC_Details noc = new NOC_Details();
                noc.valid_upto = txtvalieddate1.Value;
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                noc.noc_total_qty = 0;
                noc.party_code = Session["nocparty_code"].ToString();
                depot = ViewState["DepotRecords"] as DataTable;
                noc.financial_year = fisicalyear.Value;
                Session["depot"] = depot;
                int j = 0;
                noc.depot = new List<NOC_Depot>();
                foreach (GridViewRow dr in gridDepotData.Rows)
                {
                    NOC_Depot dep = new NOC_Depot();
                    dep.Depot_name = (dr.FindControl("lblDepotName") as Label).Text;
                    dep.qty = Convert.ToDouble((dr.FindControl("txtQTY") as TextBox).Text);
                    ImageButton lb = (dr.FindControl("ImageButton3") as ImageButton);
                    GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
                    dep.Depot_id = lb.CommandArgument;
                    noc.noc_total_qty = noc.noc_total_qty + Convert.ToDouble(dep.qty);
                    noc.depot.Add(dep);
                    j++;

                }
                if (user.role_name.Trim() == "Commissioner" || currentlevel.Value == "3")
                {
                    noc.record_status = "A";
                    noc.noc_status = "Recommended  by " + rolename.Value;
                }

                else if (/*ser.role_name.Trim() == "Deputy Commissioner" ||*/ currentlevel.Value == "2" && Session["recordstatus"].ToString() == "A")
                {
                    noc.record_status = "I";
                    noc.noc_status = "Issued by " + rolename.Value;

                }
                else if (user.role_name.Trim() == "Deputy Commissioner" && txtvalieddate1.Value == "")
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append("Please Select Valid date upto");
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                    return;
                }
                else
                {
                    noc.record_status = "Y";
                    noc.noc_status = "Recommended by " + rolename.Value;
                }
                noc.user_id = Session["UserID"].ToString();
                noc.approverlevel = Convert.ToInt32(currentlevel.Value);
                noc.approver_remarks = txtApproverComment.Text;
                noc.noc_id = noc_id.Value;
                noc.req_nocno = txtNOCNumber.Text;

                noc.docs = new List<EASCM_DOCS>();
                dt = ViewState["Records"] as DataTable;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {

                    if (dr["Status"].ToString() == "")
                    {
                        EASCM_DOCS doc = new EASCM_DOCS();
                        doc.doc_name = dr["Doc_Name"].ToString();
                        doc.doc_path = dr["Doc_path"].ToString();
                        doc.description = dr["Discription"].ToString();
                        // approversummary = approversummary + "{!}" + doc.doc_name + "{!}" + doc.doc_path + "{!}" + doc.description;
                        noc.docs.Add(doc);

                    }
                    i++;
                }

                int value = BL_Molasses_Allocation.GetDigitalsignature(Session["UserID"].ToString());
                if (value == 1)
                {
                    Session["ID"] = noc.noc_id;
                    Session["approverlevel"] = noc.approverlevel;
                    Session["approver_remarks"] = noc.approver_remarks;
                    Session["noc_status"] = noc.noc_status;
                    Session["record_status"] = noc.record_status;
                    Session["valid_upto"] = noc.valid_upto;
                    Response.Redirect("HtmlPage2.html");
                }

                else
                {
                    val = BL_NOC_Details.Approve(noc);
                    if (val == "0")
                    {
                        Response.Redirect("NOCApplicationList");
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
                string val = "";
                NOC_Details noc = new NOC_Details();
                //  noc.valid_upto = txtvalieddate1.Value;
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                //if (user.role_name.Trim() == "Commissioner")
                //{
                //    noc.record_status = "A";
                //    noc.noc_status = "Approved by " + rolename.Value;
                //}

                //if (user.role_name.Trim() == "Deputy Commissioner" && recordstatus == "A")
                //{
                //    noc.record_status = "I";
                //    noc.noc_status = "Issued by " + rolename.Value;
                //}
                //else
                //{
                noc.record_status = "R";
                noc.party_code = Session["nocparty_code"].ToString();
                noc.noc_status = "Rejected by " + rolename.Value;
                // }
                noc.financial_year = fisicalyear.Value;
                noc.user_id = Session["UserID"].ToString();
                noc.approverlevel = Convert.ToInt32(currentlevel.Value);
                noc.approver_remarks = txtApproverComment.Text;
                noc.noc_id = noc_id.Value;
                noc.req_nocno = txtNOCNumber.Text;
                noc.valid_upto = txtValiedDate.Text;
                val = BL_NOC_Details.Approve(noc);
                if (val == "0")
                {

                    Response.Redirect("NOCApplicationList");

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

        protected void btnReferBack_Click(object sender, EventArgs e)
        {
            string val = "";
            NOC_Details noc = new NOC_Details();
            noc.valid_upto = txtvalieddate1.Value;
            // UserDetails user = new UserDetails();
            //  user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            noc.record_status = "B";
            workflow = new List<WorkFlow>();
            workflow = BL_WorkFlow.ApprovelLevels("NOC", "113");
            noc.approverlevel = Convert.ToInt32(currentlevel.Value) - 1;
            var aplevel = (from s in workflow
                           where s.approver_level == noc.approverlevel.ToString() && s.submodule_code == "NOC"
                           select s);
            noc.noc_status = "Refer Back to " + aplevel.ToList()[0].role_name;
            noc.party_code = Session["nocparty_code"].ToString();
            noc.noc_total_qty = 0;
            depot = ViewState["DepotRecords"] as DataTable;
            noc.financial_year = fisicalyear.Value;
            Session["depot"] = depot;
            int j = 0;
            noc.depot = new List<NOC_Depot>();
            foreach (GridViewRow dr in gridDepotData.Rows)
            {
                NOC_Depot dep = new NOC_Depot();
                dep.Depot_name = (dr.FindControl("lblDepotName") as Label).Text;
                dep.qty = Convert.ToDouble((dr.FindControl("txtQTY") as TextBox).Text);
                ImageButton lb = (dr.FindControl("ImageButton3") as ImageButton);
                GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
                dep.Depot_id = lb.CommandArgument;
                noc.noc_total_qty = noc.noc_total_qty + Convert.ToDouble(dep.qty);
                noc.depot.Add(dep);
                j++;

            }
            noc.user_id = Session["UserID"].ToString();
            noc.approverlevel = noc.approverlevel - 1;
            noc.approver_remarks = txtApproverComment.Text;
            noc.noc_id = noc_id.Value;
            noc.req_nocno = txtNOCNumber.Text;
            noc.financial_year = fisicalyear.Value;
            noc.valid_upto = txtValiedDate.Text;
            noc.docs = new List<EASCM_DOCS>();
            dt = ViewState["Records"] as DataTable;
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {

                if (dr["Status"].ToString() == "")
                {
                    EASCM_DOCS doc = new EASCM_DOCS();
                    doc.doc_name = dr["Doc_Name"].ToString();
                    doc.doc_path = dr["Doc_path"].ToString();
                    doc.description = dr["Discription"].ToString();
                    // approversummary = approversummary + "{!}" + doc.doc_name + "{!}" + doc.doc_path + "{!}" + doc.description;
                    noc.docs.Add(doc);
                }
                i++;
            }
            val = BL_NOC_Details.Approve(noc);
            if (val == "0")
            {
                Response.Redirect("NOCApplicationList");
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

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

                string val = "";
                NOC_Details noc = new NOC_Details();
                noc.valid_upto = txtvalieddate1.Value;
                noc.noc_total_qty = 0;
                noc.user_id = Session["UserID"].ToString();
                noc.financial_year = fisicalyear.Value;
                if (txtvalieddate1.Value != txtValiedDate.Text)
                {
                    noc.record_status = "Valid Date Upto Updated By Admin(From " + txtValiedDate.Text + " To " + txtvalieddate1.Value + ")";
                }
                noc.noc_id = noc_id.Value;
                noc.req_nocno = txtNOCNumber.Text;
                val = BL_NOC_Details.AdminUpdate(noc);
                if (val == "0")
                {
                    Response.Redirect("NOCApplicationList");
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

        protected void txtQTY_TextChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow dr1 in gridDepotData.Rows)
            {
                string requestedqty = (dr1.FindControl("lblQTY") as Label).Text;
                string approvedqty = (dr1.FindControl("txtQTY") as TextBox).Text;
                if(requestedqty!=""&&approvedqty!="")
                {
                    if(Convert.ToDouble(approvedqty)> Convert.ToDouble(requestedqty))
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append("Approved Quantity(BL) must not be grater than the Requested Quantity(BL) ");
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                        (dr1.FindControl("txtQTY") as TextBox).Text = "";
                    }
                }

            }
        }



        protected void ddlNumberTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlNumberTypes.SelectedValue=="T")
            {
                tender.Text = "Tender Number";
            }
            else if(ddlNumberTypes.SelectedValue == "P")
            {
                tender.Text = "Permit Number";
            }
            else if(ddlNumberTypes.SelectedValue == "O")
            {
                tender.Text = "PO No";
            }
        }
        }
    
}