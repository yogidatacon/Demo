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
    public class DL_DailyDispatchClosure
    {

        public static List<DailyDispatchClosure> GetDispatchClosure()
        {
            List<DailyDispatchClosure> DispatchClosure = new List<DailyDispatchClosure>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    //if (party_code == null || party_code == "All")
                        cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name  from exciseautomation.dailydispatchclosure a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.from_dispatchvat=c.vat_code  order by a.closure_date  desc", cn);
                    //else
                    //    cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name  from exciseautomation.dailydispatchclosure a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.from_dispatchvat=c.vat_code where a.party_code='" + party_code + "' order by a.party_code,a.closure_date", cn);

                    cmd.CommandType = System.Data.CommandType.Text;

                    //NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.dailydispatchclosure where  user_id='"+userid+"' ", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        DispatchClosure = new List<DailyDispatchClosure>();
                        while (dr.Read())
                        {
                            DailyDispatchClosure dispatch = new DailyDispatchClosure();
                            dispatch.dailydispatchclosure_id = Convert.ToInt32(dr["dailydispatchclosure_id"].ToString());
                            dispatch.closure_date = Convert.ToDateTime(dr["closure_date"].ToString()).ToString("dd-MM-yyyy");
                            dispatch.party_code = dr["party_code"].ToString();
                            dispatch.party_name = dr["party_name"].ToString();
                            dispatch.financial_year= dr["financial_year"].ToString();
                            dispatch.dispatchqty = Convert.ToDouble(dr["dispatchqty"].ToString());
                            dispatch.from_dispatchvat = dr["from_dispatchvat"].ToString();
                            dispatch.vat_name = dr["vat_name"].ToString();
                            dispatch.dec_blending = Convert.ToDouble(dr["dec_blending"].ToString());
                            dispatch.dec_racking = Convert.ToDouble(dr["dec_racking"].ToString());
                            dispatch.dec_reduction = Convert.ToDouble(dr["dec_reduction"].ToString());
                            dispatch.dec_wastage = Convert.ToDouble(dr["dec_wastage"].ToString());
                            if (dr["attribute3"].ToString() != "")
                                dispatch.IncreaseBLByGroging = Convert.ToDouble(dr["attribute3"]);
                            if (dr["attribute2"].ToString() != "")
                                dispatch.txtIncreaseBLInOperation = Convert.ToDouble(dr["attribute2"]);
                            dispatch.dips = Convert.ToDouble(dr["dips"].ToString());
                            dispatch.strength = Convert.ToDouble(dr["strength"].ToString());
                            dispatch.indication = Convert.ToDouble(dr["indication"].ToString());
                            dispatch.temperature = Convert.ToDouble(dr["temperature"].ToString());
                            dispatch.remarks = dr["remarks"].ToString();
                            dispatch.record_status = dr["record_status"].ToString();
                            //dispatch.user_id = userid;
                            // dispatch.party_name = dr["party_name"].ToString();
                            DispatchClosure.Add(dispatch);

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return DispatchClosure;
            }
        }

        public static List<DailyDispatchClosure> Search(string tablename, string column, string value)
        {
            List<DailyDispatchClosure> mir = new List<DailyDispatchClosure>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name  from exciseautomation.dailydispatchclosure a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.from_dispatchvat=c.vat_code where  " + column + " Ilike '%" + value + "%' and a.record_active='true'  order by a.closure_date desc", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            mir = new List<DailyDispatchClosure>();
                            while (dr.Read())
                            {
                                DailyDispatchClosure dispatch = new DailyDispatchClosure();
                                dispatch.dailydispatchclosure_id = Convert.ToInt32(dr["dailydispatchclosure_id"].ToString());
                                dispatch.closure_date = Convert.ToDateTime(dr["closure_date"].ToString()).ToString("dd-MM-yyyy");
                                dispatch.party_code = dr["party_code"].ToString();
                                dispatch.party_name = dr["party_name"].ToString();
                                dispatch.financial_year= dr["financial_year"].ToString();
                                dispatch.dispatchqty = Convert.ToDouble(dr["dispatchqty"].ToString());
                                dispatch.from_dispatchvat = dr["from_dispatchvat"].ToString();
                                dispatch.vat_name = dr["vat_name"].ToString();
                                dispatch.dec_blending = Convert.ToDouble(dr["dec_blending"].ToString());
                                dispatch.dec_racking = Convert.ToDouble(dr["dec_racking"].ToString());
                                dispatch.dec_reduction = Convert.ToDouble(dr["dec_reduction"].ToString());
                                dispatch.dec_wastage = Convert.ToDouble(dr["dec_wastage"].ToString());
                                if (dr["attribute3"].ToString() != "")
                                    dispatch.IncreaseBLByGroging = Convert.ToDouble(dr["attribute3"]);
                                if (dr["attribute2"].ToString() != "")
                                    dispatch.txtIncreaseBLInOperation = Convert.ToDouble(dr["attribute2"]);
                                dispatch.dips = Convert.ToDouble(dr["dips"].ToString());
                                dispatch.strength = Convert.ToDouble(dr["strength"].ToString());
                                dispatch.indication = Convert.ToDouble(dr["indication"].ToString());
                                dispatch.temperature = Convert.ToDouble(dr["temperature"].ToString());
                                dispatch.remarks = dr["remarks"].ToString();
                                dispatch.record_status = dr["record_status"].ToString();
                                //dispatch.user_id = userid;
                                // dispatch.party_name = dr["party_name"].ToString();
                                mir.Add(dispatch);

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




        public static string InsertDispatch(DailyDispatchClosure dispatch)
        {
            string val = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd1 = new NpgsqlCommand("select max(dailydispatchclosure_id) from exciseautomation.dailydispatchclosure", cn);
                    string m = cmd1.ExecuteScalar().ToString();
                    int n = 0;
                    if (m == "")
                        n = 1;
                    else
                        n = Convert.ToInt32(m) + 1;
                

                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.dailydispatchclosure(dailydispatchclosure_id, closure_date, party_code, from_dispatchvat, dispatchqty, dec_reduction, dec_blending, dec_racking, dec_wastage, dips, temperature, indication, strength,bl_balanceqty, lp_balanceqty, remarks, lastmodified_date, user_id, creation_date,record_status,attribute2,attribute3,financial_year) VALUES('" + n + "', '" + dispatch.closure_date + "','" + dispatch.party_code + "', '" + dispatch.from_dispatchvat + "', '" + dispatch.dispatchqty + "', '" + dispatch.dec_reduction + "', '" + dispatch.dec_blending + "', '" + dispatch.dec_racking + "', '" + dispatch.dec_wastage + "', '" + dispatch.dips + "', '" + dispatch.temperature + "', '" + dispatch.indication + "', '" + dispatch.strength + "','" + dispatch.bl_balanceqty + "','" + dispatch.lp_balanceqty + "', '" + dispatch.remarks + "', '" + DateTime.Now.ToShortDateString() + "', '" + dispatch.user_id + "', '" + DateTime.Now.ToShortDateString() + "','" + dispatch.record_status + "','"+dispatch.txtIncreaseBLInOperation+"','"+dispatch.IncreaseBLByGroging+"','"+dispatch.financial_year+"') ", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();
                    if (dispatch.record_status == "Y")
                    {
                        //NpgsqlCommand cmd6 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + dispatch.from_dispatchvat + "' and party_code='" + dispatch.party_code + "'", cn);
                        //double k = Convert.ToDouble(cmd6.ExecuteScalar());
                        //double v = Convert.ToDouble(k) - (Convert.ToDouble(dispatch.dispatchqty) + Convert.ToDouble(dispatch.dec_blending)+ Convert.ToDouble(dispatch.dec_racking)+ Convert.ToDouble(dispatch.dec_reduction)+ Convert.ToDouble(dispatch.dec_wastage)) ;
                        //NpgsqlCommand cmd7 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + v + "'  where vat_code='" + dispatch.from_dispatchvat + "' and party_code='" + dispatch.party_code + "'", cn);
                        //cmd7.ExecuteNonQuery();
                        NpgsqlCommand cmd8 = new NpgsqlCommand("update exciseautomation.pass set record_status='D' where dispatch_date='"+dispatch.closure_date+"' and to_dispatch_vat='"+dispatch.from_dispatchvat+"'", cn);
                        cmd8.ExecuteNonQuery();
                        //NpgsqlCommand cmd9 = new NpgsqlCommand("update exciseautomation.dailydispatchclosure set attribute1='"+v+"' where closure_date='"+dispatch.closure_date+"' and from_dispatchvat='"+dispatch.from_dispatchvat+"'", cn);
                        //cmd9.ExecuteNonQuery();

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

        public static string UpdateDispatch(DailyDispatchClosure dispatch)
        {
            string val = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.dailydispatchclosure SET closure_date ='" + dispatch.closure_date + "',   from_dispatchvat ='" + dispatch.from_dispatchvat + "', dispatchqty ='" + dispatch.dispatchqty + "', dec_reduction ='" + dispatch.dec_reduction + "', dec_blending ='" + dispatch.dec_blending + "', dec_racking ='" + dispatch.dec_racking + "', dec_wastage ='" + dispatch.dec_wastage + "', dips ='" + dispatch.dips + "', temperature ='" + dispatch.temperature + "', indication ='" + dispatch.indication + "', strength ='" + dispatch.strength + "', bl_balanceqty='" + dispatch.bl_balanceqty + "', lp_balanceqty='" + dispatch.lp_balanceqty + "', remarks ='" + dispatch.remarks + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "', user_id ='" + dispatch.user_id + "', record_status='" + dispatch.record_status + "',attribute2='"+dispatch.txtIncreaseBLInOperation+ "',attribute3='"+dispatch.IncreaseBLByGroging+"'  WHERE dailydispatchclosure_id='" + dispatch.dailydispatchclosure_id + "' and financial_year='"+dispatch.financial_year+"' ", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();

                    if (dispatch.record_status == "Y")
                    {
                       
                        NpgsqlCommand cmd8 = new NpgsqlCommand("update exciseautomation.pass set record_status='D' where dispatch_date='" + dispatch.closure_date + "' and to_dispatch_vat='" + dispatch.from_dispatchvat + "' and financial_year='"+dispatch.financial_year+"' ", cn);
                        cmd8.ExecuteNonQuery();
                        //NpgsqlCommand cmd9 = new NpgsqlCommand("update exciseautomation.dailydispatchclosure set attribute1='" + v + "' where closure_date='" + dispatch.closure_date + "' and from_dispatchvat='" + dispatch.from_dispatchvat + "'", cn);
                        //cmd9.ExecuteNonQuery();
                        //NpgsqlCommand cmd1 = new NpgsqlCommand("UPDATE exciseautomation.storage_dispatch SET moved_to_nextstage ='Y' where receipt_date='" + dispatch.closure_date + "' and to_dispatchvat='" + dispatch.from_dispatchvat + "'", cn);
                        //cmd1.ExecuteNonQuery();
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


        public static List<VAT_Master> GetVat(string userid)
        {
            List<VAT_Master> vat = new List<VAT_Master>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select vat_name,vat_code from exciseautomation.vat_master where user_id='" + userid + "'", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        vat = new List<VAT_Master>();
                        while (dr.Read())
                        {
                            VAT_Master vats = new VAT_Master();
                            vats.vat_name = dr["vat_name"].ToString();
                            vats.vat_code = dr["vat_code"].ToString();
                            vats.user_id = userid;
                            vat.Add(vats);
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

        public static List<VAT_Master> GetVatAvilQty(string vatcode)
        {
            List<VAT_Master> vat = new List<VAT_Master>();
            VAT_Master vats = new VAT_Master();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where  vat_code ='" + vatcode + "' ", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        vat = new List<VAT_Master>();
                        while (dr.Read())
                        {
                            vats.vat_availablecapacity = Convert.ToDouble(dr["vat_availablecapacity"].ToString());
                            vat.Add(vats);
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

        public static DailyDispatchClosure GetDetails(string party_code, int dailydispatchclosure_id, string financial_year)
        {

            DailyDispatchClosure dispatch = new DailyDispatchClosure();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name from exciseautomation.dailydispatchclosure a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.from_dispatchvat=c.vat_code where a.financial_year='"+financial_year+"' and a.party_code='" + party_code + "' and a.dailydispatchclosure_id='" + dailydispatchclosure_id + "'", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();

                    foreach (DataRow dr in dt.Rows)
                    {
                        dispatch.dailydispatchclosure_id = Convert.ToInt32(dr["dailydispatchclosure_id"].ToString());
                        dispatch.closure_date = Convert.ToDateTime(dr["closure_date"].ToString()).ToString("dd-MM-yyyy");
                        dispatch.party_code = dr["party_code"].ToString();
                        dispatch.dispatchqty = Convert.ToDouble(dr["dispatchqty"].ToString());
                        dispatch.from_dispatchvat = dr["from_dispatchvat"].ToString();
                        dispatch.dec_blending = Convert.ToDouble(dr["dec_blending"].ToString());
                        dispatch.dec_racking = Convert.ToDouble(dr["dec_racking"].ToString());
                        dispatch.dec_reduction = Convert.ToDouble(dr["dec_reduction"].ToString());
                        dispatch.dec_wastage = Convert.ToDouble(dr["dec_wastage"].ToString());
                        if(dr["attribute3"].ToString()!="")
                        dispatch.IncreaseBLByGroging = Convert.ToDouble(dr["attribute3"]);
                        if(dr["attribute2"].ToString() != "")
                        dispatch.txtIncreaseBLInOperation= Convert.ToDouble(dr["attribute2"]);
                        dispatch.dips = Convert.ToDouble(dr["dips"].ToString());
                        dispatch.strength = Convert.ToDouble(dr["strength"].ToString());
                        dispatch.indication = Convert.ToDouble(dr["indication"].ToString());
                        dispatch.temperature = Convert.ToDouble(dr["temperature"].ToString());
                        dispatch.bl_balanceqty = Convert.ToDouble(dr["bl_balanceqty"].ToString());
                        dispatch.lp_balanceqty = Convert.ToDouble(dr["lp_balanceqty"].ToString());
                        dispatch.remarks = dr["remarks"].ToString();
                        dispatch.record_status = dr["record_status"].ToString();
                        // dispatch.party_name = dr["party_name"].ToString();

                    }


                }

                catch (Exception ex)
                {
                    throw (ex);
                }
                return dispatch;
            }
        }


        public static string Approve(DailyDispatchClosure DDC)
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
                    str.Append("update exciseautomation.dailydispatchclosure set  record_status='" + DDC.record_status + "' where dailydispatchclosure_id ='" + DDC.dailydispatchclosure_id + "' and financial_year='" + DDC.financial_year + "'");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (DDC.record_status == "A")
                    {
                        NpgsqlCommand cmd6 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + DDC.from_dispatchvat + "' and party_code='" + DDC.party_code + "'", cn);
                        double k = Convert.ToDouble(cmd6.ExecuteScalar());
                        double v = Convert.ToDouble(k) - (Convert.ToDouble(DDC.dispatchqty) + Convert.ToDouble(DDC.dec_blending) + Convert.ToDouble(DDC.dec_racking) + Convert.ToDouble(DDC.dec_reduction) + Convert.ToDouble(DDC.dec_wastage)) + Convert.ToDouble(DDC.txtIncreaseBLInOperation) + Convert.ToDouble(DDC.IncreaseBLByGroging);
                        NpgsqlCommand cmd7 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + v + "'  where vat_code='" + DDC.from_dispatchvat + "' and party_code='" + DDC.party_code + "'", cn);
                        cmd7.ExecuteNonQuery();
                        DDC.record_status = "Approved By Bond Officer";
                    }
                    else
                    {
                        NpgsqlCommand cmd8 = new NpgsqlCommand("update exciseautomation.pass set record_status='I' where dispatch_date='" + DDC.closure_date + "' and to_dispatch_vat='" + DDC.from_dispatchvat + "' and financial_year='"+DDC.financial_year+"'", cn);
                        cmd8.ExecuteNonQuery();
                        //NpgsqlCommand cmd9 = new NpgsqlCommand("update exciseautomation.dailydispatchclosure set attribute1='" + v + "' where closure_date='" + dispatch.closure_date + "' and from_dispatchvat='" + dispatch.from_dispatchvat + "'", cn);
                        //cmd9.ExecuteNonQuery();
                        //NpgsqlCommand cmd1 = new NpgsqlCommand("UPDATE exciseautomation.storage_dispatch SET moved_to_nextstage ='N' where receipt_date='" + DDC.closure_date + "' and to_dispatchvat='" + DDC.from_dispatchvat + "'", cn);
                        //cmd1.ExecuteNonQuery();
                        DDC.record_status = "Rejected By Bond Officer";
                    }
                    str = new StringBuilder();
                    str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                    str.Append("'" + DDC.dailydispatchclosure_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy") + "','DDC','" + DDC.record_status + "','" + DDC.remarks + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DDC.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DDC.user_id + "','"+DDC.financial_year+"','"+DDC.party_code+"')");
                    cmd = new NpgsqlCommand(str.ToString(), cn);
                    n = cmd.ExecuteNonQuery();
                    VAL = "0";
                    trn.Commit();
                    cn.Close();

                }
                catch (Exception ex1)
                {
                    trn.Rollback();


                }

            }
            return VAL;
        }
    }

}
