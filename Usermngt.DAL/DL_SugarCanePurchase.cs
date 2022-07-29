using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using Usermngt.Entities;

namespace Usermngt.DAL
{
   public class DL_SugarCanePurchase
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static string Insert(SugarCanePurchase scp)
        {
            
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    int max = Convert.ToInt32(DL_Org_List.GetMaxID("sugarcanepurchase").ToString()) + 1;
                    StringBuilder str = new StringBuilder();
                    int n = 0;
                    // where financial_year='" + scp.financialyear + "'
                    NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT max(sugarcanepurchase_id) FROM exciseautomation.sugarcanepurchase", cn);
                    string m = cmd1.ExecuteScalar().ToString();
                    n = 0;
                    if (m == "")
                        n = 1;
                    else
                        n = Convert.ToInt32(m) + 1;
                    str.Append("INSERT INTO exciseautomation.sugarcanepurchase(sugarcanepurchase_id, party_code, financialyear, entrydate, factorygate_purchase, outstation_purchase, ownestate_purchase, total_purchase, total_canecrushed, remarks,  user_id, creation_date,record_status,financial_year)");
                    str.Append("VALUES('" +n + "','" + scp.party_code + "','" + scp.financialyear + "','" + scp.entrydate + "','" + scp.factorygate_purchase + "','" + scp.outstation_purchase + "','" + scp.ownestate_purchase + "','" + scp.total_purchase + "','" + scp.total_canecrushed + "','" + scp.remarks + "','" + scp.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + scp.record_status + "','"+scp.financialyear+"')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        for (int i = 0; i < scp.docs.Count; i++)
                        {
                            n = 0;
                            str = new StringBuilder();
                            
                            str.Append("INSERT INTO exciseautomation.eascm_docs(doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,financial_year,party_code)");
                            str.Append("Values('" + max + "','" + scp.docs[i].doc_name + "', '"+scp.docs[i].description+"','" + scp.docs[i].doc_path + "','SCP','" + scp.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+scp.financialyear+"','"+scp.party_code+"')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd3.ExecuteNonQuery();
                        }
                        VAL = "0";
                        trn.Commit();
                        _log.Info("Sugarcanepurchase Insertion Success:" + scp.sugarcanepurchase_id + '-' + scp.party_code);
                        cn.Close();
                    }
                    else
                    {
                        VAL = "1";
                        trn.Rollback();
                        _log.Info("Sugarcanepurchase Insertion Fail:" + scp.sugarcanepurchase_id + '-' + scp.party_code);
                    }
                }
                catch (Exception ex1)
                {
                    trn.Rollback();
                    _log.Info("Sugarcanepurchase Insertion Fail:" + scp.sugarcanepurchase_id + '-' + scp.party_code + "-" + ex1.Message);
                    VAL = ex1.Message;
                }

            }
            return VAL;
        }

        public static string GetPedning(string party_code,string financial_year)
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
                    str.Append("Select (case when Sum(total_purchase)-Sum(total_canecrushed) is null then 0 else Sum(total_purchase)-Sum(total_canecrushed) end)  from exciseautomation.sugarcanepurchase where  party_code='" + party_code+ "' and record_active='True' and financial_year='"+financial_year+"'");
                    NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                    VAL= cmd3.ExecuteScalar().ToString();
                    
                    
                    trn.Commit();
                    cn.Close();

                }
                catch (Exception ex1)
                {
                    trn.Rollback();
                  
                    VAL = ex1.Message;
                }

            }
            return VAL;
        }

        public static string Approve(SugarCanePurchase scp)
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
                    str.Append("update exciseautomation.sugarcanepurchase set  record_status='" + scp.record_status + "' where sugarcanepurchase_id='" + scp.sugarcanepurchase_id + "' and financial_year='" + scp.financialyear + "'");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (scp.record_status == "A")
                    {
                        scp.record_status = "Approved By Bond Officer";
                    }
                    else
                    {
                        scp.record_status = "Rejected By Bond Officer";
                    }
                    str = new StringBuilder();
                    str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                    str.Append("'" + scp.sugarcanepurchase_id + "','','" +DateTime.Now.ToString("dd-MM-yyyy")+"','SCP','"+ scp.record_status+"','" + scp.remarks + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + scp.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + scp.user_id + "','"+scp.financialyear+"','"+scp.party_code+"')");
                    cmd = new NpgsqlCommand(str.ToString(), cn);
                    n = cmd.ExecuteNonQuery();
                    VAL = "0";
                    _log.Info("Sugarcanepurchase "+scp.record_status+" Sucess:" + scp.sugarcanepurchase_id + '-' + scp.party_code);
                    trn.Commit();
                    cn.Close();

                }
                catch (Exception ex1)
                {
                    trn.Rollback();
                    _log.Info("Sugarcanepurchase " + scp.record_status + " Fail:" + scp.sugarcanepurchase_id + '-' + scp.party_code + "-" + ex1.Message);
                    VAL = ex1.Message;
                }

            }
            return VAL;
        }

        public static string Update(SugarCanePurchase scp)
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
                    str.Append("update exciseautomation.sugarcanepurchase set  party_code='" + scp.party_code + "',financialyear='" + scp.financialyear + "',entrydate='" + scp.entrydate + "',factorygate_purchase='" + scp.factorygate_purchase + "',outstation_purchase='" + scp.outstation_purchase + "', ownestate_purchase='" + scp.ownestate_purchase + "',total_purchase='" + scp.total_purchase + "',total_canecrushed='" + scp.total_canecrushed + "',remarks='" + scp.remarks + "',user_id='" + scp.user_id + "',lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "',record_status='"+scp.record_status+ "' where sugarcanepurchase_id='" + scp.sugarcanepurchase_id+"' and financial_year='"+scp.financialyear+"'");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("delete from exciseautomation.eascm_docs where doc_id='" + scp.sugarcanepurchase_id + "' and doc_type_code='SCP' and financial_year='" + scp.financialyear + "'", cn);
                        cmd1.ExecuteNonQuery();
                        for (int i = 0; i < scp.docs.Count; i++)
                        {
                            n = 0;
                            str = new StringBuilder();

                            str.Append("INSERT INTO exciseautomation.eascm_docs(doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,financial_year,party_code)");
                            str.Append("Values('" + scp.sugarcanepurchase_id + "','" + scp.docs[i].doc_name + "', '" + scp.docs[i].description + "','" + scp.docs[i].doc_path + "','SCP','" + scp.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+scp.financialyear+"','"+scp.party_code+"')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd3.ExecuteNonQuery();
                        }
                        VAL = "0";
                        _log.Info("Sugarcanepurchase Insertion Sucess:" + scp.sugarcanepurchase_id + '-' + scp.party_code);
                        trn.Commit();
                        cn.Close();
                    }
                    else
                    {
                        trn.Rollback();
                        VAL = "1";
                        _log.Info("Sugarcanepurchase Insertion Fail:" + scp.sugarcanepurchase_id + '-' + scp.party_code);
                    }
                }
                catch (Exception ex1)
                {
                    trn.Rollback();
                    _log.Info("Sugarcanepurchase Insertion Fail:" + scp.sugarcanepurchase_id + '-' + scp.party_code + "-" + ex1.Message);
                    VAL = ex1.Message;
                }

            }
            return VAL;
        }
        public static List<SugarCanePurchase> GetList()
        {

            List<SugarCanePurchase> scplist = new List<SugarCanePurchase>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name from exciseautomation.sugarcanepurchase a inner join exciseautomation.party_master b on a.party_code=b.party_code  where a.record_active='true' order by a.party_code,a.entrydate", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            scplist = new List<SugarCanePurchase>();
                            int r = 0;
                           
                            while (dr.Read())
                            {

                               
                                SugarCanePurchase record = new SugarCanePurchase();
                                record.sugarcanepurchase_id = Convert.ToInt32(dr["sugarcanepurchase_id"].ToString());
                                record.party_code = dr["party_code"].ToString();
                                record.financialyear = dr["financial_year"].ToString();
                                record.entrydate =dr["entrydate"].ToString().Substring(0,10).Replace("/","-");
                                record.factorygate_purchase = Convert.ToDouble(dr["factorygate_purchase"].ToString());
                                record.outstation_purchase = Convert.ToDouble(dr["outstation_purchase"].ToString());
                                record.ownestate_purchase = Convert.ToDouble(dr["ownestate_purchase"].ToString());
                                record.total_purchase = Convert.ToDouble(dr["total_purchase"].ToString());
                                record.total_canecrushed = Convert.ToDouble(dr["total_canecrushed"].ToString());
                                record.party_name = dr["Party_name"].ToString();
                                record.remarks = dr["remarks"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                scplist.Add(record);
                            
                                
                            }
                        }
                    }
                    _log.Info("Sugarcanepurchase Get List Success");
                    cn.Close();
                }
                catch (Exception ex)
                {
                    _log.Info("Sugarcanepurchase Get List Fail :" + ex.Message);
                }

            }
            return scplist;
        }

        public static List<SugarCanePurchase> Search(string tablename, string column, string value)
        {
            List<SugarCanePurchase> scplist = new List<SugarCanePurchase>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name from exciseautomation.sugarcanepurchase a inner join exciseautomation.party_master b on a.party_code=b.party_code where " + column + " Ilike '%" + value + "%' and a.record_active='true'    order by a.party_code,a.entrydate", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                           scplist = new List<SugarCanePurchase>();
                            while (dr.Read())
                            {
                                SugarCanePurchase record = new SugarCanePurchase();
                                record.sugarcanepurchase_id = Convert.ToInt32(dr["sugarcanepurchase_id"].ToString());
                                record.party_code = dr["party_code"].ToString();
                                record.financialyear = dr["financialyear"].ToString();
                                record.entrydate = dr["entrydate"].ToString().Substring(0, 10).Replace("/", "-");
                                record.factorygate_purchase = Convert.ToDouble(dr["factorygate_purchase"].ToString());
                                record.outstation_purchase = Convert.ToDouble(dr["outstation_purchase"].ToString());
                                record.ownestate_purchase = Convert.ToDouble(dr["ownestate_purchase"].ToString());
                                record.total_purchase = Convert.ToDouble(dr["total_purchase"].ToString());
                                record.total_canecrushed = Convert.ToDouble(dr["total_canecrushed"].ToString());
                                record.party_name = dr["Party_name"].ToString();
                                record.remarks = dr["remarks"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                scplist.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return scplist;
        }

        public static SugarCanePurchase GetDetails(int scpid,string financial_year)
        {

            SugarCanePurchase scplist = new SugarCanePurchase();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select *from exciseautomation.sugarcanepurchase where sugarcanepurchase_id='"+scpid+"' and financial_year='"+financial_year+"'", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr1 = cmd.ExecuteReader();
                        DataTable scp = new DataTable();
                        scp.Load(dr1);
                        dr1.Close();
                        foreach (DataRow dr in scp.Rows)
                        {
                            scplist.sugarcanepurchase_id = Convert.ToInt32(dr["sugarcanepurchase_id"].ToString());
                            scplist.party_code = dr["party_code"].ToString();
                            scplist.financialyear = dr["financialyear"].ToString();
                            scplist.entrydate = dr["entrydate"].ToString().Substring(0, 10).Replace("/", "-");
                            scplist.factorygate_purchase = Convert.ToDouble(dr["factorygate_purchase"].ToString());
                            scplist.outstation_purchase = Convert.ToDouble(dr["outstation_purchase"].ToString());
                            scplist.ownestate_purchase = Convert.ToDouble(dr["ownestate_purchase"].ToString());
                            scplist.total_purchase = Convert.ToDouble(dr["total_purchase"].ToString());
                            scplist.total_canecrushed = Convert.ToDouble(dr["total_canecrushed"].ToString());
                         
                            scplist.remarks = dr["remarks"].ToString();
                            scplist.record_status = dr["record_status"].ToString();
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.eascm_docs where doc_id='" + scpid + "' and doc_type_code='SCP' and record_active='true' and financial_year='" +financial_year + "' order by eascm_docs_id", cn))
                            {
                                cmd1.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr2 = cmd1.ExecuteReader();
                                scplist.docs = new List<EASCM_DOCS>();
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
                                        scplist.docs.Add(doc);
                                    }
                                    
                                }

                            }
                        }
                    }
                    _log.Info("Sugarcanepurchase Get List Success");
                    cn.Close();
                }
                catch (Exception ex)
                {
                    _log.Info("Sugarcanepurchase Get List Fail :" + ex.Message);
                }

            }
            return scplist;
        }
    }
}
