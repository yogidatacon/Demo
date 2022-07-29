using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_VatType
    {
        public static List<VatType> GetListValue(string userid)
        {
            return DL_VatType.GetVatTypeList(userid);
        }
        public static bool InsertVat(VatType vat)
        {
            return DL_VatType.InserVatType(vat);
        }

        public static List<VatType> SearchVatType(string tablename, string column, string value)
        {
            return DL_VatType.SearchVatType(tablename, column, value);
        }
        public static bool UpdateVat(VatType vat)
        {
            return DL_VatType.UpdateVat(vat);
        }
    }
}
