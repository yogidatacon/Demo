using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
    public class Complaint
    {
        public int complaint_id { get; set; }
        public int complaint_no { get; set; }
        public string complainant_name { get; set; }
        public string email_id { get; set; }
        public  double mobile_no { get; set; }
        public string address { get; set; }
        public string complaint_details { get; set; }
        public string complaint_status { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public string complaint_type { get; set; }
        public string otp { get; set; }
        public string district { get; set; }
        public string thana { get; set; }
        public string state { get; set; }
        public string village { get; set; }
        public string landmark { get; set; }
        public List<EASCM_DOCS> docs { get; set; }
      
    }
}
