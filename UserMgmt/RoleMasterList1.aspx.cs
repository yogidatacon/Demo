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
    public partial class RoleMasterList1 : System.Web.UI.Page
    {
        List<RoleMaster> rolemaster = new List<RoleMaster>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grdRoleMasterList1.PageSize= Convert.ToInt32(Session["pagesize"].ToString());
                string userid = Session["UserID"].ToString();
                rolemaster = new List<RoleMaster>();
                rolemaster = BL_User_Mgnt.GetRoleMasterList(userid);
                grdRoleMasterList1.DataSource = rolemaster;
                grdRoleMasterList1.DataBind();
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string rolename = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblRoleName1") as Label).Text;
            string role_level_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrolelevel_code") as Label).Text;
            string role_access_type_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblaccestype_code") as Label).Text;
            string nexrole_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblnextroleCode") as Label).Text;
            string org_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblorg_id") as Label).Text;
            string role_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text;
            string strenth = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblstrenth") as Label).Text;
            Session["UserId"] = Session["UserID"].ToString();
            Session["rtype"] = "1";
            Session["rolename"] = rolename;
            Session["role_level_code"] = role_level_code;
            Session["role_access_type_code"] = role_access_type_code;
            Session["nexrole_code"] = nexrole_code;
            Session["org_id"] = org_id;
            Session["role_id"] = role_id;
            Session["strenth"] = strenth;
            Response.Redirect("~/RoleMasterForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string rolename = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblRoleName1") as Label).Text;
            string role_level_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblrolelevel_code") as Label).Text;
            string role_access_type_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblaccestype_code") as Label).Text;
            string nexrole_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblnextroleCode") as Label).Text;
            string org_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblorg_id") as Label).Text;
            string role_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text;
            string strenth = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblstrenth") as Label).Text;
            Session["UserId"] = Session["UserID"].ToString();
            Session["rtype"] = "2";
            Session["rolename"] = rolename;
            Session["role_level_code"] = role_level_code;
            Session["role_access_type_code"] = role_access_type_code;
            Session["nexrole_code"] = nexrole_code;
            Session["org_id"] = org_id;
            Session["role_id"] = role_id;
            Session["strenth"] = strenth;
            Response.Redirect("~/RoleMasterForm");
        }
        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserId"] = Session["UserID"].ToString();
            Session["rtype"] = "0";
            Response.Redirect("~/RoleMasterForm");
        }
        protected void StateMaster_Click(object sender, EventArgs e)
        {
            Session["UserId"] = Session["UserID"].ToString();
            Response.Redirect("~/StateList");
        }

        protected void DivisionMaster_Click(object sender, EventArgs e)
        {
            Session["UserId"] = Session["UserID"].ToString();
            Response.Redirect("~/DivisionList");
        }

        protected void DistrictMaster_Click(object sender, EventArgs e)
        {
            Session["UserId"] = Session["UserID"].ToString();
            Response.Redirect("~/Districtlist");

        }
        protected void ThanaMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ThanaMasterList");
        }
        protected void RoleLevelMaster_Click(object sender, EventArgs e)
        {
            Session["UserId"] = Session["UserID"].ToString();
            Response.Redirect("~/RoleLevelList");
        }

        protected void AccessTypeMaster_Click(object sender, EventArgs e)
        {
            Session["UserId"] = Session["UserID"].ToString();
            Response.Redirect("~/AccessTypeList");
        }

        protected void RoleMaster_Click(object sender, EventArgs e)
        {
            Session["UserId"] = Session["UserID"].ToString();
            Response.Redirect("~/RoleMasterList1");
        }

        protected void grdRoleMasterList1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewRow row = grdRoleMasterList1.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (e.NewPageIndex < 0)
            {
                grdRoleMasterList1.PageIndex = 0;
            }
            else
            {
                grdRoleMasterList1.PageIndex = e.NewPageIndex;
            }

            string userid = Session["UserID"].ToString();
            Session["UserID"] = userid;
            if (Session["rmsearch"] != null && Session["rmtext"] != null)
            {
                ddsearch.SelectedValue = Session["rmsearch"].ToString();
                txtpage.Text = Session["rmtext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {
                    if (txtpage != null)
                    {
                        string qery = txtSearch.Text.ToString();
                        qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                        rolemaster = new List<RoleMaster>();
                        rolemaster = BL_User_Mgnt.SearchRoleMaster("view_Role_master", ddsearch.SelectedValue, txtpage.Text);
                        grdRoleMasterList1.DataSource = rolemaster;
                        grdRoleMasterList1.DataBind();

                    }
                }
            }
            else
            {
                rolemaster = new List<RoleMaster>();
                rolemaster = BL_User_Mgnt.GetRoleMasterList(Session["UserID"].ToString());
                grdRoleMasterList1.DataSource = rolemaster;
                grdRoleMasterList1.DataBind();
            }
        }
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdRoleMasterList1.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                string userid = Session["UserID"].ToString();
                    rolemaster = new List<RoleMaster>();
                    rolemaster = BL_User_Mgnt.GetRoleMasterList(userid);
                    grdRoleMasterList1.DataSource = rolemaster;
                    grdRoleMasterList1.DataBind();
                }
            

        }
        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdRoleMasterList1.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                string userid = Session["UserID"].ToString();
                rolemaster = new List<RoleMaster>();
                rolemaster = BL_User_Mgnt.GetRoleMasterList(userid);
                grdRoleMasterList1.DataSource = rolemaster;
                grdRoleMasterList1.DataBind();
            }
            return rolemaster.ToString();
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {

           
            GridViewRow row = grdRoleMasterList1.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(txtpage);
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["rmsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
            {
                    Session["rmtext"] = txtpage.Text;
                    string qery = txtSearch.Text.ToString();
                qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());



                rolemaster = new List<RoleMaster>();
                rolemaster = BL_User_Mgnt.SearchRoleMaster("view_Role_master",ddsearch.SelectedValue,txtpage.Text);
                grdRoleMasterList1.DataSource = rolemaster;
                grdRoleMasterList1.DataBind();

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
            GridViewRow row = grdRoleMasterList1.TopPagerRow;
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
                grdRoleMasterList1.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdRoleMasterList1.PageIndex = a - 1;
            }
            

            string userid = Session["UserID"].ToString();
            rolemaster = new List<RoleMaster>();
            rolemaster = BL_User_Mgnt.GetRoleMasterList(userid);
            grdRoleMasterList1.DataSource = rolemaster;
            grdRoleMasterList1.DataBind();


        }

        protected void grdRoleMasterList1_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdRoleMasterList1.TopPagerRow;
            if (grdRoleMasterList1.Rows.Count > 0)
            {
                grdRoleMasterList1.TopPagerRow.Visible = true;
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
            lblPages.Text = grdRoleMasterList1.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdRoleMasterList1.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            if (Session["rmsearch"] != null && Session["rmtext"] != null)
            {
                ddsearch.SelectedValue = Session["rmsearch"].ToString();
                txtpages.Text = Session["rmtext"].ToString();
            }
                //}

                if (DDLPage != null)
            {
                for (int i = 0; i < grdRoleMasterList1.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdRoleMasterList1.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdRoleMasterList1.PageIndex == 0)
            {
                ((ImageButton)grdRoleMasterList1.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdRoleMasterList1.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdRoleMasterList1.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdRoleMasterList1.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdRoleMasterList1.PageIndex + 1 == grdRoleMasterList1.PageCount)
            {
                ((ImageButton)grdRoleMasterList1.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdRoleMasterList1.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdRoleMasterList1.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdRoleMasterList1.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void ddlsearch1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdRoleMasterList1.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            txtpage.Text = "";
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["rmsearch"] = null;
            Session["rmtext"] = null;
            string userid = Session["UserID"].ToString();
            rolemaster = new List<RoleMaster>();
            rolemaster = BL_User_Mgnt.GetRoleMasterList(userid);
            grdRoleMasterList1.DataSource = rolemaster;
            grdRoleMasterList1.DataBind();
        }
    }
}