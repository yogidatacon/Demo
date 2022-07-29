using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
  public  class VATTransfers_
    {
        public int vat_transfer_id { get; set; }
        public string vat_type_code { get; set; }
        public string financial_year { get; set; }
        public string vat_type_name { get; set; }
        public string from_vat { get; set; }
        public string from_vatname { get; set; }
        public string remarks { get; set; }
        public string transfered_date { get; set; }
        public double transferqty { get; set; }
        public double lp_transferqty{ get; set; }
        public double dips { get; set; }
        public double temperature { get; set; }
        public double indication { get; set; }
        public double strength { get; set; }
        public string to_vat { get; set; }
        public string to_vatname { get; set; }
        public string party_code { get; set; }
        public string user_id { get; set; }
        public string partyname { get; set; }
        public string record_status { get; set; }
    }
}
