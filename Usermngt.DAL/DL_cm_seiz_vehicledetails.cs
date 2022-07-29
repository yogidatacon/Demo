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
    #region seizure_vehicledetails
    /// <summary>
    /// exciseautomation.seizure_vehicledetails
    /// </summary>
    public class DL_cm_seiz_vehicledetails
    {
        public static List<cm_seiz_vehicledetails> GetList(string seizureNo)
        {
            string[] dept = seizureNo.Split('&');
            seizureNo = dept[0];
            string d = "";
            if (dept[1] == "Excise" || dept[1] == "E")
                d = "E";
            else
                d = "P";
            List<cm_seiz_vehicledetails> lstObj = new List<cm_seiz_vehicledetails>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    //using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.seizure_vehicledetails where seizureNo= "+ seizureNo , cn))
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select sv.*,vt.vehicle_type from exciseautomation.seizure_vehicledetails sv inner join exciseautomation.vehicle_type_master vt on vt.vehicle_type_code=sv.vehicle_type_code where seizureNo= " + seizureNo+" and raidby='"+d+ "' order by seizure_vehicledetails_id desc", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_seiz_vehicledetails>();
                            while (dr.Read())
                            {
                                cm_seiz_vehicledetails record = new cm_seiz_vehicledetails();
                                record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                                record.seizure_vehicledetails_id = Convert.ToInt32(dr["seizure_vehicledetails_id"].ToString());
                                record.vehiclename = dr["vehiclename"].ToString();
                                record.vehicle_type_code =dr["vehicle_type"].ToString();
                                record.manufacturer_code= dr["manufacturer"].ToString();
                                record.vehicle_number = dr["vehicle_number"].ToString();
                                record.chasisno = dr["chasisno"].ToString();
                                record.registrationno = dr["registrationno"].ToString();
                                record.ownername = dr["ownername"].ToString();
                                record.permanentaddress = dr["permanentaddress"].ToString();
                                record.presentaddress = dr["presentaddress"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.user_id = dr["user_id"].ToString();
                                record.makemodel = dr["makemodel"].ToString();
                                record.contactno = dr["contactno"].ToString();
                                record.engineno = dr["engineno"].ToString();
                                record.actioncompleted = dr["actioncompleted"].ToString();
                                record.challan_date = dr["challan_date"].ToString();
                                record.challan_no = dr["challan_no"].ToString();
                                record.infavourof = dr["infavourof"].ToString();
                                record.auction_or_releasedate = dr["auction_or_releasedate"].ToString();
                                record.auctionreleaseamount = dr["auctionreleaseamount"].ToString();
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

        public static List<cm_seiz_vehicledetails> GetvehicleList()
        {
            List<cm_seiz_vehicledetails> record = new List<cm_seiz_vehicledetails>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.seizure_vehicledetails ORDER BY vehicle_type_code", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            cm_seiz_vehicledetails record1 = new cm_seiz_vehicledetails();
                            while (dr.Read())
                            {
                               // record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                                record1.seizure_vehicledetails_id = Convert.ToInt32(dr["seizure_vehicledetails_id"].ToString());
                                record1.vehiclename = dr["vehiclename"].ToString();
                                record1.vehicle_type_code = dr["vehicle_type_code"].ToString();
                                // record.manufacturer_code = dr["manufacturer"]?.ToString() ?? string.Empty;
                                // record.makemodel = dr["makemodel"].ToString();
                                // record.vehicle_number = dr["vehicle_number"].ToString();
                                //record.chasisno = dr["chasisno"].ToString();
                                // record.registrationno = dr["registrationno"].ToString();
                                // record.ownername = dr["ownername"].ToString();
                                // record.contactno = dr["contactno"].ToString();
                                // record.permanentaddress = dr["permanentaddress"].ToString();
                                // record.presentaddress = dr["presentaddress"].ToString();
                                //  record.record_status = dr["record_status"].ToString();
                                record.Add(record1);
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

        public static cm_seiz_vehicledetails GetDetails(string tableId)
        {
            cm_seiz_vehicledetails record = new cm_seiz_vehicledetails();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.seizure_vehicledetails where seizure_vehicledetails_id=" + tableId, cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            record = new cm_seiz_vehicledetails();
                            while (dr.Read())
                            {
                                record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                                record.seizure_vehicledetails_id = Convert.ToInt32(dr["seizure_vehicledetails_id"].ToString());
                                record.vehiclename = dr["vehiclename"].ToString();
                                record.vehicle_type_code = dr["vehicle_type_code"]?.ToString() ?? string.Empty;
                                record.manufacturer_code = dr["manufacturer"]?.ToString() ?? string.Empty;
                                record.makemodel = dr["makemodel"].ToString();
                                record.vehicle_number = dr["vehicle_number"].ToString();
                                record.chasisno = dr["chasisno"].ToString();
                                record.registrationno = dr["registrationno"].ToString();
                                record.ownername = dr["ownername"].ToString();
                                record.contactno = dr["contactno"].ToString();
                                record.permanentaddress = dr["permanentaddress"].ToString();
                                record.presentaddress = dr["presentaddress"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.engineno = dr["engineno"].ToString();
                                record.gpscompany = dr["gpscompany"].ToString();
                                record.imeino = dr["imeino"].ToString();
                                record.simno = dr["simno"].ToString();
                                record.remarks = dr["remarks"].ToString();
                                record.SDR_CAF = dr["SDR_CAF"].ToString();
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

        public static List<cm_seiz_vehicledetails> VehicleSearch(string vno, string vType)
        {
            List<cm_seiz_vehicledetails> lstObj = new List<cm_seiz_vehicledetails>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select VD.*,VTM.* from exciseautomation.seizure_vehicledetails VD  INNER JOIN exciseautomation.vehicle_type_master VTM ON VD.vehicle_type_code=VTM.vehicle_type_code where trim(vehicle_number) ilike'%" + vno + "%'", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_seiz_vehicledetails>();
                            while (dr.Read())
                            {
                                cm_seiz_vehicledetails record = new cm_seiz_vehicledetails();
                                record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                                record.vehicle_type_code =dr["vehicle_type_code"].ToString();
                                record.vehicle_type = dr["vehicle_type"].ToString();
                                record.vehiclename= dr["vehiclename"].ToString();
                                record.vehicle_number = dr["vehicle_number"].ToString();
                                record.chasisno= dr["chasisno"].ToString();
                                record.makemodel = dr["makemodel"].ToString();
                                record.manufacturer_code = dr["manufacturer"].ToString();
                                record.registrationno = dr["registrationno"].ToString();
                                record.ownername= dr["ownername"].ToString();
                                record.contactno = dr["contactno"].ToString();
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
        /// <summary>
        /// manufacturer_code not saving need to save after removing fk in seiz_vechicle table
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool InsertSeiz_OtherExcisableArticles(cm_seiz_vehicledetails obj)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    try
                    {
                        string tableName = "exciseautomation.seizure_vehicledetails";
                        int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.seizure_vehicledetails", "seizure_vehicledetails_id").ToString()) + 1;
                        string columnNames = "seizure_vehicledetails_id, seizureno, vehicle_type_code,  vehiclename, manufacturer, makemodel, chasisno, registrationno, ownername, presentaddress, permanentaddress, contactno, ipaddress, challan_no,lastmodified_date, user_id, creation_date, record_status, record_deleted, vehicle_number,raidby,engineno,gpscompany,imeino ,simno,remarks,SDR_CAF";
                        string input = max + "','" + obj.seizureno + "','" + obj.vehicle_type_code + "','" +  obj.vehiclename + "','" +obj.manufacturer_code + "','" + obj.makemodel + "','" + obj.chasisno + "','" + obj.registrationno + "','" + obj.ownername + "','" + obj.presentaddress + "','" + obj.permanentaddress + "','" + obj.contactno + "','" + obj.ipaddress + "','" + obj.challan_no + "','" + DateTime.Now.ToShortDateString()+ "','" + obj.user_id + "','" + DateTime.Now.ToShortDateString() + "','" + obj.record_status + "','" + obj.record_deleted + "','" + obj.vehicle_number+"','"+obj.raidby+"','"+obj.engineno+"','"+obj.gpscompany+"','"+obj.imeino+"','"+obj.simno+"','"+obj.remarks+"','"+obj.SDR_CAF;

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

        public static bool Update_OtherExcisableArticles(cm_seiz_vehicledetails cm_obj)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                    cn.Open();
                try
                {                    
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure_vehicledetails SET  vehicle_type_code= '" + cm_obj.vehicle_type_code + "',  manufacturer= '" + cm_obj.manufacturer_code + "',  vehiclename= '" + cm_obj.vehiclename + "',  makemodel= '" + cm_obj.makemodel + "',  chasisno= '" + cm_obj.chasisno + "',  registrationno= '" + cm_obj.registrationno + "',  ownername= '" + cm_obj.ownername + "',  presentaddress= '" + cm_obj.presentaddress + "',  permanentaddress= '" + cm_obj.permanentaddress + "',  contactno= '" + cm_obj.contactno + "',  ipaddress= '" + cm_obj.ipaddress + "',  challan_no= '" + cm_obj.challan_no + "', lastmodified_date= '" + DateTime.Now.ToShortDateString() + "',  user_id= '" + cm_obj.user_id + "',  record_status= '" + cm_obj.record_status + "',  record_deleted= '" + cm_obj.record_deleted + "',  vehicle_number='" + cm_obj.vehicle_number + "',engineno='" + cm_obj.engineno + "',gpscompany='" + cm_obj.gpscompany + "',imeino='" + cm_obj.imeino + "' ,simno='" + cm_obj.simno + "',remarks='"+cm_obj.remarks+ "',SDR_CAF='"+cm_obj.SDR_CAF+"' WHERE seizureno ='" + cm_obj.seizureno + "' and seizure_vehicledetails_id='"+cm_obj.seizure_vehicledetails_id+"' ", cn);
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
    }
    #endregion seizure_vehicledetails
}
