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
    public class DL_NOC_Details
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static string GetMax()
        {
            string value = "0";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select case when max(noc_id) is null then 0 else max(noc_id) end as noc_id from exciseautomation.noc", cn);
                    int maxid = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                    value = maxid.ToString();
                    _log.Info("Get NOC Sucess:" + maxid);
                }
                catch (Exception ex)
                {
                    value = ex.Message;
                    _log.Info("Get NOC Fail:" + ex.Message);
                }
            }
            return value;
        }

        public static string GetPartyMax(string party_code,string financial_year)
        {
            string value = "0";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select case when max(req_nocno) is null then 0 else max(req_nocno) end as req_nocno,g.noc from exciseautomation.noc a inner join exciseautomation.document_format_master g on g.party_code=a.party_code where a.party_code='" + party_code+"' and a.financial_year='"+financial_year+"' group by a.party_code,g.noc", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {

                        value = "Req/" + dr["noc"].ToString() +financial_year +"/"+ Convert.ToInt32(Convert.ToInt32(cmd.ExecuteScalar()) + 1);
                    }
                    if(dt.Rows.Count==0)
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("select noc from  exciseautomation.document_format_master  where party_code='" + party_code + "'", cn);
                        value = "Req/" + cmd1.ExecuteScalar() + financial_year +"/1";
                    }
                    _log.Info("Get NOC Sucess:" + value);
                }
                catch (Exception ex)
                {
                    value = ex.Message;
                    _log.Info("Get NOC Fail:" + ex.Message);
                }
            }
            return value;
        }

        public static string Approve(NOC_Details noc)
        {
            string value;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    if (noc.record_status == "I")
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand("select case when max(issue_nocno) is null then 0 else max(issue_nocno) end as issue_nocno from exciseautomation.noc where party_code='"+noc.party_code+"' and financial_year='"+noc.financial_year+"'", cn);
                        int issue_nocno = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                        cmd = new NpgsqlCommand("update exciseautomation.noc set record_status='" + noc.record_status + "',noc_status='" + noc.noc_status + "',issue_date='"+DateTime.Now.ToString("dd-MM-yyyy")+ "',issue_nocno='"+ issue_nocno+ "' where party_code='" + noc.party_code + "' and noc_id='" + noc.noc_id + "' and financial_year='" + noc.financial_year + "'", cn);
                        int n = cmd.ExecuteNonQuery();
                        StringBuilder str = new StringBuilder();
                        str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                        str.Append("'" + noc.noc_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "','NOC','" + noc.noc_status + "','" + noc.remarks + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + noc.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + noc.user_id + "','"+noc.financial_year+"','"+noc.party_code+"')");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        n = cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        NpgsqlCommand cmd;
                       

                            if (noc.valid_upto == "")
                                cmd = new NpgsqlCommand("update exciseautomation.noc set record_status='" + noc.record_status + "',noc_status='" + noc.noc_status + "',approver_level='" + noc.approverlevel + "' where  party_code='" + noc.party_code + "' and noc_id='" + noc.noc_id + "' and financial_year='" + noc.financial_year + "'", cn);
                            else
                       
                                cmd = new NpgsqlCommand("update exciseautomation.noc set record_status='" + noc.record_status + "',noc_status='" + noc.noc_status + "',approver_level='" + noc.approverlevel + "',valid_upto='" + noc.valid_upto + "' where  party_code='" + noc.party_code + "' and noc_id='" + noc.noc_id + "' and financial_year='" + noc.financial_year + "'", cn);
                            int n = cmd.ExecuteNonQuery();
                        if((noc.approverlevel==2 && noc.record_status!="I")|| (noc.user_id== "hodyco" && noc.record_status == "B"))
                        {
                            if (noc.depot != null)
                            {
                                cmd = new NpgsqlCommand("update exciseautomation.noc set record_status='" + noc.record_status + "',noc_status='" + noc.noc_status + "',approver_level='" + noc.approverlevel + "',noc_total_qty='" + noc.noc_total_qty + "' where  party_code='" + noc.party_code + "' and noc_id='" + noc.noc_id + "' and financial_year='" + noc.financial_year + "'", cn);
                                cmd.ExecuteNonQuery();
                                for (int i = 0; i < noc.depot.Count; i++)
                                {
                                   
                                    StringBuilder str1 = new StringBuilder();
                                    str1.Append("Update exciseautomation.noc_depotdetail set  totalqty='" + noc.depot[i].qty + "',lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "' where noc_id='" + noc.noc_id + "' and noc_depotdetail_id='" + noc.depot[i].Depot_id + "' and financial_year='" + noc.financial_year + "'");
                                    NpgsqlCommand cmd3 = new NpgsqlCommand(str1.ToString(), cn);
                                    cmd3.ExecuteNonQuery();

                                }
                            }
                        }
                        if (noc.record_status != "R")
                        {
                            StringBuilder str1 = new StringBuilder();
                            for (int i1 = 0; i1 < noc.docs.Count; i1++)
                            {
                                str1 = new StringBuilder();
                                str1.Append("INSERT INTO exciseautomation.eascm_docs(doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,financial_year,party_code)");
                                str1.Append("Values('" + noc.noc_id + "','" + noc.docs[i1].doc_name + "', '" + noc.docs[i1].description + "','" + noc.docs[i1].doc_path + "','NOC','" + noc.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+noc.financial_year+"','"+noc.party_code+"')");
                                NpgsqlCommand cmd3 = new NpgsqlCommand(str1.ToString(), cn);
                                int r = cmd3.ExecuteNonQuery();
                            }
                        }
                        StringBuilder str = new StringBuilder();
                        str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                        str.Append("'" + noc.noc_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "','NOC','" + noc.noc_status + "','" + noc.approver_remarks + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + noc.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + noc.user_id + "','"+noc.financial_year+"','"+noc.party_code+"')");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        n = cmd.ExecuteNonQuery();
                    }
                    
                   
                    value = "0";
                    trn.Commit();
                    _log.Info("NOC Approve Sucess:" + noc.noc_id);
                    cn.Close();
                }
                catch (Exception ex)
                {
                    value = ex.Message;
                    _log.Info("NOC Approve Fail:" + noc.noc_id + "-" + value);
                    trn.Rollback();
                }
                return value;

            }
        }


        public static string AdminUpdate(NOC_Details noc)
        {
            string value;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                   
                        NpgsqlCommand cmd;
                            cmd = new NpgsqlCommand("update exciseautomation.noc set valid_upto='" + noc.valid_upto + "' where noc_id='" + noc.noc_id + "' and financial_year='" +noc.financial_year + "'", cn);
                        int n = cmd.ExecuteNonQuery();
                        StringBuilder str = new StringBuilder();
                        str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year) VALUES(");
                        str.Append("'" + noc.noc_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "','NOC','Updated By Admin','" + noc.record_status + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + noc.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + noc.user_id + "','"+noc.financial_year+"')");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        n = cmd.ExecuteNonQuery();
                    value = "0";
                    trn.Commit();
                    _log.Info("NOC Approve Sucess:" + noc.noc_id);
                    cn.Close();
                }
                catch (Exception ex)
                {
                    value = ex.Message;
                    _log.Info("NOC Approve Fail:" + noc.noc_id + "-" + value);
                    trn.Rollback();
                }
                return value;

            }
        }

        public static NOC_Details GetDetails(string noc_id, string financial_year, string party_code)
        {
            NOC_Details noc = new NOC_Details();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.cust_name, b.cust_address,f.state_name,b.district_name,c.product_name,d.noc from exciseautomation.noc a inner join exciseautomation.customer_master b on a.customer_id=b.customer_id inner join exciseautomation.product_master c on c.product_code=a.noc_for left join exciseautomation.document_format_master d on d.party_code=a.party_code inner join exciseautomation.state_master f on b.state_code=f.state_code  where noc_id='"+noc_id+"' and financial_year='"+financial_year+"' and a.party_code='"+party_code+"'  order by noc_id", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        noc.noc_id = dr["noc_id"].ToString();
                        noc.financial_year = dr["financial_year"].ToString();
                        noc.req_nocno ="Req/"+dr["noc"] + dr["financial_year"].ToString() + "/"+ dr["req_nocno"].ToString();
                        noc.customer_id = dr["customer_id"].ToString();
                        noc.Cust_name = dr["cust_name"].ToString();
                        noc.state = dr["state_name"].ToString();
                        noc.district = dr["district_name"].ToString();
                        noc.cust_address = dr["cust_address"].ToString();
                        noc.noc_total_qty = Convert.ToDouble(dr["noc_total_qty"].ToString());
                        noc.record_status = dr["record_status"].ToString();
                        noc.noc_status = dr["noc_status"].ToString();
                        noc.tenderno = dr["tenderno"].ToString();
                        noc.party_code = dr["party_code"].ToString();
                        noc.nocdate = dr["nocdate"].ToString().Substring(0, 10).Replace("/", "-");
                        noc.noc_for = dr["noc_for"].ToString();
                        noc.product_name = dr["product_name"].ToString();
                        noc.financial_year = dr["financial_year"].ToString();
                        noc.number_type = dr["number_type"].ToString();
                        if (dr["number_issuedate"].ToString()!="" && dr["number_issuedate"].ToString()!=null)
                        noc.number_issuedate = dr["number_issuedate"].ToString().Substring(0, 10).Replace("/", "-");
                        noc.remarks = dr["remarks"].ToString();
                        noc.user_id = dr["user_id"].ToString();
                        if (dr["valid_upto"].ToString()!="")
                        noc.valid_upto = dr["valid_upto"].ToString().Substring(0,10).Replace("/","-");
                        noc.approverlevel = Convert.ToInt32(dr["approver_level"]);
                        if (dr["issue_date"].ToString()!="")
                        noc.issue_date=dr["issue_date"].ToString().Substring(0, 10).Replace("/", "-");
                        noc.pono = dr["pono"].ToString();
                        cmd = new NpgsqlCommand("select case when sum(req_qty) is null then 0 else sum(req_qty) end as req_qty from exciseautomation.request_for_pass where record_status!='R' and  financial_year='"+financial_year+"' and rrnoc_request_id='" + noc.noc_id + "'", cn);
                        noc.noc_lifted_qty= Convert.ToDouble(cmd.ExecuteScalar().ToString());
                        cmd = new NpgsqlCommand("select * from exciseautomation.noc_depotdetail where noc_id='" + noc.noc_id + "' and financial_year='" + financial_year + "' and user_id='"+noc.user_id+"' and record_deleted is false order by noc_id", cn);
                        NpgsqlDataReader dr3 = cmd.ExecuteReader();
                        if (dr3.HasRows)
                        {
                            noc.depot = new List<NOC_Depot>();
                            while (dr3.Read())
                            {
                                NOC_Depot dep = new NOC_Depot();
                                dep.Depot_name = dr3["Depot"].ToString();
                                dep.Depot_id = dr3["noc_depotdetail_id"].ToString();
                               if( dr3["totalqty"].ToString()!="")
                                {
                                    dep.qty = Convert.ToDouble(dr3["totalqty"].ToString());
                                }
                                else
                                {
                                    dep.qty = 0;
                                }
                                
                                dep.reqqty = Convert.ToDouble(dr3["req_qty"].ToString());

                                noc.depot.Add(dep);
                            }
                        }
                        dr3.Close();
                        using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.eascm_docs where doc_id='" + noc_id + "' and doc_type_code='NOC' and financial_year='" + financial_year + "' and user_id='" + noc.user_id + "' order by eascm_docs_id", cn))
                        {
                            cmd1.CommandType = System.Data.CommandType.Text;
                            //cmd.Parameters.AddWithValue("@UserID", userid);
                            NpgsqlDataReader dr2 = cmd1.ExecuteReader();
                            noc.docs = new List<EASCM_DOCS>();
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
                                    doc.user_id = dr2["user_id"].ToString();
                                    noc.docs.Add(doc);
                                }

                            }
                        }
                    }
                     cn.Close();
                    _log.Info("Get NOC Details Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get NOC Details Fail:" + ex.Message);

                }
            }
            return noc;
        }

        public static List<NOC_Details> GetNOCList()
        {
            List<NOC_Details> noc = new List<NOC_Details>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.Cust_name,c.district_code,c.party_name,d.noc,f.product_name from exciseautomation.noc a inner join exciseautomation.customer_master b on a.customer_id=b.customer_id and a.party_code=b.party_code inner join exciseautomation.party_master c on a.party_code=c.party_code left join exciseautomation.document_format_master d on a.party_code=d.party_code inner join exciseautomation.product_master f on a.noc_for =f.product_code   order by a.party_code,req_nocno,a.valid_upto", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                       foreach(DataRow dr in dt.Rows)
                        {
                            NOC_Details no = new NOC_Details();
                            no.noc_id = dr["noc_id"].ToString();
                        no.financial_year = dr["financial_year"].ToString();
                        no.req_nocno ="Req/"+ dr["noc"].ToString() + dr["financial_year"].ToString() + "/" + dr["req_nocno"].ToString();
                        no.slno=Convert.ToInt32( dr["req_nocno"].ToString());
                        no.Cust_name = dr["Cust_name"].ToString().Trim();
                            no.noc_total_qty =Convert.ToDouble( dr["noc_total_qty"].ToString());
                            no.record_status = dr["record_status"].ToString();
                            if(dr["valid_upto"].ToString()!="")
                            no.valid_upto = dr["valid_upto"].ToString().Substring(0,10).Replace("/","-");
                            no.noc_status = dr["noc_status"].ToString();
                            no.party_code = dr["party_code"].ToString();
                        no.noc_for = dr["noc_for"].ToString();
                        no.product_name= dr["product_name"].ToString();
                        no.party_name = dr["party_name"].ToString().Trim();
                            if(dr["issue_nocno"].ToString()!="")
                            no.issue_nocno = dr["noc"].ToString() + dr["financial_year"].ToString() + "/" + dr["issue_nocno"].ToString();
                            no.district = dr["District_Code"].ToString();
                          //  no.req_qty = Convert.ToDouble(dr["req_qty"].ToString());
                        //if(no.party_code=="MDD")
                        cmd = new NpgsqlCommand("select case when sum(req_qty) is null then 0 else sum(req_qty) end as req_qty from exciseautomation.request_for_pass where record_status!='R' and rrnoc_request_id='" + no.noc_id + "' and pass_type='NOC' and party_code='" + no.party_code + "'", cn);
                        no.noc_lifted_qty = Convert.ToDouble(cmd.ExecuteScalar().ToString());
                        //cmd = new NpgsqlCommand("select case when sum(totalqty) is null then 0 else sum(totalqty) end as totalqty from exciseautomation.noc_depotdetail where noc_id='" + no.noc_id+"'", cn);
                        //    no.req_qty = Convert.ToDouble(cmd.ExecuteScalar().ToString());
                        cmd = new NpgsqlCommand("select case when sum(req_qty) is null then 0 else sum(req_qty) end as totalqty from exciseautomation.noc_depotdetail where noc_id='" + no.noc_id + "' and financial_year='"+no.financial_year+"'", cn);
                        no.req_qty = Convert.ToDouble(cmd.ExecuteScalar().ToString());
                        cmd = new NpgsqlCommand("select case when sum(totalqty) is null then 0 else sum(totalqty) end as totalqty from exciseautomation.noc_depotdetail where noc_id='" + no.noc_id + "' and financial_year='" + no.financial_year + "'", cn);
                        no.Approved_qty = Convert.ToDouble(cmd.ExecuteScalar().ToString());
                        //if(n<= no.noc_total_qty)
                        noc.Add(no);
                        }
                    
                    cn.Close();
                    _log.Info("Get RR List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get RR List Fail:" + ex.Message);

                }
            }
            return noc;
        }
        public static List<NOC> GetNOCList1()
        {
            List<NOC> noc = new List<NOC>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.Cust_name,c.district_code,c.party_name,d.noc,f.product_name from exciseautomation.noc a inner join exciseautomation.customer_master b on a.customer_id=b.customer_id and a.party_code=b.party_code inner join exciseautomation.party_master c on a.party_code=c.party_code left join exciseautomation.document_format_master d on a.party_code=d.party_code inner join exciseautomation.product_master f on a.noc_for =f.product_code  order by a.party_code,req_nocno,a.valid_upto", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt1 = new DataTable();
                    dt1.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt1.Rows)
                    {
                        NOC no = new NOC();
                        no.noc_id = dr["noc_id"].ToString();
                        no.financial_year = dr["financial_year"].ToString().Trim();
                        no.req_nocno = "Req/" + dr["noc"].ToString() + dr["financial_year"].ToString() + "/" + dr["req_nocno"].ToString();
                        no.slno = Convert.ToInt32(dr["req_nocno"].ToString());
                        no.Cust_name = dr["Cust_name"].ToString().Trim();
                      
                        no.noc_total_qty = Convert.ToDouble(dr["noc_total_qty"].ToString());
                        no.record_status = dr["record_status"].ToString();
                        if (dr["valid_upto"].ToString() != "")
                            no.valid_upto = dr["valid_upto"].ToString().Substring(0, 10).Replace("/", "-");
                        no.noc_status = dr["noc_status"].ToString();
                        no.party_code = dr["party_code"].ToString();
                        no.noc_for = dr["noc_for"].ToString();
                        no.product_name = dr["product_name"].ToString();
                        no.party_name = dr["party_name"].ToString().Trim();
                        no.user_id = dr["user_id"].ToString().Trim();
                        if (dr["issue_nocno"].ToString() != "")
                            no.issue_nocno = dr["noc"].ToString() + dr["financial_year"].ToString() + "/" + dr["issue_nocno"].ToString();
                        no.district = dr["District_Code"].ToString();
                        //  no.req_qty = Convert.ToDouble(dr["req_qty"].ToString());
                        //if(no.party_code=="MDD")
                        cmd = new NpgsqlCommand("select case when sum(req_qty) is null then 0 else sum(req_qty) end as req_qty from exciseautomation.request_for_pass where record_status!='R' and rrnoc_request_id='" + no.noc_id + "' and pass_type='NOC' and party_code='" + no.party_code + "'", cn);
                        no.noc_lifted_qty = Convert.ToDouble(cmd.ExecuteScalar().ToString());
                        cmd = new NpgsqlCommand("select case when sum(req_qty) is null then 0 else sum(req_qty) end as totalqty from exciseautomation.noc_depotdetail where noc_id='" + no.noc_id + "' and financial_year='" + no.financial_year + "' and user_id='" + no.user_id +"'", cn);
                        no.req_qty = Convert.ToDouble(cmd.ExecuteScalar().ToString());
                        cmd = new NpgsqlCommand("select case when sum(totalqty) is null then 0 else sum(totalqty) end as totalqty from exciseautomation.noc_depotdetail where noc_id='" + no.noc_id + "' and financial_year='" + no.financial_year + "' and user_id='" + no.user_id + "'", cn);
                        no.Approved_qty = Convert.ToDouble(cmd.ExecuteScalar().ToString());
                        //if(n<= no.noc_total_qty)
                        noc.Add(no);
                    }

                    cn.Close();
                    _log.Info("Get RR List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get RR List Fail:" + ex.Message);

                }
            }
            return noc;
        }

        public static List<NOC> Search(string tablename, string column, string value)
        {
            List<NOC> mir = new List<NOC>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd1;
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.Cust_name,c.district_code,c.party_name,d.noc,f.product_name from exciseautomation.noc a inner join exciseautomation.customer_master b on a.customer_id=b.customer_id and a.party_code=b.party_code inner join exciseautomation.party_master c on a.party_code=c.party_code left join exciseautomation.document_format_master d on a.party_code=d.party_code inner join exciseautomation.product_master f on a.noc_for =f.product_code inner join exciseautomation.noc_depotdetail g on a.noc_id=g.noc_id and a.financial_year=g.financial_year where " + column + " Ilike '%" + value + "%'  order by a.party_code,req_nocno,a.valid_upto", cn))
                    {
                        NpgsqlDataReader dr1 = cmd.ExecuteReader();
                        DataTable dt1 = new DataTable();
                        dt1.Load(dr1);
                        dr1.Close();
                        foreach (DataRow dr in dt1.Rows)
                        {
                            
                                NOC no = new NOC();
                                no.noc_id = dr["noc_id"].ToString();
                            no.financial_year = dr["financial_year"].ToString().Trim();
                            no.req_nocno = "Req/" + dr["noc"].ToString() + dr["financial_year"].ToString() + "/" + dr["req_nocno"].ToString();
                                no.slno = Convert.ToInt32(dr["req_nocno"].ToString());
                                no.Cust_name = dr["Cust_name"].ToString().Trim();
                          
                            no.noc_total_qty = Convert.ToDouble(dr["noc_total_qty"].ToString());
                                no.record_status = dr["record_status"].ToString();
                                if (dr["valid_upto"].ToString() != "")
                                    no.valid_upto = dr["valid_upto"].ToString().Substring(0, 10).Replace("/", "-");
                                no.noc_status = dr["noc_status"].ToString();
                                no.party_code = dr["party_code"].ToString();
                                no.noc_for = dr["noc_for"].ToString();
                                no.product_name = dr["product_name"].ToString();
                                no.party_name = dr["party_name"].ToString().Trim();
                                if (dr["issue_nocno"].ToString() != "")
                                    no.issue_nocno = dr["noc"].ToString() + dr["financial_year"].ToString() + "/" + dr["issue_nocno"].ToString();
                                no.district = dr["District_Code"].ToString();
                                //  no.req_qty = Convert.ToDouble(dr["req_qty"].ToString());
                                //if(no.party_code=="MDD")
                                cmd1 = new NpgsqlCommand("select case when sum(req_qty) is null then 0 else sum(req_qty) end as req_qty from exciseautomation.request_for_pass where record_status!='R' and rrnoc_request_id='" + no.noc_id + "' and pass_type='NOC' and party_code='" + no.party_code + "'", cn);
                                no.noc_lifted_qty = Convert.ToDouble(cmd1.ExecuteScalar().ToString());
                                cmd1 = new NpgsqlCommand("select case when sum(req_qty) is null then 0 else sum(req_qty) end as totalqty from exciseautomation.noc_depotdetail where noc_id='" + no.noc_id + "' and financial_year='" + no.financial_year + "'", cn);
                                no.req_qty = Convert.ToDouble(cmd1.ExecuteScalar().ToString());
                                cmd1 = new NpgsqlCommand("select case when sum(totalqty) is null then 0 else sum(totalqty) end as totalqty from exciseautomation.noc_depotdetail where noc_id='" + no.noc_id + "' and financial_year='" + no.financial_year + "'", cn);
                                no.Approved_qty = Convert.ToDouble(cmd1.ExecuteScalar().ToString());
                                //if(n<= no.noc_total_qty)
                                mir.Add(no);

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


        public static string Insert(NOC_Details noc)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                   // NpgsqlCommand cmd1 = new NpgsqlCommand("select case when max(req_nocno) is null then 0 else max(req_nocno) end as req_nocno from exciseautomation.noc where party_code='"+noc.party_code+"'", cn);
                   // int maxid = Convert.ToInt32(cmd1.ExecuteScalar()) + 1;
                    StringBuilder str = new StringBuilder();
                    string[] noc_reqno = noc.req_nocno.Split('/');
                    str.Append("INSERT INTO exciseautomation.noc(noc_id, nocdate, financial_year, req_nocno, customer_id, tenderno, pono, noc_for, noc_total_qty, party_code, ");
                    str.Append("remarks,  creation_date,  user_id, record_status,approver_level,number_type,number_issuedate)values(");
                    str.Append("'" + noc.noc_id + "','"+noc.nocdate+"','"+noc.financial_year+"','"+ noc_reqno [noc_reqno .Length-1]+ "','"+noc.customer_id+"','"+noc.tenderno+"','"+noc.pono+"','"+noc.noc_for+"','"+noc.noc_total_qty+"','"+noc.party_code+"',");
                    str.Append("'" + noc.remarks + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + noc.user_id + "','" + noc.record_status + "','0','"+noc.number_type+"','"+noc.number_issuedate+"')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        VAL = "0";
                        for (int i = 0; i < noc.depot.Count; i++)
                        {
                            NpgsqlCommand cmd4 = new NpgsqlCommand("SELECT max(noc_depotdetail_id) FROM exciseautomation.noc_depotdetail ", cn);
                            string f = cmd4.ExecuteScalar().ToString();
                            int c = 0;
                            if (f == "")
                                c = 1;
                            else
                                c = Convert.ToInt32(f) + 1;
                            n = 0;
                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.noc_depotdetail( noc_depotdetail_id,noc_id, depot,req_qty, user_id,creation_date,financial_year,party_code)");
                            str.Append("Values('"+c+"','" + noc.noc_id + "','" + noc.depot[i].Depot_name + "','" + noc.depot[i].reqqty + "','" + noc.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+noc.financial_year+"','"+noc.party_code+"')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd3.ExecuteNonQuery();

                        }
                        for (int i1 = 0; i1 < noc.docs.Count; i1++)
                        {

                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.eascm_docs(doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,financial_year,party_code)");
                            str.Append("Values('" + noc.noc_id + "','" + noc.docs[i1].doc_name + "', '" + noc.docs[i1].description + "','" + noc.docs[i1].doc_path + "','NOC','" + noc.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+noc.financial_year+"','"+noc.party_code+"')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            int r = cmd3.ExecuteNonQuery();
                        }
                    }
                    else
                    VAL = "1";
                    trn.Commit();
                    cn.Close();
                    _log.Info("NOC Insertion Sucess:" + noc.noc_id + '-' + noc.party_code);

                }
                catch (Exception ex1)
                {
                    _log.Info("NOC Insertion Fail:" + noc.noc_id + '-' + noc.party_code + " - " + ex1.Message);
                    VAL = ex1.Message;
                    trn.Rollback();
                }

            }
            return VAL;
        }
        public static string Update(NOC_Details noc)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    
                    StringBuilder str = new StringBuilder();
                    str.Append("Update exciseautomation.noc set noc_status='', customer_id='"+noc.customer_id+"', tenderno='"+noc.tenderno+"', pono='"+noc.pono+"', noc_for='"+noc.noc_for+"', noc_total_qty='"+noc.noc_total_qty+"', ");
                    str.Append("remarks='"+noc.remarks+ "',  lastmodified_date='"+DateTime.Now.ToString("dd-MM-yyyy)")+"',  user_id='"+noc.user_id+"', record_status='"+noc.record_status+ "',approver_level='0',number_type='"+noc.number_type+ "',number_issuedate='"+noc.number_issuedate+"' where noc_id='" + noc.noc_id+ "' and financial_year='" + noc.financial_year + "' and party_code='"+noc.party_code+"'");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {

                        NpgsqlCommand cmd1 = new NpgsqlCommand("delete from exciseautomation.noc_depotdetail where noc_id='" + noc.noc_id + "' and financial_year='"+noc.financial_year+"' ", cn);
                        cmd1.ExecuteNonQuery();
                        for (int i = 0; i < noc.depot.Count; i++)
                        {
                            NpgsqlCommand cmd4 = new NpgsqlCommand("SELECT max(noc_depotdetail_id) FROM exciseautomation.noc_depotdetail ", cn);
                            string f = cmd4.ExecuteScalar().ToString();
                            int c = 0;
                            if (f == "")
                                c = 1;
                            else
                                c = Convert.ToInt32(f) + 1;
                            n = 0;
                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.noc_depotdetail( noc_depotdetail_id,noc_id, depot,req_qty, user_id,creation_date,financial_year,party_code)");
                            str.Append("Values('" + c + "','" + noc.noc_id + "','" + noc.depot[i].Depot_name + "','" + noc.depot[i].reqqty + "','" + noc.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + noc.financial_year + "','"+noc.party_code+"')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd3.ExecuteNonQuery();

                        }

                        NpgsqlCommand cmd2 = new NpgsqlCommand("delete from exciseautomation.eascm_docs where doc_id='" + noc.noc_id + "' and doc_type_code='NOC' and  financial_year='" + noc.financial_year + "' and party_code='"+noc.party_code+"'", cn);
                        cmd2.ExecuteNonQuery();
                        for (int i = 0; i < noc.docs.Count; i++)
                        {
                            n = 0;
                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.eascm_docs(doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,financial_year,party_code)");
                            str.Append("Values('" + noc.noc_id + "','" + noc.docs[i].doc_name + "', '" + noc.docs[i].description + "','" + noc.docs[i].doc_path + "','NOC','" + noc.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+noc.financial_year+"','"+noc.party_code+"')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd3.ExecuteNonQuery();
                        }
                        string[] values = noc.depot[0].deleted_id.Split('_');
                        if (values.Length >= 1&& noc.depot[0].deleted_id != "")
                        {
                            for (int i = 0; i < values.Length; i++)
                            {
                                cmd2 = new NpgsqlCommand("update exciseautomation.noc_depotdetail set record_deleted=true where  noc_id='" + noc.noc_id + "' and noc_depotdetail_id='" + values[i] + "' and financial_year='" + noc.financial_year + "'", cn);
                                cmd2.ExecuteNonQuery();
                            }
                        }
                            VAL = "0";
                    }
                    trn.Commit();
                    cn.Close();
                    // _log.Info("Module Insertion Sucess:" + module.module_code + '-' + module.module_name);

                }
                catch (Exception ex1)
                {
                    // _log.Info("Module Insertion Fail:" + module.module_code + '-' + module.module_name + "-" + ex1.Message);
                    trn.Rollback();
                    VAL = ex1.Message;
                }

            }
            return VAL;
        }

    }
}
