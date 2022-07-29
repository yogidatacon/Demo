using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_Party_Type_Master
    {
        public static string Insert(Party_Type_Master partytype)
        {
            return DL_Party_Type_Master.Insert(partytype);
        }
        public static string Update(Party_Type_Master partytype)
        {
            return DL_Party_Type_Master.Update(partytype);
        }
        public static List<Party_Type_Master> SearchPartyType(string tablename, string column, string value)
        {
            return DL_Party_Type_Master.SearchPartyType(tablename, column, value);
        }
        public static List<Department> SearchDept(string tablename, string column, string value)
        {
            return DL_Party_Type_Master.SearchDept(tablename, column, value);
        }
        public static List<Party_Type_Master> GetList()
        {
            return DL_Party_Type_Master.GetList();
        }
    }
}
