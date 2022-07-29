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
    public partial class PartyMasterList : System.Web.UI.Page
    {
        List<Party_Master> partymasters = new List<Party_Master>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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
                grdpartymasterList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                partymasters = new List<Party_Master>();
                partymasters = BL_Party_Master.GetList();
                var partynames = from s in partymasters
                                 where s.party_code !="ALL"
                                 select s;
                grdpartymasterList.DataSource = partynames.ToList();
                grdpartymasterList.DataBind();
            }
        }
        protected void grdpartymasterList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0)
            {
                grdpartymasterList.PageIndex = 0;
            }
            else
            {
                grdpartymasterList.PageIndex = e.NewPageIndex;
            }

            GridViewRow row = grdpartymasterList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["pmsearch"] != null && Session["pmtext"] != null)
            {
                ddsearch.SelectedValue = Session["pmsearch"].ToString();
                txtpage.Text = Session["pmtext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        string qery = txtSearch.Text.ToString();
                        qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


                        partymasters = BL_Party_Master.SearchParty("party_master", ddsearch.SelectedValue, txtpage.Text);
                        grdpartymasterList.DataSource = partymasters;
                        grdpartymasterList.DataBind();
                    }
                }
            }
            else
            {



              
                partymasters = BL_Party_Master.GetList();
                grdpartymasterList.DataSource = partymasters;
                grdpartymasterList.DataBind();
            }
        }
      
        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "0";
            Response.Redirect("PartyMasterForm");
        }
        protected void partymaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("PartyMasterList");
        }
        protected void partyfinancialyears_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PartyFinancialYearList");
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
            Response.Redirect("DispatchTypeMasterList.aspx");
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string Party_Code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblparty_code") as Label).Text;
            Session["Party_Code"] = Party_Code;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "1";
            Response.Redirect("PartyMasterForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string Party_Code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblparty_code") as Label).Text;
            Session["Party_Code"] = Party_Code;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "2";
            Response.Redirect("PartyMasterForm");
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtSearch.Text == "")
                {
                    partymasters = BL_Party_Master.GetList();
                    grdpartymasterList.DataSource = partymasters;
                    grdpartymasterList.DataBind();
                }
            }

        }
        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdpartymasterList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                string userid = Session["UserID"].ToString();
                partymasters = BL_Party_Master.GetList();
                grdpartymasterList.DataSource = partymasters;
                grdpartymasterList.DataBind();
            }
            return partymasters.ToString();
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {

            GridViewRow row = grdpartymasterList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["pmsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["pmtext"] = txtpage.Text;
                    string qery = txtSearch.Text.ToString();
                qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


                partymasters = BL_Party_Master.SearchParty("party_master", ddsearch.SelectedValue,txtpage.Text);
                grdpartymasterList.DataSource = partymasters;
                grdpartymasterList.DataBind();
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
            GridViewRow row = grdpartymasterList.TopPagerRow;
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
                grdpartymasterList.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdpartymasterList.PageIndex = a - 1;
            }
            

            string userid = Session["UserID"].ToString();
            partymasters = new List<Party_Master>();
            partymasters = BL_Party_Master.GetList();
            var partynames = from s in partymasters
                             where s.party_code != "ALL"
                             select s;
            grdpartymasterList.DataSource = partynames.ToList();
            grdpartymasterList.DataBind();


        }

        protected void grdpartymasterList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdpartymasterList.TopPagerRow;
            if (grdpartymasterList.Rows.Count > 0)
            {
                grdpartymasterList.TopPagerRow.Visible = true;
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
            if (Session["pmsearch"] != null && Session["pmtext"] != null)
            {
                ddsearch.SelectedValue = Session["pmsearch"].ToString();
                txtpages.Text = Session["pmtext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdpartymasterList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdpartymasterList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdpartymasterList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdpartymasterList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdpartymasterList.PageIndex == 0)
            {
                ((ImageButton)grdpartymasterList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdpartymasterList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdpartymasterList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdpartymasterList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdpartymasterList.PageIndex + 1 == grdpartymasterList.PageCount)
            {
                ((ImageButton)grdpartymasterList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdpartymasterList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdpartymasterList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdpartymasterList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["pmsearch"] = null;
            Session["pmtext"] = null;
            string userid = Session["UserID"].ToString();
            partymasters = new List<Party_Master>();
            partymasters = BL_Party_Master.GetList();
            var partynames = from s in partymasters
                             where s.party_code != "ALL"
                             select s;
            grdpartymasterList.DataSource = partynames.ToList();
            grdpartymasterList.DataBind();
        }
    }
}