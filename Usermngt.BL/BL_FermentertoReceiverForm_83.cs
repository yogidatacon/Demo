using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_FermentertoReceiverForm_83
    {
        public static List<FermentertoReceiverForm_83> GetList()
        {
            return DL_FermentertoReceiverForm_83.GetList();
        }
        public static List<FermentertoReceiverForm_83> Search(string tablename, string column, string value)
        {
            return DL_FermentertoReceiverForm_83.Search(tablename, column, value);
        }

        //public static string GetExistsData(string  vat_code,string party_code)
        //{
        //    return DL_FermentertoReceiverForm_83.GetVatData(vat_code,party_code);
        //}

        public static string Insert(FermentertoReceiverForm_83 from83)
        {
            return DL_FermentertoReceiverForm_83.Insert(from83);
        }

        public static string Update(FermentertoReceiverForm_83 from83)
        {
            return DL_FermentertoReceiverForm_83.Update(from83);
        }

        public static FermentertoReceiverForm_83 GetDetails(int fermenterreceiverid, string party_Code,string financial_year)
        {
            return DL_FermentertoReceiverForm_83.GetDetails(fermenterreceiverid, party_Code,financial_year);
        }

        public static string Approve(FermentertoReceiverForm_83 record)
        {
            return DL_FermentertoReceiverForm_83.Approve(record);
        }
        public static FermentertoReceiverForm_83 GetVatAval(string Vat_code, string party_Code, string date,int id)
        {
            return DL_FermentertoReceiverForm_83.GetVatAval(Vat_code, party_Code, date,id);
        }
            //public static List<FermentertoReceiverForm_83> GetReceiverVATList(string party_code)
            //{
            //    return DL_FermentertoReceiverForm_83.GetReceiverVATList(party_code);
            //}
        }
}
