using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
  public  class State
    {
        public string state_id { get; set; }
        public string state_Code { get; set; }
        public string state_name { get; set; }
        public string country_name { get; set; }

        public string description { get; set; }
        public string status { get; set; }
        public string user_id { get; set; }
    }
}
