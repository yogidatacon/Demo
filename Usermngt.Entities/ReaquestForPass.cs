using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
  public  class ReaquestForPass
    {
        public string request_for_pass_id { get; set; }
        public string rrnoc_request_id { get; set; }
        public string financial_year { get; set; }
        public double req_qty { get; set; }
        public double approved_qty { get; set; }
        public string approval_status { get; set; }
        public string user_id { get; set; }
        public string to_party_code { get; set; }
        public string record_status { get; set; }
        public string remarks { get; set; }
        public string valied_date { get; set; }
        public string product_name { get; set; }
        public double dispatch_qty { get; set; }
        public double alloted_qty { get; set; }
        public string rr_date { get; set; }
        public double approvedqty { get; set; }
        public double blance_qty { get; set; }
        public double lifted_qty { get; set; }
        public string party_code { get; set; }
        public string toparty_code { get; set; }
        public string pass_type { get; set; }
         public string noc_depotdetail_id { get; set; }
        public string rr_allotmentno { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string product_code { get; set; }
        public string issue_nocno { get; set; }
        public string depot { get; set; }
        public string rr_noc_issuedno { get; set; }
        public double request_qty { get; set; }
        public string rr_noc_id { get; set; }
        public string permitno { get; set; }
        public string pass_valid_upto { get; set; }
        public string route_details { get; set; }
        public string digital_lock_no { get; set; }
        public string vehicle_no { get; set; }
        public string  permit_no { get; set; }
        public string request_for_pass_date { get; set; }
        public double temperature { get; set; }
        public double indication { get; set; }
        public double strength { get; set; }

        public List<VAT_Master> vats { get; set; }
    }
}
