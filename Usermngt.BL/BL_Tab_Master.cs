using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
   public class BL_Tab_Master
    {
        public static string Insert(Tab_Master tab)
        {
            return DL_Tab_Master.Insert(tab);

        }
        public static List<Tab_Master> SearchTab(string tablename, string column, string value)
        {
            return DL_Tab_Master.SearchTab(tablename, column, value);
        }
        public static string Update(Tab_Master tab)
        {
            return DL_Tab_Master.Update(tab);
        }

        public static List<Tab_Master> GetList()
        {
            return DL_Tab_Master.GetList();
        }
    }
}
