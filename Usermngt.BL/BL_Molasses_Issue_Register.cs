using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_Molasses_Issue_Register
    {
      
        public static List<Molasses_Issue_Register> GetOpeningBalanceList()
        {
            return DL_Molasses_Issue_Register.GetOpeningBalanceList();
        }
        public static List<Molasses_Issue_Register> GetOpeningBalanceListMIR( string Vat,string financial_year)
        {
            return DL_Molasses_Issue_Register.GetOpeningBalanceListMIR(Vat,financial_year);
        }
        public static string Update(Molasses_Issue_Register mir1)
        {
            return DL_Molasses_Issue_Register.Update(mir1);
        }

        public static string Insert(Molasses_Issue_Register mir1)
        {
            return DL_Molasses_Issue_Register.Insert(mir1);
        }

        public static List<Molasses_Issue_Register> GetList()
        {
            return DL_Molasses_Issue_Register.GetList();
        }
        public static List<Molasses_Issue_Register> Search(string tablename, string column, string value)
        {
            return DL_Molasses_Issue_Register.Search(tablename, column, value);
        }
        public static Molasses_Issue_Register GetDetails(string mirid,string financial_year)
        {
            return DL_Molasses_Issue_Register.GetDetails(mirid,financial_year);
        }

        public static string Approve(Molasses_Issue_Register mir)
        {
            return DL_Molasses_Issue_Register.Approve(mir);
        }

        //public static List<Molasses_Issue_Register> GetPassDetails(string party_code)
        //{
        //    return DL_Molasses_Issue_Register.GetPassDetails(party_code);
        //}

        public static string GetExistingDetails(string values)
        {
            return DL_Molasses_Issue_Register.GetExistingDetails(values);
        }
    }
}
