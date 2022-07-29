using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class workflowlist : System.Web.UI.Page
    {
        List<WorkFlow> workflow = new List<WorkFlow>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null)
                {

                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                }
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/LoginPage");
                }
                grdWorkFlowDetailsList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());

                string user_id = Session["UserID"].ToString();
                workflow = new List<WorkFlow>();
                workflow = BL_WorkFlow.Getworkflowlist(user_id);
                grdWorkFlowDetailsList.DataSource = workflow;
                grdWorkFlowDetailsList.DataBind();
            }
        }

        protected void WorkFlowDetails_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("workflowList");
        }

        protected void grdWorkFlowDetailsList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0)
            {
                grdWorkFlowDetailsList.PageIndex = 0;
            }
            else
            {
                grdWorkFlowDetailsList.PageIndex = e.NewPageIndex;
            }

            GridViewRow row = grdWorkFlowDetailsList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["wsearch"] != null && Session["wtext"] != null)
            {
                ddsearch.SelectedValue = Session["wsearch"].ToString();
                txtpage.Text = Session["wtext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        string qery = txtSearch.Text.ToString();
                        qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


                        workflow = new List<WorkFlow>();
                        workflow = BL_WorkFlow.SearchWorkFlow("workflow", ddsearch.SelectedValue, txtpage.Text);
                        grdWorkFlowDetailsList.DataSource = workflow;
                        grdWorkFlowDetailsList.DataBind();
                    }
                }
            }
            else
            {


                workflow = new List<WorkFlow>();
                workflow = BL_WorkFlow.Getworkflowlist(Session["UserID"].ToString());
                grdWorkFlowDetailsList.DataSource = workflow;
                grdWorkFlowDetailsList.DataBind();
            }
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Session["rType"] = 0;
            Response.Redirect("workflowForm");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text;
           
            Session["UserID"] = Session["UserID"].ToString();
            Session["id"] = id;
            Session["rType"] = 1;
            Response.Redirect("workflowForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text;
           
            Session["UserID"] = Session["UserID"].ToString();
            Session["id"] = id;
            Session["rType"] = 2;
            Response.Redirect("workflowForm");
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtSearch.Text == "")
                {
                    string user_id = Session["UserID"].ToString();
                    workflow = new List<WorkFlow>();
                    workflow = BL_WorkFlow.Getworkflowlist(user_id);
                    grdWorkFlowDetailsList.DataSource = workflow;
                    grdWorkFlowDetailsList.DataBind();
                    // (grdAccessTypeList.DataSource as DataTable).DefaultView.RowFilter = string.Format("access_type_name LIKE '{1}%' or access_type_desc LIKE '{2}%'  ", txtSearch.Text, txtSearch.Text);
                }
            }

        }

        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdWorkFlowDetailsList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                string userid = Session["UserID"].ToString();
                workflow = new List<WorkFlow>();
                workflow = BL_WorkFlow.Getworkflowlist(userid);
                grdWorkFlowDetailsList.DataSource = workflow;
                grdWorkFlowDetailsList.DataBind();
            }
            return workflow.ToString();
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GridViewRow row = grdWorkFlowDetailsList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["wsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["wtext"] = txtpage.Text;
                    string qery = txtSearch.Text.ToString();
                    qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                    string user_id = Session["UserID"].ToString();
                    workflow = new List<WorkFlow>();
                    workflow = BL_WorkFlow.SearchWorkFlow("workflow", ddsearch.SelectedValue,txtpage.Text);
                    grdWorkFlowDetailsList.DataSource = workflow;
                    grdWorkFlowDetailsList.DataBind();

                }


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Select Filed Name\');", true);
                ddsearch.Focus();
            }
        }




        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdWorkFlowDetailsList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            if (Convert.ToInt32(txtpage.Text) < 1)
            {
                a = 0;
            }
            else
            {
                a = Convert.ToInt32(txtpage.Text);
            }

            if (Convert.ToInt32(txtpage.Text) <= 0)
            {
                grdWorkFlowDetailsList.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdWorkFlowDetailsList.PageIndex = a - 1;
            }

            

            string user_id = Session["UserID"].ToString();
            workflow = new List<WorkFlow>();
            workflow = BL_WorkFlow.Getworkflowlist(user_id);
            grdWorkFlowDetailsList.DataSource = workflow;
            grdWorkFlowDetailsList.DataBind();


        }

        protected void grdWorkFlowDetailsList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdWorkFlowDetailsList.TopPagerRow;
            if (grdWorkFlowDetailsList.Rows.Count > 0)
            {
                grdWorkFlowDetailsList.TopPagerRow.Visible = true;
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
            if (Session["wsearch"] != null && Session["wtext"] != null)
            {
                ddsearch.SelectedValue = Session["wsearch"].ToString();
                txtpages.Text = Session["wtext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdWorkFlowDetailsList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdWorkFlowDetailsList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdWorkFlowDetailsList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdWorkFlowDetailsList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdWorkFlowDetailsList.PageIndex == 0)
            {
                ((ImageButton)grdWorkFlowDetailsList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdWorkFlowDetailsList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdWorkFlowDetailsList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdWorkFlowDetailsList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdWorkFlowDetailsList.PageIndex + 1 == grdWorkFlowDetailsList.PageCount)
            {
                ((ImageButton)grdWorkFlowDetailsList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdWorkFlowDetailsList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdWorkFlowDetailsList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdWorkFlowDetailsList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["wsearch"] = null;
            Session["wtext"] = null;
            string user_id = Session["UserID"].ToString();
            workflow = new List<WorkFlow>();
            workflow = BL_WorkFlow.Getworkflowlist(user_id);
            grdWorkFlowDetailsList.DataSource = workflow;
            grdWorkFlowDetailsList.DataBind();
        }
    }
}