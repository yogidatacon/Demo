using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class DigitalSignature
    {
        public int digital_signature_id { get; set; }
        public string user_id { get; set; }
        public int dongle_userid { get; set; }
        public string dongle_password { get; set; }
        public string dongle_id { get; set; }
        public int user_registration_id { get; set; }
        public string emp_name { get; set; }
        public string role_name { get; set; }
        public string role_name_code { get; set; }
        public string digi_user_id { get; set; }
        public string certyfing_authority { get; set; }
        public string empid { get; set; }

    }
}
