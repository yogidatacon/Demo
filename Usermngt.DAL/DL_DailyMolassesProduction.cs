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
  public  class DL_DailyMolassesProduction
    {
        public static List<DailyMolassesProduction_e> GetDailyMolassesList(string userid)
        {
            List<DailyMolassesProduction_e> productions = new List<DailyMolassesProduction_e>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select  t.*,a.uom_name from exciseautomation.uom_master a inner join  exciseautomation.vat_master t on a.uom_code=t.uom_code order by t.vat_name", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        productions = new List<DailyMolassesProduction_e>();

                        while (dr.Read())
                        {
                            DailyMolassesProduction_e production = new DailyMolassesProduction_e();
                            production.vat_code = dr["vat_code"].ToString();
                            production.vat_name = dr["vat_name"].ToString();
                            production.vat_totalcapacity =Convert.ToDouble( dr["vat_totalcapacity"].ToString());
                            production.storage_content = dr["storage_content"].ToString();
                            production.uom_code = dr["uom_code"].ToString();
                            production.uom_name = dr["uom_name"].ToString();
                            production.user_id = userid;
                            productions.Add(production);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return productions;
            }
        }

        public static List<DailyMolassesProduction_e> GetMolassesActionListWithDate(string party_code, string entrydate)
        {
            List<DailyMolassesProduction_e> Actions = new List<DailyMolassesProduction_e>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select  distinct t.*,a.uom_name,b.dailyproduction,b.brix,b.dailymolassesproduction_id,b.record_status as ststus,b.remarks from exciseautomation.uom_master a inner join exciseautomation.vat_master t on a.uom_code = t.uom_code inner join exciseautomation.dailymolassesproduction b on b.vat_code = t.vat_code  where b.party_code = '" + party_code + "' and entrydate='"+entrydate+ "'  order by b.dailymolassesproduction_id", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dmp = new DataTable();
                    dmp.Load(dr1);
                    dr1.Close();
                    int n = 0;
                    foreach (DataRow dr in dmp.Rows)
                    {
                        DailyMolassesProduction_e action = new DailyMolassesProduction_e();
                        action.dailymolassesproduction_id = dr["dailymolassesproduction_id"].ToString();
                        action.vat_code = dr["vat_code"].ToString();
                        action.vat_name = dr["vat_name"].ToString();
                        action.storage_content = dr["storage_content"].ToString();
                        action.vat_totalcapacity = Convert.ToDouble(dr["vat_totalcapacity"].ToString());
                        action.uom_code = dr["uom_code"].ToString();
                        action.uom_name = dr["uom_name"].ToString();
                        action.remarks = dr["remarks"].ToString();
                        action.record_status = dr["ststus"].ToString();
                        if (dr["dailyproduction"].ToString() != "")
                            action.dailyproduction = Convert.ToDouble(dr["dailyproduction"].ToString());
                        else
                            action.dailyproduction = 0;
                        if (dr["brix"].ToString() != "")
                            action.brix = dr["brix"].ToString();
                        else
                            action.brix = "0";
                        action.user_id = dr["User_id"].ToString();
                        if (n == 0)
                        {
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.eascm_docs where doc_id='" + action.dailymolassesproduction_id + "' and doc_type_code='DMP' order by eascm_docs_id", cn))
                            {
                                cmd1.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr2 = cmd1.ExecuteReader();
                                action.docs = new List<EASCM_DOCS>();
                                if (dr2.HasRows)
                                {
                                    while (dr2.Read())
                                    {
                                        EASCM_DOCS doc = new EASCM_DOCS();
                                        doc.id = Convert.ToInt32(dr2["eascm_docs_id"].ToString());
                                        doc.doc_id = dr2["doc_id"].ToString();
                                        doc.doc_name = dr2["doc_Name"].ToString();
                                        doc.description = dr2["doc_desc"].ToString();
                                        doc.doc_path = dr2["doc_path"].ToString();
                                        action.docs.Add(doc);
                                    }
                                }
                                dr2.Close();
                            }
                        }
                        Actions.Add(action);
                        n++;
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return Actions;
            }
        }

        public static DailyMolassesProduction_e GetProductionQTY(string partycode)
        {
           DailyMolassesProduction_e production = new DailyMolassesProduction_e();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    string[] val = partycode.Split('_');
                    NpgsqlCommand cmd = new NpgsqlCommand("select case when sum(a.vat_availablecapacity) is null then 0 else sum(a.vat_availablecapacity) end as dailyproduction,case when sum(b.openingbalancevalue) is null then 0 else sum(b.openingbalancevalue) end as openingbalancevalue from  exciseautomation.vat_master a left join exciseautomation.openingbalance b on a.vat_code=b.vat_code where a.party_code='" + val[0]+ "' and a.storage_content='"+val[1]+ "' and b.record_status='A' ", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            production.dailyproduction = Convert.ToDouble(dr["dailyproduction"].ToString());// + Convert.ToDouble(dr["openingbalancevalue"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return production;
            }
        }

        public static string Approve(string party_code, string edate, string remarks, string recordstatus,string molassesproduction_id,string userid, List<DailyMolassesProduction_e> production)
        {
            string value;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    if (recordstatus == "A")
                    {
                        for (int i = 0; i < production.Count; i++)
                        {
                            NpgsqlCommand cmd1 = new NpgsqlCommand("select case when vat_availablecapacity is null then 0 else vat_availablecapacity end vat_availablecapacity from  exciseautomation.vat_master  WHERE party_code='" + party_code + "' and vat_code='" + production[i].vat_code + "'", cn);
                            double available = Convert.ToDouble(cmd1.ExecuteScalar());
                            available = available + production[i].dailyproduction;
                            cmd1 = new NpgsqlCommand("UPDATE exciseautomation.vat_master SET  vat_availablecapacity='" + available + "' WHERE party_code='" + party_code + "' and vat_code='" + production[i].vat_code + "'", cn);
                            int G = cmd1.ExecuteNonQuery();
                            StringBuilder str2 = new StringBuilder();
                            str2.Append("INSERT INTO exciseautomation.transactionhistory(transaction_id, transaction_date, transaction_type, transaction_format, party_code, from_vat,  add_qty, user_id, creation_date,financial_year)Values(");
                            str2.Append("'" + production[i].dailymolassesproduction_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','DMP','" + production[i].dailymolassesproduction_id + "','" + production[i].party_code + "','" + production[i].vat_code + "','" + production[i].dailyproduction + "','" + production[i].user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+production[i].financial_year+"')");
                            cmd1 = new NpgsqlCommand(str2.ToString(), cn);
                            cmd1.ExecuteNonQuery();
                             cmd1 = new NpgsqlCommand("UPDATE exciseautomation.dailymolassesproduction SET  record_status='" + recordstatus + "' WHERE party_code='" + party_code + "' and entrydate='" + edate + "' and vat_code='" + production[i].vat_code + "' and financial_year='"+production[i].financial_year+"'", cn);
                            int n = cmd1.ExecuteNonQuery();
                        }
                    }
                    if(recordstatus=="R")
                    {
                        for (int i = 0; i < production.Count; i++)
                        {
                            NpgsqlCommand cmd1 = new NpgsqlCommand("UPDATE exciseautomation.dailymolassesproduction SET  record_status='" + recordstatus + "' WHERE party_code='" + party_code + "' and entrydate='" + edate + "' and vat_code='" + production[i].vat_code + "'  and financial_year='" + production[i].financial_year + "'", cn);
                            int n = cmd1.ExecuteNonQuery();
                        }
                        recordstatus = "Rejected by Bond Officer";
                    }
                    else
                    {
                        recordstatus = "Approved by Bond Officer";
                    }
                    StringBuilder str = new StringBuilder();
                    str.Append("insert INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year,party_code) VALUES(");
                    str.Append("'" + molassesproduction_id + "','','" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "','DMP','" + recordstatus + "','" +remarks + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + userid + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + userid + "','"+production[0].financial_year+"','"+party_code+"')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    cmd.ExecuteNonQuery();
                    value = "0";
                    trn.Commit();
                    _log.Info("DMP Approve Sucess:" + party_code + '-' + edate);
                    cn.Close();
                }
                catch (Exception ex)
                {
                    value = ex.Message;
                    _log.Info("DMP Approve Fail:" + party_code + '-' + edate+"-"+remarks);
                    trn.Rollback();
                }
                return value;

            }
        }

        public static string InsertDailyMolasses(List<DailyMolassesProduction_e> production)
        {
            string value = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT case when max(dailymolassesproduction_id) is null then 0 else max(dailymolassesproduction_id)  end as dailymolassesproduction_id   FROM exciseautomation.dailymolassesproduction where financial_year='"+production[0].financial_year+"' ", cn);
                int m = Convert.ToInt32(cmd1.ExecuteScalar());
                m = m + 1;
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    for (int i = 0; i < production.Count; i++)
                    {
                        if(i==0)
                        {
                            for (int i1 = 0; i1 < production[i].docs.Count; i1++)
                            {
                               
                                StringBuilder str = new StringBuilder();
                                str.Append("INSERT INTO exciseautomation.eascm_docs(doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,financial_year,party_code)");
                                str.Append("Values('" + m + "','" + production[i].docs[i1].doc_name + "', '" + production[i].docs[i1].description + "','" + production[i].docs[i1].doc_path + "','DMP','" + production[i].user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+production[i].financial_year+"','"+production[i].party_code+"')");
                                NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                                int r=cmd3.ExecuteNonQuery();
                            }
                        }
                        if (production[i].dailyproduction > 0)
                        {
                            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.dailymolassesproduction(dailymolassesproduction_id, party_code, entrydate, dailyproduction, vat_code, lastmodified_date, user_id, creation_date, brix, record_status,remarks,financial_year) VALUES('" + m + "','" + production[i].party_code + "', '" + production[i].creation_date + "','" + production[i].dailyproduction + "', '" + production[i].vat_code + "', '" + DateTime.Now.ToString("dd-MM-yyyy") + "', '" + production[i].user_id + "', '" + DateTime.Now.ToString("dd-MM-yyyy") + "', '" + production[i].brix + "','" + production[i].record_status + "','" + production[i].remarks + "','"+production[i].financial_year+"') ", cn);
                            int n = cmd.ExecuteNonQuery();
                        }
                        m++;
                     
                    }
                    
                    value = "0";
                    trn.Commit();
                }
                catch (Exception ex)
                {
                    trn.Rollback();
                    value =ex.Message;
                  //  throw (ex);
                }

                return value;

            }


        }


        public static List<DalyMolasses_e> GetMolassesList(string userid)
        {
            List<DalyMolasses_e> productions = new List<DalyMolasses_e>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select distinct(c.party_name),t.financial_year,t.entrydate,t.party_code ,t.record_status,c.party_name,a.uom_name, sum(dailyproduction) as dailyproduction from  exciseautomation.dailymolassesproduction t inner join  exciseautomation.party_master c on c.party_code=t.party_code inner join  exciseautomation.vat_master e on e.vat_code=t.vat_code inner join exciseautomation.uom_master a on a.uom_code=e.uom_code where t.record_deleted is false  GROUP BY  c.party_name, t.entrydate ,t.financial_year,t.record_status,t.party_code,a.uom_name  ", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        productions = new List<DalyMolasses_e>();

                        while (dr.Read())
                        {
                            DalyMolasses_e production1 = new DalyMolasses_e();
                            production1.party_code = dr["party_code"].ToString();
                            production1.party_name = dr["party_name"].ToString();
                            production1.financial_year = dr["financial_year"].ToString();
                            production1.dailyproduction = Convert.ToDouble(dr["dailyproduction"].ToString());
                            production1.entrydate = dr["entrydate"].ToString().Substring(0,10).Replace("/","-");
                            // production1.uom_code = dr["uom_code"].ToString();
                            production1.uom_name = dr["uom_name"].ToString();
                            production1.user_id = userid;
                            production1.record_status = dr["record_status"].ToString();
                            productions.Add(production1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return productions;
            }
        }
        public static List<DalyMolasses_e> Search(string tablename, string column, string value)
        {
            List<DalyMolasses_e> scplist = new List<DalyMolasses_e>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select distinct(c.party_name),t.entrydate,t.party_code,t.user_id ,t.record_status,c.party_name,a.uom_name, sum(dailyproduction) as dailyproduction from  exciseautomation.dailymolassesproduction t inner join  exciseautomation.party_master c on c.party_code=t.party_code inner join  exciseautomation.vat_master e on e.vat_code=t.vat_code inner join exciseautomation.uom_master a on a.uom_code=e.uom_code where t.record_deleted is false and " + column + " Ilike '%" + value + "%' and a.record_active='true'  GROUP BY  c.party_name, t.entrydate ,t.record_status,t.party_code,t.user_id,a.uom_name", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            scplist = new List<DalyMolasses_e>();
                            while (dr.Read())
                            {
                                DalyMolasses_e production1 = new DalyMolasses_e();
                                production1.party_code = dr["party_code"].ToString();
                                production1.party_name = dr["party_name"].ToString();
                                production1.financial_year = dr["financial_year"].ToString();
                                production1.dailyproduction = Convert.ToDouble(dr["dailyproduction"].ToString());
                                production1.entrydate = dr["entrydate"].ToString().Substring(0, 10).Replace("/", "-");
                                // production1.uom_code = dr["uom_code"].ToString();
                                production1.uom_name = dr["uom_name"].ToString();
                                production1.user_id = dr["user_id"].ToString();
                                production1.record_status = dr["record_status"].ToString();
                                scplist.Add(production1);
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
            return scplist;
        }

        public static List<DailyMolassesProduction_e> GetActionList(string party_code,string edate)
        {
            List<DailyMolassesProduction_e> Actions = new List<DailyMolassesProduction_e>();
            List<EASCM_DOCS> docs = new List<EASCM_DOCS>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    // NpgsqlCommand cmd = new NpgsqlCommand("select  distinct t.*,a.uom_name,b.dailyproduction,b.brix,b.dailymolassesproduction_id,b.record_status as status,b.remarks,d1.product_name from exciseautomation.uom_master a inner join exciseautomation.vat_master t on a.uom_code = t.uom_code inner join exciseautomation.dailymolassesproduction b on b.vat_code = t.vat_code inner join exciseautomation.product_master d1 on d1.product_code = t.storage_content where b.party_code = '" + party_code+ "' and b.entrydate = '"+edate+ "' and b.record_deleted=false  order by b.dailymolassesproduction_id", cn);
                    NpgsqlCommand cmd = new NpgsqlCommand("select  distinct a.*,b.dailyproduction,b.brix,b.dailymolassesproduction_id,b.record_status as status,b.remarks,b.user_id from exciseautomation.view_dmpvat a  inner join exciseautomation.dailymolassesproduction b on b.vat_code = a.vat_code and a.party_code=b.party_code and a.financial_year=b.financial_year where b.party_code = '" + party_code + "' and b.entrydate = '" + edate + "' and b.record_deleted=false  order by b.dailymolassesproduction_id", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dmp = new DataTable();
                    dmp.Load(dr1);
                    dr1.Close();
                    int n = 0;
                    Actions = new List<DailyMolassesProduction_e>();
                    foreach (DataRow dr in dmp.Rows)
                    {
                        DailyMolassesProduction_e action = new DailyMolassesProduction_e();
                            action.dailymolassesproduction_id =dr["dailymolassesproduction_id"].ToString();
                            action.vat_code = dr["vat_code"].ToString();
                            action.vat_name = dr["vat_name"].ToString();
                            action.storage_content = dr["product_name"].ToString();
                        action.record_status = dr["status"].ToString();
                        action.vat_totalcapacity = Convert.ToDouble(dr["vat_totalcapacity"].ToString());
                        action.vat_availablecapacity = Convert.ToDouble(dr["vatavailable"].ToString());
                        action.uom_code = dr["uom_code"].ToString();
                            action.uom_name = dr["uom_name"].ToString();
                        action.remarks = dr["remarks"].ToString();
                       
                        if (dr["dailyproduction"].ToString() != "")
                            action.dailyproduction = Convert.ToDouble(dr["dailyproduction"].ToString());
                        else
                            action.dailyproduction = 0;
                        if (dr["brix"].ToString() != "")
                            action.brix = dr["brix"].ToString();
                        else
                            action.brix = "0";
                            action.user_id = dr["user_id"].ToString();
                            
                                using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.eascm_docs where doc_id='" + action.dailymolassesproduction_id + "' and doc_type_code='DMP' order by eascm_docs_id", cn))
                                {
                                    cmd1.CommandType = System.Data.CommandType.Text;
                                    //cmd.Parameters.AddWithValue("@UserID", userid);
                                    NpgsqlDataReader dr2 = cmd1.ExecuteReader();
                                  //  action.docs = new List<EASCM_DOCS>();
                                    if (dr2.HasRows)
                                    {
                                        while (dr2.Read())
                                        {
                                    EASCM_DOCS doc = new EASCM_DOCS();
                                    doc.id = Convert.ToInt32(dr2["eascm_docs_id"].ToString());
                                            doc.doc_id = dr2["doc_id"].ToString();
                                            doc.doc_name = dr2["doc_Name"].ToString();
                                            doc.description = dr2["doc_desc"].ToString();
                                            doc.doc_path = dr2["doc_path"].ToString();
                                            docs.Add(doc);
                                           
                                        }
                                    }
                                dr2.Close();
                                }
                           
                            Actions.Add(action);
                            n++;
                        }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                if(Actions.Count>0)
                Actions[0].docs = docs;
                return Actions;
            }
        }
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static string UpdateMolassis(List<DailyMolassesProduction_e> action)
        {
            string value;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    int doc = 0;
                    for (int i1 = 0; i1 < action.Count; i1++)
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand("select * from  exciseautomation.dailymolassesproduction WHERE vat_code='" + action[i1].vat_code + "' and entrydate='" + action[i1].creation_date + "' and financial_year='"+action[i1].financial_year+"' ", cn);
                        NpgsqlDataReader dr2 = cmd.ExecuteReader();
                        if (dr2.HasRows)
                        {
                            while (dr2.Read())
                            {
                                action[i1].dailymolassesproduction_id = dr2["dailymolassesproduction_id"].ToString();
                            }
                        }
                        dr2.Close();
                        if (action[i1].dailymolassesproduction_id != "" && action[i1].dailyproduction>0)
                        {
                            cmd = new NpgsqlCommand("UPDATE exciseautomation.dailymolassesproduction SET   dailyproduction ='" + action[i1].dailyproduction + "', record_status='"+action[i1].record_status+"', lastmodified_date ='" + DateTime.Now.ToString("dd-MM-yyyy") + "', brix ='" + action[i1].brix + "',remarks='" + action[i1].remarks + "' WHERE vat_code='" + action[i1].vat_code + "' and entrydate='" + action[i1].creation_date + "' and financial_year='" + action[i1].financial_year + "'  ", cn);
                            int n1 = cmd.ExecuteNonQuery();
                            if (action[i1].dailymolassesproduction_id != "" && doc==0)
                            {

                                NpgsqlCommand cmd1 = new NpgsqlCommand("delete from exciseautomation.eascm_docs where doc_id='" + action[i1].dailymolassesproduction_id + "' and doc_type_code='DMP' and financial_year='" + action[i1].financial_year + "'", cn);
                                cmd1.ExecuteNonQuery();
                                for (int i = 0; i < action[0].docs.Count; i++)
                                {

                                    StringBuilder str = new StringBuilder();
                                    str.Append("INSERT INTO exciseautomation.eascm_docs(doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,financial_year,party_code)");
                                    str.Append("Values('" + action[i1].dailymolassesproduction_id + "','" + action[0].docs[i].doc_name + "', '" + action[0].docs[i].description + "','" + action[0].docs[i].doc_path + "','DMP','" + action[0].user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+action[0].financial_year+"','"+action[0].party_code+"')");
                                    NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                                    cmd3.ExecuteNonQuery();
                                    doc = 1;
                                }
                               

                            }
                        }
                        else if (action[i1].dailymolassesproduction_id != "" && action[i1].dailyproduction == 0)
                        {
                            cmd = new NpgsqlCommand("UPDATE exciseautomation.dailymolassesproduction SET   dailyproduction ='" + action[i1].dailyproduction + "',record_deleted=true WHERE vat_code='" + action[i1].vat_code + "' and entrydate='" + action[i1].creation_date + "' and financial_year='" + action[i1].financial_year + "'  ", cn);
                            int n1 = cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            if (action[i1].dailyproduction > 0)
                            {
                                NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT case when max(dailymolassesproduction_id) is null then 0 else max(dailymolassesproduction_id)  end as dailymolassesproduction_id   FROM exciseautomation.dailymolassesproduction where  financial_year='" + action[i1].financial_year + "' ", cn);
                                int m = Convert.ToInt32(cmd1.ExecuteScalar())+1;
                                m = m + 1;
                                NpgsqlCommand cmd2 = new NpgsqlCommand("INSERT INTO exciseautomation.dailymolassesproduction(dailymolassesproduction_id, party_code, entrydate, dailyproduction, vat_code, lastmodified_date, user_id, creation_date, brix, record_status,remarks,financial_year) VALUES('" + m + "','" + action[i1].party_code + "', '" + action[i1].creation_date + "','" + action[i1].dailyproduction + "', '" + action[i1].vat_code + "', '" + DateTime.Now.ToString("dd-MM-yyyy") + "', '" + action[i1].user_id + "', '" + DateTime.Now.ToString("dd-MM-yyyy") + "', '" + action[i1].brix + "','" + action[i1].record_status + "','" + action[i1].remarks + "','"+action[i1].financial_year+"') ", cn);
                                cmd2.ExecuteNonQuery();
                               
                            }
                        }
                    }
                    value = "0";
                    trn.Commit();
                    _log.Info("Sugarcanepurchase Insertion Sucess:" + action[0].dailymolassesproduction_id + '-' + action[0].party_code);
                    cn.Close();
                }
                catch (Exception ex)
                {
                    value = ex.Message;
                    _log.Info("Sugarcanepurchase Insertion Fail:" + action[0].dailymolassesproduction_id + '-' + action[0].party_code);
                    trn.Rollback();
                }
                return value;

            }
        }

        public static List<DailyMolassesProduction_e> GetPartyList(string userid, string financial_year)
        {
            List<DailyMolassesProduction_e> partylist = new List<DailyMolassesProduction_e>();
            DailyMolassesProduction_e production = new DailyMolassesProduction_e();

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    if (userid=="Admin")
                        // cmd = new NpgsqlCommand("select  t.*,a.uom_name,c.party_name,d.product_name from exciseautomation.uom_master a inner join  exciseautomation.vat_master t on a.uom_code=t.uom_code inner join  exciseautomation.party_master c on c.party_code=t.party_code inner join exciseautomation.product_master d on d.product_code = t.storage_content  order by c.party_name", cn);
                        cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.view_dmpvat", cn);
                    else
                        //  cmd = new NpgsqlCommand("select  t.*,a.uom_name,c.party_name,d.product_name from exciseautomation.uom_master a inner join  exciseautomation.vat_master t on a.uom_code=t.uom_code  inner join  exciseautomation.party_master c on c.party_code=t.party_code inner join exciseautomation.product_master d on d.product_code = t.storage_content  where  t.party_code='" + userid + "' order by t.vat_name", cn);
                        // cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.view_dmpvat  where party_code='" + userid + "' and financial_year='"+financial_year+"' order by vat_name", cn);
                        cmd = new NpgsqlCommand("select  vat_code, party_name,vat_name,party_code, vat_totalcapacity,vat_availablecapacity,storage_content,uom_code,product_name, uom_name,case when production isnull then openingbalancevalue else production end  as production ,case when issue  isnull then 0 else issue end  as issue,(case when production isnull then openingbalancevalue else production end   - case when issue  isnull then 0 else issue end ) as  vatavailable from  exciseautomation.vat_qty('" + financial_year+"','"+userid+"')", cn);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        partylist = new List<DailyMolassesProduction_e>();
                        while (dr.Read())
                        {
                            DailyMolassesProduction_e productions = new DailyMolassesProduction_e();
                            productions.party_code = dr["party_code"].ToString();
                            productions.party_name = dr["party_name"].ToString();
                            productions.vat_code = dr["vat_code"].ToString();
                            productions.vat_name = dr["vat_name"].ToString();
                            productions.vat_totalcapacity =Convert.ToDouble(dr["vat_totalcapacity"].ToString());
                            if (dr["vatavailable"].ToString() != null && dr["vatavailable"].ToString() != "")
                                productions.vat_availablecapacity = Convert.ToDouble(dr["vatavailable"].ToString());
                            else
                                production.vat_availablecapacity = 0;
                            productions.storage_content = dr["product_name"].ToString();
                            productions.uom_code = dr["uom_code"].ToString();
                            productions.uom_name = dr["uom_name"].ToString();
                            productions.user_id = userid;
                            partylist.Add(productions);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return partylist;

            }
        }

    }
}
