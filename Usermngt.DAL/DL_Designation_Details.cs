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
    public class DL_Designation_Details
    {
        public static string InsertDtype(Designation_Details des)
        {
            string value = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select case when  Count(1) is null then 0 else  Count(1) end from exciseautomation.designation_type_master where designation_type_code='" + des.designation_type_code + "'", cn);
                    int n = Convert.ToInt32(cmd.ExecuteScalar());
                    if (n == 0)
                    {
                        cmd = new NpgsqlCommand("INSERT INTO exciseautomation.designation_type_master(designation_type_code, designation_type_name, user_id, creation_date) VALUES('" + des.designation_type_code + "', '" + des.designation_type + "', '" + des.user_id + "','" + DateTime.Now.ToShortDateString() + "')", cn);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("update exciseautomation.designation_type_master set designation_type_name='"+des.designation_type+ "',lastmodified_date='" + DateTime.Now.ToShortDateString() + "',User_id='"+des.user_id+"' where designation_type_code='" + des.designation_type_code + "'", cn);
                        cmd1.ExecuteNonQuery();
                    }
                    value ="0";

                   
                  
                }
                catch (Exception ex)
                {
                  
                    value = ex.Message;
                }
                return value;
            }
        }
        public static List<Designation_Details> GetDListdesignationtype()
        {
            List<Designation_Details> des = new List<Designation_Details>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select  * from  exciseautomation.designation_type_master order by designation_type_name", cn);
                    //NpgsqlCommand cmd = new NpgsqlCommand("select trim(a.raidteamlead) as raidteamlead, (a.*), b.Designation_name, designation_type_name from exciseautomation.seizure_raiddetails a inner join exciseautomation.designation_master b on b.designation_code = a.designation_code inner join designation_type_master c on b.designation_type_code = c.designation_type_code  where seizureNo = " +seizureno+ " order by a.seizure_raiddetails_id", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    //cmd.Parameters.AddWithValue("@UserID", userid);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        des = new List<Designation_Details>();
                        while (dr.Read())
                        {
                            Designation_Details d = new Designation_Details();
                            d.designation_id = dr["designation_type_master_id"].ToString();
                            //d.designation_code = dr["designation_code"].ToString();
                            //d.designation_name = dr["designation_name"].ToString();
                            d.designation_type_code = dr["designation_type_code"].ToString();
                            d.designation_type = dr["designation_type_name"].ToString();
                            des.Add(d);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return des;
            }
        }
        public static string InsertD(Designation_Details des)
        {
            string value = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select case when  Count(1) is null then 0 else  Count(1) end from exciseautomation.designation_master where designation_code='" + des.designation_code + "'", cn);
                    int n = Convert.ToInt32(cmd.ExecuteScalar());
                    if (n == 0)
                    {
                        cmd = new NpgsqlCommand("INSERT INTO exciseautomation.designation_master(designation_type_code,designation_code, designation_name, user_id, creation_date) VALUES('" + des.designation_type_code + "', '" + des.designation_code + "', '" + des.designation_name + "', '" + des.user_id + "','" + DateTime.Now.ToShortDateString() + "')", cn);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("update exciseautomation.designation_master set designation_name='" + des.designation_name + "',designation_type_code='"+des.designation_type_code+"',lastmodified_date='" + DateTime.Now.ToShortDateString() + "',User_id='" + des.user_id + "' where designation_code='" + des.designation_type_code + "'", cn);
                        cmd1.ExecuteNonQuery();
                    }
                    value = "0";



                }
                catch (Exception ex)
                {

                    value = ex.Message;
                }
                return value;
            }
        }
        public static List<Designation_Details> GetDtypeList()
        {
            List<Designation_Details> des = new List<Designation_Details>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select  * from exciseautomation.designation_type_master order by designation_type_master_id ", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    //cmd.Parameters.AddWithValue("@UserID", userid);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        des = new List<Designation_Details>();
                        while (dr.Read())
                        {
                            Designation_Details d = new Designation_Details();
                            d.designation_id = dr["designation_type_master_id"].ToString();
                            d.designation_type_code = dr["designation_type_code"].ToString();
                            d.designation_type = dr["designation_type_name"].ToString();
                            des.Add(d);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return des;
            }
        }
        public static List<Designation_Details> SearchDesignationtype(string tablename, string column, string value)
        {
            List<Designation_Details> des = new List<Designation_Details>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select  * from exciseautomation.designation_type_master where " + column + " Ilike '" + value + "%' order by designation_type_name ", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    //cmd.Parameters.AddWithValue("@UserID", userid);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        des = new List<Designation_Details>();
                        while (dr.Read())
                        {
                            Designation_Details d = new Designation_Details();
                            d.designation_id = dr["designation_type_master_id"].ToString();
                            d.designation_type_code = dr["designation_type_code"].ToString();
                            d.designation_type = dr["designation_type_name"].ToString();
                            des.Add(d);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return des;
            }
        }
        public static List<Designation_Details> GetDList()
        {
            List<Designation_Details> des = new List<Designation_Details>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select  a.*,b.designation_type_name from exciseautomation.designation_master a inner join exciseautomation.designation_type_master b on a.designation_type_code=b.designation_type_code order by designation_master_id ", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    //cmd.Parameters.AddWithValue("@UserID", userid);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        des = new List<Designation_Details>();
                        while (dr.Read())
                        {
                            Designation_Details d = new Designation_Details();
                            d.designation_id = dr["designation_master_id"].ToString();
                            d.designation_code = dr["designation_code"].ToString();
                            d.designation_name = dr["designation_name"].ToString();
                            d.designation_type_code = dr["designation_type_code"].ToString();
                            d.designation_type = dr["designation_type_name"].ToString();
                            des.Add(d);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return des;
            }
        }


        public static List<Designation_Details> SearchDesignation(string tablename, string column, string value)
        {
            List<Designation_Details> des = new List<Designation_Details>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select  a.*,b.designation_type_name from exciseautomation.designation_master a inner join exciseautomation.designation_type_master b on a.designation_type_code=b.designation_type_code where " + column + " Ilike '" + value + "%' order by designation_master_id ", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    //cmd.Parameters.AddWithValue("@UserID", userid);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        des = new List<Designation_Details>();
                        while (dr.Read())
                        {
                            Designation_Details d = new Designation_Details();
                            d.designation_id = dr["designation_master_id"].ToString();
                            d.designation_code = dr["designation_code"].ToString();
                            d.designation_name = dr["designation_name"].ToString();
                            d.designation_type_code = dr["designation_type_code"].ToString();
                            d.designation_type = dr["designation_type_name"].ToString();
                            des.Add(d);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return des;
            }
        }
        }
}
