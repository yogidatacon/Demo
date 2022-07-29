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
   public class DL_TicketStatus
    {
        public static bool Insert(TicketStatus Ticket)
        {
            bool val = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd1 = new NpgsqlCommand("select max(ticketstatus_master_id) from exciseautomation.ticketstatus_master", cn);
                    string m = cmd1.ExecuteScalar().ToString();
                    int n = 0;
                    if (m == "")
                        n = 1;
                    else
                        n = Convert.ToInt32(m) + 1;
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.ticketstatus_master( ticketstatus_code, ticketstatus_name, lastmodified_date, user_id, creation_date, record_status)VALUES('"+Ticket.ticketstatus_code+"', '"+Ticket.ticketstatus_name+"', '"+DateTime.Now.ToShortDateString()+"', '"+Ticket.user_id+"', '"+DateTime.Now.ToShortDateString()+"','true')", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();
                    val =true;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return val;
            }
        }

        public static bool Update(TicketStatus Ticket)
        {
            bool val =false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.ticketstatus_master SET ticketstatus_code ='" +Ticket.ticketstatus_code + "',   ticketstatus_name ='" +Ticket.ticketstatus_name + "', lastmodified_date ='" +DateTime.Now.ToShortDateString() + "'  WHERE ticketstatus_master_id='" +Ticket.ticketstatus_master_id+ "' ", cn);
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



        public static List<TicketStatus> GetList()
        {
            List<TicketStatus> tickets = new List<TicketStatus>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;

                    cmd = new NpgsqlCommand("select * from exciseautomation.ticketstatus_master", cn);

                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        tickets = new List<TicketStatus>();
                        while (dr.Read())
                        {
                            TicketStatus record = new TicketStatus();
                            record.ticketstatus_master_id = Convert.ToInt32(dr["ticketstatus_master_id"].ToString());
                            record.ticketstatus_code = dr["ticketstatus_code"].ToString();
                            record.ticketstatus_name = dr["ticketstatus_name"].ToString();
                            record.user_id= dr["user_id"].ToString();
                            tickets.Add(record);
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return tickets;
        }

    }
}
