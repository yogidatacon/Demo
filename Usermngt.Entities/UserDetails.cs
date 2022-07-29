using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
  public  class UserDetails
    {
        public int id { get; set; }
        public string user_name { get; set; }
        public string user_id { get; set; }
        public string user_password { get; set; }
        public string password_expiry { get; set; }
        public string user_address { get; set; }
        public string user_age { get; set; }
        public string user_dob { get; set; }
        public string mobile { get; set; }
        public string email_id { get; set; }
        public string department_name { get; set; }
        public int org_id { get; set; }
        public string state_code { get; set; }
        public string division_code { get; set; }
        public string district_code { get; set; }
        public int role_name_code { get; set; }
        public int access_type_code { get; set; }

        public int role_level_code { get; set; }
        
        public string photoname { get; set; }
        public string designation_code { get; set; }
        public string designation_name { get; set; }
        public byte photo_image { get; set; }
        public string office_level { get; set; }
        public string Financial_year_enddate { get; set; }
        public string record_status { get; set; }
        public string party_code { get; set; }
        public string party_type { get; set; }
        public string party_type_code { get; set; }
        public int approver_level { get; set; }
        public bool active_status { get; set; }
        public string email_otp { get; set; }
        public string comments { get; set; }
        public string org_name { get; set; }
        public string role_name { get; set; }
        public string module_code { get; set; }
        public string submodule_code { get; set; }
        public string tab_name_id { get; set; }
        public string tab_name { get; set; }
        public string add { get; set; }
        public string edit { get; set; }
        public string delete { get; set; }
        public string review { get; set; }
        public string approve { get; set; }
        public string party_captive_unit_name { get; set; }
        public string financial_year { get; set; }
        public string iscaptive { get; set; }
        public string district_name { get; set; }
        public string emp_id { get; set; }
        public string digi_id { get; set; }
        public string digi_status { get; set; }
        public string digi_password { get; set; }
        public string party_name { get; set; }
        public string party_approvel_level { get; set; }
        public string division_name { get; set; }
        public string blood_group { get; set; }
        public string emergency_contact { get; set; }
        public string pran_no { get; set; }
        public string date_of_joining { get; set; }
        public string date_of_posting { get; set; }

        public string date_of_retairment { get; set; }



    }
}
