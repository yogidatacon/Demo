using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
   public class BL_Form82
    {
        public static string Insert(Form82 Ferementer)
        {
            return DL_Form82.Insert(Ferementer);
        }
        public static Form82  Getvatavl(string vat_code, string party_code)
        {
            return DL_Form82.GetVatAval(vat_code, party_code);
        }

        public static ProductType Getproduct(string Vat_code, string party_Code)
        {
            return DL_Form82.Getproduct(Vat_code, party_Code);
        }

        public static List<Form82> GetList()
        {
            return DL_Form82.GetList();
        }
        public static List<Form82> Search(string tablename, string column, string value)
        {
            return DL_Form82.Search(tablename, column, value);
        }
        public static List<Form82> Getdistinctdate(string party)
        {
            return DL_Form82.Getdistinctdate(party);
        }
        public static Form82 GetDetails(int FermenterId,string financial_year)
        {
            return DL_Form82.GetDetails(FermenterId,financial_year);
        }

        public static string Approve(Form82 fermenter)
        {
            return DL_Form82.Approve(fermenter);
        }

        public static string Update(Form82 fermenter)
        {
            return DL_Form82.Update(fermenter);
        }

        public static int GetDup( string value, string value2)
        {
            return DL_Form82.GetDuplicket( value,value2);
        }
        public static Form82 Getfermentervat(string date, string Code)
        {
            return DL_Form82.Getfermentervat(date, Code);
        }

        public static int GetExistsData(string tablename, string date, string value)
        {
            return DL_Form82.GetExistsData(tablename, date, value);
        }

        public static Form82 GetSetupdate(string party, string date,string vatcode)
        {
            return DL_Form82.GetSetupdate(party, date,vatcode);
        }

        public static Form82 Getvatavl1(string vatcode1, string party_code1)
        {
            return DL_Form82.GetVatAval1(vatcode1, party_code1);
        }
    }
}
