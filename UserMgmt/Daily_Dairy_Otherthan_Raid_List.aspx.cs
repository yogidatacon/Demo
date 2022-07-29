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
    public partial class Dairy_Daily_Otherthan_Raid_List : System.Web.UI.Page
    {
        List<daily_diary_entry_otherthan_raid> dd = new List<daily_diary_entry_otherthan_raid>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Session["UserID"] = Session["UserID"];
                dd = new List<daily_diary_entry_otherthan_raid>();
                dd = BL_cm_seiz_Daily_Dairy_otherthan_Raid.GetList(Session["UserID"].ToString());
                UserDetails user1 = new UserDetails();
                user1 = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                if (user1.role_level_code == 3 || user1.role_name.Trim() == "Commissioner")
                {
                    var ad = (from s in dd
                              select s);
                    grdRaidDetails.DataSource = ad.ToList();
                    grdRaidDetails.DataBind();
                }
                else if (user1.role_level_code == 4)
                {
                    var ad = (from s in dd
                              where s.district_code == user1.district_code

                              select s);
                    grdRaidDetails.DataSource = ad.ToList();
                    grdRaidDetails.DataBind();
                }
                else if (user1.role_level_code == 9)
                {
                    var ad = (from s in dd
                              where s.division_code == user1.division_code

                              select s);
                    grdRaidDetails.DataSource = ad.ToList();
                    grdRaidDetails.DataBind();
                }
                else
                {
                    var ad = (from s in dd
                              where s.user_id == user1.user_id

                              select s);
                    grdRaidDetails.DataSource = ad.ToList();
                    grdRaidDetails.DataBind();
                }
             
            }
        }

        protected void btnAddRecord_Click(object sender, EventArgs e)
        {
            Session["rtype"] = "0";
            Session["UserID"] = Session["UserID"];
            Response.Redirect("Daily_Dairy_Otherthan_Raid.aspx");
        }
        protected void DDER_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("Daily_Dairy_Entry_Raid_List.aspx");
        }

        protected void DDEOR_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("Daily_Dairy_Otherthan_Raid_List.aspx");
        }

        protected void EVT_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("Event_List.aspx");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            string ID = (row.Cells[row.Cells.Count - 1].FindControl("lblraidid") as Label).Text; 
           string user = (row.Cells[row.Cells.Count - 1].FindControl("lbluserid") as Label).Text;
            Session["DDOUserID"] = user;
          Session["UserID"] = Session["UserID"];
            Session["rtype"] = "1";
            Session["daily_dairy_otherthan_raid_id"] = ID;
            Response.Redirect("Daily_Dairy_Otherthan_Raid.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            string ID = (row.Cells[row.Cells.Count - 1].FindControl("lblraidid") as Label).Text;
            string user = (row.Cells[row.Cells.Count - 1].FindControl("lbluserid") as Label).Text;
            Session["DDOUserID"] = user;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "2";
            Session["daily_dairy_otherthan_raid_id"] = ID;
            Response.Redirect("Daily_Dairy_Otherthan_Raid.aspx");
        }
        protected void grdRaidDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdRaidDetails.PageIndex = e.NewPageIndex;
                dd = new List<daily_diary_entry_otherthan_raid>();
                dd = BL_cm_seiz_Daily_Dairy_otherthan_Raid.GetList(Session["UserID"].ToString());
                UserDetails user1 = new UserDetails();
                user1 = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                if (user1.role_level_code == 3 || user1.role_name.Trim() == "Commissioner")
                {
                    var ad = (from s in dd
                              select s);
                    grdRaidDetails.DataSource = ad.ToList();
                    grdRaidDetails.DataBind();
                }
                else if (user1.role_level_code == 4)
                {
                    var ad = (from s in dd
                              where s.district_code == user1.district_code 

                              select s);
                    grdRaidDetails.DataSource = ad.ToList();
                    grdRaidDetails.DataBind();
                }
                else if (user1.role_level_code == 9)
                {
                    var ad = (from s in dd
                              where s.division_code == user1.division_code

                              select s);
                    grdRaidDetails.DataSource = ad.ToList();
                    grdRaidDetails.DataBind();
                }
                else
                {
                    var ad = (from s in dd
                              where s.user_id == user1.user_id

                              select s);
                    grdRaidDetails.DataSource = ad.ToList();
                    grdRaidDetails.DataBind();
                }
            }
            catch
            {

            }
        }

        protected void grdRaidDetails_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = grdRaidDetails.TopPagerRow;
            if (grdRaidDetails.Rows.Count > 0)
            {
                grdRaidDetails.TopPagerRow.Visible = true;
            }
            if (row == null)
            {
                return;
            }

            DropDownList DDLPage = (DropDownList)row.Cells[0].FindControl("DDLPage");
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            Label lblPages = (Label)row.Cells[0].FindControl("lblPages");
            Label lblCurrent = (Label)row.Cells[0].FindControl("lblCurrent");

            lblPages.Text = grdRaidDetails.PageCount.ToString();

            int currentPage = grdRaidDetails.PageIndex + 1;
            lblCurrent.Text = currentPage.ToString();
            txtpage.Text = currentPage.ToString();


            if (DDLPage != null)
            {
                for (int i = 0; i < grdRaidDetails.PageCount; i++)
                {
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == grdRaidDetails.PageIndex)
                    {
                        item.Selected = true;
                    }
                    DDLPage.Items.Add(item);
                }
            }


            if (grdRaidDetails.PageIndex == 0)
            {
                ((ImageButton)grdRaidDetails.TopPagerRow.FindControl("btnFirst")).Enabled = true;
                ((ImageButton)grdRaidDetails.TopPagerRow.FindControl("btnFirst")).Visible = false;

                ((ImageButton)grdRaidDetails.TopPagerRow.FindControl("btnPrev")).Enabled = true;
                ((ImageButton)grdRaidDetails.TopPagerRow.FindControl("btnPrev")).Visible = false;


            }


            if (grdRaidDetails.PageIndex + 1 == grdRaidDetails.PageCount)
            {
                ((ImageButton)grdRaidDetails.TopPagerRow.FindControl("btnLast")).Enabled = true;
                ((ImageButton)grdRaidDetails.TopPagerRow.FindControl("btnLast")).Visible = false;

                ((ImageButton)grdRaidDetails.TopPagerRow.FindControl("btnNext")).Enabled = true;
                ((ImageButton)grdRaidDetails.TopPagerRow.FindControl("btnNext")).Visible = false;


            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            GridViewRow row = grdRaidDetails.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
            DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
            //  article_category = BL_cm_article_category.GetListILike(txtpage.Text, ddsearch.SelectedValue);
            //  _seiz = BL_cm_seiz_Dmconfiscation.GetDMCaseList();
            Session["Reset"] = "N";
            Session["txtpage"] = txtpage.Text;
            Session["ddsearch"] = ddsearch.SelectedValue;
            dd = BL_cm_seiz_Daily_Dairy_otherthan_Raid.GetList(Session["UserID"].ToString());

            UserDetails user1 = new UserDetails();
            user1 = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            if (ddsearch.SelectedValue == "raid_date")
            {
                if (user1.role_level_code == 3 || user1.role_name.Trim() == "Commissioner")
                {
                    var ad = (from s in dd
                              where s.raid_entry_date == txtpage.Text
                              select s);
                    grdRaidDetails.DataSource = ad.ToList();
                    grdRaidDetails.DataBind();
                }
                else if (user1.role_level_code == 4)
                {
                    var ad = (from s in dd
                              where s.district_code == user1.district_code && s.raid_entry_date == txtpage.Text

                              select s);
                    grdRaidDetails.DataSource = ad.ToList();
                    grdRaidDetails.DataBind();
                }
                else if (user1.role_level_code == 9)
                {
                    var ad = (from s in dd
                              where s.division_code== user1.division_code && s.raid_entry_date == txtpage.Text

                              select s);
                    grdRaidDetails.DataSource = ad.ToList();
                    grdRaidDetails.DataBind();
                }
                else
                {
                    var ad = (from s in dd
                              where s.user_id == user1.user_id && s.raid_entry_date == txtpage.Text

                              select s);
                    grdRaidDetails.DataSource = ad.ToList();
                    grdRaidDetails.DataBind();
                }
                //var ad = (from s in dd
                //          where s.raid_entry_date == txtpage.Text
                //          select s);
                //grdRaidDetails.DataSource = ad.ToArray();
                //grdRaidDetails.DataBind();
            }
            //if (ddsearch.SelectedValue == "raid_location")
            //{
            //    var ad = (from s in dd
            //              where s.place_of_raid == txtpage.Text
            //              select s);
            //    grdRaidDetails.DataSource = ad.ToArray();
            //    grdRaidDetails.DataBind();
            //}


        }
        protected void txtpage_TextChanged(object sender, EventArgs e)
        {
            int a;
            GridViewRow row = grdRaidDetails.TopPagerRow;
            TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtpage");
            if (txtpage.Text != "0" && grdRaidDetails.PageCount >= Convert.ToInt32(txtpage.Text))
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
                    grdRaidDetails.PageIndex = 0;
                    txtpage.Text = "1";
                }
                else
                {
                    grdRaidDetails.PageIndex = a - 1;
                }
                if (a > 1)
                {
                    string userid = Session["UserID"].ToString();

                    TextBox txtpage1 = (TextBox)row.Cells[0].FindControl("txtSearch2");
                    DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
                    if (txtpage1.Text == "")
                        if (Session["txtpage"] != null)
                            txtpage1.Text = Session["txtpage"].ToString();
                    if (ddsearch.SelectedValue == "Select" || ddsearch.SelectedValue == "")
                        if (Session["ddsearch"] != null)
                            ddsearch.SelectedValue = Session["ddsearch"].ToString();
                    dd = BL_cm_seiz_Daily_Dairy_otherthan_Raid.GetList(Session["UserID"].ToString());
                    if (ddsearch.Text != "Select" && txtpage1.Text != "")
                    {
                       
                        if (ddsearch.SelectedValue == "raid_date")
                        {
                            var ad = (from s in dd
                                      where s.raid_entry_date == txtpage.Text
                                      select s);
                            grdRaidDetails.DataSource = ad.ToArray();
                            grdRaidDetails.DataBind();
                        }
                        //if (ddsearch.SelectedValue == "raid_location")
                        //{
                        //    var ad = (from s in dd
                        //              where s.place_of_raid == txtpage.Text
                        //              select s);
                        //    grdRaidDetails.DataSource = ad.ToArray();
                        //    grdRaidDetails.DataBind();
                        //}
                    }
                }

            }
            else
            {
                grdRaidDetails.PageIndex = 0;
                TextBox txtpage1 = (TextBox)row.Cells[0].FindControl("txtSearch2");
                DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
                if (txtpage1.Text == "")
                    if (Session["txtpage"] != null)
                        txtpage1.Text = Session["txtpage"].ToString();
                if (ddsearch.SelectedValue == "Select" || ddsearch.SelectedValue == "")
                    if (Session["ddsearch"] != null)
                        ddsearch.SelectedValue = Session["ddsearch"].ToString();
                dd = BL_cm_seiz_Daily_Dairy_otherthan_Raid.GetList(Session["UserID"].ToString());
                grdRaidDetails.DataSource = dd.ToArray();
               grdRaidDetails.DataBind();
            }



        }
        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                GridViewRow row = grdRaidDetails.TopPagerRow;
                TextBox txtpage = (TextBox)row.Cells[0].FindControl("txtSearch2");
                DropDownList ddsearch = (DropDownList)row.Cells[0].FindControl("ddlsearch1");
                Session["txtpage"] = "";
                Session["ddsearch"] = "Select";
                Session["Reset"] = "Y";
                dd = BL_cm_seiz_Daily_Dairy_otherthan_Raid.GetList(Session["UserID"].ToString());
                grdRaidDetails.DataSource = dd;
                grdRaidDetails.DataBind();
            }
        }



    }
}