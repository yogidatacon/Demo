using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
   public class BL_TicketStatus
    {
        public static bool Insert(TicketStatus Ticket)
        {
            return DL_TicketStatus.Insert(Ticket);
        }

        public static bool Update(TicketStatus Ticket)
        {
            return DL_TicketStatus.Update(Ticket);
        }
        public static List<TicketStatus> GetList()
        {
            return DL_TicketStatus.GetList();
        }

    }
}
