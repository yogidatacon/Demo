using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using Usermngt.Entities;

namespace Usermngt.DAL
{
   
    public  class DL_Complaint
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");

        public static List<Complaint> Getlist()
        {
            List<Complaint> com = new List<Complaint>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    cmd = new NpgsqlCommand("select * from exciseautomation.complaint", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        com = new List<Complaint>();
                        while (dr.Read())
                        {
                            Complaint dispatch = new Complaint();
                            dispatch.complaint_id = Convert.ToInt32(dr["complaint_id"].ToString());
                            dispatch.complaint_no = Convert.ToInt32(dr["complaint_no"].ToString());
                            dispatch.creation_date = Convert.ToDateTime(dr["creation_date"].ToString()).ToString("dd-MM-yyyy");
                            dispatch.complainant_name = dr["complainant_name"].ToString();
                            dispatch.email_id = dr["email_id"].ToString();
                            dispatch.mobile_no = Convert.ToDouble(dr["contact_no"].ToString());
                            dispatch.complaint_type = dr["complaint_type"].ToString();
                            dispatch.record_status= dr["record_status"].ToString();
                            dispatch.address = dr["address"].ToString();
                            dispatch.complaint_details = dr["complaint_details"].ToString();
                            dispatch.complaint_status = dr["complainant_status"].ToString();
                            //dispatch.user_id = userid;
                            dispatch.otp = dr["otp"].ToString();
                            com.Add(dispatch);

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return com;
            }
        }

        public static List<Complaint> Search(string tablename, string column, string value)
        {
            List<Complaint> com = new List<Complaint>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    cmd = new NpgsqlCommand("select * from exciseautomation.complaint where  " + column + " Ilike '%" + value + "%' ", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        com = new List<Complaint>();
                        while (dr.Read())
                        {
                            Complaint dispatch = new Complaint();
                            dispatch.complaint_id = Convert.ToInt32(dr["complaint_id"].ToString());
                            dispatch.creation_date = Convert.ToDateTime(dr["creation_date"].ToString()).ToString("dd-MM-yyyy");
                            dispatch.complainant_name = dr["complainant_name"].ToString();
                            dispatch.email_id = dr["email_id"].ToString();
                            dispatch.mobile_no = Convert.ToDouble(dr["contact_no"].ToString());
                            dispatch.complaint_type = dr["complaint_type"].ToString();
                            dispatch.record_status = dr["record_status"].ToString();
                            dispatch.address = dr["address"].ToString();
                            dispatch.complaint_details = dr["complaint_details"].ToString();
                            //dispatch.user_id = userid;
                            dispatch.otp = dr["otp"].ToString();
                            com.Add(dispatch);

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
               
            }
            return com;
        }




        public static string Insert(Complaint com)
        {
            string val = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd2 = new NpgsqlCommand("select max(complaint_no) from exciseautomation.complaint", cn);
                    string a = cmd2.ExecuteScalar().ToString();
                    int b = 0;
                    if (a == "")
                        b = 1;
                    else
                        b = Convert.ToInt32(a) + 1;
                    StringBuilder str = new StringBuilder();
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.complaint(complainant_name, contact_no, email_id, complaint_type, address, complaint_details, creation_date,record_status, otp,state,district,thana,village,landmark,complaint_no)VALUES('" + com.complainant_name+"','"+com.mobile_no+"', '"+com.email_id+"', '"+com.complaint_type+"', '"+com.address+"', '"+com.complaint_details+"', '"+DateTime.Now.ToShortDateString()+"', '"+com.record_status+"', '"+com.otp+"','"+com.state+"','"+com.district+"','"+com.thana+"','"+com.village+"','"+com.landmark+"','"+b+"')", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    int n =cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("select max(complaint_id) from exciseautomation.complaint", cn);
                        string m = cmd1.ExecuteScalar().ToString();
                        for (int i = 0; i < com.docs.Count; i++)
                        {

                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.eascm_docs(doc_id, doc_name,doc_desc, doc_path, doc_type_code,creation_date,financial_year)");
                            str.Append("Values('" + m + "','" + com.docs[i].doc_name +"', '" + com.docs[i].description + "','" + com.docs[i].doc_path + "','CGR','" + DateTime.Now.ToString("dd-MM-yyyy") +"')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand("INSERT INTO exciseautomation.eascm_docs(doc_id, doc_name,doc_desc, doc_path, doc_type_code,creation_date,financial_year) Values('" + m + "','" + com.docs[i].doc_name + "', '" + com.docs[i].description + "','" + com.docs[i].doc_path + "','CGR','" + DateTime.Now.ToString("dd-MM-yyyy") + "','2021-2022')", cn);
                            int r = cmd3.ExecuteNonQuery();
                        }
                        val = "0"+"_"+b;
                    }

                }
                catch (Exception ex)
                {

                    throw (ex);
                }
                return val;
            }
        }

