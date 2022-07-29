using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_MF3_Details
    {
        public static List<MF3_Details> GetList()
        {
            return DL_MF3_Details.GetList();
        }
        public static List<MF3_Details> Search(string tablename, string column, string value)
        {
            return DL_MF3_Details.Search(tablename, column, value);
        }
        public static string Insert(MF3_Details mf3)
        {
            return DL_MF3_Details.Insert(mf3);
        }

        public static string Update(MF3_Details mf3)
        {
            return DL_MF3_Details.Update(mf3);
        }

        public static MF3_Details GetDetails(string mpid,string financial_year)
        {
            return DL_MF3_Details.GetValues(mpid,financial_year);
        }

        public static string GetDupValues(string v)
        {
            return DL_MF3_Details.GetDupValues(v);
        }
    }
}
