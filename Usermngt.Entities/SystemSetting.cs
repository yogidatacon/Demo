using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
   public class SystemSetting
    {
        public int system_setting_id { get; set; }
        public string parameter_value_str { get; set; }
        public string parameter_name { get; set; }
        public int parameter_value_num { get; set; }
        public  string user_id { get; set; }
    }
}
