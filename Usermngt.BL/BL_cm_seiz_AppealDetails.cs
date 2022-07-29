using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_cm_seiz_AppealDetails
    {
        public static List<cm_seiz_AppealDetails> GetList(string seizureNo)
        {
            return DL_cm_seiz_AppealDetails.GetList(seizureNo);
        }

        public static cm_seiz_AppealDetails GetDetailsByID(string tableId)
        {
            return DL_cm_seiz_AppealDetails.GetDetails(tableId);
        }

        public static bool InsertAppeal(cm_seiz_AppealDetails obj)
        {
            return DL_cm_seiz_AppealDetails.InsertSeiz_Appeal(obj);
        }

        public static bool Update(cm_seiz_AppealDetails cm_obj)
        {
            return DL_cm_seiz_AppealDetails.Update_Appeal(cm_obj);
        }        
    }
}
