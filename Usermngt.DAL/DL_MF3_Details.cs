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
    public class DL_MF3_Details
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static List<MF3_Details> GetList()
        {
            List<MF3_Details> mf3 = new List<MF3_Details>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.molasses_actual_prod where record_active='true' order by molasses_actual_prod_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            mf3 = new List<MF3_Details>();
                            while (dr.Read())
                            {
                                MF3_Details m3 = new MF3_Details();
                                m3.financial_year = dr["financial_year"].ToString();
                                m3.product_code = dr["product_code"].ToString();
                                m3.party_code = dr["party_code"].ToString();
                                m3.crushing_closedate = dr["crushing_closedate"].ToString().Substring(0, 10).Replace("/", "-");
                                m3.sugar_produced_total = Convert.ToDouble(dr["sugar_produced_total"]);
                                m3.molasses_produced_total = Convert.ToDouble(dr["molasses_produced_total"]);
                                m3.cane_crushed_total = Convert.ToDouble(dr["cane_crushed_total"]);
                                m3.qty_lifted_total = Convert.ToDouble(dr["qty_lifted_total"]);
                                m3.record_status = dr["record_status"].ToString();
                                m3.molasses_actual_prod_id = dr["molasses_actual_prod_id"].ToString();
                                mf3.Add(m3);
                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get MF3 List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get MF3 List Fail :" + ex.Message);
                }

            }
            return mf3;
        }
        public static List<MF3_Details> Search(string tablename, string column, string value)
        {
            List<MF3_Details> mir = new List<MF3_Details>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd1;
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.molasses_actual_prod where " + column + " Ilike '%" + value + "%' and record_active='true' order by  molasses_actual_prod_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            mir = new List<MF3_Details>();
                            while (dr.Read())
                            {
                                MF3_Details m3 = new MF3_Details();
                                m3.financial_year = dr["financial_year"].ToString();
                                m3.product_code = dr["product_code"].ToString();
                                m3.party_code = dr["party_code"].ToString();
                                m3.crushing_closedate = dr["crushing_closedate"].ToString().Substring(0, 10).Replace("/", "-");
                                m3.sugar_produced_total = Convert.ToDouble(dr["sugar_produced_total"]);
                                m3.molasses_produced_total = Convert.ToDouble(dr["molasses_produced_total"]);
                                m3.cane_crushed_total = Convert.ToDouble(dr["cane_crushed_total"]);
                                m3.qty_lifted_total = Convert.ToDouble(dr["qty_lifted_total"]);
                                m3.record_status = dr["record_status"].ToString();
                                m3.molasses_actual_prod_id = dr["molasses_actual_prod_id"].ToString();
                               mir.Add(m3);

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
        public static string GetDupValues(string value)
        {
            string val = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    string[] party = value.Split('_');
                    using (NpgsqlCommand cmd = new NpgsqlCommand("Select count(1) from exciseautomation.molasses_actual_prod where party_code='" + party[0] + "' and product_code='" + party[1] + "' and financial_year='" + party[2] + "' ", cn))
                    {
                        val = cmd.ExecuteScalar().ToString();
                        if (val != "" && val != "0")
                            val = "DataExist";
                    }
                }
                catch
                {
                }
            }
            return val;
        }

        public static string Insert(MF3_Details mf3)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT  case when max(molasses_actual_prod_id) is null then 0 else max(molasses_actual_prod_id) end as molasses_actual_prod_id FROM exciseautomation.molasses_actual_prod", cn);
                int m = Convert.ToInt32(cmd1.ExecuteScalar());
                m = m + 1;
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("INSERT INTO exciseautomation.molasses_actual_prod(molasses_actual_prod_id, financial_year, party_code, crushing_closedate, cane_crushed_total,  molasses_produced_total,product_code,");
                    str.Append("sugar_produced_total, qty_lifted_total, wagon_load, preventive_arrangement, bal_avail_qty_total,  user_id, creation_date, record_status)values(");
                    str.Append("'" + m + "','" + mf3.financial_year + "','" + mf3.party_code + "','" + mf3.crushing_closedate + "','" + mf3.cane_crushed_total + "','" + mf3.molasses_produced_total + "','"+mf3.product_code+"',");
                    str.Append("'" + mf3.sugar_produced_total + "','" + mf3.qty_lifted_total + "','" + mf3.wagon_load + "','" + mf3.preventive_arrangement + "','" + mf3.bal_avail_qty_total + "','" + mf3.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + mf3.record_status + "')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    cmd.ExecuteNonQuery();
                    for (int i1 = 0; i1 < mf3.storage.Count; i1++)
                    {
                        str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.molasses_prod_tank_storage(molasses_actual_prod_id, vat_code, financial_year, bal_capacity,user_id, creation_date, record_status)Values(");
                        str.Append("'" + m + "','" + mf3.storage[i1].vat_code + "','" + mf3.storage[i1].financial_year + "','" + mf3.storage[i1].bal_capacity + "','" + mf3.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + mf3.record_status + "')");
                        NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                        cmd3.ExecuteNonQuery();
                    }
                    VAL = "0";
                    trn.Commit();
                    cn.Close();
                    _log.Info("Insert MF3 Success :" + mf3.crushing_closedate + "-" + mf3.party_code);
                }
                catch (Exception ex1)
                {
                    _log.Info("Insert MF3 Fail :" + mf3.crushing_closedate + "-" + mf3.party_code + "-" + ex1.Message);
                    VAL = ex1.Message;
                    trn.Rollback();
                }

            }
            return VAL;
        }

        public static string Update(MF3_Details mf3)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();

                try
                {//molasses_prov_prod_id
                    StringBuilder str = new StringBuilder();
                    str.Append("update exciseautomation.molasses_actual_prod set  financial_year='" + mf3.financial_year + "', party_code='" + mf3.party_code + "', crushing_closedate='" + mf3.crushing_closedate + "', cane_crushed_total='" + mf3.cane_crushed_total + "',  molasses_produced_total='" + mf3.molasses_produced_total + "',");
                    str.Append("sugar_produced_total='" + mf3.sugar_produced_total + "', qty_lifted_total='" + mf3.qty_lifted_total + "', wagon_load='" + mf3.wagon_load + "', preventive_arrangement='" + mf3.preventive_arrangement + "', bal_avail_qty_total='" + mf3.bal_avail_qty_total + "',  user_id='" + mf3.user_id + "', lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "', record_status='" + mf3.record_status + "',product_code='"+mf3.product_code+"' where molasses_actual_prod_id='" + mf3.molasses_actual_prod_id + "'");
                    
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                   
                    cmd.ExecuteNonQuery();
                    for (int i1 = 0; i1 < mf3.storage.Count; i1++)
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("Select case when Count(1) is null then 0 else Count(1) end as Count1  from  exciseautomation.molasses_prod_tank_storage where molasses_prod_tank_storage_id= '" + mf3.storage[i1].molasses_prod_tank_storage_id + "'", cn);
                        int n = Convert.ToInt32(cmd1.ExecuteScalar());
                        if (n == 1)
                        {
                            str = new StringBuilder();
                            str.Append("update exciseautomation.molasses_prod_tank_storage set vat_code='" + mf3.storage[i1].vat_code + "', financial_year='" + mf3.storage[i1].financial_year + "',bal_capacity ='" + mf3.storage[i1].bal_capacity + "' where molasses_prod_tank_storage_id='" + mf3.storage[i1].molasses_prod_tank_storage_id + "'");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            cmd3.ExecuteNonQuery();
                        }
                        else
                        {
                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.molasses_prod_tank_storage(molasses_actual_prod_id, vat_code, financial_year, bal_capacity)Values(");
                            str.Append("'" + mf3.molasses_actual_prod_id + "','" + mf3.storage[i1].vat_code + "','" + mf3.storage[i1].financial_year + "','" + mf3.storage[i1].bal_capacity + "')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            cmd3.ExecuteNonQuery();
                        }

                    }
                    if (mf3.storage[0].deleted_ids.Split('_') != null)
                    {
                        string[] storagevalues = mf3.storage[0].deleted_ids.Split('_');
                        if (storagevalues[0] != "")
                        {
                            for (int i = 0; i < storagevalues.Length; i++)
                            {
                                    str = new StringBuilder();
                                    str.Append("update exciseautomation.molasses_prod_tank_storage set record_deleted=true where molasses_prod_tank_storage_id='" + storagevalues[i] + "'");
                                    NpgsqlCommand cmd33 = new NpgsqlCommand(str.ToString(), cn);
                                    cmd33.ExecuteNonQuery();
                            }
                        }
                    }

                    VAL = "0";
                    trn.Commit();
                    cn.Close();
                    _log.Info("Update MF3 Success :" + mf3.crushing_closedate + "-" + mf3.party_code);
                }
                catch (Exception ex1)
                {
                    _log.Info("Update MF3 Fail :" + mf3.crushing_closedate + "-" + mf3.party_code + "-" + ex1.Message);
                    VAL = ex1.Message;
                    trn.Rollback();
                }

            }
            return VAL;
        }

        public static MF3_Details GetValues(string mpid, string financial_year)
        {
            MF3_Details mf3 = new MF3_Details();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.molasses_actual_prod where molasses_actual_prod_id='" + mpid + "' and financial_year='"+financial_year+"'  order by molasses_actual_prod_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr1 = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr1);
                        dr1.Close();
                        foreach (DataRow dr in dt.Rows)
                        {
                            mf3.financial_year = dr["financial_year"].ToString();
                            mf3.crushing_closedate = dr["crushing_closedate"].ToString().Substring(0,10).Replace("/","-");
                            mf3.cane_crushed_total =Convert.ToDouble( dr["cane_crushed_total"].ToString());
                            mf3.sugar_produced_total = Convert.ToDouble(dr["sugar_produced_total"].ToString());
                            mf3.molasses_produced_total = Convert.ToDouble(dr["molasses_produced_total"].ToString());
                            mf3.qty_lifted_total = Convert.ToDouble(dr["qty_lifted_total"].ToString());
                            mf3.wagon_load =dr["wagon_load"].ToString();
                            mf3.preventive_arrangement = dr["preventive_arrangement"].ToString();
                            mf3.molasses_actual_prod_id = dr["molasses_actual_prod_id"].ToString();
                            mf3.record_status= dr["record_status"].ToString();
                            mf3.product_code = dr["product_code"].ToString();
                            mf3.storage = new List<Molasses_Storage_Details_MF2>();
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select a.*,b.vat_name from exciseautomation.molasses_prod_tank_storage a inner join exciseautomation.vat_master b on a.vat_code=b.vat_code  where molasses_actual_prod_id='" + mpid + "'  and financial_year='" + financial_year + "' and a.record_deleted is false order by molasses_prod_tank_storage_id", cn))
                            {
                                cmd1.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr2 = cmd1.ExecuteReader();
                                if (dr2.HasRows)
                                {

                                    while (dr2.Read())
                                    {
                                        Molasses_Storage_Details_MF2 sto = new Molasses_Storage_Details_MF2();
                                        sto.molasses_prod_tank_storage_id = dr2["molasses_prod_tank_storage_id"].ToString();
                                        sto.vat_code = dr2["vat_code"].ToString();
                                        sto.vat_name = dr2["vat_name"].ToString();
                                        sto.financial_year = mf3.financial_year;
                                        sto.bal_capacity = dr2["bal_capacity"].ToString();
                                        mf3.storage.Add(sto);
                                    }
                                }
                                dr2.Close();
                            }
                           
                        }

                    }
                    cn.Close();
                    _log.Info("Get MF3  Details Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get MF3 Details Fail :" + ex.Message);
                }

            }
            return mf3;
        }
    }
}
