using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL.MasterData;
using Usermngt.BL.ReceivingSection;
using Usermngt.BL.Service;
using Usermngt.Entities;
using Usermngt.BL.DataUtility;
using System.Globalization;

namespace UserMgmt
{
    public partial class ReceivingSectionForm : Page
    {
        #region private fields
        private readonly IReceivingSectionService _receivingSectionService = new ReceivingSectionProvider();
        private readonly IMasterDataService _masterDataService = new MasterDataProvider();
        #endregion

        [Serializable]
        public class QuantItemStr
        {
            public long id { get; set; }
            public string type_of_liquor_name { get; set; }
            public string liquor_sub_name { get; set; }
            public string size_master_name { get; set; }
            public string quantity { get; set; }
            public string brand_master_name { get; set; }
            public string batch_no { get; set; }
            public string address { get; set; }
            public string compactor_name { get; set; }

            public QuantItemStr(long id,string liqName, string liqSubName, string liqSize, string liqQuantity, string liqBrand, string liqBatch, string liqAdd, string liqComp)
            {
                this.id = id;
                this.type_of_liquor_name = liqName;
                this.liquor_sub_name = liqSubName;
                this.size_master_name = liqSize;
                this.quantity = liqQuantity;
                this.brand_master_name = liqBrand;
                this.batch_no = liqBatch;
                this.address = liqAdd;
                this.compactor_name = liqComp;
            }

            public QuantItemStr()
            {

            }

        }

        [Serializable]
        public class QuantItemInt
        {
            public long id { get; set; }
            public int type_of_liquor_id { get; set; }
            public int liquor_sub_id { get; set; }
            public int size_master_id { get; set; }
            public string quantity { get; set; }
            public int brand_master_id { get; set; }
            public string batch_no { get; set; }
            public string address { get; set; }
            public int compactor_id { get; set; }

            public QuantItemInt(long id, int liqId, int liqSubId, int liqSizeId, string liqQuantity, int liqBrandId, string liqBatch, string liqAdd, int liqCompId)
            {
                this.id = id;
                this.type_of_liquor_id = liqId;
                this.liquor_sub_id = liqSubId;
                this.size_master_id = liqSizeId;
                this.quantity = liqQuantity;
                this.brand_master_id = liqBrandId;
                this.batch_no = liqBatch;
                this.address = liqAdd;
                this.compactor_id = liqCompId;
            }

            public QuantItemInt()
            {

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

        public NpgsqlCommand GetSqlCommandWithTransaction(string commandText, NpgsqlTransaction transaction, CommandType commandType = CommandType.Text)
        {
            var sqlCommand = new NpgsqlCommand(commandText, transaction.Connection, transaction)
            {
                CommandType = commandType
            };
            return sqlCommand;
        }

        List<QuantItemStr> quantList = new List<QuantItemStr>();
        List<QuantItemInt> quantIntList = new List<QuantItemInt>();
        //Bhavin
        List<QuantItemStr> tempquantList = new List<QuantItemStr>();
        //End

        int btnStatus = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (ViewState["btnStatus"] != null)
            {
                btnStatus = (int)ViewState["btnStatus"];
            }
            if (btnStatus!=-1)
            {
                btnAdd.Text = "Update";
            }
            else
            {
                btnAdd.Text = "Add";
            }

            if (ViewState["QuantList"] != null)
            {
                quantList = (List<QuantItemStr>)ViewState["QuantList"];
                quantIntList = (List<QuantItemInt>)ViewState["QuantIntList"];
            }

            if (Session.Keys.Count == 0)
                Response.Redirect("~/LoginPage.aspx");
            if (!IsPostBack)
            {
                rdType.SelectedValue = "police";
                panPolice.Visible = true;
                panDocumentReceivingChecklist.Visible = true;
                panDistillery.Visible = false;
                panExcise.Visible = false;
                txtCourtOrder.Visible = false;
                lblTodayDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                txtSeizureList.Visible = false;
                exciseDocumentReceivingChecklist.Visible = false;
                txtSealed.Visible = false;
                dvStatus.Visible = false;
                LoadDistrics();
                LoadLiquor();
                LoadSize();
                LoadBrand();
                LoadCompactor();
                LoadDropdownsOnTypeChange();
                /*
                hdnIsSavedAlready.Value = bool.FalseString;
                if (Request.QueryString.AllKeys.Contains("QMode"))
                {
                    var receivingSectionId = Request.QueryString.Get("QId");
                    var exhibitFrom = Request.QueryString.Get("QExhibitfrom");
                    LoadDetails(receivingSectionId, exhibitFrom);
                }
                */
            }
            else
            {

            }
        }

