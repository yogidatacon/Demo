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
   public class DL_Party_Master
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static string Insert(Party_Master partytype)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    
                    StringBuilder str = new StringBuilder();
                    if (partytype.party_captive_unit_name != "")
                    {
                        str.Append("INSERT INTO exciseautomation.party_master(party_code, party_name, party_address, party_type_code, district_code,  org_id, party_license_no, mobile_no,party_captive,  party_captive_unit_name, creation_date,  user_id,tan, pan, tin, gst,party_email,financial_year,party_active,grain_based_distillery)");
                        str.Append("VALUES('" + partytype.party_code + "','" + partytype.party_name + "','" + partytype.party_address + "','" + partytype.party_type_code + "','" + partytype.district_code + "','" + partytype.org_id + "','" + partytype.party_license_no + "','" + partytype.mobile_no + "','" + partytype.party_captive + "','" + partytype.party_captive_unit_name + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + partytype.user_id + "','"+partytype.tan+"','"+partytype.pan+"','"+partytype.tin+"','"+partytype.gst+"','"+partytype.email_id+ "','" + partytype.financialyear + "','" + partytype.party_active + "','"+partytype.isgrainbased+"')");
                    }
                    else
                    {
                        str.Append("INSERT INTO exciseautomation.party_master(party_code, party_name, party_address, party_type_code, district_code,  org_id, party_license_no, mobile_no, party_captive,  creation_date,  user_id,tan, pan, tin, gst,party_email,financial_year,party_active,grain_based_distillery)");
                        str.Append("VALUES('" + partytype.party_code + "','" + partytype.party_name + "','" + partytype.party_address + "','" + partytype.party_type_code + "','" + partytype.district_code + "','" + partytype.org_id + "','" + partytype.party_license_no + "','" + partytype.mobile_no + "','" + partytype.party_captive + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + partytype.user_id + "','" + partytype.tan + "','" + partytype.pan + "','" + partytype.tin + "','" + partytype.gst + "','" + partytype.email_id + "','" + partytype.financialyear + "','" + partytype.party_active + "','"+partytype.isgrainbased+"')");
                    }
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        VAL = "0";
                    }
                    else
                        VAL = "1";
                    cn.Close();
                    _log.Info("Insert Party Master Success :"+partytype.party_code+"-"+partytype.party_name);
                }
                catch (Exception ex1)
                {
                    _log.Info("Insert Party Master Success :" + partytype.party_code + "-" + partytype.party_name+"-"+ex1.Message);
                    VAL = ex1.Message;
                }

            }
            return VAL;
        }

        public static List<Party_Master> GetProduct_Party()
        {
            List<Party_Master> partylist = new List<Party_Master>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select distinct a.Party_name,a.Party_code,a.party_type_code,c.product_code,c.product_name from exciseautomation.party_master a inner join exciseautomation.Vat_master b on a.party_code=b.party_code  inner join exciseautomation.product_master c on b.storage_content=c.product_code where a.party_active is true order by a.party_name", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            partylist = new List<Party_Master>();
                            while (dr.Read())
                            {
                                Party_Master record = new Party_Master();
                                record.party_name = dr["Party_name"].ToString();
                                record.party_code = dr["Party_code"].ToString();
                                record.product_code = dr["product_code"].ToString();
                                record.product_name = dr["product_name"].ToString();
                                record.party_type_code = dr["party_type_code"].ToString();
                                partylist.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Party Master List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Party Master List Success :" + ex.Message);
                }

            }
            return partylist;
        }

        public static string Update(Party_Master party)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    StringBuilder str = new StringBuilder();
                    if (party.party_captive_unit_name != "")
                    {
                        str.Append("Update exciseautomation.party_master set  party_name='" + party.party_name + "', party_address='" + party.party_address + "', party_type_code='" + party.party_type_code + "', district_code='" + party.district_code + "',  org_id='" + party.org_id + "', party_license_no='" + party.party_license_no + "', mobile_no='" + party.mobile_no + "', party_captive='" + party.party_captive + "', party_captive_unit_name='" + party.party_captive_unit_name + "', user_id='" + party.user_id + "', lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "',tan='"+party.tan+"',tin='"+party.tin+"',pan='"+party.pan+"',gst='"+party.gst+ "',party_email='" + party.email_id + "',financial_year='"+party.financialyear+"',party_active='"+party.party_active+ "',grain_based_distillery='"+party.isgrainbased+"' where party_code='" + party.party_code + "' ");
                    }
                    else
                    {
                        str.Append("Update exciseautomation.party_master set  party_name='" + party.party_name + "', party_address='" + party.party_address + "', party_type_code='" + party.party_type_code + "', district_code='" + party.district_code + "',  org_id='" + party.org_id + "', party_license_no='" + party.party_license_no + "', mobile_no='" + party.mobile_no + "', party_captive='" + party.party_captive + "',  user_id='" + party.user_id + "', lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "',tan='" + party.tan + "',tin='" + party.tin + "',pan='" + party.pan + "',gst='" + party.gst + "',party_email='" + party.email_id + "',financial_year='" + party.financialyear + "',party_active='" + party.party_active + "',grain_based_distillery='" + party.isgrainbased + "' where party_code='" + party.party_code + "' ");
                    }
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        VAL = "0";
                    }
                    else
                        VAL = "1";
                    cn.Close();
                    _log.Info("Update Party Master Success :" + party.party_code + "-" + party.party_name);
                }
                catch (Exception ex1)
                {
                    _log.Info("Update Party Master Success :" + party.party_code + "-" + party.party_name+"-"+ ex1.Message);
                    VAL = ex1.Message;
                }

            }
            return VAL;
        }
        public static List<Party_Master> GetList()
        {

            List<Party_Master> partylist = new List<Party_Master>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select distinct a.*,b.party_type_name,c.financial_year from exciseautomation.party_master a inner join exciseautomation.party_type_master b on a.party_type_code=b.party_type_code left join exciseautomation.party_financial_yr c on b.party_type_code=c.party_type_code  order by a.party_name", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            partylist = new List<Party_Master>();
                            while (dr.Read())
                            {
                                Party_Master record = new Party_Master();
                                record.party_name = dr["Party_name"].ToString();
                                record.party_code = dr["Party_code"].ToString();
                                record.party_type_code = dr["party_type_code"].ToString();
                                if(dr["party_active"].ToString()=="True")
                                record.party_active ="Active";
                                        else
                                    record.party_active = "InActive";
                                record.party_address = dr["party_address"].ToString();
                                record.district_code = dr["district_code"].ToString();
                                record.party_type_name = dr["party_type_name"].ToString();
                                record.financialyear = dr["financial_year"].ToString();
                                record.party_captive_unit_name = dr["party_captive_unit_name"].ToString();
                                record.party_captive = dr["party_captive"].ToString();
                                record.record_status = dr["approver_level"].ToString();
                                if (dr["lastmodified_date"].ToString()!="")
                                record.lastmodified_date = dr["lastmodified_date"].ToString().Substring(0, 10).Replace("/", "-");///Convert.ToDateTime( dr["lastmodified_date"].ToString()).ToString("dd-MM-yyyy");
                                if (dr["enddate"].ToString() != "")
                                    record.enddate = dr["enddate"].ToString().Substring(0, 10).Replace("/", "-");
                                partylist.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Party Master List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Party Master List Success :"+ex.Message);
                }

            }
            return partylist;
        }

        public static List<Party_Master> SearchParty(string tablename, string column, string value)
        {
            List<Party_Master> partylist = new List<Party_Master>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select distinct a.*,b.party_type_name,c.financial_year from exciseautomation.party_master a inner join exciseautomation.party_type_master b on a.party_type_code=b.party_type_code left join exciseautomation.party_financial_yr c on b.party_type_code=c.party_type_code where " + column + " Ilike '%" + value + "%'   order by a.party_name", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            partylist = new List<Party_Master>();
                            while (dr.Read())
                            {
                                Party_Master record = new Party_Master();
                                record.party_name = dr["Party_name"].ToString();
                                record.party_code = dr["Party_code"].ToString();
                                record.party_type_code = dr["party_type_code"].ToString();
                                if (dr["party_active"].ToString() == "True")
                                    record.party_active = "Active";
                                else
                                    record.party_active = "InActive";
                                record.party_address = dr["party_address"].ToString();
                                record.district_code = dr["district_code"].ToString();
                                record.party_type_name = dr["party_type_name"].ToString();
                                record.financialyear = dr["financial_year"].ToString();
                                record.party_captive_unit_name = dr["party_captive_unit_name"].ToString();
                                record.party_captive = dr["party_captive"].ToString();
                                if (dr["lastmodified_date"].ToString() != "")
                                record.lastmodified_date = dr["lastmodified_date"].ToString().Substring(0, 10).Replace("/", "-");
                                partylist.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Party Master List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Party Master List Success :" + ex.Message);
                }

            }
            return partylist;
        }

        public static Party_Master GetPartyDetails(string party_code)
        {

            Party_Master party = new Party_Master();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.party_master where party_code='"+party_code+"' ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                           
                            while (dr.Read())
                            {

                                party.party_name = dr["Party_name"].ToString();
                                party.party_code = dr["Party_code"].ToString();
                                party.party_type_code = dr["party_type_code"].ToString();
                                party.party_active = dr["party_active"].ToString();
                                party.party_address = dr["party_address"].ToString();
                                party.district_code = dr["district_code"].ToString();
                                party.party_license_no = dr["party_license_no"].ToString();
                                party.mobile_no = dr["mobile_no"].ToString();
                                party.party_active = dr["party_active"].ToString();
                                party.party_captive = dr["party_captive"].ToString();
                                party.party_captive_unit_name = dr["party_captive_unit_name"].ToString();
                                party.gst = dr["gst"].ToString();
                                party.tan = dr["tan"].ToString();
                                party.tin = dr["tin"].ToString();
                                party.pan = dr["pan"].ToString();
                                party.email_id = dr["party_email"].ToString();
                                party.financialyear = dr["financial_year"].ToString();
                                party.record_status = dr["approver_level"].ToString();
                                party.enddate = dr["enddate"].ToString();
                                if(dr["startdate"].ToString()!="")
                                party.startdate = dr["startdate"].ToString();
                                party.isgrainbased = dr["grain_based_distillery"].ToString();
                            }
                        }
                    }
                    cn.Close();
                    _log.Info("Get Party Master Details Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Party Master Details Success :"+ex.Message);
                }

            }
            return party;
        }


        public static string changefinancialyear(string tablename, string column, string value,string financialendate, string startdates)
        {
            string value1 = "";
            string value2 = "";
          
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
             
                try
                {
                    NpgsqlCommand cmd;
                    
                        OpeningBalance request = new OpeningBalance();
                        request.vats = DL_OpeningBalance.GetOpeningBalanceList(column, "");
                    NpgsqlCommand cmd2 = new NpgsqlCommand("select case when max(openingbalance_id) is null then 0 else max(openingbalance_id) end as openingbalance_id  from exciseautomation.openingbalance ", cn);
                    int max = Convert.ToInt32(cmd2.ExecuteScalar().ToString()) + 1;
                    List<Org_Finacial_yr> Org_Finacial = new List<Org_Finacial_yr>();
                    Org_Finacial = DL_Org_List.GetFinacListValues("");
                    var year = from s in Org_Finacial
                               where s.status == "Active"
                               select s;
                    NpgsqlCommand cmd3 = new NpgsqlCommand("update exciseautomation.party_master set financial_year='"+year.ToList()[0].financial_year+ "', lastmodified_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "',enddate='"+financialendate+ "',startdate='"+startdates+ "',approver_level='Y' where party_code = '" + column + "'", cn);
                    string re1 = cmd3.ExecuteNonQuery().ToString();

                    //NpgsqlCommand cmd4 = new NpgsqlCommand("select financial_year from exciseautomation." + tablename + "  where user_id Ilike '" + value + "%' and financial_year !='' and record_active='true'", cn);
                    //string fyear= cmd4.ExecuteScalar().ToString();
                   if(year.ToList()[0].financial_year != request.vats[0].financial_year)
                    {
                        if (tablename == "vat_master" )
                        {
                    
                            cmd = new NpgsqlCommand("update exciseautomation." + tablename + " set vat_availablecapacity = 0 where party_code= '" + column + "'", cn);
                        
                        }
                        else
                        {
                            //cmd = new NpgsqlCommand("update exciseautomation." + tablename + " set record_active='false' where user_id Ilike '" + value + "%'", cn);
                            cmd = new NpgsqlCommand("update exciseautomation." + tablename + " set record_active='false'  where party_code= '" + column + "'", cn);
                        }

                        string re = cmd.ExecuteNonQuery().ToString();
                    
                        if (tablename == "openingbalance")
                        {

                            for (int i = 0; i < request.vats.Count; i++)
                            {
                                if (year.ToList()[0].financial_year != request.vats[i].financial_year)
                                {
                                    if (Convert.ToDouble(request.vats[i].vat_availablecapacity) >= 0)
                                    {
                                        NpgsqlCommand cmd1 = new NpgsqlCommand("INSERT INTO exciseautomation.openingbalance(openingbalance_id, vat_code, uom_code, storagecontent, openingbalancevalue,openingbalanceyear,creation_date, user_id,record_status,remarks,party_code,financial_year)VALUES('" + max + "','" + request.vats[i].vat_code + "', '" + request.vats[i].uom_code + "', '" + request.vats[i].storage_content + "','" + request.vats[i].vat_availablecapacity + "','" + year.ToList()[0].financial_year.ToString() + "', '" + DateTime.Now.ToShortDateString() + "', '" + value + "','N','" + request.vats[i].remarks + "','"+request.vats[i].party_code+"','"+year.ToList()[0].financial_year+"')", cn);
                                        int n = Convert.ToInt32(cmd1.ExecuteNonQuery());
                                        max++;
                                        if (n == 0)
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        NpgsqlCommand cmd1 = new NpgsqlCommand("INSERT INTO exciseautomation.openingbalance(openingbalance_id, vat_code, uom_code, storagecontent, openingbalancevalue,openingbalanceyear,creation_date, user_id,record_status,remarks,party_code,financial_year)VALUES('" + max + "','" + request.vats[i].vat_code + "', '" + request.vats[i].uom_code + "', '" + request.vats[i].storage_content + "',0,'" + year.ToList()[0].financial_year.ToString() + "', '" + DateTime.Now.ToShortDateString() + "', '" + value + "','N','" + request.vats[i].remarks + "','" + request.vats[i].party_code + "','" + year.ToList()[0].financial_year + "')", cn);
                                        int n = Convert.ToInt32(cmd1.ExecuteNonQuery());
                                        max++;
                                        if (n == 0)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }


                        if (re == "1")
                        {
                            value1 = tablename + "............" + "Completed";
                            _log.Info("Get Existing data Success :" + tablename);
                        }
                        else
                        {
                            if (re != "")
                                value1 = tablename + "......." + "Completed";
                            _log.Info("Get Existing data Fail :" + tablename);
                        }
                   }
                    else
                    {
                       value1 = tablename + "......." + "Already Change Financial Year Done ";
                        _log.Info("Get Existing data Fail :" + tablename);
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Get Existing data Fail :" + tablename + "-" + ex.Message);
                    //value1 = "" + tablename + " - " + ex.Message;
                    value1 = "1" + tablename + " - " + ex.Message;
                }
            }
            return value1;
      
        }



        public static Party_Master Checkparty(string party_code)
        {
            Party_Master record = new Party_Master();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {

                try
                {
                    cn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.party_master where party_code='" + party_code + "'", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            record = new Party_Master();
                            while (dr.Read())
                            {
                               
                                record.party_name = dr["Party_name"].ToString();
                                record.party_code = dr["Party_code"].ToString();
                                record.party_type_code = dr["party_type_code"].ToString();
                                if (dr["party_active"].ToString() == "True")
                                    record.party_active = "Active";
                                else
                                    record.party_active = "InActive";
                                record.party_address = dr["party_address"].ToString();
                                record.district_code = dr["district_code"].ToString();
                                record.party_type_name = dr["party_type_name"].ToString();
                                record.financialyear = dr["financial_year"].ToString();
                                record.party_captive_unit_name = dr["party_captive_unit_name"].ToString();
                                record.party_captive = dr["party_captive"].ToString();
                               
                            }
                            _log.Info("Get User Success :" + record.party_name + "-" +record.party_code);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Get User Fail :" + "-" + ex.Message);
                }
            }
            return record;
        }

        public static string Approve(Party_Master DDC)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    string status = "";
                    StringBuilder str = new StringBuilder();
                    str.Append("update exciseautomation.party_master set  approver_level='" + DDC.record_status + "' where party_code ='" + DDC.party_code + "'");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    if(DDC.record_status=="R")
                    {
                      status=   "Rejected by " +DDC.rolename ;
                    }
                    else
                    {
                        status = "Approved by " + DDC.rolename;
                    }
                    int n = cmd.ExecuteNonQuery();
                    str = new StringBuilder();
                    str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                    str.Append("'" + DDC.id + "','','" + DateTime.Now.ToString("dd-MM-yyyy") + "','CHY','" + status + "','" + DDC.remarks + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DDC.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DDC.user_id + "','" + DDC.financialyear + "','"+DDC.party_code+"')");
                    cmd = new NpgsqlCommand(str.ToString(), cn);
                    n = cmd.ExecuteNonQuery();
                    VAL = "0";
                    trn.Commit();
                    cn.Close();

                }
                catch (Exception ex1)
                {
                    trn.Rollback();


                }

            }
            return VAL;
        }
    }
}
