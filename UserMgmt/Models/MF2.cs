using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseMgmt.Models
{
	public class MF2
	{
		//public int mf2_seq_no { get; set; }
		//public DateTime mf2_date { get; set; }
		//public string mf2_allocation_type { get; set; }
		//public string mf2_producer { get; set; }
		//public string mf2_fiscal_year { get; set; }
		//public string mf2_status { get; set; }
		//public string mf2_captive { get; set; }
		//public string mf2_captive_unitname { get; set; }
		//public string mf2_prod_material { get; set; }
		//public DateTime mf2_startcrush { get; set; }

		//public decimal mf2_plan_molasses_season { get; set; }
		//public string mf2_plan_molasses_daily { get; set; }
		//public string mf2_plan_sugar_daily { get; set; }
		//public decimal mf2_plan_sugar_season { get; set; }


		//public decimal mf2_total_storage { get; set; }
		//public decimal mf2_steeltank { get; set; }
		//public decimal mf2_pucca_covered { get; set; }
		//public decimal mf2_pucca_uncovered { get; set; }
		//public decimal mf2_katcha_uncovered { get; set; }
		//public decimal mf2_katcha_covered { get; set; }
		//public decimal mf2_new_storage { get; set; }
		//public decimal mf2_steeltank_4 { get; set; }
		//public decimal mf2_pucca_covered_4 { get; set; }

		//public decimal mf2_pucca_uncovered_4 { get; set; }
		//public decimal mf2_katcha_uncovered_4 { get; set; }
		//public decimal mf2_katcha_covered_4 { get; set; }

		//public string mf2_loading { get; set; }
		//public int mf2_tanks_load { get; set; }

		//public decimal mf2_captive_delivery { get; set; }
		//public decimal mf2_distillary_delivery { get; set; }
		//public decimal mf2_others_delivery { get; set; }
		//public decimal mf2_gatesale_delivery { get; set; }
		//public decimal mf2_total_delivery { get; set; }
		//public decimal mf2_steeltank_9 { get; set; }
		//public decimal mf2_pucca_covered_9 { get; set; }
		//public decimal mf2_pucca_uncovered_9 { get; set; }

		//public string mf2_stock_clearance { get; set; }
		//public string mf2_arrangement { get; set; }
		//public string mf2_storageissue { get; set; }
		//public string mf2_repair { get; set; }
		//public string mf2_address_owner { get; set; }
		//public string mf2_address_occupier { get; set; }
		//public string mf2_address_manager { get; set; }
		//public string mf2_mechanicalpump { get; set; }
		//public DateTime mf2_create_date { get; set; }
		//public DateTime mf2_lastupdated { get; set; }
		//public string mf2_record_status { get; set; }
		//public string mf2_attachment { get; set; }
		//public string mf2_tobe_alloted_total { get; set; }

		//public decimal mf2_alloted_total { get; set; }
		//public int mf2_product_master_sno { get; set; }
		//public MF2_add MF2_add { get; set; }
		//public string mf2_wagons_load { get; set; }

		public int mf2_seq_no { get; set; }
		public DateTime mf2_date { get; set; }
		public string mf2_producer { get; set; }
		public string mf2_fiscal_year { get; set; }
		public string mf2_status { get; set; }
		public string mf2_captive { get; set; }
		public DateTime mf2_startcrush { get; set; }
		public decimal mf2_plan_molasses_season { get; set; }
		public string mf2_plan_molasses_daily { get; set; }
		public string mf2_plan_sugar_daily { get; set; }
		public decimal mf2_plan_sugar_season { get; set; }
		public decimal mf2_total_storage { get; set; }
		public decimal mf2_steeltank_4 { get; set; }
		public decimal mf2_pucca_covered_4 { get; set; }
		public decimal mf2_pucca_uncovered_4 { get; set; }
		public decimal mf2_katcha_uncovered_4 { get; set; }
		public decimal mf2_katcha_covered_4 { get; set; }
		public string mf2_loading { get; set; }
		public string mf2_tanks_load { get; set; }
		public string mf2_stock_clearance { get; set; }
		public string mf2_arrangement { get; set; }
		public string mf2_storageissue { get; set; }
		public string mf2_repair { get; set; }
		public string mf2_address_owner { get; set; }
		public string mf2_address_occupier { get; set; }
		public string mf2_address_manager { get; set; }
		public string mf2_mechanicalpump { get; set; }
		public DateTime mf2_create_date { get; set; }
		public DateTime mf2_lastupdated { get; set; }
		public string mf2_record_status { get; set; }
		public string mf2_attachment { get; set; }
		public decimal mf2_tobe_alloted_total { get; set; }
		public decimal mf2_alloted_total { get; set; }
		public string mf2_captive_unitname { get; set; }
		public string mf2_allocation_type { get; set; }
		public int mf2_product_master_sno { get; set; }
		public string mf2_wagons_load { get; set; }
		public MF2_add MF2_add { get; set; }
	}

	public class MF2ViewModel
	{
		public int mf2_seq_no { get; set; }
		public DateTime mf2_date { get; set; }
		public string mf2_producer { get; set; }
		public string mf2_fiscal_year { get; set; }
		public decimal mf2_alloted_total { get; set; }
		public decimal mf2_production_total { get; set; }
		public string viewColumn { get; set; }
	}

	public class MF2_add
	{
		public int mf2_add_seq { get; set; }
		public int mf2_fk_seq { get; set; }
		public string y1 { get; set; }
		public decimal actual_prod_y1 { get; set; }
		public decimal final_prod_y1 { get; set; }
		public decimal gatesale_delivery__y1 { get; set; }
		public string y2 { get; set; }
		public decimal actual_prod_y2 { get; set; }
		public decimal final_prod_y2 { get; set; }
		public string y3 { get; set; }
		public decimal actual_prod_y3 { get; set; }
		public decimal final_prod_y3 { get; set; }
		public DateTime mf2_add_create_date { get; set; }
		public DateTime mf2_add_lastupdated { get; set; }
		public string  mf2_add_record_status { get; set; }
		public decimal y1_total { get; set; }
		public decimal y2_total { get; set; }
		public decimal y3_total { get; set; }
		public decimal mf2_add_captive_delivery_1 { get; set; }
		public decimal mf2_add_distillary_delivery_1 { get; set; }
		public decimal mf2_add_others_delivery_1 { get; set; }
		public decimal mf2_add_gatesale_delivery_1 { get; set; }
		public decimal mf2_add_total_delivery_1 { get; set; }
		public decimal mf2_add_captive_delivery_2 { get; set; }
		public decimal mf2_add_distillary_delivery_2 { get; set; }
		public decimal mf2_add_others_delivery_2 { get; set; }
		public decimal mf2_add_gatesale_delivery_2 { get; set; }
		public decimal mf2_add_total_delivery_2 { get; set; }
		public decimal mf2_add_captive_delivery_3 { get; set; }
		public decimal mf2_add_distillary_delivery_3 { get; set; }
		public decimal mf2_add_others_delivery_3 { get; set; }
		public decimal mf2_add_gatesale_delivery_3 { get; set; }
		public decimal mf2_add_total_delivery_3 { get; set; }
	}

	public class MF2Material
	{
		public int s_no { get; set; }
		public string product_name { get; set; }
	}

	public class MF2Party_master
	{
		public int ps_no { get; set; }
		public string cap_unit_name { get; set; }
	}

	public class MF2StorageVatMaster
	{
		public int s_no { get; set; }
		public string storage_name { get; set; }
		public decimal capacity { get; set; }
	}

	public class mf2_q_3
	{
		public int mf2_q_3_seq { get; set; }
		public int mf2_seq_no { get; set; }
		public string mf2_vat_material { get; set; }
		public string vat_lable { get; set; }
		public decimal vat_qty { get; set; }
		public decimal total_vat_qty { get; set; }
		public decimal mf2_pucca_covered { get; set; }
		public decimal mf2_pucca_uncovered { get; set; }
		public decimal mf2_katcha_uncovered { get; set; }
		public decimal mf2_katcha_covered { get; set; }
		public DateTime mf2_q_7b_lastupdated { get; set; }
	}

	public class mf2_q_7b
	{


		public int mf2_q_7b_seq { get; set; }
		public int mf2_seq_no { get; set; }
		public int y1 { get; set; }
		public string distillary_1 { get; set; }
		public decimal supply_qty_1 { get; set; }
		public decimal cum_qty_1 { get; set; }
		public int y2 { get; set; }
		public string distillary_2 { get; set; }
		public decimal supply_qty_2 { get; set; }
		public decimal cum_qty_2 { get; set; }
		public int y3 { get; set; }
		public string distillary_3 { get; set; }
		public decimal supply_qty_3 { get; set; }
		public decimal cum_qty { get; set; }
		public DateTime mf2_q_7b_lastupdated { get; set; }
	}

	public class mf2_q_7c
	{
		public int mf2_q_7c_seq { get; set; }
		public int mf2_seq_no { get; set; }
		public int y1 { get; set; }
		public string others_1 { get; set; }
		public decimal supply_qty_1 { get; set; }
		public decimal cum_qty_1 { get; set; }
		public int y2 { get; set; }
		public string others_2 { get; set; }
		public decimal supply_qty_2 { get; set; }
		public decimal cum_qty_2 { get; set; }
		public int y3 { get; set; }
		public string others_3 { get; set; }
		public decimal supply_qty_3 { get; set; }
		public decimal cum_qty { get; set; }
		public DateTime mf2_q_7b_lastupdated { get; set; }
	}

	public class mf2_vat9
	{
		public int mf2_vat9_seq_no { get; set; }
		public int mf2_seq_no { get; set; }
		public string mf2_vat_material { get; set; }
		public int y1 { get; set; }
		public string vat_lable_y1 { get; set; }
		public decimal vat_qty_y1 { get; set; }
		public decimal total_vat_qty_y1 { get; set; }
		public int y2 { get; set; }
		public string vat_labley2 { get; set; }
		public decimal vat_qty_y2 { get; set; }
		public decimal total_vat_qty_y2 { get; set; }
		public int y3 { get; set; }
		public string vat_lable_y3 { get; set; }
		public decimal vat_qty_y3 { get; set; }
		public decimal total_vat_qty_y3 { get; set; }
		public DateTime mf2_vat7_lastupdated { get; set; }
	}
}