using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class Distillation
    {
        public int distillation_id { get; set; }
        public int rawmaterial_fermenter_id { get; set; }
        public int fermenter_setup_id { get; set; }
        public double final_sg { get; set; }
        public double degree_of_attenuation { get; set; }
        public string no_of_vat_cask { get; set; }
        public string distillation_date { get; set; }
        public string financial_year { get; set; }
        public string start_date { get; set; }
      
        public string end_date { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string to_which_still { get; set; }
        public double bl_to_still { get; set; }
        public double total_bl_removed_from_distillation { get; set; }
        public double total_lp_removed_from_distillation { get; set; }
        public double bl_redistillation { get; set; }
        public string distillation_complete { get; set; }
        public string from_vessel { get; set; }
        public string to_which_still_removed { get; set; }
        public double bl_produced { get; set; }
        public double lp_produced { get; set; }
        public double bl_per_material { get; set; }
        public double lp_per_material { get; set; }
        public double degree_per100wash { get; set; }
        public string spirit_charge_register { get; set; }
        public string remarks { get; set; }
        public string user_id { get; set; }
        public string record_status { get; set; }
        public string vat_code { get; set; }
        public string vat_name { get; set; }
        public double vat_availablecapacity { get; set; }
        public string setup_date { get; set; }
        public string party_code { get; set; }
        public string party_name { get; set; }
        public double total_bl_washsetup { get; set; }
        public double sg_spentwash { get; set; }
        public string tofermentervat { get; set; }
        public double total_qty_transferred { get; set; }
        public List<DistillationToStore> DStore { get; set; }
    }
}
