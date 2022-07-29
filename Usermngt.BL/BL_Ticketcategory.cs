using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
   public class BL_Ticketcategory
    {
        public static bool Insert(Ticketcategory Ticket)
        {
            return DL_Ticketcategory.Insert(Ticket);
        }
        public static bool Update(Ticketcategory Ticket)
        {
            return DL_Ticketcategory.Update(Ticket);
        }

        public static List<Ticketcategory> GetList()
        {
            return DL_Ticketcategory.GetList();
        }
        }
}
