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
    public class DL_cm_seiz_Daily_Dairy_Raid
    {
        public static bool Insert(daily_diary_raid dd)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    try
                    {
                        int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.daily_dairy_raid", "daily_dairy_raid_id").ToString()) + 1;
                        dd.daily_dairy_raid_id = max.ToString();
                        string tableName = "exciseautomation.daily_dairy_raid";
                        string _creation_date = DateTime.Now.ToString("dd/MM/yyyy");
                        string columnNames = "daily_dairy_raid_id,raid_entry_date, place_of_raid, distance_of_travelled, raid_team_leader, raid_recovery, no_of_arrested, no_of_absconding, no_of_case_instituted, other_recovery,  lastmodified_date, creation_date, user_id, record_status";
                        string input = ""+max+"','"+dd.raid_entry_date+"','"+dd.place_of_raid+"','"+dd.distance_of_travelled+"','"+dd.raid_team_leader+"','"+dd.raid_recovery+"','"+dd.no_of_arrested+"','"+dd.no_of_absconding+"','"+dd.no_of_case_instituted+"','"+dd.other_recovery+"','"+_creation_date+"','"+_creation_date+"','"+dd.user_id+"','"+dd.record_status+"";
                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";
                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                          
                           
                            for(int i=0;i<dd.recovery.Count;i++)
                            {
                                  //  dd.daily_dairy_raid_id = "1";
                                    max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.daily_dairy_recovery", "daily_dairy_recovery_id").ToString()) + 1;
                                    StringBuilder str = new StringBuilder();
                                    str.Append("Insert Into exciseautomation.daily_dairy_recovery(daily_dairy_recovery_id, daily_dairy_raid_id, recovery_type, recovery_particulars_id,  recovery_qty, uom_code,  lastmodified_date, creation_date, user_id)values(");
                                    str.Append("'"+max+"','"+dd.daily_dairy_raid_id+"','"+dd.recovery[i].recovery_type.Substring(0,1)+"','"+dd.recovery[i].recovery_particulars_id+"','"+dd.recovery[i].recovery_qty+"','"+dd.recovery[i].uom_code+"','"+_creation_date+ "','" + _creation_date + "','"+dd.user_id+"')");
                                    cmd = new NpgsqlCommand(str.ToString(),cn);
                                    cmd.ExecuteNonQuery();
                              
                                
                            }
                            for (int i = 0; i < dd.docs.Count; i++)
                            {
                                n = 0;
                                StringBuilder str = new StringBuilder();
                                str.Append("INSERT INTO exciseautomation.dailydairy_docs(daily_dairy_id,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,docs_from)");
                                str.Append("Values('"+dd.daily_dairy_raid_id+"','" + max + "','" + dd.docs[i].doc_name + "', '" + dd.docs[i].description + "','" + dd.docs[i].doc_path + "','DDRE','" + dd.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','W')");
                                NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                                n = cmd3.ExecuteNonQuery();
                            }

                            for (int i = 0; i < dd.genderlist.Count; i++)
                            {
                                int max1 = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.seizure_gender", "gender_id").ToString()) + 1;
                                n = 0;
                                StringBuilder str = new StringBuilder();
                                str.Append("INSERT INTO exciseautomation.seizure_gender(gender_id,daily_dairy_raid_id ,gender_name,gender_code, arresting, user_id, creation_date,record_status)");
                                str.Append("Values('" + max1 + "','" + dd.daily_dairy_raid_id + "','" + dd.genderlist[i].gender_name + "', '" + dd.genderlist[i].gender_code + "','" + dd.genderlist[i].arresting + "','" + dd.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+dd.record_status+"')");
                                NpgsqlCommand cmd4 = new NpgsqlCommand(str.ToString(), cn);
                                n = cmd4.ExecuteNonQuery();
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

        public static bool UserUpdate(UserDetails user)
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
                        NpgsqlCommand cmd = new NpgsqlCommand("update exciseautomation.user_registration set user_name='" + user.user_name+"', photoname='"+user.photoname+"',mobile='"+user.mobile+"',user_dob='"+user.user_dob.Replace("/","-")+"',email_id='"+user.email_id+ "',designation_code='" + user.designation_code + "'  where user_id='" + user.id + "'", cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            cmd = new NpgsqlCommand("update exciseautomation.employee_master set emp_name='" + user.user_name + "', doj='" + user.date_of_joining + "',start_date='"+user.date_of_joining+"',end_date='"+user.date_of_retairment+"',dob='"+user.user_dob+"',designation_code='"+user.designation_code+"',mobile='"+user.mobile+ "',email_id='" + user.email_id + "' ,lastmodified_date='" + _creation_date + "',bloodgroup='"+user.blood_group+ "',emer_contact='"+user.emergency_contact+"' where user_id='" + user.user_id + "' or mobile='"+user.mobile+"' ", cn);
                            cmd.ExecuteNonQuery();
                            trn.Commit();
                            cn.Close();
                            value = true;
                        }
                        else
                        {
                            trn.Rollback();
                            cn.Close();
                            value = false;
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

        public static daily_diary_raid GetDetails(string raid_id)
        {
            daily_diary_raid record = new daily_diary_raid();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.daily_dairy_raid   where daily_dairy_raid_id= '" + raid_id + "' order by daily_dairy_raid_id desc", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr);
                        dr.Close();
                        record = new daily_diary_raid();
                        foreach (DataRow dr1 in dt.Rows)
                        {

                            record.daily_dairy_raid_id = dr1["daily_dairy_raid_id"].ToString();
                            record.place_of_raid = dr1["place_of_raid"].ToString();
                            record.raid_entry_date = dr1["raid_entry_date"].ToString().Substring(0, 10).Replace("/", "-");
                            record.raid_recovery = dr1["raid_recovery"].ToString();
                            record.other_recovery = dr1["other_recovery"].ToString();
                            record.no_of_absconding = dr1["no_of_absconding"].ToString();
                            record.no_of_arrested = dr1["no_of_arrested"].ToString();
                            record.no_of_case_instituted = dr1["no_of_case_instituted"].ToString();
                            record.record_status = dr1["record_status"].ToString();
                            record.user_id = dr1["user_id"].ToString();
                            record.record_status = dr1["record_status"].ToString();
                            record.distance_of_travelled = dr1["distance_of_travelled"].ToString();
                            record.raid_team_leader = dr1["raid_team_leader"].ToString();
                            record.gender= dr1["gender"].ToString();
                            record.other_recovery = dr1["other_recovery"].ToString();
                            record.recovery = new List<daily_dairy_recovery>();
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select a.*,b.Vehicle_type,c.article_category_name,d.uom_name from exciseautomation.daily_dairy_recovery a left join exciseautomation.vehicle_type_master b on a.recovery_particulars_id=b.vehicle_type_code left join exciseautomation.article_category_master c on a.recovery_particulars_id=c.article_category_code left join exciseautomation.uom_master d on a.uom_code=d.uom_code where a.daily_dairy_raid_id='" + record.daily_dairy_raid_id + "'  order by a.daily_dairy_recovery_id", cn))
                            {
                                cmd1.CommandType = System.Data.CommandType.Text;
                                NpgsqlDataReader dr2 = cmd1.ExecuteReader();
                                if (dr2.HasRows)
                                {
                                    while (dr2.Read())
                                    {
                                        daily_dairy_recovery recovery = new daily_dairy_recovery();
                                        recovery.daily_dairy_recovery_id = dr2["daily_dairy_recovery_id"].ToString();
                                        recovery.recovery_type = dr2["recovery_type"].ToString();
                                        if (recovery.recovery_type == "E")
                                            recovery.recovery_description = "Excisable Goods";
                                        if (recovery.recovery_type == "V")
                                            recovery.recovery_description = "Vehicle";
                                        if (recovery.recovery_type == "D")
                                            recovery.recovery_description = "Drunken";
                                        recovery.recovery_particulars_id = dr2["recovery_particulars_id"].ToString();
                                        if(recovery.recovery_type == "E")
                                        recovery.recovery_particulars_name = dr2["article_category_name"].ToString();
                                        if (recovery.recovery_type == "V")
                                            recovery.recovery_particulars_name = dr2["Vehicle_type"].ToString();
                                        recovery.recovery_qty = dr2["recovery_qty"].ToString();
                                        recovery.uom_code = dr2["uom_code"].ToString();
                                        recovery.uom_name = dr2["uom_name"].ToString();
                                        record.recovery.Add(recovery);
                                    }
                                }
                                dr2.Close();
                            }
                            record.docs = new List<Seizure_Docs>();
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select regexp_split_to_table(exciseautomation.dailydairy_Docs.doc_name, E',') AS doc_name_split,*from exciseautomation.dailydairy_Docs where  daily_dairy_id='" + record.daily_dairy_raid_id + "' and doc_type_code='DDRE' order by daily_dairy_docs_id", cn))
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
                                        doc.description = dr2["doc_desc"].ToString();
                                        doc.docs_from = dr2["docs_from"].ToString();
                                        if (dr2["docs_from"].ToString()=="W")
                                        {
                                            doc.doc_path = dr2["doc_path"].ToString();
                                        }
                                        else
                                        {
                                            doc.doc_path = dr2["doc_name_split"].ToString();
                                        }

                                        record.docs.Add(doc);
                                    }
                                }
                                dr2.Close();
                            }


                            record.genderlist = new List<seizure_gender>();
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.seizure_gender where daily_dairy_raid_id='" + record.daily_dairy_raid_id + "'  order by daily_dairy_raid_id", cn))
                            {
                                cmd1.CommandType = System.Data.CommandType.Text;
                                NpgsqlDataReader dr2 = cmd1.ExecuteReader();
                                if (dr2.HasRows)
                                {
                                    while (dr2.Read())
                                    {
                                      seizure_gender doc = new seizure_gender();
                                        doc.daily_dairy_raid_id= dr2["daily_dairy_raid_id"].ToString();
                                        doc.gender_code = dr2["gender_code"].ToString();
                                        doc.gender_name = dr2["gender_name"].ToString();
                                        if (dr2["arresting"].ToString()!="")
                                        doc.arresting= Convert.ToInt32(dr2["arresting"].ToString());
                                        doc.user_id = dr2["user_id"].ToString();
                                        record.genderlist.Add(doc);
                                    }
                                }
                                dr2.Close();
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
            return record;
        }

        public static List<daily_diary_raid> GetList(string user)
        {
            List<daily_diary_raid> lstObj = new List<daily_diary_raid>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT a.*,b.division_code,b.district_code FROM exciseautomation.daily_dairy_raid a inner join exciseautomation.user_registration b on a.user_id=b.user_id order by daily_dairy_raid_id desc", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<daily_diary_raid>();
                            while (dr.Read())
                            {
                                daily_diary_raid record = new daily_diary_raid();
                                record.daily_dairy_raid_id = dr["daily_dairy_raid_id"].ToString();
                                record.place_of_raid =dr["place_of_raid"].ToString();
                                record.raid_entry_date = dr["raid_entry_date"].ToString().Substring(0, 10).Replace("/", "-"); ;
                                record.raid_recovery = dr["raid_recovery"].ToString();
                                record.other_recovery = dr["other_recovery"].ToString();
                                record.no_of_absconding = dr["no_of_absconding"].ToString();
                                record.no_of_arrested = dr["no_of_arrested"].ToString();
                                record.no_of_case_instituted = dr["no_of_case_instituted"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.user_id = dr["user_id"].ToString();
                                record.district_code = dr["district_code"].ToString();
                                record.division_code = dr["division_code"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.distance_of_travelled = dr["distance_of_travelled"].ToString();
                                record.raid_team_leader = dr["raid_team_leader"].ToString();
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

        public static bool Update(daily_diary_raid dd)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    //NpgsqlTransaction trn;
                    //trn = cn.BeginTransaction();
                    try
                    {
                        // int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.daily_dairy_raid", "daily_dairy_raid_id").ToString()) + 1;
                        string _creation_date = DateTime.Now.ToString("dd-MM-yyyy");
                        NpgsqlCommand cmd = new NpgsqlCommand("update exciseautomation.daily_dairy_raid set raid_entry_date='"+dd.raid_entry_date.Substring(0,10).Replace("/","-")+"', place_of_raid='"+dd.place_of_raid+"', distance_of_travelled='"+dd.distance_of_travelled+"', raid_team_leader='"+dd.raid_team_leader+"', raid_recovery='"+dd.raid_recovery.Replace("Select","0")+"', no_of_arrested='"+dd.no_of_arrested+"', no_of_absconding='"+dd.no_of_absconding+"', no_of_case_instituted='"+dd.no_of_case_instituted+"', other_recovery='"+dd.other_recovery+"',  lastmodified_date='"+ _creation_date + "', user_id='"+dd.user_id+"', record_status='"+dd.record_status+"' where daily_dairy_raid_id='"+dd.daily_dairy_raid_id+"'", cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            NpgsqlCommand cmd_delete_recovery = new NpgsqlCommand("delete from  exciseautomation.daily_dairy_recovery where daily_dairy_raid_id ='" + dd.daily_dairy_raid_id + "'", cn);
                            int n_DOCS1 = cmd_delete_recovery.ExecuteNonQuery();
                            for (int i = 0; i < dd.recovery.Count; i++)
                            {
                                int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.daily_dairy_recovery", "daily_dairy_recovery_id").ToString()) + 1;
                                StringBuilder str = new StringBuilder();
                                str.Append("Insert Into exciseautomation.daily_dairy_recovery(daily_dairy_recovery_id, daily_dairy_raid_id, recovery_type, recovery_particulars_id,  recovery_qty, uom_code,  lastmodified_date, creation_date, user_id)values(");
                                str.Append("'" + max + "','" + dd.daily_dairy_raid_id + "','" + dd.recovery[i].recovery_type.Substring(0, 1) + "','" + dd.recovery[i].recovery_particulars_id + "','" + dd.recovery[i].recovery_qty + "','" + dd.recovery[i].uom_code + "','" + _creation_date + "','" + _creation_date + "','" + dd.user_id + "')");
                                cmd = new NpgsqlCommand(str.ToString(), cn);
                                cmd.ExecuteNonQuery();
                            }
                            NpgsqlCommand cmd_delete_docs = new NpgsqlCommand("delete from  exciseautomation.dailydairy_docs where daily_dairy_id ='" + dd.daily_dairy_raid_id + "' and  doc_type_code ='DDRE'", cn);
                            int n_DOCS = cmd_delete_docs.ExecuteNonQuery();
                            for (int i = 0; i < dd.docs.Count; i++)
                            {
                                n = 0;
                                StringBuilder str = new StringBuilder();
                                str.Append("INSERT INTO exciseautomation.dailydairy_docs(daily_dairy_id,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,lastmodified_date,docs_from)");
                                str.Append("Values('" + dd.daily_dairy_raid_id + "','" + dd.daily_dairy_raid_id + "','" + dd.docs[i].doc_name + "', '" + dd.docs[i].description + "','" + dd.docs[i].doc_path + "','DDRE','" + dd.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','W')");
                                // string cmd1=("INSERT INTO exciseautomation.seizure_docs(seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date)Values('" + obj.seizureno + "','" + max + "','" + obj.docs[i].doc_name + "', '" + obj.docs[i].description + "','" + obj.docs[i].doc_path + "','BID','" + obj.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "')");
                                //str.Append("Values('" + obj.seizureno + "','" + max + "','" + obj.docs[i].doc_name + "', '" + obj.docs[i].description + "','" + obj.docs[i].doc_path + "','BID','" + obj.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "')");
                                //NpgsqlCommand cmd4 = new NpgsqlCommand(cmd1, cn);
                                NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                                n = cmd3.ExecuteNonQuery();
                            }
                            NpgsqlCommand cmd_delete_gender = new NpgsqlCommand("delete from  exciseautomation.seizure_gender where daily_dairy_raid_id ='" + dd.daily_dairy_raid_id + "'", cn);
                            int gDOCS = cmd_delete_gender.ExecuteNonQuery();
                            for (int i = 0; i < dd.genderlist.Count; i++)
                            {
                                int max1 = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.seizure_gender", "gender_id").ToString()) + 1;
                                n = 0;
                                StringBuilder str = new StringBuilder();
                                str.Append("INSERT INTO exciseautomation.seizure_gender(gender_id,daily_dairy_raid_id ,gender_name,gender_code, arresting, user_id, creation_date,lastmodified_date,record_status)");
                                str.Append("Values('" +max1 + "','" + dd.daily_dairy_raid_id + "','" + dd.genderlist[i].gender_name + "', '" + dd.genderlist[i].gender_code + "','" + dd.genderlist[i].arresting + "','" + dd.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + dd.record_status+"')");
                                NpgsqlCommand cmd4 = new NpgsqlCommand(str.ToString(), cn);
                                n = cmd4.ExecuteNonQuery();
                            }
                            // trn.Commit();
                            cn.Close();
                            value = true;

                        }
                        else
                        {
                            value = false;
                           // trn.Rollback();
                            cn.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                     //   trn.Rollback();
                        cn.Close();
                        value = false;
                    }
                }
            }
            return value;
        }
    }
}
