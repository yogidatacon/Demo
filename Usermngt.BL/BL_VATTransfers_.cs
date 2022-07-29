using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_VATTransfers_
    {
        
        public static string Insert(VATTransfers_ vat)
        {
            return DL_VATTransfers_.Insert(vat);
        }

        public static string Update(VATTransfers_ vat)
        {
            return DL_VATTransfers_.Update(vat);
        }

        public static List<VATTransfers_> GetList(string party_code,string party)
        {
            return DL_VATTransfers_.GetList(party_code,party);
        }
        public static List<VATTransfers_> Search(string tablename, string column, string value, string party_code, string party)
        {
            return DL_VATTransfers_.Search(tablename, column, value, party_code, party);
        }

        public static string Approve(VATTransfers_ vat)
        {
            return DL_VATTransfers_.Approve(vat);
        }
    }
}
