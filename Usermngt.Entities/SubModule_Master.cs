﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
    public class SubModule_Master
    {
        public int submodule_id { get; set; }
        public string submodule_code { get; set; }
        public string submodule_name { get; set; }
        public string mns_no { get; set; }
        public string org_id { get; set; }
        public string user_id { get; set; }
        public string org_name { get; set; }
        public string module_name { get; set; }
        public List<Tab_Master> tabnames { get; set; }
    }
}
