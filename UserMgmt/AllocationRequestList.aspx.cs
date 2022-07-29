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
    public partial class AllocationRequestList : System.Web.UI.Page
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
                        Session["rolename"] = user.role_name;

                        if (user.role_name == "Applicant")
                        {
                            grdAllocationRequestView.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                            Session["party_code"] = user.party_code;
                            allots = new List<Molasses_Allocation>();
                            allots = BL_Molasses_Allocation.GetList();
                            var list = (from s in allots
                                        where s.party_code == user.party_code && s.financial_year==user.financial_year
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
                        else if (user.user_id == "Admin")
                        {
                            allots = new List<Molasses_Allocation>();
                            allots = BL_Molasses_Allocation.GetList();
                            var list = (from s in allots
                                        orderby Convert.ToDateTime(s.req_allotmentdate) descending
                                        select s);
                            grdAllocationRequestView.DataSource = list.ToList();
                            grdAllocationRequestView.DataBind();
                            grdAllocationRequestView.Columns[grdAllocationRequestView.Columns.Count - 2].Visible = false;
                            for (int i = 0; i < grdAllocationRequestView.Rows.Count; i++)
                            {
                                (grdAllocationRequestView.Rows[i].FindControl("btnEssue") as LinkButton).Visible = false;
                               if( (grdAllocationRequestView.Rows[i].FindControl("lblstatus") as Label).Text == "Pending" || (grdAllocationRequestView.Rows[i].FindControl("lblstatus") as Label).Text == "Alloted")
                                    {
                                    (grdAllocationRequestView.Rows[i].FindControl("btnEdit") as LinkButton).Visible =true;
                                }
                            }
                        }
                        else if (user.party_type == "All" || user.party_type == "ALL")
                        {
                            //btnAddRecord.Visible = false;
                            //List<WorkFlow> workflow = new List<WorkFlow>();
                            //workflow = BL_WorkFlow.Checkworkflow("MA", "110", user.role_name_code.ToString(), "", user.id.ToString());
                            //if (workflow.Count > 0)
                            //{
                            //    btnIndent.Visible = false;
                            //    allots = new List<Molasses_Allocation>();
                            //    allots = BL_Molasses_Allocation.GetList();
                            //    var list = (from s in allots
                            //                where s.record_status != "N"
                            //                orderby Convert.ToDateTime(s.req_allotmentdate) descending
                            //                select s);
                            //    grdAllocationRequestView.DataSource = list.ToList();
                            //    grdAllocationRequestView.DataBind();

                            //    for (int i = 0; i < grdAllocationRequestView.Rows.Count; i++)
                            //    {
                            //        string record_status = (grdAllocationRequestView.Rows[i].FindControl("lblstatus") as Label).Text;
                            //        if (workflow[0].approver_level == "1" && (record_status == "A" || record_status == "Approved by Commissioner"))
                            //            (grdAllocationRequestView.Rows[i].FindControl("btnEssue") as LinkButton).Visible = true;
                            //        else
                            //            (grdAllocationRequestView.Rows[i].FindControl("btnEssue") as LinkButton).Visible = false;

                            //        (grdAllocationRequestView.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                            //    }
                            //    grdAllocationRequestView.Columns[grdAllocationRequestView.Columns.Count - 3].Visible = false;
                            Response.Redirect("~/Allocation_P");
                            //}
                            //    else
                            //    {
                            //        Session["UserID"] = Session["UserID"];
                            //        Response.Redirect("~/User_Mgmt"); ;
                            //    }
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

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "0";
            Response.Redirect("AllocationRequestForm");
        }
        protected void btnIndent_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("IndentList");
        }

        protected void btnARM_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("AllocationRequestList");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string Allotment_ID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblIndentId") as Label).Text;
             string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblFincialyear") as Label).Text;
            string party_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblpartycode") as Label).Text;
            Session["aparty_code"] = party_code;
            Session["Allotment_ID"] = Allotment_ID;
             Session["UserID"] = Session["UserID"];
            Session["financial_year"] = financial_year;
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
                    // Session["Type"] = "Issue";
                    //  Session["Allvalues"] = "03053c41_490,175_180,70";
                    //  Session["ReportId"] = "allotment_letter";
                    // Session["AllotmentNo"] = Allotment_ID;
                    Response.Redirect("AllocationRequestList");

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
                Session["UserID"] = Session["UserID"];
                if (user != null)
                {
                    string userid = Session["UserID"].ToString();
                    Session["UserID"] = userid;
                    GridViewRow row = grdAllocationRequestView.TopPagerRow;
                    TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                    DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
               
                    if (Session["allsearch"] != null && Session["alldtext"] != null)
                    {
                        ddsearch.SelectedValue = Session["allsearch"].ToString();
                        txtpage.Text = Session["alldtext"].ToString();
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
                        if (user.role_name == "Applicant")
                    {

                        if (ddsearch.SelectedValue == "req_allotmentdate")
                        {
                            var list = (from s in allots
                                        where s.party_code == user.party_code && s.req_allotmentdate == txtpage.Text && s.financial_year == user.financial_year
                                        orderby Convert.ToDateTime(s.req_allotmentdate) descending
                                        select s);
                            grdAllocationRequestView.DataSource = list.ToList();
                            grdAllocationRequestView.DataBind();
                        }
                        else
                        {
                            var list = (from s in allots
                                        where s.party_code == user.party_code && s.financial_year == user.financial_year
                                        orderby Convert.ToDateTime(s.req_allotmentdate) descending
                                        select s);
                            grdAllocationRequestView.DataSource = list.ToList();
                            grdAllocationRequestView.DataBind();
                        }
                        grdAllocationRequestView.Columns[grdAllocationRequestView.Columns.Count - 2].Visible = false;
                        for (int i = 0; i < grdAllocationRequestView.Rows.Count; i++)
                        {

                            (grdAllocationRequestView.Rows[i].FindControl("btnEssue") as LinkButton).Visible = false;


                        }
                    }
                    else if (user.user_id == "Admin")
                    {
                       
                        var list = (from s in allots
                                    orderby Convert.ToDateTime(s.req_allotmentdate) descending
                                    select s);
                        grdAllocationRequestView.DataSource = list.ToList();
                        grdAllocationRequestView.DataBind();
                        grdAllocationRequestView.Columns[grdAllocationRequestView.Columns.Count - 2].Visible = false;
                        for (int i = 0; i < grdAllocationRequestView.Rows.Count; i++)
                        {
                            (grdAllocationRequestView.Rows[i].FindControl("btnEssue") as LinkButton).Visible = false;
                            if ((grdAllocationRequestView.Rows[i].FindControl("lblstatus") as Label).Text == "Pending" || (grdAllocationRequestView.Rows[i].FindControl("lblstatus") as Label).Text == "Alloted")
                            {
                                (grdAllocationRequestView.Rows[i].FindControl("btnEdit") as LinkButton).Visible = true;
                            }
                        }
                    }
                    else if (user.party_type == "All" || user.party_type == "ALL")
                    {
                        btnAddRecord.Visible = false;
                        List<WorkFlow> workflow = new List<WorkFlow>();
                        workflow = BL_WorkFlow.Checkworkflow("MA", "110", user.role_name_code.ToString(), "", user.id.ToString());
                        if (workflow.Count > 0)
                        {
                            btnIndent.Visible = false;

                            if (ddsearch.SelectedValue == "req_allotmentdate")
                            {
                                var list = (from s in allots
                                            where s.record_status != "N" && s.req_allotmentdate == txtpage.Text && s.financial_year == user.financial_year
                                            orderby Convert.ToDateTime(s.req_allotmentdate) descending
                                            select s);
                                grdAllocationRequestView.DataSource = list.ToList();
                                grdAllocationRequestView.DataBind();
                            }
                            else
                            {
                                var list = (from s in allots
                                            where s.record_status != "N" && s.financial_year == user.financial_year
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
          //  Session["Allvalues"] = "030515c5_470,120_220,70";
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
             if (userid == "Admin")
            {
                allots = new List<Molasses_Allocation>();
                allots = BL_Molasses_Allocation.GetList();
                var list = (from s in allots
                            orderby Convert.ToDateTime(s.req_allotmentdate) descending
                            select s);
                grdAllocationRequestView.DataSource = list.ToList();
                grdAllocationRequestView.DataBind();
                grdAllocationRequestView.Columns[grdAllocationRequestView.Columns.Count - 2].Visible = false;
                for (int i = 0; i < grdAllocationRequestView.Rows.Count; i++)
                {
                    (grdAllocationRequestView.Rows[i].FindControl("btnEssue") as LinkButton).Visible = false;
                    if ((grdAllocationRequestView.Rows[i].FindControl("lblstatus") as Label).Text == "Pending")
                    {
                        (grdAllocationRequestView.Rows[i].FindControl("btnEdit") as LinkButton).Visible = true;
                    }
                }
            }
        else
            {
                allots = new List<Molasses_Allocation>();
                allots = BL_Molasses_Allocation.GetList();
                var list = (from s in allots
                            where s.party_code == Session["party_code"].ToString() 
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

            if (Session["allsearch"] != null && Session["alldtext"] != null)
            {
                ddsearch.SelectedValue = Session["allsearch"].ToString();
                txtpages.Text = Session["alldtext"].ToString();
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

                Session["allsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["alldtext"] = txtpage.Text;
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
                        if (ddsearch.SelectedValue == "req_allotmentdate")
                        {
                            var list = (from s in allots
                                        where s.party_code == user.party_code && s.req_allotmentdate==txtpage.Text
                                        orderby Convert.ToDateTime(s.req_allotmentdate) descending
                                        select s);
                            grdAllocationRequestView.DataSource = list.ToList();
                            grdAllocationRequestView.DataBind();
                        }
                        else if (user.user_id == "Admin")
                        {

                            var list = (from s in allots
                                        orderby Convert.ToDateTime(s.req_allotmentdate) descending
                                        select s);
                            grdAllocationRequestView.DataSource = list.ToList();
                            grdAllocationRequestView.DataBind();
                            grdAllocationRequestView.Columns[grdAllocationRequestView.Columns.Count - 2].Visible = false;
                            for (int i = 0; i < grdAllocationRequestView.Rows.Count; i++)
                            {
                                (grdAllocationRequestView.Rows[i].FindControl("btnEssue") as LinkButton).Visible = false;
                                if ((grdAllocationRequestView.Rows[i].FindControl("lblstatus") as Label).Text == "Pending" || (grdAllocationRequestView.Rows[i].FindControl("lblstatus") as Label).Text == "Alloted")
                                {
                                    (grdAllocationRequestView.Rows[i].FindControl("btnEdit") as LinkButton).Visible = true;
                                }
                            }
                        }
                        else
                        {
                            var list = (from s in allots
                                        where s.party_code == user.party_code && s.financial_year == user.financial_year
                                        orderby Convert.ToDateTime(s.req_allotmentdate) descending
                                        select s);
                            grdAllocationRequestView.DataSource = list.ToList();
                            grdAllocationRequestView.DataBind();
                        }
                           
                        grdAllocationRequestView.Columns[grdAllocationRequestView.Columns.Count - 2].Visible = false;
                        for (int i = 0; i < grdAllocationRequestView.Rows.Count; i++)
                        {
                            (grdAllocationRequestView.Rows[i].FindControl("btnEssue") as LinkButton).Visible = false;
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
            Session["allsearch"] = null;
            Session["alldtext"] = null;
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            Session["UserID"] = Session["UserID"];
            if (user != null)
            {
                Session["rolename"] = user.role_name;

                if (user.role_name == "Applicant")
                {
                    grdAllocationRequestView.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                    Session["party_code"] = user.party_code;
                    allots = new List<Molasses_Allocation>();
                    allots = BL_Molasses_Allocation.GetList();
                    var list = (from s in allots
                                where s.party_code == user.party_code && s.financial_year == user.financial_year
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
                else if (user.user_id == "Admin")
                {
                    allots = new List<Molasses_Allocation>();
                    allots = BL_Molasses_Allocation.GetList();
                    var list = (from s in allots
                                orderby Convert.ToDateTime(s.req_allotmentdate) descending
                                select s);
                    grdAllocationRequestView.DataSource = list.ToList();
                    grdAllocationRequestView.DataBind();
                    grdAllocationRequestView.Columns[grdAllocationRequestView.Columns.Count - 2].Visible = false;
                    for (int i = 0; i < grdAllocationRequestView.Rows.Count; i++)
                    {
                        (grdAllocationRequestView.Rows[i].FindControl("btnEssue") as LinkButton).Visible = false;
                        if ((grdAllocationRequestView.Rows[i].FindControl("lblstatus") as Label).Text == "Pending")
                        {
                            (grdAllocationRequestView.Rows[i].FindControl("btnEdit") as LinkButton).Visible = true;
                        }
                    }
                }
                else if (user.party_type == "All" || user.party_type == "ALL")
                {
                 
                    Response.Redirect("~/Allocation_P");
                    
                }
            }

        }
    }
}