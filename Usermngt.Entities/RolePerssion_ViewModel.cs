using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class RolePerssion_ViewModel
    {

        public int mns_no { get; set; }
        public string module_code { get; set; }
        public string module_name { get; set; }
        public List<SubModule_Master> submodulenames { get; set; }
    }

  
}
