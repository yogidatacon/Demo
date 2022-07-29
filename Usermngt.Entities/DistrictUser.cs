using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
    public class DistrictUser
    {
        public int district_login_id { get; set; }
        public string district_name { get; set; }
        public string district_code { get; set; }
        public string department_name { get; set; }
        public string department_code { get; set; }
        public string user_id { get; set; }
        public string user_password { get; set; }
        public string email_id { get; set; }
        public string email_id2 { get; set; }
        public string email_id3 { get; set; }
        public long mobile_no { get; set; }
        public long mobile_no2 { get; set; }
        public long mobile_no3 { get; set; }
        public string full_name { get; set; }
        public DateTime? created_on { get; set; }
        public string created_by { get; set; }
        public string userid { get; set; }      
        public string dist_id { get; set; }
    }
}
