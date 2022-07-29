using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class Tab_Master
    {
        public int tab_name_id { get; set; }
        public string tab_name { get; set; }
        public string tab_desc { get; set; }
        public string org_id { get; set; }
        public string org_name { get; set; }
        public string mns_no { get; set; }
        public string module_name { get; set; }
        public string submodule_code { get; set; }
        public string submodule_name { get; set; }
        public string user_id { get; set; }
    }
}
