using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    public class BL_Molasses_Allocation
    {
        public static string Insert(Molasses_Allocation allotment)
        {
            return DL_Molasses_Allocation.Insert(allotment);
        }
        public static Molasses_Allocation GetMNTConsRegDetails(string id,string financial_year)
        {
            return DL_Molasses_Allocation.GetMNTConsRegDetails(id,financial_year);
        }

        public static string Insert_MTP_Con(Molasses_Allocation allotment)
        {
            return DL_Molasses_Allocation.Insert_MTP_Con(allotment);
        }

        public static string Insert_MTP_Issue(Molasses_Allocation allotment)
        {
            return DL_Molasses_Allocation.Insert_MTP_Issue(allotment);
        }
        public static string UpdateMNTIssueReg(Molasses_Allocation allotment)
        {
            return DL_Molasses_Allocation.UpdateMNTIssueReg(allotment);
        }
      
        public static string UpdateMNTConsumReg(Molasses_Allocation allotment)
        {
            return DL_Molasses_Allocation.UpdateMNTConsumReg(allotment);
        }

        public static List<Molasses_Allocation> GetMNTCONList()
        {
            return DL_Molasses_Allocation.GetMNTCONList();
        }

        public static Molasses_Allocation GetMNTIssueRegDetails(string id ,string financial_year)
        {
            return DL_Molasses_Allocation.GetMNTIssueRegDetails(id,financial_year);
        }



        public static List<Molasses_Allocation> GetMNTIssueList()
        {
            return DL_Molasses_Allocation.GetMNTIssueList();
        }
        public static List<Molasses_Allocation> GetMTPList()
        {
            return DL_Molasses_Allocation.GetMTPList();
        }
      

        public static string Update(Molasses_Allocation allotment)
        {
            return DL_Molasses_Allocation.Update(allotment);
        }
        public static List<Molasses_Allocation> GetList()
        {
            return DL_Molasses_Allocation.GetList();
        }
        public static List<Molasses_Allocation> Search(string tablename, string column, string value)
        {
            return DL_Molasses_Allocation.Search(tablename,column,value);
        }



        public static Molasses_Allocation GetDetails(string id,string financial_year)
        {
            return DL_Molasses_Allocation.GetDetails(id, financial_year);
        }
      

        public static Molasses_Allocation MNTP_Consumption_List(string id, string financial_year)
        {
            return DL_Molasses_Allocation.MNTP_Consumption_List(id,financial_year);
        }
        public static string Approve_MTNConsumption(Molasses_Allocation allotement)
        {
            return DL_Molasses_Allocation.Approve_MTNConsumption(allotement);
        }

        public static string Approve_MTNIssue(Molasses_Allocation allotement)
        {
            return DL_Molasses_Allocation.Approve_MTNIssue(allotement);
        }

        public static string Approve(Molasses_Allocation allotement)
        {
            return DL_Molasses_Allocation.Approve(allotement);
        }
        public static string ApproveMtpAllocation(Molasses_Allocation allotment)
        {
            return DL_Molasses_Allocation.ApproveMtpAllocation(allotment);
        }

        public static string adminupdate(Molasses_Allocation allotment)
        {
            return DL_Molasses_Allocation.adminupdate(allotment);
        }
        public static Molasses_Allocation GetQTY(string partycode, string vat)
        {
            return DL_Molasses_Allocation.GetQTY(partycode, vat);
        }
        public static int GetDigitalsignature(string userid)
        {
            return DL_Molasses_Allocation.GetDigitalsignature(userid);
        }
        public static string Issued(string allotment_ID,UserDetails user,string transaction_type,string financial_year)
        {
            return DL_Molasses_Allocation.Issued(allotment_ID, user, transaction_type, financial_year);
        }

        public static string GetValues(string value)
        {
            return DL_Molasses_Allocation.GetValues(value);
        }
    }
}
