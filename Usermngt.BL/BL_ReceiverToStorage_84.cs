using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_ReceiverToStorage_84
    {
        public static string Insert(ReceiverToStorage_84 form84)
        {
            return DL_ReceiverToStorage_84.Insert(form84);
        }
        public static ReceiverToStorage_84 GetVatAval(string Vat_code, string party_Code, string date, string time,string financial_year)
        {
            return DL_ReceiverToStorage_84.GetVatAval(Vat_code, party_Code, date, time,financial_year);
        }
        public static string Update(ReceiverToStorage_84 form84)
        {
            return DL_ReceiverToStorage_84.Update(form84);
        }
        public static List<ReceiverToStorage_84> GetList()
        {
            return DL_ReceiverToStorage_84.GetList();
        }
        public static List<ReceiverToStorage_84> Search(string tablename, string column, string value)
        {
            return DL_ReceiverToStorage_84.Search(tablename, column, value);
        }
        public static ReceiverToStorage_84 GetDetails(string party_code, string form84id,string financial_year)
        {
            return DL_ReceiverToStorage_84.GetDetails(party_code, form84id,financial_year);
        }

        public static string GetExistsData(string vat,string party_code)
        {
            return DL_ReceiverToStorage_84.GetVatData(vat, party_code);
        }

        public static List<FReceiverOuput> GetListreceiverop(string party_code)
        {
          return  DL_ReceiverToStorage_84.GetListreceiverop(party_code);
        }

        public static List<FReceiverOuput> GetStoragevat(string party, string date, string financial_year)
        {
            return DL_ReceiverToStorage_84.GetStoragevat(party,date,financial_year);
        }
        public static List<FReceiverInput> GetSTOVAt(int id ,string vat_code, string financial_year)
        {
            return DL_ReceiverToStorage_84.GetSTOVAt(id,vat_code,financial_year);
        }
        public static List<FReceiverOuput> GetReceiverVAtdetails(string vat_code, int ID)
        {
            return DL_ReceiverToStorage_84.GetReceiverVAtdetails(vat_code,ID);
        }
        public static List<FReceiverInput> GetReceiverVAt(string vat_code, string date, string party, string financial_year)
        {
            return DL_ReceiverToStorage_84.GetReceiverVAt(vat_code,date,party,financial_year);
        }
        public static List<FReceiverInput> GetdistinctVAt(string vat_code, string date, string party, string financial_year)
        {
            return DL_ReceiverToStorage_84.GetdistinctVAt(vat_code, date, party,financial_year);
        }
        public static List<FReceiverInput> GetReceiverVAt1(string vat_code, string date, string party,string financial_year)
        {
            return DL_ReceiverToStorage_84.GetReceiverVAt1(vat_code, date, party,financial_year);
        }
        public static List<FReceiverInput> GetReceiver(int id, string financial_year)
        {
          return  DL_ReceiverToStorage_84.GetReceiver(id,financial_year);
        }
        public static string Approve(ReceiverToStorage_84 form84)
        {
            return DL_ReceiverToStorage_84.Approve(form84);
        }
        public static FReceiverOuput Gettotal(string vat_code, string userid, string date,string financial_year)
        {
            return DL_ReceiverToStorage_84.Gettotal(vat_code, userid, date, financial_year);
        }
        public static List<FReceiverOuput> Getsubmiteddate(string party_code)
        {
            return DL_ReceiverToStorage_84.Getsubmiteddate(party_code);
        }
        public static List<FReceiverInput> GetReceiverVAtvalue(string vat_code, string userid, string date, string financial_year)
        {
            return DL_ReceiverToStorage_84.GetReceiverVAtvalue(vat_code, userid,date,financial_year);
        }
    }
}
