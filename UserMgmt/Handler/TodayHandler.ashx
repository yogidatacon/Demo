<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Npgsql;
using System.Linq;


public class Handler : IHttpHandler
{
    string userid = "";
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        // level mapping for each column
        string[] levelValueMapping = new string[]  { "region","country","city"};
        userid = context.Request.QueryString["userid"];

        //string drillLevel = context.Request["drillLevel"];
        //string query = "";
        //string label = "";
        //if (string.IsNullOrEmpty(drillLevel))
        //{
        //    drillLevel = "0";
        //    // build custom query 
        //    // parameter: column to be fetch
        //    query = BuildQuery(levelValueMapping[(Convert.ToInt16(drillLevel))]);
        //}
        //else
        //{
        //    drillLevel = (Convert.ToInt16(drillLevel) + 1).ToString();
        //    label = context.Request["label"];
        //    // build custom query 
        //    // parameter: column to be fetch, previously clicked value, previous level column name
        //    query = BuildQuery(levelValueMapping[(Convert.ToInt16(drillLevel))],label,levelValueMapping[(Convert.ToInt16(drillLevel)-1)]);
        //}

        DataTable dt = new DataTable();
        // establish DB connection and fetch chart data
        GetChartData(ref dt);
        // from DB data create chart compatible json
        string chartJsonData = ProcessChartData(dt);
        // send response
        context.Response.Write(chartJsonData);



    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    private string BuildQuery(string columnName)
    {
        string query;
        query = "select " + columnName + ", SUM([Total Sales]) as [Total Sales]" + "from Sales_Record group by " + columnName;
        return query;
    }
    private string BuildQuery(string columnName, string parentValue,string parentName)
    {
        string query;
        query = "select " + columnName + ", SUM([Total Sales]) as [Total Sales]" + "from Sales_Record where " + parentName + "= '" + parentValue + "'Group by " + columnName;
        return query;

    }
    private string ProcessChartData(DataTable dt)
    {
        StringBuilder jsonData = new StringBuilder();
        StringBuilder data = new StringBuilder();
        // store chart config name-config value pair

        Dictionary<string, string> chartConfig = new Dictionary<string, string>();

        string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["CASEMGMT"].ConnectionString;
        NpgsqlConnection cn = new NpgsqlConnection(connstring);

        string total ="";


        using (NpgsqlConnection con = new NpgsqlConnection(connstring))
        {
            DataTable dtt = new DataTable();
            con.Open();
            using (NpgsqlCommand command = new NpgsqlCommand())
            using (NpgsqlDataAdapter da = new NpgsqlDataAdapter("SELECT count(seizureno) as SeizureNoCount FROM"
                + " exciseautomation.seizure_basicinfo", cn))
            {
                // fill data table
                da.Fill(dtt);
                total = dtt.Rows[0]["SeizureNoCount"].ToString();

            }

        }

        string linkParam = "newchart-jsonurl-Handler/Handler.ashx?label={0}&drillLevel={1}";
        int i = 0;


        chartConfig.Add("caption", "TOTAL DIGITAL LOCKS CREATED AT SOURCE -  DISTRICT WISE"); // caption will change dynamically based on chart label
        chartConfig.Add("xAxisName", "DISTRICT"); // xaxis name will chnage dynamically based on chart label
        chartConfig.Add("yAxisName", "TOTAL DIGITAL LOCKS CREATED");
        chartConfig.Add("numberSuffix", "");
        chartConfig.Add("theme", "fusion");
        chartConfig.Add("defaultcenterlabel", "TOTAL LOCKS CREATED" + i);
        chartConfig.Add("animation", "1");
        chartConfig.Add("animationDuration", "5");
        chartConfig.Add("enableRotation", "1");
        chartConfig.Add("enableSmartLabels", "1");
        chartConfig.Add("centerLabelFontSize", "12");
        chartConfig.Add("centerLabelBold", "1");
        chartConfig.Add("formatNumberScale", "0");
        chartConfig.Add("exportEnabled", "1");
        chartConfig.Add("showHoverEffect", "1");
        chartConfig.Add("plotHoverEffect", "1");
        chartConfig.Add("alignLegendWithCanvas", "0");
        chartConfig.Add("showPercentValues", "0");
        chartConfig.Add("showValues", "1");
        chartConfig.Add("animateClockwise", "1");
        chartConfig.Add("labelSepChar", " : ");
        chartConfig.Add("labelFontBold", "1");
        chartConfig.Add("labelFontSize", "12");
        chartConfig.Add("showLegend", "0");
        chartConfig.Add("palettecolors", "#c0b8ec");
        // json data to use as chart data source
        jsonData.Append("{\"chart\":{");
        foreach (var config in chartConfig)
        {
            jsonData.AppendFormat("\"{0}\":\"{1}\",", config.Key, config.Value);
        }
        jsonData.Replace(",", "},", jsonData.Length - 1, 1);

        data.Append("\"data\":[");


        //iterate through data table to build data object
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                //if(Convert.ToInt16(drillLevel) < maxLevel - 1)
                //{
                //    string link = string.Format(linkParam,row[0].ToString(), drillLevel);
                //    data.AppendFormat("{{\"label\":\"{0}\",\"value\":\"{1}\", \"link\": \"{2}\"}},", row[0].ToString(), row[1].ToString(), link);
                //}

                // data.AppendFormat("{{\"label\":\"{0}\",\"value\":\"{1}\"}},", row[0].ToString(), row[1].ToString());
                string link = string.Format(linkParam, row[0].ToString(), "0");
                data.AppendFormat("{{\"label\":\"{0}\",\"value\":\"{1}\", \"link\": \"{2}\"}},", row[0].ToString(), row[1].ToString(), link);

            }
        }
        data.Replace(",", "]", data.Length - 1, 1);

        jsonData.Append(data.ToString());
        jsonData.Append("}");
        return jsonData.ToString();
    }

    private void GetChartData(ref DataTable dt)
    {

        //string connetionString = null;
        //string serverName = "POUSHALI-PC\\SHAREPOINT";
        //string databaseName = "DrillDownDB";
        //clear previous data from data table
        dt.Clear();
        // we are connectiong by windows authentication so, Trusted_Connection = True;
        //connetionString = "Data Source=" + serverName + ";Initial Catalog=" + databaseName + ";Trusted_Connection=True;";

        string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["CASEMGMT"].ConnectionString;
        NpgsqlConnection cn = new NpgsqlConnection(connstring);



        using (NpgsqlConnection con = new NpgsqlConnection(connstring))
        {
            con.Open();
            using (NpgsqlCommand command = new NpgsqlCommand())

            using (NpgsqlDataAdapter da= new NpgsqlDataAdapter("select source_district,sum(count) as LockCountss from exciseautomation.View_digilocker_districtwise"

                + " where alert_type='LOCKS CREATED AT SOURCE' group by source_district ORDER BY sum(count) DESC ", cn))
            {
                // fill data table
                da.Fill(dt);

            }

        }
    }

}