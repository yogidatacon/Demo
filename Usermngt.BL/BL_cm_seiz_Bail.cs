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
    public class BL_cm_seiz_Bail
    {
        public static List<cm_seiz_Bail> GetList(string seizureNo)
        {
            return DL_cm_seiz_Bail.GetList(seizureNo);
        }

        public static bool InsertSeiz_Bail(cm_seiz_Bail cm_obj)
        {
            return DL_cm_seiz_Bail.InsertSeiz_Bail(cm_obj);
        }

        public static bool Update(cm_seiz_Bail cm_obj)
        {
            return DL_cm_seiz_Bail.Update(cm_obj);
        }

        public static cm_seiz_Bail GetDetailsById(string tableId)
        {
            return DL_cm_seiz_Bail.GetDetailsById(tableId);
        }
    }
}
