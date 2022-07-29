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
    public partial class DivisionList : System.Web.UI.Page
    {
        List<Division> divisions = new List<Division>();
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
                grdDivisionList.PageSize= Convert.ToInt32(Session["pagesize"].ToString());

                string userid = Session["UserID"].ToString();
                divisions = new List<Division>();
                divisions = BL_User_Mgnt.GetDivisions(userid);
                grdDivisionList.DataSource = divisions;
                grdDivisionList.DataBind();
            }
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Session["rType"] =0;
            Response.Redirect("DivisionForm");
        }

        protected void StateMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StateList");
        }

        protected void DivisionMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DivisionList");
        }
        protected void ThanaMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ThanaMasterList");
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
            string div_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text;
            string state = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblStateName") as Label).Text;
            string div_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDivisionCode") as Label).Text;
            string div_Name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDivisionName") as Label).Text;
            string state_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblState_code") as Label).Text;
            Session["UserID"] = Session["UserID"].ToString();
            Session["div_id"] = div_id;
            Session["state"] = state;
            Session["state_code"] = state_code;
            Session["div_code"] = div_code;
            Session["div_Name"] = div_Name;
            Session["rType"] = 1;
            Response.Redirect("DivisionForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string div_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text;
            string state = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblStateName") as Label).Text;
            string div_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDivisionCode") as Label).Text;
            string div_Name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDivisionName") as Label).Text;
            string state_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblState_code") as Label).Text;
            Session["UserID"] = Session["UserID"].ToString();
            Session["div_id"] = div_id;
            Session["state"] = state;
            Session["state_code"] = state_code;
            Session["div_code"] = div_code;
            Session["div_Name"] = div_Name;
         
            Session["rType"] = 2;
            Response.Redirect("DivisionForm");
        }

        protected void grdDivisionList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewRow row = grdDivisionList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (e.NewPageIndex < 0)
            {
                grdDivisionList.PageIndex = 0;
            }
            else
            {
                grdDivisionList.PageIndex = e.NewPageIndex;
            }

            string userid = Session["UserID"].ToString();
            Session["UserID"] = userid;
            if (Session["ddsearch"] != null && Session["ddtext"] != null)
            {
                ddsearch.SelectedValue = Session["ddsearch"].ToString();
                txtpage.Text = Session["ddtext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        string qery = txtSearch.Text.ToString();
                        qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


                        divisions = new List<Division>();
                        divisions = BL_User_Mgnt.SearchDivision("division_master", ddsearch.SelectedValue, txtpage.Text);
                        grdDivisionList.DataSource = divisions;
                        grdDivisionList.DataBind();
                        ddsearch.SelectedValue = Session["dsearch"].ToString();
                        txtpage.Text = Session["dtext"].ToString();
                    }
                }
            }
            else
            {


                //  string userid = Session["UserID"].ToString();
                divisions = new List<Division>();
                divisions = BL_User_Mgnt.GetDivisions(userid);
                grdDivisionList.DataSource = divisions;
                grdDivisionList.DataBind();
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdDivisionList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                string userid = Session["UserID"].ToString();
                    divisions = new List<Division>();
                    divisions = BL_User_Mgnt.GetDivisions(userid);
                    grdDivisionList.DataSource = divisions;
                    grdDivisionList.DataBind();
                }
            

        }
        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdDivisionList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                string userid = Session["UserID"].ToString();
                divisions = new List<Division>();
                divisions = BL_User_Mgnt.GetDivisions(userid);
                grdDivisionList.DataSource = divisions;
                grdDivisionList.DataBind();
            }
            return divisions.ToString();
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GridViewRow row = grdDivisionList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["ddsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
            {
                    Session["ddtext"] = txtpage.Text;
                    string qery = txtSearch.Text.ToString();
                qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


                divisions = new List<Division>();
                divisions = BL_User_Mgnt.SearchDivision("division_master",ddsearch.SelectedValue,txtpage.Text);
                grdDivisionList.DataSource = divisions;
                grdDivisionList.DataBind();
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
            GridViewRow row = grdDivisionList.TopPagerRow;
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
                grdDivisionList.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdDivisionList.PageIndex = a - 1;
            }
            

            string userid = Session["UserID"].ToString();
            divisions = new List<Division>();
            divisions = BL_User_Mgnt.GetDivisions(userid);
            grdDivisionList.DataSource = divisions;
            grdDivisionList.DataBind();

            
        }

        protected void grdDivisionList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdDivisionList.TopPagerRow;
            if (grdDivisionList.Rows.Count > 0)
            {
                grdDivisionList.TopPagerRow.Visible = true;
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
            lblPages.Text = grdDivisionList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdDivisionList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            if (Session["ddsearch"] != null && Session["ddtext"] != null)
            {
                ddsearch.SelectedValue = Session["ddsearch"].ToString();
                txtpages.Text = Session["ddtext"].ToString();
            }
                //}

                if (DDLPage != null)
            {
                for (int i = 0; i < grdDivisionList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdDivisionList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdDivisionList.PageIndex == 0)
            {
                ((ImageButton)grdDivisionList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdDivisionList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdDivisionList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdDivisionList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdDivisionList.PageIndex + 1 == grdDivisionList.PageCount)
            {
                ((ImageButton)grdDivisionList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdDivisionList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdDivisionList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdDivisionList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["ddsearch"] = null;
            Session["ddtext"] = null;
            string userid = Session["UserID"].ToString();
            divisions = new List<Division>();
            divisions = BL_User_Mgnt.GetDivisions(userid);
            grdDivisionList.DataSource = divisions;
            grdDivisionList.DataBind();
        }
    }
}