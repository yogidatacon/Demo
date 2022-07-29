using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
    #region cm_seiz_Witness
    public class cm_seiz_Witness
    {
        public string seizure_witnessdetails_id { get; set; }
        public string seizureno { get; set; }
        public string gender_code { get; set; }
        public string witnesstype { get; set; }
        public string designation_code { get; set; }
        public string designation_name { get; set; }
        public string witnessname { get; set; }
        public string relativename { get; set; }
        public string witness_age { get; set; }
        public string presentaddress { get; set; }
        public string permanentaddress { get; set; }
        public string mobile { get; set; }
        public string landline { get; set; }
        public string witness_emailid { get; set; }
        public string user_id { get; set; }
        public string record_status { get; set; }
        public string lastmodified_date { get; set; }
        public string raidby { get; set; }
        public string ipaddress { get; set; }
        public string district_code { get; set; }
        public List<Seizure_Docs> docs { get; set; }
    }
    #endregion cm_seiz_Witness
}
