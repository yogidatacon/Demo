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
    public class DL_ReceiverToStorage_84
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static string Insert(ReceiverToStorage_84 form84)
        {
            string value = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                //where financial_year='"+form84.financial_year+"'
                NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT max(receiver_storage_receipt_id) FROM exciseautomation.receiver_storage_receipt ", cn);
                string m = cmd1.ExecuteScalar().ToString();
                int n = 0;
                if (m == "")
                    n = 1;
                else
                    n = Convert.ToInt32(m) + 1;

                try
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("INSERT INTO exciseautomation.receiver_storage_receipt(receiver_storage_receipt_id, receipt_date, receipt_time, party_code, production_date, to_storagevat, total_bl_receipt, total_lp_receipt, inc_operation, inc_groging, dec_reduction, dec_blending, dec_racking, dec_wastage, dips, temperature, indication, strength, lastmodified_date, user_id, creation_date, record_status,remarks,financial_year)");
                    str.Append("VALUES('"+n+"', '" + form84.receipt_date + "', '" + form84.receipt_time + "', '" + form84.party_code + "', '" + form84.production_date + "', '" + form84.to_storagevat + "', '" + form84.total_bl_receipt + "', '" + form84.total_lp_receipt + "', '" + form84.inc_operation + "', '" + form84.inc_groging + "', '" + form84.dec_reduction + "', '" + form84.dec_blending + "', '" + form84.dec_racking + "', '" + form84.dec_wastage + "', '" + form84.dips + "', '" + form84.temperature + "','" + form84.indication + "', '" + form84.strength + "', '"+DateTime.Now.ToShortDateString()+"', '"+form84.user_id+ "', '" + DateTime.Now.ToShortDateString() + "','"+form84.record_status+"','"+form84.remarks+"','"+form84.financial_year+"')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                   int a= cmd.ExecuteNonQuery();
                    if (a == 1)
                    {
                       
                            NpgsqlCommand cmd3 = new NpgsqlCommand("SELECT max(receiver_storage_receipt_id) FROM exciseautomation.receiver_storage_receipt where financial_year='" + form84.financial_year + "'", cn);
                            int b = Convert.ToInt32(cmd3.ExecuteScalar());

                            for (int i = 0; i < form84.ReceiverReceiptVat.Count; i++)
                        {//where financial_year='" + form84.financial_year + "'
                            NpgsqlCommand cmd4 = new NpgsqlCommand("SELECT max(receiver_storage_receiptvat_id) FROM exciseautomation.receiver_storage_receiptvat ", cn);

                                string f = cmd4.ExecuteScalar().ToString();
                                int c = 0;
                                if (f == "")
                                    c = 1;
                                else
                                    c = Convert.ToInt32(f) + 1;

                                c += 1;
                                a = 0;
                                str = new StringBuilder();
                                str.Append("INSERT INTO exciseautomation.receiver_storage_receiptvat(receiver_storage_receiptvat_id, receiver_storage_receipt_id, from_receivervat, bl_receipt, lp_receipt, lastmodified_date, user_id, creation_date, record_status,financial_year) ");
                                str.Append(" VALUES('" + c + "','" + b + "', '" + form84.ReceiverReceiptVat[i].from_receivervat + "', '" + form84.ReceiverReceiptVat[i].bl_receipt + "', '" + form84.ReceiverReceiptVat[i].lp_receipt + "', '" + DateTime.Now.ToShortDateString() + "', '" + form84.user_id + "', '" + DateTime.Now.ToShortDateString() + "', '" + form84.record_status + "','"+form84.financial_year+"')");

                                NpgsqlCommand cmd5 = new NpgsqlCommand(str.ToString(), cn);
                                a = cmd5.ExecuteNonQuery();
                                if (form84.record_status == "Y")
                                {
                                NpgsqlCommand cmd8 = new NpgsqlCommand("UPDATE exciseautomation.fermenter_receiver_output SET moved_to_nextstage ='Y' WHERE removal_date='" + form84.production_date + "' and to_storagevat='" + form84.to_storagevat + "' and financial_year='" + form84.financial_year + "'", cn);
                                cmd8.ExecuteNonQuery();
                            }
                            }
                        
                    }
                    value = "0";
                }
                catch (Exception ex)
                {
                    value = ex.Message;
                }

                return value;
            }
        }
        public static ReceiverToStorage_84 GetVatAval(string Vat_code, string party_Code, string date,string time, string financial_year)
        {
            ReceiverToStorage_84 vat = new ReceiverToStorage_84();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select case when b.bl_tostorage  is null then 0 else b.bl_tostorage end as bl_tostorage1 ,lp_tostorage   from exciseautomation.fermenter_receiver a inner join exciseautomation.fermenter_receiver_output b on a.fermenter_receiver_id=b.fermenter_receiver_id and a.financial_year=b.financial_year inner join exciseautomation.fermenter_receiver_input c on a.fermenter_receiver_id=c.fermenter_receiver_id and a.financial_year=c.financial_year  where a.party_code='" + party_Code + "' and b.removal_date='" + date + "' and b.removal_hour='"+time+"' and c.receivervat='" + Vat_code + "' and a.financial_year='"+financial_year+"'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            vat.total_bl_receipt = Convert.ToDouble(dr["bl_tostorage1"].ToString());
                            vat.total_lp_receipt = Convert.ToDouble(dr["lp_tostorage"].ToString());
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
        public static string Update(ReceiverToStorage_84 form84)
        {
            string val = "0";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    StringBuilder str = new StringBuilder();
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.receiver_storage_receipt SET  receipt_date ='"+form84.receipt_date+"', receipt_time ='"+form84.receipt_time+"', production_date ='"+form84.production_date+"', to_storagevat ='"+form84.to_storagevat+"', total_bl_receipt ='"+form84.total_bl_receipt+"', total_lp_receipt ='"+form84.total_lp_receipt+"', inc_operation ='"+form84.inc_operation+"', inc_groging ='"+form84.inc_groging+"', dec_reduction ='"+form84.dec_reduction+"', dec_blending ='"+form84.dec_blending+"', dec_racking ='"+form84.dec_racking+"', dec_wastage ='"+form84.dec_wastage+"', dips ='"+form84.dips+"', temperature ='"+form84.temperature+"', indication ='"+form84.indication+"', strength ='"+form84.strength+"', lastmodified_date ='"+DateTime.Now.ToShortDateString()+"', record_status ='"+form84.record_status+ "',remarks='"+form84.remarks+ "' WHERE receiver_storage_receipt_id ='" + form84.receiver_storage_receipt_id+ "' and financial_year='" + form84.financial_year + "'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    int a=cmd.ExecuteNonQuery();
                    if (a == 1)
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("delete from exciseautomation.receiver_storage_receiptvat where receiver_storage_receipt_id='" + form84.receiver_storage_receipt_id+ "' and financial_year='" + form84.financial_year + "'", cn);
                        cmd1.ExecuteNonQuery();
                        for (int i = 0; i < form84.ReceiverReceiptVat.Count; i++)
                        {// where financial_year='" + form84.financial_year + "'
                            NpgsqlCommand cmd4 = new NpgsqlCommand("SELECT max(receiver_storage_receiptvat_id) FROM exciseautomation.receiver_storage_receiptvat", cn);

                            string f = cmd4.ExecuteScalar().ToString();
                            int c = 0;
                            if (f == "")
                                c = 1;
                            else
                                c = Convert.ToInt32(f) + 1;

                            c += 1;
                            a = 0;
                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.receiver_storage_receiptvat(receiver_storage_receiptvat_id, receiver_storage_receipt_id, from_receivervat, bl_receipt, lp_receipt, lastmodified_date, user_id, creation_date, record_status,financial_year) ");
                            str.Append(" VALUES('" + c + "','" + form84.receiver_storage_receipt_id + "', '" + form84.ReceiverReceiptVat[i].from_receivervat + "', '" + form84.ReceiverReceiptVat[i].bl_receipt + "', '" + form84.ReceiverReceiptVat[i].lp_receipt + "', '" + DateTime.Now.ToShortDateString() + "', '" + form84.user_id + "', '" + DateTime.Now.ToShortDateString() + "', '" + form84.record_status + "','"+form84.financial_year+"')");

                            NpgsqlCommand cmd5 = new NpgsqlCommand(str.ToString(), cn);
                            a = cmd5.ExecuteNonQuery();


                            if (form84.record_status == "Y")
                            {
                                NpgsqlCommand cmd8 = new NpgsqlCommand("UPDATE exciseautomation.fermenter_receiver_output SET moved_to_nextstage ='Y' WHERE removal_date='"+form84.production_date+"' and to_storagevat='"+form84.to_storagevat+ "' and financial_year='" + form84.financial_year + "'", cn);
                                cmd8.ExecuteNonQuery();
                            }
                        }
                    }
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

        public static List<ReceiverToStorage_84> GetList()
        {
            List<ReceiverToStorage_84> from84 = new List<ReceiverToStorage_84>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    //if (party_code == null || party_code == "All")
                        cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name,d.vat_name as storagevat from exciseautomation.receiver_storage_receipt a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.to_storagevat=c.vat_code left join exciseautomation.vat_master d on a.to_storagevat = d.vat_code where a.record_active='true' order by a.receipt_date desc", cn);
                   // else
                      //  cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name,d.vat_name as storagevat from exciseautomation.receiver_storage_receipt a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.to_storagevat=c.vat_code left join exciseautomation.vat_master d on a.to_storagevat = d.vat_code where a.party_code='"+party_code+ "' order by a.party_code,a.receipt_date", cn);

                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        from84 = new List<ReceiverToStorage_84>();
                        while (dr.Read())
                        {
                            ReceiverToStorage_84 record = new ReceiverToStorage_84();
                            record.receipt_date = dr["receipt_date"].ToString().Replace("/", "-").Substring(0, 10); ;
                            record.receipt_time = dr["receipt_time"].ToString();
                            record.party_code = dr["party_code"].ToString();
                            record.financial_year = dr["financial_year"].ToString();
                            record.party_name = dr["party_name"].ToString();
                            record.storagevat = dr["storagevat"].ToString();
                            record.to_storagevat = dr["to_storagevat"].ToString();
                            record.total_bl_receipt = Convert.ToDouble( dr["total_bl_receipt"].ToString());
                            record.total_lp_receipt = Convert.ToDouble(dr["total_lp_receipt"].ToString());
                            record.dips = Convert.ToDouble(dr["dips"].ToString());
                            record.temperature = Convert.ToDouble(dr["temperature"].ToString());
                            record.indication = Convert.ToDouble(dr["indication"].ToString());
                            record.strength = Convert.ToDouble(dr["strength"].ToString());
                            record.inc_groging = Convert.ToDouble(dr["inc_groging"].ToString());
                            record.record_status = dr["record_status"].ToString();
                            //record.approval_status = dr["record_status"].ToString();
                            record.receiver_storage_receipt_id = Convert.ToInt32(dr["receiver_storage_receipt_id"].ToString());
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
        public static List<ReceiverToStorage_84> Search(string tablename, string column, string value)
        {
            List<ReceiverToStorage_84> mir = new List<ReceiverToStorage_84>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name,d.vat_name as storagevat from exciseautomation.receiver_storage_receipt a inner join exciseautomation.party_master b on a.party_code=b.party_code inner  join exciseautomation.vat_master d on a.to_storagevat = d.vat_code where  " + column + " Ilike '%" + value + "%' and a.record_active='true'  order by a.receipt_date desc", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            mir = new List<ReceiverToStorage_84>();
                            while (dr.Read())
                            {
                                ReceiverToStorage_84 record = new ReceiverToStorage_84();
                                record.receipt_date = dr["receipt_date"].ToString().Replace("/", "-").Substring(0, 10); ;
                                record.receipt_time = dr["receipt_time"].ToString();
                                record.party_code = dr["party_code"].ToString();
                                record.financial_year = dr["financial_year"].ToString();
                                record.party_name = dr["party_name"].ToString();
                                record.storagevat = dr["storagevat"].ToString();
                                record.to_storagevat = dr["to_storagevat"].ToString();
                                record.total_bl_receipt = Convert.ToDouble(dr["total_bl_receipt"].ToString());
                                record.total_lp_receipt = Convert.ToDouble(dr["total_lp_receipt"].ToString());
                                record.dips = Convert.ToDouble(dr["dips"].ToString());
                                record.temperature = Convert.ToDouble(dr["temperature"].ToString());
                                record.indication = Convert.ToDouble(dr["indication"].ToString());
                                record.strength = Convert.ToDouble(dr["strength"].ToString());
                                record.inc_groging = Convert.ToDouble(dr["inc_groging"].ToString());
                                record.record_status = dr["record_status"].ToString();
                                //record.approval_status = dr["record_status"].ToString();
                                record.receiver_storage_receipt_id = Convert.ToInt32(dr["receiver_storage_receipt_id"].ToString());
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



        public static ReceiverToStorage_84 GetDetails(string party_code, string form84id, string financial_year)
        {
            ReceiverToStorage_84 record = new ReceiverToStorage_84();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name from exciseautomation.receiver_storage_receipt a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.to_storagevat=c.vat_code where a.party_code='" + party_code + "' and a.receiver_storage_receipt_id='" + form84id + "' and a.financial_year='"+financial_year+"'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            record.receipt_date = dr["receipt_date"].ToString().Replace("/", "-").Substring(0, 10);
                            record.receipt_time= dr["receipt_time"].ToString();
                            record.production_date= Convert.ToDateTime( dr["production_date"]).ToString("dd-MM-yyyy");
                            record.party_code = dr["party_code"].ToString();
                            record.party_name = dr["party_name"].ToString();
                               record.storagevat = dr["vat_name"].ToString();
                           // record.from_receivervat = dr["from_receivervat"].ToString();
                            record.to_storagevat = dr["to_storagevat"].ToString();
                            record.total_bl_receipt = Convert.ToDouble(dr["total_bl_receipt"].ToString());
                            record.total_lp_receipt = Convert.ToDouble(dr["total_lp_receipt"].ToString());
                            record.record_status = dr["record_status"].ToString();
                            record.receiver_storage_receipt_id = Convert.ToInt32(dr["receiver_storage_receipt_id"].ToString());
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
                            //record.fermenter_receiver_output_id= Convert.ToInt32(dr["fermenter_receiver_output_id"].ToString());
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
        public static string GetVatData(string vat_code,string party_code)
        {
            string vat_data = "0";
            double to_receivervat=0;
            double from_receivervat=0;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                   
                    NpgsqlCommand cmd = new NpgsqlCommand("select  to_receivervat,sum(transferqty) as transferqty from exciseautomation.form83   where to_receivervat='" + vat_code + "' and party_code='"+party_code+ "' and record_status='A' group by to_receivervat", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                             to_receivervat = Convert.ToDouble(dr["transferqty"].ToString());
                        }
                    }
                    dr.Close();
                    cmd = new NpgsqlCommand("select  from_receivervat,sum(transferqty) as transferqty from exciseautomation.form84   where from_receivervat='" + vat_code + "' and party_code='" + party_code + "' and record_status='A' group by from_receivervat", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            from_receivervat = Convert.ToDouble(dr["transferqty"].ToString());

                            //  
                        }
                        
                    }
                    dr.Close();
                    vat_data = (to_receivervat - from_receivervat).ToString();
                    _log.Info("Get GetVatData84 Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get GetVatData84 Fail :" + ex.Message);
                }
            }
            return vat_data;
        }
        public static string Approve(ReceiverToStorage_84 form84)
        {
            string value;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.receiver_storage_receipt SET  record_status='" + form84.record_status + "' WHERE receiver_storage_receipt_id='" + form84.receiver_storage_receipt_id + "' and financial_year='" + form84.financial_year + "'", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (form84.record_status == "R")
                    {
                        form84.record_status = "Rejected by Bond Officer";
                        NpgsqlCommand cmd8 = new NpgsqlCommand("UPDATE exciseautomation.fermenter_receiver_output SET moved_to_nextstage ='N' WHERE removal_date='" + form84.production_date + "' and to_storagevat='" + form84.to_storagevat + "' and financial_year='" + form84.financial_year + "'", cn);
                        cmd8.ExecuteNonQuery();
                    }
                    else
                    {
                        NpgsqlCommand cmd6 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + form84.to_storagevat + "' and party_code='" + form84.party_code + "'", cn);
                        double a = Convert.ToDouble(cmd6.ExecuteScalar());
                        double c = Convert.ToDouble(a) +(Convert.ToDouble(form84.total_bl_receipt) +Convert.ToDouble(form84.inc_groging) + Convert.ToDouble(form84.inc_operation)) - (Convert.ToDouble(form84.dec_blending) + Convert.ToDouble(form84.dec_racking) + Convert.ToDouble(form84.dec_reduction) + Convert.ToDouble(form84.dec_wastage));
                        NpgsqlCommand cmd7 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + c + "'  where vat_code='" + form84.to_storagevat + "' and party_code='" + form84.party_code + "'", cn);
                        cmd7.ExecuteNonQuery();
                        form84.record_status = "Approved by Bond Officer";
                    }
                    StringBuilder str = new StringBuilder();
                    str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                    str.Append("'" + form84.receiver_storage_receipt_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "','RTS','" + form84.record_status + "','" + form84.remarks + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + form84.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + form84.user_id + "','"+form84.financial_year+"','"+form84.party_code+"')");
                    cmd = new NpgsqlCommand(str.ToString(), cn);
                    n = cmd.ExecuteNonQuery();
                    value = "0";
                    trn.Commit();
                    _log.Info("Form84 Approve Sucess:" + form84.receiver_storage_receipt_id);
                    cn.Close();
                }
                catch (Exception ex)
                {
                    value = ex.Message;
                    _log.Info("Form84 Approve Fail:" + form84.receiver_storage_receipt_id);
                    trn.Rollback();
                }
                return value;

            }
        }


        public static List<FReceiverOuput> GetListreceiverop(string party_code)
        {
            List<FReceiverOuput> from83 = new List<FReceiverOuput>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    if (party_code == null || party_code == "All")
                        cmd = new NpgsqlCommand("SELECT distinct(a.removal_date) as removal_date FROM exciseautomation.fermenter_receiver_output a inner join  exciseautomation.fermenter_receiver b on a.fermenter_receiver_id=b.fermenter_receiver_id where a.moved_to_nextstage='N' and b.record_status='A'  order by a.removal_date ", cn);
                    else
                        cmd = new NpgsqlCommand("SELECT distinct(a.removal_date) as removal_date FROM exciseautomation.fermenter_receiver_output a inner join  exciseautomation.fermenter_receiver b on a.fermenter_receiver_id=b.fermenter_receiver_id where a.moved_to_nextstage='N' and b.record_status='A' and b.party_code='" + party_code+"' order by a.removal_date ", cn);

                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        from83 = new List<FReceiverOuput>();
                        while (dr.Read())
                        {
                            FReceiverOuput record = new FReceiverOuput();

                           // record.fermenter_receiver_output_id = Convert.ToInt32(dr["fermenter_receiver_output_id"].ToString());
                           // record.fermenter_receiver_id= Convert.ToInt32(dr["fermenter_receiver_id"].ToString());
                            record.removal_date = Convert.ToDateTime(dr["removal_date"]).ToString("dd-MM-yyyy");
                          //  record.to_storagevat = dr["to_storagevat"].ToString();
                          //  record.vat_name= dr["vat_name"].ToString();
                            from83.Add(record);
                        }
                        _log.Info("Get VAT Master List Success");
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Get VAT Master List Fail :" + ex.Message);
                }
            }
            return from83;
        }


        public static List<FReceiverOuput> GetStoragevat(string party,string date, string financial_year)
        {
            List<FReceiverOuput> from83 = new List<FReceiverOuput>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    cmd = new NpgsqlCommand("SELECT distinct(a.to_storagevat) ,d.vat_name,a.moved_to_nextstage FROM exciseautomation.fermenter_receiver_output a inner join  exciseautomation.fermenter_receiver b on a.fermenter_receiver_id=b.fermenter_receiver_id and a.financial_year=b.financial_year inner join  exciseautomation.party_master c on b.party_code=c.party_code inner join exciseautomation.vat_master d on a.to_storagevat=d.vat_code where b.party_code='" + party+"' and a.removal_date='"+date+"' and a.financial_year='"+financial_year+"'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        from83 = new List<FReceiverOuput>();
                        while (dr.Read())
                        {
                            FReceiverOuput record = new FReceiverOuput();

                           // record.fermenter_receiver_output_id = Convert.ToInt32(dr["fermenter_receiver_output_id"].ToString());
                           // record.fermenter_receiver_id = Convert.ToInt32(dr["fermenter_receiver_id"].ToString());
                           // record.removal_date = Convert.ToDateTime(dr["removal_date"]).ToString("dd-MM-yyyy");
                            record.to_storagevat = dr["to_storagevat"].ToString();
                            record.vat_name = dr["vat_name"].ToString();
                            //record.bl_tostorage = Convert.ToDouble(dr["bl_tostorage"].ToString());
                           // record.lp_tostorage = Convert.ToDouble(dr["lp_tostorage"].ToString());
                           record.moved_to_nextstage = dr["moved_to_nextstage"].ToString();
                            from83.Add(record);
                        }
                        _log.Info("Get VAT Master List Success");
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Get VAT Master List Fail :" + ex.Message);
                }
            }
            return from83;
        }







        public static List<FReceiverOuput> GetReceiverVAtdetails(string vat_code,int ID)
        {
            List<FReceiverOuput> from83 = new List<FReceiverOuput>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                        cmd = new NpgsqlCommand("SELECT a.*,d.vat_name FROM exciseautomation.fermenter_receiver_output a inner join  exciseautomation.fermenter_receiver b on a.fermenter_receiver_id=b.fermenter_receiver_id inner join  exciseautomation.party_master c on b.party_code=c.party_code inner join exciseautomation.vat_master d on a.to_storagevat=d.vat_code where a.to_storagevat='"+vat_code+ "' and a.fermenter_receiver_output_id='"+ID+"'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        from83 = new List<FReceiverOuput>();
                        while (dr.Read())
                        {
                            FReceiverOuput record = new FReceiverOuput();

                            record.fermenter_receiver_output_id = Convert.ToInt32(dr["fermenter_receiver_output_id"].ToString());
                            record.fermenter_receiver_id = Convert.ToInt32(dr["fermenter_receiver_id"].ToString());
                            record.removal_date = Convert.ToDateTime(dr["removal_date"]).ToString("dd-MM-yyyy");
                            record.removal_hour = dr["removal_hour"].ToString();
                            record.to_storagevat = dr["to_storagevat"].ToString();
                            record.vat_name = dr["vat_name"].ToString();
                            record.bl_tostorage = Convert.ToDouble(dr["bl_tostorage"].ToString());
                            record.lp_tostorage= Convert.ToDouble(dr["lp_tostorage"].ToString());
                            record.moved_to_nextstage= dr["moved_to_nextstage"].ToString();
                            from83.Add(record);
                        }
                        _log.Info("Get VAT Master List Success");
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Get VAT Master List Fail :" + ex.Message);
                }
            }
            return from83;
        }

        public static List<FReceiverInput> GetReceiverVAt(string vat_code, string date,string party, string financial_year)
        {
            List<FReceiverInput> from83 = new List<FReceiverInput>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    cmd = new NpgsqlCommand("SELECT a.*,d.vat_name,b.bl_tostorage,b.lp_tostorage,b.removal_hour FROM exciseautomation.fermenter_receiver_input  a inner join  exciseautomation.fermenter_receiver_output b on a.fermenter_receiver_id=b.fermenter_receiver_id and a.financial_year=b.financial_year  inner join exciseautomation.fermenter_receiver f on a.fermenter_receiver_id=f.fermenter_receiver_id and a.financial_year=f.financial_year    inner join exciseautomation.vat_master d on a.receivervat=d.vat_code where f.record_status='A' and f.party_code='" + party+"'and b.to_storagevat='"+vat_code+"' and b.removal_date='"+date+"' and a.financial_year='"+financial_year+"'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        from83 = new List<FReceiverInput>();
                        while (dr.Read())
                        {
                            FReceiverInput Setup = new FReceiverInput();
                            Setup.fermenter_receiver_input_id = Convert.ToInt32(dr["fermenter_receiver_input_id"].ToString());
                            Setup.fermenter_receiver_id = Convert.ToInt32(dr["fermenter_receiver_id"].ToString());
                            Setup.fermentervat = dr["fermentervat"].ToString();
                            Setup.financial_year = dr["financial_year"].ToString();
                            Setup.vat_name = dr["vat_name"].ToString();
                            Setup.receivervat = dr["receivervat"].ToString();
                            Setup.dips = Convert.ToDouble(dr["dips"].ToString());
                            Setup.temperature = Convert.ToDouble(dr["temperature"].ToString());
                            Setup.indication = Convert.ToDouble(dr["indication"].ToString());
                            Setup.removal_hour = dr["removal_hour"].ToString();
                            Setup.strength = Convert.ToDouble(dr["strength"].ToString());
                            Setup.bl_received = Convert.ToDouble(dr["bl_received"].ToString());
                            Setup.lp_received = Convert.ToDouble(dr["lp_received"].ToString());
                            from83.Add(Setup);
                        }
                        _log.Info("Get VAT Master List Success");
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Get VAT Master List Fail :" + ex.Message);
                }
            }
            return from83;
        }

        public static List<FReceiverInput> GetdistinctVAt(string vat_code, string date, string party,string financial_year)
        {
            List<FReceiverInput> from83 = new List<FReceiverInput>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    cmd = new NpgsqlCommand("SELECT distinct(f.fermenter_receiver_id) as fermenter_receiver_id FROM exciseautomation.fermenter_receiver_input  a inner join  exciseautomation.fermenter_receiver_output b on a.fermenter_receiver_id=b.fermenter_receiver_id and a.financial_year=b.financial_year  inner join exciseautomation.fermenter_receiver f on a.fermenter_receiver_id=f.fermenter_receiver_id and a.financial_year=f.financial_year    inner join exciseautomation.vat_master d on a.receivervat=d.vat_code where f.record_status='A' and f.party_code='" + party + "'and b.to_storagevat='" + vat_code + "' and b.removal_date='" + date + "' and a.financial_year='"+financial_year+"'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        from83 = new List<FReceiverInput>();
                        while (dr.Read())
                        {
                            FReceiverInput Setup = new FReceiverInput();
                          //  Setup.fermenter_receiver_input_id = Convert.ToInt32(dr["fermenter_receiver_input_id"].ToString());
                            Setup.fermenter_receiver_id = Convert.ToInt32(dr["fermenter_receiver_id"].ToString());
                            //Setup.fermentervat = dr["fermentervat"].ToString();
                            //Setup.vat_name = dr["vat_name"].ToString();
                            //Setup.receivervat = dr["receivervat"].ToString();
                            //Setup.dips = Convert.ToDouble(dr["dips"].ToString());
                            //Setup.temperature = Convert.ToDouble(dr["temperature"].ToString());
                            //Setup.indication = Convert.ToDouble(dr["indication"].ToString());
                            //Setup.removal_hour = dr["removal_hour"].ToString();
                            //Setup.strength = Convert.ToDouble(dr["strength"].ToString());
                            //Setup.bl_received = Convert.ToDouble(dr["bl_received"].ToString());
                            //Setup.lp_received = Convert.ToDouble(dr["lp_received"].ToString());
                            from83.Add(Setup);
                        }
                        _log.Info("Get VAT Master List Success");
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Get VAT Master List Fail :" + ex.Message);
                }
            }
            return from83;
        }

        public static List<FReceiverInput> GetReceiver(int id, string financial_year)
        {
            List<FReceiverInput> from83 = new List<FReceiverInput>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    cmd = new NpgsqlCommand("SELECT a.*,d.vat_name FROM exciseautomation.fermenter_receiver_input  a  inner join exciseautomation.fermenter_receiver f on a.fermenter_receiver_id=f.fermenter_receiver_id and a.financial_year=f.financial_year    inner join exciseautomation.vat_master d on a.receivervat=d.vat_code where f.record_status='A' and f.fermenter_receiver_id='" + id+"' and a.financial_year='"+financial_year+"'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        from83 = new List<FReceiverInput>();
                        while (dr.Read())
                        {
                            FReceiverInput Setup = new FReceiverInput();
                            Setup.fermenter_receiver_input_id = Convert.ToInt32(dr["fermenter_receiver_input_id"].ToString());
                            Setup.fermenter_receiver_id = Convert.ToInt32(dr["fermenter_receiver_id"].ToString());
                            //Setup.fermentervat = dr["fermentervat"].ToString();
                            Setup.financial_year = dr["financial_year"].ToString();
                            Setup.vat_name = dr["vat_name"].ToString();
                            Setup.receivervat = dr["receivervat"].ToString();
                            Setup.dips = Convert.ToDouble(dr["dips"].ToString());
                            Setup.temperature = Convert.ToDouble(dr["temperature"].ToString());
                            Setup.indication = Convert.ToDouble(dr["indication"].ToString());
                           // Setup.removal_hour = dr["removal_hour"].ToString();
                            Setup.strength = Convert.ToDouble(dr["strength"].ToString());
                            Setup.bl_received = Convert.ToDouble(dr["bl_received"].ToString());
                            Setup.lp_received = Convert.ToDouble(dr["lp_received"].ToString());
                            from83.Add(Setup);
                        }
                        _log.Info("Get VAT Master List Success");
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Get VAT Master List Fail :" + ex.Message);
                }
            }
            return from83;
        }


        public static List<FReceiverInput> GetReceiverVAt1(string vat_code, string date, string party, string financial_year)
        {
            List<FReceiverInput> from83 = new List<FReceiverInput>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    cmd = new NpgsqlCommand("SELECT a.*,b.bl_tostorage,b.lp_tostorage,d.vat_name,b.removal_hour FROM exciseautomation.fermenter_receiver_input  a inner join  exciseautomation.fermenter_receiver_output b on a.fermenter_receiver_id=b.fermenter_receiver_id and a.financial_year=b.financial_year  inner join exciseautomation.fermenter_receiver f on a.fermenter_receiver_id=f.fermenter_receiver_id and financial_year=f.financial_year    inner join exciseautomation.vat_master d on a.receivervat=d.vat_code where f.record_status='A' and f.party_code='" + party + "'and b.to_storagevat='" + vat_code + "' and b.removal_date='" + date + "' and a.financial_year='"+financial_year+"'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        from83 = new List<FReceiverInput>();
                        while (dr.Read())
                        {
                            FReceiverInput Setup = new FReceiverInput();
                            Setup.fermenter_receiver_input_id = Convert.ToInt32(dr["fermenter_receiver_input_id"].ToString());
                            Setup.fermenter_receiver_id = Convert.ToInt32(dr["fermenter_receiver_id"].ToString());
                            Setup.fermentervat = dr["fermentervat"].ToString();
                            Setup.financial_year = dr["financial_year"].ToString();
                            Setup.removal_hour= dr["removal_hour"].ToString();
                            Setup.vat_name = dr["vat_name"].ToString();
                            Setup.receivervat = dr["receivervat"].ToString();
                            Setup.dips = Convert.ToDouble(dr["dips"].ToString());
                            Setup.temperature = Convert.ToDouble(dr["temperature"].ToString());
                            Setup.indication = Convert.ToDouble(dr["indication"].ToString());
                            Setup.strength = Convert.ToDouble(dr["strength"].ToString());
                            Setup.bl_received = Convert.ToDouble(dr["bl_tostorage"].ToString());
                            Setup.lp_received = Convert.ToDouble(dr["lp_tostorage"].ToString());
                            from83.Add(Setup);
                        }
                        _log.Info("Get VAT Master List Success");
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Get VAT Master List Fail :" + ex.Message);
                }
            }
            return from83;
        }

        public static List<FReceiverInput> GetSTOVAt(int id, string vat_code, string financial_year)
        {
            List<FReceiverInput> from83 = new List<FReceiverInput>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    cmd = new NpgsqlCommand("SELECT a.*,b.bl_tostorage,b.lp_tostorage,d.vat_name,b.removal_hour FROM exciseautomation.fermenter_receiver_input  a inner join  exciseautomation.fermenter_receiver_output b on a.fermenter_receiver_id=b.fermenter_receiver_id and a.financial_year=b.financial_year  inner join exciseautomation.fermenter_receiver f on a.fermenter_receiver_id=f.fermenter_receiver_id and a.financial_year=f.financial_year    inner join exciseautomation.vat_master d on a.receivervat=d.vat_code where f.record_status='A' and f.fermenter_receiver_id='"+id+ "'and b.to_storagevat='" + vat_code + "' and a.financial_year='"+financial_year+"'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        from83 = new List<FReceiverInput>();
                        while (dr.Read())
                        {
                            FReceiverInput Setup = new FReceiverInput();
                            Setup.fermenter_receiver_input_id = Convert.ToInt32(dr["fermenter_receiver_input_id"].ToString());
                            Setup.fermenter_receiver_id = Convert.ToInt32(dr["fermenter_receiver_id"].ToString());
                            Setup.fermentervat = dr["fermentervat"].ToString();
                            Setup.financial_year = dr["financial_year"].ToString();
                            Setup.removal_hour = dr["removal_hour"].ToString();
                            Setup.vat_name = dr["vat_name"].ToString();
                            Setup.receivervat = dr["receivervat"].ToString();
                            Setup.dips = Convert.ToDouble(dr["dips"].ToString());
                            Setup.temperature = Convert.ToDouble(dr["temperature"].ToString());
                            Setup.indication = Convert.ToDouble(dr["indication"].ToString());
                            Setup.strength = Convert.ToDouble(dr["strength"].ToString());
                            Setup.bl_received = Convert.ToDouble(dr["bl_tostorage"].ToString());
                            Setup.lp_received = Convert.ToDouble(dr["lp_tostorage"].ToString());
                            from83.Add(Setup);
                        }
                        _log.Info("Get VAT Master List Success");
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Get VAT Master List Fail :" + ex.Message);
                }
            }
            return from83;
        }
        public static List<FReceiverOuput> Getsubmiteddate(string party_code)
        {
            List<FReceiverOuput> from83 = new List<FReceiverOuput>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                  
                        cmd = new NpgsqlCommand("SELECT distinct(a.removal_date) as removal_date FROM exciseautomation.fermenter_receiver_output a inner join  exciseautomation.fermenter_receiver b on a.fermenter_receiver_id=b.fermenter_receiver_id where a.moved_to_nextstage='Y' and b.record_status='A' and b.party_code='" + party_code+"' order by a.removal_date", cn);

                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        from83 = new List<FReceiverOuput>();
                        while (dr.Read())
                        {
                            FReceiverOuput record = new FReceiverOuput();

                          //  record.fermenter_receiver_output_id = Convert.ToInt32(dr["fermenter_receiver_output_id"].ToString());
                           // record.fermenter_receiver_id = Convert.ToInt32(dr["fermenter_receiver_id"].ToString());
                            record.removal_date = Convert.ToDateTime(dr["removal_date"]).ToString("dd-MM-yyyy");
                           // record.to_storagevat = dr["to_storagevat"].ToString();
                          //  record.vat_name = dr["vat_name"].ToString();
                            from83.Add(record);
                        }
                        _log.Info("Get VAT Master List Success");
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Get VAT Master List Fail :" + ex.Message);
                }
            }
            return from83;
        }

        public static FReceiverOuput Gettotal(string vat_code, string userid, string date, string financial_year)
        {
            FReceiverOuput vat = new FReceiverOuput();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select sum(a.bl_tostorage) as bl,sum(a.lp_tostorage) as lp from exciseautomation.fermenter_receiver_output a inner join exciseautomation.fermenter_receiver b on a.fermenter_receiver_id = b.fermenter_receiver_id and a.financial_year=b.financial_year where a.to_storagevat='"+vat_code+"' and a.removal_date='"+date+"' and b.party_code='"+userid+ "' and b.record_status='A' and a.financial_year='"+financial_year+"'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            vat.bl_tostorage = Convert.ToDouble(dr["bl"].ToString());
                            vat.lp_tostorage= Convert.ToDouble(dr["lp"].ToString());
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



        public static List<FReceiverInput> GetReceiverVAtvalue(string vat_code, string userid, string date, string financial_year)
        {
            List<FReceiverInput> from83 = new List<FReceiverInput>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    cmd = new NpgsqlCommand("select a.*,c.receivervat,d.vat_name from exciseautomation.fermenter_receiver_output a inner join exciseautomation.fermenter_receiver b on a.fermenter_receiver_id = b.fermenter_receiver_id and a.financial_year=b.financial_year inner join exciseautomation.fermenter_receiver_input c on  a.fermenter_receiver_id = c.fermenter_receiver_id and a.financial_year=c.financial_year  inner join exciseautomation.vat_master d on c.receivervat=d.vat_code  where a.to_storagevat='"+vat_code+"' and a.removal_date='"+date+ "' and  b.party_code='" + userid + "' and b.record_status='A' and a.financial_year='"+financial_year+"'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        from83 = new List<FReceiverInput>();
                        while (dr.Read())
                        {
                            FReceiverInput Setup = new FReceiverInput();
                            Setup.fermenter_receiver_input_id = Convert.ToInt32(dr["fermenter_receiver_output_id"].ToString());
                            Setup.fermenter_receiver_id = Convert.ToInt32(dr["fermenter_receiver_id"].ToString());
                            Setup.vat_name = dr["vat_name"].ToString();
                            Setup.receivervat = dr["receivervat"].ToString();
                            Setup.bl_received = Convert.ToDouble(dr["bl_tostorage"].ToString());
                            Setup.lp_received = Convert.ToDouble(dr["lp_tostorage"].ToString());
                            from83.Add(Setup);
                        }
                        _log.Info("Get VAT Master List Success");
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Get VAT Master List Fail :" + ex.Message);
                }
            }
            return from83;
        }

    }
}
