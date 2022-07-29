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
    public class DL_Ticketcategory
    {

        public static bool Insert(Ticketcategory Ticket)
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
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.ticketcategory_master(ticketcategory_code, ticketcategory_name, lastmodified_date, user_id, creation_date)VALUES('"+ Ticket.ticketcategory_code+"', '"+ Ticket.ticketcategory_name+"', '"+DateTime.Now.ToShortDateString()+"', '"+ Ticket.user_id+"', '"+DateTime.Now.ToShortDateString()+"')", cn);
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

        public static bool Update(Ticketcategory Ticket)
        {
            bool val = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.ticketcategory_master SET ticketcategory_code ='" + Ticket.ticketcategory_code + "',   ticketcategory_name='" + Ticket.ticketcategory_name+ "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "'  WHERE ticketcategory_master_id ='" + Ticket.ticketcategory_master_id + "' ", cn);
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


        public static List<Ticketcategory> GetList()
        {
            List<Ticketcategory> ticket = new List<Ticketcategory>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;

                    cmd = new NpgsqlCommand("select * from exciseautomation.ticketcategory_master", cn);

                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        ticket = new List<Ticketcategory>();
                        while (dr.Read())
                        {
                            Ticketcategory record = new Ticketcategory();
                            record.ticketcategory_master_id = Convert.ToInt32(dr["ticketcategory_master_id"].ToString());
                            record.ticketcategory_code = dr["ticketcategory_code"].ToString();
                            record.ticketcategory_name= dr["ticketcategory_name"].ToString();
                            record.user_id = dr["user_id"].ToString();
                            ticket.Add(record);
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return ticket;
        }

    }
}
