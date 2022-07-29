using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_VATMaster
    {
        public static List<VAT_Master> GetvatmasterList(string userid)
        {
            return DL_VATMaster.GetvatmasterList(userid);
        }
        public static List<VAT_Master> GetGrainvatmasterList(string userid)
        {
            return DL_VATMaster.GetGrainvatmasterList(userid);
        }

        public static string Update(VAT_Master vat)
        {
            return DL_VATMaster.Update(vat);
        }
        public static List<VAT_Master> SearchVATMaster(string tablename, string column, string value)
        {
            return DL_VATMaster.SearchVATMaster(tablename, column, value);
        }
        public static string Insert(VAT_Master vat)
        {
            return DL_VATMaster.Insert(vat);
        }
        public static VAT_Master GetVATDetails(string vat_code)
        {
            return DL_VATMaster.GetVATDetails(vat_code);
        }
    }
}
