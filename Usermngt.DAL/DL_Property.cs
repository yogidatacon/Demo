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
   public class DL_Property
    {
        public static List<ThanaMaster> GetThana()
        {
            List<ThanaMaster> thana = new List<ThanaMaster>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select  b.*,a.division_name,t.state_name,c.district_name from exciseautomation.division_master a inner join  exciseautomation.State_master t on a.state_code=t.state_code inner join exciseautomation.district_master c on  a.division_code = c.division_code inner join exciseautomation.thana_master b on c.district_code=b.district_code ", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    //cmd.Parameters.AddWithValue("@UserID", userid);
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        thana = new List<ThanaMaster>();
                        while (dr.Read())
                        {
                            ThanaMaster thanas = new ThanaMaster();
                            thanas.thana_master_id = dr["thana_master_id"].ToString();
                            thanas.thana_code = dr["thana_code"].ToString();
                            thanas.thana_name = dr["thana_name"].ToString();
                            thanas.district_code = dr["district_code"].ToString();
                            thanas.division_code = dr["division_code"].ToString();
                            thanas.state_code = dr["state_code"].ToString();
                            thanas.state_name = dr["state_name"].ToString();
                            thanas.district_name = dr["district_name"].ToString();
                            thanas.division_name = dr["division_name"].ToString();
                            thanas.user_id = dr["user_id"].ToString();
                            thana.Add(thanas);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                return thana;
            }
        }



        public static string Insert(cm_seiz_Property Property)
        {
            string value = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                NpgsqlCommand cmd1 = new NpgsqlCommand("SELECT max(seizure_propertydetails_id) FROM exciseautomation.seizure_propertydetails", cn);
                string m = cmd1.ExecuteScalar().ToString();
                int n = 0;
                if (m == "")
                    n = 1;
                else
                    n = Convert.ToInt32(m) + 1;

                try
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("INSERT INTO exciseautomation.seizure_propertydetails( seizure_propertydetails_id, seizureno, property_type_code, propertyaddress, propertylocation, propertylandmark, propertycriclename, propertymauzaname, propertykhatano, propertykhasrano, propertythanano, ownername, presentaddress, permanentaddress, contactno, ipaddress,lastmodified_date, user_id, creation_date, record_status,raidby)Values(");
                    str.Append("'"+n+ "','" + Property.seizureno + "','" + Property.property_type_code + "','" + Property.propertyaddress + "','" + Property.propertylocation + "','" +Property.propertylandmark + "','" + Property.propertycriclename + "','" + Property.propertymauzaname + "','" + Property.propertykhatano + "','" +Property.propertykhasrano + "','" + Property.propertythanano + "','"+Property.ownername+"','"+Property.presentaddress+"','"+Property.permanentaddress+"','"+Property.contactno+"','"+Property.ipaddress+"','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + Property.user_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','"+Property.record_status+"','"+Property.raidby+"')");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int a = cmd.ExecuteNonQuery();
                    if (a == 1)
                    {
                       
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



        public static string Update(cm_seiz_Property Property)
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
                    str.Append("UPDATE exciseautomation.seizure_propertydetails SET   property_type_code ='"+Property.property_type_code+"', propertyaddress ='"+Property.propertyaddress+"', propertylocation ='"+Property.propertylocation+"', propertylandmark ='"+Property.propertylandmark+"', propertycriclename ='"+Property.propertycriclename+"', propertymauzaname ='"+Property.propertymauzaname+"', propertykhatano ='"+Property.propertykhatano+"', propertykhasrano ='"+Property.propertykhasrano+"', propertythanano ='"+Property.propertythanano+"', ownername ='"+Property.ownername+"', presentaddress ='"+Property.presentaddress+"', permanentaddress ='"+Property.permanentaddress+"', contactno ='"+Property.contactno+"',  lastmodified_date ='"+DateTime.Now.ToShortDateString()+"',raidby='"+Property.raidby+"',ipaddress='"+Property.ipaddress+"'  WHERE seizure_propertydetails_id ='"+Property.seizure_propertydetails_id+"' ");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        VAL = "0";
                        //_log.Info("Sugarcanepurchase Insertion Sucess:" + scp.sugarcanepurchase_id + '-' + scp.party_code);
                        trn.Commit();
                        cn.Close();
                    }
                    else
                    {
                        trn.Rollback();
                        VAL = "1";
                        //_log.Info("Sugarcanepurchase Insertion Fail:" + scp.sugarcanepurchase_id + '-' + scp.party_code);
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



        public static List<cm_seiz_Property> GetList(string seizureNo)
        {
            string[] dept = seizureNo.Split('&');
            seizureNo = dept[0];
            string d = "";
            if (dept[1] == "Excise" || dept[1] == "E")
                d = "E";
            else
                d = "P";
            List<cm_seiz_Property> property = new List<cm_seiz_Property>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;

                    // cmd = new NpgsqlCommand("SELECT a.*,b.property_type FROM exciseautomation.seizure_propertydetails a inner join exciseautomation.property_type_master b on a.property_type_code=b.property_type_code where a.user_id='" + userid+"' ORDER BY seizure_propertydetails_id", cn);
                    cmd = new NpgsqlCommand("SELECT a.user_id, a.*,b.property_type FROM exciseautomation.seizure_propertydetails a inner join exciseautomation.property_type_master b on a.property_type_code=b.property_type_code where a.seizureNo='" + seizureNo + "' and raidby='"+d+"' ORDER BY seizure_propertydetails_id", cn);

                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        property = new List<cm_seiz_Property>();
                        while (dr.Read())
                        {
                            cm_seiz_Property record = new cm_seiz_Property();
                            record.seizure_propertydetails_id = Convert.ToInt32(dr["seizure_propertydetails_id"].ToString());
                            record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());                            
                            record.property_type_code =dr["property_type_code"].ToString();
                            record.propertyaddress = dr["propertyaddress"].ToString();
                            record.propertylocation = dr["propertylocation"].ToString();
                            record.propertylandmark = dr["propertylandmark"].ToString();
                            record.propertycriclename = dr["propertycriclename"].ToString();
                            record.propertymauzaname = dr["propertymauzaname"].ToString();
                            record.propertykhasrano = dr["propertykhasrano"].ToString();
                            record.propertykhatano = dr["propertykhatano"].ToString();
                            record.propertythanano = dr["propertythanano"].ToString();
                            record.property_type = dr["property_type"].ToString();
                            record.record_status = dr["record_status"].ToString();
                             // record.user_id = dr["user_id "].ToString();
                            record.ownername = dr["ownername"].ToString();
                            record.permanentaddress = dr["permanentaddress"].ToString();
                            record.presentaddress = dr["presentaddress"].ToString();
                            record.user_id = dr["user_id"].ToString();
                            record.actioncompleted = dr["actioncompleted"].ToString();
                            record.date_of_destruction = dr["date_of_destruction"].ToString();
                            property.Add(record);
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return property;
        }


        public static cm_seiz_Property GetDetails(string userid, int Propertyid)
        {
            cm_seiz_Property record = new cm_seiz_Property();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM exciseautomation.seizure_propertydetails where user_id='" + userid + "' and seizure_propertydetails_id='"+Propertyid+"'  ", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            record.seizure_propertydetails_id = Convert.ToInt32(dr["seizure_propertydetails_id"].ToString());
                            record.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                            record.property_type_code = dr["property_type_code"].ToString();
                            record.propertyaddress = dr["propertyaddress"].ToString();
                            record.propertylocation = dr["propertylocation"].ToString();
                            record.propertylandmark = dr["propertylandmark"].ToString();
                            record.propertycriclename = dr["propertycriclename"].ToString();
                            record.propertymauzaname = dr["propertymauzaname"].ToString();
                            record.propertykhasrano = dr["propertykhasrano"].ToString();
                            record.propertykhatano = dr["propertykhatano"].ToString();
                            record.propertythanano = dr["propertythanano"].ToString();
                            record.contactno= dr["contactno"].ToString();
                            // record.user_id = dr["user_id "].ToString();
                            record.ownername = dr["ownername"].ToString();
                            record.permanentaddress = dr["permanentaddress"].ToString();
                            record.presentaddress = dr["presentaddress"].ToString();
                           
                        }

                    }

                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return record;
        }


    }
}
