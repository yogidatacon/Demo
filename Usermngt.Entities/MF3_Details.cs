using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
  public  class MF3_Details
    {
        public string molasses_actual_prod_id { get; set; }
        public string financial_year { get; set; }
        public string party_code { get; set; }
        public string crushing_closedate { get; set; }
        public double cane_crushed_total { get; set; }
        public string product_code { get; set; }
        public double molasses_produced_total { get; set; }
        public double sugar_produced_total { get; set; }
        public double qty_lifted_total { get; set; }
        public string wagon_load { get; set; }
        public string preventive_arrangement { get; set; }
        public double bal_avail_qty_total { get; set; }
        public string user_id { get; set; }
        public string record_status { get; set; }
        public List<Molasses_Storage_Details_MF2> storage = new List<Molasses_Storage_Details_MF2>();
    }
}
