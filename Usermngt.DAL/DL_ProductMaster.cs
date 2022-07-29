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
    public class DL_ProductMaster
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static List<Product_Master> GetProductMaster(string userid)
        {
            List<Product_Master> products = new List<Product_Master>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.product_type_name from exciseautomation.product_master a inner join  exciseautomation.product_type_master b on a.product_type_code=b.product_type_code order by product_master_id", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        products = new List<Product_Master>();
                        while (dr.Read())
                        {
                            Product_Master product = new Product_Master();
                            product.product_master_id = dr["product_master_id"].ToString();
                            product.product_code = dr["product_code"].ToString();
                            product.product_name = dr["product_name"].ToString();
                            product.product_type_code = dr["product_type_code"].ToString();
                            product.product_type_name = dr["product_type_name"].ToString();
                            product.user_id = userid;
                            products.Add(product);

                        }
                    }
                    cn.Close();
                    _log.Info("Get Product Master List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Product Master List Success:"+ex.Message);
                   
                }
            }
            return products;

        }

        public static List<Product_Master> GetProductMasterList(string party_code)
        {
            List<Product_Master> products = new List<Product_Master>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    if (party_code=="ALL")
                    { cmd = new NpgsqlCommand("select distinct(a.product_name)as product, a.product_master_id,a.product_code,a.product_type_code,b.product_type_name from exciseautomation.product_master a inner join  exciseautomation.product_type_master b on a.product_type_code=b.product_type_code where  a.product_code!=9 and a.product_code!=12 order by product_master_id", cn);
                        
                    }
                    else
                    {
                        cmd = new NpgsqlCommand("select distinct(a.product_name)as product, a.product_master_id,a.product_code,a.product_type_code,b.product_type_name from exciseautomation.product_master a inner join  exciseautomation.product_type_master b on a.product_type_code=b.product_type_code inner join exciseautomation.vat_master c on a.product_code=c.storage_content where c.party_code='" + party_code+ "'and a.product_code!=9 and a.product_code!=12 order by product_master_id", cn);
                       
                    }
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        products = new List<Product_Master>();
                        while (dr.Read())
                        {
                            Product_Master product = new Product_Master();
                            product.product_master_id = dr["product_master_id"].ToString();
                            product.product_code = dr["product_code"].ToString();
                            product.product_name = dr["product"].ToString();
                            product.product_type_code = dr["product_type_code"].ToString();
                            product.product_type_name = dr["product_type_name"].ToString();
                            products.Add(product);

                        }
                    }
                    cn.Close();
                    _log.Info("Get Product Master List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Product Master List Success:" + ex.Message);

                }
            }
            return products;

        }
        public static List<Product_Master> SearchProduct(string tablename, string column, string value)
        {
            List<Product_Master> products = new List<Product_Master>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.product_type_name from exciseautomation.product_master a inner join  exciseautomation.product_type_master b on a.product_type_code=b.product_type_code  where " + column + " Ilike '%" + value + "%' order by product_name", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        products = new List<Product_Master>();
                        while (dr.Read())
                        {
                            Product_Master product = new Product_Master();
                            product.product_master_id = dr["product_master_id"].ToString();
                            product.product_code = dr["product_code"].ToString();
                            product.product_name = dr["product_name"].ToString();
                            product.product_type_code = dr["product_type_code"].ToString();
                            product.product_type_name = dr["product_type_name"].ToString();
                           // product.user_id = userid;
                            products.Add(product);

                        }
                    }
                    cn.Close();
                    _log.Info("Get Product Master List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Product Master List Success:" + ex.Message);

                }
            }
            return products;
        }

        public static bool InsertProductMaster(Product_Master product)
        {
            bool value = false;

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.product_master ( product_code, product_type_code, product_name, lastmodified_date, user_id, creation_date) VALUES('"+product.product_code+"','"+product.product_type_code+"', '"+product.product_name+"', '"+DateTime.Now.ToShortDateString()+"', '"+product.user_id+"', '"+DateTime.Now.ToShortDateString()+"')", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                        _log.Info("Inssert Product Master Success :"+product.product_code+"-"+product.product_name);
                    }
                    else
                    {
                        value = false;
                    }
                   
                }
                catch (Exception ex)
                {
                    _log.Info("Inssert Product Master Success :" + product.product_code + "-" + product.product_name+"-"+ex.Message);
                   // Console.Write(ex);
                    value = false;
                }
                return value;

            }
        }


        public static bool UpdateProductMaster(Product_Master product)
        {
            bool value = false;

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(" UPDATE exciseautomation.product_master SET   product_type_code = '"+product.product_type_code+"', product_name = '"+product.product_name+"' WHERE product_code ='"+product.product_code+"' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                        _log.Info("Update Product Master Success :" + product.product_code + "-" + product.product_name);
                    }
                    else
                    {
                        value = false;
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Inssert Product Master Success :" + product.product_code + "-" + product.product_name+"-"+ex.Message);
                    value = false;
                }
                return value;

            }
        }
    }
}
