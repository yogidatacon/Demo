using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
  public  class RolePermissions
    {
        public int id { get; set; }
        public int org_id { get; set; }
        public string mns_no { get; set; }
        public int role_name_code { get; set; }
        public string role_name { get; set; }
        public string submodule_name { get; set; }
        public string dept_name { get; set; }
        public string tab_name { get; set; }
        public string addpermition { get; set; }
        public string editpermition { get; set; }
        public string deletepermition { get; set; }
        public string reviewpermition { get; set; }
        public string approvepermition { get; set; }
       
        public string list_url { get; set; }
        public string user_registration_id { get; set; }
        public string userid { get; set; }


    }
}
