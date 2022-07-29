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
    public partial class RoleLevelList : System.Web.UI.Page
    {
        List<RoleLevel> rolelevels = new List<RoleLevel>();
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
                grdRoleLevelList.PageSize=Convert.ToInt32(Session["pagesize"].ToString());
                string userid = Session["UserID"].ToString();
                rolelevels = new List<RoleLevel>();
                rolelevels = BL_User_Mgnt.GetRoleLevels(userid);
                grdRoleLevelList.DataSource = rolelevels;
                grdRoleLevelList.DataBind();
            }
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserId"] = Session["UserID"].ToString();
            Session["rtype"] = "0";
            Response.Redirect("~/RoleLevelMasterForm");
        }

        protected void StateMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StateList");
        }
        protected void ThanaMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ThanaMasterList");
        }
        protected void DivisionMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DivisionList");
        }

        protected void DistrictMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Districtlist");

        }

        protected void RoleLevelMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/RoleLevelList");
        }

        protected void AccessTypeMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AccessTypeList");
        }

        protected void RoleMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/RoleMasterList1");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string level_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblRoleCode") as Label).Text;
            string levelName = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblRoleName") as Label).Text;
            string Description = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblRoleDescription") as Label).Text;
            Session["UserId"] = Session["UserID"].ToString();
            Session["rtype"] = "1";
            Session["Description"] = Description;
            Session["Level_name"] = levelName;
            Session["Level_Id"] = level_id;
            Response.Redirect("~/RoleLevelMasterForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string level_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblRoleCode") as Label).Text;
            string levelName = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblRoleName") as Label).Text;
            string Description = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblRoleDescription") as Label).Text;
            Session["UserId"] = Session["UserID"].ToString();
            Session["rtype"] = "2";
            Session["Description"] = Description;
            Session["Level_name"] = levelName;
            Session["Level_Id"] = level_id;
            Response.Redirect("~/RoleLevelMasterForm");
        }

        protected void grdRoleLevelList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            if (e.NewPageIndex < 0)
            {
                grdRoleLevelList.PageIndex = 0;
            }
            else
            {
                grdRoleLevelList.PageIndex = e.NewPageIndex;
            }

            GridViewRow row = grdRoleLevelList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["rsearch"] != null && Session["rtext"] != null)
            {
                ddsearch.SelectedValue = Session["rsearch"].ToString();
                txtpage.Text = Session["rtext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        string qery = txtSearch.Text.ToString();
                        qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


                        rolelevels = new List<RoleLevel>();
                        rolelevels = BL_User_Mgnt.SearchroleLevel("role_level_master", ddsearch.SelectedValue, txtpage.Text);
                        grdRoleLevelList.DataSource = rolelevels;
                        grdRoleLevelList.DataBind();
                    }
                }
            }
            else
            {


                rolelevels = new List<RoleLevel>();
                rolelevels = BL_User_Mgnt.GetRoleLevels(Session["UserId"].ToString());
                grdRoleLevelList.DataSource = rolelevels;
                grdRoleLevelList.DataBind();
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdRoleLevelList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                string userid = Session["UserID"].ToString();
                    rolelevels = new List<RoleLevel>();
                    rolelevels = BL_User_Mgnt.GetRoleLevels(userid);
                    grdRoleLevelList.DataSource = rolelevels;
                    grdRoleLevelList.DataBind();
                }
            

        }
        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdRoleLevelList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                string userid = Session["UserID"].ToString();
                rolelevels = new List<RoleLevel>();
                rolelevels = BL_User_Mgnt.GetRoleLevels(userid);
                grdRoleLevelList.DataSource = rolelevels;
                grdRoleLevelList.DataBind();
            }
            return rolelevels.ToString();
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GridViewRow row = grdRoleLevelList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["rsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
            {
                    Session["rtext"] = txtpage.Text;
                    string qery = txtSearch.Text.ToString();
                    qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


                    string userid = Session["UserID"].ToString();
                    rolelevels = new List<RoleLevel>();
                    rolelevels = BL_User_Mgnt.SearchroleLevel("role_level_master",ddsearch.SelectedValue,txtpage.Text);
                    grdRoleLevelList.DataSource = rolelevels;
                    grdRoleLevelList.DataBind();


                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Select Filed Name\');", true);
                ddsearch.Focus();
            }


        }

        protected void grdRoleLevelList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdRoleLevelList.TopPagerRow;
            if (grdRoleLevelList.Rows.Count > 0)
            {
                grdRoleLevelList.TopPagerRow.Visible = true;
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
            lblPages.Text = grdRoleLevelList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdRoleLevelList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            if (Session["rsearch"] != null && Session["rtext"] != null)
            {
                ddsearch.SelectedValue = Session["rsearch"].ToString();
                txtpages.Text = Session["rtext"].ToString();
            }
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdRoleLevelList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdRoleLevelList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdRoleLevelList.PageIndex == 0)
            {
                ((ImageButton)grdRoleLevelList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdRoleLevelList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdRoleLevelList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdRoleLevelList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdRoleLevelList.PageIndex + 1 == grdRoleLevelList.PageCount)
            {
                ((ImageButton)grdRoleLevelList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdRoleLevelList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdRoleLevelList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdRoleLevelList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        
    }
        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdRoleLevelList.TopPagerRow;
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
                grdRoleLevelList.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdRoleLevelList.PageIndex = a - 1;
            }
            

            string userid = Session["UserID"].ToString();
            rolelevels = new List<RoleLevel>();
            rolelevels = BL_User_Mgnt.GetRoleLevels(userid);
            grdRoleLevelList.DataSource = rolelevels;
            grdRoleLevelList.DataBind();


        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["rsearch"] = null;
            Session["rtext"] = null;
            string userid = Session["UserID"].ToString();
            rolelevels = new List<RoleLevel>();
            rolelevels = BL_User_Mgnt.GetRoleLevels(userid);
            grdRoleLevelList.DataSource = rolelevels;
            grdRoleLevelList.DataBind();
        }
    }
}