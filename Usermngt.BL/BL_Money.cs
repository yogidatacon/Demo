using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
  public  class BL_Money
    {
        public static string Update(cm_seiz_Money Money)
        {
            return DL_Money.Update(Money);
        }
        public static string Insert(cm_seiz_Money Money)
        {
            return DL_Money.Insert(Money);
        }

        public static List<cm_seiz_Money> GetList(string userid)
        {
            return DL_Money.GetList(userid);
        }

        public static cm_seiz_Money GetDetails(string userid, int moneyid)
        {
            return DL_Money.GetDetails(userid, moneyid);
        }
    }
}
