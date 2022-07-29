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
    public class DL_ReaquestForPass
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static string Insert(ReaquestForPass pass)
        {
            string val = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select case when max(request_for_pass_id) is null then 0 else max(request_for_pass_id) end as request_for_pass_id from exciseautomation.request_for_pass where financial_year='"+pass.financial_year+"' ", cn);
                    int max = Convert.ToInt32(cmd.ExecuteScalar().ToString()) + 1;
                    if(pass.pass_type=="M")
                    {
                        pass.pass_type = "RR";
                        StringBuilder str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.request_for_pass(request_for_pass_id, rrnoc_request_id,party_code, req_qty,approved_qty,  user_id, creation_date, record_status,pass_type,financial_year)values(");
                        str.Append("'" + max + "','" + pass.rrnoc_request_id + "','"+pass.party_code+"','" + pass.req_qty + "','" + pass.req_qty + "','" + pass.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + pass.record_status + "','" + pass.pass_type + "','"+pass.financial_year+"')");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        pass.pass_type = "NOC";
                        StringBuilder str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.request_for_pass(request_for_pass_id, rrnoc_request_id, req_qty,approved_qty,  user_id, creation_date, record_status,pass_type,noc_depotdetail_id,party_code,financial_year)values(");
                        str.Append("'" + max + "','" + pass.rrnoc_request_id + "','" + pass.req_qty + "','" + pass.req_qty + "','" + pass.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + pass.record_status + "','" + pass.pass_type + "','"+pass.noc_depotdetail_id+"','"+pass.party_code+"','"+pass.financial_year+"')");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();
                    }
                   

                    val = "0";
                    cn.Close();
                    _log.Info("Insert Request Pass Success");
                }
                catch (Exception ex)
                {
                    val = ex.Message;
                    _log.Info("Insert Request Pass fail :" + ex.Message);
                }

            }
            return val;
        }

        public static ReaquestForPass GetRequest(string requestid, string pass_type,string party_code,string party_type, string financial_year)
        {
            ReaquestForPass request = new ReaquestForPass();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    if (pass_type == "M" || pass_type == "RR")
                    {
                        pass_type = "RR";
                        if(party_type=="All")
                        {
                            cmd = new NpgsqlCommand("select a.*,b.valid_date,b.molasses_supplier,b.rr_reqno,b.rr_issueno,b.rr_allotmentno,b.rr_approved_qty,b.rr_balance_qty,b.rr_lifted_qty,c.Product_name,c.Product_code,d.party_name,d.party_address,e.release_request,e.molasses_allotment from exciseautomation.request_for_pass a inner join exciseautomation.release_request b on a.rrnoc_request_id=b.release_request_id inner join exciseautomation.product_master as c on b.product_code=c.product_code inner join  exciseautomation.party_master d on d.party_code=b.party_code left join exciseautomation.document_format_master e on b.party_code=e.party_code  where a.request_for_pass_id='" + requestid + "' and pass_type='" + pass_type + "'    order by a.request_for_pass_id", cn);
                        }
                       else if(party_type!= "Distillery Unit")
                        {
                            // cmd = new NpgsqlCommand("select a.*,b.valid_date,b.molasses_supplier,b.rr_reqno,b.rr_issueno,b.rr_allotmentno,b.rr_approved_qty,b.rr_balance_qty,b.rr_lifted_qty,c.Product_name,c.Product_code,d.party_name,d.party_address,e.release_request,e.molasses_allotment from exciseautomation.request_for_pass a inner join exciseautomation.release_request b on a.rrnoc_request_id=b.release_request_id left join exciseautomation.product_master as c on b.product_code=c.product_code left join  exciseautomation.party_master d on d.party_code=b.molasses_supplier left join exciseautomation.document_format_master e on b.party_code=e.party_code where a.request_for_pass_id='" + requestid + "' and pass_type='" + pass_type + "' and b.molasses_supplier='" + party_code + "' and a.financial_year='"+financial_year+"'  order by a.request_for_pass_id", cn);
                            cmd = new NpgsqlCommand("select a.*,b.valid_date,b.molasses_supplier,b.rr_reqno,b.rr_issueno,b.rr_allotmentno,b.rr_approved_qty,b.rr_balance_qty,b.rr_lifted_qty,c.Product_name,c.Product_code,d.party_name,d.party_address,e.release_request,e.molasses_allotment,( SELECT sum(CASE WHEN pa.dispatch_qty IS NULL THEN 0 ELSE pa.dispatch_qty END) AS sum FROM exciseautomation.pass pa WHERE a.request_for_pass_id = pa.request_for_pass_id AND a.financial_year = pa.financial_year AND a.party_code = pa.party_code AND pa.record_status !='R' AND pa.record_status != 'N') AS passlifted_qty from exciseautomation.request_for_pass a inner join exciseautomation.release_request b on a.rrnoc_request_id=b.release_request_id left join exciseautomation.product_master as c on b.product_code=c.product_code left join  exciseautomation.party_master d on d.party_code=b.molasses_supplier left join exciseautomation.document_format_master e on b.party_code=e.party_code where a.request_for_pass_id='" + requestid + "' and pass_type='" + pass_type + "' and b.molasses_supplier='" + party_code + "' and a.financial_year='" + financial_year + "'  order by a.request_for_pass_id", cn);
                        }
                        else
                        {
                            //cmd = new NpgsqlCommand("select a.*,b.valid_date,b.molasses_supplier,b.rr_reqno,b.rr_issueno,b.rr_allotmentno,b.rr_approved_qty,b.rr_balance_qty,b.rr_lifted_qty,c.Product_name,c.Product_code,d.party_name,d.party_address,e.release_request,e.molasses_allotment from exciseautomation.request_for_pass a inner join exciseautomation.release_request b on a.rrnoc_request_id=b.release_request_id inner join exciseautomation.product_master as c on b.product_code=c.product_code inner join  exciseautomation.party_master d on d.party_code=b.party_code left join exciseautomation.document_format_master e on b.party_code=e.party_code  where a.request_for_pass_id='" + requestid + "' and pass_type='" + pass_type + "' and b.party_code='" + party_code + "' and a.financial_year='" + financial_year + "'   order by a.request_for_pass_id", cn);
                            cmd = new NpgsqlCommand("select a.*,b.valid_date,b.molasses_supplier,b.rr_reqno,b.rr_issueno,b.rr_allotmentno,b.rr_approved_qty,b.rr_balance_qty,b.rr_lifted_qty,c.Product_name,c.Product_code,d.party_name,d.party_address,e.release_request,e.molasses_allotment,,( SELECT sum(CASE WHEN pa.dispatch_qty IS NULL THEN 0 ELSE pa.dispatch_qty END) AS sum FROM exciseautomation.pass pa WHERE a.request_for_pass_id = pa.request_for_pass_id AND a.financial_year = pa.financial_year AND a.party_code = pa.party_code AND pa.record_status !='R' AND pa.record_status != 'N') AS passlifted_qty from exciseautomation.request_for_pass a inner join exciseautomation.release_request b on a.rrnoc_request_id=b.release_request_id inner join exciseautomation.product_master as c on b.product_code=c.product_code inner join  exciseautomation.party_master d on d.party_code=b.party_code left join exciseautomation.document_format_master e on b.party_code=e.party_code  where a.request_for_pass_id='" + requestid + "' and pass_type='" + pass_type + "' and b.party_code='" + party_code + "' and a.financial_year='" + financial_year + "'   order by a.request_for_pass_id", cn);
                        }
                            
                        NpgsqlDataReader dr1 = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr1);
                        dr1.Close();
                        foreach(DataRow dr in dt.Rows)
                        { 
                                request.request_for_pass_id = dr["request_for_pass_id"].ToString();
                                request.rrnoc_request_id= dr["rrnoc_request_id"].ToString();
                               request.financial_year = dr["financial_year"].ToString();
                              request.rr_noc_id = dr["release_request"] + dr["financial_year"].ToString() + "/" + dr["rr_reqno"].ToString();
                                request.rr_noc_issuedno =dr["release_request"] + dr["financial_year"].ToString() + "/" + dr["rr_issueno"].ToString();
                                request.product_name = dr["product_name"].ToString();
                            if(dr["valid_date"].ToString()!="")
                                request.valied_date = dr["valid_date"].ToString().Substring(0, 10).Replace("/", "-");
                                request.rr_allotmentno =dr["molasses_allotment"] + dr["financial_year"].ToString() + "/" + dr["rr_allotmentno"].ToString();
                            if(dr["req_qty"].ToString()!="")
                                request.req_qty = Convert.ToDouble(dr["req_qty"].ToString());
                             //   request.approvedqty = Convert.ToDouble(dr["rr_approved_qty"].ToString());
                                request.toparty_code = dr["party_code"].ToString();
                                request.party_code= dr["molasses_supplier"].ToString();
                                request.product_name = dr["product_name"].ToString();
                                request.product_code = dr["product_code"].ToString();
                                request.name = dr["party_name"].ToString();
                                request.address = dr["party_address"].ToString();
                            if (party_type == "All")
                                request.vats = DL_VATMaster.GetvatmasterList("Admin");
                            else
                                request.vats = DL_VATMaster.GetvatmasterList(request.party_code);
                            //if(dr["lifted_qty"].ToString()!="") passlifted_qty
                            //request.lifted_qty = Convert.ToDouble(dr["lifted_qty"].ToString()); 
                            if (dr["passlifted_qty"].ToString() != "")
                                request.lifted_qty = Convert.ToDouble(dr["passlifted_qty"].ToString());
                                else
                                request.lifted_qty = 0;
                            string val = String.Format("{0:0.00}", request.req_qty - request.lifted_qty);
                            request.blance_qty =Convert.ToDouble(val);
                        }
                        
                    }
                    else
                    {
                        pass_type = "NOC";
                        if (party_type == "All")
                        {
                            cmd = new NpgsqlCommand("select a.*,b.lifted_qty,b.req_qty as req_qty1,b.record_status as record_status1,c.party_code,c.req_nocno ,c.issue_nocno,c.issue_date,h.product_name,h.product_code,b.request_for_pass_id,c.valid_upto,b.approved_qty,d.customer_id,d.cust_name,d.cust_address,c.issue_nocno,g.noc,f.product_code,f.product_name from exciseautomation.noc_depotdetail a inner join exciseautomation.request_for_pass b on a.noc_id=b.rrnoc_request_id and a.noc_depotdetail_id=b.noc_depotdetail_id and a.financial_year=b.financial_year left join exciseautomation.noc c on a.noc_id = c.noc_id left join exciseautomation.customer_master d on c.customer_id = d.customer_id inner join exciseautomation.product_master f on f.product_code=c.noc_for left join exciseautomation.document_format_master g on g.party_code=c.party_code inner join exciseautomation.product_master h on h.product_code=f.product_code  where b.request_for_pass_id='" + requestid + "' and b.pass_type='" + pass_type + "' ", cn);
                        }
                        else
                        {
                            cmd = new NpgsqlCommand("select a.*,b.lifted_qty,b.req_qty as req_qty1,b.record_status as record_status1,c.party_code,c.req_nocno ,c.issue_nocno,c.issue_date,h.product_name,h.product_code,b.request_for_pass_id,c.valid_upto,b.approved_qty,d.customer_id,d.cust_name,d.cust_address,c.issue_nocno,g.noc,f.product_code,f.product_name from exciseautomation.noc_depotdetail a inner join exciseautomation.request_for_pass b on a.noc_id=b.rrnoc_request_id and a.noc_depotdetail_id=b.noc_depotdetail_id and a.financial_year=b.financial_year left join exciseautomation.noc c on a.noc_id = c.noc_id and a.financial_year=c.financial_year left join exciseautomation.customer_master d on c.customer_id = d.customer_id inner join exciseautomation.product_master f on f.product_code=c.noc_for left join exciseautomation.document_format_master g on g.party_code=c.party_code inner join exciseautomation.product_master h on h.product_code=f.product_code  where b.request_for_pass_id='" + requestid + "' and b.pass_type='" + pass_type + "' and b.party_code='" + party_code + "' and a.financial_year='" + financial_year + "' ", cn);
                        }
                          
                        NpgsqlDataReader dr1 = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr1);
                        dr1.Close();
                        foreach (DataRow dr in dt.Rows)
                        {
                            request.request_for_pass_id = dr["request_for_pass_id"].ToString();
                            request.rrnoc_request_id = dr["noc_id"].ToString();
                            request.financial_year = dr["financial_year"].ToString();
                            request.rr_noc_id = dr["noc"] + dr["financial_year"].ToString() + "/" + dr["req_nocno"].ToString();
                            request.rr_noc_issuedno = dr["noc"] + dr["financial_year"].ToString() + "/" + dr["issue_nocno"].ToString();
                            request.req_qty = Convert.ToDouble(dr["req_qty1"].ToString());
                            request.product_name = dr["product_name"].ToString();
                            request.product_code = dr["product_code"].ToString();
                            if (dr["valid_upto"].ToString()!="")
                            request.valied_date = dr["valid_upto"].ToString().Substring(0, 10).Replace("/", "-");
                            request.record_status = dr["record_status1"].ToString();
                            if (dr["issue_date"].ToString() != "")
                            request.rr_date = dr["issue_date"].ToString().Substring(0, 10).Replace("/", "-");
                           // request.alloted_qty = Convert.ToDouble(dr["totalqty"].ToString());
                            request.issue_nocno = dr["noc"] + dr["financial_year"].ToString() + "/" + dr["issue_nocno"].ToString();
                            request.party_code = dr["party_code"].ToString();
                            request.toparty_code = dr["customer_id"].ToString();
                            //if(dr["liftedqty"].ToString()!="")
                            //request.lifted_qty =Convert.ToDouble( dr["liftedqty"].ToString());
                           // request.blance_qty = request.alloted_qty - request.lifted_qty;
                            request.noc_depotdetail_id = dr["noc_depotdetail_id"].ToString();
                            request.name = dr["cust_name"].ToString();
                            request.address = dr["cust_address"].ToString();
                            request.depot = dr["depot"].ToString();
                            if (party_type == "All")
                                request.vats = DL_VATMaster.GetvatmasterList("Admin");
                            else
                                request.vats = DL_VATMaster.GetvatmasterList(request.party_code);
                            //   cmd = new NpgsqlCommand("select case when sum(dispatch_qty) is null then 0 else sum(dispatch_qty) end as dispatch_qty from exciseautomation.pass where pass_reqno='" + requestid + "'", cn);
                            if(dr["lifted_qty"].ToString()!="")
                            request.lifted_qty =Convert.ToDouble( dr["lifted_qty"].ToString()); 
                            else
                                request.lifted_qty =0;

                            request.blance_qty = request.req_qty - request.lifted_qty;
                        }
                        
                    }
                    
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return request;
            }
        }

        public static List<ReaquestForPass> GetNOCList(string user_id)
        {
            List<ReaquestForPass> request = new List<ReaquestForPass>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    //  NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.valid_upto,c.product_name,b.customer_id,b.noc_total_qty,b.party_code as party,d.noc,b.issue_nocno,b1.cust_name from exciseautomation.request_for_pass a inner join exciseautomation.noc b on a.rrnoc_request_id=b.noc_id and a.financial_year=b.financial_year and a.party_code=b.party_code inner join exciseautomation.product_master as c on b.noc_for=c.product_code left join exciseautomation.document_format_master d on a.party_code=d.party_code left join exciseautomation.customer_master b1 on b.customer_id=b1.customer_id where a.pass_type='NOC'   order by request_for_pass_id", cn);
                    NpgsqlCommand cmd = new NpgsqlCommand("select * from  exciseautomation.view_request_for_pass_noc", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        request = new List<ReaquestForPass>();

                        while (dr.Read())
                        {
                            ReaquestForPass production = new ReaquestForPass();
                            production.request_for_pass_id = dr["request_for_pass_id"].ToString();
                            production.rrnoc_request_id = dr["rrnoc_request_id"].ToString();
                            production.financial_year = dr["financial_year"].ToString();
                            production.issue_nocno =dr["noc"].ToString() + dr["financial_year"].ToString() + "/" + dr["issue_nocno"].ToString();
                            production.req_qty = Convert.ToDouble(dr["req_qty"].ToString());
                            if (dr["approved_qty"].ToString() != "")
                                production.approved_qty = Convert.ToDouble(dr["approved_qty"].ToString());
                            else
                                production.approved_qty = 0;
                            if (dr["passlifted_qty"].ToString() != "")
                                production.lifted_qty = Convert.ToDouble(dr["passlifted_qty"].ToString());
                            else
                                production.lifted_qty = 0;
                            //if (dr["lifted_qty"].ToString() != "") 
                            //    production.lifted_qty = Convert.ToDouble(dr["lifted_qty"].ToString());
                            //else
                            //    production.lifted_qty = 0;
                            production.product_name = dr["product_name"].ToString();
                            if(dr["valid_upto"].ToString()!="")
                            production.valied_date = dr["valid_upto"].ToString().Substring(0, 10).Replace("/", "-");
                            production.record_status = dr["record_status"].ToString();
                            production.party_code = dr["party"].ToString();
                            production.toparty_code = dr["customer_id"].ToString();
                            production.name = dr["cust_name"].ToString();
                            production.approvedqty = Convert.ToDouble(dr["noc_total_qty"].ToString());
                            production.blance_qty = production.approvedqty - production.approved_qty;
                            request.Add(production);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return request;
            }
        }
        public static List<ReaquestForPass> Search1(string tablename, string column, string value)
        {
            List<ReaquestForPass> mir = new List<ReaquestForPass>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd1;
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.valid_upto,c.product_name,b.customer_id,b.noc_total_qty,b.party_code as party,d.noc,b.issue_nocno,b1.cust_name from exciseautomation.request_for_pass a inner join exciseautomation.noc b on a.rrnoc_request_id=b.noc_id and a.financial_year=b.financial_year and a.party_code=b.party_code inner join exciseautomation.product_master as c on b.noc_for=c.product_code left join exciseautomation.document_format_master d on a.party_code=d.party_code left join exciseautomation.customer_master b1 on b.customer_id=b1.customer_id where a.pass_type='NOC'  and " + column + " Ilike '%" + value + "%'  order by rrnoc_request_id,request_for_pass_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            mir = new List<ReaquestForPass>();
                            while (dr.Read())
                            {
                                ReaquestForPass production = new ReaquestForPass();
                                production.request_for_pass_id = dr["request_for_pass_id"].ToString();
                                production.rrnoc_request_id = dr["rrnoc_request_id"].ToString();
                                production.financial_year= dr["financial_year"].ToString();
                                production.issue_nocno = dr["noc"].ToString() + dr["financial_year"].ToString() + "/" + dr["issue_nocno"].ToString();
                                production.req_qty = Convert.ToDouble(dr["req_qty"].ToString());
                                if (dr["approved_qty"].ToString() != "")
                                    production.approved_qty = Convert.ToDouble(dr["approved_qty"].ToString());
                                else
                                    production.approved_qty = 0;
                                if (dr["lifted_qty"].ToString() != "")
                                    production.lifted_qty = Convert.ToDouble(dr["lifted_qty"].ToString());
                                else
                                    production.lifted_qty = 0;
                                production.product_name = dr["product_name"].ToString();
                                if (dr["valid_upto"].ToString() != "")
                                    production.valied_date = dr["valid_upto"].ToString().Substring(0, 10).Replace("/", "-");
                                production.record_status = dr["record_status"].ToString();
                                production.party_code = dr["party"].ToString();
                                production.toparty_code = dr["customer_id"].ToString();
                                production.name = dr["cust_name"].ToString();
                                production.approvedqty = Convert.ToDouble(dr["noc_total_qty"].ToString());
                                production.blance_qty = production.approvedqty - production.approved_qty;
                              
                                mir.Add(production);

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

        public static ReaquestForPass GetDetails(string requestid, string pass_type,string party_code,string financial_year)
        {
           ReaquestForPass request = new ReaquestForPass();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    if (pass_type == "M")
                    {
                        pass_type = "RR";
                        cmd = new NpgsqlCommand("select a.*,b.valid_date,b.allocation_qty,b.molasses_supplier,b.party_code,b.rr_date,b.rr_approved_qty,c.Product_name,d.party_name from exciseautomation.request_for_pass a inner join exciseautomation.release_request b on a.rrnoc_request_id = b.release_request_id inner join exciseautomation.product_master as c on b.product_code = c.product_code inner join exciseautomation.party_master as d on d.party_code = b.molasses_supplier  where a.request_for_pass_id='" + requestid + "' and pass_type='" + pass_type + "' and a.party_code='" + party_code + "' and a.financial_year='"+financial_year+"'  order by a.request_for_pass_id", cn);
                    }
                    else
                    {
                        pass_type = "NOC";
                        cmd = new NpgsqlCommand("select a.*,b.valid_upto as valid_date,b.noc_total_qty as allocation_qty,b.customer_id as molasses_supplier,b.party_code,b.nocdate as rr_date,b.noc_total_qty as rr_approved_qty,c.Product_name,d.cust_name from exciseautomation.request_for_pass a inner join exciseautomation.noc b on a.rrnoc_request_id = b.noc_id inner join exciseautomation.product_master as c on b.noc_for = c.product_code inner join exciseautomation.Customer_master as d on b.customer_id = d.customer_id  where a.request_for_pass_id='" + requestid + "' and pass_type='" + pass_type + "' and a.party_code='" + party_code + "'and a.financial_year='" + financial_year + "'  order by a.request_for_pass_id", cn);
                    }
                   
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        

                        while (dr.Read())
                        {

                            request.request_for_pass_id = dr["request_for_pass_id"].ToString();
                            request.rrnoc_request_id = dr["rrnoc_request_id"].ToString();
                            request.req_qty = Convert.ToDouble(dr["req_qty"].ToString());
                            if (dr["approved_qty"].ToString() != "")
                                request.approved_qty = Convert.ToDouble(dr["approved_qty"].ToString());
                            else
                                request.approved_qty = 0;
                            request.product_name = dr["product_name"].ToString();
                            request.valied_date = dr["valid_date"].ToString().Substring(0, 10).Replace("/", "-");
                            request.record_status = dr["record_status"].ToString();
                            request.rr_date = dr["rr_date"].ToString().Substring(0, 10).Replace("/", "-");
                            request.alloted_qty =Convert.ToDouble( dr["allocation_qty"].ToString());
                            request.approvedqty = Convert.ToDouble(dr["rr_approved_qty"].ToString());
                            request.party_code = dr["party_code"].ToString();
                            request.toparty_code =dr["molasses_supplier"].ToString();
                            if(pass_type != "NOC")
                            request.blance_qty = request.approvedqty - request.approved_qty;
                            request.noc_depotdetail_id = dr["noc_depotdetail_id"].ToString();
                            if (pass_type == "NOC")
                                request.name = dr["Cust_name"].ToString();
                            else
                                request.name = dr["party_name"].ToString();


                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return request;
            }
        }
       
        public static string Update(ReaquestForPass pass)
        {
            string val = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    if (pass.pass_type == "M")
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand("Update exciseautomation.request_for_pass set   req_qty='" + pass.req_qty + "',approved_qty='" + pass.req_qty + "',  user_id='" + pass.user_id + "',lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "', record_status='" + pass.record_status + "' where request_for_pass_id='" + pass.request_for_pass_id + "' and financial_year='" + pass.financial_year + "'", cn);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand("Update exciseautomation.request_for_pass set  req_qty='" + pass.req_qty + "', approved_qty='" + pass.req_qty + "',  user_id='" + pass.user_id + "',lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "', record_status='" + pass.record_status + "',noc_depotdetail_id='"+pass.noc_depotdetail_id+"' where request_for_pass_id='" + pass.request_for_pass_id + "'  and financial_year='" + pass.financial_year + "'", cn);
                        cmd.ExecuteNonQuery();
                    }
                    val = "0";

                    cn.Close();
                    _log.Info("Update Request Pass Success");
                }
                catch (Exception ex)
                {
                    val = ex.Message;
                    _log.Info("Update Request Pass fail :" + ex.Message);
                }

            }
            return val;
        }
        public static string GetBalance(string id,string pass_type,string financial_year)
        {
            string val = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    NpgsqlCommand cmd = new NpgsqlCommand("Select case when sum(req_qty) is null then 0 else sum(req_qty) end approved_qty from exciseautomation.request_for_pass  where rrnoc_request_id='" + id+ "' and pass_type='"+pass_type+"' and record_status!='R' and financial_year='"+financial_year+"'", cn);
                    val=cmd.ExecuteScalar().ToString();
                    cn.Close();
                    _log.Info("Get Balance Request Pass Success");
                }
                catch (Exception ex)
                {
                    val = ex.Message;
                    _log.Info("Get Balance Request Pass fail :" + ex.Message);
                }

            }
            return val;
        }
        public static string Approve(ReaquestForPass pass)
        {
            string val = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {

                    NpgsqlCommand cmd = new NpgsqlCommand("Update exciseautomation.request_for_pass set   approved_qty='" + pass.approved_qty + "', record_status='" + pass.record_status + "' where request_for_pass_id='" + pass.request_for_pass_id + "'  and financial_year='" + pass.financial_year + "'", cn);
                    cmd.ExecuteNonQuery();
                    val = "0";
                    if (pass.record_status == "R")
                    {
                        pass.record_status = "Rejected by Bond Officer";
                    }
                    else
                    {
                        pass.record_status = "Approved by Bond Officer";
                    }
                    StringBuilder str = new StringBuilder();
                    str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                    str.Append("'" + pass.request_for_pass_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "','RFP','" + pass.record_status + "','" + pass.approval_status + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + pass.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + pass.user_id + "','"+pass.financial_year+"','"+pass.party_code+"')");
                    cmd = new NpgsqlCommand(str.ToString(), cn);
                    cmd.ExecuteNonQuery();
                    val = "0";
                    trn.Commit();
                    cn.Close();
                    _log.Info("Approve Request Pass Success");
                }
                catch (Exception ex)
                {
                    val = ex.Message;
                    trn.Rollback();
                    _log.Info("Approve Request Pass fail :" + ex.Message);
                }

            }
            return val;
        }
        public static List<ReaquestForPass> GetList(string userid)
        {
            List<ReaquestForPass> request = new List<ReaquestForPass>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    //NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.valid_date,c.Product_name,b.rr_approved_qty,b.party_code AS PARTY,b.molasses_supplier,b.rr_issueno,d.release_request,p.party_name from exciseautomation.request_for_pass a inner join exciseautomation.release_request b on a.rrnoc_request_id=b.release_request_id and a.financial_year=b.financial_year inner join exciseautomation.product_master as c on b.product_code=c.product_code left join exciseautomation.document_format_master as d on b.party_code=d.party_code left join exciseautomation.party_master as p on b.molasses_supplier=p.party_code where a.pass_type='RR'   order by request_for_pass_id,b.valid_date desc", cn);
                    NpgsqlCommand cmd = new NpgsqlCommand("select * from  exciseautomation.view_request_for_pass ", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        request = new List<ReaquestForPass>();

                        while (dr.Read())
                        {
                            ReaquestForPass production = new ReaquestForPass();
                            production.request_for_pass_id = dr["request_for_pass_id"].ToString();
                            production.rrnoc_request_id = dr["rrnoc_request_id"].ToString();
                            production.financial_year = dr["financial_year"].ToString();
                            production.rr_allotmentno =dr["release_request"] + dr["financial_year"].ToString() + "/" + dr["rr_issueno"].ToString();
                            production.req_qty = Convert.ToDouble(dr["req_qty"].ToString());
                            if (dr["approved_qty"].ToString() != "")
                                production.approved_qty = Convert.ToDouble(dr["approved_qty"].ToString());
                            else
                                production.approved_qty = 0;
                            //if (dr["lifted_qty"].ToString() != "")
                            //    production.lifted_qty = Convert.ToDouble(dr["lifted_qty"].ToString());
                            if (dr["passlifted_qty"].ToString() != "")
                                production.lifted_qty = Convert.ToDouble(dr["passlifted_qty"].ToString());
                            else
                                production.lifted_qty = 0;
                            production.product_name = dr["product_name"].ToString();
                            if(dr["valid_date"].ToString()!="")
                            production.valied_date = dr["valid_date"].ToString().Substring(0, 10).Replace("/", "-");
                            production.record_status = dr["record_status"].ToString();
                            production.party_code = dr["PARTY"].ToString();
                            production.toparty_code = dr["molasses_supplier"].ToString();
                            if(production.pass_type=="RR")
                            production.name = dr["party_name"].ToString();
                            else
                                production.name = dr["party_name"].ToString();
                            //  production.cu = dr["molasses_supplier"].ToString();
                            production.approvedqty =Convert.ToDouble( dr["rr_approved_qty"].ToString());
                            production.blance_qty = production.approvedqty - production.approved_qty;
                            production.user_id = userid;
                            request.Add(production);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return request;
            }
        }


        public static List<ReaquestForPass> Search(string tablename, string column, string value)
        {
            List<ReaquestForPass> mir = new List<ReaquestForPass>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd1;
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.valid_date,c.Product_name,b.rr_approved_qty,b.party_code AS PARTY,b.molasses_supplier,b.rr_issueno,d.release_request,p.party_name from exciseautomation.request_for_pass a inner join exciseautomation.release_request b on a.rrnoc_request_id=b.release_request_id inner join exciseautomation.product_master as c on b.product_code=c.product_code left join exciseautomation.document_format_master as d on b.party_code=d.party_code left join exciseautomation.party_master as p on b.molasses_supplier=p.party_code where a.pass_type='RR' and " + column + " Ilike '%" + value + "%' order by request_for_pass_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            mir = new List<ReaquestForPass>();
                            while (dr.Read())
                            {
                                ReaquestForPass production = new ReaquestForPass();
                                production.request_for_pass_id = dr["request_for_pass_id"].ToString();
                                production.rrnoc_request_id = dr["rrnoc_request_id"].ToString();
                                production.financial_year = dr["financial_year"].ToString();
                                production.rr_allotmentno = dr["release_request"] + dr["financial_year"].ToString() + "/" + dr["rr_issueno"].ToString();
                                production.req_qty = Convert.ToDouble(dr["req_qty"].ToString());
                                if (dr["approved_qty"].ToString() != "")
                                    production.approved_qty = Convert.ToDouble(dr["approved_qty"].ToString());
                                else
                                    production.approved_qty = 0;
                                if (dr["lifted_qty"].ToString() != "")
                                    production.lifted_qty = Convert.ToDouble(dr["lifted_qty"].ToString());
                                else
                                    production.lifted_qty = 0;
                                production.product_name = dr["product_name"].ToString();
                                production.valied_date = dr["valid_date"].ToString().Substring(0, 10).Replace("/", "-");
                                production.record_status = dr["record_status"].ToString();
                                production.party_code = dr["PARTY"].ToString();
                                production.toparty_code = dr["molasses_supplier"].ToString();
                                if (production.pass_type == "RR")
                                    production.name = dr["party_name"].ToString();
                                else
                                    production.name = dr["party_name"].ToString();
                                //  production.cu = dr["molasses_supplier"].ToString();
                                production.approvedqty = Convert.ToDouble(dr["rr_approved_qty"].ToString());
                                production.blance_qty = production.approvedqty - production.approved_qty;
                                production.user_id = dr["user_id"].ToString();
                                mir.Add(production);

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

    }
}
