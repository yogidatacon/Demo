using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class Form82
    {
        public int id { get; set; }
        public string setup_date { get; set; }
        public string financial_year { get; set; }
        public string setup_time { get; set; }
        public string party_code { get; set; }
        public string vat_code { get; set; }
        public double vat_availablecapacity { get; set; }
        public string tofermentervat { get; set; }
        public int rawmaterial_fermenter_id { get; set; }
        public string fromstoragevat { get; set; }
        public double total_qty_transferred { get; set; }
        public string setup_complete { get; set; }
        public string no_of_each_vat { get; set; }
        public double mahua { get; set; }
        public double molasses { get; set; }
        public double gur { get; set; }
        public double spentwash { get; set; }
        public double activewash { get; set; }
        public double water { get; set; }
        public string other_material { get; set; }
        public double total_bl_washsetup { get; set; }
        public double sg_spentwash { get; set; }
        public double sg_of_wash { get; set; }
        public double final_sg { get; set; }
        public double degree_of_attenuation { get; set; }
        public string no_of_vat_cask { get; set; }
        public string  receiver_vat_to_still { get; set; }
        public string start_of_distillation { get; set; }
        public string hour_of_startdistillation { get; set; }
        public double bl_to_still { get; set; }
        public string to_which_still { get; set; }
        public string end_of_distillation { get; set; }
        public string hour_of_enddistillation { get; set; }
        public double total_bl_removed_from_distillation { get; set; }
        public double total_lp_removed_from_distillation { get; set; }
        public string date_of_distillation { get; set; }
        public double bl_redistillation { get; set; }
        public string from_vessel { get; set; }
        public string to_which_still_removed { get; set; }
        public double bl_store { get; set; }
        public double lp_store { get; set; }
        public double bl_produced { get; set; }
        public double lp_produced { get; set; }
        public double bl_per_material { get; set; }
        public double lp_per_material { get; set; }
        public double degree_per100wash { get; set; }
        public string spirit_charge_register { get; set; }
        public string remarks { get; set; }
        public string user_id { get; set; }
        public string record_status { get; set; }
        public string record_deleted { get; set; }
        public string vat_name { get; set; }
        public string party_name { get; set; }
        public List<FermenterSetUp> fermSetup { get; set; }

    }
}
