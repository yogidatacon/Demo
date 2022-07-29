using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class Product_Master
    {
        public string product_master_id { get; set; }
        public string product_code { get; set; }
        public string product_name { get; set; }
         
        public string product_type_code { get; set; }
        public string product_type_name { get; set; }
        public string party_type_code { get; set; }
        public string user_id { get; set; }
    }
}
