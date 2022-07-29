using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class HD_DOCS
    {
        public int hd_docs_id { get; set; }
        public int id { get; set; }
        public string doc_id { get; set; }
        public string doc_name { get; set; }
        public string doc_type { get; set; }
        public string doc_path { get; set; }
        public string user_id { get; set; }
        public string description { get; set; }
    }
}
