using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseMgmt.Models
{
    public class MF3
    {
        public int mf3_seq_no { get; set; }
        public DateTime mf3_date { get; set; }
        public string mf3_producer { get; set; }
        public string mf3_fiscal_year { get; set; }
        public string mf3_status { get; set; }
        public string mf3_captive { get; set; }
        public string mf3_prod_material { get; set; }
        public DateTime mf3_endcrush { get; set; }
        public decimal mf3_final_molasses_produced { get; set; }
        public decimal mf3_final_sugar_produced { get; set; }
        public decimal mf3_qty_lifted { get; set; }
        public decimal mf3_qty_tobelifted { get; set; }
        public decimal mf3_steeltank { get; set; }
        public decimal mf3_pucca_covered { get; set; }
        public decimal mf3_pucca_uncovered { get; set; }
        public decimal mf3_katcha_covered { get; set; }
        public decimal mf3_katcha_uncovered { get; set; }
        public string mf3_arrangement { get; set; }
        public int mf3_wagons { get; set; }
        public string mf3_preventive_arrangement { get; set; }
        public DateTime mf3_create_date { get; set; }
        public DateTime mf3_lastupdated { get; set; }
        public string mf3_record_status { get; set; }
        public decimal mf2_alloted_total { get; set; }
        public string mf3_attachment { get; set; }
        public decimal mf3_production_total { get; set; }//Need to verify this column        
    }

    public class MF3ViewModel
    {
        public int mf3_seq_no { get; set; }
        public DateTime mf3_date { get; set; }
        public string mf3_producer { get; set; }
        public string mf3_fiscal_year { get; set; } 
        public decimal mf2_alloted_total { get; set; } 
        public decimal mf3_production_total { get; set; }//Need to verify this column        
        public string viewColumn { get; set; }//Need to verify this column        
    }
}