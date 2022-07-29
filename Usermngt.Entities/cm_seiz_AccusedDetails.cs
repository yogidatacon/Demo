using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
  public  class cm_seiz_AccusedDetails
    {
        public int seizure_accused_details_id { get; set; }
        public int seizureno { get; set; }
        public string raidby { get; set; }
        public string offence_code { get; set; }
        public string gender_code { get; set; }
        public string religion_code { get; set; }
        public string state_code { get; set; }
        public string district_code { get; set; }
        public string thana_code { get; set; }
        public string district_code1 { get; set; }
        public string thana_code1 { get; set; }
        public string caste_code { get; set; }
        public string category_code { get; set; }
        public string caste_details { get; set; }
        public string idproof_code { get; set; }
        public string accusedstatus_code { get; set; }
        public string gender_name { get; set; }
        public string religion_name { get; set; }
        public string caste_name { get; set; }
        public string idproof_name { get; set; }
        public string accusedstatus_name { get; set; }
        public string accusedname { get; set; }
        public string relativename { get; set; }
        public string accused_age { get; set; }
        public string identificaton_mark { get; set; }
        public string occupation { get; set; }
        public string presentaddress { get; set; }
        public string permanentaddress { get; set; }
        public string breathtest { get; set; }
        public string breathtest_result { get; set; }
        public string bloodtest { get; set; }
        public string bloodtest_result { get; set; }
        public string idno { get; set; }
        public string idphoto { get; set; }
        public string mobileno { get; set; }
        public string mobileno1 { get; set; }
        public string accused_photo { get; set; }
        public int previouscase_count { get; set; }
        public string previous_case_id { get; set; }
        public string ipaddress { get; set; }
        public string fir_status { get; set; }
        public string bail_status { get; set; }
        public string chargesheet_status { get; set; }
        public string judgement_status { get; set; }
        public DateTime lastmodified_date { get; set; }
        public string user_id { get; set; }
        public DateTime creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
        public int seizure_fir_id { get; set; }
        public int seizure_chargesheet_id { get; set; }
        public string SDR_CAF { get; set; }
        


    }
}
