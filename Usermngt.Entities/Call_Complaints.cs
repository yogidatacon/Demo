using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities.CaseMgmt
{
   public class Call_Complaints
    {
        public  string comid { get; set; }
        public string v_issue { get; set; }
        public string comsource { get; set; }
        public string comname { get; set; }
        public string phone { get; set; }
        public string accperson { get; set; }
        public string nearby { get; set; }
        public string complaintstatus { get; set; }
        public string record_status { get; set; }
        public string userid { get; set; }
        public string seizureno { get; set; }
        public string prfirno { get; set; }
        public string thana_mst_code { get; set; }
    }
}
