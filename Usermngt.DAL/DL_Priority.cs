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
     public class DL_Priority
    {
        public static bool Insert(Priority priority)
        {
            bool val = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd1 = new NpgsqlCommand("select max(priority_master_id) from exciseautomation.priority_master", cn);
                    string m = cmd1.ExecuteScalar().ToString();
                    int n = 0;
                    if (m == "")
                        n = 1;
                    else
                        n = Convert.ToInt32(m) + 1;
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.priority_master(priority_code, priority_name, lastmodified_date, user_id, creation_date, record_status,priority_resolvetime)VALUES( '" + priority.priority_code+"', '"+priority.priority_name+"','"+ DateTime.Now.ToShortDateString()+"', '"+priority.user_id+"', '"+ DateTime.Now.ToShortDateString()+"', 'true','"+priority.priority_resolvetime+"')", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();
                    val = true;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return val;
            }
        }

        public static bool Update(Priority priority)
        {
            bool val = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.priority_master SET priority_code ='" + priority.priority_code + "',   priority_name='" + priority.priority_name + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "',priority_resolvetime='"+priority.priority_resolvetime+"'  WHERE priority_master_id ='" + priority.priority_master_id+ "' ", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();
                    val = true;
                }
                catch (Exception ex)
                {

                    throw (ex);
                }
                return val;
            }
        }


        public static List<Priority> GetList()
        {
            List<Priority> priority = new List<Priority>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;

                    cmd = new NpgsqlCommand("select * from exciseautomation.priority_master", cn);

                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        priority = new List<Priority>();
                        while (dr.Read())
                        {
                            Priority record = new Priority();
                            record.priority_master_id = Convert.ToInt32(dr["priority_master_id"].ToString());
                            record.priority_code = dr["priority_code"].ToString();
                            record.priority_name = dr["priority_name"].ToString();
                            record.priority_resolvetime= dr["priority_resolvetime"].ToString();
                            record.user_id = dr["user_id"].ToString();
                            priority.Add(record);
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return priority;
        }


        public static Priority Getreslovetime( string Code)
        {
            Priority priort = new Priority();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("	select priority_resolvetime from exciseautomation.priority_master where priority_code='"+Code+"'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            priort.priority_resolvetime = dr["priority_resolvetime"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return priort;
            }

        }


    }
}
