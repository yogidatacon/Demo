using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
 public   class BL_DailyMolassesProduction
    {
        public static List<DailyMolassesProduction_e> GetProductionList(string userid)
        {
            return DL_DailyMolassesProduction.GetDailyMolassesList(userid);
        }
        public static string InsertDailyProduction(List<DailyMolassesProduction_e> production)
        {
            return DL_DailyMolassesProduction.InsertDailyMolasses(production);
        }
        public static List<DalyMolasses_e> GetList(string userid)
        {
            return DL_DailyMolassesProduction.GetMolassesList(userid);
        }
        public static List<DalyMolasses_e> Search(string tablename, string column, string value)
        {
            return DL_DailyMolassesProduction.Search(tablename,column,value);
        }
        public static List<DailyMolassesProduction_e> GetProduction(string userid,string financial_year)
        {
            return DL_DailyMolassesProduction.GetPartyList(userid,financial_year);
        }
        public static List<DailyMolassesProduction_e> GetMolassesActionList(string party_code,string edate )
        {
            return DL_DailyMolassesProduction.GetActionList(party_code, edate);
        }
        public static string updateMolassesaction(List<DailyMolassesProduction_e> action)
        {
            return DL_DailyMolassesProduction.UpdateMolassis(action);

        }
        public static string Approve(string party_code, string edate, string remarks, string recordstatus,string molassesproduction_id,string userid, List<DailyMolassesProduction_e> production)
        {
            return DL_DailyMolassesProduction.Approve(party_code, edate, remarks, recordstatus, molassesproduction_id, userid,production);
        }

        public static DailyMolassesProduction_e GetProductionQTY(string partycode)
        {
            return DL_DailyMolassesProduction.GetProductionQTY(partycode);
        }

        public static List<DailyMolassesProduction_e> GetMolassesActionListWithDate(string party_code, string entrydate)
        {
            return DL_DailyMolassesProduction.GetActionList(party_code, entrydate);
        }
    }
}