        protected void grdQuantList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit_Record")
            {
                btnAdd.Text = "Update";
                var inputParams = e.CommandArgument.ToString().Split(',');
                var tmp_id = inputParams.Length > 0 ? inputParams[0] : default(string);
                long tmp_id_long = long.Parse(tmp_id.ToString());
                //Bhavin
                ViewState["tmp_id"] = tmp_id_long;
                //End

                for (int i = 0; i < quantList.Count; i++)
                {
                    if (quantList[i].id == tmp_id_long)
                    {
                        ddlTypeOfLiquor.SelectedValue = quantIntList[i].type_of_liquor_id.ToString();
                        LoadSubLiquorType(quantIntList[i].type_of_liquor_id);
                        ddlQuantitySubType.SelectedValue = quantIntList[i].liquor_sub_id.ToString();
                        ddlQuantitySize.SelectedValue = quantIntList[i].size_master_id.ToString();
                        txtQuantity.Text = quantIntList[i].quantity;
                        ddlBrandName.SelectedValue = quantIntList[i].brand_master_id.ToString();
                        txtBatchNo.Text = quantIntList[i].batch_no;
                        txtAddressOfMan.Text = quantIntList[i].address;
                        ddlCompactor.SelectedValue = quantIntList[i].compactor_id.ToString();
                        btnStatus = i;
                        ViewState["btnStatus"] = btnStatus;
                        break;
                    }
                }
            }
            if(e.CommandName == "Delete_Record")
            {
                var inputParams = e.CommandArgument.ToString().Split(',');
                var tmp_id = inputParams.Length > 0 ? inputParams[0] : default(string);
                long tmp_id_long = long.Parse(tmp_id.ToString());
                for(int i = 0; i < quantList.Count; i++)
                {
                    if(quantList[i].id == tmp_id_long)
                    {
                        quantList.RemoveAt(i);
                        quantIntList.RemoveAt(i);
                        break;
                    }
                }
                //ReLoad GridView
                grdQuantList.DataSource = quantList;
                grdQuantList.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //Bhavin
            bool flag = false;
            if (btnStatus == -1)
            {
                
                if (ViewState["QuantList"] != null)
                {
                    tempquantList = (List<QuantItemStr>)ViewState["QuantList"];

                    for (int i =0; i < tempquantList.Count(); i++)
                    {
                        if (tempquantList[i].type_of_liquor_name == ddlTypeOfLiquor.SelectedItem.Text && tempquantList[i].liquor_sub_name == ddlQuantitySubType.SelectedItem.Text && tempquantList[i].size_master_name == ddlQuantitySize.SelectedItem.Text && tempquantList[i].quantity.ToUpper() == txtQuantity.Text.ToUpper() && tempquantList[i].brand_master_name == ddlBrandName.SelectedItem.Text && tempquantList[i].batch_no == txtBatchNo.Text && tempquantList[i].address.ToUpper() == txtAddressOfMan.Text.ToUpper())
                        {
                            flag = true;
                            break;

                         //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert();", true);
                        }
                    }

                    if (flag == true)
                    {
                        //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert();", true);
                        // ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "updatealert", @"&lt;script language='javascript'>setTimeout(""alert('Record already Exist')"",500); </script>"); 
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Record already Exist ')</script>");
                        return;
                    }
                }
               

                if (Session["Compactor"] == null)
                {
                    Session["Compactor"] = ddlCompactor.SelectedItem.Text.ToString();
                }
                else
                {
                    if (Session["Compactor"].ToString() != ddlCompactor.SelectedItem.Text.ToString())
                    {
                        //if (Session["Compactor"] != null)
                        //{
                        //    ddlCompactor.SelectedItem.Text = Session["Compactor"].ToString();
                        //}

                        ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Compactor must be same')</script>");
                        return;
                    }
                }

                //End

                DateTime foo = DateTime.Now;
                long unixTime = ((DateTimeOffset)foo).ToUnixTimeSeconds();
                string tmpLiqType = ddlTypeOfLiquor.SelectedItem.Text.ToString();
                int tmpLiqId = Convert.ToInt32(ddlTypeOfLiquor.SelectedValue);
                string tmpLiqSubType = ddlQuantitySubType.SelectedItem.Text.ToString();
                int tmpLiqSubId = Convert.ToInt32(ddlQuantitySubType.SelectedValue);
                string tmpLiqSize = ddlQuantitySize.SelectedItem.Text.ToString();
                int tmpLiqSizeId = Convert.ToInt32(ddlQuantitySize.SelectedValue);
                string tmpQuantity = txtQuantity.Text.ToString();
                string tmpLiqBrand = ddlBrandName.SelectedItem.Text.ToString();
                int tmpLiqBrandId = Convert.ToInt32(ddlBrandName.SelectedValue);
                string tmpLiqBatch = txtBatchNo.Text.ToString();
                string tmpLiqAdd = txtAddressOfMan.Text.ToString();
                string tmpLiqComp = ddlCompactor.SelectedItem.Text.ToString();
                int tmpLiqCompId = Convert.ToInt32(ddlCompactor.SelectedValue);
                quantList.Add(new QuantItemStr(unixTime, tmpLiqType, tmpLiqSubType, tmpLiqSize, tmpQuantity, tmpLiqBrand, tmpLiqBatch, tmpLiqAdd, tmpLiqComp));
                quantIntList.Add(new QuantItemInt(unixTime, tmpLiqId, tmpLiqSubId, tmpLiqSizeId, tmpQuantity, tmpLiqBrandId, tmpLiqBatch, tmpLiqAdd, tmpLiqCompId));
                ViewState["QuantList"] = quantList;
                ViewState["QuantIntList"] = quantIntList;
                grdQuantList.DataSource = quantList;
                grdQuantList.DataBind();
            }
            else
            {
                //Bhavin
                long tmpid = (long)ViewState["tmp_id"];

                tempquantList = (List<QuantItemStr>)ViewState["QuantList"];

                for (int i = 0; i < tempquantList.Count(); i++)
                {
                    if (tmpid != tempquantList[i].id)
                    {
                        if (tempquantList[i].type_of_liquor_name == ddlTypeOfLiquor.SelectedItem.Text && tempquantList[i].liquor_sub_name == ddlQuantitySubType.SelectedItem.Text && tempquantList[i].size_master_name == ddlQuantitySize.SelectedItem.Text && tempquantList[i].quantity.ToUpper() == txtQuantity.Text.ToUpper() && tempquantList[i].brand_master_name == ddlBrandName.SelectedItem.Text && tempquantList[i].batch_no == txtBatchNo.Text && tempquantList[i].address.ToUpper() == txtAddressOfMan.Text.ToUpper())
                        {
                            flag = true;
                            break;

                            //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "showAlert();", true);
                        }
                    }
                }

                if (flag == true)
                {
                 
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Record already Exist ')</script>");
                    return;
                }

                if (tempquantList.Count() == 1)
                {
                    if (Session["Compactor"] != null)
                    {
                        Session["Compactor"] = ddlCompactor.SelectedItem.Text.ToString();
                    }
                }
                else
                {
                    if (Session["Compactor"].ToString() != ddlCompactor.SelectedItem.Text.ToString())
                    {
                        //if (Session["Compactor"] != null)
                        //{
                        //    ddlCompactor.SelectedItem.Text = Session["Compactor"].ToString();
                        //}

                        ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Compactor must be same')</script>");
                        return;
                    }
                }

                //End

                int index = btnStatus;
                quantList[index].type_of_liquor_name = ddlTypeOfLiquor.SelectedItem.Text.ToString();
                quantList[index].liquor_sub_name = ddlQuantitySubType.SelectedItem.Text.ToString();
                quantList[index].size_master_name = ddlQuantitySize.SelectedItem.Text.ToString();
                quantList[index].quantity = txtQuantity.Text.ToString();
                quantList[index].brand_master_name = ddlBrandName.SelectedItem.Text.ToString(); 
                quantList[index].batch_no = txtBatchNo.Text.ToString();
                quantList[index].address = txtAddressOfMan.Text.ToString();
                quantList[index].compactor_name = ddlCompactor.SelectedItem.Text.ToString();

                quantIntList[index].type_of_liquor_id = Convert.ToInt32(ddlTypeOfLiquor.SelectedValue); ;
                quantIntList[index].liquor_sub_id = Convert.ToInt32(ddlQuantitySubType.SelectedValue); ;
                quantIntList[index].size_master_id = Convert.ToInt32(ddlQuantitySize.SelectedValue); ;
                quantIntList[index].quantity = txtQuantity.Text.ToString();
                quantIntList[index].brand_master_id = Convert.ToInt32(ddlBrandName.SelectedValue);
                quantIntList[index].batch_no = txtBatchNo.Text.ToString();
                quantIntList[index].address = txtAddressOfMan.Text.ToString();
                quantIntList[index].compactor_id = Convert.ToInt32(ddlCompactor.SelectedValue);

                btnStatus = -1;
                ViewState["btnStatus"] = btnStatus;                
                ViewState["QuantList"] = quantList;
                ViewState["QuantIntList"] = quantIntList;
                grdQuantList.DataSource = quantList;
                grdQuantList.DataBind();

                btnAdd.Text = "Add";
            }
            reset();
        }

