using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_StorageToDispatch
    {

        public static string Insert(StorageToDispatch frm84D)
        {
            return DL_StorageToDispatch.Insert(frm84D);
        }

        public static string Update(StorageToDispatch frm84D)
        {
            return DL_StorageToDispatch.Update(frm84D);
        }

        public static List<StorageToDispatch> GetList()
        {
            return DL_StorageToDispatch.GetList();
        }
        public static List<StorageToDispatch> Search(string tablename, string column, string value)
        {
            return DL_StorageToDispatch.Search(tablename, column, value);
        }

        public static StorageToDispatch GetDetails(string party_code, string form84Did,string financial_year)
        {
            return DL_StorageToDispatch.GetDetails(party_code, form84Did,financial_year);
        }

        public static string Approve(StorageToDispatch frm84D)
        {
            return DL_StorageToDispatch.Approve(frm84D);
        }

        public static List<StorageToDispatch> GetDEN(string party_code, string date)
        {
            return DL_StorageToDispatch.GetDEN(party_code, date);
        }

        public static StorageToDispatch GetDispatchqty(string party_code, string date, string vat_code)
        {
            return DL_StorageToDispatch.GetDispatchqty( party_code, date,vat_code);
        }

        public static List<StorageToDispatch> GetsubmitedVat(string party_code, String date)
        {
            return DL_StorageToDispatch.GetsubmitedVat(party_code, date);
        }
        public static List<StorageToDispatch> GetDENList1(string party_code, string date)
        {
            return DL_StorageToDispatch.GetDENList1(party_code, date);
        }
        public static List<StorageToDispatch> GetDENList(string party_code,string date,string vatcode)
        {
            return DL_StorageToDispatch.GetDENList(party_code,date,vatcode);
        }
    }
}
