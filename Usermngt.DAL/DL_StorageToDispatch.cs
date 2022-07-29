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
   public class DL_StorageToDispatch
    {
        public static string Insert(StorageToDispatch frm84D)
        {
            string value = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                
                cn.Open();
              //  where financial_year = '"+frm84D.financial_year+"'
                NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT max(storage_dispatch_id) FROM exciseautomation.storage_dispatch ", cn);
                string m = cmd1.ExecuteScalar().ToString();
                int n = 0;
                if (m == "")
                    n = 1;
                else
                    n = Convert.ToInt32(m) + 1;

                try
                {
                    NpgsqlCommand cmd2 = new NpgsqlCommand("SELECT fromstoragevat FROM exciseautomation.receiver_storage_transfer where receiver_storage_transfer_id = '"+frm84D.receiver_storage_transfer_id+ "' and financial_year='" + frm84D.financial_year + "' ", cn);
                    string b = cmd2.ExecuteScalar().ToString();
                    StringBuilder str = new StringBuilder();
                    str.Append("INSERT INTO exciseautomation.storage_dispatch(storage_dispatch_id, receipt_date, receipt_hour, transfer_date, party_code, from_storagevat, to_dispatchvat, total_bl_receipt, total_lp_receipt, denaturing_agent, denatured_qty, hasqtyincreased, inc_operation, inc_groging, dec_reduction, dec_blending, dec_racking, dec_wastage, dips, temperature, indication, strength, bl_balanceqty, lp_balanceqty, remarks, lastmodified_date, user_id, creation_date, record_status,receiver_storage_transfer_id,moved_to_nextstage,financial_year) ");
                    str.Append("VALUES('" + n + "', '" +frm84D.receipt_date + "', '" +frm84D.receipt_hour + "', '" + frm84D.transfer_date + "','" + frm84D.party_code + "', '" + b + "', '"+frm84D.to_dispatchvat+"', '" + frm84D.total_bl_receipt + "', '" + frm84D.total_lp_receipt + "', '" + frm84D.denaturing_agent + "','" + frm84D.denatured_qty + "','" + frm84D.hasqtyincreased + "','" + frm84D.inc_operation + "', '" + frm84D.inc_groging + "', '" + frm84D.dec_reduction + "', '" + frm84D.dec_blending + "', '" + frm84D.dec_racking + "', '" + frm84D.dec_wastage + "', '" + frm84D.dips + "', '" + frm84D.temperature + "','" + frm84D.indication + "', '" + frm84D.strength + "', '" + frm84D.bl_balanceqty + "','" + frm84D.lp_balanceqty + "','" + frm84D.remarks + "','" + DateTime.Now.ToShortDateString() + "', '" + frm84D.user_id + "', '" + DateTime.Now.ToShortDateString() + "','" + frm84D.record_status + "','"+frm84D.receiver_storage_transfer_id+"','N','"+frm84D.financial_year+"')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    cmd.ExecuteNonQuery();

                    if (frm84D.record_status == "Y")
                    {
                        //NpgsqlCommand cmd8 = new NpgsqlCommand("UPDATE exciseautomation.receiver_storage_transfer SET moved_to_nextstage ='Y' WHERE  receiver_storage_transfer_id ='" + frm84D.receiver_storage_transfer_id + "'", cn);
                        //cmd8.ExecuteNonQuery();
                      



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

        public static string Update(StorageToDispatch frm84D)
        {
            string val = "0";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    //NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.storage_dispatch SET  receipt_date ='"+frm84D.receipt_date+"', receipt_hour ='"+frm84D.receipt_hour+"', transfer_date ='"+frm84D.transfer_date+"', to_dispatchvat ='"+frm84D.to_dispatchvat+"', total_bl_receipt ='"+frm84D.total_bl_receipt+"', total_lp_receipt ='"+frm84D.total_lp_receipt+"', denaturing_agent ='"+frm84D.denaturing_agent+"', denatured_qty ='"+frm84D.denatured_qty+"', hasqtyincreased ='"+frm84D.hasqtyincreased+"', inc_operation ='"+frm84D.inc_operation+"', inc_groging ='"+frm84D.inc_groging+"', dec_reduction ='"+frm84D.dec_reduction+"', dec_blending ='"+frm84D.dec_blending+"', dec_racking ='"+frm84D.dec_racking+"', dec_wastage ='"+frm84D.dec_wastage+"', dips ='"+frm84D.dips+"', temperature ='"+frm84D.temperature+"', indication ='"+frm84D.indication+"', strength ='"+frm84D.strength+"', bl_balanceqty ='"+frm84D.bl_balanceqty+"', lp_balanceqty ='"+frm84D.lp_balanceqty+"', remarks ='"+frm84D.remarks+"', lastmodified_date ='"+DateTime.Now.ToShortDateString()+"' , record_status ='"+frm84D.record_status+ "', receiver_storage_transfer_id='"+frm84D.receiver_storage_transfer_id+"'  WHERE storage_dispatch_id ='" + frm84D.storage_dispatch_id+"' ", cn);
                    //cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlCommand cmd2 = new NpgsqlCommand("SELECT fromstoragevat FROM exciseautomation.receiver_storage_transfer where receiver_storage_transfer_id = '" + frm84D.receiver_storage_transfer_id + "' and financial_year='" + frm84D.financial_year + "'", cn);
                    string b = cmd2.ExecuteScalar().ToString();
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.storage_dispatch SET  receipt_date ='" + frm84D.receipt_date + "', receipt_hour ='" + frm84D.receipt_hour + "',from_storagevat='" + b + "', transfer_date ='" + frm84D.transfer_date + "', to_dispatchvat ='" + frm84D.to_dispatchvat + "', total_bl_receipt ='" + frm84D.total_bl_receipt + "', total_lp_receipt ='" + frm84D.total_lp_receipt + "', denaturing_agent ='" + frm84D.denaturing_agent + "', denatured_qty ='" + frm84D.denatured_qty + "', hasqtyincreased ='" + frm84D.hasqtyincreased + "', inc_operation ='" + frm84D.inc_operation + "', inc_groging ='" + frm84D.inc_groging + "', dec_reduction ='" + frm84D.dec_reduction + "', dec_blending ='" + frm84D.dec_blending + "', dec_racking ='" + frm84D.dec_racking + "', dec_wastage ='" + frm84D.dec_wastage + "', dips ='" + frm84D.dips + "', temperature ='" + frm84D.temperature + "', indication ='" + frm84D.indication + "', strength ='" + frm84D.strength + "', bl_balanceqty ='" + frm84D.bl_balanceqty + "', lp_balanceqty ='" + frm84D.lp_balanceqty + "', remarks ='" + frm84D.remarks + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "' , record_status ='" + frm84D.record_status + "', receiver_storage_transfer_id='" + frm84D.receiver_storage_transfer_id + "'  WHERE storage_dispatch_id ='" + frm84D.storage_dispatch_id + "' and financial_year='" + frm84D.financial_year + "' ", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();
                    if(frm84D.record_status=="Y")
                    {
                    //NpgsqlCommand cmd8 = new NpgsqlCommand("UPDATE exciseautomation.receiver_storage_transfer SET moved_to_nextstage ='Y' WHERE  receiver_storage_transfer_id ='" +frm84D.receiver_storage_transfer_id+ "'", cn);
                    //cmd8.ExecuteNonQuery();
                        //if (frm84D.hasqtyincreased == "Y")
                        //{
                        //    NpgsqlCommand cmd6 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + frm84D.to_dispatchvat + "' and party_code='" + frm84D.party_code + "'", cn);
                        //    double a = Convert.ToDouble(cmd6.ExecuteScalar());
                        //    double c = Convert.ToDouble(a) +  (Convert.ToDouble(frm84D.denatured_qty) + Convert.ToDouble(frm84D.inc_groging) + Convert.ToDouble(frm84D.inc_operation)) - (Convert.ToDouble(frm84D.dec_blending) + Convert.ToDouble(frm84D.dec_racking) + Convert.ToDouble(frm84D.dec_reduction) + Convert.ToDouble(frm84D.dec_wastage));
                        //    NpgsqlCommand cmd7 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + c + "'  where vat_code='" + frm84D.to_dispatchvat + "' and party_code='" + frm84D.party_code + "'", cn);
                        //    cmd7.ExecuteNonQuery();
                        //}
                        //else
                        //{
                        //    NpgsqlCommand cmd6 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + frm84D.to_dispatchvat + "' and party_code='" + frm84D.party_code + "'", cn);
                        //    double a = Convert.ToDouble(cmd6.ExecuteScalar());
                        //    double c = Convert.ToDouble(a) + (Convert.ToDouble(frm84D.inc_groging) + Convert.ToDouble(frm84D.inc_operation)) - (Convert.ToDouble(frm84D.dec_blending) + Convert.ToDouble(frm84D.dec_racking) + Convert.ToDouble(frm84D.dec_reduction) + Convert.ToDouble(frm84D.dec_wastage));
                        //    NpgsqlCommand cmd7 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + c + "'  where vat_code='" + frm84D.to_dispatchvat + "' and party_code='" + frm84D.party_code + "'", cn);
                        //    cmd7.ExecuteNonQuery();
                        //}

                    }
                    val = "0";
                }
                catch (Exception ex)
                {
                    val = ex.Message;
                }
            }
            return val;
        }



        public static List<StorageToDispatch> GetList()
        {
            List<StorageToDispatch> frm84D = new List<StorageToDispatch>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                  //  if (party_code == null || party_code == "All")
                        cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name as dispatchvat  from exciseautomation.storage_dispatch a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.to_dispatchvat=c.vat_code  order by a.receipt_date desc", cn);
                   // else
                     //   cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name as dispatchvat  from exciseautomation.storage_dispatch a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.to_dispatchvat=c.vat_code where b.party_code='" + party_code+ "'  order by a.party_code,a.receipt_date", cn);

                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        frm84D = new List<StorageToDispatch>();
                        while (dr.Read())
                        {
                            StorageToDispatch record = new StorageToDispatch();
                            record.receipt_date = dr["receipt_date"].ToString().Replace("/", "-").Substring(0, 10); ;
                            record.receipt_hour = dr["receipt_hour"].ToString();
                            record.transfer_date = Convert.ToDateTime( dr["transfer_date"]).ToString("dd-MM-yyyy");
                            record.party_code = dr["party_code"].ToString();
                            record.financial_year = dr["financial_year"].ToString();
                            record.party_name = dr["party_name"].ToString();
                            record.dispatchvat = dr["dispatchvat"].ToString();
                            record.to_dispatchvat = dr["to_dispatchvat"].ToString();
                            record.from_storagevat = dr["from_storagevat"].ToString();
                            record.total_bl_receipt = Convert.ToDouble(dr["total_bl_receipt"].ToString());
                            record.total_lp_receipt = Convert.ToDouble(dr["total_lp_receipt"].ToString());
                            record.dips = Convert.ToDouble(dr["dips"].ToString());
                            record.temperature = Convert.ToDouble(dr["temperature"].ToString());
                            record.indication = Convert.ToDouble(dr["indication"].ToString());
                            record.strength = Convert.ToDouble(dr["strength"].ToString());
                            record.inc_groging = Convert.ToDouble(dr["inc_groging"].ToString());
                            record.bl_balanceqty = Convert.ToDouble(dr["bl_balanceqty"].ToString());
                            record.lp_balanceqty = Convert.ToDouble(dr["lp_balanceqty"].ToString());
                            record.record_status = dr["record_status"].ToString();
                            record.denaturing_agent = dr["denaturing_agent"].ToString();
                            record.denatured_qty = Convert.ToDouble(dr["denatured_qty"].ToString());
                            //record.approval_status = dr["record_status"].ToString();
                         // record.receiver_storage_transfer_id = Convert.ToInt32(dr["receiver_storage_transfer_id"].ToString());
                            record.storage_dispatch_id = Convert.ToInt32(dr["storage_dispatch_id"].ToString());
                            frm84D.Add(record);
                        }
                       
                    }

                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return frm84D;
        }
        public static List<StorageToDispatch> Search(string tablename, string column, string value)
        {
            List<StorageToDispatch> mir = new List<StorageToDispatch>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name as dispatchvat  from exciseautomation.storage_dispatch a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.to_dispatchvat=c.vat_code where  " + column + " Ilike '%" + value + "%' and a.record_active='true' order by a.receipt_date desc", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            mir = new List<StorageToDispatch>();
                            while (dr.Read())
                            {
                                StorageToDispatch record = new StorageToDispatch();
                                record.receipt_date = dr["receipt_date"].ToString().Replace("/", "-").Substring(0, 10); ;
                                record.receipt_hour = dr["receipt_hour"].ToString();
                                record.transfer_date = Convert.ToDateTime(dr["transfer_date"]).ToString("dd-MM-yyyy");
                                record.party_code = dr["party_code"].ToString();
                                record.party_name = dr["party_name"].ToString();
                                record.financial_year = dr["financial_year"].ToString();
                                record.dispatchvat = dr["dispatchvat"].ToString();
                                record.to_dispatchvat = dr["to_dispatchvat"].ToString();
                                record.from_storagevat = dr["from_storagevat"].ToString();
                                record.total_bl_receipt = Convert.ToDouble(dr["total_bl_receipt"].ToString());
                                record.total_lp_receipt = Convert.ToDouble(dr["total_lp_receipt"].ToString());
                                record.dips = Convert.ToDouble(dr["dips"].ToString());
                                record.temperature = Convert.ToDouble(dr["temperature"].ToString());
                                record.indication = Convert.ToDouble(dr["indication"].ToString());
                                record.strength = Convert.ToDouble(dr["strength"].ToString());
                                record.inc_groging = Convert.ToDouble(dr["inc_groging"].ToString());
                                record.bl_balanceqty = Convert.ToDouble(dr["bl_balanceqty"].ToString());
                                record.lp_balanceqty = Convert.ToDouble(dr["lp_balanceqty"].ToString());
                                record.record_status = dr["record_status"].ToString();
                                record.denaturing_agent = dr["denaturing_agent"].ToString();
                                record.denatured_qty = Convert.ToDouble(dr["denatured_qty"].ToString());
                                //record.approval_status = dr["record_status"].ToString();
                                // record.receiver_storage_transfer_id = Convert.ToInt32(dr["receiver_storage_transfer_id"].ToString());
                                record.storage_dispatch_id = Convert.ToInt32(dr["storage_dispatch_id"].ToString());
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



        public static StorageToDispatch GetDetails(string party_code, string form84Did,string financial_year)
        {
            StorageToDispatch record = new StorageToDispatch();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name from exciseautomation.storage_dispatch a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.to_dispatchvat=c.vat_code where a.party_code='"+party_code+"' and a.storage_dispatch_id='"+form84Did+"' and a.financial_year='"+financial_year+"'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            record.receipt_date = dr["receipt_date"].ToString().Replace("/", "-").Substring(0, 10); ;
                            record.receipt_hour = dr["receipt_hour"].ToString();
                            record.transfer_date = Convert.ToDateTime(dr["transfer_date"]).ToString("dd-MM-yyyy");
                            record.party_code = dr["party_code"].ToString();
                            record.party_name = dr["party_name"].ToString();
                           // record.dispatchvat = dr["dispatchvat"].ToString();
                            record.to_dispatchvat = dr["to_dispatchvat"].ToString();
                            record.from_storagevat = dr["from_storagevat"].ToString();
                            record.total_bl_receipt = Convert.ToDouble(dr["total_bl_receipt"].ToString());
                            record.total_lp_receipt = Convert.ToDouble(dr["total_lp_receipt"].ToString());
                            record.dips = Convert.ToDouble(dr["dips"].ToString());
                            record.temperature = Convert.ToDouble(dr["temperature"].ToString());
                            record.indication = Convert.ToDouble(dr["indication"].ToString());
                            record.strength = Convert.ToDouble(dr["strength"].ToString());
                            record.inc_groging = Convert.ToDouble(dr["inc_groging"].ToString());
                            record.bl_balanceqty = Convert.ToDouble(dr["bl_balanceqty"].ToString());
                            record.lp_balanceqty = Convert.ToDouble(dr["lp_balanceqty"].ToString());
                            record.dec_blending = Convert.ToDouble(dr["dec_blending"].ToString());
                            record.dec_racking = Convert.ToDouble(dr["dec_racking"].ToString());
                            record.dec_reduction = Convert.ToDouble(dr["dec_reduction"].ToString());
                            record.dec_wastage = Convert.ToDouble(dr["dec_wastage"].ToString());
                            record.inc_operation = Convert.ToDouble(dr["inc_operation"].ToString());

                            record.record_status = dr["record_status"].ToString();
                            record.denaturing_agent = dr["denaturing_agent"].ToString();
                            record.denatured_qty = Convert.ToDouble(dr["denatured_qty"].ToString());
                            record.hasqtyincreased= dr["hasqtyincreased"].ToString();
                            record.remarks= dr["remarks"].ToString();
                            //record.approval_status = dr["record_status"].ToString();
                           record.receiver_storage_transfer_id = Convert.ToInt32(dr["receiver_storage_transfer_id"].ToString());
                            record.storage_dispatch_id = Convert.ToInt32(dr["storage_dispatch_id"].ToString());
                        }
                     
                    }

                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return record;
        }


        public static string Approve(StorageToDispatch frm84D)
        {
            string value;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.storage_dispatch SET  record_status='" + frm84D.record_status + "' WHERE storage_dispatch_id='" + frm84D.storage_dispatch_id+ "' and   financial_year= '"+frm84D.financial_year+"'", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (frm84D.record_status == "R")
                    {
                        NpgsqlCommand cmd8 = new NpgsqlCommand("UPDATE exciseautomation.receiver_storage_transfer SET moved_to_nextstage ='N' WHERE  receiver_storage_transfer_id ='" + frm84D.receiver_storage_transfer_id + "' and  financial_year= '" + frm84D.financial_year + "'", cn);
                        cmd8.ExecuteNonQuery();
                        frm84D.record_status = "Rejected by Bond Officer";
                    }
                    else
                    {
                        if (frm84D.hasqtyincreased == "Y")
                        {
                            NpgsqlCommand cmd6 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + frm84D.to_dispatchvat + "' and party_code='" + frm84D.party_code + "'", cn);
                            double a = Convert.ToDouble(cmd6.ExecuteScalar());
                            double c = Convert.ToDouble(a) + (Convert.ToDouble(frm84D.total_bl_receipt)+Convert.ToDouble(frm84D.denatured_qty) + Convert.ToDouble(frm84D.inc_groging) + Convert.ToDouble(frm84D.inc_operation)) - (Convert.ToDouble(frm84D.dec_blending) + Convert.ToDouble(frm84D.dec_racking) + Convert.ToDouble(frm84D.dec_reduction) + Convert.ToDouble(frm84D.dec_wastage));
                            NpgsqlCommand cmd7 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + c + "'  where vat_code='" + frm84D.to_dispatchvat + "' and party_code='" + frm84D.party_code + "'", cn);
                            cmd7.ExecuteNonQuery();
                            NpgsqlCommand cmd8 = new NpgsqlCommand("UPDATE exciseautomation.receiver_storage_transfer SET moved_to_nextstage ='Y' WHERE  receiver_storage_transfer_id ='" + frm84D.receiver_storage_transfer_id + "' and   financial_year= '" + frm84D.financial_year + "'", cn);
                            cmd8.ExecuteNonQuery();
                        }
                        else
                        {
                            NpgsqlCommand cmd6 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + frm84D.to_dispatchvat + "' and party_code='" + frm84D.party_code + "'", cn);
                            double a = Convert.ToDouble(cmd6.ExecuteScalar());
                            double c = Convert.ToDouble(a) + (Convert.ToDouble(frm84D.total_bl_receipt)+Convert.ToDouble(frm84D.inc_groging) + Convert.ToDouble(frm84D.inc_operation)) - (Convert.ToDouble(frm84D.dec_blending) + Convert.ToDouble(frm84D.dec_racking) + Convert.ToDouble(frm84D.dec_reduction) + Convert.ToDouble(frm84D.dec_wastage));
                            NpgsqlCommand cmd7 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + c + "'  where vat_code='" + frm84D.to_dispatchvat + "' and party_code='" + frm84D.party_code + "'", cn);
                            cmd7.ExecuteNonQuery();
                            NpgsqlCommand cmd8 = new NpgsqlCommand("UPDATE exciseautomation.receiver_storage_transfer SET moved_to_nextstage ='Y' WHERE  receiver_storage_transfer_id ='" + frm84D.receiver_storage_transfer_id + "' and  financial_year= '" + frm84D.financial_year + "'", cn);
                            cmd8.ExecuteNonQuery();
                        }
                        frm84D.record_status = "Approved by Bond Officer";
                    }
                    StringBuilder str = new StringBuilder();
                    str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                    str.Append("'" + frm84D.storage_dispatch_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "','STD','" + frm84D.record_status + "','" + frm84D.remarks + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + frm84D.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + frm84D.user_id + "',"+frm84D.financial_year+"','"+frm84D.party_code+"')");
                    cmd = new NpgsqlCommand("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES('" + frm84D.storage_dispatch_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "','STD','" + frm84D.record_status + "','" + frm84D.remarks + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + frm84D.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + frm84D.user_id + "','" + frm84D.financial_year + "','"+frm84D.party_code+"')", cn);
                    n = cmd.ExecuteNonQuery();
                    value = "0";
                    trn.Commit();
                  
                    cn.Close();
                }
                catch (Exception ex)
                {
                    value = ex.Message;
                 
                    trn.Rollback();
                }
                return value;

            }
        }

        public static List<StorageToDispatch> GetDEN(string party_code,string date)
        {
            List<StorageToDispatch> frm84D = new List<StorageToDispatch>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                        cmd = new NpgsqlCommand("select distinct(c.vat_name)as dispatchvat, a.to_dispatch_vat as to_dispatchvat  from exciseautomation.pass  a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.to_dispatch_vat=c.vat_code inner join exciseautomation.product_master d on c.storage_content=d.product_code  where  b.party_code='" + party_code+"' and a.dispatch_date='"+date+ "' and(d.product_name Ilike 'E%' or d.product_name Ilike 'S%')  and a.record_status ='I'  ", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        frm84D = new List<StorageToDispatch>();
                        while (dr.Read())
                        {
                            StorageToDispatch record = new StorageToDispatch();
                       
                            //record.party_code = dr["party_code"].ToString();
                            //record.party_name = dr["party_name"].ToString();
                            record.dispatchvat = dr["dispatchvat"].ToString();
                            record.to_dispatchvat = dr["to_dispatchvat"].ToString();
                         
                            frm84D.Add(record);
                        }

                    }

                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return frm84D;
        }


        public static StorageToDispatch GetDispatchqty(string party_code,string date,string vat_code)
        {
            StorageToDispatch sto = new StorageToDispatch();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select distinct(a.dispatch_date), sum(dispatch_qty) as dispatchqty,a.party_code,b.party_name,  c.vat_name as dispatchvat, a.to_dispatch_vat as to_dispatchvat  from exciseautomation.pass  a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.to_dispatch_vat=c.vat_code inner join exciseautomation.product_master d on c.storage_content=d.product_code  where  b.party_code='" + party_code+"' and a.dispatch_date='"+date+ "' and a.to_dispatch_vat='"+vat_code+ "' and (d.product_name Ilike 'E%' or d.product_name Ilike 'S%' ) and a.record_status ='I' GROUP BY a.dispatch_date,a.party_code,b.party_name,c.vat_name,a.to_dispatch_vat  order by a.party_code", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                           sto.dispatchvat = dr["dispatchvat"].ToString();
                            sto.party_code = dr["party_code"].ToString();
                            sto.party_name = dr["party_name"].ToString();
                            sto.to_dispatchvat = dr["to_dispatchvat"].ToString();
                            sto.dispatchqty = Convert.ToDouble(dr["dispatchqty"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return sto;
            }
        }

        public static List<StorageToDispatch> GetsubmitedVat(string party_code, String date)
        {
            List<StorageToDispatch> STO = new List<StorageToDispatch>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;

                    cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name as dispatchvat, a.to_dispatch_vat as to_dispatchvat  from exciseautomation.pass  a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.to_dispatch_vat=c.vat_code inner join exciseautomation.product_master d on c.storage_content=d.product_code  where  b.party_code='" + party_code + "' and a.dispatch_date='" + date + "' and (d.product_name like 'E%' or d.product_name like 'S%') and a.record_status ='D'  order by a.party_code,a.pass_id", cn);

                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        STO = new List<StorageToDispatch>();
                        while (dr.Read())
                        {
                            StorageToDispatch record = new StorageToDispatch();

                            record.party_code = dr["party_code"].ToString();
                            record.party_name = dr["party_name"].ToString();
                            record.dispatchvat = dr["dispatchvat"].ToString();
                            record.to_dispatchvat = dr["to_dispatchvat"].ToString();
                            STO.Add(record);
                        }
                       
                    }

                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return STO;
        }


        public static List<StorageToDispatch> GetDENList(string party_code,string date, string vatcode)
        {
            List<StorageToDispatch> frm84D = new List<StorageToDispatch>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name as dispatchvat, a.to_dispatch_vat as to_dispatchvat,a.dispatch_date as receipt_date, a.dispatch_qty as total_bl_receipt   from exciseautomation.pass  a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.to_dispatch_vat=c.vat_code inner join exciseautomation.product_master d on c.storage_content=d.product_code where  b.party_code='" + party_code + "' and a.dispatch_date='" + date + "' and a.to_dispatch_vat='"+vatcode+ "' and (d.product_name Ilike 'E%' or d.product_name Ilike 'S%')    order by a.party_code,a.pass_id", cn);

                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        frm84D = new List<StorageToDispatch>();
                        while (dr.Read())
                        {
                            StorageToDispatch record = new StorageToDispatch();
                            if(dr["receipt_date"].ToString()!="")
                            record.receipt_date = dr["receipt_date"].ToString().Replace("/", "-").Substring(0, 10); ;
                            record.party_code = dr["party_code"].ToString();
                            record.party_name = dr["party_name"].ToString();
                            record.dispatchvat = dr["dispatchvat"].ToString();
                            record.to_dispatchvat = dr["to_dispatchvat"].ToString();
                            record.total_bl_receipt = Convert.ToDouble(dr["total_bl_receipt"].ToString());
                            record.record_status = dr["record_status"].ToString();
                            frm84D.Add(record);
                        }

                    }

                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return frm84D;
        }
        public static List<StorageToDispatch> GetDENList1(string party_code, string date)
        {
            List<StorageToDispatch> frm84D = new List<StorageToDispatch>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name as dispatchvat, a.to_dispatch_vat as to_dispatchvat,a.dispatch_date as receipt_date, a.dispatch_qty as total_bl_receipt   from exciseautomation.pass  a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.to_dispatch_vat=c.vat_code inner join exciseautomation.product_master d on a.pass_for=d.product_code where  b.party_code='" + party_code + "' and a.dispatch_date='" + date + "'and d.product_name like 'E%'    order by a.party_code,a.pass_id", cn);

                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        frm84D = new List<StorageToDispatch>();
                        while (dr.Read())
                        {
                            StorageToDispatch record = new StorageToDispatch();
                            if (dr["receipt_date"].ToString() != "")
                                record.receipt_date = dr["receipt_date"].ToString().Replace("/", "-").Substring(0, 10); ;
                            record.party_code = dr["party_code"].ToString();
                            record.party_name = dr["party_name"].ToString();
                            record.dispatchvat = dr["dispatchvat"].ToString();
                            record.to_dispatchvat = dr["to_dispatchvat"].ToString();
                            record.total_bl_receipt = Convert.ToDouble(dr["total_bl_receipt"].ToString());
                            record.record_status = dr["record_status"].ToString();
                            frm84D.Add(record);
                        }

                    }

                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return frm84D;
        }
    }
}
