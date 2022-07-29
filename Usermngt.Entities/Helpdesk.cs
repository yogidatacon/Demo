using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
    public class Helpdesk
    {
        public int helpdesk_ticket_id { get; set; }
        public string ticketno { get; set; }
        public string ticket_query { get; set; }
        public int ticket_raisedby { get; set; }
        public string ticket_formname { get; set; }
        public string user_email { get; set; }
        public double user_contact { get; set; }
        public int ticket_developer { get; set; }
        public int ticket_tester { get; set; }
        public string priority_code { get; set; }
        public string ticketstatus_code { get; set; }
        public string ticket_start { get; set; }
        public string ticket_end { get; set; }
        public string timetaken { get; set; }
        public string user_id { get; set; }
        public string record_status { get; set; }
        public string developer { get; set; }
        public string tester { get; set; }
        public string priority { get; set; }
        public string path { get; set; }
        public int developer_code { get; set; }
        public int tester_code { get; set; }
        public string ticketstatus { get; set; }
        public string creation_date { get; set; }
        public string lastmodified_date { get; set; }
        public int user_registration_id { get; set; }
        public string priority_resolvetime { get; set; }
        public int timetaken_dev { get; set; }
        public int timetaken_tester { get; set; }
        public string ticket_start_dev { get; set; }
        public string ticket_end_dev { get; set; }
        public string ticket_start_tester { get; set; }
        public string ticketcategory_code { get; set; }
        public string ticket_end_tester { get; set; }
        public string party_name { get; set; }

    }
}
