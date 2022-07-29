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
    public partial class EmployeeList : System.Web.UI.Page
    {
        List<Employee_Details> des = new List<Employee_Details>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
                string userid = Session["UserID"].ToString();
                Session["UserID"] = userid;
                List<Employee_Details> des = new List<Employee_Details>();
                des = BL_Employee_Details.GetList();
                grdemployeelist.DataSource = des;
                grdemployeelist.DataBind();

            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string EmpID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblempid") as Label).Text;
          
            Session["UserID"] = Session["UserID"].ToString();
            Session["rType"] = 1;
            Session["EmpID"] = EmpID;
          
            Response.Redirect("EmployeeForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string EmpID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblempid") as Label).Text;

            Session["UserID"] = Session["UserID"].ToString();
            Session["rType"] = 2;
            Session["EmpID"] = EmpID;

            Response.Redirect("EmployeeForm");
        }

        protected void btnAddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Session["rtype"] = 0;
            Response.Redirect("EmployeeForm");
        }
        protected void UserRegistration_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("UserRegistrationList");
        }
        protected void Employee_Details_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("EmployeeList");
        }

        protected void Designation_1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("DepartmentMasterList.aspx");
        }

        protected void Designation_2_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("DesignationList");
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtSearch.Text == "")
                {
                    string userid = Session["UserID"].ToString();
                    Session["UserID"] = userid;
                    List<Employee_Details> des = new List<Employee_Details>();
                    des = BL_Employee_Details.GetList();
                    grdemployeelist.DataSource = des;
                    grdemployeelist.DataBind();
                    // (grdAccessTypeList.DataSource as DataTable).DefaultView.RowFilter = string.Format("access_type_name LIKE '{1}%' or access_type_desc LIKE '{2}%'  ", txtSearch.Text, txtSearch.Text);
                }
            }

        }

        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdemployeelist.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                List<Employee_Details> des = new List<Employee_Details>();
                des = BL_Employee_Details.GetList();
                grdemployeelist.DataSource = des;
                grdemployeelist.DataBind();
            }
            return des.ToString();
        }


        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GridViewRow row = grdemployeelist.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["esearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["etext"] = txtpage.Text;
                    string qery = txtSearch.Text.ToString();
                qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                string userid = Session["UserID"].ToString();
                Session["UserID"] = userid;
                List<Employee_Details> des = new List<Employee_Details>();
                des = BL_Employee_Details.SearchEmployee("employee_master",ddsearch.SelectedValue,txtpage.Text);
                grdemployeelist.DataSource = des;
                grdemployeelist.DataBind();

            }
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Select Field Name\');", true);
                ddsearch.Focus();
            }


        }

        protected void grdemployeelist_DataBound(object sender, EventArgs e)
        {
            {
                GridViewRow row = grdemployeelist.TopPagerRow;
                if (grdemployeelist.Rows.Count > 0)
                {
                    grdemployeelist.TopPagerRow.Visible = true;
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

                //if (lblPages != null)
                //{
                lblPages.Text = grdemployeelist.PageCount.ToString();
                //}

                //if (lblCurrent != null)
                //{
                int currentPage = grdemployeelist.PageIndex + 1;
                lblCurrent.Text = currentPage.ToString();
                txtpage.Text = currentPage.ToString();
                if (Session["esearch"] != null && Session["etext"] != null)
                {
                    ddsearch.SelectedValue = Session["esearch"].ToString();
                    txtpages.Text = Session["etext"].ToString();
                }
                //}

                if (DDLPage != null)
                {
                    for (int i = 0; i < grdemployeelist.PageCount; i++)
                    {
                        int pageNumber = i + 1;
                        ListItem item = new ListItem(pageNumber.ToString());
                        if (i == grdemployeelist.PageIndex)
                        {
                            item.Selected = true;
                        }
                        DDLPage.Items.Add(item);
                    }
                }

                //-- For First and Previous ImageButton
                if (grdemployeelist.PageIndex == 0)
                {
                    ((ImageButton)grdemployeelist.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                    ((ImageButton)grdemployeelist.TopPagerRow.FindControl("btnFirst")).Visible = false;

                    ((ImageButton)grdemployeelist.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                    ((ImageButton)grdemployeelist.TopPagerRow.FindControl("btnPrev")).Visible = false;

                    //--- OR ---\\
                    //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                    //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                    //btnFirst.Visible = false;
                    //btnPrev.Visible = false;

                }

                //-- For Last and Next ImageButton
                if (grdemployeelist.PageIndex + 1 == grdemployeelist.PageCount)
                {
                    ((ImageButton)grdemployeelist.TopPagerRow.FindControl("btnLast")).Enabled = true;
                    ((ImageButton)grdemployeelist.TopPagerRow.FindControl("btnLast")).Visible = false;

                    ((ImageButton)grdemployeelist.TopPagerRow.FindControl("btnNext")).Enabled = true;
                    ((ImageButton)grdemployeelist.TopPagerRow.FindControl("btnNext")).Visible = false;

                    //--- OR ---\\
                    //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                    //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                    //btnLast.Visible = false;
                    //btnNext.Visible = false;
                }
            }
        }


        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdemployeelist.TopPagerRow;
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
                grdemployeelist.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdemployeelist.PageIndex = a - 1;
            }


            string userid = Session["UserID"].ToString();
            //Session["UserID"] = userid;
            List<Employee_Details> des = new List<Employee_Details>();
            des = BL_Employee_Details.GetList();
            grdemployeelist.DataSource = des;
            grdemployeelist.DataBind();


        }

        protected void grdemployeelist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewRow row = grdemployeelist.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (e.NewPageIndex < 0)
            {
                grdemployeelist.PageIndex = 0;
            }
            else
            {
                grdemployeelist.PageIndex = e.NewPageIndex;
            }

            string userid = Session["UserID"].ToString();
            Session["UserID"] = userid;
            if (Session["esearch"] != null && Session["etext"] != null)
            {
                ddsearch.SelectedValue = Session["esearch"].ToString();
                txtpage.Text = Session["etext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        string qery = txtSearch.Text.ToString();
                        qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


                        List<Employee_Details> des = new List<Employee_Details>();
                        des = BL_Employee_Details.SearchEmployee("employee_master", ddsearch.SelectedValue, txtpage.Text);
                        grdemployeelist.DataSource = des;
                        grdemployeelist.DataBind();
                        ddsearch.SelectedValue = Session["esearch"].ToString();
                        txtpage.Text = Session["etext"].ToString();
                    }
                }
            }
            else
            {

                List<Employee_Details> des = new List<Employee_Details>();
                des = BL_Employee_Details.GetList();
                grdemployeelist.DataSource = des;
                grdemployeelist.DataBind();


            }
        }
        }
}