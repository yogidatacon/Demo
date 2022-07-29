using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_CM_Common
    {
        public static UserDetails CheckUser(string userid)
        {
            return DL_Common.CheckUser(userid);
        }

        public static cm_seiz_FIR GetPRFIRNo(string seizureno)
        {
            return DL_Common.GetPRFIRNo(seizureno);
        }

        public static string GetDate(string tablename, string ldate)
        {
            return DL_Common.GetDate(tablename,ldate);
        }
    }
}
