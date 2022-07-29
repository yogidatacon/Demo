using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseMgmt.Models
{
	public class MF_Allotment
	{
		public int a_seq { get; set; }
		public string a_fiscal_year { get; set; }
		public string a_number { get; set; }
		public DateTime a_date { get; set; }
		public DateTime a_from_date { get; set; }
		public DateTime a_end_date { get; set; }
		public int a_ext_no { get; set; }
		public string a_user_id { get; set; }
		public string a_ind_material { get; set; }
		public string a_type { get; set; }
		public string a_status { get; set; }
		public string a_sugar { get; set; }
		public string a_iscustomer_captive { get; set; }
		public string a_distillary { get; set; }
		public decimal a_qty_req_total { get; set; }
		public decimal a_plan_produced { get; set; }
		public decimal a_final_produced { get; set; }
		public decimal a_qty { get; set; }
		public decimal a_qty_tobeallocated { get; set; }
		public DateTime a_create_date { get; set; }
		public DateTime a_lastupdated { get; set; }
		public decimal a_lifted { get; set; }
		public string a_record_status { get; set; }		
		public decimal a_balance_qty { get; set; }
		public int max_rr { get; set; }		
	}

	public class MF_AllotmentViewModel
	{
		public int mf1_seq_no { get; set; }		
		public string mf1_indentor { get; set; }
		public string mf1_fiscal_year { get; set; }
		public decimal mf1_qty_req_total { get; set; }
		public decimal Alloted { get; set; }
		public string mf1_status { get; set; }
		public string mf1_allocation_no { get; set; }		
		public string viewColumn { get; set; }
	}
}