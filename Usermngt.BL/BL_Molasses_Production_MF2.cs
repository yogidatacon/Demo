using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_Molasses_Production_MF2
    {
        public static string Insert(Molasses_Production_MF2 mf2)
        {
            return DL_Molasses_Production_MF2.Insert(mf2);
        }

        public static string Update(Molasses_Production_MF2 mf2)
        {
            return DL_Molasses_Production_MF2.Update(mf2);
        }

        public static List<Molasses_Production_MF2> GetList()
        {
            return DL_Molasses_Production_MF2.GetList();
        }
        public static List<Molasses_Production_MF2> Search(string tablename, string column, string value)
        {
            return DL_Molasses_Production_MF2.Search(tablename, column, value);
        }

        public static Molasses_Production_MF2 GetDetails(string mpid,string financial_year)
        {
            return DL_Molasses_Production_MF2.GetDetails(mpid,financial_year);
        }

        public static string GetValues(string values)
        {
            return DL_Molasses_Production_MF2.GetValues(values);
        }
    }
}
