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
    public partial class NOC_I : System.Web.UI.Page
    {
        List<WorkFlow> workflow = new List<WorkFlow>();
        List<NOC> noc = new List<NOC>();
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
                    if (user != null)
                    {
                        Session["rolename"] = user.role_name;
                        Session["Useridd"] = user.id;
                        Session["rolenamecode"] = user.role_name_code;
                        districtcode.Value = user.district_code;
                        rolename.Value = user.role_name;
                        List<Product_Master> product1 = new List<Product_Master>();
                        product1 = BL_ProductMaster.GetProductMasterpartyList(user.party_code);
                        var pro = (from s in product1
                                   where s.product_type_code == "1" || s.product_type_code == "2" || s.product_type_code == "6"
                                   select s);
                        ddproduct.DataSource = pro.ToList();
                        ddproduct.DataTextField = "product_name";
                        ddproduct.DataValueField = "product_code";
                        ddproduct.DataBind();
                        ddproduct.Items.Insert(0, "Select");
                        grdNOCApplicationList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                        if (Session["product3"] != null)
                        {
                            ddproduct.SelectedValue = Session["product3"].ToString();
                            ddproduct_TextChanged(sender, e);

                        }
                        else
                        {
                            if (user.role_name == "Applicant")
                            {
                                Session["party_code"] = user.party_code;
                                List<NOC_Details> noc = new List<NOC_Details>();
                                noc = BL_NOC_Details.GetNOCList();
                                var noc1 = (from s in noc
                                            where s.party_code == user.party_code
                                            orderby s.noc_id descending
                                            select s);
                                grdNOCApplicationList.DataSource = noc1.ToList();
                                grdNOCApplicationList.DataBind();
                                grdNOCApplicationList.Columns[grdNOCApplicationList.Columns.Count - 2].Visible = false;
                                for (int i = 0; i < grdNOCApplicationList.Rows.Count; i++)
                                {
                                    (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = false;

                                }
                            }
                            else if (user.party_type == "All" || user.party_type == "ALL")
                            {
                                btnAddRecord.Visible = false;
                                workflow = new List<WorkFlow>();
                                workflow = BL_WorkFlow.Checkworkflow("NOC", "113", user.role_name_code.ToString(), "", user.id.ToString());
                                if (workflow.Count > 0)
                                {
                                    List<NOC> noc = new List<NOC>();
                                    noc = BL_NOC_Details.GetNOCList1();
                                    if (workflow[0].approver_level == "1")
                                    {
                                        var product = (from s in noc
                                                       where (s.record_status == "A" || s.record_status == "I") && s.district == user.district_code
                                                       orderby s.noc_id descending
                                                       select s);

                                        grdNOCApplicationList.DataSource = product.ToList();
                                        grdNOCApplicationList.DataBind();

                                    }
                                    if (workflow[0].approver_level == "2")
                                    {
                                        var product = (from s in noc
                                                       where s.record_status == "I"
                                                       orderby s.noc_id descending
                                                       select s);

                                        grdNOCApplicationList.DataSource = product.ToList();
                                        grdNOCApplicationList.DataBind();

                                    }
                                    if (workflow[0].approver_level == "3")
                                    {
                                        var product = (from s in noc
                                                       where s.record_status == "I"
                                                       orderby s.noc_id descending
                                                       select s);

                                        grdNOCApplicationList.DataSource = product.ToList();
                                        grdNOCApplicationList.DataBind();

                                    }
                                    grdNOCApplicationList.Columns[grdNOCApplicationList.Columns.Count - 3].Visible = false;
                                    for (int i = 0; i < grdNOCApplicationList.Rows.Count; i++)
                                    {
                                        string record_status = (grdNOCApplicationList.Rows[i].FindControl("lblstatus") as Label).Text;
                                        if (workflow[0].approver_level == "2" && (record_status == "Recommended  by Commissioner" || record_status == "Approved"))
                                            (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = true;
                                        else
                                            (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = false;
                                        (grdNOCApplicationList.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                                    }

                                }

                            }
                            else
                            {
                                Response.Redirect("~/User_Mgmt");
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
                else
                {
                    Response.Redirect("~/User_Mgmt");
                }
            }
        }
        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = 0;
            Response.Redirect("NOCApplicationForm");
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

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            int rowindex = gvr.RowIndex;
            string NOC_ID = (grdNOCApplicationList.Rows[rowindex].FindControl("lblApplicationNo") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            string party_code = (grdNOCApplicationList.Rows[rowindex].FindControl("lblparty_code") as Label).Text;
            Session["nocparty_code"] = party_code;
            Session["nocfinancial_year"] = financial_year;
            Session["UserID"] = Session["UserID"].ToString();
            Session["NOC_ID"] = NOC_ID;
            Session["rtype"] = 1;
            Session["formid"] = "I";
            Response.Redirect("NOCApplicationForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            int rowindex = gvr.RowIndex;
            string NOC_ID = (grdNOCApplicationList.Rows[rowindex].FindControl("lblApplicationNo") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            string party_code = (grdNOCApplicationList.Rows[rowindex].FindControl("lblparty_code") as Label).Text;
            Session["nocparty_code"] = party_code;
            Session["nocfinancial_year"] = financial_year;
            Session["UserID"] = Session["UserID"].ToString();
            Session["NOC_ID"] = NOC_ID;
            Session["rtype"] = 2;
            Session["formid"] = "I";
            Response.Redirect("NOCApplicationForm");
        }

        protected void Issued_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Session["UserID"] = Session["UserID"];
                LinkButton btn = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)btn.NamingContainer;
                int rowindex = gvr.RowIndex;
                string NOC_ID = (grdNOCApplicationList.Rows[rowindex].FindControl("lblApplicationNo") as Label).Text;
                string party_code = (grdNOCApplicationList.Rows[rowindex].FindControl("lblparty_code") as Label).Text;
                string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
                Session["nocfinancial_year"] = financial_year;
                Session["nocparty_code"] = party_code;
                Session["UserID"] = Session["UserID"].ToString();
                Session["NOC_ID"] = NOC_ID;
                NOC_Details noc = new NOC_Details();
                noc.record_status = "I";
                noc.noc_status = "Issued";
                noc.remarks = "Issued by " + rolename.Value;
                noc.noc_id = NOC_ID;
                noc.party_code = party_code;
                noc.user_id = Session["UserID"].ToString();
                workflow = new List<WorkFlow>();
                string r = Session["rolename"].ToString();
                workflow = BL_WorkFlow.Checkworkflow("NOC", "113", Session["rolenamecode"].ToString(), "", Session["Useridd"].ToString());
                noc.approverlevel = Convert.ToInt32(workflow[0].approver_level);
                string val = "";
                val = BL_NOC_Details.Approve(noc);
                if (val == "0")
                {
                    List<NOC_Details> noc1 = new List<NOC_Details>();
                    noc1 = BL_NOC_Details.GetNOCList();
                    var product = (from s in noc1
                                   where s.record_status != "N"
                                   orderby s.noc_id descending
                                   select s);
                    grdNOCApplicationList.DataSource = noc1;
                    grdNOCApplicationList.DataBind();
                    //}
                    grdNOCApplicationList.Columns[grdNOCApplicationList.Columns.Count - 3].Visible = false;
                    for (int i = 0; i < grdNOCApplicationList.Rows.Count; i++)
                    {
                        string record_status = (grdNOCApplicationList.Rows[i].FindControl("lblstatus") as Label).Text;
                        if (workflow[0].approver_level == "2" && record_status == "Recommended  by Commissioner" || record_status == "Approved")
                            (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = true;
                        else
                            (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = false;
                        (grdNOCApplicationList.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                    }
                    // LinkButton btn = (LinkButton)sender;
                    //   GridViewRow gvr = (GridViewRow)btn.NamingContainer;
                    string noc11 = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblApplicationNo") as Label).Text;
                    List<Reportmaster> reports = new List<Reportmaster>();
                    reports = BL_WorkFlow.GetReports(Session["UserID"].ToString());
                    Session["ReportId"] = "NOC";
                    Session["UserID"] = Session["UserID"].ToString();
                    Session["NOC_ID"] = noc11;
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('ReportPage' ,'_blank');", true);
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append("Issued Successfully");
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
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

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Session["ReportId"] = "NOC";
                Session["UserID"] = Session["UserID"].ToString();
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('ReportPage' ,'_blank');", true);
            }
        }

        protected void grdNOCApplicationList_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdNOCApplicationList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["UserID"] != null)
            {
                if (e.NewPageIndex < 0)
                {
                    grdNOCApplicationList.PageIndex = 0;
                }
                else
                {
                    grdNOCApplicationList.PageIndex = e.NewPageIndex;
                }

                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                GridViewRow row = grdNOCApplicationList.TopPagerRow;
                TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
                if (Session["nocisearch"] != null && Session["nocitext"] != null)
                {
                    ddsearch.SelectedValue = Session["nocisearch"].ToString();
                    txtpage.Text = Session["nocitext"].ToString();
                    if (ddsearch.SelectedValue != "Select")
                    {

                        if (txtpage != null)
                        {

                            if (ddsearch.SelectedValue == "valid_upto")
                            {
                                if (txtpage.Text.ToString().Length == 10)
                                {

                                    noc = BL_NOC_Details.GetNOCList1();
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                    txtpage.Focus();
                                    txtpage.Text = "";
                                    ddsearch.SelectedIndex = 0;

                                    noc = BL_NOC_Details.GetNOCList1();
                                }
                            }
                            else
                            {

                                noc = BL_NOC_Details.Search("", ddsearch.SelectedValue, txtpage.Text);
                            }

                        }
                    }
                }
                else
                {
                    noc = BL_NOC_Details.GetNOCList1();
                }
                if (ddproduct.SelectedValue != "Select")
                {

                    if (user != null)
                    {

                        districtcode.Value = user.district_code;
                        rolename.Value = user.role_name;
                        grdNOCApplicationList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                        Session["party_code"] = user.party_code;


                        if (user.role_name == "Applicant")
                        {
                            Session["party_code"] = user.party_code;
                            List<NOC_Details> noc = new List<NOC_Details>();
                            noc = BL_NOC_Details.GetNOCList();
                            var noc1 = (from s in noc
                                        where s.party_code == user.party_code && s.noc_for == ddproduct.SelectedValue
                                        orderby s.noc_id descending
                                        select s);
                            grdNOCApplicationList.DataSource = noc1.ToList();
                            grdNOCApplicationList.DataBind();
                            grdNOCApplicationList.Columns[grdNOCApplicationList.Columns.Count - 2].Visible = false;
                            for (int i = 0; i < grdNOCApplicationList.Rows.Count; i++)
                            {
                                (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = false;

                            }
                        }
                        else if (user.party_type == "All" || user.party_type == "ALL")
                        {
                            btnAddRecord.Visible = false;
                            workflow = new List<WorkFlow>();
                            workflow = BL_WorkFlow.Checkworkflow("NOC", "113", user.role_name_code.ToString(), "", user.id.ToString());
                            if (workflow.Count > 0)
                            {
                                List<NOC_Details> noc = new List<NOC_Details>();
                                noc = BL_NOC_Details.GetNOCList();
                                if (workflow[0].approver_level == "1")
                                {
                                    if (ddsearch.SelectedValue == "valid_upto")
                                    {
                                        var product = (from s in noc
                                                       where (s.record_status == "A" || s.record_status == "I") && s.district == user.district_code && s.noc_for == ddproduct.SelectedValue && s.valid_upto == txtpage.Text
                                                       orderby s.noc_id descending
                                                       select s);

                                        grdNOCApplicationList.DataSource = product.ToList();
                                        grdNOCApplicationList.DataBind();
                                    }
                                    else
                                    {
                                        var product = (from s in noc
                                                       where (s.record_status == "A" || s.record_status == "I") && s.district == user.district_code && s.noc_for == ddproduct.SelectedValue
                                                       orderby s.noc_id descending
                                                       select s);

                                        grdNOCApplicationList.DataSource = product.ToList();
                                        grdNOCApplicationList.DataBind();
                                    }


                                }
                                if (workflow[0].approver_level == "2")
                                {
                                    if (ddsearch.SelectedValue == "valid_upto")
                                    {
                                        var product = (from s in noc
                                                       where s.record_status == "I" && s.noc_for == ddproduct.SelectedValue && s.valid_upto == txtpage.Text
                                                       orderby s.noc_id descending
                                                       select s);

                                        grdNOCApplicationList.DataSource = product.ToList();
                                        grdNOCApplicationList.DataBind();
                                    }
                                    else
                                    {
                                        var product = (from s in noc
                                                       where s.record_status == "I" && s.noc_for == ddproduct.SelectedValue
                                                       orderby s.noc_id descending
                                                       select s);

                                        grdNOCApplicationList.DataSource = product.ToList();
                                        grdNOCApplicationList.DataBind();
                                    }

                                }
                                if (workflow[0].approver_level == "3")
                                {
                                    if (workflow[0].approver_level == "2")
                                    {
                                        if (ddsearch.SelectedValue == "valid_upto")
                                        {
                                            var product = (from s in noc
                                                           where s.record_status == "I" && s.noc_for == ddproduct.SelectedValue && s.valid_upto == txtpage.Text
                                                           orderby s.noc_id descending
                                                           select s);

                                            grdNOCApplicationList.DataSource = product.ToList();
                                            grdNOCApplicationList.DataBind();
                                        }
                                        else
                                        {
                                            var product = (from s in noc
                                                           where s.record_status == "I" && s.noc_for == ddproduct.SelectedValue
                                                           orderby s.noc_id descending
                                                           select s);

                                            grdNOCApplicationList.DataSource = product.ToList();
                                            grdNOCApplicationList.DataBind();
                                        }

                                    }
                                    grdNOCApplicationList.Columns[grdNOCApplicationList.Columns.Count - 3].Visible = false;
                                    for (int i = 0; i < grdNOCApplicationList.Rows.Count; i++)
                                    {
                                        string record_status = (grdNOCApplicationList.Rows[i].FindControl("lblstatus") as Label).Text;
                                        if (workflow[0].approver_level == "2" && (record_status == "Recommended  by Commissioner" || record_status == "Approved"))
                                            (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = true;
                                        else
                                            (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = false;
                                        (grdNOCApplicationList.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                                    }

                                }


                            }

                        }
                    }
                }
                else
                {
                    if (user != null)
                    {

                        districtcode.Value = user.district_code;
                        rolename.Value = user.role_name;
                        grdNOCApplicationList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                        if (user.role_name == "Applicant")
                        {
                            Session["party_code"] = user.party_code;
                            List<NOC_Details> noc = new List<NOC_Details>();
                            noc = BL_NOC_Details.GetNOCList();
                            var noc1 = (from s in noc
                                        where s.party_code == user.party_code
                                        orderby s.noc_id descending
                                        select s);
                            grdNOCApplicationList.DataSource = noc1.ToList();
                            grdNOCApplicationList.DataBind();
                            grdNOCApplicationList.Columns[grdNOCApplicationList.Columns.Count - 2].Visible = false;
                            for (int i = 0; i < grdNOCApplicationList.Rows.Count; i++)
                            {
                                (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = false;

                            }
                        }
                        else if (user.party_type == "All" || user.party_type == "ALL")
                        {
                            btnAddRecord.Visible = false;
                            workflow = new List<WorkFlow>();
                            workflow = BL_WorkFlow.Checkworkflow("NOC", "113", user.role_name_code.ToString(), "", user.id.ToString());
                            if (workflow.Count > 0)
                            {
                                if (workflow[0].approver_level == "1")
                                {
                                    if (ddsearch.SelectedValue == "valid_upto")
                                    {

                                        var product = (from s in noc
                                                       where (s.record_status == "A" || s.record_status == "I") && s.district == user.district_code && s.valid_upto == txtpage.Text
                                                       orderby s.noc_id descending
                                                       select s);

                                        grdNOCApplicationList.DataSource = product.ToList();
                                        grdNOCApplicationList.DataBind();
                                    }
                                    else
                                    {
                                        var product = (from s in noc
                                                       where (s.record_status == "A" || s.record_status == "I") && s.district == user.district_code
                                                       orderby s.noc_id descending
                                                       select s);

                                        grdNOCApplicationList.DataSource = product.ToList();
                                        grdNOCApplicationList.DataBind();
                                    }

                                    btn_I.Visible = false;
                                }
                                if (workflow[0].approver_level == "2")
                                {
                                    if (ddsearch.SelectedValue == "valid_upto")
                                    {

                                        var product = (from s in noc
                                                       where s.record_status.Trim() == "I" && s.valid_upto == txtpage.Text
                                                       orderby s.noc_id descending
                                                       select s);

                                        grdNOCApplicationList.DataSource = product.ToList();
                                        grdNOCApplicationList.DataBind();
                                    }
                                    else
                                    {
                                        var product = (from s in noc
                                                       where s.record_status.Trim() == "I"
                                                       orderby s.noc_id descending
                                                       select s);

                                        grdNOCApplicationList.DataSource = product.ToList();
                                        grdNOCApplicationList.DataBind();
                                    }
                                    btn_I.Visible = true;
                                }
                                if (workflow[0].approver_level == "3")
                                {
                                    if (ddsearch.SelectedValue == "valid_upto")
                                    {

                                        var product = (from s in noc
                                                       where s.record_status.Trim() == "I" && s.valid_upto == txtpage.Text
                                                       orderby s.noc_id descending
                                                       select s);

                                        grdNOCApplicationList.DataSource = product.ToList();
                                        grdNOCApplicationList.DataBind();
                                    }
                                    else
                                    {
                                        var product = (from s in noc
                                                       where s.record_status.Trim() == "I"
                                                       orderby s.noc_id descending
                                                       select s);

                                        grdNOCApplicationList.DataSource = product.ToList();
                                        grdNOCApplicationList.DataBind();
                                    }
                                    btn_I.Visible = false;
                                }
                                grdNOCApplicationList.Columns[grdNOCApplicationList.Columns.Count - 3].Visible = false;
                                for (int i = 0; i < grdNOCApplicationList.Rows.Count; i++)
                                {
                                    string record_status = (grdNOCApplicationList.Rows[i].FindControl("lblstatus") as Label).Text;
                                    if (workflow[0].approver_level == "2" && (record_status == "Recommended  by Commissioner" || record_status == "Approved"))
                                        (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = true;
                                    else
                                        (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = false;
                                    (grdNOCApplicationList.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                                }

                            }

                        }

                    }
                }
            }
        }
        protected void btnprint_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string noc = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblApplicationNo") as Label).Text;
            string party_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblparty_code") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
        
            Session["nocparty_code"] = party_code;
            Session["nocfinancial_year"] = financial_year;
            List<Reportmaster> reports = new List<Reportmaster>();
            reports = BL_WorkFlow.GetReports(Session["UserID"].ToString());
            int value = BL_RawMaterialReceipt.GetGrainData(party_code);

            if (value == 1)
            {
                Session["ReportId"] = "ENA";
            }
            else
            {
                Session["ReportId"] = "NOC";
            }
         
            Session["UserID"] = Session["UserID"].ToString();
            Session["NOC_ID"] = noc;
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('ReportPage' ,'_blank');", true);
        }
        protected void btn_P_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("NOC_P");
        }

        protected void btn_A_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("NOC_A");
        }

        protected void btn_R_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("NOC_B");
        }

        protected void btn_I_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("NOC_I");
        }
        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdNOCApplicationList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            if (Convert.ToInt32(txtpage.Text) <=0)
            {
                a = 0;
            }
            else
            {
                a = Convert.ToInt32(txtpage.Text);
            }
            if (a != 0)
            {
                grdNOCApplicationList.PageIndex = a - 1;

            }
            else
            {
                grdNOCApplicationList.PageIndex = a;
            }

           

            string userid = Session["UserID"].ToString();
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            if (ddproduct.SelectedValue != "Select")
            {

                if (user != null)
                {

                    districtcode.Value = user.district_code;
                    rolename.Value = user.role_name;
                    grdNOCApplicationList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                    Session["party_code"] = user.party_code;


                    if (user.role_name == "Applicant")
                    {
                        Session["party_code"] = user.party_code;
                        List<NOC_Details> noc = new List<NOC_Details>();
                        noc = BL_NOC_Details.GetNOCList();
                        var noc1 = (from s in noc
                                    where s.party_code == user.party_code && s.noc_for == ddproduct.SelectedValue
                                    orderby s.noc_id descending
                                    select s);
                        grdNOCApplicationList.DataSource = noc1.ToList();
                        grdNOCApplicationList.DataBind();
                        grdNOCApplicationList.Columns[grdNOCApplicationList.Columns.Count - 2].Visible = false;
                        for (int i = 0; i < grdNOCApplicationList.Rows.Count; i++)
                        {
                            (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = false;

                        }
                    }
                    else if (user.party_type == "All" || user.party_type == "ALL")
                    {
                        btnAddRecord.Visible = false;
                        workflow = new List<WorkFlow>();
                        workflow = BL_WorkFlow.Checkworkflow("NOC", "113", user.role_name_code.ToString(), "", user.id.ToString());
                        if (workflow.Count > 0)
                        {
                            List<NOC_Details> noc = new List<NOC_Details>();
                            noc = BL_NOC_Details.GetNOCList();
                            if (workflow[0].approver_level == "1")
                            {
                                var product = (from s in noc
                                               where (s.record_status == "A" || s.record_status == "I") && s.district == user.district_code && s.noc_for == ddproduct.SelectedValue
                                               orderby s.noc_id descending
                                               select s);

                                grdNOCApplicationList.DataSource = product.ToList();
                                grdNOCApplicationList.DataBind();

                            }
                            if (workflow[0].approver_level == "2")
                            {
                                var product = (from s in noc
                                               where s.record_status == "I" && s.noc_for == ddproduct.SelectedValue
                                               orderby s.noc_id descending
                                               select s);

                                grdNOCApplicationList.DataSource = product.ToList();
                                grdNOCApplicationList.DataBind();

                            }
                            if (workflow[0].approver_level == "3")
                            {
                                var product = (from s in noc
                                               where s.record_status == "I" && s.noc_for == ddproduct.SelectedValue
                                               orderby s.noc_id descending
                                               select s);

                                grdNOCApplicationList.DataSource = product.ToList();
                                grdNOCApplicationList.DataBind();

                            }
                            grdNOCApplicationList.Columns[grdNOCApplicationList.Columns.Count - 3].Visible = false;
                            for (int i = 0; i < grdNOCApplicationList.Rows.Count; i++)
                            {
                                string record_status = (grdNOCApplicationList.Rows[i].FindControl("lblstatus") as Label).Text;
                                if (workflow[0].approver_level == "2" && (record_status == "Recommended  by Commissioner" || record_status == "Approved"))
                                    (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = true;
                                else
                                    (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = false;
                                (grdNOCApplicationList.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                            }

                        }


                    }

                }
            }
            else
            {
                if (user != null)
                {

                    districtcode.Value = user.district_code;
                    rolename.Value = user.role_name;
                    grdNOCApplicationList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                    if (user.role_name == "Applicant")
                    {
                        Session["party_code"] = user.party_code;
                        List<NOC_Details> noc = new List<NOC_Details>();
                        noc = BL_NOC_Details.GetNOCList();
                        var noc1 = (from s in noc
                                    where s.party_code == user.party_code
                                    orderby s.noc_id descending
                                    select s);
                        grdNOCApplicationList.DataSource = noc1.ToList();
                        grdNOCApplicationList.DataBind();
                        grdNOCApplicationList.Columns[grdNOCApplicationList.Columns.Count - 2].Visible = false;
                        for (int i = 0; i < grdNOCApplicationList.Rows.Count; i++)
                        {
                            (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = false;

                        }
                    }
                    else if (user.party_type == "All" || user.party_type == "ALL")
                    {
                        btnAddRecord.Visible = false;
                        workflow = new List<WorkFlow>();
                        workflow = BL_WorkFlow.Checkworkflow("NOC", "113", user.role_name_code.ToString(), "", user.id.ToString());
                        if (workflow.Count > 0)
                        {
                            List<NOC_Details> noc = new List<NOC_Details>();
                            noc = BL_NOC_Details.GetNOCList();
                            if (workflow[0].approver_level == "1")
                            {
                                var product = (from s in noc
                                               where (s.record_status == "A" || s.record_status == "I") && s.district == user.district_code
                                               orderby s.noc_id descending
                                               select s);

                                grdNOCApplicationList.DataSource = product.ToList();
                                grdNOCApplicationList.DataBind();

                            }
                            if (workflow[0].approver_level == "2")
                            {
                                var product = (from s in noc
                                               where s.record_status == "I"
                                               orderby s.noc_id descending
                                               select s);

                                grdNOCApplicationList.DataSource = product.ToList();
                                grdNOCApplicationList.DataBind();

                            }
                            if (workflow[0].approver_level == "3")
                            {
                                var product = (from s in noc
                                               where s.record_status == "I"
                                               orderby s.noc_id descending
                                               select s);

                                grdNOCApplicationList.DataSource = product.ToList();
                                grdNOCApplicationList.DataBind();

                            }
                            grdNOCApplicationList.Columns[grdNOCApplicationList.Columns.Count - 3].Visible = false;
                            for (int i = 0; i < grdNOCApplicationList.Rows.Count; i++)
                            {
                                string record_status = (grdNOCApplicationList.Rows[i].FindControl("lblstatus") as Label).Text;
                                if (workflow[0].approver_level == "2" && (record_status == "Recommended  by Commissioner" || record_status == "Approved"))
                                    (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = true;
                                else
                                    (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = false;
                                (grdNOCApplicationList.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                            }

                        }

                    }

                }
            }

        }

        protected void grdNOCApplicationList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdNOCApplicationList.TopPagerRow;
            if (grdNOCApplicationList.PageCount != 0)
            {
                grdNOCApplicationList.TopPagerRow.Visible = true;
            }

            if (row == null)
            {
                return;
            }

            DropDownList DDLPage = (DropDownList)row.Cells[0].FindControl("DDLPage");
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            Label lblPages = (Label)row.Cells[0].FindControl("lblPages");
            Label lblCurrent = (Label)row.Cells[0].FindControl("lblCurrent");

            //if (lblPages != null)
            //{
            lblPages.Text = grdNOCApplicationList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdNOCApplicationList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdNOCApplicationList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdNOCApplicationList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdNOCApplicationList.PageIndex == 0)
            {
                ((ImageButton)grdNOCApplicationList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdNOCApplicationList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdNOCApplicationList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdNOCApplicationList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdNOCApplicationList.PageIndex + 1 == grdNOCApplicationList.PageCount)
            {
                ((ImageButton)grdNOCApplicationList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdNOCApplicationList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdNOCApplicationList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdNOCApplicationList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }

        }
        protected void ddproduct_TextChanged(object sender, EventArgs e)
        {

                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                if (ddproduct.SelectedValue != "Select")
                {
                    Session["product3"] = ddproduct.SelectedValue;
                    if (user != null)
                    {

                        districtcode.Value = user.district_code;
                        rolename.Value = user.role_name;
                        grdNOCApplicationList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                        Session["party_code"] = user.party_code;

                      
                        if (user.role_name == "Applicant")
                        {
                            Session["party_code"] = user.party_code;
                            List<NOC_Details> noc = new List<NOC_Details>();
                            noc = BL_NOC_Details.GetNOCList();
                            var noc1 = (from s in noc
                                        where s.party_code == user.party_code && s.noc_for == ddproduct.SelectedValue
                                        orderby s.noc_id descending
                                        select s);
                            grdNOCApplicationList.DataSource = noc1.ToList();
                            grdNOCApplicationList.DataBind();
                            grdNOCApplicationList.Columns[grdNOCApplicationList.Columns.Count - 2].Visible = false;
                            for (int i = 0; i < grdNOCApplicationList.Rows.Count; i++)
                            {
                                (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = false;

                            }
                        }
                        else if (user.party_type == "All" || user.party_type == "ALL")
                        {
                            btnAddRecord.Visible = false;
                            workflow = new List<WorkFlow>();
                            workflow = BL_WorkFlow.Checkworkflow("NOC", "113", user.role_name_code.ToString(), "", user.id.ToString());
                            if (workflow.Count > 0)
                            {
                                List<NOC_Details> noc = new List<NOC_Details>();
                                noc = BL_NOC_Details.GetNOCList();
                                if (workflow[0].approver_level == "1")
                                {
                                    var product = (from s in noc
                                                   where (s.record_status == "A" || s.record_status == "I") && s.district == user.district_code && s.noc_for == ddproduct.SelectedValue
                                                   orderby s.noc_id descending
                                                   select s);

                                    grdNOCApplicationList.DataSource = product.ToList();
                                    grdNOCApplicationList.DataBind();

                                }
                                if (workflow[0].approver_level == "2")
                                {
                                    var product = (from s in noc
                                                   where s.record_status == "I" && s.noc_for == ddproduct.SelectedValue
                                                   orderby s.noc_id descending
                                                   select s);

                                    grdNOCApplicationList.DataSource = product.ToList();
                                    grdNOCApplicationList.DataBind();

                                }
                                if (workflow[0].approver_level == "3")
                                {
                                    var product = (from s in noc
                                                   where s.record_status == "I" && s.noc_for == ddproduct.SelectedValue
                                                   orderby s.noc_id descending
                                                   select s);

                                    grdNOCApplicationList.DataSource = product.ToList();
                                    grdNOCApplicationList.DataBind();

                                }
                                grdNOCApplicationList.Columns[grdNOCApplicationList.Columns.Count - 3].Visible = false;
                                for (int i = 0; i < grdNOCApplicationList.Rows.Count; i++)
                                {
                                    string record_status = (grdNOCApplicationList.Rows[i].FindControl("lblstatus") as Label).Text;
                                    if (workflow[0].approver_level == "2" && (record_status == "Recommended  by Commissioner" || record_status == "Approved"))
                                        (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = true;
                                    else
                                        (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = false;
                                    (grdNOCApplicationList.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                                }

                            }


                        }

                    }
                }
                else
                {
                    if (user != null)
                    {

                        districtcode.Value = user.district_code;
                        rolename.Value = user.role_name;
                        grdNOCApplicationList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                        if (user.role_name == "Applicant")
                        {
                            Session["party_code"] = user.party_code;
                            List<NOC_Details> noc = new List<NOC_Details>();
                            noc = BL_NOC_Details.GetNOCList();
                            var noc1 = (from s in noc
                                        where s.party_code == user.party_code
                                        orderby s.noc_id descending
                                        select s);
                            grdNOCApplicationList.DataSource = noc1.ToList();
                            grdNOCApplicationList.DataBind();
                            grdNOCApplicationList.Columns[grdNOCApplicationList.Columns.Count - 2].Visible = false;
                            for (int i = 0; i < grdNOCApplicationList.Rows.Count; i++)
                            {
                                (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = false;

                            }
                        }
                        else if (user.party_type == "All" || user.party_type == "ALL")
                        {
                            btnAddRecord.Visible = false;
                            workflow = new List<WorkFlow>();
                            workflow = BL_WorkFlow.Checkworkflow("NOC", "113", user.role_name_code.ToString(), "", user.id.ToString());
                            if (workflow.Count > 0)
                            {
                                List<NOC_Details> noc = new List<NOC_Details>();
                                noc = BL_NOC_Details.GetNOCList();
                                if (workflow[0].approver_level == "1")
                                {
                                    var product = (from s in noc
                                                   where (s.record_status == "A" || s.record_status == "I") && s.district == user.district_code
                                                   orderby s.noc_id descending
                                                   select s);

                                    grdNOCApplicationList.DataSource = product.ToList();
                                    grdNOCApplicationList.DataBind();

                                }
                                if (workflow[0].approver_level == "2")
                                {
                                    var product = (from s in noc
                                                   where s.record_status == "I"
                                                   orderby s.noc_id descending
                                                   select s);

                                    grdNOCApplicationList.DataSource = product.ToList();
                                    grdNOCApplicationList.DataBind();

                                }
                                if (workflow[0].approver_level == "3")
                                {
                                    var product = (from s in noc
                                                   where s.record_status == "I"
                                                   orderby s.noc_id descending
                                                   select s);

                                    grdNOCApplicationList.DataSource = product.ToList();
                                    grdNOCApplicationList.DataBind();

                                }
                                grdNOCApplicationList.Columns[grdNOCApplicationList.Columns.Count - 3].Visible = false;
                                for (int i = 0; i < grdNOCApplicationList.Rows.Count; i++)
                                {
                                    string record_status = (grdNOCApplicationList.Rows[i].FindControl("lblstatus") as Label).Text;
                                    if (workflow[0].approver_level == "2" && (record_status == "Recommended  by Commissioner" || record_status == "Approved"))
                                        (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = true;
                                    else
                                        (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = false;
                                    (grdNOCApplicationList.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                                }

                            }

                        }

                    }
                }
            
        }


        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GridViewRow row = grdNOCApplicationList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {
                Session["nocisearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["nocitext"] = txtpage.Text;
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        if (ddsearch.SelectedValue == "valid_upto")
                        {
                            if (txtpage.Text.ToString().Length == 10)
                            {

                                noc = BL_NOC_Details.GetNOCList1();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                txtpage.Focus();
                                txtpage.Text = "";
                                ddsearch.SelectedIndex = 0;

                                noc = BL_NOC_Details.GetNOCList1();
                            }
                        }
                        else
                        {

                            noc = BL_NOC_Details.Search("", ddsearch.SelectedValue, txtpage.Text);
                        }
                        if (user.party_type == "All" || user.party_type == "ALL")
                        {
                            btnAddRecord.Visible = false;
                            workflow = new List<WorkFlow>();
                            workflow = BL_WorkFlow.Checkworkflow("NOC", "113", user.role_name_code.ToString(), "", user.id.ToString());
                            if (workflow.Count > 0)
                            {
                                if (workflow[0].approver_level == "1")
                                {
                                    if (ddsearch.SelectedValue == "valid_upto")
                                    {

                                        var product = (from s in noc
                                                       where (s.record_status == "A" || s.record_status == "I") && s.district == user.district_code && s.valid_upto == txtpage.Text
                                                       orderby s.noc_id descending
                                                       select s);

                                        grdNOCApplicationList.DataSource = product.ToList();
                                        grdNOCApplicationList.DataBind();
                                    }
                                    else
                                    {
                                        var product = (from s in noc
                                                       where (s.record_status == "A" || s.record_status == "I") && s.district == user.district_code
                                                       orderby s.noc_id descending
                                                       select s);

                                        grdNOCApplicationList.DataSource = product.ToList();
                                        grdNOCApplicationList.DataBind();
                                    }

                                    btn_I.Visible = false;
                                }
                                if (workflow[0].approver_level == "2")
                                {
                                    if (ddsearch.SelectedValue == "valid_upto")
                                    {

                                        var product = (from s in noc
                                                       where s.record_status.Trim() == "I" && s.valid_upto == txtpage.Text
                                                       orderby s.noc_id descending
                                                       select s);

                                        grdNOCApplicationList.DataSource = product.ToList();
                                        grdNOCApplicationList.DataBind();
                                    }
                                    else
                                    {
                                        var product = (from s in noc
                                                       where s.record_status.Trim() == "I"
                                                       orderby s.noc_id descending
                                                       select s);

                                        grdNOCApplicationList.DataSource = product.ToList();
                                        grdNOCApplicationList.DataBind();
                                    }
                                    btn_I.Visible = true;
                                }
                                if (workflow[0].approver_level == "3")
                                {
                                    if (ddsearch.SelectedValue == "valid_upto")
                                    {

                                        var product = (from s in noc
                                                       where  s.record_status.Trim() == "I" && s.valid_upto == txtpage.Text
                                                       orderby s.noc_id descending
                                                       select s);

                                        grdNOCApplicationList.DataSource = product.ToList();
                                        grdNOCApplicationList.DataBind();
                                    }
                                    else
                                    {
                                        var product = (from s in noc
                                                       where  s.record_status.Trim() == "I"
                                                       orderby s.noc_id descending
                                                       select s);

                                        grdNOCApplicationList.DataSource = product.ToList();
                                        grdNOCApplicationList.DataBind();
                                    }
                                    btn_I.Visible = false;
                                }

                                grdNOCApplicationList.Columns[grdNOCApplicationList.Columns.Count - 3].Visible = false;
                                for (int i = 0; i < grdNOCApplicationList.Rows.Count; i++)
                                {
                                    string record_status = (grdNOCApplicationList.Rows[i].FindControl("lblstatus") as Label).Text;
                                    if (workflow[0].approver_level == "2" && (record_status == "Recommended  by Commissioner" || record_status == "Approved"))
                                        (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = true;
                                    else
                                        (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = false;
                                    (grdNOCApplicationList.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                                }

                            }
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

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["nocisearch"] = null;
            Session["nocitext"] = null;
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            if (Session["product3"] != null)
            {
                ddproduct.SelectedValue = Session["product3"].ToString();
                ddproduct_TextChanged(sender, e);

            }
            else
            {
                if (user.role_name == "Applicant")
                {
                    Session["party_code"] = user.party_code;
                    List<NOC_Details> noc = new List<NOC_Details>();
                    noc = BL_NOC_Details.GetNOCList();
                    var noc1 = (from s in noc
                                where s.party_code == user.party_code
                                orderby s.noc_id descending
                                select s);
                    grdNOCApplicationList.DataSource = noc1.ToList();
                    grdNOCApplicationList.DataBind();
                    grdNOCApplicationList.Columns[grdNOCApplicationList.Columns.Count - 2].Visible = false;
                    for (int i = 0; i < grdNOCApplicationList.Rows.Count; i++)
                    {
                        (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = false;

                    }
                }
                else if (user.party_type == "All" || user.party_type == "ALL")
                {
                    btnAddRecord.Visible = false;
                    workflow = new List<WorkFlow>();
                    workflow = BL_WorkFlow.Checkworkflow("NOC", "113", user.role_name_code.ToString(), "", user.id.ToString());
                    if (workflow.Count > 0)
                    {
                        List<NOC> noc = new List<NOC>();
                        noc = BL_NOC_Details.GetNOCList1();
                        if (workflow[0].approver_level == "1")
                        {
                            var product = (from s in noc
                                           where (s.record_status == "A" || s.record_status == "I") && s.district == user.district_code
                                           orderby s.noc_id descending
                                           select s);

                            grdNOCApplicationList.DataSource = product.ToList();
                            grdNOCApplicationList.DataBind();

                        }
                        if (workflow[0].approver_level == "2")
                        {
                            var product = (from s in noc
                                           where s.record_status == "I"
                                           orderby s.noc_id descending
                                           select s);

                            grdNOCApplicationList.DataSource = product.ToList();
                            grdNOCApplicationList.DataBind();

                        }
                        if (workflow[0].approver_level == "3")
                        {
                            var product = (from s in noc
                                           where s.record_status == "I"
                                           orderby s.noc_id descending
                                           select s);

                            grdNOCApplicationList.DataSource = product.ToList();
                            grdNOCApplicationList.DataBind();

                        }
                        grdNOCApplicationList.Columns[grdNOCApplicationList.Columns.Count - 3].Visible = false;
                        for (int i = 0; i < grdNOCApplicationList.Rows.Count; i++)
                        {
                            string record_status = (grdNOCApplicationList.Rows[i].FindControl("lblstatus") as Label).Text;
                            if (workflow[0].approver_level == "2" && (record_status == "Recommended  by Commissioner" || record_status == "Approved"))
                                (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = true;
                            else
                                (grdNOCApplicationList.Rows[i].FindControl("Issued") as LinkButton).Visible = false;
                            (grdNOCApplicationList.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                        }

                    }

                }
            }
        }
    }
}