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
   public class DL_Thana
    {

        public static List<ThanaMaster> GetThana(string userid)
        {
            List<ThanaMaster> thana = new List<ThanaMaster>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select  b.*,a.division_name,t.state_name,c.district_name from exciseautomation.division_master a inner join  exciseautomation.State_master t on a.state_code=t.state_code inner join exciseautomation.district_master c on  a.division_code = c.division_code inner join exciseautomation.thana_master b on c.district_code=b.district_code order by b.thana_name ", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    //cmd.Parameters.AddWithValue("@UserID", userid);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        thana = new List<ThanaMaster>();
                        while (dr.Read())
                        {
                            ThanaMaster thanas = new ThanaMaster();
                            thanas.thana_master_id = dr["thana_master_id"].ToString();
                            thanas.thana_code = dr["thana_code"].ToString();
                            thanas.thana_name = dr["thana_name"].ToString();
                            thanas.district_code = dr["district_code"].ToString();
                            thanas.division_code = dr["division_code"].ToString();
                            thanas.state_code = dr["state_code"].ToString();
                            thanas.state_name = dr["state_name"].ToString();
                            thanas.district_name = dr["district_name"].ToString();
                            thanas.division_name = dr["division_name"].ToString();
                            thanas.user_id = dr["user_id"].ToString();
                            thana.Add(thanas);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return thana;
            }
        }

        public static List<ThanaMaster> GetThana1(string district)
        {
            List<ThanaMaster> thana = new List<ThanaMaster>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select  b.*,a.division_name,t.state_name,c.district_name from exciseautomation.division_master a inner join  exciseautomation.State_master t on a.state_code=t.state_code inner join exciseautomation.district_master c on  a.division_code = c.division_code inner join exciseautomation.thana_master b on c.district_code=b.district_code where c.district_code='"+district+"' order by b.thana_name ", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    //cmd.Parameters.AddWithValue("@UserID", userid);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        thana = new List<ThanaMaster>();
                        while (dr.Read())
                        {
                            ThanaMaster thanas = new ThanaMaster();
                            thanas.thana_master_id = dr["thana_master_id"].ToString();
                            thanas.thana_code = dr["thana_code"].ToString();
                            thanas.thana_name = dr["thana_name"].ToString();
                            thanas.district_code = dr["district_code"].ToString();
                            thanas.division_code = dr["division_code"].ToString();
                            thanas.state_code = dr["state_code"].ToString();
                            thanas.state_name = dr["state_name"].ToString();
                            thanas.district_name = dr["district_name"].ToString();
                            thanas.division_name = dr["division_name"].ToString();
                            thanas.user_id = dr["user_id"].ToString();
                            thana.Add(thanas);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return thana;
            }
        }
        public static int GetMax()
        {
            int n = 0;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select case when max(thana_master_id) is null then 0 else max(thana_master_id) end as thana_master_id  from exciseautomation.thana_master", cn);
                    n = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                }
                catch
                {

                }
                return n;
            }
        }
        public static List<ThanaMaster> SearchThana(string tablename, string column, string value)
        {
            List<ThanaMaster> thana = new List<ThanaMaster>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select  b.*,a.division_name,t.state_name,c.district_name from exciseautomation.division_master a inner join  exciseautomation.State_master t on a.state_code=t.state_code inner join exciseautomation.district_master c on  a.division_code = c.division_code inner join exciseautomation.thana_master b on c.district_code=b.district_code where " + column + " Ilike '%" + value + "%'   order by b.thana_name ", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    //cmd.Parameters.AddWithValue("@UserID", userid);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        thana = new List<ThanaMaster>();
                        while (dr.Read())
                        {
                            ThanaMaster thanas = new ThanaMaster();
                            thanas.thana_master_id = dr["thana_master_id"].ToString();
                            thanas.thana_code = dr["thana_code"].ToString();
                            thanas.thana_name = dr["thana_name"].ToString();
                            thanas.district_code = dr["district_code"].ToString();
                            thanas.division_code = dr["division_code"].ToString();
                            thanas.state_code = dr["state_code"].ToString();
                            thanas.state_name = dr["state_name"].ToString();
                            thanas.district_name = dr["district_name"].ToString();
                            thanas.division_name = dr["division_name"].ToString();
                            thanas.user_id = dr["user_id"].ToString();
                            thana.Add(thanas);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return thana;
            }
        }

        public static bool Insetthana(ThanaMaster thana)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.thana_master(thana_code, thana_name, district_code, division_code, state_code, lastmodified_date, user_id, creation_date) VALUES('" + thana.thana_code + "', '" + thana.thana_name + "', '" + thana.district_code + "', '" + thana.division_code + "', '" + thana.state_code + "' , '" + DateTime.Now.ToShortDateString() + "','" + thana.user_id + "','" + DateTime.Now.ToShortDateString() + "')", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;

                    }
                    else
                    {
                        value = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    value = false;
                }
                return value;
            }

        }

        public static bool UpdateThana(ThanaMaster thana)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.thana_master SET  thana_name ='"+thana.thana_name+"', district_code ='"+thana.district_code+"', division_code ='"+thana.division_code+"', state_code ='"+thana.state_code+"', lastmodified_date ='"+DateTime.Now.ToShortDateString()+"' WHERE  thana_code ='"+thana.thana_code+"' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                    }
                    else
                    {
                        value = false;

                    }

                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    value = false;
                }
                return value;
            }
        }
    }
}
