using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_cm_seiz_Daily_Dairy_Raid
    {
        public static bool Insert(daily_diary_raid dd)
        {
           return  DL_cm_seiz_Daily_Dairy_Raid.Insert(dd);
        }

        public static bool Update(daily_diary_raid dd)
        {
            return DL_cm_seiz_Daily_Dairy_Raid.Update(dd);
        }

        public static daily_diary_raid GetDetails(string raid_id)
        {
            return DL_cm_seiz_Daily_Dairy_Raid.GetDetails(raid_id);
        }

        public static List<daily_diary_raid> GetList(string user)
        {
            return DL_cm_seiz_Daily_Dairy_Raid.GetList(user);
        }

        public static bool UserUpdate(UserDetails user)
        {
            return DL_cm_seiz_Daily_Dairy_Raid.UserUpdate(user);
        }
    }
}
