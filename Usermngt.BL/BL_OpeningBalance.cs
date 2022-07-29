using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
   public class BL_OpeningBalance
    {
        public static List<OpeningBalance> GetOpeningBalanceList(string party_code,string cdate)
        {
            return DL_OpeningBalance.GetOpeningBalanceList(party_code, cdate);
        }
        public static List<OpeningBalance> Search(string tablename, string column, string value, string party_code, string party)
        {

            return DL_OpeningBalance.Search(tablename, column, value, party_code, party);
        }
        public static OpeningBalance GetOpeningBalance(string party)
        {
            return DL_OpeningBalance.GetOpeningBalance(party); ;
        }
        public static string InsertOpeningbalance(List<OpeningBalance> openingbalance)
        {
            return DL_OpeningBalance.InsertOpeningbalance(openingbalance); 
        }
        public static string UpdateOpeningBlance(List<OpeningBalance> openingbalance)
        {
            return DL_OpeningBalance.UpdateOpeningbalance(openingbalance);
        }
        public static string Approve(List<OpeningBalance> openingbalance)
        {
            return DL_OpeningBalance.Approve(openingbalance);
        }
        public static int GetExistsData(string tablename, string column, string value)
        {
            return DL_OpeningBalance.GetExistsData(tablename, column, value);
        }
        }
    }
