using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
  public  class Org_Master
    {
        public string org_id { get; set; }
        public string org_name { get; set; }
        public string org_type { get; set; }
        public string org_code { get; set; }
        public string org_address { get; set; }
        public string gst { get; set; }
        public string pan { get; set; }
        public string tan { get; set; }
        public string tin { get; set; }
        public DateTime start_date { get; set; }
        public DateTime creation_date { get; set; }
        public DateTime end_date { get; set; }
        public DateTime lastmodified_date { get; set; }
        public string org_desc { get; set; }
        public string status { get; set; }
        public string user_id { get; set; }
        public string cont_number { get; set; }
        public string email_id { get; set; }


       

        public Org_Master()
        {
           
        }
    }
}
