using FusionCharts.Charts;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserMgmt
{
    public partial class DigiLocker : System.Web.UI.Page
    {
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
        public string userid;
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
                btnByThana.BackColor = Color.SandyBrown;
                RenderChartD2();
              
            }
        }
        private void RenderChartD2()
        {
            Chart chart = new Chart("doughnut2d", "first_chart", "1000", "400", "jsonurl", "Handler2/DigiLockerSummary.ashx?userid=" + Session["UserID"].ToString());
            Chart chart2 = new Chart("column2d", "second_chart", "1000", "300", "jsonurl", "Handler2/DigiLocksClosed.ashx?userid=" + Session["UserID"].ToString());
            Chart chart3 = new Chart("column2d", "third_chart", "1000", "300", "jsonurl", "Handler2/DigiLockerDistrictWise.ashx?userid=" + Session["UserID"].ToString());
            Chart chart4 = new Chart("column2d", "fourth_chart", "1000", "300", "jsonurl", "Handler2/DigiLocksTampered.ashx?userid=" + Session["UserID"].ToString());
            //Chart chart5 = new Chart("column2d", "fifth_chart", "290", "300", "jsonurl", "Handler/TodayHandler.ashx");
            Chart chart6 = new Chart("column2d", "sixth_chart", "1000", "300", "jsonurl", "Handler2/DigiLocksOverStoppage.ashx?userid=" + Session["UserID"].ToString());
            overall.Text = chart.Render();
            DistrictClosed.Text = chart2.Render();
            DistrictCreated.Text = chart3.Render();
            DistrictTampered.Text = chart4.Render();
            //Literal5.Text = chart5.Render();
            DistrictOverStop.Text = chart6.Render();
        }
        private void RenderChart()
        {
            Chart chart3 = new Chart("column2d", "third_chart", "1000", "300", "jsonurl", "Handler2/DigiLockerDistrictWise.ashx?userid=" + Session["UserID"].ToString());
            DistrictCreated.Text = chart3.Render();
        }
        private void RenderChartD3()
        {
            Chart chart9 = new Chart("column2d", "ninth_chart", "1000", "300", "jsonurl", "Handler/TodayHandler.ashx?userid=" + Session["UserID"].ToString());
            DistrictCreated.Text = chart9.Render();
        }
        protected void btnByThana_Click(object sender, EventArgs e)
        {

            RenderChartD2();
            btnByThana.BackColor = Color.SandyBrown;
            btnByDate.BackColor = Color.SkyBlue;

        }

        protected void btnByDate_Click(object sender, EventArgs e)
        {
            RenderChartD3();

            btnByDate.BackColor = Color.SandyBrown;
            btnByThana.BackColor = Color.SkyBlue;

        }
    }

}