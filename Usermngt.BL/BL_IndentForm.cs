using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_IndentForm
    {
        public static string Insert(Indent_Form indent)
        {
            return DL_IndentForm.Insert(indent);
        }

        public static string Update(Indent_Form indent)
        {
            return DL_IndentForm.Update(indent);
        }
        public static List<Indent_Form> GetList()
        {
            return DL_IndentForm.GetList();
        }
        public static List<Indent_Form> Search(string tablename, string column, string value)
        {
            return DL_IndentForm.Search(tablename, column, value);
        }
        public static Indent_Form GetDetails(string id,string financial_year)
        {
            return DL_IndentForm.GetDetails(id,financial_year);
        }

        public static string GetValues(string v)
        {
            return DL_IndentForm.GetValues(v);
        }
    }
}
