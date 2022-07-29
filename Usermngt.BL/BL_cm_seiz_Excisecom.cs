using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_cm_seiz_Excisecom
    {
        public static List<cm_seiz_Excisecom> GetList(string seizureNo)
        {
            return DL_cm_seiz_Excisecom.GetList(seizureNo);
        }

        public static cm_seiz_Excisecom GetDetailsByID(string tableId)
        {
            return DL_cm_seiz_Excisecom.GetDetails(tableId);
        }

        public static bool InsertSeiz_Excisecom(cm_seiz_Excisecom obj)
        {
            return DL_cm_seiz_Excisecom.InsertSeiz_Excisecom(obj);
        }

        public static bool Update(cm_seiz_Excisecom cm_obj)
        {
            return DL_cm_seiz_Excisecom.Update_Excisecom(cm_obj);
        }
    }
}
