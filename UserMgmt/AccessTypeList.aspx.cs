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
    public partial class AccessTypeList : System.Web.UI.Page
    {
        List<AccessType> accesstypelist = new List<AccessType>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grdAccessTypeList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                string strPreviousPage = "";
                if (Request.UrlReferrer != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                }
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/LoginPage");
                }
                grdAccessTypeList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                string userid = Session["UserID"].ToString();
                accesstypelist = new List<AccessType>();
                accesstypelist = BL_User_Mgnt.GetAccessTypeList(userid);
                grdAccessTypeList.DataSource = accesstypelist;
                grdAccessTypeList.DataBind();
            }
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserId"] = Session["UserID"].ToString();
            Session["rtype"] = "0";
            Response.Redirect("~/AccessTypeFrom");
        }

        protected void StateMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StateList");
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
        protected void ThanaMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ThanaMasterList");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string level_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblAccessTypeCode") as Label).Text;
            string levelName = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblAccessTypeName") as Label).Text;
            string Description = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblRoleDescription") as Label).Text;
            Session["UserId"] = Session["UserID"].ToString();
            Session["rtype"] = "1";
            Session["Description"] = Description;
            Session["AccessTypeName"] = levelName;
            Session["AccessTypeCode"] = level_id;
            Response.Redirect("~/AccessTypeFrom");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string level_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblAccessTypeCode") as Label).Text;
            string levelName = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblAccessTypeName") as Label).Text;
            string Description = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblRoleDescription") as Label).Text;
            Session["UserId"] = Session["UserID"].ToString();
            Session["rtype"] = "2";
            Session["Description"] = Description;
            Session["AccessTypeName"] = levelName;
            Session["AccessTypeCode"] = level_id;
            Response.Redirect("~/AccessTypeFrom");
        }

        protected void grdAccessTypeList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewRow row = grdAccessTypeList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (e.NewPageIndex < 0)
            {
                grdAccessTypeList.PageIndex = 0;
            }
            else
            {
                grdAccessTypeList.PageIndex = e.NewPageIndex;
            }

            string userid = Session["UserID"].ToString();
            Session["UserID"] = userid;
            if (Session["asearch"] != null && Session["atext"] != null)
            {
                ddsearch.SelectedValue = Session["asearch"].ToString();
                txtpage.Text = Session["atext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        string qery = txtSearch.Text.ToString();
                        qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


                        accesstypelist = new List<AccessType>();
                        accesstypelist = BL_User_Mgnt.SearchExistsData("access_type_master", ddsearch.SelectedValue, txtpage.Text);
                        grdAccessTypeList.DataSource = accesstypelist;
                        grdAccessTypeList.DataBind();
                        ddsearch.SelectedValue = Session["asearch"].ToString();
                        txtpage.Text = Session["atext"].ToString();
                    }
                }
            }
            else
            {


              
                grdAccessTypeList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                accesstypelist = new List<AccessType>();
                accesstypelist = BL_User_Mgnt.GetAccessTypeList(Session["UserId"].ToString());
                grdAccessTypeList.DataSource = accesstypelist;
                grdAccessTypeList.DataBind();
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdAccessTypeList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                accesstypelist = new List<AccessType>();
                    accesstypelist = BL_User_Mgnt.GetAccessTypeList(Session["UserID"].ToString());
                    grdAccessTypeList.DataSource = accesstypelist;
                    grdAccessTypeList.DataBind();
                    // (grdAccessTypeList.DataSource as DataTable).DefaultView.RowFilter = string.Format("access_type_name LIKE '{1}%' or access_type_desc LIKE '{2}%'  ", txtSearch.Text, txtSearch.Text);
                }
           

        }
         string accesstype = "";
        [WebMethod]
        public  string chkDuplicateAccessTypeName(Object accesstypename)
        {
            int value = 0;
            GridViewRow row = grdAccessTypeList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                accesstypelist = new List<AccessType>();
                accesstypelist = BL_User_Mgnt.GetAccessTypeList(Session["UserID"].ToString());
                grdAccessTypeList.DataSource = accesstypelist;
                grdAccessTypeList.DataBind();
            }
            return accesstypelist.ToString();
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GridViewRow row = grdAccessTypeList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["asearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["atext"] = txtpage.Text;
                    string qery = txtSearch.Text.ToString();
                qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


                accesstypelist = new List<AccessType>();
                accesstypelist = BL_User_Mgnt.SearchExistsData("access_type_master", ddsearch.SelectedValue, txtpage.Text);
                grdAccessTypeList.DataSource = accesstypelist;
                grdAccessTypeList.DataBind();
            }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Select Field Name\');", true);
                ddsearch.Focus();
            }


        }

        
        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdAccessTypeList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            if (Convert.ToInt32(txtpage.Text) <= 1)
            {
                a = 0;
            }
            else
            {
                a = Convert.ToInt32(txtpage.Text);
            }
            if (Convert.ToInt32(txtpage.Text) <= 0)
            {
                grdAccessTypeList.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdAccessTypeList.PageIndex = a - 1;
            }
            

            string userid = Session["UserID"].ToString();
            accesstypelist = new List<AccessType>();
            accesstypelist = BL_User_Mgnt.GetAccessTypeList(userid);
            grdAccessTypeList.DataSource = accesstypelist;
            grdAccessTypeList.DataBind();


        }

        protected void grdAccessTypeList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdAccessTypeList.TopPagerRow;
            if (grdAccessTypeList.Rows.Count > 0)
            {
                grdAccessTypeList.TopPagerRow.Visible = true;
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
            if (Session["asearch"] != null && Session["atext"] != null)
            {
                ddsearch.SelectedValue = Session["asearch"].ToString();
                txtpages.Text = Session["atext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdAccessTypeList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdAccessTypeList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdAccessTypeList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdAccessTypeList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdAccessTypeList.PageIndex == 0)
            {
                ((ImageButton)grdAccessTypeList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdAccessTypeList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdAccessTypeList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdAccessTypeList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdAccessTypeList.PageIndex + 1 == grdAccessTypeList.PageCount)
            {
                ((ImageButton)grdAccessTypeList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdAccessTypeList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdAccessTypeList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdAccessTypeList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["asearch"] = null;
            Session["atext"] = null;
            string userid = Session["UserID"].ToString();
            accesstypelist = new List<AccessType>();
            accesstypelist = BL_User_Mgnt.GetAccessTypeList(userid);
            grdAccessTypeList.DataSource = accesstypelist;
            grdAccessTypeList.DataBind();


        }
    }
}