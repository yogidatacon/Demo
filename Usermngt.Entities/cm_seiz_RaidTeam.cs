using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
    public class cm_seiz_RaidTeam
    {
        public string seizure_raiddetails_id { get; set; }
        public string seizureno { get; set; }
        public string raidby { get; set; }
        public string seizure_docs_id { get; set; }
        public string raidteam_type { get; set; }
        public string officername { get; set; }
        public string designation_code { get; set; }
        public string designation_name { get; set; }
        public string raidteamlead { get; set; }
        public string raidteamcode { get; set; }
        public string ipaddress { get; set; }
        public DateTime lastmodified_date { get; set; }
        public string user_id { get; set; }
        public DateTime creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
        public string mobileno { get; set; }
        public List<Seizure_Docs> docs { get; set; }
    }
}
