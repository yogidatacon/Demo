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
  public  class DL_RawmaterialWastage
    {
        public static List<RawmaterialWastage> GetDispatch()
        {
            List<RawmaterialWastage> Dispatchs = new List<RawmaterialWastage>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    //if (party_code == null || party_code == "All")
                    cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name  from exciseautomation.rawmaterial_wastage a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.vat_code=c.vat_code where a.record_active='true'  order by a.rmw_entrydate  desc", cn);
                    //else
                    //    cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name  from exciseautomation.dailydispatchclosure a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.from_dispatchvat=c.vat_code where a.party_code='" + party_code + "' order by a.party_code,a.closure_date", cn);

                    cmd.CommandType = System.Data.CommandType.Text;

                    //NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.dailydispatchclosure where  user_id='"+userid+"' ", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        Dispatchs = new List<RawmaterialWastage>();
                        while (dr.Read())
                        {
                            RawmaterialWastage dispatch = new RawmaterialWastage();
                            dispatch.rawmaterial_wastage_id = Convert.ToInt32(dr["rawmaterial_wastage_id"].ToString());
                            dispatch.rmw_entrydate = Convert.ToDateTime(dr["rmw_entrydate"].ToString()).ToString("dd-MM-yyyy");
                            dispatch.party_code = dr["party_code"].ToString();
                            dispatch.financial_year = dr["financial_year"].ToString();
                            dispatch.party_name = dr["party_name"].ToString();
                            dispatch.transit_wastage = Convert.ToDouble(dr["transit_wastage"].ToString());
                            dispatch.vat_code = dr["vat_code"].ToString();
                            dispatch.vat_name = dr["vat_name"].ToString();
                            dispatch.storage_wastage = Convert.ToDouble(dr["storage_wastage"].ToString());
                            dispatch.handling_wastage = Convert.ToDouble(dr["handling_wastage"].ToString());
                            dispatch.remarks = dr["remarks"].ToString();
                            dispatch.record_status = dr["record_status"].ToString();
                           dispatch.user_id = dr["user_id"].ToString();
                            // dispatch.party_name = dr["party_name"].ToString();
                            Dispatchs.Add(dispatch);

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return Dispatchs;
            }
        }
        public static List<RawmaterialWastage> Search(string tablename, string column, string value)
        {
            List<RawmaterialWastage> mir = new List<RawmaterialWastage>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name  from exciseautomation.rawmaterial_wastage a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.vat_code=c.vat_code where  " + column + " Ilike '%" + value + "%' and a.record_active='true'  order by a.rmw_entrydate desc", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            mir = new List<RawmaterialWastage>();
                            while (dr.Read())
                            {
                                RawmaterialWastage dispatch = new RawmaterialWastage();
                                dispatch.rawmaterial_wastage_id = Convert.ToInt32(dr["rawmaterial_wastage_id"].ToString());
                                dispatch.rmw_entrydate = Convert.ToDateTime(dr["rmw_entrydate"].ToString()).ToString("dd-MM-yyyy");
                                dispatch.party_code = dr["party_code"].ToString();
                                dispatch.financial_year = dr["financial_year"].ToString();
                                dispatch.party_name = dr["party_name"].ToString();
                                dispatch.transit_wastage = Convert.ToDouble(dr["transit_wastage"].ToString());
                                dispatch.vat_code = dr["vat_code"].ToString();
                                dispatch.vat_name = dr["vat_name"].ToString();
                                dispatch.storage_wastage = Convert.ToDouble(dr["storage_wastage"].ToString());
                                dispatch.handling_wastage = Convert.ToDouble(dr["handling_wastage"].ToString());
                                dispatch.remarks = dr["remarks"].ToString();
                                dispatch.record_status = dr["record_status"].ToString();
                                dispatch.user_id = dr["user_id"].ToString();
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




        public static string Insert(RawmaterialWastage dispatch)
        {
            string val = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                   // where financial_year = '"+dispatch.financial_year+"'
                    NpgsqlCommand cmd1 = new NpgsqlCommand("select max(rawmaterial_wastage_id) from exciseautomation.rawmaterial_wastage ", cn);
                    string m = cmd1.ExecuteScalar().ToString();
                    int n = 0;
                    if (m == "")
                        n = 1;
                    else
                        n = Convert.ToInt32(m) + 1;


                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.rawmaterial_wastage(rawmaterial_wastage_id, rmw_entrydate, party_code, vat_code, transit_wastage, storage_wastage, handling_wastage, remarks, user_id, creation_date, record_status,inc_operation,dec_wastage,financial_year)VALUES( '" + n+"','" + dispatch.rmw_entrydate+"', '"+dispatch.party_code+"', '"+dispatch.vat_code+"', '"+dispatch.transit_wastage+"', '"+dispatch.storage_wastage+"', '"+dispatch.handling_wastage+"','"+dispatch.remarks+"','"+dispatch.user_id+"','"+DateTime.Now.ToShortDateString()+"','"+dispatch.record_status+"','"+dispatch.inc_operation+"','"+dispatch.dec_wastage+"','"+dispatch.financial_year+"')", cn);
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
        public static int GetExistsData( string date, string value, string year)
        {
            int value1 = 0;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    cmd = new NpgsqlCommand("select count(1) from exciseautomation.rawmaterial_wastage where extract (month FROM rmw_entrydate)='" + date + "' and extract (year FROM rmw_entrydate)='" + year+"' and vat_code='" + value + "'", cn);
                    string re = cmd.ExecuteScalar().ToString();
                    if (re == "1")
                    {
                        value1 = 1;

                    }
                    else
                    {
                        if (re != "")
                            value1 = Convert.ToInt32(re);

                    }

                }
                catch (Exception ex)
                {

                    value1 = 0;
                }
            }
            return value1;
        }

        public static string Update(RawmaterialWastage dispatch)
        {
            string val = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.rawmaterial_wastage SET  rmw_entrydate ='"+dispatch.rmw_entrydate+"', party_code ='"+dispatch.party_code+"', vat_code ='"+dispatch.vat_code+"', transit_wastage ='"+dispatch.transit_wastage+"', storage_wastage ='"+dispatch.storage_wastage+"', handling_wastage ='"+dispatch.handling_wastage+"', remarks ='"+dispatch.remarks+"', lastmodified_date ='"+DateTime.Now.ToShortDateString()+"', user_id ='"+dispatch.user_id+"', record_status ='"+dispatch.record_status+ "',inc_operation='"+dispatch.inc_operation+"',dec_wastage='" + dispatch.dec_wastage+"' WHERE rawmaterial_wastage_id ='" + dispatch.rawmaterial_wastage_id+ "' and financial_year='" + dispatch.financial_year + "' ", cn);
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

        public static string AdminUpdate(RawmaterialWastage dispatch)
        {
            string val = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.rawmaterial_wastage SET   transit_wastage ='" + dispatch.transit_wastage + "', storage_wastage ='" + dispatch.storage_wastage + "', handling_wastage ='" + dispatch.handling_wastage + "' WHERE rawmaterial_wastage_id ='" + dispatch.rawmaterial_wastage_id + "' and financial_year='" + dispatch.financial_year + "' ", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();
                    StringBuilder str = new StringBuilder();
                    str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year) VALUES(");
                    str.Append("'" + dispatch.rawmaterial_wastage_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy") + "','RMW','Updated by Admin','Updated by Admin','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + dispatch.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + dispatch.user_id + "','"+dispatch.financial_year+"')");
                    cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
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

        public static RawmaterialWastage GetDetails(string party_code, int id, string financial_year)
        {

            RawmaterialWastage dispatch = new RawmaterialWastage();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    /*a.party_code='" + party_code + "' and*/
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name  from exciseautomation.rawmaterial_wastage a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.vat_code=c.vat_code  where  a.rawmaterial_wastage_id='" + id + "' and a.financial_year='"+financial_year+"'", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();

                    foreach (DataRow dr in dt.Rows)
                    {
                      
                        dispatch.rawmaterial_wastage_id = Convert.ToInt32(dr["rawmaterial_wastage_id"].ToString());
                        dispatch.rmw_entrydate = Convert.ToDateTime(dr["rmw_entrydate"].ToString()).ToString("dd-MM-yyyy");
                        dispatch.party_code = dr["party_code"].ToString();
                        dispatch.party_name = dr["party_name"].ToString();
                        dispatch.transit_wastage = Convert.ToDouble(dr["transit_wastage"].ToString());
                        dispatch.vat_code = dr["vat_code"].ToString();
                        dispatch.vat_name = dr["vat_name"].ToString();
                        dispatch.storage_wastage = Convert.ToDouble(dr["storage_wastage"].ToString());
                        dispatch.handling_wastage = Convert.ToDouble(dr["handling_wastage"].ToString());
                        if(dr["inc_operation"].ToString()!="")
                        dispatch.inc_operation = Convert.ToDouble(dr["inc_operation"].ToString());
                        if (dr["dec_wastage"].ToString() != "")
                        dispatch.dec_wastage = Convert.ToDouble(dr["dec_wastage"].ToString());
                        dispatch.remarks = dr["remarks"].ToString();
                        dispatch.record_status = dr["record_status"].ToString();
                        dispatch.user_id = dr["user_id"].ToString();
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


        public static string Approve(RawmaterialWastage DDC)
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
                    str.Append("update exciseautomation.rawmaterial_wastage set  record_status='" + DDC.record_status + "' where rawmaterial_wastage_id ='" + DDC.rawmaterial_wastage_id + "' and financial_year='" + DDC.financial_year + "'");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (DDC.record_status == "A")
                    {
                        NpgsqlCommand cmd6 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + DDC.vat_code + "' and party_code='" + DDC.party_code + "'", cn);
                        double k = Convert.ToDouble(cmd6.ExecuteScalar());
                        double v = Convert.ToDouble(k) - (Convert.ToDouble(DDC.handling_wastage) + Convert.ToDouble(DDC.storage_wastage) + Convert.ToDouble(DDC.transit_wastage) ) + Convert.ToDouble(DDC.inc_operation) - Convert.ToDouble(DDC.dec_wastage);
                        NpgsqlCommand cmd7 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + v + "'  where vat_code='" + DDC.vat_code + "' and party_code='" + DDC.party_code + "'", cn);
                        cmd7.ExecuteNonQuery();
                        DDC.record_status = "Approved By Bond Officer";
                    }
                    else
                    {
                        //NpgsqlCommand cmd8 = new NpgsqlCommand("update exciseautomation.pass set record_status='I' where dispatch_date='" + DDC.closure_date + "' and to_dispatch_vat='" + DDC.from_dispatchvat + "'", cn);
                        //cmd8.ExecuteNonQuery();
                        //NpgsqlCommand cmd9 = new NpgsqlCommand("update exciseautomation.dailydispatchclosure set attribute1='" + v + "' where closure_date='" + dispatch.closure_date + "' and from_dispatchvat='" + dispatch.from_dispatchvat + "'", cn);
                        //cmd9.ExecuteNonQuery();
                        //NpgsqlCommand cmd1 = new NpgsqlCommand("UPDATE exciseautomation.storage_dispatch SET moved_to_nextstage ='N' where receipt_date='" + DDC.closure_date + "' and to_dispatchvat='" + DDC.from_dispatchvat + "'", cn);
                        //cmd1.ExecuteNonQuery();
                        DDC.record_status = "Rejected By Bond Officer";
                    }
                    str = new StringBuilder();
                    str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                    str.Append("'" + DDC.rawmaterial_wastage_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy") + "','RMW','" + DDC.record_status + "','" + DDC.remarks + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DDC.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DDC.user_id + "','"+DDC.financial_year+"','"+DDC.party_code+"')");
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

        }     }
    }
