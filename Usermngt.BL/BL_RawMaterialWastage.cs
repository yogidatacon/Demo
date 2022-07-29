using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.Entities;
using Usermngt.DAL;

namespace Usermngt.BL
{
    public class BL_RawMaterialWastage
    {

        public static string Insert(RawmaterialWastage dispatch)
        {
            return DL_RawmaterialWastage.Insert(dispatch);
        }
        public static string Update(RawmaterialWastage dispatch)
        {
            return DL_RawmaterialWastage.Update(dispatch);
        }
        public static string AdminUpdate(RawmaterialWastage dispatch)
        {
            return DL_RawmaterialWastage.AdminUpdate(dispatch);
        }
        public static int GetExistsData(string date, string value,string year)
        {
            return DL_RawmaterialWastage.GetExistsData(date, value,year);
        }
        public static List<RawmaterialWastage> GetDispatch()
        {
            return DL_RawmaterialWastage.GetDispatch();
        }
        public static List<RawmaterialWastage> Search(string tablename, string column, string value)
        {
            return DL_RawmaterialWastage.Search(tablename, column, value);
        }
        public static string Approve(RawmaterialWastage DDC)
        {
            return DL_RawmaterialWastage.Approve(DDC);
        }

        public static RawmaterialWastage GetDetails(string party_code, int id,string financial_year)
        {
            return DL_RawmaterialWastage.GetDetails(party_code, id,financial_year);
        }
        }
}
