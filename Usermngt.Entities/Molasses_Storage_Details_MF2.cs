using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class Molasses_Storage_Details_MF2
    {
        public string molasses_prod_tank_storage_id { get; set; }
        public string molasses_actual_prod_id { get; set; }

        public string financial_year { get; set; }

        public string vat_code { get; set; }
        public string vat_name { get; set; }
        public string bal_capacity { get; set; }
        public string deleted_ids { get; set; }

    }
}
