using FusionCharts.Charts;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class Uset_Mgmt : System.Web.UI.Page
    {
        string query = "";
        string selectedtype = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string strPreviousPage = "";
            
            if (Request.UrlReferrer != null && System.Web.HttpContext.Current.Session["UserID"] != null)
            {
                if (System.Web.HttpContext.Current.Session["UserID"].ToString() != "")
                {
                    Session["UserID"] = Request.QueryString["UserID"].ToString();
                    //if (Session["UserID"].ToString() == "admin")
                    //    Session["UserID"] = "Admin";
                }
                else
                {
                    Session["UserID"] = Session["UserID"];
                }
                Session["UserID"] = Session["UserID"];
                string userid = Session["UserID"].ToString();
             
                // string userid = "Admin";
                Session["UserID"] = userid;
               
                UserDetails user = new UserDetails();
                user = BL_UserDetails.CheckUser(userid);
                string username =user.user_name;
                Session["partycode"] = user.party_code;
             //if (user.role_name_code == 42)
             //{
             //    Session["UserID"] = Session["UserID"];
             //    //  Response.Redirect("SeizureList");
             //    //if (Session["UserID"].ToString().Contains("excise_") )
             //    //    Response.Redirect("https://prohibitionbihar.in/procasemg/dashboard.aspx?username=" + Session["UserID"].ToString());
             //    //else if (Session["UserID"].ToString().Contains("police_"))
             //    //    Response.Redirect("https://prohibitionbihar.in/policecase/dashboard?username=" + Session["UserID"].ToString());
             //}

             //}
             int value = BL_RawMaterialReceipt.GetGrainData(Session["partycode"].ToString());
                if (value == 1)
                {
                    btnSCM.Visible = false;
                    Response.Redirect("MainPage");
                }
                    if (user.role_name_code != 42)
                {
                    btnSCM_Click(sender, null);
                }
                else
                {
                    EA.Visible = false;
                }
            }

            //if (strPreviousPage == "" )
            //{
            //    Response.Redirect("~/LoginPage");
            //}
        }


        private void RenderChartD2()
        {

            //   Chart chart4 = new Chart("pie2d", "fourth_chart", "950", "500", "jsonurl", "Handler/Allotment.ashx");
            Chart chart5 = new Chart("column2d", "third_chart", "1000", "800", "jsonurl", "Handler/SugarCane.ashx");

            Chart chart6 = new Chart("pie2d", "sixth_chart", "500", "500", "jsonurl", "Handler/NOCDashboard.ashx");
            Chart chart7 = new Chart("pie2d", "seventh_chart", "500", "500", "jsonurl", "Handler/NOCQtyDashboard.ashx");
            Chart chart = new Chart("doughnut2d", "first_chart", "500", "500", "jsonurl", "Handler/MF1_STATUS.ashx");
            Chart chart2 = new Chart("doughnut2d", "second_chart", "500", "500", "jsonurl", "Handler/MF2_STATUS.ashx");
            Chart chart3 = new Chart("doughnut2d", "third_chart", "500", "500", "jsonurl", "Handler/MF3_STATUS.ashx");

            //Literal6.Text = chart5.Render();
            // Literal7.Text = chart4.Render();
            distdiv.Visible = true;


        }
        protected void rdbUnitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UnitTypeChange();
        }

        private void UnitTypeChange()
        {
            if (rdbUnitType.SelectedValue.ToString() == "S")
            {
                selectedtype = "S";
                sugarmill.Visible = true;
                distdiv.Visible = false;
                SugarMolasseslist.Visible = false;
                sugarMolassescharts.Visible = true;
                MolassesProduction.Visible = true;
                MolassesProductionList.Visible = false;

            }
            if (rdbUnitType.SelectedValue.ToString() == "D")
            {
                selectedtype = "D";
                sugarmill.Visible = false;
                distdiv.Visible = true;
                DWEthanolCharts.Visible = true;
                DWAbsoluteAlcoholChart.Visible = true;
                DWMolassesPurchasecharts.Visible = true;
                DWAbsoluteAlcohollist.Visible = false;
                DWMolassesPurchaselist.Visible = false;
                DWEthanolList.Visible = false;
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            sugarMolassescharts.Visible = true;
            MolassesProduction.Visible = true;
            SugarMolasseslist.Visible = false;
            MolassesProductionList.Visible = false;
            distdiv.Visible = false;
           // Sugarprint.Visible = false;
        }

        protected void btnback2_Click(object sender, EventArgs e)
        {
            sugarMolassescharts.Visible = true;
            SugarMolasseslist.Visible = false;
            MolassesProduction.Visible = true;
            MolassesProductionList.Visible = false;
            distdiv.Visible = false;
        }


        protected void btnback3_Click(object sender, EventArgs e)
        {
            DWEthanolCharts.Visible = true;
            DWAbsoluteAlcohollist.Visible = false;
            DWMolassesPurchaselist.Visible = false;
            DWEthanolList.Visible = false;
            sugarmill.Visible = false;
        }

        protected void btnback4_Click(object sender, EventArgs e)
        {
            DWAbsoluteAlcoholChart.Visible = true;
            DWAbsoluteAlcohollist.Visible = false;
            DWMolassesPurchaselist.Visible = false;
            DWEthanolList.Visible = false;
            sugarmill.Visible = false;
        }
        protected void btnback5_Click(object sender, EventArgs e)
        {
            DWMolassesPurchasecharts.Visible = true;
            DWAbsoluteAlcohollist.Visible = false;
            DWMolassesPurchaselist.Visible = false;
            DWEthanolList.Visible = false;
            sugarmill.Visible = false;
        }

        protected void btnprint_Click(object sender, EventArgs e)
        {
            //sugarMolassescharts.Visible = false;
            //SugarMolasseslist.Visible = true;
            //MolassesProduction.Visible = false;
            //rdbUnitType.Visible = false;
            //btnback.Visible = false;
            //btnprint.Visible = false;
            //Sugar.Visible = true;
            //Sugar.Attributes.Add("style", "height:110%");
            //Sugar.Attributes.Add("style", "width:1190px");
            ClientScript.RegisterStartupScript(typeof(Page), "key", "<script type='text/javascript'>printdiv('Sugarprint');;</script>");
            //MolassesProductionList.Visible = false;
            //GridView1.Visible = true;
            //distdiv.Visible = false;
        }

        protected void btnprint2_Click(object sender, EventArgs e)
        {
            sugarMolassescharts.Visible = false;
            SugarMolasseslist.Visible = false;
            MolassesProduction.Visible = false;
           // Sugarprint.Visible = false;
           // Sugar.Visible = false;
            Molasses.Visible = true;
            Molasses.Attributes.Add("style", "height:110%");
            Molasses.Attributes.Add("style", "width:1190px");
            // Sugar.Attributes["style"] = "width:100px; height:200px;";
            ClientScript.RegisterStartupScript(typeof(Page), "key", "<script type='text/javascript'>window.print();;</script>");
            MolassesProductionList.Visible = true;
            GridView2.Visible = true;
            distdiv.Visible = false;
        }

        protected void btnEA_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainPage");
        }
        protected void btnSCM_Click(object sender, EventArgs e)
        {

            Response.Redirect("SCMDashBoard");
        }
    }
}