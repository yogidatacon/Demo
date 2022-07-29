using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Usermngt.Entities;
using Npgsql;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Usermngt.DAL
{
    #region DL_cm_seiz_ExcisableArticlesSeized
    public class DL_cm_seiz_ExcisableArticlesSeized
    {
        public static List<cm_seiz_ExcisableArticlesSeized> GetList(string seizureNo)
        {
            string[] dept = seizureNo.Split('&');
            seizureNo = dept[0];
            string d = "";
            if (dept[1] == "Excise"|| dept[1] == "E")
                d = "E";
            else
                d = "P";
            List<cm_seiz_ExcisableArticlesSeized> lstObj = new List<cm_seiz_ExcisableArticlesSeized>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.article_name,c.article_sub_category_name,d.article_category_name  from exciseautomation.seizure_excisable_articles a left join exciseautomation.article_name_master b on a.article_name_code=b.article_name_code "
                          + "  left join exciseautomation.article_sub_category_master c on  a.article_sub_category_code = c.article_sub_category_code inner join exciseautomation.article_category_master d on a.article_category_code=d.article_category_code  where seizureNo= " + seizureNo+" and a.raidby='"+d+ "' order by a.seizureNo,seizure_excisable_articles_id desc", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_seiz_ExcisableArticlesSeized>();
                            while (dr.Read())
                            {
                                cm_seiz_ExcisableArticlesSeized record = new cm_seiz_ExcisableArticlesSeized();
                                record.seizure_excisable_articles_id =Convert.ToInt32(dr["seizure_excisable_articles_id"].ToString());
                                record.article_category_code = dr["article_category_code"].ToString();
                                record.article_category_name = dr["article_category_name"].ToString();
                                record.seizureno =Convert.ToInt32(dr["seizureno"].ToString());
                                record.article_sub_category_code = dr["article_sub_category_code"].ToString();
                                record.article_sub_category_name = dr["article_sub_category_name"].ToString();
                                record.article_name = dr["article_name"].ToString();
                                record.quantity = (dr["quantity"].ToString());
                                record.uom_code = dr["uom_code"].ToString();
                                record.record_status = dr["record_status"].ToString();
                                record.manufacturer_code = dr["manufacturer_name"].ToString();
                                record.packingsize_code = dr["packingsize"].ToString();
                                
                                record.actioncompleted = dr["actioncompleted"].ToString();
                                record.date_of_destruction = dr["date_of_destruction"].ToString();
                                record.user_id = dr["user_id"].ToString();
                               
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

        

        public static cm_seiz_ExcisableArticlesSeized GetDetails(string tableId)
        {
            cm_seiz_ExcisableArticlesSeized record = new cm_seiz_ExcisableArticlesSeized();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,(b.article_name) as article_name  from exciseautomation.seizure_excisable_articles a left join exciseautomation.article_name_master b on a.article_name_code=b.article_name_code where seizure_excisable_articles_id=" + tableId, cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr1 = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr1);
                        dr1.Close();
                        //if (dr1.HasRows)
                        //{
                            record = new cm_seiz_ExcisableArticlesSeized();
                            foreach (DataRow dr in dt.Rows)
                            {
                                //cm_seiz_ExcisableArticlesSeized record = new cm_seiz_ExcisableArticlesSeized();
                                record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                                record.seizure_excisable_articles_id = Convert.ToInt32(dr["seizure_excisable_articles_id"].ToString());
                                record.article_name_code =dr["article_name_code"].ToString();
                                record.article_category_code = dr["article_category_code"].ToString();
                                record.article_sub_category_code = dr["article_sub_category_code"].ToString();
                                record.manufacturer_code =(dr["manufacturer_name"]?.ToString()??string.Empty);
                                record.article_name = dr["article_name"].ToString();
                                record.quantity = (dr["quantity"].ToString());
                                record.uom_code = dr["uom_code"].ToString();
                                record.farmingsize = dr["farmingsize"].ToString();
                                record.packingsize_code =(dr["packingsize"].ToString());
                                record.remarks = dr["remarks"].ToString();
                                record.record_status = dr["record_status"].ToString();
                            record.batchno = dr["batchno"].ToString();
                           // string c = dr["manufacturing_date"].ToString().Trim();
                            if (dr["manufacturing_date"].ToString()!=null&& dr["manufacturing_date"].ToString().Trim()!="")
                            record.manufacturing_date = dr["manufacturing_date"].ToString().Substring(0,10).Replace("/", "-");
                            record.prod_state_code = dr["prod_state_code"].ToString();
                            record.sale_state_code = dr["sale_state_code"].ToString();
                            record.Different_Liquor = dr["Different_Liquor"].ToString();
                            record.raidby = dr["raidby"].ToString();
                                record.docs = new List<Seizure_Docs>();

                                try
                                {
                                    using (NpgsqlCommand cmd1 = new NpgsqlCommand("select * from exciseautomation.Seizure_Docs where doc_id='" + record.seizure_excisable_articles_id + "' and doc_type_code='EAS' and raidby='"+record.raidby+"' order by seizure_docs_id", cn))
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
                                                record.docs.Add(doc);
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
                        }
                    //}
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

        public static List<cm_court> GetFilterLists(string distcode,string fromdate)
        {
            List<cm_court> lstObj = new List<cm_court>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT  dmcase_registration_id, seizure_fir_id, dm.district_code,d.district_name, dcm.district_court_master_id, 'Vehicle' as confiscation, CASE WHEN raidby='E' then 'Excise' else 'Police' end as Department,oldcaseno FROM exciseautomation.dmcase_registration dm inner join exciseautomation.district_court_master dcm on dm.district_code=dcm.district_code and dcm.district_court_master_id=dm.district_court_master_id inner join exciseautomation.court_master cm on dcm.court_master_code=cm.court_master_code inner join exciseautomation.district_master d on d.district_code=dm.district_code where cm.court_master_code='"+distcode+ "' and casedate='"+fromdate+"'  order by casedate", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_court>();
                            while (dr.Read())
                            {
                                cm_court record = new cm_court();
                                record.dmcase_registration_id = Convert.ToInt32(dr["dmcase_registration_id"].ToString());
                                record.seizure_fir_no = Convert.ToInt32(dr["seizure_fir_id"].ToString());
                                record.district_code = dr["district_code"].ToString();
                                record.district_name = dr["district_name"].ToString();
                                record.caseno = dr["caseno"].ToString();
                                record.raidby = dr["Department"].ToString();
                                record.vp = dr["confiscation"].ToString();

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
        public static List<cm_seiz_ExcisableArticlesSeized> ArticleSearch(string _article)
        {
            List<cm_seiz_ExcisableArticlesSeized> lstObj = new List<cm_seiz_ExcisableArticlesSeized>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.article_name_master where article_name ilike '%" + _article+"%'", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<cm_seiz_ExcisableArticlesSeized>();
                            while (dr.Read())
                            {
                                cm_seiz_ExcisableArticlesSeized record = new cm_seiz_ExcisableArticlesSeized();
                              //  record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                                record.article_category_code = dr["article_category_code"].ToString();
                                record.article_sub_category_code = dr["article_sub_category_code"].ToString();
                                record.article_name_code = dr["article_name_code"].ToString();
                                record.article_name= dr["article_name"].ToString();
                                // record.quantity =(dr["quantity"].ToString());
                                // record.uom_code = dr["uom_code"].ToString();
                                record.record_status = dr["record_status"].ToString();
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

        public static bool InsertSeiz_ExcisableArticlesSeized(cm_seiz_ExcisableArticlesSeized obj)
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
                        int max = Convert.ToInt32(DL_Common.GetMaxID("exciseautomation.seizure_excisable_articles", "seizure_excisable_articles_id").ToString()) + 1;
                        StringBuilder str = new StringBuilder();
                       
                        string tableName = "exciseautomation.seizure_excisable_articles";
                        string columnNames = "";
                        string input = "";
                        
                        if (obj.manufacturing_date == null || obj.manufacturing_date == "")
                        {
                            columnNames = "seizure_excisable_articles_id, seizureno, article_category_code, article_sub_category_code, article_name_code,manufacturer_name, uom_code,packingsize, quantity, farmingsize,  lastmodified_date, user_id, creation_date, record_deleted, remarks, record_status,raidby,batchno ,prod_state_code ,sale_state_code,ipaddress,Different_Liquor";
                            input = max + "','" + obj.seizureno + "','" + obj.article_category_code + "','" + obj.article_sub_category_code + "','" + obj.article_name_code + "','" + obj.manufacturer_code + "','" + obj.uom_code + "','" + obj.packingsize_code + "','" + obj.quantity + "','" + obj.farmingsize + "','" + DateTime.Now.ToShortDateString() + "','" + obj.user_id + "','" + DateTime.Now.ToShortDateString() + "','" + obj.record_deleted + "','" + obj.remarks + "','" + obj.record_status + "','" + obj.raidby + "','" + obj.batchno + "','" + obj.prod_state_code + "','" + obj.sale_state_code+"','"+obj.ipaddress+"','"+obj.Different_Liquor;
                        }
                        else
                        {
                            columnNames = "seizure_excisable_articles_id, seizureno, article_category_code, article_sub_category_code, article_name_code,manufacturer_name, uom_code,packingsize, quantity, farmingsize,  lastmodified_date, user_id, creation_date, record_deleted, remarks, record_status,raidby,batchno ,manufacturing_date,prod_state_code ,sale_state_code,ipaddress,Different_Liquor";
                            input = max + "','" + obj.seizureno + "','" + obj.article_category_code + "','" + obj.article_sub_category_code + "','" + obj.article_name_code + "','" + obj.manufacturer_code + "','" + obj.uom_code + "','" + obj.packingsize_code + "','" + obj.quantity + "','" + obj.farmingsize + "','" + DateTime.Now.ToShortDateString() + "','" + obj.user_id + "','" + DateTime.Now.ToShortDateString() + "','" + obj.record_deleted + "','" + obj.remarks + "','" + obj.record_status + "','" + obj.raidby + "','" + obj.batchno + "','" + obj.manufacturing_date + "','" + obj.prod_state_code + "','" + obj.sale_state_code + "','" + obj.ipaddress+"','"+obj.Different_Liquor;
                        }
                        string InsertQuery = "INSERT INTO " + tableName + " ( " + columnNames + ") VALUES( '" + input + "' ) ";

                        NpgsqlCommand cmd = new NpgsqlCommand(InsertQuery, cn);
                        int n = cmd.ExecuteNonQuery();
                        if (n == 1)
                        {
                            for (int i = 0; i < obj.docs.Count; i++)
                            {
                                n = 0;
                                str = new StringBuilder();
                                str.Append("INSERT INTO exciseautomation.seizure_docs(raidby,seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date)");
                                str.Append("Values('" + obj.raidby + "','" + obj.seizureno + "','" + max + "','" + obj.docs[i].doc_name + "', '" + obj.docs[i].description + "','" + obj.docs[i].doc_path + "','EAS','" + obj.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "')");
                                NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                                n = cmd3.ExecuteNonQuery();
                            }
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

        public static bool Update_ExcisableArticlesSeized(cm_seiz_ExcisableArticlesSeized cm_obj)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                if (cn != null && ConnectionState.Closed == cn.State)
                cn.Open();
                NpgsqlTransaction trn;
                trn = cn.BeginTransaction();
                try
                {
                    StringBuilder str = new StringBuilder();
                    NpgsqlCommand cmd;
                    if (cm_obj.manufacturing_date==""|| cm_obj.manufacturing_date==null)
                     cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure_excisable_articles  SET  article_category_code ='" + cm_obj.article_category_code+ "', article_sub_category_code ='" + cm_obj.article_sub_category_code+ "', manufacturer_name ='" + cm_obj.manufacturer_code + "', packingsize ='" + cm_obj.packingsize_code + "', article_name_code ='" + cm_obj.article_name_code+ "', uom_code ='" + cm_obj.uom_code+ "', quantity ='" + cm_obj.quantity+ "', farmingsize ='" + cm_obj.farmingsize+ "', lastmodified_date ='" + DateTime.Now.ToShortDateString()+ "', user_id ='" + cm_obj.user_id+ "', record_deleted ='" + cm_obj.record_deleted+ "', remarks ='" + cm_obj.remarks+ "', record_status ='"+ cm_obj.record_status+ "',batchno ='"+cm_obj.batchno+ "',prod_state_code ='" + cm_obj.prod_state_code + "',sale_state_code='" + cm_obj.sale_state_code + "',ipaddress='"+cm_obj.ipaddress+ "',Different_Liquor='"+cm_obj.Different_Liquor+"'  WHERE seizure_excisable_articles_id =" + cm_obj.seizure_excisable_articles_id + "", cn);
                    else
                        cmd = new NpgsqlCommand("UPDATE exciseautomation.seizure_excisable_articles  SET  article_category_code ='" + cm_obj.article_category_code + "', article_sub_category_code ='" + cm_obj.article_sub_category_code + "', manufacturer_name ='" + cm_obj.manufacturer_code + "', packingsize ='" + cm_obj.packingsize_code + "', article_name_code ='" + cm_obj.article_name_code + "', uom_code ='" + cm_obj.uom_code + "', quantity ='" + cm_obj.quantity + "', farmingsize ='" + cm_obj.farmingsize + "', lastmodified_date ='" + DateTime.Now.ToShortDateString() + "', user_id ='" + cm_obj.user_id + "', record_deleted ='" + cm_obj.record_deleted + "', remarks ='" + cm_obj.remarks + "', record_status ='" + cm_obj.record_status + "',batchno ='" + cm_obj.batchno + "',manufacturing_date='" + cm_obj.manufacturing_date + "',prod_state_code ='" + cm_obj.prod_state_code + "',sale_state_code='" + cm_obj.sale_state_code + "',ipaddress='" + cm_obj.ipaddress + "',Different_Liquor='" + cm_obj.Different_Liquor + "'  WHERE seizure_excisable_articles_id =" + cm_obj.seizure_excisable_articles_id + "", cn);
                    int n = cmd.ExecuteNonQuery();
                    
                    cmd = new NpgsqlCommand("Delete from exciseautomation.seizure_docs where doc_type_code='EAS' and doc_id='" + cm_obj.seizure_excisable_articles_id + "' and raidby='" + cm_obj.raidby + "'", cn);
                    cmd.ExecuteNonQuery();
                    for (int i = 0; i < cm_obj.docs.Count; i++)
                    {
                        str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.seizure_docs(seizureno,doc_id, doc_name,doc_desc, doc_path, doc_type_code, user_id, creation_date,raidby)");
                        str.Append("Values('" + cm_obj.seizureno + "','" +cm_obj.seizure_excisable_articles_id + "','"+ cm_obj.docs[i].doc_name + "', '" + cm_obj.docs[i].description + "','" + cm_obj.docs[i].doc_path + "','EAS','" + cm_obj.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+cm_obj.raidby+"')");
                        NpgsqlCommand cmd3 = new NpgsqlCommand(str.ToString(), cn);
                        n = cmd3.ExecuteNonQuery();
                    }
                    if (n == 1)
                    {
                        value = true;
                        trn.Commit();
                    }
                    else
                    {
                        value = false;
                        trn.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    value = false;
                    trn.Rollback();
                    throw (ex);
                }
            }
            return value;
        }
    }
    #endregion DL_cm_seiz_ExcisableArticlesSeized
}
