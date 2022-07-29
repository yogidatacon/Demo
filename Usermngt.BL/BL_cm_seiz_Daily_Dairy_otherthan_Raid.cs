using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_cm_seiz_Daily_Dairy_otherthan_Raid
    {
        public static List<daily_diary_entry_otherthan_raid> GetList(string user)
        {
            return DL_cm_seiz_Daily_Dairy_otherthan_Raid.GetList(user);
        }

        public static bool Update(daily_diary_entry_otherthan_raid dd)
        {
            return DL_cm_seiz_Daily_Dairy_otherthan_Raid.Update(dd);
        }

        public static bool Insert(daily_diary_entry_otherthan_raid dd)
        {
            return DL_cm_seiz_Daily_Dairy_otherthan_Raid.Insert(dd);
        }

        public static daily_diary_entry_otherthan_raid GetDetails(string id)
        {
            return DL_cm_seiz_Daily_Dairy_otherthan_Raid.GetDetails(id);
        }
    }
}
