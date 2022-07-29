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
  public  class DL_RawMaterial
    {
        public static List<RawMaterial> GetRawMaterial(string userid)
        {
            List<RawMaterial> rawmaterials = new List<RawMaterial>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                    try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,t.rawmaterial_type_name,b.uom_name from exciseautomation.rawmaterial_master a inner join  exciseautomation.rawmaterial_type_master t on a.rawmaterial_type_code=t.rawmaterial_type_code  inner join exciseautomation.uom_master b on a.uom_code = b.uom_code order by a.rawmaterial_name", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if(dr.HasRows)
                    {
                         rawmaterials = new List<RawMaterial>();
                        while(dr.Read())
                        {
                          RawMaterial  rawmaterial = new RawMaterial();
                            rawmaterial.rawmaterial_code = dr["rawmaterial_code"].ToString();
                            rawmaterial.rawmaterial_name = dr["rawmaterial_name"].ToString();
                            rawmaterial.rawmaterial_description = dr["rawmaterial_description"].ToString();
                            rawmaterial.rawmaterial_type_code = dr["rawmaterial_type_code"].ToString();
                            rawmaterial.rawmaterial_type_name = dr["rawmaterial_type_name"].ToString();
                            rawmaterial.uom_code = dr["uom_code"].ToString();
                            rawmaterial.product_type_code = dr["product_type_code"].ToString();
                            rawmaterial.uom_name = dr["uom_name"].ToString();
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


        public static List<RawMaterial> GetRawMateriallist(string userid)
        {
            List<RawMaterial> rawmaterials = new List<RawMaterial>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    if (userid =="ALL")
                    {
                        cmd = new NpgsqlCommand("select distinct a.rawmaterial_code,a.product_type_code,a.rawmaterial_name from exciseautomation.rawmaterial_master a inner join exciseautomation.vat_master b on a.product_type_code=b.product_type_code", cn);
                    }
                    else
                    {
                        cmd = new NpgsqlCommand("select distinct a.rawmaterial_code,a.product_type_code,a.rawmaterial_name from exciseautomation.rawmaterial_master a, exciseautomation.vat_master b where a.product_type_code = b.product_type_code and b.party_code='" + userid + "'", cn);
                    }
                  
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        rawmaterials = new List<RawMaterial>();
                        while (dr.Read())
                        {
                            RawMaterial rawmaterial = new RawMaterial();
                            rawmaterial.rawmaterial_code = dr["rawmaterial_code"].ToString();
                            rawmaterial.rawmaterial_name = dr["rawmaterial_name"].ToString();
                            rawmaterial.product_type_code = dr["product_type_code"].ToString();
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
        public static List<RawMaterial> SearchRawMaterial(string tablename, string column, string value)
        {
            List<RawMaterial> rawmaterials = new List<RawMaterial>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,t.rawmaterial_type_name,b.uom_name from exciseautomation.rawmaterial_master a inner join  exciseautomation.rawmaterial_type_master t on a.rawmaterial_type_code=t.rawmaterial_type_code  inner join exciseautomation.uom_master b on a.uom_code = b.uom_code where " + column + " Ilike '%" + value + "%' order by a.rawmaterial_name", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        rawmaterials = new List<RawMaterial>();
                        while (dr.Read())
                        {
                            RawMaterial rawmaterial = new RawMaterial();
                            rawmaterial.rawmaterial_code = dr["rawmaterial_code"].ToString();
                            rawmaterial.rawmaterial_name = dr["rawmaterial_name"].ToString();
                            rawmaterial.rawmaterial_description = dr["rawmaterial_description"].ToString();
                            rawmaterial.rawmaterial_type_code = dr["rawmaterial_type_code"].ToString();
                            rawmaterial.rawmaterial_type_name = dr["rawmaterial_type_name"].ToString();
                            rawmaterial.uom_code = dr["uom_code"].ToString();
                            rawmaterial.uom_name = dr["uom_name"].ToString();
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

        public static bool InsertRawMaterial(RawMaterial rawmaterial)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.rawmaterial_master(rawmaterial_code, rawmaterial_name, rawmaterial_description, rawmaterial_type_code, uom_code, lastmodified_date,product_type_code, user_id, creation_date) VALUES('" + rawmaterial.rawmaterial_code+"', '"+rawmaterial.rawmaterial_name+"', '"+rawmaterial.rawmaterial_description+"','"+rawmaterial.rawmaterial_type_code+"', '"+rawmaterial.uom_code+"', '"+DateTime.Now.ToShortDateString()+ "','" + rawmaterial.product_type_code + "', '" + rawmaterial.user_id+"', '"+DateTime.Now.ToShortDateString()+"')", cn);
                    int n = cmd.ExecuteNonQuery();
                    if(n == 1)
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


        public static bool UpdateRawMaterial(RawMaterial rawmaterial)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.rawmaterial_master SET  rawmaterial_name ='"+rawmaterial.rawmaterial_name+"', rawmaterial_description ='"+rawmaterial.rawmaterial_description+"', rawmaterial_type_code ='"+rawmaterial.rawmaterial_type_code+"', uom_code ='"+rawmaterial.uom_code+"', lastmodified_date ='"+DateTime.Now.ToShortDateString()+"',product_type_code='"+rawmaterial.product_type_code+"' WHERE rawmaterial_code ='"+rawmaterial.rawmaterial_code+"' ", cn);
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
