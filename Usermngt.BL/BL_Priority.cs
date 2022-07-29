using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
  public  class BL_Priority
    {
        public static bool Insert(Priority priority)
        {
            return DL_Priority.Insert(priority);
        }

        public static bool Update(Priority priority)
        {
            return DL_Priority.Update(priority);
        }
        public static List<Priority> GetList()
        {
            return DL_Priority.GetList();
        }
        public static Priority Getreslovetime(string Code)
        {
            return DL_Priority.Getreslovetime(Code);
        }

        }
}
