using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
  public  class BL_Employee_Details
    {
        public static string Insert(Employee_Details emp)
        {
            return DL_Employee_Details.Insert(emp);
        }
        public static List<Employee_Details> SearchEmployee(string tablename, string column, string value)
        {
            return DL_Employee_Details.SearchEmployee(tablename, column, value);
        }
        public static string Update(Employee_Details emp)
        {
            return DL_Employee_Details.Update(emp);
        }

        public static List<Employee_Details> GetList()
        {
            return DL_Employee_Details.GetList();
        }

        public static Employee_Details GetDetails(string emp_id)
        {
            return DL_Employee_Details.GetDetails(emp_id);
        }

        public static List<Bank_Master> GetBankList()
        {
            return DL_Employee_Details.GetBankList();
        }
    }
}
