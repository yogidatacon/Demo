using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
    public class VatType
    {
        public string id { get; set; }
        public string vat_type_code { get; set; }
        public string vat_type_name { get; set; }
        //public string org_id { get; set; }
        //public string vat_active { get; set; }
        public string user_id { get; set; }
        //public string status { get; set; }
    }
}
