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
    public partial class RawMaterialList : System.Web.UI.Page
    {
        List<RawMaterial> rawmaterial = new List<RawMaterial>();
        protected void Page_Load(object sender, EventArgs e)
        {
            string userid = Session["UserID"].ToString();

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
                grdRawMaterialMasterView.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                rawmaterial = new List<RawMaterial>();
                rawmaterial = BL_RawMaterial.GetRawMaterialList(userid);
                grdRawMaterialMasterView.DataSource = rawmaterial;
                grdRawMaterialMasterView.DataBind();
            }


        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "0";
            Response.Redirect("RawMaterialForm.aspx");
        }
        protected void partyfinancialyears_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PartyFinancialYearList");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            string rawmaterial_type_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblRawMaterialTypeName") as Label).Text;
            string rawmaterial_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblRawMaterialName") as Label).Text;
            string uom_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblUOM") as Label).Text;
            string rawmaterial_type_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblRawMaterialTypeCode") as Label).Text;
            string rawmaterial_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblRawMaterialCode") as Label).Text;
            string uom_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblUOMCode") as Label).Text;
            string product_type_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblProducttypecode") as Label).Text;
            // string rmrid = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text;
            Session["UserID"] = Session["UserID"].ToString();
            //  Session["rawmaterial_type_code"] = rawmaterial_type_code;
            Session["rawmaterial_type_name"] = rawmaterial_type_name;
           // Session["rmr_id"] = rmrid;
            Session["rawmaterial_name"] = rawmaterial_name;
            Session["uom_name"] = uom_name;
            Session["rawmaterial_type_code"] = rawmaterial_type_code;
            Session["rawmaterial_code"] = rawmaterial_code;
            Session["uom_code"] = uom_code;
            Session["product_type_code"] = product_type_code;
            Session["rtype"] = "1";
            Response.Redirect("RawMaterialForm.aspx");
        }


        protected void btnEdite_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string rawmaterial_type_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblRawMaterialTypeName") as Label).Text;
            string rawmaterial_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblRawMaterialName") as Label).Text;
            string uom_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblUOM") as Label).Text;
            string rawmaterial_type_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblRawMaterialTypeCode") as Label).Text;
            string rawmaterial_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblRawMaterialCode") as Label).Text;
            string uom_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblUOMCode") as Label).Text;
            string product_type_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblProducttypecode") as Label).Text;
            //    string rmrid = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text;
            //  Session["rmr_id"] = rmrid;
            Session["product_type_code"] = product_type_code;
            Session["UserID"] = Session["UserID"].ToString();
            Session["rawmaterial_type_name"] = rawmaterial_type_name;
            Session["rawmaterial_name"] = rawmaterial_name;
            Session["uom_name"] = uom_name;
            Session["rawmaterial_type_code"] = rawmaterial_type_code;
            Session["rawmaterial_code"] = rawmaterial_code;
            Session["uom_code"] = uom_code;
            Session["rType"] = 2;
            Response.Redirect("RawMaterialForm.aspx");
        }
        protected void RawMaterialTypeMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialTypeMasterList");
        }

        protected void RawMaterial_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("RawMaterialList");
        }
        protected void partymaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("PartyMasterList");
        }
        protected void vattypemaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VatTypeMasterList");
        }
        protected void productmaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ProductMasterList");
        }

        protected void vatmaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VATMasterList");

        }

        protected void partytypemaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("PartyTypeMasterList");
        }

        protected void producttypemaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ProductTypeList");
        }
        protected void uommaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("UOMMasterList");
        }
        protected void DispatchTypeMaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DispatchTypeMasterList.aspx");
        }
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtSearch.Text == "")
                {
                   
                    string userid = Session["UserID"].ToString();
                    //Session["UserID"] = userid;
                    rawmaterial = new List<RawMaterial>();
                    rawmaterial = BL_RawMaterial.GetRawMaterialList(userid);
                    grdRawMaterialMasterView.DataSource = rawmaterial;
                    grdRawMaterialMasterView.DataBind();

                }
            }

        }
        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdRawMaterialMasterView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                string userid = Session["UserID"].ToString();
                rawmaterial = new List<RawMaterial>();
                rawmaterial = BL_RawMaterial.GetRawMaterialList(userid);
                grdRawMaterialMasterView.DataSource = rawmaterial;
                grdRawMaterialMasterView.DataBind();

            }
            return rawmaterial.ToString();
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {

            GridViewRow row = grdRawMaterialMasterView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["rawsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["rawtext"] = txtpage.Text;
                    string qery = txtSearch.Text.ToString();
                qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());

                rawmaterial = new List<RawMaterial>();
                rawmaterial = BL_RawMaterial.SearchRawMaterial("rawmaterial_master", ddsearch.SelectedValue,txtpage.Text);
                grdRawMaterialMasterView.DataSource = rawmaterial;
                grdRawMaterialMasterView.DataBind();
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
            GridViewRow row = grdRawMaterialMasterView.TopPagerRow;
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
                grdRawMaterialMasterView.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdRawMaterialMasterView.PageIndex = a - 1;
            }
            

            string userid = Session["UserID"].ToString();
            rawmaterial = new List<RawMaterial>();
            rawmaterial = BL_RawMaterial.GetRawMaterialList(userid);
            grdRawMaterialMasterView.DataSource = rawmaterial;
            grdRawMaterialMasterView.DataBind();


        }

        protected void grdRawMaterialMasterView_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdRawMaterialMasterView.TopPagerRow;
            if (grdRawMaterialMasterView.Rows.Count > 0)
            {
                grdRawMaterialMasterView.TopPagerRow.Visible = true;
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
            if (Session["rawsearch"] != null && Session["rawtext"] != null)
            {
                ddsearch.SelectedValue = Session["rawsearch"].ToString();
                txtpages.Text = Session["rawtext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdRawMaterialMasterView.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdRawMaterialMasterView.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdRawMaterialMasterView.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdRawMaterialMasterView.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdRawMaterialMasterView.PageIndex == 0)
            {
                ((ImageButton)grdRawMaterialMasterView.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdRawMaterialMasterView.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdRawMaterialMasterView.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdRawMaterialMasterView.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdRawMaterialMasterView.PageIndex + 1 == grdRawMaterialMasterView.PageCount)
            {
                ((ImageButton)grdRawMaterialMasterView.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdRawMaterialMasterView.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdRawMaterialMasterView.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdRawMaterialMasterView.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void grdRawMaterialMasterView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0)
            {
                grdRawMaterialMasterView.PageIndex = 0;
            }
            else
            {
                grdRawMaterialMasterView.PageIndex = e.NewPageIndex;
            }

            GridViewRow row = grdRawMaterialMasterView.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["rawsearch"] != null && Session["rawtext"] != null)
            {
                ddsearch.SelectedValue = Session["rawsearch"].ToString();
                txtpage.Text = Session["rawtext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        string qery = txtSearch.Text.ToString();
                        qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());

                        rawmaterial = new List<RawMaterial>();
                        rawmaterial = BL_RawMaterial.SearchRawMaterial("rawmaterial_master", ddsearch.SelectedValue, txtpage.Text);
                        grdRawMaterialMasterView.DataSource = rawmaterial;
                        grdRawMaterialMasterView.DataBind();
                    }
                }
            }
            else
            {

                string userid = Session["UserID"].ToString();
                rawmaterial = new List<RawMaterial>();
                rawmaterial = BL_RawMaterial.GetRawMaterialList(userid);
                grdRawMaterialMasterView.DataSource = rawmaterial;
                grdRawMaterialMasterView.DataBind();
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["rawsearch"] = null;
            Session["rawtext"] = null;
            string userid = Session["UserID"].ToString();
            rawmaterial = new List<RawMaterial>();
            rawmaterial = BL_RawMaterial.GetRawMaterialList(userid);
            grdRawMaterialMasterView.DataSource = rawmaterial;
            grdRawMaterialMasterView.DataBind();

        }
    }
}