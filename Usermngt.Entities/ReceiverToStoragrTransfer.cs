using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class ReceiverToStoragrTransfer
    {
        public int receiver_storage_transfer_id { get; set; }
        public string transfer_date { get; set; }
        public string transfer_time { get; set; }
        public string financial_year{ get; set; }
        public string party_code { get; set; }
        public string party_name { get; set; }
        public string production_date { get; set; }
        public string to_storagevat { get; set; }
        public double total_bl_transfer { get; set; }
        public double total_lp_transfer { get; set; }
        public double inc_operation { get; set; }
        public double inc_groging { get; set; }
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
        public string record_status { get; set; }
        public string record_deleted { get; set; }
        public string fromstoragevat { get; set; }
        public string moved_to_nextstage { get; set; }
        public string storagevat { get; set; }
        public double vat_availablecapacity { get; set; }
        public string todispatchvat { get; set; }
        public string dispatchvat { get; set; }

    }
}
