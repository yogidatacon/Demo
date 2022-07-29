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
    public partial class IndentList : System.Web.UI.Page
    {
        List<Indent_Form> indents = new List<Indent_Form>();
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
                    Session["party"] = user.party_code;
                    if (user != null)
                    {
                        grdIndentforyMolassesView.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                        Session["UserID"] = Session["UserID"];
                        if (user.role_name == "Applicant")
                        {
                            Session["UserID"] = Session["UserID"];
                            indents = new List<Indent_Form>();
                            indents = BL_IndentForm.GetList();
                            var list = (from s in indents
                                        where s.party_code == user.party_code && s.financial_year==user.financial_year
                                        select s);
                            grdIndentforyMolassesView.DataSource = list.ToList();
                            grdIndentforyMolassesView.DataBind();
                        }
                      else  if (user.user_id == "Admin")
                        {
                          
                            Session["UserID"] = Session["UserID"];
                            indents = new List<Indent_Form>();
                            indents = BL_IndentForm.GetList();
                            var list = (from s in indents
                                        select s);
                            grdIndentforyMolassesView.DataSource = list.ToList();
                            grdIndentforyMolassesView.DataBind();
                        }
                        else if (user.party_type == "All" || user.party_type == "ALL")
                        {
                            List<WorkFlow> workflow = new List<WorkFlow>();
                            workflow = BL_WorkFlow.Checkworkflow("MA", "110", user.role_name_code.ToString(), "", user.id.ToString());
                            if (workflow.Count > 0)
                            {
                                Session["UserID"] = Session["UserID"];
                                Response.Redirect("~/Allocation_P.aspx");
                            }
                            else
                            {
                                Session["UserID"] = Session["UserID"];
                                Response.Redirect("~/User_Mgmt");
                            }
                        }
                        else
                        {
                            Session["UserID"] = Session["UserID"];
                            Response.Redirect("~/User_Mgmt");
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
                    Response.Redirect("~/LoginPage");
                }
            }
        }
        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "0";
            Response.Redirect("IndentForm");
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
            string Indent_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblIndentId") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblFincialyear") as Label).Text;
            Session["Ifinancial_year"] = financial_year;
            Session["Indent_id"] = Indent_id;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "1";
            Response.Redirect("IndentForm");
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string Indent_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblIndentId") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblFincialyear") as Label).Text;
            Session["Ifinancial_year"] = financial_year;
            Session["Indent_id"] = Indent_id;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "2";
            Response.Redirect("IndentForm");
        }
        protected void grdIndentforyMolassesView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["UserID"] != null)
            {
                if (e.NewPageIndex < 0)
                {
                    grdIndentforyMolassesView.PageIndex = 0;
                }
                else
                {
                    grdIndentforyMolassesView.PageIndex = e.NewPageIndex;
                }



                GridViewRow row = grdIndentforyMolassesView.TopPagerRow;
                TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                string userid = Session["UserID"].ToString();
                Session["UserID"] = userid;
                if (Session["indsearch"] != null && Session["indtext"] != null)
                {
                    ddsearch.SelectedValue = Session["indsearch"].ToString();
                    txtpage.Text = Session["indtext"].ToString();
                    if (ddsearch.SelectedValue != "Select")
                    {

                        if (txtpage != null)
                        {
                            if (user.role_name == "Applicant")
                            {
                                Session["UserID"] = Session["UserID"];
                                indents = new List<Indent_Form>();
                                indents = BL_IndentForm.Search("", ddsearch.SelectedValue, txtpage.Text);
                                var list = (from s in indents
                                            where s.party_code == user.party_code
                                            select s);
                                grdIndentforyMolassesView.DataSource = list.ToList();
                                grdIndentforyMolassesView.DataBind();
                            }
                            else if (user.party_type == "All" || user.party_type == "ALL")
                            {
                                List<WorkFlow> workflow = new List<WorkFlow>();
                                workflow = BL_WorkFlow.Checkworkflow("MA", "110", user.role_name_code.ToString(), "", user.id.ToString());
                                if (workflow.Count > 0)
                                {
                                    Session["UserID"] = Session["UserID"];
                                    Response.Redirect("~/AllocationRequestList");
                                }
                            }

                        }
                    }
                }
                else
                {
                    Session["UserID"] = Session["UserID"];
                    if (user != null)
                    {
                        if (user.role_name == "Applicant")
                        {
                            Session["UserID"] = Session["UserID"];
                            indents = new List<Indent_Form>();
                            indents = BL_IndentForm.GetList();
                            var list = (from s in indents
                                        where s.party_code == user.party_code
                                        select s);
                            grdIndentforyMolassesView.DataSource = list.ToList();
                            grdIndentforyMolassesView.DataBind();
                        }
                        else if (user.party_type == "All" || user.party_type == "ALL")
                        {
                            List<WorkFlow> workflow = new List<WorkFlow>();
                            workflow = BL_WorkFlow.Checkworkflow("MA", "110", user.role_name_code.ToString(), "", user.id.ToString());
                            if (workflow.Count > 0)
                            {
                                Session["UserID"] = Session["UserID"];
                                Response.Redirect("~/AllocationRequestList");
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
            string IndentNO = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblIndentId") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblFincialyear") as Label).Text;
            Session["Ifinancial_year"] = financial_year;
            Session["ReportId"] = "MF1";
            Session["UserID"] = Session["UserID"].ToString();
            Session["IndentNO"] = IndentNO;
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openModal", "window.open('ReportPage' ,'_blank');", true);
        }
        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdIndentforyMolassesView.TopPagerRow;
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
                grdIndentforyMolassesView.PageIndex = a - 1;
            }
            else
            {
                grdIndentforyMolassesView.PageIndex = a;
            }


            string userid = Session["UserID"].ToString();
            indents = new List<Indent_Form>();
            indents = BL_IndentForm.GetList();
            var list = (from s in indents
                        where s.party_code == Session["party"].ToString()
                        select s);
            grdIndentforyMolassesView.DataSource = list.ToList();
            grdIndentforyMolassesView.DataBind();


        }

        protected void grdIndentforyMolassesView_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdIndentforyMolassesView.TopPagerRow;
            if (grdIndentforyMolassesView.PageCount != 0)
            {
                grdIndentforyMolassesView.TopPagerRow.Visible = true;
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

            if (Session["indsearch"] != null && Session["indtext"] != null)
            {
                ddsearch.SelectedValue = Session["indsearch"].ToString();
                txtpages.Text = Session["indtext"].ToString();
            }

            //if (lblPages != null)
            //{
            lblPages.Text = grdIndentforyMolassesView.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdIndentforyMolassesView.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdIndentforyMolassesView.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdIndentforyMolassesView.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdIndentforyMolassesView.PageIndex == 0)
            {
                ((ImageButton)grdIndentforyMolassesView.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdIndentforyMolassesView.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdIndentforyMolassesView.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdIndentforyMolassesView.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdIndentforyMolassesView.PageIndex + 1 == grdIndentforyMolassesView.PageCount)
            {
                ((ImageButton)grdIndentforyMolassesView.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdIndentforyMolassesView.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdIndentforyMolassesView.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdIndentforyMolassesView.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {


            GridViewRow row = grdIndentforyMolassesView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["indsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["indtext"] = txtpage.Text;
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        indents = new List<Indent_Form>();
                        indents = BL_IndentForm.Search("", ddsearch.SelectedValue, txtpage.Text);
                        var list2 = from s in indents
                                    where s.party_code == user.party_code
                                    select s;
                        grdIndentforyMolassesView.DataSource = list2.ToList();
                        grdIndentforyMolassesView.DataBind();
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
            Session["indsearch"] = null;
            Session["indtext"] = null;
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            if (user != null)
            {
                grdIndentforyMolassesView.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                Session["UserID"] = Session["UserID"];
                if (user.role_name == "Applicant")
                {
                    Session["UserID"] = Session["UserID"];
                    indents = new List<Indent_Form>();
                    indents = BL_IndentForm.GetList();
                    var list = (from s in indents
                                where s.party_code == user.party_code
                                select s);
                    grdIndentforyMolassesView.DataSource = list.ToList();
                    grdIndentforyMolassesView.DataBind();
                }
                else if (user.party_type == "All" || user.party_type == "ALL")
                {
                    List<WorkFlow> workflow = new List<WorkFlow>();
                    workflow = BL_WorkFlow.Checkworkflow("MA", "110", user.role_name_code.ToString(), "", user.id.ToString());
                    if (workflow.Count > 0)
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("~/Allocation_P.aspx");
                    }
                    else
                    {
                        Session["UserID"] = Session["UserID"];
                        Response.Redirect("~/User_Mgmt");
                    }
                }
            }
        }
    }
}