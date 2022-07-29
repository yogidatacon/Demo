using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
   public class BL_HelpDesk
    {
        public static string Insert(Helpdesk helpdesk)
        {
            return DL_HelpDesk.Insert(helpdesk);
        }

        public static List<Helpdesk> GetList(string userid)
        {
            return DL_HelpDesk.GetList(userid);
        }

        public static Helpdesk GetDetails( int id)
        {
            return DL_HelpDesk.GetDetails( id);
        }
        public static List<TicketHistory> GetDetailsHistory(int id)
        {
            return DL_HelpDesk.GetDetailsHistory(id);
        }
        public static int GetExistsData(string tablename, string column, string value)
        {
            return DL_HelpDesk.GetExistsData(tablename, column, value);
        }

        public static string InsertHistory(TicketHistory ticket)
        {
            return DL_HelpDesk.InsertHistory(ticket);
        }
    }
}
