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
    public partial class SeizureList : System.Web.UI.Page
    {
        List<cm_seiz_BasicIformation> _seizure = new List<cm_seiz_BasicIformation>();
        List<District> Districts = new List<District>();
        List<cm_seiz_BasicIformation> getThanaList = new List<cm_seiz_BasicIformation>();
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
                txtraiddate.Text = DateTime.Now.ToString("dd/mm/yyyy");
                Session["UserID"] = Session["UserID"];
                Session["seizureNo"] = "";
                //List<cm_seiz_BasicIformation> getRaidList = new List<cm_seiz_BasicIformation>();
                //getRaidList = BL_cm_seiz_BasicIformation.GetRaidList();
                //ddlRaidDate.DataSource = getRaidList;
                //ddlRaidDate.DataTextField = "raid_date";
                ////ddlRaidDate.DataValueField = "gender_code";
                //ddlRaidDate.DataBind();
                //ddlRaidDate.Items.Insert(0, "Select");

                //Get distinct Thana list from BasicInformation 
                List<Division> divisions = new List<Division>();
                divisions = BL_User_Mgnt.GetDivisions("");
                ddlDivision.DataSource = divisions;
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_Code";
                ddlDivision.DataBind();
                ddlDivision.Items.Insert(0, "Select");

                getThanaList = new List<cm_seiz_BasicIformation>();
                getThanaList = BL_cm_seiz_BasicIformation.GetThanaList();

                string distcode = Session["district_code"].ToString();

                string username = Session["UserID"].ToString();
                UserDetails user1 = new UserDetails();
                user1 = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                if (user1.role_name_code == 42)
                {
                    _seizure = BL_cm_seiz_BasicIformation.GetSubmittedSeizureListByName(username + "&" + Session["district_code"].ToString());
                    string raidby = Session["RaidBy"].ToString();
                    if (Session["RaidBy"].ToString().Substring(0, 1).ToUpper() == "E")
                        raidby = "E";
                    else
                        raidby = "P";
                    var ad = (from s in _seizure
                              where s.user_id == user1.user_id /*&& s.raidby == Session["RaidBy"].ToString()*/
                              orderby s.seizureno descending
                              select s);
                    grdSeizureView.DataSource = ad.ToList();
                    grdSeizureView.DataBind();
                    _seizure = BL_cm_seiz_BasicIformation.GetUnSubmittedSeizureList(username + "&" + Session["district_code"].ToString());
                    var ad1 = (from s in _seizure
                               where  s.user_id == user1.user_id
                               orderby s.seizureno descending
                               select s);

                    grdUnSubmittedList.DataSource = ad1.ToList();
                    grdUnSubmittedList.DataBind();
                    for (int i = 0; i < grdUnSubmittedList.Rows.Count; i++)
                    {
                        string user = (grdUnSubmittedList.Rows[i].FindControl("lbluserid") as Label).Text;
                        if (Session["UserID"].ToString().Contains("thana_"))
                        {
                            if (Session["UserID"].ToString() != user)
                                (grdUnSubmittedList.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                        }
                    }
                    var ad2 = (from s in getThanaList
                               where s.district_code == distcode
                               select s);
                    ddlThana.DataSource = ad2.ToArray();
                   // ddlsubthana.DataSource = ad2.ToArray();
                    ddlDivision.SelectedValue = user1.division_code;
                    ddlDivision_SelectedIndexChanged(sender, e);
                    ddDistrict.SelectedValue = user1.district_code;
                    ddldistrict_SelectedIndexChanged(sender, e);
                    ddDistrict.Enabled = false;
                    ddlDivision.Enabled = false;

                }

                else if (user1.role_name_code == 4)
                {
                    _seizure = BL_cm_seiz_BasicIformation.GetSubmittedSeizureListByALL(Session["RaidBy"].ToString());
                    var ad = (from s in _seizure
                              where s.district_code == distcode && s.raidby== Session["RaidBy"].ToString()
                              orderby s.seizureno descending
                              select s);
                    grdSeizureView.DataSource = ad.ToList();
                    grdSeizureView.DataBind();



                    _seizure = BL_cm_seiz_BasicIformation.GetUnSubmittedSeizureAllList(Session["RaidBy"].ToString());
                    var ad1 = (from s in _seizure
                               where s.district_code == distcode && s.raidby == Session["RaidBy"].ToString()
                               orderby s.seizureno descending
                               select s);

                    grdUnSubmittedList.DataSource = ad1.ToList();
                    grdUnSubmittedList.DataBind();
                    for (int i = 0; i < grdUnSubmittedList.Rows.Count; i++)
                    {
                        string user = (grdUnSubmittedList.Rows[i].FindControl("lbluserid") as Label).Text;
                        if (user != username)
                        {
                            (grdUnSubmittedList.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                        }

                    }
                    var ad2 = (from s in getThanaList
                               where s.district_code == distcode
                               select s);
                    ddlThana.DataSource = ad2.ToArray();
                    //ddlsubthana.DataSource = ad2.ToArray();
                    ddlDivision.SelectedValue = user1.division_code;
                    ddlDivision_SelectedIndexChanged(sender, e);
                    ddDistrict.SelectedValue = user1.district_code;
                    ddDistrict.Enabled = false;
                    ddlDivision.Enabled = false;
                    ddldistrict_SelectedIndexChanged(sender, e);
                }
                else if (user1.role_name_code == 9)
                {
                    _seizure = BL_cm_seiz_BasicIformation.GetSubmittedSeizureListByALL(Session["RaidBy"].ToString());
                    var ad = (from s in _seizure
                              where s.division_code == user1.division_code && s.raidby == Session["RaidBy"].ToString()
                              orderby s.seizureno descending
                              select s);
                    grdSeizureView.DataSource = ad.ToList();
                    grdSeizureView.DataBind();



                    _seizure = BL_cm_seiz_BasicIformation.GetUnSubmittedSeizureAllList(Session["RaidBy"].ToString());
                    var ad1 = (from s in _seizure
                               where s.division_code == user1.division_code && s.raidby == Session["RaidBy"].ToString()
                               orderby s.seizureno descending
                               select s);

                    grdUnSubmittedList.DataSource = ad1.ToList();
                    grdUnSubmittedList.DataBind();
                    for (int i = 0; i < grdUnSubmittedList.Rows.Count; i++)
                    {
                        string user = (grdUnSubmittedList.Rows[i].FindControl("lbluserid") as Label).Text;
                        if (user != username)
                        {
                            (grdUnSubmittedList.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                        }

                    }
                    var ad2 = (from s in getThanaList
                               where s.division_code == user1.division_code
                               select s);
                    ddlThana.DataSource = ad2.ToArray();
                    //ddlsubthana.DataSource = ad2.ToArray();
                    ddlDivision.SelectedValue = user1.division_code;
                    ddlDivision_SelectedIndexChanged(sender, e);
                    ddlDivision.Enabled = false;
                    //ddDistrict.SelectedValue = user1.division_code;
                    //ddldistrict_SelectedIndexChanged(sender, e);
                }
                else if (user1.role_level_code == 3)
                {
                    _seizure = BL_cm_seiz_BasicIformation.GetSubmittedSeizureListByALL(Session["RaidBy"].ToString());
                    if(Session["UserID"].ToString()=="com")
                    {
                        var ad = (from s in _seizure

                                  orderby s.seizureno descending
                                  select s);
                        grdSeizureView.DataSource = ad.ToList();
                    }
                    else
                    {
                        var ad = (from s in _seizure
                                  where  s.raidby == Session["RaidBy"].ToString()
                                  orderby s.seizureno descending
                                  select s);
                        grdSeizureView.DataSource = ad.ToList();
                    }
                   
                  
                    grdSeizureView.DataBind();
                    _seizure = BL_cm_seiz_BasicIformation.GetUnSubmittedSeizureAllList(Session["RaidBy"].ToString());
                    if (Session["UserID"].ToString() == "com")
                    {
                        var ad1 = (from s in _seizure
                                   orderby s.seizureno descending
                                   select s);
                        grdUnSubmittedList.DataSource = ad1.ToList();
                    }
                    else
                    {
                        var ad1 = (from s in _seizure
                                   where s.raidby == Session["RaidBy"].ToString()
                                   orderby s.seizureno descending
                                   select s);
                        grdUnSubmittedList.DataSource = ad1.ToList();
                    }
                       
                    grdUnSubmittedList.DataBind();
                    for (int i = 0; i < grdUnSubmittedList.Rows.Count; i++)
                    {
                        string user = (grdUnSubmittedList.Rows[i].FindControl("lbluserid") as Label).Text;
                       
                                (grdUnSubmittedList.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                        
                    }
                    var ad2 = (from s in getThanaList
                             
                               select s);
                    ddlThana.DataSource = ad2.ToArray();
                  // ddlsubthana.DataSource = ad2.ToArray();
                   
                  
                }


                ddlThana.DataTextField = "thanaName";
                ddlThana.DataValueField = "thana_code";
                ddlThana.DataBind();
                ddlThana.Items.Insert(0, "Select");
                if (Session["UserID"].ToString().Contains("thana_"))
                {
                    string v = Session["UserID"].ToString();//.Replace("thana_", "").Trim();
                    ddlThana.SelectedValue =Session["UserID"].ToString().Replace("thana_","").Trim();
                    ddlThana.Enabled = false;
                    ddlsubthana.SelectedValue = Session["UserID"].ToString().Replace("thana_", "").Trim();
                    ddlsubthana.Enabled = false;
                }
            }
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            Session["rtype"] = 0;
            Session["rtype1"] = 0;
            Response.Redirect("BasicIformationForm");
        }

        protected void ddlSelect_SelectedIndexChange(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// Seach Unsubmitted records 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
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
                UserDetails user1 = new UserDetails();
                user1 = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                string _thana = ddlThana.SelectedValue?.ToString() ?? string.Empty; //ddlThana.SelectedItem?.ToString()??string.Empty;
                string _raidDate = txtraiddate.Text;
                string _seizureNo =txtunseizureno.Text;
                if (_thana == "Select" && _raidDate == "" && _seizureNo == "")
                {
                    string username = Session["UserID"].ToString();
                   // grdUnSubmittedList.PageIndex = e.NewPageIndex;
                    _seizure = new List<cm_seiz_BasicIformation>();
                    _seizure = BL_cm_seiz_BasicIformation.GetUnSubmittedSeizureList(username + "&" + Session["district_code"].ToString());
                    grdUnSubmittedList.DataSource = _seizure.ToList();
                    grdUnSubmittedList.DataBind();
                }
                else
                {
                    string username = Session["UserID"].ToString();
                  //  grdUnSubmittedList.PageIndex = e.NewPageIndex;
                    _seizure = BL_cm_seiz_BasicIformation.GetUnsubmittedList(_thana, _raidDate,_seizureNo);
                    if (user1.role_name_code == 42)
                    {
                        var ad = (from s in _seizure
                                  where s.user_id == user1.user_id 
                                  orderby s.seizureno descending
                                  select s);
                        grdUnSubmittedList.DataSource = ad.ToList();
                        grdUnSubmittedList.DataBind();
                    }
                    else if (user1.role_name_code == 4)
                    {
                        var ad = (from s in _seizure
                                  where s.district_code == user1.district_code && s.raidby == Session["RaidBy"].ToString()
                                  orderby s.seizureno descending
                                  select s);
                        grdUnSubmittedList.DataSource = ad.ToList();
                        grdUnSubmittedList.DataBind();
                       
                       
                            for (int i = 0; i < grdUnSubmittedList.Rows.Count; i++)
                            {
                            string user = (grdUnSubmittedList.Rows[i].FindControl("lbluserid") as Label).Text;
                            if (user!=username)
                            {
                                (grdUnSubmittedList.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                            }

                            }
                        
                    }
                    else if (user1.role_name_code == 9)
                    {
                        var ad = (from s in _seizure
                                  where s.division_code == user1.division_code && s.raidby == Session["RaidBy"].ToString()
                                  orderby s.seizureno descending
                                  select s);
                        grdUnSubmittedList.DataSource = ad.ToList();
                        grdUnSubmittedList.DataBind();


                        for (int i = 0; i < grdUnSubmittedList.Rows.Count; i++)
                        {
                            string user = (grdUnSubmittedList.Rows[i].FindControl("lbluserid") as Label).Text;
                            if (user != username)
                            {
                                (grdUnSubmittedList.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                            }

                        }

                    }
                    else if (user1.role_level_code == 3)
                    {
                        var ad = (from s in _seizure
                                  orderby s.seizureno descending
                                  select s);
                        grdUnSubmittedList.DataSource = ad.ToList();
                        grdUnSubmittedList.DataBind();
                        for (int i = 0; i < grdUnSubmittedList.Rows.Count; i++)
                        {
                            string user = (grdUnSubmittedList.Rows[i].FindControl("lbluserid") as Label).Text;
                            if (user != username)
                            {
                                (grdUnSubmittedList.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                            }

                        }
                    }
                    else
                    {
                        grdUnSubmittedList.DataSource = _seizure.ToList();
                        grdUnSubmittedList.DataBind();
                    }
                  
                }
            }
        }

        protected void btnSearchSeizureNo_Click(object sender, EventArgs e)
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

            string _seizureNo = txtSearch.Text;
            string prfirno = txtprfirno.Text;
            string thana = ddlsubthana.SelectedValue;
            if(ddlsubthana.SelectedValue=="Select")
            {
                thana = "";
            }
            if (_seizureNo != "" || prfirno != "" || thana != "")
            {
                //if (prfirno != "" && _seizureNo != "" && thana != "")
                //{
                    _seizure = BL_cm_seiz_BasicIformation.GetSubmittedSeizureList(_seizureNo, prfirno, thana);
              //  }
              //else  if (prfirno != "" && _seizureNo != "" && thana == "")
              //  {
              //      _seizure = BL_cm_seiz_BasicIformation.GetSubmittedSeizureList(_seizureNo, prfirno, "");
              //  }
              //  else if (thana != "" && prfirno != "" && _seizureNo == "")
              //  {
              //      _seizure = BL_cm_seiz_BasicIformation.GetSubmittedSeizureList("", prfirno, thana);
              //  }
              // else if (thana != "" && _seizureNo != "" && prfirno == "")
              //      {
              //          _seizure = BL_cm_seiz_BasicIformation.GetSubmittedSeizureList(_seizureNo, "", thana);
              //      }
              //  else if (thana != "" && _seizureNo == "" && prfirno == "")
              //  {
              //      _seizure = BL_cm_seiz_BasicIformation.GetSubmittedSeizureList("", "", thana);
              //  }
              //  else if (thana == "" && _seizureNo != "" && prfirno == "")
              //  {
              //      _seizure = BL_cm_seiz_BasicIformation.GetSubmittedSeizureList(_seizureNo, "", "");
              //  }
              //  else if (thana == "" && _seizureNo == "" && prfirno != "")
              //  {
              //      _seizure = BL_cm_seiz_BasicIformation.GetSubmittedSeizureList("", prfirno, "");
              //  }
              //  else  if (prfirno == "" && _seizureNo != "")
              //  {
              //      _seizure = BL_cm_seiz_BasicIformation.GetSubmittedSeizureList(_seizureNo, "","");
              //  }
              // else if (prfirno != "" && _seizureNo == "")
              //  {
              //      _seizure = BL_cm_seiz_BasicIformation.GetSubmittedSeizureList("", prfirno,"");
              //  }
              
              // else if (thana != "" && _seizureNo == "" )
              //  {
              //      _seizure = BL_cm_seiz_BasicIformation.GetSubmittedSeizureList("","", thana);
              //  }
              //else  if (thana != ""  && prfirno == "")
              //  {
              //      _seizure = BL_cm_seiz_BasicIformation.GetSubmittedSeizureList("","", thana);
              //  }
               
                UserDetails user1 = new UserDetails();
                user1 = BL_UserDetails.CheckUser(Session["UserID"].ToString());
                if (user1.role_name_code == 42)
                {
                    var ad1 = (from s in _seizure
                               where  s.user_id ==user1.user_id /*&& s.raidby == Session["RaidBy"].ToString()*/ /*&& s.district_code == user1.district_code*/
                               select s);

                    grdSeizureView.DataSource = ad1.ToList();
                }
             else   if (user1.role_name_code == 4)
                {
                    var ad1 = (from s in _seizure
                               where s.raidby == Session["RaidBy"].ToString() && s.district_code == user1.district_code
                               select s);

                    grdSeizureView.DataSource = ad1.ToList();
                }
                else if (user1.role_name_code == 9)
                {
                    var ad1 = (from s in _seizure
                               where s.raidby == Session["RaidBy"].ToString() && s.division_code ==user1.division_code
                               select s);

                    grdSeizureView.DataSource = ad1.ToList();
                }
                else if (user1.role_level_code == 3)
                {
                    if (Session["UserID"].ToString() == "com")
                    {
                        var ad = (from s in _seizure

                                  orderby s.seizureno descending
                                  select s);
                        grdSeizureView.DataSource = ad.ToList();
                    }
                    else
                    {
                        var ad = (from s in _seizure
                                  where s.raidby == Session["RaidBy"].ToString()
                                  orderby s.seizureno descending
                                  select s);
                        grdSeizureView.DataSource = ad.ToList();
                    }
                }
                else
                {
                    var ad1 = (from s in _seizure
                               select s);

                    grdSeizureView.DataSource = ad1.ToList();
                }

                grdSeizureView.DataBind();
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            string ID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblSeizureNo") as Label).Text;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "1";
            Session["rtype1"] = "1";
            Session["stype"] = "0";
            Session["seizureno"] = ID;
            Response.Redirect("BasicIformationForm");
        }
        protected void btnSubView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            string ID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblSeizureNo") as Label).Text;
            string FIR = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblprfirno") as Label).Text;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "1";
            Session["rtype1"] = "1";
            Session["stype"] = "1";
            Session["seizureno"] = ID;
            Session["SFIR"]= FIR;
            Response.Redirect("BasicIformationForm");
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            string ID = (gvr.Cells[gvr.Cells.Count - 1].FindControl("lblSeizureNo") as Label).Text;
            Session["UserID"] = Session["UserID"];
            Session["rtype"] = "2";
            Session["rtype1"] = "2";
            Session["stype"] = "0";
            Session["seizureno"] = ID;
            Response.Redirect("BasicIformationForm");
        }

        protected void grdSeizureView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string username = Session["UserID"].ToString();
            grdSeizureView.PageIndex = e.NewPageIndex;
            _seizure = new List<cm_seiz_BasicIformation>();
            UserDetails user1 = new UserDetails();
            user1 = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            if (user1.role_name_code ==42)
            {
                if (ddlsubthana.SelectedValue != "Select")
                {
                   
                        _seizure = BL_cm_seiz_BasicIformation.GetSubmittedSeizureList("", "", ddlsubthana.SelectedValue);
                    var ad = (from s in _seizure
                              where s.user_id == user1.user_id 
                              orderby s.seizureno descending
                              select s);
                    grdSeizureView.DataSource = ad.ToList();
                    grdSeizureView.DataBind();
                }
                else
                {
                    _seizure = BL_cm_seiz_BasicIformation.GetSubmittedSeizureListByName(username + "&" + Session["district_code"].ToString());
                    var ad = (from s in _seizure
                              where s.user_id == user1.user_id 
                              orderby s.seizureno descending
                              select s);
                    grdSeizureView.DataSource = ad.ToList();
                    grdSeizureView.DataBind();
                }
            }
            else if(user1.role_name_code == 4)
            {
                if (ddlsubthana.SelectedValue != "Select")
                {
                    _seizure = BL_cm_seiz_BasicIformation.GetSubmittedSeizureList("", "", ddlsubthana.SelectedValue);
                    var ad = (from s in _seizure
                              where s.district_code == user1.district_code && s.raidby == Session["RaidBy"].ToString()
                              orderby s.seizureno descending
                              select s);
                    grdSeizureView.DataSource = ad.ToList();
                    grdSeizureView.DataBind();
                }
                else
                {
                    _seizure = BL_cm_seiz_BasicIformation.GetSubmittedSeizureListByALL(Session["RaidBy"].ToString());
                    var ad = (from s in _seizure
                              where s.district_code == user1.district_code && s.raidby == Session["RaidBy"].ToString()
                              orderby s.seizureno descending
                              select s);
                    grdSeizureView.DataSource = ad.ToList();
                    grdSeizureView.DataBind();
                }
            }
            else if (user1.role_name_code == 9)
            {
                if (ddlsubthana.SelectedValue != "Select")
                {
                    _seizure = BL_cm_seiz_BasicIformation.GetSubmittedSeizureList("", "", ddlsubthana.SelectedValue);
                    var ad = (from s in _seizure
                              where s.division_code == user1.division_code && s.raidby == Session["RaidBy"].ToString()
                              orderby s.seizureno descending
                              select s);
                    grdSeizureView.DataSource = ad.ToList();
                    grdSeizureView.DataBind();
                }
                else
                {
                    _seizure = BL_cm_seiz_BasicIformation.GetSubmittedSeizureListByALL(Session["RaidBy"].ToString());
                    var ad = (from s in _seizure
                              where s.division_code== user1.division_code && s.raidby == Session["RaidBy"].ToString()
                              orderby s.seizureno descending
                              select s);
                    grdSeizureView.DataSource = ad.ToList();
                    grdSeizureView.DataBind();
                }
            }
            else if(user1.role_level_code == 3)
            {
                if (ddlsubthana.SelectedValue != "Select")
                {
                    _seizure = BL_cm_seiz_BasicIformation.GetSubmittedSeizureList("", "", ddlsubthana.SelectedValue);
                    if (Session["UserID"].ToString() == "com")
                    {
                        var ad = (from s in _seizure

                                  orderby s.seizureno descending
                                  select s);
                        grdSeizureView.DataSource = ad.ToList();
                    }
                    else
                    {
                        var ad = (from s in _seizure
                                  where s.raidby == Session["RaidBy"].ToString()
                                  orderby s.seizureno descending
                                  select s);
                        grdSeizureView.DataSource = ad.ToList();
                    }
                    grdSeizureView.DataBind();
                }
                else
                {
                    _seizure = BL_cm_seiz_BasicIformation.GetSubmittedSeizureListByALL(Session["RaidBy"].ToString());
                    var ad = (from s in _seizure
                              orderby s.seizureno descending
                              select s);
                    grdSeizureView.DataSource = ad.ToList();
                    grdSeizureView.DataBind();
                }
            }
        }

        protected void grdUnSubmittedList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string _thana = ddlThana.SelectedValue?.ToString() ?? string.Empty; //ddlThana.SelectedItem?.ToString()??string.Empty;
            string _raidDate =txtraiddate.Text;
            string _seizureNo = txtunseizureno.Text;
            UserDetails user1 = new UserDetails();
            user1 = BL_UserDetails.CheckUser(Session["UserID"].ToString());
            if (_thana == "Select"  &&_raidDate == "" &&_seizureNo == ""  )
            {
                string username = Session["UserID"].ToString();
                grdUnSubmittedList.PageIndex = e.NewPageIndex;

                if (user1.role_name_code==42)
                {
                    _seizure = BL_cm_seiz_BasicIformation.GetUnSubmittedSeizureList(username + "&" + Session["district_code"].ToString());
                    var ad = (from s in _seizure
                              where s.user_id == user1.user_id 
                              orderby s.seizureno descending
                              select s);
                    grdUnSubmittedList.DataSource = ad.ToList();
                    grdUnSubmittedList.DataBind();
                }
                else if (user1.role_name_code == 4)
                {
                    _seizure = BL_cm_seiz_BasicIformation.GetUnSubmittedSeizureAllList(Session["RaidBy"].ToString());
                    var ad = (from s in _seizure
                              where s.district_code == user1.district_code && s.raidby == Session["RaidBy"].ToString()
                              orderby s.seizureno descending
                              select s);
                    grdUnSubmittedList.DataSource = ad.ToList();
                    grdUnSubmittedList.DataBind();
                    for (int i = 0; i < grdUnSubmittedList.Rows.Count; i++)
                    {
                        string user = (grdUnSubmittedList.Rows[i].FindControl("lbluserid") as Label).Text;
                        if (user != username)
                        {
                            (grdUnSubmittedList.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                        }

                    }
                }
                //role_level_code
                else if (user1.role_name_code == 9)
                {
                    _seizure = BL_cm_seiz_BasicIformation.GetUnSubmittedSeizureAllList(Session["RaidBy"].ToString());
                    var ad = (from s in _seizure
                              where s.division_code == user1.division_code && s.raidby == Session["RaidBy"].ToString()
                              orderby s.seizureno descending
                              select s);
                    grdUnSubmittedList.DataSource = ad.ToList();
                    grdUnSubmittedList.DataBind();
                    for (int i = 0; i < grdUnSubmittedList.Rows.Count; i++)
                    {
                        string user = (grdUnSubmittedList.Rows[i].FindControl("lbluserid") as Label).Text;
                        if (user != username)
                        {
                            (grdUnSubmittedList.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                        }

                    }
                }
                else if (user1.role_level_code == 3)
                {
                    _seizure = BL_cm_seiz_BasicIformation.GetUnSubmittedSeizureAllList(Session["RaidBy"].ToString());
                    if (Session["UserID"].ToString() == "com")
                    {
                        var ad1 = (from s in _seizure
                                   orderby s.seizureno descending
                                   select s);
                        grdUnSubmittedList.DataSource = ad1.ToList();
                    }
                    else
                    {
                        var ad1 = (from s in _seizure
                                   where s.raidby == Session["RaidBy"].ToString()
                                   orderby s.seizureno descending
                                   select s);
                        grdUnSubmittedList.DataSource = ad1.ToList();
                    }
                    grdUnSubmittedList.DataBind();
                }
            }
            else
            {
                string username = Session["UserID"].ToString();
                grdUnSubmittedList.PageIndex = e.NewPageIndex;
                _seizure = BL_cm_seiz_BasicIformation.GetUnsubmittedList(_thana, _raidDate,_seizureNo);
                if (user1.role_name_code == 42)
                {
                   // _seizure = BL_cm_seiz_BasicIformation.GetUnSubmittedSeizureList(username + "&" + Session["district_code"].ToString());
                    var ad = (from s in _seizure
                              where s.user_id == user1.user_id 
                              orderby s.seizureno descending
                              select s);
                    grdUnSubmittedList.DataSource = ad.ToList();
                    grdUnSubmittedList.DataBind();
                }
                else if (user1.role_name_code == 4)
                {
                 //   _seizure = BL_cm_seiz_BasicIformation.GetUnSubmittedSeizureAllList();
                    var ad = (from s in _seizure
                              where s.district_code == user1.district_code && s.raidby == Session["RaidBy"].ToString()
                              orderby s.seizureno descending
                              select s);
                    grdUnSubmittedList.DataSource = ad.ToList();
                    grdUnSubmittedList.DataBind();
                    for (int i = 0; i < grdUnSubmittedList.Rows.Count; i++)
                    {
                        string user = (grdUnSubmittedList.Rows[i].FindControl("lbluserid") as Label).Text;
                        if (user != username)
                        {
                            (grdUnSubmittedList.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                        }

                    }
                }
                else if (user1.role_name_code == 9)
                {
                    //   _seizure = BL_cm_seiz_BasicIformation.GetUnSubmittedSeizureAllList();
                    var ad = (from s in _seizure
                              where s.division_code == user1.division_code && s.raidby == Session["RaidBy"].ToString()
                              orderby s.seizureno descending
                              select s);
                    grdUnSubmittedList.DataSource = ad.ToList();
                    grdUnSubmittedList.DataBind();
                    for (int i = 0; i < grdUnSubmittedList.Rows.Count; i++)
                    {
                        string user = (grdUnSubmittedList.Rows[i].FindControl("lbluserid") as Label).Text;
                        if (user != username)
                        {
                            (grdUnSubmittedList.Rows[i].FindControl("btnEdit") as LinkButton).Visible = false;
                        }

                    }
                }
                else if (user1.role_level_code == 3)
                {
                    // _seizure = BL_cm_seiz_BasicIformation.GetUnSubmittedSeizureAllList();
                    if (Session["UserID"].ToString() == "com")
                    {
                        var ad1 = (from s in _seizure
                                   orderby s.seizureno descending
                                   select s);
                        grdUnSubmittedList.DataSource = ad1.ToList();
                    }
                    else
                    {
                        var ad1 = (from s in _seizure
                                   where s.raidby == Session["RaidBy"].ToString()
                                   orderby s.seizureno descending
                                   select s);
                        grdUnSubmittedList.DataSource = ad1.ToList();
                    }
                    grdUnSubmittedList.DataBind();
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            _seizure = new List<cm_seiz_BasicIformation>();
            _seizure = BL_cm_seiz_BasicIformation.GetSubmittedSeizureListByName(Session["UserID"].ToString() + "&" + Session["district_code"].ToString());
            grdSeizureView.DataSource = _seizure.ToList();
            grdSeizureView.DataBind();
        }

        protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            getThanaList = new List<cm_seiz_BasicIformation>();
            getThanaList = BL_cm_seiz_BasicIformation.GetThanaList();
            var ad2 = (from s in getThanaList
                       where s.district_code == ddDistrict.SelectedValue
                       select s);
            ddlsubthana.DataSource = ad2.ToArray();
            ddlsubthana.DataTextField = "thanaName";
            ddlsubthana.DataValueField = "thana_code";
            ddlsubthana.DataBind();
            ddlsubthana.Items.Insert(0, "Select");


        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            Districts = new List<District>();
            Districts = BL_User_Mgnt.GetDistricts(ddlDivision.SelectedValue);
            var org_master1 = from s in Districts
                              where s.division_Code == ddlDivision.SelectedValue
                              select s;
            ddDistrict.DataSource = org_master1.ToList();
            ddDistrict.DataTextField = "District_Name";
            ddDistrict.DataValueField = "District_Code";
            ddDistrict.DataBind();
            ddDistrict.Items.Insert(0, "Select");
        }
    }
}