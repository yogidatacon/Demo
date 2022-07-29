using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.Entities;

namespace Usermngt.DAL
{
   public class DL_ReceiverToStoragrTransfer
    {


        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static string Insert(ReceiverToStoragrTransfer form84)
        {
            string value = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
               // where financial_year = '"+form84.financial_year+"'
                NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT max(receiver_storage_transfer_id) FROM exciseautomation.receiver_storage_transfer ", cn);
                string m = cmd1.ExecuteScalar().ToString();
                int n = 0;
                if (m == "")
                    n = 1;
                else
                    n = Convert.ToInt32(m) + 1;

                try
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("INSERT INTO exciseautomation.receiver_storage_transfer(receiver_storage_transfer_id, transfer_date, transfer_time, party_code, fromstoragevat, moved_to_nextstage, total_bl_transfer, total_lp_transfer, inc_operation, inc_groging, dec_reduction, dec_blending, dec_racking, dec_wastage, dips, temperature, indication, strength, lastmodified_date, user_id, creation_date, record_status,remarks,todispatchvat,financial_year)");
                    str.Append("VALUES('" + n + "', '" + form84.transfer_date + "', '" + form84.transfer_time + "', '" + form84.party_code + "', '" + form84.fromstoragevat + "', 'N', '" + form84.total_bl_transfer + "', '" + form84.total_lp_transfer + "', '" + form84.inc_operation + "', '" + form84.inc_groging + "', '" + form84.dec_reduction + "', '" + form84.dec_blending + "', '" + form84.dec_racking + "', '" + form84.dec_wastage + "', '" + form84.dips + "', '" + form84.temperature + "','" + form84.indication + "', '" + form84.strength + "', '" + DateTime.Now.ToShortDateString() + "', '" + form84.user_id + "', '" + DateTime.Now.ToShortDateString() + "','" + form84.record_status + "','"+form84.remarks+"','"+form84.todispatchvat+"','"+form84.financial_year+"')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    cmd.ExecuteNonQuery();

                    if (form84.record_status == "Y")
                    {
                      
                        
                    }

                    value = "0";
                    _log.Info("Get form84 Insert Success: " + n + "-" + form84.party_code + "-" + form84.fromstoragevat);
                }
                catch (Exception ex)
                {

                    value = ex.Message;
                    _log.Info("Get form84 Insert fail: " + n + "-" + form84.party_code + "-" + form84.fromstoragevat);
                }

                return value;
            }
        }

