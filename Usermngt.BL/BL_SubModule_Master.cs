using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
   public class BL_SubModule_Master
    {
        public static string Insert(SubModule_Master submodule)
        {
            return DL_SubModule_Master.Insert(submodule);

        }
        public static List<SubModule_Master> SearchSubModule(string tablename, string column, string value)
        {
            return DL_SubModule_Master.SearchSubModule(tablename, column, value);
        }
        public static string Update(SubModule_Master submodule)
        {
            return DL_SubModule_Master.Update(submodule);
        }

        public static List<SubModule_Master> GetList()
        {
            return DL_SubModule_Master.GetList();
        }
    }
}
