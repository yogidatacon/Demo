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
    public class DL_FermentertoReceiverForm_83
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static List<FermentertoReceiverForm_83> GetList()
        {
            List<FermentertoReceiverForm_83> from83 = new List<FermentertoReceiverForm_83>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                   // if (party_code == null || party_code == "All")
                        cmd = new NpgsqlCommand("select a.*,b.party_name,b.party_code from exciseautomation.fermenter_receiver a inner join exciseautomation.party_master b on a.party_code=b.party_code    order by a.gauged_date desc", cn);
                   // else
                     //   cmd = new NpgsqlCommand("select a.*,b.party_name,b.party_code from exciseautomation.fermenter_receiver a inner join exciseautomation.party_master b on a.party_code=b.party_code where a.party_code='" + party_code + "'  order by a.gauged_date", cn);

                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        from83 = new List<FermentertoReceiverForm_83>();
                        while (dr.Read())
                        {
                            FermentertoReceiverForm_83 record = new FermentertoReceiverForm_83();
                            record.gauged_date = dr["gauged_date"].ToString().Replace("/", "-").Substring(0, 10); ;
                            record.party_code = dr["party_code"].ToString();
                            record.financial_year = dr["financial_year"].ToString();
                            record.party_name = dr["party_name"].ToString();
                            record.distillation_date = dr["distillation_date"].ToString().Replace("/", "-").Substring(0, 10); ; 
                         record.distillation_id= dr["attribute1"].ToString();
                            record.party_code = dr["party_code"].ToString();
                            record.record_status = dr["record_status"].ToString();
                            record.fermenter_receiver_id = Convert.ToInt32(dr["fermenter_receiver_id"].ToString());
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
        public static List<FermentertoReceiverForm_83> Search(string tablename, string column, string value)
        {
            List<FermentertoReceiverForm_83> mir = new List<FermentertoReceiverForm_83>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name,b.party_code from exciseautomation.fermenter_receiver a inner join exciseautomation.party_master b on a.party_code=b.party_code where  " + column + " Ilike '%" + value + "%' and a.record_active='true'  order by a.gauged_date desc", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            mir = new List<FermentertoReceiverForm_83>();
                            while (dr.Read())
                            {
                                FermentertoReceiverForm_83 record = new FermentertoReceiverForm_83();
                                record.gauged_date = dr["gauged_date"].ToString().Replace("/", "-").Substring(0, 10); ;
                                record.party_code = dr["party_code"].ToString();
                                record.party_name = dr["party_name"].ToString();
                                record.financial_year= dr["financial_year"].ToString();
                                record.distillation_date = dr["distillation_date"].ToString().Replace("/", "-").Substring(0, 10); ;
                                record.distillation_id = dr["attribute1"].ToString();
                                record.party_code = dr["party_code"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.fermenter_receiver_id = Convert.ToInt32(dr["fermenter_receiver_id"].ToString());
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


        //public static List<FermentertoReceiverForm_83> GetReceiverVATList(string party_code)
        //{
        //    List<FermentertoReceiverForm_83> from83 = new List<FermentertoReceiverForm_83>();
        //    using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
        //    {
        //        cn.Open();
        //        try
        //        {
        //            NpgsqlCommand cmd;
        //            if (party_code == null || party_code == "All")
        //                cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name,d.vat_name as receivervat from exciseautomation.form83 a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.vat_code=c.vat_code left join exciseautomation.vat_master d on a.to_receivervat=d.vat_code  order by a.party_code,a.form83_id", cn);
        //            else
        //                cmd = new NpgsqlCommand("select a.*,b.party_name,c.vat_name,d.vat_name as receivervat from exciseautomation.form83 a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_master c on a.from_fermentervat=c.vat_code left join exciseautomation.vat_master d on a.to_receivervat=d.vat_code where a.party_code='" + party_code + "' order by a.form83_id", cn);

        //            cmd.CommandType = System.Data.CommandType.Text;
        //            NpgsqlDataReader dr = cmd.ExecuteReader();
        //            if (dr.HasRows)
        //            {
        //                from83 = new List<FermentertoReceiverForm_83>();
        //                while (dr.Read())
        //                {
        //                    FermentertoReceiverForm_83 record = new FermentertoReceiverForm_83();
        //                    record.gauged_date = dr["gauged_date"].ToString().Replace("/", "-").Substring(0, 10); ;
        //                    record.party_code = dr["party_code"].ToString();
        //                    record.party_name = dr["party_name"].ToString();
        //                    record.fermentervat = dr["vat_name"].ToString();
        //                    record.receivervat = dr["receivervat"].ToString();
        //                    record.from_fermentervat = dr["from_fermentervat"].ToString();
        //                    record.to_receivervat = dr["to_receivervat"].ToString();
        //                    record.transferqty = Convert.ToDouble(dr["transferqty"].ToString());
        //                    record.removal_date_distillation = dr["removal_date_distillation"].ToString().Replace("/", "-").Substring(0, 10); ;
        //                    record.record_status = dr["record_status"].ToString();
        //                    record.approval_status = dr["record_status"].ToString();
        //                    record.from83_id = Convert.ToInt32(dr["form83_id"].ToString());
        //                    from83.Add(record);
        //                }
        //                _log.Info("Get VAT Master List Success");
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            _log.Info("Get VAT Master List Fail :" + ex.Message);
        //        }
        //    }
        //    return from83;
        //}
        public static FermentertoReceiverForm_83 GetVatAval(string Vat_code, string party_Code ,string date,int id)
        {
            FermentertoReceiverForm_83 vat = new FermentertoReceiverForm_83();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select case when sum(b.bl_tostorage)  is null then 0 else sum(b.bl_tostorage) end as bl_tostorage1   from exciseautomation.fermenter_receiver a inner join exciseautomation.fermenter_receiver_output b on a.fermenter_receiver_id=b.fermenter_receiver_id inner join exciseautomation.fermenter_receiver_input c on a.fermenter_receiver_id=c.fermenter_receiver_id  where a.party_code='" + party_Code+ "' and a.record_status !='R' and a.distillation_date='" + date+ "'and c.receivervat='"+Vat_code+ "' and a.attribute1='"+id+"'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            vat.total_output_bl_qty = Convert.ToDouble( dr["bl_tostorage1"].ToString());
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

        public static string Approve(FermentertoReceiverForm_83 record)
        {
            List<FermentertoReceiverForm_83> from83 = new List<FermentertoReceiverForm_83>();
           // FermentertoReceiverForm_83 record = new FermentertoReceiverForm_83();
            string value;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    StringBuilder str = new StringBuilder();
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.fermenter_receiver SET  record_status='" + record.record_status + "' WHERE fermenter_receiver_id='" + record.fermenter_receiver_id + "' and financial_year='"+record.financial_year+"'", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (record.record_status == "R")
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("delete from exciseautomation.fermenter_receiver_input where  fermenter_receiver_id='" + record.fermenter_receiver_id + "' and financial_year='" + record.financial_year + "'", cn);
                        cmd1.ExecuteNonQuery();
                        for (int i = 0; i < record.ReceiverInput.Count; i++)
                        {
                            NpgsqlCommand cmd4 = new NpgsqlCommand("SELECT max(fermenter_receiver_input_id) FROM exciseautomation.fermenter_receiver_input where  financial_year='" + record.financial_year + "'", cn);
                            string f = cmd4.ExecuteScalar().ToString();
                            int c = 0;
                            if (f == "")
                                c = 1;
                            else
                                c = Convert.ToInt32(f) + 1;
                            n = 0;
                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.fermenter_receiver_input(fermenter_receiver_input_id, fermenter_receiver_id, fermentervat, receivervat, dips, temperature, indication, strength, bl_received, lp_received, lastmodified_date, user_id, creation_date, record_status,financial_year) ");
                            str.Append(" VALUES('" + c + "','" + record.fermenter_receiver_id + "', '" + record.ReceiverInput[i].fermentervat + "', '" + record.ReceiverInput[i].receivervat + "', '" + record.ReceiverInput[i].dips + "', '" + record.ReceiverInput[i].temperature + "', '" + record.ReceiverInput[i].indication + "', '" + record.ReceiverInput[i].strength + "', '" + record.ReceiverInput[i].bl_received + "', '" + record.ReceiverInput[i].lp_received + "', '" + DateTime.Now.ToShortDateString() + "', '" + record.user_id + "', '" + DateTime.Now.ToShortDateString() + "', '" + record.record_status + "','"+record.financial_year+"')");
                            NpgsqlCommand cmd5 = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd5.ExecuteNonQuery();

                         
                                NpgsqlCommand cmd7 = new NpgsqlCommand("SELECT a.distillation_id FROM exciseautomation.distillation a inner join exciseautomation.rawmaterial_fermenter c on a.rawmaterial_fermenter_id=c.rawmaterial_fermenter_id inner join exciseautomation.distillation_tostore f on a.distillation_id=f.distillation_id inner join   exciseautomation.party_master d on c.party_code=d.party_code where a.distillation_date='" + record.distillation_date + "' and c.tofermentervat='" + record.ReceiverInput[i].fermentervat + "' and a.financial_year='"+record.financial_year+"'", cn);
                                int g = Convert.ToInt32(cmd7.ExecuteScalar());

                                NpgsqlCommand cmd8 = new NpgsqlCommand("UPDATE exciseautomation.distillation_tostore SET moved_to_nextstage ='N' WHERE  distillation_id ='" + g + "' and vat_code='" + record.ReceiverInput[i].receivervat + "' and financial_year='"+record.financial_year+"'", cn);
                                cmd8.ExecuteNonQuery();
                          
                        }
                        record.record_status = "Rejected by Bond Officer";
                        //NpgsqlCommand cmd8 = new NpgsqlCommand("UPDATE exciseautomation.distillation_tostore SET moved_to_nextstage ='N' WHERE  distillation_id ='" + Distillationid + "'", cn);
                        //cmd8.ExecuteNonQuery();
                    }
                    else
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("delete from exciseautomation.fermenter_receiver_input where  fermenter_receiver_id='" + record.fermenter_receiver_id + "'  and financial_year='" + record.financial_year + "'", cn);
                        cmd1.ExecuteNonQuery();
                        for (int i = 0; i < record.ReceiverInput.Count; i++)
                        {
                            NpgsqlCommand cmd4 = new NpgsqlCommand("SELECT max(fermenter_receiver_input_id) FROM exciseautomation.fermenter_receiver_input ", cn);
                            string f = cmd4.ExecuteScalar().ToString();
                            int c = 0;
                            if (f == "")
                                c = 1;
                            else
                                c = Convert.ToInt32(f) + 1;
                            n = 0;
                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.fermenter_receiver_input(fermenter_receiver_input_id, fermenter_receiver_id, fermentervat, receivervat, dips, temperature, indication, strength, bl_received, lp_received, lastmodified_date, user_id, creation_date, record_status,financial_year) ");
                            str.Append(" VALUES('" + c + "','" + record.fermenter_receiver_id + "', '" + record.ReceiverInput[i].fermentervat + "', '" + record.ReceiverInput[i].receivervat + "', '" + record.ReceiverInput[i].dips + "', '" + record.ReceiverInput[i].temperature + "', '" + record.ReceiverInput[i].indication + "', '" + record.ReceiverInput[i].strength + "', '" + record.ReceiverInput[i].bl_received + "', '" + record.ReceiverInput[i].lp_received + "', '" + DateTime.Now.ToShortDateString() + "', '" + record.user_id + "', '" + DateTime.Now.ToShortDateString() + "', '" + record.record_status + "','"+record.financial_year+"')");
                            NpgsqlCommand cmd5 = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd5.ExecuteNonQuery();

                        
                                NpgsqlCommand cmd7 = new NpgsqlCommand("SELECT a.distillation_id FROM exciseautomation.distillation a inner join exciseautomation.rawmaterial_fermenter c on a.rawmaterial_fermenter_id=c.rawmaterial_fermenter_id inner join exciseautomation.distillation_tostore f on a.distillation_id=f.distillation_id inner join   exciseautomation.party_master d on c.party_code=d.party_code where a.distillation_date='" + record.distillation_date + "' and c.tofermentervat='" + record.ReceiverInput[i].fermentervat + "' and a.rawmaterial_fermenter_id='" + record.distillation_id + "' and a.financial_year='" + record.financial_year + "'", cn);
                                int g = Convert.ToInt32(cmd7.ExecuteScalar());
                            NpgsqlCommand cmd12 = new NpgsqlCommand("SELECT case when sum(f.bl_store) is null then 0 else sum(f.bl_store) end    FROM exciseautomation.distillation a inner join exciseautomation.rawmaterial_fermenter c on a.rawmaterial_fermenter_id=c.rawmaterial_fermenter_id inner join exciseautomation.distillation_tostore f on a.distillation_id=f.distillation_id inner join   exciseautomation.party_master d on c.party_code=d.party_code where a.distillation_date='" + record.distillation_date+"' and d.party_code='"+record.party_code+"' and f.vat_code='" + record.ReceiverInput[i].receivervat + "' and c.rawmaterial_fermenter_id='"+record.distillation_id+ "' and f.financial_year='" + record.financial_year + "'", cn);
                            double p = Convert.ToDouble(cmd12.ExecuteScalar());
                            NpgsqlCommand cmd11 = new NpgsqlCommand("select case when sum(b.bl_tostorage) is null then 0 else sum(b.bl_tostorage) end from exciseautomation.fermenter_receiver a inner join exciseautomation.fermenter_receiver_output b on a.fermenter_receiver_id=b.fermenter_receiver_id inner join exciseautomation.fermenter_receiver_input c on a.fermenter_receiver_id=c.fermenter_receiver_id  where a.party_code='" + record.party_code+"' and a.distillation_date='"+record.distillation_date+ "' and c.receivervat='" + record.ReceiverInput[i].receivervat + "' and a.attribute1='"+record.distillation_id+ "' and b.financial_year='" + record.financial_year + "'", cn);
                            double s = Convert.ToDouble(cmd11.ExecuteScalar());
                            if(s==p||s>p)
                            {
                                NpgsqlCommand cmd8 = new NpgsqlCommand("UPDATE exciseautomation.distillation_tostore SET moved_to_nextstage ='Y' WHERE  distillation_id ='" + g + "' and vat_code='" + record.ReceiverInput[i].receivervat + "' and financial_year='" + record.financial_year + "'", cn);
                                cmd8.ExecuteNonQuery();
                                NpgsqlCommand cmd9 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + record.ReceiverInput[i].receivervat + "' and party_code='" + record.party_code + "'", cn);
                                double k = Convert.ToDouble(cmd9.ExecuteScalar());
                                double v = Convert.ToDouble(k) - Convert.ToDouble(record.ReceiverInput[i].bl_received);
                                NpgsqlCommand cmd10 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + v + "'  where vat_code='" + record.ReceiverInput[i].receivervat + "' and party_code='" + record.party_code + "'", cn);
                                cmd10.ExecuteNonQuery();
                            }
                            else
                            {
                                NpgsqlCommand cmd9 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + record.ReceiverInput[i].receivervat + "' and party_code='" + record.party_code + "'", cn);
                                double k = Convert.ToDouble(cmd9.ExecuteScalar());
                                double v = Convert.ToDouble(k) - Convert.ToDouble(s);
                                NpgsqlCommand cmd10 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + v + "'  where vat_code='" + record.ReceiverInput[i].receivervat + "' and party_code='" + record.party_code + "'", cn);
                                cmd10.ExecuteNonQuery();
                            }



                        }

                        NpgsqlCommand cmd2 = new NpgsqlCommand("delete from exciseautomation.fermenter_receiver_output where  fermenter_receiver_id='" + record.fermenter_receiver_id + "' and financial_year='" + record.financial_year + "'", cn);
                        cmd2.ExecuteNonQuery();
                        for (int i = 0; i < record.ReceiverOutput.Count; i++)
                        {

                            n = 0;
                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.fermenter_receiver_output(fermenter_receiver_id, to_storagevat, removal_date, removal_hour, bl_tostorage, lp_tostorage, moved_to_nextstage, lastmodified_date, user_id, creation_date, record_status,financial_year)");
                            str.Append(" VALUES('" + record.fermenter_receiver_id + "', '" + record.ReceiverOutput[i].to_storagevat + "', '" + record.ReceiverOutput[i].removal_date + "', '" + record.ReceiverOutput[i].removal_hour + "', '" + record.ReceiverOutput[i].bl_tostorage + "', '" + record.ReceiverOutput[i].lp_tostorage + "', 'N', '" + DateTime.Now.ToShortDateString() + "', '" + record.user_id + "', '" + DateTime.Now.ToShortDateString() + "', '" + record.record_status + "','"+record.financial_year+"')");
                            NpgsqlCommand cmd6 = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd6.ExecuteNonQuery();
                          
                                //NpgsqlCommand cmd9 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + record.ReceiverOutput[i].to_storagevat + "' and party_code='" + record.party_code + "'", cn);
                                //double k = Convert.ToDouble(cmd9.ExecuteScalar());
                                //double v = Convert.ToDouble(k) + Convert.ToDouble(record.ReceiverOutput[i].bl_tostorage);
                                //NpgsqlCommand cmd10 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + v + "'  where vat_code='" + record.ReceiverOutput[i].to_storagevat + "' and party_code='" + record.party_code + "'", cn);
                                //cmd10.ExecuteNonQuery();
                           

                        }
                        record.record_status = "Approved by Bond Officer";
                    }
                    StringBuilder str1 = new StringBuilder();
                    str1.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                    str1.Append("'" + record.fermenter_receiver_id+ "','','" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "','FTR','" + record.record_status + "','" + record.remarks + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + record.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + record.user_id + "','"+record.financial_year+"','"+record.party_code+"')");
                    cmd = new NpgsqlCommand(str1.ToString(), cn);
                    n = cmd.ExecuteNonQuery();
                    value = "0";
                    trn.Commit();
                    _log.Info("Form83 Approve Sucess:" + record.fermenter_receiver_id);
                    cn.Close();
                }
                catch (Exception ex)
                {
                    value = ex.Message;
                    _log.Info("Form83 Approve Fail:" + record.fermenter_receiver_id);
                    trn.Rollback();
                }
                return value;

            }
        }

        public static FermentertoReceiverForm_83 GetDetails(int fermenterreceiverid, string party_code, string financial_year)
        {
            FermentertoReceiverForm_83 from83 = new FermentertoReceiverForm_83();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.fermenter_receiver where fermenter_receiver_id='" + fermenterreceiverid + "' and party_code='"+party_code+"' and financial_year='"+financial_year+"' ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr1 = cmd.ExecuteReader();
                        DataTable scp = new DataTable();
                        scp.Load(dr1);
                        dr1.Close();
                        foreach (DataRow dr in scp.Rows)
                        {
                            from83.fermenter_receiver_id = Convert.ToInt32(dr["fermenter_receiver_id"].ToString());
                            from83.gauged_date = Convert.ToDateTime(dr["gauged_date"]).ToString("dd-MM-yyyy");
                            from83.distillation_date = Convert.ToDateTime(dr["distillation_date"]).ToString("dd-MM-yyyy");
                            from83.total_input_bl_qty = Convert.ToDouble(dr["total_input_bl_qty"].ToString());
                            from83.total_input_lp_qty = Convert.ToDouble(dr["total_input_lp_qty"].ToString());
                            from83.total_output_bl_qty = Convert.ToDouble(dr["total_output_bl_qty"].ToString());
                            from83.total_output_lp_qty = Convert.ToDouble(dr["total_output_lp_qty"].ToString());
                            from83.redistillation_bl_qty = Convert.ToDouble(dr["redistillation_bl_qty"].ToString());
                            from83.redistillation_lp_qty = Convert.ToDouble(dr["redistillation_lp_qty"].ToString());
                            from83.distillation_id= dr["attribute1"].ToString();
                            from83.removal_date = dr["removal_date"].ToString();
                            from83.party_code = dr["party_code"].ToString();
                            from83.record_status= dr["record_status"].ToString();
                            from83.to_which_still = dr["to_which_still"].ToString();
                            from83.remarks = dr["remarks"].ToString();

                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT a.*,b.vat_name FROM exciseautomation.fermenter_receiver_input a inner  join  exciseautomation.vat_master b on a.receivervat=b.vat_code where fermenter_receiver_id= '" + fermenterreceiverid + "' and financial_year='" + financial_year + "'", cn))
                            {
                                cmd1.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr2 = cmd1.ExecuteReader();
                                from83.ReceiverInput = new List<FReceiverInput>();
                                if (dr2.HasRows)
                                {
                                    while (dr2.Read())
                                    {
                                        FReceiverInput Setup = new FReceiverInput();
                                        Setup.fermenter_receiver_input_id = Convert.ToInt32(dr2["fermenter_receiver_input_id"].ToString());
                                        Setup.fermentervat = dr2["fermentervat"].ToString();
                                        Setup.vat_name = dr2["vat_name"].ToString();
                                        Setup.receivervat = dr2["receivervat"].ToString();
                                        Setup.dips = Convert.ToDouble(dr2["dips"].ToString());
                                        Setup.temperature = Convert.ToDouble(dr2["temperature"].ToString());
                                        Setup.indication = Convert.ToDouble(dr2["indication"].ToString());
                                        Setup.strength = Convert.ToDouble(dr2["strength"].ToString());
                                        Setup.bl_received = Convert.ToDouble(dr2["bl_received"].ToString());
                                        Setup.lp_received = Convert.ToDouble(dr2["lp_received"].ToString());
                                        from83.ReceiverInput.Add(Setup);
                                    }
                                }
                                dr2.Close();
                            }

                            using (NpgsqlCommand cmd2 = new NpgsqlCommand("SELECT a.*,b.vat_name FROM exciseautomation.fermenter_receiver_output a inner  join  exciseautomation.vat_master b on a.to_storagevat=b.vat_code where fermenter_receiver_id= '" + fermenterreceiverid + "' and financial_year='" + financial_year + "'", cn))
                            {
                                cmd2.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr3 = cmd2.ExecuteReader();
                                from83.ReceiverOutput = new List<FReceiverOuput>();
                                if (dr3.HasRows)
                                {
                                    while (dr3.Read())
                                    {
                                        FReceiverOuput Setu = new FReceiverOuput();
                                        Setu.fermenter_receiver_output_id = Convert.ToInt32(dr3["fermenter_receiver_output_id"].ToString());
                                        Setu.to_storagevat = dr3["to_storagevat"].ToString();
                                        Setu.vat_name= dr3["vat_name"].ToString();
                                        Setu.removal_date=Convert.ToDateTime( dr3["removal_date"]).ToString("dd-MM-yyyy");
                                        Setu.removal_hour= dr3["removal_hour"].ToString();
                                        Setu.bl_tostorage = Convert.ToDouble(dr3["bl_tostorage"].ToString());
                                        Setu.lp_tostorage = Convert.ToDouble(dr3["lp_tostorage"].ToString());
                                        from83.ReceiverOutput.Add(Setu);
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
            return from83;
        }


        public static string Insert(FermentertoReceiverForm_83 from83)
        {
            string value = "";
           // where financial_year = '" + from83.financial_year + "'
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT max(fermenter_receiver_id) FROM exciseautomation.fermenter_receiver ", cn);
                string m = cmd1.ExecuteScalar().ToString();
                int n = 0;
                int a = 0;
                if (m == "")
                    n = 1;
                else
                    n = Convert.ToInt32(m) + 1;

                try
                {
                   
                    StringBuilder str = new StringBuilder();
                    if (from83.removal_date==null)
                    {
                        str.Append("INSERT INTO exciseautomation.fermenter_receiver(fermenter_receiver_id, gauged_date, distillation_date, party_code, total_input_bl_qty, total_input_lp_qty, total_output_bl_qty, total_output_lp_qty, to_which_still, removal_date, redistillation_bl_qty, redistillation_lp_qty, remarks, lastmodified_date, user_id, creation_date, record_status,attribute1,financial_year)");
                        str.Append("VALUES('" + n + "', '" + from83.gauged_date + "', '" + from83.distillation_date + "', '" + from83.party_code + "', '" + from83.total_input_bl_qty + "', '" + from83.total_input_lp_qty + "', '" + from83.total_output_bl_qty + "', '" + from83.total_output_lp_qty + "', '" + from83.to_which_still + "', null,'" + from83.redistillation_bl_qty + "', '" + from83.redistillation_lp_qty + "', '" + from83.remarks + "', '" + DateTime.Now.ToShortDateString() + "', '" + from83.user_id + "', '" + DateTime.Now.ToShortDateString() + "', '" + from83.record_status + "','" + from83.distillation_id + "','"+from83.financial_year+"')");
                    }
                    else
                    {
                        str.Append("INSERT INTO exciseautomation.fermenter_receiver(fermenter_receiver_id, gauged_date, distillation_date, party_code, total_input_bl_qty, total_input_lp_qty, total_output_bl_qty, total_output_lp_qty, to_which_still, removal_date, redistillation_bl_qty, redistillation_lp_qty, remarks, lastmodified_date, user_id, creation_date, record_status,attribute1,financial_year)");
                        str.Append("VALUES('" + n + "', '" + from83.gauged_date + "', '" + from83.distillation_date + "', '" + from83.party_code + "', '" + from83.total_input_bl_qty + "', '" + from83.total_input_lp_qty + "', '" + from83.total_output_bl_qty + "', '" + from83.total_output_lp_qty + "', '" + from83.to_which_still + "', '" + from83.removal_date + "','" + from83.redistillation_bl_qty + "', '" + from83.redistillation_lp_qty + "', '" + from83.remarks + "', '" + DateTime.Now.ToShortDateString() + "', '" + from83.user_id + "', '" + DateTime.Now.ToShortDateString() + "', '" + from83.record_status + "','" + from83.distillation_id + "','"+from83.financial_year+"')");
                    }
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                   a= cmd.ExecuteNonQuery();
                    if (a == 1)
                    {
                        NpgsqlCommand cmd3 = new NpgsqlCommand("SELECT max(fermenter_receiver_id) FROM exciseautomation.fermenter_receiver where  financial_year='" + from83.financial_year + "'", cn);
                       int b = Convert.ToInt32( cmd3.ExecuteScalar());
                      
                        for (int i = 0; i < from83.ReceiverInput.Count; i++)
                        {//where  financial_year='" + from83.financial_year + "'
                            NpgsqlCommand cmd4 = new NpgsqlCommand("SELECT max(fermenter_receiver_input_id) FROM exciseautomation.fermenter_receiver_input ", cn);
                           
                            string f = cmd4.ExecuteScalar().ToString();
                            int c = 0;
                            if (f == "")
                                c = 1;
                            else
                                c = Convert.ToInt32(f) + 1;

                            c += 1;
                            a = 0;
                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.fermenter_receiver_input(fermenter_receiver_input_id, fermenter_receiver_id, fermentervat, receivervat, dips, temperature, indication, strength, bl_received, lp_received, lastmodified_date, user_id, creation_date, record_status,financial_year) ");
                            str.Append(" VALUES('"+c+"','"+b+ "', '" + from83.ReceiverInput[i].fermentervat + "', '" + from83.ReceiverInput[i].receivervat+ "', '" + from83.ReceiverInput[i].dips + "', '" + from83.ReceiverInput[i].temperature + "', '" + from83.ReceiverInput[i].indication + "', '" + from83.ReceiverInput[i].strength+ "', '" + from83.ReceiverInput[i].bl_received + "', '" + from83.ReceiverInput[i].lp_received + "', '" + DateTime.Now.ToShortDateString() + "', '" + from83.user_id + "', '" + DateTime.Now.ToShortDateString() + "', '" + from83.record_status + "','"+from83.financial_year+"')");
                           
                            NpgsqlCommand cmd5 = new NpgsqlCommand(str.ToString(), cn);
                            a = cmd5.ExecuteNonQuery();
                            //if (from83.record_status == "Y")
                            //{
                            //    NpgsqlCommand cmd7 = new NpgsqlCommand("SELECT a.distillation_id FROM exciseautomation.distillation a inner join exciseautomation.rawmaterial_fermenter c on a.rawmaterial_fermenter_id=c.rawmaterial_fermenter_id inner join exciseautomation.distillation_tostore f on a.distillation_id=f.distillation_id inner join   exciseautomation.party_master d on c.party_code=d.party_code where a.distillation_date='" + from83.distillation_date+"' and c.tofermentervat='"+from83.ReceiverInput[i].fermentervat+"'", cn);
                            //    int g = Convert.ToInt32(cmd7.ExecuteScalar());

                            //    NpgsqlCommand cmd8 = new NpgsqlCommand("UPDATE exciseautomation.distillation_tostore SET moved_to_nextstage ='Y' WHERE  distillation_id ='" + g + "' and vat_code='" + from83.ReceiverInput[i].receivervat + "'", cn);
                            //    cmd8.ExecuteNonQuery();
                            //}
                        }
                        
                       
                      
                        for (int i = 0; i < from83.ReceiverOutput.Count; i++)
                        {
                            a = 0;
                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.fermenter_receiver_output(fermenter_receiver_id, to_storagevat, removal_date, removal_hour, bl_tostorage, lp_tostorage, moved_to_nextstage, lastmodified_date, user_id, creation_date, record_status,financial_year)");
                            str.Append(" VALUES('"+b+"', '"+from83.ReceiverOutput[i].to_storagevat+ "', '" + from83.ReceiverOutput[i].removal_date + "', '" + from83.ReceiverOutput[i].removal_hour + "', '" + from83.ReceiverOutput[i].bl_tostorage + "', '" + from83.ReceiverOutput[i].lp_tostorage + "', 'N', '" + DateTime.Now.ToShortDateString() + "', '"+from83.user_id+ "', '" + DateTime.Now.ToShortDateString() + "', '"+from83.record_status+"','"+from83.financial_year+"')");
                            NpgsqlCommand cmd6 = new NpgsqlCommand(str.ToString(), cn);
                            a = cmd6.ExecuteNonQuery();
                            //if (from83.record_status == "Y")
                            //{
                            //    NpgsqlCommand cmd9 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + from83.ReceiverOutput[i].to_storagevat + "' and party_code='" + from83.party_code + "'", cn);
                            //    double k = Convert.ToDouble(cmd9.ExecuteScalar());
                            //    double v = Convert.ToDouble(k) + Convert.ToDouble(from83.ReceiverOutput[i].bl_tostorage);
                            //    NpgsqlCommand cmd10 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + v + "'  where vat_code='" + from83.ReceiverOutput[i].to_storagevat + "' and party_code='" + from83.party_code + "'", cn);
                            //    cmd10.ExecuteNonQuery();
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

        //public static string Update(FermentertoReceiverForm_83 from83)
        //{
        //    string val = "0";
        //    using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
        //    {
        //        cn.Open();
        //        try
        //        {

        //            NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.fermenter_receiver SET  gauged_date ='"+from83.gauged_date+"', distillation_date ='"+from83.distillation_date+"', party_code ='"+from83.party_code+"', total_input_bl_qty ='"+from83.total_input_bl_qty+"', total_input_lp_qty ='"+from83.total_input_lp_qty+"', total_output_bl_qty ='"+from83.total_output_bl_qty+"', total_output_lp_qty ='"+from83.total_output_lp_qty+"', to_which_still ='"+from83.to_which_still+"', removal_date ='"+from83.removal_date+"', redistillation_bl_qty ='"+from83.redistillation_bl_qty+"', redistillation_lp_qty ='"+from83.redistillation_lp_qty+"', remarks ='"+from83.remarks+"', lastmodified_date ='"+DateTime.Now.ToShortDateString()+"' WHERE fermenter_receiver_id ='"+from83.fermenter_receiver_id+"' ", cn);
        //            cmd.CommandType = System.Data.CommandType.Text;
        //            cmd.ExecuteNonQuery();
        //            val = "0";
        //            //_log.Info("Get form83 Update Success");
        //        }
        //        catch (Exception ex)
        //        {
        //            val = ex.Message;
        //            //_log.Info("Get form83 update Fail :" + ex.Message);
        //        }
        //    }
        //    return val;

        //}

        public static string Update(FermentertoReceiverForm_83 from83)
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
                    if (from83.removal_date == null)
                    {
                        str.Append("UPDATE exciseautomation.fermenter_receiver SET  gauged_date ='" + from83.gauged_date + "', distillation_date ='" + from83.distillation_date + "', party_code ='" + from83.party_code + "', total_input_bl_qty ='" + from83.total_input_bl_qty + "', total_input_lp_qty ='" + from83.total_input_lp_qty + "', total_output_bl_qty ='" + from83.total_output_bl_qty + "', total_output_lp_qty ='" + from83.total_output_lp_qty + "', to_which_still ='" + from83.to_which_still + "', removal_date = null, redistillation_bl_qty ='" + from83.redistillation_bl_qty + "', redistillation_lp_qty ='" + from83.redistillation_lp_qty + "', remarks ='" + from83.remarks + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "', record_status='" + from83.record_status + "',attribute1='" + from83.distillation_id + "'  WHERE fermenter_receiver_id ='" + from83.fermenter_receiver_id + "' and financial_year='"+from83.financial_year+"'");
                    }
                    else
                    {
                        str.Append("UPDATE exciseautomation.fermenter_receiver SET  gauged_date ='" + from83.gauged_date + "', distillation_date ='" + from83.distillation_date + "', party_code ='" + from83.party_code + "', total_input_bl_qty ='" + from83.total_input_bl_qty + "', total_input_lp_qty ='" + from83.total_input_lp_qty + "', total_output_bl_qty ='" + from83.total_output_bl_qty + "', total_output_lp_qty ='" + from83.total_output_lp_qty + "', to_which_still ='" + from83.to_which_still + "', removal_date ='" + from83.removal_date + "', redistillation_bl_qty ='" + from83.redistillation_bl_qty + "', redistillation_lp_qty ='" + from83.redistillation_lp_qty + "', remarks ='" + from83.remarks + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "', record_status='" + from83.record_status + "' ,attribute1='" + from83.distillation_id + "'  WHERE fermenter_receiver_id ='" + from83.fermenter_receiver_id + "' and financial_year='" + from83.financial_year + "'");
                    }

                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("delete from exciseautomation.fermenter_receiver_input where  fermenter_receiver_id='" +from83.fermenter_receiver_id + "' and  financial_year='" + from83.financial_year + "'", cn);
                        cmd1.ExecuteNonQuery();
                        for (int i = 0; i < from83.ReceiverInput.Count; i++)
                        {//where  financial_year='" + from83.financial_year + "'
                            NpgsqlCommand cmd4 = new NpgsqlCommand("SELECT max(fermenter_receiver_input_id) FROM exciseautomation.fermenter_receiver_input ", cn);
                            string f = cmd4.ExecuteScalar().ToString();
                            int c = 0;
                            if (f == "")
                                c = 1;
                            else
                                c = Convert.ToInt32(f) + 1;
                            n = 0;
                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.fermenter_receiver_input(fermenter_receiver_input_id, fermenter_receiver_id, fermentervat, receivervat, dips, temperature, indication, strength, bl_received, lp_received, lastmodified_date, user_id, creation_date, record_status,financial_year) ");
                            str.Append(" VALUES('" + c + "','" + from83.fermenter_receiver_id + "', '" + from83.ReceiverInput[i].fermentervat + "', '" + from83.ReceiverInput[i].receivervat + "', '" + from83.ReceiverInput[i].dips + "', '" + from83.ReceiverInput[i].temperature + "', '" + from83.ReceiverInput[i].indication + "', '" + from83.ReceiverInput[i].strength + "', '" + from83.ReceiverInput[i].bl_received + "', '" + from83.ReceiverInput[i].lp_received + "', '" + DateTime.Now.ToShortDateString() + "', '" + from83.user_id + "', '" + DateTime.Now.ToShortDateString() + "', '" + from83.record_status + "','"+from83.financial_year+"')");
                            NpgsqlCommand cmd5 = new NpgsqlCommand(str.ToString(), cn);
                           n = cmd5.ExecuteNonQuery();

                            //if (from83.record_status == "Y")
                            //{
                            //    NpgsqlCommand cmd7 = new NpgsqlCommand("SELECT a.distillation_id FROM exciseautomation.distillation a inner join exciseautomation.rawmaterial_fermenter c on a.rawmaterial_fermenter_id=c.rawmaterial_fermenter_id inner join exciseautomation.distillation_tostore f on a.distillation_id=f.distillation_id inner join   exciseautomation.party_master d on c.party_code=d.party_code where a.distillation_date='" + from83.distillation_date + "' and c.tofermentervat='" + from83.ReceiverInput[i].fermentervat + "'", cn);
                            //    int g = Convert.ToInt32(cmd7.ExecuteScalar());

                            //    NpgsqlCommand cmd8 = new NpgsqlCommand("UPDATE exciseautomation.distillation_tostore SET moved_to_nextstage ='Y' WHERE  distillation_id ='" + g + "' and vat_code='" + from83.ReceiverInput[i].receivervat + "'", cn);
                            //    cmd8.ExecuteNonQuery();
                            //}
                        }
                       
                        NpgsqlCommand cmd2 = new NpgsqlCommand("delete from exciseautomation.fermenter_receiver_output where  fermenter_receiver_id='" + from83.fermenter_receiver_id + "' and financial_year='" + from83.financial_year + "'", cn);
                        cmd2.ExecuteNonQuery();
                        for (int i = 0; i < from83.ReceiverOutput.Count; i++)
                        {

                            n = 0;
                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.fermenter_receiver_output(fermenter_receiver_id, to_storagevat, removal_date, removal_hour, bl_tostorage, lp_tostorage, moved_to_nextstage, lastmodified_date, user_id, creation_date, record_status,financial_year)");
                            str.Append(" VALUES('" + from83.fermenter_receiver_id + "', '" + from83.ReceiverOutput[i].to_storagevat + "', '" + from83.ReceiverOutput[i].removal_date + "', '" + from83.ReceiverOutput[i].removal_hour + "', '" + from83.ReceiverOutput[i].bl_tostorage + "', '" + from83.ReceiverOutput[i].lp_tostorage + "', 'N', '" + DateTime.Now.ToShortDateString() + "', '" + from83.user_id + "', '" + DateTime.Now.ToShortDateString() + "', '" + from83.record_status + "','"+from83.financial_year+"')");
                            NpgsqlCommand cmd6 = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd6.ExecuteNonQuery();
                            //if (from83.record_status == "Y")
                            //{
                            //    NpgsqlCommand cmd9 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + from83.ReceiverOutput[i].to_storagevat + "' and party_code='" + from83.party_code + "'", cn);
                            //    double k = Convert.ToDouble(cmd9.ExecuteScalar());
                            //    double v = Convert.ToDouble(k) + Convert.ToDouble(from83.ReceiverOutput[i].bl_tostorage);
                            //    NpgsqlCommand cmd10 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + v + "'  where vat_code='" + from83.ReceiverOutput[i].to_storagevat + "' and party_code='" + from83.party_code + "'", cn);
                            //    cmd10.ExecuteNonQuery();
                            //}

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


        //public static string GetVatData(string vat_code,string party_code)
        //{
        //    string vat_data = "0";
        //    double vat_availablecapacity = 0;
        //    double from_fermentervat = 0;
        //    using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
        //    {
        //        cn.Open();
        //        try
        //        {

        //            NpgsqlCommand cmd = new NpgsqlCommand("select  vat_code,vat_availablecapacity  from exciseautomation.vat_master   where vat_code='" + vat_code + "' and party_code='" + party_code + "' ", cn);
        //            cmd.CommandType = System.Data.CommandType.Text;
        //            NpgsqlDataReader dr = cmd.ExecuteReader();
        //            if (dr.HasRows)
        //            {
        //                while (dr.Read())
        //                {
        //                    vat_availablecapacity = Convert.ToDouble(dr["vat_availablecapacity"].ToString());

        //                    //  vat_data = (to_receivervat - from_receivervat).ToString();
        //                }

        //            }
        //            dr.Close();
        //            cmd = new NpgsqlCommand("select  from_fermentervat,sum(transferqty) as transferqty from exciseautomation.form83   where from_fermentervat='" + vat_code + "' and party_code='" + party_code + "' and record_status='A' group by from_fermentervat", cn);
        //            cmd.CommandType = System.Data.CommandType.Text;
        //            dr = cmd.ExecuteReader();
        //            if (dr.HasRows)
        //            {
        //                while (dr.Read())
        //                {
        //                    from_fermentervat = Convert.ToDouble(dr["transferqty"].ToString());

        //                    //  
        //                }

        //            }
        //            dr.Close();
        //            vat_data = (vat_availablecapacity - from_fermentervat).ToString();
        //            _log.Info("Get GetVatData83 Success");
        //        }
        //        catch (Exception ex)
        //        {
        //            _log.Info("Get GetVatData83 Fail :" + ex.Message);
        //        }
        //    }
        //    return vat_data;
        //}
    }
}
