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
    public class DL_Pass_Details
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static string Insert(Pass_Details pass)
        {
            string VAL = "";
            int m=0;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    StringBuilder str = new StringBuilder();
                    NpgsqlCommand cmd1 = new NpgsqlCommand("Select case when max(pass_id) is null then 0 else max(pass_id) end as pass_id from exciseautomation.pass where financial_year='"+pass.financial_year+"'", cn);
                    m = Convert.ToInt32(cmd1.ExecuteScalar())+1;
                    cmd1 = new NpgsqlCommand("Select case when max(pass_reqno) is null then 0 else max(pass_reqno) end as pass_reqno from exciseautomation.pass where supplier_unit='" + pass.supplier_unit+ "' and financial_year='" + pass.financial_year + "'", cn);
                    int m1 = Convert.ToInt32(cmd1.ExecuteScalar()) + 1;
                    if (pass.pass_type == "M"|| pass.pass_type == "RR")
                    {
                        pass.pass_type = "RR";
                        str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.pass(pass_id, pass_reqno,request_for_pass_id, pass_date,pass_for, pass_type, rrnoc_record_request_id,");
                        str.Append("validupto, supplier_unit, dispatch_type_id, dispatch_qty,to_dispatch_vat,");
                        str.Append("prev_prod_year, brix, sugar_content, taxinvoice, remarks,carrier, vehicle_no, vehicle_type, driver, challan_no, digital_lock_no, dispatch_date, dispatch_time, dispatch_duration, route_details, creation_date,  user_id, record_status,party_code,financial_year)values(");
                        str.Append("'" + m + "','" + m1 + "','"+pass.request_for_pass_id+"', '" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + pass.pass_for + "','" + pass.pass_type + "','" + pass.rrnoc_record_request_id + "',");
                        str.Append("'" + pass.allotment_validupto + "','" + pass.supplier_unit + "','" + pass.dispatch_type_id + "','" + pass.dispatch_qty + "','"+pass.to_dispatch_vat+"',");
                        str.Append("'" + pass.prev_prod_year + "','" + pass.brix + "','" + pass.sugar_content + "','" + pass.taxinvoice + "','" + pass.remarks + "','" + pass.carrier + "','" + pass.vehicle_no + "','" + pass.vehicle_type + "','" + pass.driver + "','" + pass.challan_no + "','" + pass.digital_lock_no + "','" + pass.dispatch_date + "','" + pass.dispatch_time + "','" + pass.dispatch_duration + "','" + pass.route_details + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + pass.user_id + "','" + pass.record_status + "','"+pass.party_code+"','"+pass.financial_year+"')");
                    }
                   
                  else
                    {
                        pass.pass_type = "NOC";
                        str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.pass(pass_id, pass_reqno,request_for_pass_id, pass_date,pass_for, pass_type, rrnoc_record_request_id,");
                        str.Append("noc_depotdetail_id, validupto,  customer_id, dispatch_type_id, dispatch_qty,  to_dispatch_vat,supplier_unit,");
                        str.Append("taxinvoice, remarks,carrier, vehicle_no, vehicle_type, driver, challan_no, digital_lock_no, dispatch_date, dispatch_time, dispatch_duration, route_details,  creation_date,  user_id, record_status,party_code,financial_year)values(");
                        str.Append("'" + m + "','" + m1 + "','"+pass.request_for_pass_id+"','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + pass.pass_for + "','" + pass.pass_type + "','" + pass.rrnoc_record_request_id + "',");
                        str.Append("'" + pass.noc_depotdetail_id + "','" + pass.allotment_validupto + "','" + pass.customer_id + "','" + pass.dispatch_type_id + "','" + pass.dispatch_qty + "','" + pass.to_dispatch_vat + "','"+pass.supplier_unit+"',");
                        str.Append("'" + pass.taxinvoice + "','" + pass.remarks + "','" + pass.carrier + "','" + pass.vehicle_no + "','" + pass.vehicle_type + "','" + pass.driver + "','" + pass.challan_no + "','" + pass.digital_lock_no + "','" + pass.dispatch_date + "','" + pass.dispatch_time + "','" + pass.dispatch_duration + "','" + pass.route_details + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + pass.user_id + "','" + pass.record_status + "','"+pass.party_code+"','"+pass.financial_year+"')");
                    }
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (pass.record_status == "Y")
                    {
                        NpgsqlCommand cmd2 = new NpgsqlCommand("select case when sum(lifted_qty) is null then 0 else sum(lifted_qty) end from exciseautomation.request_for_pass  where request_for_pass_id='" + pass.request_for_pass_id + "' and party_code='"+pass.party_code+"' and financial_year='" + pass.financial_year + "'", cn);
                        double lift = Convert.ToDouble(cmd2.ExecuteScalar()) + pass.dispatch_qty;
                        cmd2 = new NpgsqlCommand("update exciseautomation.request_for_pass set lifted_qty='" + lift + "' where request_for_pass_id='" + pass.request_for_pass_id + "'and party_code='" + pass.party_code + "' and financial_year='" + pass.financial_year + "'", cn);
                        cmd2.ExecuteNonQuery();
                    }
                    VAL = "0";
                    trn.Commit();
                    cn.Close();
                    _log.Info("Pass Insertion Sucess:" + m+ '-' + pass.party_code);

                }
                catch (Exception ex1)
                {
                    _log.Info("Pass Insertion Fail:" + m + '-' + pass.party_code + " - " + ex1.Message);
                    VAL = ex1.Message;
                    trn.Rollback();
                }

            }
            return VAL;
        }

        public static Pass_Details GetPassDetails(string passno, string financial_year)
        {
            Pass_Details pass = new Pass_Details();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("Select a.*,b.party_name,c.cust_name,d.dispatch_type_name from exciseautomation.pass a left join exciseautomation.party_master b on a.party_code=b.party_code left join exciseautomation.customer_master c on a.customer_id=c.customer_id inner join exciseautomation.dispatch_type_master d on a.dispatch_type_id=d.dispatch_type_id  where a.pass_id='" + passno + "' and a.financial_year='"+financial_year+"'", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {

                                pass.pass_id = dr["pass_id"].ToString();
                                pass.pass_for = dr["pass_for"].ToString();
                                pass.dispatch_qty = Convert.ToDouble(dr["dispatch_qty"].ToString());
                                pass.dispatch_type_id = dr["dispatch_type_id"].ToString();
                                pass.dispatch_type_name = dr["dispatch_type_name"].ToString();
                                pass.dispatch_qty = Convert.ToDouble(dr["dispatch_qty"].ToString());
                                pass.to_dispatch_vat= dr["to_dispatch_vat"].ToString();
                                pass.pass_type = dr["pass_type"].ToString();
                                pass.rem_pass_qty =Convert.ToDouble( dr["rem_pass_qty"].ToString());
                               

                                pass.digital_lock_no = dr["digital_lock_no"].ToString();
                                if (dr["dispatch_date"].ToString() != "")
                                    pass.dispatch_date = dr["dispatch_date"].ToString().Substring(0, 10);
                                pass.dispatch_time = dr["dispatch_time"].ToString();
                                if(pass.pass_type=="NOC")
                                {
                                    pass.customer_id = dr["customer_id"].ToString();
                                    pass.cutomer_name = dr["cust_name"].ToString();
                                }
                                else
                                {
                                    pass.customer_id = dr["party_code"].ToString();
                                    pass.cutomer_name = dr["party_name"].ToString();
                                }

                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Pass List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Pass List Fail :" + ex.Message);
                }

            }
            return pass;
        }

        public static string GetAvailableQTY(string vat,string financial_year)
        {
            string VAL = "";
            int m = 0;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("Select a.Vat_code, case when sum(vat_availablecapacity) is null then 0 else sum(vat_availablecapacity) end as vat_availablecapacity ,case when sum( openingbalancevalue) is null then 0 else sum(openingbalancevalue) end as openingbalancevalue   from exciseautomation.vat_master a left join exciseautomation.openingbalance b on a.vat_code=b.vat_code and b.record_status='A'   where a.vat_code='" + vat+ "'and b.financial_year='"+financial_year+"' group by a.vat_code");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        
                        while (dr.Read())
                        {
                            double avilable = Convert.ToDouble(dr["vat_availablecapacity"]);
                          
                            double total = avilable;// + opening;
                            VAL = total.ToString();
                        }
                    }
                    dr.Close();
                    str = new StringBuilder();
                    str.Append("Select case when sum(dispatch_qty) is null then 0 else sum(dispatch_qty) end as dispatch_qty  from exciseautomation.pass where to_dispatch_vat='" + vat + "'and financial_year='" + financial_year + "' and (record_status='I' or record_status='P' or record_status='Y')");
                    cmd = new NpgsqlCommand(str.ToString(), cn);
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            double avilable = Convert.ToDouble(dr["dispatch_qty"]);
                            double total = avilable;// + opening;
                            VAL = VAL +"_"+ total;
                        }
                    }
                    dr.Close();
                    trn.Commit();
                    cn.Close();

                }
                catch (Exception ex1)
                {
                   
                    VAL = ex1.Message;
                    if(ex1.Message!="")
                    trn.Rollback();
                }
                return VAL;
            }
        }

        public static string Adminupdate(Pass_Details pass)
        {
            string VAL = "";
            // int m = 0;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                        StringBuilder str = new StringBuilder();
                        str.Append("update exciseautomation.pass  set to_dispatch_vat='" + pass.to_dispatch_vat + "',taxinvoice='" + pass.taxinvoice + "', carrier='" + pass.carrier + "',vehicle_no='" + pass.vehicle_no + "',vehicle_type='" + pass.vehicle_type + "',driver='" + pass.driver + "',challan_no='" + pass.challan_no + "',digital_lock_no='" + pass.digital_lock_no + "',dispatch_date='" + pass.dispatch_date + "',dispatch_time='" + pass.dispatch_time + "',dispatch_duration='" + pass.dispatch_duration + "',route_details='" + pass.route_details + "' where pass_id='" + pass.pass_id + "' and financial_year='" + pass.financial_year + "'  ");
                        NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();
                            str = new StringBuilder();
                            str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year) VALUES(");
                            str.Append("'" + pass.pass_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy") + "','Pass','Updated By Admin','Updated By Admin','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + pass.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + pass.user_id + "','"+pass.financial_year+"')");
                            cmd = new NpgsqlCommand(str.ToString(), cn);
                            cmd.ExecuteNonQuery();
                   
                    VAL = "0";
                    trn.Commit();
                    cn.Close();
                    _log.Info("Pass Issued Sucess:" + pass.party_code);

                }
                catch (Exception ex1)
                {
                    _log.Info("Pass Issued Fail:" + pass.party_code + " - " + ex1.Message);
                    VAL = ex1.Message;
                    trn.Rollback();
                }
                return VAL;
            }
        }

        public static string Issue(Pass_Details pass)
        {
            string VAL = "";
           // int m = 0;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    if (pass.record_status == "I")
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("Select case when max(pass_issueno) is null then 0 else max(pass_issueno) end as pass_issueno from exciseautomation.pass where supplier_unit='" + pass.party_code + "' and financial_year='"+pass.financial_year+"'", cn);
                        int m1 = Convert.ToInt32(cmd1.ExecuteScalar()) + 1;
                        StringBuilder str = new StringBuilder();
                        str.Append("update exciseautomation.pass set pass_issueno='" + m1 + "',pass_issuedate='" + DateTime.Now.ToString("dd-MM-yyyy") + "',carrier='" + pass.carrier + "',vehicle_no='" + pass.vehicle_no + "',vehicle_type='" + pass.vehicle_type + "',driver='" + pass.driver + "',challan_no='" + pass.challan_no + "',digital_lock_no='" + pass.digital_lock_no + "',dispatch_date='" + pass.dispatch_date + "',dispatch_time='" + pass.dispatch_time + "',dispatch_duration='" + pass.dispatch_duration + "',route_details='" + pass.route_details + "',  record_status='" + pass.record_status + "',rem_pass_qty='" + pass.dispatch_qty + "' where pass_id='" + pass.pass_id + "' and pass_issueno is null and financial_year='" + pass.financial_year + "' ");
                        NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();
                        if (pass.pass_type == "RR" || pass.pass_type == "M")
                        {
                            cmd = new NpgsqlCommand("select case when rr_lifted_qty is null then 0 else rr_lifted_qty end as rr_lifted_qty from exciseautomation.release_request  WHERE release_request_id='" + pass.rrnoc_record_request_id + "' and party_code='" + pass.party_code + "' and financial_year='" + pass.financial_year + "'", cn);
                            double lifted = Convert.ToDouble(cmd.ExecuteScalar());
                            lifted = lifted + pass.dispatch_qty;
                            cmd = new NpgsqlCommand("UPDATE exciseautomation.release_request SET  rr_lifted_qty='" + lifted + "' WHERE release_request_id='" + pass.rrnoc_record_request_id + "' and party_code='" + pass.party_code + "' and financial_year='" + pass.financial_year + "'", cn);
                            cmd.ExecuteNonQuery();
                            str = new StringBuilder();
                            str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                            str.Append("'" + pass.pass_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy") + "','Pass','" + pass.approver_remarks + " ','Issued','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + pass.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + pass.user_id + "','"+pass.financial_year+"','"+pass.party_code+"')");
                            cmd = new NpgsqlCommand(str.ToString(), cn);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd = new NpgsqlCommand("select case when liftedqty is null then 0 else liftedqty end as liftedqty from exciseautomation.noc_depotdetail  WHERE noc_id='" + pass.rrnoc_record_request_id + "' and noc_depotdetail_id='" + pass.noc_depotdetail_id + "' and financial_year='" + pass.financial_year + "'", cn);
                            double lifted = Convert.ToDouble(cmd.ExecuteScalar());
                            lifted = lifted + pass.dispatch_qty;
                            cmd = new NpgsqlCommand("UPDATE exciseautomation.noc_depotdetail SET  liftedqty='" + lifted + "' WHERE noc_id='" + pass.rrnoc_record_request_id + "' and noc_depotdetail_id='" + pass.noc_depotdetail_id + "' and financial_year='" + pass.financial_year + "'", cn);
                            cmd.ExecuteNonQuery();
                            str = new StringBuilder();
                            str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                            str.Append("'" + pass.pass_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy") + "','Pass','Issued','" + pass.approver_remarks + " ','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + pass.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + pass.user_id + "','"+pass.financial_year+"','"+pass.party_code+"')");
                            cmd = new NpgsqlCommand(str.ToString(), cn);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    if(pass.record_status == "R")
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("Select case when max(pass_issueno) is null then 0 else max(pass_issueno) end as pass_issueno from exciseautomation.pass where supplier_unit='" + pass.party_code + "' and financial_year='" + pass.financial_year + "'", cn);
                        int m1 = Convert.ToInt32(cmd1.ExecuteScalar()) + 1;
                        StringBuilder str = new StringBuilder();
                        str.Append("update exciseautomation.pass set record_status='" + pass.record_status + "'  where pass_id='" + pass.pass_id + "' and pass_issueno is null and financial_year='" + pass.financial_year + "' ");
                        NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();
                        NpgsqlCommand cmd2 = new NpgsqlCommand("select case when sum(lifted_qty) is null then 0 else sum(lifted_qty) end from exciseautomation.request_for_pass  where request_for_pass_id='" + pass.request_for_pass_id + "' and party_code='" + pass.party_code + "' and financial_year='" + pass.financial_year + "'", cn);
                        double lift = Convert.ToDouble(cmd2.ExecuteScalar()) - pass.dispatch_qty;
                        cmd2 = new NpgsqlCommand("update exciseautomation.request_for_pass set lifted_qty='" + lift + "' where request_for_pass_id='" + pass.request_for_pass_id + "' and party_code='" + pass.party_code + "' and financial_year='" + pass.financial_year + "'", cn);
                        cmd2.ExecuteNonQuery();
                        str = new StringBuilder();
                        str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                        str.Append("'" + pass.pass_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy") + "','Pass','Rejected','" + pass.approver_remarks + " ','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + pass.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + pass.user_id + "','"+pass.financial_year+"','"+pass.party_code+"')");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        cmd.ExecuteNonQuery();
                    }
                    VAL = "0";
                    trn.Commit();
                    cn.Close();
                    _log.Info("Pass Issued Sucess:" + pass.party_code);

                }
                catch (Exception ex1)
                {
                    _log.Info("Pass Issued Fail:" + pass.party_code + " - " + ex1.Message);
                    VAL = ex1.Message;
                    trn.Rollback();
                }
                return VAL;
            }
        }

        public static string Update(Pass_Details pass)
        {
            string VAL = "";
            int m = 0;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    StringBuilder str = new StringBuilder();
                    if (pass.pass_type == "N" || pass.pass_type == "NOC")
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand("update exciseautomation.pass set  dispatch_type_id='" + pass.dispatch_type_id + "', dispatch_qty='" + pass.dispatch_qty + "',  to_dispatch_vat='" + pass.to_dispatch_vat + "',noc_depotdetail_id='" + pass.noc_depotdetail_id+"', taxinvoice='" + pass.taxinvoice + "', remarks='" + pass.remarks + "',carrier='" + pass.carrier + "',vehicle_no='" + pass.vehicle_no + "',vehicle_type='" + pass.vehicle_type + "',driver='" + pass.driver + "',challan_no='" + pass.challan_no + "',digital_lock_no='" + pass.digital_lock_no + "',dispatch_date='" + pass.dispatch_date + "',dispatch_time='" + pass.dispatch_time + "',dispatch_duration='" + pass.dispatch_duration + "',route_details='" + pass.route_details + "',record_status='" + pass.record_status + "',lastmodified_date='"+DateTime.Now.ToString("dd-MM-yyyy")+"',user_id='"+pass.user_id+"' where pass_id='" + pass.pass_id + "' and financial_year='" + pass.financial_year + "'", cn);
                        int n = cmd.ExecuteNonQuery();
                    }

                    else
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand("update exciseautomation.pass set dispatch_type_id='" + pass.dispatch_type_id + "', dispatch_qty='" + pass.dispatch_qty + "',to_dispatch_vat='" + pass.to_dispatch_vat + "',prev_prod_year='" + pass.prev_prod_year + "', brix='" + pass.brix + "', sugar_content='" + pass.sugar_content + "', taxinvoice='" + pass.taxinvoice + "', remarks='" + pass.remarks + "',carrier='" + pass.carrier + "',vehicle_no='" + pass.vehicle_no + "',vehicle_type='" + pass.vehicle_type + "',driver='" + pass.driver + "',challan_no='" + pass.challan_no + "',digital_lock_no='" + pass.digital_lock_no + "',dispatch_date='" + pass.dispatch_date + "',dispatch_time='" + pass.dispatch_time + "',dispatch_duration='" + pass.dispatch_duration + "',route_details='" + pass.route_details + "',record_status='" + pass.record_status + "',lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "',user_id='" + pass.user_id + "' where pass_id='" + pass.pass_id + "' and financial_year='" + pass.financial_year + "'", cn);
                        int n = cmd.ExecuteNonQuery();
                    }
                   
                   
                    if (pass.record_status == "Y")
                    {
                        NpgsqlCommand cmd2 = new NpgsqlCommand("select case when sum(lifted_qty) is null then 0 else sum(lifted_qty) end from exciseautomation.request_for_pass  where request_for_pass_id='" + pass.request_for_pass_id + "' and party_code='" + pass.party_code + "' and financial_year='" + pass.financial_year + "'", cn);
                        double lift = Convert.ToDouble(cmd2.ExecuteScalar()) + pass.dispatch_qty;
                        cmd2 = new NpgsqlCommand("update exciseautomation.request_for_pass set lifted_qty='" + lift + "' where request_for_pass_id='" + pass.request_for_pass_id + "' and party_code='" + pass.party_code + "' and financial_year='" + pass.financial_year + "'", cn);
                        cmd2.ExecuteNonQuery();
                    }
                    VAL = "0";
                    trn.Commit();
                    cn.Close();
                    _log.Info("Pass Insertion Sucess:" + m + '-' + pass.party_code);

                }
                catch (Exception ex1)
                {
                    _log.Info("Pass Insertion Fail:" + m + '-' + pass.party_code + " - " + ex1.Message);
                    VAL = ex1.Message;
                    trn.Rollback();
                }
                return VAL;
            }
        }

        public static List<Pass_Details> GetPassList()
        {
            List<Pass_Details> pass = new List<Pass_Details>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                  
                    using (NpgsqlCommand cmd = new NpgsqlCommand("Select distinct(a.financial_year),a.*,b.party_name,c.cust_name,d.pass,d.noc,case when a.pass_type like 'NOC' then f.issue_nocno else e.rr_issueno end as RR_NOC_IssuedNo,d1.release_request as release_request1  from exciseautomation.pass a left join exciseautomation.party_master b on a.supplier_unit=b.party_code left join exciseautomation.customer_master c on a.customer_id=c.customer_id and a.party_code=c.party_code left join exciseautomation.document_format_master d on d.party_code=a.supplier_unit left join exciseautomation.document_format_master d1 on d1.party_code=a.party_code left join exciseautomation.Release_request e on e.release_request_id=a.rrnoc_record_request_id and a.financial_year=e.financial_year  left join exciseautomation.NOC f on a.rrnoc_record_request_id=f.noc_id and a.financial_year=f.financial_year and a.party_code=f.party_code order by a.pass_reqno,a.pass_issueno", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        
                       // DataTable dt = new DataTable();
                      //  dt.Load(dr);

                        if (dr.HasRows)
                        {
                            pass = new List<Pass_Details>();
                            while (dr.Read())
                            {
                                Pass_Details record = new Pass_Details();
                                record.pass_id = dr["pass_id"].ToString();
                                record.pass_for = dr["pass_for"].ToString();
                                record.party_code = dr["party_code"].ToString();
                                record.from_party = dr["supplier_unit"].ToString();
                                record.financial_year = dr["financial_year"].ToString();
                                record.request_for_pass_id = dr["request_for_pass_id"].ToString();
                                record.rrnoc_record_request_id = dr["rrnoc_record_request_id"].ToString();
                                if (dr["dispatch_date"].ToString() != "")
                                    record.dispatch_date = dr["dispatch_date"].ToString().Substring(0, 10).Replace("/", "-");
                                if (dr["pass_type"].ToString()=="RR")
                                record.supplier_unit = dr["party_name"].ToString();
                                else
                                    record.supplier_unit = dr["cust_name"].ToString();
                                record.dispatch_qty =Convert.ToDouble( dr["dispatch_qty"].ToString());
                                record.brix = dr["Brix"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.pass_type = dr["pass_type"].ToString();
                                if (dr["pass_type"].ToString() == "RR")
                                    record.rr_noc_issuedno = dr["release_request1"].ToString() + dr["financial_year"].ToString() + "/" + dr["RR_NOC_IssuedNo"].ToString();
                                else
                                    record.rr_noc_issuedno = dr["NOC"].ToString() + dr["financial_year"].ToString() + "/" + dr["RR_NOC_IssuedNo"].ToString();
                              
                                if (dr["pass_reqno"].ToString()!="")
                                record.pass_reqno =Convert.ToInt32( dr["pass_reqno"].ToString());
                                if (dr["rem_pass_qty"].ToString() != "")
                                    record.rem_pass_qty = Convert.ToDouble(dr["rem_pass_qty"].ToString());
                                else
                                    record.rem_pass_qty =0;
                                if (dr["pass_issueno"].ToString() != "")
                                    record.pass_issuedno = dr["pass"] + dr["financial_year"].ToString() + "/" + dr["pass_issueno"].ToString();
                               
                                pass.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Pass List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Pass List Fail :" + ex.Message);
                }

            }
            return pass;
        }

        public static List<Pass_Details> Search(string tablename, string column, string value)
        {
            List<Pass_Details> mir = new List<Pass_Details>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd1;
                    if(column== "rr_noc_issuedno")
                    {
                        if(tablename=="NOC")
                        {
                            column = "f.issue_nocno";
                        }
                        else
                        {
                            column = "e.rr_issueno";
                        }
                    }
                    if(column=="b.party_name")
                    {
                        if (tablename == "NOC")
                        {
                            column = "c.cust_name";
                        }
                        else
                        {
                            column = "b.party_name";
                        }
                    }
                    using (NpgsqlCommand cmd = new NpgsqlCommand("Select distinct(a.financial_year),a.*,b.party_name,c.cust_name,d.pass,d.noc,case when a.pass_type like 'NOC' then f.issue_nocno else e.rr_issueno end as RR_NOC_IssuedNo,d1.release_request as release_request1  from exciseautomation.pass a left join exciseautomation.party_master b on a.supplier_unit=b.party_code left join exciseautomation.customer_master c on a.customer_id=c.customer_id and a.party_code=c.party_code left join exciseautomation.document_format_master d on d.party_code=a.supplier_unit left join exciseautomation.document_format_master d1 on d1.party_code=a.party_code left join exciseautomation.Release_request e on e.release_request_id=a.rrnoc_record_request_id and a.financial_year=e.financial_year  left join exciseautomation.NOC f on a.rrnoc_record_request_id=f.noc_id and a.financial_year=f.financial_year and a.party_code=f.party_code   where " + column + " Ilike '%" + value + "%'  order by a.pass_reqno,a.pass_issueno,a.dispatch_date", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            mir = new List<Pass_Details>();
                            while (dr.Read())
                            {
                                Pass_Details record = new Pass_Details();
                                record.pass_id = dr["pass_id"].ToString();
                                record.pass_for = dr["pass_for"].ToString();
                                record.party_code = dr["party_code"].ToString();
                                record.from_party = dr["supplier_unit"].ToString();
                                record.financial_year = dr["financial_year"].ToString();
                                record.request_for_pass_id = dr["request_for_pass_id"].ToString();
                                record.rrnoc_record_request_id = dr["rrnoc_record_request_id"].ToString();
                                if (dr["dispatch_date"].ToString() != "")
                                    record.dispatch_date = dr["dispatch_date"].ToString().Substring(0, 10).Replace("/", "-");
                                if (dr["pass_type"].ToString() == "RR")
                                    record.supplier_unit = dr["party_name"].ToString();
                                else
                                    record.supplier_unit = dr["cust_name"].ToString();
                                record.dispatch_qty = Convert.ToDouble(dr["dispatch_qty"].ToString());
                                record.brix = dr["Brix"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.pass_type = dr["pass_type"].ToString();
                                if (dr["pass_type"].ToString() == "RR")
                                    record.rr_noc_issuedno = dr["release_request1"].ToString() + dr["financial_year"].ToString() + "/" + dr["RR_NOC_IssuedNo"].ToString();
                                else
                                    record.rr_noc_issuedno = dr["NOC"].ToString() + dr["financial_year"].ToString() + "/" + dr["RR_NOC_IssuedNo"].ToString();

                                if (dr["pass_reqno"].ToString() != "")
                                    record.pass_reqno = Convert.ToInt32(dr["pass_reqno"].ToString());
                                if (dr["rem_pass_qty"].ToString() != "")
                                    record.rem_pass_qty = Convert.ToDouble(dr["rem_pass_qty"].ToString());
                                else
                                    record.rem_pass_qty = 0;
                                if (dr["pass_issueno"].ToString() != "")
                                    record.pass_issuedno = dr["pass"] + dr["financial_year"].ToString() + "/" + dr["pass_issueno"].ToString();

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


        public static Pass_Details GetDetails(string pass_id,string financial_year)
        {
            Pass_Details pass = new Pass_Details();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("Select a.*,d1.pass from exciseautomation.pass a inner join exciseautomation.document_format_master d1 on d1.party_code=a.party_code where a.pass_id='" + pass_id+"' and a.financial_year='"+financial_year+"'", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                           
                            while (dr.Read())
                            {
                               
                                pass.pass_id = dr["pass_id"].ToString();
                                pass.pass_for = dr["pass_for"].ToString();
                                pass.rrnoc_record_request_id = dr["rrnoc_record_request_id"].ToString();
                              pass.financial_year = dr["financial_year"].ToString();
                                pass.dispatch_qty = Convert.ToDouble(dr["dispatch_qty"].ToString());
                                pass.brix = dr["Brix"].ToString();
                                pass.dispatch_type_id = dr["dispatch_type_id"].ToString();
                                pass.dispatch_qty = Convert.ToDouble(dr["dispatch_qty"].ToString());
                                if (dr["pass_issueno"].ToString() != "")
                                    pass.pass_issuedno = dr["pass"] + dr["financial_year"].ToString() + "/" + dr["pass_issueno"].ToString();
                                pass.taxinvoice = dr["taxinvoice"].ToString();
                                pass.to_dispatch_vat = dr["to_dispatch_vat"].ToString();
                                pass.sugar_content = dr["sugar_content"].ToString();
                                pass.record_status = dr["record_status"].ToString();
                                pass.pass_type = dr["pass_type"].ToString();
                                pass.remarks = dr["remarks"].ToString();
                                pass.prev_prod_year = dr["prev_prod_year"].ToString();
                                pass.carrier = dr["carrier"].ToString();
                                pass.vehicle_no = dr["vehicle_no"].ToString();
                                pass.vehicle_type = dr["vehicle_type"].ToString();
                                pass.driver = dr["driver"].ToString();
                                pass.challan_no = dr["challan_no"].ToString();
                                pass.digital_lock_no = dr["digital_lock_no"].ToString();
                                if(dr["dispatch_date"].ToString()!="")
                                pass.dispatch_date = dr["dispatch_date"].ToString().Substring(0,10).Replace("/","-");
                                pass.dispatch_time = dr["dispatch_time"].ToString();
                                pass.dispatch_duration = dr["dispatch_duration"].ToString();
                                pass.route_details = dr["route_details"].ToString();

                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Pass List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Pass List Fail :" + ex.Message);
                }

            }
            return pass;
        }

      
    }
}
