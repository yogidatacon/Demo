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
   public class DL_ServerConfig
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static List<Server_Configs> GetSeverList(string userid) { 
        List<Server_Configs> servercon = new List<Server_Configs>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.config_master", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if(dr.HasRows)
                    {
                        servercon = new List<Server_Configs>();
                        while(dr.Read())
                        {
                            Server_Configs server = new Server_Configs();
                            server.config_master_id = dr["config_master_id"].ToString();
                            server.server_code = dr["server_code"].ToString();
                            server.server_user = dr["server_user"].ToString();
                            server.server_password = dr["server_password"].ToString();
                            server.server_url = dr["server_url"].ToString();
                            server.server_domain = dr["server_domain"].ToString(); 
                            server.user_id = dr["user_id"].ToString();
                            server.projectname= dr["projectname"].ToString();
                            servercon.Add(server);
                        }

                    }
                    _log.Info("Get GetSeverList Success");
                }
                catch(Exception ex)
                {
                    _log.Info("Get GetSeverList Fail :"+ex.Message);
                }

            }
            return servercon;


        }

        public static bool InsertSeverConfig(Server_Configs server)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.config_master(server_code, server_user, server_password, server_domain, server_url, lastmodified_date, user_id, creation_date)VALUES('"+server.server_code+"', '"+server.server_user+"','"+server.server_password+"', '"+server.server_domain+"', '"+server.server_url+"', '"+DateTime.Now.ToShortDateString()+"', '"+server.user_id+ "', '" + DateTime.Now.ToShortDateString() + "')", cn);
                    int n = cmd.ExecuteNonQuery();
                    if(n == 1)
                    {
                        value = true;
                        _log.Info("Insert SeverList Success :"+server.server_code+"-"+server.server_user);
                    }
                    else
                    {
                        value = false;
                        _log.Info("Insert SeverList Fail :" + server.server_code + "-" + server.server_user);
                    }
                }
                catch(Exception ex)
                {
                    value = false;
                    _log.Info("Insert SeverList Fail :" + server.server_code + "-" + server.server_user+"-"+ex.Message);

                }
                return value;
            }
        }

        public static bool UpdateSeverConfig(Server_Configs server)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.config_master SET  server_code ='" + server.server_code + "', server_user ='"+server.server_user+"', server_password ='"+server.server_password+"', server_domain ='"+server.server_domain+"', server_url ='"+server.server_url+" WHERE user_id ='"+server.user_id+"'", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                        _log.Info("Update SeverList Success :" + server.server_code + "-" + server.server_user);
                    }
                    else
                    {
                        value = false;
                        _log.Info("Update SeverList Fail :" + server.server_code + "-" + server.server_user);
                    }
                }
                catch (Exception ex)
                {
                    value = false;
                    _log.Info("Update SeverList Fail :" + server.server_code + "-" + server.server_user + "-" + ex.Message);

                }
                return value;
            }
        }

    }
}
