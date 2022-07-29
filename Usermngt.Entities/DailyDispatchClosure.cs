using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
    public class DailyDispatchClosure
    {
        public int dailydispatchclosure_id { get; set; }
        public string closure_date { get; set; }
        public string party_code { get; set; }
        public string party_name { get; set; }
        public string financial_year { get; set; }
        public string from_dispatchvat { get; set; }
        public double dispatchqty { get; set; }
        public double dec_reduction { get; set; }
        public double dec_blending { get; set; }
        public double dec_racking { get; set; }
        public double dec_wastage { get; set; }
        public double dips { get; set; }
        public double temperature { get; set; }
        public double indication { get; set; }
        public double strength { get; set; }
        public string remarks { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string vat_code { get; set; }
        public string vat_name { get; set; }
        public double vat_availablecapacity { get; set; }
        public string record_status { get; set; }
        public double bl_balanceqty { get; set; }
        public double lp_balanceqty { get; set; }
        public double IncreaseBLByGroging { get; set; }
        public double txtIncreaseBLInOperation { get; set; }
    }
}
