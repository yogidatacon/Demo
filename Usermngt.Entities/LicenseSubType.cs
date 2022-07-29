using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class LicenseSubType
    {
        public string lic_subtype_code { get; set; }
        public string lic_subtype_name { get; set; }
        public string lic_type_name { get; set; }
        public string lic_type_code { get; set; }
        
        public string user_id { get; set; }
    }
}
