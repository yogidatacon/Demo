using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
  public  class DailyMolassesProduction_e
    {
        
        public string dailymolassesproduction_id { get; set; }
        public string vat_code { get; set; }
        public string financial_year { get; set; }
        public string vat_name { get; set; }
        public string storage_content { get; set; }
        public string party_code { get; set; }
        public string party_name { get; set; }
        public string uom_code { get; set; }
        public string uom_name { get; set; }
        public double vat_totalcapacity { get; set; }
        public string user_id { get; set; }
        public string brix { get; set; }
        public double dailyproduction { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public string remarks { get; set; }
        public double vat_availablecapacity { get; set; }
        public List<EASCM_DOCS> docs { get; set; }
    }
}
