using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class AccessType
    {
        public string id { get; set; }
        public string access_type_code { get; set; }
        public string access_type_name { get; set; }
        public string access_type_desc { get; set; }
        public string status { get; set; }
        public string user_id { get; set; }
    }
}
