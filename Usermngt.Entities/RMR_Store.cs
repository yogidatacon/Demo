using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
  public  class RMR_Store
    {
        public string vat_code { get; set; }
        public string vat_name { get; set; }
        public double storedqty { get; set; }
        public double opening_dips { get; set; }
        public string rmstorageid { get; set; }

        public string user_id { get; set; }
    }
}
