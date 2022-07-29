using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class FermenterSetUp
    {
        public int fermenter_setup_id { get; set; }
        public int rawmaterial_fermenter_id { get; set; }
        public string fromstoragevat { get; set; }
        public string no_of_each_vat { get; set; }
        public string user_id { get; set; }
        public double mahua { get; set; }
        public double molasses { get; set; }
        public double gur { get; set; }
        public double spentwash { get; set; }
        public double activewash { get; set; }
        public double water { get; set; }
        public string other_material { get; set; }
        public string record_status { get; set; }
        public string vat_code { get; set; }
        public string vat_name { get; set; }
        public string rawmaterial { get; set; }
        public string rawmaterialname { get; set; }


    }
}
