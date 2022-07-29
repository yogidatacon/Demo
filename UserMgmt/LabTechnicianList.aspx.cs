using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using Usermngt.BL.LabTechnician;

namespace UserMgmt
{
    public partial class LabTechnicianList : System.Web.UI.Page
    {
        public class LabTech
        {
            public string QuantId { get; set; }
            public string LiqType { get; set; }
            public string LiqSubType { get; set; }
            public string LiqSize { get; set; }
            public string LiqQuant { get; set; }
            public string LiqBrand { get; set; }
            public string LiqBatch { get; set; }
            public string LiqAddr { get; set; }
            public string LiqStatus { get; set; }
            public string FormNo { get; set; }
            public string FormDate { get; set; }
            public string CompId { get; set; }

            public LabTech()
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

                LoadGrid();
            }
        }

        protected void GridReceivingSectionList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                var inputParams = e.CommandArgument.ToString().Split(',');
                var quant_id = inputParams.Length > 0 ? inputParams[0] : default(string);
                var form_no = inputParams.Length > 1 ? inputParams[1] : default(string);
                var status = inputParams.Length > 2 ? inputParams[2] : default(string);
                Response.Redirect($"~/LabTechreportForm.aspx?form_no={form_no}&qid={quant_id}&status={status}");
            }
        }

        protected void grdReceivingSectionList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdReceivingSectionList.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        #region Private Methods
        private void LoadGrid()
        {
            List<LabTech> labTechList = getList();
            grdReceivingSectionList.DataSource = labTechList;
            grdReceivingSectionList.DataBind();
        }

        List<LabTech> getList()
        {
            List<LabTech> list = new List<LabTech>();
            //var query = $@"SELECT 
            //              A.quant_received_id, 
            //              A.quantity,
            //              A.batch_no,
            //              A.address,
            //              A.status,
            //              B.liq_type,
            //              C.liq_sub_type_name,
            //              D.size_name,
            //              E.brand_name,
            //              F.comp_id,
            //              G.form_no
            //            FROM 
            //              exciseautomation.tab_quant_received A
            //              LEFT JOIN exciseautomation.tab_liq_type B ON A.liq_type_id=B.liq_id
            //              LEFT JOIN exciseautomation.tab_liq_subtype C ON A.sub_type_id=C.liq_sub_type_id
            //              LEFT JOIN exciseautomation.tab_liq_size D ON A.size_id=D.size_id
            //              LEFT JOIN exciseautomation.tab_liq_brand_name E ON A.brand_name_id=E.brand_id
            //              LEFT JOIN exciseautomation.tab_compactor F ON A.compactor_id=F.comp_id
            //              LEFT JOIN exciseautomation.tab_quant_form_no G ON A.quant_received_id=G.quant_received_id
            //              WHERE (A.status='retest' OR A.status='untested')
            //              ORDER BY A.quant_received_id DESC";



            var query = $@"SELECT 
                          A.quant_received_id, 
                          A.quantity,
                          A.batch_no,
                          A.address,
                          A.status,
                          B.liq_type,
                          C.liq_sub_type_name,
                          D.size_name,
                          E.brand_name,
                          F.comp_id,
                          G.form_no
                        FROM 
                          exciseautomation.tab_quant_received A
                          LEFT JOIN exciseautomation.tab_liq_type B ON A.liq_type_id=B.liq_id
                          LEFT JOIN exciseautomation.tab_liq_subtype C ON A.sub_type_id=C.liq_sub_type_id
                          LEFT JOIN exciseautomation.tab_liq_size D ON A.size_id=D.size_id
                          LEFT JOIN exciseautomation.tab_liq_brand_name E ON A.brand_name_id=E.brand_id
                          LEFT JOIN exciseautomation.tab_compactor F ON A.compactor_id=F.comp_id
                        inner JOIN exciseautomation.tab_quant_form_no G ON A.quant_received_id=G.quant_received_id
                          WHERE (A.status='retest' OR A.status='untested') and A.Compactor_id in (select Compactor_id from exciseautomation.tab_compactor_id_tech where tech_id = '{ Session["user_Reg_Id"]}' )
                          ORDER BY A.quant_received_id DESC";


            using (var command = GetSqlCommand(query))
            {
                command.Connection.Open();
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        LabTech item = new LabTech();
                        item.QuantId = dr.IsDBNull(dr.GetOrdinal("quant_received_id")) ? string.Empty : dr.GetInt32(dr.GetOrdinal("quant_received_id")).ToString();
                        item.LiqType = dr.IsDBNull(dr.GetOrdinal("liq_type")) ? string.Empty : dr.GetString(dr.GetOrdinal("liq_type"));
                        item.LiqSubType = dr.IsDBNull(dr.GetOrdinal("liq_sub_type_name")) ? string.Empty : dr.GetString(dr.GetOrdinal("liq_sub_type_name"));
                        item.LiqSize = dr.IsDBNull(dr.GetOrdinal("size_name")) ? string.Empty : dr.GetString(dr.GetOrdinal("size_name"));
                        item.LiqQuant = dr.IsDBNull(dr.GetOrdinal("quantity")) ? string.Empty : dr.GetString(dr.GetOrdinal("quantity"));
                        item.LiqBrand = dr.IsDBNull(dr.GetOrdinal("brand_name")) ? string.Empty : dr.GetString(dr.GetOrdinal("brand_name"));
                        item.LiqBatch = dr.IsDBNull(dr.GetOrdinal("batch_no")) ? string.Empty : dr.GetString(dr.GetOrdinal("batch_no")); 
                        item.LiqAddr = dr.IsDBNull(dr.GetOrdinal("address")) ? string.Empty : dr.GetString(dr.GetOrdinal("address")); 
                        item.LiqStatus = dr.IsDBNull(dr.GetOrdinal("status")) ? string.Empty : dr.GetString(dr.GetOrdinal("status"));
                        item.FormNo = dr.IsDBNull(dr.GetOrdinal("form_no")) ? string.Empty : dr.GetInt32(dr.GetOrdinal("form_no")).ToString();
                        item.CompId = dr.IsDBNull(dr.GetOrdinal("comp_id")) ? string.Empty : dr.GetInt32(dr.GetOrdinal("comp_id")).ToString();
                        if (item.FormNo == string.Empty)
                        {
                            item.FormDate = string.Empty;
                        }
                        else
                        {
                            string query2 = $@"SELECT * FROM exciseautomation.tab_form_no_date where form_no='{item.FormNo}' LIMIT 1";
                            using (var command2 = GetSqlCommand(query2))
                            {
                                command2.Connection.Open();
                                NpgsqlDataReader dr2 = command2.ExecuteReader();
                                if (dr2.HasRows)
                                {
                                    while (dr2.Read())
                                    {
                                        item.FormDate = dr2.IsDBNull(dr2.GetOrdinal("date_of_creation")) ? string.Empty : dr2.GetDateTime(dr2.GetOrdinal("date_of_creation")).ToString("dd'/'MM'/'yyyy");
                                    }
                                }
                                command2.Connection.Close();
                            }
                        }
                        
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