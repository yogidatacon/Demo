using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class RawMaterial
    {
        public string rawmaterial_code { get; set; }
        public string rawmaterial_name { get; set; }
        public string rawmaterial_description { get; set; }
        public string rawmaterial_type_code { get; set; }
        public string rawmaterial_type_name { get; set; }
        public string uom_code { get; set; }
        public string uom_name { get; set; }
        public string user_id { get; set; }
        public string product_type_code { get; set; }
    }
}
