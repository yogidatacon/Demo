using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
   public class BL_Complaint
    {
        public static List<Complaint> Getlist()
        {
            return DL_Complaint.Getlist();
        }
        public static List<Complaint> Search(string tablename, string column, string value)
        {
            return DL_Complaint.Search(tablename, column, value);
        }
        public static string Insert(Complaint com)
        {
            return DL_Complaint.Insert(com);
        }
        public static string SuggestionInsert(Complaint com)
        {
            return DL_Complaint.SuggestionInsert(com);
        }
        public static string Update(Complaint com)
        {
            return DL_Complaint.Update(com);
        }
        public static string StatusUpdate(Complaint com)
        {
            return DL_Complaint.StatusUpdate(com);
        }
        public static int GetExistsData(string tablename, string column, int value)
        {
            return DL_Complaint.GetExistsData(tablename,column,value);
        }

        public static Complaint GetDetails( int dailydispatchclosure_id)
        {
            return DL_Complaint.GetDetails(dailydispatchclosure_id);
        }

       
    }
}
