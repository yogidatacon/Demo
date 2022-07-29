using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
  public  class RoleMaster
    {
        public int id { get; set; }
        public string rolecode { get; set; }
        public string rolename { get; set; }
        public string rolelevel { get; set; }
        public string rolelevel_name { get; set; }
        public string accestype { get; set; }
        public string accestype_name { get; set; }
        public string nextroleCode { get; set; }
        public string organization { get; set; }
        public string organization_name { get; set; }
        public string user_id { get; set; }
        public string sanction_strength { get; set; }
        public string status { get; set; }
        public string nextrole { get; set; }
    }
}
