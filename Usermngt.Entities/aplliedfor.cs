using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class aplliedfor
    {
        public string lic_subtype_code { get; set; }
        public string lic_subtype_name { get; set; }
        public int lic_application_id { get; set; }
        public int lic_applied_for_id { get; set; }
    }
}
