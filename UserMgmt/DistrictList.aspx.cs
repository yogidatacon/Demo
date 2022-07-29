using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class DistrictList : System.Web.UI.Page
    {
        List<District> districts = new List<District>();
       
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
                grdDistrictList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                string userid = Session["UserID"].ToString();
                Session["UserID"] = userid;
                districts = new List<District>();
                districts = BL_User_Mgnt.GetDistricts(userid);
                grdDistrictList.DataSource = districts;
                grdDistrictList.DataBind();
               
            }
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Session["rType"] = 0;
            Response.Redirect("DistrictForm");
        }

        protected void StateMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("~/StateList");
        }

        protected void DivisionMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("~/DivisionList");
        }

        protected void DistrictMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("~/Districtlist");

        }

        protected void RoleLevelMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("~/RoleLevelList");

        }

        protected void AccessTypeMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("~/AccessTypeList");
        }

        protected void RoleMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("~/RoleMasterList1");
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string div_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text;
            string state = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblStateName") as Label).Text;
            string div_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDivisionCode") as Label).Text;
            string div_Name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDivisionName") as Label).Text;
            string state_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblStatecode") as Label).Text;
            string District_Name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDistrictName") as Label).Text;
            string District_Code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDistrictcode") as Label).Text;
            string lab_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lbllabid") as Label).Text;
            Session["UserID"] = Session["UserID"].ToString();
            Session["div_id"] = div_id;
            Session["state"] = state;
            Session["state_code"] = state_code;
            Session["div_code"] = div_code;
            Session["div_Name"] = div_Name;
            Session["District_Name"] = District_Name;
            Session["District_Code"] = District_Code;
            Session["lab_id"] = lab_id;
            Session["rType"] = 1;
            Response.Redirect("DistrictForm");
        }
        protected void ThanaMaster_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("ThanaMasterList");
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string div_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblid") as Label).Text;
            string state = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblStateName") as Label).Text;
            string div_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDivisionCode") as Label).Text;
            string div_Name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDivisionName") as Label).Text;
            string state_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblStatecode") as Label).Text;
            string District_Name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDistrictName") as Label).Text; 
            string District_Code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblDistrictcode") as Label).Text;
            string lab_id = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lbllabid") as Label).Text;
            Session["UserID"] = Session["UserID"].ToString();
            Session["div_id"] = div_id;
            Session["state"] = state;
            Session["state_code"] = state_code;
            Session["div_code"] = div_code;
            Session["div_Name"] = div_Name;
            Session["District_Name"] = District_Name;
            Session["District_Code"] = District_Code;
            Session["lab_id"] = lab_id;
            Session["rType"] = 2;
            Response.Redirect("DistrictForm");
        }

        protected void grdDistrictList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewRow row = grdDistrictList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (e.NewPageIndex < 0)
            {
                grdDistrictList.PageIndex = 0;
            }
            else
            {
                grdDistrictList.PageIndex = e.NewPageIndex;
            }
           
            string userid = Session["UserID"].ToString();
            Session["UserID"] = userid;
            if (Session["dsearch"] != null && Session["dtext"] != null)
            {
                ddsearch.SelectedValue = Session["dsearch"].ToString();
                txtpage.Text = Session["dtext"].ToString();
                if (ddsearch.SelectedValue != "Select")
                {

                    if (txtpage != null)
                    {
                        string qery = txtSearch.Text.ToString();
                        qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


                        districts = new List<District>();
                        districts = BL_User_Mgnt.SearchDistrictData("district_master", ddsearch.SelectedValue, txtpage.Text);
                        grdDistrictList.DataSource = districts;
                        grdDistrictList.DataBind();
                        ddsearch.SelectedValue = Session["dsearch"].ToString();
                        txtpage.Text = Session["dtext"].ToString();
                    }
                }
            }
            else
            {


                districts = new List<District>();
                districts = BL_User_Mgnt.GetDistricts(userid);
                grdDistrictList.DataSource = districts;
                grdDistrictList.DataBind();
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdDistrictList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                districts = new List<District>();
                    districts = BL_User_Mgnt.GetDistricts(Session["UserID"].ToString());
                    grdDistrictList.DataSource = districts;
                    grdDistrictList.DataBind();
                }
            }


        string accesstype = "";
        [WebMethod]
        public string chkDuplicateAccessTypeName(Object name)
        {
            int value = 0;
            GridViewRow row = grdDistrictList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if (txtpage == null)
            {
                string userid = Session["UserID"].ToString();
                districts = new List<District>();
                districts = BL_User_Mgnt.GetDistricts(Session["UserID"].ToString());
                grdDistrictList.DataSource = districts;
                grdDistrictList.DataBind();
            }
            return districts.ToString();
        }


        protected void btnsearch_Click(object sender, EventArgs e)
        {

        GridViewRow row = grdDistrictList.TopPagerRow;
        TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
        DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            if(ddsearch.SelectedValue !="Select")
            {

                Session["dsearch"] = ddsearch.SelectedValue;
        if (txtpage != null)
        {
                    Session["dtext"] = txtpage.Text;
            string qery = txtSearch.Text.ToString();
                qery = Regex.Replace(qery, @"(^\w)|(\s\w)", m => m.Value.ToUpper());


                districts = new List<District>();
                districts = BL_User_Mgnt.SearchDistrictData("district_master",ddsearch.SelectedValue,txtpage.Text);
                grdDistrictList.DataSource = districts;
                grdDistrictList.DataBind();
            }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Select Field Name\');", true);
                ddsearch.Focus();
            }


        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdDistrictList.TopPagerRow;
            if (grdDistrictList.Rows.Count > 0)
            {
                grdDistrictList.TopPagerRow.Visible = true;
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
            lblPages.Text = grdDistrictList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdDistrictList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            if (Session["dsearch"] != null && Session["dtext"] != null)
            {
                ddsearch.SelectedValue = Session["dsearch"].ToString();
                txtpages.Text = Session["dtext"].ToString();
            }
                //}

                if (DDLPage != null)
            {
                for (int i = 0; i < grdDistrictList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdDistrictList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdDistrictList.PageIndex == 0)
            {
                ((ImageButton)grdDistrictList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdDistrictList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdDistrictList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdDistrictList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdDistrictList.PageIndex + 1 == grdDistrictList.PageCount)
            {
                ((ImageButton)grdDistrictList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdDistrictList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdDistrictList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdDistrictList.TopPagerRow.FindControl("btnNext")).Visible = false;

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
        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdDistrictList.TopPagerRow;
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
                grdDistrictList.PageIndex = 0;
                txtpage.Text = "1";
            }
            else
            {
                grdDistrictList.PageIndex = a - 1;
            }
            

            districts = new List<District>();
            districts = BL_User_Mgnt.GetDistricts(Session["UserID"].ToString());
            grdDistrictList.DataSource = districts;
            grdDistrictList.DataBind();
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["dsearch"] = null;
            Session["dtext"] = null;
            districts = new List<District>();
            districts = BL_User_Mgnt.GetDistricts(Session["UserID"].ToString());
            grdDistrictList.DataSource = districts;
            grdDistrictList.DataBind();
        }
    }
}