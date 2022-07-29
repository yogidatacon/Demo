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
    public partial class ComplaintList : System.Web.UI.Page
    {
        List<Complaint> allots = new List<Complaint>();
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

                        //if (user.role_name == "Applicant")
                        //{
                        //    grdAllocationRequestView.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                        //    Session["party_code"] = user.party_code;
                        //    allots = new List<Complaint>();
                        //    allots = BL_Complaint.Getlist();
                        //    var list = (from s in allots
                        //                select s);
                        //    grdAllocationRequestView.DataSource = list.ToList();
                        //    grdAllocationRequestView.DataBind();
                        //    grdAllocationRequestView.Columns[grdAllocationRequestView.Columns.Count - 2].Visible = false;
                        //    for (int i = 0; i < grdAllocationRequestView.Rows.Count; i++)
                        //    {
                        //        (grdAllocationRequestView.Rows[i].FindControl("btnEssue") as LinkButton).Visible = false;
                        //    }
                        //}
                        //else 
                        if (user.party_type == "All" || user.party_type == "ALL")
                        {
                            grdAllocationRequestView.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                            Session["party_code"] = user.party_code;
                            allots = new List<Complaint>();
                            allots = BL_Complaint.Getlist();
                            var list = (from s in allots
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
            Response.Redirect("ComplaintForm.aspx");
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
            string complaint_ID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblIndentId") as Label).Text;
            Session["Complaint_ID"] = complaint_ID;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "1";
            Response.Redirect("ComplaintForm.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string complaint_ID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblIndentId") as Label).Text;
            Session["Complaint_ID"] = complaint_ID;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "2";
            Response.Redirect("ComplaintForm.aspx");
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

                    if (Session["cmpsearch"] != null && Session["cmptext"] != null)
                    {
                        ddsearch.SelectedValue = Session["cmpsearch"].ToString();
                        txtpage.Text = Session["cmptext"].ToString();
                        if (ddsearch.SelectedValue != "Select")
                        {

                            if (txtpage != null)
                            {
                                if (ddsearch.SelectedValue == "req_allotmentdate")
                                {
                                    if (txtpage.Text.ToString().Length == 10)
                                    {


                                        allots = BL_Complaint.Getlist();
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                        txtpage.Focus();
                                        txtpage.Text = "";
                                        ddsearch.SelectedIndex = 0;
                                        allots = BL_Complaint.Getlist();
                                    }
                                }
                                else
                                {
                                    allots = new List<Complaint>();
                                    allots = BL_Complaint.Search("", ddsearch.SelectedValue, txtpage.Text);
                                }

                            }
                        }
                    }
                    else
                    {
                        allots = new List<Complaint>();
                        allots = BL_Complaint.Getlist();
                    }
                    if (user.role_name == "Applicant")
                    {

                        if (ddsearch.SelectedValue == "req_allotmentdate")
                        {
                            var list = (from s in allots
                                        //where s.party_code == user.party_code && s.req_allotmentdate == txtpage.Text && s.financial_year == user.financial_year
                                        //orderby Convert.ToDateTime(s.req_allotmentdate) descending
                                        select s);
                            grdAllocationRequestView.DataSource = list.ToList();
                            grdAllocationRequestView.DataBind();
                        }
                        else
                        {
                            var list = (from s in allots
                                        //where s.party_code == user.party_code && s.financial_year == user.financial_year
                                        //orderby Convert.ToDateTime(s.req_allotmentdate) descending
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
                    else if (user.party_type == "All" || user.party_type == "ALL")
                    {
                       

                            if (ddsearch.SelectedValue == "req_allotmentdate")
                            {
                                var list = (from s in allots
                                            //where s.record_status != "N" && s.req_allotmentdate == txtpage.Text && s.financial_year == user.financial_year
                                            //orderby Convert.ToDateTime(s.req_allotmentdate) descending
                                            select s);
                                grdAllocationRequestView.DataSource = list.ToList();
                                grdAllocationRequestView.DataBind();
                            }
                            else
                            {
                                var list = (from s in allots
                                            //where s.record_status != "N" && s.financial_year == user.financial_year
                                            //orderby Convert.ToDateTime(s.req_allotmentdate) descending
                                            select s);
                                grdAllocationRequestView.DataSource = list.ToList();
                                grdAllocationRequestView.DataBind();
                            }
                            grdAllocationRequestView.Columns[grdAllocationRequestView.Columns.Count - 3].Visible = false;
                           
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
        

        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdAllocationRequestView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            if (Convert.ToInt32(txtpage.Text) <= 0)
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
           
                allots = new List<Complaint>();
                allots = BL_Complaint.Getlist();
                var list = (from s in allots
                            //where s.party_code == Session["party_code"].ToString()
                            //orderby Convert.ToDateTime(s.req_allotmentdate) descending
                            select s);
                grdAllocationRequestView.DataSource = list.ToList();
                grdAllocationRequestView.DataBind();
                grdAllocationRequestView.Columns[grdAllocationRequestView.Columns.Count - 2].Visible = false;
                for (int i = 0; i < grdAllocationRequestView.Rows.Count; i++)
                {
                    (grdAllocationRequestView.Rows[i].FindControl("btnEssue") as LinkButton).Visible = false;
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

            if (Session["cmpsearch"] != null && Session["cmptext"] != null)
            {
                ddsearch.SelectedValue = Session["cmpsearch"].ToString();
                txtpages.Text = Session["cmptext"].ToString();
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

                Session["cmpsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["cmptext"] = txtpage.Text;
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {

                        if (ddsearch.SelectedValue == "req_allotmentdate")
                        {
                            if (txtpage.Text.ToString().Length == 10)
                            {


                                allots = BL_Complaint.Getlist();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please enter full date\');", true);
                                txtpage.Focus();
                                txtpage.Text = "";
                                ddsearch.SelectedIndex = 0;

                                allots =BL_Complaint.Getlist();
                            }
                        }
                        else
                        {
                            allots = new List<Complaint>();
                            allots = BL_Complaint.Search("", ddsearch.SelectedValue, txtpage.Text);
                        }
                        if (ddsearch.SelectedValue == "req_allotmentdate")
                        {
                            var list = (from s in allots
                                        //where s.party_code == user.party_code && s.req_allotmentdate == txtpage.Text
                                        //orderby Convert.ToDateTime(s.req_allotmentdate) descending
                                        select s);
                            grdAllocationRequestView.DataSource = list.ToList();
                            grdAllocationRequestView.DataBind();
                        }
                        else if (user.user_id == "Admin")
                        {

                            var list = (from s in allots
                            //            orderby Convert.ToDateTime(s.req_allotmentdate) descending
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
                                        //where s.party_code == user.party_code && s.financial_year == user.financial_year
                                        //orderby Convert.ToDateTime(s.req_allotmentdate) descending
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
            Session["cmpsearch"] = null;
            Session["cmptext"] = null;
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
                    allots = new List<Complaint>();
                    allots = BL_Complaint.Getlist();
                    var list = (from s in allots
                                
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
                    grdAllocationRequestView.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                    Session["party_code"] = user.party_code;
                    allots = new List<Complaint>();
                    allots = BL_Complaint.Getlist();
                    var list = (from s in allots

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

        }
    }
}