using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class Permit_item
    {
        public int permit_id { get; set; }
        public int permit_item_id { get; set; }
        public int product_code { get; set; }
        public double req_qty { get; set; }
        public double strength { get; set; }
        public string uom_code { get; set; }
        public string uom_name { get; set; }
        public string product_name { get; set; }
        public string user_id { get; set; }



    }
}
