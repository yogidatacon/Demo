using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;


namespace Usermngt.BL
{
  public  class BL_Module_Master
    {
        public static string Insert(Module_Master module)
        {
            return DL_Module_Master.Insert(module);

        }
        public static List<Module_Master> SearchModule(string tablename, string column, string value)
        {
            return DL_Module_Master.SearchModule(tablename, column, value);
        }
        public static string Update(Module_Master module)
        {
            return DL_Module_Master.Update(module);
        }

        public static List<Module_Master> GetList()
        {
            return DL_Module_Master.GetList();
        }
    }
}
