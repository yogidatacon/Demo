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

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        // level mapping for each column
        string[] levelValueMapping = new string[] { "region", "country", "city" };


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
    private string BuildQuery(string columnName, string parentValue, string parentName)
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

        string linkParam = "newchart-jsonurl-Handler/Handler.ashx?label={0}&drillLevel={1}";
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            i += Convert.ToInt32(dr[1].ToString());
        }

        chartConfig.Add("caption", "Total Seizures by District Today"); // caption will change dynamically based on chart label
        chartConfig.Add("xAxisName", "District"); // xaxis name will chnage dynamically based on chart label
        chartConfig.Add("yAxisName", "Total Seizure");

        chartConfig.Add("theme", "fusion");
        chartConfig.Add("defaultcenterlabel", "");
        chartConfig.Add("animation", "1");
        chartConfig.Add("animationDuration", "5");

        chartConfig.Add("enableSmartLabels", "1");
        chartConfig.Add("centerLabelFontSize", "20");
        chartConfig.Add("centerLabelBold", "1");
        chartConfig.Add("rotateNames", "1");

        chartConfig.Add("showvalues", "1");
        chartConfig.Add("formatNumberScale", "0");
        chartConfig.Add("rotateValues", "1");
        chartConfig.Add("useDataPlotColorForLabels", "1");
        chartConfig.Add("valueFontColor", "#FF5733 ");
        chartConfig.Add("valueFontBold", "1");


        chartConfig.Add("showHoverEffect", "1");
        chartConfig.Add("plotHoverEffect", "1");
        chartConfig.Add("dataInvalidMessage", "No Seizure's");
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

                data.AppendFormat("{{\"label\":\"{0}\",\"value\":\"{1}\"}},", row[0].ToString(), row[1].ToString());

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
            using (NpgsqlDataAdapter da = new NpgsqlDataAdapter("SELECT dm.district_name ,count(seizureno) as SeizureNoCount FROM exciseautomation.seizure_basicinfo sb "
                + " inner join exciseautomation.district_master dm on dm.district_code =sb.district_code where sb.creation_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "' group by dm.district_code,dm.district_name order by count(seizureno) desc", cn))
            {
                // fill data table
                da.Fill(dt);
if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add("No Records", 0);
                }


            }

        }
    }

}