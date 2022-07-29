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
    public class DL_Molasses_Production_MF2
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static string Insert(Molasses_Production_MF2 mf2)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT  case when max(molasses_prov_prod_id) is null then 0 else max(molasses_prov_prod_id) end as molasses_prov_prod_id FROM exciseautomation.molasses_prov_prod where financial_year='" + mf2.financial_year + "'", cn);
                int m = Convert.ToInt32(cmd1.ExecuteScalar());
                m = m + 1;
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("INSERT INTO exciseautomation.molasses_prov_prod(molasses_prov_prod_id, entry_date, financial_year, party_code, iscaptive, to_party_code, product_code,");
                    str.Append("canecrush_startdate, molasses_plan_next_season, sugar_plan_next_season, molasses_prod_daily, sugar_prod_daily, total_storage_capacity, new_prod_storage,");
                    str.Append("wagon_loading, actualprod_prevyr1, actualprod_prevyr2, actualprod_prevyr3, total_molasses_delivered, other_person_total, molasses_avail_pyr1, ");
                    str.Append("molasses_avail_pyr2, molasses_avail_pyr3, total_avail_stock_stored, why_stock_not_cleared, newcrop_storage, newcrop_storage_difficulty, cleaned_storage,");
                    str.Append("name_address_occupier, name_address_manager, mechanical_pump, to_be_allotted, allotted_qty, user_id, creation_date, record_status,attribute1)Values(");
                    str.Append("'" + m + "','" + mf2.entry_date + "','" + mf2.financial_year + "','" + mf2.party_code + "','" + mf2.iscaptive + "','" + mf2.to_party_code + "','" + mf2.product_code + "',");
                    str.Append("'" + mf2.cane_crushing_date + "','" + mf2.molasses_plan_next_season + "','" + mf2.sugar_plan_next_season + "','" + mf2.molasses_prod_daily + "','" + mf2.sugar_prod_daily + "','" + mf2.total_storage_capacity + "','" + mf2.new_prod_storage + "',");
                    str.Append("'" + mf2.wagon_loading + "','" + mf2.actualprod_prevyr1 + "','" + mf2.actualprod_prevyr2 + "','" + mf2.actualprod_prevyr3 + "','" + mf2.total_molasses_delivered + "','" + mf2.other_person_total + "','" + mf2.molasses_avail_pyr1 + "',");
                    str.Append("'" + mf2.molasses_avail_pyr2 + "','" + mf2.molasses_avail_pyr3 + "','" + mf2.total_avail_stock_stored + "','" + mf2.why_stock_not_cleared + "','" + mf2.newcrop_storage + "','" + mf2.newcrop_storage_difficulty + "','" + mf2.cleaned_storage + "',");
                    str.Append("'" + mf2.name_address_occupier + "','" + mf2.name_address_manager + "','" + mf2.mechanical_pump + "','" + mf2.to_be_allotted + "','" + mf2.allotted_qty + "','" + mf2.userid + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + mf2.record_status + "','"+mf2.name_address_applicant+"')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    cmd.ExecuteNonQuery();
                    for (int i1 = 0; i1 < mf2.vatmaster.Count; i1++)
                    {
                        str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.molasses_tank_storage(molasses_prov_prod_id, vat_code, storage_capacity, user_id, creation_date, record_status,financial_year)Values(");
                        str.Append("'" + m + "','" + mf2.vatmaster[i1].vat_code + "','" + mf2.vatmaster[i1].vat_totalcapacity + "','" + mf2.userid + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + mf2.record_status + "','"+mf2.financial_year+"')");
                        NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                        cmd3.ExecuteNonQuery();
                    }
                    for (int i1 = 0; i1 < mf2.delevery.Count; i1++)
                    {
                        str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.molasses_deliverydetail(molasses_prov_prod_id, party_code, delivered_year, delivered_qty, user_id, creation_date, record_status,financial_year)Values(");
                        str.Append("'" + m + "','" + mf2.delevery[i1].party_code + "','" + mf2.delevery[i1].delivered_year + "','" + mf2.delevery[i1].delivered_qty + "','" + mf2.userid + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + mf2.record_status + "','" + mf2.financial_year + "')");
                        NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                        cmd3.ExecuteNonQuery();
                    }
                    for (int i1 = 0; i1 < mf2.other.Count; i1++)
                    {
                        str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.molasses_other_deliverydetail(molasses_prov_prod_id, other_party, delivered_year, delivered_qty,  user_id, creation_date, record_status,financial_year)Values(");
                        str.Append("'" + m + "','" + mf2.other[i1].other_party + "','" + mf2.other[i1].delivered_year + "','" + mf2.other[i1].delivered_qty + "','" + mf2.userid + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + mf2.record_status + "','" + mf2.financial_year + "')");
                        NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                        cmd3.ExecuteNonQuery();
                    }
                    for (int i1 = 0; i1 < mf2.storage.Count; i1++)
                    {
                        str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.molasses_avail_storage_detail(molasses_prov_prod_id, vat_code, storage_year, avail_qty,financial_year)Values(");
                        str.Append("'" + m + "','" + mf2.storage[i1].vat_code + "','" + mf2.storage[i1].financial_year + "','" + mf2.storage[i1].bal_capacity + "','" + mf2.financial_year + "')");
                        NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                        cmd3.ExecuteNonQuery();
                    }
                    VAL = "0";
                    trn.Commit();
                    cn.Close();
                    _log.Info("Insert MF2 Success :" + mf2.entry_date + "-" + mf2.party_code);
                }
                catch (Exception ex1)
                {
                    _log.Info("Insert MF2 Fail :" + mf2.entry_date + "-" + mf2.party_code + "-" + ex1.Message);
                    VAL = ex1.Message;
                    trn.Rollback();
                }

            }
            return VAL;
        }

        public static string GetValues(string value)
        {
            string val = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    string[] party = value.Split('_');
                    using (NpgsqlCommand cmd = new NpgsqlCommand("Select count(1) from exciseautomation.molasses_prov_prod where party_code='" + party[0] + "' and product_code='" + party[1] + "' and financial_year='" + party[2] + "' ", cn))
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

        public static string Update(Molasses_Production_MF2 mf2)
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
                    str.Append("update exciseautomation.molasses_prov_prod set entry_date='"+mf2.entry_date+"', financial_year='"+mf2.financial_year+"', party_code='"+mf2.party_code+"', iscaptive='"+mf2.iscaptive+"', to_party_code='"+mf2.to_party_code+"', product_code='"+mf2.product_code+"',");
                    str.Append("canecrush_startdate='"+mf2.cane_crushing_date+"', molasses_plan_next_season='"+mf2.molasses_plan_next_season+"', sugar_plan_next_season='"+mf2.sugar_plan_next_season+"', molasses_prod_daily='"+mf2.molasses_prod_daily+"', sugar_prod_daily='"+mf2.sugar_prod_daily+"', total_storage_capacity='"+mf2.total_storage_capacity+"', new_prod_storage='"+mf2.new_prod_storage+"', ");
                    str.Append("wagon_loading='"+mf2.wagon_loading+"', actualprod_prevyr1='"+mf2.actualprod_prevyr1+"', actualprod_prevyr2='"+mf2.actualprod_prevyr2+"', actualprod_prevyr3='"+mf2.actualprod_prevyr3+"', total_molasses_delivered='"+mf2.total_molasses_delivered+"', other_person_total='"+mf2.other_person_total+"', molasses_avail_pyr1='"+mf2.molasses_avail_pyr1+"', ");
                    str.Append("molasses_avail_pyr2='"+mf2.molasses_avail_pyr2+"', molasses_avail_pyr3='"+mf2.molasses_avail_pyr3+"', total_avail_stock_stored='"+mf2.total_avail_stock_stored+"', why_stock_not_cleared='"+mf2.why_stock_not_cleared+"', newcrop_storage='"+mf2.newcrop_storage+"', newcrop_storage_difficulty='"+mf2.newcrop_storage_difficulty+"', cleaned_storage='"+ mf2.cleaned_storage+"', ");
                    str.Append("name_address_occupier='"+mf2.name_address_occupier+"', name_address_manager='"+mf2.name_address_manager+"', mechanical_pump='"+mf2.mechanical_pump+"', to_be_allotted='"+mf2.to_be_allotted+"', allotted_qty='"+mf2.allotted_qty+"', user_id='"+mf2.userid+ "', lastmodified_date='"+DateTime.Now.ToString("dd-MM-yyyy")+"', record_status='"+mf2.record_status+ "',attribute1='" + mf2.name_address_applicant+"' where molasses_prov_prod_id='" + mf2.molasses_prov_prod_id+"' and financial_year='"+mf2.financial_year+"'");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    cmd.ExecuteNonQuery();
                    //str = new StringBuilder("delete from exciseautomation.molasses_tank_storage where molasses_prov_prod_id='" + mf2.molasses_prov_prod_id + "'");
                    //cmd = new NpgsqlCommand(str.ToString(), cn);
                    //cmd.ExecuteNonQuery();
                    //for (int i1 = 0; i1 < mf2.vatmaster.Count; i1++)
                    //{
                        
                    //    str = new StringBuilder();
                    //    str.Append("INSERT INTO exciseautomation.molasses_tank_storage(molasses_prov_prod_id, vat_code, storage_capacity, user_id, creation_date, record_status)Values(");
                    //    str.Append("'" + mf2.molasses_prov_prod_id+ "','" + mf2.vatmaster[i1].vat_code + "','" + mf2.vatmaster[i1].vat_totalcapacity + "','" + mf2.userid + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + mf2.record_status + "')");
                    //    NpgsqlCommand  cmd3 = new NpgsqlCommand(str.ToString(), cn);
                    //    cmd3.ExecuteNonQuery();
                    //}
                    for (int i1 = 0; i1 < mf2.delevery.Count; i1++)
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("Select case when Count(1) is null then 0 else Count(1) end as Count1 from exciseautomation.molasses_deliverydetail where molasses_deliverydetail_id= '" + mf2.delevery[i1].molasses_deliverydetail_id+"' and financial_year='"+mf2.financial_year+"'", cn);
                        int n =Convert.ToInt32( cmd1.ExecuteScalar());
                        if (n == 1)
                        {
                            str = new StringBuilder();
                            str.Append("update exciseautomation.molasses_deliverydetail set party_code='" + mf2.delevery[i1].party_code + "', delivered_year='" + mf2.delevery[i1].delivered_year + "', delivered_qty='" + mf2.delevery[i1].delivered_qty + "', user_id='" + mf2.userid + "', lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "', record_status='" + mf2.record_status + "' where molasses_deliverydetail_id='" + mf2.delevery[i1].molasses_deliverydetail_id + "' and financial_year='" + mf2.financial_year + "'");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            cmd3.ExecuteNonQuery();
                        }
                        else
                        {
                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.molasses_deliverydetail(molasses_prov_prod_id, party_code, delivered_year, delivered_qty, user_id, creation_date, record_status,financial_year)Values(");
                            str.Append("'" +mf2.molasses_prov_prod_id + "','" + mf2.delevery[i1].party_code + "','" + mf2.delevery[i1].delivered_year + "','" + mf2.delevery[i1].delivered_qty + "','" + mf2.userid + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + mf2.record_status + "','"+mf2.financial_year+"')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            cmd3.ExecuteNonQuery();
                        }
                    }
                    for (int i1 = 0; i1 < mf2.other.Count; i1++)
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("Select case when Count(1) is null then 0 else Count(1) end as Count1 from exciseautomation.molasses_other_deliverydetail where molasses_other_deliverydetail_id= '" + mf2.other[i1].molasses_other_deliverydetail_id + "' and financial_year='" + mf2.financial_year + "'", cn);
                        int n = Convert.ToInt32(cmd1.ExecuteScalar());
                        if (n == 1)
                        {
                            str = new StringBuilder();
                            str.Append("update exciseautomation.molasses_other_deliverydetail set other_party='" + mf2.other[i1].other_party + "', delivered_year='" + mf2.other[i1].delivered_year + "', delivered_qty='" + mf2.other[i1].delivered_qty + "',  user_id='" + mf2.userid + "',  lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "', record_status='" + mf2.record_status + "' where molasses_other_deliverydetail_id='" + mf2.other[i1].molasses_other_deliverydetail_id + "' and financial_year='" + mf2.financial_year + "'");

                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            cmd3.ExecuteNonQuery();
                        }
                        else
                        {
                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.molasses_other_deliverydetail(molasses_prov_prod_id, other_party, delivered_year, delivered_qty,  user_id, creation_date, record_status,financial_year)Values(");
                            str.Append("'" + mf2.molasses_prov_prod_id + "','" + mf2.other[i1].other_party + "','" + mf2.other[i1].delivered_year + "','" + mf2.other[i1].delivered_qty + "','" + mf2.userid + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + mf2.record_status + "','"+mf2.financial_year+"')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            cmd3.ExecuteNonQuery();
                        }
                      
                    }
                    for (int i1 = 0; i1 < mf2.storage.Count; i1++)
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("Select case when Count(1) is null then 0 else Count(1) end as Count1  from  exciseautomation.molasses_avail_storage_detail where molasses_avail_storage_detail_id= '" + mf2.storage[i1].molasses_prod_tank_storage_id + "' and financial_year='" + mf2.financial_year + "'", cn);
                        int n = Convert.ToInt32(cmd1.ExecuteScalar());
                        if (n == 1)
                        {
                            str = new StringBuilder();
                            str.Append("update exciseautomation.molasses_avail_storage_detail set vat_code='" + mf2.storage[i1].vat_code + "', storage_year='" + mf2.storage[i1].bal_capacity + "',avail_qty ='" + mf2.storage[i1].bal_capacity + "' where molasses_avail_storage_detail_id='" + mf2.storage[i1].molasses_prod_tank_storage_id + "' and financial_year='" + mf2.financial_year + "'");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            cmd3.ExecuteNonQuery();
                        }
                        else
                        {
                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.molasses_avail_storage_detail(molasses_prov_prod_id, vat_code, storage_year, avail_qty,financial_year)Values(");
                            str.Append("'" + mf2.molasses_prov_prod_id + "','" + mf2.storage[i1].vat_code + "','" + mf2.storage[i1].financial_year + "','" + mf2.storage[i1].bal_capacity + "','"+mf2.financial_year+"')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            cmd3.ExecuteNonQuery();
                        }
                        
                    }
                    if (mf2.delevery.Count > 0)
                    {
                        if (mf2.delevery[0].deleted_ids != null)
                        {
                            string[] values = mf2.delevery[0].deleted_ids.Split('_');
                            if (values.Length > 0 && values[0] != "")
                            {
                                for (int i = 0; i < values.Length; i++)
                                {
                                    if (values[i] != "")
                                    {
                                        str = new StringBuilder();
                                        str.Append("update exciseautomation.molasses_deliverydetail set record_deleted=true where molasses_deliverydetail_id='" + values[i] + "' and financial_year='" + mf2.financial_year + "'");
                                        NpgsqlCommand cmd33 = new NpgsqlCommand(str.ToString(), cn);
                                        int n = cmd33.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                    if (mf2.other.Count > 0)
                    {
                        if (mf2.other[0].deleted_ids != null)
                        {
                            string[] othervalues = mf2.other[0].deleted_ids.Split('_');
                            if (othervalues.Length > 0 && othervalues[0] != "")
                            {
                                for (int i = 0; i < othervalues.Length; i++)
                                {
                                    if (othervalues[i] != "")
                                    {
                                        str = new StringBuilder();
                                        str.Append("update exciseautomation.molasses_other_deliverydetail set record_deleted=true where molasses_other_deliverydetail_id='" + othervalues[i] + "' and financial_year='" + mf2.financial_year + "'");
                                        NpgsqlCommand cmd33 = new NpgsqlCommand(str.ToString(), cn);
                                        cmd33.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                    if (mf2.storage.Count > 0)
                    {
                        if (mf2.storage[0].deleted_ids!=null)
                        {
                            string[] storagevalues = mf2.storage[0].deleted_ids.Split('_');
                            if (storagevalues.Length > 0  && storagevalues[0] != "")
                            {
                                for (int i = 0; i < storagevalues.Length; i++)
                                {
                                    if (storagevalues[i] != "")
                                    {
                                        str = new StringBuilder();
                                        str.Append("update exciseautomation.molasses_avail_storage_detail set record_deleted=true where molasses_avail_storage_detail_id='" + storagevalues[i] + "' and financial_year='" + mf2.financial_year + "'");
                                        NpgsqlCommand cmd33 = new NpgsqlCommand(str.ToString(), cn);
                                       int n= cmd33.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                    VAL = "0";
                    trn.Commit();
                    cn.Close();
                    _log.Info("Update MF2 Success :" + mf2.entry_date + "-" + mf2.party_code);
                }
                catch (Exception ex1)
                {
                    _log.Info("Update MF2 Fail :" + mf2.entry_date + "-" + mf2.party_code + "-" + ex1.Message);
                    VAL = ex1.Message;
                    trn.Rollback();
                }

            }
            return VAL;
        }
        public static List<Molasses_Production_MF2> GetList()
        {
            List<Molasses_Production_MF2> mf2 = new List<Molasses_Production_MF2>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.product_name from exciseautomation.molasses_prov_prod a inner join exciseautomation.product_master b on a.product_code=b.product_code where a.record_active='true' order by molasses_prov_prod_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            mf2 = new List<Molasses_Production_MF2>();
                            while (dr.Read())
                            {
                                Molasses_Production_MF2 m2 =new Molasses_Production_MF2();
                                m2.financial_year = dr["financial_year"].ToString();
                                m2.product_code = dr["product_name"].ToString();
                                m2.party_code = dr["party_code"].ToString();
                                m2.entry_date = dr["entry_date"].ToString().Substring(0,10).Replace("/","-");
                                m2.new_prod_storage = Convert.ToDouble(dr["new_prod_storage"]);
                                m2.molasses_plan_next_season = Convert.ToDouble(dr["molasses_plan_next_season"]);
                                m2.record_status = dr["record_status"].ToString();
                                m2.molasses_prov_prod_id = dr["molasses_prov_prod_id"].ToString();
                                mf2.Add(m2);
                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get MF2 List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get MF2 List Fail :" + ex.Message);
                }

            }
            return mf2;
        }

        public static List<Molasses_Production_MF2> Search(string tablename, string column, string value)
        {
            List <Molasses_Production_MF2 > mir = new List<Molasses_Production_MF2>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd1;
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.product_name from exciseautomation.molasses_prov_prod a inner join exciseautomation.product_master b on a.product_code=b.product_code where "+ column + " Ilike '%" + value + "%' and a.record_active='true'  order by  molasses_prov_prod_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            mir = new List<Molasses_Production_MF2>();
                            while (dr.Read())
                            {
                                Molasses_Production_MF2 m2 = new Molasses_Production_MF2();
                                m2.financial_year = dr["financial_year"].ToString();
                                m2.product_code = dr["product_name"].ToString();
                                m2.party_code = dr["party_code"].ToString();
                                m2.entry_date = dr["entry_date"].ToString().Substring(0, 10).Replace("/", "-");
                                m2.new_prod_storage = Convert.ToDouble(dr["new_prod_storage"]);
                                m2.molasses_plan_next_season = Convert.ToDouble(dr["molasses_plan_next_season"]);
                                m2.record_status = dr["record_status"].ToString();
                                m2.molasses_prov_prod_id = dr["molasses_prov_prod_id"].ToString();
                                mir.Add(m2);

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
        public static Molasses_Production_MF2 GetDetails(string id, string financial_year)
        {
            Molasses_Production_MF2 mf2 = new Molasses_Production_MF2();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.molasses_prov_prod where molasses_prov_prod_id='" + id + "' and financial_year='"+financial_year+"'  order by molasses_prov_prod_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr1 = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr1);
                        dr1.Close();
                        foreach (DataRow dr in dt.Rows)
                        {
                            mf2.financial_year = dr["financial_year"].ToString();
                            mf2.entry_date = dr["entry_date"].ToString().Substring(0,10).Replace("/","-");
                            mf2.iscaptive = dr["iscaptive"].ToString();
                            mf2.to_party_code = dr["to_party_code"].ToString();
                            mf2.product_code = dr["product_code"].ToString();
                            mf2.cane_crushing_date = dr["canecrush_startdate"].ToString().Substring(0, 10).Replace("/", "-");
                            mf2.sugar_plan_next_season = Convert.ToDouble(dr["sugar_plan_next_season"].ToString());
                            mf2.molasses_plan_next_season = Convert.ToDouble(dr["molasses_plan_next_season"].ToString());
                            mf2.molasses_prod_daily = Convert.ToDouble(dr["molasses_prod_daily"].ToString());
                            mf2.sugar_prod_daily = Convert.ToDouble(dr["sugar_prod_daily"].ToString());
                            mf2.new_prod_storage = Convert.ToDouble(dr["new_prod_storage"].ToString());
                            mf2.wagon_loading = dr["wagon_loading"].ToString();
                            mf2.actualprod_prevyr1 = Convert.ToDouble(dr["actualprod_prevyr1"].ToString());
                            mf2.actualprod_prevyr2 = Convert.ToDouble(dr["actualprod_prevyr2"].ToString());
                            mf2.actualprod_prevyr3 = Convert.ToDouble(dr["actualprod_prevyr3"].ToString());
                            mf2.molasses_avail_pyr1 = Convert.ToDouble(dr["molasses_avail_pyr1"].ToString());
                            mf2.molasses_avail_pyr2 = Convert.ToDouble(dr["molasses_avail_pyr2"].ToString());
                            mf2.molasses_avail_pyr3 = Convert.ToDouble(dr["molasses_avail_pyr3"].ToString());
                            mf2.why_stock_not_cleared = dr["why_stock_not_cleared"].ToString();
                            mf2.newcrop_storage = dr["newcrop_storage"].ToString();
                            mf2.newcrop_storage_difficulty = dr["newcrop_storage_difficulty"].ToString();
                            mf2.cleaned_storage = dr["cleaned_storage"].ToString();
                            mf2.name_address_manager = dr["name_address_manager"].ToString();
                            mf2.name_address_occupier = dr["name_address_occupier"].ToString();
                            mf2.mechanical_pump = dr["mechanical_pump"].ToString();
                            mf2.molasses_prov_prod_id = dr["molasses_prov_prod_id"].ToString();
                            mf2.record_status = dr["record_status"].ToString();
                            mf2.total_molasses_delivered = Convert.ToDouble(dr["total_molasses_delivered"].ToString());
                            mf2.total_storage_capacity = Convert.ToDouble(dr["total_storage_capacity"].ToString());
                            mf2.other_person_total = Convert.ToDouble(dr["other_person_total"].ToString());
                            mf2.total_avail_stock_stored= Convert.ToDouble(dr["total_avail_stock_stored"].ToString());
                            mf2.name_address_applicant = dr["attribute1"].ToString();
                            mf2.delevery = new List<Molasses_Delivery_Details_MF2>();
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select a.*,b.party_name from exciseautomation.molasses_deliverydetail a inner join exciseautomation.party_master b on a.party_code=b.party_code  where a.molasses_prov_prod_id='" + id + "' and a.record_deleted is false  and a.financial_year='" + financial_year + "'   order by molasses_deliverydetail_id", cn))
                            {
                                cmd.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr2 = cmd1.ExecuteReader();
                                if (dr2.HasRows)
                                {

                                    while (dr2.Read())
                                    {
                                        Molasses_Delivery_Details_MF2 del = new Molasses_Delivery_Details_MF2();
                                        del.molasses_deliverydetail_id = dr2["molasses_deliverydetail_id"].ToString();
                                        del.delivered_year = dr2["delivered_year"].ToString();
                                        del.delivered_qty = dr2["delivered_qty"].ToString();
                                        del.party_code = dr2["party_code"].ToString();
                                        del.party_name = dr2["party_name"].ToString();
                                        mf2.delevery.Add(del);
                                    }
                                }
                                dr2.Close();
                            }
                                
                                mf2.other = new List<Molasses_Other_Delevery_MF2>();
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.molasses_other_deliverydetail where molasses_prov_prod_id='" + id + "' and record_deleted is false  and financial_year='" + financial_year + "'   order by molasses_other_deliverydetail_id", cn))
                            {
                                cmd.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr2 = cmd1.ExecuteReader();
                                if (dr2.HasRows)
                                {

                                    while (dr2.Read())
                                    {
                                        Molasses_Other_Delevery_MF2 other = new Molasses_Other_Delevery_MF2();
                                        other.molasses_other_deliverydetail_id = dr2["molasses_other_deliverydetail_id"].ToString();
                                        other.delivered_year = dr2["delivered_year"].ToString();
                                        other.other_party = dr2["other_party"].ToString();
                                        other.delivered_qty = dr2["delivered_qty"].ToString();
                                        mf2.other.Add(other);
                                    }
                                }
                                dr2.Close();
                            }
                            mf2.storage = new List<Molasses_Storage_Details_MF2>();
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select a.*,b.vat_name from exciseautomation.molasses_avail_storage_detail a inner join exciseautomation.vat_master b on a.vat_code=b.vat_code  where molasses_prov_prod_id='" + id + "' and a.record_deleted is false  and a.financial_year='" + financial_year + "'   order by molasses_avail_storage_detail_id", cn))
                            {
                                cmd1.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr2 = cmd1.ExecuteReader();
                                if (dr2.HasRows)
                                {

                                    while (dr2.Read())
                                    {
                                        Molasses_Storage_Details_MF2 sto = new Molasses_Storage_Details_MF2();
                                        sto.molasses_prod_tank_storage_id = dr2["molasses_avail_storage_detail_id"].ToString();
                                        sto.vat_code = dr2["vat_code"].ToString();
                                        sto.vat_name = dr2["vat_name"].ToString();
                                        sto.financial_year = dr2["storage_year"].ToString();
                                        sto.bal_capacity = dr2["avail_qty"].ToString();
                                        mf2.storage.Add(sto);
                                    }
                                }
                                dr2.Close();
                            }
                            mf2.vatmaster = new List<VAT_Master>();
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select a.*,b.vat_name from exciseautomation.molasses_tank_storage a inner join exciseautomation.vat_master b on a.vat_code=b.vat_code  where a.molasses_prov_prod_id='" + id + "'   and a.financial_year='" + financial_year + "'   order by molasses_tank_storage_id", cn))
                            {
                                cmd1.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr2 = cmd1.ExecuteReader();
                                if (dr2.HasRows)
                                {

                                    while (dr2.Read())
                                    {
                                        VAT_Master sto = new VAT_Master();
                                       // sto. = dr2["molasses_tank_storage_id"].ToString();
                                        sto.vat_code = dr2["vat_code"].ToString();
                                        sto.vat_name = dr2["vat_name"].ToString();
                                        sto.vat_totalcapacity =Convert.ToDouble( dr2["storage_capacity"].ToString());
                                     //   sto.bal_capacity = dr2["avail_qty"].ToString();
                                        mf2.vatmaster.Add(sto);
                                    }
                                }
                                dr2.Close();
                            }
                        }
                        
                    }
                    cn.Close();
                    _log.Info("Get MF2  Details Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get MF2 Details Fail :" + ex.Message);
                }

            }
            return mf2;
        }
    }
}
