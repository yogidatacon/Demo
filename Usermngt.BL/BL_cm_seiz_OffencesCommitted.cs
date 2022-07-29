using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_cm_seiz_OffencesCommitted
    {
        public static string Insert(cm_seiz_OffencesCommitted offence)
        {
            return DL_cm_seiz_OffencesCommitted.Insert(offence);
        }

        public static string Update(cm_seiz_OffencesCommitted offence)
        {
            return DL_cm_seiz_OffencesCommitted.Update(offence);
        }

        public static List<cm_seiz_OffencesCommitted> GetList( string seizureno)
        {
            return DL_cm_seiz_OffencesCommitted.GetDetails(seizureno);
        }

        public static List<cm_seiz_OffencesCommitted> GetoffenceList(string v)
        {
            return DL_cm_seiz_OffencesCommitted.GetoffenceList(v);
        }
    }
}
