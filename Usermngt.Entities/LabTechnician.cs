using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
    public class LabTechReportContext
    {
        public int lab_report_id { get; set; }
        public int receiving_section_id { get; set; }
        public string smell { get; set; }

        public string proofstrength1 { get; set; }

        public string proofstrength2 { get; set; }

        public string color { get; set; }

        public string remarks1 { get; set; }

        public string deviceused { get; set; }

        public string hydrometerindication { get; set; }

        public string hydrometertempearature { get; set; }

        public string pyknomenterweight { get; set; }

        public string dmpyknometerweight { get; set; }

        public string samplepyknometerweight { get; set; }

        public string pyknomentertemperature { get; set; }

        public bool? passtestaceticacid { get; set; }

        public bool? passtestresidue { get; set; }

        public bool? passtestmethylalcohol { get; set; }

        public bool? passtestamylalcohol { get; set; }
        public bool? passtestfurfural { get; set; }

        public bool? passtestethylacetate { get; set; }
        public bool? passtestcopper { get; set; }

        public bool? passtestaldehyes { get; set; }

        public bool? passtestbyvalueaceticacid { get; set; }

        public bool? passtestbyvalueresidue { get; set; }
        public bool? passtestbyvaluemethylalcohol { get; set; }

        public bool? passtestbyvalueamylalcohol { get; set; }
        public bool? passtestbyvaluefurfural { get; set; }

        public bool? passtestbyvalueethylacetate { get; set; }

        public bool? passtestbyvaluecopper { get; set; }

        public bool? passtestbyvaluealdehyes { get; set; }
        public bool? testresult { get; set; }

        public string remarks2  { get;set;}

        public string status { get; set; }

        public DateTime? created_on { get; set; }
        public string created_by { get; set; }
        public DateTime? updated_on { get; set; }
        public string updated_by { get; set; }


    }


    public class LabTechReport
    {
        public int lab_report_id { get; set; }
        public int receiving_section_id { get; set; }
        public string exhibit_from { get; set; }
        public string type_of_liquor_name { get; set; }
        public string liquor_sub_name { get; set; }
        public string size_master_name { get; set; }
        public string quantity { get; set; }
        public string brand_master_name { get; set; }
        public string batch_no { get; set; }
        public string address { get; set; }
        public string status { get; set; }

        public string remark { get; set; }

        public string status_text
        {
            get
            {
                return string.IsNullOrWhiteSpace(status) ? "untested" : this.status;
            }
        }
        public DateTime receiving_date { get; set; }
        public string letter_no { get; set; }
        public DateTime? letter_date { get; set; }
        public int compactor_id { get; set; }

        
    }

    public class LabTechnicianList
    {
        public int lab_report_id { get; set; }
        public int receiving_section_id { get; set; }
        public string exhibit_from { get; set; }
        public string type_of_liquor_name { get; set; }
        public string liquor_sub_name { get; set; }
        public string size_master_name { get; set; }
        public string quantity { get; set; }
        public string brand_master_name { get; set; }
        public string batch_no { get; set; }
        public string address { get; set; }
        public string compactor_name { get; set; }
        public string status { get; set; }

        public string status_text
        {
            get
            {
                return  string.IsNullOrWhiteSpace(status) ? "untested" : this.status;
            }
        }

    }
}
