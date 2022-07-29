using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class Reportmaster
    {
        public int id { get; set; }
        public int org_id { get; set; }
        public string module_name { get; set; }
        public int mns_no { get; set; }
        public int role_name_code { get; set; }
        public string  reportname { get; set; }
        public string reportstatus { get; set; }
        public string user_id { get; set; }
        public string report_path { get; set; }
       
        public string reportfilename { get; set; }
        public string partytype { get; set; }
        public string partytypecode { get; set; }
    }
}
