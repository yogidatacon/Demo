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
    public partial class DispatchTypeMasterList : System.Web.UI.Page
    {
        List<DispatchType> dis = new List<DispatchType>();
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
                grdDispatchTypeList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                Session["UserID"] = Session["UserID"];

                List<DispatchType> dis = new List<DispatchType>();
                dis = BL_DispatchType.GetList();
                //partytypes = new List<Party_Type_Master>();
                //partytypes = BL_Party_Type_Master.GetList();
                //var partynames = from s in partytypes
                //                 where s.party_type_code != "All"
                //                 select s;
                grdDispatchTypeList.DataSource = dis;
                grdDispatchTypeList.DataBind();
            }
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "0";
            Response.Redirect("DispatchTypeMaster");
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
        protected void partyfinancialyears_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PartyFinancialYearList");
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
        protected void DispatchTypeMaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DispatchTypeMasterList");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            int rowindex = gvr.RowIndex;
            string dtype_id = (grdDispatchTypeList.Rows[rowindex].FindControl("lblDispatchTypeId") as Label).Text;
            string dtype_code = (grdDispatchTypeList.Rows[rowindex].FindControl("lblDispatchTypeCode") as Label).Text;
            string dtype_name = (grdDispatchTypeList.Rows[rowindex].FindControl("lblDispatchTypeName") as Label).Text;
            Session["UserID"] = Session["UserID"].ToString();
            Session["dtype_id"] = dtype_id;
            Session["dtype_code"] = dtype_code;
            Session["dtype_name"] = dtype_name;
            Session["rtype"] = 2;
            Response.Redirect("DispatchTypeMaster");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            int rowindex = gvr.RowIndex;
            string dtype_id = (grdDispatchTypeList.Rows[rowindex].FindControl("lblDispatchTypeId") as Label).Text;
            string dtype_code = (grdDispatchTypeList.Rows[rowindex].FindControl("lblDispatchTypeCode") as Label).Text;
            string dtype_name = (grdDispatchTypeList.Rows[rowindex].FindControl("lblDispatchTypeName") as Label).Text;
            Session["UserID"] = Session["UserID"].ToString();
            Session["dtype_id"] = dtype_id;
            Session["dtype_code"] = dtype_code;
            Session["dtype_name"] = dtype_name;
            Session["rtype"] = 1;
            Response.Redirect("DispatchTypeMaster");
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtSearch.Text == "")
                {

                    string userid = Session["UserID"].ToString();
                    Session["UserID"] = Session["UserID"];
                    List<DispatchType> dis = new List<DispatchType>();
                    dis = BL_DispatchType.GetList();
                    grdDispatchTypeList.DataSource = dis;
                    grdDispatchTypeList.DataBind();

                }
            }

        }
        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdDispatchTypeList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                string userid = Session["UserID"].ToString();
                List<DispatchType> dis = new List<DispatchType>();
                dis = BL_DispatchType.GetList();
                grdDispatchTypeList.DataSource = dis;
                grdDispatchTypeList.DataBind();

            }
            return dis.ToString();
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {

            GridViewRow row = grdDispatchTypeList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["distsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["disttext"] = txtpage.Text;

                    string userid = Session["UserID"].ToString();
                List<DispatchType> dis = new List<DispatchType>();
                dis = BL_DispatchType.SearchDispatchType("dispatch_type_master", ddsearch.SelectedValue,txtpage.Text);
                grdDispatchTypeList.DataSource = dis;
                grdDispatchTypeList.DataBind();

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
            GridViewRow row = grdDispatchTypeList.TopPagerRow;
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
                grdDispatchTypeList.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdDispatchTypeList.PageIndex = a - 1;
            }
            

            string userid = Session["UserID"].ToString();
            Session["UserID"] = Session["UserID"];

            List<DispatchType> dis = new List<DispatchType>();
            dis = BL_DispatchType.GetList();
            //partytypes = new List<Party_Type_Master>();
            //partytypes = BL_Party_Type_Master.GetList();
            //var partynames = from s in partytypes
            //                 where s.party_type_code != "All"
            //                 select s;
            grdDispatchTypeList.DataSource = dis;
            grdDispatchTypeList.DataBind();


        }

        protected void grdDispatchTypeList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdDispatchTypeList.TopPagerRow;
            if (grdDispatchTypeList.Rows.Count > 0)
            {
                grdDispatchTypeList.TopPagerRow.Visible = true;
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
            if (Session["distsearch"] != null && Session["disttext"] != null)
            {
                ddsearch.SelectedValue = Session["distsearch"].ToString();
                txtpages.Text = Session["disttext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdDispatchTypeList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdDispatchTypeList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdDispatchTypeList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdDispatchTypeList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdDispatchTypeList.PageIndex == 0)
            {
                ((ImageButton)grdDispatchTypeList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdDispatchTypeList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdDispatchTypeList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdDispatchTypeList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdDispatchTypeList.PageIndex + 1 == grdDispatchTypeList.PageCount)
            {
                ((ImageButton)grdDispatchTypeList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdDispatchTypeList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdDispatchTypeList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdDispatchTypeList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void grdDispatchTypeList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex == -1)
            {
                grdDispatchTypeList.PageIndex = 0;
            }
            else
            {
                grdDispatchTypeList.PageIndex = e.NewPageIndex;
            }
            GridViewRow row = grdDispatchTypeList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["distsearch"] != null && Session["disttext"] != null)
            {
                ddsearch.SelectedValue = Session["distsearch"].ToString();
                txtpage.Text = Session["disttext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        string qery = txtSearch.Text.ToString();
                        qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());

                        List<DispatchType> dis = new List<DispatchType>();
                        dis = BL_DispatchType.SearchDispatchType("dispatch_type_master", ddsearch.SelectedValue, txtpage.Text);
                        grdDispatchTypeList.DataSource = dis;
                        grdDispatchTypeList.DataBind();

                    }
                }
            }
            else
            {

               
                List<DispatchType> dis = new List<DispatchType>();
                dis = BL_DispatchType.GetList();
                //partytypes = new List<Party_Type_Master>();
                //partytypes = BL_Party_Type_Master.GetList();
                //var partynames = from s in partytypes
                //                 where s.party_type_code != "All"
                //                 select s;
                grdDispatchTypeList.DataSource = dis;
                grdDispatchTypeList.DataBind();
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["distsearch"] = null;
            Session["disttext"] = null;
            Session["UserID"] = Session["UserID"];

            List<DispatchType> dis = new List<DispatchType>();
            dis = BL_DispatchType.GetList();
           
            grdDispatchTypeList.DataSource = dis;
            grdDispatchTypeList.DataBind();
        }
    }
}