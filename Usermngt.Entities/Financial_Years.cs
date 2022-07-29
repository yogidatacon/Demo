using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class Financial_Years
    {
        public int id { get; set; }
        public string party_type_code { get; set; }
        public string party_type_name { get; set; }
        public string start_date { get; set; }
        public string financial_year { get; set; }
        public string end_date { get; set; }
        public string status { get; set; }
        public string deleted { get; set; }
    }
}
