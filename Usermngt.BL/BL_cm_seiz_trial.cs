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
    public class BL_cm_seiz_trial
    {
        public static List<cm_seiz_trial> GetList(string seizureNo, string trialstage_code)
        {
            return DL_cm_seiz_trial.GetList(seizureNo, trialstage_code);
        }

        public static bool InsertSeiz_Trial(cm_seiz_trial cm_obj, string trialstage_code)
        {
            return DL_cm_seiz_trial.InsertSeiz_trial(cm_obj, trialstage_code);
        }

        public static bool Update(cm_seiz_trial cm_obj)
        {
            return DL_cm_seiz_trial.Update(cm_obj);
        }

        public static cm_seiz_trial GetDetailsById(string tableId)
        {
            return DL_cm_seiz_trial.GetDetailsById(tableId);
        }
    }
}
