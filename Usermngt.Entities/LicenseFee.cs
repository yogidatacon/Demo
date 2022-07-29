using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
  public  class LicenseFee
    {
        public string lic_fee_code { get; set; }
        public int lic_fee_master_id { get; set; }
        public string lic_type_code { get; set; }
        public string lic_subtype_code { get; set; }
        public string lic_type_name { get; set; }
        public string lic_subtype_name { get; set; }
        public double lic_fee_amt { get; set; }
        public double lic_regn_amt { get; set; }
        public double lic_renewal_fee { get; set; }
        public double lic_security_amt { get; set; }
        public double lic_proc_fee { get; set; }
        public double lic_adv_fee { get; set; }
        public string user_id { get; set; }
    }
}
