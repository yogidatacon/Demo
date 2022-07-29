using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_All_Approvals
    {
        public static List<All_Approvals> GetApprovals(string userid, string sugarcanepurchase_id,string tranction_type)
        {
            return DL_All_Approvals.GetApprovals(userid, sugarcanepurchase_id, tranction_type);
        }

       
    }
}
