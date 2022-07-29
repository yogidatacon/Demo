using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
    public class cm_seiz_OffencesCommitted
    {
        public string seizure_accused_offences_id { get; set; }
        public string seizure_accused_details_id { get; set; }
        public string raidby { get; set; }
        public string accusedname { get; set; }
        public string seizureno { get; set; }
        public string offence_code { get; set; }
        public string offence_name { get; set; }
        public string offence_section_code { get; set; }
        public string offence_section_name { get; set; }
        public string offence_details { get; set; }
        public string other_offences { get; set; }
        public string ipaddress { get; set; }
        public DateTime lastmodified_date { get; set; }
        public string user_id { get; set; }
        public DateTime creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
        public List<Seizure_Docs> docs { get; set; }

    }
}
