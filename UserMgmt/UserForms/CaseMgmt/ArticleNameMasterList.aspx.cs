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
    public partial class ArticleNameMasterList : System.Web.UI.Page
    {
        List<cm_article_name> _article_name = new List<cm_article_name>();

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
                Session["Reset"] = "";
                Session["txtpage"] = "";
                Session["ddsearch"] = "Select";
                _article_name = BL_cm_article_name.GetList();
                ArticleNameMasterView.DataSource = _article_name.ToList();
                ArticleNameMasterView.DataBind();
            }
        }

        #region Master Forms Navigation
        protected void ArticleCategoryMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ArticleCategoryMasterList");
        }

        protected void Articlename_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ArticleNameMasterList");
        }

        protected void ArticleSubCategory_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ArticleSubCategoryMasterList");
        }

        protected void Bail_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("BailMasterList");
        }

        protected void Caste_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("CasteMasterList");
        }
        protected void Court_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("CourtMasterList");
        }

        protected void Designation_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DesignationMasterList");
        }
        protected void DesignationType_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DesignationTypeMasterList");
        }

        protected void DisposalofProperty_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DisposalofPropertyMasterList");
        }

        protected void Gender_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("GenderMasterList");
        }

        protected void Idproof_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("IdproofMasterList");
        }

        protected void Offence_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("OffenceMasterList");
        }
        protected void OffenceType_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("OffenceTypeMasterList");
        }

        protected void Religion_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ReligionMasterList");
        }

        protected void SeizurStage_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("SeizurStageList");
        }
        protected void SeizurStatus_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("SeizurStatusMasterList");
        }

        protected void propertytype_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("propertytypeMasterList");
        }

        protected void Vehicle_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VehicleMasterList");
        }
        #endregion Master Forms Navigation

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["rtype"] = 0;
            Response.Redirect("ArticleNameMasterForm");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblCode") as Label).Text;
            string Name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblName") as Label).Text;
            string articlecode = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblArticleCategory") as Label).Text;
            string articlecodesub = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblArticleSubCategory") as Label).Text;
            string ID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblId") as Label).Text;
            Session["UserID"] = Session["UserID"].ToString();
            Session["rtype"] = "1";
            Session["ArticleNName"] = Name;
            Session["ArticleNCode"] = code;
            Session["Article1code"] = articlecode;
            Session["ArticleNsubCode"] = articlecodesub;
            Session["ArticleNId"] = ID;
            Response.Redirect("ArticleNameMasterForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblCode") as Label).Text;
            string Name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblName") as Label).Text;
            string articlecode = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblArticleCategorycode") as Label).Text;
            string articlecodesub = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblArticleSubCategory") as Label).Text;
            string ID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblId") as Label).Text;
            Session["UserID"] = Session["UserID"].ToString();
            Session["rtype"] = "2";
            Session["ArticleNName"] = Name;
            Session["ArticleNCode"] = code;
            Session["Article1code"] = articlecode;
            Session["ArticleNsubCode"] = articlecodesub;
            Session["ArticleNId"] = ID;
            Response.Redirect("ArticleNameMasterForm");
        }

        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = ArticleNameMasterView.TopPagerRow;
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
                ArticleNameMasterView.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                ArticleNameMasterView.PageIndex = a - 1;
            }

            TextBox txtpage1 = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");

            if (Session["ddsearch"].ToString() != "Select" && Session["txtpage"].ToString() != "")
            {
                txtpage1.Text = Session["txtpage"].ToString();
                ddsearch.SelectedValue = Session["ddsearch"].ToString();
                _article_name = BL_cm_article_name.GetListILike(txtpage1.Text, ddsearch.SelectedValue);
                ArticleNameMasterView.DataSource = _article_name.ToList();
                ArticleNameMasterView.DataBind();

            }

            else
            {

                _article_name = BL_cm_article_name.GetList();
                ArticleNameMasterView.DataSource = _article_name.ToList();
                ArticleNameMasterView.DataBind();
            }

        }
        protected void ArticleNameMasterView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.NewPageIndex == -1)
                {
                    ArticleNameMasterView.PageIndex = 0;
                }
                else
                {
                    ArticleNameMasterView.PageIndex = e.NewPageIndex;
                }

                GridViewRow row = ArticleNameMasterView.TopPagerRow;
                TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");

                if (Session["ddsearch"].ToString() != "Select" && Session["txtpage"].ToString() != "")
                {
                    txtpage.Text = Session["txtpage"].ToString();
                    ddsearch.SelectedValue = Session["ddsearch"].ToString();
                    _article_name = BL_cm_article_name.GetListILike(txtpage.Text, ddsearch.SelectedValue);
                    ArticleNameMasterView.DataSource = _article_name.ToList();
                    ArticleNameMasterView.DataBind();

                }

                else
                {

                    _article_name = BL_cm_article_name.GetList();
                    ArticleNameMasterView.DataSource = _article_name.ToList();
                    ArticleNameMasterView.DataBind();
                }


            }





        }

        protected void ArticleNameMasterView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            GridViewRow row = ArticleNameMasterView.TopPagerRow;
            if (ArticleNameMasterView.Rows.Count > 0)
            {
                ArticleNameMasterView.TopPagerRow.Visible = true;
            }
            if (row == null)
            {
                return;
            }

            DropDownList DDLPage = (DropDownList)row.Cells[0].FindControl("DDLPage");
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            Label lblPages = (Label)row.Cells[0].FindControl("lblPages");
            Label lblCurrent = (Label)row.Cells[0].FindControl("lblCurrent");

            //if (lblPages != null)
            //{
            lblPages.Text = ArticleNameMasterView.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = ArticleNameMasterView.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < ArticleNameMasterView.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == ArticleNameMasterView.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (ArticleNameMasterView.PageIndex == 0)
            {
                ((ImageButton)ArticleNameMasterView.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)ArticleNameMasterView.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)ArticleNameMasterView.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)ArticleNameMasterView.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (ArticleNameMasterView.PageIndex + 1 == ArticleNameMasterView.PageCount)
            {
                ((ImageButton)ArticleNameMasterView.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)ArticleNameMasterView.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)ArticleNameMasterView.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)ArticleNameMasterView.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {




            GridViewRow row = ArticleNameMasterView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");

            _article_name = BL_cm_article_name.GetListILike(txtpage.Text, ddsearch.SelectedValue);
            Session["Reset"] = "N";
            Session["txtpage"] = txtpage.Text;
            Session["ddsearch"] = ddsearch.SelectedValue;
            ArticleNameMasterView.PageIndex = 0;

            ArticleNameMasterView.DataSource = _article_name;
            ArticleNameMasterView.DataBind();


        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                GridViewRow row = ArticleNameMasterView.TopPagerRow;
                TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");

                Session["Reset"] = "Y";
                ArticleNameMasterView.PageIndex = 0;
                Session["txtpage"] = "";
                Session["ddsearch"] = "Select";
                _article_name = BL_cm_article_name.GetList();
                ArticleNameMasterView.DataSource = _article_name.ToList();
                ArticleNameMasterView.DataBind();
            }
        }

        protected void ArticleNameMasterView_DataBound(object sender, EventArgs e)
        {

            GridViewRow row = ArticleNameMasterView.TopPagerRow;
            if (ArticleNameMasterView.Rows.Count > 0)
            {
                ArticleNameMasterView.TopPagerRow.Visible = true;
            }
            if (row == null)
            {
                return;
            }

            TextBox txtpage1 = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");

            if (Session["ddsearch"] != null && Session["txtpage"] != null)
            {
                txtpage1.Text = Session["txtpage"].ToString();
                ddsearch.SelectedValue = Session["ddsearch"].ToString();
            }

            if (Session["Reset"].ToString() == "Y")
            {
                txtpage1.Text = "";
                ddsearch.SelectedValue = "Select";
            }

            DropDownList DDLPage = (DropDownList)row.Cells[0].FindControl("DDLPage");
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            Label lblPages = (Label)row.Cells[0].FindControl("lblPages");
            Label lblCurrent = (Label)row.Cells[0].FindControl("lblCurrent");

            //if (lblPages != null)
            //{
            lblPages.Text = ArticleNameMasterView.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = ArticleNameMasterView.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < ArticleNameMasterView.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == ArticleNameMasterView.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (ArticleNameMasterView.PageIndex == 0)
            {
                ((ImageButton)ArticleNameMasterView.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)ArticleNameMasterView.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)ArticleNameMasterView.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)ArticleNameMasterView.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (ArticleNameMasterView.PageIndex + 1 == ArticleNameMasterView.PageCount)
            {
                ((ImageButton)ArticleNameMasterView.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)ArticleNameMasterView.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)ArticleNameMasterView.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)ArticleNameMasterView.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;

            }
        }
    }
}