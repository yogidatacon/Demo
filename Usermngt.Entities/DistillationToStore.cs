using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
    public class DistillationToStore
    {

        public int distillation_tostore_id { get; set; }
        public int distillation_id { get; set; }
        public string vat_code { get; set; }
        public string vat_name { get; set; }
        public double bl_store { get; set; }
        public double lp_store { get; set; }
        public string party_code { get; set; }
        public string party_name { get; set; }
        public string moved_to_nextstage { get; set; }
    }
}
