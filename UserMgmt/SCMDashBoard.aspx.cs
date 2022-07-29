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
    public partial class SCMDashBoard : System.Web.UI.Page
    {
            string query = "";
            string selectedtype = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string strPreviousPage = "";

            if (Request.UrlReferrer != null)
            {
                Session["UserID"] = Session["UserID"];
                // Session["UserName"] = Session["UserID"];
                strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                RenderChartD2();
            }
            if (strPreviousPage == "")
            {
                Response.Redirect("~/LoginPage");
            }
            Session["UserID"] = Session["UserID"];
            string userid = Session["UserID"].ToString();
            // string userid = "Admin";
            Session["UserID"] = userid;
            string username = Session["Username"].ToString();
            UserDetails user = new UserDetails();
            user = BL_UserDetails.CheckUser(userid);
            Session["party_code"] = user.party_code;
            Session["party_type"] = user.party_type;
            //if (!this.IsPostBack)
            //{

            string district = Request.QueryString["label"]?.ToString() ?? string.Empty;
            string viewno = Request.QueryString["viewno"]?.ToString() ?? string.Empty;
            //btnprint.Visible = false;
            //btnback.Visible = false;
            //btnprint2.Visible = false;
            //btnback2.Visible = false;
            //btnprint3.Visible = false;
            //btnback3.Visible = false;
            //btnprint4.Visible = false;
            //btnback4.Visible = false;
            //btnprint5.Visible = false;
            //btnback5.Visible = false;
            DataTable dt1 = new DataTable();

            if (userid == "com" || userid == "hodyco")
            {
                rdbUnitType.Visible = true;
                if(rdbUnitType.SelectedValue == "C")
                {
                    Response.Redirect("CMS_DashBoard.aspx");
                }
            }
            if(user.role_name_code == 42 || user.role_level_code == 11 || user.role_level_code == 12)
            {
                dasboard.Visible = false;
            }

            if (rdbUnitType.SelectedValue.ToString() == "" && viewno == "")
            {
                if (Session["rdbUnitType"] != null)
                {
                    rdbUnitType.SelectedValue = Session["rdbUnitType"].ToString();
                }
                else
                {
                    rdbUnitType.SelectedValue = "S";
                }
            }
            if ((rdbUnitType.SelectedValue.ToString() == "") && (viewno == "0" || viewno == "1"))
            {
                if (Session["rdbUnitType"]!=null)
                {
                    rdbUnitType.SelectedValue = Session["rdbUnitType"].ToString();
                }
                else
                {
                    rdbUnitType.SelectedValue = "S";
                }
                
            }
            else if ((rdbUnitType.SelectedValue.ToString() == "") && (viewno == "99" || viewno == "98" || viewno == "97"))
            {
                rdbUnitType.SelectedValue = "D";
            }
            UnitTypeChange();

            if (user.party_type == "Sugar Mill")
            {
                rdbUnitType.Visible = false;
                sugarmill.Visible = true;
                distdiv.Visible = false;
                SugarMolasseslist.Visible = false;
                sugarMolassescharts.Visible = true;
                MolassesProduction.Visible = true;
                MolassesProductionList.Visible = false;
            }
            if (user.party_type == "Distillery Unit" || user.party_type == "ENA Distillery Unit")
            {
                rdbUnitType.Visible = false;
                sugarmill.Visible = false;
                distdiv.Visible = true;
               
                DWEthanolCharts.Visible = true;
                DWAbsoluteAlcoholChart.Visible = true;
                DWMolassesPurchasecharts.Visible = true;
                DWAbsoluteAlcohollist.Visible = false;
                DWMolassesPurchaselist.Visible = false;
                DWEthanolList.Visible = false;

            }
            if ( user.party_type == "ENA Distillery Unit")
            {
                rdbUnitType.Visible = false;
                sugarmill.Visible = false;
                distdiv.Visible = true;
                purchase.InnerText = "Raw Material Purchase Vs Consumption";
                AAPC.InnerText = "Production Vs Consumption";
                EPS.InnerText = "Production Vs Sale";
                DWEthanolCharts.Visible = true;
                DWAbsoluteAlcoholChart.Visible = true;
                DWMolassesPurchasecharts.Visible = true;
                DWAbsoluteAlcohollist.Visible = false;
                DWMolassesPurchaselist.Visible = false;
                DWEthanolList.Visible = false;

            }
            if (Session["party_type"].ToString() == "M & tP")
            {
                Literal2.Visible = false;
                Literal4.Visible = false;
                rdbUnitType.Visible = false;
                sugarmill.Visible = false;
                btnEA.Visible = true;
                distdiv.Visible = true;
                DWEthanolCharts.Visible =false;
                DWAbsoluteAlcoholChart.Visible = false;
                purchase.InnerText = "Raw Material Purchase Vs Consumption";
                //purchase.Visible = false;
                DWMolassesPurchasecharts.Visible =true;
                DWAbsoluteAlcohollist.Visible = false;
                DWMolassesPurchaselist.Visible = false;
                DWEthanolList.Visible = false;
            }
            string conn = System.Configuration.ConfigurationManager.ConnectionStrings["CASEMGMT"].ConnectionString;
            NpgsqlConnection cn1 = new NpgsqlConnection(conn);

            //if (viewno == "0" || viewno == "LOCKS CREATED AT SOURCE")
            //{
            //    query = "SELECT  a.total_purchase ,a.total_canecrushed,sum(a.total_purchase) - sum(a.total_canecrushed) AS closingbalance,to_char(a.entrydate, 'DD/MM/YYYY') as entrydate FROM exciseautomation.sugarcanepurchase a inner join exciseautomation.party_master b on a.party_code = b.party_code where b.party_name = '" + district + "' GROUP BY a.entrydate,a.total_purchase,a.total_canecrushed   ";
            //    using (NpgsqlConnection con = new NpgsqlConnection(conn))
            //    {
            //        con.Open();
            //        using (NpgsqlCommand command = new NpgsqlCommand())
            //        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, cn1))
            //        {
            //            // fill data table
            //            da.Fill(dt1);
            //        }
            //    }
            //    GridView1.Visible = true;
            //    GridView2.Visible = false;
            //    //GridView3.Visible = false;
            //    //GridView4.Visible = false;
            //    //GridView5.Visible = false;
            //    GridView1.DataSource = dt1;
            //    GridView1.DataBind();
            //    //GridView6.DataSource = dt1;
            //    // GridView6.DataBind();
            //    //GridView6.Visible = false;
            //    lblHeading.Text = "Sugar Cane Purchase v/s Consumption For : " + district;
            //    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "Func()", true);
            //    // btnprint.Visible = true;
            //    btnback.Visible = true;
            //    sugarMolassescharts.Visible = false;
            //    SugarMolasseslist.Visible = true;
            //    MolassesProduction.Visible = true;
            //    MolassesProductionList.Visible = false;
            //    distdiv.Visible = false;
            //    viewno = string.Empty;

            //}

            //if (viewno == "99" || viewno == "LOCKS CREATED AT SOURCE")
            //{
            //    query = "select user_name ,case when lag(closingbalance,1) over (order by user_name) isnull then openingbalance else lag(closingbalance,1) over (order by user_name)  end as OpeningBalance,  Purchase totalPurchase,Consumption as totalConsumption,closingbalance as closingbalance from exciseautomation.distillerywisemolassespurchasevsconsumption where user_name='" + district + "'";
            //    using (NpgsqlConnection con = new NpgsqlConnection(conn))
            //    {
            //        con.Open();
            //        using (NpgsqlCommand command = new NpgsqlCommand())
            //        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, cn1))
            //        {
            //            // fill data table
            //            da.Fill(dt1);

            //        }

            //    }
            //    GridView1.Visible = false;
            //    GridView2.Visible = false;
            //    GridView3.Visible = false;
            //    GridView4.Visible = false;
            //    GridView5.Visible = true;
            //    GridView5.DataSource = dt1;
            //    GridView5.DataBind();
            //    lblHeading5.Text = "Molasses Purchase v/s Consumption For : " + district;
            //    //GridView5.HeaderRow.TableSection = TableRowSection.TableHeader;
            //    //btnprint5.Visible = true;
            //    btnback5.Visible = true;
            //    DWMolassesPurchasecharts.Visible = false;
            //    DWMolassesPurchaselist.Visible = true;
            //    DWEthanolList.Visible = false;
            //    DWAbsoluteAlcohollist.Visible = false;
            //    sugarmill.Visible = false;
            //    //rdbUnitType.SelectedValue = "D";
            //    viewno = string.Empty;

            //}
            //if (viewno == "98" || viewno == "LOCKS CREATED AT SOURCE")
            //{
            //    query = "select user_name ,case when lag(closingbalance,1) over (order by user_name) isnull then openingbalance else lag(closingbalance,1) over (order by user_name)  end as OpeningBalance,  Purchase totalPurchase,Consumption as totalConsumption,closingbalance as closingbalance from exciseautomation.distillerywiseabsolutealcoholproductionvsconsumption where user_name='" + district + "' ";
            //    using (NpgsqlConnection con = new NpgsqlConnection(conn))
            //    {
            //        con.Open();
            //        using (NpgsqlCommand command = new NpgsqlCommand())
            //        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, cn1))
            //        {
            //            // fill data table
            //            da.Fill(dt1);

            //        }

            //    }
            //    GridView1.Visible = false;
            //    GridView2.Visible = false;
            //    GridView3.Visible = false;
            //    GridView4.Visible = true;
            //    GridView5.Visible = false;
            //    GridView4.DataSource = dt1;
            //    GridView4.DataBind();
            //    lblHeading4.Text = "Absolute Alcohol Production Vs Consumption For : " + district;
            //    //GridView5.HeaderRow.TableSection = TableRowSection.TableHeader;
            //    // btnprint4.Visible = true;
            //    btnback4.Visible = true;
            //    DWAbsoluteAlcoholChart.Visible = false;
            //    DWMolassesPurchaselist.Visible = false;
            //    DWEthanolList.Visible = false;
            //    DWAbsoluteAlcohollist.Visible = true;
            //    sugarmill.Visible = false;
            //    rdbUnitType.SelectedValue = "D";
            //    viewno = string.Empty;
            //}

            //if (viewno == "97" || viewno == "LOCKS CREATED AT SOURCE")
            //{
            //    query = "select user_name ,case when lag(closingbalance,1) over (order by user_name) isnull then openingbalance else lag(closingbalance,1) over (order by user_name)  end as OpeningBalance,  Purchase totalPurchase,Consumption as totalConsumption,closingbalance as closingbalance from exciseautomation.distillerywiseethanolproductionvssale where user_name='" + district + "'";
            //    using (NpgsqlConnection con = new NpgsqlConnection(conn))
            //    {
            //        con.Open();
            //        using (NpgsqlCommand command = new NpgsqlCommand())
            //        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, cn1))
            //        {
            //            // fill data table
            //            da.Fill(dt1);

            //        }

            //    }
            //    GridView1.Visible = false;
            //    GridView2.Visible = false;
            //    GridView3.Visible = true;
            //    GridView4.Visible = false;
            //    GridView5.Visible = false;
            //    GridView3.DataSource = dt1;
            //    GridView3.DataBind();
            //    lblHeading3.Text = "Ethanol Production Vs Sale For : " + district;
            //    //GridView5.HeaderRow.TableSection = TableRowSection.TableHeader;
            //    // btnprint3.Visible = true;
            //    btnback3.Visible = true;
            //    DWEthanolCharts.Visible = false;
            //    DWMolassesPurchaselist.Visible = false;
            //    DWEthanolList.Visible = true;
            //    DWAbsoluteAlcohollist.Visible = false;
            //    sugarmill.Visible = false;
            //    rdbUnitType.SelectedValue = "D";
            //    viewno = string.Empty;
            //}

            //else if (viewno == "1" || viewno == "LOCKS CLOSED AT DESTINATION")
            //{
            //    query = "select  a.openingbalancevalue,d.dailyproduction,to_char( d.entrydate, 'DD/MM/YYYY') as entrydate,f.issuedqty, sum(a.openingbalancevalue + d.dailyproduction - f.issuedqty) AS closingbalance  from  exciseautomation.openingbalance a left join exciseautomation.user_registration b on a.user_id=b.user_id  left join exciseautomation.dailymolassesproduction d on b.party_code=d.party_code left join exciseautomation.molassesissueregister f on d.party_code=f.party_code where b.user_name='" + district + "' GROUP BY a.openingbalancevalue,d.dailyproduction,f.issuedqty,d.entrydate   ";
            //    using (NpgsqlConnection con = new NpgsqlConnection(conn))
            //    {
            //        con.Open();
            //        using (NpgsqlCommand command = new NpgsqlCommand())
            //        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, cn1))
            //        {
            //            // fill data table
            //            da.Fill(dt1);
            //        }
            //    }
            //    GridView1.Visible = false;
            //    GridView2.Visible = true;
            //    //GridView3.Visible = false;
            //    //GridView4.Visible = false;
            //    //GridView5.Visible = false;
            //    GridView2.DataSource = dt1;
            //    GridView2.DataBind();
            //    lblHeading1.Text = "Molasses Production Vs Dispatch - " + district;
            //    // GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
            //    btnback2.Visible = true;
            //    // btnprint2.Visible = true;
            //    sugarMolassescharts.Visible = true;
            //    SugarMolasseslist.Visible = false;
            //    MolassesProduction.Visible = false;
            //    MolassesProductionList.Visible = true;
            //    distdiv.Visible = false;
            //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "Func()", true);
            //    viewno = string.Empty;
            //}
            // }
            //  UserDetails user = new UserDetails();
            // user = BL_UserDetails.CheckUser(Session["UserID"].ToString());

            //   if (Session["UserID"].ToString() == "Admin" || Session["UserID"].ToString() == "com" || Session["UserID"].ToString() == "hodyco")
            //  {

            //  RenderChartD2();
            string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["CASEMGMT"].ConnectionString;
            //  string connstring_EA = System.Configuration.ConfigurationManager.ConnectionStrings["CASEMGMT_PRO"].ConnectionString;
            NpgsqlConnection cn = new NpgsqlConnection(connstring);

            DashboardCharts(userid, username, user, connstring, cn);

        }
        #region Dashboard
        private void DashboardCharts(string userid, string username, UserDetails user, string connstring, NpgsqlConnection cn)
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection con = new NpgsqlConnection(connstring))
            {
                // Sugar Cane Purchase Vs Consumption
                con.Open();
                if (userid == "com" || userid == "hodyco")
                {
                    query = "SELECT* FROM exciseautomation.sugarcanepurchasevsconsumption";
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, cn))
                    {
                        // fill data table
                        da.Fill(dt);
                    }
                }
                else if (Session["party_type"].ToString() == "Sugar Mill")
                {
                    query = "SELECT* FROM exciseautomation.sugarcanepurchasevsconsumption where partycode='" + username + "'";
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, cn))
                    {
                        // fill data table
                        da.Fill(dt);
                    }
                }


                //  }
                string n = string.Empty;
                if (Session["party_type"].ToString() == "M & tP")
                {
                    n = "Raw Material Purchase Vs Consumption";
                }
                else
                {
                    n = "Sugar Cane Purchase Vs Consumption";
                }
              
                string s1 = string.Empty,
                 s2 = string.Empty,
                 s3 = string.Empty,
                 s4 = string.Empty,
                 s5 = string.Empty,
                 s6 = string.Empty,
                 s7 = string.Empty,
                 s8 = string.Empty,
                 s9 = string.Empty,
                 s10 = string.Empty,
                 s11 = string.Empty;
                string p1 = string.Empty,
              p2 = string.Empty,
              p3 = string.Empty,
              p4 = string.Empty,
              p5 = string.Empty,
              p6 = string.Empty,
              p7 = string.Empty,
              p8 = string.Empty,
              p9 = string.Empty,
              p10 = string.Empty,
              p11 = string.Empty;
                string u1 = string.Empty,
             u2 = string.Empty,
             u3 = string.Empty,
             u4 = string.Empty,
             u5 = string.Empty,
             u6 = string.Empty,
             u7 = string.Empty,
             u8 = string.Empty,
             u9 = string.Empty,
             u10 = string.Empty,
             u11 = string.Empty;
                string b1 = string.Empty,
              b2 = string.Empty,
              b3 = string.Empty,
              b4 = string.Empty,
              b5 = string.Empty,
              b6 = string.Empty,
              b7 = string.Empty,
              b8 = string.Empty,
              b9 = string.Empty,
              b10 = string.Empty,
              b11 = string.Empty;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        s1 = dt.Rows[i]["partycode"].ToString();
                        p1 = dt.Rows[i]["TotalPurchase"].ToString();
                        u1 = dt.Rows[i]["TotalConsumption"].ToString();
                        b1 = dt.Rows[i]["ClosingBalance"].ToString();

                    }

                    if (i == 1)
                    {
                        s2 = dt.Rows[i]["partycode"].ToString();
                        p2 = dt.Rows[i]["TotalPurchase"].ToString();
                        u2 = dt.Rows[i]["TotalConsumption"].ToString();
                        b2 = dt.Rows[i]["ClosingBalance"].ToString();

                    }
                    if (i == 2)
                    {
                        s3 = dt.Rows[i]["partycode"].ToString();
                        p3 = dt.Rows[i]["TotalPurchase"].ToString();
                        u3 = dt.Rows[i]["TotalConsumption"].ToString();
                        b3 = dt.Rows[i]["ClosingBalance"].ToString();

                    }
                    if (i == 3)
                    {
                        s4 = dt.Rows[i]["partycode"].ToString();
                        p4 = dt.Rows[i]["TotalPurchase"].ToString();
                        u4 = dt.Rows[i]["TotalConsumption"].ToString();
                        b4 = dt.Rows[i]["ClosingBalance"].ToString();

                    }
                    if (i == 4)
                    {
                        s5 = dt.Rows[i]["partycode"].ToString();
                        p5 = dt.Rows[i]["TotalPurchase"].ToString();
                        u5 = dt.Rows[i]["TotalConsumption"].ToString();
                        b5 = dt.Rows[i]["ClosingBalance"].ToString();
                    }
                    if (i == 5)
                    {
                        s6 = dt.Rows[i]["partycode"].ToString();
                        p6 = dt.Rows[i]["TotalPurchase"].ToString();
                        u6 = dt.Rows[i]["TotalConsumption"].ToString();
                        b6 = dt.Rows[i]["ClosingBalance"].ToString();
                    }
                    if (i == 6)
                    {
                        s7 = dt.Rows[i]["partycode"].ToString();
                        p7 = dt.Rows[i]["TotalPurchase"].ToString();
                        u7 = dt.Rows[i]["TotalConsumption"].ToString();
                        b7 = dt.Rows[i]["ClosingBalance"].ToString();
                    }

                    if (i == 7)
                    {
                        s8 = dt.Rows[i]["partycode"].ToString();
                        p8 = dt.Rows[i]["TotalPurchase"].ToString();
                        u8 = dt.Rows[i]["TotalConsumption"].ToString();
                        b8 = dt.Rows[i]["ClosingBalance"].ToString();
                    }
                    if (i == 8)
                    {
                        s9 = dt.Rows[i]["partycode"].ToString();
                        p9 = dt.Rows[i]["TotalPurchase"].ToString();
                        u9 = dt.Rows[i]["TotalConsumption"].ToString();
                        b9 = dt.Rows[i]["ClosingBalance"].ToString();
                    }
                    if (i == 9)
                    {
                        s10 = dt.Rows[i]["partycode"].ToString();
                        p10 = dt.Rows[i]["TotalPurchase"].ToString();
                        u10 = dt.Rows[i]["TotalConsumption"].ToString();
                        b10 = dt.Rows[i]["ClosingBalance"].ToString();
                    }
                    if (i == 10)
                    {
                        s11 = dt.Rows[i]["partycode"].ToString();
                        p11 = dt.Rows[i]["TotalPurchase"].ToString();
                        u11 = dt.Rows[i]["TotalConsumption"].ToString();
                        b11 = dt.Rows[i]["ClosingBalance"].ToString();
                    }

                }

                Dictionary<string, string> chartConfig = new Dictionary<string, string>();
                // Create the chart - mscolumn2d Chart with data from Data/Data.xml
                Chart sales = new Chart();

                // Setting chart id
                sales.SetChartParameter(Chart.ChartParameter.chartId, "myChart");

                // Setting chart type to mscolumn2d chart
                sales.SetChartParameter(Chart.ChartParameter.chartType, "mscolumn2d");

                // Setting chart width to 600px
                sales.SetChartParameter(Chart.ChartParameter.chartWidth, "950");

                // Setting chart height to 350px
                sales.SetChartParameter(Chart.ChartParameter.chartHeight, "400");

                // Setting chart data as JSON String (Uncomment below line
                //sales.SetData("{\n  \"chart\": {\n    \"caption\": \"Sugar Cane Purchase Vs Consumption\",\n    \"subcaption\": \" \",\n    \"xaxisname\": \"Sugar Mills\",\n    \"yaxisname\": \"Quintals\"," +
                //   "\n    \"formatnumberscale\": \"0\",\n    \"plottooltext\": \"<b>$dataValue</b> quintals of <b>$seriesName</b> in $label\",\n    \"theme\": \"fusion\",\n    \"drawcrossline\": \"1\"\n  }," +
                //   "\n  \"categories\": [\n    {\n      \"category\": [\n        {\n          \"label\": \"" + s1 + "\"\n        },\n        {\n          \"label\": \"" + s2 + "\"\n        },\n        {\n          \"label\": \"" + s3 + "\"\n        },\n        {\n          \"label\": \"" + s4 + "\"\n        },\n        {\n          \"label\": \"" + s5 + "\"\n        },\n        {\n          \"label\": \"" + s6 + "\"\n        },\n        {\n          \"label\": \"" + s7 + "\"\n        },\n        {\n          \"label\": \"" + s8 + "\"\n        },\n        {\n          \"label\": \"" + s9 + "\"\n        }," +
                //   "\n        {\n          \"label\": \"" + s10 + "\"\n        },\n        {\n          \"label\": \"" + s11 + "\"\n        }\n      ]\n    }\n  ],\n  \"dataset\": [\n    {\n      \"seriesname\": \"Opening Balance\",\n      " +
                //   "\"data\": [\n        {\n          \"value\": \"0\"\n        },\n        {\n          \"value\": \"0\"\n        },\n        {\n          \"value\": \"0\"\n        },\n       " +
                //   " {\n          \"value\": \"0\"\n        },{\n          \"value\": \"0\"\n        },{\n          \"value\": \"0\"\n        },{\n          \"value\": \"0\"\n        },{\n          \"value\": \"0\"\n        },{\n          \"value\": \"0\"\n        },{\n          \"value\": \"0\"\n        },{\n          \"value\": \"0\"\n        },{\n          \"value\": \"0\"\n        },{\n          \"value\": \"0\"\n        },{\n          \"value\": \"0\"\n        },\n        {\n          \"value\": \"0\"\n        }\n      ]\n    },\n    {\n      \"seriesname\": \"Total Purchase\",\n    " +
                //   "  \"data\": [\n        {\n          \"value\": \"" + p1 + "\"\n        },\n        {\n          \"value\": \"" + p2 + "\"\n        },\n        {\n          \"value\": \"" + p3 + "\"\n        },\n        {\n          \"value\": \"" + p4 + "\"\n        },\n        {\n          \"value\": \"" + p5 + "\"\n        },\n        {\n          \"value\": \"" + p6 + "\"\n        },\n        {\n          \"value\": \"" + p7 + "\"\n        },\n        {\n          \"value\": \"" + p8 + "\"\n        },\n        {\n          \"value\": \"" + p9 + "\"\n        },\n    " +
                //   "    {\n          \"value\": \"" + p10 + "\"\n        },\n        {\n          \"value\": \"" + p11 + "\"\n        }\n      ]\n    },\n    {\n      \"seriesname\": \"Total Comsumption\",\n   " +
                //   "   \"data\": [\n        {\n          \"value\": \"" + u1 + "\"\n        },\n        {\n          \"value\": \"" + u2 + "\"\n        },\n        {\n          \"value\": \"" + u3 + "\"\n        },\n        {\n          \"value\": \"" + u4 + "\"\n        },\n        {\n          \"value\": \"" + u5 + "\"\n        },\n        {\n          \"value\": \"" + u6 + "\"\n        },\n        {\n          \"value\": \"" + u7 + "\"\n        },\n        {\n          \"value\": \"" + u8 + "\"\n        },\n        {\n          \"value\": \"" + u9 + "\"\n        },\n     " +
                //   "   {\n          \"value\": \"" + u10 + "\"\n        },\n        {\n          \"value\": \"" + u11 + "\"\n        }\n      ]\n    },\n    {\n      \"seriesname\": \"Closing Balance\",\n   " +
                //   "   \"data\": [\n        {\n          \"value\": \"" + b1 + "\"\n        },\n        {\n          \"value\": \"" + b2 + "\"\n        },\n        {\n          \"value\": \"" + b3 + "\"\n        },\n        {\n          \"value\": \"" + b4 + "\"\n        },\n        {\n          \"value\": \"" + b5 + "\"\n        },\n        {\n          \"value\": \"" + b6 + "\"\n        },\n        {\n          \"value\": \"" + b7 + "\"\n        },\n        {\n          \"value\": \"" + b8 + "\"\n        },\n        {\n          \"value\": \"" + b9 + "\"\n        },\n     " +
                //   "   {\n          \"value\": \"" + b10 + "\"\n        },\n        {\n          \"value\": \"" + b11 + "\"\n        }\n      ]\n    }\n  ]\n}", Chart.DataFormat.json);


                string linkParam = "SCMDashBoard.aspx";
                // Setting chart data as JSON String (Uncomment below line
                sales.SetData("{\n  \"chart\": {\n   \"exportEnabled\":\"1\",\n   \"exportFileName\":\""+n+"\",\n    \"caption\": \"\",\n    \"subcaption\": \" \",\n    \"xaxisname\": \"\",\n    \"yaxisname\": \"Quintals\"," +
                   "\n    \"formatnumberscale\": \"0\",\n    \"plottooltext\": \"<b>$dataValue</b> quintals of <b>$seriesName</b>\",\n    \"theme\": \"fusion\",\n    \"drawcrossline\": \"1\"\n  }," +
                   "\n  \"categories\": [\n    {\n      \"category\": [\n        {\n          \"label\": \"" + s1 + "\"\n        },\n        {\n          \"label\": \"" + s2 + "\"\n        },\n        {\n          \"label\": \"" + s3 + "\"\n        },\n        {\n          \"label\": \"" + s4 + "\"\n        },\n        {\n          \"label\": \"" + s5 + "\"\n        },\n        {\n          \"label\": \"" + s6 + "\"\n        },\n        {\n          \"label\": \"" + s7 + "\"\n        },\n        {\n          \"label\": \"" + s8 + "\"\n        },\n        {\n          \"label\": \"" + s9 + "\"\n        }," +
                   "\n        {\n          \"label\": \"" + s10 + "\"\n        },\n        {\n          \"label\": \"" + s11 + "\"\n        }\n      ]\n    }\n  ],\n  \"dataset\": [\n    {\n      \"seriesname\": \"Opening Balance\",\n      " +
                   "\"data\": [\n        {\n          \"value\": \"0\"\n        },\n        {\n          \"value\": \"0\"\n        },\n        {\n          \"value\": \"0\"\n        },\n       " +
                   " {\n          \"value\": \"0\"\n        },{\n          \"value\": \"0\"\n        },{\n          \"value\": \"0\"\n        },{\n          \"value\": \"0\"\n        },{\n          \"value\": \"0\"\n        },{\n          \"value\": \"0\"\n        },{\n          \"value\": \"0\"\n        },{\n          \"value\": \"0\"\n        },{\n          \"value\": \"0\"\n        },{\n          \"value\": \"0\"\n        },{\n          \"value\": \"0\"\n        },\n        {\n          \"value\": \"0\"\n        }\n      ]\n    },\n    {\n      \"seriesname\": \"Total Purchase\",\n    " +
                   "  \"data\": [\n        {\n          \"value\": \"" + p1 + "\",\n \"link\": \"" + linkParam + "?label=" + s1 + "&viewno=0\"\n       },\n        {\n          \"value\": \"" + p2 + "\",\n \"link\": \"" + linkParam + "?label=" + s2 + "&viewno=0\"\n },\n        {\n          \"value\": \"" + p3 + "\",\n \"link\": \"" + linkParam + "?label=" + s3 + "&viewno=0\"\n       },\n        {\n          \"value\": \"" + p4 + "\",\n \"link\": \"" + linkParam + "?label=" + s4 + "&viewno=0\"\n        },\n        {\n          \"value\": \"" + p5 + "\",\n \"link\": \"" + linkParam + "?label=" + s5 + "&viewno=0\"\n       },\n        {\n          \"value\": \"" + p6 + "\",\n \"link\": \"" + linkParam + "?label=" + s6 + "&viewno=0\"\n       },\n        {\n          \"value\": \"" + p7 + "\",\n \"link\": \"" + linkParam + "?label=" + s7 + "&viewno=0\"\n       },\n        {\n          \"value\": \"" + p8 + "\",\n \"link\": \"" + linkParam + "?label=" + s8 + "&viewno=0\"\n       },\n        {\n          \"value\": \"" + p9 + "\",\n \"link\": \"" + linkParam + "?label=" + s9 + "&viewno=0\"\n       },\n    " +
                   "    {\n          \"value\": \"" + p10 + "\",\n \"link\": \"" + linkParam + "?label=" + s10 + "&viewno=0\"\n       },\n        {\n          \"value\": \"" + p11 + "\",\n \"link\": \"" + linkParam + "?label=" + s11 + "&viewno=0\"\n       }\n      ]\n    },\n    {\n      \"seriesname\": \"Total Comsumption\",\n   " +
                   "   \"data\": [\n        {\n          \"value\": \"" + u1 + "\",\n \"link\": \"" + linkParam + "?label=" + s1 + "&viewno=0\"\n       },\n        {\n          \"value\": \"" + u2 + "\",\n \"link\": \"" + linkParam + "?label=" + s2 + "&viewno=0\"\n       },\n        {\n          \"value\": \"" + u3 + "\",\n \"link\": \"" + linkParam + "?label=" + s3 + "&viewno=0\"\n       },\n        {\n          \"value\": \"" + u4 + "\",\n \"link\": \"" + linkParam + "?label=" + s4 + "&viewno=0\"\n       },\n        {\n          \"value\": \"" + u5 + "\",\n \"link\": \"" + linkParam + "?label=" + s5 + "&viewno=0\"\n       },\n        {\n          \"value\": \"" + u6 + "\",\n \"link\": \"" + linkParam + "?label=" + s6 + "&viewno=0\"\n       },\n        {\n          \"value\": \"" + u7 + "\",\n \"link\": \"" + linkParam + "?label=" + s7 + "&viewno=0\"\n       },\n        {\n          \"value\": \"" + u8 + "\",\n \"link\": \"" + linkParam + "?label=" + s8 + "&viewno=0\"\n       },\n        {\n          \"value\": \"" + u9 + "\",\n \"link\": \"" + linkParam + "?label=" + s9 + "&viewno=0\"\n       },\n     " +
                   "   {\n          \"value\": \"" + u10 + "\",\n \"link\": \"" + linkParam + "?label=" + s10 + "&viewno=0\"\n       },\n        {\n          \"value\": \"" + u11 + "\",\n \"link\": \"" + linkParam + "?label=" + s11 + "&viewno=0\"\n       }\n      ]\n    },\n    {\n      \"seriesname\": \"Closing Balance\",\n   " +
                   "   \"data\": [\n        {\n          \"value\": \"" + b1 + "\",\n \"link\": \"" + linkParam + "?label=" + s1 + "&viewno=0\"\n       },\n        {\n          \"value\": \"" + b2 + "\",\n \"link\": \"" + linkParam + "?label=" + s2 + "&viewno=0\"\n       },\n        {\n          \"value\": \"" + b3 + "\",\n \"link\": \"" + linkParam + "?label=" + s3 + "&viewno=0\"\n       },\n        {\n          \"value\": \"" + b4 + "\",\n \"link\": \"" + linkParam + "?label=" + s4 + "&viewno=0\"\n       },\n        {\n          \"value\": \"" + b5 + "\",\n \"link\": \"" + linkParam + "?label=" + s5 + "&viewno=0\"\n       },\n        {\n          \"value\": \"" + b6 + "\",\n \"link\": \"" + linkParam + "?label=" + s6 + "&viewno=0\"\n       },\n        {\n          \"value\": \"" + b7 + "\",\n \"link\": \"" + linkParam + "?label=" + s7 + "&viewno=0\"\n       },\n        {\n          \"value\": \"" + b8 + "\",\n \"link\": \"" + linkParam + "?label=" + s8 + "&viewno=0\"\n       },\n        {\n          \"value\": \"" + b9 + "\",\n \"link\": \"" + linkParam + "?label=" + s9 + "&viewno=0\"\n       },\n     " +
                   "   {\n          \"value\": \"" + b10 + "\",\n \"link\": \"" + linkParam + "?label=" + s10 + "&viewno=0\"\n       },\n        {\n          \"value\": \"" + b11 + "\",\n \"link\": \"" + linkParam + "?label=" + s11 + "&viewno=0\"\n       }\n      ]\n    }\n  ]\n}", Chart.DataFormat.json);

                Literal1.Text = sales.Render();


                DataTable dt2 = new DataTable();
                using (NpgsqlConnection con1 = new NpgsqlConnection(connstring))
                {
                    //Molasses Production Vs Dispatch
                    con1.Open();
                    if (userid == "com" || userid == "hodyco")
                    {
                        query = "SELECT * FROM exciseautomation.molassesproductionvsdispatch";
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, cn))
                        {
                            // fill data table
                            da.Fill(dt2);

                        }
                    }
                    else if (Session["party_type"].ToString() == "Sugar Mill")
                    {
                        query = "SELECT * FROM exciseautomation.molassesproductionvsdispatch where party_code='" + Session["party_code"].ToString() + "'";
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, cn))
                        {
                            // fill data table
                            da.Fill(dt2);

                        }
                    }


                }

                if (dt2.Rows.Count > 0)
                {
                    string ss1 = string.Empty,
                   ss2 = string.Empty,
                   ss3 = string.Empty,
                   ss4 = string.Empty,
                   ss5 = string.Empty,
                   ss6 = string.Empty,
                   ss7 = string.Empty,
                   ss8 = string.Empty,
                   ss9 = string.Empty,
                   ss10 = string.Empty,
                   ss11 = string.Empty;
                    string pp1 = string.Empty,
                 pp2 = string.Empty,
                 pp3 = string.Empty,
                 pp4 = string.Empty,
                 pp5 = string.Empty,
                 pp6 = string.Empty,
                 pp7 = string.Empty,
                 pp8 = string.Empty,
                 pp9 = string.Empty,
                 pp10 = string.Empty,
                 pp11 = string.Empty;
                    string uu1 = string.Empty,
                     uu2 = string.Empty,
                     uu3 = string.Empty,
                   uu4 = string.Empty,
                     uu5 = string.Empty,
                     uu6 = string.Empty,
                       uu7 = string.Empty,
                       uu8 = string.Empty,
                      uu9 = string.Empty,
                       uu10 = string.Empty,
                      uu11 = string.Empty;
                    string bb1 = string.Empty,
                  bb2 = string.Empty,
                  bb3 = string.Empty,
                  bb4 = string.Empty,
                  bb5 = string.Empty,
                  bb6 = string.Empty,
                  bb7 = string.Empty,
                  bb8 = string.Empty,
                  bb9 = string.Empty,
                  bb10 = string.Empty,
                  bb11 = string.Empty;
                    string o1 = string.Empty,
                 o2 = string.Empty,
                 o3 = string.Empty,
                 o4 = string.Empty,
                 o5 = string.Empty,
                 o6 = string.Empty,
                 o7 = string.Empty,
                 o8 = string.Empty,
                 o9 = string.Empty,
                 o10 = string.Empty,
                  o11 = string.Empty;
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            ss1 = dt2.Rows[i]["user_name"].ToString();
                            pp1 = dt2.Rows[i]["TotalProduction"].ToString();
                            uu1 = dt2.Rows[i]["TotalDispatch"].ToString();
                            bb1 = dt2.Rows[i]["ClosingBalance"].ToString();
                            o1 = dt2.Rows[i]["OpeningBalance"].ToString();
                        }
                        if (i == 1)
                        {
                            ss2 = dt2.Rows[i]["user_name"].ToString();
                            pp2 = dt2.Rows[i]["TotalProduction"].ToString();
                            uu2 = dt2.Rows[i]["TotalDispatch"].ToString();
                            bb2 = dt2.Rows[i]["ClosingBalance"].ToString();
                            o2 = dt2.Rows[i]["OpeningBalance"].ToString();
                        }
                        if (i == 2)
                        {
                            ss3 = dt2.Rows[i]["user_name"].ToString();
                            pp3 = dt2.Rows[i]["TotalProduction"].ToString();
                            uu3 = dt2.Rows[i]["TotalDispatch"].ToString();
                            bb3 = dt2.Rows[i]["ClosingBalance"].ToString();
                            o3 = dt2.Rows[i]["OpeningBalance"].ToString();
                        }
                        if (i == 3)
                        {
                            ss4 = dt2.Rows[i]["user_name"].ToString();
                            pp4 = dt2.Rows[i]["TotalProduction"].ToString();
                            uu4 = dt2.Rows[i]["TotalDispatch"].ToString();
                            bb4 = dt2.Rows[i]["ClosingBalance"].ToString();
                            o4 = dt2.Rows[i]["OpeningBalance"].ToString();
                        }
                        if (i == 4)
                        {
                            ss5 = dt2.Rows[i]["user_name"].ToString();
                            pp5 = dt2.Rows[i]["TotalProduction"].ToString();
                            uu5 = dt2.Rows[i]["TotalDispatch"].ToString();
                            bb5 = dt2.Rows[i]["ClosingBalance"].ToString();
                            o5 = dt2.Rows[i]["OpeningBalance"].ToString();
                        }
                        if (i == 5)
                        {
                            ss6 = dt2.Rows[i]["user_name"].ToString();
                            pp6 = dt2.Rows[i]["TotalProduction"].ToString();
                            uu6 = dt2.Rows[i]["TotalDispatch"].ToString();
                            bb6 = dt2.Rows[i]["ClosingBalance"].ToString();
                            o6 = dt2.Rows[i]["OpeningBalance"].ToString();
                        }
                        if (i == 6)
                        {
                            ss7 = dt2.Rows[i]["user_name"].ToString();
                            pp7 = dt2.Rows[i]["TotalProduction"].ToString();
                            uu7 = dt2.Rows[i]["TotalDispatch"].ToString();
                            bb7 = dt2.Rows[i]["ClosingBalance"].ToString();
                            o7 = dt2.Rows[i]["OpeningBalance"].ToString();
                        }
                        if (i == 7)
                        {
                            ss8 = dt2.Rows[i]["user_name"].ToString();
                            pp8 = dt2.Rows[i]["TotalProduction"].ToString();
                            uu8 = dt2.Rows[i]["TotalDispatch"].ToString();
                            bb8 = dt2.Rows[i]["ClosingBalance"].ToString();
                            o8 = dt2.Rows[i]["OpeningBalance"].ToString();
                        }
                        if (i == 8)
                        {
                            ss9 = dt2.Rows[i]["user_name"].ToString();
                            pp9 = dt2.Rows[i]["TotalProduction"].ToString();
                            uu9 = dt2.Rows[i]["TotalDispatch"].ToString();
                            bb9 = dt2.Rows[i]["ClosingBalance"].ToString();
                            o9 = dt2.Rows[i]["OpeningBalance"].ToString();
                        }
                        if (i == 9)
                        {
                            ss10 = dt2.Rows[i]["user_name"].ToString();
                            pp10 = dt2.Rows[i]["TotalProduction"].ToString();
                            uu10 = dt2.Rows[i]["TotalDispatch"].ToString();
                            bb10 = dt2.Rows[i]["ClosingBalance"].ToString();
                            o10 = dt2.Rows[i]["OpeningBalance"].ToString();
                        }
                        if (i == 10)
                        {
                            ss11 = dt2.Rows[i]["user_name"].ToString();
                            pp11 = dt2.Rows[i]["TotalProduction"].ToString();
                            uu11 = dt2.Rows[i]["TotalDispatch"].ToString();
                            bb11 = dt2.Rows[i]["ClosingBalance"].ToString();
                            o11 = dt2.Rows[i]["OpeningBalance"].ToString();
                        }
                    }

                    Dictionary<string, string> chartConfig1 = new Dictionary<string, string>();
                    // Create the chart - mscolumn2d Chart with data from Data/Data.xml
                    Chart sales1 = new Chart();

                    // Setting chart id
                    sales1.SetChartParameter(Chart.ChartParameter.chartId, "myChart1");

                    // Setting chart type to mscolumn2d chart
                    sales1.SetChartParameter(Chart.ChartParameter.chartType, "mscolumn2d");

                    // Setting chart width to 600px
                    sales1.SetChartParameter(Chart.ChartParameter.chartWidth, "950");

                    // Setting chart height to 350px
                    sales1.SetChartParameter(Chart.ChartParameter.chartHeight, "400");

                    string linkParam1 = "SCMDashBoard.aspx";
                    // Setting chart data as JSON String (Uncomment below line
                    sales1.SetData("{\n  \"chart\": { \n   \"exportEnabled\":\"1\",\n   \"exportFileName\":\"Molasses Production Vs Dispatch\",\n    \"caption\": \"\",\n    \"subcaption\": \" \",\n    \"xaxisname\": \"\",\n    \"yaxisname\": \"Quintals\"," +
                       "\n    \"formatnumberscale\": \"0\",\n    \"plottooltext\": \"<b>$dataValue</b> quintals of <b>$seriesName</b> \",\n    \"theme\": \"fusion\",\n    \"drawcrossline\": \"1\"\n  }," +
                       "\n  \"categories\": [\n    {\n      \"category\": [\n        {\n          \"label\": \"" + ss1 + "\"\n        },\n        {\n          \"label\": \"" + ss2 + "\"\n        },\n        {\n          \"label\": \"" + ss3 + "\"\n        },\n        {\n          \"label\": \"" + ss4 + "\"\n        },\n        {\n          \"label\": \"" + ss5 + "\"\n        },\n        {\n          \"label\": \"" + ss6 + "\"\n        },\n        {\n          \"label\": \"" + ss7 + "\"\n        },\n        {\n          \"label\": \"" + ss8 + "\"\n        },\n        {\n          \"label\": \"" + ss9 + "\"\n        }," +
                       "\n        {\n          \"label\": \"" + ss10 + "\"\n        },\n        {\n          \"label\": \"" + ss11 + "\"\n        }\n      ]\n    }\n  ],\n  \"dataset\": [\n    {\n      \"seriesname\": \"Opening Balance\",\n      " +
                       "\"data\": [\n        {\n          \"value\": \"" + o1 + "\",\n \"link\": \"" + linkParam + "?label=" + ss1 + "&viewno=1\"\n},\n        {\n          \"value\": \"" + o2 + "\",\n \"link\": \"" + linkParam + "?label=" + ss2 + "&viewno=1\"\n},\n        {\n          \"value\": \"" + o3 + "\",\n \"link\": \"" + linkParam + "?label=" + ss3 + "&viewno=1\"\n},\n       " +
                       " {\n          \"value\": \"" + o4 + "\",\n \"link\": \"" + linkParam + "?label=" + ss4 + "&viewno=1\"\n},{\n          \"value\": \"" + o5 + "\",\n \"link\": \"" + linkParam + "?label=" + ss5 + "&viewno=1\"\n},{\n          \"value\": \"" + o6 + "\",\n \"link\": \"" + linkParam + "?label=" + ss6 + "&viewno=1\"\n},{\n          \"value\": \"" + o7 + "\",\n \"link\": \"" + linkParam + "?label=" + ss7 + "&viewno=1\"\n},{\n          \"value\": \"" + o8 + "\",\n \"link\": \"" + linkParam + "?label=" + ss8 + "&viewno=1\"\n},{\n          \"value\": \"" + o9 + "\",\n \"link\": \"" + linkParam + "?label=" + ss9 + "&viewno=1\"\n},{\n          \"value\": \"" + o10 + "\",\n \"link\": \"" + linkParam + "?label=" + ss10 + "&viewno=1\"\n},\n        {\n          \"value\": \"" + o11 + "\",\n \"link\": \"" + linkParam + "?label=" + ss11 + "&viewno=1\"\n}\n      ]\n    },\n    {\n      \"seriesname\": \"Total Production\",\n    " +
                       "  \"data\": [\n        {\n          \"value\": \"" + pp1 + "\",\n \"link\": \"" + linkParam + "?label=" + ss1 + "&viewno=1\"\n              },\n        {\n          \"value\": \"" + pp2 + "\",\n \"link\": \"" + linkParam + "?label=" + ss2 + "&viewno=1\"\n              },\n        {\n          \"value\": \"" + pp3 + "\",\n \"link\": \"" + linkParam + "?label=" + ss3 + "&viewno=1\"\n                     },\n        {\n          \"value\": \"" + pp4 + "\",\n \"link\": \"" + linkParam + "?label=" + ss4 + "&viewno=1\"\n                      },\n        {\n          \"value\": \"" + pp5 + "\",\n \"link\": \"" + linkParam + "?label=" + ss5 + "&viewno=1\"\n                    },\n        {\n          \"value\": \"" + pp6 + "\",\n \"link\": \"" + linkParam + "?label=" + ss6 + "&viewno=1\"\n                     },\n        {\n          \"value\": \"" + pp7 + "\",\n \"link\": \"" + linkParam + "?label=" + ss7 + "&viewno=1\"\n                      },\n        {\n          \"value\": \"" + pp8 + "\",\n \"link\": \"" + linkParam + "?label=" + ss8 + "&viewno=1\"\n                     },\n        {\n          \"value\": \"" + pp9 + "\",\n \"link\": \"" + linkParam + "?label=" + ss9 + "&viewno=1\"\n                      },\n    " +
                       "    {\n          \"value\": \"" + pp10 + "\",\n \"link\": \"" + linkParam + "?label=" + ss10 + "&viewno=1\"\n},\n        {\n          \"value\": \"" + pp11 + "\",\n \"link\": \"" + linkParam + "?label=" + ss11 + "&viewno=1\"\n}\n      ]\n    },\n    {\n      \"seriesname\": \"Total Dispatch\",\n   " +
                       "   \"data\": [\n        {\n          \"value\": \"" + uu1 + "\",\n \"link\": \"" + linkParam + "?label=" + ss1 + "&viewno=1\"\n        },\n        {\n          \"value\": \"" + uu2 + "\",\n \"link\": \"" + linkParam + "?label=" + ss2 + "&viewno=1\"\n},\n        {\n          \"value\": \"" + uu3 + "\",\n \"link\": \"" + linkParam + "?label=" + ss3 + "&viewno=1\"\n        },\n        {\n          \"value\": \"" + uu4 + "\",\n \"link\": \"" + linkParam + "?label=" + ss4 + "&viewno=1\"\n},\n        {\n          \"value\": \"" + uu5 + "\",\n \"link\": \"" + linkParam + "?label=" + ss5 + "&viewno=1\"\n},\n        {\n          \"value\": \"" + uu6 + "\",\n \"link\": \"" + linkParam + "?label=" + ss6 + "&viewno=1\"\n},\n        {\n          \"value\": \"" + uu7 + "\",\n \"link\": \"" + linkParam + "?label=" + ss7 + "&viewno=1\"\n},\n        {\n          \"value\": \"" + uu8 + "\",\n \"link\": \"" + linkParam + "?label=" + ss8 + "&viewno=1\"\n},\n        {\n          \"value\": \"" + uu9 + "\",\n \"link\": \"" + linkParam + "?label=" + ss9 + "&viewno=1\"\n},\n     " +
                       "   {\n          \"value\": \"" + uu10 + "\",\n \"link\": \"" + linkParam + "?label=" + ss10 + "&viewno=1\"\n},\n        {\n          \"value\": \"" + uu11 + "\",\n \"link\": \"" + linkParam + "?label=" + ss11 + "&viewno=1\"\n}\n      ]\n    },\n    {\n      \"seriesname\": \"Closing Balance\",\n   " +
                       "   \"data\": [\n        {\n          \"value\": \"" + bb1 + "\",\n \"link\": \"" + linkParam + "?label=" + ss1 + "&viewno=1\"\n},\n        {\n          \"value\": \"" + bb2 + "\",\n \"link\": \"" + linkParam + "?label=" + ss2 + "&viewno=1\"\n},\n        {\n          \"value\": \"" + bb3 + "\",\n \"link\": \"" + linkParam + "?label=" + ss3 + "&viewno=1\"\n},\n        {\n          \"value\": \"" + bb4 + "\",\n \"link\": \"" + linkParam + "?label=" + ss4 + "&viewno=1\"\n},\n        {\n          \"value\": \"" + bb5 + "\",\n \"link\": \"" + linkParam + "?label=" + ss5 + "&viewno=1\"\n},\n        {\n          \"value\": \"" + bb6 + "\",\n \"link\": \"" + linkParam + "?label=" + ss6 + "&viewno=1\"\n},\n        {\n          \"value\": \"" + bb7 + "\",\n \"link\": \"" + linkParam + "?label=" + ss7 + "&viewno=1\"\n},\n        {\n          \"value\": \"" + bb8 + "\",\n \"link\": \"" + linkParam + "?label=" + ss8 + "&viewno=1\"\n},\n        {\n          \"value\": \"" + bb9 + "\",\n \"link\": \"" + linkParam + "?label=" + ss9 + "&viewno=1\"\n},\n     " +
                       "   {\n          \"value\": \"" + bb10 + "\",\n \"link\": \"" + linkParam + "?label=" + ss10 + "&viewno=1\"\n},\n        {\n          \"value\": \"" + bb11 + "\",\n \"link\": \"" + linkParam + "?label=" + ss11 + "&viewno=1\"\n}\n      ]\n    }\n  ]\n}", Chart.DataFormat.json);

                    Literal5.Text = sales1.Render();
                }

                //3.
                DataTable dt3 = new DataTable();
                using (NpgsqlConnection con2 = new NpgsqlConnection(connstring))
                {
                    //Distillery Wise Molasses Purchase Vs Consumption
                    con2.Open();
                    if (userid == "com" || userid == "hodyco")
                    {
                        query = "SELECT * FROM exciseautomation.distillerywisemolassespurchasevsconsumption";
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, cn))
                        {
                            // fill data table
                            da.Fill(dt3);

                        }
                    }
                    else if (Session["party_type"].ToString() == "Distillery Unit" || Session["party_type"].ToString() == "M & tP"|| Session["party_type"].ToString() == "ENA Distillery Unit")
                    {
                         query = "SELECT * FROM exciseautomation.distillerywisemolassespurchasevsconsumption where party_code='" + Session["party_code"].ToString() + "'";
                        //query = "select SUM(passqty) as received,u.user_name,u.user_id,(select SUM(total_qty_transferred) from exciseautomation.rawmaterial_fermenter rf where party_code = '" + Session["party_code"].ToString() + "') as used,"
                        //     + "(select sum(o.openingbalancevalue) from exciseautomation.openingbalance o inner join exciseautomation.view_checkuser ur on ur.user_id = o.user_id where ur.party_code = '" + Session["party_code"].ToString() + "' and CASE WHEN ur.party_type_code::text = 'MTP'::text THEN o.storagecontent::text ~~'E%'::text WHEN ur.party_type_code::text = 'ENA'::text  THEN o.storagecontent::text ~~'G%'::text ELSE o.storagecontent::text ~~'M%'::text END group by ur.party_code) as OB from exciseautomation.rawmaterial_receipt rr "
                        //     + "inner join exciseautomation.user_registration u on u.user_id = rr.user_id where rr.party_code = '" + Session["party_code"].ToString() + "' group by u.user_name,u.user_id";

                        using (NpgsqlCommand command = new NpgsqlCommand())
                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, cn))
                        {
                            // fill data table
                            da.Fill(dt3);

                        }
                    }
                }
                if (dt3.Rows.Count > 0)
                {
                    string a = string.Empty;
                    if (Session["party_type"].ToString() == "M & tP" || Session["party_type"].ToString() == "ENA Distillery Unit")
                    {
                        a = "Raw Material Purchase Vs Consumption";
                    }
                    else
                    {
                        a = "Distillery Wise Molasses Purchase Vs Consumption";
                    }
                   
                    string sss1 = string.Empty,
                             sss2 = string.Empty,
                             sss3 = string.Empty,
                             sss4 = string.Empty,
                             sss5 = string.Empty,
                             sss6 = string.Empty,
                             sss7 = string.Empty;
                    string ppp1 = string.Empty,
                      ppp2 = string.Empty,
                      ppp3 = string.Empty,
                      ppp4 = string.Empty,
                      ppp5 = string.Empty,
                      ppp6 = string.Empty,
                      ppp7 = string.Empty;
                    string uuu1 = string.Empty,
                          uuu2 = string.Empty,
                          uuu3 = string.Empty,
                          uuu4 = string.Empty,
                          uuu5 = string.Empty,
                          uuu6 = string.Empty,
                          uuu7 = string.Empty;
                    string bbb1 = string.Empty,
                     bbb2 = string.Empty,
                     bbb3 = string.Empty,
                     bbb4 = string.Empty,
                     bbb5 = string.Empty,
                     bbb6 = string.Empty,
                     bbb7 = string.Empty;
                    string oo1 = string.Empty,
                    oo2 = string.Empty,
                    oo3 = string.Empty,
                    oo4 = string.Empty,
                    oo5 = string.Empty,
                    oo6 = string.Empty,
                    oo7 = string.Empty;
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            sss1 = dt3.Rows[i]["user_name"].ToString();
                            ppp1 = dt3.Rows[i]["purchase"].ToString();
                            uuu1 = dt3.Rows[i]["consumption"].ToString();
                            bbb1 = dt3.Rows[i]["closingbalance"].ToString();
                            oo1 = dt3.Rows[i]["openingbalance"].ToString();
                        }
                        if (i == 1)
                        {
                            sss2 = dt3.Rows[i]["user_name"].ToString();
                            ppp2 = dt3.Rows[i]["purchase"].ToString();
                            uuu2 = dt3.Rows[i]["consumption"].ToString();
                            bbb2 = dt3.Rows[i]["closingbalance"].ToString();
                            oo2 = dt3.Rows[i]["openingbalance"].ToString();
                        }
                        if (i == 2)
                        {
                            sss3 = dt3.Rows[i]["user_name"].ToString();
                            ppp3 = dt3.Rows[i]["purchase"].ToString();
                            uuu3 = dt3.Rows[i]["consumption"].ToString();
                            bbb3 = dt3.Rows[i]["closingbalance"].ToString();
                            oo3 = dt3.Rows[i]["openingbalance"].ToString();
                        }
                        if (i == 3)
                        {
                            sss4 = dt3.Rows[i]["user_name"].ToString();
                            ppp4 = dt3.Rows[i]["purchase"].ToString();
                            uuu4 = dt3.Rows[i]["consumption"].ToString();
                            bbb4 = dt3.Rows[i]["closingbalance"].ToString();
                            oo4 = dt3.Rows[i]["openingbalance"].ToString();
                        }
                        if (i == 4)
                        {
                            sss5 = dt3.Rows[i]["user_name"].ToString();
                            ppp5 = dt3.Rows[i]["purchase"].ToString();
                            uuu5 = dt3.Rows[i]["consumption"].ToString();
                            bbb5 = dt3.Rows[i]["closingbalance"].ToString();
                            oo5 = dt3.Rows[i]["openingbalance"].ToString();
                        }
                        if (i == 5)
                        {
                            sss6 = dt3.Rows[i]["user_name"].ToString();
                            ppp6 = dt3.Rows[i]["purchase"].ToString();
                            uuu6 = dt3.Rows[i]["consumption"].ToString();
                            bbb6 = dt3.Rows[i]["closingbalance"].ToString();
                            oo6 = dt3.Rows[i]["openingbalance"].ToString();
                        }
                        if (i == 6)
                        {
                            sss7 = dt3.Rows[i]["user_name"].ToString();
                            ppp7 = dt3.Rows[i]["purchase"].ToString();
                            uuu7 = dt3.Rows[i]["consumption"].ToString();
                            bbb7 = dt3.Rows[i]["closingbalance"].ToString();
                            oo7 = dt3.Rows[i]["openingbalance"].ToString();
                        }
                    }





                    Dictionary<string, string> chartConfig3 = new Dictionary<string, string>();
                    // Create the chart - mscolumn2d Chart with data from Data/Data.xml
                    Chart sales3 = new Chart();


                    // Setting chart id
                    sales3.SetChartParameter(Chart.ChartParameter.chartId, "myChart3");

                    // Setting chart type to mscolumn2d chart
                    sales3.SetChartParameter(Chart.ChartParameter.chartType, "mscolumn2d");

                    // Setting chart width to 600px
                    sales3.SetChartParameter(Chart.ChartParameter.chartWidth, "950");

                    // Setting chart height to 350px
                    sales3.SetChartParameter(Chart.ChartParameter.chartHeight, "400");

                    string linkParam1 = "SCMDashBoard.aspx";
                    // Setting chart data as JSON String (Uncomment below line
                    sales3.SetData("{\n  \"chart\": {\n   \"exportEnabled\":\"1\",\n   \"exportFileName\":\""+a+"\",\n    \"caption\": \"\",\n    \"subcaption\": \" \",\n    \"xaxisname\": \"\",\n    \"yaxisname\": \"Quintals\"," +
                       "\n    \"formatnumberscale\": \"0\",\n    \"plottooltext\": \"<b>$dataValue</b> quintals of <b>$seriesName</b> \",\n    \"theme\": \"fusion\",\n    \"drawcrossline\": \"1\"\n  }," +
                       "\n  \"categories\": [\n    {\n      \"category\": [\n        {\n          \"label\": \"" + sss1 + "\"\n        },\n        {\n          \"label\": \"" + sss2 + "\"\n        },\n        {\n          \"label\": \"" + sss3 + "\"\n        },\n        {\n          \"label\": \"" + sss4 + "\"\n        },\n        {\n          \"label\": \"" + sss5 + "\"\n        },\n        {\n          \"label\": \"" + sss6 + "\"\n        }," +
                       "\n        {\n          \"label\": \"" + sss7 + "\"\n        }\n      ]\n    }\n  ],\n  \"dataset\": [\n    {\n      \"seriesname\": \"Opening Balance\",\n      " +
                       "\"data\": [\n        {\n          \"value\": \"" + oo1 + "\",\n \"link\": \"" + linkParam + "?label=" + sss1 + "&viewno=99\"\n},\n        {\n          \"value\": \"" + oo2 + "\",\n \"link\": \"" + linkParam + "?label=" + sss2 + "&viewno=99\"\n},\n        {\n          \"value\": \"" + oo3 + "\",\n \"link\": \"" + linkParam + "?label=" + sss3 + "&viewno=99\"\n},\n       " +
                       " {\n          \"value\": \"" + oo4 + "\",\n \"link\": \"" + linkParam + "?label=" + sss4 + "&viewno=99\"\n},{\n          \"value\": \"" + oo5 + "\",\n \"link\": \"" + linkParam + "?label=" + sss5 + "&viewno=99\"\n},{\n          \"value\": \"" + oo6 + "\",\n \"link\": \"" + linkParam + "?label=" + sss6 + "&viewno=99\"\n},{\n          \"value\": \"" + oo7 + "\",\n \"link\": \"" + linkParam + "?label=" + sss7 + "&viewno=99\"\n}\n      ]\n    },\n    {\n      \"seriesname\": \"Total Receipt\",\n    " +
                       "  \"data\": [\n        {\n          \"value\": \"" + ppp1 + "\",\n \"link\": \"" + linkParam + "?label=" + sss1 + "&viewno=99\"\n},\n        {\n          \"value\": \"" + ppp2 + "\",\n \"link\": \"" + linkParam + "?label=" + sss2 + "&viewno=99\"\n},\n        {\n          \"value\": \"" + ppp3 + "\",\n \"link\": \"" + linkParam + "?label=" + sss3 + "&viewno=99\"\n},\n        {\n          \"value\": \"" + ppp4 + "\",\n \"link\": \"" + linkParam + "?label=" + sss4 + "&viewno=99\"\n},\n        {\n          \"value\": \"" + ppp5 + "\",\n \"link\": \"" + linkParam + "?label=" + sss5 + "&viewno=99\"\n},\n        {\n          \"value\": \"" + ppp6 + "\",\n \"link\": \"" + linkParam + "?label=" + sss6 + "&viewno=99\"\n},\n    " +
                       "    {\n          \"value\": \"" + ppp7 + "\",\n \"link\": \"" + linkParam + "?label=" + sss7 + "&viewno=99\"\n}\n      ]\n    },\n    {\n      \"seriesname\": \"Total Consumption\",\n   " +
                       "   \"data\": [\n        {\n          \"value\": \"" + uuu1 + "\",\n \"link\": \"" + linkParam + "?label=" + sss1 + "&viewno=99\"\n},\n        {\n          \"value\": \"" + uuu2 + "\",\n \"link\": \"" + linkParam + "?label=" + sss2 + "&viewno=99\"\n},\n        {\n          \"value\": \"" + uuu3 + "\",\n \"link\": \"" + linkParam + "?label=" + sss3 + "&viewno=99\"\n},\n        {\n          \"value\": \"" + uuu4 + "\",\n \"link\": \"" + linkParam + "?label=" + sss4 + "&viewno=99\"\n},\n        {\n          \"value\": \"" + uuu5 + "\",\n \"link\": \"" + linkParam + "?label=" + sss5 + "&viewno=99\"\n},\n        {\n          \"value\": \"" + uuu6 + "\",\n \"link\": \"" + linkParam + "?label=" + sss6 + "&viewno=99\"\n},\n     " +
                       "   {\n          \"value\": \"" + uuu7 + "\",\n \"link\": \"" + linkParam + "?label=" + sss7 + "&viewno=99\"\n}\n      ]\n    },\n    {\n      \"seriesname\": \"Closing Balance\",\n   " +
                       "   \"data\": [\n        {\n          \"value\": \"" + bbb1 + "\",\n \"link\": \"" + linkParam + "?label=" + sss1 + "&viewno=99\"\n},\n        {\n          \"value\": \"" + bbb2 + "\",\n \"link\": \"" + linkParam + "?label=" + sss2 + "&viewno=99\"\n},\n        {\n          \"value\": \"" + bbb3 + "\",\n \"link\": \"" + linkParam + "?label=" + sss3 + "&viewno=99\"\n},\n        {\n          \"value\": \"" + bbb4 + "\",\n \"link\": \"" + linkParam + "?label=" + sss4 + "&viewno=99\"\n},\n        {\n          \"value\": \"" + bbb5 + "\",\n \"link\": \"" + linkParam + "?label=" + sss5 + "&viewno=99\"\n},\n        {\n          \"value\": \"" + bbb6 + "\",\n \"link\": \"" + linkParam + "?label=" + sss6 + "&viewno=99\"\n},\n     " +
                       "   {\n          \"value\": \"" + bbb7 + "\",\n \"link\": \"" + linkParam + "?label=" + sss7 + "&viewno=99\"\n}\n      ]\n    }\n  ]\n}", Chart.DataFormat.json);

                    Literal3.Text = sales3.Render();
                }


                //4.

                DataTable dt4 = new DataTable();
                using (NpgsqlConnection con3 = new NpgsqlConnection(connstring))
                {
                    //Distillery Wise Absolute Alcohol Production Vs Consumption
                    con3.Open();
                    if (userid == "com" || userid == "hodyco")
                    {
                        query = "SELECT * FROM exciseautomation.distillerywiseabsolutealcoholproductionvsconsumption";
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, cn))
                        {
                            // fill data table
                            da.Fill(dt4);

                        }

                    }
                    else if (Session["party_type"].ToString() == "Distillery Unit" || Session["party_type"].ToString() == "ENA Distillery Unit")
                    {
                        query = "SELECT * FROM exciseautomation.distillerywiseabsolutealcoholproductionvsconsumption where party_code='" + Session["party_code"].ToString() + "'";
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, cn))
                        {
                            // fill data table
                            da.Fill(dt4);

                        }

                    }

                }

                if (dt4.Rows.Count > 0)
                {
                    string a = string.Empty;
                    if (Session["party_type"].ToString() == "M & tP" || Session["party_type"].ToString() == "ENA Distillery Unit")
                    {
                        a = "Production Vs Consumption";
                    }
                    else
                    {
                        a = "Distillery Wise Absolute Alcohol Production Vs Consumption";
                    }

                    string sss_1 = string.Empty,
                        sss_2 = string.Empty,
                        sss_3 = string.Empty,
                        sss_4 = string.Empty,
                        sss_5 = string.Empty,
                        sss_6 = string.Empty,
                        sss_7 = string.Empty,
                        sss_8= string.Empty;
                    string ppp_1 = string.Empty,
                      ppp_2 = string.Empty,
                      ppp_3 = string.Empty,
                      ppp_4 = string.Empty,
                      ppp_5 = string.Empty,
                      ppp_6 = string.Empty,
                      ppp_7 = string.Empty,
                    ppp_8 = string.Empty;
                    string uuu_1 = string.Empty,
                          uuu_2 = string.Empty,
                          uuu_3 = string.Empty,
                          uuu_4 = string.Empty,
                          uuu_5 = string.Empty,
                          uuu_6 = string.Empty,
                          uuu_7 = string.Empty,
                    uuu_8 = string.Empty;
                    string bbb_1 = string.Empty,
                     bbb_2 = string.Empty,
                     bbb_3 = string.Empty,
                     bbb_4 = string.Empty,
                     bbb_5 = string.Empty,
                     bbb_6 = string.Empty,
                     bbb_7 = string.Empty,
                    bbb_8 = string.Empty;
                    string oo_1 = string.Empty,
                    oo_2 = string.Empty,
                    oo_3 = string.Empty,
                    oo_4 = string.Empty,
                    oo_5 = string.Empty,
                    oo_6 = string.Empty,
                    oo_7 = string.Empty,
                    oo_8 = string.Empty,
                    oo_9 = string.Empty;
                    for (int i = 0; i < dt4.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            sss_1 = dt4.Rows[i]["user_name"].ToString();
                            ppp_1 = dt4.Rows[i]["purchase"].ToString();
                            uuu_1 = dt4.Rows[i]["consumption"].ToString();
                            bbb_1 = dt4.Rows[i]["closingbalance"].ToString();
                            oo_1 = dt4.Rows[i]["openingbalance"].ToString();
                        }
                        if (i == 1)
                        {
                            sss_2 = dt4.Rows[i]["user_name"].ToString();
                            ppp_2 = dt4.Rows[i]["purchase"].ToString();
                            uuu_2 = dt4.Rows[i]["consumption"].ToString();
                            bbb_2 = dt4.Rows[i]["closingbalance"].ToString();
                            oo_2 = dt4.Rows[i]["openingbalance"].ToString();
                        }
                        if (i == 2)
                        {
                            sss_3 = dt4.Rows[i]["user_name"].ToString();
                            ppp_3 = dt4.Rows[i]["purchase"].ToString();
                            uuu_3 = dt4.Rows[i]["consumption"].ToString();
                            bbb_3 = dt4.Rows[i]["closingbalance"].ToString();
                            oo_3 = dt4.Rows[i]["openingbalance"].ToString();
                        }
                        if (i == 3)
                        {
                            sss_4 = dt4.Rows[i]["user_name"].ToString();
                            ppp_4 = dt4.Rows[i]["purchase"].ToString();
                            uuu_4 = dt4.Rows[i]["consumption"].ToString();
                            bbb_4 = dt4.Rows[i]["closingbalance"].ToString();
                            oo_4 = dt4.Rows[i]["openingbalance"].ToString(); ;
                        }
                        if (i == 4)
                        {
                            sss_5 = dt4.Rows[i]["user_name"].ToString();
                            ppp_5 = dt4.Rows[i]["purchase"].ToString();
                            uuu_5 = dt4.Rows[i]["consumption"].ToString();
                            bbb_5 = dt4.Rows[i]["closingbalance"].ToString();
                            oo_5 = dt4.Rows[i]["openingbalance"].ToString();
                        }
                        if (i == 5)
                        {
                            sss_6 = dt4.Rows[i]["user_name"].ToString();
                            ppp_6 = dt4.Rows[i]["purchase"].ToString();
                            uuu_6 = dt4.Rows[i]["consumption"].ToString();
                            bbb_6 = dt4.Rows[i]["closingbalance"].ToString();
                            oo_6 = dt4.Rows[i]["openingbalance"].ToString();
                        }
                        if (i == 6)
                        {
                            sss_7 = dt4.Rows[i]["user_name"].ToString();
                            ppp_7 = dt4.Rows[i]["purchase"].ToString();
                            uuu_7 = dt4.Rows[i]["consumption"].ToString();
                            bbb_7 = dt4.Rows[i]["closingbalance"].ToString();
                            oo_7 = dt4.Rows[i]["openingbalance"].ToString();
                        }
                        if (i == 7)
                        {
                            sss_8 = dt4.Rows[i]["user_name"].ToString();
                            ppp_8 = dt4.Rows[i]["purchase"].ToString();
                            uuu_8 = dt4.Rows[i]["consumption"].ToString();
                            bbb_8 = dt4.Rows[i]["closingbalance"].ToString();
                            oo_8 = dt4.Rows[i]["openingbalance"].ToString();
                        }


                    }



                    Dictionary<string, string> chartConfig4 = new Dictionary<string, string>();
                    // Create the chart - mscolumn2d Chart with data from Data/Data.xml
                    Chart sales4 = new Chart();

                    // Setting chart id
                    sales4.SetChartParameter(Chart.ChartParameter.chartId, "myChart4");

                    // Setting chart type to mscolumn2d chart
                    sales4.SetChartParameter(Chart.ChartParameter.chartType, "mscolumn2d");

                    // Setting chart width to 600px
                    sales4.SetChartParameter(Chart.ChartParameter.chartWidth, "950");

                    // Setting chart height to 350px
                    sales4.SetChartParameter(Chart.ChartParameter.chartHeight, "400");

                    string linkParam1 = "SCMDashBoard.aspx";
                    // Setting chart data as JSON String (Uncomment below line
                    sales4.SetData("{\n  \"chart\": {\n   \"exportEnabled\":\"1\",\n   \"exportFileName\":\""+a+"\",\n    \"caption\": \"\",\n    \"subcaption\": \" \",\n    \"xaxisname\": \"\",\n    \"yaxisname\": \"Bulk Litres\"," +
                       "\n    \"formatnumberscale\": \"0\",\n    \"plottooltext\": \"<b>$dataValue</b> Bulk Litres of <b>$seriesName</b> \",\n    \"theme\": \"fusion\",\n    \"drawcrossline\": \"1\"\n  }," +
                       "\n  \"categories\": [\n    {\n      \"category\": [\n        {\n          \"label\": \"" + sss_1 + "\"\n        },\n        {\n          \"label\": \"" + sss_2 + "\"\n        },\n        {\n          \"label\": \"" + sss_3 + "\"\n        },\n        {\n          \"label\": \"" + sss_4 + "\"\n        },\n        {\n          \"label\": \"" + sss_5 + "\"\n        },\n        {\n          \"label\": \"" + sss_6 + "\"\n        }," +
                       "\n        {\n          \"label\": \"" + sss_7 + "\"\n        },\n {\n          \"label\": \"" + sss_8 + "\"\n        }\n      ]\n    }\n  ],\n  \"dataset\": [\n    {\n      \"seriesname\": \"Opening Balance\",\n      " +
                       "\"data\": [\n        {\n          \"value\": \"" + oo_1 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_1 + "&viewno=98\"\n},\n        {\n          \"value\": \"" + oo_2 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_2 + "&viewno=98\"\n},\n        {\n          \"value\": \"" + oo_3 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_3 + "&viewno=98\"\n},\n       " +
                       " {\n          \"value\": \"" + oo_4 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_4 + "&viewno=98\"\n},{\n          \"value\": \"" + oo_5 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_5 + "&viewno=98\"\n},{\n          \"value\": \"" + oo_6 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_6 + "&viewno=98\"\n},{\n          \"value\": \"" + oo_7 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_7 + "&viewno=98\"\n},\n {\n          \"value\": \"" + oo_8 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_8 + "&viewno=98\"\n}\n      ]\n    },\n    {\n      \"seriesname\": \"Total Receipt\",\n    " +
                       "  \"data\": [\n        {\n          \"value\": \"" + ppp_1 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_1 + "&viewno=98\"\n},\n        {\n          \"value\": \"" + ppp_2 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_2 + "&viewno=98\"\n},\n        {\n          \"value\": \"" + ppp_3 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_3 + "&viewno=98\"\n},\n        {\n          \"value\": \"" + ppp_4 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_4 + "&viewno=98\"\n},\n        {\n          \"value\": \"" + ppp_5 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_5 + "&viewno=98\"\n},\n        {\n          \"value\": \"" + ppp_6 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_6 + "&viewno=98\"\n},\n    " +
                       "    {\n          \"value\": \"" + ppp_7 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_7 + "&viewno=98\"\n}, \n {\n          \"value\": \"" + ppp_8 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_8 + "&viewno=98\"\n}\n      ]\n    },\n    {\n      \"seriesname\": \"Total Consumption\",\n   " +
                       "   \"data\": [\n        {\n          \"value\": \"" + uuu_1 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_1 + "&viewno=98\"\n},\n        {\n          \"value\": \"" + uuu_2 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_2 + "&viewno=98\"\n},\n        {\n          \"value\": \"" + uuu_3 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_3 + "&viewno=98\"\n},\n        {\n          \"value\": \"" + uuu_4 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_4 + "&viewno=98\"\n},\n        {\n          \"value\": \"" + uuu_5 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_5 + "&viewno=98\"\n},\n        {\n          \"value\": \"" + uuu_6 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_6 + "&viewno=98\"\n},\n     " +
                       "   {\n          \"value\": \"" + uuu_7 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_7 + "&viewno=98\"\n},\n {\n          \"value\": \"" + uuu_8 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_8 + "&viewno=98\"\n}\n      ]\n    },\n    {\n      \"seriesname\": \"Closing Balance\",\n   " +
                       "   \"data\": [\n        {\n          \"value\": \"" + bbb_1 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_1 + "&viewno=98\"\n},\n        {\n          \"value\": \"" + bbb_2 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_2 + "&viewno=98\"\n},\n        {\n          \"value\": \"" + bbb_3 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_3 + "&viewno=98\"\n},\n        {\n          \"value\": \"" + bbb_4 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_4 + "&viewno=98\"\n},\n        {\n          \"value\": \"" + bbb_5 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_5 + "&viewno=98\"\n},\n        {\n          \"value\": \"" + bbb_6 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_6 + "&viewno=98\"\n},\n     " +
                       "   {\n          \"value\": \"" + bbb_7 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_7 + "&viewno=98\"\n},\n {\n          \"value\": \"" + bbb_8 + "\",\n \"link\": \"" + linkParam + "?label=" + sss_8 + "&viewno=98\"\n}\n      ]\n    }\n  ]\n}", Chart.DataFormat.json);

                    Literal2.Text = sales4.Render();
                }
                // 5.

                DataTable dt5 = new DataTable();
                using (NpgsqlConnection con4 = new NpgsqlConnection(connstring))
                {
                    // Distillery Wise Ethanol Production Vs Sale
                    con4.Open();
                    if (userid == "com" || userid == "hodyco")
                    {
                        query = "SELECT * FROM exciseautomation.distillerywiseethanolproductionvssale";
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, cn))
                        {
                            // fill data table
                            da.Fill(dt5);

                        }
                    }
                    else if (Session["party_type"].ToString() == "Distillery Unit" || Session["party_type"].ToString() == "ENA Distillery Unit")
                    {
                        query = "SELECT * FROM exciseautomation.distillerywiseethanolproductionvssale  where party_code='" + Session["party_code"].ToString() + "'";
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, cn))
                        {
                            // fill data table
                            da.Fill(dt5);

                        }
                    }


                }
                if (dt5.Rows.Count > 0)
                {
                    string a = string.Empty;
                    if (Session["party_type"].ToString() == "M & tP" || Session["party_type"].ToString() == "ENA Distillery Unit")
                    {
                        a = "Production Vs Sale";
                    }
                    else
                    {
                        a = "Distillery Wise Ethanol Production Vs Sale";
                    }
                    string sss__1 = string.Empty,
                      sss__2 = string.Empty,
                      sss__3 = string.Empty,
                      sss__4 = string.Empty,
                      sss__5 = string.Empty,
                      sss__6 = string.Empty,
                      sss__7 = string.Empty,
                      sss__8 = string.Empty,
                      sss__9 = string.Empty,
                      sss__10 = string.Empty,
                      sss__11 = string.Empty;
                    string ppp__1 = string.Empty,
                      ppp__2 = string.Empty,
                      ppp__3 = string.Empty,
                      ppp__4 = string.Empty,
                      ppp__5 = string.Empty,
                      ppp__6 = string.Empty,
                      ppp__7 = string.Empty,
                      ppp__8 = string.Empty,
                      ppp__9 = string.Empty,
                      ppp__10 = string.Empty,
                      ppp__11 = string.Empty;
                    string uuu__1 = string.Empty,
                          uuu__2 = string.Empty,
                          uuu__3 = string.Empty,
                          uuu__4 = string.Empty,
                          uuu__5 = string.Empty,
                          uuu__6 = string.Empty,
                          uuu__7 = string.Empty,
                          uuu__8 = string.Empty,
                          uuu__9 = string.Empty,
                          uuu__10 = string.Empty,
                          uuu__11 = string.Empty,
                          uuu__12 = string.Empty;
                    string bbb__1 = string.Empty,
                     bbb__2 = string.Empty,
                     bbb__3 = string.Empty,
                     bbb__4 = string.Empty,
                     bbb__5 = string.Empty,
                     bbb__6 = string.Empty,
                     bbb__7 = string.Empty,
                     bbb__8 = string.Empty,
                     bbb__9 = string.Empty,
                     bbb__10 = string.Empty,
                     bbb__11 = string.Empty;
                    string oo__1 = string.Empty,
                    oo__2 = string.Empty,
                    oo__3 = string.Empty,
                    oo__4 = string.Empty,
                    oo__5 = string.Empty,
                    oo__6 = string.Empty,
                    oo__7 = string.Empty,
                    oo__8 = string.Empty,
                    oo__9 = string.Empty,
                    oo__10 = string.Empty,
                    oo__11 = string.Empty;

                    for (int i = 0; i < dt5.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            sss__1 = dt5.Rows[i]["user_name"].ToString();
                            ppp__1 = dt5.Rows[i]["purchase"].ToString();
                            uuu__1 = dt5.Rows[i]["consumption"].ToString();
                            bbb__1 = dt5.Rows[i]["closingbalance"].ToString();
                            oo__1 = dt5.Rows[i]["openingbalance"].ToString();
                        }
                        if (i == 1)
                        {
                            sss__2 = dt5.Rows[i]["user_name"].ToString();
                            ppp__2 = dt5.Rows[i]["purchase"].ToString();
                            uuu__2 = dt5.Rows[i]["consumption"].ToString();
                            bbb__2 = dt5.Rows[i]["closingbalance"].ToString();
                            oo__2 = dt5.Rows[i]["openingbalance"].ToString();
                        }
                        if (i == 2)
                        {
                            sss__3 = dt5.Rows[i]["user_name"].ToString();
                            ppp__3 = dt5.Rows[i]["purchase"].ToString();
                            uuu__3 = dt5.Rows[i]["consumption"].ToString();
                            bbb__3 = dt5.Rows[i]["closingbalance"].ToString();
                            oo__3 = dt5.Rows[i]["openingbalance"].ToString();
                        }
                        if (i == 3)
                        {
                            sss__4 = dt5.Rows[i]["user_name"].ToString();
                            ppp__4 = dt5.Rows[i]["purchase"].ToString();
                            uuu__4 = dt5.Rows[i]["consumption"].ToString();
                            bbb__4 = dt5.Rows[i]["closingbalance"].ToString();
                            oo__4 = dt5.Rows[i]["openingbalance"].ToString();
                        }
                        if (i == 4)
                        {
                            sss__5 = dt5.Rows[i]["user_name"].ToString();
                            ppp__5 = dt5.Rows[i]["purchase"].ToString();
                            uuu__5 = dt5.Rows[i]["consumption"].ToString();
                            bbb__5 = dt5.Rows[i]["closingbalance"].ToString();
                            oo__5 = dt5.Rows[i]["openingbalance"].ToString();
                        }
                        if (i == 5)
                        {
                            sss__6 = dt5.Rows[i]["user_name"].ToString();
                            ppp__6 = dt5.Rows[i]["purchase"].ToString();
                            uuu__6 = dt5.Rows[i]["consumption"].ToString();
                            bbb__6 = dt5.Rows[i]["closingbalance"].ToString();
                            oo__6 = dt5.Rows[i]["openingbalance"].ToString();
                        }
                        if (i == 6)
                        {
                            sss__7 = dt5.Rows[i]["user_name"].ToString();
                            ppp__7 = dt5.Rows[i]["purchase"].ToString();
                            uuu__7 = dt5.Rows[i]["consumption"].ToString();
                            bbb__7 = dt5.Rows[i]["closingbalance"].ToString();
                            oo__7 = dt5.Rows[i]["openingbalance"].ToString();
                        }
                        if (i == 7)
                        {
                            sss__8 = dt5.Rows[i]["user_name"].ToString();
                            ppp__8 = dt5.Rows[i]["purchase"].ToString();
                            uuu__8 = dt5.Rows[i]["consumption"].ToString();
                            bbb__8 = dt5.Rows[i]["closingbalance"].ToString();
                            oo__8 = dt5.Rows[i]["openingbalance"].ToString();
                        }
                        if (i == 8)
                        {
                            sss__9 = dt5.Rows[i]["user_name"].ToString();
                            ppp__9 = dt5.Rows[i]["purchase"].ToString();
                            uuu__9 = dt5.Rows[i]["consumption"].ToString();
                            bbb__9 = dt5.Rows[i]["closingbalance"].ToString();
                            oo__9 = dt5.Rows[i]["openingbalance"].ToString();
                        }
                        if (i == 9)
                        {
                            sss__10 = dt5.Rows[i]["user_name"].ToString();
                            ppp__10= dt5.Rows[i]["purchase"].ToString();
                            uuu__10 = dt5.Rows[i]["consumption"].ToString();
                            bbb__10 = dt5.Rows[i]["closingbalance"].ToString();
                            oo__10 = dt5.Rows[i]["openingbalance"].ToString();
                        }
                        if (i == 10)
                        {
                            sss__11 = dt5.Rows[i]["user_name"].ToString();
                            ppp__11 = dt5.Rows[i]["purchase"].ToString();
                            uuu__11 = dt5.Rows[i]["consumption"].ToString();
                            bbb__11 = dt5.Rows[i]["closingbalance"].ToString();
                            oo__11 = dt5.Rows[i]["openingbalance"].ToString();
                        }

                    }

                    Dictionary<string, string> chartConfig5 = new Dictionary<string, string>();
                    // Create the chart - mscolumn2d Chart with data from Data/Data.xml
                    Chart sales5 = new Chart();

                    // Setting chart id
                    sales5.SetChartParameter(Chart.ChartParameter.chartId, "myChart5");

                    // Setting chart type to mscolumn2d chart
                    sales5.SetChartParameter(Chart.ChartParameter.chartType, "mscolumn2d");

                    // Setting chart width to 600px
                    sales5.SetChartParameter(Chart.ChartParameter.chartWidth, "950");

                    // Setting chart height to 350px
                    sales5.SetChartParameter(Chart.ChartParameter.chartHeight, "400");

                    string linkParam1 = "SCMDashBoard.aspx";
                    // Setting chart data as JSON String (Uncomment below line
                    sales5.SetData("{\n  \"chart\": {\n   \"exportEnabled\":\"1\",\n   \"exportFileName\":\""+a+"\",\n    \"caption\": \"\",\n    \"subcaption\": \" \",\n    \"xaxisname\": \"\",\n    \"yaxisname\": \"Bulk Litres\"," +
                       "\n    \"formatnumberscale\": \"0\",\n    \"plottooltext\": \"<b>$dataValue</b> Bulk Litres of <b>$seriesName</b> \",\n    \"theme\": \"fusion\",\n    \"drawcrossline\": \"1\"\n  }," +
                       "\n  \"categories\": [\n    {\n      \"category\": [\n        {\n          \"label\": \"" + sss__1 + "\"\n        },\n        {\n          \"label\": \"" + sss__2 + "\"\n        },\n        {\n          \"label\": \"" + sss__3 + "\"\n        },\n        {\n          \"label\": \"" + sss__4 + "\"\n        },\n        {\n          \"label\": \"" + sss__5 + "\"\n        },\n        {\n          \"label\": \"" + sss__6 + "\"\n        }," +
                       "\n        {\n          \"label\": \"" + sss__7 + "\"\n        },\n  {\n          \"label\": \"" + sss__8 + "\"\n        },\n  {\n          \"label\": \"" + sss__9 + "\"\n        } ,\n  {\n          \"label\": \"" + sss__10 + "\"\n        },\n  {\n          \"label\": \"" + sss__11 + "\"\n        }\n      ]\n    }\n  ],\n  \"dataset\": [\n    {\n      \"seriesname\": \"Opening Balance\",\n      " +
                       "\"data\": [\n        {\n          \"value\": \"" + oo__1 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__1 + "&viewno=97\"\n},\n        {\n          \"value\": \"" + oo__2 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__2 + "&viewno=97\"\n},\n        {\n          \"value\": \"" + oo__3 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__3 + "&viewno=97\"\n},\n       " +
                       " {\n          \"value\": \"" + oo__4 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__4 + "&viewno=97\"\n},{\n          \"value\": \"" + oo__5 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__5 + "&viewno=97\"\n},{\n          \"value\": \"" + oo__6 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__6 + "&viewno=97\"\n},{\n          \"value\": \"" + oo__7 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__7 + "&viewno=97\"\n},{\n          \"value\": \"" + oo__8 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__8 + "&viewno=97\"\n},{\n          \"value\": \"" + oo__9 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__9 + "&viewno=97\"\n},{\n          \"value\": \"" + oo__10 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__10 + "&viewno=97\"\n},{\n          \"value\": \"" + oo__11 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__11 + "&viewno=97\"\n}\n      ]\n    },\n    {\n      \"seriesname\": \"Total Receipt\",\n    " +
                       "  \"data\": [\n        {\n          \"value\": \"" + ppp__1 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__1 + "&viewno=97\"\n},\n        {\n          \"value\": \"" + ppp__2 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__2 + "&viewno=97\"\n},\n        {\n          \"value\": \"" + ppp__3 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__3 + "&viewno=97\"\n},\n        {\n          \"value\": \"" + ppp__4 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__4 + "&viewno=97\"\n},\n        {\n          \"value\": \"" + ppp__5 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__5 + "&viewno=97\"\n},\n        {\n          \"value\": \"" + ppp__6 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__6 + "&viewno=97\"\n},\n    " +
                       "    {\n          \"value\": \"" + ppp__7 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__7 + "&viewno=97\"\n},\n{\n          \"value\": \"" + ppp__8 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__8 + "&viewno=97\"\n},\n{\n          \"value\": \"" + ppp__9 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__9 + "&viewno=97\"\n},\n{\n          \"value\": \"" + ppp__10 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__10 + "&viewno=97\"\n},\n{\n          \"value\": \"" + ppp__11 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__11 + "&viewno=97\"\n}\n      ]\n    },\n    {\n      \"seriesname\": \"Total Consumption\",\n   " +
                       "   \"data\": [\n        {\n          \"value\": \"" + uuu__1 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__1 + "&viewno=97\"\n},\n        {\n          \"value\": \"" + uuu__2 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__2 + "&viewno=97\"\n},\n        {\n          \"value\": \"" + uuu__3 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__3 + "&viewno=97\"\n},\n        {\n          \"value\": \"" + uuu__4 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__4 + "&viewno=97\"\n},\n        {\n          \"value\": \"" + uuu__5 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__5 + "&viewno=97\"\n},\n        {\n          \"value\": \"" + uuu__6 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__6 + "&viewno=97\"\n},\n     " +
                       "   {\n          \"value\": \"" + uuu__7 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__7 + "&viewno=97\"\n},\n{\n          \"value\": \"" + uuu__8 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__8 + "&viewno=97\"\n},\n{\n          \"value\": \"" + uuu__9 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__9 + "&viewno=97\"\n},\n{\n          \"value\": \"" + uuu__10 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__10 + "&viewno=97\"\n},\n{\n          \"value\": \"" + uuu__11 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__11 + "&viewno=97\"\n}\n      ]\n    },\n    {\n      \"seriesname\": \"Closing Balance\",\n   " +
                       "   \"data\": [\n        {\n          \"value\": \"" + bbb__1 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__1 + "&viewno=97\"\n},\n        {\n          \"value\": \"" + bbb__2 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__2 + "&viewno=97\"\n},\n        {\n          \"value\": \"" + bbb__3 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__3 + "&viewno=97\"\n},\n        {\n          \"value\": \"" + bbb__4 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__4 + "&viewno=97\"\n},\n        {\n          \"value\": \"" + bbb__5 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__5 + "&viewno=97\"\n},\n        {\n          \"value\": \"" + bbb__6 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__6 + "&viewno=97\"\n},\n     " +
                       "   {\n          \"value\": \"" + bbb__7 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__7 + "&viewno=97\"\n},\n{\n          \"value\": \"" + bbb__8 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__8 + "&viewno=97\"\n},\n{\n          \"value\": \"" + bbb__9 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__9 + "&viewno=97\"\n},\n{\n          \"value\": \"" + bbb__10 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__10 + "&viewno=97\"\n},\n{\n          \"value\": \"" + bbb__11 + "\",\n \"link\": \"" + linkParam + "?label=" + sss__11+ "&viewno=97\"\n}\n      ]\n    }\n  ]\n}", Chart.DataFormat.json);

                    Literal4.Text = sales5.Render();
                }
            }
        }
        #endregion

        #region Rendercharts
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
        #endregion

        #region Radio
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
        #endregion

        #region Button_Events
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
        #endregion
        protected void btnEA_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainPage");
        }
        protected void btnSCM_Click(object sender, EventArgs e)
        {
            Response.Redirect("SCMDashBoard.aspx");
        }
    }
}