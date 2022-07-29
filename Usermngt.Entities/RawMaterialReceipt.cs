using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class RawMaterialReceipt
    {
        public string party_code { get; set;}
        public string party_name { get; set;}
        public string rmr_entrydate { get; set; }
        public double passqty { get; set; }
        public string vehicleno { get; set;}
        public string passno { get; set; }
        public string rmrpassno { get; set; }
        public string financial_year { get; set; }
        public double grossweight { get; set;}
        public double tankerweight { get; set;}
        public double transitweight { get; set;}
        public double supplierweight { get; set;}
        public string passissuedate { get; set;}
        public double netweight { get; set; }
        public string user_id { get; set;}
        public string remarks { get; set; }
        public string creation_date { get; set; }
        public string rawmaterial_receipt_id { get; set; }
        public string vat_code { get; set; }
        public string vat_name { get; set; }
        public double storedqty { get; set; }
        public string record_status { get; set; }
        public string digilocker { get; set; }
        public string rmstorageid { get; set; }
        public double vat_totalcapacity { get; set;}
        public double opening_dips { get; set; }
        public string product_type_name { get; set; }
        public string product_type_code { get; set; }
        public string supplier { get; set; }
        public string rawmaterial { get; set; }
        public string suppliertype { get; set; }
        public string suppliername { get; set; }
        public string uom { get; set; }

        public List<RMR_Store> store { get; set; }

    }
}
