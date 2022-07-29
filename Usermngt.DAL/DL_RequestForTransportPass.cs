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
   public class DL_RequestForTransportPass
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

                    NpgsqlCommand cmd4 = new NpgsqlCommand("select case when max(request_for_pass_reqno) is null then 0 else max(request_for_pass_reqno) end as request_for_pass_reqno from exciseautomation.request_for_pass where financial_year='" + pass.financial_year + "'  ", cn);
                    int m = Convert.ToInt32(cmd4.ExecuteScalar().ToString()) + 1;
                    if (pass.pass_type == "E")
                    {
                        pass.pass_type = "EXP";
                        StringBuilder str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.request_for_pass(request_for_pass_id, rrnoc_request_id,party_code, req_qty,approved_qty,approval_status,  user_id, creation_date, record_status,pass_type,noc_depotdetail_id,route_details, request_for_pass_date, vehicle_no, digital_lock_no, temperature, indication, strength, pass_valid_upto,request_for_pass_reqno,permitno,financial_year)values(");
                        str.Append("'" + max + "','" + pass.rrnoc_request_id + "','" + pass.party_code + "','" + pass.req_qty + "','" + pass.req_qty + "','"+pass.approval_status+"','" + pass.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + pass.record_status + "','" + pass.pass_type + "','" + pass.noc_depotdetail_id + "','" + pass.route_details+"','"+pass.request_for_pass_date+"','"+pass.vehicle_no+"','"+pass.digital_lock_no+"','"+pass.temperature+"','"+pass.indication+"','"+pass.strength+"','"+pass.pass_valid_upto+"','"+m+"','"+pass.permitno+"','"+pass.financial_year+"')");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        pass.pass_type = "DOM";
                        StringBuilder str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.request_for_pass(request_for_pass_id, rrnoc_request_id, req_qty,approved_qty,approval_status,  user_id, creation_date, record_status,pass_type,noc_depotdetail_id,party_code,route_details, request_for_pass_date, vehicle_no, digital_lock_no, temperature, indication, strength, pass_valid_upto,request_for_pass_reqno,permitno,financial_year)values(");
                        str.Append("'" + max + "','" + pass.rrnoc_request_id + "','" + pass.req_qty + "','" + pass.req_qty + "','"+pass.approval_status+"','" + pass.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + pass.record_status + "','" + pass.pass_type + "','" + pass.noc_depotdetail_id + "','" + pass.party_code + "','" + pass.route_details + "','" + pass.request_for_pass_date + "','" + pass.vehicle_no + "','" + pass.digital_lock_no + "','" + pass.temperature + "','" + pass.indication + "','" + pass.strength + "','" + pass.pass_valid_upto + "','"+ m +"','"+pass.permitno+"','"+pass.financial_year+"')");
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

        public static ReaquestForPass GetRequest(string requestid, string pass_type, string party_code, string party_type, string financial_year)
        {
            ReaquestForPass request = new ReaquestForPass();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    if (pass_type == "E" || pass_type == "EXP")
                    {
                        pass_type = "EXP";
                        cmd = new NpgsqlCommand("select a.*,b.lifted_qty,b.financial_year as rfinancial_year,b.req_qty as req_qty1,b.record_status as record_status1,c.tenderno,c.party_code,c.req_nocno ,c.issue_nocno,c.issue_date,h.product_name,h.product_code,b.request_for_pass_id,c.valid_upto,b.approved_qty,d.customer_id,d.cust_name,d.cust_address,c.issue_nocno,g.noc,g.pass,f.product_code,f.product_name,b.digital_lock_no,b.vehicle_no from exciseautomation.noc_depotdetail a inner join exciseautomation.request_for_pass b on a.noc_id=b.rrnoc_request_id and a.noc_depotdetail_id=b.noc_depotdetail_id and a.financial_year=b.financial_year left join exciseautomation.noc c on a.noc_id = c.noc_id and b.financial_year=c.financial_year and b.party_code=c.party_code left join exciseautomation.customer_master d on c.customer_id = d.customer_id inner join exciseautomation.product_master f on f.product_code=c.noc_for left join exciseautomation.document_format_master g on g.party_code=c.party_code inner join exciseautomation.product_master h on h.product_code=f.product_code  where b.request_for_pass_id='" + requestid + "' and b.pass_type='" + pass_type + "' and b.party_code='" + party_code + "' and b.financial_year='"+financial_year+"'", cn);
                        NpgsqlDataReader dr1 = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr1);
                        dr1.Close();
                        foreach (DataRow dr in dt.Rows)
                        {
                            request.request_for_pass_id = dr["request_for_pass_id"].ToString();
                            request.rrnoc_request_id = dr["noc_id"].ToString();
                            request.financial_year = dr["rfinancial_year"].ToString();
                            request.rr_noc_id = dr["noc"]+ dr["rfinancial_year"].ToString() + "/" + dr["req_nocno"].ToString();
                            request.rr_noc_issuedno = dr["noc"] + dr["rfinancial_year"].ToString() + "/" + dr["issue_nocno"].ToString();
                            request.req_qty = Convert.ToDouble(dr["req_qty1"].ToString());
                            request.product_name = dr["product_name"].ToString();
                            request.product_code = dr["product_code"].ToString();
                            if(dr["valid_upto"].ToString()!="")
                            request.valied_date = dr["valid_upto"].ToString().Substring(0, 10).Replace("/", "-");
                            request.record_status = dr["record_status1"].ToString();
                            if(dr["issue_date"].ToString()!="")
                            request.rr_date = dr["issue_date"].ToString().Substring(0, 10).Replace("/", "-");
                            // request.alloted_qty = Convert.ToDouble(dr["totalqty"].ToString());
                            request.issue_nocno = dr["noc"] + dr["rfinancial_year"].ToString() + "/" + dr["issue_nocno"].ToString();
                            request.permit_no = dr["tenderno"].ToString();
                            request.party_code = dr["party_code"].ToString();
                            request.toparty_code = dr["customer_id"].ToString();
                            request.vehicle_no = dr["vehicle_no"].ToString();
                            request.digital_lock_no = dr["digital_lock_no"].ToString();
                            //if(dr["liftedqty"].ToString()!="")
                            //request.lifted_qty =Convert.ToDouble( dr["liftedqty"].ToString());
                            //request.blance_qty = request.alloted_qty - request.lifted_qty;
                            request.noc_depotdetail_id = dr["noc_depotdetail_id"].ToString();
                            request.name = dr["cust_name"].ToString();
                            request.address = dr["cust_address"].ToString();
                            request.depot = dr["depot"].ToString();
                            request.vats = DL_VATMaster.GetvatmasterList(request.party_code);
                            //   cmd = new NpgsqlCommand("select case when sum(dispatch_qty) is null then 0 else sum(dispatch_qty) end as dispatch_qty from exciseautomation.pass where pass_reqno='" + requestid + "'", cn);
                            if (dr["lifted_qty"].ToString() != "")
                                request.lifted_qty = Convert.ToDouble(dr["lifted_qty"].ToString());
                            else
                                request.lifted_qty = 0;

                            request.blance_qty = request.req_qty - request.lifted_qty;
                        }

                    }
                    else
                    {
                        pass_type = "DOM";
                        cmd = new NpgsqlCommand("select a.*,b.pass_valid_upto,b.party_code as party,b.digital_lock_no,b.request_for_pass_id,b.route_details,b.vehicle_no,b.lifted_qty,b.req_qty as req_qty1,b.record_status as record_status1,b.approved_qty,b.request_for_pass_issuedate,h.party_address,h.party_name,f.product_code,f.product_name,n.permit from exciseautomation.permit a inner join exciseautomation.request_for_pass b on a.permit_id=b.rrnoc_request_id left join exciseautomation.molasses_allotment_request c on a.molasses_allotment_request_id = c.molasses_allotment_request_id  inner join exciseautomation.product_master f on f.product_code=c.product_code left join exciseautomation.document_format_master g on g.party_code=a.party_code inner join exciseautomation.party_master h on h.party_code=a.party_code left join exciseautomation.document_format_master n on a.party_code=n.party_code where b.request_for_pass_id='" + requestid + "' and b.pass_type='" + pass_type + "' and b.party_code='" + party_code + "' and b.financial_year='" + financial_year + "'", cn);
                        NpgsqlDataReader dr1 = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr1);
                        dr1.Close();
                        foreach (DataRow dr in dt.Rows)
                        {
                            request.request_for_pass_id = dr["request_for_pass_id"].ToString();
                            request.rrnoc_request_id = dr["permit_id"].ToString();
                            request.financial_year= dr["financial_year"].ToString();
                            request.rr_noc_id = "Req/" + dr["permit"].ToString() + dr["financial_year"].ToString() + "/" + dr["permit_no"].ToString();
                            request.rr_noc_issuedno =  dr["permit_no"].ToString();
                            request.req_qty = Convert.ToDouble(dr["req_qty1"].ToString());
                            request.product_name = dr["product_name"].ToString();
                            request.product_code = dr["product_code"].ToString();
                            if(dr["pass_valid_upto"].ToString()!="")
                            request.valied_date = dr["pass_valid_upto"].ToString().Substring(0, 10).Replace("/", "-");
                            request.record_status = dr["record_status1"].ToString();
                            if(dr["request_for_pass_issuedate"].ToString()!="")
                            request.rr_date = dr["request_for_pass_issuedate"].ToString().Substring(0, 10).Replace("/", "-");
                            // request.alloted_qty = Convert.ToDouble(dr["totalqty"].ToString());
                           request.issue_nocno = dr["permit"].ToString() + dr["financial_year"].ToString() + "/" + dr["permit_issueno"].ToString();
                            request.permit_no = dr["permit"].ToString() + dr["financial_year"].ToString() + "/" + dr["permit_no"].ToString();
                            request.toparty_code= dr["party_code"].ToString();
                            request.party_code = dr["party"].ToString();
                            request.dispatch_qty= Convert.ToDouble(dr["approved_qty"].ToString());
                            request.vehicle_no= dr["vehicle_no"].ToString();
                            request.route_details= dr["route_details"].ToString();
                            // request.toparty_code = dr["customer_id"].ToString();
                            //if(dr["liftedqty"].ToString()!="")
                            //request.lifted_qty =Convert.ToDouble( dr["liftedqty"].ToString());
                            // request.blance_qty = request.alloted_qty - request.lifted_qty;
                            //  request.noc_depotdetail_id = dr["noc_depotdetail_id"].ToString();
                            request.name = dr["party_name"].ToString();
                            request.address = dr["party_address"].ToString();
                           request.depot = dr["product_name"].ToString();
                            request.digital_lock_no= dr["digital_lock_no"].ToString();
                            request.vats = DL_VATMaster.GetvatmasterList(request.party_code);
                            //   cmd = new NpgsqlCommand("select case when sum(dispatch_qty) is null then 0 else sum(dispatch_qty) end as dispatch_qty from exciseautomation.pass where pass_reqno='" + requestid + "'", cn);
                            if (dr["lifted_qty"].ToString() != "")
                                request.lifted_qty = Convert.ToDouble(dr["lifted_qty"].ToString());
                            else
                                request.lifted_qty = 0;

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

        public static List<ReaquestForPass> GetNOCList(string user_id ,string party_type)
        {
            List<ReaquestForPass> request = new List<ReaquestForPass>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.valid_upto,c.product_name,b.customer_id,b.noc_total_qty,b.party_code as party,d.noc,b.issue_nocno,b1.cust_name from exciseautomation.request_for_pass a inner join exciseautomation.noc b on a.rrnoc_request_id=b.noc_id and a.financial_year =b.financial_year and a.party_code =b.party_code inner join exciseautomation.product_master as c on b.noc_for=c.product_code left join exciseautomation.document_format_master d on a.party_code=d.party_code left join exciseautomation.customer_master b1 on b.customer_id=b1.customer_id where a.pass_type='" + party_type+ "' and a.record_active='true'  order by b.valid_upto,a.request_for_pass_id desc", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        request = new List<ReaquestForPass>();

                        while (dr.Read())
                        {
                            ReaquestForPass production = new ReaquestForPass();
                            production.request_for_pass_id = dr["request_for_pass_id"].ToString();
                            production.rrnoc_request_id = dr["rrnoc_request_id"].ToString();
                            production.pass_type= dr["pass_type"].ToString();
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
                            //if (dr["valid_upto"].ToString() != "")
                            //    production.valied_date = dr["valid_upto"].ToString().Substring(0, 10).Replace("/", "-");
                            if (dr["pass_valid_upto"].ToString() != "")
                                production.pass_valid_upto= dr["pass_valid_upto"].ToString().Substring(0, 10).Replace("/", "-");
                            production.record_status = dr["record_status"].ToString();
                            production.party_code = dr["party"].ToString();
                            production.approval_status= dr["approval_status"].ToString();
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
        public static List<ReaquestForPass> Searchena(string tablename, string column, string value, string passtype)
        {
            List<ReaquestForPass> mir = new List<ReaquestForPass>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd1;
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.valid_upto,c.product_name,b.customer_id,b.noc_total_qty,b.party_code as party,d.noc,b.issue_nocno,b1.cust_name from exciseautomation.request_for_pass a inner join exciseautomation.noc b on a.rrnoc_request_id=b.noc_id inner join exciseautomation.product_master as c on b.noc_for=c.product_code left join exciseautomation.document_format_master d on a.party_code=d.party_code left join exciseautomation.customer_master b1 on b.customer_id=b1.customer_id where a.pass_type='" +passtype + "' and " + column + " Ilike '%" + value + "%' and a.record_active='true'  order by rrnoc_request_id,request_for_pass_id", cn))
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
                                production.pass_type = dr["pass_type"].ToString();
                                production.financial_year = dr["financial_year"].ToString();
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
                                //if (dr["valid_upto"].ToString() != "")
                                //    production.valied_date = dr["valid_upto"].ToString().Substring(0, 10).Replace("/", "-");
                                if (dr["pass_valid_upto"].ToString() != "")
                                    production.pass_valid_upto = dr["pass_valid_upto"].ToString().Substring(0, 10).Replace("/", "-");
                                production.record_status = dr["record_status"].ToString();
                                production.party_code = dr["party"].ToString();
                                production.approval_status = dr["approval_status"].ToString();
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

        public static List<ReaquestForPass> GetpermitList(string user_id, string party_type)
        {
            List<ReaquestForPass> request = new List<ReaquestForPass>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,c.product_name,b.party_code as party,b.permit_id,b.permit_qty,b.permit_no,b1.party_name as cust_name,d.permit from exciseautomation.request_for_pass a inner join exciseautomation.permit b on a.rrnoc_request_id=b.permit_id inner join exciseautomation.molasses_allotment_request f on b.molasses_allotment_request_id=f.molasses_allotment_request_id inner join exciseautomation.product_master as c on f.product_code=c.product_code left join exciseautomation.document_format_master d on b.party_code=d.party_code left join exciseautomation.party_master b1 on b.party_code=b1.party_code where a.pass_type='" + party_type+ "'  order by request_for_pass_id", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        request = new List<ReaquestForPass>();

                        while (dr.Read())
                        {
                            ReaquestForPass production = new ReaquestForPass();
                            production.request_for_pass_id = dr["request_for_pass_id"].ToString();
                            production.rrnoc_request_id = dr["permit_id"].ToString();
                            production.pass_type = dr["pass_type"].ToString();
                            production.financial_year = dr["financial_year"].ToString();
                            production.issue_nocno = dr["permit"].ToString() + dr["financial_year"].ToString() + "/" + dr["permit_no"].ToString();
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
                            //if (dr["valid_upto"].ToString() != "")
                            //    production.valied_date = dr["valid_upto"].ToString().Substring(0, 10).Replace("/", "-");
                            if (dr["pass_valid_upto"].ToString() != "")
                                production.pass_valid_upto = dr["pass_valid_upto"].ToString().Substring(0, 10).Replace("/", "-");
                            production.record_status = dr["record_status"].ToString();
                            production.party_code = dr["party_code"].ToString();
                            production.approval_status = dr["approval_status"].ToString();
                          //  production.toparty_code = dr["customer_id"].ToString();
                            production.name = dr["cust_name"].ToString();
                            production.approvedqty = Convert.ToDouble(dr["permit_qty"].ToString());
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

        public static List<ReaquestForPass> Searchpermit(string tablename, string column, string value,string passtype)
        {
            List<ReaquestForPass> mir = new List<ReaquestForPass>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd1;
                    if(column== "issue_nocno")
                    {
                        if(passtype=="DOM")
                        {
                            column = "b.permit_no";
                        }
                       
                    }
                    else if(column == "b.noc_total_qty")
                    {
                        if (passtype == "DOM")
                        {
                            column = "b.permit_qty";
                        }
                    }
                    else if (column == "b1.cust_name")
                    {
                        if (passtype == "DOM")
                        {
                            column = "b1.party_name";
                        }
                    }
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,c.product_name,b.party_code as party,b.permit_id,b.permit_qty,b.permit_no,b1.party_name as cust_name,d.permit from exciseautomation.request_for_pass a inner join exciseautomation.permit b on a.rrnoc_request_id=b.permit_id inner join exciseautomation.molasses_allotment_request f on b.molasses_allotment_request_id=f.molasses_allotment_request_id inner join exciseautomation.product_master as c on f.product_code=c.product_code left join exciseautomation.document_format_master d on b.party_code=d.party_code left join exciseautomation.party_master b1 on b.party_code=b1.party_code where a.pass_type='" +passtype + "' and " + column + " Ilike '%" + value + "%'  order by request_for_pass_id", cn))
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
                                production.rrnoc_request_id = dr["permit_id"].ToString();
                                production.financial_year = dr["financial_year"].ToString();
                                production.pass_type = dr["pass_type"].ToString();
                                production.issue_nocno = dr["permit"].ToString() + dr["financial_year"].ToString() + "/" + dr["permit_no"].ToString();
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
                                //if (dr["valid_upto"].ToString() != "")
                                //    production.valied_date = dr["valid_upto"].ToString().Substring(0, 10).Replace("/", "-");
                                if (dr["pass_valid_upto"].ToString() != "")
                                    production.pass_valid_upto = dr["pass_valid_upto"].ToString().Substring(0, 10).Replace("/", "-");
                                production.record_status = dr["record_status"].ToString();
                                production.party_code = dr["party_code"].ToString();
                                production.approval_status = dr["approval_status"].ToString();
                                //  production.toparty_code = dr["customer_id"].ToString();
                                production.name = dr["cust_name"].ToString();
                                production.approvedqty = Convert.ToDouble(dr["permit_qty"].ToString());
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


        public static ReaquestForPass GetDetails(string requestid, string pass_type,string financial_year)
        {
            ReaquestForPass request = new ReaquestForPass();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    if (pass_type == "E")
                    {
                        pass_type = "EXP";
                        // cmd = new NpgsqlCommand("select a.*,b.valid_date,b.allocation_qty,b.molasses_supplier,b.party_code,b.rr_date,b.rr_approved_qty,c.Product_name,d.party_name from exciseautomation.request_for_pass a inner join exciseautomation.release_request b on a.rrnoc_request_id = b.release_request_id inner join exciseautomation.product_master as c on b.product_code = c.product_code inner join exciseautomation.party_master as d on d.party_code = b.molasses_supplier  where a.request_for_pass_id='" + requestid + "' and pass_type='" + pass_type + "'   order by a.request_for_pass_id", cn);
                        cmd = new NpgsqlCommand("select a.*,b.valid_upto as valid_date,b.noc_total_qty as allocation_qty,b.customer_id as molasses_supplier,b.party_code,b.nocdate as rr_date,b.noc_total_qty as rr_approved_qty,c.Product_name,d.cust_name from exciseautomation.request_for_pass a inner join exciseautomation.noc b on a.rrnoc_request_id = b.noc_id inner join exciseautomation.product_master as c on b.noc_for = c.product_code inner join exciseautomation.Customer_master as d on b.customer_id = d.customer_id  where a.request_for_pass_id='" + requestid + "' and pass_type='" + pass_type + "' and a.financial_year='"+financial_year+"'   order by a.request_for_pass_id", cn);
                    }
                    else
                    {
                        pass_type = "DOM";
                        cmd = new NpgsqlCommand("select a.*,b.permit_validity as valid_date,b.permit_qty,f.allotted_qty as allocation_qty,b.purchase_from_party as molasses_supplier,b.party_code,b.permit_date as rr_date,b.permit_qty as rr_approved_qty,c.Product_name,h.party_name as cust_name from exciseautomation.request_for_pass a inner join exciseautomation.permit b on a.rrnoc_request_id = b.permit_id inner join exciseautomation.user_registration g on b.purchase_from_party=g.user_id  inner join exciseautomation.party_master h on b.party_code=h.party_code  inner join exciseautomation.molasses_allotment_request f on b.molasses_allotment_request_id=f.molasses_allotment_request_id  inner join exciseautomation.product_master as c on f.product_code = c.product_code  where a.request_for_pass_id='"+requestid+"' and pass_type='"+pass_type+"' and a.financial_year='"+financial_year+"'   order by a.request_for_pass_id", cn);
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
                            if(dr["valid_date"].ToString()!="")
                            request.valied_date = dr["valid_date"].ToString().Substring(0, 10).Replace("/", "-");
                            request.pass_valid_upto = dr["pass_valid_upto"].ToString().Substring(0, 10).Replace("/", "-");
                            request.request_for_pass_date = dr["request_for_pass_date"].ToString().Substring(0, 10).Replace("/", "-");
                            request.route_details = dr["route_details"].ToString();
                            request.temperature= Convert.ToDouble(dr["temperature"].ToString());
                            request.strength = Convert.ToDouble(dr["strength"].ToString());
                            request.indication = Convert.ToDouble(dr["indication"].ToString());
                            request.vehicle_no= dr["vehicle_no"].ToString();
                            request.permitno = dr["permitno"].ToString();
                            request.digital_lock_no= dr["digital_lock_no"].ToString();
                            request.record_status = dr["record_status"].ToString();
                            request.rr_date = dr["rr_date"].ToString().Substring(0, 10).Replace("/", "-");
                            request.alloted_qty = Convert.ToDouble(dr["allocation_qty"].ToString());
                            request.approvedqty = Convert.ToDouble(dr["rr_approved_qty"].ToString());
                            request.party_code = dr["party_code"].ToString();
                            request.toparty_code = dr["molasses_supplier"].ToString();
                            request.approval_status = dr["approval_status"].ToString();
                            request.user_id = dr["user_id"].ToString();
                            if (pass_type != "NOC")
                                request.blance_qty = request.approvedqty - request.approved_qty;
                            request.noc_depotdetail_id = dr["noc_depotdetail_id"].ToString();
                            if (pass_type == "DOM")
                                request.dispatch_qty= Convert.ToDouble(dr["permit_qty"].ToString());
                            if (pass_type == "DOM" || pass_type == "EXP")
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
                    if (pass.pass_type == "E")
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand("Update exciseautomation.request_for_pass set   req_qty='" + pass.req_qty + "',approved_qty='" + pass.req_qty + "', approval_status='"+pass.approval_status+"',  user_id='" + pass.user_id + "',lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "', record_status='" + pass.record_status + "',noc_depotdetail_id='" + pass.noc_depotdetail_id + "',route_details='" + pass.route_details + "', request_for_pass_date='" + pass.request_for_pass_date + "', vehicle_no='" + pass.vehicle_no + "', digital_lock_no='" + pass.digital_lock_no + "', temperature='" + pass.temperature + "', indication='" + pass.indication + "', strength='" + pass.strength + "', pass_valid_upto='" + pass.pass_valid_upto + "',permitno='"+pass.permitno+"' where request_for_pass_id='" + pass.request_for_pass_id + "' and financial_year='" + pass.financial_year + "' ", cn);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand("Update exciseautomation.request_for_pass set  req_qty='" + pass.req_qty + "', approved_qty='" + pass.req_qty + "', approval_status='"+pass.approval_status+"',  user_id='" + pass.user_id + "',lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "', record_status='" + pass.record_status + "',noc_depotdetail_id='" + pass.noc_depotdetail_id + "',route_details='"+pass.route_details+"', request_for_pass_date='"+pass.request_for_pass_date+"', vehicle_no='"+pass.vehicle_no+"', digital_lock_no='"+pass.digital_lock_no+"', temperature='"+pass.temperature+"', indication='"+pass.indication+"', strength='"+pass.strength+"', pass_valid_upto='"+pass.pass_valid_upto+ "',permitno='"+pass.permitno+"' where request_for_pass_id='" + pass.request_for_pass_id + "' and financial_year='" + pass.financial_year + "'", cn);
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
        public static string GetBalance(string id, string pass_type)
        {
            string val = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    if (pass_type == "E")
                    {
                        pass_type = "EXP";
                    }
                    else
                    {
                        pass_type = "DOM";
                    }
                        NpgsqlCommand cmd = new NpgsqlCommand("Select case when sum(req_qty) is null then 0 else sum(req_qty) end approved_qty from exciseautomation.request_for_pass  where rrnoc_request_id='" + id + "' and pass_type='" + pass_type + "' and record_status!='R'", cn);
                    val = cmd.ExecuteScalar().ToString();
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

                    NpgsqlCommand cmd = new NpgsqlCommand("Update exciseautomation.request_for_pass set   approved_qty='" + pass.approved_qty + "',approval_status='"+pass.approval_status+ "',lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "',  record_status='" + pass.record_status + "' where request_for_pass_id='" + pass.request_for_pass_id + "' and financial_year='" + pass.financial_year + "'", cn);
                    cmd.ExecuteNonQuery();
                    val = "0";
                    if(pass.record_status=="I")
                    {
                        NpgsqlCommand cmd4 = new NpgsqlCommand("select case when max(request_for_pass_issueno) is null then 0 else max(request_for_pass_issueno) end as request_for_pass_issueno from exciseautomation.request_for_pass where financial_year='"+pass.financial_year+"' ", cn);
                        int m = Convert.ToInt32(cmd4.ExecuteScalar().ToString()) + 1;
                        NpgsqlCommand cmd3= new NpgsqlCommand("Update exciseautomation.request_for_pass set   request_for_pass_issueno='" +m+ "',request_for_pass_issuedate='" + DateTime.Now.ToString("dd-MM-yyyy") + "' where request_for_pass_id='" + pass.request_for_pass_id + "' and financial_year='" + pass.financial_year + "'", cn);
                        cmd3.ExecuteNonQuery();
                    }
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
                    str.Append("'" + pass.request_for_pass_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "','RTP','" + pass.approval_status + "','" + pass.remarks + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + pass.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + pass.user_id + "','"+pass.financial_year+"','"+pass.party_code+"')");
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
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.valid_date,c.Product_name,b.rr_approved_qty,b.party_code AS PARTY,b.molasses_supplier,b.rr_issueno,d.release_request,p.party_name from exciseautomation.request_for_pass a inner join exciseautomation.release_request b on a.rrnoc_request_id=b.release_request_id inner join exciseautomation.product_master as c on b.product_code=c.product_code left join exciseautomation.document_format_master as d on b.party_code=d.party_code left join exciseautomation.party_master as p on b.party_code=p.party_code where a.pass_type='RR'  order by a.request_for_pass_id,b.valid_date", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        request = new List<ReaquestForPass>();

                        while (dr.Read())
                        {
                            ReaquestForPass production = new ReaquestForPass();
                            production.request_for_pass_id = dr["request_for_pass_id"].ToString();
                            production.rrnoc_request_id = dr["rrnoc_request_id"].ToString();
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


        public static List<NOC_Details> GetNOCList1(string pass_type)
        {
            List<NOC_Details> noc = new List<NOC_Details>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.Cust_name,c.district_code,c.party_name,d.noc,f.product_name from exciseautomation.noc a inner join exciseautomation.customer_master b on a.customer_id=b.customer_id and a.party_code=b.party_code inner join exciseautomation.party_master c on a.party_code=c.party_code left join exciseautomation.document_format_master d on a.party_code=d.party_code inner join exciseautomation.product_master f on a.noc_for =f.product_code   order by a.party_code,req_nocno", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        NOC_Details no = new NOC_Details();
                        no.noc_id = dr["noc_id"].ToString();
                        no.req_nocno = "Req/" + dr["noc"].ToString() + dr["financial_year"].ToString() + "/" + dr["req_nocno"].ToString();
                        no.slno = Convert.ToInt32(dr["req_nocno"].ToString());
                        no.Cust_name = dr["Cust_name"].ToString().Trim();
                        no.noc_total_qty = Convert.ToDouble(dr["noc_total_qty"].ToString());
                        no.record_status = dr["record_status"].ToString();
                        if (dr["valid_upto"].ToString() != "")
                            no.valid_upto = dr["valid_upto"].ToString().Substring(0, 10).Replace("/", "-");
                        no.noc_status = dr["noc_status"].ToString();
                        no.party_code = dr["party_code"].ToString();
                        no.product_name = dr["product_name"].ToString();
                        no.party_name = dr["party_name"].ToString().Trim();
                        if (dr["issue_nocno"].ToString() != "")
                            no.issue_nocno = dr["noc"].ToString() + dr["financial_year"].ToString() + "/" + dr["issue_nocno"].ToString();
                        no.district = dr["District_Code"].ToString();
                        //  no.req_qty = Convert.ToDouble(dr["req_qty"].ToString());
                        //if(no.party_code=="MDD")
                        cmd = new NpgsqlCommand("select case when sum(req_qty) is null then 0 else sum(req_qty) end as req_qty from exciseautomation.request_for_pass where record_status!='R' and rrnoc_request_id='" + no.noc_id + "' and party_code='" + no.party_code + "'", cn);
                        no.noc_lifted_qty = Convert.ToDouble(cmd.ExecuteScalar().ToString());
                        cmd = new NpgsqlCommand("select case when sum(totalqty) is null then 0 else sum(totalqty) end as totalqty from exciseautomation.noc_depotdetail where noc_id='" + no.noc_id + "' and financial_year='"+no.financial_year+"'", cn);
                        no.req_qty = Convert.ToDouble(cmd.ExecuteScalar().ToString());
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
    }
}
