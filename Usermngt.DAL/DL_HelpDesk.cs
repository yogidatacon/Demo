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
   public  class DL_HelpDesk
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static string Insert(Helpdesk helpdesk)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT count(helpdesk_ticket_id) FROM exciseautomation.helpdesk_ticket where user_id='"+helpdesk.user_id+"'", cn);
                    cmd1.CommandType = System.Data.CommandType.Text;
                  int n= Convert.ToInt32( cmd1.ExecuteScalar());
                    n += 1;
                    StringBuilder str = new StringBuilder();
                       str.Append("INSERT INTO exciseautomation.helpdesk_ticket( ticketno, ticket_query, ticket_raisedby, ticket_formname, user_email, user_contact,ticketstatus_code, lastmodified_date, user_id, creation_date,record_status)")
                        .Append("VALUES('"+helpdesk.ticketno+"','"+helpdesk.ticket_query+"','"+helpdesk.ticket_raisedby+"', '"+helpdesk.ticket_formname+"', '"+helpdesk.user_email+"', '"+helpdesk.user_contact+"', '"+helpdesk.ticketstatus_code+"', '"+DateTime.Now.ToShortDateString()+"','"+helpdesk.user_id+"','"+DateTime.Now.ToShortDateString()+"','"+helpdesk.record_status+"')");
                    string ticketno = helpdesk.ticketno + "/" + n;
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.helpdesk_ticket(ticketno, ticket_query, ticket_raisedby, ticket_formname, user_email, user_contact,ticketstatus_code, lastmodified_date, user_id, creation_date,record_status)VALUES('" + ticketno + "','" + helpdesk.ticket_query + "','" + helpdesk.ticket_raisedby + "', '" + helpdesk.ticket_formname + "', '" + helpdesk.user_email + "', '" + helpdesk.user_contact + "', '" + helpdesk.ticketstatus_code + "', '" + DateTime.Now.ToShortDateString() + "','" + helpdesk.user_id + "','" + DateTime.Now.ToShortDateString() + "','" + helpdesk.record_status + "')", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    int a=cmd.ExecuteNonQuery();
                    if(a==1)
                    {
                        if(helpdesk.path !=null && helpdesk.path!="")
                        {
                        NpgsqlCommand cmd2 = new NpgsqlCommand("SELECT max(helpdesk_ticket_id) FROM exciseautomation.helpdesk_ticket where user_id='" + helpdesk.user_id + "'", cn);
                        cmd2.CommandType = System.Data.CommandType.Text;
                        int m = Convert.ToInt32(cmd2.ExecuteScalar());
                        NpgsqlCommand cmd3 = new NpgsqlCommand("INSERT INTO exciseautomation.hd_ticket_history( transaction_id, transaction_date, user_registration_id,lastmodified_date, createdby_id, creation_date, user_id)VALUES('" +m + "', '" + DateTime.Now.ToShortDateString() + "', '" + helpdesk.user_registration_id + "', '" + DateTime.Now.ToShortDateString() + "', '" +helpdesk.ticket_raisedby+ "', '" + DateTime.Now.ToShortDateString() + "', '" + helpdesk.user_id + "')", cn);
                        cmd3.CommandType = System.Data.CommandType.Text;
                        int b = cmd3.ExecuteNonQuery();
                        if(b==1)
                        {
                            NpgsqlCommand cmd5 = new NpgsqlCommand("SELECT max(hd_ticket_history_id) FROM exciseautomation.hd_ticket_history", cn);
                            int l = Convert.ToInt32(cmd5.ExecuteScalar());
                            NpgsqlCommand cmd4 = new NpgsqlCommand("INSERT INTO exciseautomation.hd_docs(doc_id, doc_path, doc_type_code, user_id, creation_date) Values('" + l + "','" + helpdesk.path + "','HDTIC','" + helpdesk.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "')", cn);
                             cmd4.ExecuteNonQuery();
                        }
                        }
                    }
                }
                catch (Exception ex1)
                {
                   
                    VAL = ex1.Message;
                }

            }
            return VAL;
        }


        public static List<Helpdesk> GetList(string userid)
        {
            List<Helpdesk> Helpdesk = new List<Helpdesk>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    if ( userid== "Admin" || userid==null)
                        cmd = new NpgsqlCommand("SELECT a.*,b.ticketstatus_name,c.priority_name,d.user_name as developer, e.user_name as tester,f.user_name FROM exciseautomation.helpdesk_ticket a left join exciseautomation.ticketstatus_master b on a.ticketstatus_code=b.ticketstatus_code left join exciseautomation.priority_master c on a.priority_code=c.priority_code left join exciseautomation.user_registration d on a.ticket_developer=d.user_registration_id left join exciseautomation.user_registration e on a.ticket_tester=e.user_registration_id left join exciseautomation.user_registration f on a.ticket_raisedby=f.user_registration_id ORDER BY creation_date ", cn);
                    else
                        cmd = new NpgsqlCommand("SELECT a.*,b.ticketstatus_name,c.priority_name,d.user_name as developer, e.user_name as tester,f.user_name FROM exciseautomation.helpdesk_ticket a left join exciseautomation.ticketstatus_master b on a.ticketstatus_code=b.ticketstatus_code left join exciseautomation.priority_master c on a.priority_code=c.priority_code left join exciseautomation.user_registration d on a.ticket_developer=d.user_registration_id left join exciseautomation.user_registration e on a.ticket_tester=e.user_registration_id left join exciseautomation.user_registration f on a.ticket_raisedby=f.user_registration_id ORDER BY creation_date ", cn);

                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        Helpdesk = new List<Helpdesk>();
                        while (dr.Read())
                        {
                            Helpdesk record = new Helpdesk();
                            record.helpdesk_ticket_id = Convert.ToInt32(dr["helpdesk_ticket_id"].ToString());
                            record.ticketno = dr["ticketno"].ToString();
                            record.ticket_raisedby = Convert.ToInt32(dr["ticket_raisedby"].ToString());
                            record.record_status = dr["record_status"].ToString();
                            record.user_contact =Convert.ToDouble( dr["user_contact"].ToString());
                            record.user_email = dr["user_email"].ToString();
                            record.ticket_query = dr["ticket_query"].ToString();
                            record.ticket_formname = dr["ticket_formname"].ToString();
                            record.developer = dr["developer"].ToString();
                            //if(dr["ticket_developer"].ToString()!="" || dr["ticket_developer"].ToString() !=null)
                            //{
                            //    record.developer_code = Convert.ToInt32(dr["ticket_developer"].ToString());
                            //}
                            //if (dr["ticket_tester"].ToString() != "" || dr["ticket_tester"].ToString() != null)
                            //{
                            //    record.tester_code = Convert.ToInt32(dr["ticket_tester"].ToString());
                            //}
                            record.tester= dr["tester"].ToString();
                            record.creation_date =Convert.ToDateTime(dr["creation_date"].ToString()).ToString("dd/MM/yyyy");
                            record.priority = dr["priority_name"].ToString();
                            record.ticketstatus= dr["ticketstatus_name"].ToString();
                            record.party_name= dr["user_name"].ToString();
                            record.user_id = dr["user_id"].ToString();
                            Helpdesk.Add(record);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return Helpdesk;
        }

        public static Helpdesk GetDetails( int id)
        {

            Helpdesk record = new Helpdesk();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT a.*,b.ticketstatus_name,c.priority_name,c.priority_resolvetime,d.user_name as developer,d.user_registration_id as developer_code , e.user_name as tester,e.user_registration_id as tester_code,f.user_name FROM exciseautomation.helpdesk_ticket a left join exciseautomation.ticketstatus_master b on a.ticketstatus_code=b.ticketstatus_code left join exciseautomation.priority_master c on a.priority_code=c.priority_code left join exciseautomation.user_registration d on a.ticket_developer=d.user_registration_id left join exciseautomation.user_registration e on a.ticket_tester=e.user_registration_id left join exciseautomation.user_registration f on a.ticket_raisedby=f.user_registration_id where  a.helpdesk_ticket_id='" + id+"'", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();

                    foreach (DataRow dr in dt.Rows)
                    {
                        record.helpdesk_ticket_id = Convert.ToInt32(dr["helpdesk_ticket_id"].ToString());
                        record.ticketno = dr["ticketno"].ToString();
                        record.ticket_raisedby = Convert.ToInt32(dr["ticket_raisedby"].ToString());
                        record.record_status = dr["record_status"].ToString();
                        record.user_contact = Convert.ToDouble(dr["user_contact"].ToString());
                        record.user_email = dr["user_email"].ToString();
                        record.ticket_query = dr["ticket_query"].ToString();
                        record.ticket_formname = dr["ticket_formname"].ToString();
                        record.developer = dr["developer"].ToString();
                        if (dr["developer_code"].ToString() != "")
                            record.developer_code = Convert.ToInt32(dr["developer_code"].ToString());
                            record.tester = dr["tester"].ToString();
                        if (dr["tester_code"].ToString() != "")
                            record.tester_code = Convert.ToInt32(dr["tester_code"].ToString());
                        if (dr["timetaken_dev"].ToString() != "")
                            record.timetaken_dev= Convert.ToInt32(dr["timetaken_dev"].ToString());
                        if (dr["timetaken_tester"].ToString() != "")
                            record.timetaken_tester = Convert.ToInt32(dr["timetaken_tester"].ToString());
                        record.creation_date = Convert.ToDateTime(dr["creation_date"].ToString()).ToString("dd/MM/yyyy");
                       record.lastmodified_date= Convert.ToDateTime(dr["lastmodified_date"].ToString()).ToString("dd/MM/yyyy");
                        record.priority_code= dr["priority_code"].ToString();
                        record.ticketstatus_code= dr["ticketstatus_code"].ToString();
                        record.priority = dr["priority_name"].ToString();
                        record.priority_resolvetime= dr["priority_resolvetime"].ToString();
                        if(dr["ticket_category"].ToString()!=null ||dr["ticket_category"].ToString()=="")
                        record.ticketcategory_code= dr["ticket_category"].ToString();
                        record.ticketstatus = dr["ticketstatus_name"].ToString();
                        record.party_name = dr["user_name"].ToString();
                        record.user_id = dr["user_id"].ToString();
                    }

                }

                catch (Exception ex)
                {
                    throw (ex);
                }
                return record;
            }
        }


        public static int GetExistsData(string tablename, string column, string value)
        {
            int value1 = 0;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                  
                        cmd = new NpgsqlCommand("select count(1) from exciseautomation." + tablename + " where " + column + "='" + value + "'", cn);
                    string re = cmd.ExecuteScalar().ToString();
                    if (re == "1")
                    {
                        value1 = 1;
                        _log.Info("Get Existing data Success :" + tablename);
                    }
                    else
                    {
                        if (re != "")
                        value1 = Convert.ToInt32(re);
                        _log.Info("Get Existing data Fail :" + tablename);
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Get Existing data Fail :" + tablename + "-" + ex.Message);
                    value1 = 0;
                }
            }
            return value1;
        }


        public static string InsertHistory(TicketHistory ticket)
        {
            string VAL = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT max(hd_ticket_history_id) FROM exciseautomation.hd_ticket_history", cn);
                   string  m = Convert.ToString( cmd1.ExecuteScalar());
                    int n = 0;
                    int a = 0;
                    if (m == "")
                        n = 1;
                    else
                        n = Convert.ToInt32(m) + 1;
                    StringBuilder str = new StringBuilder();
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.hd_ticket_history( transaction_id, transaction_date, user_registration_id, remarks, lastmodified_date, createdby_id, creation_date, user_id)VALUES('"+ticket.transaction_id+"', '"+ticket.transaction_date+"', '"+ticket.user_registration_id+"','"+ticket.remarks+"', '"+DateTime.Now.ToShortDateString()+"', '"+ticket.createdby_id+"', '"+DateTime.Now.ToShortDateString()+"', '"+ticket.user_id+"')", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                   int b= cmd.ExecuteNonQuery();
                    if (b == 1)
                    {
                       // for (int i = 0; i < ticket.docs.Count; i++)
                      //  {
                            str = new StringBuilder();
                            //str.Append("INSERT INTO exciseautomation.eascm_docs(hd_docs_id,doc_id, doc_path, doc_type_code, user_id, creation_date)");
                            //str.Append("Values('"+ticket.transaction_id+"','" + m + "','" + ticket.path + "','HDTIC','" + ticket.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "')");
                            if(ticket.path !="" && ticket.path!=null)
                        { 
                            NpgsqlCommand cmd4 = new NpgsqlCommand("INSERT INTO exciseautomation.hd_docs(doc_id, doc_path, doc_type_code, user_id, creation_date) Values('" + n + "','" + ticket.path + "','HDTIC','" + ticket.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "')", cn);
                             n=cmd4.ExecuteNonQuery();
                        }
                        //}
                        NpgsqlCommand cmd3;

                        if ((ticket.developer != "Select" && ticket.developer != "" && ticket.developer != null) && (ticket.tester != "Select" && ticket.tester != "" && ticket.tester != null))
                            cmd3 = new NpgsqlCommand(" update exciseautomation.helpdesk_ticket set priority_code='" + ticket.priority_code + "', ticketstatus_code='" + ticket.ticketsatus + "',record_status='" + ticket.record_status + "',ticket_tester='" + ticket.tester + "',ticket_developer='" + ticket.developer + "',lastmodified_date='" + DateTime.Now.ToShortDateString() + "',ticket_category='" + ticket.ticketcategory_code + "' where  helpdesk_ticket_id='" + ticket.transaction_id + "' ", cn);
                        else if (ticket.developer !="Select" && ticket.developer !="" && ticket.developer !=null )
                        cmd3 = new NpgsqlCommand(" update exciseautomation.helpdesk_ticket set priority_code='"+ticket.priority_code+"', ticketstatus_code='"+ticket.ticketsatus+"',ticket_developer='"+ticket.developer+ "',record_status='"+ticket.record_status+ "',lastmodified_date='" + DateTime.Now.ToShortDateString() + "',ticket_category='"+ticket.ticketcategory_code+"' where  helpdesk_ticket_id='" + ticket.transaction_id+"' ", cn);
                        else if(ticket.tester != "Select" && ticket.tester != "" && ticket.tester != null)
                            cmd3 = new NpgsqlCommand(" update exciseautomation.helpdesk_ticket set priority_code='" + ticket.priority_code + "', ticketstatus_code='" + ticket.ticketsatus + "',ticket_tester='"+ticket.tester+ "',record_status='" + ticket.record_status + "',lastmodified_date='" + DateTime.Now.ToShortDateString() + "',ticket_category='" + ticket.ticketcategory_code + "' where  helpdesk_ticket_id='" + ticket.transaction_id + "' ", cn);
                        else
                            cmd3 = new NpgsqlCommand(" update exciseautomation.helpdesk_ticket set priority_code='" + ticket.priority_code + "', ticketstatus_code='" + ticket.ticketsatus + "',record_status='" + ticket.record_status + "',lastmodified_date='" + DateTime.Now.ToShortDateString() + "',ticket_category='" + ticket.ticketcategory_code + "' where  helpdesk_ticket_id='" + ticket.transaction_id + "' ", cn);
                        cmd3.CommandType = System.Data.CommandType.Text;
                       cmd3.ExecuteNonQuery();
                        NpgsqlCommand cmd5;
                        if ((ticket.developer != "Select" && ticket.developer != "" && ticket.developer != null) && ticket.record_status=="N")
                            cmd5= new NpgsqlCommand(" update exciseautomation.helpdesk_ticket set ticket_start_dev='" + DateTime.Now.ToShortDateString() + "',lastmodified_date='" + DateTime.Now.ToShortDateString() + "'  where  helpdesk_ticket_id='" + ticket.transaction_id + "' ", cn);
                        else if ((ticket.developer != "Select" && ticket.developer != "" && ticket.developer != null) && ticket.record_status == "R")
                            cmd5 = new NpgsqlCommand(" update exciseautomation.helpdesk_ticket set ticket_end_dev='" +DateTime.Now.ToShortDateString() + "',ticket_start_tester='" + DateTime.Now.ToShortDateString() + "' ,lastmodified_date='" + DateTime.Now.ToShortDateString() + "'  where  helpdesk_ticket_id='" + ticket.transaction_id + "' ", cn);
                        else if ((ticket.tester != "Select" && ticket.tester != "" && ticket.tester != null) && ticket.record_status == "C")
                            cmd5 = new NpgsqlCommand(" update exciseautomation.helpdesk_ticket set ticket_end_tester='" + DateTime.Now.ToShortDateString() + "'  where  helpdesk_ticket_id='" + ticket.transaction_id + "' ", cn);
                        else
                            cmd5 = new NpgsqlCommand(" update exciseautomation.helpdesk_ticket set lastmodified_date='" + DateTime.Now.ToShortDateString() + "' where  helpdesk_ticket_id='" + ticket.transaction_id + "' ", cn);
                        cmd5.CommandType = System.Data.CommandType.Text;
                        cmd5.ExecuteNonQuery();

                        if((ticket.developer != "Select" && ticket.developer != "" && ticket.developer != null) && ticket.record_status == "R")
                        {
                            NpgsqlCommand cmd7 = new NpgsqlCommand("select (EXTRACT(EPOCH FROM ticket_end_dev) - EXTRACT(EPOCH FROM ticket_start_dev))/3600 as timetaken FROM exciseautomation.helpdesk_ticket where helpdesk_ticket_id='" + ticket.transaction_id + "'", cn);
                            int t = Convert.ToInt32(cmd7.ExecuteScalar());
                            NpgsqlCommand cmd6;
                            cmd6 = new NpgsqlCommand(" update exciseautomation.helpdesk_ticket set timetaken_dev='"+t+"'  where  helpdesk_ticket_id='" + ticket.transaction_id + "' ", cn);
                            cmd6.CommandType = System.Data.CommandType.Text;
                            cmd6.ExecuteNonQuery();
                        }
                        if ((ticket.tester != "Select" && ticket.tester != "" && ticket.tester != null) && ticket.record_status == "C")
                        {
                            NpgsqlCommand cmd9 = new NpgsqlCommand("select (EXTRACT(EPOCH FROM ticket_end_tester) - EXTRACT(EPOCH FROM ticket_start_tester))/3600 as timetaken FROM exciseautomation.helpdesk_ticket where helpdesk_ticket_id='" + ticket.transaction_id + "'", cn);
                            int tt = Convert.ToInt32(cmd9.ExecuteScalar());
                            NpgsqlCommand cmd8;
                            cmd8 = new NpgsqlCommand(" update exciseautomation.helpdesk_ticket set timetaken_tester='" + tt + "'  where  helpdesk_ticket_id='" + ticket.transaction_id + "' ", cn);
                            cmd8.CommandType = System.Data.CommandType.Text;
                            cmd8.ExecuteNonQuery();
                        }

                            VAL = "0";
                        
                          ticket.hd_ticket_history_id = ticket.transaction_id;

                        cn.Close();
                    }
                    else
                    {
                        VAL = "1";
                      
                    }
                }
                catch (Exception ex1)
                {
                    VAL = ex1.Message;
                }
            }
            return VAL;
        }

        public static List<TicketHistory> GetDetailsHistory(int id)
        {
            List<TicketHistory> record1 = new List<TicketHistory>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,c.user_name,d.doc_path from exciseautomation.hd_ticket_history a  inner join exciseautomation.helpdesk_ticket b on a.transaction_id= b.helpdesk_ticket_id inner join exciseautomation.user_registration c on a.user_id=c.user_id left join exciseautomation.hd_docs d on a.hd_ticket_history_id=d.doc_id where  transaction_id='" + id + "'", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        record1 = new List<TicketHistory>();
                        while (dr.Read())
                        {
                            TicketHistory record = new TicketHistory();
                            record.hd_ticket_history_id = Convert.ToInt32(dr["hd_ticket_history_id"].ToString());
                            record.transaction_id = Convert.ToInt32(dr["transaction_id"].ToString());
                            record.transaction_date = Convert.ToDateTime(dr["transaction_date"].ToString()).ToString("dd/MM/yyyy");
                            record.createdby_id = dr["createdby_id"].ToString();
                            record.creation_date = Convert.ToDateTime(dr["creation_date"].ToString()).ToString("dd/MM/yyyy");
                            record.remarks = dr["remarks"].ToString();
                            record.user_name= dr["user_name"].ToString();
                            record.path= dr["doc_path"].ToString();
                            record.creation_date = Convert.ToDateTime(dr["creation_date"].ToString()).ToString("dd/MM/yyyy");
                            record.lastmodified_date = Convert.ToDateTime(dr["lastmodified_date"].ToString()).ToString("dd/MM/yyyy");
                            record.user_id = dr["user_id"].ToString();
                            record1.Add(record);
                        }


                    }
                }

                catch (Exception ex)
                {
                    throw (ex);
                }
                return record1;
            }
        }

    }
}
