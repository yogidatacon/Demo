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
    public class DL_VATTransfers_
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static string Update(VATTransfers_ vat)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    StringBuilder str = new StringBuilder();
                    str.Append("update exciseautomation.vat_transfer set vat_type_code='" + vat.vat_type_code + "', from_vat='" + vat.from_vat + "', transferqty='" + vat.transferqty + "', dips='" + vat.dips + "', temperature='" + vat.temperature + "', indication='" + vat.indication + "', strength='" + vat.strength + "', to_vat='" + vat.to_vat + "', lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "', user_id='" + vat.user_id + "', record_status='" + vat.record_status + "',vat_transfer_date='"+vat.transfered_date+"', remarks='"+vat.remarks+ "',lp_transferqty='"+vat.lp_transferqty+"' where  vat_transfer_id='" + vat.vat_transfer_id + "' and financial_year='"+vat.financial_year+"'");
                  
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        VAL = "0";
                    }
                    else
                        VAL = "1";
                    cn.Close();
                    _log.Info("Update vat_transfer Success :" + vat.from_vat + "-" + vat.to_vat+"-" + vat.vat_transfer_id);
                }
                catch (Exception ex1)
                {
                    _log.Info("Update vat_transfer Success :" + vat.from_vat + " - " + vat.to_vat +"-" + vat.vat_transfer_id+ " - " + ex1.Message);
                    VAL = ex1.Message;
                }

            }
            return VAL;
        }

        public static string Approve(VATTransfers_ vat)
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
                    str.Append("update exciseautomation.vat_transfer set   record_status='" + vat.record_status+ "' where  vat_transfer_id='" + vat.vat_transfer_id + "' and financial_year='" + vat.financial_year + "'");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (vat.record_status == "A")
                    {
                        vat.record_status = "Approved By Bond Officer";
                        NpgsqlCommand cmd9 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" + vat.from_vat + "' and party_code='" +vat.partyname + "'", cn);
                        double k = Convert.ToDouble(cmd9.ExecuteScalar());
                        double v = Convert.ToDouble(k) - Convert.ToDouble(vat.transferqty);
                        NpgsqlCommand cmd10 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + v + "'  where vat_code='" +vat.from_vat + "' and party_code='" + vat.partyname + "'", cn);
                        cmd10.ExecuteNonQuery();
                        NpgsqlCommand cmd19 = new NpgsqlCommand("SELECT vat_availablecapacity FROM exciseautomation.vat_master where vat_code='" +vat.to_vat + "' and party_code='" +vat.partyname + "'", cn);
                        double k1 = Convert.ToDouble(cmd19.ExecuteScalar());
                        double v1 = Convert.ToDouble(k1) + Convert.ToDouble(vat.transferqty);
                        NpgsqlCommand cmd11 = new NpgsqlCommand("update exciseautomation.vat_master set vat_availablecapacity ='" + v1 + "'  where vat_code='" +vat.to_vat + "' and party_code='" +vat.partyname + "'", cn);
                        cmd11.ExecuteNonQuery();
                    }
                    else
                    {
                        vat.record_status = "Rejected By Bond Officer";
                    }
                    str = new StringBuilder();
                    str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                    str.Append("'" + vat.vat_transfer_id+ "','','" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "','VTV','" +vat.record_status + "','" +vat.remarks + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + vat.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" +vat.user_id+ "','"+vat.financial_year+"','"+vat.partyname+"')");
                    cmd = new NpgsqlCommand(str.ToString(), cn);
                    n = cmd.ExecuteNonQuery();
                    VAL = "0";
                    _log.Info("vat_transfer  " +vat.record_status + " Sucess:" + vat.vat_transfer_id );
                    trn.Commit();
                    cn.Close();
                }
                catch (Exception ex1)
                {
                    _log.Info("vat_transfer Success : " + vat.record_status + " Sucess:" + vat.vat_transfer_id + " - " + ex1.Message);
                    VAL = ex1.Message;
                }

            }
            return VAL;
        }

        public static List<VATTransfers_> GetList(string party_code,string party)
        {
            List<VATTransfers_> transfers = new List<VATTransfers_>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    StringBuilder str = new StringBuilder();
                    if(party_code=="ALL"||party_code==null)
                    {
                        str.Append("Select a.*,b.vat_name as from_vatname,d.vat_name as to_vatname,e.vat_type_name from exciseautomation.vat_transfer a inner join exciseautomation.vat_master b on a.from_vat = b.vat_code inner join exciseautomation.party_master c  on b.party_code = c.party_code left join exciseautomation.vat_master d on  a.to_vat = d.vat_code inner join exciseautomation.vat_type_master e on e.vat_type_code=a.vat_type_code inner join exciseautomation.view_checkuser f on a.user_id=f.user_id where f.party_type_name='"+party+ "' and a.record_active='true'  order by a.vat_transfer_date desc ");
                    }
                    else
                    {
                        str.Append("Select a.*,b.vat_name as from_vatname,d.vat_name as to_vatname,e.vat_type_name from exciseautomation.vat_transfer a inner join exciseautomation.vat_master b on a.from_vat = b.vat_code inner join exciseautomation.party_master c  on b.party_code = c.party_code left join exciseautomation.vat_master d on  a.to_vat = d.vat_code inner join exciseautomation.vat_type_master e on e.vat_type_code=a.vat_type_code where b.party_code='" + party_code + "' and a.record_active='true' order by a.vat_transfer_date desc ");

                    }

                    using (NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            transfers = new List<VATTransfers_>();
                            while (dr.Read())
                            {
                                VATTransfers_ trans = new VATTransfers_();
                                trans.vat_type_code = dr["vat_type_code"].ToString();  
                                trans.vat_type_name = dr["vat_type_name"].ToString();
                               trans.transfered_date = Convert.ToDateTime( dr["vat_transfer_date"].ToString()).ToString("dd-MM-yyyy");
                                trans.from_vat = dr["from_vat"].ToString();
                                trans.from_vatname = dr["from_vatname"].ToString();
                                trans.financial_year = dr["financial_year"].ToString();
                                trans.to_vat = dr["to_vat"].ToString();
                                trans.to_vatname = dr["to_vatname"].ToString();
                                trans.dips =Convert.ToDouble(dr["dips"].ToString());
                                trans.temperature = Convert.ToDouble(dr["temperature"].ToString());
                                trans.indication = Convert.ToDouble(dr["indication"].ToString());
                                trans.strength = Convert.ToDouble(dr["strength"].ToString());
                                trans.transferqty= Convert.ToDouble(dr["transferqty"].ToString());
                                trans.remarks = dr["remarks"].ToString();
                                trans.record_status = dr["record_status"].ToString();
                                if(dr["vat_transfer_id"].ToString()!="")
                                trans.vat_transfer_id =Convert.ToInt32( dr["vat_transfer_id"].ToString());
                                transfers.Add(trans);
                            }
                        }
                    }
                }
                catch (Exception ex1)
                {
                    // _log.Info("Insert Party Master Success :" + vat.from_vat + " - " + vat.to_vat + " - " + ex1.Message);

                }

            }
            return transfers;
        }

        public static List<VATTransfers_> Search(string tablename, string column, string value, string party_code, string party)
        {
            List<VATTransfers_> transfers = new List<VATTransfers_>();

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    StringBuilder str = new StringBuilder();
                    if (party_code == "ALL" || party_code == null)
                    {
                        if(column=="from_vatname")
                        {
                            str.Append("Select a.*,b.vat_name as from_vatname,d.vat_name as to_vatname,e.vat_type_name from exciseautomation.vat_transfer a inner join exciseautomation.vat_master b on a.from_vat = b.vat_code inner join exciseautomation.party_master c  on b.party_code = c.party_code left join exciseautomation.vat_master d on  a.to_vat = d.vat_code inner join exciseautomation.vat_type_master e on e.vat_type_code=a.vat_type_code inner join exciseautomation.view_checkuser f on a.user_id=f.user_id where    b.vat_name Ilike '%" + value + "%' and a.record_active='true'  order by a.vat_transfer_date desc ");

                        }
                        else if (column == "to_vatname")
                        {
                            str.Append("Select a.*,b.vat_name as from_vatname,d.vat_name as to_vatname,e.vat_type_name from exciseautomation.vat_transfer a inner join exciseautomation.vat_master b on a.from_vat = b.vat_code inner join exciseautomation.party_master c  on b.party_code = c.party_code left join exciseautomation.vat_master d on  a.to_vat = d.vat_code inner join exciseautomation.vat_type_master e on e.vat_type_code=a.vat_type_code inner join exciseautomation.view_checkuser f on a.user_id=f.user_id where  d.vat_name Ilike '%" + value + "%' and a.record_active='true'  order by a.vat_transfer_date desc ");

                        }
                        else
                        {
                            str.Append("Select a.*,b.vat_name as from_vatname,d.vat_name as to_vatname,e.vat_type_name from exciseautomation.vat_transfer a inner join exciseautomation.vat_master b on a.from_vat = b.vat_code inner join exciseautomation.party_master c  on b.party_code = c.party_code left join exciseautomation.vat_master d on  a.to_vat = d.vat_code inner join exciseautomation.vat_type_master e on e.vat_type_code=a.vat_type_code inner join exciseautomation.view_checkuser f on a.user_id=f.user_id where   " + column + " Ilike '%" + value + "%' and a.record_active='true' order by a.vat_transfer_date desc ");

                        }

                    }
                    else
                    {
                        if (column == "from_vatname")
                        {
                            str.Append("Select a.*,b.vat_name as from_vatname,d.vat_name as to_vatname,e.vat_type_name from exciseautomation.vat_transfer a inner join exciseautomation.vat_master b on a.from_vat = b.vat_code inner join exciseautomation.party_master c  on b.party_code = c.party_code left join exciseautomation.vat_master d on  a.to_vat = d.vat_code inner join exciseautomation.vat_type_master e on e.vat_type_code=a.vat_type_code where b.party_code='" + party_code + "' and   b.vat_name Ilike '%" + value + "%' and a.record_active='true' order by a.vat_transfer_date desc ");

                        }
                        else if (column == "to_vatname")
                        {
                            str.Append("Select a.*,b.vat_name as from_vatname,d.vat_name as to_vatname,e.vat_type_name from exciseautomation.vat_transfer a inner join exciseautomation.vat_master b on a.from_vat = b.vat_code inner join exciseautomation.party_master c  on b.party_code = c.party_code left join exciseautomation.vat_master d on  a.to_vat = d.vat_code inner join exciseautomation.vat_type_master e on e.vat_type_code=a.vat_type_code where b.party_code='" + party_code + "' and  d.vat_name Ilike '%" + value + "%' and a.record_active='true' order by a.vat_transfer_date desc ");

                        }
                        else
                        {
                            str.Append("Select a.*,b.vat_name as from_vatname,d.vat_name as to_vatname,e.vat_type_name from exciseautomation.vat_transfer a inner join exciseautomation.vat_master b on a.from_vat = b.vat_code inner join exciseautomation.party_master c  on b.party_code = c.party_code left join exciseautomation.vat_master d on  a.to_vat = d.vat_code inner join exciseautomation.vat_type_master e on e.vat_type_code=a.vat_type_code where b.party_code='" + party_code + "' and   " + column + " Ilike '%" + value + "%' and a.record_active='true' order by a.vat_transfer_date desc ");

                        }
                    }

                    using (NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            transfers = new List<VATTransfers_>();
                            while (dr.Read())
                            {
                                VATTransfers_ trans = new VATTransfers_();
                                trans.vat_type_code = dr["vat_type_code"].ToString();
                                trans.vat_type_name = dr["vat_type_name"].ToString();
                                trans.transfered_date = Convert.ToDateTime(dr["vat_transfer_date"].ToString()).ToString("dd-MM-yyyy");
                                trans.from_vat = dr["from_vat"].ToString();
                                trans.financial_year = dr["financial_year"].ToString();
                                trans.from_vatname = dr["from_vatname"].ToString();
                                trans.to_vat = dr["to_vat"].ToString();
                                trans.to_vatname = dr["to_vatname"].ToString();
                                trans.dips = Convert.ToDouble(dr["dips"].ToString());
                                trans.temperature = Convert.ToDouble(dr["temperature"].ToString());
                                trans.indication = Convert.ToDouble(dr["indication"].ToString());
                                trans.strength = Convert.ToDouble(dr["strength"].ToString());
                                trans.transferqty = Convert.ToDouble(dr["transferqty"].ToString());
                                trans.remarks = dr["remarks"].ToString();
                                trans.record_status = dr["record_status"].ToString();
                                trans.vat_transfer_id = Convert.ToInt32(dr["vat_transfer_id"].ToString());
                                transfers.Add(trans);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return transfers;
        }



        public static string Insert(VATTransfers_ vat)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT max(vat_transfer_id) FROM exciseautomation.vat_transfer ", cn);
                    string m = cmd1.ExecuteScalar().ToString();
                    int p = 0;
                    if (m == "")
                        p= 1;
                    else
                        p = Convert.ToInt32(m) + 1;
                    StringBuilder str = new StringBuilder();
                    str.Append("INSERT INTO exciseautomation.vat_transfer(vat_transfer_id,vat_type_code, from_vat, transferqty, dips, temperature, indication, strength, to_vat, lastmodified_date, user_id, creation_date, record_status,vat_transfer_date, remarks,lp_transferqty,party_code,financial_year)Values(");
                    str.Append("'"+p+"','" + vat.vat_type_code + "','" + vat.from_vat + "','" + vat.transferqty + "','" + vat.dips + "','" + vat.temperature + "','" + vat.indication + "','" + vat.strength + "','" + vat.to_vat + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + vat.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + vat.record_status + "','"+vat.transfered_date+"','"+vat.remarks+"','"+vat.lp_transferqty+"','"+vat.party_code+"','"+vat.financial_year+"')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        VAL = "0";
                    }
                    else
                        VAL = "1";
                    cn.Close();
                    _log.Info("Insert Party Master Success :" + vat.from_vat + "-" + vat.to_vat);
                }
                catch (Exception ex1)
                {
                    _log.Info("Insert Party Master Success :" + vat.from_vat + " - " + vat.to_vat + " - " + ex1.Message);
                    VAL = ex1.Message;
                }

            }
            return VAL;
        }
    }
}
