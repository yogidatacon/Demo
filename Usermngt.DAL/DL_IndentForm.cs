using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.Entities;

namespace Usermngt.DAL
{
    public class DL_IndentForm
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static string Insert(Indent_Form indent)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT  case when max(molasses_indent_id) is null then 0 else max(molasses_indent_id) end as molasses_indent_id FROM exciseautomation.molasses_indent where financial_year='"+indent.financial_year+"'", cn);
                int m = Convert.ToInt32(cmd1.ExecuteScalar());
                cmd1 = new NpgsqlCommand("SELECT  case when max(molasses_indent_reqno) is null then 0 else max(molasses_indent_reqno) end as molasses_indent_reqno FROM exciseautomation.molasses_indent where party_code='"+indent.party_code+ "' and  financial_year='" + indent.financial_year + "'", cn);
                int m1 = Convert.ToInt32(cmd1.ExecuteScalar())+1;
                m = m + 1;
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("INSERT INTO exciseautomation.molasses_indent(molasses_indent_id, molasses_indent_reqno, record_id_format, indent_date, indent_qty, financial_year, is_captive, party_code, product_code,from_unit,");
                    str.Append("req_cs, req_rs, req_pa, req_ds, recd_pycs, recd_pyrs, recd_pypa, recd_pyds, used_pycs, used_pyrs, used_pypa, used_pyds, used_cycs,used_cyds, used_cypa,  used_cyrs,");
                    str.Append("molasses_distilled, working_wastage, transit_wastage, molasses_instock_cy, molasses_recd_cy, molasses_used_cy, molasses_to_lift, molasses_to_consume, molasses_instock, molasses_to_be_lifted,");
                    str.Append("molasses_bal_allotment, molasses_bal_storage, molasses_allocated_qty, molasses_lifted_qty, creation_date,  user_id, record_status)Values(");
                    str.Append("'" + m + "','" + m1 + "','" + indent.record_id_format + "','" + indent.indent_date + "','" + indent.indent_qty + "','" + indent.financial_year + "','" + indent.is_captive + "','" + indent.party_code + "','" + indent.product_code + "','"+indent.captive_unit_name+"',");
                    str.Append("'" + indent.req_cs + "','" + indent.req_rs + "','" + indent.req_pa + "','" + indent.req_ds + "','" + indent.recd_pycs + "','" + indent.recd_pyrs + "','" + indent.recd_pypa + "','" + indent.recd_pyds + "','" + indent.used_pycs + "','" + indent.used_pyrs + "','" + indent.used_pypa + "','" + indent.used_pyds + "','" + indent.used_cycs + "','" + indent.used_cyds + "','" + indent.used_cypa + "','" + indent.used_cyrs + "',");
                    str.Append("'" + indent.molasses_distilled + "','" + indent.working_wastage + "','" + indent.transit_wastage + "','" + indent.molasses_instock_cy + "','" + indent.molasses_recd_cy + "','" + indent.molasses_used_cy + "','" + indent.molasses_to_lift + "','" + indent.molasses_to_consume + "','" + indent.molasses_instock + "','" + indent.molasses_to_be_lifted + "',");
                    str.Append("'" + indent.molasses_bal_allotment + "','" + indent.molasses_bal_storage + "','" + indent.molasses_allocated_qty + "','" + indent.molasses_lifted_qty + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + indent.user_id + "','" + indent.record_status + "')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    for (int i1 = 0; i1 < indent.docs.Count; i1++)
                    {
                        str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.eascm_docs(doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,financial_year,party_code)");
                        str.Append("Values('" + m + "','" + indent.docs[i1].doc_name + "', '" + indent.docs[i1].description + "','" + indent.docs[i1].doc_path + "','IND','" + indent.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+indent.financial_year+"','"+indent.party_code+"')");
                        NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                        int r = cmd3.ExecuteNonQuery();
                    }
                    VAL = "0";
                    trn.Commit();
                    cn.Close();
                    _log.Info("Insert Indent Success :" + indent.indent_date + "-" + indent.party_code);
                }
                catch (Exception ex1)
                {
                    _log.Info("Insert Indent Success :" + indent.indent_date + "-" + indent.party_code + "-" + ex1.Message);
                    VAL = ex1.Message;
                    trn.Rollback();
                }

            }
            return VAL;
        }
        public static string GetValues(string value)
        {
            string val = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    string[] party = value.Split('_');
                    using (NpgsqlCommand cmd = new NpgsqlCommand("Select count(1) from exciseautomation.molasses_Indent where party_code='" + party[0] + "' and product_code='" + party[1] + "' and financial_year='" + party[2] + "' ", cn))
                    {
                        val = cmd.ExecuteScalar().ToString();
                        if (val != "" && val != "0")
                            val = "DataExist";
                    }
                }
                catch
                {
                }
            }
            return val;
        }

        public static string Update(Indent_Form indent)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    //(,
                    StringBuilder str = new StringBuilder();
                    str.Append("update exciseautomation.molasses_indent set  indent_date='" + indent.indent_date + "', indent_qty='" + indent.indent_qty + "', financial_year='"+indent.financial_year+"', is_captive='"+indent.is_captive+"', party_code='"+indent.party_code+"', product_code='"+indent.product_code+"',from_unit='"+indent.captive_unit_name+"',");
                    str.Append("req_cs='"+indent.req_cs+"', req_rs='"+indent.req_rs+ "', req_pa='"+indent.req_pa+"', req_ds='" + indent.req_ds + "', recd_pycs='"+indent.recd_pycs+"', recd_pyrs='"+indent.recd_pyrs+"', recd_pypa='"+indent.recd_pypa+"', recd_pyds='"+indent.recd_pyds+"', used_pycs='"+indent.used_pycs+"', used_pyrs='"+indent.used_pyrs+"', used_pypa='"+indent.used_pypa+"', used_pyds='"+indent.used_pyds+"', used_cycs='"+indent.used_cycs+"',used_cyds='"+indent.used_cyds+"', used_cypa='"+indent.used_cypa+"',  used_cyrs='"+indent.used_cyrs+"',");
                    str.Append("molasses_distilled='"+indent.molasses_distilled+"', working_wastage='"+indent.working_wastage+"', transit_wastage='"+indent.transit_wastage+"', molasses_instock_cy='"+indent.molasses_instock_cy+"', molasses_recd_cy='"+indent.molasses_recd_cy+"', molasses_used_cy='"+indent.molasses_used_cy+"', molasses_to_lift='"+indent.molasses_to_lift+"', molasses_to_consume='"+indent.molasses_to_consume+"', molasses_instock='"+indent.molasses_instock+"', molasses_to_be_lifted='"+indent.molasses_to_be_lifted+"',");
                    str.Append("molasses_bal_allotment='"+indent.molasses_bal_allotment+"', molasses_bal_storage='"+indent.molasses_bal_storage+"', molasses_allocated_qty='"+indent.molasses_allocated_qty+"', molasses_lifted_qty='"+indent.molasses_lifted_qty+"',  user_id='"+indent.user_id+"', record_status='"+indent.record_status+ "',lastmodified_date='"+DateTime.Now.ToString("dd-MM-yyyy")+ "' where molasses_indent_id='"+indent.molasses_indent_id+ "' and  financial_year='" + indent.financial_year + "'");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                   
                    if (n == 1)
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("delete from exciseautomation.eascm_docs where doc_id='" + indent.molasses_indent_id + "' and doc_type_code='IND' and  financial_year='" + indent.financial_year + "'", cn);
                        cmd1.ExecuteNonQuery();
                        for (int i = 0; i < indent.docs.Count; i++)
                        {
                            n = 0;
                            str = new StringBuilder();

                            str.Append("INSERT INTO exciseautomation.eascm_docs(doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,financial_year,party_code)");
                            str.Append("Values('" + indent.molasses_indent_id + "','" + indent.docs[i].doc_name + "', '" + indent.docs[i].description + "','" + indent.docs[i].doc_path + "','IND','" + indent.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+indent.financial_year+"','"+indent.party_code+"')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd3.ExecuteNonQuery();
                        }
                        VAL = "0";
                    }
                    else
                        VAL = "1";
                    trn.Commit();
                    cn.Close();
                    _log.Info("Update Indent Success :" + indent.indent_date + "-" + indent.party_code);
                }
                catch (Exception ex1)
                {
                    _log.Info("Update Indent Success :" + indent.indent_date + "-" + indent.party_code + "-" + ex1.Message);
                    VAL = ex1.Message;
                    trn.Rollback();
                }
            }

            return VAL;
        }

        public static List<Indent_Form> GetList()
        {
            List<Indent_Form> indets = new List<Indent_Form>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.view_mf1_indent_listdetails  order by product_name ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            indets = new List<Indent_Form>();
                            while (dr.Read())
                            {
                                Indent_Form record = new Indent_Form();
                                record.indent_qty =Convert.ToDouble( dr["indent_qty"].ToString());
                                record.financial_year = dr["financial_year"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.molasses_indent_id = dr["molasses_indent_id"].ToString();
                                record.product_name = dr["product_name"].ToString();
                                record.product_code = dr["product_code"].ToString();
                                record.party_code = dr["party_code"].ToString();
                                record.molasses_allocated_qty = Convert.ToDouble(dr["Allotted_qty"].ToString());
                                record.molasses_lifted_qty = Convert.ToDouble(dr["rr_lifted_qty"].ToString());
                                 record.record_active= dr["record_active"].ToString();
                                indets.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Indent List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Indent List Success :" + ex.Message);
                }

            }
            return indets;
        }

        public static List<Indent_Form> Search(string tablename, string column, string value)
        {
            List<Indent_Form> mir = new List<Indent_Form>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.view_mf1_indent_listdetails where  " + column + " Ilike '%" + value + "%' order by product_name", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            mir = new List<Indent_Form>();
                            while (dr.Read())
                            {
                                Indent_Form record = new Indent_Form();
                                record.indent_qty = Convert.ToDouble(dr["indent_qty"].ToString());
                                record.financial_year = dr["financial_year"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.molasses_indent_id = dr["molasses_indent_id"].ToString();
                                record.product_name = dr["product_name"].ToString();
                                record.product_code = dr["product_code"].ToString();
                                record.party_code = dr["party_code"].ToString();
                                record.molasses_allocated_qty = Convert.ToDouble(dr["Allotted_qty"].ToString());
                                record.molasses_lifted_qty = Convert.ToDouble(dr["rr_lifted_qty"].ToString());
                                // record.alloted_no = dr["final_allotmentno"].ToString();
                                mir.Add(record);
                         
                            }
                        }
                    }
                    cn.Close();

                }
                catch (Exception ex)
                {
                    throw (ex);
                }

            }
            return mir;
        }



        public static Indent_Form GetDetails(string id, string financial_year)
        {
            Indent_Form indent = new Indent_Form();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("Select * from exciseautomation.molasses_indent where molasses_indent_id='"+id+"' and financial_year='"+financial_year+"' ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr1 = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr1);
                        dr1.Close();
                        foreach(DataRow dr in dt.Rows)
                            {

                                indent.financial_year =dr["financial_year"].ToString();
                                indent.indent_date = dr["indent_date"].ToString().Substring(0, 10).Replace("/","-"); 
                                indent.indent_qty = Convert.ToDouble(dr["indent_qty"].ToString());
                                indent.is_captive = dr["is_captive"].ToString();
                                indent.captive_unit_name = dr["from_unit"].ToString();
                                indent.product_code = dr["product_code"].ToString();
                                //////////////Quantity of molasses required///////////////
                                indent.req_cs = Convert.ToDouble(dr["req_cs"].ToString());
                            indent.req_rs = Convert.ToDouble(dr["req_rs"].ToString());
                            indent.req_pa = Convert.ToDouble(dr["req_pa"].ToString());
                            indent.req_ds = Convert.ToDouble(dr["req_ds"].ToString());
                            //////////////Quantity of molasses Recieved///////////////
                            indent.recd_pycs = Convert.ToDouble(dr["recd_pycs"].ToString());
                            indent.recd_pyrs = Convert.ToDouble(dr["recd_pyrs"].ToString());
                            indent.recd_pypa = Convert.ToDouble(dr["recd_pypa"].ToString());
                            indent.recd_pyds = Convert.ToDouble(dr["recd_pyds"].ToString());
                            //////////////Quantity of molasses PR Used///////////////
                            indent.used_pycs = Convert.ToDouble(dr["used_pycs"].ToString());
                            indent.used_pyrs = Convert.ToDouble(dr["used_pyrs"].ToString());
                            indent.used_pypa = Convert.ToDouble(dr["used_pypa"].ToString());
                            indent.used_pyds = Convert.ToDouble(dr["used_pyds"].ToString());
                            //////////////Quantity of molasses cr Used///////////////
                            indent.used_cycs = Convert.ToDouble(dr["used_cycs"].ToString());
                            indent.used_cyrs = Convert.ToDouble(dr["used_cyrs"].ToString());
                            indent.used_cypa = Convert.ToDouble(dr["used_cypa"].ToString());
                            indent.used_cyds = Convert.ToDouble(dr["used_cyds"].ToString());
                            ////////////Last Menu///////////////
                            indent.molasses_distilled = Convert.ToDouble(dr["molasses_distilled"].ToString());
                                indent.working_wastage = Convert.ToDouble(dr["working_wastage"].ToString());
                                indent.transit_wastage = Convert.ToDouble(dr["transit_wastage"].ToString());
                                indent.molasses_instock_cy = Convert.ToDouble(dr["molasses_instock_cy"].ToString());
                                indent.molasses_recd_cy = Convert.ToDouble(dr["molasses_recd_cy"].ToString());
                                indent.molasses_used_cy = Convert.ToDouble(dr["molasses_used_cy"].ToString());
                                indent.molasses_to_be_lifted = Convert.ToDouble(dr["molasses_to_be_lifted"].ToString());
                                indent.molasses_to_consume = Convert.ToDouble(dr["molasses_to_consume"].ToString());
                                indent.molasses_instock = Convert.ToDouble(dr["molasses_instock"].ToString());
                                indent.molasses_lifted_qty = Convert.ToDouble(dr["molasses_lifted_qty"].ToString());
                                indent.molasses_bal_allotment = Convert.ToDouble(dr["molasses_bal_allotment"].ToString());
                                indent.molasses_bal_storage = Convert.ToDouble(dr["molasses_bal_storage"].ToString());
                                indent.party_code = dr["party_code"].ToString();
                                indent.molasses_indent_reqno = dr["molasses_indent_reqno"].ToString();
                                indent.record_id_format = dr["record_id_format"].ToString();
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.eascm_docs where doc_id='" + id + "' and doc_type_code='IND' and  financial_year='" + financial_year + "' order by eascm_docs_id", cn))
                            {
                                cmd1.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr2 = cmd1.ExecuteReader();
                                indent.docs = new List<EASCM_DOCS>();
                                if (dr2.HasRows)
                                {

                                    while (dr2.Read())
                                    {
                                        EASCM_DOCS doc = new EASCM_DOCS();
                                        doc.id = Convert.ToInt32(dr2["eascm_docs_id"].ToString());
                                        doc.doc_id = dr2["doc_id"].ToString();
                                        doc.doc_name = dr2["doc_Name"].ToString();
                                        doc.description = dr2["doc_desc"].ToString();
                                        doc.doc_path = dr2["doc_path"].ToString();
                                        indent.docs.Add(doc);
                                    }

                                }

                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Indent Details Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Indent Details Success :" + ex.Message);
                }

            }
            return indent;
        }
    }
}
