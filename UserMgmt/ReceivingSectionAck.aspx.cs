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
    public partial class ReceivingSectionAck : System.Web.UI.Page
    {
        public class QuantReceived
        {
            public string QuantId { get; set; }
            public string LiqType { get; set; }
            public string LiqSubType { get; set; }
            public string LiqSize { get; set; }
            public string LiqQuant { get; set; }
            public string LiqBrand { get; set; }
            public string LiqBatch { get; set; }
            public string LiqAddr { get; set; }

            public QuantReceived()
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

        protected void Page_Load(object sender, EventArgs e)
        {
            string form_no = Request.QueryString["form_no"];
            string dept = Request.QueryString["dept"];
            if (dept == "1")
            {
                //Police
                panPolice.Visible = true;
                panExcise.Visible = false;
                panDistill.Visible = false;
                string tmp1 = "SELECT A.date_of_creation,C.dist_name,D.letter_number,D.letter_date,D.\"Sealed_details\",D.\"Sealed_details_desc\",E.fir_no,E.date_of_fir,F.thana_name,G.court_order,G.fir_copy,G.seizure_list FROM ";
                string tmp2 = $@"exciseautomation.tab_form_no_date A
                                  LEFT JOIN exciseautomation.tab_form_no_dist_id B ON A.form_no=B.form_no
                                  LEFT JOIN exciseautomation.tab_district C ON B.dist_id=C.dist_id
                                  LEFT JOIN exciseautomation.tab_letter_no_form_no D ON A.form_no=D.form_no
                                  LEFT JOIN exciseautomation.tab_police_info E ON A.form_no=E.form_no
                                  LEFT JOIN exciseautomation.tab_thana F ON E.thana_id=F.thana_id
                                  LEFT JOIN exciseautomation.tab_doc_pol G ON A.form_no=G.form_no
                                  WHERE A.form_no='{form_no}' LIMIT 1";
                string query = tmp1 + tmp2;
                using (var command = GetSqlCommand(query))
                {
                    command.Connection.Open();
                    NpgsqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            string formDate = dr.IsDBNull(dr.GetOrdinal("date_of_creation")) ? string.Empty : dr.GetDateTime(dr.GetOrdinal("date_of_creation")).ToString("dd'-'MM'-'yyyy");
                            string distName = dr.IsDBNull(dr.GetOrdinal("dist_name")) ? string.Empty : dr.GetString(dr.GetOrdinal("dist_name"));
                            string letterNumber = dr.IsDBNull(dr.GetOrdinal("letter_number")) ? string.Empty : dr.GetString(dr.GetOrdinal("letter_number"));
                            string letterDate = dr.IsDBNull(dr.GetOrdinal("letter_date")) ? string.Empty : dr.GetDateTime(dr.GetOrdinal("letter_date")).ToString("dd'-'MM'-'yyyy");
                            string sealedStatus = dr.IsDBNull(dr.GetOrdinal("Sealed_details")) ? string.Empty : dr.GetString(dr.GetOrdinal("Sealed_details"));
                            string sealedDetails = dr.IsDBNull(dr.GetOrdinal("Sealed_details_desc")) ? string.Empty : dr.GetString(dr.GetOrdinal("Sealed_details_desc"));
                            //Bhavin
                            //string firNo = dr.IsDBNull(dr.GetOrdinal("fir_no")) ? string.Empty : dr.GetString(dr.GetOrdinal("dist_name"));
                            string firNo = dr.IsDBNull(dr.GetOrdinal("fir_no")) ? string.Empty : dr.GetString(dr.GetOrdinal("fir_no"));
                            //End
                            string firDate = dr.IsDBNull(dr.GetOrdinal("date_of_fir")) ? string.Empty : dr.GetDateTime(dr.GetOrdinal("date_of_fir")).ToString("dd'-'MM'-'yyyy");
                            string thanaName = dr.IsDBNull(dr.GetOrdinal("thana_name")) ? string.Empty : dr.GetString(dr.GetOrdinal("thana_name"));
                            string courtOrder = dr.IsDBNull(dr.GetOrdinal("court_order")) ? string.Empty : dr.GetString(dr.GetOrdinal("court_order"));
                            string firCopy = dr.IsDBNull(dr.GetOrdinal("fir_copy")) ? string.Empty : dr.GetString(dr.GetOrdinal("fir_copy"));
                            string seizureList = dr.IsDBNull(dr.GetOrdinal("seizure_list")) ? string.Empty : dr.GetString(dr.GetOrdinal("seizure_list"));

                            polFormDate.Text = form_no+'/'+formDate;
                            polDist.Text = distName;
                            polThana.Text = thanaName;
                            polFirNo.Text = firNo;
                            polFirDate.Text = firDate;
                            polLetterNo.Text = letterNumber;
                            polLetterDate.Text = letterDate;
                            polFirCopy.Text = firCopy;
                            polCourtOrder.Text = courtOrder;
                            polSeizureList.Text = seizureList;
                            polSealedStatus.Text = sealedStatus;
                            polSealedDetails.Text = sealedDetails;
                        }
                    }
                    command.Connection.Close();
                }
            }
            else if (dept == "2")
            {
                //Excise
                panPolice.Visible = false;
                panExcise.Visible = true;
                panDistill.Visible = false;
                string tmp1 = "SELECT A.date_of_creation,C.dist_name,D.letter_number,D.letter_date,D.\"Sealed_details\",D.\"Sealed_details_desc\",E.case_no,E.date_of_case,E.excise_remark,F.pr_no,F.state_vs FROM ";
                string tmp2 = $@"exciseautomation.tab_form_no_date A
                                  LEFT JOIN exciseautomation.tab_form_no_dist_id B ON A.form_no=B.form_no
                                  LEFT JOIN exciseautomation.tab_district C ON B.dist_id=C.dist_id
                                  LEFT JOIN exciseautomation.tab_letter_no_form_no D ON A.form_no=D.form_no
                                  LEFT JOIN exciseautomation.tab_excise_info E ON A.form_no=E.form_no
                                  LEFT JOIN exciseautomation.tab_doc_excise F ON A.form_no=F.form_no
                                  WHERE A.form_no='{form_no}' LIMIT 1";
                string query = tmp1 + tmp2;
                using (var command = GetSqlCommand(query))
                {
                    command.Connection.Open();
                    NpgsqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            string formDate = dr.IsDBNull(dr.GetOrdinal("date_of_creation")) ? string.Empty : dr.GetDateTime(dr.GetOrdinal("date_of_creation")).ToString("dd'-'MM'-'yyyy");
                            string distName = dr.IsDBNull(dr.GetOrdinal("dist_name")) ? string.Empty : dr.GetString(dr.GetOrdinal("dist_name"));
                            string letterNumber = dr.IsDBNull(dr.GetOrdinal("letter_number")) ? string.Empty : dr.GetString(dr.GetOrdinal("letter_number"));
                            string letterDate = dr.IsDBNull(dr.GetOrdinal("letter_date")) ? string.Empty : dr.GetDateTime(dr.GetOrdinal("letter_date")).ToString("dd'-'MM'-'yyyy");
                            string sealedStatus = dr.IsDBNull(dr.GetOrdinal("Sealed_details")) ? string.Empty : dr.GetString(dr.GetOrdinal("Sealed_details"));
                            string sealedDetails = dr.IsDBNull(dr.GetOrdinal("Sealed_details_desc")) ? string.Empty : dr.GetString(dr.GetOrdinal("Sealed_details_desc"));
                            string CaseNo = dr.IsDBNull(dr.GetOrdinal("case_no")) ? string.Empty : dr.GetString(dr.GetOrdinal("case_no"));
                            string CaseDate = dr.IsDBNull(dr.GetOrdinal("date_of_case")) ? string.Empty : dr.GetDateTime(dr.GetOrdinal("date_of_case")).ToString("dd'-'MM'-'yyyy");
                            string ExciseRemark = dr.IsDBNull(dr.GetOrdinal("excise_remark")) ? string.Empty : dr.GetString(dr.GetOrdinal("excise_remark"));
                            string prNo = dr.IsDBNull(dr.GetOrdinal("pr_no")) ? string.Empty : dr.GetString(dr.GetOrdinal("pr_no"));
                            string stateVs = dr.IsDBNull(dr.GetOrdinal("state_vs")) ? string.Empty : dr.GetString(dr.GetOrdinal("state_vs"));

                            exFormDate.Text = form_no + '/' + formDate;
                            exDist.Text = distName;
                            exLetterNo.Text = letterNumber;
                            exLetterDate.Text = letterDate;
                            exSealedStatus.Text = sealedStatus;
                            exSealedDetails.Text = sealedDetails;
                            exCaseNo.Text = CaseNo;
                            exCaseDate.Text = CaseDate;
                            exRemark.Text = ExciseRemark;
                            exPrNo.Text = prNo;
                            exStateVs.Text = stateVs;
                        }
                    }
                    command.Connection.Close();
                }
            }
            else if (dept == "3")
            {
                //Distillery
                panPolice.Visible = false;
                panExcise.Visible = false;
                panDistill.Visible = true;
                string tmp1 = "SELECT A.date_of_creation,C.dist_name,D.letter_number,D.letter_date,D.\"Sealed_details\",D.\"Sealed_details_desc\",E.ref_no,E.distill_remark,E.distill_den_date,F.distill_name FROM ";
                string tmp2 = $@"exciseautomation.tab_form_no_date A
                                  LEFT JOIN exciseautomation.tab_form_no_dist_id B ON A.form_no=B.form_no
                                  LEFT JOIN exciseautomation.tab_district C ON B.dist_id=C.dist_id
                                  LEFT JOIN exciseautomation.tab_letter_no_form_no D ON A.form_no=D.form_no
                                  LEFT JOIN exciseautomation.tab_distill_info E ON A.form_no=E.form_no
                                  LEFT JOIN exciseautomation.tab_distill F ON E.distill_id=F.distill_id
                                  WHERE A.form_no='{form_no}' LIMIT 1";
                string query = tmp1 + tmp2;
                using (var command = GetSqlCommand(query))
                {
                    command.Connection.Open();
                    NpgsqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            string formDate = dr.IsDBNull(dr.GetOrdinal("date_of_creation")) ? string.Empty : dr.GetDateTime(dr.GetOrdinal("date_of_creation")).ToString("dd'-'MM'-'yyyy");
                            string distName = dr.IsDBNull(dr.GetOrdinal("dist_name")) ? string.Empty : dr.GetString(dr.GetOrdinal("dist_name"));
                            string letterNumber = dr.IsDBNull(dr.GetOrdinal("letter_number")) ? string.Empty : dr.GetString(dr.GetOrdinal("letter_number"));
                            string letterDate = dr.IsDBNull(dr.GetOrdinal("letter_date")) ? string.Empty : dr.GetDateTime(dr.GetOrdinal("letter_date")).ToString("dd'-'MM'-'yyyy");
                            string sealedStatus = dr.IsDBNull(dr.GetOrdinal("Sealed_details")) ? string.Empty : dr.GetString(dr.GetOrdinal("Sealed_details"));
                            string sealedDetails = dr.IsDBNull(dr.GetOrdinal("Sealed_details_desc")) ? string.Empty : dr.GetString(dr.GetOrdinal("Sealed_details_desc"));
                            string distillName = dr.IsDBNull(dr.GetOrdinal("distill_name")) ? string.Empty : dr.GetString(dr.GetOrdinal("distill_name"));
                            string VatNo = dr.IsDBNull(dr.GetOrdinal("ref_no")) ? string.Empty : dr.GetString(dr.GetOrdinal("ref_no"));
                            string DistillRemark = dr.IsDBNull(dr.GetOrdinal("distill_remark")) ? string.Empty : dr.GetString(dr.GetOrdinal("distill_remark"));
                            string DenaturedDate = dr.IsDBNull(dr.GetOrdinal("distill_den_date")) ? string.Empty : dr.GetDateTime(dr.GetOrdinal("distill_den_date")).ToString("dd'-'MM'-'yyyy");

                            disFormDate.Text = form_no + '/' + formDate;
                            disDistrict.Text = distName;
                            disLetterNo.Text = letterNumber;
                            disLetterDate.Text = letterDate;
                            disSealedStatus.Text = sealedStatus;
                            disSealedDetails.Text = sealedDetails;
                            disDistilleryName.Text = distillName;
                            disVatNo.Text = VatNo;
                            disRemark.Text = DistillRemark;
                            disDenaturedDate.Text = DenaturedDate;
                        }
                    }
                    command.Connection.Close();
                }
            }

            LoadCommon(form_no,dept);
        }

        public void LoadCommon(string form_no,string dept)
        {
            LoadGrid(form_no);
        }

        public void LoadGrid(string form_no)
        {
            grdQuantList.DataSource = GetList(form_no);
            grdQuantList.DataBind();
            grdQuantList.HeaderStyle.CssClass = "gridcontent";
            grdQuantList.RowStyle.CssClass = "gridcontent";
        }

        List<QuantReceived> GetList(string form_no)
        {
            List<QuantReceived> items = new List<QuantReceived>();

            //string query = $@"SELECT
            //              G.quant_received_id, 
            //              A.quantity,
            //              A.batch_no,
            //              A.address,
            //              B.type_of_liquor_name,
            //              C.liquor_sub_name,
            //              D.size_name,
            //              E.brand_name
            //            FROM
            //              exciseautomation.tab_quant_form_no G 
            //              LEFT JOIN exciseautomation.tab_quant_received A ON G.quant_received_id=A.quant_received_id
            //              LEFT JOIN exciseautomation.type_of_liquor B ON A.liq_type_id = B.type_of_liquor_id
            //              LEFT JOIN exciseautomation.liquor_sub_type C ON A.sub_type_id = C.liquor_sub_type_id
            //              LEFT JOIN exciseautomation.tab_liq_size D ON A.size_id = D.size_id
            //              LEFT JOIN exciseautomation.tab_liq_brand_name E ON A.brand_name_id = E.brand_id
            //              WHERE G.form_no='{form_no}'";


            string query = $@"SELECT
                          G.quant_received_id, 
                          A.quantity,
                          A.batch_no,
                          A.address,
                          B.liq_type,
                          C.liq_sub_type_name,
                          D.size_name,
                          E.brand_name
                        FROM
                          exciseautomation.tab_quant_form_no G 
                          LEFT JOIN exciseautomation.tab_quant_received A ON G.quant_received_id=A.quant_received_id
                          LEFT JOIN exciseautomation.tab_liq_type B ON A.liq_type_id = B.liq_id
                          LEFT JOIN exciseautomation.tab_liq_subtype C ON A.sub_type_id = C.liq_sub_type_id
                          LEFT JOIN exciseautomation.tab_liq_size D ON A.size_id = D.size_id
                          LEFT JOIN exciseautomation.tab_liq_brand_name E ON A.brand_name_id = E.brand_id
                          WHERE G.form_no='{form_no}'";
            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        QuantReceived item = new QuantReceived();
                        item.QuantId = dr.IsDBNull(dr.GetOrdinal("quant_received_id")) ? string.Empty : dr.GetInt32(dr.GetOrdinal("quant_received_id")).ToString();
                        item.LiqType = dr.IsDBNull(dr.GetOrdinal("liq_type")) ? string.Empty : dr.GetString(dr.GetOrdinal("liq_type"));
                        item.LiqSubType = dr.IsDBNull(dr.GetOrdinal("liq_sub_type_name")) ? string.Empty : dr.GetString(dr.GetOrdinal("liq_sub_type_name"));
                        item.LiqSize = dr.IsDBNull(dr.GetOrdinal("size_name")) ? string.Empty : dr.GetString(dr.GetOrdinal("size_name"));
                        item.LiqQuant = dr.IsDBNull(dr.GetOrdinal("quantity")) ? string.Empty : dr.GetString(dr.GetOrdinal("quantity"));
                        item.LiqBrand = dr.IsDBNull(dr.GetOrdinal("brand_name")) ? string.Empty : dr.GetString(dr.GetOrdinal("brand_name"));
                        item.LiqBatch = dr.IsDBNull(dr.GetOrdinal("batch_no")) ? string.Empty : dr.GetString(dr.GetOrdinal("batch_no"));
                        item.LiqAddr = dr.IsDBNull(dr.GetOrdinal("address")) ? string.Empty : dr.GetString(dr.GetOrdinal("address"));
                        items.Add(item);

                        //QuantReceived item = new QuantReceived();
                        //item.QuantId = dr.IsDBNull(dr.GetOrdinal("quant_received_id")) ? string.Empty : dr.GetInt32(dr.GetOrdinal("quant_received_id")).ToString();
                        //item.LiqType = dr.IsDBNull(dr.GetOrdinal("type_of_liquor_name")) ? string.Empty : dr.GetString(dr.GetOrdinal("type_of_liquor_name"));
                        //item.LiqSubType = dr.IsDBNull(dr.GetOrdinal("liquor_sub_name")) ? string.Empty : dr.GetString(dr.GetOrdinal("liquor_sub_name"));
                        //item.LiqSize = dr.IsDBNull(dr.GetOrdinal("size_name")) ? string.Empty : dr.GetString(dr.GetOrdinal("size_name"));
                        //item.LiqQuant = dr.IsDBNull(dr.GetOrdinal("quantity")) ? string.Empty : dr.GetString(dr.GetOrdinal("quantity"));
                        //item.LiqBrand = dr.IsDBNull(dr.GetOrdinal("brand_name")) ? string.Empty : dr.GetString(dr.GetOrdinal("brand_name"));
                        //item.LiqBatch = dr.IsDBNull(dr.GetOrdinal("batch_no")) ? string.Empty : dr.GetString(dr.GetOrdinal("batch_no"));
                        //item.LiqAddr = dr.IsDBNull(dr.GetOrdinal("address")) ? string.Empty : dr.GetString(dr.GetOrdinal("address"));
                        //items.Add(item);
                    }
                }
                command.Connection.Close();
            }
            return items;
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect($"~/ReceivingSectionForm.aspx");
        }
    }
}