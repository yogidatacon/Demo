using System;
using System.Collections.Generic;
using System.Linq;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class NestedMasterCaseMgmt : System.Web.UI.MasterPage
    {
        List<Thana_Details> thana1 = new List<Thana_Details>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.Count == 0)
                {
                    Response.Redirect("~/LoginPage");
                }
                else
                {
                    string userid = Session["UserID"].ToString();
                    if (userid != string.Empty)
                    {
                        try
                        {
                            Session["UserID"] = userid;
                            UserDetails user = new UserDetails();
                            user = BL_CM_Common.CheckUser(userid.ToString());
                            lbDivision.Text = user.division_name;
                            lbDistrict.Text = user.district_name;
                            lbRaidBy.Text = user.department_name;
                            Session["RaidBy"] = user.department_name;
                            Session["division_code"] = user.division_code;
                            Session["district_code"] = user.district_code;
                            //if (Session["topdiv"].ToString() == "1")
                            //{
                            //    ldlfir.Text = "";
                            //    lblseizure.Text = "";
                            //}
                            //else
                            //{
                            if (Session["SFIR"] != null )
                            {
                                if (Session["SFIR"].ToString() == "")
                                {
                                    ldlfir.Text = "";
                                }
                                else
                                {
                                    ldlfir.Text = Session["SFIR"].ToString();
                                }
                               
                            }
                          

                            if (Session["seizureNo"] != null && Session["seizureNo"].ToString() != "")
                            {
                                lblseizure.Text = Session["seizureNo"].ToString();
                            }
                            else
                            {
                                lblseizure.Text = "";
                            }
                            // }
                            lblThana.Text = "";
                            if (userid.Contains("thana_"))
                            {
                                thana1 = BL_User_Mgnt.GetThanaList(string.Empty);
                                var ad = (from s in thana1
                                          where s.thana_code == Session["UserID"].ToString().Replace("thana_", "").Trim()
                                          select s);
                                lblThana.Text = ad.ToList()[0].thana_name;// + Session["RaidBy"].ToString().Substring(0,1).ToUpper();
                            }
                            else
                                thana.Visible = false;
                        }
                        catch (Exception exe)
                        {
                            Response.Redirect("~/LoginPage");
                        }

                    }
                    else
                    {
                        Session["UserID"] = Session["UserID"];
                    }
                }
            }
        }

        protected void rdExcise_Oncheckedchange(object sender, EventArgs e)
        {
            //rdPolice.Checked = false;
        }


        protected void rdpolice_Oncheckedchange(object sender, EventArgs e)
        {
            //rdExcise.Checked = false;
        }
    }
}