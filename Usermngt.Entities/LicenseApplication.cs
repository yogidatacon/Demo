using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
    public class LicenseApplication 
    {
       public int lic_application_id { get; set; }
        public int lic_application_no { get; set; }
        public string lic_application_date { get; set; }
        public string financial_year { get; set; }
        public string lic_type_code { get; set; }
        public double lic_fee_amt { get; set; }
        public string lic_subtype_code { get; set; }
        public string division_code { get; set; }
        public string party_code { get; set; }
        public string lic_fee_code { get; set; }
        public string lic_type_name { get; set; }
        public string lic_fee_name { get; set; }
        public string renewed { get; set; }
        public int lic_issue_no { get; set; }
        public string lic_issue_date { get; set; }
        public string applicant_name { get; set; }
        public string father_unit_name { get; set; }
        public string applicant_rank { get; set; }
        public string dob { get; set; }
        public string advt_ref { get; set;}
        public string address { get; set; }
        public string state_code { get; set; }
        public string district_code { get; set; }
        public string thana_code { get; set; }
        public string taluk_town { get; set; }
        public string pan { get; set; }
        public int pin { get; set; }
        public double mobile { get; set; }
        public string tan { get; set; }
        public int aadhaar { get; set; }
        public string tin { get; set; }
        public string gst { get; set; }
        public string email { get; set; }
        public string photoname { get; set; }
        public string idproof_code { get; set; }
        public string idproof_name { get; set; }
        public string photo_image { get; set; }
        public string remarks { get; set; }
       public string idproof_image { get; set; }
        public string lic_status { get; set; }
        public string user_id { get; set; }
        public string record_status { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string user { get; set; }
        public List<aplliedfor> applied { get; set; }
        public List<EASCM_DOCS> doc { get; set; }
    }
}
