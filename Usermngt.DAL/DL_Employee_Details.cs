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
    public class DL_Employee_Details
    {
        static NLog.Logger _log = NLog.LogManager.GetLogger("PGCalcService");
        public static Employee_Details GetDetails(string noc_id)
        {
           Employee_Details emp = new Employee_Details();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.employee_master   order by employee_master_id", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        emp.employee_master_id = dr["employee_master_id"].ToString();
                        emp.emp_code = dr["emp_code"].ToString();
                        emp.emp_name = dr["emp_name"].ToString();
                        emp.dob = dr["dob"].ToString().Substring(0, 10).Replace("/", "-"); 
                        emp.age = dr["age"].ToString();
                        emp.present_address = dr["present_address"].ToString();
                        emp.permanent_address = dr["permanent_address"].ToString();
                        emp.state_code = dr["state_code"].ToString();
                        emp.division_code = dr["division_code"].ToString();
                        emp.district_code = dr["district_code"].ToString();
                        emp.bank = dr["Bank_code"].ToString();
                        emp.branch = dr["branch_name"].ToString();
                        emp.ifsc = dr["ifsc_code"].ToString();
                        emp.account_no = dr["bank_accountno"].ToString();
                        emp.pancard = dr["pancard"].ToString();
                        emp.aadharcard = dr["aadharcard"].ToString();
                        emp.mobile = dr["mobile"].ToString();
                        emp.email_id = dr["email_id"].ToString();
                        emp.designation_code = dr["designation_code"].ToString();
                        emp.department_code = dr["department_code"].ToString();
                       // emp.pincode = dr["pincode"].ToString();
                        emp.doj = dr["doj"].ToString().Substring(0, 10).Replace("/", "-");
                        emp.start_date = dr["start_date"].ToString().Substring(0, 10).Replace("/", "-"); 
                        emp.org_id = dr["org_id"].ToString();
                        emp.record_status = dr["record_status"].ToString();

                    }
                    cn.Close();
                    _log.Info("Get EMP Details Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get EMP Details Fail:" + ex.Message);

                }
            }
            return emp;
        }

        public static List<Bank_Master> GetBankList()
        {
            List<Bank_Master> banks = new List<Bank_Master>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select * from exciseautomation.bank_master order by bank_name", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    banks = new List<Bank_Master>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Bank_Master bank = new Bank_Master();
                        bank.bank_code = dr["bank_code"].ToString();
                        bank.bank_name = dr["bank_name"].ToString();
                        banks.Add(bank);
                    }
                    cn.Close();
                    _log.Info("Get Emp List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Emp List Fail:" + ex.Message);

                }
            }
            return banks;
        }

        public static string Insert(Employee_Details emp)
        {
            string value = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("Select case when max(employee_master_id) is null then 0 else max(employee_master_id) end from  exciseautomation.employee_master", cn);
                    int maxid = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                    StringBuilder str = new StringBuilder();
                    str.Append("INSERT INTO exciseautomation.employee_master(employee_master_id, emp_code, emp_name,   present_address, permanent_address, dob, age,");
                    str.Append("pancard, aadharcard, mobile, email_id, doj, start_date,  bank_code,branch_name, bank_accountno, ifsc_code,  designation_code, department_code,");
                    str.Append("district_code, division_code, state_code, org_id, creation_date,  user_id, record_status)Values(");
                    str.Append("'" + maxid + "','" + emp.emp_code + "','" + emp.emp_name + "','" + emp.present_address + "','" + emp.permanent_address + "','" + emp.dob + "','" + emp.age + "',");
                    str.Append("'" + emp.pancard + "','" + emp.aadharcard + "','" + emp.mobile + "','" + emp.email_id + "','" + emp.doj + "','" + emp.start_date + "','" + emp.bank + "','" + emp.branch + "','" + emp.account_no + "','" + emp.ifsc + "','" + emp.designation_code + "','" + emp.department_code + "',");
                    str.Append("'" + emp.district_code + "','" + emp.division_code + "','" + emp.state_code + "','" + emp.org_id + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + emp.user_id + "','" + emp.record_status + "')");
                    cmd = new NpgsqlCommand(str.ToString(), cn);
                    cmd.ExecuteNonQuery();
                    value ="0";
                    _log.Info("Insert EMP Sucess:" + maxid);
                }
                catch (Exception ex)
                {
                    value = ex.Message;
                    _log.Info("Insert EMP Fail:" + ex.Message);
                }
            }
            return value;
        }

        public static List<Employee_Details> GetList()
        {
            List<Employee_Details> emp = new List<Employee_Details>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.Designation_name,c.Department_name from exciseautomation.employee_master a inner join exciseautomation.designation_master b on a.designation_code=b.designation_code inner join exciseautomation.department_master c on a.department_code=c.department_code order by employee_master_id", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    emp = new List<Employee_Details>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Employee_Details em =new Employee_Details();
                        em.employee_master_id = dr["employee_master_id"].ToString();
                        em.emp_code = dr["emp_code"].ToString();
                        em.emp_name= dr["emp_name"].ToString();
                        em.designation_code = dr["designation_name"].ToString();
                        em.department_code = dr["department_name"].ToString();
                        emp.Add(em);
                    }
                    cn.Close();
                    _log.Info("Get Emp List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Emp List Fail:" + ex.Message);

                }
            }
            return emp;
        }

        public static List<Employee_Details> SearchEmployee(string tablename, string column, string value)
        {
            List<Employee_Details> emp = new List<Employee_Details>();
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("select a.*,b.Designation_name,c.Department_name from exciseautomation.employee_master a inner join exciseautomation.designation_master b on a.designation_code=b.designation_code inner join exciseautomation.department_master c on a.department_code=c.department_code where " + column + " Ilike '" + value + "%' order by emp_name", cn);
                    NpgsqlDataReader dr1 = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    dr1.Close();
                    emp = new List<Employee_Details>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Employee_Details em = new Employee_Details();
                        em.employee_master_id = dr["employee_master_id"].ToString();
                        em.emp_code = dr["emp_code"].ToString();
                        em.emp_name = dr["emp_name"].ToString();
                        em.designation_code = dr["designation_name"].ToString();
                        em.department_code = dr["department_name"].ToString();
                        emp.Add(em);
                    }
                    cn.Close();
                    _log.Info("Get Emp List Success");
                }
                catch (Exception ex)
                {
                    _log.Info("Get Emp List Fail:" + ex.Message);

                }
            }
            return emp;
        }
        public static string Update(Employee_Details emp)
        {
            string value = "";
            using (NpgsqlConnection cn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CASEMGMT"].ToString()))
            {
                cn.Open();
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("update exciseautomation.employee_master set emp_code='" + emp.emp_code + "', emp_name='" + emp.emp_name + "',   present_address='" + emp.present_address + "', permanent_address='" + emp.permanent_address + "', dob='" + emp.dob + "', age='" + emp.age + "',");
                    str.Append("pancard='" + emp.pancard + "', aadharcard='" + emp.aadharcard + "', mobile='" + emp.mobile + "', email_id='" + emp.email_id + "', doj='" + emp.doj + "', start_date='" + emp.start_date + "', bank_code='" + emp.bank + "',Branch_name='" + emp.branch + "',bank_accountno='" + emp.account_no + "' , ifsc_code='" + emp.ifsc + "', designation_code='" + emp.designation_code + "',department_code='" + emp.department_code + "',");
                    str.Append("district_code='" + emp.district_code + "', division_code='" + emp.division_code + "', state_code='" + emp.state_code + "', org_id='" + emp.org_id + "', creation_date='" + DateTime.Now.ToString("dd-MM-yyyy") + "',  user_id='" + emp.user_id + "', record_status='" + emp.record_status + "' where employee_master_id='" + emp.employee_master_id+"'");
                    NpgsqlCommand cmd = new NpgsqlCommand(str.ToString(), cn);
                    cmd.ExecuteNonQuery();
                     value = "0";
                    _log.Info("Update EMP Sucess:" + emp.employee_master_id);
                }
                catch (Exception ex)
                {
                    value = ex.Message;
                    _log.Info("Update EMP Fail:" + ex.Message);
                }
            }
            return value;
        }
    }
}
