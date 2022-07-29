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
    public partial class ThanaMasterList : System.Web.UI.Page
    {
        List<ThanaMaster> thanalist = new List<ThanaMaster>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ThanaMasterView.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                string userid = Session["UserID"].ToString();
                //Session["UserID"] = userid;
                thanalist = new List<ThanaMaster>();
                thanalist = BL_Thana.GetThanaList(userid);
                ThanaMasterView.DataSource = thanalist;
                ThanaMasterView.DataBind();
            }
        }
        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rType"] = 0;
            Response.Redirect("ThanaMasterForm");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string thana_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblCode") as Label).Text;
            string thana_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblName") as Label).Text;
            string district_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDistrictCode") as Label).Text;
            string division_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDivisionCode") as Label).Text;
            string state_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblStatecode") as Label).Text;
            string state_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblState") as Label).Text;
            string district_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDistrict") as Label).Text;
            string division_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDivision") as Label).Text;
            Session["UserID"] = Session["UserID"].ToString();
            Session["thana_code"] = thana_code;
            Session["thana_name"] = thana_name;
            Session["district_code"] = district_code;
            Session["division_code"] = division_code;
            Session["state_name"] = state_name;
            Session["district_name"] = district_name;
            Session["division_name"] = division_name;
            Session["state_code"] = state_code;
            Session["rType"] = 1;
            Response.Redirect("ThanaMasterForm");
        }

        protected void btnEdite_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string thana_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblCode") as Label).Text;
            string thana_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblName") as Label).Text;
            string district_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDistrictCode") as Label).Text;
            string division_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDivisionCode") as Label).Text;
            string state_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblStatecode") as Label).Text;
            string state_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblState") as Label).Text;
            string district_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDistrict") as Label).Text;
            string division_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDivision") as Label).Text;
            Session["UserID"] = Session["UserID"].ToString();
            Session["thana_code"] = thana_code;
            Session["thana_name"] = thana_name;
            Session["district_code"] = district_code;
            Session["division_code"] = division_code;
            Session["state_name"] = state_name;
            Session["district_name"] = district_name;
            Session["division_name"] = division_name;
            Session["state_code"] = state_code;
            Session["rType"] = 2;
            Response.Redirect("ThanaMasterForm");
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
            Response.Redirect("Districtlist");

        }

        protected void RoleLevelMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("RoleLevelList");
        }

        protected void AccessTypeMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("AccessTypeList");
        }

        protected void RoleMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("RoleMasterList1");
        }
        protected void ThanaMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ThanaMasterList");
        }

        protected void ThanaMasterView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewRow row = ThanaMasterView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (e.NewPageIndex < 0)
            {
                ThanaMasterView.PageIndex = 0;
            }
            else
            {
                ThanaMasterView.PageIndex = e.NewPageIndex;
            }

            string userid = Session["UserID"].ToString();
            Session["UserID"] = userid;
            if (Session["tsearch"] != null && Session["ttext"] != null)
            {
                ddsearch.SelectedValue = Session["tsearch"].ToString();
                txtpage.Text = Session["ttext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        string qery = txtSearch.Text.ToString();
                        qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


                        thanalist = new List<ThanaMaster>();
                        thanalist = BL_Thana.SearchThana("thana_master", ddsearch.SelectedValue, txtpage.Text);
                        ThanaMasterView.DataSource = thanalist;
                        ThanaMasterView.DataBind();
                        ddsearch.SelectedValue = Session["tsearch"].ToString();
                        txtpage.Text = Session["ttext"].ToString();
                    }
                }
            }
            else
            {

               
                thanalist = new List<ThanaMaster>();
                thanalist = BL_Thana.GetThanaList(Session["UserID"].ToString());
                ThanaMasterView.DataSource = thanalist;
                ThanaMasterView.DataBind();
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ThanaMasterView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                thanalist = new List<ThanaMaster>();
                    thanalist = BL_Thana.GetThanaList(Session["UserID"].ToString());
                    ThanaMasterView.DataSource = thanalist;
                    ThanaMasterView.DataBind();
                }
            

        }

        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = ThanaMasterView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                thanalist = new List<ThanaMaster>();
                thanalist = BL_Thana.GetThanaList(Session["UserID"].ToString());
                ThanaMasterView.DataSource = thanalist;
                ThanaMasterView.DataBind();
            }
            return thanalist.ToString();
        }



        protected void btnsearch_Click(object sender, EventArgs e)
        {

            GridViewRow row = ThanaMasterView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["tsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["ttext"] = txtpage.Text;
                    string qery = txtSearch.Text.ToString();
                    qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


                    thanalist = new List<ThanaMaster>();
                    thanalist = BL_Thana.SearchThana("thana_master", ddsearch.SelectedValue, txtpage.Text);
                    ThanaMasterView.DataSource = thanalist;
                    ThanaMasterView.DataBind();
                }
            }
        
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Select Field Name\');", true);
                ddsearch.Focus();
            }

}

        protected void ThanaMasterView_DataBound(object sender, EventArgs e)
        {

            {
                GridViewRow row = ThanaMasterView.TopPagerRow;
                if (ThanaMasterView.Rows.Count > 0)
                {
                    ThanaMasterView.TopPagerRow.Visible = true;
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
                lblPages.Text = ThanaMasterView.PageCount.ToString();
                //}

                //if (lblCurrent != null)
                //{
                int currentPage = ThanaMasterView.PageIndex + 1;
                lblCurrent.Text = currentPage.ToString();
                txtpage.Text = currentPage.ToString();
                if (Session["tsearch"] != null && Session["ttext"] != null)
                {
                    ddsearch.SelectedValue = Session["tsearch"].ToString();
                    txtpages.Text = Session["ttext"].ToString();
                }
                    //}

                    if (DDLPage != null)
                {
                    for (int i = 0; i < ThanaMasterView.PageCount; i++)
                    {
                        int pageNumber = i + 1;
                        ListItem item = new ListItem(pageNumber.ToString());
                        if (i == ThanaMasterView.PageIndex)
                        {
                            item.Selected = true;
                        }
                        DDLPage.Items.Add(item);
                    }
                }

                //-- For First and Previous ImageButton
                if (ThanaMasterView.PageIndex == 0)
                {
                    ((ImageButton)ThanaMasterView.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                    ((ImageButton)ThanaMasterView.TopPagerRow.FindControl("btnFirst")).Visible = false;

                    ((ImageButton)ThanaMasterView.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                    ((ImageButton)ThanaMasterView.TopPagerRow.FindControl("btnPrev")).Visible = false;

                    //--- OR ---\\
                    //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                    //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                    //btnFirst.Visible = false;
                    //btnPrev.Visible = false;

                }

                //-- For Last and Next ImageButton
                if (ThanaMasterView.PageIndex + 1 == ThanaMasterView.PageCount)
                {
                    ((ImageButton)ThanaMasterView.TopPagerRow.FindControl("btnLast")).Enabled = true;
                    ((ImageButton)ThanaMasterView.TopPagerRow.FindControl("btnLast")).Visible = false;

                    ((ImageButton)ThanaMasterView.TopPagerRow.FindControl("btnNext")).Enabled = true;
                    ((ImageButton)ThanaMasterView.TopPagerRow.FindControl("btnNext")).Visible = false;

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
            GridViewRow row = ThanaMasterView.TopPagerRow;
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
                ThanaMasterView.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                ThanaMasterView.PageIndex = a - 1;
            }
            

            string userid = Session["UserID"].ToString();
            //Session["UserID"] = userid;
            thanalist = new List<ThanaMaster>();
            thanalist = BL_Thana.GetThanaList(userid);
            ThanaMasterView.DataSource = thanalist;
            ThanaMasterView.DataBind();


        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["tsearch"] = null;
            Session["ttext"] = null;
            string userid = Session["UserID"].ToString();
            //Session["UserID"] = userid;
            thanalist = new List<ThanaMaster>();
            thanalist = BL_Thana.GetThanaList(userid);
            ThanaMasterView.DataSource = thanalist;
            ThanaMasterView.DataBind();

        }
    }

}