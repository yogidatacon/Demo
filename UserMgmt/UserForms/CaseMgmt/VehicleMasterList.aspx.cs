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
    public partial class VehicleMasterList : System.Web.UI.Page
    {
        List<cm_Vehicle_type> vehicle_type = new List<cm_Vehicle_type>();

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
                vehicle_type = BL_cm_Vehicle_type.GetList();
                grdVehicleMasterList.DataSource = vehicle_type.ToList();
                grdVehicleMasterList.DataBind();
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
            Response.Redirect("VehicleMasterForm");
        }


        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblVehicleTypeCode") as Label).Text;
            string Name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblVehicleTypeName") as Label).Text;
            string ID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblId") as Label).Text;
            Session["UserID"] = Session["UserID"].ToString();
            Session["rtype"] = "1";
            Session["VehicleTypeName"] = Name;
            Session["VehicleTypeCode"] = code;
            Session["VehicleTypeId"] = ID;
            Response.Redirect("VehicleMasterForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblVehicleTypeCode") as Label).Text;
            string Name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblVehicleTypeName") as Label).Text;
            string ID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblId") as Label).Text;
            Session["UserID"] = Session["UserID"].ToString();
            Session["rtype"] = "2";
            Session["VehicleTypeName"] = Name;
            Session["VehicleTypeCode"] = code;
            Session["VehicleTypeId"] = ID;
            Response.Redirect("VehicleMasterForm");
        }
        protected void grdVehicleMasterList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.NewPageIndex == -1)
                {
                    grdVehicleMasterList.PageIndex = 0;
                }
                else
                {
                    grdVehicleMasterList.PageIndex = e.NewPageIndex;
                }

                GridViewRow row = grdVehicleMasterList.TopPagerRow;
                TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");

                if (Session["ddsearch"].ToString() != "Select" && Session["txtpage"].ToString() != "")
                {
                    txtpage.Text = Session["txtpage"].ToString();
                    ddsearch.SelectedValue = Session["ddsearch"].ToString();
                    vehicle_type = BL_cm_Vehicle_type.GetListILike(txtpage.Text, ddsearch.SelectedValue);
                    grdVehicleMasterList.DataSource = vehicle_type;
                    grdVehicleMasterList.DataBind();

                }

                else
                {

                    vehicle_type = new List<cm_Vehicle_type>();
                    vehicle_type = BL_cm_Vehicle_type.GetList();
                    grdVehicleMasterList.DataSource = vehicle_type;
                    grdVehicleMasterList.DataBind();
                }


            }






        }

        protected void Button2_Click(object sender, EventArgs e)
        {



            GridViewRow row = grdVehicleMasterList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");


            Session["Reset"] = "N";
            Session["txtpage"] = txtpage.Text;
            Session["ddsearch"] = ddsearch.SelectedValue;
            vehicle_type = new List<cm_Vehicle_type>();

            vehicle_type = BL_cm_Vehicle_type.GetListILike(txtpage.Text, ddsearch.SelectedValue);
            grdVehicleMasterList.PageIndex = 0;
            grdVehicleMasterList.DataSource = vehicle_type;
            grdVehicleMasterList.DataBind();


        }

        protected void grdVehicleMasterList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            GridViewRow row = grdVehicleMasterList.TopPagerRow;
            if (grdVehicleMasterList.Rows.Count > 0)
            {
                grdVehicleMasterList.TopPagerRow.Visible = true;
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
            lblPages.Text = grdVehicleMasterList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdVehicleMasterList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdVehicleMasterList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdVehicleMasterList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdVehicleMasterList.PageIndex == 0)
            {
                ((ImageButton)grdVehicleMasterList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdVehicleMasterList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdVehicleMasterList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdVehicleMasterList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdVehicleMasterList.PageIndex + 1 == grdVehicleMasterList.PageCount)
            {
                ((ImageButton)grdVehicleMasterList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdVehicleMasterList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdVehicleMasterList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdVehicleMasterList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                GridViewRow row = grdVehicleMasterList.TopPagerRow;
                TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");

                Session["Reset"] = "Y";
                Session["txtpage"] = "";
                Session["ddsearch"] = "Select";
                grdVehicleMasterList.PageIndex = 0;
                vehicle_type = BL_cm_Vehicle_type.GetList();
                grdVehicleMasterList.DataSource = vehicle_type.ToList();
                grdVehicleMasterList.DataBind();
            }
        }

        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdVehicleMasterList.TopPagerRow;
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
                grdVehicleMasterList.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdVehicleMasterList.PageIndex = a - 1;
            }

            TextBox txtpage1 = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");


            if (Session["ddsearch"].ToString() != "Select" && Session["txtpage"].ToString() != "")
            {
                txtpage1.Text = Session["txtpage"].ToString();
                ddsearch.SelectedValue = Session["ddsearch"].ToString();
                vehicle_type = BL_cm_Vehicle_type.GetListILike(txtpage1.Text, ddsearch.SelectedValue);
                grdVehicleMasterList.DataSource = vehicle_type.ToList();
                grdVehicleMasterList.DataBind();

            }

            else
            {

                vehicle_type = BL_cm_Vehicle_type.GetList();
                grdVehicleMasterList.DataSource = vehicle_type.ToList();
                grdVehicleMasterList.DataBind();
            }
            string userid = Session["UserID"].ToString();



        }

        protected void grdVehicleMasterList_DataBound(object sender, EventArgs e)
        {

            GridViewRow row = grdVehicleMasterList.TopPagerRow;
            if (grdVehicleMasterList.Rows.Count > 0)
            {
                grdVehicleMasterList.TopPagerRow.Visible = true;
            }
            if (row == null)
            {
                return;
            }

            DropDownList DDLPage = (DropDownList)row.Cells[0].FindControl("DDLPage");
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            Label lblPages = (Label)row.Cells[0].FindControl("lblPages");
            Label lblCurrent = (Label)row.Cells[0].FindControl("lblCurrent");

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
            //if (lblPages != null)
            //{
            lblPages.Text = grdVehicleMasterList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdVehicleMasterList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdVehicleMasterList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdVehicleMasterList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdVehicleMasterList.PageIndex == 0)
            {
                ((ImageButton)grdVehicleMasterList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdVehicleMasterList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdVehicleMasterList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdVehicleMasterList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdVehicleMasterList.PageIndex + 1 == grdVehicleMasterList.PageCount)
            {
                ((ImageButton)grdVehicleMasterList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdVehicleMasterList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdVehicleMasterList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdVehicleMasterList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }
    }
}