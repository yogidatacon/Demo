using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.Entities;

namespace Usermngt.DAL
{
   public class DL_Permit
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");


        public static List<Permit> GetList()
        {
            List<Permit> from83 = new List<Permit>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    cmd = new NpgsqlCommand("select a.*,b.party_name,b.party_code,d.permit,b.district_code,b.party_type_code from exciseautomation.permit a inner join exciseautomation.party_master b on a.party_code=b.party_code  left join exciseautomation.document_format_master d on a.party_code=d.party_code where a.record_active='true' order by a.permit_date desc", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        from83 = new List<Permit>();
                        while (dr.Read())
                        {
                            Permit record = new Permit();
                            record.permit_date = dr["permit_date"].ToString().Replace("/", "-").Substring(0, 10); ;
                            record.party_code = dr["party_code"].ToString();
                            record.party_type_code = dr["party_type_code"].ToString();
                            record.financial_year = dr["financial_year"].ToString();
                            record.party_name = dr["party_name"].ToString();
                            if (dr["duty_paid"].ToString()=="Y")
                                if(dr["challan_date"].ToString() !="")
                            record.challan_date = dr["challan_date"].ToString().Replace("/", "-").Substring(0, 10); ;
                            record.permit_no= "Req/" + dr["permit"].ToString() + dr["financial_year"].ToString() + "/" + dr["permit_no"].ToString();
                            if(dr["permit_issueno"].ToString()!="")
                            record.permit_issueno= dr["permit"].ToString() + dr["financial_year"].ToString() + "/" + dr["permit_no"].ToString();
                            record.district_code = dr["district_code"].ToString();
                            record.permit_qty = Convert.ToDouble(dr["permit_qty"].ToString());
                            record.party_code = dr["party_code"].ToString();
                            record.record_status = dr["record_status"].ToString();
                            record.permit_id = Convert.ToInt32(dr["permit_id"].ToString());
                            from83.Add(record);
                        }
                       
                    }

                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return from83;
        }




        public static List<Permit> Gettrasforpass()
        {
            List<Permit> from83 = new List<Permit>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    cmd = new NpgsqlCommand("select a.*,c.party_name,c.party_code as party,d.permit from exciseautomation.permit a inner join exciseautomation.user_registration b on a.purchase_from_party=b.user_id inner join exciseautomation.party_master c on b.party_code=c.party_code left join exciseautomation.document_format_master d on a.party_code=d.party_code  order by a.permit_date desc", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        from83 = new List<Permit>();
                        while (dr.Read())
                        {
                            Permit record = new Permit();
                            record.permit_date = dr["permit_date"].ToString().Replace("/", "-").Substring(0, 10); ;
                           // record.party_code = dr["party_code"].ToString();
                            record.party_name = dr["party_name"].ToString();
                            if(dr["challan_date"].ToString()!="")
                            record.challan_date = dr["challan_date"].ToString().Replace("/", "-").Substring(0, 10); ;
                            record.permit_no = /*"Req/"+*/dr["permit"].ToString() + dr["financial_year"].ToString() + "/" + dr["permit_no"].ToString();
                            record.party_code = dr["party"].ToString();
                            record.record_status = dr["record_status"].ToString();
                            record.permit_id = Convert.ToInt32(dr["permit_id"].ToString());
                            from83.Add(record);
                        }

                    }

                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return from83;
        }

