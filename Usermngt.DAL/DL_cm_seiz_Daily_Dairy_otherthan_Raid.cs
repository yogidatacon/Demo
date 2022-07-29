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
    public class DL_cm_seiz_Daily_Dairy_otherthan_Raid
    {
        public static List<daily_diary_entry_otherthan_raid> GetList(string user)
        {
            List<daily_diary_entry_otherthan_raid> lstObj = new List<daily_diary_entry_otherthan_raid>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT a.*,b.division_code,b.district_code FROM exciseautomation.daily_dairy_otherthan_raid  a inner join exciseautomation.user_registration b on a.user_id=b.user_id  order by daily_dairy_otherthan_raid_id desc", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<daily_diary_entry_otherthan_raid>();
                            while (dr.Read())
                            {
                                daily_diary_entry_otherthan_raid record = new daily_diary_entry_otherthan_raid();
                                record.daily_dairy_otherthan_raid_id = dr["daily_dairy_otherthan_raid_id"].ToString();
                                record.raid_entry_date = dr["raid_entry_date"].ToString().Substring(0, 10).Replace("/", "-"); ;
                                record.raid_recovery = dr["raid_recovery"].ToString();
                                record.intelligence_gathering = dr["intelligence_gathering"].ToString();
                                record.petrolling = dr["petrolling"].ToString();
                                record.vehicle_check = dr["vehicle_check"].ToString();
                                record.liquor_destruction = dr["liquor_destruction"].ToString();
                                record.witness_appearance_in_court = dr["witness_appearance_in_court"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.district_code = dr["district_code"].ToString();
                                record.division_code = dr["division_code"].ToString();
                                record.user_id = dr["user_id"].ToString();
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

        public static daily_diary_entry_otherthan_raid GetDetails(string id)
        {
            daily_diary_entry_otherthan_raid record = new daily_diary_entry_otherthan_raid();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.daily_dairy_otherthan_raid   where daily_dairy_otherthan_raid_id= '" + id + "' ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr);
                        dr.Close();
                        record = new daily_diary_entry_otherthan_raid();
                        foreach (DataRow dr1 in dt.Rows)
                        {
                            record.daily_dairy_otherthan_raid_id = dr1["daily_dairy_otherthan_raid_id"].ToString();
                            record.raid_entry_date = dr1["raid_entry_date"].ToString().Substring(0, 10).Replace("/", "-"); ;
                            record.raid_recovery = dr1["raid_recovery"].ToString();
                            record.intelligence_gathering = dr1["intelligence_gathering"].ToString();
                            record.petrolling = dr1["petrolling"].ToString();
                            record.vehicle_check = dr1["vehicle_check"].ToString();
                            record.liquor_destruction = dr1["liquor_destruction"].ToString();
                            record.witness_appearance_in_court = dr1["witness_appearance_in_court"].ToString();
                            record.others = dr1["others_"].ToString();
                            record.record_status = dr1["record_status"].ToString();
                            record.meeting = dr1["meeting"].ToString();
                            record.uom_code = dr1["uom_code"].ToString();
                            if(dr1["liquor_quqntity"].ToString()!="" && dr1["liquor_quqntity"].ToString() !=null)
                            record.quantity=Convert.ToDouble(dr1["liquor_quqntity"].ToString());
                            record.user_id = dr1["user_id"].ToString();
                            record.docs = new List<Seizure_Docs>();
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select regexp_split_to_table(exciseautomation.dailydairy_Docs.doc_name, E',') AS doc_name_split,*from exciseautomation.dailydairy_Docs where daily_dairy_id='" + record.daily_dairy_otherthan_raid_id + "' and doc_type_code='DDOE' order by daily_dairy_docs_id", cn))
                            {
                                cmd1.CommandType = System.Data.CommandType.Text;
                                NpgsqlDataReader dr2 = cmd1.ExecuteReader();
                                if (dr2.HasRows)
                                {
                                    while (dr2.Read())
                                    {
                                        Seizure_Docs doc = new Seizure_Docs();
                                        doc.seizure_docs_id = dr2["daily_dairy_docs_id"].ToString();
                                        doc.doc_id = dr2["doc_id"].ToString();
                                        doc.doc_name = dr2["doc_name_split"].ToString();
                                        doc.docs_from = dr2["docs_from"].ToString();
                                        doc.description = dr2["doc_desc"].ToString();
                                        doc.doc_path = dr2["doc_path"].ToString();
                                        record.docs.Add(doc);
                                    }
                                }
                                dr2.Close();
                            }
                        }
                    }

                    cn.Close();
                
                }
                catch (Exception ex)
                {
                   
                }

            }
            return record;
        }

        public static bool Insert(daily_diary_entry_otherthan_raid dd)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    try
                    {
                        string columnNames = "";
                        string input = "";
                        int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.daily_dairy_otherthan_raid", "daily_dairy_otherthan_raid_id").ToString()) + 1;
                        dd.daily_dairy_otherthan_raid_id = max.ToString();
                        string tableName = "exciseautomation.daily_dairy_otherthan_raid";
                        string _creation_date = DateTime.Now.ToString("dd-MM-yyyy");
                        if(dd.liquor_destruction=="Yes")
                        {
                            columnNames = "daily_dairy_otherthan_raid_id, raid_entry_date, intelligence_gathering, petrolling, vehicle_check, liquor_destruction, witness_appearance_in_court, others_, raid_recovery,  lastmodified_date, creation_date, user_id, record_status,uom_code,liquor_quqntity,meeting";
                            input = "" + max + "','" + dd.raid_entry_date + "','" + dd.intelligence_gathering + "','" + dd.petrolling + "','" + dd.vehicle_check + "','" + dd.liquor_destruction + "','" + dd.witness_appearance_in_court + "','" + dd.others + "','" + dd.raid_recovery + "','" + _creation_date + "','" + _creation_date + "','" + dd.user_id + "','" + dd.record_status + "','"+dd.uom_code+"','"+dd.quantity+"','"+dd.meeting+"";
                        }
                        else
                        {
                       columnNames = "daily_dairy_otherthan_raid_id, raid_entry_date, intelligence_gathering, petrolling, vehicle_check, liquor_destruction, witness_appearance_in_court, others_, raid_recovery,  lastmodified_date, creation_date, user_id, record_status,meeting";
                        input = "" + max + "','" + dd.raid_entry_date + "','" + dd.intelligence_gathering + "','" + dd.petrolling + "','" + dd.vehicle_check + "','" + dd.liquor_destruction + "','" + dd.witness_appearance_in_court + "','" + dd.others + "','" + dd.raid_recovery + "','" + _creation_date + "','" + _creation_date + "','" + dd.user_id + "','" + dd.record_status + "','"+dd.meeting+"";
                        }
                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";
                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {

                            for (int i = 0; i < dd.docs.Count; i++)
                            {
                                n = 0;
                                StringBuilder str = new StringBuilder();
                                str.Append("INSERT INTO exciseautomation.dailydairy_docs(daily_dairy_id,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,docs_from)");
                                str.Append("Values('" + dd.daily_dairy_otherthan_raid_id + "','" + dd.daily_dairy_otherthan_raid_id + "','" + dd.docs[i].doc_name + "', '" + dd.docs[i].description + "','" + dd.docs[i].doc_path + "','DDOE','" + dd.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','W')");
                                NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                                n = cmd3.ExecuteNonQuery();
                            }
                            value = true;
                        }
                        else
                            value = false;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        value = false;
                    }
                }
            }
            return value;
        }

        public static bool Update(daily_diary_entry_otherthan_raid dd)
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
                        // int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.daily_dairy_raid", "daily_dairy_raid_id").ToString()) + 1;
                        string _creation_date = DateTime.Now.ToString("dd-MM-yyyy");
                        NpgsqlCommand cmd;
                        if(dd.liquor_destruction=="Yes")
                        {
                            cmd = new NpgsqlCommand("update exciseautomation.daily_dairy_otherthan_raid set  raid_entry_date='" + dd.raid_entry_date.Replace("/", "-") + "', intelligence_gathering='" + dd.intelligence_gathering + "', petrolling='" + dd.petrolling + "', vehicle_check='" + dd.vehicle_check + "', liquor_destruction='" + dd.liquor_destruction + "', witness_appearance_in_court='" + dd.witness_appearance_in_court + "', others_='" + dd.others + "', raid_recovery='" + dd.raid_recovery + "',  lastmodified_date='" + _creation_date + "',  user_id='" + dd.user_id + "', record_status='" + dd.record_status + "',uom_code='"+dd.uom_code+ "',liquor_quqntity='"+dd.quantity+"',meeting='"+dd.meeting+"' where daily_dairy_otherthan_raid_id='" + dd.daily_dairy_otherthan_raid_id + "'", cn);
                        }
                        else
                        {
                            cmd = new NpgsqlCommand("update exciseautomation.daily_dairy_otherthan_raid set  raid_entry_date='" + dd.raid_entry_date.Replace("/", "-") + "', intelligence_gathering='" + dd.intelligence_gathering + "', petrolling='" + dd.petrolling + "', vehicle_check='" + dd.vehicle_check + "', liquor_destruction='" + dd.liquor_destruction + "', witness_appearance_in_court='" + dd.witness_appearance_in_court + "', others_='" + dd.others + "', raid_recovery='" + dd.raid_recovery + "',  lastmodified_date='" + _creation_date + "',  user_id='" + dd.user_id + "', record_status='" + dd.record_status + "',meeting='" + dd.meeting + "' where daily_dairy_otherthan_raid_id='" + dd.daily_dairy_otherthan_raid_id + "'", cn);
                        }
                
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            NpgsqlCommand cmd_delete_docs = new NpgsqlCommand("delete from  exciseautomation.dailydairy_docs where daily_dairy_id ='" + dd.daily_dairy_otherthan_raid_id + "' and  doc_type_code ='DDOE'", cn);
                            int n_DOCS = cmd_delete_docs.ExecuteNonQuery();
                            for (int i = 0; i < dd.docs.Count; i++)
                            {
                                n = 0;
                                StringBuilder str = new StringBuilder();
                                str.Append("INSERT INTO exciseautomation.dailydairy_docs(daily_dairy_id,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,docs_from)");
                                str.Append("Values('" + dd.daily_dairy_otherthan_raid_id + "','" + dd.daily_dairy_otherthan_raid_id + "','" + dd.docs[i].doc_name + "', '" + dd.docs[i].description + "','" + dd.docs[i].doc_path + "','DDOE','" + dd.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','W')");
                                // string cmd1=("INSERT INTO exciseautomation.seizure_docs(seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date)Values('" + obj.seizureno + "','" + max + "','" + obj.docs[i].doc_name + "', '" + obj.docs[i].description + "','" + obj.docs[i].doc_path + "','BID','" + obj.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "')");
                                //str.Append("Values('" + obj.seizureno + "','" + max + "','" + obj.docs[i].doc_name + "', '" + obj.docs[i].description + "','" + obj.docs[i].doc_path + "','BID','" + obj.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "')");
                                //NpgsqlCommand cmd4 = new NpgsqlCommand(cmd1, cn);
                                NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                                n = cmd3.ExecuteNonQuery();
                            }
                            trn.Commit();
                            cn.Close();
                            value = true;

                        }
                        else
                        {
                            value = false;
                            trn.Rollback();
                            cn.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        trn.Rollback();
                        cn.Close();
                        value = false;
                    }
                }
            }
            return value;
        }
    }
}
