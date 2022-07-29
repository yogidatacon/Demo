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
    public class DL_Org_List
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static List<Org_Master> GetListValues(string userid)
        {

            List<Org_Master> orglist = new List<Org_Master>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.org_name,a.org_id,a.org_type,a.org_desc,a.org_address,b.status from exciseautomation.org_master a left join exciseautomation.org_financial_yr b on a.org_id=b.org_id order by a.org_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            orglist = new List<Org_Master>();
                            while (dr.Read())
                            {
                                Org_Master record = new Org_Master();
                                record.org_name = dr["org_name"].ToString();
                                record.org_id = dr["org_id"].ToString();
                                record.org_type = dr["org_type"].ToString();
                                record.org_desc = dr["org_desc"].ToString();
                                record.org_address = dr["org_address"].ToString();
                                record.status = dr["status"].ToString();
                                orglist.Add(record);
                            }
                        }
                    }
                    _log.Info("Org Get List Success");
                    cn.Close();
                }
                catch (Exception ex)
                {
                    _log.Info("Org Get Fail:"+ ex.Message);
                }

            }
            return orglist;
        }

        public static List<Org_Master> SearchOrg(string tablename, string column, string value)
        {
            List<Org_Master> orglist = new List<Org_Master>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.org_name,a.org_id,a.org_type,a.org_desc,a.org_address,b.status from exciseautomation.org_master a left join exciseautomation.org_financial_yr b on a.org_id=b.org_id  where a." + column + " Ilike '%" + value + "%' order by a.org_name", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            orglist = new List<Org_Master>();
                            while (dr.Read())
                            {
                                Org_Master record = new Org_Master();
                                record.org_name = dr["org_name"].ToString();
                                record.org_id = dr["org_id"].ToString();
                                record.org_type = dr["org_type"].ToString();
                                record.org_desc = dr["org_desc"].ToString();
                                record.org_address = dr["org_address"].ToString();
                                record.status = dr["status"].ToString();
                                orglist.Add(record);
                            }
                        }
                    }
                    _log.Info("Org Get List Success");
                    cn.Close();
                }
                catch (Exception ex)
                {
                    _log.Info("Org Get Fail:" + ex.Message);
                }

            }
            return orglist;
        }

        public static Org_Master GetOrg_Details(string org_id)
        {
            Org_Master org = new Org_Master();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.org_master where org_id='"+ org_id+"' ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            
                            while (dr.Read())
                            {

                                org.org_name = dr["org_name"].ToString();
                                org.org_id = dr["org_id"].ToString();
                                org.org_type = dr["org_type"].ToString();
                                org.org_desc = dr["org_desc"].ToString();
                                org.org_address = dr["org_address"].ToString();
                                org.gst = dr["gst"].ToString();
                                org.pan = dr["pan"].ToString();
                                org.tan = dr["tan"].ToString();
                                org.tin = dr["tin"].ToString();
                                //org.start_date =Convert.ToDateTime( dr["start_date"].ToString());
                                //org.end_date = Convert.ToDateTime(dr["end_date"].ToString());
                                org.cont_number= dr["cont_number"].ToString();
                                org.email_id = dr["email_id"].ToString();
                                org.lastmodified_date = Convert.ToDateTime(dr["lastmodified_date"].ToString());
                                org.org_code= dr["org_code"].ToString();
                              
                               
                            }
                        }
                    }
                    _log.Info("Org Get GetOrg_Details Success");
                    cn.Close();
                }
                catch(Exception ex)
                {
                    _log.Info("Org Get GetOrg_Details Success:"+ex.Message);
                }

            }
            return org;
        }
        public static bool UpdateOrg_Details(Org_Master org)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    string query = "update exciseautomation.org_master set end_date='"+ org.end_date.ToShortDateString()+ "', lastmodified_date='" + DateTime.Now.ToString("dd/MM/yyyy") + "', org_address='" + org.org_address+ "',  org_desc='" + org.org_desc + "', org_name='" + org.org_name + "', org_type='" + org.org_type + "', pan='" + org.pan + "',gst='"+org.gst+"', start_date='" + org.start_date.ToShortDateString() + "', status='update', tan='" + org.tan + "', tin='" + org.tin + "' where org_id='" + org.org_id+"'";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                    }
                    else
                        value = false;
                    cn.Close();
                    _log.Info("Update Org_Details Success :"+org.org_id);
                }
                catch (Exception ex1)
                {
                    _log.Info("Update Org_Details Fail :" + org.org_id+"-"+ex1.Message);
                    value = false;
                }

            }
            return value;
           
        }
        public static bool InsertOrg_Details(Org_Master org)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    int max = Convert.ToInt32(DL_Org_List.GetMaxID("org_master").ToString()) + 1;
                    org.org_code = "ORG" + String.Format("{0:0000}", max);
                    StringBuilder str = new StringBuilder();
                    str.Append("INSERT INTO exciseautomation.org_master(creation_date,  lastmodified_date, org_address, org_code, org_desc, org_id, org_name, org_type,gst, pan, start_date, status, tan, tin, user_id, cont_number, email_id)");
                    str.Append("VALUES('"+DateTime.Now.ToString("dd/MM/yyyy")+"','"+DateTime.Now.ToString("dd/MM/yyyy")+"','"+org.org_address+"','"+org.org_code+"','"+org.org_desc+"','"+max+"','"+org.org_name+"','"+org.org_type+"','"+org.gst+"','"+org.pan+"','"+org.start_date.ToShortDateString()+"','"+org.status+"','"+org.tan+"','"+org.tin+"','"+org.user_id+"','"+org.cont_number+"','"+org.email_id+"')");
	
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                    }
                    else
                        value = false;
                    cn.Close();
                    _log.Info("Insertion Org_Details Success :" + org.org_id);
                }
                catch (Exception ex1)
                {
                    _log.Info("Insertion Org_Details Fail :" + org.org_id+"-"+ex1.Message);
                    value = false;
                }

            }
            return value;

        }
        public static bool UpdateOrgFinance(Org_Finacial_yr org)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    string query = "update exciseautomation.org_master set lastmodified_date='" + DateTime.Now.ToShortDateString() + "', org_name='" + org.org_name + "' where org_id='" + org.org_id + "'";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                    }
                    else
                        value = false;
                    cn.Close();
                    _log.Info("Update OrgFinance Success :" + org.org_id);
                }
                catch (Exception ex1)
                {
                    _log.Info("Update OrgFinance Fail :" + org.org_id+"-"+ex1.Message);
               
                value = false;
                }

            }
            return value;

        }
        public static bool InsertOrgFinance(Org_Finacial_yr org)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd1 = new NpgsqlCommand("update exciseautomation.org_financial_yr set status='Inactive' where org_id='"+org.org_id+"'", cn);
                    cmd1.ExecuteNonQuery();
                    org.user_id = "admin";
                    StringBuilder str = new StringBuilder();
                    str.Append("INSERT INTO exciseautomation.org_financial_yr(lastmodified_date,financial_year, org_id,  status)");
                    str.Append("VALUES('" + DateTime.Now.ToString("dd/MM/yyyy") + "','"+org.financial_year+"','" + org.org_id + "','Active')");

                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                    }
                    else
                        value = false;
                    cn.Close();
                    _log.Info("Insertion OrgFinance Success :" + org.org_id);
                
                }
                catch (Exception ex1)
                {
                    _log.Info("Insertion OrgFinance Success :" + org.org_id+"-"+ex1.Message);
                    value = false;
                }

            }
            return value;

        }
        //public static string GetMaxID(string tablename)
        //{
        //    string value ="";
        //    using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
        //    {
        //        cn.Open();
        //        try
        //        {
        //            string id = "org_id";
        //            if(tablename=="role_master")
        //            {
        //                id = "role_master_id";
        //            }
        //            if (tablename == "role_level_master")
        //            {
        //                id = "role_level_code";
        //            }
        //            if (tablename == "Role_level_master")
        //            {
        //                id = "role_level_code";
        //            }
        //            if (tablename == "access_type_master")
        //            {
        //                id = "access_type_code";
        //            }
        //            if (tablename == "vat_master")
        //            {
        //                id = "vat_seqno";
        //            }
        //            if (tablename == "Module_master")
        //            {
        //                id = "mns_no";
        //            }
        //            if (tablename == "sugarcanepurchase")
        //            {
        //                id = "sugarcanepurchase_id";
        //            }
        //            if (tablename == "openingbalance")
        //            {
        //                id = "openingbalance_id";
        //            }
        //            NpgsqlCommand cmd = new NpgsqlCommand("Select Max("+id+") orgid from exciseautomation."+tablename+"", cn);
        //            value = cmd.ExecuteScalar().ToString();
        //            if(value=="")
        //            {
        //                value = "0";
        //            }
        //            cn.Close();
        //            _log.Info("Get MaxID Success :" + tablename);
        //        }
        //        catch (Exception ex1)
        //        {
        //            _log.Info("Get MaxID Success :" + tablename);
        //            value = "";
        //        }

        //    }
        //    return value;

        //}


        public static string GetMaxID(string tablename)
        {
            string value = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    string id = "org_id";
                    string[] ta = tablename.Split('&');
                    if (tablename == "role_master")
                    {
                        id = "role_name_code";
                    }
                    if (tablename == "role_level_master")
                    {
                        id = "role_level_code";
                    }
                    if (tablename == "Role_level_master")
                    {
                        id = "role_level_code";
                    }
                    if (tablename == "access_type_master")
                    {
                        id = "access_type_code";
                    }
                    if (tablename == "vat_master")
                    {
                        id = "vat_seqno";
                    }
                    if (tablename == "Module_master")
                    {
                        id = "mns_no";
                    }
                    if (tablename == "sugarcanepurchase")
                    {
                        id = "sugarcanepurchase_id";
                    }
                    if (tablename == "openingbalance")
                    {
                        id = "openingbalance_id";
                    }
                    if (tablename == "article_category_master")
                    {
                        id = "right(article_category_code,2)";
                    }
                    if (tablename == "article_sub_category_master")
                    {
                        id = "right(article_sub_category_code,3)";
                    }
                    if (tablename == "article_name_master")
                    {
                        id = "right(article_name_code,4)";
                    }
                    if (tablename == "caste_master")
                    {
                        id = "right(caste_code,4)";
                    }
                    if (tablename == "bail_type_master")
                    {
                        id = "right(bail_type_master_code,2)";
                    }
                    if (tablename == "vehicle_type_master")
                    {
                        id = "right(vehicle_type_code,2)";
                    }
                    if (tablename == "offence_master")
                    {
                        id = "right(offence_code,2)";
                    }
                    if (ta[0] == "article")
                    {

                        tablename = "seizure_excisable_articles where article_name_code='" + ta[1] + "'";
                        id = "right(article_name_code,4)";
                    }
                    if (ta[0] == "Property")
                    {

                        tablename = "seizure_propertydetails where property_type_code='" + ta[1] + "'";
                        id = "right(property_type_code,2)";
                    }
                    if (ta[0] == "property_type_master")
                    {
                        tablename = "property_type_master where property_type='" + ta[1] + "'";
                        id = "right(property_type_code,2)";
                    }
                    if (ta[0] == "designation")
                    {
                        tablename = "seizure_raiddetails where designation_code='" + ta[1] + "'";
                        id = "right(designation_code,2)";
                    }
                    if (ta[0] == "designation_type")
                    {
                        tablename = "designation_master where designation_type_code='" + ta[1] + "'";
                        id = "designation_master_id";
                    }
                    if (ta[0] == "designation_master")
                    {
                        //  tablename = "designation_master where designation_type_code='" + ta[1] + "'";
                        id = "right(designation_code,2)";
                    }
                    if (ta[0] == "seizure_bail")
                    {
                        tablename = "seizure_bail where bail_type_master_code='" + ta[1] + "'";
                        id = "seizure_bail_id";
                    }
                    if (ta[0] == "Court_master")
                    {
                        tablename = "seizure_bail where Court_master_code='" + ta[1] + "'";
                        id = "seizure_bail_id";
                    }
                    if (ta[0] == "vehicle_type")
                    {
                        tablename = "vehicle_type_master where vehicle_type_code='" + ta[1] + "'";
                        id = "right(vehicle_type_code,2)";
                    }
                    NpgsqlCommand cmd = new NpgsqlCommand("Select Max(" + id + ") orgid from exciseautomation." + tablename + "", cn);
                    value = cmd.ExecuteScalar().ToString();
                    if (value == "")
                    {
                        value = "0";
                    }
                    cn.Close();
                    _log.Info("Get MaxID Success :" + tablename);
                }
                catch (Exception ex1)
                {
                    _log.Info("Get MaxID Success :" + tablename);
                    value = "";
                }

            }
            return value;

        }
        public static List<Org_Finacial_yr> GetFinacListValues(string userid)
        {

            List<Org_Finacial_yr> FinacList = new List<Org_Finacial_yr>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.org_name,b.* from exciseautomation.org_Master a inner join exciseautomation.org_financial_yr b on a.org_id=b.org_id order by b.org_financial_yr_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            FinacList = new List<Org_Finacial_yr>();
                            while (dr.Read())
                            {
                                Org_Finacial_yr record = new Org_Finacial_yr();
                                record.org_name = dr["org_name"].ToString();
                                record.org_id = dr["org_id"].ToString();
                                record.lastmodified_date = Convert.ToDateTime(dr["lastmodified_date"].ToString());
                                record.status = dr["status"].ToString();
                                record.financial_year = dr["financial_year"].ToString();
                                record.slno =Convert.ToInt32( dr["org_financial_yr_id"].ToString());
                                FinacList.Add(record);
                            }
                        }
                    }

                    cn.Close();
                    _log.Info("GetFinacListValues Success");
                }
                catch (Exception ex)
                {
                    _log.Info("GetFinacListValues Success :"+ex.Message);
                }

            }
            return FinacList;
        }

        public static List<Org_Finacial_yr> SearchFinacialYear(string tablename, string column, string value)
        {
            List<Org_Finacial_yr> FinacList = new List<Org_Finacial_yr>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.org_name,b.* from exciseautomation.org_Master a inner join exciseautomation.org_financial_yr b on a.org_id=b.org_id where b." + column + " Ilike '%" + value + "%' order by b.org_financial_yr_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            FinacList = new List<Org_Finacial_yr>();
                            while (dr.Read())
                            {
                                Org_Finacial_yr record = new Org_Finacial_yr();
                                record.org_name = dr["org_name"].ToString();
                                record.org_id = dr["org_id"].ToString();
                                record.lastmodified_date = Convert.ToDateTime(dr["lastmodified_date"].ToString());
                                record.status = dr["status"].ToString();
                                record.financial_year = dr["financial_year"].ToString();
                                record.slno = Convert.ToInt32(dr["org_financial_yr_id"].ToString());
                                FinacList.Add(record);
                            }
                        }
                    }

                    cn.Close();
                    _log.Info("GetFinacListValues Success");
                }
                catch (Exception ex)
                {
                    _log.Info("GetFinacListValues Success :" + ex.Message);
                }

            }
            return FinacList;
        }
        public static List<Org_Finacial_yr> GetOrgnames(string userid)
        {

            List<Org_Finacial_yr> FinacList = new List<Org_Finacial_yr>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select Distinct org_id,org_name from exciseautomation.org_Master", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            FinacList = new List<Org_Finacial_yr>();
                            while (dr.Read())
                            {
                                Org_Finacial_yr record = new Org_Finacial_yr();
                                record.org_name = dr["org_name"].ToString();
                                record.org_id = dr["org_id"].ToString();
                                FinacList.Add(record);
                            }
                        }
                    }
                    _log.Info("Get OrgNames in Financial Form Success");
                    cn.Close();
                }
                catch (Exception ex)
                {
                    _log.Info("Get OrgNames in Financial Form Success :"+ex.Message);
                }

            }
            return FinacList;
        }
    }
}
