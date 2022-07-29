using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
  public  class Release_Request
    {
        public string release_request_id { get; set; }
        public string financial_year { get; set; }
        public string rr_reqno { get; set; }
        public string rr_date { get; set; }
        public string uom_code { get; set; }
        public string rr_allotmentno { get; set; }
        public string party_code { get; set; }
        public string molasses_supplier { get; set; }
        public double allocation_qty { get; set; }
        public string allotment_date { get; set; }
        public string req_allotmentdate { get; set; }
        public double rr_quantity { get; set; }
        public double rr_balance_qty { get; set; }
        public double rr_approved_qty { get; set; }
        public double rr_lifted_qty { get; set; }
        public string valid_date { get; set; }
        public string rr_issueno { get; set; }
        public string product_code { get; set; }
        public string approval_level { get; set; }
        public string approval_status { get; set; }
        public string user_id { get; set; }
        public string record_status { get; set; }
        public string record_deleted { get; set; }
        public string product_name { get; set; }
        public string party_name { get; set; }
        public double prov_indent_qty { get; set; }
        public double rrqty { get; set; }
        public string from_party { get; set; }
        public string district_code { get; set; }
        public string remarks { get; set; }
        public string final_allotment_no { get; set; }
        public string rr_noc_id { get; set; }
        public string suplier_district { get; set; }
        public List<EASCM_DOCS> doc { get;set; }

    }
}
