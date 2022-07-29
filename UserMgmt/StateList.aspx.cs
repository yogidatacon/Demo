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
    public partial class StateList : System.Web.UI.Page
    {
        List<State> statelist = new List<State>();
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
                 StateListView.PageSize = Convert.ToInt32(Session["pagesize"].ToString());

                //if (Session["pageindex"].ToString() != " ")
                //{
                //    StateListView.PageIndex = Convert.ToInt32(Session["pageindex"].ToString());
                //}
                string userid = Session["UserID"].ToString();
                statelist = new List<State>();
                statelist = BL_User_Mgnt.GetListValues(userid);
                StateListView.DataSource = statelist;
                StateListView.DataBind();
               
            }
        }
        protected void StateMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/StateList");
        }

        protected void DivisionMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/DivisionList");
        }

        protected void DistrictMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/Districtlist");

        }

        protected void RoleLevelMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/RoleLevelList");
        }

        protected void AccessTypeMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/AccessTypeList");
        }

        protected void RoleMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/RoleMasterList1");
        }
        protected void ThanaMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ThanaMasterList");
        }
        protected void AddRecords_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Session["rType"] = 0;
            Response.Redirect("StateMasterForm");
        }

        protected void StateListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
           Session["pageindex"]= StateListView.PageIndex;
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string Stae_Code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblStateCode") as Label).Text;
            string State_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblStateName") as Label).Text;
            string country_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblcountry_name") as Label).Text;
            Session["UserID"] = Session["UserID"].ToString();
            Session["Sate_Code"] = Stae_Code;
            Session["State_name"] = State_name;
            Session["country"] = country_name;
            Session["rType"] = 1;
            Response.Redirect("StateMasterForm");
        }

        protected void StateListView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0)
            {
                StateListView.PageIndex = 0;
            }
            else
            {
                StateListView.PageIndex = e.NewPageIndex;
            }
            GridViewRow row = StateListView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["ssearch"] != null && Session["stext"] != null)
            {
                ddsearch.SelectedValue = Session["ssearch"].ToString();
                txtpage.Text = Session["stext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    Session["ssearch"] = ddsearch.SelectedValue;
                    if (txtpage != null)
                    {
                        Session["stext"] = txtpage.Text;
                        string qery = txtSearch.Text.ToString();

                        qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                        statelist = new List<State>();
                        statelist = BL_User_Mgnt.SearchState("state_master", ddsearch.SelectedValue, txtpage.Text);
                        StateListView.DataSource = statelist;
                        StateListView.DataBind();
                    }
                }
            }
            else
            {


                statelist = new List<State>();
                statelist = BL_User_Mgnt.GetListValues(Session["UserID"].ToString());
                StateListView.DataSource = statelist;
                StateListView.DataBind();
            }
        }
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //if (IsPostBack)
            //{
                GridViewRow row = StateListView.TopPagerRow;
                TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
                if (txtpage == null)
                {
                    string userid = Session["UserID"].ToString();
                    statelist = new List<State>();
                    statelist = BL_User_Mgnt.GetListValues(userid);
                    StateListView.DataSource = statelist;
                    StateListView.DataBind();
                }
           // }

        }
        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = StateListView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                string userid = Session["UserID"].ToString();
                statelist = new List<State>();
                statelist = BL_User_Mgnt.GetListValues(userid);
                StateListView.DataSource = statelist;
                StateListView.DataBind();
            }
            return statelist.ToString();
        }


        protected void btnsearch_Click(object sender, EventArgs e)
        {
            //if (IsPostBack)
            //{
                GridViewRow row = StateListView.TopPagerRow;
                TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["ssearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["stext"] = txtpage.Text;
                    string qery = txtSearch.Text.ToString();

                    qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                    statelist = new List<State>();
                    statelist = BL_User_Mgnt.SearchState("state_master", ddsearch.SelectedValue, txtpage.Text);
                    StateListView.DataSource = statelist;
                    StateListView.DataBind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Select Field Name\');", true);
                ddsearch.Focus();
            }

                // }
            }
        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = StateListView.TopPagerRow;

            DropDownList DDLSearch = (DropDownList)row.Cells[0].FindControl("ddlsearch2");
            if (DDLSearch!=null)
            {

            }
            if (StateListView.Rows.Count > 0)
            {
                StateListView.TopPagerRow.Visible = true;
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
            lblPages.Text = StateListView.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = StateListView.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text= currentPage.ToString();
            if (Session["ssearch"] != null && Session["stext"] != null)
            {
                ddsearch.SelectedValue = Session["ssearch"].ToString();
                txtpages.Text = Session["stext"].ToString();
            }
                //}

                if (DDLPage != null)
            {
                for (int i = 0; i < StateListView.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == StateListView.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (StateListView.PageIndex == 0)
            {
                ((ImageButton)StateListView.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)StateListView.TopPagerRow.FindControl("btnFirst")).Visible =false;

                ((ImageButton)StateListView.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)StateListView.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (StateListView.PageIndex + 1 == StateListView.PageCount)
            {
                ((ImageButton)StateListView.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)StateListView.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)StateListView.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)StateListView.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }

        }


        protected void DDLPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GridViewRow row = StateListView.TopPagerRow;
            //DropDownList DDLPage = (DropDownList)row.Cells[0].FindControl("DDLPage");

            //StateListView.PageIndex = DDLPage.SelectedIndex;
            //statelist = new List<State>();
            //statelist = BL_User_Mgnt.GetListValues(Session["UserID"].ToString());
            //StateListView.DataSource = statelist;
            //StateListView.DataBind();
        }
        protected void GridView2_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = StateListView.BottomPagerRow;
            StateListView.TopPagerRow.Visible = true;
            if (row == null)
            {
                return;
            }

            DropDownList DDLPage = (DropDownList)row.Cells[0].FindControl("DDLPage");
            Label lblPages = (Label)row.Cells[0].FindControl("lblPages");
            Label lblCurrent = (Label)row.Cells[0].FindControl("lblCurrent");

            //if (lblPages != null)
            //{
            lblPages.Text = StateListView.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = StateListView.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < StateListView.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == StateListView.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (StateListView.PageIndex == 0)
            {
                ((ImageButton)StateListView.BottomPagerRow.FindControl("btnFirst")).Enabled = false;
                ((ImageButton)StateListView.BottomPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)StateListView.BottomPagerRow.FindControl("btnPrev")).Enabled = false;
                ((ImageButton)StateListView.BottomPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (StateListView.PageIndex + 1 == StateListView.PageCount)
            {
                ((ImageButton)StateListView.BottomPagerRow.FindControl("btnLast")).Enabled = false;
                ((ImageButton)StateListView.BottomPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)StateListView.BottomPagerRow.FindControl("btnNext")).Enabled = false;
                ((ImageButton)StateListView.BottomPagerRow.FindControl("btnNext")).Visible = false;

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
            GridViewRow row = StateListView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            if(Convert.ToInt32(txtpage.Text)<= 0)
            {
                 a = 0;
            }
            else
            {
                a = Convert.ToInt32(txtpage.Text);
            }
            if (Convert.ToInt32(txtpage.Text) <= 0)
            {
                StateListView.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                StateListView.PageIndex = a - 1;
            }
            

            statelist = new List<State>();
            statelist = BL_User_Mgnt.GetListValues(Session["UserID"].ToString());
            StateListView.DataSource = statelist;
            StateListView.DataBind();
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["ssearch"] = null;
            Session["stext"] = null;
            statelist = new List<State>();
            statelist = BL_User_Mgnt.GetListValues(Session["UserID"].ToString());
            StateListView.DataSource = statelist;
            StateListView.DataBind();
        }
    }
}