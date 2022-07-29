using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;

namespace UserMgmt
{
    public partial class DistrictOfficerView : System.Web.UI.Page
    {
        public class DistrictOfficerViewList
        {
            public string liq_type { get; set; }
            public string indication { get; set; }
            public int temperature { get; set; }
            public string proof { get; set; }
            public string color { get; set; }
            public string smell { get; set; }
            public string remarks { get; set; }
            public string status { get; set; }
            public string final_status { get; set; }

            public DistrictOfficerViewList() { }

            public DistrictOfficerViewList(string Liq,string Ind, int Temp, string Proof_type,float Proof_str, string Col,string Smell,string Rem,string Status, int Ptr)
            {
                this.liq_type = Liq;
                this.indication = Ind;
                this.temperature = Temp;
                this.proof = Proof_type + " " + Proof_str.ToString();
                this.color = Col;
                this.smell = Smell;
                this.remarks = Rem;
                this.status = Status;
                if (Status == "tested" || Status == "verified")
                {
                    if (Ptr == 0)
                    {
                        this.final_status = "Passed";
                    }
                    else
                    {
                        this.final_status = "Not Passed";
                    }
                }
                else
                {
                    this.final_status = "";
                }
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

        public string form_no = "";
        public int form_no_int = -1, letter_no = -1;
        public string depart_name = "", district_name = "";
        public DateTime creation_date;
        public string fir_no = "", thana_name = "";
        public string case_no = "";
        public string ref_no = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            form_no = Request.QueryString["id"];
            district_name = Request.QueryString["district"];
            form_no_int = int.Parse(form_no);
            lblForm.Text = form_no;
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
                try
                {
                    getDetails();
                    LoadGrid();
                }

                catch (Exception ex)
                {
                    Response.Write("Contact Admin : " + ex.Message.ToString());
                }
            }
        }

        public List<DistrictOfficerViewList> GetList()
        {
            List<DistrictOfficerViewList> items = new List<DistrictOfficerViewList>();
            var query = $@"SELECT
                            B.status,
                            C.liq_type,
                            D.indication,
                            D.temperature,
                            D.proof_strength,
                            D.proof_type,
                            D.color,
                            D.smell,
                            D.remarks,
                            E.parametertestresult
                           FROM
                            exciseautomation.tab_quant_form_no A
                            LEFT JOIN exciseautomation.tab_quant_received B ON A.quant_received_id=B.quant_received_id
                            LEFT JOIN exciseautomation.tab_liq_type C ON B.liq_type_id=C.liq_id
                            LEFT JOIN exciseautomation.tab_quant_id_report D ON A.quant_received_id = D.quant_received_id
                            LEFT JOIN exciseautomation.tab_quant_id_report_parameter E ON A.quant_received_id = E.quant_received_id
                           WHERE
                            A.form_no={form_no_int}";
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string t_status = dr.GetString(dr.GetOrdinal("status"));
                        string t_liQtype = dr.GetString(dr.GetOrdinal("liq_type"));
                        string t_ind = dr.GetString(dr.GetOrdinal("indication"));
                        int t_temp = dr.GetInt32(dr.GetOrdinal("temperature"));
                        string t_proof_type = dr.GetString(dr.GetOrdinal("proof_type"));
                        float t_proof_str = dr.GetFloat(dr.GetOrdinal("proof_strength"));
                        string t_col = dr.GetString(dr.GetOrdinal("color"));
                        string t_smell = dr.GetString(dr.GetOrdinal("smell"));
                        string t_remarks = dr.GetString(dr.GetOrdinal("remarks"));
                        int t_ptr = dr.GetInt32(dr.GetOrdinal("parametertestresult"));
                        items.Add(new DistrictOfficerViewList(t_liQtype, t_ind,t_temp,t_proof_type,t_proof_str,t_col,t_smell,t_remarks,t_status,t_ptr));
                    }
                }
                command.Connection.Close();
            }
            return items;
        }

        protected void grdDistrictOfficerView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDistrictOfficerView.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        private void LoadGrid()
        {
            grdDistrictOfficerView.DataSource = GetList();
            grdDistrictOfficerView.DataBind();
        }

        public void getDetails()
        {
            var query1 = $@"select letter_no from exciseautomation.tab_letter_no_form_no where form_no={form_no_int} LIMIT 1";
            using (var command = GetSqlCommand(query1))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        letter_no = dr.GetInt32(dr.GetOrdinal("letter_no"));
                    }
                }
                command.Connection.Close();
            }

            var query2 = $@"select tab_form_no_depart.form_no,tab_depart.depart_name from exciseautomation.tab_form_no_depart,exciseautomation.tab_depart where (tab_form_no_depart.depart_id=tab_depart.depart_id) and tab_form_no_depart.form_no={form_no_int} LIMIT 1";
            using (var command = GetSqlCommand(query2))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        depart_name = dr.GetString(dr.GetOrdinal("depart_name"));
                    }
                }
                command.Connection.Close();
            }

            var query3 = $@"select date_of_creation from exciseautomation.tab_form_no_date where form_no={form_no_int} LIMIT 1";
            using (var command = GetSqlCommand(query3))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        creation_date = dr.GetDateTime(dr.GetOrdinal("date_of_creation"));
                    }
                }
                command.Connection.Close();
            }

            var query4 = $@"select tab_form_no_dist_id.form_no,tab_district.dist_name from exciseautomation.tab_form_no_dist_id,exciseautomation.tab_district where (tab_form_no_dist_id.dist_id=tab_district.dist_id) and form_no='{form_no_int}' LIMIT 1";
            using (var command = GetSqlCommand(query4))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        district_name = dr.GetString(dr.GetOrdinal("dist_name"));
                    }
                }
                command.Connection.Close();
            }

            lblForm.Text = form_no;
            lblLetter.Text = letter_no.ToString();
            if (depart_name == "police")
            {
                var query5 = $@"select tab_thana.thana_name,tab_police_info.fir_no from exciseautomation.tab_police_info,exciseautomation.tab_thana where (tab_police_info.thana_id=tab_thana.thana_id) and  form_no='{form_no_int}' LIMIT 1";
                using (var command = GetSqlCommand(query5))
                {
                    command.Connection.Open();
                    NpgsqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            fir_no = dr.GetString(dr.GetOrdinal("fir_no"));
                            thana_name = dr.GetString(dr.GetOrdinal("thana_name"));
                        }
                    }
                    command.Connection.Close();
                }
                lblFir.Text = fir_no;
                lblThana.Text = thana_name;
            }
            if (depart_name == "excise")
            {
                var query6 = $@"select case_no from exciseautomation.tab_excise_info where form_no='{form_no_int}' LIMIT 1";
                using (var command = GetSqlCommand(query6))
                {
                    command.Connection.Open();
                    NpgsqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            case_no = dr.GetString(dr.GetOrdinal("case_no"));
                        }
                    }
                    command.Connection.Close();
                }
                lblCase.Text = case_no;
            }
            if (depart_name == "distillery")
            {
                var query7 = $@"select ref_no from exciseautomation.tab_distill_info where form_no='{form_no_int}' LIMIT 1";
                using (var command = GetSqlCommand(query7))
                {
                    command.Connection.Open();
                    NpgsqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ref_no = dr.GetString(dr.GetOrdinal("ref_no"));
                        }
                    }
                    command.Connection.Close();
                }
                lblRef.Text = ref_no;
            }
            lblDIst.Text = district_name;
            lblDate.Text = creation_date.ToString();
        }
    }
}