using System;

namespace Usermngt.Entities
{
    public class ReceivingSectionContext
    {
        public ReceivingSection ReceivingSection { get; set; }
        public PoliceReceiving PoliceReceiving { get; set; }
        public ExciseReceiving ExciseReceiving { get; set; }
        public DistilleryReceiving DistilleryReceiving { get; set; }
    }

    public class ReceivingSection
    {
        public int receiving_section_id { get; set; }
        public int type_of_liquor_id { get; set; }
        public int liquor_sub_type_id { get; set; }
        public int size_master_id { get; set; }
        public int brand_master_id { get; set; }
        public int compactor_id { get; set; }
        public DateTime receiving_date { get; set; }
        public string letter_no { get; set; }
        public DateTime? letter_date { get; set; }
        public string exhibit_from { get; set; }
        public bool is_sealed { get; set; }
        public string issealed_text { get; set; }
        public string quantity { get; set; }
        public string batch_no { get; set; }
        public string address { get; set; }
        public bool issaved { get; set; }
        public DateTime? created_on { get; set; }
        public string created_by { get; set; }
        public DateTime? updated_on { get; set; }
        public string updated_by { get; set; }
    }

    public class PoliceReceiving
    {
        public int police_receiving_id { get; set; }
        public int receiving_section_id { get; set; }
        public string district_code { get; set; }
        public int thana_master_id { get; set; }
        public string police_type { get; set; }
        public string designation { get; set; }
        public string fir_no { get; set; }
        public DateTime? fir_date { get; set; }
        public bool court_order { get; set; }
        public string court_order_text { get; set; }
        public bool fir_copy { get; set; }
        public bool seizure_list { get; set; }
        public string seizure_list_text { get; set; }
    }

    public class ExciseReceiving
    {
        public int excise_receiving_id { get; set; }
        public int receiving_section_id { get; set; }
        public string district_code { get; set; }
        public string excise_type { get; set; }
        public string designation { get; set; }
        public string case_no { get; set; }
        public DateTime? case_date { get; set; }
        public string remark { get; set; }
        public string prno { get; set; }
        public string statevs { get; set; }
    }

    public class DistilleryReceiving
    {
        public int distillery_receiving_id { get; set; }
        public int receiving_section_id { get; set; }
        public string district_code { get; set; }
        public string distillery_code { get; set; }
        public string designation { get; set; }
        public string vat_no { get; set; }
        public DateTime? denatured_date { get; set; }
        public string remark { get; set; }
    }


    public class ReceivingSectionList
    {
        public int receiving_section_id { get; set; }
        public string exhibit_from { get; set; }
        public string type_of_liquor_name { get; set; }
        public string liquor_sub_name { get; set; }
        public string size_master_name { get; set; }
        public string quantity { get; set; }
        public string brand_master_name { get; set; }
        public string batch_no { get; set; }
        public string address { get; set; }
        public string compactor_name { get; set; }
        public bool issaved { get; set; }
        public string issaved_text
        {
            get
            {
                return this.issaved ? "Saved" : "Draft";
            }
        }
    }

    //20220301
    public class ReceivingSectionList1
    {
        public int quant_received_id { get; set; }
        public int form_no { get; set; }
        public string liq_type { get; set; }
        public string liq_sub_type_name { get; set; }
        // public string size_master_name { get; set; }
        public string quantity { get; set; }
        public string brand_name { get; set; }
        public string batch_no { get; set; }
        public string address { get; set; }
        public int comp_id { get; set; }
        public string status { get; set; }



    }
}
