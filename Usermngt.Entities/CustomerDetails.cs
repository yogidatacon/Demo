using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
  public  class CustomerDetails
    {
        public string customer_id { get; set; }
        public string party_code { get; set; }
        public string party_name { get; set; }
        public string cust_name { get; set; }
        public string cust_address { get; set; }
        public string cust_mobile { get; set; }
        public string cust_email { get; set; }
        public string district_code { get; set; }
        public string thana_code { get; set; }
        public string pincode { get; set; }
        public string state_code { get; set; }
        public string user_id { get; set; }
        public string record_status { get; set; }

        public string district_name { get; set; }
        public string thana_name { get; set; }
        public string state_name { get; set; }
        
    }
}
