using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
    public class Distillery
    {
        public int distillery_master_id { get; set; }
        public int distillery_code { get; set; }
        public string distillery_name { get; set; }
        public DateTime? created_on { get; set; }
        public string created_by { get; set; }
        public DateTime? updated_on { get; set; }
        public string updated_by { get; set; }
    }
}
