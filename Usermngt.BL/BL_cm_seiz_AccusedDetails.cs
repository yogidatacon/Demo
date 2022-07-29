using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_cm_seiz_AccusedDetails
    {
        public static string Insert(cm_seiz_AccusedDetails ad)
        {
            return DL_cm_seiz_AccusedDetails.Insert(ad);
        }

        public static string Update(cm_seiz_AccusedDetails ad)
        {
            return DL_cm_seiz_AccusedDetails.Update(ad);
        }

        public static List<cm_seiz_AccusedDetails> GetDetails(string adid)
        {
            return DL_cm_seiz_AccusedDetails.GetDetails(adid);
        }

        public static List<cm_seiz_AccusedDetails> GetSearchDetails(string qery)
        {
            return DL_cm_seiz_AccusedDetails.GetSearchDetails(qery);
        }

        public static DataTable GetSearchDetailsID(string id)
        {
            return DL_cm_seiz_AccusedDetails.GetSearchDetailsID(id);
        }
    }
}
