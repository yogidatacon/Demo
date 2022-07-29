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
   public class DL_RawMaterialType
    {
        public static List<RawMaterialType> GetRawMaterial(string userid)
        {
            List<RawMaterialType> rawmaterials = new List<RawMaterialType>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.rawmaterial_type_master order by rawmaterial_type_name", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if(dr.HasRows)
                    {
                        rawmaterials = new List<RawMaterialType>();
                        while(dr.Read())
                        {
                            RawMaterialType rawmaterial = new RawMaterialType();
                            rawmaterial.rawmaterial_type_master_id = dr["rawmaterial_type_master_id"].ToString();
                            rawmaterial.rawmaterial_type_code = dr["rawmaterial_type_code"].ToString();
                            rawmaterial.rawmaterial_type_name = dr["rawmaterial_type_name"].ToString();
                            rawmaterial.user_id = userid;
                            rawmaterials.Add(rawmaterial);
                        }
                    }
                }
                catch(Exception ex)
                {
                    throw (ex);
                }
                return rawmaterials;
            }
        }

        public static List<RawMaterialType> SearchRawMaterialType(string tablename, string column, string value)
        {
            List<RawMaterialType> rawmaterials = new List<RawMaterialType>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.rawmaterial_type_master where " + column + " Ilike '%" + value + "%'  order by rawmaterial_type_name", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        rawmaterials = new List<RawMaterialType>();
                        while (dr.Read())
                        {
                            RawMaterialType rawmaterial = new RawMaterialType();
                            rawmaterial.rawmaterial_type_master_id = dr["rawmaterial_type_master_id"].ToString();
                            rawmaterial.rawmaterial_type_code = dr["rawmaterial_type_code"].ToString();
                            rawmaterial.rawmaterial_type_name = dr["rawmaterial_type_name"].ToString();
                           // rawmaterial.user_id = userid;
                            rawmaterials.Add(rawmaterial);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return rawmaterials;
            }
        }

        public static bool InsertRawMaterial(RawMaterialType rawmaterial)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.rawmaterial_type_master( rawmaterial_type_code, rawmaterial_type_name, lastmodified_date, user_id, creation_date)VALUES('"+rawmaterial.rawmaterial_type_code+"', '"+rawmaterial.rawmaterial_type_name+"', '"+DateTime.Now.ToShortDateString()+"', '"+rawmaterial.user_id+"', '"+DateTime.Now.ToShortDateString()+"') ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if(n ==1)
                    {
                        value = true;
                    }
                    else
                    {
                        value = false;
                    }


                }
                catch(Exception ex)
                {
                    value = false;
                    throw (ex);
                }
                return value;

         }
        }


        public static bool UpdateRawMaterial(RawMaterialType rawmaterial)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.rawmaterial_type_master SET   rawmaterial_type_name = '"+rawmaterial.rawmaterial_type_name+"' WHERE  rawmaterial_type_code = '"+rawmaterial.rawmaterial_type_code+"' ", cn);
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
                    value = false;
                    throw (ex);
                }
                return value;

            }
        }

    }
}
