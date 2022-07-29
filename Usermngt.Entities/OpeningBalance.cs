using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
 public   class OpeningBalance
    {
        public string vat_code { get; set; }
        public string vat_name { get; set; }
        public string vat_type_code { get; set; }
        public string vat_type_name { get; set; }
        public string storage_content { get; set; }
        public string uom_code { get; set; }
        public string uom_name { get; set; }
        public string user_id { get; set; }
        public string openingbalance_id { get; set; }
      
        public string openingbalanceyear{ get; set; }
        public string financial_year { get; set; }
        public double openingbalancevalue { get; set; }
        public string party_code { get; set; }
        public string party_name { get; set; }
        public string record_status { get; set; }
        public string creation_date { get; set; }
        public string remarks { get; set; }
        
        public double Total_Capacity { get; set; }
        public double vat_availablecapacity { get; set; }
        public List<OpeningBalance> vats { get; set; }

    }
}
