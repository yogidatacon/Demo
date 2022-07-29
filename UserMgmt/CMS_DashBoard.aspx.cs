using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FusionCharts.Charts;
using System.Text;
using Npgsql;
using System.Data;
 

namespace UserMgmt
{
    public partial class CMS_DashBoard : System.Web.UI.Page
    {
        static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["CASEMGMT"].ConnectionString;
        NpgsqlConnection conn = new NpgsqlConnection(connstring);
        class ColorRange
        {
            public double Min
            {
                get;
                set;
            }
            public double Max
            {
                get;
                set;
            }
            public string ColorCode
            {
                get;
                set;
            }

            public ColorRange(double min, double max, string code)
            {
                Min = min;
                Max = max;
                ColorCode = code;
            }
        }

        class CountryData
        {
            public string ID
            {
                get;
                set;
            }
            public double Value
            {
                get;
                set;
            }
            public int ShowLabel
            {
                get;
                set;
            }

            public string DistrictName
            {
                get;
                set;
            }

            public CountryData(string id, double value, int showLabel, string districtName)
            {
                ID = id;
                Value = value;
                ShowLabel = showLabel;
                DistrictName = districtName;



            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {

            //  
            if (!IsPostBack)
            {
                rdbUnitType.SelectedValue = "C";
                Chart chart1 = new Chart("doughnut2d", "first_chart", "680", "500", "jsonurl", "CMS_DashBoard/ComplaintStatus.ashx");
                Chart chart2 = new Chart("doughnut2d", "second_chart", "680", "500", "jsonurl", "CMS_DashBoard/ComplaintStatusDistrict.ashx");
                Chart chart3 = new Chart("doughnut2d", "third_chart", "680", "500", "jsonurl", "CMS_DashBoard/DivisionWiseOverallSeizureCount.ashx");
                Chart chart4 = new Chart("doughnut2d", "fourth_chart", "680", "500", "jsonurl", "CMS_DashBoard/DivisionWiseTodaySeizureCount.ashx");
                Chart chart5 = new Chart("column2d", "fifth_chart", "1200", "500", "jsonurl", "CMS_DashBoard/DistrictWiseOverallSeizureCount.ashx");
                Chart chart6 = new Chart("column2d", "sixth_chart", "1200", "500", "jsonurl", "CMS_DashBoard/DistrictWiseTodaySeizureCount.ashx");
                Chart chart7 = new Chart("column2d", "seventh_chart", "290", "300", "jsonurl", "CMS_DashBoard/SiezedVehicleStatus.ashx");
                Chart chart8 = new Chart("column2d", "eigth_chart", "290", "300", "jsonurl", "CMS_DashBoard/ApparatusSeizedCount.ashx");
                Chart chart9 = new Chart("column2d", "nineth_chart", "290", "300", "jsonurl", "CMS_DashBoard/PropertySeizedCount.ashx");
                Chart chart10 = new Chart("column2d", "tenth_chart", "290", "300", "jsonurl", "CMS_DashBoard/AccussedStatusCount.ashx");
                Chart chart11 = new Chart("column2d", "eleventh_chart", "1200", "500", "jsonurl", "CMS_DashBoard/ExciseArticleSeizedCount.ashx");
                Chart chart12 = new Chart("column2d", "twelth_chart", "1200", "500", "jsonurl", "CMS_DashBoard/OneWeekSeizure.ashx");

                Literal1.Text = chart1.Render();
                Literal2.Text = chart2.Render();
                L3.Text = chart3.Render();
                L4.Text = chart4.Render();
                L5.Text = chart5.Render();
                L6.Text = chart6.Render();
                L7.Text = chart7.Render();
                L8.Text = chart8.Render();
                L9.Text = chart9.Render();
                L10.Text = chart10.Render();
                L11.Text = chart11.Render();
                Literal3.Text = chart12.Render();

                #region 1.HeatMapChart
                //1.Heat Map Chart

                // store color code for different range
                List<ColorRange> color = new List<ColorRange>();
                color.Add(new ColorRange(0, 500, "#48C9B0")); ;
                color.Add(new ColorRange(501, 5000, "#E74C3C")); ;
                color.Add(new ColorRange(5001, 25000, "#E74C3C")); ;

                Dictionary<string, string> chartConfig = new Dictionary<string, string>();
                chartConfig.Add("animation", "0");
                chartConfig.Add("usehovercolor", "1");
                chartConfig.Add("showlegend", "0");
                chartConfig.Add("legendposition", "RIGHT");
                chartConfig.Add("legendbordercolor", "ffffff");
                chartConfig.Add("legendallowdrag", "0");
                chartConfig.Add("legendshadow", "0");
                chartConfig.Add("caption", "District Wise Seizure Map");
                chartConfig.Add("legendborderalpha", "0");
                chartConfig.Add("interactiveLegend", "0");
                chartConfig.Add("hovercolor", "CCCCCC");
                chartConfig.Add("theme", "fusion");

                DataTable dt = new DataTable();

                string drillLevel = "0";

                using (NpgsqlConnection con = new NpgsqlConnection(connstring))
                {
                    con.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter("SELECT d.districtmapid as ID,count(seizureno) as Value,'1' as ShowLabel,d.district_name FROM exciseautomation.seizure_basicinfo   s  "
                        + " inner join exciseautomation.district_master d on d.district_code=s.district_code  group by d.districtmapid,d.district_name order by count(seizureno) desc", con))
                    {
                        // fill data table
                        da.Fill(dt);

                    }
                    con.Close();


                }

                List<CountryData> countries = new List<CountryData>();
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    countries.Add(new CountryData(dt.Rows[i]["ID"].ToString(), Convert.ToDouble(dt.Rows[i]["Value"].ToString()), Convert.ToInt16(dt.Rows[i]["ShowLabel"].ToString()), dt.Rows[i]["district_name"].ToString()));
                }

                // json data to use as chart data source
                StringBuilder jsonData = new StringBuilder();
                //build chart config object
                jsonData.Append("{'chart':{");
                foreach (var config in chartConfig)
                {
                    jsonData.AppendFormat("'{0}':'{1}',", config.Key, config.Value);
                }
                jsonData.Replace(",", "},", jsonData.Length - 1, 1);

                StringBuilder range = new StringBuilder();
                //build colorRange object
                range.Append("'colorRange':{");
                range.Append("'color':[");
                foreach (ColorRange clr in color)
                {
                    range.AppendFormat("{{'minValue':'{0}','maxValue':'{1}','code':'{2}'}},", clr.Min, clr.Max, clr.ColorCode);
                }
                range.Replace(",", "]},", range.Length - 1, 1);

                // build data object
                StringBuilder data = new StringBuilder();
                data.Append("'data':[");
                foreach (CountryData country in countries)
                {

                    string link = string.Format(country.DistrictName, drillLevel);
                    data.AppendFormat("{{'id':'{0}','value':'{1}','showLabel':'{2}'}},", country.ID, country.Value, country.ShowLabel);
                    //data.AppendFormat("{{'id':'{0}','value':'{1}','showLabel':'{2}'}},", country.ID, country.Value);

                }
                data.Replace(",", "]", data.Length - 1, 1);
                jsonData.Append(range);
                jsonData.Append(data);
                jsonData.Append("}");
                //Create map instance
                // map type, mapid, width, height, data format, data

                if (drillLevel == "1")
                {
                    Chart MyFirstMap = new Chart("column2d", "first_map", "800", "500", "json", jsonData.ToString());
                    //render map
                    Literal10.Text = MyFirstMap.Render();
                }

                else
                {
                    Chart MyFirstMap = new Chart("bihar", "first_map", "800", "500", "json", jsonData.ToString());
                    //render map
                    Literal10.Text = MyFirstMap.Render();
                }

            }

        }

        #endregion

        protected void rdbUnitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["rdbUnitType"] = rdbUnitType.SelectedValue;
            if (rdbUnitType.SelectedValue=="C")
            {
                rdbUnitType.SelectedValue = "C";
                Response.Redirect("CMS_DashBoard.aspx");
            }
            else if (rdbUnitType.SelectedValue == "S")
            {
                Response.Redirect("SCMDashBoard");
            }
                else
            {
                Response.Redirect("SCMDashBoard");
            }

        }
    }
}
