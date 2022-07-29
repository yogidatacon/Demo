using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class FermentertoReceiverForm_83
    {
        public int fermenter_receiver_id { get; set; }
        public string gauged_date { get; set; }
        public string distillation_date { get; set; }
        public string distillation_id { get; set; }
        public string financial_year { get; set; }
        public double total_input_bl_qty { get; set; }
        public double total_input_lp_qty { get; set; }
        public double total_output_bl_qty { get; set; }
        public double total_output_lp_qty { get; set; }
        public string party_code { get; set; }
        public string party_name { get; set; }
        public int no_of_receiver { get; set; }
        public string from_fermentervat { get; set; }
        public string fermentervat { get; set; }
        public string receivervat { get; set; }
        public double transferqty { get; set; }
        public string to_receivervat { get; set; }
        public double bl_distillation { get; set; }
        public double redistillation_bl_qty { get; set; }
        public double redistillation_lp_qty { get; set; }
        public string to_which_still { get; set; }
        public string removal_date { get; set; }
        public string remarks { get; set; }
        public string user_id { get; set; }
        public string record_status { get; set; }
        public string record_deleted { get; set; }
        public string approval_status { get; set; }
        public List<FReceiverInput> ReceiverInput { get; set; }
        public List<FReceiverOuput> ReceiverOutput { get; set; }

    }
}
