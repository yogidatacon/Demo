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
   public class DL_VATMaster
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static List<VAT_Master> GetvatmasterList(string userid)
        {
            List<VAT_Master> vats = new List<VAT_Master>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    if (userid == "Admin"|| userid=="ALL")
                        cmd = new NpgsqlCommand("select  a.*,b.party_name,c.vat_type_name,d.uom_name,e.product_name from exciseautomation.vat_master a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_type_master c on a.vat_type_code=c.vat_type_code inner join exciseautomation.uom_master d on a.uom_code=d.uom_code inner join exciseautomation.product_master e on e.product_code=a.storage_content  order by a.party_code,a.vat_type_code,vat_code,vat_name", cn);
                    else
                        cmd = new NpgsqlCommand("select  a.*,b.party_name,c.vat_type_name,d.uom_name,e.product_name from exciseautomation.vat_master a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_type_master c on a.vat_type_code=c.vat_type_code inner join exciseautomation.uom_master d on a.uom_code=d.uom_code inner join exciseautomation.product_master e on e.product_code=a.storage_content  where a.party_code='" + userid + "' order by a.party_code,a.vat_type_code,vat_code,vat_name", cn);

                    cmd.CommandType = System.Data.CommandType.Text;

                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            vats = new List<VAT_Master>();
                            while (dr.Read())
                            {
                                VAT_Master vat = new VAT_Master();
                                vat.vat_code = dr["vat_code"].ToString();
                                vat.vat_name = dr["vat_name"].ToString();
                                vat.org_id =1;
                                vat.party_code = dr["party_code"].ToString();
                                vat.uom_code = dr["uom_code"].ToString();
                                vat.vat_availablecapacity = Convert.ToDouble(dr["vat_availablecapacity"].ToString());
                                vat.content=dr["storage_content"].ToString();
                                vat.product_name = dr["product_name"].ToString();
                                vat.vat_depth= Convert.ToDouble(dr["vat_depth"].ToString());
                                vat.vat_totalcapacity= Convert.ToDouble(dr["vat_totalcapacity"].ToString());
                                vat.vat_type_code = dr["vat_type_code"].ToString();
                                vat.product_type_code = dr["vat_type_code"].ToString();
                                vat.party_type_code = dr["party_type_code"].ToString();
                                vat.party_name = dr["party_name"].ToString();
                                vat.vat_type_name = dr["vat_type_name"].ToString();
                                vat.uom_name = dr["uom_name"].ToString();
                                vats.Add(vat);
                            }
                            _log.Info("Get VAT Master List Success");
                        }
                   
                }
                catch (Exception ex)
                {
                   // _log.Info("Get VAT Master List Fail :"+ex.Message);
                }
            }
            return vats;
        }


        public static List<VAT_Master> GetGrainvatmasterList(string userid)
        {
            List<VAT_Master> vats = new List<VAT_Master>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    if (userid == "Admin")
                        cmd = new NpgsqlCommand("select  a.*,b.party_name,c.vat_type_name,d.uom_name,e.product_name from exciseautomation.vat_master a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_type_master c on a.vat_type_code=c.vat_type_code inner join exciseautomation.uom_master d on a.uom_code=d.uom_code inner join exciseautomation.product_master e on e.product_code=a.storage_content inner join exciseautomation.rawmaterial_master f on a.product_type_code=f.product_type_code  order by a.party_code,a.vat_type_code,vat_code,vat_name", cn);
                    else
                        cmd = new NpgsqlCommand("select  a.*,b.party_name,c.vat_type_name,d.uom_name,e.product_name from exciseautomation.vat_master a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_type_master c on a.vat_type_code=c.vat_type_code inner join exciseautomation.uom_master d on a.uom_code=d.uom_code inner join exciseautomation.product_master e on e.product_code=a.storage_content inner join exciseautomation.rawmaterial_master f on a.product_type_code=f.product_type_code   order by a.party_code,a.vat_type_code,vat_code,vat_name", cn);

                    cmd.CommandType = System.Data.CommandType.Text;

                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        vats = new List<VAT_Master>();
                        while (dr.Read())
                        {
                            VAT_Master vat = new VAT_Master();
                            vat.vat_code = dr["vat_code"].ToString();
                            vat.vat_name = dr["vat_name"].ToString();
                            vat.org_id = 1;
                            vat.party_code = dr["party_code"].ToString();
                            vat.uom_code = dr["uom_code"].ToString();
                            vat.vat_availablecapacity = Convert.ToDouble(dr["vat_availablecapacity"].ToString());
                            vat.content = dr["storage_content"].ToString();
                            vat.product_name = dr["product_name"].ToString();
                            vat.vat_depth = Convert.ToDouble(dr["vat_depth"].ToString());
                            vat.vat_totalcapacity = Convert.ToDouble(dr["vat_totalcapacity"].ToString());
                            vat.vat_type_code = dr["vat_type_code"].ToString();
                            vat.product_type_code = dr["vat_type_code"].ToString();
                            vat.party_type_code = dr["party_type_code"].ToString();
                            vat.party_name = dr["party_name"].ToString();
                            vat.vat_type_name = dr["vat_type_name"].ToString();
                            vat.uom_name = dr["uom_name"].ToString();
                            vats.Add(vat);
                        }
                        _log.Info("Get VAT Master List Success");
                    }

                }
                catch (Exception ex)
                {
                    // _log.Info("Get VAT Master List Fail :"+ex.Message);
                }
            }
            return vats;
        }

        public static List<VAT_Master> SearchVATMaster(string tablename, string column, string value)
        {
            List<VAT_Master> vats = new List<VAT_Master>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                   
                        cmd = new NpgsqlCommand("select  a.*,b.party_name,c.vat_type_name,d.uom_name,e.product_name from exciseautomation.vat_master a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.vat_type_master c on a.vat_type_code=c.vat_type_code inner join exciseautomation.uom_master d on a.uom_code=d.uom_code inner join exciseautomation.product_master e on e.product_code=a.storage_content where " + column + " Ilike '%" + value + "%'  order by a.party_code,a.vat_type_code,vat_code,vat_name", cn);
                  
                    cmd.CommandType = System.Data.CommandType.Text;

                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        vats = new List<VAT_Master>();
                        while (dr.Read())
                        {
                            VAT_Master vat = new VAT_Master();
                            vat.vat_code = dr["vat_code"].ToString();
                            vat.vat_name = dr["vat_name"].ToString();
                            vat.org_id = 1;
                            vat.party_code = dr["party_code"].ToString();
                            vat.uom_code = dr["uom_code"].ToString();
                            vat.vat_availablecapacity = Convert.ToDouble(dr["vat_availablecapacity"].ToString());
                            vat.content = dr["storage_content"].ToString();
                            vat.product_name = dr["product_name"].ToString();
                            vat.vat_depth = Convert.ToDouble(dr["vat_depth"].ToString());
                            vat.vat_totalcapacity = Convert.ToDouble(dr["vat_totalcapacity"].ToString());
                            vat.vat_type_code = dr["vat_type_code"].ToString();
                            vat.product_type_code = dr["vat_type_code"].ToString();
                            vat.party_type_code = dr["party_type_code"].ToString();
                            vat.party_name = dr["party_name"].ToString();
                            vat.vat_type_name = dr["vat_type_name"].ToString();
                            vat.uom_name = dr["uom_name"].ToString();
                            vats.Add(vat);
                        }
                        _log.Info("Get VAT Master List Success");
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Get VAT Master List Fail :" + ex.Message);
                }
            }
            return vats;
        }

        public static string Insert(VAT_Master vat)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd1 = new NpgsqlCommand("Select Max(vat_seqno)  from exciseautomation.vat_master where party_code='" + vat.party_code+"'", cn);
                    string val = cmd1.ExecuteScalar().ToString();
                    int max;
                    if (val == "")
                        max = 1;
                    else
                        max =Convert.ToInt32(val)+1;
                    vat.vat_code =vat.party_code+vat.vat_type_code+String.Format("{0:0000}",max);
                    StringBuilder str = new StringBuilder();
                    
                        str.Append("INSERT INTO exciseautomation.vat_master(vat_seqno,vat_code, vat_name, vat_type_code, party_code, vat_availablecapacity, vat_totalcapacity, org_id, storage_content, vat_depth, uom_code,lastmodified_date, user_id,party_type_code,product_type_code)");
                        str.Append("VALUES('" + max + "','"+vat.vat_code+"','" + vat.vat_name+ "','" + vat.vat_type_code + "','" + vat.party_code + "','0','" + vat.vat_totalcapacity + "','" + vat.org_id + "','" + vat.content + "','" + vat.vat_depth + "','" + vat.uom_code + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + vat.user_id + "','"+vat.party_type_code+"','"+vat.product_type_code+"')");
                   
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        _log.Info("Insert VAT Master Success :" + vat.vat_code + "-" + vat.vat_name);
                        VAL = "0";
                    }
                    else
                    {
                        VAL = "1";
                        _log.Info("Insert VAT Master Fail :" + vat.vat_code + "-" + vat.vat_name);
                    }
                    trn.Commit();
                    cn.Close();

                }
                catch (Exception ex1)
                {
                    _log.Info("Insert VAT Master Fail :" + vat.vat_code + "-" + vat.vat_name+"-"+ex1.Message);
                    VAL = ex1.Message;
                    trn.Rollback();
                }

            }
            return VAL;
        }
        public static string Update(VAT_Master vat)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    StringBuilder str = new StringBuilder();
                    
                    str.Append("Update exciseautomation.vat_master set  vat_name='" + vat.vat_name + "', vat_type_code='" + vat.vat_type_code + "',storage_content='"+vat.content+"',  party_code='" + vat.party_code + "',  org_id='" + vat.org_id + "',vat_totalcapacity='" + vat.vat_totalcapacity + "', vat_depth='" + vat.vat_depth + "',uom_code='"+vat.uom_code+"',  user_id='" + vat.user_id + "', lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "',party_type_code='"+vat.party_type_code+"',product_type_code='"+vat.product_type_code+"' where vat_code='" + vat.vat_code + "' ");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        _log.Info("Update VAT Master Success :" + vat.vat_code + "-" + vat.vat_name);
                        VAL = "0";
                    }
                    else
                    {
                        VAL = "1";
                        _log.Info("Update VAT Master Fail :" + vat.vat_code + "-" + vat.vat_name);
                    }
                    trn.Commit();
                    cn.Close();

                }
                catch (Exception ex1)
                {
                    _log.Info("Update VAT Master Fail :" + vat.vat_code + "-" + vat.vat_name + "-" + ex1.Message);
                    VAL = ex1.Message;
                    trn.Rollback();
                }

            }
            return VAL;
        }
      
        public static VAT_Master GetVATDetails(string vat_code)
        {

            VAT_Master vat = new VAT_Master();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.vat_master where vat_code='" + vat_code + "' ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {

                              
                                vat.vat_code = dr["vat_code"].ToString();
                                vat.vat_name = dr["vat_name"].ToString();
                                vat.org_id = Convert.ToInt32(dr["org_id"].ToString());
                                vat.party_code = dr["party_code"].ToString();
                                vat.uom_code = dr["uom_code"].ToString();
                                vat.vat_availablecapacity = Convert.ToDouble(dr["vat_availablecapacity"].ToString());
                                vat.content = dr["storage_content"].ToString();
                                vat.vat_depth = Convert.ToDouble(dr["vat_depth"].ToString());
                                vat.vat_totalcapacity = Convert.ToDouble(dr["vat_totalcapacity"].ToString());
                                vat.vat_type_code = dr["vat_type_code"].ToString();
                                vat.product_type_code = dr["product_type_code"].ToString();
                                vat.party_type_code = dr["party_type_code"].ToString();
                                vat.party_name = dr["party_name"].ToString();
                                vat.vat_type_name = dr["vat_type_name"].ToString();
                                vat.uom_name = dr["uom_name"].ToString();
                               

                            }
                            _log.Info("Get VAT Details Success");
                        }
                    }
                    cn.Close();
                }
                catch (Exception ex)
                {
                    _log.Info("Get VAT Details Fail :"+ex.Message);
                }

            }
            return vat;
        }
    }
}