        public static string GetPartyMax(string party_code,string district, string financial_year)
        {
            string value = "0";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select case when max(permit_no) is null then 0 else max(permit_no) end as req_nocno,g.permit from exciseautomation.permit a inner join exciseautomation.document_format_master g on g.party_code=a.party_code inner join exciseautomation.party_master c on c.party_code=a.party_code where a.party_code='"+party_code+"' and c.district_code='" +district+ "' and a.financial_year='"+financial_year+"' group by a.party_code,g.permit", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {

                        value = "Req/" + dr["permit"].ToString() +financial_year+"/"+ Convert.ToInt32(Convert.ToInt32(cmd.ExecuteScalar()) + 1);
                    }
                    if (dt.Rows.Count == 0)
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("select permit from  exciseautomation.document_format_master  where party_code='" + party_code + "'", cn);
                        value = "Req/" + cmd1.ExecuteScalar() +financial_year + "/1";
                    }
                    _log.Info("Get NOC Sucess:" + value);
                }
                catch (Exception ex)
                {
                    value = ex.Message;
                    _log.Info("Get NOC Fail:" + ex.Message);
                }
            }
            return value;
        }

        public static string Insert(Permit from)
        {
            string value = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT max(permit_no) FROM exciseautomation.permit where financial_year='"+from.financial_year+ "' and party_code='"+from.party_code+"' and record_active='true'", cn);
                string m = cmd1.ExecuteScalar().ToString();
                int n = 0;
                int a = 0;
                if (m == "")
                    n = 1;
                else
                    n = Convert.ToInt32(m) + 1;

                try
                {
                    //NpgsqlCommand cmd2 = new NpgsqlCommand("SELECT max(fermenter_receiver_id) FROM exciseautomation.fermenter_receiver", cn);
                    //int h = Convert.ToInt32(cmd2.ExecuteScalar());
                    StringBuilder str = new StringBuilder();
                    NpgsqlCommand cmd;
                    if (from.duty_type=="N")
                    {
                        if (from.lic_application_id ==0)
                        {
                            cmd = new NpgsqlCommand("INSERT INTO exciseautomation.permit( lic_application_id,party_code, permit_no, permit_date, permit_type, purchase_from_party, purchase_district, agent_name, permit_qty, route_chart, creation_date, user_id, record_status, remarks,molasses_allotment_request_id,approver_level,duty_paid,financial_year)VALUES(  '"+a+"','" + from.party_code + "', '" + n + "','" + from.permit_date + "', '" + from.permit_type + "', '" + from.purchase_from_party + "','" + from.purchase_district + "', '" + from.agent_name + "', '" + from.permit_qty + "','" + from.route_chart + "','" + DateTime.Now.ToShortDateString() + "', '" + from.user_id + "', '" + from.record_status + "','" + from.remarks + "','" + from.molasses_allotament_request_id + "','" + from.approver_level + "','" + from.duty_type + "','" + from.financial_year + "')", cn);
                        }
                        else
                        {


                            cmd = new NpgsqlCommand("INSERT INTO exciseautomation.permit(lic_application_id, party_code, permit_no, permit_date, permit_type, purchase_from_party, purchase_district, agent_name, permit_qty, route_chart, creation_date, user_id, record_status, remarks,molasses_allotment_request_id,approver_level,duty_paid,financial_year)VALUES( '" + from.lic_application_id + "', '" + from.party_code + "', '" + n + "','" + from.permit_date + "', '" + from.permit_type + "', '" + from.purchase_from_party + "','" + from.purchase_district + "', '" + from.agent_name + "', '" + from.permit_qty + "','" + from.route_chart + "','" + DateTime.Now.ToShortDateString() + "', '" + from.user_id + "', '" + from.record_status + "','" + from.remarks + "','" + from.molasses_allotament_request_id + "','" + from.approver_level + "','" + from.duty_type + "','" + from.financial_year + "')", cn);
                        }
                    }
                    else
                    {
                        if (from.lic_application_id == 0)
                        {
                            cmd = new NpgsqlCommand("INSERT INTO exciseautomation.permit( lic_application_id,party_code, permit_no, permit_date, permit_type, purchase_from_party, purchase_district, agent_name, duty_amt, duty_rate, challan_no, challan_date, permit_qty, route_chart, creation_date, user_id, record_status, remarks,molasses_allotment_request_id,approver_level,treasury,duty_paid,financial_year)VALUES( '" + a + "','" + from.party_code + "', '" + n + "','" + from.permit_date + "', '" + from.permit_type + "', '" + from.purchase_from_party + "','" + from.purchase_district + "', '" + from.agent_name + "', '" + from.duty_amt + "', '" + from.duty_rate + "',  '" + from.challan_no + "', '" + from.challan_date + "', '" + from.permit_qty + "','" + from.route_chart + "','" + DateTime.Now.ToShortDateString() + "', '" + from.user_id + "', '" + from.record_status + "','" + from.remarks + "','" + from.molasses_allotament_request_id + "','" + from.approver_level + "','" + from.treasury + "','" + from.duty_type + "','" + from.financial_year + "')", cn);
                        }
                        else
                        {
                            cmd = new NpgsqlCommand("INSERT INTO exciseautomation.permit(lic_application_id, party_code, permit_no, permit_date, permit_type, purchase_from_party, purchase_district, agent_name, duty_amt, duty_rate, challan_no, challan_date, permit_qty, route_chart, creation_date, user_id, record_status, remarks,molasses_allotment_request_id,approver_level,treasury,duty_paid,financial_year)VALUES( '" + from.lic_application_id + "', '" + from.party_code + "', '" + n + "','" + from.permit_date + "', '" + from.permit_type + "', '" + from.purchase_from_party + "','" + from.purchase_district + "', '" + from.agent_name + "', '" + from.duty_amt + "', '" + from.duty_rate + "',  '" + from.challan_no + "', '" + from.challan_date + "', '" + from.permit_qty + "','" + from.route_chart + "','" + DateTime.Now.ToShortDateString() + "', '" + from.user_id + "', '" + from.record_status + "','" + from.remarks + "','" + from.molasses_allotament_request_id + "','" + from.approver_level + "','" + from.treasury + "','" + from.duty_type + "','" + from.financial_year + "')", cn);
                        }
                    }
                  
                    a = cmd.ExecuteNonQuery();
                    if (a == 1)
                    {
                        //NpgsqlCommand cmd3 = new NpgsqlCommand("SELECT max(permit_id) FROM exciseautomation.permit", cn);
                        //int b = Convert.ToInt32(cmd3.ExecuteScalar());

                        //for (int i = 0; i < from.permit_item.Count; i++)
                        //{
                        //    NpgsqlCommand cmd4 = new NpgsqlCommand("SELECT max(permit_item_id) FROM exciseautomation.permit_item", cn);

                        //    string f = cmd4.ExecuteScalar().ToString();
                        //    int c = 0;
                        //    if (f == "")
                        //        c = 1;
                        //    else
                        //        c = Convert.ToInt32(f) + 1;

                        //   // c += 1;
                        //    a = 0;
                        //    str = new StringBuilder();
                        //    //NpgsqlCommand cmd5 = new NpgsqlCommand("INSERT INTO exciseautomation.permit_item( permit_id, product_code, req_qty, strength, uom_code, creation_date, user_id) VALUES( '" + b+"','"+from.permit_item[0].product_code+"','"+from.permit_item[0].req_qty+"', '"+from.permit_item[0].strength+"','"+from.permit_item[0].uom_code+"', '"+DateTime.Now.ToShortDateString()+"', '"+from.user_id+"')", cn);
                        //    //a = cmd5.ExecuteNonQuery();

                        //}
                        value = "0";

                    }
                    else
                    {
                        value = "1";
                    }

                }
                catch (Exception ex)
                {

                    value = ex.Message;
                    //  throw (ex);
                }

                return value;

            }
        }



        public static string Update(Permit from)
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
                    NpgsqlCommand cmd;
                    int a = 0;
                    if(from.duty_type=="N")
                    {
                        if (from.lic_application_id == 0)
                        {
                            cmd = new NpgsqlCommand("UPDATE exciseautomation.permit SET  lic_application_id ='" +a+"', party_code ='" + from.party_code + "', permit_date ='" + from.permit_date + "', permit_type ='" + from.permit_type + "', purchase_from_party ='" + from.purchase_from_party + "', purchase_district ='" + from.purchase_district + "', agent_name ='" + from.agent_name + "',  permit_qty ='" + from.permit_qty + "', route_chart ='" + from.route_chart + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "',record_status ='" + from.record_status + "', remarks ='" + from.remarks + "',molasses_allotment_request_id='" + from.molasses_allotament_request_id + "',approver_level ='" + from.approver_level + "',duty_paid='" + from.duty_type + "' WHERE permit_id ='" + from.permit_id + "' and  financial_year='" + from.financial_year + "'", cn);
                        }
                        else
                        {
                            cmd = new NpgsqlCommand("UPDATE exciseautomation.permit SET  lic_application_id ='" + from.lic_application_id + "', party_code ='" + from.party_code + "', permit_date ='" + from.permit_date + "', permit_type ='" + from.permit_type + "', purchase_from_party ='" + from.purchase_from_party + "', purchase_district ='" + from.purchase_district + "', agent_name ='" + from.agent_name + "',  permit_qty ='" + from.permit_qty + "', route_chart ='" + from.route_chart + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "',record_status ='" + from.record_status + "', remarks ='" + from.remarks + "',molasses_allotment_request_id='" + from.molasses_allotament_request_id + "',approver_level ='" + from.approver_level + "',duty_paid='" + from.duty_type + "' WHERE permit_id ='" + from.permit_id + "' and  financial_year='" + from.financial_year + "'", cn);
                        }

                    }
                    else
                    {
                        if (from.lic_application_id == 0)
                        {
                            cmd = new NpgsqlCommand("UPDATE exciseautomation.permit SET  lic_application_id ='" + a + "', party_code ='" + from.party_code + "', permit_date ='" + from.permit_date + "', permit_type ='" + from.permit_type + "', purchase_from_party ='" + from.purchase_from_party + "', purchase_district ='" + from.purchase_district + "', agent_name ='" + from.agent_name + "', duty_amt ='" + from.duty_amt + "', duty_rate ='" + from.duty_rate + "',challan_no ='" + from.challan_no + "', challan_date ='" + from.challan_date + "', permit_qty ='" + from.permit_qty + "', route_chart ='" + from.route_chart + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "',record_status ='" + from.record_status + "', remarks ='" + from.remarks + "',molasses_allotment_request_id='" + from.molasses_allotament_request_id + "',approver_level ='" + from.approver_level + "',treasury='" + from.treasury + "',duty_paid='" + from.duty_type + "' WHERE permit_id ='" + from.permit_id + "' and  financial_year='" + from.financial_year + "'", cn);
                        }
                        else
                        {
                            cmd = new NpgsqlCommand("UPDATE exciseautomation.permit SET  lic_application_id ='" + from.lic_application_id + "', party_code ='" + from.party_code + "', permit_date ='" + from.permit_date + "', permit_type ='" + from.permit_type + "', purchase_from_party ='" + from.purchase_from_party + "', purchase_district ='" + from.purchase_district + "', agent_name ='" + from.agent_name + "', duty_amt ='" + from.duty_amt + "', duty_rate ='" + from.duty_rate + "',challan_no ='" + from.challan_no + "', challan_date ='" + from.challan_date + "', permit_qty ='" + from.permit_qty + "', route_chart ='" + from.route_chart + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "',record_status ='" + from.record_status + "', remarks ='" + from.remarks + "',molasses_allotment_request_id='" + from.molasses_allotament_request_id + "',approver_level ='" + from.approver_level + "',treasury='" + from.treasury + "',duty_paid='" + from.duty_type + "' WHERE permit_id ='" + from.permit_id + "' and  financial_year='" + from.financial_year + "'", cn);
                        }

                    }
                    int n = cmd.ExecuteNonQuery(); 
                    if (n == 1)
                    {
                        //NpgsqlCommand cmd1 = new NpgsqlCommand("delete from exciseautomation.permit_item where  permit_id='" + from.permit_id + "'", cn);
                        //cmd1.ExecuteNonQuery();
                        //for (int i = 0; i < from.permit_item.Count; i++)
                        //{
                        //    NpgsqlCommand cmd4 = new NpgsqlCommand("SELECT max(permit_item_id) FROM exciseautomation.permit_item", cn);
                        //    string f = cmd4.ExecuteScalar().ToString();
                        //    int c = 0;
                        //    if (f == "")
                        //        c = 1;
                        //    else
                        //        c = Convert.ToInt32(f) + 1;
                        //    n = 0;
                        //    str = new StringBuilder();

                        //    NpgsqlCommand cmd5 = new NpgsqlCommand("INSERT INTO exciseautomation.permit_item( permit_id, product_code, req_qty, strength, uom_code, creation_date, user_id) VALUES( '" + from.permit_id + "','" + from.permit_item[0].product_code + "','" + from.permit_item[0].req_qty + "', '" + from.permit_item[0].strength + "','" + from.permit_item[0].uom_code + "', '" + DateTime.Now.ToShortDateString() + "', '" + from.user_id + "')", cn);
                        //    n = cmd5.ExecuteNonQuery();
                        //}


                        VAL = "0";

                        trn.Commit();
                        cn.Close();
                    }
                    else
                    {
                        trn.Rollback();
                        VAL = "1";
                    }
                }
                catch (Exception ex1)
                {
                    trn.Rollback();
                    // _log.Info("Sugarcanepurchase Insertion Fail:" + scp.sugarcanepurchase_id + '-' + scp.party_code + "-" + ex1.Message);
                    VAL = ex1.Message;
                }

            }
            return VAL;
        }

        public static Permit GetDetails(int permit_id ,string financial_year)
        {

           Permit lic = new Permit();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT a.* , d.permit FROM exciseautomation.permit a left join exciseautomation.document_format_master d on a.party_code=d.party_code where permit_id='" + permit_id+"' and financial_year='"+financial_year+"'", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();

                    foreach (DataRow dr in dt.Rows)
                    {
                        lic.permit_id = Convert.ToInt32(dr["permit_id"].ToString());
                        lic.permit_date = Convert.ToDateTime(dr["permit_date"].ToString()).ToString("dd-MM-yyyy");
                        if(dr["lic_application_id"].ToString()!="" && dr["lic_application_id"].ToString() !=null)
                        lic.lic_application_id = Convert.ToInt32(dr["lic_application_id"].ToString());
                        // lic.lic_subtype_code = dr["lic_subtype_code"].ToString();
                        lic.agent_name = dr["agent_name"].ToString();
                        if(dr["permit_validity"].ToString()!="")
                        lic.permit_validity = Convert.ToDateTime(dr["permit_validity"].ToString()).ToString("dd-MM-yyyy");
                        lic.permit_type = dr["permit_type"].ToString();
                        lic.purchase_from_party = dr["purchase_from_party"].ToString();
                        lic.purchase_district = dr["purchase_district"].ToString();
                        lic.party_code = dr["party_code"].ToString();
                        lic.route_chart = dr["route_chart"].ToString();
                        lic.financial_year = dr["financial_year"].ToString();
                        lic.permit_qty = Convert.ToDouble(dr["permit_qty"].ToString());
                        lic.duty_type = dr["duty_paid"].ToString();
                        if(dr["duty_paid"].ToString()=="Y")
                        {
                            if(dr["challan_date"].ToString()!="")
                            lic.challan_date = Convert.ToDateTime(dr["challan_date"].ToString()).ToString("dd-MM-yyyy");
                            lic.duty_rate = Convert.ToDouble(dr["duty_rate"].ToString());
                            lic.duty_amt = Convert.ToDouble(dr["duty_amt"].ToString());
                        }
                        lic.challan_no = dr["challan_no"].ToString();
                        lic.treasury = dr["treasury"].ToString();
                        lic.permit_no = "Req/"+dr["permit"].ToString() + dr["financial_year"].ToString() + "/" + dr["permit_no"].ToString();
                        lic.remarks = dr["remarks"].ToString();
                        lic.record_status = dr["record_status"].ToString();
                        if(dr["record_status"].ToString()!="")
                            lic.molasses_allotament_request_id = Convert.ToInt32(dr["molasses_allotment_request_id"].ToString());
                        lic.user_id = dr["user_id"].ToString();
                        //using (NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT a.*,b.product_name,c.uom_name FROM exciseautomation.permit_item a inner join exciseautomation.product_master b on a.product_code=b.product_code inner join exciseautomation.uom_master c on a.uom_code=c.uom_code where permit_id='"+permit_id+"'", cn))
                        //{
                        //    cmd1.CommandType = System.Data.CommandType.Text;
                        //    //cmd.Parameters.AddWithValue("@UserID", userid);
                        //    NpgsqlDataReader dr2 = cmd1.ExecuteReader();
                        //    lic.permit_item = new List<Permit_item>();
                        //    if (dr2.HasRows)
                        //    {
                        //        while (dr2.Read())
                        //        {
                        //            Permit_item Setup = new Permit_item();
                        //            Setup.permit_item_id = Convert.ToInt32(dr2["permit_item_id"].ToString());
                        //            Setup.permit_id= Convert.ToInt32(dr2["permit_id"].ToString());
                        //            Setup.product_code = Convert.ToInt32(dr2["product_code"].ToString());
                        //            Setup.product_name = dr2["product_name"].ToString();
                        //            Setup.req_qty = Convert.ToDouble(dr2["req_qty"].ToString());
                        //            Setup.strength = Convert.ToDouble(dr2["strength"].ToString());
                        //            Setup.uom_code = dr2["uom_code"].ToString();
                        //            Setup.uom_name = dr2["uom_name"].ToString();
                        //            Setup.user_id = dr2["user_id"].ToString();
                        //            lic.permit_item.Add(Setup);
                        //        }
                        //    }
                        //    dr2.Close();
                        //}
                        // dispatch.party_name = dr["party_name"].ToString();

                    }


                }

                catch (Exception ex)
                {
                    throw (ex);
                }
                return lic;
            }
        }

        public static Permit Gettransfordetails(int permit_id, string financial_year, string party)
        {

            Permit lic = new Permit();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name,b.party_address,c.allotted_qty,d.district_name,f.state_name,s.product_name,s.product_code,h.permit from exciseautomation.permit a inner join exciseautomation.party_master b on a.party_code=b.party_code inner join exciseautomation.molasses_allotment_request c on a.molasses_allotment_request_id=c.molasses_allotment_request_id and a.financial_year=c.financial_year inner join exciseautomation.product_master as s on c.product_code=s.product_code inner join exciseautomation.district_master d on b.district_code=d.district_code inner join exciseautomation.state_master f on d.state_code=f.state_code left join exciseautomation.document_format_master h on a.party_code=h.party_code  where a.permit_id='" + permit_id + "' and a.financial_year='"+financial_year+ "' and c.requested_fromunit='" + party+"'", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();

                    foreach (DataRow dr in dt.Rows)
                    {
                        lic.permit_id = Convert.ToInt32(dr["permit_id"].ToString());
                        lic.permit_date = Convert.ToDateTime(dr["permit_date"].ToString()).ToString("dd-MM-yyyy");
                        if (dr["lic_application_id"].ToString() != "" && dr["lic_application_id"].ToString() != null)
                            lic.lic_application_id = Convert.ToInt32(dr["lic_application_id"].ToString());
                        if(dr["duty_amt"].ToString()!="")
                        {
                            lic.duty_amt = Convert.ToDouble(dr["duty_amt"].ToString());
                        }
                        // lic.lic_subtype_code = dr["lic_subtype_code"].ToString();
                        lic.agent_name = dr["agent_name"].ToString();
                        lic.permit_validity = Convert.ToDateTime(dr["permit_validity"].ToString()).ToString("dd-MM-yyyy");
                        lic.permit_type = dr["permit_type"].ToString();
                        lic.address = dr["party_address"].ToString();
                        lic.purchase_from_party = dr["purchase_from_party"].ToString();
                        lic.purchase_district = dr["purchase_district"].ToString();
                        lic.state_name = dr["state_name"].ToString();
                        lic.district_name = dr["district_name"].ToString();
                        lic.route_chart = dr["route_chart"].ToString();
                        if (dr["duty_rate"].ToString() !="")
                        {
                            lic.duty_rate = Convert.ToDouble(dr["duty_rate"].ToString());
                        }
                        lic.permit_qty = Convert.ToDouble(dr["permit_qty"].ToString());
                        lic.alloted_qty = Convert.ToDouble(dr["allotted_qty"].ToString());
                        if (dr["challan_date"].ToString() != "")
                        {
                            lic.challan_date = Convert.ToDateTime(dr["challan_date"].ToString()).ToString("dd-MM-yyyy");
                        }
                        lic.challan_no = dr["challan_no"].ToString();
                        lic.permit_no = "Req/"+dr["permit"].ToString() + dr["financial_year"].ToString() + "/" + dr["permit_no"].ToString();
                        lic.product_name = dr["product_name"].ToString();
                        lic.product_code = dr["product_code"].ToString();
                        lic.remarks = dr["remarks"].ToString();
                        lic.record_status = dr["record_status"].ToString();
                        if (dr["molasses_allotment_request_id"].ToString() != "")
                            lic.molasses_allotament_request_id = Convert.ToInt32(dr["molasses_allotment_request_id"].ToString());
                        lic.user_id = dr["user_id"].ToString();
                    //    using (NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT a.*,b.product_name,c.uom_name FROM exciseautomation.permit_item a inner join exciseautomation.product_master b on a.product_code=b.product_code inner join exciseautomation.uom_master c on a.uom_code=c.uom_code where permit_id='" + permit_id + "'", cn))
                    //    {
                    //        cmd1.CommandType = System.Data.CommandType.Text;
                    //        //cmd.Parameters.AddWithValue("@UserID", userid);
                    //        NpgsqlDataReader dr2 = cmd1.ExecuteReader();
                    //        lic.permit_item = new List<Permit_item>();
                    //        if (dr2.HasRows)
                    //        {
                    //            while (dr2.Read())
                    //            {
                    //                Permit_item Setup = new Permit_item();
                    //                Setup.permit_item_id = Convert.ToInt32(dr2["permit_item_id"].ToString());
                    //                Setup.permit_id = Convert.ToInt32(dr2["permit_id"].ToString());
                    //                Setup.product_code = Convert.ToInt32(dr2["product_code"].ToString());
                    //                Setup.product_name = dr2["product_name"].ToString();
                    //                Setup.req_qty = Convert.ToDouble(dr2["req_qty"].ToString());
                    //                Setup.strength = Convert.ToDouble(dr2["strength"].ToString());
                    //                Setup.uom_code = dr2["uom_code"].ToString();
                    //                Setup.uom_name = dr2["uom_name"].ToString();
                    //                Setup.user_id = dr2["user_id"].ToString();
                    //                lic.permit_item.Add(Setup);
                    //            }
                    //        }
                    //        dr2.Close();
                    //    }
                    //    // dispatch.party_name = dr["party_name"].ToString();

                    }


                }

                catch (Exception ex)
                {
                    throw (ex);
                }
                return lic;
            }
        }
        public static Permit Getvalue(string id ,string financial_year)
        {
            Permit allot = new Permit();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("Select case when sum(permit_qty) is null then 0 else sum(permit_qty) end as permit_qty from exciseautomation.permit where molasses_allotment_request_id='" + id + "' and financial_year='"+financial_year+"' ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr1 = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr1);
                        dr1.Close();
                        foreach (DataRow dr in dt.Rows)
                        {
                     allot.permit_qty= Convert.ToDouble(dr["permit_qty"].ToString());
                        }
                    }
                    cn.Close();
                    
                }
                catch (Exception ex)
                {
                    throw (ex);
                }

            }
            return allot;
        }

        public static Permit GetRRvalue(string id)
        {
            Permit allot = new Permit();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("Select case when sum(permit_qty) is null then 0 else sum(permit_qty) end as permit_qty from exciseautomation.permit where permit_id='" + id + "' ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr1 = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr1);
                        dr1.Close();
                        foreach (DataRow dr in dt.Rows)
                        {
                            allot.permit_qty = Convert.ToDouble(dr["permit_qty"].ToString());
                        }
                    }
                    cn.Close();

                }
                catch (Exception ex)
                {
                    throw (ex);
                }

            }
            return allot;
        }
        public static RawMaterialReceipt GetList(int receipt_id)
        {
            RawMaterialReceipt rawmaterials = new RawMaterialReceipt();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.party_name,c.pass,d.vehicle_no as vehicle_no1,d.pass_issueno from exciseautomation.rawmaterial_receipt a  right join exciseautomation.party_master b on a.supplier_party_code=b.party_code inner join exciseautomation.document_format_master c on c.party_code=a.supplier_party_code inner join exciseautomation.pass d on a.passno=d.pass_id where  a.rawmaterial_receipt_id='" + receipt_id + "'", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        rawmaterials.rawmaterial_receipt_id = dr["rawmaterial_receipt_id"].ToString();
                        rawmaterials.party_code = dr["party_code"].ToString();
                        rawmaterials.party_name = dr["party_name"].ToString();
                        if (dr["rmr_entrydate"].ToString() != "")
                            rawmaterials.rmr_entrydate = dr["rmr_entrydate"].ToString().Substring(0, 10).Replace("/", "-");
                        rawmaterials.passissuedate = dr["passissuedate"].ToString();
                        rawmaterials.passno = dr["pass"] + dr["pass_issueno"].ToString();
                        // rawmaterials.rmrpassno= dr["passno"].ToString();
                        if (dr["passqty"].ToString() == "0")
                            rawmaterials.passqty = 0;
                        else
                            rawmaterials.passqty = Convert.ToDouble(dr["passqty"].ToString());
                        rawmaterials.vehicleno = dr["vehicle_no1"].ToString();
                        if (dr["grossweight"].ToString() == "")
                            rawmaterials.grossweight = 0;
                        else
                            rawmaterials.grossweight = Convert.ToDouble(dr["grossweight"].ToString());
                        if (dr["tankerweight"].ToString() == "")
                            rawmaterials.tankerweight = 0;
                        else
                            rawmaterials.tankerweight = Convert.ToDouble(dr["tankerweight"].ToString());
                        if (dr["supplierweight"].ToString() == "")
                            rawmaterials.supplierweight = 0;
                        else
                            rawmaterials.supplierweight = Convert.ToDouble(dr["supplierweight"].ToString());
                        if (dr["transitweight"].ToString() == "")
                            rawmaterials.transitweight = 0;
                        else
                            rawmaterials.transitweight = Convert.ToDouble(dr["transitweight"].ToString());
                        if (dr["netweight"].ToString() == "")
                            rawmaterials.netweight = 0;
                        else
                            rawmaterials.netweight = Convert.ToDouble(dr["netweight"].ToString());
                        //rawmaterials.suppliername= dr["attribute3"].ToString();
                        //rawmaterials.rawmaterial= dr["attribute1"].ToString();
                        //rawmaterials.suppliertype= dr["attribute2"].ToString();
                        //rawmaterials.supplier= dr["supplier_party_code"].ToString();
                        rawmaterials.record_status = dr["record_status"].ToString();
                        rawmaterials.remarks = dr["remarks"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return rawmaterials;
            }
        }
        //public static List<RmrReceiptStorage> GetStorage(int receipt_id)
        //{
        //    List<RmrReceiptStorage> str = new List<RmrReceiptStorage>();
        //    using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
        //    {
        //        cn.Open();
        //        try
        //        {
        //            NpgsqlCommand cmd = new NpgsqlCommand("SELECT  a.*,b.vat_name FROM exciseautomation.rmreceipt_storage a inner join exciseautomation.vat_master b on a.vat_code = b.vat_code where rawmaterial_receipt_id = '" + receipt_id + "'", cn);
        //            NpgsqlDataReader dr = cmd.ExecuteReader();
        //            if (dr.HasRows)
        //            {
        //                str = new List<RmrReceiptStorage>();
        //                while (dr.Read())
        //                {
        //                    RmrReceiptStorage storage = new RmrReceiptStorage();
        //                    storage.vat_name = dr["vat_name"].ToString();
        //                    storage.vat_code = dr["vat_code"].ToString();
        //                    storage.storedqty = Convert.ToDouble(dr["storedqty"].ToString());
        //                    str.Add(storage);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw (ex);
        //        }
        //        return str;
        //    }
        //}



        public static string Approve(Permit record)
        {
            List<Permit> from83 = new List<Permit>();
            // FermentertoReceiverForm_83 record = new FermentertoReceiverForm_83();
            string value;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    StringBuilder str = new StringBuilder();
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.permit SET  route_chart ='" + record.route_chart + "',record_status='" + record.record_status + "',permit_validity='"+record.permit_validity+"',approver_level = '" + record.approver_level+"' WHERE permit_id='" + record.permit_id + "' and  financial_year='" +record.financial_year + "'", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (record.record_status == "R")
                    {
                        
                        record.record_status = "Rejected by Assistant Commissioner";
                    }
                    else
                    {
                        //NpgsqlCommand cmd6 = new NpgsqlCommand("Select allotted_qty from exciseautomation.molasses_allotment_request where molasses_allotment_request_id='" + record.molasses_allotament_request_id + "'", cn);
                        //double a = Convert.ToDouble(cmd6.ExecuteScalar());
                        //double c = Convert.ToDouble(a) - Convert.ToDouble(record.permit_qty) ;
                        //NpgsqlCommand cmd7 = new NpgsqlCommand("update exciseautomation.molasses_allotment_request set allotted_qty='" + c + "'  where molasses_allotment_request_id='" + record.molasses_allotament_request_id + "'", cn);
                        //cmd7.ExecuteNonQuery();
                        //NpgsqlCommand cmd4 = new NpgsqlCommand("SELECT max(permit_issueno) FROM exciseautomation.permit where party_code='"+record.party_code+"'", cn);
                        //string f = cmd4.ExecuteScalar().ToString();
                        //int c = 0;
                        //if (f == "")
                        //    c = 1;
                        //else
                        //    c = Convert.ToInt32(f) + 1;
                        NpgsqlCommand cmd4 = new NpgsqlCommand("Select case when max(permit_issueno) is null then 0 else max(permit_issueno) end as permit_issueno from exciseautomation.permit where party_code='"+record.party_code+"' and financial_year='"+record.financial_year+"'", cn);
                        int c = Convert.ToInt32(cmd4.ExecuteScalar()) + 1;
                        NpgsqlCommand cmd1 = new NpgsqlCommand("UPDATE exciseautomation.permit SET  record_status='" + record.record_status + "',approver_level = '" + record.approver_level + "',permit_issueno='"+c+"',permit_issuedate='"+DateTime.Now.ToShortDateString()+"' WHERE permit_id='" + record.permit_id + "' and  financial_year='" + record.financial_year + "'", cn);
                        int n1 = cmd1.ExecuteNonQuery();
                        record.record_status = "Approved by Assistant Commissioner";
                    }
                    StringBuilder str1 = new StringBuilder();
                    str1.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                    str1.Append("'" + record.permit_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "','PER','" + record.record_status + "','" + record.remarks + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + record.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + record.user_id + "','"+record.financial_year+"','"+record.party_code+"')");
                    cmd = new NpgsqlCommand(str1.ToString(), cn);
                    n = cmd.ExecuteNonQuery();
                    value = "0";
                    trn.Commit();
                    
                    cn.Close();
                }
                catch (Exception ex)
                {
                    value = ex.Message;
                    
                    trn.Rollback();
                }
                return value;

            }
        }



        public static UserDetails CheckUser(string userid)
        {
            UserDetails user = new UserDetails();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {

                try
                {
                    cn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.district_name from exciseautomation.view_checkuser a inner join exciseautomation.district_master b on a.district_code=b.district_code  where a.user_id='" + userid + "'", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            user = new UserDetails();
                            while (dr.Read())
                            {
                                user.id = Convert.ToInt32(dr["user_registration_id"].ToString());
                                user.user_id = dr["user_id"].ToString();
                                user.user_dob = dr["user_dob"].ToString();
                                user.user_password = dr["user_password"].ToString();
                                user.access_type_code = Convert.ToInt32(dr["access_type_code"].ToString());
                                if (dr["role_level_code"].ToString() != "")
                                    user.role_level_code = Convert.ToInt32(dr["role_level_code"].ToString());
                                user.org_id = Convert.ToInt32(dr["org_id"].ToString());
                                user.role_name_code = Convert.ToInt32(dr["role_name_code"].ToString());
                                user.user_address = dr["user_address"].ToString();
                                user.mobile = dr["mobile"].ToString();
                                user.email_id = dr["email_id"].ToString();
                                user.state_code = dr["State_code"].ToString();
                                user.division_code = dr["Division_code"].ToString();
                                user.district_code = dr["District_Code"].ToString();
                                user.district_name = dr["District_Name"].ToString();
                                user.photoname = dr["photoname"].ToString();
                                //user.comments = dr["Comments"].ToString();
                                user.user_name = dr["user_name"].ToString();
                                user.mobile = dr["mobile"].ToString();
                                user.email_id = dr["email_id"].ToString();
                                user.department_name = dr["department_code"].ToString();
                                user.party_code = dr["Party_code"].ToString();
                                user.party_name = dr["Party_name"].ToString();
                                user.party_type = dr["Party_type_name"].ToString();
                                user.role_name = dr["role_name"].ToString();
                                user.party_captive_unit_name = dr["party_captive_unit_name"].ToString();
                                user.financial_year = dr["financial_year"].ToString();
                                user.iscaptive = dr["party_captive"].ToString();
                                user.digi_id = dr["dongle_id"].ToString();
                                user.digi_password = dr["dongle_password"].ToString();
                                //user.module_code = dr["module_code"].ToString();
                                //user.submodule_code = dr["submodule_code"].ToString();
                                //user.tab_name_id = dr["tab_name_id"].ToString();
                                //user.tab_name = dr["tab_name"].ToString();
                                //user.add = dr["add_role_permission"].ToString();
                                //user.edit = dr["edit_role_permission"].ToString();
                                //user.delete = dr["delete_role_permission"].ToString();
                                //user.review = dr["review_role_permission"].ToString();
                                //user.approve = dr["approve_role_permission"].ToString();
                            }
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return user;
        }


        public static List<UserDetails> Check()
        {
            List<UserDetails> users = new List<UserDetails>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {

                try
                {
                    cn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.district_name from exciseautomation.view_checkuser a inner join exciseautomation.district_master b on a.district_code=b.district_code  ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            users = new List<UserDetails>();
                            while (dr.Read())
                            {
                                UserDetails user = new UserDetails();
                                user.id = Convert.ToInt32(dr["user_registration_id"].ToString());
                                user.user_id = dr["user_id"].ToString();
                                user.user_dob = dr["user_dob"].ToString();
                                user.user_password = dr["user_password"].ToString();
                                user.access_type_code = Convert.ToInt32(dr["access_type_code"].ToString());
                                if (dr["role_level_code"].ToString() != "")
                                    user.role_level_code = Convert.ToInt32(dr["role_level_code"].ToString());
                                user.org_id = Convert.ToInt32(dr["org_id"].ToString());
                                user.role_name_code = Convert.ToInt32(dr["role_name_code"].ToString());
                                user.user_address = dr["user_address"].ToString();
                                user.mobile = dr["mobile"].ToString();
                                user.email_id = dr["email_id"].ToString();
                                user.state_code = dr["State_code"].ToString();
                                user.division_code = dr["Division_code"].ToString();
                                user.district_code = dr["District_Code"].ToString();
                                user.district_name = dr["District_Name"].ToString();
                                user.photoname = dr["photoname"].ToString();
                                //user.comments = dr["Comments"].ToString();
                                user.user_name = dr["user_name"].ToString();
                                user.mobile = dr["mobile"].ToString();
                                user.email_id = dr["email_id"].ToString();
                                user.department_name = dr["department_code"].ToString();
                                user.party_code = dr["Party_code"].ToString();
                                user.party_name = dr["Party_name"].ToString();
                                user.party_type = dr["Party_type_name"].ToString();
                                user.role_name = dr["role_name"].ToString();
                                user.party_captive_unit_name = dr["party_captive_unit_name"].ToString();
                                user.financial_year = dr["financial_year"].ToString();
                                user.iscaptive = dr["party_captive"].ToString();
                                user.digi_id = dr["dongle_id"].ToString();
                                user.digi_password = dr["dongle_password"].ToString();
                                //user.module_code = dr["module_code"].ToString();
                                //user.submodule_code = dr["submodule_code"].ToString();
                                //user.tab_name_id = dr["tab_name_id"].ToString();
                                //user.tab_name = dr["tab_name"].ToString();
                                //user.add = dr["add_role_permission"].ToString();
                                //user.edit = dr["edit_role_permission"].ToString();
                                //user.delete = dr["delete_role_permission"].ToString();
                                //user.review = dr["review_role_permission"].ToString();
                                //user.approve = dr["approve_role_permission"].ToString();
                                users.Add(user);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return users;
        }


    }
}
