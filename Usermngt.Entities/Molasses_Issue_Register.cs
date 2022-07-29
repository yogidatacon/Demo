using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
  public  class Molasses_Issue_Register
    {
        public int molassesissueregister_id { get; set; }
        public string party_code { get; set; }
        public string to_party_code { get; set; }
        public string financial_year { get; set; }
        public string party_name { get; set; }
        public string vat_code { get; set; }
        public string vat_name { get; set; }
        public string mir_entrydate { get; set; }
        public string passno { get; set; }
        public string pass_issuedno { get; set; }
        public string passdate { get; set; }
        public string issuetype { get; set; }
        public string digilockno { get; set; }
        public double issuedqty { get; set; }
        public double valuers { get; set; }
        public double basicrs { get; set; }
        public double splrs { get; set; }
        public double destroyedqty { get; set; }
        public string remarks { get; set; }
        public string record_id_format { get; set; }
        public string user_id { get; set; }
        public string record_status { get; set; }
        public string record_deleted { get; set; }
        public double openingbalance { get; set; }
        public double production { get; set; }
        public string uom { get; set; }
        public string vehicle_no { get; set; }
        public double rem_pass_qty { get; set; }
        public double closing_dips { get; set; }
        public double pass_type { get; set; }
        public double cutomer_name { get; set; }
        public double cutomer_id { get; set;}
        public string dispatch_type_name { get; set; }
        public double grossweight { get; set; }
    }
}
