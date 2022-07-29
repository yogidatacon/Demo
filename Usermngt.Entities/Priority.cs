using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
    public class Priority
    {
        public int priority_master_id { get; set; }
        public string priority_code { get; set; }
        public string priority_name { get; set; }
        public string priority_resolvetime { get; set; }
        public string user_id { get; set; }
    }
}
