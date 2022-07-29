using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
  public  class Party_Type_Master
    {
        public string party_type_code { get; set; }
        public string party_type_name { get; set; }
        public string party_active { get; set; }
        public int org_id { get; set; }
        public string user_id { get; set; }
    }
}
