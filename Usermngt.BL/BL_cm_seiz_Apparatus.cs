using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    #region BL_cm_seiz_Apparatus
    public class BL_cm_seiz_Apparatus
    {
        public static List<cm_seiz_Apparatus> GetList(string seizureNo)
        {
            return DL_cm_seiz_Apparatus.GetList(seizureNo);
        }

        public static bool InsertApparatus(cm_seiz_Apparatus obj)
        {
            return DL_cm_seiz_Apparatus.InsertSeiz_Apparatus(obj);
        }

        public static bool Update(cm_seiz_Apparatus cm_obj)
        {
            return DL_cm_seiz_Apparatus.Update_Apparatus(cm_obj);
        }

        public static List<cm_seiz_Apparatus> ApparatusSearch(string name)
        {
            return DL_cm_seiz_Apparatus.ApparatusSearch(name);
        }

        public static List<apparatus_type_master> GetApparatusTypeMasterList(string empty)
        {
            return DL_cm_seiz_Apparatus.GetApparatusTypeMasterList(empty);
        }

        public static cm_seiz_Apparatus GetDetails(string tableId)
        {
            return DL_cm_seiz_Apparatus.GetDetails(tableId);
        }

    }
    #endregion BL_cm_seiz_Apparatus
}
