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
    public class DL_Release_Request
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static List<Release_Request> GetList()
        {
            List<Release_Request> rr = new List<Release_Request>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,case when b.rrapqty is null then 0 else b.rrapqty end  as rr_approved_qty,case when b.liftedqty is null then 0 else b.liftedqty end as rr_lifted_qty,x.molasses_allotment from exciseautomation.view_release_request_list a left join exciseautomation.view_rr_approved_qty b on a.final_allotmentno=b.rr_allotmentno and a.party_code=b.party_code and a.financial_year=b.financial_year   left join exciseautomation.document_format_master x on x.party_code=a.party_code  order by a.allotment_validdate desc", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        rr = new List<Release_Request>();
                        while (dr.Read())
                        {
                            Release_Request request = new Release_Request();
                            request.rr_allotmentno = dr["final_allotmentno"].ToString();
                            request.product_code = dr["product_code"].ToString();
                           
                            request.prov_indent_qty = Convert.ToDouble(dr["prov_indent_qty"].ToString());
                          

                            request.allocation_qty = Convert.ToDouble(dr["allotted_qty"].ToString());
                            if (dr["allotment_validdate"].ToString() != "")
                            {
                                request.valid_date = dr["allotment_validdate"].ToString().Substring(0, 10).Replace("/", "-");
                                
                            }
                            request.rr_approved_qty = Convert.ToDouble(dr["rr_approved_qty"].ToString());
                            request.rr_balance_qty = request.allocation_qty - request.rr_approved_qty;
                            request.party_name = dr["party_name"].ToString();
                            request.product_name = dr["product_name"].ToString();
                            request.party_code = dr["party_code"].ToString();
                            request.from_party = dr["requested_fromunit"].ToString();
                            request.rr_quantity =Convert.ToDouble( dr["rr_quantity"].ToString());
                            request.financial_year = dr["financial_year"].ToString();
                            request.district_code = dr["district_code"].ToString();
                            request.final_allotment_no = dr["molasses_allotment"].ToString() + dr["financial_year"].ToString() + "/" + dr["final_allotmentno"].ToString();
                          //  request.remarks = dr["attribute1"].ToString();
                            rr.Add(request);

                        }
                    }
                    cn.Close();
                    _log.Info("Get RR List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get RR List Fail:" + ex.Message);

                }
            }
            return rr;

        }

        public static List<Release_Request> Search(string tablename, string column, string value)
        {
            List<Release_Request> mir = new List<Release_Request>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,case when b.rrapqty is null then 0 else b.rrapqty end  as rr_approved_qty,case when b.liftedqty is null then 0 else b.liftedqty end as rr_lifted_qty,x.molasses_allotment from exciseautomation.view_release_request_list a left join exciseautomation.view_rr_approved_qty b on a.final_allotmentno=b.rr_allotmentno and a.party_code=b.party_code  left join exciseautomation.document_format_master x on x.party_code=a.party_code where " + column + " Ilike '%" + value + "%' order by a.allotment_validdate desc", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            mir = new List<Release_Request>();
                            while (dr.Read())
                            {
                                Release_Request request = new Release_Request();
                                request.rr_allotmentno = dr["final_allotmentno"].ToString();
                                request.product_code = dr["product_code"].ToString();

                                request.prov_indent_qty = Convert.ToDouble(dr["prov_indent_qty"].ToString());


                                request.allocation_qty = Convert.ToDouble(dr["allotted_qty"].ToString());
                                if (dr["allotment_validdate"].ToString() != "")
                                {
                                    request.valid_date = dr["allotment_validdate"].ToString().Substring(0, 10).Replace("/", "-");

                                }
                                request.rr_approved_qty = Convert.ToDouble(dr["rr_approved_qty"].ToString());
                                request.rr_balance_qty = request.allocation_qty - request.rr_approved_qty;
                                request.party_name = dr["party_name"].ToString();
                                request.product_name = dr["product_name"].ToString();
                                request.party_code = dr["party_code"].ToString();
                                request.from_party = dr["requested_fromunit"].ToString();
                                request.rr_quantity = Convert.ToDouble(dr["rr_quantity"].ToString());
                                request.financial_year = dr["financial_year"].ToString();
                                request.district_code = dr["district_code"].ToString();
                                request.final_allotment_no = dr["molasses_allotment"].ToString() + dr["financial_year"].ToString() + "/" + dr["final_allotmentno"].ToString();
                                //  request.remarks = dr["attribute1"].ToString();
                                mir.Add(request);

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


        public static string Approve(Release_Request rr)
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
                    NpgsqlCommand cmd=new NpgsqlCommand();
                    if (rr.record_status != "I")
                    {
                        double balance = rr.rr_approved_qty;
                        if (rr.record_status == "A")
                        {
                            str = new StringBuilder();
                            str.Append("update exciseautomation.release_request set  record_status='" + rr.record_status + "',approval_status='" + rr.approval_status + "',rr_approved_qty='" + rr.rr_approved_qty + "',rr_balance_qty='" + rr.rr_balance_qty + "',valid_date='" + rr.valid_date + "'  where release_request_id='" + rr.release_request_id + "' and financial_year='" + rr.financial_year + "'");
                            cmd = new NpgsqlCommand(str.ToString(), cn);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            str = new StringBuilder();
                            str.Append("update exciseautomation.release_request set  record_status='" + rr.record_status + "',approval_status='" + rr.approval_status + "',rr_approved_qty='0',rr_balance_qty='" + rr.rr_balance_qty + "'  where release_request_id='" + rr.release_request_id + "' and financial_year='" + rr.financial_year + "'");
                            cmd = new NpgsqlCommand(str.ToString(), cn);
                            cmd.ExecuteNonQuery();
                        }
                        str = new StringBuilder();
                        str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                        str.Append("'" + rr.release_request_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy") + "','RRL','" + rr.approval_status + "','" + rr.remarks + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + rr.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + rr.user_id + "','"+rr.financial_year+"','"+rr.party_code+"')");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        int n = cmd.ExecuteNonQuery();
                        VAL = "0";
                        _log.Info("RR " + rr.approval_status + " Sucess:" + rr.release_request_id + '-' + rr.party_code);
                        trn.Commit();
                        cn.Close();
                    }
                    else
                    {
                      
                        NpgsqlCommand cmd1 = new NpgsqlCommand("select case when max(rr_issueno) is null then 0 else max(rr_issueno) end as rr_issueno from exciseautomation.release_request where party_code='" + rr.party_code + "'and financial_year='"+rr.financial_year+"'", cn);
                        int maxid = Convert.ToInt32(cmd1.ExecuteScalar()) + 1;
                        str = new StringBuilder();
                        str.Append("update exciseautomation.release_request set rr_issueno='"+maxid+"', record_status='" + rr.record_status + "',approval_status='Issued'  where release_request_id='" + rr.release_request_id + "' and financial_year='" + rr.financial_year + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        int n = cmd.ExecuteNonQuery();
                       
                        str = new StringBuilder();
                        str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                        str.Append("'" + rr.release_request_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy") + "','RRL','" + rr.approval_status + " ','Issued','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + rr.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + rr.user_id + "','"+rr.financial_year+"','"+rr.party_code+"')");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        n = cmd.ExecuteNonQuery();
                        VAL = "0";
                        _log.Info("RR " + rr.approval_status + " Sucess:" + rr.release_request_id + '-' + rr.party_code);
                        trn.Commit();
                        cn.Close();
                    }

                }
                catch (Exception ex1)
                {
                    trn.Rollback();
                    _log.Info("RR " +rr.approval_status + " Fail:" + rr.release_request_id + '-' + rr.party_code + "-" + ex1.Message);
                    VAL = ex1.Message;
                }

            }
            return VAL;
        }



        public static string Adminupdate(Release_Request rr)
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
                    NpgsqlCommand cmd = new NpgsqlCommand();

                        str = new StringBuilder();
                        str.Append("update exciseautomation.release_request set valid_date='" + rr.valid_date + "',rr_approved_qty='" + rr.rr_approved_qty + "'   where release_request_id='" + rr.release_request_id + "' and financial_year='" + rr.financial_year + "'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        int n = cmd.ExecuteNonQuery();
                        str = new StringBuilder();
                 
                        str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year) VALUES(");
                        str.Append("'" + rr.release_request_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy") + "','RRL','Updated By Admin','"+rr.record_status+"','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + rr.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + rr.user_id + "','"+rr.financial_year+"')");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        n = cmd.ExecuteNonQuery();
                        VAL = "0";
                        _log.Info("RR " + rr.approval_status + " Sucess:" + rr.release_request_id + '-' + rr.party_code);
                        trn.Commit();
                        cn.Close();

                }
                catch (Exception ex1)
                {
                    trn.Rollback();
                    _log.Info("RR " + rr.approval_status + " Fail:" + rr.release_request_id + '-' + rr.party_code + "-" + ex1.Message);
                    VAL = ex1.Message;
                }

            }
            return VAL;
        }

        public static Release_Request GetDetails(string rrno,string financial_year)
        {
            Release_Request request = new Release_Request();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    string[] rr = rrno.Split('_');
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name,c.product_name,d.rrapqty,d.liftedqty,e.rrqty,x.Molasses_Allotment,y.allotment_validdate,x.release_request from exciseautomation.release_request a inner join exciseautomation.party_master b on a.molasses_supplier=b.party_code inner join exciseautomation.product_master c on a.product_code=c.product_code  left join exciseautomation.view_rr_approved_qty d on a.rr_allotmentno=d.rr_allotmentno and a.party_code=d.party_code left join exciseautomation.view_rr_qty e on a.rr_allotmentno=e.rr_allotmentno and a.party_code=e.party_code left join exciseautomation.document_format_master x on x.party_code=a.party_code left join exciseautomation.molasses_allotment_request y on a.rr_allotmentno=y.final_allotmentno and a.party_code=y.party_code and a.financial_year=y.financial_year where release_request_id='" + rr[0]+"' and a.party_code='"+rr[1]+ "' and a.financial_year='"+financial_year+"' and y.record_active='true'", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {

                        request.release_request_id = dr["release_request_id"].ToString();
                       // request.rr_reqno=
                        request.rr_allotmentno = dr["rr_allotmentno"].ToString();
                        request.financial_year = dr["financial_year"].ToString();
                        request.product_code = dr["product_code"].ToString();
                        request.allocation_qty = Convert.ToDouble(dr["allocation_qty"].ToString());
                        request.allotment_date = dr["allotment_validdate"].ToString().Substring(0, 10).Replace("/", "-"); 
                        if (dr["valid_date"].ToString()!="")
                        request.valid_date = dr["valid_date"].ToString().Substring(0, 10).Replace("/", "-");
                        if(dr["rrapqty"].ToString()!="")
                        request.rr_approved_qty = Convert.ToDouble(dr["rrapqty"].ToString());
                        request.rr_balance_qty = request.allocation_qty - request.rr_approved_qty;
                        if (dr["liftedqty"].ToString() != "")
                            request.rr_lifted_qty = Convert.ToDouble(dr["liftedqty"].ToString());
                        request.party_name = dr["party_name"].ToString();
                        request.product_name = dr["product_name"].ToString();
                        request.party_code = dr["party_code"].ToString();
                        request.from_party = dr["molasses_supplier"].ToString();
                        request.rr_quantity = Convert.ToDouble(dr["rr_quantity"].ToString());
                        request.record_status = dr["record_status"].ToString();
                        request.approval_status = dr["approval_status"].ToString();
                        request.rr_reqno ="Req/"+dr["release_request"] + dr["financial_year"].ToString() + "/" + dr["rr_reqno"].ToString();
                        request.rr_date = dr["rr_date"].ToString().Substring(0, 10).Replace("/", "-");
                     
                        if(dr["rrqty"].ToString()!="")
                        request.rrqty = Convert.ToDouble(dr["rrqty"].ToString());
                        request.final_allotment_no = dr["molasses_allotment"].ToString() + dr["financial_year"].ToString() + "/" + dr["rr_allotmentno"].ToString();
                        request.remarks = dr["attribute1"].ToString();
                        using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.eascm_docs where doc_id='" + request.release_request_id + "' and doc_type_code='RRL' and financial_year='" + financial_year + "' order by eascm_docs_id", cn))
                        {
                            cmd1.CommandType = System.Data.CommandType.Text;
                            //cmd.Parameters.AddWithValue("@UserID", userid);
                            NpgsqlDataReader dr2 = cmd1.ExecuteReader();
                            request.doc = new List<EASCM_DOCS>();
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
                                    request.doc.Add(doc);
                                }

                            }

                        }

                    }
                    
                    cn.Close();
                    _log.Info("Get RR Details Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get RR Details Fail:" + ex.Message);

                }
            }
            return request;
        }

        public static List<Release_Request> GetRRList()
        {
            List<Release_Request> rr = new List<Release_Request>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,d.party_name,d.district_code,f.district_code as suplier_district,c.product_name,f.party_name as supplier,g.release_request,g.molasses_allotment from exciseautomation.release_request a  inner join exciseautomation.product_master c on a.product_code=c.product_code inner join exciseautomation.party_master d on a.party_code=d.party_code inner join exciseautomation.party_master f on a.molasses_supplier=f.party_code left join exciseautomation.document_format_master g on g.party_code=a.party_code order by a.party_code,release_request_id", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Release_Request request = new Release_Request();
                            request.release_request_id = dr["release_request_id"].ToString();
                            request.rr_allotmentno = dr["rr_allotmentno"].ToString();// + dr["rr_allotmentno"].ToString();
                        request.financial_year = dr["financial_year"].ToString();
                        request.final_allotment_no = dr["molasses_allotment"] + dr["financial_year"].ToString() + "/" + dr["rr_allotmentno"].ToString();
                            request.product_code = dr["product_code"].ToString();
                            request.allocation_qty = Convert.ToDouble(dr["allocation_qty"].ToString());

                       // request.allotment_date = dr["final_allotmentdate"].ToString().Substring(0, 10).Replace("/", "-");
                        if (dr["valid_date"].ToString() != "")
                            request.valid_date = dr["valid_date"].ToString().Substring(0, 10).Replace("/", "-");
                        request.rr_approved_qty = Convert.ToDouble(dr["rr_approved_qty"].ToString());
                            request.rr_balance_qty = Convert.ToDouble(dr["rr_balance_qty"].ToString());
                            request.party_name = dr["party_name"].ToString();
                            request.product_name = dr["product_name"].ToString();
                            request.party_code = dr["party_code"].ToString();
                            request.from_party = dr["molasses_supplier"].ToString();
                            request.molasses_supplier = dr["supplier"].ToString();
                            request.rr_quantity = Convert.ToDouble(dr["rr_quantity"].ToString());
                            request.record_status = dr["record_status"].ToString();
                            request.approval_status = dr["approval_status"].ToString();
                            request.rr_reqno = dr["rr_reqno"].ToString();
                            request.rr_date = dr["rr_date"].ToString().Substring(0, 10).Replace("/", "-");
                     
                            request.district_code = dr["district_code"].ToString();
                            request.suplier_district = dr["suplier_district"].ToString();
                            if (dr["rr_issueno"].ToString()!="")
                            request.rr_issueno =dr["release_request"] + dr["financial_year"].ToString() + "/" + dr["rr_issueno"].ToString();
                            request.remarks = dr["attribute1"].ToString();
                            cmd = new NpgsqlCommand("select case when sum(req_qty) is null then 0 else sum(req_qty) end as req_qty from exciseautomation.request_for_pass where record_status!='R' and rrnoc_request_id='" + request.release_request_id + "' and pass_type='RR' and party_code='" + request.party_code + "'", cn);
                            request.rr_lifted_qty= Convert.ToDouble(cmd.ExecuteScalar().ToString());
                             
                            rr.Add(request);

                        }
                    
                    cn.Close();
                    _log.Info("Get RR List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get RR List Fail:" + ex.Message);

                }
            }
                return rr;
            }
        public static List<Release_Request> Search1(string tablename, string column, string value)
        {
            List<Release_Request> mir = new List<Release_Request>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;

                    cmd = new NpgsqlCommand("select a.*,d.party_name,d.district_code,f.district_code as suplier_district,c.product_name,f.party_name as supplier,g.release_request,g.molasses_allotment from exciseautomation.release_request a  inner join exciseautomation.product_master c on a.product_code=c.product_code inner join exciseautomation.party_master d on a.party_code=d.party_code inner join exciseautomation.party_master f on a.molasses_supplier=f.party_code left join exciseautomation.document_format_master g on g.party_code=a.party_code where " + column + " Ilike '%" + value + "%' order by a.party_code,release_request_id", cn);

                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Release_Request request = new Release_Request();
                                request.release_request_id = dr["release_request_id"].ToString();
                                request.rr_allotmentno = dr["rr_allotmentno"].ToString();// + dr["rr_allotmentno"].ToString();
                                request.financial_year = dr["financial_year"].ToString();
                               request.final_allotment_no = dr["molasses_allotment"] + dr["financial_year"].ToString() + "/" + dr["rr_allotmentno"].ToString();
                                request.product_code = dr["product_code"].ToString();
                                request.allocation_qty = Convert.ToDouble(dr["allocation_qty"].ToString());

                                // request.allotment_date = dr["final_allotmentdate"].ToString().Substring(0, 10).Replace("/", "-");
                                if (dr["valid_date"].ToString() != "")
                                    request.valid_date = dr["valid_date"].ToString().Substring(0, 10).Replace("/", "-");
                                request.rr_approved_qty = Convert.ToDouble(dr["rr_approved_qty"].ToString());
                                request.rr_balance_qty = Convert.ToDouble(dr["rr_balance_qty"].ToString());
                                request.party_name = dr["party_name"].ToString();
                                request.product_name = dr["product_name"].ToString();
                                request.party_code = dr["party_code"].ToString();
                                request.from_party = dr["molasses_supplier"].ToString();
                                request.molasses_supplier = dr["supplier"].ToString();
                                request.rr_quantity = Convert.ToDouble(dr["rr_quantity"].ToString());
                                request.record_status = dr["record_status"].ToString();
                                request.approval_status = dr["approval_status"].ToString();
                                request.rr_reqno = dr["rr_reqno"].ToString();
                                request.rr_date = dr["rr_date"].ToString().Substring(0, 10).Replace("/", "-");
                             
                                request.district_code = dr["district_code"].ToString();
                                request.suplier_district = dr["suplier_district"].ToString();
                                if (dr["rr_issueno"].ToString() != "")
                                    request.rr_issueno = dr["release_request"] + dr["financial_year"].ToString() + "/" + dr["rr_issueno"].ToString();
                                request.remarks = dr["attribute1"].ToString();
                                cmd = new NpgsqlCommand("select case when sum(req_qty) is null then 0 else sum(req_qty) end as req_qty from exciseautomation.request_for_pass where record_status!='R' and rrnoc_request_id='" + request.release_request_id + "' and pass_type='RR' and party_code='" + request.party_code + "'", cn);
                                request.rr_lifted_qty = Convert.ToDouble(cmd.ExecuteScalar().ToString());

                                mir.Add(request);

                            }
                    cn.Close();
                    _log.Info("Get RR List Success");

                }
                catch (Exception ex)
                {
                    _log.Info("Get RR List Fail:" + ex.Message);
                }

            }
            return mir;
        }


        public static string Update(Release_Request rr)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    int max = Convert.ToInt32(DL_Org_List.GetMaxID("Module_master").ToString()) + 1;
                    StringBuilder str = new StringBuilder();
                    str.Append("update exciseautomation.release_request set rr_quantity ='"+rr.rr_quantity+ "',user_id='"+rr.user_id+ "', lastmodified_date='"+DateTime.Now.ToString("dd-MM-yyyy")+"', record_status='"+rr.record_status+ "',attribute1='" + rr.remarks+"'  where release_request_id='" + rr.release_request_id + "' and financial_year='"+rr.financial_year+"'");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    
                        if (n == 1)
                        {
                            NpgsqlCommand cmd1 = new NpgsqlCommand("delete from exciseautomation.eascm_docs where doc_id='" + rr.release_request_id + "' and doc_type_code='RRL' and financial_year='" + rr.financial_year + "'", cn);
                            cmd1.ExecuteNonQuery();
                            for (int i = 0; i < rr.doc.Count; i++)
                            {
                                n = 0;
                                str = new StringBuilder();
                                str.Append("INSERT INTO exciseautomation.eascm_docs(doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,financial_year,party_code)");
                                str.Append("Values('" + rr.release_request_id + "','" + rr.doc[i].doc_name + "', '" + rr.doc[i].description + "','" + rr.doc[i].doc_path + "','RRL','" + rr.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+rr.financial_year+"','"+rr.party_code+"')");
                                NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                                n = cmd3.ExecuteNonQuery();
                            }
                            VAL = "0";
                        }
                   
                    cn.Close();
                   // _log.Info("Module Insertion Sucess:" + module.module_code + '-' + module.module_name);

                }
                catch (Exception ex1)
                {
                   // _log.Info("Module Insertion Fail:" + module.module_code + '-' + module.module_name + "-" + ex1.Message);

                    VAL = ex1.Message;
                }

            }
            return VAL;
        }

        public static string Insert(Release_Request rr)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd1 = new NpgsqlCommand("select case when max(release_request_id) is null then 0 else max(release_request_id) end as release_request_id from exciseautomation.release_request where financial_year='"+rr.financial_year+"' ", cn);
                    int maxid = Convert.ToInt32(cmd1.ExecuteScalar()) + 1;
                    StringBuilder str = new StringBuilder();
                    string[] allotmentno = rr.rr_allotmentno.Split('/');
                    string[] rr_reqno = rr.rr_reqno.Split('/');
                    str.Append("INSERT INTO exciseautomation.release_request(release_request_id, financial_year,rr_reqno, rr_date, product_code, rr_allotmentno, party_code, molasses_supplier, allocation_qty,");
                    str.Append("rr_quantity, rr_balance_qty, rr_approved_qty, rr_lifted_qty,   user_id, creation_date, record_status,attribute1)values(");
                    str.Append("'"+maxid+"','"+rr.financial_year+"','"+ rr_reqno[rr_reqno.Length-1] + "','"+rr.rr_date+"','"+rr.product_code+"','"+allotmentno[allotmentno.Length-1]+"','"+rr.party_code+"','"+rr.from_party+"','"+rr.allocation_qty+"',");
                    str.Append("'"+rr.rr_quantity+"','"+rr.rr_balance_qty+"','"+rr.rr_approved_qty+"','"+rr.rr_lifted_qty+"','"+rr.user_id+"','"+rr.rr_date+"','"+rr.record_status+"','"+rr.remarks+"')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                       // trn.Rollback();
                        VAL = "0";
                        for (int i1 = 0; i1 < rr.doc.Count; i1++)
                        {
                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.eascm_docs(doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,financial_year,party_code)");
                            str.Append("Values('" + maxid + "','" + rr.doc[i1].doc_name + "', '" + rr.doc[i1].description + "','" + rr.doc[i1].doc_path + "','RRL','" + rr.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+rr.financial_year+"','"+rr.party_code+"')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            int r = cmd3.ExecuteNonQuery();
                        }
                    }
                    else
                        VAL = "1";
                    trn.Commit();
                    cn.Close();
                    _log.Info("Module Insertion Sucess:" + rr.rr_reqno + '-' + rr.party_code);

                }
                catch (Exception ex1)
                {
                    _log.Info("Module Insertion Fail:" + rr.rr_reqno + '-' + rr.party_code + " - " + ex1.Message);
                    VAL = ex1.Message;
                    trn.Rollback();
                }

            }
            return VAL;
        }

        public static string GetMax(string party_code)
        {
            string value = "0"; 
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select case when max(a.rr_reqno) is null then 0 else max(a.rr_reqno) end as rr_reqno,g.release_request,a.financial_year from exciseautomation.release_request a inner join exciseautomation.document_format_master g on g.party_code=a.party_code where a.party_code='" + party_code+ "' group by a.party_code,g.release_request,a.financial_year", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close(); 
                    foreach (DataRow dr in dt.Rows)
                    {

                      value ="Req/"+dr["release_request"].ToString() + dr["financial_year"].ToString() + "/"+ Convert.ToInt32(Convert.ToInt32(cmd.ExecuteScalar()) + 1);
                    }
                    if (dt.Rows.Count == 0)
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("select release_request from  exciseautomation.document_format_master  where party_code='" + party_code + "'", cn);
                        value = "Req/" + cmd1.ExecuteScalar() + "1";
                    }
                    //   value = maxid.ToString();
                    _log.Info("Get RR No Sucess:" + value);
                }
                catch(Exception ex)
                {
                    value = ex.Message;
                    _log.Info("Get RR No Fail:" + ex.Message);
                }
            }
            return value;
        }
    }
}
