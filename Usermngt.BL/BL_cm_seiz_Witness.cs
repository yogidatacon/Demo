using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_cm_seiz_Witness
    {
        public static List<cm_seiz_Witness> GetList(string seizureNo)
        {
            return DL_cm_seiz_Witness.GetList(seizureNo);
        }      

        public static cm_seiz_Witness GetDetails(string tableId)
        {
            return DL_cm_seiz_Witness.GetDetails(tableId);
        }

        public static string InsertWitness(cm_seiz_Witness cm_obj)
        {
            return DL_cm_seiz_Witness.Insert(cm_obj);
        }

        public static string Update(cm_seiz_Witness witness)
        {
            return DL_cm_seiz_Witness.Update(witness);
        }

        public static List<cm_seiz_Witness> WitnessSearch(string witnessname, string fatherName, string mobile)
        {
            return DL_cm_seiz_Witness.WitnessSearch(witnessname, fatherName, mobile);
        }
    }
}
