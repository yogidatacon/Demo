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
    public partial class Allocation_B : System.Web.UI.Page
    {
        List<Molasses_Allocation> allots = new List<Molasses_Allocation>();
        // static UserDetails user = new UserDetails();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    Session["UserID"] = Session["UserID"];
                    if (user != null)
                    {
                        if (user.role_name.ToString().Trim() == "Commissioner")
                        {
                            btnAllocation_I.Visible = false;
                        }
                        Session["rolename"] = user.role_name;
                        grdAllocationRequestView.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                        if (user.role_name == "Applicant")
                        {
                            Session["party_code"] = user.party_code;
                            allots = new List<Molasses_Allocation>();
                            allots = BL_Molasses_Allocation.GetList();
                            var list = (from s in allots
                                        where s.party_code == user.party_code
                                        orderby Convert.ToDateTime(s.req_allotmentdate) descending
                                        select s);
                            grdAllocationRequestView.DataSource = list.ToList();
                            grdAllocationRequestView.DataBind();
                            grdAllocationRequestView.Columns[grdAllocationRequestView.Columns.Count - 2].Visible = false;
                            for (int i = 0; i < grdAllocationRequestView.Rows.Count; i++)
                            {
                                (grdAllocationRequestView.Rows[i].FindControl("btnEssue") as LinkButton).Visible = false;
                            }
                        }
                        else if (user.party_type == "All" || user.party_type == "ALL")
                        {
                            //  btnAddRecord.Visible = false;
                            List<WorkFlow> workflow = new List<WorkFlow>();
                            workflow = BL_WorkFlow.Checkworkflow("MA", "110", user.role_name_code.ToString(), "", user.id.ToString());
                            if (workflow.Count > 0)
                            {
                                //  btnIndent.Visible = false;
                                allots = new List<Molasses_Allocation>();
                                allots = BL_Molasses_Allocation.GetList();
                                var list = (from s in allots
                                            where s.record_status == "B"
                                            orderby Convert.ToDateTime(s.req_allotmentdate) descending
                                            select s);
                                grdAllocationRequestView.DataSource = list.ToList();
                                grdAllocationRequestView.DataBind();

                                for (int i = 0; i < grdAllocationRequestView.Rows.Count; i++)
                                {
                                    string record_status = (grdAllocationRequestView.Rows[i].FindControl("lblstatus") as Label).Text;
                                    if (workflow[0].approver_level == "1" && (record_status == "A" || record_status == "Approved by Commissioner"))
                                        (grdAllocationRequestView.Rows[i].FindControl("btnEssue") as LinkButton).Visible = true;
                                    else
                                        (grdAllocationRequestView.Rows[i].FindControl("btnEssue") as LinkButton).Visible = false;

                                    (grdAllocationRequestView.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                                }
                                grdAllocationRequestView.Columns[grdAllocationRequestView.Columns.Count - 3].Visible = false;
                            }
                            else
                            {
                                Session["UserID"] = Session["UserID"];
                                Response.Redirect("~/User_Mgmt"); ;
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

        protected void btnAllocation_P_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("Allocation_P");
        }

        protected void btnAllocation_B_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("Allocation_B");
        }

        protected void btnAllocation_A_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("Allocation_A");
        }

        protected void btnAllocation_I_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("Allocation_I");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string Allotment_ID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblIndentId") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblFincialyear") as Label).Text;
            string party_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpartycode") as Label).Text;
            Session["aparty_code"] = party_code;
            Session["financial_year"] = financial_year;
            Session["Allotment_ID"] = Allotment_ID;
            Session["UserID"] = Session["UserID"];
            Session["formid"] = "B";
            Session["rtype"] = "1";
            Response.Redirect("AllocationRequestForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string Allotment_ID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblIndentId") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblFincialyear") as Label).Text;
            string party_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpartycode") as Label).Text;
            Session["aparty_code"] = party_code;
            Session["financial_year"] = financial_year;
            Session["Allotment_ID"] = Allotment_ID;
            Session["UserID"] = Session["UserID"];
            Session["formid"] = "B";
            Session["rtype"] = "2";
            Response.Redirect("AllocationRequestForm");
        }

        protected void btnEssue_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            if (user != null)
            {
                string Allotment_ID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblIndentId") as Label).Text;
                string val = "";
                string transaction_type = "ALT";
                string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblFincialyear") as Label).Text;
                val = BL_Molasses_Allocation.Issued(Allotment_ID, user, transaction_type, financial_year);
             
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("Allocation_I.");

                }

                else
                {

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

        protected void grdAllocationRequestView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["UserID"] != null)
            {
                if (e.NewPageIndex < 0)
                {
                    grdAllocationRequestView.PageIndex = 0;
                }
                else
                {
                    grdAllocationRequestView.PageIndex = e.NewPageIndex;
                }
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                GridViewRow row = grdAllocationRequestView.TopPagerRow;
                TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");

                if (Session["allbsearch"] != null && Session["allbtext"] != null)
                {
                    ddsearch.SelectedValue = Session["allbsearch"].ToString();
                    txtpage.Text = Session["allbtext"].ToString();
                    if (ddsearch.SelectedValue != "Select")
                    {

                        if (txtpage != null)
                        {
                            if (ddsearch.SelectedValue == "req_allotmentdate")
                            {
                                if (txtpage.Text.ToString().Length == 10)
                                {


                                    allots = BL_Molasses_Allocation.GetList();
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                    txtpage.Focus();
                                    txtpage.Text = "";
                                    ddsearch.SelectedIndex = 0;

                                    allots = BL_Molasses_Allocation.GetList();
                                }
                            }
                            else
                            {
                                allots = new List<Molasses_Allocation>();
                                allots = BL_Molasses_Allocation.Search("", ddsearch.SelectedValue, txtpage.Text);
                            }

                        }
                    }
                }
                else
                {
                    allots = new List<Molasses_Allocation>();
                    allots = BL_Molasses_Allocation.GetList();
                }
                Session["UserID"] = Session["UserID"];
                if (user != null)
                {
                    if (user.role_name.ToString().Trim() == "Commissioner")
                    {
                        btnAllocation_I.Visible = false;
                    }
                    if (user.role_name == "Applicant")
                    {
                       
                        var list = (from s in allots
                                    where s.party_code == user.party_code
                                    orderby Convert.ToDateTime(s.req_allotmentdate) descending
                                    select s);
                        grdAllocationRequestView.DataSource = list.ToList();
                        grdAllocationRequestView.DataBind();
                        grdAllocationRequestView.Columns[grdAllocationRequestView.Columns.Count - 2].Visible = false;
                        for (int i = 0; i < grdAllocationRequestView.Rows.Count; i++)
                        {

                            (grdAllocationRequestView.Rows[i].FindControl("btnEssue") as LinkButton).Visible = false;


                        }
                    }
                    else if (user.party_type == "All" || user.party_type == "ALL")
                    {
                        //  btnAddRecord.Visible = false;
                        List<WorkFlow> workflow = new List<WorkFlow>();
                        workflow = BL_WorkFlow.Checkworkflow("MA", "110", user.role_name_code.ToString(), "", user.id.ToString());
                        if (workflow.Count > 0)
                        {
                            //btnIndent.Visible = false;
                            if (ddsearch.SelectedValue == "req_allotmentdate")
                            {
                                var list = (from s in allots
                                            where s.record_status == "B" && s.req_allotmentdate == txtpage.Text
                                            orderby Convert.ToDateTime(s.req_allotmentdate) descending
                                            select s);
                                grdAllocationRequestView.DataSource = list.ToList();
                                grdAllocationRequestView.DataBind();
                            }
                            else
                            {
                                var list = (from s in allots
                                            where s.record_status == "B"
                                            orderby Convert.ToDateTime(s.req_allotmentdate) descending
                                            select s);
                                grdAllocationRequestView.DataSource = list.ToList();
                                grdAllocationRequestView.DataBind();
                            }


                            grdAllocationRequestView.Columns[grdAllocationRequestView.Columns.Count - 3].Visible = false;
                            for (int i = 0; i < grdAllocationRequestView.Rows.Count; i++)
                            {
                                // string record_status = list.ToList()[i].record_status;
                                string record_status = (grdAllocationRequestView.Rows[i].FindControl("lblstatus") as Label).Text;
                                if (workflow[0].approver_level == "1" && (record_status == "A" || record_status == "Approved by Commissioner"))
                                    (grdAllocationRequestView.Rows[i].FindControl("btnEssue") as LinkButton).Visible = true;
                                else
                                    (grdAllocationRequestView.Rows[i].FindControl("btnEssue") as LinkButton).Visible = false;

                                (grdAllocationRequestView.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
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
        }

        protected void btnprint_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string IndentNO = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblIndentId") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblFincialyear") as Label).Text;
            string party_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpartycode") as Label).Text;
            Session["aparty_code"] = party_code;
            Session["financial_year"] = financial_year;
            Session["ReportId"] = "allotment_letter";
            Session["UserID"] = Session["UserID"].ToString();
            Session["AllotmentNo"] = IndentNO;
            Session["Type"] = "Print";
            Session["Allvalues"] = "030515c5_470,120_220,70";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('ReportPage' ,'_blank');", true);
        }
        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdAllocationRequestView.TopPagerRow;
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
                grdAllocationRequestView.PageIndex = a - 1;
            }
            else
            {
                grdAllocationRequestView.PageIndex = a;
            }
            string userid = Session["UserID"].ToString();
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            Session["UserID"] = Session["UserID"];
            if (user != null)
            {
                if (user.role_name.ToString().Trim() == "Commissioner")
                {
                    btnAllocation_I.Visible = false;
                }
                if (user.role_name == "Applicant")
                {
                    allots = new List<Molasses_Allocation>();
                    allots = BL_Molasses_Allocation.GetList();
                    var list = (from s in allots
                                where s.party_code == user.party_code
                                orderby Convert.ToDateTime(s.req_allotmentdate) descending
                                select s);
                    grdAllocationRequestView.DataSource = list.ToList();
                    grdAllocationRequestView.DataBind();
                    grdAllocationRequestView.Columns[grdAllocationRequestView.Columns.Count - 2].Visible = false;
                    for (int i = 0; i < grdAllocationRequestView.Rows.Count; i++)
                    {

                        (grdAllocationRequestView.Rows[i].FindControl("btnEssue") as LinkButton).Visible = false;


                    }
                }
                else if (user.party_type == "All" || user.party_type == "ALL")
                {
                    //  btnAddRecord.Visible = false;
                    List<WorkFlow> workflow = new List<WorkFlow>();
                    workflow = BL_WorkFlow.Checkworkflow("MA", "110", user.role_name_code.ToString(), "", user.id.ToString());
                    if (workflow.Count > 0)
                    {
                        //btnIndent.Visible = false;
                        allots = new List<Molasses_Allocation>();
                        allots = BL_Molasses_Allocation.GetList();

                        var list = (from s in allots
                                    where s.record_status == "B"
                                    orderby Convert.ToDateTime(s.req_allotmentdate) descending
                                    select s);
                        grdAllocationRequestView.DataSource = list.ToList();
                        grdAllocationRequestView.DataBind();


                        grdAllocationRequestView.Columns[grdAllocationRequestView.Columns.Count - 3].Visible = false;
                        for (int i = 0; i < grdAllocationRequestView.Rows.Count; i++)
                        {
                            // string record_status = list.ToList()[i].record_status;
                            string record_status = (grdAllocationRequestView.Rows[i].FindControl("lblstatus") as Label).Text;
                            if (workflow[0].approver_level == "1" && (record_status == "A" || record_status == "Approved by Commissioner"))
                                (grdAllocationRequestView.Rows[i].FindControl("btnEssue") as LinkButton).Visible = true;
                            else
                                (grdAllocationRequestView.Rows[i].FindControl("btnEssue") as LinkButton).Visible = false;

                            (grdAllocationRequestView.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
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
        protected void grdAllocationRequestView_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdAllocationRequestView.TopPagerRow;
            if (grdAllocationRequestView.PageCount != 0)
            {
                grdAllocationRequestView.TopPagerRow.Visible = true;
            }

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

            if (Session["allbsearch"] != null && Session["allbtext"] != null)
            {
                ddsearch.SelectedValue = Session["allbsearch"].ToString();
                txtpages.Text = Session["allbtext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdAllocationRequestView.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdAllocationRequestView.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdAllocationRequestView.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdAllocationRequestView.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdAllocationRequestView.PageIndex == 0)
            {
                ((ImageButton)grdAllocationRequestView.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdAllocationRequestView.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdAllocationRequestView.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdAllocationRequestView.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdAllocationRequestView.PageIndex + 1 == grdAllocationRequestView.PageCount)
            {
                ((ImageButton)grdAllocationRequestView.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdAllocationRequestView.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdAllocationRequestView.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdAllocationRequestView.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {


            GridViewRow row = grdAllocationRequestView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["allbsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["allbtext"] = txtpage.Text;
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {

                        if (ddsearch.SelectedValue == "req_allotmentdate")
                        {
                            if (txtpage.Text.ToString().Length == 10)
                            {


                                allots = BL_Molasses_Allocation.GetList();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                txtpage.Focus();
                                txtpage.Text = "";
                                ddsearch.SelectedIndex = 0;

                                allots = BL_Molasses_Allocation.GetList();
                            }
                        }
                        else
                        {
                            allots = new List<Molasses_Allocation>();
                            allots = BL_Molasses_Allocation.Search("", ddsearch.SelectedValue, txtpage.Text);
                        }
                        if (user.party_type == "All" || user.party_type == "ALL")
                        {
                            //  btnAddRecord.Visible = false;
                            List<WorkFlow> workflow = new List<WorkFlow>();
                            workflow = BL_WorkFlow.Checkworkflow("MA", "110", user.role_name_code.ToString(), "", user.id.ToString());
                            if (workflow.Count > 0)
                            {
                                //  btnIndent.Visible = false;
                              
                                    if (ddsearch.SelectedValue == "req_allotmentdate")
                                    {
                                        var list = (from s in allots
                                                    where s.record_status == "B" && s.req_allotmentdate == txtpage.Text
                                                    orderby Convert.ToDateTime(s.req_allotmentdate) descending
                                                    select s);
                                        grdAllocationRequestView.DataSource = list.ToList();
                                        grdAllocationRequestView.DataBind();
                                    }
                                    else
                                    {
                                        var list = (from s in allots
                                                    where s.record_status == "B"
                                                    orderby Convert.ToDateTime(s.req_allotmentdate) descending
                                                    select s);
                                        grdAllocationRequestView.DataSource = list.ToList();
                                        grdAllocationRequestView.DataBind();
                                    }

                                for (int i = 0; i < grdAllocationRequestView.Rows.Count; i++)
                                {
                                    string record_status = (grdAllocationRequestView.Rows[i].FindControl("lblstatus") as Label).Text;
                                    if (workflow[0].approver_level == "1" && (record_status == "A" || record_status == "Approved by Commissioner"))
                                        (grdAllocationRequestView.Rows[i].FindControl("btnEssue") as LinkButton).Visible = true;
                                    else
                                        (grdAllocationRequestView.Rows[i].FindControl("btnEssue") as LinkButton).Visible = false;

                                    (grdAllocationRequestView.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                                }
                                grdAllocationRequestView.Columns[grdAllocationRequestView.Columns.Count - 3].Visible = false;
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
            Session["allbsearch"] = null;
            Session["allbtext"] = null;
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            if (user.role_name == "Applicant")
            {
                Session["party_code"] = user.party_code;
                allots = new List<Molasses_Allocation>();
                allots = BL_Molasses_Allocation.GetList();
                var list = (from s in allots
                            where s.party_code == user.party_code
                            orderby Convert.ToDateTime(s.req_allotmentdate) descending
                            select s);
                grdAllocationRequestView.DataSource = list.ToList();
                grdAllocationRequestView.DataBind();
                grdAllocationRequestView.Columns[grdAllocationRequestView.Columns.Count - 2].Visible = false;
                for (int i = 0; i < grdAllocationRequestView.Rows.Count; i++)
                {
                    (grdAllocationRequestView.Rows[i].FindControl("btnEssue") as LinkButton).Visible = false;
                }
            }
            else if (user.party_type == "All" || user.party_type == "ALL")
            {
                //  btnAddRecord.Visible = false;
                List<WorkFlow> workflow = new List<WorkFlow>();
                workflow = BL_WorkFlow.Checkworkflow("MA", "110", user.role_name_code.ToString(), "", user.id.ToString());
                if (workflow.Count > 0)
                {
                    //  btnIndent.Visible = false;
                    allots = new List<Molasses_Allocation>();
                    allots = BL_Molasses_Allocation.GetList();
                    var list = (from s in allots
                                where s.record_status == "B"
                                orderby Convert.ToDateTime(s.req_allotmentdate) descending
                                select s);
                    grdAllocationRequestView.DataSource = list.ToList();
                    grdAllocationRequestView.DataBind();

                    for (int i = 0; i < grdAllocationRequestView.Rows.Count; i++)
                    {
                        string record_status = (grdAllocationRequestView.Rows[i].FindControl("lblstatus") as Label).Text;
                        if (workflow[0].approver_level == "1" && (record_status == "A" || record_status == "Approved by Commissioner"))
                            (grdAllocationRequestView.Rows[i].FindControl("btnEssue") as LinkButton).Visible = true;
                        else
                            (grdAllocationRequestView.Rows[i].FindControl("btnEssue") as LinkButton).Visible = false;

                        (grdAllocationRequestView.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                    }
                    grdAllocationRequestView.Columns[grdAllocationRequestView.Columns.Count - 3].Visible = false;
                }
            }
        }
    }
}