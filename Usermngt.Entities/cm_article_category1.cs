using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
    public class cm_article_category1
    {
        public int article_category_master_id { get; set; }       
        public string article_category_code { get; set; }
        public string article_category_name { get; set; }
        public DateTime lastmodified_date { get; set; }
        public string user_id { get; set; }
        public DateTime creation_date { get; set; }
        public bool record_status { get; set; }
        public bool record_deleted { get; set; }
    }
}
