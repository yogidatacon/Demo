using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
  public  class All_Approvals
    {
        public string record_id { get; set; }
        public string record_id_format { get; set; }
        public string transaction_date { get; set; }
        public string transaction_type { get; set; }
        public string transaction_state { get; set; }
        public string financial_year { get; set; }
        public string remarks { get; set; }
        public string creation_date { get; set; }
        public string user_id { get; set; }
        public string role_name { get; set; }
        public string party_code { get; set; }

    }
}
