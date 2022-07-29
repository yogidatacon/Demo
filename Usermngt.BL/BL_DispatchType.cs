using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_DispatchType
    {
        public static List<DispatchType> GetList()
        {
            return DL_DispatchType.GetList();
        }

        public static string Insert(DispatchType dtype)
        {
            return DL_DispatchType.Insert(dtype);
        }
        public static List<DispatchType> SearchDispatchType(string tablename, string column, string value)
        {
            return DL_DispatchType.SearchDispatchType(tablename, column, value);
        }
        public static string Update(DispatchType dtype)
        {
            return DL_DispatchType.Update(dtype);
        }

        //public static List<DocumentFormats> GetReportList()
        //{
        //    //return DL_DispatchType.GetReportList();
        //}

        public static string InsertDoc(DocumentFormats doc)
        {
            return DL_DispatchType.InsertDoc(doc);
        }

        public static List<DocumentFormats> GetDocReportList()
        {
            return DL_DispatchType.GetDocReportList();
        }
        public static List<DocumentFormats> Search(string tablename, string column, string value)
        {
            return DL_DispatchType.Search(tablename, column, value);
        }
        public static string GetDOC(string party_code, string report_type)
        {
            return DL_DispatchType.GetDoc(party_code, report_type);
        }
    }
}