        public void reset()
        {
            ddlTypeOfLiquor.SelectedValue = "Select";
            ddlQuantitySubType.SelectedValue = "Select";
            ddlQuantitySize.SelectedValue = "Select";
            txtQuantity.Text = "";
            ddlBrandName.SelectedValue = "Select";
            txtBatchNo.Text = "";
            txtAddressOfMan.Text = "";
            ddlCompactor.SelectedValue = "Select";
            //if (Session["Compactor"] != null)
            //{
            //    ddlCompactor.SelectedItem.Text = Session["Compactor"].ToString();
            //}
        }


        public int GetQuantRecId()
        {
            string query = @"select max(quant_received_id) as quant_received_id from exciseautomation.tab_quant_received";
            int Id = 0;

            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Id = dr.GetInt32(dr.GetOrdinal("quant_received_id"));
                    }
                }
                command.Connection.Close();
            }
            //Id = Id + 1;
            return Id;

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {    
            //Bhavin
            int no = 0;
            for (int i = 0; i < quantIntList.Count; i++)
            {               
                if (i == 0)
                {
                    no = quantIntList[i].compactor_id;
                }
                else
                {
                    if (no != quantIntList[i].compactor_id)
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Compactor must be same.')</script>");
                        return;
                    }
                }
            }
            //End
           
            int f_no = -1;
            if (quantIntList.Count == 0)
            {
                string script = "<script type=\"text/javascript\">alert('At one Item should be entered.');</script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script);
                return;
            }
            int depart_id = 1;
            if (rdType.SelectedValue == "police") depart_id = 1;
            else if (rdType.SelectedValue == "excise") depart_id = 2;
            else if (rdType.SelectedValue == "distillery") depart_id = 3;

