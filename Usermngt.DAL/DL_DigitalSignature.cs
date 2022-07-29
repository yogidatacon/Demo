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
    public class DL_DigitalSignature
    {

       
            public static bool InserDigitalSignature(DigitalSignature DigitalSignature)
            {
                bool value = false;
                using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
                {
                    if (cn != null && ConnectionState.Closed == cn.State)
                    {
                        cn.Open();
                        try
                        {

                        StringBuilder str = new StringBuilder();
                        str.Append("INSERT INTO exciseautomation.digital_signature(dongle_userid, dongle_id, dongle_password, creation_date,  user_id,  dongle_ca)values(");
                        str.Append("'" + DigitalSignature.dongle_userid + "','" + DigitalSignature.dongle_id + "','" + DigitalSignature.dongle_password + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DigitalSignature.user_id + "','" + DigitalSignature.certyfing_authority + "')");
                            NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
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

        public static DigitalSignature GetDetails(string id)
        {
            DigitalSignature record = new DigitalSignature();

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT a.*,b.User_name,b.role_name_code,b.employee_master_id FROM exciseautomation.digital_signature a inner join exciseautomation.user_registration b on  a.dongle_userid=b.user_registration_id  where a.digital_signature_id='" + id + "'", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {

                                record.digital_signature_id = Convert.ToInt32(dr["digital_signature_id"].ToString());
                                record.dongle_userid = Convert.ToInt32(dr["dongle_userid"].ToString());
                                record.empid = dr["employee_master_id"].ToString();
                                record.role_name_code = dr["role_name_code"].ToString();
                                record.emp_name = dr["User_name"].ToString();
                                record.dongle_password = dr["dongle_password"].ToString();
                                record.certyfing_authority = dr["dongle_ca"].ToString();
                                record.dongle_id = dr["dongle_id"].ToString();
                                
                            }
                        }
                    }
                    cn.Close();

                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return record;
        }

        public static bool UpdateDigitalSignature(DigitalSignature DigitalSignature)
            {
                bool value = false;
                using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
                {
                    cn.Open();
                    try
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand("UPDATE exciseautomation.digital_signature SET  dongle_userid ='"+DigitalSignature.dongle_userid+"', dongle_id ='"+DigitalSignature.dongle_id+"', dongle_password ='"+DigitalSignature.dongle_password+"', lastmodified_date ='"+DateTime.Now.ToShortDateString()+ "',dongle_ca='"+DigitalSignature.certyfing_authority+"'  WHERE digital_signature_id ='" + DigitalSignature.digital_signature_id+"' ", cn);
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
       
            public static List<DigitalSignature> GetList()
            {
                List<DigitalSignature> lstObj = new List<DigitalSignature>();
                using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
                {
                    cn.Open();
                    try
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT a.*,c.role_name,b.user_name FROM exciseautomation.digital_signature a inner join exciseautomation.user_registration b on a.dongle_userid=b.user_registration_id left join exciseautomation.role_master c on b.role_name_code=c.role_name_code   ", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                lstObj = new List<DigitalSignature>();
                                while (dr.Read())
                                {
                                DigitalSignature record = new DigitalSignature();
                                    record.digital_signature_id= Convert.ToInt32(dr["digital_signature_id"].ToString());
                                record.dongle_userid = Convert.ToInt32(dr["dongle_userid"].ToString());
                                record.role_name= dr["role_name"].ToString();
                                record.emp_name= dr["User_name"].ToString();
                                record.dongle_password= dr["dongle_password"].ToString();
                                record.dongle_id = dr["dongle_id"].ToString();
                                    lstObj.Add(record);
                                }
                            }
                        }
                        cn.Close();
                        
                    }
                    catch (Exception ex)
                    {
                        //_log.Info("Get Party Type Master List Success :" + ex.Message);
                    }

                }
                return lstObj;
            }


        public static  DigitalSignature  GetRolename(int id)
        {

            DigitalSignature sto = new DigitalSignature();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT c.role_name FROM exciseautomation.employee_master a inner join exciseautomation.user_registration b on a.employee_master_id=b.employee_master_id inner join exciseautomation.role_master c on b.role_name_code=c.role_name_code where b.user_registration_id='" + id+"'", cn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            sto.role_name = dr["role_name"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                return sto;
            }
        }


        public static List<DigitalSignature> GetEmpList()
        {
            List<DigitalSignature> lstObj = new List<DigitalSignature>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand(" SELECT distinct b.role_name,a.role_name_code,a.user_id,a.User_name, a.user_registration_id,a.employee_master_id FROM  exciseautomation.user_registration a left join exciseautomation.role_master b on a.role_name_code=b.role_name_code where a.Party_code='ALL'  order by b.role_name ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            lstObj = new List<DigitalSignature>();
                            while (dr.Read())
                            {
                                DigitalSignature record = new DigitalSignature();
                                record.user_registration_id = Convert.ToInt32(dr["user_registration_id"].ToString());
                                record.role_name = dr["role_name"].ToString();
                                record.role_name_code = dr["role_name_code"].ToString();
                                record.emp_name = dr["User_name"].ToString();
                                record.empid = dr["employee_master_id"].ToString();
                                record.user_id = dr["User_id"].ToString();
                                lstObj.Add(record);
                            }
                        }
                    }
                    cn.Close();

                }
                catch (Exception ex)
                {
                    //_log.Info("Get Party Type Master List Success :" + ex.Message);
                }

            }
            return lstObj;
        }


    }

    }

