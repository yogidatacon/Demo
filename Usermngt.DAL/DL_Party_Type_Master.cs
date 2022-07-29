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
    public class DL_Party_Type_Master
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static string Insert(Party_Type_Master partytype)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("INSERT INTO exciseautomation.party_type_master(party_type_code, party_type_name, party_active, org_id, user_id, creation_date)");
                    str.Append("VALUES('"+partytype.party_type_code+"','"+partytype.party_type_name+"','"+partytype.party_active+"','"+partytype.org_id+"','"+partytype.user_id+"','"+DateTime.Now.ToString("dd-MM-yyyy")+"')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        VAL = "0";
                    }
                    else
                        VAL = "1";
                    cn.Close();
                    _log.Info("Insert Party Type Master Success :" + partytype.party_type_code + "-" + partytype.party_type_name);
                }
                catch (Exception ex1)
                {
                    _log.Info("Insert Party Type Master Success :" + partytype.party_type_code + "-" + partytype.party_type_name+"-"+ex1.Message);
                    VAL = ex1.Message;
                }

            }
            return VAL;
        }
        public static string Update(Party_Type_Master partytype)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("Update exciseautomation.party_type_master set party_active='"+partytype.party_active+ "',party_type_name='"+partytype.party_type_name+"' where party_type_code='" + partytype.party_type_code+ "' ");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        VAL = "0";
                    }
                    else
                        VAL = "1";
                    cn.Close();
                    _log.Info("Update Party Type Master Success :" + partytype.party_type_code + "-" + partytype.party_type_name);
                }
                catch (Exception ex1)
                {
                    _log.Info("Insert Party Type Master Success :" + partytype.party_type_code + "-" + partytype.party_type_name+"-"+ex1.Message);
                    VAL = ex1.Message;
                }

            }
            return VAL;
        }
        public static List<Party_Type_Master> GetList()
        {

            List<Party_Type_Master> partytypelist = new List<Party_Type_Master>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.party_type_master order by party_type_code", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            partytypelist = new List<Party_Type_Master>();
                            while (dr.Read())
                            {
                                Party_Type_Master record = new Party_Type_Master();
                                record.party_type_name = dr["Party_type_name"].ToString();
                                record.party_type_code= dr["party_type_code"].ToString();
                                record.party_active = dr["party_active"].ToString();
                               
                                partytypelist.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Party Type Master List Success :"+ex.Message);
                }

            }
            return partytypelist;
        }

        public static List<Party_Type_Master> SearchPartyType(string tablename, string column, string value)
        {
            List<Party_Type_Master> partytypelist = new List<Party_Type_Master>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.party_type_master where " + column + " Ilike '%" + value + "%'    order by party_type_name  ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            partytypelist = new List<Party_Type_Master>();
                            while (dr.Read())
                            {
                                Party_Type_Master record = new Party_Type_Master();
                                record.party_type_name = dr["Party_type_name"].ToString();
                                record.party_type_code = dr["party_type_code"].ToString();
                                record.party_active = dr["party_active"].ToString();

                                partytypelist.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return partytypelist;
        }

        public static List<Department> SearchDept(string tablename, string column, string value)
        {
            List<Department> partytypelist = new List<Department>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.department_master where " + column + " Ilike '%" + value + "%'    order by department_name  ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            partytypelist = new List<Department>();
                            while (dr.Read())
                            {
                               Department record = new Department();
                                record.dept_name = dr["department_name"].ToString();
                                record.dept_code = dr["department_code"].ToString();
                                record.user_id = dr["user_id"].ToString();

                                partytypelist.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Department Master List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Department Master List Success :" + ex.Message);
                }

            }
            return partytypelist;
        }
    }
}
