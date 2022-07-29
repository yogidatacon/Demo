using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
  public class NOC_Details
    {
        public string noc_id { get; set; }
        public double Approved_qty { get; set; }
        public string nocdate { get; set; }
        public string financial_year { get; set; }
        public string req_nocno { get; set; }
        public string customer_id { get; set; }
        public string tenderno { get; set; }
        public string pono { get; set; }
        public string noc_for { get; set; }
        public double noc_total_qty { get; set; }
        public double noc_lifted_qty { get; set; }
        public double noc_balance_qty { get; set; }
        public string noc_status { get; set; }
        public string applicant_id { get; set; }
        public string valid_upto { get; set; }
        public string remarks { get; set; }
        public string approver_remarks { get; set; }
        public int approverlevel { get; set; }
        public string issued { get; set; }
        public string issue_nocno { get; set; }
        public string number_type { get; set; }
        public string number_issuedate { get; set; }
        public string issue_date { get; set; }
        public string user_id { get; set; }
        public string record_status { get; set; }
        public string party_code { get; set; }
        public string Cust_name { get; set; }
        public string cust_address { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string district { get; set; }
        public string thana { get; set; }
        public string state { get; set; }
        public string pincode { get; set; }
        public string party_name { get; set; }
        public string product_name { get; set; }
        public double req_qty { get; set; }
        public int slno { get; set; }
        public List<EASCM_DOCS> docs { get; set; }
        public List<NOC_Depot> depot { get; set; }
            
    }
}
