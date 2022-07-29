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
    public partial class DMH_List : System.Web.UI.Page
    {
        List<cm_seiz_BasicIformation> _seizure = new List<cm_seiz_BasicIformation>();
        List<cm_court> _seiz = new List<cm_court>();
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
                Session["UserID"] = Session["UserID"];
                Session["seizureNo"] = "";


                //List<cm_court> _court = new List<cm_court>();
                //_court = BL_cm_court.GetList();
                //var clist = from c in _court
                //            select c;
                //ddlThana.DataSource = clist.ToList();
                //ddlThana.DataTextField = "court_master_name";
                //ddlThana.DataValueField = "court_master_code";
                //ddlThana.DataBind();
                //ddlThana.Items.Insert(0, "Select");
                //string username = Session["UserID"].ToString();
                string[] court = Session["UserID"].ToString().ToUpper().Split('_');
                string s1 = Session["district_code"].ToString();
                _seiz = BL_cm_seiz_Dmconfiscation.GetDMCaseList();
                var ad = (from s in _seiz
                          where s.record_status=="Y" && s.court_master_code==court[0] && s.district_code==Session["district_code"].ToString()
                          select s);
                grdUnSubmittedList.DataSource = ad.ToList();
                grdUnSubmittedList.DataBind();
            }
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = 0;
            Response.Redirect("DMH");
        }

        protected void ddlSelect_SelectedIndexChange(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Seach Unsubmitted records 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //if (IsPostBack)
            //{
            //    string strPreviousPage = "";
            //    if (Request.UrlReferrer != null)
            //    {
            //        strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
            //    }
            //    if (strPreviousPage == "")
            //    {
            //        Response.Redirect("~/LoginPage");
            //    }
            //    Session["UserID"] = Session["UserID"].ToString();

            //    _seiz = BL_cm_seiz_ExcisableArticlesSeized.GetFilterList(ddlThana.SelectedValue.ToString(),TextBox3.Text);
            //    grdUnSubmittedList.DataSource = _seiz.ToList();
            //    grdUnSubmittedList.DataBind();

            //}
        }



        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            string ID = (row.Cells[row.Cells.Count - 1].FindControl("lblregid") as Label).Text;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "1";
            Session["dmcase_registration_id"] = ID;
            Response.Redirect("DMH");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            string ID = (row.Cells[row.Cells.Count - 1].FindControl("lblregid") as Label).Text;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "2";
            Session["dmcase_registration_id"] = ID;
            Response.Redirect("DMH");
        }



        protected void grdUnSubmittedList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                string[] court = Session["UserID"].ToString().ToUpper().Split('_');
                grdUnSubmittedList.PageIndex = e.NewPageIndex;
                var ad = (from s in _seiz
                          where s.record_status == "Y" && s.court_master_code == court[0] && s.district_code == Session["district_code"].ToString()
                          select s);
                grdUnSubmittedList.DataSource = ad.ToList();
                grdUnSubmittedList.DataBind();
            }
            catch
            {

            }
        }

        protected void grdUnSubmittedList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdUnSubmittedList.TopPagerRow;
            if (grdUnSubmittedList.Rows.Count > 0)
            {
                grdUnSubmittedList.TopPagerRow.Visible = true;
            }
            if (row == null)
            {
                return;
            }

            DropDownList DDLPage = (DropDownList)row.Cells[0].FindControl("DDLPage");
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            Label lblPages = (Label)row.Cells[0].FindControl("lblPages");
            Label lblCurrent = (Label)row.Cells[0].FindControl("lblCurrent");

            lblPages.Text = grdUnSubmittedList.PageCount.ToString();

            int currentPage = grdUnSubmittedList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();


            if (DDLPage != null)
            {
                for (int i = 0; i < grdUnSubmittedList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdUnSubmittedList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }


            if (grdUnSubmittedList.PageIndex == 0)
            {
                ((ImageButton)grdUnSubmittedList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdUnSubmittedList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdUnSubmittedList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdUnSubmittedList.TopPagerRow.FindControl("btnPrev")).Visible = false;


            }


            if (grdUnSubmittedList.PageIndex + 1 == grdUnSubmittedList.PageCount)
            {
                ((ImageButton)grdUnSubmittedList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdUnSubmittedList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdUnSubmittedList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdUnSubmittedList.TopPagerRow.FindControl("btnNext")).Visible = false;


            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            GridViewRow row = grdUnSubmittedList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            //  article_category = BL_cm_article_category.GetListILike(txtpage.Text, ddsearch.SelectedValue);
            _seiz = BL_cm_seiz_Dmconfiscation.GetDMCaseList();
            Session["Reset"] = "N";
            Session["txtpage"] = txtpage.Text;
            Session["ddsearch"] = ddsearch.SelectedValue;
            if (ddsearch.SelectedValue == "caseno")
            {
                var ad = (from s in _seiz
                          where s.caseno==txtpage.Text && s.record_status == "Y"
                          select s);
                grdUnSubmittedList.DataSource = ad.ToList();
                grdUnSubmittedList.DataBind();


                grdUnSubmittedList.PageIndex = 0;
                grdUnSubmittedList.DataSource = ad.ToList(); ;
                grdUnSubmittedList.DataBind();
            }
            if (ddsearch.SelectedValue == "prfirno")
            {
                var ad = (from s in _seiz
                          where s.prfirno == txtpage.Text && s.record_status == "Y"
                          select s);
                grdUnSubmittedList.DataSource = ad.ToList();
                grdUnSubmittedList.DataBind();
            }
            if (ddsearch.SelectedValue == "confiscation_code")
            {
                string s1 = "";
                if (txtpage.Text.Trim() == "Vehical")
                    s1 = "VH";
                if (txtpage.Text.Trim() == "Property")
                    s1 = "PR";
                var ad = (from s in _seiz
                          where s.confiscation_code == s1 && s.record_status == "Y"
                          select s);
                grdUnSubmittedList.DataSource = ad.ToList();
                grdUnSubmittedList.DataBind();
            }
            if (ddsearch.SelectedValue == "district_code")
            {
                var ad = (from s in _seiz
                          where s.district_name == txtpage.Text.Trim() && s.record_status == "Y"
                          select s);
                grdUnSubmittedList.DataSource = ad.ToList();
                grdUnSubmittedList.DataBind();
            }
            if (ddsearch.SelectedValue == "thana_name")
            {
                var ad = (from s in _seiz
                          where s.thana_name == txtpage.Text.Trim() && s.record_status == "Y"
                          select s);
                grdUnSubmittedList.DataSource = ad.ToList();
                grdUnSubmittedList.DataBind();
            }
            if (ddsearch.SelectedValue == "date_of_hearing")
            {
                var ad = (from s in _seiz
                          where s.case_hearingdate == txtpage.Text.Replace("/", "-").Trim() && s.record_status == "Y"
                          select s);
                grdUnSubmittedList.DataSource = ad.ToList();
                grdUnSubmittedList.DataBind();
            }

        }
        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdUnSubmittedList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            if (txtpage.Text != "0" && grdUnSubmittedList.PageCount > Convert.ToInt32(txtpage.Text))
            {
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
                    grdUnSubmittedList.PageIndex = 0;
                    txtpage.Text = "1";
                }
                else
                {
                    grdUnSubmittedList.PageIndex = a - 1;
                }

                string userid = Session["UserID"].ToString();

                TextBox txtpage1 = (TextBox)row.Cells[0].FindControl("txtSearch2");
                DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
                if (txtpage1.Text == "")
                    txtpage1.Text = Session["txtpage"].ToString();
                if (ddsearch.SelectedValue == "Select" || ddsearch.SelectedValue == "")
                    ddsearch.SelectedValue = Session["ddsearch"].ToString();
                if (ddsearch.Text != "Select" && txtpage1.Text != "")
                {

                    _seiz = BL_cm_seiz_Dmconfiscation.GetDMCaseList();
                    if (ddsearch.SelectedValue == "caseno")
                    {
                        var ad = (from s in _seiz
                                  where s.caseno==txtpage.Text && s.record_status == "Y"
                                  select s);
                        grdUnSubmittedList.DataSource = ad.ToList();
                        grdUnSubmittedList.DataBind();
                        Session["Reset"] = "N";
                        Session["txtpage"] = txtpage.Text;
                        Session["ddsearch"] = ddsearch.SelectedValue;

                        grdUnSubmittedList.PageIndex = 0;
                        grdUnSubmittedList.DataSource = ad.ToList(); ;
                        grdUnSubmittedList.DataBind();
                    }
                    if (ddsearch.SelectedValue == "prfirno")
                    {
                        var ad = (from s in _seiz
                                  where s.prfirno == txtpage.Text && s.record_status == "Y"
                                  select s);
                        grdUnSubmittedList.DataSource = ad.ToList();
                        grdUnSubmittedList.DataBind();
                    }
                    if (ddsearch.SelectedValue == "confiscation_code")
                    {
                        string s1 = "";
                        if (txtpage.Text.Trim() == "Vehical")
                            s1 = "VH";
                        if (txtpage.Text.Trim() == "Property")
                            s1 = "PR";
                        var ad = (from s in _seiz
                                  where s.confiscation_code == s1 && s.record_status == "Y"
                                  select s);
                        grdUnSubmittedList.DataSource = ad.ToList();
                        grdUnSubmittedList.DataBind();
                    }
                    if (ddsearch.SelectedValue == "district_code")
                    {
                        var ad = (from s in _seiz
                                  where s.district_name == txtpage.Text.Trim() && s.record_status == "Y"
                                  select s);
                        grdUnSubmittedList.DataSource = ad.ToList();
                        grdUnSubmittedList.DataBind();
                    }
                    if (ddsearch.SelectedValue == "thana_name")
                    {
                        var ad = (from s in _seiz
                                  where s.thana_name == txtpage.Text.Trim() && s.record_status == "Y"
                                  select s);
                        grdUnSubmittedList.DataSource = ad.ToList();
                        grdUnSubmittedList.DataBind();
                    }
                    if (ddsearch.SelectedValue == "date_of_hearing")
                    {
                        var ad = (from s in _seiz
                                  where s.case_hearingdate == txtpage.Text.Replace("/", "-").Trim() && s.record_status == "Y"
                                  select s);
                        grdUnSubmittedList.DataSource = ad.ToList();
                        grdUnSubmittedList.DataBind();
                    }

                }

                else
                {

                    _seiz = BL_cm_seiz_Dmconfiscation.GetDMCaseList();
                    var ad = (from s in _seiz
                              where s.record_status == "Y"
                              select s);
                    grdUnSubmittedList.DataSource = _seiz.ToList();
                    grdUnSubmittedList.DataBind();
                }
            }



        }
        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                GridViewRow row = grdUnSubmittedList.TopPagerRow;
                TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");

                Session["txtpage"] = "";
                Session["ddsearch"] = "Select";
                Session["Reset"] = "Y";
                _seiz = BL_cm_seiz_Dmconfiscation.GetDMCaseList();
                var ad = (from s in _seiz
                          where s.record_status == "Y"
                          select s);
                grdUnSubmittedList.DataSource = ad.ToList();
                grdUnSubmittedList.DataBind();
            }
        }
    }
}