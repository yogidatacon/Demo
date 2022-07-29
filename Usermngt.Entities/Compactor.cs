using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
    public class Compactor
    {
        public int compactor_id { get; set; }

        public int tech_id { get; set; }

        public string comp_name { get; set; }

        public string user_id { get; set; }
    }
}
