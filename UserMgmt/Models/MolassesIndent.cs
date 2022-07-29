using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseMgmt.Models
{
	public class MolassesIndent
	{
		public decimal mf1_indentor_qty { get; set; }
		public int mf1s_no { get; set; }
		public DateTime mf1_date { get; set; }
		public string mf1_indentor { get; set; }
		public string mf1_fiscal_year { get; set; }
		public string mf1_status { get; set; }
		public string mf1_captive { get; set; }
		public string mf1_captive_unitname { get; set; }
		public string mf1_ind_material { get; set; }
		public decimal mf1_qty_req_cs { get; set; }
		public decimal mf1_qty_req_rs { get; set; }
		public decimal mf1_qty_req_pa { get; set; }
		public decimal mf1_qty_req_ds { get; set; }
		public decimal mf1_qty_req_total { get; set; }
		public decimal mf1_qty_prevyear_cs { get; set; }
		public decimal mf1_qty_prevyear_rs { get; set; }
		public decimal mf1_qty_prevyear_pa { get; set; }
		public decimal mf1_qty_prevyear_ds { get; set; }
		public decimal mf1_qty_prevyear_total { get; set; }
       

        public decimal mf1_qty_curryear_cs { get; set; }
		public decimal mf1_qty_curryear_rs { get; set; }
		public decimal mf1_qty_curryear_pa { get; set; }
		public decimal mf1_qty_curryear_ds { get; set; }
		public decimal mf1_qty_curryear_total { get; set; }

		public decimal mf1_stock { get; set; }
		public decimal mf1_received { get; set; }
		public decimal mf1_total { get; set; }
		public decimal mf1_qty_used { get; set; }
		public decimal mf1_qty_to_lift { get; set; }
		public decimal mf1_to_consume { get; set; }
		public decimal mf1_balance { get; set; }
		public DateTime create_date { get; set; }
		public DateTime lastupdated { get; set; }
		public string record_status { get; set; }
		public decimal mf1_allocation_qty { get; set; }
		public string mf1_allocation_type { get; set; }
		public string mf1_attachment { get; set; }
		public string allocationno { get; set; }
		public string extno { get; set; }
		public decimal mf1_tobeallocated_qty { get; set; }


		public decimal mf1_qty_prevyearusage_cs { get; set; }
		public decimal mf1_qty_prevyearusage_rs { get; set; }
		public decimal mf1_qty_prevyearusage_pa { get; set; }
		public decimal mf1_qty_prevyearusage_ds { get; set; }
		public decimal mf1_qty_prevyearusage_total { get; set; }

		public decimal mf1_molasses_distiled { get; set; }
		public decimal mf1_working_wastage { get; set; }
		public decimal mf1_transit_wastage { get; set; }

		public decimal mf_1_qty_tobeconsumed { get; set; }
		public decimal mf_1_qty_instock { get; set; }
		public decimal mf_1_qty_tobelifted { get; set; }		
		public decimal mf_1_total_9 { get; set; }
		public decimal mf_1_balanceonallotment { get; set; }

	}
	public class FiscalYearModel
	{
		public string FiscalYear { get; set; }
	}

	public class MF1Attachment
	{
		public string File { get; set; }
	}

	public class IndentFormSlNo
	{
		public string SlNo { get; set; }
	}
	public class MolassesIndentViewModel
	{
		public int mf1s_no { get; set; }
		public string mf1_fiscal_year { get; set; }
		public decimal mf1_qty_req_total { get; set; }
		public decimal mf1_qty_to_lift { get; set; }
		public decimal mf1_allocation_qty { get; set; }
		public string mf1_allocation_no { get; set; }
		public string extno { get; set; }
		public string viewColumn { get; set; }
	}

	public class MF1FormViewModel
	{
		
		public int mf1s_no { get; set; }
		public string mf1_allocation_type { get; set; }		
		public string allotmentno { get; set; }
		public string mf1_indentor { get; set; }
		public string mf1_fiscal_year { get; set; }
		public decimal mf1_qty_req_total { get; set; }		
		public decimal mf1_allocation_no { get; set; }		
		public string mf1_captive_unitname { get; set; }
		
	}
}