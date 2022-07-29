using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class NOC_Depot
    { 
        public string Depot_id { get; set; }
        public string Depot_name { get; set; }
        public double qty { get; set; }
        public double totalqty { get; set; }
        public double reqqty { get; set; }
        public double liftedqty { get; set; }
        public double bal_qty { get; set; }
        public string record_status { get; set; }
        public string record_deleted { get; set; }
        public string deleted_id { get; set; }


    }
}
