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
    public partial class SystemSettingList : System.Web.UI.Page
    {
        List<SystemSetting> product = new List<SystemSetting>();
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
                grdProductList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                string userid = Session["UserID"].ToString();
                product = new List<SystemSetting>();
                product = BL_SystemSetting.GetList(userid);
                grdProductList.DataSource = product;
                grdProductList.DataBind();
            }


        }
        protected void DispatchTypeMaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DispatchTypeMasterList.aspx");
        }
        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Session["rtype"] = "0";
            Response.Redirect("SystemSettingForm.aspx");
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
            string code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblstring") as Label).Text;
            string name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblCode") as Label).Text;
            int id =Convert.ToInt32( (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text);
            int value = Convert.ToInt32( (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblName") as Label).Text);
            Session["UserID"] = Session["UserID"].ToString();
            Session["value"] = value;
            Session["name"] = name;
            Session["id"] =id;
            Session["string"] =code;
            Session["rType"] = 1;
            Response.Redirect("SystemSettingForm.aspx");
        }

        protected void btnEdite_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblstring") as Label).Text;
            string name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblCode") as Label).Text;
            int id = Convert.ToInt32((gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text);
            int value = Convert.ToInt32((gvr.Cells[gvr.Cells.Count - 1].FindControl("lblName") as Label).Text);
            Session["UserID"] = Session["UserID"].ToString();
            Session["value"] = value;
            Session["name"] = name;
            Session["id"] = id;
            Session["string"] = code;
            Session["rType"] = 2;
            Response.Redirect("SystemSettingForm.aspx");
        }
        protected void partymaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("PartyMasterList");
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

        protected void grdProductList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            if (e.NewPageIndex < 0)
            {
                grdProductList.PageIndex = 0;
            }
            else
            {
                grdProductList.PageIndex = e.NewPageIndex;
            }
            //user = new UserDetails();
            //   user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            GridViewRow row = grdProductList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["stssearch"] != null && Session["ststext"] != null)
            {
                ddsearch.SelectedValue = Session["stssearch"].ToString();
                txtpage.Text = Session["ststext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        product = new List<SystemSetting>();
                        product = BL_SystemSetting.Searchsys("product_type_master", ddsearch.SelectedValue, txtpage.Text);
                        grdProductList.DataSource = product;
                        grdProductList.DataBind();
                    }
                }
            }
            else
            {

                product = new List<SystemSetting>();
                product = BL_SystemSetting.GetList(Session["UserID"].ToString());
                grdProductList.DataSource = product;
                grdProductList.DataBind();
            }
        }
        protected void vattypemaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VatTypeMasterList");
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
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtSearch.Text == "")
                {
                    string userid = Session["UserID"].ToString();
                    product = new List<SystemSetting>();
                    product =BL_SystemSetting.GetList(userid);
                    grdProductList.DataSource = product;
                    grdProductList.DataBind();
                }
            }

        }

        //protected void btnsearch_Click(object sender, EventArgs e)
        //{

        //    if (txtSearch.Text != "")
        //    {
        //        string qery = txtSearch.Text.ToString();
        //        qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


        //        string userid = Session["UserID"].ToString();
        //        product = new List<SystemSetting>();
        //        product = BL_SystemSetting.Searchsys("product_type_master", "parameter_name", qery);
        //        grdProductList.DataSource = product;
        //        grdProductList.DataBind();
        //    }


        //}

        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdProductList.TopPagerRow;
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
                grdProductList.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdProductList.PageIndex = a - 1;
            }
            

            string userid = Session["UserID"].ToString();
            product = new List<SystemSetting>();
            product = BL_SystemSetting.GetList(userid);
            grdProductList.DataSource = product;
            grdProductList.DataBind();
        }



        protected void grdProductList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdProductList.TopPagerRow;
            if (grdProductList.Rows.Count > 0)
            {
                grdProductList.TopPagerRow.Visible = true;
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
            if (Session["stssearch"] != null && Session["ststext"] != null)
            {
                ddsearch.SelectedValue = Session["stssearch"].ToString();
                txtpages.Text = Session["ststext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdProductList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdProductList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdProductList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdProductList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdProductList.PageIndex == 0)
            {
                ((ImageButton)grdProductList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdProductList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdProductList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdProductList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdProductList.PageIndex + 1 == grdProductList.PageCount)
            {
                ((ImageButton)grdProductList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdProductList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdProductList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdProductList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }


        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdProductList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                Session["UserID"] = Session["UserID"];
                product = new List<SystemSetting>();
                product = BL_SystemSetting.GetList(Session["UserID"].ToString());
                grdProductList.DataSource = product;
                grdProductList.DataBind();
            }
            return product.ToString();
        }



        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GridViewRow row = grdProductList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["stssearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["ststext"] = txtpage.Text;
                    UserDetails user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    if (user != null)
                    {
                        Session["UserID"] = Session["UserID"];
                        string userid = Session["UserID"].ToString();
                        product = new List<SystemSetting>();
                        product = BL_SystemSetting.Searchsys("product_type_master",ddsearch.SelectedValue,txtpage.Text);
                        grdProductList.DataSource = product;
                        grdProductList.DataBind();
                    }
                }


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Select Field Name\');", true);
                ddsearch.Focus();
            }



        }
        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["stssearch"] = null;
            Session["ststext"] = null;
            Session["UserID"] = Session["UserID"];
            product = new List<SystemSetting>();
            product = BL_SystemSetting.GetList(Session["UserID"].ToString());
            grdProductList.DataSource = product;
            grdProductList.DataBind();
        }

    }
}