using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class FReceiverInput
    {
        public int fermenter_receiver_input_id { get; set; }
        public string removal_hour { get; set; }
        public int fermenter_receiver_id { get; set; }
        public string fermentervat { get; set; }
        public string receivervat { get; set; }
        public double dips { get; set; }
        public double temperature { get; set; }
        public double indication { get; set; }
        public double strength { get; set; }
        public double bl_received { get; set; }
        public double lp_received { get; set; }
        public string vat_name { get; set; }
        public string financial_year { get; set; }
    }
}
