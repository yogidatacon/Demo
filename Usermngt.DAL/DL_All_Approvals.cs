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
   public class DL_All_Approvals
    {
        public static List<All_Approvals> GetApprovals(string userid,string value,string tranction_type)
        {
            List<All_Approvals> approvals = new List<All_Approvals>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    if (value != "")
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand("select a.*,c.role_name from exciseautomation.transaction_history a left join exciseautomation.user_registration b on a.user_id=b.user_id left join exciseautomation.role_master c on b.role_name_code=c.role_name_code where a.record_id='" + value + "' and a.transaction_type='" + tranction_type + "' order by transaction_date", cn);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            approvals = new List<All_Approvals>();
                            while (dr.Read())
                            {
                                All_Approvals app = new All_Approvals();
                                app.transaction_date = Convert.ToDateTime( dr["transaction_date"].ToString()).ToString("dd-MM-yyyy");
                                app.transaction_type = dr["transaction_type"].ToString();
                                app.transaction_state = dr["transaction_state"].ToString();
                                app.remarks = dr["remarks"].ToString();
                                app.record_id_format = dr["record_id_format"].ToString();
                                app.role_name = dr["role_name"].ToString();
                                app.financial_year= dr["financial_year"].ToString();
                                app.party_code = dr["party_code"].ToString();
                                if (app.role_name == "" && tranction_type == "Pass")
                                    app.role_name = "Bond Officer";
                                app.user_id= dr["user_id"].ToString();
                                approvals.Add(app);
                            }
                        }
                        dr.Close();
                    }
                  
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return approvals;
            }
        }
        public static string Insert(All_Approvals approvals)
        {
            string val;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("INTO exciseautomation.transaction_history(record_id, record_id_format, transaction_date, transaction_type, transaction_state, remarks, lastmodified_date, createdby_id, creation_date, user_id,financial_year) VALUES(");
                    str.Append("'"+approvals.record_id+"','"+approvals.record_id_format+"','"+approvals.transaction_date+"','"+approvals.transaction_type+"','"+approvals.transaction_state+"','"+approvals.remarks+"','"+DateTime.Now.ToString("dd-MM-yyyy")+"','"+approvals.user_id+"','"+ DateTime.Now.ToString("dd-MM-yyyy")+"','"+approvals.user_id+"','"+approvals.financial_year+"')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n= cmd.ExecuteNonQuery();
                    if(n==1)
                    {
                        val = "0";
                    }
                    else
                    {
                        val = "1";
                    }
                }
                catch (Exception ex)
                {
                    val = "2";
                  
                }
                return val;
            }
        }
    }
}
