using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
   public class BL_RawMaterialReceipt
    {
        public static string InsertRawmaterial(List<RawMaterialReceipt> rawmaterial)
        {
            return DL_RawMaterialReceipt.InsertRaw(rawmaterial);
        }

        public static string Update(List<RawMaterialReceipt> rawmaterial)
        {
            return DL_RawMaterialReceipt.UpdateRawMaterial(rawmaterial);
        }
        public static string Adminupdate(List<RawMaterialReceipt> rawmaterial)
        {
            return DL_RawMaterialReceipt.Adminupdate(rawmaterial);
        }

        public static List<RawMaterialReceipt> GetRawmaterialList(string userid)
        {
            return DL_RawMaterialReceipt.GetRawMaterial(userid);
        }
        public static List<RawMaterialReceipt> Search1(string tablename, string column, string value)
        {
            return DL_RawMaterialReceipt.Search1(tablename, column, value);
        }
        public static List<RawMaterialReceipt> Search(string tablename, string column, string value)
        {
            return DL_RawMaterialReceipt.Search(tablename, column, value);
        }
        public static List<RawMaterialReceipt> GetMTPRaw(string userid)
        {
            return DL_RawMaterialReceipt.GetMTPRaw(userid);
        }


        public static List<VAT_Master> GetVatName(string userid)
        {
            return DL_RawMaterialReceipt.GetVat(userid);
        }

        public static bool InsertRMRStorage(RawMaterialReceipt RMRStorage)
        {
            return DL_RawMaterialReceipt.InsertRMRStorage(RMRStorage);
        }

        public static RawMaterialReceipt GetList(int receipt_id,string financial_year)
        {
            return DL_RawMaterialReceipt.GetList(receipt_id,financial_year);
        }

        //public static List<RawMaterialReceipt> GetGrainVats(string party_code, string receipt_id)
        //{
        //    return DL_RawMaterialReceipt.GetGrainVats(party_code, receipt_id);
        //}

        //public static List<RmrReceiptStorage> GetStorage(int receipt_id)
        //{
        //    return DL_RawMaterialReceipt.GetStorage(receipt_id);
        //}

        public static string Approve(List<RawMaterialReceipt> RAW)
        {
            return DL_RawMaterialReceipt.Approve(RAW);
        }

        public static List<RawMaterialReceipt> GetvatsList(string party_code,string receipt_id,string financial_year)
        {
            return DL_RawMaterialReceipt.GetvatsList(party_code, receipt_id,financial_year);
        }
        public static List<RawMaterialReceipt> GetvatsList1(string party_code, string receipt_id, string product)
        {
            return DL_RawMaterialReceipt.GetvatsList1(party_code, receipt_id,product);
        }

        public static object InsertRawmaterialGrain(List<RawMaterialReceipt> rawmaterial)
        {
            return DL_RawMaterialReceipt.InsertRawGrain(rawmaterial);
        }
        public static List<RawMaterialReceipt> GetGrainRawMaterial(string userid)
        {
            return DL_RawMaterialReceipt.GetGrainRawMaterial(userid);
        }
        public static List<RawMaterialReceipt> Search2(string tablename, string column, string value)
        {
            return DL_RawMaterialReceipt.Search2(tablename, column, value);
        }
            public static RawMaterialReceipt GetGainList(int receipt_id,string financial_year)
        {
            return DL_RawMaterialReceipt.GetGainList(receipt_id,financial_year);
        }
        public static List<RawMaterialReceipt> GetGrainvatsList(string party_code, string receipt_id, string product,string financial_year)
        {
            return DL_RawMaterialReceipt.GetGrainvatsList(party_code, receipt_id, product,financial_year);
        }
        public static int GetGrainData(string party_code)
        {
            return DL_RawMaterialReceipt.GetGrainData(party_code);
        }
        public static int GetData(string party_code)
        {
            return DL_RawMaterialReceipt.GetData(party_code);
        }
        public static object UpdateGrain(List<RawMaterialReceipt> rawmaterial)
        {
            return DL_RawMaterialReceipt.UpdateGrain(rawmaterial);
        }
        public static object AdminUpdateGrain(List<RawMaterialReceipt> rawmaterial)
        {
            return DL_RawMaterialReceipt.AdminUpdateGrain(rawmaterial);
        }
        }
}
