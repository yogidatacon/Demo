using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class RawmaterialWastage
    {
        public int rawmaterial_wastage_id { get; set; }
        public string vat_code { get; set; }
        public string financial_year { get; set; }
        public string rmw_entrydate { get; set; }
        public string user_id { get; set; }
        public double transit_wastage { get; set; }
        public double storage_wastage { get; set; }
        public double handling_wastage { get; set; }
        public double inc_operation { get; set; }
        public double dec_wastage { get; set; }
        public string remarks { get; set; }
        public string record_status { get; set; }
        public string vat_name { get; set; }
        public string party_code { get; set; }
        public string party_name { get; set; }
        public string record_status1 { get; set; }
        public string rawmaterialname { get; set; }

    }
}
