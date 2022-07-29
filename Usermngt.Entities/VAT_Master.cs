using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
    public class VAT_Master
    {
        public string vat_code { get; set; }
        public string vat_name { get; set; }
        public string vat_type_code { get; set; }
        public string party_type_code { get; set; }
        public string party_code { get; set; }
        public string product_type_code { get; set; }
        public double vat_availablecapacity { get; set; }
        public double vat_totalcapacity { get; set; }
        public int org_id { get; set; }
        public string content { get; set; }
        public double vat_depth { get; set; }
        public string uom_code { get; set; }
        public string user_id { get; set; }
        public string party_name { get; set; }
        public string vat_type_name { get; set; }
        public string uom_name { get; set; }
        public string product_name { get; set; }

    }
}
