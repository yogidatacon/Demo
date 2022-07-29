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

    #region DL_cm_article_category
    public class DL_cm_article_category
    {
        public static bool InserArticleCategory(cm_article_category article_category)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    try
                    {

                        string tableName = "exciseautomation.article_category_master";
                        string _creation_date = DateTime.Now.ToString("dd/MM/yyyy");

                        //string columnNames = "article_category_code, article_category_name, lastmodified_date, user_id, creation_date, record_status, record_deleted";                       
                        //string input = article_category.article_category_code + "','" + article_category.article_category_name + "','" + article_category.lastmodified_date + "','" + article_category.user_id + "','" + _creation_date + "','" + article_category.record_status + "','" + article_category.record_deleted;

                        string columnNames = "article_category_code, article_category_name, lastmodified_date, user_id, creation_date, record_status, record_deleted";
                        string input = article_category.article_category_code + "','" + article_category.article_category_name + "','" + article_category.lastmodified_date + "','" + article_category.user_id + "','" + _creation_date + "','" + article_category.record_status + "','" + article_category.record_deleted;

                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            value = true;
                        }
                        else
                            value = false;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        value = false;
                    }
                }
            }
            return value;
        }
        public static List<cm_article_category> GetListILike(string texts, string colname)
        {
            List<cm_article_category> lstObj = new List<cm_article_category>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.article_category_master where " + colname + " Ilike '%" + texts + "%' order by article_category_code", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_article_category>();
                            while (dr.Read())
                            {
                                cm_article_category record = new cm_article_category();
                                record.article_category_master_id = Convert.ToInt32(dr["article_category_master_id"].ToString());
                                record.article_category_name = dr["article_category_name"].ToString();
                                record.article_category_code = dr["article_category_code"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
        public static bool UpdateArticle(cm_article_category article)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.article_category_master SET  article_category_code ='" + article.article_category_code + "', article_category_name ='" + article.article_category_name + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "', record_status ='" + article.record_status + "' WHERE article_category_master_id ='" + article.article_category_master_id + "'", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;

                    }
                    else
                    {
                        value = false;

                    }

                }
                catch (Exception ex)
                {
                    value = false;
                    throw (ex);

                }
            }
            return value;
        }

        public static List<cm_article_category> GetList()
        {
            List<cm_article_category> lstObj = new List<cm_article_category>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.article_category_master order by article_category_code", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_article_category>();
                            while (dr.Read())
                            {
                                cm_article_category record = new cm_article_category();
                                record.article_category_master_id = Convert.ToInt32(dr["article_category_master_id"].ToString());
                                record.article_category_name = dr["article_category_name"].ToString();
                                record.article_category_code = dr["article_category_code"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
    }
    #endregion DL_cm_article_category

    #region DL_cm_article_name
    public class DL_cm_article_name
    {
        public static bool InsertArticleName(cm_article_name article_name)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    try
                    {
                        string tableName = "exciseautomation.article_name_master";
                        string _creation_date = DateTime.Now.ToString("dd/MM/yyyy");

                        string columnNames = "article_name_code, article_sub_category_code, article_category_code, article_name, lastmodified_date, user_id, creation_date, record_status, record_deleted";
                        string input = article_name.article_name_code + "','" + article_name.article_sub_category_code + "','" + article_name.article_category_code + "','" + article_name.article_name + "','" + article_name.lastmodified_date + "','" + article_name.user_id + "','" + _creation_date + "','" + article_name.record_status + "','" + article_name.record_deleted;
                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            value = true;
                        }
                        else
                            value = false;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        value = false;
                    }
                }
            }
            return value;
        }
        public static List<cm_article_name> GetListILike(string texts, string colname)
        {
            List<cm_article_name> lstObj = new List<cm_article_name>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    if (colname == "article_category_name")
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.article_category_name,c.article_sub_category_name from exciseautomation.article_name_master a inner join exciseautomation.article_category_master b on a.article_category_code=b.article_category_code inner join exciseautomation.article_sub_category_master c on a.article_sub_category_code=c.article_sub_category_code where b." + colname + " Ilike '%" + texts + "%' order by article_name_code", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                lstObj = new List<cm_article_name>();
                                while (dr.Read())
                                {
                                    cm_article_name record = new cm_article_name();
                                    record.article_name_master_id = Convert.ToInt32(dr["article_name_master_id"].ToString());
                                    record.article_sub_category_code = dr["article_sub_category_code"].ToString();
                                    record.article_category_name = dr["article_category_name"].ToString();
                                    record.article_sub_category_name = dr["article_sub_category_name"].ToString();
                                    record.article_category_code = dr["article_category_code"].ToString();
                                    record.article_name = dr["article_name"].ToString();
                                    record.article_name_code = dr["article_name_code"].ToString();
                                    //record.record_status = dr["party_active"].ToString();
                                    lstObj.Add(record);
                                }
                            }
                        }

                    }
                    else if (colname == "article_sub_category_name")
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.article_category_name,c.article_sub_category_name from exciseautomation.article_name_master a inner join exciseautomation.article_category_master b on a.article_category_code=b.article_category_code inner join exciseautomation.article_sub_category_master c on a.article_sub_category_code=c.article_sub_category_code where c." + colname + " Ilike '%" + texts + "%' order by article_name_code", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                lstObj = new List<cm_article_name>();
                                while (dr.Read())
                                {
                                    cm_article_name record = new cm_article_name();
                                    record.article_name_master_id = Convert.ToInt32(dr["article_name_master_id"].ToString());
                                    record.article_sub_category_code = dr["article_sub_category_code"].ToString();
                                    record.article_category_name = dr["article_category_name"].ToString();
                                    record.article_sub_category_name = dr["article_sub_category_name"].ToString();
                                    record.article_category_code = dr["article_category_code"].ToString();
                                    record.article_name = dr["article_name"].ToString();
                                    record.article_name_code = dr["article_name_code"].ToString();
                                    //record.record_status = dr["party_active"].ToString();
                                    lstObj.Add(record);
                                }
                            }
                        }

                    }

                    else
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.article_category_name,c.article_sub_category_name from exciseautomation.article_name_master a inner join exciseautomation.article_category_master b on a.article_category_code=b.article_category_code inner join exciseautomation.article_sub_category_master c on a.article_sub_category_code=c.article_sub_category_code where a." + colname + " Ilike '%" + texts + "%' order by article_name_code", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                lstObj = new List<cm_article_name>();
                                while (dr.Read())
                                {
                                    cm_article_name record = new cm_article_name();
                                    record.article_name_master_id = Convert.ToInt32(dr["article_name_master_id"].ToString());
                                    record.article_sub_category_code = dr["article_sub_category_code"].ToString();
                                    record.article_category_name = dr["article_category_name"].ToString();
                                    record.article_sub_category_name = dr["article_sub_category_name"].ToString();
                                    record.article_category_code = dr["article_category_code"].ToString();
                                    record.article_name = dr["article_name"].ToString();
                                    record.article_name_code = dr["article_name_code"].ToString();
                                    //record.record_status = dr["party_active"].ToString();
                                    lstObj.Add(record);
                                }
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
        public static bool UpdateArticle(cm_article_name articlename)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.article_name_master SET  article_name_code ='" + articlename.article_name_code + "', article_sub_category_code ='" + articlename.article_sub_category_code + "', article_category_code ='" + articlename.article_category_code + "', article_name ='" + articlename.article_name + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "', record_status ='" + articlename.record_status + "' WHERE article_name_master_id ='" + articlename.article_name_master_id + "' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;

                    }
                    else
                    {
                        value = false;

                    }

                }
                catch (Exception ex)
                {
                    value = false;
                    throw (ex);

                }
            }
            return value;
        }


        public static List<cm_article_name> GetList()
        {
            List<cm_article_name> lstObj = new List<cm_article_name>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.article_category_name,c.article_sub_category_name from exciseautomation.article_name_master a left join exciseautomation.article_category_master b on a.article_category_code=b.article_category_code left join exciseautomation.article_sub_category_master c on a.article_sub_category_code=c.article_sub_category_code order by a.article_name", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_article_name>();
                            while (dr.Read())
                            {
                                cm_article_name record = new cm_article_name();
                                record.article_name_master_id = Convert.ToInt32(dr["article_name_master_id"].ToString());
                                record.article_sub_category_code = dr["article_sub_category_code"].ToString();
                                record.article_category_name = dr["article_category_name"].ToString();
                                record.article_sub_category_name = dr["article_sub_category_name"].ToString();
                                record.article_category_code = dr["article_category_code"].ToString();
                                record.article_name = dr["article_name"].ToString();
                                record.article_name_code = dr["article_name_code"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
    }
    #endregion DL_cm_article_name

    #region DL_cm_article_subcategory
    public class DL_cm_article_subcategory
    {
        public static bool InsertArticleSubCategory(cm_article_subcategory article_subcategory)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    try
                    {
                        //string tableName = "exciseautomation.article_sub_category_master";
                        //string _creation_date = DateTime.Now.ToString("dd/MM/yyyy");
                        ////article_sub_category_master_id, article_sub_category_code, article_sub_category_name, article_category_code, lastmodified_date, user_id, creation_date, record_status, record_deleted
                        //string columnNames = "article_sub_category_code, article_sub_category_name, article_category_code, lastmodified_date, user_id, creation_date, record_status, record_deleted";
                        //string input = article_subcategory.article_sub_category_code + "','" + article_subcategory.article_sub_category_name + "','" + article_subcategory.article_category_code + "','" + article_subcategory.lastmodified_date + "','" + article_subcategory.user_id + "','" + _creation_date + "','" + article_subcategory.record_status + "','" + article_subcategory.record_deleted;
                        //string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        //NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.article_sub_category_master(article_sub_category_code, article_sub_category_name, article_category_code, lastmodified_date, user_id, creation_date, record_status) VALUES( '" + article_subcategory.article_sub_category_code + "', '" + article_subcategory.article_sub_category_name + "', '" + article_subcategory.article_category_code + "', '" + DateTime.Now.ToShortDateString() + "','" + article_subcategory.user_id + "','" + DateTime.Now.ToShortDateString() + "','" + article_subcategory.record_status + "') ", cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            value = true;
                        }
                        else
                            value = false;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        value = false;
                    }
                }
            }
            return value;
        }
        public static List<cm_article_subcategory> GetListILike(string texts, string colname)
        {
            List<cm_article_subcategory> lstObj = new List<cm_article_subcategory>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    if (colname == "article_category_name")
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.article_category_name from exciseautomation.article_sub_category_master a inner join exciseautomation.article_category_master b on a.article_category_code=b.article_category_code  where b." + colname + " Ilike '%" + texts + "%' order by article_category_code", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                lstObj = new List<cm_article_subcategory>();
                                while (dr.Read())
                                {
                                    cm_article_subcategory record = new cm_article_subcategory();
                                    record.article_sub_category_master_id = Convert.ToInt32(dr["article_sub_category_master_id"].ToString());
                                    record.article_category_code = dr["article_category_code"].ToString();
                                    record.article_category_name = dr["article_category_name"].ToString();
                                    record.article_sub_category_name = dr["article_sub_category_name"].ToString();
                                    record.article_sub_category_code = dr["article_sub_category_code"].ToString();
                                    //record.record_status = dr["party_active"].ToString();
                                    lstObj.Add(record);
                                }
                            }
                        }
                    }

                    else
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.article_category_name from exciseautomation.article_sub_category_master a inner join exciseautomation.article_category_master b on a.article_category_code=b.article_category_code  where a." + colname + " Ilike '%" + texts + "%' order by article_category_code", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                lstObj = new List<cm_article_subcategory>();
                                while (dr.Read())
                                {
                                    cm_article_subcategory record = new cm_article_subcategory();
                                    record.article_sub_category_master_id = Convert.ToInt32(dr["article_sub_category_master_id"].ToString());
                                    record.article_category_code = dr["article_category_code"].ToString();
                                    record.article_category_name = dr["article_category_name"].ToString();
                                    record.article_sub_category_name = dr["article_sub_category_name"].ToString();
                                    record.article_sub_category_code = dr["article_sub_category_code"].ToString();
                                    //record.record_status = dr["party_active"].ToString();
                                    lstObj.Add(record);
                                }
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
        public static bool UpdateArticlesub(cm_article_subcategory article_subcategory)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.article_sub_category_master SET  article_sub_category_code='" + article_subcategory.article_sub_category_code + "', article_sub_category_name='" + article_subcategory.article_sub_category_name + "', article_category_code='" + article_subcategory.article_category_code + "', lastmodified_date='" + DateTime.Now.ToShortDateString() + "', record_status='" + article_subcategory.record_status + "' WHERE article_sub_category_master_id='" + article_subcategory.article_sub_category_master_id + "'", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;

                    }
                    else
                    {
                        value = false;

                    }

                }
                catch (Exception ex)
                {
                    value = false;
                    throw (ex);

                }
            }
            return value;
        }

        public static List<cm_article_subcategory> GetList()
        {
            List<cm_article_subcategory> lstObj = new List<cm_article_subcategory>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.article_category_name from exciseautomation.article_sub_category_master a inner join exciseautomation.article_category_master b on a.article_category_code=b.article_category_code order by article_sub_category_code ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_article_subcategory>();
                            while (dr.Read())
                            {
                                cm_article_subcategory record = new cm_article_subcategory();
                                record.article_sub_category_master_id = Convert.ToInt32(dr["article_sub_category_master_id"].ToString());
                                record.article_category_code = dr["article_category_code"].ToString();
                                record.article_category_name = dr["article_category_name"].ToString();
                                record.article_sub_category_name = dr["article_sub_category_name"].ToString();
                                record.article_sub_category_code = dr["article_sub_category_code"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
    }
    #endregion DL_cm_article_subcategory

    #region DL_cm_bail_type
    public class DL_cm_bail_type
    {
        public static bool InsertBailType(cm_bail_type bail_type)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    try
                    {
                        string tableName = "exciseautomation.bail_type_master";
                        string _creation_date = DateTime.Now.ToString("dd/MM/yyyy");

                        string columnNames = "bail_type_master_code, bail_type_master_name, lastmodified_date, user_id, creation_date, record_status, record_deleted";
                        string input = bail_type.bail_type_master_code + "','" + bail_type.bail_type_master_name + "','" + bail_type.lastmodified_date + "','" + bail_type.user_id + "','" + _creation_date + "','" + bail_type.record_status + "','" + bail_type.record_deleted;
                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            value = true;
                        }
                        else
                            value = false;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        value = false;
                    }
                }
            }
            return value;
        }
        public static List<cm_bail_type> GetListILike(string texts, string colname)
        {
            List<cm_bail_type> lstObj = new List<cm_bail_type>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.bail_type_master where " + colname + " Ilike '%" + texts + "%' order by bail_type_master_code", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_bail_type>();
                            while (dr.Read())
                            {
                                cm_bail_type record = new cm_bail_type();
                                record.bail_type_master_id = Convert.ToInt32(dr["bail_type_master_id"].ToString());
                                record.bail_type_master_name = dr["bail_type_master_name"].ToString();
                                record.bail_type_master_code = dr["bail_type_master_code"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
        public static bool UpdateBail(cm_bail_type Bail)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.bail_type_master SET  bail_type_master_code ='" + Bail.bail_type_master_code + "', bail_type_master_name ='" + Bail.bail_type_master_name + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "',  record_status ='" + Bail.record_status + "' WHERE bail_type_master_id ='" + Bail.bail_type_master_id + "' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;

                    }
                    else
                    {
                        value = false;

                    }

                }
                catch (Exception ex)
                {
                    value = false;
                    throw (ex);

                }
            }
            return value;
        }
        public static List<cm_bail_type> GetList()
        {
            List<cm_bail_type> lstObj = new List<cm_bail_type>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.bail_type_master order by bail_type_master_code ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_bail_type>();
                            while (dr.Read())
                            {
                                cm_bail_type record = new cm_bail_type();
                                record.bail_type_master_id = Convert.ToInt32(dr["bail_type_master_id"].ToString());
                                record.bail_type_master_name = dr["bail_type_master_name"].ToString();
                                record.bail_type_master_code = dr["bail_type_master_code"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
    }
    #endregion DL_cm_bail_type

    #region DL_cm_caste
    public class DL_cm_caste
    {
        public static bool InsertCaste(cm_caste caste)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    try
                    {
                        string tableName = "exciseautomation.caste_master";
                        string _creation_date = DateTime.Now.ToString("dd/MM/yyyy");
                        //caste_master_id, caste_code, caste_name, religion_code, lastmodified_date, user_id, creation_date, record_status, record_deleted
                        string columnNames = "caste_code, caste_name, category_code,religion_code, lastmodified_date, user_id, creation_date, record_status, record_deleted";
                        string input = caste.caste_code + "','" + caste.caste_name + "','" + caste.category_code + "','"+caste.religion_code+"','" + caste.lastmodified_date + "','" + caste.user_id + "','" + _creation_date + "','" + caste.record_status + "','" + caste.record_deleted;
                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            value = true;
                        }
                        else
                            value = false;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        value = false;
                    }
                }
            }
            return value;
        }
        public static List<cm_caste> GetListILike(string texts, string colname)
        {
            List<cm_caste> lstObj = new List<cm_caste>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    if (colname == "religion_name")
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.religion_name from exciseautomation.caste_master a inner join exciseautomation.religion_master b on a.religion_code=b.religion_code   where b." + colname + " Ilike '%" + texts + "%' order by b." + colname + "", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                lstObj = new List<cm_caste>();
                                while (dr.Read())
                                {
                                    cm_caste record = new cm_caste();
                                    record.caste_master_id = Convert.ToInt32(dr["caste_master_id"].ToString());
                                    record.caste_name = dr["caste_name"].ToString();
                                    record.caste_code = dr["caste_code"].ToString();
                                    record.category_code = dr["category_code"].ToString();
                                    record.religion_name = dr["religion_name"].ToString();
                                    record.religion_code = dr["religion_code"].ToString();
                                    lstObj.Add(record);
                                }
                            }
                        }
                    }

                    else
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.religion_name from exciseautomation.caste_master a inner join exciseautomation.religion_master b on a.religion_code=b.religion_code   where a." + colname + " Ilike '%" + texts + "%' order by a." + colname + "", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                lstObj = new List<cm_caste>();
                                while (dr.Read())
                                {
                                    cm_caste record = new cm_caste();
                                    record.caste_master_id = Convert.ToInt32(dr["caste_master_id"].ToString());
                                    record.caste_name = dr["caste_name"].ToString();
                                    record.caste_code = dr["caste_code"].ToString();
                                    record.religion_name = dr["religion_name"].ToString();
                                    record.religion_code = dr["religion_code"].ToString();
                                    record.category_code = dr["category_code"].ToString();
                                    lstObj.Add(record);
                                }
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
        public static bool UpdateCaste(cm_caste caste)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.caste_master SET  caste_code ='" + caste.caste_code + "', caste_name ='" + caste.caste_name + "', religion_code ='" + caste.religion_code + "',category_code='"+caste.category_code+"', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "', record_status ='" + caste.record_status + "' WHERE caste_master_id ='" + caste.caste_master_id + "' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;

                    }
                    else
                    {
                        value = false;

                    }

                }
                catch (Exception ex)
                {
                    value = false;
                    throw (ex);

                }
            }
            return value;
        }

        public static List<cm_caste> GetList()
        {
            List<cm_caste> lstObj = new List<cm_caste>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select distinct caste_code,caste_name,category_code,b.religion_code,b.religion_name,caste_master_id  from exciseautomation.caste_master a inner join exciseautomation.religion_master b on a.religion_code=b.religion_code  order by caste_code", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_caste>();
                            while (dr.Read())
                            {
                                cm_caste record = new cm_caste();
                                record.caste_master_id = Convert.ToInt32(dr["caste_master_id"].ToString());
                                record.caste_name = dr["caste_name"].ToString();
                                record.caste_code = dr["caste_code"].ToString();
                                record.religion_name = dr["religion_name"].ToString();
                                record.religion_code = dr["religion_code"].ToString();
                                record.category_code = dr["category_code"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
    }
    #endregion DL_cm_caste

    #region DL_cm_court
    public class DL_cm_court
    {

        //InsertExCaseRegEntry
        public static bool InsertExCaseRegEntry(cm_court obj)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    NpgsqlTransaction trn;
                    trn = cn.BeginTransaction();
                    try
                    {
                        int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.excase_registration", "excase_registration_id").ToString()) + 1;
                        StringBuilder str = new StringBuilder();
                        string tableName = "exciseautomation.excase_registration";
                        string columnNames = "";
                        string input = "";
                        
                            columnNames = "excase_registration_id,dmcase_registration_id,date_of_hearing,appealno,appealdate,appellant_name,appellant_contact,case_type,lastmodified_date, user_id, creation_date, record_status, record_deleted,confiscationorder,confiscationdate,remarks";
                            input = max + "','" + obj.dmcase_registration_id + "','" + obj.case_hearingdate + "','" + obj.appealno + "','" + obj.appealdate + "','" + obj.appellant_name + "','" + obj.appellant_contact + "','" + obj.case_type + "','" + DateTime.Now.ToShortDateString() + "','" + obj.user_id + "','" + DateTime.Now.ToShortDateString() + "','" + obj.record_status + "','" + obj.record_deleted + "','" + obj.confiscationorderno + "','" + obj.confiscationorderdate + "','" + obj.remarks;
                        

                        


                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            NpgsqlCommand cmd_u = new NpgsqlCommand("UPDATE exciseautomation.dmcase_hearing SET  record_status ='A' WHERE dmcase_registration_id ='" + obj.dmcase_registration_id + "' ", cn);
                            int ns = cmd_u.ExecuteNonQuery();

                            //for (int i = 0; i < obj.docs.Count; i++)
                            //{
                            //    n = 0;
                            //    str = new StringBuilder();
                            //    str.Append("INSERT INTO exciseautomation.seizure_docs(seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date)");
                            //    str.Append("Values('" + max + "','" + max + "','" + obj.docs[i].doc_name + "', '" + obj.docs[i].description + "','" + obj.docs[i].doc_path + "','DMCR','" + obj.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "')");
                            //    NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            //    n = cmd3.ExecuteNonQuery();
                            //}
                            value = true;
                            trn.Commit();
                            //_log.Info("Sugarcanepurchase Insertion Success:" + scp.sugarcanepurchase_id + '-' + scp.party_code);
                            cn.Close();
                        }
                        else
                        {
                            trn.Rollback();
                            value = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        trn.Rollback();
                        Console.Write(ex);
                        value = false;
                    }
                }
            }
            return value;
        }
        public static List<cm_court> GetListILike(string texts, string colname)
        {
            List<cm_court> lstObj = new List<cm_court>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.court_master where " + colname + " Ilike '%" + texts + "%' order by court_master_code", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_court>();
                            while (dr.Read())
                            {
                                cm_court record = new cm_court();
                                record.court_master_id = Convert.ToInt32(dr["court_master_id"].ToString());
                                record.court_master_name = dr["court_master_name"].ToString();
                                record.court_master_code = dr["court_master_code"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
        //InsertEXHEntry
        public static bool InsertEXHEntry(cm_court obj)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    NpgsqlTransaction trn;
                    trn = cn.BeginTransaction();
                    try
                    {
                        int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.excase_hearing", "excase_hearing_id").ToString()) + 1;
                        StringBuilder str = new StringBuilder();
                        string tableName = "exciseautomation.excase_hearing";
                        string columnNames = "";
                        string input = "";
                        if (obj.case_action == "1")
                        {
                            columnNames = "excase_hearing_id, excase_registration_id, lastmodified_date, user_id, creation_date, record_status, record_deleted,case_action,remarks";
                            input = max + "','" + obj.dmcase_registration_id + "','" + DateTime.Now.ToShortDateString() + "','" + obj.user_id + "','" + DateTime.Now.ToShortDateString() + "','" + obj.record_status + "','" + obj.record_deleted + "','" + obj.case_action + "','" + obj.remarks;
                        }

                        if (obj.case_action == "2")
                        {
                            columnNames = "excase_hearing_id, excase_registration_id,nexthearing_date, lastmodified_date, user_id, creation_date, record_status, record_deleted,case_action,remarks";
                            input = max + "','" + obj.dmcase_registration_id + "','" + obj.case_hearingdate + "','" + DateTime.Now.ToShortDateString() + "','" + obj.user_id + "','" + DateTime.Now.ToShortDateString() + "','" + obj.record_status + "','" + obj.record_deleted + "','" + obj.case_action + "','" + obj.remarks;
                        }


                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            //for (int i = 0; i < obj.docs.Count; i++)
                            //{
                            //    n = 0;
                            //    str = new StringBuilder();
                            //    str.Append("INSERT INTO exciseautomation.seizure_docs(seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date)");
                            //    str.Append("Values('" + max + "','" + max + "','" + obj.docs[i].doc_name + "', '" + obj.docs[i].description + "','" + obj.docs[i].doc_path + "','DMCR','" + obj.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "')");
                            //    NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            //    n = cmd3.ExecuteNonQuery();
                            //}
                            value = true;
                            trn.Commit();
                            //_log.Info("Sugarcanepurchase Insertion Success:" + scp.sugarcanepurchase_id + '-' + scp.party_code);
                            cn.Close();
                        }
                        else
                        {
                            trn.Rollback();
                            value = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        trn.Rollback();
                        Console.Write(ex);
                        value = false;
                    }
                }
            }
            return value;
        }

        //InsertDMHEntry

        public static bool InsertDmHCaseEntry(cm_court obj)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    NpgsqlTransaction trn;
                    trn = cn.BeginTransaction();
                    try
                    {
                        int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.dmcase_hearing", "dmcase_hearing_id").ToString()) + 1;
                        StringBuilder str = new StringBuilder();
                        string tableName = "exciseautomation.dmcase_hearing";
                        string columnNames = "";
                        string input = "";
                        if (obj.case_action == "1")
                        {
                            columnNames = "dmcase_hearing_id, dmcase_registration_id, lastmodified_date, user_id, creation_date, record_status, record_deleted,case_action,confiscationorder,confiscationdate,remarks";
                            input = max + "','" + obj.dmcase_registration_id + "','" + DateTime.Now.ToShortDateString() + "','" + obj.user_id + "','" + DateTime.Now.ToShortDateString() + "','" + obj.record_status + "','" + obj.record_deleted + "','" + obj.case_action + "','" + obj.confiscationorderno + "','" + obj.confiscationorderdate + "','" + obj.remarks;
                        }

                        if (obj.case_action == "2")
                        {
                            columnNames = "dmcase_hearing_id, dmcase_registration_id, hearing_date,nexthearing_date, lastmodified_date, user_id, creation_date, record_status, record_deleted,case_action,remarks";
                            input = max + "','" + obj.dmcase_registration_id + "','" + obj.case_hearingdate + "','" + obj.case_hearingdate + "','" + DateTime.Now.ToShortDateString() + "','" + obj.user_id + "','" + DateTime.Now.ToShortDateString() + "','" + obj.record_status + "','" + obj.record_deleted + "','" + obj.case_action + "','" + obj.remarks;
                        }
                        

                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            //for (int i = 0; i < obj.docs.Count; i++)
                            //{
                            //    n = 0;
                            //    str = new StringBuilder();
                            //    str.Append("INSERT INTO exciseautomation.seizure_docs(seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date)");
                            //    str.Append("Values('" + max + "','" + max + "','" + obj.docs[i].doc_name + "', '" + obj.docs[i].description + "','" + obj.docs[i].doc_path + "','DMCR','" + obj.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "')");
                            //    NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            //    n = cmd3.ExecuteNonQuery();
                            //}
                            value = true;
                            trn.Commit();
                            //_log.Info("Sugarcanepurchase Insertion Success:" + scp.sugarcanepurchase_id + '-' + scp.party_code);
                            cn.Close();
                        }
                        else
                        {
                            trn.Rollback();
                            value = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        trn.Rollback();
                        Console.Write(ex);
                        value = false;
                    }
                }
            }
            return value;
        }
        public static bool InsertDmCaseEntry(cm_court obj)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    NpgsqlTransaction trn;
                    trn = cn.BeginTransaction();
                    try
                    {
                        int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.dmcase_registration", "dmcase_registration_id").ToString()) + 1;
                        StringBuilder str = new StringBuilder();
                        string tableName = "exciseautomation.dmcase_registration";
                        string columnNames = "dmcase_registration_id,seizure_fir_no, district_code, court_master_code,confiscation_code,raidby,thana_code , letterno,letterdate, case_type,caseno, casedate, date_of_hearing,  lastmodified_date, user_id, creation_date,record_status,record_deleted,remarks,hearing_status";
                        string input = max+ "','"+obj.seizure_fir_no + "','" + obj.district_code + "','" + obj.court_master_code + "','"+obj.confiscation_code+"','"+obj.raidby+"','"+obj.thana_code+"','" + obj.proposed_letterno + "','" + obj.proposed_letterdate + "','" + obj.case_type + "','" + obj.caseno + "','" + obj.case_registerdate + "','" + obj.case_hearingdate + "','" + DateTime.Now.ToShortDateString() + "','" + obj.user_id + "','" + DateTime.Now.ToShortDateString() + "','" + obj.record_status + "','" + obj.record_deleted + "','" + obj.remarks+"','"+obj.hearing_status;

                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            for (int i = 0; i < obj.docs.Count; i++)
                            {
                                n = 0;
                                str = new StringBuilder();
                                str.Append("INSERT INTO exciseautomation.seizure_docs(seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,doc_type)");
                                str.Append("Values('" + obj.seizureno + "','" + max + "','" + obj.docs[i].doc_name + "', '" + obj.docs[i].description + "','" + obj.docs[i].doc_path + "','DMCR','" + obj.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+obj.docs[i].document_type+"')");
                                NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                                n = cmd3.ExecuteNonQuery();
                            }
                         
                            trn.Commit();
                            //_log.Info("Sugarcanepurchase Insertion Success:" + scp.sugarcanepurchase_id + '-' + scp.party_code);
                            cn.Close();
                            value = true;
                        }
                        else
                        {
                            trn.Rollback();
                            value = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        trn.Rollback();
                        Console.Write(ex);
                        value = false;
                    }
                }
            }
            return value;
        }
        public static bool InsertCourt(cm_court court)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();

                    try
                    {
                        string tableName = "exciseautomation.court_master";
                        string _creation_date = DateTime.Now.ToString("dd/MM/yyyy");
                        //court_master_id, court_master_code, court_master_name, lastmodified_date, user_id, creation_date, record_status, record_deleted
                        string columnNames = "court_master_code, court_master_name, lastmodified_date, user_id, creation_date, record_status, record_deleted";
                        string input = court.court_master_code + "','" + court.court_master_name + "','" + court.lastmodified_date + "','" + court.user_id + "','" + _creation_date + "','" + court.record_status + "','" + court.record_deleted;
                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            value = true;
                        }
                        else
                            value = false;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        value = false;
                    }
                }
            }
            return value;
        }

        //GetDMCONDetails
        public static cm_court GetDMCONDetails(int dmcase_registration_id)
        {
            cm_court cmr = new cm_court();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT dcm.dmcase_registration_id, seizure_fir_id, dm.district_code,dm.district_court_master_id,dm.court_master_code,confiscation_code,raidby, letterno, letterdate, case_type, oldcaseno, newcaseno,casedate,date_of_hearing,dmh.confiscationorder,dmh.confiscationdate	FROM exciseautomation.dmcase_registration dcm inner join exciseautomation.district_court_master dm on dm.district_court_master_id=dcm.district_court_master_id inner join exciseautomation.dmcase_hearing dmh on dmh.dmcase_registration_id=dcm.dmcase_registration_id where dcm.dmcase_registration_id='"+dmcase_registration_id+ "' and dmh.case_action='1' ", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        cmr.dmcase_registration_id = Convert.ToInt32(dr["dmcase_registration_id"].ToString());
                        cmr.seizure_fir_no = Convert.ToInt32(dr["seizure_fir_id"].ToString());
                        cmr.district_code = dr["district_code"].ToString();
                        cmr.district_court_master_id = Convert.ToInt32(dr["district_court_master_id"].ToString());
                        cmr.court_master_code = dr["court_master_code"].ToString();
                        cmr.vp = dr["confiscation_code"].ToString();
                        cmr.raidby = dr["raidby"].ToString();
                        cmr.proposed_letterno = dr["letterno"].ToString();
                        cmr.proposed_letterdate = dr["letterdate"].ToString();
                        cmr.case_type = dr["case_type"].ToString();
                        cmr.caseno = dr["oldcaseno"].ToString();
                        //cmr.new_caseno = Convert.ToInt32(dr["newcaseno"].ToString());
                        cmr.case_registerdate = dr["casedate"].ToString();
                        cmr.case_hearingdate = dr["date_of_hearing"].ToString();
                        cmr.confiscationorderno= dr["confiscationorder"].ToString();
                        cmr.confiscationorderdate= dr["confiscationdate"].ToString();

                    }

                    try
                    {
                        using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.Seizure_Docs where  doc_type_code='DMCR' order by seizure_docs_id", cn))
                        {
                            cmd1.CommandType = System.Data.CommandType.Text;
                            //cmd.Parameters.AddWithValue("@UserID", userid);
                            NpgsqlDataReader dr2 = cmd1.ExecuteReader();

                            //if (dr2.HasRows)
                            //{
                            //    while (dr2.Read())
                            //    {
                            //        Seizure_Docs doc = new Seizure_Docs();
                            //        doc.seizure_docs_id = dr2["seizure_docs_id"].ToString();
                            //        doc.doc_id = dr2["doc_id"].ToString();
                            //        doc.doc_name = dr2["doc_Name"].ToString();
                            //        doc.description = dr2["doc_desc"].ToString();
                            //        doc.doc_path = dr2["doc_path"].ToString();
                            //        cmr.docs.Add(doc);

                            //    }
                            //}
                            dr2.Close();
                        }
                        //lstObj.Add(record);
                    }
                    catch (Exception ex)
                    {
                        //throw;
                    }
                    trn.Commit();
                    cn.Close();

                }
                catch (Exception ex)
                {
                    trn.Rollback();

                }
            }
            return cmr;
        }
        public static cm_court GetDMCRDetails(int dmcase_registration_id)
        {
            cm_court cmr = new cm_court();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT dmcase_registration_id, seizure_fir_id, dm.district_code, dm.district_court_master_id,dm.court_master_code,confiscation_code, raidby, letterno, letterdate, case_type, oldcaseno, newcaseno, casedate,date_of_hearing	FROM exciseautomation.dmcase_registration dcm inner join exciseautomation.district_court_master dm on dm.district_court_master_id=dcm.district_court_master_id where dmcase_registration_id=" + dmcase_registration_id, cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        cmr.dmcase_registration_id = Convert.ToInt32(dr["dmcase_registration_id"].ToString());
                        cmr.seizure_fir_no = Convert.ToInt32(dr["seizure_fir_id"].ToString());
                        cmr.district_code = dr["district_code"].ToString();
                        cmr.district_court_master_id = Convert.ToInt32(dr["district_court_master_id"].ToString());
                        cmr.court_master_code = dr["court_master_code"].ToString();
                        cmr.vp = dr["confiscation_code"].ToString();
                        cmr.raidby = dr["raidby"].ToString();
                        cmr.proposed_letterno = dr["letterno"].ToString();
                        if (dr["letterdate"].ToString().Length == 10)
                            cmr.proposed_letterdate = dr["letterdate"].ToString().Substring(0, 10).Replace("/", "-");
                        cmr.case_type = dr["case_type"].ToString();
                        cmr.caseno = dr["oldcaseno"].ToString();
                        //cmr.new_caseno = Convert.ToInt32(dr["newcaseno"].ToString());
                        if (dr["casedate"].ToString().Length == 10)
                            cmr.case_registerdate = dr["casedate"].ToString().Substring(0, 10).Replace("/", "-");
                        if (dr["date_of_hearing"].ToString().Length == 10)
                            cmr.case_hearingdate = dr["date_of_hearing"].ToString().Substring(0, 10).Replace("/", "-");
                        if (dr["next_hearing_date"].ToString().Length == 10)
                            cmr.next_hearingdate = dr["next_hearing_date"].ToString().Substring(0, 10).Replace("/", "-");
                        cmr.case_action = dr["case_action"].ToString();
                        if(dr["confiscationorderdate"].ToString().Length==10)
                        cmr.confiscationorderdate = dr["confiscationorderdate"].ToString().Substring(0,10).Replace("/","-");
                        cmr.confiscationorderno = dr["confiscationorderno"].ToString();
                        try
                        {
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.Seizure_Docs where  doc_type_code='DMCR' AND doc_id='"+cmr.dmcase_registration_id+"' order by seizure_docs_id", cn))
                            {
                                cmd1.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr2 = cmd1.ExecuteReader();

                                if (dr2.HasRows)
                                {
                                    while (dr2.Read())
                                    {
                                        Seizure_Docs doc = new Seizure_Docs();
                                        doc.seizure_docs_id = dr2["seizure_docs_id"].ToString();
                                        doc.doc_id = dr2["doc_id"].ToString();
                                        doc.doc_name = dr2["doc_Name"].ToString();
                                        doc.description = dr2["doc_desc"].ToString();
                                        doc.doc_path = dr2["doc_path"].ToString();
                                        cmr.docs.Add(doc);
                                    }
                                }
                                dr2.Close();
                            }
                            //lstObj.Add(record);
                        }
                        catch (Exception ex)
                        {
                            //throw;
                        }
                        try
                        {
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.Seizure_Docs where  doc_type_code='DMCH' AND doc_id='" + cmr.dmcase_registration_id + "' order by seizure_docs_id", cn))
                            {
                                cmd1.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr2 = cmd1.ExecuteReader();

                                if (dr2.HasRows)
                                {
                                    while (dr2.Read())
                                    {
                                        Seizure_Docs doc = new Seizure_Docs();
                                        doc.seizure_docs_id = dr2["seizure_docs_id"].ToString();
                                        doc.doc_id = dr2["doc_id"].ToString();
                                        doc.doc_name = dr2["doc_Name"].ToString();
                                        doc.description = dr2["doc_desc"].ToString();
                                        doc.doc_path = dr2["doc_path"].ToString();
                                        cmr.docs1.Add(doc);

                                    }
                                }
                                dr2.Close();
                            }
                            //lstObj.Add(record);
                        }
                        catch (Exception ex)
                        {
                            //throw;
                        }
                    }
                    cn.Close();

                }
                catch (Exception ex)
                {


                }
            }
            return cmr;
        }
        public static bool InsertDistrictCourt(cm_court court)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    try
                    {
                        string tableName = "exciseautomation.district_court_master";
                        string _creation_date = DateTime.Now.ToString("dd/MM/yyyy");
                        //court_master_id, court_master_code, court_master_name, lastmodified_date, user_id, creation_date, record_status, record_deleted
                        string columnNames = "court_master_code, district_code,role_name_code lastmodified_date, user_id, creation_date, record_status, record_deleted";
                        string input = court.court_master_code + "','" + court.district_code + "','" + court.role_level_code + "','" + court.lastmodified_date + "','" + court.user_id + "','" + _creation_date + "','" + court.record_status + "','" + court.record_deleted;
                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            value = true;
                        }
                        else
                            value = false;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        value = false;
                    }
                }
            }
            return value;
        }

        public static bool UpdateDMEntry(cm_court court)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd;
                    if (court.case_action==""|| court.case_action ==null)
                     cmd = new NpgsqlCommand("UPDATE exciseautomation.dmcase_registration SET  seizure_fir_no ='" + court.seizure_fir_no + "',hearing_status='" + court.hearing_status + "', court_master_code ='" + court.court_master_code + "',confiscation_code='"+court.confiscation_code+ "',raidby='"+court.raidby+ "',letterno='"+court.proposed_letterno.Trim()+ "',letterdate='"+court.proposed_letterdate+ "',case_type='"+court.case_type+ "',caseno='"+court.caseno+ "',casedate='"+court.case_registerdate+ "',date_of_hearing='"+court.case_hearingdate+ "',remarks='"+court.remarks.Trim()+ "',record_status='"+court.record_status+"', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "' WHERE dmcase_registration_id ='" + court.dmcase_registration_id + "' ", cn);
                    else if (court.case_action == "Case Dispose")
                        cmd = new NpgsqlCommand("UPDATE exciseautomation.dmcase_registration SET  hearing_remarks='" + court.hremarks + "',hearing_status='" + court.hearing_status + "',dmconfiscationorder='"+court.confiscationorderno+ "',dmconfiscationdate='"+court.confiscationorderdate+"',case_action='"+court.case_action+"', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "' WHERE dmcase_registration_id ='" + court.dmcase_registration_id + "' ", cn);
                    else
                        cmd = new NpgsqlCommand("UPDATE exciseautomation.dmcase_registration SET hearing_remarks='" + court.hremarks + "', hearing_status='" + court.hearing_status + "',next_hearing_date='" + court.next_hearingdate + "',case_action='" + court.case_action + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "',dmconfiscationorder=null,dmconfiscationdate=null WHERE dmcase_registration_id ='" + court.dmcase_registration_id + "' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        if (court.case_action == "" || court.case_action == null)
                        {
                            cmd = new NpgsqlCommand("Delete from exciseautomation.seizure_docs where seizureno='" + court.seizure_fir_no + "' and doc_type_code='DMCR' and doc_id='" + court.dmcase_registration_id + "'", cn);
                            int n1 = cmd.ExecuteNonQuery();
                            for (int i = 0; i < court.docs.Count; i++)
                            {
                                n = 0;
                                StringBuilder str = new StringBuilder();
                                str.Append("INSERT INTO exciseautomation.seizure_docs(seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,doc_type)");
                                str.Append("Values('" + court.seizure_fir_no + "','" + court.dmcase_registration_id + "','" + court.docs[i].doc_name + "', '" + court.docs[i].description + "','" + court.docs[i].doc_path + "','DMCR','" + court.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+court.docs[i].document_type+"')");
                                NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                                n = cmd3.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            cmd = new NpgsqlCommand("Delete from exciseautomation.seizure_docs where seizureno='" + court.seizure_fir_no + "' and doc_type_code='DMCH' and doc_id='" + court.dmcase_registration_id + "'", cn);
                            int n1 = cmd.ExecuteNonQuery();
                            for (int i = 0; i < court.docs.Count; i++)
                            {
                                n = 0;
                                StringBuilder str = new StringBuilder();
                                str.Append("INSERT INTO exciseautomation.seizure_docs(seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,doc_type)");
                                str.Append("Values('" + court.seizure_fir_no + "','" + court.dmcase_registration_id + "','" + court.docs[i].doc_name + "', '" + court.docs[i].description + "','" + court.docs[i].doc_path + "','DMCH','" + court.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+court.docs[i].document_type+"')");
                                NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                                n = cmd3.ExecuteNonQuery();
                            }
                        }
                        if(court.hearing_status!="Y" && court.case_action=="Case Dispose")
                        {
                            int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.excase_registration", "excase_registration_id").ToString()) + 1;
                            StringBuilder str = new StringBuilder();
                            string tableName = "exciseautomation.excase_registration";
                            string columnNames = "excase_registration_id,dmcase_registration_id,case_action,confiscationorder,confiscationdate,caseno,casedate,creation_date,user_id,record_status";
                            string input = max + "','" + court.dmcase_registration_id + "','" + court.case_action + "','" + court.confiscationorderno + "','" + court.confiscationorderdate + "','" + court.caseno + "','" + court.case_registerdate + "','" + DateTime.Now.ToShortDateString() + "','" + court.user_id + "','" + "N";

                            string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                            NpgsqlCommand cmd1 = new NpgsqlCommand(InsertQuery, cn);
                            int n11 = cmd1.ExecuteNonQuery();
                        }
                        if (court.hearing_status == "Next Hearing" && court.case_action == "Next Hearing")
                        {
                            int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.dmcase_hearing", "dmcase_hearing_id").ToString()) + 1;
                            StringBuilder str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.dmcase_hearing (dmcase_hearing_id,seizure_fir_no,dmcase_registration_id,date_of_hearing ,nexthearing_date,case_action,remarks,user_id ,lastmodified_date,creation_date , record_status)VALUES(");
                            str.Append("'"+max+"','"+court.seizure_fir_no+"','" + court.dmcase_registration_id + "','" + court.case_hearingdate + "','" + court.next_hearingdate + "','" + court.case_action + "','" + court.hremarks + "','" + court.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','N')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd3.ExecuteNonQuery();
                        }
                        trn.Commit();
                        cn.Close();
                        value = true;
                    }
                    else
                    {
                        trn.Rollback();
                        cn.Close();
                        value = false;

                    }

                }
                catch (Exception ex)
                {
                    trn.Rollback();
                    cn.Close();
                    value = false;
                   // throw (ex);

                }
            }
            return value;
        }

        public static bool UpdateCourt(cm_court court)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.court_master SET  court_master_code ='" + court.court_master_code + "', court_master_name ='" + court.court_master_name + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "', record_status ='" + court.record_status + "' WHERE court_master_id ='" + court.court_master_id + "' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;

                    }
                    else
                    {
                        value = false;

                    }

                }
                catch (Exception ex)
                {
                    value = false;
                    throw (ex);

                }
            }
            return value;
        }

        //GetRoleLevelList

        public static List<cm_court> GetRoleLevelList()
        {
            List<cm_court> lstObj = new List<cm_court>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.role_level_master ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_court>();
                            while (dr.Read())
                            {
                                cm_court record = new cm_court();
                                record.role_level_code = dr["role_level_code"].ToString();
                                record.role_level_name = dr["role_level_name"].ToString();
                                
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }

        public static List<cm_court> GetList()
        {
            List<cm_court> lstObj = new List<cm_court>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.court_master ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_court>();
                            while (dr.Read())
                            {
                                cm_court record = new cm_court();
                                record.court_master_id = Convert.ToInt32(dr["court_master_id"].ToString());
                                record.court_master_name = dr["court_master_name"].ToString();
                                record.court_master_code = dr["court_master_code"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }

        public static List<cm_court> GetDistrictCourtList()
        {
            List<cm_court> lstObj = new List<cm_court>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT district_court_master_id,"
                        + " dcm.court_master_code,cm.court_master_name, dcm.district_code,dm.district_name"
                        + " FROM exciseautomation.district_court_master dcm inner join exciseautomation.court_master cm"
                        + " on cm.court_master_code=dcm.court_master_code inner join exciseautomation.district_master dm on dm.district_code=dcm.district_code", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_court>();
                            while (dr.Read())
                            {
                                cm_court record = new cm_court();
                                record.court_master_id = Convert.ToInt32(dr["district_court_master_id"].ToString());
                                record.court_master_name = dr["court_master_name"].ToString();
                                record.district_code = dr["district_name"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }

        public static cm_court GetDMDetails(string id)
        {
            cm_court record = new cm_court();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.dmcase_registration where dmcase_registration_id='" + id + "' ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr1 = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr1);
                        dr1.Close();



                        //while (dr.Read())
                        foreach (DataRow dr in dt.Rows)
                        {

                            record.dmcase_registration_id = Convert.ToInt32(dr["dmcase_registration_id"].ToString());
                            //  record.court_master_name = dr["court_master_name"].ToString();
                            record.court_master_code = dr["court_master_code"].ToString();
                            record.district_code = dr["District_code"].ToString();
                            record.raidby = dr["raidby"].ToString();
                            record.thana_code = dr["thana_code"].ToString();
                            record.seizure_fir_no = Convert.ToInt32(dr["seizure_fir_no"].ToString());
                            record.proposed_letterno = dr["letterno"].ToString();
                            if (dr["letterdate"].ToString() != "")
                                record.proposed_letterdate = dr["letterdate"].ToString().Substring(0, 10).Replace("/", "-");
                            record.case_type = dr["case_type"].ToString();
                            record.caseno = dr["caseno"].ToString();
                            if (dr["casedate"].ToString() != "")
                                record.case_registerdate = dr["casedate"].ToString().Substring(0, 10).Replace("/", "-");
                            if (dr["date_of_hearing"].ToString() != "")
                                record.case_hearingdate = dr["date_of_hearing"].ToString().Substring(0, 10).Replace("/", "-");
                            record.record_status = dr["record_status"].ToString();
                            record.hearing_status = dr["hearing_status"].ToString();
                            record.confiscation_code = dr["confiscation_code"].ToString();
                            record.case_action = dr["case_action"].ToString();
                            if (dr["dmconfiscationdate"].ToString() != "")
                                record.confiscationorderdate = dr["dmconfiscationdate"].ToString().Substring(0, 10).Replace("/", "-");
                            record.confiscationorderno = dr["dmconfiscationorder"].ToString();
                            record.next_hearingdate = dr["next_hearing_date"].ToString();
                            record.user_id = dr["user_id"].ToString();
                            record.remarks = dr["remarks"].ToString();
                            record.hremarks = dr["hearing_remarks"].ToString();
                            record.docs = new List<Seizure_Docs>();
                            record.docs1 = new List<Seizure_Docs>();
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.Seizure_Docs where seizureno='" + record.seizure_fir_no + "'  and doc_type_code='DMCR' order by seizure_docs_id", cn))
                            {
                                cmd1.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr2 = cmd1.ExecuteReader();

                                if (dr2.HasRows)
                                {

                                    while (dr2.Read())
                                    {
                                        Seizure_Docs doc = new Seizure_Docs();
                                        doc.seizure_docs_id = dr2["seizure_docs_id"].ToString();
                                        doc.doc_id = dr2["doc_id"].ToString();
                                        doc.doc_name = dr2["doc_Name"].ToString();
                                        doc.description = dr2["doc_desc"].ToString();
                                        doc.doc_path = dr2["doc_path"].ToString();
                                        doc.document_type = dr2["doc_type"].ToString();
                                        record.docs1.Add(doc);
                                    }

                                }
                                dr2.Close();
                            }
                            using (NpgsqlCommand cmd11 = new NpgsqlCommand("select * from exciseautomation.Seizure_Docs where seizureno='" + record.seizure_fir_no + "'  and doc_type_code='DMCH' and doc_id='"+record.dmcase_registration_id+"' order by seizure_docs_id", cn))
                            {
                                cmd11.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr21 = cmd11.ExecuteReader();

                                if (dr21.HasRows)
                                {

                                    while (dr21.Read())
                                    {
                                        Seizure_Docs doc = new Seizure_Docs();
                                        doc.seizure_docs_id = dr21["seizure_docs_id"].ToString();
                                        doc.doc_id = dr21["doc_id"].ToString();
                                        doc.doc_name = dr21["doc_Name"].ToString();
                                        doc.description = dr21["doc_desc"].ToString();
                                        doc.doc_path = dr21["doc_path"].ToString();
                                        doc.document_type = dr21["doc_type"].ToString();
                                        record.docs.Add(doc);
                                    }

                                }
                                dr21.Close();
                            }
                            record.hearings = new List<cm_hearings>();
                            using (NpgsqlCommand cmd31 = new NpgsqlCommand("select * from exciseautomation.dmcase_hearing where dmcase_registration_id='" + record.dmcase_registration_id + "'   order by dmcase_hearing_id", cn))
                            {
                                cmd31.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr23 = cmd31.ExecuteReader();

                                if (dr23.HasRows)
                                {

                                    while (dr23.Read())
                                    {
                                        cm_hearings doc = new cm_hearings();
                                        doc.hearing_id = Convert.ToInt32(dr23["dmcase_hearing_id"].ToString());
                                      
                                        doc.hearing_date = dr23["date_of_hearing"].ToString().Substring(0, 10).Replace("/", "-"); ;
                                        doc.next_hearing_date = dr23["nexthearing_date"].ToString().Substring(0, 10).Replace("/", "-"); ;
                                        doc.hearing_remarks = dr23["remarks"].ToString();
                                        doc.creation_date = dr23["creation_date"].ToString().Substring(0, 10).Replace("/", "-");
                                        doc.case_action = dr23["case_action"].ToString() ;
                                       
                                        record.hearings.Add(doc);
                                    }

                                }
                                dr23.Close();
                            }

                        }
                        cn.Close();
                    }
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return record;
        }

        public static cm_court GetEXDetails(string id)
        {
            cm_court record = new cm_court();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT a1.*,b1.confiscation_code,b1.court_master_code,b1.District_code,b1.thana_code,b1.seizure_fir_no,b1.raidby,b1.dmconfiscationdate,b1.dmconfiscationorder FROM exciseautomation.excase_registration a1 inner join exciseautomation.dmcase_registration b1 on a1.dmcase_registration_id = b1.dmcase_registration_id inner join exciseautomation.court_master b on b.court_master_code = b1.court_master_code inner join exciseautomation.district_master d on d.district_code = b1.district_code inner join exciseautomation.seizure_fir e on e.seizureno = b1.seizure_fir_no and e.raidby = b1.raidby  where a1.excase_registration_id='" + id + "' ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr1 = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr1);
                        dr1.Close();



                        //while (dr.Read())
                        foreach (DataRow dr in dt.Rows)
                        {
                            record.excase_registration_id = Convert.ToInt32(dr["excase_registration_id"].ToString());
                            record.dmcase_registration_id = Convert.ToInt32(dr["dmcase_registration_id"].ToString());
                            //  record.court_master_name = dr["court_master_name"].ToString();
                            record.court_master_code = dr["court_master_code"].ToString();
                            record.district_code = dr["District_code"].ToString();
                            record.raidby = dr["raidby"].ToString();
                            record.thana_code = dr["thana_code"].ToString();
                            record.seizure_fir_no = Convert.ToInt32(dr["seizure_fir_no"].ToString());
                           // record.proposed_letterno = dr["letterno"].ToString();
                          //  if (dr["letterdate"].ToString() != "")
                           //     record.proposed_letterdate = dr["letterdate"].ToString().Substring(0, 10).Replace("/", "-");
                            record.case_type = dr["case_type"].ToString();
                            record.appealno = dr["appealno"].ToString();
                            if (dr["appealdate"].ToString() != "")
                                record.appealdate = dr["appealdate"].ToString().Substring(0, 10).Replace("/", "-");
                            if (dr["date_of_hearing"].ToString() != "")
                                record.case_hearingdate = dr["date_of_hearing"].ToString().Substring(0, 10).Replace("/", "-");
                            record.record_status = dr["record_status"].ToString();
                            record.hearing_status = dr["hearing_status"].ToString();
                            record.appellant_name = dr["appellant_name"].ToString();
                            record.appellant_contact = dr["appellant_contact"].ToString();
                            record.confiscation_code = dr["confiscation_code"].ToString();
                            record.case_action = dr["case_action"].ToString();
                            if (dr["dmconfiscationdate"].ToString() != "")
                                record.confiscationorderdate = dr["dmconfiscationdate"].ToString().Substring(0, 10).Replace("/", "-");
                            record.confiscationorderno = dr["dmconfiscationorder"].ToString();
                            if(dr["next_hearing_date"].ToString()!="")
                            record.next_hearingdate = dr["next_hearing_date"].ToString().Substring(0, 10).Replace("/", "-");
                            if (record.next_hearingdate == null)
                                record.next_hearingdate = "";
                            record.user_id = dr["user_id"].ToString();
                            record.remarks = dr["remarks"].ToString();
                            record.hremarks = dr["hearing_remarks"].ToString();
                            record.docs = new List<Seizure_Docs>();
                            record.docs1 = new List<Seizure_Docs>();
                            record.docs2 = new List<Seizure_Docs>();
                            record.docs3 = new List<Seizure_Docs>();
                            
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.Seizure_Docs where doc_id='" + record.excase_registration_id + "'  and doc_type_code='EXCR' order by seizure_docs_id", cn))
                            {
                                cmd1.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr2 = cmd1.ExecuteReader();

                                if (dr2.HasRows)
                                {

                                    while (dr2.Read())
                                    {
                                        Seizure_Docs doc = new Seizure_Docs();
                                        doc.seizure_docs_id = dr2["seizure_docs_id"].ToString();
                                        doc.doc_id = dr2["doc_id"].ToString();
                                        doc.doc_name = dr2["doc_Name"].ToString();
                                        doc.description = dr2["doc_desc"].ToString();
                                        doc.doc_path = dr2["doc_path"].ToString();
                                        doc.document_type = dr2["doc_type"].ToString();
                                        record.docs1.Add(doc);
                                    }

                                }
                                dr2.Close();
                            }
                            using (NpgsqlCommand cmd11 = new NpgsqlCommand("select * from exciseautomation.Seizure_Docs where doc_id='" + record.excase_registration_id + "'  and doc_type_code='EXCH' order by seizure_docs_id", cn))
                            {
                                cmd11.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr21 = cmd11.ExecuteReader();

                                if (dr21.HasRows)
                                {

                                    while (dr21.Read())
                                    {
                                        Seizure_Docs doc = new Seizure_Docs();
                                        doc.seizure_docs_id = dr21["seizure_docs_id"].ToString();
                                        doc.doc_id = dr21["doc_id"].ToString();
                                        doc.doc_name = dr21["doc_Name"].ToString();
                                        doc.description = dr21["doc_desc"].ToString();
                                        doc.doc_path = dr21["doc_path"].ToString();
                                        doc.document_type = dr21["doc_type"].ToString();
                                        record.docs.Add(doc);
                                    }

                                }
                                dr21.Close();
                            }
                            using (NpgsqlCommand cmd12 = new NpgsqlCommand("select * from exciseautomation.Seizure_Docs where doc_id='" + record.dmcase_registration_id + "'  and doc_type_code='DMCR' order by seizure_docs_id", cn))
                            {
                                cmd12.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr22 = cmd12.ExecuteReader();

                                if (dr22.HasRows)
                                {

                                    while (dr22.Read())
                                    {
                                        Seizure_Docs doc = new Seizure_Docs();
                                        doc.seizure_docs_id = dr22["seizure_docs_id"].ToString();
                                        doc.doc_id = dr22["doc_id"].ToString();
                                        doc.doc_name = dr22["doc_Name"].ToString();
                                        doc.description = dr22["doc_desc"].ToString();
                                        doc.doc_path = dr22["doc_path"].ToString();
                                        doc.document_type = dr22["doc_type"].ToString();
                                        record.docs2.Add(doc);
                                    }

                                }
                                dr22.Close();
                            }
                            using (NpgsqlCommand cmd13 = new NpgsqlCommand("select * from exciseautomation.Seizure_Docs where doc_id='" + record.dmcase_registration_id + "'  and doc_type_code='DMCH' order by seizure_docs_id", cn))
                            {
                                cmd13.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr23 = cmd13.ExecuteReader();

                                if (dr23.HasRows)
                                {

                                    while (dr23.Read())
                                    {
                                        Seizure_Docs doc = new Seizure_Docs();
                                        doc.seizure_docs_id = dr23["seizure_docs_id"].ToString();
                                        doc.doc_id = dr23["doc_id"].ToString();
                                        doc.doc_name = dr23["doc_Name"].ToString();
                                        doc.description = dr23["doc_desc"].ToString();
                                        doc.doc_path = dr23["doc_path"].ToString();
                                        doc.document_type = dr23["doc_type"].ToString();
                                        record.docs3.Add(doc);
                                    }

                                }
                                dr23.Close();
                            }
                            record.hearings = new List<cm_hearings>();
                            using (NpgsqlCommand cmd33 = new NpgsqlCommand("select * from exciseautomation.excase_hearing where excase_registration_id='" + record.excase_registration_id + "'   order by excase_hearing_id", cn))
                            {
                                cmd33.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr33 = cmd33.ExecuteReader();

                                if (dr33.HasRows)
                                {

                                    while (dr33.Read())
                                    {
                                        cm_hearings doc = new cm_hearings();
                                        doc.hearing_id = Convert.ToInt32(dr33["excase_hearing_id"].ToString());
                                        doc.hearing_date = dr33["date_of_hearing"].ToString();
                                        doc.next_hearing_date = dr33["nexthearing_date"].ToString();
                                        doc.hearing_remarks = dr33["remarks"].ToString();
                                        doc.creation_date = dr33["creation_date"].ToString();
                                        doc.case_action = dr33["case_action"].ToString();
                                        record.hearings.Add(doc);
                                    }

                                }
                                dr33.Close();
                            }
                        }
                    }

                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return record;
        }

        public static bool UpdateEXEntry(cm_court court)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd;
                    if (court.case_action == "" || court.case_action == null)
                   
                        cmd = new NpgsqlCommand("UPDATE exciseautomation.excase_registration SET  case_type='" + court.case_type + "',date_of_hearing='" + court.case_hearingdate + "',remarks='" + court.remarks.Trim() + "',record_status='" + court.record_status + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "',appealno='" + court.appealno + "',appealdate='" + court.appealdate + "',appellant_name='" + court.appellant_name + "' ,appellant_contact='" + court.appellant_contact + "',hearing_status='"+court.hearing_status+"' WHERE excase_registration_id ='" + court.excase_registration_id + "' ", cn);
                   
                    else if (court.case_action == "Case Dispose")
                        cmd = new NpgsqlCommand("UPDATE exciseautomation.excase_registration SET  hearing_remarks='" + court.hremarks + "',hearing_status='" + court.hearing_status + "',case_action='" + court.case_action + "',lastmodified_date ='" + DateTime.Now.ToShortDateString() + "' WHERE excase_registration_id ='" + court.excase_registration_id + "' ", cn);
                    else
                        cmd = new NpgsqlCommand("UPDATE exciseautomation.excase_registration SET hearing_remarks='" + court.hremarks + "', hearing_status='" + court.hearing_status + "',next_hearing_date='" + court.next_hearingdate + "',case_action='" + court.case_action + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "' WHERE excase_registration_id ='" + court.excase_registration_id + "' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        if ( court.case_action == "" || court.case_action == null)
                        {
                           
                            cmd = new NpgsqlCommand("Delete from exciseautomation.seizure_docs where seizureno='" + court.seizure_fir_no + "' and doc_type_code='EXCR' and doc_id='" + court.excase_registration_id + "'", cn);
                            int n1 = cmd.ExecuteNonQuery();
                            for (int i = 0; i < court.docs.Count; i++)
                            {
                                n = 0;
                                StringBuilder str = new StringBuilder();
                                str.Append("INSERT INTO exciseautomation.seizure_docs(seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,doc_type)");
                                str.Append("Values('" + court.seizure_fir_no + "','" + court.excase_registration_id + "','" + court.docs[i].doc_name + "', '" + court.docs[i].description + "','" + court.docs[i].doc_path + "','EXCR','" + court.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+court.docs[i].document_type+"')");
                                NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                                n = cmd3.ExecuteNonQuery();
                            }
                        }
                       else
                        {
                            cmd = new NpgsqlCommand("Delete from exciseautomation.seizure_docs where seizureno='" + court.seizure_fir_no + "' and doc_type_code='EXCH' and doc_id='" + court.excase_registration_id + "'", cn);
                            int n1 = cmd.ExecuteNonQuery();
                            for (int i = 0; i < court.docs.Count; i++)
                            {
                                n = 0;
                                StringBuilder str = new StringBuilder();
                                str.Append("INSERT INTO exciseautomation.seizure_docs(seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,doc_type)");
                                str.Append("Values('" + court.seizure_fir_no + "','" + court.excase_registration_id + "','" + court.docs[i].doc_name + "', '" + court.docs[i].description + "','" + court.docs[i].doc_path + "','EXCH','" + court.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+court.docs[i].document_type+"')");
                                NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                                n = cmd3.ExecuteNonQuery();
                            }
                        }
                        if(court.hearing_status== "Next Hearing")
                        {
                            int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.excase_hearing", "excase_hearing_id").ToString()) + 1;
                            StringBuilder str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.excase_hearing (excase_hearing_id,excase_registration_id,date_of_hearing ,nexthearing_date,case_action,remarks,user_id ,lastmodified_date,creation_date , record_status)VALUES(");
                            str.Append("'" + max + "','" + court.excase_registration_id + "','" + court.case_hearingdate + "','" + court.next_hearingdate + "','" + court.case_action + "','" + court.hremarks + "','" + court.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','N')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd3.ExecuteNonQuery();
                        }
                        if(court.record_status == "Y" )
                        {
                            int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.excase_registration where excase_registration_id='" + court.excase_registration_id+"'", "dmcase_registration_id").ToString());
                            cmd = new NpgsqlCommand("UPDATE exciseautomation.dmcase_registration SET  hearing_status='Appealed in Commissioner Court' WHERE dmcase_registration_id ='" + max + "' ", cn);
                            cmd.ExecuteNonQuery();
                        }
                        if (court.hearing_status == "Disposed at Commissioner Court")
                        {
                            int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.excase_registration where excase_registration_id='" + court.excase_registration_id + "'", "dmcase_registration_id").ToString());
                            cmd = new NpgsqlCommand("UPDATE exciseautomation.dmcase_registration SET  hearing_status='Disposed at Commissioner Court' WHERE dmcase_registration_id ='" + max + "' ", cn);
                            cmd.ExecuteNonQuery();
                            int max1 = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.seccase_registration", "seccase_registration_id").ToString()) + 1;
                            StringBuilder str = new StringBuilder();
                            string tableName = "exciseautomation.seccase_registration";
                            string columnNames = "seccase_registration_id,excase_registration_id,case_action,confiscationorder,confiscationdate,creation_date,user_id,record_status";
                            string input = max1 + "','" + court.excase_registration_id + "','" + court.case_action + "','" + court.confiscationorderno + "','" + court.confiscationorderdate + "','" + DateTime.Now.ToShortDateString() + "','" + court.user_id + "','" + "N";
                            string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";
                            NpgsqlCommand cmd1 = new NpgsqlCommand(InsertQuery, cn);
                            int n11 = cmd1.ExecuteNonQuery();
                        }
                       
                        trn.Commit();
                        cn.Close();
                        value = true;
                    }
                    else
                    {
                        trn.Rollback();
                        cn.Close();
                        value = false;

                    }

                }
                catch (Exception ex)
                {
                    trn.Rollback();
                    cn.Close();
                    value = false;
                    // throw (ex);

                }
            }
            return value;
        }

        public static cm_court GetSECDetails(string id)
        {
            cm_court record = new cm_court();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT s1.*,a1.excase_registration_id,b1.dmcase_registration_id,b1.confiscation_code,b1.court_master_code,b1.District_code,b1.thana_code,b1.seizure_fir_no,b1.raidby,b1.dmconfiscationdate,b1.dmconfiscationorder FROM exciseautomation.seccase_registration s1 inner join exciseautomation.excase_registration a1 on a1.excase_registration_id=s1.seccase_registration_id  inner join exciseautomation.dmcase_registration b1 on a1.dmcase_registration_id = b1.dmcase_registration_id inner join exciseautomation.court_master b on b.court_master_code = b1.court_master_code inner join exciseautomation.district_master d on d.district_code = b1.district_code inner join exciseautomation.seizure_fir e on e.seizureno = b1.seizure_fir_no and e.raidby = b1.raidby  where s1.seccase_registration_id='" + id + "' ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr1 = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr1);
                        dr1.Close();
                        //while (dr.Read())
                        foreach (DataRow dr in dt.Rows)
                        {
                            record.seccase_registration_id = Convert.ToInt32(dr["seccase_registration_id"].ToString());
                            record.excase_registration_id = Convert.ToInt32(dr["excase_registration_id"].ToString());
                           record.dmcase_registration_id = Convert.ToInt32(dr["dmcase_registration_id"].ToString());
                            record.court_master_code = dr["court_master_code"].ToString();
                            record.district_code = dr["District_code"].ToString();
                            record.raidby = dr["raidby"].ToString();
                            record.thana_code = dr["thana_code"].ToString();
                            record.seizure_fir_no = Convert.ToInt32(dr["seizure_fir_no"].ToString());
                            record.case_type = dr["case_type"].ToString();
                            record.appealno = dr["appealno"].ToString();
                            if (dr["appealdate"].ToString() != "")
                                record.appealdate = dr["appealdate"].ToString().Substring(0, 10).Replace("/", "-");
                            if (dr["date_of_hearing"].ToString() != "")
                                record.case_hearingdate = dr["date_of_hearing"].ToString().Substring(0, 10).Replace("/", "-");
                            record.record_status = dr["record_status"].ToString();
                            record.hearing_status = dr["hearing_status"].ToString();
                            record.appellant_name = dr["appellant_name"].ToString();
                            record.appellant_contact = dr["appellant_contact"].ToString();
                            record.confiscation_code = dr["confiscation_code"].ToString();
                            record.case_action = dr["case_action"].ToString();
                            if (dr["dmconfiscationdate"].ToString() != "")
                                record.confiscationorderdate = dr["dmconfiscationdate"].ToString().Substring(0, 10).Replace("/", "-");
                            record.confiscationorderno = dr["dmconfiscationorder"].ToString();
                            if (dr["next_hearing_date"].ToString() != "")
                                record.next_hearingdate = dr["next_hearing_date"].ToString().Substring(0, 10).Replace("/", "-");
                            if (record.next_hearingdate == null)
                                record.next_hearingdate = "";
                            record.user_id = dr["user_id"].ToString();
                            record.remarks = dr["remarks"].ToString();
                            record.hremarks = dr["hearing_remarks"].ToString();
                            record.docs = new List<Seizure_Docs>();
                            record.docs1 = new List<Seizure_Docs>();
                            record.docs2 = new List<Seizure_Docs>();
                            record.docs3 = new List<Seizure_Docs>();
                            record.docs4 = new List<Seizure_Docs>();
                            record.docs5 = new List<Seizure_Docs>();

                            using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.Seizure_Docs where doc_id='" + record.dmcase_registration_id + "'  and doc_type_code='DMCR' order by seizure_docs_id", cn))
                            {
                                cmd1.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr2 = cmd1.ExecuteReader();

                                if (dr2.HasRows)
                                {

                                    while (dr2.Read())
                                    {
                                        Seizure_Docs doc = new Seizure_Docs();
                                        doc.seizure_docs_id = dr2["seizure_docs_id"].ToString();
                                        doc.doc_id = dr2["doc_id"].ToString();
                                        doc.doc_name = dr2["doc_Name"].ToString();
                                        doc.description = dr2["doc_desc"].ToString();
                                        doc.doc_path = dr2["doc_path"].ToString();
                                        doc.document_type = dr2["doc_type"].ToString();
                                        record.docs1.Add(doc);
                                    }

                                }
                                dr2.Close();
                            }
                            using (NpgsqlCommand cmd11 = new NpgsqlCommand("select * from exciseautomation.Seizure_Docs where doc_id='" + record.dmcase_registration_id + "'  and doc_type_code='DMCH' order by seizure_docs_id", cn))
                            {
                                cmd11.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr21 = cmd11.ExecuteReader();

                                if (dr21.HasRows)
                                {

                                    while (dr21.Read())
                                    {
                                        Seizure_Docs doc = new Seizure_Docs();
                                        doc.seizure_docs_id = dr21["seizure_docs_id"].ToString();
                                        doc.doc_id = dr21["doc_id"].ToString();
                                        doc.doc_name = dr21["doc_Name"].ToString();
                                        doc.description = dr21["doc_desc"].ToString();
                                        doc.doc_path = dr21["doc_path"].ToString();
                                        doc.document_type = dr21["doc_type"].ToString();
                                        record.docs2.Add(doc);
                                    }

                                }
                                dr21.Close();
                            }
                            using (NpgsqlCommand cmd12 = new NpgsqlCommand("select * from exciseautomation.Seizure_Docs where doc_id='" + record.excase_registration_id + "'  and doc_type_code='EXCR' order by seizure_docs_id", cn))
                            {
                                cmd12.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr22 = cmd12.ExecuteReader();

                                if (dr22.HasRows)
                                {

                                    while (dr22.Read())
                                    {
                                        Seizure_Docs doc = new Seizure_Docs();
                                        doc.seizure_docs_id = dr22["seizure_docs_id"].ToString();
                                        doc.doc_id = dr22["doc_id"].ToString();
                                        doc.doc_name = dr22["doc_Name"].ToString();
                                        doc.description = dr22["doc_desc"].ToString();
                                        doc.doc_path = dr22["doc_path"].ToString();
                                        doc.document_type = dr22["doc_type"].ToString();
                                        record.docs3.Add(doc);
                                    }

                                }
                                dr22.Close();
                            }
                            using (NpgsqlCommand cmd13 = new NpgsqlCommand("select * from exciseautomation.Seizure_Docs where doc_id='" + record.excase_registration_id + "'  and doc_type_code='EXCH' order by seizure_docs_id", cn))
                            {
                                cmd13.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr23 = cmd13.ExecuteReader();

                                if (dr23.HasRows)
                                {

                                    while (dr23.Read())
                                    {
                                        Seizure_Docs doc = new Seizure_Docs();
                                        doc.seizure_docs_id = dr23["seizure_docs_id"].ToString();
                                        doc.doc_id = dr23["doc_id"].ToString();
                                        doc.doc_name = dr23["doc_Name"].ToString();
                                        doc.description = dr23["doc_desc"].ToString();
                                        doc.doc_path = dr23["doc_path"].ToString();
                                        doc.document_type = dr23["doc_type"].ToString();
                                        record.docs4.Add(doc);
                                    }

                                }
                                dr23.Close();
                            }
                            using (NpgsqlCommand cmd13 = new NpgsqlCommand("select * from exciseautomation.Seizure_Docs where doc_id='" + record.seccase_registration_id + "'  and doc_type_code='SECH' order by seizure_docs_id", cn))
                            {
                                cmd13.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr23 = cmd13.ExecuteReader();

                                if (dr23.HasRows)
                                {

                                    while (dr23.Read())
                                    {
                                        Seizure_Docs doc = new Seizure_Docs();
                                        doc.seizure_docs_id = dr23["seizure_docs_id"].ToString();
                                        doc.doc_id = dr23["doc_id"].ToString();
                                        doc.doc_name = dr23["doc_Name"].ToString();
                                        doc.description = dr23["doc_desc"].ToString();
                                        doc.doc_path = dr23["doc_path"].ToString();
                                        doc.document_type = dr23["doc_type"].ToString();
                                        record.docs.Add(doc);
                                    }

                                }
                                dr23.Close();
                            }
                            using (NpgsqlCommand cmd13 = new NpgsqlCommand("select * from exciseautomation.Seizure_Docs where doc_id='" + record.seccase_registration_id + "'  and doc_type_code='SECR' order by seizure_docs_id", cn))
                            {
                                cmd13.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr23 = cmd13.ExecuteReader();

                                if (dr23.HasRows)
                                {

                                    while (dr23.Read())
                                    {
                                        Seizure_Docs doc = new Seizure_Docs();
                                        doc.seizure_docs_id = dr23["seizure_docs_id"].ToString();
                                        doc.doc_id = dr23["doc_id"].ToString();
                                        doc.doc_name = dr23["doc_Name"].ToString();
                                        doc.description = dr23["doc_desc"].ToString();
                                        doc.doc_path = dr23["doc_path"].ToString();
                                        doc.document_type = dr23["doc_type"].ToString();
                                        record.docs5.Add(doc);
                                    }

                                }
                                dr23.Close();
                            }
                            record.hearings = new List<cm_hearings>();
                            using (NpgsqlCommand cmd33 = new NpgsqlCommand("select * from exciseautomation.seccase_hearing where seccase_registration_id='" + record.seccase_registration_id + "'   order by seccase_hearing_id", cn))
                            {
                                cmd33.CommandType = System.Data.CommandType.Text;
                                //cmd.Parameters.AddWithValue("@UserID", userid);
                                NpgsqlDataReader dr33 = cmd33.ExecuteReader();

                                if (dr33.HasRows)
                                {

                                    while (dr33.Read())
                                    {
                                        cm_hearings doc = new cm_hearings();
                                        doc.hearing_id = Convert.ToInt32(dr33["seccase_hearing_id"].ToString());
                                        doc.hearing_date = dr33["date_of_hearing"].ToString();
                                        doc.next_hearing_date = dr33["nexthearing_date"].ToString();
                                        doc.hearing_remarks = dr33["remarks"].ToString();
                                        doc.creation_date = dr33["creation_date"].ToString();
                                        doc.case_action = dr33["case_action"].ToString();
                                        record.hearings.Add(doc);
                                    }

                                }
                                dr33.Close();
                            }
                        }
                    }

                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return record;
        }

        public static bool UpdateSECEntry(cm_court court)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    NpgsqlCommand cmd;
                    if (court.case_action == "" || court.case_action == null)

                        cmd = new NpgsqlCommand("UPDATE exciseautomation.seccase_registration SET  case_type='" + court.case_type + "',date_of_hearing='" + court.case_hearingdate + "',remarks='" + court.remarks.Trim() + "',record_status='" + court.record_status + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "',appealno='" + court.appealno + "',appealdate='" + court.appealdate + "',appellant_name='" + court.appellant_name + "' ,appellant_contact='" + court.appellant_contact + "',hearing_status='" + court.hearing_status + "' WHERE seccase_registration_id ='" + court.seccase_registration_id + "' ", cn);

                    else if (court.case_action == "Case Dispose")
                        cmd = new NpgsqlCommand("UPDATE exciseautomation.seccase_registration SET  hearing_remarks='" + court.hremarks + "',hearing_status='" + court.hearing_status + "',case_action='" + court.case_action + "',lastmodified_date ='" + DateTime.Now.ToShortDateString() + "' WHERE seccase_registration_id ='" + court.seccase_registration_id + "' ", cn);
                    else
                        cmd = new NpgsqlCommand("UPDATE exciseautomation.seccase_registration SET hearing_remarks='" + court.hremarks + "', hearing_status='" + court.hearing_status + "',next_hearing_date='" + court.next_hearingdate + "',case_action='" + court.case_action + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "' WHERE seccase_registration_id ='" + court.seccase_registration_id + "' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        if (court.case_action == "" || court.case_action == null)
                        {

                            cmd = new NpgsqlCommand("Delete from exciseautomation.seizure_docs where seizureno='" + court.seizure_fir_no + "' and doc_type_code='SECR' and doc_id='" + court.seccase_registration_id + "'", cn);
                            int n1 = cmd.ExecuteNonQuery();
                            for (int i = 0; i < court.docs.Count; i++)
                            {
                                n = 0;
                                StringBuilder str = new StringBuilder();
                                str.Append("INSERT INTO exciseautomation.seizure_docs(seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,doc_type)");
                                str.Append("Values('" + court.seizure_fir_no + "','" + court.seccase_registration_id + "','" + court.docs[i].doc_name + "', '" + court.docs[i].description + "','" + court.docs[i].doc_path + "','SECR','" + court.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+court.docs[i].document_type+"')");
                                NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                                n = cmd3.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            cmd = new NpgsqlCommand("Delete from exciseautomation.seizure_docs where seizureno='" + court.seizure_fir_no + "' and doc_type_code='SECH' and doc_id='" + court.seccase_registration_id + "'", cn);
                            int n1 = cmd.ExecuteNonQuery();
                            for (int i = 0; i < court.docs.Count; i++)
                            {
                                n = 0;
                                StringBuilder str = new StringBuilder();
                                str.Append("INSERT INTO exciseautomation.seizure_docs(seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,doc_type)");
                                str.Append("Values('" + court.seizure_fir_no + "','" + court.seccase_registration_id + "','" + court.docs[i].doc_name + "', '" + court.docs[i].description + "','" + court.docs[i].doc_path + "','SECH','" + court.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+court.docs[i].document_type+"')");
                                NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                                n = cmd3.ExecuteNonQuery();
                            }
                        }
                        if (court.hearing_status == "Next Hearing")
                        {
                            int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.seccase_hearing", "seccase_hearing_id").ToString()) + 1;
                            StringBuilder str = new StringBuilder();
                            str.Append("INSERT INTO exciseautomation.seccase_hearing (seccase_hearing_id,seccase_registration_id,date_of_hearing ,nexthearing_date,case_action,remarks,user_id ,lastmodified_date,creation_date , record_status)VALUES(");
                            str.Append("'" + max + "','" + court.seccase_registration_id + "','" + court.case_hearingdate + "','" + court.next_hearingdate + "','" + court.case_action + "','" + court.hremarks + "','" + court.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','N')");
                            NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                            n = cmd3.ExecuteNonQuery();
                        }
                        if (court.record_status == "Y")
                        {
                            court.excase_registration_id = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.seccase_registration where seccase_registration_id='" + court.seccase_registration_id + "'", "excase_registration_id").ToString());
                            int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.excase_registration where excase_registration_id='" + court.excase_registration_id + "'", "dmcase_registration_id").ToString());
                            cmd = new NpgsqlCommand("UPDATE exciseautomation.dmcase_registration SET  hearing_status='Appealed in Secretary Court' WHERE dmcase_registration_id ='" + max + "' ", cn);
                            cmd.ExecuteNonQuery();
                          
                            cmd = new NpgsqlCommand("UPDATE exciseautomation.excase_registration SET  hearing_status='Appealed in Secretary Court' WHERE excase_registration_id ='" + max + "' ", cn);
                            cmd.ExecuteNonQuery();
                        }
                        if (court.hearing_status == "Disposed at Secretary Court")
                        {
                            court.excase_registration_id = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.seccase_registration where seccase_registration_id='" + court.seccase_registration_id + "'", "excase_registration_id").ToString());
                            int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.excase_registration where excase_registration_id='" + court.excase_registration_id + "'", "dmcase_registration_id").ToString());
                            cmd = new NpgsqlCommand("UPDATE exciseautomation.dmcase_registration SET  hearing_status='Disposed at Secretary Court' WHERE dmcase_registration_id ='" + max + "' ", cn);
                            cmd.ExecuteNonQuery();
                            
                            cmd = new NpgsqlCommand("UPDATE exciseautomation.excase_registration SET  hearing_status='Disposed at Secretary Court' WHERE excase_registration_id ='" + court.excase_registration_id + "' ", cn);
                            cmd.ExecuteNonQuery();
                           
                        }

                        trn.Commit();
                        cn.Close();
                        value = true;
                    }
                    else
                    {
                        trn.Rollback();
                        cn.Close();
                        value = false;

                    }

                }
                catch (Exception ex)
                {
                    trn.Rollback();
                    cn.Close();
                    value = false;
                    // throw (ex);

                }
            }
            return value;
        }
    }
    #endregion DL_cm_court

    #region DL_cm_designation
    public class DL_cm_designation
    {
        public static bool InsertDesignation(cm_designation designation)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    try
                    {
                        string tableName = "exciseautomation.designation_master";
                        string _creation_date = DateTime.Now.ToString("dd/MM/yyyy");
                        //designation_master_id, designation_code, designation_type_code, designation_name, lastmodified_date, user_id, creation_date, record_status, record_deleted
                        string columnNames = "designation_code, designation_type_code, designation_name, lastmodified_date, user_id, creation_date, record_status, record_deleted";
                        string input = designation.designation_code + "','" + designation.designation_type_code + "','" + designation.designation_name + "','" + designation.lastmodified_date + "','" + designation.user_id + "','" + _creation_date + "','" + designation.record_status + "','" + designation.record_deleted;
                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            value = true;
                        }
                        else
                            value = false;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        value = false;
                    }
                }
            }
            return value;
        }
        public static List<cm_designation> GetListILike(string texts, string colname)
        {
            List<cm_designation> lstObj = new List<cm_designation>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    if (colname == "designation_type_name")
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.designation_type_name from exciseautomation.designation_master a inner join exciseautomation.designation_type_master b on a.designation_type_code=b.designation_type_code where b." + colname + " Ilike '%" + texts + "%' order by b." + colname + "", cn))

                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                lstObj = new List<cm_designation>();
                                while (dr.Read())
                                {
                                    cm_designation record = new cm_designation();
                                    record.designation_master_id = Convert.ToInt32(dr["designation_master_id"].ToString());
                                    record.designation_type_name = dr["designation_type_name"].ToString();
                                    record.designation_name = dr["designation_name"].ToString();
                                    record.designation_code = dr["designation_code"].ToString();
                                    record.designation_type_code = dr["designation_type_code"].ToString();
                                    lstObj.Add(record);
                                }
                            }
                        }
                    }

                    else
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.designation_type_name from exciseautomation.designation_master a inner join exciseautomation.designation_type_master b on a.designation_type_code=b.designation_type_code where a." + colname + " Ilike '%" + texts + "%' order by a." + colname + "", cn))

                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                lstObj = new List<cm_designation>();
                                while (dr.Read())
                                {
                                    cm_designation record = new cm_designation();
                                    record.designation_master_id = Convert.ToInt32(dr["designation_master_id"].ToString());
                                    record.designation_type_name = dr["designation_type_name"].ToString();
                                    record.designation_name = dr["designation_name"].ToString();
                                    record.designation_code = dr["designation_code"].ToString();
                                    record.designation_type_code = dr["designation_type_code"].ToString();
                                    lstObj.Add(record);
                                }
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
        public static bool UpdateDesignation(cm_designation designation)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.designation_master SET  designation_code ='" + designation.designation_code + "', designation_type_code ='" + designation.designation_type_code + "', designation_name ='" + designation.designation_name + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "', record_status ='" + designation.record_status + "' WHERE designation_master_id ='" + designation.designation_master_id + "' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;

                    }
                    else
                    {
                        value = false;

                    }

                }
                catch (Exception ex)
                {
                    value = false;
                    throw (ex);

                }
            }
            return value;
        }

        public static List<cm_designation> GetList()
        {
            List<cm_designation> lstObj = new List<cm_designation>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.designation_type_name from exciseautomation.designation_master a inner join exciseautomation.designation_type_master b on a.designation_type_code=b.designation_type_code order by a.designation_name", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_designation>();
                            while (dr.Read())
                            {
                                cm_designation record = new cm_designation();
                                record.designation_master_id = Convert.ToInt32(dr["designation_master_id"].ToString());
                                record.designation_type_name = dr["designation_type_name"].ToString();
                                record.designation_name = dr["designation_name"].ToString();
                                record.designation_code = dr["designation_code"].ToString();
                                record.designation_type_code = dr["designation_type_code"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
    }
    #endregion DL_cm_designation

    #region DL_cm_designation_type
    public class DL_cm_designation_type
    {
        public static bool InsertDesignationType(cm_designation_type designation_type)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    try
                    {
                        string tableName = "exciseautomation.designation_type_master";
                        string _creation_date = DateTime.Now.ToString("dd/MM/yyyy");
                        //designation_type_master_id, designation_type_code, designation_type_name, lastmodified_date, user_id, creation_date, record_status, record_deleted
                        string columnNames = "designation_type_code, designation_type_name, lastmodified_date, user_id, creation_date, record_status, record_deleted";
                        string input = designation_type.designation_type_code + "','" + designation_type.designation_type_name + "','" + designation_type.lastmodified_date + "','" + designation_type.user_id + "','" + _creation_date + "','" + designation_type.record_status + "','" + designation_type.record_deleted;
                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            value = true;
                        }
                        else
                            value = false;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        value = false;
                    }
                }
            }
            return value;
        }
        public static List<cm_designation_type> GetListILike(string texts, string colname)
        {
            List<cm_designation_type> lstObj = new List<cm_designation_type>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.designation_type_master where " + colname + " Ilike '%" + texts + "%' order by " + colname + "", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_designation_type>();
                            while (dr.Read())
                            {
                                cm_designation_type record = new cm_designation_type();
                                record.designation_type_master_id = Convert.ToInt32(dr["designation_type_master_id"].ToString());
                                record.designation_type_name = dr["designation_type_name"].ToString();
                                record.designation_type_code = dr["designation_type_code"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
        public static bool UpdateDesignation(cm_designation_type designation_type)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.designation_type_master SET  designation_type_code ='" + designation_type.designation_type_code + "', designation_type_name ='" + designation_type.designation_type_name + "', lastmodified_date ='" + DateTime.Now.ToString() + "', record_status ='" + designation_type.record_status + "' WHERE designation_type_master_id ='" + designation_type.designation_type_master_id + "' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;

                    }
                    else
                    {
                        value = false;

                    }

                }
                catch (Exception ex)
                {
                    value = false;
                    throw (ex);

                }
            }
            return value;
        }

        public static List<cm_designation_type> GetList()
        {
            List<cm_designation_type> lstObj = new List<cm_designation_type>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.designation_type_master ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_designation_type>();
                            while (dr.Read())
                            {
                                cm_designation_type record = new cm_designation_type();
                                record.designation_type_master_id = Convert.ToInt32(dr["designation_type_master_id"].ToString());
                                record.designation_type_name = dr["designation_type_name"].ToString();
                                record.designation_type_code = dr["designation_type_code"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
    }
    #endregion DL_cm_designation_type

    #region DL_cm_disposal_of_property
    public class DL_cm_disposal_of_property
    {
        public static bool InsertDisposalOfProperty(cm_disposal_of_property disposal_of_property)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    try
                    {
                        string tableName = "exciseautomation.disposal_of_property_master";
                        string _creation_date = DateTime.Now.ToString("dd/MM/yyyy");
                        //disposal_of_property_id, disposal_of_property_code, disposal_of_property_name, lastmodified_date, user_id, creation_date, record_status, record_deleted
                        string columnNames = "disposal_of_property_code, disposal_of_property_name, lastmodified_date, user_id, creation_date, record_status, record_deleted";
                        string input = disposal_of_property.disposal_of_property_code + "','" + disposal_of_property.disposal_of_property_name + "','" + disposal_of_property.lastmodified_date + "','" + disposal_of_property.user_id + "','" + _creation_date + "','" + disposal_of_property.record_status + "','" + disposal_of_property.record_deleted;
                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            value = true;
                        }
                        else
                            value = false;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        value = false;
                    }
                }
            }
            return value;
        }

        public static bool UpdateDisposalType(cm_disposal_of_property disposal_of_property)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.disposal_of_property_master SET  disposal_of_property_code ='" + disposal_of_property.disposal_of_property_code + "', disposal_of_property_name ='" + disposal_of_property.disposal_of_property_name + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "', record_status ='" + disposal_of_property.record_status + "'  WHERE disposal_of_property_id ='" + disposal_of_property.disposal_of_property_id + "' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;

                    }
                    else
                    {
                        value = false;

                    }

                }
                catch (Exception ex)
                {
                    value = false;
                    throw (ex);

                }
            }
            return value;
        }

        public static List<cm_disposal_of_property> GetList()
        {
            List<cm_disposal_of_property> lstObj = new List<cm_disposal_of_property>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.disposal_of_property_master ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_disposal_of_property>();
                            while (dr.Read())
                            {
                                cm_disposal_of_property record = new cm_disposal_of_property();
                                record.disposal_of_property_id = Convert.ToInt32(dr["disposal_of_property_id"].ToString());
                                record.disposal_of_property_name = dr["disposal_of_property_name"].ToString();
                                record.disposal_of_property_code = dr["disposal_of_property_code"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
    }
    #endregion DL_cm_disposal_of_property

    #region DL_cm_gender
    public class DL_cm_gender
    {
        public static bool InsertGender(cm_gender gender)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    try
                    {
                        string tableName = "exciseautomation.gender_master";
                        string _creation_date = DateTime.Now.ToString("dd/MM/yyyy");
                        //gender_master_id, gender_code, gender_name, lastmodified_date, user_id, creation_date, record_status, record_deleted
                        string columnNames = "gender_code, gender_name, lastmodified_date, user_id, creation_date, record_status, record_deleted";
                        string input = gender.gender_code + "','" + gender.gender_name + "','" + gender.lastmodified_date + "','" + gender.user_id + "','" + _creation_date + "','" + gender.record_status + "','" + gender.record_deleted;
                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            value = true;
                        }
                        else
                            value = false;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        value = false;
                    }
                }
            }
            return value;
        }
        public static List<cm_gender> GetListILike(string texts, string colname)
        {
            List<cm_gender> lstObj = new List<cm_gender>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.gender_master where " + colname + " Ilike '%" + texts + "%'", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_gender>();
                            while (dr.Read())
                            {
                                cm_gender record = new cm_gender();
                                record.gender_master_id = Convert.ToInt32(dr["gender_master_id"].ToString());
                                record.gender_name = dr["gender_name"].ToString();
                                record.gender_code = dr["gender_code"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
        public static bool UpdateGender(cm_gender gender)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.gender_master SET  gender_code ='" + gender.gender_code + "', gender_name ='" + gender.gender_name + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "', record_status ='" + gender.record_status + "' WHERE gender_master_id ='" + gender.gender_master_id + "' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;

                    }
                    else
                    {
                        value = false;

                    }

                }
                catch (Exception ex)
                {
                    value = false;
                    throw (ex);

                }
            }
            return value;
        }
        public static List<cm_gender> GetList()
        {
            List<cm_gender> lstObj = new List<cm_gender>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.gender_master order by gender_code ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_gender>();
                            while (dr.Read())
                            {
                                cm_gender record = new cm_gender();
                                record.gender_master_id = Convert.ToInt32(dr["gender_master_id"].ToString());
                                record.gender_name = dr["gender_name"].ToString();
                                record.gender_code = dr["gender_code"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
    }
    #endregion DL_cm_gender

    #region DL_cm_idproof
    public class DL_cm_idproof
    {
        public static bool InsertIDProof(cm_idproof idproof)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    try
                    {
                        string tableName = "exciseautomation.idproof_master";
                        string _creation_date = DateTime.Now.ToString("dd/MM/yyyy");
                        //idproof_master_id, idproof_code, idproof_name, lastmodified_date, user_id, creation_date, record_status, record_deleted
                        string columnNames = "idproof_code, idproof_name, lastmodified_date, user_id, creation_date, record_status, record_deleted";
                        string input = idproof.idproof_code + "','" + idproof.idproof_name + "','" + idproof.lastmodified_date + "','" + idproof.user_id + "','" + _creation_date + "','" + idproof.record_status + "','" + idproof.record_deleted;
                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";
                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            value = true;
                        }
                        else
                            value = false;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        value = false;
                    }
                }
            }
            return value;
        }
        public static List<cm_idproof> GetListILike(string texts, string colname)
        {
            List<cm_idproof> lstObj = new List<cm_idproof>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.idproof_master where " + colname + " Ilike '%" + texts + "%' order by " + colname + "", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_idproof>();
                            while (dr.Read())
                            {
                                cm_idproof record = new cm_idproof();
                                record.idproof_master_id = Convert.ToInt32(dr["idproof_master_id"].ToString());
                                record.idproof_name = dr["idproof_name"].ToString();
                                record.idproof_code = dr["idproof_code"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
        public static bool UpdateIDproof(cm_idproof idproof)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.idproof_master SET  idproof_code ='" + idproof.idproof_code + "', idproof_name ='" + idproof.idproof_name + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "', record_status ='" + idproof.record_status + "' WHERE idproof_master_id ='" + idproof.idproof_master_id + "' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;

                    }
                    else
                    {
                        value = false;

                    }

                }
                catch (Exception ex)
                {
                    value = false;
                    throw (ex);

                }
            }
            return value;
        }
        public static List<cm_idproof> GetList()
        {
            List<cm_idproof> lstObj = new List<cm_idproof>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.idproof_master ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_idproof>();
                            while (dr.Read())
                            {
                                cm_idproof record = new cm_idproof();
                                record.idproof_master_id = Convert.ToInt32(dr["idproof_master_id"].ToString());
                                record.idproof_name = dr["idproof_name"].ToString();
                                record.idproof_code = dr["idproof_code"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
    }
    #endregion DL_cm_idproof

    #region DL_cm_offence
    public class DL_cm_offence
    {
        public static bool InsertOffencey(cm_offence offence)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    try
                    {
                        string tableName = "exciseautomation.offence_master";
                        string _creation_date = DateTime.Now.ToString("dd/MM/yyyy");
                        //offence_master_id, offence_code, offence_name, offence_type_code, lastmodified_date, user_id, creation_date, record_status, record_deleted
                        string columnNames = "offence_code, offence_name,  lastmodified_date, user_id, creation_date, record_status, record_deleted";
                        string input = offence.offence_code + "','" + offence.offence_name + "','" + offence.lastmodified_date + "','" + offence.user_id + "','" + _creation_date + "','" + offence.record_status + "','" + offence.record_deleted;
                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            value = true;
                        }
                        else
                            value = false;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        value = false;
                    }
                }
            }
            return value;
        }
        public static List<cm_offence> GetListILike(string texts, string colname)
        {
            List<cm_offence> lstObj = new List<cm_offence>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    if (colname == "offence_type_name")
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.offence_type_name from exciseautomation.offence_master a inner join exciseautomation.offence_type_master b on a.offence_type_code=b.offence_type_code where b." + colname + " Ilike '%" + texts + "%' order by offence_code", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                lstObj = new List<cm_offence>();
                                while (dr.Read())
                                {
                                    cm_offence record = new cm_offence();
                                    record.offence_master_id = Convert.ToInt32(dr["offence_master_id"].ToString());
                                    record.offence_type_code = dr["offence_code"].ToString();
                                    record.offence_type_name = dr["offence_name"].ToString();
                                    record.offence_name = dr["offence_name"].ToString();
                                    record.offence_code = dr["offence_code"].ToString();
                                    //record.record_status = dr["party_active"].ToString();
                                    lstObj.Add(record);
                                }
                            }
                        }
                    }

                    else
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.offence_master  where " + colname + " Ilike '%" + texts + "%' order by offence_code", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                lstObj = new List<cm_offence>();
                                while (dr.Read())
                                {
                                    cm_offence record = new cm_offence();
                                    record.offence_master_id = Convert.ToInt32(dr["offence_master_id"].ToString());
                                    record.offence_type_code = dr["offence_code"].ToString();
                                    record.offence_type_name = dr["offence_name"].ToString();
                                    record.offence_name = dr["offence_name"].ToString();
                                    record.offence_code = dr["offence_code"].ToString();
                                    //record.record_status = dr["party_active"].ToString();
                                    lstObj.Add(record);
                                }
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
        public static bool Updateoffence(cm_offence offence)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.offence_master SET  offence_code ='" + offence.offence_code + "', offence_name ='" + offence.offence_name + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "', record_status ='" + offence.record_status + "' WHERE offence_master_id ='" + offence.offence_master_id + "' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;

                    }
                    else
                    {
                        value = false;

                    }

                }
                catch (Exception ex)
                {
                    value = false;
                    throw (ex);

                }
            }
            return value;
        }
        public static List<cm_offence> GetList()
        {
            List<cm_offence> lstObj = new List<cm_offence>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select distinct offence_name,offence_code,offence_master_id from exciseautomation.offence_master  order by offence_code", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_offence>();
                            while (dr.Read())
                            {
                                cm_offence record = new cm_offence();
                            record.offence_master_id = Convert.ToInt32(dr["offence_master_id"].ToString());
                              //  record.offence_type_code = dr["offence_type_code"].ToString();
                              //  record.offence_type_name = dr["offence_type_name"].ToString();
                                record.offence_name = dr["offence_name"].ToString();
                                record.offence_code = dr["offence_code"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
    }
    #endregion DL_cm_offence

    #region DL_cm_offence_type
    public class DL_cm_offence_type
    {
        public static bool InsertOffenceType(cm_offence_type offence_type)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    try
                    {
                        string tableName = "exciseautomation.offence_master";
                        string _creation_date = DateTime.Now.ToString("dd/MM/yyyy");
                        //offence_type_id, offence_type_code, offence_type_name, lastmodified_date, user_id, creation_date, record_status, record_deleted
                        string columnNames = "offence_code, offence_name, lastmodified_date, user_id, creation_date, record_status, record_deleted";
                        string input = offence_type.offence_code + "','" + offence_type.offence_name + "','" + offence_type.lastmodified_date + "','" + offence_type.user_id + "','" + _creation_date + "','" + offence_type.record_status + "','" + offence_type.record_deleted;
                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            value = true;
                        }
                        else
                            value = false;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        value = false;
                    }
                }
            }
            return value;
        }
        public static List<cm_offence_type> GetListILike(string texts, string colname)
        {
            List<cm_offence_type> lstObj = new List<cm_offence_type>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.offence_type_master where " + colname + " Ilike '%" + texts + "%' order by offence_type_code", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_offence_type>();
                            while (dr.Read())
                            {
                                cm_offence_type record = new cm_offence_type();
                                record.offence_master_id = Convert.ToInt32(dr["offence_master_id"].ToString());
                                record.offence_name = dr["offence_type_name"].ToString();
                                record.offence_code = dr["offence_type_code"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
        public static bool UpdateOffenceType(cm_offence_type offence_type)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.offence_master SET  offence_code ='" + offence_type.offence_code + "', offence_name ='" + offence_type.offence_name + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "', record_status ='" + offence_type.record_status + "' WHERE offence_type_id ='" + offence_type.offence_master_id + "' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;

                    }
                    else
                    {
                        value = false;

                    }

                }
                catch (Exception ex)
                {
                    value = false;
                    throw (ex);

                }
            }
            return value;
        }

        public static List<cm_offence_type> GetList()
        {
            List<cm_offence_type> lstObj = new List<cm_offence_type>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.offence_master order by offence_name", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_offence_type>();
                            while (dr.Read())
                            {
                                cm_offence_type record = new cm_offence_type();
                                record.offence_master_id = Convert.ToInt32(dr["offence_master_id"].ToString());
                                record.offence_name = dr["offence_name"].ToString();
                                record.offence_code = dr["offence_code"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }

        public static List<cm_offence_sections> GetSectionList()
        {
            List<cm_offence_sections> lstObj = new List<cm_offence_sections>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.offence_section_master order by offence_section_code,offence_section_name", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_offence_sections>();
                            while (dr.Read())
                            {
                                cm_offence_sections record = new cm_offence_sections();
                                record.offence_section_master_id = Convert.ToInt32(dr["offence_section_master_id"].ToString());
                                record.offence_section_name = dr["offence_section_code"].ToString()+"-"+dr["offence_section_name"].ToString();
                                record.offence_section_code = dr["offence_section_code"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }

        //public static List<districtdata> GetDistrictdatas()
        //{
        //    List<districtdata> lstObj = new List<districtdata>();
        //    using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
        //    {
        //        cn.Open();
        //        try
        //        {
        //            using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.district_master ", cn))
        //            {
        //                cmd.CommandType = System.Data.CommandType.Text;
        //                NpgsqlDataReader dr = cmd.ExecuteReader();
        //                if (dr.HasRows)
        //                {
        //                    lstObj = new List<districtdata>();
        //                    while (dr.Read())
        //                    {
        //                        districtdata record = new districtdata();

        //                        record.district_code = dr["district_code"].ToString();
        //                        record.district_name = dr["district_name"].ToString();
        //                        //record.record_status = dr["party_active"].ToString();
        //                        lstObj.Add(record);
        //                    }
        //                }
        //            }
        //            cn.Close();
        //            //_log.Info("Get Party Type Master List Success");
        //        }
        //        catch (Exception ex)
        //        {
        //            //_log.Info("Get Party Type Master List Success :" + ex.Message);
        //        }

        //    }
        //    return lstObj;
        //}
    }
    #endregion DL_cm_offence_type

    #region DL_cm_religion
    public class DL_cm_religion
    {
        public static bool InsertReligion(cm_religion religion)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    try
                    {
                        string tableName = "exciseautomation.religion_master";
                        string _creation_date = DateTime.Now.ToString("dd/MM/yyyy");
                        //religion_master_id, religion_code, religion_name, lastmodified_date, user_id, creation_date, record_status, record_deleted
                        string columnNames = "religion_code, religion_name, lastmodified_date, user_id, creation_date, record_status, record_deleted";
                        string input = religion.religion_code + "','" + religion.religion_name + "','" + religion.lastmodified_date + "','" + religion.user_id + "','" + _creation_date + "','" + religion.record_status + "','" + religion.record_deleted;
                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            value = true;
                        }
                        else
                            value = false;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        value = false;
                    }
                }
            }
            return value;
        }
        public static List<cm_religion> GetListILike(string texts, string colname)
        {
            List<cm_religion> lstObj = new List<cm_religion>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.religion_master where " + colname + " Ilike '%" + texts + "%'", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_religion>();
                            while (dr.Read())
                            {
                                cm_religion record = new cm_religion();
                                record.religion_master_id = Convert.ToInt32(dr["religion_master_id"].ToString());
                                record.religion_name = dr["religion_name"].ToString();
                                record.religion_code = dr["religion_code"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
        public static bool UpdateReligion(cm_religion religion)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.religion_master SET  religion_code ='" + religion.religion_code + "', religion_name ='" + religion.religion_name + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "', record_status ='" + religion.record_status + "'  WHERE religion_master_id ='" + religion.religion_master_id + "' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;

                    }
                    else
                    {
                        value = false;

                    }

                }
                catch (Exception ex)
                {
                    value = false;
                    throw (ex);

                }
            }
            return value;
        }


        public static List<cm_religion> GetList()
        {
            List<cm_religion> lstObj = new List<cm_religion>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.religion_master order by religion_code", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_religion>();
                            while (dr.Read())
                            {
                                cm_religion record = new cm_religion();
                                record.religion_master_id = Convert.ToInt32(dr["religion_master_id"].ToString());
                                record.religion_name = dr["religion_name"].ToString();
                                record.religion_code = dr["religion_code"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
    }
    #endregion DL_cm_religion

    #region DL_cm_seizure_stage
    public class DL_cm_seizure_stage
    {
        public static bool InsertSeizureStage(cm_seizure_stage seizure_stage)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    try
                    {
                        string tableName = "exciseautomation.seizure_stage";
                        string _creation_date = DateTime.Now.ToString("dd/MM/yyyy");
                        //seizure_stage_id, seizure_stage_code, seizure_stage_name, seizure_stage_sequence, lastmodified_date, user_id, creation_date, record_status, record_deleted
                        string columnNames = "seizure_stage_code, seizure_stage_name, seizure_stage_sequence, lastmodified_date, user_id, creation_date, record_status, record_deleted";
                        string input = seizure_stage.seizure_stage_code + "','" + seizure_stage.seizure_stage_name + "','" + seizure_stage.seizure_stage_sequence + "','" + seizure_stage.lastmodified_date + "','" + seizure_stage.user_id + "','" + _creation_date + "','" + seizure_stage.record_status + "','" + seizure_stage.record_deleted;
                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            value = true;
                        }
                        else
                            value = false;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        value = false;
                    }
                }
            }
            return value;
        }

        public static bool UpdateSeizurestage(cm_seizure_stage seizure_stage)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure_stage SET  seizure_stage_code ='" + seizure_stage.seizure_stage_code + "', seizure_stage_name ='" + seizure_stage.seizure_stage_name + "', seizure_stage_sequence ='" + seizure_stage.seizure_stage_sequence + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "', record_status ='" + seizure_stage.record_status + "' WHERE seizure_stage_id ='" + seizure_stage.seizure_stage_id + "' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                    }
                    else
                    {
                        value = false;
                    }
                }
                catch (Exception ex)
                {
                    value = false;
                    throw (ex);

                }
            }
            return value;
        }
        public static List<cm_seizure_stage> GetList()
        {
            List<cm_seizure_stage> lstObj = new List<cm_seizure_stage>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.seizure_stage ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_seizure_stage>();
                            while (dr.Read())
                            {
                                cm_seizure_stage record = new cm_seizure_stage();
                                record.seizure_stage_id = Convert.ToInt32(dr["seizure_stage_id"].ToString());
                                record.seizure_stage_name = dr["seizure_stage_name"].ToString();
                                record.seizure_stage_code = dr["seizure_stage_code"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
    }
    #endregion DL_cm_seizure_stage

    #region DL_cm_seizure_status
    public class DL_cm_seizure_status
    {
        public static bool InsertSeizureStatus(cm_seizure_status seizure_status)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    try
                    {
                        string tableName = "exciseautomation.seizure_status";
                        string _creation_date = DateTime.Now.ToString("dd/MM/yyyy");
                        //seizure_status_id, seizure_status_code, seizure_status_name, edit_seizure, lastmodified_date, user_id, creation_date, record_status, record_deleted
                        string columnNames = "seizure_status_code, seizure_status_name, edit_seizure, lastmodified_date, user_id, creation_date, record_status, record_deleted";
                        string input = seizure_status.seizure_status_code + "','" + seizure_status.seizure_status_name + "','" + seizure_status.edit_seizure + "','" + seizure_status.lastmodified_date + "','" + seizure_status.user_id + "','" + _creation_date + "','" + seizure_status.record_status + "','" + seizure_status.record_deleted;
                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            value = true;
                        }
                        else
                            value = false;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        value = false;
                    }
                }
            }
            return value;
        }

        public static List<cm_seizure_status> GetList()
        {
            List<cm_seizure_status> lstObj = new List<cm_seizure_status>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.seizure_status ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_seizure_status>();
                            while (dr.Read())
                            {
                                cm_seizure_status record = new cm_seizure_status();
                                record.seizure_status_name = dr["seizure_status_name"].ToString();
                                record.seizure_status_code = dr["seizure_status_code"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
    }
    #endregion DL_cm_seizure_status

    #region DL_cm_property_type
    public class DL_cm_property_type
    {
        public static bool InsertPropertyType(cm_property_type property_type)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    try
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT max(property_type_id) FROM exciseautomation.property_type_master", cn);
                        string m = cmd1.ExecuteScalar().ToString();
                        int n = 0;
                        if (m == "")
                            n = 1;
                        else
                            n = Convert.ToInt32(m) + 1;

                        string tableName = "exciseautomation.property_type_master";
                        string _creation_date = DateTime.Now.ToString("dd/MM/yyyy");
                        //product_type_master_id, product_type_code, product_type_name, lastmodified_date, user_id, creation_date, record_status, record_deleted
                        string columnNames = "property_type_id,property_type_code, property_type, lastmodified_date, user_id, creation_date, record_status, record_deleted";
                        string input = n + "','" + property_type.product_type_code + "','" + property_type.product_type_name + "','" + property_type.lastmodified_date + "','" + property_type.user_id + "','" + _creation_date + "','" + property_type.record_status + "','" + property_type.record_deleted;
                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int a = cmd.ExecuteNonQuery();
                        if (a == 1)
                        {
                            value = true;
                        }
                        else
                            value = false;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        value = false;
                    }
                }
            }
            return value;
        }
        public static List<cm_property_type> GetListILike(string texts, string colname)
        {
            List<cm_property_type> lstObj = new List<cm_property_type>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.property_type_master where " + colname + " Ilike '%" + texts + "%' order by property_type_code", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_property_type>();
                            while (dr.Read())
                            {
                                cm_property_type record = new cm_property_type();
                                record.product_type_master_id = Convert.ToInt32(dr["property_type_id"].ToString());
                                record.product_type_name = dr["property_type"].ToString();
                                record.product_type_code = dr["property_type_code"].ToString();
                                //   record.property_code = dr["property_type_code"].ToString();
                                record.user_id = dr["user_id"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
        public static bool Updateproperty_type(cm_property_type property_type)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.property_type_master SET  property_type_code ='" + property_type.product_type_code + "', property_type ='" + property_type.product_type_name + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "', record_status ='" + property_type.record_status + "' WHERE property_type_id='" + property_type.product_type_master_id + "' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;

                    }
                    else
                    {
                        value = false;

                    }

                }
                catch (Exception ex)
                {
                    value = false;
                    throw (ex);

                }
            }
            return value;
        }

        public static List<cm_property_type> GetList()
        {
            List<cm_property_type> lstObj = new List<cm_property_type>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select distinct property_type,property_type_code,property_type_id from exciseautomation.property_type_master order by  property_type_code", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_property_type>();
                            while (dr.Read())
                            {
                                cm_property_type record = new cm_property_type();
                               record.product_type_master_id = Convert.ToInt32(dr["property_type_id"].ToString());
                                record.product_type_name = dr["property_type"].ToString();
                                record.product_type_code = dr["property_type_code"].ToString();
                              //  record.user_id = dr["user_id"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
    }
    #endregion DL_cm_property_type

    #region DL_cm_Vehicle_type
    public class DL_cm_Vehicle_type
    {
        public static bool InsertVehicleType(cm_Vehicle_type vehicle_type)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                {
                    cn.Open();
                    try
                    {
                        NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT max(vehicle_type_id) FROM exciseautomation.Vehicle_type_master", cn);
                        string m = cmd1.ExecuteScalar().ToString();
                        int n = 0;
                        if (m == "")
                            n = 1;
                        else
                            n = Convert.ToInt32(m) + 1;

                        string tableName = "exciseautomation.Vehicle_type_master";
                        string _creation_date = DateTime.Now.ToString("dd/MM/yyyy");
                        //vehicle_type_id, vehicle_type_code, vehicle_type, lastmodified_date, user_id, creation_date, record_status, record_deleted
                        string columnNames = "vehicle_type_id,vehicle_type_code, vehicle_type, lastmodified_date, user_id, creation_date, record_status, record_deleted";
                        string input = n + "','" + vehicle_type.vehicle_type_code + "','" + vehicle_type.vehicle_type + "','" + vehicle_type.lastmodified_date + "','" + vehicle_type.user_id + "','" + _creation_date + "','" + vehicle_type.record_status + "','" + vehicle_type.record_deleted;
                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int a = cmd.ExecuteNonQuery();
                        if (a == 1)
                        {
                            value = true;
                        }
                        else
                            value = false;

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        value = false;
                    }
                }
            }
            return value;
        }
        public static List<cm_Vehicle_type> GetListILike(string texts, string colname)
        {
            List<cm_Vehicle_type> lstObj = new List<cm_Vehicle_type>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.Vehicle_type_master where " + colname + " Ilike '%" + texts + "%' order by vehicle_type_code", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_Vehicle_type>();
                            while (dr.Read())
                            {
                                cm_Vehicle_type record = new cm_Vehicle_type();
                                record.vehicle_type_id = Convert.ToInt32(dr["vehicle_type_id"].ToString());
                                record.vehicle_type = dr["vehicle_type"].ToString();
                                record.vehicle_type_code = dr["vehicle_type_code"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
        public static bool UpdateVehicletype(cm_Vehicle_type vehicletype)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.vehicle_type_master SET  vehicle_type_code ='" + vehicletype.vehicle_type_code + "', vehicle_type ='" + vehicletype.vehicle_type + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "', record_status ='" + vehicletype.record_status + "' WHERE vehicle_type_id ='" + vehicletype.vehicle_type_id + "' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;

                    }
                    else
                    {
                        value = false;

                    }

                }
                catch (Exception ex)
                {
                    value = false;
                    throw (ex);

                }
            }
            return value;
        }



        public static List<cm_Vehicle_type> GetList()
        {
            List<cm_Vehicle_type> lstObj = new List<cm_Vehicle_type>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select distinct vehicle_type,vehicle_type_code from exciseautomation.Vehicle_type_master order by vehicle_type_code", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_Vehicle_type>();
                            while (dr.Read())
                            {
                                cm_Vehicle_type record = new cm_Vehicle_type();
                              //  record.vehicle_type_id = Convert.ToInt32(dr["vehicle_type_id"].ToString());
                                record.vehicle_type = dr["vehicle_type"].ToString();
                                record.vehicle_type_code = dr["vehicle_type_code"].ToString();
                                //record.record_status = dr["party_active"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();
                    //_log.Info("Get Party Type Master List Success");
                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }
    }
    #endregion DL_cm_Vehicle_type



}

