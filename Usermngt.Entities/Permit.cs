using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
    public class Permit
    {
        public int permit_id { get; set; }
        public int molasses_allotament_request_id { get; set; }
        public int lic_application_id { get; set; }
        public string  treasury { get; set;}
        public string financial_year { get; set; }
        public string product_name { get; set; }
        public string product_code { get; set; }
        public string permit_no { get; set; }
        public string duty_type { get; set; }
        public double duty_amt { get; set; }
        public double permit_qty { get; set; }
        public double duty_rate { get; set; }
        public string address { get; set; }
        public string permit_date { get; set; }
        public double alloted_qty { get; set; }
        public string district_name { get; set; }
        public string district_code { get; set; }
        public string state_name { get; set; }
        public string remarks { get; set; }
        public string purchase_from_party { get; set; }
        public string purchase_of { get; set; }
        public string party_code { get; set; }
        public string party_type_code { get; set; }
        public string party_name { get; set; }
        public string purchase_district { get; set; }
        public string province_of { get; set; }
        public string province_by { get; set; }
        public string agent_name { get; set; }
        public string permit_issueno { get; set; }
        public string permit_validity { get; set; }
        public string challan_no { get; set; }
        public string challan_date { get; set; }
       
        public string route_chart { get; set; }
        public int approver_level { get; set; }

        public string user_id { get; set; }
        public string permit_type { get; set; }
        public string record_status { get; set; }
        public List<Permit_item> permit_item { get; set; }

    }
}
