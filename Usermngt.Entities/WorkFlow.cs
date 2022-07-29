using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
  public  class WorkFlow
    {
        public int id { get; set; }
        
        public string submodule_code { get; set; }
        public string submodule_name { get; set; }
        public string tab_id { get; set; }
        public string tab_name { get; set; }
        public string role_name { get; set; }
        public string role_name_code { get; set; }
        public string district { get; set; }
        public string district_code { get; set; }
        public string username { get; set; }
        public string approver_level { get; set; }
        public string user_registration_id { get; set; }
        public string user_id { get; set; }
        public int org_id { get; set; }
    }
}