        public static string SuggestionInsert(Complaint com)
        {
            string val = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    //NpgsqlCommand cmd2 = new NpgsqlCommand("select max(complaint_no) from exciseautomation.complaint", cn);
                    //string a = cmd2.ExecuteScalar().ToString();
                    //int b = 0;
                    //if (a == "")
                    //    b = 1;
                    //else
                    //    b = Convert.ToInt32(a) + 1;
                    StringBuilder str = new StringBuilder();
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.suggestion(suggestion_name, contact_no,suggestion,creation_date,record_status)VALUES('" + com.complainant_name + "','" + com.mobile_no + "', '" + com.address + "', '" + DateTime.Now.ToShortDateString() + "', '" + com.record_status + "')", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        val = "0";
                    }

                }
                catch (Exception ex)
                {

                    throw (ex);
                }
                return val;
            }
        }

        public static string Update(Complaint com)
        {
            string val = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    StringBuilder str = new StringBuilder();
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.complaint SET complainant_name='"+com.complainant_name+"'', contact_no='"+com.mobile_no+"', email_id='"+com.email_id+"', complaint_type='"+com.complaint_type+"', adddress='"+com.address+"', complaint_details='"+com.complaint_details+"', lastmodified_date='"+DateTime.Now.ToShortDateString()+"', record_status='"+ com.record_status +"', otp='"+com.otp+"',state='"+com.state+"',district='"+com.district+"',thana='"+com.thana+"',village='"+com.village+"',landmark='"+com.landmark+"' WHERE complaint_id='"+com.complaint_id+"'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();
                    NpgsqlCommand cmd2 = new NpgsqlCommand("delete from exciseautomation.eascm_docs where doc_id='" + com.complaint_id + "' and doc_type_code='CGR'", cn);
                 int n=cmd2.ExecuteNonQuery();
                    if(n==1)
                    { 
                    for (int i = 0; i < com.docs.Count; i++)
                    {
                        n = 0;
                        str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.eascm_docs(doc_id, doc_name,doc_desc, doc_path, doc_type_code,creation_date)");
                        str.Append("Values('" + com.complaint_id + "','" + com.docs[i].doc_name + "', '" + com.docs[i].description + "','" + com.docs[i].doc_path + "','CGR','" + DateTime.Now.ToString("dd-MM-yyyy") + "')");
                        NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                        n = cmd3.ExecuteNonQuery();
                    }

                    }

                    val = "0";

                }
                catch (Exception ex)
                {

                    throw (ex);
                }
                return val;
            }
        }

        public static string StatusUpdate(Complaint com)
        {
            string val = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    StringBuilder str = new StringBuilder();
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.complaint SET complainant_status='" + com.complaint_status + "'  WHERE complaint_no='" + com.complaint_no + "'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();
                    val = "0";
                }
                catch (Exception ex)
                {

                    throw (ex);
                }
                return val;
            }
        }


        public static int GetExistsData(string tablename, string column, int value)
        {
            int value1 = 0;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                        cmd = new NpgsqlCommand("select count(1) from exciseautomation." + tablename + " where " + column + "='" + value + "'", cn);
                    string re = cmd.ExecuteScalar().ToString();
                    if (re == "1")
                    {
                        value1 = 1;
                        _log.Info("Get Existing data Success :" + tablename);
                    }
                    else
                    {
                        if (re != "")
                            value1 = Convert.ToInt32(re);
                        _log.Info("Get Existing data Fail :" + tablename);
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Get Existing data Fail :" + tablename + "-" + ex.Message);
                    value1 = 0;
                }
            }
            return value1;
        }

        //public static List<VAT_Master> GetVat(string userid)
        //{
        //    List<VAT_Master> vat = new List<VAT_Master>();
        //    using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
        //    {
        //        cn.Open();
        //        try
        //        {
        //            NpgsqlCommand cmd = new NpgsqlCommand("select vat_name,vat_code from exciseautomation.vat_master where user_id='" + userid + "'", cn);
        //            NpgsqlDataReader dr = cmd.ExecuteReader();
        //            if (dr.HasRows)
        //            {
        //                vat = new List<VAT_Master>();
        //                while (dr.Read())
        //                {
        //                    VAT_Master vats = new VAT_Master();
        //                    vats.vat_name = dr["vat_name"].ToString();
        //                    vats.vat_code = dr["vat_code"].ToString();
        //                    vats.user_id = userid;
        //                    vat.Add(vats);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw (ex);
        //        }
        //        return vat;
        //    }
        //}

        //public static List<VAT_Master> GetVatAvilQty(string vatcode)
        //{
        //    List<VAT_Master> vat = new List<VAT_Master>();
        //    VAT_Master vats = new VAT_Master();
        //    using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
        //    {
        //        cn.Open();
        //        try
        //        {
        //            NpgsqlCommand cmd = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where  vat_code ='" + vatcode + "' ", cn);
        //            NpgsqlDataReader dr = cmd.ExecuteReader();
        //            if (dr.HasRows)
        //            {
        //                vat = new List<VAT_Master>();
        //                while (dr.Read())
        //                {
        //                    vats.vat_availablecapacity = Convert.ToDouble(dr["vat_availablecapacity"].ToString());
        //                    vat.Add(vats);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw (ex);
        //        }
        //        return vat;
        //    }
        //}

        public static Complaint GetDetails( int complaint_id)
        {

            Complaint com = new Complaint();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select * from  exciseautomation.complaint WHERE complaint_id='"+complaint_id+"'", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();

                    foreach (DataRow dr in dt.Rows)
                    {
                        com.complaint_id = Convert.ToInt32(dr["complaint_id"].ToString());
                        com.complaint_no = Convert.ToInt32(dr["complaint_no"].ToString());
                        com.creation_date = Convert.ToDateTime(dr["creation_date"].ToString()).ToString("dd-MM-yyyy");
                        com.complainant_name = dr["complainant_name"].ToString();
                        com.email_id = dr["email_id"].ToString();
                        com.mobile_no = Convert.ToDouble(dr["contact_no"].ToString());
                        com.complaint_type = dr["complaint_type"].ToString();
                        com.record_status = dr["record_status"].ToString();
                        com.address = dr["address"].ToString();
                        com.complaint_details = dr["complaint_details"].ToString();
                        com.state = dr["state"].ToString();
                        com.district = dr["district"].ToString();
                        com.thana = dr["thana"].ToString();
                        com.village= dr["village"].ToString();
                        com.landmark = dr["landmark"].ToString();
                        //dispatch.user_id = userid;
                        com.otp = dr["otp"].ToString();

                        using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.eascm_docs where doc_id='" + com.complaint_id + "' and doc_type_code='CGR'  order by eascm_docs_id", cn))
                        {
                            cmd1.CommandType = System.Data.CommandType.Text;
                            //cmd.Parameters.AddWithValue("@UserID", userid);
                            NpgsqlDataReader dr2 = cmd1.ExecuteReader();
                            com.docs = new List<EASCM_DOCS>();
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
                                    com.docs.Add(doc);
                                }

                            }
                        }

                    }


                }

                catch (Exception ex)
                {
                    throw (ex);
                }
                return com;
            }
        }


        //public static string Approve(DailyDispatchClosure DDC)
        //{
        //    string VAL = "";
        //    using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
        //    {
        //        cn.Open();
        //        NpgsqlTransaction trn;
        //        trn = cn.BeginTransaction();
        //        try
        //        {

        //            StringBuilder str = new StringBuilder();
        //            str.Append("update exciseautomation.dailydispatchclosure set  record_status='" + DDC.record_status + "' where dailydispatchclosure_id ='" + DDC.dailydispatchclosure_id + "' and financial_year='" + DDC.financial_year + "'");
        //            NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
        //            int n = cmd.ExecuteNonQuery();
        //            if (DDC.record_status == "A")
        //            {
        //                NpgsqlCommand cmd6 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + DDC.from_dispatchvat + "' and party_code='" + DDC.party_code + "'", cn);
        //                double k = Convert.ToDouble(cmd6.ExecuteScalar());
        //                double v = Convert.ToDouble(k) - (Convert.ToDouble(DDC.dispatchqty) + Convert.ToDouble(DDC.dec_blending) + Convert.ToDouble(DDC.dec_racking) + Convert.ToDouble(DDC.dec_reduction) + Convert.ToDouble(DDC.dec_wastage)) + Convert.ToDouble(DDC.txtIncreaseBLInOperation) + Convert.ToDouble(DDC.IncreaseBLByGroging);
        //                NpgsqlCommand cmd7 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + v + "'  where vat_code='" + DDC.from_dispatchvat + "' and party_code='" + DDC.party_code + "'", cn);
        //                cmd7.ExecuteNonQuery();
        //                DDC.record_status = "Approved By Bond Officer";
        //            }
        //            else
        //            {
        //                NpgsqlCommand cmd8 = new NpgsqlCommand("update exciseautomation.pass set record_status='I' where dispatch_date='" + DDC.closure_date + "' and to_dispatch_vat='" + DDC.from_dispatchvat + "' and financial_year='" + DDC.financial_year + "'", cn);
        //                cmd8.ExecuteNonQuery();
        //                //NpgsqlCommand cmd9 = new NpgsqlCommand("update exciseautomation.dailydispatchclosure set attribute1='" + v + "' where closure_date='" + dispatch.closure_date + "' and from_dispatchvat='" + dispatch.from_dispatchvat + "'", cn);
        //                //cmd9.ExecuteNonQuery();
        //                //NpgsqlCommand cmd1 = new NpgsqlCommand("UPDATE exciseautomation.storage_dispatch SET moved_to_nextstage ='N' where receipt_date='" + DDC.closure_date + "' and to_dispatchvat='" + DDC.from_dispatchvat + "'", cn);
        //                //cmd1.ExecuteNonQuery();
        //                DDC.record_status = "Rejected By Bond Officer";
        //            }
        //            str = new StringBuilder();
        //            str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
        //            str.Append("'" + DDC.dailydispatchclosure_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy") + "','DDC','" + DDC.record_status + "','" + DDC.remarks + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DDC.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DDC.user_id + "','" + DDC.financial_year + "','" + DDC.party_code + "')");
        //            cmd = new NpgsqlCommand(str.ToString(), cn);
        //            n = cmd.ExecuteNonQuery();
        //            VAL = "0";
        //            trn.Commit();
        //            cn.Close();

        //        }
        //        catch (Exception ex1)
        //        {
        //            trn.Rollback();


        //        }

        //    }
        //    return VAL;
        //}


    }
}
