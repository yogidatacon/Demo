using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
  public  class Molasses_Delivery_Details_MF2
    {
        public string molasses_deliverydetail_id { get; set; }
        public string molasses_prov_prod_id { get; set; }
        public string delivery_type { get; set; }
        public string party_code { get; set; }
        public string party_name { get; set; }
        public string other_party { get; set; }
        public string delivered_year { get; set; }
        public string delivered_qty { get; set; }
        public string deleted_ids { get; set; }
    }
}
