using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class Ticketcategory
    {
        public int ticketcategory_master_id { get; set; }
        public string ticketcategory_code { get; set; }
        public string ticketcategory_name { get; set; }
        public string user_id { get; set; }

    }
}
