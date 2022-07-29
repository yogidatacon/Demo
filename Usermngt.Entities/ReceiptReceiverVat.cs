using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class ReceiptReceiverVat
    {
        public int receiver_storage_receiptvat_id { get; set; }
        public int receiver_storage_receipt_id { get; set; }
        public double bl_receipt { get; set; }
        public double lp_receipt { get; set; }
        public string from_receivervat { get; set; }

    }
}
