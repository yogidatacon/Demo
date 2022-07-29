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
   public class DL_Form82
    {
        public static List<Form82> GetList(string party_code)
        {
            List<Form82> Fermenter = new List<Form82>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                   
                        cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.rawmaterial_fermenter where a.party_code='" + party_code + "' order by rawmaterial_fermenter_id", cn);

                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        Fermenter = new List<Form82>();
                        while (dr.Read())
                        {
                            Form82 record = new Form82();
                            record.rawmaterial_fermenter_id = Convert.ToInt32(dr["rawmaterial_fermenter_id"].ToString());
                            record.setup_date = dr["setup_date"].ToString().Replace("/", "-").Substring(0, 10); ;
                            record.party_code = dr["party_code"].ToString();
                            record.setup_time = dr["setup_time"].ToString();
                            record.tofermentervat = dr["tofermentervat"].ToString();
                            Fermenter.Add(record);
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return Fermenter;
        }
        public static string Insert(Form82 fermenter )
        {
            string value = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
               // where financial_year = '"+fermenter.financial_year+"'
                NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT max(rawmaterial_fermenter_id) FROM exciseautomation.rawmaterial_fermenter ", cn);
                string m = cmd1.ExecuteScalar().ToString();
                int n = 0;
                if (m == "")
                    n = 1;
                else
                    n = Convert.ToInt32(m) + 1;

                try
                {
                    StringBuilder str = new StringBuilder();
                    if(fermenter.setup_time !="")
                    {
                        str.Append("INSERT INTO exciseautomation.rawmaterial_fermenter(rawmaterial_fermenter_id, setup_date, setup_time, party_code, tofermentervat, total_qty_transferred, no_of_vat_cask, total_bl_washsetup, sg_spentwash, sg_of_wash, setup_complete, lastmodified_date, user_id, creation_date,record_status,financial_year)Values(");
                        str.Append("'" + n + "','" + fermenter.setup_date + "','" + fermenter.setup_time + "','" + fermenter.party_code + "','" + fermenter.tofermentervat + "','" + fermenter.total_qty_transferred + "','" + fermenter.no_of_vat_cask + "','" + fermenter.total_bl_washsetup + "','" + fermenter.sg_spentwash + "','" + fermenter.sg_of_wash + "','" + fermenter.setup_complete + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + fermenter.user_id + "','" + fermenter.setup_date + "','" + fermenter.record_status + "','"+fermenter.financial_year+"')");
                       
                    }
                    else
                    {
                        str.Append("INSERT INTO exciseautomation.rawmaterial_fermenter(rawmaterial_fermenter_id, setup_date, party_code, tofermentervat, total_qty_transferred, no_of_vat_cask, total_bl_washsetup, sg_spentwash, sg_of_wash, setup_complete, lastmodified_date, user_id, creation_date,record_status,financial_year)Values(");
                        str.Append("'" + n + "','" + fermenter.setup_date + "','" + fermenter.party_code + "','" + fermenter.tofermentervat + "','" + fermenter.total_qty_transferred + "','" + fermenter.no_of_vat_cask + "','" + fermenter.total_bl_washsetup + "','" + fermenter.sg_spentwash + "','" + fermenter.sg_of_wash + "','" + fermenter.setup_complete + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + fermenter.user_id + "','" + fermenter.setup_date + "','" + fermenter.record_status + "','"+fermenter.financial_year+"')");
                       
                    }
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int a=cmd.ExecuteNonQuery();
                    if(a==1)
                    {
                        for (int i = 0; i < fermenter.fermSetup.Count; i++)
                        {

                            NpgsqlCommand cmd2 = new NpgsqlCommand("SELECT max(rawmaterial_fermenter_id) FROM exciseautomation.rawmaterial_fermenter where financial_year='" + fermenter.financial_year + "'", cn);
                            int b = Convert.ToInt32( cmd2.ExecuteScalar());
                            a = 0;
                            str = new StringBuilder();
                                str.Append("INSERT INTO exciseautomation.fermenter_setup( rawmaterial_fermenter_id, fromstoragevat, no_of_each_vat, molasses, mahua, gur, spentwash, activewash, water, other_material,lastmodified_date, user_id, creation_date, record_status,attribute1,financial_year)");
                                str.Append("Values('" + b + "', '" + fermenter.fermSetup[i].fromstoragevat + "','" + fermenter.fermSetup[i].no_of_each_vat + "','" + fermenter.fermSetup[i].molasses + "','" + fermenter.fermSetup[i].mahua + "','" + fermenter.fermSetup[i].gur + "','" + fermenter.fermSetup[i].spentwash + "','" + fermenter.fermSetup[i].activewash + "','" + fermenter.fermSetup[i].water + "','" + fermenter.fermSetup[i].other_material + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + fermenter.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + fermenter.record_status + "','"+fermenter.fermSetup[i].rawmaterial+"','"+fermenter.financial_year+"')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            a = cmd3.ExecuteNonQuery();
                            if (fermenter.record_status == "Y")
                            {
                                NpgsqlCommand cmd4 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + fermenter.fermSetup[i].fromstoragevat + "' and party_code='" + fermenter.party_code + "'", cn);
                                double d = Convert.ToDouble(cmd4.ExecuteScalar());
                                double v = Convert.ToDouble(d) - Convert.ToDouble(fermenter.total_qty_transferred);
                                NpgsqlCommand cmd5 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + v + "'  where vat_code='" + fermenter.fermSetup[i].fromstoragevat + "' and party_code='" + fermenter.party_code + "'", cn);
                                cmd5.ExecuteNonQuery();
                                //NpgsqlCommand cmd6 = new NpgsqlCommand("update exciseautomation.scm_molassespurchaseconsump a set totalconsumption_on_date = b.TotalConsumption82 from exciseautomation.ViewTotalConsumption82 b where a.entrydate = '" + fermenter.setup_date + "'   and a.partycode = '" + fermenter.party_code + "' ", cn);
                                //cmd6.ExecuteNonQuery();
                            }
                        }
                        value = "0";

                    }
                    else
                    {
                        value = "1";
                    }
                    
                     
                }
                catch (Exception ex)
                {

                    value = ex.Message;
                    //  throw (ex);
                }

                return value;
            }
        }



        public static string Update(Form82 fermenter)
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
                    if(fermenter.setup_time !="")
                    {
                        str.Append("UPDATE exciseautomation.rawmaterial_fermenter SET  setup_date ='" + fermenter.setup_date + "', setup_time ='" + fermenter.setup_time + "', party_code ='" + fermenter.party_code + "', tofermentervat ='" + fermenter.tofermentervat + "', total_qty_transferred ='" + fermenter.total_qty_transferred + "', no_of_vat_cask ='" + fermenter.no_of_vat_cask + "', total_bl_washsetup ='" + fermenter.total_bl_washsetup + "', sg_spentwash ='" + fermenter.sg_spentwash + "', sg_of_wash ='" + fermenter.sg_of_wash + "', setup_complete ='" + fermenter.setup_complete + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "',  record_status ='" + fermenter.record_status + "' WHERE rawmaterial_fermenter_id ='" + fermenter.rawmaterial_fermenter_id + "' and financial_year='" + fermenter.financial_year + "' ");
                      
                    }
                    else
                    {
                        str.Append("UPDATE exciseautomation.rawmaterial_fermenter SET  setup_date ='" + fermenter.setup_date + "', party_code ='" + fermenter.party_code + "', tofermentervat ='" + fermenter.tofermentervat + "', total_qty_transferred ='" + fermenter.total_qty_transferred + "', no_of_vat_cask ='" + fermenter.no_of_vat_cask + "', total_bl_washsetup ='" + fermenter.total_bl_washsetup + "', sg_spentwash ='" + fermenter.sg_spentwash + "', sg_of_wash ='" + fermenter.sg_of_wash + "', setup_complete ='" + fermenter.setup_complete + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "',  record_status ='" + fermenter.record_status + "' WHERE rawmaterial_fermenter_id ='" + fermenter.rawmaterial_fermenter_id + "' and financial_year='" + fermenter.financial_year + "' ");
                       
                    }
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("delete from exciseautomation.fermenter_setup where  rawmaterial_fermenter_id='" + fermenter.rawmaterial_fermenter_id + "' and financial_year='" + fermenter.financial_year + "'", cn);
                        cmd1.ExecuteNonQuery();
                        for (int i = 0; i < fermenter.fermSetup.Count; i++)
                        {
                            n = 0;
                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.fermenter_setup( rawmaterial_fermenter_id, fromstoragevat, no_of_each_vat, molasses, mahua, gur, spentwash, activewash, water, other_material,lastmodified_date, user_id, creation_date, record_status,attribute1,financial_year)");
                            str.Append("Values('" +fermenter.rawmaterial_fermenter_id + "', '" + fermenter.fermSetup[i].fromstoragevat + "','" + fermenter.fermSetup[i].no_of_each_vat + "','" + fermenter.fermSetup[i].molasses + "','" + fermenter.fermSetup[i].mahua + "','" + fermenter.fermSetup[i].gur + "','" + fermenter.fermSetup[i].spentwash + "','" + fermenter.fermSetup[i].activewash + "','" + fermenter.fermSetup[i].water + "','" + fermenter.fermSetup[i].other_material + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + fermenter.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + fermenter.record_status + "','" + fermenter.fermSetup[i].rawmaterial + "','"+fermenter.financial_year+"')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd3.ExecuteNonQuery();
                            if(fermenter.record_status=="Y")
                            {
                                NpgsqlCommand cmd4 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='"+ fermenter.fermSetup[i].fromstoragevat+ "' and party_code='" + fermenter.party_code + "'", cn);
                                double m = Convert.ToDouble( cmd4.ExecuteScalar());
                               double v=Convert.ToDouble(m) - Convert.ToDouble(fermenter.total_qty_transferred); 
                               NpgsqlCommand cmd5 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='"+v+"'  where vat_code='" + fermenter.fermSetup[i].fromstoragevat + "' and party_code='" + fermenter.party_code + "'", cn);
                                cmd5.ExecuteNonQuery();
                                //NpgsqlCommand cmd6 = new NpgsqlCommand("update exciseautomation.scm_molassespurchaseconsump a set totalconsumption_on_date = b.TotalConsumption82 from exciseautomation.ViewTotalConsumption82 b where a.entrydate = '" + fermenter.setup_date + "'   and a.partycode = '" + fermenter.party_code + "' ", cn);
                                //cmd6.ExecuteNonQuery();
                            }
                        }
                        VAL = "0";
                        //_log.Info("Sugarcanepurchase Insertion Sucess:" + scp.sugarcanepurchase_id + '-' + scp.party_code);
                        trn.Commit();
                        cn.Close();
                    }
                    else
                    {
                        trn.Rollback();
                        VAL = "1";
                        //_log.Info("Sugarcanepurchase Insertion Fail:" + scp.sugarcanepurchase_id + '-' + scp.party_code);
                    }
                }
                catch (Exception ex1)
                {
                    trn.Rollback();
                   // _log.Info("Sugarcanepurchase Insertion Fail:" + scp.sugarcanepurchase_id + '-' + scp.party_code + "-" + ex1.Message);
                    VAL = ex1.Message;
                }

            }
            return VAL;
        }

        public static string InsertFermenter(Form82 fermenter)
        {
            string value = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT max(fermenter_setup_id) FROM exciseautomation.fermenter_setup", cn);
                string m = cmd1.ExecuteScalar().ToString();
                int n = 0;
                if (m == "")
                    n = 1;
                else
                    n = Convert.ToInt32(m) + 1;

                try
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("INSERT INTO exciseautomation.fermenter_setup(fermenter_setup_id, rawmaterial_fermenter_id, fromstoragevat, no_of_each_vat, molasses, mahua, gur, spentwash, activewash, water, other_material,lastmodified_date, user_id, creation_date, record_status)Values(");
                    str.Append("'" + n + "','" + fermenter.rawmaterial_fermenter_id + "','" +fermenter.fromstoragevat+ "','" + fermenter.no_of_each_vat + "','" + fermenter.molasses + "','" + fermenter.mahua + "','" + fermenter.gur+ "','" + fermenter.spentwash+ "','" + fermenter.activewash + "','" + fermenter.water + "','" + fermenter.other_material + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + fermenter.user_id + "','" + fermenter.setup_date + "','" + fermenter.record_status + "')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                   int a = cmd.ExecuteNonQuery();

                    if(a == 1)
                    {
                        value = "1";
                    }
                    else
                    {
                        value = "0";
                    }

                }
                catch (Exception ex)
                {

                    value = ex.Message;
                    //  throw (ex);
                }

                return value;

            }
        }



        public static Form82 GetVatAval1(string Vat_code, string party_Code)
        {
            Form82 vat = new Form82();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    //  NpgsqlCommand cmd = new NpgsqlCommand("select a.vat_availablecapacity from exciseautomation.vat_master a where vat_code='" + Vat_code + "' and party_code='" + party_Code + "'", cn);
                    // NpgsqlCommand cmd = new NpgsqlCommand("select   coalesce( SUM(totalpurchase) - SUM(totalconsumption + total_wastage), 0) as closingbalance  from  exciseautomation.distillery_mollasses_stock dms  where vat_code ='" + Vat_code + "' and financial_year='"+party_Code+"'", cn);
                    NpgsqlCommand cmd = new NpgsqlCommand("select   coalesce( SUM(totalpurchase ) - SUM(totalconsumption + total_wastage), 0) as closingbalance,o.openingbalancevalue  from  exciseautomation.distillery_mollasses_stock dms inner join exciseautomation.openingbalance o on dms.vat_code= o.vat_code and dms.financial_year =o.financial_year  where dms.vat_code ='"+Vat_code+"' and dms.financial_year='"+party_Code+"' group  by o.openingbalancevalue ", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            // vat.vat_availablecapacity = Convert.ToDouble(dr["vat_availablecapacity"].ToString());
                            vat.vat_availablecapacity = Convert.ToDouble(dr["closingbalance"].ToString()) + Convert.ToDouble(dr["openingbalancevalue"].ToString());
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

        public static Form82 GetVatAval(string Vat_code, string party_Code)
        {
            Form82 vat = new Form82();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                     NpgsqlCommand cmd = new NpgsqlCommand("select a.vat_availablecapacity from exciseautomation.vat_master a where vat_code='" + Vat_code + "' and party_code='" + party_Code + "'", cn);
                    //NpgsqlCommand cmd = new NpgsqlCommand("select  SUM(totalpurchase) - SUM(totalconsumption + total_wastage) as closingbalance  from  exciseautomation.distillery_mollasses_stock dms  where vat_code ='" + Vat_code + "'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                             vat.vat_availablecapacity = Convert.ToDouble(dr["vat_availablecapacity"].ToString());
                           // vat.vat_availablecapacity = Convert.ToDouble(dr["closingbalance"].ToString());
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



        public static ProductType Getproduct(string Vat_code, string party_Code)
        {
            ProductType vat = new ProductType();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select b.product_type_name,b.product_type_code from exciseautomation.vat_master a inner join exciseautomation.product_type_master b on a.product_type_code=b.product_type_code where a.vat_code='"+Vat_code+"' and a.party_code='"+party_Code+"'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            vat.product_type_code = dr["product_type_code"].ToString();
                            vat.product_type_name = dr["product_type_name"].ToString();
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


        public static List<Form82> GetList()
        {

            List<Form82> fermenter = new List<Form82>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.vat_name,c.party_name from exciseautomation.rawmaterial_fermenter a inner join exciseautomation.vat_master b on a.tofermentervat=b.vat_code inner join exciseautomation.party_master c on a.party_code=c.party_code where a.record_active='true' order by a.setup_date desc", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            fermenter = new List<Form82>();
                            int r = 0;
                            while (dr.Read())
                            {
                                Form82 record = new Form82();
                                record.rawmaterial_fermenter_id= Convert.ToInt32(dr["rawmaterial_fermenter_id"].ToString());
                                record.setup_date = Convert.ToDateTime( dr["setup_date"]).ToString("dd-MM-yyyy");
                                record.setup_time = dr["setup_time"].ToString();
                                record.party_name = dr["party_name"].ToString();
                                record.financial_year = dr["financial_year"].ToString();
                                record.tofermentervat = dr["tofermentervat"].ToString();
                                record.no_of_vat_cask = dr["no_of_vat_cask"].ToString();
                                record.total_qty_transferred = Convert.ToDouble( dr["total_qty_transferred"].ToString());
                                record.vat_name = dr["vat_name"].ToString();
                                record.party_code = dr["party_code"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.setup_complete = dr["setup_complete"].ToString();
                                fermenter.Add(record);
                            }
                        }
                    }
                    //_log.Info("Sugarcanepurchase Get List Success");
                    cn.Close();
                }
                catch (Exception ex)
                {
                    //_log.Info("Sugarcanepurchase Get List Fail :" + ex.Message);
                    throw (ex);
                }

            }
            return fermenter;
        }
        public static List<Form82> Search(string tablename, string column, string value)
        {
            List<Form82> mir = new List<Form82>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.vat_name,c.party_name from exciseautomation.rawmaterial_fermenter a inner join exciseautomation.vat_master b on a.tofermentervat=b.vat_code inner join exciseautomation.party_master c on a.party_code=c.party_code where  " + column + " Ilike '%" + value + "%'  and a.record_active='true'  order by setup_date desc", cn);
                    
                       // cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            mir = new List<Form82>();
                            while (dr.Read())
                            {
                                Form82 record = new Form82();
                                record.rawmaterial_fermenter_id = Convert.ToInt32(dr["rawmaterial_fermenter_id"].ToString());
                                record.setup_date = Convert.ToDateTime(dr["setup_date"]).ToString("dd-MM-yyy");
                                record.setup_time = dr["setup_time"].ToString();
                                record.party_name = dr["party_name"].ToString();
                                record.tofermentervat = dr["tofermentervat"].ToString();
                               record.financial_year = dr["financial_year"].ToString();
                               record.no_of_vat_cask = dr["no_of_vat_cask"].ToString();
                                record.total_qty_transferred = Convert.ToDouble(dr["total_qty_transferred"].ToString());
                                record.vat_name = dr["vat_name"].ToString();
                                record.party_code = dr["party_code"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.setup_complete = dr["setup_complete"].ToString();
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



        public static List<Form82> Getdistinctdate(string party)
        {

            List<Form82> fermenter = new List<Form82>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select distinct(a.setup_date) from exciseautomation.rawmaterial_fermenter a inner join exciseautomation.vat_master b on a.tofermentervat=b.vat_code inner join exciseautomation.party_master c on a.party_code=c.party_code where a.setup_complete='N' and a.record_status='Y' and c.party_code='"+party+"' order by a.setup_date", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            fermenter = new List<Form82>();
                            int r = 0;
                            while (dr.Read())
                            {
                                Form82 record = new Form82();
                               
                                record.setup_date = Convert.ToDateTime(dr["setup_date"]).ToString("dd-MM-yyy");
                               
                                fermenter.Add(record);
                            }
                        }
                    }
                    //_log.Info("Sugarcanepurchase Get List Success");
                    cn.Close();
                }
                catch (Exception ex)
                {
                    //_log.Info("Sugarcanepurchase Get List Fail :" + ex.Message);
                    throw (ex);
                }

            }
            return fermenter;
        }

        public static Form82 GetDetails(int Fermenterid,string financial_year)
        {
            Form82 Fermenter = new Form82();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select *from exciseautomation.rawmaterial_fermenter where rawmaterial_fermenter_id='"+Fermenterid+"' and financial_year='"+financial_year+"' ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr1 = cmd.ExecuteReader();
                        DataTable scp = new DataTable();
                        scp.Load(dr1);
                        dr1.Close();
                        foreach (DataRow dr in scp.Rows)
                        {
                            Fermenter.rawmaterial_fermenter_id = Convert.ToInt32(dr["rawmaterial_fermenter_id"].ToString());
                            Fermenter.party_code = dr["party_code"].ToString();
                            Fermenter.setup_date = Convert.ToDateTime( dr["setup_date"]).ToString("dd-MM-yyy");
                           Fermenter.setup_time = dr["setup_time"].ToString();
                            Fermenter.tofermentervat = dr["tofermentervat"].ToString();
                          //  Fermenter.vat_name = dr["vat_name"].ToString();
                            Fermenter.no_of_vat_cask = dr["no_of_vat_cask"].ToString();
                            Fermenter.total_qty_transferred = Convert.ToDouble( dr["total_qty_transferred"].ToString());
                           Fermenter.sg_of_wash = Convert.ToDouble(dr["sg_of_wash"].ToString());
                            Fermenter.sg_spentwash = Convert.ToDouble(dr["sg_spentwash"].ToString());
                           Fermenter.total_bl_washsetup = Convert.ToDouble(dr["total_bl_washsetup"].ToString());
                           Fermenter.record_status = dr["record_status"].ToString();
                          
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select a.*,b.vat_name,b.vat_code from exciseautomation.fermenter_setup a inner join exciseautomation.vat_master b on a.fromstoragevat=b.vat_code  where rawmaterial_fermenter_id='" + Fermenterid+ "' and a.financial_year='" + financial_year + "'", cn))
                            {
                                cmd1.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr2 = cmd1.ExecuteReader();
                                Fermenter.fermSetup = new List<FermenterSetUp>();
                                if (dr2.HasRows)
                                {

                                    while (dr2.Read())
                                    {
                                        FermenterSetUp Setup = new FermenterSetUp();
                                       Setup.fermenter_setup_id = Convert.ToInt32(dr2["fermenter_setup_id"].ToString());
                                       Setup.rawmaterial_fermenter_id= Convert.ToInt32(dr2["rawmaterial_fermenter_id"].ToString());
                                        Setup.fromstoragevat = dr2["fromstoragevat"].ToString();
                                        Setup.no_of_each_vat =dr2["no_of_each_vat"].ToString();
                                        Setup.rawmaterial= dr2["attribute1"].ToString();
                                        Setup.vat_name = dr2["vat_name"].ToString();
                                        Setup.vat_code= dr2["vat_code"].ToString();
                                        Setup.molasses = Convert.ToDouble( dr2["molasses"].ToString());
                                        Setup.mahua = Convert.ToDouble(dr2["mahua"].ToString());
                                        Setup.gur = Convert.ToDouble(dr2["gur"].ToString());
                                        Setup.spentwash = Convert.ToDouble(dr2["spentwash"].ToString());
                                        Setup.activewash = Convert.ToDouble(dr2["activewash"].ToString());
                                        Setup.water= Convert.ToDouble(dr2["water"].ToString());
                                        Setup.other_material = dr2["other_material"].ToString();
                                        Fermenter.fermSetup.Add(Setup);
                                    }

                                }

                            }
                        }
                    }
                    //_log.Info("Sugarcanepurchase Get List Success");
                    cn.Close();
                }
                catch (Exception ex)
                {
                    //_log.Info("Sugarcanepurchase Get List Fail :" + ex.Message);
                }

            }
            return Fermenter;
        }
        public static string Approve(Form82 fermenter)
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
                    str.Append("update exciseautomation.rawmaterial_fermenter set  record_status='" + fermenter.record_status + "' where rawmaterial_fermenter_id='" + fermenter.rawmaterial_fermenter_id + "' and and financial_year='" + fermenter.financial_year + "'");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (fermenter.record_status == "A")
                    {
                        fermenter.record_status = "Approved By Bond Officer";
                    }
                    else
                    {
                        fermenter.record_status = "Rejected By Bond Officer";
                    }
                    str = new StringBuilder();
                    str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                    str.Append("'" + fermenter.rawmaterial_fermenter_id + "','','" + DateTime.Now.ToString() + "','F82','" + fermenter.record_status + "','" + fermenter.remarks + "','" + DateTime.Now.ToString() + "','" + fermenter.user_id + "','" + DateTime.Now.ToString() + "','" + fermenter.user_id + "','"+fermenter.financial_year+"','"+fermenter.party_code+"')");
                    cmd = new NpgsqlCommand(str.ToString(), cn);
                    n = cmd.ExecuteNonQuery();
                    VAL = "0";
                    //_log.Info("Sugarcanepurchase " + scp.record_status + " Sucess:" + scp.sugarcanepurchase_id + '-' + scp.party_code);
                    trn.Commit();
                    cn.Close();
                }
                catch (Exception ex1)
                {
                    trn.Rollback();
                   //_log.Info("Sugarcanepurchase " + scp.record_status + " Fail:" + scp.sugarcanepurchase_id + '-' + scp.party_code + "-" + ex1.Message);
                    VAL = ex1.Message;
                }
            }
            return VAL;
        }
        public static int GetDuplicket( string value, string value2)
        {
            int value1 = 0;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    cmd = new NpgsqlCommand("select count(1) from exciseautomation.fermenter_setup a inner join exciseautomation.rawmaterial_fermenter b on a.rawmaterial_fermenter_id=b.rawmaterial_fermenter_id and  a.fromstoragevat='"+value+"' and b.tofermentervat='"+value2+"'", cn);
            string re = cmd.ExecuteScalar().ToString();
            if (re == "1")
            {
                value1 = 1;
            //    _log.Info("Get Existing data Success :" + tablename);
            }
            else
            {
                if (re != "")
                    value1 = Convert.ToInt32(re);
              //  _log.Info("Get Existing data Fail :" + tablename);
            }

        }
                catch (Exception ex)
                {
                 //   _log.Info("Get Existing data Fail :" +tablename + "-" + ex.Message);
                    value1 =0;
                }
}
            return value1;
        }
        public static Form82 Getfermentervat(string date, string Code)
        {
            Form82 vat = new Form82();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select * from  exciseautomation.rawmaterial_fermenter  where  setup_date='"+date+"' and tofermentervat='"+Code+"'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            vat.total_qty_transferred = Convert.ToDouble(dr["total_qty_transferred"].ToString());
                            vat.total_bl_washsetup= Convert.ToDouble(dr["total_bl_washsetup"].ToString());
                            vat.sg_spentwash= Convert.ToDouble(dr["sg_spentwash"].ToString());
                            vat.sg_of_wash = Convert.ToDouble(dr["sg_of_wash"].ToString());
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


        public static int GetExistsData(string tablename, string date, string value)
        {
            int value1 = 0;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                        cmd = new NpgsqlCommand("select count(1) from exciseautomation.rawmaterial_fermenter where setup_date='"+date+"' and tofermentervat ='"+value+"'", cn);
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



        public static Form82 GetSetupdate(string party, string date,string vatcode)
        {
            Form82 vat = new Form82();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.* from  exciseautomation.rawmaterial_fermenter a inner join exciseautomation.distillation b on a.rawmaterial_fermenter_id=b.rawmaterial_fermenter_id where b.distillation_date='" +date+"' and a.party_code='"+party+ "' and a.tofermentervat='"+vatcode+"'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            vat.setup_date = dr["setup_date"].ToString();
                            vat.total_qty_transferred = Convert.ToDouble(dr["total_qty_transferred"].ToString());
                            vat.total_bl_washsetup = Convert.ToDouble(dr["total_bl_washsetup"].ToString());
                            vat.sg_spentwash = Convert.ToDouble(dr["sg_spentwash"].ToString());
                            vat.sg_of_wash = Convert.ToDouble(dr["sg_of_wash"].ToString());
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

    }
}
