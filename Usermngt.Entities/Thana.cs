using System;

namespace Usermngt.Entities
{
    public class Thana
    {
        public int thana_master_id { get; set; }
        public string district_code { get; set; }
        public string district_name { get; set; }
        public string thana_name { get; set; }
        public DateTime? created_on { get; set; }
        public string created_by { get; set; }
        public DateTime? updated_on { get; set; }
        public string updated_by { get; set; }
    }
}
