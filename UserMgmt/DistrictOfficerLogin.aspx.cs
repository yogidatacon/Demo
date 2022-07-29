using System;
using System.Configuration;
using System.Web.UI.WebControls;
using Npgsql;
using Usermngt.BL.LabTechnician;
using Usermngt.BL.DataUtility;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace UserMgmt
{
    public partial class DistrictOfficerLogin : System.Web.UI.Page
    {
        public class DistrictOfficerList
        {
            public string district_name { get; set; }
            public string form_no { get; set; }
            public DateTime form_date { get; set; }
            public string status { get; set; }

            public DistrictOfficerList(string districtName, string formNo, DateTime formDate, string Status)
            {
                this.district_name = districtName;
                this.form_no = formNo;
                this.form_date = formDate;
                this.status = Status;
            }

        }

        public string ConnectionString => ConfigurationManager.ConnectionStrings["CASEMGMT"].ConnectionString;

        public NpgsqlConnection GetSqlConnection()
        {
            return new NpgsqlConnection(ConnectionString);
        }

        public NpgsqlCommand GetSqlCommand(string commandText, CommandType commandType = CommandType.Text)
        {
            var sqlCommand = new NpgsqlCommand(commandText, GetSqlConnection())
            {
                CommandType = commandType
            };
            return sqlCommand;
        }

        string district = "", department = "";
        int department_id = -1, district_id = -1;
        string cur_query = $@"";
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
            }
            populate();
        }

        public void populate()
        {
            district_id = Convert.ToInt32(Session["Dist_id"].ToString());//(int)Session["Dist_id"];
            department_id = Convert.ToInt32(Session["Dept_id"].ToString());//(int)Session["Dept_id"];

            //Bhavin
            //district_id = 16;
            //department_id = 2;
            //End

            try
            {
                district = (string)Session["district_name"];
                department = (string)Session["depart_name"];
                dolDepartLabel.Text = department;
                dolDistrictLabel.Text = district;
                cur_query = $@"SELECT tfnd.form_no, tfnd.date_of_creation, tfndi.dist_id, tfndep.depart_id FROM exciseautomation.tab_form_no_date as tfnd
                        LEFT JOIN exciseautomation.tab_form_no_dist_id as tfndi ON tfnd.form_no = tfndi.form_no
                        LEFT JOIN exciseautomation.tab_form_no_depart as tfndep ON tfnd.form_no = tfndep.form_no
                        WHERE tfndep.depart_id={department_id} AND tfndi.dist_id='{district_id}'";
                LoadGrid();
            }
            catch(Exception ex)
            {
                Response.Write("Contact Admin : " + ex.Message.ToString());
            }
        }

        protected void DOLShow_Click(object sender, EventArgs e)
        {
            string from_date = fromDate.Text.ToString();
            string to_date = toDate.Text.ToString();
            district_id = Convert.ToInt32(Session["Dist_id"].ToString());//(int)Session["Dist_id"];
            department_id = Convert.ToInt32(Session["Dept_id"].ToString());//(int)Session["Dept_id"];
            //district_id = (int)Session["Dist_id"];
            //department_id = (int)Session["Dept_id"];

            //Bhavin
            //district_id = 16;
            //department_id = 2;
            //End

            DateTime startDate, endDate;
            if (from_date == "")
            {
                startDate = DateTime.ParseExact(convertDate("01/01/1900"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            else
            {
                startDate =  DateTime.ParseExact(convertDate(from_date), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            if(to_date == "")
            {
                endDate = DateTime.ParseExact(convertDate("01/01/2100"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            else
            {
                endDate =  DateTime.ParseExact(convertDate(to_date), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            cur_query = $@"SELECT tfnd.form_no, tfnd.date_of_creation, tfndi.dist_id, tfndep.depart_id FROM exciseautomation.tab_form_no_date as tfnd
                           LEFT JOIN exciseautomation.tab_form_no_dist_id as tfndi ON tfnd.form_no = tfndi.form_no
                           LEFT JOIN exciseautomation.tab_form_no_depart as tfndep ON tfnd.form_no = tfndep.form_no
                           WHERE tfndep.depart_id='{department_id}' AND tfndi.dist_id='{district_id}' AND tfnd.date_of_creation between '{startDate}' AND '{endDate}'";
            LoadGrid();
        }

        protected void grdDistrictOfficerLogin_RowCommand(object sender, GridViewCommandEventArgs e)
        {
             if (e.CommandName == "Edit")
            {
                var inputParams = e.CommandArgument.ToString().Split(',');
                var id = inputParams.Length > 0 ? inputParams[0] : default(string);
                var district_name = inputParams.Length > 1 ? inputParams[1] : default(string);
                Response.Redirect($"~/DistrictOfficerView.aspx?id={id}&district={district_name}");
            } 
        }

        public List<DistrictOfficerList> GetList()
        {
            List<DistrictOfficerList> items = new List<DistrictOfficerList>();
            using (var command = GetSqlCommand(cur_query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string t_form_no = dr.GetString(dr.GetOrdinal("form_no"));
                        DateTime t_date = dr.GetDateTime(dr.GetOrdinal("date_of_creation"));
                        var query2 = $@"SELECT  tqr.status FROM exciseautomation.tab_quant_form_no as tqfn  JOIN exciseautomation.tab_quant_received as tqr ON tqfn.quant_received_id = tqr.quant_received_id WHERE tqr.status is not null and tqfn.form_no = '{t_form_no}'";
                        List<string> status_list = new List<string>();
                        using (var command2 = GetSqlCommand(query2))
                        {
                            command2.Connection.Open();
                            NpgsqlDataReader dr2 = command2.ExecuteReader();
                            if (dr2.HasRows)
                            {
                                while (dr2.Read())
                                {
                                    status_list.Add(dr2.GetString(dr2.GetOrdinal("status")));
                                }
                            }
                            command2.Connection.Close();
                        }
                        string t_status = "Partial";
                        status_list.Sort();
                        int len = status_list.Count;
                        if (status_list.Count > 0)
                        {
                            if (status_list[0] == status_list[len - 1])
                            {
                                if (status_list[0] == "verified")
                                {
                                    t_status = "Closed";
                                }
                                else if (status_list[0] == "tested")
                                {
                                    t_status = "Partial";
                                }
                                else if (status_list[0] == "untested")
                                {
                                    t_status = "Pending";
                                }
                            }
                            items.Add(new DistrictOfficerList(district, t_form_no, t_date, t_status));
                        }
                    }
                }
                command.Connection.Close();
            }
            return items;
        }

        protected void grdDistrictOfficerLogin_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDistrictOfficerLogin.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        private void LoadGrid()
        {
            grdDistrictOfficerLogin.DataSource = GetList();
            grdDistrictOfficerLogin.DataBind();
        }

        public string convertDate(string s)

        {
          

             DateTime dt = DateTime.ParseExact(s, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            //string month = dt.Month.ToString();
            //string year = dt.Year.ToString();
            //string day = dt.Day.ToString();

            string year = s.Substring(0, 4);
            string month = s.Substring(5, 2);
          string day = s.Substring(8, 2);
            string new_date = year + "-" + month + "-" + day;
            return new_date;
        }
    }
}