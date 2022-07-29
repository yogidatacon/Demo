using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
//using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.Entities;

namespace Usermngt.DAL
{
  public  class DL_Money
    {
        public static string Insert(cm_seiz_Money Money)
        {
            string value = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                //NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT max(seizure_propertydetails_id) FROM exciseautomation.seizure_propertydetails", cn);
                //string m = cmd1.ExecuteScalar().ToString();
                //int n = 0;
                //if (m == "")
                //    n = 1;
                //else
                //    n = Convert.ToInt32(m) + 1;
                //string tableName = "exciseautomation.seizure_moneydetails";
                int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.seizure_moneydetails", "seizure_moneydetails_id").ToString()) + 1;
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("INSERT INTO exciseautomation.seizure_moneydetails( seizure_moneydetails_id, seizureno, total_amount,remarks , ipaddress,lastmodified_date, user_id, creation_date,currency,coins, record_status,raidby)Values(");
                    str.Append("'" + max + "','"+ Money.seizureno + "','" + Money.total_amount + "','" + Money.remarks + "','"+Money.ipaddress+"','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + Money.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+Money.currency+"','"+Money.coins+"','"+Money.record_status+"','"+Money.raidby+"')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int a = cmd.ExecuteNonQuery();
                    if (a == 1)
                    {
                        value = "0";

                        for (int i = 0; i < Money.currencyCoins.Count; i++)
                        {                           
                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.seizure_currencycoins(seizure_moneydetails_id, seizureno, money_type, currency, noofpieces, amount, user_id, creation_date)");
                            str.Append("Values('" + max + "','" + Money.seizureno + "','" + Money.currencyCoins[i].money_type + "', '" + Money.currencyCoins[i].currency + "','" + Money.currencyCoins[i].noofpieces + "','" + Money.currencyCoins[i].amount + "','" + Money.currencyCoins[i].user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            a= cmd3.ExecuteNonQuery();
                        }
                        trn.Commit();
                    }
                    else
                    {
                        trn.Rollback();
                        value = "1";
                    }


                }
                catch (Exception ex)
                {
                    trn.Rollback();
                    value = ex.Message;
                    //  throw (ex);
                }

                return value;
            }
        }



        public static string Update(cm_seiz_Money Money)
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
                    str.Append("UPDATE exciseautomation.seizure_moneydetails SET  total_amount ='" + Money.total_amount + "', remarks ='" + Money.remarks + "',  coins ='" +Money.coins + "', currency ='" + Money.currency + "',  lastmodified_date ='" + DateTime.Now.ToShortDateString() + "',raidby='"+Money.raidby+"'  WHERE seizure_moneydetails_id ='" + Money.seizure_moneydetails_id + "' ");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        cmd = new NpgsqlCommand("Delete from exciseautomation.seizure_currencycoins where  seizure_moneydetails_id='" + Money.seizure_moneydetails_id + "'", cn);
                        cmd.ExecuteNonQuery();
                        for (int i = 0; i < Money.currencyCoins.Count; i++)
                        {
                            str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.seizure_currencycoins(seizure_moneydetails_id, seizureno, money_type, currency, noofpieces, amount, user_id, creation_date)");
                            str.Append("Values('" + Money.seizure_moneydetails_id + "','" + Money.seizureno + "','" + Money.currencyCoins[i].money_type + "', '" + Money.currencyCoins[i].currency + "','" + Money.currencyCoins[i].noofpieces + "','" + Money.currencyCoins[i].amount + "','" + Money.currencyCoins[i].user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd3.ExecuteNonQuery();
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



        public static List<cm_seiz_Money> GetList(string seizureNo)
        {
            string[] dept = seizureNo.Split('&');
            seizureNo = dept[0];
            string d = "";
            if (dept[1] == "Excise" || dept[1] == "E")
                d = "E";
            else
                d = "P";
            List<cm_seiz_Money> Property = new List<cm_seiz_Money>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();

                try
                {
                    NpgsqlCommand cmd;

                    cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.seizure_moneydetails where seizureno='" + seizureNo + "' and raidby='"+d+"'  ORDER BY seizure_moneydetails_id", cn);

                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        Property = new List<cm_seiz_Money>();
                        while (dr.Read())
                        {
                            cm_seiz_Money record = new cm_seiz_Money();
                            record.seizure_moneydetails_id = Convert.ToInt32(dr["seizure_moneydetails_id"].ToString());
                            record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                            record.total_amount = Convert.ToInt32(dr["total_amount"].ToString());
                            record.coins = Convert.ToInt32(dr["coins"].ToString());
                            record.currency = Convert.ToInt32(dr["currency"].ToString());
                            record.ipaddress = dr["ipaddress"].ToString();
                            record.remarks= dr["remarks"].ToString();
                            record.challan_date = dr["challan_date"].ToString();
                            record.record_status = dr["record_status"].ToString();
                            //  record.challan_no = dr["propertycriclename"].ToString();
                            // record.order_date= dr["order_date"].ToString();
                            record.user_id = dr["user_id"].ToString();
                            record.actioncompleted = dr["actioncompleted"].ToString();
                            record.date_of_destruction = dr["date_of_destruction"].ToString();
                            Property.Add(record);
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return Property;
        }


        public static cm_seiz_Money GetDetails(string userid, int moneyid)
        {
            cm_seiz_Money record = new cm_seiz_Money();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.seizure_moneydetails where user_id='" + userid + "' and seizure_moneydetails_id='" + moneyid + "'  ", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    System.Data.DataTable dt = new System.Data.DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        record.seizure_moneydetails_id = Convert.ToInt32(dr["seizure_moneydetails_id"].ToString());
                        record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                        record.total_amount = Convert.ToInt32(dr["total_amount"].ToString());
                        record.coins = Convert.ToInt32(dr["coins"].ToString());
                        record.currency = Convert.ToInt32(dr["currency"].ToString());
                        record.ipaddress = dr["ipaddress"].ToString();
                        record.remarks = dr["remarks"].ToString();
                        // record.challan_date = dr["challan_date"].ToString();
                        //  record.challan_no = dr["propertycriclename"].ToString();
                        //  record.order_date = dr["order_date"].ToString();
                        //  record.user_id = dr["user_id "].ToString();

                        record.currencyCoins = new List<cm_seiz_CurrencyCoins>();
                        try
                        {
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.seizure_currencycoins where seizure_moneydetails_id='" + record.seizure_moneydetails_id + "'  order by seizure_currencycoins_id", cn))
                            {
                                cmd1.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr2 = cmd1.ExecuteReader();

                                if (dr2.HasRows)
                                {
                                    while (dr2.Read())
                                    {
                                        cm_seiz_CurrencyCoins currCoins = new cm_seiz_CurrencyCoins();
                                        currCoins.money_type = dr2["money_type"].ToString();
                                        currCoins.currency = dr2["currency"].ToString();
                                        currCoins.noofpieces = dr2["noofpieces"].ToString();
                                        currCoins.amount = dr2["amount"].ToString();
                                        record.currencyCoins.Add(currCoins);
                                    }
                                }
                                dr2.Close();
                            }
                            //lstObj.Add(record);
                        }
                        catch (Exception ex)
                        {
                            //throw;
                        }
                    }

                    trn.Commit();
                    cn.Close();

                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return record;
        }


    }

}

