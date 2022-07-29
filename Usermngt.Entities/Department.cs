using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class Department
    {
         public int id { get; set; }
        public string dept_code { get; set; }
        public string  dept_name { get; set; }
        public string user_id { get; set; }
        public string record_status { get; set; }

    }
}
