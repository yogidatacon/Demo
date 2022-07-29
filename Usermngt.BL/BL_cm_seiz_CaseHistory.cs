using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_cm_seiz_CaseHistory
    {
        public static List<cm_seiz_CaseHistory> GetList(string seizureNo)
        {
            return DL_cm_seiz_CaseHistory.GetList(seizureNo);
        }

        public static cm_seiz_CaseHistory GetDetails(string tableId)
        {
            return DL_cm_seiz_CaseHistory.GetDetails(tableId);
        }

        public static string Insert(cm_seiz_CaseHistory cm_obj)
        {
            return DL_cm_seiz_CaseHistory.InsertSeiz_CaseHistory(cm_obj);
        }

        public static string Update(cm_seiz_CaseHistory cm_obj)
        {
            return DL_cm_seiz_CaseHistory.Update_CaseHistory(cm_obj);
        }
    }
}
