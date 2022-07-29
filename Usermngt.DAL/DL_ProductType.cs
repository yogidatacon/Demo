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
 public  class DL_ProductType
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static List<ProductType> GetProductType(string userid)
        {
            List<ProductType> products = new List<ProductType>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.product_type_master order by product_type_code ", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if(dr.HasRows)
                    {
                        products = new List<ProductType>();
                        while(dr.Read())
                        {
                            ProductType product = new ProductType();
                            product.product_type_master_id = dr["product_type_master_id"].ToString();
                            product.product_type_code = dr["product_type_code"].ToString();
                            product.product_type_name = dr["product_type_name"].ToString();
                            product.user_id = userid;
                            products.Add(product);

                        }
                    }
                    _log.Info("Get Product Type Master List Success");
                }
                catch(Exception ex)
                {
                    _log.Info("Get Product Type Master List Success :"+ex.Message);
                }
            }
            return products;
        }
        public static List<ProductType> SearchProductType(string tablename, string column, string value)
        {
            List<ProductType> products = new List<ProductType>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.product_type_master where " + column + " Ilike '%" + value + "%' order by product_type_code ", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        products = new List<ProductType>();
                        while (dr.Read())
                        {
                            ProductType product = new ProductType();
                            product.product_type_master_id = dr["product_type_master_id"].ToString();
                            product.product_type_code = dr["product_type_code"].ToString();
                            product.product_type_name = dr["product_type_name"].ToString();
                           // product.user_id = userid;
                            products.Add(product);

                        }
                    }
                    _log.Info("Get Product Type Master List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Product Type Master List Success :" + ex.Message);
                }
            }
            return products;

        }
        public static bool InsertProduct(ProductType product)
        {
            bool value = false;

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.product_type_master (product_type_code, product_type_name, lastmodified_date, user_id, creation_date) VALUES('"+product.product_type_code+"', '"+product.product_type_name+"', '"+DateTime.Now.ToShortDateString()+"', '"+product.user_id+"', '"+DateTime.Now.ToShortDateString()+"')", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                        _log.Info("Insert Product Type Master Success:"+product.product_type_code+"-"+product.product_type_name);
                    }
                    else
                    {
                        value = false;
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Insert Product Type Master Success:" + product.product_type_code + "-" + product.product_type_name+"-"+ex.Message);
                    value = false;
                }
                return value;

            }
        }


        public static bool InsertDept(Department product)
        {
            bool value = false;

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.department_master (department_code, department_name, lastmodified_date, user_id, creation_date) VALUES('" + product.dept_code + "', '" + product.dept_name + "', '" + DateTime.Now.ToShortDateString() + "', '" + product.user_id + "', '" + DateTime.Now.ToShortDateString() + "')", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                        _log.Info("Insert Department Master Success:" + product.dept_code + "-" + product.dept_name);
                    }
                    else
                    {
                        value = false;
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Insert Department Success:" + product.dept_code + "-" + product.dept_name + "-" + ex.Message);
                    value = false;
                }
                return value;

            }
        }


        public static bool UpdateProduct(ProductType product)
        {
            bool value = false;

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.product_type_master SET  product_type_name = '"+product.product_type_name+ "' WHERE   product_type_code = '" + product.product_type_code + "' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                        _log.Info("Update Product Type Master Success:" + product.product_type_code + "-" + product.product_type_name);
                    }
                    else
                    {
                        value = false;
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Insert Product Type Master Success:" + product.product_type_code + "-" + product.product_type_name+"-"+ex.Message);
                    value = false;
                }
                return value;

            }
        }


        public static bool UpdateDept(Department product)
        {
            bool value = false;

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.department_master SET  department_name = '" + product.dept_name + "' WHERE   department_code = '" + product.dept_code + "' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                        _log.Info("Update Department Master Success:" + product.dept_code + "-" + product.dept_name);
                    }
                    else
                    {
                        value = false;
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Insert Department Master Success:" + product.dept_code + "-" + product.dept_name + "-" + ex.Message);
                    value = false;
                }
                return value;

            }
        }

    }
}