            using (var connection = GetSqlConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    string query1 = $@"INSERT INTO exciseautomation.tab_form_no_depart(depart_id) values ('{depart_id}') returning form_no";
                    using (var command = GetSqlCommandWithTransaction(query1, transaction))
                    {
                        //update form_no_depart
                        int form_no = (int)command.ExecuteScalar();
                        f_no = form_no;
                        //update tab_letter_no_form_no
                        //Bhavin
                        string letter_no = "";
                        DateTime? letter_date = null;
                        if (txtlettername.Text != "" && txtlettername.Text != null)
                        {
                             letter_no = txtlettername.Text.ToString();
                        }
                        //End
                        //Bhavin
                        //DateTime letter_date = Convert.ToDateTime(txtLetterDate.Text);
                        if (txtLetterDate.Text != "" && txtLetterDate.Text != null)
                        {
                            letter_date = Convert.ToDateTime(txtLetterDate.Text); //DateTime.ParseExact(txtLetterDate.Text, "MM/dd/yyyy", null);
                        }
                        //End

                        string is_sealed = "no";
                        if (ddlSealed.SelectedValue == "True")
                        {
                            is_sealed = "yes";
                        }
                        else if (ddlSealed.SelectedValue == "False")
                        {
                            is_sealed = "no";
                        }
                        string issealed_text = txtSealed.Text.ToString();

                        string tmp1 = "insert into exciseautomation.tab_letter_no_form_no(form_no, letter_number, letter_date, \"Sealed_details\", \"Sealed_details_desc\") values ";
                        //Bhavin
                        string tmp2 = "";
                        if (letter_date == null || letter_date.ToString() == "")
                        {
                            tmp2 = $@"('{form_no}','{letter_no}',null,'{is_sealed}','{issealed_text}')";
                        }
                        else
                        {
                            tmp2 = $@"('{form_no}','{letter_no}','{letter_date}','{is_sealed}','{issealed_text}')";
                        }
                        //End
                        //string query2 = $@"insert into exciseautomation.tab_letter_no_form_no (form_no,letter_number,letter_date,""Sealed_details"",""Sealed_details_desc"") values ('{form_no}','{letter_no}','{letter_date}','{is_sealed}','{issealed_text}')";
                        string query2 = tmp1 + tmp2;
                        using (var command2 = GetSqlCommandWithTransaction(query2, transaction))
                        {
                            command2.CommandText = query2;
                            command2.ExecuteNonQuery();
                        }

                        //update tab_form_no_date
                        DateTime curdate = DateTime.Now;
                        string query3 = $@"insert into exciseautomation.tab_form_no_date (form_no,date_of_creation,emailsend,smssend) values ('{form_no}','{curdate}','0','0')";
                        using (var command3 = GetSqlCommandWithTransaction(query3, transaction))
                        {
                            command3.CommandText = query3;
                            command3.ExecuteNonQuery();
                        }

                        if (depart_id == 1)
                        {
                            //police info
                            int pol_type = Convert.ToInt32(ddTypePolice.SelectedValue.ToString());
                             int dist_id = Convert.ToInt32(ddlDistrictPolice.SelectedValue.ToString());
                          // string dist_id =ddlDistrictPolice.SelectedValue.ToString();
                            int thana_id = Convert.ToInt32(ddThana.SelectedValue.ToString());
                            int desg_id = Convert.ToInt32(ddlPoliceOfficerDesignation.SelectedValue.ToString());
                            string fir_no = txtFIRNo.Text.ToString();

                            //Bhavin
                           
                           // DateTime fir_date = Convert.ToDateTime(txtDateOfInstitutionOfFIR.Text);
                           DateTime fir_date = DateTime.ParseExact(txtDateOfInstitutionOfFIR.Text, "MM/dd/yyyy", null);
                            //End

                            string query_p1 = $@"insert into exciseautomation.tab_police_info(form_no,type_id,thana_id,desg_id,fir_no,date_of_fir) values('{form_no}','{pol_type}','{thana_id}','{desg_id}','{fir_no}','{fir_date}')";
                            using (var command_p1 = GetSqlCommandWithTransaction(query_p1, transaction))
                            {
                                command_p1.CommandText = query_p1;
                                command_p1.ExecuteNonQuery();
                            }

                            //police doc
                            string court_order="no", fir_copy="no", seizure_list="no";
                            if (ddlCourtOrder.SelectedValue == "True") court_order = "yes";
                            if (ddlFirCopy.SelectedValue == "True") fir_copy = "yes";
                            if (ddlSeizureList.SelectedValue == "True") seizure_list = "yes";
                            string query_p2 = $@"insert into exciseautomation.tab_doc_pol (form_no,court_order,fir_copy,seizure_list) values ('{form_no}','{court_order}','{fir_copy}','{seizure_list}')";
                            using (var command_p2 = GetSqlCommandWithTransaction(query_p2, transaction))
                            {
                                command_p2.CommandText = query_p2;
                                command_p2.ExecuteNonQuery();
                            }

                            //quant received
                            for(int i = 0; i < quantIntList.Count; i++)
                            {
                                int liq_type_id = quantIntList[i].type_of_liquor_id;
                                int sub_type_id = quantIntList[i].liquor_sub_id;
                                int size_id = quantIntList[i].size_master_id;
                                string quantity = quantIntList[i].quantity;
                                int brand_id = quantIntList[i].brand_master_id;
                                string batch_no = quantIntList[i].batch_no;
                                string address = quantIntList[i].address;
                                int comp_id = quantIntList[i].compactor_id;

                                string query_p3 = $@"insert into exciseautomation.tab_quant_received(liq_type_id,sub_type_id,size_id,quantity,brand_name_id,batch_no,address,compactor_id,status,form_no) values ('{liq_type_id}','{sub_type_id}','{size_id}','{quantity}','{brand_id}','{batch_no}','{address}', '{comp_id}','untested','{form_no}')";
                                using (var command_p3 = GetSqlCommandWithTransaction(query_p3, transaction))
                                {
                                    command_p3.CommandText = query_p3;
                                    command_p3.ExecuteNonQuery();
                                }


                                //Bhavin
                                int quant_received_id = GetQuantRecId();
                                quant_received_id = quant_received_id + i + 1;

                                //string query_p4 = $@"insert into exciseautomation.tab_quant_form_no (form_no) values ('{form_no}')";
                                string query_p4 = $@"insert into exciseautomation.tab_quant_form_no (form_no,quant_received_id) values ('{form_no}','{quant_received_id}')";

                                using (var command_p4 = GetSqlCommandWithTransaction(query_p4, transaction))
                                {
                                    command_p4.CommandText = query_p4;
                                    command_p4.ExecuteNonQuery();
                                }
                            }
                            string query_a1 = $@"insert into exciseautomation.tab_form_no_status (form_no, quantity_count, quantity_status_overall, quantity_untested,quantity_tested,quantity_retest,quantity_verified) VALUES ('{form_no}', '{quantIntList.Count}', '3', '{quantIntList.Count}', '0', '0', '0')";
                            using (var command_a1 = GetSqlCommandWithTransaction(query_a1, transaction))
                            {
                                command_a1.CommandText = query_a1;
                                command_a1.ExecuteNonQuery();
                            }

                            //police districts
                            string query_p5 = $@"insert into exciseautomation.tab_form_no_dist_id (form_no,dist_id) values ('{form_no}','{dist_id}')";
                            using (var command_p5 = GetSqlCommandWithTransaction(query_p5, transaction))
                            {
                                command_p5.CommandText = query_p5;
                                command_p5.ExecuteNonQuery();
                            }

                        }
                        else if(depart_id == 2)
                        {
                            //excise info
                            int excise_type_id = Convert.ToInt32(ddlTypeExcise.SelectedValue.ToString());
                            int excise_desg_id = Convert.ToInt32(ddlExciseDesignation.SelectedValue.ToString());
                            string case_no = txtCaseNO.Text.ToString();

                            //Bhavin
                            //DateTime case_date = Convert.ToDateTime(txtExciseDateOfInstitutionOfFIR.Text);
                            DateTime case_date = DateTime.ParseExact(txtExciseDateOfInstitutionOfFIR.Text, "MM/dd/yyyy", null);
                            //End

                            string excise_remark = txtExiseRemark.Text.ToString();

                            string query_e1 = $@"insert into exciseautomation.tab_excise_info (form_no,type_id,desg_id,case_no,date_of_case,excise_remark) values ('{form_no}','{excise_type_id}','{excise_desg_id}','{case_no}','{case_date}','{excise_remark}')";
                            using (var command_e1 = GetSqlCommandWithTransaction(query_e1, transaction))
                            {
                                command_e1.CommandText = query_e1;
                                command_e1.ExecuteNonQuery();
                            }

                            //excise doc
                            string pr_no = txtPNRNO.Text.ToString();
                            string state_vs = txtState.Text.ToString();
                            string query_e2 = $@"insert into exciseautomation.tab_doc_excise (form_no,pr_no,state_vs) values ('{form_no}','{pr_no}','{state_vs}')";
                            using (var command_e2 = GetSqlCommandWithTransaction(query_e2, transaction))
                            {
                                command_e2.CommandText = query_e2;
                                command_e2.ExecuteNonQuery();
                            }

                            //quant received
                            for (int i = 0; i < quantIntList.Count; i++)
                            {
                                int liq_type_id = quantIntList[i].type_of_liquor_id;
                                int sub_type_id = quantIntList[i].liquor_sub_id;
                                int size_id = quantIntList[i].size_master_id;
                                string quantity = quantIntList[i].quantity;
                                int brand_id = quantIntList[i].brand_master_id;
                                string batch_no = quantIntList[i].batch_no;
                                string address = quantIntList[i].address;
                                int comp_id = quantIntList[i].compactor_id;

                                string query_p3 = $@"insert into exciseautomation.tab_quant_received(liq_type_id,sub_type_id,size_id,quantity,brand_name_id,batch_no,address,compactor_id,status,form_no) values ('{liq_type_id}','{sub_type_id}','{size_id}','{quantity}','{brand_id}','{batch_no}','{address}', '{comp_id}','untested','{form_no}')";
                                using (var command_p3 = GetSqlCommandWithTransaction(query_p3, transaction))
                                {
                                    command_p3.CommandText = query_p3;
                                    command_p3.ExecuteNonQuery();
                                }

                                //Bhavin
                                int quant_received_id = GetQuantRecId();
                                quant_received_id = quant_received_id + i + 1;

                                string query_p4 = $@"insert into exciseautomation.tab_quant_form_no (form_no,quant_received_id) values ('{form_no}','{quant_received_id}')";
                                // string query_p4 = $@"insert into exciseautomation.tab_quant_form_no (form_no) values ('{form_no}')";

                                using (var command_p4 = GetSqlCommandWithTransaction(query_p4, transaction))
                                {
                                    command_p4.CommandText = query_p4;
                                    command_p4.ExecuteNonQuery();
                                }
                            }
                            string query_a1 = $@"insert into exciseautomation.tab_form_no_status (form_no, quantity_count, quantity_status_overall, quantity_untested,quantity_tested,quantity_retest,quantity_verified) VALUES ('{form_no}', '{quantIntList.Count}', '3', '{quantIntList.Count}', '0', '0', '0')";
                            using (var command_a1 = GetSqlCommandWithTransaction(query_a1, transaction))
                            {
                                command_a1.CommandText = query_a1;
                                command_a1.ExecuteNonQuery();
                            }

                            //excise districts
                            int dist_id = Convert.ToInt32(ddlExciseDistrict.SelectedValue.ToString());
                            string query_p5 = $@"insert into exciseautomation.tab_form_no_dist_id (form_no,dist_id) values ('{form_no}','{dist_id}')";
                            using (var command_p5 = GetSqlCommandWithTransaction(query_p5, transaction))
                            {
                                command_p5.CommandText = query_p5;
                                command_p5.ExecuteNonQuery();
                            }
                        }
                        else if(depart_id == 3)
                        {
                            //distill info
                            int distill_id = Convert.ToInt32(dllDistilleryName.SelectedValue.ToString());
                            int distill_desg_id = Convert.ToInt32(ddlOfficerDesignation.SelectedValue.ToString());
                            string ref_no = txtVatNo.Text.ToString();
                            string remarks = txtRemark.Text.ToString();

                            //Bhavin
                            //DateTime den_date = Convert.ToDateTime(txtDenaturedDate.Text);
                            DateTime den_date = DateTime.ParseExact(txtDenaturedDate.Text, "MM/dd/yyyy", null);
                            //End

                            string query_d1 = $@"insert into exciseautomation.tab_distill_info (form_no,distill_id,desg_id,ref_no,distill_remark,distill_den_date) values ('{form_no}','{distill_id}','{distill_desg_id}','{ref_no}','{remarks}','{den_date}')";
                            using (var command_d1 = GetSqlCommandWithTransaction(query_d1, transaction))
                            {
                                command_d1.CommandText = query_d1;
                                command_d1.ExecuteNonQuery();
                            }

                            //quant received
                            for (int i = 0; i < quantIntList.Count; i++)
                            {
                                int liq_type_id = quantIntList[i].type_of_liquor_id;
                                int sub_type_id = quantIntList[i].liquor_sub_id;
                                int size_id = quantIntList[i].size_master_id;
                                string quantity = quantIntList[i].quantity;
                                int brand_id = quantIntList[i].brand_master_id;
                                string batch_no = quantIntList[i].batch_no;
                                string address = quantIntList[i].address;
                                int comp_id = quantIntList[i].compactor_id;

                                string query_p3 = $@"insert into exciseautomation.tab_quant_received(liq_type_id,sub_type_id,size_id,quantity,brand_name_id,batch_no,address,compactor_id,status,form_no) values ('{liq_type_id}','{sub_type_id}','{size_id}','{quantity}','{brand_id}','{batch_no}','{address}', '{comp_id}','untested','{form_no}')";
                                using (var command_p3 = GetSqlCommandWithTransaction(query_p3, transaction))
                                {
                                    command_p3.CommandText = query_p3;
                                    command_p3.ExecuteNonQuery();
                                }

                                int quant_received_id = GetQuantRecId();
                                quant_received_id = quant_received_id + i + 1;

                                string query_p4 = $@"insert into exciseautomation.tab_quant_form_no (form_no,quant_received_id) values ('{form_no}','{quant_received_id}')";

                                //string query_p4 = $@"insert into exciseautomation.tab_quant_form_no (form_no) values ('{form_no}')";
                                using (var command_p4 = GetSqlCommandWithTransaction(query_p4, transaction))
                                {
                                    command_p4.CommandText = query_p4;
                                    command_p4.ExecuteNonQuery();
                                }
                            }
                            string query_a1 = $@"insert into exciseautomation.tab_form_no_status (form_no, quantity_count, quantity_status_overall, quantity_untested,quantity_tested,quantity_retest,quantity_verified) VALUES ('{form_no}', '{quantIntList.Count}', '3', '{quantIntList.Count}', '0', '0', '0')";
                            using (var command_a1 = GetSqlCommandWithTransaction(query_a1, transaction))
                            {
                                command_a1.CommandText = query_a1;
                                command_a1.ExecuteNonQuery();
                            }


                            //distill districts
                            int dist_id = Convert.ToInt32(ddlDistillerDistrict.SelectedValue.ToString());
                            string query_p5 = $@"insert into exciseautomation.tab_form_no_dist_id (form_no,dist_id) values ('{form_no}','{dist_id}')";
                            using (var command_p5 = GetSqlCommandWithTransaction(query_p5, transaction))
                            {
                                command_p5.CommandText = query_p5;
                                command_p5.ExecuteNonQuery();
                            }
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    var alert = ($@"<script type='text/javascript'>
                                    window.onload=function()
                                    {{
                                            alert('{ex}');
                                    }};
                                </script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", alert);
                    return;
                }
                finally
                {
                    connection.Close();
                }
            }

            Session["UserID"] = Session["UserID"].ToString();
            string f_no_str = f_no.ToString();
            string dept = depart_id.ToString();
            Response.Redirect($"~/ReceivingSectionAck.aspx?form_no={f_no_str}&dept={dept}");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["Compactor"] = null;
            Session["UserID"] = Convert.ToString(Session["UserID"]);
            quantIntList.Clear();
            quantList.Clear();
            ViewState["QuantList"] = quantList;
            ViewState["QuantIntList"] = quantIntList;
            Response.Redirect("~/ReceivingSectionForm.aspx");
        }

        protected void ddlSeizureList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSeizureList.SelectedValue == "True")
            {
                txtSeizureList.Visible = true;
            }
            else
            {
                txtSeizureList.Visible = false;
            }
        }

        protected void rdType_OnSelectedIndexChange(object sender, EventArgs e)
        {
            //Session["Compactor"] = null;
            LoadDropdownsOnTypeChange();
        }

        protected void ddlCourtOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCourtOrder.SelectedValue == "True")
            {
                txtCourtOrder.Visible = true;
            }
            else
            {
                txtCourtOrder.Visible = false;
            }
        }

        protected void ddDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadThana(ddlDistrictPolice.SelectedValue);
        }

        protected void ddlTypofLiquor_Changed(object sender, EventArgs e)
        {
            if (ddlTypeOfLiquor.SelectedValue != "Select")
            {
                LoadSubLiquorType(Convert.ToInt32(ddlTypeOfLiquor.SelectedValue));
            }
        }

        protected void ddlSealed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSealed.SelectedValue == "True")
            {
                txtSealed.Visible = true;
            }
            else
            {
                txtSealed.Visible = false;
            }
        }

        #region Private Methods 
        private void LoadSubLiquorType(int liquour_type_id)
        {
            var liquorSubTypes = _masterDataService.SubLiquorListByLiquorType(liquour_type_id);
            ddlQuantitySubType.DataSource = liquorSubTypes;
            ddlQuantitySubType.DataBind();
            ddlQuantitySubType.Items.Insert(0, "Select");
        }
        private void LoadDropdownsOnTypeChange()
        {
            switch (rdType.SelectedValue)
            {
                case "excise":
                    var exciseTypes = ExciseTypeList();
                    ddlTypeExcise.DataSource = exciseTypes;
                    ddlTypeExcise.DataBind();
                    ddlTypeExcise.Items.Insert(0, "Select");

                    ddlExciseDesignation.DataSource = getExciseDesgList();
                    ddlExciseDesignation.DataBind();
                    ddlExciseDesignation.Items.Insert(0, "Select");

                    panExcise.Visible = true;
                    panDocumentReceivingChecklist.Visible = false;
                    panDistillery.Visible = false;
                    panPolice.Visible = false;
                    exciseDocumentReceivingChecklist.Visible = true;
                    break;
                case "distillery":
                    LoadDistillery();
                    panDistillery.Visible = true;
                    panDocumentReceivingChecklist.Visible = false;
                    panExcise.Visible = false;
                    panPolice.Visible = false;
                    exciseDocumentReceivingChecklist.Visible = false;

                    ddlOfficerDesignation.DataSource = getDistDesgList();
                    ddlOfficerDesignation.DataBind();
                    ddlOfficerDesignation.Items.Insert(0, "Select");
                    break;
                default:
                    var policeTypes = PolTypeList();
                    ddTypePolice.DataSource = policeTypes;
                    ddTypePolice.DataBind();
                    ddTypePolice.Items.Insert(0, "Select");

                    ddlPoliceOfficerDesignation.DataSource = getPoliceDesgList();
                    ddlPoliceOfficerDesignation.DataBind();
                    ddlPoliceOfficerDesignation.Items.Insert(0, "Select");

                    panPolice.Visible = true;
                    panDocumentReceivingChecklist.Visible = true;
                    panDistillery.Visible = false;
                    panExcise.Visible = false;
                    exciseDocumentReceivingChecklist.Visible = false;
                    break;
            }
        }

        private void LoadDistrics()
        {
            var districts = _masterDataService.DistrictList();
            ddlDistrictPolice.DataSource = districts;
            ddlDistrictPolice.DataBind();
            ddlDistrictPolice.Items.Insert(0, "Select");

            ddlExciseDistrict.DataSource = districts;
            ddlExciseDistrict.DataBind();
            ddlExciseDistrict.Items.Insert(0, "Select");

            ddlDistillerDistrict.DataSource = districts;
            ddlDistillerDistrict.DataBind();
            ddlDistillerDistrict.Items.Insert(0, "Select");

        }

        private void LoadThana(string selectedDistrict)
        {
            var thanas = _masterDataService.ThanaListByDistrictCode(selectedDistrict);
            ddThana.DataSource = thanas;
            ddThana.DataBind();
            ddThana.Items.Insert(0, "Select");
        }

        private void LoadLiquor()
        {
            var liquorTypes = _masterDataService.TypeOfLiquorList(Convert.ToString("UserId"));
            ddlTypeOfLiquor.DataSource = liquorTypes;
            ddlTypeOfLiquor.DataBind();
            ddlTypeOfLiquor.Items.Insert(0, "Select");
        }

        private void LoadSize()
        {
            var sizes = _masterDataService.SizeMasterList(Convert.ToString("UserId"));
            ddlQuantitySize.DataSource = sizes;
            ddlQuantitySize.DataBind();
            ddlQuantitySize.Items.Insert(0, "Select");
        }

        private void LoadBrand()
        {
            var brands = _masterDataService.BrandMasterList(Convert.ToString("UserId"));
            ddlBrandName.DataSource = brands;
            ddlBrandName.DataBind();
            ddlBrandName.Items.Insert(0, "Select");
        }

        private void LoadCompactor()
        {
            var compactors = _receivingSectionService.Compactors();
            ddlCompactor.DataSource = compactors;
            ddlCompactor.DataBind();
            ddlCompactor.Items.Insert(0, "Select");
        }

        private void LoadDistillery()
        {
            var distilleries = _masterDataService.DistilleryList(Convert.ToString("UserId"));
            dllDistilleryName.DataSource = distilleries;
            dllDistilleryName.DataBind();
            dllDistilleryName.Items.Insert(0, "Select");
        }

        /*
        private ReceivingSectionContext FillModel()
        {
            var context = new ReceivingSectionContext()
            {
                ReceivingSection = new ReceivingSection()
                {
                    letter_no = txtlettername.Text,
                    letter_date = Convert.ToDateTime(txtLetterDate.Text),
                    exhibit_from = rdType.SelectedValue,
                    is_sealed = Convert.ToBoolean(ddlSealed.SelectedValue),
                    issealed_text = txtSealed.Text,
                    type_of_liquor_id = Convert.ToInt32(ddlTypeOfLiquor.SelectedValue),
                    liquor_sub_type_id = Convert.ToInt32(ddlQuantitySubType.SelectedValue),
                    size_master_id = Convert.ToInt32(ddlQuantitySize.SelectedValue),
                    quantity = txtQuantity.Text,
                    brand_master_id = Convert.ToInt32(ddlBrandName.SelectedValue),
                    batch_no = txtBatchNo.Text,
                    address = txtAddressOfMan.Text,
                    compactor_id = Convert.ToInt32(ddlCompactor.SelectedValue),
                    issaved = Convert.ToBoolean(hdnIsSaveOrDraft.Value),
                    created_on = DateTime.Now,
                    created_by = Convert.ToString(Session["UserID"]),
                    receiving_date = DateTime.Now
                }
            };
            switch (context.ReceivingSection.exhibit_from)
            {
                case "police":
                    context.PoliceReceiving = new PoliceReceiving()
                    {
                        police_type = ddTypePolice.SelectedValue,
                        district_code = ddlDistrictPolice.SelectedValue,
                        thana_master_id = Convert.ToInt32(ddThana.SelectedValue),
                        designation = ddlPoliceOfficerDesignation.SelectedValue,
                        fir_no = txtFIRNo.Text,
                        fir_date = Convert.ToDateTime(txtDateOfInstitutionOfFIR.Text),
                        court_order = Convert.ToBoolean(ddlCourtOrder.SelectedValue),
                        fir_copy = Convert.ToBoolean(ddlFirCopy.SelectedValue),
                        seizure_list = Convert.ToBoolean(ddlSeizureList.SelectedValue),
                        court_order_text = txtCourtOrder.Text,
                        seizure_list_text = txtSeizureList.Text
                    };
                    break;
                case "excise":
                    context.ExciseReceiving = new ExciseReceiving()
                    {
                        excise_type = ddlTypeExcise.SelectedValue,
                        district_code = ddlExciseDistrict.SelectedValue,
                        designation = ddlExciseDesignation.SelectedValue,
                        case_no = txtCaseNO.Text,
                        case_date = Convert.ToDateTime(txtExciseDateOfInstitutionOfFIR.Text),
                        remark = txtExiseRemark.Text,
                        prno = txtPNRNO.Text,
                        statevs = txtState.Text
                    };
                    break;
                default:
                    context.DistilleryReceiving = new DistilleryReceiving()
                    {
                        distillery_code = dllDistilleryName.SelectedValue,
                        district_code = ddlDistillerDistrict.SelectedValue,
                        designation = ddlOfficerDesignation.SelectedValue,
                        vat_no = txtVatNo.Text,
                        denatured_date = Convert.ToDateTime(txtDenaturedDate.Text),
                        remark = txtRemark.Text
                    };
                    break;
            }
            if (Request.QueryString.AllKeys.Contains("QMode"))
            {
                var receivingSectionId = Request.QueryString.Get("QId");
                context.ReceivingSection.receiving_section_id = Convert.ToInt32(receivingSectionId);
            }
            return context;
        }

        private void LoadDetails(string receivingSectionId, string exhibitFrom)
        {
            ReceivingSectionContext context = _receivingSectionService.GetReceivingSectionDetailById(receivingSectionId, exhibitFrom);
            if (context != null && context.ReceivingSection != null)
            {
                rdType.Enabled = false;
                dvStatus.Visible = true;
                lblStatus.InnerText = context.ReceivingSection.issaved ? "Saved" : "Draft";
                hdnIsSavedAlready.Value = context.ReceivingSection.issaved ? bool.TrueString : bool.FalseString;

                var receivingSection = context.ReceivingSection;
                lblTodayDate.Text = receivingSection.receiving_date.ToString("dd/MM/yyyy");
                txtlettername.Text = receivingSection.letter_no;
                txtLetterDate.Text = receivingSection.letter_date?.Date != null ? receivingSection.letter_date?.Date.ToString("dd/MM/yyyy") : default(string);
                rdType.SelectedValue = receivingSection.exhibit_from;
                if (receivingSection.exhibit_from != "police")
                    LoadDropdownsOnTypeChange();
                ddlSealed.SelectedValue = Convert.ToString(receivingSection.is_sealed);
                ddlTypeOfLiquor.SelectedValue = Convert.ToString(receivingSection.type_of_liquor_id);

                LoadSubLiquorType(receivingSection.type_of_liquor_id);
                ddlQuantitySubType.SelectedValue = Convert.ToString(receivingSection.liquor_sub_type_id);
                ddlQuantitySize.SelectedValue = Convert.ToString(receivingSection.size_master_id);
                txtQuantity.Text = receivingSection.quantity;
                ddlBrandName.SelectedValue = Convert.ToString(receivingSection.brand_master_id);
                txtBatchNo.Text = receivingSection.batch_no;
                txtAddressOfMan.Text = receivingSection.address;
                ddlCompactor.SelectedValue = Convert.ToString(receivingSection.compactor_id);
                hdnIsSaveOrDraft.Value = Convert.ToString(receivingSection.issaved);

                if (receivingSection.is_sealed)
                    txtSealed.Visible = true;
                txtSealed.Text = receivingSection.issealed_text;
                switch (exhibitFrom)
                {
                    case "police":
                        var policeReceiving = context.PoliceReceiving;
                        ddTypePolice.SelectedValue = policeReceiving.police_type;
                        ddlDistrictPolice.SelectedValue = policeReceiving.district_code;

                        LoadThana(policeReceiving.district_code);
                        ddThana.SelectedValue = Convert.ToString(policeReceiving.thana_master_id);
                        ddlPoliceOfficerDesignation.SelectedValue = policeReceiving.designation;
                        txtFIRNo.Text = policeReceiving.fir_no;
                        txtDateOfInstitutionOfFIR.Text = policeReceiving.fir_date.HasValue ? policeReceiving.fir_date.Value.ToString("dd/MM/yyyy") : default(string);
                        ddlCourtOrder.SelectedValue = Convert.ToString(policeReceiving.court_order);
                        ddlFirCopy.SelectedValue = Convert.ToString(policeReceiving.fir_copy);
                        ddlSeizureList.SelectedValue = Convert.ToString(policeReceiving.seizure_list);

                        if (policeReceiving.court_order)
                            txtCourtOrder.Visible = true;
                        txtCourtOrder.Text = policeReceiving.court_order_text;

                        if (policeReceiving.seizure_list)
                            txtSeizureList.Visible = true;
                        txtSeizureList.Text = policeReceiving.seizure_list_text;
                        break;
                    case "excise":
                        var exciseReceiving = context.ExciseReceiving;
                        ddlTypeExcise.SelectedValue = exciseReceiving.excise_type;
                        ddlExciseDistrict.SelectedValue = exciseReceiving.district_code;
                        ddlExciseDesignation.SelectedValue = exciseReceiving.designation;
                        txtCaseNO.Text = exciseReceiving.case_no;
                        txtExciseDateOfInstitutionOfFIR.Text = exciseReceiving.case_date.HasValue ? exciseReceiving.case_date.Value.ToString("dd/MM/yyyy") : default(string);
                        txtExiseRemark.Text = exciseReceiving.remark;
                        txtPNRNO.Text = exciseReceiving.prno;
                        txtState.Text = exciseReceiving.statevs;
                        break;
                    default:
                        var distilleryReceiving = context.DistilleryReceiving;
                        dllDistilleryName.SelectedValue = distilleryReceiving.distillery_code;
                        ddlDistillerDistrict.SelectedValue = distilleryReceiving.district_code;
                        ddlOfficerDesignation.SelectedValue = distilleryReceiving.designation;
                        txtVatNo.Text = distilleryReceiving.vat_no;
                        txtDenaturedDate.Text = distilleryReceiving.denatured_date.HasValue ? distilleryReceiving.denatured_date.Value.ToString("dd/MM/yyyy") : default(string); ;
                        txtRemark.Text = distilleryReceiving.remark;
                        break;
                }
            }
        }
        */

        public List<PolType> PolTypeList()
        {
            List<PolType> polTypeList = new List<PolType>();
            string query = @"select * from exciseautomation.tab_pol_type"; 
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        PolType item = new PolType();
                        item.pol_type_id = dr.GetInt32(dr.GetOrdinal("pol_type_id"));
                        item.pol_type_name = dr.GetString(dr.GetOrdinal("pol_type"));
                        polTypeList.Add(item);
                    }
                }
                command.Connection.Close();
            }
            return polTypeList;
        }

        public List<ExciseType> ExciseTypeList()
        {
            List<ExciseType> exTypeList = new List<ExciseType>();
            string query = @"select * from exciseautomation.tab_excise_type";
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ExciseType item = new ExciseType();
                        item.excise_type_id = dr.GetInt32(dr.GetOrdinal("excise_type_id"));
                        item.excise_type_name = dr.GetString(dr.GetOrdinal("excise_type"));
                        exTypeList.Add(item);
                    }
                }
                command.Connection.Close();
            }
            return exTypeList;
        }

        public List<PoliceDesg> getPoliceDesgList()
        {
            List<PoliceDesg> list = new List<PoliceDesg>();
            string query = @"select * from exciseautomation.tab_pol_off_desg";
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        PoliceDesg item = new PoliceDesg();
                        item.pol_desg_id = dr.GetInt32(dr.GetOrdinal("desg_id"));
                        item.pol_desg_name = dr.GetString(dr.GetOrdinal("pol_off_desg"));
                        list.Add(item);
                    }
                }
                command.Connection.Close();
            }
            return list;
        }

        public List<ExciseDesg> getExciseDesgList()
        {
            List<ExciseDesg> list = new List<ExciseDesg>();
            string query = @"select * from exciseautomation.tab_excise_off_desg";
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ExciseDesg item = new ExciseDesg();
                        item.excise_desg_id = dr.GetInt32(dr.GetOrdinal("desg_id"));
                        item.excise_desg_name = dr.GetString(dr.GetOrdinal("excise_off_desg"));
                        list.Add(item);
                    }
                }
                command.Connection.Close();
            }
            return list;
        }

        public List<DistDesg> getDistDesgList()
        {
            List<DistDesg> list = new List<DistDesg>();
            string query = @"select * from exciseautomation.tab_distill_off_desg";
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DistDesg item = new DistDesg();
                        item.dist_desg_id = dr.GetInt32(dr.GetOrdinal("desg_id"));
                        item.dist_desg_name = dr.GetString(dr.GetOrdinal("distill_off_desg"));
                        list.Add(item);
                    }
                }
                command.Connection.Close();
            }
            return list;
        }
        #endregion
    }
}