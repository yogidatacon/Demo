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
    public partial class PartyTypeMasterList : System.Web.UI.Page
    {
        List<Party_Type_Master> partytypes = new List<Party_Type_Master>();
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
                grdpartytypemasterList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                Session["UserID"] = Session["UserID"];
                partytypes = new List<Party_Type_Master>();
                partytypes = BL_Party_Type_Master.GetList();
                var partynames = from s in partytypes
                                 where s.party_type_code != "All"
                                 select s;
                grdpartytypemasterList.DataSource = partynames.ToList();
                grdpartytypemasterList.DataBind();
            }
        }

        protected void grdpartytypemasterList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0)
            {
                grdpartytypemasterList.PageIndex = 0;
            }
            else
            {
                grdpartytypemasterList.PageIndex = e.NewPageIndex;
            }

            GridViewRow row = grdpartytypemasterList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (Session["ptsearch"] != null && Session["pttext"] != null)
            {
                ddsearch.SelectedValue = Session["ptsearch"].ToString();
                txtpage.Text = Session["pttext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        string qery = txtSearch.Text.ToString();
                        qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


                        partytypes = new List<Party_Type_Master>();
                        partytypes = BL_Party_Type_Master.SearchPartyType("party_type_master", ddsearch.SelectedValue, txtpage.Text);
                        grdpartytypemasterList.DataSource = partytypes;
                        grdpartytypemasterList.DataBind();
                    }
                }
            }
            else
            {



               
                partytypes = new List<Party_Type_Master>();
                partytypes = BL_Party_Type_Master.GetList();
                grdpartytypemasterList.DataSource = partytypes;
                grdpartytypemasterList.DataBind();
            }
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
        protected void vattypemaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("VatTypeMasterList");
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

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "0";
            Response.Redirect("PartyTypeMasterForm");
        }
      

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string Party_Type_Name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblparty_type_Name") as Label).Text;
            string Party_Type_Code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblparty_type_code") as Label).Text;
            string Status = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblStatus") as Label).Text;
            Session["Party_Type_Code"] = Party_Type_Code;
            Session["Party_Type_Name"] = Party_Type_Name;
            Session["Status"] = Status;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "1";
            Response.Redirect("PartyTypeMasterForm");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string Party_Type_Name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblparty_type_Name") as Label).Text;
            string Party_Type_Code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblparty_type_code") as Label).Text;
            string Status = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblStatus") as Label).Text;
            Session["Party_Type_Code"] = Party_Type_Code;
            Session["Party_Type_Name"] = Party_Type_Name;
            Session["Status"] = Status;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "2";
            Response.Redirect("PartyTypeMasterForm");
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

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (txtSearch.Text == "")
                {
                    partytypes = new List<Party_Type_Master>();
                    partytypes = BL_Party_Type_Master.GetList();
                    grdpartytypemasterList.DataSource = partytypes;
                    grdpartytypemasterList.DataBind();
                }
            }

        }
        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdpartytypemasterList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                string userid = Session["UserID"].ToString();
                partytypes = new List<Party_Type_Master>();
                partytypes = BL_Party_Type_Master.GetList();
                grdpartytypemasterList.DataSource = partytypes;
                grdpartytypemasterList.DataBind();
            }
            return partytypes.ToString();
        }



        protected void btnsearch_Click(object sender, EventArgs e)
        {

            
                GridViewRow row = grdpartytypemasterList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (ddsearch.SelectedValue != "Select")
            {

                Session["ptsearch"] = ddsearch.SelectedValue;
                if (txtpage != null)
                {
                    Session["pttext"] = txtpage.Text;
                    string qery = txtSearch.Text.ToString();
                qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


                partytypes = new List<Party_Type_Master>();
                partytypes = BL_Party_Type_Master.SearchPartyType("party_type_master", ddsearch.SelectedValue,txtpage.Text);
                grdpartytypemasterList.DataSource = partytypes;
                grdpartytypemasterList.DataBind();
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
            GridViewRow row = grdpartytypemasterList.TopPagerRow;
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
                grdpartytypemasterList.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdpartytypemasterList.PageIndex = a - 1;
            }
            

            Session["UserID"] = Session["UserID"];
            partytypes = new List<Party_Type_Master>();
            partytypes = BL_Party_Type_Master.GetList();
            var partynames = from s in partytypes
                             where s.party_type_code != "All"
                             select s;
            grdpartytypemasterList.DataSource = partynames.ToList();
            grdpartytypemasterList.DataBind();


        }

        protected void grdpartytypemasterList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdpartytypemasterList.TopPagerRow;
            if (grdpartytypemasterList.Rows.Count > 0)
            {
                grdpartytypemasterList.TopPagerRow.Visible = true;
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
            if (Session["ptsearch"] != null && Session["pttext"] != null)
            {
                ddsearch.SelectedValue = Session["ptsearch"].ToString();
                txtpages.Text = Session["pttext"].ToString();
            }

                //if (lblPages != null)
                //{
                lblPages.Text = grdpartytypemasterList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdpartytypemasterList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdpartytypemasterList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdpartytypemasterList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdpartytypemasterList.PageIndex == 0)
            {
                ((ImageButton)grdpartytypemasterList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdpartytypemasterList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdpartytypemasterList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdpartytypemasterList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdpartytypemasterList.PageIndex + 1 == grdpartytypemasterList.PageCount)
            {
                ((ImageButton)grdpartytypemasterList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdpartytypemasterList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdpartytypemasterList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdpartytypemasterList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["ptsearch"] = null;
            Session["pttext"] = null;
            Session["UserID"] = Session["UserID"];
            partytypes = new List<Party_Type_Master>();
            partytypes = BL_Party_Type_Master.GetList();
            var partynames = from s in partytypes
                             where s.party_type_code != "All"
                             select s;
            grdpartytypemasterList.DataSource = partynames.ToList();
            grdpartytypemasterList.DataBind();
        }
    }
}