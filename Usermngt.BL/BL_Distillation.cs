using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
   public class BL_Distillation
    {
        public static string Insert(Distillation Distillation)
        {
            return DL_Distillation.Insert(Distillation);
        }

        public static string Update(Distillation distillation)
        {
            return DL_Distillation.Update(distillation);
        }
        public static string AdminUpdate(Distillation distillation)
        {
            return DL_Distillation.AdminUpdate(distillation);
        }

        public static List<Distillation> GetList()
        {
            return DL_Distillation.GetList();
        }
        public static List<Distillation> Search(string tablename, string column, string value)
        {
            return DL_Distillation.Search(tablename, column, value);
        }

        public static Distillation GetDetails(int DistillationId,string financial_year)
        {
            return DL_Distillation.GetDetails(DistillationId,financial_year);
        }

       
        public static string Approve(Distillation distillation)
        {
            return DL_Distillation.Approve(distillation);
        }

        public static List<DistillationToStore> GetToStoreList(string date,string vatcode,string party, string setupdate)
        {
            return DL_Distillation.GetToStoreList(date,vatcode,party, setupdate);
        }

        public static List<Distillation> GetVat(string date,int setupid)
        {
            return DL_Distillation.Getvat(date,setupid);
        }

        public static Distillation Getvatavailableqty(string vatcode, string Code)
        {
            return DL_Distillation.Getvatavailableqty(vatcode, Code);
        }

        public static DistillationToStore Getreciverbl(string vatcode, string date, int rfid)
        {
            return DL_Distillation.Getreciverbl(vatcode,date,rfid);
        }

        public static List<Distillation> Getdate(string party_code)
        {
            return DL_Distillation.Getdate(party_code);
        }

        public static List<Distillation> GetSubmiteddate(int distillationid)
        {
            return DL_Distillation.GetSubmiteddate(distillationid);
        }

        public static List<Distillation> SetupGetList(string userid)
        {
            return DL_Distillation.SetupGetList(userid);
        }
        public static List<Distillation> SubmitedSetupGetList(string userid)
        {
            return DL_Distillation.SubmitedSetupGetList(userid);
        }

        }
}
