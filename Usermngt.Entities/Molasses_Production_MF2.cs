using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
  public  class Molasses_Production_MF2
    {
        public string molasses_prov_prod_id { get; set; }
        public string entry_date { get; set; }
        public string financial_year { get; set; }
        public string party_code { get; set; }
        public string cane_crushing_date { get; set; }
        public string party_name { get; set; }
        public string iscaptive { get; set; }
        public string to_party_code { get; set; }
        public string product_code { get; set; }
        public double molasses_plan_next_season { get; set; }
        public double sugar_plan_next_season { get; set; }
        public double molasses_prod_daily { get; set; }
        public double sugar_prod_daily { get; set; }
        public double total_storage_capacity { get; set; }
        public double new_prod_storage { get; set; }
        public string wagon_loading { get; set; }
        public double actualprod_prevyr1 { get; set; }
        public double actualprod_prevyr2 { get; set; }
        public double actualprod_prevyr3 { get; set; }
        public double total_molasses_delivered { get; set; }


        public double other_person_total { get; set; }
        public double molasses_avail_pyr1 { get; set; }
        public double molasses_avail_pyr2 { get; set; }
        public double molasses_avail_pyr3 { get; set; }
        public double total_avail_stock_stored { get; set; }

        public string why_stock_not_cleared { get; set; }
        public string newcrop_storage { get; set; }
        public string newcrop_storage_difficulty { get; set; }
        public string cleaned_storage { get; set; }
        public string name_address_occupier { get; set; }
        public string name_address_manager { get; set; }
        public string mechanical_pump { get; set; }

        public double to_be_allotted { get; set; }
        public double allotted_qty { get; set; }
        public string record_status { get; set; }
        public string userid { get; set; }
        public string name_address_applicant { get; set; }

        public List<Molasses_Storage_Details_MF2> storage = new List<Molasses_Storage_Details_MF2>();
        public List<Molasses_Other_Delevery_MF2> other = new List<Molasses_Other_Delevery_MF2>();
        public List<Molasses_Delivery_Details_MF2> delevery = new List<Molasses_Delivery_Details_MF2>();
        public List<VAT_Master> vatmaster = new List<VAT_Master>();

    }
}
