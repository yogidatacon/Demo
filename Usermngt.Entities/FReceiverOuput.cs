using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class FReceiverOuput
    {
        public int fermenter_receiver_id { get; set; }
        public int fermenter_receiver_output_id { get; set; }
        public string to_storagevat { get; set; }
        public string removal_date { get; set; }
        public string removal_hour { get; set; }
        public double bl_tostorage { get; set; }
        public double lp_tostorage { get; set; }
        public string moved_to_nextstage { get; set; }
        public string vat_name { get; set; }
        public string party_code { get; set; }
    }
}
