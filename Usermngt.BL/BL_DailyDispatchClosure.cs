using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
   public class BL_DailyDispatchClosure
    {
        public static List<DailyDispatchClosure> GetDispatch()
        {
            return DL_DailyDispatchClosure.GetDispatchClosure();
        }
        public static List<DailyDispatchClosure> Search(string tablename, string column, string value)
        {
            return DL_DailyDispatchClosure.Search(tablename, column, value);
        }

        public static string InsertDispatchClosure(DailyDispatchClosure dispatch)
        {
            return DL_DailyDispatchClosure.InsertDispatch(dispatch);
        }
        public static string UpdateDispatchClosure(DailyDispatchClosure dispatch)
        {
            return DL_DailyDispatchClosure.UpdateDispatch(dispatch);
        }

        public static List<VAT_Master> GetVatName(string userid)
        {
            return DL_DailyDispatchClosure.GetVat(userid);
        }

        public static List<VAT_Master> GetVatAvlQty(string vatcode)
        {
            return DL_DailyDispatchClosure.GetVatAvilQty(vatcode);
        }

        public static DailyDispatchClosure GetDetails( string party_code,int dailydispatchclosure_id,string financial_year)
        {
            return DL_DailyDispatchClosure.GetDetails(party_code,dailydispatchclosure_id,financial_year);
        }

        public static string Approve(DailyDispatchClosure DDC)
        {
            return DL_DailyDispatchClosure.Approve(DDC);
        }
    }
}
