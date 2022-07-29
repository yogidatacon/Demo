using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class SugarCanePurchase
    {
        public int sugarcanepurchase_id { get; set; }
        public int eascm_docs_id { get; set; }
        public string party_code { get; set; }
        public string party_name { get; set; }
        public string financialyear { get; set; }
        public string entrydate { get; set; }
        public double factorygate_purchase { get; set; }
        public double outstation_purchase { get; set; }
        public double ownestate_purchase { get; set; }
        public double total_purchase { get; set; }
        public double total_canecrushed { get; set; }
        public string remarks { get; set; }
        public string user_id { get; set; }
        public string signature { get; set; }
        public string record_status { get; set; }
        public string attachment { get; set; }
        public int record_type { get; set; }
        public List<EASCM_DOCS> docs { get; set; }
    }
}
