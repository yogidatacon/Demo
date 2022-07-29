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
  public  class DL_UOM
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static List<UOM_Master> GetList(string userid)
        {
            List<UOM_Master> uoms = new List<UOM_Master>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.UOM_Master order by uom_code ", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        uoms = new List<UOM_Master>();
                        while (dr.Read())
                        {
                            UOM_Master uom = new UOM_Master();
                           
                            uom.uom_code = dr["uom_code"].ToString();
                            uom.uom_name = dr["uom_name"].ToString();
                            uom.user_id = userid;
                            uoms.Add(uom);

                        }
                    }
                    _log.Info("Get UOM List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get UOM List Fail");
                }
            }
            return uoms;
        }

        public static List<UOM_Master> SearchUOM(string tablename, string column, string value)
        {
            List<UOM_Master> uoms = new List<UOM_Master>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.UOM_Master where " + column + " Ilike '%" + value + "%' order by uom_name ", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        uoms = new List<UOM_Master>();
                        while (dr.Read())
                        {
                            UOM_Master uom = new UOM_Master();

                            uom.uom_code = dr["uom_code"].ToString();
                            uom.uom_name = dr["uom_name"].ToString();
                            //uom.user_id = userid;
                            uoms.Add(uom);

                        }
                    }
                    _log.Info("Get UOM List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get UOM List Fail");
                }
            }
            return uoms;
        }
        public static string Insert(UOM_Master uom)
        {
            string value ;

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.UOM_Master (uom_code, uom_name,  user_id, creation_date) VALUES('" + uom.uom_code + "', '" + uom.uom_name + "',  '" + uom.user_id + "', '" + DateTime.Now.ToShortDateString() + "')", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = "0";
                        _log.Info("Insert UOM Success :" + uom.uom_code + "-" + uom.uom_name);
                    }
                    else
                    {
                        value = "1";
                        _log.Info("Insert UOM Fail :" + uom.uom_code + "-" + uom.uom_name);
                    }
                    cn.Close();

                }
                catch (Exception ex1)
                {
                    _log.Info("Insert UOM Fail :" + uom.uom_code + "-" + uom.uom_name+"-"+ex1.Message);
                    value = ex1.Message;
                }
                return value;

            }
        }

        public static string Update(UOM_Master uom)
        {
            string value;

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.UOM_Master SET  uom_name = '" + uom.uom_name + "' WHERE   uom_code = '" + uom.uom_code + "' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = "0";
                        _log.Info("Update UOM Success :" + uom.uom_code + "-" + uom.uom_name);
                    }
                    else
                    {
                        value = "1";
                        _log.Info("Update UOM Fail :" + uom.uom_code + "-" + uom.uom_name);
                    }
                    cn.Close();
                }
                catch (Exception ex1)
                {
                    _log.Info("Update UOM Fail :" + uom.uom_code + "-" + uom.uom_name + "-" + ex1.Message);
                    value = ex1.Message;
                }
                return value;

            }
        }
    }
}
