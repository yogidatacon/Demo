using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
    public class cm_seiz_CurrencyCoins
    {
        public int seizure_currencycoins_id { set; get; }
        public int seizure_moneydetails_id { set; get; }
        public string seizureno { set; get; }
        public string money_type { set; get; }
        public string currency { set; get; }
        public string noofpieces { set; get; }
        public string amount { set; get; }
        public string lastmodified_date { set; get; }
        public string user_id { set; get; }
        public string creation_date { set; get; }
        public string record_deleted { set; get; }
        public string record_status { set; get; }
    }
}
