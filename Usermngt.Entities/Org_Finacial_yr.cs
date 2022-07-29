using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class Org_Finacial_yr
    {
        public string org_id { get; set; }
        public string org_name { get; set; }
        public string financial_year { get; set; }
        public DateTime lastmodified_date { get; set; }
        public string status { get; set; }
        public int slno { get; set; }
        public string user_id { get; set; }


    }
}
