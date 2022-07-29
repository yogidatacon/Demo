using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_cm_seiz_ChargeSheet
    {
        public static List<cm_seiz_ChargeSheet> GetList(string seizureNo)
        {
            return DL_cm_seiz_ChargeSheet.GetList(seizureNo);
        }

        public static bool InsertSeiz_ChargeSheet(cm_seiz_ChargeSheet obj)
        {
            return DL_cm_seiz_ChargeSheet.InsertSeiz_ChargeSheet(obj);
        }

        public static bool Update(cm_seiz_ChargeSheet cm_obj)
        {
            return DL_cm_seiz_ChargeSheet.Update(cm_obj);        
        }

        public static cm_seiz_ChargeSheet GetDetailsById(string tableId)
        {
            return DL_cm_seiz_ChargeSheet.GetDetailsById(tableId);
        }
    }
}
