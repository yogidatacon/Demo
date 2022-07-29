using NLog.Fluent;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using Usermngt.Entities;
using Usermngt.Entities.CaseMgmt;

namespace Usermngt.DAL
{
    #region DL_cm_seiz_BasicIformation
    public class DL_cm_seiz_BasicIformation
    {
        public static bool InsertSeiz_BasicIformation(cm_seiz_BasicIformation obj)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    NpgsqlTransaction trn;
                    trn = cn.BeginTransaction();
                    try
                    {
                        ///Insert into Seizure table
                        StringBuilder str = new StringBuilder();
                        string tableName = "exciseautomation.seizure";
                        int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.seizure", "seizureno").ToString()) + 1;
                        obj.seizureno = max;
                        obj.basicinfo_id = max;
                        string columnNames = "raidby,seizureno,  lastmodified_date, user_id, creation_date, record_status, record_deleted,district_code";                        
                        string input =obj.raidby+"','"+ obj.seizureno + "','" + obj.lastmodified_date + "','" + obj.user_id + "','" + DateTime.Now.ToShortDateString() + "','" + obj.record_status + "','" + obj.record_deleted+"','"+obj.district_code;

                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            value = true;

                            tableName = "exciseautomation.seizure_basicinfo";
                            //string columnNames = "seizureno, recoverytype, recoveryname, manualseizureno, raid_date, raid_time, raid_location, division_code, district_code, thana_code, latitude, longitude, ipaddress, lastmodified_date, user_id, creation_date, record_status, record_deleted, remarks";                           
                            //input = obj.seizureno + "','" + obj.basicinfo_id + "','" + obj.lastmodified_date + "','" + obj.user_id + "','" + _creation_date + "','" + obj.record_status + "','" + obj.record_deleted;
                            if (obj.raid_time != "" && obj.raid_time!=null)
                            {
                                columnNames = "raidby,seizureno, raid_date,raid_time,raid_location,thana_code,recoverytype,recoveryname,manualseizureno,latitude,longitude,remarks, lastmodified_date, user_id, creation_date, record_status, record_deleted,district_code,division_code,ipaddress";

                                input = obj.raidby + "','" + obj.seizureno + "','" + obj.raid_date + "','" + obj.raid_time + "','" + obj.raid_location + "','" + obj.thana_code + "','" + obj.recoverytype + "','" + obj.recoveryname + "','" + obj.manualseizureno + "','" + obj.latitude + "','" + obj.longitude + "','" + obj.remarks + "','" + obj.lastmodified_date + "','" + obj.user_id + "','" + DateTime.Now.ToShortDateString() + "','" + obj.record_status + "','" + obj.record_deleted + "','" + obj.district_code + "','" + obj.division_code + "','" + obj.ipaddress;
                            }
                            else
                            {
                                columnNames = "raidby,seizureno, raid_date,raid_location,thana_code,recoverytype,recoveryname,manualseizureno,latitude,longitude,remarks, lastmodified_date, user_id, creation_date, record_status, record_deleted,district_code,division_code,ipaddress";

                                input = obj.raidby + "','" + obj.seizureno + "','" + obj.raid_date + "','" + obj.raid_location + "','" + obj.thana_code + "','" + obj.recoverytype + "','" + obj.recoveryname + "','" + obj.manualseizureno + "','" + obj.latitude + "','" + obj.longitude + "','" + obj.remarks + "','" + obj.lastmodified_date + "','" + obj.user_id + "','" + DateTime.Now.ToShortDateString() + "','" + obj.record_status + "','" + obj.record_deleted + "','" + obj.district_code + "','" + obj.division_code + "','" + obj.ipaddress;
                            }


                                InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                            cmd = new NpgsqlCommand(InsertQuery, cn);
                            n = cmd.ExecuteNonQuery();
                            if (n == 1)
                            {
                                for (int i = 0; i < obj.docs.Count; i++)
                                {
                                    n = 0;
                                    str = new StringBuilder();
                                    str.Append("INSERT INTO exciseautomation.seizure_docs(raidby,seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date)");
                                    str.Append("Values('" + obj.raidby + "','" + obj.seizureno + "','" + max + "','" + obj.docs[i].doc_name + "', '" + obj.docs[i].description + "','" + obj.docs[i].doc_path + "','BID','" + obj.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "')");
                                    // string cmd1=("INSERT INTO exciseautomation.seizure_docs(seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date)Values('" + obj.seizureno + "','" + max + "','" + obj.docs[i].doc_name + "', '" + obj.docs[i].description + "','" + obj.docs[i].doc_path + "','BID','" + obj.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "')");
                                    //str.Append("Values('" + obj.seizureno + "','" + max + "','" + obj.docs[i].doc_name + "', '" + obj.docs[i].description + "','" + obj.docs[i].doc_path + "','BID','" + obj.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "')");
                                    //NpgsqlCommand cmd4 = new NpgsqlCommand(cmd1, cn);
                                    NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                                    n = cmd3.ExecuteNonQuery();
                                }
                                if (obj.recoverytype == "IC" && obj.user_id.Contains("thana_"))
                                {
                                    try
                                    {
                                        cmd = new NpgsqlCommand("update exciseautomation.complaint_data set seizureno='" + obj.seizureno + "' where comid='" + obj.recoveryname.Trim() + "'  ", cn);
                                        cmd.ExecuteNonQuery();
                                    }
                                    catch
                                    {

                                    }
                                }
                                value = true;
                                trn.Commit();
                                //_log.Info("Sugarcanepurchase Insertion Success:" + scp.sugarcanepurchase_id + '-' + scp.party_code);
                                cn.Close();

                                //value = true;
                                //trn.Commit();
                            }
                            else
                            {
                                value = false;
                                trn.Rollback();
                            }
                        }
                        else
                        {
                            trn.Rollback();
                            value = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        // trn.Rollback();
                        //Console.Write(ex);
                        Console.WriteLine("Rollback Exception Type", ex.StackTrace.ToString());
                        value = false;
                    }
                }
            }
            return value;
        }

        public static List<cm_seiz_BasicIformation> GetSubmittedSeizureListByALL(string rb)
        {
            List<cm_seiz_BasicIformation> lstObj = new List<cm_seiz_BasicIformation>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select t.division_code,t.district_code,thana_name,sb.* from exciseautomation.seizure_basicinfo sb INNER JOIN exciseautomation.thana_master t on sb.thana_code = t.thana_code  where sb.record_status='Y'  order by seizureno desc ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_seiz_BasicIformation>();
                            while (dr.Read())
                            {
                                cm_seiz_BasicIformation record = new cm_seiz_BasicIformation();
                                string value = dr["seizureno"]?.ToString() ?? string.Empty;
                                if (!string.IsNullOrEmpty(value))
                                {
                                    record.seizureno = Convert.ToInt32(value);
                                }
                                else record.seizureno = 0;

                                try
                                {
                                    //var value = dr["raid_date"].ToString();
                                    record.raid_date = Convert.ToDateTime(dr["raid_date"]).ToString("dd-MM-yyyy");
                                    record.raid_time = dr["raid_time"].ToString();
                                }
                                catch (Exception)
                                {

                                    //throw;
                                }

                                record.raid_location = dr["raid_location"].ToString();
                                record.thanaName = dr["thana_name"].ToString();
                                record.thana_code = dr["thana_code"].ToString();
                                record.district_code = dr["district_code"].ToString();
                                record.division_code = dr["division_code"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.user_id = dr["user_id"].ToString();
                                record.raidby = dr["raidby"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    Log.Info("Get Seizure List Success");
                }
                catch (Exception ex)
                {
                    Log.Info("Get Seizure List Error :" + ex.Message);
                }

            }
            return lstObj;
        }

        public static List<cm_seiz_BasicIformation> GetUnSubmittedSeizureAllList(string rb)
        {
            List<cm_seiz_BasicIformation> lstObj = new List<cm_seiz_BasicIformation>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(" select t.division_code,t.district_code,thana_name,sb.* from exciseautomation.seizure_basicinfo sb INNER JOIN exciseautomation.thana_master t on sb.thana_code = t.thana_code  where sb.record_status='N'  order by seizureno desc ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_seiz_BasicIformation>();
                            while (dr.Read())
                            {
                                cm_seiz_BasicIformation record = new cm_seiz_BasicIformation();
                                string value = dr["seizureno"]?.ToString() ?? string.Empty;
                                if (!string.IsNullOrEmpty(value))
                                {
                                    record.seizureno = Convert.ToInt32(value);
                                }
                                else record.seizureno = 0;

                                try
                                {
                                    //var value = dr["raid_date"].ToString();
                                    record.raid_date = Convert.ToDateTime(dr["raid_date"]).ToString("dd-MM-yyyy");
                                    record.raid_time = dr["raid_time"].ToString();
                                }
                                catch (Exception)
                                {

                                    //throw;
                                }

                                record.raid_location = dr["raid_location"].ToString();
                                record.thanaName = dr["thana_name"].ToString();
                                record.thana_code = dr["thana_code"].ToString();
                                record.district_code = dr["district_code"].ToString();
                                record.division_code = dr["division_code"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.user_id = dr["user_id"].ToString();
                                record.raidby = dr["raidby"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    Log.Info("Get Seizure List Success");
                }
                catch (Exception ex)
                {
                    Log.Info("Get Seizure List Error :" + ex.Message);
                }

            }
            return lstObj;
        }

        public static List<Call_Complaints> GetComplaintList(string userid)
        {
            List<Call_Complaints> lstObj = new List<Call_Complaints>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select comid ,v_issue,comsource ,comname ,phone, accperson,complaintstatus,nearby,prfirno,seizureno,thana_mst_code from exciseautomation.view_callcentercomplaint_list where thana_mst_code='" + userid.Replace("thana_","")+ "'  order by comid  ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<Call_Complaints>();
                            while (dr.Read())
                            {
                                Call_Complaints record = new Call_Complaints();

                                try
                                {
                                    record.comid = dr["comid"].ToString();
                                    record.comname = dr["comname"].ToString();
                                    record.v_issue = dr["v_issue"].ToString();
                                    record.comsource = dr["comsource"].ToString();
                                    record.phone = dr["phone"].ToString();
                                    record.accperson = dr["accperson"].ToString();
                                    record.complaintstatus = dr["complaintstatus"].ToString();
                                    record.nearby = dr["nearby"].ToString();
                                    record.seizureno = dr["seizureno"].ToString();
                                    record.prfirno = dr["prfirno"].ToString();
                                   
                                    record.thana_mst_code = dr["thana_mst_code"].ToString();

                                }
                                catch (Exception)
                                {

                                    //throw;
                                }
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    Log.Info("Get Seizure List Success");
                }
                catch (Exception ex)
                {
                    Log.Info("Get Seizure List Error :" + ex.Message);
                }

            }
            return lstObj;
        }

        public static cm_seizure GetSeizureDetails(string seizureNo)
        {
            string[] dept = seizureNo.Split('&');
            seizureNo = dept[0];
            string d = "";
            if (dept[1] == "Excise" || dept[1] == "E")
                d = "E";
            else
                d = "P";
            cm_seizure record = new cm_seizure();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select seizureno,seizure_stage_code,user_id,record_status from exciseautomation.seizure  where seizureno=" + seizureNo+" and raidby='"+d+"'", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            record = new cm_seizure();
                            while (dr.Read())
                            {
                                record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                                if(dr["seizure_stage_code"].ToString()!=""&& dr["seizure_stage_code"].ToString()!=null)
                                record.seizure_stage_code = Convert.ToInt32(dr["seizure_stage_code"].ToString());
                                record.user_id = dr["user_id"].ToString();
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex);
                    Console.WriteLine("Rollback Exception Type", ex.StackTrace.ToString());
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }
            
            }
            return record;
        }

        public static List<cm_seiz_BasicIformation> GetRaidList()
        {
            List<cm_seiz_BasicIformation> lstObj = new List<cm_seiz_BasicIformation>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from (select distinct  to_char(raid_date,'dd-MM-yyyy')  as ddt   from seizure_basicinfo where record_status='N'  )order by cast(ddt as date)", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_seiz_BasicIformation>();
                            while (dr.Read())
                            {
                                cm_seiz_BasicIformation record = new cm_seiz_BasicIformation();

                                try
                                {                                    
                                    record.raid_date = Convert.ToDateTime(dr["ddt"]).ToString("dd-MM-yyyy");
                                }
                                catch (Exception)
                                {

                                    //throw;
                                }
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    Log.Info("Get Seizure List Success");
                }
                catch (Exception ex)
                {
                    Log.Info("Get Seizure List Error :" + ex.Message);
                }

            }
            return lstObj;
        }

        public static List<cm_seiz_BasicIformation> GetThanaList()
        {
            List<cm_seiz_BasicIformation> lstObj = new List<cm_seiz_BasicIformation>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT distinct thana_name, thana_code,district_code FROM exciseautomation.thana_master   order by thana_name ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_seiz_BasicIformation>();
                            while (dr.Read())
                            {
                                cm_seiz_BasicIformation record = new cm_seiz_BasicIformation();

                                try
                                {
                                    record.thanaName = dr["thana_name"].ToString();
                                    record.thana_code = dr["thana_code"].ToString();
                                    record.district_code = dr["district_code"].ToString();
                                }
                                catch (Exception)
                                {

                                    //throw;
                                }
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    Log.Info("Get Seizure List Success");
                }
                catch (Exception ex)
                {
                    Log.Info("Get Seizure List Error :" + ex.Message);
                }

            }
            return lstObj;
        }

        public static List<cm_seiz_BasicIformation> GetUnSubmittedSeizureList(string username)
        {
            string[] val = username.Split('&');
            string dept = "";
            string column = "";
            if (username.Contains("thana_"))
            {
                column = "sb.user_id='" + val[0] + "'";
                dept = "P";
            }
            else
            {
                column = "sb.district_code='" + val[1] + "'";
                dept = val[0].Substring(0, 1).ToUpper();
            }
            if (dept.ToUpper() != "E")
                dept = "P";
            List<cm_seiz_BasicIformation> lstObj = new List<cm_seiz_BasicIformation>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(" select t.division_code,t.district_code,thana_name,sb.* from exciseautomation.seizure_basicinfo sb INNER JOIN exciseautomation.thana_master t on sb.thana_code = t.thana_code  where " + column+" and sb.record_status='N' order by seizureno desc ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_seiz_BasicIformation>();
                            while (dr.Read())
                            {
                                cm_seiz_BasicIformation record = new cm_seiz_BasicIformation();
                                string value = dr["seizureno"]?.ToString() ?? string.Empty;
                                if (!string.IsNullOrEmpty(value))
                                {
                                    record.seizureno = Convert.ToInt32(value);
                                }
                                else record.seizureno = 0;

                                try
                                {
                                    //var value = dr["raid_date"].ToString();
                                    record.raid_date = Convert.ToDateTime(dr["raid_date"]).ToString("dd-MM-yyyy");
                                    record.raid_time = dr["raid_time"].ToString();
                                }
                                catch (Exception)
                                {

                                    //throw;
                                }

                                record.raid_location = dr["raid_location"].ToString();
                                record.thanaName = dr["thana_name"].ToString();
                                record.thana_code= dr["thana_code"].ToString();
                                record.district_code = dr["district_code"].ToString();
                                record.division_code= dr["division_code"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.user_id = dr["user_id"].ToString();
                                record.raidby = dr["raidby"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    Log.Info("Get Seizure List Success");
                }
                catch (Exception ex)
                {
                    Log.Info("Get Seizure List Error :" + ex.Message);
                }

            }
            return lstObj;
        }

        public static cm_seiz_BasicIformation ViewDetails(string seizureNo)
        {
            string[] dept = seizureNo.Split('&');
            seizureNo = dept[0];
            string d = "";
            if (dept[1] == "Excise" || dept[1] == "E")
                d = "E";
            else
                d = "P";
            cm_seiz_BasicIformation record = new cm_seiz_BasicIformation();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    if (dept[2] == "com")
                        cmd = new NpgsqlCommand("select thana_name,sb.* from exciseautomation.seizure_basicinfo sb INNER JOIN exciseautomation.thana_master t on sb.thana_code = t.thana_code  where seizureno='" + seizureNo + "' ", cn);
                    else
                        cmd = new NpgsqlCommand("select thana_name,sb.* from exciseautomation.seizure_basicinfo sb INNER JOIN exciseautomation.thana_master t on sb.thana_code = t.thana_code  where seizureno='" + seizureNo + "' and raidby='" + d + "'", cn);
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr1 = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr1);
                        dr1.Close();

                        
                            record = new cm_seiz_BasicIformation();
                            //while (dr.Read())
                            foreach (DataRow dr in dt.Rows)
                            {

                                record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                                record.raid_date = Convert.ToDateTime(dr["raid_date"]).ToString("dd-MM-yyyy");
                                record.raid_time = dr["raid_time"].ToString();
                                record.raid_location = dr["raid_location"].ToString();
                                record.thana_code = dr["thana_code"].ToString();
                                record.recoverytype = dr["recoverytype"].ToString();
                                record.recoveryname = dr["recoveryname"].ToString();
                                record.manualseizureno = dr["manualseizureno"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.latitude = dr["latitude"].ToString();
                                record.longitude = dr["longitude"].ToString();
                                record.remarks = dr["remarks"].ToString();
                                record.user_id = dr["user_id"].ToString();
                            record.raidby = dr["raidby"].ToString();
                                record.docs = new List<Seizure_Docs>();
                                using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.Seizure_Docs where seizureno='" + record.seizureno + "' and doc_type_code='BID' and raidby='"+record.raidby+"' order by seizure_docs_id", cn))
                                {
                                    cmd1.CommandType = System.Data.CommandType.Text;
                                    //cmd.Parameters.AddWithValue("@UserID", userid);
                                    NpgsqlDataReader dr2 = cmd1.ExecuteReader();

                                    if (dr2.HasRows)
                                    {

                                        while (dr2.Read())
                                        {
                                            Seizure_Docs doc = new Seizure_Docs();
                                            doc.seizure_docs_id = dr2["seizure_docs_id"].ToString();
                                            doc.doc_id = dr2["doc_id"].ToString();
                                            doc.doc_name = dr2["doc_Name"].ToString();
                                            doc.description = dr2["doc_desc"].ToString();
                                            doc.doc_path = dr2["doc_path"].ToString();
                                            record.docs.Add(doc);
                                        }

                                    }
                                    dr2.Close();
                                }

                                //ads.Add(ad);

                            }

                        
                    
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return record;
        }

        //Todo : Update the query and binding
        public static List<cm_seiz_BasicIformation> GetUnsubmittedList(string _thana, string _raidDate, string _seizureNo)
        {
            List<cm_seiz_BasicIformation> lstObj = new List<cm_seiz_BasicIformation>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    string str = "";
                    if (_raidDate == "" && _thana!= "Select")
                        str = "select thana_name,t.thana_code as thana_code,sb.* from exciseautomation.seizure_basicinfo sb INNER JOIN exciseautomation.thana_master t on sb.thana_code = t.thana_code where sb.record_status='N' and t.thana_code='" + _thana + "'";
                  if (_thana == "Select" && _raidDate != "")
                        str = "select thana_name, t.thana_code as thana_code,sb.* from exciseautomation.seizure_basicinfo sb INNER JOIN exciseautomation.thana_master t on sb.thana_code = t.thana_code where sb.record_status='N' and raid_date = '" + _raidDate + "' ";
                     if (_thana == "Select" && _seizureNo != "")
                        str = "select thana_name, t.thana_code as thana_code,sb.* from exciseautomation.seizure_basicinfo sb INNER JOIN exciseautomation.thana_master t on sb.thana_code = t.thana_code where sb.record_status='N' and sb.seizureno = '" + _seizureNo+" ";
                    if (_seizureNo != "" && _raidDate == "")
                        str = "select thana_name, t.thana_code as thana_code,sb.* from exciseautomation.seizure_basicinfo sb INNER JOIN exciseautomation.thana_master t on sb.thana_code = t.thana_code where sb.record_status='N' and sb.seizureno = '" + _seizureNo + " ";
                    if (_thana != "Select" && _seizureNo != "")
                        str = "select thana_name, t.thana_code as thana_code,sb.* from exciseautomation.seizure_basicinfo sb INNER JOIN exciseautomation.thana_master t on sb.thana_code = t.thana_code where sb.record_status='N' and  sb.seizureno = '" + _seizureNo + "' and t.thana_code='" + _thana + "' ";
                   if (_seizureNo != "" && _raidDate != "")
                        str = "select thana_name, t.thana_code as thana_code,sb.* from exciseautomation.seizure_basicinfo sb INNER JOIN exciseautomation.thana_master t on sb.thana_code = t.thana_code where sb.record_status='N' and  sb.seizureno = '" + _seizureNo + "' and  raid_date='" + _raidDate + "'";
                    if (_thana != "Select" && _raidDate != "")
                        str = "select thana_name,t.thana_code as thana_code,sb.* from exciseautomation.seizure_basicinfo sb INNER JOIN exciseautomation.thana_master t on sb.thana_code = t.thana_code where sb.record_status='N' and raid_date='" + _raidDate + "' and t.thana_code='" + _thana + "'";
                    if (_thana != "Select" && _raidDate != "" && _seizureNo != "")
                        str = "select thana_name, t.thana_code as thana_code,sb.* from exciseautomation.seizure_basicinfo sb INNER JOIN exciseautomation.thana_master t on sb.thana_code = t.thana_code where sb.record_status='N' and raid_date = '" + _raidDate + "' and sb.seizureno = '" + _seizureNo + "'  and t.thana_code='" + _thana + "' ";
                   if (_thana == "Select" && _raidDate == "" && _seizureNo != "")
                        str = "select thana_name, t.thana_code as thana_code,sb.* from exciseautomation.seizure_basicinfo sb INNER JOIN exciseautomation.thana_master t on sb.thana_code = t.thana_code where sb.record_status='N' and sb.seizureno = '" + _seizureNo + "' ";
                 
                    using (NpgsqlCommand cmd = new NpgsqlCommand(str, cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_seiz_BasicIformation>();
                            while (dr.Read())
                            {
                                cm_seiz_BasicIformation record = new cm_seiz_BasicIformation();
                                record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());

                                try
                                {
                                    var value = dr["raid_date"].ToString();
                                    record.raid_date = Convert.ToDateTime(dr["raid_date"]).ToString("dd-MM-yyyy"); 
                                    record.raid_time = dr["raid_time"].ToString();
                                }
                                catch (Exception)
                                {

                                    //throw;
                                }

                                record.raid_location = dr["raid_location"].ToString();
                                record.thanaName = dr["thana_name"].ToString();
                                record.thana_code = dr["thana_code"].ToString();
                                record.user_id = dr["user_id"].ToString();
                                record.district_code = dr["district_code"].ToString();
                                record.raidby= dr["raidby"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }

        public static List<cm_seiz_BasicIformation> GetSubmittedSeizureListByName(string username)
        {
            string[] val = username.Split('&');
            string dept = "";
            string column = "";
            if (username.Contains("thana_"))
            {
                column = "sb.user_id='" + val[0] + "'";
                dept ="P";
            }
            else
            {
                column = "sb.district_code='" + val[1]+"'";
                dept = val[0].Substring(0, 1).ToUpper();
            }
            if (dept.ToUpper() != "E")
                dept = "P";
            List<cm_seiz_BasicIformation> lstObj = new List<cm_seiz_BasicIformation>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    string quiery = "";
                    if(val[0]=="com")
                    {
                        quiery = "select distinct f.prfirno,t.division_code,t.district_code,thana_name,sb.* from exciseautomation.seizure_basicinfo sb INNER JOIN exciseautomation.thana_master t on sb.thana_code = t.thana_code left join exciseautomation.seizure_fir f on f.seizureno=sb.seizureno and sb.raidby=f.raidby   where   sb.record_status='Y' order by seizureno desc ";
                    }
                    else
                    {
                    quiery = "select distinct f.prfirno,t.division_code,t.district_code,thana_name,sb.* from exciseautomation.seizure_basicinfo sb INNER JOIN exciseautomation.thana_master t on sb.thana_code = t.thana_code left join exciseautomation.seizure_fir f on f.seizureno=sb.seizureno and sb.raidby=f.raidby   where " + column + " and sb.raidby='" + dept + "' and  sb.record_status='Y' order by seizureno desc ";
                    }
                    using (NpgsqlCommand cmd = new NpgsqlCommand(quiery, cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_seiz_BasicIformation>();
                            while (dr.Read())
                            {
                                cm_seiz_BasicIformation record = new cm_seiz_BasicIformation();
                                string value = dr["seizureno"]?.ToString() ?? string.Empty;
                                if (!string.IsNullOrEmpty(value))
                                {
                                    record.seizureno = Convert.ToInt32(value);
                                }
                                else record.seizureno = 0;

                                try
                                {
                                    //var value = dr["raid_date"].ToString();
                                    record.raid_date = Convert.ToDateTime(dr["raid_date"]).ToString("dd-MM-yyyy");
                                    record.raid_time = dr["raid_time"].ToString();

                                }
                                catch (Exception)
                                {

                                    //throw;
                                }

                                record.raid_location = dr["raid_location"].ToString();
                                record.thanaName = dr["thana_name"].ToString();
                                record.thana_code = dr["thana_code"].ToString();
                                record.district_code = dr["district_code"].ToString();
                                record.division_code = dr["division_code"].ToString();
                                record.user_id = dr["user_id"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.raidby = dr["raidby"].ToString();
                                record.prfirno = dr["prfirno"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }

        public static List<cm_seiz_BasicIformation> GetSubmittedSeizureList(string _seizureNo,string prfirno, string Thana)
        {
            string quiery = "";
            if (_seizureNo != "" && prfirno != "" && Thana != "")
            {
                quiery = "select DISTINCT f.prfirno,thana_name,sb.* from exciseautomation.seizure_basicinfo sb INNER JOIN exciseautomation.thana_master t on sb.thana_code = t.thana_code left join exciseautomation.seizure_fir f on f.seizureno=sb.seizureno  and sb.raidby=f.raidby  where sb.record_status='Y'  and sb.seizureno = " + _seizureNo + " and t.thana_code='" + Thana + "' and f.prfirno ilike '%" + prfirno + "%'";
            }
            else if (_seizureNo == "" && prfirno == "" && Thana != "")
            {
                quiery = "select DISTINCT f.prfirno,thana_name,sb.* from exciseautomation.seizure_basicinfo sb INNER JOIN exciseautomation.thana_master t on sb.thana_code = t.thana_code left join exciseautomation.seizure_fir f on f.seizureno=sb.seizureno  and sb.raidby=f.raidby  where sb.record_status='Y'  and t.thana_code='" + Thana + "'";
            }
            else if (_seizureNo != "" && Thana != "" && prfirno == "")
            {
                quiery = "select DISTINCT f.prfirno,thana_name,sb.* from exciseautomation.seizure_basicinfo sb INNER JOIN exciseautomation.thana_master t on sb.thana_code = t.thana_code left join exciseautomation.seizure_fir f on f.seizureno=sb.seizureno  and sb.raidby=f.raidby where sb.record_status='Y' and t.thana_code='" + Thana + "' and sb.seizureno = " + _seizureNo + " ";
            }
            else if (Thana != "" && prfirno != "" && _seizureNo == "")
            {
                quiery = "select DISTINCT f.prfirno,thana_name,sb.* from exciseautomation.seizure_basicinfo sb INNER JOIN exciseautomation.thana_master t on sb.thana_code = t.thana_code left join exciseautomation.seizure_fir f on f.seizureno=sb.seizureno  and sb.raidby=f.raidby  where sb.record_status='Y' and t.thana_code='" + Thana + "' and f.prfirno ilike '%" + prfirno + "%'";
            }
          else  if (_seizureNo!="" && prfirno=="" && Thana=="")
            {
                quiery = "select DISTINCT f.prfirno,thana_name,sb.* from exciseautomation.seizure_basicinfo sb INNER JOIN exciseautomation.thana_master t on sb.thana_code = t.thana_code left join exciseautomation.seizure_fir f on f.seizureno=sb.seizureno  and sb.raidby=f.raidby where sb.record_status='Y' and  sb.seizureno = " + _seizureNo;
            }
            else if (_seizureNo != "" && prfirno != "" && Thana == "")
            {
                quiery = "select DISTINCT f.prfirno,thana_name,sb.* from exciseautomation.seizure_basicinfo sb INNER JOIN exciseautomation.thana_master t on sb.thana_code = t.thana_code left join exciseautomation.seizure_fir f on f.seizureno=sb.seizureno  and sb.raidby=f.raidby  where sb.record_status='Y'  and sb.seizureno = " + _seizureNo + " and f.prfirno ilike '%" + prfirno + "%'";
            }
          else  if (_seizureNo == "" && prfirno != "" && Thana == "")
            {
                quiery = "select DISTINCT f.prfirno,thana_name,sb.* from exciseautomation.seizure_basicinfo sb INNER JOIN exciseautomation.thana_master t on sb.thana_code = t.thana_code left join exciseautomation.seizure_fir f on f.seizureno=sb.seizureno  and sb.raidby=f.raidby  where sb.record_status='Y'  and f.prfirno ilike '%" + prfirno+"%'";
            }
            //if (_seizureNo == "" && Thana != "")
            //{
            //    quiery = "select DISTINCT f.prfirno,thana_name,sb.* from exciseautomation.seizure_basicinfo sb INNER JOIN exciseautomation.thana_master t on sb.thana_code = t.thana_code left join exciseautomation.seizure_fir f on f.seizureno=sb.seizureno  where sb.record_status='Y'  and t.thana_code='" + Thana + "'";
            //}
            //if (Thana != "" && prfirno == "")
            //{
            //    quiery = "select DISTINCT f.prfirno,thana_name,sb.* from exciseautomation.seizure_basicinfo sb INNER JOIN exciseautomation.thana_master t on sb.thana_code = t.thana_code left join exciseautomation.seizure_fir f on f.seizureno=sb.seizureno  where sb.record_status='Y'  and t.thana_code='" + Thana + "'";
            //}
          
           
            List<cm_seiz_BasicIformation> lstObj = new List<cm_seiz_BasicIformation>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(quiery, cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_seiz_BasicIformation>();
                            while (dr.Read())
                            {
                                cm_seiz_BasicIformation record = new cm_seiz_BasicIformation();
                                record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());

                                try
                                {
                                    var value = dr["raid_date"].ToString();
                                    record.raid_date = Convert.ToDateTime(dr["raid_date"]).ToString("dd-MM-yyyy");
                                    record.raid_time = dr["raid_time"].ToString();
                                }
                                catch (Exception)
                                {

                                    //throw;
                                }

                                record.raid_location = dr["raid_location"].ToString();
                                record.thanaName = dr["thana_name"].ToString();
                                record.thana_code = dr["thana_code"].ToString();
                                record.district_code = dr["district_code"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.user_id = dr["user_id"].ToString();
                                record.raidby = dr["raidby"].ToString();
                                record.prfirno = dr["prfirno"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }


        public static bool Update_BasicIformation(cm_seiz_BasicIformation cm_obj)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                    cn.Open();
                try
                {
                    StringBuilder str = new StringBuilder();
                    NpgsqlCommand cmd ;
                    if (cm_obj.raid_time==null)
                    cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure_basicinfo SET recoverytype ='" + cm_obj.recoverytype + "', recoveryname ='" + cm_obj.recoveryname + "', manualseizureno ='" + cm_obj.manualseizureno + "', raid_date ='" + cm_obj.raid_date + "', raid_time =null, raid_location ='" + cm_obj.raid_location + "', thana_code ='" + cm_obj.thana_code + "', latitude ='" + cm_obj.latitude + "', longitude ='" + cm_obj.longitude+ "', ipaddress ='" + cm_obj.ipaddress+ "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "', user_id ='" + cm_obj.user_id + "', record_deleted ='" + cm_obj.record_deleted + "', remarks ='" + cm_obj.remarks + "',raidby='"+cm_obj.raidby+ "',division_code='"+cm_obj.division_code+ "',district_code='" + cm_obj.district_code+"' WHERE  seizureno ='" + cm_obj.seizureno + "'", cn);
                    else
                        cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure_basicinfo SET recoverytype ='" + cm_obj.recoverytype + "', recoveryname ='" + cm_obj.recoveryname + "', manualseizureno ='" + cm_obj.manualseizureno + "', raid_date ='" + cm_obj.raid_date + "', raid_time ='" + cm_obj.raid_time + "', raid_location ='" + cm_obj.raid_location + "', thana_code ='" + cm_obj.thana_code + "', latitude ='" + cm_obj.latitude + "', longitude ='" + cm_obj.longitude + "', ipaddress ='" + cm_obj.ipaddress + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "', user_id ='" + cm_obj.user_id + "', record_deleted ='" + cm_obj.record_deleted + "', remarks ='" + cm_obj.remarks + "',raidby='" + cm_obj.raidby + "',division_code='" + cm_obj.division_code + "',district_code='" + cm_obj.district_code + "' WHERE  seizureno ='" + cm_obj.seizureno + "'", cn);
                    int n = cmd.ExecuteNonQuery();

                    NpgsqlCommand cmd_delete_docs = new NpgsqlCommand("delete from  exciseautomation.seizure_docs where seizureno ='" + cm_obj.seizureno + "' and  doc_type_code ='BID' and raidby='"+cm_obj.raidby+"'", cn);
                    int n_DOCS = cmd_delete_docs.ExecuteNonQuery();

                    for (int i = 0; i < cm_obj.docs.Count; i++)
                    {
                        str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.seizure_docs(seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,raidby)");
                        str.Append("Values('" + cm_obj.seizureno + "','" + cm_obj.basicinfo_id  + "','" + cm_obj.docs[i].doc_name + "', '" + cm_obj.docs[i].description + "','" + cm_obj.docs[i].doc_path + "','BID','" + cm_obj.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+cm_obj.raidby+"')");
                        NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                        n = cmd3.ExecuteNonQuery();
                    }
                    if (n == 1)
                    {
                        value = true;
                    }
                    else
                    {
                        value = false;
                    }
                }
                catch (Exception ex)
                {
                    value = false;
                    throw (ex);
                }
            }
            return value;
        }
    }
    #endregion DL_cm_seiz_BasicIformation
}
