using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
  public  class Seizure_Docs
    {
        public string seizure_docs_id { get; set; }
        public string seizureno { get; set; }
        public string seizure_excisable_articles_id { get; set; }
        public string doc_id { get; set; }
        public string doc_name { get; set; }
        public string doc_type { get; set; }
        public string doc_path { get; set; }
        public string user_id { get; set; }
        public string description { get; set; }
       
        public string docs_from { get; set; }
        public string document_type { get; set; }

    }
}
