
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
    public class DL_Molasses_Issue_Register
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static List<Molasses_Issue_Register> GetOpeningBalanceList()
        {
           
            List<Molasses_Issue_Register> mir = new List<Molasses_Issue_Register>();

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.vat_code,a.vat_availablecapacity,a.uom_code,a.vat_name,a.party_code,case when b.openingbalancevalue is null then 0 else b.openingbalancevalue end as openingbalancevalue from exciseautomation.vat_master a left join exciseautomation.openingbalance b on a.vat_code=b.vat_code  order by a.party_code,a.vat_code", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dr.Close();
                    mir = new List<Molasses_Issue_Register>();
                    foreach (DataRow row in dt.Rows)
                    {
                        Molasses_Issue_Register record = new Molasses_Issue_Register();
                        record.vat_code = row["vat_code"].ToString();
                        record.party_code = row["party_code"].ToString();
                        record.openingbalance = Convert.ToDouble(row["openingbalancevalue"].ToString());
                        record.uom = row["uom_code"].ToString();
                        NpgsqlCommand cmd12 = new NpgsqlCommand("select case when sum(issuedqty) is null then 0 else sum(issuedqty)  end as issuedqty  from exciseautomation.molassesissueregister where vat_code='" + record.vat_code + "' and party_code='" + record.party_code + "' and record_status='A' ", cn);
                        dr = cmd12.ExecuteReader();
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                record.issuedqty = Convert.ToDouble(dr["issuedqty"].ToString());
                            }
                        }
                        dr.Close();
                        NpgsqlCommand cmd1 = new NpgsqlCommand("select case when sum(dailyproduction) is null then 0 else sum(dailyproduction)  end as dailyproduction  from exciseautomation.dailymolassesproduction where vat_code='" + record.vat_code + "' and party_code='" + record.party_code + "' and record_status='A' ", cn);
                        dr = cmd1.ExecuteReader();
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                record.production = Convert.ToDouble(dr["dailyproduction"].ToString());
                            }
                        }
                        dr.Close();
                        NpgsqlCommand cmd11 = new NpgsqlCommand("select  closing_dip  from exciseautomation.molassesissueregister where vat_code='" + record.vat_code+ "' and party_code='"+record.party_code+"' and record_status='A' order by molassesissueregister_id desc limit 1 ", cn);
                        dr = cmd11.ExecuteReader();
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                if(dr["closing_dip"].ToString()!="")
                                record.closing_dips = Convert.ToDouble(dr["closing_dip"].ToString());
                            }
                        }
                        else
                        {
                            record.closing_dips = 0;
                        }
                        dr.Close();
                        mir.Add(record);
                    }
                    
                  
                   
                }
                catch (Exception ex)
                {

                }
                return mir;
            }
        }
        public static List<Molasses_Issue_Register> GetOpeningBalanceListMIR( string Vat, string financial_year)
        {

            List<Molasses_Issue_Register> mir = new List<Molasses_Issue_Register>();

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.vat_code,a.vat_availablecapacity,a.uom_code,a.vat_name,a.party_code,case when b.openingbalancevalue is null then 0 else b.openingbalancevalue end as openingbalancevalue from exciseautomation.vat_master a left join exciseautomation.openingbalance b on a.vat_code=b.vat_code where a.vat_code='"+Vat+"' and b.financial_year='"+financial_year+"'  order by a.party_code,a.vat_code", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dr.Close();
                    mir = new List<Molasses_Issue_Register>();
                    foreach (DataRow row in dt.Rows)
                    {
                        Molasses_Issue_Register record = new Molasses_Issue_Register();
                        record.vat_code = row["vat_code"].ToString();
                        record.party_code = row["party_code"].ToString();
                        record.openingbalance = Convert.ToDouble(row["openingbalancevalue"].ToString());
                        record.uom = row["uom_code"].ToString();
                        NpgsqlCommand cmd12 = new NpgsqlCommand("select case when sum(issuedqty) is null then 0 else sum(issuedqty)  end as issuedqty  from exciseautomation.molassesissueregister where vat_code='" + record.vat_code + "' and party_code='" + record.party_code + "' and financial_year='" + financial_year + "' and record_status='A' ", cn);
                        dr = cmd12.ExecuteReader();
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                record.issuedqty = Convert.ToDouble(dr["issuedqty"].ToString());
                            }
                        }
                        dr.Close();
                        NpgsqlCommand cmd1 = new NpgsqlCommand("select case when sum(dailyproduction) is null then 0 else sum(dailyproduction)  end as dailyproduction  from exciseautomation.dailymolassesproduction where vat_code='" + record.vat_code + "' and party_code='" + record.party_code + "' and financial_year='" + financial_year + "' and record_status='A' ", cn);
                        dr = cmd1.ExecuteReader();
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                record.production = Convert.ToDouble(dr["dailyproduction"].ToString());
                            }
                        }
                        dr.Close();
                        NpgsqlCommand cmd11 = new NpgsqlCommand("select  closing_dip  from exciseautomation.molassesissueregister where vat_code='" + record.vat_code + "' and party_code='" + record.party_code + "' and record_status='A' order by molassesissueregister_id desc limit 1 ", cn);
                        dr = cmd11.ExecuteReader();
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                if (dr["closing_dip"].ToString() != "")
                                    record.closing_dips = Convert.ToDouble(dr["closing_dip"].ToString());
                            }
                        }
                        else
                        {
                            record.closing_dips = 0;
                        }
                        dr.Close();
                        mir.Add(record);
                    }



                }
                catch (Exception ex)
                {

                }
                return mir;
            }
        }

        public static string GetExistingDetails(string values)
        {
            string value = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    string[] val = values.Split('_');
                    NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.molassesissueregister where party_code='" + val[0] + "' and vat_code='"+val[1]+ "' and mir_entrydate='"+val[2]+"'", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            value = "DataExist";
                        }
                        _log.Info("MIR GetDetails Sucess");
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("MIR GetDetails Fail :" + ex.Message);
                }
                return value;
            }
        }

        //public static List<Molasses_Issue_Register> GetPassDetails(string party_code)
        //{
        //    List < Molasses_Issue_Register> mir = new List<Molasses_Issue_Register>();

        //    using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
        //    {
        //        cn.Open();
        //        try
        //        {
        //            NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.dispatch_type_name,c.party_name from exciseautomation.pass a inner join exciseautomation.dispatch_type_master b on a.dispatch_type_id=b.dispatch_type_id inner join exciseautomation.dispatch_type_master b  where a.record_status='I' and a.supplier_unit='" + party_code+"' and rem_pass_qty<>0 order by pass_id  ", cn);
        //            NpgsqlDataReader dr = cmd.ExecuteReader();
        //            if (dr.HasRows)
        //            {
        //                mir = new List<Molasses_Issue_Register>();
        //                while (dr.Read())
        //                {
        //                    Molasses_Issue_Register record = new Molasses_Issue_Register();
        //                    record.pa = dr["pass_id"].ToString();
        //                    record.passno = dr["pass_id"].ToString();
                           
        //                    record.passdate = dr["dispatch_date"].ToString().Substring(0,10).Replace("/","-");
        //                    record.issuetype = dr["dispatch_type_id"].ToString();
        //                    record.dispatch_type_name = dr["dispatch_type_name"].ToString();
        //                    record.digilockno = dr["digital_lock_no"].ToString();
        //                    record.vehicle_no = dr["vehicle_no"].ToString();
                          
        //                    record.party_code = dr["party_code"].ToString();
        //                    record.issuedqty = Convert.ToDouble(dr["dispatch_qty"].ToString());
        //                    if (dr["rem_pass_qty"].ToString() == "")
        //                        record.rem_pass_qty = record.issuedqty;
        //                    else
        //                    record.rem_pass_qty =Convert.ToDouble(dr["rem_pass_qty"].ToString());
        //                    mir.Add(record);
                           
        //                }
        //                _log.Info("MIR GetPassDetails Sucess");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            _log.Info("MIR GetPassDetails Fail :" + ex.Message);
        //        }
        //        return mir;
        //    }
        //}

        public static string Approve(Molasses_Issue_Register mir)
        {
            string value;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.molassesissueregister SET  record_status='" + mir.record_status + "' WHERE molassesissueregister_id='" + mir.molassesissueregister_id + "'  and financial_year='" + mir.financial_year + "'", cn);
                    int n = cmd.ExecuteNonQuery();
                    string recordstatus;
                    if (mir.record_status == "R")
                    {
                        recordstatus = "Rejected by Bond Officer";
                    }
                    else
                    {
                        recordstatus = "Approved by Bond Officer";

                        if (mir.record_status == "A")
                        {
                            cmd = new NpgsqlCommand("UPDATE exciseautomation.pass SET  rem_pass_qty='" + mir.rem_pass_qty + "',record_status='M' WHERE pass_id='" + mir.passno + "'  and financial_year='" + mir.financial_year + "'", cn);
                            cmd.ExecuteNonQuery();
                            cmd = new NpgsqlCommand("select case when vat_availablecapacity is null then 0 else vat_availablecapacity end as vat_availablecapacity from exciseautomation.vat_master where vat_code='" + mir.vat_code + "' and party_code='" + mir.party_code + "'", cn);
                            double available = Convert.ToDouble(cmd.ExecuteScalar());
                            available = available - mir.issuedqty;
                            if(available<0)
                            {
                                available = 0;
                            }
                            cmd = new NpgsqlCommand("UPDATE exciseautomation.vat_master SET  vat_availablecapacity='" + available + "' WHERE vat_code='" + mir.vat_code + "' and party_code='" + mir.party_code + "'", cn);
                            int G = cmd.ExecuteNonQuery();
                        }
                        if (mir.record_status == "R")
                        {
                            cmd = new NpgsqlCommand("UPDATE exciseautomation.pass SET  rem_pass_qty='0' WHERE pass_id='" + mir.passno + "'  and financial_year='" + mir.financial_year + "'", cn);
                            cmd.ExecuteNonQuery();
                        }
                       cmd = new NpgsqlCommand("select pass_type from exciseautomation.pass WHERE pass_id='" + mir.passno + "'  and financial_year='" + mir.financial_year + "'", cn);
                       string pass_type= cmd.ExecuteScalar().ToString();
                        if (pass_type == "RR")
                        {
                            cmd = new NpgsqlCommand("Select count(1) from exciseautomation.rawmaterial_receipt where passno='" + mir.passno + "' and party_code='" + mir.party_code + "'  and financial_year='" + mir.financial_year + "'", cn);
                            string cnt = cmd.ExecuteScalar().ToString();
                            if (cnt == "" || cnt == "0")
                            {
                                cmd = new NpgsqlCommand("Select Max(rawmaterial_receipt_id) from exciseautomation.rawmaterial_receipt where financial_year='" + mir.financial_year + "' ", cn);
                                string val = cmd.ExecuteScalar().ToString();
                                int rmrid;
                                if (val == "" || val == "0")
                                {
                                    rmrid = 1;
                                }
                                else
                                    rmrid = Convert.ToInt32(val) + 1;
                                StringBuilder str1 = new StringBuilder();
                                str1.Append("INSERT INTO exciseautomation.rawmaterial_receipt(rawmaterial_receipt_id, supplier_party_code,party_code , passno, passissuedate, passqty,vehicleno,grossweight,financial_year)values(");
                                str1.Append("'" + rmrid + "','" + mir.party_code + "','" + mir.to_party_code + "','" + mir.passno + "','" + mir.passdate + "','" + mir.issuedqty + "','" + mir.vehicle_no + "','"+mir.grossweight+"','"+mir.financial_year+"')");
                                cmd = new NpgsqlCommand(str1.ToString(), cn);
                                int g = cmd.ExecuteNonQuery();
                                StringBuilder str2 = new StringBuilder();
                                str2.Append("INSERT INTO exciseautomation.transactionhistory(transaction_id, transaction_date, transaction_type, transaction_format, party_code, from_vat,  minus_qty, user_id, creation_date,financial_year)values(");
                                str2.Append("'" + rmrid + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','MIR','" + rmrid + "','" + mir.party_code + "','" + mir.vat_code + "','" + mir.issuedqty + "','" + mir.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+mir.financial_year+"')");
                                cmd = new NpgsqlCommand(str2.ToString(), cn);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    StringBuilder str = new StringBuilder();
                    str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year) VALUES(");
                    str.Append("'" + mir.molassesissueregister_id + "','" + mir.molassesissueregister_id + "','" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "','MIR','" + recordstatus + "','" + mir.remarks + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + mir.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + mir.user_id + "','"+mir.financial_year+"')");
                    cmd = new NpgsqlCommand(str.ToString(), cn);
                    n = cmd.ExecuteNonQuery();
                    value = "0";
                    trn.Commit();
                    _log.Info("MIR Approve Sucess:" + mir.molassesissueregister_id);
                    cn.Close();
                }
                catch (Exception ex)
                {
                    value = ex.Message;
                    _log.Info("MIR Approve Fail:" + mir.molassesissueregister_id);
                    trn.Rollback();
                }
                return value;

            }
        }

        public static List<Molasses_Issue_Register> GetList()
        {
            List<Molasses_Issue_Register> mir = new List<Molasses_Issue_Register>();

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select distinct(a.financial_year),a.*,b.uom_name,c.party_name,d.vat_name,e.cust_name,f.pass_issueno,g.pass from exciseautomation.molassesissueregister a left join exciseautomation.document_format_master g on a.party_code=g.party_code left join exciseautomation.pass f on a.passno=f.pass_id and a.financial_year=f.financial_year inner join exciseautomation.uom_master b on a.uom_code=b.uom_code left join exciseautomation.party_master c on a.to_party_code=c.party_code inner join exciseautomation.vat_master d on a.vat_code=d.vat_code left join exciseautomation.customer_master e on e.customer_id=a.to_party_code where a.record_active='true' order by mir_entrydate,molassesissueregister_id", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        mir = new List<Molasses_Issue_Register>();
                        while (dr.Read())
                        {
                            Molasses_Issue_Register record = new Molasses_Issue_Register();
                            record.vat_code = dr["vat_code"].ToString();
                            
                            record.party_code = dr["party_code"].ToString();
                            record.financial_year = dr["financial_year"].ToString();
                            record.to_party_code = dr["to_party_code"].ToString();
                            record.party_name = dr["party_name"].ToString();
                            if(record.party_name=="")
                            {
                                record.party_name = dr["cust_name"].ToString();
                            }
                            record.vat_name = dr["vat_name"].ToString();
                            record.passno = dr["passno"].ToString();
                            record.pass_issuedno = dr["pass"].ToString() + dr["financial_year"].ToString() + "/" + dr["pass_issueno"].ToString();
                            record.issuedqty = Convert.ToDouble(dr["issuedqty"].ToString());
                            record.record_status = dr["record_status"].ToString();
                          
                            record.mir_entrydate = dr["mir_entrydate"].ToString().Substring(0,10).Replace("/","-");
                            record.uom = dr["uom_name"].ToString();
                            record.molassesissueregister_id = Convert.ToInt32(dr["molassesissueregister_id"].ToString());
                            mir.Add(record);
                        }
                    }
                    
                    _log.Info("MIR GetList Sucess");
                }
                catch (Exception ex)
                {
                    _log.Info("MIR GetList Fail"+ex.Message);
                }
                return mir;
            }
        }
        public static List<Molasses_Issue_Register> Search(string tablename, string column, string value)
        {
            List<Molasses_Issue_Register> mir = new List<Molasses_Issue_Register>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select distinct(a.financial_year),a.*,b.uom_name,c.party_name,d.vat_name,e.cust_name,f.pass_issueno,g.pass from exciseautomation.molassesissueregister a left join exciseautomation.document_format_master g on a.party_code=g.party_code left join exciseautomation.pass f on a.passno=f.pass_id and a.financial_year=f.financial_year inner join exciseautomation.uom_master b on a.uom_code=b.uom_code left join exciseautomation.party_master c on a.to_party_code=c.party_code inner join exciseautomation.vat_master d on a.vat_code=d.vat_code left join exciseautomation.customer_master e on e.customer_id=a.to_party_code where " + column + " Ilike '%" + value + "%' and a.record_active='true'  order by  mir_entrydate,molassesissueregister_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            mir = new List<Molasses_Issue_Register>();
                            while (dr.Read())
                            {
                                Molasses_Issue_Register record = new Molasses_Issue_Register();
                                record.vat_code = dr["vat_code"].ToString();

                                record.party_code = dr["party_code"].ToString();
                                record.financial_year = dr["financialyear"].ToString();
                                record.to_party_code = dr["to_party_code"].ToString();
                                record.party_name = dr["party_name"].ToString();
                                if (record.party_name == "")
                                {
                                    record.party_name = dr["cust_name"].ToString();
                                }
                                record.vat_name = dr["vat_name"].ToString();
                                record.passno = dr["passno"].ToString();
                                record.pass_issuedno = dr["pass"].ToString() + dr["financial_year"].ToString() + "/" + dr["pass_issueno"].ToString();
                                record.issuedqty = Convert.ToDouble(dr["issuedqty"].ToString());
                                record.record_status = dr["record_status"].ToString();
                                record.mir_entrydate = dr["mir_entrydate"].ToString().Substring(0, 10).Replace("/", "-");
                                record.uom = dr["uom_name"].ToString();
                                record.molassesissueregister_id = Convert.ToInt32(dr["molassesissueregister_id"].ToString());
                                mir.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return mir;
        }


        public static Molasses_Issue_Register GetDetails(string mirid,string financial_year)
        {
            Molasses_Issue_Register mir = new Molasses_Issue_Register();

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.dispatch_type_name,e.cust_name,c.party_name from exciseautomation.molassesissueregister a left join exciseautomation.dispatch_type_master b  on a.issuetype=b.dispatch_type_id left join exciseautomation.party_master c on c.party_code=a.to_party_code left join exciseautomation.customer_master e on e.customer_id=a.to_party_code where molassesissueregister_id='" + mirid+"' and a.financial_year='"+financial_year+"'", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                       
                        while (dr.Read())
                        {
                            mir.vat_code = dr["vat_code"].ToString();
                            mir.party_code = dr["party_code"].ToString();
                            mir.mir_entrydate = dr["mir_entrydate"].ToString().Substring(0, 10);
                            mir.issuedqty = Convert.ToDouble(dr["issuedqty"].ToString());
                            mir.uom = dr["uom_code"].ToString();
                            mir.passno = dr["passno"].ToString();
                            mir.passdate = dr["passdate"].ToString();
                            mir.issuetype = dr["issuetype"].ToString();
                            mir.dispatch_type_name = dr["dispatch_type_name"].ToString();
                            mir.digilockno = dr["digilockno"].ToString();
                            mir.issuedqty =Convert.ToDouble(dr["issuedqty"].ToString());
                            mir.valuers= Convert.ToDouble(dr["valuers"].ToString());
                            mir.basicrs = Convert.ToDouble(dr["basicrs"].ToString());
                            mir.basicrs = Convert.ToDouble(dr["basicrs"].ToString());
                            mir.splrs = Convert.ToDouble(dr["splrs"].ToString());
                            mir.destroyedqty = Convert.ToDouble(dr["destroyedqty"].ToString());
                            mir.remarks = dr["remarks"].ToString();
                            mir.record_status = dr["record_status"].ToString();
                            mir.closing_dips =Convert.ToDouble( dr["closing_dip"].ToString());
                            mir.molassesissueregister_id = Convert.ToInt32(dr["molassesissueregister_id"].ToString());
                            mir.to_party_code = dr["to_party_code"].ToString();
                            mir.party_name= dr["party_name"].ToString();
                            if (mir.party_name=="")
                            {
                                mir.to_party_code = dr["customer_id"].ToString();
                                mir.party_name = dr["cust_name"].ToString();
                            }
                        }
                        _log.Info("MIR GetDetails Sucess");
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("MIR GetDetails Fail :"+ex.Message);
                }
                return mir;
            }
        }

        public static string Insert(Molasses_Issue_Register mir)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("Select Max(molassesissueregister_id) from exciseautomation.molassesissueregister where financial_year='"+mir.financial_year+"'", cn);
                    string val = cmd.ExecuteScalar().ToString();
                    //int max;
                    if (val == "" || val == "0")
                    {
                       mir.molassesissueregister_id = 1;
                    }
                    else
                        mir.molassesissueregister_id = Convert.ToInt32(val) + 1;
                    StringBuilder str = new StringBuilder();
                    cmd = new NpgsqlCommand("INSERT INTO exciseautomation.molassesissueregister(molassesissueregister_id, party_code, vat_code, financial_year, mir_entrydate, passno, passdate, issuetype, digilockno, issuedqty, valuers, basicrs, splrs, destroyedqty, remarks, record_id_format,  user_id, creation_date, record_status,uom_code,closing_dip,to_party_code) VALUES('" + mir.molassesissueregister_id+"','"+mir.party_code+"','"+mir.vat_code+"','"+mir.financial_year+"','"+mir.mir_entrydate+"','"+mir.passno+"','"+mir.passdate+"','"+mir.issuetype+"','"+mir.digilockno+"','"+mir.issuedqty+"','"+mir.valuers+"','"+mir.basicrs+"','"+mir.splrs+"','"+mir.destroyedqty+"','"+mir.remarks+"','"+mir.record_id_format+"','"+mir.user_id+"','"+DateTime.Now.ToString("dd-MM-yyyy")+"','"+mir.record_status+"','"+mir.uom+"','"+mir.closing_dips+"','"+mir.to_party_code+"')",cn);
                    
                    int n = cmd.ExecuteNonQuery();
                    cmd = new NpgsqlCommand("UPDATE exciseautomation.pass SET  rem_pass_qty='" + mir.rem_pass_qty + "',record_status='P' WHERE pass_id='" + mir.passno + "' and financial_year='"+mir.financial_year+"'", cn);
                    cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        VAL = "0";
                    }
                    else
                        VAL = "1";
                    cn.Close();
                    _log.Info("MIR Insertion Sucess:" +mir.molassesissueregister_id + '-' + mir.party_code);

                }
                catch (Exception ex1)
                {
                    _log.Info("MIR Insertion Fail:" + mir.molassesissueregister_id + '-' + mir.party_code + "-" + ex1.Message);

                    VAL = ex1.Message;
                }

            }
            return VAL;
        }

        public static string Update(Molasses_Issue_Register mir)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("Update exciseautomation.molassesissueregister set  vat_code='"+mir.vat_code+"',  mir_entrydate='"+mir.mir_entrydate+"', passno='"+mir.passno+"', passdate='"+mir.passdate+"', issuetype='"+mir.issuetype+"', digilockno='"+mir.digilockno+"', issuedqty='"+mir.issuedqty+"', valuers='"+mir.valuers+"', basicrs='"+mir.basicrs+"', splrs='"+mir.splrs+"', destroyedqty='"+mir.destroyedqty+"', remarks='"+mir.remarks+"', record_id_format='"+mir.record_id_format+"',  user_id='"+mir.user_id+"',  record_status='"+mir.record_status+"',uom_code='"+mir.uom+ "',closing_dip='" + mir.closing_dips + "' where molassesissueregister_id='" + mir.molassesissueregister_id + "'  and financial_year='" + mir.financial_year + "' ");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    cmd = new NpgsqlCommand("UPDATE exciseautomation.pass SET  rem_pass_qty='" + mir.issuedqty + "' WHERE pass_id='" + mir.passno + "'  and financial_year='" + mir.financial_year + "'", cn);
                    cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        VAL = "0";
                    }
                    else
                        VAL = "1";
                    cn.Close();
                    _log.Info("MIR Update Success:" +mir.party_code + '-' + mir.vat_code+"_"+mir.molassesissueregister_id);
                }
                catch (Exception ex1)
                {
                    _log.Info("MIR Update Fail :" +mir.party_code + '-' + mir.vat_code + "_" + mir.molassesissueregister_id + "-" + ex1.Message);
                    VAL = ex1.Message;
                }
            }
            return VAL;
        }
    }
}
