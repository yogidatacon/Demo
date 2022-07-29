using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class LicenseApplicationList : System.Web.UI.Page
    {
        public UserDetails user = new UserDetails();
        List<DailyDispatchClosure> DES = new List<DailyDispatchClosure>();
        string _party_code;
        List<LicenseApplication> list = new List<LicenseApplication>();
        List<WorkFlow> workflow = new List<WorkFlow>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strPreviousPage = "";
                if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];

                    Session["UserID"] = Session["UserID"];
                    user = new UserDetails();
                    user = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                    // party_code.Value = user.party_code.ToString();
                    Session["Rolename"] = user.role_name;
                    Session["Partycode"] = user.party_code;
                    Session["year"] = user.financial_year;
                    grdLicenseList.PageSize = Convert.ToInt32(Session["pagesize"].ToString());
                    list = new List<LicenseApplication>();
                    list = BL_LicenseApplication.Getlicense();
                    workflow = new List<WorkFlow>();
                    workflow = BL_WorkFlow.ApprovelLevels("LIC", "199");
                    var aplevel = (from s in workflow
                                   where s.user_registration_id == user.id.ToString()
                                   select s);
                    if (aplevel.ToList().Count > 0)
                    {
                        currentlevel.Value = aplevel.ToList()[0].approver_level;
                    }
                    if (Session["Rolename"].ToString().Trim() == "Assistant Commissioner" || currentlevel.Value == "1")
                    {
                        var list1 = from s in list
                                    where (s.record_status == "Y" || s.record_status == "R") && s.district_code==user.district_code 
                                    select s;
                        grdLicenseList.DataSource = list1.ToList();
                        grdLicenseList.DataBind();
                        foreach (GridViewRow dr1 in grdLicenseList.Rows)
                        {
                            LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                            btn.Visible = false;
                        }
                        AddRecord.Visible = false;
                    }
                    else if (Session["Rolename"].ToString().Trim() == "Deputy Commissioner" || currentlevel.Value == "2")
                    {
                        var list1 = from s in list
                                    where s.record_status == "A" || s.record_status == "C" || s.record_status == "B"
                                    select s;
                        grdLicenseList.DataSource = list1.ToList();
                        grdLicenseList.DataBind();
                        foreach (GridViewRow dr1 in grdLicenseList.Rows)
                        {
                            LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                            string a = (dr1.FindControl("lblstatus") as Label).Text;
                            if ((dr1.FindControl("lblstatus") as Label).Text == "Refer Back By Commissioner")
                            {
                                btn.Visible = true;
                            }
                            else
                            {
                                btn.Visible = false;
                            }

                        }

                        AddRecord.Visible = false;
                    }
                    else if (Session["Rolename"].ToString() == "Commissioner" || currentlevel.Value == "3")
                    {
                        var list1 = from s in list
                                    where s.record_status == "D" 
                                    select s;
                        grdLicenseList.DataSource = list1.ToList();
                        grdLicenseList.DataBind();
                        foreach (GridViewRow dr1 in grdLicenseList.Rows)
                        {
                            LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                            //if (list[0].record_status == "B")
                            //{
                            //    btn.Visible = true;
                            //}
                            //else
                            //{
                            //    btn.Visible = false;
                            //}

                        }
                        AddRecord.Visible = false;
                    }

                    else
                    {
                        var list1 = from s in list
                                    where s.party_code == Session["Partycode"].ToString() && s.financial_year==user.financial_year
                                    select s;
                        grdLicenseList.DataSource = list1.ToList();
                        grdLicenseList.DataBind();
                        foreach (GridViewRow dr1 in grdLicenseList.Rows)
                        {
                            // LinkButton btn = dr1.FindControl("btnApply") as LinkButton;

                            LinkButton btn = dr1.FindControl("btnrenewal") as LinkButton;
                            btn.Visible = false;
                           if( (dr1.FindControl("lblenddate") as Label).Text!="")
                            {
                            DateTime dt = Convert.ToDateTime((dr1.FindControl("lblenddate") as Label).Text);
                           string  lbl = (dr1.FindControl("lblstatus") as Label).Text;
                                Label lbl1 = (dr1.FindControl("lblstatus") as Label);
                            if (DateTime.Now.Date> dt && lbl== "Issued")
                            {
                                    btn.Visible = true;
                            }
                                if (lbl == "Expired")
                                {
                                    lbl1.ForeColor = System.Drawing.Color.Red;
                                }
                            }
                        }
                    }
                    //dispatch = new List<DailyDispatchClosure>();
                    //dispatch = BL_DailyDispatchClosure.GetDispatch(user.party_code);
                    //grdDailyDispatchClosure.DataSource = dispatch;
                    //grdDailyDispatchClosure.DataBind();
                }
            }
        }
        protected void grdProductList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if(e.NewPageIndex < 0)
            {
                grdLicenseList.PageIndex = 0;
            }
            else
            {
                grdLicenseList.PageIndex = e.NewPageIndex;
            }
            
            list = new List<LicenseApplication>();
            list = BL_LicenseApplication.Getlicense();
            if (Session["Rolename"].ToString() == "Assistant Commissioner")
            {
                var list1 = from s in list
                            where s.record_status == "Y" || s.record_status == "R"
                            select s;
                grdLicenseList.DataSource = list1.ToList();
                grdLicenseList.DataBind();
                foreach (GridViewRow dr1 in grdLicenseList.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    btn.Visible = false;
                }
                AddRecord.Visible = false;
            }
            else if (Session["Rolename"].ToString().Trim() == "Deputy Commissioner")
            {
                var list1 = from s in list
                            where s.record_status == "A" || s.record_status == "C"
                            select s;
                grdLicenseList.DataSource = list1.ToList();
                grdLicenseList.DataBind();
                foreach (GridViewRow dr1 in grdLicenseList.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    btn.Visible = false;
                }
                AddRecord.Visible = false;
            }
            else if (Session["Rolename"].ToString() == "Commissioner")
            {
                var list1 = from s in list
                            where s.record_status == "D" || s.record_status == "B"
                            select s;
                grdLicenseList.DataSource = list1.ToList();
                grdLicenseList.DataBind();
                foreach (GridViewRow dr1 in grdLicenseList.Rows)
                {


                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    //if (list[0].record_status == "B")
                    //{
                    //    btn.Visible = true;
                    //}
                    //else
                    //{
                    //    btn.Visible = false;
                    //}

                }
                AddRecord.Visible = false;
            }

            else
            {
                var list1 = from s in list
                            where s.user_id == Session["UserID"].ToString() && s.financial_year == user.financial_year
                            select s;
                grdLicenseList.DataSource = list1.ToList();
                grdLicenseList.DataBind();
                foreach (GridViewRow dr1 in grdLicenseList.Rows)
                {
                    // LinkButton btn = dr1.FindControl("btnApply") as LinkButton;

                    LinkButton btn = dr1.FindControl("btnrenewal") as LinkButton;
                    btn.Visible = false;
                    if ((dr1.FindControl("lblenddate") as Label).Text != "")
                    {
                        DateTime dt = Convert.ToDateTime((dr1.FindControl("lblenddate") as Label).Text);
                        string lbl = (dr1.FindControl("lblstatus") as Label).Text;
                        Label lbl1 = (dr1.FindControl("lblstatus") as Label);
                        if (DateTime.Now.Date > dt && lbl == "Issued")
                        {
                            btn.Visible = true;
                        }
                        if (lbl == "Expired")
                        {
                            lbl1.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                   
                }
            }
        }
        protected void DispatchTypeMaster1_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("DispatchTypeMasterList.aspx");
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
        protected void partyfinancialyears_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("PartyFinancialYearList");
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
            int value = 0;

            //value = BL_LicenseApplication.GetExistsData(Session["year"].ToString(),Session["Partycode"].ToString());
            //if(value==1)
            //{
            //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //    sb.Append("<script type = 'text/javascript'>");
            //    sb.Append("window.onload=function(){");
            //    sb.Append("alert('");
            //    sb.Append(" one License for per financial Year ");
            //    sb.Append("')};");
            //    sb.Append("</script>");
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
            //    Response.Redirect("LicenseApplicationList.aspx");
              
            //}
            //else
            //{
                Response.Redirect("LicenseApplicationForm.aspx");
           // }
            
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            //string uom_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblCode") as Label).Text;
            //string uom_name = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblName") as Label).Text;
            string license = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblapplicationid") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            string party_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblPartycode") as Label).Text;
            Session["Lparty_code"] = party_code;
            Session["Lfinancial_year"] = financial_year;

            Session["licenseId"] = license;
            //Session["license_code"] = uom_code;
            //Session["license_name"] = uom_name;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "1";
            Response.Redirect("LicenseApplicationForm.aspx");
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string license = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblapplicationid") as Label).Text;
            string financial_year = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblfinancialyear") as Label).Text;
            string party_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblPartycode") as Label).Text;
            Session["Lparty_code"] = party_code;
            Session["Lfinancial_year"] = financial_year;
            Session["licenseId"] = license;
            //Session["license_code"] = uom_code;
            //Session["license_name"] = uom_name;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "2";
            Response.Redirect("LicenseApplicationForm.aspx");
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
        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdLicenseList.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            if (Convert.ToInt32(txtpage.Text) <= 0)
            {
                a = 0;
            }
            else
            {
                a = Convert.ToInt32(txtpage.Text);
            }
            if(a!=0)
            {
                grdLicenseList.PageIndex = a - 1;
            }
            else
            {
                grdLicenseList.PageIndex = a;
            }
           

            string userid = Session["UserID"].ToString();
            list = new List<LicenseApplication>();
            list = BL_LicenseApplication.Getlicense();
            if (Session["Rolename"].ToString() == "Assistant Commissioner")
            {
                var list1 = from s in list
                            where s.record_status == "Y" || s.record_status == "R"
                            select s;
                grdLicenseList.DataSource = list1.ToList();
                grdLicenseList.DataBind();
                foreach (GridViewRow dr1 in grdLicenseList.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    btn.Visible = false;
                }
                AddRecord.Visible = false;
            }
            else if (Session["Rolename"].ToString().Trim() == "Deputy Commissioner")
            {
                var list1 = from s in list
                            where s.record_status == "A" || s.record_status == "C"
                            select s;
                grdLicenseList.DataSource = list1.ToList();
                grdLicenseList.DataBind();
                foreach (GridViewRow dr1 in grdLicenseList.Rows)
                {
                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    btn.Visible = false;
                }
                AddRecord.Visible = false;
            }
            else if (Session["Rolename"].ToString() == "Commissioner")
            {
                var list1 = from s in list
                            where s.record_status == "D" || s.record_status == "B"
                            select s;
                grdLicenseList.DataSource = list1.ToList();
                grdLicenseList.DataBind();
                foreach (GridViewRow dr1 in grdLicenseList.Rows)
                {


                    LinkButton btn = dr1.FindControl("btnEdit") as LinkButton;
                    //if (list[0].record_status == "B")
                    //{
                    //    btn.Visible = true;
                    //}
                    //else
                    //{
                    //    btn.Visible = false;
                    //}

                }
                AddRecord.Visible = false;
            }

            else
            {
                var list1 = from s in list
                            where s.user_id == Session["UserID"].ToString() && s.financial_year == user.financial_year
                            select s;
                grdLicenseList.DataSource = list1.ToList();
                grdLicenseList.DataBind();

                foreach (GridViewRow dr1 in grdLicenseList.Rows)
                {
                    // LinkButton btn = dr1.FindControl("btnApply") as LinkButton;

                    LinkButton btn = dr1.FindControl("btnrenewal") as LinkButton;
                    btn.Visible = false;
                    if ((dr1.FindControl("lblenddate") as Label).Text != "")
                    {
                        DateTime dt = Convert.ToDateTime((dr1.FindControl("lblenddate") as Label).Text);
                        string lbl = (dr1.FindControl("lblstatus") as Label).Text;
                        Label lbl1 = (dr1.FindControl("lblstatus") as Label);
                        if (DateTime.Now.Date > dt && lbl == "Issued")
                        {
                            btn.Visible = true;
                        }
                        if (lbl == "Expired")
                        {
                            lbl1.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }

        }
        protected void grdLicenseList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdLicenseList.TopPagerRow;
            if (grdLicenseList.PageCount != 0)
            {
                grdLicenseList.TopPagerRow.Visible = true;
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
            lblPages.Text = grdLicenseList.PageCount.ToString();
            //}

            //if (lblCurrent != null)
            //{
            int currentPage = grdLicenseList.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();
            //}

            if (DDLPage != null)
            {
                for (int i = 0; i < grdLicenseList.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdLicenseList.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }

            //-- For First and Previous ImageButton
            if (grdLicenseList.PageIndex == 0)
            {
                ((ImageButton)grdLicenseList.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdLicenseList.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdLicenseList.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdLicenseList.TopPagerRow.FindControl("btnPrev")).Visible = false;

                //--- OR ---\\
                //ImageButton btnFirst = (ImageButton)row.Cells[0].FindControl("btnFirst");
                //ImageButton btnPrev = (ImageButton)row.Cells[0].FindControl("btnPrev");
                //btnFirst.Visible = false;
                //btnPrev.Visible = false;

            }

            //-- For Last and Next ImageButton
            if (grdLicenseList.PageIndex + 1 == grdLicenseList.PageCount)
            {
                ((ImageButton)grdLicenseList.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdLicenseList.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdLicenseList.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdLicenseList.TopPagerRow.FindControl("btnNext")).Visible = false;

                //--- OR ---\\
                //ImageButton btnLast = (ImageButton)row.Cells[0].FindControl("btnLast");
                //ImageButton btnNext = (ImageButton)row.Cells[0].FindControl("btnNext");
                //btnLast.Visible = false;
                //btnNext.Visible = false;
            }
        }

        protected void btnrenewal_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string license = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblapplicationid") as Label).Text;
            string party_code = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblPartycode") as Label).Text;
            Session["Lparty_code"] = party_code;
            Session["licenseId"] = license;
            //Session["license_code"] = uom_code;
            //Session["license_name"] = uom_name;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "3";
            Response.Redirect("LicenseApplicationForm.aspx");
        }
    }
}