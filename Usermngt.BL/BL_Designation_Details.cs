using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_Designation_Details
    {
        public static string InsertDType(Designation_Details des)
        {
            return DL_Designation_Details.InsertDtype(des);
        }
        public static List<Designation_Details> GetDListdesignationtype()
        {
            return DL_Designation_Details.GetDListdesignationtype();
        }
        public static List<Designation_Details> GetDtypeList()
        {
            return DL_Designation_Details.GetDtypeList();
        }
        public static List<Designation_Details> SearchDesignation(string tablename, string column, string value)
        {
            return DL_Designation_Details.SearchDesignation(tablename, column, value);
        }
        public static string InsertD(Designation_Details des)
        {
            return DL_Designation_Details.InsertD(des);
        }
        public static List<Designation_Details> SearchDesignationtype(string tablename, string column, string value)
        {
            return DL_Designation_Details.SearchDesignationtype(tablename, column, value);
        }

        public static List<Designation_Details> GetDList()
        {
            return DL_Designation_Details.GetDList();
        }
    }
}
