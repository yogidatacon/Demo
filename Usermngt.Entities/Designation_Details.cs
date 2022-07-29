using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
  public  class Designation_Details
    {
        public string designation_id { get; set; }
        public string designation_type_code { get; set; }
        public string designation_type { get; set; }
        public string designation_code { get; set; }
        public string designation_name { get; set; }
        public string user_id { get; set; }
    }
}
