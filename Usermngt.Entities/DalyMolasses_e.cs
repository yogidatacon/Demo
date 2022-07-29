using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class DalyMolasses_e
    {
        public string party_code { get; set; }
        public string party_name { get; set; }
        public string financial_year { get; set; }
        public double dailyproduction { get; set; }
        public string entrydate { get; set; }
        public string user_id { get; set; }
        public string uom_name { get; set; }
        public string uom_code { get; set; }
        public string record_status { get; set; }
        public List<EASCM_DOCS> docs { get; set; }
    }
}
