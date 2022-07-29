using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Usermngt.Entities;
using Npgsql;
using System.Data;

namespace Usermngt.DAL
{
    #region DL_cm_seiz_Apparatus
    public class DL_cm_seiz_Apparatus
    {
        public static List<cm_seiz_Apparatus> GetList(string seizureNo)
        {
            string[] dept = seizureNo.Split('&');
            seizureNo = dept[0];
            string d = "";
            if (dept[1] == "Excise" || dept[1] == "E")
                d = "E";
            else
                d = "P";
            List<cm_seiz_Apparatus> lstObj = new List<cm_seiz_Apparatus>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT ad.*,atm.apparatus_type FROM exciseautomation.seizure_apparatusdetails ad  INNER JOIN exciseautomation.apparatus_type_master atm ON atm.apparatus_type_code=ad.apparatus_type_code where seizureNo= " + seizureNo+" order by seizure_apparatusdetails_id desc", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_seiz_Apparatus>();
                            while (dr.Read())
                            {
                                cm_seiz_Apparatus record = new cm_seiz_Apparatus();
                                record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                                record.seizure_apparatusdetails_id = Convert.ToInt32(dr["seizure_apparatusdetails_id"].ToString());
                                record.apparatus_type_code = dr["apparatus_type"].ToString();
                                record.apparatus_name = dr["apparatus_name"].ToString();
                                record.manufacturer_code = dr["manufacturer"].ToString();
                                record.makemodel = dr["makemodel"].ToString();
                                record.ownername = dr["ownername"].ToString();
                                record.contactno = dr["contactno"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.user_id = dr["user_id"].ToString();
                                record.actioncompleted = dr["actioncompleted"].ToString();
                                record.date_of_destruction = dr["date_of_destruction"].ToString();
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

        public static List<apparatus_type_master> GetApparatusTypeMasterList(string empty)
        {
            List<apparatus_type_master> apparatus_typeMaster = new List<apparatus_type_master>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT apparatus_type_code, apparatus_type FROM exciseautomation.apparatus_type_master where record_deleted =false order by apparatus_type_code", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            apparatus_typeMaster = new List<apparatus_type_master>();
                            while (dr.Read())
                            {
                                apparatus_type_master ta = new apparatus_type_master();
                                ta.apparatus_type_code = dr["apparatus_type_code"].ToString();
                                ta.apparatus_type = dr["apparatus_type"].ToString();
                                apparatus_typeMaster.Add(ta);
                            }
                        }
                    }
                    //_log.Info("Get apparatus_type master List Success");
                }
                catch (Exception ex)
                {
                    // _log.Info("Get apparatus_type List Fail");
                }
            }
            return apparatus_typeMaster;
        }

        public static cm_seiz_Apparatus GetDetails(string tableId)
        {
            cm_seiz_Apparatus record = new cm_seiz_Apparatus();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.seizure_apparatusdetails where seizure_apparatusdetails_id=" + tableId, cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            record = new cm_seiz_Apparatus();
                            while (dr.Read())
                            {
                                record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                                record.seizure_apparatusdetails_id = Convert.ToInt32(dr["seizure_apparatusdetails_id"].ToString());
                                record.apparatus_name = dr["apparatus_name"].ToString();
                                record.manufacturer_code = dr["manufacturer"].ToString();
                                record.apparatus_type_code = dr["apparatus_type_code"].ToString();
                                record.makemodel =dr["makemodel"].ToString();
                                record.ownername = dr["ownername"].ToString();
                                record.contactno = dr["contactno"].ToString();
                                record.permanentaddress = dr["permanentaddress"].ToString();
                                record.presentaddress = dr["presentaddress"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.imeino = dr["imeino"].ToString();
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

        public static bool InsertSeiz_Apparatus(cm_seiz_Apparatus obj)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    try
                    {

                        string tableName = "exciseautomation.seizure_apparatusdetails";
                        int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.seizure_apparatusdetails", "seizure_apparatusdetails_id").ToString()) + 1;
                        string columnNames = "seizure_apparatusdetails_id, seizureno, apparatus_type_code, manufacturer, apparatus_name, makemodel, ownername, presentaddress, permanentaddress, contactno,lastmodified_date, user_id, creation_date, record_status, record_deleted,raidby,imeino,ipaddress";
                        string input = max + "','" + obj.seizureno + "','" + obj.apparatus_type_code + "','" + obj.manufacturer_code + "','" + obj.apparatus_name + "','" + obj.makemodel + "','" + obj.ownername + "','" + obj.presentaddress + "','" + obj.permanentaddress + "','" + obj.contactno + "','" + DateTime.Now.ToShortDateString()+ "','" + obj.user_id + "','" + DateTime.Now.ToShortDateString() + "','" + obj.record_status + "','" + obj.record_deleted+"','"+obj.raidby+"','"+obj.imeino+"','"+obj.ipaddress;

                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
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

        public static bool Update_Apparatus(cm_seiz_Apparatus cm_obj)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                    cn.Open();
                try
                {                                        
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure_apparatusdetails SET  apparatus_type_code = '" + cm_obj.apparatus_type_code + "', manufacturer = '" + cm_obj.manufacturer_code + "', apparatus_name = '" + cm_obj.apparatus_name+ "', makemodel = '" + cm_obj.makemodel+ "', ownername = '" + cm_obj.ownername + "', presentaddress = '" + cm_obj.presentaddress+ "', permanentaddress = '" + cm_obj.permanentaddress+ "', contactno = '" + cm_obj.contactno+ "', ipaddress = '" + cm_obj.ipaddress+ "',  lastmodified_date = '" + DateTime.Now.ToShortDateString() + "', user_id = '" + cm_obj.user_id+ "', record_status = '" + cm_obj.record_status+ "',imeino='"+cm_obj.imeino+"' WHERE seizure_apparatusdetails_id ='" + cm_obj.seizure_apparatusdetails_id + "'", cn);
                    int n = cmd.ExecuteNonQuery();
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

        public static List<cm_seiz_Apparatus> ApparatusSearch(string name)
        {
            List<cm_seiz_Apparatus> lstObj = new List<cm_seiz_Apparatus>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.seizure_apparatusdetails a inner join exciseautomation.apparatus_type_master b on a.apparatus_type_code=b.apparatus_type_code where a.apparatus_name ilike '%" + name+"%'", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_seiz_Apparatus>();
                            while (dr.Read())
                            {
                                cm_seiz_Apparatus record = new cm_seiz_Apparatus();
                                record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                                record.apparatus_type_code = dr["apparatus_type_code"].ToString();
                                record.apparatus_type = dr["apparatus_type"].ToString();
                                record.apparatus_name = dr["apparatus_name"].ToString();
                                record.ownername = dr["ownername"].ToString();
                                record.permanentaddress = dr["permanentaddress"].ToString();
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
    }
    #endregion DL_cm_seiz_Apparatus
}
