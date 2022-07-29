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
    public class DL_DispatchType
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static List<DispatchType> GetList()
        {
            List<DispatchType> DispatchType = new List<DispatchType>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.dispatch_type_master order by dispatch_type_name", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            DispatchType = new List<DispatchType>();
                            while (dr.Read())
                            {
                                DispatchType record = new DispatchType();
                                record.dispatch_type_id = dr["dispatch_type_id"].ToString();
                                record.dispatch_type_code = dr["dispatch_type_code"].ToString();
                                record.dispatch_type_name = dr["dispatch_type_name"].ToString();
                                DispatchType.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Dispach Master List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Dispach Master List Success :" + ex.Message);
                }

            }
            return DispatchType;
        }
        public static List<DispatchType> SearchDispatchType(string tablename, string column, string value)
        {
            List<DispatchType> DispatchType = new List<DispatchType>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.dispatch_type_master where " + column + " Ilike '%" + value + "%' order by dispatch_type_name", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            DispatchType = new List<DispatchType>();
                            while (dr.Read())
                            {
                                DispatchType record = new DispatchType();
                                record.dispatch_type_id = dr["dispatch_type_id"].ToString();
                                record.dispatch_type_code = dr["dispatch_type_code"].ToString();
                                record.dispatch_type_name = dr["dispatch_type_name"].ToString();
                                DispatchType.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Dispach Master List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Dispach Master List Success :" + ex.Message);
                }

            }
            return DispatchType;
        }
        public static string InsertDoc(DocumentFormats doc)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select case when count(1) is null then 0 else count(1) end from exciseautomation.document_format_master where party_code='" + doc.party_code+"'", cn);
                    int n = Convert.ToInt32(cmd.ExecuteScalar());
                    if (n == 0)
                    {
                        StringBuilder str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.document_format_master(party_code,noc,pass,release_request, molasses_allotment, creation_date,  user_id,permit)");
                        str.Append("VALUES('" +doc.party_code + "','" + doc.noc + "','" + doc.pass + "','" + doc.release_request + "','" + doc.molasses_allotment + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + doc.user_id + "','"+doc.permit+"')");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        n = cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        StringBuilder str = new StringBuilder();
                        str.Append("update exciseautomation.document_format_master set  noc='"+doc.noc+ "',pass='"+doc.pass+ "',release_request='"+doc.release_request+ "',molasses_allotment='"+doc.molasses_allotment+"',  lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "',  user_id='" + doc.user_id + "',permit='"+doc.permit+"' where party_code='"+doc.party_code+"'");
                        cmd = new NpgsqlCommand(str.ToString(), cn);
                        n = cmd.ExecuteNonQuery();
                    }
                    if (n == 1)
                    {
                        VAL = "0";
                    }
                    else
                        VAL = "1";
                    cn.Close();
                    _log.Info("Insert DocFromat Master Success :" + n);
                }
                catch (Exception ex1)
                {
                    _log.Info("Insert DocFromat Master Success :" + ex1.Message);
                    VAL = ex1.Message;
                }

            }
            return VAL;
        }
        public static string GetDoc(string party_code,string report_type)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select " + report_type + " from exciseautomation.document_format_master where party_code='" + party_code + "'", cn);
                    VAL =cmd.ExecuteScalar().ToString();
                    
                    cn.Close();
                    _log.Info("Insert DocFromat Master Success :" + VAL);
                }
                catch (Exception ex1)
                {
                    _log.Info("Insert DocFromat Master Success :" + ex1.Message);
                    VAL = ex1.Message;
                }

            }
            return VAL;
        }

        
        public static List<DocumentFormats> GetDocReportList()
        {
            List<DocumentFormats> docs = new List<DocumentFormats>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name  from exciseautomation.document_format_master a inner join exciseautomation.party_master b on a.party_code=b.party_code order by a.document_format_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            docs = new List<DocumentFormats>();
                            while (dr.Read())
                            {
                                DocumentFormats record = new DocumentFormats();
                                record.party_code = dr["party_code"].ToString();
                                record.party_name = dr["party_name"].ToString();
                                record.noc = dr["noc"].ToString();
                                record.pass = dr["pass"].ToString();
                                record.release_request = dr["release_request"].ToString();
                                record.molasses_allotment = dr["molasses_allotment"].ToString();
                                record.permit = dr["permit"].ToString();
                                docs.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get DocFromat Master List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get DocFromat Master List Success :" + ex.Message);
                }

            }
            return docs;
        }

        public static List<DocumentFormats> Search(string tablename, string column, string value)
        {
            List<DocumentFormats> docs = new List<DocumentFormats>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name,b.party_code  from exciseautomation.document_format_master a inner join exciseautomation.party_master b on a.party_code=b.party_code where b." + column + " Ilike '%" + value + "%'  order by  a.document_format_id desc", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                          docs= new List<DocumentFormats>();
                            while (dr.Read())
                            {
                                DocumentFormats record = new DocumentFormats();
                                record.party_code = dr["party_code"].ToString();
                                record.party_name = dr["party_name"].ToString();
                                record.noc = dr["noc"].ToString();
                                record.pass = dr["pass"].ToString();
                                record.release_request = dr["release_request"].ToString();
                                record.molasses_allotment = dr["molasses_allotment"].ToString();
                                record.permit = dr["permit"].ToString();
                                docs.Add(record);


                            }
                        }
                    }
                    cn.Close();

                }
                catch (Exception ex)
                {
                    throw (ex);
                }

            }
            return docs;
        }

        public static string Insert(DispatchType dtype)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select case when max(dispatch_type_id) is null then 0 else max(dispatch_type_id) end as dispatch_type_id from exciseautomation.dispatch_type_master", cn);
                    int n = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                    StringBuilder str = new StringBuilder();
                    str.Append("INSERT INTO exciseautomation.dispatch_type_master(dispatch_type_id, dispatch_type_code, dispatch_type_name, creation_date,  user_id)");
                    str.Append("VALUES('" + n + "','" + dtype.dispatch_type_code + "','" + dtype.dispatch_type_name + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + dtype.user_id + "')");
                    cmd = new NpgsqlCommand(str.ToString(), cn);
                    n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        VAL = "0";
                    }
                    else
                        VAL = "1";
                    cn.Close();
                    _log.Info("Insert Party Master Success :" +n );
                }
                catch (Exception ex1)
                {
                    _log.Info("Insert Party Master Success :" + ex1.Message);
                    VAL = ex1.Message;
                }

            }
            return VAL;
        }
        public static string Update(DispatchType dtype)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("Update exciseautomation.dispatch_type_master set  dispatch_type_name='" + dtype.dispatch_type_name + "',lastmodified_date='"+DateTime.Now.ToString("dd-MM-yyyy")+"',user_id='"+dtype.user_id+"' where dispatch_type_id='" + dtype.dispatch_type_id + "' ");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        VAL = "0";
                    }
                    else
                        VAL = "1";
                    cn.Close();
                    _log.Info("Update Party Master Success :" + dtype.dispatch_type_id + "-" + dtype.dispatch_type_name);
                }
                catch (Exception ex1)
                {
                    _log.Info("Update Party Master Success :" + dtype.dispatch_type_id + "-" + dtype.dispatch_type_name + "-" + ex1.Message);
                    VAL = ex1.Message;
                }

            }
            return VAL;
        }
    }
}
