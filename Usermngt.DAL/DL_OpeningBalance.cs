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
  public  class DL_OpeningBalance
    {
        public static List<OpeningBalance> GetOpeningBalanceList(string party_code,string cdate)
        {
            List<OpeningBalance> openingbalances = new List<OpeningBalance>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    if (party_code == "ALL")
                    {

                        cmd = new NpgsqlCommand("select distinct a.*,case when round(b.openingbalancevalue::numeric,2) is null then 0 else round(b.openingbalancevalue::numeric,2) end as openingbalancevalue ,b.openingbalance_id,b.record_status as status,b.remarks,b.openingbalanceyear,b.financial_year from  exciseautomation.view_opening_balance a left join exciseautomation.openingbalance b  on a.vat_code=b.vat_code and a.party_code=b.party_code  inner join exciseautomation.view_checkuser c on b.user_id=c.user_id   order by a.vat_code", cn);
                    }
                    else
                    {
                        cmd = new NpgsqlCommand("select count(1) from exciseautomation.openingbalance where party_code='" + party_code + "'", cn);
                        string re = cmd.ExecuteScalar().ToString();
                        if (re != "0")
                        {
                            cmd = new NpgsqlCommand("select distinct a.*,case when round(b.openingbalancevalue::numeric,2) is null then 0 else round(b.openingbalancevalue::numeric,2) end as openingbalancevalue,b.openingbalance_id,b.record_status as status,b.remarks,b.openingbalanceyear,b.financial_year from  exciseautomation.view_opening_balance a left join exciseautomation.openingbalance b  on  a.vat_code=b.vat_code and a.party_code=b.party_code  left join exciseautomation.view_checkuser c on b.user_id=c.user_id where a.party_code='" + party_code + "'   order by a.vat_code", cn);
                        }
                        else
                        {
                            cmd = new NpgsqlCommand("select distinct a.*,case when round(b.openingbalancevalue::numeric,2) is null then 0 else round(b.openingbalancevalue::numeric,2) end as openingbalancevalue,b.openingbalance_id,b.record_status as status,b.remarks,b.openingbalanceyear,b.financial_year from  exciseautomation.view_opening_balance a left join exciseautomation.openingbalance b  on  a.vat_code=b.vat_code and a.party_code=b.party_code  left join exciseautomation.view_checkuser c on b.user_id=c.user_id where a.party_code='" + party_code + "'   order by a.vat_code", cn);
                        }
                    }
                      
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                  
                    if(dr.HasRows)
                    {
                        openingbalances = new List<OpeningBalance>();
                        while(dr.Read())
                        {
                            OpeningBalance openingbalance = new OpeningBalance();
                            //if (dr["status"].ToString() != "R")
                            //{
                                openingbalance.vat_code = dr["vat_code"].ToString();
                                openingbalance.vat_name = dr["vat_name"].ToString();
                                openingbalance.storage_content = dr["storage_content"].ToString();
                            openingbalance.openingbalanceyear = dr["openingbalanceyear"].ToString();
                            openingbalance.vat_type_code = dr["vat_type_code"].ToString();
                                openingbalance.vat_type_name = dr["vat_type_name"].ToString();
                                openingbalance.uom_code = dr["uom_code"].ToString();
                                openingbalance.uom_name = dr["uom_name"].ToString();
                            openingbalance.Total_Capacity = Convert.ToDouble(dr["vat_totalcapacity"].ToString());
                            openingbalance.vat_availablecapacity = Convert.ToDouble(dr["vat_availablecapacity"].ToString());
                                openingbalance.party_code = dr["Party_code"].ToString();
                                openingbalance.party_name = dr["Party_name"].ToString();
                           if( dr["financial_year"].ToString() !="")
                            openingbalance.financial_year= dr["financial_year"].ToString();
                            openingbalance.remarks = dr["remarks"].ToString();
                            if (dr["status"].ToString() == "")
                                openingbalance.record_status = "N";
                            else
                                openingbalance.record_status = dr["status"].ToString();
                                if (dr["openingbalancevalue"].ToString() == "" || dr["openingbalancevalue"].ToString() == null)
                                    openingbalance.openingbalancevalue = 0;
                                else
                                    openingbalance.openingbalancevalue = Convert.ToDouble(dr["openingbalancevalue"].ToString());
                                openingbalance.openingbalance_id = dr["openingbalance_id"].ToString();


                                // openingbalance.creation_date = dr["creation_date"].ToString().Substring(0,10);
                                openingbalances.Add(openingbalance);
                           // }
                        }
                    }
                   
                }
                catch(Exception ex)
                {
                    Console.Write(ex);
                }
                return openingbalances;
            }

        }
        public static List<OpeningBalance> Search(string tablename, string column, string value, string party_code, string party)
        {
            List<OpeningBalance> openingbalances = new List<OpeningBalance>();

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    StringBuilder str = new StringBuilder();
                    if (party_code == "ALL" || party_code == null)
                    {
                        cmd = new NpgsqlCommand("select distinct a.*,case when round(b.openingbalancevalue::numeric,2) is null then 0 else round(b.openingbalancevalue::numeric,2) end as openingbalancevalue ,b.openingbalance_id,b.record_status as status,b.remarks from  exciseautomation.view_opening_balance a left join exciseautomation.openingbalance b  on a.vat_code=b.vat_code inner join exciseautomation.view_checkuser c on b.user_id=c.user_id where  " + column + " Ilike '%" + value + "%' and b.record_active='true'  order by a.vat_code desc ", cn);
                    }
                    else
                    {
                        cmd = new NpgsqlCommand("select distinct a.*,case when round(b.openingbalancevalue::numeric,2) is null then 0 else round(b.openingbalancevalue::numeric,2) end as openingbalancevalue,b.openingbalance_id,b.record_status as status,b.remarks from  exciseautomation.view_opening_balance a left join exciseautomation.openingbalance b  on  a.vat_code=b.vat_code left join exciseautomation.view_checkuser c on b.user_id=c.user_id where a.party_code='" + party_code + "' and   " + column + " Ilike '%" + value + "%' and b.record_active='true' order by a.vat_code desc ", cn);

                    }
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            openingbalances = new List<OpeningBalance>();
                            while (dr.Read())
                            {
                                OpeningBalance openingbalance = new OpeningBalance();
                                //if (dr["status"].ToString() != "R")
                                //{
                                openingbalance.vat_code = dr["vat_code"].ToString();
                                openingbalance.vat_name = dr["vat_name"].ToString();
                                openingbalance.storage_content = dr["storage_content"].ToString();
                                openingbalance.vat_type_code = dr["vat_type_code"].ToString();
                                openingbalance.vat_type_name = dr["vat_type_name"].ToString();
                                openingbalance.uom_code = dr["uom_code"].ToString();
                                openingbalance.uom_name = dr["uom_name"].ToString();
                                openingbalance.Total_Capacity = Convert.ToDouble(dr["vat_totalcapacity"].ToString())
                                    ;
                                openingbalance.party_code = dr["Party_code"].ToString();
                                openingbalance.party_name = dr["Party_name"].ToString();
                            openingbalance.financial_year = dr["financial_year"].ToString();
                            openingbalance.remarks = dr["remarks"].ToString();
                                if (dr["status"].ToString() == "")
                                    openingbalance.record_status = "N";
                                else
                                    openingbalance.record_status = dr["status"].ToString();
                                if (dr["openingbalancevalue"].ToString() == "" || dr["openingbalancevalue"].ToString() == null)
                                    openingbalance.openingbalancevalue = 0;
                                else
                                    openingbalance.openingbalancevalue = Convert.ToDouble(dr["openingbalancevalue"].ToString());
                                openingbalance.openingbalance_id = dr["openingbalance_id"].ToString();


                                // openingbalance.creation_date = dr["creation_date"].ToString().Substring(0,10);
                                openingbalances.Add(openingbalance);
                                // }

                            }
                        }
                    
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }

            }
            return openingbalances;
        }





        public static string Approve(List<OpeningBalance> openingbalance)
        {
            string value="";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                
                    NpgsqlTransaction trn;
                    trn = cn.BeginTransaction();
                    try
                    {
                        for (int i = 0; i < openingbalance.Count; i++)
                        {
                        if (openingbalance[i].record_status == "A")
                        {
                            NpgsqlCommand cmd22 = new NpgsqlCommand("select case when count(1) is null then 0 else count(1) end from  exciseautomation.openingbalance  WHERE   financial_year='" + openingbalance[i].financial_year + "' and vat_code='" + openingbalance[i].vat_code + "' and record_status='A'", cn);
                            int str = Convert.ToInt32(cmd22.ExecuteScalar());
                            if (str == 0)
                            {
                                NpgsqlCommand cmd2 = new NpgsqlCommand("select case when vat_availablecapacity is null then 0 else vat_availablecapacity end vat_availablecapacity from  exciseautomation.vat_master  WHERE party_code='" + openingbalance[i].party_code + "' and vat_code='" + openingbalance[i].vat_code + "'", cn);
                                double available = Convert.ToDouble(cmd2.ExecuteScalar());
                                available = available+ openingbalance[i].openingbalancevalue;
                                cmd2 = new NpgsqlCommand("UPDATE exciseautomation.vat_master SET  vat_availablecapacity='" + available + "' WHERE party_code='" + openingbalance[i].party_code + "' and vat_code='" + openingbalance[i].vat_code + "'", cn);
                                int G = cmd2.ExecuteNonQuery();
                            }
                        }
                        if (openingbalance[i].openingbalance_id != "" && openingbalance[i].record_status!=null)
                        {
                            NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.openingbalance SET  record_status ='" + openingbalance[i].record_status + "' where  financial_year='" + openingbalance[i].financial_year + "' and vat_code ='" + openingbalance[i].vat_code + "' and openingbalance_id='" + openingbalance[i].openingbalance_id + "' ", cn);
                            int n = cmd.ExecuteNonQuery();
                            if (value == "")
                            {
                                string app;
                                if (openingbalance[0].record_status == "R")
                                {
                                    app = "Rejected by Bond Officer";
                                }
                                else
                                {
                                    app = "Approved by Bond Officer";
                                }
                                StringBuilder str = new StringBuilder();
                                str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                                str.Append("'" + openingbalance[i].openingbalance_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "','OBF','" + app + "','" + openingbalance[0].remarks + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + openingbalance[0].user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + openingbalance[0].user_id + "','"+openingbalance[0].financial_year+"','"+openingbalance[0].party_code+"')");
                                NpgsqlCommand cmd1 = new NpgsqlCommand(str.ToString(), cn);
                                cmd1.ExecuteNonQuery();
                                value = "0";
                            }
                        }
                        }
                   
                   
                        trn.Commit();
                    }
                    catch (Exception ex)
                    {
                        value = ex.Message;
                        trn.Rollback();
                    }
            }
            return value;
        }

        public static OpeningBalance GetOpeningBalance(string party)
        {
            OpeningBalance openingbalance = new OpeningBalance();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.View_Opening_Balance where party_code='"+ party+ "' order by openingbalance_id", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        openingbalance = new OpeningBalance();
                        while (dr.Read())
                        {
                          
                            openingbalance.vat_code = dr["vat_code"].ToString();
                            openingbalance.vat_name = dr["vat_name"].ToString();
                            openingbalance.storage_content = dr["storage_content"].ToString();
                            openingbalance.vat_type_code = dr["vat_type_code"].ToString();
                            openingbalance.vat_type_name = dr["vat_type_name"].ToString();
                            openingbalance.uom_code = dr["uom_code"].ToString();
                            openingbalance.uom_name = dr["uom_name"].ToString();
                            openingbalance.financial_year = dr["financial_year"].ToString();
                            openingbalance.openingbalancevalue =Convert.ToDouble(dr["openingbalancevalue"].ToString());
                            openingbalance.openingbalance_id = dr["openingbalance_id"].ToString();
                            openingbalance.user_id = dr["user_id"].ToString();
                           
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return openingbalance;
            }

        }
        public static string UpdateOpeningbalance(List<OpeningBalance> openingbalance)
        {
            string value ;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                //,remarks='"+openingbalance[i].remarks+"'
                try
                {
                    for (int i = 0; i < openingbalance.Count; i++)
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.openingbalance SET  openingbalancevalue ='" + openingbalance[i].openingbalancevalue + "',record_status='"+openingbalance[i].record_status+"',remarks='"+openingbalance[i].remarks+"' where vat_code ='" + openingbalance[i].vat_code + "' and openingbalance_id='" + openingbalance[i].openingbalance_id+"'", cn);
                        int n = cmd.ExecuteNonQuery();
                    }
                    value = "0";
                    trn.Commit();
                }
                catch(Exception ex)
                {
                    value = ex.Message;
                    trn.Rollback();
                }

                }
            return value;
        }

        public static string InsertOpeningbalance(List<OpeningBalance> openingbalance)
        {
            string value;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    int m=0;
                    int max = Convert.ToInt32(DL_Org_List.GetMaxID("openingbalance").ToString()) + 1;
                    NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT max(openingbalance_id) FROM exciseautomation.openingbalance where financial_year='" + openingbalance[0].financial_year + "'", cn);
                    string a = cmd1.ExecuteScalar().ToString();
                 int   n = 0;
                    if (a == "")
                        n = 1;
                    else
                        n = Convert.ToInt32(a) + 1;
                    for (int i = 0; i < openingbalance.Count; i++)
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand("select count(1) from exciseautomation.openingbalance  where financial_year='"+openingbalance[i].financial_year+"' and vat_code ='" + openingbalance[i].vat_code + "' ", cn);
                        string val = cmd.ExecuteScalar().ToString();
                        if(val!="" && val!="0")
                        {
                            //if (openingbalance[i].openingbalancevalue >=0)
                            //{
                                cmd = new NpgsqlCommand("UPDATE exciseautomation.openingbalance SET  openingbalancevalue ='" + openingbalance[i].openingbalancevalue + "',openingbalanceyear='"+openingbalance[i].openingbalanceyear+"',  record_status='" + openingbalance[0].record_status + "',remarks='" + openingbalance[i].remarks + "' where  financial_year='" + openingbalance[i].financial_year + "' and vat_code ='" + openingbalance[i].vat_code + "' ", cn);
                                m = cmd.ExecuteNonQuery();

                           // }
                        }
                        else
                        {
                            if (Convert.ToDouble(openingbalance[i].openingbalancevalue) >=0)
                            {
                                cmd = new NpgsqlCommand("INSERT INTO exciseautomation.openingbalance(openingbalance_id, vat_code, uom_code, storagecontent, openingbalancevalue,openingbalanceyear,creation_date, user_id,record_status,remarks,party_code,financial_year)VALUES('" +n + "','" + openingbalance[i].vat_code + "', '" + openingbalance[i].uom_code + "', '" + openingbalance[i].storage_content + "','" + openingbalance[i].openingbalancevalue + "','"+openingbalance[i].openingbalanceyear+"', '" + DateTime.Now.ToShortDateString() + "', '" + openingbalance[i].user_id + "','"+openingbalance[i].record_status+"','"+openingbalance[i].remarks+"','"+openingbalance[i].party_code+"','"+openingbalance[i].financial_year+"')", cn);
                                m = cmd.ExecuteNonQuery();
                                n++;
                            }
                        }
                    }
                    value = "0";
                    trn.Commit();
                   
                }
                catch (Exception ex)
                {
                    trn.Rollback();
                    value = ex.Message;
                  
                }

            }
            return value;
        }


        public static List<OpeningBalance> Getlist(string userid)
        {
            List<OpeningBalance> openingbalances = new List<OpeningBalance>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,t.vat_type_name,b.uom_name,c.openingbalancevalue from exciseautomation.vat_master a inner join  exciseautomation.vat_type_master t on a.vat_type_code=t.vat_type_code  inner join exciseautomation.uom_master b on a.uom_code = b.uom_code inner join exciseautomation.openingbalance  c on  a.vat_code = c.vat_code  ", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        openingbalances = new List<OpeningBalance>();
                        while (dr.Read())
                        {
                            OpeningBalance openingbalance = new OpeningBalance();
                            openingbalance.vat_code = dr["vat_code"].ToString();
                            openingbalance.vat_name = dr["vat_name"].ToString();
                            openingbalance.storage_content = dr["storage_content"].ToString();
                            openingbalance.vat_type_code = dr["vat_type_code"].ToString();
                            openingbalance.vat_type_name = dr["vat_type_name"].ToString();
                            openingbalance.uom_code = dr["uom_code"].ToString();
                            openingbalance.uom_name = dr["uom_name"].ToString();
                            openingbalance.openingbalancevalue = Convert.ToDouble(dr["openingbalancevalue"].ToString());
                            //  openingbalance.openingbalance_id = dr["openingbalance_id"].ToString();
                            openingbalance.party_code = dr["party_code"].ToString();
                            openingbalance.party_name = dr["party_name"].ToString();
                            openingbalance.creation_date = dr["creation_date"].ToString();
                            openingbalance.user_id = userid;
                            openingbalances.Add(openingbalance);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return openingbalances;
            }

        }


        public static int GetExistsData(string tablename, string column, string value)
        {
            int value1 = 0;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                        cmd = new NpgsqlCommand("select count(1) from exciseautomation." + tablename + " where " + column + "='" + value + "'", cn);
                    string re = cmd.ExecuteScalar().ToString();
                    if (re != "0")
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

    }
}
