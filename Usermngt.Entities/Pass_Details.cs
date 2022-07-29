using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
  public  class Pass_Details
    {
        public string pass_id { get; set; }
        public int pass_reqno { get; set; }
        public string pass_issuedno { get; set; }
        public string pass_date { get; set; }
        public string financial_year { get; set; }

        public string pass_for { get; set; }
        public string to_party_code { get; set; }
        public string customer_id { get; set; }
        public string dispatch_type_id { get; set; }
        public string dispatch_date { get; set; }
        public double dispatch_qty { get; set; }
        public string noc_depotdetail_id { get; set; }
        public string to_dispatch_vat { get; set; }
        public string allotment_validupto { get; set; }
        public string Pass_From_Reqest_No { get; set; }
        public string prev_prod_year { get; set; }
        public string brix { get; set; }
        public string sugar_content { get; set; }
        public string taxinvoice { get; set; }
        public string remarks { get; set; }
        public string carrier { get; set; }
        public string vehicle_no { get; set; }
        public string vehicle_type { get; set; }
        public string driver { get; set; }
        public string challan_no { get; set; }
        public string digital_lock_no { get; set; }
        public string dispatch_time { get; set; }
        public string dispatch_duration { get; set; }
        public string route_details { get; set; }
        public string approver_remarks { get; set; }
        public string user_id { get; set; }
        public string record_status { get; set; }
        public string supplier_unit { get; set; }
        public string party_code { get; set; }
        public string product_code { get; set; }
        public double rem_pass_qty { get; set; }
        public string rrnoc_record_request_id { get; set; }
        public string uom_code { get; set; }
        public string pass_type { get; set;}
        public string depot { get; set; }
        public string request_for_pass_id { get; set; }
        public string from_party { get; set; }
        public string cutomer_name { get; set; }
        public string dispatch_type_name { get; set; }
        public string deposit_amt { get; set; }
        public string deposit_under { get; set; }
        public string available_qty { get; set; }
        public string lifted_qty { get; set; }
        public string rr_noc_issuedno { get; set; }
    }
}
