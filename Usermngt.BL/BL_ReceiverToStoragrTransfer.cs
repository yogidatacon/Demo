using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
   public class BL_ReceiverToStoragrTransfer
    {

        public static string Insert(ReceiverToStoragrTransfer form84)
        {
            return DL_ReceiverToStoragrTransfer.Insert(form84);
        }

        public static string Update(ReceiverToStoragrTransfer form84)
        {
            return DL_ReceiverToStoragrTransfer.Update(form84);
        }

        public static ReceiverToStoragrTransfer GetVatData(string vat_code, string party_code)
        {
            return DL_ReceiverToStoragrTransfer.GetVatData(vat_code, party_code);
        }

        public static List<ReceiverToStoragrTransfer> GetList()
        {
            return DL_ReceiverToStoragrTransfer.GetList();
        }
        public static List<ReceiverToStoragrTransfer> Search(string tablename, string column, string value)
        {
            return DL_ReceiverToStoragrTransfer.Search(tablename, column, value);
        }
        public static ReceiverToStoragrTransfer GetDetails(string party_code, string form84id,string financial_year)
        {
            return DL_ReceiverToStoragrTransfer.GetDetails(party_code, form84id,financial_year);
        }

        public static string Approve(ReceiverToStoragrTransfer form84)
        {
            return DL_ReceiverToStoragrTransfer.Approve(form84);
        }

        public static List<ReceiverToStoragrTransfer> GetDENvat(int TransferId)
        {
            return DL_ReceiverToStoragrTransfer.GetDENvat(TransferId);
        }

        public static ReceiverToStoragrTransfer GetBLLPquty(string vat_code, string party_code, int transferid)
        {
            return DL_ReceiverToStoragrTransfer.GetBLLPquty(vat_code, party_code,transferid);
        }

        public static List<ReceiverToStoragrTransfer> GetTransferdate(string party_code)
        {
            return DL_ReceiverToStoragrTransfer.GetTransferdate(party_code);
        }

        public static List<ReceiverToStoragrTransfer> GetSubmiteddate(string party_code, int transferid)
        {
            return DL_ReceiverToStoragrTransfer.GetSubmiteddate(party_code, transferid);
        }
    }
}
