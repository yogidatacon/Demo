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
 public  class DL_Molasses_Allocation
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static string Insert(Molasses_Allocation allotment)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT  case when max(molasses_allotment_request_id) is null then 0 else max(molasses_allotment_request_id) end as molasses_allotment_request_id FROM exciseautomation.molasses_allotment_request where financial_year='" + allotment.financial_year + "'", cn);
                int m = Convert.ToInt32(cmd1.ExecuteScalar());
                m = m + 1;
                try
                {
                    allotment.req_allotmentno = m.ToString();
                    StringBuilder str = new StringBuilder();
                    str.Append("INSERT INTO exciseautomation.molasses_allotment_request(molasses_allotment_request_id, req_allotmentno, req_allotmentdate, financial_year, party_code, requested_fromunit,");
                    str.Append(" iscaptive,prov_indent_qty, qty_allotted_till_date, reqd_qty, product_code, remarks, creation_date,  user_id, record_status,approver_level,allotment_status)Values(");
                    str.Append("'"+m+"','"+allotment.req_allotmentno+"','"+allotment.req_allotmentdate+"','"+allotment.financial_year+"','"+allotment.party_code+"','"+allotment.requested_fromunit+"',");
                    str.Append("'"+allotment.iscaptive+"','"+allotment.prov_indent_qty+"','"+allotment.qty_allotted_till_date+"','"+allotment.reqd_qty+"','"+allotment.product_code+"','"+allotment.remarks+"','"+DateTime.Now.ToString("dd-MM-yyyy")+"','"+allotment.user_id+"','"+allotment.record_status+"','0','N')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    for (int i1 = 0; i1 < allotment.docs.Count; i1++)
                    {

                        str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.eascm_docs(doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,financial_year,party_code)");
                        str.Append("Values('" + m + "','" + allotment.docs[i1].doc_name + "', '" + allotment.docs[i1].description + "','" + allotment.docs[i1].doc_path + "','ALT','" + allotment.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+allotment.financial_year+"','"+allotment.party_code+"')");
                        NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                        int r = cmd3.ExecuteNonQuery();
                    }
                    VAL = "0";
                    trn.Commit();
                    cn.Close();
                    _log.Info("Insert Allotment Success :" + allotment.molasses_allotment_request_id + "-" + allotment.party_code);
                }
                catch (Exception ex1)
                {
                    _log.Info("Insert Allotment Success :" + allotment.molasses_allotment_request_id + "-" + allotment.party_code + "-" + ex1.Message);
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
                    using (NpgsqlCommand cmd = new NpgsqlCommand("Select count(1) from exciseautomation.molasses_allotment_request where party_code='" + party[0] + "' and product_code='"+party[1]+"' and financial_year='" + party[2] + "' ", cn))
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
        public static Molasses_Allocation GetMNTConsRegDetails(string id, string financial_year)
        {
            Molasses_Allocation allot = new Molasses_Allocation();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("Select * from exciseautomation.consumption_register where consumption_no='" + id + "' and financial_year='"+financial_year+"' ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr1 = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr1);
                        dr1.Close();
                        foreach (DataRow dr in dt.Rows)
                        {
                            allot.application_requestno = dr["consumption_reqno"].ToString();
                            allot.record_status = dr["record_status"].ToString();
                            allot.consumption_date = dr["consumption_date"].ToString();
                            allot.consumption_no = Convert.ToInt32(dr["consumption_no"].ToString());
                            allot.product_code = dr["product_code"].ToString();
                            allot.remarks = dr["remarks"].ToString();
                            allot.qty_allotted_till_date = Convert.ToDouble(dr["total_consumption_bl_qty"].ToString());
                            allot.prov_indent_qty = Convert.ToDouble(dr["total_wastage"].ToString());
                            allot.reqd_qty = Convert.ToDouble(dr["total_consumption_lp_qty"].ToString());
                            allot.docs = new List<EASCM_DOCS>();
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.consumption_register_item where consumption_register_id='" + id + "' and financial_year='" + financial_year + "' order by consumption_register_id", cn))
                            {
                                cmd1.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr2 = cmd1.ExecuteReader();

                                if (dr2.HasRows)
                                {

                                    while (dr2.Read())
                                    {
                                        EASCM_DOCS doc = new EASCM_DOCS();

                                        doc.issue_vat = dr2["issue_vat"].ToString();
                                        doc.consumption_qty = dr2["consumption_qty"].ToString();
                                        doc.strength = dr2["strength"].ToString();
                                        doc.issue_qty = dr2["medicine_qty"].ToString();
                                        allot.docs.Add(doc);
                                    }

                                }

                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Allotment Details Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Allotment Details Success :" + ex.Message);
                }

            }
            return allot;
        }



        public static int GetDigitalsignature(string userid)
        {
            int value1 = 0;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;

                    cmd = new NpgsqlCommand("select a.user_registration_id  from exciseautomation.user_registration a inner join exciseautomation.digital_signature b on a.user_registration_id=b.dongle_userid where a.user_id='"+userid+"'", cn);
                  string re = cmd.ExecuteScalar().ToString();
                    if (re != "0")
                    {
                        value1 = 1;

                    }
                    //else
                    //{
                    //    if (re != "")
                    //        value1 = Convert.ToInt32(re);

                    //}

                }
                catch (Exception ex)
                {

                    value1 = 0;
                }
            }
            return value1;
        }

        public static Molasses_Allocation GetQTY(string partycode,string vat)
        {
         Molasses_Allocation production = new Molasses_Allocation();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    string[] val = partycode.Split('_');
                    NpgsqlCommand cmd = new NpgsqlCommand("select case when sum(a.allotted_qty) is null then 0 else sum(a.allotted_qty) end as quantity from  exciseautomation.molasses_allotment_request a  where a.party_code='"+partycode+"' and a.product_code='"+vat+ "' and a.record_status='I'", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            production.qty_allotted_till_date = Convert.ToDouble(dr["quantity"].ToString());// + Convert.ToDouble(dr["openingbalancevalue"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return production;
            }
        }
        public static string adminupdate(Molasses_Allocation allotment)
        {
            string value;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    
                        NpgsqlCommand cmd1 = new NpgsqlCommand("UPDATE exciseautomation.molasses_allotment_request SET  allotment_validdate='" + allotment.allotment_validdate + "' WHERE molasses_allotment_request_id='" + allotment.molasses_allotment_request_id + "'  and financial_year='" +allotment.financial_year + "'", cn);
                        int n = cmd1.ExecuteNonQuery();
                  

                    StringBuilder str = new StringBuilder();
                    str = new StringBuilder();
                    str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year) VALUES(");
                    str.Append("'" + allotment.molasses_allotment_request_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "','ALT','Updated By Admin','Valid Date Upto Updated By Admin(From "+allotment.req_allotmentdate+" To "+allotment.allotment_validdate+")','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + allotment.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + allotment.user_id + "','"+allotment.financial_year+"')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    cmd.ExecuteNonQuery();
                    value = "0";
                    trn.Commit();
                    _log.Info("Allotement updated Sucess:" + allotment.molasses_allotment_request_id);
                    cn.Close();
                }
                catch (Exception ex)
                {
                    value = ex.Message;
                    _log.Info("Allotement updated Fail:" + allotment.molasses_allotment_request_id + "-" + value);
                    trn.Rollback();
                }
                return value;

            }
        }


        public static string Approve(Molasses_Allocation allotment)
        {
            string value;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    string[] lev = allotment.product_name.Split('_');
                    if(lev[1]=="2" && allotment.record_status == "Y")
                    {
                        allotment.allotment_status = "Approved by "+lev[2];
                        allotment.record_status = "A";

                        NpgsqlCommand cmd1 = new NpgsqlCommand("UPDATE exciseautomation.molasses_allotment_request SET  allotment_status='" + allotment.allotment_status + "',approver_level='" + lev[1] + "',record_status='" + allotment.record_status + "',allotted_qty='" + allotment.qty_allotted_till_date + "' WHERE financial_year='" + allotment.financial_year + "' and molasses_allotment_request_id='" + allotment.molasses_allotment_request_id + "'", cn);
                        int n = cmd1.ExecuteNonQuery();
                    }
                    else
                    {

                        if (allotment.record_status == "R")
                        {
                            allotment.allotment_status = "Rejected by " + lev[2];
                            NpgsqlCommand cmd2 = new NpgsqlCommand("UPDATE exciseautomation.molasses_allotment_request SET  allotment_status='" + allotment.allotment_status + "',approver_level='" + lev[1] + "',record_status='" + allotment.record_status + "',allotted_qty='" + allotment.qty_allotted_till_date + "' WHERE financial_year='" + allotment.financial_year + "' and molasses_allotment_request_id='" + allotment.molasses_allotment_request_id + "'", cn);
                            int n = cmd2.ExecuteNonQuery();
                        }
                        else if (allotment.record_status == "B")
                        {
                            allotment.allotment_status = "Refer Back by " + lev[2];
                          //  allotment.record_status = "Y";
                            NpgsqlCommand cmd2 = new NpgsqlCommand("UPDATE exciseautomation.molasses_allotment_request SET  allotment_status='" + allotment.allotment_status + "',approver_level='0',record_status='" + allotment.record_status + "' WHERE  financial_year='" + allotment.financial_year + "' and molasses_allotment_request_id='" + allotment.molasses_allotment_request_id + "'", cn);
                            int n = cmd2.ExecuteNonQuery();
                        }
                        else
                        {
                            allotment.allotment_status = "Recommended  by " + lev[2];
                            NpgsqlCommand cmd3 = new NpgsqlCommand("UPDATE exciseautomation.molasses_allotment_request SET  allotment_validdate='" + allotment.allotment_validdate + "',requested_fromunit='" + allotment.requested_fromunit + "',allotment_status='" + allotment.allotment_status + "',approver_level='" + lev[1] + "',record_status='" + allotment.record_status + "',allotted_qty='" + allotment.qty_allotted_till_date + "' WHERE financial_year='" + allotment.financial_year + "' and  molasses_allotment_request_id='" + allotment.molasses_allotment_request_id + "'", cn);
                            int n = cmd3.ExecuteNonQuery();
                        }
                    }
                  
                    StringBuilder str = new StringBuilder();
                    for (int i1 = 0; i1 < allotment.docs.Count; i1++)
                    {
                        str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.eascm_docs(doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,financial_year,party_code)");
                        str.Append("Values('" + allotment.molasses_allotment_request_id + "','" + allotment.docs[i1].doc_name + "', '" + allotment.docs[i1].description + "','" + allotment.docs[i1].doc_path + "','ALT','" + allotment.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+allotment.financial_year+"','"+allotment.party_code+"')");
                        NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                        int r = cmd3.ExecuteNonQuery();
                    }
                    str = new StringBuilder();
                    str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                    str.Append("'" + allotment.molasses_allotment_request_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "','ALT','" + allotment.allotment_status + "','" + allotment.remarks + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + allotment.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + allotment.user_id + "','"+allotment.financial_year+"','"+allotment.party_code+"')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    cmd.ExecuteNonQuery();
                    value = "0";
                    trn.Commit();
                    _log.Info("Allotement Approve Sucess:" + allotment.molasses_allotment_request_id );
                    cn.Close();
                }
                catch (Exception ex)
                {
                    value = ex.Message;
                    _log.Info("Allotement Approve Fail:" + allotment.molasses_allotment_request_id + "-" + value);
                    trn.Rollback();
                }
                return value;

            }
        }


        public static string ApproveMtpAllocation(Molasses_Allocation allotment)
        {
            string value;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try 
                {
                    string[] lev = allotment.product_name.Split('_');
                    if (lev[1] == "3" && allotment.record_status == "Y")
                    {
                        allotment.allotment_status = "Approved by " + lev[2];
                        allotment.record_status = "A";

                        NpgsqlCommand cmd1 = new NpgsqlCommand("UPDATE exciseautomation.molasses_allotment_request SET  allotment_status='" + allotment.allotment_status + "',approver_level='" + lev[1] + "',record_status='" + allotment.record_status + "',allotted_qty='" + allotment.qty_allotted_till_date + "' WHERE molasses_allotment_request_id='" + allotment.molasses_allotment_request_id + "' and  financial_year='" + allotment.financial_year + "'", cn);
                        int n = cmd1.ExecuteNonQuery();
                    }
                    else
                    {

                        if (allotment.record_status == "R")
                        {
                            allotment.allotment_status = "Rejected by " + lev[2];
                            NpgsqlCommand cmd2 = new NpgsqlCommand("UPDATE exciseautomation.molasses_allotment_request SET  allotment_status='" + allotment.allotment_status + "',approver_level='" + lev[1] + "',record_status='" + allotment.record_status + "',allotted_qty='" + allotment.qty_allotted_till_date + "' WHERE molasses_allotment_request_id='" + allotment.molasses_allotment_request_id + "' and  financial_year='" + allotment.financial_year + "'", cn);
                            int n = cmd2.ExecuteNonQuery();
                        }
                        else if (allotment.record_status == "B")
                        {
                            allotment.allotment_status = "Refer Back by " + lev[2];
                            //  allotment.record_status = "Y";
                            NpgsqlCommand cmd2 = new NpgsqlCommand("UPDATE exciseautomation.molasses_allotment_request SET  allotment_status='" + allotment.allotment_status + "',approver_level='0',record_status='" + allotment.record_status + "' WHERE molasses_allotment_request_id='" + allotment.molasses_allotment_request_id + "' and  financial_year='" + allotment.financial_year + "'", cn);
                            int n = cmd2.ExecuteNonQuery();
                        }
                        else
                        {
                            allotment.allotment_status = "Recommended  by " + lev[2];
                            NpgsqlCommand cmd3;
                            if(allotment.allotment_validdate !="")
                            {
                                cmd3 = new NpgsqlCommand("UPDATE exciseautomation.molasses_allotment_request SET  allotment_validdate='" + allotment.allotment_validdate + "',requested_fromunit='" + allotment.requested_fromunit + "',allotment_status='" + allotment.allotment_status + "',approver_level='" + lev[1] + "',record_status='" + allotment.record_status + "',allotted_qty='" + allotment.qty_allotted_till_date + "' WHERE molasses_allotment_request_id='" + allotment.molasses_allotment_request_id + "' and  financial_year='" + allotment.financial_year + "'", cn);
                            }
                            else
                            {
                                cmd3 = new NpgsqlCommand("UPDATE exciseautomation.molasses_allotment_request SET requested_fromunit='" + allotment.requested_fromunit + "',allotment_status='" + allotment.allotment_status + "',approver_level='" + lev[1] + "',record_status='" + allotment.record_status + "',allotted_qty='" + allotment.qty_allotted_till_date + "' WHERE molasses_allotment_request_id='" + allotment.molasses_allotment_request_id + "' and  financial_year='" + allotment.financial_year + "'", cn);
                            }
                        
                            int n = cmd3.ExecuteNonQuery();
                        }
                    }

                    StringBuilder str = new StringBuilder();
                    for (int i1 = 0; i1 < allotment.docs.Count; i1++)
                    {
                        str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.eascm_docs(doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,financial_year,party_code)");
                        str.Append("Values('" + allotment.molasses_allotment_request_id + "','" + allotment.docs[i1].doc_name + "', '" + allotment.docs[i1].description + "','" + allotment.docs[i1].doc_path + "','ALT','" + allotment.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+allotment.financial_year+"','"+allotment.party_code+"')");
                        NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                        int r = cmd3.ExecuteNonQuery();
                    }
                    str = new StringBuilder();
                    str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                    str.Append("'" + allotment.molasses_allotment_request_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "','MALT','" + allotment.allotment_status + "','" + allotment.remarks + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + allotment.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + allotment.user_id + "','"+allotment.financial_year+"','"+allotment.party_code+"')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    cmd.ExecuteNonQuery();
                    value = "0";
                    trn.Commit();
                    _log.Info("Allotement Approve Sucess:" + allotment.molasses_allotment_request_id);
                    cn.Close();
                }
                catch (Exception ex)
                {
                    value = ex.Message;
                    _log.Info("Allotement Approve Fail:" + allotment.molasses_allotment_request_id + "-" + value);
                    trn.Rollback();
                }
                return value;

            }
        }
        public static string Issued(string allotmentid,UserDetails user,string transaction_type,string financial_year)
        {
            string value;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select party_code,case when allotted_qty is null then 0 else allotted_qty end as allotted_qty ,case when qty_allotted_till_date is null then 0 else qty_allotted_till_date end as qty_allotted_till_date  from  exciseautomation.molasses_allotment_request  WHERE molasses_allotment_request_id='" + allotmentid + "' and financial_year='" + financial_year + "'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    //cmd.Parameters.AddWithValue("@UserID", userid);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    double total=0;
                    string party_code = "0";
                    while (dr.Read())
                    {
                        double alloted = Convert.ToDouble(dr["qty_allotted_till_date"].ToString());
                        double allotted_qty = Convert.ToDouble(dr["allotted_qty"].ToString());
                        party_code = dr["party_code"].ToString();
                        total = allotted_qty + alloted;
                    }
                    ///total = 15000;
                    dr.Close();
                    NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT  case when max(final_allotmentno) is null then 0 else max(final_allotmentno) end as final_allotmentno FROM exciseautomation.molasses_allotment_request where party_code='"+party_code+"' and financial_year='"+financial_year+"'", cn);
                    int final_allotmentno = Convert.ToInt32(cmd1.ExecuteScalar());
                    final_allotmentno = final_allotmentno + 1;
                    cmd1 = new NpgsqlCommand("UPDATE exciseautomation.molasses_allotment_request SET  allotment_status='Issued',Record_status='I',qty_allotted_till_date='"+total+ "',final_allotmentno='"+ final_allotmentno + "',final_allotmentdate='"+DateTime.Now.ToString("dd-MM-yyyy")+"' WHERE molasses_allotment_request_id='" + allotmentid + "' and financial_year='" + financial_year + "'", cn);
                    int n = cmd1.ExecuteNonQuery();
                    StringBuilder str = new StringBuilder();
                    str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                    str.Append("'" + allotmentid+ "','','" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "','"+transaction_type+"','Issued by "+user.role_name+"','Issued','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + user.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + user.user_id + "','"+financial_year+"','"+party_code+"')");
                    cmd = new NpgsqlCommand(str.ToString(), cn);
                    n = cmd.ExecuteNonQuery();
                    value = "0";
                    trn.Commit();
                    _log.Info("Allotement Issued Sucess:" + allotmentid);
                    cn.Close();
                }
                catch (Exception ex)
                {
                    value = ex.Message;
                    _log.Info("Allotement Issued Fail:" + allotmentid + "-" + value);
                    trn.Rollback();
                }
                return value;

            }
        }

        public static string Update(Molasses_Allocation allotment)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    //(,molasses_allotment_request_id
                    StringBuilder str = new StringBuilder();
                    str.Append("Update exciseautomation.molasses_allotment_request set req_allotmentno='"+allotment.molasses_allotment_request_id+ "', req_allotmentdate='" + allotment.req_allotmentdate+"', financial_year='"+allotment.financial_year+"', party_code='"+allotment.party_code+"', requested_fromunit='"+allotment.requested_fromunit+"',");
                    str.Append(" iscaptive='"+allotment.iscaptive+"',prov_indent_qty='"+allotment.prov_indent_qty+"', qty_allotted_till_date='"+allotment.qty_allotted_till_date+"', reqd_qty='"+allotment.reqd_qty+"', product_code='"+allotment.product_code+"', remarks='"+allotment.remarks+ "', lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "',  user_id='"+allotment.user_id+"', record_status='"+allotment.record_status+ "',allotment_status='N',approver_level='0'  where financial_year='"+allotment.financial_year+"' and molasses_allotment_request_id='" + allotment.molasses_allotment_request_id+"'");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("delete from exciseautomation.eascm_docs where doc_id='" + allotment.molasses_allotment_request_id + "' and doc_type_code='ALT' and  financial_year='" + allotment.financial_year + "'", cn);
                        cmd1.ExecuteNonQuery();
                        for (int i = 0; i < allotment.docs.Count; i++)
                        {
                            n = 0;
                            str = new StringBuilder();

                            str.Append("INSERT INTO exciseautomation.eascm_docs(doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,financial_year,party_code)");
                            str.Append("Values('" + allotment.molasses_allotment_request_id + "','" + allotment.docs[i].doc_name + "', '" + allotment.docs[i].description + "','" + allotment.docs[i].doc_path + "','ALT','" + allotment.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+allotment.financial_year+"','"+allotment.party_code+"')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd3.ExecuteNonQuery();
                        }
                        VAL = "0";
                    }
                    else
                        VAL = "1";
                    cn.Close();
                    _log.Info("Update Allotment Success :" + allotment.molasses_allotment_request_id + "-" + allotment.party_code);
                }
                catch (Exception ex1)
                {
                    _log.Info("Update Allotment Success :" + allotment.molasses_allotment_request_id + "-" + allotment.party_code + "-" + ex1.Message);
                    VAL = ex1.Message;
                }
            }

            return VAL;
        }

        public static List<Molasses_Allocation> GetList()
        {
            List<Molasses_Allocation> allots = new List<Molasses_Allocation>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("Select a.*,b.Product_name,c.party_name as requested_fromunit1,d.party_name ,e.molasses_allotment,c.district_code from exciseautomation.molasses_allotment_request a inner join exciseautomation.Product_master b on a.product_code=b.product_code left join exciseautomation.party_master c on a.requested_fromunit=c.party_code left join exciseautomation.party_master d on a.party_code=d.party_code left join exciseautomation.document_format_master e on a.party_code=e.party_code where   d.party_type_code ='SGR' or d.party_type_code ='DIS' order by a.party_code,a.molasses_allotment_request_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            allots = new List<Molasses_Allocation>();
                            while (dr.Read())
                            {
                                Molasses_Allocation record = new Molasses_Allocation();
                                record.financial_year = dr["financial_year"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.req_allotmentno = dr["req_allotmentno"].ToString();
                                record.financial_year= dr["financial_year"].ToString();
                                if (dr["final_allotmentno"].ToString()!="")
                                record.final_allotmentno =dr["molasses_allotment"]+ dr["financial_year"].ToString() +"/"+ dr["final_allotmentno"].ToString();
                                record.req_allotmentdate = dr["req_allotmentdate"].ToString().Substring(0, 10).Replace("/", "-");
                                record.iscaptive = dr["iscaptive"].ToString();
                                record.requested_fromunit = dr["requested_fromunit1"].ToString();
                                record.party_name = dr["party_name"].ToString();
                                record.prov_indent_qty = Convert.ToDouble(dr["prov_indent_qty"].ToString());
                                record.reqd_qty= Convert.ToDouble(dr["reqd_qty"].ToString());
                                record.molasses_allotment_request_id = dr["molasses_allotment_request_id"].ToString();
                                record.product_code = dr["product_code"].ToString();
                                record.product_name = dr["product_name"].ToString();
                                record.allotment_status = dr["allotment_status"].ToString();
                                record.party_code = dr["party_code"].ToString();
                                record.district_code = dr["district_code"].ToString();
                                if (dr["allotted_qty"].ToString() == "")
                                    record.approved_qty = 0;
                                else
                                    record.approved_qty = Convert.ToDouble(dr["allotted_qty"].ToString());
                                allots.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Allotment List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Allotment List Success :" + ex.Message);
                }

            }
            return allots;
        }

        public static List<Molasses_Allocation> Search(string tablename, string column, string value)
        {
            List<Molasses_Allocation> mir = new List<Molasses_Allocation>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                
                    NpgsqlCommand cmd;
                    if (column == "financial_year")
                    {
                        column = "a.financial_year";
                    }
                    else if(column== "party_name")
                    {
                        column = "d.party_name";
                    }
                    else if(column== "requested_fromunit")
                    {
                        column = "c.party_name";
                    }
                    cmd = new NpgsqlCommand("Select a.*,b.Product_name,c.party_name as requested_fromunit1,d.party_name ,e.molasses_allotment,c.district_code from exciseautomation.molasses_allotment_request a inner join exciseautomation.Product_master b on a.product_code=b.product_code left join exciseautomation.party_master c on a.requested_fromunit=c.party_code left join exciseautomation.party_master d on a.party_code=d.party_code left join exciseautomation.document_format_master e on a.party_code=e.party_code where d.party_type_code !='MTP' and " + column + " Ilike '%" + value + "%' and a.record_active='true' order by a.party_code,a.molasses_allotment_request_id", cn);
                  

                    NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            mir = new List<Molasses_Allocation>();
                            while (dr.Read())
                            {
                                Molasses_Allocation record = new Molasses_Allocation();
                                record.financial_year = dr["financial_year"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.req_allotmentno = dr["req_allotmentno"].ToString();
                                if (dr["final_allotmentno"].ToString() != "")
                                    record.final_allotmentno = dr["molasses_allotment"] + dr["financial_year"].ToString() + "/" + dr["final_allotmentno"].ToString();
                                record.req_allotmentdate = dr["req_allotmentdate"].ToString().Substring(0, 10).Replace("/", "-");
                                record.iscaptive = dr["iscaptive"].ToString();
                                record.requested_fromunit = dr["requested_fromunit1"].ToString();
                                record.party_name = dr["party_name"].ToString();
                                record.prov_indent_qty = Convert.ToDouble(dr["prov_indent_qty"].ToString());
                                record.reqd_qty = Convert.ToDouble(dr["reqd_qty"].ToString());
                                record.molasses_allotment_request_id = dr["molasses_allotment_request_id"].ToString();
                                record.product_code = dr["product_code"].ToString();
                                record.product_name = dr["product_name"].ToString();
                                record.allotment_status = dr["allotment_status"].ToString();
                                record.party_code = dr["party_code"].ToString();
                                record.district_code = dr["district_code"].ToString();
                                if (dr["allotted_qty"].ToString() == "")
                                    record.approved_qty = 0;
                                else
                                    record.approved_qty = Convert.ToDouble(dr["allotted_qty"].ToString());
                                mir.Add(record);

                            }
                        }
                  

                }
                catch (Exception ex)
                {
                    throw (ex);
                }

            }
            return mir;
        }


        public static List<Molasses_Allocation> GetMTPList()
        {
            List<Molasses_Allocation> allots = new List<Molasses_Allocation>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("Select a.*,b.Product_name,c.party_name as requested_fromunit1,d.party_name ,e.molasses_allotment,d.district_code from exciseautomation.molasses_allotment_request a inner join exciseautomation.Product_master b on a.product_code=b.product_code left join exciseautomation.party_master c on a.requested_fromunit=c.party_code left join exciseautomation.party_master d on a.party_code=d.party_code left join exciseautomation.document_format_master e on a.party_code=e.party_code where a.record_active='true' and d.party_type_code='MTP' order by a.party_code,a.molasses_allotment_request_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            allots = new List<Molasses_Allocation>();
                            while (dr.Read())
                            {
                                Molasses_Allocation record = new Molasses_Allocation();
                                record.financial_year = dr["financial_year"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.req_allotmentno = dr["req_allotmentno"].ToString();
                                if (dr["final_allotmentno"].ToString() != "")
                                    record.final_allotmentno = dr["molasses_allotment"] + dr["financial_year"].ToString() + "/" + dr["final_allotmentno"].ToString();
                                record.req_allotmentdate = dr["req_allotmentdate"].ToString().Substring(0, 10).Replace("/", "-");
                                record.iscaptive = dr["iscaptive"].ToString();
                                record.requested_fromunit = dr["requested_fromunit1"].ToString();
                                record.party_name = dr["party_name"].ToString();
                                record.prov_indent_qty = Convert.ToDouble(dr["prov_indent_qty"].ToString());
                                record.reqd_qty = Convert.ToDouble(dr["reqd_qty"].ToString());
                                record.molasses_allotment_request_id = dr["molasses_allotment_request_id"].ToString();
                                record.product_code = dr["product_code"].ToString();
                                record.product_name = dr["product_name"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.approverlevel =Convert.ToInt32( dr["approver_level"].ToString());
                                record.allotment_status = dr["allotment_status"].ToString();
                                record.party_code = dr["party_code"].ToString();
                                record.district_code = dr["district_code"].ToString();
                                if (dr["allotted_qty"].ToString() == "")
                                    record.approved_qty = 0;
                                else
                                    record.approved_qty = Convert.ToDouble(dr["allotted_qty"].ToString());
                                allots.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Allotment List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Allotment List Success :" + ex.Message);
                }

            }
            return allots;
        }
        public static Molasses_Allocation GetMNTIssueRegDetails(string id,string financial_year)
        {
            Molasses_Allocation allot = new Molasses_Allocation();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("Select * from exciseautomation.issue_register where issue_no='" + id + "' and financial_year='"+financial_year+"' ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr1 = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr1);
                        dr1.Close();
                        foreach (DataRow dr in dt.Rows)
                        {
                            allot.application_requestno = dr["issue_reqno"].ToString();
                            allot.record_status = dr["record_status"].ToString();
                            allot.issue_date = dr["issue_date"].ToString();
                            allot.issue_no = Convert.ToInt32(dr["issue_no"].ToString());
                            allot.product_code = dr["product_code"].ToString();
                            allot.remarks = dr["remarks"].ToString();
                            allot.docs = new List<EASCM_DOCS>();
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.issue_register_item where issue_register_id='" + id + "' and financial_year='" + financial_year + "' order by issue_register_id", cn))
                            {
                                cmd1.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr2 = cmd1.ExecuteReader();

                                if (dr2.HasRows)
                                {

                                    while (dr2.Read())
                                    {
                                        EASCM_DOCS doc = new EASCM_DOCS();
                                        doc.storage_vat = dr2["storage_vat"].ToString();
                                        doc.issue_vat = dr2["issue_vat"].ToString();
                                        doc.issue_qty = dr2["issue_qty"].ToString();
                                        doc.strength = dr2["strength"].ToString();

                                        allot.docs.Add(doc);
                                    }

                                }

                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Allotment Details Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Allotment Details Success :" + ex.Message);
                }

            }
            return allot;
        }


        public static string Insert_MTP_Con(Molasses_Allocation allotment)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT  case when max(consumption_register_id) is null then 0 else max(consumption_register_id) end as consumption_register_id FROM exciseautomation.consumption_register where  financial_year='"+allotment.financial_year+"'", cn);
                int m = Convert.ToInt32(cmd1.ExecuteScalar());
                m = m + 1;
                try
                {
                    allotment.req_allotmentno = m.ToString();
                    StringBuilder str = new StringBuilder();
                    str.Append("INSERT INTO exciseautomation.consumption_register(consumption_register_id,consumption_no, consumption_date, product_code,remarks,record_status,consumption_reqno,party_code,total_consumption_bl_qty,total_wastage,total_consumption_lp_qty,user_id,creation_date,lastmodified_date,financial_year) Values(");

                    str.Append("'" + m + "','" + m + "','" + allotment.consumption_date + "','" + allotment.product_code + "','" + allotment.remarks + "','" + allotment.record_status + "','" + allotment.application_requestno + "','" + allotment.party_code + "','" + allotment.qty_allotted_till_date + "','" + allotment.prov_indent_qty + "','" + allotment.reqd_qty + "','" + allotment.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+allotment.financial_year+"')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    for (int i1 = 0; i1 < allotment.docs.Count; i1++)
                    {

                        str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.consumption_register_item(consumption_register_id, issue_vat,medicine_name, batch_no, consumption_bl_qty, strength,medicine_qty,consumption_lp_qty,user_id,creation_date,lastmodified_date,financial_year)");
                        str.Append("Values('" + m + "','" + allotment.docs[i1].issue_vat + "', '" + allotment.docs[i1].medicine_name + "','" + allotment.docs[i1].batch_no + "','" + allotment.docs[i1].consumption_qty + "','" + allotment.docs[i1].strength + "','" + allotment.docs[i1].issue_qty + "','" + allotment.docs[i1].storage_vat + "','" + allotment.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+allotment.financial_year+"')");
                        NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                        int r = cmd3.ExecuteNonQuery();
                    }
                    VAL = "0";
                    trn.Commit();
                    cn.Close();
                    _log.Info("Insert Consumption Success :" + allotment.molasses_allotment_request_id + "-" + allotment.party_code);
                }
                catch (Exception ex1)
                {
                    _log.Info("Insert Consumption Success :" + allotment.molasses_allotment_request_id + "-" + allotment.party_code + "-" + ex1.Message);
                    VAL = ex1.Message;
                    trn.Rollback();
                }

            }
            return VAL;
        }


        public static string UpdateMNTConsumReg(Molasses_Allocation allotment)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    //(,molasses_allotment_request_id
                    StringBuilder str = new StringBuilder();
                    str.Append("Update exciseautomation.consumption_register set record_status='" + allotment.record_status + "',consumption_date='" + allotment.consumption_date + "', product_code='" + allotment.product_code + "', remarks='" + allotment.remarks + "',total_wastage='"+allotment.prov_indent_qty+"', consumption_reqno='" + allotment.application_requestno + "',lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "',user_id='" + allotment.user_id + "' WHERE consumption_no='" + allotment.issue_no+"' and financial_year = '"+allotment.financial_year+"'");

                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("delete from exciseautomation.consumption_register_item where consumption_register_id='" + allotment.issue_no + "' and financial_year='" + allotment.financial_year + "'", cn);
                        cmd1.ExecuteNonQuery();
                        for (int i1 = 0; i1 < allotment.docs.Count; i1++)
                        {
                            n = 0;
                            str = new StringBuilder();


                            str.Append("INSERT INTO exciseautomation.consumption_register_item(consumption_register_id, issue_vat,medicine_name, batch_no, consumption_bl_qty, strength,medicine_qty,consumption_lp_qty,user_id,creation_date,lastmodified_date,financial_year)");
                            str.Append("Values('" + allotment.issue_no + "','" + allotment.docs[i1].issue_vat + "', '" + allotment.docs[i1].medicine_name + "','" + allotment.docs[i1].batch_no + "','" + allotment.docs[i1].consumption_qty + "','" + allotment.docs[i1].strength + "','" + allotment.docs[i1].issue_qty + "','" + allotment.docs[i1].storage_vat + "','" + allotment.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+allotment.financial_year+"')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd3.ExecuteNonQuery();



                        }
                        VAL = "0";
                    }
                    else
                        VAL = "1";
                    cn.Close();
                    _log.Info("Update Allotment Success :" + allotment.molasses_allotment_request_id + "-" + allotment.party_code);
                }
                catch (Exception ex1)
                {
                    _log.Info("Update Allotment Success :" + allotment.molasses_allotment_request_id + "-" + allotment.party_code + "-" + ex1.Message);
                    VAL = ex1.Message;
                }
            }

            return VAL;
        }

        public static List<Molasses_Allocation> GetMNTCONList()
        {
            List<Molasses_Allocation> allots = new List<Molasses_Allocation>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("Select pm.product_name as product,consumption_reqno,ir.record_status,consumption_no ,consumption_date,party_code,ir.financial_year from exciseautomation.consumption_register ir inner join exciseautomation.product_master pm on pm.product_code =ir.product_code where ir.record_active='true' order by ir.consumption_date desc", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            allots = new List<Molasses_Allocation>();
                            while (dr.Read())
                            {
                                Molasses_Allocation record = new Molasses_Allocation();
                                record.consumption_no = Convert.ToInt32(dr["consumption_no"].ToString());
                                record.application_requestno = dr["consumption_reqno"].ToString();
                                record.product_name = dr["product"].ToString();
                                record.financial_year = dr["financial_year"].ToString();
                                record.consumption_date = Convert.ToDateTime(dr["consumption_date"].ToString()).ToString("dd/MM/yyyy");
                                record.record_status = dr["record_status"].ToString();
                                record.party_code = dr["party_code"].ToString();
                                allots.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Allotment List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Allotment List Success :" + ex.Message);
                }

            }
            return allots;
        }
        public static Molasses_Allocation GetMNTIssueRegDetails1(string id)
        {
            Molasses_Allocation allot = new Molasses_Allocation();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("Select * from exciseautomation.issue_register where issue_no='" + id + "' ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr1 = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr1);
                        dr1.Close();
                        foreach (DataRow dr in dt.Rows)
                        {
                            allot.application_requestno = dr["issue_reqno"].ToString();
                            allot.record_status = dr["record_status"].ToString();
                            allot.issue_date = dr["issue_date"].ToString();
                            allot.issue_no = Convert.ToInt32(dr["issue_no"].ToString());
                            allot.product_code = dr["product_code"].ToString();
                            allot.remarks = dr["remarks"].ToString();
                            allot.docs = new List<EASCM_DOCS>();
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.issue_register_item where issue_register_id='" + id + "' order by issue_register_id", cn))
                            {
                                cmd1.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr2 = cmd1.ExecuteReader();

                                if (dr2.HasRows)
                                {

                                    while (dr2.Read())
                                    {
                                        EASCM_DOCS doc = new EASCM_DOCS();
                                        doc.storage_vat = dr2["storage_vat"].ToString();
                                        doc.issue_vat = dr2["issue_vat"].ToString();
                                        doc.issue_qty = dr2["issue_qty"].ToString();
                                        doc.strength = dr2["strength"].ToString();

                                        allot.docs.Add(doc);
                                    }

                                }

                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Allotment Details Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Allotment Details Success :" + ex.Message);
                }

            }
            return allot;
        }

        public static string Insert_MTP_Issue(Molasses_Allocation allotment)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT  case when max(issue_register_id) is null then 0 else max(issue_register_id) end as issue_register_id FROM exciseautomation.issue_register where financial_year='"+allotment.financial_year+"'", cn);
                int m = Convert.ToInt32(cmd1.ExecuteScalar());
                m = m + 1;
                try
                {
                    allotment.req_allotmentno = m.ToString();
                    StringBuilder str = new StringBuilder();
                    str.Append("INSERT INTO exciseautomation.issue_register(issue_register_id,issue_no, issue_date, product_code,remarks,record_status,issue_reqno,party_code,user_id,creation_date,lastmodified_date,financial_year) Values(");

                    str.Append("'" + m + "','" + m + "','" + allotment.consumption_date + "','" + allotment.product_code + "','" + allotment.remarks + "','" + allotment.record_status + "','" + allotment.application_requestno + "','" + allotment.party_code + "','" + allotment.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+allotment.financial_year+"')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    for (int i1 = 0; i1 < allotment.docs.Count; i1++)
                    {

                        str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.issue_register_item(issue_register_id,storage_vat, issue_vat,issue_qty, strength,issue_qty_lpl,user_id,creation_date,lastmodified_date,financial_year)");
                        str.Append("Values('" + m + "','" + allotment.docs[i1].storage_vat + "','" + allotment.docs[i1].issue_vat + "', '" + allotment.docs[i1].issue_qty + "','" + allotment.docs[i1].strength + "', '" + allotment.docs[i1].consumption_qty + "','" + allotment.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+allotment.financial_year+"')");
                        NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                        int r = cmd3.ExecuteNonQuery();
                    }
                    VAL = "0";
                    trn.Commit();
                    cn.Close();
                    _log.Info("Insert Consumption Success :" + allotment.molasses_allotment_request_id + "-" + allotment.party_code);
                }
                catch (Exception ex1)
                {
                    _log.Info("Insert Consumption Success :" + allotment.molasses_allotment_request_id + "-" + allotment.party_code + "-" + ex1.Message);
                    VAL = ex1.Message;
                    trn.Rollback();
                }

            }
            return VAL;
        }

        public static string Approve_MTNIssue(Molasses_Allocation allotment)
        {
            string value;
            string status_text = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {

                    if (allotment.record_status == "A")
                    {
                        //allotment.allotment_status = "Approved by " + lev[2];
                        allotment.record_status = "A";
                        status_text = "Approved by Bond Officer";

                        NpgsqlCommand cmd1 = new NpgsqlCommand("UPDATE exciseautomation.issue_register SET  record_status='" + allotment.record_status + "' WHERE issue_no='" + allotment.issue_no + "' and  financial_year='" + allotment.financial_year + "'", cn);
                        int n = cmd1.ExecuteNonQuery();
                    }
                    else
                    {

                        if (allotment.record_status == "R")
                        {
                            allotment.record_status = "R";
                            status_text = "Rejected by Bond Officer";

                            NpgsqlCommand cmd1 = new NpgsqlCommand("UPDATE exciseautomation.issue_register SET  record_status='" + allotment.record_status + "' WHERE issue_no='" + allotment.issue_no + "' and  financial_year='" + allotment.financial_year + "'", cn);
                            int n = cmd1.ExecuteNonQuery();
                        }


                    }

                    StringBuilder str = new StringBuilder();
                    for (int i1 = 0; i1 < allotment.docs.Count; i1++)
                    {
                        if (allotment.record_status != "R")
                        {
                            //str = new StringBuilder();
                            //str.Append("INSERT INTO exciseautomation.eascm_docs(doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date)");
                            //str.Append("Values('" + allotment.molasses_allotment_request_id + "','" + allotment.docs[i1].doc_name + "', '" + allotment.docs[i1].description + "','" + allotment.docs[i1].doc_path + "','ALT','" + allotment.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "')");
                            //NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            //int r = cmd3.ExecuteNonQuery();

                            NpgsqlCommand cmd2 = new NpgsqlCommand("select case when vat_availablecapacity is null then 0 else vat_availablecapacity end vat_availablecapacity from  exciseautomation.vat_master  WHERE vat_code='" + allotment.docs[i1].issue_vat + "'", cn);
                            double available = Convert.ToDouble(cmd2.ExecuteScalar());
                            available = available + Convert.ToDouble(allotment.docs[i1].issue_qty);
                            cmd2 = new NpgsqlCommand("UPDATE exciseautomation.vat_master SET  vat_availablecapacity='" + available + "' WHERE  vat_code='" + allotment.docs[i1].issue_vat + "'", cn);
                            int G = cmd2.ExecuteNonQuery();

                            NpgsqlCommand cmd3 = new NpgsqlCommand("select case when vat_availablecapacity is null then 0 else vat_availablecapacity end vat_availablecapacity from  exciseautomation.vat_master  WHERE vat_code='" + allotment.docs[i1].storage_vat + "'", cn);
                            double available_storage = Convert.ToDouble(cmd3.ExecuteScalar());
                            available_storage = available_storage - Convert.ToDouble(allotment.docs[i1].issue_qty);
                            cmd3 = new NpgsqlCommand("UPDATE exciseautomation.vat_master SET  vat_availablecapacity='" + available_storage + "' WHERE  vat_code='" + allotment.docs[i1].storage_vat + "'", cn);
                            int G1 = cmd3.ExecuteNonQuery();

                        }

                    }
                    str = new StringBuilder();
                    str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                    str.Append("'" + allotment.issue_no + "','','" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "','MTI','" + status_text.ToString() + "','" + allotment.remarks + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + allotment.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + allotment.user_id + "','"+allotment.financial_year+"','"+allotment.party_code+"')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    cmd.ExecuteNonQuery();
                    value = "0";
                    trn.Commit();
                    _log.Info("Allotement Approve Sucess:" + allotment.issue_no);
                    cn.Close();
                }
                catch (Exception ex)
                {
                    value = ex.Message;
                    _log.Info("Allotement Approve Fail:" + allotment.issue_no + "-" + value);
                    trn.Rollback();
                }
                return value;

            }
        }


        public static string Approve_MTNConsumption(Molasses_Allocation allotment)
        {
            string value;
            string status_text = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {

                    if (allotment.record_status == "A")
                    {
                        //allotment.allotment_status = "Approved by " + lev[2];
                        allotment.record_status = "A";
                        status_text = "Approved by Bond Officer";



                        NpgsqlCommand cmd1 = new NpgsqlCommand("UPDATE exciseautomation.consumption_register SET  record_status='" + allotment.record_status + "' WHERE consumption_no='" + allotment.consumption_no + "' and financial_year='" + allotment.financial_year + "'", cn);
                        int n = cmd1.ExecuteNonQuery();
                    }
                    else
                    {

                        if (allotment.record_status == "R")
                        {
                            allotment.record_status = "R";
                            status_text = "Rejected by Bond Officer";
                            NpgsqlCommand cmd1 = new NpgsqlCommand("UPDATE exciseautomation.consumption_register SET  record_status='" + allotment.record_status + "' WHERE consumption_no='" + allotment.consumption_no + "' and financial_year='" + allotment.financial_year + "'", cn);
                            int n = cmd1.ExecuteNonQuery();
                        }


                    }

                    StringBuilder str = new StringBuilder();
                    int i2 = 0;
                    i2 = allotment.docs.Count;
                    double available = 0;
                    NpgsqlCommand cmd2;
                    string vatcode = "";
                    for (int i1 = 0; i1 < allotment.docs.Count; i1++)
                    {
                        if (allotment.record_status != "R")
                        {
                            //str = new StringBuilder();
                            //str.Append("INSERT INTO exciseautomation.eascm_docs(doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date)");
                            //str.Append("Values('" + allotment.molasses_allotment_request_id + "','" + allotment.docs[i1].doc_name + "', '" + allotment.docs[i1].description + "','" + allotment.docs[i1].doc_path + "','ALT','" + allotment.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "')");
                            //NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            //int r = cmd3.ExecuteNonQuery();

                            cmd2 = new NpgsqlCommand("select case when vat_availablecapacity is null then 0 else vat_availablecapacity end vat_availablecapacity from  exciseautomation.vat_master  WHERE vat_code='" + allotment.docs[i1].issue_vat + "'", cn);
                            available = Convert.ToDouble(cmd2.ExecuteScalar());
                            available = available - (Convert.ToDouble(allotment.docs[i1].consumption_qty));
                            vatcode = allotment.docs[i1].issue_vat;
                            cmd2 = new NpgsqlCommand("UPDATE exciseautomation.vat_master SET  vat_availablecapacity='" + available + "' WHERE  vat_code='" + allotment.docs[i1].issue_vat + "'", cn);
                            int G = cmd2.ExecuteNonQuery();
                            i2--;
                        }
                        if (i2 == 0)
                        {
                            available = available - allotment.reqd_qty;
                            cmd2 = new NpgsqlCommand("UPDATE exciseautomation.vat_master SET  vat_availablecapacity='" + available + "' WHERE  vat_code='" + vatcode + "'", cn);
                            int G = cmd2.ExecuteNonQuery();
                        }

                    }

                    str = new StringBuilder();
                    str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                    str.Append("'" + allotment.consumption_no + "','','" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "','MTC','" + status_text + "','" + allotment.remarks + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + allotment.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + allotment.user_id + "','"+allotment.financial_year+"','"+allotment.party_code+"')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    cmd.ExecuteNonQuery();
                    value = "0";
                    trn.Commit();
                    _log.Info("Allotement Approve Sucess:" + allotment.issue_no);
                    cn.Close();
                }
                catch (Exception ex)
                {
                    value = ex.Message;
                    _log.Info("Allotement Approve Fail:" + allotment.issue_no + "-" + value);
                    trn.Rollback();
                }
                return value;

            }
        }


        public static string UpdateMNTIssueReg(Molasses_Allocation allotment)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    //(,molasses_allotment_request_id
                    StringBuilder str = new StringBuilder();
                    str.Append("Update exciseautomation.issue_register set issue_date='" + allotment.consumption_date + "', product_code='" + allotment.product_code + "', remarks='" + allotment.remarks + "', issue_reqno='" + allotment.application_requestno + "',record_status='" + allotment.record_status + "',lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "',user_id='" + allotment.user_id + "' WHERE issue_no='" + allotment.issue_no+ "' and  financial_year='" + allotment.financial_year + "'");

                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("delete from exciseautomation.issue_register_item where issue_register_id='" + allotment.issue_no + "' and  financial_year='" + allotment.financial_year + "'", cn);
                        cmd1.ExecuteNonQuery();
                        for (int i1 = 0; i1 < allotment.docs.Count; i1++)
                        {
                            n = 0;
                            str = new StringBuilder();


                            str.Append("INSERT INTO exciseautomation.issue_register_item(issue_register_id,storage_vat, issue_vat,issue_qty, strength,issue_qty_lpl,user_id,creation_date,lastmodified_date,financial_year)");
                            str.Append("Values('" + allotment.issue_no + "','" + allotment.docs[i1].storage_vat + "','" + allotment.docs[i1].issue_vat + "', '" + allotment.docs[i1].issue_qty + "','" + allotment.docs[i1].strength + "', '" + allotment.docs[i1].consumption_qty + "','" + allotment.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+allotment.financial_year+"')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd3.ExecuteNonQuery();


                            //int r = cmd3.ExecuteNonQuery();
                        }
                        VAL = "0";
                    }
                    else
                        VAL = "1";
                    cn.Close();
                    _log.Info("Update Allotment Success :" + allotment.molasses_allotment_request_id + "-" + allotment.party_code);
                }
                catch (Exception ex1)
                {
                    _log.Info("Update Allotment Success :" + allotment.molasses_allotment_request_id + "-" + allotment.party_code + "-" + ex1.Message);
                    VAL = ex1.Message;
                }
            }

            return VAL;
        }
        public static List<Molasses_Allocation> GetMNTIssueList()
        {
            List<Molasses_Allocation> allots = new List<Molasses_Allocation>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT issue_reqno, pm.product_name  as product,ir.record_status,issue_no, date(issue_date) as issue_date ,party_code,ir.financial_year FROM exciseautomation.issue_register ir inner join exciseautomation.product_master pm on pm.product_code =ir.product_code where ir.record_active='true'", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            allots = new List<Molasses_Allocation>();
                            while (dr.Read())
                            {
                                Molasses_Allocation record = new Molasses_Allocation();
                                record.issue_no = Convert.ToInt32(dr["issue_no"].ToString());
                                DateTime dtt = new DateTime();
                                //record.issue_date = dr["issue_date"].ToString();
                                record.issue_date = Convert.ToDateTime(dr["issue_date"].ToString()).ToString("dd/MM/yyyy");
                                record.product_name = dr["product"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.financial_year = dr["financial_year"].ToString();
                                record.application_requestno = dr["issue_reqno"].ToString();
                                record.party_code = dr["party_code"].ToString();
                                allots.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Allotment List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Allotment List Success :" + ex.Message);
                }

            }
            return allots;
        }




        public static string UpdateMNTIssueReg1(Molasses_Allocation allotment)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    //(,molasses_allotment_request_id
                    StringBuilder str = new StringBuilder();
                    str.Append("Update exciseautomation.issue_register set issue_date='" + allotment.consumption_date + "', product_code='" + allotment.product_code + "', remarks='" + allotment.remarks + "', issue_reqno='" + allotment.application_requestno + "' WHERE issue_no=" + allotment.issue_no);

                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("delete from exciseautomation.issue_register_item where issue_register_id='" + allotment.issue_no + "'", cn);
                        cmd1.ExecuteNonQuery();
                        for (int i1 = 0; i1 < allotment.docs.Count; i1++)
                        {
                            n = 0;
                            str = new StringBuilder();


                            str.Append("INSERT INTO exciseautomation.issue_register_item(issue_register_id,storage_vat, issue_vat,issue_qty, strength)");
                            str.Append("Values('" + allotment.issue_no + "','" + allotment.docs[i1].storage_vat + "','" + allotment.docs[i1].issue_vat + "', '" + allotment.docs[i1].issue_qty + "','" + allotment.docs[i1].strength + "')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd3.ExecuteNonQuery();


                            int r = cmd3.ExecuteNonQuery();
                        }
                        VAL = "0";
                    }
                    else
                        VAL = "1";
                    cn.Close();
                    _log.Info("Update Allotment Success :" + allotment.molasses_allotment_request_id + "-" + allotment.party_code);
                }
                catch (Exception ex1)
                {
                    _log.Info("Update Allotment Success :" + allotment.molasses_allotment_request_id + "-" + allotment.party_code + "-" + ex1.Message);
                    VAL = ex1.Message;
                }
            }

            return VAL;
        }
        //GetMNTIssueList
        public static List<Molasses_Allocation> GetMNTIssueList1()
        {
            List<Molasses_Allocation> allots = new List<Molasses_Allocation>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT issue_no, substring(issue_date,0,10) as issue_date FROM exciseautomation.issue_register", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            allots = new List<Molasses_Allocation>();
                            while (dr.Read())
                            {
                                Molasses_Allocation record = new Molasses_Allocation();
                                record.issue_no = Convert.ToInt32(dr["issue_no"].ToString());
                                record.issue_date = dr["issue_date"].ToString();
                                allots.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Allotment List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Allotment List Success :" + ex.Message);
                }

            }
            return allots;
        }
        //GetMNTCONList
        public static List<Molasses_Allocation> GetMNTCONList1()
        {
            List<Molasses_Allocation> allots = new List<Molasses_Allocation>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("Select consumption_no , substring(consumption_date,1,10) as consumption_date from exciseautomation.consumption_register", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            allots = new List<Molasses_Allocation>();
                            while (dr.Read())
                            {
                                Molasses_Allocation record = new Molasses_Allocation();
                                record.consumption_no = Convert.ToInt32(dr["consumption_no"].ToString());
                                record.consumption_date = dr["consumption_date"].ToString();
                                allots.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Allotment List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Allotment List Success :" + ex.Message);
                }

            }
            return allots;
        }
        //MNTP_Consumption_List
        public static Molasses_Allocation GetDetails(string id,string financial_year)
        {
            Molasses_Allocation allot = new Molasses_Allocation();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("Select a.*,b.product_name from exciseautomation.molasses_allotment_request a inner join exciseautomation.product_master b on a.product_code=b.product_code where financial_year='"+financial_year+"' and molasses_allotment_request_id='" + id + "' ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr1 = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr1);
                        dr1.Close();
                        foreach (DataRow dr in dt.Rows)
                        {
                            allot.financial_year = dr["financial_year"].ToString();
                            allot.record_status = dr["record_status"].ToString();
                            allot.req_allotmentno = dr["req_allotmentno"].ToString();
                            allot.final_allotmentno = dr["final_allotmentno"].ToString();
                            allot.req_allotmentdate = dr["req_allotmentdate"].ToString().Substring(0, 10).Replace("/", "-");
                            allot.iscaptive = dr["iscaptive"].ToString();
                            allot.requested_fromunit = dr["requested_fromunit"].ToString();
                            allot.prov_indent_qty = Convert.ToDouble(dr["prov_indent_qty"].ToString());
                            allot.qty_allotted_till_date = Convert.ToDouble(dr["qty_allotted_till_date"].ToString());
                            allot.reqd_qty = Convert.ToDouble(dr["reqd_qty"].ToString());
                            allot.remarks = dr["remarks"].ToString();
                            allot.product_code = dr["product_code"].ToString();
                            allot.product_name = dr["product_name"].ToString();
                            allot.party_code = dr["party_code"].ToString();
                            allot.allotment_status = dr["allotment_status"].ToString();
                            if (dr["allotment_validdate"].ToString() != "")
                                allot.allotment_validdate = dr["allotment_validdate"].ToString().Substring(0, 10).Replace("/", "-");
                            if (dr["approver_level"].ToString() == "")
                                allot.approverlevel = 0;
                            else
                                allot.approverlevel = Convert.ToInt32(dr["approver_level"]);
                            // allot.molasses_allotment_request_id = dr["molasses_allotment_request_id"].ToString();
                            if (dr["allotted_qty"].ToString() == "")
                                allot.approved_qty = 0;
                            else
                                allot.approved_qty = Convert.ToDouble(dr["allotted_qty"].ToString());
                            allot.docs = new List<EASCM_DOCS>();
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.eascm_docs where doc_id='" + id + "' and doc_type_code='ALT' and  financial_year='" + financial_year + "' order by eascm_docs_id", cn))
                            {
                                cmd1.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr2 = cmd1.ExecuteReader();

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
                                        allot.docs.Add(doc);
                                    }

                                }

                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Allotment Details Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Allotment Details Success :" + ex.Message);
                }

            }
            return allot;
        }
        public static Molasses_Allocation MNTP_Consumption_List(string id,string financial_year)
        {
            Molasses_Allocation allot = new Molasses_Allocation();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT consumption_register_id, consumption_no, consumption_date, pm.product_code, pm.product_name,party_code, remarks,  cr.record_status"
                        + " FROM exciseautomation.consumption_register cr inner join exciseautomation.product_master pm on pm.product_code=cr.product_code"
                        + " where user_id='" + id + "'", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr1 = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr1);
                        dr1.Close();
                        foreach (DataRow dr in dt.Rows)
                        {
                            allot.financial_year = dr["financial_year"].ToString();
                            allot.record_status = dr["record_status"].ToString();
                            allot.req_allotmentno = dr["req_allotmentno"].ToString();
                            allot.final_allotmentno = dr["final_allotmentno"].ToString();
                            allot.req_allotmentdate = dr["req_allotmentdate"].ToString().Substring(0,10).Replace("/","-");
                            allot.iscaptive = dr["iscaptive"].ToString();
                            allot.requested_fromunit = dr["requested_fromunit"].ToString();
                            allot.prov_indent_qty = Convert.ToDouble(dr["prov_indent_qty"].ToString());
                            allot.qty_allotted_till_date = Convert.ToDouble(dr["qty_allotted_till_date"].ToString());
                            allot.reqd_qty = Convert.ToDouble(dr["reqd_qty"].ToString());
                            allot.remarks = dr["remarks"].ToString();
                            allot.product_code = dr["product_code"].ToString();
                            allot.party_code = dr["party_code"].ToString();
                            allot.allotment_status = dr["allotment_status"].ToString();
                            if (dr["allotment_validdate"].ToString()!="")
                            allot.allotment_validdate = dr["allotment_validdate"].ToString().Substring(0, 10).Replace("/", "-");
                            if (dr["approver_level"].ToString() == "")
                                allot.approverlevel = 0;
                            else
                                allot.approverlevel = Convert.ToInt32(dr["approver_level"]);
                            // allot.molasses_allotment_request_id = dr["molasses_allotment_request_id"].ToString();
                            if (dr["allotted_qty"].ToString() == "")
                                allot.approved_qty = 0;
                            else
                                allot.approved_qty = Convert.ToDouble(dr["allotted_qty"].ToString());
                            allot.docs = new List<EASCM_DOCS>();
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.eascm_docs where doc_id='" + id + "' and doc_type_code='ALT' and  financial_year='" + financial_year + "' order by eascm_docs_id", cn))
                            {
                                cmd1.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr2 = cmd1.ExecuteReader();
                               
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
                                        allot.docs.Add(doc);
                                    }

                                }

                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Allotment Details Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Allotment Details Success :" + ex.Message);
                }

            }
            return allot;
        }

        //Insert_MTP_Issue
        public static string Insert_MTP_Issue1(Molasses_Allocation allotment)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT  case when max(issue_register_id) is null then 0 else max(issue_register_id) end as issue_register_id FROM exciseautomation.issue_register", cn);
                int m = Convert.ToInt32(cmd1.ExecuteScalar());
                m = m + 1;
                try
                {
                    allotment.req_allotmentno = m.ToString();
                    StringBuilder str = new StringBuilder();
                    str.Append("INSERT INTO exciseautomation.issue_register(issue_no, issue_date, product_code,remarks,record_status,issue_reqno,party_code) Values(");

                    str.Append("'" + m + "','" + allotment.consumption_date + "','" + allotment.product_code + "','" + allotment.remarks + "','" + allotment.record_status + "','" + allotment.application_requestno + "','" + allotment.party_code + "')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    for (int i1 = 0; i1 < allotment.docs.Count; i1++)
                    {

                        str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.issue_register_item(issue_register_id,storage_vat, issue_vat,issue_qty, strength)");
                        str.Append("Values('" + m + "','" + allotment.docs[i1].storage_vat + "','" + allotment.docs[i1].issue_vat + "', '" + allotment.docs[i1].issue_qty + "','" + allotment.docs[i1].strength + "')");
                        NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                        int r = cmd3.ExecuteNonQuery();
                    }
                    VAL = "0";
                    trn.Commit();
                    cn.Close();
                    _log.Info("Insert Consumption Success :" + allotment.molasses_allotment_request_id + "-" + allotment.party_code);
                }
                catch (Exception ex1)
                {
                    _log.Info("Insert Consumption Success :" + allotment.molasses_allotment_request_id + "-" + allotment.party_code + "-" + ex1.Message);
                    VAL = ex1.Message;
                    trn.Rollback();
                }

            }
            return VAL;
        }

        public static string Insert_MTP_Con1(Molasses_Allocation allotment)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT  case when max(molasses_allotment_request_id) is null then 0 else max(molasses_allotment_request_id) end as molasses_allotment_request_id FROM exciseautomation.molasses_allotment_request", cn);
                int m = Convert.ToInt32(cmd1.ExecuteScalar());
                m = m + 1;
                try
                {
                    allotment.req_allotmentno = m.ToString();
                    StringBuilder str = new StringBuilder();
                    str.Append("INSERT INTO exciseautomation.consumption_register(consumption_no, consumption_date, product_code,remarks) Values(");

                    str.Append("'" + m + "','" + allotment.consumption_date + "','" + allotment.product_code + "','" + allotment.remarks + "')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    for (int i1 = 0; i1 < allotment.docs.Count; i1++)
                    {

                        str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.consumption_register_item(consumption_register_id, issue_vat,medicine_name, batch_no, consumption_qty, strength)");
                        str.Append("Values('" + m + "','VAT', '" + allotment.docs[i1].medicine_name + "','" + allotment.docs[i1].batch_no + "','" + allotment.docs[i1].consumption_qty + "','" + allotment.docs[i1].strength + "')");
                        NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                        int r = cmd3.ExecuteNonQuery();
                    }
                    VAL = "0";
                    trn.Commit();
                    cn.Close();
                    _log.Info("Insert Consumption Success :" + allotment.molasses_allotment_request_id + "-" + allotment.party_code);
                }
                catch (Exception ex1)
                {
                    _log.Info("Insert Consumption Success :" + allotment.molasses_allotment_request_id + "-" + allotment.party_code + "-" + ex1.Message);
                    VAL = ex1.Message;
                    trn.Rollback();
                }

            }
            return VAL;
        }

    }
}