        public static string Update(ReceiverToStoragrTransfer form84)
        {
            string val = "0";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.receiver_storage_transfer SET  transfer_date ='" + form84.transfer_date + "', transfer_time ='" + form84.transfer_time + "',  fromstoragevat ='" + form84.fromstoragevat + "', total_bl_transfer ='" + form84.total_bl_transfer + "', total_lp_transfer ='" + form84.total_lp_transfer + "', inc_operation ='" + form84.inc_operation + "', inc_groging ='" + form84.inc_groging + "', dec_reduction ='" + form84.dec_reduction + "', dec_blending ='" + form84.dec_blending + "', dec_racking ='" + form84.dec_racking + "', dec_wastage ='" + form84.dec_wastage + "', dips ='" + form84.dips + "', temperature ='" + form84.temperature + "', indication ='" + form84.indication + "', strength ='" + form84.strength + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "', record_status ='" + form84.record_status + "',remarks='"+form84.remarks+ "',todispatchvat='"+form84.todispatchvat+ "' WHERE receiver_storage_transfer_id ='" + form84.receiver_storage_transfer_id + "' and financial_year='" + form84.financial_year + "'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();

                    //if (form84.record_status == "Y")
                    //{
                        
                    //    NpgsqlCommand cmd4 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + form84.fromstoragevat + "' and party_code='" + form84.party_code + "'", cn);
                    //    double a = Convert.ToDouble(cmd4.ExecuteScalar());
                    //    double v = Convert.ToDouble(a) - Convert.ToDouble(form84.total_bl_transfer);
                    //    NpgsqlCommand cmd5 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + v + "'  where vat_code='" + form84.fromstoragevat + "' and party_code='" + form84.party_code + "'", cn);
                    //    cmd5.ExecuteNonQuery();

                    //    NpgsqlCommand cmd6 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + form84.todispatchvat + "' and party_code='" + form84.party_code + "'", cn);
                    //    double b = Convert.ToDouble(cmd6.ExecuteScalar());
                    //    double c = Convert.ToDouble(b) + Convert.ToDouble(form84.total_bl_transfer);
                    //    NpgsqlCommand cmd7 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + c + "'  where vat_code='" + form84.todispatchvat + "' and party_code='" + form84.party_code + "'", cn);
                    //    cmd7.ExecuteNonQuery();
                    //}
                    val = "0";
                    _log.Info("Get form84 Update Success");
                }
                catch (Exception ex)
                {
                    val = ex.Message;
                    _log.Info("Get form84 update Fail :" + ex.Message);
                }
            }
            return val;
        }

        public static List<ReceiverToStoragrTransfer> GetList()
        {
            List<ReceiverToStoragrTransfer> from84 = new List<ReceiverToStoragrTransfer>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                   // if (party_code == null || party_code == "All")
                        cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name as storagevat,d.vat_name as dispatchvat from exciseautomation.receiver_storage_transfer a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.fromstoragevat=c.vat_code inner join exciseautomation.vat_master d on a.todispatchvat=d.vat_code where a.record_active='true' order by a.transfer_date desc", cn);
                  //  else
                  //      cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name as storagevat,d.vat_name as dispatchvat from exciseautomation.receiver_storage_transfer a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.fromstoragevat=c.vat_code inner join exciseautomation.vat_master d on a.todispatchvat=d.vat_code where a.party_code='" + party_code+ "' order by a.party_code,a.transfer_date", cn);

                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        from84 = new List<ReceiverToStoragrTransfer>();
                        while (dr.Read())
                        {
                            ReceiverToStoragrTransfer record = new ReceiverToStoragrTransfer();
                            record.transfer_date = dr["transfer_date"].ToString().Replace("/", "-").Substring(0, 10); ;
                            record.transfer_time = dr["transfer_time"].ToString();
                            record.party_code = dr["party_code"].ToString();
                            record.financial_year = dr["financial_year"].ToString();
                            record.party_name = dr["party_name"].ToString();
                            record.storagevat = dr["storagevat"].ToString();
                            record.dispatchvat = dr["dispatchvat"].ToString();
                            record.fromstoragevat = dr["fromstoragevat"].ToString();
                            record.total_bl_transfer = Convert.ToDouble(dr["total_bl_transfer"].ToString());
                            record.total_lp_transfer = Convert.ToDouble(dr["total_lp_transfer"].ToString());
                            record.dips = Convert.ToDouble(dr["dips"].ToString());
                            record.temperature = Convert.ToDouble(dr["temperature"].ToString());
                            record.indication = Convert.ToDouble(dr["indication"].ToString());
                            record.strength = Convert.ToDouble(dr["strength"].ToString());
                            record.inc_groging = Convert.ToDouble(dr["inc_groging"].ToString());
                            record.record_status = dr["record_status"].ToString();
                            //record.approval_status = dr["record_status"].ToString();
                            record.receiver_storage_transfer_id= Convert.ToInt32(dr["receiver_storage_transfer_id"].ToString());
                            from84.Add(record);
                        }
                        _log.Info("Get Form84 List Success");
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Get Form84 List Fail :" + ex.Message);
                }
            }
            return from84;
        }
        public static List<ReceiverToStoragrTransfer> Search(string tablename, string column, string value)
        {
            List<ReceiverToStoragrTransfer> mir = new List<ReceiverToStoragrTransfer>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    NpgsqlCommand cmd;
                 if(column== "storagevat")
                    {
                        cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name as storagevat,d.vat_name as dispatchvat from exciseautomation.receiver_storage_transfer a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.fromstoragevat=c.vat_code inner join exciseautomation.vat_master d on a.todispatchvat=d.vat_code where  c.vat_name Ilike '%" + value + "%' and a.record_active='true'  order by a.transfer_date desc", cn);

                    }
                    else if(column== "dispatchvat")
                    {
                        cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name as storagevat,d.vat_name as dispatchvat from exciseautomation.receiver_storage_transfer a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.fromstoragevat=c.vat_code inner join exciseautomation.vat_master d on a.todispatchvat=d.vat_code where  d.vat_name Ilike '%" + value + "%' and a.record_active='true'  order by a.transfer_date desc", cn);

                    }
                    else
                    {
                        cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name as storagevat,d.vat_name as dispatchvat from exciseautomation.receiver_storage_transfer a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.fromstoragevat=c.vat_code inner join exciseautomation.vat_master d on a.todispatchvat=d.vat_code where  " + column + " Ilike '%" + value + "%' and a.record_active='true'  order by a.transfer_date desc", cn);

                    }

                    cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            mir = new List<ReceiverToStoragrTransfer>();
                            while (dr.Read())
                            {
                                ReceiverToStoragrTransfer record = new ReceiverToStoragrTransfer();
                                record.transfer_date = dr["transfer_date"].ToString().Replace("/", "-").Substring(0, 10); ;
                                record.transfer_time = dr["transfer_time"].ToString();
                                record.party_code = dr["party_code"].ToString();
                            record.financial_year = dr["financial_year"].ToString();
                            record.party_name = dr["party_name"].ToString();
                                record.storagevat = dr["storagevat"].ToString();
                                record.dispatchvat = dr["dispatchvat"].ToString();
                                record.fromstoragevat = dr["fromstoragevat"].ToString();
                                record.total_bl_transfer = Convert.ToDouble(dr["total_bl_transfer"].ToString());
                                record.total_lp_transfer = Convert.ToDouble(dr["total_lp_transfer"].ToString());
                                record.dips = Convert.ToDouble(dr["dips"].ToString());
                                record.temperature = Convert.ToDouble(dr["temperature"].ToString());
                                record.indication = Convert.ToDouble(dr["indication"].ToString());
                                record.strength = Convert.ToDouble(dr["strength"].ToString());
                                record.inc_groging = Convert.ToDouble(dr["inc_groging"].ToString());
                                record.record_status = dr["record_status"].ToString();
                                //record.approval_status = dr["record_status"].ToString();
                                record.receiver_storage_transfer_id = Convert.ToInt32(dr["receiver_storage_transfer_id"].ToString());
                                mir.Add(record);
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




        public static ReceiverToStoragrTransfer GetDetails(string party_code, string form84id, string financial_year)
        {
            ReceiverToStoragrTransfer record = new ReceiverToStoragrTransfer();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name from exciseautomation.receiver_storage_transfer a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.fromstoragevat=c.vat_code where a.party_code='" + party_code + "' and a.receiver_storage_transfer_id='" + form84id + "' and a.financial_year='"+financial_year+"'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            record.transfer_date = dr["transfer_date"].ToString().Replace("/", "-").Substring(0, 10);
                            record.transfer_time = dr["transfer_time"].ToString();
                           // record.production_date = Convert.ToDateTime(dr["production_date"]).ToString("dd-MM-yyyy");
                            record.party_code = dr["party_code"].ToString();
                            record.party_name = dr["party_name"].ToString();
                            record.storagevat = dr["vat_name"].ToString();
                            // record.from_receivervat = dr["from_receivervat"].ToString();
                            record.fromstoragevat = dr["fromstoragevat"].ToString();
                            record.total_bl_transfer = Convert.ToDouble(dr["total_bl_transfer"].ToString());
                            record.total_lp_transfer = Convert.ToDouble(dr["total_lp_transfer"].ToString());
                            record.record_status = dr["record_status"].ToString();
                            record.receiver_storage_transfer_id = Convert.ToInt32(dr["receiver_storage_transfer_id"].ToString());
                            record.remarks = dr["remarks"].ToString();
                            record.dips = Convert.ToDouble(dr["dips"].ToString());
                            record.temperature = Convert.ToDouble(dr["temperature"].ToString());
                            record.indication = Convert.ToDouble(dr["indication"].ToString());
                            record.strength = Convert.ToDouble(dr["strength"].ToString());
                            record.inc_operation = Convert.ToDouble(dr["inc_operation"].ToString());
                            record.inc_groging = Convert.ToDouble(dr["inc_groging"].ToString());
                            record.dec_blending = Convert.ToDouble(dr["dec_blending"].ToString());
                            record.dec_racking = Convert.ToDouble(dr["dec_racking"].ToString());
                            record.dec_reduction = Convert.ToDouble(dr["dec_reduction"].ToString());
                            record.dec_wastage = Convert.ToDouble(dr["dec_wastage"].ToString());
                            record.todispatchvat = dr["todispatchvat"].ToString();
                        }
                        _log.Info("Get VAT Master List Success");
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Get VAT Master List Fail :" + ex.Message);
                }
            }
            return record;
        }
        public static ReceiverToStoragrTransfer GetVatData(string vat_code, string party_code)
        {
            ReceiverToStoragrTransfer vat = new ReceiverToStoragrTransfer();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.vat_availablecapacity from exciseautomation.vat_master a  inner join exciseautomation.party_master b on a.party_code=b.party_code where  a.vat_code='"+vat_code+"' and b.party_code='"+party_code+"'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            vat.vat_availablecapacity = Convert.ToDouble(dr["vat_availablecapacity"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return vat;
            }
        }
        public static string Approve(ReceiverToStoragrTransfer form84)
        {
            string value;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.receiver_storage_transfer SET  record_status='" + form84.record_status + "' WHERE receiver_storage_transfer_id='" + form84.receiver_storage_transfer_id + "' and financial_year='" + form84.financial_year + "'", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (form84.record_status == "R")
                    {
                        form84.record_status = "Rejected by Bond Officer";
                    }
                    else
                    {
                        NpgsqlCommand cmd4 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + form84.fromstoragevat + "' and party_code='" + form84.party_code + "'", cn);
                        double a = Convert.ToDouble(cmd4.ExecuteScalar());
                        double v = Convert.ToDouble(a) - Convert.ToDouble(form84.total_bl_transfer);
                        NpgsqlCommand cmd5 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + v + "'  where vat_code='" + form84.fromstoragevat + "' and party_code='" + form84.party_code + "'", cn);
                        cmd5.ExecuteNonQuery();

                        //NpgsqlCommand cmd6 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + form84.todispatchvat + "' and party_code='" + form84.party_code + "'", cn);
                        //double b = Convert.ToDouble(cmd6.ExecuteScalar());
                        //double c = Convert.ToDouble(b) + Convert.ToDouble(form84.total_bl_transfer);
                        //NpgsqlCommand cmd7 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + c + "'  where vat_code='" + form84.todispatchvat + "' and party_code='" + form84.party_code + "'", cn);
                        //cmd7.ExecuteNonQuery();
                        form84.record_status = "Approved by Bond Officer";
                    }
                    StringBuilder str = new StringBuilder();
                    str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                    str.Append("'" + form84.receiver_storage_transfer_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "','RTST','" + form84.record_status + "','" + form84.remarks + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + form84.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + form84.user_id + "','"+form84.financial_year+"','"+form84.party_code+"')");
                    cmd = new NpgsqlCommand(str.ToString(), cn);
                    n = cmd.ExecuteNonQuery();
                    value = "0";
                    trn.Commit();
                    _log.Info("Form84 Approve Sucess:" + form84.receiver_storage_transfer_id);
                    cn.Close();
                }
                catch (Exception ex)
                {
                    value = ex.Message;
                    _log.Info("Form84 Approve Fail:" + form84.receiver_storage_transfer_id);
                    trn.Rollback();
                }
                return value;

            }
        }


        public static List<ReceiverToStoragrTransfer> GetTransferdate(string party_code)
        {
            List<ReceiverToStoragrTransfer> from84 = new List<ReceiverToStoragrTransfer>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    if (party_code == null || party_code == "All")
                        cmd = new NpgsqlCommand("SELECT a.* FROM exciseautomation.receiver_storage_transfer a  inner join  exciseautomation.party_master c on a.party_code=c.party_code inner join exciseautomation.vat_master d on a.todispatchvat=d.vat_code where  a.moved_to_nextstage ='N'", cn);
                    else
                        cmd = new NpgsqlCommand("SELECT a.* FROM exciseautomation.receiver_storage_transfer a  inner join  exciseautomation.party_master c on a.party_code=c.party_code inner join exciseautomation.vat_master d on a.todispatchvat=d.vat_code where c.party_code='"+party_code+ "' and a.moved_to_nextstage ='N' and a.record_status='A' ", cn);

                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        from84 = new List<ReceiverToStoragrTransfer>();
                        while (dr.Read())
                        {
                            ReceiverToStoragrTransfer record = new ReceiverToStoragrTransfer();
                            record.transfer_date = dr["transfer_date"].ToString().Replace("/", "-").Substring(0, 10); ;
                            record.transfer_time = dr["transfer_time"].ToString();
                            record.party_code = dr["party_code"].ToString();
                            //record.party_name = dr["party_name"].ToString();
                            // record.storagevat = dr["storagevat"].ToString();
                            record.fromstoragevat = dr["fromstoragevat"].ToString();
                            record.total_bl_transfer = Convert.ToDouble(dr["total_bl_transfer"].ToString());
                            record.total_lp_transfer = Convert.ToDouble(dr["total_lp_transfer"].ToString());
                            record.dips = Convert.ToDouble(dr["dips"].ToString());
                            record.temperature = Convert.ToDouble(dr["temperature"].ToString());
                            record.indication = Convert.ToDouble(dr["indication"].ToString());
                            record.strength = Convert.ToDouble(dr["strength"].ToString());
                            record.financial_year = dr["financial_year"].ToString();
                            record.inc_groging = Convert.ToDouble(dr["inc_groging"].ToString());
                            record.record_status = dr["record_status"].ToString();
                            record.todispatchvat = dr["todispatchvat"].ToString();
                          //  record.dispatchvat = dr["dispatchvat"].ToString();
                            //record.approval_status = dr["record_status"].ToString();
                            record.receiver_storage_transfer_id = Convert.ToInt32(dr["receiver_storage_transfer_id"].ToString());
                            from84.Add(record);
                            
                        }
                        _log.Info("Get VAT Master List Success");
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Get VAT Master List Fail :" + ex.Message);
                }
            }
            return from84;
        }


        public static List<ReceiverToStoragrTransfer> GetDENvat(int TransferId)
        {
            List<ReceiverToStoragrTransfer> from84 = new List<ReceiverToStoragrTransfer>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    cmd = new NpgsqlCommand("SELECT a.*,d.vat_name as dispatchvat FROM exciseautomation.receiver_storage_transfer a  inner join  exciseautomation.party_master c on a.party_code=c.party_code inner join exciseautomation.vat_master d on a.todispatchvat=d.vat_code where a.receiver_storage_transfer_id='"+TransferId+"'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        from84 = new List<ReceiverToStoragrTransfer>();
                        while (dr.Read())
                        {
                            ReceiverToStoragrTransfer record = new ReceiverToStoragrTransfer();

                            record.transfer_date = dr["transfer_date"].ToString().Replace("/", "-").Substring(0, 10); ;
                            record.transfer_time = dr["transfer_time"].ToString();
                            record.party_code = dr["party_code"].ToString();
                            //record.party_name = dr["party_name"].ToString();
                            // record.storagevat = dr["storagevat"].ToString();
                            record.fromstoragevat = dr["fromstoragevat"].ToString();
                            record.total_bl_transfer = Convert.ToDouble(dr["total_bl_transfer"].ToString());
                            record.total_lp_transfer = Convert.ToDouble(dr["total_lp_transfer"].ToString());
                            record.dips = Convert.ToDouble(dr["dips"].ToString());
                            record.temperature = Convert.ToDouble(dr["temperature"].ToString());
                            record.financial_year = dr["financial_year"].ToString();
                            record.indication = Convert.ToDouble(dr["indication"].ToString());
                            record.strength = Convert.ToDouble(dr["strength"].ToString());
                            record.inc_groging = Convert.ToDouble(dr["inc_groging"].ToString());
                            record.record_status = dr["record_status"].ToString();
                            record.todispatchvat= dr["todispatchvat"].ToString();
                            record.dispatchvat = dr["dispatchvat"].ToString();
                            //record.approval_status = dr["record_status"].ToString();
                            record.receiver_storage_transfer_id = Convert.ToInt32(dr["receiver_storage_transfer_id"].ToString());
                            from84.Add(record);
                        }
                        _log.Info("Get VAT Master List Success");
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Get VAT Master List Fail :" + ex.Message);
                }
            }
            return from84;
        }

        public static ReceiverToStoragrTransfer GetBLLPquty(string vat_code, string party_code,int transferid)
        {
            ReceiverToStoragrTransfer transfer = new ReceiverToStoragrTransfer();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.* from exciseautomation.receiver_storage_transfer a inner join exciseautomation.vat_master b on a.todispatchvat=b.vat_code inner join exciseautomation.party_master c on a.party_code=c.party_code where  a.todispatchvat='"+vat_code+"' and b.party_code='"+party_code+ "' and a.receiver_storage_transfer_id='"+transferid+"'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            transfer.total_bl_transfer = Convert.ToDouble(dr["total_bl_transfer"].ToString());
                            transfer.total_lp_transfer = Convert.ToDouble(dr["total_lp_transfer"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return transfer;
            }
        }

        public static List<ReceiverToStoragrTransfer> GetSubmiteddate(string party_code, int transferid)
        {
            List<ReceiverToStoragrTransfer> from84 = new List<ReceiverToStoragrTransfer>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    cmd = new NpgsqlCommand("SELECT a.* FROM exciseautomation.receiver_storage_transfer a  inner join  exciseautomation.party_master c on a.party_code=c.party_code inner join exciseautomation.vat_master d on a.todispatchvat=d.vat_code where c.party_code='" + party_code + "' and a.moved_to_nextstage ='Y' and a.receiver_storage_transfer_id='" + transferid + "' ", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        from84 = new List<ReceiverToStoragrTransfer>();
                        while (dr.Read())
                        {
                            ReceiverToStoragrTransfer record = new ReceiverToStoragrTransfer();
                            record.transfer_date = dr["transfer_date"].ToString().Replace("/", "-").Substring(0, 10); ;
                            record.transfer_time = dr["transfer_time"].ToString();
                            record.party_code = dr["party_code"].ToString();
                            //record.party_name = dr["party_name"].ToString();
                            // record.storagevat = dr["storagevat"].ToString();
                            record.fromstoragevat = dr["fromstoragevat"].ToString();
                            record.total_bl_transfer = Convert.ToDouble(dr["total_bl_transfer"].ToString());
                            record.total_lp_transfer = Convert.ToDouble(dr["total_lp_transfer"].ToString());
                            record.dips = Convert.ToDouble(dr["dips"].ToString());
                            record.temperature = Convert.ToDouble(dr["temperature"].ToString());
                            record.financial_year = dr["financial_year"].ToString();
                            record.indication = Convert.ToDouble(dr["indication"].ToString());
                            record.strength = Convert.ToDouble(dr["strength"].ToString());
                            record.inc_groging = Convert.ToDouble(dr["inc_groging"].ToString());
                            record.record_status = dr["record_status"].ToString();
                            record.todispatchvat = dr["todispatchvat"].ToString();
                            //  record.dispatchvat = dr["dispatchvat"].ToString();
                            //record.approval_status = dr["record_status"].ToString();
                            record.receiver_storage_transfer_id = Convert.ToInt32(dr["receiver_storage_transfer_id"].ToString());
                            from84.Add(record);

                        }
                    }

                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return from84;
        }

    }
}
