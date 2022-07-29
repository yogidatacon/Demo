using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class Indent_Form
    {
        public string molasses_indent_id { get; set; }
        public string molasses_indent_reqno { get; set; }
        public string record_id_format { get; set; }
        public string indent_date { get; set; }
        public double indent_qty { get; set; }
        public string financial_year { get; set; }
        public string is_captive { get; set; }
        public string captive_unit_name { get; set; }
        public string party_code { get; set; }
        public string party_name { get; set; }
        public string product_code { get; set; }
        public string product_name { get; set; }
        public double req_cs { get; set; }
        public double req_rs { get; set; }
        public double req_pa { get; set; }
        public double req_ds { get; set; }
        public double recd_pycs { get; set; }
        public double recd_pyrs { get; set; }
        public double recd_pypa { get; set; }
        public double recd_pyds { get; set; }
        public double used_pycs { get; set; }
        public double used_pyrs { get; set; }
        public double used_pypa { get; set; }
        public double used_pyds { get; set; }
        public double used_cycs { get; set; }
        public double used_cyrs { get; set; }
        public double used_cypa { get; set; }
        public double used_cyds { get; set; }
        public double molasses_distilled { get; set; }
        public double working_wastage { get; set; }
        public double transit_wastage { get; set; }
        public double molasses_instock_cy { get; set; }
        public double molasses_recd_cy { get; set; }
        public double molasses_used_cy { get; set; }
        public double molasses_to_lift { get; set; }
        public double molasses_to_consume { get; set; }
        public double molasses_instock { get; set; }
        public double molasses_to_be_lifted { get; set; }
        public double molasses_bal_allotment { get; set; }
        public double molasses_bal_storage { get; set; }
        public double molasses_allocated_qty { get; set; }
        public double molasses_lifted_qty { get; set; }
        public string user_id { get; set; }
        public string record_status { get; set; }
        public string record_active { get; set; }
        public string alloted_no { get; set; }
        public List<EASCM_DOCS> docs { get; set; }

    }
}
