using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class TicketStatus
    {
        public int ticketstatus_master_id { get; set; }
        public string ticketstatus_code { get; set; }
        public string ticketstatus_name { get; set; }
        public string user_id { get; set; }

    }
}
