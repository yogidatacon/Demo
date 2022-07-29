using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_cm_seiz_RaidTeam
    {
        public static string Insert(cm_seiz_RaidTeam raid)
        {
            return DL_cm_seiz_RaidTeam.Insert(raid);
        }

        public static string Update(cm_seiz_RaidTeam raid)
        {
            return DL_cm_seiz_RaidTeam.Update(raid);
        }

        public static List<cm_seiz_RaidTeam> GetDeails(string seizureNo)
        {
            return DL_cm_seiz_RaidTeam.GetDetails(seizureNo);
        }
    }
}
