using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.BL;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_cm_seiz_FIR
    {
        public static List<cm_seiz_FIR> GetList(string seizureNo)
        {
            return DL_cm_seiz_FIR.GetList(seizureNo);
        }

        public static bool InsertSeiz_FIR(cm_seiz_FIR cm_obj)
        {
            return DL_cm_seiz_FIR.InsertSeiz_FIR(cm_obj);
        }

        public static bool Update(cm_seiz_FIR cm_obj)
        {
            return DL_cm_seiz_FIR.Update(cm_obj);
        }

        public static cm_seiz_FIR GetDetailsById(string tableId)
        {
            return DL_cm_seiz_FIR.GetDetailsById(tableId);
        }
    }
}
