using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_cm_seiz_Dmconfiscation
    {
        public static List<cm_seiz_Dmconfiscation> GetList(string seizureNo)
        {
            return DL_cm_seiz_Dmconfiscation.GetList(seizureNo);
        }

        public static List<cm_court> GetDMCaseList()
        {
            return DL_cm_seiz_Dmconfiscation.GetDMCaseList();
        }
       
        public static List<cm_court> GetEXCaseList()
        {
            return DL_cm_seiz_Dmconfiscation.GetEXCaseList();
        }





        public static cm_seiz_Dmconfiscation GetDetailsByID(string tableId)
        {
            return DL_cm_seiz_Dmconfiscation.GetDetails(tableId);
        }

        public static bool InsertDmconfiscation(cm_seiz_Dmconfiscation obj)
        {
            return DL_cm_seiz_Dmconfiscation.InsertSeiz_Appeal(obj);
        }

        public static List<cm_court> GetSECCaseList()
        {
            return DL_cm_seiz_Dmconfiscation.GetSECCaseList();
        }

        public static bool Update(cm_seiz_Dmconfiscation cm_obj)
        {
            return DL_cm_seiz_Dmconfiscation.Update_Appeal(cm_obj);
        }
    }
}
