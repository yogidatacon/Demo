using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL.LabTechnician;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class LabTechreportForm : System.Web.UI.Page
    {
        public class parameterChecklist
        {
            public string txt { get; set; }
            public bool tick { get; set; }

            public parameterChecklist() { }

            public parameterChecklist(string Txt, bool Tick)
            {
                this.txt = Txt;
                this.tick = Tick;
            }

        }

        public class parameterTextlist
        {
            public string txt { get; set; }
            public string input { get; set; }

            public parameterTextlist() { }

            public parameterTextlist(string Txt, string Input)
            {
                this.txt = Txt;
                this.input = Input;
            }
        }

        public string form_no, qid, mode;
        public int form_no_int, qid_int;
        public string quantity, batch_no, address, status;
        public int compactor_id, liq_id, liq_sub_type_id;
        public string liq_type, liq_sub_type_name, size_name, brand_name;
        public string date_of_creation, proof_type, color, smell, remarks;
        public float proof_strength;
        public int labdevice, temperature, parametertestresult, parametertest;
        public string indication, pyknometerempty, pyknometerdmwater, pyknometersample;
        public string parameter_ids, parameter_checked, parameter_text, testremarks;
        public string formanddate;
        public string hydroChecked = "", pyknoChecked = "";
        public string pyknotemperature = "", hydrotemperature = "";
        public Dictionary<int, string> dict;
        public string assigned_parameter_str;
        public List<string> assigned_para_list, checked_para_list, input_list;
        public List<parameterChecklist> parameterchecklist;
        public List<parameterTextlist> parametertextlist;
        public List<string> colors;

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
        //Bhavin
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Session["UserID"] = Session["UserID"].ToString();
            Response.Redirect("~/LabTechnicianList.aspx");
        }
        //End


        protected void parameterpasses_CheckedChanged(object sender, EventArgs e)
        {
            int rowcount = parametergrid1.Rows.Count;
            for (int i = 0; i < rowcount; i++)
            {
                GridViewRow row = parametergrid1.Rows[i];
                CheckBox cb = (CheckBox)row.FindControl("parameter_checkbox");
                cb.Enabled = true;
                cb.Checked = true;               
            }

            int rowcount2 = parametergrid2.Rows.Count;
            for (int j = 0; j < rowcount2; j++)
            {
                GridViewRow row = parametergrid2.Rows[j];
                //TextBox
                TextBox tb = (TextBox)row.FindControl("parameter_textbox");
                tb.Text = "";
                tb.Enabled = false;
            }
            populate();
        }

        protected void parameterpassesbyvalue_CheckedChanged(object sender, EventArgs e)
        {
            int rowcount2 = parametergrid2.Rows.Count;
            for (int j = 0; j < rowcount2; j++)
            {
                GridViewRow row = parametergrid2.Rows[j];
                //TextBox
                TextBox tb = (TextBox)row.FindControl("parameter_textbox");
                tb.Text = "";
                tb.Enabled = true;
            }

            int rowcount = parametergrid1.Rows.Count;
            for (int i = 0; i < rowcount; i++)
            {
                GridViewRow row = parametergrid1.Rows[i];
                CheckBox cb = (CheckBox)row.FindControl("parameter_checkbox");
                cb.Checked = false;
                cb.Enabled = false;
            }
            populate();
        }


        protected void parametergrid1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    ((CheckBox)e.Row.FindControl("parameter_checkbox")).Checked = true;
            //}
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            form_no = "";
            qid = "";
            mode = Request.QueryString["status"];
            form_no_int = -1;
            qid_int = -1;
            dict = new Dictionary<int, string>();
            assigned_para_list = new List<string>();
            checked_para_list = new List<string>();
            parameterchecklist = new List<parameterChecklist>();
            parametertextlist = new List<parameterTextlist>();
            input_list = new List<string>();
            colors = new List<string>();
            lblError.Visible = false;
            hide();
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
                populate();
            }
            else
            {
                //string msg = Request.QueryString["msg"];
                //if (msg != "")
                //{
                //    lblError.Text = msg;
                //     lblError.Visible = true;
                //}
            }
        }

        private void populate()
        {
            form_no = Request.QueryString["form_no"];
            form_no_int = int.Parse(form_no);
            qid = Request.QueryString["qid"];
            qid_int = int.Parse(qid);
            mode = Request.QueryString["status"];
            if (mode == "retest")
            {
                try
                {
                    var query = $@"SELECT
                            A.quant_received_id,
                            A.quantity,
                            A.batch_no,
                            A.address,
                            A.compactor_id,
                            A.status,
                            B.liq_type,
                            B.liq_id,
                            C.liq_sub_type_id,
                            C.liq_sub_type_name,
                            D.size_name,
                            E.brand_name,
                            F.form_no,
                            G.date_of_creation,
                            H.proof_strength,
                            H.proof_type,
                            H.color,
                            H.smell,
                            H.remarks,
                            I.parametertestresult,
                            I.labdevice,
                            I.indication,
                            I.temperature,
                            I.pyknometerempty, 
                            I.pyknometerdmwater,
                            I.pyknometersample,
                            I.parametertest,
                            I.parameter_ids,
                            I.parameter_checked,
                            I.parameter_text,
                            I.testremarks
                           FROM
                            exciseautomation.tab_quant_received A
                            LEFT JOIN exciseautomation.tab_liq_type B ON A.liq_type_id=B.liq_id
                            LEFT JOIN exciseautomation.tab_liq_subtype C ON A.sub_type_id=C.liq_sub_type_id
                            LEFT JOIN exciseautomation.tab_liq_size D ON A.size_id=D.size_id
                            LEFT JOIN exciseautomation.tab_liq_brand_name E ON A.brand_name_id=E.brand_id
                            LEFT JOIN exciseautomation.tab_quant_form_no F ON A.quant_received_id=F.quant_received_id
                            LEFT JOIN exciseautomation.tab_form_no_date G ON F.form_no = G.form_no
                            LEFT JOIN exciseautomation.tab_quant_id_report H ON A.quant_received_id = H.quant_received_id
                            LEFT JOIN exciseautomation.tab_quant_id_report_parameter I ON A.quant_received_id = I.quant_received_id
                           WHERE
                            A.quant_received_id={qid_int}";

                    using (var command = GetSqlCommand(query))
                    {
                        command.Connection.Open();
                        NpgsqlDataReader dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                quantity = dr.GetString(dr.GetOrdinal("quantity"));
                                batch_no = dr.GetString(dr.GetOrdinal("batch_no"));
                                address = dr.GetString(dr.GetOrdinal("address"));
                                status = dr.GetString(dr.GetOrdinal("status"));
                                compactor_id = dr.GetInt32(dr.GetOrdinal("compactor_id"));
                                liq_id = dr.GetInt32(dr.GetOrdinal("liq_id"));
                                liq_sub_type_id = dr.GetInt32(dr.GetOrdinal("liq_sub_type_id"));
                                liq_type = dr.GetString(dr.GetOrdinal("liq_type"));
                                liq_sub_type_name = dr.GetString(dr.GetOrdinal("liq_sub_type_name"));
                                size_name = dr.GetString(dr.GetOrdinal("size_name"));
                                brand_name = dr.GetString(dr.GetOrdinal("brand_name"));
                                proof_type = dr.GetString(dr.GetOrdinal("proof_type"));
                                color = dr.GetString(dr.GetOrdinal("color"));
                                smell = dr.GetString(dr.GetOrdinal("smell"));
                                remarks = dr.GetString(dr.GetOrdinal("remarks"));
                                proof_strength = dr.GetFloat(dr.GetOrdinal("proof_strength"));
                                DateTime creationDate = dr.GetDateTime(dr.GetOrdinal("date_of_creation"));
                                date_of_creation = creationDate.ToString();
                                formanddate = form_no + '/' + date_of_creation;
                                labdevice = dr.GetInt32(dr.GetOrdinal("labdevice"));
                                temperature = dr.GetInt32(dr.GetOrdinal("temperature"));
                                parametertest = dr.GetInt32(dr.GetOrdinal("parametertest"));
                                parametertestresult = dr.GetInt32(dr.GetOrdinal("parametertestresult"));
                                indication = dr.GetString(dr.GetOrdinal("indication"));
                                pyknometerempty = dr.GetString(dr.GetOrdinal("pyknometerempty"));
                                pyknometerdmwater = dr.GetString(dr.GetOrdinal("pyknometerdmwater"));
                                pyknometersample = dr.GetString(dr.GetOrdinal("pyknometersample"));
                                parameter_ids = dr.GetString(dr.GetOrdinal("parameter_ids"));
                                parameter_checked = dr.GetString(dr.GetOrdinal("parameter_checked"));
                                parameter_text = dr.GetString(dr.GetOrdinal("parameter_text"));
                                testremarks = dr.GetString(dr.GetOrdinal("testremarks"));
                                //testremarks = dr.GetString(dr.GetOrdinal("remarks"));
                            }
                        }
                        command.Connection.Close();
                    }
                    if (parameter_checked.Length != 0)
                    {
                        checked_para_list = parameter_checked.Split(',').ToList<string>();
                    }
                    if (parameter_text.Length != 0)
                    {
                        input_list = parameter_text.Split(',').ToList<string>();
                    }
                    //var query2 = $@"SELECT parameter_id, parameter_type FROM exciseautomation.tab_parameter order by parameter_type asc ";
                    //using (var command = GetSqlCommand(query2))
                    //{
                    //    command.Connection.Open();
                    //    NpgsqlDataReader dr = command.ExecuteReader();
                    //    if (dr.HasRows)
                    //    {
                    //        while (dr.Read())
                    //        {
                    //            int t1 = dr.GetInt32(dr.GetOrdinal("parameter_id"));
                    //            string t2 = dr.GetString(dr.GetOrdinal("parameter_type"));
                    //            dict.Add(t1, t2);
                    //        }
                    //    }
                    //    command.Connection.Close();
                    //}


                    var query2 = $@"Select parameter_master_id, parameter_master_name from exciseautomation.parameter_master";
                    using (var command = GetSqlCommand(query2))
                    {
                        command.Connection.Open();
                        NpgsqlDataReader dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                int t1 = dr.GetInt32(dr.GetOrdinal("parameter_master_id"));
                                string t2 = dr.GetString(dr.GetOrdinal("parameter_master_name"));
                                dict.Add(t1, t2);
                            }
                        }
                        command.Connection.Close();
                    }



                    // var query3 = $@"SELECT * FROM exciseautomation.tab_parameter_assign WHERE liq_sub_type_name='{liq_sub_type_id}' AND liq_type='{liq_id}' LIMIT 1";


                    //var query3 = $@"SELECT B.parameter_master_id from exciseautomation.assign_parameter as A Inner Join  
                    //             exciseautomation.assign_parameter_assigned_list as B on A.assign_parameter_id = B.assign_parameter_id
                    //             where A.type_of_liquor_id = '{liq_id}' and A.liquor_sub_type_id = '{liq_sub_type_id}'";

                    //Jay start
                    var query3 = $@"SELECT B.parameter_master_id from exciseautomation.assign_parameter as A Inner Join  
                                 exciseautomation.assign_parameter_assigned_list as B on A.assign_parameter_id = B.assign_parameter_id
                                 where A.type_of_liquor_id = '{liq_id}' and A.liquor_sub_type_id = '{liq_sub_type_id}'";
                    //Jay end
                    using (var command = GetSqlCommand(query3))
                    {
                        command.Connection.Open();
                        NpgsqlDataReader dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                //int t_liq_para_id = dr.GetInt32(dr.GetOrdinal("liq_parameter_id"));
                                //assigned_parameter_str = dr.GetString(dr.GetOrdinal("assigned_parameter"));

                                assigned_parameter_str = dr.GetInt32(dr.GetOrdinal("parameter_master_id")).ToString();


                                ViewState["assigned"] = assigned_parameter_str;
                                //System.Diagnostics.Debug.WriteLine((string)ViewState["assigned"]);
                                if (assigned_parameter_str.Length != 0)
                                {
                                    assigned_para_list = assigned_parameter_str.Split(',').ToList<string>();
                                }
                                //passes 
                                for (int i = 0; i < assigned_para_list.Count; i++)
                                {
                                    int val = int.Parse(assigned_para_list[i]);
                                    if (checked_para_list.Count > 0)
                                    {
                                        if (checked_para_list.Contains(i.ToString()))
                                        {
                                            parameterchecklist.Add(new parameterChecklist(dict[val], true));
                                        }
                                        else
                                        {
                                            parameterchecklist.Add(new parameterChecklist(dict[val], false));
                                        }
                                    }
                                    else
                                    {
                                        parameterchecklist.Add(new parameterChecklist(dict[val], false));
                                    }
                                }
                                //passes by value

                                for (int i = 0; i < assigned_para_list.Count; i++)
                                {
                                    int val = int.Parse(assigned_para_list[i]);
                                    if (input_list.Count > i)
                                    {
                                        parametertextlist.Add(new parameterTextlist(dict[val], input_list[i]));
                                    }
                                    else
                                    {
                                        parametertextlist.Add(new parameterTextlist(dict[val], ""));
                                    }
                                }
                            }
                        }
                        command.Connection.Close();
                    }

                    var query4 = $@"select color_name from exciseautomation.tab_color order by color_name asc";
                    using (var command = GetSqlCommand(query4))
                    {
                        command.Connection.Open();
                        NpgsqlDataReader dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                string clr = dr.GetString(dr.GetOrdinal("color_name"));
                                colors.Add(clr);
                            }
                        }
                        command.Connection.Close();
                    }

                    colorDropdown.DataSource = colors;
                    colorDropdown.DataBind();
                    colorDropdown.SelectedValue = color;
                    proofDropdown.SelectedValue = proof_type;
                    parametergrid1.DataSource = parameterchecklist;
                    parametergrid1.DataBind();
                    parametergrid2.DataSource = parametertextlist;
                    parametergrid2.DataBind();

                    if (labdevice == 0)
                    {
                        hydrotemperature = temperature.ToString();
                    }
                    else if (labdevice == 1)
                    {
                        pyknotemperature = temperature.ToString();
                    }
                    if (labdevice == 0)
                    {
                        labdeviceradiohydro.Checked = true;
                        labdeviceradiopykno.Checked = false;
                    }
                    else if (labdevice == 1)
                    {
                        labdeviceradiohydro.Checked = false;
                        labdeviceradiopykno.Checked = true;
                    }

                    if (parametertest == 0)
                    {
                        parameterpasses.Checked = true;
                        parameterpassesbyvalue.Checked = false;
                    }
                    else if (parametertest == 1)
                    {
                        parameterpasses.Checked = false;
                        parameterpassesbyvalue.Checked = true;
                    }

                    if (parametertestresult == 0)
                    {
                        testresultpassed.Checked = true;
                        testresultnotpassed.Checked = false;
                    }
                    else if (parametertestresult == 1)
                    {
                        testresultpassed.Checked = false;
                        testresultnotpassed.Checked = true;
                    }
                    txtProofstr.Text = proof_strength.ToString();
                    txtSmell.Text = smell.ToString();
                    txtRemarks.Text = remarks.ToString();
                    txtIndication.Text = indication;
                    txtHydrotemp.Text = hydrotemperature;
                    txtPyknometerempty.Text = pyknometerempty;
                    txtPyknometerdmwater.Text = pyknometerdmwater;
                    txtPyknometersample.Text = pyknometersample;
                    txtPyknotemperature.Text = pyknotemperature;
                    txtTestRemarks.Text = testremarks;
                }

                catch (Exception ex)
                {
                    Response.Write("Contact Admin : " + ex.Message.ToString());
                }
            }
            else
            {
                reset();
            }
        }

        public void reset()
        {
            try
            {
                qid = Request.QueryString["qid"];
                var query = $@"SELECT
                            A.quant_received_id,
                            A.quantity,
                            A.batch_no,
                            A.address,
                            A.compactor_id,
                            A.status,
                            B.liq_type,
                            B.liq_id,
                            C.liq_sub_type_id,
                            C.liq_sub_type_name,
                            D.size_name,
                            E.brand_name,
                            F.form_no,
                            G.date_of_creation
                           FROM
                            exciseautomation.tab_quant_received A
                            LEFT JOIN exciseautomation.tab_liq_type B ON A.liq_type_id=B.liq_id
                            LEFT JOIN exciseautomation.tab_liq_subtype C ON A.sub_type_id=C.liq_sub_type_id
                            LEFT JOIN exciseautomation.tab_liq_size D ON A.size_id=D.size_id
                            LEFT JOIN exciseautomation.tab_liq_brand_name E ON A.brand_name_id=E.brand_id
                         inner JOIN exciseautomation.tab_quant_form_no F ON A.quant_received_id=F.quant_received_id
                            LEFT JOIN exciseautomation.tab_form_no_date G ON F.form_no = G.form_no
                           WHERE
                            A.quant_received_id='{qid}'";

                using (var command = GetSqlCommand(query))
                {
                    command.Connection.Open();
                    NpgsqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            quantity = dr.GetString(dr.GetOrdinal("quantity"));
                            batch_no = dr.GetString(dr.GetOrdinal("batch_no"));
                            address = dr.GetString(dr.GetOrdinal("address"));
                            status = dr.GetString(dr.GetOrdinal("status"));
                            compactor_id = dr.GetInt32(dr.GetOrdinal("compactor_id"));
                            liq_id = dr.GetInt32(dr.GetOrdinal("liq_id"));
                            liq_sub_type_id = dr.GetInt32(dr.GetOrdinal("liq_sub_type_id"));
                            liq_type = dr.GetString(dr.GetOrdinal("liq_type"));
                            liq_sub_type_name = dr.GetString(dr.GetOrdinal("liq_sub_type_name"));
                            size_name = dr.GetString(dr.GetOrdinal("size_name"));
                            brand_name = dr.GetString(dr.GetOrdinal("brand_name"));
                            DateTime creationDate = dr.GetDateTime(dr.GetOrdinal("date_of_creation"));
                            date_of_creation = creationDate.ToString();
                            formanddate = form_no + '/' + date_of_creation;
                        }
                    }
                    command.Connection.Close();
                }


                //var query2 = $@"SELECT parameter_id, parameter_type FROM exciseautomation.tab_parameter order by parameter_type asc ";
                //using (var command = GetSqlCommand(query2))
                //{
                //    command.Connection.Open();
                //    NpgsqlDataReader dr = command.ExecuteReader();
                //    if (dr.HasRows)
                //    {
                //        while (dr.Read())
                //        {
                //            int t1 = dr.GetInt32(dr.GetOrdinal("parameter_id"));
                //            string t2 = dr.GetString(dr.GetOrdinal("parameter_type"));
                //            dict.Add(t1, t2);
                //        }
                //    }
                //    command.Connection.Close();
                //}

                var query2 = $@"Select parameter_master_id, parameter_master_name from exciseautomation.parameter_master";
                using (var command = GetSqlCommand(query2))
                {
                    command.Connection.Open();
                    NpgsqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            int t1 = dr.GetInt32(dr.GetOrdinal("parameter_master_id"));
                            string t2 = dr.GetString(dr.GetOrdinal("parameter_master_name"));
                            dict.Add(t1, t2);
                        }
                    }
                    command.Connection.Close();
                }



                // var query3 = $@"SELECT * FROM exciseautomation.tab_parameter_assign WHERE liq_sub_type_name='{liq_sub_type_id}' AND liq_type='{liq_id}' LIMIT 1";

                //var query3 = $@"SELECT B.parameter_master_id from exciseautomation.assign_parameter as A Inner Join  
                //                 exciseautomation.assign_parameter_assigned_list as B on A.assign_parameter_id = B.assign_parameter_id
                //                 where A.type_of_liquor_id = '{liq_id}' and A.liquor_sub_type_id = '{liq_sub_type_id}'";


                var query3 = $@"SELECT B.parameter_master_id from exciseautomation.assign_parameter as A Inner Join  
                                 exciseautomation.assign_parameter_assigned_list as B on A.assign_parameter_id = B.assign_parameter_id
                                 where A.type_of_liquor_id = '{liq_id}' and A.liquor_sub_type_id = '{liq_sub_type_id}'";

                using (var command = GetSqlCommand(query3))
                {
                    command.Connection.Open();
                    NpgsqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            // int t_liq_para_id = dr.GetInt32(dr.GetOrdinal("liq_parameter_id"));
                            // assigned_parameter_str = dr.GetString(dr.GetOrdinal("assigned_parameter"));
                            // assigned_parameter_str = dr.GetString(dr.GetOrdinal("parameter_master_id"));

                            assigned_parameter_str = dr.GetInt32(dr.GetOrdinal("parameter_master_id")).ToString();

                            ViewState["assigned"] = assigned_parameter_str;
                            //System.Diagnostics.Debug.WriteLine((string)ViewState["assigned"]);
                            if (assigned_parameter_str.Length != 0)
                            {
                                assigned_para_list = assigned_parameter_str.Split(',').ToList<string>();
                            }
                            //passes 
                            for (int i = 0; i < assigned_para_list.Count; i++)
                            {
                                int val = int.Parse(assigned_para_list[i]);
                                if (checked_para_list.Count > 0)
                                {
                                    if (checked_para_list.Contains(i.ToString()))
                                    {
                                        parameterchecklist.Add(new parameterChecklist(dict[val], false));
                                    }
                                    else
                                    {
                                        parameterchecklist.Add(new parameterChecklist(dict[val], false));
                                    }
                                }
                                else
                                {
                                    parameterchecklist.Add(new parameterChecklist(dict[val], false));
                                }
                            }
                            //passes by value

                            for (int i = 0; i < assigned_para_list.Count; i++)
                            {
                                int val = int.Parse(assigned_para_list[i]);
                                if (input_list.Count > i)
                                {
                                    parametertextlist.Add(new parameterTextlist(dict[val], ""));
                                }
                                else
                                {
                                    parametertextlist.Add(new parameterTextlist(dict[val], ""));
                                }
                            }
                        }
                    }
                    command.Connection.Close();
                }

                var query4 = $@"select color_name from exciseautomation.tab_color order by color_name asc";
                using (var command = GetSqlCommand(query4))
                {
                    command.Connection.Open();
                    NpgsqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            string clr = dr.GetString(dr.GetOrdinal("color_name"));
                            colors.Add(clr);
                        }
                    }
                    command.Connection.Close();
                }

                colorDropdown.DataSource = colors;
                colorDropdown.DataBind();
                //colorDropdown.SelectedValue = color;
                //proofDropdown.SelectedValue = proof_type;
                parametergrid1.DataSource = parameterchecklist;
                parametergrid1.DataBind();
                parametergrid2.DataSource = parametertextlist;
                parametergrid2.DataBind();
            }

            catch (Exception ex)
            {
                Response.Write("Contact Admin : " + ex.Message.ToString());
            }
        }

        //vishv
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            form_no = Request.QueryString["form_no"];
            qid = Request.QueryString["qid"];
            mode = Request.QueryString["status"];
            string res = validate();
            if (res == "")
            {
                update();
            }
            else
            {
                //lblError.Text = res;
                //lblError.Visible = true;
                //populate2();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            reset();
            proofDropdown.SelectedValue = "UP";
            txtProofstr.Text = "";
            txtSmell.Text = "";
            txtRemarks.Text = "";
            labdeviceradiohydro.Checked = false;
            labdeviceradiopykno.Checked = false;
            txtIndication.Text = "";
            txtPyknometerempty.Text = "";
            txtHydrotemp.Text = "";
            txtPyknometerdmwater.Text = "";
            txtPyknometersample.Text = "";
            txtPyknotemperature.Text = "";
            parameterpasses.Checked = false;
            parameterpassesbyvalue.Checked = false;
            testresultpassed.Checked = false;
            testresultnotpassed.Checked = false;
            txtTestRemarks.Text = "";
        }



        public string validate()
        {
            string msg = "";

            if (labdeviceradiohydro.Checked == false && labdeviceradiopykno.Checked == false)
            {

                //Bhavin
                 msg = "Please select one of laboratory device used for testing";
                //return msg;

                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Please select one of laboratory device used for testing ')</script>");
                return msg;
                //End
            }
            if (labdeviceradiohydro.Checked == true)
            {
                if (txtIndication.Text == "")
                {
                    //Bhavin
                    msg = "Please fillup indication value";
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Please fillup indication value ')</script>");
                    //End

                    return msg;
                }
                if (txtHydrotemp.Text == "")
                {
                    //Bhavin
                    msg = "Please fillup hydrometer temperature value";
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Please fillup hydrometer temperature value')</script>");
                    //End

                    return msg;
                }
            }
            if (labdeviceradiopykno.Checked == true)
            {
                if (txtPyknometerdmwater.Text == "")
                {
                    //Bhavin
                    msg = "Please fillup DM water with pyknometer weight value";
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Please fillup DM water with pyknometer weight value')</script>");
                    //End

                    return msg;
                }
                if (txtPyknometerempty.Text == "")
                {
                    //Bhavin
                    msg = "Please fillup empty pyknometer weight value";
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Please fillup empty pyknometer weight value')</script>");
                    //End

                    return msg;
                }
                if (txtPyknometersample.Text == "")
                {
                    //Bhavin
                    msg = "Please fillup Sample with pyknometer weight value";
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Please fillup Sample with pyknometer weight value')</script>");
                    //End

                    return msg;
                }
                if (txtPyknotemperature.Text == "")
                {
                    //Bhavin
                    msg = "Please fillup pyknometer temperature value";
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Please fillup pyknometer temperature value')</script>");
                    //End

                    return msg;
                }
            }

            if (parameterpasses.Checked == false && parameterpassesbyvalue.Checked == false)
            {
                //Bhavin
                msg = "Please select one of the parameter testing type";
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Please select one of the parameter testing type')</script>");
                //End

                return msg;
            }

            if (parameterpasses.Checked == true && testresultpassed.Checked == true)
            {
                int rowcount = parametergrid1.Rows.Count;
                for (int i = 0; i < rowcount; i++)
                {
                    GridViewRow row = parametergrid1.Rows[i];
                    CheckBox cb = (CheckBox)row.FindControl("parameter_checkbox");
                    if (cb.Checked == false)
                    {
                        //Bhavin
                        msg = "Please select right values for 'Passes the Test' parameter or select not passed";
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Please select right values for 'Passes the Test' parameter or select not passed')</script>");                        
                        return msg;
                        //End
                    }
                }
            }

            if (parameterpasses.Checked == true && testresultnotpassed.Checked == true)
            {
                int rowcount = parametergrid1.Rows.Count;
                for (int i = 0; i < rowcount; i++)
                {
                    GridViewRow row = parametergrid1.Rows[i];
                    CheckBox cb = (CheckBox)row.FindControl("parameter_checkbox");
                    if (cb.Checked == true)
                    {
                        //Bhavin
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Please uncheck some values for 'Passes the Test' parameter or select passed')</script>");
                        msg = "Please uncheck some values for 'Passes the Test' parameter or select passed";
                        return msg;
                        //End
                    }
                }
            }

            if (parameterpassesbyvalue.Checked == true && testresultpassed.Checked == true)
            {
                int rowcount2 = parametergrid2.Rows.Count;
                for (int i = 0; i < rowcount2; i++)
                {
                    GridViewRow row = parametergrid2.Rows[i];
                    TextBox tb = (TextBox)row.FindControl("parameter_textbox");
                    if (tb.Text.ToString() == "")
                    {
                        //Bhavin
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Please enter all the values in Parameter Details section under the Passes the Test by value')</script>");
                        msg = "Please enter all the values in Parameter Details section under the Passes the Test by value";
                        return msg;
                        //End
                    }
                }
            }

            //Bhavin
            if (parameterpassesbyvalue.Checked == true && testresultpassed.Checked == false)
            {
                int rowcount2 = parametergrid2.Rows.Count;
                for (int i = 0; i < rowcount2; i++)
                {
                    GridViewRow row = parametergrid2.Rows[i];
                    TextBox tb = (TextBox)row.FindControl("parameter_textbox");
                    if (tb.Text.ToString() == "")
                    {
                        //Bhavin
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Please enter all the values in Parameter Details section under the Passes the Test by value')</script>");
                        msg = "Please enter all the values in Parameter Details section under the Passes the Test by value";
                        return msg;
                        //End
                    }
                }
            }
            //End


            if (parameterpassesbyvalue.Checked == true && testresultnotpassed.Checked == true)
            {
                int rowcount2 = parametergrid2.Rows.Count;
                for (int i = 0; i < rowcount2; i++)
                {
                    GridViewRow row = parametergrid2.Rows[i];
                    TextBox tb = (TextBox)row.FindControl("parameter_textbox");
                    if (tb.Text.ToString() == "")
                    {
                        //Bhavin
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Please enter all the values in Parameter Details section under the Passes the Test by value')</script>");
                        msg = "Please enter all the values in Parameter Details section under the Passes the Test by value";
                        return msg;
                        //End
                    }
                }
            }

            if (testresultpassed.Checked == false && testresultnotpassed.Checked == false)
            {
                //Bhavin
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Please select final test result Passed or Not Passed')</script>");
                msg = "Please select final test result Passed or Not Passed";
                return msg;
                //End
            }

            if (testresultnotpassed.Checked == true && txtTestRemarks.Text.ToString() == "")
            {
                //Bhavin
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Please fillup final not passed test remark value')</script>");
                msg = "Please fillup final not passed test remark value";
                return msg;
                //End
            }

            if (txtHydrotemp.Text != null && txtHydrotemp.Text != "")
            {
                int n;
                bool isNumber = int.TryParse(txtHydrotemp.Text, out n);
                if (isNumber == false)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Please Enter number in Hydrometer Temperature')</script>");
                    msg = "Please Enter number in Hydrometer Temperature";
                    return msg;
                }
            }

            if (txtPyknotemperature.Text != null && txtPyknotemperature.Text != "")
            {
                int n;
                bool isNumber = int.TryParse(txtPyknotemperature.Text, out n);
                if (isNumber == false)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Please Enter number in Pyknometer Temperature')</script>");
                    msg = "Please Enter number in Pyknometer Temperature";
                    return msg;
                }
            }

            if (txtProofstr.Text != null && txtProofstr.Text != "")
            {
                decimal d;
                bool isDecimal = decimal.TryParse(txtProofstr.Text, out d);
                if(isDecimal == false)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Please Enter numeric in Proof Strength')</script>");
                    msg = "Please Enter numeric in Proof Strength";
                    return msg;
                }
            }

            return msg;
        }

        public void update()
        {
            try
            {
                form_no = Request.QueryString["form_no"];
                form_no_int = int.Parse(form_no);
                qid = Request.QueryString["qid"];
                qid_int = int.Parse(qid);
                mode = Request.QueryString["status"];
                string assigned_para = (string)ViewState["assigned"];
                //System.Diagnostics.Debug.WriteLine(assigned_para);
                string checked_para = "", final_parameter_text = "";
                if (parameterpasses.Checked == true)
                {
                    int rowcount = parametergrid1.Rows.Count;
                    for (int i = 0; i < rowcount; i++)
                    {
                        GridViewRow row = parametergrid1.Rows[i];
                        CheckBox cb = (CheckBox)row.FindControl("parameter_checkbox");
                        if (cb.Checked == true)
                        {
                            if (checked_para == "")
                            {
                                checked_para += (i.ToString());
                            }
                            else
                            {
                                checked_para += ',';
                                checked_para += (i.ToString());
                            }
                        }
                    }
                }
                else if (parameterpassesbyvalue.Checked == true)
                {
                    int rowcount2 = parametergrid2.Rows.Count;
                    for (int i = 0; i < rowcount2; i++)
                    {
                        GridViewRow row = parametergrid2.Rows[i];
                        TextBox tb = (TextBox)row.FindControl("parameter_textbox");
                        if (final_parameter_text == "")
                        {
                            final_parameter_text += (tb.Text.ToString());
                        }
                        else
                        {
                            final_parameter_text += ',';
                            final_parameter_text += (tb.Text.ToString());
                        }
                    }
                }

                string t_indication = "", t_pyknoempty = "", t_pyknodmwater = "", t_pyknosample = "", t_test_remarks = "";
                int t_temp = -1;
                float t_proof_str = 0;
                string t_proof_type = proofDropdown.SelectedValue.ToString();

                //Bhavin 
                if (txtProofstr.Text != "" && txtProofstr.Text != null)
                {
                     t_proof_str = float.Parse(txtProofstr.Text.ToString());
                }
                string t_color = colorDropdown.SelectedValue.ToString();
                string t_smell = txtSmell.Text.ToString();
                //string t_remarks = txtTestRemarks.Text.ToString();
                string t_remarks = txtRemarks.Text.ToString();     //Vishv2021
                int t_labdevice = 0, t_parametertest = 0, t_parameterresult = 0;

                if (labdeviceradiohydro.Checked == true)
                {
                    t_temp = int.Parse(txtHydrotemp.Text.ToString());
                    t_labdevice = 0;
                    t_indication = txtIndication.Text.ToString();
                    t_pyknodmwater = "";
                    t_pyknoempty = "";
                    t_pyknosample = "";
                }
                else if (labdeviceradiopykno.Checked == true)
                {
                    t_temp = int.Parse(txtPyknotemperature.Text.ToString());
                    t_labdevice = 1;
                    t_indication = "";
                    t_pyknodmwater = txtPyknometerdmwater.Text.ToString();
                    t_pyknoempty = txtPyknometerempty.Text.ToString();
                    t_pyknosample = txtPyknometersample.Text.ToString();
                }

                if (parameterpasses.Checked == true)
                {
                    t_parametertest = 0;
                    final_parameter_text = "";
                }
                else if (parameterpassesbyvalue.Checked == true)
                {
                    t_parametertest = 1;
                    checked_para = "";
                }

                if (testresultpassed.Checked == true)
                {
                    t_parameterresult = 0;
                    //t_test_remarks = "";
                    t_test_remarks = txtTestRemarks.Text.ToString(); 
                }
                else if (testresultnotpassed.Checked == true)
                {
                    t_parameterresult = 1;
                    t_test_remarks = txtTestRemarks.Text.ToString();
                }

                //step 1 : update report
                var query1 = $@"SELECT * FROM exciseautomation.tab_quant_id_report WHERE quant_received_id={qid_int} LIMIT 1";
                using (var command = GetSqlCommand(query1))
                {
                    command.Connection.Open();
                    NpgsqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        string query3 = $@"UPDATE exciseautomation.tab_quant_id_report SET indication='{t_indication}',temperature={t_temp},proof_type='{t_proof_type}',proof_strength={t_proof_str},color='{t_color}',smell='{t_smell}',remarks='{t_remarks}' WHERE quant_received_id={qid_int}";
                        using (var command2 = GetSqlCommand(query3))
                        {
                            command2.Connection.Open();
                            command2.ExecuteNonQuery();
                            command2.Connection.Close();
                        }
                    }
                    else
                    {
                        string query3 = $@"INSERT INTO exciseautomation.tab_quant_id_report (quant_received_id,indication,temperature,proof_type,proof_strength,color,smell,remarks,form_no) VALUES ('{qid_int}','{t_indication}','{t_temp}','{t_proof_type}','{t_proof_str}','{t_color}','{t_smell}','{t_remarks}','{form_no_int}')";
                        using (var command2 = GetSqlCommand(query3))
                        {
                            command2.Connection.Open();
                            command2.ExecuteNonQuery();
                            command2.Connection.Close();
                        }
                    }
                    command.Connection.Close();
                }

                //step 2 : update report_parameter
                var query2 = $@"SELECT * FROM exciseautomation.tab_quant_id_report_parameter WHERE quant_received_id={qid_int} LIMIT 1";
                using (var command = GetSqlCommand(query2))
                {
                    command.Connection.Open();
                    NpgsqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        string query3 = $@"UPDATE exciseautomation.tab_quant_id_report_parameter SET labdevice = {t_labdevice}, indication = '{t_indication}', temperature = {t_temp}, pyknometerempty = '{t_pyknoempty}', pyknometerdmwater = '{t_pyknodmwater}', pyknometersample = '{t_pyknosample}', parametertest = {t_parametertest}, parameter_ids = '{assigned_para}', parameter_checked = '{checked_para}', parameter_text = '{final_parameter_text}', parametertestresult = {t_parameterresult}, testremarks = '{t_test_remarks}' WHERE quant_received_id={qid_int}";
                        using (var command2 = GetSqlCommand(query3))
                        {
                            command2.Connection.Open();
                            command2.ExecuteNonQuery();
                            command2.Connection.Close();
                        }
                    }
                    else
                    {
                        string query4 = $@"INSERT INTO exciseautomation.tab_quant_id_report_parameter (quant_received_id, form_no, labdevice, indication, temperature, pyknometerempty, pyknometerdmwater, pyknometersample, parametertest, parameter_ids, parameter_checked, parameter_text,parametertestresult,testremarks) VALUES ('{qid_int}','{form_no}', '{t_labdevice}','{t_indication}','{t_temp}', '{t_pyknoempty}', '{t_pyknodmwater}', '{t_pyknosample}', '{t_parametertest}', '{assigned_para}', '{checked_para}', '{final_parameter_text}', '{t_parameterresult}', '{t_test_remarks}')";
                        using (var command2 = GetSqlCommand(query4))
                        {
                            command2.Connection.Open();
                            command2.ExecuteNonQuery();
                            command2.Connection.Close();
                        }
                    }
                    command.Connection.Close();
                }

                //step 3 : update form_no status
                var query6 = $@"select * from exciseautomation.tab_form_no_status where form_no='{form_no}' limit 1";
                using (var command = GetSqlCommand(query6))
                {
                    command.Connection.Open();
                    NpgsqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        int quantity_count=-1, quantity_tested=-1, quantity_untested=-1, quantity_retest=-1, quantity_verified=-1, quantity_status_overall=-1;
                        while (dr.Read())
                        {
                            quantity_count = dr.GetInt32(dr.GetOrdinal("quantity_count"));
                            quantity_untested = dr.GetInt32(dr.GetOrdinal("quantity_untested"));
                            quantity_tested = dr.GetInt32(dr.GetOrdinal("quantity_tested"));
                            quantity_retest = dr.GetInt32(dr.GetOrdinal("quantity_retest"));
                            quantity_verified = dr.GetInt32(dr.GetOrdinal("quantity_verified"));
                            quantity_status_overall = dr.GetInt32(dr.GetOrdinal("quantity_status_overall"));
                        }
                        if (mode == "untested")
                        {
                            if (quantity_untested > 1) quantity_status_overall = 3;
                            else if (quantity_untested == 1 && quantity_retest >= 1) quantity_status_overall = 3;
                            else if (quantity_untested == 1 && quantity_retest == 0) quantity_status_overall = 1;
                            string query7 = $@"update exciseautomation.tab_form_no_status SET quantity_status_overall = '{quantity_status_overall}' , quantity_untested = quantity_untested-1 , quantity_tested = quantity_tested+1 where form_no = '{form_no}'";
                            using (var command2 = GetSqlCommand(query7))
                            {
                                command2.Connection.Open();
                                command2.ExecuteNonQuery();
                                command2.Connection.Close();
                            }
                        }
                        else if (mode == "retest")
                        {
                            if(quantity_retest>1) quantity_status_overall = 3;
                            else if(quantity_retest==1 && quantity_untested>=1) quantity_status_overall = 3;
                            else if(quantity_retest==1 && quantity_untested==0) quantity_status_overall = 1;
                            string query7 = $@"update exciseautomation.tab_form_no_status SET quantity_status_overall = '{quantity_status_overall}' , quantity_retest = quantity_retest-1 , quantity_tested = quantity_tested+1 where form_no = '{form_no}'";
                            using (var command2 = GetSqlCommand(query7))
                            {
                                command2.Connection.Open();
                                command2.ExecuteNonQuery();
                                command2.Connection.Close();
                            }
                        }
                    }
                    command.Connection.Close();
                }


                //step 4 : update status of quant received
                string query5 = $@"update exciseautomation.tab_quant_received set status='tested' where quant_received_id={qid_int}";
                using (var command = GetSqlCommand(query5))
                {
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
                string fn = Request.QueryString["form_no"];
                Response.Redirect($"~/LabTechnicianList.aspx");
            }
            catch (Exception ex)
            {
                Response.Write("Contact Admin : " + ex.Message.ToString());
                populate();
            }
        }



        private void populate2()
        {
            form_no = Request.QueryString["form_no"];
            form_no_int = int.Parse(form_no);
            qid = Request.QueryString["qid"];
            qid_int = int.Parse(qid);
            mode = Request.QueryString["status"];
            try
            {
                var query = $@"SELECT
                            A.quant_received_id,
                            A.quantity,
                            A.batch_no,
                            A.address,
                            A.compactor_id,
                            A.status,
                            B.liq_type,
                            B.liq_id,
                            C.liq_sub_type_id,
                            C.liq_sub_type_name,
                            D.size_name,
                            E.brand_name,
                            F.form_no,
                            G.date_of_creation,
                            H.proof_strength,
                            H.proof_type,
                            H.color,
                            H.smell,
                            H.remarks,
                            I.parametertestresult,
                            I.labdevice,
                            I.indication,
                            I.temperature,
                            I.pyknometerempty, 
                            I.pyknometerdmwater,
                            I.pyknometersample,
                            I.parametertest,
                            I.parameter_ids,
                            I.parameter_checked,
                            I.parameter_text,
                            I.testremarks
                           FROM
                            exciseautomation.tab_quant_received A
                            LEFT JOIN exciseautomation.tab_liq_type B ON A.liq_type_id=B.liq_id
                            LEFT JOIN exciseautomation.tab_liq_subtype C ON A.sub_type_id=C.liq_sub_type_id
                            LEFT JOIN exciseautomation.tab_liq_size D ON A.size_id=D.size_id
                            LEFT JOIN exciseautomation.tab_liq_brand_name E ON A.brand_name_id=E.brand_id
                            LEFT JOIN exciseautomation.tab_quant_form_no F ON A.quant_received_id=F.quant_received_id
                            LEFT JOIN exciseautomation.tab_form_no_date G ON F.form_no = G.form_no
                            LEFT JOIN exciseautomation.tab_quant_id_report H ON A.quant_received_id = H.quant_received_id
                            LEFT JOIN exciseautomation.tab_quant_id_report_parameter I ON A.quant_received_id = I.quant_received_id
                           WHERE
                            A.quant_received_id={qid_int}";

                using (var command = GetSqlCommand(query))
                {
                    command.Connection.Open();
                    NpgsqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            quantity = dr.GetString(dr.GetOrdinal("quantity"));
                            batch_no = dr.GetString(dr.GetOrdinal("batch_no"));
                            address = dr.GetString(dr.GetOrdinal("address"));
                            status = dr.GetString(dr.GetOrdinal("status"));
                            compactor_id = dr.GetInt32(dr.GetOrdinal("compactor_id"));
                            liq_id = dr.GetInt32(dr.GetOrdinal("liq_id"));
                            liq_sub_type_id = dr.GetInt32(dr.GetOrdinal("liq_sub_type_id"));
                            liq_type = dr.GetString(dr.GetOrdinal("liq_type"));
                            liq_sub_type_name = dr.GetString(dr.GetOrdinal("liq_sub_type_name"));
                            size_name = dr.GetString(dr.GetOrdinal("size_name"));
                            brand_name = dr.GetString(dr.GetOrdinal("brand_name"));
                            proof_type = dr.GetString(dr.GetOrdinal("proof_type"));
                            smell = dr.GetString(dr.GetOrdinal("smell"));
                            remarks = dr.GetString(dr.GetOrdinal("remarks"));
                            proof_strength = dr.GetFloat(dr.GetOrdinal("proof_strength"));
                            DateTime creationDate = dr.GetDateTime(dr.GetOrdinal("date_of_creation"));
                            date_of_creation = creationDate.ToString();
                            formanddate = form_no + '/' + date_of_creation;
                        }
                    }
                    command.Connection.Close();
                }

            }

            catch (Exception ex)
            {
                Response.Write("Contact Admin : " + ex.Message.ToString());
            }
        }

        public void hide()
        {
            string status = Request.QueryString["status"];
            if (status == "retest")
            {
                btnSave.Visible = false;
                btnVerify.Visible = true;
            }else if (status == "untested")
            {
                btnSave.Visible = true;
                btnVerify.Visible = false;
            }
        }
    }
}