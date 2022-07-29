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
    public class DL_Distillation
    {


        public static List<Distillation> GetList()
        {
            List<Distillation> distillation = new List<Distillation>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;

                    cmd = new NpgsqlCommand("select d.*,b.vat_name,c.party_name,c.party_code,a.setup_date,a.total_qty_transferred,a.financial_year from exciseautomation.rawmaterial_fermenter a inner join exciseautomation.vat_master b on a.tofermentervat=b.vat_code inner join exciseautomation.party_master c on a.party_code=c.party_code inner join exciseautomation.distillation d on a.rawmaterial_fermenter_id=d.rawmaterial_fermenter_id and a.financial_year=d.financial_year  order by d.distillation_date desc", cn);

                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        distillation = new List<Distillation>();
                        while (dr.Read())
                        {
                            Distillation record = new Distillation();
                            record.distillation_id = Convert.ToInt32(dr["distillation_id"].ToString());
                            record.rawmaterial_fermenter_id = Convert.ToInt32(dr["rawmaterial_fermenter_id"].ToString());
                            record.setup_date = dr["setup_date"].ToString().Replace("/", "-").Substring(0, 10);
                            record.distillation_date = dr["distillation_date"].ToString().Replace("/", "-").Substring(0, 10);
                            record.party_code = dr["party_code"].ToString();
                            record.financial_year = dr["financial_year"].ToString();
                            record.total_qty_transferred = Convert.ToDouble(dr["total_qty_transferred"].ToString());
                            record.vat_name = dr["vat_name"].ToString();
                            record.record_status = dr["record_status"].ToString();
                            record.party_name = dr["party_name"].ToString();
                            distillation.Add(record);
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return distillation;
        }
        public static List<Distillation> Search(string tablename, string column, string value)
        {
            List<Distillation> mir = new List<Distillation>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {


                    using (NpgsqlCommand cmd = new NpgsqlCommand("select d.*,b.vat_name,c.party_name,c.party_code,a.setup_date,a.total_qty_transferred,a.financial_year from exciseautomation.rawmaterial_fermenter a inner join exciseautomation.vat_master b on a.tofermentervat=b.vat_code inner join exciseautomation.party_master c on a.party_code=c.party_code inner join exciseautomation.distillation d on a.rawmaterial_fermenter_id=d.rawmaterial_fermenter_id where  "+column+" Ilike '%"+value+ "%'   order by d.distillation_date desc ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            mir = new List<Distillation>();
                            while (dr.Read())
                            {
                                Distillation record = new Distillation();
                                record.distillation_id = Convert.ToInt32(dr["distillation_id"].ToString());
                                record.rawmaterial_fermenter_id = Convert.ToInt32(dr["rawmaterial_fermenter_id"].ToString());
                                record.setup_date = dr["setup_date"].ToString().Replace("/", "-").Substring(0, 10);
                                record.distillation_date = dr["distillation_date"].ToString().Replace("/", "-").Substring(0, 10);
                                record.party_code = dr["party_code"].ToString();
                                record.financial_year = dr["financial_year"].ToString();
                                record.total_qty_transferred = Convert.ToDouble(dr["total_qty_transferred"].ToString());
                                record.vat_name = dr["vat_name"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.party_name = dr["party_name"].ToString();
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





        public static string Insert(Distillation Distillation)
        {
            string value = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
               // where financial_year = '"+Distillation.financial_year+"'
                NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT max(distillation_id) FROM exciseautomation.distillation ", cn);
                string m = cmd1.ExecuteScalar().ToString();
                int n = 0;
                if (m == "")
                    n = 1;
                else
                    n = Convert.ToInt32(m) + 1;
                NpgsqlCommand cmd3 = new NpgsqlCommand("select rawmaterial_fermenter_id from exciseautomation.rawmaterial_fermenter where setup_date='" + Distillation.setup_date + "' and tofermentervat='" + Distillation.vat_code + "' and user_id='"+Distillation.user_id+ "' and  setup_complete ='N' and financial_year='" + Distillation.financial_year + "' ", cn);
                int c = Convert.ToInt32(cmd3.ExecuteScalar());

                NpgsqlCommand cmd4 = new NpgsqlCommand("select fermenter_setup_id from exciseautomation.fermenter_setup where rawmaterial_fermenter_id='" + c + "' and financial_year='" + Distillation.financial_year + "' ", cn);
                int d = Convert.ToInt32(cmd4.ExecuteScalar());
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("INSERT INTO exciseautomation.distillation( distillation_id,rawmaterial_fermenter_id, fermenter_setup_id, final_sg, degree_of_attenuation, no_of_vat_cask, distillation_date, start_date, start_time, end_date, end_time, bl_to_still, to_which_still, total_bl_removed_from_distillation, total_lp_removed_from_distillation, distillation_complete, bl_redistillation, from_vessel, to_which_still_removed, bl_produced, lp_produced, bl_per_material, lp_per_material, degree_per100wash, spirit_charge_register, remarks, lastmodified_date, user_id, creation_date, record_status,financial_year)Values(");
                    str.Append("'"+n+"','" +c+ "','" + d + "','" + Distillation.final_sg + "','" + Distillation.degree_of_attenuation + "','" + Distillation.no_of_vat_cask + "','" + Distillation.distillation_date + "','" + Distillation.start_date + "','" + Distillation.start_time + "','" + Distillation.end_date + "','" + Distillation.end_time + "','" + Distillation.bl_to_still + "','" + Distillation.to_which_still + "','" + Distillation.total_bl_removed_from_distillation + "','" + Distillation.total_lp_removed_from_distillation + "','" + Distillation.distillation_complete + "','" + Distillation.bl_redistillation + "','" + Distillation.from_vessel + "','" + Distillation.to_which_still_removed + "','" + Distillation.bl_produced + "','" + Distillation.lp_produced + "','" + Distillation.bl_per_material + "','" + Distillation.lp_per_material + "','" + Distillation.degree_per100wash + "','" + Distillation.spirit_charge_register + "','" + Distillation.remarks + "','" + DateTime.Now.ToShortDateString() + "','" + Distillation.user_id + "','" + DateTime.Now.ToShortDateString() + "','" + Distillation.record_status + "','"+Distillation.financial_year+"')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int a = cmd.ExecuteNonQuery();

                    if(Distillation.record_status=="Y")
                    { 
                    if(Distillation.distillation_complete=="Y")
                    {
                        NpgsqlCommand cmd6 = new NpgsqlCommand("UPDATE exciseautomation.rawmaterial_fermenter SET   setup_complete ='Y' WHERE rawmaterial_fermenter_id ='" + c+ "' and financial_year='" + Distillation.financial_year + "'", cn);
                        int g = Convert.ToInt32(cmd6.ExecuteScalar());
                    }
                    //else
                    //{
                    //    NpgsqlCommand cmd6 = new NpgsqlCommand("UPDATE exciseautomation.rawmaterial_fermenter SET   setup_complete ='N' WHERE rawmaterial_fermenter_id ='" + c+ "'", cn);
                    //    int g = Convert.ToInt32(cmd6.ExecuteScalar());

                    //}
                    }
                    if (a == 1)
                    {
                      
                        for (int i = 0; i < Distillation.DStore.Count; i++)
                        {
                            //where financial_year='" + Distillation.financial_year + "'
                            NpgsqlCommand cmd2 = new NpgsqlCommand("SELECT max(distillation_id) FROM exciseautomation.distillation ", cn);
                            int b = Convert.ToInt32(cmd2.ExecuteScalar());
                            NpgsqlCommand cvs = new NpgsqlCommand("SELECT max(distillation_tostore_id) FROM exciseautomation.distillation_tostore ", cn);
                            string k = cvs.ExecuteScalar().ToString();
                            int j = 0;
                            if (k == "")
                                j = 1;
                            else
                                j = Convert.ToInt32(k) + 1;
                            a = 0;
                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.distillation_tostore( distillation_tostore_id,distillation_id, vat_code, bl_store, lp_store, lastmodified_date, user_id, creation_date, record_status,moved_to_nextstage,financial_year)");
                            str.Append("Values('"+j+"','" + b + "', '" + Distillation.DStore[i].vat_code + "','" + Distillation.DStore[i].bl_store + "','" + Distillation.DStore[i].lp_store + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + Distillation.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + Distillation.record_status + "','N','"+Distillation.financial_year+"')");
                            NpgsqlCommand cmd5 = new NpgsqlCommand(str.ToString(), cn);
                            a = cmd5.ExecuteNonQuery();
                            
                            //if (Distillation.record_status == "Y")
                            //{
                            //    NpgsqlCommand cmd6 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + Distillation.DStore[i].vat_code + "' and party_code='" + Distillation.party_code + "'", cn);
                            //    double k = Convert.ToDouble(cmd6.ExecuteScalar());
                            //    double v = Convert.ToDouble(k) + Convert.ToDouble(Distillation.DStore[i].bl_store);
                            //    NpgsqlCommand cmd7 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + v + "'  where vat_code='" + Distillation.DStore[i].vat_code + "' and party_code='" + Distillation.party_code + "'", cn);
                            //    cmd7.ExecuteNonQuery();
                            //}
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






        public static string Update(Distillation distillation)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                NpgsqlCommand cmd5;
                //     cmd5 = new NpgsqlCommand("select a.rawmaterial_fermenter_id from exciseautomation.rawmaterial_fermenter a inner join  exciseautomation.distillation b on a.rawmaterial_fermenter_id=b.rawmaterial_fermenter_id where distillation_id='"+distillation.distillation_id+"' ", cn);
                //int c = Convert.ToInt32(cmd5.ExecuteScalar());
                //if(c==0)
               // {
                    cmd5 = new NpgsqlCommand("select rawmaterial_fermenter_id from exciseautomation.rawmaterial_fermenter where setup_date='" + distillation.setup_date + "' and tofermentervat='" + distillation.vat_code + "' and user_id='" + distillation.user_id + "' and financial_year='" + distillation.financial_year + "' ", cn);
                //}
            int c = Convert.ToInt32(cmd5.ExecuteScalar());
                NpgsqlCommand cmd4 = new NpgsqlCommand("select fermenter_setup_id from exciseautomation.fermenter_setup where rawmaterial_fermenter_id='" + c + "' and financial_year='" + distillation.financial_year + "' ", cn);
                int d = Convert.ToInt32(cmd4.ExecuteScalar());
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("UPDATE exciseautomation.distillation SET rawmaterial_fermenter_id='"+c+"', fermenter_setup_id='"+d+"',  final_sg ='" + distillation.final_sg+ "', degree_of_attenuation ='" + distillation.degree_of_attenuation + "', no_of_vat_cask ='" + distillation.no_of_vat_cask + "', distillation_date ='" + distillation.distillation_date + "', start_date ='" + distillation.start_date + "', start_time ='" + distillation.start_time + "', end_date ='" + distillation.end_date + "', end_time ='" + distillation.end_time + "', bl_to_still ='" + distillation.bl_to_still + "', to_which_still ='" + distillation.to_which_still + "', total_bl_removed_from_distillation ='" + distillation.total_bl_removed_from_distillation + "', total_lp_removed_from_distillation ='" + distillation.total_lp_removed_from_distillation + "', distillation_complete ='" + distillation.distillation_complete + "', bl_redistillation ='" + distillation.bl_redistillation + "', from_vessel ='" + distillation.from_vessel + "', to_which_still_removed ='" + distillation.to_which_still_removed + "', bl_produced ='" + distillation.bl_produced + "', lp_produced ='" + distillation.lp_produced + "', bl_per_material ='" + distillation.bl_per_material + "', lp_per_material ='" + distillation.lp_per_material + "', degree_per100wash ='" + distillation.degree_per100wash + "', spirit_charge_register ='" + distillation.spirit_charge_register + "', remarks ='" + distillation.remarks + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "', record_status ='" + distillation.record_status + "' WHERE distillation_id ='" + distillation.distillation_id + "' and financial_year='" + distillation.financial_year + "' ");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (distillation.record_status == "Y")
                    {
                        if (distillation.distillation_complete == "Y")
                        {
                            NpgsqlCommand cmd6 = new NpgsqlCommand("UPDATE exciseautomation.rawmaterial_fermenter SET   setup_complete ='Y' WHERE rawmaterial_fermenter_id ='" +c+ "' and financial_year='" + distillation.financial_year + "'", cn);
                            int g = Convert.ToInt32(cmd6.ExecuteScalar());
                        }
                        //else
                        //{
                        //    NpgsqlCommand cmd6 = new NpgsqlCommand("UPDATE exciseautomation.rawmaterial_fermenter SET   setup_complete ='N' WHERE rawmaterial_fermenter_id ='" +c+ "'", cn);
                        //    int g = Convert.ToInt32(cmd6.ExecuteScalar());

                        //}
                    }
                    if (n == 1)
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("delete from exciseautomation.distillation_tostore where  distillation_id='" + distillation.distillation_id + "' and financial_year='" + distillation.financial_year + "'", cn);
                        cmd1.ExecuteNonQuery();
                        for (int i = 0; i < distillation.DStore.Count; i++)
                        {
                            NpgsqlCommand cvs = new NpgsqlCommand("SELECT max(distillation_tostore_id) FROM exciseautomation.distillation_tostore ", cn);
                            string k = cvs.ExecuteScalar().ToString();
                            int j = 0;
                            if (k == "")
                                j = 1;
                            else
                                j = Convert.ToInt32(k) + 1;
                            n = 0;
                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.distillation_tostore(distillation_tostore_id,distillation_id, vat_code, bl_store, lp_store, lastmodified_date, user_id, creation_date, record_status,moved_to_nextstage,financial_year)");
                            str.Append("Values('"+j+"','" + distillation.distillation_id + "', '" + distillation.DStore[i].vat_code + "','" + distillation.DStore[i].bl_store + "','" + distillation.DStore[i].lp_store + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + distillation.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + distillation.record_status + "','N','"+distillation.financial_year+"')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd3.ExecuteNonQuery();
                          
                                //NpgsqlCommand cmd6 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + distillation.DStore[i].vat_code + "' and party_code='" + distillation.party_code + "'", cn);
                                //double k = Convert.ToDouble(cmd6.ExecuteScalar());
                                //double v = Convert.ToDouble(k) + Convert.ToDouble(distillation.DStore[i].bl_store);
                                //NpgsqlCommand cmd7 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + v + "'  where vat_code='" + distillation.DStore[i].vat_code + "' and party_code='" + distillation.party_code + "'", cn);
                                //cmd7.ExecuteNonQuery();
                           
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

        public static string AdminUpdate(Distillation distillation)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                NpgsqlCommand cmd5;
               
                cmd5 = new NpgsqlCommand("select a.rawmaterial_fermenter_id from exciseautomation.rawmaterial_fermenter a inner join exciseautomation.distillation b  on a.rawmaterial_fermenter_id=b.rawmaterial_fermenter_id   where b.distillation_id='" + distillation.distillation_id + "' and financial_year='"+distillation.financial_year+"' ", cn);
               
                int c = Convert.ToInt32(cmd5.ExecuteScalar());
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("UPDATE exciseautomation.distillation SET  start_date ='" + distillation.start_date + "',  end_date ='" + distillation.end_date + "', bl_to_still ='" + distillation.bl_to_still + "', to_which_still ='" + distillation.to_which_still + "', total_bl_removed_from_distillation ='" + distillation.total_bl_removed_from_distillation + "', total_lp_removed_from_distillation ='" + distillation.total_lp_removed_from_distillation + "'  WHERE distillation_id ='" + distillation.distillation_id + "' and financial_year='" + distillation.financial_year + "' ");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                  
                        //if (distillation.distillation_complete == "Y")
                        //{
                        //    NpgsqlCommand cmd6 = new NpgsqlCommand("UPDATE exciseautomation.rawmaterial_fermenter SET   setup_complete ='Y' WHERE rawmaterial_fermenter_id ='" + c + "'", cn);
                        //    int g = Convert.ToInt32(cmd6.ExecuteScalar());
                        //}
                    str = new StringBuilder();
                    str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year) VALUES(");
                    str.Append("'" + distillation.distillation_id + "','','" + DateTime.Now.ToString() + "','F82D','Approved By Admin ','Approved By Admin','" + DateTime.Now.ToString() + "','" + distillation.user_id + "','" + DateTime.Now.ToString() + "','" + distillation.user_id + "','"+distillation.financial_year+"')");
                    cmd = new NpgsqlCommand(str.ToString(), cn);
                    n = cmd.ExecuteNonQuery();
                    VAL = "0";
                    trn.Commit();
                    cn.Close();
                }
                catch (Exception ex1)
                {
                    trn.Rollback();
                    VAL = ex1.Message;
                }
            }
            return VAL;
        }



        public static Distillation GetDetails(int Distillationid, string financial_year)
        {
            Distillation distillation = new Distillation();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT a.*,b.setup_date,b.total_qty_transferred,b.tofermentervat,b.total_bl_washsetup,b.sg_of_wash,c.vat_name FROM exciseautomation.distillation a inner join exciseautomation.rawmaterial_fermenter b on a.rawmaterial_fermenter_id=b.rawmaterial_fermenter_id inner join exciseautomation.vat_master c on b.tofermentervat=c.vat_code  where a.distillation_id ='" + Distillationid + "' and a.financial_year='"+financial_year+"'", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr1 = cmd.ExecuteReader();
                        DataTable scp = new DataTable();
                        scp.Load(dr1);
                        dr1.Close();
                        foreach (DataRow dr in scp.Rows)
                        {
                            distillation.distillation_id = Convert.ToInt32(dr["distillation_id"].ToString());
                            distillation.rawmaterial_fermenter_id = Convert.ToInt32(dr["rawmaterial_fermenter_id"].ToString());
                            distillation.distillation_date = Convert.ToDateTime(dr["distillation_date"]).ToString("dd-MM-yyy");
                            distillation.final_sg = Convert.ToDouble(dr["final_sg"].ToString());
                            distillation.degree_of_attenuation = Convert.ToDouble(dr["degree_of_attenuation"].ToString());
                            distillation.no_of_vat_cask = dr["no_of_vat_cask"].ToString();
                            distillation.start_date = Convert.ToDateTime(dr["start_date"]).ToString("dd-MM-yyy");
                            distillation.start_time = dr["start_time"].ToString();
                            distillation.end_date = Convert.ToDateTime(dr["end_date"]).ToString("dd-MM-yyy");
                            distillation.end_time = dr["end_time"].ToString();
                            distillation.bl_to_still = Convert.ToDouble(dr["bl_to_still"].ToString());
                            distillation.to_which_still = dr["to_which_still"].ToString();
                            distillation.total_bl_removed_from_distillation = Convert.ToDouble(dr["total_bl_removed_from_distillation"].ToString());
                            distillation.total_lp_removed_from_distillation = Convert.ToDouble(dr["total_lp_removed_from_distillation"].ToString());
                            distillation.distillation_complete = dr["distillation_complete"].ToString();
                            distillation.from_vessel = dr["from_vessel"].ToString();
                            distillation.to_which_still_removed = dr["to_which_still_removed"].ToString();
                            distillation.bl_produced = Convert.ToDouble(dr["bl_produced"].ToString());
                            distillation.lp_produced = Convert.ToDouble(dr["lp_produced"].ToString());
                            distillation.bl_per_material = Convert.ToDouble(dr["bl_per_material"].ToString());
                            distillation.lp_per_material = Convert.ToDouble(dr["lp_per_material"].ToString());
                            distillation.degree_per100wash = Convert.ToDouble(dr["degree_per100wash"].ToString());
                            distillation.spirit_charge_register = dr["spirit_charge_register"].ToString();
                            distillation.setup_date = Convert.ToDateTime(dr["setup_date"]).ToString("dd-MM-yyy");
                            distillation.tofermentervat = dr["tofermentervat"].ToString();
                            distillation.total_qty_transferred = Convert.ToDouble(dr["total_qty_transferred"].ToString());
                            distillation.total_bl_washsetup = Convert.ToDouble(dr["total_bl_washsetup"].ToString());
                            distillation.sg_spentwash = Convert.ToDouble(dr["sg_of_wash"].ToString());
                            distillation.vat_name = dr["vat_name"].ToString();
                            distillation.remarks = dr["remarks"].ToString();
                            distillation.record_status = dr["record_status"].ToString();
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select a.*,b.vat_name from exciseautomation.distillation_tostore a inner join exciseautomation.vat_master b on a.vat_code=b.vat_code  where a.distillation_id= '" + Distillationid + "' and a.financial_year='"+financial_year+"'", cn))
                            {
                                cmd1.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr2 = cmd1.ExecuteReader();
                                distillation.DStore = new List<DistillationToStore>();
                                if (dr2.HasRows)
                                {

                                    while (dr2.Read())
                                    {
                                        DistillationToStore Setup = new DistillationToStore();
                                        // Setup.distillation_tostore_id = Convert.ToInt32(dr["distillation_tostore_id"].ToString());
                                        /// Setup.distillation_id= Convert.ToInt32(dr["distillation_id"].ToString());
                                        Setup.bl_store = Convert.ToDouble(dr2["bl_store"].ToString());
                                        Setup.lp_store = Convert.ToDouble(dr2["lp_store"].ToString());
                                        Setup.vat_name = dr2["vat_name"].ToString();
                                        Setup.vat_code = dr2["vat_code"].ToString();
                                        distillation.DStore.Add(Setup);
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
            return distillation;
        }


        public static string Approve(Distillation distillation)
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
                    str.Append("update exciseautomation.distillation set  record_status='" + distillation.record_status + "' where distillation_id='" + distillation.distillation_id + "' and financial_year='" + distillation.financial_year + "'");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (distillation.record_status == "A")
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("delete from exciseautomation.distillation_tostore where  distillation_id='" + distillation.distillation_id + "' and financial_year='" + distillation.financial_year + "'", cn);
                        cmd1.ExecuteNonQuery();
                        for (int i = 0; i < distillation.DStore.Count; i++)
                        {
                            NpgsqlCommand cvs = new NpgsqlCommand("SELECT max(distillation_tostore_id) FROM exciseautomation.distillation_tostore ", cn);
                            string g = cvs.ExecuteScalar().ToString();
                            int j= 0;
                            if (g == "")
                                j = 1;
                            else
                                j = Convert.ToInt32(g) + 1;
                            n = 0;
                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.distillation_tostore(distillation_tostore_id,distillation_id, vat_code, bl_store, lp_store, lastmodified_date, user_id, creation_date, record_status,moved_to_nextstage,financial_year)");
                            str.Append("Values('"+j+"','" + distillation.distillation_id + "', '" + distillation.DStore[i].vat_code + "','" + distillation.DStore[i].bl_store + "','" + distillation.DStore[i].lp_store + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + distillation.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + distillation.record_status + "','N','"+distillation.financial_year+"')");
                            NpgsqlCommand cmd11 = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd11.ExecuteNonQuery();

                            NpgsqlCommand cmd6 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + distillation.DStore[i].vat_code + "' and party_code='" + distillation.party_code + "'", cn);
                            double k = Convert.ToDouble(cmd6.ExecuteScalar());
                            double v = Convert.ToDouble(k) + Convert.ToDouble(distillation.DStore[i].bl_store);
                            NpgsqlCommand cmd7 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + v + "'  where vat_code='" + distillation.DStore[i].vat_code + "' and party_code='" + distillation.party_code + "'", cn);
                            cmd7.ExecuteNonQuery();

                        }
                        distillation.record_status = "Approved By Bond Officer";
                        //StringBuilder str1 = new StringBuilder();
                        //str1.Append("update exciseautomation.distillation set distillation_complete='Y' where distillation_id='"+ distillation.distillation_id +"'");
                        //NpgsqlCommand cmd3 = new NpgsqlCommand(str1.ToString(), cn);
                        //cmd3.ExecuteNonQuery();
                        //NpgsqlCommand cmd5;
                        //cmd5 = new NpgsqlCommand("select a.rawmaterial_fermenter_id from exciseautomation.rawmaterial_fermenter a inner join  exciseautomation.distillation b on a.rawmaterial_fermenter_id=b.rawmaterial_fermenter_id where distillation_id='" + distillation.distillation_id + "' ", cn);
                        //int c = Convert.ToInt32(cmd5.ExecuteScalar());
                        //str = new StringBuilder();
                        //str.Append("update  exciseautomation.rawmaterial_fermenter set setup_complete='Y' where rawmaterial_fermenter_id='"+c+"'");
                        //NpgsqlCommand cmd2 = new NpgsqlCommand(str.ToString(), cn);
                        // cmd2.ExecuteNonQuery();
                    }
                    else
                    {
                        NpgsqlCommand cmd5;
                        cmd5 = new NpgsqlCommand("select a.rawmaterial_fermenter_id from exciseautomation.rawmaterial_fermenter a inner join  exciseautomation.distillation b on a.rawmaterial_fermenter_id=b.rawmaterial_fermenter_id where b.distillation_id='" + distillation.distillation_id + "' and b.financial_year='" + distillation.financial_year + "' ", cn);
                        int c = Convert.ToInt32(cmd5.ExecuteScalar());
                        NpgsqlCommand cmd2 = new NpgsqlCommand("update  exciseautomation.rawmaterial_fermenter set setup_complete='N' where rawmaterial_fermenter_id='" + c + "' and financial_year='" + distillation.financial_year + "'", cn);
                        cmd2.ExecuteNonQuery();

                        distillation.record_status = "Rejected By Bond Officer";
                    }
                    str = new StringBuilder();
                    str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                    str.Append("'" + distillation.distillation_id + "','','" + DateTime.Now.ToString() + "','F82D','" + distillation.record_status + "','" + distillation.remarks + "','" + DateTime.Now.ToString() + "','" + distillation.user_id + "','" + DateTime.Now.ToString() + "','" + distillation.user_id + "','"+distillation.financial_year+"','"+distillation.party_code+"')");
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



        public static List<DistillationToStore> GetToStoreList(string date,string vatcode,string party, string setupdate)
        {
            List<DistillationToStore> distillation = new List<DistillationToStore>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;

                    cmd = new NpgsqlCommand("SELECT a.*,b.vat_name,f.party_name,f.party_code FROM exciseautomation.distillation_tostore a  inner join exciseautomation.vat_master b on a.vat_code=b.vat_code inner join exciseautomation.distillation c on a.distillation_id=c.distillation_id inner join exciseautomation.rawmaterial_fermenter d on c.rawmaterial_fermenter_id=d.rawmaterial_fermenter_id inner join exciseautomation.party_master f on d.party_code=f.party_code where c.record_status='A' and a.moved_to_nextstage='N' and c.distillation_date='" + date+"' and d.tofermentervat='"+vatcode+"' and f.party_code='"+party+ "' and d.setup_date='"+setupdate+"'", cn);

                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        distillation = new List<DistillationToStore>();
                        while (dr.Read())
                        {
                            DistillationToStore record = new DistillationToStore();
                            record.distillation_id = Convert.ToInt32(dr["distillation_id"].ToString());
                            record.party_code = dr["party_code"].ToString();
                            record.vat_code= dr["vat_code"].ToString();
                            record.vat_name = dr["vat_name"].ToString();
                           record.party_name = dr["party_name"].ToString();
                            distillation.Add(record);
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return distillation;
        
    }



        public static List<Distillation> Getvat(string date,int setupid)
        {
            List<Distillation> distillation = new List<Distillation>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;

                    cmd = new NpgsqlCommand("select a.*,c.vat_name,c.vat_code,d.party_code,d.party_name from exciseautomation.distillation a inner join exciseautomation.rawmaterial_fermenter b on a.rawmaterial_fermenter_id= b.rawmaterial_fermenter_id inner join  exciseautomation.vat_master c on b.tofermentervat=c.vat_code inner join exciseautomation.party_master d on d.party_code=c.party_code where a.distillation_date='"+date+ "' and b.setup_complete='Y' and b.rawmaterial_fermenter_id='"+setupid+"'", cn);

                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        distillation = new List<Distillation>();
                        while (dr.Read())
                        {
                            Distillation record = new Distillation();
                            record.distillation_id = Convert.ToInt32(dr["distillation_id"].ToString());
                            record.party_code = dr["party_code"].ToString();
                            record.vat_code = dr["vat_code"].ToString();
                            record.vat_name = dr["vat_name"].ToString();
                            record.party_name = dr["party_name"].ToString();
                            distillation.Add(record);
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return distillation;

        }




        public static Distillation Getvatavailableqty(string vatcode, string Code)
        {
            Distillation vat = new Distillation();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select c.vat_availablecapacity from exciseautomation.distillation a inner join exciseautomation.rawmaterial_fermenter b on a.rawmaterial_fermenter_id= b.rawmaterial_fermenter_id inner join  exciseautomation.vat_master c on b.tofermentervat=c.vat_code inner join exciseautomation.party_master d on d.party_code=c.party_code where  b.tofermentervat='"+vatcode+"' and d.party_code='" + Code+"'", cn);
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



        public static DistillationToStore Getreciverbl(string vatcode, string date,int rfid)
        {
            DistillationToStore blqty = new DistillationToStore();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT a.bl_store FROM exciseautomation.distillation_tostore a inner join exciseautomation.distillation f on f.distillation_id=a.distillation_id  inner join exciseautomation.vat_master b on a.vat_code=b.vat_code inner join exciseautomation.party_master c on c.party_code=b.party_code where a.moved_to_nextstage='N' and f.record_status ='A' and f.distillation_date='" + date+"' and a.vat_code='"+vatcode+ "' and f.rawmaterial_fermenter_id='"+rfid+"'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            blqty.bl_store= Convert.ToDouble(dr["bl_store"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return blqty;
            }

        }


        public static List<Distillation> Getdate( string party_code)
        {
            List<Distillation> distillation = new List<Distillation>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;

                    cmd = new NpgsqlCommand("SELECT distinct(a.distillation_date)as distillation_date FROM exciseautomation.distillation a inner join exciseautomation.rawmaterial_fermenter c on a.rawmaterial_fermenter_id=c.rawmaterial_fermenter_id inner join exciseautomation.distillation_tostore f on a.distillation_id=f.distillation_id inner join   exciseautomation.party_master d on c.party_code=d.party_code where a.record_status='A' and f.moved_to_nextstage='N' and c.party_code='"+party_code+ "' and c.setup_complete='Y'", cn);

                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        distillation = new List<Distillation>();
                        while (dr.Read())
                        {
                            Distillation record = new Distillation();
                            //record.distillation_id = Convert.ToInt32(dr["distillation_id"].ToString());
                            record.distillation_date = dr["distillation_date"].ToString().Replace("/", "-").Substring(0, 10);
                            distillation.Add(record);
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return distillation;
        }


        public static List<Distillation> GetSubmiteddate(int distillationid)
        {
            List<Distillation> distillation = new List<Distillation>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;

                    cmd = new NpgsqlCommand("SELECT distillation_date,financial_year FROM exciseautomation.fermenter_receiver where fermenter_receiver_id='"+distillationid+"' ", cn);

                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        distillation = new List<Distillation>();
                        while (dr.Read())
                        {
                            Distillation record = new Distillation();
                            //record.distillation_id = Convert.ToInt32(dr["distillation_id"].ToString());
                            record.distillation_date = dr["distillation_date"].ToString().Replace("/", "-").Substring(0, 10);
                            record.financial_year = dr["financial_year"].ToString();
                            distillation.Add(record);
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return distillation;
        }
        public static List<Distillation> SetupGetList(string userid)
        {
            List<Distillation> distillation = new List<Distillation>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;

                    cmd = new NpgsqlCommand("select distinct(a.setup_date),a.rawmaterial_fermenter_id,d.distillation_date from exciseautomation.rawmaterial_fermenter a inner join exciseautomation.vat_master b on a.tofermentervat=b.vat_code inner join exciseautomation.party_master c on a.party_code=c.party_code inner join exciseautomation.distillation d on a.rawmaterial_fermenter_id=d.rawmaterial_fermenter_id inner join exciseautomation.distillation_tostore f on d.distillation_id=f.distillation_id where f.moved_to_nextstage='N' and a.party_code='" + userid+"' order by a.setup_date", cn);

                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        distillation = new List<Distillation>();
                        while (dr.Read())
                        {
                            Distillation record = new Distillation();
                            //record.distillation_id = Convert.ToInt32(dr["distillation_id"].ToString());
                            record.rawmaterial_fermenter_id = Convert.ToInt32(dr["rawmaterial_fermenter_id"].ToString());
                            record.setup_date = dr["setup_date"].ToString().Replace("/", "-").Substring(0, 10);
                            record.distillation_date = dr["distillation_date"].ToString().Replace("/", "-").Substring(0, 10);
                          //  record.party_code = dr["party_code"].ToString();
                          //  record.total_qty_transferred = Convert.ToDouble(dr["total_qty_transferred"].ToString());
                          //  record.vat_name = dr["vat_name"].ToString();
                           // record.record_status = dr["record_status"].ToString();
                           // record.party_name = dr["party_name"].ToString();
                            distillation.Add(record);
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return distillation;
        }


        public static List<Distillation> SubmitedSetupGetList(string userid)
        {
            List<Distillation> distillation = new List<Distillation>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;

                    cmd = new NpgsqlCommand("select distinct(a.setup_date),a.rawmaterial_fermenter_id,d.distillation_date from exciseautomation.rawmaterial_fermenter a inner join exciseautomation.vat_master b on a.tofermentervat=b.vat_code inner join exciseautomation.party_master c on a.party_code=c.party_code inner join exciseautomation.distillation d on a.rawmaterial_fermenter_id=d.rawmaterial_fermenter_id inner join exciseautomation.distillation_tostore f on d.distillation_id=f.distillation_id where f.moved_to_nextstage='Y' and a.party_code='" + userid + "'  order by a.setup_date ", cn);

                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        distillation = new List<Distillation>();
                        while (dr.Read())
                        {
                            Distillation record = new Distillation();
                         //   record.distillation_id = Convert.ToInt32(dr["distillation_id"].ToString());
                            record.rawmaterial_fermenter_id = Convert.ToInt32(dr["rawmaterial_fermenter_id"].ToString());
                            record.setup_date = dr["setup_date"].ToString().Replace("/", "-").Substring(0, 10);
                           record.distillation_date = dr["distillation_date"].ToString().Replace("/", "-").Substring(0, 10);
                          //  record.party_code = dr["party_code"].ToString();
                          //  record.total_qty_transferred = Convert.ToDouble(dr["total_qty_transferred"].ToString());
                            //record.vat_name = dr["vat_name"].ToString();
                           // record.record_status = dr["record_status"].ToString();
                           // record.party_name = dr["party_name"].ToString();
                            distillation.Add(record);
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return distillation;
        }

    }
}
