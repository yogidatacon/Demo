using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class EASCM_DOCS
    {
        public int id { get; set; }
        public string doc_id { get; set; }
        public string doc_name { get; set; }
        public string doc_type { get; set; }
        public string doc_path { get; set; }
        public string user_id { get; set;}
        public string description { get; set; }

        public string issue_vat { get; set; }
        public string storage_vat { get; set; }

        public string medicine_name { get; set; }

        public string batch_no { get; set; }

        public string consumption_qty { get; set; }
        public string issue_qty { get; set; }

        public string strength { get; set; }
    }
}
