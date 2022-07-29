
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
    public class DL_User_Mngnt
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static List<State> GetStateList(string userid)
        {
            List<State> states = new List<State>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.country_name from exciseautomation.State_master a inner join  exciseautomation.country_master b on a.country_code=b.country_code order by a.state_code", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            states = new List<State>();
                            while (dr.Read())
                            {
                                State state = new State();
                                state.state_id = dr["state_master_id"].ToString();
                                state.state_Code = dr["state_Code"].ToString();
                                state.state_name = dr["State_Name"].ToString();
                                state.status = dr["status"].ToString();
                                state.user_id = dr["State_Name"].ToString();
                                state.country_name = dr["country_name"].ToString();
                                states.Add(state);
                            }
                        }
                    }
                    _log.Info("Get State List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get State List Fail :"+ex.Message);
                }
            }
            return states;
        }
        public static List<District> GetAllDistricts()
        {
            List<District> districts = new List<District>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    if (0 == 0)
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("select * from  exciseautomation.district_master  order by district_name", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            //cmd.Parameters.AddWithValue("@UserID", userid);
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                districts = new List<District>();
                                while (dr.Read())
                                {
                                    District district = new District();
                                    district.district_Code = dr["District_Code"].ToString();
                                    district.district_Name = dr["District_Name"].ToString();
                                    //district.state_name = dr["State_Name"].ToString();
                                    //district.state_Code = dr["state_Code"].ToString();
                                    //district.division_Code = dr["division_Code"].ToString();
                                    //district.division_name = dr["division_name"].ToString();
                                    //district.id = dr["district_master_id"].ToString();
                                    districts.Add(district);
                                }
                            }
                        }
                    }
                    else
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("select c.*,a.division_name,t.state_name from exciseautomation.division_master a inner join  exciseautomation.State_master t on a.state_code=t.state_code inner join exciseautomation.district_master c on  a.division_code = c.division_code order by district_master_id", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            //cmd.Parameters.AddWithValue("@UserID", userid);
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                districts = new List<District>();
                                while (dr.Read())
                                {
                                    District district = new District();
                                    district.district_Code = dr["District_Code"].ToString();
                                    district.district_Name = dr["District_Name"].ToString();
                                    district.state_name = dr["State_Name"].ToString();
                                    district.state_Code = dr["state_Code"].ToString();
                                    district.division_Code = dr["division_Code"].ToString();
                                    district.division_name = dr["division_name"].ToString();
                                    district.id = dr["district_master_id"].ToString();
                                    districts.Add(district);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }


            }
            return districts;
        }

        public static bool DeletefileEascm(string v1, string v2, string v3, string v4)
        {
            bool val = false;

            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(" delete   FROM  exciseautomation." + v1 + " where eascm_docs_id='" + v2 + "' and party_code='"+v4+"' and doc_path Ilike'%" + v3 + "' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    val = true;


                }

                catch (Exception ex)
                {
                    val = false;
                }


            }
            return val;
        }

        public static bool Deletefile(string v1, string v2, string v3)
        {
            bool val = false;
          
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(" delete   FROM  exciseautomation."+v1+ " where seizureno='"+v2+ "' and doc_path Ilike'%"+v3+"' ", cn);
                    int n = cmd.ExecuteNonQuery();
                    val = true;


                }

                catch (Exception ex)
                {
                    val = false;
                }


            }
            return val;
        }

        public static List<cm_court> GetAllDistrictsPR(string distid)
        {
            List<cm_court> districts1 = new List<cm_court>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    if (distid != "")
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("select f.raidby, b.seizureno,Rtrim(Ltrim(f.prfirno)) as prfirno,f.seizure_fir_id,t.thana_code,t.thana_name,"
                            + " t.district_code,d.district_name from exciseautomation.seizure_basicinfo b inner join exciseautomation.thana_master t on t.thana_code=b.thana_code inner join exciseautomation.district_master d on d.district_code=t.district_code inner join exciseautomation.seizure_fir f on f.seizureno=b.seizureno where  d.district_code='" + distid + "'", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            //cmd.Parameters.AddWithValue("@UserID", userid);
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                districts1 = new List<cm_court>();
                                while (dr.Read())
                                {
                                    cm_court district = new cm_court();
                                    district.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                                    district.prfirno = dr["prfirno"].ToString();
                                    district.thana_name = dr["thana_name"].ToString();
                                    district.thana_code = dr["thana_code"].ToString();
                                    district.raidby = dr["raidby"].ToString();
                                    //district.state_Code = dr["state_Code"].ToString();
                                    //district.division_Code = dr["division_Code"].ToString();
                                    //district.division_name = dr["division_name"].ToString();
                                    //district.id = dr["district_master_id"].ToString();
                                    districts1.Add(district);
                                }
                            }
                        }
                    }
                    else
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("select c.*,a.division_name,t.state_name from exciseautomation.division_master a inner join  exciseautomation.State_master t on a.state_code=t.state_code inner join exciseautomation.district_master c on  a.division_code = c.division_code order by district_master_id", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            //cmd.Parameters.AddWithValue("@UserID", userid);
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                districts1 = new List<cm_court>();
                                while (dr.Read())
                                {
                                    cm_court district = new cm_court();

                                    district.seizureno = Convert.ToInt32(dr["seizureno"].ToString());
                                    district.prfirno = dr["prfirno"].ToString();
                                    district.thana_name = dr["thana_name"].ToString();
                                    //district.state_Code = dr["state_Code"].ToString();
                                    //district.division_Code = dr["division_Code"].ToString();
                                    //district.division_name = dr["division_name"].ToString();
                                    //district.id = dr["district_master_id"].ToString();
                                    districts1.Add(district);

                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }


            }
            return districts1;
        }
        public static List<cm_court> GetDistrictCourtList(string distid)
        {
            List<cm_court> districts1 = new List<cm_court>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT  court_master_code,court_master_name FROM  exciseautomation.court_master order by court_master_name", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            districts1 = new List<cm_court>();
                            while (dr.Read())
                            {
                                cm_court district = new cm_court();
                                // district.district_court_master_id = Convert.ToInt32(dr["district_court_master_id"].ToString());
                                district.court_master_name = dr["court_master_name"].ToString();
                                district.court_master_code = dr["court_master_code"].ToString();
                                districts1.Add(district);
                            }
                        }
                    }

                }

                catch (Exception ex)
                {

                }


            }
            return districts1;
        }
        public static List<State> SearchState(string tablename, string column, string value)
        {
            List<State> states = new List<State>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.country_name from exciseautomation.State_master a inner join  exciseautomation.country_master b on a.country_code=b.country_code  where " + column + " Ilike '%" + value + "%' order by " + column + "", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            states = new List<State>();
                            while (dr.Read())
                            {
                                State state = new State();
                                state.state_id = dr["state_master_id"].ToString();
                                state.state_Code = dr["state_Code"].ToString();
                                state.state_name = dr["State_Name"].ToString();
                                state.status = dr["status"].ToString();
                                state.user_id = dr["State_Name"].ToString();
                                state.country_name = dr["country_name"].ToString();
                                states.Add(state);
                            }
                        }
                    }
                    _log.Info("Get State List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get State List Fail :" + ex.Message);
                }
            }
            return states;

        }

        public static CustomerDetails GetCustomerDetails(string cutid)
        {
            CustomerDetails cust = new CustomerDetails();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.state_name from exciseautomation.customer_master a inner join  exciseautomation.state_master b on a.state_code=b.state_code where a.customer_id='" + cutid + "'", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                           
                            while (dr.Read())
                            {

                                cust.customer_id = dr["customer_id"].ToString();
                                cust.cust_name = dr["cust_name"].ToString();
                                cust.district_code = dr["district_code"].ToString();
                                cust.thana_code = dr["thana_code"].ToString();
                                cust.state_code = dr["state_code"].ToString();
                                cust.cust_address = dr["cust_address"].ToString();
                                cust.pincode = dr["pincode"].ToString();
                                cust.cust_mobile = dr["cust_mobile"].ToString();
                                cust.cust_email = dr["cust_email"].ToString();
                                cust.district_name = dr["district_name"].ToString();
                                cust.thana_name = dr["thana_name"].ToString();
                                cust.state_name = dr["state_name"].ToString();
                            }
                        }
                    }
                    _log.Info("Get Customer Details Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Customer Details Fail :" + ex.Message);
                }
            }
            return cust;
        }

        public static List<CustomerDetails> GetCustomers(string party_code)
        {
            List<CustomerDetails> cust = new List<CustomerDetails>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.state_name from exciseautomation.customer_master a inner join  exciseautomation.state_master b on a.state_code=b.state_code  order by a.customer_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            cust = new List<CustomerDetails>();
                            while (dr.Read())
                            {
                                CustomerDetails state = new CustomerDetails();
                                state.customer_id = dr["customer_id"].ToString();
                                state.cust_name = dr["cust_name"].ToString();
                                state.district_code = dr["district_name"].ToString();
                                state.thana_code = dr["thana_name"].ToString();
                                state.state_code = dr["state_name"].ToString();
                                state.party_code = dr["party_code"].ToString();
                                cust.Add(state);
                            }
                        }
                    }
                    _log.Info("Get State List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get State List Fail :" + ex.Message);
                }
            }
            return cust;
        }

        public static List<CustomerDetails> Search(string tablename, string column, string value)
        {
            List<CustomerDetails> mir = new List<CustomerDetails>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.state_name from exciseautomation.customer_master a inner join  exciseautomation.state_master b on a.state_code=b.state_code where  " + column + " Ilike '%" + value + "%' order by a.customer_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            mir = new List<CustomerDetails>();
                            while (dr.Read())
                            {
                                CustomerDetails state = new CustomerDetails();
                                state.customer_id = dr["customer_id"].ToString();
                                state.cust_name = dr["cust_name"].ToString();
                                state.district_code = dr["district_name"].ToString();
                                state.thana_code = dr["thana_name"].ToString();
                                state.state_code = dr["state_name"].ToString();
                                state.party_code = dr["party_code"].ToString();
                                mir.Add(state);

                            }
                        }
                    }
                    cn.Close();

                }
                catch (Exception ex)
                {
                    throw (ex);
                }

            }
            return mir;
        }


        public static string InsertCust(CustomerDetails cust)
        {
            string value = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    if (cust.customer_id == "" || cust.customer_id == null)
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand("Select case when Max(customer_id) is null then 0 else Max(customer_id) end from exciseautomation.Customer_master ", cn);
                        int max = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                        cmd = new NpgsqlCommand("INSERT INTO exciseautomation.Customer_master(customer_id,party_code,cust_name,cust_address,cust_mobile,cust_email,district_name,thana_name,pincode,state_code,creation_date,user_id)values('" + max + "','" + cust.party_code + "','" + cust.cust_name + "','" + cust.cust_address + "','" + cust.cust_mobile + "','" + cust.cust_email + "','" + cust.district_code + "','" + cust.thana_code + "','" + cust.pincode + "','" + cust.state_code + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + cust.user_id + "')", cn);
                        int n = cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand("update exciseautomation.Customer_master set cust_name='" + cust.cust_name + "',cust_address='"+cust.cust_address+"',cust_mobile='"+cust.cust_mobile+"',cust_email='"+cust.cust_email+"',district_name='"+cust.district_code+"',thana_name='"+cust.thana_code+"',pincode='"+cust.pincode+"',state_code='"+cust.state_code+ "',lastmodified_date='"+DateTime.Now.ToString("dd-MM-yyyy")+"',user_id='"+cust.user_id+"' where customer_id='" + cust.customer_id+"' ", cn);
                        int n = cmd.ExecuteNonQuery();
                    }
                    _log.Info("Insert State Sucesss");
                }
                catch (Exception ex)
                {
                    _log.Info("Insert State Fail :"+ ex.Message);
                    value = ex.Message;
                }
            }
            return value;
        }

        public static List<Thana_Details> GetThanaList(object userid)
        {
            List<Thana_Details> thana = new List<Thana_Details>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select distinct thana_code ,thana_name,District_code,state_code from exciseautomation.thana_master order by thana_name", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            thana = new List<Thana_Details>();
                            while (dr.Read())
                            {
                                Thana_Details ta = new Thana_Details();
                                ta.thana_code = dr["thana_code"].ToString();
                                ta.thana_name = dr["thana_name"].ToString();
                                ta.district_code = dr["District_code"].ToString();
                                ta.state_code = dr["state_code"].ToString();
                                thana.Add(ta);
                            }
                        }
                    }
                    _log.Info("Get Thana List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Thana List Fail");
                }
            }
            return thana;
        }

        public static List<Department> GetDeptList(string userid)
        {
            List<Department> department = new List<Department>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select distinct department_name ,department_code from exciseautomation.department_master order by department_name", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            department = new List<Department>();
                            while (dr.Read())
                            {
                                Department dept = new Department();
                                dept.dept_code = dr["department_code"].ToString();
                                dept.dept_name = dr["department_name"].ToString();
                                department.Add(dept);
                            }
                        }
                    }
                    _log.Info("Get Dept List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Dept List Fail");
                }
            }
            return department;
        }
        public static string InserState(State state)
        {
            string value = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.state_master(state_code, state_name, status, creation_date, lastmodified_date, user_id, country_code) VALUES('"+state.state_Code+"', '"+state.state_name+"', 'Created', '"+DateTime.Now.ToShortDateString()+ "', '" + DateTime.Now.ToShortDateString() + "', '"+state.user_id+"',  '"+state.country_name+"'); ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = "1";
                        _log.Info("Insert State Success :" + state.state_Code + "-" + state.state_name);
                    }
                    else
                    {
                        value = "0";
                        _log.Info("Insert State Fail :" + state.state_Code + "-" + state.state_name);
                    }
                   
                }
                catch (Exception ex)
                {
                    _log.Info("Insert State Fail :" + state.state_Code + "-" + state.state_name+"-"+ex.Message);
                    value = ex.Message;
                }
            }
            return value;
        }
        public static List<Division> GetDivisionList(string state_code)
        {
            List<Division> divisions = new List<Division>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    if (state_code.Length == 2)
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.State_Name from exciseautomation.division_master a inner join  exciseautomation.state_master b on a.state_code=b.state_code and a.state_code='" + state_code + "' order by a.division_master_id", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            //cmd.Parameters.AddWithValue("@UserID", userid);
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                divisions = new List<Division>();
                                while (dr.Read())
                                {
                                    Division divsion = new Division();
                                    divsion.id = dr["division_master_id"].ToString();
                                    divsion.state_Code = dr["state_Code"].ToString();
                                    divsion.Division_Code = dr["division_code"].ToString();
                                    divsion.Division_name = dr["division_name"].ToString();
                                    divsion.status = dr["status"].ToString();
                                    divsion.state_Name = dr["state_name"].ToString();
                                    divisions.Add(divsion);
                                }
                            }
                        }
                        _log.Info("Get Division List Success");
                    }
                    else
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.State_Name from exciseautomation.division_master a inner join  exciseautomation.state_master b on a.state_code=b.state_code order by division_master_id", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            //cmd.Parameters.AddWithValue("@UserID", userid);
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                divisions = new List<Division>();
                                while (dr.Read())
                                {
                                    Division divsion = new Division();
                                    divsion.id = dr["division_master_id"].ToString();
                                    divsion.state_Code = dr["state_Code"].ToString();
                                    divsion.Division_Code = dr["division_code"].ToString();
                                    divsion.Division_name = dr["division_name"].ToString();
                                    divsion.status = dr["status"].ToString();
                                    divsion.state_Name = dr["state_name"].ToString();
                                    divisions.Add(divsion);
                                }
                            }
                        }
                        _log.Info("Get Division List Success");
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Get Division List Fail :"+ex.Message);
                }
            }
            return divisions;
        }

        public static List<Division> SearchDivision(string tablename, string column, string value)
        {
            List<Division> divisions = new List<Division>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.State_Name from exciseautomation.division_master a inner join  exciseautomation.state_master b on a.state_code=b.state_code where " + column + " Ilike '%" + value + "%' order by " + column + " ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            divisions = new List<Division>();
                            while (dr.Read())
                            {
                                Division divsion = new Division();
                                divsion.id = dr["division_master_id"].ToString();
                                divsion.state_Code = dr["state_Code"].ToString();
                                divsion.Division_Code = dr["division_code"].ToString();
                                divsion.Division_name = dr["division_name"].ToString();
                                divsion.status = dr["status"].ToString();
                                divsion.state_Name = dr["state_name"].ToString();
                                divisions.Add(divsion);
                            }
                        }
                        _log.Info("Get access_type List Success");
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Get access_type List Success :" + ex.Message);
                }
            }
            return divisions;
        }
        public static bool InserDivision(Division division)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.division_master(division_code, division_name, state_code, status, creation_date, lastmodified_date, user_id) VALUES('"+division.Division_Code+"', '"+division.Division_name+"', '"+division.state_Code+"', '"+division.status+"', '"+DateTime.Now.ToString("dd/MM/yyyy")+ "', '" + DateTime.Now.ToString("dd/MM/yyyy") + "','"+division.user_id+"')", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                        _log.Info("Insert Division Success :" +division.Division_Code + "-" + division.Division_name);
                    }
                    else
                    {
                        value = false;
                        _log.Info("Insert Division Fail :" + division.Division_Code + "-" + division.Division_name);
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Insert Division Fail :" + division.Division_Code + "-" + division.Division_name+ex.Message);
                    value = false;
                }
            }
            return value;
        }
        public static bool UpdateDivision(Division division)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("update exciseautomation.division_master set division_code='"+division.Division_Code+ "',division_name='" + division.Division_name+"',state_code='"+division.state_Code+ "',status='Updated',lastmodified_date='"+DateTime.Now.ToString("dd/MM/yyyy")+ "' where division_master_id='"+division.id+"'", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                        _log.Info("Update Division Success :" + division.Division_Code + "-" + division.Division_name);
                    }
                    else
                    {
                        value = false;
                        _log.Info("Update Division Fail :" + division.Division_Code + "-" + division.Division_name);
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Update Division Fail :" + division.Division_Code + "-" + division.Division_name + ex.Message);
                    value = false;
                }
            }
            return value;
        }
        public static List<District> GetDistrictList(string division_code)
        {
            List<District> districts = new List<District>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    if (division_code.Length == 2)
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("select c.*,a.division_name,t.state_name from exciseautomation.division_master a inner join  exciseautomation.State_master t on a.state_code=t.state_code inner join exciseautomation.district_master c on  a.division_code = c.division_code and a.division_code='" + division_code + "' order by district_master_id", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            //cmd.Parameters.AddWithValue("@UserID", userid);
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                districts = new List<District>();
                                while (dr.Read())
                                {
                                    District district = new District();
                                    district.district_Code = dr["District_Code"].ToString();
                                    district.district_Name = dr["District_Name"].ToString();
                                    district.state_name = dr["State_Name"].ToString();
                                    district.state_Code = dr["state_Code"].ToString();
                                    district.division_Code = dr["division_Code"].ToString();
                                    district.division_name = dr["division_name"].ToString();
                                    district.id = dr["district_master_id"].ToString();
                                    district.tab_district_id = dr["tab_district_id"].ToString();
                                    districts.Add(district);
                                }
                            }
                        }
                    }
                    else
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("select c.*,a.division_name,t.state_name from exciseautomation.division_master a inner join  exciseautomation.State_master t on a.state_code=t.state_code inner join exciseautomation.district_master c on  a.division_code = c.division_code order by district_master_id", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            //cmd.Parameters.AddWithValue("@UserID", userid);
                            NpgsqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                districts = new List<District>();
                                while (dr.Read())
                                {
                                    District district = new District();
                                    district.district_Code = dr["District_Code"].ToString();
                                    district.district_Name = dr["District_Name"].ToString();
                                    district.state_name = dr["State_Name"].ToString();
                                    district.state_Code = dr["state_Code"].ToString();
                                    district.division_Code = dr["division_Code"].ToString();
                                    district.division_name = dr["division_name"].ToString();
                                    district.id = dr["district_master_id"].ToString();
                                    district.tab_district_id = dr["tab_district_id"].ToString();
                                    districts.Add(district);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            

            }
            return districts;
        }

        public static List<District> SearchDistrictData(string tablename, string column, string value)
        {
            List<District> districts = new List<District>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select c.*,a.division_name,t.state_name from exciseautomation.division_master a inner join  exciseautomation.State_master t on a.state_code=t.state_code inner join exciseautomation.district_master c on  a.division_code = c.division_code  where " + column + " Ilike '%" + value + "%' order by " + column + " ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            districts = new List<District>();
                            while (dr.Read())
                            {
                                District district = new District();
                                district.district_Code = dr["District_Code"].ToString();
                                district.district_Name = dr["District_Name"].ToString();
                                district.state_name = dr["State_Name"].ToString();
                                district.state_Code = dr["state_Code"].ToString();
                                district.division_Code = dr["division_Code"].ToString();
                                district.division_name = dr["division_name"].ToString();
                                district.id = dr["district_master_id"].ToString();
                                districts.Add(district);
                            }
                        }
                        _log.Info("Get access_type List Success");
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Get access_type List Success :" + ex.Message);
                }
            }
            return districts;
        }
        public static bool InserDistrict(District district)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                   
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.District_Master(division_code, state_code,District_name,district_code, status, creation_date, lastmodified_date, user_id,tab_district_id) VALUES('" + district.division_Code + "', '" + district.state_Code + "','"+district.district_Name+"','"+district.district_Code+"', '" + district.status + "', '" + DateTime.Now.ToString("dd/MM/yyyy") + "', '" + DateTime.Now.ToString("dd/MM/yyyy") + "','" + district.user_id + "','"+district.tab_district_id+"')", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                        _log.Info("Insert District Success :" + district.district_Code + "-" + district.district_Name);
                    }
                    else
                    {
                        value = false;
                        _log.Info("Insert District Fail :" + district.district_Code + "-" + district.district_Name);
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Insert District Fail :" + district.district_Code + "-" + district.district_Name+"-"+ex.Message);
                    value = false;
                }
            }
            return value;
        }
        public static string UpdateDistrict(District district)
        {
            string value = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                 
                    NpgsqlCommand cmd = new NpgsqlCommand("update exciseautomation.district_master set division_code='" + district.division_Code + "',district_name='" + district.district_Name + "',state_code='" + district.state_Code + "',status='Updated',lastmodified_date='" + DateTime.Now.ToString("dd/MM/yyyy") + "', tab_district_id='"+district.tab_district_id+"' where district_master_id='" + district.id + "'", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = "1";
                        _log.Info("Update District Success :" + district.district_Code + "-" + district.district_Name);
                    }
                    else
                    {
                        value = "0";
                        _log.Info("Update District Fail :" + district.district_Code + "-" + district.district_Name);
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Update District Fail :" + district.district_Code + "-" + district.district_Name + "-" + ex.Message);
                    value = ex.Message;
                }
            }
            return value;
        }
        public static int GetExistsData(string tablename,string column,string value)
        {
            int value1 = 0;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd;
                    if (tablename == "VAT_Master")
                    {
                        string[] values = value.Split('_');
                        cmd = new NpgsqlCommand("select count(1) from exciseautomation." + tablename + " where " + column + "='" + values[0] + "' and party_code='" + values[1] + "'", cn);
                    }
                   else if (tablename== "sugarcanepurchase")
                    {
                        string[] values = value.Split('_');
                      cmd = new NpgsqlCommand("select count(1) from exciseautomation." + tablename + " where entrydate='" + values[0] + "' and party_code='"+values[1]+"'", cn);
                    }
                  else  if (tablename == "dailymolassesproduction")
                    {
                        string[] values = value.Split('_');
                        cmd = new NpgsqlCommand("select count(1) from exciseautomation." + tablename + " where entrydate='" + values[0] + "' and party_code='" + values[1] + "'", cn);
                    }
                    else
                    cmd = new NpgsqlCommand("select count(1) from exciseautomation."+tablename+" where "+column+"='"+value+"'", cn);
                    string re = cmd.ExecuteScalar().ToString();
                    if (re == "1")
                    {
                        value1 =1;
                        _log.Info("Get Existing data Success :" + tablename);
                    }
                    else
                    {
                        if(re!="")
                        value1 =Convert.ToInt32(re);
                        _log.Info("Get Existing data Fail :" + tablename);
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Get Existing data Fail :" +tablename + "-" + ex.Message);
                    value1 =0;
                }
            }
            return value1;
        }
        public static bool InserRoleLevel(RoleLevel rolelevel)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    int max = Convert.ToInt32(DL_Org_List.GetMaxID("role_level_master").ToString()) + 1;
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.role_level_master( role_level_code, role_level_name, role_level_desc,status, creation_date, lastmodified_date, user_id) VALUES('"+max+"','"+rolelevel.levelname+"', '"+rolelevel.description+"', 'Created', '"+DateTime.Now.ToString("dd/MM/yyyy")+ "', '" + DateTime.Now.ToString("dd/MM/yyyy") + "', '"+rolelevel.user_id+"')", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                        _log.Info("Insert Role Level Success :" + rolelevel.role_level_code + "-" +rolelevel.levelname);
                    }
                    else
                    {
                        value = false;
                        _log.Info("Insert Role Level Fail :" + rolelevel.role_level_code + "-" + rolelevel.levelname);
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Insert Role Level Fail :" + rolelevel.role_level_code + "-" + rolelevel.levelname+"-"+ex.Message);
                    value = false;
                }
            }
            return value;
        }
        public static bool UpdateRoleLevel(RoleLevel rolelevel)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("update exciseautomation.role_level_master set role_level_name='"+rolelevel.levelname+"',role_level_desc='"+rolelevel.description+"',  status='Updated',lastmodified_date='" + DateTime.Now.ToString("dd/MM/yyyy") + "' where role_level_code='" + rolelevel.role_level_code + "'", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                        _log.Info("Update Role Level Success :" + rolelevel.role_level_code + "-" + rolelevel.levelname);
                    }
                    else
                    {
                        value = false;
                        _log.Info("Update Role Level Fail :" + rolelevel.role_level_code + "-" + rolelevel.levelname);
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Update Role Level Fail :" + rolelevel.role_level_code + "-" + rolelevel.levelname + "-" + ex.Message);
                    value = false;
                }
            }
            return value;
        }
        public static List<RoleLevel> GetRoleLevels(string userid)
        {
            List<RoleLevel> roleLevel = new List<RoleLevel>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.role_level_master order by Role_level_Code", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            roleLevel = new List<RoleLevel>();
                            while (dr.Read())
                            {
                                RoleLevel level = new RoleLevel();
                                level.role_level_code = dr["Role_level_Code"].ToString();
                                level.levelname= dr["Role_level_Name"].ToString();
                                level.description = dr["role_level_desc"].ToString();
                                level.status = dr["status"].ToString();
                                roleLevel.Add(level);
                            }
                        }
                        _log.Info("Get Role Level List Success");
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Get Role Level List Success :"+ex.Message);
                }
            }
            return roleLevel;
        }

        public static List<RoleLevel> SearchroleLevel(string tablename, string column, string value)
        {
            List<RoleLevel> roleLevel = new List<RoleLevel>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.role_level_master  where " + column + " Ilike '%" + value + "%' order by " + column + " ", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            roleLevel = new List<RoleLevel>();
                            while (dr.Read())
                            {
                                RoleLevel level = new RoleLevel();
                                level.role_level_code = dr["Role_level_Code"].ToString();
                                level.levelname = dr["Role_level_Name"].ToString();
                                level.description = dr["role_level_desc"].ToString();
                                level.status = dr["status"].ToString();
                                roleLevel.Add(level);
                            }
                        }
                        _log.Info("Get Role Level List Success");
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Get Role Level List Success :" + ex.Message);
                }
            }
            return roleLevel;
        }

        public static List<AccessType> GetAccessTypeList(string userid)
        {
            List<AccessType> accesstypes = new List<AccessType>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.access_type_master order by access_type_code", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            accesstypes = new List<AccessType>();
                            while (dr.Read())
                            {
                                AccessType type = new AccessType();
                                type.access_type_code = dr["access_type_code"].ToString();
                                type.access_type_name = dr["access_type_name"].ToString();
                                type.access_type_desc = dr["access_type_desc"].ToString();
                                type.status = dr["status"].ToString();
                                accesstypes.Add(type);
                            }
                        }
                        _log.Info("Get access_type List Success");
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Get access_type List Success :" + ex.Message);
                }
            }
            return accesstypes;
        }
        public static List<AccessType> SearchExistsData(string tablename, string column, string value)
        {
            List<AccessType> accesstypes = new List<AccessType>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation." + tablename + "  where " + column + " Ilike '%" + value + "%' order by access_type_code", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            accesstypes = new List<AccessType>();
                            while (dr.Read())
                            {
                                AccessType type = new AccessType();
                                type.access_type_code = dr["access_type_code"].ToString();
                                type.access_type_name = dr["access_type_name"].ToString();
                                type.access_type_desc = dr["access_type_desc"].ToString();
                                type.status = dr["status"].ToString();
                                accesstypes.Add(type);
                            }
                        }
                        _log.Info("Get access_type List Success");
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Get access_type List Success :" + ex.Message);
                }
            }
            return accesstypes;
        }

        public static bool InserAccessType(AccessType accesstype)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    int max = Convert.ToInt32(DL_Org_List.GetMaxID("access_type_master").ToString()) + 1;
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.access_type_master( access_type_code, access_type_name, access_type_desc, status,creation_date, lastmodified_date,  user_id) VALUES('" +accesstype.access_type_code+"','"+accesstype.access_type_name+"','"+accesstype.access_type_desc+ "', 'Created', '" + DateTime.Now.ToString("dd/MM/yyyy") + "', '" + DateTime.Now.ToString("dd/MM/yyyy") + "', '" + accesstype.user_id + "') ", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                        _log.Info("Insert access_type_master Success :" + accesstype.access_type_code + "-" + accesstype.access_type_name);
                    }
                    else
                    {
                        value = false;
                        _log.Info("Insert access_type_master Fail :" + accesstype.access_type_code + "-" + accesstype.access_type_name);
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Insert access_type_master Fail :" + accesstype.access_type_code + "-" + accesstype.access_type_name+"-"+ex.Message);
                    value = false;
                }
            }
            return value;
        }
        public static bool UpdateAccessType(AccessType accesstype)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("update exciseautomation.access_type_master set access_type_name='" + accesstype.access_type_name + "',access_type_desc='" + accesstype.access_type_desc + "',  status='Updated',lastmodified_date='" + DateTime.Now.ToString("dd/MM/yyyy") + "' where access_type_code='" + accesstype.access_type_code + "'", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                        _log.Info("Update access_type_master Success :" + accesstype.access_type_code + "-" + accesstype.access_type_name);
                    }
                    else
                    {
                        value = false;
                        _log.Info("Update access_type_master Fail :" + accesstype.access_type_code + "-" + accesstype.access_type_name);
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Update access_type_master Fail :" + accesstype.access_type_code + "-" + accesstype.access_type_name + "-" + ex.Message);
                    value = false;
                }
            }
            return value;
        }
        public static List<RoleMaster> GetRoleMasterList(string userid)
        {
            List<RoleMaster> rolemaster = new List<RoleMaster>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.role_level_name,c.access_type_name,d.org_name from exciseautomation.role_master a inner join exciseautomation.role_level_master b on a.role_level_code=b.role_level_code inner join exciseautomation.access_type_master c on a.access_type_code = c.access_type_code inner join exciseautomation.org_master d on a.org_id=d.org_id order by a.role_master_id", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            rolemaster = new List<RoleMaster>();
                            while (dr.Read())
                            {
                                RoleMaster role = new RoleMaster();
                                role.id =Convert.ToInt32( dr["role_master_id"].ToString());
                                role.rolecode = dr["role_name_code"].ToString();
                                role.rolename = dr["role_name"].ToString();
                                role.accestype = dr["access_type_code"].ToString();
                                role.organization = dr["org_id"].ToString();
                                role.rolelevel = dr["role_level_code"].ToString();
                                role.sanction_strength = dr["sanction_strength"].ToString();
                                role.nextrole= dr["next_role_name"].ToString();
                                role.nextroleCode = dr["next_role_name_code"].ToString();
                                role.status = dr["status"].ToString();
                                role.organization_name = dr["org_name"].ToString();
                                role.rolelevel_name = dr["role_level_name"].ToString();
                                role.accestype_name = dr["access_type_name"].ToString();
                                rolemaster.Add(role);
                            }
                        }
                        _log.Info("Get view_Role_master List Success");
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Get view_Role_master List Success :" + ex.Message);
                }
            }
            return rolemaster;
        }
        public static List<RoleMaster> SearchRoleMaster(string tablename, string column, string value)
        {
            List<RoleMaster> rolemaster = new List<RoleMaster>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {

                    using (NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.role_level_name,c.access_type_name,d.org_name from exciseautomation.role_master a inner join exciseautomation.role_level_master b on a.role_level_code=b.role_level_code inner join exciseautomation.access_type_master c on a.access_type_code = c.access_type_code inner join exciseautomation.org_master d on a.org_id=d.org_id where " + column + " Ilike '%" + value + "%' order by a.role_name", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        //cmd.Parameters.AddWithValue("@UserID", userid);
                        NpgsqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            rolemaster = new List<RoleMaster>();
                            while (dr.Read())
                            {
                                RoleMaster role = new RoleMaster();
                                role.id = Convert.ToInt32(dr["role_master_id"].ToString());
                                role.rolecode = dr["role_name_code"].ToString();
                                role.rolename = dr["role_name"].ToString();
                                role.accestype = dr["access_type_code"].ToString();
                                role.organization = dr["org_id"].ToString();
                                role.rolelevel = dr["role_level_code"].ToString();
                                role.sanction_strength = dr["sanction_strength"].ToString();
                                role.nextrole = dr["next_role_name"].ToString();
                                role.nextroleCode = dr["next_role_name_code"].ToString();
                                role.status = dr["status"].ToString();
                                role.organization_name = dr["org_name"].ToString();
                                role.rolelevel_name = dr["role_level_name"].ToString();
                                role.accestype_name = dr["access_type_name"].ToString();
                                rolemaster.Add(role);
                            }
                        }
                        _log.Info("Get view_Role_master List Success");
                    }
                }
                catch (Exception ex)
                {
                    _log.Info("Get view_Role_master List Success :" + ex.Message);
                }
            }
            return rolemaster;
        }
        public static bool UpdateRoleMaster(RoleMaster rolemaster)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("update exciseautomation.role_master set role_name='"+rolemaster.rolename+ "',role_level_code='"+rolemaster.rolelevel+ "',access_type_code='"+rolemaster.accestype+ "',next_role_name_code='"+rolemaster.nextroleCode+ "',next_role_name='"+rolemaster.nextrole+ "',org_id='"+rolemaster.organization+"',status='"+rolemaster.status+"',user_id='"+rolemaster.user_id+ "',sanction_strength='"+rolemaster.sanction_strength+ "',lastmodified_date='"+DateTime.Now.ToString("dd/MM/yyyy")+ "' where  role_master_id='"+rolemaster.id+"'", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                        _log.Info("Update role_master Success :" + rolemaster.rolecode + "-" + rolemaster.rolename);
                    }
                    else
                    {
                        value = false;
                        _log.Info("Update role_master Fail :" + rolemaster.rolecode + "-" + rolemaster.rolename);
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Update role_master Fail :" + rolemaster.rolecode + "-" + rolemaster.rolename+"-"+ex.Message);
                    value = false;
                }
            }
            return value;
        }
        public static bool InsertRoleMaster(RoleMaster rolemaster)
        {
            bool value = false;
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    int max = Convert.ToInt32(DL_Org_List.GetMaxID("role_master").ToString()) + 1;
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO exciseautomation.role_master(role_name_code, role_name, role_level_code, access_type_code, next_role_name_code, next_role_name, org_id, status, user_id, sanction_strength, creation_date, lastmodified_date) VALUES('"+max+"','"+rolemaster.rolename+"','"+rolemaster.rolelevel+"', '"+rolemaster.accestype+"', '"+ rolemaster.nextroleCode + "', '"+rolemaster.nextrole+"','"+rolemaster.organization+"','Created', '"+rolemaster.user_id+"', '"+rolemaster.sanction_strength+"', '"+DateTime.Now.ToString("dd/MM/yyyy")+ "', '" + DateTime.Now.ToString("dd/MM/yyyy") + "')", cn);
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        value = true;
                        _log.Info("Insert role_master Success :" + rolemaster.rolecode + "-" + rolemaster.rolename);
                    }
                    else
                    {
                        value = false;
                        _log.Info("Insert role_master Fail :" + rolemaster.rolecode + "-" + rolemaster.rolename);
                    }

                }
                catch (Exception ex)
                {
                    _log.Info("Insert role_master Fail :" + rolemaster.rolecode + "-" + rolemaster.rolename + "-" + ex.Message);
                    value = false;
                }
            }
            return value;
        }
    }
}
