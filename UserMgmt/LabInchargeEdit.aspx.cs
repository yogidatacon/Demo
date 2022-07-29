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
    public partial class LabInchargeEdit : System.Web.UI.Page
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
        public int assign_parameter_id;
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

        }
        public NpgsqlCommand GetSqlCommand(string commandText, CommandType commandType = CommandType.Text)
        {
            var sqlCommand = new NpgsqlCommand(commandText, GetSqlConnection())
            {
                CommandType = commandType
            };
            return sqlCommand;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            form_no = "";
            qid = "";
            mode = "";
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
            mode = Request.QueryString["mode"];
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
                var query2 = $@"SELECT parameter_master_id, parameter_master_name FROM exciseautomation.parameter_master order by parameter_master_name asc ";
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
                var Assignparameter = $@"SELECT * FROM assign_parameter WHERE liquor_sub_type_id='{liq_sub_type_id}' AND type_of_liquor_id='{liq_id}'";
                using (var command = GetSqlCommand(Assignparameter))
                {
                    command.Connection.Open();
                    NpgsqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            assign_parameter_id = dr.GetInt32(dr.GetOrdinal("assign_parameter_id"));

                        }
                    }
                    command.Connection.Close();
                }

                //var query3 = $@"SELECT * FROM exciseautomation.tab_parameter_assign WHERE liq_sub_type_name='{liq_sub_type_id}' AND liq_type='{liq_id}' LIMIT 1";
                var query3 = $@"SELECT * FROM exciseautomation.assign_parameter_assigned_list WHERE assign_parameter_id='{assign_parameter_id}'";
                using (var command = GetSqlCommand(query3))
                {
                    command.Connection.Open();
                    NpgsqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                           // int t_liq_para_id = dr.GetInt32(dr.GetOrdinal("liq_parameter_id"));
                            //assigned_parameter_str = dr.GetString(dr.GetOrdinal("assigned_parameter"));
                            int t_assigned_parameter = dr.GetInt32(dr.GetOrdinal("parameter_master_id")); //vishv
                            //if (assigned_parameter_str.Length != 0)
                            //{
                            //    assigned_para_list = assigned_parameter_str.Split(',').ToList<string>();
                            //}
                            //passes 
                            //for (int i = 0; i < assigned_para_list.Count; i++)
                            //{
                                int val = t_assigned_parameter;
                            
                                if (checked_para_list.Count > 0)
                                {
                                    //if (checked_para_list.Contains(assigned_para_list.ToString()))
                                    //{
                                        parameterchecklist.Add(new parameterChecklist(dict[val], true));
                                    //}
                                    //else
                                    //{
                                    //    parameterchecklist.Add(new parameterChecklist(dict[val], false));
                                    //}
                                }
                                else
                                {
                                    parameterchecklist.Add(new parameterChecklist(dict[val], false));
                                }
                            //}
                            //passes by value

                            //for (int i = 0; i < assigned_para_list.Count; i++)
                            //{
                                //int val = int.Parse(assigned_para_list[i]);
                                if (input_list.Count > assigned_para_list.Count)
                                {
                                    parametertextlist.Add(new parameterTextlist(dict[val], input_list[assigned_para_list.Count]));
                                }
                                else
                                {
                                    parametertextlist.Add(new parameterTextlist(dict[val], ""));
                                }
                            //}
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

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            form_no = Request.QueryString["form_no"];
            qid = Request.QueryString["qid"];
            mode = Request.QueryString["mode"];
            string res=validate();
            if (res == "")
            {
                update();
            }
            else
            {
                lblError.Text = res;
                lblError.Visible = true;
                populate2();
            }
        }

        public string validate()
        {
            string msg = "";
            if(labdeviceradiohydro.Checked==false && labdeviceradiopykno.Checked == false)
            {
                msg = "Please select one of laboratory device used for testing";
                return msg;
            }
            if (labdeviceradiohydro.Checked == true)
            {
                if (txtIndication.Text == "")
                {
                    msg = "Please fillup indication value";
                    return msg;
                }
                if(txtHydrotemp.Text == "")
                {
                    msg = "Please fillup hydrometer temperature value";
                    return msg;
                }
            }
            if (labdeviceradiopykno.Checked == true)
            {
                if (txtPyknometerdmwater.Text == "")
                {
                    msg = "Please fillup DM water with pyknometer weight value";
                    return msg;
                }
                if(txtPyknometerempty.Text == "")
                {
                    msg = "Please fillup empty pyknometer weight value";
                    return msg;
                }
                if (txtPyknometersample.Text == "")
                {
                    msg = "Please fillup Sample with pyknometer weight value";
                    return msg;
                }
                if (txtPyknotemperature.Text == "")
                {
                    msg = "Please fillup pyknometer temperature value";
                    return msg;
                }
            }

            if(parameterpasses.Checked==false && parameterpassesbyvalue.Checked == false)
            {
                msg = "Please select one of the parameter testing type";
                return msg;
            }

            if(parameterpasses.Checked==true && testresultpassed.Checked == true)
            {
                int rowcount = parametergrid1.Rows.Count;
                for (int i = 0; i < rowcount; i++)
                {
                    GridViewRow row = parametergrid1.Rows[i];
                    CheckBox cb = (CheckBox)row.FindControl("parameter_checkbox");
                    if (cb.Checked == false)
                    {
                        msg = "Please select right values for 'Passes the Test' parameter or select not passed";
                        return msg;
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
                        msg = "Please uncheck some values for 'Passes the Test' parameter or select passed";
                        return msg;
                    }
                }
            }

            if(parameterpassesbyvalue.Checked==true && testresultpassed.Checked == true)
            {
                int rowcount2 = parametergrid2.Rows.Count;
                for (int i = 0; i < rowcount2; i++)
                {
                    GridViewRow row = parametergrid2.Rows[i];
                    TextBox tb = (TextBox)row.FindControl("parameter_textbox");
                    if (tb.Text.ToString() == "")
                    {
                        msg = "Please enter all the values in Parameter Details section under the Passes the Test by value";
                        return msg;
                    }               
                }
            }

            if (parameterpassesbyvalue.Checked == true && testresultnotpassed.Checked == true)
            {
                int rowcount2 = parametergrid2.Rows.Count;
                for (int i = 0; i < rowcount2; i++)
                {
                    GridViewRow row = parametergrid2.Rows[i];
                    TextBox tb = (TextBox)row.FindControl("parameter_textbox");
                    if (tb.Text.ToString() == "")
                    {
                        msg = "Please enter all the values in Parameter Details section under the Passes the Test by value";
                        return msg;
                    }
                }
            }

            if(testresultpassed.Checked==false && testresultnotpassed.Checked == false)
            {
                msg = "Please select final test result Passed or Not Passed";
                return msg;
            }

            if(testresultnotpassed.Checked==true && txtTestRemarks.Text.ToString() == "")
            {
                msg = "Please fillup final not passed test remark value";
                return msg;
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
                mode = Request.QueryString["mode"];
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
                string t_proof_type = proofDropdown.SelectedValue.ToString();
                float t_proof_str = float.Parse(txtProofstr.Text.ToString());
                string t_color = colorDropdown.SelectedValue.ToString();
                string t_smell = txtSmell.Text.ToString();
                string t_remarks = txtRemarks.Text.ToString();                  //Vishv20211028
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
                    //t_test_remarks = ""; //remark down getting null vishv
                    t_test_remarks = txtTestRemarks.Text.ToString();
                }
                else if (testresultnotpassed.Checked == true)
                {
                    t_parameterresult = 1;
                    t_test_remarks = txtTestRemarks.Text.ToString();
                }

                //Response.Write("Proof str:" + t_proof_str);
                //step 1
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

                //step 2
                var query2 = $@"SELECT * FROM exciseautomation.tab_quant_id_report_parameter WHERE quant_received_id={qid_int} LIMIT 1";
                using (var command = GetSqlCommand(query2))
                {
                    command.Connection.Open();
                    NpgsqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        string query3 = $@"UPDATE exciseautomation.tab_quant_id_report_parameter SET labdevice = {t_labdevice}, indication = '{t_indication}', temperature = {t_temp}, pyknometerempty = '{t_pyknoempty}', pyknometerdmwater = '{t_pyknodmwater}', pyknometersample = '{t_pyknosample}', parametertest = {t_parametertest}, parameter_ids = '{assigned_parameter_str}', parameter_checked = '{checked_para}', parameter_text = '{final_parameter_text}', parametertestresult = {t_parameterresult}, testremarks = '{t_test_remarks}' WHERE quant_received_id={qid_int}";
                        using (var command2 = GetSqlCommand(query3))
                        {
                            command2.Connection.Open();
                            command2.ExecuteNonQuery();
                            command2.Connection.Close();
                        }
                    }
                    else
                    {
                        string query4 = $@"INSERT INTO exciseautomation.tab_quant_id_report_parameter (quant_received_id, form_no, labdevice, indication, temperature, pyknometerempty, pyknometerdmwater, pyknometersample, parametertest, parameter_ids, parameter_checked, parameter_text,parametertestresult,testremarks) VALUES ('{qid_int}','{form_no}', '{t_labdevice}','{t_indication}','{t_temp}', '{t_pyknoempty}', '{t_pyknodmwater}', '{t_pyknosample}', '{t_parametertest}', '{assigned_parameter_str}', '{checked_para}', '{final_parameter_text}', '{t_parameterresult}', '{t_test_remarks}')";
                        using (var command2 = GetSqlCommand(query4))
                        {
                            command2.Connection.Open();
                            command2.ExecuteNonQuery();
                            command2.Connection.Close();
                        }
                    }
                    command.Connection.Close();
                }

                //step 3
                string query5 = $@"update exciseautomation.tab_quant_received set status='tested' where quant_received_id={qid_int}";
                using (var command = GetSqlCommand(query5))
                {
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                } 
                string fn = Request.QueryString["form_no"];
                Response.Redirect($"~/LabInchargeView.aspx?form_no={fn}"); 
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
            mode = Request.QueryString["mode"];
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
    }
}