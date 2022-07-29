using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class TicketHistory
    {
        public int hd_ticket_history_id { get; set; }
        public int transaction_id { get; set; }
        public string transaction_date { get; set; }
        public int user_registration_id { get; set; }
        public string remarks { get; set; }
        public string lastmodified_date { get; set; }
        public string createdby_id { get; set; }
        public string creation_date { get; set; }
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string priority_code { get; set; }
        public string ticketsatus { get; set; }
        public string developer { get; set; }
        public string tester { get; set; }
        public string record_status { get; set; }
        public string path { get; set; }
        public int timetaken_dev { get; set; }
        public string ticketcategory_code { get; set; }
        public int timetaken_tester { get; set; }
        public string ticket_start_dev { get; set; }
        public string ticket_end_dev { get; set; }
        public string ticket_start_tester { get; set; }
        public string ticket_end_tester { get; set; }
        public List<HD_DOCS> docs { get; set; }

    }
}
