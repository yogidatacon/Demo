using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
  public  class Molasses_Allocation
    {
        public string molasses_allotment_request_id { get; set; }
        public string req_allotmentno { get; set; }
        public string req_allotmentdate { get; set; }
        public string financial_year { get; set; }
        public string final_allotmentdate { get; set; }
        public string final_allotmentno { get; set; }
        public string party_code { get; set; }
        public string requested_fromunit { get; set; }
        public string iscaptive { get; set; }
        public double prov_indent_qty { get; set; }
        public double qty_allotted_till_date { get; set; }
        public double reqd_qty { get; set; }
        public string product_code { get; set; }
        public string allotment_validdate { get; set; }
        public string allotment_status { get; set; }
        public string remarks { get; set; }
        public string approver_remarks { get; set; }
        public string user_id { get; set; }
        public string record_status { get; set; }
        public string record_active { get; set; }
        public string product_name { get; set; }
        public int approverlevel { get; set; }
        public string party_name { get; set; }
        public string district_code { get; set; }
        public double approved_qty { get; set; }
        public List<EASCM_DOCS> docs { get; set; }

        public int consumption_register_id { get; set; }
        public int consumption_no { get; set; }

        public int issue_no { get; set; }
        public string consumption_date { get; set; }
        public string issue_date { get; set; }

        public string creation_date { get; set; }

        public string lastmodified_date { get; set; }
        
        public string record_deleted { get; set; }

        public string application_requestno { get; set; }
    }

    
}
