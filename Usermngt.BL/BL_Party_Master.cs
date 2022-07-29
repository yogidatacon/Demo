using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
   public class BL_Party_Master
    {
        public static string Insert(Party_Master partytype)
        {
            return DL_Party_Master.Insert(partytype);
        }
        public static string Update(Party_Master partytype)
        {
            return DL_Party_Master.Update(partytype);
        }
        public static List<Party_Master> SearchParty(string tablename, string column, string value)
        {
            return DL_Party_Master.SearchParty(tablename, column, value);
        }
        public static List<Party_Master> GetList()
        {
            return DL_Party_Master.GetList();
        }
        public static Party_Master GetPartyDetails(string party_code)
        {
            return DL_Party_Master.GetPartyDetails(party_code);
        }
        public static string changefinancialyear(string tablename, string column, string value, string financialendate, string startdates)
        {
            return DL_Party_Master.changefinancialyear(tablename, column, value, financialendate,startdates);
        }
        public static string Approve(Party_Master DDC)
        {
            return DL_Party_Master.Approve(DDC);
        }
        public static Party_Master Checkparty(string party_code)
        {
            return DL_Party_Master.Checkparty(party_code);
        }
        public static List<Party_Master> GetProduct_Party()
        {
            return DL_Party_Master.GetProduct_Party();
        }
    }
}
